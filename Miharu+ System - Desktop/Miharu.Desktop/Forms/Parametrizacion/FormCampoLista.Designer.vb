Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCampoLista
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCampoLista))
            Me.CampoListaDataGridView = New System.Windows.Forms.DataGridView()
            Me.id_Campo_Lista = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo_Lista = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EntidadComboBox = New System.Windows.Forms.ComboBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.NuevoCampoListaButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.EliminarCampoListaButton = New System.Windows.Forms.Button()
            CType(Me.CampoListaDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'CampoListaDataGridView
            '
            Me.CampoListaDataGridView.AllowUserToAddRows = False
            Me.CampoListaDataGridView.AllowUserToDeleteRows = False
            Me.CampoListaDataGridView.AllowUserToOrderColumns = True
            Me.CampoListaDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                       Or System.Windows.Forms.AnchorStyles.Left) _
                                                      Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CampoListaDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CampoListaDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_Campo_Lista, Me.Nombre_Campo_Lista})
            Me.CampoListaDataGridView.Location = New System.Drawing.Point(12, 53)
            Me.CampoListaDataGridView.MultiSelect = False
            Me.CampoListaDataGridView.Name = "CampoListaDataGridView"
            Me.CampoListaDataGridView.ReadOnly = True
            Me.CampoListaDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.CampoListaDataGridView.Size = New System.Drawing.Size(394, 399)
            Me.CampoListaDataGridView.TabIndex = 39
            '
            'id_Campo_Lista
            '
            Me.id_Campo_Lista.DataPropertyName = "id_Campo_Lista"
            Me.id_Campo_Lista.HeaderText = "Id Campo Lista"
            Me.id_Campo_Lista.Name = "id_Campo_Lista"
            Me.id_Campo_Lista.ReadOnly = True
            '
            'Nombre_Campo_Lista
            '
            Me.Nombre_Campo_Lista.DataPropertyName = "Nombre_Campo_Lista"
            Me.Nombre_Campo_Lista.HeaderText = "Campo Lista"
            Me.Nombre_Campo_Lista.Name = "Nombre_Campo_Lista"
            Me.Nombre_Campo_Lista.ReadOnly = True
            Me.Nombre_Campo_Lista.Width = 250
            '
            'EntidadComboBox
            '
            Me.EntidadComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadComboBox.FormattingEnabled = True
            Me.EntidadComboBox.Location = New System.Drawing.Point(15, 26)
            Me.EntidadComboBox.Name = "EntidadComboBox"
            Me.EntidadComboBox.Size = New System.Drawing.Size(207, 21)
            Me.EntidadComboBox.TabIndex = 43
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(12, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(50, 13)
            Me.Label1.TabIndex = 42
            Me.Label1.Text = "Entidad"
            '
            'NuevoCampoListaButton
            '
            Me.NuevoCampoListaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.NuevoCampoListaButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NuevoCampoListaButton.Image = CType(resources.GetObject("NuevoCampoListaButton.Image"), System.Drawing.Image)
            Me.NuevoCampoListaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.NuevoCampoListaButton.Location = New System.Drawing.Point(12, 458)
            Me.NuevoCampoListaButton.Name = "NuevoCampoListaButton"
            Me.NuevoCampoListaButton.Size = New System.Drawing.Size(145, 33)
            Me.NuevoCampoListaButton.TabIndex = 41
            Me.NuevoCampoListaButton.Text = "Nuevo Campo Lista"
            Me.NuevoCampoListaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.NuevoCampoListaButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = CType(resources.GetObject("CerrarButton.Image"), System.Drawing.Image)
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(318, 457)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 33)
            Me.CerrarButton.TabIndex = 40
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'EliminarCampoListaButton
            '
            Me.EliminarCampoListaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.EliminarCampoListaButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EliminarCampoListaButton.Image = Global.Miharu.Desktop.My.Resources.Resources.Cancelar
            Me.EliminarCampoListaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.EliminarCampoListaButton.Location = New System.Drawing.Point(163, 457)
            Me.EliminarCampoListaButton.Name = "EliminarCampoListaButton"
            Me.EliminarCampoListaButton.Size = New System.Drawing.Size(145, 33)
            Me.EliminarCampoListaButton.TabIndex = 45
            Me.EliminarCampoListaButton.Text = "Eliminar Campo"
            Me.EliminarCampoListaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.EliminarCampoListaButton.UseVisualStyleBackColor = True
            '
            'FormCampoLista
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(423, 498)
            Me.Controls.Add(Me.EliminarCampoListaButton)
            Me.Controls.Add(Me.EntidadComboBox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.NuevoCampoListaButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.CampoListaDataGridView)
            Me.Name = "FormCampoLista"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Campos Lista"
            CType(Me.CampoListaDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents NuevoCampoListaButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents CampoListaDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents id_Campo_Lista As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo_Lista As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EntidadComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents EliminarCampoListaButton As System.Windows.Forms.Button
    End Class
End Namespace