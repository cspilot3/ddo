Namespace Procesos.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormPreDestape
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
            Me.FechaProcesoComboBox = New System.Windows.Forms.ComboBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.OTComboBox = New System.Windows.Forms.ComboBox()
            Me.PrecintosDataGridView = New System.Windows.Forms.DataGridView()
            Me.Precinto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Cerrado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Nombres = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Precinto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CrearOTButton = New System.Windows.Forms.Button()
            Me.AbrirOTButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.CorrecionDestapeButton = New System.Windows.Forms.Button()
            Me.MasivoDestapeButton = New System.Windows.Forms.Button()
            CType(Me.PrecintosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'FechaProcesoComboBox
            '
            Me.FechaProcesoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.FechaProcesoComboBox.FormattingEnabled = True
            Me.FechaProcesoComboBox.Location = New System.Drawing.Point(15, 26)
            Me.FechaProcesoComboBox.Name = "FechaProcesoComboBox"
            Me.FechaProcesoComboBox.Size = New System.Drawing.Size(390, 21)
            Me.FechaProcesoComboBox.TabIndex = 22
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(12, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(92, 13)
            Me.Label1.TabIndex = 21
            Me.Label1.Text = "Fecha Proceso"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(12, 50)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(24, 13)
            Me.Label2.TabIndex = 23
            Me.Label2.Text = "OT"
            '
            'OTComboBox
            '
            Me.OTComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OTComboBox.FormattingEnabled = True
            Me.OTComboBox.Location = New System.Drawing.Point(15, 66)
            Me.OTComboBox.Name = "OTComboBox"
            Me.OTComboBox.Size = New System.Drawing.Size(390, 21)
            Me.OTComboBox.TabIndex = 24
            '
            'PrecintosDataGridView
            '
            Me.PrecintosDataGridView.AllowUserToAddRows = False
            Me.PrecintosDataGridView.AllowUserToDeleteRows = False
            Me.PrecintosDataGridView.AllowUserToOrderColumns = True
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.PrecintosDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.PrecintosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.PrecintosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Precinto, Me.Fecha, Me.Cerrado, Me.Nombres, Me.id_Precinto})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.PrecintosDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.PrecintosDataGridView.Location = New System.Drawing.Point(15, 97)
            Me.PrecintosDataGridView.MultiSelect = False
            Me.PrecintosDataGridView.Name = "PrecintosDataGridView"
            Me.PrecintosDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.PrecintosDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.PrecintosDataGridView.RowHeadersWidth = 10
            Me.PrecintosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.PrecintosDataGridView.Size = New System.Drawing.Size(516, 253)
            Me.PrecintosDataGridView.TabIndex = 25
            '
            'Precinto
            '
            Me.Precinto.DataPropertyName = "Precinto"
            Me.Precinto.HeaderText = "Precinto"
            Me.Precinto.Name = "Precinto"
            Me.Precinto.ReadOnly = True
            '
            'Fecha
            '
            Me.Fecha.DataPropertyName = "Fecha"
            Me.Fecha.HeaderText = "Fecha"
            Me.Fecha.Name = "Fecha"
            Me.Fecha.ReadOnly = True
            '
            'Cerrado
            '
            Me.Cerrado.DataPropertyName = "Cerrado"
            Me.Cerrado.HeaderText = "Cerrado"
            Me.Cerrado.Name = "Cerrado"
            Me.Cerrado.ReadOnly = True
            '
            'Nombres
            '
            Me.Nombres.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Nombres.DataPropertyName = "Nombres"
            Me.Nombres.HeaderText = "Usuario"
            Me.Nombres.Name = "Nombres"
            Me.Nombres.ReadOnly = True
            '
            'id_Precinto
            '
            Me.id_Precinto.DataPropertyName = "id_Precinto"
            Me.id_Precinto.HeaderText = "id_Precinto"
            Me.id_Precinto.Name = "id_Precinto"
            Me.id_Precinto.ReadOnly = True
            Me.id_Precinto.Visible = False
            '
            'CrearOTButton
            '
            Me.CrearOTButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.CrearOTButton.Enabled = False
            Me.CrearOTButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CrearOTButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.sheet_add
            Me.CrearOTButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CrearOTButton.Location = New System.Drawing.Point(15, 356)
            Me.CrearOTButton.Name = "CrearOTButton"
            Me.CrearOTButton.Size = New System.Drawing.Size(88, 33)
            Me.CrearOTButton.TabIndex = 26
            Me.CrearOTButton.Text = "Nueva"
            Me.CrearOTButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CrearOTButton.UseVisualStyleBackColor = True
            '
            'AbrirOTButton
            '
            Me.AbrirOTButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.AbrirOTButton.Enabled = False
            Me.AbrirOTButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AbrirOTButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.refresh
            Me.AbrirOTButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AbrirOTButton.Location = New System.Drawing.Point(109, 356)
            Me.AbrirOTButton.Name = "AbrirOTButton"
            Me.AbrirOTButton.Size = New System.Drawing.Size(110, 33)
            Me.AbrirOTButton.TabIndex = 27
            Me.AbrirOTButton.Text = "Seleccionar"
            Me.AbrirOTButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AbrirOTButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancelar
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(442, 356)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 33)
            Me.CerrarButton.TabIndex = 28
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'BuscarButton
            '
            Me.BuscarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(441, 60)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(89, 31)
            Me.BuscarButton.TabIndex = 29
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'CorrecionDestapeButton
            '
            Me.CorrecionDestapeButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.CorrecionDestapeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CorrecionDestapeButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CorrecionDestapeButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Edit
            Me.CorrecionDestapeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CorrecionDestapeButton.Location = New System.Drawing.Point(225, 356)
            Me.CorrecionDestapeButton.Name = "CorrecionDestapeButton"
            Me.CorrecionDestapeButton.Size = New System.Drawing.Size(102, 33)
            Me.CorrecionDestapeButton.TabIndex = 30
            Me.CorrecionDestapeButton.Text = "Corrección."
            Me.CorrecionDestapeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CorrecionDestapeButton.UseVisualStyleBackColor = True
            '
            'MasivoDestapeButton
            '
            Me.MasivoDestapeButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.MasivoDestapeButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MasivoDestapeButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.BtnCargar
            Me.MasivoDestapeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.MasivoDestapeButton.Location = New System.Drawing.Point(333, 356)
            Me.MasivoDestapeButton.Name = "MasivoDestapeButton"
            Me.MasivoDestapeButton.Size = New System.Drawing.Size(103, 33)
            Me.MasivoDestapeButton.TabIndex = 31
            Me.MasivoDestapeButton.Text = "Masivo..."
            Me.MasivoDestapeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.MasivoDestapeButton.UseVisualStyleBackColor = True
            '
            'FormPreDestape
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(544, 405)
            Me.Controls.Add(Me.MasivoDestapeButton)
            Me.Controls.Add(Me.CorrecionDestapeButton)
            Me.Controls.Add(Me.BuscarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.AbrirOTButton)
            Me.Controls.Add(Me.CrearOTButton)
            Me.Controls.Add(Me.PrecintosDataGridView)
            Me.Controls.Add(Me.OTComboBox)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.FechaProcesoComboBox)
            Me.Controls.Add(Me.Label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormPreDestape"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Pre-Destape"
            CType(Me.PrecintosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents FechaProcesoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents OTComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents PrecintosDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents CrearOTButton As System.Windows.Forms.Button
        Friend WithEvents AbrirOTButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents Precinto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Fecha As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Cerrado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Nombres As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Precinto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CorrecionDestapeButton As System.Windows.Forms.Button
        Friend WithEvents MasivoDestapeButton As System.Windows.Forms.Button
    End Class
End Namespace