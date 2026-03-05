Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormValidaciones
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormValidaciones))
            Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
            Me.Button2 = New System.Windows.Forms.Button()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.ValidacionesDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.EsquemaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.DocumentoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.EntidadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ProyectoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.FiltrarButton = New System.Windows.Forms.Button()
            Me.Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Eliminado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Esquema = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Validacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Pregunta_Validacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Obligatorio = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Es_Subsanable = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Usa_Motivo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Nombre_Campo_Lista = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Validacion_Categoria = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Tipo_Validacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Pregunta_Validacion_Reporte = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Esquema = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Campo_Lista = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Validacion_Categoria = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Tipo_Validacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
            'Button2
            '
            Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Button2.Image = Global.Miharu.Desktop.My.Resources.Resources.btnAgregar
            Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Button2.Location = New System.Drawing.Point(12, 403)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(151, 30)
            Me.Button2.TabIndex = 58
            Me.Button2.Text = "&Agregar Validación"
            Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.Button2.UseVisualStyleBackColor = True
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
            Me.ValidacionesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Entidad, Me.Eliminado, Me.Proyecto, Me.Esquema, Me.fk_Documento, Me.id_Validacion, Me.Pregunta_Validacion, Me.Obligatorio, Me.Es_Subsanable, Me.Usa_Motivo, Me.Nombre_Campo_Lista, Me.fk_Validacion_Categoria, Me.Tipo_Validacion, Me.Pregunta_Validacion_Reporte, Me.fk_Entidad, Me.fk_Proyecto, Me.fk_Esquema, Me.id_Documento, Me.fk_Campo_Lista, Me.id_Validacion_Categoria, Me.fk_Tipo_Validacion})
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
            Me.GroupBox1.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.DocumentoDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label3)
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
            'EsquemaDesktopComboBox
            '
            Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaDesktopComboBox.DisabledEnter = False
            Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
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
            'DocumentoDesktopComboBox
            '
            Me.DocumentoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DocumentoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DocumentoDesktopComboBox.DisabledEnter = False
            Me.DocumentoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DocumentoDesktopComboBox.FormattingEnabled = True
            Me.DocumentoDesktopComboBox.Location = New System.Drawing.Point(477, 55)
            Me.DocumentoDesktopComboBox.Name = "DocumentoDesktopComboBox"
            Me.DocumentoDesktopComboBox.Size = New System.Drawing.Size(224, 21)
            Me.DocumentoDesktopComboBox.TabIndex = 62
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(399, 58)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(72, 13)
            Me.Label3.TabIndex = 61
            Me.Label3.Text = "Documento"
            '
            'EntidadDesktopComboBox
            '
            Me.EntidadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadDesktopComboBox.DisabledEnter = False
            Me.EntidadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
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
            'Entidad
            '
            Me.Entidad.DataPropertyName = "Nombre_Entidad"
            Me.Entidad.HeaderText = "Entidad"
            Me.Entidad.Name = "Entidad"
            Me.Entidad.ReadOnly = True
            Me.Entidad.Width = 74
            '
            'Eliminado
            '
            Me.Eliminado.DataPropertyName = "Eliminado"
            Me.Eliminado.HeaderText = "Eliminado"
            Me.Eliminado.Name = "Eliminado"
            Me.Eliminado.ReadOnly = True
            Me.Eliminado.Width = 67
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
            Me.fk_Documento.DataPropertyName = "Nombre_Documento"
            Me.fk_Documento.HeaderText = "Documento"
            Me.fk_Documento.Name = "fk_Documento"
            Me.fk_Documento.ReadOnly = True
            Me.fk_Documento.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.fk_Documento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            Me.fk_Documento.Width = 78
            '
            'id_Validacion
            '
            Me.id_Validacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.id_Validacion.DataPropertyName = "id_Validacion"
            Me.id_Validacion.HeaderText = "Id"
            Me.id_Validacion.Name = "id_Validacion"
            Me.id_Validacion.ReadOnly = True
            Me.id_Validacion.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.id_Validacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            Me.id_Validacion.Width = 25
            '
            'Pregunta_Validacion
            '
            Me.Pregunta_Validacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Pregunta_Validacion.DataPropertyName = "Pregunta_Validacion"
            Me.Pregunta_Validacion.HeaderText = "Pregunta"
            Me.Pregunta_Validacion.Name = "Pregunta_Validacion"
            Me.Pregunta_Validacion.ReadOnly = True
            Me.Pregunta_Validacion.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.Pregunta_Validacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            Me.Pregunta_Validacion.Width = 65
            '
            'Obligatorio
            '
            Me.Obligatorio.DataPropertyName = "Obligatorio"
            Me.Obligatorio.HeaderText = "Obligatorio"
            Me.Obligatorio.Name = "Obligatorio"
            Me.Obligatorio.ReadOnly = True
            Me.Obligatorio.Width = 75
            '
            'Es_Subsanable
            '
            Me.Es_Subsanable.DataPropertyName = "Es_Subsanable"
            Me.Es_Subsanable.HeaderText = "Es Subsanable"
            Me.Es_Subsanable.Name = "Es_Subsanable"
            Me.Es_Subsanable.ReadOnly = True
            Me.Es_Subsanable.Width = 84
            '
            'Usa_Motivo
            '
            Me.Usa_Motivo.DataPropertyName = "Usa_Motivo"
            Me.Usa_Motivo.HeaderText = "Usa Motivo"
            Me.Usa_Motivo.Name = "Usa_Motivo"
            Me.Usa_Motivo.ReadOnly = True
            Me.Usa_Motivo.Width = 68
            '
            'Nombre_Campo_Lista
            '
            Me.Nombre_Campo_Lista.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Nombre_Campo_Lista.DataPropertyName = "Nombre_Campo_Lista"
            Me.Nombre_Campo_Lista.HeaderText = "Lista"
            Me.Nombre_Campo_Lista.Name = "Nombre_Campo_Lista"
            Me.Nombre_Campo_Lista.ReadOnly = True
            Me.Nombre_Campo_Lista.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.Nombre_Campo_Lista.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            Me.Nombre_Campo_Lista.Width = 40
            '
            'fk_Validacion_Categoria
            '
            Me.fk_Validacion_Categoria.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.fk_Validacion_Categoria.DataPropertyName = "Nombre_Validacion_Categoria"
            Me.fk_Validacion_Categoria.HeaderText = "Categoria"
            Me.fk_Validacion_Categoria.Name = "fk_Validacion_Categoria"
            Me.fk_Validacion_Categoria.ReadOnly = True
            Me.fk_Validacion_Categoria.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.fk_Validacion_Categoria.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            Me.fk_Validacion_Categoria.Width = 68
            '
            'Tipo_Validacion
            '
            Me.Tipo_Validacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Tipo_Validacion.DataPropertyName = "Nombre_Tipo_Validacion"
            Me.Tipo_Validacion.HeaderText = "Tipo"
            Me.Tipo_Validacion.Name = "Tipo_Validacion"
            Me.Tipo_Validacion.ReadOnly = True
            Me.Tipo_Validacion.Width = 56
            '
            'Pregunta_Validacion_Reporte
            '
            Me.Pregunta_Validacion_Reporte.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Pregunta_Validacion_Reporte.DataPropertyName = "Pregunta_Validacion_Reporte"
            Me.Pregunta_Validacion_Reporte.HeaderText = "Pregunta Reporte"
            Me.Pregunta_Validacion_Reporte.Name = "Pregunta_Validacion_Reporte"
            Me.Pregunta_Validacion_Reporte.ReadOnly = True
            Me.Pregunta_Validacion_Reporte.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.Pregunta_Validacion_Reporte.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            Me.Pregunta_Validacion_Reporte.Width = 103
            '
            'fk_Entidad
            '
            Me.fk_Entidad.DataPropertyName = "fk_Entidad"
            Me.fk_Entidad.HeaderText = "fk_Entidad"
            Me.fk_Entidad.Name = "fk_Entidad"
            Me.fk_Entidad.ReadOnly = True
            Me.fk_Entidad.Visible = False
            Me.fk_Entidad.Width = 92
            '
            'fk_Proyecto
            '
            Me.fk_Proyecto.DataPropertyName = "fk_Proyecto"
            Me.fk_Proyecto.HeaderText = "fk_Proyecto"
            Me.fk_Proyecto.Name = "fk_Proyecto"
            Me.fk_Proyecto.ReadOnly = True
            Me.fk_Proyecto.Visible = False
            Me.fk_Proyecto.Width = 101
            '
            'fk_Esquema
            '
            Me.fk_Esquema.DataPropertyName = "fk_Esquema"
            Me.fk_Esquema.HeaderText = "fk_Esquema"
            Me.fk_Esquema.Name = "fk_Esquema"
            Me.fk_Esquema.ReadOnly = True
            Me.fk_Esquema.Visible = False
            Me.fk_Esquema.Width = 101
            '
            'id_Documento
            '
            Me.id_Documento.DataPropertyName = "id_Documento"
            Me.id_Documento.HeaderText = "id_Documento"
            Me.id_Documento.Name = "id_Documento"
            Me.id_Documento.ReadOnly = True
            Me.id_Documento.Visible = False
            Me.id_Documento.Width = 114
            '
            'fk_Campo_Lista
            '
            Me.fk_Campo_Lista.DataPropertyName = "fk_Campo_Lista"
            Me.fk_Campo_Lista.HeaderText = "fk_Campo_Lista"
            Me.fk_Campo_Lista.Name = "fk_Campo_Lista"
            Me.fk_Campo_Lista.ReadOnly = True
            Me.fk_Campo_Lista.Visible = False
            Me.fk_Campo_Lista.Width = 123
            '
            'id_Validacion_Categoria
            '
            Me.id_Validacion_Categoria.DataPropertyName = "id_Validacion_Categoria"
            Me.id_Validacion_Categoria.HeaderText = "id_Validacion_Categoria"
            Me.id_Validacion_Categoria.Name = "id_Validacion_Categoria"
            Me.id_Validacion_Categoria.ReadOnly = True
            Me.id_Validacion_Categoria.Visible = False
            Me.id_Validacion_Categoria.Width = 168
            '
            'fk_Tipo_Validacion
            '
            Me.fk_Tipo_Validacion.DataPropertyName = "fk_Tipo_Validacion"
            Me.fk_Tipo_Validacion.HeaderText = "fk_Tipo_Validacion"
            Me.fk_Tipo_Validacion.Name = "fk_Tipo_Validacion"
            Me.fk_Tipo_Validacion.ReadOnly = True
            Me.fk_Tipo_Validacion.Visible = False
            Me.fk_Tipo_Validacion.Width = 138
            '
            'FormValidaciones
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(865, 443)
            Me.Controls.Add(Me.Button2)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.GroupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormValidaciones"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Validaciones"
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
        Friend WithEvents Button2 As System.Windows.Forms.Button
        Friend WithEvents EntidadDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ProyectoComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents EsquemaDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents DocumentoDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Eliminado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Esquema As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Validacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Pregunta_Validacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Obligatorio As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Es_Subsanable As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Usa_Motivo As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Nombre_Campo_Lista As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Validacion_Categoria As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Tipo_Validacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Pregunta_Validacion_Reporte As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Esquema As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Campo_Lista As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Validacion_Categoria As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Tipo_Validacion As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace