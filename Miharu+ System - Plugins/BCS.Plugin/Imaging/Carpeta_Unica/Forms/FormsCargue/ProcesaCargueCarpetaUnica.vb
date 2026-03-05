Imports System.ComponentModel
Imports DBArchiving
Imports DBIntegration
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Imaging.Carpeta_Unica.Forms.FormsCargue

    Public Class ProcesaCargueCarpetaUnica

#Region " Declaraciones "

        'Private Const CAMPOS_ESTANDAR As Integer = 4 'Campos Esquema, Tipología, Clase, CBarras

#End Region

#Region " Estructuras "

        Public Structure LlavesTipo
            Dim Llaves As List(Of String)
            Dim Tipo As Short
        End Structure

#End Region

#Region " Funciones "

        Public Function ProcesaLogCajaSocial(ByRef data As DataTable, ByVal fileName As String, ByRef CargueBackgroundWorker As BackgroundWorker, _
                                             ByVal _Plugin As CarpetaUnicaPlugin, ByRef IdCargue As Integer, Data_Log As DBIntegration.SchemaLoad.TBL_Log_DataType, _
                                             ByVal Valida_Duplicados As Boolean, NombreProceso As String, FechaProcesoEjecucion As String) As DesktopConfig.TypeResult
            Dim dbmIntegration = New DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
            Dim retorno As DesktopConfig.TypeResult = Nothing
            CargueBackgroundWorker.ReportProgress(0)
            Dim Entidad As Integer = _Plugin.Manager.ImagingGlobal.Entidad
            Dim Proyecto As Integer = _Plugin.Manager.ImagingGlobal.Proyecto
            Dim usuario As Integer = _Plugin.Manager.Sesion.Usuario.id
            Dim LtErrores As List(Of String) = New List(Of String)
            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                Dim obj As New Object
                Dim TablaErrores As New DataTable

                SyncLock (Me)
                    Const tabla As String = "#VALIDA_CARGUE_PARCIAL"
                    BulkInsert.InsertDataTableReport(data, dbmIntegration, tabla)
                    Dim Id_Campo_Log_Data As Integer = 0
                    Dim StrError As String = ""

                    If (NombreProceso = "DOE") Then
                        StrError = "Este documento tiene Numeros de Transaccion repetidos, porfavor validar!!"
                        Id_Campo_Log_Data = 19
                    ElseIf (NombreProceso = "PASIVO" Or NombreProceso = "MICROFINANZAS") Then
                        StrError = "Este documento tiene Numeros de Productos repetidos, porfavor validar!!"
                        Id_Campo_Log_Data = 1
                    End If


                    If (Valida_Duplicados) Then
                        Dim validaRepetidos = dbmIntegration.SchemaBCSCarpetaUnica.PA_Validacion_Interna_CargueLog.DBExecute(Data_Log.fk_Tipo_Log, Data_Log.Fecha_Proceso, Data_Log.fk_Tipo_Proceso, tabla, Id_Campo_Log_Data)
                        If (validaRepetidos.Rows.Count > 0) Then
                            retorno.Result = False
                            LtErrores.Add(StrError)
                            retorno.Parameters = LtErrores
                        End If
                    Else
                        TablaErrores = dbmIntegration.SchemaBCSCarpetaUnica.PA_Procesa_SeleccionaLog.DBExecute(Data_Log.fk_Tipo_Log, Entidad, Proyecto, tabla, usuario, _
                                                                                                               fileName, Data_Log.Fecha_Proceso, FechaProcesoEjecucion, Data_Log.fk_Tipo_Proceso, _
                                                                                                               NombreProceso)

                        If (TablaErrores IsNot Nothing) Then
                            If (TablaErrores.Rows.Count > 0) Then
                                dbmIntegration.Transaction_Rollback()
                                retorno.Result = False
                                LtErrores.Add(TablaErrores.Rows(0)("ErrorMessage"))
                                retorno.Parameters = LtErrores
                            Else
                                retorno.Result = True
                            End If
                        End If

                        Dim UltimoCargueInvalido = dbmIntegration.SchemaConfig.TBL_Cargue.DBFindByfk_Entidadfk_Proyectofk_Tipo_LogArchivo_CargueValidoFecha_Proceso(Entidad, Proyecto, Data_Log.fk_Tipo_Log, fileName, False, Data_Log.Fecha_Proceso)
                        If (UltimoCargueInvalido.Rows.Count > 0) Then
                            IdCargue = CInt(UltimoCargueInvalido.Rows(0)("id_Cargue"))
                        End If
                    End If
                End SyncLock


            Catch ex As Exception
                retorno.Result = False
                LtErrores.Add(ex.Message)
                retorno.Parameters = LtErrores
            Finally
                dbmIntegration.Connection_Close()
            End Try

            Return retorno
        End Function

        Public Function ProcesaUniversal(ByRef data As DataTable, ByVal fileName As String, ByRef CargueBackgroundWorker As BackgroundWorker) As DesktopConfig.TypeResult
            Dim dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Dim trRetrun As New DesktopConfig.TypeResult
            trRetrun.Result = True

            Dim nIdEsquema As Integer
            Dim nCountUniversalFolderLlave As Integer 'Inicia en 1, para saltar el esquema.
            Dim countReg As Integer = 0
            Dim resultValidacion As DesktopConfig.TypeResult

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                resultValidacion = ValidaUniversal(data, CargueBackgroundWorker)

                If resultValidacion.Result Then
                    CargueBackgroundWorker.ReportProgress(0)

                    'Crea el cargue
                    dbmArchiving.Transaction_Begin()
                    Dim idCargue = dbmArchiving.SchemaRisk.TBL_Cargue.DBNextId()
                    Dim Cargue = New DBArchiving.SchemaRisk.TBL_CargueType

                    Cargue.id_Cargue = idCargue
                    Cargue.fk_Entidad = Program.RiskGlobal.Entidad
                    Cargue.fk_Proyecto = CShort(Program.RiskGlobal.Proyecto)
                    Cargue.fk_Cargue_Tipo = DesktopConfig.TipoCargue.Universal
                    Cargue.Archivo_Cargue = fileName
                    Cargue.Fecha_Cargue = CDate(Now.ToString("yyyy-MM-dd hh:mm:ss"))
                    Cargue.fk_Usuario_Log = Program.Sesion.Usuario.id

                    dbmArchiving.SchemaRisk.TBL_Cargue.DBInsert(Cargue)


                    'Obtiene el id_Universal_Folder, y se maneja como variable local, para no calcularlo desde la BD
                    Dim idUniversal_Folder = dbmArchiving.SchemaRisk.TBL_Universal_Folder.DBNextId()
                    dbmArchiving.Transaction_Commit()

                    For Each row As DataRow In data.Rows
                        dbmArchiving.Transaction_Begin()

                        countReg += 1
                        nIdEsquema = CInt(row(0))

                        'Crea el universal folder
                        dbmArchiving.SchemaRisk.TBL_Universal_Folder.DBInsert(idUniversal_Folder, idCargue, DesktopConfig.FolderEstado.Pendiente, CShort(nIdEsquema), Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)

                        'Crea los universal folder llave
                        nCountUniversalFolderLlave = 1
                        For Each Llave In Program.RiskGlobal.LLavesProyecto
                            Select Case Llave.Tipo
                                Case DesktopConfig.CampoTipo.Texto
                                    Dim value As Object = row(nCountUniversalFolderLlave).ToString()
                                    dbmArchiving.SchemaRisk.TBL_Universal_Folder_Llave.DBInsert(idUniversal_Folder, Llave.Id, Llave.Tipo, value, Llave.Id)

                                Case DesktopConfig.CampoTipo.Numerico
                                    Dim value As Object = CLng(row(nCountUniversalFolderLlave))
                                    dbmArchiving.SchemaRisk.TBL_Universal_Folder_Llave.DBInsert(idUniversal_Folder, Llave.Id, Llave.Tipo, value, Llave.Id)

                                Case DesktopConfig.CampoTipo.Fecha
                                    'Se valida si la fecha viene con Hora
                                    Dim myDate As Date
                                    Dim arrDate = row(nCountUniversalFolderLlave).ToString().Split(CChar(" "))

                                    If arrDate(0).IndexOf("/") > 0 OrElse arrDate(0).IndexOf("-") > 0 Then
                                        arrDate(0) = arrDate(0).Replace("/", "").Replace("-", "")
                                    End If

                                    If arrDate.Length > 1 Then 'Viene Con Hora
                                        myDate = New Date(CInt(arrDate(0).Substring(0, 4)), CInt(arrDate(0).Substring(4, 2)), CInt(arrDate(0).Substring(6, 2)), 0, 0, 0)
                                    Else 'Sin Hora
                                        myDate = New Date(CInt(arrDate(0).Substring(0, 4)), CInt(arrDate(0).Substring(4, 2)), CInt(arrDate(0).Substring(6, 2)))
                                    End If

                                    Dim value As Object = myDate
                                    dbmArchiving.SchemaRisk.TBL_Universal_Folder_Llave.DBInsert(idUniversal_Folder, Llave.Id, Llave.Tipo, value, Llave.Id)
                            End Select
                            nCountUniversalFolderLlave += 1
                        Next

                        idUniversal_Folder = CLng(idUniversal_Folder + 1)
                        CargueBackgroundWorker.ReportProgress(countReg)

                        dbmArchiving.Transaction_Commit()
                    Next
                Else
                    trRetrun.Result = False
                    trRetrun.Parameters = resultValidacion.Parameters
                    trRetrun.Resumen = resultValidacion.Resumen
                End If
            Catch
                dbmArchiving.Transaction_Rollback()
                Throw
            Finally
                dbmArchiving.Connection_Close()
            End Try

            Return trRetrun
        End Function

        Public Function ProcesaParcial(ByVal data As DataTable, ByVal fileName As String, ByVal idEntidad As Short, ByVal idEsquema As Short, ByRef CargueBackgroundWorker As BackgroundWorker) As DesktopConfig.TypeResult
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Dim trRetrun As New DesktopConfig.TypeResult

            Try
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Const tabla As String = "#VALIDA_CARGUE_PARCIAL"
                BulkInsert.InsertDataTable(data, dbmArchiving, tabla)

                '1.
                dbmArchiving.Transaction_Begin()
                'Valida e inserta los datos cargados
                Dim TablaErrores = dbmArchiving.Schemadbo.PA_Cargue_Parcial.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tabla, Program.Sesion.Usuario.id, fileName, idEsquema, idEntidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)

                'If Not CBool(TablaErrores.Rows(0)("Exitoso")) Then
                '    ProcesaError(data, TablaErrores, trRetrun)
                'Else
                '    Try
                '        trRetrun.OT = CInt(TablaErrores.Rows(0)("id_OT"))
                '        trRetrun.Result = True
                '        '2.
                '        dbmArchiving.Transaction_Commit()
                '    Catch ex As Exception
                '        '3.
                '        dbmArchiving.Transaction_Rollback()

                '        Dim ListMsgError As New List(Of String)
                '        Dim bRegistroValido As Boolean
                '        trRetrun.Resumen = New DesktopConfig.ValidacionCargue() With {.NoValido = 1}

                '        trRetrun.Parameters = ListMsgError
                '        ReportaError(ex.Message, ListMsgError, trRetrun, bRegistroValido)
                '        trRetrun.Result = False
                '    End Try

                'End If

            Catch
                '4.
                dbmArchiving.Transaction_Rollback()
                Throw
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
            End Try

            Return trRetrun
        End Function

        Public Function ProcesaActualizacion(ByRef data As DataTable, ByVal fileName As String, ByVal idEntidad As Short, ByVal idEsquema As Short, ByRef CargueBackgroundWorker As BackgroundWorker) As DesktopConfig.TypeResult
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim trRetrun As New DesktopConfig.TypeResult

            Try

                Const tabla As String = "#VALIDA_CARGUE_ACTUALIZACION"
                BulkInsert.InsertDataTable(data, dbmArchiving, tabla)

                'Valida los datos insertados
                Dim TablaErrores = dbmArchiving.Schemadbo.PA_Valida_Cargue_Actualizaciones.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tabla)
                ProcesaError(data, TablaErrores, trRetrun)

                If trRetrun.Result Then
                    Try
                        dbmArchiving.Schemadbo.PA_Carga_Cargue_Actualizaciones.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tabla)
                        trRetrun.Result = True

                    Catch ex As Exception
                        Dim ListMsgError As New List(Of String)
                        Dim bRegistroValido As Boolean
                        trRetrun.Resumen = New DesktopConfig.ValidacionCargue() With {.NoValido = 1}

                        trRetrun.Parameters = ListMsgError
                        ReportaError(ex.Message, ListMsgError, trRetrun, bRegistroValido)
                        trRetrun.Result = False
                    End Try

                End If

            Catch
                Throw
            Finally
                dbmArchiving.Connection_Close()
            End Try

            Return trRetrun
        End Function


        Private Function ValidaUniversal(ByVal data As DataTable, ByRef CargueBackgroundWorker As BackgroundWorker) As DesktopConfig.TypeResult
            Dim trRetrun As New DesktopConfig.TypeResult
            trRetrun.Result = True

            Dim nNumeroCamposData As Integer
            Dim ListmsgError As New List(Of String)
            Dim countReg As Integer = 0
            Dim countRegistrosNoValidos As Integer = 0
            Dim resultCargue As New DesktopConfig.ValidacionCargue

            nNumeroCamposData = data.Columns.Count

            'El número de campos recibidos, debe ser igual al número de llaves del proyecto más 1 (Esquema)
            If Not (nNumeroCamposData = Program.RiskGlobal.LLavesProyecto.Count + 1) Then
                trRetrun.Result = False
                ListmsgError.Add("- El número de campos recibidos, debe ser igual al número de llaves del proyecto mas uno (Esquema). Valide los datos y que el carácter de separación sea el adecuado.")
                trRetrun.Parameters = ListmsgError
            End If

            For Each row As DataRow In data.Rows
                Dim nIdEsquema = -1
                countReg += 1
                If IsNumeric(row(0)) Then
                    nIdEsquema = CInt(row(0))
                End If

                Dim nCountUniversalFolderLlave = 1
                Dim bRegistroValido As Boolean = True

                If nIdEsquema <> -1 Then
                    For Each Llave In Program.RiskGlobal.LLavesProyecto
                        Dim valorCampo = row(nCountUniversalFolderLlave)
                        Dim bTipoDatoValido As Boolean = False

                        Select Case Llave.Tipo
                            Case DesktopConfig.CampoTipo.Texto
                                bTipoDatoValido = (valorCampo.ToString() <> "")
                            Case DesktopConfig.CampoTipo.Numerico
                                bTipoDatoValido = IsNumeric(valorCampo)
                            Case DesktopConfig.CampoTipo.Fecha
                                Dim arrDate = valorCampo.ToString().Split(CChar(" "))
                                bTipoDatoValido = IsDate(arrDate(0))
                        End Select

                        If Not bTipoDatoValido Then
                            ListmsgError.Add("Línea " + countReg.ToString() + ": Tipo de dato de campos de data no coincidente")
                            trRetrun.Result = False
                            bRegistroValido = False
                        End If
                        nCountUniversalFolderLlave += 1
                    Next
                    CargueBackgroundWorker.ReportProgress(countReg)
                Else
                    ListmsgError.Add("Línea " + countReg.ToString() + ": Esquema no válido, ó registro vacío.")
                    trRetrun.Result = False
                    bRegistroValido = False
                End If

                If Not bRegistroValido Then
                    countRegistrosNoValidos += 1
                End If
            Next
            resultCargue.NoValido = countRegistrosNoValidos
            resultCargue.Valido = data.Rows.Count - countRegistrosNoValidos

            trRetrun.Parameters = ListmsgError
            trRetrun.Resumen = resultCargue

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

#Region " Metodos "

#End Region

    End Class

End Namespace
