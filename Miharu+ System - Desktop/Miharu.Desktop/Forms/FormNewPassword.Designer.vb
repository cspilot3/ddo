Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNewPassword
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango3 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNewPassword))
            Me.gbxLogin = New System.Windows.Forms.GroupBox()
            Me.ConfirmPasswordTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.CnfirmPasswordLabel = New System.Windows.Forms.Label()
            Me.NewPasswordTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.OldPasswordTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.OldPasswordLabel = New System.Windows.Forms.Label()
            Me.NewPasswordLabel = New System.Windows.Forms.Label()
            Me.LoginPictureBox = New System.Windows.Forms.PictureBox()
            Me.gbxLogin.SuspendLayout()
            CType(Me.LoginPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'gbxLogin
            '
            Me.gbxLogin.Controls.Add(Me.ConfirmPasswordTextBox)
            Me.gbxLogin.Controls.Add(Me.CnfirmPasswordLabel)
            Me.gbxLogin.Controls.Add(Me.NewPasswordTextBox)
            Me.gbxLogin.Controls.Add(Me.CancelarButton)
            Me.gbxLogin.Controls.Add(Me.AceptarButton)
            Me.gbxLogin.Controls.Add(Me.OldPasswordTextBox)
            Me.gbxLogin.Controls.Add(Me.OldPasswordLabel)
            Me.gbxLogin.Controls.Add(Me.NewPasswordLabel)
            Me.gbxLogin.Controls.Add(Me.LoginPictureBox)
            Me.gbxLogin.Dock = System.Windows.Forms.DockStyle.Fill
            Me.gbxLogin.Location = New System.Drawing.Point(5, 5)
            Me.gbxLogin.Margin = New System.Windows.Forms.Padding(0)
            Me.gbxLogin.Name = "gbxLogin"
            Me.gbxLogin.Padding = New System.Windows.Forms.Padding(0)
            Me.gbxLogin.Size = New System.Drawing.Size(425, 251)
            Me.gbxLogin.TabIndex = 0
            Me.gbxLogin.TabStop = False
            '
            'ConfirmPasswordTextBox
            '
            Me.ConfirmPasswordTextBox._Obligatorio = False
            Me.ConfirmPasswordTextBox._PermitePegar = False
            Me.ConfirmPasswordTextBox.Cantidad_Decimales = CType(0, Short)
            Me.ConfirmPasswordTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ConfirmPasswordTextBox.DateFormat = Nothing
            Me.ConfirmPasswordTextBox.DisabledEnter = False
            Me.ConfirmPasswordTextBox.DisabledTab = False
            Me.ConfirmPasswordTextBox.EnabledShortCuts = False
            Me.ConfirmPasswordTextBox.fk_Campo = 0
            Me.ConfirmPasswordTextBox.fk_Documento = 0
            Me.ConfirmPasswordTextBox.fk_Validacion = 0
            Me.ConfirmPasswordTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ConfirmPasswordTextBox.FocusOut = System.Drawing.Color.White
            Me.ConfirmPasswordTextBox.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
            Me.ConfirmPasswordTextBox.Location = New System.Drawing.Point(200, 154)
            Me.ConfirmPasswordTextBox.MaskedTextBox_Property = ""
            Me.ConfirmPasswordTextBox.MaximumLength = CType(0, Short)
            Me.ConfirmPasswordTextBox.MinimumLength = CType(0, Short)
            Me.ConfirmPasswordTextBox.Name = "ConfirmPasswordTextBox"
            Me.ConfirmPasswordTextBox.Obligatorio = False
            Me.ConfirmPasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.ConfirmPasswordTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0R
            Me.ConfirmPasswordTextBox.Rango = Rango1
            Me.ConfirmPasswordTextBox.Size = New System.Drawing.Size(214, 26)
            Me.ConfirmPasswordTextBox.TabIndex = 5
            Me.ConfirmPasswordTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ConfirmPasswordTextBox.Usa_Decimales = False
            Me.ConfirmPasswordTextBox.Validos_Cantidad_Puntos = False
            '
            'CnfirmPasswordLabel
            '
            Me.CnfirmPasswordLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CnfirmPasswordLabel.Location = New System.Drawing.Point(197, 134)
            Me.CnfirmPasswordLabel.Name = "CnfirmPasswordLabel"
            Me.CnfirmPasswordLabel.Size = New System.Drawing.Size(136, 16)
            Me.CnfirmPasswordLabel.TabIndex = 4
            Me.CnfirmPasswordLabel.Text = "Confirmar contraseña"
            '
            'NewPasswordTextBox
            '
            Me.NewPasswordTextBox._Obligatorio = False
            Me.NewPasswordTextBox._PermitePegar = False
            Me.NewPasswordTextBox.Cantidad_Decimales = CType(0, Short)
            Me.NewPasswordTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.NewPasswordTextBox.DateFormat = Nothing
            Me.NewPasswordTextBox.DisabledEnter = False
            Me.NewPasswordTextBox.DisabledTab = False
            Me.NewPasswordTextBox.EnabledShortCuts = False
            Me.NewPasswordTextBox.fk_Campo = 0
            Me.NewPasswordTextBox.fk_Documento = 0
            Me.NewPasswordTextBox.fk_Validacion = 0
            Me.NewPasswordTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.NewPasswordTextBox.FocusOut = System.Drawing.Color.White
            Me.NewPasswordTextBox.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
            Me.NewPasswordTextBox.Location = New System.Drawing.Point(201, 100)
            Me.NewPasswordTextBox.MaskedTextBox_Property = ""
            Me.NewPasswordTextBox.MaximumLength = CType(0, Short)
            Me.NewPasswordTextBox.MinimumLength = CType(0, Short)
            Me.NewPasswordTextBox.Name = "NewPasswordTextBox"
            Me.NewPasswordTextBox.Obligatorio = False
            Me.NewPasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.NewPasswordTextBox.permitePegar = False
            Rango2.MaxValue = 2147483647.0R
            Rango2.MinValue = 0R
            Me.NewPasswordTextBox.Rango = Rango2
            Me.NewPasswordTextBox.Size = New System.Drawing.Size(214, 26)
            Me.NewPasswordTextBox.TabIndex = 3
            Me.NewPasswordTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.NewPasswordTextBox.Usa_Decimales = False
            Me.NewPasswordTextBox.Validos_Cantidad_Puntos = False
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.CancelarButton.Cursor = System.Windows.Forms.Cursors.Hand
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.FlatAppearance.BorderSize = 0
            Me.CancelarButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
            Me.CancelarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.Cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(316, 199)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(96, 35)
            Me.CancelarButton.TabIndex = 7
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.AceptarButton.Cursor = System.Windows.Forms.Cursors.Hand
            Me.AceptarButton.FlatAppearance.BorderSize = 0
            Me.AceptarButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
            Me.AceptarButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
            Me.AceptarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.Aceptar
            Me.AceptarButton.Location = New System.Drawing.Point(206, 199)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(93, 35)
            Me.AceptarButton.TabIndex = 6
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
            '
            'OldPasswordTextBox
            '
            Me.OldPasswordTextBox._Obligatorio = False
            Me.OldPasswordTextBox._PermitePegar = False
            Me.OldPasswordTextBox.Cantidad_Decimales = CType(0, Short)
            Me.OldPasswordTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.OldPasswordTextBox.DateFormat = Nothing
            Me.OldPasswordTextBox.DisabledEnter = False
            Me.OldPasswordTextBox.DisabledTab = False
            Me.OldPasswordTextBox.EnabledShortCuts = False
            Me.OldPasswordTextBox.fk_Campo = 0
            Me.OldPasswordTextBox.fk_Documento = 0
            Me.OldPasswordTextBox.fk_Validacion = 0
            Me.OldPasswordTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.OldPasswordTextBox.FocusOut = System.Drawing.Color.White
            Me.OldPasswordTextBox.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
            Me.OldPasswordTextBox.Location = New System.Drawing.Point(201, 47)
            Me.OldPasswordTextBox.MaskedTextBox_Property = ""
            Me.OldPasswordTextBox.MaximumLength = CType(0, Short)
            Me.OldPasswordTextBox.MinimumLength = CType(0, Short)
            Me.OldPasswordTextBox.Name = "OldPasswordTextBox"
            Me.OldPasswordTextBox.Obligatorio = False
            Me.OldPasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.OldPasswordTextBox.permitePegar = False
            Rango3.MaxValue = 2147483647.0R
            Rango3.MinValue = 0R
            Me.OldPasswordTextBox.Rango = Rango3
            Me.OldPasswordTextBox.Size = New System.Drawing.Size(214, 26)
            Me.OldPasswordTextBox.TabIndex = 1
            Me.OldPasswordTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.OldPasswordTextBox.Usa_Decimales = False
            Me.OldPasswordTextBox.Validos_Cantidad_Puntos = False
            '
            'OldPasswordLabel
            '
            Me.OldPasswordLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OldPasswordLabel.Location = New System.Drawing.Point(198, 27)
            Me.OldPasswordLabel.Name = "OldPasswordLabel"
            Me.OldPasswordLabel.Size = New System.Drawing.Size(136, 16)
            Me.OldPasswordLabel.TabIndex = 0
            Me.OldPasswordLabel.Text = "Contraseña anterior"
            '
            'NewPasswordLabel
            '
            Me.NewPasswordLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NewPasswordLabel.Location = New System.Drawing.Point(198, 80)
            Me.NewPasswordLabel.Name = "NewPasswordLabel"
            Me.NewPasswordLabel.Size = New System.Drawing.Size(136, 16)
            Me.NewPasswordLabel.TabIndex = 2
            Me.NewPasswordLabel.Text = "Nueva contraseña"
            '
            'LoginPictureBox
            '
            Me.LoginPictureBox.Image = Global.Miharu.Desktop.My.Resources.Resources.password
            Me.LoginPictureBox.Location = New System.Drawing.Point(8, 16)
            Me.LoginPictureBox.Name = "LoginPictureBox"
            Me.LoginPictureBox.Size = New System.Drawing.Size(172, 164)
            Me.LoginPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.LoginPictureBox.TabIndex = 0
            Me.LoginPictureBox.TabStop = False
            '
            'FormNewPassword
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(435, 261)
            Me.Controls.Add(Me.gbxLogin)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormNewPassword"
            Me.Padding = New System.Windows.Forms.Padding(5)
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Miharu DDO [Password]"
            Me.gbxLogin.ResumeLayout(False)
            Me.gbxLogin.PerformLayout()
            CType(Me.LoginPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents gbxLogin As System.Windows.Forms.GroupBox
        Friend WithEvents NewPasswordTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents OldPasswordTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents OldPasswordLabel As System.Windows.Forms.Label
        Friend WithEvents NewPasswordLabel As System.Windows.Forms.Label
        Friend WithEvents LoginPictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents ConfirmPasswordTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents CnfirmPasswordLabel As System.Windows.Forms.Label

    End Class
End Namespace