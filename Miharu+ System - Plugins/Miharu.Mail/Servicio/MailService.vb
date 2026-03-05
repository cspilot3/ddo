Imports System
Imports System.IO
Imports System.Threading
Imports DBCore.SchemaProcess
Imports Miharu.Security.Library.WebService

Namespace Servicio

    Public Class MailService

#Region " Declaraciones "

        Private Detener As Boolean
        Private Const fk_Entidad_Cliente As Short = 2 'Coomeva
        Private Const fk_Proyecto As Short = 2
        Private Const fk_Esquema As Short = 1

        Private id_Calendario As Short = 0

#End Region

#Region " Metodos reemplazados "

        Protected Overrides Sub OnStart(ByVal args() As String)
            IniciarServicio()
        End Sub

        Protected Overrides Sub OnStop()
            DetenerServicio()
        End Sub

#End Region

#Region " Metodos "

        Private Sub LoadConfig()
            ' Leer la configuración
            If (File.Exists(Program.AppDataPath + MailConfig.ConfigFileName)) Then
                Program.Config = MailConfig.Deserialize(Program.AppDataPath)
            End If
        End Sub

        Public Sub IniciarServicio()

            JWriteLog("Funcion Iniciar Servicio Version 1.1", EventLogEntryType.Information)

            Try
                Dim WebService As SecurityWebService

                LoadConfig()

                WebService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")
                JWriteLog(Program.Config.SecurityWebServiceURL, EventLogEntryType.Information)
                'WebService = New Miharu.Security.Library.SecurityWebService("http://localhost:51500/SecurityService.asmx", "")

