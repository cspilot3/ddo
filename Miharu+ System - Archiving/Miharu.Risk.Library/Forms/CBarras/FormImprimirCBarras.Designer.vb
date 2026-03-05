Namespace Forms.CBarras
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormImprimirCBarras
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormImprimirCBarras))
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.CBarrasBarCodeControl = New Miharu.Desktop.Controls.BarCode.BarCodeControl()
            Me.ImprimirButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CBarrasProgressBar = New System.Windows.Forms.ProgressBar()
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.BackColor = System.Drawing.SystemColors.Desktop
            Me.Panel1.Controls.Add(Me.Panel2)
            Me.Panel1.Location = New System.Drawing.Point(14, 42)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(336, 189)
            Me.Panel1.TabIndex = 30
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
            'ImprimirButton
            '
            Me.ImprimirButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ImprimirButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnPrinter
            Me.ImprimirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ImprimirButton.Location = New System.Drawing.Point(152, 293)
            Me.ImprimirButton.Name = "ImprimirButton"
            Me.ImprimirButton.Size = New System.Drawing.Size(97, 30)
            Me.ImprimirButton.TabIndex = 0
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
            Me.CerrarButton.Location = New System.Drawing.Point(255, 293)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(97, 30)
            Me.CerrarButton.TabIndex = 1
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(12, 12)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(102, 13)
            Me.Label1.TabIndex = 22
            Me.Label1.Text = "Codigo de Barras"
            '
            'CBarrasProgressBar
            '
            Me.CBarrasProgressBar.BackColor = System.Drawing.Color.White
            Me.CBarrasProgressBar.ForeColor = System.Drawing.SystemColors.Desktop
            Me.CBarrasProgressBar.Location = New System.Drawing.Point(15, 239)
            Me.CBarrasProgressBar.Name = "CBarrasProgressBar"
            Me.CBarrasProgressBar.Size = New System.Drawing.Size(337, 33)
            Me.CBarrasProgressBar.TabIndex = 23
            '
            'FormImprimirCBarras
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(364, 335)
            Me.Controls.Add(Me.CBarrasProgressBar)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.ImprimirButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Panel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormImprimirCBarras"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Impresion de codigos de barras"
            Me.Panel1.ResumeLayout(False)
            Me.Panel2.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents ImprimirButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CBarrasProgressBar As System.Windows.Forms.ProgressBar
        Friend WithEvents CBarrasBarCodeControl As Miharu.Desktop.Controls.BarCode.BarCodeControl


    End Class
End Namespace