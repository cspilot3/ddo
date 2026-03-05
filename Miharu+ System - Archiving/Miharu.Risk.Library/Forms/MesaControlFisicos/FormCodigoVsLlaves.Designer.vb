Namespace Forms.MesaControlFisicos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCodigoVsLlaves
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCodigoVsLlaves))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.CBarrasLabel = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.SpacePanel = New System.Windows.Forms.Panel()
            Me.DestaparButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(12, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(314, 19)
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "Validacion códigos de barras pegados"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(14, 48)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(105, 13)
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "Código de barras:"
            '
            'CBarrasLabel
            '
            Me.CBarrasLabel.AutoSize = True
            Me.CBarrasLabel.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CBarrasLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.CBarrasLabel.Location = New System.Drawing.Point(122, 48)
            Me.CBarrasLabel.Name = "CBarrasLabel"
            Me.CBarrasLabel.Size = New System.Drawing.Size(16, 16)
            Me.CBarrasLabel.TabIndex = 3
            Me.CBarrasLabel.Text = "_"
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.SpacePanel)
            Me.GroupBox1.Location = New System.Drawing.Point(17, 82)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(333, 164)
            Me.GroupBox1.TabIndex = 4
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Llaves"
            '
            'SpacePanel
            '
            Me.SpacePanel.Location = New System.Drawing.Point(7, 21)
            Me.SpacePanel.Name = "SpacePanel"
            Me.SpacePanel.Size = New System.Drawing.Size(320, 137)
            Me.SpacePanel.TabIndex = 0
            '
            'DestaparButton
            '
            Me.DestaparButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DestaparButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSigiente
            Me.DestaparButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.DestaparButton.Location = New System.Drawing.Point(130, 262)
            Me.DestaparButton.Name = "DestaparButton"
            Me.DestaparButton.Size = New System.Drawing.Size(107, 30)
            Me.DestaparButton.TabIndex = 13
            Me.DestaparButton.Text = "&Siguiente"
            Me.DestaparButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(243, 262)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(107, 30)
            Me.CerrarButton.TabIndex = 14
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormCodigoVsLlaves
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(362, 304)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.DestaparButton)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.CBarrasLabel)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCodigoVsLlaves"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Validacion codigos de barras"
            Me.GroupBox1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents CBarrasLabel As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents DestaparButton As System.Windows.Forms.Button
        Friend WithEvents SpacePanel As System.Windows.Forms.Panel
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
    End Class
End Namespace