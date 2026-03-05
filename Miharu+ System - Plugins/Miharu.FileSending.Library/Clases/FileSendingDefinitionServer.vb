Imports System.Xml.Serialization
Imports System.IO

Namespace Clases

    Public Class FileSendingDefinitionServer

#Region " Declaraciones "

        Private _LastSentPart As Integer

#End Region

#Region " Propiedades "

        Public Property FileName As String

        Public Property ID As String

        Public Property FileSize As Long

        Public Property PackageSize As Integer

        Public Property FilePackages As Integer

        'Public Property Parts As Boolean()

        Public Property CreationDate As DateTime

        Public Property LastUpdate As DateTime

        Public Property LastSentPart As Integer
            Get
                Return _LastSentPart
            End Get
            Set(ByVal value As Integer)
                'Parts(value) = True
                _LastSentPart = value
                LastUpdate = Now
            End Set
        End Property

#End Region

#Region " Constructores "

        Public Sub New()

        End Sub

        Public Sub New(ByVal nDefinition As FileSendingDefinitionClient)
            _CreationDate = Now
            _LastUpdate = Now

            _FileName = Path.GetFileName(nDefinition.FileName)
            _ID = nDefinition.ID
            _FileSize = nDefinition.FileSize
            _PackageSize = nDefinition.PackageSize
            _FilePackages = nDefinition.FilePackages

            'ReDim _Parts(_FilePackages - 1)
            _LastSentPart = -1
        End Sub

#End Region

#Region " Metodos "

        Public Shared Sub Serialize(ByVal nObject As FileSendingDefinitionServer, ByVal nFileName As String)
            Dim Serializador As New XmlSerializer(GetType(FileSendingDefinitionServer))
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

        Public Shared Function Deserialize(ByVal nFileName As String) As FileSendingDefinitionServer
            Dim Serializador As New XmlSerializer(GetType(FileSendingDefinitionServer))
            Dim Archivo As New StreamReader(nFileName)

            Try
                Return CType(Serializador.Deserialize(Archivo), FileSendingDefinitionServer)
            Catch ex As Exception
                Throw New Exception("No se pudo leer el archivo. " & ex.Message, ex)
            Finally
                Archivo.Close()
            End Try
        End Function

#End Region

    End Class

End Namespace