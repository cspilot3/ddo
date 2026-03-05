Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.CBarras
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCBarrasFolderFile
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCBarrasFolderFile))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ImprimirButton = New System.Windows.Forms.Button()
            Me.CBarrasBarCodeControl = New Miharu.Desktop.Controls.BarCode.BarCodeControl()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.cbarrasDesktopCBarrasControl = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Panel2.SuspendLayout()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(11, 11)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(102, 13)
            Me.Label1.TabIndex = 33
            Me.Label1.Text = "Codigo de Barras"
            '
            'ImprimirButton
            '
            Me.ImprimirButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ImprimirButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnPrinter
            Me.ImprimirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ImprimirButton.Location = New System.Drawing.Point(153, 294)
            Me.ImprimirButton.Name = "ImprimirButton"
            Me.ImprimirButton.Size = New System.Drawing.Size(97, 30)
            Me.ImprimirButton.TabIndex = 31
            Me.ImprimirButton.Text = "&Imprimir"
            Me.ImprimirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ImprimirButton.UseVisualStyleBackColor = True
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
            Me.CBarrasBarCodeControl.HeaderText = "PYC"
            Me.CBarrasBarCodeControl.LeftMargin = 1
            Me.CBarrasBarCodeControl.Location = New System.Drawing.Point(6, 7)
            Me.CBarrasBarCodeControl.Name = "CBarrasBarCodeControl"
            Me.CBarrasBarCodeControl.Padding = New System.Windows.Forms.Padding(1)
            Me.CBarrasBarCodeControl.ShowFooter = True
            Me.CBarrasBarCodeControl.ShowHeader = True
            Me.CBarrasBarCodeControl.Size = New System.Drawing.Size(306, 153)
            Me.CBarrasBarCodeControl.TabIndex = 0
            Me.CBarrasBarCodeControl.TopMargin = 1
            Me.CBarrasBarCodeControl.Weight = Miharu.Desktop.Controls.BarCode.BarCodeWeight.Small
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(256, 294)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(97, 30)
            Me.CerrarButton.TabIndex = 32
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Panel2
            '
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.Controls.Add(Me.CBarrasBarCodeControl)
            Me.Panel2.Location = New System.Drawing.Point(10, 11)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(318, 167)
            Me.Panel2.TabIndex = 2
            '
            'Panel1
            '
            Me.Panel1.BackColor = System.Drawing.SystemColors.Desktop
            Me.Panel1.Controls.Add(Me.Panel2)
            Me.Panel1.Location = New System.Drawing.Point(12, 78)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(336, 189)
            Me.Panel1.TabIndex = 35
            '
            'cbarrasDesktopCBarrasControl
            '
            Me.cbarrasDesktopCBarrasControl.Cantidad_Decimales = CType(2, Short)
            Me.cbarrasDesktopCBarrasControl.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.cbarrasDesktopCBarrasControl.DateFormat = Nothing
            Me.cbarrasDesktopCBarrasControl.DisabledEnter = False
            Me.cbarrasDesktopCBarrasControl.DisabledTab = False
            Me.cbarrasDesktopCBarrasControl.EnabledShortCuts = False
            Me.cbarrasDesktopCBarrasControl.FocusIn = System.Drawing.Color.LightYellow
            Me.cbarrasDesktopCBarrasControl.FocusOut = System.Drawing.Color.White
            Me.cbarrasDesktopCBarrasControl.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cbarrasDesktopCBarrasControl.Location = New System.Drawing.Point(12, 33)
            Me.cbarrasDesktopCBarrasControl.MaskedTextBox_Property = ""
            Me.cbarrasDesktopCBarrasControl.MaximumLength = CType(0, Short)
            Me.cbarrasDesktopCBarrasControl.MinimumLength = CType(0, Short)
            Me.cbarrasDesktopCBarrasControl.Name = "cbarrasDesktopCBarrasControl"
            Rango1.MaxValue = 1.7976931348623157E+308R
            Rango1.MinValue = 0.0R
            Me.cbarrasDesktopCBarrasControl.Rango = Rango1
            Me.cbarrasDesktopCBarrasControl.Size = New System.Drawing.Size(336, 20)
            Me.cbarrasDesktopCBarrasControl.TabIndex = 36
            Me.cbarrasDesktopCBarrasControl.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.cbarrasDesktopCBarrasControl.Usa_Decimales = False
            Me.cbarrasDesktopCBarrasControl.Validos_Cantidad_Puntos = False
            '
            'FormCBarrasFolderFile
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(364, 335)
            Me.Controls.Add(Me.cbarrasDesktopCBarrasControl)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.ImprimirButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Panel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCBarrasFolderFile"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Codigos de barras de Carpetas y documentos"
            Me.Panel2.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ImprimirButton As System.Windows.Forms.Button
        Friend WithEvents CBarrasBarCodeControl As Miharu.Desktop.Controls.BarCode.BarCodeControl
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents cbarrasDesktopCBarrasControl As DesktopTextBoxControl
    End Class
End Namespace