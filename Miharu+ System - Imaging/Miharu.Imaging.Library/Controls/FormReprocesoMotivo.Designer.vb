Namespace Controls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormReprocesoMotivo
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormReprocesoMotivo))
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.MotivoGroupBox = New System.Windows.Forms.GroupBox()
            Me.MotivoComboBox = New System.Windows.Forms.ComboBox()
            Me.MotivoGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(312, 83)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(89, 32)
            Me.CancelarButton.TabIndex = 5
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(184, 83)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(89, 32)
            Me.AceptarButton.TabIndex = 4
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'MotivoGroupBox
            '
            Me.MotivoGroupBox.Controls.Add(Me.MotivoComboBox)
            Me.MotivoGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MotivoGroupBox.Location = New System.Drawing.Point(12, 12)
            Me.MotivoGroupBox.Name = "MotivoGroupBox"
            Me.MotivoGroupBox.Size = New System.Drawing.Size(405, 65)
            Me.MotivoGroupBox.TabIndex = 6
            Me.MotivoGroupBox.TabStop = False
            Me.MotivoGroupBox.Text = "Motivo de reproceso"
            '
            'MotivoComboBox
            '
            Me.MotivoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.MotivoComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MotivoComboBox.FormattingEnabled = True
            Me.MotivoComboBox.Location = New System.Drawing.Point(14, 28)
            Me.MotivoComboBox.Name = "MotivoComboBox"
            Me.MotivoComboBox.Size = New System.Drawing.Size(375, 21)
            Me.MotivoComboBox.TabIndex = 1
            '
            'FormReprocesoMotivo
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(427, 126)
            Me.Controls.Add(Me.MotivoGroupBox)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormReprocesoMotivo"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Motivo Reproceso"
            Me.MotivoGroupBox.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents MotivoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents MotivoComboBox As System.Windows.Forms.ComboBox
    End Class
End Namespace