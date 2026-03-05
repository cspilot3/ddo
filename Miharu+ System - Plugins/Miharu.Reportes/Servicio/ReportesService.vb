Imports System.IO
Imports System.Net
Imports System.Threading
Imports System.Windows.Forms
Imports Miharu.Security.Library.WebService

Namespace Servicio
    Public Class ReportesService

        Protected Overrides Sub OnStart(ByVal args() As String)
            ' Agregue el código aquí para iniciar el servicio. Este método debería poner
            ' en movimiento los elementos para que el servicio pueda funcionar.
            IniciarServicio()
        End Sub

        Protected Overrides Sub OnStop()
            ' Agregue el código aquí para realizar cualquier anulación necesaria para detener el servicio.
            DetenerServicio()
        End Sub
#Region " Declaraciones "
        Private Detener As Boolean
        Private ThreadArchivos As Thread
#End Region

#Region " Metodos "

        Private Sub LoadConfig()
            ' Leer la configuración
            If (File.Exists(Program.AppDataPath + ReportesConfig.ConfigFileName)) Then
                Program.Config = ReportesConfig.Deserialize(Program.AppDataPath)
            End If
        End Sub

        Public Sub IniciarServicio()
            'JWriteLog("Funcion Iniciar Servicio Version 1.1", EventLogEntryType.Information)
            WriteErrorLog("Funcion Iniciar Servicio")

            Try
                Dim WebService As SecurityWebService

                LoadConfig()

                WebService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")
                'JWriteLog(Program.Config.SecurityWebServiceURL, EventLogEntryType.Information)
                WriteErrorLog(Program.Config.SecurityWebServiceURL)
                'WebService = New Miharu.Security.Library.SecurityWebService("http://localhost:51500/SecurityService.asmx", "")

#If Not DEBUG Then
            ' Validar que la versión corresponda
            Dim VersionApp As String = WebService.getAssemblyVersion(Program.AssemblyName)

            If Not VersionApp = Program.AssemblyVersion Then
                WriteErrorLog("La versión del aplicativo no corresponde a la registrada en la base de datos," & vbCrLf & vbCrLf & _
                                "Versión registrada: [" & VersionApp & "]" & vbCrLf & _
                                "Versión ejecutable: [" & Program.AssemblyVersion & "]")

                Me.Stop()

                Return
            End If
