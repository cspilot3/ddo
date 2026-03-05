Imports System.IO
Imports System.Net
Imports System.Security.Policy
Imports System.Threading
Imports System.Windows.Forms.VisualStyles
Imports DBCore
Imports DBCore.SchemaConfig
Imports DBCore.SchemaProcess
Imports DBCore.Schemadbo
Imports Ionic.Zip
Imports Microsoft.Win32
Imports Miharu.Desktop.Library.Config
Imports Miharu.FileProvider.Library
Imports Miharu.Security.Library.WebService
Imports Slyg.Tools.Imaging

Namespace Servicio
    Public Class DDOTransferService

#Region " Declaraciones "
        Private Detener As Boolean
        Private _centro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType
        Private ThreadArchivosD As Thread
        Private ThreadArchivosF As Thread
#End Region

#Region " Metodos reemplazados "
        Protected Overrides Sub OnStart(ByVal args() As String)
            ' Agregue el código aquí para iniciar el servicio. Este método debería poner
            ' en movimiento los elementos para que el servicio pueda funcionar.
            IniciarServicio()
        End Sub

        Protected Overrides Sub OnStop()
            ' Agregue el código aquí para realizar cualquier anulación necesaria para detener el servicio.
            DetenerServicio()
        End Sub
#End Region

