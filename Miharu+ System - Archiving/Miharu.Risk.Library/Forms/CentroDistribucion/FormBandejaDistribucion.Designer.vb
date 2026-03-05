Imports Miharu.Desktop.Controls.DesktopDataGridView

Namespace Forms.CentroDistribucion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormBandejaDistribucion
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormBandejaDistribucion))
            Me.DistribucionTabControl = New System.Windows.Forms.TabControl()
            Me.TabPage1 = New System.Windows.Forms.TabPage()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.CajasDataGridView = New System.Windows.Forms.DataGridView()
            Me.Id_Caja = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarras = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Externo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EntidadCustodia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.SedeCustodia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.BovedaCustodia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CrearRemisionButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.EntidadComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.ProyectoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.TabPage2 = New System.Windows.Forms.TabPage()
            Me.GroupBox4 = New System.Windows.Forms.GroupBox()
            Me.SedeRemisionDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.PendientesDesktopCheckBox = New Miharu.Desktop.Controls.DesktopCheckBox.DesktopCheckBoxControl()
            Me.BovedaRemisionDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.EntidadRemisionDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.ImprimirButton = New System.Windows.Forms.Button()
            Me.CerrarRemisionesButton = New System.Windows.Forms.Button()
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            Me.RemisionesDesktopDataGridView = New DesktopDataGridViewControl()
            Me.Id_Remision = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Sede = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Boveda = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Direccion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Responsable = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DistribucionImageList = New System.Windows.Forms.ImageList(Me.components)
            Me.DistribucionTabControl.SuspendLayout()
            Me.TabPage1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            CType(Me.CajasDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox1.SuspendLayout()
            Me.TabPage2.SuspendLayout()
            Me.GroupBox4.SuspendLayout()
            Me.GroupBox3.SuspendLayout()
            CType(Me.RemisionesDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'DistribucionTabControl
            '
            Me.DistribucionTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
            Me.DistribucionTabControl.CausesValidation = False
            Me.DistribucionTabControl.Controls.Add(Me.TabPage1)
            Me.DistribucionTabControl.Controls.Add(Me.TabPage2)
            Me.DistribucionTabControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DistribucionTabControl.ImageList = Me.DistribucionImageList
            Me.DistribucionTabControl.Location = New System.Drawing.Point(0, 0)
            Me.DistribucionTabControl.Name = "DistribucionTabControl"
            Me.DistribucionTabControl.SelectedIndex = 0
            Me.DistribucionTabControl.Size = New System.Drawing.Size(742, 523)
            Me.DistribucionTabControl.TabIndex = 0
            '
            'TabPage1
            '
            Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
            Me.TabPage1.Controls.Add(Me.CerrarButton)
            Me.TabPage1.Controls.Add(Me.GroupBox2)
            Me.TabPage1.Controls.Add(Me.CrearRemisionButton)
            Me.TabPage1.Controls.Add(Me.GroupBox1)
            Me.TabPage1.ImageKey = "PendientesDistribucion.png"
            Me.TabPage1.Location = New System.Drawing.Point(4, 26)
            Me.TabPage1.Name = "TabPage1"
            Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage1.Size = New System.Drawing.Size(734, 493)
            Me.TabPage1.TabIndex = 0
            Me.TabPage1.Text = "Pendientes"
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(644, 462)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(75, 23)
            Me.CerrarButton.TabIndex = 5
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'GroupBox2
            '
            Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                          Or System.Windows.Forms.AnchorStyles.Left) _
                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox2.Controls.Add(Me.CajasDataGridView)
            Me.GroupBox2.Location = New System.Drawing.Point(16, 108)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(703, 348)
            Me.GroupBox2.TabIndex = 4
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Cajas pendientes distribución"
            '
            'CajasDataGridView
            '
            Me.CajasDataGridView.AllowUserToAddRows = False
            Me.CajasDataGridView.AllowUserToDeleteRows = False
            Me.CajasDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                  Or System.Windows.Forms.AnchorStyles.Left) _
                                                 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CajasDataGridView.BackgroundColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CajasDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.CajasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CajasDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id_Caja, Me.CBarras, Me.Externo, Me.Proyecto, Me.EntidadCustodia, Me.SedeCustodia, Me.BovedaCustodia})
            Me.CajasDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.CajasDataGridView.Location = New System.Drawing.Point(10, 20)
            Me.CajasDataGridView.MultiSelect = False
            Me.CajasDataGridView.Name = "CajasDataGridView"
            Me.CajasDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CajasDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
            Me.CajasDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle4
            Me.CajasDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.CajasDataGridView.Size = New System.Drawing.Size(680, 313)
            Me.CajasDataGridView.TabIndex = 1
            '
            'Id_Caja
            '
            Me.Id_Caja.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
            Me.Id_Caja.DataPropertyName = "Id_Caja"
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            Me.Id_Caja.DefaultCellStyle = DataGridViewCellStyle2
            Me.Id_Caja.HeaderText = "Id"
            Me.Id_Caja.Name = "Id_Caja"
            Me.Id_Caja.ReadOnly = True
            Me.Id_Caja.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.Id_Caja.Width = 44
            '
            'CBarras
            '
            Me.CBarras.DataPropertyName = "Codigo_Caja"
            Me.CBarras.HeaderText = "C. Barras"
            Me.CBarras.Name = "CBarras"
            Me.CBarras.ReadOnly = True
            '
            'Externo
            '
            Me.Externo.DataPropertyName = "Usa_Custodia_Externa"
            Me.Externo.HeaderText = "Externo"
            Me.Externo.Name = "Externo"
            Me.Externo.ReadOnly = True
            Me.Externo.Width = 60
            '
            'Proyecto
            '
            Me.Proyecto.DataPropertyName = "Nombre_Proyecto"
            Me.Proyecto.HeaderText = "Proyecto"
            Me.Proyecto.Name = "Proyecto"
            Me.Proyecto.ReadOnly = True
            Me.Proyecto.Width = 126
            '
            'EntidadCustodia
            '
            Me.EntidadCustodia.DataPropertyName = "Nombre_Entidad"
            Me.EntidadCustodia.HeaderText = "Entidad Custodia"
            Me.EntidadCustodia.Name = "EntidadCustodia"
            Me.EntidadCustodia.ReadOnly = True
            Me.EntidadCustodia.Width = 127
            '
            'SedeCustodia
            '
            Me.SedeCustodia.DataPropertyName = "Nombre_Sede"
            Me.SedeCustodia.HeaderText = "Sede Custodia"
            Me.SedeCustodia.Name = "SedeCustodia"
            Me.SedeCustodia.ReadOnly = True
            Me.SedeCustodia.Width = 126
            '
            'BovedaCustodia
            '
            Me.BovedaCustodia.DataPropertyName = "Nombre_Boveda"
            Me.BovedaCustodia.HeaderText = "Bóveda Custodia"
            Me.BovedaCustodia.Name = "BovedaCustodia"
            Me.BovedaCustodia.ReadOnly = True
            Me.BovedaCustodia.Width = 125
            '
            'CrearRemisionButton
            '
            Me.CrearRemisionButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CrearRemisionButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.CrearRemision
            Me.CrearRemisionButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CrearRemisionButton.Location = New System.Drawing.Point(617, 40)
            Me.CrearRemisionButton.Name = "CrearRemisionButton"
            Me.CrearRemisionButton.Size = New System.Drawing.Size(102, 62)
            Me.CrearRemisionButton.TabIndex = 2
            Me.CrearRemisionButton.Tag = ""
            Me.CrearRemisionButton.Text = "&Crear Remisión"
            Me.CrearRemisionButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CrearRemisionButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.EntidadComboBox)
            Me.GroupBox1.Controls.Add(Me.ProyectoComboBox)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Location = New System.Drawing.Point(16, 18)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(316, 84)
            Me.GroupBox1.TabIndex = 3
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Búsqueda"
            '
            'EntidadComboBox
            '
            Me.EntidadComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadComboBox.DisabledEnter = False
            Me.EntidadComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadComboBox.FormattingEnabled = True
            Me.EntidadComboBox.Location = New System.Drawing.Point(88, 18)
            Me.EntidadComboBox.Name = "EntidadComboBox"
            Me.EntidadComboBox.Size = New System.Drawing.Size(217, 21)
            Me.EntidadComboBox.TabIndex = 4
            '
            'ProyectoComboBox
            '
            Me.ProyectoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProyectoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProyectoComboBox.DisabledEnter = False
            Me.ProyectoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProyectoComboBox.FormattingEnabled = True
            Me.ProyectoComboBox.Location = New System.Drawing.Point(88, 43)
            Me.ProyectoComboBox.Name = "ProyectoComboBox"
            Me.ProyectoComboBox.Size = New System.Drawing.Size(217, 21)
            Me.ProyectoComboBox.TabIndex = 3
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(7, 46)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(61, 13)
            Me.Label2.TabIndex = 1
            Me.Label2.Text = "Proyecto:"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(7, 21)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(52, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Entidad:"
            '
            'TabPage2
            '
            Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
            Me.TabPage2.Controls.Add(Me.GroupBox4)
            Me.TabPage2.Controls.Add(Me.ImprimirButton)
            Me.TabPage2.Controls.Add(Me.CerrarRemisionesButton)
            Me.TabPage2.Controls.Add(Me.GroupBox3)
            Me.TabPage2.ImageKey = "RemisionDistribucion.png"
            Me.TabPage2.Location = New System.Drawing.Point(4, 26)
            Me.TabPage2.Name = "TabPage2"
            Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage2.Size = New System.Drawing.Size(734, 493)
            Me.TabPage2.TabIndex = 1
            Me.TabPage2.Text = "Remisiones"
            '
            'GroupBox4
            '
            Me.GroupBox4.Controls.Add(Me.SedeRemisionDesktopComboBox)
            Me.GroupBox4.Controls.Add(Me.Label5)
            Me.GroupBox4.Controls.Add(Me.PendientesDesktopCheckBox)
            Me.GroupBox4.Controls.Add(Me.BovedaRemisionDesktopComboBox)
            Me.GroupBox4.Controls.Add(Me.EntidadRemisionDesktopComboBox)
            Me.GroupBox4.Controls.Add(Me.Label3)
            Me.GroupBox4.Controls.Add(Me.Label4)
            Me.GroupBox4.Location = New System.Drawing.Point(12, 21)
            Me.GroupBox4.Name = "GroupBox4"
            Me.GroupBox4.Size = New System.Drawing.Size(440, 103)
            Me.GroupBox4.TabIndex = 8
            Me.GroupBox4.TabStop = False
            Me.GroupBox4.Text = "Búsqueda"
            '
            'SedeRemisionDesktopComboBox
            '
            Me.SedeRemisionDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SedeRemisionDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SedeRemisionDesktopComboBox.DisabledEnter = False
            Me.SedeRemisionDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SedeRemisionDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SedeRemisionDesktopComboBox.FormattingEnabled = True
            Me.SedeRemisionDesktopComboBox.Location = New System.Drawing.Point(210, 46)
            Me.SedeRemisionDesktopComboBox.Name = "SedeRemisionDesktopComboBox"
            Me.SedeRemisionDesktopComboBox.Size = New System.Drawing.Size(217, 21)
            Me.SedeRemisionDesktopComboBox.TabIndex = 6
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(129, 49)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(38, 13)
            Me.Label5.TabIndex = 5
            Me.Label5.Text = "Sede:"
            '
            'PendientesDesktopCheckBox
            '
            Me.PendientesDesktopCheckBox.AutoSize = True
            Me.PendientesDesktopCheckBox.Checked = True
            Me.PendientesDesktopCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
            Me.PendientesDesktopCheckBox.DisabledEnter = False
            Me.PendientesDesktopCheckBox.FocusIn = System.Drawing.Color.LightYellow
            Me.PendientesDesktopCheckBox.FocusOut = System.Drawing.SystemColors.Control
            Me.PendientesDesktopCheckBox.Location = New System.Drawing.Point(10, 21)
            Me.PendientesDesktopCheckBox.Name = "PendientesDesktopCheckBox"
            Me.PendientesDesktopCheckBox.Size = New System.Drawing.Size(89, 17)
            Me.PendientesDesktopCheckBox.TabIndex = 4
            Me.PendientesDesktopCheckBox.Text = "Pendientes"
            Me.PendientesDesktopCheckBox.UseVisualStyleBackColor = True
            '
            'BovedaRemisionDesktopComboBox
            '
            Me.BovedaRemisionDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.BovedaRemisionDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.BovedaRemisionDesktopComboBox.DisabledEnter = False
            Me.BovedaRemisionDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.BovedaRemisionDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.BovedaRemisionDesktopComboBox.FormattingEnabled = True
            Me.BovedaRemisionDesktopComboBox.Location = New System.Drawing.Point(210, 73)
            Me.BovedaRemisionDesktopComboBox.Name = "BovedaRemisionDesktopComboBox"
            Me.BovedaRemisionDesktopComboBox.Size = New System.Drawing.Size(217, 21)
            Me.BovedaRemisionDesktopComboBox.TabIndex = 3
            '
            'EntidadRemisionDesktopComboBox
            '
            Me.EntidadRemisionDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadRemisionDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadRemisionDesktopComboBox.DisabledEnter = False
            Me.EntidadRemisionDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadRemisionDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadRemisionDesktopComboBox.FormattingEnabled = True
            Me.EntidadRemisionDesktopComboBox.Location = New System.Drawing.Point(210, 19)
            Me.EntidadRemisionDesktopComboBox.Name = "EntidadRemisionDesktopComboBox"
            Me.EntidadRemisionDesktopComboBox.Size = New System.Drawing.Size(217, 21)
            Me.EntidadRemisionDesktopComboBox.TabIndex = 2
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(129, 76)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(52, 13)
            Me.Label3.TabIndex = 1
            Me.Label3.Text = "Bóveda:"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(129, 22)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(52, 13)
            Me.Label4.TabIndex = 0
            Me.Label4.Text = "Entidad:"
            '
            'ImprimirButton
            '
            Me.ImprimirButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.print
            Me.ImprimirButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.ImprimirButton.Location = New System.Drawing.Point(605, 44)
            Me.ImprimirButton.Name = "ImprimirButton"
            Me.ImprimirButton.Size = New System.Drawing.Size(120, 61)
            Me.ImprimirButton.TabIndex = 7
            Me.ImprimirButton.Text = "&Imprimir Remisión"
            Me.ImprimirButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.ImprimirButton.UseVisualStyleBackColor = True
            '
            'CerrarRemisionesButton
            '
            Me.CerrarRemisionesButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarRemisionesButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarRemisionesButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarRemisionesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarRemisionesButton.Location = New System.Drawing.Point(653, 462)
            Me.CerrarRemisionesButton.Name = "CerrarRemisionesButton"
            Me.CerrarRemisionesButton.Size = New System.Drawing.Size(75, 23)
            Me.CerrarRemisionesButton.TabIndex = 6
            Me.CerrarRemisionesButton.Text = "&Cerrar"
            Me.CerrarRemisionesButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarRemisionesButton.UseVisualStyleBackColor = True
            '
            'GroupBox3
            '
            Me.GroupBox3.Controls.Add(Me.RemisionesDesktopDataGridView)
            Me.GroupBox3.Location = New System.Drawing.Point(12, 130)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Size = New System.Drawing.Size(713, 326)
            Me.GroupBox3.TabIndex = 0
            Me.GroupBox3.TabStop = False
            Me.GroupBox3.Text = "Remisiones"
            '
            'RemisionesDesktopDataGridView
            '
            Me.RemisionesDesktopDataGridView.AllowUserToAddRows = False
            Me.RemisionesDesktopDataGridView.AllowUserToDeleteRows = False
            Me.RemisionesDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.RemisionesDesktopDataGridView.BackgroundColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.RemisionesDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
            Me.RemisionesDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.RemisionesDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id_Remision, Me.Nombre_Entidad, Me.Nombre_Sede, Me.Nombre_Boveda, Me.Direccion, Me.Responsable})
            DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.RemisionesDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle6
            Me.RemisionesDesktopDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.RemisionesDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.RemisionesDesktopDataGridView.Location = New System.Drawing.Point(3, 17)
            Me.RemisionesDesktopDataGridView.MultiSelect = False
            Me.RemisionesDesktopDataGridView.Name = "RemisionesDesktopDataGridView"
            Me.RemisionesDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.RemisionesDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
            Me.RemisionesDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.RemisionesDesktopDataGridView.Size = New System.Drawing.Size(707, 306)
            Me.RemisionesDesktopDataGridView.TabIndex = 0
            '
            'Id_Remision
            '
            Me.Id_Remision.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Id_Remision.DataPropertyName = "Id_Remision_Caja"
            Me.Id_Remision.HeaderText = "Id_Remision"
            Me.Id_Remision.Name = "Id_Remision"
            Me.Id_Remision.ReadOnly = True
            '
            'Nombre_Entidad
            '
            Me.Nombre_Entidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Nombre_Entidad.DataPropertyName = "Nombre_Entidad"
            Me.Nombre_Entidad.HeaderText = "Entidad"
            Me.Nombre_Entidad.Name = "Nombre_Entidad"
            Me.Nombre_Entidad.ReadOnly = True
            '
            'Nombre_Sede
            '
            Me.Nombre_Sede.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Nombre_Sede.DataPropertyName = "Nombre_Sede"
            Me.Nombre_Sede.HeaderText = "Sede"
            Me.Nombre_Sede.Name = "Nombre_Sede"
            Me.Nombre_Sede.ReadOnly = True
            '
            'Nombre_Boveda
            '
            Me.Nombre_Boveda.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Nombre_Boveda.DataPropertyName = "Nombre_Boveda"
            Me.Nombre_Boveda.HeaderText = "Bóveda"
            Me.Nombre_Boveda.Name = "Nombre_Boveda"
            Me.Nombre_Boveda.ReadOnly = True
            '
            'Direccion
            '
            Me.Direccion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Direccion.DataPropertyName = "Direccion"
            Me.Direccion.HeaderText = "Dirección"
            Me.Direccion.Name = "Direccion"
            Me.Direccion.ReadOnly = True
            '
            'Responsable
            '
            Me.Responsable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Responsable.DataPropertyName = "Responsable"
            Me.Responsable.HeaderText = "Responsable"
            Me.Responsable.Name = "Responsable"
            Me.Responsable.ReadOnly = True
            '
            'DistribucionImageList
            '
            Me.DistribucionImageList.ImageStream = CType(resources.GetObject("DistribucionImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.DistribucionImageList.TransparentColor = System.Drawing.Color.Transparent
            Me.DistribucionImageList.Images.SetKeyName(0, "PendientesDistribucion.png")
            Me.DistribucionImageList.Images.SetKeyName(1, "RemisionDistribucion.png")
            '
            'FormBandejaDistribucion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(742, 523)
            Me.Controls.Add(Me.DistribucionTabControl)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormBandejaDistribucion"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Bandeja Distribución"
            Me.DistribucionTabControl.ResumeLayout(False)
            Me.TabPage1.ResumeLayout(False)
            Me.GroupBox2.ResumeLayout(False)
            CType(Me.CajasDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.TabPage2.ResumeLayout(False)
            Me.GroupBox4.ResumeLayout(False)
            Me.GroupBox4.PerformLayout()
            Me.GroupBox3.ResumeLayout(False)
            CType(Me.RemisionesDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents DistribucionTabControl As System.Windows.Forms.TabControl
        Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
        Friend WithEvents CrearRemisionButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents CajasDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents DistribucionImageList As System.Windows.Forms.ImageList
        Friend WithEvents ProyectoComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Id_Caja As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarras As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Externo As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EntidadCustodia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents SedeCustodia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents BovedaCustodia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents RemisionesDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents Id_Remision As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Sede As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Boveda As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Direccion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Responsable As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CerrarRemisionesButton As System.Windows.Forms.Button
        Friend WithEvents ImprimirButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
        Friend WithEvents BovedaRemisionDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents EntidadRemisionDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents PendientesDesktopCheckBox As Miharu.Desktop.Controls.DesktopCheckBox.DesktopCheckBoxControl
        Friend WithEvents EntidadComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents SedeRemisionDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label5 As System.Windows.Forms.Label
    End Class
End Namespace