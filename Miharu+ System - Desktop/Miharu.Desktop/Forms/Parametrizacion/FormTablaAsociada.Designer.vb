Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormTablaAsociada
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormTablaAsociada))
            Me.Label2 = New System.Windows.Forms.Label()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.CamposDataGridView = New System.Windows.Forms.DataGridView()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label = New System.Windows.Forms.Label()
            Me.EntidadTextBox = New System.Windows.Forms.TextBox()
            Me.DocumentoTextBox = New System.Windows.Forms.TextBox()
            Me.CampoTextBox = New System.Windows.Forms.TextBox()
            Me.fk_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Campo_Tabla = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Campo_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Campo_Lista = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo_Lista = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Es_Campo_Busqueda = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.fk_Campo_Busqueda = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo_Busqueda = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Es_Obligatorio_Campo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Length_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Es_Exportable = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Eliminado_Campo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Usa_Decimales = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Caracter_Decimal = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Cantidad_Decimales = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Valor_por_Defecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(349, 9)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(71, 13)
            Me.Label2.TabIndex = 63
            Me.Label2.Text = "Documento"
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.GuardarButton.Image = CType(resources.GetObject("GuardarButton.Image"), System.Drawing.Image)
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(15, 461)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(105, 30)
            Me.GuardarButton.TabIndex = 59
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
            Me.CerrarButton.Location = New System.Drawing.Point(825, 459)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(105, 30)
            Me.CerrarButton.TabIndex = 60
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
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
            Me.CamposDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fk_Entidad, Me.fk_Documento, Me.fk_Campo, Me.id_Campo_Tabla, Me.Nombre_Campo, Me.fk_Campo_Tipo, Me.Nombre_Campo_Tipo, Me.fk_Campo_Lista, Me.Nombre_Campo_Lista, Me.Es_Campo_Busqueda, Me.fk_Campo_Busqueda, Me.Nombre_Campo_Busqueda, Me.Es_Obligatorio_Campo, Me.Length_Campo, Me.Es_Exportable, Me.Eliminado_Campo, Me.Usa_Decimales, Me.Caracter_Decimal, Me.Cantidad_Decimales, Me.Valor_por_Defecto})
            Me.CamposDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            Me.CamposDataGridView.Location = New System.Drawing.Point(15, 51)
            Me.CamposDataGridView.MultiSelect = False
            Me.CamposDataGridView.Name = "CamposDataGridView"
            Me.CamposDataGridView.ReadOnly = True
            Me.CamposDataGridView.RowHeadersWidth = 20
            Me.CamposDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
            Me.CamposDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.CamposDataGridView.Size = New System.Drawing.Size(915, 402)
            Me.CamposDataGridView.TabIndex = 58
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(12, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(50, 13)
            Me.Label1.TabIndex = 61
            Me.Label1.Text = "Entidad"
            '
            'Label
            '
            Me.Label.AutoSize = True
            Me.Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label.Location = New System.Drawing.Point(687, 9)
            Me.Label.Name = "Label"
            Me.Label.Size = New System.Drawing.Size(45, 13)
            Me.Label.TabIndex = 69
            Me.Label.Text = "Campo"
            '
            'EntidadTextBox
            '
            Me.EntidadTextBox.Location = New System.Drawing.Point(15, 25)
            Me.EntidadTextBox.Name = "EntidadTextBox"
            Me.EntidadTextBox.ReadOnly = True
            Me.EntidadTextBox.Size = New System.Drawing.Size(240, 20)
            Me.EntidadTextBox.TabIndex = 70
            '
            'DocumentoTextBox
            '
            Me.DocumentoTextBox.Location = New System.Drawing.Point(352, 25)
            Me.DocumentoTextBox.Name = "DocumentoTextBox"
            Me.DocumentoTextBox.ReadOnly = True
            Me.DocumentoTextBox.Size = New System.Drawing.Size(240, 20)
            Me.DocumentoTextBox.TabIndex = 73
            '
            'CampoTextBox
            '
            Me.CampoTextBox.Location = New System.Drawing.Point(689, 25)
            Me.CampoTextBox.Name = "CampoTextBox"
            Me.CampoTextBox.ReadOnly = True
            Me.CampoTextBox.Size = New System.Drawing.Size(240, 20)
            Me.CampoTextBox.TabIndex = 74
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
            'fk_Campo
            '
            Me.fk_Campo.DataPropertyName = "fk_Campo"
            Me.fk_Campo.HeaderText = "fk Campo"
            Me.fk_Campo.Name = "fk_Campo"
            Me.fk_Campo.ReadOnly = True
            Me.fk_Campo.Visible = False
            Me.fk_Campo.Width = 50
            '
            'id_Campo_Tabla
            '
            Me.id_Campo_Tabla.DataPropertyName = "id_Campo_Tabla"
            Me.id_Campo_Tabla.HeaderText = "Id Campo Tabla"
            Me.id_Campo_Tabla.Name = "id_Campo_Tabla"
            Me.id_Campo_Tabla.ReadOnly = True
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
            'Nombre_Campo_Lista
            '
            Me.Nombre_Campo_Lista.DataPropertyName = "Nombre_Campo_Lista"
            Me.Nombre_Campo_Lista.HeaderText = "Campo Lista"
            Me.Nombre_Campo_Lista.Name = "Nombre_Campo_Lista"
            Me.Nombre_Campo_Lista.ReadOnly = True
            '
            'Es_Campo_Busqueda
            '
            Me.Es_Campo_Busqueda.DataPropertyName = "Es_Campo_Busqueda"
            Me.Es_Campo_Busqueda.HeaderText = "Es Campo Busqueda"
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
            'Nombre_Campo_Busqueda
            '
            Me.Nombre_Campo_Busqueda.DataPropertyName = "Nombre_Campo_Busqueda"
            Me.Nombre_Campo_Busqueda.HeaderText = "Campo Busqueda"
            Me.Nombre_Campo_Busqueda.Name = "Nombre_Campo_Busqueda"
            Me.Nombre_Campo_Busqueda.ReadOnly = True
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
            'Valor_por_Defecto
            '
            Me.Valor_por_Defecto.DataPropertyName = "Valor_por_Defecto"
            Me.Valor_por_Defecto.HeaderText = "Valor Defecto"
            Me.Valor_por_Defecto.Name = "Valor_por_Defecto"
            Me.Valor_por_Defecto.ReadOnly = True
            '
            'FormTablaAsociada
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(942, 511)
            Me.Controls.Add(Me.CampoTextBox)
            Me.Controls.Add(Me.DocumentoTextBox)
            Me.Controls.Add(Me.EntidadTextBox)
            Me.Controls.Add(Me.Label)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.CamposDataGridView)
            Me.Controls.Add(Me.Label1)
            Me.Name = "FormTablaAsociada"
            Me.Text = "Tabla Asociada"
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents CamposDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label As System.Windows.Forms.Label
        Friend WithEvents EntidadTextBox As System.Windows.Forms.TextBox
        Friend WithEvents DocumentoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CampoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents fk_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Campo_Tabla As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Campo_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Campo_Lista As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo_Lista As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Es_Campo_Busqueda As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents fk_Campo_Busqueda As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo_Busqueda As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Es_Obligatorio_Campo As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Length_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Es_Exportable As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Eliminado_Campo As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Usa_Decimales As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Caracter_Decimal As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Cantidad_Decimales As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Valor_por_Defecto As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace