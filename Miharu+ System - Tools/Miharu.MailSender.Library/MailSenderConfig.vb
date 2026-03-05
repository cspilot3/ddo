Imports System.IO
Imports System.Xml.Serialization
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Security.Cryptography
Imports Slyg.Tools.Cryptographic
Imports Miharu.Security.Library.WebService


#Region " Clases "

Public Module ParameterSystem
    Public Const URLAPIMasic As String = "URL_API_Masic"
    Public Const UserAPIMasiv As String = "User_API_Masiv"
    Public Const PasswordAPIMasiv As String = "Password_API_Masiv"
    Public Const EmailSendAditional As String = "Email_Send_Aditional"
End Module


Public Class MailSenderConfig


#Region " Estructuras "

    Public Structure TypeConnectionString
        Public Tools As String
        Public Core As String
    End Structure

    <Serializable()>
    Public Structure TypeParameterSystem
        Public _URLAPIMasic As String
        Public _UserAPIMasiv As String
        Public _PasswordAPIMasiv As String
        Public _EmailSendAditional As String
    End Structure

#End Region

#Region " Declaraciones "

    ' Mail
    'Private _FromMailAddress As String = "domesa.workflow@domesa.com.co"
    'Private _FromMailDisplay As String = "MIHARU MailSender"
    'Private _SMTPServerAddress As String = "10.64.19.60"
    'Private _SMTPServerPort As Integer = 25
    'Private _MailUser As String = "dworkflow"
    'Private _MailUserPassword As String = "lotus2009"
    'Private _EnabledSSL As Boolean = False

    Private _FromMailAddress As String = "noreply@procesosycanje.com.co"
    Private _FromMailDisplay As String = "MIHARU MailSender"
    Private _SMTPServerAddress As String = "smtp.brinksinc.com"
    Private _SMTPServerPort As Integer = 25
    Private _MailUser As String = "noreply"
    Private _MailUserPassword As String = ""
    Private _EnabledSSL As Boolean = False

    ' Servicio
    Private _Intervalo As Integer = 5000000

    'Private _SecurityWebServiceURL As String = "http://localhost:51500/SecurityService.asmx"   ' DESARROLLO
    Private _SecurityWebServiceURL As String = "http://uatapconvenios.brinks.col/SecurityServiceDDO/SecurityService.asmx"      'CERTIFICACION
    'Private _SecurityWebServiceURL As String = "http://proapconvenios/Miharu/Miharu.Security.WebService/SecurityService.asmx"  'PRODUCCION
    'Private _SecurityWebServiceURL As String = "http://apbanagrariocert/miharu.security.webservice/SecurityService.asmx"

    Private _User As String = "Service"
    Private _Password As Byte() = Nothing

    Private _UsaRemoting As Boolean = False
    Private _RemotingIPName As String = "UATAPCONVENIOS"
    Private _RemotingPuerto As Integer = 8085
    Private _RemotingAppName As String = "DataRemotingService41"
    Private _RemotingPassword As Byte() = Nothing
    Private _RemotingCifrado As Boolean = False

#End Region

#Region " Propiedades "

    ' Mail
    <Category("Mail"), Browsable(True), _
    Description("Define la dirección de correo desde donde se envian los correos")> _
    Public Property FromMailAddress() As String
        Get
            Return _FromMailAddress
        End Get
        Set(ByVal value As String)
            _FromMailAddress = value
        End Set
    End Property
    <Category("Mail"), Browsable(True), _
    Description("Define el nombre a mostrar como remitente de los correo enviados")> _
    Public Property FromMailDisplay() As String
        Get
            Return _FromMailDisplay
        End Get
        Set(ByVal value As String)
            _FromMailDisplay = value
        End Set
    End Property
    <Category("Mail"), Browsable(True), _
    Description("Define la dirección del servidor de correo")> _
    Public Property SMTPServerAddress() As String
        Get
            Return _SMTPServerAddress
        End Get
        Set(ByVal value As String)
            _SMTPServerAddress = value
        End Set
    End Property
    <Category("Mail"), Browsable(True), _
    Description("Define el puero de conexión al servidor de correos")> _
    Public Property SMTPServerPort() As Integer
        Get
            Return _SMTPServerPort
        End Get
        Set(ByVal value As Integer)
            _SMTPServerPort = value
        End Set
    End Property
    <Category("Mail"), Browsable(True), _
    Description("Define el usuario del servidor de correo")> _
    Public Property MailUser() As String
        Get
            Return _MailUser
        End Get
        Set(ByVal value As String)
            _MailUser = value
        End Set
    End Property
    <Category("Mail"), Browsable(True), _
    Description("Define la contaseña del usuario del servidor de correo")> _
    Public Property MailUserPassword() As String
        Get
            Return _MailUserPassword
        End Get
        Set(ByVal value As String)
            _MailUserPassword = value
        End Set
    End Property
    <Category("Mail"), Browsable(True), _
    Description("Define si se utiliza SSL para el envío de los correos")> _
    Public Property EnabledSSL() As Boolean
        Get
            Return _EnabledSSL
        End Get
        Set(ByVal value As Boolean)
            _EnabledSSL = value
        End Set
    End Property

    ' Servicio
    <Category("Servicio"), Browsable(True), _
    Description("Define el intervalo de tiempo en milisegundos a esperar antes de validar si hay nuevos correos para enviar")> _
    Public Property Intervalo() As Integer
        Get
            Return _Intervalo
        End Get
        Set(ByVal value As Integer)
            _Intervalo = value
        End Set
    End Property

    <Category("Servicio"), Browsable(True), _
    Description("Servicio web de validación de usuarios")> _
    Public Property SecurityWebServiceURL() As String
        Get
            Return _SecurityWebServiceURL
        End Get
        Set(ByVal Value As String)
            _SecurityWebServiceURL = Value
        End Set
    End Property
    <Category("Servicio"), Browsable(True), _
    Description("Usuario de inicio de sesión")> _
    Public Property User() As String
        Get
            Return _User
        End Get
        Set(ByVal Value As String)
            _User = Value
        End Set
    End Property
    <Category("Servicio"), Browsable(True), _
    Description("Contraseña del usuario de inicio de sesión")> _
    Public Property Password() As Byte()
        Get
            Return _Password
        End Get
        Set(ByVal Value() As Byte)
            _Password = Value
        End Set
    End Property

    <Category("Remoting"), Browsable(True), _
    Description("Usa DataRemoting para conexión con base de datos")> _
    Public Property UsaRemoting() As Boolean
        Get
            Return _UsaRemoting
        End Get
        Set(ByVal Value As Boolean)
            _UsaRemoting = Value
        End Set
    End Property

    <Category("Remoting"), Browsable(True), _
    Description("IP Name remoting para conexión con base de datos")> _
    Public Property RemotingIPName() As String
        Get
            Return _RemotingIPName
        End Get
        Set(ByVal Value As String)
            _RemotingIPName = Value
        End Set
    End Property

    <Category("Remoting"), Browsable(True), _
    Description("Puerto remoting para conexión con base de datos")> _
    Public Property RemotingPuerto() As Integer
        Get
            Return _RemotingPuerto
        End Get
        Set(ByVal Value As Integer)
            _RemotingPuerto = Value
        End Set
    End Property

    <Category("Remoting"), Browsable(True), _
    Description("App Name de remoting para conexión con base de datos")> _
    Public Property RemotingAppName() As String
        Get
            Return _RemotingAppName
        End Get
        Set(ByVal Value As String)
            _RemotingAppName = Value
        End Set
    End Property

    <Category("Remoting"), Browsable(True), _
    Description("Password de remoting para conexión con base de datos")> _
    Public Property RemotingPassword() As Byte()
        Get
            Return _RemotingPassword
        End Get
        Set(ByVal Value As Byte())
            _RemotingPassword = Value
        End Set
    End Property

    <Category("Remoting"), Browsable(True), _
    Description("Cifrado de remoting para conexión con base de datos")> _
    Public Property RemotingCifrado() As Boolean
        Get
            Return _RemotingCifrado
        End Get
        Set(ByVal Value As Boolean)
            _RemotingCifrado = Value
        End Set
    End Property

#End Region

#Region " Funciones "

    Public Shared Function Serialize(ByRef nObjectConfig As MailSenderConfig, ByVal nConfigFileName As String) As Boolean
        Dim objXmlSerializer As New XmlSerializer(nObjectConfig.GetType())
        Dim objTextWriter = New StreamWriter(nConfigFileName)

        Try
            objXmlSerializer.Serialize(objTextWriter, nObjectConfig)
            Return True
        Catch ex As Exception
            MessageBox.Show("No se pudo ecribir el archivo de configuración. " + ex.Message, "ObjectConfig", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            objTextWriter.Close()
        End Try
    End Function
    Public Shared Function Deserialize(ByRef nObjectConfig As MailSenderConfig, ByVal nConfigFileName As String) As Boolean
        Dim objXmlSerializer = New XmlSerializer(nObjectConfig.GetType())
        Dim objTextReader = New StreamReader(nConfigFileName)

        Try
            nObjectConfig = CType(objXmlSerializer.Deserialize(objTextReader), MailSenderConfig)
            Return True
        Catch ex As Exception
            MessageBox.Show("No se pudo leer el archivo de configuración. " + ex.Message, "ObjectConfig", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            objTextReader.Close()
        End Try
    End Function

    Public Shared Function getCadenasConexion(ByRef nWebService As SecurityWebService) As TypeConnectionString
        Dim Cadenas As New TypeConnectionString

        Dim ConnectionStrings = nWebService.getCadenasConexion()

        For Each Modulo As TypeModulo In ConnectionStrings
            Select Case Modulo.Id
                Case 3 : Cadenas.Tools = Modulo.ConnectionString
                Case 6 : Cadenas.Core = Modulo.ConnectionString
            End Select
        Next

        Return Cadenas
    End Function

    Public Shared Function GetModulosURLsSystem(ByRef _DBMMailSender As DBTools.DBToolsDataBaseManager) As TypeParameterSystem
        Dim parameters As New TypeParameterSystem()

        Dim connectionParameterSystem = _DBMMailSender.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_Proyecto(0, 0)

        For Each moduloParameter In connectionParameterSystem
            Select Case moduloParameter.Nombre_Parametro
                Case ParameterSystem.URLAPIMasic
                    parameters._URLAPIMasic = moduloParameter.Valor_Parametro
                Case ParameterSystem.UserAPIMasiv
                    parameters._UserAPIMasiv = moduloParameter.Valor_Parametro
                Case ParameterSystem.PasswordAPIMasiv
                    parameters._PasswordAPIMasiv = moduloParameter.Valor_Parametro
                Case ParameterSystem.EmailSendAditional
                    parameters._EmailSendAditional = moduloParameter.Valor_Parametro
            End Select
        Next

        Return parameters
    End Function

    Public Shared Function Encrypt(ByVal nData As String) As Byte()
        Return Crypto.DPAPI.Encrypt(nData, MemoryProtectionScope.CrossProcess, 51)
    End Function
    Public Shared Function Decrypt(ByVal nData() As Byte) As String
        Return Crypto.DPAPI.Decrypt(nData, MemoryProtectionScope.CrossProcess, 51)
    End Function

#End Region

End Class

#End Region

