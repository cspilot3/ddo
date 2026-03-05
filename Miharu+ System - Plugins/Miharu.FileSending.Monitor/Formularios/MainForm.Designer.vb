Namespace Formularios
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
            Me.WorkingFolderLabel = New System.Windows.Forms.Label()
            Me.PuertoTextBox = New System.Windows.Forms.TextBox()
            Me.PuertoLabel = New System.Windows.Forms.Label()
            Me.AppNameLabel = New System.Windows.Forms.Label()
            Me.AppNameTextBox = New System.Windows.Forms.TextBox()
            Me.NotificacionNotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
            Me.MenuContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.AbrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
            Me.SalirContextualToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.EstadoStatusStrip = New System.Windows.Forms.StatusStrip()
            Me.EstadoStatusStripLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.TituloLabel = New System.Windows.Forms.Label()
            Me.ToolTipText = New System.Windows.Forms.ToolTip(Me.components)
            Me.WorkingFolderTextBox = New System.Windows.Forms.TextBox()
            Me.ProbarButton = New System.Windows.Forms.Button()
            Me.PasswordTextBox = New System.Windows.Forms.TextBox()
            Me.UserTextBox = New System.Windows.Forms.TextBox()
            Me.ServicioWebTextBox = New System.Windows.Forms.TextBox()
            Me.IdentifierDateFormatTextBox = New System.Windows.Forms.TextBox()
            Me.ProbarRemotingButton = New System.Windows.Forms.Button()
            Me.GuardarServicioButton = New System.Windows.Forms.Button()
            Me.IniciarButton = New System.Windows.Forms.Button()
            Me.DetenerButton = New System.Windows.Forms.Button()
            Me.ReiniciarButton = New System.Windows.Forms.Button()
            Me.BaseMenuStrip = New System.Windows.Forms.MenuStrip()
            Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.AyudaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.AcercaDeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.MonitorServicioTimer = New System.Windows.Forms.Timer(Me.components)
            Me.IconosImageList = New System.Windows.Forms.ImageList(Me.components)
            Me.BaseTabControl = New System.Windows.Forms.TabControl()
            Me.ServicioTabPage = New System.Windows.Forms.TabPage()
            Me.DataRemotingCheckBox = New System.Windows.Forms.CheckBox()
            Me.RemotingLabel = New System.Windows.Forms.Label()
            Me.IdentifierDateFormatLabel = New System.Windows.Forms.Label()
            Me.PasswordLabel = New System.Windows.Forms.Label()
            Me.UserLabel = New System.Windows.Forms.Label()
            Me.ServicioWebLabel = New System.Windows.Forms.Label()
            Me.CargarPathButton = New System.Windows.Forms.Button()
            Me.RemotingTabPage = New System.Windows.Forms.TabPage()
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
            Me.TabImageList = New System.Windows.Forms.ImageList(Me.components)
            Me.ServicioGroupBox = New System.Windows.Forms.GroupBox()
            Me.LogFolderTextBox = New System.Windows.Forms.TextBox()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.LogActivoCheckBox = New System.Windows.Forms.CheckBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.MenuContextMenuStrip.SuspendLayout()
            Me.EstadoStatusStrip.SuspendLayout()
            Me.BaseMenuStrip.SuspendLayout()
            Me.BaseTabControl.SuspendLayout()
            Me.ServicioTabPage.SuspendLayout()
            Me.RemotingTabPage.SuspendLayout()
            Me.RemotingPanel.SuspendLayout()
            Me.ServicioGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'WorkingFolderLabel
            '
            Me.WorkingFolderLabel.AutoSize = True
            Me.WorkingFolderLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.WorkingFolderLabel.ForeColor = System.Drawing.Color.Maroon
            Me.WorkingFolderLabel.Location = New System.Drawing.Point(8, 152)
            Me.WorkingFolderLabel.Name = "WorkingFolderLabel"
            Me.WorkingFolderLabel.Size = New System.Drawing.Size(150, 16)
            Me.WorkingFolderLabel.TabIndex = 2
            Me.WorkingFolderLabel.Text = "Directorio de trabajo"
            '
            'PuertoTextBox
            '
            Me.PuertoTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PuertoTextBox.Location = New System.Drawing.Point(94, 203)
            Me.PuertoTextBox.Name = "PuertoTextBox"
            Me.PuertoTextBox.Size = New System.Drawing.Size(100, 22)
            Me.PuertoTextBox.TabIndex = 8
            Me.ToolTipText.SetToolTip(Me.PuertoTextBox, "Puerto por el cual se realiza la conexión")
            '
            'PuertoLabel
            '
            Me.PuertoLabel.AutoSize = True
            Me.PuertoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PuertoLabel.ForeColor = System.Drawing.Color.Maroon
            Me.PuertoLabel.Location = New System.Drawing.Point(9, 203)
            Me.PuertoLabel.Name = "PuertoLabel"
            Me.PuertoLabel.Size = New System.Drawing.Size(52, 16)
            Me.PuertoLabel.TabIndex = 4
            Me.PuertoLabel.Text = "Puerto"
            '
            'AppNameLabel
            '
            Me.AppNameLabel.AutoSize = True
            Me.AppNameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AppNameLabel.ForeColor = System.Drawing.Color.Maroon
            Me.AppNameLabel.Location = New System.Drawing.Point(8, 96)
            Me.AppNameLabel.Name = "AppNameLabel"
            Me.AppNameLabel.Size = New System.Drawing.Size(76, 16)
            Me.AppNameLabel.TabIndex = 0
            Me.AppNameLabel.Text = "AppName"
            '
            'AppNameTextBox
            '
            Me.AppNameTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AppNameTextBox.Location = New System.Drawing.Point(111, 96)
            Me.AppNameTextBox.Name = "AppNameTextBox"
            Me.AppNameTextBox.Size = New System.Drawing.Size(314, 22)
            Me.AppNameTextBox.TabIndex = 4
            Me.ToolTipText.SetToolTip(Me.AppNameTextBox, "Nombre que utiliza la aplicación para publicar las referencia")
            '
            'NotificacionNotifyIcon
            '
            Me.NotificacionNotifyIcon.ContextMenuStrip = Me.MenuContextMenuStrip
            Me.NotificacionNotifyIcon.Icon = CType(resources.GetObject("NotificacionNotifyIcon.Icon"), System.Drawing.Icon)
            Me.NotificacionNotifyIcon.Text = "SLYG FileSendingMonitor"
            Me.NotificacionNotifyIcon.Visible = True
            '
            'MenuContextMenuStrip
            '
            Me.MenuContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbrirToolStripMenuItem, Me.ToolStripSeparator1, Me.SalirContextualToolStripMenuItem})
            Me.MenuContextMenuStrip.Name = "cmsMenu"
            Me.MenuContextMenuStrip.Size = New System.Drawing.Size(173, 54)
            '
            'AbrirToolStripMenuItem
            '
            Me.AbrirToolStripMenuItem.Enabled = False
            Me.AbrirToolStripMenuItem.Name = "AbrirToolStripMenuItem"
            Me.AbrirToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
            Me.AbrirToolStripMenuItem.Text = "Despelgar monitor"
            '
            'ToolStripSeparator1
            '
            Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
            Me.ToolStripSeparator1.Size = New System.Drawing.Size(169, 6)
            '
            'SalirContextualToolStripMenuItem
            '
            Me.SalirContextualToolStripMenuItem.Name = "SalirContextualToolStripMenuItem"
            Me.SalirContextualToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
            Me.SalirContextualToolStripMenuItem.Text = "Salir"
            '
            'EstadoStatusStrip
            '
            Me.EstadoStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EstadoStatusStripLabel})
            Me.EstadoStatusStrip.Location = New System.Drawing.Point(0, 405)
            Me.EstadoStatusStrip.Name = "EstadoStatusStrip"
            Me.EstadoStatusStrip.Size = New System.Drawing.Size(479, 22)
            Me.EstadoStatusStrip.TabIndex = 2
            Me.EstadoStatusStrip.Text = "StatusStrip1"
            '
            'EstadoStatusStripLabel
            '
            Me.EstadoStatusStripLabel.ForeColor = System.Drawing.Color.Brown
            Me.EstadoStatusStripLabel.Name = "EstadoStatusStripLabel"
            Me.EstadoStatusStripLabel.Size = New System.Drawing.Size(42, 17)
            Me.EstadoStatusStripLabel.Text = "Estado"
            '
            'TituloLabel
            '
            Me.TituloLabel.AutoSize = True
            Me.TituloLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TituloLabel.ForeColor = System.Drawing.Color.RoyalBlue
            Me.TituloLabel.Location = New System.Drawing.Point(250, 27)
            Me.TituloLabel.Name = "TituloLabel"
            Me.TituloLabel.Size = New System.Drawing.Size(189, 16)
            Me.TituloLabel.TabIndex = 11
            Me.TituloLabel.Text = "Configuración del Servicio"
            Me.TituloLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'WorkingFolderTextBox
            '
            Me.WorkingFolderTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight
            Me.WorkingFolderTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.WorkingFolderTextBox.Location = New System.Drawing.Point(11, 171)
            Me.WorkingFolderTextBox.Name = "WorkingFolderTextBox"
            Me.WorkingFolderTextBox.Size = New System.Drawing.Size(379, 22)
            Me.WorkingFolderTextBox.TabIndex = 6
            Me.ToolTipText.SetToolTip(Me.WorkingFolderTextBox, "Ruta base para buscar las imágenes")
            '
            'ProbarButton
            '
            Me.ProbarButton.Location = New System.Drawing.Point(350, 49)
            Me.ProbarButton.Name = "ProbarButton"
            Me.ProbarButton.Size = New System.Drawing.Size(75, 23)
            Me.ProbarButton.TabIndex = 3
            Me.ProbarButton.Text = "Probar"
            Me.ToolTipText.SetToolTip(Me.ProbarButton, "Probar la configuración del servicio web")
            Me.ProbarButton.UseVisualStyleBackColor = True
            '
            'PasswordTextBox
            '
            Me.PasswordTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PasswordTextBox.ForeColor = System.Drawing.Color.RoyalBlue
            Me.PasswordTextBox.Location = New System.Drawing.Point(111, 64)
            Me.PasswordTextBox.Name = "PasswordTextBox"
            Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.PasswordTextBox.Size = New System.Drawing.Size(191, 22)
            Me.PasswordTextBox.TabIndex = 2
            Me.ToolTipText.SetToolTip(Me.PasswordTextBox, "Contraseña del usuario de inicio de sesión")
            '
            'UserTextBox
            '
            Me.UserTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.UserTextBox.ForeColor = System.Drawing.Color.MidnightBlue
            Me.UserTextBox.Location = New System.Drawing.Point(111, 36)
            Me.UserTextBox.Name = "UserTextBox"
            Me.UserTextBox.ReadOnly = True
            Me.UserTextBox.Size = New System.Drawing.Size(191, 22)
            Me.UserTextBox.TabIndex = 1
            Me.UserTextBox.Text = "Service"
            Me.ToolTipText.SetToolTip(Me.UserTextBox, "Usuario de inicio de sesión")
            '
            'ServicioWebTextBox
            '
            Me.ServicioWebTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ServicioWebTextBox.ForeColor = System.Drawing.Color.RoyalBlue
            Me.ServicioWebTextBox.Location = New System.Drawing.Point(111, 8)
            Me.ServicioWebTextBox.Name = "ServicioWebTextBox"
            Me.ServicioWebTextBox.Size = New System.Drawing.Size(314, 22)
            Me.ServicioWebTextBox.TabIndex = 0
            Me.ToolTipText.SetToolTip(Me.ServicioWebTextBox, "Servicio web de validación de usuarios")
            '
            'IdentifierDateFormatTextBox
            '
            Me.IdentifierDateFormatTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.IdentifierDateFormatTextBox.Location = New System.Drawing.Point(189, 125)
            Me.IdentifierDateFormatTextBox.Name = "IdentifierDateFormatTextBox"
            Me.IdentifierDateFormatTextBox.Size = New System.Drawing.Size(236, 22)
            Me.IdentifierDateFormatTextBox.TabIndex = 5
            Me.ToolTipText.SetToolTip(Me.IdentifierDateFormatTextBox, "Nombre que utiliza la aplicación para publicar las referencia")
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
            'GuardarServicioButton
            '
            Me.GuardarServicioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GuardarServicioButton.Image = Global.Miharu.FileSending.Monitor.My.Resources.Resources.save
            Me.GuardarServicioButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarServicioButton.Location = New System.Drawing.Point(51, 357)
            Me.GuardarServicioButton.Name = "GuardarServicioButton"
            Me.GuardarServicioButton.Size = New System.Drawing.Size(147, 28)
            Me.GuardarServicioButton.TabIndex = 12
            Me.GuardarServicioButton.Text = "Almacenar cambios"
            Me.GuardarServicioButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ToolTipText.SetToolTip(Me.GuardarServicioButton, "Almacenar la parametrización del servicio")
            Me.GuardarServicioButton.UseVisualStyleBackColor = True
            '
            'IniciarButton
            '
            Me.IniciarButton.Image = Global.Miharu.FileSending.Monitor.My.Resources.Resources.Iniciar
            Me.IniciarButton.Location = New System.Drawing.Point(10, 22)
            Me.IniciarButton.Name = "IniciarButton"
            Me.IniciarButton.Size = New System.Drawing.Size(32, 32)
            Me.IniciarButton.TabIndex = 1
            Me.ToolTipText.SetToolTip(Me.IniciarButton, "Iniciar el servicio")
            Me.IniciarButton.UseVisualStyleBackColor = True
            '
            'DetenerButton
            '
            Me.DetenerButton.Image = Global.Miharu.FileSending.Monitor.My.Resources.Resources.Detener
            Me.DetenerButton.Location = New System.Drawing.Point(48, 22)
            Me.DetenerButton.Name = "DetenerButton"
            Me.DetenerButton.Size = New System.Drawing.Size(32, 32)
            Me.DetenerButton.TabIndex = 2
            Me.ToolTipText.SetToolTip(Me.DetenerButton, "Detener el servicio")
            Me.DetenerButton.UseVisualStyleBackColor = True
            '
            'ReiniciarButton
            '
            Me.ReiniciarButton.Image = Global.Miharu.FileSending.Monitor.My.Resources.Resources.Reiniciar
            Me.ReiniciarButton.Location = New System.Drawing.Point(109, 22)
            Me.ReiniciarButton.Name = "ReiniciarButton"
            Me.ReiniciarButton.Size = New System.Drawing.Size(32, 32)
            Me.ReiniciarButton.TabIndex = 0
            Me.ToolTipText.SetToolTip(Me.ReiniciarButton, "Reiniciar el servicio")
            Me.ReiniciarButton.UseVisualStyleBackColor = True
            '
            'BaseMenuStrip
            '
            Me.BaseMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem, Me.AyudaToolStripMenuItem})
            Me.BaseMenuStrip.Location = New System.Drawing.Point(0, 0)
            Me.BaseMenuStrip.Name = "BaseMenuStrip"
            Me.BaseMenuStrip.Size = New System.Drawing.Size(479, 24)
            Me.BaseMenuStrip.TabIndex = 0
            Me.BaseMenuStrip.Text = "MenuStrip1"
            '
            'ArchivoToolStripMenuItem
            '
            Me.ArchivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalirToolStripMenuItem})
            Me.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem"
            Me.ArchivoToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
            Me.ArchivoToolStripMenuItem.Text = "&Archivo"
            '
            'SalirToolStripMenuItem
            '
            Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
            Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(96, 22)
            Me.SalirToolStripMenuItem.Text = "Salir"
            '
            'AyudaToolStripMenuItem
            '
            Me.AyudaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AcercaDeToolStripMenuItem})
            Me.AyudaToolStripMenuItem.Name = "AyudaToolStripMenuItem"
            Me.AyudaToolStripMenuItem.Size = New System.Drawing.Size(53, 20)
            Me.AyudaToolStripMenuItem.Text = "A&yuda"
            '
            'AcercaDeToolStripMenuItem
            '
            Me.AcercaDeToolStripMenuItem.Name = "AcercaDeToolStripMenuItem"
            Me.AcercaDeToolStripMenuItem.Size = New System.Drawing.Size(242, 22)
            Me.AcercaDeToolStripMenuItem.Text = "Acerca de FileSendingMonitor..."
            '
            'MonitorServicioTimer
            '
            Me.MonitorServicioTimer.Interval = 1000
            '
            'IconosImageList
            '
            Me.IconosImageList.ImageStream = CType(resources.GetObject("IconosImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.IconosImageList.TransparentColor = System.Drawing.Color.Transparent
            Me.IconosImageList.Images.SetKeyName(0, "FileSendingIconActivo.ico")
            Me.IconosImageList.Images.SetKeyName(1, "FileSendingIconInctivo.ico")
            '
            'BaseTabControl
            '
            Me.BaseTabControl.Controls.Add(Me.ServicioTabPage)
            Me.BaseTabControl.Controls.Add(Me.RemotingTabPage)
            Me.BaseTabControl.Dock = System.Windows.Forms.DockStyle.Top
            Me.BaseTabControl.ImageList = Me.TabImageList
            Me.BaseTabControl.Location = New System.Drawing.Point(0, 24)
            Me.BaseTabControl.Name = "BaseTabControl"
            Me.BaseTabControl.SelectedIndex = 0
            Me.BaseTabControl.Size = New System.Drawing.Size(479, 304)
            Me.BaseTabControl.TabIndex = 1
            '
            'ServicioTabPage
            '
            Me.ServicioTabPage.Controls.Add(Me.LogActivoCheckBox)
            Me.ServicioTabPage.Controls.Add(Me.Label7)
            Me.ServicioTabPage.Controls.Add(Me.LogFolderTextBox)
            Me.ServicioTabPage.Controls.Add(Me.Label6)
            Me.ServicioTabPage.Controls.Add(Me.DataRemotingCheckBox)
            Me.ServicioTabPage.Controls.Add(Me.RemotingLabel)
            Me.ServicioTabPage.Controls.Add(Me.IdentifierDateFormatLabel)
            Me.ServicioTabPage.Controls.Add(Me.IdentifierDateFormatTextBox)
            Me.ServicioTabPage.Controls.Add(Me.ProbarButton)
            Me.ServicioTabPage.Controls.Add(Me.PasswordTextBox)
            Me.ServicioTabPage.Controls.Add(Me.PasswordLabel)
            Me.ServicioTabPage.Controls.Add(Me.UserTextBox)
            Me.ServicioTabPage.Controls.Add(Me.UserLabel)
            Me.ServicioTabPage.Controls.Add(Me.ServicioWebTextBox)
            Me.ServicioTabPage.Controls.Add(Me.ServicioWebLabel)
            Me.ServicioTabPage.Controls.Add(Me.CargarPathButton)
            Me.ServicioTabPage.Controls.Add(Me.WorkingFolderTextBox)
            Me.ServicioTabPage.Controls.Add(Me.AppNameLabel)
            Me.ServicioTabPage.Controls.Add(Me.WorkingFolderLabel)
            Me.ServicioTabPage.Controls.Add(Me.PuertoTextBox)
            Me.ServicioTabPage.Controls.Add(Me.PuertoLabel)
            Me.ServicioTabPage.Controls.Add(Me.AppNameTextBox)
            Me.ServicioTabPage.ImageIndex = 0
            Me.ServicioTabPage.Location = New System.Drawing.Point(4, 23)
            Me.ServicioTabPage.Name = "ServicioTabPage"
            Me.ServicioTabPage.Padding = New System.Windows.Forms.Padding(3)
            Me.ServicioTabPage.Size = New System.Drawing.Size(471, 277)
            Me.ServicioTabPage.TabIndex = 0
            Me.ServicioTabPage.Text = "Servicio"
            Me.ServicioTabPage.UseVisualStyleBackColor = True
            '
            'DataRemotingCheckBox
            '
            Me.DataRemotingCheckBox.AutoSize = True
            Me.DataRemotingCheckBox.Location = New System.Drawing.Point(375, 205)
            Me.DataRemotingCheckBox.Name = "DataRemotingCheckBox"
            Me.DataRemotingCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.DataRemotingCheckBox.TabIndex = 34
            Me.DataRemotingCheckBox.UseVisualStyleBackColor = True
            '
            'RemotingLabel
            '
            Me.RemotingLabel.AutoSize = True
            Me.RemotingLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.RemotingLabel.ForeColor = System.Drawing.Color.Maroon
            Me.RemotingLabel.Location = New System.Drawing.Point(223, 203)
            Me.RemotingLabel.Name = "RemotingLabel"
            Me.RemotingLabel.Size = New System.Drawing.Size(138, 16)
            Me.RemotingLabel.TabIndex = 21
            Me.RemotingLabel.Text = "Usa DataRemoting"
            '
            'IdentifierDateFormatLabel
            '
            Me.IdentifierDateFormatLabel.AutoSize = True
            Me.IdentifierDateFormatLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.IdentifierDateFormatLabel.ForeColor = System.Drawing.Color.Maroon
            Me.IdentifierDateFormatLabel.Location = New System.Drawing.Point(8, 128)
            Me.IdentifierDateFormatLabel.Name = "IdentifierDateFormatLabel"
            Me.IdentifierDateFormatLabel.Size = New System.Drawing.Size(111, 16)
            Me.IdentifierDateFormatLabel.TabIndex = 20
            Me.IdentifierDateFormatLabel.Text = "Formato Fecha"
            '
            'PasswordLabel
            '
            Me.PasswordLabel.AutoSize = True
            Me.PasswordLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PasswordLabel.ForeColor = System.Drawing.Color.Maroon
            Me.PasswordLabel.Location = New System.Drawing.Point(8, 70)
            Me.PasswordLabel.Name = "PasswordLabel"
            Me.PasswordLabel.Size = New System.Drawing.Size(86, 16)
            Me.PasswordLabel.TabIndex = 17
            Me.PasswordLabel.Text = "Contraseña"
            '
            'UserLabel
            '
            Me.UserLabel.AutoSize = True
            Me.UserLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.UserLabel.ForeColor = System.Drawing.Color.Maroon
            Me.UserLabel.Location = New System.Drawing.Point(9, 40)
            Me.UserLabel.Name = "UserLabel"
            Me.UserLabel.Size = New System.Drawing.Size(61, 16)
            Me.UserLabel.TabIndex = 15
            Me.UserLabel.Text = "Usuario"
            '
            'ServicioWebLabel
            '
            Me.ServicioWebLabel.AutoSize = True
            Me.ServicioWebLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ServicioWebLabel.ForeColor = System.Drawing.Color.Maroon
            Me.ServicioWebLabel.Location = New System.Drawing.Point(8, 9)
            Me.ServicioWebLabel.Name = "ServicioWebLabel"
            Me.ServicioWebLabel.Size = New System.Drawing.Size(96, 16)
            Me.ServicioWebLabel.TabIndex = 13
            Me.ServicioWebLabel.Text = "Servicio web"
            '
            'CargarPathButton
            '
            Me.CargarPathButton.Image = Global.Miharu.FileSending.Monitor.My.Resources.Resources.Directorios
            Me.CargarPathButton.Location = New System.Drawing.Point(397, 170)
            Me.CargarPathButton.Name = "CargarPathButton"
            Me.CargarPathButton.Size = New System.Drawing.Size(28, 24)
            Me.CargarPathButton.TabIndex = 7
            '
            'RemotingTabPage
            '
            Me.RemotingTabPage.Controls.Add(Me.RemotingPanel)
            Me.RemotingTabPage.Location = New System.Drawing.Point(4, 23)
            Me.RemotingTabPage.Name = "RemotingTabPage"
            Me.RemotingTabPage.Padding = New System.Windows.Forms.Padding(3)
            Me.RemotingTabPage.Size = New System.Drawing.Size(471, 247)
            Me.RemotingTabPage.TabIndex = 1
            Me.RemotingTabPage.Text = "Remoting"
            Me.RemotingTabPage.UseVisualStyleBackColor = True
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
            Me.RemotingPanel.Location = New System.Drawing.Point(22, 19)
            Me.RemotingPanel.Name = "RemotingPanel"
            Me.RemotingPanel.Size = New System.Drawing.Size(430, 214)
            Me.RemotingPanel.TabIndex = 0
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
            'TabImageList
            '
            Me.TabImageList.ImageStream = CType(resources.GetObject("TabImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.TabImageList.TransparentColor = System.Drawing.Color.Transparent
            Me.TabImageList.Images.SetKeyName(0, "Config.png")
            Me.TabImageList.Images.SetKeyName(1, "folder_explore.png")
            '
            'ServicioGroupBox
            '
            Me.ServicioGroupBox.Controls.Add(Me.IniciarButton)
            Me.ServicioGroupBox.Controls.Add(Me.DetenerButton)
            Me.ServicioGroupBox.Controls.Add(Me.ReiniciarButton)
            Me.ServicioGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ServicioGroupBox.ForeColor = System.Drawing.Color.Maroon
            Me.ServicioGroupBox.Location = New System.Drawing.Point(283, 334)
            Me.ServicioGroupBox.Name = "ServicioGroupBox"
            Me.ServicioGroupBox.Size = New System.Drawing.Size(147, 65)
            Me.ServicioGroupBox.TabIndex = 13
            Me.ServicioGroupBox.TabStop = False
            Me.ServicioGroupBox.Text = "Servicio"
            '
            'LogFolderTextBox
            '
            Me.LogFolderTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight
            Me.LogFolderTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LogFolderTextBox.Location = New System.Drawing.Point(8, 252)
            Me.LogFolderTextBox.Name = "LogFolderTextBox"
            Me.LogFolderTextBox.Size = New System.Drawing.Size(361, 22)
            Me.LogFolderTextBox.TabIndex = 36
            Me.LogFolderTextBox.Text = "C:\Temp\LogsFileSendingDDO"
            Me.ToolTipText.SetToolTip(Me.LogFolderTextBox, "Ruta base para buscar las imágenes")
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label6.ForeColor = System.Drawing.Color.Maroon
            Me.Label6.Location = New System.Drawing.Point(5, 233)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(127, 16)
            Me.Label6.TabIndex = 35
            Me.Label6.Text = "Directorio de Log"
            '
            'LogActivoCheckBox
            '
            Me.LogActivoCheckBox.AutoSize = True
            Me.LogActivoCheckBox.Location = New System.Drawing.Point(411, 252)
            Me.LogActivoCheckBox.Name = "LogActivoCheckBox"
            Me.LogActivoCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.LogActivoCheckBox.TabIndex = 38
            Me.LogActivoCheckBox.UseVisualStyleBackColor = True
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label7.ForeColor = System.Drawing.Color.Maroon
            Me.Label7.Location = New System.Drawing.Point(372, 233)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(80, 16)
            Me.Label7.TabIndex = 37
            Me.Label7.Text = "Log Activo"
            '
            'MainForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(479, 427)
            Me.Controls.Add(Me.GuardarServicioButton)
            Me.Controls.Add(Me.ServicioGroupBox)
            Me.Controls.Add(Me.TituloLabel)
            Me.Controls.Add(Me.BaseTabControl)
            Me.Controls.Add(Me.EstadoStatusStrip)
            Me.Controls.Add(Me.BaseMenuStrip)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MainMenuStrip = Me.BaseMenuStrip
            Me.MaximizeBox = False
            Me.Name = "MainForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "SLYG FileSendingMonitor"
            Me.MenuContextMenuStrip.ResumeLayout(False)
            Me.EstadoStatusStrip.ResumeLayout(False)
            Me.EstadoStatusStrip.PerformLayout()
            Me.BaseMenuStrip.ResumeLayout(False)
            Me.BaseMenuStrip.PerformLayout()
            Me.BaseTabControl.ResumeLayout(False)
            Me.ServicioTabPage.ResumeLayout(False)
            Me.ServicioTabPage.PerformLayout()
            Me.RemotingTabPage.ResumeLayout(False)
            Me.RemotingPanel.ResumeLayout(False)
            Me.RemotingPanel.PerformLayout()
            Me.ServicioGroupBox.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents WorkingFolderLabel As System.Windows.Forms.Label
        Friend WithEvents PuertoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents PuertoLabel As System.Windows.Forms.Label
        Friend WithEvents AppNameLabel As System.Windows.Forms.Label
        Friend WithEvents AppNameTextBox As System.Windows.Forms.TextBox
        Friend WithEvents NotificacionNotifyIcon As System.Windows.Forms.NotifyIcon
        Friend WithEvents EstadoStatusStrip As System.Windows.Forms.StatusStrip
        Friend WithEvents EstadoStatusStripLabel As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents TituloLabel As System.Windows.Forms.Label
        Friend WithEvents ToolTipText As System.Windows.Forms.ToolTip
        Friend WithEvents BaseMenuStrip As System.Windows.Forms.MenuStrip
        Friend WithEvents ArchivoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SalirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents AyudaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents AcercaDeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents MonitorServicioTimer As System.Windows.Forms.Timer
        Friend WithEvents IconosImageList As System.Windows.Forms.ImageList
        Friend WithEvents MenuContextMenuStrip As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents AbrirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents SalirContextualToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BaseTabControl As System.Windows.Forms.TabControl
        Friend WithEvents ServicioTabPage As System.Windows.Forms.TabPage
        Friend WithEvents TabImageList As System.Windows.Forms.ImageList
        Friend WithEvents CargarPathButton As System.Windows.Forms.Button
        Friend WithEvents WorkingFolderTextBox As System.Windows.Forms.TextBox
        Friend WithEvents ProbarButton As System.Windows.Forms.Button
        Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
        Friend WithEvents PasswordLabel As System.Windows.Forms.Label
        Friend WithEvents UserTextBox As System.Windows.Forms.TextBox
        Friend WithEvents UserLabel As System.Windows.Forms.Label
        Friend WithEvents ServicioWebTextBox As System.Windows.Forms.TextBox
        Friend WithEvents ServicioWebLabel As System.Windows.Forms.Label
        Friend WithEvents IdentifierDateFormatLabel As System.Windows.Forms.Label
        Friend WithEvents IdentifierDateFormatTextBox As System.Windows.Forms.TextBox
        Friend WithEvents RemotingTabPage As System.Windows.Forms.TabPage
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
        Friend WithEvents GuardarServicioButton As System.Windows.Forms.Button
        Friend WithEvents ServicioGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents IniciarButton As System.Windows.Forms.Button
        Friend WithEvents DetenerButton As System.Windows.Forms.Button
        Friend WithEvents ReiniciarButton As System.Windows.Forms.Button
        Friend WithEvents LogFolderTextBox As TextBox
        Friend WithEvents Label6 As Label
        Friend WithEvents LogActivoCheckBox As CheckBox
        Friend WithEvents Label7 As Label
    End Class
End Namespace