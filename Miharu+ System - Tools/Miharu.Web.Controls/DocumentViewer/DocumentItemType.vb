Namespace DocumentViewerObjects

    <Serializable()> _
    Public Class DocumentItemType

        Public ImageURL As String
        Public ThumbnailURL As String
        Public Rotacion As Byte

        Public Sub New()

        End Sub

        Public Sub New(ByVal Data As String)
            Dim Partes() As String = Data.Split("|"c)

            ImageURL = Partes(0)
            ThumbnailURL = Partes(1)
            Rotacion = CByte(Partes(2))
        End Sub

        Friend Function getData() As String
            Return ImageURL & "|" & ThumbnailURL & "|" & Rotacion
        End Function

    End Class

End Namespace