<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormExportClientesEnLog
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
        Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormExportClientesEnLog))
        Me.ArchivoOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.CargueBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.SelecionarArchivoLabel = New System.Windows.Forms.Label()
        Me.CargueProgressBar = New System.Windows.Forms.ProgressBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TotalRegistrosLabel = New System.Windows.Forms.Label()
        Me.ProcesadosTituloLabel = New System.Windows.Forms.Label()
        Me.ProcesadosLabel = New System.Windows.Forms.Label()
        Me.ProgresoGroupBox = New System.Windows.Forms.GroupBox()
        Me.gbCargue = New System.Windows.Forms.GroupBox()
        Me.ArchivoDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
        Me.BuscarArchivoButton = New System.Windows.Forms.Button()
        Me.BuscarCarpetaButton = New System.Windows.Forms.Button()
        Me.lblCarpeta = New System.Windows.Forms.Label()
        Me.CarpetaSalidaTextBox = New System.Windows.Forms.TextBox()
        Me.CargandoPictureBox = New System.Windows.Forms.PictureBox()
        Me.DatosCargadosDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
        Me.CargarButton = New System.Windows.Forms.Button()
        Me.ProgresoGroupBox.SuspendLayout()
        Me.gbCargue.SuspendLayout()
        CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DatosCargadosDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ArchivoOpenFileDialog
        '
        Me.ArchivoOpenFileDialog.Filter = "Archivos de Cargue (*.xls;*.xlsx;)|*.xls;*.xlsx;"
        Me.ArchivoOpenFileDialog.InitialDirectory = """c:\"""
        Me.ArchivoOpenFileDialog.Multiselect = True
        Me.ArchivoOpenFileDialog.ReadOnlyChecked = True
        Me.ArchivoOpenFileDialog.RestoreDirectory = True
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
        Me.SelecionarArchivoLabel.Location = New System.Drawing.Point(34, 33)
        Me.SelecionarArchivoLabel.Name = "SelecionarArchivoLabel"
        Me.SelecionarArchivoLabel.Size = New System.Drawing.Size(163, 13)
        Me.SelecionarArchivoLabel.TabIndex = 30
        Me.SelecionarArchivoLabel.Text = "Seleccionar archivo de log:"
        '
        'CargueProgressBar
        '
        Me.CargueProgressBar.ForeColor = System.Drawing.SystemColors.Desktop
        Me.CargueProgressBar.Location = New System.Drawing.Point(20, 408)
        Me.CargueProgressBar.Name = "CargueProgressBar"
        Me.CargueProgressBar.Size = New System.Drawing.Size(623, 28)
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
        Me.TotalRegistrosLabel.ForeColor = System.Drawing.Color.Black
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
        Me.ProgresoGroupBox.Location = New System.Drawing.Point(20, 109)
        Me.ProgresoGroupBox.Name = "ProgresoGroupBox"
        Me.ProgresoGroupBox.Size = New System.Drawing.Size(183, 68)
        Me.ProgresoGroupBox.TabIndex = 38
        Me.ProgresoGroupBox.TabStop = False
        Me.ProgresoGroupBox.Text = "Progreso"
        '
        'gbCargue
        '
        Me.gbCargue.Controls.Add(Me.ArchivoDesktopTextBox)
        Me.gbCargue.Controls.Add(Me.BuscarArchivoButton)
        Me.gbCargue.Controls.Add(Me.BuscarCarpetaButton)
        Me.gbCargue.Controls.Add(Me.lblCarpeta)
        Me.gbCargue.Controls.Add(Me.CarpetaSalidaTextBox)
        Me.gbCargue.Controls.Add(Me.CargueProgressBar)
        Me.gbCargue.Controls.Add(Me.CargandoPictureBox)
        Me.gbCargue.Controls.Add(Me.DatosCargadosDesktopDataGridView)
        Me.gbCargue.Controls.Add(Me.CargarButton)
        Me.gbCargue.Controls.Add(Me.ProgresoGroupBox)
        Me.gbCargue.Location = New System.Drawing.Point(16, 12)
        Me.gbCargue.Name = "gbCargue"
        Me.gbCargue.Size = New System.Drawing.Size(675, 450)
        Me.gbCargue.TabIndex = 51
        Me.gbCargue.TabStop = False
        Me.gbCargue.Text = "Exportar"
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
        Me.ArchivoDesktopTextBox.Location = New System.Drawing.Point(21, 39)
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
        Me.ArchivoDesktopTextBox.Size = New System.Drawing.Size(538, 21)
        Me.ArchivoDesktopTextBox.TabIndex = 31
        Me.ArchivoDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
        Me.ArchivoDesktopTextBox.Usa_Decimales = False
        Me.ArchivoDesktopTextBox.Validos_Cantidad_Puntos = False
        '
        'BuscarArchivoButton
        '
        Me.BuscarArchivoButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BuscarArchivoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BuscarArchivoButton.Location = New System.Drawing.Point(565, 37)
        Me.BuscarArchivoButton.Name = "BuscarArchivoButton"
        Me.BuscarArchivoButton.Size = New System.Drawing.Size(87, 23)
        Me.BuscarArchivoButton.TabIndex = 32
        Me.BuscarArchivoButton.Text = "&Buscar"
        Me.BuscarArchivoButton.UseVisualStyleBackColor = True
        '
        'BuscarCarpetaButton
        '
        Me.BuscarCarpetaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BuscarCarpetaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnDestape
        Me.BuscarCarpetaButton.Location = New System.Drawing.Point(600, 76)
        Me.BuscarCarpetaButton.Name = "BuscarCarpetaButton"
        Me.BuscarCarpetaButton.Size = New System.Drawing.Size(43, 30)
        Me.BuscarCarpetaButton.TabIndex = 45
        Me.BuscarCarpetaButton.UseVisualStyleBackColor = True
        '
        'lblCarpeta
        '
        Me.lblCarpeta.AutoSize = True
        Me.lblCarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCarpeta.Location = New System.Drawing.Point(17, 64)
        Me.lblCarpeta.Name = "lblCarpeta"
        Me.lblCarpeta.Size = New System.Drawing.Size(108, 13)
        Me.lblCarpeta.TabIndex = 43
        Me.lblCarpeta.Text = "Carpeta de Salida"
        '
        'CarpetaSalidaTextBox
        '
        Me.CarpetaSalidaTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CarpetaSalidaTextBox.Location = New System.Drawing.Point(21, 82)
        Me.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox"
        Me.CarpetaSalidaTextBox.Size = New System.Drawing.Size(573, 21)
        Me.CarpetaSalidaTextBox.TabIndex = 44
        '
        'CargandoPictureBox
        '
        Me.CargandoPictureBox.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.ajax_loader
        Me.CargandoPictureBox.Location = New System.Drawing.Point(256, 219)
        Me.CargandoPictureBox.Name = "CargandoPictureBox"
        Me.CargandoPictureBox.Size = New System.Drawing.Size(146, 112)
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
        Me.DatosCargadosDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
        Me.DatosCargadosDesktopDataGridView.Location = New System.Drawing.Point(21, 183)
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
        Me.DatosCargadosDesktopDataGridView.Size = New System.Drawing.Size(622, 219)
        Me.DatosCargadosDesktopDataGridView.TabIndex = 37
        '
        'CargarButton
        '
        Me.CargarButton.AutoSize = True
        Me.CargarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CargarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.BtnCargar
        Me.CargarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CargarButton.Location = New System.Drawing.Point(525, 115)
        Me.CargarButton.Name = "CargarButton"
        Me.CargarButton.Size = New System.Drawing.Size(118, 62)
        Me.CargarButton.TabIndex = 34
        Me.CargarButton.Text = "&Cargar Archivo"
        Me.CargarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CargarButton.UseVisualStyleBackColor = True
        '
        'FormExportClientesEnLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(703, 469)
        Me.Controls.Add(Me.SelecionarArchivoLabel)
        Me.Controls.Add(Me.gbCargue)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormExportClientesEnLog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Exportar información de clientes en Log por llaves"
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
    Friend WithEvents CargueBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents BuscarArchivoButton As System.Windows.Forms.Button
    Friend WithEvents SelecionarArchivoLabel As System.Windows.Forms.Label
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
    Friend WithEvents gbCargue As System.Windows.Forms.GroupBox
    Friend WithEvents BuscarCarpetaButton As System.Windows.Forms.Button
    Friend WithEvents lblCarpeta As System.Windows.Forms.Label
    Friend WithEvents CarpetaSalidaTextBox As System.Windows.Forms.TextBox
End Class