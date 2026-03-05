Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.MesaControlFisicos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormPrimeraCapturarDataAdicionalDeceval
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
            Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPrimeraCapturarDataAdicionalDeceval))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CBarrasLabel = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.TipologiaLabel = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.CapturaLabel = New System.Windows.Forms.Label()
            Me.EsquemaLabel = New System.Windows.Forms.Label()
            Me.ProyectoLabel = New System.Windows.Forms.Label()
            Me.EntidadLabel = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label11 = New System.Windows.Forms.Label()
            Me.Label12 = New System.Windows.Forms.Label()
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.FoliosDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.FoliosLabel = New System.Windows.Forms.Label()
            Me.MontoLabel = New System.Windows.Forms.Label()
            Me.MontoDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.FoliosMontoGroupBox = New System.Windows.Forms.GroupBox()
            Me.GroupBoxValidaciones = New System.Windows.Forms.GroupBox()
            Me.ValidacionesPanel = New System.Windows.Forms.Panel()
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
            Me.CamposPanel = New System.Windows.Forms.Panel()
            Me.GroupBoxCampos = New System.Windows.Forms.GroupBox()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox3.SuspendLayout()
            Me.FoliosMontoGroupBox.SuspendLayout()
            Me.GroupBoxValidaciones.SuspendLayout()
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.GroupBoxCampos.SuspendLayout()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(16, 17)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(156, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Código Barras Documento:"
            '
            'CBarrasLabel
            '
            Me.CBarrasLabel.AutoSize = True
            Me.CBarrasLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.CBarrasLabel.Location = New System.Drawing.Point(193, 17)
            Me.CBarrasLabel.Name = "CBarrasLabel"
            Me.CBarrasLabel.Size = New System.Drawing.Size(14, 13)
            Me.CBarrasLabel.TabIndex = 1
            Me.CBarrasLabel.Text = "_"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(15, 43)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(61, 13)
            Me.Label3.TabIndex = 2
            Me.Label3.Text = "Tipología:"
            '
            'TipologiaLabel
            '
            Me.TipologiaLabel.AutoSize = True
            Me.TipologiaLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.TipologiaLabel.Location = New System.Drawing.Point(193, 43)
            Me.TipologiaLabel.Name = "TipologiaLabel"
            Me.TipologiaLabel.Size = New System.Drawing.Size(14, 13)
            Me.TipologiaLabel.TabIndex = 3
            Me.TipologiaLabel.Text = "_"
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.CapturaLabel)
            Me.GroupBox1.Controls.Add(Me.EsquemaLabel)
            Me.GroupBox1.Controls.Add(Me.ProyectoLabel)
            Me.GroupBox1.Controls.Add(Me.EntidadLabel)
            Me.GroupBox1.Controls.Add(Me.Label5)
            Me.GroupBox1.Controls.Add(Me.Label11)
            Me.GroupBox1.Controls.Add(Me.Label12)
            Me.GroupBox1.Location = New System.Drawing.Point(545, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(248, 130)
            Me.GroupBox1.TabIndex = 3
            Me.GroupBox1.TabStop = False
            '
            'CapturaLabel
            '
            Me.CapturaLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CapturaLabel.AutoSize = True
            Me.CapturaLabel.BackColor = System.Drawing.SystemColors.Control
            Me.CapturaLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CapturaLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.CapturaLabel.Location = New System.Drawing.Point(14, 12)
            Me.CapturaLabel.Name = "CapturaLabel"
            Me.CapturaLabel.Size = New System.Drawing.Size(19, 19)
            Me.CapturaLabel.TabIndex = 0
            Me.CapturaLabel.Text = "_"
            Me.CapturaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'EsquemaLabel
            '
            Me.EsquemaLabel.AutoSize = True
            Me.EsquemaLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EsquemaLabel.Location = New System.Drawing.Point(88, 105)
            Me.EsquemaLabel.Name = "EsquemaLabel"
            Me.EsquemaLabel.Size = New System.Drawing.Size(13, 13)
            Me.EsquemaLabel.TabIndex = 6
            Me.EsquemaLabel.Text = "_"
            '
            'ProyectoLabel
            '
            Me.ProyectoLabel.AutoSize = True
            Me.ProyectoLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ProyectoLabel.Location = New System.Drawing.Point(88, 82)
            Me.ProyectoLabel.Name = "ProyectoLabel"
            Me.ProyectoLabel.Size = New System.Drawing.Size(13, 13)
            Me.ProyectoLabel.TabIndex = 5
            Me.ProyectoLabel.Text = "_"
            '
            'EntidadLabel
            '
            Me.EntidadLabel.AutoSize = True
            Me.EntidadLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EntidadLabel.Location = New System.Drawing.Point(88, 61)
            Me.EntidadLabel.Name = "EntidadLabel"
            Me.EntidadLabel.Size = New System.Drawing.Size(13, 13)
            Me.EntidadLabel.TabIndex = 4
            Me.EntidadLabel.Text = "_"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label5.Location = New System.Drawing.Point(15, 61)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(49, 13)
            Me.Label5.TabIndex = 1
            Me.Label5.Text = "Cliente:"
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label11.Location = New System.Drawing.Point(15, 105)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(61, 13)
            Me.Label11.TabIndex = 3
            Me.Label11.Text = "Esquema:"
            '
            'Label12
            '
            Me.Label12.AutoSize = True
            Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label12.Location = New System.Drawing.Point(15, 82)
            Me.Label12.Name = "Label12"
            Me.Label12.Size = New System.Drawing.Size(61, 13)
            Me.Label12.TabIndex = 2
            Me.Label12.Text = "Proyecto:"
            '
            'GroupBox3
            '
            Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox3.Controls.Add(Me.Label1)
            Me.GroupBox3.Controls.Add(Me.CBarrasLabel)
            Me.GroupBox3.Controls.Add(Me.Label3)
            Me.GroupBox3.Controls.Add(Me.TipologiaLabel)
            Me.GroupBox3.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Size = New System.Drawing.Size(527, 74)
            Me.GroupBox3.TabIndex = 0
            Me.GroupBox3.TabStop = False
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(708, 536)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(85, 25)
            Me.CerrarButton.TabIndex = 5
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(612, 536)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(85, 25)
            Me.AceptarButton.TabIndex = 4
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'FoliosDesktopTextBox
            '
            Me.FoliosDesktopTextBox._Obligatorio = False
            Me.FoliosDesktopTextBox._PermitePegar = False
            Me.FoliosDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.FoliosDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.FoliosDesktopTextBox.DateFormat = Nothing
            Me.FoliosDesktopTextBox.DisabledEnter = False
            Me.FoliosDesktopTextBox.DisabledTab = False
            Me.FoliosDesktopTextBox.EnabledShortCuts = False
            Me.FoliosDesktopTextBox.fk_Campo = 0
            Me.FoliosDesktopTextBox.fk_Documento = 0
            Me.FoliosDesktopTextBox.fk_Validacion = 0
            Me.FoliosDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.FoliosDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.FoliosDesktopTextBox.Location = New System.Drawing.Point(108, 17)
            Me.FoliosDesktopTextBox.MaskedTextBox_Property = ""
            Me.FoliosDesktopTextBox.MaximumLength = CType(0, Short)
            Me.FoliosDesktopTextBox.MinimumLength = CType(0, Short)
            Me.FoliosDesktopTextBox.Name = "FoliosDesktopTextBox"
            Me.FoliosDesktopTextBox.Obligatorio = False
            Me.FoliosDesktopTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 1.0R
            Me.FoliosDesktopTextBox.Rango = Rango1
            Me.FoliosDesktopTextBox.Size = New System.Drawing.Size(124, 21)
            Me.FoliosDesktopTextBox.TabIndex = 1
            Me.FoliosDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Numerico
            Me.FoliosDesktopTextBox.Usa_Decimales = False
            Me.FoliosDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'FoliosLabel
            '
            Me.FoliosLabel.AutoSize = True
            Me.FoliosLabel.Location = New System.Drawing.Point(16, 20)
            Me.FoliosLabel.Name = "FoliosLabel"
            Me.FoliosLabel.Size = New System.Drawing.Size(42, 13)
            Me.FoliosLabel.TabIndex = 0
            Me.FoliosLabel.Text = "Folios:"
            '
            'MontoLabel
            '
            Me.MontoLabel.AutoSize = True
            Me.MontoLabel.Location = New System.Drawing.Point(238, 22)
            Me.MontoLabel.Name = "MontoLabel"
            Me.MontoLabel.Size = New System.Drawing.Size(46, 13)
            Me.MontoLabel.TabIndex = 2
            Me.MontoLabel.Text = "Monto:"
            '
            'MontoDesktopTextBox
            '
            Me.MontoDesktopTextBox._Obligatorio = False
            Me.MontoDesktopTextBox._PermitePegar = False
            Me.MontoDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.MontoDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.MontoDesktopTextBox.DateFormat = Nothing
            Me.MontoDesktopTextBox.DisabledEnter = True
            Me.MontoDesktopTextBox.DisabledTab = True
            Me.MontoDesktopTextBox.EnabledShortCuts = False
            Me.MontoDesktopTextBox.fk_Campo = 0
            Me.MontoDesktopTextBox.fk_Documento = 0
            Me.MontoDesktopTextBox.fk_Validacion = 0
            Me.MontoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.MontoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.MontoDesktopTextBox.Location = New System.Drawing.Point(299, 17)
            Me.MontoDesktopTextBox.MaskedTextBox_Property = ""
            Me.MontoDesktopTextBox.MaximumLength = CType(0, Short)
            Me.MontoDesktopTextBox.MinimumLength = CType(0, Short)
            Me.MontoDesktopTextBox.Name = "MontoDesktopTextBox"
            Me.MontoDesktopTextBox.Obligatorio = False
            Me.MontoDesktopTextBox.permitePegar = False
            Rango2.MaxValue = 9.2233720368547758E+18R
            Rango2.MinValue = 0.0R
            Me.MontoDesktopTextBox.Rango = Rango2
            Me.MontoDesktopTextBox.Size = New System.Drawing.Size(215, 21)
            Me.MontoDesktopTextBox.TabIndex = 3
            Me.MontoDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Numerico
            Me.MontoDesktopTextBox.Usa_Decimales = False
            Me.MontoDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'FoliosMontoGroupBox
            '
            Me.FoliosMontoGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FoliosMontoGroupBox.Controls.Add(Me.MontoDesktopTextBox)
            Me.FoliosMontoGroupBox.Controls.Add(Me.MontoLabel)
            Me.FoliosMontoGroupBox.Controls.Add(Me.FoliosLabel)
            Me.FoliosMontoGroupBox.Controls.Add(Me.FoliosDesktopTextBox)
            Me.FoliosMontoGroupBox.Location = New System.Drawing.Point(12, 92)
            Me.FoliosMontoGroupBox.Name = "FoliosMontoGroupBox"
            Me.FoliosMontoGroupBox.Size = New System.Drawing.Size(527, 50)
            Me.FoliosMontoGroupBox.TabIndex = 1
            Me.FoliosMontoGroupBox.TabStop = False
            '
            'GroupBoxValidaciones
            '
            Me.GroupBoxValidaciones.Controls.Add(Me.ValidacionesPanel)
            Me.GroupBoxValidaciones.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GroupBoxValidaciones.Location = New System.Drawing.Point(3, 3)
            Me.GroupBoxValidaciones.Name = "GroupBoxValidaciones"
            Me.GroupBoxValidaciones.Padding = New System.Windows.Forms.Padding(30, 20, 3, 3)
            Me.GroupBoxValidaciones.Size = New System.Drawing.Size(771, 162)
            Me.GroupBoxValidaciones.TabIndex = 0
            Me.GroupBoxValidaciones.TabStop = False
            Me.GroupBoxValidaciones.Text = "Validaciones"
            '
            'ValidacionesPanel
            '
            Me.ValidacionesPanel.AutoScroll = True
            Me.ValidacionesPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.ValidacionesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ValidacionesPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValidacionesPanel.Location = New System.Drawing.Point(30, 34)
            Me.ValidacionesPanel.Name = "ValidacionesPanel"
            Me.ValidacionesPanel.Size = New System.Drawing.Size(738, 125)
            Me.ValidacionesPanel.TabIndex = 0
            '
            'SplitContainer1
            '
            Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.SplitContainer1.Location = New System.Drawing.Point(12, 170)
            Me.SplitContainer1.Name = "SplitContainer1"
            Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.AutoScroll = True
            Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBoxCampos)
            Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(1)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.AutoScroll = True
            Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBoxValidaciones)
            Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
            Me.SplitContainer1.Panel2MinSize = 100
            Me.SplitContainer1.Size = New System.Drawing.Size(781, 369)
            Me.SplitContainer1.SplitterDistance = 193
            Me.SplitContainer1.TabIndex = 2
            '
            'CamposPanel
            '
            Me.CamposPanel.AutoScroll = True
            Me.CamposPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.CamposPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.CamposPanel.Location = New System.Drawing.Point(30, 34)
            Me.CamposPanel.Name = "CamposPanel"
            Me.CamposPanel.Size = New System.Drawing.Size(738, 146)
            Me.CamposPanel.TabIndex = 0
            '
            'GroupBoxCampos
            '
            Me.GroupBoxCampos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.GroupBoxCampos.Controls.Add(Me.CamposPanel)
            Me.GroupBoxCampos.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GroupBoxCampos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.GroupBoxCampos.Location = New System.Drawing.Point(1, 1)
            Me.GroupBoxCampos.Name = "GroupBoxCampos"
            Me.GroupBoxCampos.Padding = New System.Windows.Forms.Padding(30, 20, 3, 3)
            Me.GroupBoxCampos.Size = New System.Drawing.Size(775, 187)
            Me.GroupBoxCampos.TabIndex = 0
            Me.GroupBoxCampos.TabStop = False
            Me.GroupBoxCampos.Text = "Campos"
            Me.GroupBoxCampos.Visible = False
            '
            'FormPrimeraCapturarDataAdicionalDeceval
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoScroll = True
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(800, 569)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.SplitContainer1)
            Me.Controls.Add(Me.GroupBox3)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.FoliosMontoGroupBox)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormPrimeraCapturarDataAdicionalDeceval"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Configuraciones Mesa de Control"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBox3.ResumeLayout(False)
            Me.GroupBox3.PerformLayout()
            Me.FoliosMontoGroupBox.ResumeLayout(False)
            Me.FoliosMontoGroupBox.PerformLayout()
            Me.GroupBoxValidaciones.ResumeLayout(False)
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.ResumeLayout(False)
            Me.GroupBoxCampos.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CBarrasLabel As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents TipologiaLabel As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents EsquemaLabel As System.Windows.Forms.Label
        Friend WithEvents ProyectoLabel As System.Windows.Forms.Label
        Friend WithEvents EntidadLabel As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label11 As System.Windows.Forms.Label
        Friend WithEvents Label12 As System.Windows.Forms.Label
        Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents CapturaLabel As System.Windows.Forms.Label
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents FoliosDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents FoliosLabel As System.Windows.Forms.Label
        Friend WithEvents MontoLabel As System.Windows.Forms.Label
        Friend WithEvents MontoDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents FoliosMontoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBoxValidaciones As System.Windows.Forms.GroupBox
        Friend WithEvents ValidacionesPanel As System.Windows.Forms.Panel
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents GroupBoxCampos As System.Windows.Forms.GroupBox
        Friend WithEvents CamposPanel As System.Windows.Forms.Panel
    End Class
End Namespace