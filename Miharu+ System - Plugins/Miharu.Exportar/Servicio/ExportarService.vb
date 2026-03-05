Imports DBSecurity
Imports System.IO
Imports Miharu.Security.Library.WebService
Imports System.Threading
Imports System.Xml.Linq
Imports System.Linq
Imports Miharu.FileProvider.Library
Imports Slyg.Tools.Imaging
Imports Miharu.Desktop.Library.Config
Imports Miharu.Security.Library
Imports Miharu.Imaging.Library.Eventos
Imports Slyg.Tools
Imports Miharu.Imaging.Library
Imports Miharu.Desktop.Library.Plugins
Imports Ionic.Zip
Imports System.Net
Imports OfficeOpenXml

Namespace Servicio
    Public Class ExportarService

#Region "Declaraciones"

        Private Detener As Boolean
        Private Const fk_Entidad_Procesamiento As Short = 11 'P&C
        Private Const fk_Entidad_Calendario As Short = 33
        Private Const fk_Sede_Procesamiento As Short = 3 'P&C - Bogota
        Private Const id_Centro_Procesamiento As Short = 1 'P&C - Bogota - Scanner Bogota
        Private Fecha_Inicial As String = Nothing
        Private Fecha_Final As String = Nothing
        Private id_Calendario As Short = 0
        Private Folders As String = Nothing
        Private Files As String = Nothing
        Private Folios As String = Nothing
        Private Tamaño As String = Nothing
        Dim ltDatatables As List(Of DataTable)
        Dim ItemsDinamicos As List(Of String)
        Dim retorno = True
        Dim RutasPath As String
        Dim contadorHojas As Int16 = 0
        Dim ltSheetsHojas = New List(Of String)
        Dim FileNamesCons = New List(Of String)
        Dim formatoAux As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Private servidor As DBImaging.SchemaCore.CTA_ServidorSimpleType
        Private centro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType
        Private Compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression
        Dim _ProyectoImagingrow As DBImaging.SchemaConfig.CTA_ProyectoRow
        Dim FileFolderName As String
        Dim OutputFolder As String
        Enum TipoComprimido
            zip
            rar
        End Enum
#End Region

        Protected Overrides Sub OnStart(ByVal args() As String)
            IniciarServicio()
        End Sub

        Protected Overrides Sub OnStop()
            DetenerServicio()
        End Sub

        Public Sub IniciarServicio()
            JWriteLog("Funcion Iniciar Servicio Version 1.1", EventLogEntryType.Information)
            Try
                Dim WebService As SecurityWebService
                LoadConfig()
                WebService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")
                JWriteLog(Program.Config.SecurityWebServiceURL, EventLogEntryType.Information)
                RutasPath = Program.Config.RutaPath
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
                WebService.setUser(Program.Config.User, DescargaMasivaImagenesConfig.Decrypt(Program.Config.Password))
                Program.ConnectionStrings = DescargaMasivaImagenesConfig.getCadenasConexion(WebService)

                If Program.ConnectionStrings.DescargaMasiva = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos DB_User_Explorer")
                    Me.Stop()
                    Return
                End If

                If Program.ConnectionStrings.Integration = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos DB_Integration")
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
                JWriteLog("Error IniciarServicio ex: " & ex.Message & " " & ex.StackTrace, EventLogEntryType.Error)
                Me.Stop()
            End Try
        End Sub

        Public Sub DetenerServicio()
            Detener = True
        End Sub

        Public Sub JWriteLog(ByVal mensaje As String, ByVal tipo As EventLogEntryType)
            If Not EventLog.SourceExists("DescargaMasivaImagenesService") Then
                EventLog.CreateEventSource("DescargaMasivaImagenesService", "Application")
            End If
            Dim eventLog1 As EventLog = New EventLog()
            eventLog1.Source = "DescargaMasivaImagenesService"
            eventLog1.WriteEntry(mensaje, tipo)
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
                Try : JWriteLog(ex.Message, EventLogEntryType.Error) : Catch : End Try
            End Try
            Windows.Forms.Application.DoEvents()
        End Sub

        Private Sub LoadConfig()
            ' Leer la configuración
            If (File.Exists(Program.AppDataPath + DescargaMasivaImagenesConfig.ConfigFileName)) Then
                Program.Config = DescargaMasivaImagenesConfig.Deserialize(Program.AppDataPath)
            End If
        End Sub

        Private Sub Proceso()

            Dim dbmUserExplorer As DB_Users_Explorer.DB_Users_ExplorerDataBaseManager = Nothing
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)
            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
            dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Program.ConnectionStrings.Integration)
            Try

                dbmSecurity.Connection_Open(1)
                dbmImaging.Connection_Open(1)
                dbmIntegration.Connection_Open(1)

                While Not Detener
                    If Detener Then
                        Return
                    End If
                    Dim CalendarioDataTable = dbmSecurity.SchemaConfig.TBL_Calendario.DBFindByfk_EntidadNombre_Calendario(fk_Entidad_Calendario, "DescargaMasiva")
                    If CalendarioDataTable.Count > 0 Then
                        id_Calendario = CalendarioDataTable(0).id_Calendario
                    Else
                        WriteErrorLog("No hay calendario programado para el servicio")
                    End If
                    Dim habil = dbmSecurity.SchemaConfig.PA_Es_Hora_Habil.DBExecute(fk_Entidad_Procesamiento, id_Calendario)


                    If Detener Then Return
                    Thread.Sleep(Program.Config.Intervalo)

                End While
            Catch ex As Exception
                WriteErrorLog("Error Proceso ex: " & ex.Message)
            Finally
                'If (dbmUserExplorer IsNot Nothing) Then dbmUserExplorer.Connection_Close()
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Function Validar() As Boolean
            If (Not Directory.Exists(RutasPath)) Then
                WriteErrorLog("El directorio no existe, Seleccione un directorio existente")
                Return False
            End If
            Return True
        End Function

        Private Sub exportarimagen(manager As FileProviderManager, ItemFile As Object, Compresion As ImageManager.EnumCompression, RutaDir As String, _ProyectoImagingrow As DBImaging.SchemaConfig.CTA_ProyectoRow)
            Dim expediente = Convert.ToInt64(ItemFile("fk_Expediente"))
            Dim folder = Convert.ToInt16(ItemFile("fk_Folder"))
            Dim file = Convert.ToInt16(ItemFile("fk_File"))
            Dim documento = Convert.ToInt32(ItemFile("fk_Documento"))
            Dim Version = Convert.ToInt16(ItemFile("id_Version"))
            Dim Nombre_Imagen = Convert.ToString(ItemFile("Nombre_Imagen"))
            Dim Folios = manager.GetFolios(expediente, folder, file, Version)


            Dim FileNames As New List(Of String)
            Dim FileName As String = Nothing
            Dim FileNameAux As String = Nothing
            Dim ExtensionAux As String = Nothing

            Try
                If Folios > 0 Then
                    For folio As Short = 1 To Folios
                        Dim Imagen() As Byte = Nothing
                        Dim Thumbnail() As Byte = Nothing
                        manager.GetFolio(expediente, folder, file, 1, folio, Imagen, Thumbnail)
                        FileName = RutaDir & "\" & Guid.NewGuid().ToString() & _ProyectoImagingrow.Extension_Formato_Imagen_Salida
                        FileNames.Add(FileName)
                        FileNamesCons.Add(FileName)
                        Using fs = New FileStream(FileName, FileMode.Create)
                            fs.Write(Imagen, 0, Imagen.Length)
                            fs.Close()
                        End Using
                    Next
                End If
                If Not _ProyectoImagingrow.Exportar_Unico_Archivo_TIFF Then
                    If FileNames.Count > 0 Then
                        Dim Format As ImageManager.EnumFormat
                        If _ProyectoImagingrow.Extension_Formato_Imagen_Salida = ".bmp" Then
                            Format = ImageManager.EnumFormat.Bmp
                        End If
                        If _ProyectoImagingrow.Extension_Formato_Imagen_Salida = ".gif" Then
                            Format = ImageManager.EnumFormat.Gif
                        End If
                        If _ProyectoImagingrow.Extension_Formato_Imagen_Salida = ".jpg" Then
                            Format = ImageManager.EnumFormat.Jpeg
                            Compresion = ImageManager.EnumCompression.Jpeg
                        End If
                        If _ProyectoImagingrow.Extension_Formato_Imagen_Salida = ".pdf" Then
                            Format = ImageManager.EnumFormat.Pdf
                            Compresion = ImageManager.EnumCompression.Jpeg
                        End If
                        If _ProyectoImagingrow.Extension_Formato_Imagen_Salida = ".png" Then
                            Format = ImageManager.EnumFormat.Png
                        End If
                        If _ProyectoImagingrow.Extension_Formato_Imagen_Salida = ".tif" Then
                            Format = ImageManager.EnumFormat.Tiff
                        End If
                        Dim Valido As Boolean = True
                        Dim MsgError As String = ""
                        If _ProyectoImagingrow.Usa_Renombramiento_Imagen_Exportacion Then
                            FileNameAux = Nombre_Imagen
                        End If
                        If Format = ImageManager.EnumFormat.Pdf Then
                            Format = ".pdf"
                        Else
                            ExtensionAux = _ProyectoImagingrow.Extension_Formato_Imagen_Salida.ToString()
                        End If
                        If Valido = True And FileNameAux = Nothing Then
                            FileName = RutaDir + ItemFile("File_Unique_Identifier").ToString() + "_0001" + ExtensionAux
                        End If
                        If Valido = True And Not FileNameAux = Nothing Then
                            If Not ExtensionAux = Nothing Then
                                ExtensionAux = _ProyectoImagingrow.Extension_Formato_Imagen_Salida
                                FileName = RutaDir + FileNameAux + ExtensionAux
                            End If
                        Else
                            ExtensionAux = ExtensionAux
                            FileName = RutaDir + FileNameAux + ExtensionAux
                        End If
                        If Valido = False Then
                            Throw New Exception(MsgError)
                        End If
                        ImageManager.Save(FileNames, FileName, "", formatoAux, False, False, RutaDir, True)
                    End If
                End If

            Catch ex As Exception
                WriteErrorLog("Error exportarimagen ex: " & ex.Message)
            End Try
        End Sub



        Private Function CompressFile(startPath As String, zipPath As String, deleteFile As Boolean) As Boolean

            If IO.File.Exists(zipPath) Then IO.File.Delete(zipPath)
            'Creacion de archivo zip a partir de path de entrada y path de salida
            Using zip As ZipFile = New ZipFile()
                zip.AddDirectory(startPath)
                zip.Save(zipPath)
                Return True
            End Using
        End Function
        Public Function getIp() As String
            Dim valorIp As String
            valorIp = Dns.GetHostEntry(My.Computer.Name).AddressList.FirstOrDefault(Function(i) i.AddressFamily = Sockets.AddressFamily.InterNetwork).ToString()
            Return valorIp
        End Function

        Private Function escribeArchivoExcel(RutaCompleta As String, NombreReporte As String, Extension As String, ByRef ds As DataTable) As Boolean
            Dim respuesta As Boolean = False
            Try
                Dim rute = RutaCompleta.Replace("\\", "\") + NombreReporte + Extension
                Dim newFile As New FileInfo(rute)
                Dim pck As ExcelPackage = New ExcelPackage(newFile)
                Dim ws = pck.Workbook.Worksheets.Add("Hoja1")
                ws.Cells("A1").LoadFromDataTable(ds, True)
                pck.Save()
                pck.Dispose()
                pck = Nothing
                respuesta = True
                Return respuesta
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class
End Namespace
