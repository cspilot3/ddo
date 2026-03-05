Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Security.Policy
Imports System.Threading
Imports System.Windows.Forms
Imports DBCore
Imports DBCore.SchemaConfig
Imports DBCore.Schemadbo
Imports DBCore.SchemaProcess
Imports GdPicture12
Imports ImageMagick
Imports ImageMagick.Formats
Imports Miharu.Desktop.Library.Config
Imports Miharu.FileProvider.Library
Imports Miharu.Security.Library.WebService
Imports Slyg.Tools.Imaging

Namespace Servicio
    Public Class DDOEnvioCorreoService

#Region " Declaraciones "
        Private Detener As Boolean
        Private _centro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType
        Private ThreadArchivosD As Thread
        Private ThreadArchivosF As Thread
        Private DiaInicial As Integer
        Private DiaFinal As Integer
        Private ErrorMsg As String
        Private RutaLog As String = Application.StartupPath & "\Log\DDOEnvioCorreoService_" & DateTime.Now.ToString("yyyyMMdd") & ".log"
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
            If (File.Exists(Program.AppDataPath + DDOEnvioCorreoConfig.ConfigFileName)) Then
                Program.Config = DDOEnvioCorreoConfig.Deserialize(Program.AppDataPath)
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
                WebService.setUser(Program.Config.User, DDOEnvioCorreoConfig.Decrypt(Program.Config.Password))
                Program.ConnectionStrings = DDOEnvioCorreoConfig.getCadenasConexion(WebService)

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
                        Dim EnvioCorreosDataTable = dbmCore.SchemaConfig.PA_Consulta_Envio_Correos_Get.DBExecute("EnvioCorreos")
                        Try
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                        Catch ex As Exception : End Try

                        If EnvioCorreosDataTable.Rows.Count = 0 Then
                            WriteErrorLog("00- No existe configuración para el proceso de envio de correos automático")
                            Return
                        End If

                        For Each RowParam As DataRow In EnvioCorreosDataTable.Rows
                            DiaInicial = RowParam.Item("Dia_Inicial")
                            DiaFinal = RowParam.Item("Dia_Final")

                            If fecha.Day >= DiaInicial AndAlso fecha.Day <= DiaFinal Then
                                Dim CalendarioDataTable = dbmSecurity.SchemaConfig.TBL_Calendario.DBFindByfk_EntidadNombre_Calendario(1, "EnvioCorreos")
                                If CalendarioDataTable.Rows.Count > 0 Then
                                    Dim habil = dbmSecurity.SchemaConfig.PA_Es_Hora_Habil.DBExecute(CalendarioDataTable(0).fk_Entidad, CalendarioDataTable(0).id_Calendario)

                                    If (habil) And DateTime.Now.Hour >= Val(RowParam.Item("Hora_Inicio")) Then
                                        EnviarCorreos(RowParam)
                                        'Else
                                        ' WriteErrorLog("No es una hora habil para ejecutar el proceso envio de correos automático")
                                    End If
                                Else
                                    WriteErrorLog("No existe el calendario del servicio de envio de correos automático")
                                End If
                            Else
                                WriteErrorLog("No esta en las fechas establecidas de envio de correos automático")
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

        Private Sub EnviarCorreos(RowParam As DataRow)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim OutputFolder As String = ""
            If Trim(Program.Config.RutaLog) <> "" Then
                OutputFolder = Mid(Program.Config.RutaLog, 1, InStrRev(Program.Config.RutaLog, "\"))
            End If
            Dim EnviarCorreosDataTable As New DataTable
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(1)
                ' Actualiza el control de envío de correo automático
                Dim ActualizaEnviosDataTable = dbmImaging.SchemaProcess.PA_Crea_Control_Envio_Correo_Automatico.DBExecute(CInt(RowParam.Item("id_Envio_Correo")), CInt(RowParam.Item("fk_Documento")), CLng(RowParam.Item("fk_Estado_Expediente")), CLng(RowParam.Item("Dias_Caida")))

                ' Consulta los correos a enviar
                EnviarCorreosDataTable = dbmImaging.SchemaProcess.PA_Consulta_Envio_Correos_Automatico.DBExecute(CInt(RowParam.Item("id_Envio_Correo")), CInt(RowParam.Item("enviosxHilo")))

                If Not EnviarCorreosDataTable.Rows.Count > 0 Then
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    Exit Sub
                End If
            Catch ex As Exception
                ErrorMsg = "Error consultando registros envio correo. Linea 0  ex: " & ex.ToString()
                WriteErrorLog(ErrorMsg)
                Exit Sub
            End Try
            For Each RowCorreo As DataRow In EnviarCorreosDataTable.Rows
                Try
                    'Consultar la imagen del expediente para el correo, si no se encuentra la imagen marcar error en la base de datos y continuar.
                    Dim ImagenDatatable = dbmImaging.SchemaProcess.PA_Consulta_Imagen_Envio_Correo.DBExecute(CLng(RowCorreo.Item("fk_Expediente")), CShort(RowCorreo.Item("fk_Folder")), CShort(RowCorreo.Item("fk_File")), CInt(RowParam.Item("fk_Esquema")), CInt(RowParam.Item("fk_Documento")))
                    If Not ImagenDatatable.Rows.Count > 0 Then
                        ErrorMsg = "Error.  No se encontro imagen para el expediente: " & RowCorreo.Item("fk_Expediente") & Chr(13).ToString.Trim & " No fue posible crear correo para este documento."
                        Dim Resp = dbmImaging.SchemaProcess.PA_Actualiza_Estado_Envio_Correo.DBExecute(CInt(RowCorreo.Item("fk_Envio_Correo")), CLng(RowCorreo.Item("fk_Expediente")), CShort(RowCorreo.Item("fk_Folder")), CShort(RowCorreo.Item("fk_File")), 0, RowCorreo.Item("fk_Expediente").ToString.Trim, 0, 1, ErrorMsg)
                        WriteErrorLog(ErrorMsg)
                        Continue For
                    End If
                    Dim Asunto As String = ""
                    Dim ArcImagen As String = ""
                    'Validamos que el campo identificacion este capturado
                    Dim Identificacion As String = ImagenDatatable.Rows(0).Field(Of String)("Identificacion")
                    If String.IsNullOrWhiteSpace(Identificacion) = True Then
                        ImagenDatatable.Rows(0).Item("Identificacion") = "0"
                    End If

                    Asunto = Trim(ImagenDatatable.Rows(0).Item("Oficina")) & " - "
                    Asunto &= Trim(ImagenDatatable.Rows(0).Item("Identificacion")) & " - "
                    Asunto &= Format(CDate(ImagenDatatable.Rows(0).Item("Fecha")), "yyyyMMdd") & " - Legalizaciones"

                    ArcImagen = Trim(ImagenDatatable.Rows(0).Item("Identificacion")) & "_"
                    ArcImagen &= Trim(RowParam.Item("nombreDocumento")) & "_"
                    ArcImagen &= Trim(ImagenDatatable.Rows(0).Item("fk_Expediente"))
                    ArcImagen &= Trim(RowParam.Item("extension_Archivo"))
                    ' Crear el directorio temp de las imágenes
                    If Trim(OutputFolder) = "" Then
                        'OutputFolder = "C:\1\Borrar\H\Temp\" & Trim(ImagenDatatable.Rows(0).Item("fk_Expediente"))
                        OutputFolder = Application.StartupPath & "\Temp\" & Trim(ImagenDatatable.Rows(0).Item("fk_Expediente"))
                    Else
                        OutputFolder &= "Temp\" & Trim(ImagenDatatable.Rows(0).Item("fk_Expediente"))
                    End If
                    Try
                        If Not Directory.Exists(OutputFolder) Then
                            Directory.CreateDirectory(OutputFolder)
                        End If
                    Catch ex As Exception
                        ErrorMsg = "Error.  No fue posible crear la carpeta : " & OutputFolder & "Temp" & Chr(13) & " No fue posible crear correo para este documento."
                        Dim Resp = dbmImaging.SchemaProcess.PA_Actualiza_Estado_Envio_Correo.DBExecute(CInt(RowCorreo.Item("fk_Envio_Correo")), CLng(RowCorreo.Item("fk_Expediente")), CShort(RowCorreo.Item("fk_Folder")), CShort(RowCorreo.Item("fk_File")), 0, Asunto, 0, 1, ErrorMsg)
                        WriteErrorLog(ErrorMsg)
                        Continue For
                    End Try
                    Try
                        'adjuntarArchivo, extension_Archivo, notificacion_Envio, notificacion_Error, peso_Maximo_Adjunto,
                        If RowParam.Item("adjuntarArchivo") = 1 Then
                            If ExportarImagenFilenet(dbmImaging, ImagenDatatable, OutputFolder, Trim(RowParam.Item("extension_Archivo")), OutputFolder & "\" & ArcImagen, Val(RowParam.Item("peso_Maximo_Adjunto"))) = False Then
                                'Marcar error en base de datos enviado en la base de datos
                                Dim Resp = dbmImaging.SchemaProcess.PA_Actualiza_Estado_Envio_Correo.DBExecute(CInt(RowCorreo.Item("fk_Envio_Correo")), CLng(RowCorreo.Item("fk_Expediente")), CShort(RowCorreo.Item("fk_Folder")), CShort(RowCorreo.Item("fk_File")), 0, Asunto, 0, 1, ErrorMsg)
                                WriteErrorLog(ErrorMsg)
                                Try
                                    Directory.Delete(OutputFolder, True)
                                Catch : End Try
                                Continue For
                            End If
                            'Esperar 5 segundos para que el archivo se libere y pueda ser adjuntado al correo
                            System.Threading.Thread.Sleep(5000)
                            If File.Exists(OutputFolder & "\" & ArcImagen) Then
                                'Enviar correo con el archivo adjunto
                                'Validar peso del archivo
                                Dim fileInfo As New FileInfo(OutputFolder & "\" & ArcImagen)
                                If fileInfo.Length <= (Val(RowParam.Item("peso_Maximo_Adjunto")) * 1024) Then
                                    If Trim(ImagenDatatable.Rows(0).Item("Identificacion")) <> "0" Then
                                        EnviarNotificacion(CInt(RowParam.Item("notificacion_Envio")), Asunto, OutputFolder & "\" & ArcImagen, "", RowParam)
                                        'Marcar como enviado en la base de datos
                                        Dim Resp = dbmImaging.SchemaProcess.PA_Actualiza_Estado_Envio_Correo.DBExecute(CInt(RowCorreo.Item("fk_Envio_Correo")), CLng(RowCorreo.Item("fk_Expediente")), CShort(RowCorreo.Item("fk_Folder")), CShort(RowCorreo.Item("fk_File")), 1, Asunto, CInt((fileInfo.Length / 1024)), 0, "")
                                    Else
                                        'Marcar error en la base de datos por peso del archivo excedido
                                        ErrorMsg = "Error. No se capturo información para el número de identificación. Expediente: " & Trim(RowCorreo.Item("fk_Expediente"))
                                        Dim Resp = dbmImaging.SchemaProcess.PA_Actualiza_Estado_Envio_Correo.DBExecute(CInt(RowCorreo.Item("fk_Envio_Correo")), CLng(RowCorreo.Item("fk_Expediente")), CShort(RowCorreo.Item("fk_Folder")), CShort(RowCorreo.Item("fk_File")), 0, Asunto, CInt((fileInfo.Length / 1024)), 1, ErrorMsg)
                                        WriteErrorLog(ErrorMsg)
                                        EnviarNotificacionError(CInt(RowParam.Item("notificacion_Error")), Asunto, OutputFolder & "\" & ArcImagen, ErrorMsg, RowParam)
                                    End If
                                Else
                                    'Marcar error en la base de datos por peso del archivo excedido
                                    ErrorMsg = "Error. El peso del archivo adjunto excede el máximo permitido. Peso Archivo: " & CInt((fileInfo.Length / 1024)) & " Kb. Peso Máximo Permitido: " & RowParam.Item("peso_Maximo_Adjunto") & " KB. Expediente: " & Trim(RowCorreo.Item("fk_Expediente"))
                                    Dim Resp = dbmImaging.SchemaProcess.PA_Actualiza_Estado_Envio_Correo.DBExecute(CInt(RowCorreo.Item("fk_Envio_Correo")), CLng(RowCorreo.Item("fk_Expediente")), CShort(RowCorreo.Item("fk_Folder")), CShort(RowCorreo.Item("fk_File")), 0, Asunto, CInt((fileInfo.Length / 1024)), 1, ErrorMsg)
                                    WriteErrorLog(ErrorMsg)
                                    EnviarNotificacionError(CInt(RowParam.Item("notificacion_Error")), Asunto, OutputFolder & "\" & ArcImagen, ErrorMsg, RowParam)
                                End If
                                Try
                                    Directory.Delete(OutputFolder, True)
                                Catch : End Try
                            Else
                                'Archivo no encontrado, marcar error en la base de datos
                                ErrorMsg = "Error. archivo no fue generado en disco para su envio por correo. Expediente: " & RowCorreo.Item("fk_Expediente") & " Archivo:" & ArcImagen
                                Dim Resp = dbmImaging.SchemaProcess.PA_Actualiza_Estado_Envio_Correo.DBExecute(CInt(RowCorreo.Item("fk_Envio_Correo")), CLng(RowCorreo.Item("fk_Expediente")), CShort(RowCorreo.Item("fk_Folder")), CShort(RowCorreo.Item("fk_File")), 0, Asunto, 0, 1, ErrorMsg)
                                WriteErrorLog(ErrorMsg)
                                EnviarNotificacionError(CInt(RowParam.Item("notificacion_Error")), Asunto, OutputFolder & "\" & ArcImagen, ErrorMsg, RowParam)
                                Try
                                    Directory.Delete(OutputFolder, True)
                                Catch : End Try
                            End If
                        Else
                            'Enviar correo sin el archivo adjunto
                            EnviarNotificacion(CInt(RowParam.Item("notificacion_Envio")), Asunto, OutputFolder & "\" & ArcImagen, "", RowParam)
                            'Marcar como enviado en la base de datos
                            Dim Resp = dbmImaging.SchemaProcess.PA_Actualiza_Estado_Envio_Correo.DBExecute(CInt(RowCorreo.Item("fk_Envio_Correo")), CLng(RowCorreo.Item("fk_Expediente")), CShort(RowCorreo.Item("fk_Folder")), CShort(RowCorreo.Item("fk_File")), 1, Asunto, 0, 0, "")
                            Try
                                Directory.Delete(OutputFolder, True)
                            Catch : End Try
                        End If
                    Catch ex As Exception
                        'Marcar error en la base de datos con el error y continuar con el siguiente registro
                        ErrorMsg = "Error no fue posible enviar correo  ex: " & ex.ToString()
                        WriteErrorLog(ErrorMsg)
                        Dim Resp = dbmImaging.SchemaProcess.PA_Actualiza_Estado_Envio_Correo.DBExecute(CInt(RowCorreo.Item("fk_Envio_Correo")), CLng(RowCorreo.Item("fk_Expediente")), CShort(RowCorreo.Item("fk_Folder")), CShort(RowCorreo.Item("fk_File")), 0, Asunto, 0, 1, ErrorMsg)
                        Try
                            Directory.Delete(OutputFolder, True)
                        Catch : End Try
                    End Try
                Catch ex As Exception
                    ErrorMsg = "Error no fue posible enviar correo ex: " & ex.ToString()
                    WriteErrorLog(ErrorMsg)
                End Try
            Next
            Try
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            Catch ex As Exception
            End Try

        End Sub

        Private Function ExportarImagenFilenet(ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nItemFile As DataTable, nFolderName As String, ByVal nformatoSalida As String, ByVal nOutPutFolderTemp As String, ByVal PesoMax As Integer) As Boolean
            ExportarImagenFilenet = True
            Dim FileNames As New List(Of String)
            ErrorMsg = ""
            'RowImagen
            'Sigla, Oficina, Fecha, Identificacion,fk_Expediente, fk_Folder, fk_File, id_File_Record_Folio, Image_Binary, Thumbnail_Binary
            Try
                For Each Folio As DataRow In nItemFile.Rows
                    Dim Imagen() As Byte
                    Imagen = Folio.Item("Image_Binary")

                    Dim tempFileName = nFolderName & "\" & Path.GetFileNameWithoutExtension(nOutPutFolderTemp) & "_" & Folio.Item("id_File_Record_Folio").ToString & Trim(nformatoSalida)

                    Using fs As New FileStream(tempFileName, FileMode.Create, FileAccess.Write)
                        fs.Write(Imagen, 0, Imagen.Length)
                    End Using
                    FileNames.Add(tempFileName)
                Next

                If (FileNames.Count > 0) Then
                    Try
                        MergeWithMagick(FileNames, nOutPutFolderTemp, nformatoSalida, False)
                        'Validar peso y generar en Blanco y Negro si pesa demaciado.
                        Dim fileInfo As New FileInfo(nOutPutFolderTemp)
                        If fileInfo.Length > (PesoMax * 1024) Then
                            MergeWithMagick(FileNames, nOutPutFolderTemp, nformatoSalida, True)
                        End If
                    Catch ex As Exception
                        ErrorMsg = "Error exportando imagen fk_Expediente: " & nItemFile.Rows(0).Item("fk_Expediente").ToString() & ", fk_Folder: " & nItemFile.Rows(0).Item("fk_Folder").ToString() & ", fk_File: " & nItemFile.Rows(0).Item("fk_File").ToString() & ": " & ex.ToString()
                        Return False
                    End Try
                    '  ImageManager.Save(FileNames, finalFileName, "", Format, nCompresion, False, Path.GetDirectoryName(nOutPutFolderTemp), False)
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                ErrorMsg = "Error exportando imagen fk_Expediente: " & nItemFile.Rows(0).Item("fk_Expediente").ToString() & ", fk_Folder: " & nItemFile.Rows(0).Item("fk_Folder").ToString() & ", fk_File: " & nItemFile.Rows(0).Item("fk_File").ToString() & ": " & ex.ToString()
                WriteErrorLog(ErrorMsg)
                Return False
            End Try
        End Function

        Sub MergeWithMagick(inputFiles As IEnumerable(Of String), outputFile As String, Formato As String, BN As Boolean)
            If Formato.ToUpper = ".TIF" Then
                Using collection As New MagickImageCollection()
                    For Each f In inputFiles
                        Dim img As New MagickImage(f)
                        If BN = False Then
                            'Escala de grises al 50 queda en 198kb
                            img.Strip()
                            img.ColorSpace = ColorSpace.Gray
                            img.FilterType = FilterType.Triangle
                            img.Contrast()
                            img.Despeckle()
                            img.MedianFilter(1)
                            img.SelectiveBlur(1.5, 1.0, 0.02)
                            img.Alpha(AlphaOption.Remove)
                            img.Resize(New Percentage(50))
                            img.Format = MagickFormat.Tiff
                            img.Settings.Compression = CompressionMethod.JPEG
                            img.Quality = 60    ' 60–80 suele ser el “sweet spot”
                        Else
                            ''FORMATO BLANCO Y NEGRO
                            img.Strip()
                            img.ColorSpace = ColorSpace.Gray
                            img.Alpha(AlphaOption.Remove)
                            img.FilterType = FilterType.Triangle
                            img.Despeckle()                ' quita punticos/“sal-pimienta” suaves
                            img.MedianFilter(1)            ' no subas mucho para no adelgazar trazos
                            img.SelectiveBlur(1.2, 0.8, 0.02) ' suaviza ruido de bajo contraste sin “lavar” bordes
                            img.Resize(New Percentage(70)) ' ajusta según tu caso (60–70% si quieres más detalle)
                            img.AutoLevel()
                            img.ColorType = ColorType.Bilevel
                            img.Settings.SetDefine(MagickFormat.Tiff, "bits-per-sample", "1")
                            img.Settings.SetDefine(MagickFormat.Tiff, "samples-per-pixel", "1")
                            img.Format = MagickFormat.Tiff
                            img.Settings.Compression = CompressionMethod.Group4
                            img.Settings.SetDefine(MagickFormat.Tiff, "photometric", "miniswhite")
                            img.Settings.SetDefine(MagickFormat.Tiff, "rows-per-strip", "0")
                        End If
                        collection.Add(img)
                    Next
                    ' Importante: usa la sobrecarga (String, IWriteDefines)
                    Dim defines As New TiffWriteDefines() With {
                    .PreserveCompression = False}
                    collection.Write(outputFile, defines)
                End Using
            Else   'PDF
                Using collection As New MagickImageCollection()
                    For Each f In inputFiles
                        Dim img As New MagickImage(f)
                        img.Format = MagickFormat.Pdf
                        collection.Add(img)
                    Next
                    collection.Write(outputFile)
                End Using
            End If
        End Sub


        'Sub MergeWithMagick(inputFiles As IEnumerable(Of String), outputFile As String, Formato As String)
        '    If Formato.ToUpper = ".TIF" Then
        '        Using collection As New MagickImageCollection()
        '            For Each f In inputFiles
        '                Dim img As New MagickImage(f)
        '                img.Strip()
        '                img.ColorSpace = ColorSpace.Gray
        '                img.Settings.Compression = CompressionMethod.LZW   '  o Zip  
        '                img.Settings.SetDefine(MagickFormat.Tiff, "predictor", "2")
        '                img.Format = MagickFormat.Tiff
        '                collection.Add(img)
        '            Next
        '            ' Importante: usa la sobrecarga (String, IWriteDefines)
        '            Dim defines As New TiffWriteDefines() With {
        '                .PreserveCompression = True}
        '            collection.Write(outputFile, defines)
        '        End Using
        '    Else   'PDF
        '        Using collection As New MagickImageCollection()
        '            For Each f In inputFiles
        '                Dim img As New MagickImage(f)
        '                img.Format = MagickFormat.Pdf
        '                collection.Add(img)
        '            Next
        '            collection.Write(outputFile)
        '        End Using
        '    End If
        'End Sub

        Private Sub EnviarNotificacion(idNotificacion As Integer, Asunto As String, Archivo As String, Mensaje As String, RowParam As DataRow)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)
                dbmCore.Connection_Open(1)
                Dim Notificacion = dbmCore.SchemaConfig.PA_Get_Notificacion.DBExecute(idNotificacion)
                If Notificacion.Rows.Count > 0 Then
                    Dim CorreoDatatable = dbmCore.SchemaConfig.PA_Busqueda_Parametros_Correo.DBExecute(Notificacion(0).id_Notificacion, Notificacion(0).id_Notificacion_Lista)
                    If CorreoDatatable.Rows.Count > 0 Then
                        Dim FechaDatos As String = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")
                        Dim Message As String = ""
                        Dim MailTo As String = ""
                        Dim MailCC As String = ""
                        Dim MailCCO As String = ""
                        Dim Subject As String = ""
                        Dim nAttachName As String = ""
                        Dim nAttach As Byte() = Nothing
                        Dim CuerpoCorreo As String = ""
                        Dim Novedades As String = ""
                        Dim Asuntos As String = ""

                        If Trim(Archivo) <> "" Then
                            nAttachName = Path.GetFileName(Archivo)
                            nAttach = File.ReadAllBytes(Archivo)
                        End If

                        CuerpoCorreo = CorreoDatatable(0).CUERPO
                        CuerpoCorreo = CuerpoCorreo.Replace("@FechaEnvio", FechaDatos)

                        Message = CorreoDatatable(0).SALUDO & CuerpoCorreo & CorreoDatatable(0).FIRMA
                        MailTo = CorreoDatatable(0).CORREOS

                        Asuntos = CorreoDatatable(0).ASUNTO
                        Asuntos = Asuntos.Replace("@Asunto", Asunto)
                        Subject = Asuntos

                        Dim DBMTools As DBTools.DBToolsDataBaseManager = Nothing

                        Try
                            DBMTools = New DBTools.DBToolsDataBaseManager(Program.ConnectionStrings.Tools)
                            DBMTools.Connection_Open()
                            DBMTools.InsertMail(RowParam.Item("fk_Entidad"), 2, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)
                        Catch ex As Exception
                            WriteErrorLog("Error proceso envío notificación proceso: " & RowParam.Item("descripcion") & ", notificación: " & Notificacion(0).Nombre_Notificacion & ", ex: " & ex.Message.ToString())
                        Finally
                            DBMTools.Connection_Close()
                        End Try
                    End If
                Else
                    WriteErrorLog("No se encuentra configuración para envio de correo del proceso: " & RowParam.Item("descripcion") & ", fk_Notificaion: " & idNotificacion)
                End If
            Catch ex As Exception
                WriteErrorLog("Error EnvioReporte ex: " & ex.ToString())
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub EnviarNotificacionError(idNotificacion As Integer, Asunto As String, Archivo As String, Mensaje As String, RowParam As DataRow)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)
                dbmCore.Connection_Open(1)
                Dim Notificacion = dbmCore.SchemaConfig.PA_Get_Notificacion.DBExecute(idNotificacion)
                If Notificacion.Rows.Count > 0 Then
                    Dim CorreoDatatable = dbmCore.SchemaConfig.PA_Busqueda_Parametros_Correo.DBExecute(Notificacion(0).id_Notificacion, Notificacion(0).id_Notificacion_Lista)
                    If CorreoDatatable.Rows.Count > 0 Then
                        Dim FechaDatos As String = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")
                        Dim Message As String = ""
                        Dim MailTo As String = ""
                        Dim MailCC As String = ""
                        Dim MailCCO As String = ""
                        Dim Subject As String = ""
                        Dim nAttachName As String = ""
                        Dim nAttach As Byte() = Nothing
                        Dim CuerpoCorreo As String = ""
                        Dim Novedades As String = ""
                        Dim Asuntos As String = ""

                        If Trim(Archivo) <> "" Then
                            nAttachName = Path.GetFileName(Archivo)
                            nAttach = File.ReadAllBytes(Archivo)
                        End If

                        CuerpoCorreo = CorreoDatatable(0).CUERPO
                        CuerpoCorreo = CuerpoCorreo.Replace("@FechaEnvio", FechaDatos)
                        CuerpoCorreo = CuerpoCorreo.Replace("@Mensaje", Mensaje)

                        Message = CorreoDatatable(0).SALUDO & CuerpoCorreo & CorreoDatatable(0).FIRMA
                        MailTo = CorreoDatatable(0).CORREOS

                        Asuntos = CorreoDatatable(0).ASUNTO
                        Asuntos = Asuntos.Replace("@Asunto", Asunto)
                        Subject = Asuntos

                        Dim DBMTools As DBTools.DBToolsDataBaseManager = Nothing
                        Try
                            DBMTools = New DBTools.DBToolsDataBaseManager(Program.ConnectionStrings.Tools)
                            DBMTools.Connection_Open()

                            DBMTools.InsertMail(RowParam.Item("fk_Entidad"), 2, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)
                        Catch ex As Exception
                            WriteErrorLog("Error proceso envío notificación proceso: " & RowParam.Item("descripcion") & ", notificación: " & Notificacion(0).Nombre_Notificacion & ", ex: " & ex.Message.ToString())
                        Finally
                            DBMTools.Connection_Close()
                        End Try
                    End If
                Else
                    WriteErrorLog("No se encuentra configuración para envio de correo del proceso: " & RowParam.Item("descripcion") & ", fk_Notificaion: " & idNotificacion)
                End If
            Catch ex As Exception
                WriteErrorLog("Error EnvioReporte ex: " & ex.ToString())
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
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
            If Program.Config.LogActivo = False Then
                Exit Sub
            End If

            If Trim(Program.Config.RutaLog) = "" Then
                RutaLog = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) & "\LogsCorreo"
            Else
                RutaLog = Program.Config.RutaLog
            End If
            If Directory.Exists(RutaLog) = False Then
                Directory.CreateDirectory(RutaLog)
            End If
            Try
                Dim sw As New StreamWriter(RutaLog & "\log_" & Now.ToString("yyyyMMdd") & ".txt", True)

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
