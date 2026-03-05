<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAbout
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAbout))
        Me.pnlSeparador = New System.Windows.Forms.Panel
        Me.txtBoxDescription = New System.Windows.Forms.TextBox
        Me.lblCompanyName = New System.Windows.Forms.Label
        Me.lblCopyright = New System.Windows.Forms.Label
        Me.lblVersion = New System.Windows.Forms.Label
        Me.lblProductName = New System.Windows.Forms.Label
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.pi = New System.Windows.Forms.PictureBox
        CType(Me.pi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlSeparador
        '
        Me.pnlSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlSeparador.Location = New System.Drawing.Point(275, 198)
        Me.pnlSeparador.Name = "pnlSeparador"
        Me.pnlSeparador.Size = New System.Drawing.Size(232, 4)
        Me.pnlSeparador.TabIndex = 15
        '
        'txtBoxDescription
        '
        Me.txtBoxDescription.BackColor = System.Drawing.Color.White
        Me.txtBoxDescription.Location = New System.Drawing.Point(279, 126)
        Me.txtBoxDescription.Multiline = True
        Me.txtBoxDescription.Name = "txtBoxDescription"
        Me.txtBoxDescription.ReadOnly = True
        Me.txtBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBoxDescription.Size = New System.Drawing.Size(224, 64)
        Me.txtBoxDescription.TabIndex = 14
        '
        'lblCompanyName
        '
        Me.lblCompanyName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyName.Location = New System.Drawing.Point(279, 94)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(224, 16)
        Me.lblCompanyName.TabIndex = 13
        Me.lblCompanyName.Text = "lblCompanyName"
        '
        'lblCopyright
        '
        Me.lblCopyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyright.Location = New System.Drawing.Point(279, 70)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(224, 16)
        Me.lblCopyright.TabIndex = 12
        Me.lblCopyright.Text = "lblCopyright"
        '
        'lblVersion
        '
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(279, 46)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(224, 16)
        Me.lblVersion.TabIndex = 11
        Me.lblVersion.Text = "lblVersion"
        '
        'lblProductName
        '
        Me.lblProductName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductName.Location = New System.Drawing.Point(279, 20)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(232, 23)
        Me.lblProductName.TabIndex = 9
        Me.lblProductName.Text = "lblProductName"
        '
        'btnAceptar
        '
        Me.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.Image = Global.Miharu.Imaging.OffLineViewer.My.Resources.Resources.tick
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(424, 214)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(79, 24)
        Me.btnAceptar.TabIndex = 8
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pi
        '
        Me.pi.Image = Global.Miharu.Imaging.OffLineViewer.My.Resources.Resources.OffLineViewer
        Me.pi.Location = New System.Drawing.Point(10, -10)
        Me.pi.Name = "pi"
        Me.pi.Size = New System.Drawing.Size(256, 256)
        Me.pi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pi.TabIndex = 10
        Me.pi.TabStop = False
        '
        'FormAbout
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnAceptar
        Me.ClientSize = New System.Drawing.Size(514, 252)
        Me.Controls.Add(Me.pnlSeparador)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.txtBoxDescription)
        Me.Controls.Add(Me.lblCompanyName)
        Me.Controls.Add(Me.lblCopyright)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblProductName)
        Me.Controls.Add(Me.pi)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormAbout"
        Me.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
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
