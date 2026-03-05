Imports Miharu.Desktop.Controls.DesktopTextBox

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAutorizacion
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
        Dim Rango1 As Rango = New Rango()
        Dim Rango2 As Rango = New Rango()
        Me.TextoAutorizacionTextBox = New System.Windows.Forms.TextBox()
        Me.AceptarButton = New System.Windows.Forms.Button()
        Me.CancelarButton = New System.Windows.Forms.Button()
        Me.PasswordTextBox = New DesktopTextBoxControl()
        Me.LoginTextBox = New DesktopTextBoxControl()
        Me.lblLogin = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TextoAutorizacionTextBox
        '
        Me.TextoAutorizacionTextBox.Location = New System.Drawing.Point(14, 36)
        Me.TextoAutorizacionTextBox.Multiline = True
        Me.TextoAutorizacionTextBox.Name = "TextoAutorizacionTextBox"
        Me.TextoAutorizacionTextBox.ReadOnly = True
        Me.TextoAutorizacionTextBox.Size = New System.Drawing.Size(432, 85)
        Me.TextoAutorizacionTextBox.TabIndex = 1
        Me.TextoAutorizacionTextBox.TabStop = False
        '
        'AceptarButton
        '
        Me.AceptarButton.Image = Global.Miharu.Desktop.Library.My.Resources.Resources.Aceptar
        Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AceptarButton.Location = New System.Drawing.Point(258, 257)
        Me.AceptarButton.Name = "AceptarButton"
        Me.AceptarButton.Size = New System.Drawing.Size(87, 23)
        Me.AceptarButton.TabIndex = 6
        Me.AceptarButton.Text = "     Aceptar"
        Me.AceptarButton.UseVisualStyleBackColor = True
        '
        'CancelarButton
        '
        Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelarButton.Image = Global.Miharu.Desktop.Library.My.Resources.Resources.btnSalir
        Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelarButton.Location = New System.Drawing.Point(359, 257)
        Me.CancelarButton.Name = "CancelarButton"
        Me.CancelarButton.Size = New System.Drawing.Size(87, 23)
        Me.CancelarButton.TabIndex = 7
        Me.CancelarButton.Text = "    Cancelar"
        Me.CancelarButton.UseVisualStyleBackColor = True
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox.Cantidad_Decimales = CType(0, Short)
        Me.PasswordTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
        Me.PasswordTextBox.DisabledEnter = False
        Me.PasswordTextBox.DisabledTab = False
        Me.PasswordTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.PasswordTextBox.FocusOut = System.Drawing.Color.White
        Me.PasswordTextBox.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.PasswordTextBox.Location = New System.Drawing.Point(14, 212)
        Me.PasswordTextBox.MaximumLength = CType(0, Short)
        Me.PasswordTextBox.MaxLength = 0
        Me.PasswordTextBox.MinimumLength = CType(0, Short)
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Rango1.MaxValue = 2147483647.0R
        Rango1.MinValue = 0.0R
        Me.PasswordTextBox.Rango = Rango1
        Me.PasswordTextBox.ShortcutsEnabled = False
        Me.PasswordTextBox.Size = New System.Drawing.Size(432, 26)
        Me.PasswordTextBox.TabIndex = 5
        Me.PasswordTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
        Me.PasswordTextBox.Usa_Decimales = False
        '
        'LoginTextBox
        '
        Me.LoginTextBox.Cantidad_Decimales = CType(0, Short)
        Me.LoginTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
        Me.LoginTextBox.DisabledEnter = False
        Me.LoginTextBox.DisabledTab = False        
        Me.LoginTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.LoginTextBox.FocusOut = System.Drawing.Color.White
        Me.LoginTextBox.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LoginTextBox.Location = New System.Drawing.Point(14, 159)
        Me.LoginTextBox.MaximumLength = CType(0, Short)
        Me.LoginTextBox.MaxLength = 0
        Me.LoginTextBox.MinimumLength = CType(0, Short)
        Me.LoginTextBox.Name = "LoginTextBox"
        Rango2.MaxValue = 2147483647.0R
        Rango2.MinValue = 0.0R
        Me.LoginTextBox.Rango = Rango2
        Me.LoginTextBox.ShortcutsEnabled = False
        Me.LoginTextBox.Size = New System.Drawing.Size(432, 26)
        Me.LoginTextBox.TabIndex = 3
        Me.LoginTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
        Me.LoginTextBox.Usa_Decimales = False
        '
        'lblLogin
        '
        Me.lblLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogin.Location = New System.Drawing.Point(11, 139)
        Me.lblLogin.Name = "lblLogin"
        Me.lblLogin.Size = New System.Drawing.Size(136, 16)
        Me.lblLogin.TabIndex = 2
        Me.lblLogin.Text = "Usuario:"
        '
        'lblPassword
        '
        Me.lblPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.Location = New System.Drawing.Point(11, 192)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(136, 16)
        Me.lblPassword.TabIndex = 4
        Me.lblPassword.Text = "Contraseña:"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Accion a realizar"
        '
        'FormAutorizacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CancelarButton
        Me.ClientSize = New System.Drawing.Size(461, 293)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PasswordTextBox)
        Me.Controls.Add(Me.LoginTextBox)
        Me.Controls.Add(Me.lblLogin)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.CancelarButton)
        Me.Controls.Add(Me.AceptarButton)
        Me.Controls.Add(Me.TextoAutorizacionTextBox)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormAutorizacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Autorizar"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextoAutorizacionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AceptarButton As System.Windows.Forms.Button
    Friend WithEvents CancelarButton As System.Windows.Forms.Button
    Friend WithEvents PasswordTextBox As DesktopTextBoxControl
    Friend WithEvents LoginTextBox As DesktopTextBoxControl
    Friend WithEvents lblLogin As System.Windows.Forms.Label
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
