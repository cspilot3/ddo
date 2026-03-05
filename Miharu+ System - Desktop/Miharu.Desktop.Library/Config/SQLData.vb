Imports Slyg.Data.Manager
Imports Slyg.Data.DataBase

Namespace Config

    Public Class SqlData

        Public Shared Function ExecuteQuery(ByVal nQuery As String, ByVal nConnection As DataBaseManager) As DataSet            
            Return ExecuteQuery(nQuery, nConnection, DataBaseType.SqlServer)
        End Function

        Public Shared Function ExecuteQuery(ByVal nQuery As String, ByVal nConnection As DataBaseManager, dataBaseType As DataBaseType) As DataSet
            Dim adapter As System.Data.Common.DataAdapter

            Select Case dataBaseType
                Case Slyg.Data.DataBase.DataBaseType.Oracle
                    adapter = New OracleClient.OracleDataAdapter(nQuery, nConnection.DataBase.Connection)
                    CType(adapter, OracleClient.OracleDataAdapter).SelectCommand.Transaction = nConnection.DataBase.Transaction
                    CType(adapter, OracleClient.OracleDataAdapter).SelectCommand.CommandTimeout = 3600000 '60 Minutos 

                Case Slyg.Data.DataBase.DataBaseType.SqlServer
                    adapter = New SqlClient.SqlDataAdapter(nQuery, nConnection.DataBase.Connection)
                    CType(adapter, SqlClient.SqlDataAdapter).SelectCommand.Transaction = nConnection.DataBase.Transaction
                    CType(adapter, SqlClient.SqlDataAdapter).SelectCommand.CommandTimeout = 3600000 '60 Minutos 
                Case Else
                    Throw New Exception("El tipo de servidor no es válido")
            End Select

            Dim data As New DataSet            
            adapter.Fill(data)
            Return data
        End Function

        Public Shared Function ExecuteReader(ByVal nQuery As String, ByVal nConnection As DataBaseManager) As Common.DbDataReader            
            Return ExecuteReader(nQuery, nConnection, DataBaseType.SqlServer)
        End Function

        Public Shared Function ExecuteReader(ByVal nQuery As String, ByVal nConnection As DataBaseManager, dataBaseType As DataBaseType) As Common.DbDataReader
            Dim cmd As System.Data.Common.DbCommand

            Select Case dataBaseType
                Case Slyg.Data.DataBase.DataBaseType.Oracle
                    cmd = New OracleClient.OracleCommand(nQuery, nConnection.DataBase.Connection)

                Case Slyg.Data.DataBase.DataBaseType.SqlServer
                    cmd = New SqlClient.SqlCommand(nQuery, nConnection.DataBase.Connection)                    
                Case Else
                    Throw New Exception("El tipo de servidor no es válido")
            End Select

            cmd.Transaction = nConnection.DataBase.Transaction
            cmd.CommandTimeout = 3600000 '60 Minutos             

            Return cmd.ExecuteReader()
        End Function

    End Class

End Namespace