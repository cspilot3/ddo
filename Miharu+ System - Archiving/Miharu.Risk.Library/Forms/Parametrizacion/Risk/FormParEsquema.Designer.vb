Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Library

Namespace Forms.Parametrizacion.Risk

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParEsquema
        Inherits FormBase

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormParEsquema))
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.aceptafoldersobranteCheckBox = New System.Windows.Forms.CheckBox()
            Me.folderTipoComboBox = New DesktopComboBoxControl()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.SubserieComboBox = New DesktopComboBoxControl()
            Me.aceptafaltanteobligatoriosCheckBox = New System.Windows.Forms.CheckBox()
            Me.SerieComboBox = New DesktopComboBoxControl()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.TRDComboBox = New DesktopComboBoxControl()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.AceptasobrantesCheckBox = New System.Windows.Forms.CheckBox()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.fk_esquema = New System.Windows.Forms.Label()
            Me.EsquemaComboBox = New DesktopComboBoxControl()
            Me.EsquemaFacturacionDesktopComboBox = New DesktopComboBoxControl()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.SuspendLayout()
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnGuardar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(256, 425)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(90, 30)
            Me.AceptarButton.TabIndex = 31
            Me.AceptarButton.Text = "&Guardar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(352, 425)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 30
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(12, 15)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(64, 13)
            Me.Label1.TabIndex = 28
            Me.Label1.Text = "Esquemas"
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.aceptafoldersobranteCheckBox)
            Me.GroupBox1.Controls.Add(Me.folderTipoComboBox)
            Me.GroupBox1.Controls.Add(Me.Label8)
            Me.GroupBox1.Controls.Add(Me.SubserieComboBox)
            Me.GroupBox1.Controls.Add(Me.aceptafaltanteobligatoriosCheckBox)
            Me.GroupBox1.Controls.Add(Me.SerieComboBox)
            Me.GroupBox1.Controls.Add(Me.Label7)
            Me.GroupBox1.Controls.Add(Me.TRDComboBox)
            Me.GroupBox1.Controls.Add(Me.Label6)
            Me.GroupBox1.Controls.Add(Me.AceptasobrantesCheckBox)
            Me.GroupBox1.Controls.Add(Me.Label5)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Location = New System.Drawing.Point(15, 87)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(427, 219)
            Me.GroupBox1.TabIndex = 33
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Propiedades"
            '
            'aceptafoldersobranteCheckBox
            '
            Me.aceptafoldersobranteCheckBox.AutoSize = True
            Me.aceptafoldersobranteCheckBox.Location = New System.Drawing.Point(393, 151)
            Me.aceptafoldersobranteCheckBox.Name = "aceptafoldersobranteCheckBox"
            Me.aceptafoldersobranteCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.aceptafoldersobranteCheckBox.TabIndex = 46
            Me.aceptafoldersobranteCheckBox.UseVisualStyleBackColor = True
            '
            'folderTipoComboBox
            '
            Me.folderTipoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.folderTipoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.folderTipoComboBox.DisabledEnter = False
            Me.folderTipoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.folderTipoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.folderTipoComboBox.FormattingEnabled = True
            Me.folderTipoComboBox.Location = New System.Drawing.Point(106, 113)
            Me.folderTipoComboBox.Name = "folderTipoComboBox"
            Me.folderTipoComboBox.Size = New System.Drawing.Size(302, 21)
            Me.folderTipoComboBox.TabIndex = 45
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Location = New System.Drawing.Point(219, 151)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(161, 13)
            Me.Label8.TabIndex = 45
            Me.Label8.Text = "Acepta Carpetas sobrantes"
            '
            'SubserieComboBox
            '
            Me.SubserieComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SubserieComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SubserieComboBox.DisabledEnter = False
            Me.SubserieComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SubserieComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SubserieComboBox.FormattingEnabled = True
            Me.SubserieComboBox.Location = New System.Drawing.Point(106, 84)
            Me.SubserieComboBox.Name = "SubserieComboBox"
            Me.SubserieComboBox.Size = New System.Drawing.Size(302, 21)
            Me.SubserieComboBox.TabIndex = 44
            '
            'aceptafaltanteobligatoriosCheckBox
            '
            Me.aceptafaltanteobligatoriosCheckBox.AutoSize = True
            Me.aceptafaltanteobligatoriosCheckBox.Location = New System.Drawing.Point(393, 184)
            Me.aceptafaltanteobligatoriosCheckBox.Name = "aceptafaltanteobligatoriosCheckBox"
            Me.aceptafaltanteobligatoriosCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.aceptafaltanteobligatoriosCheckBox.TabIndex = 44
            Me.aceptafaltanteobligatoriosCheckBox.UseVisualStyleBackColor = True
            '
            'SerieComboBox
            '
            Me.SerieComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SerieComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SerieComboBox.DisabledEnter = False
            Me.SerieComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SerieComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SerieComboBox.FormattingEnabled = True
            Me.SerieComboBox.Location = New System.Drawing.Point(106, 53)
            Me.SerieComboBox.Name = "SerieComboBox"
            Me.SerieComboBox.Size = New System.Drawing.Size(302, 21)
            Me.SerieComboBox.TabIndex = 43
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(219, 185)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(171, 13)
            Me.Label7.TabIndex = 43
            Me.Label7.Text = "Acepta faltantes obligatorios"
            '
            'TRDComboBox
            '
            Me.TRDComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TRDComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TRDComboBox.DisabledEnter = False
            Me.TRDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TRDComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TRDComboBox.FormattingEnabled = True
            Me.TRDComboBox.Location = New System.Drawing.Point(106, 23)
            Me.TRDComboBox.Name = "TRDComboBox"
            Me.TRDComboBox.Size = New System.Drawing.Size(302, 21)
            Me.TRDComboBox.TabIndex = 42
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(20, 116)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(75, 13)
            Me.Label6.TabIndex = 41
            Me.Label6.Text = "Tipo Folders"
            '
            'AceptasobrantesCheckBox
            '
            Me.AceptasobrantesCheckBox.AutoSize = True
            Me.AceptasobrantesCheckBox.Location = New System.Drawing.Point(133, 151)
            Me.AceptasobrantesCheckBox.Name = "AceptasobrantesCheckBox"
            Me.AceptasobrantesCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.AceptasobrantesCheckBox.TabIndex = 40
            Me.AceptasobrantesCheckBox.UseVisualStyleBackColor = True
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(20, 151)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(107, 13)
            Me.Label5.TabIndex = 37
            Me.Label5.Text = "Acepta sobrantes"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(20, 87)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(56, 13)
            Me.Label4.TabIndex = 36
            Me.Label4.Text = "Subserie"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(20, 56)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(36, 13)
            Me.Label3.TabIndex = 35
            Me.Label3.Text = "Serie"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(20, 26)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(30, 13)
            Me.Label2.TabIndex = 34
            Me.Label2.Text = "TRD"
            '
            'fk_esquema
            '
            Me.fk_esquema.AutoSize = True
            Me.fk_esquema.Location = New System.Drawing.Point(365, 17)
            Me.fk_esquema.Name = "fk_esquema"
            Me.fk_esquema.Size = New System.Drawing.Size(77, 13)
            Me.fk_esquema.TabIndex = 41
            Me.fk_esquema.Text = "fk_esquema"
            Me.fk_esquema.Visible = False
            '
            'EsquemaComboBox
            '
            Me.EsquemaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaComboBox.DisabledEnter = False
            Me.EsquemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaComboBox.FormattingEnabled = True
            Me.EsquemaComboBox.Location = New System.Drawing.Point(12, 34)
            Me.EsquemaComboBox.Name = "EsquemaComboBox"
            Me.EsquemaComboBox.Size = New System.Drawing.Size(430, 21)
            Me.EsquemaComboBox.TabIndex = 46
            '
            'EsquemaFacturacionDesktopComboBox
            '
            Me.EsquemaFacturacionDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaFacturacionDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaFacturacionDesktopComboBox.DisabledEnter = False
            Me.EsquemaFacturacionDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaFacturacionDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaFacturacionDesktopComboBox.FormattingEnabled = True
            Me.EsquemaFacturacionDesktopComboBox.Location = New System.Drawing.Point(26, 52)
            Me.EsquemaFacturacionDesktopComboBox.Name = "EsquemaFacturacionDesktopComboBox"
            Me.EsquemaFacturacionDesktopComboBox.Size = New System.Drawing.Size(382, 21)
            Me.EsquemaFacturacionDesktopComboBox.TabIndex = 47
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Location = New System.Drawing.Point(23, 29)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(126, 13)
            Me.Label9.TabIndex = 48
            Me.Label9.Text = "Esquema por defecto"
            '
            'GroupBox2
            '
            Me.GroupBox2.Controls.Add(Me.Label9)
            Me.GroupBox2.Controls.Add(Me.EsquemaFacturacionDesktopComboBox)
            Me.GroupBox2.Location = New System.Drawing.Point(15, 321)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(427, 88)
            Me.GroupBox2.TabIndex = 49
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Facturacion"
            '
            'FormParEsquema
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(462, 467)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.EsquemaComboBox)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.fk_esquema)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.CerrarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormParEsquema"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Parametrizacion de Esquemas"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents AceptasobrantesCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents fk_esquema As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents aceptafoldersobranteCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents aceptafaltanteobligatoriosCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents folderTipoComboBox As DesktopComboBoxControl
        Friend WithEvents SubserieComboBox As DesktopComboBoxControl
        Friend WithEvents SerieComboBox As DesktopComboBoxControl
        Friend WithEvents TRDComboBox As DesktopComboBoxControl
        Friend WithEvents EsquemaComboBox As DesktopComboBoxControl
        Friend WithEvents EsquemaFacturacionDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    End Class
End Namespace