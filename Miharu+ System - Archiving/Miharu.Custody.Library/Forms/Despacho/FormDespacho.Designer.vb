Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Despacho
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormDespacho
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDespacho))
            Me.ItemsDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.Eliminar = New System.Windows.Forms.DataGridViewImageColumn()
            Me.CBarrasCarpeta = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarrasFile = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.MotivoSolicitud = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipoSolicitud = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Prioridad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipoDocumento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Solicitud = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Item_Solicitud = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IDDestinatario = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.cbarrasDesktopCBarrasControl = New Miharu.Desktop.Controls.DesktopCBarras.DesktopCBarrasControl()
            Me.AgregarButton = New System.Windows.Forms.Button()
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
            Me.SolicitudesTreeView = New System.Windows.Forms.TreeView()
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
            Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
            Me.DestinatarioDesktopDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.AsignarGuiaButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.ReimprimirGuiaButton = New System.Windows.Forms.Button()
            Me.CerrarGuiaColumn = New System.Windows.Forms.DataGridViewImageColumn()
            Me.Destinatario = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Guia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Sello = New System.Windows.Forms.DataGridViewTextBoxColumn()
            CType(Me.ItemsDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.FiltrosGroupBox.SuspendLayout()
            CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer2.Panel1.SuspendLayout()
            Me.SplitContainer2.Panel2.SuspendLayout()
            Me.SplitContainer2.SuspendLayout()
            CType(Me.DestinatarioDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ItemsDesktopDataGridView
            '
            Me.ItemsDesktopDataGridView.AllowUserToAddRows = False
            Me.ItemsDesktopDataGridView.AllowUserToDeleteRows = False
            Me.ItemsDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.ItemsDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ItemsDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.ItemsDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ItemsDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Eliminar, Me.CBarrasCarpeta, Me.CBarrasFile, Me.MotivoSolicitud, Me.TipoSolicitud, Me.Prioridad, Me.TipoDocumento, Me.fk_Solicitud, Me.id_Item_Solicitud, Me.IDDestinatario})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.ItemsDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.ItemsDesktopDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ItemsDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.ItemsDesktopDataGridView.Location = New System.Drawing.Point(3, 3)
            Me.ItemsDesktopDataGridView.MultiSelect = False
            Me.ItemsDesktopDataGridView.Name = "ItemsDesktopDataGridView"
            Me.ItemsDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ItemsDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.ItemsDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ItemsDesktopDataGridView.Size = New System.Drawing.Size(453, 268)
            Me.ItemsDesktopDataGridView.TabIndex = 0
            '
            'Eliminar
            '
            Me.Eliminar.HeaderText = ""
            Me.Eliminar.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.Eliminar.Name = "Eliminar"
            Me.Eliminar.ReadOnly = True
            Me.Eliminar.ToolTipText = "Eliminar Item de guia"
            Me.Eliminar.Width = 5
            '
            'CBarrasCarpeta
            '
            Me.CBarrasCarpeta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.CBarrasCarpeta.DataPropertyName = "CBarras_Folder"
            Me.CBarrasCarpeta.HeaderText = "CBarras Carpeta"
            Me.CBarrasCarpeta.Name = "CBarrasCarpeta"
            Me.CBarrasCarpeta.ReadOnly = True
            Me.CBarrasCarpeta.Width = 114
            '
            'CBarrasFile
            '
            Me.CBarrasFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.CBarrasFile.DataPropertyName = "CBarras_File"
            Me.CBarrasFile.HeaderText = "CBarras File"
            Me.CBarrasFile.Name = "CBarrasFile"
            Me.CBarrasFile.ReadOnly = True
            Me.CBarrasFile.Width = 90
            '
            'MotivoSolicitud
            '
            Me.MotivoSolicitud.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.MotivoSolicitud.DataPropertyName = "Nombre_Solicitud_Motivo"
            Me.MotivoSolicitud.HeaderText = "Motivo Solicitud"
            Me.MotivoSolicitud.Name = "MotivoSolicitud"
            Me.MotivoSolicitud.ReadOnly = True
            Me.MotivoSolicitud.Width = 112
            '
            'TipoSolicitud
            '
            Me.TipoSolicitud.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.TipoSolicitud.DataPropertyName = "Nombre_solicitud_Tipo"
            Me.TipoSolicitud.HeaderText = "Tipo Solicitud"
            Me.TipoSolicitud.Name = "TipoSolicitud"
            Me.TipoSolicitud.ReadOnly = True
            Me.TipoSolicitud.Width = 98
            '
            'Prioridad
            '
            Me.Prioridad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Prioridad.DataPropertyName = "Nombre_solicitud_Prioridad"
            Me.Prioridad.HeaderText = "Prioridad"
            Me.Prioridad.Name = "Prioridad"
            Me.Prioridad.ReadOnly = True
            Me.Prioridad.Width = 83
            '
            'TipoDocumento
            '
            Me.TipoDocumento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.TipoDocumento.DataPropertyName = "Nombre_Tipologia"
            Me.TipoDocumento.HeaderText = "Tipo Documento"
            Me.TipoDocumento.Name = "TipoDocumento"
            Me.TipoDocumento.ReadOnly = True
            Me.TipoDocumento.Width = 114
            '
            'fk_Solicitud
            '
            Me.fk_Solicitud.DataPropertyName = "fk_Solicitud"
            Me.fk_Solicitud.HeaderText = "fk_Solicitud"
            Me.fk_Solicitud.Name = "fk_Solicitud"
            Me.fk_Solicitud.ReadOnly = True
            Me.fk_Solicitud.Visible = False
            Me.fk_Solicitud.Width = 98
            '
            'id_Item_Solicitud
            '
            Me.id_Item_Solicitud.DataPropertyName = "id_Item_Solicitud"
            Me.id_Item_Solicitud.HeaderText = "id_Item_Solicitud"
            Me.id_Item_Solicitud.Name = "id_Item_Solicitud"
            Me.id_Item_Solicitud.ReadOnly = True
            Me.id_Item_Solicitud.Visible = False
            Me.id_Item_Solicitud.Width = 132
            '
            'IDDestinatario
            '
            Me.IDDestinatario.DataPropertyName = "ID"
            Me.IDDestinatario.HeaderText = "IDDestinatario"
            Me.IDDestinatario.Name = "IDDestinatario"
            Me.IDDestinatario.ReadOnly = True
            Me.IDDestinatario.Visible = False
            Me.IDDestinatario.Width = 115
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(15, 21)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(102, 13)
            Me.Label1.TabIndex = 8
            Me.Label1.Text = "Codigo de Barras"
            '
            'cbarrasDesktopCBarrasControl
            '
            Me.cbarrasDesktopCBarrasControl.FocusIn = System.Drawing.Color.LightYellow
            Me.cbarrasDesktopCBarrasControl.FocusOut = System.Drawing.Color.White
            Me.cbarrasDesktopCBarrasControl.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cbarrasDesktopCBarrasControl.Location = New System.Drawing.Point(123, 18)
            Me.cbarrasDesktopCBarrasControl.MaxLength = 0
            Me.cbarrasDesktopCBarrasControl.Name = "cbarrasDesktopCBarrasControl"
            Me.cbarrasDesktopCBarrasControl.Permite_Pegar = False
            Me.cbarrasDesktopCBarrasControl.Size = New System.Drawing.Size(193, 20)
            Me.cbarrasDesktopCBarrasControl.TabIndex = 0
            '
            'AgregarButton
            '
            Me.AgregarButton.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.AgregarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Procesar
            Me.AgregarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AgregarButton.Location = New System.Drawing.Point(365, 56)
            Me.AgregarButton.Name = "AgregarButton"
            Me.AgregarButton.Size = New System.Drawing.Size(104, 24)
            Me.AgregarButton.TabIndex = 9
            Me.AgregarButton.Text = "&Procesar"
            Me.AgregarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AgregarButton.UseVisualStyleBackColor = True
            '
            'SplitContainer1
            '
            Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
            Me.SplitContainer1.Location = New System.Drawing.Point(12, 12)
            Me.SplitContainer1.Name = "SplitContainer1"
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.SolicitudesTreeView)
            Me.SplitContainer1.Panel1.Controls.Add(Me.FiltrosGroupBox)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
            Me.SplitContainer1.Panel2.Controls.Add(Me.AsignarGuiaButton)
            Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
            Me.SplitContainer1.Panel2.Controls.Add(Me.cbarrasDesktopCBarrasControl)
            Me.SplitContainer1.Panel2.Margin = New System.Windows.Forms.Padding(3)
            Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
            Me.SplitContainer1.Size = New System.Drawing.Size(768, 508)
            Me.SplitContainer1.SplitterDistance = 275
            Me.SplitContainer1.TabIndex = 0
            '
            'SolicitudesTreeView
            '
            Me.SolicitudesTreeView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SolicitudesTreeView.Location = New System.Drawing.Point(15, 208)
            Me.SolicitudesTreeView.Name = "SolicitudesTreeView"
            Me.SolicitudesTreeView.Size = New System.Drawing.Size(239, 277)
            Me.SolicitudesTreeView.TabIndex = 15
            '
            'FiltrosGroupBox
            '
            Me.FiltrosGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FiltrosGroupBox.Controls.Add(Me.BuscarButton)
            Me.FiltrosGroupBox.Controls.Add(Me.IdSolicitanteDesktopTextBox)
            Me.FiltrosGroupBox.Controls.Add(Me.lblSolicitante)
            Me.FiltrosGroupBox.Controls.Add(Me.PrioridadDesktopComboBox)
            Me.FiltrosGroupBox.Controls.Add(Me.lblPrioridad)
            Me.FiltrosGroupBox.Controls.Add(Me.ProyectoDesktopComboBox)
            Me.FiltrosGroupBox.Controls.Add(Me.lblProyecto)
            Me.FiltrosGroupBox.Controls.Add(Me.lblEntidad)
            Me.FiltrosGroupBox.Controls.Add(Me.EntidadDesktopComboBox)
            Me.FiltrosGroupBox.Location = New System.Drawing.Point(15, 14)
            Me.FiltrosGroupBox.Name = "FiltrosGroupBox"
            Me.FiltrosGroupBox.Size = New System.Drawing.Size(239, 188)
            Me.FiltrosGroupBox.TabIndex = 9
            Me.FiltrosGroupBox.TabStop = False
            Me.FiltrosGroupBox.Text = "Filtro de Búsqueda"
            '
            'BuscarButton
            '
            Me.BuscarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(125, 152)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(94, 23)
            Me.BuscarButton.TabIndex = 14
            Me.BuscarButton.Text = "&Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'IdSolicitanteDesktopTextBox
            '
            Me.IdSolicitanteDesktopTextBox._Obligatorio = False
            Me.IdSolicitanteDesktopTextBox._PermitePegar = False
            Me.IdSolicitanteDesktopTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.IdSolicitanteDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.IdSolicitanteDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.IdSolicitanteDesktopTextBox.DateFormat = Nothing
            Me.IdSolicitanteDesktopTextBox.DisabledEnter = False
            Me.IdSolicitanteDesktopTextBox.DisabledTab = False
            Me.IdSolicitanteDesktopTextBox.EnabledShortCuts = False
            Me.IdSolicitanteDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.IdSolicitanteDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.IdSolicitanteDesktopTextBox.Location = New System.Drawing.Point(83, 118)
            Me.IdSolicitanteDesktopTextBox.MaskedTextBox_Property = ""
            Me.IdSolicitanteDesktopTextBox.MaximumLength = CType(0, Short)
            Me.IdSolicitanteDesktopTextBox.MinimumLength = CType(0, Short)
            Me.IdSolicitanteDesktopTextBox.Name = "IdSolicitanteDesktopTextBox"
            Me.IdSolicitanteDesktopTextBox.Obligatorio = False
            Me.IdSolicitanteDesktopTextBox.permitePegar = False
            Rango1.MaxValue = 1.7976931348623157E+308R
            Rango1.MinValue = 0.0R
            Me.IdSolicitanteDesktopTextBox.Rango = Rango1
            Me.IdSolicitanteDesktopTextBox.Size = New System.Drawing.Size(136, 21)
            Me.IdSolicitanteDesktopTextBox.TabIndex = 13
            Me.IdSolicitanteDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.IdSolicitanteDesktopTextBox.Usa_Decimales = False
            Me.IdSolicitanteDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'lblSolicitante
            '
            Me.lblSolicitante.AutoSize = True
            Me.lblSolicitante.Location = New System.Drawing.Point(6, 121)
            Me.lblSolicitante.Name = "lblSolicitante"
            Me.lblSolicitante.Size = New System.Drawing.Size(70, 13)
            Me.lblSolicitante.TabIndex = 6
            Me.lblSolicitante.Text = "Solicitante:"
            '
            'PrioridadDesktopComboBox
            '
            Me.PrioridadDesktopComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrioridadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.PrioridadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.PrioridadDesktopComboBox.DisabledEnter = False
            Me.PrioridadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.PrioridadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.PrioridadDesktopComboBox.FormattingEnabled = True
            Me.PrioridadDesktopComboBox.Location = New System.Drawing.Point(83, 88)
            Me.PrioridadDesktopComboBox.Name = "PrioridadDesktopComboBox"
            Me.PrioridadDesktopComboBox.Size = New System.Drawing.Size(136, 21)
            Me.PrioridadDesktopComboBox.TabIndex = 12
            '
            'lblPrioridad
            '
            Me.lblPrioridad.AutoSize = True
            Me.lblPrioridad.Location = New System.Drawing.Point(6, 88)
            Me.lblPrioridad.Name = "lblPrioridad"
            Me.lblPrioridad.Size = New System.Drawing.Size(61, 13)
            Me.lblPrioridad.TabIndex = 4
            Me.lblPrioridad.Text = "Prioridad:"
            '
            'ProyectoDesktopComboBox
            '
            Me.ProyectoDesktopComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ProyectoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProyectoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProyectoDesktopComboBox.DisabledEnter = False
            Me.ProyectoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProyectoDesktopComboBox.FormattingEnabled = True
            Me.ProyectoDesktopComboBox.Location = New System.Drawing.Point(83, 53)
            Me.ProyectoDesktopComboBox.Name = "ProyectoDesktopComboBox"
            Me.ProyectoDesktopComboBox.Size = New System.Drawing.Size(136, 21)
            Me.ProyectoDesktopComboBox.TabIndex = 11
            '
            'lblProyecto
            '
            Me.lblProyecto.AutoSize = True
            Me.lblProyecto.Location = New System.Drawing.Point(6, 56)
            Me.lblProyecto.Name = "lblProyecto"
            Me.lblProyecto.Size = New System.Drawing.Size(61, 13)
            Me.lblProyecto.TabIndex = 2
            Me.lblProyecto.Text = "Proyecto:"
            '
            'lblEntidad
            '
            Me.lblEntidad.AutoSize = True
            Me.lblEntidad.Location = New System.Drawing.Point(6, 23)
            Me.lblEntidad.Name = "lblEntidad"
            Me.lblEntidad.Size = New System.Drawing.Size(52, 13)
            Me.lblEntidad.TabIndex = 1
            Me.lblEntidad.Text = "Entidad:"
            '
            'EntidadDesktopComboBox
            '
            Me.EntidadDesktopComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EntidadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadDesktopComboBox.DisabledEnter = False
            Me.EntidadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadDesktopComboBox.FormattingEnabled = True
            Me.EntidadDesktopComboBox.Location = New System.Drawing.Point(83, 20)
            Me.EntidadDesktopComboBox.Name = "EntidadDesktopComboBox"
            Me.EntidadDesktopComboBox.Size = New System.Drawing.Size(136, 21)
            Me.EntidadDesktopComboBox.TabIndex = 10
            '
            'SplitContainer2
            '
            Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SplitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.SplitContainer2.Location = New System.Drawing.Point(13, 48)
            Me.SplitContainer2.Name = "SplitContainer2"
            Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer2.Panel1
            '
            Me.SplitContainer2.Panel1.Controls.Add(Me.DestinatarioDesktopDataGridView)
            Me.SplitContainer2.Panel1.Margin = New System.Windows.Forms.Padding(3)
            Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(3)
            Me.SplitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
            '
            'SplitContainer2.Panel2
            '
            Me.SplitContainer2.Panel2.Controls.Add(Me.ItemsDesktopDataGridView)
            Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(3)
            Me.SplitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.SplitContainer2.Size = New System.Drawing.Size(463, 437)
            Me.SplitContainer2.SplitterDistance = 155
            Me.SplitContainer2.TabIndex = 21
            '
            'DestinatarioDesktopDataGridView
            '
            Me.DestinatarioDesktopDataGridView.AllowUserToAddRows = False
            Me.DestinatarioDesktopDataGridView.AllowUserToDeleteRows = False
            Me.DestinatarioDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.DestinatarioDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DestinatarioDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
            Me.DestinatarioDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DestinatarioDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CerrarGuiaColumn, Me.Destinatario, Me.ID, Me.Guia, Me.Sello})
            DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DestinatarioDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle5
            Me.DestinatarioDesktopDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DestinatarioDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.DestinatarioDesktopDataGridView.Location = New System.Drawing.Point(3, 3)
            Me.DestinatarioDesktopDataGridView.MultiSelect = False
            Me.DestinatarioDesktopDataGridView.Name = "DestinatarioDesktopDataGridView"
            Me.DestinatarioDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DestinatarioDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
            Me.DestinatarioDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DestinatarioDesktopDataGridView.Size = New System.Drawing.Size(453, 145)
            Me.DestinatarioDesktopDataGridView.TabIndex = 2
            '
            'AsignarGuiaButton
            '
            Me.AsignarGuiaButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnAgregar
            Me.AsignarGuiaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AsignarGuiaButton.Location = New System.Drawing.Point(348, 16)
            Me.AsignarGuiaButton.Name = "AsignarGuiaButton"
            Me.AsignarGuiaButton.Size = New System.Drawing.Size(128, 23)
            Me.AsignarGuiaButton.TabIndex = 1
            Me.AsignarGuiaButton.Text = "&Asignar a Guia"
            Me.AsignarGuiaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AsignarGuiaButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(693, 531)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(87, 30)
            Me.CerrarButton.TabIndex = 22
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'ReimprimirGuiaButton
            '
            Me.ReimprimirGuiaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.ReimprimirGuiaButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnPrinter
            Me.ReimprimirGuiaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ReimprimirGuiaButton.Location = New System.Drawing.Point(12, 531)
            Me.ReimprimirGuiaButton.Name = "ReimprimirGuiaButton"
            Me.ReimprimirGuiaButton.Size = New System.Drawing.Size(148, 30)
            Me.ReimprimirGuiaButton.TabIndex = 23
            Me.ReimprimirGuiaButton.Text = "&Reimprimir Guias"
            Me.ReimprimirGuiaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ReimprimirGuiaButton.UseVisualStyleBackColor = True
            '
            'CerrarGuiaColumn
            '
            Me.CerrarGuiaColumn.HeaderText = ""
            Me.CerrarGuiaColumn.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir
            Me.CerrarGuiaColumn.Name = "CerrarGuiaColumn"
            Me.CerrarGuiaColumn.ReadOnly = True
            Me.CerrarGuiaColumn.ToolTipText = "Cerrar guia de despacho"
            Me.CerrarGuiaColumn.Width = 5
            '
            'Destinatario
            '
            Me.Destinatario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Destinatario.DataPropertyName = "Nombre_Destinatario"
            Me.Destinatario.HeaderText = "Destinatario"
            Me.Destinatario.Name = "Destinatario"
            Me.Destinatario.ReadOnly = True
            '
            'ID
            '
            Me.ID.DataPropertyName = "ID"
            Me.ID.HeaderText = "ID"
            Me.ID.Name = "ID"
            Me.ID.ReadOnly = True
            Me.ID.Visible = False
            Me.ID.Width = 45
            '
            'Guia
            '
            Me.Guia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Guia.DataPropertyName = "Guia"
            Me.Guia.HeaderText = "Guia"
            Me.Guia.Name = "Guia"
            Me.Guia.ReadOnly = True
            Me.Guia.Width = 57
            '
            'Sello
            '
            Me.Sello.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Sello.DataPropertyName = "Sello"
            Me.Sello.HeaderText = "Sello"
            Me.Sello.Name = "Sello"
            Me.Sello.ReadOnly = True
            Me.Sello.Width = 59
            '
            'FormDespacho
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(792, 573)
            Me.Controls.Add(Me.ReimprimirGuiaButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.SplitContainer1)
            Me.Controls.Add(Me.AgregarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormDespacho"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Despacho de Solicitudes"
            CType(Me.ItemsDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            Me.SplitContainer1.Panel2.PerformLayout()
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.ResumeLayout(False)
            Me.FiltrosGroupBox.ResumeLayout(False)
            Me.FiltrosGroupBox.PerformLayout()
            Me.SplitContainer2.Panel1.ResumeLayout(False)
            Me.SplitContainer2.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer2.ResumeLayout(False)
            CType(Me.DestinatarioDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ItemsDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents cbarrasDesktopCBarrasControl As DesktopCbarrasControl
        Friend WithEvents AgregarButton As System.Windows.Forms.Button
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
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
        Friend WithEvents SolicitudesTreeView As System.Windows.Forms.TreeView
        Friend WithEvents AsignarGuiaButton As System.Windows.Forms.Button
        Friend WithEvents DestinatarioDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Eliminar As System.Windows.Forms.DataGridViewImageColumn
        Friend WithEvents CBarrasCarpeta As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarrasFile As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents MotivoSolicitud As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents TipoSolicitud As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Prioridad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents TipoDocumento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Solicitud As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Item_Solicitud As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IDDestinatario As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ReimprimirGuiaButton As System.Windows.Forms.Button
        Friend WithEvents CerrarGuiaColumn As System.Windows.Forms.DataGridViewImageColumn
        Friend WithEvents Destinatario As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Guia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Sello As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace