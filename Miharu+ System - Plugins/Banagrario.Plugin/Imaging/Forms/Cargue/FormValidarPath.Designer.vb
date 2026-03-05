Imports Miharu.Desktop.Controls.DesktopTextBox

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormValidarPath
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
        Dim Rango2 As Rango = New Rango()
        Dim Rango1 As Rango = New Rango()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormValidarPath))
        Me.ValidacionGroupBox = New System.Windows.Forms.GroupBox()
        Me.OficinaDesktopTextBox = New DesktopTextBoxControl()
        Me.FechaLabel = New System.Windows.Forms.Label()
        Me.OficinaLabel = New System.Windows.Forms.Label()
        Me.FechaDesktopTextBox = New DesktopTextBoxControl()
        Me.AceptarButton = New System.Windows.Forms.Button()
        Me.ValidacionGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'ValidacionGroupBox
        '
        Me.ValidacionGroupBox.Controls.Add(Me.AceptarButton)
        Me.ValidacionGroupBox.Controls.Add(Me.FechaDesktopTextBox)
        Me.ValidacionGroupBox.Controls.Add(Me.OficinaDesktopTextBox)
        Me.ValidacionGroupBox.Controls.Add(Me.FechaLabel)
        Me.ValidacionGroupBox.Controls.Add(Me.OficinaLabel)
        Me.ValidacionGroupBox.Location = New System.Drawing.Point(15, 12)
        Me.ValidacionGroupBox.Name = "ValidacionGroupBox"
        Me.ValidacionGroupBox.Size = New System.Drawing.Size(210, 132)
        Me.ValidacionGroupBox.TabIndex = 0
        Me.ValidacionGroupBox.TabStop = False
        '
        'OficinaDesktopTextBox
        '
        Me.OficinaDesktopTextBox.Cantidad_Decimales = CType(0, Short)
        Me.OficinaDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
        Me.OficinaDesktopTextBox.DisabledEnter = False
        Me.OficinaDesktopTextBox.DisabledTab = False
        Me.OficinaDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.OficinaDesktopTextBox.FocusOut = System.Drawing.Color.White
        Me.OficinaDesktopTextBox.Location = New System.Drawing.Point(90, 23)
        Me.OficinaDesktopTextBox.MaximumLength = CType(0, Short)
        Me.OficinaDesktopTextBox.MaxLength = 4
        Me.OficinaDesktopTextBox.MinimumLength = CType(4, Short)
        Me.OficinaDesktopTextBox.Name = "OficinaDesktopTextBox"
        Rango2.MaxValue = 1.7976931348623157E+308R
        Rango2.MinValue = 0.0R
        Me.OficinaDesktopTextBox.Rango = Rango2
        Me.OficinaDesktopTextBox.ShortcutsEnabled = False
        Me.OficinaDesktopTextBox.Size = New System.Drawing.Size(100, 20)
        Me.OficinaDesktopTextBox.TabIndex = 0
        Me.OficinaDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
        Me.OficinaDesktopTextBox.Usa_Decimales = False
        '
        'FechaLabel
        '
        Me.FechaLabel.AutoSize = True
        Me.FechaLabel.Location = New System.Drawing.Point(21, 59)
        Me.FechaLabel.Name = "FechaLabel"
        Me.FechaLabel.Size = New System.Drawing.Size(40, 13)
        Me.FechaLabel.TabIndex = 1
        Me.FechaLabel.Text = "Fecha:"
        '
        'OficinaLabel
        '
        Me.OficinaLabel.AutoSize = True
        Me.OficinaLabel.Location = New System.Drawing.Point(21, 26)
        Me.OficinaLabel.Name = "OficinaLabel"
        Me.OficinaLabel.Size = New System.Drawing.Size(43, 13)
        Me.OficinaLabel.TabIndex = 0
        Me.OficinaLabel.Text = "Oficina:"
        '
        'FechaDesktopTextBox
        '
        Me.FechaDesktopTextBox.Cantidad_Decimales = CType(0, Short)
        Me.FechaDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
        Me.FechaDesktopTextBox.DisabledEnter = False
        Me.FechaDesktopTextBox.DisabledTab = False
        Me.FechaDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.FechaDesktopTextBox.FocusOut = System.Drawing.Color.White
        Me.FechaDesktopTextBox.Location = New System.Drawing.Point(90, 56)
        Me.FechaDesktopTextBox.MaximumLength = CType(0, Short)
        Me.FechaDesktopTextBox.MaxLength = 0
        Me.FechaDesktopTextBox.MinimumLength = CType(0, Short)
        Me.FechaDesktopTextBox.Name = "FechaDesktopTextBox"
        Rango1.MaxValue = 1.7976931348623157E+308R
        Rango1.MinValue = 0.0R
        Me.FechaDesktopTextBox.Rango = Rango1
        Me.FechaDesktopTextBox.ShortcutsEnabled = False
        Me.FechaDesktopTextBox.Size = New System.Drawing.Size(100, 20)
        Me.FechaDesktopTextBox.TabIndex = 1
        Me.FechaDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Fecha
        Me.FechaDesktopTextBox.Usa_Decimales = False
        '
        'AceptarButton
        '
        Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AceptarButton.Image = Global.Banagrario.Plugin.My.Resources.tick
        Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AceptarButton.Location = New System.Drawing.Point(65, 93)
        Me.AceptarButton.Name = "AceptarButton"
        Me.AceptarButton.Size = New System.Drawing.Size(80, 23)
        Me.AceptarButton.TabIndex = 2
        Me.AceptarButton.Text = "&Aceptar"
        Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FormValidarPath
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(241, 156)
        Me.Controls.Add(Me.ValidacionGroupBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormValidarPath"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Validación Path Cargue"
        Me.ValidacionGroupBox.ResumeLayout(False)
        Me.ValidacionGroupBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ValidacionGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents OficinaDesktopTextBox As DesktopTextBoxControl
    Friend WithEvents FechaLabel As System.Windows.Forms.Label
    Friend WithEvents OficinaLabel As System.Windows.Forms.Label
    Friend WithEvents FechaDesktopTextBox As DesktopTextBoxControl
    Friend WithEvents AceptarButton As System.Windows.Forms.Button
End Class
