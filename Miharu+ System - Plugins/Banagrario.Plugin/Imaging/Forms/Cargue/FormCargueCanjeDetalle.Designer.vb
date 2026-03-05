Namespace Imaging.Forms.Cargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCargueCanjeDetalle
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
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.Panel3 = New System.Windows.Forms.Panel()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.Advertencias_TextBox = New System.Windows.Forms.TextBox()
            Me.Panel4 = New System.Windows.Forms.Panel()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Errores_TextBox = New System.Windows.Forms.TextBox()
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.Panel3.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.Panel4.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
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
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(175, 8)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(88, 30)
            Me.AceptarButton.TabIndex = 44
            Me.AceptarButton.Text = "    &Aceptar"
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.Panel2)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel1.Location = New System.Drawing.Point(20, 355)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(718, 41)
            Me.Panel1.TabIndex = 45
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.CerrarButton)
            Me.Panel2.Controls.Add(Me.AceptarButton)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
            Me.Panel2.Location = New System.Drawing.Point(339, 0)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(379, 41)
            Me.Panel2.TabIndex = 45
            '
            'Panel3
            '
            Me.Panel3.Controls.Add(Me.GroupBox2)
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel3.Location = New System.Drawing.Point(20, 178)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Padding = New System.Windows.Forms.Padding(5)
            Me.Panel3.Size = New System.Drawing.Size(718, 177)
            Me.Panel3.TabIndex = 48
            '
            'GroupBox2
            '
            Me.GroupBox2.Controls.Add(Me.Advertencias_TextBox)
            Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GroupBox2.Location = New System.Drawing.Point(5, 5)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(708, 167)
            Me.GroupBox2.TabIndex = 48
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Advertencias"
            '
            'Advertencias_TextBox
            '
            Me.Advertencias_TextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Advertencias_TextBox.Location = New System.Drawing.Point(3, 16)
            Me.Advertencias_TextBox.Multiline = True
            Me.Advertencias_TextBox.Name = "Advertencias_TextBox"
            Me.Advertencias_TextBox.ReadOnly = True
            Me.Advertencias_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.Advertencias_TextBox.Size = New System.Drawing.Size(702, 148)
            Me.Advertencias_TextBox.TabIndex = 1
            '
            'Panel4
            '
            Me.Panel4.Controls.Add(Me.GroupBox1)
            Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel4.Location = New System.Drawing.Point(20, 20)
            Me.Panel4.Name = "Panel4"
            Me.Panel4.Padding = New System.Windows.Forms.Padding(5)
            Me.Panel4.Size = New System.Drawing.Size(718, 158)
            Me.Panel4.TabIndex = 49
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Errores_TextBox)
            Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GroupBox1.Location = New System.Drawing.Point(5, 5)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(708, 148)
            Me.GroupBox1.TabIndex = 47
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Errores del Archivo de Canje"
            '
            'Errores_TextBox
            '
            Me.Errores_TextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Errores_TextBox.Location = New System.Drawing.Point(3, 16)
            Me.Errores_TextBox.Multiline = True
            Me.Errores_TextBox.Name = "Errores_TextBox"
            Me.Errores_TextBox.ReadOnly = True
            Me.Errores_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.Errores_TextBox.Size = New System.Drawing.Size(702, 129)
            Me.Errores_TextBox.TabIndex = 0
            '
            'FormCargueCanjeDetalle
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(758, 416)
            Me.Controls.Add(Me.Panel4)
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.Panel1)
            Me.MinimizeBox = False
            Me.MinimumSize = New System.Drawing.Size(764, 444)
            Me.Name = "FormCargueCanjeDetalle"
            Me.Padding = New System.Windows.Forms.Padding(20)
            Me.Text = "Resumen cargue de canje"
            Me.Panel1.ResumeLayout(False)
            Me.Panel2.ResumeLayout(False)
            Me.Panel3.ResumeLayout(False)
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.Panel4.ResumeLayout(False)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents Advertencias_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Panel4 As System.Windows.Forms.Panel
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Errores_TextBox As System.Windows.Forms.TextBox
    End Class
End Namespace