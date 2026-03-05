Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.CBarras
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCBarrasCajas
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCBarrasCajas))
            Dim Rango1 As Rango = New Rango()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.CBarrasBarCodeControl = New Miharu.Desktop.Controls.BarCode.BarCodeControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ImprimirButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.ParametroDesktopTextBox = New DesktopTextBoxControl()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.TipoDesktopComboBox = New DesktopComboBoxControl()
            Me.Panel2.SuspendLayout()
            Me.Panel1.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel2
            '
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.Controls.Add(Me.CBarrasBarCodeControl)
            Me.Panel2.Location = New System.Drawing.Point(20, 11)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(288, 149)
            Me.Panel2.TabIndex = 2
            '
            'CBarrasBarCodeControl
            '
            Me.CBarrasBarCodeControl.Align = Miharu.Desktop.Controls.BarCode.AlignType.Center
            Me.CBarrasBarCodeControl.AutoPrint = False
            Me.CBarrasBarCodeControl.BackColor = System.Drawing.SystemColors.Window
            Me.CBarrasBarCodeControl.BarCode = "1234567890"
            Me.CBarrasBarCodeControl.BarCodeHeight = 30
            Me.CBarrasBarCodeControl.BarCodePrintDocument = Nothing
            Me.CBarrasBarCodeControl.BarCodeType = Miharu.Desktop.Controls.BarCode.BarCodeTypeType.EAN128
            Me.CBarrasBarCodeControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CBarrasBarCodeControl.FooterColumns = 2
            Me.CBarrasBarCodeControl.FooterFont = New System.Drawing.Font("Tahoma", 7.0!)
            Me.CBarrasBarCodeControl.FooterLinesString = ""
            Me.CBarrasBarCodeControl.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 18.0!)
            Me.CBarrasBarCodeControl.HeaderText = "Caja"
            Me.CBarrasBarCodeControl.LeftMargin = 1
            Me.CBarrasBarCodeControl.Location = New System.Drawing.Point(4, 4)
            Me.CBarrasBarCodeControl.Name = "CBarrasBarCodeControl"
            Me.CBarrasBarCodeControl.Padding = New System.Windows.Forms.Padding(1)
            Me.CBarrasBarCodeControl.ShowFooter = True
            Me.CBarrasBarCodeControl.ShowHeader = True
            Me.CBarrasBarCodeControl.Size = New System.Drawing.Size(280, 140)
            Me.CBarrasBarCodeControl.TabIndex = 0
            Me.CBarrasBarCodeControl.TopMargin = 1
            Me.CBarrasBarCodeControl.Weight = Miharu.Desktop.Controls.BarCode.BarCodeWeight.Small
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(10, 10)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(102, 13)
            Me.Label1.TabIndex = 39
            Me.Label1.Text = "Codigo de Barras"
            '
            'ImprimirButton
            '
            Me.ImprimirButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ImprimirButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnPrinter
            Me.ImprimirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ImprimirButton.Location = New System.Drawing.Point(140, 293)
            Me.ImprimirButton.Name = "ImprimirButton"
            Me.ImprimirButton.Size = New System.Drawing.Size(97, 30)
            Me.ImprimirButton.TabIndex = 2
            Me.ImprimirButton.Text = "&Imprimir"
            Me.ImprimirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ImprimirButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(243, 293)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(97, 30)
            Me.CerrarButton.TabIndex = 3
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Panel1
            '
            Me.Panel1.BackColor = System.Drawing.SystemColors.Desktop
            Me.Panel1.Controls.Add(Me.Panel2)
            Me.Panel1.Location = New System.Drawing.Point(13, 106)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(327, 170)
            Me.Panel1.TabIndex = 40
            '
            'ParametroDesktopTextBox
            '
            Me.ParametroDesktopTextBox.DisabledEnter = False
            Me.ParametroDesktopTextBox.DisabledTab = False
            Me.ParametroDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ParametroDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ParametroDesktopTextBox.Location = New System.Drawing.Point(154, 24)
            Me.ParametroDesktopTextBox.Name = "ParametroDesktopTextBox"
            Rango1.MaxValue = 2147483647
            Rango1.MinValue = 0
            Me.ParametroDesktopTextBox.Rango = Rango1
            Me.ParametroDesktopTextBox.Size = New System.Drawing.Size(154, 21)
            Me.ParametroDesktopTextBox.TabIndex = 1
            Me.ParametroDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.TipoDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.ParametroDesktopTextBox)
            Me.GroupBox1.Location = New System.Drawing.Point(13, 39)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(327, 61)
            Me.GroupBox1.TabIndex = 43
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Imprimir"
            '
            'TipoDesktopComboBox
            '
            Me.TipoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipoDesktopComboBox.DisabledEnter = False
            Me.TipoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipoDesktopComboBox.FormattingEnabled = True
            Me.TipoDesktopComboBox.Items.AddRange(New Object() {"Rango", "CBarras"})
            Me.TipoDesktopComboBox.Location = New System.Drawing.Point(11, 24)
            Me.TipoDesktopComboBox.Name = "TipoDesktopComboBox"
            Me.TipoDesktopComboBox.Size = New System.Drawing.Size(137, 21)
            Me.TipoDesktopComboBox.TabIndex = 0
            '
            'FormCBarrasCajas
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(362, 333)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.ImprimirButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Panel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCBarrasCajas"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Codigos de barras Cajas"
            Me.Panel2.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents CBarrasBarCodeControl As Miharu.Desktop.Controls.BarCode.BarCodeControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ImprimirButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents ParametroDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents TipoDesktopComboBox As DesktopComboBoxControl
    End Class
End Namespace