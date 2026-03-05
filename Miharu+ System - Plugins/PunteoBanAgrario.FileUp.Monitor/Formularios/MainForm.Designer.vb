Namespace Formularios
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class MainForm
        Inherits System.Windows.Forms.Form

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
            Me.btnProbar = New System.Windows.Forms.Button()
            Me.lblTitulo = New System.Windows.Forms.Label()
            Me.MainMenu = New System.Windows.Forms.MenuStrip()
            Me.mnuArchivo = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuSalir = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuAyuda = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuAcercaDe = New System.Windows.Forms.ToolStripMenuItem()
            Me.txtContraseña = New System.Windows.Forms.TextBox()
            Me.tpServicio = New System.Windows.Forms.TabPage()
            Me.lblContraseña = New System.Windows.Forms.Label()
            Me.txtUsuario = New System.Windows.Forms.TextBox()
            Me.lblUsuario = New System.Windows.Forms.Label()
            Me.txtServicioWeb = New System.Windows.Forms.TextBox()
            Me.lblServicioWeb = New System.Windows.Forms.Label()
            Me.lblMilisegundos = New System.Windows.Forms.Label()
            Me.txtIntervaloEscaneo = New System.Windows.Forms.TextBox()
            Me.lblIntervaloEscaneo = New System.Windows.Forms.Label()
            Me.tcBase = New System.Windows.Forms.TabControl()
            Me.tbCarga = New System.Windows.Forms.TabPage()
            Me.lblCargaLog = New System.Windows.Forms.Label()
            Me.txtCarpetaNoProcesado = New System.Windows.Forms.TextBox()
            Me.lblNoProcesados = New System.Windows.Forms.Label()
            Me.txtCarpetaProcesado = New System.Windows.Forms.TextBox()
            Me.lblProcesados = New System.Windows.Forms.Label()
            Me.txtCarpetaFileUp = New System.Windows.Forms.TextBox()
            Me.lblCarpetaCarga = New System.Windows.Forms.Label()
            Me.tbPDF = New System.Windows.Forms.TabPage()
            Me.lblGenerarPDF = New System.Windows.Forms.Label()
            Me.txtCarpetaPDF = New System.Windows.Forms.TextBox()
            Me.lblCarpetaPDF = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.dtHoraGeneracion = New System.Windows.Forms.DateTimePicker()
            Me.imlTab = New System.Windows.Forms.ImageList(Me.components)
            Me.tmrMonitorServicio = New System.Windows.Forms.Timer(Me.components)
            Me.btnGuardarServicio = New System.Windows.Forms.Button()
            Me.ToolTipText = New System.Windows.Forms.ToolTip(Me.components)
            Me.btnReiniciar = New System.Windows.Forms.Button()
            Me.btnIniciar = New System.Windows.Forms.Button()
            Me.btnDetener = New System.Windows.Forms.Button()
            Me.ssEstado = New System.Windows.Forms.StatusStrip()
            Me.tsslEstado = New System.Windows.Forms.ToolStripStatusLabel()
            Me.mnuiSalir2 = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuiS1 = New System.Windows.Forms.ToolStripSeparator()
            Me.IconoNotificacion = New System.Windows.Forms.NotifyIcon(Me.components)
            Me.cmsMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.mnuiAbrir = New System.Windows.Forms.ToolStripMenuItem()
            Me.imgIconoList = New System.Windows.Forms.ImageList(Me.components)
            Me.tbCifrado = New System.Windows.Forms.TabPage()
            Me.CifradoLabel = New System.Windows.Forms.Label()
            Me.txtNombreLlaveCifrado = New System.Windows.Forms.TextBox()
            Me.NombreLLaveLabel = New System.Windows.Forms.Label()
            Me.MainMenu.SuspendLayout()
            Me.tpServicio.SuspendLayout()
            Me.tcBase.SuspendLayout()
            Me.tbCarga.SuspendLayout()
            Me.tbPDF.SuspendLayout()
            Me.ssEstado.SuspendLayout()
            Me.cmsMenu.SuspendLayout()
            Me.tbCifrado.SuspendLayout()
            Me.SuspendLayout()
            '
            'btnProbar
            '
            Me.btnProbar.Location = New System.Drawing.Point(389, 143)
            Me.btnProbar.Name = "btnProbar"
            Me.btnProbar.Size = New System.Drawing.Size(75, 23)
            Me.btnProbar.TabIndex = 12
            Me.btnProbar.Text = "Probar"
            Me.ToolTipText.SetToolTip(Me.btnProbar, "Probar la configuración del servicio web")
            Me.btnProbar.UseVisualStyleBackColor = True
            '
            'lblTitulo
            '
            Me.lblTitulo.AutoSize = True
            Me.lblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTitulo.ForeColor = System.Drawing.Color.DarkBlue
            Me.lblTitulo.Location = New System.Drawing.Point(17, 14)
            Me.lblTitulo.Name = "lblTitulo"
            Me.lblTitulo.Size = New System.Drawing.Size(190, 16)
            Me.lblTitulo.TabIndex = 29
            Me.lblTitulo.Text = "Configuración del Servicio"
            Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'MainMenu
            '
            Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuArchivo, Me.mnuAyuda})
            Me.MainMenu.Location = New System.Drawing.Point(0, 0)
            Me.MainMenu.Name = "MainMenu"
            Me.MainMenu.Size = New System.Drawing.Size(517, 24)
            Me.MainMenu.TabIndex = 22
            Me.MainMenu.Text = "MenuStrip1"
            '
            'mnuArchivo
            '
            Me.mnuArchivo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSalir})
            Me.mnuArchivo.Name = "mnuArchivo"
            Me.mnuArchivo.Size = New System.Drawing.Size(55, 20)
            Me.mnuArchivo.Text = "&Archivo"
            '
            'mnuSalir
            '
            Me.mnuSalir.Name = "mnuSalir"
            Me.mnuSalir.Size = New System.Drawing.Size(105, 22)
            Me.mnuSalir.Text = "Salir"
            '
            'mnuAyuda
            '
            Me.mnuAyuda.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAcercaDe})
            Me.mnuAyuda.Name = "mnuAyuda"
            Me.mnuAyuda.Size = New System.Drawing.Size(50, 20)
            Me.mnuAyuda.Text = "A&yuda"
            '
            'mnuAcercaDe
            '
            Me.mnuAcercaDe.Name = "mnuAcercaDe"
            Me.mnuAcercaDe.Size = New System.Drawing.Size(283, 22)
            Me.mnuAcercaDe.Text = "Acerca de WorkflowNotificationMonitor..."
            '
            'txtContraseña
            '
            Me.txtContraseña.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtContraseña.ForeColor = System.Drawing.Color.RoyalBlue
            Me.txtContraseña.Location = New System.Drawing.Point(150, 143)
            Me.txtContraseña.Name = "txtContraseña"
            Me.txtContraseña.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.txtContraseña.Size = New System.Drawing.Size(191, 22)
            Me.txtContraseña.TabIndex = 11
            Me.ToolTipText.SetToolTip(Me.txtContraseña, "Contraseña del usuario de inicio de sesión")
            '
            'tpServicio
            '
            Me.tpServicio.Controls.Add(Me.btnProbar)
            Me.tpServicio.Controls.Add(Me.lblTitulo)
            Me.tpServicio.Controls.Add(Me.txtContraseña)
            Me.tpServicio.Controls.Add(Me.lblContraseña)
            Me.tpServicio.Controls.Add(Me.txtUsuario)
            Me.tpServicio.Controls.Add(Me.lblUsuario)
            Me.tpServicio.Controls.Add(Me.txtServicioWeb)
            Me.tpServicio.Controls.Add(Me.lblServicioWeb)
            Me.tpServicio.Controls.Add(Me.lblMilisegundos)
            Me.tpServicio.Controls.Add(Me.txtIntervaloEscaneo)
            Me.tpServicio.Controls.Add(Me.lblIntervaloEscaneo)
            Me.tpServicio.ImageIndex = 0
            Me.tpServicio.Location = New System.Drawing.Point(4, 23)
            Me.tpServicio.Name = "tpServicio"
            Me.tpServicio.Padding = New System.Windows.Forms.Padding(3)
            Me.tpServicio.Size = New System.Drawing.Size(483, 197)
            Me.tpServicio.TabIndex = 0
            Me.tpServicio.Text = "Servicio"
            Me.tpServicio.UseVisualStyleBackColor = True
            '
            'lblContraseña
            '
            Me.lblContraseña.AutoSize = True
            Me.lblContraseña.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblContraseña.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblContraseña.Location = New System.Drawing.Point(16, 143)
            Me.lblContraseña.Name = "lblContraseña"
            Me.lblContraseña.Size = New System.Drawing.Size(87, 16)
            Me.lblContraseña.TabIndex = 10
            Me.lblContraseña.Text = "Contraseña"
            '
            'txtUsuario
            '
            Me.txtUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtUsuario.ForeColor = System.Drawing.Color.MidnightBlue
            Me.txtUsuario.Location = New System.Drawing.Point(150, 115)
            Me.txtUsuario.Name = "txtUsuario"
            Me.txtUsuario.Size = New System.Drawing.Size(191, 22)
            Me.txtUsuario.TabIndex = 9
            Me.ToolTipText.SetToolTip(Me.txtUsuario, "Usuario de inicio de sesión")
            '
            'lblUsuario
            '
            Me.lblUsuario.AutoSize = True
            Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblUsuario.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblUsuario.Location = New System.Drawing.Point(17, 113)
            Me.lblUsuario.Name = "lblUsuario"
            Me.lblUsuario.Size = New System.Drawing.Size(62, 16)
            Me.lblUsuario.TabIndex = 8
            Me.lblUsuario.Text = "Usuario"
            '
            'txtServicioWeb
            '
            Me.txtServicioWeb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtServicioWeb.ForeColor = System.Drawing.Color.RoyalBlue
            Me.txtServicioWeb.Location = New System.Drawing.Point(150, 87)
            Me.txtServicioWeb.Name = "txtServicioWeb"
            Me.txtServicioWeb.Size = New System.Drawing.Size(314, 22)
            Me.txtServicioWeb.TabIndex = 7
            Me.ToolTipText.SetToolTip(Me.txtServicioWeb, "Servicio web de validación de usuarios")
            '
            'lblServicioWeb
            '
            Me.lblServicioWeb.AutoSize = True
            Me.lblServicioWeb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblServicioWeb.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblServicioWeb.Location = New System.Drawing.Point(16, 82)
            Me.lblServicioWeb.Name = "lblServicioWeb"
            Me.lblServicioWeb.Size = New System.Drawing.Size(97, 16)
            Me.lblServicioWeb.TabIndex = 6
            Me.lblServicioWeb.Text = "Servicio web"
            '
            'lblMilisegundos
            '
            Me.lblMilisegundos.AutoSize = True
            Me.lblMilisegundos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblMilisegundos.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblMilisegundos.Location = New System.Drawing.Point(288, 61)
            Me.lblMilisegundos.Name = "lblMilisegundos"
            Me.lblMilisegundos.Size = New System.Drawing.Size(28, 16)
            Me.lblMilisegundos.TabIndex = 2
            Me.lblMilisegundos.Text = "ms"
            '
            'txtIntervaloEscaneo
            '
            Me.txtIntervaloEscaneo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtIntervaloEscaneo.ForeColor = System.Drawing.Color.RoyalBlue
            Me.txtIntervaloEscaneo.Location = New System.Drawing.Point(182, 55)
            Me.txtIntervaloEscaneo.Name = "txtIntervaloEscaneo"
            Me.txtIntervaloEscaneo.Size = New System.Drawing.Size(100, 22)
            Me.txtIntervaloEscaneo.TabIndex = 1
            Me.ToolTipText.SetToolTip(Me.txtIntervaloEscaneo, "Define el intervalo de tiempo en milisegundos a esperar antes de validar si hay n" & _
                                                              "uevas notificaciones para enviar")
            '
            'lblIntervaloEscaneo
            '
            Me.lblIntervaloEscaneo.AutoSize = True
            Me.lblIntervaloEscaneo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblIntervaloEscaneo.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblIntervaloEscaneo.Location = New System.Drawing.Point(16, 55)
            Me.lblIntervaloEscaneo.Name = "lblIntervaloEscaneo"
            Me.lblIntervaloEscaneo.Size = New System.Drawing.Size(132, 16)
            Me.lblIntervaloEscaneo.TabIndex = 0
            Me.lblIntervaloEscaneo.Text = "Intervalo escaneo"
            '
            'tcBase
            '
            Me.tcBase.Controls.Add(Me.tpServicio)
            Me.tcBase.Controls.Add(Me.tbCarga)
            Me.tcBase.Controls.Add(Me.tbPDF)
            Me.tcBase.Controls.Add(Me.tbCifrado)
            Me.tcBase.ImageList = Me.imlTab
            Me.tcBase.Location = New System.Drawing.Point(12, 35)
            Me.tcBase.Name = "tcBase"
            Me.tcBase.SelectedIndex = 0
            Me.tcBase.Size = New System.Drawing.Size(491, 224)
            Me.tcBase.TabIndex = 24
            '
            'tbCarga
            '
            Me.tbCarga.Controls.Add(Me.lblCargaLog)
            Me.tbCarga.Controls.Add(Me.txtCarpetaNoProcesado)
            Me.tbCarga.Controls.Add(Me.lblNoProcesados)
            Me.tbCarga.Controls.Add(Me.txtCarpetaProcesado)
            Me.tbCarga.Controls.Add(Me.lblProcesados)
            Me.tbCarga.Controls.Add(Me.txtCarpetaFileUp)
            Me.tbCarga.Controls.Add(Me.lblCarpetaCarga)
            Me.tbCarga.ImageIndex = 2
            Me.tbCarga.Location = New System.Drawing.Point(4, 23)
            Me.tbCarga.Name = "tbCarga"
            Me.tbCarga.Size = New System.Drawing.Size(483, 197)
            Me.tbCarga.TabIndex = 1
            Me.tbCarga.Text = "Carga Log"
            Me.tbCarga.UseVisualStyleBackColor = True
            '
            'lblCargaLog
            '
            Me.lblCargaLog.AutoSize = True
            Me.lblCargaLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCargaLog.ForeColor = System.Drawing.Color.DarkBlue
            Me.lblCargaLog.Location = New System.Drawing.Point(17, 15)
            Me.lblCargaLog.Name = "lblCargaLog"
            Me.lblCargaLog.Size = New System.Drawing.Size(396, 16)
            Me.lblCargaLog.TabIndex = 30
            Me.lblCargaLog.Text = "Configuración de Carpetas asociadas al Cargue del Log"
            Me.lblCargaLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'txtCarpetaNoProcesado
            '
            Me.txtCarpetaNoProcesado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtCarpetaNoProcesado.ForeColor = System.Drawing.Color.RoyalBlue
            Me.txtCarpetaNoProcesado.Location = New System.Drawing.Point(151, 121)
            Me.txtCarpetaNoProcesado.Name = "txtCarpetaNoProcesado"
            Me.txtCarpetaNoProcesado.Size = New System.Drawing.Size(314, 22)
            Me.txtCarpetaNoProcesado.TabIndex = 24
            Me.ToolTipText.SetToolTip(Me.txtCarpetaNoProcesado, "Servicio web de validación de usuarios")
            '
            'lblNoProcesados
            '
            Me.lblNoProcesados.AutoSize = True
            Me.lblNoProcesados.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblNoProcesados.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblNoProcesados.Location = New System.Drawing.Point(17, 121)
            Me.lblNoProcesados.Name = "lblNoProcesados"
            Me.lblNoProcesados.Size = New System.Drawing.Size(134, 16)
            Me.lblNoProcesados.TabIndex = 23
            Me.lblNoProcesados.Text = "C. No Procesados"
            Me.ToolTipText.SetToolTip(Me.lblNoProcesados, "Ubicación de los archivos que no son cargados")
            '
            'txtCarpetaProcesado
            '
            Me.txtCarpetaProcesado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtCarpetaProcesado.ForeColor = System.Drawing.Color.RoyalBlue
            Me.txtCarpetaProcesado.Location = New System.Drawing.Point(151, 92)
            Me.txtCarpetaProcesado.Name = "txtCarpetaProcesado"
            Me.txtCarpetaProcesado.Size = New System.Drawing.Size(314, 22)
            Me.txtCarpetaProcesado.TabIndex = 22
            Me.ToolTipText.SetToolTip(Me.txtCarpetaProcesado, "Servicio web de validación de usuarios")
            '
            'lblProcesados
            '
            Me.lblProcesados.AutoSize = True
            Me.lblProcesados.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProcesados.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblProcesados.Location = New System.Drawing.Point(17, 92)
            Me.lblProcesados.Name = "lblProcesados"
            Me.lblProcesados.Size = New System.Drawing.Size(110, 16)
            Me.lblProcesados.TabIndex = 21
            Me.lblProcesados.Text = "C. Procesados"
            Me.ToolTipText.SetToolTip(Me.lblProcesados, "Ubicación de los archivos que son cargados correctamente")
            '
            'txtCarpetaFileUp
            '
            Me.txtCarpetaFileUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtCarpetaFileUp.ForeColor = System.Drawing.Color.RoyalBlue
            Me.txtCarpetaFileUp.Location = New System.Drawing.Point(151, 60)
            Me.txtCarpetaFileUp.Name = "txtCarpetaFileUp"
            Me.txtCarpetaFileUp.Size = New System.Drawing.Size(314, 22)
            Me.txtCarpetaFileUp.TabIndex = 20
            Me.ToolTipText.SetToolTip(Me.txtCarpetaFileUp, "Servicio web de validación de usuarios")
            '
            'lblCarpetaCarga
            '
            Me.lblCarpetaCarga.AutoSize = True
            Me.lblCarpetaCarga.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCarpetaCarga.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblCarpetaCarga.Location = New System.Drawing.Point(17, 59)
            Me.lblCarpetaCarga.Name = "lblCarpetaCarga"
            Me.lblCarpetaCarga.Size = New System.Drawing.Size(72, 16)
            Me.lblCarpetaCarga.TabIndex = 19
            Me.lblCarpetaCarga.Text = "C. Origen"
            Me.ToolTipText.SetToolTip(Me.lblCarpetaCarga, "Carpeta donde se encuentran los archivos a ser cargados")
            '
            'tbPDF
            '
            Me.tbPDF.Controls.Add(Me.lblGenerarPDF)
            Me.tbPDF.Controls.Add(Me.txtCarpetaPDF)
            Me.tbPDF.Controls.Add(Me.lblCarpetaPDF)
            Me.tbPDF.Controls.Add(Me.Label1)
            Me.tbPDF.Controls.Add(Me.dtHoraGeneracion)
            Me.tbPDF.ImageIndex = 1
            Me.tbPDF.Location = New System.Drawing.Point(4, 23)
            Me.tbPDF.Name = "tbPDF"
            Me.tbPDF.Size = New System.Drawing.Size(483, 197)
            Me.tbPDF.TabIndex = 2
            Me.tbPDF.Text = "Generar PDF"
            Me.tbPDF.UseVisualStyleBackColor = True
            '
            'lblGenerarPDF
            '
            Me.lblGenerarPDF.AutoSize = True
            Me.lblGenerarPDF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblGenerarPDF.ForeColor = System.Drawing.Color.DarkBlue
            Me.lblGenerarPDF.Location = New System.Drawing.Point(17, 18)
            Me.lblGenerarPDF.Name = "lblGenerarPDF"
            Me.lblGenerarPDF.Size = New System.Drawing.Size(296, 16)
            Me.lblGenerarPDF.TabIndex = 31
            Me.lblGenerarPDF.Text = "Configuración para la Generación de PDF"
            Me.lblGenerarPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'txtCarpetaPDF
            '
            Me.txtCarpetaPDF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtCarpetaPDF.ForeColor = System.Drawing.Color.RoyalBlue
            Me.txtCarpetaPDF.Location = New System.Drawing.Point(161, 68)
            Me.txtCarpetaPDF.Name = "txtCarpetaPDF"
            Me.txtCarpetaPDF.Size = New System.Drawing.Size(314, 22)
            Me.txtCarpetaPDF.TabIndex = 24
            Me.ToolTipText.SetToolTip(Me.txtCarpetaPDF, "Servicio web de validación de usuarios")
            '
            'lblCarpetaPDF
            '
            Me.lblCarpetaPDF.AutoSize = True
            Me.lblCarpetaPDF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCarpetaPDF.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblCarpetaPDF.Location = New System.Drawing.Point(17, 69)
            Me.lblCarpetaPDF.Name = "lblCarpetaPDF"
            Me.lblCarpetaPDF.Size = New System.Drawing.Size(140, 16)
            Me.lblCarpetaPDF.TabIndex = 23
            Me.lblCarpetaPDF.Text = "C. Generación PDF"
            Me.ToolTipText.SetToolTip(Me.lblCarpetaPDF, "Ubicación donde serán generados los PDF")
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
            Me.Label1.Location = New System.Drawing.Point(17, 99)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(126, 16)
            Me.Label1.TabIndex = 20
            Me.Label1.Text = "Hora Generación"
            Me.ToolTipText.SetToolTip(Me.Label1, "Carpeta donde se encuentran los archivos a ser cargados")
            '
            'dtHoraGeneracion
            '
            Me.dtHoraGeneracion.CustomFormat = "mm = minutos; tt = am o pm"
            Me.dtHoraGeneracion.Format = System.Windows.Forms.DateTimePickerFormat.Time
            Me.dtHoraGeneracion.Location = New System.Drawing.Point(160, 99)
            Me.dtHoraGeneracion.Name = "dtHoraGeneracion"
            Me.dtHoraGeneracion.ShowUpDown = True
            Me.dtHoraGeneracion.Size = New System.Drawing.Size(97, 20)
            Me.dtHoraGeneracion.TabIndex = 0
            '
            'imlTab
            '
            Me.imlTab.ImageStream = CType(resources.GetObject("imlTab.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imlTab.TransparentColor = System.Drawing.Color.Transparent
            Me.imlTab.Images.SetKeyName(0, "Config.png")
            Me.imlTab.Images.SetKeyName(1, "email_edit.png")
            Me.imlTab.Images.SetKeyName(2, "database_edit.png")
            Me.imlTab.Images.SetKeyName(3, "cryptography.png")
            '
            'tmrMonitorServicio
            '
            Me.tmrMonitorServicio.Interval = 1000
            '
            'btnGuardarServicio
            '
            Me.btnGuardarServicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnGuardarServicio.ForeColor = System.Drawing.SystemColors.WindowText
            Me.btnGuardarServicio.Image = CType(resources.GetObject("btnGuardarServicio.Image"), System.Drawing.Image)
            Me.btnGuardarServicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnGuardarServicio.Location = New System.Drawing.Point(354, 265)
            Me.btnGuardarServicio.Name = "btnGuardarServicio"
            Me.btnGuardarServicio.Size = New System.Drawing.Size(147, 28)
            Me.btnGuardarServicio.TabIndex = 28
            Me.btnGuardarServicio.Text = "Almacenar cambios"
            Me.btnGuardarServicio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ToolTipText.SetToolTip(Me.btnGuardarServicio, "Almacenar la parametrización del servicio")
            Me.btnGuardarServicio.UseVisualStyleBackColor = True
            '
            'btnReiniciar
            '
            Me.btnReiniciar.Image = CType(resources.GetObject("btnReiniciar.Image"), System.Drawing.Image)
            Me.btnReiniciar.Location = New System.Drawing.Point(132, 265)
            Me.btnReiniciar.Name = "btnReiniciar"
            Me.btnReiniciar.Size = New System.Drawing.Size(32, 32)
            Me.btnReiniciar.TabIndex = 27
            Me.ToolTipText.SetToolTip(Me.btnReiniciar, "Reiniciar el servicio")
            Me.btnReiniciar.UseVisualStyleBackColor = True
            '
            'btnIniciar
            '
            Me.btnIniciar.Image = CType(resources.GetObject("btnIniciar.Image"), System.Drawing.Image)
            Me.btnIniciar.Location = New System.Drawing.Point(33, 265)
            Me.btnIniciar.Name = "btnIniciar"
            Me.btnIniciar.Size = New System.Drawing.Size(32, 32)
            Me.btnIniciar.TabIndex = 23
            Me.ToolTipText.SetToolTip(Me.btnIniciar, "Iniciar el servicio")
            Me.btnIniciar.UseVisualStyleBackColor = True
            '
            'btnDetener
            '
            Me.btnDetener.Image = CType(resources.GetObject("btnDetener.Image"), System.Drawing.Image)
            Me.btnDetener.Location = New System.Drawing.Point(71, 265)
            Me.btnDetener.Name = "btnDetener"
            Me.btnDetener.Size = New System.Drawing.Size(32, 32)
            Me.btnDetener.TabIndex = 25
            Me.ToolTipText.SetToolTip(Me.btnDetener, "Detener el servicio")
            Me.btnDetener.UseVisualStyleBackColor = True
            '
            'ssEstado
            '
            Me.ssEstado.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslEstado})
            Me.ssEstado.Location = New System.Drawing.Point(0, 307)
            Me.ssEstado.Name = "ssEstado"
            Me.ssEstado.Size = New System.Drawing.Size(517, 22)
            Me.ssEstado.TabIndex = 26
            Me.ssEstado.Text = "StatusStrip1"
            '
            'tsslEstado
            '
            Me.tsslEstado.ForeColor = System.Drawing.SystemColors.WindowText
            Me.tsslEstado.Name = "tsslEstado"
            Me.tsslEstado.Size = New System.Drawing.Size(40, 17)
            Me.tsslEstado.Text = "Estado"
            '
            'mnuiSalir2
            '
            Me.mnuiSalir2.Name = "mnuiSalir2"
            Me.mnuiSalir2.Size = New System.Drawing.Size(172, 22)
            Me.mnuiSalir2.Text = "Salir"
            '
            'mnuiS1
            '
            Me.mnuiS1.Name = "mnuiS1"
            Me.mnuiS1.Size = New System.Drawing.Size(169, 6)
            '
            'IconoNotificacion
            '
            Me.IconoNotificacion.ContextMenuStrip = Me.cmsMenu
            Me.IconoNotificacion.Icon = CType(resources.GetObject("IconoNotificacion.Icon"), System.Drawing.Icon)
            Me.IconoNotificacion.Text = "BanAgrario File Up"
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
            'imgIconoList
            '
            Me.imgIconoList.ImageStream = CType(resources.GetObject("imgIconoList.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imgIconoList.TransparentColor = System.Drawing.Color.Transparent
            Me.imgIconoList.Images.SetKeyName(0, "FileUpActivoIcon.ico")
            Me.imgIconoList.Images.SetKeyName(1, "FileUpInactivoIcon.ico")
            '
            'tbCifrado
            '
            Me.tbCifrado.Controls.Add(Me.txtNombreLlaveCifrado)
            Me.tbCifrado.Controls.Add(Me.NombreLLaveLabel)
            Me.tbCifrado.Controls.Add(Me.CifradoLabel)
            Me.tbCifrado.ImageIndex = 3
            Me.tbCifrado.Location = New System.Drawing.Point(4, 23)
            Me.tbCifrado.Name = "tbCifrado"
            Me.tbCifrado.Padding = New System.Windows.Forms.Padding(3)
            Me.tbCifrado.Size = New System.Drawing.Size(483, 197)
            Me.tbCifrado.TabIndex = 3
            Me.tbCifrado.Text = "Cifrado"
            Me.tbCifrado.UseVisualStyleBackColor = True
            '
            'CifradoLabel
            '
            Me.CifradoLabel.AutoSize = True
            Me.CifradoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CifradoLabel.ForeColor = System.Drawing.Color.DarkBlue
            Me.CifradoLabel.Location = New System.Drawing.Point(14, 12)
            Me.CifradoLabel.Name = "CifradoLabel"
            Me.CifradoLabel.Size = New System.Drawing.Size(179, 16)
            Me.CifradoLabel.TabIndex = 32
            Me.CifradoLabel.Text = "Configuración de Cifrado"
            Me.CifradoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'txtNombreLlaveCifrado
            '
            Me.txtNombreLlaveCifrado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtNombreLlaveCifrado.ForeColor = System.Drawing.Color.RoyalBlue
            Me.txtNombreLlaveCifrado.Location = New System.Drawing.Point(213, 47)
            Me.txtNombreLlaveCifrado.Name = "txtNombreLlaveCifrado"
            Me.txtNombreLlaveCifrado.Size = New System.Drawing.Size(259, 22)
            Me.txtNombreLlaveCifrado.TabIndex = 34
            Me.ToolTipText.SetToolTip(Me.txtNombreLlaveCifrado, "Servicio web de validación de usuarios")
            '
            'NombreLLaveLabel
            '
            Me.NombreLLaveLabel.AutoSize = True
            Me.NombreLLaveLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NombreLLaveLabel.ForeColor = System.Drawing.SystemColors.WindowText
            Me.NombreLLaveLabel.Location = New System.Drawing.Point(14, 48)
            Me.NombreLLaveLabel.Name = "NombreLLaveLabel"
            Me.NombreLLaveLabel.Size = New System.Drawing.Size(179, 16)
            Me.NombreLLaveLabel.TabIndex = 33
            Me.NombreLLaveLabel.Text = "Nombre llave  de cifrado"
            Me.ToolTipText.SetToolTip(Me.NombreLLaveLabel, "Nombre de la llave importada para decifrar los arhivos.")
            '
            'MainForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(517, 329)
            Me.Controls.Add(Me.MainMenu)
            Me.Controls.Add(Me.tcBase)
            Me.Controls.Add(Me.btnReiniciar)
            Me.Controls.Add(Me.ssEstado)
            Me.Controls.Add(Me.btnGuardarServicio)
            Me.Controls.Add(Me.btnIniciar)
            Me.Controls.Add(Me.btnDetener)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "MainForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "CargaLogMonitor"
            Me.MainMenu.ResumeLayout(False)
            Me.MainMenu.PerformLayout()
            Me.tpServicio.ResumeLayout(False)
            Me.tpServicio.PerformLayout()
            Me.tcBase.ResumeLayout(False)
            Me.tbCarga.ResumeLayout(False)
            Me.tbCarga.PerformLayout()
            Me.tbPDF.ResumeLayout(False)
            Me.tbPDF.PerformLayout()
            Me.ssEstado.ResumeLayout(False)
            Me.ssEstado.PerformLayout()
            Me.cmsMenu.ResumeLayout(False)
            Me.tbCifrado.ResumeLayout(False)
            Me.tbCifrado.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents btnProbar As System.Windows.Forms.Button
        Friend WithEvents ToolTipText As System.Windows.Forms.ToolTip
        Friend WithEvents lblTitulo As System.Windows.Forms.Label
        Friend WithEvents MainMenu As System.Windows.Forms.MenuStrip
        Friend WithEvents mnuArchivo As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuSalir As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuAyuda As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuAcercaDe As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents txtContraseña As System.Windows.Forms.TextBox
        Friend WithEvents tpServicio As System.Windows.Forms.TabPage
        Friend WithEvents lblContraseña As System.Windows.Forms.Label
        Friend WithEvents txtUsuario As System.Windows.Forms.TextBox
        Friend WithEvents lblUsuario As System.Windows.Forms.Label
        Friend WithEvents txtServicioWeb As System.Windows.Forms.TextBox
        Friend WithEvents lblServicioWeb As System.Windows.Forms.Label
        Friend WithEvents lblMilisegundos As System.Windows.Forms.Label
        Friend WithEvents txtIntervaloEscaneo As System.Windows.Forms.TextBox
        Friend WithEvents lblIntervaloEscaneo As System.Windows.Forms.Label
        Friend WithEvents tcBase As System.Windows.Forms.TabControl
        Friend WithEvents imlTab As System.Windows.Forms.ImageList
        Friend WithEvents tmrMonitorServicio As System.Windows.Forms.Timer
        Friend WithEvents btnGuardarServicio As System.Windows.Forms.Button
        Friend WithEvents btnReiniciar As System.Windows.Forms.Button
        Friend WithEvents btnIniciar As System.Windows.Forms.Button
        Friend WithEvents btnDetener As System.Windows.Forms.Button
        Friend WithEvents ssEstado As System.Windows.Forms.StatusStrip
        Friend WithEvents tsslEstado As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents mnuiSalir2 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuiS1 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents IconoNotificacion As System.Windows.Forms.NotifyIcon
        Friend WithEvents cmsMenu As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents mnuiAbrir As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents imgIconoList As System.Windows.Forms.ImageList
        Friend WithEvents tbCarga As System.Windows.Forms.TabPage
        Friend WithEvents tbPDF As System.Windows.Forms.TabPage
        Friend WithEvents dtHoraGeneracion As System.Windows.Forms.DateTimePicker
        Friend WithEvents txtCarpetaNoProcesado As System.Windows.Forms.TextBox
        Friend WithEvents lblNoProcesados As System.Windows.Forms.Label
        Friend WithEvents txtCarpetaProcesado As System.Windows.Forms.TextBox
        Friend WithEvents lblProcesados As System.Windows.Forms.Label
        Friend WithEvents txtCarpetaFileUp As System.Windows.Forms.TextBox
        Friend WithEvents lblCarpetaCarga As System.Windows.Forms.Label
        Friend WithEvents txtCarpetaPDF As System.Windows.Forms.TextBox
        Friend WithEvents lblCarpetaPDF As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents lblCargaLog As System.Windows.Forms.Label
        Friend WithEvents lblGenerarPDF As System.Windows.Forms.Label
        Friend WithEvents tbCifrado As System.Windows.Forms.TabPage
        Friend WithEvents txtNombreLlaveCifrado As System.Windows.Forms.TextBox
        Friend WithEvents NombreLLaveLabel As System.Windows.Forms.Label
        Friend WithEvents CifradoLabel As System.Windows.Forms.Label

    End Class
End Namespace