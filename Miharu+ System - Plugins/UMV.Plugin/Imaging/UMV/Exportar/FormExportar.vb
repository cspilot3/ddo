Imports System.Windows.Forms
Imports System.IO
Imports Miharu.Tools.Progress
Imports Slyg.Tools.Imaging
Imports Miharu.Desktop.Library.Config
Imports System.Threading
Imports Miharu.FileProvider.Library
Imports GdPicture12
Imports Ionic.Zip
Imports System.Drawing
Imports iTextSharp.text.pdf
Imports Slyg.Tools
Imports System.Drawing.Imaging



Namespace Imaging.UMV.Exportar

    Public Class ProcesadorHilosExportar

        Dim NumHilos As Integer = 4
        Dim ListaHilos As New List(Of Thread)
        Public formulario As FormExportar
        Public Shared objLock = New Object()


        Public Sub AgregarHilo(ByVal ArrayParameters As ArrayList)



            If TieneHiloslibres() = False Then

                Do

                    Thread.Sleep(1000)

                    If TieneHiloslibres() Then

                        Exit Do

                    End If

                Loop

            End If


            Dim Threads As New System.Threading.Thread(AddressOf formulario.ProcesoHilosLlaves)

            Threads.Start(ArrayParameters)

            SyncLock objLock
                ListaHilos.Add(Threads)
            End SyncLock

        End Sub

        Public Function TieneHiloslibres() As Boolean
            SyncLock objLock
                Dim ListaHilosBorrar As New List(Of Thread)
                For Each hilo In ListaHilos
                    If hilo.ThreadState = ThreadState.Stopped Then
                        ListaHilosBorrar.Add(hilo)
                    End If
                Next
                For Each hilo In ListaHilosBorrar
                    ListaHilos.Remove(hilo)
                Next
            End SyncLock
            If ListaHilos.Count < NumHilos Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function TerminoHilos() As Boolean

            SyncLock objLock

                For Each hilo In ListaHilos

                    If hilo.ThreadState <> ThreadState.Stopped Then

                        Return False

                    End If

                Next

            End SyncLock

            Return True



        End Function



    End Class



    Public Class FormExportar

#Region " Declaraciones "
        Public _plugin As UMVPlugin
        Private ArrayNotificacion As ArrayList
        Private BloqueoConcurrencia As New Object
        Private RutaSalida As String
        Private NombreCarpetaSalida As String
        Public Shared FileNamesCons As New List(Of String)
        Private formatoAux As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Private CompresionAux As Slyg.Tools.Imaging.ImageManager.EnumCompression
        Private Usa_Exportacion_PDF As Boolean
        Private formato As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Dim compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression
        Dim FileDataTable As New Data.DataTable
        Dim RutaFile
        Dim OTs As List(Of Object) = Nothing
        Dim _StrArchivoLog As String
#End Region

