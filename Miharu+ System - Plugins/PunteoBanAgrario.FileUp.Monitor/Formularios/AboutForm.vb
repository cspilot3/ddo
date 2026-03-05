Public Class AboutForm
    Inherits System.Windows.Forms.Form

    Private Sub classFormAbout_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        ShowData()
    End Sub

    Private Sub ShowData()
        Me.Text = String.Format("Acerca de {0}", Program.AssemblyTitle)

        Me.lblProductName.Text = Program.AssemblyProduct
        Me.lblVersion.Text = String.Format("Versión {0}", Program.AssemblyVersion)
        Me.lblCopyright.Text = Program.AssemblyCopyright
        Me.lblCompanyName.Text = Program.AssemblyCompany
        Me.txtBoxDescription.Text = Program.AssemblyDescription
    End Sub

End Class