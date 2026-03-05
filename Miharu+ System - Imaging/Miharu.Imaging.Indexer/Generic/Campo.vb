Imports Miharu.Desktop.Library.Config

Namespace Generic

    Public Class Campo

        Public Property id As Short
        Public Property Value As Object
        Public Property Type As DesktopConfig.CampoTipo

        Public Sub New(ByVal nId As Short, ByVal nValue As Object, ByVal nType As DesktopConfig.CampoTipo)
            id = nId
            Value = nValue
            Type = nType
        End Sub

    End Class

End Namespace