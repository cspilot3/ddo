Namespace Imaging.Configuracion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormConfigValidacion
        Inherits System.Windows.Forms.Form

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
            Me.components = New System.ComponentModel.Container()
            Me.id_DocumentoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.ValidacionesDataGridView = New System.Windows.Forms.DataGridView()
            Me.CTAValidacionParametrizacionDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Validacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Validacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Activo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.fk_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdValidacionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreValidacionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ActivoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.GroupBox1.SuspendLayout()
            CType(Me.ValidacionesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CTAValidacionParametrizacionDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'id_DocumentoDesktopComboBox
            '
            Me.id_DocumentoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.id_DocumentoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.id_DocumentoDesktopComboBox.DisabledEnter = False
            Me.id_DocumentoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.id_DocumentoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.id_DocumentoDesktopComboBox.FormattingEnabled = True
            Me.id_DocumentoDesktopComboBox.Location = New System.Drawing.Point(89, 19)
            Me.id_DocumentoDesktopComboBox.Name = "id_DocumentoDesktopComboBox"
            Me.id_DocumentoDesktopComboBox.Size = New System.Drawing.Size(337, 21)
            Me.id_DocumentoDesktopComboBox.TabIndex = 0
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.id_DocumentoDesktopComboBox)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(448, 52)
            Me.GroupBox1.TabIndex = 35
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Campo"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(11, 22)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(62, 13)
            Me.Label1.TabIndex = 12
            Me.Label1.Text = "Documento"
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Ripley.Plugin.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(512, 362)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(90, 30)
            Me.GuardarButton.TabIndex = 36
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'ValidacionesDataGridView
            '
            Me.ValidacionesDataGridView.AllowUserToAddRows = False
            Me.ValidacionesDataGridView.AllowUserToDeleteRows = False
            Me.ValidacionesDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ValidacionesDataGridView.AutoGenerateColumns = False
            Me.ValidacionesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ValidacionesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.id_Validacion, Me.Nombre_Validacion, Me.Activo, Me.fk_Documento, Me.IdValidacionDataGridViewTextBoxColumn, Me.NombreValidacionDataGridViewTextBoxColumn, Me.ActivoDataGridViewCheckBoxColumn})
            Me.ValidacionesDataGridView.DataSource = Me.CTAValidacionParametrizacionDataTableBindingSource
            Me.ValidacionesDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            Me.ValidacionesDataGridView.Location = New System.Drawing.Point(12, 70)
            Me.ValidacionesDataGridView.Name = "ValidacionesDataGridView"
            Me.ValidacionesDataGridView.RowHeadersWidth = 20
            Me.ValidacionesDataGridView.Size = New System.Drawing.Size(686, 286)
            Me.ValidacionesDataGridView.TabIndex = 38
            '
            'CTAValidacionParametrizacionDataTableBindingSource
            '
            Me.CTAValidacionParametrizacionDataTableBindingSource.DataSource = GetType(DBIntegration.SchemaRipley.TBL_Config_ValidacionDataTable)
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Ripley.Plugin.My.Resources.Resources.cancel
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(608, 362)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 37
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Column1
            '
            Me.Column1.DataPropertyName = "fk_Documento"
            Me.Column1.HeaderText = "Column1"
            Me.Column1.Name = "Column1"
            '
            'id_Validacion
            '
            Me.id_Validacion.DataPropertyName = "id_Validacion"
            Me.id_Validacion.HeaderText = "id_Validacion"
            Me.id_Validacion.Name = "id_Validacion"
            '
            'Nombre_Validacion
            '
            Me.Nombre_Validacion.DataPropertyName = "Nombre_Validacion"
            Me.Nombre_Validacion.HeaderText = "Nombre_Validacion"
            Me.Nombre_Validacion.Name = "Nombre_Validacion"
            '
            'Activo
            '
            Me.Activo.DataPropertyName = "Activo"
            Me.Activo.HeaderText = "Activo"
            Me.Activo.Name = "Activo"
            '
            'fk_Documento
            '
            Me.fk_Documento.DataPropertyName = "fk_Documento"
            Me.fk_Documento.HeaderText = "fk_Documento"
            Me.fk_Documento.Name = "fk_Documento"
            '
            'IdValidacionDataGridViewTextBoxColumn
            '
            Me.IdValidacionDataGridViewTextBoxColumn.DataPropertyName = "id_Validacion"
            Me.IdValidacionDataGridViewTextBoxColumn.HeaderText = "id_Validacion"
            Me.IdValidacionDataGridViewTextBoxColumn.Name = "IdValidacionDataGridViewTextBoxColumn"
            '
            'NombreValidacionDataGridViewTextBoxColumn
            '
            Me.NombreValidacionDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Validacion"
            Me.NombreValidacionDataGridViewTextBoxColumn.HeaderText = "Nombre_Validacion"
            Me.NombreValidacionDataGridViewTextBoxColumn.Name = "NombreValidacionDataGridViewTextBoxColumn"
            '
            'ActivoDataGridViewCheckBoxColumn
            '
            Me.ActivoDataGridViewCheckBoxColumn.DataPropertyName = "Activo"
            Me.ActivoDataGridViewCheckBoxColumn.HeaderText = "Activo"
            Me.ActivoDataGridViewCheckBoxColumn.Name = "ActivoDataGridViewCheckBoxColumn"
            '
            'FormConfigValidacion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(710, 404)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.ValidacionesDataGridView)
            Me.Controls.Add(Me.CerrarButton)
            Me.Name = "FormConfigValidacion"
            Me.Text = "FormConfigValidacion"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.ValidacionesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CTAValidacionParametrizacionDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents id_DocumentoDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents CTAValidacionParametrizacionDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents ValidacionesDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Validacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Validacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Activo As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents fk_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdValidacionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreValidacionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ActivoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace