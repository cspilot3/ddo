Public Class PopupMasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            lrtMasterScripts.Text = "<script src='" & ResolveUrl("~/_js/CmiGridView.js") & "' type='text/javascript'></script>"
            lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/MasterPopUp.js") & "' type='text/javascript'></script>"
        End If
    End Sub

    Public Sub CloseWindow(ByVal result As Boolean)
        AddEndRequest("Close", CStr(result))
    End Sub

    Public Sub ShowMessage(ByVal TextMessage As String)
        AddEndRequest("Message", TextMessage)
    End Sub

    Public Sub ParentRedirect(ByVal relativeUrl As String, ByVal paramsUrl As String)
        AddEndRequest("ParentRedirect", relativeUrl & "$" & paramsUrl)
    End Sub

    Public Sub AddEndRequest(ByVal Action As String, ByVal Message As String)
        If (EndRequestAction.Value <> "") Then
            EndRequestAction.Value &= "|"
            EndRequestMessage.Value &= "|"
        End If
        EndRequestAction.Value &= Action
        EndRequestMessage.Value &= Message
    End Sub
    Public Sub SetActive()
        updRequest.Update()
    End Sub
End Class