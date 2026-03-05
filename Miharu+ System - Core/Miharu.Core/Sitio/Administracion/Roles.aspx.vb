Imports Miharu.Core.Clases
Imports DBCore

Namespace Sitio.Administracion

    Public Class Roles
        Inherits FormBase

#Region "Declaraciones"

        Private _esquema As String
        Private _entidad As String
        Private _tabla As String

        Dim idRol As Short
        Dim EntidadesDataTable As New SchemaSecurity.CTA_EntidadDataTable
        Dim ProyectosDataTable As New SchemaConfig.TBL_ProyectoDataTable
        Dim RolEsquemaDatatable As New DataTable

        Public Property Esquema As String
            Get
                Return _esquema
            End Get
            Set(value As String)
                _esquema = value
            End Set
        End Property

        Public Property Entidad As String
            Get
                Return _entidad
            End Get
            Set(value As String)
                _entidad = value
            End Set
        End Property

        Public Property Tabla As String
            Get
                Return _tabla
            End Get
            Set(value As String)
                _tabla = value
            End Set
        End Property

#End Region

#Region "Eventos"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Esquema = "Security"
            Tabla = "TBL_Rol_Esquema"
            Entidad = CStr(MySesion.Entidad.id)
            idRol = CShort(MySesion.Parameter("idRol"))
            RolEsquemaDatatable = CType(MySesion.Parameter("RolEsquemaDatatable"), DataTable)
            config()
        End Sub

        Protected Sub Page_LoadComplete() Handles Me.LoadComplete
            Config_Page()
        End Sub

        Protected Sub btnGuardar_Click() Handles MyBase.CommandActionSave
            GuardarCambios()
        End Sub

        Protected Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdData.SelectedIndexChanged
            IndexChanged()
        End Sub

        Private Sub EntidadDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles EntidadDropDownList.SelectedIndexChanged
            CargaProyectos(CShort(EntidadDropDownList.SelectedValue))
            cargarGrillaEsquemas()
        End Sub

        Private Sub ProyectoDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProyectoDropDownList.SelectedIndexChanged
            cargarGrillaEsquemas()
        End Sub

#End Region

