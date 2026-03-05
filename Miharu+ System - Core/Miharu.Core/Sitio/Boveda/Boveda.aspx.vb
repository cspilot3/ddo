Imports DBCore
Imports Miharu.Core.Clases

Namespace Sitio.Boveda

    Public Class Boveda
        Inherits FormBase

#Region "Declaraciones"
        Private Const MyPathPermiso As String = "3.1"
        Private nEntidad As Short
        Private container As Object
        Private Const schema As String = "custody"
        Private Const table As String = "TBL_Boveda"
        Private dtSecciones As DataTable
#End Region

#Region "Eventos"

        Protected Sub CentrosProcesamiento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
            Try
                If id_Boveda.Text = "" Then
                    MyMasterPage.ShowMessage("Debe guardar primero antes de configurar los centros de procesamiento", MsgBoxIcon.IconInformation)
                Else
                    Dim parametros As New ParameterCollection
                    parametros.Add("fk_entidad", fk_Entidad.SelectedValue)
                    parametros.Add("fk_sede", fk_Sede.SelectedValue)
                    parametros.Add("id_Boveda", id_Boveda.Text)

                    GlobalParameterCollection = parametros
                    OpenModalDialog("Save", "../sitio/Boveda/p_Centros_Procesamiento.aspx", "msgCentrosProcesamiento", "Centros de procesamiento", 600, 350)
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            container = pnlDetalle
            nEntidad = MySesion.Entidad.id

            Config_Page()

            If Not IsPostBack Then
                Carga_Campos_Hijos(fk_Entidad)

                Dim imgB As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgSave"), ImageButton)
                imgB.ValidationGroup = "Guardar"
                Session("TableForm") = DataDictionary(schema, table)
            Else
                dtSecciones = CType(Session("dtSecciones"), DataTable)
            End If
        End Sub

        Private Sub ImgAgregarSeccion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAgregarSeccion.Click
            Try
                If id_Boveda.Text <> "" Then
                    Dim parametros As New ParameterCollection
                    parametros.Add("fk_entidad", fk_Entidad.SelectedValue)
                    parametros.Add("fk_sede", fk_Sede.SelectedValue)
                    parametros.Add("id_Boveda", id_Boveda.Text)
                    parametros.Add("id_Seccion", "-1")

                    GlobalParameterCollection = parametros
                    OpenModalDialog("Save", "../sitio/Boveda/p_Secciones.aspx", "msgSeccion", "Secciones", 600, 350)
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub GrdSecciones_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdSecciones.RowCommand
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Try
                dbmCore = New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                If e.CommandSource.GetType() Is GetType(Web.UI.WebControls.ImageButton) Then
                    Dim idEliminar As Integer = CInt(dtSecciones.Rows(CType(sender, CoreGridView).PreSelectedIndex).Item("Id_Boveda_Seccion"))

                    dbmCore.SchemaCustody.TBL_Boveda_Seccion.DBDelete(CShort(fk_Entidad.SelectedValue), CShort(fk_Sede.SelectedValue), CShort(id_Boveda.Text), CShort(idEliminar))

                    Config_Page()
                    Carga_Campos_Hijos(fk_Entidad)
                    CurrentMasterTab = MasterTabType.Grid
                End If
            Catch ex As Exception
                Dim msg As String = valida_Excepcion(ex)
                If msg <> "" Then
                    MyMasterPage.ShowMessage(msg, Miharu.Core.MsgBoxIcon.IconWarning, "Error al eliminar Registro")
                Else
                    showErrorPage(ex)
                End If
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub GrdSecciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSecciones.SelectedIndexChanged
            Try
                If grdSecciones.PreSelectedIndex > -1 Then
                    Dim parametros As New ParameterCollection
                    parametros.Add("fk_entidad", fk_Entidad.SelectedValue)
                    parametros.Add("fk_sede", fk_Sede.SelectedValue)
                    parametros.Add("id_Boveda", id_Boveda.Text)
                    parametros.Add("id_Seccion", grdSecciones.Rows(grdSecciones.PreSelectedIndex).Cells(1).Text)

                    GlobalParameterCollection = parametros
                    OpenModalDialog("Save", "../sitio/Boveda/p_Secciones.aspx", "msgSeccion", "Secciones", 600, 350)
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub Boveda_ModalClosed(ByVal parameters As String) Handles Me.ModalClosed
            Try
                Select Case ModalDialogNombre
                    Case "msgSeccion"
                        If CStr(MySesion.Pagina.Parameter("RESPUESTA")) = "SI" Then
                            CargaDataSeccion()
                        End If

                    Case "msgDelete"
                        Dim nTable As DataTable = CType(Session("TableForm"), DataTable)
                        CreateDelete(MySesion.Usuario.id, container, nTable)
                        CurrentMasterTab = MasterTabType.Grid
                End Select
            Catch ex As Exception
                Dim msg As String = valida_Excepcion(ex)
                If msg <> "" Then
                    MyMasterPage.ShowMessage(msg, Miharu.Core.MsgBoxIcon.IconWarning, "Error al eliminar Registro")
                Else
                    showErrorPage(ex)
                End If
            End Try
        End Sub

        Protected Sub Fk_Entidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles fk_Entidad.SelectedIndexChanged
            Try
                Carga_Campos_Hijos(fk_Entidad)
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

#End Region

#Region "Métodos"

        Private Sub Config_Page()
            Dim dmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            dmCore.Connection_Open(MySesion.Usuario.id)

            Try
                If Not IsPostBack Then
                    Dim data As New DataSet
                    data.Tables.Add(dmCore.SchemaSecurity.CTA_Sede.DBGet())
                    data.Tables.Add(dmCore.SchemaSecurity.CTA_Centro_Procesamiento.DBGet())
                    GlobalData = data

                    'Carga grilla
                    LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, schema, table))

                    Dim dtEntidades = dmCore.Schemadbo.CTA_Entidad.DBFindByid_Entidad(Nothing)
                    Llenacombo(fk_Entidad, dtEntidades, dtEntidades.id_EntidadColumn.ColumnName, dtEntidades.Nombre_EntidadColumn.ColumnName)
                    fk_Entidad.SelectedValue = CStr(nEntidad)
                    fk_Entidad.Enabled = AccesoEntidad
                End If

            Catch ex As Exception
                showErrorPage(ex)
            End Try

            dmCore.Connection_Close()
        End Sub

        Private Sub Carga_Campos_Hijos(ByVal nfk_Entidad As DropDownList)
            Dim dmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            dmCore.Connection_Open(MySesion.Usuario.id)

            If CInt(nfk_Entidad.SelectedValue) > -1 Then
                Dim dtSede As DataTable = dmCore.Schemadbo.CTA_Sede.DBFindByfk_Entidad(CShort(nfk_Entidad.SelectedValue))
                Fill_ListControl(CType(fk_Sede, DropDownList), dtSede, "Nombre_Sede", "Id_Sede", True, "Seleccione ...", "-1", "fk_Sede")
            End If

            dmCore.Connection_Close()
        End Sub

#End Region

#Region "Automatic"

        Protected Sub BtnGuardar_Click() Handles MyBase.CommandActionSave
            Try
                Dim dtTable As DataTable = CType(Session("TableForm"), DataTable)

                If SaveType = SaveType.Insert Then CreateInsert(MySesion.Usuario.id, container, dtTable)
                If SaveType = SaveType.Update Then CreateUpdate(MySesion.Usuario.id, container, dtTable)

            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub
        Protected Sub BtnEliminar_Click() Handles MyBase.CommandActionDelete
            Try
                If (grdData.PreSelectedIndex > -1) Then
                    MySesion.Pagina.Parameter("mensaje") = "¿Desea realmente elminar el registro?. Recuerde que los datos serán elminados permanentemente."
                    OpenModalDialog("CampoLista", "../Controles/Confirmacion.aspx", "msgDelete", "Eliminar Registro", 550, 150)
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub
        Protected Sub BtnNuevo_Click() Handles MyBase.CommandActionNew
            Try
                'LimpiarControles(container, table, CStr(nEntidad))
                Clear_Controls(CType(pnlDetalle, UI.Control))

                dtSecciones = New DataTable()
                Session("dtSecciones") = dtSecciones

                grdSecciones.DataSource = dtSecciones
                grdSecciones.DataBind()


                'Por defecto la entidad Actual
                fk_Entidad.SelectedValue = CStr(nEntidad)

                CurrentMasterTab = MasterTabType.Detail
                SaveType = SaveType.Insert
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub
        Protected Sub GrdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdData.SelectedIndexChanged
            IndexChanged()
        End Sub
        Protected Sub BtnEdit_Click() Handles MyBase.CommandActionEdit
            IndexChanged()
        End Sub

        Public Sub IndexChanged()
            Try
                If grdData.SelectedIndex > -1 Then
                    CoreGridViewLinkControls(GridData, grdData.SelectedIndex, container)
                    CargaDataSeccion()
                    CurrentMasterTab = MasterTabType.Detail
                    SaveType = SaveType.Update
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub CargaDataSeccion()
            Dim dmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            dmCore.Connection_Open(MySesion.Usuario.id)

            Try
                'Buscar las secciones de la bóveda actual
                dtSecciones = dmCore.SchemaCustody.TBL_Boveda_Seccion.DBFindByfk_Entidadfk_Sedefk_Boveda(CShort(fk_Entidad.SelectedValue), CShort(fk_Sede.SelectedValue), CShort(id_Boveda.Text))

                grdSecciones.DataSource = dtSecciones
                grdSecciones.DataBind()
                Session("dtSecciones") = dtSecciones
                MyMasterPage.MasterDetailPanel.Update()
            Catch ex As Exception
                showErrorPage(ex)
            End Try

            dmCore.Connection_Close()
        End Sub

        Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
            If Not IsPostBack Then
                MyMasterPage.MasterTabContainer.Tabs(0).Enabled = False
                CurrentMasterTab = MasterTabType.Grid
            End If

            NumRegistros.Text = "Número de registros: " & grdData.Rows.Count
        End Sub

#End Region


    End Class
End Namespace