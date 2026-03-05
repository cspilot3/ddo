
Imports Miharu.Imaging.Indexer.View.Indexacion.Table

Namespace View.Indexacion

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormIndexerView
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormIndexerView))
            Me.ToolTipText = New System.Windows.Forms.ToolTip(Me.components)
            Me.TipoDocumentalComboBox = New System.Windows.Forms.ComboBox()
            Me.EsquemaComboBox = New System.Windows.Forms.ComboBox()
            Me.Table_UpPanel = New System.Windows.Forms.Panel()
            Me.Table_ColumnOCRButton = New System.Windows.Forms.Button()
            Me.PageStatusLabel = New System.Windows.Forms.Label()
            Me.Table_SaveButton = New System.Windows.Forms.Button()
            Me.Table_CloseButton = New System.Windows.Forms.Button()
            Me.PreviousFolioButton = New System.Windows.Forms.Button()
            Me.NextFolioButton = New System.Windows.Forms.Button()
            Me.ExitButton = New System.Windows.Forms.Button()
            Me.SaveButton = New System.Windows.Forms.Button()
            Me.ReprocesoButton = New System.Windows.Forms.Button()
            Me.DesIndexarFolioButton = New System.Windows.Forms.Button()
            Me.NewFileButton = New System.Windows.Forms.Button()
            Me.NewFolderButton = New System.Windows.Forms.Button()
            Me.AddFolioButton = New System.Windows.Forms.Button()
            Me.DeleteFolioButton = New System.Windows.Forms.Button()
            Me.DocumentFileo_2Panel = New System.Windows.Forms.Panel()
            Me.Folio_2_2PictureBox = New System.Windows.Forms.PictureBox()
            Me.Folio_2_1PictureBox = New System.Windows.Forms.PictureBox()
            Me.ImageFlotantePanel = New System.Windows.Forms.Panel()
            Me.ImageFlotantePictureBox = New System.Windows.Forms.PictureBox()
            Me.ImageEditorPanel = New System.Windows.Forms.Panel()
            Me.MarcoDibujoPanel = New System.Windows.Forms.Panel()
            Me.ImagePanel = New System.Windows.Forms.Panel()
            Me.SeñaladorRulerControl = New Miharu.Imaging.Indexer.View.Indexacion.RulerControl()
            Me.ImagePictureBox = New System.Windows.Forms.PictureBox()
            Me.ImageToolStrip = New System.Windows.Forms.ToolStrip()
            Me.AjustarAltoToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.AjustarAnchoToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
            Me.ZoomOutToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.ZoomToolStripComboBox = New System.Windows.Forms.ToolStripComboBox()
            Me.ZoomInToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
            Me.RotateLeftToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.RotateRightToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.FlipHorizontalToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.FlipVerticalToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
            Me.ShowAccesosToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
            Me.ShowThumbnailToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.AnexosToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
            Me.AnexoTotalToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.AnexoFolderToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.AutoindexarToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
            Me.AutoIndexarToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.AnexosToolStripLabel = New System.Windows.Forms.ToolStripLabel()
            Me.RulerToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
            Me.RulerToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.SubrayarToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.QuitarSubrayadoToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
            Me.DrawingRactangleButton = New System.Windows.Forms.ToolStripButton()
            Me.DrawingTableButton = New System.Windows.Forms.ToolStripButton()
            Me.DrawVerticalLinesButton = New System.Windows.Forms.ToolStripButton()
            Me.DrawHorizontalLinesButton = New System.Windows.Forms.ToolStripButton()
            Me.DeleteLineButton = New System.Windows.Forms.ToolStripButton()
            Me.StartOCRButton = New System.Windows.Forms.ToolStripButton()
            Me.labelTableCoordinates = New System.Windows.Forms.ToolStripLabel()
            Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
            Me.ProgresoToolStripProgressBar = New System.Windows.Forms.ToolStripProgressBar()
            Me.MensajeToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.AvanceToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.OpcionesGroupBox = New System.Windows.Forms.GroupBox()
            Me.BreakButton = New System.Windows.Forms.Button()
            Me.OpcionesPanel = New System.Windows.Forms.Panel()
            Me.TotalSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.CapturaSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.CamposSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.CamposGroupBox = New System.Windows.Forms.GroupBox()
            Me.CamposPanel = New System.Windows.Forms.Panel()
            Me.CamposLlaveGroupBox = New System.Windows.Forms.GroupBox()
            Me.CamposLlavePanel = New System.Windows.Forms.Panel()
            Me.ValidacionesGroupBox = New System.Windows.Forms.GroupBox()
            Me.ValidacionesPanel = New System.Windows.Forms.Panel()
            Me.ValidacionListasGroupBox = New System.Windows.Forms.GroupBox()
            Me.ValidacionListasPanel = New System.Windows.Forms.Panel()
            Me.ToolTipLabel = New System.Windows.Forms.Label()
            Me.InformacionGroupBox = New System.Windows.Forms.GroupBox()
            Me.InformacionTextBox = New System.Windows.Forms.TextBox()
            Me.Folder_1Panel = New System.Windows.Forms.Panel()
            Me.EtiquetaFolder_1Label = New System.Windows.Forms.Label()
            Me.DocumentFileo_1Panel = New System.Windows.Forms.Panel()
            Me.Folio_1_3PictureBox = New System.Windows.Forms.PictureBox()
            Me.Folio_1_2PictureBox = New System.Windows.Forms.PictureBox()
            Me.Folio_1_1PictureBox = New System.Windows.Forms.PictureBox()
            Me.ContadorToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.BackThumbnailPanel = New System.Windows.Forms.Panel()
            Me.ThumbnailFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.EstadoStatusStrip = New System.Windows.Forms.StatusStrip()
            Me.VersionToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.IndexerTablePanel = New System.Windows.Forms.Panel()
            Me.CapturaIndexerTable = New Miharu.Imaging.Indexer.View.Indexacion.Table.IndexerTable()
            Me.DocumentsTabControl = New System.Windows.Forms.TabControl()
            Me.DocumentMainTabPage = New System.Windows.Forms.TabPage()
            Me.DocumentConfigTabPage = New System.Windows.Forms.TabPage()
            Me.DocumentsCheckedListBox = New System.Windows.Forms.CheckedListBox()
            Me.VisualizarSeleccionadosButton = New System.Windows.Forms.Button()
            Me.MainImageList = New System.Windows.Forms.ImageList(Me.components)
            Me.TipoDocumentoComboBoxControl = New Miharu.Imaging.Indexer.View.Indexacion.ComboBoxControl()
            Me.Table_UpPanel.SuspendLayout()
            Me.DocumentFileo_2Panel.SuspendLayout()
            CType(Me.Folio_2_2PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.Folio_2_1PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ImageFlotantePanel.SuspendLayout()
            CType(Me.ImageFlotantePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ImageEditorPanel.SuspendLayout()
            Me.MarcoDibujoPanel.SuspendLayout()
            Me.ImagePanel.SuspendLayout()
            CType(Me.ImagePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ImageToolStrip.SuspendLayout()
            Me.OpcionesGroupBox.SuspendLayout()
            Me.OpcionesPanel.SuspendLayout()
            CType(Me.TotalSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TotalSplitContainer.Panel1.SuspendLayout()
            Me.TotalSplitContainer.Panel2.SuspendLayout()
            Me.TotalSplitContainer.SuspendLayout()
            CType(Me.CapturaSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.CapturaSplitContainer.Panel1.SuspendLayout()
            Me.CapturaSplitContainer.Panel2.SuspendLayout()
            Me.CapturaSplitContainer.SuspendLayout()
            CType(Me.CamposSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.CamposSplitContainer.Panel1.SuspendLayout()
            Me.CamposSplitContainer.Panel2.SuspendLayout()
            Me.CamposSplitContainer.SuspendLayout()
            Me.CamposGroupBox.SuspendLayout()
            Me.CamposLlaveGroupBox.SuspendLayout()
            Me.ValidacionesGroupBox.SuspendLayout()
            Me.ValidacionListasGroupBox.SuspendLayout()
            Me.InformacionGroupBox.SuspendLayout()
            Me.Folder_1Panel.SuspendLayout()
            Me.DocumentFileo_1Panel.SuspendLayout()
            CType(Me.Folio_1_3PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.Folio_1_2PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.Folio_1_1PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.BackThumbnailPanel.SuspendLayout()
            Me.ThumbnailFlowLayoutPanel.SuspendLayout()
            Me.EstadoStatusStrip.SuspendLayout()
            Me.IndexerTablePanel.SuspendLayout()
            Me.DocumentsTabControl.SuspendLayout()
            Me.DocumentMainTabPage.SuspendLayout()
            Me.DocumentConfigTabPage.SuspendLayout()
            Me.SuspendLayout()
            '
            'TipoDocumentalComboBox
            '
            Me.TipoDocumentalComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipoDocumentalComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipoDocumentalComboBox.BackColor = System.Drawing.SystemColors.Window
            Me.TipoDocumentalComboBox.DropDownHeight = 1
            Me.TipoDocumentalComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipoDocumentalComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TipoDocumentalComboBox.FormattingEnabled = True
            Me.TipoDocumentalComboBox.IntegralHeight = False
            Me.TipoDocumentalComboBox.Location = New System.Drawing.Point(10, 103)
            Me.TipoDocumentalComboBox.Name = "TipoDocumentalComboBox"
            Me.TipoDocumentalComboBox.Size = New System.Drawing.Size(324, 24)
            Me.TipoDocumentalComboBox.TabIndex = 2
            Me.ToolTipText.SetToolTip(Me.TipoDocumentalComboBox, "Tipo DocumentFileal del DocumentFileo actual")
            '
            'EsquemaComboBox
            '
            Me.EsquemaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaComboBox.BackColor = System.Drawing.SystemColors.Window
            Me.EsquemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EsquemaComboBox.FormattingEnabled = True
            Me.EsquemaComboBox.Location = New System.Drawing.Point(10, 73)
            Me.EsquemaComboBox.Name = "EsquemaComboBox"
            Me.EsquemaComboBox.Size = New System.Drawing.Size(324, 24)
            Me.EsquemaComboBox.TabIndex = 1
            Me.ToolTipText.SetToolTip(Me.EsquemaComboBox, "Tipo DocumentFileal del DocumentFileo actual")
            '
            'Table_UpPanel
            '
            Me.Table_UpPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
            Me.Table_UpPanel.Controls.Add(Me.Table_ColumnOCRButton)
            Me.Table_UpPanel.Controls.Add(Me.PageStatusLabel)
            Me.Table_UpPanel.Controls.Add(Me.Table_SaveButton)
            Me.Table_UpPanel.Controls.Add(Me.Table_CloseButton)
            Me.Table_UpPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.Table_UpPanel.Location = New System.Drawing.Point(0, 0)
            Me.Table_UpPanel.Name = "Table_UpPanel"
            Me.Table_UpPanel.Size = New System.Drawing.Size(628, 23)
            Me.Table_UpPanel.TabIndex = 1
            Me.ToolTipText.SetToolTip(Me.Table_UpPanel, "Pagina 1 de 2")
            '
            'Table_ColumnOCRButton
            '
            Me.Table_ColumnOCRButton.Dock = System.Windows.Forms.DockStyle.Right
            Me.Table_ColumnOCRButton.Enabled = False
            Me.Table_ColumnOCRButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.play
            Me.Table_ColumnOCRButton.Location = New System.Drawing.Point(559, 0)
            Me.Table_ColumnOCRButton.Name = "Table_ColumnOCRButton"
            Me.Table_ColumnOCRButton.Size = New System.Drawing.Size(23, 23)
            Me.Table_ColumnOCRButton.TabIndex = 5
            Me.Table_ColumnOCRButton.UseVisualStyleBackColor = True
            Me.Table_ColumnOCRButton.Visible = False
            '
            'PageStatusLabel
            '
            Me.PageStatusLabel.AutoSize = True
            Me.PageStatusLabel.Dock = System.Windows.Forms.DockStyle.Left
            Me.PageStatusLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight
            Me.PageStatusLabel.Location = New System.Drawing.Point(0, 0)
            Me.PageStatusLabel.Margin = New System.Windows.Forms.Padding(3)
            Me.PageStatusLabel.Name = "PageStatusLabel"
            Me.PageStatusLabel.Padding = New System.Windows.Forms.Padding(6, 3, 6, 0)
            Me.PageStatusLabel.Size = New System.Drawing.Size(12, 16)
            Me.PageStatusLabel.TabIndex = 2
            Me.PageStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Table_SaveButton
            '
            Me.Table_SaveButton.Dock = System.Windows.Forms.DockStyle.Right
            Me.Table_SaveButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.tick
            Me.Table_SaveButton.Location = New System.Drawing.Point(582, 0)
            Me.Table_SaveButton.Name = "Table_SaveButton"
            Me.Table_SaveButton.Size = New System.Drawing.Size(23, 23)
            Me.Table_SaveButton.TabIndex = 1
            Me.Table_SaveButton.UseVisualStyleBackColor = True
            '
            'Table_CloseButton
            '
            Me.Table_CloseButton.Dock = System.Windows.Forms.DockStyle.Right
            Me.Table_CloseButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.cancel
            Me.Table_CloseButton.Location = New System.Drawing.Point(605, 0)
            Me.Table_CloseButton.Name = "Table_CloseButton"
            Me.Table_CloseButton.Size = New System.Drawing.Size(23, 23)
            Me.Table_CloseButton.TabIndex = 0
            Me.Table_CloseButton.UseVisualStyleBackColor = True
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
            Me.PreviousFolioButton.Location = New System.Drawing.Point(0, 27)
            Me.PreviousFolioButton.Name = "PreviousFolioButton"
            Me.PreviousFolioButton.Size = New System.Drawing.Size(30, 270)
            Me.PreviousFolioButton.TabIndex = 7
            Me.ToolTipText.SetToolTip(Me.PreviousFolioButton, "Ver el folio anterior")
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
            Me.NextFolioButton.Location = New System.Drawing.Point(586, 27)
            Me.NextFolioButton.Name = "NextFolioButton"
            Me.NextFolioButton.Size = New System.Drawing.Size(30, 270)
            Me.NextFolioButton.TabIndex = 6
            Me.ToolTipText.SetToolTip(Me.NextFolioButton, "Ver el siguiente folio")
            Me.NextFolioButton.UseVisualStyleBackColor = False
            '
            'ExitButton
            '
            Me.ExitButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.cancelar
            Me.ExitButton.Location = New System.Drawing.Point(288, 18)
            Me.ExitButton.Name = "ExitButton"
            Me.ExitButton.Size = New System.Drawing.Size(46, 36)
            Me.ExitButton.TabIndex = 5
            Me.ExitButton.TabStop = False
            Me.ToolTipText.SetToolTip(Me.ExitButton, "Descartar los cambios realizados")
            Me.ExitButton.UseVisualStyleBackColor = True
            '
            'SaveButton
            '
            Me.SaveButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.filesave
            Me.SaveButton.Location = New System.Drawing.Point(185, 19)
            Me.SaveButton.Name = "SaveButton"
            Me.SaveButton.Size = New System.Drawing.Size(46, 36)
            Me.SaveButton.TabIndex = 2
            Me.SaveButton.TabStop = False
            Me.ToolTipText.SetToolTip(Me.SaveButton, "Guardar los cambios realizados hasta el momento")
            Me.SaveButton.UseVisualStyleBackColor = True
            '
            'ReprocesoButton
            '
            Me.ReprocesoButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.Reproceso
            Me.ReprocesoButton.Location = New System.Drawing.Point(10, 18)
            Me.ReprocesoButton.Name = "ReprocesoButton"
            Me.ReprocesoButton.Size = New System.Drawing.Size(46, 36)
            Me.ReprocesoButton.TabIndex = 4
            Me.ReprocesoButton.TabStop = False
            Me.ToolTipText.SetToolTip(Me.ReprocesoButton, "Enviar a reproceso")
            Me.ReprocesoButton.UseVisualStyleBackColor = True
            '
            'DesIndexarFolioButton
            '
            Me.DesIndexarFolioButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.refresh
            Me.DesIndexarFolioButton.Location = New System.Drawing.Point(10, 18)
            Me.DesIndexarFolioButton.Name = "DesIndexarFolioButton"
            Me.DesIndexarFolioButton.Size = New System.Drawing.Size(46, 36)
            Me.DesIndexarFolioButton.TabIndex = 5
            Me.DesIndexarFolioButton.TabStop = False
            Me.ToolTipText.SetToolTip(Me.DesIndexarFolioButton, "Desindexar el último Folio")
            Me.DesIndexarFolioButton.UseVisualStyleBackColor = True
            '
            'NewFileButton
            '
            Me.NewFileButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.document_add
            Me.NewFileButton.Location = New System.Drawing.Point(76, 18)
            Me.NewFileButton.Name = "NewFileButton"
            Me.NewFileButton.Size = New System.Drawing.Size(46, 36)
            Me.NewFileButton.TabIndex = 1
            Me.NewFileButton.TabStop = False
            Me.ToolTipText.SetToolTip(Me.NewFileButton, "Iniciar un nuevo Documento")
            Me.NewFileButton.UseVisualStyleBackColor = True
            '
            'NewFolderButton
            '
            Me.NewFolderButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.folder_add
            Me.NewFolderButton.Location = New System.Drawing.Point(128, 18)
            Me.NewFolderButton.Name = "NewFolderButton"
            Me.NewFolderButton.Size = New System.Drawing.Size(46, 36)
            Me.NewFolderButton.TabIndex = 0
            Me.NewFolderButton.TabStop = False
            Me.ToolTipText.SetToolTip(Me.NewFolderButton, "Iniciar un nuevo Folder")
            Me.NewFolderButton.UseVisualStyleBackColor = True
            '
            'AddFolioButton
            '
            Me.AddFolioButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.sheet_add
            Me.AddFolioButton.Location = New System.Drawing.Point(185, 19)
            Me.AddFolioButton.Name = "AddFolioButton"
            Me.AddFolioButton.Size = New System.Drawing.Size(46, 36)
            Me.AddFolioButton.TabIndex = 3
            Me.AddFolioButton.TabStop = False
            Me.ToolTipText.SetToolTip(Me.AddFolioButton, "Insertar un nuevo folio")
            Me.AddFolioButton.UseVisualStyleBackColor = True
            '
            'DeleteFolioButton
            '
            Me.DeleteFolioButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.sheet_delete
            Me.DeleteFolioButton.Location = New System.Drawing.Point(237, 18)
            Me.DeleteFolioButton.Name = "DeleteFolioButton"
            Me.DeleteFolioButton.Size = New System.Drawing.Size(46, 36)
            Me.DeleteFolioButton.TabIndex = 2
            Me.DeleteFolioButton.TabStop = False
            Me.ToolTipText.SetToolTip(Me.DeleteFolioButton, "Eliminar el Folio actual")
            Me.DeleteFolioButton.UseVisualStyleBackColor = True
            '
            'DocumentFileo_2Panel
            '
            Me.DocumentFileo_2Panel.BackColor = System.Drawing.SystemColors.Control
            Me.DocumentFileo_2Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.DocumentFileo_2Panel.Controls.Add(Me.Folio_2_2PictureBox)
            Me.DocumentFileo_2Panel.Controls.Add(Me.Folio_2_1PictureBox)
            Me.DocumentFileo_2Panel.Location = New System.Drawing.Point(181, 3)
            Me.DocumentFileo_2Panel.Name = "DocumentFileo_2Panel"
            Me.DocumentFileo_2Panel.Size = New System.Drawing.Size(95, 66)
            Me.DocumentFileo_2Panel.TabIndex = 2
            '
            'Folio_2_2PictureBox
            '
            Me.Folio_2_2PictureBox.BackColor = System.Drawing.Color.White
            Me.Folio_2_2PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Folio_2_2PictureBox.Location = New System.Drawing.Point(49, 3)
            Me.Folio_2_2PictureBox.Name = "Folio_2_2PictureBox"
            Me.Folio_2_2PictureBox.Size = New System.Drawing.Size(40, 58)
            Me.Folio_2_2PictureBox.TabIndex = 3
            Me.Folio_2_2PictureBox.TabStop = False
            '
            'Folio_2_1PictureBox
            '
            Me.Folio_2_1PictureBox.BackColor = System.Drawing.Color.White
            Me.Folio_2_1PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.Folio_2_1PictureBox.Location = New System.Drawing.Point(3, 3)
            Me.Folio_2_1PictureBox.Name = "Folio_2_1PictureBox"
            Me.Folio_2_1PictureBox.Size = New System.Drawing.Size(40, 58)
            Me.Folio_2_1PictureBox.TabIndex = 2
            Me.Folio_2_1PictureBox.TabStop = False
            '
            'ImageFlotantePanel
            '
            Me.ImageFlotantePanel.AutoSize = True
            Me.ImageFlotantePanel.BackColor = System.Drawing.SystemColors.ControlDarkDark
            Me.ImageFlotantePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ImageFlotantePanel.Controls.Add(Me.ImageFlotantePictureBox)
            Me.ImageFlotantePanel.Location = New System.Drawing.Point(783, 332)
            Me.ImageFlotantePanel.Margin = New System.Windows.Forms.Padding(0)
            Me.ImageFlotantePanel.Name = "ImageFlotantePanel"
            Me.ImageFlotantePanel.Padding = New System.Windows.Forms.Padding(2)
            Me.ImageFlotantePanel.Size = New System.Drawing.Size(166, 226)
            Me.ImageFlotantePanel.TabIndex = 22
            Me.ImageFlotantePanel.Visible = False
            '
            'ImageFlotantePictureBox
            '
            Me.ImageFlotantePictureBox.BackColor = System.Drawing.Color.White
            Me.ImageFlotantePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ImageFlotantePictureBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ImageFlotantePictureBox.Location = New System.Drawing.Point(2, 2)
            Me.ImageFlotantePictureBox.Margin = New System.Windows.Forms.Padding(0)
            Me.ImageFlotantePictureBox.Name = "ImageFlotantePictureBox"
            Me.ImageFlotantePictureBox.Size = New System.Drawing.Size(160, 220)
            Me.ImageFlotantePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.ImageFlotantePictureBox.TabIndex = 0
            Me.ImageFlotantePictureBox.TabStop = False
            '
            'ImageEditorPanel
            '
            Me.ImageEditorPanel.Controls.Add(Me.MarcoDibujoPanel)
            Me.ImageEditorPanel.Controls.Add(Me.PreviousFolioButton)
            Me.ImageEditorPanel.Controls.Add(Me.NextFolioButton)
            Me.ImageEditorPanel.Controls.Add(Me.ImageToolStrip)
            Me.ImageEditorPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ImageEditorPanel.Location = New System.Drawing.Point(3, 3)
            Me.ImageEditorPanel.Name = "ImageEditorPanel"
            Me.ImageEditorPanel.Padding = New System.Windows.Forms.Padding(0, 0, 0, 6)
            Me.ImageEditorPanel.Size = New System.Drawing.Size(616, 303)
            Me.ImageEditorPanel.TabIndex = 20
            '
            'MarcoDibujoPanel
            '
            Me.MarcoDibujoPanel.AutoScroll = True
            Me.MarcoDibujoPanel.BackColor = System.Drawing.Color.Teal
            Me.MarcoDibujoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.MarcoDibujoPanel.Controls.Add(Me.ImagePanel)
            Me.MarcoDibujoPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.MarcoDibujoPanel.Location = New System.Drawing.Point(30, 27)
            Me.MarcoDibujoPanel.Name = "MarcoDibujoPanel"
            Me.MarcoDibujoPanel.Size = New System.Drawing.Size(556, 270)
            Me.MarcoDibujoPanel.TabIndex = 3
            '
            'ImagePanel
            '
            Me.ImagePanel.BackColor = System.Drawing.SystemColors.ControlDarkDark
            Me.ImagePanel.Controls.Add(Me.SeñaladorRulerControl)
            Me.ImagePanel.Controls.Add(Me.ImagePictureBox)
            Me.ImagePanel.Location = New System.Drawing.Point(7, 8)
            Me.ImagePanel.Name = "ImagePanel"
            Me.ImagePanel.Padding = New System.Windows.Forms.Padding(10)
            Me.ImagePanel.Size = New System.Drawing.Size(384, 264)
            Me.ImagePanel.TabIndex = 0
            '
            'SeñaladorRulerControl
            '
            Me.SeñaladorRulerControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SeñaladorRulerControl.BackColor = System.Drawing.Color.SteelBlue
            Me.SeñaladorRulerControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
            Me.SeñaladorRulerControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.SeñaladorRulerControl.Location = New System.Drawing.Point(5, 30)
            Me.SeñaladorRulerControl.Name = "SeñaladorRulerControl"
            Me.SeñaladorRulerControl.Size = New System.Drawing.Size(374, 40)
            Me.SeñaladorRulerControl.TabIndex = 1
            Me.SeñaladorRulerControl.Visible = False
            '
            'ImagePictureBox
            '
            Me.ImagePictureBox.BackColor = System.Drawing.Color.White
            Me.ImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ImagePictureBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ImagePictureBox.Location = New System.Drawing.Point(10, 10)
            Me.ImagePictureBox.Name = "ImagePictureBox"
            Me.ImagePictureBox.Size = New System.Drawing.Size(364, 244)
            Me.ImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.ImagePictureBox.TabIndex = 0
            Me.ImagePictureBox.TabStop = False
            '
            'ImageToolStrip
            '
            Me.ImageToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AjustarAltoToolStripButton, Me.AjustarAnchoToolStripButton, Me.ToolStripSeparator1, Me.ZoomOutToolStripButton, Me.ZoomToolStripComboBox, Me.ZoomInToolStripButton, Me.ToolStripSeparator2, Me.RotateLeftToolStripButton, Me.RotateRightToolStripButton, Me.FlipHorizontalToolStripButton, Me.FlipVerticalToolStripButton, Me.ToolStripSeparator4, Me.ShowAccesosToolStripButton, Me.ToolStripSeparator3, Me.ShowThumbnailToolStripButton, Me.AnexosToolStripSeparator, Me.AnexoTotalToolStripButton, Me.AnexoFolderToolStripButton, Me.AutoindexarToolStripSeparator, Me.AutoIndexarToolStripButton, Me.AnexosToolStripLabel, Me.RulerToolStripSeparator, Me.RulerToolStripButton, Me.SubrayarToolStripButton, Me.QuitarSubrayadoToolStripButton, Me.ToolStripSeparator6, Me.DrawingRactangleButton, Me.DrawingTableButton, Me.DrawVerticalLinesButton, Me.DrawHorizontalLinesButton, Me.DeleteLineButton, Me.StartOCRButton, Me.labelTableCoordinates, Me.ToolStripSeparator7})
            Me.ImageToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
            Me.ImageToolStrip.Location = New System.Drawing.Point(0, 0)
            Me.ImageToolStrip.Name = "ImageToolStrip"
            Me.ImageToolStrip.Size = New System.Drawing.Size(616, 27)
            Me.ImageToolStrip.TabIndex = 2
            Me.ImageToolStrip.Text = "ToolStrip1"
            '
            'AjustarAltoToolStripButton
            '
            Me.AjustarAltoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.AjustarAltoToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.btnAjustarAlto_Image
            Me.AjustarAltoToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.AjustarAltoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.AjustarAltoToolStripButton.Name = "AjustarAltoToolStripButton"
            Me.AjustarAltoToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.AjustarAltoToolStripButton.ToolTipText = "Ajustar la imagen al alto de la pantalla"
            '
            'AjustarAnchoToolStripButton
            '
            Me.AjustarAnchoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.AjustarAnchoToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.btnAjustarAncho_Image
            Me.AjustarAnchoToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.AjustarAnchoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.AjustarAnchoToolStripButton.Name = "AjustarAnchoToolStripButton"
            Me.AjustarAnchoToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.AjustarAnchoToolStripButton.ToolTipText = "Ajustar la imagen al ancho de la pantalla"
            '
            'ToolStripSeparator1
            '
            Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
            Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 27)
            '
            'ZoomOutToolStripButton
            '
            Me.ZoomOutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ZoomOutToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.btnZoomOut_Image
            Me.ZoomOutToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.ZoomOutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ZoomOutToolStripButton.Name = "ZoomOutToolStripButton"
            Me.ZoomOutToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.ZoomOutToolStripButton.ToolTipText = "Alejar la imagen"
            '
            'ZoomToolStripComboBox
            '
            Me.ZoomToolStripComboBox.AutoSize = False
            Me.ZoomToolStripComboBox.Items.AddRange(New Object() {"10%", "25%", "50%", "75%", "100%", "150%", "200%", "400%"})
            Me.ZoomToolStripComboBox.Name = "ZoomToolStripComboBox"
            Me.ZoomToolStripComboBox.Size = New System.Drawing.Size(70, 23)
            Me.ZoomToolStripComboBox.Text = "100%"
            Me.ZoomToolStripComboBox.ToolTipText = "Ajustar el tamaño de la imagen"
            '
            'ZoomInToolStripButton
            '
            Me.ZoomInToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ZoomInToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.btnZoomIn_Image
            Me.ZoomInToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.ZoomInToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ZoomInToolStripButton.Name = "ZoomInToolStripButton"
            Me.ZoomInToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.ZoomInToolStripButton.ToolTipText = "Acercar la imagen"
            '
            'ToolStripSeparator2
            '
            Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
            Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 27)
            '
            'RotateLeftToolStripButton
            '
            Me.RotateLeftToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.RotateLeftToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.btnRotarIzquierda_Image
            Me.RotateLeftToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.RotateLeftToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.RotateLeftToolStripButton.Name = "RotateLeftToolStripButton"
            Me.RotateLeftToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.RotateLeftToolStripButton.ToolTipText = "Rotar la imagen a la izquierda"
            '
            'RotateRightToolStripButton
            '
            Me.RotateRightToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.RotateRightToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.btnRotarDerecha_Image
            Me.RotateRightToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.RotateRightToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.RotateRightToolStripButton.Name = "RotateRightToolStripButton"
            Me.RotateRightToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.RotateRightToolStripButton.ToolTipText = "Rotar la imagen a la derecha"
            '
            'FlipHorizontalToolStripButton
            '
            Me.FlipHorizontalToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.FlipHorizontalToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.btnReflejarHorizontal_Image
            Me.FlipHorizontalToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.FlipHorizontalToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.FlipHorizontalToolStripButton.Name = "FlipHorizontalToolStripButton"
            Me.FlipHorizontalToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.FlipHorizontalToolStripButton.ToolTipText = "Reflejar la imagen horizontalmente"
            '
            'FlipVerticalToolStripButton
            '
            Me.FlipVerticalToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.FlipVerticalToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.btnReflejarVertical_Image
            Me.FlipVerticalToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.FlipVerticalToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.FlipVerticalToolStripButton.Name = "FlipVerticalToolStripButton"
            Me.FlipVerticalToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.FlipVerticalToolStripButton.ToolTipText = "Reflejar la imagen verticalmente"
            '
            'ToolStripSeparator4
            '
            Me.ToolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
            Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 27)
            '
            'ShowAccesosToolStripButton
            '
            Me.ShowAccesosToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ShowAccesosToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ShowAccesosToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.lightning_go
            Me.ShowAccesosToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ShowAccesosToolStripButton.Name = "ShowAccesosToolStripButton"
            Me.ShowAccesosToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.ShowAccesosToolStripButton.ToolTipText = "Mostrar el listado de teclas de acceso rapido"
            '
            'ToolStripSeparator3
            '
            Me.ToolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
            Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 27)
            '
            'ShowThumbnailToolStripButton
            '
            Me.ShowThumbnailToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ShowThumbnailToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ShowThumbnailToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.film
            Me.ShowThumbnailToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ShowThumbnailToolStripButton.Name = "ShowThumbnailToolStripButton"
            Me.ShowThumbnailToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.ShowThumbnailToolStripButton.ToolTipText = "Mostrar el esquema de indexación"
            '
            'AnexosToolStripSeparator
            '
            Me.AnexosToolStripSeparator.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.AnexosToolStripSeparator.Name = "AnexosToolStripSeparator"
            Me.AnexosToolStripSeparator.Size = New System.Drawing.Size(6, 27)
            '
            'AnexoTotalToolStripButton
            '
            Me.AnexoTotalToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.AnexoTotalToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.AnexoTotalToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.anexo_completo
            Me.AnexoTotalToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.AnexoTotalToolStripButton.Name = "AnexoTotalToolStripButton"
            Me.AnexoTotalToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.AnexoTotalToolStripButton.ToolTipText = "Marcar todo el documento como anexo"
            '
            'AnexoFolderToolStripButton
            '
            Me.AnexoFolderToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.AnexoFolderToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.AnexoFolderToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.anexo_folder
            Me.AnexoFolderToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.AnexoFolderToolStripButton.Name = "AnexoFolderToolStripButton"
            Me.AnexoFolderToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.AnexoFolderToolStripButton.ToolTipText = "Marcar folder actual como anexo"
            '
            'AutoindexarToolStripSeparator
            '
            Me.AutoindexarToolStripSeparator.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.AutoindexarToolStripSeparator.Name = "AutoindexarToolStripSeparator"
            Me.AutoindexarToolStripSeparator.Size = New System.Drawing.Size(6, 27)
            '
            'AutoIndexarToolStripButton
            '
            Me.AutoIndexarToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.AutoIndexarToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.AutoIndexarToolStripButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.auto_indexar
            Me.AutoIndexarToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.AutoIndexarToolStripButton.Name = "AutoIndexarToolStripButton"
            Me.AutoIndexarToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.AutoIndexarToolStripButton.ToolTipText = "Auto indexar el paquete a partir de la imagen actual"
            '
            'AnexosToolStripLabel
            '
            Me.AnexosToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.AnexosToolStripLabel.BackColor = System.Drawing.SystemColors.Control
            Me.AnexosToolStripLabel.ForeColor = System.Drawing.Color.Red
            Me.AnexosToolStripLabel.Name = "AnexosToolStripLabel"
            Me.AnexosToolStripLabel.Size = New System.Drawing.Size(12, 24)
            Me.AnexosToolStripLabel.Text = "-"
            '
            'RulerToolStripSeparator
            '
            Me.RulerToolStripSeparator.Name = "RulerToolStripSeparator"
            Me.RulerToolStripSeparator.Size = New System.Drawing.Size(6, 27)
            '
            'RulerToolStripButton
            '
            Me.RulerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.RulerToolStripButton.Image = CType(resources.GetObject("RulerToolStripButton.Image"), System.Drawing.Image)
            Me.RulerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.RulerToolStripButton.Name = "RulerToolStripButton"
            Me.RulerToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.RulerToolStripButton.ToolTipText = "Señalador"
            '
            'SubrayarToolStripButton
            '
            Me.SubrayarToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.SubrayarToolStripButton.Image = CType(resources.GetObject("SubrayarToolStripButton.Image"), System.Drawing.Image)
            Me.SubrayarToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.SubrayarToolStripButton.Name = "SubrayarToolStripButton"
            Me.SubrayarToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.SubrayarToolStripButton.ToolTipText = "Subrayar"
            '
            'QuitarSubrayadoToolStripButton
            '
            Me.QuitarSubrayadoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.QuitarSubrayadoToolStripButton.Image = CType(resources.GetObject("QuitarSubrayadoToolStripButton.Image"), System.Drawing.Image)
            Me.QuitarSubrayadoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.QuitarSubrayadoToolStripButton.Name = "QuitarSubrayadoToolStripButton"
            Me.QuitarSubrayadoToolStripButton.Size = New System.Drawing.Size(23, 24)
            Me.QuitarSubrayadoToolStripButton.ToolTipText = "Borrar el subrayado"
            '
            'ToolStripSeparator6
            '
            Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
            Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 27)
            '
            'DrawingRactangleButton
            '
            Me.DrawingRactangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.DrawingRactangleButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.lente
            Me.DrawingRactangleButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.DrawingRactangleButton.Name = "DrawingRactangleButton"
            Me.DrawingRactangleButton.Size = New System.Drawing.Size(23, 24)
            Me.DrawingRactangleButton.ToolTipText = "Dibujar Seleccion OCR"
            Me.DrawingRactangleButton.Visible = False
            '
            'DrawingTableButton
            '
            Me.DrawingTableButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.DrawingTableButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.draw_table
            Me.DrawingTableButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.DrawingTableButton.Name = "DrawingTableButton"
            Me.DrawingTableButton.Size = New System.Drawing.Size(23, 24)
            Me.DrawingTableButton.ToolTipText = "Dibujar tabla"
            Me.DrawingTableButton.Visible = False
            '
            'DrawVerticalLinesButton
            '
            Me.DrawVerticalLinesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.DrawVerticalLinesButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.draw_Vertical_Table
            Me.DrawVerticalLinesButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.DrawVerticalLinesButton.Name = "DrawVerticalLinesButton"
            Me.DrawVerticalLinesButton.Size = New System.Drawing.Size(23, 24)
            Me.DrawVerticalLinesButton.ToolTipText = "Dibujar linea vertical"
            Me.DrawVerticalLinesButton.Visible = False
            '
            'DrawHorizontalLinesButton
            '
            Me.DrawHorizontalLinesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.DrawHorizontalLinesButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.draw_Horizontal_Table
            Me.DrawHorizontalLinesButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.DrawHorizontalLinesButton.Name = "DrawHorizontalLinesButton"
            Me.DrawHorizontalLinesButton.Size = New System.Drawing.Size(23, 24)
            Me.DrawHorizontalLinesButton.ToolTipText = "Dibujar linea horizontal"
            Me.DrawHorizontalLinesButton.Visible = False
            '
            'DeleteLineButton
            '
            Me.DeleteLineButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.DeleteLineButton.Image = CType(resources.GetObject("DeleteLineButton.Image"), System.Drawing.Image)
            Me.DeleteLineButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.DeleteLineButton.Name = "DeleteLineButton"
            Me.DeleteLineButton.Size = New System.Drawing.Size(23, 24)
            Me.DeleteLineButton.ToolTipText = "Eliminar Linea Anterior"
            Me.DeleteLineButton.Visible = False
            '
            'StartOCRButton
            '
            Me.StartOCRButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.StartOCRButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.play
            Me.StartOCRButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.StartOCRButton.Name = "StartOCRButton"
            Me.StartOCRButton.Size = New System.Drawing.Size(23, 24)
            Me.StartOCRButton.ToolTipText = "Iniciar OCR"
            Me.StartOCRButton.Visible = False
            '
            'labelTableCoordinates
            '
            Me.labelTableCoordinates.Name = "labelTableCoordinates"
            Me.labelTableCoordinates.Size = New System.Drawing.Size(0, 24)
            Me.labelTableCoordinates.Visible = False
            '
            'ToolStripSeparator7
            '
            Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
            Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 27)
            Me.ToolStripSeparator7.Visible = False
            '
            'ProgresoToolStripProgressBar
            '
            Me.ProgresoToolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ProgresoToolStripProgressBar.AutoToolTip = True
            Me.ProgresoToolStripProgressBar.Name = "ProgresoToolStripProgressBar"
            Me.ProgresoToolStripProgressBar.Size = New System.Drawing.Size(100, 18)
            '
            'MensajeToolStripStatusLabel
            '
            Me.MensajeToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
            Me.MensajeToolStripStatusLabel.ForeColor = System.Drawing.Color.Maroon
            Me.MensajeToolStripStatusLabel.Name = "MensajeToolStripStatusLabel"
            Me.MensajeToolStripStatusLabel.Size = New System.Drawing.Size(51, 19)
            Me.MensajeToolStripStatusLabel.Text = "Mensaje"
            '
            'AvanceToolStripStatusLabel
            '
            Me.AvanceToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
            Me.AvanceToolStripStatusLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.AvanceToolStripStatusLabel.Name = "AvanceToolStripStatusLabel"
            Me.AvanceToolStripStatusLabel.Size = New System.Drawing.Size(48, 19)
            Me.AvanceToolStripStatusLabel.Text = "Avance"
            '
            'OpcionesGroupBox
            '
            Me.OpcionesGroupBox.Controls.Add(Me.BreakButton)
            Me.OpcionesGroupBox.Controls.Add(Me.EsquemaComboBox)
            Me.OpcionesGroupBox.Controls.Add(Me.ExitButton)
            Me.OpcionesGroupBox.Controls.Add(Me.TipoDocumentalComboBox)
            Me.OpcionesGroupBox.Controls.Add(Me.SaveButton)
            Me.OpcionesGroupBox.Controls.Add(Me.ReprocesoButton)
            Me.OpcionesGroupBox.Controls.Add(Me.DesIndexarFolioButton)
            Me.OpcionesGroupBox.Controls.Add(Me.NewFileButton)
            Me.OpcionesGroupBox.Controls.Add(Me.NewFolderButton)
            Me.OpcionesGroupBox.Controls.Add(Me.AddFolioButton)
            Me.OpcionesGroupBox.Controls.Add(Me.DeleteFolioButton)
            Me.OpcionesGroupBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.OpcionesGroupBox.Location = New System.Drawing.Point(5, 5)
            Me.OpcionesGroupBox.Name = "OpcionesGroupBox"
            Me.OpcionesGroupBox.Size = New System.Drawing.Size(344, 144)
            Me.OpcionesGroupBox.TabIndex = 0
            Me.OpcionesGroupBox.TabStop = False
            Me.OpcionesGroupBox.Text = "Opciones"
            '
            'BreakButton
            '
            Me.BreakButton.Location = New System.Drawing.Point(10, 60)
            Me.BreakButton.Name = "BreakButton"
            Me.BreakButton.Size = New System.Drawing.Size(324, 12)
            Me.BreakButton.TabIndex = 0
            Me.BreakButton.UseVisualStyleBackColor = True
            '
            'OpcionesPanel
            '
            Me.OpcionesPanel.Controls.Add(Me.TotalSplitContainer)
            Me.OpcionesPanel.Controls.Add(Me.InformacionGroupBox)
            Me.OpcionesPanel.Controls.Add(Me.OpcionesGroupBox)
            Me.OpcionesPanel.Dock = System.Windows.Forms.DockStyle.Right
            Me.OpcionesPanel.Location = New System.Drawing.Point(630, 0)
            Me.OpcionesPanel.Name = "OpcionesPanel"
            Me.OpcionesPanel.Padding = New System.Windows.Forms.Padding(5)
            Me.OpcionesPanel.Size = New System.Drawing.Size(354, 568)
            Me.OpcionesPanel.TabIndex = 0
            '
            'TotalSplitContainer
            '
            Me.TotalSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TotalSplitContainer.Location = New System.Drawing.Point(5, 243)
            Me.TotalSplitContainer.Name = "TotalSplitContainer"
            Me.TotalSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'TotalSplitContainer.Panel1
            '
            Me.TotalSplitContainer.Panel1.Controls.Add(Me.CapturaSplitContainer)
            '
            'TotalSplitContainer.Panel2
            '
            Me.TotalSplitContainer.Panel2.Controls.Add(Me.ToolTipLabel)
            Me.TotalSplitContainer.Size = New System.Drawing.Size(344, 320)
            Me.TotalSplitContainer.SplitterDistance = 290
            Me.TotalSplitContainer.TabIndex = 4
            '
            'CapturaSplitContainer
            '
            Me.CapturaSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CapturaSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
            Me.CapturaSplitContainer.Location = New System.Drawing.Point(0, 0)
            Me.CapturaSplitContainer.Name = "CapturaSplitContainer"
            Me.CapturaSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'CapturaSplitContainer.Panel1
            '
            Me.CapturaSplitContainer.Panel1.Controls.Add(Me.CamposSplitContainer)
            Me.CapturaSplitContainer.Panel1MinSize = 160
            '
            'CapturaSplitContainer.Panel2
            '
            Me.CapturaSplitContainer.Panel2.Controls.Add(Me.ValidacionesGroupBox)
            Me.CapturaSplitContainer.Panel2.Controls.Add(Me.ValidacionListasGroupBox)
            Me.CapturaSplitContainer.Panel2MinSize = 110
            Me.CapturaSplitContainer.Size = New System.Drawing.Size(344, 290)
            Me.CapturaSplitContainer.SplitterDistance = 160
            Me.CapturaSplitContainer.TabIndex = 2
            '
            'CamposSplitContainer
            '
            Me.CamposSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CamposSplitContainer.Location = New System.Drawing.Point(0, 0)
            Me.CamposSplitContainer.MinimumSize = New System.Drawing.Size(0, 130)
            Me.CamposSplitContainer.Name = "CamposSplitContainer"
            Me.CamposSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'CamposSplitContainer.Panel1
            '
            Me.CamposSplitContainer.Panel1.Controls.Add(Me.CamposGroupBox)
            Me.CamposSplitContainer.Panel1MinSize = 65
            '
            'CamposSplitContainer.Panel2
            '
            Me.CamposSplitContainer.Panel2.Controls.Add(Me.CamposLlaveGroupBox)
            Me.CamposSplitContainer.Panel2MinSize = 60
            Me.CamposSplitContainer.Size = New System.Drawing.Size(344, 160)
            Me.CamposSplitContainer.SplitterDistance = 81
            Me.CamposSplitContainer.TabIndex = 4
            '
            'CamposGroupBox
            '
            Me.CamposGroupBox.Controls.Add(Me.CamposPanel)
            Me.CamposGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CamposGroupBox.Location = New System.Drawing.Point(0, 0)
            Me.CamposGroupBox.Name = "CamposGroupBox"
            Me.CamposGroupBox.Padding = New System.Windows.Forms.Padding(10, 5, 10, 10)
            Me.CamposGroupBox.Size = New System.Drawing.Size(344, 81)
            Me.CamposGroupBox.TabIndex = 3
            Me.CamposGroupBox.TabStop = False
            Me.CamposGroupBox.Text = "Campos"
            '
            'CamposPanel
            '
            Me.CamposPanel.AutoScroll = True
            Me.CamposPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.CamposPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CamposPanel.Location = New System.Drawing.Point(10, 18)
            Me.CamposPanel.Name = "CamposPanel"
            Me.CamposPanel.Size = New System.Drawing.Size(324, 53)
            Me.CamposPanel.TabIndex = 0
            '
            'CamposLlaveGroupBox
            '
            Me.CamposLlaveGroupBox.Controls.Add(Me.CamposLlavePanel)
            Me.CamposLlaveGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CamposLlaveGroupBox.Location = New System.Drawing.Point(0, 0)
            Me.CamposLlaveGroupBox.Name = "CamposLlaveGroupBox"
            Me.CamposLlaveGroupBox.Padding = New System.Windows.Forms.Padding(10, 5, 10, 10)
            Me.CamposLlaveGroupBox.Size = New System.Drawing.Size(344, 75)
            Me.CamposLlaveGroupBox.TabIndex = 4
            Me.CamposLlaveGroupBox.TabStop = False
            Me.CamposLlaveGroupBox.Text = "Campos Carpeta"
            '
            'CamposLlavePanel
            '
            Me.CamposLlavePanel.AutoScroll = True
            Me.CamposLlavePanel.AutoSize = True
            Me.CamposLlavePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.CamposLlavePanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CamposLlavePanel.Location = New System.Drawing.Point(10, 18)
            Me.CamposLlavePanel.Name = "CamposLlavePanel"
            Me.CamposLlavePanel.Size = New System.Drawing.Size(324, 47)
            Me.CamposLlavePanel.TabIndex = 0
            '
            'ValidacionesGroupBox
            '
            Me.ValidacionesGroupBox.Controls.Add(Me.ValidacionesPanel)
            Me.ValidacionesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValidacionesGroupBox.Location = New System.Drawing.Point(0, 0)
            Me.ValidacionesGroupBox.Name = "ValidacionesGroupBox"
            Me.ValidacionesGroupBox.Padding = New System.Windows.Forms.Padding(10, 5, 10, 10)
            Me.ValidacionesGroupBox.Size = New System.Drawing.Size(344, 126)
            Me.ValidacionesGroupBox.TabIndex = 3
            Me.ValidacionesGroupBox.TabStop = False
            Me.ValidacionesGroupBox.Text = "Validaciones"
            '
            'ValidacionesPanel
            '
            Me.ValidacionesPanel.AutoScroll = True
            Me.ValidacionesPanel.AutoSize = True
            Me.ValidacionesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ValidacionesPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValidacionesPanel.Location = New System.Drawing.Point(10, 18)
            Me.ValidacionesPanel.Name = "ValidacionesPanel"
            Me.ValidacionesPanel.Size = New System.Drawing.Size(324, 98)
            Me.ValidacionesPanel.TabIndex = 0
            '
            'ValidacionListasGroupBox
            '
            Me.ValidacionListasGroupBox.Controls.Add(Me.ValidacionListasPanel)
            Me.ValidacionListasGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValidacionListasGroupBox.Location = New System.Drawing.Point(0, 0)
            Me.ValidacionListasGroupBox.Name = "ValidacionListasGroupBox"
            Me.ValidacionListasGroupBox.Padding = New System.Windows.Forms.Padding(10, 5, 10, 10)
            Me.ValidacionListasGroupBox.Size = New System.Drawing.Size(344, 126)
            Me.ValidacionListasGroupBox.TabIndex = 3
            Me.ValidacionListasGroupBox.TabStop = False
            Me.ValidacionListasGroupBox.Text = "Validacion Listas"
            '
            'ValidacionListasPanel
            '
            Me.ValidacionListasPanel.AutoScroll = True
            Me.ValidacionListasPanel.AutoSize = True
            Me.ValidacionListasPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ValidacionListasPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValidacionListasPanel.Location = New System.Drawing.Point(10, 18)
            Me.ValidacionListasPanel.Name = "ValidacionListasPanel"
            Me.ValidacionListasPanel.Size = New System.Drawing.Size(324, 98)
            Me.ValidacionListasPanel.TabIndex = 0
            '
            'ToolTipLabel
            '
            Me.ToolTipLabel.BackColor = System.Drawing.Color.LemonChiffon
            Me.ToolTipLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ToolTipLabel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ToolTipLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ToolTipLabel.Location = New System.Drawing.Point(0, 0)
            Me.ToolTipLabel.Name = "ToolTipLabel"
            Me.ToolTipLabel.Size = New System.Drawing.Size(344, 26)
            Me.ToolTipLabel.TabIndex = 3
            Me.ToolTipLabel.Text = "-"
            '
            'InformacionGroupBox
            '
            Me.InformacionGroupBox.Controls.Add(Me.InformacionTextBox)
            Me.InformacionGroupBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.InformacionGroupBox.Location = New System.Drawing.Point(5, 149)
            Me.InformacionGroupBox.Name = "InformacionGroupBox"
            Me.InformacionGroupBox.Padding = New System.Windows.Forms.Padding(10)
            Me.InformacionGroupBox.Size = New System.Drawing.Size(344, 94)
            Me.InformacionGroupBox.TabIndex = 1
            Me.InformacionGroupBox.TabStop = False
            Me.InformacionGroupBox.Text = "Información"
            '
            'InformacionTextBox
            '
            Me.InformacionTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.InformacionTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.InformacionTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.InformacionTextBox.Location = New System.Drawing.Point(10, 23)
            Me.InformacionTextBox.Multiline = True
            Me.InformacionTextBox.Name = "InformacionTextBox"
            Me.InformacionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.InformacionTextBox.Size = New System.Drawing.Size(324, 61)
            Me.InformacionTextBox.TabIndex = 0
            Me.InformacionTextBox.TabStop = False
            '
            'Folder_1Panel
            '
            Me.Folder_1Panel.AutoScroll = True
            Me.Folder_1Panel.BackColor = System.Drawing.Color.Khaki
            Me.Folder_1Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Folder_1Panel.Controls.Add(Me.EtiquetaFolder_1Label)
            Me.Folder_1Panel.Controls.Add(Me.DocumentFileo_2Panel)
            Me.Folder_1Panel.Controls.Add(Me.DocumentFileo_1Panel)
            Me.Folder_1Panel.Location = New System.Drawing.Point(3, 3)
            Me.Folder_1Panel.Name = "Folder_1Panel"
            Me.Folder_1Panel.Size = New System.Drawing.Size(284, 74)
            Me.Folder_1Panel.TabIndex = 5
            '
            'EtiquetaFolder_1Label
            '
            Me.EtiquetaFolder_1Label.BackColor = System.Drawing.Color.SteelBlue
            Me.EtiquetaFolder_1Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.EtiquetaFolder_1Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EtiquetaFolder_1Label.ForeColor = System.Drawing.Color.White
            Me.EtiquetaFolder_1Label.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.folder_image
            Me.EtiquetaFolder_1Label.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.EtiquetaFolder_1Label.Location = New System.Drawing.Point(3, 3)
            Me.EtiquetaFolder_1Label.Margin = New System.Windows.Forms.Padding(2)
            Me.EtiquetaFolder_1Label.Name = "EtiquetaFolder_1Label"
            Me.EtiquetaFolder_1Label.Size = New System.Drawing.Size(25, 66)
            Me.EtiquetaFolder_1Label.TabIndex = 4
            Me.EtiquetaFolder_1Label.Text = "1"
            Me.EtiquetaFolder_1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'DocumentFileo_1Panel
            '
            Me.DocumentFileo_1Panel.BackColor = System.Drawing.SystemColors.Control
            Me.DocumentFileo_1Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.DocumentFileo_1Panel.Controls.Add(Me.Folio_1_3PictureBox)
            Me.DocumentFileo_1Panel.Controls.Add(Me.Folio_1_2PictureBox)
            Me.DocumentFileo_1Panel.Controls.Add(Me.Folio_1_1PictureBox)
            Me.DocumentFileo_1Panel.Location = New System.Drawing.Point(34, 3)
            Me.DocumentFileo_1Panel.Name = "DocumentFileo_1Panel"
            Me.DocumentFileo_1Panel.Size = New System.Drawing.Size(141, 66)
            Me.DocumentFileo_1Panel.TabIndex = 1
            '
            'Folio_1_3PictureBox
            '
            Me.Folio_1_3PictureBox.BackColor = System.Drawing.Color.White
            Me.Folio_1_3PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Folio_1_3PictureBox.Location = New System.Drawing.Point(95, 3)
            Me.Folio_1_3PictureBox.Name = "Folio_1_3PictureBox"
            Me.Folio_1_3PictureBox.Size = New System.Drawing.Size(40, 58)
            Me.Folio_1_3PictureBox.TabIndex = 4
            Me.Folio_1_3PictureBox.TabStop = False
            '
            'Folio_1_2PictureBox
            '
            Me.Folio_1_2PictureBox.BackColor = System.Drawing.Color.White
            Me.Folio_1_2PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Folio_1_2PictureBox.Location = New System.Drawing.Point(49, 3)
            Me.Folio_1_2PictureBox.Name = "Folio_1_2PictureBox"
            Me.Folio_1_2PictureBox.Size = New System.Drawing.Size(40, 58)
            Me.Folio_1_2PictureBox.TabIndex = 3
            Me.Folio_1_2PictureBox.TabStop = False
            '
            'Folio_1_1PictureBox
            '
            Me.Folio_1_1PictureBox.BackColor = System.Drawing.Color.White
            Me.Folio_1_1PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Folio_1_1PictureBox.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Folio_1_1PictureBox.Location = New System.Drawing.Point(3, 3)
            Me.Folio_1_1PictureBox.Name = "Folio_1_1PictureBox"
            Me.Folio_1_1PictureBox.Size = New System.Drawing.Size(40, 58)
            Me.Folio_1_1PictureBox.TabIndex = 2
            Me.Folio_1_1PictureBox.TabStop = False
            '
            'ContadorToolStripStatusLabel
            '
            Me.ContadorToolStripStatusLabel.Name = "ContadorToolStripStatusLabel"
            Me.ContadorToolStripStatusLabel.Size = New System.Drawing.Size(67, 19)
            Me.ContadorToolStripStatusLabel.Text = "Paquetes: 1"
            '
            'BackThumbnailPanel
            '
            Me.BackThumbnailPanel.AutoScroll = True
            Me.BackThumbnailPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark
            Me.BackThumbnailPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.BackThumbnailPanel.Controls.Add(Me.ThumbnailFlowLayoutPanel)
            Me.BackThumbnailPanel.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.BackThumbnailPanel.Location = New System.Drawing.Point(0, 568)
            Me.BackThumbnailPanel.Margin = New System.Windows.Forms.Padding(0)
            Me.BackThumbnailPanel.Name = "BackThumbnailPanel"
            Me.BackThumbnailPanel.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
            Me.BackThumbnailPanel.Size = New System.Drawing.Size(984, 100)
            Me.BackThumbnailPanel.TabIndex = 21
            Me.BackThumbnailPanel.Visible = False
            '
            'ThumbnailFlowLayoutPanel
            '
            Me.ThumbnailFlowLayoutPanel.AutoSize = True
            Me.ThumbnailFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.ThumbnailFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ThumbnailFlowLayoutPanel.Controls.Add(Me.Folder_1Panel)
            Me.ThumbnailFlowLayoutPanel.Location = New System.Drawing.Point(10, 0)
            Me.ThumbnailFlowLayoutPanel.Margin = New System.Windows.Forms.Padding(0)
            Me.ThumbnailFlowLayoutPanel.Name = "ThumbnailFlowLayoutPanel"
            Me.ThumbnailFlowLayoutPanel.Size = New System.Drawing.Size(292, 82)
            Me.ThumbnailFlowLayoutPanel.TabIndex = 1
            '
            'EstadoStatusStrip
            '
            Me.EstadoStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AvanceToolStripStatusLabel, Me.MensajeToolStripStatusLabel, Me.ProgresoToolStripProgressBar, Me.ContadorToolStripStatusLabel, Me.VersionToolStripStatusLabel})
            Me.EstadoStatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
            Me.EstadoStatusStrip.Location = New System.Drawing.Point(0, 668)
            Me.EstadoStatusStrip.Name = "EstadoStatusStrip"
            Me.EstadoStatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
            Me.EstadoStatusStrip.Size = New System.Drawing.Size(984, 24)
            Me.EstadoStatusStrip.TabIndex = 23
            Me.EstadoStatusStrip.Text = "StatusStrip1"
            '
            'VersionToolStripStatusLabel
            '
            Me.VersionToolStripStatusLabel.Name = "VersionToolStripStatusLabel"
            Me.VersionToolStripStatusLabel.Size = New System.Drawing.Size(40, 19)
            Me.VersionToolStripStatusLabel.Text = "1.0.0.3"
            '
            'IndexerTablePanel
            '
            Me.IndexerTablePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.IndexerTablePanel.Controls.Add(Me.CapturaIndexerTable)
            Me.IndexerTablePanel.Controls.Add(Me.Table_UpPanel)
            Me.IndexerTablePanel.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.IndexerTablePanel.Location = New System.Drawing.Point(0, 336)
            Me.IndexerTablePanel.Name = "IndexerTablePanel"
            Me.IndexerTablePanel.Size = New System.Drawing.Size(630, 232)
            Me.IndexerTablePanel.TabIndex = 27
            Me.IndexerTablePanel.Visible = False
            '
            'CapturaIndexerTable
            '
            Me.CapturaIndexerTable.AllowAddItemRow = False
            Me.CapturaIndexerTable.AllowAddRow = False
            Me.CapturaIndexerTable.AllowDeleteRow = False
            Me.CapturaIndexerTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.CapturaIndexerTable.ColumHeaderHeight = 0
            Me.CapturaIndexerTable.ControlRegistros = 0
            Me.CapturaIndexerTable.ControlValor = 0R
            Me.CapturaIndexerTable.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CapturaIndexerTable.hasValuesRowsTable = False
            Me.CapturaIndexerTable.IsCalidad = False
            Me.CapturaIndexerTable.Location = New System.Drawing.Point(0, 23)
            Me.CapturaIndexerTable.Margin = New System.Windows.Forms.Padding(0)
            Me.CapturaIndexerTable.MaxRegistros = CType(0, Short)
            Me.CapturaIndexerTable.MinRegistros = CType(0, Short)
            Me.CapturaIndexerTable.Name = "CapturaIndexerTable"
            Me.CapturaIndexerTable.PageNumber = 0
            Me.CapturaIndexerTable.RowHeaderWidth = 50
            Me.CapturaIndexerTable.ShowControlRegistros = False
            Me.CapturaIndexerTable.ShowControlValor = False
            Me.CapturaIndexerTable.ShowSecondControls = False
            Me.CapturaIndexerTable.Size = New System.Drawing.Size(628, 207)
            Me.CapturaIndexerTable.TabIndex = 2
            Me.CapturaIndexerTable.tableItemRowOCR = False
            '
            'DocumentsTabControl
            '
            Me.DocumentsTabControl.Controls.Add(Me.DocumentMainTabPage)
            Me.DocumentsTabControl.Controls.Add(Me.DocumentConfigTabPage)
            Me.DocumentsTabControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DocumentsTabControl.HotTrack = True
            Me.DocumentsTabControl.ImageList = Me.MainImageList
            Me.DocumentsTabControl.Location = New System.Drawing.Point(0, 0)
            Me.DocumentsTabControl.Name = "DocumentsTabControl"
            Me.DocumentsTabControl.SelectedIndex = 0
            Me.DocumentsTabControl.Size = New System.Drawing.Size(630, 336)
            Me.DocumentsTabControl.TabIndex = 28
            Me.DocumentsTabControl.TabStop = False
            '
            'DocumentMainTabPage
            '
            Me.DocumentMainTabPage.Controls.Add(Me.ImageEditorPanel)
            Me.DocumentMainTabPage.ImageIndex = 1
            Me.DocumentMainTabPage.Location = New System.Drawing.Point(4, 23)
            Me.DocumentMainTabPage.Name = "DocumentMainTabPage"
            Me.DocumentMainTabPage.Padding = New System.Windows.Forms.Padding(3)
            Me.DocumentMainTabPage.Size = New System.Drawing.Size(622, 309)
            Me.DocumentMainTabPage.TabIndex = 0
            Me.DocumentMainTabPage.Text = "Principal"
            Me.DocumentMainTabPage.UseVisualStyleBackColor = True
            '
            'DocumentConfigTabPage
            '
            Me.DocumentConfigTabPage.Controls.Add(Me.DocumentsCheckedListBox)
            Me.DocumentConfigTabPage.Controls.Add(Me.VisualizarSeleccionadosButton)
            Me.DocumentConfigTabPage.ImageIndex = 2
            Me.DocumentConfigTabPage.Location = New System.Drawing.Point(4, 23)
            Me.DocumentConfigTabPage.Name = "DocumentConfigTabPage"
            Me.DocumentConfigTabPage.Padding = New System.Windows.Forms.Padding(10)
            Me.DocumentConfigTabPage.Size = New System.Drawing.Size(622, 309)
            Me.DocumentConfigTabPage.TabIndex = 1
            Me.DocumentConfigTabPage.Text = "Visualizar"
            Me.DocumentConfigTabPage.UseVisualStyleBackColor = True
            '
            'DocumentsCheckedListBox
            '
            Me.DocumentsCheckedListBox.CheckOnClick = True
            Me.DocumentsCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DocumentsCheckedListBox.FormattingEnabled = True
            Me.DocumentsCheckedListBox.Location = New System.Drawing.Point(10, 10)
            Me.DocumentsCheckedListBox.Name = "DocumentsCheckedListBox"
            Me.DocumentsCheckedListBox.Size = New System.Drawing.Size(602, 256)
            Me.DocumentsCheckedListBox.TabIndex = 0
            '
            'VisualizarSeleccionadosButton
            '
            Me.VisualizarSeleccionadosButton.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.VisualizarSeleccionadosButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.VisualizarSeleccionadosButton.ImageIndex = 2
            Me.VisualizarSeleccionadosButton.ImageList = Me.MainImageList
            Me.VisualizarSeleccionadosButton.Location = New System.Drawing.Point(10, 266)
            Me.VisualizarSeleccionadosButton.Name = "VisualizarSeleccionadosButton"
            Me.VisualizarSeleccionadosButton.Size = New System.Drawing.Size(602, 33)
            Me.VisualizarSeleccionadosButton.TabIndex = 1
            Me.VisualizarSeleccionadosButton.Text = "Visualizar documentos seleccionados - [Ctrl] + [F3]"
            Me.VisualizarSeleccionadosButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
            Me.VisualizarSeleccionadosButton.UseVisualStyleBackColor = True
            '
            'MainImageList
            '
            Me.MainImageList.ImageStream = CType(resources.GetObject("MainImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.MainImageList.TransparentColor = System.Drawing.Color.Transparent
            Me.MainImageList.Images.SetKeyName(0, "page_white_code.png")
            Me.MainImageList.Images.SetKeyName(1, "page_white_key.png")
            Me.MainImageList.Images.SetKeyName(2, "text_list_numbers.png")
            '
            'TipoDocumentoComboBoxControl
            '
            Me.TipoDocumentoComboBoxControl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TipoDocumentoComboBoxControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.TipoDocumentoComboBoxControl.Location = New System.Drawing.Point(408, 107)
            Me.TipoDocumentoComboBoxControl.Name = "TipoDocumentoComboBoxControl"
            Me.TipoDocumentoComboBoxControl.Size = New System.Drawing.Size(545, 231)
            Me.TipoDocumentoComboBoxControl.TabIndex = 25
            Me.TipoDocumentoComboBoxControl.Visible = False
            '
            'FormIndexerView
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(984, 692)
            Me.ControlBox = False
            Me.Controls.Add(Me.TipoDocumentoComboBoxControl)
            Me.Controls.Add(Me.DocumentsTabControl)
            Me.Controls.Add(Me.ImageFlotantePanel)
            Me.Controls.Add(Me.IndexerTablePanel)
            Me.Controls.Add(Me.OpcionesPanel)
            Me.Controls.Add(Me.BackThumbnailPanel)
            Me.Controls.Add(Me.EstadoStatusStrip)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.KeyPreview = True
            Me.Name = "FormIndexerView"
            Me.Text = "Indexar"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            Me.Table_UpPanel.ResumeLayout(False)
            Me.Table_UpPanel.PerformLayout()
            Me.DocumentFileo_2Panel.ResumeLayout(False)
            CType(Me.Folio_2_2PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.Folio_2_1PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ImageFlotantePanel.ResumeLayout(False)
            CType(Me.ImageFlotantePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ImageEditorPanel.ResumeLayout(False)
            Me.ImageEditorPanel.PerformLayout()
            Me.MarcoDibujoPanel.ResumeLayout(False)
            Me.ImagePanel.ResumeLayout(False)
            CType(Me.ImagePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ImageToolStrip.ResumeLayout(False)
            Me.ImageToolStrip.PerformLayout()
            Me.OpcionesGroupBox.ResumeLayout(False)
            Me.OpcionesPanel.ResumeLayout(False)
            Me.TotalSplitContainer.Panel1.ResumeLayout(False)
            Me.TotalSplitContainer.Panel2.ResumeLayout(False)
            CType(Me.TotalSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TotalSplitContainer.ResumeLayout(False)
            Me.CapturaSplitContainer.Panel1.ResumeLayout(False)
            Me.CapturaSplitContainer.Panel2.ResumeLayout(False)
            CType(Me.CapturaSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.CapturaSplitContainer.ResumeLayout(False)
            Me.CamposSplitContainer.Panel1.ResumeLayout(False)
            Me.CamposSplitContainer.Panel2.ResumeLayout(False)
            CType(Me.CamposSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.CamposSplitContainer.ResumeLayout(False)
            Me.CamposGroupBox.ResumeLayout(False)
            Me.CamposLlaveGroupBox.ResumeLayout(False)
            Me.CamposLlaveGroupBox.PerformLayout()
            Me.ValidacionesGroupBox.ResumeLayout(False)
            Me.ValidacionesGroupBox.PerformLayout()
            Me.ValidacionListasGroupBox.ResumeLayout(False)
            Me.ValidacionListasGroupBox.PerformLayout()
            Me.InformacionGroupBox.ResumeLayout(False)
            Me.InformacionGroupBox.PerformLayout()
            Me.Folder_1Panel.ResumeLayout(False)
            Me.DocumentFileo_1Panel.ResumeLayout(False)
            CType(Me.Folio_1_3PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.Folio_1_2PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.Folio_1_1PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.BackThumbnailPanel.ResumeLayout(False)
            Me.BackThumbnailPanel.PerformLayout()
            Me.ThumbnailFlowLayoutPanel.ResumeLayout(False)
            Me.EstadoStatusStrip.ResumeLayout(False)
            Me.EstadoStatusStrip.PerformLayout()
            Me.IndexerTablePanel.ResumeLayout(False)
            Me.DocumentsTabControl.ResumeLayout(False)
            Me.DocumentMainTabPage.ResumeLayout(False)
            Me.DocumentConfigTabPage.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ToolTipText As System.Windows.Forms.ToolTip
        Friend WithEvents NewFolderButton As System.Windows.Forms.Button
        Friend WithEvents PreviousFolioButton As System.Windows.Forms.Button
        Friend WithEvents NextFolioButton As System.Windows.Forms.Button
        Friend WithEvents NewFileButton As System.Windows.Forms.Button
        Friend WithEvents ExitButton As System.Windows.Forms.Button
        Friend WithEvents TipoDocumentalComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents DocumentFileo_2Panel As System.Windows.Forms.Panel
        Friend WithEvents Folio_2_2PictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents Folio_2_1PictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents ImageFlotantePanel As System.Windows.Forms.Panel
        Friend WithEvents ImageFlotantePictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents ImageEditorPanel As System.Windows.Forms.Panel
        Friend WithEvents MarcoDibujoPanel As System.Windows.Forms.Panel
        Friend WithEvents ImagePanel As System.Windows.Forms.Panel
        Public WithEvents ImagePictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents ImageToolStrip As System.Windows.Forms.ToolStrip
        Friend WithEvents AjustarAltoToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents AjustarAnchoToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents ZoomOutToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ZoomToolStripComboBox As System.Windows.Forms.ToolStripComboBox
        Friend WithEvents ZoomInToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents RotateLeftToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents RotateRightToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents FlipHorizontalToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents FlipVerticalToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents ShowAccesosToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents ShowThumbnailToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ProgresoToolStripProgressBar As System.Windows.Forms.ToolStripProgressBar
        Friend WithEvents MensajeToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents AvanceToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
        Public WithEvents OpcionesGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents Folder_1Panel As System.Windows.Forms.Panel
        Friend WithEvents EtiquetaFolder_1Label As System.Windows.Forms.Label
        Friend WithEvents DocumentFileo_1Panel As System.Windows.Forms.Panel
        Friend WithEvents Folio_1_3PictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents Folio_1_2PictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents Folio_1_1PictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents InformacionGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents InformacionTextBox As System.Windows.Forms.TextBox
        Friend WithEvents ContadorToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents BackThumbnailPanel As System.Windows.Forms.Panel
        Friend WithEvents ThumbnailFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents EstadoStatusStrip As System.Windows.Forms.StatusStrip
        Friend WithEvents DeleteFolioButton As System.Windows.Forms.Button
        Friend WithEvents AddFolioButton As System.Windows.Forms.Button
        Friend WithEvents SaveButton As System.Windows.Forms.Button
        Friend WithEvents CapturaSplitContainer As System.Windows.Forms.SplitContainer
        Public WithEvents CamposPanel As System.Windows.Forms.Panel
        Public WithEvents CamposLlaveGroupBox As System.Windows.Forms.GroupBox
        Public WithEvents ValidacionListasGroupBox As System.Windows.Forms.GroupBox
        Public WithEvents ValidacionListasPanel As System.Windows.Forms.Panel
        Friend WithEvents CamposLlavePanel As System.Windows.Forms.Panel
        Friend WithEvents ToolTipLabel As System.Windows.Forms.Label
        Public WithEvents TotalSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents EsquemaComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents DesIndexarFolioButton As System.Windows.Forms.Button
        Friend WithEvents ReprocesoButton As System.Windows.Forms.Button
        Friend WithEvents TipoDocumentoComboBoxControl As ComboBoxControl
        Friend WithEvents BreakButton As System.Windows.Forms.Button
        Friend WithEvents IndexerTablePanel As System.Windows.Forms.Panel
        Friend WithEvents Table_UpPanel As System.Windows.Forms.Panel
        Friend WithEvents Table_CloseButton As System.Windows.Forms.Button
        Friend WithEvents CapturaIndexerTable As IndexerTable
        Friend WithEvents Table_SaveButton As System.Windows.Forms.Button
        Friend WithEvents AnexoTotalToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents AnexoFolderToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents AutoindexarToolStripSeparator As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents AutoIndexarToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents AnexosToolStripSeparator As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents AnexosToolStripLabel As System.Windows.Forms.ToolStripLabel
        Friend WithEvents SeñaladorRulerControl As RulerControl
        Friend WithEvents RulerToolStripSeparator As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents RulerToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents SubrayarToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents QuitarSubrayadoToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents DocumentsTabControl As System.Windows.Forms.TabControl
        Friend WithEvents DocumentMainTabPage As System.Windows.Forms.TabPage
        Friend WithEvents DocumentConfigTabPage As System.Windows.Forms.TabPage
        Friend WithEvents MainImageList As System.Windows.Forms.ImageList
        Friend WithEvents VisualizarSeleccionadosButton As System.Windows.Forms.Button
        Friend WithEvents DocumentsCheckedListBox As System.Windows.Forms.CheckedListBox
        Friend WithEvents VersionToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
        Public WithEvents CamposGroupBox As System.Windows.Forms.GroupBox
        Public WithEvents OpcionesPanel As System.Windows.Forms.Panel
        Friend WithEvents CamposSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents ValidacionesGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ValidacionesPanel As System.Windows.Forms.Panel
        Friend WithEvents DrawingTableButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents DrawVerticalLinesButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents DrawHorizontalLinesButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents labelTableCoordinates As System.Windows.Forms.ToolStripLabel
        Friend WithEvents DeleteLineButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents StartOCRButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
        Public WithEvents PageStatusLabel As System.Windows.Forms.Label
        Friend WithEvents DrawingRactangleButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents Table_ColumnOCRButton As System.Windows.Forms.Button
    End Class

End Namespace