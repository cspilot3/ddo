Imports Miharu.Core.Clases

Public Class Catalogo
    Inherits FormBase

#Region "Declaraciones"
    Dim Par As New Parametrizador
    Dim tabTable As New Table
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Dim imgB As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgSave"), ImageButton)
            imgB.ValidationGroup = "Guardar"

            tabTable = Par.BeginLink(grdData, decodeBase64(Request.QueryString("Tabla")), tabTable, MyBase.ConnectionString.Core, MySesion.Entidad.id, MySesion.Usuario.id)

            If Not IsNothing(tabTable) Then
                phDinamico.Controls.Add(tabTable)
            End If

            MyMasterPage.MasterDetailPanel.Update()

        Catch ex As Exception
            showErrorPage(ex)
        End Try
    End Sub

    Protected Sub Load_Complete() Handles Me.LoadComplete
        Try
            If Not IsPostBack Then
                CType(tabTable.FindControl("fk_entidad"), DropDownList).AutoPostBack = True
            End If

            DropDownListCascading(CType(tabTable.FindControl("fk_sede"), DropDownList), CType(Session("SEDE_DATA"), DataTable), "id_sede", "nombre_sede", "fk_entidad=" & GetControlValue(tabTable, "fk_entidad").ToString)

        Catch : End Try
    End Sub

    Private Sub Page_ModalClosed(ByVal parameters As String) Handles Me.ModalClosed
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

    Protected Sub btnGuardar_Click() Handles MyBase.CommandActionSave
        Try
            Par.Guardar_(tabTable)
            CurrentMasterTab = MasterTabType.Grid
        Catch ex As Exception
            Dim msg As String = valida_Excepcion(ex)
            If msg <> "" Then
                MyMasterPage.ShowMessage(msg, MsgBoxIcon.IconWarning, "Error al guardar Registro")
            Else
                showErrorPage(ex)
            End If
        End Try
    End Sub

    Protected Sub btnEliminar_Click() Handles MyBase.CommandActionDelete
        Try
            If (grdData.PreSelectedIndex > -1) Then
                MySesion.Pagina.Parameter("mensaje") = "¿Desea realmente elminar el registro?. Recuerde que los datos serán elminados permanentemente."
                OpenModalDialog("CampoLista", "../Controles/Confirmacion.aspx", "msgDelete", "Eliminar Registro", 550, 150)
            End If
            'Par.Eliminar(tabTable)
            'CurrentMasterTab = MasterTabType.Grid
        Catch ex As Exception
            Dim msg As String = valida_Excepcion(ex)
            If msg <> "" Then
                MyMasterPage.ShowMessage(msg, MsgBoxIcon.IconWarning, "Error al eliminar Registro")
            Else
                showErrorPage(ex)
            End If
        End Try
    End Sub

    Protected Sub BtnNuevo_Click() Handles MyBase.CommandActionNew
        Try
            Par.Nuevo(tabTable)
            CurrentMasterTab = MasterTabType.Detail
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
                Par.EnlazaControles(tabTable, grdData.SelectedIndex)
                CurrentMasterTab = MasterTabType.Detail
            End If
        Catch ex As Exception
            showErrorPage(ex)
        End Try
    End Sub

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As EventArgs) Handles Me.LoadComplete
        If Not IsPostBack Then
            MyMasterPage.MasterTabContainer.Tabs(0).Enabled = False
            CurrentMasterTab = MasterTabType.Grid
        End If

        NumRegistros.Text = "Número de registros: " & grdData.Rows.Count
    End Sub


    Private Sub EliminarRegistro()
        Par.Eliminar(tabTable)
        CurrentMasterTab = MasterTabType.Grid
    End Sub

#End Region

End Class
