Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms
Imports Slyg.Tools.CSV
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports System.Dynamic
Imports DBImaging
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Linq

' LOL
Public Class FormExportClientesEnLog
    Inherits FormBase

#Region " Declaraciones "

    Private _File As Stream = Nothing
    Private _OutputFolder As String = Nothing
    Private _DataFile As System.Data.DataTable = Nothing
    Private _DataRegistros As Integer = 0
    Private _DataColumnas As Integer = 0
    Private _EstadoProceso As Short = 0 '0 Validando, 1 Procesando.
    Private _trResultado As DesktopConfig.TypeResultDataDate
    Private TiposLogDataTable As DBImaging.SchemaConfig.TBL_Tipos_LogDataTable
    Private TiposLogrow As DBImaging.SchemaConfig.TBL_Tipos_LogRow
    Private SaltoPrimasLineas As Integer
    'Private passwordFile As String = ""
    Private _entidad As Short
    Private _proyecto As Short
    Private _nombreProyecto As String

#End Region

#Region " Constructor "
    Sub New(entidad As Short, proyecto As Short, nombreProyecto As String)
        _entidad = entidad
        _proyecto = proyecto
        _nombreProyecto = nombreProyecto
        InitializeComponent()
    End Sub

#End Region

#Region " Metodos "
    Private Sub BuscarArchivo()
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

    Private Sub HabilitarControles(ByVal valor As Boolean)
        ArchivoDesktopTextBox.Enabled = valor
        BuscarArchivoButton.Enabled = valor
        CargarButton.Enabled = valor
        CarpetaSalidaTextBox.Enabled = valor
        BuscarCarpetaButton.Enabled = valor
    End Sub

#End Region

#Region " Funciones "

    Private Function SubString(ByVal CamposLog As System.Data.DataTable, ByVal File As Stream) As System.Data.DataTable

        Dim Lee = New System.IO.StreamReader(CType(File, FileStream).Name)
        Dim Data = New System.Data.DataTable()

        For Each x As DataRow In CamposLog.Rows
            Data.Columns.Add(x.Item("Descripcion").ToString())
        Next

        Dim texto = ""
        Dim numLinea = 0
        If CInt(TiposLogrow.Salto_Primeras_Lineas) = 0 Then
            texto = Lee.ReadLine()
            numLinea = numLinea + 1
        Else
            For index As Integer = 0 To CInt(TiposLogrow.Salto_Primeras_Lineas) ' - 1
                texto = Lee.ReadLine()
                numLinea = numLinea + 1
            Next
        End If

        Dim longitud_Fija = 0

        If Not IsDBNull(TiposLogrow.Longitud_Fija) Then
            longitud_Fija = CInt(TiposLogrow.Longitud_Fija)
        End If

        While texto IsNot Nothing
            Dim lenTexto = texto.Length
            Dim Valida = True
            If (TiposLogrow.Identificador_Linea <> "" Or Not IsDBNull(TiposLogrow.Identificador_Linea)) Then
                Dim lon = TiposLogrow.Identificador_Linea.Length
                If (lenTexto > 0) Then
                    If (texto.Substring(0, lon) = TiposLogrow.Identificador_Linea) Then
                        Valida = True
                    Else
                        Valida = False
                        texto = Lee.ReadLine()
                    End If
                Else
                    Valida = False
                    texto = Lee.ReadLine()
                End If
            End If

            If (Valida) Then
                If (longitud_Fija <> 0 And lenTexto = longitud_Fija) Or (longitud_Fija = 0) Then
                    Dim row = Data.NewRow
                    Try
                        For Each x As DataRow In CamposLog.Rows
                            row.Item(x.Item("Descripcion").ToString()) = "" + texto.Substring(CInt(x.Item("Length_Inicia")), CInt(x.Item("Length_Fijo")))
                        Next
                        Data.Rows.Add(row)
                    Catch
                        Throw New Exception("Error en la linea " + numLinea.ToString() + " por favor revisar el log y volver a intentar")
                    End Try
                    texto = Lee.ReadLine()
                    numLinea = numLinea + 1
                Else
                    Throw New Exception("Error en la linea " + numLinea.ToString() + " por favor revisar el log y volver a intentar")
                End If
            End If

        End While

        Dim saltosUltimas = ""

        If Not IsDBNull(TiposLogrow.Salto_Ultimas_Lineas) Then
            saltosUltimas = TiposLogrow.Salto_Ultimas_Lineas
        End If

        For index As Integer = 0 To CInt(saltosUltimas) - 1
            Data.Rows.RemoveAt(Data.Rows.Count - 1)
        Next

        Return Data
    End Function

    Private Function CargarArchivo(ByVal Entidad As Short, ByVal Esquema As Short, ByVal sender As BackgroundWorker, ByVal e As DoWorkEventArgs) As DesktopConfig.TypeResultDataDate
        Dim trReturn As New DesktopConfig.TypeResultDataDate
        trReturn.Result = True
        Dim _Data As System.Data.DataTable = New System.Data.DataTable

        Dim CamposLog As System.Data.DataTable = New System.Data.DataTable

        Dim CamposLLenos As Boolean = False
        Dim contadorValoresEncontrados As Integer = 0
        Dim ltCamposLog As List(Of String) = New List(Of String)
        Try
            Dim manejaEncabezado As Boolean = True
            Dim objXLS As New XLSData
            Dim dsHojas As DataSet = objXLS.ImportExcelXLS(CType(_File, FileStream).Name, manejaEncabezado)
            _Data = dsHojas.Tables(0)

            _DataFile = New System.Data.DataTable()
            ' TODO: Mejora, obtener las columnas llave de una tabla
            _DataFile.Columns.Add("Llave_01")
            _DataFile.Columns.Add("Llave_02")

            If _Data.Rows.Count > 0 Then
                For index As Integer = 0 To _Data.Rows.Count - 1
                    Dim actualRow = _Data.Rows(index)
                    For Each itemCampo As DataColumn In _DataFile.Columns
                        Dim valorCampo = actualRow.Item(itemCampo.ColumnName).ToString()
                        ltCamposLog.Add(valorCampo)
                    Next
                    _DataFile.Rows.Add(ltCamposLog.ToArray())
                    ltCamposLog.Clear()
                Next
            End If

            _DataRegistros = _DataFile.Rows.Count
            _DataColumnas = _DataFile.Columns.Count

            If _DataRegistros > 0 And _DataColumnas > 1 Then
                CargueBackgroundWorker.ReportProgress(0)

                Dim objProcesaCargue As ProcesaClientesEnLog = New ProcesaClientesEnLog(_OutputFolder, _entidad, _proyecto, _nombreProyecto, sender, e)
                Dim trRespuestaCargue As DesktopConfig.TypeResultDataDate = objProcesaCargue.ProcesaCargue(_DataFile)

                If trRespuestaCargue.Result Then
                    trReturn = trRespuestaCargue
                Else
                    trReturn.Result = False
                    trReturn.tablaResultados = trRespuestaCargue.tablaResultados
                End If
            Else
                trReturn.Result = False
                Dim lisMsgError = New List(Of String)
                lisMsgError.Add("- Archivo no válido. Por favor verifique lo siguiente:")
                lisMsgError.Add("- 1. Que el archivo contenga datos.")
                lisMsgError.Add("- 2. El carácter de separación sea el adecuado.")
                trReturn.Parameters = lisMsgError
            End If
        Catch ex As Exception
            trReturn.Result = False
            DesktopMessageBoxControl.DesktopMessageShow("CargarArchivo", ex)
            Dim lisMsgError = New List(Of String)
            lisMsgError.Add("- Error: " & ex.Message)
            trReturn.Result = False
            trReturn.Parameters = lisMsgError
        End Try

        Return trReturn
    End Function

