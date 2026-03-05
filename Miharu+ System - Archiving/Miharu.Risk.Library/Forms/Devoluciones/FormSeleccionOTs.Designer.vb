Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.Devoluciones
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeleccionOTs
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSeleccionOTs))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.OTDesktopTextBox = New DesktopTextBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(9, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(28, 13)
            Me.Label1.TabIndex = 33
            Me.Label1.Text = "OTs"
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(11, 49)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(100, 30)
            Me.AceptarButton.TabIndex = 31
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(117, 49)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(100, 30)
            Me.CerrarButton.TabIndex = 32
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'OTDesktopTextBox
            '
            Me.OTDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.OTDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.OTDesktopTextBox.DisabledEnter = False
            Me.OTDesktopTextBox.DisabledTab = False
            Me.OTDesktopTextBox.EnabledShortCuts = False
            Me.OTDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.OTDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.OTDesktopTextBox.Location = New System.Drawing.Point(62, 9)
            Me.OTDesktopTextBox.MaximumLength = CType(0, Short)
            Me.OTDesktopTextBox.MinimumLength = CType(0, Short)
            Me.OTDesktopTextBox.Name = "OTDesktopTextBox"
            Rango1.MaxValue = 9.2233720368547758E+18R
            Rango1.MinValue = 0.0R
            Me.OTDesktopTextBox.Rango = Rango1
            Me.OTDesktopTextBox.ShortcutsEnabled = False
            Me.OTDesktopTextBox.Size = New System.Drawing.Size(155, 21)
            Me.OTDesktopTextBox.TabIndex = 34
            Me.OTDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            Me.OTDesktopTextBox.Usa_Decimales = False
            Me.OTDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(246, 14)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(241, 65)
            Me.Label2.TabIndex = 35
            Me.Label2.Text = resources.GetString("Label2.Text")
            Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'FormSeleccionOTs
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(489, 95)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.OTDesktopTextBox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormSeleccionOTs"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Seleccion OT"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents OTDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
    End Class
End Namespace