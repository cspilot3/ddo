Imports DBSecurity
Imports System.Windows.Forms
Imports Miharu.Security.Library.SecurityServiceReference
Imports Miharu.Security.Library.WebService

Public Class FormAutorizacion

#Region " Declaraciones "

    Private Permiso As String

    Private SecurityWebServiceURL As String

    Private ClientIPAddress As String

    Public Shared UsuarioAutorizador As String

#End Region

#Region " Constructores "

    Public Sub New(ByVal nTextoAutorizacion As String, ByVal nAsseblyName As String, ByVal nPermiso As String, ByVal nSecurityWebServiceURL As String, ByVal nClientIPAddress As String)
        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.TextoAutorizacionTextBox.Text = nTextoAutorizacion
        Me.Text = nAsseblyName
        Me.Permiso = nPermiso
        Me.SecurityWebServiceURL = nSecurityWebServiceURL
        Me.ClientIPAddress = nClientIPAddress
    End Sub

#End Region

#Region " Eventos "

    Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
        If (Validar()) Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

#End Region

#Region " Funciones "

    Private Function Validar() As Boolean
        Dim WebService = New SecurityWebService(Me.SecurityWebServiceURL, Me.ClientIPAddress)
        Dim idEntidad As Short = -1
        Dim idUsuario As Integer = -1
        Dim LogonResult = EnumValidateUser.LOGIN_ERROR

        WebService.CrearCanalSeguro()
        WebService.setUser(Me.LoginTextBox.Text, Me.PasswordTextBox.Text)

        If Not (WebService.ValidateUser(idEntidad, idUsuario, LogonResult)) Then

            Select Case LogonResult
                Case EnumValidateUser.FALTA_LOGIN
                    MessageBox.Show("Debe ingresar el usuario", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Case EnumValidateUser.INVALIDO_LOGIN, EnumValidateUser.INVALIDO_PASSWORD
                    MessageBox.Show("Usuario o contraseña invalida", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Case EnumValidateUser.INACTIVO
                    MessageBox.Show("El usuario no se encuentra activo", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Case EnumValidateUser.CAMBIAR_PASSWORD
                    MessageBox.Show("Su clave ha expirado.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)

            End Select

        Else
            Select Case LogonResult
                Case EnumValidateUser.VALIDO, EnumValidateUser.VALIDO

                    Dim LocalSession As New Security.Library.Session.Sesion()

                    WebService.FillSession(LocalSession, Me.Text)

                    If (LocalSession.Usuario.PerfilManager.PuedeEditar(Permiso)) Then
                        UsuarioAutorizador = Me.LoginTextBox.Text
                        Return True
                    Else
                        MessageBox.Show("El usuario no cuenta con permisos suficientes para realizar la acción", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
            End Select

            PasswordTextBox.Focus()
            PasswordTextBox.SelectAll()

        End If


        Return False
    End Function

#End Region

End Class

