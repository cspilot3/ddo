Namespace Imaging.Configuracion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormConfigDocumento
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
            Me.DocumentosDataGridView = New System.Windows.Forms.DataGridView()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.TBLConfigDocumentoDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.IdDocumentoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreDocumentoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkDocumentoCoreDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ActualizarButton = New System.Windows.Forms.Button()
            CType(Me.DocumentosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.TBLConfigDocumentoDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'DocumentosDataGridView
            '
            Me.DocumentosDataGridView.AllowUserToAddRows = False
            Me.DocumentosDataGridView.AllowUserToDeleteRows = False
            Me.DocumentosDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DocumentosDataGridView.AutoGenerateColumns = False
            Me.DocumentosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DocumentosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdDocumentoDataGridViewTextBoxColumn, Me.NombreDocumentoDataGridViewTextBoxColumn, Me.FkDocumentoCoreDataGridViewTextBoxColumn})
            Me.DocumentosDataGridView.DataSource = Me.TBLConfigDocumentoDataTableBindingSource
            Me.DocumentosDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            Me.DocumentosDataGridView.Location = New System.Drawing.Point(12, 12)
            Me.DocumentosDataGridView.Name = "DocumentosDataGridView"
            Me.DocumentosDataGridView.RowHeadersWidth = 20
            Me.DocumentosDataGridView.Size = New System.Drawing.Size(441, 353)
            Me.DocumentosDataGridView.TabIndex = 38
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Ripley.Plugin.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(267, 371)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(90, 30)
            Me.GuardarButton.TabIndex = 36
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
            Me.CerrarButton.Location = New System.Drawing.Point(363, 371)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 37
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'TBLConfigDocumentoDataTableBindingSource
            '
            Me.TBLConfigDocumentoDataTableBindingSource.DataSource = GetType(DBIntegration.SchemaRipley.TBL_Config_DocumentoDataTable)
            '
            'IdDocumentoDataGridViewTextBoxColumn
            '
            Me.IdDocumentoDataGridViewTextBoxColumn.DataPropertyName = "id_Documento"
            Me.IdDocumentoDataGridViewTextBoxColumn.HeaderText = "Id Documento"
            Me.IdDocumentoDataGridViewTextBoxColumn.Name = "IdDocumentoDataGridViewTextBoxColumn"
            '
            'NombreDocumentoDataGridViewTextBoxColumn
            '
            Me.NombreDocumentoDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Documento"
            Me.NombreDocumentoDataGridViewTextBoxColumn.HeaderText = "Nombre Documento"
            Me.NombreDocumentoDataGridViewTextBoxColumn.Name = "NombreDocumentoDataGridViewTextBoxColumn"
            Me.NombreDocumentoDataGridViewTextBoxColumn.Width = 200
            '
            'FkDocumentoCoreDataGridViewTextBoxColumn
            '
            Me.FkDocumentoCoreDataGridViewTextBoxColumn.DataPropertyName = "fk_Documento_Core"
            Me.FkDocumentoCoreDataGridViewTextBoxColumn.HeaderText = "Id Documento Core"
            Me.FkDocumentoCoreDataGridViewTextBoxColumn.Name = "FkDocumentoCoreDataGridViewTextBoxColumn"
            '
            'ActualizarButton
            '
            Me.ActualizarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ActualizarButton.Image = Global.Ripley.Plugin.My.Resources.Resources.refresh
            Me.ActualizarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ActualizarButton.Location = New System.Drawing.Point(12, 371)
            Me.ActualizarButton.Name = "ActualizarButton"
            Me.ActualizarButton.Size = New System.Drawing.Size(90, 30)
            Me.ActualizarButton.TabIndex = 39
            Me.ActualizarButton.Text = "&Actualizar"
            Me.ActualizarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ActualizarButton.UseVisualStyleBackColor = True
            '
            'FormConfigDocumento
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(465, 413)
            Me.Controls.Add(Me.ActualizarButton)
            Me.Controls.Add(Me.DocumentosDataGridView)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Name = "FormConfigDocumento"
            Me.Text = "Configuracion de Documentos"
            CType(Me.DocumentosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.TBLConfigDocumentoDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents DocumentosDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents TBLConfigDocumentoDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents IdDocumentoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreDocumentoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkDocumentoCoreDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ActualizarButton As System.Windows.Forms.Button
    End Class
End Namespace