Namespace MiharuDMZ

    Public Class PostRequestModel
        Public Property JsonPayload As String
        Public Property Url As String
    End Class

    Public Class QueryParameter
        Public Property name As String
        Public Property value As String
    End Class

    Public Class QueryRequest
        Public Property name As String
        Public Property queryRequestType As QueryRequestType
        Public Property queryResponseType As QueryResponseType
        Public Property parameters As List(Of QueryParameter)
    End Class

    Public Enum QueryRequestType
        StoredProcedure
        Table
    End Enum

    Public Class QueryResponse
        Public Property dataTable As DataTable
        Public Property scalar As Object
        Public Property rows As Integer
        Public Property success As Boolean
        Public Property [error] As String
    End Class

    Public Enum QueryResponseType
        Table
        Scalar
        NonQuery
    End Enum
End Namespace
