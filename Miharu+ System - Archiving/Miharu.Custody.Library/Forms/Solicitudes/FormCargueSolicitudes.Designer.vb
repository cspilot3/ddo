Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.Solicitudes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCargueSolicitudes
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim Rango1 As Rango = New Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCargueSolicitudes))
            Me.ArchivoOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.CargueProgressBar = New System.Windows.Forms.ProgressBar()
            Me.chkEncabezado = New System.Windows.Forms.CheckBox()
            Me.CargarButton = New System.Windows.Forms.Button()
            Me.OpcionesSeparadorGroupBox = New System.Windows.Forms.GroupBox()
            Me.PuntoComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.TabuladorRadioButton = New System.Windows.Forms.RadioButton()
            Me.ComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.SelecionarArchivoLabel = New System.Windows.Forms.Label()
            Me.BuscarArchivoButton = New System.Windows.Forms.Button()
            Me.ProgresoGroupBox = New System.Windows.Forms.GroupBox()
            Me.ProcesadosLabel = New System.Windows.Forms.Label()
            Me.ProcesadosTituloLabel = New System.Windows.Forms.Label()
            Me.TotalRegistrosLabel = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ExportarButton = New System.Windows.Forms.Button()
            Me.ExportarBackgroundWorker = New System.ComponentModel.BackgroundWorker()
            Me.ArchivoSaveFileDialog = New System.Windows.Forms.SaveFileDialog()
            Me.DatosCargadosDesktopDataGridView = New DesktopDataGridViewControl()
            Me.Resultado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Esquema = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Llave1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Llave2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Llave3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Prioridad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Motivo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ArchivoDesktopTextBox = New DesktopTextBoxControl()
            Me.OpcionesSeparadorGroupBox.SuspendLayout()
            Me.ProgresoGroupBox.SuspendLayout()
            CType(Me.DatosCargadosDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ArchivoOpenFileDialog
            '
            Me.ArchivoOpenFileDialog.Filter = "Archivos de Cargue (*.txt; *.xls; *.csv)|*.txt;*.xls;*.csv"
            Me.ArchivoOpenFileDialog.InitialDirectory = """c:\"""
            Me.ArchivoOpenFileDialog.ReadOnlyChecked = True
            Me.ArchivoOpenFileDialog.RestoreDirectory = True
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(472, 395)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(74, 28)
            Me.CerrarButton.TabIndex = 5
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'CargueProgressBar
            '
            Me.CargueProgressBar.ForeColor = System.Drawing.SystemColors.Desktop
            Me.CargueProgressBar.Location = New System.Drawing.Point(5, 395)
            Me.CargueProgressBar.Name = "CargueProgressBar"
            Me.CargueProgressBar.Size = New System.Drawing.Size(419, 28)
            Me.CargueProgressBar.TabIndex = 14
            '
            'chkEncabezado
            '
            Me.chkEncabezado.AutoSize = True
            Me.chkEncabezado.Location = New System.Drawing.Point(211, 43)
            Me.chkEncabezado.Name = "chkEncabezado"
            Me.chkEncabezado.Size = New System.Drawing.Size(139, 17)
            Me.chkEncabezado.TabIndex = 13
            Me.chkEncabezado.Text = "Maneja encabezado"
            Me.chkEncabezado.UseVisualStyleBackColor = True
            '
            'CargarButton
            '
            Me.CargarButton.AutoSize = True
            Me.CargarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Cargue
            Me.CargarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CargarButton.Location = New System.Drawing.Point(445, 72)
            Me.CargarButton.Name = "CargarButton"
            Me.CargarButton.Size = New System.Drawing.Size(101, 62)
            Me.CargarButton.TabIndex = 12
            Me.CargarButton.Text = "&Cargar Archivo"
            Me.CargarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CargarButton.UseVisualStyleBackColor = True
            '
            'OpcionesSeparadorGroupBox
            '
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.PuntoComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.TabuladorRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.ComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Enabled = False
            Me.OpcionesSeparadorGroupBox.Location = New System.Drawing.Point(5, 43)
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
            Me.SelecionarArchivoLabel.Location = New System.Drawing.Point(2, 9)
            Me.SelecionarArchivoLabel.Name = "SelecionarArchivoLabel"
            Me.SelecionarArchivoLabel.Size = New System.Drawing.Size(179, 13)
            Me.SelecionarArchivoLabel.TabIndex = 8
            Me.SelecionarArchivoLabel.Text = "Seleccionar archivo de cargue:"
            '
            'BuscarArchivoButton
            '
            Me.BuscarArchivoButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnBuscar
            Me.BuscarArchivoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarArchivoButton.Location = New System.Drawing.Point(471, 3)
            Me.BuscarArchivoButton.Name = "BuscarArchivoButton"
            Me.BuscarArchivoButton.Size = New System.Drawing.Size(75, 23)
            Me.BuscarArchivoButton.TabIndex = 10
            Me.BuscarArchivoButton.Text = "&Buscar"
            Me.BuscarArchivoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarArchivoButton.UseVisualStyleBackColor = True
            '
            'ProgresoGroupBox
            '
            Me.ProgresoGroupBox.Controls.Add(Me.ProcesadosLabel)
            Me.ProgresoGroupBox.Controls.Add(Me.ProcesadosTituloLabel)
            Me.ProgresoGroupBox.Controls.Add(Me.TotalRegistrosLabel)
            Me.ProgresoGroupBox.Controls.Add(Me.Label1)
            Me.ProgresoGroupBox.Location = New System.Drawing.Point(211, 66)
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
            'ExportarButton
            '
            Me.ExportarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.excel_csv
            Me.ExportarButton.Location = New System.Drawing.Point(430, 395)
            Me.ExportarButton.Name = "ExportarButton"
            Me.ExportarButton.Size = New System.Drawing.Size(35, 28)
            Me.ExportarButton.TabIndex = 17
            Me.ExportarButton.UseVisualStyleBackColor = True
            '
            'ExportarBackgroundWorker
            '
            Me.ExportarBackgroundWorker.WorkerReportsProgress = True
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
            Me.DatosCargadosDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            Me.DatosCargadosDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Resultado, Me.Entidad, Me.Proyecto, Me.Esquema, Me.Documento, Me.Llave1, Me.Llave2, Me.Llave3, Me.Prioridad, Me.Tipo, Me.Motivo})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DatosCargadosDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.DatosCargadosDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.DatosCargadosDesktopDataGridView.Location = New System.Drawing.Point(5, 140)
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
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DatosCargadosDesktopDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle4
            Me.DatosCargadosDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DatosCargadosDesktopDataGridView.Size = New System.Drawing.Size(541, 249)
            Me.DatosCargadosDesktopDataGridView.TabIndex = 15
            '
            'Resultado
            '
            Me.Resultado.DataPropertyName = "Resultado"
            Me.Resultado.HeaderText = "Resultado"
            Me.Resultado.Name = "Resultado"
            Me.Resultado.ReadOnly = True
            Me.Resultado.Width = 89
            '
            'Entidad
            '
            Me.Entidad.DataPropertyName = "Nombre_Entidad"
            Me.Entidad.HeaderText = "Entidad"
            Me.Entidad.Name = "Entidad"
            Me.Entidad.ReadOnly = True
            Me.Entidad.Width = 74
            '
            'Proyecto
            '
            Me.Proyecto.DataPropertyName = "Nombre_Proyecto"
            Me.Proyecto.HeaderText = "Proyecto"
            Me.Proyecto.Name = "Proyecto"
            Me.Proyecto.ReadOnly = True
            Me.Proyecto.Width = 83
            '
            'Esquema
            '
            Me.Esquema.DataPropertyName = "Nombre_Esquema"
            Me.Esquema.HeaderText = "Esquema"
            Me.Esquema.Name = "Esquema"
            Me.Esquema.ReadOnly = True
            Me.Esquema.Width = 83
            '
            'Documento
            '
            Me.Documento.DataPropertyName = "Nombre_Documento"
            Me.Documento.HeaderText = "Documento"
            Me.Documento.Name = "Documento"
            Me.Documento.ReadOnly = True
            Me.Documento.Width = 97
            '
            'Llave1
            '
            Me.Llave1.DataPropertyName = "Llave1"
            Me.Llave1.HeaderText = "Llave 1"
            Me.Llave1.Name = "Llave1"
            Me.Llave1.ReadOnly = True
            Me.Llave1.Width = 72
            '
            'Llave2
            '
            Me.Llave2.DataPropertyName = "Llave2"
            Me.Llave2.HeaderText = "Llave 2"
            Me.Llave2.Name = "Llave2"
            Me.Llave2.ReadOnly = True
            Me.Llave2.Width = 72
            '
            'Llave3
            '
            Me.Llave3.DataPropertyName = "Llave3"
            Me.Llave3.HeaderText = "Llave 3"
            Me.Llave3.Name = "Llave3"
            Me.Llave3.ReadOnly = True
            Me.Llave3.Width = 72
            '
            'Prioridad
            '
            Me.Prioridad.DataPropertyName = "Prioridad"
            Me.Prioridad.HeaderText = "Prioridad"
            Me.Prioridad.Name = "Prioridad"
            Me.Prioridad.ReadOnly = True
            Me.Prioridad.Width = 83
            '
            'Tipo
            '
            Me.Tipo.DataPropertyName = "Tipo"
            Me.Tipo.HeaderText = "Tipo"
            Me.Tipo.Name = "Tipo"
            Me.Tipo.ReadOnly = True
            Me.Tipo.Width = 56
            '
            'Motivo
            '
            Me.Motivo.DataPropertyName = "Motivo"
            Me.Motivo.HeaderText = "Motivo"
            Me.Motivo.Name = "Motivo"
            Me.Motivo.ReadOnly = True
            Me.Motivo.Width = 71
            '
            'ArchivoDesktopTextBox
            '
            Me.ArchivoDesktopTextBox.BackColor = System.Drawing.Color.White
            Me.ArchivoDesktopTextBox.DisabledEnter = False
            Me.ArchivoDesktopTextBox.DisabledTab = False
            Me.ArchivoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ArchivoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ArchivoDesktopTextBox.Location = New System.Drawing.Point(187, 5)
            Me.ArchivoDesktopTextBox.Name = "ArchivoDesktopTextBox"
            Rango1.MaxValue = CType(2147483647, Long)
            Rango1.MinValue = CType(0, Long)
            Me.ArchivoDesktopTextBox.Rango = Rango1
            Me.ArchivoDesktopTextBox.ReadOnly = True
            Me.ArchivoDesktopTextBox.ShortcutsEnabled = False
            Me.ArchivoDesktopTextBox.Size = New System.Drawing.Size(278, 21)
            Me.ArchivoDesktopTextBox.TabIndex = 9
            Me.ArchivoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            '
            'FormCargueSolicitudes
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(553, 429)
            Me.Controls.Add(Me.ExportarButton)
            Me.Controls.Add(Me.ProgresoGroupBox)
            Me.Controls.Add(Me.DatosCargadosDesktopDataGridView)
            Me.Controls.Add(Me.CargueProgressBar)
            Me.Controls.Add(Me.chkEncabezado)
            Me.Controls.Add(Me.CargarButton)
            Me.Controls.Add(Me.ArchivoDesktopTextBox)
            Me.Controls.Add(Me.OpcionesSeparadorGroupBox)
            Me.Controls.Add(Me.SelecionarArchivoLabel)
            Me.Controls.Add(Me.BuscarArchivoButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCargueSolicitudes"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Cargue Solicitudes Masivas"
            Me.OpcionesSeparadorGroupBox.ResumeLayout(False)
            Me.OpcionesSeparadorGroupBox.PerformLayout()
            Me.ProgresoGroupBox.ResumeLayout(False)
            Me.ProgresoGroupBox.PerformLayout()
            CType(Me.DatosCargadosDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ArchivoOpenFileDialog As System.Windows.Forms.OpenFileDialog
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents CargueProgressBar As System.Windows.Forms.ProgressBar
        Friend WithEvents chkEncabezado As System.Windows.Forms.CheckBox
        Friend WithEvents CargarButton As System.Windows.Forms.Button
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
        Friend WithEvents Resultado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Esquema As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Llave1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Llave2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Llave3 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Prioridad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Motivo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ExportarButton As System.Windows.Forms.Button
        Friend WithEvents ExportarBackgroundWorker As System.ComponentModel.BackgroundWorker
        Friend WithEvents ArchivoSaveFileDialog As System.Windows.Forms.SaveFileDialog
    End Class
End Namespace