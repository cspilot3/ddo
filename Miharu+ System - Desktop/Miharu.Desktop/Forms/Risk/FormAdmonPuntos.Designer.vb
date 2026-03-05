Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox
Namespace Forms.Risk
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormAdmonPuntos
        Inherits Miharu.Desktop.Library.FormBase

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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.ArchivoOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
            Me.CargueBackgroundWorker = New System.ComponentModel.BackgroundWorker()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.fk_entidadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label15 = New System.Windows.Forms.Label()
            Me.fk_proyectoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.CargarButton = New System.Windows.Forms.Button()
            Me.BuscarArchivoButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.OpcionesSeparadorGroupBox = New System.Windows.Forms.GroupBox()
            Me.PuntoComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.TabuladorRadioButton = New System.Windows.Forms.RadioButton()
            Me.ComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.chkEncabezado = New System.Windows.Forms.CheckBox()
            Me.ProgresoGroupBox = New System.Windows.Forms.GroupBox()
            Me.InsertadosLabel = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.ActualizadosLabel = New System.Windows.Forms.Label()
            Me.ProcesadosTituloLabel = New System.Windows.Forms.Label()
            Me.TotalRegistrosLabel = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.FiltrosGroupBox = New System.Windows.Forms.GroupBox()
            Me.BuscarPunto = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.PuntosClienteDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.ArchivoDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Codigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Regional = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Ciudad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Direccion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Responsable = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Telefono = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Correo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.OpcionesSeparadorGroupBox.SuspendLayout()
            Me.ProgresoGroupBox.SuspendLayout()
            Me.FiltrosGroupBox.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            CType(Me.PuntosClienteDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ArchivoOpenFileDialog
            '
            Me.ArchivoOpenFileDialog.Filter = "Archivos de Cargue (*.txt; *.xls; *.csv)|*.txt;*.xls;*.csv"
            Me.ArchivoOpenFileDialog.InitialDirectory = """c:\"""
            Me.ArchivoOpenFileDialog.ReadOnlyChecked = True
            Me.ArchivoOpenFileDialog.RestoreDirectory = True
            '
            'CargueBackgroundWorker
            '
            Me.CargueBackgroundWorker.WorkerReportsProgress = True
            Me.CargueBackgroundWorker.WorkerSupportsCancellation = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(29, 23)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(52, 13)
            Me.Label1.TabIndex = 2
            Me.Label1.Text = "Entidad:"
            '
            'fk_entidadDesktopComboBox
            '
            Me.fk_entidadDesktopComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.fk_entidadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.fk_entidadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.fk_entidadDesktopComboBox.DisabledEnter = False
            Me.fk_entidadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.fk_entidadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.fk_entidadDesktopComboBox.FormattingEnabled = True
            Me.fk_entidadDesktopComboBox.Location = New System.Drawing.Point(90, 20)
            Me.fk_entidadDesktopComboBox.Name = "fk_entidadDesktopComboBox"
            Me.fk_entidadDesktopComboBox.Size = New System.Drawing.Size(219, 21)
            Me.fk_entidadDesktopComboBox.TabIndex = 3
            '
            'Label15
            '
            Me.Label15.AutoSize = True
            Me.Label15.Location = New System.Drawing.Point(330, 23)
            Me.Label15.Name = "Label15"
            Me.Label15.Size = New System.Drawing.Size(61, 13)
            Me.Label15.TabIndex = 4
            Me.Label15.Text = "Proyecto:"
            '
            'fk_proyectoDesktopComboBox
            '
            Me.fk_proyectoDesktopComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.fk_proyectoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.fk_proyectoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.fk_proyectoDesktopComboBox.DisabledEnter = False
            Me.fk_proyectoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.fk_proyectoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.fk_proyectoDesktopComboBox.FormattingEnabled = True
            Me.fk_proyectoDesktopComboBox.Location = New System.Drawing.Point(398, 20)
            Me.fk_proyectoDesktopComboBox.Name = "fk_proyectoDesktopComboBox"
            Me.fk_proyectoDesktopComboBox.Size = New System.Drawing.Size(192, 21)
            Me.fk_proyectoDesktopComboBox.TabIndex = 5
            '
            'CargarButton
            '
            Me.CargarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CargarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnAgregar
            Me.CargarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CargarButton.Location = New System.Drawing.Point(29, 261)
            Me.CargarButton.Name = "CargarButton"
            Me.CargarButton.Size = New System.Drawing.Size(121, 30)
            Me.CargarButton.TabIndex = 7
            Me.CargarButton.Text = "&Cargar Puntos"
            Me.CargarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CargarButton.UseVisualStyleBackColor = True
            '
            'BuscarArchivoButton
            '
            Me.BuscarArchivoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarArchivoButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnBuscar
            Me.BuscarArchivoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarArchivoButton.Location = New System.Drawing.Point(453, 261)
            Me.BuscarArchivoButton.Name = "BuscarArchivoButton"
            Me.BuscarArchivoButton.Size = New System.Drawing.Size(87, 30)
            Me.BuscarArchivoButton.TabIndex = 9
            Me.BuscarArchivoButton.Text = "&Buscar"
            Me.BuscarArchivoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarArchivoButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(664, 261)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(105, 30)
            Me.CerrarButton.TabIndex = 10
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'OpcionesSeparadorGroupBox
            '
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.PuntoComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.TabuladorRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.ComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Enabled = False
            Me.OpcionesSeparadorGroupBox.Location = New System.Drawing.Point(29, 307)
            Me.OpcionesSeparadorGroupBox.Name = "OpcionesSeparadorGroupBox"
            Me.OpcionesSeparadorGroupBox.Size = New System.Drawing.Size(233, 91)
            Me.OpcionesSeparadorGroupBox.TabIndex = 12
            Me.OpcionesSeparadorGroupBox.TabStop = False
            Me.OpcionesSeparadorGroupBox.Text = "Opciones de Separador"
            '
            'PuntoComaRadioButton
            '
            Me.PuntoComaRadioButton.AutoSize = True
            Me.PuntoComaRadioButton.Location = New System.Drawing.Point(7, 67)
            Me.PuntoComaRadioButton.Name = "PuntoComaRadioButton"
            Me.PuntoComaRadioButton.Size = New System.Drawing.Size(119, 17)
            Me.PuntoComaRadioButton.TabIndex = 2
            Me.PuntoComaRadioButton.Text = "Punto y Coma (;)"
            Me.PuntoComaRadioButton.UseVisualStyleBackColor = True
            '
            'TabuladorRadioButton
            '
            Me.TabuladorRadioButton.AutoSize = True
            Me.TabuladorRadioButton.Location = New System.Drawing.Point(8, 44)
            Me.TabuladorRadioButton.Name = "TabuladorRadioButton"
            Me.TabuladorRadioButton.Size = New System.Drawing.Size(110, 17)
            Me.TabuladorRadioButton.TabIndex = 1
            Me.TabuladorRadioButton.Text = "Tabulador (     )"
            Me.TabuladorRadioButton.UseVisualStyleBackColor = True
            '
            'ComaRadioButton
            '
            Me.ComaRadioButton.AutoSize = True
            Me.ComaRadioButton.Checked = True
            Me.ComaRadioButton.Location = New System.Drawing.Point(8, 21)
            Me.ComaRadioButton.Name = "ComaRadioButton"
            Me.ComaRadioButton.Size = New System.Drawing.Size(73, 17)
            Me.ComaRadioButton.TabIndex = 0
            Me.ComaRadioButton.TabStop = True
            Me.ComaRadioButton.Text = "Coma (,)"
            Me.ComaRadioButton.UseVisualStyleBackColor = True
            '
            'chkEncabezado
            '
            Me.chkEncabezado.AutoSize = True
            Me.chkEncabezado.Location = New System.Drawing.Point(308, 314)
            Me.chkEncabezado.Name = "chkEncabezado"
            Me.chkEncabezado.Size = New System.Drawing.Size(139, 17)
            Me.chkEncabezado.TabIndex = 14
            Me.chkEncabezado.Text = "Maneja encabezado"
            Me.chkEncabezado.UseVisualStyleBackColor = True
            '
            'ProgresoGroupBox
            '
            Me.ProgresoGroupBox.Controls.Add(Me.InsertadosLabel)
            Me.ProgresoGroupBox.Controls.Add(Me.Label4)
            Me.ProgresoGroupBox.Controls.Add(Me.ActualizadosLabel)
            Me.ProgresoGroupBox.Controls.Add(Me.ProcesadosTituloLabel)
            Me.ProgresoGroupBox.Controls.Add(Me.TotalRegistrosLabel)
            Me.ProgresoGroupBox.Controls.Add(Me.Label2)
            Me.ProgresoGroupBox.Location = New System.Drawing.Point(308, 330)
            Me.ProgresoGroupBox.Name = "ProgresoGroupBox"
            Me.ProgresoGroupBox.Size = New System.Drawing.Size(266, 68)
            Me.ProgresoGroupBox.TabIndex = 17
            Me.ProgresoGroupBox.TabStop = False
            Me.ProgresoGroupBox.Text = "Registros"
            '
            'InsertadosLabel
            '
            Me.InsertadosLabel.AutoSize = True
            Me.InsertadosLabel.ForeColor = System.Drawing.Color.DarkGreen
            Me.InsertadosLabel.Location = New System.Drawing.Point(123, 48)
            Me.InsertadosLabel.Name = "InsertadosLabel"
            Me.InsertadosLabel.Size = New System.Drawing.Size(14, 13)
            Me.InsertadosLabel.TabIndex = 5
            Me.InsertadosLabel.Text = "0"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(6, 47)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(75, 13)
            Me.Label4.TabIndex = 4
            Me.Label4.Text = "Insertados: "
            '
            'ActualizadosLabel
            '
            Me.ActualizadosLabel.AutoSize = True
            Me.ActualizadosLabel.ForeColor = System.Drawing.Color.DarkGreen
            Me.ActualizadosLabel.Location = New System.Drawing.Point(123, 34)
            Me.ActualizadosLabel.Name = "ActualizadosLabel"
            Me.ActualizadosLabel.Size = New System.Drawing.Size(14, 13)
            Me.ActualizadosLabel.TabIndex = 3
            Me.ActualizadosLabel.Text = "0"
            '
            'ProcesadosTituloLabel
            '
            Me.ProcesadosTituloLabel.AutoSize = True
            Me.ProcesadosTituloLabel.Location = New System.Drawing.Point(6, 34)
            Me.ProcesadosTituloLabel.Name = "ProcesadosTituloLabel"
            Me.ProcesadosTituloLabel.Size = New System.Drawing.Size(85, 13)
            Me.ProcesadosTituloLabel.TabIndex = 2
            Me.ProcesadosTituloLabel.Text = "Actualizados: "
            '
            'TotalRegistrosLabel
            '
            Me.TotalRegistrosLabel.AutoSize = True
            Me.TotalRegistrosLabel.ForeColor = System.Drawing.Color.Red
            Me.TotalRegistrosLabel.Location = New System.Drawing.Point(123, 21)
            Me.TotalRegistrosLabel.Name = "TotalRegistrosLabel"
            Me.TotalRegistrosLabel.Size = New System.Drawing.Size(14, 13)
            Me.TotalRegistrosLabel.TabIndex = 1
            Me.TotalRegistrosLabel.Text = "0"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(6, 21)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(56, 13)
            Me.Label2.TabIndex = 0
            Me.Label2.Text = "Archivo: "
            '
            'FiltrosGroupBox
            '
            Me.FiltrosGroupBox.Controls.Add(Me.Label1)
            Me.FiltrosGroupBox.Controls.Add(Me.fk_entidadDesktopComboBox)
            Me.FiltrosGroupBox.Controls.Add(Me.fk_proyectoDesktopComboBox)
            Me.FiltrosGroupBox.Controls.Add(Me.Label15)
            Me.FiltrosGroupBox.Location = New System.Drawing.Point(29, 12)
            Me.FiltrosGroupBox.Name = "FiltrosGroupBox"
            Me.FiltrosGroupBox.Size = New System.Drawing.Size(635, 53)
            Me.FiltrosGroupBox.TabIndex = 18
            Me.FiltrosGroupBox.TabStop = False
            Me.FiltrosGroupBox.Text = "Filtros"
            '
            'BuscarPunto
            '
            Me.BuscarPunto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarPunto.Image = Global.Miharu.Desktop.My.Resources.Resources.btnBuscar
            Me.BuscarPunto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarPunto.Location = New System.Drawing.Point(681, 26)
            Me.BuscarPunto.Name = "BuscarPunto"
            Me.BuscarPunto.Size = New System.Drawing.Size(87, 30)
            Me.BuscarPunto.TabIndex = 19
            Me.BuscarPunto.Text = "&Buscar"
            Me.BuscarPunto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarPunto.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.PuntosClienteDataGridView)
            Me.GroupBox1.Location = New System.Drawing.Point(29, 71)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(740, 184)
            Me.GroupBox1.TabIndex = 20
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Detalle"
            '
            'PuntosClienteDataGridView
            '
            Me.PuntosClienteDataGridView.AllowUserToAddRows = False
            Me.PuntosClienteDataGridView.AllowUserToDeleteRows = False
            Me.PuntosClienteDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.PuntosClienteDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.PuntosClienteDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.PuntosClienteDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.PuntosClienteDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Codigo, Me.Nombre, Me.Regional, Me.Ciudad, Me.Direccion, Me.Responsable, Me.Telefono, Me.Correo})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.PuntosClienteDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.PuntosClienteDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.PuntosClienteDataGridView.Location = New System.Drawing.Point(19, 20)
            Me.PuntosClienteDataGridView.MultiSelect = False
            Me.PuntosClienteDataGridView.Name = "PuntosClienteDataGridView"
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.PuntosClienteDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.PuntosClienteDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.PuntosClienteDataGridView.Size = New System.Drawing.Size(702, 148)
            Me.PuntosClienteDataGridView.TabIndex = 5
            '
            'ArchivoDesktopTextBox
            '
            Me.ArchivoDesktopTextBox._Obligatorio = False
            Me.ArchivoDesktopTextBox._PermitePegar = False
            Me.ArchivoDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.ArchivoDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.ArchivoDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ArchivoDesktopTextBox.DateFormat = Nothing
            Me.ArchivoDesktopTextBox.DisabledEnter = False
            Me.ArchivoDesktopTextBox.DisabledTab = False
            Me.ArchivoDesktopTextBox.EnabledShortCuts = False
            Me.ArchivoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ArchivoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ArchivoDesktopTextBox.Location = New System.Drawing.Point(169, 267)
            Me.ArchivoDesktopTextBox.MaskedTextBox_Property = ""
            Me.ArchivoDesktopTextBox.MaximumLength = CType(0, Short)
            Me.ArchivoDesktopTextBox.MinimumLength = CType(0, Short)
            Me.ArchivoDesktopTextBox.Name = "ArchivoDesktopTextBox"
            Me.ArchivoDesktopTextBox.Obligatorio = False
            Me.ArchivoDesktopTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.ArchivoDesktopTextBox.Rango = Rango1
            Me.ArchivoDesktopTextBox.ReadOnly = True
            Me.ArchivoDesktopTextBox.Size = New System.Drawing.Size(278, 21)
            Me.ArchivoDesktopTextBox.TabIndex = 10
            Me.ArchivoDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ArchivoDesktopTextBox.Usa_Decimales = False
            Me.ArchivoDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Codigo
            '
            Me.Codigo.DataPropertyName = "Codigo_Punto"
            Me.Codigo.HeaderText = "Codigo"
            Me.Codigo.Name = "Codigo"
            Me.Codigo.Width = 70
            '
            'Nombre
            '
            Me.Nombre.DataPropertyName = "Nombre_Punto"
            Me.Nombre.HeaderText = "Nombre"
            Me.Nombre.Name = "Nombre"
            Me.Nombre.Width = 76
            '
            'Regional
            '
            Me.Regional.DataPropertyName = "Regional"
            Me.Regional.HeaderText = "Regional"
            Me.Regional.Name = "Regional"
            Me.Regional.Width = 81
            '
            'Ciudad
            '
            Me.Ciudad.DataPropertyName = "Ciudad"
            Me.Ciudad.HeaderText = "Ciudad"
            Me.Ciudad.Name = "Ciudad"
            Me.Ciudad.Width = 70
            '
            'Direccion
            '
            Me.Direccion.DataPropertyName = "Direccion"
            Me.Direccion.HeaderText = "Direccion"
            Me.Direccion.Name = "Direccion"
            Me.Direccion.Width = 84
            '
            'Responsable
            '
            Me.Responsable.DataPropertyName = "Responsable"
            Me.Responsable.HeaderText = "Responsable"
            Me.Responsable.Name = "Responsable"
            Me.Responsable.Width = 104
            '
            'Telefono
            '
            Me.Telefono.DataPropertyName = "Telefono"
            Me.Telefono.HeaderText = "Telefono"
            Me.Telefono.Name = "Telefono"
            Me.Telefono.Width = 81
            '
            'Correo
            '
            Me.Correo.DataPropertyName = "Correo"
            Me.Correo.HeaderText = "Correo"
            Me.Correo.Name = "Correo"
            Me.Correo.Width = 70
            '
            'FormAdmonPuntos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(783, 410)
            Me.Controls.Add(Me.ArchivoDesktopTextBox)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.BuscarPunto)
            Me.Controls.Add(Me.FiltrosGroupBox)
            Me.Controls.Add(Me.ProgresoGroupBox)
            Me.Controls.Add(Me.chkEncabezado)
            Me.Controls.Add(Me.OpcionesSeparadorGroupBox)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.BuscarArchivoButton)
            Me.Controls.Add(Me.CargarButton)
            Me.Name = "FormAdmonPuntos"
            Me.Text = "FormAdmonPuntos"
            Me.OpcionesSeparadorGroupBox.ResumeLayout(False)
            Me.OpcionesSeparadorGroupBox.PerformLayout()
            Me.ProgresoGroupBox.ResumeLayout(False)
            Me.ProgresoGroupBox.PerformLayout()
            Me.FiltrosGroupBox.ResumeLayout(False)
            Me.FiltrosGroupBox.PerformLayout()
            Me.GroupBox1.ResumeLayout(False)
            CType(Me.PuntosClienteDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents ArchivoOpenFileDialog As System.Windows.Forms.OpenFileDialog
        Friend WithEvents CargueBackgroundWorker As System.ComponentModel.BackgroundWorker
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents fk_entidadDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label15 As System.Windows.Forms.Label
        Friend WithEvents fk_proyectoDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents CargarButton As System.Windows.Forms.Button
        Friend WithEvents BuscarArchivoButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents OpcionesSeparadorGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents PuntoComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents TabuladorRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents ComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents chkEncabezado As System.Windows.Forms.CheckBox
        Friend WithEvents ProgresoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ActualizadosLabel As System.Windows.Forms.Label
        Friend WithEvents ProcesadosTituloLabel As System.Windows.Forms.Label
        Friend WithEvents TotalRegistrosLabel As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents FiltrosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents BuscarPunto As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents PuntosClienteDataGridView As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents ArchivoDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents InsertadosLabel As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Codigo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Regional As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Ciudad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Direccion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Responsable As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Telefono As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Correo As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class

End Namespace
