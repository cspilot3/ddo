Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.MesaControlFisicos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormTerceraCapturarDataAdicional
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
            Dim Rango1 As Rango = New Rango()
            Dim Rango2 As Rango = New Rango()
            Dim Rango3 As Rango = New Rango()
            Dim Rango4 As Rango = New Rango()
            Dim Rango5 As Rango = New Rango()
            Dim Rango6 As Rango = New Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormTerceraCapturarDataAdicional))
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
            Me.Monto3TextBox = New DesktopTextBoxControl()
            Me.Folios3TextBox = New DesktopTextBoxControl()
            Me.Monto2TextBox = New DesktopTextBoxControl()
            Me.Folios2TextBox = New DesktopTextBoxControl()
            Me.Monto1TextBox = New DesktopTextBoxControl()
            Me.MontoLabel = New System.Windows.Forms.Label()
            Me.FoliosLabel = New System.Windows.Forms.Label()
            Me.Folios1TextBox = New DesktopTextBoxControl()
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
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(592, 384)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(85, 26)
            Me.CerrarButton.TabIndex = 25
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(494, 384)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(92, 26)
            Me.AceptarButton.TabIndex = 24
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'CamposGroupBox
            '
            Me.CamposGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.CamposGroupBox.Controls.Add(Me.CamposPanel)
            Me.CamposGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CamposGroupBox.Location = New System.Drawing.Point(11, 148)
            Me.CamposGroupBox.Name = "CamposGroupBox"
            Me.CamposGroupBox.Size = New System.Drawing.Size(669, 230)
            Me.CamposGroupBox.TabIndex = 23
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
            Me.CamposPanel.Size = New System.Drawing.Size(663, 211)
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
            Me.GroupBox1.Location = New System.Drawing.Point(414, 8)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(263, 111)
            Me.GroupBox1.TabIndex = 22
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
            Me.GroupBox3.Location = New System.Drawing.Point(14, 8)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Size = New System.Drawing.Size(394, 63)
            Me.GroupBox3.TabIndex = 21
            Me.GroupBox3.TabStop = False
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(9, 12)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(158, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Código Barras Documento:"
            '
            'CBarrasLabel
            '
            Me.CBarrasLabel.AutoSize = True
            Me.CBarrasLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CBarrasLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.CBarrasLabel.Location = New System.Drawing.Point(178, 12)
            Me.CBarrasLabel.Name = "CBarrasLabel"
            Me.CBarrasLabel.Size = New System.Drawing.Size(15, 15)
            Me.CBarrasLabel.TabIndex = 1
            Me.CBarrasLabel.Text = "_"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(8, 38)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(65, 13)
            Me.Label3.TabIndex = 2
            Me.Label3.Text = "Tipología:"
            '
            'TipologiaLabel
            '
            Me.TipologiaLabel.AutoSize = True
            Me.TipologiaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TipologiaLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.TipologiaLabel.Location = New System.Drawing.Point(178, 38)
            Me.TipologiaLabel.Name = "TipologiaLabel"
            Me.TipologiaLabel.Size = New System.Drawing.Size(15, 15)
            Me.TipologiaLabel.TabIndex = 3
            Me.TipologiaLabel.Text = "_"
            '
            'FoliosMontoGroupBox
            '
            Me.FoliosMontoGroupBox.Controls.Add(Me.Monto3TextBox)
            Me.FoliosMontoGroupBox.Controls.Add(Me.Folios3TextBox)
            Me.FoliosMontoGroupBox.Controls.Add(Me.Monto2TextBox)
            Me.FoliosMontoGroupBox.Controls.Add(Me.Folios2TextBox)
            Me.FoliosMontoGroupBox.Controls.Add(Me.Monto1TextBox)
            Me.FoliosMontoGroupBox.Controls.Add(Me.MontoLabel)
            Me.FoliosMontoGroupBox.Controls.Add(Me.FoliosLabel)
            Me.FoliosMontoGroupBox.Controls.Add(Me.Folios1TextBox)
            Me.FoliosMontoGroupBox.Location = New System.Drawing.Point(11, 74)
            Me.FoliosMontoGroupBox.Name = "FoliosMontoGroupBox"
            Me.FoliosMontoGroupBox.Size = New System.Drawing.Size(397, 68)
            Me.FoliosMontoGroupBox.TabIndex = 0
            Me.FoliosMontoGroupBox.TabStop = False
            Me.FoliosMontoGroupBox.Visible = False
            '
            'Monto3TextBox
            '
            Me.Monto3TextBox.DisabledEnter = True
            Me.Monto3TextBox.DisabledTab = True
            Me.Monto3TextBox.EnabledShortCuts = False
            Me.Monto3TextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Monto3TextBox.FocusOut = System.Drawing.Color.White
            Me.Monto3TextBox.Location = New System.Drawing.Point(265, 37)
            Me.Monto3TextBox.Name = "Monto3TextBox"
            Rango1.MaxValue = CType(9223372036854775807, Long)
            Rango1.MinValue = CType(0, Long)
            Me.Monto3TextBox.Rango = Rango1
            Me.Monto3TextBox.ShortcutsEnabled = False
            Me.Monto3TextBox.Size = New System.Drawing.Size(126, 20)
            Me.Monto3TextBox.TabIndex = 2
            Me.Monto3TextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            Me.Monto3TextBox.Visible = False
            '
            'Folios3TextBox
            '
            Me.Folios3TextBox.DisabledEnter = False
            Me.Folios3TextBox.DisabledTab = False
            Me.Folios3TextBox.EnabledShortCuts = False
            Me.Folios3TextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Folios3TextBox.FocusOut = System.Drawing.Color.White
            Me.Folios3TextBox.Location = New System.Drawing.Point(265, 11)
            Me.Folios3TextBox.Name = "Folios3TextBox"
            Rango2.MaxValue = CType(9223372036854775807, Long)
            Rango2.MinValue = CType(1, Long)
            Me.Folios3TextBox.Rango = Rango2
            Me.Folios3TextBox.ShortcutsEnabled = False
            Me.Folios3TextBox.Size = New System.Drawing.Size(126, 20)
            Me.Folios3TextBox.TabIndex = 1
            Me.Folios3TextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            Me.Folios3TextBox.Visible = False
            '
            'Monto2TextBox
            '
            Me.Monto2TextBox.DisabledEnter = True
            Me.Monto2TextBox.DisabledTab = True
            Me.Monto2TextBox.Enabled = False
            Me.Monto2TextBox.EnabledShortCuts = False
            Me.Monto2TextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Monto2TextBox.FocusOut = System.Drawing.Color.White
            Me.Monto2TextBox.Location = New System.Drawing.Point(163, 37)
            Me.Monto2TextBox.Name = "Monto2TextBox"
            Rango3.MaxValue = CType(9223372036854775807, Long)
            Rango3.MinValue = CType(0, Long)
            Me.Monto2TextBox.Rango = Rango3
            Me.Monto2TextBox.ShortcutsEnabled = False
            Me.Monto2TextBox.Size = New System.Drawing.Size(96, 20)
            Me.Monto2TextBox.TabIndex = 11
            Me.Monto2TextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            Me.Monto2TextBox.Visible = False
            '
            'Folios2TextBox
            '
            Me.Folios2TextBox.DisabledEnter = False
            Me.Folios2TextBox.DisabledTab = False
            Me.Folios2TextBox.Enabled = False
            Me.Folios2TextBox.EnabledShortCuts = False
            Me.Folios2TextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Folios2TextBox.FocusOut = System.Drawing.Color.White
            Me.Folios2TextBox.Location = New System.Drawing.Point(163, 11)
            Me.Folios2TextBox.Name = "Folios2TextBox"
            Rango4.MaxValue = CType(2147483647, Long)
            Rango4.MinValue = CType(0, Long)
            Me.Folios2TextBox.Rango = Rango4
            Me.Folios2TextBox.ShortcutsEnabled = False
            Me.Folios2TextBox.Size = New System.Drawing.Size(96, 20)
            Me.Folios2TextBox.TabIndex = 10
            Me.Folios2TextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            Me.Folios2TextBox.Visible = False
            '
            'Monto1TextBox
            '
            Me.Monto1TextBox.DisabledEnter = True
            Me.Monto1TextBox.DisabledTab = True
            Me.Monto1TextBox.Enabled = False
            Me.Monto1TextBox.EnabledShortCuts = False
            Me.Monto1TextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Monto1TextBox.FocusOut = System.Drawing.Color.White
            Me.Monto1TextBox.Location = New System.Drawing.Point(64, 37)
            Me.Monto1TextBox.Name = "Monto1TextBox"
            Rango5.MaxValue = CType(9223372036854775807, Long)
            Rango5.MinValue = CType(0, Long)
            Me.Monto1TextBox.Rango = Rango5
            Me.Monto1TextBox.ShortcutsEnabled = False
            Me.Monto1TextBox.Size = New System.Drawing.Size(93, 20)
            Me.Monto1TextBox.TabIndex = 1
            Me.Monto1TextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            Me.Monto1TextBox.Visible = False
            '
            'MontoLabel
            '
            Me.MontoLabel.AutoSize = True
            Me.MontoLabel.Location = New System.Drawing.Point(16, 44)
            Me.MontoLabel.Name = "MontoLabel"
            Me.MontoLabel.Size = New System.Drawing.Size(40, 13)
            Me.MontoLabel.TabIndex = 9
            Me.MontoLabel.Text = "Monto:"
            Me.MontoLabel.Visible = False
            '
            'FoliosLabel
            '
            Me.FoliosLabel.AutoSize = True
            Me.FoliosLabel.Location = New System.Drawing.Point(16, 14)
            Me.FoliosLabel.Name = "FoliosLabel"
            Me.FoliosLabel.Size = New System.Drawing.Size(37, 13)
            Me.FoliosLabel.TabIndex = 8
            Me.FoliosLabel.Text = "Folios:"
            Me.FoliosLabel.Visible = False
            '
            'Folios1TextBox
            '
            Me.Folios1TextBox.DisabledEnter = False
            Me.Folios1TextBox.DisabledTab = False
            Me.Folios1TextBox.Enabled = False
            Me.Folios1TextBox.EnabledShortCuts = False
            Me.Folios1TextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Folios1TextBox.FocusOut = System.Drawing.Color.White
            Me.Folios1TextBox.Location = New System.Drawing.Point(64, 11)
            Me.Folios1TextBox.Name = "Folios1TextBox"
            Rango6.MaxValue = CType(2147483647, Long)
            Rango6.MinValue = CType(0, Long)
            Me.Folios1TextBox.Rango = Rango6
            Me.Folios1TextBox.ShortcutsEnabled = False
            Me.Folios1TextBox.Size = New System.Drawing.Size(93, 20)
            Me.Folios1TextBox.TabIndex = 0
            Me.Folios1TextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            Me.Folios1TextBox.Visible = False
            '
            'FormTerceraCapturarDataAdicional
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(692, 414)
            Me.Controls.Add(Me.FoliosMontoGroupBox)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.CamposGroupBox)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.GroupBox3)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormTerceraCapturarDataAdicional"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Tercera Capturar de datos"
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
        Friend WithEvents Monto1TextBox As DesktopTextBoxControl
        Friend WithEvents MontoLabel As System.Windows.Forms.Label
        Friend WithEvents FoliosLabel As System.Windows.Forms.Label
        Friend WithEvents Folios1TextBox As DesktopTextBoxControl
        Friend WithEvents Monto2TextBox As DesktopTextBoxControl
        Friend WithEvents Folios2TextBox As DesktopTextBoxControl
        Friend WithEvents Monto3TextBox As DesktopTextBoxControl
        Friend WithEvents Folios3TextBox As DesktopTextBoxControl
    End Class
End Namespace