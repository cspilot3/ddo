Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCategoria
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCategoria))
            Me.entidadComboBox = New System.Windows.Forms.ComboBox()
            Me.entidadLabel = New System.Windows.Forms.Label()
            Me.addButton = New System.Windows.Forms.Button()
            Me.closeButton = New System.Windows.Forms.Button()
            Me.categoriaDataGridView = New System.Windows.Forms.DataGridView()
            Me.fkEntidadColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.idCategoriaColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.nombreCategoriaColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.editButton = New System.Windows.Forms.Button()
            CType(Me.categoriaDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'entidadComboBox
            '
            Me.entidadComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.entidadComboBox.FormattingEnabled = True
            Me.entidadComboBox.Location = New System.Drawing.Point(12, 29)
            Me.entidadComboBox.Name = "entidadComboBox"
            Me.entidadComboBox.Size = New System.Drawing.Size(283, 21)
            Me.entidadComboBox.TabIndex = 52
            '
            'entidadLabel
            '
            Me.entidadLabel.AutoSize = True
            Me.entidadLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.entidadLabel.Location = New System.Drawing.Point(9, 12)
            Me.entidadLabel.Name = "entidadLabel"
            Me.entidadLabel.Size = New System.Drawing.Size(50, 13)
            Me.entidadLabel.TabIndex = 51
            Me.entidadLabel.Text = "Entidad"
            '
            'addButton
            '
            Me.addButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.addButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.addButton.Image = CType(resources.GetObject("addButton.Image"), System.Drawing.Image)
            Me.addButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.addButton.Location = New System.Drawing.Point(12, 270)
            Me.addButton.Name = "addButton"
            Me.addButton.Size = New System.Drawing.Size(105, 38)
            Me.addButton.TabIndex = 53
            Me.addButton.Text = "&Agregar"
            Me.addButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.addButton.UseVisualStyleBackColor = True
            '
            'closeButton
            '
            Me.closeButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.closeButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.closeButton.Image = CType(resources.GetObject("closeButton.Image"), System.Drawing.Image)
            Me.closeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.closeButton.Location = New System.Drawing.Point(441, 270)
            Me.closeButton.Name = "closeButton"
            Me.closeButton.Size = New System.Drawing.Size(105, 38)
            Me.closeButton.TabIndex = 54
            Me.closeButton.Text = "&Cerrar"
            Me.closeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.closeButton.UseVisualStyleBackColor = True
            '
            'categoriaDataGridView
            '
            Me.categoriaDataGridView.AllowUserToAddRows = False
            Me.categoriaDataGridView.AllowUserToDeleteRows = False
            Me.categoriaDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.categoriaDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fkEntidadColumn, Me.idCategoriaColumn, Me.nombreCategoriaColumn})
            Me.categoriaDataGridView.Location = New System.Drawing.Point(12, 57)
            Me.categoriaDataGridView.Name = "categoriaDataGridView"
            Me.categoriaDataGridView.ReadOnly = True
            Me.categoriaDataGridView.RowHeadersWidth = 20
            Me.categoriaDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.categoriaDataGridView.Size = New System.Drawing.Size(534, 203)
            Me.categoriaDataGridView.TabIndex = 55
            '
            'fkEntidadColumn
            '
            Me.fkEntidadColumn.DataPropertyName = "fk_Entidad"
            Me.fkEntidadColumn.HeaderText = "Entidad"
            Me.fkEntidadColumn.Name = "fkEntidadColumn"
            Me.fkEntidadColumn.ReadOnly = True
            Me.fkEntidadColumn.Visible = False
            '
            'idCategoriaColumn
            '
            Me.idCategoriaColumn.DataPropertyName = "id_Categoria"
            Me.idCategoriaColumn.HeaderText = "id"
            Me.idCategoriaColumn.Name = "idCategoriaColumn"
            Me.idCategoriaColumn.ReadOnly = True
            Me.idCategoriaColumn.Width = 50
            '
            'nombreCategoriaColumn
            '
            Me.nombreCategoriaColumn.DataPropertyName = "Nombre_Categoria"
            Me.nombreCategoriaColumn.HeaderText = "Categoría"
            Me.nombreCategoriaColumn.Name = "nombreCategoriaColumn"
            Me.nombreCategoriaColumn.ReadOnly = True
            Me.nombreCategoriaColumn.Width = 400
            '
            'editButton
            '
            Me.editButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.editButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.editButton.Image = Global.Miharu.Desktop.My.Resources.Resources.Edit
            Me.editButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.editButton.Location = New System.Drawing.Point(123, 270)
            Me.editButton.Name = "editButton"
            Me.editButton.Size = New System.Drawing.Size(105, 38)
            Me.editButton.TabIndex = 56
            Me.editButton.Text = "&Editar"
            Me.editButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.editButton.UseVisualStyleBackColor = True
            '
            'FormCategoria
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.closeButton
            Me.ClientSize = New System.Drawing.Size(558, 312)
            Me.Controls.Add(Me.editButton)
            Me.Controls.Add(Me.categoriaDataGridView)
            Me.Controls.Add(Me.addButton)
            Me.Controls.Add(Me.closeButton)
            Me.Controls.Add(Me.entidadComboBox)
            Me.Controls.Add(Me.entidadLabel)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCategoria"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Categoría"
            CType(Me.categoriaDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents entidadComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents entidadLabel As System.Windows.Forms.Label
        Friend WithEvents addButton As System.Windows.Forms.Button
        Friend WithEvents closeButton As System.Windows.Forms.Button
        Friend WithEvents categoriaDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents fkEntidadColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents idCategoriaColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents nombreCategoriaColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents editButton As System.Windows.Forms.Button
    End Class
End Namespace