Namespace Forms.Empaque
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEmpaqueCaja
        Inherits System.Windows.Forms.Form

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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEmpaqueCaja))
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            Me.AgregarCarpetaButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.CerrarCajaButton = New System.Windows.Forms.Button()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.CarpetasLabel = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CajaLabel = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.CarpetasDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.CBarras_Folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.GroupBox3.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            CType(Me.CarpetasDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GroupBox3
            '
            Me.GroupBox3.Controls.Add(Me.AgregarCarpetaButton)
            Me.GroupBox3.Controls.Add(Me.CerrarButton)
            Me.GroupBox3.Controls.Add(Me.CerrarCajaButton)
            Me.GroupBox3.Location = New System.Drawing.Point(12, 119)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Size = New System.Drawing.Size(207, 167)
            Me.GroupBox3.TabIndex = 10
            Me.GroupBox3.TabStop = False
            Me.GroupBox3.Text = "Acciones"
            '
            'AgregarCarpetaButton
            '
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
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(31, 110)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(149, 32)
            Me.CerrarButton.TabIndex = 2
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'CerrarCajaButton
            '
            Me.CerrarCajaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CerrarCajaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Cancelar
            Me.CerrarCajaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarCajaButton.Location = New System.Drawing.Point(31, 71)
            Me.CerrarCajaButton.Name = "CerrarCajaButton"
            Me.CerrarCajaButton.Size = New System.Drawing.Size(149, 33)
            Me.CerrarCajaButton.TabIndex = 1
            Me.CerrarCajaButton.Text = "&Cerrar Caja"
            Me.CerrarCajaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarCajaButton.UseVisualStyleBackColor = True
            '
            'GroupBox2
            '
            Me.GroupBox2.Controls.Add(Me.CarpetasLabel)
            Me.GroupBox2.Controls.Add(Me.Label2)
            Me.GroupBox2.Controls.Add(Me.Label1)
            Me.GroupBox2.Controls.Add(Me.CajaLabel)
            Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(207, 100)
            Me.GroupBox2.TabIndex = 9
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Caja custodia"
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
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.CarpetasDesktopDataGridView)
            Me.GroupBox1.Location = New System.Drawing.Point(235, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(275, 275)
            Me.GroupBox1.TabIndex = 8
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Carpetas"
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
            Me.CarpetasDesktopDataGridView.Size = New System.Drawing.Size(241, 238)
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
            'FormEmpaqueCaja
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(522, 299)
            Me.Controls.Add(Me.GroupBox3)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.GroupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormEmpaqueCaja"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Empaque Caja Nueva"
            Me.GroupBox3.ResumeLayout(False)
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.GroupBox1.ResumeLayout(False)
            CType(Me.CarpetasDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents AgregarCarpetaButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarCajaButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents CarpetasLabel As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CajaLabel As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents CarpetasDesktopDataGridView As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents CBarras_Folder As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace