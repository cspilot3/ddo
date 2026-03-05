Imports DBCore
Imports Miharu.Core.Clases

Namespace Sitio.Administracion

    Public Class Servidor_Volumen_Imaging
        Inherits FormBase

#Region "Declaraciones"
        Private Const MyPathPermiso As String = "1.5.3"
        Public nEntidad As Short
        Dim container As Object
        Public schema As String = "imaging"
        Public table As String = "TBL_Servidor_Volumen"
#End Region

#Region "Eventos"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            container = pnlDetalle
            If Not IsPostBack Then
                Config_Page()
                Dim imgB As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgSave"), ImageButton)
                imgB.ValidationGroup = "Guardar"
                Session("TableForm") = DataDictionary(schema, table)
            End If
        End Sub

        Private Sub fk_Entidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fk_Entidad.SelectedIndexChanged
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
                nEntidad = MySesion.Entidad.id

                'Carga grilla
                LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, schema, table))

                Dim dtEntidad As DataTable = dmCore.Schemadbo.CTA_Entidad.DBFindByid_Entidad(Nothing)
                Fill_ListControl(CType(fk_Entidad, DropDownList), dtEntidad, "Nombre_Entidad", "id_Entidad", False, "", "", "fk_Entidad")
                fk_Entidad.SelectedValue = CStr(nEntidad)
                fk_Entidad.Enabled = AccesoEntidad

                Carga_Campos_Hijos(fk_Entidad)

            Catch ex As Exception
                showErrorPage(ex)
            End Try

            dmCore.Connection_Close()
        End Sub

        Public Sub CoreGridViewChange()
            'Llenacombo(fk_Campo_Busqueda, DBM.SchemaConfig.TBL_Campo_Busqueda.DBFindByfk_Campo_Tipo(Nothing), "id_campo_busqueda", "nombre_campo_busqueda")
        End Sub
        Private Sub Carga_Campos_Hijos(ByVal nfkEntidad As DropDownList)
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                If CInt(nfkEntidad.SelectedValue) > -1 Then
                    Dim dtServidor As DataTable = dbmCore.SchemaImaging.TBL_Servidor.DBFindByfk_Entidad(CShort(nfkEntidad.SelectedValue))
                    Fill_ListControl(CType(fk_Servidor, DropDownList), dtServidor, "Nombre_Servidor", "Id_Servidor", True, "Seleccione ...", "-1", "fk_Servidor")
                End If
            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

#End Region

#Region "Automatic"

        Protected Sub btnGuardar_Click() Handles MyBase.CommandActionSave
            Try
                Dim dtTable As DataTable = CType(Session("TableForm"), DataTable)

                If SaveType = SaveType.Insert Then CreateInsert(MySesion.Usuario.id, container, dtTable)
                If SaveType = SaveType.Update Then CreateUpdate(MySesion.Usuario.id, container, dtTable)

                CurrentMasterTab = MasterTabType.Grid
                LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, schema, table))
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub
        Protected Sub btnEliminar_Click() Handles MyBase.CommandActionDelete
            Try
                Dim nTable As DataTable = CType(Session("TableForm"), DataTable)

                CreateDelete(MySesion.Usuario.id, container, nTable)
                CurrentMasterTab = MasterTabType.Grid
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub
        Protected Sub BtnNuevo_Click() Handles MyBase.CommandActionNew
            Try
                'LimpiarControles(container, table, CStr(nEntidad))
                Clear_Controls(CType(pnlDetalle, UI.Control))
                CurrentMasterTab = MasterTabType.Detail
                SaveType = SaveType.Insert
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub
        Protected Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdData.SelectedIndexChanged
            IndexChanged()
        End Sub
        Protected Sub BtnEdit_Click() Handles MyBase.CommandActionEdit
            IndexChanged()
        End Sub
        Public Sub IndexChanged()
            Try
                If grdData.SelectedIndex > -1 Then
                    CoreGridViewChange()
                    CoreGridViewLinkControls(GridData, grdData.SelectedIndex, container)
                    CurrentMasterTab = MasterTabType.Detail
                    SaveType = SaveType.Update
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
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