Imports Miharu.Desktop.Controls.Flecha
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Imaging.Library.Controls
Imports Miharu.DDO

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormImagingWorkSpace
    Inherits Miharu.Desktop.Library.FormBase

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormImagingWorkSpace))
        Me.IconosImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.MainFormMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.ConfiguracionModuloToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GeneralesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CamposToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TablasAsociadasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ValidacionesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TipoOTToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContenedorCamposToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrecintoCamposToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmpaqueCamposToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProcesoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AccesosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SeguimientoCargueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogoFirmaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProcesosEspecialesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RechazosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportaPDFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ValidacionesDinámicasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProcesoCierreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CargueLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerarHojaDeControlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerarRotulosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerarRotulosCajaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerarFuidPorCajaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerarFuidToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReemplazarImagenesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CierreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CredivaloresToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogCargaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerarEstampadoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainTabControl = New System.Windows.Forms.TabControl()
        Me.ProcesoTabPage = New System.Windows.Forms.TabPage()
        Me.WSPanel = New System.Windows.Forms.Panel()
        Me.EnviarCorreoButton = New System.Windows.Forms.Button()
        Me.SeguimientoCorreoButton = New System.Windows.Forms.Button()
        Me.ExportarButton = New System.Windows.Forms.Button()
        Me.ExportarCredivaloresButton = New System.Windows.Forms.Button()
        Me.CorreccionMaquinaButton = New System.Windows.Forms.Button()
        Me.CalidadRecortesButton = New System.Windows.Forms.Button()
        Me.ProcesoAdicionalCapturaButton = New System.Windows.Forms.Button()
        Me.Flecha7 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Flecha6 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Flecha4 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.EmpaqueButton = New System.Windows.Forms.Button()
        Me.Flecha5 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.DestapeButton = New System.Windows.Forms.Button()
        Me.OTButton = New System.Windows.Forms.Button()
        Me.FechaProcesoButton = New System.Windows.Forms.Button()
        Me.UsaDominioExternoLabel = New System.Windows.Forms.Label()
        Me.TerceraCapturaFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.CalidadCapturaButton = New System.Windows.Forms.Button()
        Me.UsaCacheLocalLabel = New System.Windows.Forms.Label()
        Me.ValidacionesOpcionalesButton = New System.Windows.Forms.Button()
        Me.SeguimientoCargueButton = New System.Windows.Forms.Button()
        Me.Flecha3 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Flecha2 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.PreCapturaButton = New System.Windows.Forms.Button()
        Me.Flecha1 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.ExportarFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Publicar1Flecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Publicar2Flecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Publicar3Flecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.RecortesButton = New System.Windows.Forms.Button()
        Me.SegundaCapturaFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.CapturaFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.PreindexarFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.CargueFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.TerceraCapturaButton = New System.Windows.Forms.Button()
        Me.SegundaCapturaButton = New System.Windows.Forms.Button()
        Me.ReprocesosButton = New System.Windows.Forms.Button()
        Me.CapturaButton = New System.Windows.Forms.Button()
        Me.IndexarButton = New System.Windows.Forms.Button()
        Me.CargueButton = New System.Windows.Forms.Button()
        Me.RelevoTabPage = New System.Windows.Forms.TabPage()
        Me.UCRelevo = New Miharu.Imaging.Library.Controls.UCRelevo()
        Me.BusquedaTabPage = New System.Windows.Forms.TabPage()
        Me.WorkSpaceImagingSearchControl = New Miharu.Imaging.Library.Controls.ImagingSearchControl()
        Me.DigitalizacionTabPage = New System.Windows.Forms.TabPage()
        Me.WorkSpaceDDODigitalizacionControl = New Miharu.DDO.Forms.UsrDigitaliza()
        Me.BusquedaLoteTabPage = New System.Windows.Forms.TabPage()
        Me.WorkSpaceImagingSearchControlContenedor = New Miharu.Imaging.Library.Controls.ImagingSearchControlContenedor()
        Me.InformesTabPage = New System.Windows.Forms.TabPage()
        Me.WorkSpaceImagingReportViewerControl = New Miharu.Desktop.Controls.DesktopReportViewer.DesktopReportViewerControl()
        Me.RelevoCentralTabPage = New System.Windows.Forms.TabPage()
        Me.UCRelevoCentral = New Miharu.Imaging.Library.Controls.UCRelevoCentralizador()
        Me.MainFormMenuStrip.SuspendLayout()
        Me.MainTabControl.SuspendLayout()
        Me.ProcesoTabPage.SuspendLayout()
        Me.WSPanel.SuspendLayout()
        Me.RelevoTabPage.SuspendLayout()
        Me.BusquedaTabPage.SuspendLayout()
        Me.DigitalizacionTabPage.SuspendLayout()
        Me.BusquedaLoteTabPage.SuspendLayout()
        Me.InformesTabPage.SuspendLayout()
        Me.RelevoCentralTabPage.SuspendLayout()
        Me.SuspendLayout()
        '
        'IconosImageList
        '
        Me.IconosImageList.ImageStream = CType(resources.GetObject("IconosImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.IconosImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.IconosImageList.Images.SetKeyName(0, "proceso.png")
        Me.IconosImageList.Images.SetKeyName(1, "Busqueda.png")
        Me.IconosImageList.Images.SetKeyName(2, "Reportes.png")
        Me.IconosImageList.Images.SetKeyName(3, "Relevo.png")
        Me.IconosImageList.Images.SetKeyName(4, "configu.png")
        Me.IconosImageList.Images.SetKeyName(5, "escaner.png")
        Me.IconosImageList.Images.SetKeyName(6, "Centro04.jpg")
        Me.IconosImageList.Images.SetKeyName(7, "Centro03.jpg")
        '
        'MainFormMenuStrip
        '
        Me.MainFormMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfiguracionModuloToolStripMenuItem, Me.ProcesoToolStripMenuItem, Me.ProcesosEspecialesToolStripMenuItem, Me.ProcesoCierreToolStripMenuItem})
        Me.MainFormMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MainFormMenuStrip.Name = "MainFormMenuStrip"
        Me.MainFormMenuStrip.Size = New System.Drawing.Size(962, 24)
        Me.MainFormMenuStrip.TabIndex = 0
        '
        'ConfiguracionModuloToolStripMenuItem
        '
        Me.ConfiguracionModuloToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GeneralesToolStripMenuItem, Me.DocumentosToolStripMenuItem, Me.CamposToolStripMenuItem, Me.TablasAsociadasToolStripMenuItem, Me.ValidacionesToolStripMenuItem, Me.TipoOTToolStripMenuItem1, Me.ContenedorCamposToolStripMenuItem1, Me.PrecintoCamposToolStripMenuItem1, Me.EmpaqueCamposToolStripMenuItem})
        Me.ConfiguracionModuloToolStripMenuItem.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGenerarEstructura
        Me.ConfiguracionModuloToolStripMenuItem.Name = "ConfiguracionModuloToolStripMenuItem"
        Me.ConfiguracionModuloToolStripMenuItem.Size = New System.Drawing.Size(165, 20)
        Me.ConfiguracionModuloToolStripMenuItem.Text = "Configuración módulo..."
        '
        'GeneralesToolStripMenuItem
        '
        Me.GeneralesToolStripMenuItem.Name = "GeneralesToolStripMenuItem"
        Me.GeneralesToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.GeneralesToolStripMenuItem.Text = "Parámetros..."
        '
        'DocumentosToolStripMenuItem
        '
        Me.DocumentosToolStripMenuItem.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnCopiar
        Me.DocumentosToolStripMenuItem.Name = "DocumentosToolStripMenuItem"
        Me.DocumentosToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.DocumentosToolStripMenuItem.Text = "Documentos..."
        '
        'CamposToolStripMenuItem
        '
        Me.CamposToolStripMenuItem.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.MainField
        Me.CamposToolStripMenuItem.Name = "CamposToolStripMenuItem"
        Me.CamposToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.CamposToolStripMenuItem.Text = "Campos..."
        '
        'TablasAsociadasToolStripMenuItem
        '
        Me.TablasAsociadasToolStripMenuItem.Name = "TablasAsociadasToolStripMenuItem"
        Me.TablasAsociadasToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.TablasAsociadasToolStripMenuItem.Text = "Tablas Asociadas ..."
        '
        'ValidacionesToolStripMenuItem
        '
        Me.ValidacionesToolStripMenuItem.Name = "ValidacionesToolStripMenuItem"
        Me.ValidacionesToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ValidacionesToolStripMenuItem.Text = "Validaciones..."
        '
        'TipoOTToolStripMenuItem1
        '
        Me.TipoOTToolStripMenuItem1.Name = "TipoOTToolStripMenuItem1"
        Me.TipoOTToolStripMenuItem1.Size = New System.Drawing.Size(193, 22)
        Me.TipoOTToolStripMenuItem1.Text = "Tipo OT..."
        '
        'ContenedorCamposToolStripMenuItem1
        '
        Me.ContenedorCamposToolStripMenuItem1.Name = "ContenedorCamposToolStripMenuItem1"
        Me.ContenedorCamposToolStripMenuItem1.Size = New System.Drawing.Size(193, 22)
        Me.ContenedorCamposToolStripMenuItem1.Text = "Contenedor Campos..."
        '
        'PrecintoCamposToolStripMenuItem1
        '
        Me.PrecintoCamposToolStripMenuItem1.Name = "PrecintoCamposToolStripMenuItem1"
        Me.PrecintoCamposToolStripMenuItem1.Size = New System.Drawing.Size(193, 22)
        Me.PrecintoCamposToolStripMenuItem1.Text = "Precinto Campos..."
        '
        'EmpaqueCamposToolStripMenuItem
        '
        Me.EmpaqueCamposToolStripMenuItem.Name = "EmpaqueCamposToolStripMenuItem"
        Me.EmpaqueCamposToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.EmpaqueCamposToolStripMenuItem.Text = "Empaque Campos..."
        '
        'ProcesoToolStripMenuItem
        '
        Me.ProcesoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AccesosToolStripMenuItem, Me.SeguimientoCargueToolStripMenuItem, Me.LogoFirmaToolStripMenuItem})
        Me.ProcesoToolStripMenuItem.Name = "ProcesoToolStripMenuItem"
        Me.ProcesoToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.ProcesoToolStripMenuItem.Text = "Proceso"
        '
        'AccesosToolStripMenuItem
        '
        Me.AccesosToolStripMenuItem.Name = "AccesosToolStripMenuItem"
        Me.AccesosToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.AccesosToolStripMenuItem.Text = "Accesos..."
        '
        'SeguimientoCargueToolStripMenuItem
        '
        Me.SeguimientoCargueToolStripMenuItem.Name = "SeguimientoCargueToolStripMenuItem"
        Me.SeguimientoCargueToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.SeguimientoCargueToolStripMenuItem.Text = "Seguimiento Cargue"
        '
        'LogoFirmaToolStripMenuItem
        '
        Me.LogoFirmaToolStripMenuItem.Name = "LogoFirmaToolStripMenuItem"
        Me.LogoFirmaToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.LogoFirmaToolStripMenuItem.Text = "Logo - Firma"
        '
        'ProcesosEspecialesToolStripMenuItem
        '
        Me.ProcesosEspecialesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RechazosToolStripMenuItem, Me.ExportaPDFToolStripMenuItem, Me.ValidacionesDinámicasToolStripMenuItem})
        Me.ProcesosEspecialesToolStripMenuItem.Name = "ProcesosEspecialesToolStripMenuItem"
        Me.ProcesosEspecialesToolStripMenuItem.Size = New System.Drawing.Size(122, 20)
        Me.ProcesosEspecialesToolStripMenuItem.Text = "Procesos Especiales"
        Me.ProcesosEspecialesToolStripMenuItem.Visible = False
        '
        'RechazosToolStripMenuItem
        '
        Me.RechazosToolStripMenuItem.Name = "RechazosToolStripMenuItem"
        Me.RechazosToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.RechazosToolStripMenuItem.Text = "Rechazos"
        '
        'ExportaPDFToolStripMenuItem
        '
        Me.ExportaPDFToolStripMenuItem.Name = "ExportaPDFToolStripMenuItem"
        Me.ExportaPDFToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ExportaPDFToolStripMenuItem.Text = "Exporta a PDF"
        '
        'ValidacionesDinámicasToolStripMenuItem
        '
        Me.ValidacionesDinámicasToolStripMenuItem.Name = "ValidacionesDinámicasToolStripMenuItem"
        Me.ValidacionesDinámicasToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ValidacionesDinámicasToolStripMenuItem.Text = "Validaciones Dinámicas"
        '
        'ProcesoCierreToolStripMenuItem
        '
        Me.ProcesoCierreToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CargueLogToolStripMenuItem, Me.GenerarHojaDeControlToolStripMenuItem, Me.GenerarRotulosToolStripMenuItem, Me.GenerarRotulosCajaToolStripMenuItem, Me.GenerarFuidPorCajaToolStripMenuItem, Me.GenerarFuidToolStripMenuItem, Me.ReemplazarImagenesToolStripMenuItem, Me.CierreToolStripMenuItem, Me.CredivaloresToolStripMenuItem, Me.LogCargaToolStripMenuItem, Me.GenerarEstampadoToolStripMenuItem})
        Me.ProcesoCierreToolStripMenuItem.Name = "ProcesoCierreToolStripMenuItem"
        Me.ProcesoCierreToolStripMenuItem.Size = New System.Drawing.Size(111, 20)
        Me.ProcesoCierreToolStripMenuItem.Text = "Proceso Generico"
        '
        'CargueLogToolStripMenuItem
        '
        Me.CargueLogToolStripMenuItem.Name = "CargueLogToolStripMenuItem"
        Me.CargueLogToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.CargueLogToolStripMenuItem.Text = "Cargue Log"
        '
        'GenerarHojaDeControlToolStripMenuItem
        '
        Me.GenerarHojaDeControlToolStripMenuItem.Name = "GenerarHojaDeControlToolStripMenuItem"
        Me.GenerarHojaDeControlToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.GenerarHojaDeControlToolStripMenuItem.Text = "Generar Hoja de Control"
        Me.GenerarHojaDeControlToolStripMenuItem.Visible = False
        '
        'GenerarRotulosToolStripMenuItem
        '
        Me.GenerarRotulosToolStripMenuItem.Name = "GenerarRotulosToolStripMenuItem"
        Me.GenerarRotulosToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.GenerarRotulosToolStripMenuItem.Text = "Generar Rotulos Carpeta"
        Me.GenerarRotulosToolStripMenuItem.Visible = False
        '
        'GenerarRotulosCajaToolStripMenuItem
        '
        Me.GenerarRotulosCajaToolStripMenuItem.Name = "GenerarRotulosCajaToolStripMenuItem"
        Me.GenerarRotulosCajaToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.GenerarRotulosCajaToolStripMenuItem.Text = "Generar Rotulos Caja"
        Me.GenerarRotulosCajaToolStripMenuItem.Visible = False
        '
        'GenerarFuidPorCajaToolStripMenuItem
        '
        Me.GenerarFuidPorCajaToolStripMenuItem.Name = "GenerarFuidPorCajaToolStripMenuItem"
        Me.GenerarFuidPorCajaToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.GenerarFuidPorCajaToolStripMenuItem.Text = "Generar Fuid por Caja"
        Me.GenerarFuidPorCajaToolStripMenuItem.Visible = False
        '
        'GenerarFuidToolStripMenuItem
        '
        Me.GenerarFuidToolStripMenuItem.Name = "GenerarFuidToolStripMenuItem"
        Me.GenerarFuidToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.GenerarFuidToolStripMenuItem.Text = "Generar Fuid Global"
        Me.GenerarFuidToolStripMenuItem.Visible = False
        '
        'ReemplazarImagenesToolStripMenuItem
        '
        Me.ReemplazarImagenesToolStripMenuItem.Name = "ReemplazarImagenesToolStripMenuItem"
        Me.ReemplazarImagenesToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.ReemplazarImagenesToolStripMenuItem.Text = "Reemplazar Imagenes"
        Me.ReemplazarImagenesToolStripMenuItem.Visible = False
        '
        'CierreToolStripMenuItem
        '
        Me.CierreToolStripMenuItem.Name = "CierreToolStripMenuItem"
        Me.CierreToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.CierreToolStripMenuItem.Text = "Cierre"
        '
        'CredivaloresToolStripMenuItem
        '
        Me.CredivaloresToolStripMenuItem.Name = "CredivaloresToolStripMenuItem"
        Me.CredivaloresToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.CredivaloresToolStripMenuItem.Text = "LogCredivalores"
        Me.CredivaloresToolStripMenuItem.Visible = False
        '
        'LogCargaToolStripMenuItem
        '
        Me.LogCargaToolStripMenuItem.Enabled = False
        Me.LogCargaToolStripMenuItem.Name = "LogCargaToolStripMenuItem"
        Me.LogCargaToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.LogCargaToolStripMenuItem.Text = "Log Web Banco Popular"
        Me.LogCargaToolStripMenuItem.Visible = False
        '
        'GenerarEstampadoToolStripMenuItem
        '
        Me.GenerarEstampadoToolStripMenuItem.Name = "GenerarEstampadoToolStripMenuItem"
        Me.GenerarEstampadoToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.GenerarEstampadoToolStripMenuItem.Text = "Generación de Estampado"
        Me.GenerarEstampadoToolStripMenuItem.Visible = False
        '
        'MainTabControl
        '
        Me.MainTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.MainTabControl.Controls.Add(Me.ProcesoTabPage)
        Me.MainTabControl.Controls.Add(Me.RelevoTabPage)
        Me.MainTabControl.Controls.Add(Me.BusquedaTabPage)
        Me.MainTabControl.Controls.Add(Me.DigitalizacionTabPage)
        Me.MainTabControl.Controls.Add(Me.BusquedaLoteTabPage)
        Me.MainTabControl.Controls.Add(Me.InformesTabPage)
        Me.MainTabControl.Controls.Add(Me.RelevoCentralTabPage)
        Me.MainTabControl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTabControl.HotTrack = True
        Me.MainTabControl.ImageList = Me.IconosImageList
        Me.MainTabControl.ItemSize = New System.Drawing.Size(125, 50)
        Me.MainTabControl.Location = New System.Drawing.Point(0, 24)
        Me.MainTabControl.Multiline = True
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.Padding = New System.Drawing.Point(1, 1)
        Me.MainTabControl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MainTabControl.SelectedIndex = 0
        Me.MainTabControl.Size = New System.Drawing.Size(962, 524)
        Me.MainTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.MainTabControl.TabIndex = 1
        '
        'ProcesoTabPage
        '
        Me.ProcesoTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.ProcesoTabPage.Controls.Add(Me.WSPanel)
        Me.ProcesoTabPage.ImageIndex = 0
        Me.ProcesoTabPage.Location = New System.Drawing.Point(4, 54)
        Me.ProcesoTabPage.Name = "ProcesoTabPage"
        Me.ProcesoTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ProcesoTabPage.Size = New System.Drawing.Size(954, 466)
        Me.ProcesoTabPage.TabIndex = 0
        Me.ProcesoTabPage.Text = "Proceso"
        '
        'WSPanel
        '
        Me.WSPanel.Controls.Add(Me.EnviarCorreoButton)
        Me.WSPanel.Controls.Add(Me.SeguimientoCorreoButton)
        Me.WSPanel.Controls.Add(Me.ExportarButton)
        Me.WSPanel.Controls.Add(Me.ExportarCredivaloresButton)
        Me.WSPanel.Controls.Add(Me.CorreccionMaquinaButton)
        Me.WSPanel.Controls.Add(Me.CalidadRecortesButton)
        Me.WSPanel.Controls.Add(Me.ProcesoAdicionalCapturaButton)
        Me.WSPanel.Controls.Add(Me.Flecha7)
        Me.WSPanel.Controls.Add(Me.Flecha6)
        Me.WSPanel.Controls.Add(Me.Flecha4)
        Me.WSPanel.Controls.Add(Me.EmpaqueButton)
        Me.WSPanel.Controls.Add(Me.Flecha5)
        Me.WSPanel.Controls.Add(Me.DestapeButton)
        Me.WSPanel.Controls.Add(Me.OTButton)
        Me.WSPanel.Controls.Add(Me.FechaProcesoButton)
        Me.WSPanel.Controls.Add(Me.UsaDominioExternoLabel)
        Me.WSPanel.Controls.Add(Me.TerceraCapturaFlecha)
        Me.WSPanel.Controls.Add(Me.CalidadCapturaButton)
        Me.WSPanel.Controls.Add(Me.UsaCacheLocalLabel)
        Me.WSPanel.Controls.Add(Me.ValidacionesOpcionalesButton)
        Me.WSPanel.Controls.Add(Me.SeguimientoCargueButton)
        Me.WSPanel.Controls.Add(Me.Flecha3)
        Me.WSPanel.Controls.Add(Me.Flecha2)
        Me.WSPanel.Controls.Add(Me.PreCapturaButton)
        Me.WSPanel.Controls.Add(Me.Flecha1)
        Me.WSPanel.Controls.Add(Me.ExportarFlecha)
        Me.WSPanel.Controls.Add(Me.Publicar1Flecha)
        Me.WSPanel.Controls.Add(Me.Publicar2Flecha)
        Me.WSPanel.Controls.Add(Me.Publicar3Flecha)
        Me.WSPanel.Controls.Add(Me.RecortesButton)
        Me.WSPanel.Controls.Add(Me.SegundaCapturaFlecha)
        Me.WSPanel.Controls.Add(Me.CapturaFlecha)
        Me.WSPanel.Controls.Add(Me.PreindexarFlecha)
        Me.WSPanel.Controls.Add(Me.CargueFlecha)
        Me.WSPanel.Controls.Add(Me.TerceraCapturaButton)
        Me.WSPanel.Controls.Add(Me.SegundaCapturaButton)
        Me.WSPanel.Controls.Add(Me.ReprocesosButton)
        Me.WSPanel.Controls.Add(Me.CapturaButton)
        Me.WSPanel.Controls.Add(Me.IndexarButton)
        Me.WSPanel.Controls.Add(Me.CargueButton)
        Me.WSPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WSPanel.Location = New System.Drawing.Point(3, 3)
        Me.WSPanel.Name = "WSPanel"
        Me.WSPanel.Size = New System.Drawing.Size(948, 460)
        Me.WSPanel.TabIndex = 1
        '
        'EnviarCorreoButton
        '
        Me.EnviarCorreoButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.EnviarCorreoButton.BackColor = System.Drawing.Color.White
        Me.EnviarCorreoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EnviarCorreoButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EnviarCorreoButton.Image = CType(resources.GetObject("EnviarCorreoButton.Image"), System.Drawing.Image)
        Me.EnviarCorreoButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.EnviarCorreoButton.Location = New System.Drawing.Point(455, 393)
        Me.EnviarCorreoButton.Name = "EnviarCorreoButton"
        Me.EnviarCorreoButton.Size = New System.Drawing.Size(100, 61)
        Me.EnviarCorreoButton.TabIndex = 86
        Me.EnviarCorreoButton.Text = "Enviar Correos"
        Me.EnviarCorreoButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.EnviarCorreoButton.UseVisualStyleBackColor = False
        '
        'SeguimientoCorreoButton
        '
        Me.SeguimientoCorreoButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SeguimientoCorreoButton.BackColor = System.Drawing.Color.White
        Me.SeguimientoCorreoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SeguimientoCorreoButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SeguimientoCorreoButton.Image = CType(resources.GetObject("SeguimientoCorreoButton.Image"), System.Drawing.Image)
        Me.SeguimientoCorreoButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.SeguimientoCorreoButton.Location = New System.Drawing.Point(335, 393)
        Me.SeguimientoCorreoButton.Name = "SeguimientoCorreoButton"
        Me.SeguimientoCorreoButton.Size = New System.Drawing.Size(100, 61)
        Me.SeguimientoCorreoButton.TabIndex = 85
        Me.SeguimientoCorreoButton.Text = "Seguimiento Correo (F12)"
        Me.SeguimientoCorreoButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.SeguimientoCorreoButton.UseVisualStyleBackColor = False
        '
        'ExportarButton
        '
        Me.ExportarButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ExportarButton.BackColor = System.Drawing.Color.White
        Me.ExportarButton.Enabled = False
        Me.ExportarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExportarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExportarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Export
        Me.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ExportarButton.Location = New System.Drawing.Point(818, 314)
        Me.ExportarButton.Name = "ExportarButton"
        Me.ExportarButton.Size = New System.Drawing.Size(100, 60)
        Me.ExportarButton.TabIndex = 22
        Me.ExportarButton.Tag = "Ctrl + E"
        Me.ExportarButton.Text = "&Exportar (F9)"
        Me.ExportarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ExportarButton.UseVisualStyleBackColor = False
        '
        'ExportarCredivaloresButton
        '
        Me.ExportarCredivaloresButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ExportarCredivaloresButton.BackColor = System.Drawing.Color.White
        Me.ExportarCredivaloresButton.Enabled = False
        Me.ExportarCredivaloresButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExportarCredivaloresButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExportarCredivaloresButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Export
        Me.ExportarCredivaloresButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ExportarCredivaloresButton.Location = New System.Drawing.Point(818, 393)
        Me.ExportarCredivaloresButton.Name = "ExportarCredivaloresButton"
        Me.ExportarCredivaloresButton.Size = New System.Drawing.Size(100, 60)
        Me.ExportarCredivaloresButton.TabIndex = 83
        Me.ExportarCredivaloresButton.Tag = "Ctrl + E"
        Me.ExportarCredivaloresButton.Text = "&Exportar Proyectos"
        Me.ExportarCredivaloresButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ExportarCredivaloresButton.UseVisualStyleBackColor = False
        Me.ExportarCredivaloresButton.Visible = False
        '
        'CorreccionMaquinaButton
        '
        Me.CorreccionMaquinaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CorreccionMaquinaButton.BackColor = System.Drawing.Color.DodgerBlue
        Me.CorreccionMaquinaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CorreccionMaquinaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CorreccionMaquinaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.prepare
        Me.CorreccionMaquinaButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CorreccionMaquinaButton.Location = New System.Drawing.Point(660, 118)
        Me.CorreccionMaquinaButton.Name = "CorreccionMaquinaButton"
        Me.CorreccionMaquinaButton.Size = New System.Drawing.Size(100, 68)
        Me.CorreccionMaquinaButton.TabIndex = 82
        Me.CorreccionMaquinaButton.Text = "Corrección Máquina"
        Me.CorreccionMaquinaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CorreccionMaquinaButton.UseVisualStyleBackColor = False
        '
        'CalidadRecortesButton
        '
        Me.CalidadRecortesButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CalidadRecortesButton.BackColor = System.Drawing.Color.Khaki
        Me.CalidadRecortesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CalidadRecortesButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CalidadRecortesButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.publicar
        Me.CalidadRecortesButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CalidadRecortesButton.Location = New System.Drawing.Point(818, 218)
        Me.CalidadRecortesButton.Name = "CalidadRecortesButton"
        Me.CalidadRecortesButton.Size = New System.Drawing.Size(100, 60)
        Me.CalidadRecortesButton.TabIndex = 80
        Me.CalidadRecortesButton.Text = "Calidad Recortes"
        Me.CalidadRecortesButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CalidadRecortesButton.UseVisualStyleBackColor = False
        '
        'ProcesoAdicionalCapturaButton
        '
        Me.ProcesoAdicionalCapturaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ProcesoAdicionalCapturaButton.BackColor = System.Drawing.Color.White
        Me.ProcesoAdicionalCapturaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ProcesoAdicionalCapturaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProcesoAdicionalCapturaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.publicar
        Me.ProcesoAdicionalCapturaButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ProcesoAdicionalCapturaButton.Location = New System.Drawing.Point(28, 314)
        Me.ProcesoAdicionalCapturaButton.Name = "ProcesoAdicionalCapturaButton"
        Me.ProcesoAdicionalCapturaButton.Size = New System.Drawing.Size(100, 60)
        Me.ProcesoAdicionalCapturaButton.TabIndex = 81
        Me.ProcesoAdicionalCapturaButton.Text = "Adicional Captura"
        Me.ProcesoAdicionalCapturaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ProcesoAdicionalCapturaButton.UseVisualStyleBackColor = False
        '
        'Flecha7
        '
        Me.Flecha7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Flecha7.ArrowWidth = 10
        Me.Flecha7.BackColor = System.Drawing.Color.Transparent
        Me.Flecha7.BorderColor = System.Drawing.Color.Black
        Me.Flecha7.BorderWidth = CType(1, Byte)
        Me.Flecha7.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.Flecha7.FillColor = System.Drawing.Color.White
        Me.Flecha7.Location = New System.Drawing.Point(818, 280)
        Me.Flecha7.Name = "Flecha7"
        Me.Flecha7.Size = New System.Drawing.Size(100, 29)
        Me.Flecha7.TabIndex = 79
        Me.Flecha7.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN
        '
        'Flecha6
        '
        Me.Flecha6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Flecha6.ArrowWidth = 10
        Me.Flecha6.BackColor = System.Drawing.Color.Transparent
        Me.Flecha6.BorderColor = System.Drawing.Color.Black
        Me.Flecha6.BorderWidth = CType(1, Byte)
        Me.Flecha6.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.Flecha6.FillColor = System.Drawing.Color.White
        Me.Flecha6.Location = New System.Drawing.Point(455, 49)
        Me.Flecha6.Name = "Flecha6"
        Me.Flecha6.Size = New System.Drawing.Size(46, 60)
        Me.Flecha6.TabIndex = 77
        Me.Flecha6.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'Flecha4
        '
        Me.Flecha4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Flecha4.ArrowWidth = 10
        Me.Flecha4.BackColor = System.Drawing.Color.Transparent
        Me.Flecha4.BorderColor = System.Drawing.Color.Black
        Me.Flecha4.BorderWidth = CType(1, Byte)
        Me.Flecha4.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.Flecha4.FillColor = System.Drawing.Color.White
        Me.Flecha4.Location = New System.Drawing.Point(296, 49)
        Me.Flecha4.Name = "Flecha4"
        Me.Flecha4.Size = New System.Drawing.Size(46, 60)
        Me.Flecha4.TabIndex = 76
        Me.Flecha4.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'EmpaqueButton
        '
        Me.EmpaqueButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.EmpaqueButton.BackColor = System.Drawing.Color.White
        Me.EmpaqueButton.Enabled = False
        Me.EmpaqueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EmpaqueButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmpaqueButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Export
        Me.EmpaqueButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.EmpaqueButton.Location = New System.Drawing.Point(507, 49)
        Me.EmpaqueButton.Name = "EmpaqueButton"
        Me.EmpaqueButton.Size = New System.Drawing.Size(100, 60)
        Me.EmpaqueButton.TabIndex = 75
        Me.EmpaqueButton.Text = "Empaque"
        Me.EmpaqueButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.EmpaqueButton.UseVisualStyleBackColor = False
        '
        'Flecha5
        '
        Me.Flecha5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Flecha5.ArrowWidth = 10
        Me.Flecha5.BackColor = System.Drawing.Color.Transparent
        Me.Flecha5.BorderColor = System.Drawing.Color.Black
        Me.Flecha5.BorderWidth = CType(1, Byte)
        Me.Flecha5.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.Flecha5.FillColor = System.Drawing.Color.White
        Me.Flecha5.Location = New System.Drawing.Point(135, 49)
        Me.Flecha5.Name = "Flecha5"
        Me.Flecha5.Size = New System.Drawing.Size(46, 60)
        Me.Flecha5.TabIndex = 73
        Me.Flecha5.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'DestapeButton
        '
        Me.DestapeButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.DestapeButton.BackColor = System.Drawing.Color.White
        Me.DestapeButton.Enabled = False
        Me.DestapeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DestapeButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DestapeButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.folder_add
        Me.DestapeButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.DestapeButton.Location = New System.Drawing.Point(347, 49)
        Me.DestapeButton.Name = "DestapeButton"
        Me.DestapeButton.Size = New System.Drawing.Size(100, 60)
        Me.DestapeButton.TabIndex = 72
        Me.DestapeButton.Text = "Destape"
        Me.DestapeButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.DestapeButton.UseVisualStyleBackColor = False
        '
        'OTButton
        '
        Me.OTButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OTButton.BackColor = System.Drawing.Color.White
        Me.OTButton.Enabled = False
        Me.OTButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OTButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OTButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.prepare
        Me.OTButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.OTButton.Location = New System.Drawing.Point(188, 49)
        Me.OTButton.Name = "OTButton"
        Me.OTButton.Size = New System.Drawing.Size(100, 60)
        Me.OTButton.TabIndex = 71
        Me.OTButton.Text = "OT"
        Me.OTButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.OTButton.UseVisualStyleBackColor = False
        '
        'FechaProcesoButton
        '
        Me.FechaProcesoButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FechaProcesoButton.BackColor = System.Drawing.Color.White
        Me.FechaProcesoButton.Enabled = False
        Me.FechaProcesoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.FechaProcesoButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FechaProcesoButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.MesaImagenes
        Me.FechaProcesoButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.FechaProcesoButton.Location = New System.Drawing.Point(28, 49)
        Me.FechaProcesoButton.Name = "FechaProcesoButton"
        Me.FechaProcesoButton.Size = New System.Drawing.Size(100, 60)
        Me.FechaProcesoButton.TabIndex = 70
        Me.FechaProcesoButton.Text = "Fecha Proceso"
        Me.FechaProcesoButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.FechaProcesoButton.UseVisualStyleBackColor = False
        '
        'UsaDominioExternoLabel
        '
        Me.UsaDominioExternoLabel.AutoSize = True
        Me.UsaDominioExternoLabel.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsaDominioExternoLabel.ForeColor = System.Drawing.Color.Black
        Me.UsaDominioExternoLabel.Location = New System.Drawing.Point(5, 29)
        Me.UsaDominioExternoLabel.Name = "UsaDominioExternoLabel"
        Me.UsaDominioExternoLabel.Size = New System.Drawing.Size(297, 24)
        Me.UsaDominioExternoLabel.TabIndex = 69
        Me.UsaDominioExternoLabel.Text = "Dominio externo no definido"
        '
        'TerceraCapturaFlecha
        '
        Me.TerceraCapturaFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TerceraCapturaFlecha.ArrowWidth = 10
        Me.TerceraCapturaFlecha.BackColor = System.Drawing.Color.Transparent
        Me.TerceraCapturaFlecha.BorderColor = System.Drawing.Color.Black
        Me.TerceraCapturaFlecha.BorderWidth = CType(1, Byte)
        Me.TerceraCapturaFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TerceraCapturaFlecha.FillColor = System.Drawing.Color.IndianRed
        Me.TerceraCapturaFlecha.Location = New System.Drawing.Point(455, 218)
        Me.TerceraCapturaFlecha.Name = "TerceraCapturaFlecha"
        Me.TerceraCapturaFlecha.Size = New System.Drawing.Size(46, 60)
        Me.TerceraCapturaFlecha.TabIndex = 67
        Me.TerceraCapturaFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'CalidadCapturaButton
        '
        Me.CalidadCapturaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CalidadCapturaButton.BackColor = System.Drawing.Color.LightGreen
        Me.CalidadCapturaButton.Enabled = False
        Me.CalidadCapturaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CalidadCapturaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CalidadCapturaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Terceracaptura
        Me.CalidadCapturaButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CalidadCapturaButton.Location = New System.Drawing.Point(507, 218)
        Me.CalidadCapturaButton.Name = "CalidadCapturaButton"
        Me.CalidadCapturaButton.Size = New System.Drawing.Size(100, 60)
        Me.CalidadCapturaButton.TabIndex = 66
        Me.CalidadCapturaButton.Tag = "Ctrl + T"
        Me.CalidadCapturaButton.Text = "Calidad Captura"
        Me.CalidadCapturaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CalidadCapturaButton.UseVisualStyleBackColor = False
        '
        'UsaCacheLocalLabel
        '
        Me.UsaCacheLocalLabel.AutoSize = True
        Me.UsaCacheLocalLabel.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsaCacheLocalLabel.ForeColor = System.Drawing.Color.Black
        Me.UsaCacheLocalLabel.Location = New System.Drawing.Point(5, 5)
        Me.UsaCacheLocalLabel.Name = "UsaCacheLocalLabel"
        Me.UsaCacheLocalLabel.Size = New System.Drawing.Size(191, 24)
        Me.UsaCacheLocalLabel.TabIndex = 65
        Me.UsaCacheLocalLabel.Text = "Cache no definido"
        '
        'ValidacionesOpcionalesButton
        '
        Me.ValidacionesOpcionalesButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ValidacionesOpcionalesButton.BackColor = System.Drawing.Color.Khaki
        Me.ValidacionesOpcionalesButton.Enabled = False
        Me.ValidacionesOpcionalesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ValidacionesOpcionalesButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ValidacionesOpcionalesButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.MesaImagenes
        Me.ValidacionesOpcionalesButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ValidacionesOpcionalesButton.Location = New System.Drawing.Point(28, 393)
        Me.ValidacionesOpcionalesButton.Name = "ValidacionesOpcionalesButton"
        Me.ValidacionesOpcionalesButton.Size = New System.Drawing.Size(100, 70)
        Me.ValidacionesOpcionalesButton.TabIndex = 64
        Me.ValidacionesOpcionalesButton.Text = "&Validaciones opcionales (F11)"
        Me.ValidacionesOpcionalesButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ValidacionesOpcionalesButton.UseVisualStyleBackColor = False
        '
        'SeguimientoCargueButton
        '
        Me.SeguimientoCargueButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SeguimientoCargueButton.BackColor = System.Drawing.Color.White
        Me.SeguimientoCargueButton.Enabled = False
        Me.SeguimientoCargueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SeguimientoCargueButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SeguimientoCargueButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.MesaImagenes
        Me.SeguimientoCargueButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.SeguimientoCargueButton.Location = New System.Drawing.Point(660, 48)
        Me.SeguimientoCargueButton.Name = "SeguimientoCargueButton"
        Me.SeguimientoCargueButton.Size = New System.Drawing.Size(100, 61)
        Me.SeguimientoCargueButton.TabIndex = 63
        Me.SeguimientoCargueButton.Text = "&Seguimiento Cargue (F10)"
        Me.SeguimientoCargueButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.SeguimientoCargueButton.UseVisualStyleBackColor = False
        '
        'Flecha3
        '
        Me.Flecha3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Flecha3.ArrowWidth = 10
        Me.Flecha3.BackColor = System.Drawing.Color.Transparent
        Me.Flecha3.BorderColor = System.Drawing.Color.Black
        Me.Flecha3.BorderWidth = CType(1, Byte)
        Me.Flecha3.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.Flecha3.FillColor = System.Drawing.Color.White
        Me.Flecha3.Location = New System.Drawing.Point(296, 118)
        Me.Flecha3.Name = "Flecha3"
        Me.Flecha3.Size = New System.Drawing.Size(46, 60)
        Me.Flecha3.TabIndex = 62
        Me.Flecha3.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'Flecha2
        '
        Me.Flecha2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Flecha2.ArrowWidth = 10
        Me.Flecha2.BackColor = System.Drawing.Color.Transparent
        Me.Flecha2.BorderColor = System.Drawing.Color.Black
        Me.Flecha2.BorderWidth = CType(1, Byte)
        Me.Flecha2.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.Flecha2.FillColor = System.Drawing.Color.Khaki
        Me.Flecha2.Location = New System.Drawing.Point(766, 218)
        Me.Flecha2.Name = "Flecha2"
        Me.Flecha2.Size = New System.Drawing.Size(46, 60)
        Me.Flecha2.TabIndex = 61
        Me.Flecha2.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'PreCapturaButton
        '
        Me.PreCapturaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PreCapturaButton.BackColor = System.Drawing.Color.White
        Me.PreCapturaButton.Enabled = False
        Me.PreCapturaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PreCapturaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PreCapturaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Indexar
        Me.PreCapturaButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.PreCapturaButton.Location = New System.Drawing.Point(348, 118)
        Me.PreCapturaButton.Name = "PreCapturaButton"
        Me.PreCapturaButton.Size = New System.Drawing.Size(100, 60)
        Me.PreCapturaButton.TabIndex = 60
        Me.PreCapturaButton.Text = "P&re Captura (F5)"
        Me.PreCapturaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.PreCapturaButton.UseVisualStyleBackColor = False
        '
        'Flecha1
        '
        Me.Flecha1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Flecha1.ArrowWidth = 10
        Me.Flecha1.BackColor = System.Drawing.Color.Transparent
        Me.Flecha1.BorderColor = System.Drawing.Color.Black
        Me.Flecha1.BorderWidth = CType(1, Byte)
        Me.Flecha1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.Flecha1.FillColor = System.Drawing.Color.White
        Me.Flecha1.Location = New System.Drawing.Point(266, 183)
        Me.Flecha1.Name = "Flecha1"
        Me.Flecha1.Size = New System.Drawing.Size(299, 25)
        Me.Flecha1.TabIndex = 59
        Me.Flecha1.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN_LEFT
        '
        'ExportarFlecha
        '
        Me.ExportarFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ExportarFlecha.ArrowWidth = 10
        Me.ExportarFlecha.BackColor = System.Drawing.Color.Transparent
        Me.ExportarFlecha.BorderColor = System.Drawing.Color.Black
        Me.ExportarFlecha.BorderWidth = CType(1, Byte)
        Me.ExportarFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.ExportarFlecha.FillColor = System.Drawing.Color.DarkSeaGreen
        Me.ExportarFlecha.Location = New System.Drawing.Point(614, 218)
        Me.ExportarFlecha.Name = "ExportarFlecha"
        Me.ExportarFlecha.Size = New System.Drawing.Size(40, 60)
        Me.ExportarFlecha.TabIndex = 58
        Me.ExportarFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'Publicar1Flecha
        '
        Me.Publicar1Flecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Publicar1Flecha.ArrowWidth = 10
        Me.Publicar1Flecha.BackColor = System.Drawing.Color.Transparent
        Me.Publicar1Flecha.BorderColor = System.Drawing.Color.Black
        Me.Publicar1Flecha.BorderWidth = CType(1, Byte)
        Me.Publicar1Flecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.Publicar1Flecha.FillColor = System.Drawing.Color.DarkSeaGreen
        Me.Publicar1Flecha.Location = New System.Drawing.Point(228, 279)
        Me.Publicar1Flecha.Name = "Publicar1Flecha"
        Me.Publicar1Flecha.Size = New System.Drawing.Size(127, 77)
        Me.Publicar1Flecha.TabIndex = 57
        Me.Publicar1Flecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN_RIGHT
        '
        'Publicar2Flecha
        '
        Me.Publicar2Flecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Publicar2Flecha.ArrowWidth = 10
        Me.Publicar2Flecha.BackColor = System.Drawing.Color.Transparent
        Me.Publicar2Flecha.BorderColor = System.Drawing.Color.Black
        Me.Publicar2Flecha.BorderWidth = CType(1, Byte)
        Me.Publicar2Flecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.Publicar2Flecha.FillColor = System.Drawing.Color.DarkSeaGreen
        Me.Publicar2Flecha.Location = New System.Drawing.Point(388, 279)
        Me.Publicar2Flecha.Name = "Publicar2Flecha"
        Me.Publicar2Flecha.Size = New System.Drawing.Size(137, 77)
        Me.Publicar2Flecha.TabIndex = 56
        Me.Publicar2Flecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN_RIGHT
        '
        'Publicar3Flecha
        '
        Me.Publicar3Flecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Publicar3Flecha.ArrowWidth = 10
        Me.Publicar3Flecha.BackColor = System.Drawing.Color.Transparent
        Me.Publicar3Flecha.BorderColor = System.Drawing.Color.Black
        Me.Publicar3Flecha.BorderWidth = CType(1, Byte)
        Me.Publicar3Flecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.Publicar3Flecha.FillColor = System.Drawing.Color.DarkSeaGreen
        Me.Publicar3Flecha.Location = New System.Drawing.Point(552, 279)
        Me.Publicar3Flecha.Name = "Publicar3Flecha"
        Me.Publicar3Flecha.Size = New System.Drawing.Size(260, 77)
        Me.Publicar3Flecha.TabIndex = 55
        Me.Publicar3Flecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN_RIGHT
        '
        'RecortesButton
        '
        Me.RecortesButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.RecortesButton.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.RecortesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RecortesButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RecortesButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.publicar
        Me.RecortesButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.RecortesButton.Location = New System.Drawing.Point(660, 218)
        Me.RecortesButton.Name = "RecortesButton"
        Me.RecortesButton.Size = New System.Drawing.Size(100, 60)
        Me.RecortesButton.TabIndex = 54
        Me.RecortesButton.Text = "Recortes"
        Me.RecortesButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.RecortesButton.UseVisualStyleBackColor = False
        '
        'SegundaCapturaFlecha
        '
        Me.SegundaCapturaFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SegundaCapturaFlecha.ArrowWidth = 10
        Me.SegundaCapturaFlecha.BackColor = System.Drawing.Color.Transparent
        Me.SegundaCapturaFlecha.BorderColor = System.Drawing.Color.Black
        Me.SegundaCapturaFlecha.BorderWidth = CType(1, Byte)
        Me.SegundaCapturaFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.SegundaCapturaFlecha.FillColor = System.Drawing.Color.IndianRed
        Me.SegundaCapturaFlecha.Location = New System.Drawing.Point(294, 218)
        Me.SegundaCapturaFlecha.Name = "SegundaCapturaFlecha"
        Me.SegundaCapturaFlecha.Size = New System.Drawing.Size(46, 60)
        Me.SegundaCapturaFlecha.TabIndex = 33
        Me.SegundaCapturaFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'CapturaFlecha
        '
        Me.CapturaFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CapturaFlecha.ArrowWidth = 10
        Me.CapturaFlecha.BackColor = System.Drawing.Color.Transparent
        Me.CapturaFlecha.BorderColor = System.Drawing.Color.Black
        Me.CapturaFlecha.BorderWidth = CType(1, Byte)
        Me.CapturaFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.CapturaFlecha.FillColor = System.Drawing.Color.Khaki
        Me.CapturaFlecha.Location = New System.Drawing.Point(133, 218)
        Me.CapturaFlecha.Name = "CapturaFlecha"
        Me.CapturaFlecha.Size = New System.Drawing.Size(46, 60)
        Me.CapturaFlecha.TabIndex = 32
        Me.CapturaFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'PreindexarFlecha
        '
        Me.PreindexarFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PreindexarFlecha.ArrowWidth = 10
        Me.PreindexarFlecha.BackColor = System.Drawing.Color.Transparent
        Me.PreindexarFlecha.BorderColor = System.Drawing.Color.Black
        Me.PreindexarFlecha.BorderWidth = CType(1, Byte)
        Me.PreindexarFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.PreindexarFlecha.FillColor = System.Drawing.Color.White
        Me.PreindexarFlecha.Location = New System.Drawing.Point(188, 180)
        Me.PreindexarFlecha.Name = "PreindexarFlecha"
        Me.PreindexarFlecha.Size = New System.Drawing.Size(100, 36)
        Me.PreindexarFlecha.TabIndex = 31
        Me.PreindexarFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.DOWN
        '
        'CargueFlecha
        '
        Me.CargueFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CargueFlecha.ArrowWidth = 10
        Me.CargueFlecha.BackColor = System.Drawing.Color.Transparent
        Me.CargueFlecha.BorderColor = System.Drawing.Color.Black
        Me.CargueFlecha.BorderWidth = CType(1, Byte)
        Me.CargueFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.CargueFlecha.FillColor = System.Drawing.Color.White
        Me.CargueFlecha.Location = New System.Drawing.Point(135, 118)
        Me.CargueFlecha.Name = "CargueFlecha"
        Me.CargueFlecha.Size = New System.Drawing.Size(46, 60)
        Me.CargueFlecha.TabIndex = 30
        Me.CargueFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'TerceraCapturaButton
        '
        Me.TerceraCapturaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TerceraCapturaButton.BackColor = System.Drawing.Color.IndianRed
        Me.TerceraCapturaButton.Enabled = False
        Me.TerceraCapturaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TerceraCapturaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TerceraCapturaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Terceracaptura
        Me.TerceraCapturaButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.TerceraCapturaButton.Location = New System.Drawing.Point(347, 218)
        Me.TerceraCapturaButton.Name = "TerceraCapturaButton"
        Me.TerceraCapturaButton.Size = New System.Drawing.Size(100, 60)
        Me.TerceraCapturaButton.TabIndex = 26
        Me.TerceraCapturaButton.Tag = "Ctrl + T"
        Me.TerceraCapturaButton.Text = "&Tercera Captura (F8)"
        Me.TerceraCapturaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.TerceraCapturaButton.UseVisualStyleBackColor = False
        '
        'SegundaCapturaButton
        '
        Me.SegundaCapturaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SegundaCapturaButton.BackColor = System.Drawing.Color.Khaki
        Me.SegundaCapturaButton.Enabled = False
        Me.SegundaCapturaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SegundaCapturaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SegundaCapturaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.SegundaCaptura
        Me.SegundaCapturaButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.SegundaCapturaButton.Location = New System.Drawing.Point(186, 218)
        Me.SegundaCapturaButton.Name = "SegundaCapturaButton"
        Me.SegundaCapturaButton.Size = New System.Drawing.Size(100, 60)
        Me.SegundaCapturaButton.TabIndex = 25
        Me.SegundaCapturaButton.Text = "Se&gunda Captura  (F7)"
        Me.SegundaCapturaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.SegundaCapturaButton.UseVisualStyleBackColor = False
        '
        'ReprocesosButton
        '
        Me.ReprocesosButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ReprocesosButton.BackColor = System.Drawing.Color.IndianRed
        Me.ReprocesosButton.Enabled = False
        Me.ReprocesosButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ReprocesosButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReprocesosButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Devoluciones
        Me.ReprocesosButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ReprocesosButton.Location = New System.Drawing.Point(507, 118)
        Me.ReprocesosButton.Name = "ReprocesosButton"
        Me.ReprocesosButton.Size = New System.Drawing.Size(100, 60)
        Me.ReprocesosButton.TabIndex = 23
        Me.ReprocesosButton.Text = "&Reprocesos  (F4)"
        Me.ReprocesosButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ReprocesosButton.UseVisualStyleBackColor = False
        '
        'CapturaButton
        '
        Me.CapturaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CapturaButton.BackColor = System.Drawing.Color.White
        Me.CapturaButton.Enabled = False
        Me.CapturaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CapturaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CapturaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Indexar
        Me.CapturaButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CapturaButton.Location = New System.Drawing.Point(28, 218)
        Me.CapturaButton.Name = "CapturaButton"
        Me.CapturaButton.Size = New System.Drawing.Size(100, 60)
        Me.CapturaButton.TabIndex = 21
        Me.CapturaButton.Text = "Ca&ptura (F6)"
        Me.CapturaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CapturaButton.UseVisualStyleBackColor = False
        '
        'IndexarButton
        '
        Me.IndexarButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.IndexarButton.BackColor = System.Drawing.Color.White
        Me.IndexarButton.Enabled = False
        Me.IndexarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IndexarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IndexarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.PreIndexar
        Me.IndexarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.IndexarButton.Location = New System.Drawing.Point(188, 118)
        Me.IndexarButton.Name = "IndexarButton"
        Me.IndexarButton.Size = New System.Drawing.Size(100, 60)
        Me.IndexarButton.TabIndex = 20
        Me.IndexarButton.Text = "&Indexar (F3)"
        Me.IndexarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.IndexarButton.UseVisualStyleBackColor = False
        '
        'CargueButton
        '
        Me.CargueButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CargueButton.BackColor = System.Drawing.Color.White
        Me.CargueButton.Enabled = False
        Me.CargueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CargueButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CargueButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Cargue
        Me.CargueButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CargueButton.Location = New System.Drawing.Point(28, 118)
        Me.CargueButton.Name = "CargueButton"
        Me.CargueButton.Size = New System.Drawing.Size(100, 60)
        Me.CargueButton.TabIndex = 19
        Me.CargueButton.Text = "&Cargue (F2)"
        Me.CargueButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CargueButton.UseVisualStyleBackColor = False
        '
        'RelevoTabPage
        '
        Me.RelevoTabPage.Controls.Add(Me.UCRelevo)
        Me.RelevoTabPage.ImageIndex = 3
        Me.RelevoTabPage.Location = New System.Drawing.Point(4, 54)
        Me.RelevoTabPage.Name = "RelevoTabPage"
        Me.RelevoTabPage.Size = New System.Drawing.Size(954, 466)
        Me.RelevoTabPage.TabIndex = 3
        Me.RelevoTabPage.Text = "Relevo"
        Me.RelevoTabPage.UseVisualStyleBackColor = True
        '
        'UCRelevo
        '
        Me.UCRelevo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UCRelevo.Location = New System.Drawing.Point(0, 0)
        Me.UCRelevo.Margin = New System.Windows.Forms.Padding(7, 3, 7, 3)
        Me.UCRelevo.Name = "UCRelevo"
        Me.UCRelevo.Size = New System.Drawing.Size(954, 466)
        Me.UCRelevo.TabIndex = 0
        '
        'BusquedaTabPage
        '
        Me.BusquedaTabPage.Controls.Add(Me.WorkSpaceImagingSearchControl)
        Me.BusquedaTabPage.ImageIndex = 1
        Me.BusquedaTabPage.Location = New System.Drawing.Point(4, 54)
        Me.BusquedaTabPage.Name = "BusquedaTabPage"
        Me.BusquedaTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.BusquedaTabPage.Size = New System.Drawing.Size(954, 466)
        Me.BusquedaTabPage.TabIndex = 1
        Me.BusquedaTabPage.Text = "Búsqueda"
        Me.BusquedaTabPage.UseVisualStyleBackColor = True
        '
        'WorkSpaceImagingSearchControl
        '
        Me.WorkSpaceImagingSearchControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WorkSpaceImagingSearchControl.Location = New System.Drawing.Point(3, 3)
        Me.WorkSpaceImagingSearchControl.Margin = New System.Windows.Forms.Padding(7, 3, 7, 3)
        Me.WorkSpaceImagingSearchControl.Name = "WorkSpaceImagingSearchControl"
        Me.WorkSpaceImagingSearchControl.Size = New System.Drawing.Size(948, 460)
        Me.WorkSpaceImagingSearchControl.TabIndex = 0
        '
        'DigitalizacionTabPage
        '
        Me.DigitalizacionTabPage.Controls.Add(Me.WorkSpaceDDODigitalizacionControl)
        Me.DigitalizacionTabPage.ImageIndex = 5
        Me.DigitalizacionTabPage.Location = New System.Drawing.Point(4, 54)
        Me.DigitalizacionTabPage.Name = "DigitalizacionTabPage"
        Me.DigitalizacionTabPage.Size = New System.Drawing.Size(954, 466)
        Me.DigitalizacionTabPage.TabIndex = 5
        Me.DigitalizacionTabPage.Text = "Digitalización"
        Me.DigitalizacionTabPage.UseVisualStyleBackColor = True
        '
        'WorkSpaceDDODigitalizacionControl
        '
        Me.WorkSpaceDDODigitalizacionControl.BackColor = System.Drawing.SystemColors.Control
        Me.WorkSpaceDDODigitalizacionControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WorkSpaceDDODigitalizacionControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WorkSpaceDDODigitalizacionControl.Location = New System.Drawing.Point(0, 0)
        Me.WorkSpaceDDODigitalizacionControl.Margin = New System.Windows.Forms.Padding(7, 3, 7, 3)
        Me.WorkSpaceDDODigitalizacionControl.Name = "WorkSpaceDDODigitalizacionControl"
        Me.WorkSpaceDDODigitalizacionControl.Size = New System.Drawing.Size(954, 466)
        Me.WorkSpaceDDODigitalizacionControl.TabIndex = 0
        '
        'BusquedaLoteTabPage
        '
        Me.BusquedaLoteTabPage.Controls.Add(Me.WorkSpaceImagingSearchControlContenedor)
        Me.BusquedaLoteTabPage.ImageIndex = 1
        Me.BusquedaLoteTabPage.Location = New System.Drawing.Point(4, 54)
        Me.BusquedaLoteTabPage.Name = "BusquedaLoteTabPage"
        Me.BusquedaLoteTabPage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BusquedaLoteTabPage.Size = New System.Drawing.Size(954, 466)
        Me.BusquedaLoteTabPage.TabIndex = 6
        Me.BusquedaLoteTabPage.Text = "Busqueda Lote"
        Me.BusquedaLoteTabPage.UseVisualStyleBackColor = True
        '
        'WorkSpaceImagingSearchControlContenedor
        '
        Me.WorkSpaceImagingSearchControlContenedor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WorkSpaceImagingSearchControlContenedor.Location = New System.Drawing.Point(0, 0)
        Me.WorkSpaceImagingSearchControlContenedor.Margin = New System.Windows.Forms.Padding(7, 3, 7, 3)
        Me.WorkSpaceImagingSearchControlContenedor.Name = "WorkSpaceImagingSearchControlContenedor"
        Me.WorkSpaceImagingSearchControlContenedor.Size = New System.Drawing.Size(954, 466)
        Me.WorkSpaceImagingSearchControlContenedor.TabIndex = 0
        '
        'InformesTabPage
        '
        Me.InformesTabPage.Controls.Add(Me.WorkSpaceImagingReportViewerControl)
        Me.InformesTabPage.ImageIndex = 2
        Me.InformesTabPage.Location = New System.Drawing.Point(4, 54)
        Me.InformesTabPage.Name = "InformesTabPage"
        Me.InformesTabPage.Size = New System.Drawing.Size(954, 466)
        Me.InformesTabPage.TabIndex = 2
        Me.InformesTabPage.Text = "Informes"
        Me.InformesTabPage.UseVisualStyleBackColor = True
        '
        'WorkSpaceImagingReportViewerControl
        '
        Me.WorkSpaceImagingReportViewerControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WorkSpaceImagingReportViewerControl.Location = New System.Drawing.Point(0, 0)
        Me.WorkSpaceImagingReportViewerControl.Margin = New System.Windows.Forms.Padding(7, 3, 7, 3)
        Me.WorkSpaceImagingReportViewerControl.Name = "WorkSpaceImagingReportViewerControl"
        Me.WorkSpaceImagingReportViewerControl.Size = New System.Drawing.Size(954, 466)
        Me.WorkSpaceImagingReportViewerControl.TabIndex = 0
        '
        'RelevoCentralTabPage
        '
        Me.RelevoCentralTabPage.Controls.Add(Me.UCRelevoCentral)
        Me.RelevoCentralTabPage.ImageIndex = 3
        Me.RelevoCentralTabPage.Location = New System.Drawing.Point(4, 54)
        Me.RelevoCentralTabPage.Name = "RelevoCentralTabPage"
        Me.RelevoCentralTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.RelevoCentralTabPage.Size = New System.Drawing.Size(954, 466)
        Me.RelevoCentralTabPage.TabIndex = 7
        Me.RelevoCentralTabPage.Text = "Relevo Central"
        Me.RelevoCentralTabPage.UseVisualStyleBackColor = True
        '
        'UCRelevoCentral
        '
        Me.UCRelevoCentral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UCRelevoCentral.Location = New System.Drawing.Point(3, 3)
        Me.UCRelevoCentral.Margin = New System.Windows.Forms.Padding(7, 3, 7, 3)
        Me.UCRelevoCentral.Name = "UCRelevoCentral"
        Me.UCRelevoCentral.Size = New System.Drawing.Size(948, 460)
        Me.UCRelevoCentral.TabIndex = 0
        '
        'FormImagingWorkSpace
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(962, 548)
        Me.ControlBox = False
        Me.Controls.Add(Me.MainTabControl)
        Me.Controls.Add(Me.MainFormMenuStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MainFormMenuStrip
        Me.Name = "FormImagingWorkSpace"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WorkSpace"
        Me.MainFormMenuStrip.ResumeLayout(False)
        Me.MainFormMenuStrip.PerformLayout()
        Me.MainTabControl.ResumeLayout(False)
        Me.ProcesoTabPage.ResumeLayout(False)
        Me.WSPanel.ResumeLayout(False)
        Me.WSPanel.PerformLayout()
        Me.RelevoTabPage.ResumeLayout(False)
        Me.BusquedaTabPage.ResumeLayout(False)
        Me.DigitalizacionTabPage.ResumeLayout(False)
        Me.BusquedaLoteTabPage.ResumeLayout(False)
        Me.InformesTabPage.ResumeLayout(False)
        Me.RelevoCentralTabPage.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents IconosImageList As System.Windows.Forms.ImageList
    Friend WithEvents ProcesoTabPage As System.Windows.Forms.TabPage
    Public WithEvents WSPanel As System.Windows.Forms.Panel
    Friend WithEvents ExportarFlecha As FlechaControl
    Friend WithEvents Publicar1Flecha As FlechaControl
    Friend WithEvents Publicar2Flecha As FlechaControl
    Friend WithEvents Publicar3Flecha As FlechaControl
    Friend WithEvents RecortesButton As System.Windows.Forms.Button
    Friend WithEvents SegundaCapturaFlecha As FlechaControl
    Friend WithEvents CapturaFlecha As FlechaControl
    Friend WithEvents PreindexarFlecha As FlechaControl
    Friend WithEvents CargueFlecha As FlechaControl
    Friend WithEvents InformesTabPage As System.Windows.Forms.TabPage
    Public WithEvents ConfiguracionModuloToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CamposToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BusquedaTabPage As System.Windows.Forms.TabPage
    Friend WithEvents Flecha1 As FlechaControl
    Public WithEvents MainTabControl As System.Windows.Forms.TabControl
    Friend WithEvents Flecha2 As FlechaControl
    Friend WithEvents Flecha3 As FlechaControl
    Friend WithEvents ValidacionesOpcionalesButton As System.Windows.Forms.Button
    Public WithEvents WorkSpaceImagingSearchControl As ImagingSearchControl
    Public WithEvents WorkSpaceImagingSearchControlContenedor As ImagingSearchControlContenedor
    Public WithEvents UCRelevo As UCRelevo
    Public WithEvents UCRelevoCentral As UCRelevoCentralizador
    Public WithEvents WorkSpaceDDODigitalizacionControl As Miharu.DDO.Forms.UsrDigitaliza
    Public WithEvents WorkSpaceImagingReportViewerControl As DesktopReportViewerControl
    Public WithEvents CargueButton As System.Windows.Forms.Button
    Public WithEvents ExportarButton As System.Windows.Forms.Button
    Friend WithEvents UsaCacheLocalLabel As System.Windows.Forms.Label
    Friend WithEvents DocumentosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TerceraCapturaFlecha As FlechaControl
    Friend WithEvents ValidacionesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GeneralesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsaDominioExternoLabel As System.Windows.Forms.Label
    Friend WithEvents Flecha6 As FlechaControl
    Friend WithEvents Flecha4 As FlechaControl
    Friend WithEvents Flecha5 As FlechaControl
    Public WithEvents FechaProcesoButton As System.Windows.Forms.Button
    Friend WithEvents ContenedorCamposToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrecintoCamposToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TipoOTToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProcesoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SeguimientoCargueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmpaqueCamposToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TablasAsociadasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MainFormMenuStrip As System.Windows.Forms.MenuStrip
    Public WithEvents TerceraCapturaButton As System.Windows.Forms.Button
    Public WithEvents SegundaCapturaButton As System.Windows.Forms.Button
    Public WithEvents ReprocesosButton As System.Windows.Forms.Button
    Public WithEvents CapturaButton As System.Windows.Forms.Button
    Public WithEvents IndexarButton As System.Windows.Forms.Button
    Public WithEvents PreCapturaButton As System.Windows.Forms.Button
    Public WithEvents SeguimientoCargueButton As System.Windows.Forms.Button
    Public WithEvents CalidadCapturaButton As System.Windows.Forms.Button
    Public WithEvents EmpaqueButton As System.Windows.Forms.Button
    Public WithEvents DestapeButton As System.Windows.Forms.Button
    Public WithEvents OTButton As System.Windows.Forms.Button
    Friend WithEvents Flecha7 As FlechaControl
    Friend WithEvents CalidadRecortesButton As System.Windows.Forms.Button
    Friend WithEvents AccesosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ProcesoAdicionalCapturaButton As System.Windows.Forms.Button
    Public WithEvents ProcesosEspecialesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents RechazosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ExportaPDFToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProcesoCierreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CierreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CargueLogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents CorreccionMaquinaButton As System.Windows.Forms.Button
    Public WithEvents ExportarCredivaloresButton As System.Windows.Forms.Button
    Friend WithEvents GenerarRotulosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerarHojaDeControlToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerarRotulosCajaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerarFuidToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerarFuidPorCajaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReemplazarImagenesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CredivaloresToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogCargaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ValidacionesDinámicasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerarEstampadoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents SeguimientoCorreoButton As System.Windows.Forms.Button
    Public WithEvents EnviarCorreoButton As System.Windows.Forms.Button
    Friend WithEvents LogoFirmaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RelevoTabPage As System.Windows.Forms.TabPage
    Friend WithEvents DigitalizacionTabPage As System.Windows.Forms.TabPage
    Friend WithEvents BusquedaLoteTabPage As TabPage
    Friend WithEvents RelevoCentralTabPage As System.Windows.Forms.TabPage
End Class
