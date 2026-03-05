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
Imports Coomeva.Plugin.Risk.FormWrappers

Namespace Forms.Desembolsos

    Public Class FormCargueDesembolsos
        Inherits FormBase

#Region " Declaraciones "
        Private _Plugin As CoomevaRiskPlugin
        Private _File As Stream = Nothing
        Private _DataFile As DataTable = Nothing
        Private _DataRegistros As Integer = 0
        Private _DataColumnas As Integer = 0
        Private _EstadoProceso As Short = 0 '0 Validando, 1 Procesando.
        Private objCSV As New Slyg.Tools.CSV.CSVData
        Private objXLS As New XLSData
        Private trResultado As DesktopConfig.TypeResult
        Private Segundos As Integer = 0
        Private Minuto As Integer = 0
        Private Hora As Integer = 0

#End Region

#Region " Constructor "

        Sub New(ByVal nCoomevaDesktopPlugin As CoomevaRiskPlugin)
            ' This call is required by the designer.
            InitializeComponent()
            _Plugin = nCoomevaDesktopPlugin
            ' Add any initialization after the InitializeComponent() call.

        End Sub

#End Region

#Region " Metodos "

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
            CargarLogButton.Enabled = valor
        End Sub

        Private Sub CargaDatos()

        End Sub

#End Region

