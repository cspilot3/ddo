Imports Miharu.Imaging.OffLineViewer.Library.Visor

Namespace Controls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class ImagingSearchControlContenedor
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
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
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImagingSearchControlContenedor))
            Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.MainPanel = New System.Windows.Forms.Panel()
            Me.BasePanel = New System.Windows.Forms.Panel()
            Me.BaseSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.ResultadosPanel = New System.Windows.Forms.Panel()
            Me.ResultadosDataGridView = New System.Windows.Forms.DataGridView()
            Me.FolderColumn_Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FolderColumn_Fecha_Creacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FolderColumn_fk_Entidad_Servidor = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FolderColumn_fk_Servidor = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Columnfk_Cargue = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Columnfk_Cargue_Paquete = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.PanelResultados = New System.Windows.Forms.Panel()
            Me.ResultadosLabel = New System.Windows.Forms.Label()
            Me.btnGuardarCambios = New System.Windows.Forms.Button()
            Me.btnGuardar = New System.Windows.Forms.Button()
            Me.CentralOffLineViewer = New Miharu.Imaging.OffLineViewer.Library.Visor.ImageViewer()
            Me.IndexerTablePanel = New System.Windows.Forms.Panel()
            Me.Table_UpPanel = New System.Windows.Forms.Panel()
            Me.TablaLabel = New System.Windows.Forms.Label()
            Me.Table_CloseButton = New System.Windows.Forms.Button()
            Me.LeftPanel = New System.Windows.Forms.Panel()
            Me.SearchSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.SearchPanel = New System.Windows.Forms.Panel()
            Me.VentanasPanel = New System.Windows.Forms.Panel()
            Me.TituloVentanasLabel = New System.Windows.Forms.Label()
            Me.DataSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.TipologiasDataGridView = New System.Windows.Forms.DataGridView()
            Me.TipologiaColumn_Nombre_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipologiaColumn_Nombre_Estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipologiaColumn_Nombre_Content_Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipologiaColumn_id_Estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipologiaColumn_fk_Entidad_Servidor = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipologiaColumn_fk_Servidor = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipologiaColumn_Nombre_Imagen_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipologiaColumn_Folios_Documento_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipologiaColumn_Tamaño_Imagen_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Cargue = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Cargue_Paquete = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Cargue_Paquete_Item = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CargueBasePanel = New System.Windows.Forms.Panel()
            Me.CargueOpcionesPanel = New System.Windows.Forms.Panel()
            Me.ShowImageButton = New System.Windows.Forms.Button()
            Me.PanelTipologias = New System.Windows.Forms.Panel()
            Me.TipologiasLabel = New System.Windows.Forms.Label()
            Me.DataTabControl = New System.Windows.Forms.TabControl()
            Me.PanelInformacion = New System.Windows.Forms.Panel()
            Me.InformacionLabel = New System.Windows.Forms.Label()
            Me.MensajesToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.CamposDataGridView = New System.Windows.Forms.DataGridView()
            Me.CamposColumn_Nombre_Campo_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CamposColumn_Valor_File_Data = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CamposColumn_Nombre_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CamposColumn_fk_Campo_Busqueda = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CamposColumn_fk_Campo_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CamposColumn_Es_Campo_Busqueda = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CamposColumn_fk_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CamposColumn_fk_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CamposColumn_fk_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CamposColumn_id_Folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CamposColumn_fk_Expediente = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CamposTabPage = New System.Windows.Forms.TabPage()
            Me.TablaAsociadaDataGridView = New System.Windows.Forms.DataGridView()
            Me.NombreCampo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataCampo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.MainPanel.SuspendLayout()
            Me.BasePanel.SuspendLayout()
            CType(Me.BaseSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.BaseSplitContainer.Panel1.SuspendLayout()
            Me.BaseSplitContainer.Panel2.SuspendLayout()
            Me.BaseSplitContainer.SuspendLayout()
            Me.ResultadosPanel.SuspendLayout()
            CType(Me.ResultadosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.PanelResultados.SuspendLayout()
            Me.IndexerTablePanel.SuspendLayout()
            Me.Table_UpPanel.SuspendLayout()
            Me.LeftPanel.SuspendLayout()
            CType(Me.SearchSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SearchSplitContainer.Panel1.SuspendLayout()
            Me.SearchSplitContainer.Panel2.SuspendLayout()
            Me.SearchSplitContainer.SuspendLayout()
            Me.SearchPanel.SuspendLayout()
            Me.VentanasPanel.SuspendLayout()
            CType(Me.DataSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.DataSplitContainer.Panel1.SuspendLayout()
            Me.DataSplitContainer.Panel2.SuspendLayout()
            Me.DataSplitContainer.SuspendLayout()
            CType(Me.TipologiasDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.CargueBasePanel.SuspendLayout()
            Me.CargueOpcionesPanel.SuspendLayout()
            Me.PanelTipologias.SuspendLayout()
            Me.DataTabControl.SuspendLayout()
            Me.PanelInformacion.SuspendLayout()
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.CamposTabPage.SuspendLayout()
            CType(Me.TablaAsociadaDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'MainPanel
            '
            Me.MainPanel.Controls.Add(Me.BasePanel)
            Me.MainPanel.Controls.Add(Me.LeftPanel)
            Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.MainPanel.Location = New System.Drawing.Point(0, 0)
            Me.MainPanel.Name = "MainPanel"
            Me.MainPanel.Size = New System.Drawing.Size(921, 543)
            Me.MainPanel.TabIndex = 0
            '
            'BasePanel
            '
            Me.BasePanel.BackColor = System.Drawing.SystemColors.Control
            Me.BasePanel.Controls.Add(Me.BaseSplitContainer)
            Me.BasePanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.BasePanel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BasePanel.Location = New System.Drawing.Point(320, 0)
            Me.BasePanel.Name = "BasePanel"
            Me.BasePanel.Size = New System.Drawing.Size(601, 543)
            Me.BasePanel.TabIndex = 10
            '
            'BaseSplitContainer
            '
            Me.BaseSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.BaseSplitContainer.Location = New System.Drawing.Point(0, 0)
            Me.BaseSplitContainer.Name = "BaseSplitContainer"
            Me.BaseSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'BaseSplitContainer.Panel1
            '
            Me.BaseSplitContainer.Panel1.Controls.Add(Me.ResultadosPanel)
            '
            'BaseSplitContainer.Panel2
            '
            Me.BaseSplitContainer.Panel2.Controls.Add(Me.btnGuardarCambios)
            Me.BaseSplitContainer.Panel2.Controls.Add(Me.btnGuardar)
            Me.BaseSplitContainer.Panel2.Controls.Add(Me.CentralOffLineViewer)
            Me.BaseSplitContainer.Panel2.Controls.Add(Me.IndexerTablePanel)
            Me.BaseSplitContainer.Size = New System.Drawing.Size(601, 543)
            Me.BaseSplitContainer.SplitterDistance = 158
            Me.BaseSplitContainer.TabIndex = 29
            '
            'ResultadosPanel
            '
            Me.ResultadosPanel.Controls.Add(Me.ResultadosDataGridView)
            Me.ResultadosPanel.Controls.Add(Me.PanelResultados)
            Me.ResultadosPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ResultadosPanel.Location = New System.Drawing.Point(0, 0)
            Me.ResultadosPanel.Name = "ResultadosPanel"
            Me.ResultadosPanel.Size = New System.Drawing.Size(601, 158)
            Me.ResultadosPanel.TabIndex = 20
            '
            'ResultadosDataGridView
            '
            Me.ResultadosDataGridView.AllowUserToAddRows = False
            Me.ResultadosDataGridView.AllowUserToDeleteRows = False
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ResultadosDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.ResultadosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ResultadosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FolderColumn_Token, Me.FolderColumn_Fecha_Creacion, Me.FolderColumn_fk_Entidad_Servidor, Me.FolderColumn_fk_Servidor, Me.Columnfk_Cargue, Me.Columnfk_Cargue_Paquete})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.ResultadosDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.ResultadosDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ResultadosDataGridView.Location = New System.Drawing.Point(0, 20)
            Me.ResultadosDataGridView.MultiSelect = False
            Me.ResultadosDataGridView.Name = "ResultadosDataGridView"
            Me.ResultadosDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ResultadosDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.ResultadosDataGridView.RowHeadersWidth = 10
            Me.ResultadosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ResultadosDataGridView.Size = New System.Drawing.Size(601, 138)
            Me.ResultadosDataGridView.TabIndex = 16
            '
            'FolderColumn_Token
            '
            Me.FolderColumn_Token.DataPropertyName = "Token"
            Me.FolderColumn_Token.HeaderText = "Contenedor"
            Me.FolderColumn_Token.Name = "FolderColumn_Token"
            Me.FolderColumn_Token.ReadOnly = True
            '
            'FolderColumn_Fecha_Creacion
            '
            Me.FolderColumn_Fecha_Creacion.DataPropertyName = "Fecha_Apertura"
            Me.FolderColumn_Fecha_Creacion.HeaderText = "Creación"
            Me.FolderColumn_Fecha_Creacion.Name = "FolderColumn_Fecha_Creacion"
            Me.FolderColumn_Fecha_Creacion.ReadOnly = True
            Me.FolderColumn_Fecha_Creacion.Width = 200
            '
            'FolderColumn_fk_Entidad_Servidor
            '
            Me.FolderColumn_fk_Entidad_Servidor.DataPropertyName = "fk_Entidad_Servidor"
            Me.FolderColumn_fk_Entidad_Servidor.HeaderText = "fk_Entidad_Servidor"
            Me.FolderColumn_fk_Entidad_Servidor.Name = "FolderColumn_fk_Entidad_Servidor"
            Me.FolderColumn_fk_Entidad_Servidor.ReadOnly = True
            Me.FolderColumn_fk_Entidad_Servidor.Visible = False
            '
            'FolderColumn_fk_Servidor
            '
            Me.FolderColumn_fk_Servidor.DataPropertyName = "fk_Servidor"
            Me.FolderColumn_fk_Servidor.HeaderText = "fk_Servidor"
            Me.FolderColumn_fk_Servidor.Name = "FolderColumn_fk_Servidor"
            Me.FolderColumn_fk_Servidor.ReadOnly = True
            Me.FolderColumn_fk_Servidor.Visible = False
            '
            'Columnfk_Cargue
            '
            Me.Columnfk_Cargue.DataPropertyName = "fk_Cargue"
            Me.Columnfk_Cargue.HeaderText = "Cargue"
            Me.Columnfk_Cargue.Name = "Columnfk_Cargue"
            Me.Columnfk_Cargue.ReadOnly = True
            '
            'Columnfk_Cargue_Paquete
            '
            Me.Columnfk_Cargue_Paquete.DataPropertyName = "fk_Cargue_Paquete"
            Me.Columnfk_Cargue_Paquete.HeaderText = "Paquete"
            Me.Columnfk_Cargue_Paquete.Name = "Columnfk_Cargue_Paquete"
            Me.Columnfk_Cargue_Paquete.ReadOnly = True
            '
            'PanelResultados
            '
            Me.PanelResultados.BackColor = System.Drawing.Color.RoyalBlue
            Me.PanelResultados.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.PanelResultados.Controls.Add(Me.ResultadosLabel)
            Me.PanelResultados.Dock = System.Windows.Forms.DockStyle.Top
            Me.PanelResultados.Location = New System.Drawing.Point(0, 0)
            Me.PanelResultados.Name = "PanelResultados"
            Me.PanelResultados.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
            Me.PanelResultados.Size = New System.Drawing.Size(601, 20)
            Me.PanelResultados.TabIndex = 22
            '
            'ResultadosLabel
            '
            Me.ResultadosLabel.Dock = System.Windows.Forms.DockStyle.Left
            Me.ResultadosLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ResultadosLabel.ForeColor = System.Drawing.Color.White
            Me.ResultadosLabel.Location = New System.Drawing.Point(5, 0)
            Me.ResultadosLabel.Name = "ResultadosLabel"
            Me.ResultadosLabel.Size = New System.Drawing.Size(148, 18)
            Me.ResultadosLabel.TabIndex = 1
            Me.ResultadosLabel.Text = "Resultados [0]"
            Me.ResultadosLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'btnGuardarCambios
            '
            Me.btnGuardarCambios.Enabled = False
            Me.btnGuardarCambios.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGuardar2
            Me.btnGuardarCambios.Location = New System.Drawing.Point(416, 4)
            Me.btnGuardarCambios.Name = "btnGuardarCambios"
            Me.btnGuardarCambios.Size = New System.Drawing.Size(26, 27)
            Me.btnGuardarCambios.TabIndex = 8
            Me.MensajesToolTip.SetToolTip(Me.btnGuardarCambios, "Actualizar documento")
            Me.btnGuardarCambios.UseVisualStyleBackColor = True
            '
            'btnGuardar
            '
            Me.btnGuardar.Location = New System.Drawing.Point(0, 0)
            Me.btnGuardar.Name = "btnGuardar"
            Me.btnGuardar.Size = New System.Drawing.Size(75, 23)
            Me.btnGuardar.TabIndex = 0
            '
            'CentralOffLineViewer
            '
            Me.CentralOffLineViewer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CentralOffLineViewer.ImagePath = CType(resources.GetObject("CentralOffLineViewer.ImagePath"), System.Collections.Generic.IEnumerable(Of String))
            Me.CentralOffLineViewer.Location = New System.Drawing.Point(0, 0)
            Me.CentralOffLineViewer.Name = "CentralOffLineViewer"
            Me.CentralOffLineViewer.SelectedPage = 1
            Me.CentralOffLineViewer.Size = New System.Drawing.Size(601, 149)
            Me.CentralOffLineViewer.TabIndex = 21
            Me.CentralOffLineViewer.UseThread = False
            Me.CentralOffLineViewer.Zoom = CType(100, Short)
            '
            'IndexerTablePanel
            '
            Me.IndexerTablePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.IndexerTablePanel.Controls.Add(Me.TablaAsociadaDataGridView)
            Me.IndexerTablePanel.Controls.Add(Me.Table_UpPanel)
            Me.IndexerTablePanel.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.IndexerTablePanel.Location = New System.Drawing.Point(0, 149)
            Me.IndexerTablePanel.Name = "IndexerTablePanel"
            Me.IndexerTablePanel.Size = New System.Drawing.Size(601, 232)
            Me.IndexerTablePanel.TabIndex = 28
            Me.IndexerTablePanel.Visible = False
            '
            'Table_UpPanel
            '
            Me.Table_UpPanel.BackColor = System.Drawing.Color.RoyalBlue
            Me.Table_UpPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Table_UpPanel.Controls.Add(Me.TablaLabel)
            Me.Table_UpPanel.Controls.Add(Me.Table_CloseButton)
            Me.Table_UpPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.Table_UpPanel.Location = New System.Drawing.Point(0, 0)
            Me.Table_UpPanel.Name = "Table_UpPanel"
            Me.Table_UpPanel.Size = New System.Drawing.Size(599, 23)
            Me.Table_UpPanel.TabIndex = 1
            '
            'TablaLabel
            '
            Me.TablaLabel.Dock = System.Windows.Forms.DockStyle.Left
            Me.TablaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TablaLabel.ForeColor = System.Drawing.Color.White
            Me.TablaLabel.Location = New System.Drawing.Point(0, 0)
            Me.TablaLabel.Name = "TablaLabel"
            Me.TablaLabel.Size = New System.Drawing.Size(148, 21)
            Me.TablaLabel.TabIndex = 2
            Me.TablaLabel.Text = "Tabla asociada [0]"
            Me.TablaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Table_CloseButton
            '
            Me.Table_CloseButton.Dock = System.Windows.Forms.DockStyle.Right
            Me.Table_CloseButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancel
            Me.Table_CloseButton.Location = New System.Drawing.Point(574, 0)
            Me.Table_CloseButton.Name = "Table_CloseButton"
            Me.Table_CloseButton.Size = New System.Drawing.Size(23, 21)
            Me.Table_CloseButton.TabIndex = 0
            Me.Table_CloseButton.UseVisualStyleBackColor = True
            '
            'LeftPanel
            '
            Me.LeftPanel.Controls.Add(Me.SearchSplitContainer)
            Me.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left
            Me.LeftPanel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LeftPanel.Location = New System.Drawing.Point(0, 0)
            Me.LeftPanel.Name = "LeftPanel"
            Me.LeftPanel.Size = New System.Drawing.Size(320, 543)
            Me.LeftPanel.TabIndex = 9
            '
            'SearchSplitContainer
            '
            Me.SearchSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SearchSplitContainer.Location = New System.Drawing.Point(0, 0)
            Me.SearchSplitContainer.Name = "SearchSplitContainer"
            Me.SearchSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SearchSplitContainer.Panel1
            '
            Me.SearchSplitContainer.Panel1.Controls.Add(Me.SearchPanel)
            Me.SearchSplitContainer.Panel1MinSize = 140
            '
            'SearchSplitContainer.Panel2
            '
            Me.SearchSplitContainer.Panel2.Controls.Add(Me.DataSplitContainer)
            Me.SearchSplitContainer.Panel2MinSize = 140
            Me.SearchSplitContainer.Size = New System.Drawing.Size(320, 543)
            Me.SearchSplitContainer.SplitterDistance = 159
            Me.SearchSplitContainer.TabIndex = 20
            '
            'SearchPanel
            '
            Me.SearchPanel.BackColor = System.Drawing.SystemColors.Control
            Me.SearchPanel.Controls.Add(Me.VentanasPanel)
            Me.SearchPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SearchPanel.Location = New System.Drawing.Point(0, 0)
            Me.SearchPanel.Name = "SearchPanel"
            Me.SearchPanel.Size = New System.Drawing.Size(320, 159)
            Me.SearchPanel.TabIndex = 18
            '
            'VentanasPanel
            '
            Me.VentanasPanel.BackColor = System.Drawing.Color.RoyalBlue
            Me.VentanasPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.VentanasPanel.Controls.Add(Me.TituloVentanasLabel)
            Me.VentanasPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.VentanasPanel.Location = New System.Drawing.Point(0, 0)
            Me.VentanasPanel.Name = "VentanasPanel"
            Me.VentanasPanel.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
            Me.VentanasPanel.Size = New System.Drawing.Size(320, 20)
            Me.VentanasPanel.TabIndex = 21
            '
            'TituloVentanasLabel
            '
            Me.TituloVentanasLabel.Dock = System.Windows.Forms.DockStyle.Left
            Me.TituloVentanasLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TituloVentanasLabel.ForeColor = System.Drawing.Color.White
            Me.TituloVentanasLabel.Location = New System.Drawing.Point(5, 0)
            Me.TituloVentanasLabel.Name = "TituloVentanasLabel"
            Me.TituloVentanasLabel.Size = New System.Drawing.Size(148, 18)
            Me.TituloVentanasLabel.TabIndex = 1
            Me.TituloVentanasLabel.Text = "Busqueda"
            Me.TituloVentanasLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'DataSplitContainer
            '
            Me.DataSplitContainer.BackColor = System.Drawing.SystemColors.Control
            Me.DataSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.DataSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DataSplitContainer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DataSplitContainer.Location = New System.Drawing.Point(0, 0)
            Me.DataSplitContainer.Name = "DataSplitContainer"
            Me.DataSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'DataSplitContainer.Panel1
            '
            Me.DataSplitContainer.Panel1.Controls.Add(Me.TipologiasDataGridView)
            Me.DataSplitContainer.Panel1.Controls.Add(Me.CargueBasePanel)
            Me.DataSplitContainer.Panel1.Controls.Add(Me.PanelTipologias)
            '
            'DataSplitContainer.Panel2
            '
            Me.DataSplitContainer.Panel2.Controls.Add(Me.DataTabControl)
            Me.DataSplitContainer.Panel2.Controls.Add(Me.PanelInformacion)
            Me.DataSplitContainer.Size = New System.Drawing.Size(320, 380)
            Me.DataSplitContainer.SplitterDistance = 220
            Me.DataSplitContainer.SplitterWidth = 5
            Me.DataSplitContainer.TabIndex = 19
            '
            'TipologiasDataGridView
            '
            Me.TipologiasDataGridView.AllowUserToAddRows = False
            Me.TipologiasDataGridView.AllowUserToDeleteRows = False
            DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.TipologiasDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
            Me.TipologiasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.TipologiasDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TipologiaColumn_Nombre_Documento, Me.TipologiaColumn_Nombre_Estado, Me.TipologiaColumn_Nombre_Content_Type, Me.TipologiaColumn_id_Estado, Me.TipologiaColumn_fk_Entidad_Servidor, Me.TipologiaColumn_fk_Servidor, Me.TipologiaColumn_Nombre_Imagen_File, Me.TipologiaColumn_Folios_Documento_File, Me.TipologiaColumn_Tamaño_Imagen_File, Me.fk_Cargue, Me.fk_Cargue_Paquete, Me.fk_Cargue_Paquete_Item})
            Me.TipologiasDataGridView.DataMember = "TBL_Folder_TBL_File"
            DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.TipologiasDataGridView.DefaultCellStyle = DataGridViewCellStyle8
            Me.TipologiasDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TipologiasDataGridView.Location = New System.Drawing.Point(0, 20)
            Me.TipologiasDataGridView.MultiSelect = False
            Me.TipologiasDataGridView.Name = "TipologiasDataGridView"
            Me.TipologiasDataGridView.ReadOnly = True
            DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.TipologiasDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
            Me.TipologiasDataGridView.RowHeadersWidth = 10
            Me.TipologiasDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.TipologiasDataGridView.Size = New System.Drawing.Size(270, 196)
            Me.TipologiasDataGridView.TabIndex = 16
            '
            'TipologiaColumn_Nombre_Documento
            '
            Me.TipologiaColumn_Nombre_Documento.DataPropertyName = "Nombre_Documento"
            Me.TipologiaColumn_Nombre_Documento.HeaderText = "Nombre Documento"
            Me.TipologiaColumn_Nombre_Documento.Name = "TipologiaColumn_Nombre_Documento"
            Me.TipologiaColumn_Nombre_Documento.ReadOnly = True
            Me.TipologiaColumn_Nombre_Documento.Width = 200
            '
            'TipologiaColumn_Nombre_Estado
            '
            Me.TipologiaColumn_Nombre_Estado.DataPropertyName = "Nombre_Estado"
            Me.TipologiaColumn_Nombre_Estado.HeaderText = "Estado"
            Me.TipologiaColumn_Nombre_Estado.Name = "TipologiaColumn_Nombre_Estado"
            Me.TipologiaColumn_Nombre_Estado.ReadOnly = True
            '
            'TipologiaColumn_Nombre_Content_Type
            '
            Me.TipologiaColumn_Nombre_Content_Type.DataPropertyName = "Nombre_Content_Type"
            Me.TipologiaColumn_Nombre_Content_Type.HeaderText = "Tipo"
            Me.TipologiaColumn_Nombre_Content_Type.Name = "TipologiaColumn_Nombre_Content_Type"
            Me.TipologiaColumn_Nombre_Content_Type.ReadOnly = True
            '
            'TipologiaColumn_id_Estado
            '
            Me.TipologiaColumn_id_Estado.DataPropertyName = "id_Estado"
            Me.TipologiaColumn_id_Estado.HeaderText = "idEstado"
            Me.TipologiaColumn_id_Estado.Name = "TipologiaColumn_id_Estado"
            Me.TipologiaColumn_id_Estado.ReadOnly = True
            Me.TipologiaColumn_id_Estado.Visible = False
            '
            'TipologiaColumn_fk_Entidad_Servidor
            '
            Me.TipologiaColumn_fk_Entidad_Servidor.DataPropertyName = "fk_Entidad_Servidor"
            Me.TipologiaColumn_fk_Entidad_Servidor.HeaderText = "Entidad Servidor "
            Me.TipologiaColumn_fk_Entidad_Servidor.Name = "TipologiaColumn_fk_Entidad_Servidor"
            Me.TipologiaColumn_fk_Entidad_Servidor.ReadOnly = True
            Me.TipologiaColumn_fk_Entidad_Servidor.Visible = False
            '
            'TipologiaColumn_fk_Servidor
            '
            Me.TipologiaColumn_fk_Servidor.DataPropertyName = "fk_Servidor"
            Me.TipologiaColumn_fk_Servidor.HeaderText = "Servidor"
            Me.TipologiaColumn_fk_Servidor.Name = "TipologiaColumn_fk_Servidor"
            Me.TipologiaColumn_fk_Servidor.ReadOnly = True
            Me.TipologiaColumn_fk_Servidor.Visible = False
            '
            'TipologiaColumn_Nombre_Imagen_File
            '
            Me.TipologiaColumn_Nombre_Imagen_File.DataPropertyName = "Nombre_Imagen_File"
            Me.TipologiaColumn_Nombre_Imagen_File.HeaderText = "Nombre Imagen"
            Me.TipologiaColumn_Nombre_Imagen_File.Name = "TipologiaColumn_Nombre_Imagen_File"
            Me.TipologiaColumn_Nombre_Imagen_File.ReadOnly = True
            Me.TipologiaColumn_Nombre_Imagen_File.Visible = False
            '
            'TipologiaColumn_Folios_Documento_File
            '
            Me.TipologiaColumn_Folios_Documento_File.DataPropertyName = "Folios_Documento_File"
            Me.TipologiaColumn_Folios_Documento_File.HeaderText = "Folios"
            Me.TipologiaColumn_Folios_Documento_File.Name = "TipologiaColumn_Folios_Documento_File"
            Me.TipologiaColumn_Folios_Documento_File.ReadOnly = True
            '
            'TipologiaColumn_Tamaño_Imagen_File
            '
            Me.TipologiaColumn_Tamaño_Imagen_File.DataPropertyName = "Tamaño_Imagen_File"
            Me.TipologiaColumn_Tamaño_Imagen_File.HeaderText = "Tamaño"
            Me.TipologiaColumn_Tamaño_Imagen_File.Name = "TipologiaColumn_Tamaño_Imagen_File"
            Me.TipologiaColumn_Tamaño_Imagen_File.ReadOnly = True
            '
            'fk_Cargue
            '
            Me.fk_Cargue.DataPropertyName = "fk_Cargue"
            Me.fk_Cargue.HeaderText = "fk_Cargue"
            Me.fk_Cargue.Name = "fk_Cargue"
            Me.fk_Cargue.ReadOnly = True
            '
            'fk_Cargue_Paquete
            '
            Me.fk_Cargue_Paquete.DataPropertyName = "fk_Cargue_Paquete"
            Me.fk_Cargue_Paquete.HeaderText = "fk_Cargue_Paquete"
            Me.fk_Cargue_Paquete.Name = "fk_Cargue_Paquete"
            Me.fk_Cargue_Paquete.ReadOnly = True
            '
            'fk_Cargue_Paquete_Item
            '
            Me.fk_Cargue_Paquete_Item.DataPropertyName = "fk_Cargue_Paquete_Item"
            Me.fk_Cargue_Paquete_Item.HeaderText = "fk_Cargue_Paquete_Item"
            Me.fk_Cargue_Paquete_Item.Name = "fk_Cargue_Paquete_Item"
            Me.fk_Cargue_Paquete_Item.ReadOnly = True
            '
            'CargueBasePanel
            '
            Me.CargueBasePanel.BackColor = System.Drawing.SystemColors.Control
            Me.CargueBasePanel.Controls.Add(Me.CargueOpcionesPanel)
            Me.CargueBasePanel.Dock = System.Windows.Forms.DockStyle.Right
            Me.CargueBasePanel.Location = New System.Drawing.Point(270, 20)
            Me.CargueBasePanel.Name = "CargueBasePanel"
            Me.CargueBasePanel.Size = New System.Drawing.Size(46, 196)
            Me.CargueBasePanel.TabIndex = 20
            '
            'CargueOpcionesPanel
            '
            Me.CargueOpcionesPanel.Controls.Add(Me.ShowImageButton)
            Me.CargueOpcionesPanel.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.CargueOpcionesPanel.Location = New System.Drawing.Point(0, 0)
            Me.CargueOpcionesPanel.Name = "CargueOpcionesPanel"
            Me.CargueOpcionesPanel.Size = New System.Drawing.Size(46, 196)
            Me.CargueOpcionesPanel.TabIndex = 1
            '
            'ShowImageButton
            '
            Me.ShowImageButton.Enabled = False
            Me.ShowImageButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.zoom
            Me.ShowImageButton.Location = New System.Drawing.Point(6, 4)
            Me.ShowImageButton.Name = "ShowImageButton"
            Me.ShowImageButton.Size = New System.Drawing.Size(34, 34)
            Me.ShowImageButton.TabIndex = 2
            Me.MensajesToolTip.SetToolTip(Me.ShowImageButton, "Visualizar imagen")
            Me.ShowImageButton.UseVisualStyleBackColor = True
            '
            'PanelTipologias
            '
            Me.PanelTipologias.BackColor = System.Drawing.Color.RoyalBlue
            Me.PanelTipologias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.PanelTipologias.Controls.Add(Me.TipologiasLabel)
            Me.PanelTipologias.Dock = System.Windows.Forms.DockStyle.Top
            Me.PanelTipologias.Location = New System.Drawing.Point(0, 0)
            Me.PanelTipologias.Name = "PanelTipologias"
            Me.PanelTipologias.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
            Me.PanelTipologias.Size = New System.Drawing.Size(316, 20)
            Me.PanelTipologias.TabIndex = 15
            '
            'TipologiasLabel
            '
            Me.TipologiasLabel.Dock = System.Windows.Forms.DockStyle.Left
            Me.TipologiasLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TipologiasLabel.ForeColor = System.Drawing.Color.White
            Me.TipologiasLabel.Location = New System.Drawing.Point(5, 0)
            Me.TipologiasLabel.Name = "TipologiasLabel"
            Me.TipologiasLabel.Size = New System.Drawing.Size(148, 18)
            Me.TipologiasLabel.TabIndex = 1
            Me.TipologiasLabel.Text = "Tipologias [0]"
            Me.TipologiasLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'DataTabControl
            '
            Me.DataTabControl.Controls.Add(Me.CamposTabPage)
            Me.DataTabControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DataTabControl.Location = New System.Drawing.Point(0, 20)
            Me.DataTabControl.Name = "DataTabControl"
            Me.DataTabControl.SelectedIndex = 0
            Me.DataTabControl.Size = New System.Drawing.Size(316, 131)
            Me.DataTabControl.TabIndex = 17
            '
            'PanelInformacion
            '
            Me.PanelInformacion.BackColor = System.Drawing.Color.RoyalBlue
            Me.PanelInformacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.PanelInformacion.Controls.Add(Me.InformacionLabel)
            Me.PanelInformacion.Dock = System.Windows.Forms.DockStyle.Top
            Me.PanelInformacion.Location = New System.Drawing.Point(0, 0)
            Me.PanelInformacion.Name = "PanelInformacion"
            Me.PanelInformacion.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
            Me.PanelInformacion.Size = New System.Drawing.Size(316, 20)
            Me.PanelInformacion.TabIndex = 15
            '
            'InformacionLabel
            '
            Me.InformacionLabel.Dock = System.Windows.Forms.DockStyle.Left
            Me.InformacionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.InformacionLabel.ForeColor = System.Drawing.Color.White
            Me.InformacionLabel.Location = New System.Drawing.Point(5, 0)
            Me.InformacionLabel.Name = "InformacionLabel"
            Me.InformacionLabel.Size = New System.Drawing.Size(148, 18)
            Me.InformacionLabel.TabIndex = 1
            Me.InformacionLabel.Text = "Información [0]"
            Me.InformacionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'CamposDataGridView
            '
            Me.CamposDataGridView.AllowUserToAddRows = False
            Me.CamposDataGridView.AllowUserToDeleteRows = False
            DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CamposDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
            Me.CamposDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CamposDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NombreCampo, Me.DataCampo})
            DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.CamposDataGridView.DefaultCellStyle = DataGridViewCellStyle11
            Me.CamposDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CamposDataGridView.Location = New System.Drawing.Point(3, 3)
            Me.CamposDataGridView.MultiSelect = False
            Me.CamposDataGridView.Name = "CamposDataGridView"
            Me.CamposDataGridView.ReadOnly = True
            DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CamposDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
            Me.CamposDataGridView.RowHeadersWidth = 10
            Me.CamposDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.CamposDataGridView.Size = New System.Drawing.Size(302, 99)
            Me.CamposDataGridView.TabIndex = 16
            '
            'CamposColumn_Nombre_Campo_Tipo
            '
            Me.CamposColumn_Nombre_Campo_Tipo.DataPropertyName = "Nombre_Campo_Tipo"
            Me.CamposColumn_Nombre_Campo_Tipo.HeaderText = "Tipo"
            Me.CamposColumn_Nombre_Campo_Tipo.Name = "CamposColumn_Nombre_Campo_Tipo"
            Me.CamposColumn_Nombre_Campo_Tipo.ReadOnly = True
            '
            'CamposColumn_Valor_File_Data
            '
            Me.CamposColumn_Valor_File_Data.DataPropertyName = "Data_Campo"
            Me.CamposColumn_Valor_File_Data.HeaderText = "Valor"
            Me.CamposColumn_Valor_File_Data.Name = "CamposColumn_Valor_File_Data"
            Me.CamposColumn_Valor_File_Data.ReadOnly = True
            Me.CamposColumn_Valor_File_Data.Width = 200
            '
            'CamposColumn_Nombre_Campo
            '
            Me.CamposColumn_Nombre_Campo.DataPropertyName = "Nombre_Campo"
            Me.CamposColumn_Nombre_Campo.HeaderText = "Nombre"
            Me.CamposColumn_Nombre_Campo.Name = "CamposColumn_Nombre_Campo"
            Me.CamposColumn_Nombre_Campo.ReadOnly = True
            Me.CamposColumn_Nombre_Campo.Width = 200
            '
            'CamposColumn_fk_Campo_Busqueda
            '
            Me.CamposColumn_fk_Campo_Busqueda.DataPropertyName = "fk_Campo_Busqueda"
            Me.CamposColumn_fk_Campo_Busqueda.HeaderText = "fk_Campo_Busqueda"
            Me.CamposColumn_fk_Campo_Busqueda.Name = "CamposColumn_fk_Campo_Busqueda"
            Me.CamposColumn_fk_Campo_Busqueda.ReadOnly = True
            Me.CamposColumn_fk_Campo_Busqueda.Visible = False
            '
            'CamposColumn_fk_Campo_Tipo
            '
            Me.CamposColumn_fk_Campo_Tipo.DataPropertyName = "fk_Campo_Tipo"
            Me.CamposColumn_fk_Campo_Tipo.HeaderText = "fk_Campo_Tipo"
            Me.CamposColumn_fk_Campo_Tipo.Name = "CamposColumn_fk_Campo_Tipo"
            Me.CamposColumn_fk_Campo_Tipo.ReadOnly = True
            Me.CamposColumn_fk_Campo_Tipo.Visible = False
            '
            'CamposColumn_Es_Campo_Busqueda
            '
            Me.CamposColumn_Es_Campo_Busqueda.DataPropertyName = "Es_Campo_Busqueda"
            Me.CamposColumn_Es_Campo_Busqueda.HeaderText = "Es_Campo_Busqueda"
            Me.CamposColumn_Es_Campo_Busqueda.Name = "CamposColumn_Es_Campo_Busqueda"
            Me.CamposColumn_Es_Campo_Busqueda.ReadOnly = True
            Me.CamposColumn_Es_Campo_Busqueda.Visible = False
            '
            'CamposColumn_fk_Campo
            '
            Me.CamposColumn_fk_Campo.DataPropertyName = "fk_Campo"
            Me.CamposColumn_fk_Campo.HeaderText = "fk_Campo"
            Me.CamposColumn_fk_Campo.Name = "CamposColumn_fk_Campo"
            Me.CamposColumn_fk_Campo.ReadOnly = True
            Me.CamposColumn_fk_Campo.Visible = False
            '
            'CamposColumn_fk_Documento
            '
            Me.CamposColumn_fk_Documento.DataPropertyName = "fk_Documento"
            Me.CamposColumn_fk_Documento.HeaderText = "fk_Documento"
            Me.CamposColumn_fk_Documento.Name = "CamposColumn_fk_Documento"
            Me.CamposColumn_fk_Documento.ReadOnly = True
            Me.CamposColumn_fk_Documento.Visible = False
            '
            'CamposColumn_fk_File
            '
            Me.CamposColumn_fk_File.DataPropertyName = "fk_File"
            Me.CamposColumn_fk_File.HeaderText = "fk_File"
            Me.CamposColumn_fk_File.Name = "CamposColumn_fk_File"
            Me.CamposColumn_fk_File.ReadOnly = True
            Me.CamposColumn_fk_File.Visible = False
            '
            'CamposColumn_id_Folder
            '
            Me.CamposColumn_id_Folder.DataPropertyName = "id_Folder"
            Me.CamposColumn_id_Folder.HeaderText = "fk_Folder"
            Me.CamposColumn_id_Folder.Name = "CamposColumn_id_Folder"
            Me.CamposColumn_id_Folder.ReadOnly = True
            Me.CamposColumn_id_Folder.Visible = False
            '
            'CamposColumn_fk_Expediente
            '
            Me.CamposColumn_fk_Expediente.DataPropertyName = "fk_Expediente"
            Me.CamposColumn_fk_Expediente.HeaderText = "fk_Expediente"
            Me.CamposColumn_fk_Expediente.Name = "CamposColumn_fk_Expediente"
            Me.CamposColumn_fk_Expediente.ReadOnly = True
            Me.CamposColumn_fk_Expediente.Visible = False
            '
            'CamposTabPage
            '
            Me.CamposTabPage.Controls.Add(Me.CamposDataGridView)
            Me.CamposTabPage.Location = New System.Drawing.Point(4, 22)
            Me.CamposTabPage.Name = "CamposTabPage"
            Me.CamposTabPage.Padding = New System.Windows.Forms.Padding(3)
            Me.CamposTabPage.Size = New System.Drawing.Size(308, 105)
            Me.CamposTabPage.TabIndex = 0
            Me.CamposTabPage.Text = "Campos"
            Me.CamposTabPage.UseVisualStyleBackColor = True
            '
            'TablaAsociadaDataGridView
            '
            Me.TablaAsociadaDataGridView.AllowUserToAddRows = False
            Me.TablaAsociadaDataGridView.AllowUserToDeleteRows = False
            Me.TablaAsociadaDataGridView.AllowUserToOrderColumns = True
            DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.TablaAsociadaDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
            Me.TablaAsociadaDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.TablaAsociadaDataGridView.DefaultCellStyle = DataGridViewCellStyle5
            Me.TablaAsociadaDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TablaAsociadaDataGridView.Location = New System.Drawing.Point(0, 23)
            Me.TablaAsociadaDataGridView.Name = "TablaAsociadaDataGridView"
            Me.TablaAsociadaDataGridView.ReadOnly = True
            DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.TablaAsociadaDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
            Me.TablaAsociadaDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.TablaAsociadaDataGridView.Size = New System.Drawing.Size(599, 207)
            Me.TablaAsociadaDataGridView.TabIndex = 2
            '
            'NombreCampo
            '
            Me.NombreCampo.DataPropertyName = "Nombre_Campo"
            Me.NombreCampo.HeaderText = "Nombre Campo"
            Me.NombreCampo.Name = "NombreCampo"
            Me.NombreCampo.ReadOnly = True
            Me.NombreCampo.Width = 200
            '
            'DataCampo
            '
            Me.DataCampo.DataPropertyName = "Data_Campo"
            Me.DataCampo.HeaderText = "Valor"
            Me.DataCampo.Name = "DataCampo"
            Me.DataCampo.ReadOnly = True
            '
            'ImagingSearchControlContenedor
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.MainPanel)
            Me.Name = "ImagingSearchControlContenedor"
            Me.Size = New System.Drawing.Size(921, 543)
            Me.MainPanel.ResumeLayout(False)
            Me.BasePanel.ResumeLayout(False)
            Me.BaseSplitContainer.Panel1.ResumeLayout(False)
            Me.BaseSplitContainer.Panel2.ResumeLayout(False)
            CType(Me.BaseSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.BaseSplitContainer.ResumeLayout(False)
            Me.ResultadosPanel.ResumeLayout(False)
            CType(Me.ResultadosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.PanelResultados.ResumeLayout(False)
            Me.IndexerTablePanel.ResumeLayout(False)
            Me.Table_UpPanel.ResumeLayout(False)
            Me.LeftPanel.ResumeLayout(False)
            Me.SearchSplitContainer.Panel1.ResumeLayout(False)
            Me.SearchSplitContainer.Panel2.ResumeLayout(False)
            CType(Me.SearchSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SearchSplitContainer.ResumeLayout(False)
            Me.SearchPanel.ResumeLayout(False)
            Me.VentanasPanel.ResumeLayout(False)
            Me.DataSplitContainer.Panel1.ResumeLayout(False)
            Me.DataSplitContainer.Panel2.ResumeLayout(False)
            CType(Me.DataSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.DataSplitContainer.ResumeLayout(False)
            CType(Me.TipologiasDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.CargueBasePanel.ResumeLayout(False)
            Me.CargueOpcionesPanel.ResumeLayout(False)
            Me.PanelTipologias.ResumeLayout(False)
            Me.DataTabControl.ResumeLayout(False)
            Me.PanelInformacion.ResumeLayout(False)
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.CamposTabPage.ResumeLayout(False)
            CType(Me.TablaAsociadaDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents MainPanel As System.Windows.Forms.Panel
        Friend WithEvents BasePanel As System.Windows.Forms.Panel
        Friend WithEvents CentralOffLineViewer As ImageViewer
        Friend WithEvents ResultadosPanel As System.Windows.Forms.Panel
        Friend WithEvents ResultadosDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents PanelResultados As System.Windows.Forms.Panel
        Friend WithEvents ResultadosLabel As System.Windows.Forms.Label
        Friend WithEvents LeftPanel As System.Windows.Forms.Panel
        Friend WithEvents DataSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents TipologiasDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents CargueBasePanel As System.Windows.Forms.Panel
        Friend WithEvents CargueOpcionesPanel As System.Windows.Forms.Panel
        Friend WithEvents PanelTipologias As System.Windows.Forms.Panel
        Friend WithEvents TipologiasLabel As System.Windows.Forms.Label
        Friend WithEvents PanelInformacion As System.Windows.Forms.Panel
        Friend WithEvents InformacionLabel As System.Windows.Forms.Label
        Friend WithEvents SearchPanel As System.Windows.Forms.Panel
        Friend WithEvents VentanasPanel As System.Windows.Forms.Panel
        Friend WithEvents TituloVentanasLabel As System.Windows.Forms.Label
        Friend WithEvents IndexerTablePanel As System.Windows.Forms.Panel
        Friend WithEvents Table_UpPanel As System.Windows.Forms.Panel
        Friend WithEvents Table_CloseButton As System.Windows.Forms.Button
        Friend WithEvents SearchSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents BaseSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents TablaLabel As System.Windows.Forms.Label
        Friend WithEvents MensajesToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents DataTabControl As System.Windows.Forms.TabControl
        Public WithEvents ShowImageButton As System.Windows.Forms.Button
        Public WithEvents btnGuardar As System.Windows.Forms.Button
        Private WithEvents btnGuardarCambios As System.Windows.Forms.Button
        Friend WithEvents TipologiaColumn_Nombre_Documento As DataGridViewTextBoxColumn
        Friend WithEvents TipologiaColumn_Nombre_Estado As DataGridViewTextBoxColumn
        Friend WithEvents TipologiaColumn_Nombre_Content_Type As DataGridViewTextBoxColumn
        Friend WithEvents TipologiaColumn_id_Estado As DataGridViewTextBoxColumn
        Friend WithEvents TipologiaColumn_fk_Entidad_Servidor As DataGridViewTextBoxColumn
        Friend WithEvents TipologiaColumn_fk_Servidor As DataGridViewTextBoxColumn
        Friend WithEvents TipologiaColumn_Nombre_Imagen_File As DataGridViewTextBoxColumn
        Friend WithEvents TipologiaColumn_Folios_Documento_File As DataGridViewTextBoxColumn
        Friend WithEvents TipologiaColumn_Tamaño_Imagen_File As DataGridViewTextBoxColumn
        Friend WithEvents fk_Cargue As DataGridViewTextBoxColumn
        Friend WithEvents fk_Cargue_Paquete As DataGridViewTextBoxColumn
        Friend WithEvents fk_Cargue_Paquete_Item As DataGridViewTextBoxColumn
        Friend WithEvents FolderColumn_Token As DataGridViewTextBoxColumn
        Friend WithEvents FolderColumn_Fecha_Creacion As DataGridViewTextBoxColumn
        Friend WithEvents FolderColumn_fk_Entidad_Servidor As DataGridViewTextBoxColumn
        Friend WithEvents FolderColumn_fk_Servidor As DataGridViewTextBoxColumn
        Friend WithEvents Columnfk_Cargue As DataGridViewTextBoxColumn
        Friend WithEvents Columnfk_Cargue_Paquete As DataGridViewTextBoxColumn
        Friend WithEvents CamposTabPage As TabPage
        Friend WithEvents CamposDataGridView As DataGridView
        Friend WithEvents CamposColumn_Nombre_Campo_Tipo As DataGridViewTextBoxColumn
        Friend WithEvents CamposColumn_Valor_File_Data As DataGridViewTextBoxColumn
        Friend WithEvents CamposColumn_Nombre_Campo As DataGridViewTextBoxColumn
        Friend WithEvents CamposColumn_fk_Campo_Busqueda As DataGridViewTextBoxColumn
        Friend WithEvents CamposColumn_fk_Campo_Tipo As DataGridViewTextBoxColumn
        Friend WithEvents CamposColumn_Es_Campo_Busqueda As DataGridViewTextBoxColumn
        Friend WithEvents CamposColumn_fk_Campo As DataGridViewTextBoxColumn
        Friend WithEvents CamposColumn_fk_Documento As DataGridViewTextBoxColumn
        Friend WithEvents CamposColumn_fk_File As DataGridViewTextBoxColumn
        Friend WithEvents CamposColumn_id_Folder As DataGridViewTextBoxColumn
        Friend WithEvents CamposColumn_fk_Expediente As DataGridViewTextBoxColumn
        Friend WithEvents TablaAsociadaDataGridView As DataGridView
        Friend WithEvents NombreCampo As DataGridViewTextBoxColumn
        Friend WithEvents DataCampo As DataGridViewTextBoxColumn
    End Class
End Namespace