#End If

                WebService.CrearCanalSeguro()
                WebService.setUser(Program.Config.User, ReportesConfig.Decrypt(Program.Config.Password))
                Program.ConnectionStrings = ReportesConfig.getCadenasConexion(WebService)

                If Program.ConnectionStrings.Security = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Security")
                    Me.Stop()
                    Return
                End If

                If Program.ConnectionStrings.Core = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Core")
                    Me.Stop()
                    Return
                End If

                Dim NewThread As New Thread(AddressOf Proceso)

                Detener = False
                NewThread.Start()

            Catch ex As Exception
                WriteErrorLog("Error IniciarServicio ex: " & ex.Message & " " & ex.ToString())
                Me.Stop()
            End Try
        End Sub

        Public Sub DetenerServicio()
            Detener = True

            If ThreadArchivos IsNot Nothing Then
                Try
                    ThreadArchivos.Abort()
                Catch ex As Exception

                End Try
            End If
        End Sub

        Private Sub Proceso()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)

                While Not Detener
                    If Detener Then Return
                    Try
                        dbmSecurity.Connection_Open(1)
                        dbmCore.Connection_Open(1)

                        Dim CalendarioDataTable = dbmSecurity.SchemaConfig.TBL_Calendario.DBFindByfk_EntidadNombre_Calendario(1, "Reportes")

                        If CalendarioDataTable.Rows.Count > 0 Then
                            Dim habil = dbmSecurity.SchemaConfig.PA_Es_Hora_Habil.DBExecute(CalendarioDataTable(0).fk_Entidad, CalendarioDataTable(0).id_Calendario)

                            If (habil) Then
                                Dim ReportesConfigDT = dbmCore.SchemaConfig.CTA_Get_Reporte_Automatico.DBGet()
                                Dim ReportesPendientesDT = dbmCore.SchemaProcess.PA_Get_Reporte_Automatico_Pendiente.DBExecute()

                                If ReportesConfigDT.Rows.Count > 0 AndAlso ReportesPendientesDT.Rows.Count > 0 Then
                                    For Each Reporte As DBCore.SchemaConfig.CTA_Get_Reporte_AutomaticoRow In ReportesConfigDT
                                        Dim dtDatosReportePendiente As DataTable = ReportesPendientesDT.Clone()

                                        Dim pendientesFiltrados() As DataRow = ReportesPendientesDT.Select("fk_Reporte = " & Reporte.id_Reporte)

                                        For Each row As DataRow In pendientesFiltrados
                                            dtDatosReportePendiente.ImportRow(row)
                                        Next

                                        If dtDatosReportePendiente.Rows.Count > 0 Then
                                            Dim ArraListParameters As ArrayList = New ArrayList

                                            ArraListParameters.Add(Reporte)
                                            ArraListParameters.Add(dtDatosReportePendiente)

                                            ThreadArchivos = New Thread(AddressOf GenerarReporte)
                                            ThreadArchivos.Start(ArraListParameters)
                                        End If
                                    Next
                                Else
                                    WriteErrorLog("No hay reportes automáticos para ejecutar")
                                End If
                            Else
                                WriteErrorLog("No es una hora habil para ejecutar el proceso")
                            End If
                        Else
                            WriteErrorLog("No existe el calendario del servicio de reportes")
                        End If
                    Catch ex As Exception
                        WriteErrorLog("Error Proceso ex: " & ex.ToString())
                        'agregar actualización de campo enProceso
                    Finally
                        If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                        If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    End Try

                    If Detener Then Return

                    Thread.Sleep(Program.Config.Intervalo * 6000000) ' Esperar n segundos antes de continuar
                End While
            Catch ex As Exception
                WriteErrorLog("Error Proceso ex: " & ex.ToString())
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub GenerarReporte(ByVal objectArray As Object)
            Dim ArraListParameters As ArrayList = objectArray
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Dim reporte As DBCore.SchemaConfig.CTA_Get_Reporte_AutomaticoRow = ArraListParameters(0)
            Dim datosReporte As DataTable = ArraListParameters(1)
            Dim valido As Boolean = False

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)
                dbmCore.Connection_Open(1)

                Dim Notificacion = dbmCore.SchemaConfig.PA_Get_Notificacion.DBExecute(reporte.fk_Notificacion_Correo)

                If Notificacion.Rows.Count > 0 Then
                    Dim CorreoDatatable = dbmCore.SchemaConfig.PA_Busqueda_Parametros_Correo.DBExecute(Notificacion(0).id_Notificacion, Notificacion(0).id_Notificacion_Lista)
                    If CorreoDatatable.Rows.Count > 0 Then
                        Dim Message As String = ""
                        Dim MailTo As String = ""
                        Dim MailCC As String = ""
                        Dim MailCCO As String = ""
                        Dim Subject As String = ""
                        Dim nAttachName As String = ""
                        Dim nAttach As Byte() = Nothing
                        Dim CuerpoCorreo As String = ""
                        Dim Novedades As String = ""

                        Dim usa_encabezado As Boolean = reporte.usa_Encabezado
                        Dim caracterSeparador As String = If(String.IsNullOrEmpty(reporte.separador), ";", reporte.separador)
                        nAttachName = ValidacionesSplit(reporte.estructura_Nombre_Archivo.Split("|"c), reporte)

                        If Not String.IsNullOrWhiteSpace(nAttachName) Then
                            Message = CorreoDatatable(0).SALUDO & CuerpoCorreo & CorreoDatatable(0).FIRMA
                            MailTo = CorreoDatatable(0).CORREOS
                            Subject = Replace(CorreoDatatable(0).ASUNTO, "@Asunto", nAttachName)

                            If reporte.extension_Archivo = ".csv" Then
                                Using ms As New MemoryStream()
                                    Using sw As New StreamWriter(ms, New Text.UTF8Encoding(False))
                                        ' Escribir encabezado si aplica
                                        If usa_encabezado Then
                                            'Dim encabezado = String.Join(caracterSeparador, datosReporte.Columns.Cast(Of DataColumn).Select(Function(c) c.ColumnName))
                                            sw.WriteLine(reporte.columnas_Reporte)
                                        End If

                                        Dim cantidadColumnas As Integer = reporte.cant_Columnas_Reporte ' o el valor que corresponda

                                        Dim columnasCampo = datosReporte.Columns.Cast(Of DataColumn)().
                                    Where(Function(c)
                                              If c.ColumnName.StartsWith("Campo_", StringComparison.OrdinalIgnoreCase) Then
                                                  Dim partes = c.ColumnName.Split("_"c)
                                                  Dim numero As Integer

                                                  ' Validar que la parte numérica exista y sea número
                                                  If partes.Length = 2 AndAlso Integer.TryParse(partes(1), numero) Then
                                                      Return numero >= 1 AndAlso numero <= cantidadColumnas
                                                  End If
                                              End If

                                              Return False
                                          End Function).
                                    ToList()

                                        ' Escribir datos solo de esas columnas
                                        For Each fila As DataRow In datosReporte.Rows
                                            Dim valores = columnasCampo.Select(Function(c) fila(c).ToString())
                                            sw.WriteLine(String.Join(caracterSeparador, valores))
                                        Next

                                        sw.Flush()
                                        nAttach = ms.ToArray()
                                        nAttachName = nAttachName + reporte.extension_Archivo
                                    End Using
                                End Using
                            End If

                            Dim dbmTools As DBTools.DBToolsDataBaseManager = Nothing

                            Try
                                dbmTools = New DBTools.DBToolsDataBaseManager(Program.ConnectionStrings.Tools)
                                dbmTools.Connection_Open()

                                dbmTools.InsertMail(reporte.fk_Entidad, 2, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)

                                valido = True

                            Catch ex As Exception
                                WriteErrorLog("Error proceso envío notificación proceso: " & reporte.nombre_Reporte & ", notificación: " & Notificacion(0).Nombre_Notificacion & ", ex: " & ex.Message.ToString())
                            Finally
                                If (dbmTools IsNot Nothing) Then dbmTools.Connection_Close()
                            End Try
                        Else
                            'Actualizar a error, enProceso = 0
                            dbmCore.SchemaProcess.PA_Set_Reporte_Automatico_Pendiente.DBExecute(CInt(datosReporte.Rows(0)("id_Reportes_Pendientes").ToString()), 1)
                        End If
                    End If
                End If
            Catch ex As Exception
                WriteErrorLog("Error EnvioReporte ex: " & ex.ToString())
            Finally
                If valido Then
                    'Actualizar a ejecutado
                    dbmCore.SchemaProcess.PA_Set_Reporte_Automatico_Pendiente.DBExecute(CInt(datosReporte.Rows(0)("id_Reportes_Pendientes").ToString()), 2)
                Else
                    'Actualizar a error, enProceso = 0
                    dbmCore.SchemaProcess.PA_Set_Reporte_Automatico_Pendiente.DBExecute(CInt(datosReporte.Rows(0)("id_Reportes_Pendientes").ToString()), 1)
                End If
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Function ValidacionesSplit(ByVal StrValidacionColumn As String(), ByVal nItemFile As DBCore.SchemaConfig.CTA_Get_Reporte_AutomaticoRow) As String
            Dim nretorno As String = ""
            Try
                If (StrValidacionColumn.Count() > 0) Then
                    Dim retorno As String = ""
                    For index As Integer = 0 To StrValidacionColumn.Count() - 1
                        Dim AuxItemValidacion As String = Nothing
                        AuxItemValidacion = StrValidacionColumn(index).ToString()

                        If (AuxItemValidacion.Contains("<FECHAGENERACION>")) Then
                            Dim formato As String = AuxItemValidacion.Replace("<FECHAGENERACION>", "")

                            retorno = Format(Now(), formato)
                        ElseIf (AuxItemValidacion.Contains("<SIGLA>")) Then
                            retorno = nItemFile.Item("Sigla").ToString()
                        ElseIf (AuxItemValidacion.Contains("<MESREPORTE>")) Then
                            If nItemFile.Item("data_Generar").ToString() = "m-1" Then
                                retorno = DateTime.Now.AddMonths(-1).ToString("MMMM", New Globalization.CultureInfo("es-ES"))

                                retorno = Char.ToUpper(retorno(0)) & retorno.Substring(1)
                            End If
                        Else
                            retorno = AuxItemValidacion
                        End If

                        If index = 0 Then
                            nretorno = retorno
                        Else
                            nretorno = nretorno & retorno
                        End If
                    Next
                End If
            Catch ex As Exception
                nretorno = ""
            End Try
            Return nretorno
        End Function

        Private Sub WriteErrorLog(ByVal nMessage As String)
            Try
                Dim sw As New StreamWriter(Program.AppDataPath & "log_" & Now.ToString("yyyyMMdd") & ".txt", True)

                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sw.WriteLine("Mensaje: " & nMessage)
                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine("")

                sw.Flush()
                sw.Close()
            Catch ex As Exception
                Try : JWriteLog(ex.ToString(), EventLogEntryType.Error) : Catch : End Try
            End Try
            Windows.Forms.Application.DoEvents()
        End Sub

        Public Sub JWriteLog(ByVal mensaje As String, ByVal tipo As EventLogEntryType)
            If Not EventLog.SourceExists("MiharuReportesDDOService") Then
                EventLog.CreateEventSource("MiharuReportesDDOService", "Application")
            End If

            Dim eventLog1 As EventLog = New EventLog()
            eventLog1.Source = "MiharuReportesDDOService"
            eventLog1.WriteEntry(mensaje, tipo)
        End Sub
#End Region
    End Class
End Namespace