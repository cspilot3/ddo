Imports System.Windows.Forms

Namespace View.Indexacion

    Public Class FormReprocesoMotivo

#Region " Propiedades "

        Public ReadOnly Property Motivo As Short
            Get
                Return CType(CType(MotivoComboBox.SelectedItem, Slyg.Tools.Item).Value, Short)
            End Get
        End Property

#End Region

#Region " Eventos "

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If (Validar()) Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Public Sub LoadData(nMotivos As List(Of Slyg.Tools.Item))
            Me.MotivoComboBox.Items.Clear()
            Me.MotivoComboBox.DisplayMember = "Display"
            Me.MotivoComboBox.ValueMember = "Value"

            Me.MotivoComboBox.Items.Add(New Slyg.Tools.Item(-1, "- Seleccione un motivo -"))
            For Each Item In nMotivos
                Me.MotivoComboBox.Items.Add(New Slyg.Tools.Item(Item.Value, Item.Display))
            Next

            MotivoComboBox.SelectedIndex = 0
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If (MotivoComboBox.SelectedIndex > 0) Then
                Dim Respuesta As DialogResult

                Respuesta = MessageBox.Show("¿Esta seguro que desea enviar a reproceso el documento actual con el motivo: " & MotivoComboBox.Text & "?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                Return Respuesta = Windows.Forms.DialogResult.Yes
            End If

            Return False
        End Function

#End Region

    End Class

End Namespace