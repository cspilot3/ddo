Imports DBCore
Imports Miharu.Core.Clases
Imports Slyg.Tools

Namespace Sitio.Administracion

    Public Class Campo_Lista_Item
        Inherits FormBase

#Region "Declaraciones"

        Private Const MyPathPermiso As String = "1.4.7"
        Public nEntidad As Short
        Dim container As Object
        Public schema As String = "config"
        Public table As String = "TBL_Campo_Lista"

        Private dtItemLista As DataTable
#End Region

#Region "Eventos"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            container = pnlDetalle
            If Not IsPostBack Then
                Config_Page()

                Dim imgB As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgSave"), ImageButton)
                imgB.ValidationGroup = "Guardar"
                Session("TableForm") = DataDictionary(schema, table)
            Else
                Load_Data()
            End If
            Etiqueta_Campo_Lista_Item.Focus()
        End Sub

        Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgregar.Click
            If Etiqueta_Campo_Lista_Item.Text <> "" And Valor_Campo_Lista_Item.Text <> "" Then
                agregarItem()
            Else
                MyMasterPage.ShowMessage("La etiqueta y el valor son obligatorios", MsgBoxIcon.IconInformation, "Items")
            End If
        End Sub

        Private Sub grdListaItem_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdListaItem.RowCommand
            Try
                If e.CommandSource.GetType() Is GetType(Web.UI.WebControls.ImageButton) Then
                    dtItemLista.Rows(CType(sender, CoreGridView).PreSelectedIndex).Delete()
                    dtItemLista.AcceptChanges()
                    grdListaItem.DataSource = dtItemLista
                    grdListaItem.DataBind()
                    Session("dtItemLista") = dtItemLista
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub Campo_Lista_Item_ModalClosed(ByVal parameters As String) Handles Me.ModalClosed
            Try
                Select Case ModalDialogNombre
                    Case "msgDelete"
                        If CStr(MySesion.Pagina.Parameter("RESPUESTA")) = "Si" Then
                            EliminarRegistro()
                        End If
                End Select
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub Valor_Campo_Lista_Item_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Valor_Campo_Lista_Item.TextChanged
            If Etiqueta_Campo_Lista_Item.Text <> "" And Valor_Campo_Lista_Item.Text <> "" Then
                agregarItem()
            Else
                MyMasterPage.ShowMessage("La etiqueta y el valor son obligatorios", MsgBoxIcon.IconInformation, "Items")
            End If
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

                Dim dtEntidades As DataTable = dmCore.Schemadbo.CTA_Entidad.DBFindByid_Entidad(Nothing)
                Fill_ListControl(CType(fk_Entidad, DropDownList), dtEntidades, "Nombre_Entidad", "id_Entidad", False, "", "", "fk_Entidad")
                fk_Entidad.SelectedValue = CStr(nEntidad)
                fk_Entidad.Enabled = AccesoEntidad


                'Crea la estructura del DTListaItem
                dtItemLista = New DataTable()
                dtItemLista.Columns.Add("Etiqueta_Campo_Lista_Item")
                dtItemLista.Columns.Add("Valor_Campo_Lista_Item")
                dtItemLista.Columns.Add("id_Campo_Lista_Item")
                Session("dtItemLista") = dtItemLista
            Catch ex As Exception
                showErrorPage(ex)
            End Try

            dmCore.Connection_Close()
        End Sub

        Private Sub Load_Data()
            Try
                dtItemLista = CType(Session("dtItemLista"), DataTable)
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub agregarItem()
            Try
                Dim dr As DataRow = dtItemLista.NewRow()
                dr("Etiqueta_Campo_Lista_Item") = Etiqueta_Campo_Lista_Item.Text
                dr("Valor_Campo_Lista_Item") = Valor_Campo_Lista_Item.Text
                dtItemLista.Rows.Add(dr)

                Session("dtItemLista") = dtItemLista
                grdListaItem.DataSource = dtItemLista
                grdListaItem.DataBind()

                Etiqueta_Campo_Lista_Item.Text = ""
                Valor_Campo_Lista_Item.Text = ""
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub EliminarRegistro()
            Dim dmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            Try
                dmCore.Connection_Open(MySesion.Usuario.id)

                'Elimina Items
                dmCore.Schemadbo.PA_BASIC_TBL_Campo_Lista_Item_Delete.DBExecute(CShort(fk_Entidad.SelectedValue), CShort(id_Campo_Lista.Text))

                'Elimina la lista
                dmCore.SchemaConfig.TBL_Campo_Lista.DBDelete(CShort(fk_Entidad.SelectedValue), CShort(id_Campo_Lista.Text))

                dmCore.Connection_Close()

                'Carga grilla
                LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, schema, table))
                MyMasterPage.MasterFilterPanel.Update()
                CurrentMasterTab = MasterTabType.Grid
            Catch ex As Exception
                Dim msg As String = valida_Excepcion(ex)
                If msg <> "" Then
                    MyMasterPage.ShowMessage(msg, Miharu.Core.MsgBoxIcon.IconWarning, "Error al eliminar Registro")
                Else
                    showErrorPage(ex)
                End If
            End Try
        End Sub
