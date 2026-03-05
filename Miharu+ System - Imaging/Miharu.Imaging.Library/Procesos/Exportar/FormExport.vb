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
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Imaging
Imports Newtonsoft.Json
Imports Miharu.Imaging.Indexer
Imports Miharu.Imaging.Indexer.Controller.Indexer.Capturas.OCR
Imports Slyg.Tools.Imaging.OCR
Imports Slyg.Tools.Imaging.OCR.Models

Namespace Procesos.Exportar

    Public Class FormExport
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

#Region "Constructor"

        Public Sub New()

            InitializeComponent()

            Usa_Exportacion_PDF = Program.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF

            If Usa_Exportacion_PDF Then
                OCRCheckBox.Visible = True
            End If
        End Sub

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

            'Usa_Exportacion_PDF = Program.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF
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

                            dbmImaging.Connection_Open(1)
                            dbmCore.Connection_Open(1)

                            Dim FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                            Dim FileDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable
                            Dim FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                            Dim FileValidacionDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable

                            Dim ServidoresDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(OTRow.id_OT)
                            If (CheckBoxExpedientesValidos.Visible) Then
                                FolderDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Folders.DBExecute(OTRow.id_OT, CheckBoxExpedientesValidos.Checked)
                                FileDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Files.DBExecute(OTRow.id_OT, CheckBoxExpedientesValidos.Checked)
                                FileDataDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Data.DBExecute(OTRow.id_OT, CheckBoxExpedientesValidos.Checked)
                                FileValidacionDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Validaciones.DBExecute(OTRow.id_OT, CheckBoxExpedientesValidos.Checked)
                            Else
                                FolderDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Folders.DBExecute(OTRow.id_OT, Nothing)
                                FileDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Files.DBExecute(OTRow.id_OT, Nothing)
                                FileDataDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Data.DBExecute(OTRow.id_OT, Nothing)
                                FileValidacionDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Validaciones.DBExecute(OTRow.id_OT, Nothing)
                            End If

                            Dim OutputFolder As String = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
                            Dim FilesDataView As New DataView(FileDataTable)
                            Dim Progreso As Integer = 0

#If Not Debug Then
                            ProgressForm.Show()
#End If

                            ProgressForm.SetProceso("Exportar")
                            ProgressForm.SetAccion("Obteniendo imágenes...")
                            ProgressForm.SetProgreso(0)
                            ProgressForm.SetMaxValue(TotalesDataTable(0).Folios)

                            Application.DoEvents()

                            ' Crear el directorio de las imágenes
                            Directory.CreateDirectory(OutputFolder & "images")

                            Dim Compresion As ImageManager.EnumCompression

                            If (Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                                Compresion = ImageManager.EnumCompression.Ccitt4
                            Else
                                Compresion = ImageManager.EnumCompression.Lzw
                            End If

                            For Each RowServidor In ServidoresDataTable
                                Dim manager As FileProviderManager = Nothing

                                Try
                                    Dim servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(OTRow.id_OT)(0).ToCTA_ServidorSimpleType()
                                    Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                                    manager = New FileProviderManager(servidor, centro, dbmImaging, Program.Sesion.Usuario.id)
                                    manager.Connect()

                                    Dim FileFolderName = "images\" & OTRow.id_OT.ToString("0000000000") & "\"
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
                                                ExportarImagen(manager, ItemFile, Compresion, OutputFolder & FileFolderName, FileFolderName)

                                                For Each Item As DBImaging.SchemaProcess.CTA_Exportacion_FilesRow In FileDataTable
                                                    If Item.fk_Expediente = CLng(ItemFile.Item("fk_Expediente")) And Item.fk_Folder = CShort(ItemFile.Item("fk_Folder")) And Item.fk_File = CShort(ItemFile.Item("fk_File")) Then
                                                        Item.Nombre_Imagen_File = CStr(ItemFile.Item("Nombre_Imagen_File"))
                                                    End If
                                                Next

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
                                                ExportarImagenAgrupada(manager, FilesExpedientesbyGroupDataView, grupo, Expediente, 1, Compresion, OutputFolder & FileFolderName, FileFolderName)

                                                For Each Item As DBImaging.SchemaProcess.CTA_Exportacion_FilesRow In FileDataTable
                                                    If Item.fk_Expediente = Expediente And Item.fk_Grupo = grupo Then
                                                        Item.Nombre_Imagen_File = CStr(FilesExpedientesbyGroupDataView(0).Item("Nombre_Imagen_File"))
                                                    End If
                                                Next

                                                Progreso += 1
                                                ProgressForm.SetProgreso(Progreso)
                                                Application.DoEvents()
                                            Next

                                        End If
                                    Next
                                Catch ex As Exception
                                    If (manager IsNot Nothing) Then manager.Disconnect()
                                    Throw (ex)
                                End Try
                            Next

                            '------------  Si proyecto tiene configurado Exportar_Unico_Archivo_TIFF  ------------------
                            If (CBool(Program.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF)) Then
                                ExportAllFillesInTiff(Compresion, FolderNameOutput)
                            End If
                            '--------------------------------

                            If (Me.VisorRadioButton.Checked) Then
                                GenerarVisor(dbmCore, dbmImaging, OTRow.id_OT, OutputFolder, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable)
                            ElseIf (Me.XMLRadioButton.Checked) Then
                                GenerarXML(dbmCore, dbmImaging, OTRow.id_OT, OutputFolder, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable)
                            Else
                                GenerarTXT(OutputFolder, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable)
                            End If

                            ' Crear la exportación

                            Dim ExportacionType = New DBImaging.SchemaProcess.TBL_ExportacionType()

                            If (Me.VisorRadioButton.Checked) Then ExportacionType.fk_Exportacion_Tipo = DBImaging.TipoExportacionEnum.VISOR
                            If (Me.XMLRadioButton.Checked) Then ExportacionType.fk_Exportacion_Tipo = DBImaging.TipoExportacionEnum.XML
                            If (Me.TXTRadioButton.Checked) Then ExportacionType.fk_Exportacion_Tipo = DBImaging.TipoExportacionEnum.TEXTO_PLANO
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

                            Dim ExportacionDataSetXML As New OffLineViewer.Library.xsdOfflineData
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

                                        If Me.TXTRadioButton.Checked Then
                                            TablaDesdeTemporal(FolderDataTableTXT, tmpFolderDataTable)
                                            TablaDesdeTemporal(FileDataTableTXT, tmpFileDataTable)
                                            TablaDesdeTemporal(FileDataDataTableTXT, tmpFileDataDataTable)
                                            TablaDesdeTemporal(FileValidacionDataTableTXT, tmpFileValidacionDataTable)
                                        End If

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

                                            Dim FileFolderName As String = "images\"

                                            FileFolderName = FileFolderName & CInt(rowOt(0)).ToString("0000000000") & "\"

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
                                                        ExportarImagen(manager, ItemFile, Compresion, OutputFolder & FileFolderName, FileFolderName)

                                                        For Each Item As DBImaging.SchemaProcess.CTA_Exportacion_FilesRow In FileDataTable
                                                            If Item.fk_Expediente = CLng(ItemFile.Item("fk_Expediente")) And Item.fk_Folder = CShort(ItemFile.Item("fk_Folder")) And Item.fk_File = CShort(ItemFile.Item("fk_File")) Then
                                                                Item.Nombre_Imagen_File = CStr(ItemFile.Item("Nombre_Imagen_File"))
                                                            End If
                                                        Next

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
                                                        ExportarImagenAgrupada(manager, FilesExpedientesbyGroupDataView, grupo, Expediente, 1, Compresion, OutputFolder & FileFolderName, FileFolderName)

                                                        For Each Item As DBImaging.SchemaProcess.CTA_Exportacion_FilesRow In FileDataTable
                                                            If Item.fk_Expediente = Expediente And Item.fk_Grupo = grupo Then
                                                                Item.Nombre_Imagen_File = CStr(FilesExpedientesbyGroupDataView(0).Item("Nombre_Imagen_File"))
                                                            End If
                                                        Next

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

                                    '------------  Si proyecto tiene configurado Exportar_Unico_Archivo_TIFF  ------------------
                                    If (CBool(Program.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF)) Then
                                        ExportAllFillesInTiff(Compresion, FolderNameOutput)
                                    End If
                                    '--------------------------------

                                    If (Me.VisorRadioButton.Checked) Then
                                        GenerarVisor(dbmCore, dbmImaging, CInt(rowOt(0)), OutputFolder, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable)
                                    ElseIf (Me.XMLRadioButton.Checked) Then
                                        Dim generar As Boolean = False
                                        If rowOt.Equals(OTs.Rows(OTs.Rows.Count - 1)) Then
                                            generar = True
                                        End If
                                        GenerarXMLExpedientes(generar, dbmCore, dbmImaging, CInt(rowOt(0)), OutputFolder, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable, ExportacionDataSetXML)
                                    ElseIf rowOt.Equals(OTs.Rows(OTs.Rows.Count - 1)) Then

                                        GenerarTXT(OutputFolder, FolderDataTableTXT, FileDataTableTXT, FileDataDataTableTXT, FileValidacionDataTableTXT)

                                    End If

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

        Private Sub ExportarImagen(nManager As FileProviderManager, ByVal ItemFile As DataRowView, nCompresion As ImageManager.EnumCompression, nFileFolderName As String, nFolderName As String)
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
                            FileNameAux = ItemFile.Item("File_Unique_Identifier").ToString() & "_0001"
                            FileName = nFileFolderName & FileNameAux & ExtensionAux
                        ElseIf ((Valido = True) And (FileNameAux <> String.Empty)) Then
                            ExtensionAux = Convert.ToString(IIf(ExtensionAux Is String.Empty, Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, ExtensionAux))
                            FileName = nFileFolderName & FileNameAux & ExtensionAux
                        ElseIf Valido = False Then
                            Throw New Exception(MsgError)
                        End If

                        If OCRCheckBox.Visible AndAlso OCRCheckBox.Checked Then
                            CreatePDFOCR(FileNames, FileName, Program.AppPath & Program.TempPath)
                        Else
                            ImageManager.Save(FileNames, FileName, "", formatoAux, CompresionAux, False, Program.AppPath & Program.TempPath, True)
                        End If

                        ItemFile.Item("Nombre_Imagen_File") = nFolderName & FileNameAux & ExtensionAux
                    End If
                End If
            Catch ex As Exception
                DMB.DesktopMessageShow("Exportar imagen", ex)
            End Try
        End Sub

        Private Sub CreatePDFOCR(_FileNames As List(Of String), nOutputFileName As String, nTempPath As String)

            If Not Directory.Exists(Path.GetDirectoryName(nOutputFileName)) Then
                Directory.CreateDirectory(Path.GetDirectoryName(nOutputFileName))
            End If

            If Not Directory.Exists(nTempPath) Then
                Directory.CreateDirectory(nTempPath)
            End If

            Dim listWordsExtractionOCR As List(Of ImageTextExtractionOCR) = New List(Of ImageTextExtractionOCR)()

            For i As Integer = 0 To _FileNames.Count - 1

                Dim fileName As String = _FileNames(i).ToString()

                Dim imageTiff As Image = ConvertImageToTiff(fileName)
                If imageTiff Is Nothing Then
                    Continue For
                End If

                Dim ImageTIFFBytes As Byte() = ImageToByteArray(imageTiff)
                Dim grayscaleImageBytes As Byte() = ConvertBytesToGrayscale(ImageTIFFBytes)

                Dim jsonPayload As String = SerializeImageBytes(grayscaleImageBytes)
                Dim urlTableOCR As String = Program.DesktopGlobal.ConnectionURLStrings.ValuePageOCR
                Dim responseText As String = SendDataPostRequest(jsonPayload, urlTableOCR)
                Dim wordsExtractionOCR As ImageTextExtractionOCR = JsonConvert.DeserializeObject(Of ImageTextExtractionOCR)(responseText)
                listWordsExtractionOCR.Add(wordsExtractionOCR)

                imageTiff.Dispose()
            Next

            ImageManagerOCR.SavePDF(_FileNames, listWordsExtractionOCR, nOutputFileName)
        End Sub

        ''' <summary>
        ''' Convierte una imagen en formato JPG (u otros formatos compatibles) a TIFF y la devuelve como un objeto Image.
        ''' </summary>
        ''' <param name="fileName">Ruta del archivo de la imagen de entrada.</param>
        ''' <returns>Devuelve un objeto Image que representa la imagen convertida en formato TIFF, o Nothing si ocurre un error.</returns>
        Private Function ConvertImageToTiff(fileName As String) As Image

            Using fileStream As New FileStream(fileName, FileMode.Open, FileAccess.Read)

                Dim originalImage As Image = System.Drawing.Image.FromStream(fileStream)

                ' Convertir la imagen a TIFF como antes
                Dim fiBitmap = FreeImageAPI.FreeImage.CreateFromBitmap(DirectCast(originalImage, System.Drawing.Bitmap))
                Dim tiffBitmap = FreeImageAPI.FreeImage.ConvertToType(fiBitmap, FreeImageAPI.FREE_IMAGE_TYPE.FIT_BITMAP, False)

                If fiBitmap.IsNull Then
                    Return Nothing
                End If

                Dim imageTIFF As Image = FreeImageAPI.FreeImage.GetBitmap(tiffBitmap)

                If tiffBitmap.IsNull Then
                    Return Nothing
                End If

                ' Liberar recursos
                FreeImageAPI.FreeImage.Unload(fiBitmap)
                FreeImageAPI.FreeImage.Unload(tiffBitmap)

                Return imageTIFF
            End Using
        End Function

        ' Función para convertir System.Drawing.Image a un arreglo de bytes
        Private Function ImageToByteArray(image As Image) As Byte()
            ' Usar un MemoryStream para almacenar la imagen en formato TIFF
            Using ms As New MemoryStream()
                ' Guardar la imagen en el MemoryStream en formato TIFF
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Tiff)
                ' Retornar los datos del MemoryStream como un arreglo de bytes
                Return ms.ToArray()
            End Using
        End Function

        ''' <summary>
        ''' Serializa un array de bytes de una imagen en formato JSON.
        ''' </summary>
        ''' <param name="imageBytes">Los bytes de la imagen a serializar.</param>
        ''' <returns>Una cadena JSON que representa los bytes de la imagen.</returns>
        Private Function SerializeImageBytes(imageBytes As Byte()) As String

            Dim imageBytesData As New OCRDataStructures.DataStructureImageBytes()
            imageBytesData.ImageBytes = imageBytes

            Return JsonConvert.SerializeObject(imageBytesData)
        End Function

        ''' Agregar en carpeta Utilities, clase HttpUtility.vb
        ''' <summary>
        ''' Realiza una solicitud POST a una URL especificada con un JSON payload y devuelve la respuesta como una cadena.
        ''' </summary>
        ''' <param name="jsonPayload">El contenido JSON que se incluirá en la solicitud POST.</param>
        ''' <param name="url">La URL a la que se realizará la solicitud POST.</param>
        ''' <returns>La respuesta de la solicitud POST como una cadena o una cadena vacía si no se pudo obtener una respuesta.</returns>
        Private Function SendDataPostRequest(jsonPayload As String, url As String) As String

            ' Crea la solicitud HTTP con la URL especificada.
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"

            ' Agrega la cabecera Accept-Encoding para indicar la aceptación de compresión.
            'request.Headers.Add(HttpRequestHeader.Accept, "*/*")
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br")

            ' Escribe el JSON en el cuerpo de la solicitud.
            Using streamWriter As New StreamWriter(request.GetRequestStream())
                streamWriter.Write(jsonPayload)
            End Using

            ' Obtiene y devuelve la respuesta.
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Using reader As New StreamReader(response.GetResponseStream())
                        Return reader.ReadToEnd()
                    End Using
                End If
            End Using

            ' Si no se pudo obtener una respuesta, se devuelve una cadena vacía.
            Return String.Empty
        End Function


        ''' <summary>
        ''' Convierte el arreglo de bytes de una imagen a escala de grises y retorna los bytes resultantes.
        ''' </summary>
        ''' <param name="imageBytes">Arreglo de bytes que representa la imagen original.</param>
        ''' <returns>Un arreglo de bytes que representa la imagen en escala de grises.</returns>
        Private Function ConvertBytesToGrayscale(imageBytes As Byte()) As Byte()
            Try
                Using ms As New MemoryStream(imageBytes)
                    Dim originalImage As Image = CType(FreeImageAPI.FreeImageBitmap.FromStream(ms), Drawing.Image)  ' Usa FreeImageAPI para obtener la imagen original
                    Return ConvertImageToGrayscaleBytes(originalImage)
                End Using
            Catch ex As Exception
                Throw New Exception("Error al convertir a imagen de escala de grises. Detalles: " & ex.ToString, ex)
            End Try
        End Function

        ''' <summary>
        ''' Convierte la imagen proporcionada a escala de grises y retorna los bytes resultantes.
        ''' </summary>
        ''' <param name="originalImage">Imagen original a convertir.</param>
        ''' <returns>Un arreglo de bytes que representa la imagen en escala de grises.</returns>
        Private Function ConvertImageToGrayscaleBytes(originalImage As Image) As Byte()

            ' Crea un nuevo objeto Bitmap en escala de grises
            Using grayscaleImage As New Bitmap(originalImage.Width, originalImage.Height)

                ' Configura el modo de dibujo para mejorar la calidad
                Using g As Graphics = Graphics.FromImage(grayscaleImage)
                    g.CompositingMode = Drawing2D.CompositingMode.SourceCopy                 ' Establece el modo de composición para copiar directamente los píxeles de origen
                    g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality          ' Establece la calidad de composición en alta para mejorar la calidad general
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic     ' Establece el modo de interpolación en alta calidad bicúbica para reducir artefactos
                    g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality                    ' Establece el modo de suavizado para mejorar la calidad de los bordes
                    g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality                ' Establece el modo de compensación de píxeles para obtener resultados de alta calidad

                    ' Configura el modo de dibujo para convertir a escala de grises
                    Dim attributes As New ImageAttributes()
                    attributes.SetColorMatrix(New ColorMatrix(New Single()() {
                        New Single() {0.298999995F, 0.298999995F, 0.298999995F, 0, 0},
                        New Single() {0.587000012F, 0.587000012F, 0.587000012F, 0, 0},
                        New Single() {0.114F, 0.114F, 0.114F, 0, 0},
                        New Single() {0, 0, 0, 1, 0},
                        New Single() {0, 0, 0, 0, 1}
                    }))

                    ' Dibuja la imagen original en el nuevo objeto Bitmap en escala de grises
                    g.DrawImage(originalImage, New Rectangle(0, 0, originalImage.Width, originalImage.Height), 0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, attributes)
                End Using

                ' Convierte la imagen en escala de grises a un arreglo de bytes
                Using msGrayscale As New MemoryStream()
                    grayscaleImage.Save(msGrayscale, ImageFormat.Tiff) ' cambiar el formato según necesidades
                    Return msGrayscale.ToArray()
                End Using

            End Using
        End Function

        ''' <summary>
        ''' Obtiene el codificador de imágenes asociado a un tipo MIME específico.
        ''' </summary>
        ''' <param name="mimeType"></param>
        ''' <returns>
        ''' Un objeto ImageCodecInfo que representa el codificador de imágenes correspondiente o Nothing si no se encuentra ninguno.
        ''' </returns>
        ''' <remarks>
        ''' Esta función busca entre los codificadores disponibles y devuelve el que coincide con el tipo MIME especificado.
        ''' Si no se encuentra un codificador correspondiente, devuelve Nothing.
        ''' </remarks>
        Private Function GetEncoderInfo(ByVal mimeType As String) As ImageCodecInfo

            Dim encoders() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()  ' Obtener la lista de codificadores de imágenes disponibles

            For Each encoder As ImageCodecInfo In encoders                        ' Iterar a través de los codificadores disponibles

                If encoder.MimeType = mimeType Then                               ' Comprobar si el tipo MIME del codificador coincide con el tipo MIME especificado
                    Return encoder                                                ' Devolver el codificador correspondiente
                End If
            Next
            Return Nothing
        End Function



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

        Private Sub GenerarVisor(dbmCore As DBCore.DBCoreDataBaseManager, dbmImaging As DBImaging.DBImagingDataBaseManager, idOT As Integer, OutputFolder As String, FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable, FileDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable, FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable, FileValidacionesDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable)
            Const DataBaseName As String = "ExportedData.accdb"
            Dim ConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & OutputFolder & DataBaseName & ";Persist Security Info=False"

            Dim Conexion As OleDb.OleDbConnection = Nothing


            Try

                ' Copiar visor
                If Not File.Exists(OutputFolder & DataBaseName) Then
                    File.Copy(Program.AppPath & "OffLineViewer\ExportedData.accdb", OutputFolder & DataBaseName, True)
                End If
                If Not File.Exists(OutputFolder & "OffLineViewer.exe") Then
                    File.Copy(Program.AppPath & "OffLineViewer\OffLineViewer.exe", OutputFolder & "OffLineViewer.exe", True)
                End If
                If Not File.Exists(OutputFolder & "OffLineViewer.Library.dll") Then
                    File.Copy(Program.AppPath & "OffLineViewer\OffLineViewer.Library.dll", OutputFolder & "OffLineViewer.Library.dll", True)
                End If


                Conexion = New OleDb.OleDbConnection(ConnectionString)
                Conexion.Open()

                Dim Comando = New OleDb.OleDbCommand("", Conexion)

                ' Llaves
                Dim KeysDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)

                Dim KeyName1 As String = ""
                If (KeysDataTable.Count > 0) Then KeyName1 = KeysDataTable(0).Nombre_Proyecto_Llave
                Dim KeyName2 As String = ""
                If (KeysDataTable.Count > 1) Then KeyName2 = KeysDataTable(1).Nombre_Proyecto_Llave
                Dim KeyName3 As String = ""
                If (KeysDataTable.Count > 2) Then KeyName3 = KeysDataTable(2).Nombre_Proyecto_Llave

                ' Crear Configuracion

                Dim Dtresultados As New DataTable
                Dim adapter As OleDb.OleDbDataAdapter


                Comando.CommandText = "SELECT * FROM TBL_Config WHERE id_Entidad = " & Program.ImagingGlobal.Entidad &
                                        " AND id_Proyecto = " & Program.ImagingGlobal.Proyecto & ";"

                adapter = New OleDb.OleDbDataAdapter(Comando)
                adapter.Fill(Dtresultados)

                If Dtresultados.Rows.Count = 0 Then
                    Dim EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(Program.ImagingGlobal.Entidad)
                    Comando.CommandText = " INSERT INTO TBL_Config (id_Entidad, Nombre_Entidad, id_Proyecto, Nombre_Proyecto, Key_1, Key_2, Key_3)" &
                                            "SELECT " & Program.ImagingGlobal.Entidad &
                                            ", '" & EntidadDataTable(0).Nombre_Entidad & "', " &
                                            Program.ImagingGlobal.Proyecto &
                                            ", '" & Program.ImagingGlobal.ProyectoImagingRow.Nombre_Proyecto &
                                            "', '" & KeyName1 &
                                            "', '" & KeyName2 &
                                            "', '" & KeyName3 & "';"

                    Comando.ExecuteNonQuery()
                End If



                ' Crear los Esquemas
                Dim EsquemasDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Esquema.DBExecute(idOT)



                For Each EsquemaRow In EsquemasDataTable

                    Comando.CommandText = "SELECT * FROM TBL_Esquema WHERE id_Esquema = " & EsquemaRow.id_Esquema & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Esquema (id_Esquema, Nombre_Esquema)" &
                                            "SELECT " & EsquemaRow.id_Esquema &
                                            ", '" & EsquemaRow.Nombre_Esquema & "';"

                        Comando.ExecuteNonQuery()
                    End If
                Next

                ' Crear los Documentos
                Dim DocumentosDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Documento.DBExecute(idOT)
                For Each DocumentoRow In DocumentosDataTable

                    Comando.CommandText = "SELECT * FROM TBL_Documento WHERE id_Documento = " & DocumentoRow.id_Documento & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)
                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Documento (id_Documento, Nombre_Documento)" &
                                            "SELECT " & DocumentoRow.id_Documento &
                                            ", '" & DocumentoRow.Nombre_Documento & "';"

                        Comando.ExecuteNonQuery()
                    End If

                Next

                ' Crear Campos de Búsqueda
                Dim CampoBusquedaDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo_Busqueda.DBExecute(idOT)
                For Each CampoBusquedaRow In CampoBusquedaDataTable

                    Comando.CommandText = "SELECT * FROM TBL_Campo_Busqueda WHERE" &
                                        " fk_Campo_Tipo = " & CampoBusquedaRow.fk_Campo_Tipo &
                                        " AND id_Campo_Busqueda = " & CampoBusquedaRow.id_Campo_Busqueda & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Campo_Busqueda (fk_Campo_Tipo, id_Campo_Busqueda, Nombre_Campo_Busqueda)" &
                                            "SELECT " & CampoBusquedaRow.fk_Campo_Tipo &
                                            ", " & CampoBusquedaRow.id_Campo_Busqueda &
                                            ", '" & CampoBusquedaRow.Nombre_Campo_Busqueda & "';"

                        Comando.ExecuteNonQuery()

                    End If

                Next

                ' Crear los Campos
                Dim CamposDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo.DBExecute(idOT)
                For Each CampoRow In CamposDataTable

                    Comando.CommandText = "SELECT * FROM TBL_Campo WHERE" &
                                        " fk_Documento = " & CampoRow.fk_Documento &
                                        " AND id_Campo = " & CampoRow.id_Campo & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Campo (fk_Documento, id_Campo, Nombre_Campo, Es_Campo_Busqueda, fk_Campo_Tipo, fk_Campo_Busqueda)" &
                                            "SELECT " & CampoRow.fk_Documento &
                                            ", " & CampoRow.id_Campo &
                                            ", '" & CampoRow.Nombre_Campo & "'" &
                                            ", " & IIf(CampoRow.Es_Campo_Busqueda, "1", "0").ToString() &
                                            ", " & CampoRow.fk_Campo_Tipo &
                                            ", " & CampoRow.fk_Campo_Busqueda & ";"

                        Comando.ExecuteNonQuery()
                    End If
                Next

                ' Crear las Validaciones
                Dim ValidacionDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Validacion.DBExecute(idOT)
                For Each ValidacionRow In ValidacionDataTable

                    Comando.CommandText = "SELECT * FROM TBL_Validacion WHERE" &
                                        " fk_Documento = " & ValidacionRow.fk_Documento &
                                        " AND id_Validacion = " & ValidacionRow.id_Validacion & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Validacion (fk_Documento, id_Validacion, Pregunta_Validacion)" &
                                            "SELECT " & ValidacionRow.fk_Documento &
                                            ", " & ValidacionRow.id_Validacion &
                                            ", '" & ValidacionRow.Pregunta_Validacion & "';"

                        Comando.ExecuteNonQuery()
                    End If
                Next

                ' Crear Folders            
                For Each FolderRow In FolderDataTable
                    Comando.CommandText = "INSERT INTO TBL_Folder (fk_Expediente, id_Folder, fk_Esquema, Key_1, Key_2, Key_3, CBarras_Folder)" &
                                        "SELECT " & FolderRow.fk_Expediente &
                                        ", " & FolderRow.fk_Folder &
                                        ", " & FolderRow.id_Esquema &
                                        ", '" & FolderRow.Key_1 & "'" &
                                        ", '" & FolderRow.Key_2 & "'" &
                                        ", '" & FolderRow.Key_3 & "'" &
                                        ", '" & FolderRow.CBarras_Folder & "';"

                    Comando.ExecuteNonQuery()
                Next

                ' Crear Files            
                For Each FileRow In FileDataTable
                    Comando.CommandText = "INSERT INTO TBL_File (fk_Expediente, fk_Folder, id_File, id_Version, File_Unique_Identifier, fk_Documento, Nombre_Imagen_File, Folios_Documento_File, Tamaño_Imagen_File)" &
                                        "SELECT " & FileRow.fk_Expediente &
                                        ", " & FileRow.fk_Folder &
                                        ", " & FileRow.fk_File &
                                        ", " & FileRow.id_Version &
                                        ", '" & FileRow.File_Unique_Identifier.ToString() & "'" &
                                        ", " & FileRow.fk_Documento &
                                        ", '" & FileRow.Nombre_Imagen_File & "'" &
                                        ", " & FileRow.Folios_Documento_File &
                                        ", " & FileRow.Tamaño_Imagen_File & ";"

                    Comando.ExecuteNonQuery()
                Next

                ' Crear File Data            
                For Each DataRow In FileDataDataTable
                    'Dim valor As String = ""
                    'If (Not DataRow.IsNull("Valor_File_Data")) Then valor = DataRow.Valor_File_Data
                    Comando.CommandText = "INSERT INTO TBL_File_Data (fk_Expediente, fk_Folder, fk_File, fk_Version, fk_Campo, fk_Documento, fk_Campo_Tipo, Valor_File_Data)" &
                                        "SELECT " & DataRow.fk_Expediente &
                                        ", " & DataRow.fk_Folder &
                                        ", " & DataRow.fk_File &
                                        ", " & DataRow.id_Version &
                                        ", " & DataRow.id_Campo &
                                        ", " & DataRow.fk_Documento &
                                        ", " & DataRow.fk_Campo_Tipo &
                                        ", '" & DataRow.Valor_File_Data & "';"

                    Comando.ExecuteNonQuery()
                Next

                ' Crear File Validacion            
                For Each DataRow In FileValidacionesDataTable
                    Comando.CommandText = "INSERT INTO TBL_File_Validacion (fk_Expediente, fk_Folder, fk_File, fk_Version, fk_Validacion, fk_Documento, Respuesta)" &
                                        "SELECT " & DataRow.fk_Expediente &
                                        ", " & DataRow.fk_Folder &
                                        ", " & DataRow.fk_File &
                                        ", " & DataRow.id_Version &
                                        ", " & DataRow.id_Validacion &
                                        ", " & DataRow.fk_Documento &
                                        ", " & IIf(DataRow.Respuesta, "1", "0").ToString() & ";"

                    Comando.ExecuteNonQuery()
                Next
            Catch ex As Exception
                Throw
            Finally
                If (Conexion IsNot Nothing) Then Conexion.Close()
            End Try
        End Sub

        Private Sub GenerarXML(dbmCore As DBCore.DBCoreDataBaseManager, dbmImaging As DBImaging.DBImagingDataBaseManager, idOT As Integer, OutputFolder As String, FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable, FileDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable, FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable, FileValidacionDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable)
            Dim ExportacionDataSet As New OffLineViewer.Library.xsdOfflineData

            ' Llaves
            Dim KeysDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)

            Dim KeyName1 As String = ""
            If (KeysDataTable.Count > 0) Then KeyName1 = KeysDataTable(0).Nombre_Proyecto_Llave
            Dim KeyName2 As String = ""
            If (KeysDataTable.Count > 1) Then KeyName2 = KeysDataTable(1).Nombre_Proyecto_Llave
            Dim KeyName3 As String = ""
            If (KeysDataTable.Count > 2) Then KeyName3 = KeysDataTable(2).Nombre_Proyecto_Llave

            ' Configuración
            Dim EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(Program.ImagingGlobal.Entidad)
            ExportacionDataSet.TBL_Config.AddTBL_ConfigRow(Program.ImagingGlobal.Entidad, EntidadDataTable(0).Nombre_Entidad, Program.ImagingGlobal.Proyecto, Program.ImagingGlobal.ProyectoImagingRow.Nombre_Proyecto, KeyName1, KeyName2, KeyName3)

            ' Folder
            For Each FolderRow In FolderDataTable
                ExportacionDataSet.TBL_Folder.AddTBL_FolderRow(FolderRow.fk_Expediente, FolderRow.fk_Folder, FolderRow.id_Esquema, FolderRow.Nombre_Esquema, FolderRow.Key_1, FolderRow.Key_2, FolderRow.Key_3, FolderRow.CBarras_Folder)
            Next

            ' File
            For Each FileRow In FileDataTable
                ExportacionDataSet.TBL_File.AddTBL_FileRow(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, FileRow.File_Unique_Identifier, FileRow.Nombre_Documento, FileRow.Nombre_Imagen_File, FileRow.Folios_Documento_File, FileRow.Tamaño_Imagen_File)
            Next

            ' Data
            For Each FileDataRow In FileDataDataTable
                ExportacionDataSet.TBL_File_Data.AddTBL_File_DataRow(FileDataRow.fk_Expediente, FileDataRow.fk_Folder, FileDataRow.fk_File, FileDataRow.id_Version, FileDataRow.id_Campo, FileDataRow.Nombre_Campo, FileDataRow.Es_Campo_Busqueda, FileDataRow.fk_Campo_Tipo, FileDataRow.fk_Campo_Busqueda, FileDataRow.Valor_File_Data, FileDataRow.fk_Documento)
            Next

            ' Validaciones
            For Each FileValidacionRow In FileValidacionDataTable
                ExportacionDataSet.TBL_File_Validacion.AddTBL_File_ValidacionRow(FileValidacionRow.fk_Expediente, FileValidacionRow.fk_Folder, FileValidacionRow.fk_File, FileValidacionRow.id_Version, FileValidacionRow.id_Validacion, FileValidacionRow.Pregunta_Validacion, FileValidacionRow.Respuesta, FileValidacionRow.fk_Documento)
            Next

            ' Busqueda
            Dim CampoBusquedaDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo_Busqueda.DBExecute(idOT)
            For Each CampoBusquedaRow In CampoBusquedaDataTable
                ExportacionDataSet.TBL_Campo_Busqueda.AddTBL_Campo_BusquedaRow(CampoBusquedaRow.fk_Campo_Tipo, CampoBusquedaRow.id_Campo_Busqueda, CampoBusquedaRow.Nombre_Campo_Busqueda)
            Next


            ExportacionDataSet.WriteXml(OutputFolder & "\" & "ExportedData.xml")
        End Sub

        Private Sub GenerarXMLExpedientes(Generar As Boolean, dbmCore As DBCore.DBCoreDataBaseManager, dbmImaging As DBImaging.DBImagingDataBaseManager, _
                                                idOT As Integer, OutputFolder As String, _
                                                FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable, _
                                                FileDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable, _
                                                FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable, _
                                                FileValidacionDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable, _
                                                ExportacionDataSet As OffLineViewer.Library.xsdOfflineData)
            'Dim ExportacionDataSet As New OffLineViewer.Library.xsdOffLineData

            ' Llaves
            Dim KeysDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)

            Dim KeyName1 As String = ""
            If (KeysDataTable.Count > 0) Then KeyName1 = KeysDataTable(0).Nombre_Proyecto_Llave
            Dim KeyName2 As String = ""
            If (KeysDataTable.Count > 1) Then KeyName2 = KeysDataTable(1).Nombre_Proyecto_Llave
            Dim KeyName3 As String = ""
            If (KeysDataTable.Count > 2) Then KeyName3 = KeysDataTable(2).Nombre_Proyecto_Llave

            ' Configuración
            If ExportacionDataSet.TBL_Config.Select("id_Entidad = " & Program.ImagingGlobal.Entidad & "and  id_Proyecto = " & Program.ImagingGlobal.Proyecto).Length = 0 Then
                Dim EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(Program.ImagingGlobal.Entidad)
                ExportacionDataSet.TBL_Config.AddTBL_ConfigRow(Program.ImagingGlobal.Entidad, EntidadDataTable(0).Nombre_Entidad, Program.ImagingGlobal.Proyecto, Program.ImagingGlobal.ProyectoImagingRow.Nombre_Proyecto, KeyName1, KeyName2, KeyName3)
            End If

            ' Folder
            For Each FolderRow In FolderDataTable
                ExportacionDataSet.TBL_Folder.AddTBL_FolderRow(FolderRow.fk_Expediente, FolderRow.fk_Folder, FolderRow.id_Esquema, FolderRow.Nombre_Esquema, FolderRow.Key_1, FolderRow.Key_2, FolderRow.Key_3, FolderRow.CBarras_Folder)
            Next

            ' File
            For Each FileRow In FileDataTable
                ExportacionDataSet.TBL_File.AddTBL_FileRow(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, FileRow.File_Unique_Identifier, FileRow.Nombre_Documento, FileRow.Nombre_Imagen_File, FileRow.Folios_Documento_File, FileRow.Tamaño_Imagen_File)
            Next

            ' Data
            For Each FileDataRow In FileDataDataTable
                ExportacionDataSet.TBL_File_Data.AddTBL_File_DataRow(FileDataRow.fk_Expediente, FileDataRow.fk_Folder, FileDataRow.fk_File, FileDataRow.id_Version, FileDataRow.id_Campo, FileDataRow.Nombre_Campo, FileDataRow.Es_Campo_Busqueda, FileDataRow.fk_Campo_Tipo, FileDataRow.fk_Campo_Busqueda, FileDataRow.Valor_File_Data, FileDataRow.fk_Documento)
            Next

            ' Validaciones
            For Each FileValidacionRow In FileValidacionDataTable
                ExportacionDataSet.TBL_File_Validacion.AddTBL_File_ValidacionRow(FileValidacionRow.fk_Expediente, FileValidacionRow.fk_Folder, FileValidacionRow.fk_File, FileValidacionRow.id_Version, FileValidacionRow.id_Validacion, FileValidacionRow.Pregunta_Validacion, FileValidacionRow.Respuesta, FileValidacionRow.fk_Documento)
            Next

            ' Busqueda
            Dim CampoBusquedaDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo_Busqueda.DBExecute(idOT)
            For Each CampoBusquedaRow In CampoBusquedaDataTable
                If ExportacionDataSet.TBL_Campo_Busqueda.Select("fk_Campo_Tipo = " & CampoBusquedaRow.fk_Campo_Tipo & "and  id_Campo_Busqueda = " & CampoBusquedaRow.id_Campo_Busqueda).Length = 0 Then
                    ExportacionDataSet.TBL_Campo_Busqueda.AddTBL_Campo_BusquedaRow(CampoBusquedaRow.fk_Campo_Tipo, CampoBusquedaRow.id_Campo_Busqueda, CampoBusquedaRow.Nombre_Campo_Busqueda)
                End If

            Next



            If Generar Then
                If Not File.Exists(OutputFolder & "\" & "ExportedData.xml") Then
                    ExportacionDataSet.WriteXml(OutputFolder & "\" & "ExportedData.xml")
                End If

            End If

        End Sub

        Private Sub GenerarTXT(ByVal OutputFolder As String, ByVal FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable, ByVal FileDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable, ByVal FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable, ByVal FileValidacionDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable)
            Dim CSVData = New CSV.CSVData(vbTab, """", True)

            CSVData.SaveAsCSV(New CSV.CSVTable(FolderDataTable), OutputFolder & "Folders.txt", False)
            CSVData.SaveAsCSV(New CSV.CSVTable(FileDataTable), OutputFolder & "Files.txt", False)
            CSVData.SaveAsCSV(New CSV.CSVTable(FileDataDataTable), OutputFolder & "Data.txt", False)
            CSVData.SaveAsCSV(New CSV.CSVTable(FileValidacionDataTable), OutputFolder & "Validaciones.txt", False)
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

        Private Sub ExportarImagenAgrupada(nManager As FileProviderManager, ByVal nFilesExpedientesbyGroupDataView As DataView, ngrupo As Integer, nfk_Expediente As Long, nfk_Folder As Short, nCompresion As ImageManager.EnumCompression, nFileFolderName As String, nFolderName As String)
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

                For Each itemfile As DataRowView In nFilesExpedientesbyGroupDataView
                    itemfile.Item("Nombre_Imagen_File") = nFolderName & FileNameAux & ExtensionAux
                Next
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

    Public Class ImageManagerDomain
        Inherits MarshalByRefObject

        Public Sub Save(ByVal nInputFileNames As List(Of String), ByVal nOutputFileName As String, ByVal nSuffixFormat As String, ByVal nFormat As ImageManager.EnumFormat, ByVal nCompression As ImageManager.EnumCompression, ByVal nSinglePage As Boolean, ByVal nTempPath As String, ByVal nIsInputSingle As Boolean)
            ImageManager.Save(nInputFileNames, nOutputFileName, nSuffixFormat, nFormat, nCompression, nSinglePage, nTempPath, nIsInputSingle)
        End Sub
    End Class

End Namespace