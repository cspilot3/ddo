Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls

Namespace Forms.Devoluciones
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormImprimirDevolucion
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormImprimirDevolucion))
            Me.ReprocesoGroupBox = New System.Windows.Forms.GroupBox()
            Me.SelloTextBox = New DesktopTextBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.GuiaTextBox = New DesktopTextBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ImpresionGroupBox = New System.Windows.Forms.GroupBox()
            Me.OrdenPunteoCheckBox = New DesktopCheckBox.DesktopCheckBoxControl()
            Me.AgrupadoCheckBox = New DesktopCheckBox.DesktopCheckBoxControl()
            Me.ImprimirButton = New System.Windows.Forms.Button()
            Me.ReprocesoGroupBox.SuspendLayout()
            Me.ImpresionGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'ReprocesoGroupBox
            '
            Me.ReprocesoGroupBox.Controls.Add(Me.SelloTextBox)
            Me.ReprocesoGroupBox.Controls.Add(Me.Label2)
            Me.ReprocesoGroupBox.Controls.Add(Me.GuiaTextBox)
            Me.ReprocesoGroupBox.Controls.Add(Me.Label1)
            Me.ReprocesoGroupBox.Location = New System.Drawing.Point(4, 4)
            Me.ReprocesoGroupBox.Name = "ReprocesoGroupBox"
            Me.ReprocesoGroupBox.Size = New System.Drawing.Size(341, 78)
            Me.ReprocesoGroupBox.TabIndex = 0
            Me.ReprocesoGroupBox.TabStop = False
            Me.ReprocesoGroupBox.Text = "Devolución"
            '
            'SelloTextBox
            '
            Me.SelloTextBox.DisabledEnter = False
            Me.SelloTextBox.DisabledTab = False
            Me.SelloTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.SelloTextBox.FocusOut = System.Drawing.Color.White
            Me.SelloTextBox.Location = New System.Drawing.Point(63, 45)
            Me.SelloTextBox.Name = "SelloTextBox"
            Rango1.MaxValue = 2147483647
            Rango1.MinValue = 0
            Me.SelloTextBox.Rango = Rango1
            Me.SelloTextBox.Size = New System.Drawing.Size(271, 21)
            Me.SelloTextBox.TabIndex = 1
            Me.SelloTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(9, 48)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(37, 13)
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "Sello:"
            '
            'GuiaTextBox
            '
            Me.GuiaTextBox.DisabledEnter = False
            Me.GuiaTextBox.DisabledTab = False
            Me.GuiaTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.GuiaTextBox.FocusOut = System.Drawing.Color.White
            Me.GuiaTextBox.Location = New System.Drawing.Point(63, 18)
            Me.GuiaTextBox.Name = "GuiaTextBox"
            Rango2.MaxValue = 2147483647
            Rango2.MinValue = 0
            Me.GuiaTextBox.Rango = Rango2
            Me.GuiaTextBox.Size = New System.Drawing.Size(271, 21)
            Me.GuiaTextBox.TabIndex = 0
            Me.GuiaTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(9, 21)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(35, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Guía:"
            '
            'ImpresionGroupBox
            '
            Me.ImpresionGroupBox.Controls.Add(Me.OrdenPunteoCheckBox)
            Me.ImpresionGroupBox.Controls.Add(Me.AgrupadoCheckBox)
            Me.ImpresionGroupBox.Location = New System.Drawing.Point(4, 88)
            Me.ImpresionGroupBox.Name = "ImpresionGroupBox"
            Me.ImpresionGroupBox.Size = New System.Drawing.Size(260, 60)
            Me.ImpresionGroupBox.TabIndex = 1
            Me.ImpresionGroupBox.TabStop = False
            Me.ImpresionGroupBox.Text = "Impresión"
            '
            'OrdenPunteoCheckBox
            '
            Me.OrdenPunteoCheckBox.AutoSize = True
            Me.OrdenPunteoCheckBox.DisabledEnter = False
            Me.OrdenPunteoCheckBox.FocusIn = System.Drawing.Color.LightYellow
            Me.OrdenPunteoCheckBox.FocusOut = System.Drawing.SystemColors.Control
            Me.OrdenPunteoCheckBox.Location = New System.Drawing.Point(11, 39)
            Me.OrdenPunteoCheckBox.Name = "OrdenPunteoCheckBox"
            Me.OrdenPunteoCheckBox.Size = New System.Drawing.Size(163, 17)
            Me.OrdenPunteoCheckBox.TabIndex = 3
            Me.OrdenPunteoCheckBox.Text = "Reporte por orden de punteo"
            Me.OrdenPunteoCheckBox.UseVisualStyleBackColor = True
            '
            'AgrupadoCheckBox
            '
            Me.AgrupadoCheckBox.AutoSize = True
            Me.AgrupadoCheckBox.Checked = True
            Me.AgrupadoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
            Me.AgrupadoCheckBox.DisabledEnter = False
            Me.AgrupadoCheckBox.FocusIn = System.Drawing.Color.LightYellow
            Me.AgrupadoCheckBox.FocusOut = System.Drawing.SystemColors.Control
            Me.AgrupadoCheckBox.Location = New System.Drawing.Point(11, 19)
            Me.AgrupadoCheckBox.Name = "AgrupadoCheckBox"
            Me.AgrupadoCheckBox.Size = New System.Drawing.Size(112, 17)
            Me.AgrupadoCheckBox.TabIndex = 2
            Me.AgrupadoCheckBox.Text = "Reporte agrupado"
            Me.AgrupadoCheckBox.UseVisualStyleBackColor = True
            '
            'ImprimirButton
            '
            Me.ImprimirButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.print
            Me.ImprimirButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.ImprimirButton.Location = New System.Drawing.Point(270, 94)
            Me.ImprimirButton.Name = "ImprimirButton"
            Me.ImprimirButton.Size = New System.Drawing.Size(75, 54)
            Me.ImprimirButton.TabIndex = 4
            Me.ImprimirButton.Text = "&Imprimir"
            Me.ImprimirButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.ImprimirButton.UseVisualStyleBackColor = True
            '
            'FormImprimirDevolucion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(350, 157)
            Me.Controls.Add(Me.ImprimirButton)
            Me.Controls.Add(Me.ImpresionGroupBox)
            Me.Controls.Add(Me.ReprocesoGroupBox)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormImprimirDevolucion"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Impresión de Devoluciones"
            Me.ReprocesoGroupBox.ResumeLayout(False)
            Me.ReprocesoGroupBox.PerformLayout()
            Me.ImpresionGroupBox.ResumeLayout(False)
            Me.ImpresionGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ReprocesoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents SelloTextBox As DesktopTextBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents GuiaTextBox As DesktopTextBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ImpresionGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents OrdenPunteoCheckBox As DesktopCheckBox.DesktopCheckBoxControl
        Friend WithEvents AgrupadoCheckBox As DesktopCheckBox.DesktopCheckBoxControl
        Friend WithEvents ImprimirButton As System.Windows.Forms.Button
    End Class
End Namespace