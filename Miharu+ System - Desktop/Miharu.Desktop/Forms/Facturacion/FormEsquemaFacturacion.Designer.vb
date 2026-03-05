Namespace Forms.Facturacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEsquemaFacturacion
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango3 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango4 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEsquemaFacturacion))
            Me.fk_EntidadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.id_EsquemaDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Nombre_EsquemaDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Descripcion_DesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Codigo_FacturacionDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.ServiciosDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.id_Servicio = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Servicio = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.AplicaServicio = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            Me.EntidadesDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.id_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.AplicaEntidad = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            CType(Me.ServiciosDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox3.SuspendLayout()
            CType(Me.EntidadesDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'fk_EntidadDesktopComboBox
            '
            Me.fk_EntidadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.fk_EntidadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.fk_EntidadDesktopComboBox.DisabledEnter = False
            Me.fk_EntidadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.fk_EntidadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.fk_EntidadDesktopComboBox.FormattingEnabled = True
            Me.fk_EntidadDesktopComboBox.Location = New System.Drawing.Point(159, 33)
            Me.fk_EntidadDesktopComboBox.Name = "fk_EntidadDesktopComboBox"
            Me.fk_EntidadDesktopComboBox.Size = New System.Drawing.Size(326, 21)
            Me.fk_EntidadDesktopComboBox.TabIndex = 0
            '
            'id_EsquemaDesktopTextBox
            '
            Me.id_EsquemaDesktopTextBox.BackColor = System.Drawing.Color.Silver
            Me.id_EsquemaDesktopTextBox.DisabledEnter = False
            Me.id_EsquemaDesktopTextBox.DisabledTab = False
            Me.id_EsquemaDesktopTextBox.Enabled = False
            Me.id_EsquemaDesktopTextBox.FocusIn = System.Drawing.Color.Silver
            Me.id_EsquemaDesktopTextBox.FocusOut = System.Drawing.Color.Silver
            Me.id_EsquemaDesktopTextBox.Location = New System.Drawing.Point(637, 33)
            Me.id_EsquemaDesktopTextBox.Name = "id_EsquemaDesktopTextBox"
            Rango1.MaxValue = 2147483647
            Rango1.MinValue = 0
            Me.id_EsquemaDesktopTextBox.Rango = Rango1
            Me.id_EsquemaDesktopTextBox.Size = New System.Drawing.Size(110, 21)
            Me.id_EsquemaDesktopTextBox.TabIndex = 100
            Me.id_EsquemaDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            '
            'Nombre_EsquemaDesktopTextBox
            '
            Me.Nombre_EsquemaDesktopTextBox.DisabledEnter = False
            Me.Nombre_EsquemaDesktopTextBox.DisabledTab = False
            Me.Nombre_EsquemaDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Nombre_EsquemaDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.Nombre_EsquemaDesktopTextBox.Location = New System.Drawing.Point(159, 63)
            Me.Nombre_EsquemaDesktopTextBox.Name = "Nombre_EsquemaDesktopTextBox"
            Rango2.MaxValue = 2147483647
            Rango2.MinValue = 0
            Me.Nombre_EsquemaDesktopTextBox.Rango = Rango2
            Me.Nombre_EsquemaDesktopTextBox.Size = New System.Drawing.Size(326, 21)
            Me.Nombre_EsquemaDesktopTextBox.TabIndex = 1
            Me.Nombre_EsquemaDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            '
            'Descripcion_DesktopTextBox
            '
            Me.Descripcion_DesktopTextBox.DisabledEnter = False
            Me.Descripcion_DesktopTextBox.DisabledTab = False
            Me.Descripcion_DesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Descripcion_DesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.Descripcion_DesktopTextBox.Location = New System.Drawing.Point(159, 93)
            Me.Descripcion_DesktopTextBox.Multiline = True
            Me.Descripcion_DesktopTextBox.Name = "Descripcion_DesktopTextBox"
            Rango3.MaxValue = 2147483647
            Rango3.MinValue = 0
            Me.Descripcion_DesktopTextBox.Rango = Rango3
            Me.Descripcion_DesktopTextBox.Size = New System.Drawing.Size(588, 56)
            Me.Descripcion_DesktopTextBox.TabIndex = 3
            Me.Descripcion_DesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            '
            'Codigo_FacturacionDesktopTextBox
            '
            Me.Codigo_FacturacionDesktopTextBox.DisabledEnter = False
            Me.Codigo_FacturacionDesktopTextBox.DisabledTab = False
            Me.Codigo_FacturacionDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Codigo_FacturacionDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.Codigo_FacturacionDesktopTextBox.Location = New System.Drawing.Point(637, 63)
            Me.Codigo_FacturacionDesktopTextBox.Name = "Codigo_FacturacionDesktopTextBox"
            Rango4.MaxValue = 2147483647
            Rango4.MinValue = 0
            Me.Codigo_FacturacionDesktopTextBox.Rango = Rango4
            Me.Codigo_FacturacionDesktopTextBox.Size = New System.Drawing.Size(110, 21)
            Me.Codigo_FacturacionDesktopTextBox.TabIndex = 2
            Me.Codigo_FacturacionDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(20, 36)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(49, 13)
            Me.Label1.TabIndex = 34
            Me.Label1.Text = "Entidad"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(508, 36)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(73, 13)
            Me.Label2.TabIndex = 35
            Me.Label2.Text = "Id Esquema"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(20, 66)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(105, 13)
            Me.Label3.TabIndex = 36
            Me.Label3.Text = "Nombre Esquema"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(20, 95)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(126, 13)
            Me.Label4.TabIndex = 37
            Me.Label4.Text = "Descripcion Esquema"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(508, 66)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(114, 13)
            Me.Label5.TabIndex = 38
            Me.Label5.Text = "Codigo Facturacion"
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.Label5)
            Me.GroupBox1.Controls.Add(Me.fk_EntidadDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.id_EsquemaDesktopTextBox)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.Nombre_EsquemaDesktopTextBox)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.Descripcion_DesktopTextBox)
            Me.GroupBox1.Controls.Add(Me.Codigo_FacturacionDesktopTextBox)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(768, 164)
            Me.GroupBox1.TabIndex = 39
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Información Esquema"
            '
            'GroupBox2
            '
            Me.GroupBox2.Controls.Add(Me.ServiciosDesktopDataGridView)
            Me.GroupBox2.Location = New System.Drawing.Point(12, 182)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(369, 193)
            Me.GroupBox2.TabIndex = 40
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Servicios facturables"
            '
            'ServiciosDesktopDataGridView
            '
            Me.ServiciosDesktopDataGridView.AllowUserToAddRows = False
            Me.ServiciosDesktopDataGridView.AllowUserToDeleteRows = False
            Me.ServiciosDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.ServiciosDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ServiciosDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.ServiciosDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ServiciosDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_Servicio, Me.Servicio, Me.AplicaServicio})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.ServiciosDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.ServiciosDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.ServiciosDesktopDataGridView.Location = New System.Drawing.Point(6, 20)
            Me.ServiciosDesktopDataGridView.MultiSelect = False
            Me.ServiciosDesktopDataGridView.Name = "ServiciosDesktopDataGridView"
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ServiciosDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.ServiciosDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ServiciosDesktopDataGridView.Size = New System.Drawing.Size(357, 166)
            Me.ServiciosDesktopDataGridView.TabIndex = 4
            '
            'id_Servicio
            '
            Me.id_Servicio.DataPropertyName = "id_Servicio"
            Me.id_Servicio.HeaderText = "id_Servicio"
            Me.id_Servicio.Name = "id_Servicio"
            Me.id_Servicio.Visible = False
            Me.id_Servicio.Width = 94
            '
            'Servicio
            '
            Me.Servicio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Servicio.DataPropertyName = "Nombre_Servicio"
            Me.Servicio.HeaderText = "Servicio"
            Me.Servicio.Name = "Servicio"
            Me.Servicio.ReadOnly = True
            '
            'AplicaServicio
            '
            Me.AplicaServicio.DataPropertyName = "Aplica"
            Me.AplicaServicio.HeaderText = "Aplica"
            Me.AplicaServicio.Name = "AplicaServicio"
            Me.AplicaServicio.Width = 47
            '
            'GroupBox3
            '
            Me.GroupBox3.Controls.Add(Me.EntidadesDesktopDataGridView)
            Me.GroupBox3.Location = New System.Drawing.Point(403, 182)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Size = New System.Drawing.Size(377, 193)
            Me.GroupBox3.TabIndex = 50
            Me.GroupBox3.TabStop = False
            Me.GroupBox3.Text = "Clientes a aplicar"
            '
            'EntidadesDesktopDataGridView
            '
            Me.EntidadesDesktopDataGridView.AllowUserToAddRows = False
            Me.EntidadesDesktopDataGridView.AllowUserToDeleteRows = False
            Me.EntidadesDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.EntidadesDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.EntidadesDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
            Me.EntidadesDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.EntidadesDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_Entidad, Me.Entidad, Me.AplicaEntidad})
            DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.EntidadesDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle5
            Me.EntidadesDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.EntidadesDesktopDataGridView.Location = New System.Drawing.Point(6, 20)
            Me.EntidadesDesktopDataGridView.MultiSelect = False
            Me.EntidadesDesktopDataGridView.Name = "EntidadesDesktopDataGridView"
            DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.EntidadesDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
            Me.EntidadesDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.EntidadesDesktopDataGridView.Size = New System.Drawing.Size(365, 166)
            Me.EntidadesDesktopDataGridView.TabIndex = 5
            '
            'id_Entidad
            '
            Me.id_Entidad.DataPropertyName = "Entidad_Cliente"
            Me.id_Entidad.HeaderText = "id_Entidad"
            Me.id_Entidad.Name = "id_Entidad"
            Me.id_Entidad.Visible = False
            Me.id_Entidad.Width = 91
            '
            'Entidad
            '
            Me.Entidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Entidad.DataPropertyName = "Nombre_Entidad"
            Me.Entidad.HeaderText = "Entidad"
            Me.Entidad.Name = "Entidad"
            Me.Entidad.ReadOnly = True
            '
            'AplicaEntidad
            '
            Me.AplicaEntidad.DataPropertyName = "Aplica"
            Me.AplicaEntidad.HeaderText = "Aplica"
            Me.AplicaEntidad.Name = "AplicaEntidad"
            Me.AplicaEntidad.Width = 47
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnGuardar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(585, 385)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(100, 30)
            Me.AceptarButton.TabIndex = 6
            Me.AceptarButton.Text = "&Guardar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(691, 385)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(89, 30)
            Me.CerrarButton.TabIndex = 7
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormEsquemaFacturacion
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(792, 427)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.GroupBox3)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.GroupBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.KeyPreview = True
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormEsquemaFacturacion"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Esquemas de Facturacion"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBox2.ResumeLayout(False)
            CType(Me.ServiciosDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox3.ResumeLayout(False)
            CType(Me.EntidadesDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents fk_EntidadDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents id_EsquemaDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Nombre_EsquemaDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Descripcion_DesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Codigo_FacturacionDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents EntidadesDesktopDataGridView As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents ServiciosDesktopDataGridView As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents id_Servicio As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Servicio As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents AplicaServicio As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents id_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents AplicaEntidad As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace