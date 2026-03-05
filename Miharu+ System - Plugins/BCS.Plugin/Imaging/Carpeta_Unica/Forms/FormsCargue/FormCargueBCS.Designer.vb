Namespace Imaging.Carpeta_Unica.Forms.FormsCargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCargueBCS
        Inherits System.Windows.Forms.Form

        'Form reemplaza a Dispose para limpiar la lista de componentes.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Requerido por el Diseñador de Windows Forms
        Private components As System.ComponentModel.IContainer

        'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
        'Se puede modificar usando el Diseñador de Windows Forms.  
        'No lo modifique con el editor de código.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCargueBCS))
            Me.ArchivoOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.CargueBackgroundWorker = New System.ComponentModel.BackgroundWorker()
            Me.SelecionarArchivoLabel = New System.Windows.Forms.Label()
            Me.lblFechaProceso = New System.Windows.Forms.Label()
            Me.ComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.TabuladorRadioButton = New System.Windows.Forms.RadioButton()
            Me.PuntoComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.OpcionesSeparadorGroupBox = New System.Windows.Forms.GroupBox()
            Me.chkEncabezado = New System.Windows.Forms.CheckBox()
            Me.CargueProgressBar = New System.Windows.Forms.ProgressBar()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.TotalRegistrosLabel = New System.Windows.Forms.Label()
            Me.ProcesadosTituloLabel = New System.Windows.Forms.Label()
            Me.ProcesadosLabel = New System.Windows.Forms.Label()
            Me.ProgresoGroupBox = New System.Windows.Forms.GroupBox()
            Me.EsquemaFacLabel = New System.Windows.Forms.Label()
            Me.TiempoLabel = New System.Windows.Forms.Label()
            Me.lblSeleccionarProceso = New System.Windows.Forms.Label()
            Me.lblSeleccionarJornada = New System.Windows.Forms.Label()
            Me.dtpFechaProceso = New System.Windows.Forms.DateTimePicker()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.gbCargue = New System.Windows.Forms.GroupBox()
            Me.chk_CargaLogsFechasAnteriores = New System.Windows.Forms.CheckBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.dtpFechaEjecucion = New System.Windows.Forms.DateTimePicker()
            Me.CargandoPictureBox = New System.Windows.Forms.PictureBox()
            Me.CargarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.BuscarArchivoButton = New System.Windows.Forms.Button()
            Me.BackgroundTemporal = New System.ComponentModel.BackgroundWorker()
            Me.DesktopComboBoxControlTiposLog = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.DesktopComboBoxControlJornada = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.DesktopComboBoxControlProcesos = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.EsquemaFacturacionComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.DatosCargadosDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.ArchivoDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.OpcionesSeparadorGroupBox.SuspendLayout()
            Me.ProgresoGroupBox.SuspendLayout()
            Me.gbCargue.SuspendLayout()
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DatosCargadosDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ArchivoOpenFileDialog
            '
            Me.ArchivoOpenFileDialog.Filter = "Archivos de Cargue (*.*;*.txt; *.xls;*.xlsx; *.csv;*.dat)|*.txt;*.xls;*.xlsx;*.cs" & _
        "v;*.dat;*.*"
            Me.ArchivoOpenFileDialog.InitialDirectory = """c:\"""
            Me.ArchivoOpenFileDialog.Multiselect = True
            Me.ArchivoOpenFileDialog.ReadOnlyChecked = True
            Me.ArchivoOpenFileDialog.RestoreDirectory = True
            '
            'Timer1
            '
            Me.Timer1.Interval = 1000
            '
            'CargueBackgroundWorker
            '
            Me.CargueBackgroundWorker.WorkerReportsProgress = True
            Me.CargueBackgroundWorker.WorkerSupportsCancellation = True
            '
            'SelecionarArchivoLabel
            '
            Me.SelecionarArchivoLabel.AutoSize = True
            Me.SelecionarArchivoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.SelecionarArchivoLabel.Location = New System.Drawing.Point(29, 58)
            Me.SelecionarArchivoLabel.Name = "SelecionarArchivoLabel"
            Me.SelecionarArchivoLabel.Size = New System.Drawing.Size(185, 13)
            Me.SelecionarArchivoLabel.TabIndex = 30
            Me.SelecionarArchivoLabel.Text = "Seleccionar archivo de cargue:"
            '
            'lblFechaProceso
            '
            Me.lblFechaProceso.AutoSize = True
            Me.lblFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaProceso.Location = New System.Drawing.Point(15, 195)
            Me.lblFechaProceso.Name = "lblFechaProceso"
            Me.lblFechaProceso.Size = New System.Drawing.Size(96, 13)
            Me.lblFechaProceso.TabIndex = 47
            Me.lblFechaProceso.Text = "Fecha Proceso:"
            '
            'ComaRadioButton
            '
            Me.ComaRadioButton.AutoSize = True
            Me.ComaRadioButton.Checked = True
            Me.ComaRadioButton.Location = New System.Drawing.Point(7, 21)
            Me.ComaRadioButton.Name = "ComaRadioButton"
            Me.ComaRadioButton.Size = New System.Drawing.Size(72, 17)
            Me.ComaRadioButton.TabIndex = 0
            Me.ComaRadioButton.TabStop = True
            Me.ComaRadioButton.Text = "Coma (,)"
            Me.ComaRadioButton.UseVisualStyleBackColor = True
            '
            'TabuladorRadioButton
            '
            Me.TabuladorRadioButton.AutoSize = True
            Me.TabuladorRadioButton.Location = New System.Drawing.Point(7, 44)
            Me.TabuladorRadioButton.Name = "TabuladorRadioButton"
            Me.TabuladorRadioButton.Size = New System.Drawing.Size(114, 17)
            Me.TabuladorRadioButton.TabIndex = 1
            Me.TabuladorRadioButton.Text = "Tabulador (     )"
            Me.TabuladorRadioButton.UseVisualStyleBackColor = True
            '
            'PuntoComaRadioButton
            '
            Me.PuntoComaRadioButton.AutoSize = True
            Me.PuntoComaRadioButton.Location = New System.Drawing.Point(6, 67)
            Me.PuntoComaRadioButton.Name = "PuntoComaRadioButton"
            Me.PuntoComaRadioButton.Size = New System.Drawing.Size(119, 17)
            Me.PuntoComaRadioButton.TabIndex = 2
            Me.PuntoComaRadioButton.Text = "Punto y Coma (;)"
            Me.PuntoComaRadioButton.UseVisualStyleBackColor = True
            '
            'OpcionesSeparadorGroupBox
            '
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.PuntoComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.TabuladorRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.ComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Enabled = False
            Me.OpcionesSeparadorGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OpcionesSeparadorGroupBox.Location = New System.Drawing.Point(32, 266)
            Me.OpcionesSeparadorGroupBox.Name = "OpcionesSeparadorGroupBox"
            Me.OpcionesSeparadorGroupBox.Size = New System.Drawing.Size(200, 91)
            Me.OpcionesSeparadorGroupBox.TabIndex = 33
            Me.OpcionesSeparadorGroupBox.TabStop = False
            Me.OpcionesSeparadorGroupBox.Text = "Opciones de Separador"
            '
            'chkEncabezado
            '
            Me.chkEncabezado.AutoSize = True
            Me.chkEncabezado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.chkEncabezado.Location = New System.Drawing.Point(238, 266)
            Me.chkEncabezado.Name = "chkEncabezado"
            Me.chkEncabezado.Size = New System.Drawing.Size(140, 17)
            Me.chkEncabezado.TabIndex = 35
            Me.chkEncabezado.Text = "Maneja encabezado"
            Me.chkEncabezado.UseVisualStyleBackColor = True
            Me.chkEncabezado.Visible = False
            '
            'CargueProgressBar
            '
            Me.CargueProgressBar.ForeColor = System.Drawing.SystemColors.Desktop
            Me.CargueProgressBar.Location = New System.Drawing.Point(32, 588)
            Me.CargueProgressBar.Name = "CargueProgressBar"
            Me.CargueProgressBar.Size = New System.Drawing.Size(385, 28)
            Me.CargueProgressBar.TabIndex = 36
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(7, 21)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(68, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Registros: "
            '
            'TotalRegistrosLabel
            '
            Me.TotalRegistrosLabel.AutoSize = True
            Me.TotalRegistrosLabel.ForeColor = System.Drawing.Color.Red
            Me.TotalRegistrosLabel.Location = New System.Drawing.Point(107, 21)
            Me.TotalRegistrosLabel.Name = "TotalRegistrosLabel"
            Me.TotalRegistrosLabel.Size = New System.Drawing.Size(14, 13)
            Me.TotalRegistrosLabel.TabIndex = 1
            Me.TotalRegistrosLabel.Text = "0"
            '
            'ProcesadosTituloLabel
            '
            Me.ProcesadosTituloLabel.AutoSize = True
            Me.ProcesadosTituloLabel.Location = New System.Drawing.Point(7, 44)
            Me.ProcesadosTituloLabel.Name = "ProcesadosTituloLabel"
            Me.ProcesadosTituloLabel.Size = New System.Drawing.Size(81, 13)
            Me.ProcesadosTituloLabel.TabIndex = 2
            Me.ProcesadosTituloLabel.Text = "Procesados: "
            '
            'ProcesadosLabel
            '
            Me.ProcesadosLabel.AutoSize = True
            Me.ProcesadosLabel.ForeColor = System.Drawing.Color.DarkGreen
            Me.ProcesadosLabel.Location = New System.Drawing.Point(107, 44)
            Me.ProcesadosLabel.Name = "ProcesadosLabel"
            Me.ProcesadosLabel.Size = New System.Drawing.Size(14, 13)
            Me.ProcesadosLabel.TabIndex = 3
            Me.ProcesadosLabel.Text = "0"
            '
            'ProgresoGroupBox
            '
            Me.ProgresoGroupBox.Controls.Add(Me.ProcesadosLabel)
            Me.ProgresoGroupBox.Controls.Add(Me.ProcesadosTituloLabel)
            Me.ProgresoGroupBox.Controls.Add(Me.TotalRegistrosLabel)
            Me.ProgresoGroupBox.Controls.Add(Me.Label1)
            Me.ProgresoGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ProgresoGroupBox.Location = New System.Drawing.Point(238, 289)
            Me.ProgresoGroupBox.Name = "ProgresoGroupBox"
            Me.ProgresoGroupBox.Size = New System.Drawing.Size(228, 68)
            Me.ProgresoGroupBox.TabIndex = 38
            Me.ProgresoGroupBox.TabStop = False
            Me.ProgresoGroupBox.Text = "Progreso"
            '
            'EsquemaFacLabel
            '
            Me.EsquemaFacLabel.AutoSize = True
            Me.EsquemaFacLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EsquemaFacLabel.Location = New System.Drawing.Point(29, 91)
            Me.EsquemaFacLabel.Name = "EsquemaFacLabel"
            Me.EsquemaFacLabel.Size = New System.Drawing.Size(204, 13)
            Me.EsquemaFacLabel.TabIndex = 39
            Me.EsquemaFacLabel.Text = "Seleccionar Esquema Facturación:"
            Me.EsquemaFacLabel.Visible = False
            '
            'TiempoLabel
            '
            Me.TiempoLabel.AutoSize = True
            Me.TiempoLabel.ForeColor = System.Drawing.Color.Maroon
            Me.TiempoLabel.Location = New System.Drawing.Point(433, 596)
            Me.TiempoLabel.Name = "TiempoLabel"
            Me.TiempoLabel.Size = New System.Drawing.Size(49, 13)
            Me.TiempoLabel.TabIndex = 42
            Me.TiempoLabel.Text = "00:00:00"
            Me.TiempoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSeleccionarProceso
            '
            Me.lblSeleccionarProceso.AutoSize = True
            Me.lblSeleccionarProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSeleccionarProceso.Location = New System.Drawing.Point(29, 118)
            Me.lblSeleccionarProceso.Name = "lblSeleccionarProceso"
            Me.lblSeleccionarProceso.Size = New System.Drawing.Size(57, 13)
            Me.lblSeleccionarProceso.TabIndex = 43
            Me.lblSeleccionarProceso.Text = "Proceso:"
            '
            'lblSeleccionarJornada
            '
            Me.lblSeleccionarJornada.AutoSize = True
            Me.lblSeleccionarJornada.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSeleccionarJornada.Location = New System.Drawing.Point(29, 148)
            Me.lblSeleccionarJornada.Name = "lblSeleccionarJornada"
            Me.lblSeleccionarJornada.Size = New System.Drawing.Size(56, 13)
            Me.lblSeleccionarJornada.TabIndex = 45
            Me.lblSeleccionarJornada.Text = "Jornada:"
            '
            'dtpFechaProceso
            '
            Me.dtpFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpFechaProceso.Location = New System.Drawing.Point(233, 201)
            Me.dtpFechaProceso.Name = "dtpFechaProceso"
            Me.dtpFechaProceso.Size = New System.Drawing.Size(192, 20)
            Me.dtpFechaProceso.TabIndex = 48
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(29, 174)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(57, 13)
            Me.Label2.TabIndex = 49
            Me.Label2.Text = "Tipo Log"
            '
            'gbCargue
            '
            Me.gbCargue.Controls.Add(Me.chk_CargaLogsFechasAnteriores)
            Me.gbCargue.Controls.Add(Me.Label3)
            Me.gbCargue.Controls.Add(Me.dtpFechaEjecucion)
            Me.gbCargue.Controls.Add(Me.lblFechaProceso)
            Me.gbCargue.Location = New System.Drawing.Point(14, 12)
            Me.gbCargue.Name = "gbCargue"
            Me.gbCargue.Size = New System.Drawing.Size(579, 626)
            Me.gbCargue.TabIndex = 51
            Me.gbCargue.TabStop = False
            Me.gbCargue.Text = "Cargue"
            '
            'chk_CargaLogsFechasAnteriores
            '
            Me.chk_CargaLogsFechasAnteriores.AutoSize = True
            Me.chk_CargaLogsFechasAnteriores.Location = New System.Drawing.Point(417, 193)
            Me.chk_CargaLogsFechasAnteriores.Name = "chk_CargaLogsFechasAnteriores"
            Me.chk_CargaLogsFechasAnteriores.Size = New System.Drawing.Size(146, 17)
            Me.chk_CargaLogsFechasAnteriores.TabIndex = 48
            Me.chk_CargaLogsFechasAnteriores.Text = "Cargar Log fecha anterior"
            Me.chk_CargaLogsFechasAnteriores.UseVisualStyleBackColor = True
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(15, 223)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(144, 13)
            Me.Label3.TabIndex = 1
            Me.Label3.Text = "Fecha Proceso Anterior:"
            '
            'dtpFechaEjecucion
            '
            Me.dtpFechaEjecucion.Enabled = False
            Me.dtpFechaEjecucion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpFechaEjecucion.Location = New System.Drawing.Point(219, 216)
            Me.dtpFechaEjecucion.Name = "dtpFechaEjecucion"
            Me.dtpFechaEjecucion.Size = New System.Drawing.Size(192, 20)
            Me.dtpFechaEjecucion.TabIndex = 0
            '
            'CargandoPictureBox
            '
            Me.CargandoPictureBox.Image = Global.BCS.Plugin.My.Resources.Resources.ajax_loader
            Me.CargandoPictureBox.Location = New System.Drawing.Point(234, 385)
            Me.CargandoPictureBox.Name = "CargandoPictureBox"
            Me.CargandoPictureBox.Size = New System.Drawing.Size(125, 112)
            Me.CargandoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.CargandoPictureBox.TabIndex = 41
            Me.CargandoPictureBox.TabStop = False
            Me.CargandoPictureBox.Visible = False
            '
            'CargarButton
            '
            Me.CargarButton.AutoSize = True
            Me.CargarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CargarButton.Image = Global.BCS.Plugin.My.Resources.Resources.BtnCargar
            Me.CargarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CargarButton.Location = New System.Drawing.Point(472, 295)
            Me.CargarButton.Name = "CargarButton"
            Me.CargarButton.Size = New System.Drawing.Size(101, 62)
            Me.CargarButton.TabIndex = 34
            Me.CargarButton.Text = "&Cargar Archivo"
            Me.CargarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CargarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.BCS.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(499, 588)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(74, 28)
            Me.CerrarButton.TabIndex = 29
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'BuscarArchivoButton
            '
            Me.BuscarArchivoButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarArchivoButton.Image = Global.BCS.Plugin.My.Resources.Resources.btnBuscar
            Me.BuscarArchivoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarArchivoButton.Location = New System.Drawing.Point(498, 52)
            Me.BuscarArchivoButton.Name = "BuscarArchivoButton"
            Me.BuscarArchivoButton.Size = New System.Drawing.Size(75, 23)
            Me.BuscarArchivoButton.TabIndex = 32
            Me.BuscarArchivoButton.Text = "&Buscar"
            Me.BuscarArchivoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarArchivoButton.UseVisualStyleBackColor = True
            '
            'BackgroundTemporal
            '
            Me.BackgroundTemporal.WorkerReportsProgress = True
            Me.BackgroundTemporal.WorkerSupportsCancellation = True
            '
            'DesktopComboBoxControlTiposLog
            '
            Me.DesktopComboBoxControlTiposLog.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DesktopComboBoxControlTiposLog.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DesktopComboBoxControlTiposLog.DisabledEnter = False
            Me.DesktopComboBoxControlTiposLog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DesktopComboBoxControlTiposLog.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DesktopComboBoxControlTiposLog.FormattingEnabled = True
            Me.DesktopComboBoxControlTiposLog.Location = New System.Drawing.Point(233, 170)
            Me.DesktopComboBoxControlTiposLog.Name = "DesktopComboBoxControlTiposLog"
            Me.DesktopComboBoxControlTiposLog.Size = New System.Drawing.Size(339, 21)
            Me.DesktopComboBoxControlTiposLog.TabIndex = 50
            '
            'DesktopComboBoxControlJornada
            '
            Me.DesktopComboBoxControlJornada.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DesktopComboBoxControlJornada.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DesktopComboBoxControlJornada.DisabledEnter = False
            Me.DesktopComboBoxControlJornada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DesktopComboBoxControlJornada.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DesktopComboBoxControlJornada.FormattingEnabled = True
            Me.DesktopComboBoxControlJornada.Location = New System.Drawing.Point(234, 143)
            Me.DesktopComboBoxControlJornada.Name = "DesktopComboBoxControlJornada"
            Me.DesktopComboBoxControlJornada.Size = New System.Drawing.Size(339, 21)
            Me.DesktopComboBoxControlJornada.TabIndex = 46
            '
            'DesktopComboBoxControlProcesos
            '
            Me.DesktopComboBoxControlProcesos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DesktopComboBoxControlProcesos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DesktopComboBoxControlProcesos.DisabledEnter = False
            Me.DesktopComboBoxControlProcesos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DesktopComboBoxControlProcesos.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DesktopComboBoxControlProcesos.FormattingEnabled = True
            Me.DesktopComboBoxControlProcesos.Location = New System.Drawing.Point(234, 115)
            Me.DesktopComboBoxControlProcesos.Name = "DesktopComboBoxControlProcesos"
            Me.DesktopComboBoxControlProcesos.Size = New System.Drawing.Size(339, 21)
            Me.DesktopComboBoxControlProcesos.TabIndex = 44
            '
            'EsquemaFacturacionComboBox
            '
            Me.EsquemaFacturacionComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaFacturacionComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaFacturacionComboBox.DisabledEnter = False
            Me.EsquemaFacturacionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaFacturacionComboBox.Enabled = False
            Me.EsquemaFacturacionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaFacturacionComboBox.FormattingEnabled = True
            Me.EsquemaFacturacionComboBox.Location = New System.Drawing.Point(234, 88)
            Me.EsquemaFacturacionComboBox.Name = "EsquemaFacturacionComboBox"
            Me.EsquemaFacturacionComboBox.Size = New System.Drawing.Size(339, 21)
            Me.EsquemaFacturacionComboBox.TabIndex = 40
            Me.EsquemaFacturacionComboBox.Visible = False
            '
            'DatosCargadosDesktopDataGridView
            '
            Me.DatosCargadosDesktopDataGridView.AllowUserToAddRows = False
            Me.DatosCargadosDesktopDataGridView.AllowUserToDeleteRows = False
            Me.DatosCargadosDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.DatosCargadosDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DatosCargadosDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.DatosCargadosDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DatosCargadosDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.DatosCargadosDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.DatosCargadosDesktopDataGridView.Location = New System.Drawing.Point(32, 363)
            Me.DatosCargadosDesktopDataGridView.MultiSelect = False
            Me.DatosCargadosDesktopDataGridView.Name = "DatosCargadosDesktopDataGridView"
            Me.DatosCargadosDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DatosCargadosDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.DatosCargadosDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DatosCargadosDesktopDataGridView.Size = New System.Drawing.Size(541, 219)
            Me.DatosCargadosDesktopDataGridView.TabIndex = 37
            '
            'ArchivoDesktopTextBox
            '
            Me.ArchivoDesktopTextBox._Obligatorio = False
            Me.ArchivoDesktopTextBox._PermitePegar = False
            Me.ArchivoDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.ArchivoDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.ArchivoDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ArchivoDesktopTextBox.DateFormat = Nothing
            Me.ArchivoDesktopTextBox.DisabledEnter = False
            Me.ArchivoDesktopTextBox.DisabledTab = False
            Me.ArchivoDesktopTextBox.EnabledShortCuts = False
            Me.ArchivoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ArchivoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ArchivoDesktopTextBox.Location = New System.Drawing.Point(233, 54)
            Me.ArchivoDesktopTextBox.MaskedTextBox_Property = ""
            Me.ArchivoDesktopTextBox.MaximumLength = CType(0, Short)
            Me.ArchivoDesktopTextBox.MinimumLength = CType(0, Short)
            Me.ArchivoDesktopTextBox.Name = "ArchivoDesktopTextBox"
            Me.ArchivoDesktopTextBox.Obligatorio = False
            Me.ArchivoDesktopTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.ArchivoDesktopTextBox.Rango = Rango1
            Me.ArchivoDesktopTextBox.ReadOnly = True
            Me.ArchivoDesktopTextBox.Size = New System.Drawing.Size(259, 20)
            Me.ArchivoDesktopTextBox.TabIndex = 31
            Me.ArchivoDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ArchivoDesktopTextBox.Usa_Decimales = False
            Me.ArchivoDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'FormCargueBCS
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(607, 650)
            Me.Controls.Add(Me.DesktopComboBoxControlTiposLog)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.dtpFechaProceso)
            Me.Controls.Add(Me.DesktopComboBoxControlJornada)
            Me.Controls.Add(Me.lblSeleccionarJornada)
            Me.Controls.Add(Me.DesktopComboBoxControlProcesos)
            Me.Controls.Add(Me.lblSeleccionarProceso)
            Me.Controls.Add(Me.TiempoLabel)
            Me.Controls.Add(Me.CargandoPictureBox)
            Me.Controls.Add(Me.EsquemaFacturacionComboBox)
            Me.Controls.Add(Me.EsquemaFacLabel)
            Me.Controls.Add(Me.CargarButton)
            Me.Controls.Add(Me.ProgresoGroupBox)
            Me.Controls.Add(Me.DatosCargadosDesktopDataGridView)
            Me.Controls.Add(Me.CargueProgressBar)
            Me.Controls.Add(Me.chkEncabezado)
            Me.Controls.Add(Me.ArchivoDesktopTextBox)
            Me.Controls.Add(Me.OpcionesSeparadorGroupBox)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.SelecionarArchivoLabel)
            Me.Controls.Add(Me.BuscarArchivoButton)
            Me.Controls.Add(Me.gbCargue)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "FormCargueBCS"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Cargue"
            Me.OpcionesSeparadorGroupBox.ResumeLayout(False)
            Me.OpcionesSeparadorGroupBox.PerformLayout()
            Me.ProgresoGroupBox.ResumeLayout(False)
            Me.ProgresoGroupBox.PerformLayout()
            Me.gbCargue.ResumeLayout(False)
            Me.gbCargue.PerformLayout()
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DatosCargadosDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ArchivoOpenFileDialog As System.Windows.Forms.OpenFileDialog
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        Friend WithEvents CargueBackgroundWorker As System.ComponentModel.BackgroundWorker
        Friend WithEvents BuscarArchivoButton As System.Windows.Forms.Button
        Friend WithEvents SelecionarArchivoLabel As System.Windows.Forms.Label
        Friend WithEvents lblFechaProceso As System.Windows.Forms.Label
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents ComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents TabuladorRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents PuntoComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents OpcionesSeparadorGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ArchivoDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents chkEncabezado As System.Windows.Forms.CheckBox
        Friend WithEvents CargueProgressBar As System.Windows.Forms.ProgressBar
        Friend WithEvents DatosCargadosDesktopDataGridView As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents TotalRegistrosLabel As System.Windows.Forms.Label
        Friend WithEvents ProcesadosTituloLabel As System.Windows.Forms.Label
        Friend WithEvents ProcesadosLabel As System.Windows.Forms.Label
        Friend WithEvents ProgresoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CargarButton As System.Windows.Forms.Button
        Friend WithEvents EsquemaFacLabel As System.Windows.Forms.Label
        Friend WithEvents EsquemaFacturacionComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents CargandoPictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents TiempoLabel As System.Windows.Forms.Label
        Friend WithEvents lblSeleccionarProceso As System.Windows.Forms.Label
        Friend WithEvents DesktopComboBoxControlProcesos As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents lblSeleccionarJornada As System.Windows.Forms.Label
        Friend WithEvents DesktopComboBoxControlJornada As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents dtpFechaProceso As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents DesktopComboBoxControlTiposLog As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents gbCargue As System.Windows.Forms.GroupBox
        Friend WithEvents BackgroundTemporal As System.ComponentModel.BackgroundWorker
        Friend WithEvents chk_CargaLogsFechasAnteriores As System.Windows.Forms.CheckBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents dtpFechaEjecucion As System.Windows.Forms.DateTimePicker
    End Class
End Namespace

