Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing
Imports BarcodeLib
Imports BarcodeLib.BarcodeReader
Imports DBCore
Imports DBImaging
Imports Miharu.FileProvider.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Imaging.Indexer
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Imaging.Indexer.Controller.Indexer.Capturas
Imports Slyg.Tools.Imaging
Imports Microsoft.VisualBasic.FileIO
Imports Slyg.Tools

Namespace Embargos.Forms.FormGenerarCartas.FormCargueCartas
    Public Class FormCargueCartas

        Private _plugin As EmbargosImagingPlugin
        Private _centro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType
        Public Const MaxThumbnailWidth As Integer = 60
        Public Const MaxThumbnailHeight As Integer = 80

        Public Sub New(nPlugin As EmbargosImagingPlugin)
            _plugin = nPlugin
            InitializeComponent()
        End Sub

        Private Sub FormCargueCartas_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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

        Private Sub ProcessButton_Click(sender As System.Object, e As System.EventArgs) Handles ProcessButton.Click
            Try

                If Validar() Then
                    Dim records = New List(Of Record)
                    Dim fileNames = Directory.GetFiles(RutaTextBox.Text)
                    Dim Correcto As Boolean = False

                    ProgressBar.Minimum = 0
                    ProgressBar.Value = 0
                    ProgressBar.Maximum = fileNames.Length

                    For Each File In fileNames
                        Dim record = New Record(Path.GetFileName(File))

                        Dim extension = Path.GetExtension(File).ToUpper()
                        If (extension <> ".JPG" And extension <> ".JPEG" And extension <> ".TIF") Then
                            Application.DoEvents()
                            ProgressBar.Value += 1
                            Continue For
                        End If

                        Dim valido As Boolean = False
                        Dim CBarrasFile = BarcodeReader.read(File, BarcodeReader.CODE128)
                        Dim fileCore As DBCore.SchemaProcess.TBL_FileDataTable = Nothing
                        Dim dbmCore As DBCoreDataBaseManager = Nothing
                        dbmCore = New DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                        If CBarrasFile.Length > 0 Then
                            Try
                                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                                fileCore = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(CBarrasFile(0))
                                record.CBarras_Capturado = CBarrasFile(0)
                                If fileCore.Rows.Count = 0 Then
                                    Dim CbarrasCaptura = CapturaManual(CBarrasFile(0), File)
                                    If Not String.IsNullOrWhiteSpace(CbarrasCaptura) Then
                                        fileCore = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(CbarrasCaptura)
                                        record.CBarras_Capturado = CbarrasCaptura
                                        Correcto = True
                                    End If
                                Else
                                    Correcto = True
                                End If

                            Catch ex As Exception
                                MessageBox.Show("Error : " & ex.Message, "GenerarCartas", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Finally
                                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                            End Try

                        Else
                            Dim CbarrasCaptura = CapturaManual(String.Empty, File)
                            If Not String.IsNullOrWhiteSpace(CbarrasCaptura) Then
                                Try
                                    dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                                    fileCore = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(CbarrasCaptura)
                                    record.CBarras_Capturado = CbarrasCaptura
                                    Correcto = True
                                Catch ex As Exception
                                    MessageBox.Show("Error : " & ex.Message, "GenerarCartas", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Finally
                                    If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                                End Try
                            End If
                        End If

                        If Correcto Then
                            Dim manager As FileProviderManager = Nothing
                            Dim formato = Utilities.GetEnumFormat(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Entrada)
                            Dim compresion = Utilities.GetEnumCompression(CType(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))
                            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                            Dim dbmStorage As DBStorage.DBStorageDataBaseManager = Nothing

                            Try
                                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                                dbmCore.Transaction_Begin()
                                'Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Servidor)(0).ToCTA_ServidorSimpleType
                                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(_plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad, _plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType

                                _centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                                manager = New FileProviderManager(servidor, _centro, dbmImaging, _plugin.Manager.Sesion.Usuario.id)
                                manager.Connect()

                                'Inserta File en Imaging
                                Dim FileImagingType = New DBCore.SchemaImaging.TBL_FileType
                                FileImagingType.fk_Expediente = fileCore(0).fk_Expediente
                                FileImagingType.fk_Folder = fileCore(0).fk_Folder
                                FileImagingType.fk_File = fileCore(0).id_File
                                FileImagingType.id_Version = 1
                                FileImagingType.File_Unique_Identifier = fileCore(0).File_Unique_Identifier
                                FileImagingType.Folios_Documento_File = 1
                                FileImagingType.Tamaño_Imagen_File = 30
                                FileImagingType.Nombre_Imagen_File = record.FileName
                                FileImagingType.Key_Cargue_Item = record.FileName
                                FileImagingType.Save_FileName = record.FileName
                                FileImagingType.fk_Content_Type = ".jpg"
                                FileImagingType.fk_Usuario_Log = _plugin.Manager.Sesion.Usuario.id
                                FileImagingType.Validaciones_Opcionales = False
                                FileImagingType.Es_Anexo = False
                                FileImagingType.Exportado = False
                                FileImagingType.fk_Entidad_Servidor = _plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad
                                FileImagingType.fk_Servidor = _plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor
                                FileImagingType.Fecha_Creacion = SlygNullable.SysDate

                                dbmCore.SchemaImaging.TBL_File.DBInsert(FileImagingType)

                                Dim leerFolio As Boolean = True
                                Dim folios As Integer = 1
                                While leerFolio
                                    Try
                                        Dim FolioBitmap = ImageManager.GetFolioBitmap(RutaTextBox.Text & "\\" & record.FileName, folios)
                                        Dim dataImage = ImageManager.GetFolioData(FolioBitmap, 1, 1, formato, compresion)
                                        Dim dataImageThumbnail = ImageManager.GetThumbnailData(RutaTextBox.Text & "\\" & record.FileName, 1, folios, MaxThumbnailWidth, MaxThumbnailHeight)

                                        If folios = 1 Then manager.CreateItem(fileCore(0).fk_Expediente, fileCore(0).fk_Folder, fileCore(0).id_File, 1, ".jpg", fileCore(0).File_Unique_Identifier)
                                        manager.CreateFolio(fileCore(0).fk_Expediente, fileCore(0).fk_Folder, fileCore(0).id_File, 1, folios, dataImage, dataImageThumbnail(folios - 1), False)
                                        folios += 1
                                    Catch ex As Exception
                                        leerFolio = False
                                    End Try

                                End While
                                My.Computer.FileSystem.DeleteFile(File, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin)
                                Utilities.ActualizaEstadoFileImaging(dbmImaging, dbmCore, record.CBarras_Capturado, 2, 44, _plugin.Manager.Sesion.Usuario.id)
                                dbmCore.Transaction_Commit()
                                ProgressBar.Value += 1
                            Catch ex As Exception
                                MessageBox.Show("Error : " & ex.Message, "GenerarCartas", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                dbmCore.Transaction_Rollback()
                            Finally
                                If dbmImaging IsNot Nothing Then dbmImaging.Connection_Close()
                                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                                If manager IsNot Nothing Then manager.Disconnect()
                            End Try
                        End If

                    Next
                End If
            Catch ex As Exception
                MessageBox.Show("Error : " & ex.Message, "GenerarCartas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            MessageBox.Show("Proceso Finalizado", "Fin del Proceso", MessageBoxButtons.OK, MessageBoxIcon.None)
        End Sub

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
            Dim CapturaCbarras = New FormCapturaCbarras.FormCapturaCbarras(_plugin)
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
End Namespace