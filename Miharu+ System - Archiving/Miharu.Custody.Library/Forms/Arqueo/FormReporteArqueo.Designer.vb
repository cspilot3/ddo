Namespace Forms.Arqueo
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormReporteArqueo
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormReporteArqueo))
            Me.InformacionStatusStrip = New System.Windows.Forms.StatusStrip()
            Me.InformacionStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.ComandosToolStrip = New System.Windows.Forms.ToolStrip()
            Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
            Me.ModoComboBox = New System.Windows.Forms.ToolStripComboBox()
            Me.ExportarToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.PrimarioSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.CajaSplit = New System.Windows.Forms.SplitContainer()
            Me.CajaButton = New System.Windows.Forms.Button()
            Me.CajaDataGridView = New System.Windows.Forms.DataGridView()
            Me.NombreBovedaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreBovedaSeccionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CodigoBovedaPosicionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CodigoCajaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreEntidadClienteDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CajaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.dsCustody = New DBCore.CustodyDataSet()
            Me.SecundarioSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.CarpetaSplit = New System.Windows.Forms.SplitContainer()
            Me.CarpetaButton = New System.Windows.Forms.Button()
            Me.CarpetaDataGridView = New System.Windows.Forms.DataGridView()
            Me.CodigoCajaDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarrasFolderDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ArqueadoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.SobranteDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.FaltanteDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.NombreProyectoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CarpetaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.DocumentoSplit = New System.Windows.Forms.SplitContainer()
            Me.DocumentoButton = New System.Windows.Forms.Button()
            Me.DocumentoDataGridView = New System.Windows.Forms.DataGridView()
            Me.CodigoCajaDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarrasFolderDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarrasFileDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Data_1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Data_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Data_3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ArqueadoDataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.SobranteDataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.FaltanteDataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.DocumentoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.InformacionStatusStrip.SuspendLayout()
            Me.ComandosToolStrip.SuspendLayout()
            CType(Me.PrimarioSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.PrimarioSplitContainer.Panel1.SuspendLayout()
            Me.PrimarioSplitContainer.Panel2.SuspendLayout()
            Me.PrimarioSplitContainer.SuspendLayout()
            CType(Me.CajaSplit, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.CajaSplit.Panel1.SuspendLayout()
            Me.CajaSplit.Panel2.SuspendLayout()
            Me.CajaSplit.SuspendLayout()
            CType(Me.CajaDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CajaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dsCustody, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.SecundarioSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SecundarioSplitContainer.Panel1.SuspendLayout()
            Me.SecundarioSplitContainer.Panel2.SuspendLayout()
            Me.SecundarioSplitContainer.SuspendLayout()
            CType(Me.CarpetaSplit, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.CarpetaSplit.Panel1.SuspendLayout()
            Me.CarpetaSplit.Panel2.SuspendLayout()
            Me.CarpetaSplit.SuspendLayout()
            CType(Me.CarpetaDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CarpetaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DocumentoSplit, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.DocumentoSplit.Panel1.SuspendLayout()
            Me.DocumentoSplit.Panel2.SuspendLayout()
            Me.DocumentoSplit.SuspendLayout()
            CType(Me.DocumentoDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DocumentoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'InformacionStatusStrip
            '
            Me.InformacionStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InformacionStripStatusLabel})
            Me.InformacionStatusStrip.Location = New System.Drawing.Point(0, 452)
            Me.InformacionStatusStrip.Name = "InformacionStatusStrip"
            Me.InformacionStatusStrip.Size = New System.Drawing.Size(859, 22)
            Me.InformacionStatusStrip.TabIndex = 0
            Me.InformacionStatusStrip.Text = "StatusStrip1"
            '
            'InformacionStripStatusLabel
            '
            Me.InformacionStripStatusLabel.Name = "InformacionStripStatusLabel"
            Me.InformacionStripStatusLabel.Size = New System.Drawing.Size(39, 17)
            Me.InformacionStripStatusLabel.Text = "Status"
            '
            'ComandosToolStrip
            '
            Me.ComandosToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.ComandosToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.ModoComboBox, Me.ExportarToolStripButton})
            Me.ComandosToolStrip.Location = New System.Drawing.Point(0, 0)
            Me.ComandosToolStrip.Name = "ComandosToolStrip"
            Me.ComandosToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
            Me.ComandosToolStrip.Size = New System.Drawing.Size(859, 25)
            Me.ComandosToolStrip.TabIndex = 1
            Me.ComandosToolStrip.Text = "Barra de Comandos"
            '
            'ToolStripLabel1
            '
            Me.ToolStripLabel1.Name = "ToolStripLabel1"
            Me.ToolStripLabel1.Size = New System.Drawing.Size(34, 22)
            Me.ToolStripLabel1.Text = "Nivel"
            '
            'ModoComboBox
            '
            Me.ModoComboBox.Name = "ModoComboBox"
            Me.ModoComboBox.Size = New System.Drawing.Size(121, 25)
            Me.ModoComboBox.ToolTipText = "Modo de Visualizacion"
            '
            'ExportarToolStripButton
            '
            Me.ExportarToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ExportarToolStripButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.document_export
            Me.ExportarToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ExportarToolStripButton.Name = "ExportarToolStripButton"
            Me.ExportarToolStripButton.Size = New System.Drawing.Size(70, 22)
            Me.ExportarToolStripButton.Text = "Exportar"
            Me.ExportarToolStripButton.ToolTipText = "Exporta los datos a Excel"
            '
            'PrimarioSplitContainer
            '
            Me.PrimarioSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.PrimarioSplitContainer.Location = New System.Drawing.Point(0, 25)
            Me.PrimarioSplitContainer.Name = "PrimarioSplitContainer"
            Me.PrimarioSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'PrimarioSplitContainer.Panel1
            '
            Me.PrimarioSplitContainer.Panel1.Controls.Add(Me.CajaSplit)
            Me.PrimarioSplitContainer.Panel1MinSize = 23
            '
            'PrimarioSplitContainer.Panel2
            '
            Me.PrimarioSplitContainer.Panel2.Controls.Add(Me.SecundarioSplitContainer)
            Me.PrimarioSplitContainer.Size = New System.Drawing.Size(859, 427)
            Me.PrimarioSplitContainer.SplitterDistance = 134
            Me.PrimarioSplitContainer.SplitterWidth = 1
            Me.PrimarioSplitContainer.TabIndex = 2
            '
            'CajaSplit
            '
            Me.CajaSplit.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CajaSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
            Me.CajaSplit.IsSplitterFixed = True
            Me.CajaSplit.Location = New System.Drawing.Point(0, 0)
            Me.CajaSplit.Name = "CajaSplit"
            Me.CajaSplit.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'CajaSplit.Panel1
            '
            Me.CajaSplit.Panel1.Controls.Add(Me.CajaButton)
            '
            'CajaSplit.Panel2
            '
            Me.CajaSplit.Panel2.Controls.Add(Me.CajaDataGridView)
            Me.CajaSplit.Size = New System.Drawing.Size(859, 134)
            Me.CajaSplit.SplitterDistance = 25
            Me.CajaSplit.SplitterWidth = 1
            Me.CajaSplit.TabIndex = 0
            '
            'CajaButton
            '
            Me.CajaButton.BackColor = System.Drawing.SystemColors.HotTrack
            Me.CajaButton.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CajaButton.FlatAppearance.BorderSize = 0
            Me.CajaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CajaButton.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CajaButton.ForeColor = System.Drawing.SystemColors.HighlightText
            Me.CajaButton.Location = New System.Drawing.Point(0, 0)
            Me.CajaButton.Name = "CajaButton"
            Me.CajaButton.Size = New System.Drawing.Size(859, 25)
            Me.CajaButton.TabIndex = 0
            Me.CajaButton.Text = "Caja"
            Me.CajaButton.UseVisualStyleBackColor = False
            '
            'CajaDataGridView
            '
            Me.CajaDataGridView.AllowUserToOrderColumns = True
            DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.CajaDataGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
            Me.CajaDataGridView.AutoGenerateColumns = False
            Me.CajaDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
            Me.CajaDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CajaDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NombreBovedaDataGridViewTextBoxColumn, Me.NombreBovedaSeccionDataGridViewTextBoxColumn, Me.CodigoBovedaPosicionDataGridViewTextBoxColumn, Me.CodigoCajaDataGridViewTextBoxColumn, Me.NombreEntidadClienteDataGridViewTextBoxColumn})
            Me.CajaDataGridView.DataSource = Me.CajaBindingSource
            Me.CajaDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CajaDataGridView.Location = New System.Drawing.Point(0, 0)
            Me.CajaDataGridView.Name = "CajaDataGridView"
            Me.CajaDataGridView.ReadOnly = True
            Me.CajaDataGridView.Size = New System.Drawing.Size(859, 108)
            Me.CajaDataGridView.TabIndex = 0
            '
            'NombreBovedaDataGridViewTextBoxColumn
            '
            Me.NombreBovedaDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Boveda"
            Me.NombreBovedaDataGridViewTextBoxColumn.HeaderText = "Boveda"
            Me.NombreBovedaDataGridViewTextBoxColumn.Name = "NombreBovedaDataGridViewTextBoxColumn"
            Me.NombreBovedaDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreBovedaDataGridViewTextBoxColumn.Width = 69
            '
            'NombreBovedaSeccionDataGridViewTextBoxColumn
            '
            Me.NombreBovedaSeccionDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Boveda_Seccion"
            Me.NombreBovedaSeccionDataGridViewTextBoxColumn.HeaderText = "Seccion"
            Me.NombreBovedaSeccionDataGridViewTextBoxColumn.Name = "NombreBovedaSeccionDataGridViewTextBoxColumn"
            Me.NombreBovedaSeccionDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreBovedaSeccionDataGridViewTextBoxColumn.Width = 71
            '
            'CodigoBovedaPosicionDataGridViewTextBoxColumn
            '
            Me.CodigoBovedaPosicionDataGridViewTextBoxColumn.DataPropertyName = "Codigo_Boveda_Posicion"
            Me.CodigoBovedaPosicionDataGridViewTextBoxColumn.HeaderText = "Codigo Posicion"
            Me.CodigoBovedaPosicionDataGridViewTextBoxColumn.Name = "CodigoBovedaPosicionDataGridViewTextBoxColumn"
            Me.CodigoBovedaPosicionDataGridViewTextBoxColumn.ReadOnly = True
            Me.CodigoBovedaPosicionDataGridViewTextBoxColumn.Width = 99
            '
            'CodigoCajaDataGridViewTextBoxColumn
            '
            Me.CodigoCajaDataGridViewTextBoxColumn.DataPropertyName = "Codigo_Caja"
            Me.CodigoCajaDataGridViewTextBoxColumn.HeaderText = "Caja"
            Me.CodigoCajaDataGridViewTextBoxColumn.Name = "CodigoCajaDataGridViewTextBoxColumn"
            Me.CodigoCajaDataGridViewTextBoxColumn.ReadOnly = True
            Me.CodigoCajaDataGridViewTextBoxColumn.Width = 53
            '
            'NombreEntidadClienteDataGridViewTextBoxColumn
            '
            Me.NombreEntidadClienteDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Entidad_Cliente"
            Me.NombreEntidadClienteDataGridViewTextBoxColumn.HeaderText = "Cliente"
            Me.NombreEntidadClienteDataGridViewTextBoxColumn.Name = "NombreEntidadClienteDataGridViewTextBoxColumn"
            Me.NombreEntidadClienteDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreEntidadClienteDataGridViewTextBoxColumn.Width = 64
            '
            'CajaBindingSource
            '
            Me.CajaBindingSource.AllowNew = False
            Me.CajaBindingSource.DataMember = "CTA_RPT_Arqueo_Posicion"
            Me.CajaBindingSource.DataSource = Me.dsCustody
            '
            'dsCustody
            '
            Me.dsCustody.DataSetName = "NewDataSet"
            '
            'SecundarioSplitContainer
            '
            Me.SecundarioSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SecundarioSplitContainer.Location = New System.Drawing.Point(0, 0)
            Me.SecundarioSplitContainer.Name = "SecundarioSplitContainer"
            Me.SecundarioSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SecundarioSplitContainer.Panel1
            '
            Me.SecundarioSplitContainer.Panel1.Controls.Add(Me.CarpetaSplit)
            Me.SecundarioSplitContainer.Panel1MinSize = 23
            '
            'SecundarioSplitContainer.Panel2
            '
            Me.SecundarioSplitContainer.Panel2.Controls.Add(Me.DocumentoSplit)
            Me.SecundarioSplitContainer.Size = New System.Drawing.Size(859, 292)
            Me.SecundarioSplitContainer.SplitterDistance = 141
            Me.SecundarioSplitContainer.SplitterWidth = 1
            Me.SecundarioSplitContainer.TabIndex = 0
            '
            'CarpetaSplit
            '
            Me.CarpetaSplit.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CarpetaSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
            Me.CarpetaSplit.IsSplitterFixed = True
            Me.CarpetaSplit.Location = New System.Drawing.Point(0, 0)
            Me.CarpetaSplit.Name = "CarpetaSplit"
            Me.CarpetaSplit.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'CarpetaSplit.Panel1
            '
            Me.CarpetaSplit.Panel1.Controls.Add(Me.CarpetaButton)
            '
            'CarpetaSplit.Panel2
            '
            Me.CarpetaSplit.Panel2.Controls.Add(Me.CarpetaDataGridView)
            Me.CarpetaSplit.Size = New System.Drawing.Size(859, 141)
            Me.CarpetaSplit.SplitterDistance = 25
            Me.CarpetaSplit.SplitterWidth = 1
            Me.CarpetaSplit.TabIndex = 0
            '
            'CarpetaButton
            '
            Me.CarpetaButton.BackColor = System.Drawing.SystemColors.HotTrack
            Me.CarpetaButton.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CarpetaButton.FlatAppearance.BorderSize = 0
            Me.CarpetaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CarpetaButton.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CarpetaButton.ForeColor = System.Drawing.SystemColors.HighlightText
            Me.CarpetaButton.Location = New System.Drawing.Point(0, 0)
            Me.CarpetaButton.Name = "CarpetaButton"
            Me.CarpetaButton.Size = New System.Drawing.Size(859, 25)
            Me.CarpetaButton.TabIndex = 0
            Me.CarpetaButton.Text = "Carpeta"
            Me.CarpetaButton.UseVisualStyleBackColor = False
            '
            'CarpetaDataGridView
            '
            Me.CarpetaDataGridView.AllowUserToOrderColumns = True
            DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.CarpetaDataGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
            Me.CarpetaDataGridView.AutoGenerateColumns = False
            Me.CarpetaDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
            Me.CarpetaDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CarpetaDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CodigoCajaDataGridViewTextBoxColumn1, Me.CBarrasFolderDataGridViewTextBoxColumn, Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.ArqueadoDataGridViewCheckBoxColumn, Me.SobranteDataGridViewCheckBoxColumn, Me.FaltanteDataGridViewCheckBoxColumn, Me.NombreProyectoDataGridViewTextBoxColumn})
            Me.CarpetaDataGridView.DataSource = Me.CarpetaBindingSource
            Me.CarpetaDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CarpetaDataGridView.Location = New System.Drawing.Point(0, 0)
            Me.CarpetaDataGridView.Name = "CarpetaDataGridView"
            Me.CarpetaDataGridView.ReadOnly = True
            Me.CarpetaDataGridView.Size = New System.Drawing.Size(859, 115)
            Me.CarpetaDataGridView.TabIndex = 0
            '
            'CodigoCajaDataGridViewTextBoxColumn1
            '
            Me.CodigoCajaDataGridViewTextBoxColumn1.DataPropertyName = "Codigo_Caja"
            Me.CodigoCajaDataGridViewTextBoxColumn1.HeaderText = "Caja"
            Me.CodigoCajaDataGridViewTextBoxColumn1.Name = "CodigoCajaDataGridViewTextBoxColumn1"
            Me.CodigoCajaDataGridViewTextBoxColumn1.ReadOnly = True
            Me.CodigoCajaDataGridViewTextBoxColumn1.Width = 53
            '
            'CBarrasFolderDataGridViewTextBoxColumn
            '
            Me.CBarrasFolderDataGridViewTextBoxColumn.DataPropertyName = "CBarras_Folder"
            Me.CBarrasFolderDataGridViewTextBoxColumn.HeaderText = "Codigo de Barras"
            Me.CBarrasFolderDataGridViewTextBoxColumn.Name = "CBarrasFolderDataGridViewTextBoxColumn"
            Me.CBarrasFolderDataGridViewTextBoxColumn.ReadOnly = True
            Me.CBarrasFolderDataGridViewTextBoxColumn.Width = 77
            '
            'DataGridViewTextBoxColumn1
            '
            Me.DataGridViewTextBoxColumn1.DataPropertyName = "Data_1"
            Me.DataGridViewTextBoxColumn1.HeaderText = ""
            Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
            Me.DataGridViewTextBoxColumn1.ReadOnly = True
            Me.DataGridViewTextBoxColumn1.Width = 19
            '
            'DataGridViewTextBoxColumn2
            '
            Me.DataGridViewTextBoxColumn2.DataPropertyName = "Data_2"
            Me.DataGridViewTextBoxColumn2.HeaderText = ""
            Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
            Me.DataGridViewTextBoxColumn2.ReadOnly = True
            Me.DataGridViewTextBoxColumn2.Width = 19
            '
            'DataGridViewTextBoxColumn3
            '
            Me.DataGridViewTextBoxColumn3.DataPropertyName = "Data_3"
            Me.DataGridViewTextBoxColumn3.HeaderText = ""
            Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
            Me.DataGridViewTextBoxColumn3.ReadOnly = True
            Me.DataGridViewTextBoxColumn3.Width = 19
            '
            'ArqueadoDataGridViewCheckBoxColumn
            '
            Me.ArqueadoDataGridViewCheckBoxColumn.DataPropertyName = "Arqueado"
            Me.ArqueadoDataGridViewCheckBoxColumn.HeaderText = "Arqueado"
            Me.ArqueadoDataGridViewCheckBoxColumn.Name = "ArqueadoDataGridViewCheckBoxColumn"
            Me.ArqueadoDataGridViewCheckBoxColumn.ReadOnly = True
            Me.ArqueadoDataGridViewCheckBoxColumn.Width = 59
            '
            'SobranteDataGridViewCheckBoxColumn
            '
            Me.SobranteDataGridViewCheckBoxColumn.DataPropertyName = "Sobrante"
            Me.SobranteDataGridViewCheckBoxColumn.HeaderText = "Sobrante"
            Me.SobranteDataGridViewCheckBoxColumn.Name = "SobranteDataGridViewCheckBoxColumn"
            Me.SobranteDataGridViewCheckBoxColumn.ReadOnly = True
            Me.SobranteDataGridViewCheckBoxColumn.Width = 56
            '
            'FaltanteDataGridViewCheckBoxColumn
            '
            Me.FaltanteDataGridViewCheckBoxColumn.DataPropertyName = "Faltante"
            Me.FaltanteDataGridViewCheckBoxColumn.HeaderText = "Faltante"
            Me.FaltanteDataGridViewCheckBoxColumn.Name = "FaltanteDataGridViewCheckBoxColumn"
            Me.FaltanteDataGridViewCheckBoxColumn.ReadOnly = True
            Me.FaltanteDataGridViewCheckBoxColumn.Width = 51
            '
            'NombreProyectoDataGridViewTextBoxColumn
            '
            Me.NombreProyectoDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Proyecto"
            Me.NombreProyectoDataGridViewTextBoxColumn.HeaderText = "Proyecto"
            Me.NombreProyectoDataGridViewTextBoxColumn.Name = "NombreProyectoDataGridViewTextBoxColumn"
            Me.NombreProyectoDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreProyectoDataGridViewTextBoxColumn.Width = 74
            '
            'CarpetaBindingSource
            '
            Me.CarpetaBindingSource.AllowNew = False
            Me.CarpetaBindingSource.DataMember = "CTA_RPT_Arqueo_Folder"
            Me.CarpetaBindingSource.DataSource = Me.dsCustody
            '
            'DocumentoSplit
            '
            Me.DocumentoSplit.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DocumentoSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
            Me.DocumentoSplit.IsSplitterFixed = True
            Me.DocumentoSplit.Location = New System.Drawing.Point(0, 0)
            Me.DocumentoSplit.Name = "DocumentoSplit"
            Me.DocumentoSplit.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'DocumentoSplit.Panel1
            '
            Me.DocumentoSplit.Panel1.Controls.Add(Me.DocumentoButton)
            '
            'DocumentoSplit.Panel2
            '
            Me.DocumentoSplit.Panel2.Controls.Add(Me.DocumentoDataGridView)
            Me.DocumentoSplit.Size = New System.Drawing.Size(859, 150)
            Me.DocumentoSplit.SplitterDistance = 25
            Me.DocumentoSplit.SplitterWidth = 1
            Me.DocumentoSplit.TabIndex = 0
            '
            'DocumentoButton
            '
            Me.DocumentoButton.BackColor = System.Drawing.SystemColors.HotTrack
            Me.DocumentoButton.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DocumentoButton.FlatAppearance.BorderSize = 0
            Me.DocumentoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.DocumentoButton.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DocumentoButton.ForeColor = System.Drawing.SystemColors.HighlightText
            Me.DocumentoButton.Location = New System.Drawing.Point(0, 0)
            Me.DocumentoButton.Name = "DocumentoButton"
            Me.DocumentoButton.Size = New System.Drawing.Size(859, 25)
            Me.DocumentoButton.TabIndex = 0
            Me.DocumentoButton.Text = "Documento"
            Me.DocumentoButton.UseVisualStyleBackColor = False
            '
            'DocumentoDataGridView
            '
            Me.DocumentoDataGridView.AllowUserToOrderColumns = True
            DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.DocumentoDataGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
            Me.DocumentoDataGridView.AutoGenerateColumns = False
            Me.DocumentoDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
            Me.DocumentoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DocumentoDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CodigoCajaDataGridViewTextBoxColumn2, Me.CBarrasFolderDataGridViewTextBoxColumn1, Me.CBarrasFileDataGridViewTextBoxColumn, Me.Data_1, Me.Data_2, Me.Data_3, Me.ArqueadoDataGridViewCheckBoxColumn1, Me.SobranteDataGridViewCheckBoxColumn1, Me.FaltanteDataGridViewCheckBoxColumn1})
            Me.DocumentoDataGridView.DataSource = Me.DocumentoBindingSource
            Me.DocumentoDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DocumentoDataGridView.Location = New System.Drawing.Point(0, 0)
            Me.DocumentoDataGridView.Name = "DocumentoDataGridView"
            Me.DocumentoDataGridView.ReadOnly = True
            Me.DocumentoDataGridView.Size = New System.Drawing.Size(859, 124)
            Me.DocumentoDataGridView.TabIndex = 0
            '
            'CodigoCajaDataGridViewTextBoxColumn2
            '
            Me.CodigoCajaDataGridViewTextBoxColumn2.DataPropertyName = "Codigo_Caja"
            Me.CodigoCajaDataGridViewTextBoxColumn2.HeaderText = "Caja"
            Me.CodigoCajaDataGridViewTextBoxColumn2.Name = "CodigoCajaDataGridViewTextBoxColumn2"
            Me.CodigoCajaDataGridViewTextBoxColumn2.ReadOnly = True
            Me.CodigoCajaDataGridViewTextBoxColumn2.Width = 53
            '
            'CBarrasFolderDataGridViewTextBoxColumn1
            '
            Me.CBarrasFolderDataGridViewTextBoxColumn1.DataPropertyName = "CBarras_Folder"
            Me.CBarrasFolderDataGridViewTextBoxColumn1.HeaderText = "Carpeta"
            Me.CBarrasFolderDataGridViewTextBoxColumn1.Name = "CBarrasFolderDataGridViewTextBoxColumn1"
            Me.CBarrasFolderDataGridViewTextBoxColumn1.ReadOnly = True
            Me.CBarrasFolderDataGridViewTextBoxColumn1.Width = 69
            '
            'CBarrasFileDataGridViewTextBoxColumn
            '
            Me.CBarrasFileDataGridViewTextBoxColumn.DataPropertyName = "CBarras_File"
            Me.CBarrasFileDataGridViewTextBoxColumn.HeaderText = "Documento"
            Me.CBarrasFileDataGridViewTextBoxColumn.Name = "CBarrasFileDataGridViewTextBoxColumn"
            Me.CBarrasFileDataGridViewTextBoxColumn.ReadOnly = True
            Me.CBarrasFileDataGridViewTextBoxColumn.Width = 87
            '
            'Data_1
            '
            Me.Data_1.DataPropertyName = "Data_1"
            Me.Data_1.HeaderText = ""
            Me.Data_1.Name = "Data_1"
            Me.Data_1.ReadOnly = True
            Me.Data_1.Width = 19
            '
            'Data_2
            '
            Me.Data_2.DataPropertyName = "Data_2"
            Me.Data_2.HeaderText = ""
            Me.Data_2.Name = "Data_2"
            Me.Data_2.ReadOnly = True
            Me.Data_2.Width = 19
            '
            'Data_3
            '
            Me.Data_3.DataPropertyName = "Data_3"
            Me.Data_3.HeaderText = ""
            Me.Data_3.Name = "Data_3"
            Me.Data_3.ReadOnly = True
            Me.Data_3.Width = 19
            '
            'ArqueadoDataGridViewCheckBoxColumn1
            '
            Me.ArqueadoDataGridViewCheckBoxColumn1.DataPropertyName = "Arqueado"
            Me.ArqueadoDataGridViewCheckBoxColumn1.HeaderText = "Arqueado"
            Me.ArqueadoDataGridViewCheckBoxColumn1.Name = "ArqueadoDataGridViewCheckBoxColumn1"
            Me.ArqueadoDataGridViewCheckBoxColumn1.ReadOnly = True
            Me.ArqueadoDataGridViewCheckBoxColumn1.Width = 59
            '
            'SobranteDataGridViewCheckBoxColumn1
            '
            Me.SobranteDataGridViewCheckBoxColumn1.DataPropertyName = "Sobrante"
            Me.SobranteDataGridViewCheckBoxColumn1.HeaderText = "Sobrante"
            Me.SobranteDataGridViewCheckBoxColumn1.Name = "SobranteDataGridViewCheckBoxColumn1"
            Me.SobranteDataGridViewCheckBoxColumn1.ReadOnly = True
            Me.SobranteDataGridViewCheckBoxColumn1.Width = 56
            '
            'FaltanteDataGridViewCheckBoxColumn1
            '
            Me.FaltanteDataGridViewCheckBoxColumn1.DataPropertyName = "Faltante"
            Me.FaltanteDataGridViewCheckBoxColumn1.HeaderText = "Faltante"
            Me.FaltanteDataGridViewCheckBoxColumn1.Name = "FaltanteDataGridViewCheckBoxColumn1"
            Me.FaltanteDataGridViewCheckBoxColumn1.ReadOnly = True
            Me.FaltanteDataGridViewCheckBoxColumn1.Width = 51
            '
            'DocumentoBindingSource
            '
            Me.DocumentoBindingSource.AllowNew = False
            Me.DocumentoBindingSource.DataMember = "CTA_RPT_Arqueo_File"
            Me.DocumentoBindingSource.DataSource = Me.dsCustody
            '
            'FormReporteArqueo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(859, 474)
            Me.Controls.Add(Me.PrimarioSplitContainer)
            Me.Controls.Add(Me.InformacionStatusStrip)
            Me.Controls.Add(Me.ComandosToolStrip)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormReporteArqueo"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Reporte de Arqueo"
            Me.InformacionStatusStrip.ResumeLayout(False)
            Me.InformacionStatusStrip.PerformLayout()
            Me.ComandosToolStrip.ResumeLayout(False)
            Me.ComandosToolStrip.PerformLayout()
            Me.PrimarioSplitContainer.Panel1.ResumeLayout(False)
            Me.PrimarioSplitContainer.Panel2.ResumeLayout(False)
            CType(Me.PrimarioSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.PrimarioSplitContainer.ResumeLayout(False)
            Me.CajaSplit.Panel1.ResumeLayout(False)
            Me.CajaSplit.Panel2.ResumeLayout(False)
            CType(Me.CajaSplit, System.ComponentModel.ISupportInitialize).EndInit()
            Me.CajaSplit.ResumeLayout(False)
            CType(Me.CajaDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CajaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dsCustody, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SecundarioSplitContainer.Panel1.ResumeLayout(False)
            Me.SecundarioSplitContainer.Panel2.ResumeLayout(False)
            CType(Me.SecundarioSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SecundarioSplitContainer.ResumeLayout(False)
            Me.CarpetaSplit.Panel1.ResumeLayout(False)
            Me.CarpetaSplit.Panel2.ResumeLayout(False)
            CType(Me.CarpetaSplit, System.ComponentModel.ISupportInitialize).EndInit()
            Me.CarpetaSplit.ResumeLayout(False)
            CType(Me.CarpetaDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CarpetaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.DocumentoSplit.Panel1.ResumeLayout(False)
            Me.DocumentoSplit.Panel2.ResumeLayout(False)
            CType(Me.DocumentoSplit, System.ComponentModel.ISupportInitialize).EndInit()
            Me.DocumentoSplit.ResumeLayout(False)
            CType(Me.DocumentoDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DocumentoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents InformacionStatusStrip As System.Windows.Forms.StatusStrip
        Friend WithEvents InformacionStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents ComandosToolStrip As System.Windows.Forms.ToolStrip
        Friend WithEvents PrimarioSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents SecundarioSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents CajaSplit As System.Windows.Forms.SplitContainer
        Friend WithEvents CajaButton As System.Windows.Forms.Button
        Friend WithEvents CarpetaSplit As System.Windows.Forms.SplitContainer
        Friend WithEvents CarpetaButton As System.Windows.Forms.Button
        Friend WithEvents DocumentoSplit As System.Windows.Forms.SplitContainer
        Friend WithEvents DocumentoButton As System.Windows.Forms.Button
        Friend WithEvents CajaDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents CarpetaDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents DocumentoDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents ModoComboBox As System.Windows.Forms.ToolStripComboBox
        Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
        Friend WithEvents NombreEntidadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DecripcionArqueoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreBovedaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreBovedaSeccionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CodigoBovedaPosicionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CodigoCajaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreEntidadClienteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreProyectoClienteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkEstadoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CajaBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents dsCustody As DBCore.CustodyDataSet
        Friend WithEvents NombreEntidadDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DecripcionArqueoDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Expr1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CarpetaBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents ExportarToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents DocumentoBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents DecripcionArqueoDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ValorFileDataDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CodigoCajaDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarrasFolderDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarrasFileDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Data_1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Data_2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Data_3 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ArqueadoDataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents SobranteDataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents FaltanteDataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents CodigoCajaDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarrasFolderDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ArqueadoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents SobranteDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents FaltanteDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents NombreProyectoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace