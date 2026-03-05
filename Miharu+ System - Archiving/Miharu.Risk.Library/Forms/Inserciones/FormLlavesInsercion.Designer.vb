Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.Inserciones
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormLlavesInsercion
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLlavesInsercion))
            Me.SpaceFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.cbarrasDesktopCBarrasControl = New Miharu.Desktop.Controls.DesktopCBarras.DesktopCBarrasControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'SpaceFlowLayoutPanel
            '
            Me.SpaceFlowLayoutPanel.Location = New System.Drawing.Point(12, 95)
            Me.SpaceFlowLayoutPanel.Name = "SpaceFlowLayoutPanel"
            Me.SpaceFlowLayoutPanel.Size = New System.Drawing.Size(334, 167)
            Me.SpaceFlowLayoutPanel.TabIndex = 3
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(261, 296)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(85, 30)
            Me.CerrarButton.TabIndex = 5
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(170, 296)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(85, 30)
            Me.AceptarButton.TabIndex = 4
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(12, 12)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(102, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Codigo de Barras"
            '
            'cbarrasDesktopCBarrasControl
            '            
            Me.cbarrasDesktopCBarrasControl.FocusIn = System.Drawing.Color.LightYellow
            Me.cbarrasDesktopCBarrasControl.FocusOut = System.Drawing.Color.White
            Me.cbarrasDesktopCBarrasControl.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cbarrasDesktopCBarrasControl.Location = New System.Drawing.Point(12, 31)
            Me.cbarrasDesktopCBarrasControl.MaxLength = 0
            Me.cbarrasDesktopCBarrasControl.Name = "cbarrasDesktopCBarrasControl"
            Me.cbarrasDesktopCBarrasControl.Size = New System.Drawing.Size(335, 20)
            Me.cbarrasDesktopCBarrasControl.TabIndex = 1
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(12, 76)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(43, 13)
            Me.Label3.TabIndex = 2
            Me.Label3.Text = "Llaves"
            '
            'FormLlavesInsercion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(364, 335)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.SpaceFlowLayoutPanel)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.cbarrasDesktopCBarrasControl)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormLlavesInsercion"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Inserciones"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents cbarrasDesktopCBarrasControl As DesktopCbarrasControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents SpaceFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents Label3 As System.Windows.Forms.Label
    End Class
End Namespace