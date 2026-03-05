Public Class Item

#Region " Declaraciones "

    Private _Value As Object
    Private _Display As String

#End Region

#Region " Metodos "

    Public Sub New(ByVal nValue As Object, ByVal nDisplay As String)
        _Value = nValue
        _Display = nDisplay
    End Sub

#End Region

#Region " Propiedades "

    Public ReadOnly Property Value() As Object
        Get
            Return _Value
        End Get
    End Property
    Public ReadOnly Property Display() As String
        Get
            Return _Display
        End Get
    End Property

#End Region

#Region " Funciones "

    Public Overrides Function ToString() As String
        Return CStr(Me._Value) & " - " & Me._Display
    End Function

#End Region

End Class
