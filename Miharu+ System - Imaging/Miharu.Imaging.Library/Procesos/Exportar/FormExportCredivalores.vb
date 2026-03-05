Imports System.Windows.Forms
Imports System.IO
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library
Imports Slyg.Tools
Imports Miharu.Imaging.Library.Eventos
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Xml.Linq
Imports System.Linq
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Drawing
Imports System.Web
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Slyg.Tools.Progress
Imports DBImaging
Imports System.Drawing.Imaging

Namespace Procesos.Exportar

    Public Class FormExportCredivalores
        Inherits FormBase

#Region " Declaraciones "
        Private threadList As List(Of Thread) = New List(Of Thread)()
        Private threadListPorject As List(Of Thread) = New List(Of Thread)()
        Private Usa_Exportacion_PDF As Boolean
        Private formatoAux As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Private CompresionAux As Slyg.Tools.Imaging.ImageManager.EnumCompression
        Private formato As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Private ArrayNotificacion As ArrayList
        Private ArrayImagenes As ArrayList
        Private ArrayNotificacionProjectos As ArrayList
        Private BloqueoConcurrencia As Object
        Private BloqueoImagenes As Object
        Private BloqueoConcurrenciaProjectos As Object
        Private totalFolios As Integer = 0
        Private ProgressForm As New FormProgress()
        Dim compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression
        Private TotalRegistrosExportar As Integer
        Private Count_Process_Delta As Integer = 0
        Private ViewExpedientes As New DataView
        Private ExpedientesSeleccion As New DataTable
        Public Shared FileNamesCons As New List(Of String)
        Dim FolderNameOutput As String


#End Region

#Region " Propiedades"
        Private Entidad As Short
        Private Proyecto As Short
        Private NombreProyecto As String
        Private DataTableExpedientes As DataTable
        Private DataTableExportacionFolders As DataTable
        Private DataTableExportacion_Data As DataTable
        Private DataTableExportacion_Validaciones As DataTable
        Private ResultTableLogs As DataTable
        Private EscargueLog As Boolean

        Sub New(nEntidad As Short, nProyecto As Short, nNombreProyecto As String, nResultTableLogs As DataTable, nEscargueLog As Boolean)
            ' TODO: Complete member initialization 
            InitializeComponent()
            Entidad = nEntidad
            Proyecto = nProyecto
            NombreProyecto = nNombreProyecto
            ResultTableLogs = nResultTableLogs
            EscargueLog = nEscargueLog
        End Sub

#End Region

