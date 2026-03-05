Imports Miharu.Core.Clases
Imports DBSecurity
Imports Miharu.Security.Library
Imports Miharu.Core.Sitio
Imports Miharu.Security.Library.WebService
Imports Miharu.Security.Library.SecurityServiceReference
Imports Miharu.Security.Library.Session

Namespace Main

    Partial Public Class Login
        Inherits Page

#Region " DECLARACIONES "

        Private MySesion As Session.Sesion

#End Region

#Region " PROPIEDADES "

        Public Shadows ReadOnly Property Master() As MainMasterPage
            Get
                Return CType(MyBase.Master, MainMasterPage)
            End Get
        End Property

#End Region

#Region " EVENTOS "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Master.SessionExitButton.Visible = False

            If Not Me.IsPostBack Or Not btnIngresar.Enabled Then
                If Not Session("SesionError") Is Nothing Then
                    If CBool(Session("SesionError")) = True Then
                        Master.ShowMessageBox("Sesión Finalizada", "La sesión ha caducado, por favor vuelva a ingresar al sistema", MsgBoxIcon.IconWarning)
                    End If
                End If

                txtUsuario.Focus()

                ' Inicializar el objeto sesion
                MySesion = New Session.Sesion
                Session("Sesion") = MySesion
            Else
                MySesion = CType(Session("Sesion"), Session.Sesion)
            End If

            If btnIngresar.Enabled Then ValidarIPBloqueada()
            txtUsuario.Focus()
        End Sub

        Protected Sub btnIngresar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnIngresar.Click
            If Validar() Then
                IniciarSesion(txtUsuario.Text, txtContraseña.Text)
            End If
        End Sub

        Protected Sub btnPasswordAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPasswordAceptar.Click
            AsignarPassword()
        End Sub

#End Region

#Region " METODOS "

        Private Sub ValidarIPBloqueada()
            Try
                MySesion.ClientIPAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")

                If MySesion.ClientIPAddress = "" Then
                    MySesion.ClientIPAddress = Request.ServerVariables("REMOTE_ADDR")
                End If

                Dim WebService As New WebService.SecurityWebService(Program.getSecurityWebServiceURL, MySesion.ClientIPAddress)

                If WebService.IsIPBloqueada() Then
                    Session("Error_Titulo") = "IP Bloqueada"
                    Session("Error_Mensaje") = "La dirección IP: " & MySesion.ClientIPAddress & " se encuentra bloqueada por exeder el número de intentos de conexión fallidos, por favor comuniquese con el administrador del sistema"

                    Response.Redirect("~/" & "Main/Error.aspx")
                End If

            Catch ex As Exception
                Master.ShowMessageBox("Error", ex.Message, MsgBoxIcon.IconError)
                txtUsuario.Enabled = False
                txtContraseña.Enabled = False
                btnIngresar.Enabled = False

            End Try
        End Sub

        Private Sub IniciarSesion(ByVal nUserName As String, ByVal nPassword As String)
            If (Validar()) Then
                Dim WebService = New SecurityWebService(Program.getSecurityWebServiceURL, Me.MySesion.ClientIPAddress)

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
                                    txtPassword1.Focus()
                                    ModalPopupPassword.Show()
                                Else
                                    Master.ShowMessageBox("Login", "Usuario no cuenta con permisos paraingresar a este módulo", MsgBoxIcon.IconWarning)
                                End If

                            Case EnumValidateUser.VALIDO
                                LocalSession.Usuario.Password = nPassword

                                If (MySesion.Usuario.PerfilManager.Permisos.Count > 0) Then
                                    MySesion.Pagina = New Pagina(GetType(Inicio).FullName, "MIHARU Core", "~/" & "Sitio/inicio.aspx", "0")
                                    Response.Redirect("~/" & "Main/MainPage.aspx")
                                Else
                                    Master.ShowMessageBox("Login", "Usuario no cuenta con permisos paraingresar a este módulo", MsgBoxIcon.IconWarning)
                                End If

                            Case Else
                                Master.ShowMessageBox("Login", "Usuario o contraseña invalida", MsgBoxIcon.IconWarning)

                        End Select
                    Else
                        Select Case LogonResult
                            Case EnumValidateUser.FALTA_LOGIN
                                Master.ShowMessageBox("Login", "Debe ingresar el usuario", MsgBoxIcon.IconWarning)

                            Case EnumValidateUser.INVALIDO_LOGIN
                                Master.ShowMessageBox("Login", "Usuario o contraseña invalida", MsgBoxIcon.IconWarning)

                            Case EnumValidateUser.INVALIDO_PASSWORD
                                Master.ShowMessageBox("Login", "Usuario o contraseña invalida", MsgBoxIcon.IconWarning)

                            Case EnumValidateUser.INACTIVO
                                Master.ShowMessageBox("Login", "El usuario no se encuentra activo", MsgBoxIcon.IconWarning)

                            Case Else
                                Master.ShowMessageBox("Login", "Usuario o contraseña invalida", MsgBoxIcon.IconWarning)

                        End Select
                    End If
                Catch ex As Exception
                    Master.ShowMessageBox("Error", ex.Message, MsgBoxIcon.IconWarning)

                    Return
                End Try
            End If
        End Sub

        Private Sub AsignarPassword()
            Dim DBMSecurity As New DBSecurityDataBaseManager(CType(MySesion.Parameter("ConnectionStrings"), Program.TypeConnectionString).Security)
            Const MsgError As String = ""

            Dim WebService As New WebService.SecurityWebService(Program.getSecurityWebServiceURL, MySesion.ClientIPAddress)
            Dim ConnectionStrings As Program.TypeConnectionString

            Try
                WebService.CrearCanalSeguro()
                WebService.setUser(txtUsuario.Text, txtContraseña.Text)
                ConnectionStrings = Program.getCadenasConexion(WebService)
                MySesion.Parameter("ConnectionStrings") = ConnectionStrings
            Catch ex As Exception
                Master.ShowMessageBox("Usuario o contraseña invalida", "Usuario o contraseña invalida", MsgBoxIcon.IconWarning)

                Return
            End Try

            ModalPopupPassword.Hide()

            If txtPassword1.Text <> txtPassword2.Text Then
                Master.ShowMessageBox("Contraseña Invalida", "Las contraseñas ingresadas no coinciden", MsgBoxIcon.IconWarning)
            Else

                Try
                    DBMSecurity.Connection_Open(MySesion.Usuario.id)

                    If CBool(WebService.ChangePassword(MySesion.Usuario.Login, txtPassword1.Text, MsgError)) Then
                        MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Inicio).FullName, "Inicio", "~/Sitio/Inicio.aspx", "0.0")
                        Response.Redirect("~/Sitio/Inicio.aspx")
                    End If

                Catch ex As Exception
                Finally
                    DBMSecurity.Connection_Close()
                End Try
            End If
        End Sub

#End Region

#Region " FUNCIONES "

        Private Function Validar() As Boolean
            If txtUsuario.Text = "" Then
                txtUsuario.Focus()
            Else
                Return True
            End If

            Return False
        End Function

#End Region

    End Class
End Namespace