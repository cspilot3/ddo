Public Partial Class paginaconstruccion
    Inherits Imaging.FormBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblTitulo.Text = MySesion.Pagina.PageTitle
    End Sub

End Class