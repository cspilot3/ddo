Imports System.IO
Imports System.Xml.Serialization
Imports System.Security.Cryptography
Imports Miharu.Security.Library.WebService

Namespace Clases

    Public Class FileSendingConfig

#Region " Declaraciones "

        Public Const ConfigFileName As String = "FileSendingConfig.dat"

#End Region

#Region " Estructuras "

        Public Structure TypeConnectionString
            Public Core As String
            Public Imaging As String
            Public Banagrario As String
            Public Archiving As String
            Public Security As String
            Public Integration As String
            Public Tools As String
        End Structure

#End Region

#Region " Propiedades "

        Public Property SecurityWebServiceURL As String

        Public Property SecurityWebServiceUser As String

        Public Property SecurityWebServicePassword As Byte()

        Public Property Puerto() As Integer

        Public Property AppName() As String

        Public Property WorkingFolder() As String

        Public Property IdentifierDateFormat As String

        Public Property UsaRemoting As Boolean

        Public Property RemotingIPName As String

        Public Property RemotingPuerto() As Integer

        Public Property RemotingAppName() As String

        Public Property RemotingPassword As Byte()

        Public Property RemotingCifrado As Boolean

        Public Property LogFolder As String

        Public Property LogActivo As Boolean

#End Region

#Region " Constructores "

        Public Sub New()
            Me.SecurityWebServiceURL = "http://localhost/Miharu.Security.WebService/SecurityService.asmx"
            Me.SecurityWebServiceUser = "Service"
            Me.SecurityWebServicePassword = Nothing
            Me.Puerto = 8087
            Me.AppName = "FileSendingServiceApp"
            Me.WorkingFolder = ""
            Me.IdentifierDateFormat = "yyyy-MM-dd HH:mm:ss"
            Me.LogFolder = ""
            Me.LogActivo = CBool(0)
            'Me.UsaRemoting = False
            'Me.RemotingIPName = ""
            'Me.RemotingPuerto = CInt("")
            'Me.RemotingAppName = ""
            'Me.RemotingPassword = Nothing
            'Me.RemotingCifrado = CBool("")
        End Sub

#End Region

#Region " Serializar "

        Public Shared Sub Serialize(ByRef nObjectConfig As FileSendingConfig, ByVal nPath As String)
            If (Not Directory.Exists(nPath)) Then Directory.CreateDirectory(nPath)

            Dim Serializador As New XmlSerializer(GetType(FileSendingConfig))
            Dim Escritor = New StreamWriter(nPath.TrimEnd("\"c) & "\" & ConfigFileName)

            Try
                Serializador.Serialize(Escritor, nObjectConfig)
            Catch
                Throw
            Finally
                Escritor.Close()
            End Try
        End Sub

        Public Shared Function Deserialize(ByVal nPath As String) As FileSendingConfig
            Dim Serializador = New XmlSerializer(GetType(FileSendingConfig))
            Dim Lector = New StreamReader(nPath.TrimEnd("\"c) & "\" & ConfigFileName)

            Try
                Return CType(Serializador.Deserialize(Lector), FileSendingConfig)
            Catch
                Throw
            Finally
                Lector.Close()
            End Try
        End Function

#End Region

#Region " Funciones "

        Public Shared Function getCadenasConexion(ByRef nWebService As SecurityWebService) As TypeConnectionString
            Dim Cadenas As New TypeConnectionString
            Dim ConnectionStrings = nWebService.getCadenasConexion()

            For Each Modulo In ConnectionStrings
                Select Case Modulo.Id
                    Case 0 : Cadenas.Security = Modulo.ConnectionString
                    Case 2 : Cadenas.Imaging = Modulo.ConnectionString
                    Case 3 : Cadenas.Tools = Modulo.ConnectionString
                    Case 6 : Cadenas.Core = Modulo.ConnectionString
                    Case 13 : Cadenas.Banagrario = Modulo.ConnectionString
                    Case 30 : Cadenas.Integration = Modulo.ConnectionString
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
End Namespace