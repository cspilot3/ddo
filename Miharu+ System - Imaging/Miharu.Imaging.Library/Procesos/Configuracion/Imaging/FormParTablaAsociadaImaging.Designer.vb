Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Procesos.Configuracion.Imaging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParTablaAsociadaImaging
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
            Me.CamposDataGridView = New System.Windows.Forms.DataGridView()
            Me.FkEntidadDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkDocumentoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdCampoTablaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.MascaraDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FormatoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EliminadoCampoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.CTATablaAsociadaConfiguracionDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.CamposDesktopComboBox = New DesktopComboBoxControl()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.EsquemaDesktopComboBox = New DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.id_DocumentoDesktopComboBox = New DesktopComboBoxControl()
            Me.CerrarButton = New System.Windows.Forms.Button()
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CTATablaAsociadaConfiguracionDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
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
            Me.CamposDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FkEntidadDataGridViewTextBoxColumn, Me.FkDocumentoDataGridViewTextBoxColumn, Me.FkCampoDataGridViewTextBoxColumn, Me.IdCampoTablaDataGridViewTextBoxColumn, Me.NombreCampoDataGridViewTextBoxColumn, Me.MascaraDataGridViewTextBoxColumn, Me.FormatoDataGridViewTextBoxColumn, Me.EliminadoCampoDataGridViewCheckBoxColumn})
            Me.CamposDataGridView.DataSource = Me.CTATablaAsociadaConfiguracionDataTableBindingSource
            Me.CamposDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            Me.CamposDataGridView.Location = New System.Drawing.Point(12, 125)
            Me.CamposDataGridView.Name = "CamposDataGridView"
            Me.CamposDataGridView.RowHeadersWidth = 20
            Me.CamposDataGridView.Size = New System.Drawing.Size(543, 251)
            Me.CamposDataGridView.TabIndex = 34
            '
            'FkEntidadDataGridViewTextBoxColumn
            '
            Me.FkEntidadDataGridViewTextBoxColumn.DataPropertyName = "fk_Entidad"
            Me.FkEntidadDataGridViewTextBoxColumn.HeaderText = "fk_Entidad"
            Me.FkEntidadDataGridViewTextBoxColumn.Name = "FkEntidadDataGridViewTextBoxColumn"
            Me.FkEntidadDataGridViewTextBoxColumn.Visible = False
            '
            'FkDocumentoDataGridViewTextBoxColumn
            '
            Me.FkDocumentoDataGridViewTextBoxColumn.DataPropertyName = "fk_Documento"
            Me.FkDocumentoDataGridViewTextBoxColumn.HeaderText = "fk_Documento"
            Me.FkDocumentoDataGridViewTextBoxColumn.Name = "FkDocumentoDataGridViewTextBoxColumn"
            Me.FkDocumentoDataGridViewTextBoxColumn.Visible = False
            '
            'FkCampoDataGridViewTextBoxColumn
            '
            Me.FkCampoDataGridViewTextBoxColumn.DataPropertyName = "fk_Campo"
            Me.FkCampoDataGridViewTextBoxColumn.HeaderText = "fk_Campo"
            Me.FkCampoDataGridViewTextBoxColumn.Name = "FkCampoDataGridViewTextBoxColumn"
            Me.FkCampoDataGridViewTextBoxColumn.Visible = False
            '
            'IdCampoTablaDataGridViewTextBoxColumn
            '
            Me.IdCampoTablaDataGridViewTextBoxColumn.DataPropertyName = "id_Campo_Tabla"
            Me.IdCampoTablaDataGridViewTextBoxColumn.HeaderText = "Id Campo Tabla"
            Me.IdCampoTablaDataGridViewTextBoxColumn.Name = "IdCampoTablaDataGridViewTextBoxColumn"
            '
            'NombreCampoDataGridViewTextBoxColumn
            '
            Me.NombreCampoDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Campo"
            Me.NombreCampoDataGridViewTextBoxColumn.HeaderText = "Nombre Campo"
            Me.NombreCampoDataGridViewTextBoxColumn.Name = "NombreCampoDataGridViewTextBoxColumn"
            '
            'MascaraDataGridViewTextBoxColumn
            '
            Me.MascaraDataGridViewTextBoxColumn.DataPropertyName = "Mascara"
            Me.MascaraDataGridViewTextBoxColumn.HeaderText = "Mascara"
            Me.MascaraDataGridViewTextBoxColumn.Name = "MascaraDataGridViewTextBoxColumn"
            '
            'FormatoDataGridViewTextBoxColumn
            '
            Me.FormatoDataGridViewTextBoxColumn.DataPropertyName = "Formato"
            Me.FormatoDataGridViewTextBoxColumn.HeaderText = "Formato"
            Me.FormatoDataGridViewTextBoxColumn.Name = "FormatoDataGridViewTextBoxColumn"
            '
            'EliminadoCampoDataGridViewCheckBoxColumn
            '
            Me.EliminadoCampoDataGridViewCheckBoxColumn.DataPropertyName = "Eliminado_Campo"
            Me.EliminadoCampoDataGridViewCheckBoxColumn.HeaderText = "Eliminado_Campo"
            Me.EliminadoCampoDataGridViewCheckBoxColumn.Name = "EliminadoCampoDataGridViewCheckBoxColumn"
            '
            'CTATablaAsociadaConfiguracionDataTableBindingSource
            '
            Me.CTATablaAsociadaConfiguracionDataTableBindingSource.DataSource = GetType(DBImaging.SchemaConfig.CTA_Tabla_Asociada_ParametrizacionDataTable)
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(369, 382)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(90, 30)
            Me.GuardarButton.TabIndex = 32
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.CamposDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label7)
            Me.GroupBox1.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.id_DocumentoDesktopComboBox)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(442, 107)
            Me.GroupBox1.TabIndex = 31
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Campo"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(12, 75)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(40, 13)
            Me.Label2.TabIndex = 24
            Me.Label2.Text = "Campo"
            '
            'CamposDesktopComboBox
            '
            Me.CamposDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.CamposDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.CamposDesktopComboBox.DisabledEnter = False
            Me.CamposDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CamposDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CamposDesktopComboBox.FormattingEnabled = True
            Me.CamposDesktopComboBox.Location = New System.Drawing.Point(90, 72)
            Me.CamposDesktopComboBox.Name = "CamposDesktopComboBox"
            Me.CamposDesktopComboBox.Size = New System.Drawing.Size(337, 21)
            Me.CamposDesktopComboBox.TabIndex = 23
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(12, 21)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(51, 13)
            Me.Label7.TabIndex = 22
            Me.Label7.Text = "Esquema"
            '
            'EsquemaDesktopComboBox
            '
            Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaDesktopComboBox.DisabledEnter = False
            Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaDesktopComboBox.FormattingEnabled = True
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(90, 18)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(338, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 21
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(12, 48)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(62, 13)
            Me.Label1.TabIndex = 12
            Me.Label1.Text = "Documento"
            '
            'id_DocumentoDesktopComboBox
            '
            Me.id_DocumentoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.id_DocumentoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.id_DocumentoDesktopComboBox.DisabledEnter = False
            Me.id_DocumentoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.id_DocumentoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.id_DocumentoDesktopComboBox.FormattingEnabled = True
            Me.id_DocumentoDesktopComboBox.Location = New System.Drawing.Point(90, 45)
            Me.id_DocumentoDesktopComboBox.Name = "id_DocumentoDesktopComboBox"
            Me.id_DocumentoDesktopComboBox.Size = New System.Drawing.Size(337, 21)
            Me.id_DocumentoDesktopComboBox.TabIndex = 0
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(465, 382)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 33
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormTablaAsociadaImaging
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(567, 424)
            Me.Controls.Add(Me.CamposDataGridView)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.CerrarButton)
            Me.Name = "FormTablaAsociadaImaging"
            Me.Text = "FormTablaAsociadaImaging"
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CTATablaAsociadaConfiguracionDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CamposDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents CamposDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents id_DocumentoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents EsquemaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents FkEntidadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkDocumentoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdCampoTablaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents MascaraDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FormatoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EliminadoCampoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents CTATablaAsociadaConfiguracionDataTableBindingSource As System.Windows.Forms.BindingSource
    End Class
End Namespace