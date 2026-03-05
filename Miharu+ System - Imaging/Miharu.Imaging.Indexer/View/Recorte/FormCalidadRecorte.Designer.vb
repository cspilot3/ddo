Namespace View.Recorte

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCalidadRecorte
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCalidadRecorte))
            Me.MarcoDibujoPanel = New System.Windows.Forms.Panel()
            Me.ImagePanel = New System.Windows.Forms.Panel()
            Me.ImagePictureBox = New System.Windows.Forms.PictureBox()
            Me.PreviousFolioButton = New System.Windows.Forms.Button()
            Me.NextFolioButton = New System.Windows.Forms.Button()
            Me.OpcionesGroupBox = New System.Windows.Forms.GroupBox()
            Me.SaveButton = New System.Windows.Forms.Button()
            Me.ExitButton = New System.Windows.Forms.Button()
            Me.DataControlPanel = New System.Windows.Forms.Panel()
            Me.EstadoStatusStrip = New System.Windows.Forms.StatusStrip()
            Me.AvanceToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.MensajeToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.ProgresoToolStripProgressBar = New System.Windows.Forms.ToolStripProgressBar()
            Me.ContadorToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.VersionToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.MainPanel = New System.Windows.Forms.Panel()
            Me.ImageToolStrip = New System.Windows.Forms.ToolStrip()
            Me.ShowAccesosToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.OpcionesPanel = New System.Windows.Forms.Panel()
            Me.MarcoDibujoPanel.SuspendLayout()
            Me.ImagePanel.SuspendLayout()
            CType(Me.ImagePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.OpcionesGroupBox.SuspendLayout()
            Me.EstadoStatusStrip.SuspendLayout()
            Me.MainPanel.SuspendLayout()
            Me.ImageToolStrip.SuspendLayout()
            Me.OpcionesPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'MarcoDibujoPanel
            '
            Me.MarcoDibujoPanel.AutoScroll = True
            Me.MarcoDibujoPanel.AutoSize = True
            Me.MarcoDibujoPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.MarcoDibujoPanel.BackColor = System.Drawing.Color.Teal
            Me.MarcoDibujoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.MarcoDibujoPanel.Controls.Add(Me.ImagePanel)
            Me.MarcoDibujoPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.MarcoDibujoPanel.Location = New System.Drawing.Point(30, 25)
            Me.MarcoDibujoPanel.Name = "MarcoDibujoPanel"
            Me.MarcoDibujoPanel.Size = New System.Drawing.Size(580, 653)
            Me.MarcoDibujoPanel.TabIndex = 4
            '
            'ImagePanel
            '
            Me.ImagePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.ImagePanel.BackColor = System.Drawing.SystemColors.ControlDarkDark
            Me.ImagePanel.Controls.Add(Me.ImagePictureBox)
            Me.ImagePanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ImagePanel.Location = New System.Drawing.Point(0, 0)
            Me.ImagePanel.Name = "ImagePanel"
            Me.ImagePanel.Padding = New System.Windows.Forms.Padding(10)
            Me.ImagePanel.Size = New System.Drawing.Size(578, 651)
            Me.ImagePanel.TabIndex = 0
            '
            'ImagePictureBox
            '
            Me.ImagePictureBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ImagePictureBox.BackColor = System.Drawing.Color.Teal
            Me.ImagePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.ImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ImagePictureBox.Location = New System.Drawing.Point(10, 10)
            Me.ImagePictureBox.Name = "ImagePictureBox"
            Me.ImagePictureBox.Size = New System.Drawing.Size(558, 631)
            Me.ImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.ImagePictureBox.TabIndex = 0
            Me.ImagePictureBox.TabStop = False
            '
            'PreviousFolioButton
            '
            Me.PreviousFolioButton.BackColor = System.Drawing.SystemColors.Control
            Me.PreviousFolioButton.Dock = System.Windows.Forms.DockStyle.Left
            Me.PreviousFolioButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray
            Me.PreviousFolioButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveCaption
            Me.PreviousFolioButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption
            Me.PreviousFolioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.PreviousFolioButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.btnPaginaAnterior_Image
            Me.PreviousFolioButton.Location = New System.Drawing.Point(0, 25)
            Me.PreviousFolioButton.Name = "PreviousFolioButton"
            Me.PreviousFolioButton.Size = New System.Drawing.Size(30, 653)
            Me.PreviousFolioButton.TabIndex = 9
            Me.PreviousFolioButton.UseVisualStyleBackColor = False
            '
            'NextFolioButton
            '
            Me.NextFolioButton.Dock = System.Windows.Forms.DockStyle.Right
            Me.NextFolioButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray
            Me.NextFolioButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveCaption
            Me.NextFolioButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption
            Me.NextFolioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.NextFolioButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.btnPaginaSiguiente_Image
            Me.NextFolioButton.Location = New System.Drawing.Point(610, 25)
            Me.NextFolioButton.Name = "NextFolioButton"
            Me.NextFolioButton.Size = New System.Drawing.Size(30, 653)
            Me.NextFolioButton.TabIndex = 8
            Me.NextFolioButton.UseVisualStyleBackColor = False
            '
            'OpcionesGroupBox
            '
            Me.OpcionesGroupBox.Controls.Add(Me.SaveButton)
            Me.OpcionesGroupBox.Controls.Add(Me.ExitButton)
            Me.OpcionesGroupBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.OpcionesGroupBox.Location = New System.Drawing.Point(0, 0)
            Me.OpcionesGroupBox.Name = "OpcionesGroupBox"
            Me.OpcionesGroupBox.Size = New System.Drawing.Size(354, 60)
            Me.OpcionesGroupBox.TabIndex = 12
            Me.OpcionesGroupBox.TabStop = False
            Me.OpcionesGroupBox.Text = "Opciones"
            '
            'SaveButton
            '
            Me.SaveButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.filesave
            Me.SaveButton.Location = New System.Drawing.Point(194, 18)
            Me.SaveButton.Name = "SaveButton"
            Me.SaveButton.Size = New System.Drawing.Size(46, 36)
            Me.SaveButton.TabIndex = 2
            Me.SaveButton.TabStop = False
            Me.SaveButton.UseVisualStyleBackColor = True
            '
            'ExitButton
            '
            Me.ExitButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.cancelar
            Me.ExitButton.Location = New System.Drawing.Point(289, 18)
            Me.ExitButton.Name = "ExitButton"
            Me.ExitButton.Size = New System.Drawing.Size(46, 36)
            Me.ExitButton.TabIndex = 5
            Me.ExitButton.TabStop = False
            Me.ExitButton.UseVisualStyleBackColor = True
            '
            'DataControlPanel
            '
            Me.DataControlPanel.AutoScroll = True
            Me.DataControlPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DataControlPanel.Location = New System.Drawing.Point(0, 60)
            Me.DataControlPanel.Name = "DataControlPanel"
            Me.DataControlPanel.Size = New System.Drawing.Size(354, 618)
            Me.DataControlPanel.TabIndex = 0
            '
            'EstadoStatusStrip
            '
            Me.EstadoStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AvanceToolStripStatusLabel, Me.MensajeToolStripStatusLabel, Me.ProgresoToolStripProgressBar, Me.ContadorToolStripStatusLabel, Me.VersionToolStripStatusLabel})
            Me.EstadoStatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
            Me.EstadoStatusStrip.Location = New System.Drawing.Point(0, 678)
            Me.EstadoStatusStrip.Name = "EstadoStatusStrip"
            Me.EstadoStatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
            Me.EstadoStatusStrip.Size = New System.Drawing.Size(994, 24)
            Me.EstadoStatusStrip.TabIndex = 24
            Me.EstadoStatusStrip.Text = "StatusStrip1"
            '
            'AvanceToolStripStatusLabel
            '
            Me.AvanceToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
            Me.AvanceToolStripStatusLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.AvanceToolStripStatusLabel.Name = "AvanceToolStripStatusLabel"
            Me.AvanceToolStripStatusLabel.Size = New System.Drawing.Size(48, 19)
            Me.AvanceToolStripStatusLabel.Text = "Avance"
            '
            'MensajeToolStripStatusLabel
            '
            Me.MensajeToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
            Me.MensajeToolStripStatusLabel.ForeColor = System.Drawing.Color.Maroon
            Me.MensajeToolStripStatusLabel.Name = "MensajeToolStripStatusLabel"
            Me.MensajeToolStripStatusLabel.Size = New System.Drawing.Size(51, 19)
            Me.MensajeToolStripStatusLabel.Text = "Mensaje"
            '
            'ProgresoToolStripProgressBar
            '
            Me.ProgresoToolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ProgresoToolStripProgressBar.AutoToolTip = True
            Me.ProgresoToolStripProgressBar.Name = "ProgresoToolStripProgressBar"
            Me.ProgresoToolStripProgressBar.Size = New System.Drawing.Size(100, 18)
            '
            'ContadorToolStripStatusLabel
            '
            Me.ContadorToolStripStatusLabel.Name = "ContadorToolStripStatusLabel"
            Me.ContadorToolStripStatusLabel.Size = New System.Drawing.Size(67, 19)
            Me.ContadorToolStripStatusLabel.Text = "Paquetes: 1"
            '
            'VersionToolStripStatusLabel
            '
            Me.VersionToolStripStatusLabel.Name = "VersionToolStripStatusLabel"
            Me.VersionToolStripStatusLabel.Size = New System.Drawing.Size(40, 19)
            Me.VersionToolStripStatusLabel.Text = "1.0.0.3"
            '
            'MainPanel
            '
            Me.MainPanel.Controls.Add(Me.MarcoDibujoPanel)
            Me.MainPanel.Controls.Add(Me.NextFolioButton)
            Me.MainPanel.Controls.Add(Me.PreviousFolioButton)
            Me.MainPanel.Controls.Add(Me.ImageToolStrip)
            Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.MainPanel.Location = New System.Drawing.Point(0, 0)
            Me.MainPanel.Name = "MainPanel"
            Me.MainPanel.Size = New System.Drawing.Size(640, 678)
            Me.MainPanel.TabIndex = 25
            '
            'ImageToolStrip
            '
            Me.ImageToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowAccesosToolStripButton})
            Me.ImageToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
            Me.ImageToolStrip.Location = New System.Drawing.Point(0, 0)
            Me.ImageToolStrip.Name = "ImageToolStrip"
            Me.ImageToolStrip.Size = New System.Drawing.Size(640, 25)
            Me.ImageToolStrip.TabIndex = 11
            Me.ImageToolStrip.Text = "ToolStrip1"
            '
            'ShowAccesosToolStripButton
            '
            Me.ShowAccesosToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ShowAccesosToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ShowAccesosToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.lightning_go
            Me.ShowAccesosToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ShowAccesosToolStripButton.Name = "ShowAccesosToolStripButton"
            Me.ShowAccesosToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.ShowAccesosToolStripButton.ToolTipText = "Mostrar el listado de teclas de acceso rapido"
            '
            'OpcionesPanel
            '
            Me.OpcionesPanel.Controls.Add(Me.DataControlPanel)
            Me.OpcionesPanel.Controls.Add(Me.OpcionesGroupBox)
            Me.OpcionesPanel.Dock = System.Windows.Forms.DockStyle.Right
            Me.OpcionesPanel.Location = New System.Drawing.Point(640, 0)
            Me.OpcionesPanel.Name = "OpcionesPanel"
            Me.OpcionesPanel.Size = New System.Drawing.Size(354, 678)
            Me.OpcionesPanel.TabIndex = 26
            '
            'FormCalidadRecorte
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(994, 702)
            Me.ControlBox = False
            Me.Controls.Add(Me.MainPanel)
            Me.Controls.Add(Me.OpcionesPanel)
            Me.Controls.Add(Me.EstadoStatusStrip)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.KeyPreview = True
            Me.Name = "FormCalidadRecorte"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Calidad Recorte"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            Me.MarcoDibujoPanel.ResumeLayout(False)
            Me.ImagePanel.ResumeLayout(False)
            CType(Me.ImagePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.OpcionesGroupBox.ResumeLayout(False)
            Me.EstadoStatusStrip.ResumeLayout(False)
            Me.EstadoStatusStrip.PerformLayout()
            Me.MainPanel.ResumeLayout(False)
            Me.MainPanel.PerformLayout()
            Me.ImageToolStrip.ResumeLayout(False)
            Me.ImageToolStrip.PerformLayout()
            Me.OpcionesPanel.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents MarcoDibujoPanel As System.Windows.Forms.Panel
        Friend WithEvents ImagePanel As System.Windows.Forms.Panel
        Friend WithEvents ImagePictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents DataControlPanel As System.Windows.Forms.Panel
        Friend WithEvents PreviousFolioButton As System.Windows.Forms.Button
        Friend WithEvents NextFolioButton As System.Windows.Forms.Button
        Friend WithEvents EstadoStatusStrip As System.Windows.Forms.StatusStrip
        Friend WithEvents AvanceToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents MensajeToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents ProgresoToolStripProgressBar As System.Windows.Forms.ToolStripProgressBar
        Friend WithEvents ContadorToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents VersionToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents OpcionesGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ExitButton As System.Windows.Forms.Button
        Friend WithEvents SaveButton As System.Windows.Forms.Button
        Friend WithEvents OpcionesPanel As System.Windows.Forms.Panel
        Friend WithEvents ImageToolStrip As System.Windows.Forms.ToolStrip
        Friend WithEvents ShowAccesosToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents MainPanel As System.Windows.Forms.Panel
    End Class
End Namespace