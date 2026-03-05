Namespace Visor

    Public Class ImageFolio
        Public Property Filename As String
        Public Property Folio As Integer

        Public Sub New(nFilename As String, nFolio As Integer)
            Me.Filename = nFilename
            Me.Folio = nFolio
        End Sub

    End Class

End Namespace