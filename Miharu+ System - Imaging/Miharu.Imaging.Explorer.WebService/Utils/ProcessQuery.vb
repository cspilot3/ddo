Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Web

Public Class ProcessQuery
    Public Function getConnectionString() As String
        Return System.Configuration.ConfigurationManager.ConnectionStrings("SQLCnnMiharu.CoreDMZ").ConnectionString
    End Function

    Public Function getConnectionStringImaging() As String
        Return System.Configuration.ConfigurationManager.ConnectionStrings("SQLCnnMiharu.ImagingDMZ").ConnectionString
    End Function

    Public Function execute(ByVal queryRequest As QueryRequest) As QueryResponse
        If queryRequest.parameters Is Nothing Then queryRequest.parameters = New List(Of QueryParameter)()
        Dim queryResponse As QueryResponse = New QueryResponse()

        Try

            If queryRequest.queryRequestType = QueryRequestType.StoredProcedure Then
                queryResponse = executeStoredProcedure(queryRequest)
            Else
                queryResponse.dataTable = executeTableQuery(queryRequest)
                queryResponse.rows = queryResponse.dataTable.Rows.Count
                queryResponse.success = True
            End If

        Catch ex As Exception
            queryResponse.[error] = ex.Message
            queryResponse.success = False
        End Try

        Return queryResponse
    End Function

    Private Function executeStoredProcedure(ByVal queryRequest As QueryRequest) As QueryResponse
        Dim queryResponse As QueryResponse = New QueryResponse()

        Select Case queryRequest.queryResponseType
            Case QueryResponseType.Table
                queryResponse.dataTable = executeDataTable(queryRequest)
                queryResponse.rows = queryResponse.dataTable.Rows.Count
                queryResponse.success = True
            Case QueryResponseType.Scalar
                queryResponse.scalar = executeScalar(queryRequest)
                queryResponse.success = True
            Case QueryResponseType.NonQuery
                queryResponse.rows = executeNonQuery(queryRequest)
                queryResponse.success = True
            Case Else
                queryResponse.[error] = "Tipo operacion desconocida"
                queryResponse.success = False
        End Select

        Return queryResponse
    End Function

    Private Function executeDataTable(ByVal queryRequest As QueryRequest) As DataTable
        Dim sqlConnection As SqlConnection = Nothing

        Try
            sqlConnection = New SqlConnection(getConnectionString())
            sqlConnection.Open()
            Dim sqlCommand As SqlCommand = sqlConnection.CreateCommand()
            sqlCommand.CommandType = CommandType.StoredProcedure
            sqlCommand.CommandText = queryRequest.name
            SqlCommandBuilder.DeriveParameters(sqlCommand)

            For Each parameter As QueryParameter In queryRequest.parameters
                sqlCommand.Parameters("@" & parameter.name).Value = parameter.value
            Next

            Dim sqlDataAdapter As SqlDataAdapter = New SqlDataAdapter(sqlCommand)
            Dim dataTable As DataTable = New DataTable()
            sqlDataAdapter.Fill(dataTable)
            sqlDataAdapter.Dispose()
            dataTable.TableName = "Response"
            Return dataTable
        Finally

            Try
                sqlConnection.Close()
            Catch
            End Try
        End Try
    End Function

    Private Function executeScalar(ByVal queryRequest As QueryRequest) As Object
        Dim sqlConnection As SqlConnection = Nothing

        Try
            sqlConnection = New SqlConnection(getConnectionString())
            sqlConnection.Open()
            Dim sqlCommand As SqlCommand = sqlConnection.CreateCommand()
            sqlCommand.CommandType = CommandType.StoredProcedure
            sqlCommand.CommandText = queryRequest.name
            SqlCommandBuilder.DeriveParameters(sqlCommand)

            For Each parameter As QueryParameter In queryRequest.parameters
                sqlCommand.Parameters("@" & parameter.name).Value = parameter.value
            Next

            Return sqlCommand.ExecuteScalar()
        Finally

            Try
                sqlConnection.Close()
            Catch
            End Try
        End Try
    End Function

    Private Function executeNonQuery(ByVal queryRequest As QueryRequest) As Integer
        Dim sqlConnection As SqlConnection = Nothing

        Try
            sqlConnection = New SqlConnection(getConnectionString())
            sqlConnection.Open()
            Dim sqlCommand As SqlCommand = sqlConnection.CreateCommand()
            sqlCommand.CommandType = CommandType.StoredProcedure
            sqlCommand.CommandText = queryRequest.name
            SqlCommandBuilder.DeriveParameters(sqlCommand)

            For Each parameter As QueryParameter In queryRequest.parameters
                sqlCommand.Parameters("@" & parameter.name).Value = parameter.value
            Next

            Return sqlCommand.ExecuteNonQuery()
        Finally

            Try
                sqlConnection.Close()
            Catch
            End Try
        End Try
    End Function

    Private Function executeTableQuery(ByVal queryRequest As QueryRequest) As DataTable
        Dim sqlConnection As SqlConnection = Nothing
        Dim sqlstring As String = "select * from " & queryRequest.name

        If queryRequest.parameters.Count > 0 Then
            sqlstring += " where "
            Dim i As Integer = 0

            For Each parameter As QueryParameter In queryRequest.parameters

                If i > 0 Then
                    sqlstring += " and "
                End If

                sqlstring += "[" & parameter.name & "]=@" + parameter.name
                i += 1
            Next
        End If

        Try
            sqlConnection = New SqlConnection(getConnectionString())
            sqlConnection.Open()
            Dim sqlCommand As SqlCommand = sqlConnection.CreateCommand()
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.CommandText = sqlstring

            For Each parameter As QueryParameter In queryRequest.parameters
                sqlCommand.Parameters.Add(New SqlParameter("@" & parameter.name, parameter.value))
            Next

            Dim sqlDataAdapter As SqlDataAdapter = New SqlDataAdapter(sqlCommand)
            Dim dataTable As DataTable = New DataTable()
            sqlDataAdapter.Fill(dataTable)
            sqlDataAdapter.Dispose()
            dataTable.TableName = "Response"
            Return dataTable
        Finally

            Try
                sqlConnection.Close()
            Catch
            End Try
        End Try
    End Function
End Class
