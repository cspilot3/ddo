Namespace Procesos.Configuracion.Imaging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormConfigImagenes
        Inherits Miharu.Desktop.Library.FormBase

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
            Me.HeaderLogoImageTextBox = New System.Windows.Forms.TextBox()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.LogoImageButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.LogoImagePictureBox = New System.Windows.Forms.PictureBox()
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            Me.FooterLogoImageTextBox = New System.Windows.Forms.TextBox()
            Me.FooterImageButton = New System.Windows.Forms.Button()
            Me.FooterImagePictureBox = New System.Windows.Forms.PictureBox()
            Me.cbxSerieDocumental = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblCiald = New System.Windows.Forms.Label()
            Me.cbxFormato = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            CType(Me.SignatureImagePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox2.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            CType(Me.LogoImagePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox3.SuspendLayout()
            CType(Me.FooterImagePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'SignatureImageTextBox
            '
            Me.SignatureImageTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SignatureImageTextBox.Location = New System.Drawing.Point(24, 135)
            Me.SignatureImageTextBox.Name = "SignatureImageTextBox"
            Me.SignatureImageTextBox.Size = New System.Drawing.Size(325, 21)
            Me.SignatureImageTextBox.TabIndex = 4
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(259, 568)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(93, 23)
            Me.AceptarButton.TabIndex = 10
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'SignatureImageButton
            '
            Me.SignatureImageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SignatureImageButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.SignatureImageButton.Location = New System.Drawing.Point(357, 135)
            Me.SignatureImageButton.Name = "SignatureImageButton"
            Me.SignatureImageButton.Size = New System.Drawing.Size(96, 20)
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
            Me.CancelarButton.Location = New System.Drawing.Point(374, 568)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(93, 23)
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
            Me.SignatureImagePictureBox.Location = New System.Drawing.Point(24, 19)
            Me.SignatureImagePictureBox.Name = "SignatureImagePictureBox"
            Me.SignatureImagePictureBox.Size = New System.Drawing.Size(428, 110)
            Me.SignatureImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.SignatureImagePictureBox.TabIndex = 2
            Me.SignatureImagePictureBox.TabStop = False
            '
            'HeaderLogoImageTextBox
            '
            Me.HeaderLogoImageTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.HeaderLogoImageTextBox.Location = New System.Drawing.Point(24, 135)
            Me.HeaderLogoImageTextBox.Name = "HeaderLogoImageTextBox"
            Me.HeaderLogoImageTextBox.Size = New System.Drawing.Size(325, 21)
            Me.HeaderLogoImageTextBox.TabIndex = 2
            '
            'GroupBox2
            '
            Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox2.Controls.Add(Me.SignatureImageTextBox)
            Me.GroupBox2.Controls.Add(Me.SignatureImageButton)
            Me.GroupBox2.Controls.Add(Me.SignatureImagePictureBox)
            Me.GroupBox2.Enabled = False
            Me.GroupBox2.Location = New System.Drawing.Point(7, 237)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(460, 161)
            Me.GroupBox2.TabIndex = 9
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Firma Autorizada"
            '
            'LogoImageButton
            '
            Me.LogoImageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LogoImageButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.LogoImageButton.Location = New System.Drawing.Point(357, 135)
            Me.LogoImageButton.Name = "LogoImageButton"
            Me.LogoImageButton.Size = New System.Drawing.Size(96, 20)
            Me.LogoImageButton.TabIndex = 1
            Me.LogoImageButton.Text = "Buscar ..."
            Me.LogoImageButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.LogoImageButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.HeaderLogoImageTextBox)
            Me.GroupBox1.Controls.Add(Me.LogoImageButton)
            Me.GroupBox1.Controls.Add(Me.LogoImagePictureBox)
            Me.GroupBox1.Enabled = False
            Me.GroupBox1.Location = New System.Drawing.Point(7, 70)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(460, 161)
            Me.GroupBox1.TabIndex = 8
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Encabezado"
            '
            'LogoImagePictureBox
            '
            Me.LogoImagePictureBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LogoImagePictureBox.BackColor = System.Drawing.SystemColors.Control
            Me.LogoImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.LogoImagePictureBox.Location = New System.Drawing.Point(24, 20)
            Me.LogoImagePictureBox.Name = "LogoImagePictureBox"
            Me.LogoImagePictureBox.Size = New System.Drawing.Size(428, 109)
            Me.LogoImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.LogoImagePictureBox.TabIndex = 0
            Me.LogoImagePictureBox.TabStop = False
            '
            'GroupBox3
            '
            Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox3.Controls.Add(Me.FooterLogoImageTextBox)
            Me.GroupBox3.Controls.Add(Me.FooterImageButton)
            Me.GroupBox3.Controls.Add(Me.FooterImagePictureBox)
            Me.GroupBox3.Enabled = False
            Me.GroupBox3.Location = New System.Drawing.Point(7, 399)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Size = New System.Drawing.Size(460, 161)
            Me.GroupBox3.TabIndex = 12
            Me.GroupBox3.TabStop = False
            Me.GroupBox3.Text = "pie de Pagina"
            '
            'FooterLogoImageTextBox
            '
            Me.FooterLogoImageTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FooterLogoImageTextBox.Location = New System.Drawing.Point(24, 135)
            Me.FooterLogoImageTextBox.Name = "FooterLogoImageTextBox"
            Me.FooterLogoImageTextBox.Size = New System.Drawing.Size(325, 21)
            Me.FooterLogoImageTextBox.TabIndex = 4
            '
            'FooterImageButton
            '
            Me.FooterImageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FooterImageButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.FooterImageButton.Location = New System.Drawing.Point(357, 135)
            Me.FooterImageButton.Name = "FooterImageButton"
            Me.FooterImageButton.Size = New System.Drawing.Size(96, 20)
            Me.FooterImageButton.TabIndex = 3
            Me.FooterImageButton.Text = "Buscar ..."
            Me.FooterImageButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.FooterImageButton.UseVisualStyleBackColor = True
            '
            'FooterImagePictureBox
            '
            Me.FooterImagePictureBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FooterImagePictureBox.BackColor = System.Drawing.SystemColors.Control
            Me.FooterImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.FooterImagePictureBox.Location = New System.Drawing.Point(24, 19)
            Me.FooterImagePictureBox.Name = "FooterImagePictureBox"
            Me.FooterImagePictureBox.Size = New System.Drawing.Size(428, 110)
            Me.FooterImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.FooterImagePictureBox.TabIndex = 2
            Me.FooterImagePictureBox.TabStop = False
            '
            'cbxSerieDocumental
            '
            Me.cbxSerieDocumental.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cbxSerieDocumental.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cbxSerieDocumental.DisabledEnter = False
            Me.cbxSerieDocumental.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxSerieDocumental.fk_Campo = 0
            Me.cbxSerieDocumental.fk_Documento = 0
            Me.cbxSerieDocumental.fk_Validacion = 0
            Me.cbxSerieDocumental.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cbxSerieDocumental.FormattingEnabled = True
            Me.cbxSerieDocumental.Location = New System.Drawing.Point(31, 35)
            Me.cbxSerieDocumental.Name = "cbxSerieDocumental"
            Me.cbxSerieDocumental.Size = New System.Drawing.Size(172, 21)
            Me.cbxSerieDocumental.TabIndex = 13
            '
            'lblCiald
            '
            Me.lblCiald.AutoSize = True
            Me.lblCiald.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCiald.Location = New System.Drawing.Point(47, 17)
            Me.lblCiald.Name = "lblCiald"
            Me.lblCiald.Size = New System.Drawing.Size(126, 15)
            Me.lblCiald.TabIndex = 27
            Me.lblCiald.Text = "Serie Documental:"
            '
            'cbxFormato
            '
            Me.cbxFormato.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cbxFormato.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cbxFormato.DisabledEnter = False
            Me.cbxFormato.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxFormato.fk_Campo = 0
            Me.cbxFormato.fk_Documento = 0
            Me.cbxFormato.fk_Validacion = 0
            Me.cbxFormato.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cbxFormato.FormattingEnabled = True
            Me.cbxFormato.Location = New System.Drawing.Point(273, 35)
            Me.cbxFormato.Name = "cbxFormato"
            Me.cbxFormato.Size = New System.Drawing.Size(172, 21)
            Me.cbxFormato.TabIndex = 28
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(311, 17)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(64, 15)
            Me.Label1.TabIndex = 29
            Me.Label1.Text = "Formato:"
            '
            'FormConfigImagenes
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(475, 601)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.cbxFormato)
            Me.Controls.Add(Me.lblCiald)
            Me.Controls.Add(Me.cbxSerieDocumental)
            Me.Controls.Add(Me.GroupBox3)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.GroupBox1)
            Me.Name = "FormConfigImagenes"
            Me.Text = "Cargue Imagen Logo y Firma"
            CType(Me.SignatureImagePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.LogoImagePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox3.ResumeLayout(False)
            Me.GroupBox3.PerformLayout()
            CType(Me.FooterImagePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents SignatureImageTextBox As System.Windows.Forms.TextBox
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents SignatureImageButton As System.Windows.Forms.Button
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents SignatureImagePictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents HeaderLogoImageTextBox As System.Windows.Forms.TextBox
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents LogoImageButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents LogoImagePictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents FooterLogoImageTextBox As System.Windows.Forms.TextBox
        Friend WithEvents FooterImageButton As System.Windows.Forms.Button
        Friend WithEvents FooterImagePictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents cbxSerieDocumental As Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents lblCiald As Label
        Friend WithEvents cbxFormato As Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label1 As Label
    End Class
End Namespace