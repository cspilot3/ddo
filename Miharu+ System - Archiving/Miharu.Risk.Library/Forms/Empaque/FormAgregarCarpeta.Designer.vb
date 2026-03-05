Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.Empaque
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormAgregarCarpeta
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAgregarCarpeta))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.TipoLabel = New System.Windows.Forms.Label()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.cbarrasDesktopCBarrasControl = New Miharu.Desktop.Controls.DesktopCBarras.DesktopCBarrasControl()
            Me.CarpetaLabel = New System.Windows.Forms.Label()
            Me.FolderLabel = New System.Windows.Forms.Label()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.TipoLabel)
            Me.GroupBox1.Controls.Add(Me.CancelarButton)
            Me.GroupBox1.Controls.Add(Me.AceptarButton)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.cbarrasDesktopCBarrasControl)
            Me.GroupBox1.Location = New System.Drawing.Point(13, 6)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(327, 155)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            '
            'TipoLabel
            '
            Me.TipoLabel.AutoSize = True
            Me.TipoLabel.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TipoLabel.Location = New System.Drawing.Point(16, 17)
            Me.TipoLabel.Name = "TipoLabel"
            Me.TipoLabel.Size = New System.Drawing.Size(16, 16)
            Me.TipoLabel.TabIndex = 10
            Me.TipoLabel.Text = "_"
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CancelarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(221, 107)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(93, 32)
            Me.CancelarButton.TabIndex = 9
            Me.CancelarButton.Text = "&Cerrar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(122, 107)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(93, 32)
            Me.AceptarButton.TabIndex = 8
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(16, 52)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(150, 13)
            Me.Label1.TabIndex = 7
            Me.Label1.Text = "Código de Barras Carpeta"
            '
            'cbarrasDesktopCBarrasControl
            '
            Me.cbarrasDesktopCBarrasControl.FocusIn = System.Drawing.Color.LightYellow
            Me.cbarrasDesktopCBarrasControl.FocusOut = System.Drawing.Color.White
            Me.cbarrasDesktopCBarrasControl.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cbarrasDesktopCBarrasControl.Location = New System.Drawing.Point(19, 76)
            Me.cbarrasDesktopCBarrasControl.MaxLength = 0
            Me.cbarrasDesktopCBarrasControl.Name = "cbarrasDesktopCBarrasControl"
            Me.cbarrasDesktopCBarrasControl.Permite_Pegar = False
            Me.cbarrasDesktopCBarrasControl.ShortcutsEnabled = False
            Me.cbarrasDesktopCBarrasControl.Size = New System.Drawing.Size(299, 20)
            Me.cbarrasDesktopCBarrasControl.TabIndex = 3
            '
            'CarpetaLabel
            '
            Me.CarpetaLabel.AutoSize = True
            Me.CarpetaLabel.ForeColor = System.Drawing.Color.Green
            Me.CarpetaLabel.Location = New System.Drawing.Point(31, 181)
            Me.CarpetaLabel.Name = "CarpetaLabel"
            Me.CarpetaLabel.Size = New System.Drawing.Size(153, 13)
            Me.CarpetaLabel.TabIndex = 11
            Me.CarpetaLabel.Text = "Ultima Carpeta Empacada"
            '
            'FolderLabel
            '
            Me.FolderLabel.AutoSize = True
            Me.FolderLabel.ForeColor = System.Drawing.SystemColors.ControlText
            Me.FolderLabel.Location = New System.Drawing.Point(198, 181)
            Me.FolderLabel.Name = "FolderLabel"
            Me.FolderLabel.Size = New System.Drawing.Size(14, 13)
            Me.FolderLabel.TabIndex = 12
            Me.FolderLabel.Text = "_"
            '
            'FormAgregarCarpeta
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(352, 205)
            Me.Controls.Add(Me.FolderLabel)
            Me.Controls.Add(Me.CarpetaLabel)
            Me.Controls.Add(Me.GroupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormAgregarCarpeta"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Empaque Carpeta"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents cbarrasDesktopCBarrasControl As DesktopCbarrasControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents TipoLabel As System.Windows.Forms.Label
        Friend WithEvents CarpetaLabel As System.Windows.Forms.Label
        Friend WithEvents FolderLabel As System.Windows.Forms.Label
    End Class
End Namespace