Namespace Embargos.Forms.FormGenerarCartas.FormCapturaCbarras
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCapturaCbarras
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
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.CbarrasCapturaTextBox = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.CbarrasLeidoTextBox = New System.Windows.Forms.TextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.pnlMarcoDibujo = New System.Windows.Forms.Panel()
            Me.pnlImage = New System.Windows.Forms.Panel()
            Me.picImage = New System.Windows.Forms.PictureBox()
            Me.GroupBox1.SuspendLayout()
            Me.pnlMarcoDibujo.SuspendLayout()
            Me.pnlImage.SuspendLayout()
            CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.AceptarButton)
            Me.GroupBox1.Controls.Add(Me.CbarrasCapturaTextBox)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.CbarrasLeidoTextBox)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
            Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(727, 54)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            '
            'AceptarButton
            '
            Me.AceptarButton.Image = Global.Santander.Plugin.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(546, 24)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(72, 23)
            Me.AceptarButton.TabIndex = 4
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'CbarrasCapturaTextBox
            '
            Me.CbarrasCapturaTextBox.Location = New System.Drawing.Point(356, 26)
            Me.CbarrasCapturaTextBox.Name = "CbarrasCapturaTextBox"
            Me.CbarrasCapturaTextBox.Size = New System.Drawing.Size(176, 20)
            Me.CbarrasCapturaTextBox.TabIndex = 3
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(242, 27)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(108, 15)
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "Código Imagen:"
            '
            'CbarrasLeidoTextBox
            '
            Me.CbarrasLeidoTextBox.Enabled = False
            Me.CbarrasLeidoTextBox.Location = New System.Drawing.Point(106, 24)
            Me.CbarrasLeidoTextBox.Name = "CbarrasLeidoTextBox"
            Me.CbarrasLeidoTextBox.Size = New System.Drawing.Size(130, 20)
            Me.CbarrasLeidoTextBox.TabIndex = 1
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(9, 25)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(96, 15)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Código Leído:"
            '
            'pnlMarcoDibujo
            '
            Me.pnlMarcoDibujo.AutoScroll = True
            Me.pnlMarcoDibujo.BackColor = System.Drawing.Color.Teal
            Me.pnlMarcoDibujo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.pnlMarcoDibujo.Controls.Add(Me.pnlImage)
            Me.pnlMarcoDibujo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlMarcoDibujo.Location = New System.Drawing.Point(0, 54)
            Me.pnlMarcoDibujo.Name = "pnlMarcoDibujo"
            Me.pnlMarcoDibujo.Size = New System.Drawing.Size(727, 396)
            Me.pnlMarcoDibujo.TabIndex = 3
            '
            'pnlImage
            '
            Me.pnlImage.AutoSize = True
            Me.pnlImage.BackColor = System.Drawing.SystemColors.ControlDarkDark
            Me.pnlImage.Controls.Add(Me.picImage)
            Me.pnlImage.Location = New System.Drawing.Point(3, 3)
            Me.pnlImage.Name = "pnlImage"
            Me.pnlImage.Padding = New System.Windows.Forms.Padding(10)
            Me.pnlImage.Size = New System.Drawing.Size(384, 264)
            Me.pnlImage.TabIndex = 0
            '
            'picImage
            '
            Me.picImage.BackColor = System.Drawing.Color.White
            Me.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.picImage.Dock = System.Windows.Forms.DockStyle.Fill
            Me.picImage.Location = New System.Drawing.Point(10, 10)
            Me.picImage.Name = "picImage"
            Me.picImage.Size = New System.Drawing.Size(364, 244)
            Me.picImage.TabIndex = 0
            Me.picImage.TabStop = False
            '
            'FormCapturaCbarras
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(727, 450)
            Me.Controls.Add(Me.pnlMarcoDibujo)
            Me.Controls.Add(Me.GroupBox1)
            Me.Name = "FormCapturaCbarras"
            Me.Text = "Captura Códigos Ilegibles"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.pnlMarcoDibujo.ResumeLayout(False)
            Me.pnlMarcoDibujo.PerformLayout()
            Me.pnlImage.ResumeLayout(False)
            CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CbarrasCapturaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents CbarrasLeidoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents pnlMarcoDibujo As System.Windows.Forms.Panel
        Friend WithEvents pnlImage As System.Windows.Forms.Panel
        Friend WithEvents picImage As System.Windows.Forms.PictureBox
    End Class
End Namespace