#Region " Funciones "

        Private Function CargarArchivo(ByVal NombreArchivo As String) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            trReturn.Result = True
            Try
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

                Dim objProcesaCargue As ProcesaCargueDesembolsos = New ProcesaCargueDesembolsos(_Plugin)
                Dim trRespuestaCargue As New DesktopConfig.TypeResult

                _DataRegistros = _DataFile.Rows.Count
                _DataColumnas = _DataFile.Columns.Count

                If _DataRegistros > 0 And _DataColumnas > 1 Then
                    CargueBackgroundWorker.ReportProgress(0)


                    trRespuestaCargue = objProcesaCargue.ProcesaCargueDesembolsos(_DataFile, NombreArchivo, Me.CargueBackgroundWorker)

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

        Private Function ValidarNombreArchivo(ByVal NombreArchivo As String)
            Dim Cumple As Boolean = True
            Dim MsjError As String = "El nombre de archivo que está intentando cargar no cumple con las especificaciones de formato, por favor seleccione otro archivo de cargue."
            'Dim fecha As DateTime

            Try

                If Not (NombreArchivo.Length = 20 And NombreArchivo.ToUpper.StartsWith("DESEMBOLSOS ")) Then
                    Cumple = False
                End If

                Dim splitstr() As String = Split(NombreArchivo, " ")

                If splitstr.Count <= 1 Then
                    Cumple = False
                ElseIf Not splitstr(1).Length = 8 Then
                    Cumple = False
                End If

                If GetDateFromstring(splitstr(1)) Is Nothing Then
                    Cumple = False
                End If

            Catch ex As Exception
                MsjError = ex.Message
                Cumple = False
            End Try

            If Not Cumple Then
                DesktopMessageBoxControl.DesktopMessageShow(MsjError, "Nombre de Archivo", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Return False
            End If

            Return True
        End Function

        Private Function GetDateFromstring(ByVal strDate As String) As DateTime?
            Dim fecha As DateTime?
            Dim Formato As String

            For Cuenta As Integer = 1 To 4 Step 1

                Select Case Cuenta
                    Case 1
                        Formato = "yyyyMMdd"
                    Case 2
                        Formato = "ddMMyyyy"
                    Case 3
                        Formato = "MMddyyyy"
                    Case 4
                        Formato = "yyyyddMM"
                    Case Else
                        Formato = ""
                        Exit For
                End Select

                Try
                    fecha = DateTime.ParseExact(strDate, Formato, CultureInfo.InvariantCulture)
                    Exit For

                Catch ex As Exception
                    fecha = Nothing
                End Try


            Next

            Return fecha

        End Function

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
                Me.Text = "Cargue Desembolsos"
                CargaDatos()
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

        Private Sub CargarLogButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargarLogButton.Click
            Timer1.Enabled = True

            Me.CargueBackgroundWorker.WorkerSupportsCancellation = True

            Dim dbmArchiving = New DBArchivingDataBaseManager(_Plugin.CoomevaConnectionString)
            Try
                dbmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                If Not IsNothing(_File) Then


                    'Valida si el nombre del archivo ya fué cargado.
                    Dim NombreArchivo As String = Path.GetFileName(CType(_File, FileStream).Name)
                    Dim NombreArchivoSinExtension As String = Path.GetFileNameWithoutExtension(CType(_File, FileStream).Name)
                    Dim FechaDesembolso As DateTime
                    Dim RecoleccionDataTable As DBArchiving.SchemaRisk.TBL_Control_Cargue_DesembolsoDataTable = Nothing

                    If Not ValidarNombreArchivo(NombreArchivoSinExtension) Then
                        _File = Nothing
                        ArchivoDesktopTextBox.Text = ""
                        Return
                    End If

                    '********Validaciones del cargue*********
                    FechaDesembolso = GetDateFromstring(NombreArchivo.Substring(12, 8))
                    RecoleccionDataTable = dbmArchiving.SchemaRisk.PA_Valida_Archivo_Desembolso.DBExecute(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto, NombreArchivo, _Plugin.Manager.Sesion.Usuario.id)


                    Dim dtCargues = dbmArchiving.SchemaRisk.TBL_Cargue_Desembolso.DBFindByfk_Entidadfk_ProyectoArchivo_Cargue_Desembolso(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto, NombreArchivo)
                    If dtCargues.Count > 0 Then
                        DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo de Desembolso que está intentando cargar ya se encuentra registrado, por favor seleccione otro archivo de cargue.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        _File = Nothing
                        ArchivoDesktopTextBox.Text = ""
                    ElseIf Not (RecoleccionDataTable Is Nothing) AndAlso RecoleccionDataTable.Count <= 0 Then
                        DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo de Desembolsos que está intentando cargar no corresponde a una fecha de Cargue semanal de Desembolsos valida, por favor verifique el archivo.", "Archivo de Cargue no valido", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        _File = Nothing
                        ArchivoDesktopTextBox.Text = ""
                    ElseIf Not (RecoleccionDataTable Is Nothing) AndAlso RecoleccionDataTable.Count > 0 Then
                        If RecoleccionDataTable(0).Fecha_Desembolso.ToString("yyyy/MM/dd") <> FechaDesembolso.ToString("yyyy/MM/dd") Then
                            DesktopMessageBoxControl.DesktopMessageShow("Se encuentran pendientes los archivos de cargue de Desembolsos ('" + RecoleccionDataTable(0).Fecha_Desembolso.ToString("yyyy/MM/dd") + "', en adelante). Por favor Validar.", "Archivo de Cargue no valido", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                            _File = Nothing
                            ArchivoDesktopTextBox.Text = ""
                        ElseIf RecoleccionDataTable(0).fk_Cargue_Desembolso <> 0 Then
                            DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo de Desembolso que está intentando cargar corresponde a una fecha ya cargada, por favor seleccione otro archivo de cargue.", "Archivo de Cargue no valido", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                            _File = Nothing
                            ArchivoDesktopTextBox.Text = ""
                        Else

                            HabilitarControles(False)
                            Me.CargueBackgroundWorker.RunWorkerAsync(NombreArchivo)

                        End If
                    Else

                        HabilitarControles(False)
                        Me.CargueBackgroundWorker.RunWorkerAsync(NombreArchivo)
                    End If


                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un archivo de cargue y un esquema de facturación.", "Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If

                    'Timer1.Enabled = False

            Catch ex As Exception
                'Timer1.Enabled = False
                DesktopMessageBoxControl.DesktopMessageShow("CargarLogButton_Click", ex)
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

                For i As Integer = 0 To Parametros.Length - 4 Step 1
                    NombreArchivo = Parametros(i)
                Next

            Else
                NombreArchivo = Parametros(0)
            End If

            trResultado = CargarArchivo(NombreArchivo)
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
                If Not IsNothing(trResultado.Cargue) Then
                    If Not trResultado.Cargue.IsNull Then
                        DesktopMessageBoxControl.DesktopMessageShow("Se generó el Cargue de Desembolso: " & trResultado.Cargue.ToString(), "Cargue Desembolso", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    End If
                End If

                DatosCargadosDesktopDataGridView.DataSource = _DataFile
                CargueProgressBar.Value = CargueProgressBar.Maximum
                DesktopMessageBoxControl.DesktopMessageShow("Datos cargados éxitosamente", "Cargue de datos", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Else
                If Not IsNothing(trResultado.Parameters) Then
                    Dim myError As New FormResultadoValidacionDesembolsos(trResultado)
                    CargueBackgroundWorker.Dispose()
                    myError.ShowDialog()
                End If
            End If

            _File = Nothing
        End Sub


#End Region

#End Region

    End Class

End Namespace