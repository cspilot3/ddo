Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Solicitudes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormAtencionSolicitudes
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAtencionSolicitudes))
            Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.FiltrosGroupBox = New System.Windows.Forms.GroupBox()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.IdSolicitanteDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.lblSolicitante = New System.Windows.Forms.Label()
            Me.PrioridadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblPrioridad = New System.Windows.Forms.Label()
            Me.ProyectoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblProyecto = New System.Windows.Forms.Label()
            Me.lblEntidad = New System.Windows.Forms.Label()
            Me.EntidadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.FoldersGroupBox = New System.Windows.Forms.GroupBox()
            Me.cbarrasFolderDesktopCBarrasControl = New Miharu.Desktop.Controls.DesktopCBarras.DesktopCBarrasControl()
            Me.lblCbarrasFolder = New System.Windows.Forms.Label()
            Me.FoldersDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.Id_Solicitud = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarras = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ItemsSolicitudGroupBox = New System.Windows.Forms.GroupBox()
            Me.cbarrasItemDesktopCBarrasControl = New Miharu.Desktop.Controls.DesktopCBarras.DesktopCBarrasControl()
            Me.CBarrasItemLabel = New System.Windows.Forms.Label()
            Me.ItemsSolicitudDesktopData = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.IconosImageList = New System.Windows.Forms.ImageList(Me.components)
            Me.AtencionSolicitudesBackgroundWorker = New System.ComponentModel.BackgroundWorker()
            Me.CargandoPictureBox = New System.Windows.Forms.PictureBox()
            Me.SolicitudesGroupBox = New System.Windows.Forms.GroupBox()
            Me.SolicitudesDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Estado = New System.Windows.Forms.DataGridViewImageColumn()
            Me.Responsbale = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Solicitud = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarras_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipoSolicitud = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FiltrosGroupBox.SuspendLayout()
            Me.FoldersGroupBox.SuspendLayout()
            CType(Me.FoldersDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ItemsSolicitudGroupBox.SuspendLayout()
            CType(Me.ItemsSolicitudDesktopData, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SolicitudesGroupBox.SuspendLayout()
            CType(Me.SolicitudesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'FiltrosGroupBox
            '
            Me.FiltrosGroupBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FiltrosGroupBox.Controls.Add(Me.BuscarButton)
            Me.FiltrosGroupBox.Controls.Add(Me.IdSolicitanteDesktopTextBox)
            Me.FiltrosGroupBox.Controls.Add(Me.lblSolicitante)
            Me.FiltrosGroupBox.Controls.Add(Me.PrioridadDesktopComboBox)
            Me.FiltrosGroupBox.Controls.Add(Me.lblPrioridad)
            Me.FiltrosGroupBox.Controls.Add(Me.ProyectoDesktopComboBox)
            Me.FiltrosGroupBox.Controls.Add(Me.lblProyecto)
            Me.FiltrosGroupBox.Controls.Add(Me.lblEntidad)
            Me.FiltrosGroupBox.Controls.Add(Me.EntidadDesktopComboBox)
            Me.FiltrosGroupBox.Location = New System.Drawing.Point(4, 8)
            Me.FiltrosGroupBox.Name = "FiltrosGroupBox"
            Me.FiltrosGroupBox.Size = New System.Drawing.Size(718, 111)
            Me.FiltrosGroupBox.TabIndex = 1
            Me.FiltrosGroupBox.TabStop = False
            Me.FiltrosGroupBox.Text = "Filtro de Búsqueda"
            '
            'BuscarButton
            '
            Me.BuscarButton.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(638, 80)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(74, 23)
            Me.BuscarButton.TabIndex = 4
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
            Me.IdSolicitanteDesktopTextBox.Location = New System.Drawing.Point(427, 53)
            Me.IdSolicitanteDesktopTextBox.MaskedTextBox_Property = ""
            Me.IdSolicitanteDesktopTextBox.MaximumLength = CType(0, Short)
            Me.IdSolicitanteDesktopTextBox.MinimumLength = CType(0, Short)
            Me.IdSolicitanteDesktopTextBox.Name = "IdSolicitanteDesktopTextBox"
            Me.IdSolicitanteDesktopTextBox.Obligatorio = False
            Me.IdSolicitanteDesktopTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.IdSolicitanteDesktopTextBox.Rango = Rango1
            Me.IdSolicitanteDesktopTextBox.Size = New System.Drawing.Size(285, 21)
            Me.IdSolicitanteDesktopTextBox.TabIndex = 3
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
            Me.lblSolicitante.Location = New System.Drawing.Point(351, 56)
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
            Me.PrioridadDesktopComboBox.Location = New System.Drawing.Point(427, 20)
            Me.PrioridadDesktopComboBox.Name = "PrioridadDesktopComboBox"
            Me.PrioridadDesktopComboBox.Size = New System.Drawing.Size(285, 21)
            Me.PrioridadDesktopComboBox.TabIndex = 1
            '
            'lblPrioridad
            '
            Me.lblPrioridad.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblPrioridad.AutoSize = True
            Me.lblPrioridad.Location = New System.Drawing.Point(351, 23)
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
            Me.ProyectoDesktopComboBox.Size = New System.Drawing.Size(248, 21)
            Me.ProyectoDesktopComboBox.TabIndex = 2
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
            Me.EntidadDesktopComboBox.Size = New System.Drawing.Size(248, 21)
            Me.EntidadDesktopComboBox.TabIndex = 0
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(647, 488)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(75, 23)
            Me.CerrarButton.TabIndex = 3
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FoldersGroupBox
            '
            Me.FoldersGroupBox.Controls.Add(Me.cbarrasFolderDesktopCBarrasControl)
            Me.FoldersGroupBox.Controls.Add(Me.lblCbarrasFolder)
            Me.FoldersGroupBox.Controls.Add(Me.FoldersDataGridView)
            Me.FoldersGroupBox.Location = New System.Drawing.Point(431, 130)
            Me.FoldersGroupBox.Name = "FoldersGroupBox"
            Me.FoldersGroupBox.Padding = New System.Windows.Forms.Padding(5)
            Me.FoldersGroupBox.Size = New System.Drawing.Size(291, 152)
            Me.FoldersGroupBox.TabIndex = 5
            Me.FoldersGroupBox.TabStop = False
            Me.FoldersGroupBox.Text = "2 - Folders"
            '
            'cbarrasFolderDesktopCBarrasControl
            '
            Me.cbarrasFolderDesktopCBarrasControl.FocusIn = System.Drawing.Color.LightYellow
            Me.cbarrasFolderDesktopCBarrasControl.FocusOut = System.Drawing.Color.White
            Me.cbarrasFolderDesktopCBarrasControl.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cbarrasFolderDesktopCBarrasControl.Location = New System.Drawing.Point(119, 16)
            Me.cbarrasFolderDesktopCBarrasControl.MaxLength = 0
            Me.cbarrasFolderDesktopCBarrasControl.Name = "cbarrasFolderDesktopCBarrasControl"
            Me.cbarrasFolderDesktopCBarrasControl.Permite_Pegar = False
            Me.cbarrasFolderDesktopCBarrasControl.Size = New System.Drawing.Size(164, 20)
            Me.cbarrasFolderDesktopCBarrasControl.TabIndex = 5
            '
            'lblCbarrasFolder
            '
            Me.lblCbarrasFolder.AutoSize = True
            Me.lblCbarrasFolder.Location = New System.Drawing.Point(8, 19)
            Me.lblCbarrasFolder.Name = "lblCbarrasFolder"
            Me.lblCbarrasFolder.Size = New System.Drawing.Size(105, 13)
            Me.lblCbarrasFolder.TabIndex = 7
            Me.lblCbarrasFolder.Text = "Código de Barras:"
            '
            'FoldersDataGridView
            '
            Me.FoldersDataGridView.AllowUserToAddRows = False
            Me.FoldersDataGridView.AllowUserToDeleteRows = False
            Me.FoldersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.FoldersDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.FoldersDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.FoldersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.FoldersDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id_Solicitud, Me.CBarras})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.FoldersDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.FoldersDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.FoldersDataGridView.Location = New System.Drawing.Point(5, 43)
            Me.FoldersDataGridView.MultiSelect = False
            Me.FoldersDataGridView.Name = "FoldersDataGridView"
            Me.FoldersDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.FoldersDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.FoldersDataGridView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FoldersDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.FoldersDataGridView.Size = New System.Drawing.Size(281, 104)
            Me.FoldersDataGridView.TabIndex = 3
            '
            'Id_Solicitud
            '
            Me.Id_Solicitud.DataPropertyName = "fk_Solicitud"
            Me.Id_Solicitud.HeaderText = "Id"
            Me.Id_Solicitud.Name = "Id_Solicitud"
            Me.Id_Solicitud.ReadOnly = True
            Me.Id_Solicitud.Width = 44
            '
            'CBarras
            '
            Me.CBarras.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.CBarras.DataPropertyName = "CBarras_Folder"
            Me.CBarras.HeaderText = "C. Barras"
            Me.CBarras.Name = "CBarras"
            Me.CBarras.ReadOnly = True
            '
            'ItemsSolicitudGroupBox
            '
            Me.ItemsSolicitudGroupBox.Controls.Add(Me.cbarrasItemDesktopCBarrasControl)
            Me.ItemsSolicitudGroupBox.Controls.Add(Me.CBarrasItemLabel)
            Me.ItemsSolicitudGroupBox.Controls.Add(Me.ItemsSolicitudDesktopData)
            Me.ItemsSolicitudGroupBox.Location = New System.Drawing.Point(5, 289)
            Me.ItemsSolicitudGroupBox.Name = "ItemsSolicitudGroupBox"
            Me.ItemsSolicitudGroupBox.Padding = New System.Windows.Forms.Padding(5)
            Me.ItemsSolicitudGroupBox.Size = New System.Drawing.Size(717, 193)
            Me.ItemsSolicitudGroupBox.TabIndex = 6
            Me.ItemsSolicitudGroupBox.TabStop = False
            Me.ItemsSolicitudGroupBox.Text = "3 - Items"
            '
            'cbarrasItemDesktopCBarrasControl
            '
            Me.cbarrasItemDesktopCBarrasControl.FocusIn = System.Drawing.Color.LightYellow
            Me.cbarrasItemDesktopCBarrasControl.FocusOut = System.Drawing.Color.White
            Me.cbarrasItemDesktopCBarrasControl.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cbarrasItemDesktopCBarrasControl.Location = New System.Drawing.Point(113, 16)
            Me.cbarrasItemDesktopCBarrasControl.MaxLength = 0
            Me.cbarrasItemDesktopCBarrasControl.Name = "cbarrasItemDesktopCBarrasControl"
            Me.cbarrasItemDesktopCBarrasControl.Permite_Pegar = False
            Me.cbarrasItemDesktopCBarrasControl.Size = New System.Drawing.Size(217, 20)
            Me.cbarrasItemDesktopCBarrasControl.TabIndex = 6
            '
            'CBarrasItemLabel
            '
            Me.CBarrasItemLabel.AutoSize = True
            Me.CBarrasItemLabel.Location = New System.Drawing.Point(2, 19)
            Me.CBarrasItemLabel.Name = "CBarrasItemLabel"
            Me.CBarrasItemLabel.Size = New System.Drawing.Size(105, 13)
            Me.CBarrasItemLabel.TabIndex = 5
            Me.CBarrasItemLabel.Text = "Código de Barras:"
            '
            'ItemsSolicitudDesktopData
            '
            Me.ItemsSolicitudDesktopData.AllowUserToAddRows = False
            Me.ItemsSolicitudDesktopData.AllowUserToDeleteRows = False
            Me.ItemsSolicitudDesktopData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.ItemsSolicitudDesktopData.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ItemsSolicitudDesktopData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
            Me.ItemsSolicitudDesktopData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ItemsSolicitudDesktopData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fk_Solicitud, Me.CBarras_File, Me.Documento, Me.TipoSolicitud})
            DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.ItemsSolicitudDesktopData.DefaultCellStyle = DataGridViewCellStyle5
            Me.ItemsSolicitudDesktopData.GridColor = System.Drawing.SystemColors.Control
            Me.ItemsSolicitudDesktopData.Location = New System.Drawing.Point(5, 43)
            Me.ItemsSolicitudDesktopData.MultiSelect = False
            Me.ItemsSolicitudDesktopData.Name = "ItemsSolicitudDesktopData"
            DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ItemsSolicitudDesktopData.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
            Me.ItemsSolicitudDesktopData.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ItemsSolicitudDesktopData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ItemsSolicitudDesktopData.Size = New System.Drawing.Size(707, 144)
            Me.ItemsSolicitudDesktopData.TabIndex = 4
            '
            'IconosImageList
            '
            Me.IconosImageList.ImageStream = CType(resources.GetObject("IconosImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.IconosImageList.TransparentColor = System.Drawing.Color.Transparent
            Me.IconosImageList.Images.SetKeyName(0, "Green.png")
            Me.IconosImageList.Images.SetKeyName(1, "Yellow.png")
            Me.IconosImageList.Images.SetKeyName(2, "Red.png")
            '
            'AtencionSolicitudesBackgroundWorker
            '
            Me.AtencionSolicitudesBackgroundWorker.WorkerReportsProgress = True
            Me.AtencionSolicitudesBackgroundWorker.WorkerSupportsCancellation = True
            '
            'CargandoPictureBox
            '
            Me.CargandoPictureBox.Image = Global.Miharu.Custody.Library.My.Resources.Resources.ajax_loader
            Me.CargandoPictureBox.Location = New System.Drawing.Point(129, 41)
            Me.CargandoPictureBox.Name = "CargandoPictureBox"
            Me.CargandoPictureBox.Size = New System.Drawing.Size(136, 101)
            Me.CargandoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.CargandoPictureBox.TabIndex = 21
            Me.CargandoPictureBox.TabStop = False
            Me.CargandoPictureBox.Visible = False
            '
            'SolicitudesGroupBox
            '
            Me.SolicitudesGroupBox.Controls.Add(Me.CargandoPictureBox)
            Me.SolicitudesGroupBox.Controls.Add(Me.SolicitudesDataGridView)
            Me.SolicitudesGroupBox.Location = New System.Drawing.Point(5, 130)
            Me.SolicitudesGroupBox.Name = "SolicitudesGroupBox"
            Me.SolicitudesGroupBox.Padding = New System.Windows.Forms.Padding(5)
            Me.SolicitudesGroupBox.Size = New System.Drawing.Size(411, 152)
            Me.SolicitudesGroupBox.TabIndex = 4
            Me.SolicitudesGroupBox.TabStop = False
            Me.SolicitudesGroupBox.Text = "1- Solicitudes"
            '
            'SolicitudesDataGridView
            '
            Me.SolicitudesDataGridView.AllowUserToAddRows = False
            Me.SolicitudesDataGridView.AllowUserToDeleteRows = False
            Me.SolicitudesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.SolicitudesDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.SolicitudesDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
            Me.SolicitudesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.SolicitudesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.Estado, Me.Responsbale})
            DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.SolicitudesDataGridView.DefaultCellStyle = DataGridViewCellStyle8
            Me.SolicitudesDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.SolicitudesDataGridView.Location = New System.Drawing.Point(5, 19)
            Me.SolicitudesDataGridView.MultiSelect = False
            Me.SolicitudesDataGridView.Name = "SolicitudesDataGridView"
            Me.SolicitudesDataGridView.ReadOnly = True
            DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.SolicitudesDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
            Me.SolicitudesDataGridView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.SolicitudesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.SolicitudesDataGridView.Size = New System.Drawing.Size(401, 128)
            Me.SolicitudesDataGridView.TabIndex = 2
            '
            'Id
            '
            Me.Id.DataPropertyName = "Id_Solicitud"
            Me.Id.HeaderText = "Id"
            Me.Id.Name = "Id"
            Me.Id.ReadOnly = True
            Me.Id.Width = 44
            '
            'Estado
            '
            Me.Estado.HeaderText = "Estado"
            Me.Estado.Name = "Estado"
            Me.Estado.ReadOnly = True
            Me.Estado.Width = 51
            '
            'Responsbale
            '
            Me.Responsbale.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Responsbale.DataPropertyName = "Nombres"
            Me.Responsbale.HeaderText = "Responsable"
            Me.Responsbale.Name = "Responsbale"
            Me.Responsbale.ReadOnly = True
            '
            'fk_Solicitud
            '
            Me.fk_Solicitud.DataPropertyName = "id_Item_Solicitud"
            Me.fk_Solicitud.HeaderText = "Id"
            Me.fk_Solicitud.Name = "fk_Solicitud"
            Me.fk_Solicitud.ReadOnly = True
            Me.fk_Solicitud.Width = 44
            '
            'CBarras_File
            '
            Me.CBarras_File.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.CBarras_File.DataPropertyName = "CBarras_File"
            Me.CBarras_File.HeaderText = "C. Barras"
            Me.CBarras_File.Name = "CBarras_File"
            Me.CBarras_File.ReadOnly = True
            Me.CBarras_File.Visible = False
            '
            'Documento
            '
            Me.Documento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Documento.DataPropertyName = "Nombre_Documento"
            Me.Documento.HeaderText = "Documento"
            Me.Documento.Name = "Documento"
            Me.Documento.ReadOnly = True
            '
            'TipoSolicitud
            '
            Me.TipoSolicitud.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.TipoSolicitud.DataPropertyName = "Nombre_Solicitud_Tipo"
            Me.TipoSolicitud.HeaderText = "Tipo Solicitud"
            Me.TipoSolicitud.Name = "TipoSolicitud"
            Me.TipoSolicitud.ReadOnly = True
            Me.TipoSolicitud.Width = 200
            '
            'FormAtencionSolicitudes
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(726, 515)
            Me.Controls.Add(Me.ItemsSolicitudGroupBox)
            Me.Controls.Add(Me.FoldersGroupBox)
            Me.Controls.Add(Me.SolicitudesGroupBox)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.FiltrosGroupBox)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.MinimumSize = New System.Drawing.Size(734, 542)
            Me.Name = "FormAtencionSolicitudes"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Atención Solicitudes"
            Me.FiltrosGroupBox.ResumeLayout(False)
            Me.FiltrosGroupBox.PerformLayout()
            Me.FoldersGroupBox.ResumeLayout(False)
            Me.FoldersGroupBox.PerformLayout()
            CType(Me.FoldersDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ItemsSolicitudGroupBox.ResumeLayout(False)
            Me.ItemsSolicitudGroupBox.PerformLayout()
            CType(Me.ItemsSolicitudDesktopData, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SolicitudesGroupBox.ResumeLayout(False)
            CType(Me.SolicitudesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents FiltrosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents IdSolicitanteDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents lblSolicitante As System.Windows.Forms.Label
        Friend WithEvents PrioridadDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents lblPrioridad As System.Windows.Forms.Label
        Friend WithEvents ProyectoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents lblProyecto As System.Windows.Forms.Label
        Friend WithEvents lblEntidad As System.Windows.Forms.Label
        Friend WithEvents EntidadDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents FoldersGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents FoldersDataGridView As DesktopDataGridViewControl
        Friend WithEvents Id_Solicitud As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarras As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ItemsSolicitudGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ItemsSolicitudDesktopData As DesktopDataGridViewControl
        Friend WithEvents IconosImageList As System.Windows.Forms.ImageList
        Friend WithEvents cbarrasItemDesktopCBarrasControl As DesktopCBarrasControl
        Friend WithEvents CBarrasItemLabel As System.Windows.Forms.Label
        Friend WithEvents cbarrasFolderDesktopCBarrasControl As DesktopCbarrasControl
        Friend WithEvents lblCbarrasFolder As System.Windows.Forms.Label
        Friend WithEvents AtencionSolicitudesBackgroundWorker As System.ComponentModel.BackgroundWorker
        Public WithEvents CargandoPictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents SolicitudesGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents SolicitudesDataGridView As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Estado As System.Windows.Forms.DataGridViewImageColumn
        Friend WithEvents Responsbale As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Solicitud As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarras_File As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents TipoSolicitud As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace