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
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Data.SqlClient
Imports DBImaging

Namespace Imaging.Exportar
    Public Class FormExport

#Region " Declaraciones "
        Private Usa_Exportacion_PDF As Boolean
        Private formatoAux As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Private Extension As String
        Private CompresionAux As Slyg.Tools.Imaging.ImageManager.EnumCompression
        Private formato As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Dim compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression

        Private FoldersExportacionTemp As New List(Of String)
        Private ViewOts As New DataView
        Private OTsSeleccion As New DataTable
        Public Shared FileNamesCons As New List(Of String)
        Dim FolderNameOutput As String

        Dim _EventManager As EventManager
        Dim PesoDeceval As Integer
        Dim reduccionPeso As Integer = 20

        Private Segundos As Integer = 0
        Private Minuto As Integer = 0
        Private Hora As Integer = 0
        Private _File As Stream = Nothing
        Private _Registros As DataTable
        Private _RegistrosF As DataTable
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

        Private Sub BuscarFechaButton_Click(sender As System.Object, e As EventArgs)
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
            If TabControlExportacion.SelectedIndex = 0 Then
                'Exportar()
            ElseIf TabControlExportacion.SelectedIndex = 1 Then
                ExportarConLog()
            End If
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

        Private Sub OTDataGridView_ColumnHeaderMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
            If e.ColumnIndex = 3 Then
                For i = 0 To OTDataGridView.RowCount - 1
                    OTDataGridView.Rows(i).Cells("ColumnExportar").Value = True
                Next
            End If
        End Sub

        Private Sub BuscarArchivoButton_Click(sender As System.Object, e As System.EventArgs) Handles BuscarArchivoButton.Click
            BuscarArchivo()
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

        Private Sub ExportarConLog()
            If Validar() Then
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim totalesDataTable As DBCore.SchemaProcess.CTA_Exportacion_Totales_LogDataTable = Nothing
                Dim progressForm As New Miharu.Tools.Progress.FormProgress

                Try
                    If Me._File IsNot Nothing Then
                        dbmCore = New DBCore.DBCoreDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                        dbmCore.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                        dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                        Dim csvData As New DBCore.CSVData() 'Slyg.Tools.CSV.CSVData()
                        Dim caracter As Char

                        caracter = ";"c

                        csvData.LoadCSV(ArchivoDesktopTextBox.Text, True, caracter, "")
                        _Registros = csvData.DataTable.ToDataTable()

                        If Not (_Registros Is Nothing) Then
                            Dim tablaTemp = "##RegistrosLog_" & DateTime.Now.ToString("HHmmss")
                            BulkInsert.InsertDataTableReport(_Registros, dbmImaging, tablaTemp)

                            'totalesDataTable = dbmCore.SchemaProcess.PA_Exportacion_Totales_Log.DBExecute(Plugin.Manager.ImagingGlobal.Entidad, Plugin.Manager.ImagingGlobal.Proyecto)

                            'If (totalesDataTable.Rows.Count > 0) Then
                            'Dim Respuesta As DialogResult

                            'Respuesta = MessageBox.Show("Se encontró : " & vbCrLf & _
                            '                            totalesDataTable(0).Folders & " Unidades Documentales, " & vbCrLf & _
                            '                            totalesDataTable(0).Files & " Documentos, " & vbCrLf & _
                            '                            totalesDataTable(0).Folios & " Folios con " & vbCrLf & _
                            '                            (totalesDataTable(0).Tamaño / 1024 / 1024).ToString("#,##0.00") & "MB de tamaño, " & vbCrLf & _
                            '                            "¿Desea Exportar esta información?", Program.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                            'If Respuesta = DialogResult.Yes Then
                            Dim FileDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable
                            'Dim fileDataTable = dbmCore.SchemaReport.PA_Generar_Archivo_Deceval_Log.DBExecute(Plugin.Manager.ImagingGlobal.Entidad, Plugin.Manager.ImagingGlobal.Proyecto)
                            Dim Consulta As String = Replace(My.Resources.Consulta, "@fk_Entidad", Plugin.Manager.ImagingGlobal.Entidad)
                            Consulta = Replace(Consulta, "@fk_Proyecto", Plugin.Manager.ImagingGlobal.Proyecto)
                            Consulta = Replace(Consulta, "@TablaTemp", tablaTemp)

                            'ejecutarconsulta
                            Dim dt As DataTable = Nothing
                            FileDataTable = ExecuteQuery(Consulta, dbmImaging)
                            'FileDataTable = CType(dt, DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable)

                            If FileDataTable.Rows.Count > 0 Then
                                Me.Enabled = False

                                Dim Progreso As Integer = 0
                                progressForm.SetProceso("Exportar")
                                progressForm.SetAccion("Obteniendo imágenes...")
                                progressForm.SetProgreso(0)
                                'progressForm.SetMaxValue(totalesDataTable(0).Files)
                                progressForm.SetMaxValue(FileDataTable.Rows.Count)
                                Application.DoEvents()
                                progressForm.Show()

                                Dim Compresion As ImageManager.EnumCompression

                                If (Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                                    Compresion = ImageManager.EnumCompression.Ccitt4
                                Else
                                    Compresion = ImageManager.EnumCompression.Lzw
                                End If

                                'Se crea Tabla de OTs
                                Dim Ots As List(Of Object) = Nothing
                                Ots = (From a In FileDataTable Group a By groupDt = a.Field(Of Integer)("id_OT") Into Group Select Group.Select(Function(x) x("id_OT")).First()).ToList()

                                For Each ot As Integer In Ots
                                    Dim FilesbyOT = FileDataTable.Select("id_OT = " + ot.ToString()).CopyToDataTable

                                    'Dim CantFolders As Integer = 0
                                    'Dim CantFiles As Integer = 0

                                    'For Each filebyot As DataRow In FilesbyOT.Rows
                                    '    CantFolders = CantFolders + filebyot.Item("fk_Folder")
                                    '    CantFiles = CantFiles + filebyot.Item("fk_File")
                                    'Next


                                    Dim reduccionPesoDataTable As Integer = 15 'dbmCore.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(Plugin.Manager.ImagingGlobal.Entidad, Plugin.Manager.ImagingGlobal.Proyecto, "ReduccionPeso")

                                    'If reduccionPesoDataTable.Rows.Count > 0 Then
                                    '    reduccionPeso = Convert.ToInt32(reduccionPesoDataTable(0).Valor_Parametro_Sistema)
                                    'End If

                                    Dim ServidoresDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(ot)
                                    'Dim OutputFolder As String = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\" & ot.ToString("0000000000") & "\"
                                    Dim OutputFolder As String = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"

                                    If (Not Directory.Exists(OutputFolder)) Then
                                        Directory.CreateDirectory(OutputFolder)
                                    End If

                                    Dim FilesDataView As New DataView(FileDataTable)

                                    ''Define Nombre del Archivo Deceval
                                    'Dim NomArchivo = "E" & Format(Now(), "yyyyMMddHHmmss")
                                    'Dim idImagen As Integer = 1

                                    'Dim id_Exportacion_Global = dbmImaging.SchemaReport.PA_Set_Exportacion_Global.DBExecute(Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _
                                    '                                                                                Plugin.Manager.ImagingGlobal.Entidad, _
                                    '                                                                                Plugin.Manager.ImagingGlobal.Proyecto, _
                                    '                                                                                ot, _
                                    '                                                                                Plugin.Manager.Sesion.Usuario.id, _
                                    '                                                                                CantFolders, _
                                    '                                                                                CantFiles, _
                                    '                                                                                OutputFolder, _
                                    '                                                                                NomArchivo)

                                    'Dim swReporte = New StreamWriter(File.Open(OutputFolder & NomArchivo & ".csv", FileMode.Create), System.Text.Encoding.UTF8)
                                    'Dim swNombreDocumento = New StreamWriter(File.Open(OutputFolder & NomArchivo.Replace("E", "EA") & ".csv", FileMode.Create), System.Text.Encoding.UTF8)

                                    'swReporte.Close()
                                    'swReporte.Dispose()
                                    'swNombreDocumento.Close()
                                    'swNombreDocumento.Dispose()

                                    For Each RowServidor In ServidoresDataTable
                                        Dim manager As FileProviderManager = Nothing

                                        Try
                                            Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(RowServidor.fk_Entidad, RowServidor.id_Servidor)(0).ToCTA_ServidorSimpleType
                                            Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                                            manager = New FileProviderManager(servidor, centro, dbmImaging, Plugin.Manager.Sesion.Usuario.id)
                                            manager.Connect()

                                            ' Obtener los Files a transferir
                                            FilesDataView.RowFilter = "id_OT = " & ot & "AND fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor

                                            For Each ItemFile As DataRowView In FilesDataView
                                                If progressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                                                'Dim RowFile = CType(ItemFile.Row, DBCore.SchemaReport.CTA_Exportacion_Files_DecevalRow)
                                                Dim RowFile = CType(ItemFile.Row, DBImaging.SchemaProcess.CTA_Exportacion_FilesRow)
                                                'Dim TempFileName As String

                                                ' Enviar el archivo
                                                'TempFileName = ExportarImagen(manager, RowFile, Compresion, reduccionPeso)
                                                'ExportarImagen(manager, RowFile, Compresion, reduccionPeso)
                                                ExportarImagen(manager, RowFile, Compresion, OutputFolder, OutputFolder)

                                                'Dim NombreImagen = NomArchivo & "_" & idImagen.ToString("00000") & Extension
                                                'File.Move(TempFileName, OutputFolder & NombreImagen)
                                                'File.Move(TempFileName, OutputFolder & RowFile.Nombre_Imagen_File)

                                                'swReporte = New StreamWriter(File.Open(OutputFolder & NomArchivo & ".csv", FileMode.Append), System.Text.Encoding.UTF8)
                                                'swNombreDocumento = New StreamWriter(File.Open(OutputFolder & NomArchivo.Replace("E", "EA") & ".csv", FileMode.Append), System.Text.Encoding.UTF8)

                                                'RowFile.Reporte = RowFile.Reporte.Replace(RowFile.Nombre_Imagen_File, NombreImagen)
                                                'RowFile.Nombre_Imagen_File = NombreImagen
                                                'swReporte.WriteLine(RowFile.Reporte)
                                                'swNombreDocumento.WriteLine(RowFile.Nombre_Documento)

                                                'swReporte.Close()
                                                'swReporte.Dispose()
                                                'swNombreDocumento.Close()
                                                'swNombreDocumento.Dispose()

                                                'idImagen += 1

                                                Progreso += 1
                                                progressForm.SetProgreso(Progreso)
                                                Application.DoEvents()
                                            Next
                                        Catch ex As Exception
                                            If (manager IsNot Nothing) Then manager.Disconnect()
                                            Throw
                                        Finally
                                            If (manager IsNot Nothing) Then manager.Disconnect()
                                        End Try
                                    Next

                                    ''Actualizar nombre de imagen, nombre de archivo Y estado del file a generado
                                    'Dim RegistrosAct As DataTable
                                    'RegistrosAct = FileDataTable.Select("id_OT = " + ot.ToString()).CopyToDataTable
                                    'BulkInsert.InsertDataTableReport(RegistrosAct, dbmImaging, "#RegistrosLogAct")
                                    'dbmImaging.SchemaReport.PA_Actualizar_Registros_Exportacion_Global.DBExecute(Plugin.Manager.ImagingGlobal.Entidad, Plugin.Manager.ImagingGlobal.Proyecto, id_Exportacion_Global)

                                    ''Fin Exportación por OT
                                    'Dim ExportacionGlobalType = New DBImaging.SchemaProcess.TBL_Exportacion_GlobalType()
                                    'ExportacionGlobalType.Fecha_Fin_Exportacion = SlygNullable.SysDate
                                    'dbmImaging.SchemaProcess.TBL_Exportacion_Global.DBUpdate(ExportacionGlobalType, id_Exportacion_Global)
                                Next

                                Me.Enabled = True
                                MessageBox.Show("La información se exportó con éxito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                            'Else
                            '    MessageBox.Show("Operación cancelada por Usuario", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            'End If
                            'End If
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    progressForm.Hide()
                    Application.DoEvents()
                Finally
                    Me.Enabled = True

                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()

                    progressForm.Close()
                End Try
            End If
        End Sub

        'Private Sub Exportar()
        '    If (Validar()) Then
        '        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        '        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
        '        Dim totalesDataTable As DBImaging.SchemaProcess.CTA_Exportacion_TotalesDataTable = Nothing
        '        Dim progressForm As New Miharu.Tools.Progress.FormProgress
        '        Dim otRow = CType(CType(Me.OTDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Exportacion_OTRow)

        '        If LlenaOtsSeleccion() Then
        '            Try
        '                dbmImaging = New DBImaging.DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
        '                dbmCore = New DBCore.DBCoreDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

        '                dbmCore.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
        '                dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

        '                Dim Folders As Integer = 0
        '                Dim Files As Integer = 0
        '                Dim Folios As Integer = 0
        '                Dim Tamaño As Double = 0

        '                For Each row As DataRow In ViewOts.ToTable.Rows
        '                    totalesDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Totales.DBExecute(CInt(row(0)), Nothing)
        '                    If totalesDataTable.Rows.Count > 0 Then
        '                        Folders += CInt(totalesDataTable(0).Folders)
        '                        Files += CInt(totalesDataTable(0).Files)
        '                        Folios += CInt(totalesDataTable(0).Folios)
        '                        Tamaño += CDbl(totalesDataTable(0).Tamaño)
        '                    End If
        '                Next


        '                Dim Respuesta As DialogResult

        '                Respuesta = MessageBox.Show("Se encontró : " & vbCrLf & _
        '                                            Folders & " Unidades Documentales, " & vbCrLf & _
        '                                            Files & " Documentos, " & vbCrLf & _
        '                                            Folios & " Folios con " & vbCrLf & _
        '                                            (Tamaño / 1024 / 1024).ToString("#,##0.00") & "MB de tamaño, " & vbCrLf & _
        '                                            "¿Desea Exportar esta información?", Program.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)


        '                If Respuesta = DialogResult.Yes Then
        '                    Me.Enabled = False

        '                    'Se crea Tabla de OTs
        '                    Dim OTs As New DataTable
        '                    OTs = ViewOts.ToTable(True, "id_OT")

        '                    If OTs.Rows.Count > 0 Then

        '                        Dim reduccionPesoDataTable = dbmCore.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(Plugin.Manager.ImagingGlobal.Entidad, Plugin.Manager.ImagingGlobal.Proyecto, "ReduccionPeso")

        '                        If reduccionPesoDataTable.Rows.Count > 0 Then
        '                            reduccionPeso = Convert.ToInt32(reduccionPesoDataTable(0).Valor_Parametro_Sistema)
        '                        End If

        '                        Dim Progreso As Integer = 0
        '                        progressForm.SetProceso("Exportar")
        '                        progressForm.SetAccion("Obteniendo imágenes...")
        '                        progressForm.SetProgreso(0)
        '                        progressForm.SetMaxValue(Files)
        '                        Application.DoEvents()
        '                        progressForm.Show()


        '                        Dim Compresion As ImageManager.EnumCompression

        '                        If (Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
        '                            Compresion = ImageManager.EnumCompression.Ccitt4
        '                        Else
        '                            Compresion = ImageManager.EnumCompression.Lzw
        '                        End If

        '                        For Each rowOt As DataRow In OTs.Rows
        '                            Dim NomArchivo = "E" & Format(Now(), "yyyyMMddHHmmss")
        '                            Dim ServidoresDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(CInt(rowOt(0)))
        '                            Dim totalesDataTableOT = dbmImaging.SchemaProcess.PA_Exportacion_Totales.DBExecute(CInt(rowOt(0)), Nothing)

        '                            Dim OutputFolder As String = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\" & CInt(rowOt(0)).ToString("0000000000") & "\"

        '                            If (Not Directory.Exists(OutputFolder)) Then
        '                                Directory.CreateDirectory(OutputFolder)
        '                            End If

        '                            Dim id_Exportacion_Global = dbmImaging.SchemaReport.PA_Set_Exportacion_Global.DBExecute(Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _
        '                                                                                                                    Plugin.Manager.ImagingGlobal.Entidad, _
        '                                                                                                                    Plugin.Manager.ImagingGlobal.Proyecto, _
        '                                                                                                                    otRow.id_OT, _
        '                                                                                                                    Plugin.Manager.Sesion.Usuario.id, _
        '                                                                                                                    totalesDataTableOT(0).Folders, _
        '                                                                                                                    totalesDataTableOT(0).Files, _
        '                                                                                                                    OutputFolder, _
        '                                                                                                                    NomArchivo)

        '                            Dim fileDataTable = dbmCore.SchemaReport.PA_Generar_Archivo_Deceval.DBExecute(otRow.id_OT)

        '                            Dim FilesDataView As New DataView(fileDataTable)

        '                            Dim idImagen As Integer = 1

        '                            Dim swReporte = New StreamWriter(File.Open(OutputFolder & NomArchivo & ".csv", FileMode.Create), System.Text.Encoding.UTF8)
        '                            Dim swNombreDocumento = New StreamWriter(File.Open(OutputFolder & NomArchivo.Replace("E", "EA") & ".csv", FileMode.Create), System.Text.Encoding.UTF8)

        '                            swReporte.Close()
        '                            swReporte.Dispose()
        '                            swNombreDocumento.Close()
        '                            swNombreDocumento.Dispose()

        '                            For Each RowServidor In ServidoresDataTable
        '                                Dim manager As FileProviderManager = Nothing

        '                                Try
        '                                    Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(RowServidor.fk_Entidad, RowServidor.id_Servidor)(0).ToCTA_ServidorSimpleType
        '                                    Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

        '                                    manager = New FileProviderManager(servidor, centro, dbmImaging, Plugin.Manager.Sesion.Usuario.id)
        '                                    manager.Connect()

        '                                    ' Obtener los Files a transferir
        '                                    FilesDataView.RowFilter = "fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor

        '                                    For Each ItemFile As DataRowView In FilesDataView
        '                                        If progressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

        '                                        Dim RowFile = CType(ItemFile.Row, DBCore.SchemaReport.CTA_Exportacion_Files_DecevalRow)

        '                                        ' Enviar el archivo
        '                                        Dim TempFileName = ExportarImagen(manager, RowFile, Compresion, reduccionPeso)

        '                                        Dim NombreImagen = NomArchivo & "_" & idImagen.ToString("00000") & Extension
        '                                        File.Move(TempFileName, OutputFolder & NombreImagen)

        '                                        swReporte = New StreamWriter(File.Open(OutputFolder & NomArchivo & ".csv", FileMode.Append), System.Text.Encoding.UTF8)
        '                                        swNombreDocumento = New StreamWriter(File.Open(OutputFolder & NomArchivo.Replace("E", "EA") & ".csv", FileMode.Append), System.Text.Encoding.UTF8)

        '                                        RowFile.Reporte = RowFile.Reporte.Replace(RowFile.Nombre_Imagen_File, NombreImagen)
        '                                        RowFile.Nombre_Imagen_File = NombreImagen
        '                                        swReporte.WriteLine(RowFile.Reporte)
        '                                        swNombreDocumento.WriteLine(RowFile.Nombre_Documento)

        '                                        swReporte.Close()
        '                                        swReporte.Dispose()
        '                                        swNombreDocumento.Close()
        '                                        swNombreDocumento.Dispose()

        '                                        idImagen += 1

        '                                        Progreso += 1
        '                                        progressForm.SetProgreso(Progreso)
        '                                        Application.DoEvents()
        '                                    Next
        '                                Catch ex As Exception
        '                                    If (manager IsNot Nothing) Then manager.Disconnect()
        '                                    Throw
        '                                End Try
        '                            Next

        '                            'Actualizar nombre de imagen, nombre de archivo Y estado del file a generado
        '                            Dim RegistrosAct As DataTable
        '                            RegistrosAct = fileDataTable.Select("id_OT = " + otRow.id_OT.ToString()).CopyToDataTable
        '                            BulkInsert.InsertDataTableReport(RegistrosAct, dbmImaging, "#RegistrosLogAct")
        '                            dbmImaging.SchemaReport.PA_Actualizar_Registros_Exportacion_Global.DBExecute(Plugin.Manager.ImagingGlobal.Entidad, Plugin.Manager.ImagingGlobal.Proyecto, id_Exportacion_Global)

        '                            'Fin Exportación por OT
        '                            Dim ExportacionGlobalType = New DBImaging.SchemaProcess.TBL_Exportacion_GlobalType()
        '                            ExportacionGlobalType.Fecha_Fin_Exportacion = SlygNullable.SysDate
        '                            dbmImaging.SchemaProcess.TBL_Exportacion_Global.DBUpdate(ExportacionGlobalType, id_Exportacion_Global)

        '                            ' Crear la exportación
        '                            Dim ExportacionType = New DBImaging.SchemaProcess.TBL_ExportacionType()
        '                            ExportacionType.fk_Exportacion_Tipo = DBImaging.TipoExportacionEnum.PLIGIN
        '                            ExportacionType.Fecha_Exportacion = SlygNullable.SysDate
        '                            ExportacionType.fk_Usuario = Plugin.Manager.Sesion.Usuario.id
        '                            ExportacionType.fk_Sede_Procesamiento = Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Sede
        '                            ExportacionType.fk_Centro_Procesamiento = Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Centro_Procesamiento
        '                            ExportacionType.fk_Puesto_Trabajo = Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.id_Puesto_Trabajo
        '                            ExportacionType.Total_Folders = totalesDataTable(0).Folders
        '                            ExportacionType.Total_Files = totalesDataTable(0).Files
        '                            ExportacionType.Ruta = OutputFolder

        '                            Dim OTType = New DBImaging.SchemaProcess.TBL_OTType()
        '                            OTType.Exportado = True

        '                            Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBGet(otRow.id_OT)
        '                            Dim Exportado As Boolean = False
        '                            If (Not OTDataTable(0).Isfk_ExportacionNull()) Then
        '                                Exportado = dbmImaging.SchemaProcess.TBL_Exportacion.DBGet(OTDataTable(0).fk_Entidad_Procesamiento, OTDataTable(0).fk_Entidad, OTDataTable(0).fk_Proyecto, OTDataTable(0).fk_fecha_proceso, OTDataTable(0).fk_Exportacion).Count > 0
        '                            End If

        '                            Try
        '                                dbmImaging.Transaction_Begin()

        '                                If (Exportado) Then
        '                                    dbmImaging.SchemaProcess.TBL_Exportacion.DBUpdate(ExportacionType, OTDataTable(0).fk_Entidad_Procesamiento, OTDataTable(0).fk_Entidad, OTDataTable(0).fk_Proyecto, OTDataTable(0).fk_fecha_proceso, OTDataTable(0).fk_Exportacion)

        '                                    OTType.fk_Exportacion = OTDataTable(0).fk_Exportacion
        '                                Else
        '                                    ExportacionType.fk_Entidad_Procesamiento = OTDataTable(0).fk_Entidad_Procesamiento
        '                                    ExportacionType.fk_Entidad = OTDataTable(0).fk_Entidad
        '                                    ExportacionType.fk_Proyecto = OTDataTable(0).fk_Proyecto
        '                                    ExportacionType.fk_Fecha_Proceso = OTDataTable(0).fk_fecha_proceso
        '                                    ExportacionType.id_Exportacion = dbmImaging.SchemaProcess.TBL_Exportacion.DBNextId(OTDataTable(0).fk_Entidad_Procesamiento, OTDataTable(0).fk_Entidad, OTDataTable(0).fk_Proyecto, OTDataTable(0).fk_fecha_proceso)

        '                                    dbmImaging.SchemaProcess.TBL_Exportacion.DBInsert(ExportacionType)

        '                                    OTType.fk_Exportacion = ExportacionType.id_Exportacion
        '                                End If

        '                                dbmImaging.SchemaProcess.TBL_OT.DBUpdate(OTType, otRow.id_OT)

        '                                dbmImaging.Transaction_Commit()
        '                            Catch
        '                                dbmImaging.Transaction_Rollback()
        '                                Throw
        '                            End Try
        '                        Next
        '                    End If
        '                    MessageBox.Show("La información se exportó con éxito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                    CargarOTs()
        '                Else
        '                    MessageBox.Show("Operación cancelada por Usuario", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '                End If
        '            Catch ex As Exception
        '                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)

        '                progressForm.Hide()
        '                Application.DoEvents()

        '                Return
        '            Finally
        '                Me.Enabled = True

        '                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        '                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()

        '                BorrarTemporal()

        '                progressForm.Close()
        '            End Try

        '        End If
        '    End If
        'End Sub

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

        Private Sub ExportarImagen(nManager As FileProviderManager, ByVal RowFile As DBImaging.SchemaProcess.CTA_Exportacion_FilesRow, nCompresion As ImageManager.EnumCompression, nFileFolderName As String, nFolderName As String)

            'Dim Resultado As String
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

                'Dim ImagenByte As Byte() = Nothing
                'Dim Imagebmp = Bitmap.FromStream(New MemoryStream(Imagen))
                'Dim bmp = ImageManager.ComprimirImagen(Imagebmp, System.Drawing.Imaging.ImageFormat.Jpeg, reduccionPeso)
                'ImagenByte = ImageToByte(bmp)


                'Using fs = New FileStream(FileName, FileMode.Create)
                '    fs.Write(ImagenByte, 0, ImagenByte.Length)
                '    fs.Close()
                '    fs.Dispose()
                'End Using

                'GC.Collect()

                Using fs = New FileStream(FileName, FileMode.Create)
                    fs.Write(Imagen, 0, Imagen.Length)
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
                Extension = ExtensionAux

                'FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & ExtensionAux
                FileName = nFileFolderName & RowFile.Nombre_Imagen_File & ExtensionAux

                ''-------------------------------------------------------------------------
                'ImageManager.Save(FileNames, FileName, "", formatoAux, CompresionAux, False, Program.AppPath & Program.TempPath, True, True)
                ''-------------------------------------------------------------------------

                '-------------------------------------------------------------------------
                ImageManager.Save(FileNames, FileName, "", formatoAux, CompresionAux, False, Program.AppPath & Program.TempPath, True)
                '-------------------------------------------------------------------------

                'Try
                '    For Each Registro In FileNames
                '        File.Delete(Registro)
                '    Next
                'Catch ex As Exception

                'End Try
            End If

            'Resultado = FileName

            'Return Resultado
        End Sub

        Public Shared Function ImageToByte(ByVal img As Image) As Byte()
            Dim converter As ImageConverter = New ImageConverter()
            Return CType(converter.ConvertTo(img, GetType(Byte())), Byte())
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="img"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>

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

        Private Sub BuscarArchivo()
            Segundos = 0
            Minuto = 0
            Hora = 0

            Try
                Dim Respuesta = ArchivoOpenFileDialog.ShowDialog()

                If Respuesta = DialogResult.OK Then
                    Try
                        ArchivoDesktopTextBox.Text = ArchivoOpenFileDialog.FileName

                        _File = ArchivoOpenFileDialog.OpenFile()
                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("BuscarArchivo", ex)
                    Finally
                        If _File IsNot Nothing Then
                            _File.Close()
                        End If
                    End Try

                ElseIf Respuesta = DialogResult.Cancel Then
                    ArchivoDesktopTextBox.Text = ""
                    _File = Nothing
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscarArchivo", ex)
            End Try
        End Sub

        'Private Function ExecuteQuery(ByVal s As String, ByVal dbmImaging As DBImagingDataBaseManager, ByVal ParamArray params() As SqlParameter) As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable
        '    Dim dt As DataTable = Nothing
        '    Using da As New System.Data.SqlClient.SqlDataAdapter(s, dbmImaging.DataBase.ConnectionString)
        '        dt = New DataTable
        '        If params.Length > 0 Then
        '            da.SelectCommand.Parameters.AddRange(params)
        '        End If
        '        da.SelectCommand.CommandTimeout = 86400
        '        da.Fill(dt)
        '    End Using
        '    Return dt
        'End Function

        Private Function ExecuteQuery(ByVal s As String, ByVal dbmImaging As DBImagingDataBaseManager, ByVal ParamArray params() As SqlParameter) As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable
            Dim dt As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable = Nothing
            Using da As New System.Data.SqlClient.SqlDataAdapter(s, dbmImaging.DataBase.ConnectionString)
                dt = New DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable
                If params.Length > 0 Then
                    da.SelectCommand.Parameters.AddRange(params)
                End If
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(dt)
            End Using
            Return dt
        End Function
#End Region

#Region " Funciones "
        Private Function Validar() As Boolean

            If (Not Directory.Exists(CarpetaSalidaTextBox.Text)) Then
                MessageBox.Show("El directorio no existe, Seleccione un directorio existente", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()

            ElseIf (Directory.GetDirectories(CarpetaSalidaTextBox.Text).Length > 0 Or Directory.GetFiles(CarpetaSalidaTextBox.Text).Length > 0) Then
                MessageBox.Show("La carpeta debe estar vacia para exportar los datos", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()

            Else
                If TabControlExportacion.SelectedIndex = 0 Then
                    If (Me.OTDataGridView.SelectedRows.Count = 0) Then
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
                ElseIf TabControlExportacion.SelectedIndex = 1 Then
                    If Me.ArchivoDesktopTextBox.Text = "" Then
                        MessageBox.Show("Debe seleccionar un archivo", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.ArchivoDesktopTextBox.Focus()
                    Else
                        Return True
                    End If
                End If
            End If

            Return False
        End Function

        Private Function getOTs() As DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                Return dbmImaging.SchemaProcess.PA_Exportacion_OT.DBExecute(Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _
                                                                                Plugin.Manager.ImagingGlobal.Entidad, _
                                                                                Plugin.Manager.ImagingGlobal.Proyecto, _
                                                                                CInt(Me.FechaProcesoInicialDateTimePicker.Value.ToString("yyyyMMdd")), _
                                                                                CInt(Me.FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd")))
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return New DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable()
        End Function

        Private Function LlenaOtsSeleccion() As Boolean

            'Se crea la tabla que contendrá las OT´s seleccionadas en la grilla
            Dim OTsSeleccion_ As New DataTable

            For Each col As DataGridViewColumn In OTDataGridView.Columns
                OTsSeleccion_.Columns.Add(col.DataPropertyName)
            Next

            For Each row As DataGridViewRow In OTDataGridView.Rows
                If CBool(row.Cells("ColumnExportar").Value) Then 'OT's seleccionadas
                    Dim newRow As DataRow = OTsSeleccion_.NewRow()

                    For Each col As DataGridViewColumn In OTDataGridView.Columns
                        newRow(col.Index) = row.Cells(col.Index).Value
                    Next

                    OTsSeleccion_.Rows.Add(newRow)
                End If
            Next

            OTsSeleccion = OTsSeleccion_

            If OTsSeleccion_.Rows.Count > 0 Then
                ViewOts.RowFilter = String.Empty
                ViewOts = New DataView(OTsSeleccion_)
                Return True
            End If

            Return False

        End Function

        Private Function ExecuteQuery(ByVal s As String, ByVal dbmCore As DBCore.DBCoreDataBaseManager, ByVal ParamArray params() As SqlParameter) As DBCore.SchemaReport.CTA_Exportacion_Files_DecevalDataTable
            Dim dt As DBCore.SchemaReport.CTA_Exportacion_Files_DecevalDataTable = Nothing
            Using da As New System.Data.SqlClient.SqlDataAdapter(s, dbmCore.DataBase.ConnectionString)
                dt = New DBCore.SchemaReport.CTA_Exportacion_Files_DecevalDataTable
                If params.Length > 0 Then
                    da.SelectCommand.Parameters.AddRange(params)
                End If
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(dt)
            End Using
            Return dt
        End Function
#End Region

    End Class
End Namespace