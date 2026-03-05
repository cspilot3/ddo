Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Parametrizacion.Solicitudes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Formtipoxrol
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Formtipoxrol))
            Me.rolesDesktopComboBox = New DesktopComboBoxControl()
            Me.lblEntidad = New System.Windows.Forms.Label()
            Me.DocumentosDesktopDataGridView = New DesktopDataGridViewControl()
            Me.guardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.TipoDesktopComboBox = New DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.EntidadDesktopComboBox = New DesktopComboBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.ProyectoDesktopComboBox = New DesktopComboBoxControl()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.EsquemaDesktopComboBox = New DesktopComboBoxControl()
            Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Aplica = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            CType(Me.DocumentosDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
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
            Me.rolesDesktopComboBox.Location = New System.Drawing.Point(125, 21)
            Me.rolesDesktopComboBox.Name = "rolesDesktopComboBox"
            Me.rolesDesktopComboBox.Size = New System.Drawing.Size(228, 21)
            Me.rolesDesktopComboBox.TabIndex = 0
            '
            'lblEntidad
            '
            Me.lblEntidad.AutoSize = True
            Me.lblEntidad.Location = New System.Drawing.Point(28, 21)
            Me.lblEntidad.Name = "lblEntidad"
            Me.lblEntidad.Size = New System.Drawing.Size(25, 13)
            Me.lblEntidad.TabIndex = 2
            Me.lblEntidad.Text = "Rol"
            '
            'DocumentosDesktopDataGridView
            '
            Me.DocumentosDesktopDataGridView.AllowUserToAddRows = False
            Me.DocumentosDesktopDataGridView.AllowUserToDeleteRows = False
            Me.DocumentosDesktopDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                              Or System.Windows.Forms.AnchorStyles.Left) _
                                                             Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DocumentosDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.DocumentosDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DocumentosDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.DocumentosDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DocumentosDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.Documento, Me.Aplica})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DocumentosDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.DocumentosDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.DocumentosDesktopDataGridView.Location = New System.Drawing.Point(15, 189)
            Me.DocumentosDesktopDataGridView.MultiSelect = False
            Me.DocumentosDesktopDataGridView.Name = "DocumentosDesktopDataGridView"
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DocumentosDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.DocumentosDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DocumentosDesktopDataGridView.Size = New System.Drawing.Size(346, 264)
            Me.DocumentosDesktopDataGridView.TabIndex = 3
            '
            'guardarButton
            '
            Me.guardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.guardarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSave
            Me.guardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.guardarButton.Location = New System.Drawing.Point(93, 473)
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
            Me.CerrarButton.Location = New System.Drawing.Point(190, 473)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(91, 30)
            Me.CerrarButton.TabIndex = 5
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(28, 54)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(82, 13)
            Me.Label1.TabIndex = 7
            Me.Label1.Text = "Tipo Solicitud"
            '
            'TipoDesktopComboBox
            '
            Me.TipoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipoDesktopComboBox.DisabledEnter = False
            Me.TipoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipoDesktopComboBox.FormattingEnabled = True
            Me.TipoDesktopComboBox.Location = New System.Drawing.Point(125, 51)
            Me.TipoDesktopComboBox.Name = "TipoDesktopComboBox"
            Me.TipoDesktopComboBox.Size = New System.Drawing.Size(228, 21)
            Me.TipoDesktopComboBox.TabIndex = 6
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(28, 86)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(49, 13)
            Me.Label2.TabIndex = 9
            Me.Label2.Text = "Entidad"
            '
            'EntidadDesktopComboBox
            '
            Me.EntidadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadDesktopComboBox.DisabledEnter = False
            Me.EntidadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadDesktopComboBox.FormattingEnabled = True
            Me.EntidadDesktopComboBox.Location = New System.Drawing.Point(125, 83)
            Me.EntidadDesktopComboBox.Name = "EntidadDesktopComboBox"
            Me.EntidadDesktopComboBox.Size = New System.Drawing.Size(228, 21)
            Me.EntidadDesktopComboBox.TabIndex = 8
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(28, 120)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(58, 13)
            Me.Label3.TabIndex = 11
            Me.Label3.Text = "Proyecto"
            '
            'ProyectoDesktopComboBox
            '
            Me.ProyectoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProyectoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProyectoDesktopComboBox.DisabledEnter = False
            Me.ProyectoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProyectoDesktopComboBox.FormattingEnabled = True
            Me.ProyectoDesktopComboBox.Location = New System.Drawing.Point(125, 117)
            Me.ProyectoDesktopComboBox.Name = "ProyectoDesktopComboBox"
            Me.ProyectoDesktopComboBox.Size = New System.Drawing.Size(228, 21)
            Me.ProyectoDesktopComboBox.TabIndex = 10
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(28, 153)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(58, 13)
            Me.Label4.TabIndex = 13
            Me.Label4.Text = "Esquema"
            '
            'EsquemaDesktopComboBox
            '
            Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaDesktopComboBox.DisabledEnter = False
            Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaDesktopComboBox.FormattingEnabled = True
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(125, 150)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(228, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 12
            '
            'ID
            '
            Me.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.ID.DataPropertyName = "id_Documento"
            Me.ID.HeaderText = "ID"
            Me.ID.Name = "ID"
            Me.ID.ReadOnly = True
            Me.ID.Width = 45
            '
            'Documento
            '
            Me.Documento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Documento.DataPropertyName = "Nombre_Documento"
            Me.Documento.HeaderText = "Documento"
            Me.Documento.Name = "Documento"
            Me.Documento.ReadOnly = True
            '
            'Aplica
            '
            Me.Aplica.DataPropertyName = "Aplica"
            Me.Aplica.HeaderText = "Aplica"
            Me.Aplica.Name = "Aplica"
            Me.Aplica.Width = 47
            '
            'Formtipoxrol
            '
            Me.AcceptButton = Me.guardarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(374, 515)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.ProyectoDesktopComboBox)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.EntidadDesktopComboBox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.TipoDesktopComboBox)
            Me.Controls.Add(Me.guardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.DocumentosDesktopDataGridView)
            Me.Controls.Add(Me.lblEntidad)
            Me.Controls.Add(Me.rolesDesktopComboBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "Formtipoxrol"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Tipo de solicitud por ROL"
            CType(Me.DocumentosDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents rolesDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents lblEntidad As System.Windows.Forms.Label
        Friend WithEvents DocumentosDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents guardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents TipoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents EntidadDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents ProyectoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents EsquemaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Aplica As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace