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
            Me.IniciarButton = New System.Windows.Forms.Button()
            Me.ReiniciarButton = New System.Windows.Forms.Button()
            Me.DetenerButton = New System.Windows.Forms.Button()
            Me.IntervaloTextBox = New System.Windows.Forms.TextBox()
            Me.lblIntervalo = New System.Windows.Forms.Label()
            Me.NotificacionNotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
            Me.MenuContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.AbrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
            Me.SalirContextualToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.EstadoStatusStrip = New System.Windows.Forms.StatusStrip()
            Me.EstadoStatusStripLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.lblTitulo = New System.Windows.Forms.Label()
            Me.ToolTipText = New System.Windows.Forms.ToolTip(Me.components)
            Me.GuardarServicioButton = New System.Windows.Forms.Button()
            Me.ProbarButton = New System.Windows.Forms.Button()
            Me.PasswordTextBox = New System.Windows.Forms.TextBox()
            Me.UserTextBox = New System.Windows.Forms.TextBox()
            Me.ServicioWebTextBox = New System.Windows.Forms.TextBox()
            Me.MainMenu = New System.Windows.Forms.MenuStrip()
            Me.mnuArchivo = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuSalir = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuAyuda = New System.Windows.Forms.ToolStripMenuItem()
            Me.AcercaDeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.MonitorServicioTimer = New System.Windows.Forms.Timer(Me.components)
            Me.IconosImageList = New System.Windows.Forms.ImageList(Me.components)
            Me.tcBase = New System.Windows.Forms.TabControl()
            Me.tpServicio = New System.Windows.Forms.TabPage()
            Me.lblContraseña = New System.Windows.Forms.Label()
            Me.lblUsuario = New System.Windows.Forms.Label()
            Me.lblServicioWeb = New System.Windows.Forms.Label()
            Me.lblMilisegundos = New System.Windows.Forms.Label()
            Me.TabImageList = New System.Windows.Forms.ImageList(Me.components)
            Me.MenuContextMenuStrip.SuspendLayout()
            Me.EstadoStatusStrip.SuspendLayout()
            Me.MainMenu.SuspendLayout()
            Me.tcBase.SuspendLayout()
            Me.tpServicio.SuspendLayout()
            Me.SuspendLayout()
            '
            'IniciarButton
            '
            Me.IniciarButton.Image = Global.Miharu.Imaging.FileTransfer.Monitor.My.Resources.Resources.Iniciar
            Me.IniciarButton.Location = New System.Drawing.Point(35, 250)
            Me.IniciarButton.Name = "IniciarButton"
            Me.IniciarButton.Size = New System.Drawing.Size(32, 32)
            Me.IniciarButton.TabIndex = 0
            Me.ToolTipText.SetToolTip(Me.IniciarButton, "Iniciar el servicio")
            Me.IniciarButton.UseVisualStyleBackColor = True
            '
            'ReiniciarButton
            '
            Me.ReiniciarButton.Image = Global.Miharu.Imaging.FileTransfer.Monitor.My.Resources.Resources.Reiniciar
            Me.ReiniciarButton.Location = New System.Drawing.Point(134, 250)
            Me.ReiniciarButton.Name = "ReiniciarButton"
            Me.ReiniciarButton.Size = New System.Drawing.Size(32, 32)
            Me.ReiniciarButton.TabIndex = 2
            Me.ToolTipText.SetToolTip(Me.ReiniciarButton, "Reiniciar el servicio")
            Me.ReiniciarButton.UseVisualStyleBackColor = True
            '
            'DetenerButton
            '
            Me.DetenerButton.Image = Global.Miharu.Imaging.FileTransfer.Monitor.My.Resources.Resources.Detener
            Me.DetenerButton.Location = New System.Drawing.Point(73, 250)
            Me.DetenerButton.Name = "DetenerButton"
            Me.DetenerButton.Size = New System.Drawing.Size(32, 32)
            Me.DetenerButton.TabIndex = 1
            Me.ToolTipText.SetToolTip(Me.DetenerButton, "Detener el servicio")
            Me.DetenerButton.UseVisualStyleBackColor = True
            '
            'IntervaloTextBox
            '
            Me.IntervaloTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.IntervaloTextBox.Location = New System.Drawing.Point(176, 19)
            Me.IntervaloTextBox.Name = "IntervaloTextBox"
            Me.IntervaloTextBox.Size = New System.Drawing.Size(100, 22)
            Me.IntervaloTextBox.TabIndex = 5
            Me.ToolTipText.SetToolTip(Me.IntervaloTextBox, "Define el intervalo de tiempo en milisegundos a esperar antes de validar si hay n" & _
                                                           "uevas actividades para procesar")
            '
            'lblIntervalo
            '
            Me.lblIntervalo.AutoSize = True
            Me.lblIntervalo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblIntervalo.ForeColor = System.Drawing.Color.Maroon
            Me.lblIntervalo.Location = New System.Drawing.Point(16, 19)
            Me.lblIntervalo.Name = "lblIntervalo"
            Me.lblIntervalo.Size = New System.Drawing.Size(134, 16)
            Me.lblIntervalo.TabIndex = 4
            Me.lblIntervalo.Text = "Intervalo escaneo"
            '
            'NotificacionNotifyIcon
            '
            Me.NotificacionNotifyIcon.ContextMenuStrip = Me.MenuContextMenuStrip
            Me.NotificacionNotifyIcon.Icon = CType(resources.GetObject("NotificacionNotifyIcon.Icon"), System.Drawing.Icon)
            Me.NotificacionNotifyIcon.Text = "SLYG FileTransferMonitor"
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
            Me.EstadoStatusStrip.Location = New System.Drawing.Point(0, 295)
            Me.EstadoStatusStrip.Name = "EstadoStatusStrip"
            Me.EstadoStatusStrip.Size = New System.Drawing.Size(472, 22)
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
            'lblTitulo
            '
            Me.lblTitulo.AutoSize = True
            Me.lblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTitulo.ForeColor = System.Drawing.Color.RoyalBlue
            Me.lblTitulo.Location = New System.Drawing.Point(276, 35)
            Me.lblTitulo.Name = "lblTitulo"
            Me.lblTitulo.Size = New System.Drawing.Size(195, 16)
            Me.lblTitulo.TabIndex = 11
            Me.lblTitulo.Text = "Configuración del Servicio"
            Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'GuardarServicioButton
            '
            Me.GuardarServicioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GuardarServicioButton.Image = Global.Miharu.Imaging.FileTransfer.Monitor.My.Resources.Resources.save
            Me.GuardarServicioButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarServicioButton.Location = New System.Drawing.Point(301, 250)
            Me.GuardarServicioButton.Name = "GuardarServicioButton"
            Me.GuardarServicioButton.Size = New System.Drawing.Size(147, 28)
            Me.GuardarServicioButton.TabIndex = 7
            Me.GuardarServicioButton.Text = "Almacenar cambios"
            Me.GuardarServicioButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ToolTipText.SetToolTip(Me.GuardarServicioButton, "Almacenar la parametrización del servicio")
            Me.GuardarServicioButton.UseVisualStyleBackColor = True
            '
            'ProbarButton
            '
            Me.ProbarButton.Location = New System.Drawing.Point(357, 115)
            Me.ProbarButton.Name = "ProbarButton"
            Me.ProbarButton.Size = New System.Drawing.Size(75, 23)
            Me.ProbarButton.TabIndex = 16
            Me.ProbarButton.Text = "Probar"
            Me.ToolTipText.SetToolTip(Me.ProbarButton, "Probar la configuración del servicio web")
            Me.ProbarButton.UseVisualStyleBackColor = True
            '
            'PasswordTextBox
            '
            Me.PasswordTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PasswordTextBox.Location = New System.Drawing.Point(118, 115)
            Me.PasswordTextBox.Name = "PasswordTextBox"
            Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.PasswordTextBox.Size = New System.Drawing.Size(191, 22)
            Me.PasswordTextBox.TabIndex = 15
            Me.ToolTipText.SetToolTip(Me.PasswordTextBox, "Contraseña del usuario de inicio de sesión")
            '
            'UserTextBox
            '
            Me.UserTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.UserTextBox.Location = New System.Drawing.Point(118, 87)
            Me.UserTextBox.Name = "UserTextBox"
            Me.UserTextBox.ReadOnly = True
            Me.UserTextBox.Size = New System.Drawing.Size(191, 22)
            Me.UserTextBox.TabIndex = 13
            Me.UserTextBox.Text = "Service"
            Me.ToolTipText.SetToolTip(Me.UserTextBox, "Usuario de inicio de sesión")
            '
            'ServicioWebTextBox
            '
            Me.ServicioWebTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ServicioWebTextBox.Location = New System.Drawing.Point(118, 59)
            Me.ServicioWebTextBox.Name = "ServicioWebTextBox"
            Me.ServicioWebTextBox.Size = New System.Drawing.Size(314, 22)
            Me.ServicioWebTextBox.TabIndex = 11
            Me.ToolTipText.SetToolTip(Me.ServicioWebTextBox, "Servicio web de validación de usuarios")
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
            Me.mnuSalir.Size = New System.Drawing.Size(96, 22)
            Me.mnuSalir.Text = "Salir"
            '
            'mnuAyuda
            '
            Me.mnuAyuda.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AcercaDeToolStripMenuItem})
            Me.mnuAyuda.Name = "mnuAyuda"
            Me.mnuAyuda.Size = New System.Drawing.Size(53, 20)
            Me.mnuAyuda.Text = "A&yuda"
            '
            'AcercaDeToolStripMenuItem
            '
            Me.AcercaDeToolStripMenuItem.Name = "AcercaDeToolStripMenuItem"
            Me.AcercaDeToolStripMenuItem.Size = New System.Drawing.Size(242, 22)
            Me.AcercaDeToolStripMenuItem.Text = "Acerca de FileTransferMonitor..."
            '
            'MonitorServicioTimer
            '
            Me.MonitorServicioTimer.Interval = 1000
            '
            'IconosImageList
            '
            Me.IconosImageList.ImageStream = CType(resources.GetObject("IconosImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.IconosImageList.TransparentColor = System.Drawing.Color.Transparent
            Me.IconosImageList.Images.SetKeyName(0, "FileTransferIconActivo.ico")
            Me.IconosImageList.Images.SetKeyName(1, "FileTransferIconInactivo.ico")
            '
            'tcBase
            '
            Me.tcBase.Controls.Add(Me.tpServicio)
            Me.tcBase.ImageList = Me.TabImageList
            Me.tcBase.Location = New System.Drawing.Point(12, 35)
            Me.tcBase.Name = "tcBase"
            Me.tcBase.SelectedIndex = 0
            Me.tcBase.Size = New System.Drawing.Size(453, 200)
            Me.tcBase.TabIndex = 1
            '
            'tpServicio
            '
            Me.tpServicio.Controls.Add(Me.ProbarButton)
            Me.tpServicio.Controls.Add(Me.PasswordTextBox)
            Me.tpServicio.Controls.Add(Me.lblContraseña)
            Me.tpServicio.Controls.Add(Me.UserTextBox)
            Me.tpServicio.Controls.Add(Me.lblUsuario)
            Me.tpServicio.Controls.Add(Me.ServicioWebTextBox)
            Me.tpServicio.Controls.Add(Me.lblServicioWeb)
            Me.tpServicio.Controls.Add(Me.lblMilisegundos)
            Me.tpServicio.Controls.Add(Me.IntervaloTextBox)
            Me.tpServicio.Controls.Add(Me.lblIntervalo)
            Me.tpServicio.ImageIndex = 0
            Me.tpServicio.Location = New System.Drawing.Point(4, 23)
            Me.tpServicio.Name = "tpServicio"
            Me.tpServicio.Padding = New System.Windows.Forms.Padding(3)
            Me.tpServicio.Size = New System.Drawing.Size(445, 173)
            Me.tpServicio.TabIndex = 0
            Me.tpServicio.Text = "Servicio"
            Me.tpServicio.UseVisualStyleBackColor = True
            '
            'lblContraseña
            '
            Me.lblContraseña.AutoSize = True
            Me.lblContraseña.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblContraseña.ForeColor = System.Drawing.Color.Maroon
            Me.lblContraseña.Location = New System.Drawing.Point(16, 115)
            Me.lblContraseña.Name = "lblContraseña"
            Me.lblContraseña.Size = New System.Drawing.Size(89, 16)
            Me.lblContraseña.TabIndex = 14
            Me.lblContraseña.Text = "Contraseña"
            '
            'lblUsuario
            '
            Me.lblUsuario.AutoSize = True
            Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblUsuario.ForeColor = System.Drawing.Color.Maroon
            Me.lblUsuario.Location = New System.Drawing.Point(16, 87)
            Me.lblUsuario.Name = "lblUsuario"
            Me.lblUsuario.Size = New System.Drawing.Size(63, 16)
            Me.lblUsuario.TabIndex = 12
            Me.lblUsuario.Text = "Usuario"
            '
            'lblServicioWeb
            '
            Me.lblServicioWeb.AutoSize = True
            Me.lblServicioWeb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblServicioWeb.ForeColor = System.Drawing.Color.Maroon
            Me.lblServicioWeb.Location = New System.Drawing.Point(16, 59)
            Me.lblServicioWeb.Name = "lblServicioWeb"
            Me.lblServicioWeb.Size = New System.Drawing.Size(99, 16)
            Me.lblServicioWeb.TabIndex = 10
            Me.lblServicioWeb.Text = "Servicio web"
            '
            'lblMilisegundos
            '
            Me.lblMilisegundos.AutoSize = True
            Me.lblMilisegundos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblMilisegundos.ForeColor = System.Drawing.Color.Maroon
            Me.lblMilisegundos.Location = New System.Drawing.Point(282, 25)
            Me.lblMilisegundos.Name = "lblMilisegundos"
            Me.lblMilisegundos.Size = New System.Drawing.Size(28, 16)
            Me.lblMilisegundos.TabIndex = 9
            Me.lblMilisegundos.Text = "ms"
            '
            'TabImageList
            '
            Me.TabImageList.ImageStream = CType(resources.GetObject("TabImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.TabImageList.TransparentColor = System.Drawing.Color.Transparent
            Me.TabImageList.Images.SetKeyName(0, "Config.png")
            Me.TabImageList.Images.SetKeyName(1, "email_edit.png")
            Me.TabImageList.Images.SetKeyName(2, "database_edit.png")
            '
            'MainForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(472, 317)
            Me.Controls.Add(Me.lblTitulo)
            Me.Controls.Add(Me.tcBase)
            Me.Controls.Add(Me.EstadoStatusStrip)
            Me.Controls.Add(Me.MainMenu)
            Me.Controls.Add(Me.GuardarServicioButton)
            Me.Controls.Add(Me.IniciarButton)
            Me.Controls.Add(Me.DetenerButton)
            Me.Controls.Add(Me.ReiniciarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MainMenuStrip = Me.MainMenu
            Me.MaximizeBox = False
            Me.Name = "MainForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "SLYG FileTransferMonitor"
            Me.MenuContextMenuStrip.ResumeLayout(False)
            Me.EstadoStatusStrip.ResumeLayout(False)
            Me.EstadoStatusStrip.PerformLayout()
            Me.MainMenu.ResumeLayout(False)
            Me.MainMenu.PerformLayout()
            Me.tcBase.ResumeLayout(False)
            Me.tpServicio.ResumeLayout(False)
            Me.tpServicio.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents IniciarButton As System.Windows.Forms.Button
        Friend WithEvents ReiniciarButton As System.Windows.Forms.Button
        Friend WithEvents DetenerButton As System.Windows.Forms.Button
        Friend WithEvents IntervaloTextBox As System.Windows.Forms.TextBox
        Friend WithEvents lblIntervalo As System.Windows.Forms.Label
        Friend WithEvents NotificacionNotifyIcon As System.Windows.Forms.NotifyIcon
        Friend WithEvents EstadoStatusStrip As System.Windows.Forms.StatusStrip
        Friend WithEvents EstadoStatusStripLabel As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents lblTitulo As System.Windows.Forms.Label
        Friend WithEvents ToolTipText As System.Windows.Forms.ToolTip
        Friend WithEvents MainMenu As System.Windows.Forms.MenuStrip
        Friend WithEvents mnuArchivo As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuSalir As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuAyuda As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents AcercaDeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents GuardarServicioButton As System.Windows.Forms.Button
        Friend WithEvents MonitorServicioTimer As System.Windows.Forms.Timer
        Friend WithEvents IconosImageList As System.Windows.Forms.ImageList
        Friend WithEvents MenuContextMenuStrip As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents AbrirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents SalirContextualToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tcBase As System.Windows.Forms.TabControl
        Friend WithEvents tpServicio As System.Windows.Forms.TabPage
        Friend WithEvents TabImageList As System.Windows.Forms.ImageList
        Friend WithEvents lblMilisegundos As System.Windows.Forms.Label
        Friend WithEvents ProbarButton As System.Windows.Forms.Button
        Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
        Friend WithEvents lblContraseña As System.Windows.Forms.Label
        Friend WithEvents UserTextBox As System.Windows.Forms.TextBox
        Friend WithEvents lblUsuario As System.Windows.Forms.Label
        Friend WithEvents ServicioWebTextBox As System.Windows.Forms.TextBox
        Friend WithEvents lblServicioWeb As System.Windows.Forms.Label
    End Class
End Namespace