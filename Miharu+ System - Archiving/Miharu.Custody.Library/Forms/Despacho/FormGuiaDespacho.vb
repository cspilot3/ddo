Imports System.Windows.Forms
Imports Miharu.Desktop.Library

Namespace Forms.Despacho

    Public Class FormGuiaDespacho
        Inherits FormBase


        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Guia = ""
            Sello = ""
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Guia = GuiaDesktopTextBox.Text
            Sello = SelloDesktopTextBox.Text
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Public Property Guia As String
        Public Property Sello As String

        Private Sub FormGuiaDespacho_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            GuiaDesktopTextBox.Text = Guia
            SelloDesktopTextBox.Text = Sello
        End Sub
    End Class

End Namespace