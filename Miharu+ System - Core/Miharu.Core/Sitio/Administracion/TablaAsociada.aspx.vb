Imports DBCore
Imports Miharu.Core.Clases

Namespace Sitio.Administracion

    Public Class TablaAsociada
        Inherits FormBase

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            grilla = grdData
            container = pnlDetalle
            Esquema = "config"
            Entidad = CStr(MySesion.Entidad.id)
            Tabla = "tbl_tabla_asociada"

            LoadAutomatic()
        End Sub
        Protected Sub Page_LoadComplete() Handles Me.LoadComplete
            Config_Page()
        End Sub

        Public Sub Config_Page()

            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                If Not IsPostBack Then
                    Dim data As New DataSet
                    data.Tables.Add(dbmCore.Schemadbo.CTA_tblcampo_tbltablaasociada.DBGet())
                    data.Tables.Add(dbmCore.Schemadbo.CTA_CampoBusqueda_Entidad.DBGet())
                    GlobalData = data

                    LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, Esquema, Tabla))

                    Llenacombo(fk_Entidad, dbmCore.Schemadbo.CTA_Entidad.DBGet(), "id_entidad", "nombre_entidad")
                    Llenacombo(fk_Campo_Tipo, dbmCore.SchemaConfig.TBL_Campo_Tipo.DBGet(Nothing), "id_campo_tipo", "nombre_campo_tipo")
                    Llenacombo(fk_Campo_Lista, dbmCore.SchemaConfig.TBL_Campo_Lista.DBGet(Nothing, Nothing), "id_campo_lista", "nombre_campo_lista")
                End If

                DropDownListCascading(fk_Documento, GlobalData.Tables(0), "id_documento", "nombre_documento", "fk_entidad='" & fk_Entidad.SelectedValue & "'")
                DropDownListCascading(fk_Campo, GlobalData.Tables(0), "id_Campo", "nombre_campo", "fk_entidad='" & fk_Entidad.SelectedValue & "'", "id_documento='" & fk_Documento.SelectedValue & "'")
                DropDownListCascading(fk_Campo_Busqueda, GlobalData.Tables(1), "fk_campo_busqueda", "nombre_campo_busqueda", "fk_entidad='" & fk_Entidad.SelectedValue & "'", "fk_campo_tipo='" & fk_Campo_Tipo.SelectedValue & "'")

            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

        Public Sub CoreGridViewChange()
        End Sub

        Public Sub Validar()
            Try
                If fk_Entidad.SelectedValue = "-1" Or fk_Entidad.SelectedValue = "" Then Throw New Exception("Debe seleccionar una entidad.")
                If fk_Documento.SelectedValue = "-1" Or fk_Documento.SelectedValue = "" Then Throw New Exception("Debe seleccionar una documento.")
                If fk_Campo.SelectedValue = "-1" Or fk_Campo.SelectedValue = "" Then Throw New Exception("Debe seleccionar un campo.")
                If fk_Campo_Tipo.SelectedValue = "-1" Or fk_Campo_Tipo.SelectedValue = "" Then Throw New Exception("Debe seleccionar un tipo de campo.")
            Catch ex As Exception
                Throw
            End Try
        End Sub

#Region "Declaraciones"

        Dim Esquema As String
        Dim Entidad As String
        Dim Tabla As String
        Dim container As Object
        Dim grilla As CoreGridView

#End Region
#Region "Automatic"

        Public Sub LoadAutomatic()
            If Not IsPostBack Then
                Dim imgB As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgSave"), ImageButton)
                imgB.ValidationGroup = "Guardar"
                Session("TableForm") = DataDictionary(Esquema, Tabla)
            End If
        End Sub

        Protected Sub btnGuardar_Click() Handles MyBase.CommandActionSave
            Try
                Validar()
                Dim table As DataTable = CType(Session("TableForm"), DataTable)

                If SaveType = SaveType.Insert Then CreateInsert(MySesion.Usuario.id, container, table)
                If SaveType = SaveType.Update Then CreateUpdate(MySesion.Usuario.id, container, table)

                CurrentMasterTab = MasterTabType.Grid
                LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, Esquema, Tabla))
                LimpiarControles(container, table, Entidad)
            Catch ex As Exception
                MyMasterPage.ShowMessage(ex.Message, MsgBoxIcon.IconWarning, "")
            End Try
        End Sub
        Protected Sub btnEliminar_Click() Handles MyBase.CommandActionDelete
            Try
                Dim table As DataTable = CType(Session("TableForm"), DataTable)

                CreateDelete(MySesion.Usuario.id, container, table)
                CurrentMasterTab = MasterTabType.Grid
                LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, Esquema, Tabla))
                LimpiarControles(container, table, Entidad)
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub
        Protected Sub BtnNuevo_Click() Handles MyBase.CommandActionNew
            Try
                Dim table As DataTable = CType(Session("TableForm"), DataTable)

                LimpiarControles(container, table, Entidad)
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
                    CoreGridViewLinkControls(GridData, grilla.SelectedIndex, container)
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