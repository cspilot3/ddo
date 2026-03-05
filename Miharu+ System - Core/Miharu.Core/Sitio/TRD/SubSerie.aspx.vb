Imports DBCore
Imports Miharu.Core.Clases

Namespace Sitio.TRD

    Public Class SubSerie
        Inherits FormBase

#Region "Declaraciones"
        Private Const MyPathPermiso As String = "5.3"
        Public nEntidad As Short
        Dim container As Object
        Public schema As String = "config"
        Public table As String = "TBL_TRD_Subserie"
#End Region

#Region "Eventos"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            container = pnlDetalle
            nEntidad = MySesion.Entidad.id
            If Not IsPostBack Then
                Config_Page()

                Dim imgB As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgSave"), ImageButton)
                imgB.ValidationGroup = "Guardar"
                Session("TableForm") = DataDictionary(schema, table)
            End If
        End Sub

        Private Sub fk_TRD_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fk_TRD.SelectedIndexChanged
            Try
                Carga_Campos_Hijos(fk_TRD)
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub
#End Region

#Region "Métodos"
        Private Sub Config_Page()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                'Carga grilla
                LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, schema, table))
                Dim dtTRD As DataTable = Nothing

                'If AccesoEntidad = True Then
                '    dtTRD = dbmCore.SchemaConfig.TBL_TRD.DBFindByfk_Entidad(Nothing)
                'Else
                '    dtTRD = dbmCore.SchemaConfig.TBL_TRD.DBFindByfk_Entidad(nEntidad)
                'End If
                dtTRD = dbmCore.SchemaConfig.TBL_TRD.DBFindByfk_Entidad(Nothing)

                Fill_ListControl(CType(fk_TRD, DropDownList), dtTRD, "nombre_TRD", "id_TRD", False, "", "", "fk_TRD")

                Carga_Campos_Hijos(fk_TRD)

                Dim dtDependnecia As DataTable = dbmCore.Schemadbo.CTA_Dependencia.DBFindByfk_Entidad(nEntidad)
                Fill_ListControl(CType(fk_Dependencia, DropDownList), dtDependnecia, "Nombre_Dependencia", "id_Dependencia", True, "Seleccione ...", "-1", "fk_Dependencia")

            Catch ex As Exception
                showErrorPage(ex)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub CoreGridViewChange()
        End Sub

        Private Sub Carga_Campos_Hijos(ByVal nfk_TRD As DropDownList)
            Dim dmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            dmCore.Connection_Open(MySesion.Usuario.id)

            If CInt(nfk_TRD.SelectedValue) > -1 Then
                Dim dtSerie As DataTable = dmCore.SchemaConfig.TBL_TRD_Serie.DBFindByfk_TRD(Short.Parse(nfk_TRD.SelectedValue))
                Fill_ListControl(CType(fk_TRD_Serie, DropDownList), dtSerie, "Nombre_TRD_Serie", "Id_TRD_Serie", True, "Seleccione ...", "-1", "ddlDocumento")
            End If

            dmCore.Connection_Close()
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