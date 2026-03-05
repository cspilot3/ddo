Namespace Procesos.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCrearOT
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
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.TipoOTComboBox = New System.Windows.Forms.ComboBox()
            Me.lineaProcesoTextBox = New System.Windows.Forms.TextBox()
            Me.lineaProcesoLabel = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancel
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(205, 125)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 24)
            Me.CancelarButton.TabIndex = 16
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.tick
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(119, 125)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 23)
            Me.AceptarButton.TabIndex = 15
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(12, 17)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(71, 13)
            Me.Label2.TabIndex = 18
            Me.Label2.Text = "Tipo de OT"
            '
            'TipoOTComboBox
            '
            Me.TipoOTComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipoOTComboBox.FormattingEnabled = True
            Me.TipoOTComboBox.Location = New System.Drawing.Point(15, 33)
            Me.TipoOTComboBox.Name = "TipoOTComboBox"
            Me.TipoOTComboBox.Size = New System.Drawing.Size(270, 21)
            Me.TipoOTComboBox.TabIndex = 19
            '
            'lineaProcesoTextBox
            '
            Me.lineaProcesoTextBox.Location = New System.Drawing.Point(15, 87)
            Me.lineaProcesoTextBox.MaxLength = 9
            Me.lineaProcesoTextBox.Name = "lineaProcesoTextBox"
            Me.lineaProcesoTextBox.Size = New System.Drawing.Size(270, 20)
            Me.lineaProcesoTextBox.TabIndex = 20
            '
            'lineaProcesoLabel
            '
            Me.lineaProcesoLabel.AutoSize = True
            Me.lineaProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lineaProcesoLabel.Location = New System.Drawing.Point(12, 71)
            Me.lineaProcesoLabel.Name = "lineaProcesoLabel"
            Me.lineaProcesoLabel.Size = New System.Drawing.Size(107, 13)
            Me.lineaProcesoLabel.TabIndex = 21
            Me.lineaProcesoLabel.Text = "Línea de proceso"
            '
            'FormCrearOT
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(299, 158)
            Me.Controls.Add(Me.lineaProcesoLabel)
            Me.Controls.Add(Me.lineaProcesoTextBox)
            Me.Controls.Add(Me.TipoOTComboBox)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCrearOT"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Crear OT"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents TipoOTComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents lineaProcesoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents lineaProcesoLabel As System.Windows.Forms.Label
    End Class
End Namespace