Imports System.Windows.Forms
Imports System.IO
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Miharu.Tools.Progress
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library
Imports Slyg.Tools
Imports Miharu.Imaging.Library.Eventos
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Drawing
Imports iTextSharp.text.pdf
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports GraphicsMagick
Imports System.Runtime.InteropServices

Namespace Imaging.Exportar
    Public Class FormExport
#Region " Variables "
        Private Usa_Exportacion_PDF As Boolean
        Private formatoAux As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Private CompresionAux As Slyg.Tools.Imaging.ImageManager.EnumCompression
        Private formato As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Dim compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression

        Private FoldersExportacionTemp As New List(Of String)
        Private ViewExpedientes As New DataView
        Private ExpedientesSeleccion As New DataTable
        Public Shared FileNamesCons As New List(Of String)
        Dim FolderNameOutput As String

        Dim _EventManager As EventManager
        Dim PesoDeceval As Integer
#End Region

#Region " Propiedades "

        Public Property Plugin() As Plugin

#End Region

#Region " Eventos "
        Private Sub FormExport_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Usa_Exportacion_PDF = Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF
            formato = Utilities.GetEnumFormat(Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
            compresion = Utilities.GetEnumCompression(CType(Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

            Load_FormatoCargue()
        End Sub

        Private Sub BuscarFechaButton_Click(sender As System.Object, e As EventArgs) Handles BuscarFechaButton.Click
            CargarOTs()
        End Sub

        Private Sub BuscarCarpetaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarCarpetaButton.Click
            Dim Selector As New FolderBrowserDialog()

            Selector.SelectedPath = CarpetaSalidaTextBox.Text
            If (Selector.ShowDialog() = DialogResult.OK) Then
                Me.CarpetaSalidaTextBox.Text = Selector.SelectedPath
            End If
        End Sub

        Private Sub ExportarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportarButton.Click
            Exportar()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub
#End Region

#Region " Metodos "
        Private Sub CargarOTs()
            Me.OTDataGridView.AutoGenerateColumns = False
            Me.OTDataGridView.DataSource = getOTs()
            Me.OTDataGridView.Refresh()

            If (Me.OTDataGridView.RowCount = 0) Then
                MessageBox.Show("No se encontraron OTs para la fecha de proceso seleccionada", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub Exportar()
            If (Validar()) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
                Dim totalesDataTable As DBImaging.SchemaProcess.CTA_Exportacion_TotalesDataTable
                Dim progressForm As New Miharu.Tools.Progress.FormProgress
                Dim otRow = CType(CType(Me.OTDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Exportacion_OTRow)
                Dim reduccionPeso As Integer = 20

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                    totalesDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Totales.DBExecute(otRow.id_OT, Nothing)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try

                If (totalesDataTable.Rows.Count > 0) Then
                    Dim Respuesta As DialogResult

                    Respuesta = MessageBox.Show("Se encontró : " & vbCrLf & _
                                                totalesDataTable(0).Folders & " Unidades Documentales, " & vbCrLf & _
                                                totalesDataTable(0).Files & " Documentos, " & vbCrLf & _
                                                totalesDataTable(0).Folios & " Folios con " & vbCrLf & _
                                                (totalesDataTable(0).Tamaño / 1024 / 1024).ToString("#,##0.00") & "MB de tamaño, " & vbCrLf & _
                                                "¿Desea Exportar esta información?", Program.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If Respuesta = DialogResult.Yes Then
                        Try
                            Me.Enabled = False
                            dbmCore = New DBCore.DBCoreDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                            dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Plugin.DecevalConnectionString)

                            dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                            dbmCore.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                            dbmIntegration.Connection_Open(Plugin.Manager.Sesion.Usuario.id)


                            Dim reduccionPesoDataTable = dbmCore.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(Plugin.Manager.ImagingGlobal.Entidad, Plugin.Manager.ImagingGlobal.Proyecto, "ReduccionPeso")

                            If reduccionPesoDataTable.Rows.Count > 0 Then
                                reduccionPeso = Convert.ToInt32(reduccionPesoDataTable(0).Valor_Parametro_Sistema)
                            End If

                            Dim servidoresDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(otRow.id_OT)
                            Dim fileDataTable = dbmCore.SchemaReport.PA_Generar_Archivo_Deceval.DBExecute(otRow.id_OT)

                            Dim OutputFolder As String = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
                            Dim FilesDataView As New DataView(fileDataTable)
                            Dim Progreso As Integer = 0

                            progressForm.Show()

                            progressForm.SetProceso("Exportar")
                            progressForm.SetAccion("Obteniendo imágenes...")
                            progressForm.SetProgreso(0)
                            progressForm.SetMaxValue(FilesDataView.Count)

                            Application.DoEvents()

                            ' Define Nombre del Archivo Deceval
                            Dim NomArchivo = "E" & Format(Now(), "yyyyMMddHHmmss")

                            Dim Compresion As ImageManager.EnumCompression

                            If (Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                                Compresion = ImageManager.EnumCompression.Ccitt4
                            Else
                                Compresion = ImageManager.EnumCompression.Lzw
                            End If

                            For Each RowServidor In servidoresDataTable
                                Dim manager As FileProviderManager = Nothing

                                Try
                                    Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(RowServidor.fk_Entidad, RowServidor.id_Servidor)(0).ToCTA_ServidorSimpleType
                                    Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                                    manager = New FileProviderManager(servidor, centro, dbmImaging, Plugin.Manager.Sesion.Usuario.id)
                                    manager.Connect()

                                    ' Obtener los Files a transferir
                                    FilesDataView.RowFilter = "fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor

                                    For Each ItemFile As DataRowView In FilesDataView
                                        If progressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                                        Dim RowFile = CType(ItemFile.Row, DBCore.SchemaReport.CTA_Exportacion_Files_Archivo_DecevalRow)

                                        ' Enviar el archivo
                                        Dim TempFileName = ExportarImagen(manager, RowFile, Compresion, reduccionPeso)

                                        File.Move(TempFileName, OutputFolder & RowFile.Nombre_Imagen_File)

                                        NomArchivo = RowFile.Nombre_Imagen_File.Substring(0, 15)

                                        Progreso += 1
                                        progressForm.SetProgreso(Progreso)
                                        Application.DoEvents()
                                    Next
                                Catch ex As Exception
                                    If (manager IsNot Nothing) Then manager.Disconnect()
                                    Throw
                                End Try
                            Next
                            'Generación Archivos de salida
                            Dim sw = New StreamWriter(File.Open(OutputFolder & NomArchivo & ".csv", FileMode.Create), System.Text.Encoding.UTF8)
                            For Each Registro As DataRow In fileDataTable.Rows
                                sw.WriteLine(Registro("Reporte"))
                            Next
                            sw.Close()
                            sw.Dispose()

                            sw = New StreamWriter(File.Open(OutputFolder & NomArchivo.Replace("E", "EA") & ".csv", FileMode.Create), System.Text.Encoding.UTF8)
                            For Each Registro As DataRow In fileDataTable.Rows
                                sw.WriteLine(Registro("Nombre_Documento"))
                            Next
                            sw.Close()
                            sw.Dispose()

                            ' Crear la exportación
                            Dim ExportacionType = New DBImaging.SchemaProcess.TBL_ExportacionType()
                            ExportacionType.fk_Exportacion_Tipo = DBImaging.TipoExportacionEnum.PLIGIN
                            ExportacionType.Fecha_Exportacion = SlygNullable.SysDate
                            ExportacionType.fk_Usuario = Plugin.Manager.Sesion.Usuario.id
                            ExportacionType.fk_Sede_Procesamiento = Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Sede
                            ExportacionType.fk_Centro_Procesamiento = Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Centro_Procesamiento
                            ExportacionType.fk_Puesto_Trabajo = Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.id_Puesto_Trabajo
                            ExportacionType.Total_Folders = totalesDataTable(0).Folders
                            ExportacionType.Total_Files = totalesDataTable(0).Files
                            ExportacionType.Ruta = OutputFolder

                            Dim OTType = New DBImaging.SchemaProcess.TBL_OTType()
                            OTType.Exportado = True

                            Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBGet(otRow.id_OT)
                            Dim Exportado As Boolean = False
                            If (Not OTDataTable(0).Isfk_ExportacionNull()) Then
                                Exportado = dbmImaging.SchemaProcess.TBL_Exportacion.DBGet(OTDataTable(0).fk_Entidad_Procesamiento, OTDataTable(0).fk_Entidad, OTDataTable(0).fk_Proyecto, OTDataTable(0).fk_fecha_proceso, OTDataTable(0).fk_Exportacion).Count > 0
                            End If

                            Try
                                dbmImaging.Transaction_Begin()
                                If (Exportado) Then
                                    dbmImaging.SchemaProcess.TBL_Exportacion.DBUpdate(ExportacionType, OTDataTable(0).fk_Entidad_Procesamiento, OTDataTable(0).fk_Entidad, OTDataTable(0).fk_Proyecto, OTDataTable(0).fk_fecha_proceso, OTDataTable(0).fk_Exportacion)

                                    OTType.fk_Exportacion = OTDataTable(0).fk_Exportacion
                                Else
                                    ExportacionType.fk_Entidad_Procesamiento = OTDataTable(0).fk_Entidad_Procesamiento
                                    ExportacionType.fk_Entidad = OTDataTable(0).fk_Entidad
                                    ExportacionType.fk_Proyecto = OTDataTable(0).fk_Proyecto
                                    ExportacionType.fk_Fecha_Proceso = OTDataTable(0).fk_fecha_proceso
                                    ExportacionType.id_Exportacion = dbmImaging.SchemaProcess.TBL_Exportacion.DBNextId(OTDataTable(0).fk_Entidad_Procesamiento, OTDataTable(0).fk_Entidad, OTDataTable(0).fk_Proyecto, OTDataTable(0).fk_fecha_proceso)

                                    dbmImaging.SchemaProcess.TBL_Exportacion.DBInsert(ExportacionType)

                                    OTType.fk_Exportacion = ExportacionType.id_Exportacion
                                End If

                                dbmImaging.SchemaProcess.TBL_OT.DBUpdate(OTType, otRow.id_OT)

                                dbmImaging.Transaction_Commit()
                            Catch
                                dbmImaging.Transaction_Rollback()
                                Throw
                            End Try

                        Catch ex As Exception
                            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)

                            progressForm.Hide()
                            Application.DoEvents()

                            Return
                        Finally
                            Me.Enabled = True

                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                            If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()

                            BorrarTemporal()

                            progressForm.Close()
                        End Try
                        MessageBox.Show("La información se exportó con éxito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        CargarOTs()
                    Else
                        MessageBox.Show("Operación cancelada por Usuario", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                Else
                    MessageBox.Show("No se encontraron registros para exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If

            BorrarTemporal()
        End Sub

        ' Returns the image codec with the given mime type 
        Private Shared Function GetEncoderInfo(ByVal mimeType As String) As ImageCodecInfo
            ' Get image codecs for all image formats 
            Dim codecs As ImageCodecInfo() = ImageCodecInfo.GetImageEncoders()

            ' Find the correct image codec 
            For i As Integer = 0 To codecs.Length - 1
                If (codecs(i).MimeType = mimeType) Then
                    Return codecs(i)
                End If
            Next i

            Return Nothing
        End Function

        Private Function ExportarImagen(nManager As FileProviderManager, ByVal RowFile As DBCore.SchemaReport.CTA_Exportacion_Files_Archivo_DecevalRow, nCompresion As ImageManager.EnumCompression, ByVal reduccionPeso As Integer) As String

            Dim Resultado As String
            Dim Folios = nManager.GetFolios(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version)

            Dim FileNames As New List(Of String)
            Dim FileName As String = ""
            Dim FileNameAux As String = Nothing
            Dim ExtensionAux As String = String.Empty

            For folio As Short = 1 To Folios
                Dim Imagen() As Byte = Nothing
                Dim Thumbnail() As Byte = Nothing

                nManager.GetFolio(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version, folio, Imagen, Thumbnail)

                FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                FileNames.Add(FileName)

                FileNamesCons.Add(FileName)

                Dim ImagenByte As Byte() = Nothing
                Dim Imagebmp = Bitmap.FromStream(New MemoryStream(Imagen))
                Dim bmp = ImageManager.ComprimirImagen(Imagebmp, System.Drawing.Imaging.ImageFormat.Jpeg, reduccionPeso)
                ImagenByte = ImageToByte(bmp)

                Using fs = New FileStream(FileName, FileMode.Create)
                    fs.Write(ImagenByte, 0, ImagenByte.Length)
                    fs.Close()
                End Using
            Next

            If (FileNames.Count > 0) Then
                Dim Format As ImageManager.EnumFormat

                Select Case Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    Case ".bmp"
                        Format = ImageManager.EnumFormat.Bmp
                    Case ".gif"
                        Format = ImageManager.EnumFormat.Gif
                    Case ".jpg"
                        Format = ImageManager.EnumFormat.Jpeg
                        nCompresion = ImageManager.EnumCompression.Lzw
                    Case ".pdf"
                        Format = ImageManager.EnumFormat.Pdf
                        nCompresion = ImageManager.EnumCompression.Jpeg
                    Case ".png"
                        Format = ImageManager.EnumFormat.Png
                    Case ".tif"
                        Format = ImageManager.EnumFormat.Tiff
                End Select

                ExtensionAux = IIf(formatoAux = ImageManager.EnumFormat.Pdf, ".pdf", Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida).ToString

                FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & ExtensionAux

                '-------------------------------------------------------------------------
                ImageManager.Save(FileNames, FileName, "", formatoAux, CompresionAux, False, Program.AppPath & Program.TempPath, True, True)
                '-------------------------------------------------------------------------
            End If

            Resultado = FileName

            Return Resultado
        End Function

        Public Shared Function ImageToByte(ByVal img As Image) As Byte()
            Dim converter As ImageConverter = New ImageConverter()
            Return CType(converter.ConvertTo(img, GetType(Byte())), Byte())
        End Function

        ''' 
        Public Function RedimensionarImagen(img As Image) As Image
            Const max As Integer = 2600
            Dim h As Integer = img.Height
            Dim w As Integer = img.Width
            Dim newH, newW As Integer

            If h > w AndAlso h > max Then
                newH = max
                newW = (w * max) / h
            ElseIf w > h AndAlso w > max Then
                newW = max
                newH = (h * max) / w
            Else
                newH = h
                newW = w
            End If

            If h <> newH AndAlso w <> newW Then
                Dim newImg As Bitmap = New Bitmap(img, newW, newH)
                Dim g As Graphics = Graphics.FromImage(newImg)
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear
                g.DrawImage(img, 0, 0, newImg.Width, newImg.Height)
                Return newImg
            Else
                Return img
            End If
        End Function

        Private Shared Sub BorrarTemporal()
            Dim objDirectoryInfo = New DirectoryInfo(Program.AppPath & Program.TempPath)
            Dim fileInfoArray As DirectoryInfo() = objDirectoryInfo.GetDirectories()
            Dim objFileInfo As DirectoryInfo
            For Each objFileInfo In fileInfoArray
                Try
                    Directory.Delete(objFileInfo.FullName, True)
                    'objFileInfo.Delete()
                Catch
                End Try
            Next objFileInfo
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
#End Region

#Region " Funciones "
        Private Function Validar() As Boolean
            If (Not Directory.Exists(CarpetaSalidaTextBox.Text)) Then
                MessageBox.Show("El directorio no existe, Seleccione un directorio existente", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()

            ElseIf (Directory.GetDirectories(CarpetaSalidaTextBox.Text).Length > 0 Or Directory.GetFiles(CarpetaSalidaTextBox.Text).Length > 0) Then
                MessageBox.Show("La carpeta debe estar vacia para exportar los datos", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()

            ElseIf (Me.OTDataGridView.SelectedRows.Count = 0) Then
                MessageBox.Show("Se debe seleccionar una OT", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.OTDataGridView.Focus()

            Else
                Dim OTRow = CType(CType(Me.OTDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Exportacion_OTRow)

                ' Validar si ya fue exportado
                If (OTRow.Exportado) Then
                    Dim Respuesta = MessageBox.Show("La OT: " & OTRow.id_OT & ", ya fue exportada, ¿desea volverla a exportar?", Program.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If (Respuesta = DialogResult.No) Then Return False
                End If

                ' Validar si la OT se puede exportar
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                    Dim Resultado = dbmImaging.SchemaProcess.PA_Validar_Cargado_Completo.DBExecute(OTRow.id_OT)

                    If (Not Resultado) Then
                        MessageBox.Show("La OT no ha sido totalmente procesada y no se puede exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.OTDataGridView.Focus()
                    Else
                        Return True
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If

            Return False
        End Function

        Private Function getFecha() As Integer
            Return CInt(Me.FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"))
        End Function

        Private Function getOTs() As DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                Return dbmImaging.SchemaProcess.CTA_Exportacion_OT.DBFindByfk_Entidad_Procesamientofk_Entidadfk_Proyectofk_fecha_proceso(Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _
                                                                                                                                        Plugin.Manager.ImagingGlobal.Entidad, _
                                                                                                                                        Plugin.Manager.ImagingGlobal.Proyecto, _
                                                                                                                                        getFecha())
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return New DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable()
        End Function
#End Region


    End Class
End Namespace