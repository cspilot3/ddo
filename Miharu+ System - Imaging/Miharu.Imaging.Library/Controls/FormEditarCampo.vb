
Namespace Controls

    Public Class FormEditarCampo

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Save()
        End Sub

        Private Sub Save()
            If (Validar()) Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End Sub

        Private Function Validar() As Boolean
            If (NuevoComboBox.Visible And NuevoComboBox.SelectedIndex <= 0) Then
                MessageBox.Show("Debe seleccionar un elemento", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Return True
            End If

            Return True
        End Function

    End Class

End Namespace