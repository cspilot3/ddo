Imports Miharu.Security._clases

Namespace _sitio

    Partial Public Class paginaconstruccion
        Inherits FormBase

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            lblTitulo.Text = MySesion.Pagina.PageTitle
        End Sub

    End Class

End Namespace