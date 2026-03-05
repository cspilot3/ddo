Imports System.IO
Imports System.Drawing.Drawing2D
Imports DBImaging.SchemaProcess
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Imaging

Namespace Firmas.Forms.Exportar
    Public Class FormExportar

#Region " Declaraciones "
        Private _Plugin As FirmasImagingPlugin
        Public Shared Fecha_Proceso As Integer
#End Region

#Region " Constructores "
        Public Sub New(ByVal nBanagrarioImaginPlugin As FirmasImagingPlugin)
            InitializeComponent()
            _Plugin = nBanagrarioImaginPlugin
        End Sub
#End Region

#Region " Eventos "

        Private Sub CargueGetInfo_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LoadConfig()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            GeneraArchivoBinarioFirmas()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
        End Sub

        Private Sub BuscarFechaButton_Click(sender As System.Object, e As EventArgs) Handles BuscarFechaButton.Click
            CargarOTs()
        End Sub

        Private Sub BuscarCarpetaButton_Click(sender As System.Object, e As EventArgs) Handles BuscarCarpetaButton.Click
            Dim Selector As New FolderBrowserDialog()

            Selector.SelectedPath = CarpetaSalidaTextBox.Text
            If (Selector.ShowDialog() = DialogResult.OK) Then
                Me.CarpetaSalidaTextBox.Text = Selector.SelectedPath
            End If

        End Sub


        Private Sub FechaProcesoPicker_ValueChanged(sender As Object, e As EventArgs) Handles FechaProcesoPicker.ValueChanged
            CargarOTs()
        End Sub
#End Region