#Region " Metodos "

        Private Sub LoadConfig()
            ' Leer la configuración
            If (File.Exists(Program.AppDataPath + DDOTransferConfig.ConfigFileName)) Then
                Program.Config = DDOTransferConfig.Deserialize(Program.AppDataPath)
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
                WebService.setUser(Program.Config.User, DDOTransferConfig.Decrypt(Program.Config.Password))
                Program.ConnectionStrings = DDOTransferConfig.getCadenasConexion(WebService)

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

            If ThreadArchivosD IsNot Nothing Then
                Try
                    ThreadArchivosD.Abort()
                Catch ex As Exception

                End Try
            End If

            If ThreadArchivosF IsNot Nothing Then
                Try
                    ThreadArchivosF.Abort()
                Catch ex As Exception

                End Try
            End If

        End Sub

        Private Sub Proceso()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)

                Try
                    dbmCore.Connection_Open(1)

                    Dim Procesos = dbmCore.SchemaConfig.CTA_Get_Proceso_Automatico.DBGet()

                    If Procesos.Rows.Count > 0 Then
                        For Each Proceso As DBCore.SchemaConfig.CTA_Get_Proceso_AutomaticoRow In Procesos

                            Dim ArraListParameters As ArrayList = New ArrayList

                            If Proceso.Nombre_Proceso_Automatico = "Digicom" Then
                                ArraListParameters.Add(Proceso)

                                ThreadArchivosD = New Thread(AddressOf Digicom)
                                ThreadArchivosD.Start(ArraListParameters)
                            End If

                            If Proceso.Nombre_Proceso_Automatico = "Filenet" Then
                                ArraListParameters.Add(Proceso)

                                ThreadArchivosF = New Thread(AddressOf Filenet)
                                ThreadArchivosF.Start(ArraListParameters)
                            End If
                        Next
                    Else
                        WriteErrorLog("No hay proceso automáticos para ejecutar")
                    End If
                Catch ex As Exception
                    WriteErrorLog("Error Proceso ex: " & ex.ToString())
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try
            Catch ex As Exception
                WriteErrorLog("Error Proceso ex: " & ex.ToString())
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub Filenet(ByVal objectArray As Object)
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim ArraListParametersTapas As ArrayList = objectArray

            Dim proceso As DBCore.SchemaConfig.CTA_Get_Proceso_AutomaticoRow = ArraListParametersTapas(0)

            dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)
            dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)

            While Not Detener
                If Detener Then Return

                Try
                    dbmSecurity.Connection_Open(1)
                    dbmCore.Connection_Open(1)

                    Dim CalendarioDataTable = dbmSecurity.SchemaConfig.TBL_Calendario.DBGet(Proceso.fk_Entidad, Proceso.fk_Calendario)

                    If CalendarioDataTable.Rows.Count > 0 Then
                        Dim habil = dbmSecurity.SchemaConfig.PA_Es_Hora_Habil.DBExecute(Proceso.fk_Entidad, Proceso.fk_Calendario)

                        Dim horas() As String = Nothing

                        If Not proceso.IshoraEjecucionNull AndAlso proceso.horaEjecucion <> "" Then
                            horas = proceso.horaEjecucion.ToString().Split("|"c)
                        End If

                        If (habil) Then
                            If horas IsNot Nothing AndAlso horas.Count > 0 Then
                                For Each hora As String In horas
                                    If hora = Date.Now.ToString("HH") Then
                                        GeneracionFilenet(dbmCore, proceso)
                                    End If
                                Next
                            Else
                                GeneracionFilenet(dbmCore, proceso)
                                End If
                            Else
                                WriteErrorLog("No es una hora habil para ejecutar el proceso: " + proceso.Descripcion)
                            End If
                        Else
                            WriteErrorLog("No existe el calendario del servicio: " & Proceso.Nombre_Proceso_Automatico & ", fk_Entidad: " & Proceso.fk_Entidad & ", fk_Calendario: " & Proceso.fk_Calendario)
                    End If
                Catch ex As Exception
                    WriteErrorLog("Error Proceso Filenet ex: " & ex.ToString())
                Finally
                    If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try

                If Detener Then Return

                Thread.Sleep(Proceso.Intervalo_Ejecucion_Minutos * 60000) ' Esperar n milisegundos antes de continuar
            End While
        End Sub

        Private Sub GeneracionFilenet(dbmCore As DBCoreDataBaseManager, nproceso As CTA_Get_Proceso_AutomaticoRow)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim OutputFolder As String = ""

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(1)

                Dim FileDataTable = dbmCore.SchemaProcess.PA_Get_Expedientes_Exportacion_Filenet.DBExecute(nproceso.fk_Entidad, nproceso.fk_Proyecto, nproceso.fk_Esquema, nproceso.fk_Estado, nproceso.id_Proceso_Automatico)

                If FileDataTable.Rows.Count > 0 Then
                    Dim dataTable As DataTable = New DataTable()

                    dataTable.Columns.Add("fk_Expediente", GetType(Long))
                    dataTable.Columns.Add("fk_Folder", GetType(Short))
                    dataTable.Columns.Add("fk_File", GetType(Integer))
                    dataTable.Columns.Add("fk_Documento", GetType(Integer))

                    For i = 0 To FileDataTable.Rows.Count - 1
                        Dim datarow = dataTable.NewRow()
                        datarow("fk_Expediente") = FileDataTable(i).fk_Expediente
                        datarow("fk_Folder") = FileDataTable(i).fk_Folder
                        datarow("fk_File") = FileDataTable(i).fk_File
                        datarow("fk_Documento") = FileDataTable(i).fk_Documento
                        dataTable.Rows.Add(datarow)
                    Next
                    dataTable.AcceptChanges()

                    Const tabla As String = "#RegistrosDataFilenet"
                    BulkInsert.InsertDataTableCore(dataTable, dbmCore, tabla)

                    Dim FileDataDataTable = dbmCore.SchemaProcess.PA_Get_Expedientes_Data_Exportacion_Filenet.DBExecute(tabla)

                    If FileDataDataTable.Rows.Count > 0 Then
                        'TO DO
                        'Validar que cada uno de los expedientes tenga registros en la FileDataDataTable

                        Dim idProcesosFilenet As String = "<table border=1 cellspacing=0 cellpadding=2 bordercolor=""666633""><tr><td align=""Center""><b>NOMBRE CARPETA</b></td><td align=""Center""><b>CANTIDAD IMÁGENES</b></td></tr>"
                        OutputFolder = nproceso.Ruta

                        ' Crear el directorio temp de las imágenes
                        If Not Directory.Exists(OutputFolder & "\Temp") Then
                            Directory.CreateDirectory(OutputFolder & "\Temp")
                        End If

                        Dim NombreCarpeta As String = ""
                        Dim Compresion As ImageManager.EnumCompression

                        If (nproceso.fk_Formato_Salida_Imagen = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                            Compresion = ImageManager.EnumCompression.Ccitt4
                        Else
                            Compresion = ImageManager.EnumCompression.Lzw
                        End If

                        Dim formato = Utilities.GetEnumFormat(nproceso.Extension_Formato_Imagen)

                        Dim Contador As Integer = 0
                        Dim CrearNuevaCarpeta As Boolean = True
                        Dim OutputFolderCarpeta As String = ""
                        Dim NomArchivo = ""
                        Dim sw As StreamWriter
                        Dim datetimenow As String = ""
                        Dim ImagenValida As Boolean = False
                        Dim id_Generacion_Proceso_Automatico As Long
                        Dim DataTableRegistosPorCarpeta As New DBCore.SchemaProcess.CTA_Set_Exportacion_DigicomDataTable

                        Dim esquemaExportacion As List(Of Object) = Nothing
                        esquemaExportacion = (From a In FileDataTable Group a By groupDt = a.Field(Of String)("Sigla") Into Group Select Group.Select(Function(x) x("Sigla")).First()).ToList()

                        For Each sigla As String In esquemaExportacion
                            Dim FilesbyGroup = FileDataTable.Select("Sigla = '" + sigla.ToString() + "'").CopyToDataTable

                            Dim FilesbyGroupDataView As New DataView(FilesbyGroup)

                            Dim CantidadRegistros As Integer = FilesbyGroupDataView.Count
                            Dim CantidadCarpetas As Integer = 1
                            CrearNuevaCarpeta = True

                            If nproceso.cantidadImagenes > 0 Then
                                CantidadCarpetas = CInt(CantidadRegistros / nproceso.cantidadImagenes)
                            End If

                            Dim GeneracionProcesoAutomaticoType As New DBCore.SchemaProcess.TBL_Generacion_Proceso_AutomaticoType

                            Try
                                For Each ItemFile As DataRowView In FilesbyGroupDataView
                                    If (CrearNuevaCarpeta) Then
                                        datetimenow = Date.Now.ToString("HHmm")

                                        'nombre_Carpeta
                                        NombreCarpeta = ValidacionesSplit(nproceso.Estructura_Nombre_Carpeta.Split("|"c), ItemFile)

                                        If NombreCarpeta = "" Then
                                            GeneracionProcesoAutomaticoType.valido = False
                                            GeneracionProcesoAutomaticoType.Observaciones = "Error al crear la carpeta"

                                            dbmCore.SchemaProcess.TBL_Generacion_Proceso_Automatico.DBUpdate(GeneracionProcesoAutomaticoType, id_Generacion_Proceso_Automatico)
                                            Return
                                        End If

                                        'Inserción inicio proceso BD
                                        id_Generacion_Proceso_Automatico = dbmCore.SchemaProcess.PA_Set_Generacion_Proceso_Automatico.DBExecute(nproceso.id_Proceso_Automatico, NombreCarpeta)

                                        OutputFolderCarpeta = OutputFolder & NombreCarpeta

                                        ' Crear el directorio de las imágenes
                                        Directory.CreateDirectory(OutputFolderCarpeta)

                                        If (nproceso.Genera_Archivo) Then
                                            ' Define Nombre del Archivo
                                            If nproceso.Estructura_Nombre_Carpeta = nproceso.Estructura_Nombre_Archivo Then
                                                NomArchivo = NombreCarpeta & nproceso.Extension_Archivo
                                            Else
                                                NomArchivo = ValidacionesSplit(nproceso.Estructura_Nombre_Carpeta.Split("|"c), ItemFile)

                                                If NomArchivo = "" Then
                                                    GeneracionProcesoAutomaticoType.valido = False
                                                    GeneracionProcesoAutomaticoType.Observaciones = "Error al crear el archivo"

                                                    dbmCore.SchemaProcess.TBL_Generacion_Proceso_Automatico.DBUpdate(GeneracionProcesoAutomaticoType, id_Generacion_Proceso_Automatico)
                                                    Return
                                                End If
                                            End If

                                            If nproceso.Extension_Archivo = ".csv" Then
                                                sw = New StreamWriter(File.Open(OutputFolderCarpeta & "\" & NomArchivo, FileMode.Create), New Text.UTF8Encoding(False))
                                                sw.Close()
                                                sw.Dispose()
                                            End If
                                        End If


                                    End If
                                    CrearNuevaCarpeta = False

                                    ImagenValida = False
                                    ExportarImagenFilenet(dbmImaging, ItemFile, Compresion, formato, OutputFolderCarpeta, nproceso.Extension_Formato_Imagen, OutputFolder & "\Temp\", ImagenValida)

                                    If (nproceso.Genera_Archivo) And ImagenValida = True Then
                                        Dim consultaDataExpediente = "fk_Expediente = '" & CLng(ItemFile.Item("fk_Expediente")).ToString() & "' AND fk_Folder = '" + CShort(ItemFile.Item("fk_Folder")).ToString() & "' AND fk_File = '" + CInt(ItemFile.Item("fk_File")).ToString() & "'"
                                        Dim datacsv As String = ""
                                        Try
                                            Dim DataExpediente = FileDataDataTable.[Select](consultaDataExpediente).CopyToDataTable()
                                            Dim DataExpedienteView As DataView = New DataView(DataExpediente)
                                            For Each registro As DataRowView In DataExpedienteView
                                                If datacsv = "" Then
                                                    datacsv = registro("dataCsv").ToString()
                                                Else
                                                    datacsv = datacsv & ";" & registro("dataCsv").ToString()
                                                End If
                                            Next
                                            datacsv = ItemFile.Item("dataCsv").ToString().Replace("@data", datacsv)
                                        Catch
                                            datacsv = ItemFile.Item("dataCsv").ToString().Replace(";@data", datacsv)
                                        End Try

                                        sw = New StreamWriter(File.Open(OutputFolderCarpeta & "\" & NomArchivo, FileMode.Append), New Text.UTF8Encoding(False))
                                        sw.WriteLine(datacsv)
                                        sw.Close()
                                        sw.Dispose()
                                    End If

                                    If (ImagenValida) Then
                                        Dim row As DBCore.SchemaProcess.CTA_Set_Exportacion_DigicomRow = DataTableRegistosPorCarpeta.NewRow
                                        row.fk_OT = CInt(ItemFile.Item("fk_OT"))
                                        row.fk_Precinto = 0
                                        row.fk_Contenedor = 0
                                        row.fk_Expediente = CLng(ItemFile.Item("fk_Expediente"))
                                        row.fk_Folder = CInt(ItemFile.Item("fk_Folder"))
                                        row.fk_File = CInt(ItemFile.Item("fk_File"))
                                        row.nombreImagen = ItemFile.Item("Nombre_Imagen_File").ToString() & nproceso.Extension_Formato_Imagen

                                        DataTableRegistosPorCarpeta.Rows.Add(row)
                                        DataTableRegistosPorCarpeta.AcceptChanges()
                                    End If

                                    Contador = Contador + 1

                                    If Contador = nproceso.cantidadImagenes Or Contador = FilesbyGroupDataView.Count Then
                                        Const tabla2 As String = "#RegistrosPorCarpeta"
                                        If DataTableRegistosPorCarpeta IsNot Nothing AndAlso DataTableRegistosPorCarpeta.Rows.Count > 0 Then
                                            BulkInsert.InsertDataTableCore(DataTableRegistosPorCarpeta.CopyToDataTable(), dbmCore, tabla2)
                                            'Inserción finalización proceso BD
                                            dbmCore.SchemaProcess.PA_Set_Generacion_Proceso_Automatico_Detalle.DBExecute(nproceso.id_Proceso_Automatico, nproceso.fk_Entidad, nproceso.fk_Proyecto, nproceso.fk_Esquema, nproceso.fk_Estado, id_Generacion_Proceso_Automatico, tabla2)
                                        End If

                                        idProcesosFilenet = idProcesosFilenet & "<tr><td align=""Center"">" & OutputFolderCarpeta.Substring(OutputFolderCarpeta.LastIndexOf("\") + 1) & "</td><td align=""Center"">" & Contador & "</td></tr>"

                                        CrearNuevaCarpeta = True
                                        Contador = 0
                                        DataTableRegistosPorCarpeta = New DBCore.SchemaProcess.CTA_Set_Exportacion_DigicomDataTable

                                        Dim nombreArchivo As String = OutputFolderCarpeta

                                        'comprimir
                                        If nproceso.Comprimir Then
                                            If nproceso.Extension_Comprimido = ".zip" Then
                                                comprimirzip(OutputFolderCarpeta)
                                                nombreArchivo = nombreArchivo + ".zip"
                                            End If
                                        End If

                                        'mover carpeta
                                        If nproceso.usa_Mover_A_Ruta And Not nproceso.Ispath_Ruta_A_MoverNull AndAlso Not String.IsNullOrWhiteSpace(nproceso.path_Ruta_A_Mover) Then
                                            File.Move(nombreArchivo, nproceso.path_Ruta_A_Mover + Path.GetFileName(nombreArchivo))
                                        End If

                                        'si no ha pasado un minuto esperar el minuto
                                        If datetimenow = Date.Now.ToString("HHmm") And CInt(ItemFile.Item("idRegistro")) < FilesbyGroupDataView.Count Then
                                            System.Threading.Thread.Sleep(60000)
                                        End If
                                    End If
                                Next
                            Catch ex As Exception
                                GeneracionProcesoAutomaticoType.valido = False
                                GeneracionProcesoAutomaticoType.Observaciones = ex.Message.ToString()

                                dbmCore.SchemaProcess.TBL_Generacion_Proceso_Automatico.DBUpdate(GeneracionProcesoAutomaticoType, id_Generacion_Proceso_Automatico)
                            End Try
                        Next

                        'enviar correo
                        idProcesosFilenet = idProcesosFilenet & "</table>"
                        If nproceso.Genera_Envio_Reporte Then
                            EnvioReporte(nproceso, idProcesosFilenet)
                        End If

                        'enviar reporte correo adicional con adjunto
                        If nproceso.usa_Notificacion_Adicional Then
                            EnvioReporteFilenet(nproceso)
                        End If
                    Else
                        WriteErrorLog("No se encontró data de los registros a generar: " & nproceso.Nombre_Proceso_Automatico)
                    End If
                Else
                    WriteErrorLog("No hay registros para generar: " & nproceso.Nombre_Proceso_Automatico)
                End If
            Catch ex As Exception
                WriteErrorLog("Error GeneracionFilenet ex: " & ex.ToString())
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub ExportarImagenFilenet(ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nItemFile As DataRowView, nCompresion As ImageManager.EnumCompression, nformato As ImageManager.EnumFormat, nFolderName As String, ByVal nformatoSalida As String, ByVal nOutPutFolderTemp As String, ByRef ImagenValida As Boolean)
            Dim FileNames As New List(Of String)
            Dim manager As FileProviderManager = Nothing

            Try
                Dim fk_Expediente As Long = CLng(nItemFile.Item("fk_Expediente"))
                Dim fk_Folder As Short = CShort(nItemFile.Item("fk_Folder"))
                Dim fk_File As Short = CShort(nItemFile.Item("fk_File"))
                Dim fk_Version As Short = CShort(nItemFile.Item("fk_Version"))

                manager = New FileProviderManager(fk_Expediente, fk_Folder, fk_File, fk_Version, dbmImaging, 1)
                manager.Connect()

                Dim Folios = manager.GetFolios(fk_Expediente, fk_Folder, fk_File, fk_Version)

                For folio As Short = 1 To Folios
                    Dim Imagen() As Byte = Nothing
                    Dim Thumbnail() As Byte = Nothing

                    manager.GetFolio(fk_Expediente, fk_Folder, fk_File, fk_Version, folio, Imagen, Thumbnail)

                    'FileName = nOutPutFolderTemp & Guid.NewGuid().ToString() & nformatoSalida
                    'FileNames.Add(FileName)

                    Dim tempFileName = Path.Combine(nOutPutFolderTemp, Guid.NewGuid().ToString() & nformatoSalida)
                    FileNames.Add(tempFileName)

                    Using fs As New FileStream(tempFileName, FileMode.Create, FileAccess.Write)
                        fs.Write(Imagen, 0, Imagen.Length)
                    End Using

                    'Using fs = New FileStream(FileName, FileMode.Create)
                    '    fs.Write(Imagen, 0, Imagen.Length)
                    '    fs.Close()
                    'End Using
                Next

                If (FileNames.Count > 0) Then
                    Dim Format As ImageManager.EnumFormat = nformato

                    Select Case nformatoSalida
                        Case ".bmp"
                            Format = ImageManager.EnumFormat.Bmp
                        Case ".gif"
                            Format = ImageManager.EnumFormat.Gif
                        Case ".jpg"
                            Format = ImageManager.EnumFormat.Jpeg
                            nCompresion = ImageManager.EnumCompression.Jpeg
                        Case ".pdf"
                            Format = ImageManager.EnumFormat.Pdf
                            nCompresion = ImageManager.EnumCompression.Jpeg
                        Case ".png"
                            Format = ImageManager.EnumFormat.Png
                        Case ".tif"
                            Format = ImageManager.EnumFormat.Tiff
                    End Select

                    'FileName = nFolderName & "\" & nItemFile.Item("Nombre_Imagen_File").ToString() & nformatoSalida
                    Dim finalFileName = Path.Combine(nFolderName, nItemFile("Nombre_Imagen_File").ToString() & nformatoSalida)

                    'ImageManager.Save(FileNames, FileName, "", nformato, nCompresion, False, nOutPutFolderTemp, True)
                    ImageManager.Save(FileNames, finalFileName, "", Format, nCompresion, False, nOutPutFolderTemp, True)
                    ImagenValida = True
                End If

            Catch ex As Exception
                WriteErrorLog("Error exportando imagen fk_Expediente: " & nItemFile.Item("fk_Expediente").ToString() & ", fk_Folder: " & nItemFile.Item("fk_Folder").ToString() & ", fk_File: " & nItemFile.Item("fk_File").ToString() & ": " & ex.ToString())
            Finally
                If (manager IsNot Nothing) Then
                    manager.Disconnect()
                End If

                ''borrar archivos temporales
                For Each tempFile In FileNames
                    If File.Exists(tempFile) Then
                        Try
                            File.Delete(tempFile)
                        Catch ex As Exception
                            WriteErrorLog($"Error eliminando archivo temporal: {tempFile} - {ex.Message}")
                        End Try
                    End If
                Next
            End Try
        End Sub

        Private Sub Digicom(ByVal objectArray As Object)
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim ArraListParametersTapas As ArrayList = objectArray

            Dim proceso As DBCore.SchemaConfig.CTA_Get_Proceso_AutomaticoRow = ArraListParametersTapas(0)
            dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)
            dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)

            While Not Detener
                If Detener Then Return

                Try
                    dbmSecurity.Connection_Open(1)
                    dbmCore.Connection_Open(1)

                    Dim CalendarioDataTable = dbmSecurity.SchemaConfig.TBL_Calendario.DBGet(Proceso.fk_Entidad, Proceso.fk_Calendario)

                    If CalendarioDataTable.Rows.Count > 0 Then
                        Dim habil = dbmSecurity.SchemaConfig.PA_Es_Hora_Habil.DBExecute(Proceso.fk_Entidad, Proceso.fk_Calendario)

                        If (habil) Then
                            If Proceso.usaNotificacionConsolidado Then
                                If Date.Now.ToString("HH") = Proceso.horaEnvioNotificacion Then
                                    EnviarNotificacionConsolidadaDigicom(Proceso)
                                End If
                            End If

                            MoverArchivosDigicom(dbmCore, Proceso)
                        Else
                            WriteErrorLog("No es una hora habil para ejecutar el proceso")
                        End If
                    Else
                        WriteErrorLog("No existe el calendario del servicio: " & Proceso.Nombre_Proceso_Automatico & ", fk_Entidad: " & Proceso.fk_Entidad & ", fk_Calendario: " & Proceso.fk_Calendario)
                    End If
                Catch ex As Exception
                    WriteErrorLog("Error Proceso Digicom ex: " & ex.ToString())
                Finally
                    If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try

                If Detener Then Return

                Thread.Sleep(Proceso.Intervalo_Ejecucion_Minutos * 60000) ' Esperar n milisegundos antes de continuar
            End While
        End Sub

        Private Sub EnviarNotificacionConsolidadaDigicom(nproceso As CTA_Get_Proceso_AutomaticoRow)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)

                dbmCore.Connection_Open(1)

                Dim Notificacion = dbmCore.SchemaConfig.PA_Get_Notificacion.DBExecute(nproceso.fk_NotificacionConsolidado)

                If Notificacion.Rows.Count > 0 Then
                    Dim CorreoDatatable = dbmCore.SchemaConfig.PA_Busqueda_Parametros_Correo.DBExecute(Notificacion(0).id_Notificacion, Notificacion(0).id_Notificacion_Lista)
                    If CorreoDatatable.Rows.Count > 0 Then
                        Dim FechaDatos As String = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")
                        Dim DatosConsolidado = dbmCore.SchemaProcess.PA_Get_Datos_Notificacion_Consolidado_Digicom.DBExecute(FechaDatos)

                        If DatosConsolidado.Rows.Count > 0 Then
                            Dim Message As String = ""
                            Dim MailTo As String = ""
                            Dim MailCC As String = ""
                            Dim MailCCO As String = ""
                            Dim Subject As String = ""
                            Dim nAttachName As String = ""
                            Dim nAttach As Byte() = Nothing
                            Dim CuerpoCorreo As String = ""
                            Dim Novedades As String = ""

                            CuerpoCorreo = CorreoDatatable(0).CUERPO.Replace("@Tabla", DatosConsolidado.Rows(0).Item("Tabla").ToString())
                            CuerpoCorreo = CuerpoCorreo.Replace("@FechaEnvio", FechaDatos)

                            Message = CorreoDatatable(0).SALUDO & CuerpoCorreo & CorreoDatatable(0).FIRMA
                            MailTo = CorreoDatatable(0).CORREOS
                            Subject = CorreoDatatable(0).ASUNTO

                            Dim DBMTools As DBTools.DBToolsDataBaseManager = Nothing

                            Try
                                DBMTools = New DBTools.DBToolsDataBaseManager(Program.ConnectionStrings.Tools)
                                DBMTools.Connection_Open()

                                DBMTools.InsertMail(nproceso.fk_Entidad, 2, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)

                                dbmCore.SchemaProcess.PA_Set_Datos_Notificacion_Consolidado_Digicom.DBExecute(DatosConsolidado.Rows(0).Item("idProcesosEnviar").ToString())
                            Catch ex As Exception
                                WriteErrorLog("Error proceso envío notificación proceso: " & nproceso.Nombre_Proceso_Automatico & ", notificación: " & Notificacion(0).Nombre_Notificacion & ", ex: " & ex.Message.ToString())
                            Finally
                                DBMTools.Connection_Close()
                            End Try
                        End If
                    End If
                Else
                    WriteErrorLog("No se encuentra configuración para envio de correo del proceso: " & nproceso.Nombre_Proceso_Automatico & ", fk_Notificaion: " & nproceso.fk_Notificacion.ToString())
                End If

            Catch ex As Exception
                WriteErrorLog("Error EnvioReporte ex: " & ex.ToString())
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub MoverArchivosDigicom(ByVal dbmCore As DBCore.DBCoreDataBaseManager, ByVal nproceso As DBCore.SchemaConfig.CTA_Get_Proceso_AutomaticoRow)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim OutputFolder As String = ""
            Dim esPorCargue As Boolean = False

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(1)

                Me._centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(nproceso.fk_Entidad, 1, 1)(0).ToCTA_Centro_ProcesamientoSimpleType()

                Dim FileDataTable = dbmCore.SchemaProcess.PA_Get_Encarpetado_Digicom.DBExecute(nproceso.fk_Entidad, nproceso.fk_Proyecto, nproceso.fk_Esquema, nproceso.fk_Estado, nproceso.es_Por_Contenedor, nproceso.DatosReporte, nproceso.id_Proceso_Automatico)

                If FileDataTable.Rows.Count > 0 Then
                    Dim idProcesosEncarpetados As String = "<table border=1 cellspacing=0 cellpadding=2 bordercolor=""666633""><tr><td align=""Center""><b>COLA DOCUMENTAL</b></td><td align=""Center""><b>NOMBRE CARPETA</b></td><td align=""Center""><b>CANTIDAD IMÁGENES</b></td></tr>"

                    OutputFolder = nproceso.Ruta

                    ' Crear el directorio temp de las imágenes
                    If Not Directory.Exists(OutputFolder & "\Temp") Then
                        Directory.CreateDirectory(OutputFolder & "\Temp")
                    End If

                    Dim NombreCarpeta As String = ""
                    Dim Compresion As ImageManager.EnumCompression

                    If (nproceso.fk_Formato_Salida_Imagen = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                        Compresion = ImageManager.EnumCompression.Ccitt4
                    Else
                        Compresion = ImageManager.EnumCompression.Lzw
                    End If

                    Dim formato = Utilities.GetEnumFormat(nproceso.Extension_Formato_Imagen)

                    Dim Contador As Integer = 0
                    Dim CrearNuevaCarpeta As Boolean = True
                    Dim OutputFolderCarpeta As String = ""
                    Dim NomArchivo = ""
                    Dim sw As StreamWriter
                    Dim datetimenow As String = ""
                    Dim ImagenValida As Boolean = False
                    Dim id_Generacion_Proceso_Automatico As Long
                    Dim DataTableRegistosPorCarpeta As New DBCore.SchemaProcess.CTA_Set_Exportacion_DigicomDataTable

                    Dim esquemaExportacion As List(Of Object) = Nothing
                    esquemaExportacion = (From a In FileDataTable Group a By groupDt = a.Field(Of Integer)("fk_Esquema") Into Group Select Group.Select(Function(x) x("fk_Esquema")).First()).ToList()

                    For Each esquema As String In esquemaExportacion
                        Dim FilesbyGroup = FileDataTable.Select("fk_Esquema = '" + esquema.ToString() + "'").CopyToDataTable

                        Dim FilesbyGroupDataView As New DataView(FilesbyGroup)

                        Dim CantidadRegistros As Integer = 0
                        Dim CantidadCarpetas As Integer = 1
                        CrearNuevaCarpeta = True

                        CantidadRegistros = FilesbyGroupDataView.Count

                        If nproceso.cantidadImagenes > 0 Then
                            CantidadCarpetas = CInt(CantidadRegistros / nproceso.cantidadImagenes)
                        End If

                        Dim GeneracionProcesoAutomaticoType As New DBCore.SchemaProcess.TBL_Generacion_Proceso_AutomaticoType

                        Try
                            For Each ItemFile As DataRowView In FilesbyGroupDataView
                                esPorCargue = CBool(ItemFile.Item("es_Por_Cargue").ToString())
                                If (CrearNuevaCarpeta) Then
                                    datetimenow = Date.Now.ToString("HHmm")
                                    'nombre_Carpeta
                                    NombreCarpeta = ValidacionesSplit(nproceso.Estructura_Nombre_Carpeta.Split("|"c), Nothing)

                                    If NombreCarpeta = "" Then
                                        GeneracionProcesoAutomaticoType.valido = False
                                        GeneracionProcesoAutomaticoType.Observaciones = "Error al crear la carpeta"

                                        dbmCore.SchemaProcess.TBL_Generacion_Proceso_Automatico.DBUpdate(GeneracionProcesoAutomaticoType, id_Generacion_Proceso_Automatico)
                                        Return
                                    End If

                                    'Inserción inicio proceso BD
                                    id_Generacion_Proceso_Automatico = dbmCore.SchemaProcess.PA_Set_Generacion_Proceso_Automatico.DBExecute(nproceso.id_Proceso_Automatico, NombreCarpeta)

                                    OutputFolderCarpeta = OutputFolder & NombreCarpeta

                                    ' Crear el directorio de las imágenes
                                    Directory.CreateDirectory(OutputFolderCarpeta)

                                    If (nproceso.Genera_Archivo) Then
                                        ' Define Nombre del Archivo
                                        If nproceso.Estructura_Nombre_Carpeta = nproceso.Estructura_Nombre_Archivo Then
                                            NomArchivo = NombreCarpeta & nproceso.Extension_Archivo
                                        Else
                                            NomArchivo = ValidacionesSplit(nproceso.Estructura_Nombre_Carpeta.Split("|"c), Nothing)

                                            If NomArchivo = "" Then
                                                GeneracionProcesoAutomaticoType.valido = False
                                                GeneracionProcesoAutomaticoType.Observaciones = "Error al crear el archivo"

                                                dbmCore.SchemaProcess.TBL_Generacion_Proceso_Automatico.DBUpdate(GeneracionProcesoAutomaticoType, id_Generacion_Proceso_Automatico)
                                                Return
                                            End If
                                        End If

                                        If nproceso.Extension_Archivo = ".csv" Then
                                            sw = New StreamWriter(File.Open(OutputFolderCarpeta & "\" & NomArchivo, FileMode.Create), New Text.UTF8Encoding(False))
                                            sw.Close()
                                            sw.Dispose()
                                        End If
                                    End If
                                End If
                                CrearNuevaCarpeta = False

                                ImagenValida = False
                                If esPorCargue Then
                                    ExportarImagenCargue(dbmImaging, ItemFile, Compresion, formato, OutputFolderCarpeta, nproceso.Extension_Formato_Imagen, OutputFolder & "\Temp\", ImagenValida)
                                Else
                                    ExportarImagen(dbmImaging, ItemFile, Compresion, formato, OutputFolderCarpeta, nproceso.Extension_Formato_Imagen, OutputFolder & "\Temp\", ImagenValida)
                                End If

                                If (nproceso.Genera_Archivo) And ImagenValida = True Then
                                    sw = New StreamWriter(File.Open(OutputFolderCarpeta & "\" & NomArchivo, FileMode.Append), New Text.UTF8Encoding(False))
                                    Dim Reporte As String = ItemFile.Item("Reporte").ToString().Replace("@NombreCarpeta", OutputFolderCarpeta.Substring(OutputFolderCarpeta.LastIndexOf("\") + 1))
                                    'Reporte = Reporte.Replace("@NombreCarpeta", OutputFolderCarpeta.Substring(OutputFolderCarpeta.LastIndexOf("\") + 1))
                                    sw.WriteLine(Reporte)
                                    sw.Close()
                                    sw.Dispose()
                                End If

                                If (ImagenValida) Then
                                    Dim row As DBCore.SchemaProcess.CTA_Set_Exportacion_DigicomRow = DataTableRegistosPorCarpeta.NewRow
                                    row.fk_OT = CInt(ItemFile.Item("fk_OT"))
                                    row.fk_Precinto = CInt(ItemFile.Item("fk_Precinto"))
                                    row.fk_Contenedor = CInt(ItemFile.Item("fk_Contenedor"))

                                    If esPorCargue Then
                                        row.fk_Expediente = CInt(ItemFile.Item("fk_Cargue"))
                                        row.fk_Folder = CInt(ItemFile.Item("fk_Cargue_Paquete"))
                                        row.fk_File = CInt(ItemFile.Item("fk_Cargue_Item"))
                                    Else
                                        row.fk_Expediente = CInt(ItemFile.Item("fk_Expediente"))
                                        row.fk_Folder = CInt(ItemFile.Item("fk_Folder"))
                                        row.fk_File = CInt(ItemFile.Item("fk_File"))
                                    End If

                                    row.nombreImagen = ItemFile.Item("Nombre_Imagen_Folio")

                                    DataTableRegistosPorCarpeta.Rows.Add(row)
                                    DataTableRegistosPorCarpeta.AcceptChanges()
                                End If
                                Contador = Contador + 1

                                If Contador = nproceso.cantidadImagenes Or Contador = FilesbyGroupDataView.Count Or ItemFile.Item("idRegistro") = FilesbyGroupDataView.Count Then
                                    'Linea totalizadora por archivo
                                    sw = New StreamWriter(File.Open(OutputFolderCarpeta & "\" & NomArchivo, FileMode.Append), New Text.UTF8Encoding(False))
                                    sw.WriteLine("Total;" & Contador)
                                    sw.Close()
                                    sw.Dispose()

                                    Const tabla As String = "#RegistrosPorCarpeta"
                                    If DataTableRegistosPorCarpeta IsNot Nothing AndAlso DataTableRegistosPorCarpeta.Rows.Count > 0 Then
                                        BulkInsert.InsertDataTableCore(DataTableRegistosPorCarpeta.CopyToDataTable(), dbmCore, tabla)

                                        'Inserción finalización proceso BD
                                        dbmCore.SchemaProcess.PA_Set_Generacion_Proceso_Automatico_Detalle.DBExecute(nproceso.id_Proceso_Automatico, nproceso.fk_Entidad, nproceso.fk_Proyecto, nproceso.fk_Esquema, nproceso.fk_Estado, id_Generacion_Proceso_Automatico, tabla)
                                    End If

                                    idProcesosEncarpetados = idProcesosEncarpetados & "<tr><td align=""Center"">" & ItemFile.Item("Nombre_Esquema") & "</td><td align=""Center"">" & OutputFolderCarpeta.Substring(OutputFolderCarpeta.LastIndexOf("\") + 1) & "</td><td align=""Center"">" & Contador & "</td></tr>"

                                    CrearNuevaCarpeta = True
                                    Contador = 0
                                    DataTableRegistosPorCarpeta = New DBCore.SchemaProcess.CTA_Set_Exportacion_DigicomDataTable

                                    Dim nombreArchivo As String = OutputFolderCarpeta

                                    'comprimir
                                    If nproceso.Comprimir Then
                                        If nproceso.Extension_Comprimido = ".zip" Then
                                            comprimirzip(OutputFolderCarpeta)
                                            nombreArchivo = nombreArchivo + ".zip"
                                        End If
                                    End If

                                    'mover carpeta
                                    If nproceso.usa_Mover_A_Ruta And Not nproceso.Ispath_Ruta_A_MoverNull And nproceso.path_Ruta_A_Mover <> "" Then
                                        File.Move(nombreArchivo, nproceso.path_Ruta_A_Mover + Path.GetFileName(nombreArchivo))
                                    End If

                                    'si no ha pasado un minuto esperar el minuto
                                    If datetimenow = Date.Now.ToString("HHmm") Then
                                        System.Threading.Thread.Sleep(60000)
                                    End If
                                End If
                            Next
                        Catch ex As Exception
                            GeneracionProcesoAutomaticoType.valido = False
                            GeneracionProcesoAutomaticoType.Observaciones = ex.Message.ToString()

                            dbmCore.SchemaProcess.TBL_Generacion_Proceso_Automatico.DBUpdate(GeneracionProcesoAutomaticoType, id_Generacion_Proceso_Automatico)
                        End Try
                    Next
                    'enviar correo
                    idProcesosEncarpetados = idProcesosEncarpetados & "</table>"
                    If nproceso.Genera_Envio_Reporte Then
                        EnvioReporte(nproceso, idProcesosEncarpetados)
                    End If
                Else
                    WriteErrorLog("No hay registros para transmitir: " & nproceso.Nombre_Proceso_Automatico)
                End If
            Catch ex As Exception
                WriteErrorLog("Error Proceso ex: " & ex.ToString())
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub EnvioReporte(ByVal nproceso As DBCore.SchemaConfig.CTA_Get_Proceso_AutomaticoRow, ByVal nidProcesosEncarpetados As String)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)

                dbmCore.Connection_Open(1)

                Dim Notificacion = dbmCore.SchemaConfig.PA_Get_Notificacion.DBExecute(nproceso.fk_Notificacion)

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

                        CuerpoCorreo = CorreoDatatable(0).CUERPO.Replace("@Tabla", nidProcesosEncarpetados)

                        Message = CorreoDatatable(0).SALUDO & CuerpoCorreo & CorreoDatatable(0).FIRMA
                        MailTo = CorreoDatatable(0).CORREOS
                        Subject = Replace(CorreoDatatable(0).ASUNTO, "@consecutivo", DateTime.Now.ToString("yyyyMMddHHmmss"))

                        Dim dbmTools As DBTools.DBToolsDataBaseManager = Nothing

                        Try
                            dbmTools = New DBTools.DBToolsDataBaseManager(Program.ConnectionStrings.Tools)
                            dbmTools.Connection_Open()

                            dbmTools.InsertMail(nproceso.fk_Entidad, 2, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)

                        Catch ex As Exception
                            WriteErrorLog("Error proceso envío notificación proceso: " & nproceso.Nombre_Proceso_Automatico & ", notificación: " & Notificacion(0).Nombre_Notificacion & ", ex: " & ex.Message.ToString())
                        Finally
                            If (dbmTools IsNot Nothing) Then dbmTools.Connection_Close()
                        End Try
                    End If
                Else
                    WriteErrorLog("No se encuentra configuración para envio de correo del proceso: " & nproceso.Nombre_Proceso_Automatico & ", fk_Notificaion: " & nproceso.fk_Notificacion.ToString())
                End If

            Catch ex As Exception
                WriteErrorLog("Error EnvioReporte ex: " & ex.ToString())
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub EnvioReporteFilenet(ByVal nproceso As DBCore.SchemaConfig.CTA_Get_Proceso_AutomaticoRow)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)

                dbmCore.Connection_Open(1)

                Dim Notificacion = dbmCore.SchemaConfig.PA_Get_Notificacion.DBExecute(nproceso.fk_Notificacion_Adicional)

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

                        Message = CorreoDatatable(0).SALUDO & CuerpoCorreo & CorreoDatatable(0).FIRMA
                        MailTo = CorreoDatatable(0).CORREOS
                        Subject = CorreoDatatable(0).ASUNTO

                        If Not nproceso.Isreporte_Notificacion_AdicionalNull AndAlso nproceso.reporte_Notificacion_Adicional <> "" Then
                            Dim respuestaDatatable = dbmCore.SchemaProcess.PA_Proceso_Automatico_Reporte_Adicional.DBExecute(nproceso.id_Proceso_Automatico, nproceso.fk_Entidad, nproceso.fk_Proyecto, nproceso.fk_Esquema, nproceso.reporte_Notificacion_Adicional)

                            If respuestaDatatable IsNot Nothing AndAlso respuestaDatatable.Rows.Count > 0 Then
                                Dim usa_encabezado As Boolean = nproceso.usa_Encabezado
                                Dim caracterSeparador As String = If(String.IsNullOrEmpty(nproceso.separador_Archivo_Reporte), ";", nproceso.separador_Archivo_Reporte)
                                nAttachName = ValidacionesSplit(nproceso.estructura_Nombre_Archivo_Reporte.Split("|"c), Nothing)

                                If nproceso.extension_Archivo_Reporte = ".csv" Then
                                    Using ms As New MemoryStream()
                                        Using sw As New StreamWriter(ms, New Text.UTF8Encoding(False))
                                            ' Escribir encabezado si aplica
                                            If usa_encabezado Then
                                                Dim encabezado = String.Join(caracterSeparador, respuestaDatatable.Columns.Cast(Of DataColumn).Select(Function(c) c.ColumnName))
                                                sw.WriteLine(encabezado)
                                            End If

                                            ' Escribir datos
                                            For Each fila As DataRow In respuestaDatatable.Rows
                                                Dim valores = respuestaDatatable.Columns.Cast(Of DataColumn).Select(Function(c) fila(c).ToString())
                                                sw.WriteLine(String.Join(caracterSeparador, valores))
                                            Next

                                            sw.Flush()
                                            nAttach = ms.ToArray()
                                            nAttachName = nAttachName + nproceso.extension_Archivo_Reporte
                                        End Using
                                    End Using
                                End If

                                Dim dbmTools As DBTools.DBToolsDataBaseManager = Nothing

                                Try
                                    dbmTools = New DBTools.DBToolsDataBaseManager(Program.ConnectionStrings.Tools)
                                    dbmTools.Connection_Open()

                                    dbmTools.InsertMail(nproceso.fk_Entidad, 2, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)

                                Catch ex As Exception
                                    WriteErrorLog("Error proceso envío notificación proceso: " & nproceso.Nombre_Proceso_Automatico & ", notificación: " & Notificacion(0).Nombre_Notificacion & ", ex: " & ex.Message.ToString())
                                Finally
                                    If (dbmTools IsNot Nothing) Then dbmTools.Connection_Close()
                                End Try
                            End If
                        End If
                    End If
                Else
                    WriteErrorLog("No se encuentra configuración para envio de correo del proceso: " & nproceso.Nombre_Proceso_Automatico & ", fk_Notificaion: " & nproceso.fk_Notificacion.ToString())
                End If

            Catch ex As Exception
                WriteErrorLog("Error EnvioReporte ex: " & ex.ToString())
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub comprimr(ByVal nproceso As DBCore.SchemaConfig.CTA_Get_Proceso_AutomaticoRow, ByVal noutputfolder As String)
            Dim DirectoryNames As String()
            DirectoryNames = Directory.GetDirectories(noutputfolder)

            Select Case nproceso.Extension_Comprimido
                Case ".zip"
                    For Each directorio In DirectoryNames
                        comprimirzip(directorio)
                    Next
            End Select
        End Sub

        Private Sub comprimirzip(ByVal noutputfolderCarpeta As String)
            Try
                Dim ZipFileName As String = noutputfolderCarpeta & ".zip"

                Using zip As Ionic.Zip.ZipFile = New Ionic.Zip.ZipFile()
                    zip.AddDirectory(noutputfolderCarpeta)
                    zip.Save(ZipFileName)
                End Using

                Directory.Delete(noutputfolderCarpeta, True)
            Catch ex As Exception
                WriteErrorLog("Error comprimirzip ex: " & ex.ToString())
            End Try
        End Sub

        Private Sub ExportarImagen(ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nItemFile As DataRowView, nCompresion As ImageManager.EnumCompression, nformato As ImageManager.EnumFormat, nFolderName As String, ByVal nformatoSalida As String, ByVal nOutPutFolderTemp As String, ByRef ImagenValida As Boolean)

            Dim FileNames As New List(Of String)
            Dim FileName As String = Nothing
            Dim FileNameAux As String = Nothing
            Dim ExtensionAux As String = String.Empty

            Dim manager As FileProviderManager = Nothing

            Try
                Dim fk_Expediente As Long = CLng(nItemFile.Item("fk_Expediente"))
                Dim fk_Folder As Short = CShort(nItemFile.Item("fk_Folder"))
                Dim fk_File As Short = CShort(nItemFile.Item("fk_File"))
                Dim fk_Version As Short = CShort(nItemFile.Item("fk_Version"))

                manager = New FileProviderManager(fk_Expediente, fk_Folder, fk_File, fk_Version, dbmImaging, 1)
                manager.Connect()

                Dim Folios = manager.GetFolios(fk_Expediente, fk_Folder, fk_File, fk_Version)

                For folio As Short = 1 To Folios
                    Dim Imagen() As Byte = Nothing
                    Dim Thumbnail() As Byte = Nothing

                    manager.GetFolio(fk_Expediente, fk_Folder, fk_File, fk_Version, folio, Imagen, Thumbnail)

                    FileName = nOutPutFolderTemp & Guid.NewGuid().ToString() & nformatoSalida
                    FileNames.Add(FileName)

                    Using fs = New FileStream(FileName, FileMode.Create)
                        fs.Write(Imagen, 0, Imagen.Length)
                        fs.Close()
                    End Using
                Next

                If (FileNames.Count > 0) Then
                    Dim Format As ImageManager.EnumFormat

                    Select Case nformatoSalida
                        Case ".bmp"
                            Format = ImageManager.EnumFormat.Bmp
                        Case ".gif"
                            Format = ImageManager.EnumFormat.Gif
                        Case ".jpg"
                            Format = ImageManager.EnumFormat.Jpeg
                            nCompresion = ImageManager.EnumCompression.Jpeg
                        Case ".pdf"
                            Format = ImageManager.EnumFormat.Pdf
                            nCompresion = ImageManager.EnumCompression.Jpeg
                        Case ".png"
                            Format = ImageManager.EnumFormat.Png
                        Case ".tif"
                            Format = ImageManager.EnumFormat.Tiff
                    End Select

                    FileName = nFolderName & "\" & nItemFile.Item("Nombre_Imagen_Folio").ToString()

                    ImageManager.Save(FileNames, FileName, "", nformato, nCompresion, False, nOutPutFolderTemp, True)
                    ImagenValida = True
                End If

            Catch ex As Exception
                WriteErrorLog("Error exportando imagen fk_Expediente: " & nItemFile.Item("fk_Expediente").ToString() & ", fk_Folder: " & nItemFile.Item("fk_Folder").ToString() & ", fk_File: " & nItemFile.Item("fk_File").ToString() & ": " & ex.ToString())
            Finally
                If (manager IsNot Nothing) Then
                    manager.Disconnect()
                End If

                ''borrar archivos temporales
                For Each FileName In FileNames
                    File.Delete(FileName)
                Next
            End Try

        End Sub

        Private Sub ExportarImagenCargue(ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nItemFile As DataRowView, nCompresion As ImageManager.EnumCompression, nformato As ImageManager.EnumFormat, nFolderName As String, ByVal nformatoSalida As String, ByVal nOutPutFolderTemp As String, ByRef ImagenValida As Boolean)

            Dim FileNames As New List(Of String)
            Dim FileName As String = Nothing
            Dim FileNameAux As String = Nothing
            Dim ExtensionAux As String = String.Empty

            Dim manager As FileProviderManager = Nothing

            Try
                Dim fk_Cargue As Integer = CInt(nItemFile.Item("fk_Cargue"))
                Dim fk_Cargue_Paquete As Short = CShort(nItemFile.Item("fk_Cargue_Paquete"))
                Dim fk_Cargue_Item As Integer = CInt(nItemFile.Item("fk_Cargue_Item"))

                manager = New FileProviderManager(fk_Cargue, Me._centro, dbmImaging, 1)
                manager.Connect()

                Dim Folios = manager.GetFolios(fk_Cargue, fk_Cargue_Paquete, fk_Cargue_Item)

                For folio As Short = 1 To Folios
                    Dim Imagen() As Byte = Nothing
                    Dim Thumbnail() As Byte = Nothing

                    manager.GetFolio(fk_Cargue, fk_Cargue_Paquete, fk_Cargue_Item, folio, Imagen, Thumbnail)

                    FileName = nOutPutFolderTemp & Guid.NewGuid().ToString() & nformatoSalida
                    FileNames.Add(FileName)

                    Using fs = New FileStream(FileName, FileMode.Create)
                        fs.Write(Imagen, 0, Imagen.Length)
                        fs.Close()
                    End Using
                Next

                If (FileNames.Count > 0) Then
                    Dim Format As ImageManager.EnumFormat

                    Select Case nformatoSalida
                        Case ".bmp"
                            Format = ImageManager.EnumFormat.Bmp
                        Case ".gif"
                            Format = ImageManager.EnumFormat.Gif
                        Case ".jpg"
                            Format = ImageManager.EnumFormat.Jpeg
                            nCompresion = ImageManager.EnumCompression.Jpeg
                        Case ".pdf"
                            Format = ImageManager.EnumFormat.Pdf
                            nCompresion = ImageManager.EnumCompression.Jpeg
                        Case ".png"
                            Format = ImageManager.EnumFormat.Png
                        Case ".tif"
                            Format = ImageManager.EnumFormat.Tiff
                    End Select

                    FileName = nFolderName & "\" & nItemFile.Item("Nombre_Imagen_Folio").ToString()

                    ImageManager.Save(FileNames, FileName, "", nformato, nCompresion, False, nOutPutFolderTemp, True)
                    ImagenValida = True
                End If

            Catch ex As Exception
                WriteErrorLog("Error exportando imagen fk_Cargue: " & CInt(nItemFile.Item("fk_Cargue")) & ", fk_Cargue_Paquete: " & CShort(nItemFile.Item("fk_Cargue_Paquete")) & ", fk_Cargue_Item: " & CInt(nItemFile.Item("fk_Cargue_Item")) & ": " & ex.ToString())
            Finally
                If (manager IsNot Nothing) Then
                    manager.Disconnect()
                End If

                ''borrar archivos temporales
                For Each FileName In FileNames
                    File.Delete(FileName)
                Next
            End Try

        End Sub

        Private Function ValidacionesSplit(ByVal StrValidacionColumn As String(), ByVal nItemFile As DataRowView) As String
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

                            For Each columna In nItemFile.Row.Table.Columns
                                If columna.ToString() = "Sigla" Then
                                    retorno = nItemFile.Item("Sigla").ToString()
                                End If
                            Next
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

            'SyncLock objectLock

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

            'End SyncLock
        End Sub

        Public Sub JWriteLog(ByVal mensaje As String, ByVal tipo As EventLogEntryType)

            If Not EventLog.SourceExists("DDOTransferService") Then
                EventLog.CreateEventSource("DDOTransferService", "Application")
            End If

            Dim eventLog1 As EventLog = New EventLog()
            eventLog1.Source = "DDOTransferService"
            eventLog1.WriteEntry(mensaje, tipo)

        End Sub
#End Region

    End Class
End Namespace
