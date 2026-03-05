Imports System.IO
Imports System.Xml.Serialization
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Security.Cryptography
Imports Miharu.Security.Library.WebService

Public Class PunteoAgrarioFileUpConfig

#Region " Constantes"

    Public Const ConfigFileName As String = "PunteoBanAgrario.dat"

#End Region

#Region " Estructuras "

    Public Structure TypeConnectionString
        Public PunteoAgrario As String
    End Structure

#End Region

#Region " Declaraciones "

    ' Servicio
    Private _IntervaloEscaneo As Integer = 5000
    Private _IntervaloNotificacion As Integer = 1440
    Private _SecurityWebServiceURL As String = "http://10.64.19.76/Miharu.Security.webservice/SecurityService.asmx"
    Private _User As String = "edwin.arias"
    Private _Password As Byte() = Nothing
    Private _CarpetaFileUp As String = ""
    Private _CarpetaProcesado As String = ""
    Private _CarpetaNoProcesado As String = ""
    Private _CarpetaGeneracionPDF As String = ""
    Private _HoraGeneracionPDF As Integer = 0
    Private _NombreLlaveCifrado As String = ""

#End Region

#Region " Propiedades "

    ' Servicio
    <Category("Servicio"), Browsable(True), _
    Description("Define el intervalo de tiempo en milisegundos a esperar antes de validar si hay nuevas notificaciones para enviar")> _
    Public Property IntervaloEscaneo() As Integer
        Get
            Return _IntervaloEscaneo
        End Get
        Set(ByVal value As Integer)
            _IntervaloEscaneo = value
        End Set
    End Property

    <Category("Servicio"), Browsable(True), _
    Description("Define el intervalo de tiempo en minutos a esperar antes de reenviar una notificación")> _
    Public Property IntervaloNotificacion() As Integer
        Get
            Return _IntervaloNotificacion
        End Get
        Set(ByVal value As Integer)
            _IntervaloNotificacion = value
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

    <Category("Servicio"), Browsable(True), _
    Description("Carpeta donde se alojan los archivos que van a ser cargados")> _
    Public Property CarpetaFileUp() As String
        Get
            Return _CarpetaFileUp
        End Get
        Set(ByVal Value As String)
            _CarpetaFileUp = Value
        End Set
    End Property

    <Category("Servicio"), Browsable(True), _
    Description("Carpeta donde se almacenan los archivos que pueron cargados de forma exitosa")> _
    Public Property CarpetaProcesado() As String
        Get
            Return _CarpetaProcesado
        End Get
        Set(ByVal Value As String)
            _CarpetaProcesado = Value
        End Set
    End Property

    <Category("Servicio"), Browsable(True), _
    Description("Carpeta donde se almacenan los archivos que no se pudieron cargar debido al incumplimiento de " & _
        "alguna validación o un error de Base de Datos")> _
    Public Property CarpetaNoProcesado() As String
        Get
            Return _CarpetaNoProcesado
        End Get
        Set(ByVal Value As String)
            _CarpetaNoProcesado = Value
        End Set
    End Property

    <Category("Servicio"), Browsable(True), _
    Description("Hora en la cual se debe generar los PDF")> _
    Public Property HoraGeneracionPDF() As String
        Get
            Return _HoraGeneracionPDF
        End Get
        Set(ByVal Value As String)
            _HoraGeneracionPDF = Value
        End Set
    End Property

    <Category("Servicio"), Browsable(True), _
   Description("Carpeta donde se almacenan los reportes en formato PDF")> _
    Public Property CarpetaGeneracionPDF() As String
        Get
            Return _CarpetaGeneracionPDF
        End Get
        Set(ByVal Value As String)
            _CarpetaGeneracionPDF = Value
        End Set
    End Property

    <Category("Servicio"), Browsable(True), _
    Description("Nombre de la llave de Cifrado")> _
    Public Property NombreLlaveCifrado() As String
        Get
            Return _NombreLlaveCifrado
        End Get
        Set(ByVal Value As String)
            _NombreLlaveCifrado = Value
        End Set
    End Property
#End Region

#Region " Funciones "

    Public Shared Sub Serialize(ByRef nObjectConfig As PunteoAgrarioFileUpConfig, ByVal nPath As String)
        If (Not Directory.Exists(nPath)) Then Directory.CreateDirectory(nPath)
        Dim Serializador As New XmlSerializer(GetType(PunteoAgrarioFileUpConfig))
        Dim Escritor = New StreamWriter(nPath.TrimEnd("\"c) & "\" & ConfigFileName)

        Try
            Serializador.Serialize(Escritor, nObjectConfig)
        Catch ex As Exception
            Throw ex
        Finally
            Escritor.Close()
        End Try
    End Sub

    Public Shared Function Deserialize(ByVal nPath As String) As PunteoAgrarioFileUpConfig
        Dim Serializador = New XmlSerializer(GetType(PunteoAgrarioFileUpConfig))
        Dim Lector = New StreamReader(nPath.TrimEnd("\"c) & "\" & ConfigFileName)

        Try
            Return CType(Serializador.Deserialize(Lector), PunteoAgrarioFileUpConfig)
        Catch ex As Exception
            Throw ex
        Finally
            Lector.Close()
        End Try
    End Function

    Public Shared Function getCadenasConexion(ByRef nWebService As SecurityWebService) As TypeConnectionString
        Dim Cadenas As New TypeConnectionString
        Dim ConnectionStrings = nWebService.getCadenasConexion()

        For Each Modulo In ConnectionStrings
            Select Case Modulo.Id
                Case 13 : Cadenas.PunteoAgrario = Modulo.ConnectionString
            End Select
        Next

        Return Cadenas
    End Function

    Public Shared Function Encrypt(ByVal nData As String) As Byte()
        Return Slyg.Tools.Cryptographic.Crypto.DPAPI.Encrypt(nData, MemoryProtectionScope.CrossProcess, 51)
    End Function

    Public Shared Function Decrypt(ByVal nData() As Byte) As String
        Return Slyg.Tools.Cryptographic.Crypto.DPAPI.Decrypt(nData, MemoryProtectionScope.CrossProcess, 51)
    End Function

#End Region

End Class
