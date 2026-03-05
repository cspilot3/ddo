Imports DBArchiving
Imports DBCore
Imports DBIntegration
Imports Slyg.Data.Manager
Imports DBImaging

Namespace Config

    Partial Public Class BulkInsert

        Public Shared Sub InsertDataTable(ByVal data As DataTable, ByVal dmArchiving As DBArchivingDataBaseManager, ByVal tableName As String)
            Dim collation = dmArchiving.DataBase.ExecuteQueryGet("SELECT CONVERT (varchar, DATABASEPROPERTYEX('" & dmArchiving.DataBase.Connection.Database & "','collation')) as Colation")

            'Agrega un autonumerico para validacion de las filas
            data.Columns.Add("REGISTRO")
            For i As Integer = 0 To data.Rows.Count - 1 Step 1
                data.Rows(i)("REGISTRO") = i + 1
            Next

            'Crea una tabla temporal en la base de datos
            Dim query As String = "CREATE TABLE " & tableName & " ("
            For i As Integer = 1 To data.Columns.Count - 1 Step 1
                query &= "Column" & i.ToString() & " VARCHAR(200) COLLATE " & collation.Rows(0)(0).ToString & ","
            Next
            query &= "REGISTRO VARCHAR(10) COLLATE " & collation.Rows(0)(0).ToString & ");"
            dmArchiving.DataBase.ExecuteNonQuery(query)

            'Inserta los datos en la tabla creada
            Dim bulkCopy As New SqlClient.SqlBulkCopy(CType(dmArchiving.DataBase.Connection, SqlClient.SqlConnection))
            bulkCopy.DestinationTableName = tableName
            bulkCopy.WriteToServer(data)
        End Sub

        Public Shared Sub InsertDataTableColumna(ByVal data As DataTable, ByVal dmArchiving As DBArchivingDataBaseManager, ByVal tableName As String)
            Dim collation = dmArchiving.DataBase.ExecuteQueryGet("SELECT CONVERT (varchar, DATABASEPROPERTYEX('" & dmArchiving.DataBase.Connection.Database & "','collation')) as Colation")

            'Agrega un autonumerico para validacion de las filas
            data.Columns.Add("REGISTRO")
            For i As Integer = 0 To data.Rows.Count - 1 Step 1
                data.Rows(i)("REGISTRO") = i + 1
            Next

            'Crea una tabla temporal en la base de datos
            Dim query As String = "CREATE TABLE " & tableName & " ("
            For i As Integer = 0 To data.Columns.Count - 2 Step 1
                query &= data.Columns(i).ColumnName.ToString() & " VARCHAR(200) COLLATE " & collation.Rows(0)(0).ToString & ","
            Next
            query &= "REGISTRO VARCHAR(10) COLLATE " & collation.Rows(0)(0).ToString & ");"
            dmArchiving.DataBase.ExecuteNonQuery(query)

            'Inserta los datos en la tabla creada
            Dim bulkCopy As New SqlClient.SqlBulkCopy(CType(dmArchiving.DataBase.Connection, SqlClient.SqlConnection))
            bulkCopy.DestinationTableName = tableName
            bulkCopy.WriteToServer(data)
        End Sub

        Public Shared Sub InsertDataTable(ByVal data As DataTable, ByVal dmCore As DBCoreDataBaseManager, ByVal tableName As String)
            Dim collation = dmCore.DataBase.ExecuteQueryGet("SELECT CONVERT (varchar, DATABASEPROPERTYEX('" & dmCore.DataBase.Connection.Database & "','collation')) as Colation")

            'Agrega un autonumerico para validacion de las filas
            data.Columns.Add("REGISTRO")
            For i As Integer = 0 To data.Rows.Count - 1 Step 1
                data.Rows(i)("REGISTRO") = i + 1
            Next

            'Crea una tabla temporal en la base de datos
            Dim query As String = "CREATE TABLE " & tableName & " ("
            For i As Integer = 1 To data.Columns.Count - 1 Step 1
                query &= "Column" & i.ToString() & " VARCHAR(200) COLLATE " & collation.Rows(0)(0).ToString & ","
            Next
            query &= "REGISTRO VARCHAR(10) COLLATE " & collation.Rows(0)(0).ToString & ");"
            dmCore.DataBase.ExecuteNonQuery(query)

            'Inserta los datos en la tabla creada
            Dim bulkCopy As New SqlClient.SqlBulkCopy(CType(dmCore.DataBase.Connection, SqlClient.SqlConnection))
            bulkCopy.DestinationTableName = tableName
            bulkCopy.WriteToServer(data)
        End Sub

        Public Shared Sub InsertDataTableReport(ByVal data As DataTable, ByVal dmArchiving As DBArchivingDataBaseManager, ByVal tableName As String)
            Dim collation = dmArchiving.DataBase.ExecuteQueryGet("SELECT CONVERT (varchar, DATABASEPROPERTYEX('" & dmArchiving.DataBase.Connection.Database & "','collation')) as Colation")

            'Agrega un autonumerico para validacion de las filas
            data.Columns.Add("REGISTRO")
            For i As Integer = 0 To data.Rows.Count - 1 Step 1
                data.Rows(i)("REGISTRO") = i + 1
            Next

            'Crea una tabla temporal en la base de datos
            Dim query As String = "CREATE TABLE " & tableName & " ("
            For i As Integer = 1 To data.Columns.Count - 1 Step 1
                If data.Columns(i - 1).ColumnName.Contains("[") Then
                    query &= data.Columns(i - 1).ColumnName & " VARCHAR(200) COLLATE " & collation.Rows(0)(0).ToString & ","
                Else
                    query &= "[" & data.Columns(i - 1).ColumnName & "] VARCHAR(200) COLLATE " & collation.Rows(0)(0).ToString & ","
                End If
            Next
            query &= "REGISTRO VARCHAR(10) COLLATE " & collation.Rows(0)(0).ToString & ");"
            dmArchiving.DataBase.ExecuteNonQuery(query)

            'Inserta los datos en la tabla creada
            Dim bulkCopy As New SqlClient.SqlBulkCopy(CType(dmArchiving.DataBase.Connection, SqlClient.SqlConnection))
            bulkCopy.DestinationTableName = tableName
            bulkCopy.WriteToServer(data)
        End Sub

        Public Shared Sub InsertDataTableReport(ByVal data As DataTable, ByVal dbm As DataBaseManager, ByVal tableName As String)
            Dim collation = dbm.DataBase.ExecuteQueryGet("SELECT CONVERT (varchar, DATABASEPROPERTYEX('" & dbm.DataBase.Connection.Database & "','collation')) as Colation")

            'Agrega un autonumerico para validacion de las filas
            data.Columns.Add("REGISTRO")
            For i As Integer = 0 To data.Rows.Count - 1 Step 1
                data.Rows(i)("REGISTRO") = i + 1
            Next

            'Crea una tabla temporal en la base de datos
            Dim query As String = "CREATE TABLE " & tableName & " ("
            For i As Integer = 1 To data.Columns.Count - 1 Step 1
                If data.Columns(i - 1).ColumnName.Contains("[") Then
                    query &= data.Columns(i - 1).ColumnName & " VARCHAR(500) COLLATE " & collation.Rows(0)(0).ToString & ","
                Else
                    query &= "[" & data.Columns(i - 1).ColumnName & "] VARCHAR(500) COLLATE " & collation.Rows(0)(0).ToString & ","
                End If
            Next
            query &= "REGISTRO VARCHAR(10) COLLATE " & collation.Rows(0)(0).ToString & ");"
            dbm.DataBase.ExecuteNonQuery(query)

            'Inserta los datos en la tabla creada
            Dim bulkCopy As New SqlClient.SqlBulkCopy(CType(dbm.DataBase.Connection, SqlClient.SqlConnection))
            bulkCopy.DestinationTableName = tableName
            bulkCopy.WriteToServer(data)

        End Sub

        Public Shared Sub InsertDataTableReport(ByVal data As DataTable, ByVal dmIntegration As DBIntegrationDataBaseManager, ByVal tableName As String)
            Dim collation = dmIntegration.DataBase.ExecuteQueryGet("SELECT CONVERT (varchar, DATABASEPROPERTYEX('" & dmIntegration.DataBase.Connection.Database & "','collation')) as Colation")

            'Agrega un autonumerico para validacion de las filas
            data.Columns.Add("REGISTRO")
            For i As Integer = 0 To data.Rows.Count - 1 Step 1
                data.Rows(i)("REGISTRO") = i + 1
            Next

            'Crea una tabla temporal en la base de datos
            Dim query As String = "CREATE TABLE " & tableName & " ("
            For i As Integer = 1 To data.Columns.Count - 1 Step 1
                If data.Columns(i - 1).ColumnName.Contains("[") Then
                    query &= data.Columns(i - 1).ColumnName & " VARCHAR(200) COLLATE " & collation.Rows(0)(0).ToString & ","
                Else
                    query &= "[" & data.Columns(i - 1).ColumnName & "] VARCHAR(200) COLLATE " & collation.Rows(0)(0).ToString & ","
                End If
            Next
            query &= "REGISTRO VARCHAR(10) COLLATE " & collation.Rows(0)(0).ToString & ");"
            dmIntegration.DataBase.ExecuteNonQuery(query)

            'Inserta los datos en la tabla creada
            Dim bulkCopy As New SqlClient.SqlBulkCopy(CType(dmIntegration.DataBase.Connection, SqlClient.SqlConnection))
            bulkCopy.DestinationTableName = tableName
            bulkCopy.WriteToServer(data)

        End Sub

        Public Shared Sub DropTempTable(ByVal dmIntegration As DBIntegrationDataBaseManager, ByVal tableName As String)
            If tableName.StartsWith("#") Then
                dmIntegration.DataBase.ExecuteNonQuery("DROP TABLE " + tableName)
            End If
        End Sub

        Public Shared Sub InsertDataTable(ByVal data As DataTable, ByVal dbmImaging As DBImagingDataBaseManager, ByVal tableName As String)
            Dim collation = dbmImaging.DataBase.ExecuteQueryGet("SELECT CONVERT (varchar, DATABASEPROPERTYEX('" & dbmImaging.DataBase.Connection.Database & "','collation')) as Colation")

            'Agrega un autonumerico para validacion de las filas
            data.Columns.Add("REGISTRO")
            For i As Integer = 0 To data.Rows.Count - 1 Step 1
                data.Rows(i)("REGISTRO") = i + 1
            Next

            'Crea una tabla temporal en la base de datos
            Dim query As String = "CREATE TABLE " & tableName & " ("
            For i As Integer = 1 To data.Columns.Count - 1 Step 1
                query &= "Column" & i.ToString() & " VARCHAR(200) COLLATE " & collation.Rows(0)(0).ToString & ","
            Next
            query &= "REGISTRO VARCHAR(10) COLLATE " & collation.Rows(0)(0).ToString & ");"
            dbmImaging.DataBase.ExecuteNonQuery(query)

            'Inserta los datos en la tabla creada
            Dim bulkCopy As New SqlClient.SqlBulkCopy(CType(dbmImaging.DataBase.Connection, SqlClient.SqlConnection))
            bulkCopy.DestinationTableName = tableName
            bulkCopy.WriteToServer(data)
        End Sub

        Public Shared Sub InsertDataTableTemp(ByVal data As DataTable, ByVal dbmImaging As DBImagingDataBaseManager, ByVal tableName As String)
            Dim collation = dbmImaging.DataBase.ExecuteQueryGet("SELECT CONVERT (varchar, DATABASEPROPERTYEX('" & dbmImaging.DataBase.Connection.Database & "','collation')) as Colation")

            'Agrega un autonumerico para validacion de las filas
            data.Columns.Add("REGISTRO")
            For i As Integer = 0 To data.Rows.Count - 1 Step 1
                data.Rows(i)("REGISTRO") = i + 1
            Next

            'Crea una tabla temporal en la base de datos
            Dim query As String = "CREATE TABLE " & tableName & " ("
            For i As Integer = 1 To data.Columns.Count - 1 Step 1
                If data.Columns(i - 1).ColumnName.Contains("[") Then
                    query &= data.Columns(i - 1).ColumnName & " VARCHAR(300) COLLATE " & collation.Rows(0)(0).ToString & ","
                Else
                    query &= "[" & data.Columns(i - 1).ColumnName & "] VARCHAR(300) COLLATE " & collation.Rows(0)(0).ToString & ","
                End If
            Next
            query &= "REGISTRO VARCHAR(10) COLLATE " & collation.Rows(0)(0).ToString & ");"
            dbmImaging.DataBase.ExecuteNonQuery(query)

            'Inserta los datos en la tabla creada
            Dim bulkCopy As New SqlClient.SqlBulkCopy(CType(dbmImaging.DataBase.Connection, SqlClient.SqlConnection))
            bulkCopy.DestinationTableName = tableName
            bulkCopy.WriteToServer(data)
        End Sub

        Public Shared Sub InsertDataTableCore(ByVal data As DataTable, ByVal dmCore As DBCoreDataBaseManager, ByVal tableName As String)
            Dim collation = dmCore.DataBase.ExecuteQueryGet("SELECT CONVERT (varchar, DATABASEPROPERTYEX('" & dmCore.DataBase.Connection.Database & "','collation')) as Colation")

            'Agrega un autonumerico para validacion de las filas
            data.Columns.Add("REGISTRO")
            For i As Integer = 0 To data.Rows.Count - 1 Step 1
                data.Rows(i)("REGISTRO") = i + 1
            Next

            'Crea una tabla temporal en la base de datos
            Dim query As String = "CREATE TABLE " & tableName & " ("
            For i As Integer = 1 To data.Columns.Count - 1 Step 1
                query &= "Column" & i.ToString() & " VARCHAR(200) COLLATE " & collation.Rows(0)(0).ToString & ","
            Next
            query &= "REGISTRO VARCHAR(10) COLLATE " & collation.Rows(0)(0).ToString & ");"
            dmCore.DataBase.ExecuteNonQuery(query)

            'Inserta los datos en la tabla creada
            Dim bulkCopy As New SqlClient.SqlBulkCopy(CType(dmCore.DataBase.Connection, SqlClient.SqlConnection))
            bulkCopy.DestinationTableName = tableName
            bulkCopy.WriteToServer(data)
        End Sub
    End Class

End Namespace