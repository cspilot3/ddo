Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Solicitudes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormAdministracionSolicitudes
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAdministracionSolicitudes))
            Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.FiltrosGroupBox = New System.Windows.Forms.GroupBox()
            Me.EstadoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.EstadoLabel = New System.Windows.Forms.Label()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.IdSolicitanteDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.lblSolicitante = New System.Windows.Forms.Label()
            Me.PrioridadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblPrioridad = New System.Windows.Forms.Label()
            Me.ProyectoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblProyecto = New System.Windows.Forms.Label()
            Me.lblEntidad = New System.Windows.Forms.Label()
            Me.EntidadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.CargandoPictureBox = New System.Windows.Forms.PictureBox()
            Me.ReversarButton = New System.Windows.Forms.Button()
            Me.PrintButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.PauseButton = New System.Windows.Forms.Button()
            Me.PlayButton = New System.Windows.Forms.Button()
            Me.SolicitudesTreeView = New System.Windows.Forms.TreeView()
            Me.IconosTreeList = New System.Windows.Forms.ImageList(Me.components)
            Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.DesktopTextBoxControlSolicitud = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.lblSolicitud = New System.Windows.Forms.Label()
            Me.SolicitudesBackgroundWorker = New System.ComponentModel.BackgroundWorker()
            Me.btnDetenerBusqueda = New System.Windows.Forms.Button()
            Me.FiltrosGroupBox.SuspendLayout()
            Me.Panel1.SuspendLayout()
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'FiltrosGroupBox
            '
            Me.FiltrosGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FiltrosGroupBox.Controls.Add(Me.EstadoDesktopComboBox)
            Me.FiltrosGroupBox.Controls.Add(Me.EstadoLabel)
            Me.FiltrosGroupBox.Controls.Add(Me.BuscarButton)
            Me.FiltrosGroupBox.Controls.Add(Me.IdSolicitanteDesktopTextBox)
            Me.FiltrosGroupBox.Controls.Add(Me.lblSolicitante)
            Me.FiltrosGroupBox.Controls.Add(Me.PrioridadDesktopComboBox)
            Me.FiltrosGroupBox.Controls.Add(Me.lblPrioridad)
            Me.FiltrosGroupBox.Controls.Add(Me.ProyectoDesktopComboBox)
            Me.FiltrosGroupBox.Controls.Add(Me.lblProyecto)
            Me.FiltrosGroupBox.Controls.Add(Me.lblEntidad)
            Me.FiltrosGroupBox.Controls.Add(Me.EntidadDesktopComboBox)
            Me.FiltrosGroupBox.Location = New System.Drawing.Point(6, 1)
            Me.FiltrosGroupBox.Name = "FiltrosGroupBox"
            Me.FiltrosGroupBox.Size = New System.Drawing.Size(512, 111)
            Me.FiltrosGroupBox.TabIndex = 0
            Me.FiltrosGroupBox.TabStop = False
            Me.FiltrosGroupBox.Text = "Filtro de Búsqueda"
            '
            'EstadoDesktopComboBox
            '
            Me.EstadoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EstadoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EstadoDesktopComboBox.DisabledEnter = False
            Me.EstadoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EstadoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EstadoDesktopComboBox.FormattingEnabled = True
            Me.EstadoDesktopComboBox.Location = New System.Drawing.Point(83, 84)
            Me.EstadoDesktopComboBox.Name = "EstadoDesktopComboBox"
            Me.EstadoDesktopComboBox.Size = New System.Drawing.Size(121, 21)
            Me.EstadoDesktopComboBox.TabIndex = 10
            Me.EstadoDesktopComboBox.Visible = False
            '
            'EstadoLabel
            '
            Me.EstadoLabel.AutoSize = True
            Me.EstadoLabel.Location = New System.Drawing.Point(6, 85)
            Me.EstadoLabel.Name = "EstadoLabel"
            Me.EstadoLabel.Size = New System.Drawing.Size(48, 13)
            Me.EstadoLabel.TabIndex = 9
            Me.EstadoLabel.Text = "Estado:"
            Me.EstadoLabel.Visible = False
            '
            'BuscarButton
            '
            Me.BuscarButton.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(429, 80)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(77, 23)
            Me.BuscarButton.TabIndex = 8
            Me.BuscarButton.Text = "&Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'IdSolicitanteDesktopTextBox
            '
            Me.IdSolicitanteDesktopTextBox._Obligatorio = False
            Me.IdSolicitanteDesktopTextBox._PermitePegar = False
            Me.IdSolicitanteDesktopTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.IdSolicitanteDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.IdSolicitanteDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.IdSolicitanteDesktopTextBox.DateFormat = Nothing
            Me.IdSolicitanteDesktopTextBox.DisabledEnter = False
            Me.IdSolicitanteDesktopTextBox.DisabledTab = False
            Me.IdSolicitanteDesktopTextBox.EnabledShortCuts = False
            Me.IdSolicitanteDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.IdSolicitanteDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.IdSolicitanteDesktopTextBox.Location = New System.Drawing.Point(373, 53)
            Me.IdSolicitanteDesktopTextBox.MaskedTextBox_Property = ""
            Me.IdSolicitanteDesktopTextBox.MaximumLength = CType(0, Short)
            Me.IdSolicitanteDesktopTextBox.MinimumLength = CType(0, Short)
            Me.IdSolicitanteDesktopTextBox.Name = "IdSolicitanteDesktopTextBox"
            Me.IdSolicitanteDesktopTextBox.Obligatorio = False
            Me.IdSolicitanteDesktopTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.IdSolicitanteDesktopTextBox.Rango = Rango1
            Me.IdSolicitanteDesktopTextBox.Size = New System.Drawing.Size(133, 21)
            Me.IdSolicitanteDesktopTextBox.TabIndex = 7
            Me.IdSolicitanteDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Numerico
            Me.IdSolicitanteDesktopTextBox.Usa_Decimales = False
            Me.IdSolicitanteDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'lblSolicitante
            '
            Me.lblSolicitante.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblSolicitante.AutoSize = True
            Me.lblSolicitante.Location = New System.Drawing.Point(295, 56)
            Me.lblSolicitante.Name = "lblSolicitante"
            Me.lblSolicitante.Size = New System.Drawing.Size(70, 13)
            Me.lblSolicitante.TabIndex = 6
            Me.lblSolicitante.Text = "Solicitante:"
            '
            'PrioridadDesktopComboBox
            '
            Me.PrioridadDesktopComboBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrioridadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.PrioridadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.PrioridadDesktopComboBox.DisabledEnter = False
            Me.PrioridadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.PrioridadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.PrioridadDesktopComboBox.FormattingEnabled = True
            Me.PrioridadDesktopComboBox.Location = New System.Drawing.Point(373, 20)
            Me.PrioridadDesktopComboBox.Name = "PrioridadDesktopComboBox"
            Me.PrioridadDesktopComboBox.Size = New System.Drawing.Size(133, 21)
            Me.PrioridadDesktopComboBox.TabIndex = 5
            '
            'lblPrioridad
            '
            Me.lblPrioridad.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblPrioridad.AutoSize = True
            Me.lblPrioridad.Location = New System.Drawing.Point(295, 23)
            Me.lblPrioridad.Name = "lblPrioridad"
            Me.lblPrioridad.Size = New System.Drawing.Size(61, 13)
            Me.lblPrioridad.TabIndex = 4
            Me.lblPrioridad.Text = "Prioridad:"
            '
            'ProyectoDesktopComboBox
            '
            Me.ProyectoDesktopComboBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ProyectoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProyectoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProyectoDesktopComboBox.DisabledEnter = False
            Me.ProyectoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProyectoDesktopComboBox.FormattingEnabled = True
            Me.ProyectoDesktopComboBox.Location = New System.Drawing.Point(83, 53)
            Me.ProyectoDesktopComboBox.Name = "ProyectoDesktopComboBox"
            Me.ProyectoDesktopComboBox.Size = New System.Drawing.Size(185, 21)
            Me.ProyectoDesktopComboBox.TabIndex = 3
            '
            'lblProyecto
            '
            Me.lblProyecto.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblProyecto.AutoSize = True
            Me.lblProyecto.Location = New System.Drawing.Point(6, 56)
            Me.lblProyecto.Name = "lblProyecto"
            Me.lblProyecto.Size = New System.Drawing.Size(61, 13)
            Me.lblProyecto.TabIndex = 2
            Me.lblProyecto.Text = "Proyecto:"
            '
            'lblEntidad
            '
            Me.lblEntidad.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblEntidad.AutoSize = True
            Me.lblEntidad.Location = New System.Drawing.Point(6, 23)
            Me.lblEntidad.Name = "lblEntidad"
            Me.lblEntidad.Size = New System.Drawing.Size(52, 13)
            Me.lblEntidad.TabIndex = 1
            Me.lblEntidad.Text = "Entidad:"
            '
            'EntidadDesktopComboBox
            '
            Me.EntidadDesktopComboBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EntidadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadDesktopComboBox.DisabledEnter = False
            Me.EntidadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadDesktopComboBox.FormattingEnabled = True
            Me.EntidadDesktopComboBox.Location = New System.Drawing.Point(83, 20)
            Me.EntidadDesktopComboBox.Name = "EntidadDesktopComboBox"
            Me.EntidadDesktopComboBox.Size = New System.Drawing.Size(185, 21)
            Me.EntidadDesktopComboBox.TabIndex = 0
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.CargandoPictureBox)
            Me.Panel1.Controls.Add(Me.ReversarButton)
            Me.Panel1.Controls.Add(Me.PrintButton)
            Me.Panel1.Controls.Add(Me.CerrarButton)
            Me.Panel1.Controls.Add(Me.PauseButton)
            Me.Panel1.Controls.Add(Me.PlayButton)
            Me.Panel1.Controls.Add(Me.SolicitudesTreeView)
            Me.Panel1.Location = New System.Drawing.Point(6, 172)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(517, 277)
            Me.Panel1.TabIndex = 1
            '
            'CargandoPictureBox
            '
            Me.CargandoPictureBox.Image = Global.Miharu.Custody.Library.My.Resources.Resources.ajax_loader
            Me.CargandoPictureBox.Location = New System.Drawing.Point(152, 86)
            Me.CargandoPictureBox.Name = "CargandoPictureBox"
            Me.CargandoPictureBox.Size = New System.Drawing.Size(136, 115)
            Me.CargandoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.CargandoPictureBox.TabIndex = 20
            Me.CargandoPictureBox.TabStop = False
            Me.CargandoPictureBox.Visible = False
            '
            'ReversarButton
            '
            Me.ReversarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ReversarButton.BackColor = System.Drawing.SystemColors.Control
            Me.ReversarButton.FlatAppearance.BorderSize = 0
            Me.ReversarButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.ReversarButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.ReversarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ReversarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Reversar
            Me.ReversarButton.Location = New System.Drawing.Point(461, 168)
            Me.ReversarButton.Name = "ReversarButton"
            Me.ReversarButton.Size = New System.Drawing.Size(55, 50)
            Me.ReversarButton.TabIndex = 5
            Me.ToolTip.SetToolTip(Me.ReversarButton, "Reversar Solicitud")
            Me.ReversarButton.UseVisualStyleBackColor = False
            '
            'PrintButton
            '
            Me.PrintButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrintButton.BackColor = System.Drawing.SystemColors.Control
            Me.PrintButton.FlatAppearance.BorderSize = 0
            Me.PrintButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.PrintButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.PrintButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.PrintButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.document_print
            Me.PrintButton.Location = New System.Drawing.Point(461, 112)
            Me.PrintButton.Name = "PrintButton"
            Me.PrintButton.Size = New System.Drawing.Size(55, 50)
            Me.PrintButton.TabIndex = 4
            Me.ToolTip.SetToolTip(Me.PrintButton, "Imprimir Reporte")
            Me.PrintButton.UseVisualStyleBackColor = False
            '
            'CerrarButton
            '
            Me.CerrarButton.BackColor = System.Drawing.SystemColors.Control
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.FlatAppearance.BorderSize = 0
            Me.CerrarButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.CerrarButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.CerrarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CerrarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Close_Red
            Me.CerrarButton.Location = New System.Drawing.Point(461, 224)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(55, 50)
            Me.CerrarButton.TabIndex = 3
            Me.ToolTip.SetToolTip(Me.CerrarButton, "Cerrar")
            Me.CerrarButton.UseVisualStyleBackColor = False
            '
            'PauseButton
            '
            Me.PauseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PauseButton.BackColor = System.Drawing.SystemColors.Control
            Me.PauseButton.Enabled = False
            Me.PauseButton.FlatAppearance.BorderSize = 0
            Me.PauseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.PauseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.PauseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.PauseButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.pause
            Me.PauseButton.Location = New System.Drawing.Point(461, 56)
            Me.PauseButton.Name = "PauseButton"
            Me.PauseButton.Size = New System.Drawing.Size(55, 50)
            Me.PauseButton.TabIndex = 2
            Me.ToolTip.SetToolTip(Me.PauseButton, "Pausar Solicitud")
            Me.PauseButton.UseVisualStyleBackColor = False
            '
            'PlayButton
            '
            Me.PlayButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PlayButton.BackColor = System.Drawing.SystemColors.Control
            Me.PlayButton.Enabled = False
            Me.PlayButton.FlatAppearance.BorderSize = 0
            Me.PlayButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.PlayButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.PlayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.PlayButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.play
            Me.PlayButton.Location = New System.Drawing.Point(461, 0)
            Me.PlayButton.Name = "PlayButton"
            Me.PlayButton.Size = New System.Drawing.Size(55, 50)
            Me.PlayButton.TabIndex = 1
            Me.ToolTip.SetToolTip(Me.PlayButton, "Activar Solicitud")
            Me.PlayButton.UseVisualStyleBackColor = False
            '
            'SolicitudesTreeView
            '
            Me.SolicitudesTreeView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SolicitudesTreeView.ImageIndex = 0
            Me.SolicitudesTreeView.ImageList = Me.IconosTreeList
            Me.SolicitudesTreeView.Location = New System.Drawing.Point(0, 0)
            Me.SolicitudesTreeView.Name = "SolicitudesTreeView"
            Me.SolicitudesTreeView.SelectedImageIndex = 0
            Me.SolicitudesTreeView.Size = New System.Drawing.Size(455, 274)
            Me.SolicitudesTreeView.TabIndex = 0
            '
            'IconosTreeList
            '
            Me.IconosTreeList.ImageStream = CType(resources.GetObject("IconosTreeList.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.IconosTreeList.TransparentColor = System.Drawing.Color.Transparent
            Me.IconosTreeList.Images.SetKeyName(0, "Solicitud.png")
            Me.IconosTreeList.Images.SetKeyName(1, "Folder.png")
            Me.IconosTreeList.Images.SetKeyName(2, "File.png")
            Me.IconosTreeList.Images.SetKeyName(3, "Solicitud_OK.png")
            Me.IconosTreeList.Images.SetKeyName(4, "Solicitud_Proxima.png")
            Me.IconosTreeList.Images.SetKeyName(5, "Solicitud_Vencida.png")
            Me.IconosTreeList.Images.SetKeyName(6, "clock_ok.png")
            Me.IconosTreeList.Images.SetKeyName(7, "clock_proximo.png")
            Me.IconosTreeList.Images.SetKeyName(8, "clock_vencido.png")
            '
            'DesktopTextBoxControlSolicitud
            '
            Me.DesktopTextBoxControlSolicitud._Obligatorio = False
            Me.DesktopTextBoxControlSolicitud._PermitePegar = False
            Me.DesktopTextBoxControlSolicitud.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DesktopTextBoxControlSolicitud.Cantidad_Decimales = CType(2, Short)
            Me.DesktopTextBoxControlSolicitud.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.DesktopTextBoxControlSolicitud.DateFormat = Nothing
            Me.DesktopTextBoxControlSolicitud.DisabledEnter = False
            Me.DesktopTextBoxControlSolicitud.DisabledTab = False
            Me.DesktopTextBoxControlSolicitud.EnabledShortCuts = False
            Me.DesktopTextBoxControlSolicitud.FocusIn = System.Drawing.Color.LightYellow
            Me.DesktopTextBoxControlSolicitud.FocusOut = System.Drawing.Color.White
            Me.DesktopTextBoxControlSolicitud.Location = New System.Drawing.Point(89, 131)
            Me.DesktopTextBoxControlSolicitud.MaskedTextBox_Property = ""
            Me.DesktopTextBoxControlSolicitud.MaximumLength = CType(0, Short)
            Me.DesktopTextBoxControlSolicitud.MinimumLength = CType(0, Short)
            Me.DesktopTextBoxControlSolicitud.Name = "DesktopTextBoxControlSolicitud"
            Me.DesktopTextBoxControlSolicitud.Obligatorio = False
            Me.DesktopTextBoxControlSolicitud.permitePegar = False
            Rango2.MaxValue = 2147483647.0R
            Rango2.MinValue = 0.0R
            Me.DesktopTextBoxControlSolicitud.Rango = Rango2
            Me.DesktopTextBoxControlSolicitud.Size = New System.Drawing.Size(185, 21)
            Me.DesktopTextBoxControlSolicitud.TabIndex = 11
            Me.DesktopTextBoxControlSolicitud.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.DesktopTextBoxControlSolicitud.Usa_Decimales = False
            Me.DesktopTextBoxControlSolicitud.Validos_Cantidad_Puntos = False
            '
            'lblSolicitud
            '
            Me.lblSolicitud.AutoSize = True
            Me.lblSolicitud.Location = New System.Drawing.Point(12, 134)
            Me.lblSolicitud.Name = "lblSolicitud"
            Me.lblSolicitud.Size = New System.Drawing.Size(58, 13)
            Me.lblSolicitud.TabIndex = 11
            Me.lblSolicitud.Text = "Solicitud:"
            '
            'SolicitudesBackgroundWorker
            '
            Me.SolicitudesBackgroundWorker.WorkerReportsProgress = True
            Me.SolicitudesBackgroundWorker.WorkerSupportsCancellation = True
            '
            'btnDetenerBusqueda
            '
            Me.btnDetenerBusqueda.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnDetenerBusqueda.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Cancelar
            Me.btnDetenerBusqueda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnDetenerBusqueda.Location = New System.Drawing.Point(321, 129)
            Me.btnDetenerBusqueda.Name = "btnDetenerBusqueda"
            Me.btnDetenerBusqueda.Size = New System.Drawing.Size(140, 23)
            Me.btnDetenerBusqueda.TabIndex = 11
            Me.btnDetenerBusqueda.Text = "Detener Busqueda"
            Me.btnDetenerBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnDetenerBusqueda.UseVisualStyleBackColor = True
            Me.btnDetenerBusqueda.Visible = False
            '
            'FormAdministracionSolicitudes
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(522, 486)
            Me.Controls.Add(Me.btnDetenerBusqueda)
            Me.Controls.Add(Me.lblSolicitud)
            Me.Controls.Add(Me.DesktopTextBoxControlSolicitud)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.FiltrosGroupBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormAdministracionSolicitudes"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Administración Solicitudes"
            Me.FiltrosGroupBox.ResumeLayout(False)
            Me.FiltrosGroupBox.PerformLayout()
            Me.Panel1.ResumeLayout(False)
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents FiltrosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents SolicitudesTreeView As System.Windows.Forms.TreeView
        Friend WithEvents ProyectoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents lblProyecto As System.Windows.Forms.Label
        Friend WithEvents lblEntidad As System.Windows.Forms.Label
        Friend WithEvents EntidadDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents lblPrioridad As System.Windows.Forms.Label
        Friend WithEvents PrioridadDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents IdSolicitanteDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents lblSolicitante As System.Windows.Forms.Label
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents IconosTreeList As System.Windows.Forms.ImageList
        Friend WithEvents PlayButton As System.Windows.Forms.Button
        Friend WithEvents PauseButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents EstadoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents EstadoLabel As System.Windows.Forms.Label
        Friend WithEvents PrintButton As System.Windows.Forms.Button
        Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents ReversarButton As System.Windows.Forms.Button
        Friend WithEvents DesktopTextBoxControlSolicitud As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents lblSolicitud As System.Windows.Forms.Label
        Friend WithEvents SolicitudesBackgroundWorker As System.ComponentModel.BackgroundWorker
        Public WithEvents CargandoPictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents btnDetenerBusqueda As System.Windows.Forms.Button
    End Class
End Namespace