#Region " Metodos "

        Private Sub LoadConfig()

            Dim NewDate = Now
            If (NewDate.Hour < _Plugin.HoraCambioFechaProceso) Then
                NewDate = NewDate.AddDays(-1)
            End If
            FechaProcesoPicker.Value = NewDate
        End Sub

        Private Sub GeneraArchivoBinarioFirmas()
            Me.Enabled = False

            AvanceProgressBar.Value = 0

            If (Validar()) Then
                Dim fechaProceso = CInt(FechaProcesoPicker.Value.ToString("yyyyMMdd"))
                Dim listBytes As New List(Of Byte())()
                Const SeparadorCampos As String = "|<>|"
                Const SeparadorRegistros As String = "<||>"
                Dim SepCamposByte As [Byte]() = StrToByteArray(SeparadorCampos)
                Dim SepRegistrosByte As [Byte]() = StrToByteArray(SeparadorRegistros)

                Dim Imagen As [Byte]()

                Dim imagePath As String

                Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

                Try
                    dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                    dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    Dim data = dbmAgrario.SchemaFirmas.PA_GenerarArchivoBinario.DBExecute(fechaProceso, _Plugin.Manager.Sesion.Usuario.id)
                    Dim i As Integer = 0

                    If (data.Rows.Count > 0) Then
                        AvanceProgressBar.Maximum = data.Rows.Count

                        Dim Respuesta = MessageBox.Show("Se encontró : " & vbCrLf & _
                                                        data.Rows.Count & " Recorte(s)" & vbCrLf & _
                                                        "¿Desea Exportar esta información?", "Firmas Banco Agrario", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                        If Respuesta = DialogResult.Yes Then
                            Dim dbmImaging As DBImaging.DBImagingDataBaseManager
                            Dim manager As FileProviderManager = Nothing

                            dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                            dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                            Try
                                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(_Plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType()
                                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                                manager = New FileProviderManager(servidor, centro, dbmImaging, _Plugin.Manager.Sesion.Usuario.id)
                                manager.Connect()

                                For Each ItemFile In data
                                    Dim Thumbnail() As Byte = Nothing
                                    Dim Img() As Byte = Nothing
                                    manager.GetFolio(ItemFile.fk_Expediente, ItemFile.fk_Folder, ItemFile.fk_File, ItemFile.fk_Version, ItemFile.Folio, Img, Thumbnail)
                                    ' Dim BitMapImg As New Bitmap(New MemoryStream(Img))

                                    Dim rect As New Rectangle(ItemFile.X, ItemFile.Y, ItemFile.Ancho, ItemFile.Alto)
                                    Dim ImagenFin = cropImage(Image.FromStream(New MemoryStream(Img)), rect)

                                    imagePath = Trim(CarpetaSalidaTextBox.Text) + "\TEMP\" + (i + 1).ToString + "_" + ItemFile.File_Unique_Identifier.ToString + ".TIF"
                                    Select Case ItemFile.Angulo
                                        Case 90 : ImagenFin.RotateFlip(RotateFlipType.Rotate90FlipNone)
                                        Case 180 : ImagenFin.RotateFlip(RotateFlipType.Rotate180FlipNone)
                                        Case 270 : ImagenFin.RotateFlip(RotateFlipType.Rotate270FlipNone)
                                    End Select

                                    'ImagenFin = ResizeImage(ImagenFin, New Size(325, 154), True)

                                    Dim BitMapImgRecorte As New Bitmap(ImagenFin)
                                    Dim CambioResolucion = dbmAgrario.SchemaConfig.TBL_Parametro.DBFindByNombre_Parametro("AplicaCambioResolucion")
                                    Dim dpi As Integer = 0
                                    If CambioResolucion.Rows.Count > 0 Then
                                        If Integer.Parse(CambioResolucion(0).Valor_Parametro) = 1 Then

                                            Dim dpiResolucion = dbmAgrario.SchemaConfig.TBL_Parametro.DBFindByNombre_Parametro("CambioResolucionDpi")
                                            If dpiResolucion.Count > 0 Then
                                                dpi = Integer.Parse(dpiResolucion(0).Valor_Parametro)
                                            End If
                                        End If
                                    End If

                                    'dpi = 200

                                    If dpi <> 0 Then
                                        BitMapImgRecorte.SetResolution(dpi, dpi)
                                    End If

                                    If BlancoNegroRadioButton.Checked Then
                                        BitMapImgRecorte = ToBlackAndWhite(BitMapImgRecorte)
                                    ElseIf GrisesRadioButton.Checked Then
                                        BitMapImgRecorte = ToGrayScale(BitMapImgRecorte)
                                    End If

                                    BitMapImgRecorte = ResizeImage(BitMapImgRecorte, New Size(620, 300), True)

                                    ImageManager.Save(New FreeImageAPI.FreeImageBitmap(BitMapImgRecorte), imagePath, "", ImageManager.EnumFormat.TIFF, ImageManager.EnumCompression.LZW, False, "C:\FirmasAgrario")
                                    imagePath = ConvertTiffToJpeg(imagePath, dpi)
                                    i += 1
                                    '
                                    Imagen = File.ReadAllBytes(imagePath)
                                    ' Secuencial
                                    listBytes.Add(StrToByteArray(i.ToString()))
                                    listBytes.Add(SepCamposByte)
                                    ' Imágen
                                    listBytes.Add(Imagen)
                                    listBytes.Add(SepCamposByte)
                                    ' Tipo Imágen
                                    listBytes.Add(StrToByteArray(ItemFile.Tipo_Registro))
                                    listBytes.Add(SepCamposByte)
                                    ' Clase de enté
                                    listBytes.Add(StrToByteArray(ItemFile.ClaseEnte))
                                    listBytes.Add(SepCamposByte)
                                    ' Código del ente
                                    listBytes.Add(StrToByteArray(ItemFile.Ente.ToString()))
                                    listBytes.Add(SepCamposByte)
                                    listBytes.Add(StrToByteArray(ItemFile.Producto))
                                    ' Código producto tarjeta
                                    listBytes.Add(SepCamposByte)
                                    ' Número de cuenta

                                    '
                                    If ItemFile.IsNumero_CuentaNull Then
                                        listBytes.Add(StrToByteArray(""))
                                    Else
                                        listBytes.Add(StrToByteArray(ItemFile.Numero_Cuenta.ToString()))
                                    End If

                                    listBytes.Add(SepCamposByte)
                                    ' Código Transacción
                                    listBytes.Add(StrToByteArray(ItemFile.CodigoTransaccion))
                                    listBytes.Add(SepCamposByte)
                                    'Fecha de proceso
                                    listBytes.Add(StrToByteArray(ItemFile.Fecha_Movimiento))
                                    listBytes.Add(SepCamposByte)
                                    listBytes.Add(SepCamposByte)
                                    listBytes.Add(SepRegistrosByte)

                                    AvanceProgressBar.Value += 1
                                Next
                            Catch ex As Exception
                                If (manager IsNot Nothing) Then manager.Disconnect()
                                MessageBox.Show(ex.Message, "Firmas Banco Agrario", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try

                            Dim horaProceso = DateTime.Now.ToString("HHmm")
                            Dim NombreArchBin As String = Trim(CarpetaSalidaTextBox.Text) & "\fi_cg_imadig_" & fechaProceso.ToString & horaProceso & ".bin"

                            File.WriteAllBytes(NombreArchBin, Combine(listBytes))
                            MessageBox.Show("La exportación se realizó con exito", "Firmas Banco Agrario", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        MessageBox.Show("No se encontró data para exportar, por favor revise si se realizó el cruce.", "Firmas Banco Agrario", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                Catch
                    Throw
                Finally
                    If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                End Try
            End If

            Me.Enabled = True
        End Sub

        Private Shared Function cropImage(img As Image, cropArea As Rectangle) As Image
            Dim bmpImage = New Bitmap(img)
            Dim bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat)
            Return CType(bmpCrop, Image)
        End Function

        Public Shared Function ResizeImage(ByVal image As Bitmap, ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = True) As Bitmap
            Dim newWidth As Integer
            Dim newHeight As Integer
            If preserveAspectRatio Then
                Dim originalWidth As Integer = image.Width
                Dim originalHeight As Integer = image.Height
                Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
                Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
                Dim percent As Single = If(percentHeight < percentWidth,
                                           percentHeight, percentWidth)
                newWidth = CInt(originalWidth * percent)
                newHeight = CInt(originalHeight * percent)
            Else
                newWidth = size.Width
                newHeight = size.Height
            End If
            Dim newImage As Bitmap = New Bitmap(newWidth, newHeight)
            newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution)
            Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
            End Using
            Return newImage
        End Function

        Public Shared Function Combine(ByVal arrays As List(Of Byte())) As Byte()
            Dim length As Integer = 0

            For Each item In arrays
                length += item.Length
            Next

            Dim ret = New Byte(length - 1) {}

            Dim offset As Integer = 0
            For Each data As Byte() In arrays
                Buffer.BlockCopy(data, 0, ret, offset, data.Length)
                offset += data.Length
            Next
            Return ret
        End Function

        Private Sub CargarOTs()
            Me.OTDataGridView.AutoGenerateColumns = False
            Me.OTDataGridView.DataSource = getOTs()
            Me.OTDataGridView.Refresh()

            If (Me.OTDataGridView.RowCount = 0) Then
                MessageBox.Show("No se encontraron OTs para la fecha de proceso seleccionada", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub
#End Region

#Region " Funciones "

        'Private Function FechaInvalida() As Boolean
        '    Dim horas As TimeSpan = Date.Now.TimeOfDay
        '    Dim hreglamentaria As TimeSpan = TimeSpan.Parse(_Plugin.HoraCambioFechaProceso & ":00:00")
        '    Dim fechahoy As DateTime = CDate(Date.Now.ToShortDateString)
        '    Dim fechaselec As DateTime = CDate(FechaProcesoPicker.Value.ToShortDateString)
        '    If fechaselec = fechahoy Then
        '        Return False
        '    Else
        '        If fechaselec = fechahoy.AddDays(-1) Then
        '            If horas <= hreglamentaria Then
        '                Return False
        '            Else
        '                Return True
        '            End If
        '        Else
        '            If fechaselec > fechahoy Then
        '                Return False
        '            End If
        '        End If
        '        Return True
        '    End If
        'End Function

        Public Shared Function StrToByteArray(ByVal str As String) As Byte()
            Dim encoding As New System.Text.UTF8Encoding()
            Return encoding.GetBytes(str)
        End Function

        Public Shared Function Combine(ByVal first As Byte(), ByVal second As Byte()) As Byte()
            Dim ret As Byte() = New Byte(first.Length + (second.Length - 1)) {}
            Buffer.BlockCopy(first, 0, ret, 0, first.Length)
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length)
            Return ret
        End Function

        'Public Function ToBlackAndWhite(ByVal bmp As Bitmap) As Bitmap
        '    Dim x As Integer
        '    Dim y As Integer
        '    Dim r, g, b As Double
        '    Dim col As Color

        '    For x = 0 To bmp.Width - 1
        '        For y = 0 To bmp.Height - 1

        '            col = bmp.GetPixel(x, y)
        '            r = col.R * 0.3
        '            g = col.G * 0.59
        '            b = col.B * 0.11

        '            'R/G/B = (red * 0.30) + (green * 0.59) + (blue * 0.11)

        '            col = Color.FromArgb(255, CInt(r), CInt(g), CInt(b))
        '            bmp.SetPixel(x, y, col)


        '        Next y
        '    Next x

        '    ToBlackAndWhite = bmp
        'End Function

        Public Function ToBlackAndWhite(ByVal bmp As Bitmap) As Bitmap
            Dim x As Integer
            Dim y As Integer
            Dim gem As Integer
            Dim r, g, b As Integer
            Dim col As Color

            For x = 0 To bmp.Width - 1
                For y = 0 To bmp.Height - 1
                    col = bmp.GetPixel(x, y)
                    r = col.R
                    g = col.G
                    b = col.B
                    gem = CInt((r + g + b) / 3)

                    If gem > 150 Then
                        bmp.SetPixel(x, y, Color.White)
                    Else
                        bmp.SetPixel(x, y, Color.Black)
                    End If

                Next y
            Next x

            ToBlackAndWhite = bmp
        End Function

        Public Function ToGrayScale(ByVal bmp As Bitmap) As Bitmap
            Dim bm As New Bitmap(bmp)
            Dim X As Integer
            Dim Y As Integer
            Dim clr As Integer

            For X = 0 To bm.Width - 1
                For Y = 0 To bm.Height - 1
                    clr = (CInt(bm.GetPixel(X, Y).R) + _
                           bm.GetPixel(X, Y).G + _
                           bm.GetPixel(X, Y).B) \ 3
                    bm.SetPixel(X, Y, Color.FromArgb(clr, clr, clr))
                Next Y
            Next X

            Return bm
        End Function

        Public Shared Function ConvertTiffToJpeg(fileName As String, dpi As Integer) As String
            Using imageFile As Image = Image.FromFile(fileName)
                Dim frameDimensions As New FrameDimension(imageFile.FrameDimensionsList(0))

                ' Gets the number of pages from the tiff image (if multipage) 
                Dim frameNum As Integer = imageFile.GetFrameCount(frameDimensions)
                Dim jpegPaths As String = "" '() = New String(frameNum - 1) {}

                For frame As Integer = 0 To frameNum - 1
                    ' Selects one frame at a time and save as jpeg. 
                    imageFile.SelectActiveFrame(frameDimensions, frame)
                    Using bmp As New Bitmap(imageFile)
                        If dpi <> 0 Then
                            bmp.SetResolution(dpi, dpi)
                        End If
                        jpegPaths = [String].Format("{0}\{1}{2}.jpg", Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName), frame)
                        bmp.Save(jpegPaths, ImageFormat.Jpeg)
                        Return jpegPaths
                    End Using
                Next
                Return jpegPaths
            End Using
        End Function

        Private Function getOTs() As DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Return dbmImaging.SchemaProcess.CTA_Exportacion_OT.DBFindByfk_Entidad_Procesamientofk_Entidadfk_Proyectofk_fecha_proceso(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _
                                                                                                                                         _Plugin.Manager.ImagingGlobal.Entidad, _
                                                                                                                                         _Plugin.Manager.ImagingGlobal.Proyecto, _
                                                                                                                                         getFecha())
            Catch ex As Exception
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return New DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable()
        End Function

        Private Function Validar() As Boolean
            If (Not Directory.Exists(CarpetaSalidaTextBox.Text)) Then
                MessageBox.Show("El directorio no existe, Seleccione un directorio existente", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()
                Return False
            ElseIf (Directory.GetDirectories(CarpetaSalidaTextBox.Text).Length > 0 Or Directory.GetFiles(CarpetaSalidaTextBox.Text).Length > 0) Then
                MessageBox.Show("La carpeta debe estar vacia para exportar los datos", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()
                Return False
            ElseIf (Me.OTDataGridView.SelectedRows.Count = 0) Then
                MessageBox.Show("Se debe seleccionar una OT", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.OTDataGridView.Focus()
                Return False
            Else
                Dim OTRow = CType(CType(Me.OTDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Exportacion_OTRow)

                ' Validar si ya fue exportado
                If (OTRow.Exportado) Then
                    Dim Respuesta = MessageBox.Show("La OT: " & OTRow.id_OT & ", ya fue exportada, ¿desea volverla a exportar?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If (Respuesta = DialogResult.No) Then Return False
                End If

                ' Validar si la OT se puede exportar
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    Dim OTDataTable = dbmImaging.SchemaProcess.CTA_Exportacion_OT.DBFindByfk_Entidad_Procesamientofk_Entidadfk_Proyectofk_fecha_proceso(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _
                                                                                                                                         _Plugin.Manager.ImagingGlobal.Entidad, _
                                                                                                                                         _Plugin.Manager.ImagingGlobal.Proyecto, _
                                                                                                                                         CInt(FechaProcesoPicker.Value.ToString("yyyyMMdd")))

                    For Each ctaExportacionOtRow As CTA_Exportacion_OTRow In OTDataTable

                        Dim Resultado = dbmImaging.SchemaProcess.PA_Validar_Cargado_Completo.DBExecute(ctaExportacionOtRow.id_OT)

                        If (Not Resultado) Then
                            Me.OTDataGridView.Focus()
                            Throw New Exception("La OT: " + ctaExportacionOtRow.id_OT.ToString() + " no ha sido totalmente procesada y no se puede exportar")
                        End If
                    Next
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If

            Return True
        End Function

        Private Function getFecha() As Integer
            Return CInt(Me.FechaProcesoPicker.Value.ToString("yyyyMMdd"))
        End Function

#End Region

    End Class
End Namespace