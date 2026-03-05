Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class MainForm
        Inherits System.Windows.Forms.Form

        'Form reemplaza a Dispose para limpiar la lista de componentes.
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

        'Requerido por el Diseñador de Windows Forms
        Private components As System.ComponentModel.IContainer

        'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
        'Se puede modificar usando el Diseñador de Windows Forms.  
        'No lo modifique con el editor de código.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
            Me.MainMenu = New System.Windows.Forms.MenuStrip()
            Me.mnuArchivo = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuSalir = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuAyuda = New System.Windows.Forms.ToolStripMenuItem()
            Me.AcercaDeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.EstadoStatusStrip = New System.Windows.Forms.StatusStrip()
            Me.EstadoStatusStripLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.tcBase = New System.Windows.Forms.TabControl()
            Me.tpServicio = New System.Windows.Forms.TabPage()
            Me.ProbarButton = New System.Windows.Forms.Button()
            Me.PasswordTextBox = New System.Windows.Forms.TextBox()
            Me.lblContraseña = New System.Windows.Forms.Label()
            Me.UserTextBox = New System.Windows.Forms.TextBox()
            Me.lblUsuario = New System.Windows.Forms.Label()
            Me.ServicioWebTextBox = New System.Windows.Forms.TextBox()
            Me.lblServicioWeb = New System.Windows.Forms.Label()
            Me.lblMilisegundos = New System.Windows.Forms.Label()
            Me.IntervaloTextBox = New System.Windows.Forms.TextBox()
            Me.lblIntervalo = New System.Windows.Forms.Label()
            Me.GuardarServicioButton = New System.Windows.Forms.Button()
            Me.IniciarButton = New System.Windows.Forms.Button()
            Me.DetenerButton = New System.Windows.Forms.Button()
            Me.ReiniciarButton = New System.Windows.Forms.Button()
            Me.ToolTipText = New System.Windows.Forms.ToolTip(Me.components)
            Me.MonitorServicioTimer = New System.Windows.Forms.Timer(Me.components)
            Me.IconosImageList = New System.Windows.Forms.ImageList(Me.components)
            Me.NotificacionNotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
            Me.MenuContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.AbrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
            Me.SalirContextualToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.MainMenu.SuspendLayout()
            Me.EstadoStatusStrip.SuspendLayout()
            Me.tcBase.SuspendLayout()
            Me.tpServicio.SuspendLayout()
            Me.MenuContextMenuStrip.SuspendLayout()
            Me.SuspendLayout()
            '
            'MainMenu
            '
            Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuArchivo, Me.mnuAyuda})
            Me.MainMenu.Location = New System.Drawing.Point(0, 0)
            Me.MainMenu.Name = "MainMenu"
            Me.MainMenu.Size = New System.Drawing.Size(466, 24)
            Me.MainMenu.TabIndex = 3
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
            Me.AcercaDeToolStripMenuItem.Size = New System.Drawing.Size(239, 22)
            Me.AcercaDeToolStripMenuItem.Text = "Acerca de CargueLogMonitor..."
            '
            'EstadoStatusStrip
            '
            Me.EstadoStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EstadoStatusStripLabel})
            Me.EstadoStatusStrip.Location = New System.Drawing.Point(0, 289)
            Me.EstadoStatusStrip.Name = "EstadoStatusStrip"
            Me.EstadoStatusStrip.Size = New System.Drawing.Size(466, 22)
            Me.EstadoStatusStrip.TabIndex = 6
            Me.EstadoStatusStrip.Text = "StatusStrip1"
            '
            'EstadoStatusStripLabel
            '
            Me.EstadoStatusStripLabel.ForeColor = System.Drawing.Color.Brown
            Me.EstadoStatusStripLabel.Name = "EstadoStatusStripLabel"
            Me.EstadoStatusStripLabel.Size = New System.Drawing.Size(42, 17)
            Me.EstadoStatusStripLabel.Text = "Estado"
            '
            'tcBase
            '
            Me.tcBase.Controls.Add(Me.tpServicio)
            Me.tcBase.ItemSize = New System.Drawing.Size(72, 19)
            Me.tcBase.Location = New System.Drawing.Point(1, 37)
            Me.tcBase.Name = "tcBase"
            Me.tcBase.SelectedIndex = 0
            Me.tcBase.Size = New System.Drawing.Size(453, 200)
            Me.tcBase.TabIndex = 7
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
            'ProbarButton
            '
            Me.ProbarButton.Location = New System.Drawing.Point(357, 115)
            Me.ProbarButton.Name = "ProbarButton"
            Me.ProbarButton.Size = New System.Drawing.Size(75, 23)
            Me.ProbarButton.TabIndex = 16
            Me.ProbarButton.Text = "Probar"
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
            '
            'lblContraseña
            '
            Me.lblContraseña.AutoSize = True
            Me.lblContraseña.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblContraseña.ForeColor = System.Drawing.Color.Maroon
            Me.lblContraseña.Location = New System.Drawing.Point(16, 115)
            Me.lblContraseña.Name = "lblContraseña"
            Me.lblContraseña.Size = New System.Drawing.Size(86, 16)
            Me.lblContraseña.TabIndex = 14
            Me.lblContraseña.Text = "Contraseña"
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
            '
            'lblUsuario
            '
            Me.lblUsuario.AutoSize = True
            Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblUsuario.ForeColor = System.Drawing.Color.Maroon
            Me.lblUsuario.Location = New System.Drawing.Point(16, 87)
            Me.lblUsuario.Name = "lblUsuario"
            Me.lblUsuario.Size = New System.Drawing.Size(61, 16)
            Me.lblUsuario.TabIndex = 12
            Me.lblUsuario.Text = "Usuario"
            '
            'ServicioWebTextBox
            '
            Me.ServicioWebTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ServicioWebTextBox.Location = New System.Drawing.Point(118, 59)
            Me.ServicioWebTextBox.Name = "ServicioWebTextBox"
            Me.ServicioWebTextBox.Size = New System.Drawing.Size(314, 22)
            Me.ServicioWebTextBox.TabIndex = 11
            '
            'lblServicioWeb
            '
            Me.lblServicioWeb.AutoSize = True
            Me.lblServicioWeb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblServicioWeb.ForeColor = System.Drawing.Color.Maroon
            Me.lblServicioWeb.Location = New System.Drawing.Point(16, 59)
            Me.lblServicioWeb.Name = "lblServicioWeb"
            Me.lblServicioWeb.Size = New System.Drawing.Size(96, 16)
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
            Me.lblMilisegundos.Size = New System.Drawing.Size(27, 16)
            Me.lblMilisegundos.TabIndex = 9
            Me.lblMilisegundos.Text = "ms"
            '
            'IntervaloTextBox
            '
            Me.IntervaloTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.IntervaloTextBox.Location = New System.Drawing.Point(176, 19)
            Me.IntervaloTextBox.Name = "IntervaloTextBox"
            Me.IntervaloTextBox.Size = New System.Drawing.Size(100, 22)
            Me.IntervaloTextBox.TabIndex = 5
            '
            'lblIntervalo
            '
            Me.lblIntervalo.AutoSize = True
            Me.lblIntervalo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblIntervalo.ForeColor = System.Drawing.Color.Maroon
            Me.lblIntervalo.Location = New System.Drawing.Point(16, 19)
            Me.lblIntervalo.Name = "lblIntervalo"
            Me.lblIntervalo.Size = New System.Drawing.Size(131, 16)
            Me.lblIntervalo.TabIndex = 4
            Me.lblIntervalo.Text = "Intervalo escaneo"
            '
            'GuardarServicioButton
            '
            Me.GuardarServicioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GuardarServicioButton.Image = CType(resources.GetObject("GuardarServicioButton.Image"), System.Drawing.Image)
            Me.GuardarServicioButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarServicioButton.Location = New System.Drawing.Point(293, 248)
            Me.GuardarServicioButton.Name = "GuardarServicioButton"
            Me.GuardarServicioButton.Size = New System.Drawing.Size(147, 28)
            Me.GuardarServicioButton.TabIndex = 21
            Me.GuardarServicioButton.Text = "Almacenar cambios"
            Me.GuardarServicioButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarServicioButton.UseVisualStyleBackColor = True
            '
            'IniciarButton
            '
            Me.IniciarButton.Image = CType(resources.GetObject("IniciarButton.Image"), System.Drawing.Image)
            Me.IniciarButton.Location = New System.Drawing.Point(27, 248)
            Me.IniciarButton.Name = "IniciarButton"
            Me.IniciarButton.Size = New System.Drawing.Size(32, 32)
            Me.IniciarButton.TabIndex = 19
            Me.IniciarButton.UseVisualStyleBackColor = True
            '
            'DetenerButton
            '
            Me.DetenerButton.Image = CType(resources.GetObject("DetenerButton.Image"), System.Drawing.Image)
            Me.DetenerButton.Location = New System.Drawing.Point(65, 248)
            Me.DetenerButton.Name = "DetenerButton"
            Me.DetenerButton.Size = New System.Drawing.Size(32, 32)
            Me.DetenerButton.TabIndex = 20
            Me.DetenerButton.UseVisualStyleBackColor = True
            '
            'ReiniciarButton
            '
            Me.ReiniciarButton.Image = CType(resources.GetObject("ReiniciarButton.Image"), System.Drawing.Image)
            Me.ReiniciarButton.Location = New System.Drawing.Point(126, 248)
            Me.ReiniciarButton.Name = "ReiniciarButton"
            Me.ReiniciarButton.Size = New System.Drawing.Size(32, 32)
            Me.ReiniciarButton.TabIndex = 22
            Me.ReiniciarButton.UseVisualStyleBackColor = True
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
            'NotificacionNotifyIcon
            '
            Me.NotificacionNotifyIcon.ContextMenuStrip = Me.MenuContextMenuStrip
            Me.NotificacionNotifyIcon.Text = "PYC SynergeticsTransferMonitor"
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
            'MainForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(466, 311)
            Me.Controls.Add(Me.GuardarServicioButton)
            Me.Controls.Add(Me.IniciarButton)
            Me.Controls.Add(Me.DetenerButton)
            Me.Controls.Add(Me.ReiniciarButton)
            Me.Controls.Add(Me.tcBase)
            Me.Controls.Add(Me.EstadoStatusStrip)
            Me.Controls.Add(Me.MainMenu)
            Me.Name = "MainForm"
            Me.Text = "Miharu CargueLogDDO Monitor"
            Me.MainMenu.ResumeLayout(False)
            Me.MainMenu.PerformLayout()
            Me.EstadoStatusStrip.ResumeLayout(False)
            Me.EstadoStatusStrip.PerformLayout()
            Me.tcBase.ResumeLayout(False)
            Me.tpServicio.ResumeLayout(False)
            Me.tpServicio.PerformLayout()
            Me.MenuContextMenuStrip.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents MainMenu As MenuStrip
        Friend WithEvents mnuArchivo As ToolStripMenuItem
        Friend WithEvents mnuSalir As ToolStripMenuItem
        Friend WithEvents mnuAyuda As ToolStripMenuItem
        Friend WithEvents AcercaDeToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents EstadoStatusStrip As StatusStrip
        Friend WithEvents EstadoStatusStripLabel As ToolStripStatusLabel
        Friend WithEvents tcBase As TabControl
        Friend WithEvents tpServicio As TabPage
        Friend WithEvents ProbarButton As Button
        Friend WithEvents PasswordTextBox As TextBox
        Friend WithEvents lblContraseña As Label
        Friend WithEvents UserTextBox As TextBox
        Friend WithEvents lblUsuario As Label
        Friend WithEvents ServicioWebTextBox As TextBox
        Friend WithEvents lblServicioWeb As Label
        Friend WithEvents lblMilisegundos As Label
        Friend WithEvents IntervaloTextBox As TextBox
        Friend WithEvents lblIntervalo As Label
        Friend WithEvents GuardarServicioButton As Button
        Friend WithEvents IniciarButton As Button
        Friend WithEvents DetenerButton As Button
        Friend WithEvents ReiniciarButton As Button
        Friend WithEvents ToolTipText As ToolTip
        Friend WithEvents MonitorServicioTimer As Timer
        Friend WithEvents IconosImageList As ImageList
        Friend WithEvents NotificacionNotifyIcon As NotifyIcon
        Friend WithEvents MenuContextMenuStrip As ContextMenuStrip
        Friend WithEvents AbrirToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
        Friend WithEvents SalirContextualToolStripMenuItem As ToolStripMenuItem
    End Class
End Namespace