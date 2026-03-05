Namespace Procesos.Correos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeguimientoCorreo
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango3 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango4 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango5 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.GroupBoxParametrosBusqueda = New System.Windows.Forms.GroupBox()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.ESTADODesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.BuscarFechaButton = New System.Windows.Forms.Button()
            Me.OTDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblOT = New System.Windows.Forms.Label()
            Me.FechaProcesoFinalLabel = New System.Windows.Forms.Label()
            Me.FechaProcesoFinalDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaProcesoLabel = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.DataGridViewCorreos = New System.Windows.Forms.DataGridView()
            Me.BtnExportar = New System.Windows.Forms.Button()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.TotalRegistrosTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.EstadoRecibidoTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.EstadoRechazadoTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.EstadoEnviadosTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.EstadoProgramadosTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.DataGridViewButtonColumn1 = New System.Windows.Forms.DataGridViewButtonColumn()
            Me.ColumnFechaProceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnIdTrackingMail = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnOT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnExpediente = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Columnfk_Folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Columnid_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnNombreImagen = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnOficio = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnRadicado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnCorreo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnTipoNotificacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreNotificacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnNumeroGuia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnFechaLog = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FechaEnvio = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnEstado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnObservaciones = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnFechaRecepcionRespuesta = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EnviarFisicoColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.ColumnReenviarCorreo = New System.Windows.Forms.DataGridViewButtonColumn()
            Me.ColumnActivo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.GroupBoxParametrosBusqueda.SuspendLayout()
            CType(Me.DataGridViewCorreos, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBoxParametrosBusqueda
            '
            Me.GroupBoxParametrosBusqueda.Controls.Add(Me.Label6)
            Me.GroupBoxParametrosBusqueda.Controls.Add(Me.ESTADODesktopComboBox)
            Me.GroupBoxParametrosBusqueda.Controls.Add(Me.BuscarFechaButton)
            Me.GroupBoxParametrosBusqueda.Controls.Add(Me.OTDesktopComboBox)
            Me.GroupBoxParametrosBusqueda.Controls.Add(Me.lblOT)
            Me.GroupBoxParametrosBusqueda.Controls.Add(Me.FechaProcesoFinalLabel)
            Me.GroupBoxParametrosBusqueda.Controls.Add(Me.FechaProcesoFinalDateTimePicker)
            Me.GroupBoxParametrosBusqueda.Controls.Add(Me.FechaProcesoDateTimePicker)
            Me.GroupBoxParametrosBusqueda.Controls.Add(Me.FechaProcesoLabel)
            Me.GroupBoxParametrosBusqueda.Dock = System.Windows.Forms.DockStyle.Top
            Me.GroupBoxParametrosBusqueda.Location = New System.Drawing.Point(0, 0)
            Me.GroupBoxParametrosBusqueda.Name = "GroupBoxParametrosBusqueda"
            Me.GroupBoxParametrosBusqueda.Size = New System.Drawing.Size(1089, 84)
            Me.GroupBoxParametrosBusqueda.TabIndex = 0
            Me.GroupBoxParametrosBusqueda.TabStop = False
            Me.GroupBoxParametrosBusqueda.Text = "Parámetros de Búsqueda"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label6.Location = New System.Drawing.Point(717, 25)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(57, 13)
            Me.Label6.TabIndex = 35
            Me.Label6.Text = "ESTADO"
            '
            'ESTADODesktopComboBox
            '
            Me.ESTADODesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ESTADODesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ESTADODesktopComboBox.DisabledEnter = False
            Me.ESTADODesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ESTADODesktopComboBox.fk_Campo = 0
            Me.ESTADODesktopComboBox.fk_Documento = 0
            Me.ESTADODesktopComboBox.fk_Validacion = 0
            Me.ESTADODesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ESTADODesktopComboBox.FormattingEnabled = True
            Me.ESTADODesktopComboBox.Location = New System.Drawing.Point(720, 44)
            Me.ESTADODesktopComboBox.Name = "ESTADODesktopComboBox"
            Me.ESTADODesktopComboBox.Size = New System.Drawing.Size(127, 21)
            Me.ESTADODesktopComboBox.TabIndex = 34
            '
            'BuscarFechaButton
            '
            Me.BuscarFechaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarFechaButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarFechaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnBuscar
            Me.BuscarFechaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarFechaButton.Location = New System.Drawing.Point(975, 38)
            Me.BuscarFechaButton.Name = "BuscarFechaButton"
            Me.BuscarFechaButton.Size = New System.Drawing.Size(99, 36)
            Me.BuscarFechaButton.TabIndex = 33
            Me.BuscarFechaButton.Text = "Buscar   "
            Me.BuscarFechaButton.UseVisualStyleBackColor = True
            '
            'OTDesktopComboBox
            '
            Me.OTDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OTDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OTDesktopComboBox.DisabledEnter = False
            Me.OTDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OTDesktopComboBox.fk_Campo = 0
            Me.OTDesktopComboBox.fk_Documento = 0
            Me.OTDesktopComboBox.fk_Validacion = 0
            Me.OTDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OTDesktopComboBox.FormattingEnabled = True
            Me.OTDesktopComboBox.Location = New System.Drawing.Point(587, 45)
            Me.OTDesktopComboBox.Name = "OTDesktopComboBox"
            Me.OTDesktopComboBox.Size = New System.Drawing.Size(127, 21)
            Me.OTDesktopComboBox.TabIndex = 32
            '
            'lblOT
            '
            Me.lblOT.AutoSize = True
            Me.lblOT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblOT.Location = New System.Drawing.Point(584, 25)
            Me.lblOT.Name = "lblOT"
            Me.lblOT.Size = New System.Drawing.Size(24, 13)
            Me.lblOT.TabIndex = 31
            Me.lblOT.Text = "OT"
            '
            'FechaProcesoFinalLabel
            '
            Me.FechaProcesoFinalLabel.AutoSize = True
            Me.FechaProcesoFinalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoFinalLabel.Location = New System.Drawing.Point(294, 25)
            Me.FechaProcesoFinalLabel.Name = "FechaProcesoFinalLabel"
            Me.FechaProcesoFinalLabel.Size = New System.Drawing.Size(138, 15)
            Me.FechaProcesoFinalLabel.TabIndex = 16
            Me.FechaProcesoFinalLabel.Text = "Fecha Proceso Final"
            '
            'FechaProcesoFinalDateTimePicker
            '
            Me.FechaProcesoFinalDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoFinalDateTimePicker.Location = New System.Drawing.Point(297, 44)
            Me.FechaProcesoFinalDateTimePicker.Name = "FechaProcesoFinalDateTimePicker"
            Me.FechaProcesoFinalDateTimePicker.Size = New System.Drawing.Size(279, 22)
            Me.FechaProcesoFinalDateTimePicker.TabIndex = 15
            '
            'FechaProcesoDateTimePicker
            '
            Me.FechaProcesoDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoDateTimePicker.Location = New System.Drawing.Point(9, 44)
            Me.FechaProcesoDateTimePicker.Name = "FechaProcesoDateTimePicker"
            Me.FechaProcesoDateTimePicker.Size = New System.Drawing.Size(279, 22)
            Me.FechaProcesoDateTimePicker.TabIndex = 14
            '
            'FechaProcesoLabel
            '
            Me.FechaProcesoLabel.AutoSize = True
            Me.FechaProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoLabel.Location = New System.Drawing.Point(6, 25)
            Me.FechaProcesoLabel.Name = "FechaProcesoLabel"
            Me.FechaProcesoLabel.Size = New System.Drawing.Size(145, 15)
            Me.FechaProcesoLabel.TabIndex = 13
            Me.FechaProcesoLabel.Text = "Fecha Proceso Inicial"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(395, 17)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(104, 15)
            Me.Label1.TabIndex = 35
            Me.Label1.Text = "Total Registros"
            '
            'DataGridViewCorreos
            '
            Me.DataGridViewCorreos.AllowUserToAddRows = False
            Me.DataGridViewCorreos.AllowUserToDeleteRows = False
            Me.DataGridViewCorreos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridViewCorreos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnFechaProceso, Me.ColumnIdTrackingMail, Me.ColumnOT, Me.ColumnExpediente, Me.Columnfk_Folder, Me.Columnid_File, Me.ColumnNombreImagen, Me.ColumnOficio, Me.ColumnRadicado, Me.ColumnCorreo, Me.ColumnTipoNotificacion, Me.NombreNotificacion, Me.ColumnNumeroGuia, Me.ColumnFechaLog, Me.FechaEnvio, Me.ColumnEstado, Me.ColumnObservaciones, Me.ColumnFechaRecepcionRespuesta, Me.EnviarFisicoColumn, Me.ColumnReenviarCorreo, Me.ColumnActivo})
            Me.DataGridViewCorreos.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DataGridViewCorreos.Location = New System.Drawing.Point(0, 0)
            Me.DataGridViewCorreos.Name = "DataGridViewCorreos"
            Me.DataGridViewCorreos.Size = New System.Drawing.Size(1089, 376)
            Me.DataGridViewCorreos.TabIndex = 1
            '
            'BtnExportar
            '
            Me.BtnExportar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnExportar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnExportar.Location = New System.Drawing.Point(975, 17)
            Me.BtnExportar.Name = "BtnExportar"
            Me.BtnExportar.Size = New System.Drawing.Size(99, 36)
            Me.BtnExportar.TabIndex = 2
            Me.BtnExportar.Text = "Exportar"
            Me.BtnExportar.UseVisualStyleBackColor = True
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.Label1)
            Me.Panel1.Controls.Add(Me.Label5)
            Me.Panel1.Controls.Add(Me.TotalRegistrosTextBox)
            Me.Panel1.Controls.Add(Me.EstadoRecibidoTextBox)
            Me.Panel1.Controls.Add(Me.EstadoRechazadoTextBox)
            Me.Panel1.Controls.Add(Me.Label4)
            Me.Panel1.Controls.Add(Me.EstadoEnviadosTextBox)
            Me.Panel1.Controls.Add(Me.EstadoProgramadosTextBox)
            Me.Panel1.Controls.Add(Me.BtnExportar)
            Me.Panel1.Controls.Add(Me.Label3)
            Me.Panel1.Controls.Add(Me.Label2)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel1.Location = New System.Drawing.Point(0, 460)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(1089, 65)
            Me.Panel1.TabIndex = 3
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label5.Location = New System.Drawing.Point(217, 39)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(71, 15)
            Me.Label5.TabIndex = 42
            Me.Label5.Text = "Recibidos"
            '
            'TotalRegistrosTextBox
            '
            Me.TotalRegistrosTextBox._Obligatorio = False
            Me.TotalRegistrosTextBox._PermitePegar = False
            Me.TotalRegistrosTextBox.Cantidad_Decimales = CType(0, Short)
            Me.TotalRegistrosTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.TotalRegistrosTextBox.DateFormat = Nothing
            Me.TotalRegistrosTextBox.DisabledEnter = False
            Me.TotalRegistrosTextBox.DisabledTab = False
            Me.TotalRegistrosTextBox.EnabledShortCuts = False
            Me.TotalRegistrosTextBox.fk_Campo = 0
            Me.TotalRegistrosTextBox.fk_Documento = 0
            Me.TotalRegistrosTextBox.fk_Validacion = 0
            Me.TotalRegistrosTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.TotalRegistrosTextBox.FocusOut = System.Drawing.Color.White
            Me.TotalRegistrosTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TotalRegistrosTextBox.Location = New System.Drawing.Point(505, 12)
            Me.TotalRegistrosTextBox.MaskedTextBox_Property = ""
            Me.TotalRegistrosTextBox.MaximumLength = CType(0, Short)
            Me.TotalRegistrosTextBox.MinimumLength = CType(0, Short)
            Me.TotalRegistrosTextBox.Name = "TotalRegistrosTextBox"
            Me.TotalRegistrosTextBox.Obligatorio = False
            Me.TotalRegistrosTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.TotalRegistrosTextBox.Rango = Rango1
            Me.TotalRegistrosTextBox.Size = New System.Drawing.Size(57, 21)
            Me.TotalRegistrosTextBox.TabIndex = 34
            Me.TotalRegistrosTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.TotalRegistrosTextBox.Usa_Decimales = False
            Me.TotalRegistrosTextBox.Validos_Cantidad_Puntos = False
            '
            'EstadoRecibidoTextBox
            '
            Me.EstadoRecibidoTextBox._Obligatorio = False
            Me.EstadoRecibidoTextBox._PermitePegar = False
            Me.EstadoRecibidoTextBox.Cantidad_Decimales = CType(0, Short)
            Me.EstadoRecibidoTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.EstadoRecibidoTextBox.DateFormat = Nothing
            Me.EstadoRecibidoTextBox.DisabledEnter = False
            Me.EstadoRecibidoTextBox.DisabledTab = False
            Me.EstadoRecibidoTextBox.EnabledShortCuts = False
            Me.EstadoRecibidoTextBox.fk_Campo = 0
            Me.EstadoRecibidoTextBox.fk_Documento = 0
            Me.EstadoRecibidoTextBox.fk_Validacion = 0
            Me.EstadoRecibidoTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.EstadoRecibidoTextBox.FocusOut = System.Drawing.Color.White
            Me.EstadoRecibidoTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EstadoRecibidoTextBox.Location = New System.Drawing.Point(294, 37)
            Me.EstadoRecibidoTextBox.MaskedTextBox_Property = ""
            Me.EstadoRecibidoTextBox.MaximumLength = CType(0, Short)
            Me.EstadoRecibidoTextBox.MinimumLength = CType(0, Short)
            Me.EstadoRecibidoTextBox.Name = "EstadoRecibidoTextBox"
            Me.EstadoRecibidoTextBox.Obligatorio = False
            Me.EstadoRecibidoTextBox.permitePegar = False
            Rango2.MaxValue = 2147483647.0R
            Rango2.MinValue = 0.0R
            Me.EstadoRecibidoTextBox.Rango = Rango2
            Me.EstadoRecibidoTextBox.Size = New System.Drawing.Size(57, 21)
            Me.EstadoRecibidoTextBox.TabIndex = 41
            Me.EstadoRecibidoTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.EstadoRecibidoTextBox.Usa_Decimales = False
            Me.EstadoRecibidoTextBox.Validos_Cantidad_Puntos = False
            '
            'EstadoRechazadoTextBox
            '
            Me.EstadoRechazadoTextBox._Obligatorio = False
            Me.EstadoRechazadoTextBox._PermitePegar = False
            Me.EstadoRechazadoTextBox.Cantidad_Decimales = CType(0, Short)
            Me.EstadoRechazadoTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.EstadoRechazadoTextBox.DateFormat = Nothing
            Me.EstadoRechazadoTextBox.DisabledEnter = False
            Me.EstadoRechazadoTextBox.DisabledTab = False
            Me.EstadoRechazadoTextBox.EnabledShortCuts = False
            Me.EstadoRechazadoTextBox.fk_Campo = 0
            Me.EstadoRechazadoTextBox.fk_Documento = 0
            Me.EstadoRechazadoTextBox.fk_Validacion = 0
            Me.EstadoRechazadoTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.EstadoRechazadoTextBox.FocusOut = System.Drawing.Color.White
            Me.EstadoRechazadoTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EstadoRechazadoTextBox.Location = New System.Drawing.Point(294, 11)
            Me.EstadoRechazadoTextBox.MaskedTextBox_Property = ""
            Me.EstadoRechazadoTextBox.MaximumLength = CType(0, Short)
            Me.EstadoRechazadoTextBox.MinimumLength = CType(0, Short)
            Me.EstadoRechazadoTextBox.Name = "EstadoRechazadoTextBox"
            Me.EstadoRechazadoTextBox.Obligatorio = False
            Me.EstadoRechazadoTextBox.permitePegar = False
            Rango3.MaxValue = 2147483647.0R
            Rango3.MinValue = 0.0R
            Me.EstadoRechazadoTextBox.Rango = Rango3
            Me.EstadoRechazadoTextBox.Size = New System.Drawing.Size(57, 21)
            Me.EstadoRechazadoTextBox.TabIndex = 40
            Me.EstadoRechazadoTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.EstadoRechazadoTextBox.Usa_Decimales = False
            Me.EstadoRechazadoTextBox.Validos_Cantidad_Puntos = False
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(202, 17)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(86, 15)
            Me.Label4.TabIndex = 38
            Me.Label4.Text = "Rechazados"
            '
            'EstadoEnviadosTextBox
            '
            Me.EstadoEnviadosTextBox._Obligatorio = False
            Me.EstadoEnviadosTextBox._PermitePegar = False
            Me.EstadoEnviadosTextBox.Cantidad_Decimales = CType(0, Short)
            Me.EstadoEnviadosTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.EstadoEnviadosTextBox.DateFormat = Nothing
            Me.EstadoEnviadosTextBox.DisabledEnter = False
            Me.EstadoEnviadosTextBox.DisabledTab = False
            Me.EstadoEnviadosTextBox.EnabledShortCuts = False
            Me.EstadoEnviadosTextBox.fk_Campo = 0
            Me.EstadoEnviadosTextBox.fk_Documento = 0
            Me.EstadoEnviadosTextBox.fk_Validacion = 0
            Me.EstadoEnviadosTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.EstadoEnviadosTextBox.FocusOut = System.Drawing.Color.White
            Me.EstadoEnviadosTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EstadoEnviadosTextBox.Location = New System.Drawing.Point(111, 38)
            Me.EstadoEnviadosTextBox.MaskedTextBox_Property = ""
            Me.EstadoEnviadosTextBox.MaximumLength = CType(0, Short)
            Me.EstadoEnviadosTextBox.MinimumLength = CType(0, Short)
            Me.EstadoEnviadosTextBox.Name = "EstadoEnviadosTextBox"
            Me.EstadoEnviadosTextBox.Obligatorio = False
            Me.EstadoEnviadosTextBox.permitePegar = False
            Rango4.MaxValue = 2147483647.0R
            Rango4.MinValue = 0.0R
            Me.EstadoEnviadosTextBox.Rango = Rango4
            Me.EstadoEnviadosTextBox.Size = New System.Drawing.Size(57, 21)
            Me.EstadoEnviadosTextBox.TabIndex = 39
            Me.EstadoEnviadosTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.EstadoEnviadosTextBox.Usa_Decimales = False
            Me.EstadoEnviadosTextBox.Validos_Cantidad_Puntos = False
            '
            'EstadoProgramadosTextBox
            '
            Me.EstadoProgramadosTextBox._Obligatorio = False
            Me.EstadoProgramadosTextBox._PermitePegar = False
            Me.EstadoProgramadosTextBox.Cantidad_Decimales = CType(0, Short)
            Me.EstadoProgramadosTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.EstadoProgramadosTextBox.DateFormat = Nothing
            Me.EstadoProgramadosTextBox.DisabledEnter = False
            Me.EstadoProgramadosTextBox.DisabledTab = False
            Me.EstadoProgramadosTextBox.EnabledShortCuts = False
            Me.EstadoProgramadosTextBox.fk_Campo = 0
            Me.EstadoProgramadosTextBox.fk_Documento = 0
            Me.EstadoProgramadosTextBox.fk_Validacion = 0
            Me.EstadoProgramadosTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.EstadoProgramadosTextBox.FocusOut = System.Drawing.Color.White
            Me.EstadoProgramadosTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EstadoProgramadosTextBox.Location = New System.Drawing.Point(111, 11)
            Me.EstadoProgramadosTextBox.MaskedTextBox_Property = ""
            Me.EstadoProgramadosTextBox.MaximumLength = CType(0, Short)
            Me.EstadoProgramadosTextBox.MinimumLength = CType(0, Short)
            Me.EstadoProgramadosTextBox.Name = "EstadoProgramadosTextBox"
            Me.EstadoProgramadosTextBox.Obligatorio = False
            Me.EstadoProgramadosTextBox.permitePegar = False
            Rango5.MaxValue = 2147483647.0R
            Rango5.MinValue = 0.0R
            Me.EstadoProgramadosTextBox.Rango = Rango5
            Me.EstadoProgramadosTextBox.Size = New System.Drawing.Size(57, 21)
            Me.EstadoProgramadosTextBox.TabIndex = 38
            Me.EstadoProgramadosTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.EstadoProgramadosTextBox.Usa_Decimales = False
            Me.EstadoProgramadosTextBox.Validos_Cantidad_Puntos = False
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(40, 38)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(65, 15)
            Me.Label3.TabIndex = 37
            Me.Label3.Text = "Enviados"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(12, 17)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(93, 15)
            Me.Label2.TabIndex = 36
            Me.Label2.Text = "Programados"
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.DataGridViewCorreos)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel2.Location = New System.Drawing.Point(0, 84)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(1089, 376)
            Me.Panel2.TabIndex = 4
            '
            'DataGridViewButtonColumn1
            '
            Me.DataGridViewButtonColumn1.DataPropertyName = "ReenviarCorreo"
            Me.DataGridViewButtonColumn1.HeaderText = "Reenviar Correo"
            Me.DataGridViewButtonColumn1.Name = "DataGridViewButtonColumn1"
            Me.DataGridViewButtonColumn1.ReadOnly = True
            Me.DataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            '
            'ColumnFechaProceso
            '
            Me.ColumnFechaProceso.DataPropertyName = "fk_Fecha_Proceso"
            Me.ColumnFechaProceso.HeaderText = "Fecha Proceso"
            Me.ColumnFechaProceso.Name = "ColumnFechaProceso"
            '
            'ColumnIdTrackingMail
            '
            Me.ColumnIdTrackingMail.DataPropertyName = "id_Tracking_Mail"
            Me.ColumnIdTrackingMail.HeaderText = "Id TrackingMail"
            Me.ColumnIdTrackingMail.Name = "ColumnIdTrackingMail"
            Me.ColumnIdTrackingMail.Visible = False
            '
            'ColumnOT
            '
            Me.ColumnOT.DataPropertyName = "id_OT"
            Me.ColumnOT.HeaderText = "OT"
            Me.ColumnOT.Name = "ColumnOT"
            '
            'ColumnExpediente
            '
            Me.ColumnExpediente.DataPropertyName = "fk_Expediente"
            Me.ColumnExpediente.HeaderText = "Expediente"
            Me.ColumnExpediente.Name = "ColumnExpediente"
            '
            'Columnfk_Folder
            '
            Me.Columnfk_Folder.DataPropertyName = "fk_Folder"
            Me.Columnfk_Folder.HeaderText = "Folder"
            Me.Columnfk_Folder.Name = "Columnfk_Folder"
            Me.Columnfk_Folder.Visible = False
            '
            'Columnid_File
            '
            Me.Columnid_File.DataPropertyName = "id_File"
            Me.Columnid_File.HeaderText = "File"
            Me.Columnid_File.Name = "Columnid_File"
            Me.Columnid_File.Visible = False
            '
            'ColumnNombreImagen
            '
            Me.ColumnNombreImagen.DataPropertyName = "NombreImagen"
            Me.ColumnNombreImagen.HeaderText = "Nombre Imagen"
            Me.ColumnNombreImagen.Name = "ColumnNombreImagen"
            '
            'ColumnOficio
            '
            Me.ColumnOficio.DataPropertyName = "Oficio"
            Me.ColumnOficio.HeaderText = "Oficio"
            Me.ColumnOficio.Name = "ColumnOficio"
            '
            'ColumnRadicado
            '
            Me.ColumnRadicado.DataPropertyName = "Radicado"
            Me.ColumnRadicado.HeaderText = "Radicado"
            Me.ColumnRadicado.Name = "ColumnRadicado"
            '
            'ColumnCorreo
            '
            Me.ColumnCorreo.DataPropertyName = "Correo"
            Me.ColumnCorreo.HeaderText = "Correo"
            Me.ColumnCorreo.Name = "ColumnCorreo"
            '
            'ColumnTipoNotificacion
            '
            Me.ColumnTipoNotificacion.DataPropertyName = "TipoNotificacion"
            Me.ColumnTipoNotificacion.HeaderText = "Tipo Notificación"
            Me.ColumnTipoNotificacion.Name = "ColumnTipoNotificacion"
            Me.ColumnTipoNotificacion.Visible = False
            '
            'NombreNotificacion
            '
            Me.NombreNotificacion.DataPropertyName = "Nombre_Notificacion"
            Me.NombreNotificacion.HeaderText = "Nombre Notificacion"
            Me.NombreNotificacion.Name = "NombreNotificacion"
            '
            'ColumnNumeroGuia
            '
            Me.ColumnNumeroGuia.DataPropertyName = "Numero_Guia"
            Me.ColumnNumeroGuia.HeaderText = "Numero Guia"
            Me.ColumnNumeroGuia.Name = "ColumnNumeroGuia"
            Me.ColumnNumeroGuia.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            '
            'ColumnFechaLog
            '
            Me.ColumnFechaLog.DataPropertyName = "FechaLog"
            Me.ColumnFechaLog.HeaderText = "Fecha Log"
            Me.ColumnFechaLog.Name = "ColumnFechaLog"
            '
            'FechaEnvio
            '
            Me.FechaEnvio.DataPropertyName = "FechaEnvio"
            Me.FechaEnvio.HeaderText = "Fecha Envío"
            Me.FechaEnvio.Name = "FechaEnvio"
            '
            'ColumnEstado
            '
            Me.ColumnEstado.DataPropertyName = "Estado"
            Me.ColumnEstado.HeaderText = "Estado"
            Me.ColumnEstado.Name = "ColumnEstado"
            Me.ColumnEstado.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ColumnEstado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnObservaciones
            '
            Me.ColumnObservaciones.DataPropertyName = "Observaciones"
            Me.ColumnObservaciones.HeaderText = "Observaciones"
            Me.ColumnObservaciones.Name = "ColumnObservaciones"
            '
            'ColumnFechaRecepcionRespuesta
            '
            Me.ColumnFechaRecepcionRespuesta.DataPropertyName = "FechaRecepcionRespuesta"
            Me.ColumnFechaRecepcionRespuesta.HeaderText = "Fecha Recepcion Respuesta"
            Me.ColumnFechaRecepcionRespuesta.Name = "ColumnFechaRecepcionRespuesta"
            '
            'EnviarFisicoColumn
            '
            Me.EnviarFisicoColumn.DataPropertyName = "EnviarFisico"
            Me.EnviarFisicoColumn.FalseValue = "0"
            Me.EnviarFisicoColumn.HeaderText = "EnviarFisico"
            Me.EnviarFisicoColumn.Name = "EnviarFisicoColumn"
            Me.EnviarFisicoColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            Me.EnviarFisicoColumn.TrueValue = "1"
            '
            'ColumnReenviarCorreo
            '
            Me.ColumnReenviarCorreo.DataPropertyName = "ReenviarCorreo"
            Me.ColumnReenviarCorreo.HeaderText = "Reenviar Correo"
            Me.ColumnReenviarCorreo.Name = "ColumnReenviarCorreo"
            Me.ColumnReenviarCorreo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            '
            'ColumnActivo
            '
            Me.ColumnActivo.DataPropertyName = "Activo"
            Me.ColumnActivo.HeaderText = "Activo"
            Me.ColumnActivo.Name = "ColumnActivo"
            Me.ColumnActivo.Visible = False
            '
            'FormSeguimientoCorreo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1089, 525)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.GroupBoxParametrosBusqueda)
            Me.Name = "FormSeguimientoCorreo"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Seguimiento Correo"
            Me.GroupBoxParametrosBusqueda.ResumeLayout(False)
            Me.GroupBoxParametrosBusqueda.PerformLayout()
            CType(Me.DataGridViewCorreos, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBoxParametrosBusqueda As System.Windows.Forms.GroupBox
        Friend WithEvents FechaProcesoFinalLabel As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoFinalDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaProcesoDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents lblOT As System.Windows.Forms.Label
        Friend WithEvents OTDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents BuscarFechaButton As System.Windows.Forms.Button
        Friend WithEvents DataGridViewCorreos As System.Windows.Forms.DataGridView
        Friend WithEvents BtnExportar As System.Windows.Forms.Button
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents DataGridViewButtonColumn1 As System.Windows.Forms.DataGridViewButtonColumn
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents TotalRegistrosTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents EstadoRecibidoTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents EstadoRechazadoTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents EstadoEnviadosTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents EstadoProgramadosTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents ESTADODesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents ColumnFechaProceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnIdTrackingMail As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnOT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnExpediente As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Columnfk_Folder As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Columnid_File As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnNombreImagen As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnOficio As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnRadicado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnCorreo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnTipoNotificacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreNotificacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnNumeroGuia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnFechaLog As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FechaEnvio As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnEstado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnObservaciones As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnFechaRecepcionRespuesta As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EnviarFisicoColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents ColumnReenviarCorreo As System.Windows.Forms.DataGridViewButtonColumn
        Friend WithEvents ColumnActivo As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace