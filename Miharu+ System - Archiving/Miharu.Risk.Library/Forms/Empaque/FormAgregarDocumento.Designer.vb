Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.Empaque
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormAgregarDocumento
        Inherits Miharu.Desktop.Library.FormBase

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
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.cbarrasDesktopCBarrasControl = New Miharu.Desktop.Controls.DesktopCBarras.DesktopCBarrasControl()
            Me.FileLabel = New System.Windows.Forms.Label()
            Me.CarpetaLabel = New System.Windows.Forms.Label()
            Me.LblInformativo = New System.Windows.Forms.Label()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.CancelarButton)
            Me.GroupBox1.Controls.Add(Me.AceptarButton)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.cbarrasDesktopCBarrasControl)
            Me.GroupBox1.Location = New System.Drawing.Point(14, 10)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(327, 127)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CancelarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(222, 80)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(93, 32)
            Me.CancelarButton.TabIndex = 1
            Me.CancelarButton.Text = "&Cerrar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(123, 80)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(93, 32)
            Me.AceptarButton.TabIndex = 0
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(12, 25)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(186, 13)
            Me.Label1.TabIndex = 2
            Me.Label1.Text = "Codigo de Barras de documento"
            '
            'cbarrasDesktopCBarrasControl
            '
            Me.cbarrasDesktopCBarrasControl.FocusIn = System.Drawing.Color.LightYellow
            Me.cbarrasDesktopCBarrasControl.FocusOut = System.Drawing.Color.White
            Me.cbarrasDesktopCBarrasControl.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cbarrasDesktopCBarrasControl.Location = New System.Drawing.Point(15, 49)
            Me.cbarrasDesktopCBarrasControl.MaxLength = 0
            Me.cbarrasDesktopCBarrasControl.Name = "cbarrasDesktopCBarrasControl"
            Me.cbarrasDesktopCBarrasControl.Permite_Pegar = False
            Me.cbarrasDesktopCBarrasControl.ShortcutsEnabled = False
            Me.cbarrasDesktopCBarrasControl.Size = New System.Drawing.Size(299, 20)
            Me.cbarrasDesktopCBarrasControl.TabIndex = 3
            '
            'FileLabel
            '
            Me.FileLabel.AutoSize = True
            Me.FileLabel.ForeColor = System.Drawing.SystemColors.ControlText
            Me.FileLabel.Location = New System.Drawing.Point(207, 164)
            Me.FileLabel.Name = "FileLabel"
            Me.FileLabel.Size = New System.Drawing.Size(14, 13)
            Me.FileLabel.TabIndex = 2
            Me.FileLabel.Text = "_"
            '
            'CarpetaLabel
            '
            Me.CarpetaLabel.AutoSize = True
            Me.CarpetaLabel.ForeColor = System.Drawing.Color.Green
            Me.CarpetaLabel.Location = New System.Drawing.Point(26, 164)
            Me.CarpetaLabel.Name = "CarpetaLabel"
            Me.CarpetaLabel.Size = New System.Drawing.Size(173, 13)
            Me.CarpetaLabel.TabIndex = 1
            Me.CarpetaLabel.Text = "Ultima Documento Empacado"
            '
            'LblInformativo
            '
            Me.LblInformativo.Location = New System.Drawing.Point(357, 23)
            Me.LblInformativo.Name = "LblInformativo"
            Me.LblInformativo.Size = New System.Drawing.Size(165, 99)
            Me.LblInformativo.TabIndex = 3
            '
            'FormAgregarDocumento
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(534, 186)
            Me.Controls.Add(Me.LblInformativo)
            Me.Controls.Add(Me.FileLabel)
            Me.Controls.Add(Me.CarpetaLabel)
            Me.Controls.Add(Me.GroupBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormAgregarDocumento"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Agregar Documento"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents cbarrasDesktopCBarrasControl As DesktopCBarrasControl
        Friend WithEvents FileLabel As System.Windows.Forms.Label
        Friend WithEvents CarpetaLabel As System.Windows.Forms.Label
        Friend WithEvents LblInformativo As System.Windows.Forms.Label
    End Class
End Namespace