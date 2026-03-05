Imports System
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms
Imports Miharu.MailSender.Library
Imports Miharu.Security.Library.WebService
Imports DBTools

Namespace Servicio

    Public Class MailSenderService

#Region " Declaraciones "

        Private Detener As Boolean

        Private Const InvalidEmailMessage As String = "La dirección de correo electrónico no es válida o está vacía"
        Private Const MailServiceErrorMessage As String = "Ocurrió un error en el servicio MailSenderService de envío de correos"

#End Region

#Region " Propiedades "

        Private ReadOnly Property AppPath() As String
            Get
                Return Application.StartupPath + "\"
            End Get
        End Property

#End Region

#Region " Metodos Remplazados "

        Protected Overrides Sub OnStart(ByVal args() As String)
            IniciarServicio()
        End Sub
        Protected Overrides Sub OnStop()
            DetenerServicio()
        End Sub

#End Region

#Region " Metodos "

        Public Sub IniciarServicio()
            Try
                Dim WebService As SecurityWebService

                LoadConfig()

                WebService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")

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
                WebService.setUser(Program.Config.User, MailSenderConfig.Decrypt(Program.Config.Password))
                Program.ConnectionStrings = MailSenderConfig.getCadenasConexion(WebService)

                If Program.ConnectionStrings.Tools = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Tools")
                    Me.Stop()

                    Return
                End If

                If (Not Directory.Exists(Program.AppTempPath)) Then
                    Directory.CreateDirectory(Program.AppTempPath)                
                End If

                Dim NewThread As New Thread(AddressOf Proceso)

                Detener = False

                NewThread.Start()

            Catch ex As Exception
                WriteErrorLog(ex.Message)
                Me.Stop()
            End Try
        End Sub

        Public Sub DetenerServicio()
            Detener = True
        End Sub

        Private Sub Proceso()

            Dim ConnectionStringTools As String
            Dim ConnectionStringCore As String

            If Program.Config.UsaRemoting Then
                Dim DataRemoting = ";RemotingUrl=tcp://" & Program.Config.RemotingIPName & ":" & Program.Config.RemotingPuerto & "/" & Program.Config.RemotingAppName & ";RemotingPwd=" & Library.MailSenderConfig.Decrypt(Program.Config.RemotingPassword) & ";RemotingTrusted=" & Program.Config.RemotingCifrado

                ConnectionStringTools = Program.ConnectionStrings.Tools & DataRemoting
                ConnectionStringCore = Program.ConnectionStrings.Core & DataRemoting
            Else
                ConnectionStringTools = Program.ConnectionStrings.Tools
                ConnectionStringCore = Program.ConnectionStrings.Core
            End If

            Dim DBMMailSender As New DBToolsDataBaseManager(ConnectionStringTools)
            Dim DBMCore As New DBCore.DBCoreDataBaseManager(ConnectionStringCore)
            Dim MailManager As New MailSenderManager()

            Try
                MailManager.SMTPServerAddress = Program.Config.SMTPServerAddress
                MailManager.Port = Program.Config.SMTPServerPort
                MailManager.FromMailAddress = Program.Config.FromMailAddress
                MailManager.FromMailDisplay = Program.Config.FromMailDisplay
                MailManager.User = Program.Config.MailUser
                MailManager.Password = Program.Config.MailUserPassword
                MailManager.EnabledSSL = Program.Config.EnabledSSL

                While Not Detener

                    If Detener Then Return

                    DBMMailSender.Connection_Open()
                    DBMCore.Connection_Open(1)

                    Dim tblQueue = DBMMailSender.SchemaMail.PA_Basic_TBL_Queue_get.DBExecute()
                    If tblQueue.Count > 0 Then

                        Program.ConnectionParameterString = MailSenderConfig.GetModulosURLsSystem(DBMMailSender)
                        MailManager.URLAPIMasiv = Program.ConnectionParameterString._URLAPIMasic
                        MailManager.UserAPIMasiv = Program.ConnectionParameterString._UserAPIMasiv
                        MailManager.PasswordAPIMasiv = Program.ConnectionParameterString._PasswordAPIMasiv
                        MailManager.AditionalEmailAdress = Program.ConnectionParameterString._EmailSendAditional

                        For Each RowQueue As SchemaMail.TBL_QueueRow In tblQueue.Rows

                            If Detener Then Return

                            Try
                                ClearTempDirectory()

                                Dim adjuntos As New List(Of String)

                                If (RowQueue.AttachName_Queue <> "" And Not RowQueue.IsAttach_QueueNull()) Then
                                    Dim fileName = Program.AppTempPath & RowQueue.AttachName_Queue
                                    Using writer As New FileStream(fileName, FileMode.Create)
                                        writer.Write(RowQueue.Attach_Queue, 0, RowQueue.Attach_Queue.Length)
                                    End Using

                                    adjuntos.Add(fileName)
                                End If

                                ' Valida el correo del remitente
                                If String.IsNullOrWhiteSpace(RowQueue.EmailAddress_Queue) Then
                                    Throw New ApplicationException(InvalidEmailMessage)
                                End If

                                If RowQueue.EmailTracking Then
                                    MailManager.SendMailTracking(DBMMailSender, DBMCore, RowQueue, adjuntos.ToArray())
                                Else
                                    Dim EmailFrom = ""
                                    Dim EmailFromDisplay = ""

                                    If Not RowQueue.IsEmailFromNull() Then
                                        EmailFrom = RowQueue.EmailFrom
                                    End If

                                    If Not RowQueue.IsEmailFromDisplayNull() Then
                                        EmailFromDisplay = RowQueue.EmailFromDisplay
                                    End If

                                    MailManager.SendMail(EmailFrom, EmailFromDisplay, RowQueue.EmailAddress_Queue, RowQueue.CC_Queue, RowQueue.CCO_Queue, RowQueue.Subject_Queue, RowQueue.Message_Queue, adjuntos.ToArray())
                                End If

                                DBMMailSender.SchemaMail.PA_Basic_TBL_Queue_delete.DBExecute(RowQueue.id_Queue)

                            Catch ex As Exception

                                If RowQueue.EmailTracking Then
                                    Dim notificationMessage As String

                                    If ex.Message.Equals(InvalidEmailMessage) Then
                                        notificationMessage = InvalidEmailMessage
                                    Else
                                        notificationMessage = MailServiceErrorMessage
                                    End If

                                    UpdateTrackingMailWithError(DBMMailSender, RowQueue, notificationMessage)
                                End If

                                WriteErrorLog(ex.Message)
                                DBMMailSender.SchemaMail.PA_Basic_TBL_Queue_Error_insert.DBExecute(RowQueue.id_Queue, ex.Message)
                            End Try

                            Application.DoEvents()
                        Next
                    End If

                    DBMMailSender.Connection_Close() ' System
                    DBMCore.Connection_Close()

                    If Detener Then Return

                    Thread.Sleep(Program.Config.Intervalo) ' Esperar n segundos antes de continuar
                End While
            Catch ex As Exception
                WriteErrorLog(ex.Message)
            Finally
                Try
                    DBMMailSender.Connection_Close()
                    DBMCore.Connection_Close()
                Catch
                End Try
            End Try

            Me.Stop()
        End Sub

        Private Sub UpdateTrackingMailWithError(ByRef DBMMailSender As DBToolsDataBaseManager, RowQueue As SchemaMail.TBL_QueueRow, errorMessage As String)

            ' Traer el TBL_TRacking_Mail por id_Queue
            Dim dataTBLTrackingMailfkQueue = DBMMailSender.SchemaMail.TBL_Tracking_Mail.DBFindByfk_Queuefk_EntidadIsActive(RowQueue.id_Queue, RowQueue.fk_Entidad, True)
            If dataTBLTrackingMailfkQueue.Count > 0 Then
                Dim tblTrackingMailfkQueueRow = dataTBLTrackingMailfkQueue(0)

                Dim dataTBLTrackingMailType = New DBTools.SchemaMail.TBL_Tracking_MailType
                dataTBLTrackingMailType.fk_Estado_Correo = 3
                dataTBLTrackingMailType.Detalle_Envio = errorMessage

                DBMMailSender.SchemaMail.TBL_Tracking_Mail.DBUpdate(dataTBLTrackingMailType, tblTrackingMailfkQueueRow.id_Tracking_Mail)
            End If
        End Sub


        Public Sub WriteErrorLog(ByVal nMessage As String)
            Try
                Dim sw As New StreamWriter(Program.AppPath & "log.txt", True)

                sw.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sw.WriteLine("Mensaje: " & nMessage)
                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine("")

                sw.Flush()
                sw.Close()
            Catch ex As Exception
                Try : EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error) : Catch : End Try
            End Try

            Application.DoEvents()
        End Sub

        Private Sub LoadConfig()
            ' Leer la configuración
            If (File.Exists(Me.AppPath + Program.ConfigFileName)) Then
                MailSenderConfig.Deserialize(Program.Config, Me.AppPath + Program.ConfigFileName)
            End If
        End Sub

        Private Sub ClearTempDirectory()
            Dim fileNames = Directory.GetFiles(Program.AppTempPath)

            For Each fileName As String In fileNames
                Try
                    File.Delete(fileName)
                Catch
                End Try
            Next

        End Sub
#End Region

    End Class

End Namespace