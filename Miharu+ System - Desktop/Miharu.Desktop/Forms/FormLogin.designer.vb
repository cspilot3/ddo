<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormLogin
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
        Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLogin))
        Me.gbxLogin = New System.Windows.Forms.GroupBox()
        Me.PasswordTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
        Me.CancelarButton = New System.Windows.Forms.Button()
        Me.AceptarButton = New System.Windows.Forms.Button()
        Me.LoginTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
        Me.LoginLabel = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.LoginPictureBox = New System.Windows.Forms.PictureBox()
        Me.gbxLogin.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoginPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbxLogin
        '
        Me.gbxLogin.Controls.Add(Me.PictureBox2)
        Me.gbxLogin.Controls.Add(Me.PasswordTextBox)
        Me.gbxLogin.Controls.Add(Me.CancelarButton)
        Me.gbxLogin.Controls.Add(Me.AceptarButton)
        Me.gbxLogin.Controls.Add(Me.LoginTextBox)
        Me.gbxLogin.Controls.Add(Me.LoginLabel)
        Me.gbxLogin.Controls.Add(Me.PasswordLabel)
        Me.gbxLogin.Controls.Add(Me.LoginPictureBox)
        Me.gbxLogin.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbxLogin.Location = New System.Drawing.Point(0, 34)
        Me.gbxLogin.Margin = New System.Windows.Forms.Padding(0)
        Me.gbxLogin.Name = "gbxLogin"
        Me.gbxLogin.Padding = New System.Windows.Forms.Padding(0)
        Me.gbxLogin.Size = New System.Drawing.Size(427, 180)
        Me.gbxLogin.TabIndex = 0
        Me.gbxLogin.TabStop = False
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox._Obligatorio = False
        Me.PasswordTextBox._PermitePegar = False
        Me.PasswordTextBox.Cantidad_Decimales = CType(0, Short)
        Me.PasswordTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
        Me.PasswordTextBox.DateFormat = Nothing
        Me.PasswordTextBox.DisabledEnter = False
        Me.PasswordTextBox.DisabledTab = False
        Me.PasswordTextBox.EnabledShortCuts = False
        Me.PasswordTextBox.fk_Campo = 0
        Me.PasswordTextBox.fk_Documento = 0
        Me.PasswordTextBox.fk_Validacion = 0
        Me.PasswordTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.PasswordTextBox.FocusOut = System.Drawing.Color.White
        Me.PasswordTextBox.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.PasswordTextBox.Location = New System.Drawing.Point(191, 89)
        Me.PasswordTextBox.MaskedTextBox_Property = ""
        Me.PasswordTextBox.MaximumLength = CType(0, Short)
        Me.PasswordTextBox.MinimumLength = CType(0, Short)
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.Obligatorio = False
        Me.PasswordTextBox.permitePegar = False
        Rango1.MaxValue = 2147483647.0R
        Rango1.MinValue = 0R
        Me.PasswordTextBox.Rango = Rango1
        Me.PasswordTextBox.Size = New System.Drawing.Size(214, 26)
        Me.PasswordTextBox.TabIndex = 3
        Me.ToolTip.SetToolTip(Me.PasswordTextBox, "Contraseña de acceso al sistema.")
        Me.PasswordTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
        Me.PasswordTextBox.Usa_Decimales = False
        Me.PasswordTextBox.UseSystemPasswordChar = True
        Me.PasswordTextBox.Validos_Cantidad_Puntos = False
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
        Me.CancelarButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelarButton.Location = New System.Drawing.Point(316, 130)
        Me.CancelarButton.Name = "CancelarButton"
        Me.CancelarButton.Size = New System.Drawing.Size(96, 35)
        Me.CancelarButton.TabIndex = 5
        Me.CancelarButton.Text = "&Cancelar"
        Me.CancelarButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip.SetToolTip(Me.CancelarButton, "Cancelar inicio de sesión.")
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
        Me.AceptarButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.AceptarButton.Location = New System.Drawing.Point(198, 130)
        Me.AceptarButton.Name = "AceptarButton"
        Me.AceptarButton.Size = New System.Drawing.Size(93, 35)
        Me.AceptarButton.TabIndex = 4
        Me.AceptarButton.Text = "&Aceptar"
        Me.AceptarButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip.SetToolTip(Me.AceptarButton, "Iniciar Sesión.")
        '
        'LoginTextBox
        '
        Me.LoginTextBox._Obligatorio = False
        Me.LoginTextBox._PermitePegar = False
        Me.LoginTextBox.Cantidad_Decimales = CType(0, Short)
        Me.LoginTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
        Me.LoginTextBox.DateFormat = Nothing
        Me.LoginTextBox.DisabledEnter = False
        Me.LoginTextBox.DisabledTab = False
        Me.LoginTextBox.EnabledShortCuts = False
        Me.LoginTextBox.fk_Campo = 0
        Me.LoginTextBox.fk_Documento = 0
        Me.LoginTextBox.fk_Validacion = 0
        Me.LoginTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.LoginTextBox.FocusOut = System.Drawing.Color.White
        Me.LoginTextBox.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LoginTextBox.Location = New System.Drawing.Point(191, 37)
        Me.LoginTextBox.MaskedTextBox_Property = ""
        Me.LoginTextBox.MaximumLength = CType(0, Short)
        Me.LoginTextBox.MinimumLength = CType(0, Short)
        Me.LoginTextBox.Name = "LoginTextBox"
        Me.LoginTextBox.Obligatorio = False
        Me.LoginTextBox.permitePegar = False
        Rango2.MaxValue = 2147483647.0R
        Rango2.MinValue = 0R
        Me.LoginTextBox.Rango = Rango2
        Me.LoginTextBox.Size = New System.Drawing.Size(214, 26)
        Me.LoginTextBox.TabIndex = 1
        Me.ToolTip.SetToolTip(Me.LoginTextBox, "Nombre de usuario de acceso.")
        Me.LoginTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
        Me.LoginTextBox.Usa_Decimales = False
        Me.LoginTextBox.Validos_Cantidad_Puntos = False
        '
        'LoginLabel
        '
        Me.LoginLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LoginLabel.Location = New System.Drawing.Point(188, 17)
        Me.LoginLabel.Name = "LoginLabel"
        Me.LoginLabel.Size = New System.Drawing.Size(136, 16)
        Me.LoginLabel.TabIndex = 0
        Me.LoginLabel.Text = "Usuario"
        '
        'PasswordLabel
        '
        Me.PasswordLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PasswordLabel.Location = New System.Drawing.Point(188, 70)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(136, 16)
        Me.PasswordLabel.TabIndex = 2
        Me.PasswordLabel.Text = "Contraseña"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(427, 42)
        Me.Panel2.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(269, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(139, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "MIHARU DDO"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.PictureBox1.Image = Global.Miharu.Desktop.My.Resources.Resources.LogoPYC
        Me.PictureBox1.Location = New System.Drawing.Point(26, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(128, 42)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(406, 93)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(19, 19)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'LoginPictureBox
        '
        Me.LoginPictureBox.Image = CType(resources.GetObject("LoginPictureBox.Image"), System.Drawing.Image)
        Me.LoginPictureBox.Location = New System.Drawing.Point(13, 14)
        Me.LoginPictureBox.Name = "LoginPictureBox"
        Me.LoginPictureBox.Size = New System.Drawing.Size(172, 163)
        Me.LoginPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LoginPictureBox.TabIndex = 0
        Me.LoginPictureBox.TabStop = False
        '
        'FormLogin
        '
        Me.AcceptButton = Me.AceptarButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.CancelarButton
        Me.ClientSize = New System.Drawing.Size(427, 214)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.gbxLogin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Miharu DDO [Login]"
        Me.gbxLogin.ResumeLayout(False)
        Me.gbxLogin.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LoginPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AceptarButton As System.Windows.Forms.Button
    Friend WithEvents CancelarButton As System.Windows.Forms.Button
    Friend WithEvents gbxLogin As System.Windows.Forms.GroupBox
    Friend WithEvents LoginLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents LoginPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents LoginTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
    Friend WithEvents PasswordTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox2 As PictureBox
End Class
