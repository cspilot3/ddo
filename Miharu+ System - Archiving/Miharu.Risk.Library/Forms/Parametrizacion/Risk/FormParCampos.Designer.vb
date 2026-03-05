Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Library

Namespace Forms.Parametrizacion.Risk

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParCampos
        Inherits FormBase

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormParCampos))
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.CamposDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.DocumentosComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.EsquemaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Es_Llave = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.fk_documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Es_campo_cargue = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Es_cargue_carpeta = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Es_Campo_Actualizacion = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Es_Imprimible = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Label = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Usa_Captura = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Usa_Doble_Captura = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Es_Campo_Destape = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Es_Categoria = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Usa_Trigger = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.AsignarCampoTrigger = New System.Windows.Forms.DataGridViewImageColumn()
            CType(Me.CamposDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(572, 331)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(100, 30)
            Me.AceptarButton.TabIndex = 26
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(680, 331)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(100, 30)
            Me.CerrarButton.TabIndex = 25
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'CamposDesktopDataGridView
            '
            Me.CamposDesktopDataGridView.AllowUserToAddRows = False
            Me.CamposDesktopDataGridView.AllowUserToDeleteRows = False
            Me.CamposDesktopDataGridView.AllowUserToOrderColumns = True
            Me.CamposDesktopDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CamposDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.CamposDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CamposDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.CamposDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CamposDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Es_Llave, Me.fk_documento, Me.Nombre_Campo, Me.id_campo, Me.fk_campo, Me.Es_campo_cargue, Me.Es_cargue_carpeta, Me.Es_Campo_Actualizacion, Me.Es_Imprimible, Me.Label, Me.Usa_Captura, Me.Usa_Doble_Captura, Me.Es_Campo_Destape, Me.Es_Categoria, Me.Usa_Trigger, Me.AsignarCampoTrigger})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.CamposDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.CamposDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.CamposDesktopDataGridView.Location = New System.Drawing.Point(12, 84)
            Me.CamposDesktopDataGridView.MultiSelect = False
            Me.CamposDesktopDataGridView.Name = "CamposDesktopDataGridView"
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CamposDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.CamposDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.CamposDesktopDataGridView.Size = New System.Drawing.Size(768, 241)
            Me.CamposDesktopDataGridView.TabIndex = 24
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(311, 18)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(81, 13)
            Me.Label1.TabIndex = 22
            Me.Label1.Text = "Documentos:"
            '
            'DocumentosComboBox
            '
            Me.DocumentosComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DocumentosComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DocumentosComboBox.DisabledEnter = False
            Me.DocumentosComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentosComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DocumentosComboBox.FormattingEnabled = True
            Me.DocumentosComboBox.Location = New System.Drawing.Point(314, 38)
            Me.DocumentosComboBox.Name = "DocumentosComboBox"
            Me.DocumentosComboBox.Size = New System.Drawing.Size(326, 21)
            Me.DocumentosComboBox.TabIndex = 27
            '
            'EsquemaDesktopComboBox
            '
            Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaDesktopComboBox.DisabledEnter = False
            Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaDesktopComboBox.FormattingEnabled = True
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(14, 38)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(244, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 29
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(12, 18)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(67, 13)
            Me.Label2.TabIndex = 28
            Me.Label2.Text = "Esquemas:"
            '
            'Es_Llave
            '
            Me.Es_Llave.DataPropertyName = "Es_Llave"
            Me.Es_Llave.HeaderText = "Es Llave"
            Me.Es_Llave.Name = "Es_Llave"
            Me.Es_Llave.Width = 58
            '
            'fk_documento
            '
            Me.fk_documento.DataPropertyName = "fk_documento"
            Me.fk_documento.HeaderText = "Documento"
            Me.fk_documento.Name = "fk_documento"
            Me.fk_documento.ReadOnly = True
            Me.fk_documento.Visible = False
            Me.fk_documento.Width = 97
            '
            'Nombre_Campo
            '
            Me.Nombre_Campo.DataPropertyName = "Nombre_Campo"
            Me.Nombre_Campo.HeaderText = "Nombre Campo"
            Me.Nombre_Campo.Name = "Nombre_Campo"
            Me.Nombre_Campo.ReadOnly = True
            Me.Nombre_Campo.Width = 108
            '
            'id_campo
            '
            Me.id_campo.DataPropertyName = "id_campo"
            Me.id_campo.HeaderText = "Id Campo"
            Me.id_campo.Name = "id_campo"
            Me.id_campo.ReadOnly = True
            Me.id_campo.Visible = False
            Me.id_campo.Width = 79
            '
            'fk_campo
            '
            Me.fk_campo.DataPropertyName = "fk_campo"
            Me.fk_campo.HeaderText = "fk_campo"
            Me.fk_campo.Name = "fk_campo"
            Me.fk_campo.Visible = False
            Me.fk_campo.Width = 88
            '
            'Es_campo_cargue
            '
            Me.Es_campo_cargue.DataPropertyName = "Es_campo_cargue"
            Me.Es_campo_cargue.HeaderText = "Es Campo Cargue"
            Me.Es_campo_cargue.Name = "Es_campo_cargue"
            Me.Es_campo_cargue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            Me.Es_campo_cargue.Width = 118
            '
            'Es_cargue_carpeta
            '
            Me.Es_cargue_carpeta.DataPropertyName = "Es_cargue_carpeta"
            Me.Es_cargue_carpeta.HeaderText = "Es Cargue carpeta"
            Me.Es_cargue_carpeta.Name = "Es_cargue_carpeta"
            Me.Es_cargue_carpeta.Width = 104
            '
            'Es_Campo_Actualizacion
            '
            Me.Es_Campo_Actualizacion.DataPropertyName = "Es_Campo_Actualizacion"
            Me.Es_Campo_Actualizacion.HeaderText = "Es Campo Actualizacion"
            Me.Es_Campo_Actualizacion.Name = "Es_Campo_Actualizacion"
            Me.Es_Campo_Actualizacion.Width = 131
            '
            'Es_Imprimible
            '
            Me.Es_Imprimible.DataPropertyName = "Es_Imprimible"
            Me.Es_Imprimible.HeaderText = "Imprimible"
            Me.Es_Imprimible.Name = "Es_Imprimible"
            Me.Es_Imprimible.Width = 75
            '
            'Label
            '
            Me.Label.DataPropertyName = "Label"
            Me.Label.HeaderText = "Label"
            Me.Label.Name = "Label"
            Me.Label.Width = 62
            '
            'Usa_Captura
            '
            Me.Usa_Captura.DataPropertyName = "Usa_Captura"
            Me.Usa_Captura.HeaderText = "Usa Captura"
            Me.Usa_Captura.Name = "Usa_Captura"
            Me.Usa_Captura.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.Usa_Captura.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            Me.Usa_Captura.Width = 93
            '
            'Usa_Doble_Captura
            '
            Me.Usa_Doble_Captura.DataPropertyName = "Usa_Doble_Captura"
            Me.Usa_Doble_Captura.HeaderText = "Usa Doble Captura"
            Me.Usa_Doble_Captura.Name = "Usa_Doble_Captura"
            Me.Usa_Doble_Captura.Width = 105
            '
            'Es_Campo_Destape
            '
            Me.Es_Campo_Destape.DataPropertyName = "Es_Campo_Destape"
            Me.Es_Campo_Destape.HeaderText = "Campo Destape"
            Me.Es_Campo_Destape.Name = "Es_Campo_Destape"
            Me.Es_Campo_Destape.Width = 92
            '
            'Es_Categoria
            '
            Me.Es_Categoria.DataPropertyName = "Es_Categoria"
            Me.Es_Categoria.HeaderText = "Es Campo Fondo"
            Me.Es_Categoria.Name = "Es_Categoria"
            Me.Es_Categoria.Width = 94
            '
            'Usa_Trigger
            '
            Me.Usa_Trigger.DataPropertyName = "Usa_Trigger"
            Me.Usa_Trigger.HeaderText = "Usa Trigger"
            Me.Usa_Trigger.Name = "Usa_Trigger"
            Me.Usa_Trigger.Width = 70
            '
            'AsignarCampoTrigger
            '
            Me.AsignarCampoTrigger.HeaderText = "Trigger"
            Me.AsignarCampoTrigger.Image = Global.Miharu.Risk.Library.My.Resources.Resources.cog_go
            Me.AsignarCampoTrigger.Name = "AsignarCampoTrigger"
            Me.AsignarCampoTrigger.Width = 54
            '
            'FormParCampos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(792, 373)
            Me.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.DocumentosComboBox)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.CamposDesktopDataGridView)
            Me.Controls.Add(Me.Label1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormParCampos"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Parametrizacion de Campos"
            CType(Me.CamposDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents CamposDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents DocumentosComboBox As DesktopComboBoxControl
        Friend WithEvents EsquemaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Es_Llave As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents fk_documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Es_campo_cargue As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Es_cargue_carpeta As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Es_Campo_Actualizacion As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Es_Imprimible As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Label As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Usa_Captura As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Usa_Doble_Captura As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Es_Campo_Destape As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Es_Categoria As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Usa_Trigger As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents AsignarCampoTrigger As System.Windows.Forms.DataGridViewImageColumn
    End Class
End Namespace