Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormRiskWorkSpace
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRiskWorkSpace))
        Me.WSPanel = New System.Windows.Forms.Panel()
        Me.CargueDecevalButton = New System.Windows.Forms.Button()
        Me.ButtonCancelados = New System.Windows.Forms.Button()
        Me.PrevalidarFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.PrevalidarButton = New System.Windows.Forms.Button()
        Me.DesembolsosButton = New System.Windows.Forms.Button()
        Me.CierreFechaRecoleccion = New System.Windows.Forms.Button()
        Me.Envio_CorreoButton = New System.Windows.Forms.Button()
        Me.EmpaqueEndososButton = New System.Windows.Forms.Button()
        Me.lblProyecto = New System.Windows.Forms.Label()
        Me.lblEntidad = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cerrarOTButton = New System.Windows.Forms.Button()
        Me.ot_CerrarOTFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.empaque_CentroDFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.centroDistribucionButton = New System.Windows.Forms.Button()
        Me.recepcion_DestapeFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.mesa_DevolucionFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.insercionesButton = New System.Windows.Forms.Button()
        Me.destape_DevolucionFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.devolucionesButton = New System.Windows.Forms.Button()
        Me.mesaControlImagenesButton = New System.Windows.Forms.Button()
        Me.centroD_CustodiaFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.destape_MesaFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.ot_RecepcionFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.cargueUniversal_OTFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Actualizar_RecepcionFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.custodiaButton = New System.Windows.Forms.Button()
        Me.empaqueButton = New System.Windows.Forms.Button()
        Me.mesaControlFisicoButton = New System.Windows.Forms.Button()
        Me.actualizarButton = New System.Windows.Forms.Button()
        Me.destapeButton = New System.Windows.Forms.Button()
        Me.recepcionButton = New System.Windows.Forms.Button()
        Me.cargueUniversalButton = New System.Windows.Forms.Button()
        Me.mesa_EmpaqueFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.otButton = New System.Windows.Forms.Button()
        Me.cargueParcialButton = New System.Windows.Forms.Button()
        Me.MainTabControl = New System.Windows.Forms.TabControl()
        Me.ProcesoTabPage = New System.Windows.Forms.TabPage()
        Me.FisicosTabPage = New System.Windows.Forms.TabPage()
        Me.MesasFlowLayoutPanel = New System.Windows.Forms.Panel()
        Me.Empaque_CentroDMesaFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.CentroDistribucionMesaButton = New System.Windows.Forms.Button()
        Me.Primera_DevolucionesFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Segunda_TerceraFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Tercera_EmpaqueFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Segunda_EmpaqueFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Primera_EmpaqueFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Empaque2Button = New System.Windows.Forms.Button()
        Me.Primera_SegundaFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.FisicoTerceraCapturaButton = New System.Windows.Forms.Button()
        Me.FisicoSegundaCapturaButton = New System.Windows.Forms.Button()
        Me.DevolucionesMCButton = New System.Windows.Forms.Button()
        Me.FisicoPrimeraCapturaButton = New System.Windows.Forms.Button()
        Me.BusquedaTabPage = New System.Windows.Forms.TabPage()
        Me.DataPanel = New System.Windows.Forms.Panel()
        Me.CerraDataButton = New System.Windows.Forms.Button()
        Me.DataAsociadaDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
        Me.DetallePanel = New System.Windows.Forms.Panel()
        Me.CerrarButton = New System.Windows.Forms.Button()
        Me.DetalleDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
        Me.CBarras = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha_Log = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Estado_Log = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CajaProceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CajaCustodia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Usuario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BusquedaSplitContainer = New System.Windows.Forms.SplitContainer()
        Me.FoldersGroupBox = New System.Windows.Forms.GroupBox()
        Me.RemoveFolderButton = New System.Windows.Forms.Button()
        Me.FoldersDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
        Me.CBarras_Folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Esquema = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Data1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Data2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Data3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Llaves = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FileDataSplitContainer = New System.Windows.Forms.SplitContainer()
        Me.FilesGroupBox = New System.Windows.Forms.GroupBox()
        Me.CambiaDocumentoButton = New System.Windows.Forms.Button()
        Me.FilesDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
        Me.CBarras_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Monto_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Folios_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre_Tipologia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CBarrasFolder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fk_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fk_Log_cargue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RemoveFileButton = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.EstadosGroupBox = New System.Windows.Forms.GroupBox()
        Me.EstadosDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
        Me.Modulo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EstadoFile = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGroupBox = New System.Windows.Forms.GroupBox()
        Me.TablaAsociadaButton = New System.Windows.Forms.Button()
        Me.DataDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
        Me.Nombre_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Valor_File_Data = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EditDataButton = New System.Windows.Forms.Button()
        Me.BusquedaGroupBox = New System.Windows.Forms.GroupBox()
        Me.BuscarButton = New System.Windows.Forms.Button()
        Me.CBarrasRadioButton = New System.Windows.Forms.RadioButton()
        Me.CampoRadioButton = New System.Windows.Forms.RadioButton()
        Me.CBarrasPanel = New System.Windows.Forms.Panel()
        Me.cbarrasDesktopCBarrasControl = New Miharu.Desktop.Controls.DesktopCBarras.DesktopCBarrasControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CampoPanel = New System.Windows.Forms.Panel()
        Me.CampoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.CondicionComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.ValorBusquedaTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
        Me.InformesTabPage = New System.Windows.Forms.TabPage()
        Me.WorkSpaceRiskReportViewerControl = New Miharu.Desktop.Controls.DesktopReportViewer.DesktopReportViewerControl()
        Me.ProyectoDecevalTabPage = New System.Windows.Forms.TabPage()
        Me.btnCargueLogDeceval = New System.Windows.Forms.Button()
        Me.FlechaControl1 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.FlechaControl2 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.FlechaControl5 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.FlechaControl6 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.FlechaControl7 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.btnMesaControlDeceval = New System.Windows.Forms.Button()
        Me.TabControlImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.WorkSpaceMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.configuracionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RiskToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EsquemasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CamposToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DestapeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OTsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UniversalVSParcialToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MesaDeControlFísicosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReprocesosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EstadosOTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CodigosDeBarrasToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CarpetasYDocumentosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CajasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProcesamientoLightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EstrucutrasDeCargueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CargueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActualizaciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProcesosEspecialesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ValidacionesDinámicasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.WSPanel.SuspendLayout()
        Me.MainTabControl.SuspendLayout()
        Me.ProcesoTabPage.SuspendLayout()
        Me.FisicosTabPage.SuspendLayout()
        Me.MesasFlowLayoutPanel.SuspendLayout()
        Me.BusquedaTabPage.SuspendLayout()
        Me.DataPanel.SuspendLayout()
        CType(Me.DataAsociadaDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DetallePanel.SuspendLayout()
        CType(Me.DetalleDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BusquedaSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BusquedaSplitContainer.Panel1.SuspendLayout()
        Me.BusquedaSplitContainer.Panel2.SuspendLayout()
        Me.BusquedaSplitContainer.SuspendLayout()
        Me.FoldersGroupBox.SuspendLayout()
        CType(Me.FoldersDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FileDataSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FileDataSplitContainer.Panel1.SuspendLayout()
        Me.FileDataSplitContainer.Panel2.SuspendLayout()
        Me.FileDataSplitContainer.SuspendLayout()
        Me.FilesGroupBox.SuspendLayout()
        CType(Me.FilesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.EstadosGroupBox.SuspendLayout()
        CType(Me.EstadosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DataGroupBox.SuspendLayout()
        CType(Me.DataDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BusquedaGroupBox.SuspendLayout()
        Me.CBarrasPanel.SuspendLayout()
        Me.CampoPanel.SuspendLayout()
        Me.InformesTabPage.SuspendLayout()
        Me.ProyectoDecevalTabPage.SuspendLayout()
        Me.WorkSpaceMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'WSPanel
        '
        Me.WSPanel.BackColor = System.Drawing.SystemColors.Control
        Me.WSPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.WSPanel.Controls.Add(Me.CargueDecevalButton)
        Me.WSPanel.Controls.Add(Me.ButtonCancelados)
        Me.WSPanel.Controls.Add(Me.PrevalidarFlecha)
        Me.WSPanel.Controls.Add(Me.PrevalidarButton)
        Me.WSPanel.Controls.Add(Me.DesembolsosButton)
        Me.WSPanel.Controls.Add(Me.CierreFechaRecoleccion)
        Me.WSPanel.Controls.Add(Me.Envio_CorreoButton)
        Me.WSPanel.Controls.Add(Me.EmpaqueEndososButton)
        Me.WSPanel.Controls.Add(Me.lblProyecto)
        Me.WSPanel.Controls.Add(Me.lblEntidad)
        Me.WSPanel.Controls.Add(Me.Label3)
        Me.WSPanel.Controls.Add(Me.Label2)
        Me.WSPanel.Controls.Add(Me.cerrarOTButton)
        Me.WSPanel.Controls.Add(Me.ot_CerrarOTFlecha)
        Me.WSPanel.Controls.Add(Me.empaque_CentroDFlecha)
        Me.WSPanel.Controls.Add(Me.centroDistribucionButton)
        Me.WSPanel.Controls.Add(Me.recepcion_DestapeFlecha)
        Me.WSPanel.Controls.Add(Me.mesa_DevolucionFlecha)
        Me.WSPanel.Controls.Add(Me.insercionesButton)
        Me.WSPanel.Controls.Add(Me.destape_DevolucionFlecha)
        Me.WSPanel.Controls.Add(Me.devolucionesButton)
        Me.WSPanel.Controls.Add(Me.mesaControlImagenesButton)
        Me.WSPanel.Controls.Add(Me.centroD_CustodiaFlecha)
        Me.WSPanel.Controls.Add(Me.destape_MesaFlecha)
        Me.WSPanel.Controls.Add(Me.ot_RecepcionFlecha)
        Me.WSPanel.Controls.Add(Me.cargueUniversal_OTFlecha)
        Me.WSPanel.Controls.Add(Me.Actualizar_RecepcionFlecha)
        Me.WSPanel.Controls.Add(Me.custodiaButton)
        Me.WSPanel.Controls.Add(Me.empaqueButton)
        Me.WSPanel.Controls.Add(Me.mesaControlFisicoButton)
        Me.WSPanel.Controls.Add(Me.actualizarButton)
        Me.WSPanel.Controls.Add(Me.destapeButton)
        Me.WSPanel.Controls.Add(Me.recepcionButton)
        Me.WSPanel.Controls.Add(Me.cargueUniversalButton)
        Me.WSPanel.Controls.Add(Me.mesa_EmpaqueFlecha)
        Me.WSPanel.Controls.Add(Me.otButton)
        Me.WSPanel.Controls.Add(Me.cargueParcialButton)
        Me.WSPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WSPanel.Location = New System.Drawing.Point(3, 3)
        Me.WSPanel.Name = "WSPanel"
        Me.WSPanel.Size = New System.Drawing.Size(1331, 418)
        Me.WSPanel.TabIndex = 1
        '
        'CargueDecevalButton
        '
        Me.CargueDecevalButton.AccessibleDescription = ""
        Me.CargueDecevalButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CargueDecevalButton.BackColor = System.Drawing.Color.White
        Me.CargueDecevalButton.Enabled = False
        Me.CargueDecevalButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CargueDecevalButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CargueDecevalButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.BtnCargar
        Me.CargueDecevalButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CargueDecevalButton.Location = New System.Drawing.Point(129, 79)
        Me.CargueDecevalButton.Name = "CargueDecevalButton"
        Me.CargueDecevalButton.Size = New System.Drawing.Size(113, 60)
        Me.CargueDecevalButton.TabIndex = 84
        Me.CargueDecevalButton.Tag = "Ctrl + U"
        Me.CargueDecevalButton.Text = "Cargue Deceval"
        Me.CargueDecevalButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CargueDecevalButton.UseVisualStyleBackColor = False
        '
        'ButtonCancelados
        '
        Me.ButtonCancelados.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonCancelados.BackColor = System.Drawing.Color.YellowGreen
        Me.ButtonCancelados.Enabled = False
        Me.ButtonCancelados.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonCancelados.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCancelados.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Empaque
        Me.ButtonCancelados.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonCancelados.Location = New System.Drawing.Point(1214, 117)
        Me.ButtonCancelados.Name = "ButtonCancelados"
        Me.ButtonCancelados.Size = New System.Drawing.Size(110, 60)
        Me.ButtonCancelados.TabIndex = 83
        Me.ButtonCancelados.Text = "Empaque Cancelados"
        Me.ButtonCancelados.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ButtonCancelados.UseVisualStyleBackColor = False
        '
        'PrevalidarFlecha
        '
        Me.PrevalidarFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PrevalidarFlecha.ArrowWidth = 10
        Me.PrevalidarFlecha.BackColor = System.Drawing.Color.Transparent
        Me.PrevalidarFlecha.BorderColor = System.Drawing.Color.Black
        Me.PrevalidarFlecha.BorderWidth = CType(1, Byte)
        Me.PrevalidarFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.PrevalidarFlecha.FillColor = System.Drawing.Color.White
        Me.PrevalidarFlecha.Location = New System.Drawing.Point(248, 210)
        Me.PrevalidarFlecha.Name = "PrevalidarFlecha"
        Me.PrevalidarFlecha.Size = New System.Drawing.Size(34, 36)
        Me.PrevalidarFlecha.TabIndex = 82
        Me.PrevalidarFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        Me.PrevalidarFlecha.Visible = False
        '
        'PrevalidarButton
        '
        Me.PrevalidarButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PrevalidarButton.BackColor = System.Drawing.Color.LightSteelBlue
        Me.PrevalidarButton.Enabled = False
        Me.PrevalidarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PrevalidarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrevalidarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.CargueParcial
        Me.PrevalidarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.PrevalidarButton.Location = New System.Drawing.Point(132, 196)
        Me.PrevalidarButton.Name = "PrevalidarButton"
        Me.PrevalidarButton.Size = New System.Drawing.Size(110, 60)
        Me.PrevalidarButton.TabIndex = 81
        Me.PrevalidarButton.Text = "Prevalidar Cargue"
        Me.PrevalidarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.PrevalidarButton.UseVisualStyleBackColor = False
        Me.PrevalidarButton.Visible = False
        '
        'DesembolsosButton
        '
        Me.DesembolsosButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.DesembolsosButton.BackColor = System.Drawing.Color.White
        Me.DesembolsosButton.Enabled = False
        Me.DesembolsosButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DesembolsosButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DesembolsosButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.CargueParcial
        Me.DesembolsosButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.DesembolsosButton.Location = New System.Drawing.Point(1084, 30)
        Me.DesembolsosButton.Name = "DesembolsosButton"
        Me.DesembolsosButton.Size = New System.Drawing.Size(110, 60)
        Me.DesembolsosButton.TabIndex = 80
        Me.DesembolsosButton.Text = "Desembolsos"
        Me.DesembolsosButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.DesembolsosButton.UseVisualStyleBackColor = False
        '
        'CierreFechaRecoleccion
        '
        Me.CierreFechaRecoleccion.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CierreFechaRecoleccion.BackColor = System.Drawing.Color.White
        Me.CierreFechaRecoleccion.Enabled = False
        Me.CierreFechaRecoleccion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CierreFechaRecoleccion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CierreFechaRecoleccion.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Calendario
        Me.CierreFechaRecoleccion.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CierreFechaRecoleccion.Location = New System.Drawing.Point(1084, 349)
        Me.CierreFechaRecoleccion.Name = "CierreFechaRecoleccion"
        Me.CierreFechaRecoleccion.Size = New System.Drawing.Size(110, 60)
        Me.CierreFechaRecoleccion.TabIndex = 79
        Me.CierreFechaRecoleccion.Text = "Cierre Fecha Recolección"
        Me.CierreFechaRecoleccion.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CierreFechaRecoleccion.UseVisualStyleBackColor = False
        '
        'Envio_CorreoButton
        '
        Me.Envio_CorreoButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Envio_CorreoButton.BackColor = System.Drawing.Color.White
        Me.Envio_CorreoButton.Enabled = False
        Me.Envio_CorreoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Envio_CorreoButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Envio_CorreoButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.envio_correo
        Me.Envio_CorreoButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Envio_CorreoButton.Location = New System.Drawing.Point(1084, 235)
        Me.Envio_CorreoButton.Name = "Envio_CorreoButton"
        Me.Envio_CorreoButton.Size = New System.Drawing.Size(110, 60)
        Me.Envio_CorreoButton.TabIndex = 78
        Me.Envio_CorreoButton.Text = "Enviar Correo"
        Me.Envio_CorreoButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Envio_CorreoButton.UseVisualStyleBackColor = False
        '
        'EmpaqueEndososButton
        '
        Me.EmpaqueEndososButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.EmpaqueEndososButton.BackColor = System.Drawing.Color.LightSteelBlue
        Me.EmpaqueEndososButton.Enabled = False
        Me.EmpaqueEndososButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EmpaqueEndososButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmpaqueEndososButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Empaque
        Me.EmpaqueEndososButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.EmpaqueEndososButton.Location = New System.Drawing.Point(1084, 117)
        Me.EmpaqueEndososButton.Name = "EmpaqueEndososButton"
        Me.EmpaqueEndososButton.Size = New System.Drawing.Size(110, 60)
        Me.EmpaqueEndososButton.TabIndex = 77
        Me.EmpaqueEndososButton.Text = "Empaque Endosos"
        Me.EmpaqueEndososButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.EmpaqueEndososButton.UseVisualStyleBackColor = False
        '
        'lblProyecto
        '
        Me.lblProyecto.AutoSize = True
        Me.lblProyecto.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProyecto.Location = New System.Drawing.Point(88, 45)
        Me.lblProyecto.Name = "lblProyecto"
        Me.lblProyecto.Size = New System.Drawing.Size(17, 17)
        Me.lblProyecto.TabIndex = 76
        Me.lblProyecto.Text = "_"
        '
        'lblEntidad
        '
        Me.lblEntidad.AutoSize = True
        Me.lblEntidad.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntidad.Location = New System.Drawing.Point(88, 16)
        Me.lblEntidad.Name = "lblEntidad"
        Me.lblEntidad.Size = New System.Drawing.Size(17, 17)
        Me.lblEntidad.TabIndex = 75
        Me.lblEntidad.Text = "_"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.DarkRed
        Me.Label3.Location = New System.Drawing.Point(12, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 17)
        Me.Label3.TabIndex = 74
        Me.Label3.Text = "Proyecto"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DarkRed
        Me.Label2.Location = New System.Drawing.Point(12, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 17)
        Me.Label2.TabIndex = 73
        Me.Label2.Text = "Entidad"
        '
        'cerrarOTButton
        '
        Me.cerrarOTButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cerrarOTButton.BackColor = System.Drawing.Color.LightSteelBlue
        Me.cerrarOTButton.Enabled = False
        Me.cerrarOTButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cerrarOTButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cerrarOTButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.CerrarOT
        Me.cerrarOTButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cerrarOTButton.Location = New System.Drawing.Point(442, 281)
        Me.cerrarOTButton.Name = "cerrarOTButton"
        Me.cerrarOTButton.Size = New System.Drawing.Size(110, 60)
        Me.cerrarOTButton.TabIndex = 72
        Me.cerrarOTButton.Text = "(F9) Cerrar O.T."
        Me.cerrarOTButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cerrarOTButton.UseVisualStyleBackColor = False
        '
        'ot_CerrarOTFlecha
        '
        Me.ot_CerrarOTFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ot_CerrarOTFlecha.ArrowWidth = 10
        Me.ot_CerrarOTFlecha.BackColor = System.Drawing.Color.Transparent
        Me.ot_CerrarOTFlecha.BorderColor = System.Drawing.Color.Black
        Me.ot_CerrarOTFlecha.BorderWidth = CType(1, Byte)
        Me.ot_CerrarOTFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.ot_CerrarOTFlecha.FillColor = System.Drawing.Color.White
        Me.ot_CerrarOTFlecha.Location = New System.Drawing.Point(335, 262)
        Me.ot_CerrarOTFlecha.Name = "ot_CerrarOTFlecha"
        Me.ot_CerrarOTFlecha.Size = New System.Drawing.Size(101, 60)
        Me.ot_CerrarOTFlecha.TabIndex = 71
        Me.ot_CerrarOTFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN_RIGHT
        '
        'empaque_CentroDFlecha
        '
        Me.empaque_CentroDFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.empaque_CentroDFlecha.ArrowWidth = 10
        Me.empaque_CentroDFlecha.BackColor = System.Drawing.Color.Transparent
        Me.empaque_CentroDFlecha.BorderColor = System.Drawing.Color.Black
        Me.empaque_CentroDFlecha.BorderWidth = CType(1, Byte)
        Me.empaque_CentroDFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.empaque_CentroDFlecha.FillColor = System.Drawing.Color.SteelBlue
        Me.empaque_CentroDFlecha.Location = New System.Drawing.Point(931, 183)
        Me.empaque_CentroDFlecha.Name = "empaque_CentroDFlecha"
        Me.empaque_CentroDFlecha.Size = New System.Drawing.Size(110, 41)
        Me.empaque_CentroDFlecha.TabIndex = 70
        Me.empaque_CentroDFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN
        '
        'centroDistribucionButton
        '
        Me.centroDistribucionButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.centroDistribucionButton.BackColor = System.Drawing.Color.White
        Me.centroDistribucionButton.Enabled = False
        Me.centroDistribucionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.centroDistribucionButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.centroDistribucionButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.CentroDistribucion
        Me.centroDistribucionButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.centroDistribucionButton.Location = New System.Drawing.Point(931, 235)
        Me.centroDistribucionButton.Name = "centroDistribucionButton"
        Me.centroDistribucionButton.Size = New System.Drawing.Size(113, 60)
        Me.centroDistribucionButton.TabIndex = 69
        Me.centroDistribucionButton.Text = "(F7) Distribución"
        Me.centroDistribucionButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.centroDistribucionButton.UseVisualStyleBackColor = False
        '
        'recepcion_DestapeFlecha
        '
        Me.recepcion_DestapeFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.recepcion_DestapeFlecha.ArrowWidth = 10
        Me.recepcion_DestapeFlecha.BackColor = System.Drawing.Color.Transparent
        Me.recepcion_DestapeFlecha.BorderColor = System.Drawing.Color.Black
        Me.recepcion_DestapeFlecha.BorderWidth = CType(1, Byte)
        Me.recepcion_DestapeFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.recepcion_DestapeFlecha.FillColor = System.Drawing.Color.LightSteelBlue
        Me.recepcion_DestapeFlecha.Location = New System.Drawing.Point(558, 196)
        Me.recepcion_DestapeFlecha.Name = "recepcion_DestapeFlecha"
        Me.recepcion_DestapeFlecha.Size = New System.Drawing.Size(37, 60)
        Me.recepcion_DestapeFlecha.TabIndex = 68
        Me.recepcion_DestapeFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'mesa_DevolucionFlecha
        '
        Me.mesa_DevolucionFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.mesa_DevolucionFlecha.ArrowWidth = 10
        Me.mesa_DevolucionFlecha.BackColor = System.Drawing.Color.Transparent
        Me.mesa_DevolucionFlecha.BorderColor = System.Drawing.Color.Black
        Me.mesa_DevolucionFlecha.BorderWidth = CType(1, Byte)
        Me.mesa_DevolucionFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.mesa_DevolucionFlecha.FillColor = System.Drawing.Color.IndianRed
        Me.mesa_DevolucionFlecha.Location = New System.Drawing.Point(793, 219)
        Me.mesa_DevolucionFlecha.Name = "mesa_DevolucionFlecha"
        Me.mesa_DevolucionFlecha.Size = New System.Drawing.Size(42, 172)
        Me.mesa_DevolucionFlecha.TabIndex = 67
        Me.mesa_DevolucionFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN_LEFT
        '
        'insercionesButton
        '
        Me.insercionesButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.insercionesButton.BackColor = System.Drawing.Color.LightSteelBlue
        Me.insercionesButton.Enabled = False
        Me.insercionesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.insercionesButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.insercionesButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Insercion
        Me.insercionesButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.insercionesButton.Location = New System.Drawing.Point(285, 349)
        Me.insercionesButton.Name = "insercionesButton"
        Me.insercionesButton.Size = New System.Drawing.Size(110, 60)
        Me.insercionesButton.TabIndex = 66
        Me.insercionesButton.Text = "Inserciones"
        Me.insercionesButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.insercionesButton.UseVisualStyleBackColor = False
        '
        'destape_DevolucionFlecha
        '
        Me.destape_DevolucionFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.destape_DevolucionFlecha.ArrowWidth = 10
        Me.destape_DevolucionFlecha.BackColor = System.Drawing.Color.Transparent
        Me.destape_DevolucionFlecha.BorderColor = System.Drawing.Color.Black
        Me.destape_DevolucionFlecha.BorderWidth = CType(1, Byte)
        Me.destape_DevolucionFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.destape_DevolucionFlecha.FillColor = System.Drawing.Color.IndianRed
        Me.destape_DevolucionFlecha.Location = New System.Drawing.Point(629, 262)
        Me.destape_DevolucionFlecha.Name = "destape_DevolucionFlecha"
        Me.destape_DevolucionFlecha.Size = New System.Drawing.Size(42, 129)
        Me.destape_DevolucionFlecha.TabIndex = 61
        Me.destape_DevolucionFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN_RIGHT
        '
        'devolucionesButton
        '
        Me.devolucionesButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.devolucionesButton.BackColor = System.Drawing.Color.IndianRed
        Me.devolucionesButton.Enabled = False
        Me.devolucionesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.devolucionesButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.devolucionesButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Devoluciones
        Me.devolucionesButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.devolucionesButton.Location = New System.Drawing.Point(677, 337)
        Me.devolucionesButton.Name = "devolucionesButton"
        Me.devolucionesButton.Size = New System.Drawing.Size(110, 72)
        Me.devolucionesButton.TabIndex = 36
        Me.devolucionesButton.Text = "(F8) Devoluciones"
        Me.devolucionesButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.devolucionesButton.UseVisualStyleBackColor = False
        '
        'mesaControlImagenesButton
        '
        Me.mesaControlImagenesButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.mesaControlImagenesButton.BackColor = System.Drawing.Color.White
        Me.mesaControlImagenesButton.Enabled = False
        Me.mesaControlImagenesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.mesaControlImagenesButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mesaControlImagenesButton.ForeColor = System.Drawing.Color.Black
        Me.mesaControlImagenesButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.MesaImagenes
        Me.mesaControlImagenesButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.mesaControlImagenesButton.Location = New System.Drawing.Point(760, 142)
        Me.mesaControlImagenesButton.Name = "mesaControlImagenesButton"
        Me.mesaControlImagenesButton.Size = New System.Drawing.Size(110, 71)
        Me.mesaControlImagenesButton.TabIndex = 35
        Me.mesaControlImagenesButton.Text = "Mesa Imágenes (F5)"
        Me.mesaControlImagenesButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.mesaControlImagenesButton.UseVisualStyleBackColor = False
        '
        'centroD_CustodiaFlecha
        '
        Me.centroD_CustodiaFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.centroD_CustodiaFlecha.ArrowWidth = 10
        Me.centroD_CustodiaFlecha.BackColor = System.Drawing.Color.Transparent
        Me.centroD_CustodiaFlecha.BorderColor = System.Drawing.Color.Black
        Me.centroD_CustodiaFlecha.BorderWidth = CType(1, Byte)
        Me.centroD_CustodiaFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.centroD_CustodiaFlecha.FillColor = System.Drawing.Color.SteelBlue
        Me.centroD_CustodiaFlecha.Location = New System.Drawing.Point(934, 303)
        Me.centroD_CustodiaFlecha.Name = "centroD_CustodiaFlecha"
        Me.centroD_CustodiaFlecha.Size = New System.Drawing.Size(110, 41)
        Me.centroD_CustodiaFlecha.TabIndex = 34
        Me.centroD_CustodiaFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN
        '
        'destape_MesaFlecha
        '
        Me.destape_MesaFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.destape_MesaFlecha.ArrowWidth = 10
        Me.destape_MesaFlecha.BackColor = System.Drawing.Color.Transparent
        Me.destape_MesaFlecha.BorderColor = System.Drawing.Color.Black
        Me.destape_MesaFlecha.BorderWidth = CType(1, Byte)
        Me.destape_MesaFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.destape_MesaFlecha.FillColor = System.Drawing.Color.LightSteelBlue
        Me.destape_MesaFlecha.Location = New System.Drawing.Point(648, 131)
        Me.destape_MesaFlecha.Name = "destape_MesaFlecha"
        Me.destape_MesaFlecha.Size = New System.Drawing.Size(106, 59)
        Me.destape_MesaFlecha.TabIndex = 33
        Me.destape_MesaFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.UP_RIGHT
        '
        'ot_RecepcionFlecha
        '
        Me.ot_RecepcionFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ot_RecepcionFlecha.ArrowWidth = 10
        Me.ot_RecepcionFlecha.BackColor = System.Drawing.Color.Transparent
        Me.ot_RecepcionFlecha.BorderColor = System.Drawing.Color.Black
        Me.ot_RecepcionFlecha.BorderWidth = CType(1, Byte)
        Me.ot_RecepcionFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.ot_RecepcionFlecha.FillColor = System.Drawing.Color.LightSteelBlue
        Me.ot_RecepcionFlecha.Location = New System.Drawing.Point(401, 197)
        Me.ot_RecepcionFlecha.Name = "ot_RecepcionFlecha"
        Me.ot_RecepcionFlecha.Size = New System.Drawing.Size(35, 60)
        Me.ot_RecepcionFlecha.TabIndex = 30
        Me.ot_RecepcionFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'cargueUniversal_OTFlecha
        '
        Me.cargueUniversal_OTFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cargueUniversal_OTFlecha.ArrowWidth = 10
        Me.cargueUniversal_OTFlecha.BackColor = System.Drawing.Color.Transparent
        Me.cargueUniversal_OTFlecha.BorderColor = System.Drawing.Color.Black
        Me.cargueUniversal_OTFlecha.BorderWidth = CType(1, Byte)
        Me.cargueUniversal_OTFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.cargueUniversal_OTFlecha.FillColor = System.Drawing.Color.White
        Me.cargueUniversal_OTFlecha.Location = New System.Drawing.Point(288, 144)
        Me.cargueUniversal_OTFlecha.Name = "cargueUniversal_OTFlecha"
        Me.cargueUniversal_OTFlecha.Size = New System.Drawing.Size(110, 48)
        Me.cargueUniversal_OTFlecha.TabIndex = 29
        Me.cargueUniversal_OTFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN
        '
        'Actualizar_RecepcionFlecha
        '
        Me.Actualizar_RecepcionFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Actualizar_RecepcionFlecha.ArrowWidth = 10
        Me.Actualizar_RecepcionFlecha.BackColor = System.Drawing.Color.Transparent
        Me.Actualizar_RecepcionFlecha.BorderColor = System.Drawing.Color.Black
        Me.Actualizar_RecepcionFlecha.BorderWidth = CType(1, Byte)
        Me.Actualizar_RecepcionFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.Actualizar_RecepcionFlecha.FillColor = System.Drawing.Color.Khaki
        Me.Actualizar_RecepcionFlecha.Location = New System.Drawing.Point(463, 144)
        Me.Actualizar_RecepcionFlecha.Name = "Actualizar_RecepcionFlecha"
        Me.Actualizar_RecepcionFlecha.Size = New System.Drawing.Size(76, 49)
        Me.Actualizar_RecepcionFlecha.TabIndex = 27
        Me.Actualizar_RecepcionFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN
        '
        'custodiaButton
        '
        Me.custodiaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.custodiaButton.BackColor = System.Drawing.Color.SteelBlue
        Me.custodiaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.custodiaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.custodiaButton.ForeColor = System.Drawing.Color.White
        Me.custodiaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Custodia
        Me.custodiaButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.custodiaButton.Location = New System.Drawing.Point(934, 349)
        Me.custodiaButton.Name = "custodiaButton"
        Me.custodiaButton.Size = New System.Drawing.Size(110, 60)
        Me.custodiaButton.TabIndex = 26
        Me.custodiaButton.Text = "&Custodia"
        Me.custodiaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.custodiaButton.UseVisualStyleBackColor = False
        '
        'empaqueButton
        '
        Me.empaqueButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.empaqueButton.BackColor = System.Drawing.Color.LightSteelBlue
        Me.empaqueButton.Enabled = False
        Me.empaqueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.empaqueButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.empaqueButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Empaque
        Me.empaqueButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.empaqueButton.Location = New System.Drawing.Point(934, 117)
        Me.empaqueButton.Name = "empaqueButton"
        Me.empaqueButton.Size = New System.Drawing.Size(110, 60)
        Me.empaqueButton.TabIndex = 25
        Me.empaqueButton.Text = "Empaque (F6)"
        Me.empaqueButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.empaqueButton.UseVisualStyleBackColor = False
        '
        'mesaControlFisicoButton
        '
        Me.mesaControlFisicoButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.mesaControlFisicoButton.BackColor = System.Drawing.Color.LightSteelBlue
        Me.mesaControlFisicoButton.Enabled = False
        Me.mesaControlFisicoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.mesaControlFisicoButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mesaControlFisicoButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.MesaFisicos
        Me.mesaControlFisicoButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.mesaControlFisicoButton.Location = New System.Drawing.Point(761, 70)
        Me.mesaControlFisicoButton.Name = "mesaControlFisicoButton"
        Me.mesaControlFisicoButton.Size = New System.Drawing.Size(110, 68)
        Me.mesaControlFisicoButton.TabIndex = 24
        Me.mesaControlFisicoButton.Text = "Mesa Físicos (F4)"
        Me.mesaControlFisicoButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.mesaControlFisicoButton.UseVisualStyleBackColor = False
        '
        'actualizarButton
        '
        Me.actualizarButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.actualizarButton.BackColor = System.Drawing.Color.Khaki
        Me.actualizarButton.Enabled = False
        Me.actualizarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.actualizarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.actualizarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Autualizar
        Me.actualizarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.actualizarButton.Location = New System.Drawing.Point(442, 78)
        Me.actualizarButton.Name = "actualizarButton"
        Me.actualizarButton.Size = New System.Drawing.Size(110, 60)
        Me.actualizarButton.TabIndex = 23
        Me.actualizarButton.Text = "&Actualizar"
        Me.actualizarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.actualizarButton.UseVisualStyleBackColor = False
        '
        'destapeButton
        '
        Me.destapeButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.destapeButton.BackColor = System.Drawing.Color.LightSteelBlue
        Me.destapeButton.Enabled = False
        Me.destapeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.destapeButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.destapeButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Destape
        Me.destapeButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.destapeButton.Location = New System.Drawing.Point(601, 196)
        Me.destapeButton.Name = "destapeButton"
        Me.destapeButton.Size = New System.Drawing.Size(110, 60)
        Me.destapeButton.TabIndex = 22
        Me.destapeButton.Text = "&Destape (F3)"
        Me.destapeButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.destapeButton.UseVisualStyleBackColor = False
        '
        'recepcionButton
        '
        Me.recepcionButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.recepcionButton.BackColor = System.Drawing.Color.LightSteelBlue
        Me.recepcionButton.Enabled = False
        Me.recepcionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.recepcionButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.recepcionButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Recepcion
        Me.recepcionButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.recepcionButton.Location = New System.Drawing.Point(442, 196)
        Me.recepcionButton.Name = "recepcionButton"
        Me.recepcionButton.Size = New System.Drawing.Size(110, 60)
        Me.recepcionButton.TabIndex = 21
        Me.recepcionButton.Text = "&Recepción (F2)"
        Me.recepcionButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.recepcionButton.UseVisualStyleBackColor = False
        '
        'cargueUniversalButton
        '
        Me.cargueUniversalButton.AccessibleDescription = ""
        Me.cargueUniversalButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cargueUniversalButton.BackColor = System.Drawing.Color.White
        Me.cargueUniversalButton.Enabled = False
        Me.cargueUniversalButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cargueUniversalButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cargueUniversalButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.CargueUniversal
        Me.cargueUniversalButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cargueUniversalButton.Location = New System.Drawing.Point(285, 78)
        Me.cargueUniversalButton.Name = "cargueUniversalButton"
        Me.cargueUniversalButton.Size = New System.Drawing.Size(113, 60)
        Me.cargueUniversalButton.TabIndex = 18
        Me.cargueUniversalButton.Tag = "Ctrl + U"
        Me.cargueUniversalButton.Text = "Cargue Universal"
        Me.cargueUniversalButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cargueUniversalButton.UseVisualStyleBackColor = False
        '
        'mesa_EmpaqueFlecha
        '
        Me.mesa_EmpaqueFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.mesa_EmpaqueFlecha.ArrowWidth = 10
        Me.mesa_EmpaqueFlecha.BackColor = System.Drawing.Color.Transparent
        Me.mesa_EmpaqueFlecha.BorderColor = System.Drawing.Color.Black
        Me.mesa_EmpaqueFlecha.BorderWidth = CType(1, Byte)
        Me.mesa_EmpaqueFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.mesa_EmpaqueFlecha.FillColor = System.Drawing.Color.LightSteelBlue
        Me.mesa_EmpaqueFlecha.Location = New System.Drawing.Point(877, 79)
        Me.mesa_EmpaqueFlecha.Name = "mesa_EmpaqueFlecha"
        Me.mesa_EmpaqueFlecha.Size = New System.Drawing.Size(51, 126)
        Me.mesa_EmpaqueFlecha.TabIndex = 32
        Me.mesa_EmpaqueFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'otButton
        '
        Me.otButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.otButton.BackColor = System.Drawing.Color.LightSteelBlue
        Me.otButton.Enabled = False
        Me.otButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.otButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.otButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.OT
        Me.otButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.otButton.Location = New System.Drawing.Point(288, 196)
        Me.otButton.Name = "otButton"
        Me.otButton.Size = New System.Drawing.Size(110, 60)
        Me.otButton.TabIndex = 20
        Me.otButton.Text = "&O.T."
        Me.otButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.otButton.UseVisualStyleBackColor = False
        '
        'cargueParcialButton
        '
        Me.cargueParcialButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cargueParcialButton.BackColor = System.Drawing.Color.LightSteelBlue
        Me.cargueParcialButton.Enabled = False
        Me.cargueParcialButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cargueParcialButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cargueParcialButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.CargueParcial
        Me.cargueParcialButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cargueParcialButton.Location = New System.Drawing.Point(288, 196)
        Me.cargueParcialButton.Name = "cargueParcialButton"
        Me.cargueParcialButton.Size = New System.Drawing.Size(110, 60)
        Me.cargueParcialButton.TabIndex = 19
        Me.cargueParcialButton.Text = "Cargue &Parcial"
        Me.cargueParcialButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cargueParcialButton.UseVisualStyleBackColor = False
        '
        'MainTabControl
        '
        Me.MainTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.MainTabControl.Controls.Add(Me.ProcesoTabPage)
        Me.MainTabControl.Controls.Add(Me.FisicosTabPage)
        Me.MainTabControl.Controls.Add(Me.BusquedaTabPage)
        Me.MainTabControl.Controls.Add(Me.InformesTabPage)
        Me.MainTabControl.Controls.Add(Me.ProyectoDecevalTabPage)
        Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTabControl.ImageList = Me.TabControlImageList
        Me.MainTabControl.Location = New System.Drawing.Point(0, 24)
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        Me.MainTabControl.Size = New System.Drawing.Size(1345, 454)
        Me.MainTabControl.TabIndex = 2
        '
        'ProcesoTabPage
        '
        Me.ProcesoTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.ProcesoTabPage.Controls.Add(Me.WSPanel)
        Me.ProcesoTabPage.ImageIndex = 0
        Me.ProcesoTabPage.Location = New System.Drawing.Point(4, 26)
        Me.ProcesoTabPage.Name = "ProcesoTabPage"
        Me.ProcesoTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ProcesoTabPage.Size = New System.Drawing.Size(1337, 424)
        Me.ProcesoTabPage.TabIndex = 0
        Me.ProcesoTabPage.Text = "Proceso"
        '
        'FisicosTabPage
        '
        Me.FisicosTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.FisicosTabPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.FisicosTabPage.Controls.Add(Me.MesasFlowLayoutPanel)
        Me.FisicosTabPage.ImageIndex = 2
        Me.FisicosTabPage.Location = New System.Drawing.Point(4, 26)
        Me.FisicosTabPage.Name = "FisicosTabPage"
        Me.FisicosTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.FisicosTabPage.Size = New System.Drawing.Size(1337, 424)
        Me.FisicosTabPage.TabIndex = 2
        Me.FisicosTabPage.Text = "Mesa Control Físicos"
        '
        'MesasFlowLayoutPanel
        '
        Me.MesasFlowLayoutPanel.Controls.Add(Me.Empaque_CentroDMesaFlecha)
        Me.MesasFlowLayoutPanel.Controls.Add(Me.CentroDistribucionMesaButton)
        Me.MesasFlowLayoutPanel.Controls.Add(Me.Primera_DevolucionesFlecha)
        Me.MesasFlowLayoutPanel.Controls.Add(Me.Segunda_TerceraFlecha)
        Me.MesasFlowLayoutPanel.Controls.Add(Me.Tercera_EmpaqueFlecha)
        Me.MesasFlowLayoutPanel.Controls.Add(Me.Segunda_EmpaqueFlecha)
        Me.MesasFlowLayoutPanel.Controls.Add(Me.Primera_EmpaqueFlecha)
        Me.MesasFlowLayoutPanel.Controls.Add(Me.Empaque2Button)
        Me.MesasFlowLayoutPanel.Controls.Add(Me.Primera_SegundaFlecha)
        Me.MesasFlowLayoutPanel.Controls.Add(Me.FisicoTerceraCapturaButton)
        Me.MesasFlowLayoutPanel.Controls.Add(Me.FisicoSegundaCapturaButton)
        Me.MesasFlowLayoutPanel.Controls.Add(Me.DevolucionesMCButton)
        Me.MesasFlowLayoutPanel.Controls.Add(Me.FisicoPrimeraCapturaButton)
        Me.MesasFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MesasFlowLayoutPanel.Location = New System.Drawing.Point(3, 3)
        Me.MesasFlowLayoutPanel.Name = "MesasFlowLayoutPanel"
        Me.MesasFlowLayoutPanel.Size = New System.Drawing.Size(1327, 414)
        Me.MesasFlowLayoutPanel.TabIndex = 0
        '
        'Empaque_CentroDMesaFlecha
        '
        Me.Empaque_CentroDMesaFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Empaque_CentroDMesaFlecha.ArrowWidth = 10
        Me.Empaque_CentroDMesaFlecha.BackColor = System.Drawing.Color.Transparent
        Me.Empaque_CentroDMesaFlecha.BorderColor = System.Drawing.Color.Black
        Me.Empaque_CentroDMesaFlecha.BorderWidth = CType(1, Byte)
        Me.Empaque_CentroDMesaFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.Empaque_CentroDMesaFlecha.FillColor = System.Drawing.Color.LightSteelBlue
        Me.Empaque_CentroDMesaFlecha.Location = New System.Drawing.Point(900, 220)
        Me.Empaque_CentroDMesaFlecha.Name = "Empaque_CentroDMesaFlecha"
        Me.Empaque_CentroDMesaFlecha.Size = New System.Drawing.Size(72, 40)
        Me.Empaque_CentroDMesaFlecha.TabIndex = 90
        Me.Empaque_CentroDMesaFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.UP
        '
        'CentroDistribucionMesaButton
        '
        Me.CentroDistribucionMesaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CentroDistribucionMesaButton.BackColor = System.Drawing.Color.White
        Me.CentroDistribucionMesaButton.Enabled = False
        Me.CentroDistribucionMesaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CentroDistribucionMesaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CentroDistribucionMesaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.CentroDistribucion
        Me.CentroDistribucionMesaButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CentroDistribucionMesaButton.Location = New System.Drawing.Point(877, 137)
        Me.CentroDistribucionMesaButton.Name = "CentroDistribucionMesaButton"
        Me.CentroDistribucionMesaButton.Size = New System.Drawing.Size(113, 77)
        Me.CentroDistribucionMesaButton.TabIndex = 89
        Me.CentroDistribucionMesaButton.Text = "(F7) Distribución"
        Me.CentroDistribucionMesaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CentroDistribucionMesaButton.UseVisualStyleBackColor = False
        '
        'Primera_DevolucionesFlecha
        '
        Me.Primera_DevolucionesFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Primera_DevolucionesFlecha.ArrowWidth = 10
        Me.Primera_DevolucionesFlecha.BackColor = System.Drawing.Color.Transparent
        Me.Primera_DevolucionesFlecha.BorderColor = System.Drawing.Color.Black
        Me.Primera_DevolucionesFlecha.BorderWidth = CType(1, Byte)
        Me.Primera_DevolucionesFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.Primera_DevolucionesFlecha.FillColor = System.Drawing.Color.IndianRed
        Me.Primera_DevolucionesFlecha.Location = New System.Drawing.Point(384, 57)
        Me.Primera_DevolucionesFlecha.Name = "Primera_DevolucionesFlecha"
        Me.Primera_DevolucionesFlecha.Size = New System.Drawing.Size(490, 74)
        Me.Primera_DevolucionesFlecha.TabIndex = 88
        Me.Primera_DevolucionesFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.UP_RIGHT
        '
        'Segunda_TerceraFlecha
        '
        Me.Segunda_TerceraFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Segunda_TerceraFlecha.ArrowWidth = 10
        Me.Segunda_TerceraFlecha.BackColor = System.Drawing.Color.Transparent
        Me.Segunda_TerceraFlecha.BorderColor = System.Drawing.Color.Black
        Me.Segunda_TerceraFlecha.BorderWidth = CType(1, Byte)
        Me.Segunda_TerceraFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.Segunda_TerceraFlecha.FillColor = System.Drawing.Color.IndianRed
        Me.Segunda_TerceraFlecha.Location = New System.Drawing.Point(625, 149)
        Me.Segunda_TerceraFlecha.Name = "Segunda_TerceraFlecha"
        Me.Segunda_TerceraFlecha.Size = New System.Drawing.Size(37, 60)
        Me.Segunda_TerceraFlecha.TabIndex = 87
        Me.Segunda_TerceraFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'Tercera_EmpaqueFlecha
        '
        Me.Tercera_EmpaqueFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Tercera_EmpaqueFlecha.ArrowWidth = 10
        Me.Tercera_EmpaqueFlecha.BackColor = System.Drawing.Color.Transparent
        Me.Tercera_EmpaqueFlecha.BorderColor = System.Drawing.Color.Black
        Me.Tercera_EmpaqueFlecha.BorderWidth = CType(1, Byte)
        Me.Tercera_EmpaqueFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.Tercera_EmpaqueFlecha.FillColor = System.Drawing.Color.LightSteelBlue
        Me.Tercera_EmpaqueFlecha.Location = New System.Drawing.Point(717, 232)
        Me.Tercera_EmpaqueFlecha.Name = "Tercera_EmpaqueFlecha"
        Me.Tercera_EmpaqueFlecha.Size = New System.Drawing.Size(157, 76)
        Me.Tercera_EmpaqueFlecha.TabIndex = 86
        Me.Tercera_EmpaqueFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN_RIGHT
        '
        'Segunda_EmpaqueFlecha
        '
        Me.Segunda_EmpaqueFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Segunda_EmpaqueFlecha.ArrowWidth = 10
        Me.Segunda_EmpaqueFlecha.BackColor = System.Drawing.Color.Transparent
        Me.Segunda_EmpaqueFlecha.BorderColor = System.Drawing.Color.Black
        Me.Segunda_EmpaqueFlecha.BorderWidth = CType(1, Byte)
        Me.Segunda_EmpaqueFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.Segunda_EmpaqueFlecha.FillColor = System.Drawing.Color.LightSteelBlue
        Me.Segunda_EmpaqueFlecha.Location = New System.Drawing.Point(545, 232)
        Me.Segunda_EmpaqueFlecha.Name = "Segunda_EmpaqueFlecha"
        Me.Segunda_EmpaqueFlecha.Size = New System.Drawing.Size(166, 76)
        Me.Segunda_EmpaqueFlecha.TabIndex = 85
        Me.Segunda_EmpaqueFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN_RIGHT
        '
        'Primera_EmpaqueFlecha
        '
        Me.Primera_EmpaqueFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Primera_EmpaqueFlecha.ArrowWidth = 10
        Me.Primera_EmpaqueFlecha.BackColor = System.Drawing.Color.Transparent
        Me.Primera_EmpaqueFlecha.BorderColor = System.Drawing.Color.Black
        Me.Primera_EmpaqueFlecha.BorderWidth = CType(1, Byte)
        Me.Primera_EmpaqueFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.Primera_EmpaqueFlecha.FillColor = System.Drawing.Color.LightSteelBlue
        Me.Primera_EmpaqueFlecha.Location = New System.Drawing.Point(384, 232)
        Me.Primera_EmpaqueFlecha.Name = "Primera_EmpaqueFlecha"
        Me.Primera_EmpaqueFlecha.Size = New System.Drawing.Size(155, 76)
        Me.Primera_EmpaqueFlecha.TabIndex = 84
        Me.Primera_EmpaqueFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN_RIGHT
        '
        'Empaque2Button
        '
        Me.Empaque2Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Empaque2Button.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Empaque2Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Empaque2Button.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Empaque2Button.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Empaque
        Me.Empaque2Button.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Empaque2Button.Location = New System.Drawing.Point(877, 266)
        Me.Empaque2Button.Name = "Empaque2Button"
        Me.Empaque2Button.Size = New System.Drawing.Size(110, 60)
        Me.Empaque2Button.TabIndex = 83
        Me.Empaque2Button.Text = "(F6) Empaque"
        Me.Empaque2Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Empaque2Button.UseVisualStyleBackColor = False
        '
        'Primera_SegundaFlecha
        '
        Me.Primera_SegundaFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Primera_SegundaFlecha.ArrowWidth = 10
        Me.Primera_SegundaFlecha.BackColor = System.Drawing.Color.Transparent
        Me.Primera_SegundaFlecha.BorderColor = System.Drawing.Color.Black
        Me.Primera_SegundaFlecha.BorderWidth = CType(1, Byte)
        Me.Primera_SegundaFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.Primera_SegundaFlecha.FillColor = System.Drawing.Color.Khaki
        Me.Primera_SegundaFlecha.Location = New System.Drawing.Point(450, 149)
        Me.Primera_SegundaFlecha.Name = "Primera_SegundaFlecha"
        Me.Primera_SegundaFlecha.Size = New System.Drawing.Size(37, 60)
        Me.Primera_SegundaFlecha.TabIndex = 82
        Me.Primera_SegundaFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'FisicoTerceraCapturaButton
        '
        Me.FisicoTerceraCapturaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FisicoTerceraCapturaButton.BackColor = System.Drawing.Color.IndianRed
        Me.FisicoTerceraCapturaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.FisicoTerceraCapturaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FisicoTerceraCapturaButton.ForeColor = System.Drawing.Color.White
        Me.FisicoTerceraCapturaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Terceracaptura
        Me.FisicoTerceraCapturaButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.FisicoTerceraCapturaButton.Location = New System.Drawing.Point(668, 137)
        Me.FisicoTerceraCapturaButton.Name = "FisicoTerceraCapturaButton"
        Me.FisicoTerceraCapturaButton.Size = New System.Drawing.Size(114, 77)
        Me.FisicoTerceraCapturaButton.TabIndex = 2
        Me.FisicoTerceraCapturaButton.Text = "(Ctrl + F4) Tercera Captura"
        Me.FisicoTerceraCapturaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.FisicoTerceraCapturaButton.UseVisualStyleBackColor = False
        '
        'FisicoSegundaCapturaButton
        '
        Me.FisicoSegundaCapturaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FisicoSegundaCapturaButton.BackColor = System.Drawing.Color.Khaki
        Me.FisicoSegundaCapturaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.FisicoSegundaCapturaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FisicoSegundaCapturaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Doblecaptura
        Me.FisicoSegundaCapturaButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.FisicoSegundaCapturaButton.Location = New System.Drawing.Point(493, 137)
        Me.FisicoSegundaCapturaButton.Name = "FisicoSegundaCapturaButton"
        Me.FisicoSegundaCapturaButton.Size = New System.Drawing.Size(118, 77)
        Me.FisicoSegundaCapturaButton.TabIndex = 1
        Me.FisicoSegundaCapturaButton.Text = "(Ctrl + F3) Segunda Captura"
        Me.FisicoSegundaCapturaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.FisicoSegundaCapturaButton.UseVisualStyleBackColor = False
        '
        'DevolucionesMCButton
        '
        Me.DevolucionesMCButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.DevolucionesMCButton.BackColor = System.Drawing.Color.IndianRed
        Me.DevolucionesMCButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DevolucionesMCButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DevolucionesMCButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Devoluciones
        Me.DevolucionesMCButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.DevolucionesMCButton.Location = New System.Drawing.Point(880, 42)
        Me.DevolucionesMCButton.Name = "DevolucionesMCButton"
        Me.DevolucionesMCButton.Size = New System.Drawing.Size(110, 70)
        Me.DevolucionesMCButton.TabIndex = 79
        Me.DevolucionesMCButton.Text = "(F8) Devoluciones"
        Me.DevolucionesMCButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.DevolucionesMCButton.UseVisualStyleBackColor = False
        '
        'FisicoPrimeraCapturaButton
        '
        Me.FisicoPrimeraCapturaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FisicoPrimeraCapturaButton.BackColor = System.Drawing.Color.LightSteelBlue
        Me.FisicoPrimeraCapturaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.FisicoPrimeraCapturaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FisicoPrimeraCapturaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.MesaFisicos
        Me.FisicoPrimeraCapturaButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.FisicoPrimeraCapturaButton.Location = New System.Drawing.Point(329, 137)
        Me.FisicoPrimeraCapturaButton.Name = "FisicoPrimeraCapturaButton"
        Me.FisicoPrimeraCapturaButton.Size = New System.Drawing.Size(115, 77)
        Me.FisicoPrimeraCapturaButton.TabIndex = 0
        Me.FisicoPrimeraCapturaButton.Text = "(Ctrl + F2) Primera Captura"
        Me.FisicoPrimeraCapturaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.FisicoPrimeraCapturaButton.UseVisualStyleBackColor = False
        '
        'BusquedaTabPage
        '
        Me.BusquedaTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.BusquedaTabPage.Controls.Add(Me.DataPanel)
        Me.BusquedaTabPage.Controls.Add(Me.DetallePanel)
        Me.BusquedaTabPage.Controls.Add(Me.BusquedaSplitContainer)
        Me.BusquedaTabPage.Controls.Add(Me.BusquedaGroupBox)
        Me.BusquedaTabPage.ImageIndex = 3
        Me.BusquedaTabPage.Location = New System.Drawing.Point(4, 26)
        Me.BusquedaTabPage.Name = "BusquedaTabPage"
        Me.BusquedaTabPage.Size = New System.Drawing.Size(1337, 424)
        Me.BusquedaTabPage.TabIndex = 3
        Me.BusquedaTabPage.Text = "(F11) Búsqueda"
        '
        'DataPanel
        '
        Me.DataPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataPanel.BackColor = System.Drawing.Color.Transparent
        Me.DataPanel.Controls.Add(Me.CerraDataButton)
        Me.DataPanel.Controls.Add(Me.DataAsociadaDataGridView)
        Me.DataPanel.Location = New System.Drawing.Point(286, 131)
        Me.DataPanel.Name = "DataPanel"
        Me.DataPanel.Size = New System.Drawing.Size(766, 112)
        Me.DataPanel.TabIndex = 8
        Me.DataPanel.Visible = False
        '
        'CerraDataButton
        '
        Me.CerraDataButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CerraDataButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CerraDataButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
        Me.CerraDataButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CerraDataButton.Location = New System.Drawing.Point(688, 88)
        Me.CerraDataButton.Name = "CerraDataButton"
        Me.CerraDataButton.Size = New System.Drawing.Size(75, 23)
        Me.CerraDataButton.TabIndex = 1
        Me.CerraDataButton.Text = "&Cerrar"
        Me.CerraDataButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CerraDataButton.UseVisualStyleBackColor = True
        '
        'DataAsociadaDataGridView
        '
        Me.DataAsociadaDataGridView.AllowUserToAddRows = False
        Me.DataAsociadaDataGridView.AllowUserToDeleteRows = False
        Me.DataAsociadaDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataAsociadaDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataAsociadaDataGridView.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataAsociadaDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataAsociadaDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataAsociadaDataGridView.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataAsociadaDataGridView.GridColor = System.Drawing.SystemColors.Control
        Me.DataAsociadaDataGridView.Location = New System.Drawing.Point(3, 3)
        Me.DataAsociadaDataGridView.MultiSelect = False
        Me.DataAsociadaDataGridView.Name = "DataAsociadaDataGridView"
        Me.DataAsociadaDataGridView.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataAsociadaDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataAsociadaDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DataAsociadaDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataAsociadaDataGridView.Size = New System.Drawing.Size(760, 84)
        Me.DataAsociadaDataGridView.TabIndex = 0
        '
        'DetallePanel
        '
        Me.DetallePanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DetallePanel.BackColor = System.Drawing.Color.Transparent
        Me.DetallePanel.Controls.Add(Me.CerrarButton)
        Me.DetallePanel.Controls.Add(Me.DetalleDataGridView)
        Me.DetallePanel.Location = New System.Drawing.Point(155, 77)
        Me.DetallePanel.Name = "DetallePanel"
        Me.DetallePanel.Size = New System.Drawing.Size(1028, 206)
        Me.DetallePanel.TabIndex = 6
        Me.DetallePanel.Visible = False
        '
        'CerrarButton
        '
        Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CerrarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
        Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CerrarButton.Location = New System.Drawing.Point(950, 182)
        Me.CerrarButton.Name = "CerrarButton"
        Me.CerrarButton.Size = New System.Drawing.Size(75, 23)
        Me.CerrarButton.TabIndex = 1
        Me.CerrarButton.Text = "&Cerrar"
        Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CerrarButton.UseVisualStyleBackColor = True
        '
        'DetalleDataGridView
        '
        Me.DetalleDataGridView.AllowUserToAddRows = False
        Me.DetalleDataGridView.AllowUserToDeleteRows = False
        Me.DetalleDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DetalleDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DetalleDataGridView.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DetalleDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DetalleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DetalleDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CBarras, Me.Fecha_Log, Me.Estado_Log, Me.CajaProceso, Me.CajaCustodia, Me.Usuario})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DetalleDataGridView.DefaultCellStyle = DataGridViewCellStyle6
        Me.DetalleDataGridView.GridColor = System.Drawing.SystemColors.Control
        Me.DetalleDataGridView.Location = New System.Drawing.Point(3, 3)
        Me.DetalleDataGridView.MultiSelect = False
        Me.DetalleDataGridView.Name = "DetalleDataGridView"
        Me.DetalleDataGridView.ReadOnly = True
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DetalleDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.DetalleDataGridView.RowHeadersVisible = False
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DetalleDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.DetalleDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DetalleDataGridView.Size = New System.Drawing.Size(1022, 178)
        Me.DetalleDataGridView.TabIndex = 0
        '
        'CBarras
        '
        Me.CBarras.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.CBarras.DataPropertyName = "CBarras"
        Me.CBarras.HeaderText = "CBarras"
        Me.CBarras.Name = "CBarras"
        Me.CBarras.ReadOnly = True
        '
        'Fecha_Log
        '
        Me.Fecha_Log.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Fecha_Log.DataPropertyName = "Fecha_Log"
        Me.Fecha_Log.HeaderText = "Fecha"
        Me.Fecha_Log.Name = "Fecha_Log"
        Me.Fecha_Log.ReadOnly = True
        '
        'Estado_Log
        '
        Me.Estado_Log.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Estado_Log.DataPropertyName = "Nombre_Estado"
        Me.Estado_Log.HeaderText = "Estado"
        Me.Estado_Log.Name = "Estado_Log"
        Me.Estado_Log.ReadOnly = True
        '
        'CajaProceso
        '
        Me.CajaProceso.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.CajaProceso.DataPropertyName = "fk_Caja_Proceso"
        Me.CajaProceso.HeaderText = "Caja Proceso"
        Me.CajaProceso.Name = "CajaProceso"
        Me.CajaProceso.ReadOnly = True
        '
        'CajaCustodia
        '
        Me.CajaCustodia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.CajaCustodia.DataPropertyName = "Codigo_Caja"
        Me.CajaCustodia.HeaderText = "Caja Custodia"
        Me.CajaCustodia.Name = "CajaCustodia"
        Me.CajaCustodia.ReadOnly = True
        '
        'Usuario
        '
        Me.Usuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Usuario.DataPropertyName = "Nombres"
        Me.Usuario.HeaderText = "Usuario"
        Me.Usuario.Name = "Usuario"
        Me.Usuario.ReadOnly = True
        '
        'BusquedaSplitContainer
        '
        Me.BusquedaSplitContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BusquedaSplitContainer.Location = New System.Drawing.Point(8, 81)
        Me.BusquedaSplitContainer.Name = "BusquedaSplitContainer"
        Me.BusquedaSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'BusquedaSplitContainer.Panel1
        '
        Me.BusquedaSplitContainer.Panel1.Controls.Add(Me.FoldersGroupBox)
        '
        'BusquedaSplitContainer.Panel2
        '
        Me.BusquedaSplitContainer.Panel2.Controls.Add(Me.FileDataSplitContainer)
        Me.BusquedaSplitContainer.Size = New System.Drawing.Size(1321, 285)
        Me.BusquedaSplitContainer.SplitterDistance = 135
        Me.BusquedaSplitContainer.TabIndex = 5
        '
        'FoldersGroupBox
        '
        Me.FoldersGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FoldersGroupBox.Controls.Add(Me.RemoveFolderButton)
        Me.FoldersGroupBox.Controls.Add(Me.FoldersDataGridView)
        Me.FoldersGroupBox.Location = New System.Drawing.Point(7, 3)
        Me.FoldersGroupBox.Name = "FoldersGroupBox"
        Me.FoldersGroupBox.Size = New System.Drawing.Size(1314, 129)
        Me.FoldersGroupBox.TabIndex = 5
        Me.FoldersGroupBox.TabStop = False
        Me.FoldersGroupBox.Text = "Folders"
        '
        'RemoveFolderButton
        '
        Me.RemoveFolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RemoveFolderButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Remove
        Me.RemoveFolderButton.Location = New System.Drawing.Point(1274, 92)
        Me.RemoveFolderButton.Name = "RemoveFolderButton"
        Me.RemoveFolderButton.Size = New System.Drawing.Size(34, 31)
        Me.RemoveFolderButton.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.RemoveFolderButton, "Eliminar carpeta")
        Me.RemoveFolderButton.UseVisualStyleBackColor = True
        '
        'FoldersDataGridView
        '
        Me.FoldersDataGridView.AllowUserToAddRows = False
        Me.FoldersDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FoldersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.FoldersDataGridView.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FoldersDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.FoldersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FoldersDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CBarras_Folder, Me.Estado, Me.Entidad, Me.Proyecto, Me.Esquema, Me.Data1, Me.Data2, Me.Data3, Me.Llaves})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.FoldersDataGridView.DefaultCellStyle = DataGridViewCellStyle10
        Me.FoldersDataGridView.GridColor = System.Drawing.SystemColors.Control
        Me.FoldersDataGridView.Location = New System.Drawing.Point(7, 20)
        Me.FoldersDataGridView.MultiSelect = False
        Me.FoldersDataGridView.Name = "FoldersDataGridView"
        Me.FoldersDataGridView.ReadOnly = True
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FoldersDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.FoldersDataGridView.RowHeadersVisible = False
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FoldersDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.FoldersDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.FoldersDataGridView.Size = New System.Drawing.Size(1261, 103)
        Me.FoldersDataGridView.TabIndex = 1
        '
        'CBarras_Folder
        '
        Me.CBarras_Folder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.CBarras_Folder.DataPropertyName = "CBarras_Folder"
        Me.CBarras_Folder.HeaderText = "Carpeta"
        Me.CBarras_Folder.Name = "CBarras_Folder"
        Me.CBarras_Folder.ReadOnly = True
        '
        'Estado
        '
        Me.Estado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Estado.DataPropertyName = "Nombre_Estado"
        Me.Estado.HeaderText = "Estado"
        Me.Estado.Name = "Estado"
        Me.Estado.ReadOnly = True
        '
        'Entidad
        '
        Me.Entidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Entidad.DataPropertyName = "Nombre_Entidad"
        Me.Entidad.HeaderText = "Entidad"
        Me.Entidad.Name = "Entidad"
        Me.Entidad.ReadOnly = True
        '
        'Proyecto
        '
        Me.Proyecto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Proyecto.DataPropertyName = "Nombre_Proyecto"
        Me.Proyecto.HeaderText = "Proyecto"
        Me.Proyecto.Name = "Proyecto"
        Me.Proyecto.ReadOnly = True
        '
        'Esquema
        '
        Me.Esquema.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Esquema.DataPropertyName = "Nombre_Esquema"
        Me.Esquema.HeaderText = "Esquema"
        Me.Esquema.Name = "Esquema"
        Me.Esquema.ReadOnly = True
        '
        'Data1
        '
        Me.Data1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Data1.DataPropertyName = "Data_1"
        Me.Data1.HeaderText = "Data1"
        Me.Data1.Name = "Data1"
        Me.Data1.ReadOnly = True
        '
        'Data2
        '
        Me.Data2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Data2.DataPropertyName = "Data_2"
        Me.Data2.HeaderText = "Data2"
        Me.Data2.Name = "Data2"
        Me.Data2.ReadOnly = True
        '
        'Data3
        '
        Me.Data3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Data3.DataPropertyName = "Data_3"
        Me.Data3.HeaderText = "Data3"
        Me.Data3.Name = "Data3"
        Me.Data3.ReadOnly = True
        '
        'Llaves
        '
        Me.Llaves.DataPropertyName = "Llaves"
        Me.Llaves.HeaderText = "Llaves"
        Me.Llaves.Name = "Llaves"
        Me.Llaves.ReadOnly = True
        Me.Llaves.Visible = False
        Me.Llaves.Width = 68
        '
        'FileDataSplitContainer
        '
        Me.FileDataSplitContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FileDataSplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.FileDataSplitContainer.Name = "FileDataSplitContainer"
        '
        'FileDataSplitContainer.Panel1
        '
        Me.FileDataSplitContainer.Panel1.Controls.Add(Me.FilesGroupBox)
        '
        'FileDataSplitContainer.Panel2
        '
        Me.FileDataSplitContainer.Panel2.Controls.Add(Me.SplitContainer1)
        Me.FileDataSplitContainer.Size = New System.Drawing.Size(1321, 146)
        Me.FileDataSplitContainer.SplitterDistance = 520
        Me.FileDataSplitContainer.TabIndex = 0
        '
        'FilesGroupBox
        '
        Me.FilesGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FilesGroupBox.Controls.Add(Me.CambiaDocumentoButton)
        Me.FilesGroupBox.Controls.Add(Me.FilesDataGridView)
        Me.FilesGroupBox.Controls.Add(Me.RemoveFileButton)
        Me.FilesGroupBox.Location = New System.Drawing.Point(7, 0)
        Me.FilesGroupBox.Name = "FilesGroupBox"
        Me.FilesGroupBox.Size = New System.Drawing.Size(510, 146)
        Me.FilesGroupBox.TabIndex = 11
        Me.FilesGroupBox.TabStop = False
        Me.FilesGroupBox.Text = "Files"
        '
        'CambiaDocumentoButton
        '
        Me.CambiaDocumentoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CambiaDocumentoButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.CangeDocument
        Me.CambiaDocumentoButton.Location = New System.Drawing.Point(470, 72)
        Me.CambiaDocumentoButton.Name = "CambiaDocumentoButton"
        Me.CambiaDocumentoButton.Size = New System.Drawing.Size(34, 31)
        Me.CambiaDocumentoButton.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.CambiaDocumentoButton, "Ver tabla asociada")
        Me.CambiaDocumentoButton.UseVisualStyleBackColor = True
        '
        'FilesDataGridView
        '
        Me.FilesDataGridView.AllowUserToAddRows = False
        Me.FilesDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FilesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.FilesDataGridView.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FilesDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.FilesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FilesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CBarras_File, Me.Monto_File, Me.Folios_File, Me.Nombre_Tipologia, Me.CBarrasFolder, Me.fk_Documento, Me.fk_Log_cargue})
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.FilesDataGridView.DefaultCellStyle = DataGridViewCellStyle14
        Me.FilesDataGridView.GridColor = System.Drawing.SystemColors.Control
        Me.FilesDataGridView.Location = New System.Drawing.Point(7, 20)
        Me.FilesDataGridView.MultiSelect = False
        Me.FilesDataGridView.Name = "FilesDataGridView"
        Me.FilesDataGridView.ReadOnly = True
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FilesDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle15
        Me.FilesDataGridView.RowHeadersVisible = False
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FilesDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle16
        Me.FilesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.FilesDataGridView.Size = New System.Drawing.Size(457, 120)
        Me.FilesDataGridView.TabIndex = 4
        '
        'CBarras_File
        '
        Me.CBarras_File.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.CBarras_File.DataPropertyName = "CBarras_File"
        Me.CBarras_File.HeaderText = "Documento"
        Me.CBarras_File.Name = "CBarras_File"
        Me.CBarras_File.ReadOnly = True
        Me.CBarras_File.Width = 97
        '
        'Monto_File
        '
        Me.Monto_File.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Monto_File.DataPropertyName = "Monto_File"
        Me.Monto_File.HeaderText = "Monto"
        Me.Monto_File.Name = "Monto_File"
        Me.Monto_File.ReadOnly = True
        Me.Monto_File.Width = 68
        '
        'Folios_File
        '
        Me.Folios_File.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Folios_File.DataPropertyName = "Folios_File"
        Me.Folios_File.HeaderText = "Folios"
        Me.Folios_File.Name = "Folios_File"
        Me.Folios_File.ReadOnly = True
        Me.Folios_File.Width = 64
        '
        'Nombre_Tipologia
        '
        Me.Nombre_Tipologia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Nombre_Tipologia.DataPropertyName = "Nombre_Tipologia"
        Me.Nombre_Tipologia.HeaderText = "Tipología"
        Me.Nombre_Tipologia.Name = "Nombre_Tipologia"
        Me.Nombre_Tipologia.ReadOnly = True
        Me.Nombre_Tipologia.Width = 83
        '
        'CBarrasFolder
        '
        Me.CBarrasFolder.DataPropertyName = "CBarras_Folder"
        Me.CBarrasFolder.HeaderText = "CBarrasFolder"
        Me.CBarrasFolder.Name = "CBarrasFolder"
        Me.CBarrasFolder.ReadOnly = True
        Me.CBarrasFolder.Visible = False
        Me.CBarrasFolder.Width = 111
        '
        'fk_Documento
        '
        Me.fk_Documento.DataPropertyName = "fk_Documento"
        Me.fk_Documento.HeaderText = "fk_Documento"
        Me.fk_Documento.Name = "fk_Documento"
        Me.fk_Documento.ReadOnly = True
        Me.fk_Documento.Visible = False
        Me.fk_Documento.Width = 115
        '
        'fk_Log_cargue
        '
        Me.fk_Log_cargue.DataPropertyName = "fk_Log_cargue"
        Me.fk_Log_cargue.HeaderText = "fk_Log_cargue"
        Me.fk_Log_cargue.Name = "fk_Log_cargue"
        Me.fk_Log_cargue.ReadOnly = True
        Me.fk_Log_cargue.Visible = False
        Me.fk_Log_cargue.Width = 116
        '
        'RemoveFileButton
        '
        Me.RemoveFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RemoveFileButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Remove
        Me.RemoveFileButton.Location = New System.Drawing.Point(470, 109)
        Me.RemoveFileButton.Name = "RemoveFileButton"
        Me.RemoveFileButton.Size = New System.Drawing.Size(34, 31)
        Me.RemoveFileButton.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.RemoveFileButton, "Eliminar documento")
        Me.RemoveFileButton.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.EstadosGroupBox)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGroupBox)
        Me.SplitContainer1.Size = New System.Drawing.Size(797, 146)
        Me.SplitContainer1.SplitterDistance = 319
        Me.SplitContainer1.TabIndex = 0
        '
        'EstadosGroupBox
        '
        Me.EstadosGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EstadosGroupBox.Controls.Add(Me.EstadosDataGridView)
        Me.EstadosGroupBox.Location = New System.Drawing.Point(6, 0)
        Me.EstadosGroupBox.Name = "EstadosGroupBox"
        Me.EstadosGroupBox.Size = New System.Drawing.Size(310, 146)
        Me.EstadosGroupBox.TabIndex = 0
        Me.EstadosGroupBox.TabStop = False
        Me.EstadosGroupBox.Text = "Estados"
        '
        'EstadosDataGridView
        '
        Me.EstadosDataGridView.AllowUserToAddRows = False
        Me.EstadosDataGridView.AllowUserToDeleteRows = False
        Me.EstadosDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EstadosDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.EstadosDataGridView.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EstadosDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle17
        Me.EstadosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.EstadosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Modulo, Me.EstadoFile})
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.EstadosDataGridView.DefaultCellStyle = DataGridViewCellStyle18
        Me.EstadosDataGridView.GridColor = System.Drawing.SystemColors.Control
        Me.EstadosDataGridView.Location = New System.Drawing.Point(7, 17)
        Me.EstadosDataGridView.MultiSelect = False
        Me.EstadosDataGridView.Name = "EstadosDataGridView"
        Me.EstadosDataGridView.ReadOnly = True
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EstadosDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle19
        Me.EstadosDataGridView.RowHeadersVisible = False
        Me.EstadosDataGridView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EstadosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.EstadosDataGridView.Size = New System.Drawing.Size(297, 123)
        Me.EstadosDataGridView.TabIndex = 0
        '
        'Modulo
        '
        Me.Modulo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Modulo.DataPropertyName = "Nombre_Modulo"
        Me.Modulo.HeaderText = "Módulo"
        Me.Modulo.Name = "Modulo"
        Me.Modulo.ReadOnly = True
        '
        'EstadoFile
        '
        Me.EstadoFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.EstadoFile.DataPropertyName = "Nombre_Estado"
        Me.EstadoFile.HeaderText = "Estado"
        Me.EstadoFile.Name = "EstadoFile"
        Me.EstadoFile.ReadOnly = True
        '
        'DataGroupBox
        '
        Me.DataGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGroupBox.Controls.Add(Me.TablaAsociadaButton)
        Me.DataGroupBox.Controls.Add(Me.DataDataGridView)
        Me.DataGroupBox.Controls.Add(Me.EditDataButton)
        Me.DataGroupBox.Location = New System.Drawing.Point(3, 0)
        Me.DataGroupBox.Name = "DataGroupBox"
        Me.DataGroupBox.Size = New System.Drawing.Size(468, 146)
        Me.DataGroupBox.TabIndex = 12
        Me.DataGroupBox.TabStop = False
        Me.DataGroupBox.Text = "Data"
        '
        'TablaAsociadaButton
        '
        Me.TablaAsociadaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TablaAsociadaButton.Enabled = False
        Me.TablaAsociadaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.TablaAsociada
        Me.TablaAsociadaButton.Location = New System.Drawing.Point(428, 72)
        Me.TablaAsociadaButton.Name = "TablaAsociadaButton"
        Me.TablaAsociadaButton.Size = New System.Drawing.Size(34, 31)
        Me.TablaAsociadaButton.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.TablaAsociadaButton, "Ver tabla asociada")
        Me.TablaAsociadaButton.UseVisualStyleBackColor = True
        Me.TablaAsociadaButton.Visible = False
        '
        'DataDataGridView
        '
        Me.DataDataGridView.AllowUserToAddRows = False
        Me.DataDataGridView.AllowUserToDeleteRows = False
        Me.DataDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataDataGridView.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle20
        Me.DataDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nombre_Campo, Me.Valor_File_Data})
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle21.SelectionBackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataDataGridView.DefaultCellStyle = DataGridViewCellStyle21
        Me.DataDataGridView.GridColor = System.Drawing.SystemColors.Control
        Me.DataDataGridView.Location = New System.Drawing.Point(6, 20)
        Me.DataDataGridView.MultiSelect = False
        Me.DataDataGridView.Name = "DataDataGridView"
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle22.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle22
        Me.DataDataGridView.RowHeadersVisible = False
        DataGridViewCellStyle23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle23
        Me.DataDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataDataGridView.Size = New System.Drawing.Size(416, 120)
        Me.DataDataGridView.TabIndex = 6
        '
        'Nombre_Campo
        '
        Me.Nombre_Campo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Nombre_Campo.DataPropertyName = "Nombre_Campo"
        Me.Nombre_Campo.HeaderText = "Nombre del Campo"
        Me.Nombre_Campo.Name = "Nombre_Campo"
        Me.Nombre_Campo.ReadOnly = True
        '
        'Valor_File_Data
        '
        Me.Valor_File_Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Valor_File_Data.DataPropertyName = "Valor_File_Data"
        Me.Valor_File_Data.HeaderText = "Valor del Campo"
        Me.Valor_File_Data.Name = "Valor_File_Data"
        Me.Valor_File_Data.ReadOnly = True
        '
        'EditDataButton
        '
        Me.EditDataButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EditDataButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Edit
        Me.EditDataButton.Location = New System.Drawing.Point(428, 109)
        Me.EditDataButton.Name = "EditDataButton"
        Me.EditDataButton.Size = New System.Drawing.Size(34, 31)
        Me.EditDataButton.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.EditDataButton, "Editar Data")
        Me.EditDataButton.UseVisualStyleBackColor = True
        '
        'BusquedaGroupBox
        '
        Me.BusquedaGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BusquedaGroupBox.Controls.Add(Me.BuscarButton)
        Me.BusquedaGroupBox.Controls.Add(Me.CBarrasRadioButton)
        Me.BusquedaGroupBox.Controls.Add(Me.CampoRadioButton)
        Me.BusquedaGroupBox.Controls.Add(Me.CBarrasPanel)
        Me.BusquedaGroupBox.Controls.Add(Me.CampoPanel)
        Me.BusquedaGroupBox.Location = New System.Drawing.Point(403, 3)
        Me.BusquedaGroupBox.Name = "BusquedaGroupBox"
        Me.BusquedaGroupBox.Size = New System.Drawing.Size(531, 72)
        Me.BusquedaGroupBox.TabIndex = 0
        Me.BusquedaGroupBox.TabStop = False
        Me.BusquedaGroupBox.Text = "Filtro de Búsqueda"
        '
        'BuscarButton
        '
        Me.BuscarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar
        Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BuscarButton.Location = New System.Drawing.Point(409, 35)
        Me.BuscarButton.Name = "BuscarButton"
        Me.BuscarButton.Size = New System.Drawing.Size(75, 23)
        Me.BuscarButton.TabIndex = 5
        Me.BuscarButton.Text = "&Buscar"
        Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BuscarButton.UseVisualStyleBackColor = True
        '
        'CBarrasRadioButton
        '
        Me.CBarrasRadioButton.Location = New System.Drawing.Point(235, 16)
        Me.CBarrasRadioButton.Name = "CBarrasRadioButton"
        Me.CBarrasRadioButton.Size = New System.Drawing.Size(153, 17)
        Me.CBarrasRadioButton.TabIndex = 4
        Me.CBarrasRadioButton.Text = "Por Código de Barras"
        Me.CBarrasRadioButton.UseVisualStyleBackColor = True
        '
        'CampoRadioButton
        '
        Me.CampoRadioButton.Checked = True
        Me.CampoRadioButton.Location = New System.Drawing.Point(86, 16)
        Me.CampoRadioButton.Name = "CampoRadioButton"
        Me.CampoRadioButton.Size = New System.Drawing.Size(120, 17)
        Me.CampoRadioButton.TabIndex = 3
        Me.CampoRadioButton.TabStop = True
        Me.CampoRadioButton.Text = "Por Campo"
        Me.CampoRadioButton.UseVisualStyleBackColor = True
        '
        'CBarrasPanel
        '
        Me.CBarrasPanel.Controls.Add(Me.cbarrasDesktopCBarrasControl)
        Me.CBarrasPanel.Controls.Add(Me.Label1)
        Me.CBarrasPanel.Location = New System.Drawing.Point(47, 36)
        Me.CBarrasPanel.Name = "CBarrasPanel"
        Me.CBarrasPanel.Size = New System.Drawing.Size(344, 26)
        Me.CBarrasPanel.TabIndex = 4
        Me.CBarrasPanel.Visible = False
        '
        'cbarrasDesktopCBarrasControl
        '
        Me.cbarrasDesktopCBarrasControl.FocusIn = System.Drawing.Color.LightYellow
        Me.cbarrasDesktopCBarrasControl.FocusOut = System.Drawing.Color.White
        Me.cbarrasDesktopCBarrasControl.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbarrasDesktopCBarrasControl.Location = New System.Drawing.Point(124, 3)
        Me.cbarrasDesktopCBarrasControl.MaxLength = 0
        Me.cbarrasDesktopCBarrasControl.Name = "cbarrasDesktopCBarrasControl"
        Me.cbarrasDesktopCBarrasControl.Permite_Pegar = True
        Me.cbarrasDesktopCBarrasControl.Size = New System.Drawing.Size(217, 20)
        Me.cbarrasDesktopCBarrasControl.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Código de Barras:"
        '
        'CampoPanel
        '
        Me.CampoPanel.Controls.Add(Me.CampoComboBox)
        Me.CampoPanel.Controls.Add(Me.CondicionComboBox)
        Me.CampoPanel.Controls.Add(Me.ValorBusquedaTextBox)
        Me.CampoPanel.Location = New System.Drawing.Point(48, 35)
        Me.CampoPanel.Name = "CampoPanel"
        Me.CampoPanel.Size = New System.Drawing.Size(344, 26)
        Me.CampoPanel.TabIndex = 3
        '
        'CampoComboBox
        '
        Me.CampoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CampoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CampoComboBox.DisabledEnter = False
        Me.CampoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CampoComboBox.fk_Campo = 0
        Me.CampoComboBox.fk_Documento = 0
        Me.CampoComboBox.fk_Validacion = 0
        Me.CampoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CampoComboBox.FormattingEnabled = True
        Me.CampoComboBox.Location = New System.Drawing.Point(3, 3)
        Me.CampoComboBox.Name = "CampoComboBox"
        Me.CampoComboBox.Size = New System.Drawing.Size(160, 21)
        Me.CampoComboBox.TabIndex = 0
        '
        'CondicionComboBox
        '
        Me.CondicionComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CondicionComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CondicionComboBox.DisabledEnter = False
        Me.CondicionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CondicionComboBox.fk_Campo = 0
        Me.CondicionComboBox.fk_Documento = 0
        Me.CondicionComboBox.fk_Validacion = 0
        Me.CondicionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CondicionComboBox.FormattingEnabled = True
        Me.CondicionComboBox.Location = New System.Drawing.Point(170, 2)
        Me.CondicionComboBox.Name = "CondicionComboBox"
        Me.CondicionComboBox.Size = New System.Drawing.Size(40, 21)
        Me.CondicionComboBox.TabIndex = 1
        '
        'ValorBusquedaTextBox
        '
        Me.ValorBusquedaTextBox._Obligatorio = False
        Me.ValorBusquedaTextBox._PermitePegar = True
        Me.ValorBusquedaTextBox.Cantidad_Decimales = CType(0, Short)
        Me.ValorBusquedaTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
        Me.ValorBusquedaTextBox.DateFormat = Nothing
        Me.ValorBusquedaTextBox.DisabledEnter = False
        Me.ValorBusquedaTextBox.DisabledTab = False
        Me.ValorBusquedaTextBox.EnabledShortCuts = True
        Me.ValorBusquedaTextBox.fk_Campo = 0
        Me.ValorBusquedaTextBox.fk_Documento = 0
        Me.ValorBusquedaTextBox.fk_Validacion = 0
        Me.ValorBusquedaTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.ValorBusquedaTextBox.FocusOut = System.Drawing.Color.White
        Me.ValorBusquedaTextBox.Location = New System.Drawing.Point(217, 3)
        Me.ValorBusquedaTextBox.MaskedTextBox_Property = ""
        Me.ValorBusquedaTextBox.MaximumLength = CType(0, Short)
        Me.ValorBusquedaTextBox.MinimumLength = CType(0, Short)
        Me.ValorBusquedaTextBox.Name = "ValorBusquedaTextBox"
        Me.ValorBusquedaTextBox.Obligatorio = False
        Me.ValorBusquedaTextBox.permitePegar = True
        Rango1.MaxValue = 2147483647.0R
        Rango1.MinValue = 0.0R
        Me.ValorBusquedaTextBox.Rango = Rango1
        Me.ValorBusquedaTextBox.Size = New System.Drawing.Size(123, 21)
        Me.ValorBusquedaTextBox.TabIndex = 2
        Me.ValorBusquedaTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
        Me.ValorBusquedaTextBox.Usa_Decimales = False
        Me.ValorBusquedaTextBox.Validos_Cantidad_Puntos = False
        '
        'InformesTabPage
        '
        Me.InformesTabPage.Controls.Add(Me.WorkSpaceRiskReportViewerControl)
        Me.InformesTabPage.ImageIndex = 2
        Me.InformesTabPage.Location = New System.Drawing.Point(4, 26)
        Me.InformesTabPage.Name = "InformesTabPage"
        Me.InformesTabPage.Size = New System.Drawing.Size(1337, 424)
        Me.InformesTabPage.TabIndex = 4
        Me.InformesTabPage.Tag = "Informes"
        Me.InformesTabPage.Text = "(F12) Informes"
        Me.InformesTabPage.UseVisualStyleBackColor = True
        '
        'WorkSpaceRiskReportViewerControl
        '
        Me.WorkSpaceRiskReportViewerControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WorkSpaceRiskReportViewerControl.Location = New System.Drawing.Point(0, 0)
        Me.WorkSpaceRiskReportViewerControl.Name = "WorkSpaceRiskReportViewerControl"
        Me.WorkSpaceRiskReportViewerControl.Size = New System.Drawing.Size(1337, 424)
        Me.WorkSpaceRiskReportViewerControl.TabIndex = 2
        '
        'ProyectoDecevalTabPage
        '
        Me.ProyectoDecevalTabPage.Controls.Add(Me.btnCargueLogDeceval)
        Me.ProyectoDecevalTabPage.Controls.Add(Me.FlechaControl1)
        Me.ProyectoDecevalTabPage.Controls.Add(Me.Button1)
        Me.ProyectoDecevalTabPage.Controls.Add(Me.FlechaControl2)
        Me.ProyectoDecevalTabPage.Controls.Add(Me.FlechaControl5)
        Me.ProyectoDecevalTabPage.Controls.Add(Me.FlechaControl6)
        Me.ProyectoDecevalTabPage.Controls.Add(Me.Button2)
        Me.ProyectoDecevalTabPage.Controls.Add(Me.FlechaControl7)
        Me.ProyectoDecevalTabPage.Controls.Add(Me.Button5)
        Me.ProyectoDecevalTabPage.Controls.Add(Me.btnMesaControlDeceval)
        Me.ProyectoDecevalTabPage.ImageIndex = 0
        Me.ProyectoDecevalTabPage.Location = New System.Drawing.Point(4, 26)
        Me.ProyectoDecevalTabPage.Name = "ProyectoDecevalTabPage"
        Me.ProyectoDecevalTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ProyectoDecevalTabPage.Size = New System.Drawing.Size(1337, 424)
        Me.ProyectoDecevalTabPage.TabIndex = 5
        Me.ProyectoDecevalTabPage.Text = "ProyectoDeceval"
        Me.ProyectoDecevalTabPage.UseVisualStyleBackColor = True
        '
        'btnCargueLogDeceval
        '
        Me.btnCargueLogDeceval.AccessibleDescription = ""
        Me.btnCargueLogDeceval.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCargueLogDeceval.BackColor = System.Drawing.Color.White
        Me.btnCargueLogDeceval.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCargueLogDeceval.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCargueLogDeceval.Image = Global.Miharu.Risk.Library.My.Resources.Resources.BtnCargar
        Me.btnCargueLogDeceval.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCargueLogDeceval.Location = New System.Drawing.Point(344, 177)
        Me.btnCargueLogDeceval.Name = "btnCargueLogDeceval"
        Me.btnCargueLogDeceval.Size = New System.Drawing.Size(118, 65)
        Me.btnCargueLogDeceval.TabIndex = 104
        Me.btnCargueLogDeceval.Tag = "Ctrl + U"
        Me.btnCargueLogDeceval.Text = "Cargue Log"
        Me.btnCargueLogDeceval.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCargueLogDeceval.UseVisualStyleBackColor = False
        '
        'FlechaControl1
        '
        Me.FlechaControl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FlechaControl1.ArrowWidth = 10
        Me.FlechaControl1.BackColor = System.Drawing.Color.Transparent
        Me.FlechaControl1.BorderColor = System.Drawing.Color.Black
        Me.FlechaControl1.BorderWidth = CType(1, Byte)
        Me.FlechaControl1.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.FlechaControl1.FillColor = System.Drawing.Color.LightSteelBlue
        Me.FlechaControl1.Location = New System.Drawing.Point(758, 238)
        Me.FlechaControl1.Name = "FlechaControl1"
        Me.FlechaControl1.Size = New System.Drawing.Size(72, 40)
        Me.FlechaControl1.TabIndex = 103
        Me.FlechaControl1.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.UP
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Enabled = False
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = Global.Miharu.Risk.Library.My.Resources.Resources.CentroDistribucion
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button1.Location = New System.Drawing.Point(735, 155)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(113, 77)
        Me.Button1.TabIndex = 102
        Me.Button1.Text = "(F7) Distribución"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.UseVisualStyleBackColor = False
        '
        'FlechaControl2
        '
        Me.FlechaControl2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FlechaControl2.ArrowWidth = 10
        Me.FlechaControl2.BackColor = System.Drawing.Color.Transparent
        Me.FlechaControl2.BorderColor = System.Drawing.Color.Black
        Me.FlechaControl2.BorderWidth = CType(1, Byte)
        Me.FlechaControl2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.FlechaControl2.FillColor = System.Drawing.Color.IndianRed
        Me.FlechaControl2.Location = New System.Drawing.Point(393, 85)
        Me.FlechaControl2.Name = "FlechaControl2"
        Me.FlechaControl2.Size = New System.Drawing.Size(327, 74)
        Me.FlechaControl2.TabIndex = 101
        Me.FlechaControl2.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.UP_RIGHT
        '
        'FlechaControl5
        '
        Me.FlechaControl5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FlechaControl5.ArrowWidth = 10
        Me.FlechaControl5.BackColor = System.Drawing.Color.Transparent
        Me.FlechaControl5.BorderColor = System.Drawing.Color.Black
        Me.FlechaControl5.BorderWidth = CType(1, Byte)
        Me.FlechaControl5.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.FlechaControl5.FillColor = System.Drawing.Color.LightSteelBlue
        Me.FlechaControl5.Location = New System.Drawing.Point(554, 260)
        Me.FlechaControl5.Name = "FlechaControl5"
        Me.FlechaControl5.Size = New System.Drawing.Size(166, 76)
        Me.FlechaControl5.TabIndex = 98
        Me.FlechaControl5.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN_RIGHT
        '
        'FlechaControl6
        '
        Me.FlechaControl6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FlechaControl6.ArrowWidth = 10
        Me.FlechaControl6.BackColor = System.Drawing.Color.Transparent
        Me.FlechaControl6.BorderColor = System.Drawing.Color.Black
        Me.FlechaControl6.BorderWidth = CType(1, Byte)
        Me.FlechaControl6.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.FlechaControl6.FillColor = System.Drawing.Color.LightSteelBlue
        Me.FlechaControl6.Location = New System.Drawing.Point(393, 260)
        Me.FlechaControl6.Name = "FlechaControl6"
        Me.FlechaControl6.Size = New System.Drawing.Size(155, 76)
        Me.FlechaControl6.TabIndex = 97
        Me.FlechaControl6.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN_RIGHT
        '
        'Button2
        '
        Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Empaque
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button2.Location = New System.Drawing.Point(735, 284)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(110, 60)
        Me.Button2.TabIndex = 96
        Me.Button2.Text = "(F6) Empaque"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button2.UseVisualStyleBackColor = False
        '
        'FlechaControl7
        '
        Me.FlechaControl7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FlechaControl7.ArrowWidth = 10
        Me.FlechaControl7.BackColor = System.Drawing.Color.Transparent
        Me.FlechaControl7.BorderColor = System.Drawing.Color.Black
        Me.FlechaControl7.BorderWidth = CType(1, Byte)
        Me.FlechaControl7.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.FlechaControl7.FillColor = System.Drawing.Color.LightSteelBlue
        Me.FlechaControl7.Location = New System.Drawing.Point(470, 177)
        Me.FlechaControl7.Name = "FlechaControl7"
        Me.FlechaControl7.Size = New System.Drawing.Size(37, 60)
        Me.FlechaControl7.TabIndex = 95
        Me.FlechaControl7.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'Button5
        '
        Me.Button5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button5.BackColor = System.Drawing.Color.IndianRed
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Devoluciones
        Me.Button5.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button5.Location = New System.Drawing.Point(738, 60)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(110, 70)
        Me.Button5.TabIndex = 94
        Me.Button5.Text = "(F8) Devoluciones"
        Me.Button5.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button5.UseVisualStyleBackColor = False
        '
        'btnMesaControlDeceval
        '
        Me.btnMesaControlDeceval.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnMesaControlDeceval.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnMesaControlDeceval.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMesaControlDeceval.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMesaControlDeceval.Image = Global.Miharu.Risk.Library.My.Resources.Resources.MesaFisicos
        Me.btnMesaControlDeceval.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnMesaControlDeceval.Location = New System.Drawing.Point(522, 177)
        Me.btnMesaControlDeceval.Name = "btnMesaControlDeceval"
        Me.btnMesaControlDeceval.Size = New System.Drawing.Size(115, 65)
        Me.btnMesaControlDeceval.TabIndex = 91
        Me.btnMesaControlDeceval.Text = "Mesa Control Validaciones"
        Me.btnMesaControlDeceval.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnMesaControlDeceval.UseVisualStyleBackColor = False
        '
        'TabControlImageList
        '
        Me.TabControlImageList.ImageStream = CType(resources.GetObject("TabControlImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TabControlImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.TabControlImageList.Images.SetKeyName(0, "chart_organisation.png")
        Me.TabControlImageList.Images.SetKeyName(1, "vcard.png")
        Me.TabControlImageList.Images.SetKeyName(2, "report.png")
        Me.TabControlImageList.Images.SetKeyName(3, "Buscar.ico")
        '
        'WorkSpaceMenuStrip
        '
        Me.WorkSpaceMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.configuracionToolStripMenuItem, Me.ReportesToolStripMenuItem, Me.CodigosDeBarrasToolStripMenuItem1, Me.EstrucutrasDeCargueToolStripMenuItem, Me.ProcesosEspecialesToolStripMenuItem})
        Me.WorkSpaceMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.WorkSpaceMenuStrip.Name = "WorkSpaceMenuStrip"
        Me.WorkSpaceMenuStrip.Size = New System.Drawing.Size(1345, 24)
        Me.WorkSpaceMenuStrip.TabIndex = 35
        Me.WorkSpaceMenuStrip.Text = "MenuStrip1"
        '
        'configuracionToolStripMenuItem
        '
        Me.configuracionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RiskToolStripMenuItem})
        Me.configuracionToolStripMenuItem.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnGenerarEstructura
        Me.configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem"
        Me.configuracionToolStripMenuItem.Size = New System.Drawing.Size(167, 20)
        Me.configuracionToolStripMenuItem.Text = "Configuración Esquemas"
        '
        'RiskToolStripMenuItem
        '
        Me.RiskToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EsquemasToolStripMenuItem, Me.DocumentosToolStripMenuItem, Me.CamposToolStripMenuItem})
        Me.RiskToolStripMenuItem.Image = Global.Miharu.Risk.Library.My.Resources.Resources.MainRisk
        Me.RiskToolStripMenuItem.Name = "RiskToolStripMenuItem"
        Me.RiskToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.RiskToolStripMenuItem.Text = "Risk"
        '
        'EsquemasToolStripMenuItem
        '
        Me.EsquemasToolStripMenuItem.Name = "EsquemasToolStripMenuItem"
        Me.EsquemasToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.EsquemasToolStripMenuItem.Text = "Esquemas..."
        '
        'DocumentosToolStripMenuItem
        '
        Me.DocumentosToolStripMenuItem.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnCopiar
        Me.DocumentosToolStripMenuItem.Name = "DocumentosToolStripMenuItem"
        Me.DocumentosToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.DocumentosToolStripMenuItem.Text = "Documentos..."
        '
        'CamposToolStripMenuItem
        '
        Me.CamposToolStripMenuItem.Image = Global.Miharu.Risk.Library.My.Resources.Resources.MainField
        Me.CamposToolStripMenuItem.Name = "CamposToolStripMenuItem"
        Me.CamposToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.CamposToolStripMenuItem.Text = "Campos..."
        '
        'ReportesToolStripMenuItem
        '
        Me.ReportesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DestapeToolStripMenuItem, Me.MesaDeControlFísicosToolStripMenuItem})
        Me.ReportesToolStripMenuItem.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Mainreport
        Me.ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem"
        Me.ReportesToolStripMenuItem.Size = New System.Drawing.Size(105, 20)
        Me.ReportesToolStripMenuItem.Text = "Reportes Risk"
        '
        'DestapeToolStripMenuItem
        '
        Me.DestapeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OTsToolStripMenuItem, Me.UniversalVSParcialToolStripMenuItem})
        Me.DestapeToolStripMenuItem.Image = Global.Miharu.Risk.Library.My.Resources.Resources.MainDestape
        Me.DestapeToolStripMenuItem.Name = "DestapeToolStripMenuItem"
        Me.DestapeToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.DestapeToolStripMenuItem.Text = "Destape"
        '
        'OTsToolStripMenuItem
        '
        Me.OTsToolStripMenuItem.Name = "OTsToolStripMenuItem"
        Me.OTsToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.OTsToolStripMenuItem.Text = "OTs..."
        '
        'UniversalVSParcialToolStripMenuItem
        '
        Me.UniversalVSParcialToolStripMenuItem.Name = "UniversalVSParcialToolStripMenuItem"
        Me.UniversalVSParcialToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.UniversalVSParcialToolStripMenuItem.Text = "Universal VS Parcial..."
        '
        'MesaDeControlFísicosToolStripMenuItem
        '
        Me.MesaDeControlFísicosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReprocesosToolStripMenuItem, Me.EstadosOTToolStripMenuItem})
        Me.MesaDeControlFísicosToolStripMenuItem.Image = Global.Miharu.Risk.Library.My.Resources.Resources.MainMesaFisicos
        Me.MesaDeControlFísicosToolStripMenuItem.Name = "MesaDeControlFísicosToolStripMenuItem"
        Me.MesaDeControlFísicosToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.MesaDeControlFísicosToolStripMenuItem.Text = "Mesa de Control Físicos"
        '
        'ReprocesosToolStripMenuItem
        '
        Me.ReprocesosToolStripMenuItem.Name = "ReprocesosToolStripMenuItem"
        Me.ReprocesosToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.ReprocesosToolStripMenuItem.Text = "Reprocesos..."
        '
        'EstadosOTToolStripMenuItem
        '
        Me.EstadosOTToolStripMenuItem.Name = "EstadosOTToolStripMenuItem"
        Me.EstadosOTToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.EstadosOTToolStripMenuItem.Text = "Estados OT..."
        '
        'CodigosDeBarrasToolStripMenuItem1
        '
        Me.CodigosDeBarrasToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CarpetasYDocumentosToolStripMenuItem, Me.CajasToolStripMenuItem, Me.ProcesamientoLightToolStripMenuItem})
        Me.CodigosDeBarrasToolStripMenuItem1.Image = Global.Miharu.Risk.Library.My.Resources.Resources.MainCode
        Me.CodigosDeBarrasToolStripMenuItem1.Name = "CodigosDeBarrasToolStripMenuItem1"
        Me.CodigosDeBarrasToolStripMenuItem1.Size = New System.Drawing.Size(130, 20)
        Me.CodigosDeBarrasToolStripMenuItem1.Text = "Codigos de Barras"
        '
        'CarpetasYDocumentosToolStripMenuItem
        '
        Me.CarpetasYDocumentosToolStripMenuItem.Image = Global.Miharu.Risk.Library.My.Resources.Resources.MainFoldersFiles
        Me.CarpetasYDocumentosToolStripMenuItem.Name = "CarpetasYDocumentosToolStripMenuItem"
        Me.CarpetasYDocumentosToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.CarpetasYDocumentosToolStripMenuItem.Text = "Carpetas y documentos..."
        '
        'CajasToolStripMenuItem
        '
        Me.CajasToolStripMenuItem.Image = Global.Miharu.Risk.Library.My.Resources.Resources.MainBox
        Me.CajasToolStripMenuItem.Name = "CajasToolStripMenuItem"
        Me.CajasToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.CajasToolStripMenuItem.Text = "Cajas..."
        '
        'ProcesamientoLightToolStripMenuItem
        '
        Me.ProcesamientoLightToolStripMenuItem.Image = Global.Miharu.Risk.Library.My.Resources.Resources.BarCode
        Me.ProcesamientoLightToolStripMenuItem.Name = "ProcesamientoLightToolStripMenuItem"
        Me.ProcesamientoLightToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.ProcesamientoLightToolStripMenuItem.Text = "Procesamiento Light..."
        '
        'EstrucutrasDeCargueToolStripMenuItem
        '
        Me.EstrucutrasDeCargueToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CargueToolStripMenuItem, Me.ActualizaciónToolStripMenuItem})
        Me.EstrucutrasDeCargueToolStripMenuItem.Image = Global.Miharu.Risk.Library.My.Resources.Resources.MainStructure
        Me.EstrucutrasDeCargueToolStripMenuItem.Name = "EstrucutrasDeCargueToolStripMenuItem"
        Me.EstrucutrasDeCargueToolStripMenuItem.Size = New System.Drawing.Size(148, 20)
        Me.EstrucutrasDeCargueToolStripMenuItem.Text = "Estructuras de cargue"
        '
        'CargueToolStripMenuItem
        '
        Me.CargueToolStripMenuItem.Image = Global.Miharu.Risk.Library.My.Resources.Resources.brnCargar
        Me.CargueToolStripMenuItem.Name = "CargueToolStripMenuItem"
        Me.CargueToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.CargueToolStripMenuItem.Text = "Cargue..."
        '
        'ActualizaciónToolStripMenuItem
        '
        Me.ActualizaciónToolStripMenuItem.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Edit
        Me.ActualizaciónToolStripMenuItem.Name = "ActualizaciónToolStripMenuItem"
        Me.ActualizaciónToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.ActualizaciónToolStripMenuItem.Text = "Actualización..."
        '
        'ProcesosEspecialesToolStripMenuItem
        '
        Me.ProcesosEspecialesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ValidacionesDinámicasToolStripMenuItem})
        Me.ProcesosEspecialesToolStripMenuItem.Name = "ProcesosEspecialesToolStripMenuItem"
        Me.ProcesosEspecialesToolStripMenuItem.Size = New System.Drawing.Size(122, 20)
        Me.ProcesosEspecialesToolStripMenuItem.Text = "Procesos Especiales"
        '
        'ValidacionesDinámicasToolStripMenuItem
        '
        Me.ValidacionesDinámicasToolStripMenuItem.Name = "ValidacionesDinámicasToolStripMenuItem"
        Me.ValidacionesDinámicasToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.ValidacionesDinámicasToolStripMenuItem.Text = "Validaciones Dinámicas"
        '
        'FormRiskWorkSpace
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1345, 478)
        Me.Controls.Add(Me.MainTabControl)
        Me.Controls.Add(Me.WorkSpaceMenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "FormRiskWorkSpace"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WorkSpace"
        Me.WSPanel.ResumeLayout(False)
        Me.WSPanel.PerformLayout()
        Me.MainTabControl.ResumeLayout(False)
        Me.ProcesoTabPage.ResumeLayout(False)
        Me.FisicosTabPage.ResumeLayout(False)
        Me.MesasFlowLayoutPanel.ResumeLayout(False)
        Me.BusquedaTabPage.ResumeLayout(False)
        Me.DataPanel.ResumeLayout(False)
        CType(Me.DataAsociadaDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DetallePanel.ResumeLayout(False)
        CType(Me.DetalleDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BusquedaSplitContainer.Panel1.ResumeLayout(False)
        Me.BusquedaSplitContainer.Panel2.ResumeLayout(False)
        CType(Me.BusquedaSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BusquedaSplitContainer.ResumeLayout(False)
        Me.FoldersGroupBox.ResumeLayout(False)
        CType(Me.FoldersDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FileDataSplitContainer.Panel1.ResumeLayout(False)
        Me.FileDataSplitContainer.Panel2.ResumeLayout(False)
        CType(Me.FileDataSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FileDataSplitContainer.ResumeLayout(False)
        Me.FilesGroupBox.ResumeLayout(False)
        CType(Me.FilesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.EstadosGroupBox.ResumeLayout(False)
        CType(Me.EstadosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DataGroupBox.ResumeLayout(False)
        CType(Me.DataDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BusquedaGroupBox.ResumeLayout(False)
        Me.CBarrasPanel.ResumeLayout(False)
        Me.CBarrasPanel.PerformLayout()
        Me.CampoPanel.ResumeLayout(False)
        Me.CampoPanel.PerformLayout()
        Me.InformesTabPage.ResumeLayout(False)
        Me.ProyectoDecevalTabPage.ResumeLayout(False)
        Me.WorkSpaceMenuStrip.ResumeLayout(False)
        Me.WorkSpaceMenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents WSPanel As System.Windows.Forms.Panel
    Public WithEvents destape_MesaFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents mesa_EmpaqueFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents ot_RecepcionFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents cargueUniversal_OTFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents Actualizar_RecepcionFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents custodiaButton As System.Windows.Forms.Button
    Public WithEvents empaqueButton As System.Windows.Forms.Button
    Public WithEvents mesaControlFisicoButton As System.Windows.Forms.Button
    Public WithEvents actualizarButton As System.Windows.Forms.Button
    Public WithEvents destapeButton As System.Windows.Forms.Button
    Public WithEvents recepcionButton As System.Windows.Forms.Button
    Public WithEvents otButton As System.Windows.Forms.Button
    Public WithEvents cargueParcialButton As System.Windows.Forms.Button
    Public WithEvents cargueUniversalButton As System.Windows.Forms.Button
    Public WithEvents MainTabControl As System.Windows.Forms.TabControl
    Public WithEvents ProcesoTabPage As System.Windows.Forms.TabPage
    Public WithEvents WorkSpaceMenuStrip As System.Windows.Forms.MenuStrip
    Public WithEvents configuracionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ReportesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents TabControlImageList As System.Windows.Forms.ImageList
    Public WithEvents FisicosTabPage As System.Windows.Forms.TabPage
    Public WithEvents mesaControlImagenesButton As System.Windows.Forms.Button
    Public WithEvents centroD_CustodiaFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents devolucionesButton As System.Windows.Forms.Button
    Public WithEvents destape_DevolucionFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents insercionesButton As System.Windows.Forms.Button
    Public WithEvents recepcion_DestapeFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents mesa_DevolucionFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents MesasFlowLayoutPanel As System.Windows.Forms.Panel
    Public WithEvents Primera_DevolucionesFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents Segunda_TerceraFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents Tercera_EmpaqueFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents Segunda_EmpaqueFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents Primera_EmpaqueFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents Button18 As System.Windows.Forms.Button
    Public WithEvents Primera_SegundaFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents FisicoTerceraCapturaButton As System.Windows.Forms.Button
    Public WithEvents FisicoSegundaCapturaButton As System.Windows.Forms.Button
    Public WithEvents DevolucionesMCButton As System.Windows.Forms.Button
    Public WithEvents FisicoPrimeraCapturaButton As System.Windows.Forms.Button
    Public WithEvents DestapeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents OTsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents UniversalVSParcialToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MesaDeControlFísicosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ReprocesosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents RiskToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents EsquemasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents DocumentosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents CamposToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents EstadosOTToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents empaque_CentroDFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents centroDistribucionButton As System.Windows.Forms.Button
    Public WithEvents Empaque_CentroDMesaFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents CentroDistribucionMesaButton As System.Windows.Forms.Button
    Public WithEvents Empaque2Button As System.Windows.Forms.Button
    Public WithEvents BusquedaTabPage As System.Windows.Forms.TabPage
    Public WithEvents BusquedaGroupBox As System.Windows.Forms.GroupBox
    Public WithEvents CampoComboBox As DesktopComboBoxControl
    Public WithEvents CondicionComboBox As DesktopComboBoxControl
    Public WithEvents ValorBusquedaTextBox As DesktopTextBoxControl
    Public WithEvents CBarrasRadioButton As System.Windows.Forms.RadioButton
    Public WithEvents CampoRadioButton As System.Windows.Forms.RadioButton
    Public WithEvents CampoPanel As System.Windows.Forms.Panel
    Public WithEvents CBarrasPanel As System.Windows.Forms.Panel
    Public WithEvents cbarrasDesktopCBarrasControl As Miharu.Desktop.Controls.DesktopCBarras.DesktopCBarrasControl
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents BuscarButton As System.Windows.Forms.Button
    Public WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents BusquedaSplitContainer As System.Windows.Forms.SplitContainer
    Public WithEvents FoldersGroupBox As System.Windows.Forms.GroupBox
    Public WithEvents RemoveFolderButton As System.Windows.Forms.Button
    Public WithEvents FoldersDataGridView As DesktopDataGridViewControl
    Public WithEvents FileDataSplitContainer As System.Windows.Forms.SplitContainer
    Public WithEvents FilesGroupBox As System.Windows.Forms.GroupBox
    Public WithEvents FilesDataGridView As DesktopDataGridViewControl
    Public WithEvents RemoveFileButton As System.Windows.Forms.Button
    Public WithEvents DetallePanel As System.Windows.Forms.Panel
    Public WithEvents DetalleDataGridView As DesktopDataGridViewControl
    Public WithEvents CerrarButton As System.Windows.Forms.Button
    Public WithEvents DataPanel As System.Windows.Forms.Panel
    Public WithEvents CerraDataButton As System.Windows.Forms.Button
    Public WithEvents DataAsociadaDataGridView As DesktopDataGridViewControl
    Public WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Public WithEvents EstadosGroupBox As System.Windows.Forms.GroupBox
    Public WithEvents DataGroupBox As System.Windows.Forms.GroupBox
    Public WithEvents TablaAsociadaButton As System.Windows.Forms.Button
    Public WithEvents DataDataGridView As DesktopDataGridViewControl
    Public WithEvents Nombre_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents Valor_File_Data As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents EditDataButton As System.Windows.Forms.Button
    Public WithEvents EstadosDataGridView As DesktopDataGridViewControl
    Public WithEvents Modulo As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents EstadoFile As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents CBarras As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents Fecha_Log As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents Estado_Log As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents CajaProceso As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents CajaCustodia As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents Usuario As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents cerrarOTButton As System.Windows.Forms.Button
    Public WithEvents ot_CerrarOTFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents CodigosDeBarrasToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents CarpetasYDocumentosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents CajasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents EstrucutrasDeCargueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents CargueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ActualizaciónToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents lblProyecto As System.Windows.Forms.Label
    Public WithEvents lblEntidad As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents ProcesamientoLightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents CambiaDocumentoButton As System.Windows.Forms.Button
    Public WithEvents InformesTabPage As System.Windows.Forms.TabPage
    Public WithEvents WorkSpaceRiskReportViewerControl As Miharu.Desktop.Controls.DesktopReportViewer.DesktopReportViewerControl
    Public WithEvents EmpaqueEndososButton As System.Windows.Forms.Button
    Public WithEvents Envio_CorreoButton As System.Windows.Forms.Button
    Public WithEvents CierreFechaRecoleccion As System.Windows.Forms.Button
    Friend WithEvents CBarras_File As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Monto_File As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Folios_File As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nombre_Tipologia As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CBarrasFolder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fk_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fk_Log_cargue As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents DesembolsosButton As System.Windows.Forms.Button
    Friend WithEvents CBarras_Folder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Estado As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Esquema As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Data1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Data2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Data3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Llaves As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents PrevalidarButton As System.Windows.Forms.Button
    Public WithEvents PrevalidarFlecha As Miharu.Desktop.Controls.Flecha.FlechaControl
    Friend WithEvents ProcesosEspecialesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ValidacionesDinámicasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ButtonCancelados As System.Windows.Forms.Button
    Public WithEvents CargueDecevalButton As System.Windows.Forms.Button
    Friend WithEvents ProyectoDecevalTabPage As System.Windows.Forms.TabPage
    Public WithEvents btnCargueLogDeceval As System.Windows.Forms.Button
    Public WithEvents FlechaControl1 As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents Button1 As System.Windows.Forms.Button
    Public WithEvents FlechaControl2 As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents FlechaControl5 As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents FlechaControl6 As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents Button2 As System.Windows.Forms.Button
    Public WithEvents FlechaControl7 As Miharu.Desktop.Controls.Flecha.FlechaControl
    Public WithEvents Button5 As System.Windows.Forms.Button
    Public WithEvents btnMesaControlDeceval As System.Windows.Forms.Button
End Class

