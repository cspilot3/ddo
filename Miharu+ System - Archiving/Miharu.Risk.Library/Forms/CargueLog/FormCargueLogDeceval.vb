Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Configuration
Imports DBArchiving
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Threading
Imports System.Globalization
Imports Excel = Microsoft.Office.Interop.Excel
Imports DBImaging

Namespace Forms.Cargue
    Public Class FormCargueLogDeceval
        Inherits FormBase


#Region " Declaraciones "

        Private _File As Stream = Nothing
        Private _DataFile As DataTable = Nothing
        Private _dtTableRespuesta As DataTable = Nothing
        Private _dtTableRespuestaDuplicados As DataTable = Nothing
        Private _DataRegistros As Integer = 0
        Private _DataColumnas As Integer = 0
        Private _EstadoProceso As Short = 0 '0 Validando, 1 Procesando.
        Private objCSV As New Slyg.Tools.CSV.CSVData
        Private objXLS As New XLSData
        Private trResultado As DesktopConfig.TypeResult
        Private _TipoCargue As DesktopConfig.TipoCargue
        Private Segundos As Integer = 0
        Private Minuto As Integer = 0
        Private Hora As Integer = 0
        Private TiposLogDataTable As DBImaging.SchemaConfig.TBL_Tipos_LogDataTable
        Private TiposLogrow As DBImaging.SchemaConfig.TBL_Tipos_LogRow
        Private validaArchivoCargue As Boolean
        Private SaltoPrimasLineas As Integer
        Private ExtensionGlobal As String = ""
        Private _fechaProceso As DateTime
        Private passwordFile As String = ""
        Private Usa_Fecha_Recaudo As Boolean = False

#End Region

#Region " Constructor "
        Private _appSettings As String

        Sub New(ByVal TipoCargue As DesktopConfig.TipoCargue)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _TipoCargue = TipoCargue
        End Sub

#End Region

