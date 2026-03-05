Public Partial Class about
    Inherits PaginaBasePopUp

#Region " Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            ShowData()
        End If
    End Sub
    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCerrar.Click
        Master.Cerrar(False)
    End Sub

#End Region

#Region " Metodos "

    Private Sub ShowData()
        Me.lblProductName.Text = Program.AssemblyProduct
        Me.lblVersion.Text = String.Format("Versión {0}", Program.AssemblyVersion)
        Me.lblCopyright.Text = Program.AssemblyCopyright
        Me.lblCompanyName.Text = Program.AssemblyCompany
        Me.txtBoxDescription.Text = Program.AssemblyDescription
    End Sub

#End Region

End Class