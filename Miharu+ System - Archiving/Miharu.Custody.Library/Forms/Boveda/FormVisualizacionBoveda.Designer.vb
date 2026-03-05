Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Boveda
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormVisualizacionBoveda
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormVisualizacionBoveda))
            Me.FiltroBovedaGroupBox = New System.Windows.Forms.GroupBox()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.SeccionComboBox = New DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.BovedaComboBox = New DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.BovedaDataSet = New System.Data.DataSet()
            Me.EstantesGroupBox = New System.Windows.Forms.GroupBox()
            Me.EstantesDataGridView = New DesktopDataGridViewControl()
            Me.Ver = New System.Windows.Forms.DataGridViewImageColumn()
            Me.IdBoveda = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Codigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Filas = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Columnas = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Produndidades = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Largo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Ancho = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Alto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FiltroBovedaGroupBox.SuspendLayout()
            CType(Me.BovedaDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.EstantesGroupBox.SuspendLayout()
            CType(Me.EstantesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'FiltroBovedaGroupBox
            '
            Me.FiltroBovedaGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FiltroBovedaGroupBox.Controls.Add(Me.CerrarButton)
            Me.FiltroBovedaGroupBox.Controls.Add(Me.SeccionComboBox)
            Me.FiltroBovedaGroupBox.Controls.Add(Me.Label2)
            Me.FiltroBovedaGroupBox.Controls.Add(Me.BovedaComboBox)
            Me.FiltroBovedaGroupBox.Controls.Add(Me.Label1)
            Me.FiltroBovedaGroupBox.Location = New System.Drawing.Point(4, 5)
            Me.FiltroBovedaGroupBox.Name = "FiltroBovedaGroupBox"
            Me.FiltroBovedaGroupBox.Size = New System.Drawing.Size(516, 84)
            Me.FiltroBovedaGroupBox.TabIndex = 0
            Me.FiltroBovedaGroupBox.TabStop = False
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(425, 48)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(84, 23)
            Me.CerrarButton.TabIndex = 7
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'SeccionComboBox
            '
            Me.SeccionComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                               Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SeccionComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SeccionComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SeccionComboBox.DisabledEnter = False
            Me.SeccionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SeccionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SeccionComboBox.FormattingEnabled = True
            Me.SeccionComboBox.Location = New System.Drawing.Point(80, 49)
            Me.SeccionComboBox.Name = "SeccionComboBox"
            Me.SeccionComboBox.Size = New System.Drawing.Size(259, 21)
            Me.SeccionComboBox.TabIndex = 3
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(9, 52)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(53, 13)
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "Sección:"
            '
            'BovedaComboBox
            '
            Me.BovedaComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BovedaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.BovedaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.BovedaComboBox.DisabledEnter = False
            Me.BovedaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.BovedaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.BovedaComboBox.FormattingEnabled = True
            Me.BovedaComboBox.Location = New System.Drawing.Point(80, 18)
            Me.BovedaComboBox.Name = "BovedaComboBox"
            Me.BovedaComboBox.Size = New System.Drawing.Size(259, 21)
            Me.BovedaComboBox.TabIndex = 1
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(9, 21)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(52, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Bóveda:"
            '
            'BovedaDataSet
            '
            Me.BovedaDataSet.DataSetName = "BovedaDataSet"
            '
            'EstantesGroupBox
            '
            Me.EstantesGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                 Or System.Windows.Forms.AnchorStyles.Left) _
                                                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EstantesGroupBox.Controls.Add(Me.EstantesDataGridView)
            Me.EstantesGroupBox.Location = New System.Drawing.Point(4, 96)
            Me.EstantesGroupBox.Name = "EstantesGroupBox"
            Me.EstantesGroupBox.Padding = New System.Windows.Forms.Padding(5)
            Me.EstantesGroupBox.Size = New System.Drawing.Size(516, 265)
            Me.EstantesGroupBox.TabIndex = 2
            Me.EstantesGroupBox.TabStop = False
            Me.EstantesGroupBox.Text = "Estantes"
            '
            'EstantesDataGridView
            '
            Me.EstantesDataGridView.AllowUserToAddRows = False
            Me.EstantesDataGridView.AllowUserToDeleteRows = False
            Me.EstantesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.EstantesDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.EstantesDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.EstantesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.EstantesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Ver, Me.IdBoveda, Me.Codigo, Me.Filas, Me.Columnas, Me.Produndidades, Me.Largo, Me.Ancho, Me.Alto})
            Me.EstantesDataGridView.Cursor = System.Windows.Forms.Cursors.Hand
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.EstantesDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.EstantesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.EstantesDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.EstantesDataGridView.Location = New System.Drawing.Point(5, 19)
            Me.EstantesDataGridView.MultiSelect = False
            Me.EstantesDataGridView.Name = "EstantesDataGridView"
            Me.EstantesDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.EstantesDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EstantesDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle4
            Me.EstantesDataGridView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EstantesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.EstantesDataGridView.Size = New System.Drawing.Size(506, 241)
            Me.EstantesDataGridView.TabIndex = 2
            '
            'Ver
            '
            Me.Ver.HeaderText = ""
            Me.Ver.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir
            Me.Ver.Name = "Ver"
            Me.Ver.ReadOnly = True
            Me.Ver.Width = 5
            '
            'IdBoveda
            '
            Me.IdBoveda.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.IdBoveda.DataPropertyName = "id_Boveda_Estante"
            Me.IdBoveda.HeaderText = "Id"
            Me.IdBoveda.Name = "IdBoveda"
            Me.IdBoveda.ReadOnly = True
            '
            'Codigo
            '
            Me.Codigo.DataPropertyName = "Codigo_Boveda_Estante"
            Me.Codigo.HeaderText = "Código"
            Me.Codigo.Name = "Codigo"
            Me.Codigo.ReadOnly = True
            Me.Codigo.Width = 70
            '
            'Filas
            '
            Me.Filas.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Filas.DataPropertyName = "Filas_Boveda_Estante"
            Me.Filas.HeaderText = "Fil."
            Me.Filas.Name = "Filas"
            Me.Filas.ReadOnly = True
            '
            'Columnas
            '
            Me.Columnas.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Columnas.DataPropertyName = "Columnas_Boveda_Estante"
            Me.Columnas.HeaderText = "Col."
            Me.Columnas.Name = "Columnas"
            Me.Columnas.ReadOnly = True
            '
            'Produndidades
            '
            Me.Produndidades.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Produndidades.DataPropertyName = "Profundidades_Boveda_Estante"
            Me.Produndidades.HeaderText = "Prof."
            Me.Produndidades.Name = "Produndidades"
            Me.Produndidades.ReadOnly = True
            '
            'Largo
            '
            Me.Largo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Largo.DataPropertyName = "Largo_Boveda_Estante"
            Me.Largo.HeaderText = "Largo (cms)"
            Me.Largo.Name = "Largo"
            Me.Largo.ReadOnly = True
            '
            'Ancho
            '
            Me.Ancho.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Ancho.DataPropertyName = "Ancho_Boveda_Estante"
            Me.Ancho.HeaderText = "Ancho (cms)"
            Me.Ancho.Name = "Ancho"
            Me.Ancho.ReadOnly = True
            '
            'Alto
            '
            Me.Alto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Alto.DataPropertyName = "Alto_Boveda_Estante"
            Me.Alto.HeaderText = "Alto (cms)"
            Me.Alto.Name = "Alto"
            Me.Alto.ReadOnly = True
            '
            'FormVisualizacionBoveda
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(525, 373)
            Me.Controls.Add(Me.EstantesGroupBox)
            Me.Controls.Add(Me.FiltroBovedaGroupBox)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormVisualizacionBoveda"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Visualización Bóveda"
            Me.FiltroBovedaGroupBox.ResumeLayout(False)
            Me.FiltroBovedaGroupBox.PerformLayout()
            CType(Me.BovedaDataSet, System.ComponentModel.ISupportInitialize).EndInit()
            Me.EstantesGroupBox.ResumeLayout(False)
            CType(Me.EstantesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents FiltroBovedaGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents BovedaDataSet As System.Data.DataSet
        Friend WithEvents BovedaComboBox As DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents SeccionComboBox As DesktopComboBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents EstantesGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents EstantesDataGridView As DesktopDataGridViewControl
        Friend WithEvents Ver As System.Windows.Forms.DataGridViewImageColumn
        Friend WithEvents IdBoveda As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Codigo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Filas As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Columnas As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Produndidades As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Largo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Ancho As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Alto As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace