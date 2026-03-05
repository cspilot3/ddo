Imports System.Windows.Forms

Namespace Firmas.Forms.Indices

    Public Class FormPreview

#Region " Eventos "


        Private Sub FormPreview_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            Me.tipoComboBox.SelectedIndex = 0
            Me.cbarrasNewTextBox.Focus()
        End Sub

        Private Sub cbarrasNewTextBox_Enter(sender As System.Object, e As EventArgs) Handles cbarrasNewTextBox.Enter
            Me.cbarrasNewTextBox.SelectAll()
        End Sub

        Private Sub okButton_Click(sender As System.Object, e As EventArgs) Handles okButton.Click
            Procesar()
        End Sub

        Private Sub exitButton_Click(sender As System.Object, e As EventArgs) Handles exitButton.Click
            Dim result = MessageBox.Show("¿Está seguro que desea cancelar el proceso?", "Generar DAT", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If (result = DialogResult.Yes) Then
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
            End If
        End Sub

#End Region

#Region " Metodos"

        Private Sub Procesar()
            If (Validar()) Then
                If (Me.tipoComboBox.SelectedIndex = 1) Then ' Ilegible 1
                    Me.cbarrasNewTextBox.Text &= "-i"
                End If

                If (Me.tipoComboBox.SelectedIndex = 5) Then ' Sin Cod. de Barras 5
                    Me.cbarrasNewTextBox.Text &= "-scb"
                End If

                If (Me.tipoComboBox.SelectedIndex = 6) Then ' Diligenciada Manualmente 6
                    Me.cbarrasNewTextBox.Text &= "-dm"
                End If

                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If            
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Dim caracteres = Me.cbarrasNewTextBox.Text.Length

            If (Me.tipoComboBox.SelectedIndex = 0) Then
                MessageBox.Show("Debe seleccionar el tipo de documento", "Generar DAT", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.tipoComboBox.Focus()

            ElseIf (Me.tipoComboBox.SelectedIndex = 1 AndAlso caracteres <> 12) Then ' Ilegible 1
                MessageBox.Show("El código de barras para documento ilegibles debe tener 12 caracteres", "Generar DAT", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.cbarrasNewTextBox.Focus()

            ElseIf (Me.tipoComboBox.SelectedIndex = 2 AndAlso caracteres <> 0) Then ' No identificado 2
                MessageBox.Show("El código de barras para documento no identificado debe ser vacio", "Generar DAT", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.cbarrasNewTextBox.Focus()

            ElseIf (Me.tipoComboBox.SelectedIndex = 3 AndAlso caracteres <> 12) Then ' Tapa 3
                MessageBox.Show("El código de barras para la tapa debe tener 12 caracteres", "Generar DAT", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.cbarrasNewTextBox.Focus()

            ElseIf (Me.tipoComboBox.SelectedIndex = 4 AndAlso caracteres <> 44 AndAlso caracteres <> 38 AndAlso caracteres <> 28) Then ' Tarjeta de firmas 4
                MessageBox.Show("El código de barras para las tarjetas de firmas debe tener 28, 38 o 44 caracteres", "Generar DAT", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.cbarrasNewTextBox.Focus()

            ElseIf (Me.tipoComboBox.SelectedIndex = 5 AndAlso caracteres <> 12) Then ' Sin Cod. de Barras 5
                MessageBox.Show("El código de barras para documento Sin Código de Barras debe tener 12 caracteres", "Generar DAT", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.cbarrasNewTextBox.Focus()

            ElseIf (Me.tipoComboBox.SelectedIndex = 6 AndAlso caracteres <> 12) Then ' Diligenciada Manualmente 6
                MessageBox.Show("El código de barras para documento Diligenciados Manualmente debe tener 12 caracteres", "Generar DAT", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.cbarrasNewTextBox.Focus()
            Else
                Return True
            End If

            Return False
        End Function

#End Region

    End Class

End Namespace