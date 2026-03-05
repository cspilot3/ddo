Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.MesaControlFisicos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSegundaCapturarDataAdicional
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSegundaCapturarDataAdicional))
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.CamposGroupBox = New System.Windows.Forms.GroupBox()
            Me.CamposPanel = New System.Windows.Forms.Panel()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.CapturaLabel = New System.Windows.Forms.Label()
            Me.EsquemaLabel = New System.Windows.Forms.Label()
            Me.ProyectoLabel = New System.Windows.Forms.Label()
            Me.EntidadLabel = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label11 = New System.Windows.Forms.Label()
            Me.Label12 = New System.Windows.Forms.Label()
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CBarrasLabel = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.TipologiaLabel = New System.Windows.Forms.Label()
            Me.FoliosMontoGroupBox = New System.Windows.Forms.GroupBox()
            Me.MontoDesktopTextBox = New DesktopTextBoxControl()
            Me.MontoLabel = New System.Windows.Forms.Label()
            Me.FoliosLabel = New System.Windows.Forms.Label()
            Me.FoliosDesktopTextBox = New DesktopTextBoxControl()
            Me.CamposGroupBox.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox3.SuspendLayout()
            Me.FoliosMontoGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(522, 337)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(86, 29)
            Me.CerrarButton.TabIndex = 20
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(424, 337)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(92, 29)
            Me.AceptarButton.TabIndex = 19
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'CamposGroupBox
            '
            Me.CamposGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.CamposGroupBox.Controls.Add(Me.CamposPanel)
            Me.CamposGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CamposGroupBox.Location = New System.Drawing.Point(10, 119)
            Me.CamposGroupBox.Name = "CamposGroupBox"
            Me.CamposGroupBox.Size = New System.Drawing.Size(598, 212)
            Me.CamposGroupBox.TabIndex = 18
            Me.CamposGroupBox.TabStop = False
            Me.CamposGroupBox.Text = "Campos"
            '
            'CamposPanel
            '
            Me.CamposPanel.AutoScroll = True
            Me.CamposPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CamposPanel.Location = New System.Drawing.Point(3, 16)
            Me.CamposPanel.Margin = New System.Windows.Forms.Padding(10)
            Me.CamposPanel.Name = "CamposPanel"
            Me.CamposPanel.Size = New System.Drawing.Size(592, 193)
            Me.CamposPanel.TabIndex = 3
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.GroupBox1.Controls.Add(Me.CapturaLabel)
            Me.GroupBox1.Controls.Add(Me.EsquemaLabel)
            Me.GroupBox1.Controls.Add(Me.ProyectoLabel)
            Me.GroupBox1.Controls.Add(Me.EntidadLabel)
            Me.GroupBox1.Controls.Add(Me.Label5)
            Me.GroupBox1.Controls.Add(Me.Label11)
            Me.GroupBox1.Controls.Add(Me.Label12)
            Me.GroupBox1.Location = New System.Drawing.Point(345, 2)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(263, 111)
            Me.GroupBox1.TabIndex = 17
            Me.GroupBox1.TabStop = False
            '
            'CapturaLabel
            '
            Me.CapturaLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CapturaLabel.AutoSize = True
            Me.CapturaLabel.BackColor = System.Drawing.SystemColors.Control
            Me.CapturaLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CapturaLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.CapturaLabel.Location = New System.Drawing.Point(96, 17)
            Me.CapturaLabel.Name = "CapturaLabel"
            Me.CapturaLabel.Size = New System.Drawing.Size(19, 19)
            Me.CapturaLabel.TabIndex = 12
            Me.CapturaLabel.Text = "_"
            Me.CapturaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'EsquemaLabel
            '
            Me.EsquemaLabel.AutoSize = True
            Me.EsquemaLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EsquemaLabel.Location = New System.Drawing.Point(97, 89)
            Me.EsquemaLabel.Name = "EsquemaLabel"
            Me.EsquemaLabel.Size = New System.Drawing.Size(13, 13)
            Me.EsquemaLabel.TabIndex = 12
            Me.EsquemaLabel.Text = "_"
            '
            'ProyectoLabel
            '
            Me.ProyectoLabel.AutoSize = True
            Me.ProyectoLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ProyectoLabel.Location = New System.Drawing.Point(97, 66)
            Me.ProyectoLabel.Name = "ProyectoLabel"
            Me.ProyectoLabel.Size = New System.Drawing.Size(13, 13)
            Me.ProyectoLabel.TabIndex = 11
            Me.ProyectoLabel.Text = "_"
            '
            'EntidadLabel
            '
            Me.EntidadLabel.AutoSize = True
            Me.EntidadLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EntidadLabel.Location = New System.Drawing.Point(97, 45)
            Me.EntidadLabel.Name = "EntidadLabel"
            Me.EntidadLabel.Size = New System.Drawing.Size(13, 13)
            Me.EntidadLabel.TabIndex = 10
            Me.EntidadLabel.Text = "_"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label5.Location = New System.Drawing.Point(11, 45)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(49, 13)
            Me.Label5.TabIndex = 6
            Me.Label5.Text = "Cliente:"
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label11.Location = New System.Drawing.Point(11, 89)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(61, 13)
            Me.Label11.TabIndex = 8
            Me.Label11.Text = "Esquema:"
            '
            'Label12
            '
            Me.Label12.AutoSize = True
            Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label12.Location = New System.Drawing.Point(11, 66)
            Me.Label12.Name = "Label12"
            Me.Label12.Size = New System.Drawing.Size(61, 13)
            Me.Label12.TabIndex = 7
            Me.Label12.Text = "Proyecto:"
            '
            'GroupBox3
            '
            Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.GroupBox3.Controls.Add(Me.Label1)
            Me.GroupBox3.Controls.Add(Me.CBarrasLabel)
            Me.GroupBox3.Controls.Add(Me.Label3)
            Me.GroupBox3.Controls.Add(Me.TipologiaLabel)
            Me.GroupBox3.Location = New System.Drawing.Point(10, 2)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Size = New System.Drawing.Size(329, 74)
            Me.GroupBox3.TabIndex = 16
            Me.GroupBox3.TabStop = False
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(9, 17)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(158, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Código Barras Documento:"
            '
            'CBarrasLabel
            '
            Me.CBarrasLabel.AutoSize = True
            Me.CBarrasLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.CBarrasLabel.Location = New System.Drawing.Point(178, 17)
            Me.CBarrasLabel.Name = "CBarrasLabel"
            Me.CBarrasLabel.Size = New System.Drawing.Size(14, 13)
            Me.CBarrasLabel.TabIndex = 1
            Me.CBarrasLabel.Text = "_"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(8, 43)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(65, 13)
            Me.Label3.TabIndex = 2
            Me.Label3.Text = "Tipología:"
            '
            'TipologiaLabel
            '
            Me.TipologiaLabel.AutoSize = True
            Me.TipologiaLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.TipologiaLabel.Location = New System.Drawing.Point(178, 43)
            Me.TipologiaLabel.Name = "TipologiaLabel"
            Me.TipologiaLabel.Size = New System.Drawing.Size(14, 13)
            Me.TipologiaLabel.TabIndex = 3
            Me.TipologiaLabel.Text = "_"
            '
            'FoliosMontoGroupBox
            '
            Me.FoliosMontoGroupBox.Controls.Add(Me.MontoDesktopTextBox)
            Me.FoliosMontoGroupBox.Controls.Add(Me.MontoLabel)
            Me.FoliosMontoGroupBox.Controls.Add(Me.FoliosLabel)
            Me.FoliosMontoGroupBox.Controls.Add(Me.FoliosDesktopTextBox)
            Me.FoliosMontoGroupBox.Location = New System.Drawing.Point(10, 77)
            Me.FoliosMontoGroupBox.Name = "FoliosMontoGroupBox"
            Me.FoliosMontoGroupBox.Size = New System.Drawing.Size(329, 36)
            Me.FoliosMontoGroupBox.TabIndex = 0
            Me.FoliosMontoGroupBox.TabStop = False
            Me.FoliosMontoGroupBox.Visible = False
            '
            'MontoDesktopTextBox
            '
            Me.MontoDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.MontoDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.MontoDesktopTextBox.DateFormat = Nothing
            Me.MontoDesktopTextBox.DisabledEnter = True
            Me.MontoDesktopTextBox.DisabledTab = True
            Me.MontoDesktopTextBox.EnabledShortCuts = False
            Me.MontoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.MontoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.MontoDesktopTextBox.Location = New System.Drawing.Point(188, 11)
            Me.MontoDesktopTextBox.MaskedTextBox_Property = ""
            Me.MontoDesktopTextBox.MaximumLength = CType(0, Short)
            Me.MontoDesktopTextBox.MinimumLength = CType(0, Short)
            Me.MontoDesktopTextBox.Name = "MontoDesktopTextBox"
            Rango1.MaxValue = 9.2233720368547758E+18R
            Rango1.MinValue = 0.0R
            Me.MontoDesktopTextBox.Rango = Rango1
            Me.MontoDesktopTextBox.Size = New System.Drawing.Size(130, 21)
            Me.MontoDesktopTextBox.TabIndex = 2
            Me.MontoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            Me.MontoDesktopTextBox.Usa_Decimales = False
            Me.MontoDesktopTextBox.Validos_Cantidad_Puntos = False
            Me.MontoDesktopTextBox.Visible = False
            '
            'MontoLabel
            '
            Me.MontoLabel.AutoSize = True
            Me.MontoLabel.Location = New System.Drawing.Point(136, 14)
            Me.MontoLabel.Name = "MontoLabel"
            Me.MontoLabel.Size = New System.Drawing.Size(46, 13)
            Me.MontoLabel.TabIndex = 9
            Me.MontoLabel.Text = "Monto:"
            Me.MontoLabel.Visible = False
            '
            'FoliosLabel
            '
            Me.FoliosLabel.AutoSize = True
            Me.FoliosLabel.Location = New System.Drawing.Point(16, 14)
            Me.FoliosLabel.Name = "FoliosLabel"
            Me.FoliosLabel.Size = New System.Drawing.Size(42, 13)
            Me.FoliosLabel.TabIndex = 8
            Me.FoliosLabel.Text = "Folios:"
            Me.FoliosLabel.Visible = False
            '
            'FoliosDesktopTextBox
            '
            Me.FoliosDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.FoliosDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.FoliosDesktopTextBox.DateFormat = Nothing
            Me.FoliosDesktopTextBox.DisabledEnter = False
            Me.FoliosDesktopTextBox.DisabledTab = False
            Me.FoliosDesktopTextBox.EnabledShortCuts = False
            Me.FoliosDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.FoliosDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.FoliosDesktopTextBox.Location = New System.Drawing.Point(64, 11)
            Me.FoliosDesktopTextBox.MaskedTextBox_Property = ""
            Me.FoliosDesktopTextBox.MaximumLength = CType(0, Short)
            Me.FoliosDesktopTextBox.MinimumLength = CType(0, Short)
            Me.FoliosDesktopTextBox.Name = "FoliosDesktopTextBox"
            Rango2.MaxValue = 2147483647.0R
            Rango2.MinValue = 1.0R
            Me.FoliosDesktopTextBox.Rango = Rango2
            Me.FoliosDesktopTextBox.Size = New System.Drawing.Size(57, 21)
            Me.FoliosDesktopTextBox.TabIndex = 1
            Me.FoliosDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            Me.FoliosDesktopTextBox.Usa_Decimales = False
            Me.FoliosDesktopTextBox.Validos_Cantidad_Puntos = False
            Me.FoliosDesktopTextBox.Visible = False
            '
            'FormSegundaCapturarDataAdicional
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(621, 373)
            Me.Controls.Add(Me.FoliosMontoGroupBox)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.CamposGroupBox)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.GroupBox3)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormSegundaCapturarDataAdicional"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Segunda Capturar de datos"
            Me.CamposGroupBox.ResumeLayout(False)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBox3.ResumeLayout(False)
            Me.GroupBox3.PerformLayout()
            Me.FoliosMontoGroupBox.ResumeLayout(False)
            Me.FoliosMontoGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CamposGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CamposPanel As System.Windows.Forms.Panel
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents CapturaLabel As System.Windows.Forms.Label
        Friend WithEvents EsquemaLabel As System.Windows.Forms.Label
        Friend WithEvents ProyectoLabel As System.Windows.Forms.Label
        Friend WithEvents EntidadLabel As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label11 As System.Windows.Forms.Label
        Friend WithEvents Label12 As System.Windows.Forms.Label
        Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CBarrasLabel As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents TipologiaLabel As System.Windows.Forms.Label
        Friend WithEvents FoliosMontoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents MontoDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents MontoLabel As System.Windows.Forms.Label
        Friend WithEvents FoliosLabel As System.Windows.Forms.Label
        Friend WithEvents FoliosDesktopTextBox As DesktopTextBoxControl
    End Class
End Namespace