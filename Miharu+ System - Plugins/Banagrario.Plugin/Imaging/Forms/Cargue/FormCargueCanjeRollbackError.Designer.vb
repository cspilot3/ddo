Namespace Imaging.Forms.Cargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCargueCanjeRollbackError
        Inherits System.Windows.Forms.Form

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
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Panel4 = New System.Windows.Forms.Panel()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Errores_TextBox = New System.Windows.Forms.TextBox()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Panel4.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'CerrarButton
            '
            Me.CerrarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir1
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(269, 8)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(107, 30)
            Me.CerrarButton.TabIndex = 36
            Me.CerrarButton.Text = "     &Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Panel4
            '
            Me.Panel4.Controls.Add(Me.GroupBox1)
            Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel4.Location = New System.Drawing.Point(0, 0)
            Me.Panel4.Name = "Panel4"
            Me.Panel4.Padding = New System.Windows.Forms.Padding(5)
            Me.Panel4.Size = New System.Drawing.Size(646, 403)
            Me.Panel4.TabIndex = 52
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Errores_TextBox)
            Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GroupBox1.Location = New System.Drawing.Point(5, 5)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(636, 393)
            Me.GroupBox1.TabIndex = 47
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Errores"
            '
            'Errores_TextBox
            '
            Me.Errores_TextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Errores_TextBox.Location = New System.Drawing.Point(3, 16)
            Me.Errores_TextBox.Multiline = True
            Me.Errores_TextBox.Name = "Errores_TextBox"
            Me.Errores_TextBox.ReadOnly = True
            Me.Errores_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.Errores_TextBox.Size = New System.Drawing.Size(630, 374)
            Me.Errores_TextBox.TabIndex = 0
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.CerrarButton)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
            Me.Panel2.Location = New System.Drawing.Point(267, 0)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(379, 41)
            Me.Panel2.TabIndex = 45
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.Panel2)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel1.Location = New System.Drawing.Point(0, 403)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(646, 41)
            Me.Panel1.TabIndex = 50
            '
            'FormCargueCanjeRollbackError
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(646, 444)
            Me.Controls.Add(Me.Panel4)
            Me.Controls.Add(Me.Panel1)
            Me.MinimizeBox = False
            Me.MinimumSize = New System.Drawing.Size(662, 482)
            Me.Name = "FormCargueCanjeRollbackError"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Transacciones que no se pudieron reversar"
            Me.Panel4.ResumeLayout(False)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Panel4 As System.Windows.Forms.Panel
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Errores_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
    End Class
End Namespace