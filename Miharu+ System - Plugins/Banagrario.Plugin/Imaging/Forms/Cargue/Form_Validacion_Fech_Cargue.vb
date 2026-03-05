Imports System.Windows.Forms

Namespace Imaging.Forms.Cargue

    Public Class FormValidacionFechCargue

#Region " Declaraciones "
        Public Shared MotivoCreado As Boolean = False
        Public Shared Motivo As String
        Public Shared Descripcion As String
#End Region

#Region " Eventos "

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If ValidarCampos() Then
                MotivoCreado = True
                Motivo = Desktop_txt_Motivo.Text
                Descripcion = Desktop_txt_Descripcion.Text
                Me.Close()
            End If

        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            MotivoCreado = False
            Me.Close()
        End Sub


#End Region

#Region " Funciones "

        Private Function ValidarCampos() As Boolean

            If Desktop_txt_Motivo.Text.Length > 50 Then
                MessageBox.Show("Tenga en cuenta que la logitud de caracteres maxima de caracteres para el motivo son 50", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
            If Desktop_txt_Descripcion.Text.Length > 200 Then
                MessageBox.Show("Tenga en cuenta que la logitud de caracteres maxima de caracteres para el motivo son 200", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
            If Desktop_txt_Motivo.Text = "" Then
                MessageBox.Show("Digite por favor un Motivo", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
            Return True
        End Function



#End Region

    End Class
    
End Namespace