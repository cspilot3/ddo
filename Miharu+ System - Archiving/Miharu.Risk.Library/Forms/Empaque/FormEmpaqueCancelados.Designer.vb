Imports Miharu.Desktop.Controls.DesktopDataGridView

Namespace Forms.Empaque
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEmpaqueCancelados
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEmpaqueCancelados))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CajaLabel = New System.Windows.Forms.Label()
            Me.CarpetasDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.CBarras_Folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.FoliosLabel = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.DocumentosLabel = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.CarpetasLabel = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.AgregarDocumentoButton = New System.Windows.Forms.Button()
            Me.AgregarCarpetaButton = New System.Windows.Forms.Button()
            Me.CerrarCajaButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.SacarcarpetaButton = New System.Windows.Forms.Button()
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            CType(Me.CarpetasDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.GroupBox3.SuspendLayout()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(6, 23)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(41, 16)
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "Caja:"
            '
            'CajaLabel
            '
            Me.CajaLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CajaLabel.AutoSize = True
            Me.CajaLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CajaLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.CajaLabel.Location = New System.Drawing.Point(53, 20)
            Me.CajaLabel.Name = "CajaLabel"
            Me.CajaLabel.Size = New System.Drawing.Size(19, 19)
            Me.CajaLabel.TabIndex = 2
            Me.CajaLabel.Text = "_"
            '
            'CarpetasDesktopDataGridView
            '
            Me.CarpetasDesktopDataGridView.AllowUserToAddRows = False
            Me.CarpetasDesktopDataGridView.AllowUserToDeleteRows = False
            Me.CarpetasDesktopDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CarpetasDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.CarpetasDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CarpetasDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.CarpetasDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CarpetasDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CBarras_Folder})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.CarpetasDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.CarpetasDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.CarpetasDesktopDataGridView.Location = New System.Drawing.Point(16, 20)
            Me.CarpetasDesktopDataGridView.MultiSelect = False
            Me.CarpetasDesktopDataGridView.Name = "CarpetasDesktopDataGridView"
            Me.CarpetasDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CarpetasDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.CarpetasDesktopDataGridView.RowHeadersVisible = False
            Me.CarpetasDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.CarpetasDesktopDataGridView.Size = New System.Drawing.Size(220, 436)
            Me.CarpetasDesktopDataGridView.TabIndex = 3
            '
            'CBarras_Folder
            '
            Me.CBarras_Folder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.CBarras_Folder.DataPropertyName = "CBarras_Folder"
            Me.CBarras_Folder.HeaderText = "Codigo Carpeta"
            Me.CBarras_Folder.Name = "CBarras_Folder"
            Me.CBarras_Folder.ReadOnly = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.CarpetasDesktopDataGridView)
            Me.GroupBox1.Location = New System.Drawing.Point(239, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(254, 473)
            Me.GroupBox1.TabIndex = 4
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Carpetas"
            '
            'GroupBox2
            '
            Me.GroupBox2.Controls.Add(Me.FoliosLabel)
            Me.GroupBox2.Controls.Add(Me.Label5)
            Me.GroupBox2.Controls.Add(Me.DocumentosLabel)
            Me.GroupBox2.Controls.Add(Me.Label3)
            Me.GroupBox2.Controls.Add(Me.CarpetasLabel)
            Me.GroupBox2.Controls.Add(Me.Label2)
            Me.GroupBox2.Controls.Add(Me.Label1)
            Me.GroupBox2.Controls.Add(Me.CajaLabel)
            Me.GroupBox2.Location = New System.Drawing.Point(16, 12)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(207, 179)
            Me.GroupBox2.TabIndex = 5
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Caja custodia"
            '
            'FoliosLabel
            '
            Me.FoliosLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FoliosLabel.AutoSize = True
            Me.FoliosLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FoliosLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.FoliosLabel.Location = New System.Drawing.Point(112, 106)
            Me.FoliosLabel.Name = "FoliosLabel"
            Me.FoliosLabel.Size = New System.Drawing.Size(19, 19)
            Me.FoliosLabel.TabIndex = 8
            Me.FoliosLabel.Text = "0"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label5.Location = New System.Drawing.Point(6, 109)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(101, 16)
            Me.Label5.TabIndex = 7
            Me.Label5.Text = "Número Folios:"
            '
            'DocumentosLabel
            '
            Me.DocumentosLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DocumentosLabel.AutoSize = True
            Me.DocumentosLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DocumentosLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.DocumentosLabel.Location = New System.Drawing.Point(158, 80)
            Me.DocumentosLabel.Name = "DocumentosLabel"
            Me.DocumentosLabel.Size = New System.Drawing.Size(19, 19)
            Me.DocumentosLabel.TabIndex = 6
            Me.DocumentosLabel.Text = "0"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(6, 83)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(146, 16)
            Me.Label3.TabIndex = 5
            Me.Label3.Text = "Número Documentos:"
            '
            'CarpetasLabel
            '
            Me.CarpetasLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CarpetasLabel.AutoSize = True
            Me.CarpetasLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CarpetasLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.CarpetasLabel.Location = New System.Drawing.Point(137, 55)
            Me.CarpetasLabel.Name = "CarpetasLabel"
            Me.CarpetasLabel.Size = New System.Drawing.Size(19, 19)
            Me.CarpetasLabel.TabIndex = 4
            Me.CarpetasLabel.Text = "0"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(6, 58)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(125, 16)
            Me.Label2.TabIndex = 3
            Me.Label2.Text = "Número Carpetas:"
            '
            'AgregarDocumentoButton
            '
            Me.AgregarDocumentoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AgregarDocumentoButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.AgregarDocumentoButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.brnCargar
            Me.AgregarDocumentoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AgregarDocumentoButton.Location = New System.Drawing.Point(31, 72)
            Me.AgregarDocumentoButton.Name = "AgregarDocumentoButton"
            Me.AgregarDocumentoButton.Size = New System.Drawing.Size(149, 30)
            Me.AgregarDocumentoButton.TabIndex = 3
            Me.AgregarDocumentoButton.Text = "&Agregar Documento"
            Me.AgregarDocumentoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AgregarDocumentoButton.UseVisualStyleBackColor = True
            '
            'AgregarCarpetaButton
            '
            Me.AgregarCarpetaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AgregarCarpetaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.AgregarCarpetaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnAgregar
            Me.AgregarCarpetaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AgregarCarpetaButton.Location = New System.Drawing.Point(31, 36)
            Me.AgregarCarpetaButton.Name = "AgregarCarpetaButton"
            Me.AgregarCarpetaButton.Size = New System.Drawing.Size(149, 30)
            Me.AgregarCarpetaButton.TabIndex = 0
            Me.AgregarCarpetaButton.Text = "&Agregar Carpeta"
            Me.AgregarCarpetaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AgregarCarpetaButton.UseVisualStyleBackColor = True
            '
            'CerrarCajaButton
            '
            Me.CerrarCajaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarCajaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CerrarCajaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Cancelar
            Me.CerrarCajaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarCajaButton.Location = New System.Drawing.Point(31, 144)
            Me.CerrarCajaButton.Name = "CerrarCajaButton"
            Me.CerrarCajaButton.Size = New System.Drawing.Size(149, 33)
            Me.CerrarCajaButton.TabIndex = 1
            Me.CerrarCajaButton.Text = "&Cerrar Caja"
            Me.CerrarCajaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarCajaButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(31, 183)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(149, 32)
            Me.CerrarButton.TabIndex = 2
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'SacarcarpetaButton
            '
            Me.SacarcarpetaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SacarcarpetaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.SacarcarpetaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir1
            Me.SacarcarpetaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.SacarcarpetaButton.Location = New System.Drawing.Point(31, 108)
            Me.SacarcarpetaButton.Name = "SacarcarpetaButton"
            Me.SacarcarpetaButton.Size = New System.Drawing.Size(149, 30)
            Me.SacarcarpetaButton.TabIndex = 6
            Me.SacarcarpetaButton.Text = "&Sacar Carpeta"
            Me.SacarcarpetaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.SacarcarpetaButton.UseVisualStyleBackColor = True
            '
            'GroupBox3
            '
            Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.GroupBox3.Controls.Add(Me.AgregarCarpetaButton)
            Me.GroupBox3.Controls.Add(Me.CerrarButton)
            Me.GroupBox3.Controls.Add(Me.AgregarDocumentoButton)
            Me.GroupBox3.Controls.Add(Me.SacarcarpetaButton)
            Me.GroupBox3.Controls.Add(Me.CerrarCajaButton)
            Me.GroupBox3.Location = New System.Drawing.Point(16, 235)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Size = New System.Drawing.Size(207, 250)
            Me.GroupBox3.TabIndex = 7
            Me.GroupBox3.TabStop = False
            Me.GroupBox3.Text = "Acciones"
            '
            'FormEmpaqueCancelados
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(505, 503)
            Me.Controls.Add(Me.GroupBox3)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.GroupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "FormEmpaqueCancelados"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Empaque Cancelados"
            CType(Me.CarpetasDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.GroupBox3.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CajaLabel As System.Windows.Forms.Label
        Friend WithEvents CarpetasDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents CerrarCajaButton As System.Windows.Forms.Button
        Friend WithEvents AgregarCarpetaButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents AgregarDocumentoButton As System.Windows.Forms.Button
        Friend WithEvents SacarcarpetaButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents CarpetasLabel As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents CBarras_Folder As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FoliosLabel As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents DocumentosLabel As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
    End Class
End Namespace