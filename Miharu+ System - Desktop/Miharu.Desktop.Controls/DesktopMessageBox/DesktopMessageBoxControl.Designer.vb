Namespace DesktopMessageBox
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class DesktopMessageBoxControl
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
            Me.cancel = New System.Windows.Forms.Button()
            Me.ok = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.ErrorMessage = New System.Windows.Forms.TextBox()
            Me.IconPicture = New System.Windows.Forms.PictureBox()
            Me.TitleMessage = New System.Windows.Forms.Label()
            Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
            Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape()
            Me.HtmlErrorMessage = New System.Windows.Forms.WebBrowser()
            Me.chkVerificado = New System.Windows.Forms.CheckBox()
            Me.GroupBox1.SuspendLayout()
            CType(Me.IconPicture, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'cancel
            '
            Me.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cancel.Image = Global.Miharu.Desktop.Controls.My.Resources.Resources.Cancelar
            Me.cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.cancel.Location = New System.Drawing.Point(220, 134)
            Me.cancel.Name = "cancel"
            Me.cancel.Size = New System.Drawing.Size(91, 27)
            Me.cancel.TabIndex = 6
            Me.cancel.Text = "Cancelar"
            Me.cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.cancel.UseVisualStyleBackColor = True
            '
            'ok
            '
            Me.ok.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ok.Image = Global.Miharu.Desktop.Controls.My.Resources.Resources.Aceptar
            Me.ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ok.Location = New System.Drawing.Point(123, 134)
            Me.ok.Name = "ok"
            Me.ok.Size = New System.Drawing.Size(91, 27)
            Me.ok.TabIndex = 5
            Me.ok.Text = "Aceptar"
            Me.ok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ok.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.chkVerificado)
            Me.GroupBox1.Controls.Add(Me.ok)
            Me.GroupBox1.Controls.Add(Me.ErrorMessage)
            Me.GroupBox1.Controls.Add(Me.cancel)
            Me.GroupBox1.Controls.Add(Me.IconPicture)
            Me.GroupBox1.Controls.Add(Me.TitleMessage)
            Me.GroupBox1.Controls.Add(Me.ShapeContainer1)
            Me.GroupBox1.Controls.Add(Me.HtmlErrorMessage)
            Me.GroupBox1.Location = New System.Drawing.Point(7, 2)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(321, 175)
            Me.GroupBox1.TabIndex = 4
            Me.GroupBox1.TabStop = False
            '
            'ErrorMessage
            '
            Me.ErrorMessage.BackColor = System.Drawing.SystemColors.Control
            Me.ErrorMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.ErrorMessage.Location = New System.Drawing.Point(12, 51)
            Me.ErrorMessage.Multiline = True
            Me.ErrorMessage.Name = "ErrorMessage"
            Me.ErrorMessage.ReadOnly = True
            Me.ErrorMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.ErrorMessage.Size = New System.Drawing.Size(297, 77)
            Me.ErrorMessage.TabIndex = 7
            '
            'IconPicture
            '
            Me.IconPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.IconPicture.Location = New System.Drawing.Point(6, 10)
            Me.IconPicture.Name = "IconPicture"
            Me.IconPicture.Size = New System.Drawing.Size(39, 35)
            Me.IconPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.IconPicture.TabIndex = 2
            Me.IconPicture.TabStop = False
            '
            'TitleMessage
            '
            Me.TitleMessage.AutoSize = True
            Me.TitleMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TitleMessage.Location = New System.Drawing.Point(51, 16)
            Me.TitleMessage.Name = "TitleMessage"
            Me.TitleMessage.Size = New System.Drawing.Size(33, 14)
            Me.TitleMessage.TabIndex = 0
            Me.TitleMessage.Text = "Title"
            Me.TitleMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'ShapeContainer1
            '
            Me.ShapeContainer1.Location = New System.Drawing.Point(3, 16)
            Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
            Me.ShapeContainer1.Name = "ShapeContainer1"
            Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape1})
            Me.ShapeContainer1.Size = New System.Drawing.Size(315, 156)
            Me.ShapeContainer1.TabIndex = 3
            Me.ShapeContainer1.TabStop = False
            '
            'LineShape1
            '
            Me.LineShape1.Name = "LineShape1"
            Me.LineShape1.X1 = 50
            Me.LineShape1.X2 = 308
            Me.LineShape1.Y1 = 22
            Me.LineShape1.Y2 = 22
            '
            'HtmlErrorMessage
            '
            Me.HtmlErrorMessage.Location = New System.Drawing.Point(12, 51)
            Me.HtmlErrorMessage.MinimumSize = New System.Drawing.Size(20, 20)
            Me.HtmlErrorMessage.Name = "HtmlErrorMessage"
            Me.HtmlErrorMessage.Size = New System.Drawing.Size(297, 77)
            Me.HtmlErrorMessage.TabIndex = 9
            Me.HtmlErrorMessage.Visible = False
            '
            'chkVerificado
            '
            Me.chkVerificado.AutoSize = True
            Me.chkVerificado.Location = New System.Drawing.Point(6, 140)
            Me.chkVerificado.Name = "chkVerificado"
            Me.chkVerificado.Size = New System.Drawing.Size(73, 17)
            Me.chkVerificado.TabIndex = 10
            Me.chkVerificado.Text = "Verificado"
            Me.chkVerificado.UseVisualStyleBackColor = True
            '
            'DesktopMessageBoxControl
            '
            Me.AcceptButton = Me.ok
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.CancelButton = Me.cancel
            Me.ClientSize = New System.Drawing.Size(334, 184)
            Me.Controls.Add(Me.GroupBox1)
            Me.ForeColor = System.Drawing.Color.Black
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "DesktopMessageBoxControl"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "DesktopMessageBox"
            Me.TopMost = True
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.IconPicture, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents cancel As System.Windows.Forms.Button
        Friend WithEvents ok As System.Windows.Forms.Button
        Friend WithEvents IconPicture As System.Windows.Forms.PictureBox
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents TitleMessage As System.Windows.Forms.Label
        Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
        Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
        Friend WithEvents ErrorMessage As System.Windows.Forms.TextBox
        Friend WithEvents HtmlErrorMessage As System.Windows.Forms.WebBrowser
        Friend WithEvents chkVerificado As System.Windows.Forms.CheckBox
    End Class
End Namespace