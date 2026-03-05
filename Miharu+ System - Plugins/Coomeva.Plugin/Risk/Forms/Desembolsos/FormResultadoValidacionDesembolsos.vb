Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config

Namespace Forms.Desembolsos

    Public Class FormResultadoValidacionDesembolsos

        Dim ErroresTotales As String

#Region " Constructor "
        'Public Sub New(ByVal trErrores As List(Of String))
        Public Sub New(ByVal trErrores As DesktopConfig.TypeResult)
            InitializeComponent()

            'Carga detalle errores
            Dim msgRespuestaCargue As String = ""
            'Dim countErros As Integer = 0
            'Dim ErroresMayores100 As Boolean = False

            If trErrores.Parameters.Count > 0 Then
                For Each elemento In trErrores.Parameters
                    'If countErros <= 100 Then ' Se muestran solamente los primeros 100 errores.
                    'countErros += 1
                    msgRespuestaCargue = msgRespuestaCargue + elemento.ToString() + vbCrLf

                    'If countErros = 100 Then ErroresMayores100 = True
                    'Else
                    'ErroresTotales &= elemento.ToString() + vbCrLf
                    'End If
                Next

                ResultadosRichTextBox.Text = msgRespuestaCargue
            End If

            'Carga Resumen de errores
            ValidosDesktopTextBox.Text = trErrores.Resumen.Valido.ToString()
            NoValidosDesktopTextBox.Text = trErrores.Resumen.NoValido.ToString()

            EsquemaNoValidoDesktopTextBox.Text = trErrores.Resumen.EsquemaNoValido.ToString()
            TipoDocumentoNoValidoDesktopTextBox.Text = trErrores.Resumen.TipoDocumentoNoValido.ToString()
            ClaseRegistroNoValidoDesktopTextBox.Text = trErrores.Resumen.ClaseRegistroNoValido.ToString()
            DevolucionSinCodigoBarrasDesktopTextBox.Text = trErrores.Resumen.DevolucionSinCodigoBarras.ToString()
            AdicionConLlavesInexistentesDesktopTextBox.Text = trErrores.Resumen.AdicionConLlavesInexistentes.ToString()
            NuevoConLlavesInexistentesDesktopTextBox.Text = trErrores.Resumen.NuevoConLlavesInexistentes.ToString()
            NumeroLlavesNoCoincideDesktopTextBox.Text = trErrores.Resumen.NumeroLlavesNoCoincide.ToString()
            TipoDatoLlavesNoCoincideDesktopTextBox.Text = trErrores.Resumen.TipoDatoLlavesNoCoincide.ToString()
            NumeroCamposDataNoCoincideDesktopTextBox.Text = trErrores.Resumen.NumeroCamposDataNoCoincide.ToString()
            TipoDatoCamposDataNoCoincideDesktopTextBox.Text = trErrores.Resumen.TipoDatoCamposDataNoCoincide.ToString()
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
                datobj.SetData(DataFormats.Text, ErroresTotales)
                My.Computer.Clipboard.SetDataObject(datobj)
            Catch
            End Try
        End Sub

#End Region

    End Class

End Namespace