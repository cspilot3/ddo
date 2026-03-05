Namespace Procesos.CargueLog
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCargueLog))
            Me.ArchivoOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.CargueBackgroundWorker = New System.ComponentModel.BackgroundWorker()
            Me.SelecionarArchivoLabel = New System.Windows.Forms.Label()
            Me.lblFechaProceso = New System.Windows.Forms.Label()
            Me.CargueProgressBar = New System.Windows.Forms.ProgressBar()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.TotalRegistrosLabel = New System.Windows.Forms.Label()
            Me.ProcesadosTituloLabel = New System.Windows.Forms.Label()
            Me.ProcesadosLabel = New System.Windows.Forms.Label()
            Me.ProgresoGroupBox = New System.Windows.Forms.GroupBox()
            Me.TiempoLabel = New System.Windows.Forms.Label()
            Me.dtpFechaProceso = New System.Windows.Forms.DateTimePicker()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.gbCargue = New System.Windows.Forms.GroupBox()
            Me.dtpFechaRecaudo = New System.Windows.Forms.DateTimePicker()
            Me.lblFechaRecaudo = New System.Windows.Forms.Label()
            Me.DesktopComboBoxControlTiposLog = New System.Windows.Forms.ComboBox()
            Me.CargandoPictureBox = New System.Windows.Forms.PictureBox()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.DatosCargadosDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.CargarButton = New System.Windows.Forms.Button()
            Me.BuscarArchivoButton = New System.Windows.Forms.Button()
            Me.BackgroundTemporal = New System.ComponentModel.BackgroundWorker()
            Me.ArchivoDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
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
            Me.SelecionarArchivoLabel.Location = New System.Drawing.Point(34, 35)
            Me.SelecionarArchivoLabel.Name = "SelecionarArchivoLabel"
            Me.SelecionarArchivoLabel.Size = New System.Drawing.Size(185, 13)
            Me.SelecionarArchivoLabel.TabIndex = 30
            Me.SelecionarArchivoLabel.Text = "Seleccionar archivo de cargue:"
            '
            'lblFechaProceso
            '
            Me.lblFechaProceso.AutoSize = True
            Me.lblFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaProceso.Location = New System.Drawing.Point(17, 94)
            Me.lblFechaProceso.Name = "lblFechaProceso"
            Me.lblFechaProceso.Size = New System.Drawing.Size(96, 13)
            Me.lblFechaProceso.TabIndex = 47
            Me.lblFechaProceso.Text = "Fecha Proceso:"
            '
            'CargueProgressBar
            '
            Me.CargueProgressBar.ForeColor = System.Drawing.SystemColors.Desktop
            Me.CargueProgressBar.Location = New System.Drawing.Point(21, 481)
            Me.CargueProgressBar.Name = "CargueProgressBar"
            Me.CargueProgressBar.Size = New System.Drawing.Size(449, 28)
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
            Me.ProgresoGroupBox.Location = New System.Drawing.Point(20, 154)
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
            Me.TiempoLabel.Location = New System.Drawing.Point(489, 489)
            Me.TiempoLabel.Name = "TiempoLabel"
            Me.TiempoLabel.Size = New System.Drawing.Size(55, 13)
            Me.TiempoLabel.TabIndex = 42
            Me.TiempoLabel.Text = "00:00:00"
            Me.TiempoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'dtpFechaProceso
            '
            Me.dtpFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpFechaProceso.Location = New System.Drawing.Point(255, 86)
            Me.dtpFechaProceso.Name = "dtpFechaProceso"
            Me.dtpFechaProceso.Size = New System.Drawing.Size(396, 20)
            Me.dtpFechaProceso.TabIndex = 48
            Me.dtpFechaProceso.Value = DateTime.Now
            Me.dtpFechaProceso.MaxDate = DateTime.Now
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
            Me.gbCargue.Controls.Add(Me.dtpFechaRecaudo)
            Me.gbCargue.Controls.Add(Me.lblFechaRecaudo)
            Me.gbCargue.Controls.Add(Me.DesktopComboBoxControlTiposLog)
            Me.gbCargue.Controls.Add(Me.TiempoLabel)
            Me.gbCargue.Controls.Add(Me.dtpFechaProceso)
            Me.gbCargue.Controls.Add(Me.CargueProgressBar)
            Me.gbCargue.Controls.Add(Me.CargandoPictureBox)
            Me.gbCargue.Controls.Add(Me.CerrarButton)
            Me.gbCargue.Controls.Add(Me.Label2)
            Me.gbCargue.Controls.Add(Me.DatosCargadosDesktopDataGridView)
            Me.gbCargue.Controls.Add(Me.CargarButton)
            Me.gbCargue.Controls.Add(Me.ProgresoGroupBox)
            Me.gbCargue.Controls.Add(Me.lblFechaProceso)
            Me.gbCargue.Location = New System.Drawing.Point(16, 12)
            Me.gbCargue.Name = "gbCargue"
            Me.gbCargue.Size = New System.Drawing.Size(675, 523)
            Me.gbCargue.TabIndex = 51
            Me.gbCargue.TabStop = False
            Me.gbCargue.Text = "Cargue"
            '
            'dtpFechaRecaudo
            '
            Me.dtpFechaRecaudo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpFechaRecaudo.Location = New System.Drawing.Point(256, 119)
            Me.dtpFechaRecaudo.Name = "dtpFechaRecaudo"
            Me.dtpFechaRecaudo.Size = New System.Drawing.Size(396, 20)
            Me.dtpFechaRecaudo.TabIndex = 52
            Me.dtpFechaRecaudo.Value = DateTime.Now
            Me.dtpFechaRecaudo.MaxDate = DateTime.Now
            Me.dtpFechaRecaudo.Visible = False
            '
            'lblFechaRecaudo
            '
            Me.lblFechaRecaudo.AutoSize = True
            Me.lblFechaRecaudo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaRecaudo.Location = New System.Drawing.Point(18, 123)
            Me.lblFechaRecaudo.Name = "lblFechaRecaudo"
            Me.lblFechaRecaudo.Size = New System.Drawing.Size(101, 13)
            Me.lblFechaRecaudo.TabIndex = 51
            Me.lblFechaRecaudo.Text = "Fecha Recaudo:"
            Me.lblFechaRecaudo.Visible = False
            '
            'DesktopComboBoxControlTiposLog
            '
            Me.DesktopComboBoxControlTiposLog.FormattingEnabled = True
            Me.DesktopComboBoxControlTiposLog.Location = New System.Drawing.Point(256, 53)
            Me.DesktopComboBoxControlTiposLog.Name = "DesktopComboBoxControlTiposLog"
            Me.DesktopComboBoxControlTiposLog.Size = New System.Drawing.Size(395, 21)
            Me.DesktopComboBoxControlTiposLog.TabIndex = 50
            '
            'CargandoPictureBox
            '
            Me.CargandoPictureBox.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.ajax_loader
            Me.CargandoPictureBox.Location = New System.Drawing.Point(255, 308)
            Me.CargandoPictureBox.Name = "CargandoPictureBox"
            Me.CargandoPictureBox.Size = New System.Drawing.Size(146, 112)
            Me.CargandoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.CargandoPictureBox.TabIndex = 41
            Me.CargandoPictureBox.TabStop = False
            Me.CargandoPictureBox.Visible = False
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(566, 481)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(86, 28)
            Me.CerrarButton.TabIndex = 29
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
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
            Me.DatosCargadosDesktopDataGridView.Location = New System.Drawing.Point(20, 245)
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
            Me.DatosCargadosDesktopDataGridView.Size = New System.Drawing.Size(631, 219)
            Me.DatosCargadosDesktopDataGridView.TabIndex = 37
            '
            'CargarButton
            '
            Me.CargarButton.AutoSize = True
            Me.CargarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CargarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.BtnCargar
            Me.CargarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CargarButton.Location = New System.Drawing.Point(534, 154)
            Me.CargarButton.Name = "CargarButton"
            Me.CargarButton.Size = New System.Drawing.Size(118, 62)
            Me.CargarButton.TabIndex = 34
            Me.CargarButton.Text = "&Cargar Archivo"
            Me.CargarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CargarButton.UseVisualStyleBackColor = True
            '
            'BuscarArchivoButton
            '
            Me.BuscarArchivoButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarArchivoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarArchivoButton.Location = New System.Drawing.Point(581, 29)
            Me.BuscarArchivoButton.Name = "BuscarArchivoButton"
            Me.BuscarArchivoButton.Size = New System.Drawing.Size(87, 23)
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
            Me.ArchivoDesktopTextBox.Size = New System.Drawing.Size(301, 21)
            Me.ArchivoDesktopTextBox.TabIndex = 31
            Me.ArchivoDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ArchivoDesktopTextBox.Usa_Decimales = False
            Me.ArchivoDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'FormCargueLog
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(708, 548)
            Me.Controls.Add(Me.ArchivoDesktopTextBox)
            Me.Controls.Add(Me.SelecionarArchivoLabel)
            Me.Controls.Add(Me.BuscarArchivoButton)
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
        Friend WithEvents ArchivoDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents CargueProgressBar As System.Windows.Forms.ProgressBar
        Friend WithEvents DatosCargadosDesktopDataGridView As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents TotalRegistrosLabel As System.Windows.Forms.Label
        Friend WithEvents ProcesadosTituloLabel As System.Windows.Forms.Label
        Friend WithEvents ProcesadosLabel As System.Windows.Forms.Label
        Friend WithEvents ProgresoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CargarButton As System.Windows.Forms.Button
        Friend WithEvents CargandoPictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents TiempoLabel As System.Windows.Forms.Label
        Friend WithEvents dtpFechaProceso As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents gbCargue As System.Windows.Forms.GroupBox
        Friend WithEvents BackgroundTemporal As System.ComponentModel.BackgroundWorker
        Friend WithEvents DesktopComboBoxControlTiposLog As System.Windows.Forms.ComboBox
        Friend WithEvents dtpFechaRecaudo As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblFechaRecaudo As System.Windows.Forms.Label
    End Class
End Namespace

