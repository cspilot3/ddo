Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports DBCore
Imports DBImaging
Imports DBImaging.SchemaConfig
Imports DBImaging.SchemaProcess
Imports DBSecurity
Imports Ghostscript.NET
Imports Ghostscript.NET.Rasterizer
Imports Microsoft.VisualBasic.ApplicationServices
Imports Miharu.FileProvider.Library
Imports Miharu.Security.Library.Session
Imports Miharu.Security.Library.WebService
Imports SharpCompress.Archives
Imports SharpCompress.Common
Imports Slyg.Data
Imports Slyg.Tools
Imports Slyg.Tools.Imaging


Namespace Servicio
    Public Class CargueImagenesService

#Region " Declaraciones "
        Public Const MaxThumbnailWidth As Integer = 60
        Public Const MaxThumbnailHeight As Integer = 80
        Private Detener As Boolean
        Private DiaInicial As Integer = 0
        Private DiaFinal As Integer = 0
        Private Segundos As Integer = 0
        Private Minuto As Integer = 0
        Private Hora As Integer = 0
        Private _DataRemoting As String
        Dim ErrorMsg As String = ""
        Public Property FechaProcesoInt() As Integer
        Public Property NewOT() As Integer

        Public CargueLote As New DBImaging.SchemaProcess.TBL_Lote_DigitalizacionType()
        Public CargueType As New DBImaging.SchemaProcess.TBL_CargueType()
        Public FechaProcesoType As New DBImaging.SchemaProcess.TBL_Fecha_ProcesoType()
        Public OT_Type As New DBImaging.SchemaProcess.TBL_OTType()
        Public ContenedorType As New DBImaging.SchemaProcess.TBL_ContenedorType()
        Dim Config As New CargueImagenesConfig

        Private _cts As CancellationTokenSource
        Private _workers As List(Of Thread)


#End Region

#Region " Metodos reemplazados "
        Protected Overrides Sub OnStart(ByVal args() As String)
            Try
                WriteErrorLog("OnStart: inicializando…")

                ' Crea estructuras para esta ejecución
                _cts = New CancellationTokenSource()
                _workers = New List(Of Thread)()

                ' Llama tu método (inicializa WS, cadenas, etc. y lanza los hilos)
                IniciarServicio()

                WriteErrorLog("OnStart: servicio iniciado correctamente.")
            Catch ex As Exception
                WriteErrorLog("OnStart error: " & ex.ToString())
                Throw ' Importante: permite que Windows registre el error real de inicio
            End Try


        End Sub

        Protected Overrides Sub OnStop()

            ' Agregue el código aquí para realizar cualquier anulación necesaria para detener el servicio.
            'DetenerServicio()

            Try
                WriteErrorLog("OnStop: cancelando workers…")
                If _cts IsNot Nothing Then _cts.Cancel()
                If _workers IsNot Nothing Then
                    For Each t In _workers
                        If t IsNot Nothing AndAlso t.IsAlive Then
                            t.Join(TimeSpan.FromSeconds(120))
                        End If
                    Next
                End If
                WriteErrorLog("OnStop: servicio detenido.")
            Catch ex As Exception
                WriteErrorLog("OnStop error: " & ex.ToString())
            End Try

        End Sub

#End Region

#Region " Metodos "

        Private Sub LoadConfig()
            ' Leer la configuración
            If (System.IO.File.Exists(Program.AppDataPath + CargueImagenesConfig.ConfigFileName)) Then
                Program.Config = CargueImagenesConfig.Deserialize(Program.AppDataPath)
            End If
        End Sub


        Public Sub IniciarServicio()

            ' WriteErrorLog("Funcion Iniciar Servicio")

            LoadConfig()

            ' 1) Validar configuración requerida
            If String.IsNullOrWhiteSpace(Program.Config.SecurityWebServiceURL) Then
                Throw New ConfigurationErrorsException("SecurityWebServiceURL vacío o nulo.")
            End If

            Try
                Dim webService As New SecurityWebService(Program.Config.SecurityWebServiceURL, "")
                WriteErrorLog("URL WS: " & Program.Config.SecurityWebServiceURL)

#If Not DEBUG Then
            ' 2) Validación de versión (si no corresponde → lanzar excepción)
            Dim versionApp As String = webService.getAssemblyVersion(Program.AssemblyName)
            If Not String.Equals(versionApp, Program.AssemblyVersion, StringComparison.OrdinalIgnoreCase) Then
                Throw New InvalidOperationException(
                    "La versión del aplicativo no corresponde. " &
                    $"Registrada=[{versionApp}] Ejecutable=[{Program.AssemblyVersion}]")
            End If