#End Region

#Region " Eventos "

    Private Sub FormCargue_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If Me.CargueBackgroundWorker.IsBusy Then
            e.Cancel = True
            Dim resultado = DesktopMessageBoxControl.DesktopMessageShow("En este momento se esta procesando una exportación de archivo, ¿Desea cancelar la operación?", "Cancelación de exportación", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)
            If resultado = DialogResult.OK Then
                Me.CargueBackgroundWorker.CancelAsync()
                e.Cancel = False
            End If
        End If
    End Sub

    Private Sub FormExportClientesEnLog_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        Thread.CurrentThread.CurrentCulture = New CultureInfo("es-MX")
    End Sub

    Private Sub BuscarArchivoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarArchivoButton.Click
        BuscarArchivo()
    End Sub

    Private Sub ArchivoDesktopTextBox_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ArchivoDesktopTextBox.Click
        BuscarArchivo()
    End Sub

    Private Sub BuscarCarpetaButton_Click(sender As System.Object, e As System.EventArgs) Handles BuscarCarpetaButton.Click
        Dim Selector As New FolderBrowserDialog()

        Selector.SelectedPath = CarpetaSalidaTextBox.Text
        If (Selector.ShowDialog() = DialogResult.OK) Then
            Me.CarpetaSalidaTextBox.Text = Selector.SelectedPath
        End If
    End Sub

    Private Sub CargarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargarButton.Click
        If Me.ArchivoDesktopTextBox.Text = "" Then
            DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Log, Archivo a cargar Vacio!!", "ERROR", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Return
        End If

        Dim NombreArchivo As String = Nothing
        If Me._File IsNot Nothing Then
            NombreArchivo = Path.GetFileName(DirectCast(_File, FileStream).Name)
            Dim Extension = NombreArchivo.Substring(NombreArchivo.LastIndexOf("."))
            If ((Extension.ToUpper() = ".XLS") Or (Extension.ToUpper() = ".XLSX")) = False Then
                DesktopMessageBoxControl.DesktopMessageShow("El formato del archivo no es correcto, por favor verifique y cargue nuevamente!!", "Format Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return
            End If
        Else
            DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Log, intentelo nuevamente!!", "ERROR", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Me._File = Nothing
            Me.ArchivoDesktopTextBox.Text = ""
            Return
        End If

        Me._OutputFolder = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
        If String.IsNullOrEmpty(CarpetaSalidaTextBox.Text) Or Not Directory.Exists(_OutputFolder) Then
            DesktopMessageBoxControl.DesktopMessageShow("La carpeta de salida no existe o es invalida", "ERROR", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Return
        End If

        Me.CargueBackgroundWorker.WorkerSupportsCancellation = True

        ' Posibles tareas pendientes
        ' TODO: Valida el tipo de log contra una tabla --- como viene funcionando para otros procesos
        ' TODO: Validar que el archivo de log no haya sido procesado antes ... Tal vez no aplica

        Me.CargandoPictureBox.Visible = True
        HabilitarControles(False)
        CheckForIllegalCrossThreadCalls = False
        Me.CargueBackgroundWorker.RunWorkerAsync(NombreArchivo)
    End Sub

#Region "BackgroundWorker"

    Private Sub CargueBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As DoWorkEventArgs) Handles CargueBackgroundWorker.DoWork
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
        If worker.CancellationPending Then e.Cancel = True

        Dim nombreArchivo As String = ""
        Dim entidad = CShort(Program.ImagingGlobal.Entidad)
        Dim esquema = CShort(Program.ImagingGlobal.Proyecto)
        _trResultado = CargarArchivo(entidad, esquema, worker, e)
        Me.CargueBackgroundWorker.ReportProgress(CargueProgressBar.Maximum)
    End Sub

    Private Sub CargueBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As ProgressChangedEventArgs) Handles CargueBackgroundWorker.ProgressChanged
        If e.ProgressPercentage = 0 Then
            CargandoPictureBox.Visible = True

            CargueProgressBar.Minimum = 0
            CargueProgressBar.Maximum = _DataRegistros
            TotalRegistrosLabel.Text = _DataRegistros.ToString()

            If _EstadoProceso = 0 Then
                ProcesadosTituloLabel.Text = "Validados:"
            ElseIf _EstadoProceso = 1 Then
                ProcesadosTituloLabel.Text = "Procesados:"
            End If
            _EstadoProceso = CShort(_EstadoProceso + 1)

        Else
            'Inicio Proceso
            CargueProgressBar.Value = e.ProgressPercentage
            ProcesadosLabel.Text = e.ProgressPercentage.ToString()
        End If
    End Sub

    Private Sub CargueBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As RunWorkerCompletedEventArgs) Handles CargueBackgroundWorker.RunWorkerCompleted
        CargandoPictureBox.Visible = False

        If (e.Error IsNot Nothing) Then
            DesktopMessageBoxControl.DesktopMessageShow("Exportar archivo", e.Error)
        ElseIf (e.Cancelled) Then
            DesktopMessageBoxControl.DesktopMessageShow("Exportacion de archivos cancelada", "Exportación de datos", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
        Else
            If _trResultado.Result Then
                DatosCargadosDesktopDataGridView.DataSource = _trResultado.tablaResultados
                CargueProgressBar.Value = CargueProgressBar.Maximum
                Dim NombreArchivo = Path.GetFileName(DirectCast(_File, FileStream).Name)
                Dim Extension = NombreArchivo.Substring(NombreArchivo.LastIndexOf("."))
                NombreArchivo = NombreArchivo.Replace(Extension, "")
                Dim outputFilePath As String = _OutputFolder & NombreArchivo & "_resultados.xlsx"
                XLSData.saveExcelXLS(_trResultado.tablaResultados, outputFilePath)
                DesktopMessageBoxControl.DesktopMessageShow("Archivos exportados éxitosamente", "Exportación de datos", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Else
                If Not IsNothing(_trResultado.Parameters) Then
                    Dim msgRespuestaCargue As String = ""
                    For index = 0 To _trResultado.Parameters.Count - 1
                        msgRespuestaCargue = msgRespuestaCargue + (index + 1).ToString() + ". " + _trResultado.Parameters(index).ToString() + vbCrLf
                    Next
                    DesktopMessageBoxControl.DesktopMessageShow(msgRespuestaCargue, "Exportación de datos", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                    CargueBackgroundWorker.Dispose()
                End If
            End If
        End If
        _File = Nothing
    End Sub

#End Region

#End Region

End Class