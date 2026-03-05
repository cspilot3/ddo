Imports System.IO
Imports System.Xml.Serialization
Imports System.Security.Cryptography
Imports Slyg.Tools.Cryptographic
Imports Miharu.Security.Library.WebService

Namespace Servicio
    Public Class MigracionConfig
#Region " Estructuras "

        Public Structure TypeConnectionString
            Public RCore As String
            Public RImaging As String
            Public RSecurity As String
            Public RArchiving As String
            Public CCore As String
            Public CImaging As String
            Public CSecurity As String
            Public CArchiving As String
        End Structure

#End Region

#Region " Declaraciones "

        Public Const ConfigFileName As String = "MigracionConfig.dat"

#End Region

#Region " Propiedades "

        Public Property Intervalo() As Integer

        Public Property RSecurityWebServiceURL() As String

        Public Property CSecurityWebServiceURL() As String

        Public Property User() As String

        Public Property Password() As Byte()

#End Region

#Region " Constructores "

        Public Sub New()
            Me.Intervalo = 5000
            Me.RSecurityWebServiceURL = "http://PBOGDESYBARRIOS/Miharu.Security.WebServiceR/SecurityService.asmx"
            Me.CSecurityWebServiceURL = "http://PBOGDESYBARRIOS/Miharu.Security.WebService/SecurityService.asmx"
            Me.User = "Service"
            Me.Password = Nothing
        End Sub

#End Region

#Region " Serializar "

        Public Shared Sub Serialize(ByRef nObjectConfig As MigracionConfig, ByVal nPath As String)
            If (Not Directory.Exists(nPath)) Then Directory.CreateDirectory(nPath)

            Dim Serializador As New XmlSerializer(GetType(MigracionConfig))
            Dim Escritor = New StreamWriter(nPath.TrimEnd("\"c) & "\" & ConfigFileName)

            Try
                Serializador.Serialize(Escritor, nObjectConfig)
            Catch
                Throw
            Finally
                Escritor.Close()
            End Try
        End Sub

        Public Shared Function Deserialize(ByVal nPath As String) As MigracionConfig
            Dim Serializador = New XmlSerializer(GetType(MigracionConfig))
            Dim Lector = New StreamReader(nPath.TrimEnd("\"c) & "\" & ConfigFileName)

            Try
                Return CType(Serializador.Deserialize(Lector), MigracionConfig)
            Catch
                Throw
            Finally
                Lector.Close()
            End Try
        End Function

#End Region

#Region " Funciones "

        Public Shared Function getCadenasConexionR(ByRef nWebService As SecurityWebService) As TypeConnectionString
            Dim CadenasR As New TypeConnectionString

            Dim ConnectionStringsR = nWebService.getCadenasConexion()

            For Each Modulo As TypeModulo In ConnectionStringsR
                Select Case CType(Modulo.Id, Program.Modulo)
                    Case Program.Modulo.RCore : CadenasR.RCore = Modulo.ConnectionString
                    Case Program.Modulo.RImaging : CadenasR.RImaging = Modulo.ConnectionString
                    Case Program.Modulo.RSecurity : CadenasR.RSecurity = Modulo.ConnectionString
                    Case Program.Modulo.RArchiving : CadenasR.RArchiving = Modulo.ConnectionString
                End Select
            Next

            Return CadenasR
        End Function

        Public Shared Function getCadenasConexionC(ByRef nWebService As SecurityWebService) As TypeConnectionString
            Dim CadenasC As New TypeConnectionString

            Dim ConnectionStringsC = nWebService.getCadenasConexion()

            For Each Modulo As TypeModulo In ConnectionStringsC
                Select Case CType(Modulo.Id, Program.Modulo)
                    Case Program.Modulo.CCore : CadenasC.CCore = Modulo.ConnectionString
                    Case Program.Modulo.CImaging : CadenasC.CImaging = Modulo.ConnectionString
                    Case Program.Modulo.CSecurity : CadenasC.CSecurity = Modulo.ConnectionString
                    Case Program.Modulo.CArchiving : CadenasC.CArchiving = Modulo.ConnectionString
                End Select
            Next

            Return CadenasC
        End Function
        Public Shared Function Encrypt(ByVal nData As String) As Byte()
            Return Crypto.DPAPI.Encrypt(nData, MemoryProtectionScope.CrossProcess, 51)
        End Function

        Public Shared Function Decrypt(ByVal nData() As Byte) As String
            Return Crypto.DPAPI.Decrypt(nData, MemoryProtectionScope.CrossProcess, 51)
        End Function

#End Region
    End Class
End Namespace
