Imports Slyg.Tools.Imaging

Namespace DocumentViewerObjects

    Public Class FileProviderLocal
        Inherits FileProvider

        Public Property FileNames As List(Of String)

        Public Sub New()
            FileNames = New List(Of String)
        End Sub

        Public Sub New(ByVal nFileNames As String)
            FileNames = New List(Of String)
            FileNames.Add(nFileNames)
        End Sub

        Public Sub New(ByVal nFileNames As List(Of String))
            FileNames = nFileNames
        End Sub

        Public Overrides Function GetFolio(ByVal nFolio As Short, ByVal nResolucion As Single, ByVal nFormat As ImageManager.EnumFormat) As Byte()
            Return ImageManager.GetFolioData(FileNames(nFolio - 1), 1, nFormat, Slyg.Tools.Imaging.ImageManager.EnumCompression.None)
        End Function

        Public Overloads Overrides Function GetFolios() As Integer
            Return FileNames.Count
        End Function

        Public Overrides Function GetThumbnail(ByVal nFolioInicial As Integer, ByVal nFolioFinal As Integer, ByVal nMaxAncho As Integer, ByVal nMaxAlto As Integer, ByVal nFormat As ImageManager.EnumFormat) As List(Of Byte())
            Dim ThumbnailList = New List(Of Byte())

            For i As Integer = nFolioInicial - 1 To nFolioFinal - 1
                ThumbnailList.Add(ImageManager.GetThumbnailData(FileNames(i), 1, 1, nMaxAncho, nMaxAlto)(0))
            Next

            Return ThumbnailList
        End Function

    End Class

End Namespace