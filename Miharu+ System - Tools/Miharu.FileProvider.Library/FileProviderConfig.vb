Imports System.IO
Imports System.Xml.Serialization
Imports System.Security.Cryptography

Public Class FileProviderConfig

#Region " Declaraciones "

    Public Const ConfigFileName As String = "FileProviderConfig.dat"

#End Region

#Region " Propiedades "

    Public Property Puerto() As Integer

    Public Property AppName() As String

    Public Property WorkingFolder() As String

    Public Property IntervaloDepuracion As Integer

    Public Property TiempoHistoricoEliminacion As Integer

#End Region

#Region " Constructores "

    Public Sub New()
        Puerto = 8083
        AppName = "FileProviderServiceApp"
        IntervaloDepuracion = 21600000 '6 Horas
        TiempoHistoricoEliminacion = 24 ' Horas
    End Sub

#End Region

#Region " Serializar "

    Public Shared Sub Serialize(ByRef nObjectConfig As FileProviderConfig, nPath As String)
        If (Not Directory.Exists(nPath)) Then Directory.CreateDirectory(nPath)

        Dim Serializador As New XmlSerializer(GetType(FileProviderConfig))
        Dim Escritor = New StreamWriter(nPath.TrimEnd("\"c) & "\" & ConfigFileName)

        Try
            Serializador.Serialize(Escritor, nObjectConfig)
        Catch
            Throw
        Finally
            Escritor.Close()
        End Try
    End Sub

    Public Shared Function Deserialize(nPath As String) As FileProviderConfig
        Dim Serializador = New XmlSerializer(GetType(FileProviderConfig))
        Dim Lector = New StreamReader(nPath.TrimEnd("\"c) & "\" & ConfigFileName)

        Try
            Return CType(Serializador.Deserialize(Lector), FileProviderConfig)
        Catch
            Throw
        Finally
            Lector.Close()
        End Try
    End Function

#End Region

#Region " Funciones "

    Public Shared Function Encrypt(nData As String) As Byte()
        Return Slyg.Tools.Cryptographic.Crypto.DPAPI.Encrypt(nData, MemoryProtectionScope.CrossProcess, 51)
    End Function

    Public Shared Function Decrypt(nData() As Byte) As String
        Return Slyg.Tools.Cryptographic.Crypto.DPAPI.Decrypt(nData, MemoryProtectionScope.CrossProcess, 51)
    End Function

#End Region

End Class