#End Region

#Region "Eventos Estándares"
        Protected Sub btnGuardar_Click() Handles MyBase.CommandActionSave
            Dim dmcore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            Try
                Select Case SaveType
                    Case SaveType.Insert
                        'Calcula el next Id

                        dmcore.Connection_Open(MySesion.Usuario.id)
                        dmcore.Transaction_Begin()

                        'Dim idCampoLista As Integer = dm.SchemaConfig.TBL_Campo_Lista.DBNextId_for_fk_Entidad(CShort(fk_Entidad.SelectedValue))
                        Dim idCampoLista As Integer = NextId(schema & "." & table, "id_Campo_Lista", MyBase.ConnectionString.Core, "fk_Entidad|" & fk_Entidad.SelectedValue)

                        'Inserta en Campo Lista
                        Dim campoListaType = New SchemaConfig.TBL_Campo_ListaType()

                        With campoListaType
                            .fk_Entidad = CShort(fk_Entidad.SelectedValue)
                            .id_Campo_Lista = CShort(idCampoLista)
                            .Nombre_Campo_Lista = Nombre_Campo_Lista.Text
                            .fk_Usuario_Log = MySesion.Usuario.id
                            .Fecha_Log = SlygNullable.SysDate
                        End With

                        'dmcore.SchemaConfig.TBL_Campo_Lista.DBInsert(CShort(fk_Entidad.SelectedValue), CShort(idCampoLista), Nombre_Campo_Lista.Text, MySesion.Usuario.id, SlygNullable.SysDate)
                        dmcore.SchemaConfig.TBL_Campo_Lista.DBInsert(campoListaType)

                        For Each fila As DataRow In dtItemLista.Rows
                            Dim idCampoListaItem As Short = dmcore.SchemaConfig.TBL_Campo_Lista_Item.DBNextId(CShort(fk_Entidad.SelectedValue), CShort(idCampoLista))

                            Dim objListaItem = New SchemaConfig.TBL_Campo_Lista_ItemType
                            objListaItem.fk_Entidad = CShort(fk_Entidad.SelectedValue)
                            objListaItem.fk_Campo_Lista = CShort(idCampoLista)
                            objListaItem.id_Campo_Lista_Item = idCampoListaItem
                            objListaItem.Etiqueta_Campo_Lista_Item = fila.Item("Etiqueta_Campo_Lista_Item").ToString()
                            objListaItem.Valor_Campo_Lista_Item = fila.Item("Valor_Campo_Lista_Item").ToString()

                            dmcore.SchemaConfig.TBL_Campo_Lista_Item.DBInsert(objListaItem)
                        Next
                        dmcore.Transaction_Commit()
                        dmcore.Connection_Close()


                    Case SaveType.Update
                        dmcore.Connection_Open(MySesion.Usuario.id)
                        dmcore.Transaction_Begin()

                        'Actualiza la lista
                        dmcore.SchemaConfig.TBL_Campo_Lista.DBUpdate(CShort(fk_Entidad.SelectedValue), CShort(id_Campo_Lista.Text), Nombre_Campo_Lista.Text, MySesion.Usuario.id, SlygNullable.SysDate, CShort(fk_Entidad.SelectedValue), CShort(id_Campo_Lista.Text))

                        'Elimina los Elementos Items asociados, y añade los nuevos
                        dmcore.Schemadbo.PA_BASIC_TBL_Campo_Lista_Item_Delete.DBExecute(CShort(fk_Entidad.SelectedValue), CShort(id_Campo_Lista.Text))

                        For Each fila As DataRow In dtItemLista.Rows
                            Dim idCampoListaItem As Short = dmcore.SchemaConfig.TBL_Campo_Lista_Item.DBNextId(CShort(fk_Entidad.SelectedValue), CShort(id_Campo_Lista.Text))

                            Dim objListaItem = New SchemaConfig.TBL_Campo_Lista_ItemType
                            objListaItem.fk_Entidad = CShort(fk_Entidad.SelectedValue)
                            objListaItem.fk_Campo_Lista = CShort(id_Campo_Lista.Text)
                            objListaItem.id_Campo_Lista_Item = idCampoListaItem
                            objListaItem.Etiqueta_Campo_Lista_Item = fila.Item("Etiqueta_Campo_Lista_Item").ToString()
                            objListaItem.Valor_Campo_Lista_Item = fila.Item("Valor_Campo_Lista_Item").ToString()

                            dmcore.SchemaConfig.TBL_Campo_Lista_Item.DBInsert(objListaItem)
                        Next

                        dmcore.Transaction_Commit()
                        dmcore.Connection_Close()
                End Select

                CurrentMasterTab = MasterTabType.Grid
                LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, schema, table))
            Catch ex As Exception
                dmcore.Transaction_Rollback()
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub btnEliminar_Click() Handles MyBase.CommandActionDelete
            Try
                MySesion.Pagina.Parameter("IdCampoLista") = CShort(id_Campo_Lista.Text)

                If (grdData.PreSelectedIndex > -1 And Not MySesion.Pagina.Parameter("IdCampoLista") Is Nothing) Then
                    MySesion.Pagina.Parameter("mensaje") = "¿Desea realmente elminar el registro?. Recuerde que los datos serán elminados permanentemente."
                    OpenModalDialog("CampoLista", "../Controles/Confirmacion.aspx", "msgDelete", "Eliminar Registro", 550, 150)
                End If

            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub BtnNuevo_Click() Handles MyBase.CommandActionNew
            Try
                'Crea la estructura del DTListaItem
                dtItemLista = New DataTable()
                dtItemLista.Columns.Add("Etiqueta_Campo_Lista_Item")
                dtItemLista.Columns.Add("Valor_Campo_Lista_Item")
                dtItemLista.Columns.Add("id_Campo_Lista_Item")
                Session("dtItemLista") = dtItemLista

                grdListaItem.DataSource = Nothing
                grdListaItem.DataBind()

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
            Dim dmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            dmCore.Connection_Open(MySesion.Usuario.id)

            Try
                If grdData.SelectedIndex > -1 Then

                    Dim nIdEntidad As String = grdData.Rows(grdData.SelectedIndex).Cells(0).Text.Split(CChar("-"))(0)
                    Dim nIdCampoLista As Integer = CInt(grdData.Rows(grdData.SelectedIndex).Cells(1).Text)
                    fk_Entidad.SelectedValue = nIdEntidad
                    id_Campo_Lista.Text = CStr(nIdCampoLista)
                    Nombre_Campo_Lista.Text = Server.HtmlDecode(grdData.Rows(grdData.SelectedIndex).Cells(2).Text)

                    dtItemLista = dmCore.Schemadbo.PA_BASIC_TBL_Campo_Lista_Item_GET.DBExecute(Integer.Parse(nIdEntidad), nIdCampoLista)
                    grdListaItem.DataSource = dtItemLista
                    grdListaItem.DataBind()
                    Session("dtItemLista") = dtItemLista

                    CurrentMasterTab = MasterTabType.Detail
                    SaveType = SaveType.Update
                End If
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

            Etiqueta_Campo_Lista_Item.Focus()
        End Sub
#End Region

    End Class
End Namespace