Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Library

Namespace Forms.Parametrizacion.Risk

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParDocumentos
        Inherits FormBase

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormParDocumentos))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.AgregarButton = New System.Windows.Forms.Button()
            Me.DocumentosDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.DocumentosComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.EsquemaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.nombre_documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_tipologia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Es_obligatorio = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Folios_Doble_Captura = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Monto_Doble_Captura = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Digitalizar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.fk_entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_esquema = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            CType(Me.DocumentosDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(14, 25)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(78, 13)
            Me.Label1.TabIndex = 7
            Me.Label1.Text = "Documentos"
            '
            'AgregarButton
            '
            Me.AgregarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AgregarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnAgregar
            Me.AgregarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AgregarButton.Location = New System.Drawing.Point(511, 40)
            Me.AgregarButton.Name = "AgregarButton"
            Me.AgregarButton.Size = New System.Drawing.Size(85, 30)
            Me.AgregarButton.TabIndex = 8
            Me.AgregarButton.Text = "&Agregar"
            Me.AgregarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AgregarButton.UseVisualStyleBackColor = True
            '
            'DocumentosDesktopDataGridView
            '
            Me.DocumentosDesktopDataGridView.AllowUserToAddRows = False
            Me.DocumentosDesktopDataGridView.AllowUserToOrderColumns = True
            Me.DocumentosDesktopDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DocumentosDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.DocumentosDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DocumentosDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.DocumentosDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DocumentosDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.nombre_documento, Me.fk_tipologia, Me.Es_obligatorio, Me.Folios_Doble_Captura, Me.Monto_Doble_Captura, Me.Digitalizar, Me.fk_entidad, Me.fk_proyecto, Me.fk_esquema, Me.fk_documento})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DocumentosDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.DocumentosDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.DocumentosDesktopDataGridView.Location = New System.Drawing.Point(14, 51)
            Me.DocumentosDesktopDataGridView.MultiSelect = False
            Me.DocumentosDesktopDataGridView.Name = "DocumentosDesktopDataGridView"
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DocumentosDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.DocumentosDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DocumentosDesktopDataGridView.Size = New System.Drawing.Size(585, 189)
            Me.DocumentosDesktopDataGridView.TabIndex = 18
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(523, 358)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(100, 30)
            Me.CerrarButton.TabIndex = 19
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(417, 358)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(100, 30)
            Me.AceptarButton.TabIndex = 20
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'DocumentosComboBox
            '
            Me.DocumentosComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DocumentosComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DocumentosComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DocumentosComboBox.DisabledEnter = False
            Me.DocumentosComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentosComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DocumentosComboBox.FormattingEnabled = True
            Me.DocumentosComboBox.Location = New System.Drawing.Point(14, 46)
            Me.DocumentosComboBox.Name = "DocumentosComboBox"
            Me.DocumentosComboBox.Size = New System.Drawing.Size(491, 21)
            Me.DocumentosComboBox.TabIndex = 21
            '
            'EsquemaDesktopComboBox
            '
            Me.EsquemaDesktopComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaDesktopComboBox.DisabledEnter = False
            Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaDesktopComboBox.FormattingEnabled = True
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(84, 20)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(424, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 22
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(14, 23)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(64, 13)
            Me.Label2.TabIndex = 23
            Me.Label2.Text = "Esquemas"
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.DocumentosComboBox)
            Me.GroupBox1.Controls.Add(Me.AgregarButton)
            Me.GroupBox1.Location = New System.Drawing.Point(15, 4)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(608, 85)
            Me.GroupBox1.TabIndex = 24
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Agregar documentos"
            '
            'GroupBox2
            '
            Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox2.Controls.Add(Me.Label2)
            Me.GroupBox2.Controls.Add(Me.DocumentosDesktopDataGridView)
            Me.GroupBox2.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.GroupBox2.Location = New System.Drawing.Point(12, 99)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(611, 253)
            Me.GroupBox2.TabIndex = 25
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Configurar"
            '
            'nombre_documento
            '
            Me.nombre_documento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.nombre_documento.DataPropertyName = "nombre_documento"
            Me.nombre_documento.HeaderText = "Documento"
            Me.nombre_documento.Name = "nombre_documento"
            Me.nombre_documento.Width = 97
            '
            'fk_tipologia
            '
            Me.fk_tipologia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.fk_tipologia.DataPropertyName = "fk_tipologia"
            Me.fk_tipologia.HeaderText = "Tipologia"
            Me.fk_tipologia.Name = "fk_tipologia"
            Me.fk_tipologia.Width = 83
            '
            'Es_obligatorio
            '
            Me.Es_obligatorio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Es_obligatorio.DataPropertyName = "Es_obligatorio"
            Me.Es_obligatorio.HeaderText = "Obligatorio"
            Me.Es_obligatorio.Name = "Es_obligatorio"
            Me.Es_obligatorio.Width = 75
            '
            'Folios_Doble_Captura
            '
            Me.Folios_Doble_Captura.DataPropertyName = "Folios_Doble_Captura"
            Me.Folios_Doble_Captura.HeaderText = "Doble Captura Folios"
            Me.Folios_Doble_Captura.Name = "Folios_Doble_Captura"
            Me.Folios_Doble_Captura.Width = 115
            '
            'Monto_Doble_Captura
            '
            Me.Monto_Doble_Captura.DataPropertyName = "Monto_Doble_Captura"
            Me.Monto_Doble_Captura.HeaderText = "Doble Captura Monto"
            Me.Monto_Doble_Captura.Name = "Monto_Doble_Captura"
            Me.Monto_Doble_Captura.Width = 119
            '
            'Digitalizar
            '
            Me.Digitalizar.DataPropertyName = "Digitalizar"
            Me.Digitalizar.HeaderText = "Digitalizar"
            Me.Digitalizar.Name = "Digitalizar"
            Me.Digitalizar.Width = 70
            '
            'fk_entidad
            '
            Me.fk_entidad.DataPropertyName = "fk_entidad"
            Me.fk_entidad.HeaderText = "Entidad"
            Me.fk_entidad.Name = "fk_entidad"
            Me.fk_entidad.Visible = False
            Me.fk_entidad.Width = 74
            '
            'fk_proyecto
            '
            Me.fk_proyecto.DataPropertyName = "fk_proyecto"
            Me.fk_proyecto.HeaderText = "Proyecto"
            Me.fk_proyecto.Name = "fk_proyecto"
            Me.fk_proyecto.Visible = False
            Me.fk_proyecto.Width = 83
            '
            'fk_esquema
            '
            Me.fk_esquema.DataPropertyName = "fk_esquema"
            Me.fk_esquema.HeaderText = "Esquema"
            Me.fk_esquema.Name = "fk_esquema"
            Me.fk_esquema.Visible = False
            Me.fk_esquema.Width = 83
            '
            'fk_documento
            '
            Me.fk_documento.DataPropertyName = "fk_documento"
            Me.fk_documento.HeaderText = "Id"
            Me.fk_documento.Name = "fk_documento"
            Me.fk_documento.Visible = False
            Me.fk_documento.Width = 44
            '
            'FormParDocumentos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(640, 400)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormParDocumentos"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Parametrizacion de Documentos"
            CType(Me.DocumentosDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents AgregarButton As System.Windows.Forms.Button
        Friend WithEvents DocumentosDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents DocumentosComboBox As DesktopComboBoxControl
        Friend WithEvents EsquemaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents nombre_documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_tipologia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Es_obligatorio As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Folios_Doble_Captura As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Monto_Doble_Captura As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Digitalizar As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents fk_entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_esquema As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_documento As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace