Imports Citibank.Plugin.Imaging.Embargos
Imports System.Windows.Forms
Imports System.IO
Imports Miharu.FileProvider.Library
Imports Miharu.Desktop.Library.Config
Imports DBCore
Imports BarcodeLib.BarcodeReader
Imports Microsoft.VisualBasic.FileIO
Imports Slyg.Tools.Imaging
Imports System.Drawing
Imports System.Text
Imports Slyg.Tools
Imports Miharu.Imaging.Library
Imports Slyg.Tools.Progress

Namespace Embargos.Forms
    Public Class FormCargueCartas
        Private _plugin As EmbargosImagingPlugin
        Private _centro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType
        Public Const MaxThumbnailWidth As Integer = 60
        Public Const MaxThumbnailHeight As Integer = 80

        Public Sub New(nPlugin As EmbargosImagingPlugin)
            _plugin = nPlugin

            InitializeComponent()
        End Sub

        Private Sub GetFolderButton_Click(sender As System.Object, e As System.EventArgs) Handles GetFolderButton.Click
            RutaTextBox.Text = Select_Folder(RutaTextBox.Text)
        End Sub

        Private Function Select_Folder(nPath As String) As String
            Dim selector = New FolderBrowserDialog

            selector.SelectedPath = nPath

            If (selector.ShowDialog() = DialogResult.OK) Then
                Return selector.SelectedPath
            Else
                Return nPath
            End If
        End Function

        ''' <summary>
        ''' Escribe un mensaje en un archivo de registro en el directorio especificado.
        ''' Crea el directorio si no existe. El archivo incluye la fecha actual en su nombre.
        ''' </summary>
        ''' <param name="message">El mensaje a registrar.</param>
        ''' <param name="logDirectory">El directorio donde se almacenará el archivo de registro.</param>
        Public Sub WriteLog(message As String, logDirectory As String)

            If Not Directory.Exists(logDirectory) Then
                Directory.CreateDirectory(logDirectory)
            End If

            Dim logFileName As String = "Logs.txt"
            Dim logFilePath As String = Path.Combine(logDirectory, logFileName)

            ' Usamos StreamWriter para liberar el archivo inmediatamente después de escribir
            Using writer As New StreamWriter(logFilePath, True)
                writer.WriteLine("--------------------------------------------------------------")
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                writer.WriteLine("Mensaje: " & message)
                writer.WriteLine("--------------------------------------------------------------")
                writer.WriteLine("")
            End Using
        End Sub

        Private Sub ProcessButton_Click(sender As System.Object, e As System.EventArgs) Handles ProcessButton.Click

            Dim dbmTools As DBTools.DBToolsDataBaseManager = Nothing
            Try
                dbmTools = New DBTools.DBToolsDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Tools)
                dbmTools.Connection_Open()

                If Validar() Then

                    Dim MessageLog As New StringBuilder()

                    Dim tblEstadosAcuse = dbmTools.SchemaConfig.TBL_Estado_Acuse.DBFindByIsActive(True)
                    If tblEstadosAcuse.Count = 0 OrElse tblEstadosAcuse Is Nothing Then
                        MessageLog.AppendLine("¡¡ADVERTENCIA!! No se han podido Obtener los registros de Estado Acuse")
                        Return
                    End If

                    Dim recordsTracking = New List(Of RecordTracking)
                    Dim fileNames = Directory.GetFiles(RutaTextBox.Text)
                    Dim ProgressForm As New FormProgress()

                    Dim EtapasDictionary As New Dictionary(Of Integer, String)
                    EtapasDictionary.Add(1, "Validación Imagenes")
                    EtapasDictionary.Add(2, "Cargue de Imagenes")

                    'Iniciar proceso                
                    ProgressForm.Process = ""
                    ProgressForm.Action = ""
                    ProgressForm.ValueProcess = 0
                    ProgressForm.ValueAction = 0
                    ProgressForm.MaxValueAction = EtapasDictionary.Count

                    ProgressForm.Show()

                    Dim ListOfNamesImages = New List(Of String)
                    Dim listOfFilesNames = New List(Of String)

                    ProgressForm.Action = EtapasDictionary(1)
                    Application.DoEvents()

                    Dim Count_Process_ValidationImage As Integer = 0
                    ProgressForm.ValueProcess = Count_Process_ValidationImage
                    ProgressForm.MaxValueProcess = fileNames.Count

                    For Each Files In fileNames

                        Dim recordTracking = New RecordTracking(Path.GetFileName(Files))

                        ProgressForm.Process = recordTracking.FileName
                        Application.DoEvents()

                        Dim extension = Path.GetExtension(Files).ToUpper()
                        If (extension <> ".JPG" And extension <> ".JPEG" And extension <> ".TIF" And extension <> ".TIFF" And extension <> ".PNG") Then
                            MessageLog.AppendLine("¡¡ADVERTENCIA!! El archivo:" + recordTracking.FileName + " No contiene un Formato Valido")
                            Count_Process_ValidationImage += 1
                            ProgressForm.ValueProcess = Count_Process_ValidationImage
                            Continue For
                        End If

                        If String.IsNullOrWhiteSpace(recordTracking.FileName) Then
                            MessageLog.AppendLine("¡¡ADVERTENCIA!! El nombre del archivo " + recordTracking.FileName + " no puede estar vacío.")
                            Count_Process_ValidationImage += 1
                            ProgressForm.ValueProcess = Count_Process_ValidationImage
                            Continue For
                        End If

                        ' Extraer el nombre del archivo sin la extensión
                        Dim fileNameWithoutExtension As String = Path.GetFileNameWithoutExtension(recordTracking.FileName)
                        If String.IsNullOrWhiteSpace(fileNameWithoutExtension) Then
                            MessageLog.AppendLine("¡¡ADVERTENCIA!! El nombre del archivo " + recordTracking.FileName + " no tiene contenido válido.")
                            Count_Process_ValidationImage += 1
                            ProgressForm.ValueProcess = Count_Process_ValidationImage
                            Continue For
                        End If

                        ' Obtener la primera letra y los caracteres restantes
                        Dim firstLetter As String = fileNameWithoutExtension.Substring(0, 1).ToUpperInvariant()
                        Dim remainingCharacters As String = fileNameWithoutExtension.Substring(1)

                        ' Validar la primera letra
                        For Each rowEstadoAcuse As DBTools.SchemaConfig.TBL_Estado_AcuseRow In tblEstadosAcuse
                            If firstLetter = rowEstadoAcuse.Nombre_Estado_Acuse Then
                                recordTracking.StatusTracking = rowEstadoAcuse.Descripcion_Estado_Acuse
                            End If
                        Next

                        If recordTracking.StatusTracking Is Nothing OrElse recordTracking.StatusTracking = "" Then
                            MessageLog.AppendLine("¡¡ADVERTENCIA!! La primera letra del nombre de la imagen : " + recordTracking.FileName + " debe ser un valor valido configurado, coloque un nombre válido.")
                            Count_Process_ValidationImage += 1
                            ProgressForm.ValueProcess = Count_Process_ValidationImage
                            Continue For
                        End If

                        ListOfNamesImages.Add(fileNameWithoutExtension)
                        listOfFilesNames.Add(Files)
                        recordsTracking.Add(recordTracking)

                        If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                        Count_Process_ValidationImage += 1
                        ProgressForm.ValueProcess = Count_Process_ValidationImage
                    Next

                    If ListOfNamesImages.Count > 0 Then

                        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                        Dim manager As FileProviderManager = Nothing

                        Try
                            dbmCore = New DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                            dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                            dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                            dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                            Dim namesImages As String = String.Join(",", ListOfNamesImages)

                            ' Obtiene los files que se asociaron con los numeros de guias y actualiza estado en la tbl_tracking_Mail
                            Dim dtFilesAcuse As DataTable = dbmImaging.SchemaProcess.PA_Obtiene_Files_Acuse_Recibido.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, namesImages)
                            If dtFilesAcuse Is Nothing OrElse dtFilesAcuse.Rows.Count = 0 Then
                                MessageLog.AppendLine("¡¡ADVERTENCIA!! Ninguna de las imagenes cargadas se asocian con un numero de Guia a los registros con estado 'Entregados a Transportadora'")
                                Return
                            End If

                            Dim compresion As DesktopConfig.FormatoImagenEnum = Utilities.GetEnumCompression(CType(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                            _centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
                            Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(_plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad, _plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType

                            manager = New FileProviderManager(servidor, _centro, dbmImaging, _plugin.Manager.Sesion.Usuario.id)
                            manager.Connect()

                            ProgressForm.Action = EtapasDictionary(2)
                            Application.DoEvents()

                            Dim Count_Process_ImageLoading As Integer = 0
                            ProgressForm.ValueProcess = Count_Process_ImageLoading
                            ProgressForm.MaxValueProcess = dtFilesAcuse.Rows.Count

                            For Each row As DataRow In dtFilesAcuse.Rows

                                Dim numeroGuia As String = row("Numero_Guia").ToString()
                                Dim _Expediente As Long = CLng(row("fk_Expediente"))
                                Dim _Folder As Integer = CInt(row("fk_Folder"))
                                Dim _File As Integer = CInt(row("fk_File"))
                                Dim identifier As Slyg.Tools.SlygNullable(Of System.Guid) = New Slyg.Tools.SlygNullable(Of System.Guid)(New Guid(row("File_Unique_Identifier").ToString()))

                                Dim matchedName As String = FindMatchingName(listOfFilesNames, numeroGuia)
                                If matchedName = String.Empty Then
                                    MessageLog.AppendLine("¡¡ADVERTENCIA!! El Numero de Guia " + numeroGuia + " no se asocia a ningun nombre de Imagen.")
                                    Count_Process_ImageLoading += 1
                                    ProgressForm.ValueProcess = Count_Process_ImageLoading
                                    Continue For
                                End If

                                Dim fileNameWithoutExtension As String = Path.GetFileNameWithoutExtension(matchedName)
                                Dim formato As String = Utilities.GetEnumFormat(Path.GetExtension(matchedName).ToUpper())

                                ProgressForm.Process = fileNameWithoutExtension
                                Application.DoEvents()

                                'Inserta File en Imaging
                                Dim FileImagingType = New DBCore.SchemaImaging.TBL_FileType
                                FileImagingType.fk_Expediente = _Expediente
                                FileImagingType.fk_Folder = _Folder
                                FileImagingType.fk_File = _File
                                FileImagingType.id_Version = 1
                                FileImagingType.File_Unique_Identifier = identifier
                                FileImagingType.Folios_Documento_File = 1
                                FileImagingType.Tamaño_Imagen_File = 30
                                FileImagingType.Nombre_Imagen_File = fileNameWithoutExtension
                                FileImagingType.Key_Cargue_Item = fileNameWithoutExtension
                                FileImagingType.Save_FileName = fileNameWithoutExtension
                                FileImagingType.fk_Content_Type = ".jpg"
                                FileImagingType.fk_Usuario_Log = _plugin.Manager.Sesion.Usuario.id
                                FileImagingType.Validaciones_Opcionales = False
                                FileImagingType.Es_Anexo = False
                                FileImagingType.fk_Anexo = Nothing
                                FileImagingType.Exportado = False
                                FileImagingType.fk_Entidad_Servidor = _plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad
                                FileImagingType.fk_Servidor = _plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor
                                FileImagingType.Fecha_Creacion = SlygNullable.SysDate
                                FileImagingType.Fecha_Transferencia = SlygNullable.SysDate
                                FileImagingType.En_Transferencia = False

                                dbmCore.SchemaImaging.TBL_File.DBInsert(FileImagingType)

                                'Inserta Fileestado en process
                                Dim FileEstadoType = New DBCore.SchemaProcess.TBL_File_EstadoType
                                FileEstadoType.fk_Expediente = _Expediente
                                FileEstadoType.fk_Folder = _Folder
                                FileEstadoType.fk_File = _File
                                FileEstadoType.Modulo = DesktopConfig.Modulo.Imaging
                                FileEstadoType.fk_Estado = 38
                                FileEstadoType.fk_Usuario = _plugin.Manager.Sesion.Usuario.id
                                FileEstadoType.Fecha_Log = DateTime.Now

                                dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(FileEstadoType, _Expediente, _Folder, _File, FileEstadoType.Modulo)

                                Dim fileNamePath = Path.Combine(RutaTextBox.Text, matchedName)
                                Dim folios As Short = CShort(ImageManager.GetFolios(fileNamePath))

                                For folio As Short = 1 To folios
                                    Dim dataImage As Byte() = Nothing
                                    Dim dataImageThumbnail As Byte() = Nothing

                                    dataImage = ImageManager.GetFolioData(fileNamePath, folio, formato, compresion)
                                    dataImageThumbnail = ImageManager.GetThumbnailData(fileNamePath, folio, folio, MaxThumbnailWidth, MaxThumbnailHeight)(0)

                                    If folio = 1 Then manager.CreateItem(_Expediente, _Folder, _File, 1, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, identifier)
                                    manager.CreateFolio(_Expediente, _Folder, _File, 1, folio, dataImage, dataImageThumbnail, False)
                                    folio += 1
                                Next

                                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                                Count_Process_ImageLoading += 1
                                ProgressForm.ValueProcess = Count_Process_ImageLoading
                            Next

                        Catch ex As Exception
                            'Manejar el error, eliminar si es necesario los registros insertados
                        Finally
                            ProgressForm.Visible = False
                            ProgressForm.Hide()

                            If dbmImaging IsNot Nothing Then dbmImaging.Connection_Close()
                            If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                            If manager IsNot Nothing Then manager.Disconnect()

                            If MessageLog.Length > 0 Then
                                WriteLog(MessageLog.ToString(), RutaTextBox.Text)
                            End If
                        End Try
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Error : " & ex.Message, "GenerarAcuseRecibido", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If dbmTools IsNot Nothing Then dbmTools.Connection_Close()
            End Try
            MessageBox.Show("Proceso Finalizado", "Fin del Proceso", MessageBoxButtons.OK, MessageBoxIcon.None)
        End Sub

        Public Function GetFileExtension(ByVal fileName As String) As String
            Return Path.GetExtension(fileName)
        End Function

        Public Function FindMatchingName(ByVal listOfNames As List(Of String), ByVal numeroGuia As String) As String

            For Each name As String In listOfNames

                If name.Length > 1 Then
                    Dim fileNameWithoutExtension As String = Path.GetFileNameWithoutExtension(name)
                    Dim modifiedName As String = fileNameWithoutExtension.Substring(1)
                    If modifiedName = numeroGuia Then
                        Return Path.GetFileName(name)   ' Devuelve el valor coincidente
                    End If
                End If
            Next
            ' Si no se encuentra coincidencia, devuelve una cadena vacía
            Return String.Empty
        End Function


        Private Function Validar() As Boolean
            Dim result As Boolean = True
            If String.IsNullOrWhiteSpace(RutaTextBox.Text) Then
                MessageBox.Show("Debe seleccionar una ruta de cargue", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                result = False
            ElseIf (Not Directory.Exists(RutaTextBox.Text)) Then
                MessageBox.Show("La ruta de cargue no es válida", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                RutaTextBox.Focus()
                RutaTextBox.SelectAll()
                result = False
            End If
            Return result
        End Function

        Private Function CapturaManual(Cbarras As String, ruta As String) As String
            Dim CapturaCbarras = New Forms.FormCapturaCbarras(_plugin)
            CapturaCbarras.CbarrasLeidoTextBox.Text = Cbarras

            CapturaCbarras.picImage.Image = New Bitmap(ruta)
            If CapturaCbarras.ShowDialog() = DialogResult.OK Then
                Return CapturaCbarras.CbarrasCapturaTextBox.Text
            End If

            Return String.Empty
        End Function
    End Class

    Public Class Record
        Public Property FileName() As String
        Public Property CBarras_Leido() As String
        Public Property CBarras_Capturado() As String
        Public Property Message() As String

        Public Sub New(nFileName As String)
            Me.FileName = nFileName
        End Sub

        Function getLine() As String
            Return """" & Me.FileName & """" & vbTab & """" & CStr(IIf(Me.CBarras_Capturado <> "", Me.CBarras_Capturado, Me.CBarras_Leido)) & """"
        End Function
    End Class

    Public Class RecordTracking
        Public Property FileName As String
        Public Property StatusTracking As String
        Public Property TrackingNumber As String
        Public Property Message() As String

        Public Sub New(nFileName As String)
            Me.FileName = nFileName
        End Sub
    End Class


End Namespace
