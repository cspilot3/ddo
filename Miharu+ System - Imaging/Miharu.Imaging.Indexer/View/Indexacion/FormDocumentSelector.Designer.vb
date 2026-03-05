Namespace View.Indexacion

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormDocumentSelector
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDocumentSelector))
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.TipoDocumentalLabel = New System.Windows.Forms.Label()
            Me.FindListBox = New System.Windows.Forms.ListBox()
            Me.FindTextBox = New System.Windows.Forms.TextBox()
            Me.EsquemaComboBox = New System.Windows.Forms.ComboBox()
            Me.EsquemaLabel = New System.Windows.Forms.Label()
            Me.TodosButton = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GuardarButton
            '
            Me.GuardarButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.tick
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(221, 337)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(90, 30)
            Me.GuardarButton.TabIndex = 14
            Me.GuardarButton.Text = "&Seleccionar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(317, 337)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 15
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.TipoDocumentalLabel)
            Me.GroupBox1.Controls.Add(Me.FindListBox)
            Me.GroupBox1.Controls.Add(Me.FindTextBox)
            Me.GroupBox1.Controls.Add(Me.EsquemaComboBox)
            Me.GroupBox1.Controls.Add(Me.EsquemaLabel)
            Me.GroupBox1.Location = New System.Drawing.Point(10, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(414, 319)
            Me.GroupBox1.TabIndex = 13
            Me.GroupBox1.TabStop = False
            '
            'TipoDocumentalLabel
            '
            Me.TipoDocumentalLabel.AutoSize = True
            Me.TipoDocumentalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TipoDocumentalLabel.Location = New System.Drawing.Point(13, 67)
            Me.TipoDocumentalLabel.Name = "TipoDocumentalLabel"
            Me.TipoDocumentalLabel.Size = New System.Drawing.Size(101, 13)
            Me.TipoDocumentalLabel.TabIndex = 4
            Me.TipoDocumentalLabel.Text = "Tipo documental"
            '
            'FindListBox
            '
            Me.FindListBox.FormattingEnabled = True
            Me.FindListBox.Location = New System.Drawing.Point(16, 106)
            Me.FindListBox.Name = "FindListBox"
            Me.FindListBox.Size = New System.Drawing.Size(381, 199)
            Me.FindListBox.TabIndex = 3
            '
            'FindTextBox
            '
            Me.FindTextBox.Location = New System.Drawing.Point(16, 86)
            Me.FindTextBox.Name = "FindTextBox"
            Me.FindTextBox.Size = New System.Drawing.Size(381, 20)
            Me.FindTextBox.TabIndex = 2
            '
            'EsquemaComboBox
            '
            Me.EsquemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaComboBox.FormattingEnabled = True
            Me.EsquemaComboBox.Location = New System.Drawing.Point(16, 33)
            Me.EsquemaComboBox.Name = "EsquemaComboBox"
            Me.EsquemaComboBox.Size = New System.Drawing.Size(381, 21)
            Me.EsquemaComboBox.TabIndex = 1
            '
            'EsquemaLabel
            '
            Me.EsquemaLabel.AutoSize = True
            Me.EsquemaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EsquemaLabel.Location = New System.Drawing.Point(13, 17)
            Me.EsquemaLabel.Name = "EsquemaLabel"
            Me.EsquemaLabel.Size = New System.Drawing.Size(58, 13)
            Me.EsquemaLabel.TabIndex = 0
            Me.EsquemaLabel.Text = "Esquema"
            '
            'TodosButton
            '
            Me.TodosButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.layout_content
            Me.TodosButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.TodosButton.Location = New System.Drawing.Point(26, 334)
            Me.TodosButton.Name = "TodosButton"
            Me.TodosButton.Size = New System.Drawing.Size(90, 30)
            Me.TodosButton.TabIndex = 16
            Me.TodosButton.Text = "&Todos"
            Me.TodosButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.TodosButton.UseVisualStyleBackColor = True
            '
            'FormDocumentSelector
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(437, 376)
            Me.Controls.Add(Me.TodosButton)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.GroupBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormDocumentSelector"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Validaciones opcionales"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents EsquemaComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents EsquemaLabel As System.Windows.Forms.Label
        Friend WithEvents TipoDocumentalLabel As System.Windows.Forms.Label
        Friend WithEvents FindListBox As System.Windows.Forms.ListBox
        Friend WithEvents FindTextBox As System.Windows.Forms.TextBox
        Friend WithEvents TodosButton As System.Windows.Forms.Button
    End Class

End Namespace