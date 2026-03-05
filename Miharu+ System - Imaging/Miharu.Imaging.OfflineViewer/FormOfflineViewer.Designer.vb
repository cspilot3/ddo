Imports Miharu.Imaging.OffLineViewer.Library.Visor

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormOffLineViewer
    Inherits System.Windows.Forms.Form

    Public Sub New()
        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormOffLineViewer))
        Me.OffLineViewerMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmSalir = New System.Windows.Forms.ToolStripMenuItem()
        Me.AyudaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmAcercaDe = New System.Windows.Forms.ToolStripMenuItem()
        Me.GeneralInfoToolStripMenuItem = New System.Windows.Forms.ToolStrip()
        Me.tslEntidadL = New System.Windows.Forms.ToolStripLabel()
        Me.tslEntidad = New System.Windows.Forms.ToolStripLabel()
        Me.tss1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tslProyectoL = New System.Windows.Forms.ToolStripLabel()
        Me.tslProyecto = New System.Windows.Forms.ToolStripLabel()
        Me.SplitPrincipal = New System.Windows.Forms.SplitContainer()
        Me.SplitTipologiasInformacion = New System.Windows.Forms.SplitContainer()
        Me.pnlTipologias = New System.Windows.Forms.Panel()
        Me.TipologiasDataGridView = New System.Windows.Forms.DataGridView()
        Me.ColumnIdFile = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnTipologias = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnFolios = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OffLineDataSet = New Miharu.Imaging.OffLineViewer.Library.xsdOffLineData()
        Me.PanelTipologias = New System.Windows.Forms.Panel()
        Me.lblToggleTipologias = New System.Windows.Forms.Label()
        Me.lblTipologias = New System.Windows.Forms.Label()
        Me.pnlInformacion = New System.Windows.Forms.Panel()
        Me.DataTabControl = New System.Windows.Forms.TabControl()
        Me.CamposTabPage = New System.Windows.Forms.TabPage()
        Me.CamposDataGridView = New System.Windows.Forms.DataGridView()
        Me.ColumnCampo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnValor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ValidacionesTabPage = New System.Windows.Forms.TabPage()
        Me.ValidacionesDataGridView = New System.Windows.Forms.DataGridView()
        Me.ColumnPregunta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnRespuesta = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IdExpedienteDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdFolderDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdFileDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdVersionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdValidacionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PreguntaValidacionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RespuestaDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.FkDocumentoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PanelInformacion = New System.Windows.Forms.Panel()
        Me.lblToggleInformacion = New System.Windows.Forms.Label()
        Me.lblInformacion = New System.Windows.Forms.Label()
        Me.PanelSearch = New System.Windows.Forms.Panel()
        Me.CampoBusquedaComboBox = New System.Windows.Forms.ComboBox()
        Me.btnIniciarBusqueda = New System.Windows.Forms.Button()
        Me.txtCampoBusqueda = New System.Windows.Forms.TextBox()
        Me.lblValor = New System.Windows.Forms.Label()
        Me.lblCampo = New System.Windows.Forms.Label()
        Me.pnlVentanas = New System.Windows.Forms.Panel()
        Me.lblToggleBusqueda = New System.Windows.Forms.Label()
        Me.lblTituloVentanas = New System.Windows.Forms.Label()
        Me.SplitResultadosImagen = New System.Windows.Forms.SplitContainer()
        Me.pnlResultados = New System.Windows.Forms.Panel()
        Me.ResultadosDataGridView = New System.Windows.Forms.DataGridView()
        Me.ColumnEsquema = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnCBarrasFolder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnKey1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnKey2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnKey3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PanelResultados = New System.Windows.Forms.Panel()
        Me.lblToggleResultados = New System.Windows.Forms.Label()
        Me.lblResultados = New System.Windows.Forms.Label()
        Me.ivCentral = New Miharu.Imaging.OffLineViewer.Library.Visor.ImageViewer()
        Me.PanelImagen = New System.Windows.Forms.Panel()
        Me.lblToggleImagen = New System.Windows.Forms.Label()
        Me.lblImagen = New System.Windows.Forms.Label()
        Me.PanelIzquierdo = New System.Windows.Forms.Panel()
        Me.lblToggleBusquedaInformacion = New System.Windows.Forms.Label()
        Me.lblIzquierdo = New System.Windows.Forms.Label()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.validarImagenesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OffLineViewerMenuStrip.SuspendLayout()
        Me.GeneralInfoToolStripMenuItem.SuspendLayout()
        Me.SplitPrincipal.Panel1.SuspendLayout()
        Me.SplitPrincipal.Panel2.SuspendLayout()
        Me.SplitPrincipal.SuspendLayout()
        Me.SplitTipologiasInformacion.Panel1.SuspendLayout()
        Me.SplitTipologiasInformacion.Panel2.SuspendLayout()
        Me.SplitTipologiasInformacion.SuspendLayout()
        Me.pnlTipologias.SuspendLayout()
        CType(Me.TipologiasDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OffLineDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelTipologias.SuspendLayout()
        Me.pnlInformacion.SuspendLayout()
        Me.DataTabControl.SuspendLayout()
        Me.CamposTabPage.SuspendLayout()
        CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ValidacionesTabPage.SuspendLayout()
        CType(Me.ValidacionesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelInformacion.SuspendLayout()
        Me.PanelSearch.SuspendLayout()
        Me.pnlVentanas.SuspendLayout()
        Me.SplitResultadosImagen.Panel1.SuspendLayout()
        Me.SplitResultadosImagen.Panel2.SuspendLayout()
        Me.SplitResultadosImagen.SuspendLayout()
        Me.pnlResultados.SuspendLayout()
        CType(Me.ResultadosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelResultados.SuspendLayout()
        Me.PanelImagen.SuspendLayout()
        Me.PanelIzquierdo.SuspendLayout()
        Me.SuspendLayout()
        '
        'OffLineViewerMenuStrip
        '
        Me.OffLineViewerMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem, Me.AyudaToolStripMenuItem})
        Me.OffLineViewerMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.OffLineViewerMenuStrip.Name = "OffLineViewerMenuStrip"
        Me.OffLineViewerMenuStrip.Size = New System.Drawing.Size(914, 24)
        Me.OffLineViewerMenuStrip.TabIndex = 0
        Me.OffLineViewerMenuStrip.Text = "MenuStrip1"
        '
        'ArchivoToolStripMenuItem
        '
        Me.ArchivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.validarImagenesToolStripMenuItem, Me.ToolStripSeparator1, Me.tsmSalir})
        Me.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem"
        Me.ArchivoToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.ArchivoToolStripMenuItem.Text = "Archivo"
        '
        'tsmSalir
        '
        Me.tsmSalir.Name = "tsmSalir"
        Me.tsmSalir.Size = New System.Drawing.Size(164, 22)
        Me.tsmSalir.Text = "Salir"
        '
        'AyudaToolStripMenuItem
        '
        Me.AyudaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmAcercaDe})
        Me.AyudaToolStripMenuItem.Name = "AyudaToolStripMenuItem"
        Me.AyudaToolStripMenuItem.Size = New System.Drawing.Size(53, 20)
        Me.AyudaToolStripMenuItem.Text = "Ayuda"
        '
        'tsmAcercaDe
        '
        Me.tsmAcercaDe.Name = "tsmAcercaDe"
        Me.tsmAcercaDe.Size = New System.Drawing.Size(253, 22)
        Me.tsmAcercaDe.Text = "Acerca de Miharu OffLineViewer..."
        '
        'GeneralInfoToolStripMenuItem
        '
        Me.GeneralInfoToolStripMenuItem.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslEntidadL, Me.tslEntidad, Me.tss1, Me.tslProyectoL, Me.tslProyecto})
        Me.GeneralInfoToolStripMenuItem.Location = New System.Drawing.Point(0, 24)
        Me.GeneralInfoToolStripMenuItem.Name = "GeneralInfoToolStripMenuItem"
        Me.GeneralInfoToolStripMenuItem.Size = New System.Drawing.Size(914, 25)
        Me.GeneralInfoToolStripMenuItem.TabIndex = 1
        Me.GeneralInfoToolStripMenuItem.Text = "ToolStrip1"
        '
        'tslEntidadL
        '
        Me.tslEntidadL.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tslEntidadL.Name = "tslEntidadL"
        Me.tslEntidadL.Size = New System.Drawing.Size(51, 22)
        Me.tslEntidadL.Text = "Entidad:"
        '
        'tslEntidad
        '
        Me.tslEntidad.Name = "tslEntidad"
        Me.tslEntidad.Size = New System.Drawing.Size(47, 22)
        Me.tslEntidad.Text = "Entidad"
        '
        'tss1
        '
        Me.tss1.Name = "tss1"
        Me.tss1.Size = New System.Drawing.Size(6, 25)
        '
        'tslProyectoL
        '
        Me.tslProyectoL.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tslProyectoL.Name = "tslProyectoL"
        Me.tslProyectoL.Size = New System.Drawing.Size(60, 22)
        Me.tslProyectoL.Text = "Proyecto:"
        '
        'tslProyecto
        '
        Me.tslProyecto.Name = "tslProyecto"
        Me.tslProyecto.Size = New System.Drawing.Size(54, 22)
        Me.tslProyecto.Text = "Proyecto"
        '
        'SplitPrincipal
        '
        Me.SplitPrincipal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitPrincipal.Location = New System.Drawing.Point(0, 49)
        Me.SplitPrincipal.Name = "SplitPrincipal"
        '
        'SplitPrincipal.Panel1
        '
        Me.SplitPrincipal.Panel1.Controls.Add(Me.SplitTipologiasInformacion)
        Me.SplitPrincipal.Panel1.Controls.Add(Me.PanelSearch)
        '
        'SplitPrincipal.Panel2
        '
        Me.SplitPrincipal.Panel2.Controls.Add(Me.SplitResultadosImagen)
        Me.SplitPrincipal.Panel2.Controls.Add(Me.PanelIzquierdo)
        Me.SplitPrincipal.Size = New System.Drawing.Size(914, 546)
        Me.SplitPrincipal.SplitterDistance = 283
        Me.SplitPrincipal.TabIndex = 2
        '
        'SplitTipologiasInformacion
        '
        Me.SplitTipologiasInformacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitTipologiasInformacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitTipologiasInformacion.Location = New System.Drawing.Point(0, 120)
        Me.SplitTipologiasInformacion.Name = "SplitTipologiasInformacion"
        Me.SplitTipologiasInformacion.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitTipologiasInformacion.Panel1
        '
        Me.SplitTipologiasInformacion.Panel1.Controls.Add(Me.pnlTipologias)
        Me.SplitTipologiasInformacion.Panel1.Controls.Add(Me.PanelTipologias)
        Me.SplitTipologiasInformacion.Panel1MinSize = 20
        '
        'SplitTipologiasInformacion.Panel2
        '
        Me.SplitTipologiasInformacion.Panel2.Controls.Add(Me.pnlInformacion)
        Me.SplitTipologiasInformacion.Panel2.Controls.Add(Me.PanelInformacion)
        Me.SplitTipologiasInformacion.Panel2MinSize = 20
        Me.SplitTipologiasInformacion.Size = New System.Drawing.Size(283, 426)
        Me.SplitTipologiasInformacion.SplitterDistance = 149
        Me.SplitTipologiasInformacion.TabIndex = 1
        '
        'pnlTipologias
        '
        Me.pnlTipologias.Controls.Add(Me.TipologiasDataGridView)
        Me.pnlTipologias.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTipologias.Location = New System.Drawing.Point(0, 20)
        Me.pnlTipologias.Name = "pnlTipologias"
        Me.pnlTipologias.Padding = New System.Windows.Forms.Padding(5)
        Me.pnlTipologias.Size = New System.Drawing.Size(281, 127)
        Me.pnlTipologias.TabIndex = 16
        '
        'TipologiasDataGridView
        '
        Me.TipologiasDataGridView.AllowUserToAddRows = False
        Me.TipologiasDataGridView.AllowUserToDeleteRows = False
        Me.TipologiasDataGridView.AutoGenerateColumns = False
        Me.TipologiasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TipologiasDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnIdFile, Me.ColumnTipologias, Me.ColumnFolios})
        Me.TipologiasDataGridView.DataMember = "TBL_File"
        Me.TipologiasDataGridView.DataSource = Me.OffLineDataSet
        Me.TipologiasDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TipologiasDataGridView.Location = New System.Drawing.Point(5, 5)
        Me.TipologiasDataGridView.MultiSelect = False
        Me.TipologiasDataGridView.Name = "TipologiasDataGridView"
        Me.TipologiasDataGridView.ReadOnly = True
        Me.TipologiasDataGridView.RowHeadersWidth = 10
        Me.TipologiasDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.TipologiasDataGridView.Size = New System.Drawing.Size(271, 117)
        Me.TipologiasDataGridView.TabIndex = 0
        '
        'ColumnIdFile
        '
        Me.ColumnIdFile.DataPropertyName = "id_File"
        Me.ColumnIdFile.HeaderText = "id"
        Me.ColumnIdFile.Name = "ColumnIdFile"
        Me.ColumnIdFile.ReadOnly = True
        Me.ColumnIdFile.Width = 30
        '
        'ColumnTipologias
        '
        Me.ColumnTipologias.DataPropertyName = "Nombre_Documento"
        Me.ColumnTipologias.HeaderText = "Tipología"
        Me.ColumnTipologias.Name = "ColumnTipologias"
        Me.ColumnTipologias.ReadOnly = True
        Me.ColumnTipologias.Width = 150
        '
        'ColumnFolios
        '
        Me.ColumnFolios.DataPropertyName = "Folios_Documento_File"
        Me.ColumnFolios.HeaderText = "Folios"
        Me.ColumnFolios.Name = "ColumnFolios"
        Me.ColumnFolios.ReadOnly = True
        Me.ColumnFolios.Width = 40
        '
        'OffLineDataSet
        '
        Me.OffLineDataSet.DataSetName = "xsdOffLineData"
        Me.OffLineDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'PanelTipologias
        '
        Me.PanelTipologias.BackColor = System.Drawing.Color.RoyalBlue
        Me.PanelTipologias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelTipologias.Controls.Add(Me.lblToggleTipologias)
        Me.PanelTipologias.Controls.Add(Me.lblTipologias)
        Me.PanelTipologias.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTipologias.Location = New System.Drawing.Point(0, 0)
        Me.PanelTipologias.Name = "PanelTipologias"
        Me.PanelTipologias.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.PanelTipologias.Size = New System.Drawing.Size(281, 20)
        Me.PanelTipologias.TabIndex = 14
        '
        'lblToggleTipologias
        '
        Me.lblToggleTipologias.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblToggleTipologias.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblToggleTipologias.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToggleTipologias.ForeColor = System.Drawing.Color.White
        Me.lblToggleTipologias.Location = New System.Drawing.Point(256, 0)
        Me.lblToggleTipologias.Name = "lblToggleTipologias"
        Me.lblToggleTipologias.Size = New System.Drawing.Size(18, 18)
        Me.lblToggleTipologias.TabIndex = 2
        Me.lblToggleTipologias.Text = "^"
        Me.lblToggleTipologias.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTipologias
        '
        Me.lblTipologias.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblTipologias.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTipologias.ForeColor = System.Drawing.Color.White
        Me.lblTipologias.Location = New System.Drawing.Point(5, 0)
        Me.lblTipologias.Name = "lblTipologias"
        Me.lblTipologias.Size = New System.Drawing.Size(148, 18)
        Me.lblTipologias.TabIndex = 1
        Me.lblTipologias.Text = "Tipologias"
        Me.lblTipologias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlInformacion
        '
        Me.pnlInformacion.Controls.Add(Me.DataTabControl)
        Me.pnlInformacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlInformacion.Location = New System.Drawing.Point(0, 20)
        Me.pnlInformacion.Name = "pnlInformacion"
        Me.pnlInformacion.Padding = New System.Windows.Forms.Padding(5)
        Me.pnlInformacion.Size = New System.Drawing.Size(281, 251)
        Me.pnlInformacion.TabIndex = 16
        '
        'DataTabControl
        '
        Me.DataTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.DataTabControl.Controls.Add(Me.CamposTabPage)
        Me.DataTabControl.Controls.Add(Me.ValidacionesTabPage)
        Me.DataTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataTabControl.Location = New System.Drawing.Point(5, 5)
        Me.DataTabControl.Name = "DataTabControl"
        Me.DataTabControl.SelectedIndex = 0
        Me.DataTabControl.Size = New System.Drawing.Size(271, 241)
        Me.DataTabControl.TabIndex = 1
        '
        'CamposTabPage
        '
        Me.CamposTabPage.Controls.Add(Me.CamposDataGridView)
        Me.CamposTabPage.Location = New System.Drawing.Point(4, 4)
        Me.CamposTabPage.Name = "CamposTabPage"
        Me.CamposTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.CamposTabPage.Size = New System.Drawing.Size(263, 215)
        Me.CamposTabPage.TabIndex = 0
        Me.CamposTabPage.Text = "Campos"
        Me.CamposTabPage.UseVisualStyleBackColor = True
        '
        'CamposDataGridView
        '
        Me.CamposDataGridView.AllowUserToAddRows = False
        Me.CamposDataGridView.AllowUserToDeleteRows = False
        Me.CamposDataGridView.AutoGenerateColumns = False
        Me.CamposDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CamposDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnCampo, Me.ColumnValor})
        Me.CamposDataGridView.DataMember = "TBL_File_Data"
        Me.CamposDataGridView.DataSource = Me.OffLineDataSet
        Me.CamposDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CamposDataGridView.Location = New System.Drawing.Point(3, 3)
        Me.CamposDataGridView.MultiSelect = False
        Me.CamposDataGridView.Name = "CamposDataGridView"
        Me.CamposDataGridView.RowHeadersWidth = 10
        Me.CamposDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.CamposDataGridView.Size = New System.Drawing.Size(257, 209)
        Me.CamposDataGridView.TabIndex = 0
        '
        'ColumnCampo
        '
        Me.ColumnCampo.DataPropertyName = "Nombre_Campo"
        Me.ColumnCampo.HeaderText = "Campo"
        Me.ColumnCampo.Name = "ColumnCampo"
        Me.ColumnCampo.Width = 110
        '
        'ColumnValor
        '
        Me.ColumnValor.DataPropertyName = "Valor_File_Data"
        Me.ColumnValor.HeaderText = "Valor"
        Me.ColumnValor.Name = "ColumnValor"
        Me.ColumnValor.ReadOnly = True
        Me.ColumnValor.Width = 110
        '
        'ValidacionesTabPage
        '
        Me.ValidacionesTabPage.Controls.Add(Me.ValidacionesDataGridView)
        Me.ValidacionesTabPage.Location = New System.Drawing.Point(4, 4)
        Me.ValidacionesTabPage.Name = "ValidacionesTabPage"
        Me.ValidacionesTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ValidacionesTabPage.Size = New System.Drawing.Size(263, 215)
        Me.ValidacionesTabPage.TabIndex = 1
        Me.ValidacionesTabPage.Text = "Validaciones"
        Me.ValidacionesTabPage.UseVisualStyleBackColor = True
        '
        'ValidacionesDataGridView
        '
        Me.ValidacionesDataGridView.AllowUserToAddRows = False
        Me.ValidacionesDataGridView.AllowUserToDeleteRows = False
        Me.ValidacionesDataGridView.AutoGenerateColumns = False
        Me.ValidacionesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ValidacionesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnPregunta, Me.ColumnRespuesta, Me.IdExpedienteDataGridViewTextBoxColumn, Me.IdFolderDataGridViewTextBoxColumn, Me.IdFileDataGridViewTextBoxColumn, Me.IdVersionDataGridViewTextBoxColumn, Me.IdValidacionDataGridViewTextBoxColumn, Me.PreguntaValidacionDataGridViewTextBoxColumn, Me.RespuestaDataGridViewCheckBoxColumn, Me.FkDocumentoDataGridViewTextBoxColumn})
        Me.ValidacionesDataGridView.DataMember = "TBL_File_Validacion"
        Me.ValidacionesDataGridView.DataSource = Me.OffLineDataSet
        Me.ValidacionesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ValidacionesDataGridView.Location = New System.Drawing.Point(3, 3)
        Me.ValidacionesDataGridView.MultiSelect = False
        Me.ValidacionesDataGridView.Name = "ValidacionesDataGridView"
        Me.ValidacionesDataGridView.RowHeadersWidth = 10
        Me.ValidacionesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ValidacionesDataGridView.Size = New System.Drawing.Size(257, 209)
        Me.ValidacionesDataGridView.TabIndex = 1
        '
        'ColumnPregunta
        '
        Me.ColumnPregunta.DataPropertyName = "Pregunta_Validacion"
        Me.ColumnPregunta.HeaderText = "Validación"
        Me.ColumnPregunta.Name = "ColumnPregunta"
        Me.ColumnPregunta.Width = 200
        '
        'ColumnRespuesta
        '
        Me.ColumnRespuesta.DataPropertyName = "Respuesta"
        Me.ColumnRespuesta.HeaderText = ""
        Me.ColumnRespuesta.Name = "ColumnRespuesta"
        Me.ColumnRespuesta.ReadOnly = True
        Me.ColumnRespuesta.Width = 20
        '
        'IdExpedienteDataGridViewTextBoxColumn
        '
        Me.IdExpedienteDataGridViewTextBoxColumn.DataPropertyName = "id_Expediente"
        Me.IdExpedienteDataGridViewTextBoxColumn.HeaderText = "id_Expediente"
        Me.IdExpedienteDataGridViewTextBoxColumn.Name = "IdExpedienteDataGridViewTextBoxColumn"
        '
        'IdFolderDataGridViewTextBoxColumn
        '
        Me.IdFolderDataGridViewTextBoxColumn.DataPropertyName = "id_Folder"
        Me.IdFolderDataGridViewTextBoxColumn.HeaderText = "id_Folder"
        Me.IdFolderDataGridViewTextBoxColumn.Name = "IdFolderDataGridViewTextBoxColumn"
        '
        'IdFileDataGridViewTextBoxColumn
        '
        Me.IdFileDataGridViewTextBoxColumn.DataPropertyName = "id_File"
        Me.IdFileDataGridViewTextBoxColumn.HeaderText = "id_File"
        Me.IdFileDataGridViewTextBoxColumn.Name = "IdFileDataGridViewTextBoxColumn"
        '
        'IdVersionDataGridViewTextBoxColumn
        '
        Me.IdVersionDataGridViewTextBoxColumn.DataPropertyName = "id_Version"
        Me.IdVersionDataGridViewTextBoxColumn.HeaderText = "id_Version"
        Me.IdVersionDataGridViewTextBoxColumn.Name = "IdVersionDataGridViewTextBoxColumn"
        '
        'IdValidacionDataGridViewTextBoxColumn
        '
        Me.IdValidacionDataGridViewTextBoxColumn.DataPropertyName = "id_Validacion"
        Me.IdValidacionDataGridViewTextBoxColumn.HeaderText = "id_Validacion"
        Me.IdValidacionDataGridViewTextBoxColumn.Name = "IdValidacionDataGridViewTextBoxColumn"
        '
        'PreguntaValidacionDataGridViewTextBoxColumn
        '
        Me.PreguntaValidacionDataGridViewTextBoxColumn.DataPropertyName = "Pregunta_Validacion"
        Me.PreguntaValidacionDataGridViewTextBoxColumn.HeaderText = "Pregunta_Validacion"
        Me.PreguntaValidacionDataGridViewTextBoxColumn.Name = "PreguntaValidacionDataGridViewTextBoxColumn"
        '
        'RespuestaDataGridViewCheckBoxColumn
        '
        Me.RespuestaDataGridViewCheckBoxColumn.DataPropertyName = "Respuesta"
        Me.RespuestaDataGridViewCheckBoxColumn.HeaderText = "Respuesta"
        Me.RespuestaDataGridViewCheckBoxColumn.Name = "RespuestaDataGridViewCheckBoxColumn"
        Me.RespuestaDataGridViewCheckBoxColumn.ReadOnly = True
        '
        'FkDocumentoDataGridViewTextBoxColumn
        '
        Me.FkDocumentoDataGridViewTextBoxColumn.DataPropertyName = "fk_Documento"
        Me.FkDocumentoDataGridViewTextBoxColumn.HeaderText = "fk_Documento"
        Me.FkDocumentoDataGridViewTextBoxColumn.Name = "FkDocumentoDataGridViewTextBoxColumn"
        '
        'PanelInformacion
        '
        Me.PanelInformacion.BackColor = System.Drawing.Color.RoyalBlue
        Me.PanelInformacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelInformacion.Controls.Add(Me.lblToggleInformacion)
        Me.PanelInformacion.Controls.Add(Me.lblInformacion)
        Me.PanelInformacion.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelInformacion.Location = New System.Drawing.Point(0, 0)
        Me.PanelInformacion.Name = "PanelInformacion"
        Me.PanelInformacion.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.PanelInformacion.Size = New System.Drawing.Size(281, 20)
        Me.PanelInformacion.TabIndex = 14
        '
        'lblToggleInformacion
        '
        Me.lblToggleInformacion.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblToggleInformacion.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblToggleInformacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToggleInformacion.ForeColor = System.Drawing.Color.White
        Me.lblToggleInformacion.Location = New System.Drawing.Point(256, 0)
        Me.lblToggleInformacion.Name = "lblToggleInformacion"
        Me.lblToggleInformacion.Size = New System.Drawing.Size(18, 18)
        Me.lblToggleInformacion.TabIndex = 2
        Me.lblToggleInformacion.Text = "v"
        Me.lblToggleInformacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblInformacion
        '
        Me.lblInformacion.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblInformacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInformacion.ForeColor = System.Drawing.Color.White
        Me.lblInformacion.Location = New System.Drawing.Point(5, 0)
        Me.lblInformacion.Name = "lblInformacion"
        Me.lblInformacion.Size = New System.Drawing.Size(148, 18)
        Me.lblInformacion.TabIndex = 1
        Me.lblInformacion.Text = "Información"
        Me.lblInformacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PanelSearch
        '
        Me.PanelSearch.Controls.Add(Me.CampoBusquedaComboBox)
        Me.PanelSearch.Controls.Add(Me.btnIniciarBusqueda)
        Me.PanelSearch.Controls.Add(Me.txtCampoBusqueda)
        Me.PanelSearch.Controls.Add(Me.lblValor)
        Me.PanelSearch.Controls.Add(Me.lblCampo)
        Me.PanelSearch.Controls.Add(Me.pnlVentanas)
        Me.PanelSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelSearch.Location = New System.Drawing.Point(0, 0)
        Me.PanelSearch.Name = "PanelSearch"
        Me.PanelSearch.Size = New System.Drawing.Size(283, 120)
        Me.PanelSearch.TabIndex = 0
        '
        'CampoBusquedaComboBox
        '
        Me.CampoBusquedaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CampoBusquedaComboBox.FormattingEnabled = True
        Me.CampoBusquedaComboBox.Location = New System.Drawing.Point(62, 27)
        Me.CampoBusquedaComboBox.Name = "CampoBusquedaComboBox"
        Me.CampoBusquedaComboBox.Size = New System.Drawing.Size(203, 21)
        Me.CampoBusquedaComboBox.TabIndex = 20
        '
        'btnIniciarBusqueda
        '
        Me.btnIniciarBusqueda.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIniciarBusqueda.Image = CType(resources.GetObject("btnIniciarBusqueda.Image"), System.Drawing.Image)
        Me.btnIniciarBusqueda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnIniciarBusqueda.Location = New System.Drawing.Point(190, 91)
        Me.btnIniciarBusqueda.Name = "btnIniciarBusqueda"
        Me.btnIniciarBusqueda.Size = New System.Drawing.Size(75, 23)
        Me.btnIniciarBusqueda.TabIndex = 19
        Me.btnIniciarBusqueda.Text = "&Buscar"
        Me.btnIniciarBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnIniciarBusqueda.UseVisualStyleBackColor = True
        '
        'txtCampoBusqueda
        '
        Me.txtCampoBusqueda.Location = New System.Drawing.Point(62, 62)
        Me.txtCampoBusqueda.Name = "txtCampoBusqueda"
        Me.txtCampoBusqueda.Size = New System.Drawing.Size(203, 20)
        Me.txtCampoBusqueda.TabIndex = 18
        '
        'lblValor
        '
        Me.lblValor.AutoSize = True
        Me.lblValor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblValor.Location = New System.Drawing.Point(13, 62)
        Me.lblValor.Name = "lblValor"
        Me.lblValor.Size = New System.Drawing.Size(36, 13)
        Me.lblValor.TabIndex = 17
        Me.lblValor.Text = "Valor"
        '
        'lblCampo
        '
        Me.lblCampo.AutoSize = True
        Me.lblCampo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCampo.Location = New System.Drawing.Point(13, 27)
        Me.lblCampo.Name = "lblCampo"
        Me.lblCampo.Size = New System.Drawing.Size(45, 13)
        Me.lblCampo.TabIndex = 15
        Me.lblCampo.Text = "Campo"
        '
        'pnlVentanas
        '
        Me.pnlVentanas.BackColor = System.Drawing.Color.RoyalBlue
        Me.pnlVentanas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlVentanas.Controls.Add(Me.lblToggleBusqueda)
        Me.pnlVentanas.Controls.Add(Me.lblTituloVentanas)
        Me.pnlVentanas.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlVentanas.Location = New System.Drawing.Point(0, 0)
        Me.pnlVentanas.Name = "pnlVentanas"
        Me.pnlVentanas.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.pnlVentanas.Size = New System.Drawing.Size(283, 20)
        Me.pnlVentanas.TabIndex = 14
        '
        'lblToggleBusqueda
        '
        Me.lblToggleBusqueda.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblToggleBusqueda.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblToggleBusqueda.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToggleBusqueda.ForeColor = System.Drawing.Color.White
        Me.lblToggleBusqueda.Location = New System.Drawing.Point(258, 0)
        Me.lblToggleBusqueda.Name = "lblToggleBusqueda"
        Me.lblToggleBusqueda.Size = New System.Drawing.Size(18, 18)
        Me.lblToggleBusqueda.TabIndex = 2
        Me.lblToggleBusqueda.Text = "^"
        Me.lblToggleBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTituloVentanas
        '
        Me.lblTituloVentanas.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblTituloVentanas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTituloVentanas.ForeColor = System.Drawing.Color.White
        Me.lblTituloVentanas.Location = New System.Drawing.Point(5, 0)
        Me.lblTituloVentanas.Name = "lblTituloVentanas"
        Me.lblTituloVentanas.Size = New System.Drawing.Size(148, 18)
        Me.lblTituloVentanas.TabIndex = 1
        Me.lblTituloVentanas.Text = "Busqueda"
        Me.lblTituloVentanas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SplitResultadosImagen
        '
        Me.SplitResultadosImagen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitResultadosImagen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitResultadosImagen.Location = New System.Drawing.Point(21, 0)
        Me.SplitResultadosImagen.Name = "SplitResultadosImagen"
        Me.SplitResultadosImagen.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitResultadosImagen.Panel1
        '
        Me.SplitResultadosImagen.Panel1.Controls.Add(Me.pnlResultados)
        Me.SplitResultadosImagen.Panel1.Controls.Add(Me.PanelResultados)
        Me.SplitResultadosImagen.Panel1MinSize = 20
        '
        'SplitResultadosImagen.Panel2
        '
        Me.SplitResultadosImagen.Panel2.Controls.Add(Me.ivCentral)
        Me.SplitResultadosImagen.Panel2.Controls.Add(Me.PanelImagen)
        Me.SplitResultadosImagen.Panel2MinSize = 20
        Me.SplitResultadosImagen.Size = New System.Drawing.Size(606, 546)
        Me.SplitResultadosImagen.SplitterDistance = 120
        Me.SplitResultadosImagen.TabIndex = 0
        '
        'pnlResultados
        '
        Me.pnlResultados.Controls.Add(Me.ResultadosDataGridView)
        Me.pnlResultados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlResultados.Location = New System.Drawing.Point(0, 20)
        Me.pnlResultados.Name = "pnlResultados"
        Me.pnlResultados.Padding = New System.Windows.Forms.Padding(5)
        Me.pnlResultados.Size = New System.Drawing.Size(604, 98)
        Me.pnlResultados.TabIndex = 15
        '
        'ResultadosDataGridView
        '
        Me.ResultadosDataGridView.AllowUserToAddRows = False
        Me.ResultadosDataGridView.AllowUserToDeleteRows = False
        Me.ResultadosDataGridView.AutoGenerateColumns = False
        Me.ResultadosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ResultadosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnEsquema, Me.ColumnCBarrasFolder, Me.ColumnKey1, Me.ColumnKey2, Me.ColumnKey3})
        Me.ResultadosDataGridView.DataMember = "TBL_Folder"
        Me.ResultadosDataGridView.DataSource = Me.OffLineDataSet
        Me.ResultadosDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResultadosDataGridView.Location = New System.Drawing.Point(5, 5)
        Me.ResultadosDataGridView.MultiSelect = False
        Me.ResultadosDataGridView.Name = "ResultadosDataGridView"
        Me.ResultadosDataGridView.ReadOnly = True
        Me.ResultadosDataGridView.RowHeadersWidth = 10
        Me.ResultadosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ResultadosDataGridView.Size = New System.Drawing.Size(594, 88)
        Me.ResultadosDataGridView.TabIndex = 0
        '
        'ColumnEsquema
        '
        Me.ColumnEsquema.DataPropertyName = "Nombre_Esquema"
        Me.ColumnEsquema.HeaderText = "Esquema"
        Me.ColumnEsquema.Name = "ColumnEsquema"
        Me.ColumnEsquema.ReadOnly = True
        Me.ColumnEsquema.Width = 150
        '
        'ColumnCBarrasFolder
        '
        Me.ColumnCBarrasFolder.DataPropertyName = "CBarras_Folder"
        Me.ColumnCBarrasFolder.HeaderText = "CBarras"
        Me.ColumnCBarrasFolder.Name = "ColumnCBarrasFolder"
        Me.ColumnCBarrasFolder.ReadOnly = True
        Me.ColumnCBarrasFolder.Width = 120
        '
        'ColumnKey1
        '
        Me.ColumnKey1.DataPropertyName = "Key_1"
        Me.ColumnKey1.HeaderText = ""
        Me.ColumnKey1.Name = "ColumnKey1"
        Me.ColumnKey1.ReadOnly = True
        Me.ColumnKey1.Width = 200
        '
        'ColumnKey2
        '
        Me.ColumnKey2.DataPropertyName = "Key_2"
        Me.ColumnKey2.HeaderText = ""
        Me.ColumnKey2.Name = "ColumnKey2"
        Me.ColumnKey2.ReadOnly = True
        Me.ColumnKey2.Width = 200
        '
        'ColumnKey3
        '
        Me.ColumnKey3.DataPropertyName = "Key_3"
        Me.ColumnKey3.HeaderText = ""
        Me.ColumnKey3.Name = "ColumnKey3"
        Me.ColumnKey3.ReadOnly = True
        Me.ColumnKey3.Width = 200
        '
        'PanelResultados
        '
        Me.PanelResultados.BackColor = System.Drawing.Color.RoyalBlue
        Me.PanelResultados.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelResultados.Controls.Add(Me.lblToggleResultados)
        Me.PanelResultados.Controls.Add(Me.lblResultados)
        Me.PanelResultados.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelResultados.Location = New System.Drawing.Point(0, 0)
        Me.PanelResultados.Name = "PanelResultados"
        Me.PanelResultados.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.PanelResultados.Size = New System.Drawing.Size(604, 20)
        Me.PanelResultados.TabIndex = 14
        '
        'lblToggleResultados
        '
        Me.lblToggleResultados.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblToggleResultados.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblToggleResultados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToggleResultados.ForeColor = System.Drawing.Color.White
        Me.lblToggleResultados.Location = New System.Drawing.Point(579, 0)
        Me.lblToggleResultados.Name = "lblToggleResultados"
        Me.lblToggleResultados.Size = New System.Drawing.Size(18, 18)
        Me.lblToggleResultados.TabIndex = 2
        Me.lblToggleResultados.Text = "^"
        Me.lblToggleResultados.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblResultados
        '
        Me.lblResultados.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblResultados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResultados.ForeColor = System.Drawing.Color.White
        Me.lblResultados.Location = New System.Drawing.Point(5, 0)
        Me.lblResultados.Name = "lblResultados"
        Me.lblResultados.Size = New System.Drawing.Size(148, 18)
        Me.lblResultados.TabIndex = 1
        Me.lblResultados.Text = "Resultados"
        Me.lblResultados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ivCentral
        '
        Me.ivCentral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ivCentral.ImagePath = CType(resources.GetObject("ivCentral.ImagePath"), System.Collections.Generic.IEnumerable(Of String))
        Me.ivCentral.Location = New System.Drawing.Point(0, 20)
        Me.ivCentral.Name = "ivCentral"
        Me.ivCentral.SelectedPage = 1
        Me.ivCentral.Size = New System.Drawing.Size(604, 400)
        Me.ivCentral.TabIndex = 18
        Me.ivCentral.UseThread = True
        Me.ivCentral.Zoom = CType(100, Short)
        '
        'PanelImagen
        '
        Me.PanelImagen.BackColor = System.Drawing.Color.RoyalBlue
        Me.PanelImagen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelImagen.Controls.Add(Me.lblToggleImagen)
        Me.PanelImagen.Controls.Add(Me.lblImagen)
        Me.PanelImagen.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelImagen.Location = New System.Drawing.Point(0, 0)
        Me.PanelImagen.Name = "PanelImagen"
        Me.PanelImagen.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.PanelImagen.Size = New System.Drawing.Size(604, 20)
        Me.PanelImagen.TabIndex = 14
        '
        'lblToggleImagen
        '
        Me.lblToggleImagen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblToggleImagen.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblToggleImagen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToggleImagen.ForeColor = System.Drawing.Color.White
        Me.lblToggleImagen.Location = New System.Drawing.Point(579, 0)
        Me.lblToggleImagen.Name = "lblToggleImagen"
        Me.lblToggleImagen.Size = New System.Drawing.Size(18, 18)
        Me.lblToggleImagen.TabIndex = 2
        Me.lblToggleImagen.Text = "v"
        Me.lblToggleImagen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblImagen
        '
        Me.lblImagen.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblImagen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImagen.ForeColor = System.Drawing.Color.White
        Me.lblImagen.Location = New System.Drawing.Point(5, 0)
        Me.lblImagen.Name = "lblImagen"
        Me.lblImagen.Size = New System.Drawing.Size(148, 18)
        Me.lblImagen.TabIndex = 1
        Me.lblImagen.Text = "Imagen"
        Me.lblImagen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PanelIzquierdo
        '
        Me.PanelIzquierdo.BackColor = System.Drawing.Color.RoyalBlue
        Me.PanelIzquierdo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelIzquierdo.Controls.Add(Me.lblToggleBusquedaInformacion)
        Me.PanelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelIzquierdo.Location = New System.Drawing.Point(0, 0)
        Me.PanelIzquierdo.Name = "PanelIzquierdo"
        Me.PanelIzquierdo.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.PanelIzquierdo.Size = New System.Drawing.Size(21, 546)
        Me.PanelIzquierdo.TabIndex = 16
        '
        'lblToggleBusquedaInformacion
        '
        Me.lblToggleBusquedaInformacion.AutoSize = True
        Me.lblToggleBusquedaInformacion.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblToggleBusquedaInformacion.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblToggleBusquedaInformacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToggleBusquedaInformacion.ForeColor = System.Drawing.Color.White
        Me.lblToggleBusquedaInformacion.Location = New System.Drawing.Point(0, 0)
        Me.lblToggleBusquedaInformacion.Name = "lblToggleBusquedaInformacion"
        Me.lblToggleBusquedaInformacion.Size = New System.Drawing.Size(14, 13)
        Me.lblToggleBusquedaInformacion.TabIndex = 2
        Me.lblToggleBusquedaInformacion.Text = "<"
        Me.lblToggleBusquedaInformacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIzquierdo
        '
        Me.lblIzquierdo.AutoSize = True
        Me.lblIzquierdo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblIzquierdo.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblIzquierdo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIzquierdo.ForeColor = System.Drawing.Color.White
        Me.lblIzquierdo.Location = New System.Drawing.Point(0, 0)
        Me.lblIzquierdo.Name = "lblIzquierdo"
        Me.lblIzquierdo.Size = New System.Drawing.Size(14, 13)
        Me.lblIzquierdo.TabIndex = 2
        Me.lblIzquierdo.Text = "<"
        Me.lblIzquierdo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(161, 6)
        Me.ToolStripSeparator1.Visible = False
        '
        'validarImagenesToolStripMenuItem
        '
        Me.validarImagenesToolStripMenuItem.Name = "validarImagenesToolStripMenuItem"
        Me.validarImagenesToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.validarImagenesToolStripMenuItem.Text = "Validar imágenes"
        Me.validarImagenesToolStripMenuItem.Visible = False
        '
        'FormOffLineViewer
        '
        Me.AcceptButton = Me.btnIniciarBusqueda
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(914, 595)
        Me.Controls.Add(Me.SplitPrincipal)
        Me.Controls.Add(Me.GeneralInfoToolStripMenuItem)
        Me.Controls.Add(Me.OffLineViewerMenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.OffLineViewerMenuStrip
        Me.Name = "FormOffLineViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Visor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.OffLineViewerMenuStrip.ResumeLayout(False)
        Me.OffLineViewerMenuStrip.PerformLayout()
        Me.GeneralInfoToolStripMenuItem.ResumeLayout(False)
        Me.GeneralInfoToolStripMenuItem.PerformLayout()
        Me.SplitPrincipal.Panel1.ResumeLayout(False)
        Me.SplitPrincipal.Panel2.ResumeLayout(False)
        Me.SplitPrincipal.ResumeLayout(False)
        Me.SplitTipologiasInformacion.Panel1.ResumeLayout(False)
        Me.SplitTipologiasInformacion.Panel2.ResumeLayout(False)
        Me.SplitTipologiasInformacion.ResumeLayout(False)
        Me.pnlTipologias.ResumeLayout(False)
        CType(Me.TipologiasDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OffLineDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelTipologias.ResumeLayout(False)
        Me.pnlInformacion.ResumeLayout(False)
        Me.DataTabControl.ResumeLayout(False)
        Me.CamposTabPage.ResumeLayout(False)
        CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ValidacionesTabPage.ResumeLayout(False)
        CType(Me.ValidacionesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelInformacion.ResumeLayout(False)
        Me.PanelSearch.ResumeLayout(False)
        Me.PanelSearch.PerformLayout()
        Me.pnlVentanas.ResumeLayout(False)
        Me.SplitResultadosImagen.Panel1.ResumeLayout(False)
        Me.SplitResultadosImagen.Panel2.ResumeLayout(False)
        Me.SplitResultadosImagen.ResumeLayout(False)
        Me.pnlResultados.ResumeLayout(False)
        CType(Me.ResultadosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelResultados.ResumeLayout(False)
        Me.PanelImagen.ResumeLayout(False)
        Me.PanelIzquierdo.ResumeLayout(False)
        Me.PanelIzquierdo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OffLineViewerMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents ArchivoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AyudaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GeneralInfoToolStripMenuItem As System.Windows.Forms.ToolStrip
    Friend WithEvents tslEntidadL As System.Windows.Forms.ToolStripLabel
    Friend WithEvents SplitPrincipal As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitTipologiasInformacion As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelSearch As System.Windows.Forms.Panel
    Friend WithEvents SplitResultadosImagen As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelResultados As System.Windows.Forms.Panel
    Friend WithEvents lblToggleResultados As System.Windows.Forms.Label
    Friend WithEvents lblResultados As System.Windows.Forms.Label
    Friend WithEvents PanelImagen As System.Windows.Forms.Panel
    Friend WithEvents lblToggleImagen As System.Windows.Forms.Label
    Friend WithEvents lblImagen As System.Windows.Forms.Label
    Friend WithEvents PanelTipologias As System.Windows.Forms.Panel
    Friend WithEvents lblToggleTipologias As System.Windows.Forms.Label
    Friend WithEvents lblTipologias As System.Windows.Forms.Label
    Friend WithEvents PanelInformacion As System.Windows.Forms.Panel
    Friend WithEvents lblToggleInformacion As System.Windows.Forms.Label
    Friend WithEvents lblInformacion As System.Windows.Forms.Label
    Friend WithEvents pnlVentanas As System.Windows.Forms.Panel
    Friend WithEvents lblToggleBusqueda As System.Windows.Forms.Label
    Friend WithEvents lblTituloVentanas As System.Windows.Forms.Label
    Friend WithEvents PanelIzquierdo As System.Windows.Forms.Panel
    Friend WithEvents lblToggleBusquedaInformacion As System.Windows.Forms.Label
    Friend WithEvents lblIzquierdo As System.Windows.Forms.Label
    Friend WithEvents CampoBusquedaComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents btnIniciarBusqueda As System.Windows.Forms.Button
    Friend WithEvents txtCampoBusqueda As System.Windows.Forms.TextBox
    Friend WithEvents lblValor As System.Windows.Forms.Label
    Friend WithEvents lblCampo As System.Windows.Forms.Label
    Friend WithEvents pnlResultados As System.Windows.Forms.Panel
    Friend WithEvents ResultadosDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents pnlTipologias As System.Windows.Forms.Panel
    Friend WithEvents TipologiasDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents pnlInformacion As System.Windows.Forms.Panel
    Friend WithEvents CamposDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ivCentral As ImageViewer
    Friend WithEvents OffLineDataSet As Miharu.Imaging.OffLineViewer.Library.xsdOffLineData
    Friend WithEvents FkEntidadServidorDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FkServidorDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PathServidorVolumenDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdFolderDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdFileDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdVersionDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FkCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NombreCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EsCampoBusquedaDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FkCampoTipoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FkCampoBusquedaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ValorFileDataDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tslEntidad As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tss1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tslProyectoL As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tslProyecto As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tsmAcercaDe As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmSalir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IdFolderDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdFileDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdVersionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FileUniqueIdentifierDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NombreDocumentoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NombreImagenFileDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FoliosDocumentoFileDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TamañoImagenFileDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnEsquema As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnCBarrasFolder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnKey1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnKey2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnKey3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnIdFile As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnTipologias As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnFolios As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataTabControl As System.Windows.Forms.TabControl
    Friend WithEvents CamposTabPage As System.Windows.Forms.TabPage
    Friend WithEvents ValidacionesTabPage As System.Windows.Forms.TabPage
    Friend WithEvents ValidacionesDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ColumnCampo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnValor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnPregunta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnRespuesta As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents IdExpedienteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdFolderDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdValidacionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PreguntaValidacionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RespuestaDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FkDocumentoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents validarImagenesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator

End Class
