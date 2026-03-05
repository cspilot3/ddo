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
        Me.picSLYG = New System.Windows.Forms.PictureBox()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.lblMiharu = New System.Windows.Forms.Label()
        Me.lblImaging = New System.Windows.Forms.Label()
        CType(Me.picSLYG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 32.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblTitulo.Location = New System.Drawing.Point(274, 87)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(313, 59)
        Me.lblTitulo.TabIndex = 2
        Me.lblTitulo.Text = "OffLineViewer"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        'picSLYG
        '
        Me.picSLYG.Image = Global.Miharu.Imaging.OffLineViewer.My.Resources.Resources.LogoPYC
        Me.picSLYG.Location = New System.Drawing.Point(443, 203)
        Me.picSLYG.Name = "picSLYG"
        Me.picSLYG.Size = New System.Drawing.Size(450, 170)
        Me.picSLYG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picSLYG.TabIndex = 3
        Me.picSLYG.TabStop = False
        ''
        'picLogo
        '
        Me.picLogo.Image = Global.Miharu.Imaging.OffLineViewer.My.Resources.Resources.OffLineViewer
        Me.picLogo.Location = New System.Drawing.Point(12, 10)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(256, 256)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picLogo.TabIndex = 0
        Me.picLogo.TabStop = False
        '
        'lblMiharu
        '
        Me.lblMiharu.AutoSize = True
        Me.lblMiharu.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMiharu.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblMiharu.Location = New System.Drawing.Point(440, 9)
        Me.lblMiharu.Name = "lblMiharu"
        Me.lblMiharu.Size = New System.Drawing.Size(147, 37)
        Me.lblMiharu.TabIndex = 7
        Me.lblMiharu.Text = "MIHARU"
        '
        'lblImaging
        '
        Me.lblImaging.AutoSize = True
        Me.lblImaging.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImaging.ForeColor = System.Drawing.Color.Green
        Me.lblImaging.Location = New System.Drawing.Point(508, 41)
        Me.lblImaging.Name = "lblImaging"
        Me.lblImaging.Size = New System.Drawing.Size(63, 16)
        Me.lblImaging.TabIndex = 8
        Me.lblImaging.Text = "Imaging"
        Me.lblImaging.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FormSplash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(598, 274)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblImaging)
        Me.Controls.Add(Me.lblMiharu)
        Me.Controls.Add(Me.lblDesarrollado)
        Me.Controls.Add(Me.picSLYG)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.picLogo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormSplash"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        CType(Me.picSLYG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents picSLYG As System.Windows.Forms.PictureBox
    Friend WithEvents lblDesarrollado As System.Windows.Forms.Label
    Friend WithEvents lblMiharu As System.Windows.Forms.Label
    Friend WithEvents lblImaging As System.Windows.Forms.Label

End Class
