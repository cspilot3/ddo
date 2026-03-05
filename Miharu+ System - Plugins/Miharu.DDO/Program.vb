Imports System.Configuration
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config
Imports Miharu.FileSending.Library.Clases
Imports Miharu.Security.Library.Session
Imports Miharu.Security.Library.WebService
Imports Miharu.Uploader.Config
Imports Miharu.Uploader.Forms

Public Class Program
    'ESTO ES DDO

    Public Shared _desktopGlobal As DesktopGlobal
    Public Shared _imagingGlobal As ImagingGlobal


    Friend Shared Property DesktopGlobal() As DesktopGlobal
        Get
            Return _desktopGlobal
        End Get
        Set(ByVal value As DesktopGlobal)
            _desktopGlobal = value
        End Set
    End Property

    Public Shared Property ImagingGlobal As ImagingGlobal
        Get
            Return _imagingGlobal
        End Get
        Set(ByVal value As ImagingGlobal)
            _imagingGlobal = value
        End Set
    End Property

    Friend Shared Config As FileSendingConfig
    Friend Shared Permisos As String()
    Friend Shared UserID As Integer
    Public Shared MiharuSession As Sesion
    Private Shared _CadenasConexion As FileSendingConfig.TypeConnectionString

    Friend Shared ReadOnly Property AssemblyTitle As String
        Get
            Dim attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyTitleAttribute), False)
            If attributes.Length <= 0 Then Return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase)
            Dim titleAttribute = CType(attributes(0), AssemblyTitleAttribute)
            Return If(titleAttribute.Title <> "", titleAttribute.Title, Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase))
        End Get
    End Property

    Friend Shared ReadOnly Property AssemblyVersion As String
        Get
            Return Assembly.GetExecutingAssembly().GetName().Version.ToString()
        End Get
    End Property

    Friend Shared ReadOnly Property AssemblyDescription As String
        Get
            Dim attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyDescriptionAttribute), False)
            Return If(attributes.Length = 0, "", (CType(attributes(0), AssemblyDescriptionAttribute)).Description)
        End Get
    End Property

    Friend Shared ReadOnly Property AssemblyName As String
        Get
            Return Assembly.GetExecutingAssembly().GetName().Name
        End Get
    End Property

    Friend Shared ReadOnly Property AssemblyProduct As String
        Get
            Dim attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyProductAttribute), False)
            Return If(attributes.Length = 0, "", (CType(attributes(0), AssemblyProductAttribute)).Product)
        End Get
    End Property

    Friend Shared ReadOnly Property AssemblyCopyright As String
        Get
            Dim attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCopyrightAttribute), False)
            Return If(attributes.Length = 0, "", (CType(attributes(0), AssemblyCopyrightAttribute)).Copyright)
        End Get
    End Property

    Friend Shared ReadOnly Property AssemblyCompany As String
        Get
            Dim attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCompanyAttribute), False)
            Return If(attributes.Length = 0, "", (CType(attributes(0), AssemblyCompanyAttribute)).Company)
        End Get
    End Property

    Friend Shared ReadOnly Property AppPath As String
        Get
            Return Application.StartupPath & "\"
        End Get
    End Property

    Friend Shared ReadOnly Property AppDataPath As String
        Get
            Return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).TrimEnd("\"c) & "\PYC\" & AssemblyTitle & "\"
        End Get
    End Property

    Friend Shared ReadOnly Property ClientID As Integer
        Get
            Dim rootWebConfig = ConfigurationManager.AppSettings("ClientID")
            If Not String.IsNullOrEmpty(rootWebConfig) Then Return Integer.Parse(rootWebConfig)
            Throw New Exception("Por favor asigne la cadena <add key=""ClientID"" value=""?""/> al archivo App.config.")
        End Get
    End Property

    Friend Shared ReadOnly Property SystemID As Integer
        Get
            Dim rootWebConfig = ConfigurationManager.AppSettings("SystemID")
            If Not String.IsNullOrEmpty(rootWebConfig) Then Return Integer.Parse(rootWebConfig)
            Throw New Exception("Por favor asigne la cadena <add key=""SystemID"" value=""?""/> al archivo App.config.")
        End Get
    End Property

    Friend Shared ReadOnly Property FileServerIP As String
        Get
            Dim rootWebConfig = ConfigurationManager.AppSettings("FileServerIP")
            If Not String.IsNullOrEmpty(rootWebConfig) Then Return rootWebConfig
            Throw New Exception("Por favor asigne la cadena <add key=""FileServerIP"" value=""?""/> al archivo App.config.")
        End Get
    End Property

    Friend Shared ReadOnly Property SecurityWebServiceURL As String
        Get
            Dim rootWebConfig = ConfigurationManager.AppSettings("SecurityWebServiceURL")
            If Not String.IsNullOrEmpty(rootWebConfig) Then Return rootWebConfig
            Throw New Exception("Por favor asigne la cadena <add key=""SecurityWebServiceURL"" value=""?""/> al archivo App.config.")
        End Get
    End Property

    Friend Shared ReadOnly Property FileServerPort As Integer
        Get
            Dim rootWebConfig As String = ConfigurationManager.AppSettings("FileServerPort")
            If Not String.IsNullOrEmpty(rootWebConfig) Then Return Integer.Parse(rootWebConfig)
            Throw New Exception("Por favor asigne la cadena <add key=""FileServerPort"" value=""?""/> al archivo App.config.")
        End Get
    End Property

    Friend Shared ReadOnly Property FileServerAppName As String
        Get
            Dim rootWebConfig As String = ConfigurationManager.AppSettings("FileServerAppName")
            If Not String.IsNullOrEmpty(rootWebConfig) Then Return rootWebConfig
            Throw New Exception("Por favor asigne la cadena <add key=""FileServerAppName"" value=""?""/> al archivo App.config.")
        End Get
    End Property

    Friend Shared ReadOnly Property PackageSize As Integer
        Get
            Dim rootWebConfig As String = ConfigurationManager.AppSettings("PackageSize")
            If Not String.IsNullOrEmpty(rootWebConfig) Then Return Integer.Parse(rootWebConfig)
            Throw New Exception("Por favor asigne la cadena <add key=""PackageSize"" value=""?""/> al archivo App.config.")
        End Get
    End Property

    '  Public Shared UploaderConfigFile As UploaderConfig

    Friend Shared ReadOnly Property SourceDirectory As String
        Get
            Return "C:\MiharuUploader\TempSource\"
        End Get
    End Property

    Friend Shared ReadOnly Property TempDirectory As String
        Get
            Return "C:\MiharuUploader\TempTemp\"
        End Get
    End Property

    Public Shared Property Proyecto As Short
    Public Shared Property Esquema As Short
    Public Shared Property EntidadCliente As Short
    Public Shared Property Reporte As Short
    Public Shared Property IsImage As Boolean
    Public Shared Property IsData As Boolean
    Public Shared Property PathBytes As Byte()

    Friend Shared ReadOnly Property EntidadProcesamiento As Short
        Get
            Dim rootWebConfig = ConfigurationManager.AppSettings("EntidadProcesamiento")
            If Not String.IsNullOrEmpty(rootWebConfig) Then Return Short.Parse(rootWebConfig)
            Throw New Exception("Por favor asigne la cadena <add key=""EntidadProcesamiento"" value=""?""/> al archivo App.config.")
        End Get
    End Property

    Friend Shared ReadOnly Property SedeProcesamiento As Short
        Get
            Dim rootWebConfig = ConfigurationManager.AppSettings("SedeProcesamiento")
            If Not String.IsNullOrEmpty(rootWebConfig) Then Return Short.Parse(rootWebConfig)
            Throw New Exception("Por favor asigne la cadena <add key=""SedeProcesamiento"" value=""?""/> al archivo App.config.")
        End Get
    End Property

    Friend Shared ReadOnly Property CentroProcesamiento As Short
        Get
            Dim rootWebConfig = ConfigurationManager.AppSettings("CentroProcesamiento")
            If Not String.IsNullOrEmpty(rootWebConfig) Then Return Short.Parse(rootWebConfig)
            Throw New Exception("Por favor asigne la cadena <add key=""CentroProcesamiento"" value=""?""/> al archivo App.config.")
        End Get
    End Property

    Friend Shared ReadOnly Property UploaderWebServiceURL As String
        Get
            Dim rootWebConfig = ConfigurationManager.AppSettings("UploaderWebServiceURL")
            If Not String.IsNullOrEmpty(rootWebConfig) Then Return rootWebConfig
            Throw New Exception("Por favor asigne la cadena <add key=""UploaderWebServiceURL"" value=""?""/> al archivo App.config.")
        End Get
    End Property

    Friend Shared Property ConnectionStringList As FileSendingConfig.TypeConnectionString
        Get
            Return _CadenasConexion
        End Get
        Set(ByVal value As FileSendingConfig.TypeConnectionString)
            _CadenasConexion = value
        End Set
    End Property

    Friend Shared ReadOnly Property IdentifierDateFormat As String
        Get
            Dim rootWebConfig = ConfigurationManager.AppSettings("IdentifierDateFormat")
            If Not String.IsNullOrEmpty(rootWebConfig) Then Return rootWebConfig
            Throw New Exception("Por favor asigne la cadena <add key=""IdentifierDateFormat"" value=""?""/> al archivo App.config.")
        End Get
    End Property

    <STAThread>
    Private Sub Main()
        Try
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(True)
            Dim IP = GetClientIpAddress()
            Dim WebService = New SecurityDMZWebService(SecurityWebServiceURL, IP)

            If WebService.IsIPBloqueada() Then
                MessageBox.Show("La dirección IP local [" & WebService.ClientIPAddress & "] se encuentra bloqueada, por favor comuníquese con el administrador del sistema", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            ' LoadConfig()
            Dim LoginForm = New FormLogin()
            Dim continuar = True
            Dim contador = 0

            While continuar
                LoginForm.SelectText()
                Dim respuesta = LoginForm.ShowDialog()
                contador += 1

                If respuesta = DialogResult.OK Then
                    Dim InicioSesion = IniciarSesion(WebService, LoginForm.Login, LoginForm.Password)

                    If InicioSesion = "OK" Then

                        If MiharuSession.Usuario.isRoot OrElse MiharuSession.Usuario.PerfilManager.Permisos.Count > 0 Then
                            LoginForm.Dispose()
                            continuar = False
                        Else
                            MessageBox.Show("El usuario no cuenta con permisos para ingresar a este módulo", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    Else
                        MessageBox.Show(InicioSesion, "Error de Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If contador > 3 OrElse respuesta <> DialogResult.OK Then Return
                End If
            End While

            Dim MainForm = New FormMain()
            MainForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error de Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub



    Function IniciarSesion(ByVal nWebService As SecurityDMZWebService, ByVal nUserName As String, ByVal nPassword As String) As String
        Try
            nWebService.CrearCanalSeguro()
            nWebService.setUser(nUserName, nPassword)
            Dim idEntidad As Short
            Dim idUsuario As Integer
            Dim LogonResult As Security.Library.SecurityDMZServiceReference.EnumValidateUser

            If nWebService.ValidateUser(idEntidad, idUsuario, LogonResult) Then
                MiharuSession = New Sesion()
                Dim LocalSession = MiharuSession
                ConnectionStringList = getCadenasConexion(nWebService)
                nWebService.FillSession(LocalSession, AssemblyName)

                Select Case LogonResult
                    Case Security.Library.SecurityDMZServiceReference.EnumValidateUser.CAMBIAR_PASSWORD

                        If Not (MiharuSession.Usuario.PerfilManager.Permisos.Count > 0) Then
                            Return "El usuario no cuenta con permisos para ingresar a este módulo"
                        End If

                        LocalSession.Usuario.Login = nUserName
                        LocalSession.Usuario.Password = nPassword
                        MessageBox.Show("La contraseña ha vencido, por favor actualice la contraseña", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        If ChangePassword(nPassword) Then Return "OK"
                    Case Security.Library.SecurityDMZServiceReference.EnumValidateUser.VALIDO

                        If Not (MiharuSession.Usuario.PerfilManager.Permisos.Count > 0) Then
                            Return "El usuario no cuenta con permisos para ingresar a este módulo"
                        End If

                    Case Else
                        Return "Usuario o contraseña invalida"
                End Select
            Else

                Select Case LogonResult
                    Case Security.Library.SecurityDMZServiceReference.EnumValidateUser.FALTA_LOGIN
                        Return "Debe ingresar el usuario"
                    Case Security.Library.SecurityDMZServiceReference.EnumValidateUser.INVALIDO_LOGIN
                        Return "Usuario o contraseña inválida"
                    Case Security.Library.SecurityDMZServiceReference.EnumValidateUser.INVALIDO_PASSWORD
                        Return "Usuario o contraseña inválida"
                    Case Security.Library.SecurityDMZServiceReference.EnumValidateUser.INACTIVO
                        Return "El usuario no se encuentra activo"
                    Case Else
                        Return "Usuario o contraseña inválida"
                End Select
            End If

        Catch ex As Exception
            Return "Error no identificado: " & ex.Message
        End Try

        Return "OK"
    End Function

    Function getCadenasConexion(ByRef nWebService As SecurityDMZWebService) As FileSendingConfig.TypeConnectionString
        Dim Cadenas = New FileSendingConfig.TypeConnectionString()
        Dim ConnectionStrings = nWebService.getCadenasConexion()

        For Each typeModulo In ConnectionStrings

            Select Case typeModulo.Id
                Case 0
                    Cadenas.Security = typeModulo.ConnectionString
                Case 2
                    Cadenas.Imaging = typeModulo.ConnectionString
                Case 3
                    Cadenas.Archiving = typeModulo.ConnectionString
                Case 6
                    Cadenas.Core = typeModulo.ConnectionString
                Case 26
                    Cadenas.Integration = typeModulo.ConnectionString
            End Select
        Next

        Return Cadenas
    End Function

    Function GetClientIpAddress() As String
        Dim LocalIP = ""

        For posicion = 0 To Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length - 1
            Dim tempIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList(posicion).ToString()
            If Not IsValidIP(tempIP) Then Continue For
            LocalIP = tempIP
            Exit For
        Next

        Return LocalIP
    End Function

    Private Function IsValidIP(ByVal nIP As String) As Boolean
        Const pattern As String = "^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$"
        Dim check = New Regex(pattern)
        Return nIP <> "" AndAlso check.IsMatch(nIP, 0)
    End Function

    Private Function ChangePassword(ByVal nPassword As String) As Boolean
        Dim newPasswordForm = New FormNewPassword With {
            .OldPassword = nPassword
        }
        If newPasswordForm.ShowDialog() <> DialogResult.OK Then Return False
        MiharuSession.Usuario.Password = newPasswordForm.NewPassword
        Return True
    End Function

    'Friend Sub LoadConfig()
    '    If File.Exists(AppDataPath & UploaderConfig.ConfigFileName) Then
    '        UploaderConfigFile = UploaderConfig.Deserialize(AppDataPath)
    '        UploaderConfigFile.WorkingFolder = UploaderConfigFile.WorkingFolder.TrimEnd("\"c) & "\"
    '        'Program.Config = UploaderConfigFile
    '        Program.Config = New FileSendingConfig()
    '    Else
    '        UploaderConfigFile = New UploaderConfig()
    '        SaveConfig()
    '        Program.Config = New FileSendingConfig()
    '        'Program.Config = New UploaderConfig()
    '    End If
    'End Sub


    'Friend Sub SaveConfig()
    '    If Not Directory.Exists(AppDataPath) Then Directory.CreateDirectory(AppDataPath)
    '    FileSendingConfig.Serialize(Program.Config, AppDataPath)
    'End Sub
End Class
