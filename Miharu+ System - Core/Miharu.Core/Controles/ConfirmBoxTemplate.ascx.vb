Public Class ConfirmBoxTemplate
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Property ConfirmText() As String
        Get
            Return txtConfirmText.Text
        End Get
        Set(ByVal value As String)
            txtConfirmText.Text = value
        End Set
    End Property
End Class