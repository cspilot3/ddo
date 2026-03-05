Namespace Forms.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormLlavesDobleCaptura
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLlavesDobleCaptura))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.LlavesGroupBox = New System.Windows.Forms.GroupBox()
            Me.SpaceFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.LlavesGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.ForeColor = System.Drawing.Color.Maroon
            Me.Label1.Location = New System.Drawing.Point(15, 22)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(332, 13)
            Me.Label1.TabIndex = 2
            Me.Label1.Text = "Registro sobrante, por favor digite nuevamente las llaves"
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(437, 214)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(85, 30)
            Me.CerrarButton.TabIndex = 2
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(346, 214)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(85, 30)
            Me.AceptarButton.TabIndex = 1
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'LlavesGroupBox
            '
            Me.LlavesGroupBox.Controls.Add(Me.SpaceFlowLayoutPanel)
            Me.LlavesGroupBox.Location = New System.Drawing.Point(12, 52)
            Me.LlavesGroupBox.Name = "LlavesGroupBox"
            Me.LlavesGroupBox.Size = New System.Drawing.Size(510, 149)
            Me.LlavesGroupBox.TabIndex = 0
            Me.LlavesGroupBox.TabStop = False
            Me.LlavesGroupBox.Text = "Repetir Llaves"
            '
            'SpaceFlowLayoutPanel
            '
            Me.SpaceFlowLayoutPanel.Location = New System.Drawing.Point(21, 20)
            Me.SpaceFlowLayoutPanel.Name = "SpaceFlowLayoutPanel"
            Me.SpaceFlowLayoutPanel.Size = New System.Drawing.Size(483, 123)
            Me.SpaceFlowLayoutPanel.TabIndex = 0
            '
            'FormLlavesDobleCaptura
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(534, 253)
            Me.Controls.Add(Me.LlavesGroupBox)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.Label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormLlavesDobleCaptura"
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Doble captura sobrante"
            Me.TopMost = True
            Me.LlavesGroupBox.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents LlavesGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents SpaceFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
    End Class
End Namespace