#Region " Metodos "

        Private Property AppSettings(p1 As String) As String
            Get
                Return _appSettings
            End Get
            Set(value As String)
                _appSettings = value
            End Set
        End Property

        Private Sub BuscarArchivo()
            Segundos = 0
            Minuto = 0
            Hora = 0

            Try
                Dim Respuesta = ArchivoOpenFileDialog.ShowDialog()

                If Respuesta = DialogResult.OK Then
                    Try
                        ArchivoDesktopTextBox.Text = ArchivoOpenFileDialog.FileName

                        'Si el archivo es txt o csv, habilita la opción de manejar separador
                        If ArchivoOpenFileDialog.FileName.ToLower().EndsWith(".txt") OrElse ArchivoOpenFileDialog.FileName.ToLower().EndsWith(".csv") Then
                            OpcionesSeparadorGroupBox.Enabled = True
                        Else
                            OpcionesSeparadorGroupBox.Enabled = False
                        End If

                        _File = ArchivoOpenFileDialog.OpenFile()
                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("BuscarArchivo", ex)
                    Finally
                        If _File IsNot Nothing Then
                            _File.Close()
                        End If
                    End Try

                ElseIf Respuesta = DialogResult.Cancel Then
                    OpcionesSeparadorGroupBox.Enabled = False
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
            CargarButtonLog.Enabled = valor
        End Sub

        Private Sub CargaDatos()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                'Dim EsquemasFac = dbmArchiving.Schemadbo.CTA_Esquema_x_Facturacion.DBFindByfk_Entidad_Cliente(Program.RiskGlobal.Entidad)
                'Utilities.LlenarCombo(EsquemaFacturacionComboBox, EsquemasFac, EsquemasFac.IDColumn.ColumnName, EsquemasFac.ValorColumn.ColumnName, True)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaDatos", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function SubString(ByVal CamposLog As DataTable, ByVal File As Stream) As DataTable

            Dim Lee = New System.IO.StreamReader(CType(File, FileStream).Name)
            Dim Data = New DataTable()

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

        Private Function CargarArchivo(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Proyecto As Short) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            trReturn.Result = True

            Dim _Data As DataTable = New DataTable
            Dim CamposLog As DataTable = New DataTable
            Dim CamposLLenos As Boolean = False
            Dim contadorValoresEncontrados As Integer = 0
            Dim ltCamposLog As List(Of String) = New List(Of String)

            Try
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                dbmImaging = New DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                If ((ExtensionGlobal.ToUpper() = ".XLS") Or (ExtensionGlobal.ToUpper() = ".XLSX")) Then
                    Dim dsHojas As DataSet = objXLS.ImportExcelXLS(CType(_File, FileStream).Name, TiposLogrow.ManejaEncabezado)
                    _Data = dsHojas.Tables(0)
                Else
                    If (ExtensionGlobal.ToUpper() = ".TXT" Or ExtensionGlobal.ToUpper() = ".DAT" Or ExtensionGlobal.ToUpper() = ".RTA") And (TiposLogrow.Separador = "") Then
                        CamposLog = dbmImaging.SchemaConfig.TBL_Campos_Tipo_log.DBFindByfk_Tipo_logfk_Entidadfk_ProyectoEliminado(CInt(DesktopComboBoxControlTiposLog.SelectedValue), Entidad, Proyecto, False)
                        _Data = SubString(CamposLog, _File)
                    Else
                        objCSV.Separator = CChar(IIf(TiposLogrow.Separador = "TAB", ControlChars.Tab, TiposLogrow.Separador))
                        'Se realiza el cargue del archivo en un datatable para luego validarlo.
                        objCSV.LoadCSV(CType(_File, FileStream).Name, TiposLogrow.ManejaEncabezado)
                        _Data = objCSV.DataTable.ToDataTable()
                    End If
                End If
                'Se valida el tipo de archivo, si es CSV,TXT o XLS
                If OpcionesSeparadorGroupBox.Enabled Then 'Es plano CSV o TXT
                    'Se obtiene el separador
                    If ComaRadioButton.Checked Then objCSV.Separator = CChar(",")
                    If TabuladorRadioButton.Checked Then objCSV.Separator = ControlChars.Tab
                    If PuntoComaRadioButton.Checked Then objCSV.Separator = CChar(";")

                    'Se realiza el cargue del archivo en un datatable para luego validarlo.
                    objCSV.LoadCSV(CType(_File, FileStream).Name, chkEncabezado.Checked)
                    _DataFile = objCSV.DataTable.ToDataTable()
                Else 'Es XLS
                    Dim dsHojas As DataSet = objXLS.ImportExcelXLS(CType(_File, FileStream).Name, chkEncabezado.Checked)
                    _DataFile = dsHojas.Tables(0)
                End If

                Dim objProcesaCargue As ProcesaCargueLogDeceval = New ProcesaCargueLogDeceval()
                Dim trRespuestaCargue As New DesktopConfig.TypeResult

                Dim CamposTipoLogDataTable = dbmImaging.SchemaConfig.TBL_Campos_Tipo_log.DBFindByfk_Tipo_logfk_Entidadfk_ProyectoEliminado(TiposLogrow.id_Tipo_log, Entidad, Proyecto, False)
                Dim CamposDataLog = dbmImaging.SchemaProcess.Get_Tabla_Log.DBExecute(Entidad, Proyecto)

                _DataFile = New DataTable()

                For Each itemCampo As DataColumn In CamposDataLog.Columns
                    _DataFile.Columns.Add(itemCampo.ColumnName)
                Next

                If _Data.Rows.Count > 0 Then
                    'For index As Integer = 0 To _Data.Rows.Count - 1
                    '    For Each itemCampo As DataColumn In CamposDataLog.Columns
                    '        If itemCampo.ToString().ToUpper.Contains("CAMPO_") Or itemCampo.ToString().ToUpper.Contains("FECHA_RECAUDO") Then
                    '            Dim campoBuscar = itemCampo.ToString()
                    '            Dim ax_encontrado = CamposTipoLogDataTable.Where(Function(x) x.Nombre_Campo = campoBuscar.ToString()).ToList()
                    '            Dim CampoTipoLogRow As DBImaging.SchemaConfig.TBL_Campos_Tipo_logRow = Nothing

                    '            If ax_encontrado.Count > 0 Then
                    '                CampoTipoLogRow = ax_encontrado.First()
                    '                Dim valorCampo = _Data.Rows(index)(CampoTipoLogRow.Descripcion).ToString()
                    '                ltCamposLog.Add(valorCampo)
                    '            Else
                    '                ltCamposLog.Add("")
                    '            End If
                    '        Else
                    '            ltCamposLog.Add("")
                    '        End If
                    '    Next
                    '    _DataFile.Rows.Add(ltCamposLog.ToArray())
                    '    _DataFile.Rows(index)("fk_Entidad") = Entidad
                    '    _DataFile.Rows(index)("fk_Proyecto") = Proyecto
                    '    _DataFile.Rows(index)("fk_Tipo_Log") = TiposLogrow.id_Tipo_log

                    '    ltCamposLog.Clear()
                    'Next

                End If

                _DataRegistros = _DataFile.Rows.Count
                _DataColumnas = _DataFile.Columns.Count

                If _DataRegistros > 0 And _DataColumnas > 1 Then
                    CargueBackgroundWorker.ReportProgress(0)

                    If _TipoCargue = DesktopConfig.TipoCargue.Deceval Then
                        _dtTableRespuesta = objProcesaCargue.ProcesaDeceval(_DataFile, NombreArchivo, Entidad, Proyecto, Me.CargueBackgroundWorker)
                        _dtTableRespuestaDuplicados = objProcesaCargue.ProcesaDecevalDuplicados(_DataFile, NombreArchivo, Entidad, Proyecto, Me.CargueBackgroundWorker)
                        trRespuestaCargue.Result = True
                    End If

                    If trRespuestaCargue.Result Then
                        trReturn = trRespuestaCargue
                    Else
                        trReturn.Result = False
                        trReturn.Parameters = trRespuestaCargue.Parameters
                        trReturn.Resumen = trRespuestaCargue.Resumen
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
                Dim lisMsgError = New List(Of String)
                lisMsgError.Add("- Error: " & ex.Message)
                trReturn.Result = False
                trReturn.Parameters = lisMsgError
            End Try

            Return trReturn
        End Function

        Private Function getconfig() As DataTable
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim dv As DataView = Nothing
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmCore.DataBase.ConnectionString)
                Dim sqlquery As String = " SELECT [fk_Esquema],[fk_Tipologia] FROM [DB_Miharu.Core].[Config].[TBL_Documento]WHERE (fk_entidad = @fk_entidad) AND fk_Proyecto = @fk_proyecto "
                Dim SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@fk_entidad", Program.RiskGlobal.Entidad),
                    New SqlParameter("@fk_proyecto", Program.RiskGlobal.Proyecto)
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

        'Funcionalidad Exportar
        Private Sub btnExportar_Click(sender As System.Object, e As System.EventArgs) Handles btnExportar.Click
            Try
                Dim resultExportar As DialogResult = MessageBox.Show("Desea exportar los registros a excel ?", "Exportar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If resultExportar = DialogResult.Yes Then
                    Dim app As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
                    app.Visible = True
                    Dim wb As Microsoft.Office.Interop.Excel.Workbook = app.Workbooks.Add(1)

                    'Sheet Registros Duplicados
                    Dim ws As Microsoft.Office.Interop.Excel.Worksheet = CType(wb.Worksheets(1), Microsoft.Office.Interop.Excel.Worksheet)
                    ws.Name = "Registros Duplicados"
                    ws.Rows.HorizontalAlignment = HorizontalAlignment.Center

                    For i As Integer = 1 To DatosDuplicadosDesktopDataGridView.Columns.Count + 1 - 1
                        ws.Cells(1, i) = DatosDuplicadosDesktopDataGridView.Columns(i - 1).HeaderText
                    Next

                    For i As Integer = 0 To DatosDuplicadosDesktopDataGridView.Rows.Count - 1

                        For j As Integer = 0 To DatosDuplicadosDesktopDataGridView.Columns.Count - 1
                            ws.Cells(i + 2, j + 1) = DatosDuplicadosDesktopDataGridView.Rows(i).Cells(j).Value.ToString()
                        Next
                    Next

                    ws.Cells.EntireColumn.AutoFit()

                    'Sheet Total Registros
                    ws.Application.Worksheets.Add()
                    ws = CType(wb.Worksheets(1), Microsoft.Office.Interop.Excel.Worksheet)
                    ws.Name = "Total Registros"
                    ws.Rows.HorizontalAlignment = HorizontalAlignment.Center

                    For i As Integer = 1 To DatosCargadosDesktopDataGridView.Columns.Count + 1 - 1
                        ws.Cells(1, i) = DatosCargadosDesktopDataGridView.Columns(i - 1).HeaderText
                    Next

                    For i As Integer = 0 To DatosCargadosDesktopDataGridView.Rows.Count - 1 - 1

                        For j As Integer = 0 To DatosCargadosDesktopDataGridView.Columns.Count - 1
                            ws.Cells(i + 2, j + 1) = DatosCargadosDesktopDataGridView.Rows(i).Cells(j).Value.ToString()
                        Next
                    Next

                    ws.Cells.EntireColumn.AutoFit()
                    'wb.SaveAs("c:\Informe.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Exportar", ex)
            End Try
        End Sub
#End Region

#Region " Eventos "

        Private Sub FormCargue_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
            If Me.CargueBackgroundWorker.IsBusy Then
                Me.CargueBackgroundWorker.CancelAsync()
            End If
        End Sub

        Private Sub FormCargue_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
            Thread.CurrentThread.CurrentCulture = New CultureInfo("es-MX")

            Try
                'Cambia Titulo
                Me.Text = "Cargue " & _TipoCargue.ToString()
                CargaDatos()

                chkEncabezado.Enabled = True
                If _TipoCargue = DesktopConfig.TipoCargue.Deceval Then
                    OpcionesSeparadorGroupBox.Enabled = False
                    'chkEncabezado.Enabled = False
                    'EsquemaFacLabel.Enabled = False
                    'EsquemaFacturacionComboBox.Enabled = False
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FormCargue_Load", ex)
            End Try
        End Sub

        Private Sub BuscarArchivoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarArchivoButton.Click
            BuscarArchivo()
        End Sub

        Private Sub ArchivoDesktopTextBox_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ArchivoDesktopTextBox.Click
            BuscarArchivo()
        End Sub

        Private Sub CargarButtonLog_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargarButtonLog.Click
            Timer1.Enabled = True

            Me.CargueBackgroundWorker.WorkerSupportsCancellation = True

            Dim dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                If Not IsNothing(_File) Then
                    Dim NombreArchivo As String = Path.GetFileName(CType(_File, FileStream).Name)
                    Dim Entidad As String = CStr(Program.RiskGlobal.Entidad)
                    Dim Proyecto As String = CStr(Program.RiskGlobal.Proyecto)

                    'If _TipoCargue <> DesktopConfig.TipoCargue.Deceval Then
                    '    If CStr(EsquemaFacturacionComboBox.SelectedValue) <> "-1" Then
                    '        Dim arrEsquema = EsquemaFacturacionComboBox.SelectedValue.ToString().Split(CChar("-"))
                    '        Entidad = CStr(arrEsquema(0))
                    '        Esquema = CStr(arrEsquema(1))
                    '    Else
                    '        DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un esquema de facturación.", "Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    '    End If
                    'End If

                    If Me.ArchivoDesktopTextBox.Text = "" Then
                        DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Log, Archivo a cargar Vacio!!", "ERROR", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        Return
                    End If

                    'Valida si el nombre del archivo ya fue cargado.
                    Dim dtCargues = dbmArchiving.SchemaRisk.TBL_Cargue.DBFindByfk_Entidadfk_ProyectoArchivo_Cargue(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, NombreArchivo)
                    If dtCargues.Count > 0 Then
                        DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar ya se encuentra registrado, por favor seleccione otro archivo de cargue.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        _File = Nothing
                        ArchivoDesktopTextBox.Text = ""
                    Else
                        HabilitarControles(False)
                        Me.CargueBackgroundWorker.RunWorkerAsync(NombreArchivo & "-" & Entidad & "-" & Proyecto)
                    End If
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un archivo de cargue.", "Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If

                'Timer1.Enabled = False

            Catch ex As Exception
                'Timer1.Enabled = False
                DesktopMessageBoxControl.DesktopMessageShow("CargarButton_Click", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try

        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            If CargueBackgroundWorker.IsBusy Then
                Dim resultado = DesktopMessageBoxControl.DesktopMessageShow("En este momento se esta procesando un cargue de archivo, ¿Desea cancelar la operación?", "Cancelación de cargue", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)
                If resultado = DialogResult.OK Then
                    Me.CargueBackgroundWorker.CancelAsync()
                    Me.Close()
                End If
            Else
                Me.Close()
            End If
        End Sub

        Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As EventArgs) Handles Timer1.Tick
            If Timer1.Enabled Then
                Segundos += 1

                If Segundos = 60 Then
                    Segundos = 0
                    Minuto += 1
                End If

                If Minuto = 60 Then
                    Minuto = 0
                    Hora += 1
                End If

                TiempoLabel.Text = String.Format("{0:00}", Hora) + ":" + String.Format("{0:00}", Minuto) + ":" + String.Format("{0:00}", Segundos)
            End If
        End Sub

#Region "BackgroundWorker"

        Private Sub CargueBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As DoWorkEventArgs) Handles CargueBackgroundWorker.DoWork
            Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
            If worker.CancellationPending Then e.Cancel = True

            'Se obtiene entidad, esquema
            Dim Parametros = e.Argument.ToString().Split(CChar("-"))
            Dim NombreArchivo As String = ""

            If Parametros.Length > 3 Then
                'Dim Nombre = Parametros.Length - 3

                For i As Integer = 0 To Parametros.Length - 3 Step 1
                    NombreArchivo = Parametros(i)
                Next

            Else
                NombreArchivo = Parametros(0)
            End If

            Dim Entidad = CShort(Parametros(Parametros.Length - 2))
            Dim Proyecto = CShort(Parametros(Parametros.Length - 1))
            trResultado = CargarArchivo(NombreArchivo, Entidad, Proyecto)
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
            Timer1.Enabled = False

            ArchivoDesktopTextBox.Text = ""
            _File = Nothing

            If trResultado.Result Then
                If Not IsNothing(trResultado.OT) Then
                    If Not trResultado.OT.IsNull Then
                        DesktopMessageBoxControl.DesktopMessageShow("Se generó el cargue con OT: " & trResultado.OT.ToString(), "Cargue Parcial [OT: " & trResultado.OT.ToString() & "]", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    End If
                End If

                TotalRegistrosLabel.Text = _dtTableRespuesta.Rows.Count().ToString()
                ProcesadosLabel.Text = _dtTableRespuesta.Rows.Count().ToString()

                'Carga la información de los SP a las grillas correspondientes
                DatosCargadosDesktopDataGridView.DataSource = _dtTableRespuesta
                DatosDuplicadosDesktopDataGridView.DataSource = _dtTableRespuestaDuplicados
                CargueProgressBar.Value = CargueProgressBar.Maximum
                DesktopMessageBoxControl.DesktopMessageShow("Datos cargados éxitosamente", "Cargue de datos", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Else
                If Not IsNothing(trResultado.Parameters) Then
                    Dim myError As New FormResultadoValidacion(trResultado)
                    CargueBackgroundWorker.Dispose()
                    myError.ShowDialog()
                End If
            End If

            _File = Nothing
        End Sub

        Private Function ExecuteQuery(ByVal s As String, ByVal condb As SqlConnection, ByVal ParamArray params() As SqlParameter) As DataTable
            Dim dt As DataTable = Nothing
            Using da As New System.Data.SqlClient.SqlDataAdapter(s, condb)
                dt = New DataTable
                If params.Length > 0 Then
                    da.SelectCommand.Parameters.AddRange(params)
                End If
                If da.SelectCommand.Connection.State <> ConnectionState.Open Then da.SelectCommand.Connection.Open()
                da.Fill(dt)
            End Using
            Return dt
        End Function

#End Region

#End Region
    End Class
End Namespace

