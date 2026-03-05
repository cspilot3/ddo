Imports Miharu.Security._clases

Namespace _sitio

    Partial Public Class blankform
        Inherits FormBase

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not MySesion Is Nothing And Not Session("Sesion") Is Nothing Then
                lblTitulo.Text = MySesion.Pagina.PageTitle
            End If
        End Sub

    End Class

End Namespace