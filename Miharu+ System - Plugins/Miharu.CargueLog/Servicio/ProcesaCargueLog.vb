Imports System.ComponentModel
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports DBImaging

Namespace Servicio

    Public Class ProcesaCargueLog

#Region " Funciones "

        Public Function ProcesaCargue(ByVal data As DataTable, ByVal fileName As String, id_Tipo_Log As Integer, ByVal Valida_Duplicados As Boolean, Fecha_Proceso As Integer, ByVal nlog As DBImaging.SchemaConfig.TBL_Tipos_LogRow) As DesktopConfig.TypeResult
            Dim dbmImaging As DBImagingDataBaseManager = Nothing
            Dim trRetrun As New DesktopConfig.TypeResult

            Try
                dbmImaging = New DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(2)

                Const tabla As String = "#VALIDA_CARGUE"
                BulkInsert.InsertDataTableTemp(data, dbmImaging, tabla)
                '1
                dbmImaging.Transaction_Begin()
                ' Valida e inserta los datos cargados
                Dim TablaErrores = dbmImaging.SchemaProcess.PA_Procesa_Log_DINAMICO_2.DBExecute(id_Tipo_Log, nlog.fk_Entidad, nlog.fk_Proyecto, tabla, 2, fileName, Fecha_Proceso)

                If TablaErrores.Rows.Count > 0 Then
                    If CBool(TablaErrores.Rows(0)("Result")) Then
                        trRetrun.Result = CBool(TablaErrores.Rows(0)("Result"))
                        trRetrun.Cargue = CInt(TablaErrores.Rows(0)("fk_Cargue"))

                        dbmImaging.Transaction_Commit()
                    Else
                        ProcesaError(data, TablaErrores, trRetrun)

                        dbmImaging.Transaction_Rollback()

                        Dim ListMsgError As New List(Of String)
                        Dim bRegistroValido As Boolean
                        trRetrun.Resumen = New DesktopConfig.ValidacionCargue() With {.NoValido = 1}

                        trRetrun.Parameters = ListMsgError
                        ReportaError("", ListMsgError, trRetrun, bRegistroValido)
                        trRetrun.Result = False
                    End If
                    'Else
                    'Try
                    ''trRetrun.OT = CInt(TablaErrores.Rows(0)("id_OT"))
                    'trRetrun.Result = CBool(TablaErrores.Rows(0)("Result"))
                    'trRetrun.Cargue.Value = CInt(TablaErrores.Rows(0)("fk_Cargue"))
                    ''2.
                    'dbmImaging.Transaction_Commit()
                    'Catch ex As Exception
                    ''3.
                    'dbmImaging.Transaction_Rollback()

                    'Dim ListMsgError As New List(Of String)
                    'Dim bRegistroValido As Boolean
                    'trRetrun.Resumen = New DesktopConfig.ValidacionCargue() With {.NoValido = 1}

                    'trRetrun.Parameters = ListMsgError
                    'ReportaError(ex.Message, ListMsgError, trRetrun, bRegistroValido)
                    'trRetrun.Result = False
                    'End Try

                End If

            Catch
                '4
                dbmImaging.Transaction_Rollback()
                Throw
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return trRetrun
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

#End Region

    End Class

End Namespace
