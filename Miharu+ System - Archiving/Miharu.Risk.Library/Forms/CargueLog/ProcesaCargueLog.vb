Imports System.ComponentModel
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports DBArchiving

Namespace Forms.CargueLog

    Public Class ProcesaCargueLog

#Region " Funciones "

        Public Function ProcesaCargue(ByVal data As DataTable, ByVal fileName As String, id_Tipo_Log As Integer, ByVal Valida_Duplicados As Boolean, ByRef CargueBackgroundWorker As BackgroundWorker) As DesktopConfig.TypeResult
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Dim trRetrun As New DesktopConfig.TypeResult

            Try
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Const tabla As String = "#VALIDA_CARGUE"
                InsertDataTableTempArchiving(data, dbmArchiving, tabla)
                '1
                dbmArchiving.Transaction_Begin()
                ' Valida e inserta los datos cargados
                Dim TablaErrores = dbmArchiving.SchemaProcess.PA_Procesa_Log_DINAMICO.DBExecute(id_Tipo_Log, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tabla, Program.Sesion.Usuario.id, fileName)


                If CType(TablaErrores, DataTable).Rows.Count > 0 Then
                    ProcesaError(data, CType(TablaErrores, DataTable), trRetrun)
                Else
                    Try
                        trRetrun.Result = True
                        dbmArchiving.Transaction_Commit()
                    Catch ex As Exception
                        dbmArchiving.Transaction_Rollback()

                        Dim ListMsgError As New List(Of String)
                        Dim bRegistroValido As Boolean
                        trRetrun.Resumen = New DesktopConfig.ValidacionCargue() With {.NoValido = 1}

                        trRetrun.Parameters = ListMsgError
                        ReportaError(ex.Message, ListMsgError, trRetrun, bRegistroValido)
                        trRetrun.Result = False
                    End Try

                End If

            Catch
                dbmArchiving.Transaction_Rollback()
                Throw
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
            End Try

            Return trRetrun
        End Function

        Public Function CargarRegistrosEncontrados(ByVal Entidad As Integer, ByVal Proyecto As Integer, ByVal TipoLog As Integer, ByVal idLineaProceso As Integer, ByVal CentroProcesamientoRow As DBCore.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType, ByVal riskGlobal As RiskGlobal, ByVal estado As DBCore.EstadoEnum, ByVal idUsuario As Integer) As DataTable
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Dim trRetrun As New DesktopConfig.TypeResult
            Dim _dtRegistrosEncontrados As DataTable = Nothing

            Try
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                _dtRegistrosEncontrados = dbmArchiving.SchemaReport.PA_Reporte_Log_Deceval.DBExecute(Entidad, Proyecto, TipoLog, idLineaProceso, CentroProcesamientoRow.fk_Entidad, CentroProcesamientoRow.fk_Sede, CentroProcesamientoRow.id_Centro_Procesamiento, Program.Sesion.Usuario.id)


                For i As Integer = 0 To _dtRegistrosEncontrados.Rows.Count - 1
                    Dim row = _dtRegistrosEncontrados.Rows(i)
                    Dim idEstado As String = row("fk_Estado").ToString()

                    If idEstado = "1100" Then
                        Program.Sesion.Parameter("idEstado") = idEstado
                    End If
                Next
            Catch
                dbmArchiving.Transaction_Rollback()
                Throw
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
            End Try

            Return _dtRegistrosEncontrados
        End Function

        Public Function CargarRegistrosDuplicados(ByVal Entidad As Integer, ByVal Proyecto As Integer, ByVal TipoLog As Integer) As DataTable
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Dim trRetrun As New DesktopConfig.TypeResult
            Dim _dtRegistrosDuplicados As DataTable = Nothing

            Try
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmArchiving.Transaction_Begin()
                _dtRegistrosDuplicados = dbmArchiving.SchemaReport.PA_Reporte_Log_Deceval_Duplicados.DBExecute(Entidad, Proyecto, TipoLog)
            Catch
                dbmArchiving.Transaction_Rollback()
                Throw
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
            End Try

            Return _dtRegistrosDuplicados
        End Function

        Public Function CargarEstadoPagares(ByVal Entidad As Integer, ByVal Proyecto As Integer, ByVal TipoLog As Integer) As DataTable
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Dim trRetrun As New DesktopConfig.TypeResult
            Dim _dtEstadoPagares As DataTable = Nothing

            Try
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                _dtEstadoPagares = dbmArchiving.SchemaReport.PA_Reporte_Estado_Pagares.DBExecute(Entidad, Proyecto, TipoLog)
            Catch
                dbmArchiving.Transaction_Rollback()
                Throw
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
            End Try

            Return _dtEstadoPagares
        End Function


        Public Sub ReportaError(ByVal nError As String, ByRef ListMsgError As List(Of String), ByRef trRetrun As DesktopConfig.TypeResult, ByRef bRegistroValido As Boolean)
            ListMsgError.Add(nError)
            trRetrun.Result = False
            bRegistroValido = False
        End Sub

        Public Sub ProcesaError(ByVal data As DataTable, ByVal TablaErrores As DataTable, ByRef trRetrun As DesktopConfig.TypeResult)
            Dim bRegistroValido As Boolean
            trRetrun.Result = True
            Dim ListMsgError As New List(Of String)
            Dim resultCargue As New DesktopConfig.ValidacionCargue

            If TablaErrores.Rows.Count > 0 Then

                For Each DetalleError As DataRow In TablaErrores.Rows
                    ReportaError(DetalleError("Detalle").ToString, ListMsgError, trRetrun, bRegistroValido)
                Next

                resultCargue.AdicionConLlavesInexistentes = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 1"))
                resultCargue.ClaseRegistroNoValido = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 2"))
                resultCargue.DevolucionSinCodigoBarras = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 3"))
                resultCargue.EsquemaNoValido = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 4"))
                resultCargue.NuevoConLlavesInexistentes = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 5"))
                resultCargue.NumeroCamposDataNoCoincide = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 6"))
                resultCargue.NumeroLlavesNoCoincide = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 7"))
                resultCargue.TipoDatoCamposDataNoCoincide = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 8"))
                resultCargue.TipoDatoLlavesNoCoincide = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 9"))
                resultCargue.TipoDocumentoNoValido = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 10"))
                resultCargue.NoValido = TablaErrores.Rows.Count
                resultCargue.Valido = data.Rows.Count - TablaErrores.Rows.Count

                trRetrun.Resumen = resultCargue
                trRetrun.Parameters = ListMsgError
                trRetrun.Result = False
            End If

        End Sub

        Public Shared Sub InsertDataTableTempArchiving(ByVal data As DataTable, ByVal dbmImaging As DBArchivingDataBaseManager, ByVal tableName As String)
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
                    query &= data.Columns(i - 1).ColumnName & " VARCHAR(200) COLLATE " & collation.Rows(0)(0).ToString & ","
                Else
                    query &= "[" & data.Columns(i - 1).ColumnName & "] VARCHAR(200) COLLATE " & collation.Rows(0)(0).ToString & ","
                End If
            Next
            query &= "REGISTRO VARCHAR(10) COLLATE " & collation.Rows(0)(0).ToString & ");"
            dbmImaging.DataBase.ExecuteNonQuery(query)

            'Inserta los datos en la tabla creada
            Dim bulkCopy As New SqlClient.SqlBulkCopy(CType(dbmImaging.DataBase.Connection, SqlClient.SqlConnection))
            bulkCopy.DestinationTableName = tableName
            bulkCopy.WriteToServer(data)
        End Sub

#End Region

    End Class

End Namespace
