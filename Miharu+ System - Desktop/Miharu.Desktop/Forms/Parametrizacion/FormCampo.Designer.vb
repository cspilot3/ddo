Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParametrizacionCampos
        Inherits System.Windows.Forms.Form

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormParametrizacionCampos))
            Me.CamposDataGridView = New System.Windows.Forms.DataGridView()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.EsquemaComboBox = New System.Windows.Forms.ComboBox()
            Me.ProyectoComboBox = New System.Windows.Forms.ComboBox()
            Me.DocumentoComboBox = New System.Windows.Forms.ComboBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.EntidadComboBox = New System.Windows.Forms.ComboBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.fk_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Campo_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Campo_Lista = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.addTablaAsociada = New System.Windows.Forms.DataGridViewImageColumn()
            Me.Es_Campo_Busqueda = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.fk_Campo_Busqueda = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Es_Campo_Folder = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Es_Obligatorio_Campo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Length_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Length_Min_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Es_Exportable = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Eliminado_Campo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Usa_Decimales = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Caracter_Decimal = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Cantidad_Decimales = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Body_Query = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Validar_Registros = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Validar_Totales = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Valor_por_Defecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Campo_Control_Duplicado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'CamposDataGridView
            '
            Me.CamposDataGridView.AllowUserToAddRows = False
            Me.CamposDataGridView.AllowUserToDeleteRows = False
            Me.CamposDataGridView.AllowUserToResizeColumns = False
            Me.CamposDataGridView.AllowUserToResizeRows = False
            Me.CamposDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CamposDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
            Me.CamposDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CamposDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fk_Entidad, Me.fk_Documento, Me.id_Campo, Me.Nombre_Campo, Me.fk_Campo_Tipo, Me.Nombre_Campo_Tipo, Me.fk_Campo_Lista, Me.addTablaAsociada, Me.Es_Campo_Busqueda, Me.fk_Campo_Busqueda, Me.Es_Campo_Folder, Me.Es_Obligatorio_Campo, Me.Length_Campo, Me.Length_Min_Campo, Me.Es_Exportable, Me.Eliminado_Campo, Me.Usa_Decimales, Me.Caracter_Decimal, Me.Cantidad_Decimales, Me.Body_Query, Me.Validar_Registros, Me.Validar_Totales, Me.Valor_por_Defecto, Me.Campo_Control_Duplicado})
            Me.CamposDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            Me.CamposDataGridView.Location = New System.Drawing.Point(12, 111)
            Me.CamposDataGridView.MultiSelect = False
            Me.CamposDataGridView.Name = "CamposDataGridView"
            Me.CamposDataGridView.ReadOnly = True
            Me.CamposDataGridView.RowHeadersWidth = 20
            Me.CamposDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
            Me.CamposDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
            Me.CamposDataGridView.Size = New System.Drawing.Size(915, 341)
            Me.CamposDataGridView.TabIndex = 32
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.GuardarButton.Image = CType(resources.GetObject("GuardarButton.Image"), System.Drawing.Image)
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(12, 460)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(105, 30)
            Me.GuardarButton.TabIndex = 36
            Me.GuardarButton.Text = "&Agregar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CerrarButton.Image = CType(resources.GetObject("CerrarButton.Image"), System.Drawing.Image)
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(822, 460)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(105, 30)
            Me.CerrarButton.TabIndex = 37
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(641, 8)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(58, 13)
            Me.Label4.TabIndex = 57
            Me.Label4.Text = "Esquema"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(325, 8)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(57, 13)
            Me.Label3.TabIndex = 56
            Me.Label3.Text = "Proyecto"
            '
            'EsquemaComboBox
            '
            Me.EsquemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaComboBox.FormattingEnabled = True
            Me.EsquemaComboBox.Location = New System.Drawing.Point(644, 25)
            Me.EsquemaComboBox.Name = "EsquemaComboBox"
            Me.EsquemaComboBox.Size = New System.Drawing.Size(283, 21)
            Me.EsquemaComboBox.TabIndex = 55
            '
            'ProyectoComboBox
            '
            Me.ProyectoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoComboBox.FormattingEnabled = True
            Me.ProyectoComboBox.Location = New System.Drawing.Point(328, 25)
            Me.ProyectoComboBox.Name = "ProyectoComboBox"
            Me.ProyectoComboBox.Size = New System.Drawing.Size(283, 21)
            Me.ProyectoComboBox.TabIndex = 54
            '
            'DocumentoComboBox
            '
            Me.DocumentoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentoComboBox.FormattingEnabled = True
            Me.DocumentoComboBox.Location = New System.Drawing.Point(12, 76)
            Me.DocumentoComboBox.Name = "DocumentoComboBox"
            Me.DocumentoComboBox.Size = New System.Drawing.Size(915, 21)
            Me.DocumentoComboBox.TabIndex = 52
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(9, 60)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(71, 13)
            Me.Label2.TabIndex = 51
            Me.Label2.Text = "Documento"
            '
            'EntidadComboBox
            '
            Me.EntidadComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadComboBox.FormattingEnabled = True
            Me.EntidadComboBox.Location = New System.Drawing.Point(12, 25)
            Me.EntidadComboBox.Name = "EntidadComboBox"
            Me.EntidadComboBox.Size = New System.Drawing.Size(283, 21)
            Me.EntidadComboBox.TabIndex = 50
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(9, 8)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(50, 13)
            Me.Label1.TabIndex = 49
            Me.Label1.Text = "Entidad"
            '
            'fk_Entidad
            '
            Me.fk_Entidad.DataPropertyName = "fk_Entidad"
            Me.fk_Entidad.HeaderText = "fk_Entidad"
            Me.fk_Entidad.Name = "fk_Entidad"
            Me.fk_Entidad.ReadOnly = True
            Me.fk_Entidad.Visible = False
            '
            'fk_Documento
            '
            Me.fk_Documento.DataPropertyName = "fk_Documento"
            Me.fk_Documento.HeaderText = "fk_Documento"
            Me.fk_Documento.Name = "fk_Documento"
            Me.fk_Documento.ReadOnly = True
            Me.fk_Documento.Visible = False
            '
            'id_Campo
            '
            Me.id_Campo.DataPropertyName = "id_Campo"
            Me.id_Campo.HeaderText = "Id Campo"
            Me.id_Campo.Name = "id_Campo"
            Me.id_Campo.ReadOnly = True
            Me.id_Campo.Width = 50
            '
            'Nombre_Campo
            '
            Me.Nombre_Campo.DataPropertyName = "Nombre_Campo"
            Me.Nombre_Campo.HeaderText = "Nombre Campo"
            Me.Nombre_Campo.Name = "Nombre_Campo"
            Me.Nombre_Campo.ReadOnly = True
            '
            'fk_Campo_Tipo
            '
            Me.fk_Campo_Tipo.DataPropertyName = "fk_Campo_Tipo"
            Me.fk_Campo_Tipo.HeaderText = "Campo Tipo"
            Me.fk_Campo_Tipo.Name = "fk_Campo_Tipo"
            Me.fk_Campo_Tipo.ReadOnly = True
            Me.fk_Campo_Tipo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.fk_Campo_Tipo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            Me.fk_Campo_Tipo.Visible = False
            '
            'Nombre_Campo_Tipo
            '
            Me.Nombre_Campo_Tipo.DataPropertyName = "Nombre_Campo_Tipo"
            Me.Nombre_Campo_Tipo.HeaderText = "Campo Tipo"
            Me.Nombre_Campo_Tipo.Name = "Nombre_Campo_Tipo"
            Me.Nombre_Campo_Tipo.ReadOnly = True
            '
            'fk_Campo_Lista
            '
            Me.fk_Campo_Lista.DataPropertyName = "fk_Campo_Lista"
            Me.fk_Campo_Lista.HeaderText = "Campo Lista"
            Me.fk_Campo_Lista.Name = "fk_Campo_Lista"
            Me.fk_Campo_Lista.ReadOnly = True
            Me.fk_Campo_Lista.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.fk_Campo_Lista.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            Me.fk_Campo_Lista.Visible = False
            '
            'addTablaAsociada
            '
            Me.addTablaAsociada.HeaderText = "Tabla Asociada"
            Me.addTablaAsociada.Image = Global.Miharu.Desktop.My.Resources.Resources.WindowsMain
            Me.addTablaAsociada.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
            Me.addTablaAsociada.Name = "addTablaAsociada"
            Me.addTablaAsociada.ReadOnly = True
            '
            'Es_Campo_Busqueda
            '
            Me.Es_Campo_Busqueda.DataPropertyName = "Es_Campo_Busqueda"
            Me.Es_Campo_Busqueda.HeaderText = "Campo Busqueda"
            Me.Es_Campo_Busqueda.Name = "Es_Campo_Busqueda"
            Me.Es_Campo_Busqueda.ReadOnly = True
            Me.Es_Campo_Busqueda.Width = 80
            '
            'fk_Campo_Busqueda
            '
            Me.fk_Campo_Busqueda.DataPropertyName = "fk_Campo_Busqueda"
            Me.fk_Campo_Busqueda.HeaderText = "Campo Busqueda"
            Me.fk_Campo_Busqueda.Name = "fk_Campo_Busqueda"
            Me.fk_Campo_Busqueda.ReadOnly = True
            Me.fk_Campo_Busqueda.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.fk_Campo_Busqueda.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            Me.fk_Campo_Busqueda.Visible = False
            '
            'Es_Campo_Folder
            '
            Me.Es_Campo_Folder.DataPropertyName = "Es_Campo_Folder"
            Me.Es_Campo_Folder.HeaderText = "Campo Folder"
            Me.Es_Campo_Folder.Name = "Es_Campo_Folder"
            Me.Es_Campo_Folder.ReadOnly = True
            Me.Es_Campo_Folder.Width = 80
            '
            'Es_Obligatorio_Campo
            '
            Me.Es_Obligatorio_Campo.DataPropertyName = "Es_Obligatorio_Campo"
            Me.Es_Obligatorio_Campo.HeaderText = "Obligatorio"
            Me.Es_Obligatorio_Campo.Name = "Es_Obligatorio_Campo"
            Me.Es_Obligatorio_Campo.ReadOnly = True
            '
            'Length_Campo
            '
            Me.Length_Campo.DataPropertyName = "Length_Campo"
            Me.Length_Campo.HeaderText = "Longitud Campo"
            Me.Length_Campo.Name = "Length_Campo"
            Me.Length_Campo.ReadOnly = True
            '
            'Length_Min_Campo
            '
            Me.Length_Min_Campo.DataPropertyName = "Length_Min_Campo"
            Me.Length_Min_Campo.HeaderText = "Longitud Minima Campo"
            Me.Length_Min_Campo.Name = "Length_Min_Campo"
            Me.Length_Min_Campo.ReadOnly = True
            '
            'Es_Exportable
            '
            Me.Es_Exportable.DataPropertyName = "Es_Exportable"
            Me.Es_Exportable.HeaderText = "Exportable"
            Me.Es_Exportable.Name = "Es_Exportable"
            Me.Es_Exportable.ReadOnly = True
            '
            'Eliminado_Campo
            '
            Me.Eliminado_Campo.DataPropertyName = "Eliminado_Campo"
            Me.Eliminado_Campo.HeaderText = "Eliminado"
            Me.Eliminado_Campo.Name = "Eliminado_Campo"
            Me.Eliminado_Campo.ReadOnly = True
            '
            'Usa_Decimales
            '
            Me.Usa_Decimales.DataPropertyName = "Usa_Decimales"
            Me.Usa_Decimales.HeaderText = "Usa Decimales"
            Me.Usa_Decimales.Name = "Usa_Decimales"
            Me.Usa_Decimales.ReadOnly = True
            '
            'Caracter_Decimal
            '
            Me.Caracter_Decimal.DataPropertyName = "Caracter_Decimal"
            Me.Caracter_Decimal.HeaderText = "Caracter Decimal"
            Me.Caracter_Decimal.Name = "Caracter_Decimal"
            Me.Caracter_Decimal.ReadOnly = True
            '
            'Cantidad_Decimales
            '
            Me.Cantidad_Decimales.DataPropertyName = "Cantidad_Decimales"
            Me.Cantidad_Decimales.HeaderText = "Cantidad Decimales"
            Me.Cantidad_Decimales.Name = "Cantidad_Decimales"
            Me.Cantidad_Decimales.ReadOnly = True
            '
            'Body_Query
            '
            Me.Body_Query.DataPropertyName = "Body_Query"
            Me.Body_Query.HeaderText = "Body Query"
            Me.Body_Query.Name = "Body_Query"
            Me.Body_Query.ReadOnly = True
            Me.Body_Query.Visible = False
            '
            'Validar_Registros
            '
            Me.Validar_Registros.DataPropertyName = "Validar_Registros"
            Me.Validar_Registros.HeaderText = "Validar Registros"
            Me.Validar_Registros.Name = "Validar_Registros"
            Me.Validar_Registros.ReadOnly = True
            '
            'Validar_Totales
            '
            Me.Validar_Totales.DataPropertyName = "Validar_Totales"
            Me.Validar_Totales.HeaderText = "Validar Totales"
            Me.Validar_Totales.Name = "Validar_Totales"
            Me.Validar_Totales.ReadOnly = True
            '
            'Valor_por_Defecto
            '
            Me.Valor_por_Defecto.DataPropertyName = "Valor_por_Defecto"
            Me.Valor_por_Defecto.HeaderText = "Valor Defecto"
            Me.Valor_por_Defecto.Name = "Valor_por_Defecto"
            Me.Valor_por_Defecto.ReadOnly = True
            '
            'Campo_Control_Duplicado
            '
            Me.Campo_Control_Duplicado.DataPropertyName = "Campo_Control_Duplicado"
            Me.Campo_Control_Duplicado.HeaderText = "Campo Control Duplicado"
            Me.Campo_Control_Duplicado.Name = "Campo_Control_Duplicado"
            Me.Campo_Control_Duplicado.ReadOnly = True
            Me.Campo_Control_Duplicado.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            '
            'FormParametrizacionCampos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(939, 502)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.EsquemaComboBox)
            Me.Controls.Add(Me.ProyectoComboBox)
            Me.Controls.Add(Me.DocumentoComboBox)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.EntidadComboBox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.CamposDataGridView)
            Me.Name = "FormParametrizacionCampos"
            Me.ShowIcon = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Configuración Campos"
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents CamposDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents EsquemaComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents ProyectoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents DocumentoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents EntidadComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents fk_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Campo_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Campo_Lista As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents addTablaAsociada As System.Windows.Forms.DataGridViewImageColumn
        Friend WithEvents Es_Campo_Busqueda As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents fk_Campo_Busqueda As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Es_Campo_Folder As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Es_Obligatorio_Campo As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Length_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Length_Min_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Es_Exportable As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Eliminado_Campo As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Usa_Decimales As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Caracter_Decimal As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Cantidad_Decimales As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Body_Query As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Validar_Registros As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Validar_Totales As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Valor_por_Defecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Campo_Control_Duplicado As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace