Imports System
Imports System.IO
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Windows.Forms
Imports Miharu.FileSending.Library.Clases
Imports Miharu.Security.Library.WebService

Namespace Servicio

    Public Class FileSendingService

#Region " Declaraciones "

        Private FileSending As FileSendingServer
        Private CanalTCP As TcpChannel
        Public DataRemoting As String

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
            If (File.Exists(Program.AppDataPath + FileSendingConfig.ConfigFileName)) Then
                Program.Config = FileSendingConfig.Deserialize(Program.AppDataPath)
            End If
        End Sub

        Public Sub IniciarServicio()
            Try
                Dim WebService As SecurityWebService

                LoadConfig()

                WebService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")

#If Not Debug Then
                ' Validar que la versión corresponda
                Dim VersionApp As String = WebService.getAssemblyVersion(Program.AssemblyTitle)

                If Not VersionApp = Program.AssemblyVersion Then
                    WriteErrorLog("La versión del aplicativo no corresponde a la registrada en la base de datos," & vbCrLf & vbCrLf & _
                                  "Versión registrada: [" & VersionApp & "]" & vbCrLf & _
                                  "Versión ejecutable: [" & Program.AssemblyVersion & "]")

                    Me.Stop()

                    Return
                End If
#End If

                WebService.CrearCanalSeguro()
                WebService.setUser(Program.Config.SecurityWebServiceUser, FileSendingConfig.Decrypt(Program.Config.SecurityWebServicePassword))
                Program.ConnectionStrings = FileSendingConfig.getCadenasConexion(WebService)

                If Program.ConnectionStrings.Imaging = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Imaging")
                    Me.Stop()

                    Return
                End If

                If Program.ConnectionStrings.Core = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Core")
                    Me.Stop()

                    Return
                End If

                ' Registrar el canal de comunicación
                CanalTCP = New TcpChannel(Program.Config.Puerto)
                ChannelServices.RegisterChannel(CanalTCP, False)

                'DataRemoting
                If (Program.Config.UsaRemoting) Then
                    DataRemoting = ";RemotingUrl=tcp://" & Program.Config.RemotingIPName & ":" & Program.Config.RemotingPuerto & "/" & Program.Config.RemotingAppName & ";RemotingPwd=" & FileSendingConfig.Decrypt(Program.Config.RemotingPassword) & ";RemotingTrusted=" & Program.Config.RemotingCifrado
                    ' Publicar la referencia remota al objeto
                    FileSending = New FileSendingServer(Program.Config.WorkingFolder, Program.ConnectionStrings.Core & DataRemoting, Program.ConnectionStrings.Imaging & DataRemoting, Program.ConnectionStrings.Banagrario & DataRemoting, Program.Config.IdentifierDateFormat, DataRemoting, Program.ConnectionStrings.Tools & DataRemoting, Program.ConnectionStrings.Integration & DataRemoting, Program.Config.LogFolder, Program.Config.LogActivo)
                    RemotingServices.Marshal(FileSending, Program.Config.AppName)
                Else
                    ' Publicar la referencia remota al objeto
                    FileSending = New FileSendingServer(Program.Config.WorkingFolder, Program.ConnectionStrings.Core, Program.ConnectionStrings.Imaging, Program.ConnectionStrings.Banagrario, Program.Config.IdentifierDateFormat, "", Program.ConnectionStrings.Tools, Program.ConnectionStrings.Integration, Program.Config.LogFolder, Program.Config.LogActivo)
                    RemotingServices.Marshal(FileSending, Program.Config.AppName)
                End If


            Catch ex As Exception
                WriteErrorLog(ex.Message)

                Me.Stop()

                DetenerServicio()
            End Try
        End Sub

        Public Sub DetenerServicio()
            Try : RemotingServices.Disconnect(FileSending) : Catch : End Try

            Try : ChannelServices.UnregisterChannel(Me.CanalTCP) : Catch : End Try

            Me.Stop()
        End Sub

        Public Sub WriteErrorLog(ByVal nMessage As String)
            Try
                Dim sw As New StreamWriter(Program.AppDataPath & "log.txt", True)

                sw.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sw.WriteLine("Mensaje: " & nMessage)
                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine("")

                sw.Flush()
                sw.Close()
            Catch ex As Exception
                Try : EventLog.WriteEntry(ex.Message, EventLogEntryType.Error) : Catch : End Try
            End Try

            Application.DoEvents()
        End Sub

#End Region

    End Class

End Namespace