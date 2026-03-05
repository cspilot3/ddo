Imports System.Xml.Serialization
Imports System.IO

Namespace Clases

    <Serializable()> _
    Public Class FileSendingDefinitionClient

#Region " Declaraciones "

        Private _LastSentPart As Integer

#End Region

#Region " Propiedades "

        Public Property FileName As String

        Public Property ID As String

        Public Property FileSize As Long

        Public Property PackageSize As Integer

        Public Property FilePackages As Integer

        Public Property CreationDate As DateTime

        Public Property LastUpdate As DateTime

        Public Property LastSentPart As Integer
            Get
                Return _LastSentPart
            End Get
            Set(ByVal value As Integer)
                _LastSentPart = value
                LastUpdate = Now
            End Set
        End Property

        Public Property Cancel As Boolean = False

        Public Property Estado As String

        Public Property Message As String

        Public Property Oficina As Integer

        Public Property id_Log As Integer

        Public Property Fecha_Movimiento As DateTime

        Public Property FechaMovimiento As String

        Public Property Tipo_Movimiento As Short

        Public Property Usuario As Integer

#End Region

#Region " Constructores "

        Public Sub New()

        End Sub

        Public Sub New(ByVal nFileName As String, ByVal nID As String, ByVal nFileSize As Long, ByVal nPackageSize As Integer, ByVal nOficina As Integer, nFechaMovimiento As DateTime, nTipoMovimiento As Short, ByVal nUsuario As Integer)
            _CreationDate = Now
            _LastUpdate = Now

            _FileName = nFileName
            _ID = nID
            _FileSize = nFileSize
            _PackageSize = nPackageSize
            _FilePackages = CInt(((nFileSize - (nFileSize Mod nPackageSize)) / nPackageSize) + CInt(IIf(nFileSize Mod nPackageSize = 0, 0, 1)))

            _Oficina = nOficina
            _Fecha_Movimiento = nFechaMovimiento
            _Tipo_Movimiento = nTipoMovimiento
            _Usuario = nUsuario

            _LastSentPart = -1
        End Sub

        Public Sub New(ByVal nFileName As String, ByVal nID As String, ByVal nFileSize As Long, ByVal nPackageSize As Integer, ByVal nOficina As Integer, nFechaMovimiento As String, ByVal nUsuario As Integer)
            _CreationDate = Now
            _LastUpdate = Now

            _FileName = nFileName
            _ID = nID
            _FileSize = nFileSize
            _PackageSize = nPackageSize
            _FilePackages = CInt(((nFileSize - (nFileSize Mod nPackageSize)) / nPackageSize) + CInt(IIf(nFileSize Mod nPackageSize = 0, 0, 1)))

            _Oficina = nOficina
            _FechaMovimiento = nFechaMovimiento
            _Usuario = nUsuario

            _LastSentPart = -1
        End Sub


        Public Sub New(ByVal nFileName As String, ByVal nID As String, ByVal nFileSize As Long, ByVal nPackageSize As Integer, ByVal nUsuario As Integer)
            _CreationDate = Now
            _LastUpdate = Now

            _FileName = nFileName
            _ID = nID
            _FileSize = nFileSize
            _PackageSize = nPackageSize
            _FilePackages = CInt(((nFileSize - (nFileSize Mod nPackageSize)) / nPackageSize) + CInt(IIf(nFileSize Mod nPackageSize = 0, 0, 1)))

            _Oficina = -1
            _Fecha_Movimiento = Nothing
            _Tipo_Movimiento = -1
            _Usuario = nUsuario

            _LastSentPart = -1
        End Sub

#End Region

#Region " Metodos "

        Public Shared Sub Serialize(ByVal nObject As FileSendingDefinitionClient, ByVal nFileName As String)
            Dim Serializador As New XmlSerializer(GetType(FileSendingDefinitionClient))
            Dim Archivo As New StreamWriter(nFileName)

            Try
                nObject.LastUpdate = DateTime.Now
                Serializador.Serialize(Archivo, nObject)

            Catch ex As Exception
                Throw New Exception("No se pudo almacenar el archivo. " & ex.Message, ex)
            Finally
                Archivo.Close()
            End Try
        End Sub

#End Region

#Region " Funciones "

        Public Shared Function Deserialize(ByVal nFileName As String) As FileSendingDefinitionClient
            Dim Serializador As New XmlSerializer(GetType(FileSendingDefinitionClient))
            Dim Archivo As New StreamReader(nFileName)

            Try
                Return CType(Serializador.Deserialize(Archivo), FileSendingDefinitionClient)
            Catch ex As Exception
                Throw New Exception("No se pudo leer el archivo. " & ex.Message, ex)
            Finally
                Archivo.Close()
            End Try
        End Function

        Public Function getAvance() As Single
            If (FilePackages > 0) Then
                Return CSng((LastSentPart / FilePackages) * 100)
            Else
                Return 0
            End If
        End Function

#End Region

    End Class

End Namespace