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
        Me.NotificacionNotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.MainFormContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AbrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SalirContextualToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EstadoStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.EstadoToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TituloLabel = New System.Windows.Forms.Label()
        Me.MensajeToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.AppNameTextBox = New System.Windows.Forms.TextBox()
        Me.PuertoTextBox = New System.Windows.Forms.TextBox()
        Me.ReiniciarButton = New System.Windows.Forms.Button()
        Me.DetenerButton = New System.Windows.Forms.Button()
        Me.IniciarButton = New System.Windows.Forms.Button()
        Me.GuardarServicioButton = New System.Windows.Forms.Button()
        Me.IntervaloTextBox = New System.Windows.Forms.TextBox()
        Me.HistoricoTextBox = New System.Windows.Forms.TextBox()
        Me.MainFormMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AyudaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AcercaDeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MonitorServicioTimer = New System.Windows.Forms.Timer(Me.components)
        Me.IconosImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.TabPageImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.ServicioTabPage = New System.Windows.Forms.TabPage()
        Me.WorkingFolderTextBox = New System.Windows.Forms.TextBox()
        Me.HistorioLabel = New System.Windows.Forms.Label()
        Me.LabelIntervalo = New System.Windows.Forms.Label()
        Me.CargarPathButton = New System.Windows.Forms.Button()
        Me.WorkingFolderLabel = New System.Windows.Forms.Label()
        Me.AppNameLabel = New System.Windows.Forms.Label()
        Me.ServicioGroupBox = New System.Windows.Forms.GroupBox()
        Me.PuertoLabel = New System.Windows.Forms.Label()
        Me.MainFormTabControl = New System.Windows.Forms.TabControl()
        Me.MainFormContextMenuStrip.SuspendLayout()
        Me.EstadoStatusStrip.SuspendLayout()
        Me.MainFormMenuStrip.SuspendLayout()
        Me.ServicioTabPage.SuspendLayout()
        Me.ServicioGroupBox.SuspendLayout()
        Me.MainFormTabControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'NotificacionNotifyIcon
        '
        Me.NotificacionNotifyIcon.ContextMenuStrip = Me.MainFormContextMenuStrip
        Me.NotificacionNotifyIcon.Icon = CType(resources.GetObject("NotificacionNotifyIcon.Icon"), System.Drawing.Icon)
        Me.NotificacionNotifyIcon.Text = "SLYG FileProviderMonitor"
        Me.NotificacionNotifyIcon.Visible = True
        '
        'MainFormContextMenuStrip
        '
        Me.MainFormContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbrirToolStripMenuItem, Me.ToolStripSeparator1, Me.SalirContextualToolStripMenuItem})
        Me.MainFormContextMenuStrip.Name = "MainFormContextMenuStrip"
        Me.MainFormContextMenuStrip.Size = New System.Drawing.Size(173, 54)
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
        Me.EstadoStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EstadoToolStripStatusLabel})
        Me.EstadoStatusStrip.Location = New System.Drawing.Point(0, 325)
        Me.EstadoStatusStrip.Name = "EstadoStatusStrip"
        Me.EstadoStatusStrip.Size = New System.Drawing.Size(432, 22)
        Me.EstadoStatusStrip.TabIndex = 2
        Me.EstadoStatusStrip.Text = "StatusStrip1"
        '
        'EstadoToolStripStatusLabel
        '
        Me.EstadoToolStripStatusLabel.ForeColor = System.Drawing.Color.Brown
        Me.EstadoToolStripStatusLabel.Name = "EstadoToolStripStatusLabel"
        Me.EstadoToolStripStatusLabel.Size = New System.Drawing.Size(42, 17)
        Me.EstadoToolStripStatusLabel.Text = "Estado"
        '
        'TituloLabel
        '
        Me.TituloLabel.AutoSize = True
        Me.TituloLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TituloLabel.ForeColor = System.Drawing.Color.RoyalBlue
        Me.TituloLabel.Location = New System.Drawing.Point(204, 35)
        Me.TituloLabel.Name = "TituloLabel"
        Me.TituloLabel.Size = New System.Drawing.Size(189, 16)
        Me.TituloLabel.TabIndex = 11
        Me.TituloLabel.Text = "Configuración del Servicio"
        Me.TituloLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AppNameTextBox
        '
        Me.AppNameTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppNameTextBox.Location = New System.Drawing.Point(118, 21)
        Me.AppNameTextBox.Name = "AppNameTextBox"
        Me.AppNameTextBox.Size = New System.Drawing.Size(261, 22)
        Me.AppNameTextBox.TabIndex = 1
        Me.MensajeToolTip.SetToolTip(Me.AppNameTextBox, "Nombre que utiliza la aplicación para publicar las referencia")
        '
        'PuertoTextBox
        '
        Me.PuertoTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PuertoTextBox.Location = New System.Drawing.Point(119, 62)
        Me.PuertoTextBox.Name = "PuertoTextBox"
        Me.PuertoTextBox.Size = New System.Drawing.Size(82, 22)
        Me.PuertoTextBox.TabIndex = 5
        Me.MensajeToolTip.SetToolTip(Me.PuertoTextBox, "Puerto por el cual se realiza la conexión")
        '
        'ReiniciarButton
        '
        Me.ReiniciarButton.Image = Global.Miharu.FileProvider.Monitor.My.Resources.Resources.Reiniciar
        Me.ReiniciarButton.Location = New System.Drawing.Point(109, 22)
        Me.ReiniciarButton.Name = "ReiniciarButton"
        Me.ReiniciarButton.Size = New System.Drawing.Size(32, 32)
        Me.ReiniciarButton.TabIndex = 2
        Me.MensajeToolTip.SetToolTip(Me.ReiniciarButton, "Reiniciar el servicio")
        Me.ReiniciarButton.UseVisualStyleBackColor = True
        '
        'DetenerButton
        '
        Me.DetenerButton.Image = Global.Miharu.FileProvider.Monitor.My.Resources.Resources.Detener
        Me.DetenerButton.Location = New System.Drawing.Point(48, 22)
        Me.DetenerButton.Name = "DetenerButton"
        Me.DetenerButton.Size = New System.Drawing.Size(32, 32)
        Me.DetenerButton.TabIndex = 1
        Me.MensajeToolTip.SetToolTip(Me.DetenerButton, "Detener el servicio")
        Me.DetenerButton.UseVisualStyleBackColor = True
        '
        'IniciarButton
        '
        Me.IniciarButton.Image = Global.Miharu.FileProvider.Monitor.My.Resources.Resources.Iniciar
        Me.IniciarButton.Location = New System.Drawing.Point(10, 22)
        Me.IniciarButton.Name = "IniciarButton"
        Me.IniciarButton.Size = New System.Drawing.Size(32, 32)
        Me.IniciarButton.TabIndex = 0
        Me.MensajeToolTip.SetToolTip(Me.IniciarButton, "Iniciar el servicio")
        Me.IniciarButton.UseVisualStyleBackColor = True
        '
        'GuardarServicioButton
        '
        Me.GuardarServicioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GuardarServicioButton.Image = Global.Miharu.FileProvider.Monitor.My.Resources.Resources.save
        Me.GuardarServicioButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GuardarServicioButton.Location = New System.Drawing.Point(232, 215)
        Me.GuardarServicioButton.Name = "GuardarServicioButton"
        Me.GuardarServicioButton.Size = New System.Drawing.Size(147, 28)
        Me.GuardarServicioButton.TabIndex = 7
        Me.GuardarServicioButton.Text = "Almacenar cambios"
        Me.GuardarServicioButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.MensajeToolTip.SetToolTip(Me.GuardarServicioButton, "Almacenar la parametrización del servicio")
        Me.GuardarServicioButton.UseVisualStyleBackColor = True
        '
        'IntervaloTextBox
        '
        Me.IntervaloTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IntervaloTextBox.Location = New System.Drawing.Point(117, 178)
        Me.IntervaloTextBox.Name = "IntervaloTextBox"
        Me.IntervaloTextBox.Size = New System.Drawing.Size(82, 22)
        Me.IntervaloTextBox.TabIndex = 29
        Me.MensajeToolTip.SetToolTip(Me.IntervaloTextBox, "Puerto por el cual se realiza la conexión")
        '
        'HistoricoTextBox
        '
        Me.HistoricoTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HistoricoTextBox.Location = New System.Drawing.Point(323, 178)
        Me.HistoricoTextBox.Name = "HistoricoTextBox"
        Me.HistoricoTextBox.Size = New System.Drawing.Size(55, 22)
        Me.HistoricoTextBox.TabIndex = 31
        Me.MensajeToolTip.SetToolTip(Me.HistoricoTextBox, "Puerto por el cual se realiza la conexión")
        '
        'MainFormMenuStrip
        '
        Me.MainFormMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem, Me.AyudaToolStripMenuItem})
        Me.MainFormMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MainFormMenuStrip.Name = "MainFormMenuStrip"
        Me.MainFormMenuStrip.Size = New System.Drawing.Size(432, 24)
        Me.MainFormMenuStrip.TabIndex = 0
        Me.MainFormMenuStrip.Text = "MenuStrip1"
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
        Me.AcercaDeToolStripMenuItem.Size = New System.Drawing.Size(243, 22)
        Me.AcercaDeToolStripMenuItem.Text = "Acerca de FileProviderMonitor..."
        '
        'MonitorServicioTimer
        '
        Me.MonitorServicioTimer.Interval = 1000
        '
        'IconosImageList
        '
        Me.IconosImageList.ImageStream = CType(resources.GetObject("IconosImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.IconosImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.IconosImageList.Images.SetKeyName(0, "FileProviderIconActivo.ico")
        Me.IconosImageList.Images.SetKeyName(1, "FileProviderIconInctivo.ico")
        '
        'TabPageImageList
        '
        Me.TabPageImageList.ImageStream = CType(resources.GetObject("TabPageImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TabPageImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.TabPageImageList.Images.SetKeyName(0, "Config.png")
        Me.TabPageImageList.Images.SetKeyName(1, "folder_explore.png")
        '
        'ServicioTabPage
        '
        Me.ServicioTabPage.Controls.Add(Me.HistoricoTextBox)
        Me.ServicioTabPage.Controls.Add(Me.IntervaloTextBox)
        Me.ServicioTabPage.Controls.Add(Me.WorkingFolderTextBox)
        Me.ServicioTabPage.Controls.Add(Me.PuertoTextBox)
        Me.ServicioTabPage.Controls.Add(Me.AppNameTextBox)
        Me.ServicioTabPage.Controls.Add(Me.HistorioLabel)
        Me.ServicioTabPage.Controls.Add(Me.LabelIntervalo)
        Me.ServicioTabPage.Controls.Add(Me.CargarPathButton)
        Me.ServicioTabPage.Controls.Add(Me.WorkingFolderLabel)
        Me.ServicioTabPage.Controls.Add(Me.AppNameLabel)
        Me.ServicioTabPage.Controls.Add(Me.GuardarServicioButton)
        Me.ServicioTabPage.Controls.Add(Me.ServicioGroupBox)
        Me.ServicioTabPage.Controls.Add(Me.PuertoLabel)
        Me.ServicioTabPage.ImageIndex = 0
        Me.ServicioTabPage.Location = New System.Drawing.Point(4, 23)
        Me.ServicioTabPage.Name = "ServicioTabPage"
        Me.ServicioTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ServicioTabPage.Size = New System.Drawing.Size(400, 256)
        Me.ServicioTabPage.TabIndex = 0
        Me.ServicioTabPage.Text = "Servicio"
        Me.ServicioTabPage.UseVisualStyleBackColor = True
        '
        'WorkingFolderTextBox
        '
        Me.WorkingFolderTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.WorkingFolderTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WorkingFolderTextBox.Location = New System.Drawing.Point(18, 140)
        Me.WorkingFolderTextBox.Name = "WorkingFolderTextBox"
        Me.WorkingFolderTextBox.Size = New System.Drawing.Size(326, 22)
        Me.WorkingFolderTextBox.TabIndex = 26
        '
        'HistorioLabel
        '
        Me.HistorioLabel.AutoSize = True
        Me.HistorioLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HistorioLabel.ForeColor = System.Drawing.Color.Maroon
        Me.HistorioLabel.Location = New System.Drawing.Point(220, 181)
        Me.HistorioLabel.Name = "HistorioLabel"
        Me.HistorioLabel.Size = New System.Drawing.Size(96, 16)
        Me.HistorioLabel.TabIndex = 30
        Me.HistorioLabel.Text = "Histórico (hr)"
        '
        'LabelIntervalo
        '
        Me.LabelIntervalo.AutoSize = True
        Me.LabelIntervalo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelIntervalo.ForeColor = System.Drawing.Color.Maroon
        Me.LabelIntervalo.Location = New System.Drawing.Point(16, 181)
        Me.LabelIntervalo.Name = "LabelIntervalo"
        Me.LabelIntervalo.Size = New System.Drawing.Size(101, 16)
        Me.LabelIntervalo.TabIndex = 28
        Me.LabelIntervalo.Text = "Intervalo (ms)"
        '
        'CargarPathButton
        '
        Me.CargarPathButton.Image = Global.Miharu.FileProvider.Monitor.My.Resources.Resources.Directorios
        Me.CargarPathButton.Location = New System.Drawing.Point(351, 138)
        Me.CargarPathButton.Name = "CargarPathButton"
        Me.CargarPathButton.Size = New System.Drawing.Size(28, 24)
        Me.CargarPathButton.TabIndex = 27
        '
        'WorkingFolderLabel
        '
        Me.WorkingFolderLabel.AutoSize = True
        Me.WorkingFolderLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WorkingFolderLabel.ForeColor = System.Drawing.Color.Maroon
        Me.WorkingFolderLabel.Location = New System.Drawing.Point(16, 118)
        Me.WorkingFolderLabel.Name = "WorkingFolderLabel"
        Me.WorkingFolderLabel.Size = New System.Drawing.Size(150, 16)
        Me.WorkingFolderLabel.TabIndex = 25
        Me.WorkingFolderLabel.Text = "Directorio de trabajo"
        '
        'AppNameLabel
        '
        Me.AppNameLabel.AutoSize = True
        Me.AppNameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppNameLabel.ForeColor = System.Drawing.Color.Maroon
        Me.AppNameLabel.Location = New System.Drawing.Point(16, 21)
        Me.AppNameLabel.Name = "AppNameLabel"
        Me.AppNameLabel.Size = New System.Drawing.Size(76, 16)
        Me.AppNameLabel.TabIndex = 0
        Me.AppNameLabel.Text = "AppName"
        '
        'ServicioGroupBox
        '
        Me.ServicioGroupBox.Controls.Add(Me.IniciarButton)
        Me.ServicioGroupBox.Controls.Add(Me.DetenerButton)
        Me.ServicioGroupBox.Controls.Add(Me.ReiniciarButton)
        Me.ServicioGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ServicioGroupBox.ForeColor = System.Drawing.Color.Maroon
        Me.ServicioGroupBox.Location = New System.Drawing.Point(231, 49)
        Me.ServicioGroupBox.Name = "ServicioGroupBox"
        Me.ServicioGroupBox.Size = New System.Drawing.Size(147, 65)
        Me.ServicioGroupBox.TabIndex = 6
        Me.ServicioGroupBox.TabStop = False
        Me.ServicioGroupBox.Text = "Servicio"
        '
        'PuertoLabel
        '
        Me.PuertoLabel.AutoSize = True
        Me.PuertoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PuertoLabel.ForeColor = System.Drawing.Color.Maroon
        Me.PuertoLabel.Location = New System.Drawing.Point(16, 62)
        Me.PuertoLabel.Name = "PuertoLabel"
        Me.PuertoLabel.Size = New System.Drawing.Size(52, 16)
        Me.PuertoLabel.TabIndex = 4
        Me.PuertoLabel.Text = "Puerto"
        '
        'MainFormTabControl
        '
        Me.MainFormTabControl.Controls.Add(Me.ServicioTabPage)
        Me.MainFormTabControl.ImageList = Me.TabPageImageList
        Me.MainFormTabControl.Location = New System.Drawing.Point(12, 35)
        Me.MainFormTabControl.Name = "MainFormTabControl"
        Me.MainFormTabControl.SelectedIndex = 0
        Me.MainFormTabControl.Size = New System.Drawing.Size(408, 283)
        Me.MainFormTabControl.TabIndex = 1
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(432, 347)
        Me.Controls.Add(Me.TituloLabel)
        Me.Controls.Add(Me.MainFormTabControl)
        Me.Controls.Add(Me.EstadoStatusStrip)
        Me.Controls.Add(Me.MainFormMenuStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MainFormMenuStrip
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FileProviderDDOMonitor"
        Me.MainFormContextMenuStrip.ResumeLayout(False)
        Me.EstadoStatusStrip.ResumeLayout(False)
        Me.EstadoStatusStrip.PerformLayout()
        Me.MainFormMenuStrip.ResumeLayout(False)
        Me.MainFormMenuStrip.PerformLayout()
        Me.ServicioTabPage.ResumeLayout(False)
        Me.ServicioTabPage.PerformLayout()
        Me.ServicioGroupBox.ResumeLayout(False)
        Me.MainFormTabControl.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NotificacionNotifyIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents EstadoStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents EstadoToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TituloLabel As System.Windows.Forms.Label
    Friend WithEvents MensajeToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents MainFormMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents ArchivoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AyudaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AcercaDeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MonitorServicioTimer As System.Windows.Forms.Timer
    Friend WithEvents IconosImageList As System.Windows.Forms.ImageList
    Friend WithEvents MainFormContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AbrirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SalirContextualToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabPageImageList As System.Windows.Forms.ImageList
    Friend WithEvents ServicioTabPage As System.Windows.Forms.TabPage
    Friend WithEvents HistoricoTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IntervaloTextBox As System.Windows.Forms.TextBox
    Friend WithEvents WorkingFolderTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PuertoTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AppNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents HistorioLabel As System.Windows.Forms.Label
    Friend WithEvents LabelIntervalo As System.Windows.Forms.Label
    Friend WithEvents CargarPathButton As System.Windows.Forms.Button
    Friend WithEvents WorkingFolderLabel As System.Windows.Forms.Label
    Friend WithEvents AppNameLabel As System.Windows.Forms.Label
    Friend WithEvents GuardarServicioButton As System.Windows.Forms.Button
    Friend WithEvents ServicioGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents IniciarButton As System.Windows.Forms.Button
    Friend WithEvents DetenerButton As System.Windows.Forms.Button
    Friend WithEvents ReiniciarButton As System.Windows.Forms.Button
    Friend WithEvents PuertoLabel As System.Windows.Forms.Label
    Friend WithEvents MainFormTabControl As System.Windows.Forms.TabControl
End Class
