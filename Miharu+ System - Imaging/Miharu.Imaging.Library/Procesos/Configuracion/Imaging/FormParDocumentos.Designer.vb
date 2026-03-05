Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Procesos.Configuracion.Imaging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParDocumentos
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormParDocumentos))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.EsquemaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.DocumentosDataGridView = New System.Windows.Forms.DataGridView()
            Me.CTADocumentoDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.FkEntidadDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkProyectoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkEsquemaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdDocumentoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreDocumentoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkTipologiaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreTipologiaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EliminadoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.EsObligatorioDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.EsAnexoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.GeneraCartaRespuesta = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.idCartaRespuesta = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.idCorreoEvidencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.idDocumentoAcuseRecibido = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.GroupBox1.SuspendLayout()
            CType(Me.DocumentosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CTADocumentoDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.Label7)
            Me.GroupBox1.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(921, 57)
            Me.GroupBox1.TabIndex = 26
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Campo"
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(12, 21)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(58, 13)
            Me.Label7.TabIndex = 22
            Me.Label7.Text = "Esquema"
            '
            'EsquemaDesktopComboBox
            '
            Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaDesktopComboBox.DisabledEnter = False
            Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaDesktopComboBox.fk_Campo = 0
            Me.EsquemaDesktopComboBox.fk_Documento = 0
            Me.EsquemaDesktopComboBox.fk_Validacion = 0
            Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaDesktopComboBox.FormattingEnabled = True
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(90, 18)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(338, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 21
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
            Me.DocumentosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FkEntidadDataGridViewTextBoxColumn, Me.FkProyectoDataGridViewTextBoxColumn, Me.FkEsquemaDataGridViewTextBoxColumn, Me.IdDocumentoDataGridViewTextBoxColumn, Me.NombreDocumentoDataGridViewTextBoxColumn, Me.FkTipologiaDataGridViewTextBoxColumn, Me.NombreTipologiaDataGridViewTextBoxColumn, Me.EliminadoDataGridViewCheckBoxColumn, Me.EsObligatorioDataGridViewCheckBoxColumn, Me.EsAnexoDataGridViewCheckBoxColumn, Me.GeneraCartaRespuesta, Me.idCartaRespuesta, Me.idCorreoEvidencia, Me.idDocumentoAcuseRecibido})
            Me.DocumentosDataGridView.DataSource = Me.CTADocumentoDataTableBindingSource
            Me.DocumentosDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            Me.DocumentosDataGridView.Location = New System.Drawing.Point(12, 75)
            Me.DocumentosDataGridView.Name = "DocumentosDataGridView"
            Me.DocumentosDataGridView.RowHeadersWidth = 20
            Me.DocumentosDataGridView.Size = New System.Drawing.Size(921, 273)
            Me.DocumentosDataGridView.TabIndex = 31
            '
            'CTADocumentoDataTableBindingSource
            '
            Me.CTADocumentoDataTableBindingSource.DataSource = GetType(DBImaging.SchemaConfig.CTA_DocumentoDataTable)
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(747, 354)
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
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(843, 354)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 33
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FkEntidadDataGridViewTextBoxColumn
            '
            Me.FkEntidadDataGridViewTextBoxColumn.DataPropertyName = "fk_Entidad"
            Me.FkEntidadDataGridViewTextBoxColumn.HeaderText = "fk_Entidad"
            Me.FkEntidadDataGridViewTextBoxColumn.Name = "FkEntidadDataGridViewTextBoxColumn"
            Me.FkEntidadDataGridViewTextBoxColumn.Visible = False
            '
            'FkProyectoDataGridViewTextBoxColumn
            '
            Me.FkProyectoDataGridViewTextBoxColumn.DataPropertyName = "fk_Proyecto"
            Me.FkProyectoDataGridViewTextBoxColumn.HeaderText = "fk_Proyecto"
            Me.FkProyectoDataGridViewTextBoxColumn.Name = "FkProyectoDataGridViewTextBoxColumn"
            Me.FkProyectoDataGridViewTextBoxColumn.Visible = False
            '
            'FkEsquemaDataGridViewTextBoxColumn
            '
            Me.FkEsquemaDataGridViewTextBoxColumn.DataPropertyName = "fk_Esquema"
            Me.FkEsquemaDataGridViewTextBoxColumn.HeaderText = "fk_Esquema"
            Me.FkEsquemaDataGridViewTextBoxColumn.Name = "FkEsquemaDataGridViewTextBoxColumn"
            Me.FkEsquemaDataGridViewTextBoxColumn.Visible = False
            '
            'IdDocumentoDataGridViewTextBoxColumn
            '
            Me.IdDocumentoDataGridViewTextBoxColumn.DataPropertyName = "id_Documento"
            Me.IdDocumentoDataGridViewTextBoxColumn.HeaderText = "ID"
            Me.IdDocumentoDataGridViewTextBoxColumn.Name = "IdDocumentoDataGridViewTextBoxColumn"
            Me.IdDocumentoDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdDocumentoDataGridViewTextBoxColumn.Width = 40
            '
            'NombreDocumentoDataGridViewTextBoxColumn
            '
            Me.NombreDocumentoDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Documento"
            Me.NombreDocumentoDataGridViewTextBoxColumn.HeaderText = "Documento"
            Me.NombreDocumentoDataGridViewTextBoxColumn.Name = "NombreDocumentoDataGridViewTextBoxColumn"
            Me.NombreDocumentoDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreDocumentoDataGridViewTextBoxColumn.Width = 250
            '
            'FkTipologiaDataGridViewTextBoxColumn
            '
            Me.FkTipologiaDataGridViewTextBoxColumn.DataPropertyName = "fk_Tipologia"
            Me.FkTipologiaDataGridViewTextBoxColumn.HeaderText = "fk_Tipologia"
            Me.FkTipologiaDataGridViewTextBoxColumn.Name = "FkTipologiaDataGridViewTextBoxColumn"
            Me.FkTipologiaDataGridViewTextBoxColumn.Visible = False
            '
            'NombreTipologiaDataGridViewTextBoxColumn
            '
            Me.NombreTipologiaDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Tipologia"
            Me.NombreTipologiaDataGridViewTextBoxColumn.HeaderText = "Tipología"
            Me.NombreTipologiaDataGridViewTextBoxColumn.Name = "NombreTipologiaDataGridViewTextBoxColumn"
            Me.NombreTipologiaDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreTipologiaDataGridViewTextBoxColumn.Width = 250
            '
            'EliminadoDataGridViewCheckBoxColumn
            '
            Me.EliminadoDataGridViewCheckBoxColumn.DataPropertyName = "Eliminado"
            Me.EliminadoDataGridViewCheckBoxColumn.HeaderText = "Eliminado"
            Me.EliminadoDataGridViewCheckBoxColumn.Name = "EliminadoDataGridViewCheckBoxColumn"
            Me.EliminadoDataGridViewCheckBoxColumn.Visible = False
            '
            'EsObligatorioDataGridViewCheckBoxColumn
            '
            Me.EsObligatorioDataGridViewCheckBoxColumn.DataPropertyName = "Es_Obligatorio"
            Me.EsObligatorioDataGridViewCheckBoxColumn.HeaderText = "Obligatorio"
            Me.EsObligatorioDataGridViewCheckBoxColumn.Name = "EsObligatorioDataGridViewCheckBoxColumn"
            Me.EsObligatorioDataGridViewCheckBoxColumn.Width = 80
            '
            'EsAnexoDataGridViewCheckBoxColumn
            '
            Me.EsAnexoDataGridViewCheckBoxColumn.DataPropertyName = "Es_Anexo"
            Me.EsAnexoDataGridViewCheckBoxColumn.HeaderText = "Anexo"
            Me.EsAnexoDataGridViewCheckBoxColumn.Name = "EsAnexoDataGridViewCheckBoxColumn"
            Me.EsAnexoDataGridViewCheckBoxColumn.Width = 80
            '
            'GeneraCartaRespuesta
            '
            Me.GeneraCartaRespuesta.DataPropertyName = "Genera_Carta_Respuesta"
            Me.GeneraCartaRespuesta.HeaderText = "Genera Carta Respuesta"
            Me.GeneraCartaRespuesta.Name = "GeneraCartaRespuesta"
            '
            'idCartaRespuesta
            '
            Me.idCartaRespuesta.DataPropertyName = "id_Documento_Carta_Respuesta"
            Me.idCartaRespuesta.HeaderText = "id Documento Carta Respuesta"
            Me.idCartaRespuesta.Name = "idCartaRespuesta"
            Me.idCartaRespuesta.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.idCartaRespuesta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'idCorreoEvidencia
            '
            Me.idCorreoEvidencia.DataPropertyName = "id_Documento_Correo_Evidencia"
            Me.idCorreoEvidencia.HeaderText = "id Documento Correo Evidencia"
            Me.idCorreoEvidencia.Name = "idCorreoEvidencia"
            Me.idCorreoEvidencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'idDocumentoAcuseRecibido
            '
            Me.idDocumentoAcuseRecibido.DataPropertyName = "id_Documento_Acuse_Recibido"
            Me.idDocumentoAcuseRecibido.HeaderText = "id Documento Acuse Recibido"
            Me.idDocumentoAcuseRecibido.Name = "idDocumentoAcuseRecibido"
            '
            'FormParDocumentos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(950, 396)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.DocumentosDataGridView)
            Me.Controls.Add(Me.GroupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormParDocumentos"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Parametrización de Documentos"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.DocumentosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CTADocumentoDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents EsquemaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents DocumentosDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents CTADocumentoDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents FkEntidadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkProyectoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkEsquemaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdDocumentoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreDocumentoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkTipologiaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreTipologiaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EliminadoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents EsObligatorioDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents EsAnexoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents GeneraCartaRespuesta As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents idCartaRespuesta As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents idCorreoEvidencia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents idDocumentoAcuseRecibido As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace