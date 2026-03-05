Namespace Forms.Facturacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormHomologacionFacturacion
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormHomologacionFacturacion))
            Me.EntidadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.ProyectoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.EsquemaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.EntidadFacturacionDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.EsquemaFacturacionDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.FacturacionDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.id_Servicio = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Servicio = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Clasificacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Grupo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Genero = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Producto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Producto_Especifico = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Producto_Detalle = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Cod_Cuenta = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EditarButton = New System.Windows.Forms.Button()
            Me.CancelarEdicionButton = New System.Windows.Forms.Button()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            CType(Me.FacturacionDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'EntidadDesktopComboBox
            '
            Me.EntidadDesktopComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                      Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EntidadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadDesktopComboBox.DisabledEnter = False
            Me.EntidadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadDesktopComboBox.FormattingEnabled = True
            Me.EntidadDesktopComboBox.Location = New System.Drawing.Point(109, 25)
            Me.EntidadDesktopComboBox.Name = "EntidadDesktopComboBox"
            Me.EntidadDesktopComboBox.Size = New System.Drawing.Size(510, 21)
            Me.EntidadDesktopComboBox.TabIndex = 33
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(27, 28)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(49, 13)
            Me.Label2.TabIndex = 32
            Me.Label2.Text = "Entidad"
            '
            'ProyectoDesktopComboBox
            '
            Me.ProyectoDesktopComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                       Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ProyectoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProyectoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProyectoDesktopComboBox.DisabledEnter = False
            Me.ProyectoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProyectoDesktopComboBox.FormattingEnabled = True
            Me.ProyectoDesktopComboBox.Location = New System.Drawing.Point(109, 53)
            Me.ProyectoDesktopComboBox.Name = "ProyectoDesktopComboBox"
            Me.ProyectoDesktopComboBox.Size = New System.Drawing.Size(510, 21)
            Me.ProyectoDesktopComboBox.TabIndex = 35
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(27, 56)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(58, 13)
            Me.Label1.TabIndex = 34
            Me.Label1.Text = "Proyecto"
            '
            'EsquemaDesktopComboBox
            '
            Me.EsquemaDesktopComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                      Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaDesktopComboBox.DisabledEnter = False
            Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaDesktopComboBox.FormattingEnabled = True
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(109, 81)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(510, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 37
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(27, 84)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(58, 13)
            Me.Label3.TabIndex = 36
            Me.Label3.Text = "Esquema"
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.EntidadFacturacionDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label5)
            Me.GroupBox1.Controls.Add(Me.EsquemaFacturacionDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.BuscarButton)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.EntidadDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.ProyectoDesktopComboBox)
            Me.GroupBox1.Location = New System.Drawing.Point(19, 17)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(747, 181)
            Me.GroupBox1.TabIndex = 38
            Me.GroupBox1.TabStop = False
            '
            'EntidadFacturacionDesktopComboBox
            '
            Me.EntidadFacturacionDesktopComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                                 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EntidadFacturacionDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadFacturacionDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadFacturacionDesktopComboBox.DisabledEnter = False
            Me.EntidadFacturacionDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadFacturacionDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadFacturacionDesktopComboBox.FormattingEnabled = True
            Me.EntidadFacturacionDesktopComboBox.Location = New System.Drawing.Point(172, 111)
            Me.EntidadFacturacionDesktopComboBox.Name = "EntidadFacturacionDesktopComboBox"
            Me.EntidadFacturacionDesktopComboBox.Size = New System.Drawing.Size(447, 21)
            Me.EntidadFacturacionDesktopComboBox.TabIndex = 42
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(27, 114)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(118, 13)
            Me.Label5.TabIndex = 41
            Me.Label5.Text = "Entidad Facturacion"
            '
            'EsquemaFacturacionDesktopComboBox
            '
            Me.EsquemaFacturacionDesktopComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                                 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EsquemaFacturacionDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaFacturacionDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaFacturacionDesktopComboBox.DisabledEnter = False
            Me.EsquemaFacturacionDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaFacturacionDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaFacturacionDesktopComboBox.FormattingEnabled = True
            Me.EsquemaFacturacionDesktopComboBox.Location = New System.Drawing.Point(172, 140)
            Me.EsquemaFacturacionDesktopComboBox.Name = "EsquemaFacturacionDesktopComboBox"
            Me.EsquemaFacturacionDesktopComboBox.Size = New System.Drawing.Size(447, 21)
            Me.EsquemaFacturacionDesktopComboBox.TabIndex = 40
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(27, 143)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(127, 13)
            Me.Label4.TabIndex = 39
            Me.Label4.Text = "Esquema Facturacion"
            '
            'BuscarButton
            '
            Me.BuscarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(639, 131)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(89, 30)
            Me.BuscarButton.TabIndex = 38
            Me.BuscarButton.Text = "&Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'FacturacionDesktopDataGridView
            '
            Me.FacturacionDesktopDataGridView.AllowUserToAddRows = False
            Me.FacturacionDesktopDataGridView.AllowUserToDeleteRows = False
            Me.FacturacionDesktopDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                               Or System.Windows.Forms.AnchorStyles.Left) _
                                                              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FacturacionDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.FacturacionDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.FacturacionDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.FacturacionDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.FacturacionDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_Servicio, Me.Servicio, Me.Clasificacion, Me.Grupo, Me.Genero, Me.Producto, Me.Producto_Especifico, Me.Producto_Detalle, Me.Cod_Cuenta})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.FacturacionDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.FacturacionDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.FacturacionDesktopDataGridView.Location = New System.Drawing.Point(19, 216)
            Me.FacturacionDesktopDataGridView.MultiSelect = False
            Me.FacturacionDesktopDataGridView.Name = "FacturacionDesktopDataGridView"
            Me.FacturacionDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.FacturacionDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.FacturacionDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
            Me.FacturacionDesktopDataGridView.Size = New System.Drawing.Size(747, 295)
            Me.FacturacionDesktopDataGridView.TabIndex = 39
            '
            'id_Servicio
            '
            Me.id_Servicio.DataPropertyName = "id_Servicio"
            Me.id_Servicio.HeaderText = "id_Servicio"
            Me.id_Servicio.Name = "id_Servicio"
            Me.id_Servicio.ReadOnly = True
            Me.id_Servicio.Visible = False
            Me.id_Servicio.Width = 94
            '
            'Servicio
            '
            Me.Servicio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Servicio.DataPropertyName = "Servicio_Facturacion"
            Me.Servicio.HeaderText = "Servicio"
            Me.Servicio.Name = "Servicio"
            Me.Servicio.ReadOnly = True
            Me.Servicio.Width = 77
            '
            'Clasificacion
            '
            Me.Clasificacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Clasificacion.DataPropertyName = "Clasificacion"
            Me.Clasificacion.HeaderText = "Clasificacion"
            Me.Clasificacion.Name = "Clasificacion"
            Me.Clasificacion.ReadOnly = True
            Me.Clasificacion.Width = 101
            '
            'Grupo
            '
            Me.Grupo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Grupo.DataPropertyName = "Grupo"
            Me.Grupo.HeaderText = "Grupo"
            Me.Grupo.Name = "Grupo"
            Me.Grupo.ReadOnly = True
            Me.Grupo.Width = 66
            '
            'Genero
            '
            Me.Genero.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Genero.DataPropertyName = "Genero"
            Me.Genero.HeaderText = "Genero"
            Me.Genero.Name = "Genero"
            Me.Genero.ReadOnly = True
            Me.Genero.Width = 73
            '
            'Producto
            '
            Me.Producto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Producto.DataPropertyName = "Producto"
            Me.Producto.HeaderText = "Producto"
            Me.Producto.Name = "Producto"
            Me.Producto.ReadOnly = True
            Me.Producto.Width = 83
            '
            'Producto_Especifico
            '
            Me.Producto_Especifico.DataPropertyName = "Producto_Especifico"
            Me.Producto_Especifico.HeaderText = "Producto Especifico"
            Me.Producto_Especifico.Name = "Producto_Especifico"
            Me.Producto_Especifico.ReadOnly = True
            Me.Producto_Especifico.Width = 129
            '
            'Producto_Detalle
            '
            Me.Producto_Detalle.DataPropertyName = "Producto_Detalle"
            Me.Producto_Detalle.HeaderText = "Producto Detalle"
            Me.Producto_Detalle.Name = "Producto_Detalle"
            Me.Producto_Detalle.ReadOnly = True
            Me.Producto_Detalle.Width = 115
            '
            'Cod_Cuenta
            '
            Me.Cod_Cuenta.DataPropertyName = "Cod_Cuenta"
            Me.Cod_Cuenta.HeaderText = "Cod Cuenta"
            Me.Cod_Cuenta.Name = "Cod_Cuenta"
            Me.Cod_Cuenta.ReadOnly = True
            Me.Cod_Cuenta.Width = 88
            '
            'EditarButton
            '
            Me.EditarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.EditarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnGenerarEstructura
            Me.EditarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.EditarButton.Location = New System.Drawing.Point(19, 526)
            Me.EditarButton.Name = "EditarButton"
            Me.EditarButton.Size = New System.Drawing.Size(123, 30)
            Me.EditarButton.TabIndex = 43
            Me.EditarButton.Text = "&Editar Codigos"
            Me.EditarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.EditarButton.UseVisualStyleBackColor = True
            '
            'CancelarEdicionButton
            '
            Me.CancelarEdicionButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.CancelarEdicionButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnSalir1
            Me.CancelarEdicionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarEdicionButton.Location = New System.Drawing.Point(148, 526)
            Me.CancelarEdicionButton.Name = "CancelarEdicionButton"
            Me.CancelarEdicionButton.Size = New System.Drawing.Size(131, 30)
            Me.CancelarEdicionButton.TabIndex = 44
            Me.CancelarEdicionButton.Text = "Cancelar Edicion"
            Me.CancelarEdicionButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarEdicionButton.UseVisualStyleBackColor = True
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(577, 526)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(95, 30)
            Me.GuardarButton.TabIndex = 41
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(678, 526)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 30)
            Me.CerrarButton.TabIndex = 40
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormHomologacionFacturacion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(792, 573)
            Me.Controls.Add(Me.CancelarEdicionButton)
            Me.Controls.Add(Me.EditarButton)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.FacturacionDesktopDataGridView)
            Me.Controls.Add(Me.GroupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormHomologacionFacturacion"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Homologacion códigos de Facturación"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.FacturacionDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents EntidadDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents ProyectoDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents EsquemaDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents FacturacionDesktopDataGridView As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents EntidadFacturacionDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents EsquemaFacturacionDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents EditarButton As System.Windows.Forms.Button
        Friend WithEvents CancelarEdicionButton As System.Windows.Forms.Button
        Friend WithEvents id_Servicio As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Servicio As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Clasificacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Grupo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Genero As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Producto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Producto_Especifico As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Producto_Detalle As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Cod_Cuenta As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace