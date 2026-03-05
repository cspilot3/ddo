Imports System.Windows.Forms

Namespace Utils

    Public Class OkControl
        Inherits Control

        Sub New()
            MyBase.New()
            MyBase.Width = 12
            MyBase.Height = 12
            MyBase.BackgroundImage = Nothing
            MyBase.TabIndex = 0
            MyBase.TabStop = False
        End Sub

        Private _IsOk As TriState = TriState.UseDefault

        Public Property OK() As TriState
            Get
                Return _IsOk
            End Get
            Set(ByVal value As TriState)
                _IsOk = value
                Select Case (value)
                    Case TriState.UseDefault : MyBase.BackgroundImage = Nothing
                    Case TriState.True : MyBase.BackgroundImage = Desktop.Controls.My.Resources.Resources.ok1
                    Case TriState.False : MyBase.BackgroundImage = Desktop.Controls.My.Resources.Resources.no1
                End Select
            End Set
        End Property
    End Class

End Namespace