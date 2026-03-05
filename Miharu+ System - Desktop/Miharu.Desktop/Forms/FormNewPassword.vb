Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Security.Library.SecurityServiceReference
Imports Miharu.Security.Library.WebService

Namespace Forms

    Public Class FormNewPassword

#Region " Propiedades "

        Public Property OldPasswrd As String
            Get
                Return Me.OldPasswordTextBox.Text
            End Get
            Set(ByVal value As String)
                Me.OldPasswordTextBox.Text = value
            End Set
        End Property

        Public ReadOnly Property NewPasswrd As String
            Get
                Return Me.NewPasswordTextBox.Text
            End Get
        End Property

#End Region

#Region " Eventos "

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            CambiarPassword(Me.OldPasswordTextBox.Text, Me.NewPasswordTextBox.Text, Me.ConfirmPasswordTextBox.Text)
        End Sub

#End Region

#Region " Metodos "

        Public Sub CambiarPassword(ByVal nOldPassword As String, ByVal nNewPassword As String, ByVal nConfirmPassword As String)
            If (Validar()) Then
                Dim WebService As New SecurityWebService(Program.SecurityWebServiceURL, Program.getClientIPAddress())

                Try
                    Dim nMsgError As String = String.Empty
                    WebService.CrearCanalSeguro()
                    WebService.setUser(Program.Sesion.Usuario.Login, nOldPassword)
                    Dim Respuesta = WebService.ChangePassword(Program.Sesion.Usuario.Login, nNewPassword, nMsgError)

                    Select Case Respuesta
                        Case EnumValidateUser.INVALIDO_PASSWORD
                            MessageBox.Show("La contraseña anterior no es válida", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                        Case EnumValidateUser.ERROR_PASSWORD
                            Me.DialogResult = DialogResult.None
                            MessageBox.Show(nMsgError, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            '"La nueva contraseña no tiene la complejidad requerida por las políticas de seguridad del sistema"

                        Case EnumValidateUser.VALIDO
                            MessageBox.Show("La contraseña se cambio exitosamente", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.DialogResult = DialogResult.OK
                            Me.Close()

                        Case Else
                            Me.DialogResult = DialogResult.None
                            MessageBox.Show(nMsgError, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End Select
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If (Me.NewPasswordTextBox.Text <> Me.ConfirmPasswordTextBox.Text) Then
                DesktopMessageBoxControl.DesktopMessageShow("Las contraseñas ingresadas no son iguales", Program.AssemblyName, Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                NewPasswordTextBox.SelectAll()
                NewPasswordTextBox.Focus()
            Else
                Return True
            End If

            Return False
        End Function

#End Region

    End Class
End Namespace