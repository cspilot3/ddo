<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSplash
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
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblDesarrollado = New System.Windows.Forms.Label()
        Me.lblMiharu = New System.Windows.Forms.Label()
        Me.picDomesa = New System.Windows.Forms.PictureBox()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        CType(Me.picDomesa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 28.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblTitulo.Location = New System.Drawing.Point(286, 56)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(283, 138)
        Me.lblTitulo.TabIndex = 2
        Me.lblTitulo.Text = "Digitalización Desde el Origen"
        '
        'lblDesarrollado
        '
        Me.lblDesarrollado.AutoSize = True
        Me.lblDesarrollado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesarrollado.Location = New System.Drawing.Point(440, 180)
        Me.lblDesarrollado.Name = "lblDesarrollado"
        Me.lblDesarrollado.Size = New System.Drawing.Size(104, 13)
        Me.lblDesarrollado.TabIndex = 4
        Me.lblDesarrollado.Text = "Desarrollado por:"
        '
        'lblMiharu
        '
        Me.lblMiharu.AutoSize = True
        Me.lblMiharu.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMiharu.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblMiharu.Location = New System.Drawing.Point(430, 9)
        Me.lblMiharu.Name = "lblMiharu"
        Me.lblMiharu.Size = New System.Drawing.Size(147, 37)
        Me.lblMiharu.TabIndex = 7
        Me.lblMiharu.Text = "MIHARU"
        '
        'picDomesa
        '
        Me.picDomesa.Image = Global.Miharu.Desktop.My.Resources.Resources.LogoPYC
        Me.picDomesa.InitialImage = Global.Miharu.Desktop.My.Resources.Resources.LogoPYC
        Me.picDomesa.Location = New System.Drawing.Point(443, 199)
        Me.picDomesa.Name = "picDomesa"
        Me.picDomesa.Size = New System.Drawing.Size(149, 44)
        Me.picDomesa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picDomesa.TabIndex = 3
        Me.picDomesa.TabStop = False
        '
        'picLogo
        '
        Me.picLogo.Image = Global.Miharu.Desktop.My.Resources.Resources.MiharuDesktopLogo
        Me.picLogo.Location = New System.Drawing.Point(17, 10)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(256, 256)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picLogo.TabIndex = 0
        Me.picLogo.TabStop = False
        '
        'FormSplash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(598, 274)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblMiharu)
        Me.Controls.Add(Me.lblDesarrollado)
        Me.Controls.Add(Me.picDomesa)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.picLogo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormSplash"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        CType(Me.picDomesa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblDesarrollado As System.Windows.Forms.Label
    Friend WithEvents lblMiharu As System.Windows.Forms.Label
    Friend WithEvents picDomesa As System.Windows.Forms.PictureBox

End Class
