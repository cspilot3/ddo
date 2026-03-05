Namespace Forms.CargueLog
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCargueLog
        Inherits Miharu.Desktop.Library.FormBase

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
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCargueLog))
            Me.ArchivoOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.CargueBackgroundWorker = New System.ComponentModel.BackgroundWorker()
            Me.SelecionarArchivoLabel = New System.Windows.Forms.Label()
            Me.CargueProgressBar = New System.Windows.Forms.ProgressBar()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.TotalRegistrosLabel = New System.Windows.Forms.Label()
            Me.ProcesadosTituloLabel = New System.Windows.Forms.Label()
            Me.ProcesadosLabel = New System.Windows.Forms.Label()
            Me.ProgresoGroupBox = New System.Windows.Forms.GroupBox()
            Me.TiempoLabel = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.gbCargue = New System.Windows.Forms.GroupBox()
            Me.TabControl1 = New System.Windows.Forms.TabControl()
            Me.TabPage1 = New System.Windows.Forms.TabPage()
            Me.CargandoPictureBox = New System.Windows.Forms.PictureBox()
            Me.DatosCargadosDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.TabPage2 = New System.Windows.Forms.TabPage()
            Me.PictureBox1 = New System.Windows.Forms.PictureBox()
            Me.DatosRegistrosDuplicadosDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.TabPage3 = New System.Windows.Forms.TabPage()
            Me.PictureBox2 = New System.Windows.Forms.PictureBox()
            Me.DatosEstadoPagaresDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.btnExportar = New System.Windows.Forms.Button()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.BuscarArchivoButton = New System.Windows.Forms.Button()
            Me.OpcionesSeparadorGroupBox = New System.Windows.Forms.GroupBox()
            Me.PuntoComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.TabuladorRadioButton = New System.Windows.Forms.RadioButton()
            Me.ComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.DesktopComboBoxControlTiposLog = New System.Windows.Forms.ComboBox()
            Me.CargarButton = New System.Windows.Forms.Button()
            Me.BackgroundTemporal = New System.ComponentModel.BackgroundWorker()
            Me.ArchivoDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.ProgresoGroupBox.SuspendLayout()
            Me.gbCargue.SuspendLayout()
            Me.TabControl1.SuspendLayout()
            Me.TabPage1.SuspendLayout()
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DatosCargadosDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TabPage2.SuspendLayout()
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DatosRegistrosDuplicadosDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TabPage3.SuspendLayout()
            CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DatosEstadoPagaresDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.OpcionesSeparadorGroupBox.SuspendLayout()
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
            Me.SelecionarArchivoLabel.Location = New System.Drawing.Point(34, 35)
            Me.SelecionarArchivoLabel.Name = "SelecionarArchivoLabel"
            Me.SelecionarArchivoLabel.Size = New System.Drawing.Size(185, 13)
            Me.SelecionarArchivoLabel.TabIndex = 30
            Me.SelecionarArchivoLabel.Text = "Seleccionar archivo de cargue:"
            '
            'CargueProgressBar
            '
            Me.CargueProgressBar.ForeColor = System.Drawing.SystemColors.Desktop
            Me.CargueProgressBar.Location = New System.Drawing.Point(20, 448)
            Me.CargueProgressBar.Name = "CargueProgressBar"
            Me.CargueProgressBar.Size = New System.Drawing.Size(338, 28)
            Me.CargueProgressBar.TabIndex = 36
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(8, 21)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(68, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Registros: "
            '
            'TotalRegistrosLabel
            '
            Me.TotalRegistrosLabel.AutoSize = True
            Me.TotalRegistrosLabel.ForeColor = System.Drawing.Color.Red
            Me.TotalRegistrosLabel.Location = New System.Drawing.Point(125, 21)
            Me.TotalRegistrosLabel.Name = "TotalRegistrosLabel"
            Me.TotalRegistrosLabel.Size = New System.Drawing.Size(14, 13)
            Me.TotalRegistrosLabel.TabIndex = 1
            Me.TotalRegistrosLabel.Text = "0"
            '
            'ProcesadosTituloLabel
            '
            Me.ProcesadosTituloLabel.AutoSize = True
            Me.ProcesadosTituloLabel.Location = New System.Drawing.Point(8, 46)
            Me.ProcesadosTituloLabel.Name = "ProcesadosTituloLabel"
            Me.ProcesadosTituloLabel.Size = New System.Drawing.Size(81, 13)
            Me.ProcesadosTituloLabel.TabIndex = 2
            Me.ProcesadosTituloLabel.Text = "Procesados: "
            '
            'ProcesadosLabel
            '
            Me.ProcesadosLabel.AutoSize = True
            Me.ProcesadosLabel.ForeColor = System.Drawing.Color.DarkGreen
            Me.ProcesadosLabel.Location = New System.Drawing.Point(125, 44)
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
            Me.ProgresoGroupBox.Location = New System.Drawing.Point(256, 114)
            Me.ProgresoGroupBox.Name = "ProgresoGroupBox"
            Me.ProgresoGroupBox.Size = New System.Drawing.Size(183, 68)
            Me.ProgresoGroupBox.TabIndex = 38
            Me.ProgresoGroupBox.TabStop = False
            Me.ProgresoGroupBox.Text = "Progreso"
            '
            'TiempoLabel
            '
            Me.TiempoLabel.AutoSize = True
            Me.TiempoLabel.ForeColor = System.Drawing.Color.Maroon
            Me.TiempoLabel.Location = New System.Drawing.Point(384, 463)
            Me.TiempoLabel.Name = "TiempoLabel"
            Me.TiempoLabel.Size = New System.Drawing.Size(55, 13)
            Me.TiempoLabel.TabIndex = 42
            Me.TiempoLabel.Text = "00:00:00"
            Me.TiempoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(17, 59)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(57, 13)
            Me.Label2.TabIndex = 49
            Me.Label2.Text = "Tipo Log"
            '
            'gbCargue
            '
            Me.gbCargue.Controls.Add(Me.TabControl1)
            Me.gbCargue.Controls.Add(Me.btnExportar)
            Me.gbCargue.Controls.Add(Me.Button1)
            Me.gbCargue.Controls.Add(Me.BuscarArchivoButton)
            Me.gbCargue.Controls.Add(Me.OpcionesSeparadorGroupBox)
            Me.gbCargue.Controls.Add(Me.DesktopComboBoxControlTiposLog)
            Me.gbCargue.Controls.Add(Me.TiempoLabel)
            Me.gbCargue.Controls.Add(Me.CargueProgressBar)
            Me.gbCargue.Controls.Add(Me.Label2)
            Me.gbCargue.Controls.Add(Me.CargarButton)
            Me.gbCargue.Controls.Add(Me.ProgresoGroupBox)
            Me.gbCargue.Location = New System.Drawing.Point(16, 12)
            Me.gbCargue.Name = "gbCargue"
            Me.gbCargue.Size = New System.Drawing.Size(631, 495)
            Me.gbCargue.TabIndex = 51
            Me.gbCargue.TabStop = False
            Me.gbCargue.Text = "Cargue"
            '
            'TabControl1
            '
            Me.TabControl1.Controls.Add(Me.TabPage1)
            Me.TabControl1.Controls.Add(Me.TabPage2)
            Me.TabControl1.Controls.Add(Me.TabPage3)
            Me.TabControl1.Location = New System.Drawing.Point(21, 203)
            Me.TabControl1.Name = "TabControl1"
            Me.TabControl1.SelectedIndex = 0
            Me.TabControl1.Size = New System.Drawing.Size(589, 228)
            Me.TabControl1.TabIndex = 55
            '
            'TabPage1
            '
            Me.TabPage1.Controls.Add(Me.CargandoPictureBox)
            Me.TabPage1.Controls.Add(Me.DatosCargadosDesktopDataGridView)
            Me.TabPage1.Location = New System.Drawing.Point(4, 22)
            Me.TabPage1.Name = "TabPage1"
            Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage1.Size = New System.Drawing.Size(581, 202)
            Me.TabPage1.TabIndex = 0
            Me.TabPage1.Text = "Registros Encontrados"
            Me.TabPage1.UseVisualStyleBackColor = True
            '
            'CargandoPictureBox
            '
            Me.CargandoPictureBox.Image = Global.Miharu.Risk.Library.My.Resources.Resources.ajax_loader
            Me.CargandoPictureBox.Location = New System.Drawing.Point(231, 44)
            Me.CargandoPictureBox.Name = "CargandoPictureBox"
            Me.CargandoPictureBox.Size = New System.Drawing.Size(118, 109)
            Me.CargandoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.CargandoPictureBox.TabIndex = 41
            Me.CargandoPictureBox.TabStop = False
            Me.CargandoPictureBox.Visible = False
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
            Me.DatosCargadosDesktopDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DatosCargadosDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.DatosCargadosDesktopDataGridView.Location = New System.Drawing.Point(3, 3)
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
            Me.DatosCargadosDesktopDataGridView.Size = New System.Drawing.Size(575, 196)
            Me.DatosCargadosDesktopDataGridView.TabIndex = 42
            '
            'TabPage2
            '
            Me.TabPage2.Controls.Add(Me.PictureBox1)
            Me.TabPage2.Controls.Add(Me.DatosRegistrosDuplicadosDesktopDataGridView)
            Me.TabPage2.Location = New System.Drawing.Point(4, 22)
            Me.TabPage2.Name = "TabPage2"
            Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage2.Size = New System.Drawing.Size(581, 202)
            Me.TabPage2.TabIndex = 1
            Me.TabPage2.Text = "Registros Duplicados"
            Me.TabPage2.UseVisualStyleBackColor = True
            '
            'PictureBox1
            '
            Me.PictureBox1.Image = Global.Miharu.Risk.Library.My.Resources.Resources.ajax_loader
            Me.PictureBox1.Location = New System.Drawing.Point(231, 44)
            Me.PictureBox1.Name = "PictureBox1"
            Me.PictureBox1.Size = New System.Drawing.Size(124, 105)
            Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PictureBox1.TabIndex = 43
            Me.PictureBox1.TabStop = False
            Me.PictureBox1.Visible = False
            '
            'DatosRegistrosDuplicadosDesktopDataGridView
            '
            Me.DatosRegistrosDuplicadosDesktopDataGridView.AllowUserToAddRows = False
            Me.DatosRegistrosDuplicadosDesktopDataGridView.AllowUserToDeleteRows = False
            Me.DatosRegistrosDuplicadosDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.DatosRegistrosDuplicadosDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DatosRegistrosDuplicadosDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
            Me.DatosRegistrosDuplicadosDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DatosRegistrosDuplicadosDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle5
            Me.DatosRegistrosDuplicadosDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.DatosRegistrosDuplicadosDesktopDataGridView.Location = New System.Drawing.Point(20, 17)
            Me.DatosRegistrosDuplicadosDesktopDataGridView.MultiSelect = False
            Me.DatosRegistrosDuplicadosDesktopDataGridView.Name = "DatosRegistrosDuplicadosDesktopDataGridView"
            Me.DatosRegistrosDuplicadosDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DatosRegistrosDuplicadosDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
            Me.DatosRegistrosDuplicadosDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DatosRegistrosDuplicadosDesktopDataGridView.Size = New System.Drawing.Size(541, 168)
            Me.DatosRegistrosDuplicadosDesktopDataGridView.TabIndex = 42
            '
            'TabPage3
            '
            Me.TabPage3.Controls.Add(Me.PictureBox2)
            Me.TabPage3.Controls.Add(Me.DatosEstadoPagaresDesktopDataGridView)
            Me.TabPage3.Location = New System.Drawing.Point(4, 22)
            Me.TabPage3.Name = "TabPage3"
            Me.TabPage3.Size = New System.Drawing.Size(581, 202)
            Me.TabPage3.TabIndex = 2
            Me.TabPage3.Text = "Estado Pagarés"
            Me.TabPage3.UseVisualStyleBackColor = True
            '
            'PictureBox2
            '
            Me.PictureBox2.Image = Global.Miharu.Risk.Library.My.Resources.Resources.ajax_loader
            Me.PictureBox2.Location = New System.Drawing.Point(231, 44)
            Me.PictureBox2.Name = "PictureBox2"
            Me.PictureBox2.Size = New System.Drawing.Size(124, 105)
            Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PictureBox2.TabIndex = 45
            Me.PictureBox2.TabStop = False
            Me.PictureBox2.Visible = False
            '
            'DatosEstadoPagaresDesktopDataGridView
            '
            Me.DatosEstadoPagaresDesktopDataGridView.AllowUserToAddRows = False
            Me.DatosEstadoPagaresDesktopDataGridView.AllowUserToDeleteRows = False
            Me.DatosEstadoPagaresDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.DatosEstadoPagaresDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DatosEstadoPagaresDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
            Me.DatosEstadoPagaresDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DatosEstadoPagaresDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle8
            Me.DatosEstadoPagaresDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.DatosEstadoPagaresDesktopDataGridView.Location = New System.Drawing.Point(20, 17)
            Me.DatosEstadoPagaresDesktopDataGridView.MultiSelect = False
            Me.DatosEstadoPagaresDesktopDataGridView.Name = "DatosEstadoPagaresDesktopDataGridView"
            Me.DatosEstadoPagaresDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DatosEstadoPagaresDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
            Me.DatosEstadoPagaresDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DatosEstadoPagaresDesktopDataGridView.Size = New System.Drawing.Size(541, 168)
            Me.DatosEstadoPagaresDesktopDataGridView.TabIndex = 44
            '
            'btnExportar
            '
            Me.btnExportar.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnExportar.Image = Global.Miharu.Risk.Library.My.Resources.Resources.BtnXLS
            Me.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnExportar.Location = New System.Drawing.Point(445, 448)
            Me.btnExportar.Name = "btnExportar"
            Me.btnExportar.Size = New System.Drawing.Size(84, 28)
            Me.btnExportar.TabIndex = 54
            Me.btnExportar.Text = "Exportar"
            Me.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnExportar.UseVisualStyleBackColor = True
            '
            'Button1
            '
            Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Button1.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Button1.Location = New System.Drawing.Point(535, 448)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(74, 28)
            Me.Button1.TabIndex = 53
            Me.Button1.Text = "&Cerrar"
            Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.Button1.UseVisualStyleBackColor = True
            '
            'BuscarArchivoButton
            '
            Me.BuscarArchivoButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar
            Me.BuscarArchivoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarArchivoButton.Location = New System.Drawing.Point(535, 18)
            Me.BuscarArchivoButton.Name = "BuscarArchivoButton"
            Me.BuscarArchivoButton.Size = New System.Drawing.Size(75, 23)
            Me.BuscarArchivoButton.TabIndex = 52
            Me.BuscarArchivoButton.Text = "&Buscar"
            Me.BuscarArchivoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarArchivoButton.UseVisualStyleBackColor = True
            '
            'OpcionesSeparadorGroupBox
            '
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.PuntoComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.TabuladorRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.ComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Enabled = False
            Me.OpcionesSeparadorGroupBox.Location = New System.Drawing.Point(21, 93)
            Me.OpcionesSeparadorGroupBox.Name = "OpcionesSeparadorGroupBox"
            Me.OpcionesSeparadorGroupBox.Size = New System.Drawing.Size(200, 91)
            Me.OpcionesSeparadorGroupBox.TabIndex = 51
            Me.OpcionesSeparadorGroupBox.TabStop = False
            Me.OpcionesSeparadorGroupBox.Text = "Opciones de Separador"
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
            'TabuladorRadioButton
            '
            Me.TabuladorRadioButton.AutoSize = True
            Me.TabuladorRadioButton.Location = New System.Drawing.Point(7, 44)
            Me.TabuladorRadioButton.Name = "TabuladorRadioButton"
            Me.TabuladorRadioButton.Size = New System.Drawing.Size(110, 17)
            Me.TabuladorRadioButton.TabIndex = 1
            Me.TabuladorRadioButton.Text = "Tabulador (     )"
            Me.TabuladorRadioButton.UseVisualStyleBackColor = True
            '
            'ComaRadioButton
            '
            Me.ComaRadioButton.AutoSize = True
            Me.ComaRadioButton.Checked = True
            Me.ComaRadioButton.Location = New System.Drawing.Point(7, 21)
            Me.ComaRadioButton.Name = "ComaRadioButton"
            Me.ComaRadioButton.Size = New System.Drawing.Size(73, 17)
            Me.ComaRadioButton.TabIndex = 0
            Me.ComaRadioButton.TabStop = True
            Me.ComaRadioButton.Text = "Coma (,)"
            Me.ComaRadioButton.UseVisualStyleBackColor = True
            '
            'DesktopComboBoxControlTiposLog
            '
            Me.DesktopComboBoxControlTiposLog.FormattingEnabled = True
            Me.DesktopComboBoxControlTiposLog.Location = New System.Drawing.Point(256, 53)
            Me.DesktopComboBoxControlTiposLog.Name = "DesktopComboBoxControlTiposLog"
            Me.DesktopComboBoxControlTiposLog.Size = New System.Drawing.Size(354, 21)
            Me.DesktopComboBoxControlTiposLog.TabIndex = 50
            '
            'CargarButton
            '
            Me.CargarButton.AutoSize = True
            Me.CargarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CargarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.BtnCargar
            Me.CargarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CargarButton.Location = New System.Drawing.Point(488, 114)
            Me.CargarButton.Name = "CargarButton"
            Me.CargarButton.Size = New System.Drawing.Size(118, 62)
            Me.CargarButton.TabIndex = 34
            Me.CargarButton.Text = "&Cargar Archivo"
            Me.CargarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CargarButton.UseVisualStyleBackColor = True
            '
            'BackgroundTemporal
            '
            Me.BackgroundTemporal.WorkerReportsProgress = True
            Me.BackgroundTemporal.WorkerSupportsCancellation = True
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
            Me.ArchivoDesktopTextBox.fk_Campo = 0
            Me.ArchivoDesktopTextBox.fk_Documento = 0
            Me.ArchivoDesktopTextBox.fk_Validacion = 0
            Me.ArchivoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ArchivoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ArchivoDesktopTextBox.Location = New System.Drawing.Point(272, 31)
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
            Me.ArchivoDesktopTextBox.Size = New System.Drawing.Size(263, 21)
            Me.ArchivoDesktopTextBox.TabIndex = 31
            Me.ArchivoDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ArchivoDesktopTextBox.Usa_Decimales = False
            Me.ArchivoDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'FormCargueLog
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(658, 515)
            Me.Controls.Add(Me.ArchivoDesktopTextBox)
            Me.Controls.Add(Me.SelecionarArchivoLabel)
            Me.Controls.Add(Me.gbCargue)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "FormCargueLog"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Cargue"
            Me.ProgresoGroupBox.ResumeLayout(False)
            Me.ProgresoGroupBox.PerformLayout()
            Me.gbCargue.ResumeLayout(False)
            Me.gbCargue.PerformLayout()
            Me.TabControl1.ResumeLayout(False)
            Me.TabPage1.ResumeLayout(False)
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DatosCargadosDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TabPage2.ResumeLayout(False)
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DatosRegistrosDuplicadosDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TabPage3.ResumeLayout(False)
            CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DatosEstadoPagaresDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.OpcionesSeparadorGroupBox.ResumeLayout(False)
            Me.OpcionesSeparadorGroupBox.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ArchivoOpenFileDialog As System.Windows.Forms.OpenFileDialog
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        Friend WithEvents CargueBackgroundWorker As System.ComponentModel.BackgroundWorker
        Friend WithEvents SelecionarArchivoLabel As System.Windows.Forms.Label
        Friend WithEvents ArchivoDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents CargueProgressBar As System.Windows.Forms.ProgressBar
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents TotalRegistrosLabel As System.Windows.Forms.Label
        Friend WithEvents ProcesadosTituloLabel As System.Windows.Forms.Label
        Friend WithEvents ProcesadosLabel As System.Windows.Forms.Label
        Friend WithEvents ProgresoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CargarButton As System.Windows.Forms.Button
        Friend WithEvents CargandoPictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents TiempoLabel As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents gbCargue As System.Windows.Forms.GroupBox
        Friend WithEvents BackgroundTemporal As System.ComponentModel.BackgroundWorker
        Friend WithEvents DesktopComboBoxControlTiposLog As System.Windows.Forms.ComboBox
        Friend WithEvents OpcionesSeparadorGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents PuntoComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents TabuladorRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents ComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents BuscarArchivoButton As System.Windows.Forms.Button
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Friend WithEvents btnExportar As System.Windows.Forms.Button
        Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
        Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
        Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
        Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
        Friend WithEvents DatosRegistrosDuplicadosDesktopDataGridView As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents DatosCargadosDesktopDataGridView As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
        Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
        Friend WithEvents DatosEstadoPagaresDesktopDataGridView As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
    End Class
End Namespace