#Region " Eventos "
        Private Sub FormExportCredivalores_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Usa_Exportacion_PDF = Program.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF
            formato = Utilities.GetEnumFormat(Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
            compresion = Utilities.GetEnumCompression(CType(Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))
            Load_FormatoCargue()
            If EscargueLog Then
                FechaProcesoDateTimePicker.Visible = False
                FechaProcesoFinalDateTimePicker.Visible = False
                BuscarFechaButton.Visible = False
                FechaProcesoLabel.Visible = False
                FechaProcesoFinalLabel.Visible = False
                OTLabel.Visible = False
                ExpedientesDataGridView.Visible = False
                MainGroupBox.AutoSize = True
                Recuperar.Visible = False
            Else
                MostrarDatagrid()
            End If
        End Sub

        Private Sub BuscarFechaButton_Click(sender As System.Object, e As EventArgs) Handles BuscarFechaButton.Click
            If validarFechaProceso() Then
                CargarProyectos()
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
            If EscargueLog Then
                ExportarExpedientesLog()
            Else
                ExportarExpedientes()
            End If

        End Sub

        Private Sub Recuperar_Click(sender As System.Object, e As System.EventArgs) Handles Recuperar.Click
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim NombreProyecto As String = Nothing
            Dim NombreEntidad As String = Nothing
            Dim OutputFolder As String = Nothing
            Dim tmpFolderDataTable As DataTable = Nothing
            Dim tmpFileDataDataTable As DataTable = Nothing
            Dim tmpFileValidacionDataTable As DataTable = Nothing
            Dim DataTableexpediente As DataTable
            If validarFechaProceso() Then
                If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea exportar las fechas seleccionadas?", "Exportar imagenes", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    OutputFolder = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
                    If Directory.Exists(OutputFolder) Then
                        Dim FilesNames = Directory.GetFiles(OutputFolder, "*.txt")
                        If FilesNames.Count > 0 Then
                            Try
                                Dim DataTableTxt As DataTable = Nothing
                                DataTableexpediente = getExpedientes()
                                tmpFolderDataTable = getExportacionFolders()
                                tmpFileDataDataTable = getExportacion_Data()
                                tmpFileValidacionDataTable = getExportacion_Validaciones()

                                For Each Files In FilesNames
                                    Dim separadorArchivos = ","
                                    Dim dt As DataTable = Nothing
                                    Dim encabezados As New List(Of String)
                                    encabezados.Add("Expediente")
                                    encabezados.Add("Folder")
                                    encabezados.Add("File")
                                    encabezados.Add("Version")
                                    Dim lineaActual As String()

                                    Dim sr = New StreamReader(Files)
                                    Dim linea As String
                                    Dim lineas As List(Of String) = New List(Of String)

                                    While Not sr.EndOfStream
                                        linea = sr.ReadLine()
                                        lineas.Add(linea)
                                    End While
                                    dt = New DataTable()
                                    For Each col As String In encabezados
                                        dt.Columns.Add(col)
                                    Next
                                    For p As Integer = 1 To lineas.Count - 1
                                        lineaActual = lineas(p).Split(Convert.ToChar(separadorArchivos))

                                        Dim row As DataRow = dt.NewRow()
                                        row.ItemArray = lineaActual
                                        Dim expediente = row.Item("Expediente")
                                        Dim folder = row.Item("Folder")
                                        Dim file = row.Item("File")
                                        Dim version = row.Item("Version")
                                        Dim Data As List(Of DataRow) = DataTableexpediente.Select("fk_Expediente = " & CStr(expediente) & " AND fk_Folder = " & CStr(folder) &
                                                    " AND fk_File = " & CStr(file) & " AND id_Version = " & CStr(version)).ToList()

                                        Dim DataFolder As List(Of DataRow) = tmpFolderDataTable.Select("fk_Expediente = " & CStr(expediente) & " AND fk_Folder = " & CStr(folder)).ToList()

                                        Dim DataFile As List(Of DataRow) = tmpFileDataDataTable.Select("fk_Expediente = " & CStr(expediente) & " AND fk_Folder = " & CStr(folder) &
                                                        " AND fk_File = " & CStr(file) & " AND id_Version = " & CStr(version)).ToList()

                                        If Data.Count > 0 Then
                                            DataTableexpediente.Rows.Remove(Data(0))
                                        End If
                                        If DataFolder.Count > 0 Then
                                            tmpFolderDataTable.Rows.Remove(DataFolder(0))
                                        End If
                                        If DataFile.Count > 0 Then
                                            tmpFileDataDataTable.Rows.Remove(DataFile(0))
                                        End If
                                        Dim LogTotaltxt = CStr(expediente) + "," + CStr(folder) + "," + CStr(file) + "," + CStr(version)
                                        logExpedientes(OutputFolder, "ExpedientesTXT", LogTotaltxt)
                                    Next
                                    sr.Close()
                                    Exit For
                                Next
                                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal

                                BloqueoConcurrencia = New Object
                                BloqueoConcurrenciaProjectos = New Object
                                'Dim ProgressForm As New FormProgress

                                FileNamesCons = New List(Of String)

                                Dim DatatableNombreEntidad = getEntidad()
                                For Each rowEntidad As DataRow In DatatableNombreEntidad.Rows
                                    NombreEntidad = CStr(rowEntidad("Nombre_Entidad"))
                                Next
                                If Not Directory.Exists(OutputFolder & NombreEntidad) Then
                                    Directory.CreateDirectory(OutputFolder & NombreEntidad)
                                End If
                                Dim DataTableproyectos = getProyectos()

                                For Each rowproyectos As DataRow In DataTableproyectos.Rows
                                    Dim Proyecto = rowproyectos("id_Proyecto")
                                    NombreProyecto = CStr(rowproyectos("Nombre_Proyecto"))
                                    Dim Viewexpediente = New DataView(DataTableexpediente)
                                    Viewexpediente.RowFilter() = "fk_Proyecto = " & CStr(Proyecto)

                                    Dim ExportacionDataSetXML As New OffLineViewer.Library.xsdOffLineData
                                    Dim FolderDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                                    Dim FileDataTableTXT = New DataTable
                                    Dim FileDataDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                                    Dim FileValidacionDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable
                                    Dim FolderDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                                    Dim FileDataDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                                    Dim FileValidacionDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable
                                    Dim FileDataTable As DataTable = Nothing
                                    Try

                                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                                        dbmImaging.Connection_Open(1)
                                        dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                                        dbmCore.Connection_Open(1)

                                        If Viewexpediente.Count > 0 Then
                                            Application.DoEvents()

                                            Dim Servidor As New DataTable

                                            Dim ViewServidor = Viewexpediente
                                            Servidor = ViewServidor.ToTable(True, "fk_ServidorStorage")
                                            Dim LogTotalesOt As String = Nothing

                                            Dim TotalesDataTable = Exportacion_Totales(CInt(Proyecto))
                                            For Each ItemTotales As DataRow In TotalesDataTable.Rows
                                                LogTotalesOt = "Se encontró : " & ItemTotales.Item("Files").ToString & " Unidades Documentales, " &
                                                                        ItemTotales.Item("Folios").ToString & " Folios con " &
                                                                        (CDbl(ItemTotales.Item("Tamaño").ToString) / 1024 / 1024).ToString("#,##0.00") & "MB de tamaño, "
                                            Next




                                            Dim FileFolderName As String = Nothing
                                            FileFolderName = NombreEntidad + "\" & NombreProyecto & "\"
                                            FolderNameOutput = OutputFolder & "\" & FileFolderName
                                            If (Not Directory.Exists(OutputFolder & FileFolderName)) Then
                                                Directory.CreateDirectory(OutputFolder & FileFolderName)
                                            End If
                                            log(FolderNameOutput, "Total a exportar_" + NombreProyecto, LogTotalesOt)

                                            Dim Column1 As String = Nothing
                                            Dim Column2 As String = Nothing
                                            FolderDataTable.Columns.Add.ColumnMapping = CType(Column1, MappingType)
                                            FileDataDataTable.Columns.Add.ColumnMapping = CType(Column1, MappingType)
                                            FileDataDataTable.Columns.Add.ColumnMapping = CType(Column2, MappingType)
                                            FileValidacionDataTable.Columns.Add.ColumnMapping = CType(Column1, MappingType)
                                            FileValidacionDataTable.Columns.Add.ColumnMapping = CType(Column2, MappingType)

                                            ArrayNotificacion = New ArrayList
                                            Dim hilos As Integer = 0
                                            For Each rowServidor As DataRow In Servidor.Rows
                                                ClearMemory()
                                                If Viewexpediente.Count > 0 Then
                                                    Viewexpediente.RowFilter() = "fk_ServidorStorage = " & rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)
                                                    FileDataTable = Viewexpediente.ToTable()
                                                End If
                                                Dim tmpFolderDataTables As DataTable = Nothing
                                                Dim tmpFileDataDataTables As DataTable = Nothing
                                                Dim tmpFileValidacionDataTables As DataTable = Nothing
                                                If tmpFolderDataTable.Rows.Count > 0 Then
                                                    Dim CountRegistros As List(Of DataRow) = tmpFolderDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)).ToList
                                                    If CountRegistros.Count > 0 Then
                                                        tmpFolderDataTables = tmpFolderDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)).CopyToDataTable()
                                                        TablaDesdeTemporal(FolderDataTable, tmpFolderDataTables)
                                                    End If
                                                End If

                                                If tmpFileDataDataTable.Rows.Count > 0 Then
                                                    Dim CountRegistros As List(Of DataRow) = tmpFileDataDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)).ToList
                                                    If CountRegistros.Count > 0 Then
                                                        tmpFileDataDataTables = tmpFileDataDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)).CopyToDataTable()
                                                        TablaDesdeTemporal(FileDataDataTable, tmpFileDataDataTables)
                                                    End If
                                                End If
                                                If tmpFileValidacionDataTable.Rows.Count > 0 Then
                                                    Dim CountRegistros As List(Of DataRow) = tmpFileValidacionDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)).ToList
                                                    If CountRegistros.Count > 0 Then
                                                        tmpFileValidacionDataTables = tmpFileValidacionDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)).CopyToDataTable()
                                                        TablaDesdeTemporal(FileValidacionDataTable, tmpFileValidacionDataTables)
                                                    End If
                                                End If

                                                If Me.TXTRadioButton.Checked Then
                                                    TablaDesdeTemporal(FolderDataTableTXT, tmpFolderDataTable)
                                                    FileDataTableTXT = FileDataTable
                                                    TablaDesdeTemporal(FileDataDataTableTXT, tmpFileDataDataTable)
                                                    TablaDesdeTemporal(FileValidacionDataTableTXT, tmpFileValidacionDataTable)
                                                End If



                                                Dim Compresion As ImageManager.EnumCompression

                                                If (Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                                                    Compresion = ImageManager.EnumCompression.Ccitt3
                                                Else
                                                    Compresion = ImageManager.EnumCompression.Lzw
                                                End If

                                                Try
                                                    Dim servidore = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, CShort(rowServidor.Item("fk_ServidorStorage")))
                                                    Dim sevidores = servidore(0).ToCTA_ServidorSimpleType()
                                                    Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()


                                                    Dim Documentos As List(Of Object) = Nothing
                                                    Documentos = (From a In FileDataTable Group a By groupDt = a.Field(Of Integer)("fk_Documento") Into Group Select Group.Select(Function(x) x("fk_Documento")).First()).ToList()
                                                    Dim FilesDataView As New DataView(FileDataTable)


                                                    For Each documento As Integer In Documentos
                                                        FilesDataView.RowFilter = " fk_Documento = " & documento.ToString()

                                                        'Dim TotalHilosDisponibles = ArrayNotificacion.Count - hilos
                                                        Dim ArraListParameters As ArrayList = New ArrayList
                                                        ArraListParameters.Add(ArrayNotificacion.Count)
                                                        ArraListParameters.Add(FilesDataView.ToTable())
                                                        ArraListParameters.Add(Compresion)
                                                        ArraListParameters.Add(OutputFolder & FileFolderName)
                                                        ArraListParameters.Add(FileFolderName)
                                                        ArraListParameters.Add(CInt(Proyecto))
                                                        ArraListParameters.Add(sevidores)
                                                        ArraListParameters.Add(centro)
                                                        ArraListParameters.Add(NombreProyecto)
                                                        ArraListParameters.Add(OutputFolder)

                                                        SyncLock BloqueoConcurrencia
                                                            ArrayNotificacion.Add(False)
                                                        End SyncLock

                                                        Dim NewThreadProjects As New Thread(AddressOf ExportarImagen)
                                                        NewThreadProjects.Start(ArraListParameters)
                                                        ClearMemory()
                                                    Next
                                                Catch ex As Exception
                                                    SyncLock BloqueoConcurrencia
                                                        log(FolderNameOutput, "Errores_" + NombreProyecto, "Error Hilos: " + ex.Message)
                                                    End SyncLock
                                                End Try
                                                SyncLock BloqueoConcurrencia
                                                    '------------  Si proyecto tiene configurado Exportar_Unico_Archivo_TIFF  ------------------
                                                    If (CBool(Program.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF)) Then
                                                        ExportAllFillesInTiff(Compresion, FolderNameOutput)
                                                    End If
                                                    '--------------------------------
                                                    If Directory.GetDirectories(FolderNameOutput).Length > 0 Or Directory.GetFiles(FolderNameOutput).Length > 0 Then
                                                        If (Me.VisorRadioButton.Checked) Then
                                                            GenerarVisor(dbmCore, dbmImaging, FolderNameOutput, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable, CInt(Proyecto), NombreProyecto, CInt(rowServidor.Item("fk_ServidorStorage")))
                                                        ElseIf (Me.XMLRadioButton.Checked) Then
                                                            Dim generar As Boolean = False
                                                            If rowServidor.Equals(FileDataTable.Rows(FileDataTable.Rows.Count - 1)) Then
                                                                generar = True
                                                            End If
                                                            GenerarXMLExpedientes(generar, dbmCore, dbmImaging, FolderNameOutput, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable, ExportacionDataSetXML, CInt(Proyecto), NombreProyecto)
                                                        ElseIf rowServidor.Equals(FileDataTable.Rows(FileDataTable.Rows.Count - 1)) Then

                                                            GenerarTXT(FolderNameOutput, FolderDataTableTXT, FileDataTableTXT, FileDataDataTableTXT, FileValidacionDataTableTXT)

                                                        End If
                                                    End If
                                                End SyncLock
                                            Next
                                            Dim SalirDeltaImagenComprimir As Boolean = False
                                            While SalirDeltaImagenComprimir = False
                                                SalirDeltaImagenComprimir = True
                                                SyncLock BloqueoConcurrencia
                                                    For Each HiloTerminadoDeltaImagenComprimir As Boolean In ArrayNotificacion
                                                        If HiloTerminadoDeltaImagenComprimir = False Then
                                                            SalirDeltaImagenComprimir = False
                                                        End If
                                                    Next
                                                End SyncLock
                                                Thread.Sleep(1000)
                                            End While
                                        End If
                                    Catch ex As Exception
                                        SyncLock BloqueoConcurrencia
                                            log(FolderNameOutput, "Errores_" + NombreProyecto, "Error exportar proyecto: " + ex.Message)
                                        End SyncLock
                                    Finally
                                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                                        If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                                        BorrarTemporal()
                                    End Try
                                Next rowproyectos
                                ProgressForm.Close()
                                MessageBox.Show("La información se exportó con éxito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Catch ex As Exception
                                SyncLock BloqueoConcurrencia
                                    log(FolderNameOutput, "Errores_" + NombreProyecto, ex.Message)
                                End SyncLock
                            End Try
                        Else
                            MessageBox.Show("No se encontró log en la carpeta seleccionada para reanudar el proceso de exportación", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

        Private Sub CheckBoxExpedientes_CheckedChanged(sender As System.Object, e As System.EventArgs)
            MostrarDatagrid()
        End Sub

        Private Sub CheckBoxExpedientesValidos_CheckedChanged(sender As System.Object, e As System.EventArgs)
            MostrarDatagrid()
        End Sub

#End Region

#Region " Metodos "
        Private Sub CargarProyectos()
            Me.ExpedientesDataGridView.AutoGenerateColumns = False
            Me.ExpedientesDataGridView.DataSource = getProyectos()
            Me.ExpedientesDataGridView.Refresh()

            If (Me.ExpedientesDataGridView.RowCount = 0) Then
                MessageBox.Show("No se encontraron Proyectos para el rango de fechas de proceso seleccionadas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
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
        Private Sub ExpedientesDataGridView_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ExpedientesDataGridView.CellContentClick
            If e.ColumnIndex = 0 Then
                For i = 0 To ExpedientesDataGridView.RowCount - 1
                    Dim DataGridViewProyecto = ExpedientesDataGridView.Rows(i).Cells("Nombre_Proyecto").Value
                Next
            End If
        End Sub
        Private Sub ExportarExpedientes()

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim Files As Integer = 0
            Dim NombreProyecto As String = Nothing
            Dim NombreEntidad As String = Nothing
            Try
                If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea exportar las fechas seleccionadas?", "Exportar imagenes", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    If (Validar()) Then
                        BloqueoConcurrencia = New Object
                        BloqueoConcurrenciaProjectos = New Object
                        'Dim ProgressForm As New FormProgress
                        FileNamesCons = New List(Of String)
                        ProgressForm.Action = "Imagenes"
                        ProgressForm.Process = "Imagenes"
                        ProgressForm.ValueAction = 0
                        ProgressForm.ValueProcess = 0
                        ProgressForm.Show()
                        ProgressForm.MaxValueAction = 0
                        ProgressForm.MaxValueProcess = 0

                        Application.DoEvents()
                        If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                        Dim DataTableexpediente = getExpedientes()
                        Dim DatatableNombreEntidad = getEntidad()
                        For Each rowEntidad As DataRow In DatatableNombreEntidad.Rows
                            NombreEntidad = CStr(rowEntidad("Nombre_Entidad"))
                        Next
                        Dim OutputFolder As String = Nothing

                        OutputFolder = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
                        If Not Directory.Exists(OutputFolder & NombreEntidad) Then
                            Directory.CreateDirectory(OutputFolder & NombreEntidad)
                        End If
                        Dim DataTableproyectos = getProyectos()
                        Dim tmpFolderDataTable As DataTable = Nothing
                        Dim tmpFileDataDataTable As DataTable = Nothing
                        Dim tmpFileValidacionDataTable As DataTable = Nothing
                        tmpFolderDataTable = getExportacionFolders()
                        tmpFileDataDataTable = getExportacion_Data()
                        tmpFileValidacionDataTable = getExportacion_Validaciones()

                        For Each rowproyectos As DataRow In DataTableproyectos.Rows
                            Dim Proyecto = rowproyectos("id_Proyecto")
                            NombreProyecto = CStr(rowproyectos("Nombre_Proyecto"))
                            Dim Viewexpediente = New DataView(DataTableexpediente)
                            Viewexpediente.RowFilter() = "fk_Proyecto = " & CStr(Proyecto)

                            Dim ExportacionDataSetXML As New OffLineViewer.Library.xsdOffLineData
                            Dim FolderDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                            Dim FileDataTableTXT = New DataTable
                            Dim FileDataDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                            Dim FileValidacionDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable
                            Dim FolderDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                            Dim FileDataDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                            Dim FileValidacionDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable
                            Dim FileDataTable As DataTable = Nothing
                            Try

                                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                                dbmImaging.Connection_Open(1)
                                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                                dbmCore.Connection_Open(1)
                                TotalRegistrosExportar = Viewexpediente.Count
                                If Viewexpediente.Count > 0 Then

                                    Dim Servidor As New DataTable

                                    Dim ViewServidor = Viewexpediente
                                    Servidor = ViewServidor.ToTable(True, "fk_ServidorStorage")

                                    Dim LogTotalesOt As String = Nothing

                                    Dim TotalesDataTable = Exportacion_Totales(CInt(Proyecto))
                                    For Each ItemTotales As DataRow In TotalesDataTable.Rows
                                        LogTotalesOt = "Se encontró : " & ItemTotales.Item("Files").ToString & " Unidades Documentales, " &
                                                                ItemTotales.Item("Folios").ToString & " Folios con " &
                                                                (CDbl(ItemTotales.Item("Tamaño").ToString) / 1024 / 1024).ToString("#,##0.00") & "MB de tamaño, "
                                    Next
                                    Dim FileFolderName As String = Nothing
                                    FileFolderName = NombreEntidad + "\" & NombreProyecto & "\"
                                    FolderNameOutput = OutputFolder & "\" & FileFolderName
                                    If (Not Directory.Exists(OutputFolder & FileFolderName)) Then
                                        Directory.CreateDirectory(OutputFolder & FileFolderName)
                                    End If

                                    log(FolderNameOutput, "Total a exportar_" + NombreProyecto, LogTotalesOt)

                                    Dim Column1 As String = Nothing
                                    Dim Column2 As String = Nothing
                                    FolderDataTable.Columns.Add.ColumnMapping = CType(Column1, MappingType)
                                    FileDataDataTable.Columns.Add.ColumnMapping = CType(Column1, MappingType)
                                    FileDataDataTable.Columns.Add.ColumnMapping = CType(Column2, MappingType)
                                    FileValidacionDataTable.Columns.Add.ColumnMapping = CType(Column1, MappingType)
                                    FileValidacionDataTable.Columns.Add.ColumnMapping = CType(Column2, MappingType)

                                    ArrayNotificacion = New ArrayList
                                    Dim hilos As Integer = 0
                                    For Each rowServidor As DataRow In Servidor.Rows
                                        ClearMemory()
                                        If Viewexpediente.Count > 0 Then
                                            Viewexpediente.RowFilter() = "fk_ServidorStorage = " & rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)
                                            FileDataTable = Viewexpediente.ToTable()
                                        End If
                                        Dim tmpFolderDataTables As DataTable = Nothing
                                        Dim tmpFileDataDataTables As DataTable = Nothing
                                        Dim tmpFileValidacionDataTables As DataTable = Nothing
                                        If tmpFolderDataTable.Rows.Count > 0 Then
                                            Dim CountRegistros As List(Of DataRow) = tmpFolderDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)).ToList
                                            If CountRegistros.Count > 0 Then
                                                tmpFolderDataTables = tmpFolderDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)).CopyToDataTable()
                                                TablaDesdeTemporal(FolderDataTable, tmpFolderDataTables)
                                            End If
                                        End If

                                        If tmpFileDataDataTable.Rows.Count > 0 Then
                                            Dim CountRegistros As List(Of DataRow) = tmpFileDataDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)).ToList
                                            If CountRegistros.Count > 0 Then
                                                tmpFileDataDataTables = tmpFileDataDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)).CopyToDataTable()
                                                TablaDesdeTemporal(FileDataDataTable, tmpFileDataDataTables)
                                            End If
                                        End If
                                        If tmpFileValidacionDataTable.Rows.Count > 0 Then
                                            Dim CountRegistros As List(Of DataRow) = tmpFileValidacionDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)).ToList
                                            If CountRegistros.Count > 0 Then
                                                tmpFileValidacionDataTables = tmpFileValidacionDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString & " AND fk_Proyecto = " & CStr(Proyecto)).CopyToDataTable()
                                                TablaDesdeTemporal(FileValidacionDataTable, tmpFileValidacionDataTables)
                                            End If
                                        End If

                                        If Me.TXTRadioButton.Checked Then
                                            TablaDesdeTemporal(FolderDataTableTXT, tmpFolderDataTable)
                                            FileDataTableTXT = FileDataTable
                                            TablaDesdeTemporal(FileDataDataTableTXT, tmpFileDataDataTable)
                                            TablaDesdeTemporal(FileValidacionDataTableTXT, tmpFileValidacionDataTable)
                                        End If

                                        ' Crear el directorio de las imágenes

                                        Dim Compresion As ImageManager.EnumCompression

                                        If (Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                                            Compresion = ImageManager.EnumCompression.Ccitt3
                                        Else
                                            Compresion = ImageManager.EnumCompression.Lzw
                                        End If

                                        Try
                                            Dim servidore = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, CShort(rowServidor.Item("fk_ServidorStorage")))
                                            Dim sevidores = servidore(0).ToCTA_ServidorSimpleType()
                                            Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()


                                            Dim Documentos As List(Of Object) = Nothing
                                            Documentos = (From a In FileDataTable Group a By groupDt = a.Field(Of Integer)("fk_Documento") Into Group Select Group.Select(Function(x) x("fk_Documento")).First()).ToList()
                                            Dim FilesDataView As New DataView(FileDataTable)

                                            For Each documento As Integer In Documentos
                                                FilesDataView.RowFilter = " fk_Documento = " & documento.ToString()

                                                Dim ArraListParameters As ArrayList = New ArrayList
                                                ArraListParameters.Add(ArrayNotificacion.Count)
                                                ArraListParameters.Add(FilesDataView.ToTable())
                                                ArraListParameters.Add(Compresion)
                                                ArraListParameters.Add(OutputFolder & FileFolderName)
                                                ArraListParameters.Add(FileFolderName)
                                                ArraListParameters.Add(CInt(Proyecto))
                                                ArraListParameters.Add(sevidores)
                                                ArraListParameters.Add(centro)
                                                ArraListParameters.Add(NombreProyecto)
                                                ArraListParameters.Add(OutputFolder)

                                                SyncLock BloqueoConcurrencia
                                                    ArrayNotificacion.Add(False)
                                                End SyncLock

                                                Dim NewThreadProjects As New Thread(AddressOf ExportarImagen)
                                                NewThreadProjects.Start(ArraListParameters)
                                                ClearMemory()
                                                For Each ItemFile As DataRowView In FilesDataView
                                                    For Each Items As DataRowView In FileDataTable.Rows
                                                        If CLng(Items.Item("fk_Expediente")) = CLng(ItemFile.Item("fk_Expediente")) And CShort(Items.Item("fk_Folder")) = CShort(ItemFile.Item("fk_Folder")) And CShort(Items.Item("fk_File")) = CShort(ItemFile.Item("fk_File")) Then
                                                            Items.Item("Nombre_Imagen_File") = CStr(ItemFile.Item("Nombre_Imagen_File"))
                                                        End If
                                                    Next
                                                Next
                                            Next
                                            Dim SalirDeltaImagenComprimir As Boolean = False
                                            While SalirDeltaImagenComprimir = False
                                                SalirDeltaImagenComprimir = True
                                                SyncLock BloqueoConcurrencia
                                                    For Each HiloTerminadoDeltaImagenComprimir As Boolean In ArrayNotificacion
                                                        If HiloTerminadoDeltaImagenComprimir = False Then
                                                            SalirDeltaImagenComprimir = False
                                                        End If
                                                    Next
                                                End SyncLock
                                                Thread.Sleep(1000)
                                                ClearMemory()
                                                ProgressForm.Action = "Imagenes"
                                                ProgressForm.Process = "Imagenes"
                                                ProgressForm.Show()
                                                Application.DoEvents()
                                                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                                                ProgressForm.ValueProcess = Count_Process_Delta
                                                ProgressForm.ValueAction = Count_Process_Delta
                                                ProgressForm.MaxValueProcess = TotalRegistrosExportar
                                                ProgressForm.MaxValueAction = TotalRegistrosExportar
                                            End While
                                        Catch ex As Exception
                                            SyncLock BloqueoConcurrencia
                                                log(FolderNameOutput, "Errores" + NombreProyecto, "Error Hilos: " + ex.Message)
                                            End SyncLock
                                        End Try
                                        SyncLock BloqueoConcurrencia
                                            '------------  Si proyecto tiene configurado Exportar_Unico_Archivo_TIFF  ------------------
                                            If (CBool(Program.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF)) Then
                                                ExportAllFillesInTiff(Compresion, FolderNameOutput)
                                            End If
                                            '--------------------------------
                                            If Directory.GetDirectories(FolderNameOutput).Length > 0 Or Directory.GetFiles(FolderNameOutput).Length > 0 Then
                                                If (Me.VisorRadioButton.Checked) Then
                                                    GenerarVisor(dbmCore, dbmImaging, FolderNameOutput, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable, CInt(Proyecto), NombreProyecto, CInt(rowServidor.Item("fk_ServidorStorage")))
                                                ElseIf (Me.XMLRadioButton.Checked) Then
                                                    Dim generar As Boolean = False
                                                    If rowServidor.Equals(FileDataTable.Rows(FileDataTable.Rows.Count - 1)) Then
                                                        generar = True
                                                    End If
                                                    GenerarXMLExpedientes(generar, dbmCore, dbmImaging, FolderNameOutput, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable, ExportacionDataSetXML, CInt(Proyecto), NombreProyecto)
                                                ElseIf rowServidor.Equals(FileDataTable.Rows(FileDataTable.Rows.Count - 1)) Then
                                                    GenerarTXT(FolderNameOutput, FolderDataTableTXT, FileDataTableTXT, FileDataDataTableTXT, FileValidacionDataTableTXT)
                                                End If
                                            End If
                                        End SyncLock
                                    Next
                                End If
                            Catch ex As Exception
                                SyncLock BloqueoConcurrencia
                                    log(FolderNameOutput, "Errores_" + NombreProyecto, "Error exportar proyecto: " + ex.Message)
                                End SyncLock
                            Finally
                                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                                BorrarTemporal()
                            End Try
                        Next rowproyectos
                        MessageBox.Show("La información se exportó con éxito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Catch ex As Exception
                SyncLock BloqueoConcurrencia
                    log(FolderNameOutput, "Errores_" + NombreProyecto, ex.Message)
                End SyncLock
            End Try

        End Sub

        Private Sub ExportarExpedientesLog()

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim Files As Integer = 0
            Dim NombreEntidad As String = Nothing
            Dim Cuenta As Integer = Now.Second
            Try

                If (Validar()) Then
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    ' PictureBoxGit.Visible = True
                    Dim TempValida = InserTemporal(ResultTableLogs, dbmImaging)

                    ProgressForm.Action = "Imagenes"
                    ProgressForm.Process = "Imagenes"
                    ProgressForm.ValueAction = 0
                    ProgressForm.ValueProcess = 0
                    ProgressForm.Show()
                    ProgressForm.MaxValueAction = 0
                    ProgressForm.MaxValueProcess = 0
                    ProgressForm.BackColor = Color.Black
                    Application.DoEvents()
                    If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")



                    Dim Proyectos As String = Nothing
                    If Proyecto = 4 And Entidad = 52 Then
                        Proyectos = Proyecto & "," & "10"
                    ElseIf Proyecto = 1 And Entidad = 52 Then
                        Proyectos = Proyecto & "," & "9"
                    ElseIf Proyecto = 2 And Entidad = 52 Then
                        Proyectos = Proyecto & "," & "13"
                    Else
                        Proyectos = CStr(Proyecto)
                    End If
                    Dim DataTableExpedientes = getExpedientes(dbmImaging, Proyectos)
                    If DataTableExpedientes.Rows.Count > 0 Then
                        Dim DataTableExportacionFolders = getExportacionFolders(dbmImaging, Proyectos)

                        Dim DataTableExportacion_Data = getExportacion_Data(dbmImaging, Proyectos)

                        Dim DataTableExportacion_Validaciones = getExportacion_Validaciones(dbmImaging, Proyectos)
                        BloqueoConcurrencia = New Object
                        BloqueoConcurrenciaProjectos = New Object
                        'Dim ProgressForm As New FormProgress
                        FileNamesCons = New List(Of String)
                        Dim DataTableexpediente = DataTableExpedientes
                        Dim OutputFolder As String = Nothing
                        OutputFolder = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"


                        Dim DatatableNombreEntidad = getEntidad()
                        'For Each rowEntidad As DataRow In DatatableNombreEntidad.Rows
                        '    NombreEntidad = CStr(rowEntidad("Nombre_Entidad"))
                        'Next
                        NombreEntidad = DatatableNombreEntidad.Rows(0)("Nombre_Entidad").ToString()

                        If Not Directory.Exists(OutputFolder & NombreEntidad) Then
                            Directory.CreateDirectory(OutputFolder & NombreEntidad)
                        End If

                        Dim tmpFolderDataTable As DataTable = Nothing
                        Dim tmpFileDataDataTable As DataTable = Nothing
                        Dim tmpFileValidacionDataTable As DataTable = Nothing
                        tmpFolderDataTable = DataTableExportacionFolders
                        tmpFileDataDataTable = DataTableExportacion_Data
                        tmpFileValidacionDataTable = DataTableExportacion_Validaciones

                        Dim Viewexpediente = New DataView(DataTableexpediente)

                        Dim ExportacionDataSetXML As New OffLineViewer.Library.xsdOffLineData
                        Dim FolderDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                        Dim FileDataTableTXT = New DataTable
                        Dim FileDataDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                        Dim FileValidacionDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable
                        Dim FolderDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                        Dim FileDataDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                        Dim FileValidacionDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable
                        Dim FileDataTable As DataTable = Nothing
                        Try
                            dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                            dbmCore.Connection_Open(1)
                            TotalRegistrosExportar = Viewexpediente.Count
                            If Viewexpediente.Count > 0 Then

                                Dim Servidor As New DataTable

                                Dim ViewServidor = Viewexpediente
                                Servidor = ViewServidor.ToTable(True, "fk_ServidorStorage")

                                Dim LogTotalesOt As String = Nothing


                                LogTotalesOt = "Se encontró : " & TotalRegistrosExportar.ToString & " Unidades Documentales "
                                Dim FileFolderName As String = Nothing
                                FileFolderName = NombreEntidad + "\" & NombreProyecto & "\"
                                FolderNameOutput = OutputFolder & "\" & FileFolderName
                                If (Not Directory.Exists(OutputFolder & FileFolderName)) Then
                                    Directory.CreateDirectory(OutputFolder & FileFolderName)
                                End If

                                log(FolderNameOutput, "Total a exportar_" + NombreProyecto, LogTotalesOt)

                                Dim Column1 As String = Nothing
                                Dim Column2 As String = Nothing
                                FolderDataTable.Columns.Add.ColumnMapping = CType(Column1, MappingType)
                                FileDataDataTable.Columns.Add.ColumnMapping = CType(Column1, MappingType)
                                FileDataDataTable.Columns.Add.ColumnMapping = CType(Column2, MappingType)
                                FileValidacionDataTable.Columns.Add.ColumnMapping = CType(Column1, MappingType)
                                FileValidacionDataTable.Columns.Add.ColumnMapping = CType(Column2, MappingType)

                                ArrayNotificacion = New ArrayList
                                Dim hilos As Integer = 0
                                For Each rowServidor As DataRow In Servidor.Rows
                                    ClearMemory()
                                    If Viewexpediente.Count > 0 Then
                                        Viewexpediente.RowFilter() = "fk_ServidorStorage = " & rowServidor.Item("fk_ServidorStorage").ToString
                                        FileDataTable = Viewexpediente.ToTable()
                                    End If
                                    Dim tmpFolderDataTables As DataTable = Nothing
                                    Dim tmpFileDataDataTables As DataTable = Nothing
                                    Dim tmpFileValidacionDataTables As DataTable = Nothing
                                    If tmpFolderDataTable.Rows.Count > 0 Then
                                        Dim CountRegistros As List(Of DataRow) = tmpFolderDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString).ToList
                                        If CountRegistros.Count > 0 Then
                                            tmpFolderDataTables = tmpFolderDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString).CopyToDataTable()
                                            TablaDesdeTemporal(FolderDataTable, tmpFolderDataTables)
                                        End If
                                    End If

                                    If tmpFileDataDataTable.Rows.Count > 0 Then
                                        Dim CountRegistros As List(Of DataRow) = tmpFileDataDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString).ToList
                                        If CountRegistros.Count > 0 Then
                                            tmpFileDataDataTables = tmpFileDataDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString).CopyToDataTable()
                                            TablaDesdeTemporal(FileDataDataTable, tmpFileDataDataTables)
                                        End If
                                    End If
                                    If tmpFileValidacionDataTable.Rows.Count > 0 Then
                                        Dim CountRegistros As List(Of DataRow) = tmpFileValidacionDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString).ToList
                                        If CountRegistros.Count > 0 Then
                                            tmpFileValidacionDataTables = tmpFileValidacionDataTable.Select("fk_ServidorStorage = " + rowServidor.Item("fk_ServidorStorage").ToString).CopyToDataTable()
                                            TablaDesdeTemporal(FileValidacionDataTable, tmpFileValidacionDataTables)
                                        End If
                                    End If

                                    If Me.TXTRadioButton.Checked Then
                                        TablaDesdeTemporal(FolderDataTableTXT, tmpFolderDataTable)
                                        FileDataTableTXT = FileDataTable
                                        TablaDesdeTemporal(FileDataDataTableTXT, tmpFileDataDataTable)
                                        TablaDesdeTemporal(FileValidacionDataTableTXT, tmpFileValidacionDataTable)
                                    End If

                                    ' Crear el directorio de las imágenes

                                    Dim Compresion As ImageManager.EnumCompression

                                    If (Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                                        Compresion = ImageManager.EnumCompression.Ccitt3
                                    Else
                                        Compresion = ImageManager.EnumCompression.Lzw
                                    End If

                                    Try
                                        Dim servidore = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, CShort(rowServidor.Item("fk_ServidorStorage")))
                                        Dim sevidores = servidore(0).ToCTA_ServidorSimpleType()
                                        Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()


                                        Dim Documentos As List(Of Object) = Nothing
                                        Documentos = (From a In FileDataTable Group a By groupDt = a.Field(Of Integer)("fk_Documento") Into Group Select Group.Select(Function(x) x("fk_Documento")).First()).ToList()
                                        Dim FilesDataView As New DataView(FileDataTable)


                                        For Each documento As Integer In Documentos
                                            FilesDataView.RowFilter = " fk_Documento = " & documento.ToString()

                                            Dim ArraListParameters As ArrayList = New ArrayList
                                            ArraListParameters.Add(ArrayNotificacion.Count)
                                            ArraListParameters.Add(FilesDataView.ToTable())
                                            ArraListParameters.Add(Compresion)
                                            ArraListParameters.Add(OutputFolder & FileFolderName)
                                            ArraListParameters.Add(FileFolderName)
                                            ArraListParameters.Add(CInt(Proyecto))
                                            ArraListParameters.Add(sevidores)
                                            ArraListParameters.Add(centro)
                                            ArraListParameters.Add(NombreProyecto)
                                            ArraListParameters.Add(OutputFolder)
                                            SyncLock BloqueoConcurrencia
                                                ArrayNotificacion.Add(False)
                                            End SyncLock


                                            Dim NewThreadProjects As New Thread(AddressOf ExportarImagen)
                                            NewThreadProjects.Start(ArraListParameters)
                                            ClearMemory()


                                        Next

                                        Dim SalirDeltaImagenComprimir As Boolean = False
                                        While SalirDeltaImagenComprimir = False
                                            SalirDeltaImagenComprimir = True
                                            SyncLock BloqueoConcurrencia
                                                For Each HiloTerminadoDeltaImagenComprimir As Boolean In ArrayNotificacion
                                                    If HiloTerminadoDeltaImagenComprimir = False Then
                                                        SalirDeltaImagenComprimir = False
                                                    End If
                                                Next
                                            End SyncLock
                                            Thread.Sleep(1000)
                                            ClearMemory()
                                            ProgressForm.Action = "Imagenes"
                                            ProgressForm.Process = "Imagenes"
                                            ProgressForm.Show()
                                            Application.DoEvents()
                                            If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                                            ProgressForm.ValueProcess = Count_Process_Delta
                                            ProgressForm.ValueAction = Count_Process_Delta
                                            ProgressForm.MaxValueProcess = TotalRegistrosExportar
                                            ProgressForm.MaxValueAction = TotalRegistrosExportar

                                        End While
                                    Catch ex As Exception
                                        SyncLock BloqueoConcurrencia
                                            log(FolderNameOutput, "Errores" + NombreProyecto, "Error Hilos: " + ex.Message)
                                        End SyncLock
                                    End Try
                                    SyncLock BloqueoConcurrencia
                                        '------------  Si proyecto tiene configurado Exportar_Unico_Archivo_TIFF  ------------------
                                        If (CBool(Program.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF)) Then
                                            ExportAllFillesInTiff(Compresion, FolderNameOutput)
                                        End If
                                        '--------------------------------
                                        If (Me.VisorRadioButton.Checked) Then
                                            GenerarVisor(dbmCore, dbmImaging, FolderNameOutput, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable, CInt(Proyecto), NombreProyecto, CInt(rowServidor.Item("fk_ServidorStorage")))
                                        ElseIf (Me.XMLRadioButton.Checked) Then
                                            Dim generar As Boolean = False
                                            If rowServidor.Equals(FileDataTable.Rows(FileDataTable.Rows.Count - 1)) Then
                                                generar = True
                                            End If
                                            GenerarXMLExpedientes(generar, dbmCore, dbmImaging, FolderNameOutput, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable, ExportacionDataSetXML, CInt(Proyecto), NombreProyecto)
                                        ElseIf rowServidor.Equals(FileDataTable.Rows(FileDataTable.Rows.Count - 1)) Then
                                            GenerarTXT(FolderNameOutput, FolderDataTableTXT, FileDataTableTXT, FileDataDataTableTXT, FileValidacionDataTableTXT)
                                        End If

                                    End SyncLock

                                Next
                            End If
                        Catch ex As Exception
                            SyncLock BloqueoConcurrencia
                                'PictureBoxGit.Visible = False
                                log(FolderNameOutput, "Errores_" + NombreProyecto, "Error exportar proyecto: " + ex.Message)
                            End SyncLock
                        Finally
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                            'PictureBoxGit.Visible = False
                            BorrarTemporal()
                        End Try
                        MessageBox.Show("La información se exportó con éxito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("No se encontró expedientes", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If
            Catch ex As Exception
                ProgressForm.Close()
                ProgressForm.Dispose()
                SyncLock BloqueoConcurrencia
                    'PictureBoxGit.Visible = False
                    log(FolderNameOutput, "Errores_" + NombreProyecto, ex.Message)
                End SyncLock
            End Try

        End Sub
        Private Declare Auto Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal procHandle As IntPtr, ByVal min As Int32, ByVal max As Int32) As Boolean
        Public Sub ClearMemory()

            Try
                Dim Mem As Process
                Mem = Process.GetCurrentProcess()
                SetProcessWorkingSetSize(Mem.Handle, -1, -1)
            Catch ex As Exception
                'Control de errores
            End Try
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
        Private Sub ExportarImagen(ByVal objectArray As Object)

            Dim ArraListParameterImagen As ArrayList = CType(objectArray, ArrayList)
            Dim manager As FileProviderManager = Nothing
            Dim Hilo_Imagen As Integer = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim FileDataTable As DataTable = Nothing
            Dim nCompresion As ImageManager.EnumCompression = Nothing
            Dim nFileFolderName As String = Nothing
            Dim nFolderName As String = Nothing
            Dim nProyecto As Integer = Nothing
            Dim fk_Expediente As Integer = Nothing
            Dim fk_Documento As Integer = Nothing
            Dim fk_Folder As Short = Nothing
            Dim fk_File As Short = Nothing
            Dim id_Version As Short = Nothing
            Dim servidor As DBImaging.SchemaCore.CTA_ServidorSimpleType
            Dim centro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType
            Dim Folios As Short = Nothing
            Dim NombreProyecto As String
            Dim OutputFolder As String
            Hilo_Imagen = CInt(ArraListParameterImagen(0))
            FileDataTable = CType(ArraListParameterImagen(1), DataTable)
            nCompresion = CType(ArraListParameterImagen(2), ImageManager.EnumCompression)
            nFileFolderName = CStr(ArraListParameterImagen(3))
            nFolderName = CStr(ArraListParameterImagen(4))
            nProyecto = CInt(ArraListParameterImagen(5))
            servidor = CType(ArraListParameterImagen(6), DBImaging.SchemaCore.CTA_ServidorSimpleType)
            centro = CType(ArraListParameterImagen(7), DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType)
            NombreProyecto = CStr(ArraListParameterImagen(8))
            OutputFolder = CStr(ArraListParameterImagen(9))


            Dim fileExpedientes As DataView = Nothing
            Try

                fileExpedientes = New DataView(FileDataTable)

                For Each ItemFile As DataRowView In fileExpedientes
                    ClearMemory()
                    fk_Expediente = CInt(CLng(ItemFile.Item("fk_Expediente")))
                    fk_Documento = CInt(ItemFile.Item("fk_Documento"))
                    fk_Folder = CShort(ItemFile.Item("fk_Folder"))
                    fk_File = CShort(ItemFile.Item("fk_File"))
                    id_Version = CShort(ItemFile.Item("id_Version"))
                    Dim Llave1 = ItemFile.Item("Llave_01")
                    Dim Llave2 = ItemFile.Item("Llave_02")
                    Dim Tipologia = ItemFile.Item("fk_Tipologia")


                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(1)

                    manager = New FileProviderManager(servidor, centro, dbmImaging, Program.Sesion.Usuario.id)
                    manager.Connect()

                    Folios = manager.GetFolios(fk_Expediente, fk_Folder, fk_File, id_Version)

                    If Folios > 0 Then
                        totalFolios += Folios
                        Dim FileNames As New List(Of String)
                        Dim FileName As String = Nothing
                        Dim FileNameAux As String = Nothing
                        Dim ExtensionAux As String = String.Empty
                        Try
                            For folio As Short = 1 To Folios
                                Dim bmp As System.Drawing.Image = Nothing
                                Dim Imagen() As Byte = Nothing
                                Dim Thumbnail() As Byte = Nothing

                                manager.GetFolio(fk_Expediente, fk_Folder, fk_File, id_Version, folio, Imagen, Thumbnail)
                                bmp = Bitmap.FromStream(New MemoryStream(Imagen))
                                FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                                FileNames.Add(FileName)
                                FileNamesCons.Add(FileName)



                                Dim qualityParam As EncoderParameter = New EncoderParameter(Encoder.Quality, 20)
                                Dim jpegCodec As ImageCodecInfo = GetEncoderInfo("image/jpeg")
                                Dim encoderParams As EncoderParameters = New EncoderParameters(1)
                                encoderParams.Param(0) = qualityParam
                                bmp.Save(FileName, jpegCodec, encoderParams)
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

                                    ExtensionAux = IIf(formatoAux = ImageManager.EnumFormat.Pdf, ".pdf", Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida).ToString
                                    If IsDBNull(Llave1) Then
                                        Llave1 = ""
                                    End If
                                    If IsDBNull(Llave2) Then
                                        Llave2 = ""
                                    End If
                                    If IsDBNull(Tipologia) Then
                                        Tipologia = ""
                                    End If
                                    If ((Valido = True) And (FileNameAux = String.Empty)) Then
                                        FileNameAux = CStr(ItemFile.Item("Nombre_Imagen_File"))
                                        FileName = nFileFolderName & FileNameAux & ExtensionAux
                                        'If Not String.IsNullOrEmpty(CStr(Llave1)) And String.IsNullOrEmpty(CStr(Llave2)) And Not String.IsNullOrEmpty(CStr(Tipologia)) Then
                                        '    FileNameAux = CStr(Llave1) & "_" & CStr(Tipologia) & "_" & fk_File
                                        '    FileName = nFileFolderName & FileNameAux & ExtensionAux
                                        'End If
                                        'If Not String.IsNullOrEmpty(CStr(Llave1)) And Not String.IsNullOrEmpty(CStr(Llave2)) And Not String.IsNullOrEmpty(CStr(Tipologia)) Then
                                        '    FileNameAux = CStr(Llave1) & "_" & CStr(Llave2) & "_" & CStr(Tipologia) & "_" & fk_File
                                        '    FileName = nFileFolderName & FileNameAux & ExtensionAux
                                        'End If
                                        'If String.IsNullOrEmpty(CStr(Llave1)) And Not String.IsNullOrEmpty(CStr(Llave2)) And Not String.IsNullOrEmpty(CStr(Tipologia)) Then
                                        '    FileNameAux = CStr(Llave2) & "_" & CStr(Tipologia) & "_" & fk_File
                                        '    FileName = nFileFolderName & FileNameAux & ExtensionAux
                                        'End If
                                    ElseIf ((Valido = True) And (FileNameAux <> String.Empty)) Then
                                        ExtensionAux = Convert.ToString(IIf(ExtensionAux Is String.Empty, Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, ExtensionAux))
                                        FileName = nFileFolderName & FileNameAux & ExtensionAux

                                    ElseIf Valido = False Then
                                        Throw New Exception(MsgError)
                                    End If

                                    ImageManager.Save(FileNames, FileName, "", formatoAux, nCompresion, False, Program.AppPath & Program.TempPath, True)


                                    ItemFile.Item("Nombre_Imagen_File") = nFolderName & FileNameAux & ExtensionAux

                                    For Each Item As DataRow In FileDataTable.Select("fk_Expediente = " & ItemFile.Item("fk_Expediente").ToString & " AND fk_Folder = " & ItemFile.Item("fk_Folder").ToString & " AND fk_File = " & ItemFile.Item("fk_File").ToString).ToList()
                                        If CLng(Item.Item("fk_Expediente")) = CDbl(ItemFile.Item("fk_Expediente")) And CShort(Item.Item("fk_Folder")) = CShort(ItemFile.Item("fk_Folder")) And CShort(Item.Item("fk_File")) = CShort(ItemFile.Item("fk_File")) Then
                                            Item.Item("Nombre_Imagen_File") = ItemFile.Item("Nombre_Imagen_File")
                                        End If
                                    Next
                                    FileDataTable.Dispose()
                                    fileExpedientes.Dispose()
                                End If
                            End If
                            SyncLock BloqueoConcurrencia
                                Dim LogTotalExpedientes = CStr(fk_Expediente) + "," + CStr(fk_Folder) + "," + CStr(fk_File) + "," + CStr(id_Version)
                                logExpedientes(OutputFolder, "ExpedientesExportados", LogTotalExpedientes)
                            End SyncLock
                            ClearMemory()
                        Catch ex As Exception
                            ClearMemory()
                            SyncLock BloqueoConcurrencia
                                log(FolderNameOutput, "Errores_" + NombreProyecto, "Error al exportar Expediente: " + ex.Message)
                            End SyncLock
                        End Try
                    Else
                        SyncLock BloqueoConcurrencia
                            Dim LogNoEncontroFolios = "No se encontró : " & Folios & " Folios, " &
                                                     fk_Expediente & " Expediente  " &
                                                     fk_Folder & " fk_Folder  " &
                                                     fk_File & " fk_File  " &
                                                     id_Version & " id_Version  "
                            log(FolderNameOutput, "Expedientes no encontrados_" + NombreProyecto, LogNoEncontroFolios)
                        End SyncLock
                    End If
                    If (manager IsNot Nothing) Then manager.Disconnect()
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    Count_Process_Delta += 1
                Next
                SyncLock BloqueoConcurrencia
                    Dim LogFolios = "Se encontró : " & totalFolios & " Folios "
                    log(FolderNameOutput, "Total Folios Encontrados_" + NombreProyecto, LogFolios)
                End SyncLock
            Catch ex As Exception
                log(FolderNameOutput, "Errores_" + NombreProyecto, "Error al exportar : " + ex.Message)
            Finally
                FileDataTable.Dispose()
                If (manager IsNot Nothing) Then manager.Disconnect()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                SyncLock BloqueoConcurrencia
                    ArrayNotificacion(Hilo_Imagen) = True
                End SyncLock
            End Try
        End Sub
        Public Shared Function ImageToByte(ByVal img As Image) As Byte()
            Dim converter As ImageConverter = New ImageConverter()
            Return CType(converter.ConvertTo(img, GetType(Byte())), Byte())
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
        Private Shared Function GetEncoderInfo(ByVal mimeType As String) As ImageCodecInfo
            Dim encoders As ImageCodecInfo()
            encoders = ImageCodecInfo.GetImageEncoders()

            For Each encoder In encoders
                If encoder.MimeType = mimeType Then Return encoder
            Next
            Return Nothing
        End Function
        Private Sub GenerarVisor(dbmCore As DBCore.DBCoreDataBaseManager, dbmImaging As DBImaging.DBImagingDataBaseManager, OutputFolder As String, FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable, FileDataTable As DataTable, FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable, FileValidacionesDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable, ByVal nProyecto As Integer, ByVal nNombreProyecto As String, ByVal fk_Servidor As Integer)
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

                SyncLock BloqueoConcurrencia
                    Conexion = New OleDb.OleDbConnection(ConnectionString)
                    Conexion.Open()
                End SyncLock

                Dim Comando = New OleDb.OleDbCommand("", Conexion)
                Dim Dtresultados As New DataTable
                Dim adapter As OleDb.OleDbDataAdapter

                'Entidades
                Dim EntidadProyecto = (From a In FileDataTable
                              Group By x = a.Field(Of Short)("fk_Entidad"), y = a.Field(Of Short)("fk_Proyecto")
                              Into Group
                              Select New With {.x = x, .y = y}).ToList()

                For Each EntidadProyectoRow In EntidadProyecto
                    Dim KeysDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(CType(EntidadProyectoRow.x, Global.Slyg.Tools.SlygNullable(Of Short)), CType(EntidadProyectoRow.y, Global.Slyg.Tools.SlygNullable(Of Short)))

                    Dim KeyName1 As String = ""
                    If (KeysDataTable.Count > 0) Then KeyName1 = KeysDataTable(0).Nombre_Proyecto_Llave
                    Dim KeyName2 As String = ""
                    If (KeysDataTable.Count > 1) Then KeyName2 = KeysDataTable(1).Nombre_Proyecto_Llave
                    Dim KeyName3 As String = ""
                    If (KeysDataTable.Count > 2) Then KeyName3 = KeysDataTable(2).Nombre_Proyecto_Llave

                    ' Crear Configuracion
                    Dtresultados = New DataTable

                    Comando.CommandText = "SELECT * FROM TBL_Config WHERE id_Entidad = " & EntidadProyectoRow.x &
                                            " AND id_Proyecto = " & EntidadProyectoRow.y & ";"

                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Dim EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(CType(EntidadProyectoRow.x, Global.Slyg.Tools.SlygNullable(Of Short)))
                        Dim ProyectoDataTable = dbmCore.SchemaConfig.TBL_Proyecto.DBFindByfk_Entidadid_Proyecto(Entidad, CType(EntidadProyectoRow.y, Global.Slyg.Tools.SlygNullable(Of Short)))
                        Comando.CommandText = " INSERT INTO TBL_Config (id_Entidad, Nombre_Entidad, id_Proyecto, Nombre_Proyecto, Key_1, Key_2, Key_3)" &
                                                "SELECT " & EntidadProyectoRow.x &
                                                ", '" & EntidadDataTable(0).Nombre_Entidad & "', " &
                                                EntidadProyectoRow.y &
                                                ", '" & ProyectoDataTable(0).Nombre_Proyecto &
                                                "', '" & KeyName1 &
                                                "', '" & KeyName2 &
                                                "', '" & KeyName3 & "';"

                        Comando.ExecuteNonQuery()
                    End If
                Next



                ' Crear los Esquemas
                Dim EsquemasDataTable = getExportacion_Esquema()
                ' Dim EsquemasDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Esquema.DBExecute(idOT)
                For Each EsquemaRow As DataRow In EsquemasDataTable.Rows

                    Comando.CommandText = "SELECT * FROM TBL_Esquema WHERE id_Esquema = " & EsquemaRow.Item("id_Esquema").ToString & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Esquema (id_Esquema, Nombre_Esquema)" &
                                            "SELECT " & EsquemaRow.Item("id_Esquema").ToString &
                                            ", '" & EsquemaRow.Item("Nombre_Esquema").ToString & "';"

                        Comando.ExecuteNonQuery()
                    End If
                Next

                ' Crear los Documentos
                Dim DocumentosDataTable = getExportacion_Documento()
                'Dim DocumentosDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Documento.DBExecute(idOT)
                For Each DocumentoRow As DataRow In DocumentosDataTable.Rows

                    Comando.CommandText = "SELECT * FROM TBL_Documento WHERE id_Documento = " & DocumentoRow.Item("id_Documento").ToString & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)
                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Documento (id_Documento, Nombre_Documento)" &
                                            "SELECT " & DocumentoRow.Item("id_Documento").ToString &
                                            ", '" & DocumentoRow.Item("Nombre_Documento").ToString & "';"

                        Comando.ExecuteNonQuery()
                    End If

                Next

                ' Crear Campos de Búsqueda
                Dim CampoBusquedaDataTable = getExportacion_Campo_Busqueda(nProyecto)
                'Dim CampoBusquedaDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo_Busqueda.DBExecute(idOT)
                For Each CampoBusquedaRow As DataRow In CampoBusquedaDataTable.Rows

                    Comando.CommandText = "SELECT * FROM TBL_Campo_Busqueda WHERE" &
                                        " fk_Campo_Tipo = " & CampoBusquedaRow.Item("fk_Campo_Tipo").ToString &
                                        " AND id_Campo_Busqueda = " & CampoBusquedaRow.Item("id_Campo_Busqueda").ToString & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Campo_Busqueda (fk_Campo_Tipo, id_Campo_Busqueda, Nombre_Campo_Busqueda)" &
                                            "SELECT " & CampoBusquedaRow.Item("fk_Campo_Tipo").ToString &
                                            ", " & CampoBusquedaRow.Item("id_Campo_Busqueda").ToString &
                                            ", '" & CampoBusquedaRow.Item("Nombre_Campo_Busqueda").ToString & "';"

                        Comando.ExecuteNonQuery()

                    End If

                Next

                ' Crear los Campos
                Dim CamposDataTable = getExportacion_Campo()
                ' Dim CamposDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo.DBExecute(idOT)
                For Each CampoRow As DataRow In CamposDataTable.Rows

                    Comando.CommandText = "SELECT * FROM TBL_Campo WHERE" &
                                        " fk_Documento = " & CampoRow.Item("fk_Documento").ToString &
                                        " AND id_Campo = " & CampoRow.Item("id_Campo").ToString & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Campo (fk_Documento, id_Campo, Nombre_Campo, Es_Campo_Busqueda, fk_Campo_Tipo, fk_Campo_Busqueda)" &
                                            "SELECT " & CampoRow.Item("fk_Documento").ToString &
                                            ", " & CampoRow.Item("id_Campo").ToString &
                                            ", '" & CampoRow.Item("Nombre_Campo").ToString & "'" &
                                            ", " & IIf(CBool(CampoRow.Item("Es_Campo_Busqueda").ToString), "1", "0").ToString() &
                                            ", " & CampoRow.Item("fk_Campo_Tipo").ToString &
                                            ", " & CampoRow.Item("fk_Campo_Busqueda").ToString & ";"

                        Comando.ExecuteNonQuery()
                    End If
                Next

                ' Crear las Validaciones
                Dim ValidacionDataTable = getExportacion_Validacion()
                ' Dim ValidacionDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Validacion.DBExecute(idOT)
                For Each ValidacionRow As DataRow In ValidacionDataTable.Rows

                    Comando.CommandText = "SELECT * FROM TBL_Validacion WHERE" &
                                        " fk_Documento = " & ValidacionRow.Item("fk_Documento").ToString &
                                        " AND id_Validacion = " & ValidacionRow.Item("id_Validacion").ToString & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Validacion (fk_Documento, id_Validacion, Pregunta_Validacion)" &
                                            "SELECT " & ValidacionRow.Item("fk_Documento").ToString &
                                            ", " & ValidacionRow.Item("id_Validacion").ToString &
                                            ", '" & ValidacionRow.Item("Pregunta_Validacion").ToString & "';"

                        Comando.ExecuteNonQuery()
                    End If
                Next

                ' Crear Folders            
                For Each FolderRow In FolderDataTable
                    Comando.CommandText = "SELECT * FROM TBL_Folder WHERE " &
                                      " fk_Expediente = " & FolderRow.fk_Expediente &
                                      " AND id_Folder = " & FolderRow.fk_Folder &
                                      " AND fk_Esquema = " & FolderRow.id_Esquema &
                                      " AND CBarras_Folder = " & "'" & FolderRow.CBarras_Folder & "'" & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Folder (fk_Expediente, id_Folder, fk_Esquema, Key_1, Key_2, Key_3, CBarras_Folder)" &
                                            "SELECT " & FolderRow.fk_Expediente &
                                            ", " & FolderRow.fk_Folder &
                                            ", " & FolderRow.id_Esquema &
                                            ", '" & FolderRow.Key_1 & "'" &
                                            ", '" & FolderRow.Key_2 & "'" &
                                            ", '" & FolderRow.Key_3 & "'" &
                                            ", '" & FolderRow.CBarras_Folder & "';"

                        Comando.ExecuteNonQuery()
                    End If
                Next

                ' Crear Files            
                For Each FileRow As DataRow In FileDataTable.Rows



                    Comando.CommandText = "SELECT * FROM TBL_File WHERE" &
                                 " fk_Expediente = " & FileRow.Item("fk_Expediente").ToString() &
                                 " AND fk_Folder = " & FileRow.Item("fk_Folder").ToString() &
                                 " AND id_File = " & FileRow.Item("fk_File").ToString &
                                 " AND id_Version = " & FileRow.Item("id_Version").ToString &
                                 " AND File_Unique_Identifier = " & "'" & FileRow.Item("File_Unique_Identifier").ToString & "'" &
                                 " AND fk_Documento = " & FileRow.Item("fk_Documento").ToString &
                                 " AND Nombre_Imagen_File = " & "'" & FileRow.Item("Nombre_Imagen_File").ToString & "'" &
                                 " AND Folios_Documento_File = " & FileRow.Item("Folios_Documento_File").ToString & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_File (fk_Expediente, fk_Folder, id_File, id_Version, File_Unique_Identifier, fk_Documento, Nombre_Imagen_File, Folios_Documento_File, Tamaño_Imagen_File)" &
                                            "SELECT " & FileRow.Item("fk_Expediente").ToString() &
                                            ", " & FileRow.Item("fk_Folder").ToString() &
                                            ", " & FileRow.Item("fk_File").ToString &
                                            ", " & FileRow.Item("id_Version").ToString &
                                            ", '" & FileRow.Item("File_Unique_Identifier").ToString() & "'" &
                                            ", " & FileRow.Item("fk_Documento").ToString &
                                            ", '" & FileRow.Item("Nombre_Imagen_File").ToString & "'" &
                                            ", " & FileRow.Item("Folios_Documento_File").ToString &
                                            ", " & FileRow.Item("Tamaño_Imagen_File").ToString & ";"

                        Comando.ExecuteNonQuery()
                    End If
                Next

                ' Crear File Data            
                For Each DataRow In FileDataDataTable
                    'Dim valor As String = ""
                    'If (Not DataRow.IsNull("Valor_File_Data")) Then valor = DataRow.Valor_File_Data

                    Comando.CommandText = "SELECT * FROM TBL_File_Data WHERE" &
                                   " fk_Expediente = " & DataRow.fk_Expediente &
                                   " AND fk_Folder = " & DataRow.fk_Folder &
                                   " AND fk_File = " & DataRow.fk_File &
                                   " AND fk_Version = " & DataRow.id_Version &
                                   " AND fk_Campo = " & DataRow.id_Campo &
                                   " AND fk_Documento = " & DataRow.fk_Documento &
                                   " AND Valor_File_Data = " & "'" & DataRow.Valor_File_Data & "'" & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
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
                    End If
                Next

                ' Crear File Validacion            
                For Each DataRow In FileValidacionesDataTable

                    Comando.CommandText = "SELECT * FROM TBL_File_Validacion WHERE" &
                                  " fk_Expediente = " & DataRow.fk_Expediente &
                                  " AND fk_Folder = " & DataRow.fk_Folder &
                                  " AND fk_File = " & DataRow.fk_File &
                                  " AND fk_Version = " & DataRow.id_Version &
                                  " AND fk_Validacion = " & DataRow.id_Validacion &
                                  " AND fk_Documento = " & DataRow.fk_Documento & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then

                        Comando.CommandText = "INSERT INTO TBL_File_Validacion (fk_Expediente, fk_Folder, fk_File, fk_Version, fk_Validacion, fk_Documento, Respuesta)" &
                                            "SELECT " & DataRow.fk_Expediente &
                                            ", " & DataRow.fk_Folder &
                                            ", " & DataRow.fk_File &
                                            ", " & DataRow.id_Version &
                                            ", " & DataRow.id_Validacion &
                                            ", " & DataRow.fk_Documento &
                                            ", " & IIf(DataRow.Respuesta, "1", "0").ToString() & ";"

                        Comando.ExecuteNonQuery()
                    End If
                Next
            Catch ex As Exception
                SyncLock BloqueoConcurrencia
                    log(FolderNameOutput, "Errores" + nNombreProyecto, "Error Visor" + ex.Message)
                End SyncLock
            Finally
                SyncLock BloqueoConcurrencia
                    If (Conexion IsNot Nothing) Then Conexion.Close()
                End SyncLock
            End Try
        End Sub
        Private Sub GenerarXML(dbmCore As DBCore.DBCoreDataBaseManager, dbmImaging As DBImaging.DBImagingDataBaseManager, idOT As Integer, OutputFolder As String, FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable, FileDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable, FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable, FileValidacionDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable, ByVal nProyecto As Integer, ByVal nNombreProyecto As String)
            Dim ExportacionDataSet As New OffLineViewer.Library.xsdOffLineData

            ' Llaves
            Dim KeysDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(Entidad, CType(nProyecto, Global.Slyg.Tools.SlygNullable(Of Short)))

            Dim KeyName1 As String = ""
            If (KeysDataTable.Count > 0) Then KeyName1 = KeysDataTable(0).Nombre_Proyecto_Llave
            Dim KeyName2 As String = ""
            If (KeysDataTable.Count > 1) Then KeyName2 = KeysDataTable(1).Nombre_Proyecto_Llave
            Dim KeyName3 As String = ""
            If (KeysDataTable.Count > 2) Then KeyName3 = KeysDataTable(2).Nombre_Proyecto_Llave

            ' Configuración
            Dim EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(Entidad)
            ExportacionDataSet.TBL_Config.AddTBL_ConfigRow(Entidad, EntidadDataTable(0).Nombre_Entidad, CShort(nProyecto), nNombreProyecto, KeyName1, KeyName2, KeyName3)

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
                                                OutputFolder As String, _
                                                FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable, _
                                                FileDataTable As DataTable, _
                                                FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable, _
                                                FileValidacionDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable, _
                                                ExportacionDataSet As OffLineViewer.Library.xsdOffLineData, ByVal nProyecto As Integer, ByVal nNombreProyecto As String)
            'Dim ExportacionDataSet As New OffLineViewer.Library.xsdOffLineData

            ' Llaves
            Dim KeysDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(Entidad, CType(nProyecto, Global.Slyg.Tools.SlygNullable(Of Short)))

            Dim KeyName1 As String = ""
            If (KeysDataTable.Count > 0) Then KeyName1 = KeysDataTable(0).Nombre_Proyecto_Llave
            Dim KeyName2 As String = ""
            If (KeysDataTable.Count > 1) Then KeyName2 = KeysDataTable(1).Nombre_Proyecto_Llave
            Dim KeyName3 As String = ""
            If (KeysDataTable.Count > 2) Then KeyName3 = KeysDataTable(2).Nombre_Proyecto_Llave

            ' Configuración
            If ExportacionDataSet.TBL_Config.Select("id_Entidad = " & Entidad & "and  id_Proyecto = " & nProyecto).Length = 0 Then
                Dim EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(Entidad)
                ExportacionDataSet.TBL_Config.AddTBL_ConfigRow(Entidad, EntidadDataTable(0).Nombre_Entidad, CShort(nProyecto), nNombreProyecto, KeyName1, KeyName2, KeyName3)
            End If

            ' Folder
            For Each FolderRow In FolderDataTable
                ExportacionDataSet.TBL_Folder.AddTBL_FolderRow(FolderRow.fk_Expediente, FolderRow.fk_Folder, FolderRow.id_Esquema, FolderRow.Nombre_Esquema, FolderRow.Key_1, FolderRow.Key_2, FolderRow.Key_3, FolderRow.CBarras_Folder)
            Next

            ' File
            For Each FileRow As DataRow In FileDataTable.Rows
                ExportacionDataSet.TBL_File.AddTBL_FileRow(CLng(FileRow.Item("fk_Expediente")), CLng(FileRow.Item("fk_Folder")), CShort(FileRow.Item("fk_File")), CShort(FileRow.Item("id_Version")), CType(FileRow.Item("File_Unique_Identifier"), Guid), FileRow.Item("Nombre_Documento").ToString, FileRow.Item("Nombre_Imagen_File").ToString, CShort(FileRow.Item("Folios_Documento_File")), CLng(FileRow.Item("Tamaño_Imagen_File")))
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
            Dim CampoBusquedaDataTable = getExportacion_Campo_Busqueda(nProyecto)
            'Dim CampoBusquedaDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo_Busqueda.DBExecute(idOT)
            For Each CampoBusquedaRow As DataRow In CampoBusquedaDataTable.Rows
                If ExportacionDataSet.TBL_Campo_Busqueda.Select("fk_Campo_Tipo = " & CampoBusquedaRow.Item("fk_Campo_Tipo").ToString & "and  id_Campo_Busqueda = " & CampoBusquedaRow.Item("id_Campo_Busqueda").ToString).Length = 0 Then
                    ExportacionDataSet.TBL_Campo_Busqueda.AddTBL_Campo_BusquedaRow(CByte(CampoBusquedaRow.Item("fk_Campo_Tipo")), CShort(CampoBusquedaRow.Item("id_Campo_Busqueda")), CampoBusquedaRow.Item("Nombre_Campo_Busqueda").ToString)
                End If
            Next



            If Generar Then
                If Not File.Exists(OutputFolder & "\" & "ExportedData.xml") Then
                    ExportacionDataSet.WriteXml(OutputFolder & "\" & "ExportedData.xml")
                End If

            End If

        End Sub
        Private Sub GenerarTXT(ByVal OutputFolder As String, ByVal FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable, ByVal FileDataTable As DataTable, ByVal FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable, ByVal FileValidacionDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable)
            Dim CSVData = New CSV.CSVData(vbTab, """", True)

            CSVData.SaveAsCSV(New CSV.CSVTable(FolderDataTable), OutputFolder & "Folders.txt", False)
            CSVData.SaveAsCSV(New CSV.CSVTable(FileDataTable), OutputFolder & "Files.txt", False)
            CSVData.SaveAsCSV(New CSV.CSVTable(FileDataDataTable), OutputFolder & "Data.txt", False)
            CSVData.SaveAsCSV(New CSV.CSVTable(FileValidacionDataTable), OutputFolder & "Validaciones.txt", False)
        End Sub
        Public Shared Sub log(ByVal logFileDirectory As String, ByVal filenamePrefix As String, ByVal message As String)

            If Not Directory.Exists(logFileDirectory) Then
                Directory.CreateDirectory(logFileDirectory)
            End If

            Dim filepath As String = logFileDirectory & filenamePrefix & "_" + DateTime.Now.Date.ToShortDateString().Replace("/"c, "_"c) & ".txt"
            Dim logMessage As String = message

            If Not File.Exists(filepath) Then

                Using sw As StreamWriter = File.CreateText(filepath)
                    sw.WriteLine(logMessage)
                End Using
            Else

                Using sw As StreamWriter = File.AppendText(filepath)
                    sw.WriteLine(logMessage)
                End Using
            End If
        End Sub
        Public Shared Sub logExpedientes(ByVal logFileDirectory As String, ByVal filenamePrefix As String, ByVal message As String)

            If Not Directory.Exists(logFileDirectory) Then
                Directory.CreateDirectory(logFileDirectory)
            End If

            Dim filepath As String = logFileDirectory & filenamePrefix & ".txt"
            Dim logMessage As String = message

            If Not File.Exists(filepath) Then

                Using sw As StreamWriter = File.CreateText(filepath)
                    sw.WriteLine(logMessage)
                End Using
            Else

                Using sw As StreamWriter = File.AppendText(filepath)
                    sw.WriteLine(logMessage)
                End Using
            End If
        End Sub
        Private Sub MostrarDatagrid()
            OTLabel.Text = "Proyectos"
            ExpedientesDataGridView.Visible = True
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

            If Not validarFechaProceso() Then
                Return False
            End If

            If (Not Directory.Exists(CarpetaSalidaTextBox.Text)) Then
                MessageBox.Show("El directorio no existe, Seleccione un directorio existente", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()

            ElseIf (Directory.GetDirectories(CarpetaSalidaTextBox.Text).Length > 0 Or Directory.GetFiles(CarpetaSalidaTextBox.Text).Length > 0) Then
                MessageBox.Show("La carpeta debe estar vacia para exportar los datos", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()
            Else
                Return True
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
        Private Function Exportacion_Files_Expediente_Llave(ByVal fk_Expediente As Integer) As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim dv As DataView = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = "  SELECT	Cargue.fk_OT AS id_OT, Files.fk_Expediente, Files.fk_Folder, Files.fk_File, Files.id_Version, Files.File_Unique_Identifier, Documento.Nombre_Documento, Folder.fk_Entidad_Servidor, Folder.fk_Servidor, " +
                    " Files.Nombre_Imagen_File, " +
                    " Files.Folios_Documento_File, " +
                    " Files.Tamaño_Imagen_File, " +
                    " ProcessFile.fk_Documento, " +
                    " ISNULL(Doc_Agrupador.fk_Grupo,0) AS fk_Grupo, " +
                    " LLAVE.Llave_01 AS Llave_01, " +
                    " LLAVE.Llave_02 AS Llave_02, " +
                    " Documento.fk_Tipologia " +
                    " FROM Process.TBL_Cargue AS Cargue " +
                 "INNER JOIN	Core.CTA_Folder AS Folder " +
                   " ON	Cargue.id_Cargue = Folder.fk_Cargue " +
                 " INNER JOIN	Core.CTA_File AS Files " +
                  " ON	Folder.fk_Expediente = Files.fk_Expediente " +
                   " AND Folder.fk_Folder = Files.fk_Folder  " +
                 " INNER JOIN	Core.CTA_Process_File AS ProcessFile  " +
                  "  ON	Files.fk_Expediente = ProcessFile.fk_Expediente " +
                  " AND Files.fk_Folder = ProcessFile.fk_Folder  " +
                  " AND Files.fk_File = ProcessFile.id_File	" +
                " INNER JOIN	Core.CTA_Documento AS Documento " +
                   " ON	ProcessFile.fk_Documento = Documento.id_Documento	" +
                "  LEFT JOIN	Config.TBL_Documentos_Agrupar_Exportacion_Imagenes Doc_Agrupador " +
                   " ON	ProcessFile.fk_Documento = Doc_Agrupador.fk_Documento	" +
                 " INNER JOIN  [DB_Miharu.Core].[Process].[TBL_Expediente_Llave_Linea] LLAVE " +
                "   ON  LLAVE.fk_Expediente = Folder.fk_Expediente	" +
                " WHERE  (Folder.fk_Expediente = @fk_Expediente) "
                Dim SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@fk_Expediente", fk_Expediente)
                }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function getProyectos() As DataTable
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim dv As DataView = Nothing
            Dim dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)
            Try
                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmCore.DataBase.ConnectionString)
                Dim sqlquery As String = "  select id_Proyecto, Nombre_Proyecto from [DB_Miharu.Imaging_Core].[Config].[TBL_Proyecto]  IMC INNER JOIN  [DB_Miharu.Core].[Config].[TBL_Proyecto] IC ON  IC.id_Proyecto = IMC.fk_Proyecto  WHERE IC.fk_entidad = @fk_entidad AND Aplica_imagen = @Aplica_imagen AND Aplica_Fisico = @Aplica_Fisico group by id_Proyecto, Nombre_Proyecto;"
                Dim SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@fk_entidad", Entidad),
                    New SqlParameter("@Aplica_imagen", "True"),
                    New SqlParameter("@Aplica_Fisico", "True")
                }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function getllaves(ByVal nProyecto As Integer, ByVal nExpediente As Integer) As DataTable
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim dv As DataView = Nothing
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmCore.DataBase.ConnectionString)
                Dim sqlquery As String = "  SELECT [Column1],[Column2] FROM [DB_Miharu.Core].[Process].[TBL_Expediente_Llave_Linea] UNLOCK  WHERE fk_entidad = @fk_entidad AND fk_Proyecto = @fk_Proyecto AND fk_Expediente = @fk_Expediente;"
                Dim SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@fk_entidad", Entidad),
                    New SqlParameter("@fk_Proyecto", nProyecto),
                    New SqlParameter("@fk_Expediente", nExpediente)
                }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function getServidor(ByVal nfk_Entidad As Integer, ByVal nId_Servidor As Integer) As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim dv As DataView = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = " 	SELECT	Servidor.* " +
                             " FROM		Core.CTA_Servidor AS Servidor	 " +
                             "INNER JOIN	(SELECT DISTINCT " +
                                          "  Folder.fk_Entidad_Servidor, " +
                                           " Folder.fk_Servidor " +
                                " FROM		Process.TBL_Cargue AS Cargue " +
                                "INNER JOIN	Core.CTA_Folder AS Folder 			 " +
                                 "	ON	Cargue.id_Cargue = Folder.fk_Cargue " +
                               "	) AS Folder " +
                               "ON	Folder.fk_Entidad_Servidor = Servidor.fk_Entidad AND  " +
                                        "    Folder.fk_Servidor = Servidor.id_Servidor " +
                             " WHERE (Servidor.fk_entidad = @fk_entidad) " +
                                   " AND Servidor.id_Servidor = @Id_Servidor"

                Dim SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@fk_entidad", nfk_Entidad),
                    New SqlParameter("@Id_Servidor", nId_Servidor)
                }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function Exportacion_Files_OT_Llave(ByVal id_OT As Long) As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim dv As DataView = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = "SELECT Cargue.fk_OT AS id_OT, Files.fk_Expediente, Files.fk_Folder, Files.fk_File, Files.id_Version, Files.File_Unique_Identifier, " +
    "	Documento.Nombre_Documento, Folder.fk_Entidad_Servidor, Folder.fk_Servidor, Files.Nombre_Imagen_File, Files.Folios_Documento_File, Files.Tamaño_Imagen_File, " +
    "	ProcessFile.fk_Documento,  0 fk_grupo,  LLAVE.Llave_01 AS Llave_01, LLAVE.Llave_02 AS Llave_02,  Documento.fk_Tipologia " +
          " FROM		Process.TBL_Cargue AS Cargue " +
          " INNER JOIN	Core.CTA_Folder AS Folder  " +
           "	ON	Cargue.id_Cargue = Folder.fk_Cargue " +
          " INNER JOIN	Core.CTA_File AS Files  " +
            " ON	Folder.fk_Expediente = Files.fk_Expediente " +
           "	AND Folder.fk_Folder = Files.fk_Folder  " +
          " INNER JOIN	Core.CTA_Process_File AS ProcessFile  " +
           " 	ON	Files.fk_Expediente = ProcessFile.fk_Expediente  " +
           " 	AND Files.fk_Folder = ProcessFile.fk_Folder  " +
           "	AND Files.fk_File = ProcessFile.id_File	 " +
         "	INNER JOIN	Core.CTA_Documento AS Documento " +
           "	ON	ProcessFile.fk_Documento = Documento.id_Documento			 " +
             "    INNER JOIN  [DB_Miharu.Core].[Process].[TBL_Expediente_Llave_Linea] LLAVE " +
            "    ON  LLAVE.fk_Expediente = Folder.fk_Expediente " +
            "  AND 	LLAVE.fk_Entidad = Cargue.fk_Entidad  " +
             "   AND LLAVE.fk_proyecto = Cargue.fk_proyecto  " +
                       "  WHERE (Cargue.fk_OT = @id_OT) " +
         "	ORDER BY Files.fk_Expediente, Files.fk_Folder, Files.fk_File  "

                Dim SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@id_OT", id_OT)
                }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function getEntidad() As DataTable
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmSecurity.Connection_Open(Program.Sesion.Usuario.id)

                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmSecurity.DataBase.ConnectionString)
                Dim sqlquery As String = "select [Nombre_Entidad] from [DB_Miharu.Security_Core].[Config].[TBL_Entidad] WHERE id_Entidad = @fk_entidad;"
                dt = ExecuteQuery(sqlquery, conn, New SqlParameter("@fk_entidad", Entidad))
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function getExportacionFolders() As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)


                Dim sqlquery As String = " 	CREATE TABLE #TempServidor( Id INT IDENTITY (1,1), fk_Servidor Int )" +
                 " INSERT INTO #TempServidor " +
                " SELECT DISTINCT id_Servidor FROM [DB_Miharu.Imaging_Core].[Config].[TBL_Proyecto] TP " +
                " INNER JOIN [DB_Miharu.Core].[Imaging].[TBL_Servidor] TS " +
                " ON TP.fk_Entidad_Servidor = TS.fk_Entidad " +
                " AND TS.fk_Servidor_Tipo = 1 " +
                " WHERE TP.fk_Entidad = @fk_entidad   DECLARE @SQL VARCHAR(MAX), @Servidores varchar(10),@BaseDatos varchar(50), @ServidorBaseDatos varchar(100) " +
                " DECLARE @Servidor INT, @intServidor INT, @SQLeft  VARCHAR(MAX), @SQLCase VARCHAR(MAX), @SQLConcat VARCHAR(MAX), @SQLConcatCase VARCHAR(MAX) " +
                " SET @intServidor = 1 " +
                " WHILE (@intServidor <= (SELECT COUNT(*) FROM #TempServidor))  BEGIN " +
                " SET @Servidor = (SELECT fk_Servidor FROM #TempServidor WHERE Id = @intServidor) " +
                " SET @Servidores = (SELECT  substring(Nombre_Servidor,20,9)   FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1)  " +
                " SET @BaseDatos =(SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(ConnectionString_Servidor,'SlygProvider=SqlServer;Data Source=10.64.118.42',''),'Initial Catalog=',''),';Persist Security Info=True;User ID=U_App_Core;Password=riskapp1120',''),'\',''),';',''),'Historico','') FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1) " +
                " IF(ISNULL(@Servidores,'') = '')  BEGIN " +
                " SET @ServidorBaseDatos = '['+ @BaseDatos + ']' " +
                " End " +
                " IF(ISNULL(@Servidores,'') <> '') " +
                " BEGIN " +
                " SET @ServidorBaseDatos = '['+ @Servidores + ']' +'.'+ '['+ @BaseDatos + ']' " +
                " End " +
                " IF @Servidor > 0 " +
                " BEGIN " +
                " SET @SQLCase = ' WHEN  File'+CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente IS NOT NULL THEN ' + CONVERT(VARCHAR(5),@Servidor) " +
                " SET  @SQLeft =  ' LEFT JOIN ' + @ServidorBaseDatos + '.Imaging.TBL_File File' + CONVERT(VARCHAR(5), @Servidor)  + '	 ON File' + CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente = Files.fk_Expediente AND File'+ CONVERT(VARCHAR(5),@Servidor) +'.fk_Folder = Files.fk_Folder AND File'+CONVERT(VARCHAR(5),@Servidor) +'.fk_File = Files.fk_File' " +
                " SET  @SQLConcat  =  CONCAT(@SQLConcat, @SQLeft); " +
                " SET  @SQLConcatCase = CONCAT(@SQLConcatCase, @SQLCase); " +
                " End " +
                " PRINT @intServidor " +
                " SET @intServidor = @intServidor + 1  " +
                " End " +
                          " SET @SQL = 'SELECT DISTINCT Cargue.fk_OT AS id_OT,  Esquema.id_Esquema, Esquema.Nombre_Esquema, Cargue.fk_Entidad,   Cargue.fk_Proyecto, Folder.fk_Expediente,  " +
                                         " Folder.fk_Folder,   ProcessFolder.CBarras_Folder,   Process.Fn_getKey(Folder.fk_Expediente, 1, 0) AS Key_1,   " +
                                         "  Process.Fn_getKey(Folder.fk_Expediente, 2, 0) AS Key_2,   Process.Fn_getKey(Folder.fk_Expediente, 3, 0) AS Key_3, " +
                                           " Folder.fk_Entidad_Servidor,   Folder.fk_Servidor,   Folder.Fecha_Creacion  ' " +
                             " IF @Servidor > 0 " +
                          " BEGIN " +
                         "SET @SQL = @SQL +' ,CASE '  " +
                         " SET @SQL = CONCAT(@SQL, @SQLConcatCase); " +
                         " SET @SQL = @SQL +' END AS fk_ServidorStorage'  " +
                         " End " +
                         " SET @SQL = @SQL +'  " +
        " INTO #TempExpedientes  " +
                         " FROM Process.TBL_OT AS OT " +
                        " INNER JOIN Process.TBL_Cargue AS Cargue " +
                        " ON Cargue.fk_OT = OT.id_OT " +
                        " INNER JOIN Core.CTA_Folder AS Folder " +
                        " ON Cargue.id_Cargue = Folder.fk_Cargue " +
                        " INNER JOIN  [DB_Miharu.Core].[Process].[TBL_Expediente_Llave_Linea] LLAVE " +
                         " ON  LLAVE.fk_Expediente = Folder.fk_Expediente " +
                         " and cargue.fk_Entidad = LLAVE.fk_Entidad " +
                         " and cargue.fk_Proyecto = LLAVE.fk_Proyecto " +
                        " INNER JOIN Core.CTA_File AS Files " +
                        " ON Folder.fk_Expediente = Files.fk_Expediente " +
                        " AND Folder.fk_Folder = Files.fk_Folder " +
                        " INNER JOIN Core.CTA_Process_File AS ProcessFile  " +
                         " ON Files.fk_Expediente = ProcessFile.fk_Expediente " +
                         " AND Files.fk_Folder = ProcessFile.fk_Folder " +
                         " AND Files.fk_File = ProcessFile.id_File " +
                        " INNER JOIN	Core.CTA_Process_Folder AS ProcessFolder " +
                         " ON	Folder.fk_Expediente = ProcessFolder.fk_Expediente  " +
                          " AND Folder.fk_Folder = ProcessFolder.id_Folder " +
                        " INNER JOIN	Core.CTA_Esquema AS Esquema " +
                        "	ON	LLAVE.fk_Entidad = Esquema.fk_Entidad " +
                         " AND LLAVE.fk_Proyecto = Esquema.fk_Proyecto " +
                         " AND LLAVE.fk_Esquema = Esquema.id_Esquema " +
                         " INNER JOIN Core.CTA_Documento AS Documento " +
                         " ON ProcessFile.fk_Documento = Documento.id_Documento " +
                        " LEFT JOIN Config.TBL_Documentos_Agrupar_Exportacion_Imagenes Doc_Agrupador " +
                         " ON ProcessFile.fk_Documento = Doc_Agrupador.fk_Documento ' " +
                         " SET @SQL = CONCAT(@SQL, @SQLConcat); " +
                         "SET @SQL = @SQL + ' " +
                         " WHERE  Cargue.fk_Entidad = ' + CONVERT(VARCHAR(5),@fk_entidad) + ' " +
                         " AND (Cargue.Fecha_Proceso BETWEEN  CONVERT(DATETIME,CONVERT(VARCHAR(10), ''' + CONVERT(VARCHAR(10),@Fecha_Proceso_Inicial) +''', 111), 110) AND CONVERT(DATETIME,CONVERT(VARCHAR(10),'''+ CONVERT(VARCHAR(10),@Fecha_Proceso_Final) +''', 111) + ' + ''' 23:59:59''' +', 110)) " +
                              "UPDATE #TempExpedientes " +
                            "  SET fk_ServidorStorage = 1 " +
                            " WHERE(fk_ServidorStorage Is NULL) " +
                            " select  * from #TempExpedientes  " +
                            " DROP TABLE #TempExpedientes  ' " +
                            " EXEC (@SQL) " +
                            " DROP TABLE #TempServidor "

                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_entidad", Entidad),
                   New SqlParameter("@Fecha_Proceso_Inicial", CStr(Me.FechaProcesoDateTimePicker.Value.ToString("yyyy/MM/dd"))),
                   New SqlParameter("@Fecha_Proceso_Final", CStr(Me.FechaProcesoFinalDateTimePicker.Value.ToString("yyyy/MM/dd")))
               }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function getExportacion_Esquema() As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = "	SELECT distinct Esquema.id_Esquema, " +
                           " Esquema.Nombre_Esquema " +
                     " FROM		Process.TBL_OT as OT " +
                     " INNER JOIN	Core.CTA_Esquema AS Esquema " +
                       " ON	OT.fk_Entidad = Esquema.fk_Entidad " +
                    " WHERE    OT.fk_Entidad = @fk_entidad"
                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_entidad", Entidad)
               }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function getExportacion_Campo_Busqueda(nfk_Poryecto As Integer) As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = " SELECT DISTINCT CampoBusqueda.fk_Campo_Tipo, CampoBusqueda.id_Campo_Busqueda,  CampoBusqueda.Nombre_Campo_Busqueda " +
             " FROM		Process.TBL_Cargue AS Cargue " +
             " INNER JOIN	Core.CTA_Folder AS Folder 			 " +
               " ON	Cargue.id_Cargue = Folder.fk_Cargue	 " +
             " INNER JOIN	Core.CTA_File_Data AS FileData  " +
               " ON	Folder.fk_Expediente = FileData.fk_Expediente  " +
               " AND Folder.fk_Folder = FileData.fk_Folder 			 " +
             " INNER JOIN	Core.CTA_Campo AS Campo  " +
               " ON	FileData.fk_Documento = Campo.fk_Documento  " +
               " AND FileData.fk_Campo = Campo.id_Campo  " +
             " INNER JOIN	Core.CTA_Campo_Busqueda AS CampoBusqueda  " +
              " 	ON	Campo.fk_Campo_Tipo = CampoBusqueda.fk_Campo_Tipo  " +
               " AND Campo.fk_Campo_Busqueda = CampoBusqueda.id_Campo_Busqueda	 " +
                           " WHERE Cargue.fk_Entidad = @fk_entidad and Cargue.fk_Proyecto = @fk_Proyecto "
                Dim SqlParameter = New SqlParameter() _
    {
        New SqlParameter("@fk_Proyecto", nfk_Poryecto),
        New SqlParameter("@fk_entidad", Entidad)
    }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function getExportacion_Validacion() As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = " 		SELECT	DISTINCT Validacion.fk_Documento, Validacion.id_Validacion,	Validacion.Pregunta_Validacion_Reporte AS Pregunta_Validacion " +
                 " FROM		Process.TBL_OT as OT " +
                 " INNER JOIN	Core.CTA_Documento AS Documento " +
                   " ON	OT.fk_Entidad = Documento.fk_Entidad " +
                 " INNER JOIN	Core.CTA_Config_Validacion AS Validacion " +
                   " ON	Documento.id_Documento = Validacion.fk_Documento " +
               " WHERE OT.fk_Entidad = @fk_entidad "

                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_entidad", Entidad)
               }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function getExportacion_Campo() As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = " 	SELECT DISTINCT	Campo.fk_Documento,	Campo.id_Campo, " +
                " Campo.Nombre_Campo, " +
                "Campo.Es_Campo_Busqueda, " +
                " Campo.fk_Campo_Tipo, " +
               " ISNULL(Campo.fk_Campo_Busqueda, 0) As fk_Campo_Busqueda " +
             " FROM		Process.TBL_OT as OT " +
             " INNER JOIN	Core.CTA_Documento AS Documento " +
               " ON	OT.fk_Entidad = Documento.fk_Entidad " +
             " INNER JOIN	Core.CTA_Campo AS Campo " +
               " ON	Documento.id_Documento = Campo.fk_Documento			 " +
                " WHERE    OT.fk_Entidad = @fk_entidad "
                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_entidad", Entidad)
               }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function getExportacion_Documento() As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = " SELECT DISTINCT Documento.id_Documento, " +
                " Documento.Nombre_Documento " +
                 " FROM		Process.TBL_OT as OT " +
                 " INNER JOIN	Core.CTA_Documento AS Documento " +
                  "	ON	OT.fk_Entidad = Documento.fk_Entidad " +
                " WHERE   OT.fk_Entidad = @fk_entidad"
                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_entidad", Entidad)
               }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function getExportacion_Data() As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = " CREATE TABLE #TempServidor( Id INT IDENTITY (1,1), fk_Servidor Int )" +
                 " INSERT INTO #TempServidor " +
                " SELECT DISTINCT id_Servidor FROM [DB_Miharu.Imaging_Core].[Config].[TBL_Proyecto] TP " +
                " INNER JOIN [DB_Miharu.Core].[Imaging].[TBL_Servidor] TS " +
                " ON TP.fk_Entidad_Servidor = TS.fk_Entidad " +
                " AND TS.fk_Servidor_Tipo = 1 " +
                " WHERE TP.fk_Entidad = @fk_entidad   DECLARE @SQL VARCHAR(MAX), @Servidores varchar(10),@BaseDatos varchar(50), @ServidorBaseDatos varchar(100) " +
                " DECLARE @Servidor INT, @intServidor INT, @SQLeft  VARCHAR(MAX), @SQLCase VARCHAR(MAX), @SQLConcat VARCHAR(MAX), @SQLConcatCase VARCHAR(MAX) " +
                " SET @intServidor = 1 " +
                " WHILE (@intServidor <= (SELECT COUNT(*) FROM #TempServidor))  BEGIN " +
                " SET @Servidor = (SELECT fk_Servidor FROM #TempServidor WHERE Id = @intServidor) " +
                " SET @Servidores = (SELECT  substring(Nombre_Servidor,20,9)   FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1)  " +
                " SET @BaseDatos =(SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(ConnectionString_Servidor,'SlygProvider=SqlServer;Data Source=10.64.118.42',''),'Initial Catalog=',''),';Persist Security Info=True;User ID=U_App_Core;Password=riskapp1120',''),'\',''),';',''),'Historico','') FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1) " +
                " IF(ISNULL(@Servidores,'') = '')  BEGIN " +
                " SET @ServidorBaseDatos = '['+ @BaseDatos + ']' " +
                " End " +
                " IF(ISNULL(@Servidores,'') <> '') " +
                " BEGIN " +
                " SET @ServidorBaseDatos = '['+ @Servidores + ']' +'.'+ '['+ @BaseDatos + ']' " +
                " End " +
                " IF @Servidor > 0 " +
                " BEGIN " +
                " SET @SQLCase = ' WHEN  File'+CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente IS NOT NULL THEN ' + CONVERT(VARCHAR(5),@Servidor) " +
                " SET  @SQLeft =  ' LEFT JOIN ' + @ServidorBaseDatos + '.Imaging.TBL_File File' + CONVERT(VARCHAR(5), @Servidor)  + '	 ON File' + CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente = Files.fk_Expediente AND File'+ CONVERT(VARCHAR(5),@Servidor) +'.fk_Folder = Files.fk_Folder AND File'+CONVERT(VARCHAR(5),@Servidor) +'.fk_File = Files.fk_File' " +
                " SET  @SQLConcat  =  CONCAT(@SQLConcat, @SQLeft); " +
                " SET  @SQLConcatCase = CONCAT(@SQLConcatCase, @SQLCase); " +
                " End " +
                " PRINT @intServidor " +
                " SET @intServidor = @intServidor + 1  " +
                " End " +
                     " SET @SQL = 'SELECT  Cargue.fk_OT AS id_OT, Campo.Es_Campo_Busqueda,  Files.fk_Expediente, " +
                                   " Files.fk_Folder, Files.fk_File, Files.id_Version, Campo.fk_Documento, " +
                                   " Campo.id_Campo, Campo.fk_Campo_Tipo, Campo.fk_Campo_Busqueda, Campo.Nombre_Campo, " +
                     "isnull(Process.Fn_getFileData(Folder.fk_Expediente, Folder.fk_Folder, Files.fk_File, FileData.fk_Documento, FileData.fk_Campo, 0),'''') AS Valor_File_Data ,Cargue.fk_Proyecto' " +
                        " IF @Servidor > 0 " +
                     " BEGIN " +
                    "SET @SQL = @SQL +' ,CASE '  " +
                    " SET @SQL = CONCAT(@SQL, @SQLConcatCase); " +
                    " SET @SQL = @SQL +' END AS fk_ServidorStorage'  " +
                    " End " +
                    " SET @SQL = @SQL +'  " +
                    " INTO #TempExpedientes  " +
                    " FROM Process.TBL_Cargue AS Cargue  INNER JOIN	Core.CTA_Folder AS Folder 	   " +
                     " ON	Cargue.id_Cargue = Folder.fk_Cargue  " +
                      " INNER JOIN [DB_Miharu.Core].Process.TBL_Expediente E " +
                     " ON E.id_Expediente = Folder.fk_Expediente  " +
                      " AND Cargue.fk_Entidad = E.fk_Entidad  " +
                      " AND Cargue.fk_Proyecto = E.fk_Proyecto " +
                    "INNER JOIN	Core.CTA_File AS Files  " +
                     " ON	Folder.fk_Expediente = Files.fk_Expediente   AND Folder.fk_Folder = Files.fk_Folder	   " +
                     " INNER JOIN	Core.CTA_File_Data AS FileData   ON	Files.fk_Expediente = FileData.fk_Expediente   " +
                     " AND Files.fk_Folder = FileData.fk_Folder  AND Files.fk_File = FileData.fk_File  " +
                     " INNER JOIN	Core.CTA_Campo AS Campo   ON	FileData.fk_Documento = Campo.fk_Documento   " +
                     " AND FileData.fk_Campo = Campo.id_Campo	 '  " +
                    " SET @SQL = CONCAT(@SQL, @SQLConcat); " +
                    "SET @SQL = @SQL + ' " +
                    " WHERE  Cargue.fk_Entidad = ' + CONVERT(VARCHAR(5),@fk_entidad) + ' " +
                    " AND (Cargue.Fecha_Proceso BETWEEN  CONVERT(DATETIME,CONVERT(VARCHAR(10), ''' + CONVERT(VARCHAR(10),@Fecha_Proceso_Inicial) +''', 111), 110) AND CONVERT(DATETIME,CONVERT(VARCHAR(10),'''+ CONVERT(VARCHAR(10),@Fecha_Proceso_Final) +''', 111) + ' + ''' 23:59:59''' +', 110)) " +
                          "UPDATE #TempExpedientes " +
                            "  SET fk_ServidorStorage = 1 " +
                            " WHERE(fk_ServidorStorage Is NULL) " +
                            " select  * from #TempExpedientes  " +
                            " DROP TABLE #TempExpedientes  ' " +
                            " EXEC (@SQL) " +
                            " DROP TABLE #TempServidor "

                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_entidad", Entidad),
                   New SqlParameter("@Fecha_Proceso_Inicial", CStr(Me.FechaProcesoDateTimePicker.Value.ToString("yyyy/MM/dd"))),
                   New SqlParameter("@Fecha_Proceso_Final", CStr(Me.FechaProcesoFinalDateTimePicker.Value.ToString("yyyy/MM/dd")))
               }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function getExportacion_Validaciones() As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = "	CREATE TABLE #TempServidor( Id INT IDENTITY (1,1), fk_Servidor Int )" +
                 " INSERT INTO #TempServidor " +
                " SELECT DISTINCT id_Servidor FROM [DB_Miharu.Imaging_Core].[Config].[TBL_Proyecto] TP " +
                " INNER JOIN [DB_Miharu.Core].[Imaging].[TBL_Servidor] TS " +
                " ON TP.fk_Entidad_Servidor = TS.fk_Entidad " +
                " AND TS.fk_Servidor_Tipo = 1 " +
                " WHERE TP.fk_Entidad = @fk_entidad   DECLARE @SQL VARCHAR(MAX), @Servidores varchar(10),@BaseDatos varchar(50), @ServidorBaseDatos varchar(100) " +
                " DECLARE @Servidor INT, @intServidor INT, @SQLeft  VARCHAR(MAX), @SQLCase VARCHAR(MAX), @SQLConcat VARCHAR(MAX), @SQLConcatCase VARCHAR(MAX) " +
                " SET @intServidor = 1 " +
                " WHILE (@intServidor <= (SELECT COUNT(*) FROM #TempServidor))  BEGIN " +
                " SET @Servidor = (SELECT fk_Servidor FROM #TempServidor WHERE Id = @intServidor) " +
                " SET @Servidores = (SELECT  substring(Nombre_Servidor,20,9)   FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1)  " +
                " SET @BaseDatos =(SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(ConnectionString_Servidor,'SlygProvider=SqlServer;Data Source=10.64.118.42',''),'Initial Catalog=',''),';Persist Security Info=True;User ID=U_App_Core;Password=riskapp1120',''),'\',''),';',''),'Historico','') FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1) " +
                " IF(ISNULL(@Servidores,'') = '')  BEGIN " +
                " SET @ServidorBaseDatos = '['+ @BaseDatos + ']' " +
                " End " +
                " IF(ISNULL(@Servidores,'') <> '') " +
                " BEGIN " +
                " SET @ServidorBaseDatos = '['+ @Servidores + ']' +'.'+ '['+ @BaseDatos + ']' " +
                " End " +
                " IF @Servidor > 0 " +
                " BEGIN " +
                " SET @SQLCase = ' WHEN  File'+CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente IS NOT NULL THEN ' + CONVERT(VARCHAR(5),@Servidor) " +
                " SET  @SQLeft =  ' LEFT JOIN ' + @ServidorBaseDatos + '.Imaging.TBL_File File' + CONVERT(VARCHAR(5), @Servidor)  + '	 ON File' + CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente = Files.fk_Expediente AND File'+ CONVERT(VARCHAR(5),@Servidor) +'.fk_Folder = Files.fk_Folder AND File'+CONVERT(VARCHAR(5),@Servidor) +'.fk_File = Files.fk_File' " +
                " SET  @SQLConcat  =  CONCAT(@SQLConcat, @SQLeft); " +
                " SET  @SQLConcatCase = CONCAT(@SQLConcatCase, @SQLCase); " +
                " End " +
                " PRINT @intServidor " +
                " SET @intServidor = @intServidor + 1  " +
                " End " +
                     " SET @SQL = 'SELECT Cargue.fk_OT AS id_OT, Files.fk_Expediente, Files.fk_Folder,Files.fk_File, " +
                                        " Files.id_Version, Validacion.fk_Documento, Validacion.id_Validacion, Validacion.Pregunta_Validacion_Reporte AS Pregunta_Validacion,  " +
                                     " FileValidacion.Respuesta, Cargue.fk_Proyecto ' " +
                        " IF @Servidor > 0 " +
                     " BEGIN " +
                    "SET @SQL = @SQL +' ,CASE '  " +
                    " SET @SQL = CONCAT(@SQL, @SQLConcatCase); " +
                    " SET @SQL = @SQL +' END AS fk_ServidorStorage'  " +
                    " End " +
                    " SET @SQL = @SQL +'  " +
                    " INTO #TempExpedientes  " +
                  " FROM Process.TBL_Cargue AS Cargue   " +
                 " INNER JOIN	Core.CTA_Folder AS Folder 	  " +
                 " ON	Cargue.id_Cargue = Folder.fk_Cargue   " +
                 " INNER JOIN  [DB_Miharu.Core].[Process].[TBL_Expediente_Llave_Linea] LLAVE " +
                 " ON  LLAVE.fk_Expediente = Folder.fk_Expediente  " +
                  " AND Cargue.fk_Entidad = LLAVE.fk_Entidad  " +
                  " AND Cargue.fk_Proyecto = LLAVE.fk_Proyecto " +
                  " INNER JOIN	Core.CTA_File AS Files   " +
                  " ON	Folder.fk_Expediente = Files.fk_Expediente    " +
                  " AND Folder.fk_Folder = Files.fk_Folder	  " +
                  " INNER JOIN  Core.CTA_File_Validacion AS FileValidacion   " +
                 " ON	Files.fk_Expediente = FileValidacion.fk_Expediente   " +
                 " AND Files.fk_Folder = FileValidacion.fk_Folder   " +
                 " AND Files.fk_File = FileValidacion.fk_File   " +
                 " INNER JOIN	Core.CTA_Config_Validacion AS Validacion   " +
                 " ON	FileValidacion.fk_Documento = Validacion.fk_Documento   " +
                 " AND FileValidacion.fk_Validacion = Validacion.id_Validacion  " +
                 " INNER JOIN Core.CTA_Process_File AS ProcessFile  " +
                 " ON Files.fk_Expediente = ProcessFile.fk_Expediente " +
                 " AND Files.fk_Folder = ProcessFile.fk_Folder  " +
                  " AND Files.fk_File = ProcessFile.id_File " +
                  " INNER JOIN Core.CTA_Documento AS Documento " +
                  " ON ProcessFile.fk_Documento = Documento.id_Documento " +
                    " LEFT JOIN Config.TBL_Documentos_Agrupar_Exportacion_Imagenes Doc_Agrupador " +
                 " ON ProcessFile.fk_Documento = Doc_Agrupador.fk_Documento ' " +
                    " SET @SQL = CONCAT(@SQL, @SQLConcat); " +
                    "SET @SQL = @SQL + ' " +
                    " WHERE  Cargue.fk_Entidad = ' + CONVERT(VARCHAR(5),@fk_entidad) + ' " +
                    " AND (Cargue.Fecha_Proceso BETWEEN  CONVERT(DATETIME,CONVERT(VARCHAR(10), ''' + CONVERT(VARCHAR(10),@Fecha_Proceso_Inicial) +''', 111), 110) AND CONVERT(DATETIME,CONVERT(VARCHAR(10),'''+ CONVERT(VARCHAR(10),@Fecha_Proceso_Final) +''', 111) + ' + ''' 23:59:59''' +', 110)) " +
                           "UPDATE #TempExpedientes " +
                            "  SET fk_ServidorStorage = 1 " +
                            " WHERE(fk_ServidorStorage Is NULL) " +
                            " select  * from #TempExpedientes  " +
                            " DROP TABLE #TempExpedientes  ' " +
                            " EXEC (@SQL) " +
                            " DROP TABLE #TempServidor "


                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_entidad", Entidad),
                   New SqlParameter("@Fecha_Proceso_Inicial", CStr(Me.FechaProcesoDateTimePicker.Value.ToString("yyyy/MM/dd"))),
                   New SqlParameter("@Fecha_Proceso_Final", CStr(Me.FechaProcesoFinalDateTimePicker.Value.ToString("yyyy/MM/dd")))
               }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function getExpedientes() As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = "  CREATE TABLE #TempServidor( Id INT IDENTITY (1,1), fk_Servidor Int )" +
                 " INSERT INTO #TempServidor " +
                " SELECT DISTINCT id_Servidor FROM [DB_Miharu.Imaging_Core].[Config].[TBL_Proyecto] TP " +
                " INNER JOIN [DB_Miharu.Core].[Imaging].[TBL_Servidor] TS " +
                " ON TP.fk_Entidad_Servidor = TS.fk_Entidad " +
                " AND TS.fk_Servidor_Tipo = 1 " +
                " WHERE TP.fk_Entidad = @fk_entidad   DECLARE @SQL VARCHAR(MAX), @Servidores varchar(10),@BaseDatos varchar(50), @ServidorBaseDatos varchar(100) " +
                " DECLARE @Servidor INT, @intServidor INT, @SQLeft  VARCHAR(MAX), @SQLCase VARCHAR(MAX), @SQLConcat VARCHAR(MAX), @SQLConcatCase VARCHAR(MAX) " +
                " SET @intServidor = 1 " +
                " WHILE (@intServidor <= (SELECT COUNT(*) FROM #TempServidor))  BEGIN " +
                " SET @Servidor = (SELECT fk_Servidor FROM #TempServidor WHERE Id = @intServidor) " +
                " SET @Servidores = (SELECT  substring(Nombre_Servidor,20,9)   FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1)  " +
                " SET @BaseDatos =(SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(ConnectionString_Servidor,'SlygProvider=SqlServer;Data Source=10.64.118.42',''),'Initial Catalog=',''),';Persist Security Info=True;User ID=U_App_Core;Password=riskapp1120',''),'\',''),';',''),'Historico','') FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1) " +
                " IF(ISNULL(@Servidores,'') = '')  BEGIN " +
                " SET @ServidorBaseDatos = '['+ @BaseDatos + ']' " +
                " End " +
                " IF(ISNULL(@Servidores,'') <> '') " +
                " BEGIN " +
                " SET @ServidorBaseDatos = '['+ @Servidores + ']' +'.'+ '['+ @BaseDatos + ']' " +
                " End " +
                " IF @Servidor > 0 " +
                " BEGIN " +
                " SET @SQLCase = ' WHEN  File'+CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente IS NOT NULL THEN ' + CONVERT(VARCHAR(5),@Servidor) " +
                " SET  @SQLeft =  ' LEFT JOIN ' + @ServidorBaseDatos + '.Imaging.TBL_File File' + CONVERT(VARCHAR(5), @Servidor)  + '	 ON File' + CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente = Files.fk_Expediente AND File'+ CONVERT(VARCHAR(5),@Servidor) +'.fk_Folder = Files.fk_Folder AND File'+CONVERT(VARCHAR(5),@Servidor) +'.fk_File = Files.fk_File' " +
                " SET  @SQLConcat  =  CONCAT(@SQLConcat, @SQLeft); " +
                " SET  @SQLConcatCase = CONCAT(@SQLConcatCase, @SQLCase); " +
                " End " +
                " PRINT @intServidor " +
                " SET @intServidor = @intServidor + 1  " +
                " End " +
                " SET @SQL = 'SELECT distinct Cargue.fk_OT,Cargue.Fecha_Proceso, OT.fk_Proyecto,Files.fk_Expediente " +
                " ,Files.fk_Folder, Files.fk_File, Files.id_Version, Files.File_Unique_Identifier, Documento.Nombre_Documento " +
                " ,Folder.fk_Entidad_Servidor, Folder.fk_Servidor, Files.Nombre_Imagen_File,Files.Folios_Documento_File, " +
                " Files.Tamaño_Imagen_File,ProcessFile.fk_Documento,ISNULL(Doc_Agrupador.fk_Grupo,0) AS fk_Grupo, " +
                " LLAVE.Llave_01 AS Llave_01,LLAVE.Llave_02 AS Llave_02, Documento.fk_Tipologia ' " +
                " IF @Servidor > 0 " +
                " BEGIN " +
                "SET @SQL = @SQL +' ,CASE '  " +
                "SET @SQL = CONCAT(@SQL, @SQLConcatCase); " +
                " SET @SQL = @SQL +' END AS fk_ServidorStorage'  " +
                " End " +
                " SET @SQL = @SQL +'  " +
                 "INTO #TempExpedientes " +
                "FROM Process.TBL_OT AS OT   " +
                " INNER JOIN Process.TBL_Cargue AS Cargue  " +
                " 	ON Cargue.fk_OT = OT.id_OT  " +
                " INNER JOIN Core.CTA_Folder AS Folder  " +
                " 	ON Cargue.id_Cargue = Folder.fk_Cargue  " +
    " INNER JOIN  [DB_Miharu.Core].[Process].[TBL_Expediente_Llave_Linea] LLAVE " +
                " 	ON  LLAVE.fk_Expediente = Folder.fk_Expediente  " +
                " 	AND OT.fk_Entidad = LLAVE.fk_Entidad  " +
                " 	AND OT.fk_Proyecto = LLAVE.fk_Proyecto " +
                " INNER JOIN Core.CTA_File AS Files " +
                " 	ON Folder.fk_Expediente = Files.fk_Expediente " +
                " 	AND Folder.fk_Folder = Files.fk_Folder  " +
                " INNER JOIN Core.CTA_Process_File AS ProcessFile  " +
                "	 ON Files.fk_Expediente = ProcessFile.fk_Expediente " +
                "	 AND Files.fk_Folder = ProcessFile.fk_Folder  " +
                "	 AND Files.fk_File = ProcessFile.id_File " +
                " INNER JOIN Core.CTA_Documento AS Documento " +
                "	 ON ProcessFile.fk_Documento = Documento.id_Documento " +
                " LEFT JOIN Config.TBL_Documentos_Agrupar_Exportacion_Imagenes Doc_Agrupador " +
                "	 ON ProcessFile.fk_Documento = Doc_Agrupador.fk_Documento ' " +
                "SET @SQL = CONCAT(@SQL, @SQLConcat); " +
                " SET @SQL = @SQL + ' " +
                " WHERE  OT.fk_Entidad = ' + CONVERT(VARCHAR(5),@fk_entidad) + ' " +
                " AND (Cargue.Fecha_Proceso BETWEEN  CONVERT(DATETIME,CONVERT(VARCHAR(10), ''' + CONVERT(VARCHAR(10),@Fecha_Proceso_Inicial) +''', 111), 110) AND CONVERT(DATETIME,CONVERT(VARCHAR(10),'''+ CONVERT(VARCHAR(10),@Fecha_Proceso_Final) +''', 111) + ' + ''' 23:59:59''' +', 110))  " +
                "UPDATE #TempExpedientes " +
                "  SET fk_ServidorStorage = 1 " +
                " WHERE(fk_ServidorStorage Is NULL) " +
                " select  * from #TempExpedientes  " +
                " DROP TABLE #TempExpedientes  ' " +
                " EXEC (@SQL) " +
                " DROP TABLE #TempServidor "
                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_entidad", Entidad),
                   New SqlParameter("@Fecha_Proceso_Inicial", CStr(Me.FechaProcesoDateTimePicker.Value.ToString("yyyy/MM/dd"))),
                   New SqlParameter("@Fecha_Proceso_Final", CStr(Me.FechaProcesoFinalDateTimePicker.Value.ToString("yyyy/MM/dd")))
               }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function

        Private Function getExportarOrfeo() As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = "  "
                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_entidad", Entidad),
                   New SqlParameter("@Fecha_Proceso_Inicial", CStr(Me.FechaProcesoDateTimePicker.Value.ToString("yyyy/MM/dd"))),
                   New SqlParameter("@Fecha_Proceso_Final", CStr(Me.FechaProcesoFinalDateTimePicker.Value.ToString("yyyy/MM/dd")))
               }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function

        Private Function Exportacion_Totales(nfk_Proyecto As Integer) As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = "  CREATE TABLE #TempServidor( Id INT IDENTITY (1,1), fk_Servidor Int ) " +
                        " INSERT INTO #TempServidor " +
                        " SELECT DISTINCT id_Servidor FROM [DB_Miharu.Imaging_Core].[Config].[TBL_Proyecto] TP " +
                        "INNER JOIN [DB_Miharu.Core].[Imaging].[TBL_Servidor] TS " +
                        " ON TP.fk_Entidad_Servidor = TS.fk_Entidad " +
                        "AND TS.fk_Servidor_Tipo = 1  " +
                        "WHERE TP.fk_Entidad = @fk_entidad " +
                        " DECLARE @SQL VARCHAR(MAX), @Servidores varchar(10),@BaseDatos varchar(50), @ServidorBaseDatos varchar(100) " +
                        " DECLARE @Servidor INT, @intServidor INT, @SQLeft  VARCHAR(MAX), @SQLCase VARCHAR(MAX), @SQLConcat VARCHAR(MAX), @SQLConcatCase VARCHAR(MAX) " +
                        " SET @intServidor = 1 " +
                        " WHILE (@intServidor <= (SELECT COUNT(*) FROM #TempServidor)) " +
                        " BEGIN " +
                        " SET @Servidor = (SELECT fk_Servidor FROM #TempServidor WHERE Id = @intServidor) " +
                        " SET @Servidores = (SELECT  substring(Nombre_Servidor,20,9)   FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1)  " +
                        " SET @BaseDatos =(SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(ConnectionString_Servidor,'SlygProvider=SqlServer;Data Source=10.64.118.42',''),'Initial Catalog=',''),';Persist Security Info=True;User ID=U_App_Core;Password=riskapp1120',''),'\',''),';',''),'Historico','') FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1) " +
                        " IF(ISNULL(@Servidores,'') = '') " +
                        " BEGIN " +
                        " SET @ServidorBaseDatos = '['+ @BaseDatos + ']' " +
                        " End " +
                        " IF(ISNULL(@Servidores,'') <> '') " +
                        " BEGIN " +
                        " SET @ServidorBaseDatos = '['+ @Servidores + ']' +'.'+ '['+ @BaseDatos + ']' " +
                        " End " +
                        " IF @Servidor > 0 " +
                        "BEGIN " +
                         "	SET @SQLCase = ' WHEN  File'+CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente IS NOT NULL THEN ' + + CONVERT(VARCHAR(5),@Servidor) " +
                         " SET  @SQLeft =  ' LEFT JOIN ' + @ServidorBaseDatos + '.Imaging.TBL_File File' + CONVERT(VARCHAR(5), @Servidor) + " +
                                 " '	 ON File' + CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente = Files.fk_Expediente  " +
                          "  AND File'+ CONVERT(VARCHAR(5),@Servidor) +'.fk_Folder = Files.fk_Folder " +
                          " AND File'+CONVERT(VARCHAR(5),@Servidor) +'.fk_File = Files.fk_File' " +
                          " SET  @SQLConcat  =  CONCAT(@SQLConcat, @SQLeft); " +
                          "SET  @SQLConcatCase = CONCAT(@SQLConcatCase, @SQLCase);  " +
                                " End " +
                        " PRINT @intServidor " +
                         "   SET @intServidor = @intServidor + 1 " +
                        " End " +
                        " SET @SQL = 'SELECT 0 AS OT, " +
                             " COUNT(*) AS Files,  " +
                             " SUM(Folios_Documento_File) AS Folios,  " +
                             "SUM(Tamaño_Imagen_File) AS Tamaño " +
                         " INTO #TempExpedientes  " +
                        " FROM Process.TBL_OT AS OT   " +
                         "INNER JOIN Process.TBL_Cargue AS Cargue  " +
                        " ON Cargue.fk_OT = OT.id_OT  " +
                        " INNER JOIN Core.CTA_Folder AS Folder  " +
                        " ON Cargue.id_Cargue = Folder.fk_Cargue  " +
      " INNER JOIN  [DB_Miharu.Core].[Process].[TBL_Expediente_Llave_Linea] LLAVE  " +
                        " ON  LLAVE.fk_Expediente = Folder.fk_Expediente   " +
                        " AND OT.fk_Entidad = LLAVE.fk_Entidad  " +
                        " AND OT.fk_Proyecto = LLAVE.fk_Proyecto " +
                        " INNER JOIN Core.CTA_File AS Files " +
                        " ON Folder.fk_Expediente = Files.fk_Expediente " +
                        " AND Folder.fk_Folder = Files.fk_Folder  " +
                        " INNER JOIN Core.CTA_Process_File AS ProcessFile  " +
                        "	 ON Files.fk_Expediente = ProcessFile.fk_Expediente " +
                        "	 AND Files.fk_Folder = ProcessFile.fk_Folder  " +
                        "	 AND Files.fk_File = ProcessFile.id_File " +
                        " INNER JOIN Core.CTA_Documento AS Documento " +
                        "	 ON ProcessFile.fk_Documento = Documento.id_Documento " +
                        " LEFT JOIN Config.TBL_Documentos_Agrupar_Exportacion_Imagenes Doc_Agrupador " +
                        "	 ON ProcessFile.fk_Documento = Doc_Agrupador.fk_Documento ' " +
                        "SET @SQL = CONCAT(@SQL, @SQLConcat);  " +
                        "SET @SQL = @SQL + ' " +
                        " WHERE  OT.fk_Entidad = ' + CONVERT(VARCHAR(5),@fk_entidad) + ' " +
                         " AND OT.fk_Proyecto = ' + CONVERT(VARCHAR(5),@fk_Proyecto) + ' " +
                        "AND (Cargue.Fecha_Proceso BETWEEN  CONVERT(DATETIME,CONVERT(VARCHAR(10), ''' + CONVERT(VARCHAR(10),@Fecha_Proceso_Inicial) +''', 111), 110) AND CONVERT(DATETIME,CONVERT(VARCHAR(10),'''+ CONVERT(VARCHAR(10),@Fecha_Proceso_Final) +''', 111) + ' + ''' 23:59:59''' +', 110))  " +
                        " select  * from #TempExpedientes " +
                        " DROP TABLE #TempExpedientes  '" +
                        " EXEC (@SQL) " +
                        "DROP TABLE #TempServidor "

                Dim SqlParameter = New SqlParameter() _
               {
                    New SqlParameter("@fk_entidad", Entidad),
                   New SqlParameter("@fk_Proyecto", nfk_Proyecto),
                   New SqlParameter("@Fecha_Proceso_Inicial", CStr(Me.FechaProcesoDateTimePicker.Value.ToString("yyyy/MM/dd"))),
                   New SqlParameter("@Fecha_Proceso_Final", CStr(Me.FechaProcesoFinalDateTimePicker.Value.ToString("yyyy/MM/dd")))
               }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function
        Private Function InserTemporal(txtdatatable As DataTable, ByVal dbmImaging As DBImagingDataBaseManager) As Boolean
            Dim Valido As Boolean = False
            Const tabla As String = "##TempCarpetas"
            BulkInsert.InsertDataTableTemp(txtdatatable, dbmImaging, tabla)
            Valido = True
            Return Valido
        End Function
        Private Function ExecuteQuery(ByVal s As String, ByVal condb As SqlConnection, ByVal ParamArray params() As SqlParameter) As DataTable
            Dim dt As DataTable = Nothing
            Using da As New System.Data.SqlClient.SqlDataAdapter(s, condb)
                Try
                    dt = New DataTable
                    If params.Length > 0 Then
                        da.SelectCommand.Parameters.AddRange(params)
                    End If
                    If da.SelectCommand.Connection.State <> ConnectionState.Open Then da.SelectCommand.Connection.Open()
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(dt)
                    Return dt
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (da IsNot Nothing) Then da.SelectCommand.Connection.Close()
                End Try
                Return dt
            End Using
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
        Private Function getExpedientes(ByVal dbmImaging As DBImagingDataBaseManager, Proyectos As String) As DataTable

            Dim dt As DataTable = Nothing
            Try

                Dim sqlquery As String = My.Resources.Resources.GetExpedientes

                sqlquery = Replace(sqlquery, "@fk_Proyecto", Proyectos)

                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@Entidad", Entidad)
               }
                dt = ExecuteQuery(sqlquery, dbmImaging, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return dt
        End Function
        Private Function getExportacionFolders(ByVal dbmImaging As DBImagingDataBaseManager, Proyectos As String) As DataTable

            Dim dt As DataTable = Nothing
            Try
                Dim sqlquery As String = " 	CREATE TABLE #TempServidor( Id INT IDENTITY (1,1), fk_Servidor Int )" +
                 " INSERT INTO #TempServidor " +
                " SELECT DISTINCT id_Servidor FROM [DB_Miharu.Imaging_Core].[Config].[TBL_Proyecto] TP " +
                " INNER JOIN [DB_Miharu.Core].[Imaging].[TBL_Servidor] TS " +
                " ON TP.fk_Entidad_Servidor = TS.fk_Entidad " +
                " AND TS.fk_Servidor_Tipo = 1 " +
                " WHERE TP.fk_Entidad = @fk_entidad   DECLARE @SQL VARCHAR(MAX), @Servidores varchar(10),@BaseDatos varchar(50), @ServidorBaseDatos varchar(100) " +
                " DECLARE @Servidor INT, @intServidor INT, @SQLeft  VARCHAR(MAX), @SQLCase VARCHAR(MAX), @SQLConcat VARCHAR(MAX), @SQLConcatCase VARCHAR(MAX) " +
                " SET @intServidor = 1 " +
                " WHILE (@intServidor <= (SELECT COUNT(*) FROM #TempServidor))  BEGIN " +
                " SET @Servidor = (SELECT fk_Servidor FROM #TempServidor WHERE Id = @intServidor) " +
                " SET @Servidores = (SELECT  substring(Nombre_Servidor,20,9)   FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1)  " +
                " SET @BaseDatos =(SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(ConnectionString_Servidor,'SlygProvider=SqlServer;Data Source=10.64.118.42',''),'Initial Catalog=',''),';Persist Security Info=True;User ID=U_App_Core;Password=riskapp1120',''),'\',''),';',''),'Historico','') FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1) " +
                " IF(ISNULL(@Servidores,'') = '')  BEGIN " +
                " SET @ServidorBaseDatos = '['+ @BaseDatos + ']' " +
                " End " +
                " IF(ISNULL(@Servidores,'') <> '') " +
                " BEGIN " +
                " SET @ServidorBaseDatos = '['+ @Servidores + ']' +'.'+ '['+ @BaseDatos + ']' " +
                " End " +
                " IF @Servidor > 0 " +
                " BEGIN " +
                " SET @SQLCase = ' WHEN  File'+CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente IS NOT NULL THEN ' + CONVERT(VARCHAR(5),@Servidor) " +
                " SET  @SQLeft =  ' LEFT JOIN ' + @ServidorBaseDatos + '.Imaging.TBL_File File' + CONVERT(VARCHAR(5), @Servidor)  + '	 ON File' + CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente = Files.fk_Expediente AND File'+ CONVERT(VARCHAR(5),@Servidor) +'.fk_Folder = Files.fk_Folder AND File'+CONVERT(VARCHAR(5),@Servidor) +'.fk_File = Files.fk_File' " +
                " SET  @SQLConcat  =  CONCAT(@SQLConcat, @SQLeft); " +
                " SET  @SQLConcatCase = CONCAT(@SQLConcatCase, @SQLCase); " +
                " End " +
                " PRINT @intServidor " +
                " SET @intServidor = @intServidor + 1  " +
                " End " +
                          " SET @SQL = 'SELECT DISTINCT   Esquema.id_Esquema, Esquema.Nombre_Esquema, LLAVE.fk_Entidad,   LLAVE.fk_Proyecto, Folder.fk_Expediente,  " +
                                         " Folder.fk_Folder,   ProcessFolder.CBarras_Folder,   Process.Fn_getKey(Folder.fk_Expediente, 1, 0) AS Key_1,   " +
                                         "  Process.Fn_getKey(Folder.fk_Expediente, 2, 0) AS Key_2,   Process.Fn_getKey(Folder.fk_Expediente, 3, 0) AS Key_3, " +
                                           " Folder.fk_Entidad_Servidor,   Folder.fk_Servidor,   Folder.Fecha_Creacion  ' " +
                             " IF @Servidor > 0 " +
                          " BEGIN " +
                         "SET @SQL = @SQL +' ,CASE '  " +
                         " SET @SQL = CONCAT(@SQL, @SQLConcatCase); " +
                         " SET @SQL = @SQL +' END AS fk_ServidorStorage'  " +
                         " End " +
                         " SET @SQL = @SQL +'  " +
                         " INTO #TempExpedientes  " +
                         " FROM ##TempTableLogs T " +
                       " INNER JOIN [DB_Miharu.Core].[Process].[TBL_Expediente_Llave_Linea] LLAVE " +
                       " ON  T.Llave_01 = LLAVE.[Llave_01] " +
                       " INNER JOIN Core.CTA_Folder AS Folder " +
                       " ON LLAVE.fk_Expediente = Folder.fk_Expediente " +
                       " LEFT JOIN	Core.CTA_Process_Folder AS ProcessFolder " +
                         " ON	Folder.fk_Expediente = ProcessFolder.fk_Expediente " +
                         " AND Folder.fk_Folder = ProcessFolder.id_Folder " +
                         " INNER JOIN Core.CTA_File AS Files  " +
       " ON Folder.fk_Expediente = Files.fk_Expediente " +
                         " AND Folder.fk_Folder = Files.fk_Folder" +
       " INNER JOIN Core.CTA_Process_File AS ProcessFile " +
       " ON Files.fk_Expediente = ProcessFile.fk_Expediente " +
       " AND Files.fk_Folder = ProcessFile.fk_Folder " +
       " AND Files.fk_File = ProcessFile.id_File " +
                         " LEFT JOIN	Core.CTA_Esquema AS Esquema " +
                         " ON	LLAVE.fk_Entidad = Esquema.fk_Entidad " +
                         " AND LLAVE.fk_Proyecto = Esquema.fk_Proyecto " +
                         " AND LLAVE.fk_Esquema = Esquema.id_Esquema " +
                         " LEFT JOIN Core.CTA_Documento AS Documento " +
                         " ON ProcessFile.fk_Documento = Documento.id_Documento ' " +
                         "  SET @SQL = CONCAT(@SQL, @SQLConcat);  " +
                         "   SET @SQL = @SQL + '  WHERE  LLAVE.fk_Entidad = ' + CONVERT(VARCHAR(5),@fk_entidad) + ' AND LLAVE.fk_Proyecto IN ( " + Proyectos + " ) " +
                         " UPDATE #TempExpedientes    " +
                         "  SET fk_ServidorStorage = 1  WHERE(fk_ServidorStorage Is NULL)   " +
                         "   select  * from #TempExpedientes    " +
                         " DROP TABLE #TempExpedientes   '  EXEC (@SQL)  " +
                         "  DROP TABLE #TempServidor "

                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_entidad", Entidad)
               }
                dt = ExecuteQuery(sqlquery, dbmImaging, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return dt
        End Function
        Private Function getExportacion_Data(ByVal dbmImaging As DBImagingDataBaseManager, Proyectos As String) As DataTable
            Dim dt As DataTable = Nothing
            Try
                Dim sqlquery As String = " CREATE TABLE #TempServidor( Id INT IDENTITY (1,1), fk_Servidor Int )" +
                 " INSERT INTO #TempServidor " +
                " SELECT DISTINCT id_Servidor FROM [DB_Miharu.Imaging_Core].[Config].[TBL_Proyecto] TP " +
                " INNER JOIN [DB_Miharu.Core].[Imaging].[TBL_Servidor] TS " +
                " ON TP.fk_Entidad_Servidor = TS.fk_Entidad " +
                " AND TS.fk_Servidor_Tipo = 1 " +
                " WHERE TP.fk_Entidad = @fk_entidad   DECLARE @SQL VARCHAR(MAX), @Servidores varchar(10),@BaseDatos varchar(50), @ServidorBaseDatos varchar(100) " +
                " DECLARE @Servidor INT, @intServidor INT, @SQLeft  VARCHAR(MAX), @SQLCase VARCHAR(MAX), @SQLConcat VARCHAR(MAX), @SQLConcatCase VARCHAR(MAX) " +
                " SET @intServidor = 1 " +
                " WHILE (@intServidor <= (SELECT COUNT(*) FROM #TempServidor))  BEGIN " +
                " SET @Servidor = (SELECT fk_Servidor FROM #TempServidor WHERE Id = @intServidor) " +
                " SET @Servidores = (SELECT  substring(Nombre_Servidor,20,9)   FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1)  " +
                " SET @BaseDatos =(SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(ConnectionString_Servidor,'SlygProvider=SqlServer;Data Source=10.64.118.42',''),'Initial Catalog=',''),';Persist Security Info=True;User ID=U_App_Core;Password=riskapp1120',''),'\',''),';',''),'Historico','') FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1) " +
                " IF(ISNULL(@Servidores,'') = '')  BEGIN " +
                " SET @ServidorBaseDatos = '['+ @BaseDatos + ']' " +
                " End " +
                " IF(ISNULL(@Servidores,'') <> '') " +
                " BEGIN " +
                " SET @ServidorBaseDatos = '['+ @Servidores + ']' +'.'+ '['+ @BaseDatos + ']' " +
                " End " +
                " IF @Servidor > 0 " +
                " BEGIN " +
                " SET @SQLCase = ' WHEN  File'+CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente IS NOT NULL THEN ' + CONVERT(VARCHAR(5),@Servidor) " +
                " SET  @SQLeft =  ' LEFT JOIN ' + @ServidorBaseDatos + '.Imaging.TBL_File File' + CONVERT(VARCHAR(5), @Servidor)  + '	 ON File' + CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente = Files.fk_Expediente AND File'+ CONVERT(VARCHAR(5),@Servidor) +'.fk_Folder = Files.fk_Folder AND File'+CONVERT(VARCHAR(5),@Servidor) +'.fk_File = Files.fk_File' " +
                " SET  @SQLConcat  =  CONCAT(@SQLConcat, @SQLeft); " +
                " SET  @SQLConcatCase = CONCAT(@SQLConcatCase, @SQLCase); " +
                " End " +
                " PRINT @intServidor " +
                " SET @intServidor = @intServidor + 1  " +
                " End " +
                     " SET @SQL = 'SELECT  Campo.Es_Campo_Busqueda,  Files.fk_Expediente, " +
                                   " Files.fk_Folder, Files.fk_File, Files.id_Version, Campo.fk_Documento, " +
                                   " Campo.id_Campo, Campo.fk_Campo_Tipo, Campo.fk_Campo_Busqueda, Campo.Nombre_Campo, " +
                     "isnull(Process.Fn_getFileData(Folder.fk_Expediente, Folder.fk_Folder, Files.fk_File, FileData.fk_Documento, FileData.fk_Campo, 0),'''') AS Valor_File_Data , LLAVE.fk_Proyecto' " +
                        " IF @Servidor > 0 " +
                     " BEGIN " +
                    "SET @SQL = @SQL +' ,CASE '  " +
                    " SET @SQL = CONCAT(@SQL, @SQLConcatCase); " +
                    " SET @SQL = @SQL +' END AS fk_ServidorStorage'  " +
                    " End " +
                    " SET @SQL = @SQL +'  " +
                    " INTO #TempExpedientes  " +
                    " FROM	##TempTableLogs T  " +
     " INNER JOIN	[DB_Miharu.Core].[Process].[TBL_Expediente_Llave_Linea] LLAVE   " +
                    " ON T.Llave_01 = LLAVE.[Llave_01] " +
     " INNER JOIN Core.CTA_Folder AS Folder " +
     " ON LLAVE.fk_Expediente = Folder.fk_Expediente " +
                    " INNER JOIN Core.CTA_File AS Files  " +
     " ON	Folder.fk_Expediente = Files.fk_Expediente   AND Folder.fk_Folder = Files.fk_Folder	   " +
                    " LEFT JOIN	Core.CTA_File_Data AS FileData   ON	Files.fk_Expediente = FileData.fk_Expediente   " +
                    " AND Files.fk_Folder = FileData.fk_Folder  AND Files.fk_File = FileData.fk_File  " +
                    " LEFT JOIN	Core.CTA_Campo AS Campo   ON	FileData.fk_Documento = Campo.fk_Documento   " +
                    " AND FileData.fk_Campo = Campo.id_Campo	' " +
                       "  SET @SQL = CONCAT(@SQL, @SQLConcat);  " +
                      "    SET @SQL = @SQL + '  WHERE  LLAVE.fk_Entidad = ' + CONVERT(VARCHAR(5),@fk_entidad) + ' AND LLAVE.fk_Proyecto IN ( " + Proyectos + " ) " +
                       " UPDATE #TempExpedientes    " +
                       "  SET fk_ServidorStorage = 1  WHERE(fk_ServidorStorage Is NULL)   " +
                        "   select  * from #TempExpedientes    " +
                        " DROP TABLE #TempExpedientes   '  EXEC (@SQL)  " +
                      "  DROP TABLE #TempServidor "

                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_entidad", Entidad)
               }
                dt = ExecuteQuery(sqlquery, dbmImaging, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return dt
        End Function
        Private Function getExportacion_Validaciones(ByVal dbmImaging As DBImagingDataBaseManager, Proyectos As String) As DataTable
            Dim dt As DataTable = Nothing
            Try
                Dim sqlquery As String = "	CREATE TABLE #TempServidor( Id INT IDENTITY (1,1), fk_Servidor Int )" +
                 " INSERT INTO #TempServidor " +
                " SELECT DISTINCT id_Servidor FROM [DB_Miharu.Imaging_Core].[Config].[TBL_Proyecto] TP " +
                " INNER JOIN [DB_Miharu.Core].[Imaging].[TBL_Servidor] TS " +
                " ON TP.fk_Entidad_Servidor = TS.fk_Entidad " +
                " AND TS.fk_Servidor_Tipo = 1 " +
                " WHERE TP.fk_Entidad = @fk_entidad   DECLARE @SQL VARCHAR(MAX), @Servidores varchar(10),@BaseDatos varchar(50), @ServidorBaseDatos varchar(100) " +
                " DECLARE @Servidor INT, @intServidor INT, @SQLeft  VARCHAR(MAX), @SQLCase VARCHAR(MAX), @SQLConcat VARCHAR(MAX), @SQLConcatCase VARCHAR(MAX) " +
                " SET @intServidor = 1 " +
                " WHILE (@intServidor <= (SELECT COUNT(*) FROM #TempServidor))  BEGIN " +
                " SET @Servidor = (SELECT fk_Servidor FROM #TempServidor WHERE Id = @intServidor) " +
                " SET @Servidores = (SELECT  substring(Nombre_Servidor,20,9)   FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1)  " +
                " SET @BaseDatos =(SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(ConnectionString_Servidor,'SlygProvider=SqlServer;Data Source=10.64.118.42',''),'Initial Catalog=',''),';Persist Security Info=True;User ID=U_App_Core;Password=riskapp1120',''),'\',''),';',''),'Historico','') FROM [DB_Miharu.Core].[Imaging].[TBL_Servidor] WHERE id_Servidor = @Servidor AND fk_Servidor_Tipo = 1 AND fk_Entidad =1) " +
                " IF(ISNULL(@Servidores,'') = '')  BEGIN " +
                " SET @ServidorBaseDatos = '['+ @BaseDatos + ']' " +
                " End " +
                " IF(ISNULL(@Servidores,'') <> '') " +
                " BEGIN " +
                " SET @ServidorBaseDatos = '['+ @Servidores + ']' +'.'+ '['+ @BaseDatos + ']' " +
                " End " +
                " IF @Servidor > 0 " +
                " BEGIN " +
                " SET @SQLCase = ' WHEN  File'+CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente IS NOT NULL THEN ' + CONVERT(VARCHAR(5),@Servidor) " +
                " SET  @SQLeft =  ' LEFT JOIN ' + @ServidorBaseDatos + '.Imaging.TBL_File File' + CONVERT(VARCHAR(5), @Servidor)  + '	 ON File' + CONVERT(VARCHAR(5),@Servidor) + '.fk_Expediente = Files.fk_Expediente AND File'+ CONVERT(VARCHAR(5),@Servidor) +'.fk_Folder = Files.fk_Folder AND File'+CONVERT(VARCHAR(5),@Servidor) +'.fk_File = Files.fk_File' " +
                " SET  @SQLConcat  =  CONCAT(@SQLConcat, @SQLeft); " +
                " SET  @SQLConcatCase = CONCAT(@SQLConcatCase, @SQLCase); " +
                " End " +
                " PRINT @intServidor " +
                " SET @intServidor = @intServidor + 1  " +
                " End " +
                     " SET @SQL = 'SELECT  Files.fk_Expediente, Files.fk_Folder,Files.fk_File, " +
                                        " Files.id_Version, Validacion.fk_Documento, Validacion.id_Validacion, Validacion.Pregunta_Validacion_Reporte AS Pregunta_Validacion,  " +
                                     " FileValidacion.Respuesta, LLAVE.fk_Proyecto ' " +
                        " IF @Servidor > 0 " +
                     " BEGIN " +
                    "SET @SQL = @SQL +' ,CASE '  " +
                    " SET @SQL = CONCAT(@SQL, @SQLConcatCase); " +
                    " SET @SQL = @SQL +' END AS fk_ServidorStorage'  " +
                    " End " +
                    " SET @SQL = @SQL +'  " +
                    " INTO #TempExpedientes  " +
    " FROM	##TempTableLogs T  " +
    " INNER JOIN	[DB_Miharu.Core].[Process].[TBL_Expediente_Llave_Linea] LLAVE   " +
    " ON T.Llave_01 = LLAVE.[Llave_01] " +
    " INNER JOIN Core.CTA_Folder AS Folder " +
    " ON LLAVE.fk_Expediente = Folder.fk_Expediente " +
    " INNER JOIN Core.CTA_File AS Files  " +
    " ON	Folder.fk_Expediente = Files.fk_Expediente   AND Folder.fk_Folder = Files.fk_Folder	   " +
                " LEFT JOIN  Core.CTA_File_Validacion AS FileValidacion   " +
                " ON	Files.fk_Expediente = FileValidacion.fk_Expediente   " +
                " AND Files.fk_Folder = FileValidacion.fk_Folder   " +
                " AND Files.fk_File = FileValidacion.fk_File   " +
                " LEFT JOIN	Core.CTA_Config_Validacion AS Validacion   " +
                " ON	FileValidacion.fk_Documento = Validacion.fk_Documento   " +
                " AND FileValidacion.fk_Validacion = Validacion.id_Validacion  " +
                " LEFT JOIN Core.CTA_Process_File AS ProcessFile  " +
                " ON Files.fk_Expediente = ProcessFile.fk_Expediente " +
                " AND Files.fk_Folder = ProcessFile.fk_Folder  " +
                " AND Files.fk_File = ProcessFile.id_File " +
                " LEFT JOIN Core.CTA_Documento AS Documento " +
                " ON ProcessFile.fk_Documento = Documento.id_Documento " +
                " LEFT JOIN Config.TBL_Documentos_Agrupar_Exportacion_Imagenes Doc_Agrupador " +
                " ON ProcessFile.fk_Documento = Doc_Agrupador.fk_Documento ' " +
                       "  SET @SQL = CONCAT(@SQL, @SQLConcat);  " +
                      "  SET @SQL = @SQL + '  WHERE  LLAVE.fk_Entidad = ' + CONVERT(VARCHAR(5),@fk_entidad) + ' AND LLAVE.fk_Proyecto IN ( " + Proyectos + " ) " +
                       " UPDATE #TempExpedientes    " +
                       "  SET fk_ServidorStorage = 1  WHERE(fk_ServidorStorage Is NULL)   " +
                      "   select  * from #TempExpedientes    " +
                        " DROP TABLE #TempExpedientes '  EXEC (@SQL)  " +
                      "  DROP TABLE #TempServidor   " +
                      " DROP TABLE ##TempTableLogs "


                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_entidad", Entidad)
               }
                dt = ExecuteQuery(sqlquery, dbmImaging, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return dt
        End Function
        Private Function ExecuteQuery(ByVal s As String, ByVal dbmImaging As DBImagingDataBaseManager, ByVal ParamArray params() As SqlParameter) As DataTable
            Dim dt As DataTable = Nothing
            Using da As New System.Data.SqlClient.SqlDataAdapter(s, dbmImaging.DataBase.ConnectionString)
                Try
                    dt = New DataTable
                    If params.Length > 0 Then
                        da.SelectCommand.Parameters.AddRange(params)
                    End If
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(dt)
                    Return dt
                Catch ex As SqlException
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                Return dt
            End Using
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

    Public Class FileDataTable
        Public fk_Entidad As Integer
        Public fk_Proyecto As Integer
        Public id_Esquema As Integer
        Public Nombre_Esquema As String
        Public fk_Expediente As Long
        Public fk_Folder As Integer
        Public fk_File As Integer
        Public id_Version As Integer
        Public File_Unique_Identifier As String
        Public Nombre_Imagen_File As String
        Public Folios_Documento_File As Integer
        Public Tamaño_Imagen_File As String
        Public fk_Documento As Integer
        Public Nombre_Documento As String
        Public fk_Grupo As Integer
        Public Llave_01 As String
        Public Llave_02 As String
        Public fk_Tipologia As Integer
        Public fk_ServidorStorage As Integer
    End Class

End Namespace