#If Not Debug Then
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
                WebService.setUser(Program.Config.User, MailConfig.Decrypt(Program.Config.Password))
                Program.ConnectionStrings = MailConfig.getCadenasConexion(WebService)

                If Program.ConnectionStrings.Core = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Core")
                    Me.Stop()

                    Return
                End If

                If Program.ConnectionStrings.Security = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Security")
                    Me.Stop()

                    Return
                End If

                Dim NewThread As New Thread(AddressOf Proceso)

                Detener = False
                NewThread.Start()

            Catch ex As Exception
                JWriteLog(ex.Message & " " & ex.StackTrace, EventLogEntryType.Error)
                Me.Stop()
            End Try
        End Sub

        Public Sub DetenerServicio()
            Detener = True
        End Sub

        Private Sub EnviarCorreo()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing
            Dim TablaPrestamos, Email_Solicitante, Email_Destinatario, MailTo, MailCC As String

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)
                dbmArchiving = New DBArchiving.DBArchivingDataBaseManager(Program.ConnectionStrings.Archiving)

                dbmCore.Connection_Open(2)
                dbmArchiving.Connection_Open(2)

                Dim PrestamosDT = dbmArchiving.Schemadbo.PA_Solicitudes_Prestamo_Fuera_De_Plazo.DBExecute(fk_Entidad_Cliente, fk_Proyecto, fk_Esquema)
                If PrestamosDT.Rows.Count > 0 Then

                    For drowPrestamos As Integer = 0 To PrestamosDT.Rows.Count - 1

                        TablaPrestamos = PrestamosDT.Rows(drowPrestamos).Item("Tabla").ToString
                        Email_Solicitante = PrestamosDT.Rows(drowPrestamos).Item("EmailSolicitante").ToString
                        Email_Destinatario = PrestamosDT.Rows(drowPrestamos).Item("EmailDestinatario").ToString

                        If Email_Destinatario <> "" Then
                            MailTo = Email_Destinatario
                            MailCC = Email_Solicitante
                        Else
                            MailTo = Email_Solicitante
                            MailCC = Nothing
                        End If

                        '------------------Validacion para enviar correos--------------------
                        Dim NotificacionDataTable = dbmCore.SchemaConfig.TBL_Notificacion.DBFindByNombre_Notificacion("Gestion de Prestamos")
                        If NotificacionDataTable.Count = 1 Then
                            Dim NotificacionListasDataTable = dbmCore.SchemaConfig.TBL_Notificacion_Lista.DBFindByfk_NotificacionNombre_Lista(NotificacionDataTable(0).id_Notificacion, "Gestion Prestamos - Coomeva")
                            If NotificacionListasDataTable.Count = 1 Then
                                Dim CorreoDatatable = dbmCore.SchemaConfig.PA_Busqueda_Parametros_Correo.DBExecute(NotificacionDataTable(0).id_Notificacion, NotificacionListasDataTable(0).id_Notificacion_Lista)
                                If CorreoDatatable.Count = 1 Then
                                    Dim Mensaje As String = ""
                                    Dim CuerpoCorreo As String = ""
                                    Dim Novedades As String = ""
                                    CuerpoCorreo = CorreoDatatable(0).CUERPO.Replace("@Tabla", TablaPrestamos)

                                    Mensaje = CorreoDatatable(0).SALUDO & CuerpoCorreo & CorreoDatatable(0).FIRMA

                                    If MailCC Is Nothing Or MailCC = "" Then
                                        MailCC = CorreoDatatable(0).CORREOS
                                    Else
                                        MailCC = MailCC & ";" & CorreoDatatable(0).CORREOS
                                    End If


                                    SendMail(MailTo, MailCC, "", CorreoDatatable(0).ASUNTO, Mensaje, "", Nothing)
                                End If
                            End If
                        End If
                    Next drowPrestamos

                End If

            Catch ex As Exception
                WriteErrorLog("Error Generacion de correo: " & ex.Message)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

        End Sub

        Private Sub SendMail(ByVal MailTo As String, ByVal MailCC As String, ByVal MailCCO As String, ByVal Subject As String, ByVal Message As String, nAttachName As String, nAttach As Byte())
            Dim DBMTools As DBTools.DBToolsDataBaseManager = Nothing

            Try
                DBMTools = New DBTools.DBToolsDataBaseManager(Program.ConnectionStrings.Tools)
                DBMTools.Connection_Open()

                DBMTools.InsertMail(fk_Entidad_Cliente, 2, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)

            Catch ex As Exception
                WriteErrorLog("Error Generacion de correo: " & ex.Message)
            Finally
                DBMTools.Connection_Close()
            End Try
        End Sub

        Private Sub Proceso()

            Dim dbmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

            Try
                dbmArchiving = New DBArchiving.DBArchivingDataBaseManager(Program.ConnectionStrings.Archiving)
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)

                While Not Detener
                    If Detener Then Return
                    Try
                        dbmSecurity.Connection_Open(2)
                        Dim CalendarioDataTable = dbmSecurity.SchemaConfig.TBL_Calendario.DBFindByfk_EntidadNombre_Calendario(fk_Entidad_Cliente, "Gestion Prestamos")

                        If CalendarioDataTable.Count > 0 Then
                            id_Calendario = CalendarioDataTable(0).id_Calendario
                        Else
                            WriteErrorLog("No hay calendario programado para el servicio")
                        End If

                        Dim habil = dbmSecurity.SchemaConfig.PA_Es_Hora_Habil.DBExecute(fk_Entidad_Cliente, id_Calendario)

                        If (habil) Then

                            EnviarCorreo()

                        End If

                        dbmSecurity.Connection_Close()
                    Catch
                        Throw
                    Finally
                        If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
                        If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                    End Try

                    If Detener Then Return

                    Thread.Sleep(Program.Config.Intervalo) ' Esperar n segundos antes de continuar

                End While


            Catch ex As Exception
                WriteErrorLog("Proceso ex: " & ex.Message)
                Throw

            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try


        End Sub

        Private Sub WriteErrorLog(ByVal nMessage As String)

            JWriteLog("WriteErrorLog Path: " & Program.AppDataPath & "log.txt", EventLogEntryType.Information)
            Try

                JWriteLog(nMessage, EventLogEntryType.Error)
                Dim sw As New StreamWriter(Program.AppDataPath & "log.txt", True)

                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sw.WriteLine("Mensaje: " & nMessage)
                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine("")

                sw.Flush()
                sw.Close()
            Catch ex As Exception
                'Try : EventLog.WriteEntry(ex.Message, EventLogEntryType.Error) : Catch : End Try
                Try : JWriteLog(ex.Message, EventLogEntryType.Error) : Catch : End Try
            End Try

            Windows.Forms.Application.DoEvents()
        End Sub

        'Private Sub WriteErrorLog(ByVal nEx As Exception)
        '    Try
        '        Dim sw As New StreamWriter(Program.AppDataPath & "log.txt", True)

        '        sw.WriteLine("--------------------------------------------------------------")
        '        sw.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss"))
        '        sw.WriteLine("Mensaje: " & nEx.Message)
        '        sw.WriteLine("--------------------------------------------------------------")
        '        sw.WriteLine("Traza:")
        '        sw.WriteLine(nEx.StackTrace)
        '        sw.WriteLine("--------------------------------------------------------------")
        '        sw.WriteLine("")

        '        sw.Flush()
        '        sw.Close()
        '    Catch ex As Exception
        '        Try : EventLog.WriteEntry(ex.Message, EventLogEntryType.Error) : Catch : End Try
        '    End Try

        '    System.Windows.Forms.Application.DoEvents()
        'End Sub

#End Region

#Region " Eventos "

        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.

        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Sub JWriteLog(ByVal mensaje As String, ByVal tipo As EventLogEntryType)

            If Not EventLog.SourceExists("MailService") Then
                EventLog.CreateEventSource("MailService", "Application")
            End If

            Dim eventLog1 As EventLog = New EventLog()
            eventLog1.Source = "MailService"
            eventLog1.WriteEntry(mensaje, tipo)

        End Sub

#End Region

    End Class

End Namespace