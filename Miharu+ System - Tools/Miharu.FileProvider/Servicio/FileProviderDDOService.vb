Imports System
Imports System.IO
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Threading

Public Class FileProviderDDOService

#Region " Declaraciones "

    Private FileProvider As Library.FileProvider

    Private CanalTCP As TcpChannel

    Private Detener As Boolean

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
        If (File.Exists(Program.AppDataPath + Library.FileProviderConfig.ConfigFileName)) Then
            Program.Config = Library.FileProviderConfig.Deserialize(Program.AppDataPath)
        End If
    End Sub

    Public Sub IniciarServicio()
        Try
            WriteErrorLog("entró")
            LoadConfig()
            WriteErrorLog("loadconfig")
            ' Registrar el canal de comunicación
            CanalTCP = New TcpChannel(Program.Config.Puerto)
            ChannelServices.RegisterChannel(CanalTCP, False)
            WriteErrorLog("tcp")

            ' Publicar la referencia remota al objeto
            Program.Config.WorkingFolder = Program.Config.WorkingFolder.TrimEnd("\"c) & "\"
            FileProvider = New Library.FileProvider(Program.Config.WorkingFolder)
            'FileProvider = New Library.FileProvider()
            RemotingServices.Marshal(FileProvider, Program.Config.AppName)

            'Se lanza Hilo de depuración de imagenes antiguas.
            'Dim NewThread As New Thread(AddressOf DepurarWorkingFolder)
            'NewThread.Start()

            Detener = False

        Catch ex As Exception
            WriteErrorLog(ex.Message)

            Me.Stop()

            DetenerServicio()
        End Try
    End Sub

    Public Sub DetenerServicio()
        Try : RemotingServices.Disconnect(FileProvider) : Catch : End Try

        Detener = True

        Try : ChannelServices.UnregisterChannel(Me.CanalTCP) : Catch : End Try
    End Sub

    Private Sub DepurarWorkingFolder()
        Try
            While Not Detener
                If Detener Then Return

                Dim Directorios() As String

                'Se eliminan los Anexos
                Directorios = Directory.GetDirectories(Program.Config.WorkingFolder.TrimEnd("\"c) & "\Cache\Anexos")
                For Each nFiles As String In Directorios
                    If Detener Then Return
                    EliminaFolder(nFiles)
                Next

                'Se eliminan los Files
                Directorios = Directory.GetDirectories(Program.Config.WorkingFolder.TrimEnd("\"c) & "\Cache\Files")
                For Each nFiles As String In Directorios
                    If Detener Then Return
                    EliminaFolder(nFiles)
                Next

                'Se eliminan los Items
                Directorios = Directory.GetDirectories(Program.Config.WorkingFolder.TrimEnd("\"c) & "\Cache\Items")
                For Each nItems As String In Directorios
                    If Detener Then Return
                    EliminaFolder(nItems)
                Next

                If Detener Then Return

                Thread.Sleep(Program.Config.IntervaloDepuracion) ' Esperar 6 Horas antes de volver a procesar.
            End While
        Catch : End Try
    End Sub

    Private Sub EliminaFolder(ByVal nPathDirectorio As String)
        Try
            Dim Folder As New DirectoryInfo(nPathDirectorio)
            '''WriteErrorLog("Directorio: [" & nPathDirectorio & "] - DateFolder: [" & Folder.CreationTime & "] - Aplica: [" & (Folder.CreationTime <= DateAdd(DateInterval.Hour, -Program.Config.TiempoHistoricoEliminacion, Now.Date)) & "]")
            If (Folder.CreationTime <= DateAdd(DateInterval.Hour, -Program.Config.TiempoHistoricoEliminacion, Now)) Then
                Directory.Delete(nPathDirectorio, True)
            End If
        Catch ex As Exception
            WriteErrorLog(ex.Message)
        End Try
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

        System.Windows.Forms.Application.DoEvents()
    End Sub

#End Region

End Class
