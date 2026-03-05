Namespace View.Indexacion

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormAutoIndexar
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAutoIndexar))
            Me.ParametrosGroupBox = New System.Windows.Forms.GroupBox()
            Me.FolderRadioButton = New System.Windows.Forms.RadioButton()
            Me.DocumentoRadioButton = New System.Windows.Forms.RadioButton()
            Me.FoliosLabel = New System.Windows.Forms.Label()
            Me.FoliosNumericUpDown = New System.Windows.Forms.NumericUpDown()
            Me.DocumentoTextBox = New System.Windows.Forms.TextBox()
            Me.EsquemaTextBox = New System.Windows.Forms.TextBox()
            Me.DocumentoLabel = New System.Windows.Forms.Label()
            Me.EsquemaLabel = New System.Windows.Forms.Label()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.ParametrosGroupBox.SuspendLayout()
            CType(Me.FoliosNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ParametrosGroupBox
            '
            Me.ParametrosGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ParametrosGroupBox.Controls.Add(Me.FolderRadioButton)
            Me.ParametrosGroupBox.Controls.Add(Me.DocumentoRadioButton)
            Me.ParametrosGroupBox.Controls.Add(Me.FoliosLabel)
            Me.ParametrosGroupBox.Controls.Add(Me.FoliosNumericUpDown)
            Me.ParametrosGroupBox.Controls.Add(Me.DocumentoTextBox)
            Me.ParametrosGroupBox.Controls.Add(Me.EsquemaTextBox)
            Me.ParametrosGroupBox.Controls.Add(Me.DocumentoLabel)
            Me.ParametrosGroupBox.Controls.Add(Me.EsquemaLabel)
            Me.ParametrosGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ParametrosGroupBox.Location = New System.Drawing.Point(12, 12)
            Me.ParametrosGroupBox.Name = "ParametrosGroupBox"
            Me.ParametrosGroupBox.Size = New System.Drawing.Size(467, 174)
            Me.ParametrosGroupBox.TabIndex = 7
            Me.ParametrosGroupBox.TabStop = False
            Me.ParametrosGroupBox.Text = "Parámetros"
            '
            'FolderRadioButton
            '
            Me.FolderRadioButton.AutoSize = True
            Me.FolderRadioButton.Checked = True
            Me.FolderRadioButton.Location = New System.Drawing.Point(286, 142)
            Me.FolderRadioButton.Name = "FolderRadioButton"
            Me.FolderRadioButton.Size = New System.Drawing.Size(54, 17)
            Me.FolderRadioButton.TabIndex = 7
            Me.FolderRadioButton.TabStop = True
            Me.FolderRadioButton.Text = "Folder"
            Me.FolderRadioButton.UseVisualStyleBackColor = True
            '
            'DocumentoRadioButton
            '
            Me.DocumentoRadioButton.AutoSize = True
            Me.DocumentoRadioButton.Location = New System.Drawing.Point(366, 142)
            Me.DocumentoRadioButton.Name = "DocumentoRadioButton"
            Me.DocumentoRadioButton.Size = New System.Drawing.Size(80, 17)
            Me.DocumentoRadioButton.TabIndex = 6
            Me.DocumentoRadioButton.Text = "Documento"
            Me.DocumentoRadioButton.UseVisualStyleBackColor = True
            '
            'FoliosLabel
            '
            Me.FoliosLabel.AutoSize = True
            Me.FoliosLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FoliosLabel.Location = New System.Drawing.Point(17, 141)
            Me.FoliosLabel.Name = "FoliosLabel"
            Me.FoliosLabel.Size = New System.Drawing.Size(40, 13)
            Me.FoliosLabel.TabIndex = 5
            Me.FoliosLabel.Text = "Folios"
            '
            'FoliosNumericUpDown
            '
            Me.FoliosNumericUpDown.Location = New System.Drawing.Point(78, 139)
            Me.FoliosNumericUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.FoliosNumericUpDown.Name = "FoliosNumericUpDown"
            Me.FoliosNumericUpDown.Size = New System.Drawing.Size(83, 20)
            Me.FoliosNumericUpDown.TabIndex = 4
            Me.FoliosNumericUpDown.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            'DocumentoTextBox
            '
            Me.DocumentoTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DocumentoTextBox.Location = New System.Drawing.Point(20, 99)
            Me.DocumentoTextBox.Name = "DocumentoTextBox"
            Me.DocumentoTextBox.ReadOnly = True
            Me.DocumentoTextBox.Size = New System.Drawing.Size(426, 20)
            Me.DocumentoTextBox.TabIndex = 3
            '
            'EsquemaTextBox
            '
            Me.EsquemaTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EsquemaTextBox.Location = New System.Drawing.Point(20, 44)
            Me.EsquemaTextBox.Name = "EsquemaTextBox"
            Me.EsquemaTextBox.ReadOnly = True
            Me.EsquemaTextBox.Size = New System.Drawing.Size(426, 20)
            Me.EsquemaTextBox.TabIndex = 2
            '
            'DocumentoLabel
            '
            Me.DocumentoLabel.AutoSize = True
            Me.DocumentoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DocumentoLabel.Location = New System.Drawing.Point(17, 83)
            Me.DocumentoLabel.Name = "DocumentoLabel"
            Me.DocumentoLabel.Size = New System.Drawing.Size(71, 13)
            Me.DocumentoLabel.TabIndex = 1
            Me.DocumentoLabel.Text = "Documento"
            '
            'EsquemaLabel
            '
            Me.EsquemaLabel.AutoSize = True
            Me.EsquemaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EsquemaLabel.Location = New System.Drawing.Point(17, 27)
            Me.EsquemaLabel.Name = "EsquemaLabel"
            Me.EsquemaLabel.Size = New System.Drawing.Size(58, 13)
            Me.EsquemaLabel.TabIndex = 0
            Me.EsquemaLabel.Text = "Esquema"
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(369, 192)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(89, 32)
            Me.CancelarButton.TabIndex = 6
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(241, 192)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(89, 32)
            Me.AceptarButton.TabIndex = 5
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'FormAutoIndexar
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(491, 236)
            Me.Controls.Add(Me.ParametrosGroupBox)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormAutoIndexar"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Auto Indexar"
            Me.ParametrosGroupBox.ResumeLayout(False)
            Me.ParametrosGroupBox.PerformLayout()
            CType(Me.FoliosNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents ParametrosGroupBox As System.Windows.Forms.GroupBox
        Private WithEvents CancelarButton As System.Windows.Forms.Button
        Private WithEvents AceptarButton As System.Windows.Forms.Button
        Private WithEvents EsquemaLabel As System.Windows.Forms.Label
        Private WithEvents DocumentoLabel As System.Windows.Forms.Label
        Private WithEvents DocumentoTextBox As System.Windows.Forms.TextBox
        Private WithEvents EsquemaTextBox As System.Windows.Forms.TextBox
        Private WithEvents FoliosLabel As System.Windows.Forms.Label
        Private WithEvents FoliosNumericUpDown As System.Windows.Forms.NumericUpDown
        Friend WithEvents FolderRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents DocumentoRadioButton As System.Windows.Forms.RadioButton
    End Class
End Namespace