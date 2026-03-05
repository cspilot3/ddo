Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.MesaControlFisicos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormPistolearCarpeta
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPistolearCarpeta))
            Me.Label2 = New System.Windows.Forms.Label()
            Me.cbarrasDesktopCBarrasControl = New Miharu.Desktop.Controls.DesktopCBarras.DesktopCBarrasControl()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.LlavesDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.nombre_proyecto_llave = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Valor_llave = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.SiguienteButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.LineaProcesoLabel = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GroupBox1.SuspendLayout()
            CType(Me.LlavesDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(9, 45)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(105, 13)
            Me.Label2.TabIndex = 1
            Me.Label2.Text = "Código de barras:"
            '
            'cbarrasDesktopCBarrasControl
            '
            Me.cbarrasDesktopCBarrasControl.FocusIn = System.Drawing.Color.LightYellow
            Me.cbarrasDesktopCBarrasControl.FocusOut = System.Drawing.Color.White
            Me.cbarrasDesktopCBarrasControl.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cbarrasDesktopCBarrasControl.Location = New System.Drawing.Point(12, 61)
            Me.cbarrasDesktopCBarrasControl.MaxLength = 0
            Me.cbarrasDesktopCBarrasControl.Name = "cbarrasDesktopCBarrasControl"
            Me.cbarrasDesktopCBarrasControl.Size = New System.Drawing.Size(333, 20)
            Me.cbarrasDesktopCBarrasControl.TabIndex = 0
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.LlavesDesktopDataGridView)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 98)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(333, 154)
            Me.GroupBox1.TabIndex = 3
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Llaves"
            '
            'LlavesDesktopDataGridView
            '
            Me.LlavesDesktopDataGridView.AllowUserToAddRows = False
            Me.LlavesDesktopDataGridView.AllowUserToDeleteRows = False
            Me.LlavesDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.LlavesDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.LlavesDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.LlavesDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.LlavesDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.nombre_proyecto_llave, Me.Valor_llave})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.LlavesDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.LlavesDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.LlavesDesktopDataGridView.Location = New System.Drawing.Point(7, 21)
            Me.LlavesDesktopDataGridView.MultiSelect = False
            Me.LlavesDesktopDataGridView.Name = "LlavesDesktopDataGridView"
            Me.LlavesDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.LlavesDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.LlavesDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.LlavesDesktopDataGridView.Size = New System.Drawing.Size(320, 127)
            Me.LlavesDesktopDataGridView.TabIndex = 0
            '
            'nombre_proyecto_llave
            '
            Me.nombre_proyecto_llave.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.nombre_proyecto_llave.DataPropertyName = "nombre_proyecto_llave"
            Me.nombre_proyecto_llave.HeaderText = "Nombre Llave"
            Me.nombre_proyecto_llave.Name = "nombre_proyecto_llave"
            Me.nombre_proyecto_llave.ReadOnly = True
            '
            'Valor_llave
            '
            Me.Valor_llave.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Valor_llave.DataPropertyName = "Valor_llave"
            Me.Valor_llave.HeaderText = "Valor"
            Me.Valor_llave.Name = "Valor_llave"
            Me.Valor_llave.ReadOnly = True
            '
            'SiguienteButton
            '
            Me.SiguienteButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SiguienteButton.Enabled = False
            Me.SiguienteButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSigiente
            Me.SiguienteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.SiguienteButton.Location = New System.Drawing.Point(125, 258)
            Me.SiguienteButton.Name = "SiguienteButton"
            Me.SiguienteButton.Size = New System.Drawing.Size(107, 30)
            Me.SiguienteButton.TabIndex = 1
            Me.SiguienteButton.Text = "&Siguiente"
            Me.SiguienteButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(238, 258)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(107, 30)
            Me.CerrarButton.TabIndex = 15
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'LineaProcesoLabel
            '
            Me.LineaProcesoLabel.AutoSize = True
            Me.LineaProcesoLabel.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LineaProcesoLabel.ForeColor = System.Drawing.Color.Red
            Me.LineaProcesoLabel.Location = New System.Drawing.Point(242, 9)
            Me.LineaProcesoLabel.Name = "LineaProcesoLabel"
            Me.LineaProcesoLabel.Size = New System.Drawing.Size(103, 25)
            Me.LineaProcesoLabel.TabIndex = 17
            Me.LineaProcesoLabel.Text = "0000000"
            Me.LineaProcesoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(117, 14)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(114, 14)
            Me.Label1.TabIndex = 16
            Me.Label1.Text = "Línea de Proceso:"
            '
            'FormPistolearCarpeta
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(355, 294)
            Me.Controls.Add(Me.LineaProcesoLabel)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.SiguienteButton)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.cbarrasDesktopCBarrasControl)
            Me.Controls.Add(Me.Label2)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormPistolearCarpeta"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Mesa de control físicos"
            Me.GroupBox1.ResumeLayout(False)
            CType(Me.LlavesDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents cbarrasDesktopCBarrasControl As DesktopCbarrasControl
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents SiguienteButton As System.Windows.Forms.Button
        Friend WithEvents LlavesDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents nombre_proyecto_llave As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Valor_llave As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents LineaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace