Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Parametrizacion.Solicitudes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormMotivoXRol
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMotivoXRol))
            Me.rolesDesktopComboBox = New DesktopComboBoxControl()
            Me.lblEntidad = New System.Windows.Forms.Label()
            Me.MotivosDesktopDataGridView = New DesktopDataGridViewControl()
            Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Motivo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Aplica = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.guardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            CType(Me.MotivosDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'rolesDesktopComboBox
            '
            Me.rolesDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.rolesDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.rolesDesktopComboBox.DisabledEnter = False
            Me.rolesDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.rolesDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.rolesDesktopComboBox.FormattingEnabled = True
            Me.rolesDesktopComboBox.Location = New System.Drawing.Point(59, 25)
            Me.rolesDesktopComboBox.Name = "rolesDesktopComboBox"
            Me.rolesDesktopComboBox.Size = New System.Drawing.Size(309, 21)
            Me.rolesDesktopComboBox.TabIndex = 0
            '
            'lblEntidad
            '
            Me.lblEntidad.AutoSize = True
            Me.lblEntidad.Location = New System.Drawing.Point(28, 28)
            Me.lblEntidad.Name = "lblEntidad"
            Me.lblEntidad.Size = New System.Drawing.Size(25, 13)
            Me.lblEntidad.TabIndex = 2
            Me.lblEntidad.Text = "Rol"
            '
            'MotivosDesktopDataGridView
            '
            Me.MotivosDesktopDataGridView.AllowUserToAddRows = False
            Me.MotivosDesktopDataGridView.AllowUserToDeleteRows = False
            Me.MotivosDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.MotivosDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.MotivosDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.MotivosDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.MotivosDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.Motivo, Me.Aplica})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.MotivosDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.MotivosDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.MotivosDesktopDataGridView.Location = New System.Drawing.Point(15, 62)
            Me.MotivosDesktopDataGridView.MultiSelect = False
            Me.MotivosDesktopDataGridView.Name = "MotivosDesktopDataGridView"
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.MotivosDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.MotivosDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.MotivosDesktopDataGridView.Size = New System.Drawing.Size(367, 402)
            Me.MotivosDesktopDataGridView.TabIndex = 3
            '
            'ID
            '
            Me.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.ID.DataPropertyName = "id_Solicitud_Motivo"
            Me.ID.HeaderText = "ID"
            Me.ID.Name = "ID"
            Me.ID.ReadOnly = True
            Me.ID.Width = 45
            '
            'Motivo
            '
            Me.Motivo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Motivo.DataPropertyName = "Nombre_solicitud_Motivo"
            Me.Motivo.HeaderText = "Motivo"
            Me.Motivo.Name = "Motivo"
            Me.Motivo.ReadOnly = True
            '
            'Aplica
            '
            Me.Aplica.DataPropertyName = "Aplica"
            Me.Aplica.HeaderText = "Aplica"
            Me.Aplica.Name = "Aplica"
            Me.Aplica.Width = 47
            '
            'guardarButton
            '
            Me.guardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.guardarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSave
            Me.guardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.guardarButton.Location = New System.Drawing.Point(101, 483)
            Me.guardarButton.Name = "guardarButton"
            Me.guardarButton.Size = New System.Drawing.Size(89, 30)
            Me.guardarButton.TabIndex = 4
            Me.guardarButton.Text = "&Guardar"
            Me.guardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.guardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(198, 483)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(91, 30)
            Me.CerrarButton.TabIndex = 5
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormMotivoXRol
            '
            Me.AcceptButton = Me.guardarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(394, 525)
            Me.Controls.Add(Me.guardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.MotivosDesktopDataGridView)
            Me.Controls.Add(Me.lblEntidad)
            Me.Controls.Add(Me.rolesDesktopComboBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormMotivoXRol"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Motivos de solicitud por ROL"
            CType(Me.MotivosDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents rolesDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents lblEntidad As System.Windows.Forms.Label
        Friend WithEvents MotivosDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents guardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Motivo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Aplica As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace