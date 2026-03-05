Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.MesaControlFisicos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormMesaControlFisicos
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMesaControlFisicos))
            Me.Label2 = New System.Windows.Forms.Label()
            Me.cbarrasDesktopCBarrasControl = New Miharu.Desktop.Controls.DesktopCBarras.DesktopCBarrasControl()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.EsquemaLabel = New System.Windows.Forms.Label()
            Me.ProyectoLabel = New System.Windows.Forms.Label()
            Me.EntidadLabel = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label11 = New System.Windows.Forms.Label()
            Me.FilesDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.CBarras = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Tipologia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.nombre_estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Registro_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ProcesarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CBarrasFolderLabel = New System.Windows.Forms.Label()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.CerrarCarpetaButton = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            CType(Me.FilesDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(19, 64)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(108, 26)
            Me.Label2.TabIndex = 3
            Me.Label2.Text = "Códigos de barras" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "de documento"
            '
            'cbarrasDesktopCBarrasControl
            '
            Me.cbarrasDesktopCBarrasControl.FocusIn = System.Drawing.Color.LightYellow
            Me.cbarrasDesktopCBarrasControl.FocusOut = System.Drawing.Color.White
            Me.cbarrasDesktopCBarrasControl.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cbarrasDesktopCBarrasControl.Location = New System.Drawing.Point(133, 69)
            Me.cbarrasDesktopCBarrasControl.MaxLength = 0
            Me.cbarrasDesktopCBarrasControl.Name = "cbarrasDesktopCBarrasControl"
            Me.cbarrasDesktopCBarrasControl.ShortcutsEnabled = False
            Me.cbarrasDesktopCBarrasControl.Size = New System.Drawing.Size(202, 20)
            Me.cbarrasDesktopCBarrasControl.TabIndex = 0
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.EsquemaLabel)
            Me.GroupBox1.Controls.Add(Me.ProyectoLabel)
            Me.GroupBox1.Controls.Add(Me.EntidadLabel)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.Label5)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Location = New System.Drawing.Point(432, 27)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(248, 93)
            Me.GroupBox1.TabIndex = 5
            Me.GroupBox1.TabStop = False
            '
            'EsquemaLabel
            '
            Me.EsquemaLabel.AutoSize = True
            Me.EsquemaLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EsquemaLabel.Location = New System.Drawing.Point(88, 65)
            Me.EsquemaLabel.Name = "EsquemaLabel"
            Me.EsquemaLabel.Size = New System.Drawing.Size(13, 13)
            Me.EsquemaLabel.TabIndex = 12
            Me.EsquemaLabel.Text = "_"
            '
            'ProyectoLabel
            '
            Me.ProyectoLabel.AutoSize = True
            Me.ProyectoLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ProyectoLabel.Location = New System.Drawing.Point(88, 41)
            Me.ProyectoLabel.Name = "ProyectoLabel"
            Me.ProyectoLabel.Size = New System.Drawing.Size(13, 13)
            Me.ProyectoLabel.TabIndex = 11
            Me.ProyectoLabel.Text = "_"
            '
            'EntidadLabel
            '
            Me.EntidadLabel.AutoSize = True
            Me.EntidadLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EntidadLabel.Location = New System.Drawing.Point(88, 17)
            Me.EntidadLabel.Name = "EntidadLabel"
            Me.EntidadLabel.Size = New System.Drawing.Size(13, 13)
            Me.EntidadLabel.TabIndex = 10
            Me.EntidadLabel.Text = "_"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(15, 17)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(49, 13)
            Me.Label3.TabIndex = 6
            Me.Label3.Text = "Cliente:"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(15, 65)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(61, 13)
            Me.Label5.TabIndex = 8
            Me.Label5.Text = "Esquema:"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(15, 41)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(61, 13)
            Me.Label4.TabIndex = 7
            Me.Label4.Text = "Proyecto:"
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label11.Location = New System.Drawing.Point(18, 119)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(109, 19)
            Me.Label11.TabIndex = 6
            Me.Label11.Text = "Documentos"
            '
            'FilesDesktopDataGridView
            '
            Me.FilesDesktopDataGridView.AllowUserToAddRows = False
            Me.FilesDesktopDataGridView.AllowUserToDeleteRows = False
            Me.FilesDesktopDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FilesDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.FilesDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.FilesDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.FilesDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.FilesDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CBarras, Me.Tipologia, Me.Tipo, Me.nombre_estado, Me.id_estado, Me.fk_Registro_Tipo})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.FilesDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.FilesDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.FilesDesktopDataGridView.Location = New System.Drawing.Point(22, 141)
            Me.FilesDesktopDataGridView.MultiSelect = False
            Me.FilesDesktopDataGridView.Name = "FilesDesktopDataGridView"
            Me.FilesDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.FilesDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.FilesDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.FilesDesktopDataGridView.Size = New System.Drawing.Size(658, 389)
            Me.FilesDesktopDataGridView.TabIndex = 7
            '
            'CBarras
            '
            Me.CBarras.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.CBarras.DataPropertyName = "CBarras_file"
            Me.CBarras.HeaderText = "Código de barras"
            Me.CBarras.Name = "CBarras"
            Me.CBarras.ReadOnly = True
            '
            'Tipologia
            '
            Me.Tipologia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Tipologia.DataPropertyName = "nombre_tipologia"
            Me.Tipologia.HeaderText = "Tipología"
            Me.Tipologia.Name = "Tipologia"
            Me.Tipologia.ReadOnly = True
            '
            'Tipo
            '
            Me.Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Tipo.DataPropertyName = "Tipo"
            Me.Tipo.HeaderText = "Tipo"
            Me.Tipo.Name = "Tipo"
            Me.Tipo.ReadOnly = True
            '
            'nombre_estado
            '
            Me.nombre_estado.DataPropertyName = "nombre_estado"
            Me.nombre_estado.HeaderText = "Estado"
            Me.nombre_estado.Name = "nombre_estado"
            Me.nombre_estado.ReadOnly = True
            Me.nombre_estado.Width = 70
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
            'fk_Registro_Tipo
            '
            Me.fk_Registro_Tipo.DataPropertyName = "fk_Registro_Tipo"
            Me.fk_Registro_Tipo.HeaderText = "fk_Registro_Tipo"
            Me.fk_Registro_Tipo.Name = "fk_Registro_Tipo"
            Me.fk_Registro_Tipo.ReadOnly = True
            Me.fk_Registro_Tipo.Visible = False
            Me.fk_Registro_Tipo.Width = 129
            '
            'ProcesarButton
            '
            Me.ProcesarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnCapturarData
            Me.ProcesarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ProcesarButton.Location = New System.Drawing.Point(341, 65)
            Me.ProcesarButton.Name = "ProcesarButton"
            Me.ProcesarButton.Size = New System.Drawing.Size(85, 26)
            Me.ProcesarButton.TabIndex = 1
            Me.ProcesarButton.Text = "&Procesar"
            Me.ProcesarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ProcesarButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(18, 27)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(199, 19)
            Me.Label1.TabIndex = 9
            Me.Label1.Text = "Código Barras Carpeta:"
            '
            'CBarrasFolderLabel
            '
            Me.CBarrasFolderLabel.AutoSize = True
            Me.CBarrasFolderLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CBarrasFolderLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.CBarrasFolderLabel.Location = New System.Drawing.Point(228, 28)
            Me.CBarrasFolderLabel.Name = "CBarrasFolderLabel"
            Me.CBarrasFolderLabel.Size = New System.Drawing.Size(19, 19)
            Me.CBarrasFolderLabel.TabIndex = 10
            Me.CBarrasFolderLabel.Text = "_"
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(602, 536)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(78, 30)
            Me.CerrarButton.TabIndex = 11
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'CerrarCarpetaButton
            '
            Me.CerrarCarpetaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarCarpetaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnFolderGo
            Me.CerrarCarpetaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarCarpetaButton.Location = New System.Drawing.Point(471, 536)
            Me.CerrarCarpetaButton.Name = "CerrarCarpetaButton"
            Me.CerrarCarpetaButton.Size = New System.Drawing.Size(116, 30)
            Me.CerrarCarpetaButton.TabIndex = 12
            Me.CerrarCarpetaButton.Text = "C&errar Carpeta"
            Me.CerrarCarpetaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarCarpetaButton.UseVisualStyleBackColor = True
            '
            'FormMesaControlFisicos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(692, 573)
            Me.Controls.Add(Me.CerrarCarpetaButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.CBarrasFolderLabel)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.ProcesarButton)
            Me.Controls.Add(Me.FilesDesktopDataGridView)
            Me.Controls.Add(Me.Label11)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.cbarrasDesktopCBarrasControl)
            Me.Controls.Add(Me.Label2)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormMesaControlFisicos"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Mesa de Control Fisicos"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.FilesDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents cbarrasDesktopCBarrasControl As DesktopCbarrasControl
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents EsquemaLabel As System.Windows.Forms.Label
        Friend WithEvents ProyectoLabel As System.Windows.Forms.Label
        Friend WithEvents EntidadLabel As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label11 As System.Windows.Forms.Label
        Friend WithEvents FilesDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents ProcesarButton As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CBarrasFolderLabel As System.Windows.Forms.Label
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarCarpetaButton As System.Windows.Forms.Button
        Friend WithEvents CBarras As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Tipologia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents nombre_estado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_estado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Registro_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace