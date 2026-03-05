Namespace Generic

    Public Class Validacion

        Public Property id As Short
        Public Property Value As Boolean
        Public Property Obligatorio As Boolean

        Public Sub New(ByVal nId As Short, ByVal nValue As Boolean, ByVal nObligatorio As Boolean)
            id = nId
            Value = nValue
            Obligatorio = nObligatorio
        End Sub

    End Class

End Namespace