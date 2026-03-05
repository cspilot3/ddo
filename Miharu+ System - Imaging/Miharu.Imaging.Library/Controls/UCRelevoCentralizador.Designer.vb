Namespace Controls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class UCRelevoCentralizador
        Inherits System.Windows.Forms.UserControl

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCRelevoCentralizador))
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.SearchPanel = New System.Windows.Forms.Panel()
            Me.cbxActas = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.OficinasDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.BtnReporte = New System.Windows.Forms.Button()
            Me.btnBuscar = New System.Windows.Forms.Button()
            Me.cbxFechaRecaudo = New System.Windows.Forms.ComboBox()
            Me.cbxCiald = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblCiald = New System.Windows.Forms.Label()
            Me.lblOficina = New System.Windows.Forms.Label()
            Me.lblFechaRecaudo = New System.Windows.Forms.Label()
            Me.VentanasPanel = New System.Windows.Forms.Panel()
            Me.DGVRelevo = New System.Windows.Forms.DataGridView()
            Me.NumeroReferencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipoMovimiento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Ciald = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreOficina = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CodigoOficinaDian = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CodigoOficinaBanco = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Monto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Log_Data = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Estado_Relevo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CodigoCiald = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FechaProceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IDTipoMvto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.BtnCambioEstado = New System.Windows.Forms.DataGridViewButtonColumn()
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
            Me.BtnConsultaActas = New System.Windows.Forms.Button()
            Me.BtnAnexos = New System.Windows.Forms.Button()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
            Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
            Me.Panel3 = New System.Windows.Forms.Panel()
            Me.DGVTotalCiald = New System.Windows.Forms.DataGridView()
            Me.FechaRecaudo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CialdTot = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.RegistrosCiald = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Relevados = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Centralizados = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Faltantes = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.LoginLabel = New System.Windows.Forms.Label()
            Me.LoginReferenciaTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.SearchPanel.SuspendLayout()
            CType(Me.DGVRelevo, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer2.Panel1.SuspendLayout()
            Me.SplitContainer2.Panel2.SuspendLayout()
            Me.SplitContainer2.SuspendLayout()
            CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer3.Panel1.SuspendLayout()
            Me.SplitContainer3.Panel2.SuspendLayout()
            Me.SplitContainer3.SuspendLayout()
            Me.Panel3.SuspendLayout()
            CType(Me.DGVTotalCiald, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'SearchPanel
            '
            Me.SearchPanel.BackColor = System.Drawing.SystemColors.Control
            Me.SearchPanel.Controls.Add(Me.cbxActas)
            Me.SearchPanel.Controls.Add(Me.Label1)
            Me.SearchPanel.Controls.Add(Me.OficinasDesktopComboBox)
            Me.SearchPanel.Controls.Add(Me.BtnReporte)
            Me.SearchPanel.Controls.Add(Me.btnBuscar)
            Me.SearchPanel.Controls.Add(Me.cbxFechaRecaudo)
            Me.SearchPanel.Controls.Add(Me.cbxCiald)
            Me.SearchPanel.Controls.Add(Me.lblCiald)
            Me.SearchPanel.Controls.Add(Me.lblOficina)
            Me.SearchPanel.Controls.Add(Me.lblFechaRecaudo)
            Me.SearchPanel.Controls.Add(Me.VentanasPanel)
            Me.SearchPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SearchPanel.Location = New System.Drawing.Point(0, 0)
            Me.SearchPanel.Name = "SearchPanel"
            Me.SearchPanel.Size = New System.Drawing.Size(913, 84)
            Me.SearchPanel.TabIndex = 19
            '
            'cbxActas
            '
            Me.cbxActas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cbxActas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cbxActas.DisabledEnter = False
            Me.cbxActas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxActas.fk_Campo = 0
            Me.cbxActas.fk_Documento = 0
            Me.cbxActas.fk_Validacion = 0
            Me.cbxActas.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cbxActas.FormattingEnabled = True
            Me.cbxActas.Location = New System.Drawing.Point(585, 53)
            Me.cbxActas.Name = "cbxActas"
            Me.cbxActas.Size = New System.Drawing.Size(89, 21)
            Me.cbxActas.TabIndex = 31
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(602, 35)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(38, 15)
            Me.Label1.TabIndex = 32
            Me.Label1.Text = "Acta:"
            '
            'OficinasDesktopComboBox
            '
            Me.OficinasDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OficinasDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OficinasDesktopComboBox.DisabledEnter = False
            Me.OficinasDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OficinasDesktopComboBox.fk_Campo = 0
            Me.OficinasDesktopComboBox.fk_Documento = 0
            Me.OficinasDesktopComboBox.fk_Validacion = 0
            Me.OficinasDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OficinasDesktopComboBox.FormattingEnabled = True
            Me.OficinasDesktopComboBox.Location = New System.Drawing.Point(306, 53)
            Me.OficinasDesktopComboBox.Name = "OficinasDesktopComboBox"
            Me.OficinasDesktopComboBox.Size = New System.Drawing.Size(240, 21)
            Me.OficinasDesktopComboBox.TabIndex = 30
            '
            'BtnReporte
            '
            Me.BtnReporte.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnReporte.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnReporte.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.prepare
            Me.BtnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnReporte.Location = New System.Drawing.Point(773, 35)
            Me.BtnReporte.Name = "BtnReporte"
            Me.BtnReporte.Size = New System.Drawing.Size(137, 39)
            Me.BtnReporte.TabIndex = 26
            Me.BtnReporte.Text = "       Generar       Reporte BEAN"
            Me.BtnReporte.UseVisualStyleBackColor = True
            '
            'btnBuscar
            '
            Me.btnBuscar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnBuscar.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnBuscar
            Me.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBuscar.Location = New System.Drawing.Point(682, 35)
            Me.btnBuscar.Name = "btnBuscar"
            Me.btnBuscar.Size = New System.Drawing.Size(76, 39)
            Me.btnBuscar.TabIndex = 5
            Me.btnBuscar.Text = "Buscar"
            Me.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
            Me.btnBuscar.UseVisualStyleBackColor = True
            '
            'cbxFechaRecaudo
            '
            Me.cbxFechaRecaudo.FormattingEnabled = True
            Me.cbxFechaRecaudo.Location = New System.Drawing.Point(15, 53)
            Me.cbxFechaRecaudo.Name = "cbxFechaRecaudo"
            Me.cbxFechaRecaudo.Size = New System.Drawing.Size(108, 21)
            Me.cbxFechaRecaudo.TabIndex = 1
            '
            'cbxCiald
            '
            Me.cbxCiald.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cbxCiald.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cbxCiald.DisabledEnter = False
            Me.cbxCiald.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxCiald.fk_Campo = 0
            Me.cbxCiald.fk_Documento = 0
            Me.cbxCiald.fk_Validacion = 0
            Me.cbxCiald.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cbxCiald.FormattingEnabled = True
            Me.cbxCiald.Location = New System.Drawing.Point(136, 53)
            Me.cbxCiald.Name = "cbxCiald"
            Me.cbxCiald.Size = New System.Drawing.Size(155, 21)
            Me.cbxCiald.TabIndex = 2
            '
            'lblCiald
            '
            Me.lblCiald.AutoSize = True
            Me.lblCiald.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCiald.Location = New System.Drawing.Point(189, 35)
            Me.lblCiald.Name = "lblCiald"
            Me.lblCiald.Size = New System.Drawing.Size(44, 15)
            Me.lblCiald.TabIndex = 26
            Me.lblCiald.Text = "Ciald:"
            '
            'lblOficina
            '
            Me.lblOficina.AutoSize = True
            Me.lblOficina.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblOficina.Location = New System.Drawing.Point(378, 35)
            Me.lblOficina.Name = "lblOficina"
            Me.lblOficina.Size = New System.Drawing.Size(56, 15)
            Me.lblOficina.TabIndex = 24
            Me.lblOficina.Text = "Oficina:"
            '
            'lblFechaRecaudo
            '
            Me.lblFechaRecaudo.AutoSize = True
            Me.lblFechaRecaudo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaRecaudo.Location = New System.Drawing.Point(12, 35)
            Me.lblFechaRecaudo.Name = "lblFechaRecaudo"
            Me.lblFechaRecaudo.Size = New System.Drawing.Size(111, 15)
            Me.lblFechaRecaudo.TabIndex = 22
            Me.lblFechaRecaudo.Text = "Fecha Recaudo:"
            '
            'VentanasPanel
            '
            Me.VentanasPanel.BackColor = System.Drawing.Color.RoyalBlue
            Me.VentanasPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.VentanasPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.VentanasPanel.Location = New System.Drawing.Point(0, 0)
            Me.VentanasPanel.Name = "VentanasPanel"
            Me.VentanasPanel.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
            Me.VentanasPanel.Size = New System.Drawing.Size(913, 20)
            Me.VentanasPanel.TabIndex = 21
            '
            'DGVRelevo
            '
            Me.DGVRelevo.AllowUserToAddRows = False
            Me.DGVRelevo.AllowUserToDeleteRows = False
            Me.DGVRelevo.BackgroundColor = System.Drawing.SystemColors.HighlightText
            Me.DGVRelevo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DGVRelevo.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.DGVRelevo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            Me.DGVRelevo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NumeroReferencia, Me.TipoMovimiento, Me.Ciald, Me.NombreOficina, Me.CodigoOficinaDian, Me.CodigoOficinaBanco, Me.Estado, Me.Monto, Me.id_Log_Data, Me.fk_Estado_Relevo, Me.CodigoCiald, Me.FechaProceso, Me.IDTipoMvto, Me.BtnCambioEstado})
            Me.DGVRelevo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DGVRelevo.Location = New System.Drawing.Point(0, 0)
            Me.DGVRelevo.Name = "DGVRelevo"
            Me.DGVRelevo.ReadOnly = True
            DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DGVRelevo.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
            Me.DGVRelevo.Size = New System.Drawing.Size(913, 250)
            Me.DGVRelevo.TabIndex = 0
            '
            'NumeroReferencia
            '
            Me.NumeroReferencia.DataPropertyName = "NumeroReferencia"
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            Me.NumeroReferencia.DefaultCellStyle = DataGridViewCellStyle2
            Me.NumeroReferencia.Frozen = True
            Me.NumeroReferencia.HeaderText = "Número de Referencia"
            Me.NumeroReferencia.Name = "NumeroReferencia"
            Me.NumeroReferencia.ReadOnly = True
            Me.NumeroReferencia.Width = 150
            '
            'TipoMovimiento
            '
            Me.TipoMovimiento.DataPropertyName = "TipoMovimiento"
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            Me.TipoMovimiento.DefaultCellStyle = DataGridViewCellStyle3
            Me.TipoMovimiento.Frozen = True
            Me.TipoMovimiento.HeaderText = "Tipo de Movimiento"
            Me.TipoMovimiento.Name = "TipoMovimiento"
            Me.TipoMovimiento.ReadOnly = True
            Me.TipoMovimiento.Width = 150
            '
            'Ciald
            '
            Me.Ciald.DataPropertyName = "Ciald"
            Me.Ciald.Frozen = True
            Me.Ciald.HeaderText = "Ciald"
            Me.Ciald.Name = "Ciald"
            Me.Ciald.ReadOnly = True
            '
            'NombreOficina
            '
            Me.NombreOficina.DataPropertyName = "NombreOficina"
            Me.NombreOficina.Frozen = True
            Me.NombreOficina.HeaderText = "Oficina"
            Me.NombreOficina.Name = "NombreOficina"
            Me.NombreOficina.ReadOnly = True
            Me.NombreOficina.Width = 200
            '
            'CodigoOficinaDian
            '
            Me.CodigoOficinaDian.DataPropertyName = "CodigoOficinaDian"
            DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            Me.CodigoOficinaDian.DefaultCellStyle = DataGridViewCellStyle4
            Me.CodigoOficinaDian.Frozen = True
            Me.CodigoOficinaDian.HeaderText = "Código Dian"
            Me.CodigoOficinaDian.Name = "CodigoOficinaDian"
            Me.CodigoOficinaDian.ReadOnly = True
            '
            'CodigoOficinaBanco
            '
            Me.CodigoOficinaBanco.DataPropertyName = "CodigoOficinaBanco"
            DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            Me.CodigoOficinaBanco.DefaultCellStyle = DataGridViewCellStyle5
            Me.CodigoOficinaBanco.Frozen = True
            Me.CodigoOficinaBanco.HeaderText = "Código Banco"
            Me.CodigoOficinaBanco.Name = "CodigoOficinaBanco"
            Me.CodigoOficinaBanco.ReadOnly = True
            '
            'Estado
            '
            Me.Estado.DataPropertyName = "Estado"
            DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            Me.Estado.DefaultCellStyle = DataGridViewCellStyle6
            Me.Estado.Frozen = True
            Me.Estado.HeaderText = "Estado"
            Me.Estado.Name = "Estado"
            Me.Estado.ReadOnly = True
            '
            'Monto
            '
            Me.Monto.Frozen = True
            Me.Monto.HeaderText = "Monto"
            Me.Monto.Name = "Monto"
            Me.Monto.ReadOnly = True
            Me.Monto.Visible = False
            '
            'id_Log_Data
            '
            Me.id_Log_Data.Frozen = True
            Me.id_Log_Data.HeaderText = "id_Log_Data"
            Me.id_Log_Data.Name = "id_Log_Data"
            Me.id_Log_Data.ReadOnly = True
            Me.id_Log_Data.Visible = False
            '
            'fk_Estado_Relevo
            '
            Me.fk_Estado_Relevo.Frozen = True
            Me.fk_Estado_Relevo.HeaderText = "fk_Estado_Relevo"
            Me.fk_Estado_Relevo.Name = "fk_Estado_Relevo"
            Me.fk_Estado_Relevo.ReadOnly = True
            Me.fk_Estado_Relevo.Visible = False
            '
            'CodigoCiald
            '
            Me.CodigoCiald.Frozen = True
            Me.CodigoCiald.HeaderText = "CodigoCiald"
            Me.CodigoCiald.Name = "CodigoCiald"
            Me.CodigoCiald.ReadOnly = True
            Me.CodigoCiald.Visible = False
            '
            'FechaProceso
            '
            Me.FechaProceso.HeaderText = "FechaProceso"
            Me.FechaProceso.Name = "FechaProceso"
            Me.FechaProceso.ReadOnly = True
            Me.FechaProceso.Visible = False
            '
            'IDTipoMvto
            '
            Me.IDTipoMvto.HeaderText = "IDTipoMvto"
            Me.IDTipoMvto.Name = "IDTipoMvto"
            Me.IDTipoMvto.ReadOnly = True
            Me.IDTipoMvto.Visible = False
            '
            'BtnCambioEstado
            '
            DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnCambioEstado.DefaultCellStyle = DataGridViewCellStyle7
            Me.BtnCambioEstado.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.BtnCambioEstado.HeaderText = "Cambiar Estado"
            Me.BtnCambioEstado.Name = "BtnCambioEstado"
            Me.BtnCambioEstado.ReadOnly = True
            Me.BtnCambioEstado.Text = "Pasar a Faltante"
            Me.BtnCambioEstado.ToolTipText = "Cambiar estado del registro a faltante"
            Me.BtnCambioEstado.Width = 120
            '
            'SplitContainer1
            '
            Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer1.Name = "SplitContainer1"
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.BtnConsultaActas)
            Me.SplitContainer1.Panel1.Controls.Add(Me.BtnAnexos)
            Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
            Me.SplitContainer1.Size = New System.Drawing.Size(1060, 543)
            Me.SplitContainer1.SplitterDistance = 143
            Me.SplitContainer1.TabIndex = 20
            '
            'BtnConsultaActas
            '
            Me.BtnConsultaActas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnConsultaActas.Image = CType(resources.GetObject("BtnConsultaActas.Image"), System.Drawing.Image)
            Me.BtnConsultaActas.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.BtnConsultaActas.Location = New System.Drawing.Point(14, 187)
            Me.BtnConsultaActas.Name = "BtnConsultaActas"
            Me.BtnConsultaActas.Size = New System.Drawing.Size(111, 91)
            Me.BtnConsultaActas.TabIndex = 28
            Me.BtnConsultaActas.Text = "Consultar Actas"
            Me.BtnConsultaActas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
            Me.BtnConsultaActas.UseVisualStyleBackColor = True
            '
            'BtnAnexos
            '
            Me.BtnAnexos.BackColor = System.Drawing.SystemColors.Control
            Me.BtnAnexos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnAnexos.Image = CType(resources.GetObject("BtnAnexos.Image"), System.Drawing.Image)
            Me.BtnAnexos.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.BtnAnexos.Location = New System.Drawing.Point(15, 35)
            Me.BtnAnexos.Name = "BtnAnexos"
            Me.BtnAnexos.Size = New System.Drawing.Size(111, 93)
            Me.BtnAnexos.TabIndex = 27
            Me.BtnAnexos.Text = "Generar Anexos"
            Me.BtnAnexos.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.BtnAnexos.UseVisualStyleBackColor = False
            '
            'Panel1
            '
            Me.Panel1.BackColor = System.Drawing.Color.RoyalBlue
            Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
            Me.Panel1.Size = New System.Drawing.Size(143, 20)
            Me.Panel1.TabIndex = 22
            '
            'SplitContainer2
            '
            Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer2.Name = "SplitContainer2"
            Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer2.Panel1
            '
            Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer3)
            '
            'SplitContainer2.Panel2
            '
            Me.SplitContainer2.Panel2.Controls.Add(Me.Panel3)
            Me.SplitContainer2.Panel2.Controls.Add(Me.LoginLabel)
            Me.SplitContainer2.Panel2.Controls.Add(Me.LoginReferenciaTextBox)
            Me.SplitContainer2.Panel2.Controls.Add(Me.Panel2)
            Me.SplitContainer2.Panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.SplitContainer2.Size = New System.Drawing.Size(913, 543)
            Me.SplitContainer2.SplitterDistance = 338
            Me.SplitContainer2.TabIndex = 0
            '
            'SplitContainer3
            '
            Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer3.Name = "SplitContainer3"
            Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer3.Panel1
            '
            Me.SplitContainer3.Panel1.Controls.Add(Me.SearchPanel)
            '
            'SplitContainer3.Panel2
            '
            Me.SplitContainer3.Panel2.Controls.Add(Me.DGVRelevo)
            Me.SplitContainer3.Size = New System.Drawing.Size(913, 338)
            Me.SplitContainer3.SplitterDistance = 84
            Me.SplitContainer3.TabIndex = 1
            '
            'Panel3
            '
            Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Panel3.Controls.Add(Me.DGVTotalCiald)
            Me.Panel3.Location = New System.Drawing.Point(235, 21)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(566, 178)
            Me.Panel3.TabIndex = 27
            '
            'DGVTotalCiald
            '
            Me.DGVTotalCiald.BackgroundColor = System.Drawing.SystemColors.Window
            Me.DGVTotalCiald.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DGVTotalCiald.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FechaRecaudo, Me.CialdTot, Me.RegistrosCiald, Me.Relevados, Me.Centralizados, Me.Faltantes})
            Me.DGVTotalCiald.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DGVTotalCiald.Location = New System.Drawing.Point(0, 0)
            Me.DGVTotalCiald.Name = "DGVTotalCiald"
            Me.DGVTotalCiald.ReadOnly = True
            Me.DGVTotalCiald.Size = New System.Drawing.Size(566, 178)
            Me.DGVTotalCiald.TabIndex = 28
            '
            'FechaRecaudo
            '
            Me.FechaRecaudo.HeaderText = "Fecha Recaudo"
            Me.FechaRecaudo.Name = "FechaRecaudo"
            Me.FechaRecaudo.ReadOnly = True
            Me.FechaRecaudo.Width = 110
            '
            'CialdTot
            '
            Me.CialdTot.HeaderText = "Nombre Ciald"
            Me.CialdTot.Name = "CialdTot"
            Me.CialdTot.ReadOnly = True
            Me.CialdTot.Width = 200
            '
            'RegistrosCiald
            '
            Me.RegistrosCiald.HeaderText = "Registros Ciald"
            Me.RegistrosCiald.Name = "RegistrosCiald"
            Me.RegistrosCiald.ReadOnly = True
            Me.RegistrosCiald.Width = 80
            '
            'Relevados
            '
            Me.Relevados.HeaderText = "Relevados"
            Me.Relevados.Name = "Relevados"
            Me.Relevados.ReadOnly = True
            Me.Relevados.Width = 80
            '
            'Centralizados
            '
            Me.Centralizados.HeaderText = "Centralizados"
            Me.Centralizados.Name = "Centralizados"
            Me.Centralizados.ReadOnly = True
            Me.Centralizados.Width = 80
            '
            'Faltantes
            '
            Me.Faltantes.HeaderText = "Faltantes"
            Me.Faltantes.Name = "Faltantes"
            Me.Faltantes.ReadOnly = True
            Me.Faltantes.Width = 80
            '
            'LoginLabel
            '
            Me.LoginLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LoginLabel.Location = New System.Drawing.Point(12, 36)
            Me.LoginLabel.Name = "LoginLabel"
            Me.LoginLabel.Size = New System.Drawing.Size(195, 16)
            Me.LoginLabel.TabIndex = 25
            Me.LoginLabel.Text = "Numero de Referencia Ingresado"
            '
            'LoginReferenciaTextBox
            '
            Me.LoginReferenciaTextBox._Obligatorio = False
            Me.LoginReferenciaTextBox._PermitePegar = False
            Me.LoginReferenciaTextBox.Cantidad_Decimales = CType(0, Short)
            Me.LoginReferenciaTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.LoginReferenciaTextBox.DateFormat = Nothing
            Me.LoginReferenciaTextBox.DisabledEnter = False
            Me.LoginReferenciaTextBox.DisabledTab = False
            Me.LoginReferenciaTextBox.EnabledShortCuts = False
            Me.LoginReferenciaTextBox.fk_Campo = 0
            Me.LoginReferenciaTextBox.fk_Documento = 0
            Me.LoginReferenciaTextBox.fk_Validacion = 0
            Me.LoginReferenciaTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.LoginReferenciaTextBox.FocusOut = System.Drawing.Color.White
            Me.LoginReferenciaTextBox.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
            Me.LoginReferenciaTextBox.Location = New System.Drawing.Point(13, 55)
            Me.LoginReferenciaTextBox.MaskedTextBox_Property = ""
            Me.LoginReferenciaTextBox.MaximumLength = CType(0, Short)
            Me.LoginReferenciaTextBox.MinimumLength = CType(0, Short)
            Me.LoginReferenciaTextBox.Name = "LoginReferenciaTextBox"
            Me.LoginReferenciaTextBox.Obligatorio = False
            Me.LoginReferenciaTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0R
            Me.LoginReferenciaTextBox.Rango = Rango1
            Me.LoginReferenciaTextBox.Size = New System.Drawing.Size(195, 26)
            Me.LoginReferenciaTextBox.TabIndex = 24
            Me.LoginReferenciaTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.LoginReferenciaTextBox.Usa_Decimales = False
            Me.LoginReferenciaTextBox.Validos_Cantidad_Puntos = False
            '
            'Panel2
            '
            Me.Panel2.BackColor = System.Drawing.Color.RoyalBlue
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.Label2)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(0, 0)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
            Me.Panel2.Size = New System.Drawing.Size(913, 20)
            Me.Panel2.TabIndex = 23
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.ForeColor = System.Drawing.SystemColors.ControlLightLight
            Me.Label2.Location = New System.Drawing.Point(345, 2)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(198, 15)
            Me.Label2.TabIndex = 28
            Me.Label2.Text = "SEGUIMIENTO TOTALIZADOR"
            '
            'UCRelevoCentralizador
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.SplitContainer1)
            Me.Name = "UCRelevoCentralizador"
            Me.Size = New System.Drawing.Size(1060, 543)
            Me.SearchPanel.ResumeLayout(False)
            Me.SearchPanel.PerformLayout()
            CType(Me.DGVRelevo, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.ResumeLayout(False)
            Me.SplitContainer2.Panel1.ResumeLayout(False)
            Me.SplitContainer2.Panel2.ResumeLayout(False)
            Me.SplitContainer2.Panel2.PerformLayout()
            CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer2.ResumeLayout(False)
            Me.SplitContainer3.Panel1.ResumeLayout(False)
            Me.SplitContainer3.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer3.ResumeLayout(False)
            Me.Panel3.ResumeLayout(False)
            CType(Me.DGVTotalCiald, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents SearchPanel As Panel
        Friend WithEvents lblFechaRecaudo As Label
        Friend WithEvents lblOficina As Label
        Friend WithEvents cbxCiald As Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents lblCiald As Label
        Friend WithEvents cbxFechaRecaudo As ComboBox
        Friend WithEvents DGVRelevo As DataGridView
        Friend WithEvents SplitContainer1 As SplitContainer
        Friend WithEvents SplitContainer2 As SplitContainer
        Friend WithEvents btnBuscar As Button
        Friend WithEvents SplitContainer3 As SplitContainer
        Friend WithEvents VentanasPanel As Panel
        Friend WithEvents Panel1 As Panel
        Friend WithEvents Panel2 As Panel
        Friend WithEvents OficinasDesktopComboBox As Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents LoginLabel As Label
        Friend WithEvents LoginReferenciaTextBox As Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents BtnReporte As Button
        Friend WithEvents Panel3 As Panel
        Friend WithEvents DGVTotalCiald As DataGridView
        Friend WithEvents BtnAnexos As Button
        Friend WithEvents FechaRecaudo As DataGridViewTextBoxColumn
        Friend WithEvents CialdTot As DataGridViewTextBoxColumn
        Friend WithEvents RegistrosCiald As DataGridViewTextBoxColumn
        Friend WithEvents Relevados As DataGridViewTextBoxColumn
        Friend WithEvents Centralizados As DataGridViewTextBoxColumn
        Friend WithEvents Faltantes As DataGridViewTextBoxColumn
        Friend WithEvents BtnConsultaActas As Button
        Friend WithEvents cbxActas As Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label1 As Label
        Friend WithEvents NumeroReferencia As DataGridViewTextBoxColumn
        Friend WithEvents TipoMovimiento As DataGridViewTextBoxColumn
        Friend WithEvents Ciald As DataGridViewTextBoxColumn
        Friend WithEvents NombreOficina As DataGridViewTextBoxColumn
        Friend WithEvents CodigoOficinaDian As DataGridViewTextBoxColumn
        Friend WithEvents CodigoOficinaBanco As DataGridViewTextBoxColumn
        Friend WithEvents Estado As DataGridViewTextBoxColumn
        Friend WithEvents Monto As DataGridViewTextBoxColumn
        Friend WithEvents id_Log_Data As DataGridViewTextBoxColumn
        Friend WithEvents fk_Estado_Relevo As DataGridViewTextBoxColumn
        Friend WithEvents CodigoCiald As DataGridViewTextBoxColumn
        Friend WithEvents FechaProceso As DataGridViewTextBoxColumn
        Friend WithEvents IDTipoMvto As DataGridViewTextBoxColumn
        Friend WithEvents BtnCambioEstado As DataGridViewButtonColumn
        Friend WithEvents Label2 As Label
    End Class
End Namespace