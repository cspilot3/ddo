Imports Slyg.Tools.Imaging

Namespace DocumentViewerObjects

    Public MustInherit Class FileProvider

        'Public MustOverride Function GetFolios(ByVal nFileNames As List(Of String)) As Short

        Public MustOverride Function GetFolios() As Integer

        Public MustOverride Function GetFolio(ByVal nFolio As Short, ByVal nResolucion As Single, ByVal nFormat As ImageManager.EnumFormat) As Byte()

        Public MustOverride Function GetThumbnail(ByVal nFolioInicial As Integer, ByVal nFolioFinal As Integer, ByVal nMaxAncho As Integer, ByVal nMaxAlto As Integer, ByVal nFormat As ImageManager.EnumFormat) As List(Of Byte())

    End Class

End Namespace