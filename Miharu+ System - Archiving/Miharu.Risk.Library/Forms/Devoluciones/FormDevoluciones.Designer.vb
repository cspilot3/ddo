Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Devoluciones
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormDevoluciones
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
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDevoluciones))
            Me.FiltroGroupBox = New System.Windows.Forms.GroupBox()
            Me.PrecintoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.MotivoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.MotivoLabel = New System.Windows.Forms.Label()
            Me.PrecintoLabel = New System.Windows.Forms.Label()
            Me.OTTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.OTLabel = New System.Windows.Forms.Label()
            Me.ReprocesosDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Precinto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarras = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Motivo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.R = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.CBarras_Folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.EmpaqueButton = New System.Windows.Forms.Button()
            Me.MesaControlButton = New System.Windows.Forms.Button()
            Me.DevolucionButton = New System.Windows.Forms.Button()
            Me.GuiaButton = New System.Windows.Forms.Button()
            Me.InformeButton = New System.Windows.Forms.Button()
            Me.ToolTipMensajes = New System.Windows.Forms.ToolTip(Me.components)
            Me.FiltroGroupBox.SuspendLayout()
            CType(Me.ReprocesosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'FiltroGroupBox
            '
            Me.FiltroGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FiltroGroupBox.Controls.Add(Me.PrecintoComboBox)
            Me.FiltroGroupBox.Controls.Add(Me.BuscarButton)
            Me.FiltroGroupBox.Controls.Add(Me.MotivoComboBox)
            Me.FiltroGroupBox.Controls.Add(Me.MotivoLabel)
            Me.FiltroGroupBox.Controls.Add(Me.PrecintoLabel)
            Me.FiltroGroupBox.Controls.Add(Me.OTTextBox)
            Me.FiltroGroupBox.Controls.Add(Me.OTLabel)
            Me.FiltroGroupBox.Location = New System.Drawing.Point(2, 2)
            Me.FiltroGroupBox.Name = "FiltroGroupBox"
            Me.FiltroGroupBox.Size = New System.Drawing.Size(703, 84)
            Me.FiltroGroupBox.TabIndex = 0
            Me.FiltroGroupBox.TabStop = False
            Me.FiltroGroupBox.Text = "Filtro de Búsqueda"
            '
            'PrecintoComboBox
            '
            Me.PrecintoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.PrecintoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.PrecintoComboBox.DisabledEnter = False
            Me.PrecintoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.PrecintoComboBox.fk_Campo = 0
            Me.PrecintoComboBox.fk_Documento = 0
            Me.PrecintoComboBox.fk_Validacion = 0
            Me.PrecintoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.PrecintoComboBox.FormattingEnabled = True
            Me.PrecintoComboBox.Location = New System.Drawing.Point(471, 23)
            Me.PrecintoComboBox.Name = "PrecintoComboBox"
            Me.PrecintoComboBox.Size = New System.Drawing.Size(221, 21)
            Me.PrecintoComboBox.TabIndex = 7
            '
            'BuscarButton
            '
            Me.BuscarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(620, 52)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(75, 23)
            Me.BuscarButton.TabIndex = 6
            Me.BuscarButton.Text = "&Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'MotivoComboBox
            '
            Me.MotivoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.MotivoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.MotivoComboBox.DisabledEnter = False
            Me.MotivoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.MotivoComboBox.fk_Campo = 0
            Me.MotivoComboBox.fk_Documento = 0
            Me.MotivoComboBox.fk_Validacion = 0
            Me.MotivoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.MotivoComboBox.FormattingEnabled = True
            Me.MotivoComboBox.Location = New System.Drawing.Point(82, 54)
            Me.MotivoComboBox.Name = "MotivoComboBox"
            Me.MotivoComboBox.Size = New System.Drawing.Size(271, 21)
            Me.MotivoComboBox.TabIndex = 5
            '
            'MotivoLabel
            '
            Me.MotivoLabel.AutoSize = True
            Me.MotivoLabel.Location = New System.Drawing.Point(8, 57)
            Me.MotivoLabel.Name = "MotivoLabel"
            Me.MotivoLabel.Size = New System.Drawing.Size(49, 13)
            Me.MotivoLabel.TabIndex = 4
            Me.MotivoLabel.Text = "Motivo:"
            '
            'PrecintoLabel
            '
            Me.PrecintoLabel.AutoSize = True
            Me.PrecintoLabel.Location = New System.Drawing.Point(408, 26)
            Me.PrecintoLabel.Name = "PrecintoLabel"
            Me.PrecintoLabel.Size = New System.Drawing.Size(57, 13)
            Me.PrecintoLabel.TabIndex = 2
            Me.PrecintoLabel.Text = "Precinto:"
            '
            'OTTextBox
            '
            Me.OTTextBox._Obligatorio = False
            Me.OTTextBox._PermitePegar = False
            Me.OTTextBox.Cantidad_Decimales = CType(2, Short)
            Me.OTTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.OTTextBox.DateFormat = Nothing
            Me.OTTextBox.DisabledEnter = False
            Me.OTTextBox.DisabledTab = False
            Me.OTTextBox.EnabledShortCuts = False
            Me.OTTextBox.fk_Campo = 0
            Me.OTTextBox.fk_Documento = 0
            Me.OTTextBox.fk_Validacion = 0
            Me.OTTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.OTTextBox.FocusOut = System.Drawing.Color.White
            Me.OTTextBox.Location = New System.Drawing.Point(82, 23)
            Me.OTTextBox.MaskedTextBox_Property = ""
            Me.OTTextBox.MaximumLength = CType(0, Short)
            Me.OTTextBox.MinimumLength = CType(0, Short)
            Me.OTTextBox.Name = "OTTextBox"
            Me.OTTextBox.Obligatorio = False
            Me.OTTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.OTTextBox.Rango = Rango1
            Me.OTTextBox.Size = New System.Drawing.Size(271, 21)
            Me.OTTextBox.TabIndex = 1
            Me.OTTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.OTTextBox.Usa_Decimales = False
            Me.OTTextBox.Validos_Cantidad_Puntos = False
            '
            'OTLabel
            '
            Me.OTLabel.AutoSize = True
            Me.OTLabel.Location = New System.Drawing.Point(8, 26)
            Me.OTLabel.Name = "OTLabel"
            Me.OTLabel.Size = New System.Drawing.Size(28, 13)
            Me.OTLabel.TabIndex = 0
            Me.OTLabel.Text = "O.T."
            '
            'ReprocesosDataGridView
            '
            Me.ReprocesosDataGridView.AllowUserToAddRows = False
            Me.ReprocesosDataGridView.AllowUserToDeleteRows = False
            Me.ReprocesosDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ReprocesosDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.ReprocesosDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ReprocesosDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.ReprocesosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ReprocesosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OT, Me.Precinto, Me.CBarras, Me.Documento, Me.Motivo, Me.R, Me.CBarras_Folder})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.ReprocesosDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.ReprocesosDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.ReprocesosDataGridView.Location = New System.Drawing.Point(2, 93)
            Me.ReprocesosDataGridView.MultiSelect = False
            Me.ReprocesosDataGridView.Name = "ReprocesosDataGridView"
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ReprocesosDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ReprocesosDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle4
            Me.ReprocesosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ReprocesosDataGridView.Size = New System.Drawing.Size(652, 306)
            Me.ReprocesosDataGridView.TabIndex = 1
            '
            'OT
            '
            Me.OT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.OT.DataPropertyName = "fk_OT"
            Me.OT.HeaderText = "OT"
            Me.OT.Name = "OT"
            Me.OT.ReadOnly = True
            Me.OT.Width = 70
            '
            'Precinto
            '
            Me.Precinto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Precinto.DataPropertyName = "id_Precinto"
            Me.Precinto.HeaderText = "Precinto"
            Me.Precinto.Name = "Precinto"
            Me.Precinto.ReadOnly = True
            Me.Precinto.Width = 70
            '
            'CBarras
            '
            Me.CBarras.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.CBarras.DataPropertyName = "CBarras_File"
            Me.CBarras.HeaderText = "C. Barras"
            Me.CBarras.Name = "CBarras"
            Me.CBarras.ReadOnly = True
            '
            'Documento
            '
            Me.Documento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Documento.DataPropertyName = "Nombre_Documento"
            Me.Documento.HeaderText = "Documento"
            Me.Documento.Name = "Documento"
            Me.Documento.ReadOnly = True
            '
            'Motivo
            '
            Me.Motivo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Motivo.DataPropertyName = "Nombre_Reproceso_Motivo"
            Me.Motivo.HeaderText = "Motivo"
            Me.Motivo.Name = "Motivo"
            Me.Motivo.ReadOnly = True
            Me.Motivo.Width = 170
            '
            'R
            '
            Me.R.HeaderText = "R"
            Me.R.Name = "R"
            Me.R.ReadOnly = True
            Me.R.Width = 21
            '
            'CBarras_Folder
            '
            Me.CBarras_Folder.DataPropertyName = "CBarras_Folder"
            Me.CBarras_Folder.HeaderText = "CBarras_Folder"
            Me.CBarras_Folder.Name = "CBarras_Folder"
            Me.CBarras_Folder.ReadOnly = True
            Me.CBarras_Folder.Visible = False
            Me.CBarras_Folder.Width = 118
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.BackColor = System.Drawing.SystemColors.Control
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.FlatAppearance.BorderSize = 0
            Me.CerrarButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.CerrarButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.CerrarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Close_Red
            Me.CerrarButton.Location = New System.Drawing.Point(660, 355)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(45, 44)
            Me.CerrarButton.TabIndex = 4
            Me.ToolTipMensajes.SetToolTip(Me.CerrarButton, "Cerrar")
            Me.CerrarButton.UseVisualStyleBackColor = False
            '
            'EmpaqueButton
            '
            Me.EmpaqueButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EmpaqueButton.BackColor = System.Drawing.SystemColors.Control
            Me.EmpaqueButton.FlatAppearance.BorderColor = System.Drawing.Color.Black
            Me.EmpaqueButton.FlatAppearance.BorderSize = 0
            Me.EmpaqueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.EmpaqueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.EmpaqueButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EmpaqueButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Empaque
            Me.EmpaqueButton.Location = New System.Drawing.Point(660, 305)
            Me.EmpaqueButton.Name = "EmpaqueButton"
            Me.EmpaqueButton.Size = New System.Drawing.Size(45, 44)
            Me.EmpaqueButton.TabIndex = 5
            Me.ToolTipMensajes.SetToolTip(Me.EmpaqueButton, "Trasladar elementos a Empaque.")
            Me.EmpaqueButton.UseVisualStyleBackColor = False
            '
            'MesaControlButton
            '
            Me.MesaControlButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MesaControlButton.BackColor = System.Drawing.SystemColors.Control
            Me.MesaControlButton.FlatAppearance.BorderSize = 0
            Me.MesaControlButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.MesaControlButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.MesaControlButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.MesaControlButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.MesaFisicos
            Me.MesaControlButton.Location = New System.Drawing.Point(660, 255)
            Me.MesaControlButton.Name = "MesaControlButton"
            Me.MesaControlButton.Size = New System.Drawing.Size(45, 44)
            Me.MesaControlButton.TabIndex = 6
            Me.ToolTipMensajes.SetToolTip(Me.MesaControlButton, "Trasladar elementos a Mesa de control")
            Me.MesaControlButton.UseVisualStyleBackColor = False
            '
            'DevolucionButton
            '
            Me.DevolucionButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DevolucionButton.BackColor = System.Drawing.SystemColors.Control
            Me.DevolucionButton.FlatAppearance.BorderSize = 0
            Me.DevolucionButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.DevolucionButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.DevolucionButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DevolucionButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Destape
            Me.DevolucionButton.Location = New System.Drawing.Point(660, 205)
            Me.DevolucionButton.Name = "DevolucionButton"
            Me.DevolucionButton.Size = New System.Drawing.Size(45, 44)
            Me.DevolucionButton.TabIndex = 7
            Me.ToolTipMensajes.SetToolTip(Me.DevolucionButton, "Trasladar elementos a Destape.")
            Me.DevolucionButton.UseVisualStyleBackColor = False
            '
            'GuiaButton
            '
            Me.GuiaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuiaButton.BackColor = System.Drawing.SystemColors.Control
            Me.GuiaButton.CausesValidation = False
            Me.GuiaButton.FlatAppearance.BorderSize = 0
            Me.GuiaButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.GuiaButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.GuiaButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.GuiaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.CrearRemision
            Me.GuiaButton.Location = New System.Drawing.Point(660, 155)
            Me.GuiaButton.Name = "GuiaButton"
            Me.GuiaButton.Size = New System.Drawing.Size(45, 44)
            Me.GuiaButton.TabIndex = 8
            Me.ToolTipMensajes.SetToolTip(Me.GuiaButton, "Generar guía de devolución.")
            Me.GuiaButton.UseVisualStyleBackColor = False
            '
            'InformeButton
            '
            Me.InformeButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.InformeButton.BackColor = System.Drawing.SystemColors.Control
            Me.InformeButton.FlatAppearance.BorderSize = 0
            Me.InformeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.InformeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.InformeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.InformeButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.exportar
            Me.InformeButton.Location = New System.Drawing.Point(660, 105)
            Me.InformeButton.Name = "InformeButton"
            Me.InformeButton.Size = New System.Drawing.Size(45, 44)
            Me.InformeButton.TabIndex = 9
            Me.ToolTipMensajes.SetToolTip(Me.InformeButton, "Generar reporte de información.")
            Me.InformeButton.UseVisualStyleBackColor = False
            '
            'FormDevoluciones
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(706, 404)
            Me.Controls.Add(Me.InformeButton)
            Me.Controls.Add(Me.GuiaButton)
            Me.Controls.Add(Me.DevolucionButton)
            Me.Controls.Add(Me.MesaControlButton)
            Me.Controls.Add(Me.EmpaqueButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.ReprocesosDataGridView)
            Me.Controls.Add(Me.FiltroGroupBox)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormDevoluciones"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Devoluciones"
            Me.FiltroGroupBox.ResumeLayout(False)
            Me.FiltroGroupBox.PerformLayout()
            CType(Me.ReprocesosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents FiltroGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents OTLabel As System.Windows.Forms.Label
        Friend WithEvents OTTextBox As DesktopTextBoxControl
        Friend WithEvents PrecintoLabel As System.Windows.Forms.Label
        Friend WithEvents MotivoComboBox As DesktopComboBoxControl
        Friend WithEvents MotivoLabel As System.Windows.Forms.Label
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents ReprocesosDataGridView As DesktopDataGridViewControl
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents EmpaqueButton As System.Windows.Forms.Button
        Friend WithEvents MesaControlButton As System.Windows.Forms.Button
        Friend WithEvents DevolucionButton As System.Windows.Forms.Button
        Friend WithEvents GuiaButton As System.Windows.Forms.Button
        Friend WithEvents InformeButton As System.Windows.Forms.Button
        Friend WithEvents ToolTipMensajes As System.Windows.Forms.ToolTip
        Friend WithEvents PrecintoComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents OT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Precinto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarras As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Motivo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents R As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents CBarras_Folder As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace