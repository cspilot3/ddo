Namespace Imaging.Configuracion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormConfigCampo
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
            Me.CamposDataGridView = New System.Windows.Forms.DataGridView()
            Me.fk_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Columna = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Activo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Es_Busqueda = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.FkDocumentoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ActivoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.EsBusquedaDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.CTACampoParametrizacionDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CTACampoParametrizacionDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox1.SuspendLayout()
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
            'CamposDataGridView
            '
            Me.CamposDataGridView.AllowUserToAddRows = False
            Me.CamposDataGridView.AllowUserToDeleteRows = False
            Me.CamposDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CamposDataGridView.AutoGenerateColumns = False
            Me.CamposDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CamposDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fk_Documento, Me.id_Campo, Me.Nombre_Campo, Me.Columna, Me.Activo, Me.Es_Busqueda, Me.FkDocumentoDataGridViewTextBoxColumn, Me.IdCampoDataGridViewTextBoxColumn, Me.NombreCampoDataGridViewTextBoxColumn, Me.ColumnaDataGridViewTextBoxColumn, Me.ActivoDataGridViewCheckBoxColumn, Me.EsBusquedaDataGridViewCheckBoxColumn})
            Me.CamposDataGridView.DataSource = Me.CTACampoParametrizacionDataTableBindingSource
            Me.CamposDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            Me.CamposDataGridView.Location = New System.Drawing.Point(12, 67)
            Me.CamposDataGridView.Name = "CamposDataGridView"
            Me.CamposDataGridView.RowHeadersWidth = 20
            Me.CamposDataGridView.Size = New System.Drawing.Size(448, 273)
            Me.CamposDataGridView.TabIndex = 34
            '
            'fk_Documento
            '
            Me.fk_Documento.DataPropertyName = "fk_Documento"
            Me.fk_Documento.HeaderText = "fk_Documento"
            Me.fk_Documento.Name = "fk_Documento"
            '
            'id_Campo
            '
            Me.id_Campo.DataPropertyName = "id_Campo"
            Me.id_Campo.HeaderText = "id_Campo"
            Me.id_Campo.Name = "id_Campo"
            '
            'Nombre_Campo
            '
            Me.Nombre_Campo.DataPropertyName = "Nombre_Campo"
            Me.Nombre_Campo.HeaderText = "Nombre_Campo"
            Me.Nombre_Campo.Name = "Nombre_Campo"
            '
            'Columna
            '
            Me.Columna.DataPropertyName = "Columna"
            Me.Columna.HeaderText = "Columna"
            Me.Columna.Name = "Columna"
            '
            'Activo
            '
            Me.Activo.DataPropertyName = "Activo"
            Me.Activo.HeaderText = "Activo"
            Me.Activo.Name = "Activo"
            '
            'Es_Busqueda
            '
            Me.Es_Busqueda.DataPropertyName = "Es_Busqueda"
            Me.Es_Busqueda.HeaderText = "Es_Busqueda"
            Me.Es_Busqueda.Name = "Es_Busqueda"
            '
            'FkDocumentoDataGridViewTextBoxColumn
            '
            Me.FkDocumentoDataGridViewTextBoxColumn.DataPropertyName = "fk_Documento"
            Me.FkDocumentoDataGridViewTextBoxColumn.HeaderText = "fk_Documento"
            Me.FkDocumentoDataGridViewTextBoxColumn.Name = "FkDocumentoDataGridViewTextBoxColumn"
            '
            'IdCampoDataGridViewTextBoxColumn
            '
            Me.IdCampoDataGridViewTextBoxColumn.DataPropertyName = "id_Campo"
            Me.IdCampoDataGridViewTextBoxColumn.HeaderText = "id_Campo"
            Me.IdCampoDataGridViewTextBoxColumn.Name = "IdCampoDataGridViewTextBoxColumn"
            '
            'NombreCampoDataGridViewTextBoxColumn
            '
            Me.NombreCampoDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Campo"
            Me.NombreCampoDataGridViewTextBoxColumn.HeaderText = "Nombre_Campo"
            Me.NombreCampoDataGridViewTextBoxColumn.Name = "NombreCampoDataGridViewTextBoxColumn"
            '
            'ColumnaDataGridViewTextBoxColumn
            '
            Me.ColumnaDataGridViewTextBoxColumn.DataPropertyName = "Columna"
            Me.ColumnaDataGridViewTextBoxColumn.HeaderText = "Columna"
            Me.ColumnaDataGridViewTextBoxColumn.Name = "ColumnaDataGridViewTextBoxColumn"
            '
            'ActivoDataGridViewCheckBoxColumn
            '
            Me.ActivoDataGridViewCheckBoxColumn.DataPropertyName = "Activo"
            Me.ActivoDataGridViewCheckBoxColumn.HeaderText = "Activo"
            Me.ActivoDataGridViewCheckBoxColumn.Name = "ActivoDataGridViewCheckBoxColumn"
            '
            'EsBusquedaDataGridViewCheckBoxColumn
            '
            Me.EsBusquedaDataGridViewCheckBoxColumn.DataPropertyName = "Es_Busqueda"
            Me.EsBusquedaDataGridViewCheckBoxColumn.HeaderText = "Es_Busqueda"
            Me.EsBusquedaDataGridViewCheckBoxColumn.Name = "EsBusquedaDataGridViewCheckBoxColumn"
            '
            'CTACampoParametrizacionDataTableBindingSource
            '
            Me.CTACampoParametrizacionDataTableBindingSource.DataSource = GetType(DBIntegration.SchemaRipley.TBL_Config_CampoDataTable)
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.id_DocumentoDesktopComboBox)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 9)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(448, 52)
            Me.GroupBox1.TabIndex = 31
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
            Me.GuardarButton.Location = New System.Drawing.Point(274, 346)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(90, 30)
            Me.GuardarButton.TabIndex = 32
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Ripley.Plugin.My.Resources.Resources.cancel
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(370, 346)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 33
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormConfigCampo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(472, 388)
            Me.Controls.Add(Me.CamposDataGridView)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Name = "FormConfigCampo"
            Me.Text = "Configuracion  de Campos"
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CTACampoParametrizacionDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents id_DocumentoDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents CamposDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents CTACampoParametrizacionDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents fk_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Columna As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Activo As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Es_Busqueda As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents FkDocumentoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ActivoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents EsBusquedaDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace