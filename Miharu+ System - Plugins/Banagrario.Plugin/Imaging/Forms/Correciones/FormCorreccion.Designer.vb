Imports Miharu.Desktop.Controls.DesktopTab
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Imaging.Forms.Correciones
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCorreccion
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCorreccion))
            Me.GetErrorLlavesDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.GetErrorLlaves_DataSet = New Banagrario.Plugin.GetErrorLlaves_DataSet()
            Me.ConsultaTab = New System.Windows.Forms.TabPage()
            Me.ResultadoGroupBox = New System.Windows.Forms.GroupBox()
            Me.DatosDataGridView = New System.Windows.Forms.DataGridView()
            Me.fk_Proceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Cob = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Codigo_Contenedor = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Oficina = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Oficina = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fecha_Movimiento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Cargue = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Cargue_Paquete = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Cant_Soporte_Sobrantes = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.token = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FiltroGroupBox = New System.Windows.Forms.GroupBox()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.FechaProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ActualizarButton = New System.Windows.Forms.Button()
            Me.FechaProcesolabel = New System.Windows.Forms.Label()
            Me.CorreccionesDesktopTabControl = New Miharu.Desktop.Controls.DesktopTab.DesktopTabControl()
            Me.CorreccionTab = New System.Windows.Forms.TabPage()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.CorreccionGroupBox = New System.Windows.Forms.GroupBox()
            Me.OrigenErrorPanel = New System.Windows.Forms.Panel()
            Me.LblOrigenError = New System.Windows.Forms.Label()
            Me.RdbFuncionarioBanco = New System.Windows.Forms.RadioButton()
            Me.RdbFuncionariopyc = New System.Windows.Forms.RadioButton()
            Me.CambioLLavesPanel = New System.Windows.Forms.Panel()
            Me.CodigoContenedorTextBox = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.LblNuevaFechaMovimiento = New System.Windows.Forms.Label()
            Me.LblNuevoCodigoOficina = New System.Windows.Forms.Label()
            Me.FechaMovimientoDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.OficinaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.TipoCambioPanel = New System.Windows.Forms.Panel()
            Me.CBContenedor = New System.Windows.Forms.CheckBox()
            Me.LblTipoCambio = New System.Windows.Forms.Label()
            Me.CBFechaMovimiento = New System.Windows.Forms.CheckBox()
            Me.CBCodigoOficina = New System.Windows.Forms.CheckBox()
            Me.LblCodOfiAnterior = New System.Windows.Forms.Label()
            Me.LblFecMovAnterior = New System.Windows.Forms.Label()
            Me.Lblfk_Proceso = New System.Windows.Forms.Label()
            Me.BtnGuardar = New System.Windows.Forms.Button()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.CargueLabel = New System.Windows.Forms.Label()
            Me.CruzarButton = New System.Windows.Forms.Button()
            Me.LblValorContenedor = New System.Windows.Forms.Label()
            Me.LblValorCargue = New System.Windows.Forms.Label()
            Me.LblValorPaquete = New System.Windows.Forms.Label()
            Me.PaqueteLabel = New System.Windows.Forms.Label()
            Me.ContenedorLabel = New System.Windows.Forms.Label()
            CType(Me.GetErrorLlavesDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GetErrorLlaves_DataSet, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ConsultaTab.SuspendLayout()
            Me.ResultadoGroupBox.SuspendLayout()
            CType(Me.DatosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.FiltroGroupBox.SuspendLayout()
            Me.CorreccionesDesktopTabControl.SuspendLayout()
            Me.CorreccionTab.SuspendLayout()
            Me.Panel1.SuspendLayout()
            Me.CorreccionGroupBox.SuspendLayout()
            Me.OrigenErrorPanel.SuspendLayout()
            Me.CambioLLavesPanel.SuspendLayout()
            Me.TipoCambioPanel.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.SuspendLayout()
            '
            'GetErrorLlavesDataSetBindingSource
            '
            Me.GetErrorLlavesDataSetBindingSource.DataSource = Me.GetErrorLlaves_DataSet
            Me.GetErrorLlavesDataSetBindingSource.Position = 0
            '
            'GetErrorLlaves_DataSet
            '
            Me.GetErrorLlaves_DataSet.DataSetName = "GetErrorLlaves_DataSet"
            Me.GetErrorLlaves_DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'ConsultaTab
            '
            Me.ConsultaTab.Controls.Add(Me.ResultadoGroupBox)
            Me.ConsultaTab.Controls.Add(Me.FiltroGroupBox)
            Me.ConsultaTab.Location = New System.Drawing.Point(4, 22)
            Me.ConsultaTab.Name = "ConsultaTab"
            Me.ConsultaTab.Padding = New System.Windows.Forms.Padding(3)
            Me.ConsultaTab.Size = New System.Drawing.Size(591, 430)
            Me.ConsultaTab.TabIndex = 0
            Me.ConsultaTab.Text = "Consulta"
            Me.ConsultaTab.UseVisualStyleBackColor = True
            '
            'ResultadoGroupBox
            '
            Me.ResultadoGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ResultadoGroupBox.BackColor = System.Drawing.SystemColors.Control
            Me.ResultadoGroupBox.Controls.Add(Me.DatosDataGridView)
            Me.ResultadoGroupBox.Location = New System.Drawing.Point(2, 57)
            Me.ResultadoGroupBox.Name = "ResultadoGroupBox"
            Me.ResultadoGroupBox.Size = New System.Drawing.Size(586, 366)
            Me.ResultadoGroupBox.TabIndex = 2
            Me.ResultadoGroupBox.TabStop = False
            Me.ResultadoGroupBox.Text = "Resultado Consulta "
            '
            'DatosDataGridView
            '
            Me.DatosDataGridView.AllowUserToAddRows = False
            Me.DatosDataGridView.AllowUserToDeleteRows = False
            Me.DatosDataGridView.AutoGenerateColumns = False
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DatosDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.DatosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DatosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fk_Proceso, Me.Nombre_Cob, Me.Codigo_Contenedor, Me.fk_Oficina, Me.Nombre_Oficina, Me.fecha_Movimiento, Me.fk_Cargue, Me.fk_Cargue_Paquete, Me.Cant_Soporte_Sobrantes, Me.token})
            Me.DatosDataGridView.DataSource = Me.GetErrorLlavesDataSetBindingSource
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DatosDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.DatosDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DatosDataGridView.Location = New System.Drawing.Point(3, 16)
            Me.DatosDataGridView.Name = "DatosDataGridView"
            Me.DatosDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DatosDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.DatosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DatosDataGridView.Size = New System.Drawing.Size(580, 347)
            Me.DatosDataGridView.TabIndex = 3
            '
            'fk_Proceso
            '
            Me.fk_Proceso.DataPropertyName = "fk_Proceso"
            Me.fk_Proceso.HeaderText = "fk_Proceso"
            Me.fk_Proceso.Name = "fk_Proceso"
            Me.fk_Proceso.ReadOnly = True
            Me.fk_Proceso.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            '
            'Nombre_Cob
            '
            Me.Nombre_Cob.DataPropertyName = "Nombre_Cob"
            Me.Nombre_Cob.HeaderText = "COB"
            Me.Nombre_Cob.Name = "Nombre_Cob"
            Me.Nombre_Cob.ReadOnly = True
            '
            'Codigo_Contenedor
            '
            Me.Codigo_Contenedor.DataPropertyName = "Codigo_Contenedor"
            Me.Codigo_Contenedor.HeaderText = "Contenedor"
            Me.Codigo_Contenedor.Name = "Codigo_Contenedor"
            Me.Codigo_Contenedor.ReadOnly = True
            '
            'fk_Oficina
            '
            Me.fk_Oficina.DataPropertyName = "fk_Oficina"
            Me.fk_Oficina.HeaderText = "Oficina"
            Me.fk_Oficina.Name = "fk_Oficina"
            Me.fk_Oficina.ReadOnly = True
            Me.fk_Oficina.Width = 90
            '
            'Nombre_Oficina
            '
            Me.Nombre_Oficina.DataPropertyName = "Nombre_Oficina"
            Me.Nombre_Oficina.HeaderText = "Nombre Oficina"
            Me.Nombre_Oficina.Name = "Nombre_Oficina"
            Me.Nombre_Oficina.ReadOnly = True
            Me.Nombre_Oficina.Width = 330
            '
            'fecha_Movimiento
            '
            Me.fecha_Movimiento.DataPropertyName = "Fecha_Movimiento"
            Me.fecha_Movimiento.HeaderText = "Fecha Movimiento"
            Me.fecha_Movimiento.Name = "fecha_Movimiento"
            Me.fecha_Movimiento.ReadOnly = True
            Me.fecha_Movimiento.Width = 120
            '
            'fk_Cargue
            '
            Me.fk_Cargue.DataPropertyName = "fk_Cargue"
            Me.fk_Cargue.HeaderText = "Cargue"
            Me.fk_Cargue.Name = "fk_Cargue"
            Me.fk_Cargue.ReadOnly = True
            '
            'fk_Cargue_Paquete
            '
            Me.fk_Cargue_Paquete.DataPropertyName = "fk_Cargue_Paquete"
            Me.fk_Cargue_Paquete.HeaderText = "Paquete"
            Me.fk_Cargue_Paquete.Name = "fk_Cargue_Paquete"
            Me.fk_Cargue_Paquete.ReadOnly = True
            '
            'Cant_Soporte_Sobrantes
            '
            Me.Cant_Soporte_Sobrantes.DataPropertyName = "Cant_Soporte_Sobrantes"
            Me.Cant_Soporte_Sobrantes.HeaderText = "Cant. Soportes Sobrantes"
            Me.Cant_Soporte_Sobrantes.Name = "Cant_Soporte_Sobrantes"
            Me.Cant_Soporte_Sobrantes.ReadOnly = True
            Me.Cant_Soporte_Sobrantes.Width = 160
            '
            'token
            '
            Me.token.DataPropertyName = "token"
            Me.token.HeaderText = "Token Anexo 23"
            Me.token.Name = "token"
            Me.token.ReadOnly = True
            Me.token.Width = 200
            '
            'FiltroGroupBox
            '
            Me.FiltroGroupBox.BackColor = System.Drawing.SystemColors.Control
            Me.FiltroGroupBox.Controls.Add(Me.BuscarButton)
            Me.FiltroGroupBox.Controls.Add(Me.FechaProcesoDateTimePicker)
            Me.FiltroGroupBox.Controls.Add(Me.Label1)
            Me.FiltroGroupBox.Controls.Add(Me.ActualizarButton)
            Me.FiltroGroupBox.Controls.Add(Me.FechaProcesolabel)
            Me.FiltroGroupBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.FiltroGroupBox.Location = New System.Drawing.Point(3, 3)
            Me.FiltroGroupBox.Name = "FiltroGroupBox"
            Me.FiltroGroupBox.Size = New System.Drawing.Size(585, 52)
            Me.FiltroGroupBox.TabIndex = 1
            Me.FiltroGroupBox.TabStop = False
            Me.FiltroGroupBox.Text = "Filtro Consulta "
            '
            'BuscarButton
            '
            Me.BuscarButton.BackgroundImage = Global.Banagrario.Plugin.My.Resources.Resources.btnBuscar
            Me.BuscarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.BuscarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(322, 12)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(99, 31)
            Me.BuscarButton.TabIndex = 28
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'FechaProcesoDateTimePicker
            '
            Me.FechaProcesoDateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaProcesoDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaProcesoDateTimePicker.Location = New System.Drawing.Point(227, 17)
            Me.FechaProcesoDateTimePicker.Name = "FechaProcesoDateTimePicker"
            Me.FechaProcesoDateTimePicker.Size = New System.Drawing.Size(84, 20)
            Me.FechaProcesoDateTimePicker.TabIndex = 27
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(115, 20)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(96, 13)
            Me.Label1.TabIndex = 26
            Me.Label1.Text = "Fecha Proceso:"
            '
            'ActualizarButton
            '
            Me.ActualizarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ActualizarButton.BackgroundImage = Global.Banagrario.Plugin.My.Resources.Resources.btnInsert
            Me.ActualizarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.ActualizarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ActualizarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.ActualizarButton.Location = New System.Drawing.Point(453, 13)
            Me.ActualizarButton.Name = "ActualizarButton"
            Me.ActualizarButton.Size = New System.Drawing.Size(99, 33)
            Me.ActualizarButton.TabIndex = 25
            Me.ActualizarButton.Text = "Actualizar"
            Me.ActualizarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ActualizarButton.UseVisualStyleBackColor = True
            '
            'FechaProcesolabel
            '
            Me.FechaProcesolabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesolabel.AutoSize = True
            Me.FechaProcesolabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesolabel.Location = New System.Drawing.Point(-138, -161)
            Me.FechaProcesolabel.Name = "FechaProcesolabel"
            Me.FechaProcesolabel.Size = New System.Drawing.Size(96, 13)
            Me.FechaProcesolabel.TabIndex = 23
            Me.FechaProcesolabel.Text = "Fecha Proceso:"
            '
            'CorreccionesDesktopTabControl
            '
            Me.CorreccionesDesktopTabControl.Controls.Add(Me.ConsultaTab)
            Me.CorreccionesDesktopTabControl.Controls.Add(Me.CorreccionTab)
            Me.CorreccionesDesktopTabControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CorreccionesDesktopTabControl.Location = New System.Drawing.Point(0, 0)
            Me.CorreccionesDesktopTabControl.Name = "CorreccionesDesktopTabControl"
            Me.CorreccionesDesktopTabControl.SelectedIndex = 0
            Me.CorreccionesDesktopTabControl.Size = New System.Drawing.Size(599, 456)
            Me.CorreccionesDesktopTabControl.TabIndex = 0
            '
            'CorreccionTab
            '
            Me.CorreccionTab.Controls.Add(Me.Panel1)
            Me.CorreccionTab.Location = New System.Drawing.Point(4, 22)
            Me.CorreccionTab.Name = "CorreccionTab"
            Me.CorreccionTab.Padding = New System.Windows.Forms.Padding(3)
            Me.CorreccionTab.Size = New System.Drawing.Size(583, 418)
            Me.CorreccionTab.TabIndex = 1
            Me.CorreccionTab.Text = "Corrección Llaves"
            Me.CorreccionTab.UseVisualStyleBackColor = True
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.CorreccionGroupBox)
            Me.Panel1.Controls.Add(Me.GroupBox2)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel1.Location = New System.Drawing.Point(3, 3)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(577, 412)
            Me.Panel1.TabIndex = 4
            '
            'CorreccionGroupBox
            '
            Me.CorreccionGroupBox.BackColor = System.Drawing.SystemColors.Control
            Me.CorreccionGroupBox.Controls.Add(Me.OrigenErrorPanel)
            Me.CorreccionGroupBox.Controls.Add(Me.CambioLLavesPanel)
            Me.CorreccionGroupBox.Controls.Add(Me.TipoCambioPanel)
            Me.CorreccionGroupBox.Controls.Add(Me.LblCodOfiAnterior)
            Me.CorreccionGroupBox.Controls.Add(Me.LblFecMovAnterior)
            Me.CorreccionGroupBox.Controls.Add(Me.Lblfk_Proceso)
            Me.CorreccionGroupBox.Controls.Add(Me.BtnGuardar)
            Me.CorreccionGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CorreccionGroupBox.Location = New System.Drawing.Point(0, 102)
            Me.CorreccionGroupBox.Name = "CorreccionGroupBox"
            Me.CorreccionGroupBox.Size = New System.Drawing.Size(577, 310)
            Me.CorreccionGroupBox.TabIndex = 3
            Me.CorreccionGroupBox.TabStop = False
            Me.CorreccionGroupBox.Text = "Corrección"
            '
            'OrigenErrorPanel
            '
            Me.OrigenErrorPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OrigenErrorPanel.Controls.Add(Me.LblOrigenError)
            Me.OrigenErrorPanel.Controls.Add(Me.RdbFuncionarioBanco)
            Me.OrigenErrorPanel.Controls.Add(Me.RdbFuncionariopyc)
            Me.OrigenErrorPanel.Location = New System.Drawing.Point(8, 204)
            Me.OrigenErrorPanel.Name = "OrigenErrorPanel"
            Me.OrigenErrorPanel.Size = New System.Drawing.Size(562, 64)
            Me.OrigenErrorPanel.TabIndex = 44
            '
            'LblOrigenError
            '
            Me.LblOrigenError.AutoSize = True
            Me.LblOrigenError.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.LblOrigenError.Location = New System.Drawing.Point(12, 13)
            Me.LblOrigenError.Name = "LblOrigenError"
            Me.LblOrigenError.Size = New System.Drawing.Size(104, 13)
            Me.LblOrigenError.TabIndex = 13
            Me.LblOrigenError.Text = "Origen del Error: "
            '
            'RdbFuncionarioBanco
            '
            Me.RdbFuncionarioBanco.AutoSize = True
            Me.RdbFuncionarioBanco.Checked = True
            Me.RdbFuncionarioBanco.Location = New System.Drawing.Point(198, 9)
            Me.RdbFuncionarioBanco.Name = "RdbFuncionarioBanco"
            Me.RdbFuncionarioBanco.Size = New System.Drawing.Size(114, 17)
            Me.RdbFuncionarioBanco.TabIndex = 14
            Me.RdbFuncionarioBanco.TabStop = True
            Me.RdbFuncionarioBanco.Text = "Funcionario Banco"
            Me.RdbFuncionarioBanco.UseVisualStyleBackColor = True
            '
            'RdbFuncionariopyc
            '
            Me.RdbFuncionariopyc.AutoSize = True
            Me.RdbFuncionariopyc.Location = New System.Drawing.Point(198, 33)
            Me.RdbFuncionariopyc.Name = "RdbFuncionariopyc"
            Me.RdbFuncionariopyc.Size = New System.Drawing.Size(104, 17)
            Me.RdbFuncionariopyc.TabIndex = 15
            Me.RdbFuncionariopyc.Text = "Funcionario PYC"
            Me.RdbFuncionariopyc.UseVisualStyleBackColor = True
            '
            'CambioLLavesPanel
            '
            Me.CambioLLavesPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CambioLLavesPanel.Controls.Add(Me.CodigoContenedorTextBox)
            Me.CambioLLavesPanel.Controls.Add(Me.Label2)
            Me.CambioLLavesPanel.Controls.Add(Me.LblNuevaFechaMovimiento)
            Me.CambioLLavesPanel.Controls.Add(Me.LblNuevoCodigoOficina)
            Me.CambioLLavesPanel.Controls.Add(Me.FechaMovimientoDateTimePicker)
            Me.CambioLLavesPanel.Controls.Add(Me.OficinaDesktopComboBox)
            Me.CambioLLavesPanel.Location = New System.Drawing.Point(8, 95)
            Me.CambioLLavesPanel.Name = "CambioLLavesPanel"
            Me.CambioLLavesPanel.Size = New System.Drawing.Size(563, 103)
            Me.CambioLLavesPanel.TabIndex = 43
            '
            'CodigoContenedorTextBox
            '
            Me.CodigoContenedorTextBox.Enabled = False
            Me.CodigoContenedorTextBox.Location = New System.Drawing.Point(199, 65)
            Me.CodigoContenedorTextBox.Name = "CodigoContenedorTextBox"
            Me.CodigoContenedorTextBox.Size = New System.Drawing.Size(206, 20)
            Me.CodigoContenedorTextBox.TabIndex = 32
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.Label2.Location = New System.Drawing.Point(12, 68)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(182, 13)
            Me.Label2.TabIndex = 31
            Me.Label2.Text = "Nuevo Código de Contenedor :"
            '
            'LblNuevaFechaMovimiento
            '
            Me.LblNuevaFechaMovimiento.AutoSize = True
            Me.LblNuevaFechaMovimiento.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.LblNuevaFechaMovimiento.Location = New System.Drawing.Point(12, 12)
            Me.LblNuevaFechaMovimiento.Name = "LblNuevaFechaMovimiento"
            Me.LblNuevaFechaMovimiento.Size = New System.Drawing.Size(181, 13)
            Me.LblNuevaFechaMovimiento.TabIndex = 9
            Me.LblNuevaFechaMovimiento.Text = "Nueva Fecha de Movimiento : "
            '
            'LblNuevoCodigoOficina
            '
            Me.LblNuevoCodigoOficina.AutoSize = True
            Me.LblNuevoCodigoOficina.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.LblNuevoCodigoOficina.Location = New System.Drawing.Point(12, 41)
            Me.LblNuevoCodigoOficina.Name = "LblNuevoCodigoOficina"
            Me.LblNuevoCodigoOficina.Size = New System.Drawing.Size(157, 13)
            Me.LblNuevoCodigoOficina.TabIndex = 11
            Me.LblNuevoCodigoOficina.Text = "Nuevo Código de Oficina :"
            '
            'FechaMovimientoDateTimePicker
            '
            Me.FechaMovimientoDateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaMovimientoDateTimePicker.Enabled = False
            Me.FechaMovimientoDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaMovimientoDateTimePicker.Location = New System.Drawing.Point(198, 12)
            Me.FechaMovimientoDateTimePicker.Name = "FechaMovimientoDateTimePicker"
            Me.FechaMovimientoDateTimePicker.Size = New System.Drawing.Size(81, 20)
            Me.FechaMovimientoDateTimePicker.TabIndex = 29
            '
            'OficinaDesktopComboBox
            '
            Me.OficinaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OficinaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OficinaDesktopComboBox.DisabledEnter = True
            Me.OficinaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OficinaDesktopComboBox.Enabled = False
            Me.OficinaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OficinaDesktopComboBox.FormattingEnabled = True
            Me.OficinaDesktopComboBox.Location = New System.Drawing.Point(198, 38)
            Me.OficinaDesktopComboBox.Name = "OficinaDesktopComboBox"
            Me.OficinaDesktopComboBox.Size = New System.Drawing.Size(206, 21)
            Me.OficinaDesktopComboBox.TabIndex = 30
            '
            'TipoCambioPanel
            '
            Me.TipoCambioPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TipoCambioPanel.Controls.Add(Me.CBContenedor)
            Me.TipoCambioPanel.Controls.Add(Me.LblTipoCambio)
            Me.TipoCambioPanel.Controls.Add(Me.CBFechaMovimiento)
            Me.TipoCambioPanel.Controls.Add(Me.CBCodigoOficina)
            Me.TipoCambioPanel.Location = New System.Drawing.Point(8, 19)
            Me.TipoCambioPanel.Name = "TipoCambioPanel"
            Me.TipoCambioPanel.Size = New System.Drawing.Size(563, 70)
            Me.TipoCambioPanel.TabIndex = 42
            '
            'CBContenedor
            '
            Me.CBContenedor.AutoSize = True
            Me.CBContenedor.Location = New System.Drawing.Point(338, 40)
            Me.CBContenedor.Name = "CBContenedor"
            Me.CBContenedor.Size = New System.Drawing.Size(81, 17)
            Me.CBContenedor.TabIndex = 9
            Me.CBContenedor.Text = "Contenedor"
            Me.CBContenedor.UseVisualStyleBackColor = True
            '
            'LblTipoCambio
            '
            Me.LblTipoCambio.AutoSize = True
            Me.LblTipoCambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.LblTipoCambio.Location = New System.Drawing.Point(12, 17)
            Me.LblTipoCambio.Name = "LblTipoCambio"
            Me.LblTipoCambio.Size = New System.Drawing.Size(103, 13)
            Me.LblTipoCambio.TabIndex = 6
            Me.LblTipoCambio.Text = "Tipo de Cambio: "
            '
            'CBFechaMovimiento
            '
            Me.CBFechaMovimiento.AutoSize = True
            Me.CBFechaMovimiento.Location = New System.Drawing.Point(53, 40)
            Me.CBFechaMovimiento.Name = "CBFechaMovimiento"
            Me.CBFechaMovimiento.Size = New System.Drawing.Size(128, 17)
            Me.CBFechaMovimiento.TabIndex = 7
            Me.CBFechaMovimiento.Text = "Fecha de Movimiento"
            Me.CBFechaMovimiento.UseVisualStyleBackColor = True
            '
            'CBCodigoOficina
            '
            Me.CBCodigoOficina.AutoSize = True
            Me.CBCodigoOficina.Location = New System.Drawing.Point(198, 40)
            Me.CBCodigoOficina.Name = "CBCodigoOficina"
            Me.CBCodigoOficina.Size = New System.Drawing.Size(110, 17)
            Me.CBCodigoOficina.TabIndex = 8
            Me.CBCodigoOficina.Text = "Codigo de Oficina"
            Me.CBCodigoOficina.UseVisualStyleBackColor = True
            '
            'LblCodOfiAnterior
            '
            Me.LblCodOfiAnterior.AutoSize = True
            Me.LblCodOfiAnterior.Location = New System.Drawing.Point(783, 55)
            Me.LblCodOfiAnterior.Name = "LblCodOfiAnterior"
            Me.LblCodOfiAnterior.Size = New System.Drawing.Size(89, 13)
            Me.LblCodOfiAnterior.TabIndex = 41
            Me.LblCodOfiAnterior.Text = "LblCodOfiAnterior"
            Me.LblCodOfiAnterior.Visible = False
            '
            'LblFecMovAnterior
            '
            Me.LblFecMovAnterior.AutoSize = True
            Me.LblFecMovAnterior.Location = New System.Drawing.Point(783, 35)
            Me.LblFecMovAnterior.Name = "LblFecMovAnterior"
            Me.LblFecMovAnterior.Size = New System.Drawing.Size(96, 13)
            Me.LblFecMovAnterior.TabIndex = 40
            Me.LblFecMovAnterior.Text = "LblFecMovAnterior"
            Me.LblFecMovAnterior.Visible = False
            '
            'Lblfk_Proceso
            '
            Me.Lblfk_Proceso.AutoSize = True
            Me.Lblfk_Proceso.Location = New System.Drawing.Point(783, 16)
            Me.Lblfk_Proceso.Name = "Lblfk_Proceso"
            Me.Lblfk_Proceso.Size = New System.Drawing.Size(75, 13)
            Me.Lblfk_Proceso.TabIndex = 39
            Me.Lblfk_Proceso.Text = "Lblfk_Proceso"
            Me.Lblfk_Proceso.Visible = False
            '
            'BtnGuardar
            '
            Me.BtnGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnGuardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.BtnGuardar.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnGuardar
            Me.BtnGuardar.ImageAlign = System.Drawing.ContentAlignment.TopLeft
            Me.BtnGuardar.Location = New System.Drawing.Point(489, 276)
            Me.BtnGuardar.Name = "BtnGuardar"
            Me.BtnGuardar.Size = New System.Drawing.Size(82, 28)
            Me.BtnGuardar.TabIndex = 38
            Me.BtnGuardar.Text = "Guardar"
            Me.BtnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BtnGuardar.UseVisualStyleBackColor = True
            '
            'GroupBox2
            '
            Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
            Me.GroupBox2.Controls.Add(Me.CargueLabel)
            Me.GroupBox2.Controls.Add(Me.CruzarButton)
            Me.GroupBox2.Controls.Add(Me.LblValorContenedor)
            Me.GroupBox2.Controls.Add(Me.LblValorCargue)
            Me.GroupBox2.Controls.Add(Me.LblValorPaquete)
            Me.GroupBox2.Controls.Add(Me.PaqueteLabel)
            Me.GroupBox2.Controls.Add(Me.ContenedorLabel)
            Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
            Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(577, 102)
            Me.GroupBox2.TabIndex = 2
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Datos Contenedor - Cargue"
            '
            'CargueLabel
            '
            Me.CargueLabel.AutoSize = True
            Me.CargueLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CargueLabel.Location = New System.Drawing.Point(6, 42)
            Me.CargueLabel.Name = "CargueLabel"
            Me.CargueLabel.Size = New System.Drawing.Size(51, 13)
            Me.CargueLabel.TabIndex = 46
            Me.CargueLabel.Text = "Cargue:"
            '
            'CruzarButton
            '
            Me.CruzarButton.AccessibleDescription = ""
            Me.CruzarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CruzarButton.BackColor = System.Drawing.SystemColors.Control
            Me.CruzarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CruzarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CruzarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.cross
            Me.CruzarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CruzarButton.Location = New System.Drawing.Point(471, 23)
            Me.CruzarButton.Name = "CruzarButton"
            Me.CruzarButton.Size = New System.Drawing.Size(100, 60)
            Me.CruzarButton.TabIndex = 45
            Me.CruzarButton.Tag = "Ctrl + C"
            Me.CruzarButton.Text = "&Cruzar"
            Me.CruzarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CruzarButton.UseVisualStyleBackColor = False
            '
            'LblValorContenedor
            '
            Me.LblValorContenedor.AutoSize = True
            Me.LblValorContenedor.Location = New System.Drawing.Point(120, 19)
            Me.LblValorContenedor.Name = "LblValorContenedor"
            Me.LblValorContenedor.Size = New System.Drawing.Size(30, 13)
            Me.LblValorContenedor.TabIndex = 29
            Me.LblValorContenedor.Text = "valor"
            '
            'LblValorCargue
            '
            Me.LblValorCargue.AutoSize = True
            Me.LblValorCargue.Location = New System.Drawing.Point(120, 42)
            Me.LblValorCargue.Name = "LblValorCargue"
            Me.LblValorCargue.Size = New System.Drawing.Size(30, 13)
            Me.LblValorCargue.TabIndex = 30
            Me.LblValorCargue.Text = "valor"
            '
            'LblValorPaquete
            '
            Me.LblValorPaquete.AutoSize = True
            Me.LblValorPaquete.Location = New System.Drawing.Point(120, 67)
            Me.LblValorPaquete.Name = "LblValorPaquete"
            Me.LblValorPaquete.Size = New System.Drawing.Size(30, 13)
            Me.LblValorPaquete.TabIndex = 31
            Me.LblValorPaquete.Text = "valor"
            '
            'PaqueteLabel
            '
            Me.PaqueteLabel.AutoSize = True
            Me.PaqueteLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PaqueteLabel.Location = New System.Drawing.Point(6, 65)
            Me.PaqueteLabel.Name = "PaqueteLabel"
            Me.PaqueteLabel.Size = New System.Drawing.Size(58, 13)
            Me.PaqueteLabel.TabIndex = 28
            Me.PaqueteLabel.Text = "Paquete:"
            '
            'ContenedorLabel
            '
            Me.ContenedorLabel.AutoSize = True
            Me.ContenedorLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ContenedorLabel.Location = New System.Drawing.Point(6, 19)
            Me.ContenedorLabel.Name = "ContenedorLabel"
            Me.ContenedorLabel.Size = New System.Drawing.Size(119, 13)
            Me.ContenedorLabel.TabIndex = 26
            Me.ContenedorLabel.Text = "Código Contenedor:"
            '
            'FormCorreccion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(599, 456)
            Me.Controls.Add(Me.CorreccionesDesktopTabControl)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MinimumSize = New System.Drawing.Size(607, 483)
            Me.Name = "FormCorreccion"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Correcciones"
            CType(Me.GetErrorLlavesDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GetErrorLlaves_DataSet, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ConsultaTab.ResumeLayout(False)
            Me.ResultadoGroupBox.ResumeLayout(False)
            CType(Me.DatosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.FiltroGroupBox.ResumeLayout(False)
            Me.FiltroGroupBox.PerformLayout()
            Me.CorreccionesDesktopTabControl.ResumeLayout(False)
            Me.CorreccionTab.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            Me.CorreccionGroupBox.ResumeLayout(False)
            Me.CorreccionGroupBox.PerformLayout()
            Me.OrigenErrorPanel.ResumeLayout(False)
            Me.OrigenErrorPanel.PerformLayout()
            Me.CambioLLavesPanel.ResumeLayout(False)
            Me.CambioLLavesPanel.PerformLayout()
            Me.TipoCambioPanel.ResumeLayout(False)
            Me.TipoCambioPanel.PerformLayout()
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GetErrorLlavesDataSetBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents GetErrorLlaves_DataSet As Banagrario.Plugin.GetErrorLlaves_DataSet
        Friend WithEvents ConsultaTab As System.Windows.Forms.TabPage
        Friend WithEvents ResultadoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents DatosDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents fk_Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Cob As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Codigo_Contenedor As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Oficina As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Oficina As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fecha_Movimiento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Cargue As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Cargue_Paquete As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Cant_Soporte_Sobrantes As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents token As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FiltroGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents FechaProcesoDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ActualizarButton As System.Windows.Forms.Button
        Friend WithEvents FechaProcesolabel As System.Windows.Forms.Label
        Friend WithEvents CorreccionesDesktopTabControl As DesktopTabControl
        Friend WithEvents CorreccionTab As System.Windows.Forms.TabPage
        Friend WithEvents CorreccionGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents OrigenErrorPanel As System.Windows.Forms.Panel
        Friend WithEvents LblOrigenError As System.Windows.Forms.Label
        Friend WithEvents RdbFuncionarioBanco As System.Windows.Forms.RadioButton
        Friend WithEvents RdbFuncionariopyc As System.Windows.Forms.RadioButton
        Friend WithEvents CambioLLavesPanel As System.Windows.Forms.Panel
        Friend WithEvents LblNuevaFechaMovimiento As System.Windows.Forms.Label
        Friend WithEvents LblNuevoCodigoOficina As System.Windows.Forms.Label
        Private WithEvents FechaMovimientoDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents OficinaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents TipoCambioPanel As System.Windows.Forms.Panel
        Friend WithEvents LblTipoCambio As System.Windows.Forms.Label
        Friend WithEvents CBFechaMovimiento As System.Windows.Forms.CheckBox
        Friend WithEvents CBCodigoOficina As System.Windows.Forms.CheckBox
        Friend WithEvents LblCodOfiAnterior As System.Windows.Forms.Label
        Friend WithEvents LblFecMovAnterior As System.Windows.Forms.Label
        Friend WithEvents Lblfk_Proceso As System.Windows.Forms.Label
        Friend WithEvents BtnGuardar As System.Windows.Forms.Button
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents CruzarButton As System.Windows.Forms.Button
        Friend WithEvents LblValorContenedor As System.Windows.Forms.Label
        Friend WithEvents LblValorCargue As System.Windows.Forms.Label
        Friend WithEvents LblValorPaquete As System.Windows.Forms.Label
        Friend WithEvents PaqueteLabel As System.Windows.Forms.Label
        Friend WithEvents ContenedorLabel As System.Windows.Forms.Label
        Friend WithEvents CargueLabel As System.Windows.Forms.Label
        Friend WithEvents CBContenedor As System.Windows.Forms.CheckBox
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents CodigoContenedorTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
    End Class
End Namespace