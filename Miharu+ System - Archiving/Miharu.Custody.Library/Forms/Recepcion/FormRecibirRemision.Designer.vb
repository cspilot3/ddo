Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.Recepcion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormRecibirRemision
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRecibirRemision))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.CajasDesktopDataGridView = New DesktopDataGridViewControl()
            Me.Recibido = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.id_Caja = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Cantidad_Folders = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CodigoCaja = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CodigoCajaDesktopTextBox = New DesktopCBarrasControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.AgregarCajaButton = New System.Windows.Forms.Button()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            CType(Me.CajasDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.CajasDesktopDataGridView)
            Me.GroupBox1.Location = New System.Drawing.Point(13, 36)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(351, 254)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Cajas Remisión"
            '
            'CajasDesktopDataGridView
            '
            Me.CajasDesktopDataGridView.AllowUserToAddRows = False
            Me.CajasDesktopDataGridView.AllowUserToDeleteRows = False
            Me.CajasDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.CajasDesktopDataGridView.BackgroundColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CajasDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.CajasDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CajasDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Recibido, Me.id_Caja, Me.Cantidad_Folders, Me.CodigoCaja})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.CajasDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.CajasDesktopDataGridView.Enabled = False
            Me.CajasDesktopDataGridView.EnableHeadersVisualStyles = False
            Me.CajasDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.CajasDesktopDataGridView.Location = New System.Drawing.Point(6, 17)
            Me.CajasDesktopDataGridView.MultiSelect = False
            Me.CajasDesktopDataGridView.Name = "CajasDesktopDataGridView"
            Me.CajasDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CajasDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.CajasDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.CajasDesktopDataGridView.Size = New System.Drawing.Size(339, 231)
            Me.CajasDesktopDataGridView.TabIndex = 0
            '
            'Recibido
            '
            Me.Recibido.HeaderText = "R"
            Me.Recibido.Name = "Recibido"
            Me.Recibido.ReadOnly = True
            Me.Recibido.Width = 21
            '
            'id_Caja
            '
            Me.id_Caja.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
            Me.id_Caja.DataPropertyName = "fk_Caja"
            Me.id_Caja.HeaderText = "Caja"
            Me.id_Caja.Name = "id_Caja"
            Me.id_Caja.ReadOnly = True
            Me.id_Caja.Width = 57
            '
            'Cantidad_Folders
            '
            Me.Cantidad_Folders.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
            Me.Cantidad_Folders.DataPropertyName = "Cantidad_Folders"
            Me.Cantidad_Folders.HeaderText = "Folders"
            Me.Cantidad_Folders.Name = "Cantidad_Folders"
            Me.Cantidad_Folders.ReadOnly = True
            Me.Cantidad_Folders.Width = 73
            '
            'CodigoCaja
            '
            Me.CodigoCaja.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.CodigoCaja.DataPropertyName = "Codigo_Caja"
            Me.CodigoCaja.HeaderText = "Código"
            Me.CodigoCaja.Name = "CodigoCaja"
            Me.CodigoCaja.ReadOnly = True
            '
            'CodigoCajaDesktopTextBox
            '            
            Me.CodigoCajaDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.CodigoCajaDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.CodigoCajaDesktopTextBox.Location = New System.Drawing.Point(104, 9)
            Me.CodigoCajaDesktopTextBox.Name = "CodigoCajaDesktopTextBox"            
            Me.CodigoCajaDesktopTextBox.Size = New System.Drawing.Size(175, 21)
            Me.CodigoCajaDesktopTextBox.TabIndex = 0
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(13, 12)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(76, 13)
            Me.Label1.TabIndex = 2
            Me.Label1.Text = "Código Caja:"
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(285, 296)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(79, 23)
            Me.CerrarButton.TabIndex = 3
            Me.CerrarButton.Text = "&Cancelar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'AgregarCajaButton
            '
            Me.AgregarCajaButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnAgregar
            Me.AgregarCajaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AgregarCajaButton.Location = New System.Drawing.Point(285, 9)
            Me.AgregarCajaButton.Name = "AgregarCajaButton"
            Me.AgregarCajaButton.Size = New System.Drawing.Size(79, 23)
            Me.AgregarCajaButton.TabIndex = 1
            Me.AgregarCajaButton.Text = "&Agregar"
            Me.AgregarCajaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AgregarCajaButton.UseVisualStyleBackColor = True
            '
            'GuardarButton
            '
            Me.GuardarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Aceptar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(200, 296)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(79, 23)
            Me.GuardarButton.TabIndex = 2
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'FormRecibirRemision
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(376, 326)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.AgregarCajaButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.CodigoCajaDesktopTextBox)
            Me.Controls.Add(Me.GroupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "FormRecibirRemision"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Recibir Remisión"
            Me.GroupBox1.ResumeLayout(False)
            CType(Me.CajasDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents CodigoCajaDesktopTextBox As DesktopCbarrasControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CajasDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents AgregarCajaButton As System.Windows.Forms.Button
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents Recibido As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents id_Caja As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Cantidad_Folders As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CodigoCaja As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace