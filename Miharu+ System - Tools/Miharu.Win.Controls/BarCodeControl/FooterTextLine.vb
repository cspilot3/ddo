Namespace BarCode

    Public Class FooterLineItem

#Region " Declaraciones "

        Private _Text As String
        Private _Column As Integer

#End Region

#Region " Propiedades "

        Public Property Text() As String
            Get
                Return _Text
            End Get
            Set(ByVal value As String)
                _Text = value
            End Set
        End Property

        Public Property Column() As Integer
            Get
                Return _Column
            End Get
            Set(ByVal value As Integer)
                _Column = value
            End Set
        End Property

#End Region

#Region " Constructores "

        Public Sub New()
            _Text = ""
            _Column = 1
        End Sub

        Public Sub New(ByVal nText As String, ByVal nColumn As Integer)
            _Text = nText
            _Column = nColumn
        End Sub

#End Region

    End Class

End Namespace