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

Namespace Forms.Cargue

    Public Class FormCargue
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
        Private _TipoCargue As DesktopConfig.TipoCargue
        Private Segundos As Integer = 0
        Private Minuto As Integer = 0
        Private Hora As Integer = 0

#End Region

#Region " Constructor "

        Sub New(ByVal nCoomevaDesktopPlugin As CoomevaRiskPlugin, ByVal TipoCargue As DesktopConfig.TipoCargue)
            ' This call is required by the designer.
            InitializeComponent()
            _Plugin = nCoomevaDesktopPlugin
            ' Add any initialization after the InitializeComponent() call.
            _TipoCargue = TipoCargue

            If _TipoCargue = DesktopConfig.TipoCargue.Prevalidar Then
                CargarButton.Text = "Prevalidar"
            End If
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
            CargarButton.Enabled = valor
        End Sub

        Private Sub CargaDatos()
            Dim dbmArchiving As New DBArchivingDataBaseManager(_Plugin.CoomevaConnectionString)
            Try
                dbmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim EsquemasFac = dbmArchiving.Schemadbo.CTA_Esquema_x_Facturacion.DBFindByfk_Entidad_Cliente(_Plugin.Manager.RiskGlobal.Entidad)
                Utilities.LlenarCombo(EsquemaFacturacionComboBox, EsquemasFac, EsquemasFac.IDColumn.ColumnName, EsquemasFac.ValorColumn.ColumnName, True)

                'Dim TipoCargue = dbmArchiving.SchemaConfig.TBL_Tipo_Cargue.DBGet(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto, Nothing)
                Dim TipoCargue = dbmArchiving.SchemaConfig.TBL_Tipo_Cargue.DBFindByfk_Entidadfk_Proyecto(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto)
                Utilities.LlenarCombo(TipoCargueComboBox, TipoCargue, TipoCargue.id_Tipo_CargueColumn.ColumnName, TipoCargue.Nombre_Tipo_CargueColumn.ColumnName, True)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaDatos", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function CargarArchivo(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Esquema As Short, ByVal TipoCargue As Short) As DesktopConfig.TypeResult
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

                Dim objProcesaCargue As ProcesaCargue = New ProcesaCargue(_Plugin)
                Dim trRespuestaCargue As New DesktopConfig.TypeResult

                _DataRegistros = _DataFile.Rows.Count
                _DataColumnas = _DataFile.Columns.Count

                If _DataRegistros > 0 And _DataColumnas > 1 Then
                    CargueBackgroundWorker.ReportProgress(0)

                    If _TipoCargue = DesktopConfig.TipoCargue.Universal Then
                        trRespuestaCargue = objProcesaCargue.ProcesaUniversal(_DataFile, NombreArchivo, Me.CargueBackgroundWorker)
                    ElseIf _TipoCargue = DesktopConfig.TipoCargue.Parcial Or _TipoCargue = DesktopConfig.TipoCargue.Prevalidar Then
                        trRespuestaCargue = objProcesaCargue.ProcesaParcial(_DataFile, NombreArchivo, Entidad, Esquema, TipoCargue, Me.CargueBackgroundWorker, _TipoCargue)
                    ElseIf _TipoCargue = DesktopConfig.TipoCargue.Actualizacion Then
                        trRespuestaCargue = objProcesaCargue.ProcesaActualizacion(_DataFile, NombreArchivo, Entidad, Esquema, Me.CargueBackgroundWorker)
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
            Catch ex As ArgumentOutOfRangeException
                Dim lisMsgError = New List(Of String)
                If ex.Message.Contains("El índice estaba fuera del intervalo. Debe ser un valor no negativo e inferior al tamaño de la colección.") Then
                    lisMsgError.Add("Validar la estructura del archivo por espacios")
                    trReturn.Result = False
                    trReturn.Parameters = lisMsgError
                Else
                    lisMsgError.Add("- Error: " & ex.Message)
                    trReturn.Result = False
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

        Private Sub CargarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargarButton.Click
            Timer1.Enabled = True

            Me.CargueBackgroundWorker.WorkerSupportsCancellation = True

            Dim dbmArchiving = New DBArchivingDataBaseManager(_Plugin.CoomevaConnectionString)
            Try
                dbmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                If Not IsNothing(_File) AndAlso CStr(EsquemaFacturacionComboBox.SelectedValue) <> "-1" Then

                    If CStr(TipoCargueComboBox.SelectedValue) <> "-1" Then

                        'Valida si el nombre del archivo ya fué cargado.
                        Dim NombreArchivo As String = Path.GetFileName(CType(_File, FileStream).Name)
                        Dim NombreArchivoSinExtension As String = Path.GetFileNameWithoutExtension(CType(_File, FileStream).Name)
                        Dim arrEsquema = EsquemaFacturacionComboBox.SelectedValue.ToString().Split(CChar("-"))
                        Dim Entidad = CStr(arrEsquema(0))
                        Dim Esquema = CStr(arrEsquema(1))
                        Dim TipoCargue = TipoCargueComboBox.SelectedValue.ToString()
                        Dim FechaRecoleccion As String = ""
                        Dim RecoleccionDataTable As DBArchiving.Schemadbo.CTA_Validar_Archivo_LogDataTable = Nothing


                        If TipoCargue = "1" Then
                            If Not (NombreArchivoSinExtension.Length = 18 And NombreArchivoSinExtension.ToUpper.StartsWith("GARANTIAS ")) Then
                                DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar no cumple con las especificaciones de formato, por favor seleccione otro archivo de cargue.", "Nombre de Archivo de Cargue no valido", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                _File = Nothing
                                ArchivoDesktopTextBox.Text = ""
                                Return
                            End If

                            FechaRecoleccion = NombreArchivo.Substring(10, 8)
                            RecoleccionDataTable = dbmArchiving.Schemadbo.PA_Validar_Archivo_Log.DBExecute(FechaRecoleccion)
                        End If


                        Dim dtCargues = dbmArchiving.SchemaRisk.TBL_Cargue.DBFindByfk_Entidadfk_ProyectoArchivo_Cargue(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto, NombreArchivo)
                        If dtCargues.Count > 0 Then
                            DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar ya se encuentra registrado, por favor seleccione otro archivo de cargue.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                            _File = Nothing
                            ArchivoDesktopTextBox.Text = ""
                        ElseIf Not (RecoleccionDataTable Is Nothing) AndAlso RecoleccionDataTable.Count <= 0 And TipoCargue = "1" Then
                            DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar no corresponde a una fecha de recoleccion semanal valida, por favor seleccione otro archivo de cargue.", "Archivo de Cargue no valido", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                            _File = Nothing
                            ArchivoDesktopTextBox.Text = ""

                        ElseIf Not (RecoleccionDataTable Is Nothing) AndAlso RecoleccionDataTable.Count > 0 And TipoCargue = "1" Then
                            If RecoleccionDataTable(0).Fecha_Recoleccion <> FechaRecoleccion Then
                                DesktopMessageBoxControl.DesktopMessageShow("Una o mas fechas de recolección estan pendientes de cargue, por favor valide con el administrador.", "Archivo de Cargue no valido", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                _File = Nothing
                                ArchivoDesktopTextBox.Text = ""
                            ElseIf RecoleccionDataTable(0).fk_Cargue <> 0 Then
                                DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar corresponde a una fecha de recoleccion semanal ya cargada, por favor seleccione otro archivo de cargue.", "Archivo de Cargue no valido", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                _File = Nothing
                                ArchivoDesktopTextBox.Text = ""
                            Else

                                HabilitarControles(False)
                                Me.CargueBackgroundWorker.RunWorkerAsync(NombreArchivo & "-" & Entidad & "-" & Esquema & "-" & TipoCargue)

                            End If
                        Else

                            HabilitarControles(False)
                            Me.CargueBackgroundWorker.RunWorkerAsync(NombreArchivo & "-" & Entidad & "-" & Esquema & "-" & TipoCargue)
                        End If

                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un tipo de cargue.", "Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    End If

                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un archivo de cargue y un esquema de facturación.", "Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
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

                For i As Integer = 0 To Parametros.Length - 4 Step 1
                    NombreArchivo = Parametros(i)
                Next

            Else
                NombreArchivo = Parametros(0)
            End If

            Dim Entidad = CShort(Parametros(Parametros.Length - 3))
            Dim Esquema = CShort(Parametros(Parametros.Length - 2))
            Dim TipoCargue = CShort(Parametros(Parametros.Length - 1))

            trResultado = CargarArchivo(NombreArchivo, Entidad, Esquema, TipoCargue)
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
                If _TipoCargue <> DesktopConfig.TipoCargue.Prevalidar Then
                    If Not IsNothing(trResultado.OT) Then
                        If Not trResultado.OT.IsNull Then
                            DesktopMessageBoxControl.DesktopMessageShow("Se generó el cargue con OT: " & trResultado.OT.ToString(), "Cargue Parcial [OT: " & trResultado.OT.ToString() & "]", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                        End If
                    End If

                    
                    DesktopMessageBoxControl.DesktopMessageShow("Datos cargados éxitosamente", "Cargue de datos", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Archivo Validado Exitosamente", "Validación", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

                End If

                DatosCargadosDesktopDataGridView.DataSource = _DataFile
                CargueProgressBar.Value = CargueProgressBar.Maximum

            Else
                If Not IsNothing(trResultado.Parameters) Then
                    Dim myError As New FormResultadoValidacion(trResultado)
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