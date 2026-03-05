<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutForm))
        Me.pnlSeparador = New System.Windows.Forms.Panel()
        Me.txtBoxDescription = New System.Windows.Forms.TextBox()
        Me.lblCompanyName = New System.Windows.Forms.Label()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.pi = New System.Windows.Forms.PictureBox()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.lblProductName = New System.Windows.Forms.Label()
        CType(Me.pi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlSeparador
        '
        Me.pnlSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlSeparador.Location = New System.Drawing.Point(10, 189)
        Me.pnlSeparador.Name = "pnlSeparador"
        Me.pnlSeparador.Size = New System.Drawing.Size(350, 4)
        Me.pnlSeparador.TabIndex = 23
        '
        'txtBoxDescription
        '
        Me.txtBoxDescription.BackColor = System.Drawing.Color.White
        Me.txtBoxDescription.Location = New System.Drawing.Point(18, 117)
        Me.txtBoxDescription.Multiline = True
        Me.txtBoxDescription.Name = "txtBoxDescription"
        Me.txtBoxDescription.ReadOnly = True
        Me.txtBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBoxDescription.Size = New System.Drawing.Size(338, 64)
        Me.txtBoxDescription.TabIndex = 21
        Me.txtBoxDescription.Text = "txtBoxDescription"
        '
        'lblCompanyName
        '
        Me.lblCompanyName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyName.Location = New System.Drawing.Point(18, 85)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(300, 16)
        Me.lblCompanyName.TabIndex = 20
        Me.lblCompanyName.Text = "lblCompanyName"
        '
        'lblCopyright
        '
        Me.lblCopyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyright.Location = New System.Drawing.Point(18, 61)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(300, 24)
        Me.lblCopyright.TabIndex = 19
        Me.lblCopyright.Text = "lblCopyright"
        '
        'lblVersion
        '
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(18, 37)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(224, 16)
        Me.lblVersion.TabIndex = 18
        Me.lblVersion.Text = "lblVersion"
        '
        'pi
        '
        Me.pi.Image = CType(resources.GetObject("pi.Image"), System.Drawing.Image)
        Me.pi.Location = New System.Drawing.Point(324, 37)
        Me.pi.Name = "pi"
        Me.pi.Size = New System.Drawing.Size(32, 32)
        Me.pi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pi.TabIndex = 22
        Me.pi.TabStop = False
        '
        'btnAceptar
        '
        Me.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Image)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(284, 207)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(72, 24)
        Me.btnAceptar.TabIndex = 16
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblProductName
        '
        Me.lblProductName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblProductName.Location = New System.Drawing.Point(18, 5)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(338, 23)
        Me.lblProductName.TabIndex = 17
        Me.lblProductName.Text = "lblProductName"
        '
        'AboutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(370, 237)
        Me.Controls.Add(Me.pnlSeparador)
        Me.Controls.Add(Me.txtBoxDescription)
        Me.Controls.Add(Me.lblCompanyName)
        Me.Controls.Add(Me.lblCopyright)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.pi)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.lblProductName)
        Me.Name = "AboutForm"
        Me.Text = "Acerca de Miharu CargueLog Monitor"
        CType(Me.pi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSeparador As Panel
    Friend WithEvents txtBoxDescription As TextBox
    Friend WithEvents lblCompanyName As Label
    Friend WithEvents lblCopyright As Label
    Friend WithEvents lblVersion As Label
    Friend WithEvents pi As PictureBox
    Friend WithEvents btnAceptar As Button
    Friend WithEvents lblProductName As Label
End Class