#Region "Metodos"

        Public Sub IndexChanged()
            Dim dmCore As New DBCoreDataBaseManager(ConnectionString.Core)

            Try
                If grdData.SelectedIndex > -1 Then
                    dmCore.Connection_Open(MySesion.Usuario.id)

                    CurrentMasterTab = MasterTabType.Detail
                    idRol = CShort(GridData.Rows(grdData.SelectedIndex)("id_Rol"))
                    RolEsquemaDatatable = dmCore.SchemaProcess.PA_Esquemas_Rol.DBExecute(idRol, Nothing, Nothing)

                    MySesion.Parameter("idRol") = idRol
                    MySesion.Parameter("RolEsquemaDatatable") = RolEsquemaDatatable

                    dmCore.Connection_Close()
                    cargarGrillaEsquemas()
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Public Sub config()
            Dim dmcore As New DBCoreDataBaseManager(ConnectionString.Core)
            dmcore.Connection_Open(MySesion.Usuario.id)
            If Not IsPostBack Then

                Dim Table = dmcore.SchemaSecurity.CTA_Rol.DBGet()

                GridData = Table
                grdData.DataSource = GridData
                grdData.DataBind()
            End If

            EntidadesDataTable = dmcore.SchemaSecurity.CTA_Entidad.DBGet()
            ProyectosDataTable = dmcore.SchemaConfig.TBL_Proyecto.DBFindByfk_Entidad(Nothing)

            dmcore.Connection_Close()

            NumRegistros.Text = "Número de registros: " & grdData.Rows.Count
        End Sub

        Public Sub cargarGrillaEsquemas()
            Try
                Dim dmcore As New DBCoreDataBaseManager(ConnectionString.Core)
                dmcore.Connection_Open(MySesion.Usuario.id)

                Dim EntidadFiltro As Slyg.Tools.SlygNullable(Of String)
                Dim ProyectoFiltro As Slyg.Tools.SlygNullable(Of String)

                If EntidadDropDownList.SelectedValue = "-1" Then
                    EntidadFiltro = Nothing
                Else
                    EntidadFiltro = EntidadDropDownList.SelectedValue & " - " & EntidadDropDownList.SelectedItem.Text
                End If

                If ProyectoDropDownList.SelectedValue = "-1" Then
                    ProyectoFiltro = Nothing
                Else
                    ProyectoFiltro = ProyectoDropDownList.SelectedValue & " - " & ProyectoDropDownList.SelectedItem.Text
                End If

                Dim RolesEsquema As New DataTable
                If Not IsNothing(RolEsquemaDatatable) Then
                    Dim RolesEsquemaView = RolEsquemaDatatable.DefaultView
                    If Not IsNothing(EntidadFiltro) AndAlso Not (EntidadFiltro.IsNull) Then RolesEsquemaView.RowFilter = "nombre_entidad='" & EntidadFiltro.ToString() & "'"
                    If Not IsNothing(ProyectoFiltro) AndAlso Not (ProyectoFiltro.IsNull) Then
                        If RolesEsquemaView.RowFilter = "" Then
                            RolesEsquemaView.RowFilter = RolesEsquemaView.RowFilter & "nombre_proyecto='" & ProyectoFiltro.ToString() & "'"
                        Else
                            RolesEsquemaView.RowFilter = RolesEsquemaView.RowFilter & "AND nombre_proyecto='" & ProyectoFiltro.ToString() & "'"
                        End If
                    End If

                    RolesEsquema = RolesEsquemaView.ToTable
                End If

                dmcore.Connection_Close()

                grdEsquemas.DataSource = RolesEsquema
                grdEsquemas.DataBind()

                For Each row As GridViewRow In grdEsquemas.Rows
                    CType(row.FindControl("Aplica"), CheckBox).Checked = CBool(RolesEsquema(row.RowIndex)("Aplica"))
                Next

            Catch ex As Exception
                Throw
            End Try
        End Sub

        Private Sub Config_Page()
            If Not IsPostBack Then
                Dim imgDelete As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgDelete"), ImageButton)
                imgDelete.Attributes.Add("style", "visibility:hidden")

                Dim imgFind As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgFind"), ImageButton)
                imgFind.Attributes.Add("style", "visibility:hidden")

                MyMasterPage.MasterTabContainer.Tabs(0).Enabled = False
                CurrentMasterTab = MasterTabType.Grid

                Dim dmcore As New DBCoreDataBaseManager(ConnectionString.Core)
                dmcore.Connection_Open(MySesion.Usuario.id)


                'Combo Entidades
                Llenacombo(EntidadDropDownList, EntidadesDataTable, EntidadesDataTable.id_EntidadColumn.ColumnName, EntidadesDataTable.Nombre_EntidadColumn.ColumnName)

                'Compo Proyectos
                CargaProyectos(CShort(EntidadDropDownList.SelectedValue))

                cargarGrillaEsquemas()

                dmcore.Connection_Close()
            End If
        End Sub

        Private Sub grdEsquemas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdEsquemas.RowCommand
            Try
                If e.CommandSource.GetType() Is GetType(Web.UI.WebControls.ImageButton) Then
                    Dim parameter As New ParameterCollection
                    parameter.Add("id_Entidad", Split(grdEsquemas.Rows(grdEsquemas.PreSelectedIndex).Cells(1).Text, "-")(0))
                    parameter.Add("id_Proyecto", Split(grdEsquemas.Rows(grdEsquemas.PreSelectedIndex).Cells(2).Text, "-")(0))
                    parameter.Add("id_Esquema", Split(grdEsquemas.Rows(grdEsquemas.PreSelectedIndex).Cells(3).Text, "-")(0))
                    parameter.Add("id_Rol", CStr(idRol))
                    GlobalParameterCollection = parameter

                    OpenModalDialog("Save", "../sitio/Administracion/p_Roles.aspx", "msgDocumentos", "Documentos Roles", 600, 350)
                End If
            Catch ex As Exception
                Dim msg As String = valida_Excepcion(ex)
                If msg <> "" Then
                    MyMasterPage.ShowMessage(msg, Miharu.Core.MsgBoxIcon.IconWarning, "Error al abrir documentos")
                Else
                    showErrorPage(ex)
                End If
            End Try
        End Sub

        Public Sub GuardarCambios()
            Dim dmcore As New DBCoreDataBaseManager(ConnectionString.Core)
            dmcore.Connection_Open(MySesion.Usuario.id)

            Try
                dmcore.Transaction_Begin()

                Dim nEntidad As Short
                Dim nProyecto As Short
                Dim nEsquema As Short
                Dim activo As Boolean

                For Each Row As GridViewRow In grdEsquemas.Rows
                    nEntidad = CShort(Split(CType(Row.FindControl("Entidad"), Label).Text, "-")(0))
                    nProyecto = CShort(Split(CType(Row.FindControl("Proyecto"), Label).Text, "-")(0))
                    nEsquema = CShort(Split(CType(Row.FindControl("Esquema"), Label).Text, "-")(0))

                    activo = CType(Row.FindControl("Aplica"), CheckBox).Checked
                    If activo = True Then
                        Try
                            dmcore.SchemaSecurity.TBL_Rol_Esquema.DBInsert(idRol, nEntidad, nProyecto, nEsquema)
                        Catch : End Try
                    Else
                        dmcore.SchemaSecurity.TBL_Rol_Esquema.DBDelete(idRol, nEntidad, nProyecto, nEsquema)
                    End If
                Next

                dmcore.Transaction_Commit()
                CurrentMasterTab = MasterTabType.Grid
            Catch ex As Exception
                dmcore.Transaction_Rollback()
                MyMasterPage.ShowMessage("Ha ocurrido un problema al guardar los cambios, por favor comuniquese con el administrador.", Miharu.Core.MsgBoxIcon.IconWarning, "Error guardando cambios")
            Finally
                dmcore.Connection_Close()
            End Try

        End Sub

        Private Sub CargaProyectos(ByVal nEntidad As Short)
            Try
                Dim ProyectosView = ProyectosDataTable.DefaultView
                ProyectosView.RowFilter = "fk_Entidad=" & nEntidad

                Dim Proyectos = ProyectosView.ToTable()
                Llenacombo(ProyectoDropDownList, Proyectos, "id_Proyecto", "Nombre_Proyecto")
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

#End Region

    End Class
End Namespace