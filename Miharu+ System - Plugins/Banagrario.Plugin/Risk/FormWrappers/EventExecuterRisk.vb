Imports Miharu.Desktop.Library.Plugins

Namespace Risk.FormWrappers

    Public Class EventExecuterRisk
        Inherits EventExecuter

#Region " Declaraciones "

        ' ReSharper disable once NotAccessedField.Local
        Private _Plugin As BanagrarioRiskPlugin

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As BanagrarioRiskPlugin)
            Me._Plugin = nPlugin
        End Sub

#End Region

    End Class

End Namespace