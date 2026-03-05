Imports System.Reflection

Public Class FormAyuda
    Inherits Miharu.Desktop.Library.FormBase

#Region " Eventos "

    Private Sub FormAyuda_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            Dim objAssembly = Assembly.GetExecutingAssembly()
            Dim objstream = objAssembly.GetManifestResourceStream("Miharu.Desktop.ComandosAyuda.rtf")

            ComandosRichTextBox.LoadFile(objstream, RichTextBoxStreamType.RichText)
        Catch ex As Exception
            ComandosRichTextBox.Text = "No se logro cargar archivo de comandos."
        End Try
    End Sub

    Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
        Me.Close()
    End Sub
#End Region

End Class