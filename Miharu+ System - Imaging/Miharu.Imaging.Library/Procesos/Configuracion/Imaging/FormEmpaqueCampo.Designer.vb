Namespace Procesos.Configuracion.Imaging

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEmpaqueCampo
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
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.CamposDataGridView = New System.Windows.Forms.DataGridView()
            Me.fk_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Campo_Tipo = New System.Windows.Forms.DataGridViewComboBoxColumn()
            Me.fk_Campo_Lista = New System.Windows.Forms.DataGridViewComboBoxColumn()
            Me.Es_Obligatorio_Campo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Length_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Length_Min_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Usa_Decimales = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Cantidad_Decimales = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Eliminado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.sheet_add
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(829, 382)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(122, 30)
            Me.GuardarButton.TabIndex = 37
            Me.GuardarButton.Text = "&Agregar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(960, 382)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(122, 30)
            Me.CerrarButton.TabIndex = 38
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Button1
            '
            Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button1.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGuardar
            Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Button1.Location = New System.Drawing.Point(829, 382)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(122, 30)
            Me.Button1.TabIndex = 39
            Me.Button1.Text = "&Guardar"
            Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.Button1.UseVisualStyleBackColor = True
            Me.Button1.Visible = False
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
            Me.CamposDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fk_Entidad, Me.fk_Proyecto, Me.id_Campo, Me.Nombre_Campo, Me.fk_Campo_Tipo, Me.fk_Campo_Lista, Me.Es_Obligatorio_Campo, Me.Length_Campo, Me.Length_Min_Campo, Me.Usa_Decimales, Me.Cantidad_Decimales, Me.Eliminado})
            Me.CamposDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            Me.CamposDataGridView.Location = New System.Drawing.Point(14, 12)
            Me.CamposDataGridView.MultiSelect = False
            Me.CamposDataGridView.Name = "CamposDataGridView"
            Me.CamposDataGridView.ReadOnly = True
            Me.CamposDataGridView.RowHeadersWidth = 20
            Me.CamposDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
            Me.CamposDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
            Me.CamposDataGridView.Size = New System.Drawing.Size(1069, 364)
            Me.CamposDataGridView.TabIndex = 36
            '
            'fk_Entidad
            '
            Me.fk_Entidad.DataPropertyName = "fk_Entidad"
            Me.fk_Entidad.HeaderText = "fk_Entidad"
            Me.fk_Entidad.Name = "fk_Entidad"
            Me.fk_Entidad.ReadOnly = True
            Me.fk_Entidad.Visible = False
            '
            'fk_Proyecto
            '
            Me.fk_Proyecto.DataPropertyName = "fk_Proyecto"
            Me.fk_Proyecto.HeaderText = "fk_Proyecto"
            Me.fk_Proyecto.Name = "fk_Proyecto"
            Me.fk_Proyecto.ReadOnly = True
            Me.fk_Proyecto.Visible = False
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
            Me.fk_Campo_Tipo.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
            Me.fk_Campo_Tipo.HeaderText = "Campo Tipo"
            Me.fk_Campo_Tipo.Name = "fk_Campo_Tipo"
            Me.fk_Campo_Tipo.ReadOnly = True
            '
            'fk_Campo_Lista
            '
            Me.fk_Campo_Lista.DataPropertyName = "fk_Campo_Lista"
            Me.fk_Campo_Lista.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
            Me.fk_Campo_Lista.HeaderText = "Campo Lista"
            Me.fk_Campo_Lista.Name = "fk_Campo_Lista"
            Me.fk_Campo_Lista.ReadOnly = True
            '
            'Es_Obligatorio_Campo
            '
            Me.Es_Obligatorio_Campo.DataPropertyName = "Es_Obligatorio_Campo"
            Me.Es_Obligatorio_Campo.HeaderText = "Obligatorio"
            Me.Es_Obligatorio_Campo.Name = "Es_Obligatorio_Campo"
            Me.Es_Obligatorio_Campo.ReadOnly = True
            Me.Es_Obligatorio_Campo.Width = 80
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
            'Usa_Decimales
            '
            Me.Usa_Decimales.DataPropertyName = "Usa_Decimales"
            Me.Usa_Decimales.HeaderText = "Usa Decimales"
            Me.Usa_Decimales.Name = "Usa_Decimales"
            Me.Usa_Decimales.ReadOnly = True
            Me.Usa_Decimales.Width = 80
            '
            'Cantidad_Decimales
            '
            Me.Cantidad_Decimales.DataPropertyName = "Cantidad_Decimales"
            Me.Cantidad_Decimales.HeaderText = "Cantidad Decimales"
            Me.Cantidad_Decimales.Name = "Cantidad_Decimales"
            Me.Cantidad_Decimales.ReadOnly = True
            '
            'Eliminado
            '
            Me.Eliminado.DataPropertyName = "Eliminado"
            Me.Eliminado.HeaderText = "Eliminado"
            Me.Eliminado.Name = "Eliminado"
            Me.Eliminado.ReadOnly = True
            Me.Eliminado.Width = 70
            '
            'FormEmpaqueCampo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1097, 424)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Button1)
            Me.Controls.Add(Me.CamposDataGridView)
            Me.Name = "FormEmpaqueCampo"
            Me.ShowIcon = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Campos Empaque"
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Friend WithEvents CamposDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents fk_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Campo_Tipo As System.Windows.Forms.DataGridViewComboBoxColumn
        Friend WithEvents fk_Campo_Lista As System.Windows.Forms.DataGridViewComboBoxColumn
        Friend WithEvents Es_Obligatorio_Campo As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Length_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Length_Min_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Usa_Decimales As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Cantidad_Decimales As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Eliminado As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class

End Namespace