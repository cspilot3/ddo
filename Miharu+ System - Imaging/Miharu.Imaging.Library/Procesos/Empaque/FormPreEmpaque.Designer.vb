Namespace Procesos.Empaque
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormPreEmpaque
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
            Me.FechaProcesoComboBox = New System.Windows.Forms.ComboBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.OTComboBox = New System.Windows.Forms.ComboBox()
            Me.EmpaqueDataGridView = New System.Windows.Forms.DataGridView()
            Me.CrearOTButton = New System.Windows.Forms.Button()
            Me.AbrirOTButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.Precinto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Cerrado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Nombres = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Empaque = New System.Windows.Forms.DataGridViewTextBoxColumn()
            CType(Me.EmpaqueDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
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
            'EmpaqueDataGridView
            '
            Me.EmpaqueDataGridView.AllowUserToAddRows = False
            Me.EmpaqueDataGridView.AllowUserToDeleteRows = False
            Me.EmpaqueDataGridView.AllowUserToOrderColumns = True
            Me.EmpaqueDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.EmpaqueDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Precinto, Me.Fecha, Me.Cerrado, Me.Nombres, Me.id_Empaque})
            Me.EmpaqueDataGridView.Location = New System.Drawing.Point(15, 97)
            Me.EmpaqueDataGridView.MultiSelect = False
            Me.EmpaqueDataGridView.Name = "EmpaqueDataGridView"
            Me.EmpaqueDataGridView.ReadOnly = True
            Me.EmpaqueDataGridView.RowHeadersWidth = 10
            Me.EmpaqueDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.EmpaqueDataGridView.Size = New System.Drawing.Size(485, 253)
            Me.EmpaqueDataGridView.TabIndex = 25
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
            Me.CrearOTButton.Size = New System.Drawing.Size(145, 33)
            Me.CrearOTButton.TabIndex = 26
            Me.CrearOTButton.Text = "Nueva..."
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
            Me.AbrirOTButton.Location = New System.Drawing.Point(166, 356)
            Me.AbrirOTButton.Name = "AbrirOTButton"
            Me.AbrirOTButton.Size = New System.Drawing.Size(134, 33)
            Me.AbrirOTButton.TabIndex = 27
            Me.AbrirOTButton.Text = "Seleccionar ..."
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
            Me.CerrarButton.Location = New System.Drawing.Point(412, 356)
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
            Me.BuscarButton.Location = New System.Drawing.Point(411, 60)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(89, 31)
            Me.BuscarButton.TabIndex = 29
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
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
            'id_Empaque
            '
            Me.id_Empaque.DataPropertyName = "id_Empaque"
            Me.id_Empaque.HeaderText = "id_Empaque"
            Me.id_Empaque.Name = "id_Empaque"
            Me.id_Empaque.ReadOnly = True
            Me.id_Empaque.Visible = False
            '
            'FormPreEmpaque
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(512, 405)
            Me.Controls.Add(Me.BuscarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.AbrirOTButton)
            Me.Controls.Add(Me.CrearOTButton)
            Me.Controls.Add(Me.EmpaqueDataGridView)
            Me.Controls.Add(Me.OTComboBox)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.FechaProcesoComboBox)
            Me.Controls.Add(Me.Label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormPreEmpaque"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Pre-Empaque"
            CType(Me.EmpaqueDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents FechaProcesoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents OTComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents EmpaqueDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents CrearOTButton As System.Windows.Forms.Button
        Friend WithEvents AbrirOTButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents Precinto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Fecha As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Cerrado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Nombres As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Empaque As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace