<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutForm))
        Me.pnlSeparador = New System.Windows.Forms.Panel()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.txtBoxDescription = New System.Windows.Forms.TextBox()
        Me.lblCompanyName = New System.Windows.Forms.Label()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblProductName = New System.Windows.Forms.Label()
        Me.pi = New System.Windows.Forms.PictureBox()
        CType(Me.pi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlSeparador
        '
        Me.pnlSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlSeparador.Location = New System.Drawing.Point(6, 194)
        Me.pnlSeparador.Name = "pnlSeparador"
        Me.pnlSeparador.Size = New System.Drawing.Size(350, 4)
        Me.pnlSeparador.TabIndex = 15
        '
        'btnAceptar
        '
        Me.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(280, 212)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(72, 24)
        Me.btnAceptar.TabIndex = 8
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBoxDescription
        '
        Me.txtBoxDescription.BackColor = System.Drawing.Color.White
        Me.txtBoxDescription.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.txtBoxDescription.Location = New System.Drawing.Point(14, 122)
        Me.txtBoxDescription.Multiline = True
        Me.txtBoxDescription.Name = "txtBoxDescription"
        Me.txtBoxDescription.ReadOnly = True
        Me.txtBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBoxDescription.Size = New System.Drawing.Size(338, 64)
        Me.txtBoxDescription.TabIndex = 14
        Me.txtBoxDescription.Text = "txtBoxDescription"
        '
        'lblCompanyName
        '
        Me.lblCompanyName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyName.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.lblCompanyName.Location = New System.Drawing.Point(14, 90)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(300, 16)
        Me.lblCompanyName.TabIndex = 13
        Me.lblCompanyName.Text = "lblCompanyName"
        '
        'lblCopyright
        '
        Me.lblCopyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyright.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.lblCopyright.Location = New System.Drawing.Point(14, 66)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(300, 24)
        Me.lblCopyright.TabIndex = 12
        Me.lblCopyright.Text = "lblCopyright"
        '
        'lblVersion
        '
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.lblVersion.Location = New System.Drawing.Point(14, 42)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(224, 16)
        Me.lblVersion.TabIndex = 11
        Me.lblVersion.Text = "lblVersion"
        '
        'lblProductName
        '
        Me.lblProductName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblProductName.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.lblProductName.Location = New System.Drawing.Point(14, 10)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(338, 23)
        Me.lblProductName.TabIndex = 9
        Me.lblProductName.Text = "lblProductName"
        '
        'pi
        '
        Me.pi.ErrorImage = Global.PunteoBanAgrario.FileUp.Monitor.My.Resources.Resources.FileUpIcon
        Me.pi.Image = Global.PunteoBanAgrario.FileUp.Monitor.My.Resources.Resources.FileUpIcon
        Me.pi.Location = New System.Drawing.Point(320, 42)
        Me.pi.Name = "pi"
        Me.pi.Size = New System.Drawing.Size(32, 32)
        Me.pi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pi.TabIndex = 10
        Me.pi.TabStop = False
        '
        'AboutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(364, 245)
        Me.Controls.Add(Me.pnlSeparador)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.txtBoxDescription)
        Me.Controls.Add(Me.lblCompanyName)
        Me.Controls.Add(Me.lblCopyright)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblProductName)
        Me.Controls.Add(Me.pi)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AboutForm"
        CType(Me.pi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSeparador As System.Windows.Forms.Panel
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents txtBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblProductName As System.Windows.Forms.Label
    Friend WithEvents pi As System.Windows.Forms.PictureBox
End Class
