Namespace Forms.Reportes

    Public Enum ParameterTypeEnum
        Texto = 1
        Numerico = 2
        Fecha = 3
        SiNo = 4
        Lista = 5
    End Enum

    Public Interface IParameter

        Function GetStringParameter() As String

        Function GetParameter() As Object
        
        ReadOnly Property ParameterType() As ParameterTypeEnum

        Property ParameterName() As String

    End Interface

End Namespace