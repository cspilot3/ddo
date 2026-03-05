Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.MesaControlFisicos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormPrimeraCapturarDataAdicionalDevolucion
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPrimeraCapturarDataAdicionalDevolucion))
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
            Me.ValidacionesPanel = New System.Windows.Forms.Panel()
            Me.CamposPanel = New System.Windows.Forms.Panel()
            Me.Label13 = New System.Windows.Forms.Label()
            Me.FoliosDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            Me.GroupBox4 = New System.Windows.Forms.GroupBox()
            Me.MontoDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.GroupBox5 = New System.Windows.Forms.GroupBox()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox3.SuspendLayout()
            Me.GroupBox4.SuspendLayout()
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.GroupBox5.SuspendLayout()
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
            Me.GroupBox1.Location = New System.Drawing.Point(537, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(248, 130)
            Me.GroupBox1.TabIndex = 6
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
            Me.CapturaLabel.TabIndex = 12
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
            Me.EsquemaLabel.TabIndex = 12
            Me.EsquemaLabel.Text = "_"
            '
            'ProyectoLabel
            '
            Me.ProyectoLabel.AutoSize = True
            Me.ProyectoLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ProyectoLabel.Location = New System.Drawing.Point(88, 82)
            Me.ProyectoLabel.Name = "ProyectoLabel"
            Me.ProyectoLabel.Size = New System.Drawing.Size(13, 13)
            Me.ProyectoLabel.TabIndex = 11
            Me.ProyectoLabel.Text = "_"
            '
            'EntidadLabel
            '
            Me.EntidadLabel.AutoSize = True
            Me.EntidadLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EntidadLabel.Location = New System.Drawing.Point(88, 61)
            Me.EntidadLabel.Name = "EntidadLabel"
            Me.EntidadLabel.Size = New System.Drawing.Size(13, 13)
            Me.EntidadLabel.TabIndex = 10
            Me.EntidadLabel.Text = "_"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label5.Location = New System.Drawing.Point(15, 61)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(49, 13)
            Me.Label5.TabIndex = 6
            Me.Label5.Text = "Cliente:"
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label11.Location = New System.Drawing.Point(15, 105)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(61, 13)
            Me.Label11.TabIndex = 8
            Me.Label11.Text = "Esquema:"
            '
            'Label12
            '
            Me.Label12.AutoSize = True
            Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label12.Location = New System.Drawing.Point(15, 82)
            Me.Label12.Name = "Label12"
            Me.Label12.Size = New System.Drawing.Size(61, 13)
            Me.Label12.TabIndex = 7
            Me.Label12.Text = "Proyecto:"
            '
            'ValidacionesPanel
            '
            Me.ValidacionesPanel.AutoScroll = True
            Me.ValidacionesPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValidacionesPanel.Location = New System.Drawing.Point(30, 34)
            Me.ValidacionesPanel.Name = "ValidacionesPanel"
            Me.ValidacionesPanel.Size = New System.Drawing.Size(730, 139)
            Me.ValidacionesPanel.TabIndex = 3
            '
            'CamposPanel
            '
            Me.CamposPanel.AutoScroll = True
            Me.CamposPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CamposPanel.Location = New System.Drawing.Point(30, 34)
            Me.CamposPanel.Name = "CamposPanel"
            Me.CamposPanel.Size = New System.Drawing.Size(730, 132)
            Me.CamposPanel.TabIndex = 2
            '
            'Label13
            '
            Me.Label13.AutoSize = True
            Me.Label13.Location = New System.Drawing.Point(16, 17)
            Me.Label13.Name = "Label13"
            Me.Label13.Size = New System.Drawing.Size(42, 13)
            Me.Label13.TabIndex = 8
            Me.Label13.Text = "Folios:"
            '
            'FoliosDesktopTextBox
            '
            Me.FoliosDesktopTextBox._PermitePegar = False
            Me.FoliosDesktopTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FoliosDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.FoliosDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.FoliosDesktopTextBox.DateFormat = Nothing
            Me.FoliosDesktopTextBox.DisabledEnter = False
            Me.FoliosDesktopTextBox.DisabledTab = False
            Me.FoliosDesktopTextBox.EnabledShortCuts = False
            Me.FoliosDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.FoliosDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.FoliosDesktopTextBox.Location = New System.Drawing.Point(108, 14)
            Me.FoliosDesktopTextBox.MaskedTextBox_Property = ""
            Me.FoliosDesktopTextBox.MaximumLength = CType(0, Short)
            Me.FoliosDesktopTextBox.MinimumLength = CType(0, Short)
            Me.FoliosDesktopTextBox.Name = "FoliosDesktopTextBox"
            Me.FoliosDesktopTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 1.0R
            Me.FoliosDesktopTextBox.Rango = Rango1
            Me.FoliosDesktopTextBox.Size = New System.Drawing.Size(99, 21)
            Me.FoliosDesktopTextBox.TabIndex = 0
            Me.FoliosDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Numerico
            Me.FoliosDesktopTextBox.Usa_Decimales = False
            Me.FoliosDesktopTextBox.Validos_Cantidad_Puntos = False
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
            Me.GroupBox3.Size = New System.Drawing.Size(519, 74)
            Me.GroupBox3.TabIndex = 10
            Me.GroupBox3.TabStop = False
            '
            'GroupBox4
            '
            Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox4.Controls.Add(Me.MontoDesktopTextBox)
            Me.GroupBox4.Controls.Add(Me.Label2)
            Me.GroupBox4.Controls.Add(Me.Label13)
            Me.GroupBox4.Controls.Add(Me.FoliosDesktopTextBox)
            Me.GroupBox4.Location = New System.Drawing.Point(12, 92)
            Me.GroupBox4.Name = "GroupBox4"
            Me.GroupBox4.Size = New System.Drawing.Size(519, 50)
            Me.GroupBox4.TabIndex = 0
            Me.GroupBox4.TabStop = False
            '
            'MontoDesktopTextBox
            '
            Me.MontoDesktopTextBox._PermitePegar = False
            Me.MontoDesktopTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MontoDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.MontoDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.MontoDesktopTextBox.DateFormat = Nothing
            Me.MontoDesktopTextBox.DisabledEnter = True
            Me.MontoDesktopTextBox.DisabledTab = True
            Me.MontoDesktopTextBox.EnabledShortCuts = False
            Me.MontoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.MontoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.MontoDesktopTextBox.Location = New System.Drawing.Point(301, 14)
            Me.MontoDesktopTextBox.MaskedTextBox_Property = ""
            Me.MontoDesktopTextBox.MaximumLength = CType(0, Short)
            Me.MontoDesktopTextBox.MinimumLength = CType(0, Short)
            Me.MontoDesktopTextBox.Name = "MontoDesktopTextBox"
            Me.MontoDesktopTextBox.permitePegar = False
            Rango2.MaxValue = 9.2233720368547758E+18R
            Rango2.MinValue = 0.0R
            Me.MontoDesktopTextBox.Rango = Rango2
            Me.MontoDesktopTextBox.Size = New System.Drawing.Size(190, 21)
            Me.MontoDesktopTextBox.TabIndex = 1
            Me.MontoDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Numerico
            Me.MontoDesktopTextBox.Usa_Decimales = False
            Me.MontoDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(238, 19)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(46, 13)
            Me.Label2.TabIndex = 9
            Me.Label2.Text = "Monto:"
            '
            'SplitContainer1
            '
            Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.SplitContainer1.Location = New System.Drawing.Point(12, 161)
            Me.SplitContainer1.Name = "SplitContainer1"
            Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox2)
            Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox5)
            Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
            Me.SplitContainer1.Size = New System.Drawing.Size(773, 369)
            Me.SplitContainer1.SplitterDistance = 179
            Me.SplitContainer1.TabIndex = 2
            '
            'GroupBox2
            '
            Me.GroupBox2.Controls.Add(Me.CamposPanel)
            Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Padding = New System.Windows.Forms.Padding(30, 20, 3, 3)
            Me.GroupBox2.Size = New System.Drawing.Size(763, 169)
            Me.GroupBox2.TabIndex = 3
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Campos"
            '
            'GroupBox5
            '
            Me.GroupBox5.Controls.Add(Me.ValidacionesPanel)
            Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GroupBox5.Location = New System.Drawing.Point(3, 3)
            Me.GroupBox5.Name = "GroupBox5"
            Me.GroupBox5.Padding = New System.Windows.Forms.Padding(30, 20, 3, 3)
            Me.GroupBox5.Size = New System.Drawing.Size(763, 176)
            Me.GroupBox5.TabIndex = 0
            Me.GroupBox5.TabStop = False
            Me.GroupBox5.Text = "Validaciones"
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(700, 536)
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
            Me.AceptarButton.Location = New System.Drawing.Point(604, 536)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(85, 25)
            Me.AceptarButton.TabIndex = 4
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'FormPrimeraCapturarDataAdicionalDevolucion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(792, 569)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.SplitContainer1)
            Me.Controls.Add(Me.GroupBox3)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.GroupBox4)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormPrimeraCapturarDataAdicionalDevolucion"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Primera Captura de datos"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBox3.ResumeLayout(False)
            Me.GroupBox3.PerformLayout()
            Me.GroupBox4.ResumeLayout(False)
            Me.GroupBox4.PerformLayout()
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.ResumeLayout(False)
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox5.ResumeLayout(False)
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
        Friend WithEvents Label13 As System.Windows.Forms.Label
        Friend WithEvents FoliosDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
        Friend WithEvents CamposPanel As System.Windows.Forms.Panel
        Friend WithEvents CapturaLabel As System.Windows.Forms.Label
        Friend WithEvents ValidacionesPanel As System.Windows.Forms.Panel
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents MontoDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
    End Class
End Namespace