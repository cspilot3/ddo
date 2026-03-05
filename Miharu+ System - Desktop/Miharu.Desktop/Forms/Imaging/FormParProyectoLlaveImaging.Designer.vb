Namespace Forms.Imaging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParProyectoLlaveImaging
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormParProyectoLlaveImaging))
            Me.LlavesGroupBox = New System.Windows.Forms.GroupBox()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.PruebaConfiguracionGroupBox = New System.Windows.Forms.GroupBox()
            Me.PruebaButton = New System.Windows.Forms.Button()
            Me.ValorPruebaDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.PruebaConfiguracionGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'LlavesGroupBox
            '
            Me.LlavesGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                               Or System.Windows.Forms.AnchorStyles.Left) _
                                              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LlavesGroupBox.Location = New System.Drawing.Point(13, 13)
            Me.LlavesGroupBox.Name = "LlavesGroupBox"
            Me.LlavesGroupBox.Size = New System.Drawing.Size(356, 239)
            Me.LlavesGroupBox.TabIndex = 0
            Me.LlavesGroupBox.TabStop = False
            Me.LlavesGroupBox.Text = "Llaves del Proyecto"
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(183, 316)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(90, 30)
            Me.GuardarButton.TabIndex = 2
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(279, 316)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 3
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'PruebaConfiguracionGroupBox
            '
            Me.PruebaConfiguracionGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                                                           Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PruebaConfiguracionGroupBox.Controls.Add(Me.PruebaButton)
            Me.PruebaConfiguracionGroupBox.Controls.Add(Me.ValorPruebaDesktopTextBox)
            Me.PruebaConfiguracionGroupBox.Controls.Add(Me.Label1)
            Me.PruebaConfiguracionGroupBox.Location = New System.Drawing.Point(13, 258)
            Me.PruebaConfiguracionGroupBox.Name = "PruebaConfiguracionGroupBox"
            Me.PruebaConfiguracionGroupBox.Size = New System.Drawing.Size(356, 52)
            Me.PruebaConfiguracionGroupBox.TabIndex = 1
            Me.PruebaConfiguracionGroupBox.TabStop = False
            Me.PruebaConfiguracionGroupBox.Text = "Prueba de Configuración"
            '
            'PruebaButton
            '
            Me.PruebaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.PruebaButton.Image = Global.Miharu.Desktop.My.Resources.Resources.MainService
            Me.PruebaButton.Location = New System.Drawing.Point(312, 17)
            Me.PruebaButton.Name = "PruebaButton"
            Me.PruebaButton.Size = New System.Drawing.Size(29, 24)
            Me.PruebaButton.TabIndex = 1
            Me.PruebaButton.UseVisualStyleBackColor = True
            '
            'ValorPruebaDesktopTextBox
            '
            Me.ValorPruebaDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.ValorPruebaDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ValorPruebaDesktopTextBox.DateFormat = Nothing
            Me.ValorPruebaDesktopTextBox.DisabledEnter = False
            Me.ValorPruebaDesktopTextBox.DisabledTab = False
            Me.ValorPruebaDesktopTextBox.EnabledShortCuts = False
            Me.ValorPruebaDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ValorPruebaDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ValorPruebaDesktopTextBox.Location = New System.Drawing.Point(99, 18)
            Me.ValorPruebaDesktopTextBox.MaskedTextBox_Property = ""
            Me.ValorPruebaDesktopTextBox.MaximumLength = CType(0, Short)
            Me.ValorPruebaDesktopTextBox.MinimumLength = CType(0, Short)
            Me.ValorPruebaDesktopTextBox.Name = "ValorPruebaDesktopTextBox"
            Rango1.MaxValue = 9.2233720368547758E+18R
            Rango1.MinValue = 0.0R
            Me.ValorPruebaDesktopTextBox.Rango = Rango1
            Me.ValorPruebaDesktopTextBox.Size = New System.Drawing.Size(207, 21)
            Me.ValorPruebaDesktopTextBox.TabIndex = 0
            Me.ValorPruebaDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ValorPruebaDesktopTextBox.Usa_Decimales = False
            Me.ValorPruebaDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(7, 21)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(86, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Ingrese valor:"
            '
            'FormParProyectoLlaveImaging
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(381, 353)
            Me.Controls.Add(Me.PruebaConfiguracionGroupBox)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.LlavesGroupBox)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormParProyectoLlaveImaging"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Configuración Llaves"
            Me.PruebaConfiguracionGroupBox.ResumeLayout(False)
            Me.PruebaConfiguracionGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents LlavesGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents PruebaConfiguracionGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents PruebaButton As System.Windows.Forms.Button
        Friend WithEvents ValorPruebaDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace