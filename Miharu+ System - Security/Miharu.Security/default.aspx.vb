Partial Public Class _default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.Redirect("~/_sitio/login.aspx")
    End Sub

End Class