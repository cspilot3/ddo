<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.txtIntervalo = New System.Windows.Forms.TextBox()
        Me.lblIntervalo = New System.Windows.Forms.Label()
        Me.IconoNotificacion = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cmsMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuiAbrir = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuiS1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuiSalir2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ssEstado = New System.Windows.Forms.StatusStrip()
        Me.tsslEstado = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.ToolTipText = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkEnabledSSL = New System.Windows.Forms.CheckBox()
        Me.txtMailUserPassword = New System.Windows.Forms.TextBox()
        Me.txtMailUser = New System.Windows.Forms.TextBox()
        Me.txtSMTPServerPort = New System.Windows.Forms.TextBox()
        Me.txtSMTPServerAddress = New System.Windows.Forms.TextBox()
        Me.txtFromMailDisplay = New System.Windows.Forms.TextBox()
        Me.txtFromMailAddress = New System.Windows.Forms.TextBox()
        Me.chkAutoIniciar = New System.Windows.Forms.CheckBox()
        Me.btnIniciar = New System.Windows.Forms.Button()
        Me.btnDetener = New System.Windows.Forms.Button()
        Me.btnReiniciar = New System.Windows.Forms.Button()
        Me.btnGuardarServicio = New System.Windows.Forms.Button()
        Me.btnEmailPrueba = New System.Windows.Forms.Button()
        Me.txtServicioWeb = New System.Windows.Forms.TextBox()
        Me.txtUsuario = New System.Windows.Forms.TextBox()
        Me.txtContraseña = New System.Windows.Forms.TextBox()
        Me.btnProbar = New System.Windows.Forms.Button()
        Me.ProbarRemotingButton = New System.Windows.Forms.Button()
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.mnuArchivo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSalir = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAyuda = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAcercaDe = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmrMonitorServicio = New System.Windows.Forms.Timer(Me.components)
        Me.imglIconos = New System.Windows.Forms.ImageList(Me.components)
        Me.tcBase = New System.Windows.Forms.TabControl()
        Me.tpServidor = New System.Windows.Forms.TabPage()
        Me.DataRemotingCheckBox = New System.Windows.Forms.CheckBox()
        Me.RemotingLabel = New System.Windows.Forms.Label()
        Me.lblMailUserPassword = New System.Windows.Forms.Label()
        Me.lblMailUser = New System.Windows.Forms.Label()
        Me.lblSMTPServerPort = New System.Windows.Forms.Label()
        Me.lblSMTPServerAddress = New System.Windows.Forms.Label()
        Me.lblFromMailDisplay = New System.Windows.Forms.Label()
        Me.lblFromMailAddress = New System.Windows.Forms.Label()
        Me.tpServicio = New System.Windows.Forms.TabPage()
        Me.lblContraseña = New System.Windows.Forms.Label()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.lblServicioWeb = New System.Windows.Forms.Label()
        Me.lblMilisegundos = New System.Windows.Forms.Label()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.RemotingPanel = New System.Windows.Forms.Panel()
        Me.CifradoCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PassWordRemotingTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AppNameRemotingTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PuertoRemotingTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ServidorRemotingTextBox = New System.Windows.Forms.TextBox()
        Me.imlTab = New System.Windows.Forms.ImageList(Me.components)
        Me.cmsMenu.SuspendLayout()
        Me.ssEstado.SuspendLayout()
        Me.MainMenu.SuspendLayout()
        Me.tcBase.SuspendLayout()
        Me.tpServidor.SuspendLayout()
        Me.tpServicio.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.RemotingPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtIntervalo
        '
        Me.txtIntervalo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIntervalo.Location = New System.Drawing.Point(166, 19)
        Me.txtIntervalo.Name = "txtIntervalo"
        Me.txtIntervalo.Size = New System.Drawing.Size(100, 22)
        Me.txtIntervalo.TabIndex = 1
        Me.ToolTipText.SetToolTip(Me.txtIntervalo, "Define el intervalo de tiempo en milisegundos a esperar antes de validar si hay n" &
        "uevos correos para enviar")
        '
        'lblIntervalo
        '
        Me.lblIntervalo.AutoSize = True
        Me.lblIntervalo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIntervalo.ForeColor = System.Drawing.Color.Maroon
        Me.lblIntervalo.Location = New System.Drawing.Point(16, 19)
        Me.lblIntervalo.Name = "lblIntervalo"
        Me.lblIntervalo.Size = New System.Drawing.Size(131, 16)
        Me.lblIntervalo.TabIndex = 0
        Me.lblIntervalo.Text = "Intervalo escaneo"
        '
        'IconoNotificacion
        '
        Me.IconoNotificacion.ContextMenuStrip = Me.cmsMenu
        Me.IconoNotificacion.Icon = CType(resources.GetObject("IconoNotificacion.Icon"), System.Drawing.Icon)
        Me.IconoNotificacion.Text = "SLYG MailSenderMonitor"
        Me.IconoNotificacion.Visible = True
        '
        'cmsMenu
        '
        Me.cmsMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuiAbrir, Me.mnuiS1, Me.mnuiSalir2})
        Me.cmsMenu.Name = "cmsMenu"
        Me.cmsMenu.Size = New System.Drawing.Size(173, 54)
        '
        'mnuiAbrir
        '
        Me.mnuiAbrir.Enabled = False
        Me.mnuiAbrir.Name = "mnuiAbrir"
        Me.mnuiAbrir.Size = New System.Drawing.Size(172, 22)
        Me.mnuiAbrir.Text = "Despelgar monitor"
        '
        'mnuiS1
        '
        Me.mnuiS1.Name = "mnuiS1"
        Me.mnuiS1.Size = New System.Drawing.Size(169, 6)
        '
        'mnuiSalir2
        '
        Me.mnuiSalir2.Name = "mnuiSalir2"
        Me.mnuiSalir2.Size = New System.Drawing.Size(172, 22)
        Me.mnuiSalir2.Text = "Salir"
        '
        'ssEstado
        '
        Me.ssEstado.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslEstado})
        Me.ssEstado.Location = New System.Drawing.Point(0, 376)
        Me.ssEstado.Name = "ssEstado"
        Me.ssEstado.Size = New System.Drawing.Size(472, 22)
        Me.ssEstado.TabIndex = 2
        Me.ssEstado.Text = "StatusStrip1"
        '
        'tsslEstado
        '
        Me.tsslEstado.ForeColor = System.Drawing.Color.Brown
        Me.tsslEstado.Name = "tsslEstado"
        Me.tsslEstado.Size = New System.Drawing.Size(42, 17)
        Me.tsslEstado.Text = "Estado"
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblTitulo.Location = New System.Drawing.Point(276, 35)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(189, 16)
        Me.lblTitulo.TabIndex = 11
        Me.lblTitulo.Text = "Configuración del Servicio"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkEnabledSSL
        '
        Me.chkEnabledSSL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkEnabledSSL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnabledSSL.ForeColor = System.Drawing.Color.Maroon
        Me.chkEnabledSSL.Location = New System.Drawing.Point(15, 187)
        Me.chkEnabledSSL.Name = "chkEnabledSSL"
        Me.chkEnabledSSL.Size = New System.Drawing.Size(168, 18)
        Me.chkEnabledSSL.TabIndex = 18
        Me.chkEnabledSSL.Text = "Usar SSL"
        Me.ToolTipText.SetToolTip(Me.chkEnabledSSL, "Define si se utiliza SSL para el envío de los correos")
        Me.chkEnabledSSL.UseVisualStyleBackColor = True
        '
        'txtMailUserPassword
        '
        Me.txtMailUserPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMailUserPassword.Location = New System.Drawing.Point(159, 159)
        Me.txtMailUserPassword.Name = "txtMailUserPassword"
        Me.txtMailUserPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtMailUserPassword.Size = New System.Drawing.Size(267, 22)
        Me.txtMailUserPassword.TabIndex = 17
        Me.ToolTipText.SetToolTip(Me.txtMailUserPassword, "Define la contaseña del usuario del servidor de correo")
        '
        'txtMailUser
        '
        Me.txtMailUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMailUser.Location = New System.Drawing.Point(159, 131)
        Me.txtMailUser.Name = "txtMailUser"
        Me.txtMailUser.Size = New System.Drawing.Size(267, 22)
        Me.txtMailUser.TabIndex = 15
        Me.ToolTipText.SetToolTip(Me.txtMailUser, "Define el usuario del servidor de correo")
        '
        'txtSMTPServerPort
        '
        Me.txtSMTPServerPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPServerPort.Location = New System.Drawing.Point(159, 103)
        Me.txtSMTPServerPort.Name = "txtSMTPServerPort"
        Me.txtSMTPServerPort.Size = New System.Drawing.Size(85, 22)
        Me.txtSMTPServerPort.TabIndex = 13
        Me.ToolTipText.SetToolTip(Me.txtSMTPServerPort, "Define el puero de conexión al servidor de correos")
        '
        'txtSMTPServerAddress
        '
        Me.txtSMTPServerAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPServerAddress.Location = New System.Drawing.Point(159, 75)
        Me.txtSMTPServerAddress.Name = "txtSMTPServerAddress"
        Me.txtSMTPServerAddress.Size = New System.Drawing.Size(267, 22)
        Me.txtSMTPServerAddress.TabIndex = 11
        Me.ToolTipText.SetToolTip(Me.txtSMTPServerAddress, "Define la dirección del servidor de correo")
        '
        'txtFromMailDisplay
        '
        Me.txtFromMailDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromMailDisplay.Location = New System.Drawing.Point(159, 47)
        Me.txtFromMailDisplay.Name = "txtFromMailDisplay"
        Me.txtFromMailDisplay.Size = New System.Drawing.Size(267, 22)
        Me.txtFromMailDisplay.TabIndex = 9
        Me.ToolTipText.SetToolTip(Me.txtFromMailDisplay, "Define el nombre a mostrar como remitente de los correo enviados")
        '
        'txtFromMailAddress
        '
        Me.txtFromMailAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromMailAddress.Location = New System.Drawing.Point(159, 19)
        Me.txtFromMailAddress.Name = "txtFromMailAddress"
        Me.txtFromMailAddress.Size = New System.Drawing.Size(267, 22)
        Me.txtFromMailAddress.TabIndex = 7
        Me.ToolTipText.SetToolTip(Me.txtFromMailAddress, "Define la dirección de correo desde donde se envian los correos")
        '
        'chkAutoIniciar
        '
        Me.chkAutoIniciar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoIniciar.ForeColor = System.Drawing.Color.Maroon
        Me.chkAutoIniciar.Location = New System.Drawing.Point(35, 299)
        Me.chkAutoIniciar.Name = "chkAutoIniciar"
        Me.chkAutoIniciar.Size = New System.Drawing.Size(290, 31)
        Me.chkAutoIniciar.TabIndex = 20
        Me.chkAutoIniciar.Text = "Ejecutar al iniciar sesión"
        Me.ToolTipText.SetToolTip(Me.chkAutoIniciar, "Define si la aplicación de monitoreo se inicia automáticamente al iniciar sesión")
        Me.chkAutoIniciar.UseVisualStyleBackColor = True
        '
        'btnIniciar
        '
        Me.btnIniciar.Image = Global.Miharu.MailSender.Monitor.My.Resources.Resources.Iniciar
        Me.btnIniciar.Location = New System.Drawing.Point(35, 336)
        Me.btnIniciar.Name = "btnIniciar"
        Me.btnIniciar.Size = New System.Drawing.Size(32, 32)
        Me.btnIniciar.TabIndex = 0
        Me.ToolTipText.SetToolTip(Me.btnIniciar, "Iniciar el servicio")
        Me.btnIniciar.UseVisualStyleBackColor = True
        '
        'btnDetener
        '
        Me.btnDetener.Image = Global.Miharu.MailSender.Monitor.My.Resources.Resources.Detener
        Me.btnDetener.Location = New System.Drawing.Point(73, 336)
        Me.btnDetener.Name = "btnDetener"
        Me.btnDetener.Size = New System.Drawing.Size(32, 32)
        Me.btnDetener.TabIndex = 1
        Me.ToolTipText.SetToolTip(Me.btnDetener, "Detener el servicio")
        Me.btnDetener.UseVisualStyleBackColor = True
        '
        'btnReiniciar
        '
        Me.btnReiniciar.Image = Global.Miharu.MailSender.Monitor.My.Resources.Resources.Reiniciar
        Me.btnReiniciar.Location = New System.Drawing.Point(134, 336)
        Me.btnReiniciar.Name = "btnReiniciar"
        Me.btnReiniciar.Size = New System.Drawing.Size(32, 32)
        Me.btnReiniciar.TabIndex = 2
        Me.ToolTipText.SetToolTip(Me.btnReiniciar, "Reiniciar el servicio")
        Me.btnReiniciar.UseVisualStyleBackColor = True
        '
        'btnGuardarServicio
        '
        Me.btnGuardarServicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardarServicio.Image = Global.Miharu.MailSender.Monitor.My.Resources.Resources.save
        Me.btnGuardarServicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGuardarServicio.Location = New System.Drawing.Point(301, 336)
        Me.btnGuardarServicio.Name = "btnGuardarServicio"
        Me.btnGuardarServicio.Size = New System.Drawing.Size(147, 28)
        Me.btnGuardarServicio.TabIndex = 7
        Me.btnGuardarServicio.Text = "Almacenar cambios"
        Me.btnGuardarServicio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTipText.SetToolTip(Me.btnGuardarServicio, "Almacenar la parametrización del servicio")
        Me.btnGuardarServicio.UseVisualStyleBackColor = True
        '
        'btnEmailPrueba
        '
        Me.btnEmailPrueba.Image = Global.Miharu.MailSender.Monitor.My.Resources.Resources.email_go
        Me.btnEmailPrueba.Location = New System.Drawing.Point(394, 187)
        Me.btnEmailPrueba.Name = "btnEmailPrueba"
        Me.btnEmailPrueba.Size = New System.Drawing.Size(32, 32)
        Me.btnEmailPrueba.TabIndex = 19
        Me.ToolTipText.SetToolTip(Me.btnEmailPrueba, "Enviar un e-mail de prueba")
        Me.btnEmailPrueba.UseVisualStyleBackColor = True
        '
        'txtServicioWeb
        '
        Me.txtServicioWeb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServicioWeb.Location = New System.Drawing.Point(118, 60)
        Me.txtServicioWeb.Name = "txtServicioWeb"
        Me.txtServicioWeb.Size = New System.Drawing.Size(314, 22)
        Me.txtServicioWeb.TabIndex = 4
        Me.ToolTipText.SetToolTip(Me.txtServicioWeb, "Servicio web de validación de usuarios")
        '
        'txtUsuario
        '
        Me.txtUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsuario.Location = New System.Drawing.Point(118, 88)
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.ReadOnly = True
        Me.txtUsuario.Size = New System.Drawing.Size(191, 22)
        Me.txtUsuario.TabIndex = 6
        Me.ToolTipText.SetToolTip(Me.txtUsuario, "Usuario de inicio de sesión")
        '
        'txtContraseña
        '
        Me.txtContraseña.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContraseña.Location = New System.Drawing.Point(118, 116)
        Me.txtContraseña.Name = "txtContraseña"
        Me.txtContraseña.Size = New System.Drawing.Size(191, 22)
        Me.txtContraseña.TabIndex = 8
        Me.ToolTipText.SetToolTip(Me.txtContraseña, "Contraseña del usuario de inicio de sesión")
        '
        'btnProbar
        '
        Me.btnProbar.Location = New System.Drawing.Point(357, 116)
        Me.btnProbar.Name = "btnProbar"
        Me.btnProbar.Size = New System.Drawing.Size(75, 23)
        Me.btnProbar.TabIndex = 9
        Me.btnProbar.Text = "Probar"
        Me.ToolTipText.SetToolTip(Me.btnProbar, "Probar la configuración del servicio web")
        Me.btnProbar.UseVisualStyleBackColor = True
        '
        'ProbarRemotingButton
        '
        Me.ProbarRemotingButton.Location = New System.Drawing.Point(303, 162)
        Me.ProbarRemotingButton.Name = "ProbarRemotingButton"
        Me.ProbarRemotingButton.Size = New System.Drawing.Size(75, 28)
        Me.ProbarRemotingButton.TabIndex = 34
        Me.ProbarRemotingButton.Text = "Probar"
        Me.ToolTipText.SetToolTip(Me.ProbarRemotingButton, "Probar la configuración del servicio web")
        Me.ProbarRemotingButton.UseVisualStyleBackColor = True
        '
        'MainMenu
        '
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuArchivo, Me.mnuAyuda})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.Size = New System.Drawing.Size(472, 24)
        Me.MainMenu.TabIndex = 0
        Me.MainMenu.Text = "MenuStrip1"
        '
        'mnuArchivo
        '
        Me.mnuArchivo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSalir})
        Me.mnuArchivo.Name = "mnuArchivo"
        Me.mnuArchivo.Size = New System.Drawing.Size(60, 20)
        Me.mnuArchivo.Text = "&Archivo"
        '
        'mnuSalir
        '
        Me.mnuSalir.Name = "mnuSalir"
        Me.mnuSalir.Size = New System.Drawing.Size(180, 22)
        Me.mnuSalir.Text = "Salir"
        '
        'mnuAyuda
        '
        Me.mnuAyuda.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAcercaDe})
        Me.mnuAyuda.Name = "mnuAyuda"
        Me.mnuAyuda.Size = New System.Drawing.Size(53, 20)
        Me.mnuAyuda.Text = "A&yuda"
        '
        'mnuAcercaDe
        '
        Me.mnuAcercaDe.Name = "mnuAcercaDe"
        Me.mnuAcercaDe.Size = New System.Drawing.Size(265, 22)
        Me.mnuAcercaDe.Text = "Acerca de MailSenderDDOMonitor..."
        '
        'tmrMonitorServicio
        '
        Me.tmrMonitorServicio.Interval = 1000
        '
        'imglIconos
        '
        Me.imglIconos.ImageStream = CType(resources.GetObject("imglIconos.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imglIconos.TransparentColor = System.Drawing.Color.Transparent
        Me.imglIconos.Images.SetKeyName(0, "MailSenderIconInactivo.ico")
        Me.imglIconos.Images.SetKeyName(1, "MailSenderIconActivo.ico")
        '
        'tcBase
        '
        Me.tcBase.Controls.Add(Me.tpServidor)
        Me.tcBase.Controls.Add(Me.tpServicio)
        Me.tcBase.Controls.Add(Me.TabPage1)
        Me.tcBase.ImageList = Me.imlTab
        Me.tcBase.Location = New System.Drawing.Point(12, 35)
        Me.tcBase.Name = "tcBase"
        Me.tcBase.SelectedIndex = 0
        Me.tcBase.Size = New System.Drawing.Size(453, 258)
        Me.tcBase.TabIndex = 1
        '
        'tpServidor
        '
        Me.tpServidor.Controls.Add(Me.DataRemotingCheckBox)
        Me.tpServidor.Controls.Add(Me.RemotingLabel)
        Me.tpServidor.Controls.Add(Me.btnEmailPrueba)
        Me.tpServidor.Controls.Add(Me.chkEnabledSSL)
        Me.tpServidor.Controls.Add(Me.txtMailUserPassword)
        Me.tpServidor.Controls.Add(Me.lblMailUserPassword)
        Me.tpServidor.Controls.Add(Me.txtMailUser)
        Me.tpServidor.Controls.Add(Me.lblMailUser)
        Me.tpServidor.Controls.Add(Me.txtSMTPServerPort)
        Me.tpServidor.Controls.Add(Me.lblSMTPServerPort)
        Me.tpServidor.Controls.Add(Me.txtSMTPServerAddress)
        Me.tpServidor.Controls.Add(Me.lblSMTPServerAddress)
        Me.tpServidor.Controls.Add(Me.txtFromMailDisplay)
        Me.tpServidor.Controls.Add(Me.lblFromMailDisplay)
        Me.tpServidor.Controls.Add(Me.txtFromMailAddress)
        Me.tpServidor.Controls.Add(Me.lblFromMailAddress)
        Me.tpServidor.ImageIndex = 1
        Me.tpServidor.Location = New System.Drawing.Point(4, 23)
        Me.tpServidor.Name = "tpServidor"
        Me.tpServidor.Size = New System.Drawing.Size(445, 231)
        Me.tpServidor.TabIndex = 1
        Me.tpServidor.Text = "SMTP"
        Me.tpServidor.UseVisualStyleBackColor = True
        '
        'DataRemotingCheckBox
        '
        Me.DataRemotingCheckBox.AutoSize = True
        Me.DataRemotingCheckBox.Location = New System.Drawing.Point(168, 211)
        Me.DataRemotingCheckBox.Name = "DataRemotingCheckBox"
        Me.DataRemotingCheckBox.Size = New System.Drawing.Size(15, 14)
        Me.DataRemotingCheckBox.TabIndex = 36
        Me.DataRemotingCheckBox.UseVisualStyleBackColor = True
        '
        'RemotingLabel
        '
        Me.RemotingLabel.AutoSize = True
        Me.RemotingLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RemotingLabel.ForeColor = System.Drawing.Color.Maroon
        Me.RemotingLabel.Location = New System.Drawing.Point(16, 208)
        Me.RemotingLabel.Name = "RemotingLabel"
        Me.RemotingLabel.Size = New System.Drawing.Size(138, 16)
        Me.RemotingLabel.TabIndex = 35
        Me.RemotingLabel.Text = "Usa DataRemoting"
        '
        'lblMailUserPassword
        '
        Me.lblMailUserPassword.AutoSize = True
        Me.lblMailUserPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMailUserPassword.ForeColor = System.Drawing.Color.Maroon
        Me.lblMailUserPassword.Location = New System.Drawing.Point(16, 159)
        Me.lblMailUserPassword.Name = "lblMailUserPassword"
        Me.lblMailUserPassword.Size = New System.Drawing.Size(86, 16)
        Me.lblMailUserPassword.TabIndex = 16
        Me.lblMailUserPassword.Text = "Contraseña"
        '
        'lblMailUser
        '
        Me.lblMailUser.AutoSize = True
        Me.lblMailUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMailUser.ForeColor = System.Drawing.Color.Maroon
        Me.lblMailUser.Location = New System.Drawing.Point(16, 131)
        Me.lblMailUser.Name = "lblMailUser"
        Me.lblMailUser.Size = New System.Drawing.Size(61, 16)
        Me.lblMailUser.TabIndex = 14
        Me.lblMailUser.Text = "Usuario"
        '
        'lblSMTPServerPort
        '
        Me.lblSMTPServerPort.AutoSize = True
        Me.lblSMTPServerPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSMTPServerPort.ForeColor = System.Drawing.Color.Maroon
        Me.lblSMTPServerPort.Location = New System.Drawing.Point(16, 103)
        Me.lblSMTPServerPort.Name = "lblSMTPServerPort"
        Me.lblSMTPServerPort.Size = New System.Drawing.Size(52, 16)
        Me.lblSMTPServerPort.TabIndex = 12
        Me.lblSMTPServerPort.Text = "Puerto"
        '
        'lblSMTPServerAddress
        '
        Me.lblSMTPServerAddress.AutoSize = True
        Me.lblSMTPServerAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSMTPServerAddress.ForeColor = System.Drawing.Color.Maroon
        Me.lblSMTPServerAddress.Location = New System.Drawing.Point(16, 75)
        Me.lblSMTPServerAddress.Name = "lblSMTPServerAddress"
        Me.lblSMTPServerAddress.Size = New System.Drawing.Size(66, 16)
        Me.lblSMTPServerAddress.TabIndex = 10
        Me.lblSMTPServerAddress.Text = "Servidor"
        '
        'lblFromMailDisplay
        '
        Me.lblFromMailDisplay.AutoSize = True
        Me.lblFromMailDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromMailDisplay.ForeColor = System.Drawing.Color.Maroon
        Me.lblFromMailDisplay.Location = New System.Drawing.Point(16, 47)
        Me.lblFromMailDisplay.Name = "lblFromMailDisplay"
        Me.lblFromMailDisplay.Size = New System.Drawing.Size(130, 16)
        Me.lblFromMailDisplay.TabIndex = 8
        Me.lblFromMailDisplay.Text = "Nombre remitente"
        '
        'lblFromMailAddress
        '
        Me.lblFromMailAddress.AutoSize = True
        Me.lblFromMailAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromMailAddress.ForeColor = System.Drawing.Color.Maroon
        Me.lblFromMailAddress.Location = New System.Drawing.Point(16, 19)
        Me.lblFromMailAddress.Name = "lblFromMailAddress"
        Me.lblFromMailAddress.Size = New System.Drawing.Size(122, 16)
        Me.lblFromMailAddress.TabIndex = 6
        Me.lblFromMailAddress.Text = "Correo remitente"
        '
        'tpServicio
        '
        Me.tpServicio.Controls.Add(Me.btnProbar)
        Me.tpServicio.Controls.Add(Me.txtContraseña)
        Me.tpServicio.Controls.Add(Me.lblContraseña)
        Me.tpServicio.Controls.Add(Me.txtUsuario)
        Me.tpServicio.Controls.Add(Me.lblUsuario)
        Me.tpServicio.Controls.Add(Me.txtServicioWeb)
        Me.tpServicio.Controls.Add(Me.lblServicioWeb)
        Me.tpServicio.Controls.Add(Me.lblMilisegundos)
        Me.tpServicio.Controls.Add(Me.txtIntervalo)
        Me.tpServicio.Controls.Add(Me.lblIntervalo)
        Me.tpServicio.ImageIndex = 0
        Me.tpServicio.Location = New System.Drawing.Point(4, 23)
        Me.tpServicio.Name = "tpServicio"
        Me.tpServicio.Padding = New System.Windows.Forms.Padding(3)
        Me.tpServicio.Size = New System.Drawing.Size(445, 231)
        Me.tpServicio.TabIndex = 0
        Me.tpServicio.Text = "Servicio"
        Me.tpServicio.UseVisualStyleBackColor = True
        '
        'lblContraseña
        '
        Me.lblContraseña.AutoSize = True
        Me.lblContraseña.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContraseña.ForeColor = System.Drawing.Color.Maroon
        Me.lblContraseña.Location = New System.Drawing.Point(16, 116)
        Me.lblContraseña.Name = "lblContraseña"
        Me.lblContraseña.Size = New System.Drawing.Size(86, 16)
        Me.lblContraseña.TabIndex = 7
        Me.lblContraseña.Text = "Contraseña"
        '
        'lblUsuario
        '
        Me.lblUsuario.AutoSize = True
        Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.ForeColor = System.Drawing.Color.Maroon
        Me.lblUsuario.Location = New System.Drawing.Point(16, 88)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(61, 16)
        Me.lblUsuario.TabIndex = 5
        Me.lblUsuario.Text = "Usuario"
        '
        'lblServicioWeb
        '
        Me.lblServicioWeb.AutoSize = True
        Me.lblServicioWeb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServicioWeb.ForeColor = System.Drawing.Color.Maroon
        Me.lblServicioWeb.Location = New System.Drawing.Point(16, 60)
        Me.lblServicioWeb.Name = "lblServicioWeb"
        Me.lblServicioWeb.Size = New System.Drawing.Size(96, 16)
        Me.lblServicioWeb.TabIndex = 3
        Me.lblServicioWeb.Text = "Servicio web"
        '
        'lblMilisegundos
        '
        Me.lblMilisegundos.AutoSize = True
        Me.lblMilisegundos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMilisegundos.ForeColor = System.Drawing.Color.Maroon
        Me.lblMilisegundos.Location = New System.Drawing.Point(272, 25)
        Me.lblMilisegundos.Name = "lblMilisegundos"
        Me.lblMilisegundos.Size = New System.Drawing.Size(27, 16)
        Me.lblMilisegundos.TabIndex = 2
        Me.lblMilisegundos.Text = "ms"
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.RemotingPanel)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(445, 231)
        Me.TabPage1.TabIndex = 2
        Me.TabPage1.Text = "Remoting"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'RemotingPanel
        '
        Me.RemotingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RemotingPanel.Controls.Add(Me.ProbarRemotingButton)
        Me.RemotingPanel.Controls.Add(Me.CifradoCheckBox)
        Me.RemotingPanel.Controls.Add(Me.Label5)
        Me.RemotingPanel.Controls.Add(Me.Label4)
        Me.RemotingPanel.Controls.Add(Me.PassWordRemotingTextBox)
        Me.RemotingPanel.Controls.Add(Me.Label3)
        Me.RemotingPanel.Controls.Add(Me.AppNameRemotingTextBox)
        Me.RemotingPanel.Controls.Add(Me.Label2)
        Me.RemotingPanel.Controls.Add(Me.PuertoRemotingTextBox)
        Me.RemotingPanel.Controls.Add(Me.Label1)
        Me.RemotingPanel.Controls.Add(Me.ServidorRemotingTextBox)
        Me.RemotingPanel.Enabled = False
        Me.RemotingPanel.Location = New System.Drawing.Point(7, 8)
        Me.RemotingPanel.Name = "RemotingPanel"
        Me.RemotingPanel.Size = New System.Drawing.Size(430, 214)
        Me.RemotingPanel.TabIndex = 1
        '
        'CifradoCheckBox
        '
        Me.CifradoCheckBox.AutoSize = True
        Me.CifradoCheckBox.Location = New System.Drawing.Point(118, 162)
        Me.CifradoCheckBox.Name = "CifradoCheckBox"
        Me.CifradoCheckBox.Size = New System.Drawing.Size(15, 14)
        Me.CifradoCheckBox.TabIndex = 33
        Me.CifradoCheckBox.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Maroon
        Me.Label5.Location = New System.Drawing.Point(16, 160)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 16)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Cifrado"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Maroon
        Me.Label4.Location = New System.Drawing.Point(16, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 16)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Contraseña"
        '
        'PassWordRemotingTextBox
        '
        Me.PassWordRemotingTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassWordRemotingTextBox.Location = New System.Drawing.Point(118, 124)
        Me.PassWordRemotingTextBox.Name = "PassWordRemotingTextBox"
        Me.PassWordRemotingTextBox.Size = New System.Drawing.Size(261, 22)
        Me.PassWordRemotingTextBox.TabIndex = 31
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Maroon
        Me.Label3.Location = New System.Drawing.Point(16, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 16)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "AppName"
        '
        'AppNameRemotingTextBox
        '
        Me.AppNameRemotingTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppNameRemotingTextBox.Location = New System.Drawing.Point(117, 84)
        Me.AppNameRemotingTextBox.Name = "AppNameRemotingTextBox"
        Me.AppNameRemotingTextBox.Size = New System.Drawing.Size(261, 22)
        Me.AppNameRemotingTextBox.TabIndex = 29
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(16, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 16)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Puerto"
        '
        'PuertoRemotingTextBox
        '
        Me.PuertoRemotingTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PuertoRemotingTextBox.Location = New System.Drawing.Point(118, 50)
        Me.PuertoRemotingTextBox.Name = "PuertoRemotingTextBox"
        Me.PuertoRemotingTextBox.Size = New System.Drawing.Size(111, 22)
        Me.PuertoRemotingTextBox.TabIndex = 27
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(16, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 16)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "IPName"
        '
        'ServidorRemotingTextBox
        '
        Me.ServidorRemotingTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ServidorRemotingTextBox.Location = New System.Drawing.Point(118, 13)
        Me.ServidorRemotingTextBox.Name = "ServidorRemotingTextBox"
        Me.ServidorRemotingTextBox.Size = New System.Drawing.Size(261, 22)
        Me.ServidorRemotingTextBox.TabIndex = 25
        '
        'imlTab
        '
        Me.imlTab.ImageStream = CType(resources.GetObject("imlTab.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlTab.TransparentColor = System.Drawing.Color.Transparent
        Me.imlTab.Images.SetKeyName(0, "Config.png")
        Me.imlTab.Images.SetKeyName(1, "email_edit.png")
        Me.imlTab.Images.SetKeyName(2, "database_edit.png")
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(472, 398)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.chkAutoIniciar)
        Me.Controls.Add(Me.tcBase)
        Me.Controls.Add(Me.ssEstado)
        Me.Controls.Add(Me.btnIniciar)
        Me.Controls.Add(Me.btnDetener)
        Me.Controls.Add(Me.MainMenu)
        Me.Controls.Add(Me.btnReiniciar)
        Me.Controls.Add(Me.btnGuardarServicio)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MainMenu
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MIharu MailSenderDDOMonitor"
        Me.cmsMenu.ResumeLayout(False)
        Me.ssEstado.ResumeLayout(False)
        Me.ssEstado.PerformLayout()
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.tcBase.ResumeLayout(False)
        Me.tpServidor.ResumeLayout(False)
        Me.tpServidor.PerformLayout()
        Me.tpServicio.ResumeLayout(False)
        Me.tpServicio.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.RemotingPanel.ResumeLayout(False)
        Me.RemotingPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnIniciar As System.Windows.Forms.Button
    Friend WithEvents btnReiniciar As System.Windows.Forms.Button
    Friend WithEvents btnDetener As System.Windows.Forms.Button
    Friend WithEvents txtIntervalo As System.Windows.Forms.TextBox
    Friend WithEvents lblIntervalo As System.Windows.Forms.Label
    Friend WithEvents IconoNotificacion As System.Windows.Forms.NotifyIcon
    Friend WithEvents ssEstado As System.Windows.Forms.StatusStrip
    Friend WithEvents tsslEstado As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents ToolTipText As System.Windows.Forms.ToolTip
    Friend WithEvents MainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuArchivo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSalir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAyuda As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAcercaDe As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnGuardarServicio As System.Windows.Forms.Button
    Friend WithEvents tmrMonitorServicio As System.Windows.Forms.Timer
    Friend WithEvents imglIconos As System.Windows.Forms.ImageList
    Friend WithEvents cmsMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuiAbrir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuiS1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuiSalir2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tcBase As System.Windows.Forms.TabControl
    Friend WithEvents tpServicio As System.Windows.Forms.TabPage
    Friend WithEvents imlTab As System.Windows.Forms.ImageList
    Friend WithEvents tpServidor As System.Windows.Forms.TabPage
    Friend WithEvents txtFromMailAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblFromMailAddress As System.Windows.Forms.Label
    Friend WithEvents txtSMTPServerPort As System.Windows.Forms.TextBox
    Friend WithEvents lblSMTPServerPort As System.Windows.Forms.Label
    Friend WithEvents txtSMTPServerAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblSMTPServerAddress As System.Windows.Forms.Label
    Friend WithEvents txtFromMailDisplay As System.Windows.Forms.TextBox
    Friend WithEvents lblFromMailDisplay As System.Windows.Forms.Label
    Friend WithEvents txtMailUserPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblMailUserPassword As System.Windows.Forms.Label
    Friend WithEvents txtMailUser As System.Windows.Forms.TextBox
    Friend WithEvents lblMailUser As System.Windows.Forms.Label
    Friend WithEvents chkEnabledSSL As System.Windows.Forms.CheckBox
    Friend WithEvents lblMilisegundos As System.Windows.Forms.Label
    Friend WithEvents chkAutoIniciar As System.Windows.Forms.CheckBox
    Friend WithEvents btnEmailPrueba As System.Windows.Forms.Button
    Friend WithEvents txtServicioWeb As System.Windows.Forms.TextBox
    Friend WithEvents lblServicioWeb As System.Windows.Forms.Label
    Friend WithEvents txtContraseña As System.Windows.Forms.TextBox
    Friend WithEvents lblContraseña As System.Windows.Forms.Label
    Friend WithEvents txtUsuario As System.Windows.Forms.TextBox
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
    Friend WithEvents btnProbar As System.Windows.Forms.Button
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents RemotingPanel As System.Windows.Forms.Panel
    Friend WithEvents ProbarRemotingButton As System.Windows.Forms.Button
    Friend WithEvents CifradoCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PassWordRemotingTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AppNameRemotingTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PuertoRemotingTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ServidorRemotingTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DataRemotingCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents RemotingLabel As System.Windows.Forms.Label
End Class