#End If

                ' 3) Inicialización segura del WS / credenciales / conn strings
                webService.CrearCanalSeguro()
                webService.setUser(Program.Config.User, CargueImagenesConfig.Decrypt(Program.Config.Password))

                Program.ConnectionStrings = CargueImagenesConfig.getCadenasConexion(webService)

                If String.IsNullOrWhiteSpace(Program.ConnectionStrings.Security) Then
                    Throw New InvalidOperationException("No se pudo obtener la cadena de conexión 'Security'.")
                End If
                If String.IsNullOrWhiteSpace(Program.ConnectionStrings.Imaging) Then
                    Throw New InvalidOperationException("No se pudo obtener la cadena de conexión 'Imaging'.")
                End If

                ' 4) Lanzar **N** hilos leídos desde config
                _cts = New CancellationTokenSource()

                Dim n As Integer = Config.WorkersCount
                _workers = New List(Of Thread)(capacity:=n)

                Dim workerId = 1 ' capturar variable de bucle
                Dim t As New Thread(Sub() EjecutarProcesoSeguroCarpetas(_cts.Token, workerId))
                t.IsBackground = True
                _workers.Add(t)
                t.Start()

                If n > 1 Then
                    For i As Integer = 1 To n - 1
                        workerId = i + 1 ' capturar variable de bucle
                        t = New Thread(Sub() EjecutarProcesoSeguro(_cts.Token, workerId))
                        t.IsBackground = True
                        _workers.Add(t)
                        t.Start()
                    Next
                Else
                    workerId = 2 ' capturar variable de bucle
                    t = New Thread(Sub() EjecutarProcesoSeguro(_cts.Token, workerId))
                    t.IsBackground = True
                    _workers.Add(t)
                    t.Start()
                End If

                'Dim workerId = 1 ' capturar variable de bucle
                'Dim t As New Thread(Sub() EjecutarProcesoSeguro(_cts.Token, workerId))
                't.IsBackground = True
                '_workers.Add(t)
                't.Start()

                WriteErrorLog($"Servicio OK. Workers lanzados: {n}")
            Catch ex As Exception
                ' Importante: log completo y relanzar para que Windows lo registre
                WriteErrorLog("Error IniciarServicio: " & ex.ToString())
                Throw
            End Try
        End Sub

        Private Sub EjecutarProcesoSeguroCarpetas(token As Threading.CancellationToken, workerId As Integer)
            Try
                ' WriteErrorLog($"Worker {workerId} iniciado")

                ' Si tu Proceso() ya hace un loop infinito, puedes llamarlo directo aquí.
                ' Recomendado: hacer un loop y llamar a tu Proceso() por iteración.
                While Not token.IsCancellationRequested
                    ' ---- TU LÓGICA REAL ----
                    ProcesoCarpetas()           ' tu método existente
                    ' Evitar bucle apretado si no hay trabajo:
                    Threading.Tasks.Task.Delay(10000, token).Wait()
                End While

                WriteErrorLog($"Worker {workerId} finalizado por cancelación")
            Catch ex As OperationCanceledException
                ' Cancelación esperada al detener el servicio
                WriteErrorLog($"Worker {workerId} cancelado (OperationCanceledException)")
            Catch ex As Exception
                WriteErrorLog($"Worker {workerId} error: {ex}")
                ' Aquí decides si reintentas, o dejas morir el worker
            End Try

        End Sub

        Private Sub EjecutarProcesoSeguro(token As Threading.CancellationToken, workerId As Integer)
            Try
                ' WriteErrorLog($"Worker {workerId} iniciado")

                ' Si tu Proceso() ya hace un loop infinito, puedes llamarlo directo aquí.
                ' Recomendado: hacer un loop y llamar a tu Proceso() por iteración.
                While Not token.IsCancellationRequested
                    ' ---- TU LÓGICA REAL ----
                    Proceso(workerId)           ' tu método existente
                    ' Evitar bucle apretado si no hay trabajo:
                    Threading.Tasks.Task.Delay(10000, token).Wait()
                End While

                WriteErrorLog($"Worker {workerId} finalizado por cancelación")
            Catch ex As OperationCanceledException
                ' Cancelación esperada al detener el servicio
                WriteErrorLog($"Worker {workerId} cancelado (OperationCanceledException)")
            Catch ex As Exception
                WriteErrorLog($"Worker {workerId} error: {ex}")
                ' Aquí decides si reintentas, o dejas morir el worker
            End Try

        End Sub

        Public Sub DetenerServicio()
            Detener = True
        End Sub

        Private Sub ProcesoCarpetas()
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Dim fecha As Date = Date.Now
            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)
                While Not Detener
                    If Detener Then Return
                    Try
                        dbmSecurity.Connection_Open(1)
                        dbmCore.Connection_Open(1)
                        Dim CargueAutomaticoDataTable = dbmCore.SchemaConfig.PA_Consulta_Parametro_CargueImagenes_Get.DBExecute("CargueAutomaticoImagenes")
                        Try
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                        Catch ex As Exception : End Try

                        If CargueAutomaticoDataTable.Rows.Count = 0 Then
                            WriteErrorLog("00- No existe configuración para el proceso de cargue automático de imágenes")
                            Return
                        End If

                        For Each RowParam As DataRow In CargueAutomaticoDataTable.Rows
                            DiaInicial = RowParam.Item("Dia_Inicial")
                            DiaFinal = RowParam.Item("Dia_Final")

                            If fecha.Day >= DiaInicial AndAlso fecha.Day <= DiaFinal Then
                                Dim CalendarioDataTable = dbmSecurity.SchemaConfig.TBL_Calendario.DBFindByfk_EntidadNombre_Calendario(1, "CargueAutomaticoImagenes")
                                If CalendarioDataTable.Rows.Count > 0 Then
                                    Dim habil = dbmSecurity.SchemaConfig.PA_Es_Hora_Habil.DBExecute(CalendarioDataTable(0).fk_Entidad, CalendarioDataTable(0).id_Calendario)

                                    If (habil) And DateTime.Now.Hour >= Val(RowParam.Item("Hora_Inicio")) Then
                                        Try
                                            CargarCarpetas(RowParam)
                                        Catch ex As Exception
                                            Continue For
                                        End Try

                                    Else
                                        WriteErrorLog("No es una hora habil para ejecutar el proceso")
                                    End If
                                Else
                                    WriteErrorLog("No existe el calendario del servicio de cargue de imagenes")
                                End If
                            Else
                                WriteErrorLog("No esta en las fechas establecidas de cargue de imagenes")
                            End If
                        Next
                    Catch ex As Exception
                        WriteErrorLog("001- Error Proceso ex: " & ex.ToString())
                    Finally
                        Try
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                            If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                        Catch ex As Exception
                        End Try
                    End Try

                    If Detener Then Return

                    Thread.Sleep(Program.Config.Intervalo) ' Esperar n segundos antes de continuar
                End While
            Catch ex As Exception
                WriteErrorLog("002- Error Proceso ex: " & ex.ToString())
            Finally
                Try
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                Catch ex As Exception
                End Try
            End Try
        End Sub


        Private Sub Proceso(Hilo As Integer)
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Dim fecha As Date = Date.Now
            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)
                While Not Detener
                    If Detener Then Return
                    Try
                        dbmSecurity.Connection_Open(1)
                        dbmCore.Connection_Open(1)
                        Dim CargueAutomaticoDataTable = dbmCore.SchemaConfig.PA_Consulta_Parametro_CargueImagenes_Get.DBExecute("CargueAutomaticoImagenes")
                        Try
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                        Catch ex As Exception : End Try

                        If CargueAutomaticoDataTable.Rows.Count = 0 Then
                            WriteErrorLog("00- No existe configuración para el proceso de cargue automático de imágenes")
                            Return
                        End If

                        For Each RowParam As DataRow In CargueAutomaticoDataTable.Rows
                            DiaInicial = RowParam.Item("Dia_Inicial")
                            DiaFinal = RowParam.Item("Dia_Final")

                            If fecha.Day >= DiaInicial AndAlso fecha.Day <= DiaFinal Then
                                Dim CalendarioDataTable = dbmSecurity.SchemaConfig.TBL_Calendario.DBFindByfk_EntidadNombre_Calendario(1, "CargueAutomaticoImagenes")
                                If CalendarioDataTable.Rows.Count > 0 Then
                                    Dim habil = dbmSecurity.SchemaConfig.PA_Es_Hora_Habil.DBExecute(CalendarioDataTable(0).fk_Entidad, CalendarioDataTable(0).id_Calendario)

                                    If (habil) And DateTime.Now.Hour >= Val(RowParam.Item("Hora_Inicio")) Then
                                        'CargarCarpetas(RowParam)
                                        CargarArchivosAutomatico(RowParam, Hilo)
                                    Else
                                        WriteErrorLog("No es una hora habil para ejecutar el proceso")
                                    End If
                                Else
                                    WriteErrorLog("No existe el calendario del servicio de cargue de imagenes")
                                End If
                            Else
                                WriteErrorLog("No esta en las fechas establecidas de cargue de imagenes")
                            End If
                        Next
                    Catch ex As Exception
                        WriteErrorLog("001- Error Proceso ex: " & ex.ToString())
                    Finally
                        Try
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                            If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                        Catch ex As Exception
                        End Try
                    End Try

                    If Detener Then Return

                    Thread.Sleep(Program.Config.Intervalo) ' Esperar n segundos antes de continuar
                End While
            Catch ex As Exception
                WriteErrorLog("002- Error Proceso ex: " & ex.ToString())
            Finally
                Try
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                Catch ex As Exception
                End Try
            End Try
        End Sub

        Private Sub CargarCarpetas(RowParam As DataRow)
            Dim RutaOrigen As String = RowParam.Item("Ruta_Inicial").ToString().Trim
            If Trim(RutaOrigen.ToString()) <> "" Then
                Dim Extensiones As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
                Dim NombreTipoArchivo As String = RowParam.Item("estructura_Nombre_Archivo").ToString().Trim
                Dim textoExt As String = Convert.ToString(RowParam.Item("extension_Archivo"))
                Dim partes = textoExt.Split({"|"c}, StringSplitOptions.RemoveEmptyEntries)
                For Each p In partes
                    Dim s = p.Trim()
                    If s.Length > 0 Then
                        If Not s.StartsWith(".") Then s = "." & s
                        Extensiones.Add(s)
                    End If
                Next
                Dim FileNames As IEnumerable(Of String)
                If NombreTipoArchivo <> "*" Then
                    FileNames = Directory.EnumerateFiles(RutaOrigen, "*", System.IO.SearchOption.TopDirectoryOnly) _
                     .Where(Function(ruta)
                                Dim nombre = Path.GetFileName(ruta)
                                Dim ext = Path.GetExtension(ruta)

                                ' Coincidencia por prefijo (case-insensitive)
                                Dim coincidePrefijo = nombre.StartsWith(NombreTipoArchivo, StringComparison.OrdinalIgnoreCase)

                                ' Si no hay extensiones válidas, NO filtrar por extensión (aceptar todas).
                                Dim coincideExtension = (Extensiones.Count = 0) OrElse Extensiones.Contains(ext)

                                Return coincidePrefijo AndAlso coincideExtension
                            End Function)

                Else
                    FileNames = Directory.EnumerateFiles(RutaOrigen, "*", SearchOption.AllDirectories) _
                            .Where(Function(ruta)
                                       Dim ext = Path.GetExtension(ruta)
                                       ' Si no hay extensiones válidas, NO filtrar por extensión (acepta todas).
                                       Dim coincideExtension = (Extensiones.Count = 0) OrElse Extensiones.Contains(ext, StringComparer.OrdinalIgnoreCase)
                                       Return coincideExtension
                                   End Function)

                End If
                Dim dbmImagingCar As DBImaging.DBImagingDataBaseManager = Nothing
                Try
                    dbmImagingCar = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
                    dbmImagingCar.Connection_Open(1)
                    For Each nfile In FileNames
                        'Validar dias de caida en nombre carpeta
                        If Val(RowParam.Item("dias_Caida")) > 0 Then
                            Dim Carpeta As String = Path.GetDirectoryName(nfile)
                            Dim FechaCarpeta = ExtraerFechaCarpeta(Carpeta)
                            If FechaCarpeta Is Nothing Then
                                WriteErrorLog("El archivo: " & nfile.ToString() & " no se carga por que no se pudo extraer la fecha de la carpeta.")
                                Continue For
                            End If
                            Dim DiasDiferencia As Integer = DateDiff(DateInterval.Day, FechaCarpeta.Value, Now.Date)
                            If Now.Date <= FechaCarpeta.Value And DiasDiferencia >= Val(RowParam.Item("dias_Caida")) Then
                                Continue For
                            End If
                        End If
                        Dim NombreArchivo As String = Path.GetFileName(nfile)
                        dbmImagingCar.SchemaProcess.PA_Crea_Control_Cargue_Automatico_Imagenes.DBExecute(CInt(RowParam.Item("id_Cargue_Automatico")), nfile, NombreArchivo)
                    Next
                Catch ex As Exception
                    WriteErrorLog("Error CargarCarpetas ex: " & ex.ToString())
                Finally
                    If (dbmImagingCar IsNot Nothing) Then dbmImagingCar.Connection_Close()
                End Try
            Else
                WriteErrorLog("No existe ruta para cargar archivos: " & RutaOrigen.ToString())
            End If
        End Sub


        Public Function ExtraerFechaCarpeta(texto As String) As DateTime?

            If String.IsNullOrWhiteSpace(texto) Then Return Nothing

            Dim cultura As New CultureInfo("es-CO")

            ' Lookarounds:
            ' (?<!\d) asegura que antes del día NO haya un dígito (puede haber letras como 'Y')
            ' (?!\d) asegura que después del año NO haya un dígito
            Dim patronNumerico As String =
            "(?<!\d)(?<d>\d{1,2})\s*[-/\.]\s*(?<m>\d{1,2})\s*[-/\.]\s*(?<y>\d{2,4})(?!\d)"

            ' Mes en letras (también con lookarounds)
            Dim meses As String = "ene(?:ro)?|feb(?:rero)?|mar(?:zo)?|abr(?:il)?|may(?:o)?|jun(?:io)?|jul(?:io)?|ago(?:sto)?|sep(?:tiembre)?|set(?:iembre)?|oct(?:ubre)?|nov(?:iembre)?|dic(?:iembre)?"
            Dim patronMesLetras As String =
            "(?<!\d)(?<d>\d{1,2})\s*(?:de\s+)?(?<mes>" & meses & ")\s*(?:de\s+)?(?<y>\d{2,4})(?!\d)"

            Dim patrones = {patronNumerico, patronMesLetras}

            Dim formatosNumericos As String() = {
            "d-M-yyyy", "dd-M-yyyy", "d-MM-yyyy", "dd-MM-yyyy",
            "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy", "dd/MM/yyyy",
            "d.M.yyyy", "dd.M.yyyy", "d.MM.yyyy", "dd.MM.yyyy",
            "d-M-yy", "dd-M-yy", "d/M/yy", "dd/M/yy", "d.M.yy", "dd.M.yy",
            "d M yyyy", "dd M yyyy", "d M yy", "dd M yy"
        }

            Dim formatosConMesLetras As String() = {
            "d MMM yyyy", "dd MMM yyyy",
            "d MMMM yyyy", "dd MMMM yyyy",
            "d 'de' MMM yyyy", "dd 'de' MMM yyyy",
            "d 'de' MMMM yyyy", "dd 'de' MMMM yyyy",
            "d MMM yy", "dd MMM yy",
            "d 'de' MMM yy", "dd 'de' MMM yy"
        }

            For Each patron In patrones
                Dim rx As New Regex(patron, RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
                Dim mc = rx.Matches(texto)

                For Each m As Match In mc
                    Dim candidato As String = m.Value.Trim()
                    Dim dt As DateTime

                    If patron Is patronNumerico Then
                        Dim norm As String = Regex.Replace(candidato, "[/\.]", "-")

                        If DateTime.TryParseExact(norm, formatosNumericos, CultureInfo.InvariantCulture,
                                              DateTimeStyles.None, dt) Then
                            If dt.Day >= 1 AndAlso dt.Day <= 31 AndAlso dt.Month >= 1 AndAlso dt.Month <= 12 Then
                                ' Ajuste opcional para años de 2 dígitos (si quieres)
                                If dt.Year < 1950 Then dt = dt.AddYears(200)
                                Return dt
                            End If
                        End If

                        If DateTime.TryParse(candidato, cultura, DateTimeStyles.None, dt) Then
                            Return dt
                        End If
                    Else
                        If DateTime.TryParseExact(candidato, formatosConMesLetras, cultura,
                                              DateTimeStyles.AllowInnerWhite, dt) Then
                            If dt.Year < 1950 Then dt = dt.AddYears(200)
                            Return dt
                        End If

                        If DateTime.TryParse(candidato, cultura, DateTimeStyles.AllowInnerWhite, dt) Then
                            Return dt
                        End If
                    End If
                Next
            Next

            ' Fallback relajado (si lo deseas)
            Dim fallbackRx As New Regex("(?<!\d)\d{1,2}[-/\.]\d{1,2}[-/\.]\d{2,4}(?!\d)", RegexOptions.IgnoreCase)
            For Each m As Match In fallbackRx.Matches(texto)
                Dim dt As DateTime
                If DateTime.TryParse(m.Value, cultura, DateTimeStyles.None, dt) Then
                    If dt.Year < 1950 Then dt = dt.AddYears(200)
                    Return dt
                End If
            Next

            Return Nothing


            Return Nothing
        End Function


        Private Sub CargarArchivosAutomatico(RowParam As DataRow, Hilo As Integer)
            'Buscar las carpetas a cargar

            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(1)
            Dim CarpetasCargue = dbmImaging.SchemaProcess.PA_Consulta_Cargue_Automatico_Imagenes.DBExecute(CInt(RowParam.Item("id_Cargue_Automatico")), CInt(RowParam.Item("carpetasxHilo")))

            Dim TotCarpetas As Integer = CarpetasCargue.Rows.Count
            If CarpetasCargue.Rows.Count = 0 Then
                ' WriteErrorLog("No hay carpetas para cargue automático para ejecutar")
                Try
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                Catch ex As Exception : End Try
                Exit Sub
            End If
            Dim CarpetaOri = RowParam.Item("ruta_Inicial")

            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)
            dbmCore.Connection_Open(1)

            'Buscar los archivos ya registrados para el cargue automático
            Dim ArchivosaCargarDatatable = dbmCore.SchemaConfig.PA_Consulta_Imagenes_CargueAutomatico_Get.DBExecute(CInt(RowParam.Item("id_Cargue_Automatico")))

            'Buscar campos automaticos de cargue
            Dim CamposCargueDataTable = dbmCore.SchemaConfig.PA_Campos_Esquema_Get.DBExecute(CInt(RowParam.Item("fk_Esquema")), CInt(RowParam.Item("fk_Entidad")), CInt(RowParam.Item("fk_Proyecto")))

            'Llenar los campos automaticos de cargue
            Dim NLabel As String = ""
            Dim CamposCaptura As String = ""
            For Each campo As DataRow In CamposCargueDataTable.Rows
                If campo.Item("Nombre_Campo").ToString.ToUpper.Contains("FECHA") = True Then
                    If Trim(CamposCaptura) <> "" Then CamposCaptura &= "|"
                    CamposCaptura &= campo.Item("id_Esquema_Campo") & ";" & Format(Now(), "yyyy/MM/dd")
                End If
                If campo.Item("Nombre_Campo").ToString.ToUpper.Contains("SERIE") = True Then
                    If Trim(CamposCaptura) <> "" Then CamposCaptura &= "|"
                    CamposCaptura &= campo.Item("id_Esquema_Campo") & ";" & campo.Item("fk_Esquema")
                End If
                If campo.Item("Nombre_Campo").ToString.ToUpper.Contains("HORARIO") = True Then
                    If Trim(CamposCaptura) <> "" Then CamposCaptura &= "|"
                    CamposCaptura &= campo.Item("id_Esquema_Campo") & ";0"
                End If
                If campo.Item("Nombre_Campo").ToString.ToUpper.Contains("AREA") = True Then
                    If Trim(CamposCaptura) <> "" Then CamposCaptura &= "|"
                    CamposCaptura &= campo.Item("id_Esquema_Campo") & ";" & RowParam.Item("Area")
                    NLabel = RowParam.Item("Area")
                End If
                If campo.Item("Nombre_Campo").ToString.ToUpper.Contains("OFICINA") = True Then
                    If Trim(CamposCaptura) <> "" Then CamposCaptura &= "|"
                    CamposCaptura &= campo.Item("id_Esquema_Campo") & ";" & RowParam.Item("Oficina")
                End If
                If campo.Item("Nombre_Campo").ToString.ToUpper.Contains("LABEL") = True Then
                    If Trim(CamposCaptura) <> "" Then CamposCaptura &= "|"
                    CamposCaptura &= campo.Item("id_Esquema_Campo") & ";" & " "
                    NLabel = RowParam.Item("Area")
                End If
            Next
            'Buscar Parametros pendientes.
            Dim ParametrosDataTable As DataTable = Nothing
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)
                dbmSecurity.Connection_Open(1)
                ParametrosDataTable = dbmSecurity.SchemaConfig.PA_Get_Sede_CentroProcesamiento.DBExecute(CInt(RowParam.Item("fk_Entidad")), RowParam.Item("fk_Sede").ToString(), RowParam.Item("Oficina").ToString().Trim)
            Catch ex As Exception
                WriteErrorLog("01- No hay parametros. " & ex.Message)
                Exit Sub
            Finally
                Try
                    If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                Catch ex As Exception
                End Try
            End Try
            Dim nEntidad As Integer = RowParam.Item("fk_Entidad")
            Dim nProyecto As Integer = RowParam.Item("fk_Proyecto")
            Dim nEsquema As Integer = RowParam.Item("fk_Esquema")
            Dim nEntidadProcesamiento As Short = ParametrosDataTable.Rows(0).Item("fk_Entidad")
            Dim nSedeProcesamiento_Cargue As Short = ParametrosDataTable.Rows(0).Item("id_Sede")
            Dim nCentroProcesamiento_Cargue As Short = ParametrosDataTable.Rows(0).Item("id_Centro_Procesamiento")
            Dim nCampos As String = CamposCaptura
            Dim nUser As Integer = 2
            Dim nUsuario As String = Environment.UserName
            Dim nObservaciones As String = Environment.MachineName
            Dim nEstado As Short = 10
            Dim nCargueID As Integer = 0
            Dim nFolios As Short = 0
            Dim nContenedor As String
            Dim EsquemaDataTable As DBCore.SchemaConfig.TBL_EsquemaDataTable = Nothing
            Try
                EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, nEsquema)
            Catch ex As Exception
                ErrorMsg = "02- No fue posible encontrar el Esquema = " + nEsquema.ToString()
                WriteErrorLog(ErrorMsg)
                Try
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                Catch : End Try
                Exit Sub
            End Try
            If Not EsquemaDataTable.Count > 0 Then
                WriteErrorLog("02- No se encontro la configuración del Esquema " & nEsquema.ToString())
                Try
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                Catch ex As Exception : End Try
                Exit Sub
            End If
            Dim ServidorDataTable As DBImaging.SchemaCore.CTA_ServidorDataTable = Nothing
            Try
                ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(EsquemaDataTable(0).fk_Entidad_Servidor, EsquemaDataTable(0).fk_Servidor)
            Catch ex As Exception
                ErrorMsg = "03- No se encontro la configuiracion del Servidor = " + EsquemaDataTable(0).fk_Servidor.ToString()
                WriteErrorLog(ErrorMsg)
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                Exit Sub
            End Try
            Dim CentroProcesamientoDataTable As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoDataTable = Nothing
            Try
                CentroProcesamientoDataTable = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(CInt(nEntidad), CInt(ParametrosDataTable.Rows(0).Item("id_Sede")), CInt(ParametrosDataTable.Rows(0).Item("id_Centro_Procesamiento")))
            Catch ex As Exception
                ErrorMsg = "04- No se encontro la configuración del CentroProcesamiento_Cargue = " & Trim(ParametrosDataTable.Rows(0).Item("id_Centro_Procesamiento"))
                WriteErrorLog(ErrorMsg)
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                Exit Sub
            End Try
            Dim manager As FileProviderManager = Nothing
            manager = New FileProviderManager(ServidorDataTable(0).ToCTA_ServidorSimpleType(), CentroProcesamientoDataTable(0).ToCTA_Centro_ProcesamientoSimpleType(), dbmImaging, nUser)
            manager.Connect()



            'Verificar cada archivo en la carpeta para descomprimir si es necesario y cargarlo
            For Each RowCarpetas As DataRow In CarpetasCargue.Rows
                Dim NuevaCarpeta As String = CarpetaOri & "\" & Format(RowCarpetas.Item("fecha_Cargue"), "yyyyMMdd") & "_" & Format(Val(RowParam.Item("Oficina")), "0000") & "_" & Format(Val(RowParam.Item("fk_Esquema")), "00") & "_" & RowCarpetas.Item("id_Archivo")
                If RowParam.Item("usaDescomprimir") = True Then
                    DescomprimirArchivo(dbmImaging, RowCarpetas.Item("Ruta_Archivo").ToString().Trim, RowCarpetas, NuevaCarpeta)
                Else
                    If Not Directory.Exists(NuevaCarpeta) Then
                        Try
                            Directory.CreateDirectory(NuevaCarpeta)
                        Catch ex As Exception
                            ErrorMsg = "Descomprimir - Error al descomprimir archivo: " & NuevaCarpeta.ToString()
                            dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpetas.Item("fk_Cargue_Automatico")), CInt(RowCarpetas.Item("id_Archivo")), 0, 0, 1, 0, ErrorMsg)
                            WriteErrorLog(ErrorMsg)
                            Continue For
                        End Try
                    End If
                    File.Copy(RowCarpetas.Item("Ruta_Archivo").ToString().Trim, NuevaCarpeta & "\" & RowCarpetas.Item("nombre_Archivo").Trim, True)
                End If
                Dim carpetaBase As String = Path.GetDirectoryName(RowCarpetas.Item("Ruta_Archivo").ToString().Trim)
                Dim nombreArchivo As String = Path.GetFileNameWithoutExtension(RowCarpetas.Item("Ruta_Archivo").ToString().Trim)

                'Valido si hay subcarpetas para descomprimir
                If RowParam.Item("usaDescomprimir_SubCarpeta") = True Then
                    DescomprimirSubCarpeta(dbmImaging, NuevaCarpeta.Trim, RowParam, RowCarpetas)
                End If
                For i As Integer = 0 To ArchivosaCargarDatatable.Rows.Count - 1
                    ArchivosaCargarDatatable.Rows(i).Item("Ruta") = ""
                    ArchivosaCargarDatatable.Rows(i).Item("Archivo") = ""
                    ArchivosaCargarDatatable.Rows(i).Item("Id_Cargue") = 0
                Next
                'Validamos si existen los archivos configurados para el cargue automatico
                Dim Imagenes As New List(Of String)
                Try
                    Dim SiEsta As Boolean = False
                    For Each archivo In Directory.EnumerateFiles(NuevaCarpeta, "*.*", SearchOption.AllDirectories)
                        Dim nombre As String = Path.GetFileName(archivo)
                        SiEsta = False
                        For i As Integer = 0 To ArchivosaCargarDatatable.Rows.Count - 1
                            '            Dim prefijo As String = ArchivosaCargarDatatable.Rows(i).Item("estructura_Nombre_Archivo").ToString()
                            '            Dim extension As String = ArchivosaCargarDatatable.Rows(i).Item("extension_Archivo").ToString()
                            '            If nombre.StartsWith(prefijo, StringComparison.OrdinalIgnoreCase) AndAlso
                            '                 nombre.EndsWith(extension, StringComparison.OrdinalIgnoreCase) Then
                            '                Imagenes.Add(archivo)
                            '                ArchivosaCargarDatatable.Rows(i).Item("Ruta") = archivo
                            '                ArchivosaCargarDatatable.Rows(i).Item("Archivo") = Path.GetFileName(archivo)
                            '                Exit For
                            '            End If
                            Dim patron As New Regex(ArchivosaCargarDatatable.Rows(i).Item("estructura_Nombre_Archivo").ToString(),
                                 RegexOptions.IgnoreCase Or RegexOptions.Compiled)
                            Dim esValido As Boolean = patron.IsMatch(nombre)
                            If esValido Then
                                Imagenes.Add(archivo)
                                ArchivosaCargarDatatable.Rows(i).Item("Ruta") = archivo
                                ArchivosaCargarDatatable.Rows(i).Item("Archivo") = Path.GetFileName(archivo)
                                SiEsta = True
                                Exit For
                            End If
                        Next
                        'Esto es para el archivo que no tiene un nombre fijo, no identificado, se toma el que empieze con ^ Z \ 
                        If SiEsta = False And RowParam.Item("estructura_Nombre_Archivo").ToString().Trim = "*" Then
                            For i As Integer = 0 To ArchivosaCargarDatatable.Rows.Count - 1
                                If Left(ArchivosaCargarDatatable.Rows(i).Item("estructura_Nombre_Archivo").ToString().Trim, 5) = "^ Z \" Then
                                    Imagenes.Add(archivo)
                                    ArchivosaCargarDatatable.Rows(i).Item("Ruta") = archivo
                                    ArchivosaCargarDatatable.Rows(i).Item("Archivo") = Path.GetFileName(archivo)
                                    Exit For
                                End If
                            Next
                        End If
                    Next
                Catch ex As Exception
                    ErrorMsg = "04- No se encontraron archvios en la ruta = " & NuevaCarpeta
                    WriteErrorLog(ErrorMsg)
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    Continue For
                End Try

                'CREAR CONTENEDOR (NUMERO DE LOTE)
                If RowCarpetas.Item("contenedor").ToString().Trim <> "" Then
                    nContenedor = RowCarpetas.Item("contenedor").ToString().Trim
                Else
                    nContenedor = Format(Val(RowParam.Item("Oficina")), "0000") & "-" & Format(Val(nEsquema), "00") & "-" & Format(RowCarpetas.Item("fecha_Cargue"), "yyyyMMdd") & Format(Now(), "HHmmss") & Hilo.ToString.Trim

                    RowCarpetas.Item("contenedor") = nContenedor

                    dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpetas.Item("fk_Cargue_Automatico")), CInt(RowCarpetas.Item("id_Archivo")), 0, 0, 2, 0, nContenedor.ToString())

                    Try
                        'Registramos el lote creado
                        Dim RespDT = dbmImaging.SchemaProcess.PA_ControlLoteDitalizado_set.DBExecute(CInt(nEntidad), CInt(nProyecto), CInt(nEsquema), Trim(RowParam.Item("Nombre_Esquema")), Trim(nContenedor), CDate(Format(RowCarpetas.Item("fecha_Cargue"), "yyyy/MM/dd")), CDate(RowCarpetas.Item("fecha_Cargue")), CDate(RowCarpetas.Item("fecha_Cargue")), CInt(RowParam.Item("Oficina")), Trim(ParametrosDataTable.Rows(0).Item("Nombre_Centro_Procesamiento")), Trim(NLabel), "Normal", 3, Trim(nObservaciones), Trim(nUsuario), "ABIERTO", CDate(RowCarpetas.Item("fecha_Cargue")), CBool(0).ToString)
                    Catch ex As Exception
                        ErrorMsg = "05- Error al crear lote. " & ex.Message
                        dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpetas.Item("fk_Cargue_Automatico")), CInt(RowCarpetas.Item("id_Archivo")), 0, 0, 1, 0, ErrorMsg)
                        WriteErrorLog(ErrorMsg)
                        Continue For
                    End Try
                End If
                Dim nPaqueteName As String = nContenedor
                Dim nFolder As String = nContenedor
                Dim ExtImgSalida As String = ".pdf" 'RowParam.Item("extension_Salida").ToString().Trim
                Dim _OutputFolder As String = ""
                'Proceso de cargue de archivos encontrados
                Dim IdCargue As Integer
                Dim fk_Precinto As Short
                Dim id_Contenedor As Short
                Try
                    If (Imagenes.Count = 0) Then
                        'Marcar error en base de datos
                        ErrorMsg = "06- No se encontraron imagenes para cargar en el directorio: " + NuevaCarpeta
                        dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpetas.Item("fk_Cargue_Automatico")), CInt(RowCarpetas.Item("id_Archivo")), 0, 0, 1, 0, ErrorMsg)
                        WriteErrorLog(ErrorMsg)
                        Continue For
                    Else
                        _OutputFolder = NuevaCarpeta
                    End If
                    If Not (CentroProcesamientoDataTable.Count > 0) Then
                        ErrorMsg = "07- No se encontró una Sede y Centro de Procesamiento asignados para el cargue"
                        dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpetas.Item("fk_Cargue_Automatico")), CInt(RowCarpetas.Item("id_Archivo")), 0, 0, 1, 0, ErrorMsg)
                        WriteErrorLog(ErrorMsg)
                        Continue For
                    End If
                    Dim nSedeProcesamiento As Short = CentroProcesamientoDataTable(0).fk_Sede_Asignada
                    Dim nCentroProcesamiento As Short = CentroProcesamientoDataTable(0).fk_Centro_Procesamiento_Asignado
                    Dim FechaProceso = DateTime.Now
                    ServidorDataTable(0).ConnectionString_Servidor = ServidorDataTable(0).ConnectionString_Servidor & _DataRemoting

                    CrearFechaProceso(dbmImaging, nEntidadProcesamiento, nEntidad, nProyecto, FechaProceso, nUser)
                    CrearOT(dbmImaging, nEntidadProcesamiento, nEntidad, nProyecto, FechaProceso, nUser, nSedeProcesamiento, nCentroProcesamiento, CShort(1))
                    'Crear Destape
                    Dim DestapeDataTable = dbmImaging.SchemaProcess.PA_Crear_Precinto_Contenedor.DBExecute(nEntidad, nProyecto, nEsquema, Me.NewOT, nContenedor, nCampos, "|", ";", nUser)
                    If (CShort(DestapeDataTable.Rows(0)("fk_Precinto").ToString()) = 0) Then
                        If (CShort(DestapeDataTable.Rows(0)("fk_Contenedor").ToString()) = 0) Then
                            ErrorMsg = "08- El precinto y contenedor ya fueron destapados para la OT " & Me.NewOT & " y Fecha de Proceso con contenedor " & nContenedor

                            dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpetas.Item("fk_Cargue_Automatico")), CInt(RowCarpetas.Item("id_Archivo")), 0, 0, 1, 0, ErrorMsg)

                            WriteErrorLog(ErrorMsg)
                            Throw New Exception(ErrorMsg)
                        End If
                    End If
                    fk_Precinto = CShort(DestapeDataTable.Rows(0)("fk_Precinto").ToString())
                    id_Contenedor = CShort(DestapeDataTable.Rows(0)("fk_Contenedor").ToString())

                    ' Crear el cargue
                    CargueType.fk_Entidad = nEntidad
                    CargueType.fk_Proyecto = nProyecto
                    CargueType.fk_Estado = EstadoEnum.Creado
                    CargueType.fk_Entidad_Procesamiento = nEntidadProcesamiento
                    CargueType.fk_Sede_Procesamiento_Cargue = nSedeProcesamiento_Cargue
                    CargueType.fk_Centro_Procesamiento_Cargue = nCentroProcesamiento_Cargue
                    CargueType.fk_Entidad_Servidor = ServidorDataTable(0).fk_Entidad
                    CargueType.fk_Servidor = ServidorDataTable(0).id_Servidor
                    CargueType.fk_OT = Me.NewOT
                    CargueType.Fecha_Proceso = FechaProceso
                    CargueType.fk_Usuario_Log = nUser
                    CargueType.Observaciones = nObservaciones
                    IdCargue = dbmImaging.SchemaProcess.PA_Guardar_TBL_Cargue.DBExecute(
                       CargueType.fk_Entidad,
                       CargueType.fk_Proyecto,
                       CargueType.fk_Estado,
                       CargueType.fk_Entidad_Procesamiento,
                       CargueType.fk_Sede_Procesamiento_Cargue,
                       CargueType.fk_Centro_Procesamiento_Cargue,
                       CargueType.fk_Entidad_Servidor,
                       CargueType.fk_Servidor,
                       CargueType.fk_OT,
                       CargueType.Fecha_Proceso,
                       CargueType.Observaciones,
                       CargueType.fk_Usuario_Log)

                    ' Crear el paquete
                    Dim PaqueteType = New TBL_Cargue_PaqueteType()
                    PaqueteType.fk_Cargue = IdCargue
                    PaqueteType.id_Cargue_Paquete = CShort(1)
                    PaqueteType.fk_Estado = nEstado
                    PaqueteType.fk_Usuario_Log = nUser
                    PaqueteType.Fecha_Proceso = SlygNullable.SysDate
                    PaqueteType.Path_Cargue_Paquete = nPaqueteName
                    PaqueteType.Data_Path = nPaqueteName '& nPaqueteName
                    PaqueteType.fk_Sede_Procesamiento_Asignada = nSedeProcesamiento
                    PaqueteType.fk_Centro_Procesamiento_Asignado = nCentroProcesamiento

                    dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBInsert(PaqueteType)

                    ' Crear los items
                    Dim idArchivo As Integer = 0
                    For i As Integer = 0 To ArchivosaCargarDatatable.Rows.Count - 1
                        If Trim(ArchivosaCargarDatatable.Rows(i).Item("Ruta")) = "" Then
                            Continue For
                        End If
                        Dim ArchivoImg As String = ArchivosaCargarDatatable.Rows(i).Item("Ruta")
                        Dim PathArchivoImg As String = Path.GetDirectoryName(ArchivoImg)
                        Dim FormatoAux As String = Path.GetExtension(ArchivoImg).Replace(".", "")
                        idArchivo += 1
                        ArchivosaCargarDatatable.Rows(i).Item("Id_Cargue") = idArchivo
                        Dim CargueItemType = New TBL_Cargue_ItemType()
                        Dim infoArchivo As New FileInfo(ArchivoImg)

                        CargueItemType.fk_Cargue = IdCargue
                        CargueItemType.fk_Cargue_Paquete = PaqueteType.id_Cargue_Paquete
                        CargueItemType.id_Cargue_Item = idArchivo
                        CargueItemType.Folios_Cargue_Item = CShort(1)
                        CargueItemType.Tamaño_Cargue_Item = infoArchivo.Length
                        'If ArchivosaCargarDatatable.Rows(i).Item("usa_Cambio_Nombre") = 1 Then
                        '    CargueItemType.Path_Cargue_Item = Trim(ArchivosaCargarDatatable.Rows(i).Item("nuevo_Nombre_Archivo")) & "." & Trim(FormatoAux)
                        '    CargueItemType.Key_Cargue_Item = Trim(ArchivosaCargarDatatable.Rows(i).Item("nuevo_Nombre_Archivo"))
                        'Else
                        CargueItemType.Path_Cargue_Item = Path.GetFileName(ArchivoImg)
                        CargueItemType.Key_Cargue_Item = Path.GetFileNameWithoutExtension(CargueItemType.Path_Cargue_Item)
                        'End If
                        CargueItemType.fk_Estado = nEstado
                        CargueItemType.fk_Usuario_Log = nUser
                        CargueItemType.Bloqueado = False
                        dbmImaging.SchemaProcess.TBL_Cargue_Item.DBInsert(CargueItemType)

                        manager.CreateItem(CInt(CargueItemType.fk_Cargue), CInt(CargueItemType.fk_Cargue_Paquete), CInt(CargueItemType.id_Cargue_Item))
                        If Not FormatoAux.ToUpper = "PDF" Then
                            Using Archivo As New FileStream(Imagenes(i), FileMode.Open, FileAccess.Read)
                                Dim Data(CInt(Archivo.Length - 1)) As Byte
                                Archivo.Read(Data, 0, Data.Length)

                                Dim DataThumbnailMemoryStream As New MemoryStream()
                                ImageManager.GetThumbnail(New Bitmap(Image.FromStream(New MemoryStream(Data))), MaxThumbnailWidth, MaxThumbnailHeight).Save(DataThumbnailMemoryStream, Imaging.ImageFormat.Jpeg)

                                manager.CreateFolio(CargueItemType.fk_Cargue, CargueItemType.fk_Cargue_Paquete, CargueItemType.id_Cargue_Item, CShort(1), Data, DataThumbnailMemoryStream.GetBuffer(), True)

                                Archivo.Close()
                                Archivo.Dispose()
                            End Using
                        Else
                            Dim pdfPathUnix = ArchivoImg.Replace("\", "/")
                            Dim vInfo = GsNativeLoader.GetGsVersionInfo64(AppDomain.CurrentDomain.BaseDirectory)
                            Using Rasterizer As New Ghostscript.NET.Rasterizer.GhostscriptRasterizer()
                                Rasterizer.CustomSwitches.Add("-dNOSAFER")
                                Try
                                    Rasterizer.Open(pdfPathUnix, vInfo, False)
                                    ' Rasterizer.Open(pdfPathUnix)
                                Catch ex As Exception
                                    WriteErrorLog(ex.Message)
                                    Throw
                                End Try
                                Try
                                    For folio As Integer = 1 To Rasterizer.PageCount
                                        Dim paginaImg As Bitmap = Rasterizer.GetPage(300, folio)
                                        If paginaImg Is Nothing Then
                                            paginaImg = Rasterizer.GetPage(200, folio)
                                        End If
                                        If paginaImg Is Nothing Then
                                            ErrorMsg = "Lote:" & nPaqueteName & " no fue posible extraer imagenes del pdf."
                                            dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpetas.Item("fk_Cargue_Automatico")), CInt(RowCarpetas.Item("id_Archivo")), 0, 0, 1, 0, ErrorMsg)
                                            WriteErrorLog(ErrorMsg)
                                            Throw New Exception(ErrorMsg)
                                        End If
                                        Dim DataImg As Byte()
                                        Using ms As New MemoryStream()
                                            paginaImg.Save(ms, ImageFormat.Jpeg)
                                            ' DataImg = ms.ToArray()
                                            DataImg = ImageToGrayscaleJpegBytes(paginaImg, 90) ' 90 = buena calidad
                                        End Using

                                        Dim DataThumbnailMemoryStream As New MemoryStream()
                                        Using thumb = ImageManager.GetThumbnail(paginaImg, MaxThumbnailWidth, MaxThumbnailHeight)
                                            thumb.Save(DataThumbnailMemoryStream, Imaging.ImageFormat.Jpeg)
                                        End Using
                                        manager.CreateFolio(CargueItemType.fk_Cargue, CargueItemType.fk_Cargue_Paquete, CargueItemType.id_Cargue_Item, folio, DataImg, DataThumbnailMemoryStream.GetBuffer(), True)
                                        Dim FolioType = New DBImaging.SchemaProcess.TBL_Cargue_FolioType()
                                        FolioType.fk_Cargue = CargueItemType.fk_Cargue
                                        FolioType.fk_Cargue_Paquete = CargueItemType.fk_Cargue_Paquete
                                        FolioType.fk_Cargue_Item = CargueItemType.id_Cargue_Item
                                        FolioType.id_Folio = folio
                                        FolioType.Indexado = False
                                        dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBInsert(FolioType)

                                        paginaImg.Dispose()
                                    Next
                                Catch ex As Exception
                                    ErrorMsg = "09- Lote:" & nPaqueteName & " no fue posible extraer imagenes del pdf."
                                    dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpetas.Item("fk_Cargue_Automatico")), CInt(RowCarpetas.Item("id_Archivo")), 0, 0, 1, 0, ErrorMsg)

                                    WriteErrorLog(ErrorMsg)
                                    Throw New Exception(ErrorMsg)
                                End Try
                            End Using
                        End If
                    Next
                    'Actualizar Contenedor
                    ContenedorType.fk_Cargue = IdCargue
                    ContenedorType.fk_Paquete = CShort(1)
                    ContenedorType.fk_Estado = 10
                    dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(ContenedorType, Me.NewOT, fk_Precinto, id_Contenedor)

                    ' Borrar los archivos                        
                    Directory.Delete(_OutputFolder, True)

                    nCargueID = IdCargue
                    nFolios = CShort(Imagenes.Count)
                    '---------------------------------------------------------------------------
                    ' Actualizar Dashboard
                    '---------------------------------------------------------------------------
                    Dim DashboardPaquetesType = New TBL_Dashboard_PaquetesType()
                    DashboardPaquetesType.fk_Cargue = IdCargue 'CargueType.id_Cargue
                    DashboardPaquetesType.fk_Cargue_Paquete = PaqueteType.id_Cargue_Paquete
                    DashboardPaquetesType.fk_Entidad_Procesamiento = CargueType.fk_Entidad_Procesamiento
                    DashboardPaquetesType.fk_Sede_Procesamiento = PaqueteType.fk_Sede_Procesamiento_Asignada
                    DashboardPaquetesType.fk_Centro_Procesamiento = PaqueteType.fk_Centro_Procesamiento_Asignado
                    DashboardPaquetesType.fk_Entidad = CargueType.fk_Entidad
                    DashboardPaquetesType.fk_Proyecto = CargueType.fk_Proyecto
                    DashboardPaquetesType.fk_OT = CargueType.fk_OT
                    dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBInsert(DashboardPaquetesType)

                    '---------------------------------------------------------------------------
                    'MARCAR CARGUE OK E INDEXACION CREAR DOS CAMPOS.
                    CargueLote = New DBImaging.SchemaProcess.TBL_Lote_DigitalizacionType() With {
                           .Cargado = CBool(1)} 'Indexación Automatica
                    dbmImaging.SchemaProcess.TBL_Lote_Digitalizacion.DBUpdate(CargueLote, nPaqueteName)
                    If RowParam.Item("cargue_Directo") = 1 Then
                        If EnviarIndexacion(RowParam, RowCarpetas, ArchivosaCargarDatatable, nEntidad, nProyecto, IdCargue, PaqueteType.id_Cargue_Paquete, nEntidadProcesamiento, FechaProceso, nUser, nSedeProcesamiento, nCentroProcesamiento, nEsquema, Me.NewOT, ExtImgSalida, EsquemaDataTable, nEstado, nPaqueteName, nFolios) = False Then

                            ErrorMsg = "10- Lote:" & nPaqueteName & " no fue indexado."
                            dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpetas.Item("fk_Cargue_Automatico")), CInt(RowCarpetas.Item("id_Archivo")), 0, 0, 1, 0, ErrorMsg)
                            WriteErrorLog(ErrorMsg)
                            Throw New Exception(ErrorMsg)
                        End If
                    Else
                        ' Actualizar el estado del cargue
                        CargueType = New DBImaging.SchemaProcess.TBL_CargueType()
                        CargueType.fk_Estado = 29 'Pendiente Indexación
                        dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(CargueType, IdCargue)
                    End If
                    'Registramos el lote creado
                    Dim RespDT = dbmImaging.SchemaProcess.PA_ControlLoteEstado_set.DBExecute(nEntidad, nProyecto, nContenedor, Format(RowCarpetas.Item("fecha_Cargue"), "yyyy/MM/dd"), 3, "TRANSFERIDO", CBool(0).ToString)

                    dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpetas.Item("fk_Cargue_Automatico")), CInt(RowCarpetas.Item("id_Archivo")), 0, 1, 0, 0, "")

                Catch ex As Exception
                    If ((fk_Precinto <> 0) And (id_Contenedor <> 0)) Then
                        dbmImaging.SchemaProcess.TBL_Contenedor.DBDelete(Me.NewOT, fk_Precinto, id_Contenedor)
                        dbmImaging.SchemaProcess.TBL_Precinto.DBDelete(Me.NewOT, fk_Precinto)
                    End If

                    'Elimina el cargue realizado
                    If Not IsNothing(IdCargue) Then
                        dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBDelete(IdCargue, Nothing)
                        dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBDelete(IdCargue, Nothing, Nothing, Nothing)
                        dbmImaging.SchemaProcess.TBL_Cargue_Item.DBDelete(IdCargue, Nothing, Nothing)
                        dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBDelete(IdCargue, Nothing)
                        dbmImaging.SchemaProcess.TBL_Cargue.DBDelete(IdCargue)
                        manager.DeleteCargue(IdCargue)
                    End If
                    ErrorMsg = "12- Error General. " & ex.Message & " Cargue " & IdCargue
                    dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpetas.Item("fk_Cargue_Automatico")), CInt(RowCarpetas.Item("id_Archivo")), 0, 0, 1, 0, ErrorMsg)
                    WriteErrorLog(ErrorMsg)
                End Try
            Next
            If TotCarpetas > 0 Then
                'Notificación cargue exitoso
                If EsquemaDataTable(0).UsaNotificacionCargue Then
                    Dim NotificacionListasDataTable = dbmCore.SchemaConfig.TBL_Notificacion_Lista.DBFindByfk_Notificacion(EsquemaDataTable(0).fk_Notificacion)
                    If NotificacionListasDataTable.Count > 0 Then
                        Dim CorreoDatatable = dbmCore.SchemaConfig.PA_Busqueda_Parametros_Correo.DBExecute(EsquemaDataTable(0).fk_Notificacion, NotificacionListasDataTable(0).id_Notificacion_Lista)
                        If CorreoDatatable.Count > 0 Then
                            Dim Message As String = ""
                            Dim MailTo As String = ""
                            Dim MailCC As String = ""
                            Dim MailCCO As String = ""
                            Dim Subject As String = ""
                            Dim nAttachName As String = ""
                            Dim nAttach As Byte() = Nothing
                            Dim CuerpoCorreo As String = ""
                            Dim Novedades As String = ""

                            Dim EntidadProyectoEsquemaDataTable = dbmCore.SchemaConfig.CTA_Entidad_Proyecto_Esquema.DBFindByid_Entidadid_Proyectoid_Esquema(nEntidad, nProyecto, nEsquema)

                            CuerpoCorreo = CorreoDatatable(0).CUERPO.Replace("@Proyecto", EntidadProyectoEsquemaDataTable(0).Nombre_Entidad & "-" & EntidadProyectoEsquemaDataTable(0).Nombre_Proyecto)
                            CuerpoCorreo = CuerpoCorreo.Replace("@Esquema", EsquemaDataTable(0).Nombre_Esquema.ToString())
                            CuerpoCorreo = CuerpoCorreo.Replace("@Cargue", "XX")  ' IdCargue.ToString())
                            CuerpoCorreo = CuerpoCorreo.Replace("@FechaProceso", Me.FechaProcesoInt.ToString())
                            CuerpoCorreo = CuerpoCorreo.Replace("@OT", Me.NewOT.ToString())

                            Message = CorreoDatatable(0).SALUDO & CuerpoCorreo & CorreoDatatable(0).FIRMA
                            MailTo = CorreoDatatable(0).CORREOS
                            Subject = CorreoDatatable(0).ASUNTO

                            Dim DBMTools As DBTools.DBToolsDataBaseManager = Nothing
                            Try
                                DBMTools = New DBTools.DBToolsDataBaseManager(Program.ConnectionStrings.Tools)
                                DBMTools.Connection_Open()

                                DBMTools.SchemaMail.PA_Basic_TBL_Queue_insert_Attach.DBExecute(nEntidad, 1, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)
                            Catch ex As Exception
                                ErrorMsg = "11- Error enviando la notificación de cargue exitoso. " & ex.Message.ToString()
                                WriteErrorLog(ErrorMsg)
                            Finally
                                DBMTools.Connection_Close()
                            End Try
                        End If
                    End If
                End If
            End If

            Try
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            Catch : End Try

        End Sub

        Public Function ImageToGrayscaleJpegBytes(img As Image, calidad As Long) As Byte()
            ' calidad: 0–100 (100 = máxima calidad; archivos más grandes)
            If calidad < 0 OrElse calidad > 100 Then calidad = 90

            ' Matriz de color para escala de grises (luminancia)
            Dim cm As New ColorMatrix(New Single()() {
        New Single() {0.299F, 0.299F, 0.299F, 0, 0},
        New Single() {0.587F, 0.587F, 0.587F, 0, 0},
        New Single() {0.114F, 0.114F, 0.114F, 0, 0},
        New Single() {0, 0, 0, 1, 0},
        New Single() {0, 0, 0, 0, 1}
    })

            Dim ia As New ImageAttributes()
            ia.SetColorMatrix(cm)

            ' Crear un bitmap destino con el mismo tamaño que la imagen original
            Using bmp As New Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb)
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                    g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

                    Dim rect = New Rectangle(0, 0, bmp.Width, bmp.Height)
                    ' Dibuja aplicando la matriz de grises
                    g.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia)
                End Using

                ' Guardar a JPEG con calidad controlada
                Dim jpegCodec = ImageCodecInfo.GetImageEncoders().First(Function(c) c.MimeType = "image/jpeg")
                Dim encParams As New EncoderParameters(1)
                encParams.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, calidad)

                Using ms As New MemoryStream()
                    bmp.Save(ms, jpegCodec, encParams)
                    Return ms.ToArray()
                End Using
            End Using
        End Function


        Private Function EnviarIndexacion(RowParametros As DataRow, RowCarpetas As DataRow, DtArchivos As DataTable, nEntidad As Short, nProyecto As Short, fk_Cargue As Integer, fk_Cargue_Paquete As Short, nEntidadProcesamiento As Short, FechaProceso As Date, nUser As Integer, nSedeProcesamiento As Short, nCentroProcesamiento As Short, nEsquema As Short, fk_Ot As Integer, ExtImgSalida As String, nEsquemaDataTable As DBCore.SchemaConfig.TBL_EsquemaDataTable, nEstado As Short, nPaqueteName As String, nFolios As Integer) As Boolean

            Dim nManager As FileProviderManager = Nothing
            Dim sqlAnexoItem = New StringBuilder()
            Dim sqlAnexoFolioItem = New StringBuilder()
            Dim sqlFolderItem = New StringBuilder()
            Dim sqlLlaveItem = New StringBuilder()
            Dim sqlDocumentoItem = New StringBuilder()
            Dim sqlFolioItem = New StringBuilder()

            Dim dbmImagingIdx As New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
            dbmImagingIdx.Connection_Open(1)
            Try
                dbmImagingIdx.Transaction_Begin()
                Dim servidor = dbmImagingIdx.SchemaCore.CTA_Servidor.DBFindByfk_Entidad(1)(0).ToCTA_ServidorSimpleType()
                Dim CentroPro = New DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType()

                'Dim nManager As FileProviderManager = Nothing
                nManager = New FileProviderManager(servidor, CentroPro, dbmImagingIdx, nUser)
                nManager.Connect()

                Dim DtLoteIndexar = dbmImagingIdx.SchemaProcess.PA_Cargue_a_Indexar_Get.DBExecute(fk_Cargue, fk_Cargue_Paquete, 0)

                Dim tFolio As Integer = 0
                Dim tDcto As Integer = 0
                Dim tFolder As Integer = 1

                sqlFolderItem.AppendLine($"INSERT INTO #FolderItem (id_FolderItem, fk_Esquema, fk_Documento_Anexo) " & $"VALUES ({tFolder}, {nEsquema}, {0});")

                For i As Integer = 0 To DtLoteIndexar.Rows.Count - 1
                    nEsquema = RowParametros.Item("fk_Esquema")
                    Dim row As DataRow = DtLoteIndexar.Rows(i)
                    'ArchivosaCargarDatatable.Rows(i).Item("Id_Cargue")
                    If tFolio <> CInt(DtLoteIndexar.Rows(i).Item("id_Cargue_Item")) Then
                        tFolio = CInt(DtLoteIndexar.Rows(i).Item("id_Cargue_Item"))

                        Dim Documento As Integer = DtArchivos.AsEnumerable().
                            Where(Function(r) r.Field(Of Integer)("Id_Cargue") = tFolio).
                            Select(Function(r) r.Field(Of Integer)("fk_Documento")).
                               FirstOrDefault()

                        Dim filasFiltradas() As DataRow = DtLoteIndexar.Select("id_Cargue_Item = " & tFolio)
                        Dim countFoliosDocumento As Integer = filasFiltradas.Length

                        'sqlFolderItem.AppendLine($"INSERT INTO #FolderItem (id_FolderItem, fk_Esquema, fk_Documento_Anexo) " & $"VALUES ({tFolder}, {nEsquema}, {0});")

                        sqlDocumentoItem.AppendLine($"INSERT INTO #DocumentoItem (fk_FolderItem, id_DocumentoItem, fk_Documento, Folios) " & $"VALUES ({tFolder}, {tFolio}, {Documento}, {countFoliosDocumento});")

                    End If
                    Dim fkCargueItem As Integer = DtLoteIndexar.Rows(i).Item("id_Cargue_Item")
                    Dim fkItemFolio As Integer = DtLoteIndexar.Rows(i).Item("id_Folio")

                    sqlFolioItem.AppendLine($"INSERT INTO #FolioItem (fk_FolderItem, fk_DocumentoItem, id_FolioItem, fk_Cargue_Item, fk_Item_Folio) " & $"VALUES ({tFolder}, {tFolio}, {tFolio}, {fkCargueItem}, {fkItemFolio});")
                Next

                Dim Cargue_Item = dbmImagingIdx.SchemaProcess.TBL_Cargue_Item.DBFindByfk_Carguefk_Cargue_Paquete(fk_Cargue, fk_Cargue_Paquete).[Select](Function(e) e.Path_Cargue_Item).FirstOrDefault()

                Dim GuardarNombreImagen As Boolean = True
                Dim EnTransferencia As Boolean = False
                Dim IsSofTrac As Boolean = False

                Dim ResultadoIndexar = dbmImagingIdx.SchemaProcess.PA_Publicar_Indexacion.DBExecute(
                                fk_Ot,
                                fk_Cargue,
                                fk_Cargue_Paquete,
                                nEntidadProcesamiento,
                                nSedeProcesamiento,
                                nCentroProcesamiento,
                                nEntidad,
                                nProyecto,
                                nUser,
                                nEntidad,
                                CShort(servidor.id_Servidor),
                                EnTransferencia,
                                nEntidadProcesamiento,
                                CShort(servidor.id_Servidor),
                                ExtImgSalida,
                                sqlAnexoItem.ToString(),
                                sqlAnexoFolioItem.ToString(),
                                sqlFolderItem.ToString(),
                                sqlLlaveItem.ToString(),
                                sqlDocumentoItem.ToString(),
                                sqlFolioItem.ToString(),
                                IsSofTrac,
                                GuardarNombreImagen)

                If ResultadoIndexar Is Nothing OrElse ResultadoIndexar.Rows.Count = 0 Then Throw New InvalidOperationException("PA_Publicar_Indexacion no devolvió resultados.")

                Dim lastFolderItem As Integer? = Nothing
                Dim lastDocumentoItem As Integer? = Nothing

                ''*******Pasar datos destape a captura*****
                'If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campos_Captura_Destape Then
                dbmImagingIdx.SchemaProcess.PA_Insertar_Datos_Captura_Destape.DBExecute(fk_Cargue, fk_Cargue_Paquete)
                'End If

                Dim DtImg As New DataTable
                '[fk_Cargue], [fk_Cargue_Paquete], [id_Cargue_Item],[Path_Cargue_Item],[Key_Cargue_Item]
                For i = 0 To DtLoteIndexar.Rows.Count - 1
                    Dim CargueItem As Integer = CInt(DtLoteIndexar.Rows(i).Item("id_Cargue_Item"))
                    DtImg = dbmImagingIdx.SchemaProcess.PA_Cargue_Imagenes_a_Indexar_Get.DBExecute(CInt(DtLoteIndexar.Rows(i).Item("fk_Cargue")), CInt(DtLoteIndexar.Rows(i).Item("fk_Cargue_Paquete")), CInt(DtLoteIndexar.Rows(i).Item("id_Cargue_Item")), CInt(DtLoteIndexar.Rows(i).Item("id_Folio")))

                    ' Actualizar estado folio cargue
                    dbmImagingIdx.SchemaProcess.PA_Set_Cargue_Folio_Indexado.DBExecute(fk_Cargue, fk_Cargue_Paquete, CInt(DtLoteIndexar.Rows(i).Item("id_Cargue_Item")), CInt(ResultadoIndexar.Rows(i).Item("id_Folio_item")))

                    Dim filaFiltro As DBImaging.SchemaProcess.TBL_Publicar_IndexacionRow
                    ' Si es un File
                    filaFiltro = ResultadoIndexar.FindByEs_Anexoid_Folder_Itemid_Documento_Itemid_Folio_Item(False, CShort(ResultadoIndexar(i).Item("id_folder_item")), CShort(ResultadoIndexar(i).Item("id_Documento_Item")), CShort(ResultadoIndexar(i).Item("id_Folio_item")))

                    nManager.CreateItem(filaFiltro.fk_Expediente, filaFiltro.fk_Folder, filaFiltro.fk_File, filaFiltro.fk_Version, ExtImgSalida, Guid.NewGuid())

                    Dim DataImg() As Byte = CType(DtImg.Rows(0).Item("image_binary"), Byte())
                    Dim DataThumbnail() As Byte = CType(DtImg.Rows(0).Item("Thumbnail_Binary"), Byte())

                    nManager.CreateFolio(filaFiltro.fk_Expediente, filaFiltro.fk_Folder, filaFiltro.fk_File, filaFiltro.fk_Version, filaFiltro.id_File_Record_Folio, DataImg, DataThumbnail, False)

                    ' Si es un Anexo
                    'filaFiltro = ResultadoIndexar.FindByEs_Anexoid_Folder_Itemid_Documento_Itemid_Folio_Item(True, 0, CShort(ResultadoIndexar(i).Item("id_folder_item")), CShort(ResultadoIndexar(i).Item("id_Folio_item")))
                    'If (filaFiltro IsNot Nothing) Then
                    '    manager.CreateItem(filaFiltro.fk_Anexo, ExtImgSalida)
                    '    manager.CreateFolio(filaFiltro.fk_Anexo, filaFiltro.id_File_Record_Folio, DataImg, DataThumbnail, False)
                    'End If
                Next
                Dim carguePaqueteDataTable = dbmImagingIdx.SchemaProcess.PA_Get_Cargue_Folio_Indexado.DBExecute(fk_Cargue, fk_Cargue_Paquete, False)

                If (carguePaqueteDataTable.Count = 0) Then
                    dbmImagingIdx.SchemaProcess.TBL_Dashboard_Paquetes.DBDelete(fk_Cargue, fk_Cargue_Paquete)

                    Dim updatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                    updatePaquete.fk_Estado = DBCore.EstadoEnum.Indexado

                    dbmImagingIdx.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(updatePaquete, fk_Cargue, fk_Cargue_Paquete)
                    dbmImagingIdx.SchemaProcess.TBL_Cargue_Folio.DBDelete(fk_Cargue, fk_Cargue_Paquete, Nothing, Nothing)

                    Dim cargueDataTable = dbmImagingIdx.SchemaProcess.TBL_Cargue_Paquete.DBFindByfk_Carguefk_Estado(fk_Cargue, DBCore.EstadoEnum.Indexacion)

                    If (cargueDataTable.Count = 0) Then
                        Dim updateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                        updateCargue.fk_Estado = DBCore.EstadoEnum.Indexado
                        dbmImagingIdx.SchemaProcess.TBL_Cargue.DBUpdate(updateCargue, fk_Cargue)
                    End If
                Else
                    '---------------------------------------------------------------------------
                    ' Actualizar Dashboard
                    '---------------------------------------------------------------------------
                    dbmImagingIdx.SchemaProcess.PA_Dashboard_Desbloquear_Paquetes.DBExecute(fk_Cargue, fk_Cargue_Paquete)
                    '---------------------------------------------------------------------------
                End If
                For i = 0 To DtLoteIndexar.Rows.Count - 1
                    nManager.DeleteItem(fk_Cargue, fk_Cargue_Paquete, CShort(DtLoteIndexar.Rows(i).Item("id_Cargue_item")))
                Next

                CargueLote = New DBImaging.SchemaProcess.TBL_Lote_DigitalizacionType() With {
                            .Indexado = CBool(1)} 'Indexación Automatica
                dbmImagingIdx.SchemaProcess.TBL_Lote_Digitalizacion.DBUpdate(CargueLote, nPaqueteName)

                dbmImagingIdx.Transaction_Commit()

                '*******************************************************************************************

                Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)
                dbmCore.Connection_Open(1)
                Try
                    dbmCore.Transaction_Begin()
                    'Buscar y guardar campos extraidos del nombre de la imagen
                    Dim File As Integer = 0
                    Dim CamposaCargarDatatable As New DataTable
                    For Each Fila As DataRow In ResultadoIndexar.Rows
                        If File <> Fila.Item("fk_File") Then
                            For Each Archivo As DataRow In DtArchivos.Rows
                                If Archivo.Item("id_Cargue") = Fila.Item("fk_File") Then
                                    If Archivo.Item("extraer_Campos_Nombre") = 1 Then
                                        'Buscamos campos a extraer
                                        'Buscar los archivos ya registrados para el cargue automático
                                        CamposaCargarDatatable = dbmCore.SchemaConfig.PA_Consulta_Imagenes_Extraer_Campos.DBExecute(CInt(Archivo.Item("fk_cargue_Automatico")), CInt(Archivo.Item("id_Archivo_Cargue")))
                                        If CamposaCargarDatatable IsNot Nothing AndAlso CamposaCargarDatatable.Rows.Count > 0 Then
                                            For Each Campo As DataRow In CamposaCargarDatatable.Rows
                                                Dim ResultadoCampo As String = Nothing
                                                Dim EsCampo As Boolean = TryObtenerCampoConPatron(Archivo.Item("Archivo").trim, Archivo.Item("estructura_Nombre_Archivo").trim, CInt(Campo.Item("numero_Campo_en_Nombre")), ResultadoCampo, RegexOptions.None, 0)
                                                If EsCampo Then
                                                    If Campo.Item("fk_Campo_Tipo") = 3 Then   'Campo fecha.
                                                        Dim fecha As Date = DateTime.ParseExact(ResultadoCampo, "ddMMyyyy", Nothing)
                                                        ResultadoCampo = fecha.ToString("dd/MM/yyyy")
                                                    End If
                                                    'ResultadoCampo}
                                                    Dim DtRes = dbmCore.SchemaProcess.PA_Insert_TBL_File_Data_Cargue_Automatico.DBExecute(CLng(Fila.Item("fk_Expediente")), CShort(Fila.Item("fk_Folder")), CShort(Fila.Item("fk_File")), CShort(Campo.Item("fk_Documento")), CShort(Campo.Item("fk_Campo")), ResultadoCampo.ToString.Trim, CInt(Len(Trim(ResultadoCampo))))
                                                    If DtRes.Rows.Count > 0 Then
                                                        If CInt(DtRes.Rows(0).Item("InsertSuccess")) = 0 Then
                                                            Dim ErrorMsg As String = "Error al insertar el campo extraído del nombre de la imagen. Lote: " & nPaqueteName & " Archivo: " & Archivo.Item("Archivo").trim & " Campo: " & Campo.Item("Nombre_Campo").trim
                                                            WriteErrorLog(ErrorMsg)
                                                            'Throw New Exception(ErrorMsg)
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                End If
                            Next
                            File = Fila.Item("fk_File")
                        End If
                    Next
                    dbmCore.Transaction_Commit()
                Catch ex As Exception
                    dbmCore?.Transaction_Rollback()
                    ErrorMsg = "Error en proceso de indexación. " & ex.Message
                    WriteErrorLog(ErrorMsg)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try
                Return True
            Catch ex As Exception
                dbmImagingIdx?.Transaction_Rollback()
                ErrorMsg = "Error en proceso de indexación. " & ex.Message
                dbmImagingIdx.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpetas.Item("fk_Cargue_Automatico")), CInt(RowCarpetas.Item("id_Archivo")), 0, 0, 1, 0, ErrorMsg)
                WriteErrorLog(ErrorMsg)
                Return False
            Finally
                nManager?.Disconnect()
                'nManager.Disconnect()
                If (dbmImagingIdx IsNot Nothing) Then dbmImagingIdx.Connection_Close()
            End Try
        End Function


        Private ReadOnly DefaultOptions As RegexOptions =
        RegexOptions.Compiled Or RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant
        ''' <summary>
        ''' Intenta obtener el valor del grupo N (1..9) a partir de un nombre de archivo y un patrón específico.
        ''' Devuelve True si pudo extraerlo; el valor queda en 'resultado'.
        ''' </summary>
        ''' <param name="nombreArchivo">Nombre de archivo (solo nombre o FullPath; se usará Path.GetFileName si lo envías completo).</param>
        ''' <param name="patron">Patrón Regex que se usará para este archivo (puede contener grupos con nombre como (?&lt;p1&gt;...)).</param>
        ''' <param name="grupo">Número de grupo a devolver (1..9). Groups(0) es el match completo y no se usa.</param>
        ''' <param name="resultado">Salida con el valor del grupo solicitado si se encuentra.</param>
        ''' <param name="regexOptions">Opcional: opciones de Regex; por defecto usa Compiled + IgnoreCase + CultureInvariant.</param>
        ''' <param name="timeoutMs">Opcional: timeout de evaluación en milisegundos (evita regex muy costosas). 0 = sin timeout explícito.</param>
        Private Function TryObtenerCampoConPatron(nombreArchivo As String,
                                             patron As String,
                                             grupo As Integer,
                                             ByRef resultado As String,
                                             Optional regexOptions As RegexOptions = Nothing,
                                             Optional timeoutMs As Integer = 0) As Boolean
            resultado = Nothing

            If String.IsNullOrWhiteSpace(nombreArchivo) Then Return False
            If String.IsNullOrWhiteSpace(patron) Then Return False
            If grupo < 1 OrElse grupo > 9 Then Return False

            ' Tomar solo el nombre (por si viene la ruta completa)
            Dim nombre As String = System.IO.Path.GetFileName(nombreArchivo)

            ' Preparar Regex con timeout opcional
            Dim rx As Regex
            Dim opts As RegexOptions = If(regexOptions = Nothing, DefaultOptions, regexOptions)

            Try
                If timeoutMs > 0 Then
                    rx = New Regex(patron, opts, TimeSpan.FromMilliseconds(timeoutMs))
                Else
                    rx = New Regex(patron, opts)
                End If
            Catch ex As ArgumentException
                ' Patrón inválido
                Return False
            End Try

            ' Hacer match
            Dim m As Match = rx.Match(nombre)
            If Not m.Success Then Return False

            ' 1) Intentar por índice ordinal (orden de aparición de paréntesis de captura)
            '    Nota: Groups(0) es el match completo; los grupos reales comienzan en 1.
            Dim totalCapturas As Integer = m.Groups.Count - 1
            If grupo <= totalCapturas Then
                Dim val As String = m.Groups(grupo).Value
                If Not String.IsNullOrEmpty(val) Then
                    resultado = val
                    Return True
                End If
            End If

            ' 2) Intentar por nombre de grupo "p{N}" si existe (ej. p1, p2, ..., p9)
            Dim nombreGrupo As String = "p" & grupo.ToString()
            Dim nombres As String() = rx.GetGroupNames()

            If nombres.Contains(nombreGrupo) Then
                Dim val2 As String = m.Groups(nombreGrupo).Value
                If Not String.IsNullOrEmpty(val2) Then
                    resultado = val2
                    Return True
                End If
            End If

            ' 3) Alias opcional para casos conocidos (por ejemplo, grupo 3 llamado 'fecha')
            If grupo = 3 AndAlso nombres.Contains("fecha") Then
                Dim val3 As String = m.Groups("fecha").Value
                If Not String.IsNullOrEmpty(val3) Then
                    resultado = val3
                    Return True
                End If
            End If

            ' Si llegó aquí, el patrón coincidió pero no existe el grupo solicitado
            Return False
        End Function



        Public Sub CrearFechaProceso(ByVal dbmImaging As DBImagingDataBaseManager, ByVal nEntidadProcesamiento As Short, ByVal nEntidadCliente As Short, ByVal nProyecto As Short, ByVal nFechaProceso As Date, ByVal nUser As Integer)

            Dim DatosFechaProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBGet(nEntidadCliente, nProyecto, Integer.Parse(nFechaProceso.ToString("yyyyMMdd")), nEntidadProcesamiento)

            If DatosFechaProcesoDataTable.Count = 0 Then

                FechaProcesoType.fk_Entidad_Procesamiento = nEntidadProcesamiento
                FechaProcesoType.fk_Entidad = nEntidadCliente
                FechaProcesoType.fk_Proyecto = nProyecto
                FechaProcesoType.id_fecha_proceso = Integer.Parse(nFechaProceso.ToString("yyyyMMdd"))
                FechaProcesoType.Fecha_Proceso = nFechaProceso
                FechaProcesoType.fk_Usuario_Apertura = nUser
                FechaProcesoType.Fecha_Apertura = SlygNullable.SysDate
                FechaProcesoType.Cerrado = False

                dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBInsert(FechaProcesoType)
                Me.FechaProcesoInt = FechaProcesoType.id_fecha_proceso.Value
            Else
                Me.FechaProcesoInt = DatosFechaProcesoDataTable(0).id_fecha_proceso
            End If
        End Sub

        Public Sub CrearOT(ByVal dbmImaging As DBImagingDataBaseManager, ByVal nEntidadProcesamiento As Short, ByVal nEntidadCliente As Short, ByVal nProyecto As Short, ByVal nFechaProceso As Date, ByVal nUser As Integer, ByVal nSedeProcesamiento_Cargue As Short, ByVal nCentroProcesamiento_Cargue As Short, ByVal nfk_Tipo_OT As Short)

            Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidad_Procesamientofk_Entidadfk_Proyectofk_fecha_procesofk_OT_TipoCerrado(nEntidadProcesamiento, nEntidadCliente, nProyecto, Integer.Parse(nFechaProceso.ToString("yyyyMMdd")), nfk_Tipo_OT, False)

            If OTDataTable.Count = 0 Then

                dbmImaging.Transaction_Begin()

                OT_Type.fk_Entidad_Procesamiento = nEntidadProcesamiento
                OT_Type.fk_Entidad = nEntidadCliente
                OT_Type.fk_Proyecto = nProyecto
                OT_Type.fk_fecha_proceso = Me.FechaProcesoInt
                OT_Type.fk_OT_Tipo = nfk_Tipo_OT
                OT_Type.fk_Sede_Procesamiento = nSedeProcesamiento_Cargue
                OT_Type.fk_Centro_Procesamiento = nCentroProcesamiento_Cargue
                OT_Type.fk_Usuario_Apertura = nUser
                OT_Type.Fecha_Apertura = SlygNullable.SysDate
                OT_Type.Exportado = False
                OT_Type.Cerrado = False
                OT_Type.id_OT = dbmImaging.SchemaProcess.TBL_OT.DBNextId()

                dbmImaging.SchemaProcess.TBL_OT.DBInsert(OT_Type)

                dbmImaging.Transaction_Commit()

                Me.NewOT = OT_Type.id_OT.Value
            Else
                Me.NewOT = OTDataTable(0).id_OT
            End If

        End Sub
        Private Function DescomprimirArchivo(dbmImaging As DBImagingDataBaseManager, RutaArchivo As String, RowCarpeta As DataRow, RutaDescomprimir As String) As Boolean
            Dim ErrorMsg As String = ""
            Try
                If Not System.IO.File.Exists(RutaArchivo) Then
                    'Marcar error en base de datos
                    ErrorMsg = "Descomprimir - El archivo a descomprimir no existe en la ruta especificada: " & RutaArchivo.ToString()
                    dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpeta.Item("fk_Cargue_Automatico")), CInt(RowCarpeta.Item("id_Archivo")), 0, 0, 1, 0, ErrorMsg)
                    WriteErrorLog(ErrorMsg)
                    Return False
                End If
                ' Carpeta base y nombre del archivo sin extensión
                Dim carpetaBase As String = Path.GetDirectoryName(RutaArchivo)
                Dim nombreArchivo As String = Path.GetFileNameWithoutExtension(RutaArchivo)
                Dim carpetaDestino As String = RutaDescomprimir ' Path.Combine(carpetaBase, nombreArchivo)
                ' Crear carpeta destino si no existe
                If Not Directory.Exists(carpetaDestino) Then
                    Try
                        Directory.CreateDirectory(carpetaDestino)
                    Catch ex As Exception
                        ErrorMsg = "Descomprimir - Error al descomprimir archivo: " & carpetaDestino.ToString()
                        dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpeta.Item("fk_Cargue_Automatico")), CInt(RowCarpeta.Item("id_Archivo")), 0, 0, 1, 0, ErrorMsg)
                        WriteErrorLog(ErrorMsg)
                        Return False
                    End Try

                End If
                ' Abrir el archivo con SharpCompress (detecta el tipo automáticamente)
                Using archivo = ArchiveFactory.Open(RutaArchivo)
                    For Each entrada In archivo.Entries
                        If Not entrada.IsDirectory Then
                            ' Extraer a la carpeta destino
                            entrada.WriteToDirectory(carpetaDestino, New ExtractionOptions With {
                        .ExtractFullPath = True,
                        .Overwrite = True
                    })
                        End If
                    Next
                End Using
                Return True
            Catch ex As Exception
                ErrorMsg = "Error al Descomprimir Archivo ex: " & Left(ex.ToString(), 350)
                dbmImaging.SchemaProcess.PA_Actualiza_Estado_Archivo_CargueAutomatico.DBExecute(CInt(RowCarpeta.Item("fk_Cargue_Automatico")), CInt(RowCarpeta.Item("id_Archivo")), 0, 0, 1, 0, ErrorMsg)
                WriteErrorLog("Error al Descomprimir Archivo ex: " & ex.ToString())
                Return False
            End Try
        End Function
        Private Sub DescomprimirSubCarpeta(dbmImaging As DBImagingDataBaseManager, RutaArchivo As String, RowParam As DataRow, RowCarpetas As DataRow)
            If Trim(RutaArchivo.ToString()) <> "" Then
                Dim NombreTipoArchivo As String = RowParam.Item("estructura_Nombre_SubCarpeta").ToString().Trim
                Dim Extensiones As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
                Dim textoExt As String = Convert.ToString(RowParam.Item("extension_SubCarpeta"))
                Dim partes = textoExt.Split({"|"c}, StringSplitOptions.RemoveEmptyEntries)
                For Each p In partes
                    Dim s = p.Trim()
                    If s.Length > 0 Then
                        If Not s.StartsWith(".") Then s = "." & s
                        Extensiones.Add(s)
                    End If
                Next
                Dim FileNames As IEnumerable(Of String) =
                    Directory.EnumerateFiles(RutaArchivo, "*", System.IO.SearchOption.TopDirectoryOnly) _
                    .Where(Function(ruta)
                               Dim nombre = Path.GetFileName(ruta)
                               Dim ext = Path.GetExtension(ruta)

                               ' Coincidencia por prefijo (case-insensitive)
                               Dim coincidePrefijo = nombre.StartsWith(NombreTipoArchivo, StringComparison.OrdinalIgnoreCase)

                               ' Si no hay extensiones válidas, NO filtrar por extensión (aceptar todas).
                               Dim coincideExtension = (Extensiones.Count = 0) OrElse Extensiones.Contains(ext)

                               Return coincidePrefijo AndAlso coincideExtension
                           End Function)
                Try
                    'PROCESO DE DESCOMPRIMIR SUBCARPETAS
                    For Each nfile In FileNames
                        DescomprimirArchivo(dbmImaging, nfile, RowCarpetas, RutaArchivo)
                    Next
                Catch ex As Exception
                    WriteErrorLog("Error al descomprimir subCargarCarpetas ex: " & ex.ToString())
                End Try
            End If
        End Sub
        Private Sub WriteErrorLog(ByVal nMessage As String)
            'SyncLock objectLock
            If Program.Config.LogActivo = False Then
                Exit Sub
            End If
            'If Directory.Exists(Program.AppDataPath) = False Then
            '    Directory.CreateDirectory(Program.AppDataPath)
            'End If
            If Directory.Exists(Program.Config.RutaLog) = False Then
                Directory.CreateDirectory(Program.Config.RutaLog)
            End If
            Try
                Dim sw As New StreamWriter(Program.Config.RutaLog & "\log_" & Now.ToString("yyyyMMdd") & ".txt", True)

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

            'End SyncLock
        End Sub

        Public Sub JWriteLog(ByVal mensaje As String, ByVal tipo As EventLogEntryType)

            If Not EventLog.SourceExists("MiharuCargueImagenesDDOService") Then
                EventLog.CreateEventSource("MiharuCargueImagenesDDOService", "Application")
            End If

            Dim eventLog1 As EventLog = New EventLog()
            eventLog1.Source = "MiharuCarguImagenesDDOService"
            eventLog1.WriteEntry(mensaje, tipo)

        End Sub

