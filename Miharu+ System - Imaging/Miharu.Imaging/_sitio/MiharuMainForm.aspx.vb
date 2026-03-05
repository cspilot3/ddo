Partial Public Class MiharuMainForm
    Inherits PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If MySesion Is Nothing Then
            Session("SesionError") = True
            Response.Redirect("~/_sitio/login.aspx")
        ElseIf MySesion.Pagina Is Nothing Then
            Session("SesionError") = True
            Response.Redirect("~/_sitio/login.aspx")
        End If
    End Sub
End Class