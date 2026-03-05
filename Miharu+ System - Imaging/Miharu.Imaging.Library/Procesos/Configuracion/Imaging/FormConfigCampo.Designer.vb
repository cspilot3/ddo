Namespace Procesos.Configuracion.Imaging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormConfigCampo
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
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.FileNameTextBox = New System.Windows.Forms.TextBox()
            Me.Panel3 = New System.Windows.Forms.Panel()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.HLabel = New System.Windows.Forms.Label()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.WLabel = New System.Windows.Forms.Label()
            Me.SaveButton = New System.Windows.Forms.Button()
            Me.OpenButton = New System.Windows.Forms.Button()
            Me.YLabel = New System.Windows.Forms.Label()
            Me.XLabel = New System.Windows.Forms.Label()
            Me.MarcoDibujoPanel = New System.Windows.Forms.Panel()
            Me.ImagePanel = New System.Windows.Forms.Panel()
            Me.ImagePictureBox = New System.Windows.Forms.PictureBox()
            Me.Panel1.SuspendLayout()
            Me.Panel3.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.MarcoDibujoPanel.SuspendLayout()
            Me.ImagePanel.SuspendLayout()
            CType(Me.ImagePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.FileNameTextBox)
            Me.Panel1.Controls.Add(Me.Panel3)
            Me.Panel1.Controls.Add(Me.Panel2)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(854, 46)
            Me.Panel1.TabIndex = 1
            '
            'FileNameTextBox
            '
            Me.FileNameTextBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.FileNameTextBox.Location = New System.Drawing.Point(0, 23)
            Me.FileNameTextBox.Name = "FileNameTextBox"
            Me.FileNameTextBox.ReadOnly = True
            Me.FileNameTextBox.Size = New System.Drawing.Size(550, 20)
            Me.FileNameTextBox.TabIndex = 0
            '
            'Panel3
            '
            Me.Panel3.Controls.Add(Me.Label1)
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel3.Location = New System.Drawing.Point(0, 0)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(550, 23)
            Me.Panel3.TabIndex = 4
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(3, 5)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(42, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Imagen"
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.HLabel)
            Me.Panel2.Controls.Add(Me.CancelarButton)
            Me.Panel2.Controls.Add(Me.WLabel)
            Me.Panel2.Controls.Add(Me.SaveButton)
            Me.Panel2.Controls.Add(Me.OpenButton)
            Me.Panel2.Controls.Add(Me.YLabel)
            Me.Panel2.Controls.Add(Me.XLabel)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
            Me.Panel2.Location = New System.Drawing.Point(550, 0)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(304, 46)
            Me.Panel2.TabIndex = 3
            '
            'HLabel
            '
            Me.HLabel.AutoSize = True
            Me.HLabel.Location = New System.Drawing.Point(122, 25)
            Me.HLabel.Name = "HLabel"
            Me.HLabel.Size = New System.Drawing.Size(27, 13)
            Me.HLabel.TabIndex = 7
            Me.HLabel.Text = "H: 0"
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancelar
            Me.CancelarButton.Location = New System.Drawing.Point(256, 5)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(38, 38)
            Me.CancelarButton.TabIndex = 3
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'WLabel
            '
            Me.WLabel.AutoSize = True
            Me.WLabel.Location = New System.Drawing.Point(122, 10)
            Me.WLabel.Name = "WLabel"
            Me.WLabel.Size = New System.Drawing.Size(30, 13)
            Me.WLabel.TabIndex = 6
            Me.WLabel.Text = "W: 0"
            '
            'SaveButton
            '
            Me.SaveButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.filesave
            Me.SaveButton.Location = New System.Drawing.Point(192, 5)
            Me.SaveButton.Name = "SaveButton"
            Me.SaveButton.Size = New System.Drawing.Size(38, 38)
            Me.SaveButton.TabIndex = 2
            Me.SaveButton.UseVisualStyleBackColor = True
            '
            'OpenButton
            '
            Me.OpenButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.folder_add
            Me.OpenButton.Location = New System.Drawing.Point(3, 5)
            Me.OpenButton.Name = "OpenButton"
            Me.OpenButton.Size = New System.Drawing.Size(38, 38)
            Me.OpenButton.TabIndex = 1
            Me.OpenButton.UseVisualStyleBackColor = True
            '
            'YLabel
            '
            Me.YLabel.AutoSize = True
            Me.YLabel.Location = New System.Drawing.Point(63, 25)
            Me.YLabel.Name = "YLabel"
            Me.YLabel.Size = New System.Drawing.Size(26, 13)
            Me.YLabel.TabIndex = 5
            Me.YLabel.Text = "Y: 0"
            '
            'XLabel
            '
            Me.XLabel.AutoSize = True
            Me.XLabel.Location = New System.Drawing.Point(63, 10)
            Me.XLabel.Name = "XLabel"
            Me.XLabel.Size = New System.Drawing.Size(26, 13)
            Me.XLabel.TabIndex = 4
            Me.XLabel.Text = "X: 0"
            '
            'MarcoDibujoPanel
            '
            Me.MarcoDibujoPanel.AutoScroll = True
            Me.MarcoDibujoPanel.BackColor = System.Drawing.Color.Teal
            Me.MarcoDibujoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.MarcoDibujoPanel.Controls.Add(Me.ImagePanel)
            Me.MarcoDibujoPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.MarcoDibujoPanel.Location = New System.Drawing.Point(0, 46)
            Me.MarcoDibujoPanel.Name = "MarcoDibujoPanel"
            Me.MarcoDibujoPanel.Size = New System.Drawing.Size(854, 377)
            Me.MarcoDibujoPanel.TabIndex = 4
            '
            'ImagePanel
            '
            Me.ImagePanel.BackColor = System.Drawing.SystemColors.ControlDarkDark
            Me.ImagePanel.Controls.Add(Me.ImagePictureBox)
            Me.ImagePanel.Location = New System.Drawing.Point(16, 8)
            Me.ImagePanel.Name = "ImagePanel"
            Me.ImagePanel.Padding = New System.Windows.Forms.Padding(10)
            Me.ImagePanel.Size = New System.Drawing.Size(384, 264)
            Me.ImagePanel.TabIndex = 0
            '
            'ImagePictureBox
            '
            Me.ImagePictureBox.BackColor = System.Drawing.Color.White
            Me.ImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ImagePictureBox.Cursor = System.Windows.Forms.Cursors.Cross
            Me.ImagePictureBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ImagePictureBox.Location = New System.Drawing.Point(10, 10)
            Me.ImagePictureBox.Name = "ImagePictureBox"
            Me.ImagePictureBox.Size = New System.Drawing.Size(364, 244)
            Me.ImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.ImagePictureBox.TabIndex = 0
            Me.ImagePictureBox.TabStop = False
            '
            'FormConfigCampo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(854, 423)
            Me.Controls.Add(Me.MarcoDibujoPanel)
            Me.Controls.Add(Me.Panel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormConfigCampo"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Configurar campo"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.Panel3.ResumeLayout(False)
            Me.Panel3.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            Me.MarcoDibujoPanel.ResumeLayout(False)
            Me.ImagePanel.ResumeLayout(False)
            CType(Me.ImagePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents SaveButton As System.Windows.Forms.Button
        Friend WithEvents OpenButton As System.Windows.Forms.Button
        Friend WithEvents FileNameTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents HLabel As System.Windows.Forms.Label
        Friend WithEvents WLabel As System.Windows.Forms.Label
        Friend WithEvents YLabel As System.Windows.Forms.Label
        Friend WithEvents XLabel As System.Windows.Forms.Label
        Friend WithEvents MarcoDibujoPanel As System.Windows.Forms.Panel
        Friend WithEvents ImagePanel As System.Windows.Forms.Panel
        Friend WithEvents ImagePictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace