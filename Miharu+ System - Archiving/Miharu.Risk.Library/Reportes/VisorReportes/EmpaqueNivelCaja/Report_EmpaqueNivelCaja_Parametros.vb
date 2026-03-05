Namespace Reportes.VisorReportes.EmpaqueNivelCaja
    
    Public Class Report_EmpaqueNivelCaja_Parametros

#Region " Propiedades "

        Public Property Caja() As String
            Get
                Try
                    If CajaTextBox.Text = "" Then
                        MsgBox("Numero de caja no valido", MsgBoxStyle.Critical, "Error")
                    Else
                        Return CStr(CajaTextBox.Text)
                    End If
                    Return CStr(False)
                Catch ex As Exception
                    MsgBox("Numero de caja no valido", MsgBoxStyle.Critical, "Error")
                End Try
                Return CStr(False)
            End Get
            Set(ByVal value As String)
                CajaTextBox.Text = value
            End Set
        End Property

#End Region

#Region " Constructores "

        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()
        End Sub

#End Region

#Region " Eventos "

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

#End Region

    End Class

End Namespace