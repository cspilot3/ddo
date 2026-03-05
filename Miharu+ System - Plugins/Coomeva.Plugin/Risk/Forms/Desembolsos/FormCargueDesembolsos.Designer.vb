Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Desembolsos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCargueDesembolsos
        Inherits Miharu.Desktop.Library.FormBase

        'Form overrides dispose to clean up the component list.
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

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCargueDesembolsos))
            Me.ArchivoOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
            Me.CargueBackgroundWorker = New System.ComponentModel.BackgroundWorker()
            Me.CargueProgressBar = New System.Windows.Forms.ProgressBar()
            Me.chkEncabezado = New System.Windows.Forms.CheckBox()
            Me.ArchivoDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.OpcionesSeparadorGroupBox = New System.Windows.Forms.GroupBox()
            Me.PuntoComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.TabuladorRadioButton = New System.Windows.Forms.RadioButton()
            Me.ComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.SelecionarArchivoLabel = New System.Windows.Forms.Label()
            Me.DatosCargadosDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.ProgresoGroupBox = New System.Windows.Forms.GroupBox()
            Me.ProcesadosLabel = New System.Windows.Forms.Label()
            Me.ProcesadosTituloLabel = New System.Windows.Forms.Label()
            Me.TotalRegistrosLabel = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.TiempoLabel = New System.Windows.Forms.Label()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.CargandoPictureBox = New System.Windows.Forms.PictureBox()
            Me.BuscarArchivoButton = New System.Windows.Forms.Button()
            Me.CargarLogButton = New System.Windows.Forms.Button()
            Me.OpcionesSeparadorGroupBox.SuspendLayout()
            CType(Me.DatosCargadosDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ProgresoGroupBox.SuspendLayout()
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ArchivoOpenFileDialog
            '
            Me.ArchivoOpenFileDialog.Filter = "Archivos de Cargue (*.txt; *.xls; *.csv)|*.txt;*.xls;*.csv"
            Me.ArchivoOpenFileDialog.InitialDirectory = """c:\"""
            Me.ArchivoOpenFileDialog.ReadOnlyChecked = True
            Me.ArchivoOpenFileDialog.RestoreDirectory = True
            '
            'CargueBackgroundWorker
            '
            Me.CargueBackgroundWorker.WorkerReportsProgress = True
            Me.CargueBackgroundWorker.WorkerSupportsCancellation = True
            '
            'CargueProgressBar
            '
            Me.CargueProgressBar.ForeColor = System.Drawing.SystemColors.Desktop
            Me.CargueProgressBar.Location = New System.Drawing.Point(6, 376)
            Me.CargueProgressBar.Name = "CargueProgressBar"
            Me.CargueProgressBar.Size = New System.Drawing.Size(385, 28)
            Me.CargueProgressBar.TabIndex = 14
            '
            'chkEncabezado
            '
            Me.chkEncabezado.AutoSize = True
            Me.chkEncabezado.Location = New System.Drawing.Point(212, 54)
            Me.chkEncabezado.Name = "chkEncabezado"
            Me.chkEncabezado.Size = New System.Drawing.Size(139, 17)
            Me.chkEncabezado.TabIndex = 13
            Me.chkEncabezado.Text = "Maneja encabezado"
            Me.chkEncabezado.UseVisualStyleBackColor = True
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
            Me.ArchivoDesktopTextBox.Location = New System.Drawing.Point(203, 16)
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
            Me.ArchivoDesktopTextBox.Size = New System.Drawing.Size(258, 21)
            Me.ArchivoDesktopTextBox.TabIndex = 9
            Me.ArchivoDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ArchivoDesktopTextBox.Usa_Decimales = False
            Me.ArchivoDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'OpcionesSeparadorGroupBox
            '
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.PuntoComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.TabuladorRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.ComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Enabled = False
            Me.OpcionesSeparadorGroupBox.Location = New System.Drawing.Point(6, 54)
            Me.OpcionesSeparadorGroupBox.Name = "OpcionesSeparadorGroupBox"
            Me.OpcionesSeparadorGroupBox.Size = New System.Drawing.Size(200, 91)
            Me.OpcionesSeparadorGroupBox.TabIndex = 11
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
            'SelecionarArchivoLabel
            '
            Me.SelecionarArchivoLabel.AutoSize = True
            Me.SelecionarArchivoLabel.Location = New System.Drawing.Point(2, 19)
            Me.SelecionarArchivoLabel.Name = "SelecionarArchivoLabel"
            Me.SelecionarArchivoLabel.Size = New System.Drawing.Size(179, 13)
            Me.SelecionarArchivoLabel.TabIndex = 8
            Me.SelecionarArchivoLabel.Text = "Seleccionar archivo de cargue:"
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
            Me.DatosCargadosDesktopDataGridView.Location = New System.Drawing.Point(6, 151)
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
            Me.DatosCargadosDesktopDataGridView.TabIndex = 15
            '
            'ProgresoGroupBox
            '
            Me.ProgresoGroupBox.Controls.Add(Me.ProcesadosLabel)
            Me.ProgresoGroupBox.Controls.Add(Me.ProcesadosTituloLabel)
            Me.ProgresoGroupBox.Controls.Add(Me.TotalRegistrosLabel)
            Me.ProgresoGroupBox.Controls.Add(Me.Label1)
            Me.ProgresoGroupBox.Location = New System.Drawing.Point(212, 77)
            Me.ProgresoGroupBox.Name = "ProgresoGroupBox"
            Me.ProgresoGroupBox.Size = New System.Drawing.Size(228, 68)
            Me.ProgresoGroupBox.TabIndex = 16
            Me.ProgresoGroupBox.TabStop = False
            Me.ProgresoGroupBox.Text = "Progreso"
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
            'ProcesadosTituloLabel
            '
            Me.ProcesadosTituloLabel.AutoSize = True
            Me.ProcesadosTituloLabel.Location = New System.Drawing.Point(7, 44)
            Me.ProcesadosTituloLabel.Name = "ProcesadosTituloLabel"
            Me.ProcesadosTituloLabel.Size = New System.Drawing.Size(78, 13)
            Me.ProcesadosTituloLabel.TabIndex = 2
            Me.ProcesadosTituloLabel.Text = "Procesados: "
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
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(7, 21)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(67, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Registros: "
            '
            'Timer1
            '
            Me.Timer1.Interval = 1000
            '
            'TiempoLabel
            '
            Me.TiempoLabel.AutoSize = True
            Me.TiempoLabel.ForeColor = System.Drawing.Color.Maroon
            Me.TiempoLabel.Location = New System.Drawing.Point(407, 384)
            Me.TiempoLabel.Name = "TiempoLabel"
            Me.TiempoLabel.Size = New System.Drawing.Size(55, 13)
            Me.TiempoLabel.TabIndex = 20
            Me.TiempoLabel.Text = "00:00:00"
            Me.TiempoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Coomeva.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(473, 376)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(74, 28)
            Me.CerrarButton.TabIndex = 5
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'CargandoPictureBox
            '
            Me.CargandoPictureBox.Image = Global.Coomeva.Plugin.My.Resources.Resources.ajax_loader
            Me.CargandoPictureBox.Location = New System.Drawing.Point(224, 207)
            Me.CargandoPictureBox.Name = "CargandoPictureBox"
            Me.CargandoPictureBox.Size = New System.Drawing.Size(109, 102)
            Me.CargandoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.CargandoPictureBox.TabIndex = 19
            Me.CargandoPictureBox.TabStop = False
            Me.CargandoPictureBox.Visible = False
            '
            'BuscarArchivoButton
            '
            Me.BuscarArchivoButton.Image = Global.Coomeva.Plugin.My.Resources.Resources.Buscar
            Me.BuscarArchivoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarArchivoButton.Location = New System.Drawing.Point(472, 12)
            Me.BuscarArchivoButton.Name = "BuscarArchivoButton"
            Me.BuscarArchivoButton.Size = New System.Drawing.Size(75, 23)
            Me.BuscarArchivoButton.TabIndex = 10
            Me.BuscarArchivoButton.Text = "&Buscar"
            Me.BuscarArchivoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarArchivoButton.UseVisualStyleBackColor = True
            '
            'CargarLogButton
            '
            Me.CargarLogButton.AutoSize = True
            Me.CargarLogButton.Image = Global.Coomeva.Plugin.My.Resources.Resources.BtnCargar
            Me.CargarLogButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CargarLogButton.Location = New System.Drawing.Point(447, 83)
            Me.CargarLogButton.Name = "CargarLogButton"
            Me.CargarLogButton.Size = New System.Drawing.Size(101, 62)
            Me.CargarLogButton.TabIndex = 24
            Me.CargarLogButton.Text = "&Cargar Archivo"
            Me.CargarLogButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CargarLogButton.UseVisualStyleBackColor = True
            '
            'FormCargueDesembolsos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(553, 415)
            Me.Controls.Add(Me.CargarLogButton)
            Me.Controls.Add(Me.TiempoLabel)
            Me.Controls.Add(Me.CargandoPictureBox)
            Me.Controls.Add(Me.ProgresoGroupBox)
            Me.Controls.Add(Me.DatosCargadosDesktopDataGridView)
            Me.Controls.Add(Me.CargueProgressBar)
            Me.Controls.Add(Me.chkEncabezado)
            Me.Controls.Add(Me.ArchivoDesktopTextBox)
            Me.Controls.Add(Me.OpcionesSeparadorGroupBox)
            Me.Controls.Add(Me.SelecionarArchivoLabel)
            Me.Controls.Add(Me.BuscarArchivoButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCargueDesembolsos"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Cargue Desembolsos"
            Me.OpcionesSeparadorGroupBox.ResumeLayout(False)
            Me.OpcionesSeparadorGroupBox.PerformLayout()
            CType(Me.DatosCargadosDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ProgresoGroupBox.ResumeLayout(False)
            Me.ProgresoGroupBox.PerformLayout()
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ArchivoOpenFileDialog As System.Windows.Forms.OpenFileDialog
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents CargueBackgroundWorker As System.ComponentModel.BackgroundWorker
        Friend WithEvents CargueProgressBar As System.Windows.Forms.ProgressBar
        Friend WithEvents chkEncabezado As System.Windows.Forms.CheckBox
        Friend WithEvents ArchivoDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents OpcionesSeparadorGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents PuntoComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents TabuladorRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents ComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents SelecionarArchivoLabel As System.Windows.Forms.Label
        Friend WithEvents BuscarArchivoButton As System.Windows.Forms.Button
        Friend WithEvents DatosCargadosDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents ProgresoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ProcesadosLabel As System.Windows.Forms.Label
        Friend WithEvents ProcesadosTituloLabel As System.Windows.Forms.Label
        Friend WithEvents TotalRegistrosLabel As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CargandoPictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        Friend WithEvents TiempoLabel As System.Windows.Forms.Label
        Friend WithEvents CargarLogButton As System.Windows.Forms.Button
    End Class
End Namespace