Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls.DesktopMessageBox

Public Class FormRotuloCarpeta


    Private Sub GenerarRotuloCarpeta_Click(sender As System.Object, e As System.EventArgs) Handles BuscarButton.Click
        Dim Cedula = CedulaDesktopTextBox.Text
        If Cedula = "" Or Cedula = Nothing Or Cedula = "0" Then
            MessageBox.Show("Debe ingresar una Cedula", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim objCarpeta As New FormCarpetas(Cedula, False)
            objCarpeta.ShowDialog()
        End If
       

    End Sub
End Class