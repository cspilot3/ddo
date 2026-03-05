Imports DBImaging
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Procesos.Configuracion.Imaging

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParValidaciones
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormParValidaciones))
            Me.ValidacionesDataGridView = New System.Windows.Forms.DataGridView()
            Me.FkDocumentoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdValidacionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.PreguntaValidacionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ObligatorioDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.UsaMotivoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.UsaMarcaDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.AsignarMarcaColumn = New System.Windows.Forms.DataGridViewImageColumn()
            Me.MarcaXCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.MarcaYCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.MarcaWidthCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.MarcaHeightCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.OrdenValidacionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Etapa_CapturaColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
            Me.TBL_Etapa_CapturaDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.Nombre_Etapa_CapturaColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Modo_Respuesta_AutomaticaColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
            Me.TBL_Modo_Respuesta_AutomaticaDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.Nombre_Modo_Respuesta_AutomaticaColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ConfigColumn = New System.Windows.Forms.DataGridViewButtonColumn()
            Me.fk_Campo_1Column = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Campo_2Column = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Valor_ComparacionColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Operador_ComparacionColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CTA_Validacion_ConfiguracionDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.EsquemaDesktopComboBox = New DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.id_DocumentoDesktopComboBox = New DesktopComboBoxControl()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            CType(Me.ValidacionesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.TBL_Etapa_CapturaDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.TBL_Modo_Respuesta_AutomaticaDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CTA_Validacion_ConfiguracionDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
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
            Me.ValidacionesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FkDocumentoDataGridViewTextBoxColumn, Me.IdValidacionDataGridViewTextBoxColumn, Me.PreguntaValidacionDataGridViewTextBoxColumn, Me.ObligatorioDataGridViewCheckBoxColumn, Me.UsaMotivoDataGridViewCheckBoxColumn, Me.UsaMarcaDataGridViewCheckBoxColumn, Me.AsignarMarcaColumn, Me.MarcaXCampoDataGridViewTextBoxColumn, Me.MarcaYCampoDataGridViewTextBoxColumn, Me.MarcaWidthCampoDataGridViewTextBoxColumn, Me.MarcaHeightCampoDataGridViewTextBoxColumn, Me.OrdenValidacionDataGridViewTextBoxColumn, Me.fk_Etapa_CapturaColumn, Me.Nombre_Etapa_CapturaColumn, Me.fk_Modo_Respuesta_AutomaticaColumn, Me.Nombre_Modo_Respuesta_AutomaticaColumn, Me.ConfigColumn, Me.fk_Campo_1Column, Me.fk_Campo_2Column, Me.Valor_ComparacionColumn, Me.Operador_ComparacionColumn})
            Me.ValidacionesDataGridView.DataSource = Me.CTA_Validacion_ConfiguracionDataTableBindingSource
            Me.ValidacionesDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            Me.ValidacionesDataGridView.Location = New System.Drawing.Point(8, 102)
            Me.ValidacionesDataGridView.Name = "ValidacionesDataGridView"
            Me.ValidacionesDataGridView.RowHeadersWidth = 20
            Me.ValidacionesDataGridView.Size = New System.Drawing.Size(906, 263)
            Me.ValidacionesDataGridView.TabIndex = 34
            '
            'FkDocumentoDataGridViewTextBoxColumn
            '
            Me.FkDocumentoDataGridViewTextBoxColumn.DataPropertyName = "fk_Documento"
            Me.FkDocumentoDataGridViewTextBoxColumn.HeaderText = "fk_Documento"
            Me.FkDocumentoDataGridViewTextBoxColumn.Name = "FkDocumentoDataGridViewTextBoxColumn"
            Me.FkDocumentoDataGridViewTextBoxColumn.ReadOnly = True
            Me.FkDocumentoDataGridViewTextBoxColumn.Visible = False
            '
            'IdValidacionDataGridViewTextBoxColumn
            '
            Me.IdValidacionDataGridViewTextBoxColumn.DataPropertyName = "id_Validacion"
            Me.IdValidacionDataGridViewTextBoxColumn.HeaderText = "ID"
            Me.IdValidacionDataGridViewTextBoxColumn.Name = "IdValidacionDataGridViewTextBoxColumn"
            Me.IdValidacionDataGridViewTextBoxColumn.Width = 40
            '
            'PreguntaValidacionDataGridViewTextBoxColumn
            '
            Me.PreguntaValidacionDataGridViewTextBoxColumn.DataPropertyName = "Pregunta_Validacion"
            Me.PreguntaValidacionDataGridViewTextBoxColumn.HeaderText = "Validacion"
            Me.PreguntaValidacionDataGridViewTextBoxColumn.Name = "PreguntaValidacionDataGridViewTextBoxColumn"
            Me.PreguntaValidacionDataGridViewTextBoxColumn.ReadOnly = True
            Me.PreguntaValidacionDataGridViewTextBoxColumn.Width = 200
            '
            'ObligatorioDataGridViewCheckBoxColumn
            '
            Me.ObligatorioDataGridViewCheckBoxColumn.DataPropertyName = "Obligatorio"
            Me.ObligatorioDataGridViewCheckBoxColumn.HeaderText = "Obligatorio"
            Me.ObligatorioDataGridViewCheckBoxColumn.Name = "ObligatorioDataGridViewCheckBoxColumn"
            Me.ObligatorioDataGridViewCheckBoxColumn.ReadOnly = True
            Me.ObligatorioDataGridViewCheckBoxColumn.Width = 80
            '
            'UsaMotivoDataGridViewCheckBoxColumn
            '
            Me.UsaMotivoDataGridViewCheckBoxColumn.DataPropertyName = "Usa_Motivo"
            Me.UsaMotivoDataGridViewCheckBoxColumn.HeaderText = "Usa Motivo"
            Me.UsaMotivoDataGridViewCheckBoxColumn.Name = "UsaMotivoDataGridViewCheckBoxColumn"
            Me.UsaMotivoDataGridViewCheckBoxColumn.ReadOnly = True
            Me.UsaMotivoDataGridViewCheckBoxColumn.Width = 80
            '
            'UsaMarcaDataGridViewCheckBoxColumn
            '
            Me.UsaMarcaDataGridViewCheckBoxColumn.DataPropertyName = "Usa_Marca"
            Me.UsaMarcaDataGridViewCheckBoxColumn.HeaderText = "Usa Marca"
            Me.UsaMarcaDataGridViewCheckBoxColumn.Name = "UsaMarcaDataGridViewCheckBoxColumn"
            Me.UsaMarcaDataGridViewCheckBoxColumn.Width = 80
            '
            'AsignarMarcaColumn
            '
            Me.AsignarMarcaColumn.HeaderText = ""
            Me.AsignarMarcaColumn.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGenerarEstructura
            Me.AsignarMarcaColumn.Name = "AsignarMarcaColumn"
            Me.AsignarMarcaColumn.Width = 30
            '
            'MarcaXCampoDataGridViewTextBoxColumn
            '
            Me.MarcaXCampoDataGridViewTextBoxColumn.DataPropertyName = "Marca_X_Campo"
            Me.MarcaXCampoDataGridViewTextBoxColumn.HeaderText = "X"
            Me.MarcaXCampoDataGridViewTextBoxColumn.Name = "MarcaXCampoDataGridViewTextBoxColumn"
            Me.MarcaXCampoDataGridViewTextBoxColumn.ReadOnly = True
            Me.MarcaXCampoDataGridViewTextBoxColumn.Width = 60
            '
            'MarcaYCampoDataGridViewTextBoxColumn
            '
            Me.MarcaYCampoDataGridViewTextBoxColumn.DataPropertyName = "Marca_Y_Campo"
            Me.MarcaYCampoDataGridViewTextBoxColumn.HeaderText = "Y"
            Me.MarcaYCampoDataGridViewTextBoxColumn.Name = "MarcaYCampoDataGridViewTextBoxColumn"
            Me.MarcaYCampoDataGridViewTextBoxColumn.ReadOnly = True
            Me.MarcaYCampoDataGridViewTextBoxColumn.Width = 60
            '
            'MarcaWidthCampoDataGridViewTextBoxColumn
            '
            Me.MarcaWidthCampoDataGridViewTextBoxColumn.DataPropertyName = "Marca_Width_Campo"
            Me.MarcaWidthCampoDataGridViewTextBoxColumn.HeaderText = "Ancho"
            Me.MarcaWidthCampoDataGridViewTextBoxColumn.Name = "MarcaWidthCampoDataGridViewTextBoxColumn"
            Me.MarcaWidthCampoDataGridViewTextBoxColumn.ReadOnly = True
            Me.MarcaWidthCampoDataGridViewTextBoxColumn.Width = 60
            '
            'MarcaHeightCampoDataGridViewTextBoxColumn
            '
            Me.MarcaHeightCampoDataGridViewTextBoxColumn.DataPropertyName = "Marca_Height_Campo"
            Me.MarcaHeightCampoDataGridViewTextBoxColumn.HeaderText = "Alto"
            Me.MarcaHeightCampoDataGridViewTextBoxColumn.Name = "MarcaHeightCampoDataGridViewTextBoxColumn"
            Me.MarcaHeightCampoDataGridViewTextBoxColumn.ReadOnly = True
            Me.MarcaHeightCampoDataGridViewTextBoxColumn.Width = 60
            '
            'OrdenValidacionDataGridViewTextBoxColumn
            '
            Me.OrdenValidacionDataGridViewTextBoxColumn.DataPropertyName = "Orden_Validacion"
            Me.OrdenValidacionDataGridViewTextBoxColumn.HeaderText = "Orden"
            Me.OrdenValidacionDataGridViewTextBoxColumn.Name = "OrdenValidacionDataGridViewTextBoxColumn"
            Me.OrdenValidacionDataGridViewTextBoxColumn.Width = 60
            '
            'fk_Etapa_CapturaColumn
            '
            Me.fk_Etapa_CapturaColumn.DataPropertyName = "fk_Etapa_Captura"
            Me.fk_Etapa_CapturaColumn.DataSource = Me.TBL_Etapa_CapturaDataTableBindingSource
            Me.fk_Etapa_CapturaColumn.DisplayMember = "Nombre_Etapa_Captura"
            Me.fk_Etapa_CapturaColumn.HeaderText = "Etapa"
            Me.fk_Etapa_CapturaColumn.Name = "fk_Etapa_CapturaColumn"
            Me.fk_Etapa_CapturaColumn.ValueMember = "id_Etapa_Captura"
            '
            'TBL_Etapa_CapturaDataTableBindingSource
            '
            Me.TBL_Etapa_CapturaDataTableBindingSource.AllowNew = False
            Me.TBL_Etapa_CapturaDataTableBindingSource.DataSource = GetType(DBImaging.SchemaConfig.TBL_Etapa_CapturaDataTable)
            '
            'Nombre_Etapa_CapturaColumn
            '
            Me.Nombre_Etapa_CapturaColumn.DataPropertyName = "Nombre_Etapa_Captura"
            Me.Nombre_Etapa_CapturaColumn.HeaderText = "Nombre_Etapa_Captura"
            Me.Nombre_Etapa_CapturaColumn.Name = "Nombre_Etapa_CapturaColumn"
            Me.Nombre_Etapa_CapturaColumn.Visible = False
            '
            'fk_Modo_Respuesta_AutomaticaColumn
            '
            Me.fk_Modo_Respuesta_AutomaticaColumn.DataPropertyName = "fk_Modo_Respuesta_Automatica"
            Me.fk_Modo_Respuesta_AutomaticaColumn.DataSource = Me.TBL_Modo_Respuesta_AutomaticaDataTableBindingSource
            Me.fk_Modo_Respuesta_AutomaticaColumn.DisplayMember = "Nombre_Modo_Respuesta_Automatica"
            Me.fk_Modo_Respuesta_AutomaticaColumn.HeaderText = "Modo"
            Me.fk_Modo_Respuesta_AutomaticaColumn.Name = "fk_Modo_Respuesta_AutomaticaColumn"
            Me.fk_Modo_Respuesta_AutomaticaColumn.ValueMember = "id_Modo_Respuesta_Automatica"
            Me.fk_Modo_Respuesta_AutomaticaColumn.Width = 200
            '
            'TBL_Modo_Respuesta_AutomaticaDataTableBindingSource
            '
            Me.TBL_Modo_Respuesta_AutomaticaDataTableBindingSource.AllowNew = False
            Me.TBL_Modo_Respuesta_AutomaticaDataTableBindingSource.DataSource = GetType(DBImaging.SchemaConfig.TBL_Modo_Respuesta_AutomaticaDataTable)
            '
            'Nombre_Modo_Respuesta_AutomaticaColumn
            '
            Me.Nombre_Modo_Respuesta_AutomaticaColumn.DataPropertyName = "Nombre_Modo_Respuesta_Automatica"
            Me.Nombre_Modo_Respuesta_AutomaticaColumn.HeaderText = "Nombre_Modo_Respuesta_Automatica"
            Me.Nombre_Modo_Respuesta_AutomaticaColumn.Name = "Nombre_Modo_Respuesta_AutomaticaColumn"
            Me.Nombre_Modo_Respuesta_AutomaticaColumn.Visible = False
            '
            'ConfigColumn
            '
            Me.ConfigColumn.HeaderText = ""
            Me.ConfigColumn.Name = "ConfigColumn"
            Me.ConfigColumn.Text = "..."
            Me.ConfigColumn.Width = 30
            '
            'fk_Campo_1Column
            '
            Me.fk_Campo_1Column.DataPropertyName = "fk_Campo_1"
            Me.fk_Campo_1Column.HeaderText = "fk_Campo_1"
            Me.fk_Campo_1Column.Name = "fk_Campo_1Column"
            Me.fk_Campo_1Column.Visible = False
            '
            'fk_Campo_2Column
            '
            Me.fk_Campo_2Column.DataPropertyName = "fk_Campo_2"
            Me.fk_Campo_2Column.HeaderText = "fk_Campo_2"
            Me.fk_Campo_2Column.Name = "fk_Campo_2Column"
            Me.fk_Campo_2Column.Visible = False
            '
            'Valor_ComparacionColumn
            '
            Me.Valor_ComparacionColumn.DataPropertyName = "Valor_Comparacion"
            Me.Valor_ComparacionColumn.HeaderText = "Valor_Comparacion"
            Me.Valor_ComparacionColumn.Name = "Valor_ComparacionColumn"
            Me.Valor_ComparacionColumn.Visible = False
            '
            'Operador_ComparacionColumn
            '
            Me.Operador_ComparacionColumn.DataPropertyName = "Operador_Comparacion"
            Me.Operador_ComparacionColumn.HeaderText = "Operador_Comparacion"
            Me.Operador_ComparacionColumn.Name = "Operador_ComparacionColumn"
            Me.Operador_ComparacionColumn.Visible = False
            '
            'CTA_Validacion_ConfiguracionDataTableBindingSource
            '
            Me.CTA_Validacion_ConfiguracionDataTableBindingSource.AllowNew = False
            Me.CTA_Validacion_ConfiguracionDataTableBindingSource.DataSource = GetType(DBImaging.SchemaConfig.CTA_Validacion_ParametrizacionDataTable)
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label7)
            Me.GroupBox1.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.id_DocumentoDesktopComboBox)
            Me.GroupBox1.Location = New System.Drawing.Point(8, 7)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(523, 80)
            Me.GroupBox1.TabIndex = 31
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Campo"
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(14, 21)
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
            Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaDesktopComboBox.FormattingEnabled = True
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(105, 18)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(394, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 21
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(15, 53)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(72, 13)
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
            Me.id_DocumentoDesktopComboBox.Location = New System.Drawing.Point(106, 50)
            Me.id_DocumentoDesktopComboBox.Name = "id_DocumentoDesktopComboBox"
            Me.id_DocumentoDesktopComboBox.Size = New System.Drawing.Size(392, 21)
            Me.id_DocumentoDesktopComboBox.TabIndex = 0
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(697, 371)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(105, 30)
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
            Me.CerrarButton.Location = New System.Drawing.Point(809, 371)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(105, 30)
            Me.CerrarButton.TabIndex = 33
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormParValidaciones
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(923, 408)
            Me.Controls.Add(Me.ValidacionesDataGridView)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormParValidaciones"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Configuracion Validaciones Imaging"
            CType(Me.ValidacionesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.TBL_Etapa_CapturaDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.TBL_Modo_Respuesta_AutomaticaDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CTA_Validacion_ConfiguracionDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ValidacionesDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents EsquemaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents id_DocumentoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents CTA_Validacion_ConfiguracionDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents TBL_Modo_Respuesta_AutomaticaDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents TBL_Etapa_CapturaDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents FkDocumentoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdValidacionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents PreguntaValidacionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ObligatorioDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents UsaMotivoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents UsaMarcaDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents AsignarMarcaColumn As System.Windows.Forms.DataGridViewImageColumn
        Friend WithEvents MarcaXCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents MarcaYCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents MarcaWidthCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents MarcaHeightCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents OrdenValidacionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Etapa_CapturaColumn As System.Windows.Forms.DataGridViewComboBoxColumn
        Friend WithEvents Nombre_Etapa_CapturaColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Modo_Respuesta_AutomaticaColumn As System.Windows.Forms.DataGridViewComboBoxColumn
        Friend WithEvents Nombre_Modo_Respuesta_AutomaticaColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ConfigColumn As System.Windows.Forms.DataGridViewButtonColumn
        Friend WithEvents fk_Campo_1Column As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Campo_2Column As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Valor_ComparacionColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Operador_ComparacionColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace