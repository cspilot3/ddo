Namespace Forms.Reportes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormVisorReportes
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormVisorReportes))
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
            Me.ReportesTreeView = New System.Windows.Forms.TreeView()
            Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
            Me.resultadosPanel = New System.Windows.Forms.Panel()
            Me.parametrosGroupBox = New System.Windows.Forms.GroupBox()
            Me.camposPanel = New System.Windows.Forms.Panel()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.ExportarMasivoButton = New System.Windows.Forms.Button()
            Me.ejecutarButton = New System.Windows.Forms.Button()
            Me.nombreReporteLabel = New System.Windows.Forms.Label()
            Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
            Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.parametrosGroupBox.SuspendLayout()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'SplitContainer1
            '
            Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer1.Name = "SplitContainer1"
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.ReportesTreeView)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.resultadosPanel)
            Me.SplitContainer1.Panel2.Controls.Add(Me.parametrosGroupBox)
            Me.SplitContainer1.Panel2.Controls.Add(Me.nombreReporteLabel)
            Me.SplitContainer1.Size = New System.Drawing.Size(792, 573)
            Me.SplitContainer1.SplitterDistance = 227
            Me.SplitContainer1.TabIndex = 0
            '
            'ReportesTreeView
            '
            Me.ReportesTreeView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ReportesTreeView.ImageIndex = 0
            Me.ReportesTreeView.ImageList = Me.ImageList
            Me.ReportesTreeView.Location = New System.Drawing.Point(0, 0)
            Me.ReportesTreeView.Name = "ReportesTreeView"
            Me.ReportesTreeView.SelectedImageIndex = 0
            Me.ReportesTreeView.Size = New System.Drawing.Size(227, 573)
            Me.ReportesTreeView.TabIndex = 0
            '
            'ImageList
            '
            Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
            Me.ImageList.Images.SetKeyName(0, "category.png")
            Me.ImageList.Images.SetKeyName(1, "report.png")
            '
            'resultadosPanel
            '
            Me.resultadosPanel.AutoScroll = True
            Me.resultadosPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.resultadosPanel.Location = New System.Drawing.Point(0, 216)
            Me.resultadosPanel.Name = "resultadosPanel"
            Me.resultadosPanel.Size = New System.Drawing.Size(561, 357)
            Me.resultadosPanel.TabIndex = 3
            '
            'parametrosGroupBox
            '
            Me.parametrosGroupBox.Controls.Add(Me.camposPanel)
            Me.parametrosGroupBox.Controls.Add(Me.Panel1)
            Me.parametrosGroupBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.parametrosGroupBox.Location = New System.Drawing.Point(0, 29)
            Me.parametrosGroupBox.Name = "parametrosGroupBox"
            Me.parametrosGroupBox.Padding = New System.Windows.Forms.Padding(5)
            Me.parametrosGroupBox.Size = New System.Drawing.Size(561, 187)
            Me.parametrosGroupBox.TabIndex = 0
            Me.parametrosGroupBox.TabStop = False
            Me.parametrosGroupBox.Text = "Parámetros"
            '
            'camposPanel
            '
            Me.camposPanel.AutoScroll = True
            Me.camposPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.camposPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.camposPanel.Location = New System.Drawing.Point(5, 19)
            Me.camposPanel.Name = "camposPanel"
            Me.camposPanel.Padding = New System.Windows.Forms.Padding(5)
            Me.camposPanel.Size = New System.Drawing.Size(494, 163)
            Me.camposPanel.TabIndex = 0
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.ExportarMasivoButton)
            Me.Panel1.Controls.Add(Me.ejecutarButton)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
            Me.Panel1.Location = New System.Drawing.Point(499, 19)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(57, 163)
            Me.Panel1.TabIndex = 15
            '
            'ExportarMasivoButton
            '
            Me.ExportarMasivoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ExportarMasivoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ExportarMasivoButton.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ExportarMasivoButton.Image = Global.Miharu.Desktop.My.Resources.Resources.Aceptar
            Me.ExportarMasivoButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.ExportarMasivoButton.Location = New System.Drawing.Point(3, 110)
            Me.ExportarMasivoButton.Name = "ExportarMasivoButton"
            Me.ExportarMasivoButton.Size = New System.Drawing.Size(51, 50)
            Me.ExportarMasivoButton.TabIndex = 2
            Me.ExportarMasivoButton.Text = "Exportar Masivo"
            Me.ExportarMasivoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
            Me.ToolTip.SetToolTip(Me.ExportarMasivoButton, "Ejecutar sentencia")
            Me.ExportarMasivoButton.UseVisualStyleBackColor = True
            Me.ExportarMasivoButton.Visible = False
            '
            'ejecutarButton
            '
            Me.ejecutarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ejecutarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ejecutarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.run
            Me.ejecutarButton.Location = New System.Drawing.Point(4, 3)
            Me.ejecutarButton.Name = "ejecutarButton"
            Me.ejecutarButton.Size = New System.Drawing.Size(46, 43)
            Me.ejecutarButton.TabIndex = 1
            Me.ejecutarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.ToolTip.SetToolTip(Me.ejecutarButton, "Ejecutar sentencia")
            Me.ejecutarButton.UseVisualStyleBackColor = True
            '
            'nombreReporteLabel
            '
            Me.nombreReporteLabel.Dock = System.Windows.Forms.DockStyle.Top
            Me.nombreReporteLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.nombreReporteLabel.Location = New System.Drawing.Point(0, 0)
            Me.nombreReporteLabel.Name = "nombreReporteLabel"
            Me.nombreReporteLabel.Size = New System.Drawing.Size(561, 29)
            Me.nombreReporteLabel.TabIndex = 5
            Me.nombreReporteLabel.Text = "Nombre Informe"
            Me.nombreReporteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'ContextMenuStrip1
            '
            Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
            Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
            '
            'FormVisorReportes
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(792, 573)
            Me.Controls.Add(Me.SplitContainer1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormVisorReportes"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Visualización de Reportes"
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.ResumeLayout(False)
            Me.parametrosGroupBox.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents ReportesTreeView As System.Windows.Forms.TreeView
        Friend WithEvents parametrosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ImageList As System.Windows.Forms.ImageList
        Friend WithEvents camposPanel As System.Windows.Forms.Panel
        Friend WithEvents ejecutarButton As System.Windows.Forms.Button
        Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents resultadosPanel As System.Windows.Forms.Panel
        Friend WithEvents nombreReporteLabel As System.Windows.Forms.Label
        Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents ExportarMasivoButton As System.Windows.Forms.Button
    End Class
End Namespace