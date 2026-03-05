Imports Miharu.Desktop.Controls.Flecha

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCustodyWorkSpace
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCustodyWorkSpace))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HerramientasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SolicitudesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MotivosPorRolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrioridadPorRolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TipoPorRolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EstructuraSolicitudesMasivasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProximosAVencerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControlImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.ProcesoTabPage = New System.Windows.Forms.TabPage()
        Me.WSPanel = New System.Windows.Forms.Panel()
        Me.TrasladoCarpetaButton = New System.Windows.Forms.Button()
        Me.TrasladoCajaButton = New System.Windows.Forms.Button()
        Me.CajaButton = New System.Windows.Forms.Button()
        Me.BovedasButton = New System.Windows.Forms.Button()
        Me.CargueSolicitudesMasivasButton = New System.Windows.Forms.Button()
        Me.Flecha2 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CreacionArqueoButton = New System.Windows.Forms.Button()
        Me.Flecha1 = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.CargueUniversalFlecha = New Miharu.Desktop.Controls.Flecha.FlechaControl()
        Me.DespachoButton = New System.Windows.Forms.Button()
        Me.AdministracionSolicitudesButton = New System.Windows.Forms.Button()
        Me.AtencionSolicitudesButton = New System.Windows.Forms.Button()
        Me.RecepcionButton = New System.Windows.Forms.Button()
        Me.MainTabControl = New System.Windows.Forms.TabControl()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CodigosDeBarrasToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CarpetasYDocumentosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.ProcesoTabPage.SuspendLayout()
        Me.WSPanel.SuspendLayout()
        Me.MainTabControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HerramientasToolStripMenuItem, Me.ReportesToolStripMenuItem, Me.CodigosDeBarrasToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(792, 24)
        Me.MenuStrip1.TabIndex = 37
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HerramientasToolStripMenuItem
        '
        Me.HerramientasToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SolicitudesToolStripMenuItem, Me.EstructuraSolicitudesMasivasToolStripMenuItem})
        Me.HerramientasToolStripMenuItem.Image = Global.Miharu.Custody.Library.My.Resources.Resources.MainSettings
        Me.HerramientasToolStripMenuItem.Name = "HerramientasToolStripMenuItem"
        Me.HerramientasToolStripMenuItem.Size = New System.Drawing.Size(180, 20)
        Me.HerramientasToolStripMenuItem.Text = "Parametrización Solicitudes"
        '
        'SolicitudesToolStripMenuItem
        '
        Me.SolicitudesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MotivosPorRolToolStripMenuItem, Me.PrioridadPorRolToolStripMenuItem, Me.TipoPorRolToolStripMenuItem})
        Me.SolicitudesToolStripMenuItem.Image = Global.Miharu.Custody.Library.My.Resources.Resources.MainSolicitud
        Me.SolicitudesToolStripMenuItem.Name = "SolicitudesToolStripMenuItem"
        Me.SolicitudesToolStripMenuItem.Size = New System.Drawing.Size(241, 22)
        Me.SolicitudesToolStripMenuItem.Text = "Solicitudes"
        '
        'MotivosPorRolToolStripMenuItem
        '
        Me.MotivosPorRolToolStripMenuItem.Image = Global.Miharu.Custody.Library.My.Resources.Resources.MainMotivo
        Me.MotivosPorRolToolStripMenuItem.Name = "MotivosPorRolToolStripMenuItem"
        Me.MotivosPorRolToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.MotivosPorRolToolStripMenuItem.Text = "Motivos por rol..."
        '
        'PrioridadPorRolToolStripMenuItem
        '
        Me.PrioridadPorRolToolStripMenuItem.Image = Global.Miharu.Custody.Library.My.Resources.Resources.MainPriority
        Me.PrioridadPorRolToolStripMenuItem.Name = "PrioridadPorRolToolStripMenuItem"
        Me.PrioridadPorRolToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.PrioridadPorRolToolStripMenuItem.Text = "Prioridad por rol..."
        '
        'TipoPorRolToolStripMenuItem
        '
        Me.TipoPorRolToolStripMenuItem.Image = Global.Miharu.Custody.Library.My.Resources.Resources.MainType
        Me.TipoPorRolToolStripMenuItem.Name = "TipoPorRolToolStripMenuItem"
        Me.TipoPorRolToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.TipoPorRolToolStripMenuItem.Text = "Tipo por rol..."
        '
        'EstructuraSolicitudesMasivasToolStripMenuItem
        '
        Me.EstructuraSolicitudesMasivasToolStripMenuItem.Image = Global.Miharu.Custody.Library.My.Resources.Resources.MainCharge
        Me.EstructuraSolicitudesMasivasToolStripMenuItem.Name = "EstructuraSolicitudesMasivasToolStripMenuItem"
        Me.EstructuraSolicitudesMasivasToolStripMenuItem.Size = New System.Drawing.Size(241, 22)
        Me.EstructuraSolicitudesMasivasToolStripMenuItem.Text = "Estructura Solicitudes Masivas..."
        '
        'ReportesToolStripMenuItem
        '
        Me.ReportesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProximosAVencerToolStripMenuItem})
        Me.ReportesToolStripMenuItem.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnPrinter
        Me.ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem"
        Me.ReportesToolStripMenuItem.Size = New System.Drawing.Size(81, 20)
        Me.ReportesToolStripMenuItem.Text = "Reportes"
        '
        'ProximosAVencerToolStripMenuItem
        '
        Me.ProximosAVencerToolStripMenuItem.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Arqueo
        Me.ProximosAVencerToolStripMenuItem.Name = "ProximosAVencerToolStripMenuItem"
        Me.ProximosAVencerToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ProximosAVencerToolStripMenuItem.Text = "Proximos a vencer"
        '
        'TabControlImageList
        '
        Me.TabControlImageList.ImageStream = CType(resources.GetObject("TabControlImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TabControlImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.TabControlImageList.Images.SetKeyName(0, "chart_organisation.png")
        Me.TabControlImageList.Images.SetKeyName(1, "vcard.png")
        Me.TabControlImageList.Images.SetKeyName(2, "report.png")
        Me.TabControlImageList.Images.SetKeyName(3, "layout_content.png")
        '
        'ProcesoTabPage
        '
        Me.ProcesoTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.ProcesoTabPage.Controls.Add(Me.WSPanel)
        Me.ProcesoTabPage.ImageIndex = 0
        Me.ProcesoTabPage.Location = New System.Drawing.Point(4, 26)
        Me.ProcesoTabPage.Name = "ProcesoTabPage"
        Me.ProcesoTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ProcesoTabPage.Size = New System.Drawing.Size(784, 469)
        Me.ProcesoTabPage.TabIndex = 0
        Me.ProcesoTabPage.Text = "Proceso"
        '
        'WSPanel
        '
        Me.WSPanel.BackColor = System.Drawing.SystemColors.Control
        Me.WSPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.WSPanel.Controls.Add(Me.TrasladoCarpetaButton)
        Me.WSPanel.Controls.Add(Me.TrasladoCajaButton)
        Me.WSPanel.Controls.Add(Me.CajaButton)
        Me.WSPanel.Controls.Add(Me.BovedasButton)
        Me.WSPanel.Controls.Add(Me.CargueSolicitudesMasivasButton)
        Me.WSPanel.Controls.Add(Me.Flecha2)
        Me.WSPanel.Controls.Add(Me.Button2)
        Me.WSPanel.Controls.Add(Me.Button1)
        Me.WSPanel.Controls.Add(Me.CreacionArqueoButton)
        Me.WSPanel.Controls.Add(Me.Flecha1)
        Me.WSPanel.Controls.Add(Me.CargueUniversalFlecha)
        Me.WSPanel.Controls.Add(Me.DespachoButton)
        Me.WSPanel.Controls.Add(Me.AdministracionSolicitudesButton)
        Me.WSPanel.Controls.Add(Me.AtencionSolicitudesButton)
        Me.WSPanel.Controls.Add(Me.RecepcionButton)
        Me.WSPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WSPanel.Location = New System.Drawing.Point(3, 3)
        Me.WSPanel.Name = "WSPanel"
        Me.WSPanel.Size = New System.Drawing.Size(778, 463)
        Me.WSPanel.TabIndex = 1
        '
        'TrasladoCarpetaButton
        '
        Me.TrasladoCarpetaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TrasladoCarpetaButton.BackColor = System.Drawing.Color.White
        Me.TrasladoCarpetaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TrasladoCarpetaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TrasladoCarpetaButton.Image = CType(resources.GetObject("TrasladoCarpetaButton.Image"), System.Drawing.Image)
        Me.TrasladoCarpetaButton.Location = New System.Drawing.Point(509, 362)
        Me.TrasladoCarpetaButton.Name = "TrasladoCarpetaButton"
        Me.TrasladoCarpetaButton.Size = New System.Drawing.Size(122, 70)
        Me.TrasladoCarpetaButton.TabIndex = 43
        Me.TrasladoCarpetaButton.Text = "Traslado Car&petas"
        Me.TrasladoCarpetaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.TrasladoCarpetaButton.UseVisualStyleBackColor = False
        '
        'TrasladoCajaButton
        '
        Me.TrasladoCajaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TrasladoCajaButton.BackColor = System.Drawing.Color.White
        Me.TrasladoCajaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TrasladoCajaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TrasladoCajaButton.Image = CType(resources.GetObject("TrasladoCajaButton.Image"), System.Drawing.Image)
        Me.TrasladoCajaButton.Location = New System.Drawing.Point(297, 360)
        Me.TrasladoCajaButton.Name = "TrasladoCajaButton"
        Me.TrasladoCajaButton.Size = New System.Drawing.Size(122, 70)
        Me.TrasladoCajaButton.TabIndex = 42
        Me.TrasladoCajaButton.Text = "Traslado Ca&ja"
        Me.TrasladoCajaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.TrasladoCajaButton.UseVisualStyleBackColor = False
        '
        'CajaButton
        '
        Me.CajaButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CajaButton.BackColor = System.Drawing.Color.White
        Me.CajaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CajaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CajaButton.Image = CType(resources.GetObject("CajaButton.Image"), System.Drawing.Image)
        Me.CajaButton.Location = New System.Drawing.Point(86, 360)
        Me.CajaButton.Name = "CajaButton"
        Me.CajaButton.Size = New System.Drawing.Size(122, 70)
        Me.CajaButton.TabIndex = 41
        Me.CajaButton.Text = "&Custodia Caja"
        Me.CajaButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CajaButton.UseVisualStyleBackColor = False
        '
        'BovedasButton
        '
        Me.BovedasButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BovedasButton.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BovedasButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BovedasButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BovedasButton.ForeColor = System.Drawing.Color.Black
        Me.BovedasButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Posicion
        Me.BovedasButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BovedasButton.Location = New System.Drawing.Point(509, 152)
        Me.BovedasButton.Name = "BovedasButton"
        Me.BovedasButton.Size = New System.Drawing.Size(122, 70)
        Me.BovedasButton.TabIndex = 40
        Me.BovedasButton.Text = "Visualización Bóveda"
        Me.BovedasButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BovedasButton.UseVisualStyleBackColor = False
        '
        'CargueSolicitudesMasivasButton
        '
        Me.CargueSolicitudesMasivasButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CargueSolicitudesMasivasButton.BackColor = System.Drawing.Color.White
        Me.CargueSolicitudesMasivasButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CargueSolicitudesMasivasButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CargueSolicitudesMasivasButton.ForeColor = System.Drawing.Color.Black
        Me.CargueSolicitudesMasivasButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Cargue
        Me.CargueSolicitudesMasivasButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CargueSolicitudesMasivasButton.Location = New System.Drawing.Point(86, 152)
        Me.CargueSolicitudesMasivasButton.Name = "CargueSolicitudesMasivasButton"
        Me.CargueSolicitudesMasivasButton.Size = New System.Drawing.Size(122, 70)
        Me.CargueSolicitudesMasivasButton.TabIndex = 39
        Me.CargueSolicitudesMasivasButton.Text = "Cargue Solicitudes Masivas"
        Me.CargueSolicitudesMasivasButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CargueSolicitudesMasivasButton.UseVisualStyleBackColor = False
        '
        'Flecha2
        '
        Me.Flecha2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Flecha2.ArrowWidth = 10
        Me.Flecha2.BackColor = System.Drawing.Color.Transparent
        Me.Flecha2.BorderColor = System.Drawing.Color.Black
        Me.Flecha2.BorderWidth = CType(1, Byte)
        Me.Flecha2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.Flecha2.FillColor = System.Drawing.Color.White
        Me.Flecha2.Location = New System.Drawing.Point(214, 50)
        Me.Flecha2.Name = "Flecha2"
        Me.Flecha2.Size = New System.Drawing.Size(72, 70)
        Me.Flecha2.TabIndex = 38
        Me.Flecha2.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'Button2
        '
        Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button2.BackColor = System.Drawing.Color.SteelBlue
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Mobile
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button2.Location = New System.Drawing.Point(86, 257)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(122, 70)
        Me.Button2.TabIndex = 37
        Me.Button2.Text = "Descuelgue"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button1.BackColor = System.Drawing.Color.SteelBlue
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Mobile
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button1.Location = New System.Drawing.Point(297, 50)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(122, 70)
        Me.Button1.TabIndex = 36
        Me.Button1.Text = "Entrada bóveda"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.UseVisualStyleBackColor = False
        '
        'CreacionArqueoButton
        '
        Me.CreacionArqueoButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CreacionArqueoButton.BackColor = System.Drawing.Color.White
        Me.CreacionArqueoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CreacionArqueoButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CreacionArqueoButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Arqueo
        Me.CreacionArqueoButton.Location = New System.Drawing.Point(509, 50)
        Me.CreacionArqueoButton.Name = "CreacionArqueoButton"
        Me.CreacionArqueoButton.Size = New System.Drawing.Size(122, 70)
        Me.CreacionArqueoButton.TabIndex = 35
        Me.CreacionArqueoButton.Text = "Ar&queo"
        Me.CreacionArqueoButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CreacionArqueoButton.UseVisualStyleBackColor = False
        '
        'Flecha1
        '
        Me.Flecha1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Flecha1.ArrowWidth = 10
        Me.Flecha1.BackColor = System.Drawing.Color.Transparent
        Me.Flecha1.BorderColor = System.Drawing.Color.Black
        Me.Flecha1.BorderWidth = CType(1, Byte)
        Me.Flecha1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.Flecha1.FillColor = System.Drawing.Color.LightSteelBlue
        Me.Flecha1.Location = New System.Drawing.Point(214, 257)
        Me.Flecha1.Name = "Flecha1"
        Me.Flecha1.Size = New System.Drawing.Size(72, 70)
        Me.Flecha1.TabIndex = 34
        Me.Flecha1.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'CargueUniversalFlecha
        '
        Me.CargueUniversalFlecha.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CargueUniversalFlecha.ArrowWidth = 10
        Me.CargueUniversalFlecha.BackColor = System.Drawing.Color.Transparent
        Me.CargueUniversalFlecha.BorderColor = System.Drawing.Color.Black
        Me.CargueUniversalFlecha.BorderWidth = CType(1, Byte)
        Me.CargueUniversalFlecha.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.CargueUniversalFlecha.FillColor = System.Drawing.Color.White
        Me.CargueUniversalFlecha.Location = New System.Drawing.Point(425, 257)
        Me.CargueUniversalFlecha.Name = "CargueUniversalFlecha"
        Me.CargueUniversalFlecha.Size = New System.Drawing.Size(77, 71)
        Me.CargueUniversalFlecha.TabIndex = 33
        Me.CargueUniversalFlecha.Tipo = Miharu.Desktop.Controls.Flecha.FlechaControl.EnumTipo.RIGHT
        '
        'DespachoButton
        '
        Me.DespachoButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.DespachoButton.BackColor = System.Drawing.Color.White
        Me.DespachoButton.Enabled = False
        Me.DespachoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DespachoButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DespachoButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Despacho
        Me.DespachoButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.DespachoButton.Location = New System.Drawing.Point(509, 257)
        Me.DespachoButton.Name = "DespachoButton"
        Me.DespachoButton.Size = New System.Drawing.Size(122, 71)
        Me.DespachoButton.TabIndex = 23
        Me.DespachoButton.Text = "Despacho"
        Me.DespachoButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.DespachoButton.UseVisualStyleBackColor = False
        '
        'AdministracionSolicitudesButton
        '
        Me.AdministracionSolicitudesButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.AdministracionSolicitudesButton.BackColor = System.Drawing.Color.White
        Me.AdministracionSolicitudesButton.Enabled = False
        Me.AdministracionSolicitudesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AdministracionSolicitudesButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdministracionSolicitudesButton.ForeColor = System.Drawing.Color.Black
        Me.AdministracionSolicitudesButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.AdministracionSolicitudes
        Me.AdministracionSolicitudesButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.AdministracionSolicitudesButton.Location = New System.Drawing.Point(297, 152)
        Me.AdministracionSolicitudesButton.Name = "AdministracionSolicitudesButton"
        Me.AdministracionSolicitudesButton.Size = New System.Drawing.Size(122, 70)
        Me.AdministracionSolicitudesButton.TabIndex = 22
        Me.AdministracionSolicitudesButton.Text = "A&dministración Solicitudes"
        Me.AdministracionSolicitudesButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.AdministracionSolicitudesButton.UseVisualStyleBackColor = False
        '
        'AtencionSolicitudesButton
        '
        Me.AtencionSolicitudesButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.AtencionSolicitudesButton.BackColor = System.Drawing.Color.LightSteelBlue
        Me.AtencionSolicitudesButton.Enabled = False
        Me.AtencionSolicitudesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AtencionSolicitudesButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AtencionSolicitudesButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.AtencionCliente
        Me.AtencionSolicitudesButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.AtencionSolicitudesButton.Location = New System.Drawing.Point(297, 257)
        Me.AtencionSolicitudesButton.Name = "AtencionSolicitudesButton"
        Me.AtencionSolicitudesButton.Size = New System.Drawing.Size(122, 70)
        Me.AtencionSolicitudesButton.TabIndex = 20
        Me.AtencionSolicitudesButton.Text = "&Atención solicitudes"
        Me.AtencionSolicitudesButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.AtencionSolicitudesButton.UseVisualStyleBackColor = False
        '
        'RecepcionButton
        '
        Me.RecepcionButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.RecepcionButton.BackColor = System.Drawing.Color.White
        Me.RecepcionButton.Enabled = False
        Me.RecepcionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RecepcionButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RecepcionButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.recepcioncajas
        Me.RecepcionButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.RecepcionButton.Location = New System.Drawing.Point(86, 50)
        Me.RecepcionButton.Name = "RecepcionButton"
        Me.RecepcionButton.Size = New System.Drawing.Size(122, 70)
        Me.RecepcionButton.TabIndex = 19
        Me.RecepcionButton.Text = "&Recepción"
        Me.RecepcionButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.RecepcionButton.UseVisualStyleBackColor = False
        '
        'MainTabControl
        '
        Me.MainTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.MainTabControl.Controls.Add(Me.ProcesoTabPage)
        Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTabControl.ImageList = Me.TabControlImageList
        Me.MainTabControl.Location = New System.Drawing.Point(0, 24)
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        Me.MainTabControl.Size = New System.Drawing.Size(792, 499)
        Me.MainTabControl.TabIndex = 36
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(32, 19)
        '
        'CodigosDeBarrasToolStripMenuItem1
        '
        Me.CodigosDeBarrasToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CarpetasYDocumentosToolStripMenuItem})
        Me.CodigosDeBarrasToolStripMenuItem1.Name = "CodigosDeBarrasToolStripMenuItem1"
        Me.CodigosDeBarrasToolStripMenuItem1.Size = New System.Drawing.Size(114, 20)
        Me.CodigosDeBarrasToolStripMenuItem1.Text = "Codigos de Barras"
        '
        'CarpetasYDocumentosToolStripMenuItem
        '
        Me.CarpetasYDocumentosToolStripMenuItem.Name = "CarpetasYDocumentosToolStripMenuItem"
        Me.CarpetasYDocumentosToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.CarpetasYDocumentosToolStripMenuItem.Text = "Carpetas y documentos..."
        '
        'FormCustodyWorkSpace
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 523)
        Me.Controls.Add(Me.MainTabControl)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormCustodyWorkSpace"
        Me.Text = "WorkSpace Custodia"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ProcesoTabPage.ResumeLayout(False)
        Me.WSPanel.ResumeLayout(False)
        Me.MainTabControl.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents TabControlImageList As System.Windows.Forms.ImageList
    Friend WithEvents ProcesoTabPage As System.Windows.Forms.TabPage
    Friend WithEvents WSPanel As System.Windows.Forms.Panel
    Friend WithEvents MainTabControl As System.Windows.Forms.TabControl
    Friend WithEvents RecepcionButton As System.Windows.Forms.Button
    Friend WithEvents AtencionSolicitudesButton As System.Windows.Forms.Button
    Friend WithEvents AdministracionSolicitudesButton As System.Windows.Forms.Button
    Friend WithEvents DespachoButton As System.Windows.Forms.Button
    Friend WithEvents Flecha1 As FlechaControl
    Friend WithEvents CargueUniversalFlecha As FlechaControl
    Friend WithEvents CreacionArqueoButton As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Flecha2 As FlechaControl
    Friend WithEvents CargueSolicitudesMasivasButton As System.Windows.Forms.Button
    Friend WithEvents HerramientasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EstructuraSolicitudesMasivasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SolicitudesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MotivosPorRolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrioridadPorRolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TipoPorRolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BovedasButton As System.Windows.Forms.Button
    Friend WithEvents ReportesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProximosAVencerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CajaButton As System.Windows.Forms.Button
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrasladoCajaButton As System.Windows.Forms.Button
    Friend WithEvents TrasladoCarpetaButton As System.Windows.Forms.Button
    Public WithEvents CodigosDeBarrasToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents CarpetasYDocumentosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
