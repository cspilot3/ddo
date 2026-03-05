Namespace Embargos.Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormConfigImagenes
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormConfigImagenes))
            Me.SignatureImageTextBox = New System.Windows.Forms.TextBox()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.SignatureImageButton = New System.Windows.Forms.Button()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.SignatureImagePictureBox = New System.Windows.Forms.PictureBox()
            Me.LogoImageTextBox = New System.Windows.Forms.TextBox()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.LogoImageButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.LogoImagePictureBox = New System.Windows.Forms.PictureBox()
            CType(Me.SignatureImagePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox2.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            CType(Me.LogoImagePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'SignatureImageTextBox
            '
            Me.SignatureImageTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SignatureImageTextBox.Location = New System.Drawing.Point(21, 164)
            Me.SignatureImageTextBox.Name = "SignatureImageTextBox"
            Me.SignatureImageTextBox.Size = New System.Drawing.Size(251, 20)
            Me.SignatureImageTextBox.TabIndex = 4
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(194, 378)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 23)
            Me.AceptarButton.TabIndex = 10
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'SignatureImageButton
            '
            Me.SignatureImageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SignatureImageButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.SignatureImageButton.Location = New System.Drawing.Point(278, 164)
            Me.SignatureImageButton.Name = "SignatureImageButton"
            Me.SignatureImageButton.Size = New System.Drawing.Size(82, 20)
            Me.SignatureImageButton.TabIndex = 3
            Me.SignatureImageButton.Text = "Buscar ..."
            Me.SignatureImageButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.SignatureImageButton.UseVisualStyleBackColor = True
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(292, 378)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 23)
            Me.CancelarButton.TabIndex = 11
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'SignatureImagePictureBox
            '
            Me.SignatureImagePictureBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SignatureImagePictureBox.BackColor = System.Drawing.SystemColors.Control
            Me.SignatureImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.SignatureImagePictureBox.Location = New System.Drawing.Point(21, 19)
            Me.SignatureImagePictureBox.Name = "SignatureImagePictureBox"
            Me.SignatureImagePictureBox.Size = New System.Drawing.Size(339, 139)
            Me.SignatureImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.SignatureImagePictureBox.TabIndex = 2
            Me.SignatureImagePictureBox.TabStop = False
            '
            'LogoImageTextBox
            '
            Me.LogoImageTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LogoImageTextBox.Location = New System.Drawing.Point(21, 135)
            Me.LogoImageTextBox.Name = "LogoImageTextBox"
            Me.LogoImageTextBox.Size = New System.Drawing.Size(251, 20)
            Me.LogoImageTextBox.TabIndex = 2
            '
            'GroupBox2
            '
            Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox2.Controls.Add(Me.SignatureImageTextBox)
            Me.GroupBox2.Controls.Add(Me.SignatureImageButton)
            Me.GroupBox2.Controls.Add(Me.SignatureImagePictureBox)
            Me.GroupBox2.Location = New System.Drawing.Point(6, 176)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(366, 190)
            Me.GroupBox2.TabIndex = 9
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Firma Autorizada"
            '
            'LogoImageButton
            '
            Me.LogoImageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LogoImageButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.LogoImageButton.Location = New System.Drawing.Point(278, 135)
            Me.LogoImageButton.Name = "LogoImageButton"
            Me.LogoImageButton.Size = New System.Drawing.Size(82, 20)
            Me.LogoImageButton.TabIndex = 1
            Me.LogoImageButton.Text = "Buscar ..."
            Me.LogoImageButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.LogoImageButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.LogoImageTextBox)
            Me.GroupBox1.Controls.Add(Me.LogoImageButton)
            Me.GroupBox1.Controls.Add(Me.LogoImagePictureBox)
            Me.GroupBox1.Location = New System.Drawing.Point(6, 9)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(366, 161)
            Me.GroupBox1.TabIndex = 8
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Logo"
            '
            'LogoImagePictureBox
            '
            Me.LogoImagePictureBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LogoImagePictureBox.BackColor = System.Drawing.SystemColors.Control
            Me.LogoImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.LogoImagePictureBox.Location = New System.Drawing.Point(21, 20)
            Me.LogoImagePictureBox.Name = "LogoImagePictureBox"
            Me.LogoImagePictureBox.Size = New System.Drawing.Size(339, 109)
            Me.LogoImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.LogoImagePictureBox.TabIndex = 0
            Me.LogoImagePictureBox.TabStop = False
            '
            'FormConfigImagenes
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(379, 411)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.GroupBox1)
            Me.Name = "FormConfigImagenes"
            Me.Text = "FormConfigImagenes"
            CType(Me.SignatureImagePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.LogoImagePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents SignatureImageTextBox As System.Windows.Forms.TextBox
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents SignatureImageButton As System.Windows.Forms.Button
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents SignatureImagePictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents LogoImageTextBox As System.Windows.Forms.TextBox
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents LogoImageButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents LogoImagePictureBox As System.Windows.Forms.PictureBox
    End Class
End Namespace