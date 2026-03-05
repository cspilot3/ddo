Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeleccionarEsquema
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSeleccionarEsquema))
            Me.entidadComboBox = New System.Windows.Forms.ComboBox()
            Me.entidadLabel = New System.Windows.Forms.Label()
            Me.proyectoComboBox = New System.Windows.Forms.ComboBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.esquemaComboBox = New System.Windows.Forms.ComboBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.closeButton = New System.Windows.Forms.Button()
            Me.okButton = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'entidadComboBox
            '
            Me.entidadComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.entidadComboBox.FormattingEnabled = True
            Me.entidadComboBox.Location = New System.Drawing.Point(12, 27)
            Me.entidadComboBox.Name = "entidadComboBox"
            Me.entidadComboBox.Size = New System.Drawing.Size(365, 21)
            Me.entidadComboBox.TabIndex = 54
            '
            'entidadLabel
            '
            Me.entidadLabel.AutoSize = True
            Me.entidadLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.entidadLabel.Location = New System.Drawing.Point(9, 10)
            Me.entidadLabel.Name = "entidadLabel"
            Me.entidadLabel.Size = New System.Drawing.Size(50, 13)
            Me.entidadLabel.TabIndex = 53
            Me.entidadLabel.Text = "Entidad"
            '
            'proyectoComboBox
            '
            Me.proyectoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.proyectoComboBox.FormattingEnabled = True
            Me.proyectoComboBox.Location = New System.Drawing.Point(12, 80)
            Me.proyectoComboBox.Name = "proyectoComboBox"
            Me.proyectoComboBox.Size = New System.Drawing.Size(365, 21)
            Me.proyectoComboBox.TabIndex = 56
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(9, 63)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(57, 13)
            Me.Label1.TabIndex = 55
            Me.Label1.Text = "Proyecto"
            '
            'esquemaComboBox
            '
            Me.esquemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.esquemaComboBox.FormattingEnabled = True
            Me.esquemaComboBox.Location = New System.Drawing.Point(12, 133)
            Me.esquemaComboBox.Name = "esquemaComboBox"
            Me.esquemaComboBox.Size = New System.Drawing.Size(365, 21)
            Me.esquemaComboBox.TabIndex = 58
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(9, 116)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(58, 13)
            Me.Label2.TabIndex = 57
            Me.Label2.Text = "Esquema"
            '
            'closeButton
            '
            Me.closeButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.closeButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.closeButton.Image = CType(resources.GetObject("closeButton.Image"), System.Drawing.Image)
            Me.closeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.closeButton.Location = New System.Drawing.Point(272, 179)
            Me.closeButton.Name = "closeButton"
            Me.closeButton.Size = New System.Drawing.Size(105, 38)
            Me.closeButton.TabIndex = 59
            Me.closeButton.Text = "&Cerrar"
            Me.closeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.closeButton.UseVisualStyleBackColor = True
            '
            'okButton
            '
            Me.okButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.okButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.okButton.Image = Global.Miharu.Desktop.My.Resources.Resources.Aceptar
            Me.okButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.okButton.Location = New System.Drawing.Point(145, 179)
            Me.okButton.Name = "okButton"
            Me.okButton.Size = New System.Drawing.Size(105, 38)
            Me.okButton.TabIndex = 60
            Me.okButton.Text = "&Aceptar"
            Me.okButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.okButton.UseVisualStyleBackColor = True
            '
            'FormSeleccionarEsquema
            '
            Me.AcceptButton = Me.okButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.closeButton
            Me.ClientSize = New System.Drawing.Size(389, 229)
            Me.Controls.Add(Me.okButton)
            Me.Controls.Add(Me.closeButton)
            Me.Controls.Add(Me.esquemaComboBox)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.proyectoComboBox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.entidadComboBox)
            Me.Controls.Add(Me.entidadLabel)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormSeleccionarEsquema"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Seleccionar Esquema"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents entidadComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents entidadLabel As System.Windows.Forms.Label
        Friend WithEvents proyectoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents esquemaComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents closeButton As System.Windows.Forms.Button
        Friend WithEvents okButton As System.Windows.Forms.Button
    End Class
End Namespace