#Region " Contructores "

        Public Sub New(ByVal nPlugin As UMVPlugin)
            InitializeComponent()

            _plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormExportar_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.UMVConnectionString)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Usa_Exportacion_PDF = _plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF
                formato = Utilities.GetEnumFormat(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)

                If (_plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                    compresion = ImageManager.EnumCompression.Ccitt4
                Else
                    compresion = ImageManager.EnumCompression.Lzw
                End If

                Load_FormatoCargue()

                Me.EstibasDataGridView.AutoGenerateColumns = False
                Me.EstibasDataGridView.DataSource = dbmIntegration.SchemaUMV.TBL_Estiba.DBGet(Nothing)
                Me.EstibasDataGridView.Refresh()
                If (Me.EstibasDataGridView.RowCount = 0) Then
                    MessageBox.Show("No se encontraron Estibas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As Exception
                MessageBox.Show("Error : " & ex.Message, "CargarEstibas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If dbmIntegration IsNot Nothing Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Sub BuscarCarpetaButton_Click(sender As System.Object, e As System.EventArgs) Handles BuscarCarpetaButton.Click
            Dim Selector As New FolderBrowserDialog()

            Selector.SelectedPath = CarpetaSalidaTextBox.Text
            If (Selector.ShowDialog() = DialogResult.OK) Then
                Me.CarpetaSalidaTextBox.Text = Selector.SelectedPath
            End If
        End Sub

        Private Sub ExportarButton_Click(sender As System.Object, e As System.EventArgs) Handles ExportarButton.Click
            If (Validar()) Then
                ExportarEstiba()
            End If
        End Sub

        Private Sub CancelarButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

#End Region

#Region " Métodos "

        Private Sub ExportarEstiba()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing
            Dim EstibaRow = CType(CType(Me.EstibasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, DBIntegration.SchemaUMV.TBL_EstibaRow)
            Dim ProgressForm As New FormProgress
            'Dim SqlConnection1 As System.Data.SqlClient.SqlConnection = Nothing
            Dim Llaves As List(Of Object) = Nothing

            Try
                'Dim sqlcon As String = "Data Source=10.64.118.48\admon;Initial Catalog=DB_Miharu.Imaging_core;Persist Security Info=True;User ID=miharu;Password=miharu123"

                'SqlConnection1 = New System.Data.SqlClient.SqlConnection(sqlcon)
                'SqlConnection1.Open()

                'Dim SqlText As String = My.Resources.Resource1.ConsultaOrfeo
                'SqlText = Replace(SqlText, "@fk_Estiba", CInt(EstibaRow.CodigoEstiba))

                'Dim SqlDataAdapter1 As New System.Data.SqlClient.SqlDataAdapter(SqlText, SqlConnection1)

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Me._plugin.Manager.Sesion.Usuario.id)

                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.UMVConnectionString)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                FileDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Data_File.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, CInt(EstibaRow.CodigoEstiba))
                'SqlDataAdapter1.SelectCommand.CommandTimeout = 86400
                'SqlDataAdapter1.Fill(FileDataTable)

                'If Not SqlConnection1 Is Nothing Then SqlConnection1.Close()

                If FileDataTable.Rows.Count > 0 Then
                    Dim Progreso As Integer = 0
                    ProgressForm.SetProceso("Exportar")
                    ProgressForm.SetAccion("Obteniendo imágenes...")
                    ProgressForm.SetProgreso(0)
                    ProgressForm.SetMaxValue(FileDataTable.Rows.Count())
                    Application.DoEvents()

                    NombreCarpetaSalida = FileDataTable.Rows(0)("Sigla_Serie") & "_" & EstibaRow.CodigoEstiba
                    RutaFile = NombreCarpetaSalida & "\Imagenes"

                    RutaSalida = CarpetaSalidaTextBox.Text & "\" & NombreCarpetaSalida

                    'Creaciòn de ruta salida
                    If Not Directory.Exists(RutaSalida) Then
                        Directory.CreateDirectory(RutaSalida)
                    End If

                    'Creaciòn de ruta Imagenes
                    If Not Directory.Exists(RutaSalida & "\Imagenes") Then
                        Directory.CreateDirectory(RutaSalida & "\Imagenes")
                    End If

                    Me._StrArchivoLog = Me.CarpetaSalidaTextBox.Text + "\Log_REPORTE_UMV_" + NombreCarpetaSalida + "_" + Date.Now.ToString("yyyyMMdd") + "_" + Date.Now.Hour.ToString("00") + Date.Now.Minute.ToString("00") + Date.Now.Second.ToString("00") + ".dat"
                    EscribeLog(_StrArchivoLog, "LOG DE REPORTE PARA ESTIBA: " + NombreCarpetaSalida, True, False)

                    OTs = (From a In FileDataTable Group a By groupDt = a.Field(Of Integer)("fk_OT") Into Group Select Group.Select(Function(x) x("fk_OT")).First()).ToList()

                    Dim Servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(Convert.ToInt32(OTs.FirstOrDefault().ToString()))(0).ToCTA_ServidorSimpleType()
                    Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                    Llaves = (From a In FileDataTable Group a By GroupDt = a.Field(Of String)("fk_Llave") Into Group Select Group.Select(Function(x) x("fk_Llave")).First()).ToList()
                    manager = New FileProviderManager(Servidor, centro, dbmImaging, _plugin.Manager.Sesion.Usuario.id)
                    manager.Connect()

                    Dim procesador As New ProcesadorHilosExportar
                    procesador.formulario = Me

                    For Each itemLlave In Llaves
                        Dim ArrayParameters As ArrayList = New ArrayList

                        ArrayParameters.Add(Servidor)
                        ArrayParameters.Add(centro)
                        ArrayParameters.Add(itemLlave)
                        ArrayParameters.Add(compresion)
                        ArrayParameters.Add(RutaFile)
                        ArrayParameters.Add(FileDataTable)
                        ArrayParameters.Add(dbmImaging)
                        ArrayParameters.Add(manager)

                        procesador.AgregarHilo(ArrayParameters)
                    Next

                    While (procesador.TerminoHilos = False)
                        System.Threading.Thread.Sleep(1000)
                    End While
                    ExportarCSV(FileDataTable)
                    ConvertirZIP()
                    Dim ExportacionType = New DBIntegration.SchemaUMV.TBL_EstibaType()
                    ExportacionType.CodigoEstiba = EstibaRow.CodigoEstiba
                    ExportacionType.Fecha_Log_Exportacion = SlygNullable.SysDate
                    ExportacionType.fk_Usuario_Log_Exportacion = _plugin.Manager.Sesion.Usuario.id
                    ExportacionType.ruta = RutaSalida
                    ExportacionType.exportada = True
                    Dim EstibaDataTable = dbmIntegration.SchemaUMV.TBL_Estiba.DBGet(EstibaRow.CodigoEstiba)
                    If EstibaDataTable.Count > 0 Then
                        dbmIntegration.SchemaUMV.TBL_Estiba.DBUpdate(ExportacionType, EstibaDataTable(0).id_Estiba)
                    End If
                    MessageBox.Show("La información se exporto correctamente", "ExportarEstiba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("La estiba que intenta exportar no tiene informacion, por favor valide", "ExportarEstiba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)

                SyncLock BloqueoConcurrencia
                    EscribeLog(_StrArchivoLog, "Error ExportarEstiba: " + ex.Message, False, True)
                End SyncLock

                Return
            Finally
                'If Not SqlConnection1 Is Nothing Then SqlConnection1.Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub



        Private Sub ExportarCSV(nFileDataTable As DataTable)
            Dim EstibaRow = CType(CType(Me.EstibasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, DBIntegration.SchemaUMV.TBL_EstibaRow)
            Dim ReporteExpediente As List(Of Object) = Nothing
            ReporteExpediente = (From a In FileDataTable Group a By GroupDt = a.Field(Of String)("ReportePrecinto") Into Group Select Group.Select(Function(x) x("ReportePrecinto")).First()).ToList()

            Dim NomArchivo = nFileDataTable.Rows(0)("Sigla_Serie") & "_" & EstibaRow.CodigoEstiba & "_exp"
            Dim utf8WithoutBom As New System.Text.UTF8Encoding(False)
            Dim sw = New StreamWriter(File.Open(RutaSalida & "\" & NomArchivo & ".csv", FileMode.Create), utf8WithoutBom)
            For Each Registro In ReporteExpediente
                sw.WriteLine(Registro.ToString())
            Next
            sw.Close()
            sw.Dispose()

            Dim NomArchivoFiles = "FILES_" & nFileDataTable.Rows(0)("Sigla_Serie") & "_" & EstibaRow.CodigoEstiba
            Dim swFiles = New StreamWriter(File.Open(RutaSalida & "\" & NomArchivoFiles & ".csv", FileMode.Create), utf8WithoutBom)
            For Each Registro In FileDataTable.Rows
                swFiles.WriteLine(Registro("ReporteFiles"))
            Next
            swFiles.Close()
            swFiles.Dispose()

        End Sub

        Private Sub ConvertirZIP()
            Try
                If Directory.Exists(RutaSalida) Then
                    Using zip As ZipFile = New ZipFile()
                        zip.UseZip64WhenSaving = Zip64Option.Always
                        zip.CompressionMethod = CompressionMethod.BZip2
                        EscribeLog(_StrArchivoLog, "Inicio de compresion de la carpeta: " + RutaSalida, False, True)

                        zip.AddDirectory(RutaSalida)
                        zip.Save(RutaSalida & ".zip")

                        EscribeLog(_StrArchivoLog, "Fin de compresion de la carpeta: " + RutaSalida, False, True)

                    End Using
                Else
                    EscribeLog(_StrArchivoLog, "El directorio: " + RutaSalida + " no existe.", False, True)
                End If
            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Error comprimiendo carpeta: " + RutaSalida + " " + ex.Message, False, True)
            End Try

        End Sub

        Public Sub ProcesoHilosLlaves(ByVal objectArray As Object)
            Dim ArraListParameters As ArrayList = objectArray


            Dim nservidor As DBImaging.SchemaCore.CTA_ServidorSimpleType = ArraListParameters(0)
            Dim ncentro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType = ArraListParameters(1)
            Dim Llave As Object = ArraListParameters(2)
            Dim nCompresion As Slyg.Tools.Imaging.ImageManager.EnumCompression = ArraListParameters(3)
            Dim rutafiles As String = ArraListParameters(4)
            Dim nFileDataTable As DataTable = ArraListParameters(5)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = ArraListParameters(6)
            Dim manager As FileProviderManager = ArraListParameters(7)

            Try

                Dim FilesRuta = rutafiles & "\" & Llave & "\"
                Dim dtLlave As DataTable = Nothing
                dtLlave = nFileDataTable.Select("fk_Llave = '" + Trim(Llave.ToString()) + "'").CopyToDataTable
                Dim Rutallave As String = RutaSalida & "\Imagenes" & "\" & Llave

                If (Not Directory.Exists(Rutallave)) Then
                    Directory.CreateDirectory(Rutallave)
                End If


                For i = 0 To dtLlave.Rows.Count - 1
                    If (dtLlave.Rows(i)("ImagenPrincipal") = 1) Then
                        'Crear imagen 
                        CrearImagen(dtLlave.Rows(i), nCompresion, Rutallave, i + 1, FilesRuta)
                    Else
                        'ExportarImagen
                        ExportarImagen(manager, dtLlave.Rows(i), nCompresion, Rutallave + "\", i + 1, FilesRuta, nFileDataTable, nservidor, ncentro, dbmImaging, Llave)
                    End If
                Next
            Catch ex As Exception
                SyncLock BloqueoConcurrencia
                    EscribeLog(Me._StrArchivoLog, "Error ProcesoHilosLlaves: " + ex.Message, False, True)
                End SyncLock

            End Try

        End Sub


        Private Sub ExportarImagen(nManager As FileProviderManager, ByVal ItemFile As DataRow, nCompresion As ImageManager.EnumCompression, nFolderName As String, contador As Int32, nFileRuta As String, ndtResulFiltradaOT As DataTable, nservidor As DBImaging.SchemaCore.CTA_ServidorSimpleType, ncentro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType, dbmImaging As DBImaging.DBImagingDataBaseManager, Llave As Object)

            Dim Folios As Short

            Try
                SyncLock BloqueoConcurrencia
                    Folios = nManager.GetFolios(CLng(ItemFile.Item("fk_Expediente").ToString()), CShort(ItemFile.Item("fk_Folder").ToString()), CShort(ItemFile.Item("id_File").ToString()), CShort(ItemFile.Item("id_Version").ToString()))
                End SyncLock

                If Folios > 0 Then
                    Dim contadorbmp As Int32 = 0
                    Dim Imagen() As Byte = Nothing
                    Dim Thumbnail() As Byte = Nothing
                    Dim FileName As String = Nothing
                    Dim FileNameAux As String = Nothing
                    Dim FileNameimagen As String = Nothing
                    Dim Image As String = String.Empty

                    Dim ExtensionAux = ".pdf"

                    FileNameAux = ItemFile.Item("Sigla_Serie").ToString() & "_" & ItemFile.Item("fk_Llave").ToString() & "_" & contador.ToString("0000") & ExtensionAux
                    FileName = UMVPlugin.AppPath & UMVPlugin.TempPath & FileNameAux


                    Dim document = New iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 0, 0, 0, 0)

                    Using pdfStream = New FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None)
                        Dim pdfw = iTextSharp.text.pdf.PdfWriter.GetInstance(document, pdfStream)
                        pdfw.PDFXConformance = iTextSharp.text.pdf.PdfWriter.PDFA1B
                        pdfw.SetPdfVersion(PdfWriter.PDF_VERSION_1_5)
                        pdfw.CompressionLevel = iTextSharp.text.pdf.PdfStream.BEST_COMPRESSION
                        document.Open()

                        For folio As Short = 1 To Folios
                            Dim bmp As System.Drawing.Image = Nothing
                            document.NewPage()
                            SyncLock BloqueoConcurrencia
                                nManager.GetFolio(CLng(ItemFile.Item("fk_Expediente").ToString()), CShort(ItemFile.Item("fk_Folder").ToString()), CShort(ItemFile.Item("id_File").ToString()), CShort(ItemFile.Item("id_Version").ToString()), folio, Imagen, Thumbnail)
                                bmp = Bitmap.FromStream(New MemoryStream(Imagen))
                                FileNameimagen = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & ".jpg"
                                Dim qualityParam As EncoderParameter = New EncoderParameter(Encoder.Quality, 20)
                                Dim jpegCodec As ImageCodecInfo = GetEncoderInfo("image/jpeg")
                                Dim encoderParams As EncoderParameters = New EncoderParameters(1)
                                encoderParams.Param(0) = qualityParam
                                bmp.Save(FileNameimagen, jpegCodec, encoderParams)
                            End SyncLock
                            If File.Exists(FileNameimagen) Then
                                Dim pdfImage = iTextSharp.text.Image.GetInstance(FileNameimagen)

                                'Scale the image to fit in the page
                                Dim percentage = 0.0F
                                percentage = 620 / pdfImage.Width

                                pdfImage.ScalePercent(percentage * 100)
                                pdfImage.CompressionLevel = iTextSharp.text.pdf.PdfStream.BEST_COMPRESSION
                                pdfImage.SetAbsolutePosition(0, 0)

                                document.Add(pdfImage)
                                pdfImage = Nothing
                                My.Computer.FileSystem.DeleteFile(FileNameimagen)
                            End If
                            bmp.Dispose()
                        Next
                        pdfw.SetFullCompression()
                        pdfw.CreateXmpMetadata()
                        document.Close()
                        pdfw.Close()
                        pdfw.Dispose()
                    End Using

                    document.Dispose()
                    '--------------------------------------------k-----------------------------
                    Slyg.Tools.Imaging.ImageManager.SaveToPdfA(FileName, nFolderName & FileNameAux, nFolderName)
                    '-------------------------------------------------------------------------
                    SyncLock BloqueoConcurrencia

                        For Each Item As Data.DataRow In FileDataTable.Select("fk_Expediente = " & ItemFile.Item("fk_Expediente").ToString & " AND fk_Folder = " & ItemFile.Item("fk_Folder").ToString & " AND id_File = " & ItemFile.Item("id_File").ToString).ToList()
                            If CLng(Item.Item("fk_Expediente")) = CLng(ItemFile.Item("fk_Expediente")) And CShort(Item.Item("fk_Folder")) = CShort(ItemFile.Item("fk_Folder")) And CShort(Item.Item("id_File")) = CShort(ItemFile.Item("id_File")) Then
                                Dim lineaRuta = ItemFile.Item("ReporteFiles").ToString().Replace("@Ruta", nFileRuta & FileNameAux)
                                Dim file As FileInfo = New FileInfo(nFolderName & FileNameAux)
                                Dim peso = Math.Round((file.Length / 1024), 2).ToString()
                                Item.Item("ReporteFiles") = lineaRuta.Replace("@PesoImagenFile", peso)
                                Exit For
                            End If
                        Next
                    End SyncLock
                    My.Computer.FileSystem.DeleteFile(FileName)
                Else
                    SyncLock BloqueoConcurrencia
                        EscribeLog(Me._StrArchivoLog, "La imagen del expediente, folder, file: " + CLng(ItemFile.Item("fk_Expediente")).ToString() + ", " + CLng(ItemFile.Item("fk_Folder")).ToString() + ", " + CLng(ItemFile.Item("id_File")).ToString() + " no existe.", False, True)
                    End SyncLock
                End If
            Catch ex As Exception
                SyncLock BloqueoConcurrencia
                    EscribeLog(Me._StrArchivoLog, "Error ExportarImagen: " + ex.Message, False, True)
                    EscribeLogExpedientes(Me._StrArchivoLog + "Expedientes", CLng(ItemFile.Item("fk_Expediente")).ToString() + vbTab + CLng(ItemFile.Item("fk_Folder")).ToString() + vbTab + CLng(ItemFile.Item("id_File")).ToString() + vbTab + CShort(ItemFile.Item("id_Version")).ToString() + vbTab + Llave.ToString(), False, True)
                End SyncLock
            Finally
                FileDataTable.Dispose()
            End Try
        End Sub

        Private Shared Function GetEncoderInfo(ByVal mimeType As String) As ImageCodecInfo
            Dim encoders As ImageCodecInfo()
            encoders = ImageCodecInfo.GetImageEncoders()

            For Each encoder In encoders
                If encoder.MimeType = mimeType Then Return encoder
            Next
            Return Nothing
        End Function

        Private Sub CrearImagen(ByVal nItemFile As DataRow, ByVal nCompresion As ImageManager.EnumCompression, ByVal nFolderName As String, ByVal contador As Integer, nFileRuta As String)
            Try
                Dim dtReport As New DataTable
                dtReport.Columns.Add("DEPENDENCIA")
                dtReport.Columns.Add("CODIGO_SERIE")
                dtReport.Columns.Add("CODIGO_SUBSERIE")
                dtReport.Columns.Add("TITULO")
                dtReport.Columns.Add("FECHA_INICIAL")
                dtReport.Columns.Add("OBSERVACIONES")
                dtReport.Columns.Add("CAJA_NO")
                dtReport.Columns.Add("NUMERO_DE_FOLIOS")
                dtReport.Columns.Add("PROCESO")
                dtReport.Columns.Add("NOMBRE_SERIE")
                dtReport.Columns.Add("NOMBRE_SUBSERIE")
                dtReport.Columns.Add("FECHA_FINAL")
                dtReport.Columns.Add("TOTAL_CARPETAS")
                dtReport.Columns.Add("OTROS")
                dtReport.AcceptChanges()

                Dim drReporte As DataRow = dtReport.NewRow()
                drReporte("DEPENDENCIA") = nItemFile("Dependencia").ToString()
                drReporte("CODIGO_SERIE") = nItemFile("CodigoSerie").ToString()
                drReporte("CODIGO_SUBSERIE") = ""
                drReporte("TITULO") = nItemFile("Titulo").ToString()
                drReporte("FECHA_INICIAL") = nItemFile("FechaInicial").ToString()
                drReporte("OBSERVACIONES") = ""
                drReporte("CAJA_NO") = nItemFile("Caja").ToString()
                drReporte("NUMERO_DE_FOLIOS") = nItemFile("TotalFolios").ToString()
                drReporte("PROCESO") = nItemFile("Proceso").ToString()
                drReporte("NOMBRE_SERIE") = nItemFile("NombreSerie").ToString()
                drReporte("NOMBRE_SUBSERIE") = ""
                drReporte("FECHA_FINAL") = nItemFile("FechaFinal").ToString()
                drReporte("TOTAL_CARPETAS") = nItemFile("TotalCarpetas").ToString()
                drReporte("OTROS") = ""

                dtReport.Rows.Add(drReporte)
                dtReport.AcceptChanges()

                Dim lc As New Microsoft.Reporting.WinForms.LocalReport
                lc.ReportEmbeddedResource = "UMV.Plugin.PortadaDocumentoPrincipal.rdlc"

                lc.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dtReport))

                Dim warnings As Microsoft.Reporting.WinForms.Warning() = Nothing
                Dim streamids As String() = Nothing
                Dim mimeType As String = Nothing
                Dim encoding As String = Nothing
                Dim filenameExtension As String = Nothing
                Dim bytes As Byte() = lc.Render("PDF", Nothing, mimeType, encoding, filenameExtension, streamids, warnings)

                Dim strPath As String = nFolderName & "\" & nItemFile("Sigla_Serie").ToString() & "_" & nItemFile("fk_Llave").ToString() & "_" & contador.ToString("0000") & ".pdf"

                nFileRuta = nFileRuta & nItemFile("Sigla_Serie").ToString() & "_" & nItemFile("fk_Llave").ToString() & "_" & contador.ToString("0000") & ".pdf"

                Using fs As FileStream = New FileStream(strPath, FileMode.Create)
                    fs.Write(bytes, 0, bytes.Length)
                End Using

                '--------------------------------------------k-----------------------------
                Slyg.Tools.Imaging.ImageManager.SaveToPdfA(strPath, nFolderName, nFileRuta)
                '-------------------------------------------------------------------------

                SyncLock BloqueoConcurrencia
                    For Each Item As Data.DataRow In FileDataTable.Select("fk_Expediente = " & nItemFile.Item("fk_Expediente").ToString & " AND fk_Folder = " & nItemFile.Item("fk_Folder").ToString & " AND id_File = " & nItemFile.Item("id_File").ToString).ToList()
                        If CLng(Item.Item("fk_Expediente")) = CLng(nItemFile.Item("fk_Expediente")) And CShort(Item.Item("fk_Folder")) = CShort(nItemFile.Item("fk_Folder")) And CShort(Item.Item("id_File")) = CShort(nItemFile.Item("id_File")) Then
                            Dim lineaRuta = nItemFile.Item("ReporteFiles").ToString().Replace("@Ruta", nFileRuta)
                            Dim file As FileInfo = New FileInfo(strPath)
                            Dim peso = Math.Round((file.Length / 1024), 2).ToString()
                            Item.Item("ReporteFiles") = lineaRuta.Replace("@PesoImagenFile", peso)
                        End If
                    Next
                End SyncLock
            Catch ex As Exception
                SyncLock BloqueoConcurrencia
                    EscribeLog(Me._StrArchivoLog, "Error ExportarImagen: " + ex.Message, False, True)
                End SyncLock
            End Try
        End Sub
        Private Sub Load_FormatoCargue()
            If (Not Usa_Exportacion_PDF) Then
                formatoAux = formato
                CompresionAux = compresion
            Else
                formatoAux = ImageManager.EnumFormat.Pdf
                CompresionAux = Utilities.GetEnumCompression(CType(formatoAux, DesktopConfig.FormatoImagenEnum))
            End If

        End Sub

        Private Sub EscribeLog(pathStrFile As String, StrLine As String, Optional CrearFile As Boolean = False, Optional LeerFile As Boolean = False)
            Dim modeFile As FileMode = Nothing
            Dim modeFile_2 As FileMode = Nothing
            Dim strMessageComplete As String = StrLine + Environment.NewLine
            Try
                If (CrearFile) Then
                    modeFile = FileMode.CreateNew
                    modeFile_2 = FileAccess.Write
                    Using fs As New FileStream(pathStrFile, modeFile)
                        Using w As New BinaryWriter(fs)
                            w.Write("Date : " + Date.Now.ToString() + " " + strMessageComplete)
                        End Using
                    End Using
                ElseIf (LeerFile) Then
                    modeFile = FileMode.Append
                    Using fs As New FileStream(pathStrFile, modeFile)
                        Using w As New BinaryWriter(fs)
                            w.Write(" Date : " + Date.Now.ToString() + " " + strMessageComplete)
                        End Using
                    End Using
                End If
            Catch ex As Exception
            End Try
        End Sub
        Private Sub EscribeLogExpedientes(pathStrFile As String, StrLine As String, Optional CrearFile As Boolean = False, Optional LeerFile As Boolean = False)
            Dim modeFile As FileMode = Nothing
            Dim modeFile_2 As FileMode = Nothing
            Dim strMessageComplete As String = StrLine + Environment.NewLine
            Try
                If (CrearFile) Then
                    modeFile = FileMode.CreateNew
                    modeFile_2 = FileAccess.Write
                    Using fs As New FileStream(pathStrFile, modeFile)
                        Using w As New BinaryWriter(fs)
                            w.Write(strMessageComplete)
                        End Using
                    End Using
                ElseIf (LeerFile) Then
                    modeFile = FileMode.Append
                    Using fs As New FileStream(pathStrFile, modeFile)
                        Using w As New BinaryWriter(fs)
                            w.Write(strMessageComplete)
                        End Using
                    End Using
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub EstibasDataGridView_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles EstibasDataGridView.CellContentClick
            If e.ColumnIndex = 1 Then
                For i = 0 To EstibasDataGridView.RowCount - 1
                    EstibasDataGridView.Rows(i).Cells("Exportar").Value = EstibasDataGridView.Rows(i).Cells("Exportar").Value
                Next
            End If
        End Sub
        Private Sub EstibasDataGridView_ColumnHeaderMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles EstibasDataGridView.CellContentDoubleClick
            If e.ColumnIndex = 1 Then
                For i = 0 To EstibasDataGridView.RowCount - 1
                    EstibasDataGridView.Rows(i).Cells("Exportar").Value = EstibasDataGridView.Rows(i).Cells("Exportar").Value
                Next
            End If
        End Sub
        Private Sub EstibasDataGridView_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles EstibasDataGridView.CellClick
            If Not e.RowIndex = -1 Then
                If EstibasDataGridView.Rows(e.RowIndex).Cells("Exportar").Value Then
                    EstibasDataGridView.Rows(e.RowIndex).Cells("Exportar").Value = False
                Else
                    EstibasDataGridView.Rows(e.RowIndex).Cells("Exportar").Value = True
                End If
            End If
        End Sub
#End Region

#Region " Funciones "
        Private Function Validar() As Boolean

            Dim EstibaRow = CType(CType(Me.EstibasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, DBIntegration.SchemaUMV.TBL_EstibaRow)

            If (Not Directory.Exists(CarpetaSalidaTextBox.Text)) Then
                MessageBox.Show("El directorio no existe, Seleccione un directorio existente", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()

                'ElseIf (Directory.GetDirectories(CarpetaSalidaTextBox.Text).Length > 0 Or Directory.GetFiles(CarpetaSalidaTextBox.Text).Length > 0) Then
                '    MessageBox.Show("La carpeta debe estar vacia para exportar los datos", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Me.CarpetaSalidaTextBox.Focus()

            ElseIf (Me.EstibasDataGridView.SelectedRows.Count = 0) Then
                MessageBox.Show("Se debe seleccionar una Estiba", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.EstibasDataGridView.Focus()

            ElseIf (EstibaRow.exportada) Then
                Dim Respuesta = MessageBox.Show("La Estiba: " & EstibaRow.CodigoEstiba & ", ya fue exportada, ¿desea volverla a exportar?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If (Respuesta = DialogResult.No) Then

                    Return False
                Else
                    Return True

                End If
            End If
            Return False
        End Function

#End Region




    End Class
End Namespace