Namespace Forms.Parametrizacion

    Public Class FormNuevaTipologia

#Region " Declaraciones "

        Private _nombreTipologia As String
        Private _eliminado As Boolean

#End Region

#Region "Propiedades"

        Public Property NombreTipologia() As String
            Get
                Return _nombreTipologia
            End Get
            Set(ByVal value As String)
                _nombreTipologia = value
            End Set
        End Property

        Public Property Eliminado() As Boolean
            Get
                Return _eliminado
            End Get
            Set(ByVal value As Boolean)
                _eliminado = value
            End Set
        End Property

#End Region

#Region "Eventos"

        Private Sub AceptarButton_Click(sender As System.Object, e As EventArgs) Handles AceptarButton.Click
            If NombreTipologiaTextBox.Text <> "" Then
                _nombreTipologia = NombreTipologiaTextBox.Text
                _eliminado = EliminadoCheckBox.Checked
                DialogResult = DialogResult.OK
            Else
                MessageBox.Show("El campo Nombre no puede estar vacio")
            End If
        End Sub

        Private Sub CancelarButton_Click(sender As System.Object, e As EventArgs) Handles CancelarButton.Click
            DialogResult = DialogResult.Cancel
        End Sub

#End Region

    End Class

End Namespace