#End Region

#Region " Funciones "
        Public Function CopyList(Of T)(oldList As List(Of T)) As List(Of T)
            'Serialize
            Dim xmlString As String = ""
            Dim string_writer As New StringWriter
            Dim xml_serializer As New XmlSerializer(GetType(List(Of T)))
            xml_serializer.Serialize(string_writer, oldList)
            xmlString = string_writer.ToString()

            'Deserialize
            Dim string_reader As New StringReader(xmlString)
            Dim newList As List(Of T)
            newList = DirectCast(xml_serializer.Deserialize(string_reader), List(Of T))
            string_reader.Close()

            Return newList
        End Function
        Private Function ValidarExiste_ArchivoCargue(NombreArchivo As String, dbmImaging As DBImagingDataBaseManager, nlog As DBImaging.SchemaConfig.TBL_Tipos_LogRow) As Boolean
            Dim retorno As Boolean = True
            Try
                Dim Cargue_valido = dbmImaging.SchemaConfig.TBL_Cargue.DBFindByfk_Entidadfk_Proyectofk_Tipo_LogArchivo_CargueValido(nlog.fk_Entidad, nlog.fk_Proyecto, nlog.id_Tipo_log, NombreArchivo, True)

                If Cargue_valido.Rows.Count > 0 Then
                    WriteErrorLog("Archivo cargado: " & NombreArchivo.ToString())
                    retorno = False
                End If
            Catch ex As Exception
                WriteErrorLog("ValidarExiste ArchivoCargue, nombre archivo: " & NombreArchivo.ToString() & ", error: " & ex.ToString())
            End Try
            Return retorno
        End Function

        Private Function ValidacionesSplit(ByRef retorno As Boolean, StrValidacionColumn As String(), StrLengthValidaciones As String(), NombreArchivo_Aux As String, UsaNombreArchivoExacto As Boolean) As Object
            Dim NombreArchivoValidado As String = String.Empty
            Try
                If (StrValidacionColumn.Count() > 0) Then
                    For index As Integer = 0 To StrValidacionColumn.Count() - 1
                        Dim AuxItemValidacion As String = Nothing
                        If (StrValidacionColumn(index).ToString().Contains("FECHA") Or StrValidacionColumn(index).ToString().Contains("CORTE") Or StrValidacionColumn(index).ToString().Contains("CONSECUTIVO")) Then
                            AuxItemValidacion = StrValidacionColumn(index).ToString()
                        Else
                            AuxItemValidacion = StrValidacionColumn(index).ToString().ToUpper()
                        End If
                        Dim LengthItemValidacion As String() = StrLengthValidaciones(index).Split("-"c)
                        Dim lengthInicio As Integer = CInt(LengthItemValidacion(0).ToString())
                        Dim lengthFinal As Integer = CInt(LengthItemValidacion(1).ToString())
                        Dim auxArchivoVal As String = Nothing

                        If (AuxItemValidacion.Contains("*")) Then
                            Dim OrAnd = AuxItemValidacion.Split("*"c)
                            If (OrAnd.Contains("OR") And Not OrAnd.Contains("AND")) Then

                                OrAnd = OrAnd.Where(Function(x) Not x.Contains("OR")).ToArray()

                                For Each itemOr_loopVariable In OrAnd
                                    auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()
                                    If (auxArchivoVal = itemOr_loopVariable.ToUpper()) Then
                                        retorno = True
                                        NombreArchivoValidado = NombreArchivoValidado + auxArchivoVal
                                    End If
                                Next
                            End If
                        ElseIf (AuxItemValidacion.Contains("<FECHA>")) Then
                            Dim dtAux As New DateTime()
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio))

                            AuxItemValidacion = AuxItemValidacion.Replace("<FECHA>", "")
                            retorno = DateTime.TryParseExact(auxArchivoVal, AuxItemValidacion, Nothing, DateTimeStyles.None, dtAux)

                            NombreArchivoValidado = NombreArchivoValidado + auxArchivoVal

                            If (retorno = False) Then
                                ' TODO: might not be correct. Was : Exit For
                                Exit For
                            End If
                        ElseIf AuxItemValidacion.Contains("<OFICINA>") Then
                            AuxItemValidacion = AuxItemValidacion.Replace("<OFICINA", "")

                            Continue For
                        ElseIf AuxItemValidacion.Contains("<NUMERO>") Then
                            AuxItemValidacion = AuxItemValidacion.Replace("<NUMERO>", "")

                            Continue For
                        ElseIf AuxItemValidacion.Contains("<CORTE>") Then
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()
                            AuxItemValidacion = AuxItemValidacion.Replace("<CORTE>", "")

                            Dim check As New Regex(AuxItemValidacion)

                            retorno = check.IsMatch(auxArchivoVal)
                            NombreArchivoValidado = NombreArchivoValidado + auxArchivoVal

                            If (retorno = False) Then
                                ' TODO: might not be correct. Was : Exit For
                                Exit For
                            End If
                        ElseIf AuxItemValidacion.Contains("<ANIO>") Then
                            AuxItemValidacion = AuxItemValidacion.Replace("<ANIO>", "")
                            Dim anios = Enumerable.Range(1900, DateTime.Now.Year)
                            Dim encontrado = anios.Where(Function(x) x.ToString() = AuxItemValidacion)

                            If encontrado.Count() = 0 Then
                                retorno = False
                            End If
                        ElseIf AuxItemValidacion.Contains("<CONCEPTO>") Then 'IMPUESTOS ASISTIDOS 
                            AuxItemValidacion = AuxItemValidacion.Replace("<CONCEPTO>", "")
                            Continue For
                        ElseIf AuxItemValidacion.Contains("<FORMATO>") Then 'IMPUESTOS ASISTIDOS 
                            AuxItemValidacion = AuxItemValidacion.Replace("<FORMATO>", "")
                            Continue For
                        ElseIf AuxItemValidacion.Contains("<VERSION>") Then 'IMPUESTOS ASISTIDOS 
                            AuxItemValidacion = AuxItemValidacion.Replace("<VERSION>", "")
                            Continue For
                        ElseIf AuxItemValidacion.Contains("<AÑO>") Then 'IMPUESTOS ASISTIDOS 
                            AuxItemValidacion = AuxItemValidacion.Replace("<AÑO>", "")
                            Continue For
                        ElseIf AuxItemValidacion.Contains("<CONSECUTIVO>") Then 'IMPUESTOS ASISTIDOS 
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()
                            AuxItemValidacion = AuxItemValidacion.Replace("<CONSECUTIVO>", "")

                            Dim check As New Regex(AuxItemValidacion)

                            retorno = check.IsMatch(auxArchivoVal)

                            NombreArchivoValidado = NombreArchivoValidado + auxArchivoVal

                            If (retorno = False) Then
                                ' TODO: might not be correct. Was : Exit For
                                Exit For
                            End If
                        Else
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()

                            NombreArchivoValidado = NombreArchivoValidado + auxArchivoVal

                            If (auxArchivoVal <> AuxItemValidacion) Then
                                retorno = False
                                ' TODO: might not be correct. Was : Exit For
                                Exit For
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                retorno = False
            End Try

            If retorno Then
                If ((NombreArchivoValidado.ToUpper <> NombreArchivo_Aux.ToUpper) And (UsaNombreArchivoExacto)) Then
                    retorno = False
                End If
            End If

            Return retorno
        End Function

#End Region





    End Class
End Namespace

