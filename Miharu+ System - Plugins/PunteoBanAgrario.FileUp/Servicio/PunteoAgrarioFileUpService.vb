Imports System.IO
Imports System.Threading
Imports System.Diagnostics
Imports DBAgrario
Imports Slyg.Tools.Zip
Imports Miharu.Security.Library.WebService

Public Class PunteoAgrarioFileUpService

#Region " Declaraciones "
    Private Detener As Boolean = False
#End Region

#Region " Metodos remplazados "

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
            WebService.setUser(Program.Config.User, PunteoAgrarioFileUpConfig.Decrypt(Program.Config.Password))
            Program.ConnectionStrings = PunteoAgrarioFileUpConfig.getCadenasConexion(WebService)

            If Program.ConnectionStrings.PunteoAgrario = "" Then
                WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos PunteoBanAgrario")
                Me.Stop()

                Return
            End If

            'Se valida que se encuentre instalada la utilidad de cifrado.
            Dim procesoCipher As Process = New Process()
            procesoCipher.StartInfo.FileName = "runcipher"
            procesoCipher.StartInfo.CreateNoWindow = False
            procesoCipher.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            procesoCipher.StartInfo.UseShellExecute = True

            Try
                procesoCipher.Start()
                procesoCipher.WaitForExit(1000)
            Catch ex As Exception
                WriteErrorLog("No se encuentra configurada la utilidad de cifrado [JCipher].")
                'Me.Stop()

                Return
            End Try



            Dim NewThread As New Thread(AddressOf Proceso)

            Detener = False

            NewThread.Start()

            Dim dueTime As Integer
            dueTime = (Now.Hour) * 60 + Now.Minute

            If dueTime = Program.Config.HoraGeneracionPDF Then
                Dim NewThreadReport As New Thread(AddressOf ProcesoGenerarReportes)
                Detener = False
                NewThreadReport.Start()
            End If



        Catch ex As Exception
            WriteErrorLog(ex.Message)
            Me.Stop()
        End Try
    End Sub

    Public Sub DetenerServicio()
        Detener = True
    End Sub

    ' Leer la configuración
    Private Sub LoadConfig()
        If System.IO.File.Exists(Program.AppDataPath + PunteoAgrarioFileUpConfig.ConfigFileName) Then
            Program.Config = PunteoAgrarioFileUpConfig.Deserialize(Program.AppDataPath)
        End If
    End Sub

    'Lógica del servicio para realizar la carga del Log del Punteo Electrónico Diario
    Private Sub Proceso()
        Dim dbmBanagrario As New DBAgrarioDataBaseManager(Program.ConnectionStrings.PunteoAgrario)
        Dim archivo As IO.FileInfo

        Try
            'Obtener los path de almacenamiento de archivos
            Dim CarpetaArchivosACargar As String = Program.Config.CarpetaFileUp
            Dim CarpetaArchivosCorrectos As String = Program.Config.CarpetaProcesado
            Dim CarpetaArchivosError As String = Program.Config.CarpetaNoProcesado

            Dim NombreLlaveCifrado As String = Program.Config.NombreLlaveCifrado

            'Crear los directorios donde se trasladarán los archivos luego del proceso de carga.
            If Not (System.IO.Directory.Exists(CarpetaArchivosCorrectos & "\")) Then
                System.IO.Directory.CreateDirectory(CarpetaArchivosCorrectos & "\")
            End If

            If Not (System.IO.Directory.Exists(CarpetaArchivosError & "\")) Then
                System.IO.Directory.CreateDirectory(CarpetaArchivosError & "\")
            End If

            While Not Detener
                If Detener Then Return

                'Recorre los archivos que contiene la carpeta
                For Each arc In System.IO.Directory.GetFiles(CarpetaArchivosACargar)
                    Dim ArchiveIn As String = ""
                    Dim ArchiveOut As String = ""

                    archivo = New IO.FileInfo(arc)

                    'Los archivos cifrados tienen la extensión "enc", se debe decifrar y posteriormente renombrar para poderlo procesar.
                    'Se asume que los archivos vienen con la estructura: pediario_MMddAAAA.txt.enc.
                    If (Path.GetFileName(archivo.Name.ToUpper()).EndsWith("ENC")) Then
                        Dim procesoCipher As Process = New Process()

                        ArchiveIn = CarpetaArchivosACargar & "\" & archivo.Name
                        ArchiveOut = CarpetaArchivosACargar & "\" & Path.GetFileNameWithoutExtension(archivo.Name)

                        'Decifra el archivo.
                        procesoCipher.StartInfo.FileName = "runcipher"
                        procesoCipher.StartInfo.Arguments = "-d """ & NombreLlaveCifrado & """ """ & ArchiveIn & """ """ & ArchiveOut & """"
                        procesoCipher.StartInfo.CreateNoWindow = False
                        procesoCipher.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                        procesoCipher.StartInfo.UseShellExecute = True

                        procesoCipher.Start()

                        Do While Not procesoCipher.HasExited
                            procesoCipher.WaitForExit(100)
                        Loop

                        'Elimina el archivo cifrado
                        IO.File.Delete(CarpetaArchivosACargar & "\" & archivo.Name)

                        'Se descomprime el archivo decifrado.
                        ''El archivo ahora viene sin comprimir.
                        'ZipUtil.Descomprimir(CarpetaArchivosACargar, ArchiveOut, True, False)

                        archivo = New IO.FileInfo(ArchiveOut)
                    End If

                    Dim resultadoEjecucion As String

                    'El nombre del archivo debe comenzar con "pediario_".
                    If (Path.GetFileNameWithoutExtension(archivo.Name.ToUpper()).StartsWith("PEDIARIO_")) Then
                        dbmBanagrario.Connection_Open(0)
                        dbmBanagrario.Transaction_Begin()

                        resultadoEjecucion = dbmBanagrario.SchemaLoad.PA_Cargue_Archivo_Cruce_Principal.DBExecute(CarpetaArchivosACargar & "\" & archivo.Name)
                        If resultadoEjecucion <> "" Then
                            'Mover el log a carpeta de archivos NO procesados
                            dbmBanagrario.Transaction_Rollback()
                            MoverArchivos(CarpetaArchivosACargar & "\" & archivo.Name, CarpetaArchivosError & "\" & archivo.Name)
                            Throw New Exception("Error en Base de Datos: " & resultadoEjecucion)
                        Else
                            'Mover el log a carpeta de archivos procesados
                            dbmBanagrario.Transaction_Commit()
                            MoverArchivos(CarpetaArchivosACargar & "\" & archivo.Name, CarpetaArchivosCorrectos & "\" & archivo.Name)
                        End If

                        dbmBanagrario.Connection_Close()
                    End If
                Next
            End While

        Catch ex As Exception
            WriteErrorLog(ex.Message)

        Finally
            Try : dbmBanagrario.Connection_Close() : Catch : End Try
        End Try
        ' Me.Stop()
    End Sub

    Protected Sub MoverArchivos(ByVal PathOrigen As String, ByVal PathDestino As String)
        'Mover el archivo
        IO.File.Copy(PathOrigen, PathDestino, True)
        'Eliminar el archivo objeto de carga
        IO.File.Delete(PathOrigen)
    End Sub

    Private Sub ProcesoGenerarReportes()

        Try
            While Not Detener
                If Detener Then Return
                Dim rManager As New ReportManager()
                Dim CarpetaGeneracionPDF As String = Program.Config.CarpetaGeneracionPDF
                rManager.GenerarTodos(CarpetaGeneracionPDF)
            End While

        Catch ex As Exception
            WriteErrorLog(ex.Message)
        Finally
        End Try

        Me.Stop()
    End Sub

    'Registrar errores en Log
    Private Sub WriteErrorLog(ByVal nMessage As String)
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
