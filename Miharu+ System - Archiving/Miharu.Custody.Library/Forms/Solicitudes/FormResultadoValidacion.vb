Imports System.IO
Imports System.Text
Imports System.Windows.Forms

Namespace Forms.Solicitudes

    Public Class FormResultadoValidacion

#Region " Constructor "

        Public Sub New(ByVal trErrores As StringBuilder)
            InitializeComponent()
            ResultadosRichTextBox.Text = trErrores.ToString()
        End Sub

#End Region

#Region " Eventos "
        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub CopiarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CopiarButton.Click
            CopiarLog()
        End Sub

        Private Sub GuardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarButton.Click
            GuardarLog()
        End Sub
#End Region

#Region " Metodos "

        Public Sub GuardarLog()
            Dim Dialogo As New SaveFileDialog()

            Dialogo.Filter = "Texto plano (*.txt)|*.txt|Texto enriquecido (*.rtf)|*.rtf"
            Dialogo.FileName = "Log cargue " & Format(Now, "yyyyMMdd hhmmss") & ".txt"

            Dim Respuesta = Dialogo.ShowDialog()

            If (Respuesta = DialogResult.OK) Then
                Select Case Path.GetExtension(Dialogo.FileName).ToLower()
                    Case ".txt"
                        ResultadosRichTextBox.SaveFile(Dialogo.FileName, RichTextBoxStreamType.PlainText)
                    Case ".rtf"
                        ResultadosRichTextBox.SaveFile(Dialogo.FileName, RichTextBoxStreamType.RichText)
                End Select
            End If
        End Sub

        Public Sub CopiarLog()
            Try
                Dim datobj As New DataObject
                datobj.SetData(DataFormats.Text, ResultadosRichTextBox.Text)
                My.Computer.Clipboard.SetDataObject(datobj)
            Catch
            End Try
        End Sub

#End Region

    End Class

End Namespace