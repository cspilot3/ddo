Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.Despacho
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormGuiaDespacho
        Inherits Miharu.Desktop.Library.FormBase

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
            Dim Rango1 As Rango = New Rango()
            Dim Rango2 As Rango = New Rango()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.GuiaDesktopTextBox = New DesktopTextBoxControl()
            Me.SelloDesktopTextBox = New DesktopTextBoxControl()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(27, 25)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(32, 13)
            Me.Label1.TabIndex = 33
            Me.Label1.Text = "Guia"
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.AceptarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnAgregar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(121, 93)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(89, 30)
            Me.AceptarButton.TabIndex = 2
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(216, 93)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 30)
            Me.CerrarButton.TabIndex = 3
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(27, 58)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(34, 13)
            Me.Label2.TabIndex = 34
            Me.Label2.Text = "Sello"
            '
            'GuiaDesktopTextBox
            '
            Me.GuiaDesktopTextBox.DisabledEnter = False
            Me.GuiaDesktopTextBox.DisabledTab = False
            Me.GuiaDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.GuiaDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.GuiaDesktopTextBox.Location = New System.Drawing.Point(86, 25)
            Me.GuiaDesktopTextBox.Name = "GuiaDesktopTextBox"
            Rango1.MaxValue = 2147483647
            Rango1.MinValue = 0
            Me.GuiaDesktopTextBox.Rango = Rango1
            Me.GuiaDesktopTextBox.Size = New System.Drawing.Size(218, 21)
            Me.GuiaDesktopTextBox.TabIndex = 0
            Me.GuiaDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            '
            'SelloDesktopTextBox
            '
            Me.SelloDesktopTextBox.DisabledEnter = False
            Me.SelloDesktopTextBox.DisabledTab = False
            Me.SelloDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.SelloDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.SelloDesktopTextBox.Location = New System.Drawing.Point(86, 55)
            Me.SelloDesktopTextBox.Name = "SelloDesktopTextBox"
            Rango2.MaxValue = 2147483647
            Rango2.MinValue = 0
            Me.SelloDesktopTextBox.Rango = Rango2
            Me.SelloDesktopTextBox.Size = New System.Drawing.Size(218, 21)
            Me.SelloDesktopTextBox.TabIndex = 1
            Me.SelloDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            '
            'FormGuiaDespacho
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(324, 135)
            Me.Controls.Add(Me.SelloDesktopTextBox)
            Me.Controls.Add(Me.GuiaDesktopTextBox)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormGuiaDespacho"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Guia de Despacho"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents GuiaDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents SelloDesktopTextBox As DesktopTextBoxControl
    End Class
End Namespace