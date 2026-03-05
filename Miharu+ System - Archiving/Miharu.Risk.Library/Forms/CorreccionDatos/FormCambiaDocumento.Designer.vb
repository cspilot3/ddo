Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.CorreccionDatos

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCambiaDocumento
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCambiaDocumento))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.ReimpresionCBarrasButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.CBarrasFileLabel = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.DesktopTextBox1 = New DesktopTextBoxControl()
            Me.TipoloiaActualLabel = New System.Windows.Forms.Label()
            Me.DocumentoActualLabel = New System.Windows.Forms.Label()
            Me.DocumentoNuevoDesktopComboBox = New DesktopComboBoxControl()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.ForeColor = System.Drawing.Color.DarkRed
            Me.Label1.Location = New System.Drawing.Point(12, 96)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(111, 13)
            Me.Label1.TabIndex = 40
            Me.Label1.Text = "Documento Actual"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.ForeColor = System.Drawing.Color.DarkRed
            Me.Label2.Location = New System.Drawing.Point(12, 71)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(97, 13)
            Me.Label2.TabIndex = 41
            Me.Label2.Text = "Tipologia Actual"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.ForeColor = System.Drawing.Color.SeaGreen
            Me.Label3.Location = New System.Drawing.Point(12, 137)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(140, 13)
            Me.Label3.TabIndex = 42
            Me.Label3.Text = "Nuevo Tipo Documental"
            '
            'ReimpresionCBarrasButton
            '
            Me.ReimpresionCBarrasButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ReimpresionCBarrasButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnPrinter
            Me.ReimpresionCBarrasButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ReimpresionCBarrasButton.Location = New System.Drawing.Point(15, 173)
            Me.ReimpresionCBarrasButton.Name = "ReimpresionCBarrasButton"
            Me.ReimpresionCBarrasButton.Size = New System.Drawing.Size(192, 30)
            Me.ReimpresionCBarrasButton.TabIndex = 45
            Me.ReimpresionCBarrasButton.Text = "&Imprimir Codigo de barras"
            Me.ReimpresionCBarrasButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ReimpresionCBarrasButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnGuardar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(319, 173)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(100, 30)
            Me.AceptarButton.TabIndex = 43
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
            Me.CerrarButton.Location = New System.Drawing.Point(425, 173)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(100, 30)
            Me.CerrarButton.TabIndex = 44
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'CBarrasFileLabel
            '
            Me.CBarrasFileLabel.AutoSize = True
            Me.CBarrasFileLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CBarrasFileLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.CBarrasFileLabel.Location = New System.Drawing.Point(259, 21)
            Me.CBarrasFileLabel.Name = "CBarrasFileLabel"
            Me.CBarrasFileLabel.Size = New System.Drawing.Size(19, 19)
            Me.CBarrasFileLabel.TabIndex = 47
            Me.CBarrasFileLabel.Text = "_"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(11, 21)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(226, 19)
            Me.Label4.TabIndex = 46
            Me.Label4.Text = "Código Barras Documento:"
            '
            'DesktopTextBox1
            '
            Me.DesktopTextBox1.BackColor = System.Drawing.SystemColors.Control
            Me.DesktopTextBox1.DisabledEnter = False
            Me.DesktopTextBox1.DisabledTab = False
            Me.DesktopTextBox1.Enabled = False
            Me.DesktopTextBox1.EnabledShortCuts = False
            Me.DesktopTextBox1.FocusIn = System.Drawing.SystemColors.Control
            Me.DesktopTextBox1.FocusOut = System.Drawing.SystemColors.Control
            Me.DesktopTextBox1.Location = New System.Drawing.Point(44, 226)
            Me.DesktopTextBox1.Multiline = True
            Me.DesktopTextBox1.Name = "DesktopTextBox1"
            Rango1.MaxValue = CType(9223372036854775807, Long)
            Rango1.MinValue = CType(0, Long)
            Me.DesktopTextBox1.Rango = Rango1
            Me.DesktopTextBox1.ShortcutsEnabled = False
            Me.DesktopTextBox1.Size = New System.Drawing.Size(481, 41)
            Me.DesktopTextBox1.TabIndex = 48
            Me.DesktopTextBox1.Text = "Por favor tenga en cuenta que para cambiar el tipo documental el documento debe e" & _
                                      "star en estado de Mesa de control [30] o inferior y no tener datos ni validacion" & _
                                      "es"
            Me.DesktopTextBox1.Type = DesktopTextBoxControl.TipoTextBox.Normal
            '
            'TipoloiaActualLabel
            '
            Me.TipoloiaActualLabel.AutoSize = True
            Me.TipoloiaActualLabel.Location = New System.Drawing.Point(170, 71)
            Me.TipoloiaActualLabel.Name = "TipoloiaActualLabel"
            Me.TipoloiaActualLabel.Size = New System.Drawing.Size(14, 13)
            Me.TipoloiaActualLabel.TabIndex = 49
            Me.TipoloiaActualLabel.Text = "_"
            '
            'DocumentoActualLabel
            '
            Me.DocumentoActualLabel.AutoSize = True
            Me.DocumentoActualLabel.Location = New System.Drawing.Point(170, 96)
            Me.DocumentoActualLabel.Name = "DocumentoActualLabel"
            Me.DocumentoActualLabel.Size = New System.Drawing.Size(14, 13)
            Me.DocumentoActualLabel.TabIndex = 50
            Me.DocumentoActualLabel.Text = "_"
            '
            'DocumentoNuevoDesktopComboBox
            '
            Me.DocumentoNuevoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DocumentoNuevoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DocumentoNuevoDesktopComboBox.DisabledEnter = False
            Me.DocumentoNuevoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentoNuevoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DocumentoNuevoDesktopComboBox.FormattingEnabled = True
            Me.DocumentoNuevoDesktopComboBox.Items.AddRange(New Object() {"Rango", "CBarras"})
            Me.DocumentoNuevoDesktopComboBox.Location = New System.Drawing.Point(173, 129)
            Me.DocumentoNuevoDesktopComboBox.Name = "DocumentoNuevoDesktopComboBox"
            Me.DocumentoNuevoDesktopComboBox.Size = New System.Drawing.Size(352, 21)
            Me.DocumentoNuevoDesktopComboBox.TabIndex = 51
            '
            'FormCambiaDocumento
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(537, 279)
            Me.Controls.Add(Me.DocumentoNuevoDesktopComboBox)
            Me.Controls.Add(Me.DocumentoActualLabel)
            Me.Controls.Add(Me.TipoloiaActualLabel)
            Me.Controls.Add(Me.DesktopTextBox1)
            Me.Controls.Add(Me.CBarrasFileLabel)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.ReimpresionCBarrasButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCambiaDocumento"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Cambiar tipologia documento"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents ReimpresionCBarrasButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents CBarrasFileLabel As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents DesktopTextBox1 As DesktopTextBoxControl
        Friend WithEvents TipoloiaActualLabel As System.Windows.Forms.Label
        Friend WithEvents DocumentoActualLabel As System.Windows.Forms.Label
        Friend WithEvents DocumentoNuevoDesktopComboBox As DesktopComboBoxControl
    End Class
End Namespace