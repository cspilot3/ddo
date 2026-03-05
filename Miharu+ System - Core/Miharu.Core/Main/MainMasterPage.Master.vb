
Public Class MainMasterPage
    Inherits MasterPage

#Region " DECLARACIONES "

    Public Const IconInformation As String = "~/_images/basic/icon-information.png"
    Public Const IconWarning As String = "~/_images/basic/icon-warning.png"
    Public Const IconError As String = "~/_images/basic/icon-error.png"

    Public IsSessionPage As Boolean = True

#End Region

#Region " PROCEDIMIENTOS "

    Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If (Not cssStyles Is Nothing) Then
            cssStyles.Attributes("href") = ResolveUrl("~/_styles/Styles.css")
            cssDefault.Attributes("href") = ResolveUrl("~/_styles/Default.css")
            cssModalPopUp.Attributes("href") = ResolveUrl("~/_styles/ModalPopUp/StyleSheetModalPopUp.css")
            cssTabpanel.Attributes("href") = ResolveUrl("~/_styles/Tabpanel/TabpanelStyles.css")
            cssMarco.Attributes("href") = ResolveUrl("~/_styles/Marco/StyleSheetMaster.css")
        End If

        If (Not IsPostBack) Then

            lrtMasterScripts.Text = "<script src='" & ResolveUrl("~/Controles/Splitter/VwdCmsSplitterBar.js") & "' type='text/javascript'></script>" & vbCrLf
            lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/ConfirmBoxTemplate.js") & "' type='text/javascript'></script>"
            lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/CmiGridView.js") & "' type='text/javascript'></script>"

            lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/windows/javascripts/prototype.js") & "' type='text/javascript'></script>"
            lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/windows/javascripts/window.js") & "' type='text/javascript'></script>"
            lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/windows/javascripts/window_ext.js") & "' type='text/javascript'></script>"
            lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/windows/javascripts/effects.js") & "' type='text/javascript'></script>"
            lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/windows/javascripts/debug.js") & "' type='text/javascript'></script>"

        End If
    End Sub


    Public Sub ShowMessageBox(ByVal nTitulo As String, ByVal nMensaje As String, ByVal nIcon As MsgBoxIcon)
        MessageBoxTemplate1.MsgBoxTitulo1.Text = nTitulo
        MessageBoxTemplate1.MsgBoxMensaje1.Text = nMensaje

        Select Case nIcon
            Case MsgBoxIcon.IconError
                MessageBoxTemplate1.MsgBoxIcono1.ImageUrl = IconError

            Case MsgBoxIcon.IconInformation
                MessageBoxTemplate1.MsgBoxIcono1.ImageUrl = IconInformation

            Case MsgBoxIcon.IconWarning
                MessageBoxTemplate1.MsgBoxIcono1.ImageUrl = IconWarning


        End Select

        MessageBoxTemplate1.MsgBoxPopUp1.Show()
    End Sub

    Public Sub ShowPopUp(ByVal WindowParams As String)
        AddEndRequest("ModalPopup", WindowParams)
    End Sub

    Public Sub ShowDialog(ByVal WindowParams As String)
        AddEndRequest("ShowDialog", WindowParams)
    End Sub

    Public Sub Alert(ByVal text As String)
        AddEndRequest("Alert", text)
    End Sub


    Public Sub SelectText(ByVal ctl As TextBox)
        ctl.Attributes.Add("onfocus", ctl.ClientID + ".select();")
    End Sub

    Public Sub AddEndRequest(ByVal Action As String, ByVal Message As String)
        If (EndRequestAction.Value <> "") Then
            EndRequestAction.Value &= "|"
            EndRequestMessage.Value &= "|"
        End If
        EndRequestAction.Value &= Action
        EndRequestMessage.Value &= Message
    End Sub

    Protected Sub btnRedirectUrl_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRedirectUrl.Click
        Dim url = txtRelativeUrl.Value
        Dim pars = txtParamsUrl.Value
        Response.Redirect(ResolveUrl(url) & "?" & pars)
    End Sub

    Protected Sub btnSessionExit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSessionExit.Click
        SessionExit()
    End Sub

    Public Sub SessionExit()
        Session.Abandon()
        Response.Redirect(ResolveUrl("~/Main/Login.aspx"))
    End Sub

    Public ReadOnly Property SessionExitButton() As LinkButton
        Get
            Return btnSessionExit
        End Get
    End Property

#End Region

End Class