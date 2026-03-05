Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormDocumentos
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDocumentos))
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.AgregarButton = New System.Windows.Forms.Button()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.cbarrasDesktopCBarrasControl = New Miharu.Desktop.Controls.DesktopCBarras.DesktopCBarrasControl()
            Me.DocumentosDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarras = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CProceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.cargado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_file = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_expediente = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_tipologia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipologiaComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.ClaseLabel = New System.Windows.Forms.Label()
            CType(Me.DocumentosDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(397, 409)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(131, 30)
            Me.CerrarButton.TabIndex = 3
            Me.CerrarButton.Text = "&Cerrar Carpeta"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'AgregarButton
            '
            Me.AgregarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AgregarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnAgregar
            Me.AgregarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AgregarButton.Location = New System.Drawing.Point(242, 409)
            Me.AgregarButton.Name = "AgregarButton"
            Me.AgregarButton.Size = New System.Drawing.Size(149, 30)
            Me.AgregarButton.TabIndex = 2
            Me.AgregarButton.Text = "&Agregar documento"
            Me.AgregarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AgregarButton.UseVisualStyleBackColor = True
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(12, 77)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(61, 13)
            Me.Label2.TabIndex = 9
            Me.Label2.Text = "Tipología:"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(12, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(105, 13)
            Me.Label1.TabIndex = 6
            Me.Label1.Text = "Código de Barras:"
            '
            'cbarrasDesktopCBarrasControl
            '
            Me.cbarrasDesktopCBarrasControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cbarrasDesktopCBarrasControl.FocusIn = System.Drawing.Color.LightYellow
            Me.cbarrasDesktopCBarrasControl.FocusOut = System.Drawing.Color.White
            Me.cbarrasDesktopCBarrasControl.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cbarrasDesktopCBarrasControl.Location = New System.Drawing.Point(12, 28)
            Me.cbarrasDesktopCBarrasControl.MaxLength = 0
            Me.cbarrasDesktopCBarrasControl.Name = "cbarrasDesktopCBarrasControl"
            Me.cbarrasDesktopCBarrasControl.ShortcutsEnabled = False
            Me.cbarrasDesktopCBarrasControl.Size = New System.Drawing.Size(516, 20)
            Me.cbarrasDesktopCBarrasControl.TabIndex = 0
            '
            'DocumentosDesktopDataGridView
            '
            Me.DocumentosDesktopDataGridView.AllowUserToAddRows = False
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
            Me.DocumentosDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Tipo, Me.CBarras, Me.Estado, Me.CProceso, Me.fk_OT, Me.id_estado, Me.cargado, Me.id_file, Me.fk_expediente, Me.fk_folder, Me.id_tipologia})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DocumentosDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.DocumentosDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.DocumentosDesktopDataGridView.Location = New System.Drawing.Point(12, 136)
            Me.DocumentosDesktopDataGridView.MultiSelect = False
            Me.DocumentosDesktopDataGridView.Name = "DocumentosDesktopDataGridView"
            Me.DocumentosDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DocumentosDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.DocumentosDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DocumentosDesktopDataGridView.Size = New System.Drawing.Size(516, 237)
            Me.DocumentosDesktopDataGridView.TabIndex = 10
            '
            'Tipo
            '
            Me.Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Tipo.DataPropertyName = "Tipo"
            Me.Tipo.HeaderText = "Tipologia"
            Me.Tipo.Name = "Tipo"
            Me.Tipo.ReadOnly = True
            Me.Tipo.Width = 83
            '
            'CBarras
            '
            Me.CBarras.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.CBarras.DataPropertyName = "CBarras"
            Me.CBarras.HeaderText = "CBarras"
            Me.CBarras.Name = "CBarras"
            Me.CBarras.ReadOnly = True
            Me.CBarras.Width = 76
            '
            'Estado
            '
            Me.Estado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Estado.DataPropertyName = "Estado"
            Me.Estado.HeaderText = "Estado"
            Me.Estado.Name = "Estado"
            Me.Estado.ReadOnly = True
            Me.Estado.Width = 70
            '
            'CProceso
            '
            Me.CProceso.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.CProceso.DataPropertyName = "Proceso"
            Me.CProceso.HeaderText = "Tipo"
            Me.CProceso.Name = "CProceso"
            Me.CProceso.ReadOnly = True
            Me.CProceso.Width = 56
            '
            'fk_OT
            '
            Me.fk_OT.DataPropertyName = "fk_OT"
            Me.fk_OT.HeaderText = "fk_OT"
            Me.fk_OT.Name = "fk_OT"
            Me.fk_OT.ReadOnly = True
            Me.fk_OT.Visible = False
            Me.fk_OT.Width = 65
            '
            'id_estado
            '
            Me.id_estado.DataPropertyName = "id_estado"
            Me.id_estado.HeaderText = "id_estado"
            Me.id_estado.Name = "id_estado"
            Me.id_estado.ReadOnly = True
            Me.id_estado.Visible = False
            Me.id_estado.Width = 88
            '
            'cargado
            '
            Me.cargado.DataPropertyName = "cargado"
            Me.cargado.HeaderText = "cargado"
            Me.cargado.Name = "cargado"
            Me.cargado.ReadOnly = True
            Me.cargado.Visible = False
            Me.cargado.Width = 78
            '
            'id_file
            '
            Me.id_file.DataPropertyName = "id_file"
            Me.id_file.HeaderText = "id_file"
            Me.id_file.Name = "id_file"
            Me.id_file.ReadOnly = True
            Me.id_file.Visible = False
            Me.id_file.Width = 66
            '
            'fk_expediente
            '
            Me.fk_expediente.DataPropertyName = "fk_expediente"
            Me.fk_expediente.HeaderText = "fk_expediente"
            Me.fk_expediente.Name = "fk_expediente"
            Me.fk_expediente.ReadOnly = True
            Me.fk_expediente.Visible = False
            Me.fk_expediente.Width = 114
            '
            'fk_folder
            '
            Me.fk_folder.DataPropertyName = "fk_folder"
            Me.fk_folder.HeaderText = "fk_folder"
            Me.fk_folder.Name = "fk_folder"
            Me.fk_folder.ReadOnly = True
            Me.fk_folder.Visible = False
            Me.fk_folder.Width = 83
            '
            'id_tipologia
            '
            Me.id_tipologia.DataPropertyName = "id_tipologia"
            Me.id_tipologia.HeaderText = "id_tipologia"
            Me.id_tipologia.Name = "id_tipologia"
            Me.id_tipologia.ReadOnly = True
            Me.id_tipologia.Visible = False
            Me.id_tipologia.Width = 98
            '
            'TipologiaComboBox
            '
            Me.TipologiaComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TipologiaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipologiaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipologiaComboBox.DisabledEnter = True
            Me.TipologiaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipologiaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipologiaComboBox.FormattingEnabled = True
            Me.TipologiaComboBox.Location = New System.Drawing.Point(12, 97)
            Me.TipologiaComboBox.Name = "TipologiaComboBox"
            Me.TipologiaComboBox.Size = New System.Drawing.Size(516, 21)
            Me.TipologiaComboBox.TabIndex = 1
            '
            'Label3
            '
            Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(12, 382)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(378, 13)
            Me.Label3.TabIndex = 11
            Me.Label3.Text = "Para eliminar un documento por favor seleccionelo y presione Suprimir [Supr]."
            '
            'ClaseLabel
            '
            Me.ClaseLabel.AutoSize = True
            Me.ClaseLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ClaseLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.ClaseLabel.Location = New System.Drawing.Point(12, 420)
            Me.ClaseLabel.Name = "ClaseLabel"
            Me.ClaseLabel.Size = New System.Drawing.Size(19, 19)
            Me.ClaseLabel.TabIndex = 12
            Me.ClaseLabel.Text = "_"
            '
            'FormDocumentos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(542, 451)
            Me.Controls.Add(Me.ClaseLabel)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.TipologiaComboBox)
            Me.Controls.Add(Me.DocumentosDesktopDataGridView)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.AgregarButton)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.cbarrasDesktopCBarrasControl)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormDocumentos"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Destape Documentos"
            CType(Me.DocumentosDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents cbarrasDesktopCBarrasControl As DesktopCbarrasControl
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents AgregarButton As System.Windows.Forms.Button
        Friend WithEvents DocumentosDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents TipologiaComboBox As DesktopComboBoxControl
        Friend WithEvents Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarras As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Estado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CProceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_OT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_estado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents cargado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_file As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_expediente As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_folder As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_tipologia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents ClaseLabel As System.Windows.Forms.Label
    End Class
End Namespace