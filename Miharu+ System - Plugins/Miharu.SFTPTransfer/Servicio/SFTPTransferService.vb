Imports System.ServiceProcess
Imports System.IO
Imports Miharu.Security.Library.WebService
Imports System.Threading
Namespace Servicio
    Public Class SFTPTransferService
#Region " Declaraciones "
        Private Detener As Boolean
        Private objectLock As New Object
#End Region
#Region " Eventos "
        Public Sub New()
            InitializeComponent()
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
#End Region
#Region " Metodos "
        Private Sub LoadConfig()
            ' Leer la configuración
            If (File.Exists(Program.AppDataPath + SFTPTransferConfig.ConfigFileName)) Then
                Program.Config = SFTPTransferConfig.Deserialize(Program.AppDataPath)
            End If
        End Sub
        Public Sub IniciarServicio()
            'Uso esto para depurar, en produccion no estara.. Se hace para iniciar el servicio lento con eso podemos atar el depurador al proceso
            'Thread.Sleep(15000)
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
                WebService.setUser(Program.Config.User, SFTPTransferConfig.Decrypt(Program.Config.Password))
                Program.ConnectionStrings = SFTPTransferConfig.getCadenasConexion(WebService)
                If Program.ConnectionStrings.SofTrac = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos SofTrac")
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
                JWriteLog("Error IniciarServicio ex: " & ex.Message & " " & ex.ToString(), EventLogEntryType.Error)
                Me.Stop()
            End Try
        End Sub
        Public Sub DetenerServicio()
            Detener = True
        End Sub
        Private Sub Proceso()
            Try
                Dim procesadorHilosInstance As New ProcesadorHilos
                procesadorHilosInstance.servicio = Me
                procesadorHilosInstance.AgregarHilo()
                While (procesadorHilosInstance.TerminoHilos = False)
                    System.Threading.Thread.Sleep(1000)
                End While
                JWriteLog("Finalizo Hilo Transfiere archivos", EventLogEntryType.Information)
            Catch ex As Exception
                WriteErrorLog("Excepcion en MoverArchivosSFTPTransferService: " + ex.ToString())
            End Try
        End Sub
        Public Sub JWriteLog(ByVal mensaje As String, ByVal tipo As EventLogEntryType)
            If Not EventLog.SourceExists("SFTPTransferService") Then
                EventLog.CreateEventSource("SFTPTransferService", "Application")
            End If
            Dim eventLog1 As EventLog = New EventLog()
            eventLog1.Source = "SFTPTransferService"
            eventLog1.WriteEntry(mensaje, tipo)
        End Sub
        Private Sub WriteErrorLog(ByVal nMessage As String)
            SyncLock objectLock
                Try
                    'JWriteLog("WriteErrorLog Path: " & Program.AppDataPath & "log_" & Now.ToString("yyyyMMdd") & ".txt", EventLogEntryType.Information)
                    'JWriteLog(nMessage, EventLogEntryType.Error)
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
            End SyncLock
        End Sub
        Public Sub ProcesoPrincipalHilo()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim proyecto As DBImaging.SchemaProcess.CTA_Get_Consulta_Transferencia_Procesamiento_Sobre_ImagenesRow
            Dim MovioFile As Boolean = True
            Dim idTransferencia As Long
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(1)
                Dim ProyectoImagingDataTable = dbmImaging.SchemaProcess.PA_Get_Consulta_Transferencia_Procesamiento_Sobre_Imagenes.DBExecute()
                If ProyectoImagingDataTable.Rows.Count > 0 Then
                    JWriteLog("Inicia Transferencia Directorios ...", EventLogEntryType.Information)
                    For Each FilaProyecto As DataRow In ProyectoImagingDataTable.Rows
                        proyecto = FilaProyecto
                        Dim fk_Entidad As Short = proyecto.fk_Entidad
                        Dim fk_Proyecto As Short = proyecto.fk_Proyecto
                        Dim RutaOrigen = proyecto.Ruta_Original_Procesamiento_Sobre_Imagenes
                        Dim RutaDestino = proyecto.Ruta_Destino_Procesamiento_Sobre_Imagenes
                        Dim HoraLimite = proyecto.Hora_Limite_Fecha_Proceso
                        If Directory.Exists(RutaOrigen) Then
                            Try
                                For Each Directorio As String In My.Computer.FileSystem.GetDirectories(RutaOrigen, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                                    Dim TransferenciaUpdateType As New DBImaging.SchemaProcess.TBL_Transferencia_Procesamiento_Sobre_ImagenesType
                                    Dim NombreDirectorio = My.Computer.FileSystem.GetName(Directorio)
                                    If Not Directory.Exists(RutaDestino + "\" + NombreDirectorio) Then
                                        Directory.CreateDirectory(RutaDestino + "\" + NombreDirectorio)
                                    End If
                                    idTransferencia = dbmImaging.SchemaProcess.PA_Insertar_Transferencia_Procesamiento_Sobre_Imagenes.DBExecute(fk_Entidad, fk_Proyecto, NombreDirectorio, HoraLimite)
                                    My.Computer.FileSystem.MoveDirectory(Directorio, RutaDestino + "\" + NombreDirectorio, True)
                                    TransferenciaUpdateType.Fecha_Fin_Transferencia = DateTime.Now
                                    dbmImaging.SchemaProcess.TBL_Transferencia_Procesamiento_Sobre_Imagenes.DBUpdate(TransferenciaUpdateType, idTransferencia)
                                    JWriteLog("Transfiriendo directorio: " & NombreDirectorio, EventLogEntryType.Information)
                                Next
                            Catch ex As Exception
                                Dim TransferenciaUpdateErrorType As New DBImaging.SchemaProcess.TBL_Transferencia_Procesamiento_Sobre_ImagenesType
                                TransferenciaUpdateErrorType.Observacion = ex.Message
                                dbmImaging.SchemaProcess.TBL_Transferencia_Procesamiento_Sobre_Imagenes.DBUpdate(TransferenciaUpdateErrorType, idTransferencia)
                                WriteErrorLog("Excepcion al mover archivos: " + ex.ToString())
                                MovioFile = False
                            End Try
                        Else
                            WriteErrorLog(String.Format("No se encontro el Directorio {0} "))
                        End If
                    Next
                End If
            Catch ex As Exception
                WriteErrorLog("Error en trasferencia : " + ex.ToString())
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub
        Public Sub ProcesoHiloArchivos(nParametroHilo As Object)
        End Sub
#End Region
#Region " Metodos reemplazados "
        Protected Overrides Sub OnStart(ByVal args() As String)
            IniciarServicio()
        End Sub
        Protected Overrides Sub OnStop()
            DetenerServicio()
        End Sub
#End Region
    End Class
End Namespace