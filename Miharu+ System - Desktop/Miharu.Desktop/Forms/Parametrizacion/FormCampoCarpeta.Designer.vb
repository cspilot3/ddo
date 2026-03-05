Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCampoCarpeta

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCampoCarpeta))
            Me.CamposDataGridView = New System.Windows.Forms.DataGridView()
            Me.fk_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Campo_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Length_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Length_Min_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Eliminado_Campo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.EsquemaComboBox = New System.Windows.Forms.ComboBox()
            Me.ProyectoComboBox = New System.Windows.Forms.ComboBox()
            Me.DocumentoComboBox = New System.Windows.Forms.ComboBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.EntidadComboBox = New System.Windows.Forms.ComboBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
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
            Me.CamposDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fk_Entidad, Me.fk_Documento, Me.id_Campo, Me.Nombre_Campo, Me.fk_Campo_Tipo, Me.Nombre_Campo_Tipo, Me.Length_Campo, Me.Length_Min_Campo, Me.Eliminado_Campo})
            Me.CamposDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            Me.CamposDataGridView.Location = New System.Drawing.Point(12, 111)
            Me.CamposDataGridView.MultiSelect = False
            Me.CamposDataGridView.Name = "CamposDataGridView"
            Me.CamposDataGridView.ReadOnly = True
            Me.CamposDataGridView.RowHeadersWidth = 20
            Me.CamposDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
            Me.CamposDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
            Me.CamposDataGridView.Size = New System.Drawing.Size(652, 341)
            Me.CamposDataGridView.TabIndex = 32
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
            'Eliminado_Campo
            '
            Me.Eliminado_Campo.DataPropertyName = "Eliminado_Campo"
            Me.Eliminado_Campo.HeaderText = "Eliminado"
            Me.Eliminado_Campo.Name = "Eliminado_Campo"
            Me.Eliminado_Campo.ReadOnly = True
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(455, 8)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(58, 13)
            Me.Label4.TabIndex = 57
            Me.Label4.Text = "Esquema"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(218, 8)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(57, 13)
            Me.Label3.TabIndex = 56
            Me.Label3.Text = "Proyecto"
            '
            'EsquemaComboBox
            '
            Me.EsquemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaComboBox.FormattingEnabled = True
            Me.EsquemaComboBox.Location = New System.Drawing.Point(458, 25)
            Me.EsquemaComboBox.Name = "EsquemaComboBox"
            Me.EsquemaComboBox.Size = New System.Drawing.Size(207, 21)
            Me.EsquemaComboBox.TabIndex = 55
            '
            'ProyectoComboBox
            '
            Me.ProyectoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoComboBox.FormattingEnabled = True
            Me.ProyectoComboBox.Location = New System.Drawing.Point(221, 25)
            Me.ProyectoComboBox.Name = "ProyectoComboBox"
            Me.ProyectoComboBox.Size = New System.Drawing.Size(184, 21)
            Me.ProyectoComboBox.TabIndex = 54
            '
            'DocumentoComboBox
            '
            Me.DocumentoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentoComboBox.FormattingEnabled = True
            Me.DocumentoComboBox.Location = New System.Drawing.Point(12, 76)
            Me.DocumentoComboBox.Name = "DocumentoComboBox"
            Me.DocumentoComboBox.Size = New System.Drawing.Size(653, 21)
            Me.DocumentoComboBox.TabIndex = 52
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(9, 60)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(203, 13)
            Me.Label2.TabIndex = 51
            Me.Label2.Text = "Selección Documento para filtrado"
            '
            'EntidadComboBox
            '
            Me.EntidadComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadComboBox.FormattingEnabled = True
            Me.EntidadComboBox.Location = New System.Drawing.Point(12, 25)
            Me.EntidadComboBox.Name = "EntidadComboBox"
            Me.EntidadComboBox.Size = New System.Drawing.Size(174, 21)
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
            Me.CerrarButton.Location = New System.Drawing.Point(559, 460)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(105, 30)
            Me.CerrarButton.TabIndex = 37
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormCampoCarpeta
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(676, 502)
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
            Me.Name = "FormCampoLlave"
            Me.ShowIcon = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Configuración Campos Carpeta"
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
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
        Friend WithEvents CamposDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents fk_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Campo_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Length_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Length_Min_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Eliminado_Campo As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace