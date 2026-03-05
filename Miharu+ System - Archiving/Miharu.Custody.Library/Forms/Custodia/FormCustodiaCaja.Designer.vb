Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Custodia
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCustodiaCaja
    Inherits Miharu.Desktop.Library.FormBase

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
            Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.ProyectoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.EntidadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.CustodiaCajaDesktopData = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.Seleccionar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.id_Boveda = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Boveda = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Codigo_Caja = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Posicion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Entidad = New System.Windows.Forms.DataGridViewComboBoxColumn()
            Me.Sede = New System.Windows.Forms.DataGridViewComboBoxColumn()
            Me.Boveda = New System.Windows.Forms.DataGridViewComboBoxColumn()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.AsignarGuiaButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GroupBox2.SuspendLayout()
            CType(Me.CustodiaCajaDesktopData, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GroupBox2
            '
            Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox2.Controls.Add(Me.ProyectoDesktopComboBox)
            Me.GroupBox2.Controls.Add(Me.EntidadDesktopComboBox)
            Me.GroupBox2.Controls.Add(Me.Label5)
            Me.GroupBox2.Controls.Add(Me.Label6)
            Me.GroupBox2.Location = New System.Drawing.Point(5, 14)
            Me.GroupBox2.Margin = New System.Windows.Forms.Padding(21, 18, 21, 18)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Padding = New System.Windows.Forms.Padding(12, 10, 12, 10)
            Me.GroupBox2.Size = New System.Drawing.Size(1064, 67)
            Me.GroupBox2.TabIndex = 5
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Parámetros Búsqueda"
            '
            'ProyectoDesktopComboBox
            '
            Me.ProyectoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProyectoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProyectoDesktopComboBox.DisabledEnter = False
            Me.ProyectoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoDesktopComboBox.fk_Campo = 0
            Me.ProyectoDesktopComboBox.fk_Documento = 0
            Me.ProyectoDesktopComboBox.fk_Validacion = 0
            Me.ProyectoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProyectoDesktopComboBox.FormattingEnabled = True
            Me.ProyectoDesktopComboBox.Location = New System.Drawing.Point(440, 21)
            Me.ProyectoDesktopComboBox.Name = "ProyectoDesktopComboBox"
            Me.ProyectoDesktopComboBox.Size = New System.Drawing.Size(250, 21)
            Me.ProyectoDesktopComboBox.TabIndex = 12
            '
            'EntidadDesktopComboBox
            '
            Me.EntidadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadDesktopComboBox.DisabledEnter = False
            Me.EntidadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadDesktopComboBox.fk_Campo = 0
            Me.EntidadDesktopComboBox.fk_Documento = 0
            Me.EntidadDesktopComboBox.fk_Validacion = 0
            Me.EntidadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadDesktopComboBox.FormattingEnabled = True
            Me.EntidadDesktopComboBox.Location = New System.Drawing.Point(74, 21)
            Me.EntidadDesktopComboBox.Name = "EntidadDesktopComboBox"
            Me.EntidadDesktopComboBox.Size = New System.Drawing.Size(250, 21)
            Me.EntidadDesktopComboBox.TabIndex = 9
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(363, 24)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(58, 13)
            Me.Label5.TabIndex = 7
            Me.Label5.Text = "Proyecto"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(19, 24)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(49, 13)
            Me.Label6.TabIndex = 6
            Me.Label6.Text = "Entidad"
            '
            'CustodiaCajaDesktopData
            '
            Me.CustodiaCajaDesktopData.AllowUserToAddRows = False
            Me.CustodiaCajaDesktopData.AllowUserToDeleteRows = False
            Me.CustodiaCajaDesktopData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.CustodiaCajaDesktopData.BackgroundColor = System.Drawing.Color.White
            Me.CustodiaCajaDesktopData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CustodiaCajaDesktopData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
            Me.CustodiaCajaDesktopData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CustodiaCajaDesktopData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Seleccionar, Me.id_Boveda, Me.Nombre_Proyecto, Me.Nombre_Boveda, Me.Codigo_Caja, Me.Posicion, Me.Entidad, Me.Sede, Me.Boveda})
            DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.CustodiaCajaDesktopData.DefaultCellStyle = DataGridViewCellStyle8
            Me.CustodiaCajaDesktopData.GridColor = System.Drawing.SystemColors.Control
            Me.CustodiaCajaDesktopData.Location = New System.Drawing.Point(5, 88)
            Me.CustodiaCajaDesktopData.MultiSelect = False
            Me.CustodiaCajaDesktopData.Name = "CustodiaCajaDesktopData"
            DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CustodiaCajaDesktopData.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
            Me.CustodiaCajaDesktopData.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CustodiaCajaDesktopData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.CustodiaCajaDesktopData.Size = New System.Drawing.Size(1064, 351)
            Me.CustodiaCajaDesktopData.TabIndex = 15
            '
            'Seleccionar
            '
            Me.Seleccionar.HeaderText = "Seleccionar"
            Me.Seleccionar.Name = "Seleccionar"
            Me.Seleccionar.Width = 78
            '
            'id_Boveda
            '
            Me.id_Boveda.DataPropertyName = "id_Boveda"
            Me.id_Boveda.HeaderText = "Id"
            Me.id_Boveda.Name = "id_Boveda"
            Me.id_Boveda.ReadOnly = True
            Me.id_Boveda.Visible = False
            Me.id_Boveda.Width = 44
            '
            'Nombre_Proyecto
            '
            Me.Nombre_Proyecto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Nombre_Proyecto.DataPropertyName = "Nombre_Proyecto"
            Me.Nombre_Proyecto.HeaderText = "Proyecto"
            Me.Nombre_Proyecto.Name = "Nombre_Proyecto"
            Me.Nombre_Proyecto.ReadOnly = True
            Me.Nombre_Proyecto.Width = 130
            '
            'Nombre_Boveda
            '
            Me.Nombre_Boveda.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Nombre_Boveda.DataPropertyName = "Nombre_Boveda"
            Me.Nombre_Boveda.HeaderText = "Boveda"
            Me.Nombre_Boveda.Name = "Nombre_Boveda"
            Me.Nombre_Boveda.ReadOnly = True
            Me.Nombre_Boveda.Width = 130
            '
            'Codigo_Caja
            '
            Me.Codigo_Caja.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Codigo_Caja.DataPropertyName = "Codigo_Caja"
            Me.Codigo_Caja.HeaderText = "Codigo Caja"
            Me.Codigo_Caja.Name = "Codigo_Caja"
            Me.Codigo_Caja.ReadOnly = True
            Me.Codigo_Caja.Width = 130
            '
            'Posicion
            '
            Me.Posicion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Posicion.HeaderText = "Posicion"
            Me.Posicion.Name = "Posicion"
            Me.Posicion.ReadOnly = True
            Me.Posicion.Width = 130
            '
            'Entidad
            '
            Me.Entidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Entidad.HeaderText = "Entidad"
            Me.Entidad.Name = "Entidad"
            Me.Entidad.ReadOnly = True
            Me.Entidad.Width = 130
            '
            'Sede
            '
            Me.Sede.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Sede.HeaderText = "Sede"
            Me.Sede.Name = "Sede"
            Me.Sede.ReadOnly = True
            Me.Sede.Width = 130
            '
            'Boveda
            '
            Me.Boveda.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Boveda.HeaderText = "Boveda"
            Me.Boveda.Name = "Boveda"
            Me.Boveda.ReadOnly = True
            Me.Boveda.Width = 130
            '
            'BuscarButton
            '
            Me.BuscarButton.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(787, 445)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(90, 30)
            Me.BuscarButton.TabIndex = 22
            Me.BuscarButton.Text = "&Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'AsignarGuiaButton
            '
            Me.AsignarGuiaButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnAgregar
            Me.AsignarGuiaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AsignarGuiaButton.Location = New System.Drawing.Point(883, 445)
            Me.AsignarGuiaButton.Name = "AsignarGuiaButton"
            Me.AsignarGuiaButton.Size = New System.Drawing.Size(90, 30)
            Me.AsignarGuiaButton.TabIndex = 20
            Me.AsignarGuiaButton.Text = "&Custodiar"
            Me.AsignarGuiaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AsignarGuiaButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(979, 445)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 21
            Me.CerrarButton.Text = "&Cancelar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormCustodiaCaja
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1081, 487)
            Me.Controls.Add(Me.BuscarButton)
            Me.Controls.Add(Me.AsignarGuiaButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.CustodiaCajaDesktopData)
            Me.Controls.Add(Me.GroupBox2)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCustodiaCaja"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Custodia Cajas"
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            CType(Me.CustodiaCajaDesktopData, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents ProyectoDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents EntidadDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents CustodiaCajaDesktopData As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents AsignarGuiaButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Seleccionar As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents id_Boveda As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Boveda As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Codigo_Caja As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Posicion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Entidad As System.Windows.Forms.DataGridViewComboBoxColumn
        Friend WithEvents Sede As System.Windows.Forms.DataGridViewComboBoxColumn
        Friend WithEvents Boveda As System.Windows.Forms.DataGridViewComboBoxColumn
    End Class
End Namespace