Imports System.IO
Imports System.Xml.Serialization
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Security.Cryptography
Imports Miharu.Security.Library
Imports Slyg.Tools
Imports Slyg.Tools.Cryptographic
Imports Miharu.Security.Library.WebService

Public Class MailConfig

#Region " Estructuras "

    Public Structure TypeConnectionString
        Public Core As String
        Public Security As String
        Public Agrario As String
        Public Imaging As String
        Public Archiving As String
        Public Tools As String
    End Structure

#End Region

#Region " Declaraciones "

    Public Const ConfigFileName As String = "MailConfig.dat"

#End Region

#Region " Propiedades "

    Public Property Intervalo() As Integer

    Public Property SecurityWebServiceURL As String

    Public Property User As String

    Public Property Password As Byte()

#End Region

#Region " Constructores "

    Public Sub New()
        Me.Intervalo = 5000
        'Me.SecurityWebServiceURL = "http://localhost:51500/SecurityService.asmx"
        Me.SecurityWebServiceURL = "http://10.64.19.32:8090/Miharu.Security.WebService/SecurityService.asmx"
        Me.User = "Service"
        Me.Password = Nothing
    End Sub

#End Region

#Region " Serializar "

    Public Shared Sub Serialize(ByRef nObjectConfig As MailConfig, ByVal nPath As String)
        If (Not Directory.Exists(nPath)) Then Directory.CreateDirectory(nPath)

        Dim Serializador As New XmlSerializer(GetType(MailConfig))
        Dim Escritor = New StreamWriter(nPath.TrimEnd("\"c) & "\" & ConfigFileName)

        Try
            Serializador.Serialize(Escritor, nObjectConfig)
        Catch ex As Exception
            Throw ex
        Finally
            Escritor.Close()
        End Try
    End Sub

    Public Shared Function Deserialize(ByVal nPath As String) As MailConfig
        Dim Serializador = New XmlSerializer(GetType(MailConfig))
        Dim Lector = New StreamReader(nPath.TrimEnd("\"c) & "\" & ConfigFileName)

        Try
            Return CType(Serializador.Deserialize(Lector), MailConfig)
        Catch ex As Exception
            Throw ex
        Finally
            Lector.Close()
        End Try
    End Function

#End Region

#Region " Funciones "

    Public Shared Function getCadenasConexion(ByRef nWebService As SecurityWebService) As TypeConnectionString
        Dim Cadenas As New TypeConnectionString

        Dim ConnectionStrings = nWebService.getCadenasConexion()

        For Each Modulo As TypeModulo In ConnectionStrings
            Select Case CType(Modulo.Id, Program.Modulo)
                Case Program.Modulo.Core : Cadenas.Core = Modulo.ConnectionString
                Case Program.Modulo.Security : Cadenas.Security = Modulo.ConnectionString
                Case Program.Modulo.Agrario : Cadenas.Agrario = Modulo.ConnectionString
                Case Program.Modulo.Imaging : Cadenas.Imaging = Modulo.ConnectionString
                Case Program.Modulo.Archiving : Cadenas.Archiving = Modulo.ConnectionString
                Case Program.Modulo.Tools : Cadenas.Tools = Modulo.ConnectionString
            End Select
        Next

        Return Cadenas
    End Function

    Public Shared Function Encrypt(ByVal nData As String) As Byte()
        Return Crypto.DPAPI.Encrypt(nData, MemoryProtectionScope.CrossProcess, 51)
    End Function

    Public Shared Function Decrypt(ByVal nData() As Byte) As String
        Return Crypto.DPAPI.Decrypt(nData, MemoryProtectionScope.CrossProcess, 51)
    End Function

#End Region

End Class
