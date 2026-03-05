Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormValidacionesDinamicas
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormValidacionesDinamicas))
            Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
            Me.NuevaValidacionButton = New System.Windows.Forms.Button()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.ValidacionesDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Esquema = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Documento_1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo_1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Documento_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Operador = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Valor_Comparar = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Pregunta_Validacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Documento_Obligatorio_1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Documento_Obligatorio_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Documento_Obligatorio_3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Eliminado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.DocumentoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.EsquemaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.EntidadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ProyectoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.FiltrarButton = New System.Windows.Forms.Button()
            Me.GroupBox2.SuspendLayout()
            CType(Me.ValidacionesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'DataGridViewImageColumn1
            '
            Me.DataGridViewImageColumn1.HeaderText = "Eliminar"
            Me.DataGridViewImageColumn1.Image = Global.Miharu.Desktop.My.Resources.Resources.btnSalir
            Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
            Me.DataGridViewImageColumn1.Width = 58
            '
            'NuevaValidacionButton
            '
            Me.NuevaValidacionButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.NuevaValidacionButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnAgregar
            Me.NuevaValidacionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.NuevaValidacionButton.Location = New System.Drawing.Point(12, 403)
            Me.NuevaValidacionButton.Name = "NuevaValidacionButton"
            Me.NuevaValidacionButton.Size = New System.Drawing.Size(151, 30)
            Me.NuevaValidacionButton.TabIndex = 58
            Me.NuevaValidacionButton.Text = "&Agregar Validación"
            Me.NuevaValidacionButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.NuevaValidacionButton.UseVisualStyleBackColor = True
            '
            'GroupBox2
            '
            Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox2.Controls.Add(Me.ValidacionesDataGridView)
            Me.GroupBox2.Location = New System.Drawing.Point(12, 107)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(841, 290)
            Me.GroupBox2.TabIndex = 57
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Detalle"
            '
            'ValidacionesDataGridView
            '
            Me.ValidacionesDataGridView.AllowUserToAddRows = False
            Me.ValidacionesDataGridView.AllowUserToDeleteRows = False
            Me.ValidacionesDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ValidacionesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.ValidacionesDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ValidacionesDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.ValidacionesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ValidacionesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Entidad, Me.Proyecto, Me.Esquema, Me.fk_Documento, Me.Nombre_Documento_1, Me.Nombre_Campo_1, Me.Nombre_Documento_2, Me.Nombre_Campo_2, Me.Operador, Me.Valor_Comparar, Me.Pregunta_Validacion, Me.Nombre_Documento_Obligatorio_1, Me.Nombre_Documento_Obligatorio_2, Me.Nombre_Documento_Obligatorio_3, Me.Eliminado})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.ValidacionesDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.ValidacionesDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.ValidacionesDataGridView.Location = New System.Drawing.Point(18, 20)
            Me.ValidacionesDataGridView.MultiSelect = False
            Me.ValidacionesDataGridView.Name = "ValidacionesDataGridView"
            Me.ValidacionesDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ValidacionesDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.ValidacionesDataGridView.RowHeadersVisible = False
            Me.ValidacionesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ValidacionesDataGridView.Size = New System.Drawing.Size(808, 251)
            Me.ValidacionesDataGridView.TabIndex = 5
            '
            'Entidad
            '
            Me.Entidad.DataPropertyName = "Nombre_Entidad"
            Me.Entidad.HeaderText = "Entidad"
            Me.Entidad.Name = "Entidad"
            Me.Entidad.ReadOnly = True
            Me.Entidad.Width = 74
            '
            'Proyecto
            '
            Me.Proyecto.DataPropertyName = "Nombre_Proyecto"
            Me.Proyecto.HeaderText = "Proyecto"
            Me.Proyecto.Name = "Proyecto"
            Me.Proyecto.ReadOnly = True
            Me.Proyecto.Width = 83
            '
            'Esquema
            '
            Me.Esquema.DataPropertyName = "Nombre_Esquema"
            Me.Esquema.HeaderText = "Esquema"
            Me.Esquema.Name = "Esquema"
            Me.Esquema.ReadOnly = True
            Me.Esquema.Width = 83
            '
            'fk_Documento
            '
            Me.fk_Documento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.fk_Documento.DataPropertyName = "Nombre_Tipo_Validacion_Dinamica"
            Me.fk_Documento.HeaderText = "Tipo Validacion Dinamica"
            Me.fk_Documento.Name = "fk_Documento"
            Me.fk_Documento.ReadOnly = True
            Me.fk_Documento.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.fk_Documento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            Me.fk_Documento.Width = 137
            '
            'Nombre_Documento_1
            '
            Me.Nombre_Documento_1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Nombre_Documento_1.DataPropertyName = "Nombre_Documento_1"
            Me.Nombre_Documento_1.HeaderText = "Nombre Documento 1"
            Me.Nombre_Documento_1.Name = "Nombre_Documento_1"
            Me.Nombre_Documento_1.ReadOnly = True
            Me.Nombre_Documento_1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.Nombre_Documento_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            Me.Nombre_Documento_1.Width = 122
            '
            'Nombre_Campo_1
            '
            Me.Nombre_Campo_1.DataPropertyName = "Nombre_Campo_1"
            Me.Nombre_Campo_1.HeaderText = "Nombre Campo 1"
            Me.Nombre_Campo_1.Name = "Nombre_Campo_1"
            Me.Nombre_Campo_1.ReadOnly = True
            Me.Nombre_Campo_1.Width = 117
            '
            'Nombre_Documento_2
            '
            Me.Nombre_Documento_2.DataPropertyName = "Nombre_Documento_2"
            Me.Nombre_Documento_2.HeaderText = "Nombre Documento 2"
            Me.Nombre_Documento_2.Name = "Nombre_Documento_2"
            Me.Nombre_Documento_2.ReadOnly = True
            Me.Nombre_Documento_2.Width = 141
            '
            'Nombre_Campo_2
            '
            Me.Nombre_Campo_2.DataPropertyName = "Nombre_Campo_2"
            Me.Nombre_Campo_2.HeaderText = "Nombre Campo 2"
            Me.Nombre_Campo_2.Name = "Nombre_Campo_2"
            Me.Nombre_Campo_2.ReadOnly = True
            Me.Nombre_Campo_2.Width = 117
            '
            'Operador
            '
            Me.Operador.DataPropertyName = "Operador"
            Me.Operador.HeaderText = "Operador"
            Me.Operador.Name = "Operador"
            Me.Operador.ReadOnly = True
            Me.Operador.Width = 85
            '
            'Valor_Comparar
            '
            Me.Valor_Comparar.DataPropertyName = "Valor_Comparar"
            Me.Valor_Comparar.HeaderText = "Valor Comparar"
            Me.Valor_Comparar.Name = "Valor_Comparar"
            Me.Valor_Comparar.ReadOnly = True
            Me.Valor_Comparar.Width = 110
            '
            'Pregunta_Validacion
            '
            Me.Pregunta_Validacion.DataPropertyName = "Pregunta_Validacion"
            Me.Pregunta_Validacion.HeaderText = "Pregunta Validacion"
            Me.Pregunta_Validacion.Name = "Pregunta_Validacion"
            Me.Pregunta_Validacion.ReadOnly = True
            Me.Pregunta_Validacion.Width = 132
            '
            'Nombre_Documento_Obligatorio_1
            '
            Me.Nombre_Documento_Obligatorio_1.DataPropertyName = "Nombre_Documento_Obligatorio_1"
            Me.Nombre_Documento_Obligatorio_1.HeaderText = "Nombre Documento Obligatorio 1"
            Me.Nombre_Documento_Obligatorio_1.Name = "Nombre_Documento_Obligatorio_1"
            Me.Nombre_Documento_Obligatorio_1.ReadOnly = True
            Me.Nombre_Documento_Obligatorio_1.Width = 199
            '
            'Nombre_Documento_Obligatorio_2
            '
            Me.Nombre_Documento_Obligatorio_2.DataPropertyName = "Nombre_Documento_Obligatorio_2"
            Me.Nombre_Documento_Obligatorio_2.HeaderText = "Nombre Documento Obligatorio 2"
            Me.Nombre_Documento_Obligatorio_2.Name = "Nombre_Documento_Obligatorio_2"
            Me.Nombre_Documento_Obligatorio_2.ReadOnly = True
            Me.Nombre_Documento_Obligatorio_2.Width = 199
            '
            'Nombre_Documento_Obligatorio_3
            '
            Me.Nombre_Documento_Obligatorio_3.DataPropertyName = "Nombre_Documento_Obligatorio_3"
            Me.Nombre_Documento_Obligatorio_3.HeaderText = "Nombre Documento Obligatorio 3"
            Me.Nombre_Documento_Obligatorio_3.Name = "Nombre_Documento_Obligatorio_3"
            Me.Nombre_Documento_Obligatorio_3.ReadOnly = True
            Me.Nombre_Documento_Obligatorio_3.Width = 199
            '
            'Eliminado
            '
            Me.Eliminado.DataPropertyName = "Eliminado"
            Me.Eliminado.HeaderText = "Eliminado"
            Me.Eliminado.Name = "Eliminado"
            Me.Eliminado.ReadOnly = True
            Me.Eliminado.Width = 67
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(763, 403)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 45
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.DocumentoDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.EntidadDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.ProyectoComboBox)
            Me.GroupBox1.Controls.Add(Me.Label9)
            Me.GroupBox1.Controls.Add(Me.FiltrarButton)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(841, 89)
            Me.GroupBox1.TabIndex = 56
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Filtros"
            '
            'DocumentoDesktopComboBox
            '
            Me.DocumentoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DocumentoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DocumentoDesktopComboBox.DisabledEnter = False
            Me.DocumentoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentoDesktopComboBox.fk_Campo = 0
            Me.DocumentoDesktopComboBox.fk_Documento = 0
            Me.DocumentoDesktopComboBox.fk_Validacion = 0
            Me.DocumentoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DocumentoDesktopComboBox.FormattingEnabled = True
            Me.DocumentoDesktopComboBox.Location = New System.Drawing.Point(477, 55)
            Me.DocumentoDesktopComboBox.Name = "DocumentoDesktopComboBox"
            Me.DocumentoDesktopComboBox.Size = New System.Drawing.Size(224, 21)
            Me.DocumentoDesktopComboBox.TabIndex = 66
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(399, 58)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(72, 13)
            Me.Label3.TabIndex = 65
            Me.Label3.Text = "Documento"
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
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(131, 55)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(245, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 64
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(17, 58)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(61, 13)
            Me.Label2.TabIndex = 63
            Me.Label2.Text = "Esquema:"
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
            Me.EntidadDesktopComboBox.Location = New System.Drawing.Point(131, 24)
            Me.EntidadDesktopComboBox.Name = "EntidadDesktopComboBox"
            Me.EntidadDesktopComboBox.Size = New System.Drawing.Size(245, 21)
            Me.EntidadDesktopComboBox.TabIndex = 60
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(17, 27)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(52, 13)
            Me.Label1.TabIndex = 59
            Me.Label1.Text = "Entidad:"
            '
            'ProyectoComboBox
            '
            Me.ProyectoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProyectoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProyectoComboBox.DisabledEnter = False
            Me.ProyectoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoComboBox.fk_Campo = 0
            Me.ProyectoComboBox.fk_Documento = 0
            Me.ProyectoComboBox.fk_Validacion = 0
            Me.ProyectoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProyectoComboBox.FormattingEnabled = True
            Me.ProyectoComboBox.Location = New System.Drawing.Point(477, 24)
            Me.ProyectoComboBox.Name = "ProyectoComboBox"
            Me.ProyectoComboBox.Size = New System.Drawing.Size(224, 21)
            Me.ProyectoComboBox.TabIndex = 58
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Location = New System.Drawing.Point(399, 27)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(61, 13)
            Me.Label9.TabIndex = 57
            Me.Label9.Text = "Proyecto:"
            '
            'FiltrarButton
            '
            Me.FiltrarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnBuscar
            Me.FiltrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.FiltrarButton.Location = New System.Drawing.Point(736, 46)
            Me.FiltrarButton.Name = "FiltrarButton"
            Me.FiltrarButton.Size = New System.Drawing.Size(90, 30)
            Me.FiltrarButton.TabIndex = 55
            Me.FiltrarButton.Text = "&Filtrar"
            Me.FiltrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.FiltrarButton.UseVisualStyleBackColor = True
            '
            'FormValidacionesDinamicas
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(865, 443)
            Me.Controls.Add(Me.NuevaValidacionButton)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.GroupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormValidacionesDinamicas"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Validaciones Dinamicas"
            Me.GroupBox2.ResumeLayout(False)
            CType(Me.ValidacionesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ValidacionesDataGridView As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents FiltrarButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
        Friend WithEvents NuevaValidacionButton As System.Windows.Forms.Button
        Friend WithEvents EntidadDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ProyectoComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents EsquemaDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Esquema As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Documento_1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo_1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Documento_2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo_2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Operador As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Valor_Comparar As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Pregunta_Validacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Documento_Obligatorio_1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Documento_Obligatorio_2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Documento_Obligatorio_3 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Eliminado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents DocumentoDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label3 As System.Windows.Forms.Label
    End Class
End Namespace