Imports Miharu.Security.Library.SecurityServiceReference
Imports Miharu.Security.Library.Session
Imports Miharu.Security.Library.WebService
Imports Miharu.Security._clases

Namespace _sitio

    Partial Public Class login
        Inherits Page

#Region " Declaraciones "

        Private MySesion As Sesion

#End Region

#Region " Propiedades "

        Public Shadows ReadOnly Property Master() As MiharuMasterPage
            Get
                Return CType(MyBase.Master, MiharuMasterPage)
            End Get
        End Property

#End Region

#Region " Eventos "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


            If Not Me.IsPostBack Or Not btnIngresar.Enabled Then
                If Not Session("SesionError") Is Nothing Then
                    If CBool(Session("SesionError")) = True Then
                        Master.ShowAlert("La sesión ha caducado, por favor vuelva a ingresar al sistema", MiharuMasterPage.MsgBoxIcon.IconWarning)
                    End If
                End If

                txtUsuario.Focus()

                ' Inicializar el objeto sesion
                MySesion = New Sesion
                Session("Sesion") = MySesion
            Else
                MySesion = CType(Session("Sesion"), Sesion)
            End If

            If btnIngresar.Enabled Then ValidarIPBloqueada()

            txtUsuario.Focus()
        End Sub

        Protected Sub btnIngresar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnIngresar.Click
            If Validar() Then
                IniciarSesion(txtUsuario.Text, txtContraseña.Text)
            End If
        End Sub

        Private Sub PasswordAceptarButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PasswordAceptarButton.Click
            AsignarPassword(Me.OldPasswordTextBox.Text, Me.NewPasswordTextBox.Text, Me.ConfirmPasswordTextBox.Text)
        End Sub

#End Region

#Region " Metodos "

        Private Sub ValidarIPBloqueada()
            Try
                Me.MySesion.ClientIPAddress = getClientIPAddress()
                Dim WebService As New SecurityWebService(Program.SecurityWebServiceURL, Me.MySesion.ClientIPAddress)

                If (WebService.IsIPBloqueada()) Then
                    Throw New Exception("La dirección IP: " + Me.MySesion.ClientIPAddress + " se encuentra bloqueada por exeder el número de intentos de conexión fallidos, por favor comuniquese con el administrador del sistema")
                End If

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterPage.MsgBoxIcon.IconError)

                Me.txtUsuario.Enabled = False
                Me.txtContraseña.Enabled = False
                Me.btnIngresar.Enabled = False
            End Try
        End Sub

        Private Sub IniciarSesion(ByVal nUserName As String, ByVal nPassword As String)
            If (Validar()) Then
                Dim WebService = New SecurityWebService(Program.SecurityWebServiceURL, Me.MySesion.ClientIPAddress)

                Try
                    WebService.CrearCanalSeguro()
                    WebService.setUser(nUserName, nPassword)

                    Dim idEntidad As Short = -1
                    Dim idUsuario As Integer = -1
                    Dim LogonResult = EnumValidateUser.LOGIN_ERROR

                    If (WebService.ValidateUser(idEntidad, idUsuario, LogonResult)) Then
                        Dim LocalSession = MySesion

                        MySesion.Parameter("ConnectionStrings") = Program.getCadenasConexion(WebService)

                        WebService.FillSession(LocalSession, Program.AssemblyName)

                        Select Case LogonResult
                            Case EnumValidateUser.CAMBIAR_PASSWORD
                                LocalSession.Usuario.Password = nPassword

                                If (MySesion.Usuario.PerfilManager.Permisos.Count > 0) Then
                                    OldPasswordTextBox.Focus()
                                    ModalPopupPassword.Show()
                                Else
                                    Master.ShowAlert("Usuario no cuenta con permisos paraingresar a este módulo", MiharuMasterPage.MsgBoxIcon.IconWarning)
                                End If

                            Case EnumValidateUser.VALIDO
                                LocalSession.Usuario.Password = nPassword

                                If (MySesion.Usuario.PerfilManager.Permisos.Count > 0) Then
                                    MySesion.Pagina = New Pagina(GetType(blankform).FullName, "MIHARU", "~/_sitio/blankform.aspx", "0")
                                    Response.Redirect("~/_sitio/MiharuMainForm.aspx")
                                Else
                                    Master.ShowAlert("Usuario no cuenta con permisos paraingresar a este módulo", MiharuMasterPage.MsgBoxIcon.IconWarning)
                                End If

                            Case Else
                                Master.ShowAlert("Usuario o contraseña invalida", MiharuMasterPage.MsgBoxIcon.IconWarning)

                        End Select
                    Else
                        Select Case LogonResult
                            Case EnumValidateUser.FALTA_LOGIN
                                Master.ShowAlert("Debe ingresar el usuario", MiharuMasterPage.MsgBoxIcon.IconWarning)

                            Case EnumValidateUser.INVALIDO_LOGIN
                                Master.ShowAlert("Usuario o contraseña invalida", MiharuMasterPage.MsgBoxIcon.IconWarning)

                            Case EnumValidateUser.INVALIDO_PASSWORD
                                Master.ShowAlert("Usuario o contraseña invalida", MiharuMasterPage.MsgBoxIcon.IconWarning)

                            Case EnumValidateUser.INACTIVO
                                Master.ShowAlert("El usuario no se encuentra activo", MiharuMasterPage.MsgBoxIcon.IconWarning)

                            Case Else
                                Master.ShowAlert("Usuario o contraseña invalida", MiharuMasterPage.MsgBoxIcon.IconWarning)

                        End Select
                    End If
                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterPage.MsgBoxIcon.IconWarning)

                    Return
                End Try
            End If
        End Sub

        Private Sub AsignarPassword(ByVal nOldPassword As String, ByVal nNewPassword As String, ByVal nConfirmPassword As String)
            ModalPopupPassword.Hide()

            If (nNewPassword = nConfirmPassword) Then                
                Dim WebService = New SecurityWebService(Program.SecurityWebServiceURL, MySesion.ClientIPAddress)

                Try
                    Dim nMsgError As String = String.Empty
                    WebService.CrearCanalSeguro()
                    WebService.setUser(MySesion.Usuario.Login, nOldPassword)
                    Dim Respuesta = WebService.ChangePassword(MySesion.Usuario.Login, nNewPassword, nMsgError)

                    Select Case Respuesta
                        Case EnumValidateUser.INVALIDO_PASSWORD
                            Master.ShowAlert("Contraseña no inválida", MiharuMasterPage.MsgBoxIcon.IconWarning)

                        Case EnumValidateUser.ERROR_PASSWORD
                            Master.ShowAlert("La contraseña no tiene la complejidad requerida por las políticas de seguridad del sistema", MiharuMasterPage.MsgBoxIcon.IconWarning)

                        Case Else
                            MySesion.Pagina = New Pagina(GetType(blankform).FullName, "MIHARU", "~/_sitio/blankform.aspx", "0")
                            Response.Redirect("~/_sitio/MiharuMainForm.aspx")
                    End Select
                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterPage.MsgBoxIcon.IconError)
                End Try
            Else
                Master.ShowAlert("Las contraseñas ingresadas no coinciden", MiharuMasterPage.MsgBoxIcon.IconWarning)
            End If
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Return True
        End Function

        Private Function GetClientIpAddress() As String
            ' Guardar la IP del visitante 
            ' El visitante puede acceder por proxy, entonces tomo la IP que lo está utilizando 
            Dim clientIpAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")

            ' Si no venía de un proxy, tomo la ip del visitante 
            If (clientIpAddress Is Nothing Or clientIpAddress = "") Then
                clientIpAddress = Request.ServerVariables("REMOTE_ADDR")
            End If

            Return clientIpAddress
        End Function

#End Region

    End Class

End Namespace