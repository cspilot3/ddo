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
Imports System.Xml.Linq
Imports System.Linq

Namespace Procesos.Exportar

    Public Class FormExportPDF
        Inherits FormBase

#Region " Declaraciones "
        Private Usa_Exportacion_PDF As Boolean
        Private formatoAux As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Private CompresionAux As Slyg.Tools.Imaging.ImageManager.EnumCompression
        Private formato As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Dim compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression

        Private ViewExpedientes As New DataView
        Private ExpedientesSeleccion As New DataTable
        Public Shared FileNamesCons As New List(Of String)
        Dim FolderNameOutput As String

        Dim _EventManager As EventManager
#End Region

#Region " Propiedades"
        Public Property EventManager As EventManager
            Get
                Return Me._EventManager
            End Get
            Set(value As EventManager)
                _EventManager = value
            End Set
        End Property
#End Region

#Region " Eventos "
        Private Sub FormExport_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            If Not (Program.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_Validos) Then
                CheckBoxExpedientesValidos.Visible = False
            End If

            Usa_Exportacion_PDF = Program.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF
            formato = Utilities.GetEnumFormat(Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
            compresion = Utilities.GetEnumCompression(CType(Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

            Load_FormatoCargue()

        End Sub

        Private Sub BuscarFechaButton_Click(sender As System.Object, e As EventArgs) Handles BuscarFechaButton.Click

            If validarFechaProceso() Then
                If Not Me.CheckBoxExpedientes.Checked Then
                    CargarOTs()
                Else
                    CargarExpedientes()
                End If
            End If



        End Sub

        Private Sub BuscarCarpetaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarCarpetaButton.Click
            Dim Selector As New FolderBrowserDialog()

            Selector.SelectedPath = CarpetaSalidaTextBox.Text
            If (Selector.ShowDialog() = DialogResult.OK) Then
                Me.CarpetaSalidaTextBox.Text = Selector.SelectedPath
            End If
        End Sub

        Private Sub ExportarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportarButton.Click

            If Not CheckBoxExpedientes.Checked Then
                ExportarOTs()
            Else
                ExportarExpedientes()
            End If

        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

        Private Sub CheckBoxExpedientes_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxExpedientes.CheckedChanged
            If CheckBoxExpedientes.Checked Then
                CheckBoxExpedientesValidos.Checked = False
            End If
            MostrarDatagrid()
        End Sub

        Private Sub CheckBoxExpedientesValidos_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxExpedientesValidos.CheckedChanged
            If CheckBoxExpedientesValidos.Checked Then
                CheckBoxExpedientes.Checked = False
            End If
            MostrarDatagrid()
        End Sub

        Private Sub ExpedientesDataGridView_ColumnHeaderMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles ExpedientesDataGridView.ColumnHeaderMouseDoubleClick
            If e.ColumnIndex = 3 Then
                For i = 0 To ExpedientesDataGridView.RowCount - 1
                    ExpedientesDataGridView.Rows(i).Cells("Exportar").Value = True
                Next
            End If
        End Sub
#End Region

#Region " Metodos "
        Private Sub CargarOTs()
            Me.OTDataGridView.AutoGenerateColumns = False
            Me.OTDataGridView.DataSource = getOTs()
            Me.OTDataGridView.Refresh()

            If (Me.OTDataGridView.RowCount = 0) Then
                MessageBox.Show("No se encontraron OTs para el rango de fechas de proceso seleccionadas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub CargarExpedientes()
            Me.ExpedientesDataGridView.AutoGenerateColumns = False
            Me.ExpedientesDataGridView.DataSource = getExpedientes()
            Me.ExpedientesDataGridView.Refresh()

            If (Me.ExpedientesDataGridView.RowCount = 0) Then
                MessageBox.Show("No se encontraron Expedientes para el rango de fechas de proceso seleccionadas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub ExportarOTs()
            If (Validar()) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Dim dbmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing
                Dim TotalesDataTable As DBImaging.SchemaProcess.CTA_Exportacion_TotalesDataTable
                Dim ProgressForm As New FormProgress
                Dim OTRow = CType(CType(Me.OTDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Exportacion_OTRow)
                FileNamesCons = New List(Of String)

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    If (CheckBoxExpedientesValidos.Visible) Then
                        TotalesDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Totales.DBExecute(OTRow.id_OT, CheckBoxExpedientesValidos.Checked)
                    Else
                        TotalesDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Totales.DBExecute(OTRow.id_OT, Nothing)
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try

                If (TotalesDataTable.Rows.Count > 0) Then
                    Dim Respuesta As DialogResult

                    Respuesta = MessageBox.Show("Se encontró : " & vbCrLf & _
                                                TotalesDataTable(0).Folders & " Unidades Documentales, " & vbCrLf & _
                                                TotalesDataTable(0).Files & " Documentos, " & vbCrLf & _
                                                TotalesDataTable(0).Folios & " Folios con " & vbCrLf & _
                                                (TotalesDataTable(0).Tamaño / 1024 / 1024).ToString("#,##0.00") & "MB de tamaño, " & vbCrLf & _
                                                "¿Desea Exportar esta información?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If Respuesta = DialogResult.Yes Then
                        Try
                            Me.Enabled = False

                            dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                            dbmArchiving = New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                            dbmImaging.Connection_Open(1)
                            dbmCore.Connection_Open(1)
                            dbmArchiving.Connection_Open(1)

                            'Dim FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                            Dim FileDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable
                            'Dim FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                            'Dim FileValidacionDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable

                            Dim ServidoresDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(OTRow.id_OT)

                            FileDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Files.DBExecute(OTRow.id_OT, CheckBoxExpedientesValidos.Checked)
                            
                            Dim OutputFolder As String = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
                            Dim FilesDataView As New DataView(FileDataTable)
                            Dim Progreso As Integer = 0
                            Dim nExpediente As String = "000000"

#If Not Debug Then
                            ProgressForm.Show()
#End If

                            ProgressForm.SetProceso("Exportar")
                            ProgressForm.SetAccion("Obteniendo imágenes...")
                            ProgressForm.SetProgreso(0)
                            ProgressForm.SetMaxValue(TotalesDataTable(0).Folios)

                            Application.DoEvents()

                            ' Crear el directorio de las imágenes
                            'Directory.CreateDirectory(OutputFolder & "images")

                            ' Define Nombre del Archivo Deceval
                            'Dim NomArchivo = "E" & Format(Now(), "yyyyMMddHHmmss")

                            Dim Compresion As ImageManager.EnumCompression

                            If (Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                                Compresion = ImageManager.EnumCompression.Ccitt4
                            Else
                                Compresion = ImageManager.EnumCompression.Lzw
                            End If

                            For Each RowServidor In ServidoresDataTable
                                Dim manager As FileProviderManager = Nothing
                                Dim tempFileName As String = ""
                                Dim ExportFilename As String = ""
                                Dim NumFolio As Integer = 0

                                Try
                                    Dim servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(OTRow.id_OT)(0).ToCTA_ServidorSimpleType()
                                    Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                                    manager = New FileProviderManager(servidor, centro, dbmImaging, Program.Sesion.Usuario.id)
                                    manager.Connect()

                                    ' Obtener los Files a transferir
                                    FilesDataView.RowFilter = "fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor
                                    Dim fic As String = OutputFolder & "Imagenes_" & OTRow.id_OT & ".txt"
                                    Dim sw As New System.IO.StreamWriter(fic, False, System.Text.Encoding.UTF8)

                                    For Each ItemFile As DataRowView In FilesDataView
                                        If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                                        Dim RowFile = CType(ItemFile.Row, DBImaging.SchemaProcess.CTA_Exportacion_FilesRow)

                                        If nExpediente <> RowFile.fk_Expediente.ToString Then
                                            If nExpediente <> "000000" Then
                                                ExportFilename = tempFileName & ".pdf"

                                                ImageManager.CreatePdf(NumFolio, tempFileName, ExportFilename)
                                                Dim NomArchivo = dbmArchiving.Schemadbo.CTA_Proyecto_Llave_Expediente_Llave.DBFindByfk_Expedientefk_proyecto_Llave(RowFile.fk_Expediente, 1)
                                                'If (TipoDocumento(0).Es_Obligatorio) Then
                                                sw.WriteLine(NomArchivo(0).Valor_Llave.ToString)
                                                File.Move(ExportFilename, OutputFolder & NomArchivo(0).Valor_Llave.ToString & ".pdf")
                                                NumFolio = 0
                                            End If
                                            tempFileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString()
                                            NumFolio = ExportarImagen(manager, RowFile, Compresion, tempFileName, NumFolio)
                                        Else
                                            ' Enviar el archivo
                                            NumFolio = ExportarImagen(manager, RowFile, Compresion, tempFileName, NumFolio)
                                        End If

                                        Progreso += 1
                                        ProgressForm.SetProgreso(Progreso)
                                        Application.DoEvents()
                                        nExpediente = RowFile.fk_Expediente.ToString
                                    Next
                                    sw.Close()
                                Catch ex As Exception
                                    If (manager IsNot Nothing) Then manager.Disconnect()
                                    Throw (ex)
                                End Try
                            Next

                            ' Crear la exportación

                            Dim ExportacionType = New DBImaging.SchemaProcess.TBL_ExportacionType()

                            ExportacionType.Fecha_Exportacion = SlygNullable.SysDate
                            ExportacionType.fk_Usuario = Program.Sesion.Usuario.id
                            ExportacionType.fk_Sede_Procesamiento = Program.DesktopGlobal.PuestoTrabajoRow.fk_Sede
                            ExportacionType.fk_Centro_Procesamiento = Program.DesktopGlobal.PuestoTrabajoRow.fk_Centro_Procesamiento
                            ExportacionType.fk_Puesto_Trabajo = Program.DesktopGlobal.PuestoTrabajoRow.id_Puesto_Trabajo
                            ExportacionType.Total_Folders = TotalesDataTable(0).Folders
                            ExportacionType.Total_Files = TotalesDataTable(0).Files
                            ExportacionType.Ruta = OutputFolder

                            Dim OTType = New DBImaging.SchemaProcess.TBL_OTType()
                            OTType.Exportado = True

                            Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBGet(OTRow.id_OT)
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

                                dbmImaging.SchemaProcess.TBL_OT.DBUpdate(OTType, OTRow.id_OT)

                                dbmImaging.Transaction_Commit()
                            Catch
                                dbmImaging.Transaction_Rollback()
                                Throw
                            End Try

                        Catch ex As Exception
                            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)

                            ProgressForm.Hide()
                            Application.DoEvents()

                            Return
                        Finally
                            Me.Enabled = True

                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                            If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()


                            BorrarTemporal()

                            ProgressForm.Close()
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

        Private Function ExportarImagen(nManager As FileProviderManager, ByVal RowFile As DBImaging.SchemaProcess.CTA_Exportacion_FilesRow, nCompresion As ImageManager.EnumCompression, tempFileName As String, NumAr As Integer) As Integer
            Dim Resultado As String
            Dim Folios = nManager.GetFolios(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version)

            Dim FileNames As New List(Of String)
            Dim FileName As String = ""
            Dim FileNameAux As String = Nothing
            Dim ExtensionAux As String = String.Empty

            For folio As Short = 1 To Folios
                Dim Imagen() As Byte = Nothing
                Dim Thumbnail() As Byte = Nothing
                NumAr = NumAr + 1

                nManager.GetFolio(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version, folio, Imagen, Thumbnail)

                FileName = tempFileName & NumAr.ToString("-0000") & ".jpg"

                FileNames.Add(FileName)

                FileNamesCons.Add(FileName)

                Using fs As New FileStream(FileName, FileMode.CreateNew)
                    fs.Write(Imagen, 0, Imagen.Length)
                End Using
            Next

            'Dim ExportFilename As String = tempFileName
            'ExportFilename &= ".pdf"

            'ImageManager.CreatePdf(Folios, tempFileName, ExportFilename)

            Return NumAr
        End Function

        Private Sub ExportAllFillesInTiff(nCompresion As ImageManager.EnumCompression, ByVal nFileName As String)
            Try

                If FileNamesCons.Count > 0 Then

                    Dim FileName As String = nFileName & DateTime.Now.ToString("yyyyMMdd") & ".tiff"

                    Try
                        If File.Exists(FileName) Then
                            File.Delete(FileName)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error eliminando la imagen en la ruta: " & FileName & " - Error: " & ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    ImageManager.Save(FileNamesCons, FileName, "", ImageManager.EnumFormat.Tiff, ImageManager.EnumCompression.Lzw, False, Program.AppPath & Program.TempPath, True)

                End If

            Catch ex As Exception
                MessageBox.Show("Se ha presentado un error al exportar la imagen TIFF: " & ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub ExportarExpedientes()
            If (Validar()) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Dim TotalesDataTable As DBImaging.SchemaProcess.CTA_Exportacion_TotalesDataTable
                Dim ProgressForm As New FormProgress
                FileNamesCons = New List(Of String)

                If LlenaExpedientesSeleccion() Then

                    Dim Folders As Integer = 0
                    Dim Files As Integer = 0
                    Dim Folios As Integer = 0
                    Dim Tamaño As Double = 0
                    'Por cada OT se crea una carpeta

                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    For Each row As DataRow In ViewExpedientes.ToTable.Rows
                        TotalesDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Expediente_Totales.DBExecute(CInt(row(0)))
                        If TotalesDataTable.Rows.Count > 0 Then
                            Folders += CInt(TotalesDataTable.Rows(0)("Folders"))
                            Files += CInt(TotalesDataTable.Rows(0)("Files"))
                            Folios += CInt(TotalesDataTable.Rows(0)("Folios"))
                            Tamaño += CDbl(TotalesDataTable.Rows(0)("Tamaño"))
                        End If
                    Next

                    dbmImaging.Connection_Close()

                    Dim Respuesta As DialogResult

                    Respuesta = MessageBox.Show("Se encontró : " & vbCrLf & _
                                                Folders & " Unidades Documentales, " & vbCrLf & _
                                                Files & " Documentos, " & vbCrLf & _
                                                Folios & " Folios con " & vbCrLf & _
                                                (Tamaño / 1024 / 1024).ToString("#,##0.00") & "MB de tamaño, " & vbCrLf & _
                                                "¿Desea Exportar esta información?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If Respuesta = DialogResult.Yes Then
                        Try
                            Me.Enabled = False

                            'Se crea Tabla de OTs
                            Dim OTs As New DataTable
                            OTs = ViewExpedientes.ToTable(True, "fk_OT")

                            Dim ExportacionDataSetXML As New OffLineViewer.Library.xsdOffLineData
                            Dim FolderDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                            Dim FileDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable
                            Dim FileDataDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                            Dim FileValidacionDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable

                            If OTs.Rows.Count > 0 Then
                                Dim Progreso As Integer = 0
                                ProgressForm.SetProceso("Exportar")
                                ProgressForm.SetAccion("Obteniendo imágenes...")
                                ProgressForm.SetProgreso(0)
                                ProgressForm.SetMaxValue(Files)
                                Application.DoEvents()

                                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                                dbmCore.Connection_Open(1)
                                dbmImaging.Connection_Open(1)

#If Not Debug Then
                                ProgressForm.Show()
#End If

                                For Each rowOt As DataRow In OTs.Rows

                                    Dim ExpedientesOT As New DataTable
                                    Dim ServidoresDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(CInt(rowOt(0)))
                                    Dim FolderDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                                    Dim FileDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable
                                    Dim FileDataDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                                    Dim FileValidacionDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable

                                    'Filtar expedientes por OT
                                    ViewExpedientes.RowFilter() = "fk_OT = " & rowOt(0).ToString
                                    ExpedientesOT = ViewExpedientes.ToTable()

                                    For Each rowExpediente As DataRow In ViewExpedientes.ToTable.Rows
                                        Dim tmpFolderDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Folders_Expediente.DBExecute(CInt(rowExpediente(0)))
                                        Dim tmpFileDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Files_Expediente.DBExecute(CInt(rowExpediente(0)))
                                        Dim tmpFileDataDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Data_Expediente.DBExecute(CInt(rowExpediente(0)))
                                        Dim tmpFileValidacionDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Validaciones_Expediente.DBExecute(CInt(rowExpediente(0)))

                                        TablaDesdeTemporal(FolderDataTable, tmpFolderDataTable)
                                        TablaDesdeTemporal(FileDataTable, tmpFileDataTable)
                                        TablaDesdeTemporal(FileDataDataTable, tmpFileDataDataTable)
                                        TablaDesdeTemporal(FileValidacionDataTable, tmpFileValidacionDataTable)

                                    Next

                                    Dim OutputFolder As String = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
                                    Dim FilesDataViewExpedientes As New DataView(FileDataTable)

                                    ' Crear el directorio de las imágenes
                                    If Not Directory.Exists(OutputFolder & "images") Then
                                        Directory.CreateDirectory(OutputFolder & "images")
                                    End If

                                    Dim Compresion As ImageManager.EnumCompression

                                    If (Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                                        Compresion = ImageManager.EnumCompression.Ccitt4
                                    Else
                                        Compresion = ImageManager.EnumCompression.Lzw
                                    End If

                                    For Each RowServidor In ServidoresDataTable
                                        Dim manager As FileProviderManager = Nothing
                                    Next

                                    For Each RowServidor In ServidoresDataTable
                                        Dim manager As FileProviderManager = Nothing

                                        Try
                                            Dim servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(CInt(rowOt(0)))(0).ToCTA_ServidorSimpleType()
                                            Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                                            manager = New FileProviderManager(servidor, centro, dbmImaging, Program.Sesion.Usuario.id)
                                            manager.Connect()

                                            Dim FileFolderName = "images\" & CInt(rowOt(0)).ToString("0000000000") & "\"
                                            FolderNameOutput = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\" & FileFolderName

                                            If (Not Directory.Exists(OutputFolder & FileFolderName)) Then
                                                Directory.CreateDirectory(OutputFolder & FileFolderName)
                                            End If

                                            Dim GruposD As List(Of Object) = Nothing
                                            GruposD = (From a In FileDataTable Group a By groupDt = a.Field(Of Integer)("fk_Grupo") Into Group Select Group.Select(Function(x) x("fk_Grupo")).First()).ToList()

                                            For Each grupo As Integer In GruposD
                                                Dim FilesbyGroup = FileDataTable.Select("fk_Grupo = " + grupo.ToString()).CopyToDataTable

                                                If grupo = 0 Then
                                                    Dim FilesbyGroupDataView As New DataView(FilesbyGroup)

                                                    ' Obtener los Files a transferir   
                                                    FilesbyGroupDataView.RowFilter = "fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor
                                                    For Each ItemFile As DataRowView In FilesbyGroupDataView

                                                        If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                                                        ' Enviar el archivo
                                                        ExportarImagen(manager, ItemFile, Compresion, OutputFolder & FileFolderName)

                                                        Progreso += 1
                                                        ProgressForm.SetProgreso(Progreso)
                                                        Application.DoEvents()
                                                    Next
                                                Else
                                                    Dim Expedientes As List(Of Object) = Nothing
                                                    Expedientes = (From a In FilesbyGroup Group a By groupDt = a.Field(Of Long)("fk_Expediente") Into Group Select Group.Select(Function(x) x("fk_Expediente")).First()).ToList()

                                                    For Each Expediente As Long In Expedientes
                                                        Dim FilesExpedientesbyGroup = FilesbyGroup.Select("fk_Grupo = " + grupo.ToString() + "AND fk_Expediente = " + Expediente.ToString()).CopyToDataTable
                                                        Dim FilesExpedientesbyGroupDataView As New DataView(FilesExpedientesbyGroup)

                                                        ' Obtener los Files a transferir   
                                                        FilesExpedientesbyGroupDataView.RowFilter = "fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor

                                                        If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                                                        ' Enviar el archivo
                                                        ExportarImagenAgrupada(manager, FilesExpedientesbyGroupDataView, grupo, Expediente, 1, Compresion, OutputFolder & FileFolderName)

                                                        Progreso += 1
                                                        ProgressForm.SetProgreso(Progreso)
                                                        Application.DoEvents()
                                                    Next

                                                End If
                                            Next
                                            manager.Disconnect()
                                        Catch ex As Exception
                                            If (manager IsNot Nothing) Then manager.Disconnect()
                                            Throw
                                        End Try
                                    Next

                                    'Aqui debe ir codigo que genera la ruta

                                Next
                                MessageBox.Show("La información se exportó con éxito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            Me.Enabled = True
                            'Finalizar conexiones
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()

                            BorrarTemporal()

                            'Ocultar barras de progreso
                            ProgressForm.Close()
                        End Try
                    End If
                End If
            Else
                MessageBox.Show("No se seleccionaron registros para exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            BorrarTemporal()

        End Sub

        Private Sub TablaDesdeTemporal(ByVal tabla As DataTable, ByVal temporal As DataTable)
            For Each drow As DataRow In temporal.Rows

                Dim newRow As DataRow = tabla.NewRow()

                For Each col As DataColumn In temporal.Columns
                    newRow(col.Ordinal) = drow(col.Ordinal)
                Next

                tabla.Rows.Add(newRow)
            Next
        End Sub

        Private Function LlenaExpedientesSeleccion() As Boolean

            'Se crea la tabla que contendrá los expedientes seleccionados en la grilla
            Dim ExpedientesSeleccion_ As New DataTable

            For Each col As DataGridViewColumn In ExpedientesDataGridView.Columns
                ExpedientesSeleccion_.Columns.Add(col.DataPropertyName)
            Next

            For Each row As DataGridViewRow In ExpedientesDataGridView.Rows
                If CBool(row.Cells("Exportar").Value) Then 'Expedientes seleccionados
                    Dim newRow As DataRow = ExpedientesSeleccion_.NewRow()

                    For Each col As DataGridViewColumn In ExpedientesDataGridView.Columns
                        newRow(col.Index) = row.Cells(col.Index).Value
                    Next

                    ExpedientesSeleccion_.Rows.Add(newRow)
                End If
            Next

            ExpedientesSeleccion = ExpedientesSeleccion_

            If ExpedientesSeleccion_.Rows.Count > 0 Then
                ViewExpedientes.RowFilter = String.Empty
                ViewExpedientes = New DataView(ExpedientesSeleccion_)
                Return True
            End If

            Return False

        End Function

        Private Sub ExportarImagen(nManager As FileProviderManager, ByVal ItemFile As DataRowView, nCompresion As ImageManager.EnumCompression, nFileFolderName As String)
            Dim Folios = nManager.GetFolios(CLng(ItemFile.Item("fk_Expediente")), CShort(ItemFile.Item("fk_Folder")), CShort(ItemFile.Item("fk_File")), CShort(ItemFile.Item("id_Version")))

            Dim FileNames As New List(Of String)
            Dim FileName As String = Nothing
            Dim FileNameAux As String = Nothing
            Dim ExtensionAux As String = String.Empty

            Try
                For folio As Short = 1 To Folios
                    Dim Imagen() As Byte = Nothing
                    Dim Thumbnail() As Byte = Nothing

                    nManager.GetFolio(CLng(ItemFile.Item("fk_Expediente")), CShort(ItemFile.Item("fk_Folder")), CShort(ItemFile.Item("fk_File")), CShort(ItemFile.Item("id_Version")), folio, Imagen, Thumbnail)

                    FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    FileNames.Add(FileName)

                    FileNamesCons.Add(FileName)

                    Using fs = New FileStream(FileName, FileMode.Create)
                        fs.Write(Imagen, 0, Imagen.Length)
                        fs.Close()
                    End Using
                Next

                If Not (Program.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF) Then

                    If (FileNames.Count > 0) Then
                        Dim Format As ImageManager.EnumFormat

                        Select Case Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
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

                        Dim Valido As Boolean = True
                        Dim MsgError As String = ""

                        If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Renombramiento_Imagen_Exportacion) Then
                            FileNameAux = EventManager.Nombre_Imagen_Exportar(CLng(ItemFile.Item("fk_Expediente")), CShort(ItemFile.Item("fk_Folder")), CShort(ItemFile.Item("fk_File")), CInt(ItemFile.Item("fk_Documento")), CInt(ItemFile.Item("fk_Grupo")), Valido, MsgError)

                            If ((Valido = True) And (FileNameAux = String.Empty)) Then
                                FileNameAux = Nombre_Imagen_Exportar(CLng(ItemFile.Item("fk_Expediente")), CShort(ItemFile.Item("fk_Folder")), CShort(ItemFile.Item("fk_File")), CInt(ItemFile.Item("fk_Documento")), CInt(ItemFile.Item("fk_Grupo")), Valido, MsgError)
                            End If
                        End If

                        ExtensionAux = IIf(formatoAux = ImageManager.EnumFormat.Pdf, ".pdf", Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida).ToString

                        If ((Valido = True) And (FileNameAux = String.Empty)) Then
                            FileName = nFileFolderName & ItemFile.Item("File_Unique_Identifier").ToString() & "_0001" & ExtensionAux
                        ElseIf ((Valido = True) And (FileNameAux <> String.Empty)) Then
                            ExtensionAux = Convert.ToString(IIf(ExtensionAux Is String.Empty, Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, ExtensionAux))
                            FileName = nFileFolderName & FileNameAux & ExtensionAux
                        ElseIf Valido = False Then
                            Throw New Exception(MsgError)
                        End If

                        '-------------------------------------------------------------------------
                        ImageManager.Save(FileNames, FileName, "", formatoAux, CompresionAux, False, Program.AppPath & Program.TempPath, True)
                        '-------------------------------------------------------------------------
                    End If
                End If
            Catch ex As Exception
                DMB.DesktopMessageShow("Exportar imagen", ex)
            End Try
        End Sub

        Public Function GetFileName() As String
            'Return _EventManager.Nombre_Imagen_Exportar
            Return ""
        End Function

        Private Sub BorrarTemporal()
            Dim objDirectoryInfo = New DirectoryInfo(Program.AppPath & Program.TempPath)
            Dim fileInfoArray As FileInfo() = objDirectoryInfo.GetFiles()
            Dim objFileInfo As FileInfo
            For Each objFileInfo In fileInfoArray
                Try
                    objFileInfo.Delete()
                Catch
                End Try
            Next objFileInfo
        End Sub

        Private Sub MostrarDatagrid()
            If (Me.CheckBoxExpedientes.Checked) Then
                OTLabel.Text = "Expedientes"
                ExpedientesDataGridView.Visible = True
                OTDataGridView.Visible = False
            Else
                OTLabel.Text = "OTs"
                ExpedientesDataGridView.Visible = False
                OTDataGridView.Visible = True
            End If

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

        Private Sub ExportarImagenAgrupada(nManager As FileProviderManager, ByVal nFilesExpedientesbyGroupDataView As DataView, ngrupo As Integer, nfk_Expediente As Long, nfk_Folder As Short, nCompresion As ImageManager.EnumCompression, nFileFolderName As String)
            Dim FileNames As New List(Of String)
            Dim FileName As String = Nothing
            Dim FileNameAux As String = Nothing
            Dim ExtensionAux As String = String.Empty

            For Each itemfile As DataRowView In nFilesExpedientesbyGroupDataView

                Dim Folios = nManager.GetFolios(CLng(itemfile.Item("fk_Expediente")), CShort(itemfile.Item("fk_Folder")), CShort(itemfile.Item("fk_File")), CShort(itemfile.Item("id_Version")))

                For folio As Short = 1 To Folios
                    Dim Imagen() As Byte = Nothing
                    Dim Thumbnail() As Byte = Nothing

                    nManager.GetFolio(CLng(itemfile.Item("fk_Expediente")), CShort(itemfile.Item("fk_Folder")), CShort(itemfile.Item("fk_File")), CShort(itemfile.Item("id_Version")), folio, Imagen, Thumbnail)


                    FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    FileNames.Add(FileName)

                    Using fs = New FileStream(FileName, FileMode.Create)
                        fs.Write(Imagen, 0, Imagen.Length)
                        fs.Close()
                    End Using
                Next
            Next

            If (FileNames.Count > 0) Then
                Dim Format As ImageManager.EnumFormat

                Select Case Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
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

                Dim Valido As Boolean = True
                Dim MsgError As String = ""

                If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Renombramiento_Imagen_Exportacion) Then
                    FileNameAux = EventManager.Nombre_Imagen_Agrupada_Exportar(nfk_Expediente, nfk_Folder, ngrupo, Valido, MsgError)

                    If ((Valido = True) And (FileNameAux = String.Empty)) Then
                        FileNameAux = Nombre_Imagen_Agrupada_Exportar(nfk_Expediente, nfk_Folder, ngrupo, Valido, MsgError)
                    End If
                End If

                ExtensionAux = IIf(formatoAux = ImageManager.EnumFormat.Pdf, ".pdf", Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida).ToString

                If ((Valido = True) And (FileNameAux = String.Empty)) Then
                    FileName = nFileFolderName & Guid.NewGuid().ToString() & "_0001" & ExtensionAux
                ElseIf ((Valido = True) And (FileNameAux <> String.Empty)) Then
                    ExtensionAux = Convert.ToString(IIf(ExtensionAux Is String.Empty, Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, ExtensionAux))
                    FileName = nFileFolderName & FileNameAux & ExtensionAux
                ElseIf Valido = False Then
                    Throw New Exception(MsgError)
                End If

                '-------------------------------------------------------------------------
                ImageManager.Save(FileNames, FileName, "", formatoAux, CompresionAux, False, Program.AppPath & Program.TempPath, True)
                '-------------------------------------------------------------------------
            End If
        End Sub
#End Region

#Region " Funciones "
        Private Function Validar() As Boolean

            If Not validarFechaProceso() Then
                Return False
            End If

            If (Not Directory.Exists(CarpetaSalidaTextBox.Text)) Then
                MessageBox.Show("El directorio no existe, Seleccione un directorio existente", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()

            ElseIf (Directory.GetDirectories(CarpetaSalidaTextBox.Text).Length > 0 Or Directory.GetFiles(CarpetaSalidaTextBox.Text).Length > 0) Then
                MessageBox.Show("La carpeta debe estar vacia para exportar los datos", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()

            ElseIf (Me.OTDataGridView.SelectedRows.Count = 0) And Not Me.CheckBoxExpedientes.Checked Then
                MessageBox.Show("Se debe seleccionar una OT", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.OTDataGridView.Focus()

            ElseIf Not Me.CheckBoxExpedientes.Checked Then
                Dim OTRow = CType(CType(Me.OTDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Exportacion_OTRow)

                ' Validar si ya fue exportado
                If (OTRow.Exportado) Then
                    Dim Respuesta = MessageBox.Show("La OT: " & OTRow.id_OT & ", ya fue exportada, ¿desea volverla a exportar?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If (Respuesta = DialogResult.No) Then Return False
                End If

                ' Validar si la OT se puede exportar
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Dim Resultado = dbmImaging.SchemaProcess.PA_Validar_Cargado_Completo.DBExecute(OTRow.id_OT)

                    If (Not Resultado) Then
                        MessageBox.Show("La OT no ha sido totalmente procesada y no se puede exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.OTDataGridView.Focus()
                    Else
                        Return True
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try

            Else
                'Validaciones Exportación Expedientes

                If (Me.ExpedientesDataGridView.Rows.Count = 0) Then
                    MessageBox.Show("No se han seleccionado Expedientes para exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.ExpedientesDataGridView.Focus()
                    Return False

                Else
                    For Each row As DataGridViewRow In ExpedientesDataGridView.Rows
                        If CBool(row.Cells("Exportar").Value) Then
                            Return True
                        End If
                    Next

                    MessageBox.Show("No se han seleccionado Expedientes para exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.ExpedientesDataGridView.Focus()
                    Return False

                End If

            End If

            Return False
        End Function

        Private Function validarFechaProceso() As Boolean
            Dim FechaInicial = New DateTime(FechaProcesoDateTimePicker.Value.Year, FechaProcesoDateTimePicker.Value.Month, FechaProcesoDateTimePicker.Value.Day)
            Dim FechaFinal = New DateTime(FechaProcesoFinalDateTimePicker.Value.Year, FechaProcesoFinalDateTimePicker.Value.Month, FechaProcesoFinalDateTimePicker.Value.Day)

            If (FechaInicial > FechaFinal) Then
                MessageBox.Show("La fecha de Proceso final no puede ser inferior a la fecha de Proceso inicial", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

                Return False

            End If

            Return True

        End Function

        Private Function getFechaInicio() As Integer
            Return CInt(Me.FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"))
        End Function

        Private Function getFechaFinal() As Integer
            Return CInt(Me.FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd"))
        End Function

        Private Function getOTs() As DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Return dbmImaging.SchemaProcess.PA_Exportacion_OT.DBExecute(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _
                                                                                Program.ImagingGlobal.Entidad, _
                                                                                Program.ImagingGlobal.Proyecto, _
                                                                                getFechaInicio(), _
                                                                                getFechaFinal())

                '    Return dbmImaging.SchemaProcess.CTA_Exportacion_OT.DBFindByfk_Entidad_Procesamientofk_Entidadfk_Proyectofk_fecha_proceso(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _
                '                                                                                                                            Program.ImagingGlobal.Entidad, _
                '                                                                                                                            Program.ImagingGlobal.Proyecto, _
                '                                                                                                                            getFechaInicio())
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return New DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable()
        End Function

        Private Function getExpedientes() As DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Return dbmImaging.SchemaProcess.PA_Exportacion_Expediente.DBExecute(CDate(Me.FechaProcesoDateTimePicker.Value), CDate(Me.FechaProcesoFinalDateTimePicker.Value), Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return New DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable()
        End Function

        Private Function Nombre_Imagen_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nFk_Documento As Integer, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String
            Dim Nombre_Imagen As String = String.Empty

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Nombre_Imagen = dbmImaging.SchemaProcess.PA_GetNombreArchivo.DBExecute(nidExpediente, nidFolder, nidFile, nFk_Documento, nGrupo)

                If Nombre_Imagen = String.Empty Then
                    nValido = False
                    nMsgError = "No se encontró nombre de imagen para el expediente: " & nidExpediente.ToString() & ", fk_Documento: " & nFk_Documento.ToString()
                End If
            Catch ex As Exception
                nValido = False
                Throw New Exception("Error al generar la Imagen del Expediente: (" + nidExpediente.ToString + ") Se genero el error:" + ex.Message, ex.InnerException)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try


            Return Nombre_Imagen
        End Function

        Private Function Nombre_Imagen_Agrupada_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal ngrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String
            Dim Nombre_Imagen As String = String.Empty

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Nombre_Imagen = dbmImaging.SchemaProcess.PA_GetNombreArchivo.DBExecute(nidExpediente, nidFolder, DBNull.Value, DBNull.Value, ngrupo)

                If Nombre_Imagen = String.Empty Then
                    nValido = False
                    nMsgError = "No se encontró nombre de imagen para el expediente: " & nidExpediente.ToString() & ", fk_Documento: " & 0.ToString()
                End If
            Catch ex As Exception
                nValido = False
                Throw New Exception("Error al generar la Imagen del Expediente: (" + nidExpediente.ToString + ") Se genero el error:" + ex.Message, ex.InnerException)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try


            Return Nombre_Imagen
        End Function
#End Region

    End Class

    Public Class ImagePDFManagerDomain
        Inherits MarshalByRefObject

        Public Sub Save(ByVal nInputFileNames As List(Of String), ByVal nOutputFileName As String, ByVal nSuffixFormat As String, ByVal nFormat As ImageManager.EnumFormat, ByVal nCompression As ImageManager.EnumCompression, ByVal nSinglePage As Boolean, ByVal nTempPath As String, ByVal nIsInputSingle As Boolean)
            ImageManager.Save(nInputFileNames, nOutputFileName, nSuffixFormat, nFormat, nCompression, nSinglePage, nTempPath, nIsInputSingle)
        End Sub
    End Class

End Namespace