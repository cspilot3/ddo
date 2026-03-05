Imports Miharu.Desktop.Controls.DesktopDataGridView

Namespace Forms.Inserciones
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormInserciones
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormInserciones))
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.KeyDataGridView = New DesktopDataGridViewControl()
            Me.CTAExpedienteLLaveDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.CBarrasLabel = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.InsertarFolderButton = New System.Windows.Forms.Button()
            Me.InsertarDocumentoButton = New System.Windows.Forms.Button()
            Me.DocumentosDataGridView = New DesktopDataGridViewControl()
            Me.IdTipologiaDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreTipologiaDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FoliosFileDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.MontoFileDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarrasFileDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CTAFileDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdProyectoLlaveDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreProyectoLlaveDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ValorLlaveDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Panel1.SuspendLayout()
            CType(Me.KeyDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CTAExpedienteLLaveDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DocumentosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CTAFileDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.KeyDataGridView)
            Me.Panel1.Controls.Add(Me.CBarrasLabel)
            Me.Panel1.Controls.Add(Me.Label1)
            Me.Panel1.Location = New System.Drawing.Point(13, 12)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Padding = New System.Windows.Forms.Padding(6, 5, 6, 5)
            Me.Panel1.Size = New System.Drawing.Size(599, 78)
            Me.Panel1.TabIndex = 0
            '
            'KeyDataGridView
            '
            Me.KeyDataGridView.AllowUserToAddRows = False
            Me.KeyDataGridView.AllowUserToDeleteRows = False
            Me.KeyDataGridView.AutoGenerateColumns = False
            Me.KeyDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.KeyDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.KeyDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.KeyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.KeyDataGridView.ColumnHeadersVisible = False
            Me.KeyDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdProyectoLlaveDataGridViewTextBoxColumn, Me.NombreProyectoLlaveDataGridViewTextBoxColumn, Me.ValorLlaveDataGridViewTextBoxColumn})
            Me.KeyDataGridView.DataSource = Me.CTAExpedienteLLaveDataTableBindingSource
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.KeyDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.KeyDataGridView.Dock = System.Windows.Forms.DockStyle.Right
            Me.KeyDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.KeyDataGridView.Location = New System.Drawing.Point(132, 5)
            Me.KeyDataGridView.MultiSelect = False
            Me.KeyDataGridView.Name = "KeyDataGridView"
            Me.KeyDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.KeyDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.KeyDataGridView.RowHeadersVisible = False
            Me.KeyDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.KeyDataGridView.Size = New System.Drawing.Size(461, 68)
            Me.KeyDataGridView.TabIndex = 3
            '
            'CTAExpedienteLLaveDataTableBindingSource
            '
            Me.CTAExpedienteLLaveDataTableBindingSource.DataSource = GetType(DBCore.SchemaProcess.CTA_Expediente_LLaveDataTable)
            '
            'CBarrasLabel
            '
            Me.CBarrasLabel.AutoSize = True
            Me.CBarrasLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CBarrasLabel.Location = New System.Drawing.Point(9, 25)
            Me.CBarrasLabel.Name = "CBarrasLabel"
            Me.CBarrasLabel.Size = New System.Drawing.Size(91, 13)
            Me.CBarrasLabel.TabIndex = 1
            Me.CBarrasLabel.Text = "000111111111"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(10, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(102, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Código de barras"
            '
            'InsertarFolderButton
            '
            Me.InsertarFolderButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.folder_add2
            Me.InsertarFolderButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.InsertarFolderButton.Location = New System.Drawing.Point(232, 393)
            Me.InsertarFolderButton.Name = "InsertarFolderButton"
            Me.InsertarFolderButton.Size = New System.Drawing.Size(139, 30)
            Me.InsertarFolderButton.TabIndex = 5
            Me.InsertarFolderButton.Text = "&Agregar Anexos"
            Me.InsertarFolderButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.InsertarFolderButton.UseVisualStyleBackColor = True
            '
            'InsertarDocumentoButton
            '
            Me.InsertarDocumentoButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.document_add2
            Me.InsertarDocumentoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.InsertarDocumentoButton.Location = New System.Drawing.Point(377, 393)
            Me.InsertarDocumentoButton.Name = "InsertarDocumentoButton"
            Me.InsertarDocumentoButton.Size = New System.Drawing.Size(130, 30)
            Me.InsertarDocumentoButton.TabIndex = 4
            Me.InsertarDocumentoButton.Text = "Agregar &Folios"
            Me.InsertarDocumentoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.InsertarDocumentoButton.UseVisualStyleBackColor = True
            '
            'DocumentosDataGridView
            '
            Me.DocumentosDataGridView.AllowUserToAddRows = False
            Me.DocumentosDataGridView.AllowUserToDeleteRows = False
            Me.DocumentosDataGridView.AutoGenerateColumns = False
            Me.DocumentosDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.DocumentosDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DocumentosDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
            Me.DocumentosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DocumentosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdTipologiaDataGridViewTextBoxColumn1, Me.NombreTipologiaDataGridViewTextBoxColumn1, Me.FoliosFileDataGridViewTextBoxColumn1, Me.MontoFileDataGridViewTextBoxColumn1, Me.CBarrasFileDataGridViewTextBoxColumn1})
            Me.DocumentosDataGridView.DataSource = Me.CTAFileDataTableBindingSource
            DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DocumentosDataGridView.DefaultCellStyle = DataGridViewCellStyle5
            Me.DocumentosDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.DocumentosDataGridView.Location = New System.Drawing.Point(13, 110)
            Me.DocumentosDataGridView.MultiSelect = False
            Me.DocumentosDataGridView.Name = "DocumentosDataGridView"
            Me.DocumentosDataGridView.ReadOnly = True
            DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DocumentosDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
            Me.DocumentosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DocumentosDataGridView.Size = New System.Drawing.Size(599, 272)
            Me.DocumentosDataGridView.TabIndex = 2
            '
            'IdTipologiaDataGridViewTextBoxColumn1
            '
            Me.IdTipologiaDataGridViewTextBoxColumn1.DataPropertyName = "id_Tipologia"
            Me.IdTipologiaDataGridViewTextBoxColumn1.HeaderText = ""
            Me.IdTipologiaDataGridViewTextBoxColumn1.Name = "IdTipologiaDataGridViewTextBoxColumn1"
            Me.IdTipologiaDataGridViewTextBoxColumn1.ReadOnly = True
            Me.IdTipologiaDataGridViewTextBoxColumn1.Width = 19
            '
            'NombreTipologiaDataGridViewTextBoxColumn1
            '
            Me.NombreTipologiaDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.NombreTipologiaDataGridViewTextBoxColumn1.DataPropertyName = "Nombre_Tipologia"
            Me.NombreTipologiaDataGridViewTextBoxColumn1.HeaderText = "Tipología"
            Me.NombreTipologiaDataGridViewTextBoxColumn1.Name = "NombreTipologiaDataGridViewTextBoxColumn1"
            Me.NombreTipologiaDataGridViewTextBoxColumn1.ReadOnly = True
            '
            'FoliosFileDataGridViewTextBoxColumn1
            '
            Me.FoliosFileDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.FoliosFileDataGridViewTextBoxColumn1.DataPropertyName = "Folios_File"
            Me.FoliosFileDataGridViewTextBoxColumn1.HeaderText = "Folios"
            Me.FoliosFileDataGridViewTextBoxColumn1.Name = "FoliosFileDataGridViewTextBoxColumn1"
            Me.FoliosFileDataGridViewTextBoxColumn1.ReadOnly = True
            Me.FoliosFileDataGridViewTextBoxColumn1.Width = 64
            '
            'MontoFileDataGridViewTextBoxColumn1
            '
            Me.MontoFileDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.MontoFileDataGridViewTextBoxColumn1.DataPropertyName = "Monto_File"
            Me.MontoFileDataGridViewTextBoxColumn1.HeaderText = "Monto"
            Me.MontoFileDataGridViewTextBoxColumn1.Name = "MontoFileDataGridViewTextBoxColumn1"
            Me.MontoFileDataGridViewTextBoxColumn1.ReadOnly = True
            Me.MontoFileDataGridViewTextBoxColumn1.Width = 68
            '
            'CBarrasFileDataGridViewTextBoxColumn1
            '
            Me.CBarrasFileDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.CBarrasFileDataGridViewTextBoxColumn1.DataPropertyName = "CBarras_File"
            Me.CBarrasFileDataGridViewTextBoxColumn1.HeaderText = "CBarras"
            Me.CBarrasFileDataGridViewTextBoxColumn1.Name = "CBarrasFileDataGridViewTextBoxColumn1"
            Me.CBarrasFileDataGridViewTextBoxColumn1.ReadOnly = True
            Me.CBarrasFileDataGridViewTextBoxColumn1.Width = 76
            '
            'CTAFileDataTableBindingSource
            '
            Me.CTAFileDataTableBindingSource.DataSource = GetType(DBCore.SchemaProcess.CTA_FileDataTable)
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(513, 393)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(99, 30)
            Me.CerrarButton.TabIndex = 6
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'DataGridViewTextBoxColumn1
            '
            Me.DataGridViewTextBoxColumn1.DataPropertyName = "Valor_Llave"
            Me.DataGridViewTextBoxColumn1.HeaderText = "Valor_Llave"
            Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
            Me.DataGridViewTextBoxColumn1.ReadOnly = True
            Me.DataGridViewTextBoxColumn1.Width = 200
            '
            'DataGridViewTextBoxColumn2
            '
            Me.DataGridViewTextBoxColumn2.DataPropertyName = "Valor_Llave"
            Me.DataGridViewTextBoxColumn2.HeaderText = "Valor_Llave"
            Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
            Me.DataGridViewTextBoxColumn2.Width = 5
            '
            'IdProyectoLlaveDataGridViewTextBoxColumn
            '
            Me.IdProyectoLlaveDataGridViewTextBoxColumn.DataPropertyName = "id_Proyecto_Llave"
            Me.IdProyectoLlaveDataGridViewTextBoxColumn.HeaderText = "id_Proyecto_Llave"
            Me.IdProyectoLlaveDataGridViewTextBoxColumn.Name = "IdProyectoLlaveDataGridViewTextBoxColumn"
            Me.IdProyectoLlaveDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdProyectoLlaveDataGridViewTextBoxColumn.Width = 5
            '
            'NombreProyectoLlaveDataGridViewTextBoxColumn
            '
            Me.NombreProyectoLlaveDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.NombreProyectoLlaveDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Proyecto_Llave"
            Me.NombreProyectoLlaveDataGridViewTextBoxColumn.HeaderText = "Nombre_Proyecto_Llave"
            Me.NombreProyectoLlaveDataGridViewTextBoxColumn.Name = "NombreProyectoLlaveDataGridViewTextBoxColumn"
            Me.NombreProyectoLlaveDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreProyectoLlaveDataGridViewTextBoxColumn.Width = 5
            '
            'ValorLlaveDataGridViewTextBoxColumn
            '
            Me.ValorLlaveDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.ValorLlaveDataGridViewTextBoxColumn.DataPropertyName = "Valor_Llave"
            Me.ValorLlaveDataGridViewTextBoxColumn.HeaderText = "Valor_Llave"
            Me.ValorLlaveDataGridViewTextBoxColumn.Name = "ValorLlaveDataGridViewTextBoxColumn"
            Me.ValorLlaveDataGridViewTextBoxColumn.ReadOnly = True
            '
            'FormInserciones
            '
            Me.AcceptButton = Me.InsertarDocumentoButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(629, 435)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.DocumentosDataGridView)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.InsertarDocumentoButton)
            Me.Controls.Add(Me.InsertarFolderButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormInserciones"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Inserciones"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            CType(Me.KeyDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CTAExpedienteLLaveDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DocumentosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CTAFileDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents DocumentosDataGridView As DesktopDataGridViewControl
        Friend WithEvents CBarrasLabel As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents KeyDataGridView As DesktopDataGridViewControl
        Friend WithEvents CTAExpedienteLLaveDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents InsertarDocumentoButton As System.Windows.Forms.Button
        Friend WithEvents CTAFileDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents InsertarFolderButton As System.Windows.Forms.Button
        Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents IdTipologiaDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreTipologiaDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FoliosFileDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents MontoFileDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarrasFileDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdProyectoLlaveDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreProyectoLlaveDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ValorLlaveDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace