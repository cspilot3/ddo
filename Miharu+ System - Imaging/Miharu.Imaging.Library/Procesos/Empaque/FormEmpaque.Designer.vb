Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Procesos.Empaque
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEmpaque
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
            Me.PrecintoNumeroTextBox = New System.Windows.Forms.TextBox()
            Me.PrecintoLabel = New System.Windows.Forms.Label()
            Me.PrecintoEliminarButton = New System.Windows.Forms.Button()
            Me.PrecintoAbrirButton = New System.Windows.Forms.Button()
            Me.PrecintoFinalizarButton = New System.Windows.Forms.Button()
            Me.PrecintoInsertarButton = New System.Windows.Forms.Button()
            Me.PrecintoValidarButton = New System.Windows.Forms.Button()
            Me.PrecintoGroupBox = New System.Windows.Forms.GroupBox()
            Me.PrecintoCamposDinamicosPanel = New System.Windows.Forms.Panel()
            Me.PrecintoCamposEstaticosPanel = New System.Windows.Forms.Panel()
            Me.PrecintoCerradoLabel = New System.Windows.Forms.Label()
            Me.ContenedorGroupBox = New System.Windows.Forms.GroupBox()
            Me.ContenedorSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.ContenedorDatosGroupBox = New System.Windows.Forms.GroupBox()
            Me.ContenedorDatosEstaticosPanel = New System.Windows.Forms.Panel()
            Me.ContenedorEsquemaComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.ContenedorTokenTextBox = New System.Windows.Forms.TextBox()
            Me.ContenedorLabel = New System.Windows.Forms.Label()
            Me.ContenedorCerradoLabel = New System.Windows.Forms.Label()
            Me.ContenedorEsquemaLabel = New System.Windows.Forms.Label()
            Me.ContenedoresDestapadosGroupBox = New System.Windows.Forms.GroupBox()
            Me.ContenedoresPrecintoPanel = New System.Windows.Forms.Panel()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.ContenedoresDataGridView = New System.Windows.Forms.DataGridView()
            Me.TokenCnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Esquema = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CantContenedorPanel = New System.Windows.Forms.Panel()
            Me.CantContenedorLabel = New System.Windows.Forms.Label()
            Me.CantidadLabel = New System.Windows.Forms.Label()
            Me.DocumentoGroupBox = New System.Windows.Forms.GroupBox()
            Me.DocumentoSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.DocumentoDatosGroupBox = New System.Windows.Forms.GroupBox()
            Me.DocumentoTokenTextBox = New System.Windows.Forms.TextBox()
            Me.DocumentoLabel = New System.Windows.Forms.Label()
            Me.DocumentosGroupBox = New System.Windows.Forms.GroupBox()
            Me.DocumentosPanel = New System.Windows.Forms.Panel()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.DocumentosDataGridView = New System.Windows.Forms.DataGridView()
            Me.Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CantDocumentoPanel = New System.Windows.Forms.Panel()
            Me.CantDocLabel = New System.Windows.Forms.Label()
            Me.CantidadDocLabel = New System.Windows.Forms.Label()
            Me.WorkAreaTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            Me.PrecintoGroupPanel = New System.Windows.Forms.Panel()
            Me.ContenedorGroupPanel = New System.Windows.Forms.Panel()
            Me.DocumentoGroupPanel = New System.Windows.Forms.Panel()
            Me.MenuPanel = New System.Windows.Forms.Panel()
            Me.PrecintoMenuPanel = New System.Windows.Forms.Panel()
            Me.ContenedorMenuPanel = New System.Windows.Forms.Panel()
            Me.ContenedorNuevoButton = New System.Windows.Forms.Button()
            Me.ContenedorInsertarButton = New System.Windows.Forms.Button()
            Me.ContenedorEliminarButton = New System.Windows.Forms.Button()
            Me.ContenedorAbrirButton = New System.Windows.Forms.Button()
            Me.ContenedorCerrarButton = New System.Windows.Forms.Button()
            Me.DocumentoMenuPanel = New System.Windows.Forms.Panel()
            Me.DocumentoNuevoButton = New System.Windows.Forms.Button()
            Me.DocumentoInsertarButton = New System.Windows.Forms.Button()
            Me.DocumentoEliminarButton = New System.Windows.Forms.Button()
            Me.WorkAreaPanel = New System.Windows.Forms.Panel()
            Me.WorkAreaSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.PrecintoGroupBox.SuspendLayout()
            Me.PrecintoCamposEstaticosPanel.SuspendLayout()
            Me.ContenedorGroupBox.SuspendLayout()
            CType(Me.ContenedorSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ContenedorSplitContainer.Panel1.SuspendLayout()
            Me.ContenedorSplitContainer.Panel2.SuspendLayout()
            Me.ContenedorSplitContainer.SuspendLayout()
            Me.ContenedorDatosGroupBox.SuspendLayout()
            Me.ContenedorDatosEstaticosPanel.SuspendLayout()
            Me.ContenedoresDestapadosGroupBox.SuspendLayout()
            Me.ContenedoresPrecintoPanel.SuspendLayout()
            Me.Panel1.SuspendLayout()
            CType(Me.ContenedoresDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.CantContenedorPanel.SuspendLayout()
            Me.DocumentoGroupBox.SuspendLayout()
            CType(Me.DocumentoSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.DocumentoSplitContainer.Panel1.SuspendLayout()
            Me.DocumentoSplitContainer.Panel2.SuspendLayout()
            Me.DocumentoSplitContainer.SuspendLayout()
            Me.DocumentoDatosGroupBox.SuspendLayout()
            Me.DocumentosGroupBox.SuspendLayout()
            Me.DocumentosPanel.SuspendLayout()
            Me.Panel2.SuspendLayout()
            CType(Me.DocumentosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.CantDocumentoPanel.SuspendLayout()
            Me.WorkAreaTableLayoutPanel.SuspendLayout()
            Me.PrecintoGroupPanel.SuspendLayout()
            Me.ContenedorGroupPanel.SuspendLayout()
            Me.DocumentoGroupPanel.SuspendLayout()
            Me.MenuPanel.SuspendLayout()
            Me.PrecintoMenuPanel.SuspendLayout()
            Me.ContenedorMenuPanel.SuspendLayout()
            Me.DocumentoMenuPanel.SuspendLayout()
            Me.WorkAreaPanel.SuspendLayout()
            CType(Me.WorkAreaSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.WorkAreaSplitContainer.Panel1.SuspendLayout()
            Me.WorkAreaSplitContainer.SuspendLayout()
            Me.SuspendLayout()
            '
            'PrecintoNumeroTextBox
            '
            Me.PrecintoNumeroTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrecintoNumeroTextBox.Location = New System.Drawing.Point(5, 20)
            Me.PrecintoNumeroTextBox.MaxLength = 50
            Me.PrecintoNumeroTextBox.Name = "PrecintoNumeroTextBox"
            Me.PrecintoNumeroTextBox.Size = New System.Drawing.Size(294, 21)
            Me.PrecintoNumeroTextBox.TabIndex = 1002
            '
            'PrecintoLabel
            '
            Me.PrecintoLabel.AutoSize = True
            Me.PrecintoLabel.ForeColor = System.Drawing.Color.Maroon
            Me.PrecintoLabel.Location = New System.Drawing.Point(4, 4)
            Me.PrecintoLabel.Name = "PrecintoLabel"
            Me.PrecintoLabel.Size = New System.Drawing.Size(76, 13)
            Me.PrecintoLabel.TabIndex = 9000
            Me.PrecintoLabel.Text = "Nro Precinto"
            '
            'PrecintoEliminarButton
            '
            Me.PrecintoEliminarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PrecintoEliminarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnSalir
            Me.PrecintoEliminarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.PrecintoEliminarButton.Location = New System.Drawing.Point(526, 4)
            Me.PrecintoEliminarButton.Name = "PrecintoEliminarButton"
            Me.PrecintoEliminarButton.Size = New System.Drawing.Size(132, 23)
            Me.PrecintoEliminarButton.TabIndex = 1033
            Me.PrecintoEliminarButton.Text = "Eliminar Precinto"
            Me.PrecintoEliminarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'PrecintoAbrirButton
            '
            Me.PrecintoAbrirButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PrecintoAbrirButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cog_go
            Me.PrecintoAbrirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.PrecintoAbrirButton.Location = New System.Drawing.Point(347, 4)
            Me.PrecintoAbrirButton.Name = "PrecintoAbrirButton"
            Me.PrecintoAbrirButton.Size = New System.Drawing.Size(173, 23)
            Me.PrecintoAbrirButton.TabIndex = 1031
            Me.PrecintoAbrirButton.Text = "Abrir Precinto   CTR+F6"
            Me.PrecintoAbrirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'PrecintoFinalizarButton
            '
            Me.PrecintoFinalizarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PrecintoFinalizarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.layout_error
            Me.PrecintoFinalizarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.PrecintoFinalizarButton.Location = New System.Drawing.Point(181, 4)
            Me.PrecintoFinalizarButton.Name = "PrecintoFinalizarButton"
            Me.PrecintoFinalizarButton.Size = New System.Drawing.Size(157, 23)
            Me.PrecintoFinalizarButton.TabIndex = 1032
            Me.PrecintoFinalizarButton.Text = "Finalizar Precinto   F6"
            Me.PrecintoFinalizarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'PrecintoInsertarButton
            '
            Me.PrecintoInsertarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PrecintoInsertarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.tick
            Me.PrecintoInsertarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.PrecintoInsertarButton.Location = New System.Drawing.Point(6, 4)
            Me.PrecintoInsertarButton.Name = "PrecintoInsertarButton"
            Me.PrecintoInsertarButton.Size = New System.Drawing.Size(167, 23)
            Me.PrecintoInsertarButton.TabIndex = 1030
            Me.PrecintoInsertarButton.Text = "Insertar Precinto     F2"
            Me.PrecintoInsertarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'PrecintoValidarButton
            '
            Me.PrecintoValidarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrecintoValidarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PrecintoValidarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnBuscar
            Me.PrecintoValidarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.PrecintoValidarButton.Location = New System.Drawing.Point(305, 20)
            Me.PrecintoValidarButton.Name = "PrecintoValidarButton"
            Me.PrecintoValidarButton.Size = New System.Drawing.Size(27, 21)
            Me.PrecintoValidarButton.TabIndex = 1003
            Me.PrecintoValidarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.PrecintoValidarButton.UseVisualStyleBackColor = True
            '
            'PrecintoGroupBox
            '
            Me.PrecintoGroupBox.Controls.Add(Me.PrecintoCamposDinamicosPanel)
            Me.PrecintoGroupBox.Controls.Add(Me.PrecintoCamposEstaticosPanel)
            Me.PrecintoGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.PrecintoGroupBox.Location = New System.Drawing.Point(0, 0)
            Me.PrecintoGroupBox.Name = "PrecintoGroupBox"
            Me.PrecintoGroupBox.Size = New System.Drawing.Size(341, 527)
            Me.PrecintoGroupBox.TabIndex = 1000
            Me.PrecintoGroupBox.TabStop = False
            Me.PrecintoGroupBox.Text = "Precinto"
            '
            'PrecintoCamposDinamicosPanel
            '
            Me.PrecintoCamposDinamicosPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.PrecintoCamposDinamicosPanel.Location = New System.Drawing.Point(3, 61)
            Me.PrecintoCamposDinamicosPanel.Name = "PrecintoCamposDinamicosPanel"
            Me.PrecintoCamposDinamicosPanel.Padding = New System.Windows.Forms.Padding(3)
            Me.PrecintoCamposDinamicosPanel.Size = New System.Drawing.Size(335, 463)
            Me.PrecintoCamposDinamicosPanel.TabIndex = 1004
            '
            'PrecintoCamposEstaticosPanel
            '
            Me.PrecintoCamposEstaticosPanel.Controls.Add(Me.PrecintoValidarButton)
            Me.PrecintoCamposEstaticosPanel.Controls.Add(Me.PrecintoNumeroTextBox)
            Me.PrecintoCamposEstaticosPanel.Controls.Add(Me.PrecintoCerradoLabel)
            Me.PrecintoCamposEstaticosPanel.Controls.Add(Me.PrecintoLabel)
            Me.PrecintoCamposEstaticosPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.PrecintoCamposEstaticosPanel.Location = New System.Drawing.Point(3, 17)
            Me.PrecintoCamposEstaticosPanel.Name = "PrecintoCamposEstaticosPanel"
            Me.PrecintoCamposEstaticosPanel.Size = New System.Drawing.Size(335, 44)
            Me.PrecintoCamposEstaticosPanel.TabIndex = 1001
            '
            'PrecintoCerradoLabel
            '
            Me.PrecintoCerradoLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrecintoCerradoLabel.AutoSize = True
            Me.PrecintoCerradoLabel.ForeColor = System.Drawing.Color.Maroon
            Me.PrecintoCerradoLabel.Location = New System.Drawing.Point(279, 4)
            Me.PrecintoCerradoLabel.Name = "PrecintoCerradoLabel"
            Me.PrecintoCerradoLabel.Size = New System.Drawing.Size(52, 13)
            Me.PrecintoCerradoLabel.TabIndex = 9000
            Me.PrecintoCerradoLabel.Text = "Cerrado"
            Me.PrecintoCerradoLabel.Visible = False
            '
            'ContenedorGroupBox
            '
            Me.ContenedorGroupBox.Controls.Add(Me.ContenedorSplitContainer)
            Me.ContenedorGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedorGroupBox.Location = New System.Drawing.Point(0, 0)
            Me.ContenedorGroupBox.Name = "ContenedorGroupBox"
            Me.ContenedorGroupBox.Size = New System.Drawing.Size(341, 527)
            Me.ContenedorGroupBox.TabIndex = 2000
            Me.ContenedorGroupBox.TabStop = False
            Me.ContenedorGroupBox.Text = "Contenedor"
            '
            'ContenedorSplitContainer
            '
            Me.ContenedorSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedorSplitContainer.Location = New System.Drawing.Point(3, 17)
            Me.ContenedorSplitContainer.Name = "ContenedorSplitContainer"
            Me.ContenedorSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'ContenedorSplitContainer.Panel1
            '
            Me.ContenedorSplitContainer.Panel1.Controls.Add(Me.ContenedorDatosGroupBox)
            Me.ContenedorSplitContainer.Panel1.Padding = New System.Windows.Forms.Padding(3)
            '
            'ContenedorSplitContainer.Panel2
            '
            Me.ContenedorSplitContainer.Panel2.Controls.Add(Me.ContenedoresDestapadosGroupBox)
            Me.ContenedorSplitContainer.Panel2.Padding = New System.Windows.Forms.Padding(3)
            Me.ContenedorSplitContainer.Size = New System.Drawing.Size(335, 507)
            Me.ContenedorSplitContainer.SplitterDistance = 119
            Me.ContenedorSplitContainer.TabIndex = 9004
            Me.ContenedorSplitContainer.TabStop = False
            '
            'ContenedorDatosGroupBox
            '
            Me.ContenedorDatosGroupBox.Controls.Add(Me.ContenedorDatosEstaticosPanel)
            Me.ContenedorDatosGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedorDatosGroupBox.Location = New System.Drawing.Point(3, 3)
            Me.ContenedorDatosGroupBox.Name = "ContenedorDatosGroupBox"
            Me.ContenedorDatosGroupBox.Size = New System.Drawing.Size(329, 113)
            Me.ContenedorDatosGroupBox.TabIndex = 2001
            Me.ContenedorDatosGroupBox.TabStop = False
            Me.ContenedorDatosGroupBox.Text = "Datos del contenedor"
            '
            'ContenedorDatosEstaticosPanel
            '
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.ContenedorEsquemaComboBox)
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.ContenedorTokenTextBox)
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.ContenedorLabel)
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.ContenedorCerradoLabel)
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.ContenedorEsquemaLabel)
            Me.ContenedorDatosEstaticosPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.ContenedorDatosEstaticosPanel.Location = New System.Drawing.Point(3, 17)
            Me.ContenedorDatosEstaticosPanel.Name = "ContenedorDatosEstaticosPanel"
            Me.ContenedorDatosEstaticosPanel.Size = New System.Drawing.Size(323, 89)
            Me.ContenedorDatosEstaticosPanel.TabIndex = 2002
            '
            'ContenedorEsquemaComboBox
            '
            Me.ContenedorEsquemaComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ContenedorEsquemaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ContenedorEsquemaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ContenedorEsquemaComboBox.DisabledEnter = False
            Me.ContenedorEsquemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ContenedorEsquemaComboBox.fk_Campo = 0
            Me.ContenedorEsquemaComboBox.fk_Documento = 0
            Me.ContenedorEsquemaComboBox.fk_Validacion = 0
            Me.ContenedorEsquemaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ContenedorEsquemaComboBox.FormattingEnabled = True
            Me.ContenedorEsquemaComboBox.Location = New System.Drawing.Point(6, 62)
            Me.ContenedorEsquemaComboBox.Name = "ContenedorEsquemaComboBox"
            Me.ContenedorEsquemaComboBox.Size = New System.Drawing.Size(308, 21)
            Me.ContenedorEsquemaComboBox.TabIndex = 9001
            '
            'ContenedorTokenTextBox
            '
            Me.ContenedorTokenTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ContenedorTokenTextBox.Location = New System.Drawing.Point(4, 21)
            Me.ContenedorTokenTextBox.MaxLength = 50
            Me.ContenedorTokenTextBox.Name = "ContenedorTokenTextBox"
            Me.ContenedorTokenTextBox.Size = New System.Drawing.Size(310, 21)
            Me.ContenedorTokenTextBox.TabIndex = 2003
            '
            'ContenedorLabel
            '
            Me.ContenedorLabel.AutoSize = True
            Me.ContenedorLabel.ForeColor = System.Drawing.Color.Maroon
            Me.ContenedorLabel.Location = New System.Drawing.Point(3, 4)
            Me.ContenedorLabel.Name = "ContenedorLabel"
            Me.ContenedorLabel.Size = New System.Drawing.Size(45, 13)
            Me.ContenedorLabel.TabIndex = 9000
            Me.ContenedorLabel.Text = "Codigo"
            '
            'ContenedorCerradoLabel
            '
            Me.ContenedorCerradoLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ContenedorCerradoLabel.AutoSize = True
            Me.ContenedorCerradoLabel.ForeColor = System.Drawing.Color.Maroon
            Me.ContenedorCerradoLabel.Location = New System.Drawing.Point(265, 4)
            Me.ContenedorCerradoLabel.Name = "ContenedorCerradoLabel"
            Me.ContenedorCerradoLabel.Size = New System.Drawing.Size(52, 13)
            Me.ContenedorCerradoLabel.TabIndex = 9000
            Me.ContenedorCerradoLabel.Text = "Cerrado"
            Me.ContenedorCerradoLabel.Visible = False
            '
            'ContenedorEsquemaLabel
            '
            Me.ContenedorEsquemaLabel.AutoSize = True
            Me.ContenedorEsquemaLabel.ForeColor = System.Drawing.Color.Maroon
            Me.ContenedorEsquemaLabel.Location = New System.Drawing.Point(2, 45)
            Me.ContenedorEsquemaLabel.Name = "ContenedorEsquemaLabel"
            Me.ContenedorEsquemaLabel.Size = New System.Drawing.Size(58, 13)
            Me.ContenedorEsquemaLabel.TabIndex = 9000
            Me.ContenedorEsquemaLabel.Text = "Esquema"
            '
            'ContenedoresDestapadosGroupBox
            '
            Me.ContenedoresDestapadosGroupBox.Controls.Add(Me.ContenedoresPrecintoPanel)
            Me.ContenedoresDestapadosGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedoresDestapadosGroupBox.Location = New System.Drawing.Point(3, 3)
            Me.ContenedoresDestapadosGroupBox.Name = "ContenedoresDestapadosGroupBox"
            Me.ContenedoresDestapadosGroupBox.Size = New System.Drawing.Size(329, 378)
            Me.ContenedoresDestapadosGroupBox.TabIndex = 2050
            Me.ContenedoresDestapadosGroupBox.TabStop = False
            Me.ContenedoresDestapadosGroupBox.Text = "Contenedores destapados en el precinto"
            '
            'ContenedoresPrecintoPanel
            '
            Me.ContenedoresPrecintoPanel.Controls.Add(Me.Panel1)
            Me.ContenedoresPrecintoPanel.Controls.Add(Me.CantContenedorPanel)
            Me.ContenedoresPrecintoPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedoresPrecintoPanel.Location = New System.Drawing.Point(3, 17)
            Me.ContenedoresPrecintoPanel.Name = "ContenedoresPrecintoPanel"
            Me.ContenedoresPrecintoPanel.Size = New System.Drawing.Size(323, 358)
            Me.ContenedoresPrecintoPanel.TabIndex = 0
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.ContenedoresDataGridView)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel1.Location = New System.Drawing.Point(0, 21)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(323, 337)
            Me.Panel1.TabIndex = 3
            '
            'ContenedoresDataGridView
            '
            Me.ContenedoresDataGridView.AllowUserToAddRows = False
            Me.ContenedoresDataGridView.AllowUserToDeleteRows = False
            Me.ContenedoresDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ContenedoresDataGridView.ColumnHeadersVisible = False
            Me.ContenedoresDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TokenCnt, Me.Nombre_Esquema})
            Me.ContenedoresDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedoresDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
            Me.ContenedoresDataGridView.Location = New System.Drawing.Point(0, 0)
            Me.ContenedoresDataGridView.Name = "ContenedoresDataGridView"
            Me.ContenedoresDataGridView.ReadOnly = True
            Me.ContenedoresDataGridView.RowHeadersWidth = 20
            Me.ContenedoresDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ContenedoresDataGridView.Size = New System.Drawing.Size(323, 337)
            Me.ContenedoresDataGridView.TabIndex = 0
            '
            'TokenCnt
            '
            Me.TokenCnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.TokenCnt.DataPropertyName = "Token"
            Me.TokenCnt.HeaderText = "Token"
            Me.TokenCnt.Name = "TokenCnt"
            Me.TokenCnt.ReadOnly = True
            '
            'Nombre_Esquema
            '
            Me.Nombre_Esquema.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Nombre_Esquema.DataPropertyName = "Nombre_Esquema"
            Me.Nombre_Esquema.HeaderText = "Esquema"
            Me.Nombre_Esquema.Name = "Nombre_Esquema"
            Me.Nombre_Esquema.ReadOnly = True
            '
            'CantContenedorPanel
            '
            Me.CantContenedorPanel.Controls.Add(Me.CantContenedorLabel)
            Me.CantContenedorPanel.Controls.Add(Me.CantidadLabel)
            Me.CantContenedorPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.CantContenedorPanel.Location = New System.Drawing.Point(0, 0)
            Me.CantContenedorPanel.Name = "CantContenedorPanel"
            Me.CantContenedorPanel.Size = New System.Drawing.Size(323, 21)
            Me.CantContenedorPanel.TabIndex = 2
            '
            'CantContenedorLabel
            '
            Me.CantContenedorLabel.AutoSize = True
            Me.CantContenedorLabel.Location = New System.Drawing.Point(60, 5)
            Me.CantContenedorLabel.Name = "CantContenedorLabel"
            Me.CantContenedorLabel.Size = New System.Drawing.Size(14, 13)
            Me.CantContenedorLabel.TabIndex = 4
            Me.CantContenedorLabel.Text = "0"
            '
            'CantidadLabel
            '
            Me.CantidadLabel.AutoSize = True
            Me.CantidadLabel.Location = New System.Drawing.Point(1, 5)
            Me.CantidadLabel.Name = "CantidadLabel"
            Me.CantidadLabel.Size = New System.Drawing.Size(60, 13)
            Me.CantidadLabel.TabIndex = 3
            Me.CantidadLabel.Text = "Cantidad:"
            '
            'DocumentoGroupBox
            '
            Me.DocumentoGroupBox.Controls.Add(Me.DocumentoSplitContainer)
            Me.DocumentoGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DocumentoGroupBox.Location = New System.Drawing.Point(0, 0)
            Me.DocumentoGroupBox.Name = "DocumentoGroupBox"
            Me.DocumentoGroupBox.Size = New System.Drawing.Size(240, 527)
            Me.DocumentoGroupBox.TabIndex = 3000
            Me.DocumentoGroupBox.TabStop = False
            Me.DocumentoGroupBox.Text = "Documento"
            '
            'DocumentoSplitContainer
            '
            Me.DocumentoSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DocumentoSplitContainer.Location = New System.Drawing.Point(3, 17)
            Me.DocumentoSplitContainer.Name = "DocumentoSplitContainer"
            Me.DocumentoSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'DocumentoSplitContainer.Panel1
            '
            Me.DocumentoSplitContainer.Panel1.Controls.Add(Me.DocumentoDatosGroupBox)
            Me.DocumentoSplitContainer.Panel1.Padding = New System.Windows.Forms.Padding(3)
            '
            'DocumentoSplitContainer.Panel2
            '
            Me.DocumentoSplitContainer.Panel2.Controls.Add(Me.DocumentosGroupBox)
            Me.DocumentoSplitContainer.Panel2.Padding = New System.Windows.Forms.Padding(3)
            Me.DocumentoSplitContainer.Size = New System.Drawing.Size(234, 507)
            Me.DocumentoSplitContainer.SplitterDistance = 67
            Me.DocumentoSplitContainer.TabIndex = 10008
            Me.DocumentoSplitContainer.TabStop = False
            '
            'DocumentoDatosGroupBox
            '
            Me.DocumentoDatosGroupBox.Controls.Add(Me.DocumentoTokenTextBox)
            Me.DocumentoDatosGroupBox.Controls.Add(Me.DocumentoLabel)
            Me.DocumentoDatosGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DocumentoDatosGroupBox.Location = New System.Drawing.Point(3, 3)
            Me.DocumentoDatosGroupBox.Name = "DocumentoDatosGroupBox"
            Me.DocumentoDatosGroupBox.Size = New System.Drawing.Size(228, 61)
            Me.DocumentoDatosGroupBox.TabIndex = 3001
            Me.DocumentoDatosGroupBox.TabStop = False
            Me.DocumentoDatosGroupBox.Text = "Datos del documento"
            '
            'DocumentoTokenTextBox
            '
            Me.DocumentoTokenTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DocumentoTokenTextBox.Location = New System.Drawing.Point(4, 37)
            Me.DocumentoTokenTextBox.MaxLength = 50
            Me.DocumentoTokenTextBox.Name = "DocumentoTokenTextBox"
            Me.DocumentoTokenTextBox.Size = New System.Drawing.Size(218, 21)
            Me.DocumentoTokenTextBox.TabIndex = 3002
            '
            'DocumentoLabel
            '
            Me.DocumentoLabel.AutoSize = True
            Me.DocumentoLabel.ForeColor = System.Drawing.Color.Maroon
            Me.DocumentoLabel.Location = New System.Drawing.Point(3, 20)
            Me.DocumentoLabel.Name = "DocumentoLabel"
            Me.DocumentoLabel.Size = New System.Drawing.Size(45, 13)
            Me.DocumentoLabel.TabIndex = 9003
            Me.DocumentoLabel.Text = "Codigo"
            '
            'DocumentosGroupBox
            '
            Me.DocumentosGroupBox.Controls.Add(Me.DocumentosPanel)
            Me.DocumentosGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DocumentosGroupBox.Location = New System.Drawing.Point(3, 3)
            Me.DocumentosGroupBox.Name = "DocumentosGroupBox"
            Me.DocumentosGroupBox.Size = New System.Drawing.Size(228, 430)
            Me.DocumentosGroupBox.TabIndex = 3010
            Me.DocumentosGroupBox.TabStop = False
            Me.DocumentosGroupBox.Text = "Documentos contenedor"
            '
            'DocumentosPanel
            '
            Me.DocumentosPanel.Controls.Add(Me.Panel2)
            Me.DocumentosPanel.Controls.Add(Me.CantDocumentoPanel)
            Me.DocumentosPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DocumentosPanel.Location = New System.Drawing.Point(3, 17)
            Me.DocumentosPanel.Name = "DocumentosPanel"
            Me.DocumentosPanel.Size = New System.Drawing.Size(222, 410)
            Me.DocumentosPanel.TabIndex = 0
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.DocumentosDataGridView)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel2.Location = New System.Drawing.Point(0, 21)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(222, 389)
            Me.Panel2.TabIndex = 4
            '
            'DocumentosDataGridView
            '
            Me.DocumentosDataGridView.AllowUserToAddRows = False
            Me.DocumentosDataGridView.AllowUserToDeleteRows = False
            Me.DocumentosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DocumentosDataGridView.ColumnHeadersVisible = False
            Me.DocumentosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Token, Me.Nombre_Documento})
            Me.DocumentosDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DocumentosDataGridView.Location = New System.Drawing.Point(0, 0)
            Me.DocumentosDataGridView.MultiSelect = False
            Me.DocumentosDataGridView.Name = "DocumentosDataGridView"
            Me.DocumentosDataGridView.ReadOnly = True
            Me.DocumentosDataGridView.RowHeadersWidth = 20
            Me.DocumentosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DocumentosDataGridView.Size = New System.Drawing.Size(222, 389)
            Me.DocumentosDataGridView.TabIndex = 0
            Me.DocumentosDataGridView.TabStop = False
            '
            'Token
            '
            Me.Token.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Token.DataPropertyName = "Token"
            Me.Token.HeaderText = "Token"
            Me.Token.Name = "Token"
            Me.Token.ReadOnly = True
            '
            'Nombre_Documento
            '
            Me.Nombre_Documento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Nombre_Documento.DataPropertyName = "Nombre_Documento"
            Me.Nombre_Documento.HeaderText = "Nombre_Documento"
            Me.Nombre_Documento.Name = "Nombre_Documento"
            Me.Nombre_Documento.ReadOnly = True
            '
            'CantDocumentoPanel
            '
            Me.CantDocumentoPanel.Controls.Add(Me.CantDocLabel)
            Me.CantDocumentoPanel.Controls.Add(Me.CantidadDocLabel)
            Me.CantDocumentoPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.CantDocumentoPanel.Location = New System.Drawing.Point(0, 0)
            Me.CantDocumentoPanel.Name = "CantDocumentoPanel"
            Me.CantDocumentoPanel.Size = New System.Drawing.Size(222, 21)
            Me.CantDocumentoPanel.TabIndex = 3
            '
            'CantDocLabel
            '
            Me.CantDocLabel.AutoSize = True
            Me.CantDocLabel.Location = New System.Drawing.Point(60, 5)
            Me.CantDocLabel.Name = "CantDocLabel"
            Me.CantDocLabel.Size = New System.Drawing.Size(14, 13)
            Me.CantDocLabel.TabIndex = 4
            Me.CantDocLabel.Text = "0"
            '
            'CantidadDocLabel
            '
            Me.CantidadDocLabel.AutoSize = True
            Me.CantidadDocLabel.Location = New System.Drawing.Point(1, 5)
            Me.CantidadDocLabel.Name = "CantidadDocLabel"
            Me.CantidadDocLabel.Size = New System.Drawing.Size(60, 13)
            Me.CantidadDocLabel.TabIndex = 3
            Me.CantidadDocLabel.Text = "Cantidad:"
            '
            'WorkAreaTableLayoutPanel
            '
            Me.WorkAreaTableLayoutPanel.ColumnCount = 3
            Me.WorkAreaTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.0!))
            Me.WorkAreaTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.0!))
            Me.WorkAreaTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.0!))
            Me.WorkAreaTableLayoutPanel.Controls.Add(Me.PrecintoGroupPanel, 0, 0)
            Me.WorkAreaTableLayoutPanel.Controls.Add(Me.ContenedorGroupPanel, 1, 0)
            Me.WorkAreaTableLayoutPanel.Controls.Add(Me.DocumentoGroupPanel, 2, 0)
            Me.WorkAreaTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.WorkAreaTableLayoutPanel.Location = New System.Drawing.Point(5, 5)
            Me.WorkAreaTableLayoutPanel.Name = "WorkAreaTableLayoutPanel"
            Me.WorkAreaTableLayoutPanel.RowCount = 1
            Me.WorkAreaTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.WorkAreaTableLayoutPanel.Size = New System.Drawing.Size(940, 533)
            Me.WorkAreaTableLayoutPanel.TabIndex = 9001
            '
            'PrecintoGroupPanel
            '
            Me.PrecintoGroupPanel.Controls.Add(Me.PrecintoGroupBox)
            Me.PrecintoGroupPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.PrecintoGroupPanel.Location = New System.Drawing.Point(3, 3)
            Me.PrecintoGroupPanel.Name = "PrecintoGroupPanel"
            Me.PrecintoGroupPanel.Size = New System.Drawing.Size(341, 527)
            Me.PrecintoGroupPanel.TabIndex = 31
            '
            'ContenedorGroupPanel
            '
            Me.ContenedorGroupPanel.Controls.Add(Me.ContenedorGroupBox)
            Me.ContenedorGroupPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedorGroupPanel.Location = New System.Drawing.Point(350, 3)
            Me.ContenedorGroupPanel.Name = "ContenedorGroupPanel"
            Me.ContenedorGroupPanel.Size = New System.Drawing.Size(341, 527)
            Me.ContenedorGroupPanel.TabIndex = 32
            '
            'DocumentoGroupPanel
            '
            Me.DocumentoGroupPanel.Controls.Add(Me.DocumentoGroupBox)
            Me.DocumentoGroupPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DocumentoGroupPanel.Location = New System.Drawing.Point(697, 3)
            Me.DocumentoGroupPanel.Name = "DocumentoGroupPanel"
            Me.DocumentoGroupPanel.Size = New System.Drawing.Size(240, 527)
            Me.DocumentoGroupPanel.TabIndex = 33
            '
            'MenuPanel
            '
            Me.MenuPanel.Controls.Add(Me.PrecintoMenuPanel)
            Me.MenuPanel.Controls.Add(Me.ContenedorMenuPanel)
            Me.MenuPanel.Controls.Add(Me.DocumentoMenuPanel)
            Me.MenuPanel.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.MenuPanel.Location = New System.Drawing.Point(5, 548)
            Me.MenuPanel.Name = "MenuPanel"
            Me.MenuPanel.Padding = New System.Windows.Forms.Padding(5)
            Me.MenuPanel.Size = New System.Drawing.Size(950, 30)
            Me.MenuPanel.TabIndex = 31
            '
            'PrecintoMenuPanel
            '
            Me.PrecintoMenuPanel.Controls.Add(Me.PrecintoInsertarButton)
            Me.PrecintoMenuPanel.Controls.Add(Me.PrecintoEliminarButton)
            Me.PrecintoMenuPanel.Controls.Add(Me.PrecintoAbrirButton)
            Me.PrecintoMenuPanel.Controls.Add(Me.PrecintoFinalizarButton)
            Me.PrecintoMenuPanel.Location = New System.Drawing.Point(0, 0)
            Me.PrecintoMenuPanel.Name = "PrecintoMenuPanel"
            Me.PrecintoMenuPanel.Size = New System.Drawing.Size(947, 30)
            Me.PrecintoMenuPanel.TabIndex = 0
            '
            'ContenedorMenuPanel
            '
            Me.ContenedorMenuPanel.Controls.Add(Me.ContenedorNuevoButton)
            Me.ContenedorMenuPanel.Controls.Add(Me.ContenedorInsertarButton)
            Me.ContenedorMenuPanel.Controls.Add(Me.ContenedorEliminarButton)
            Me.ContenedorMenuPanel.Controls.Add(Me.ContenedorAbrirButton)
            Me.ContenedorMenuPanel.Controls.Add(Me.ContenedorCerrarButton)
            Me.ContenedorMenuPanel.Location = New System.Drawing.Point(0, 0)
            Me.ContenedorMenuPanel.Name = "ContenedorMenuPanel"
            Me.ContenedorMenuPanel.Size = New System.Drawing.Size(945, 30)
            Me.ContenedorMenuPanel.TabIndex = 1
            '
            'ContenedorNuevoButton
            '
            Me.ContenedorNuevoButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ContenedorNuevoButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnAgregar
            Me.ContenedorNuevoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ContenedorNuevoButton.Location = New System.Drawing.Point(533, 4)
            Me.ContenedorNuevoButton.Name = "ContenedorNuevoButton"
            Me.ContenedorNuevoButton.Size = New System.Drawing.Size(186, 23)
            Me.ContenedorNuevoButton.TabIndex = 2034
            Me.ContenedorNuevoButton.Text = "Nuevo Contenedor CTR+F3"
            Me.ContenedorNuevoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'ContenedorInsertarButton
            '
            Me.ContenedorInsertarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ContenedorInsertarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.tick
            Me.ContenedorInsertarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ContenedorInsertarButton.Location = New System.Drawing.Point(6, 4)
            Me.ContenedorInsertarButton.Name = "ContenedorInsertarButton"
            Me.ContenedorInsertarButton.Size = New System.Drawing.Size(166, 23)
            Me.ContenedorInsertarButton.TabIndex = 2030
            Me.ContenedorInsertarButton.Text = "Insertar Contenedor F3"
            Me.ContenedorInsertarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'ContenedorEliminarButton
            '
            Me.ContenedorEliminarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ContenedorEliminarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnSalir
            Me.ContenedorEliminarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ContenedorEliminarButton.Location = New System.Drawing.Point(722, 4)
            Me.ContenedorEliminarButton.Name = "ContenedorEliminarButton"
            Me.ContenedorEliminarButton.Size = New System.Drawing.Size(153, 23)
            Me.ContenedorEliminarButton.TabIndex = 2033
            Me.ContenedorEliminarButton.Text = "Eliminar Contenedor"
            Me.ContenedorEliminarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'ContenedorAbrirButton
            '
            Me.ContenedorAbrirButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ContenedorAbrirButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cog_go
            Me.ContenedorAbrirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ContenedorAbrirButton.Location = New System.Drawing.Point(347, 4)
            Me.ContenedorAbrirButton.Name = "ContenedorAbrirButton"
            Me.ContenedorAbrirButton.Size = New System.Drawing.Size(179, 23)
            Me.ContenedorAbrirButton.TabIndex = 2031
            Me.ContenedorAbrirButton.Text = "Abrir Contenedor CTR+F5"
            Me.ContenedorAbrirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'ContenedorCerrarButton
            '
            Me.ContenedorCerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ContenedorCerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.layout_error
            Me.ContenedorCerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ContenedorCerrarButton.Location = New System.Drawing.Point(177, 4)
            Me.ContenedorCerrarButton.Name = "ContenedorCerrarButton"
            Me.ContenedorCerrarButton.Size = New System.Drawing.Size(162, 23)
            Me.ContenedorCerrarButton.TabIndex = 2032
            Me.ContenedorCerrarButton.Text = "Cerrar Contenedor F5"
            Me.ContenedorCerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'DocumentoMenuPanel
            '
            Me.DocumentoMenuPanel.Controls.Add(Me.DocumentoNuevoButton)
            Me.DocumentoMenuPanel.Controls.Add(Me.DocumentoInsertarButton)
            Me.DocumentoMenuPanel.Controls.Add(Me.DocumentoEliminarButton)
            Me.DocumentoMenuPanel.Location = New System.Drawing.Point(0, 0)
            Me.DocumentoMenuPanel.Name = "DocumentoMenuPanel"
            Me.DocumentoMenuPanel.Size = New System.Drawing.Size(947, 30)
            Me.DocumentoMenuPanel.TabIndex = 2
            '
            'DocumentoNuevoButton
            '
            Me.DocumentoNuevoButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DocumentoNuevoButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnAgregar
            Me.DocumentoNuevoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.DocumentoNuevoButton.Location = New System.Drawing.Point(179, 4)
            Me.DocumentoNuevoButton.Name = "DocumentoNuevoButton"
            Me.DocumentoNuevoButton.Size = New System.Drawing.Size(197, 23)
            Me.DocumentoNuevoButton.TabIndex = 3032
            Me.DocumentoNuevoButton.Text = "Nuevo Documento CTR+F4"
            Me.DocumentoNuevoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'DocumentoInsertarButton
            '
            Me.DocumentoInsertarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DocumentoInsertarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.tick
            Me.DocumentoInsertarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.DocumentoInsertarButton.Location = New System.Drawing.Point(5, 4)
            Me.DocumentoInsertarButton.Name = "DocumentoInsertarButton"
            Me.DocumentoInsertarButton.Size = New System.Drawing.Size(167, 23)
            Me.DocumentoInsertarButton.TabIndex = 3030
            Me.DocumentoInsertarButton.Text = "Insertar Documento F4"
            Me.DocumentoInsertarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'DocumentoEliminarButton
            '
            Me.DocumentoEliminarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DocumentoEliminarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnSalir
            Me.DocumentoEliminarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.DocumentoEliminarButton.Location = New System.Drawing.Point(382, 4)
            Me.DocumentoEliminarButton.Name = "DocumentoEliminarButton"
            Me.DocumentoEliminarButton.Size = New System.Drawing.Size(150, 23)
            Me.DocumentoEliminarButton.TabIndex = 3031
            Me.DocumentoEliminarButton.Text = "&Eliminar Documento"
            Me.DocumentoEliminarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'WorkAreaPanel
            '
            Me.WorkAreaPanel.Controls.Add(Me.WorkAreaTableLayoutPanel)
            Me.WorkAreaPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.WorkAreaPanel.Location = New System.Drawing.Point(5, 5)
            Me.WorkAreaPanel.Name = "WorkAreaPanel"
            Me.WorkAreaPanel.Padding = New System.Windows.Forms.Padding(5)
            Me.WorkAreaPanel.Size = New System.Drawing.Size(950, 543)
            Me.WorkAreaPanel.TabIndex = 10000
            '
            'WorkAreaSplitContainer
            '
            Me.WorkAreaSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.WorkAreaSplitContainer.Location = New System.Drawing.Point(0, 0)
            Me.WorkAreaSplitContainer.Name = "WorkAreaSplitContainer"
            Me.WorkAreaSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'WorkAreaSplitContainer.Panel1
            '
            Me.WorkAreaSplitContainer.Panel1.Controls.Add(Me.WorkAreaPanel)
            Me.WorkAreaSplitContainer.Panel1.Controls.Add(Me.MenuPanel)
            Me.WorkAreaSplitContainer.Panel1.Padding = New System.Windows.Forms.Padding(5)
            Me.WorkAreaSplitContainer.Panel2Collapsed = True
            Me.WorkAreaSplitContainer.Size = New System.Drawing.Size(960, 583)
            Me.WorkAreaSplitContainer.SplitterDistance = 545
            Me.WorkAreaSplitContainer.TabIndex = 10001
            Me.WorkAreaSplitContainer.TabStop = False
            '
            'FormEmpaque
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(960, 583)
            Me.Controls.Add(Me.WorkAreaSplitContainer)
            Me.Name = "FormEmpaque"
            Me.Text = "Empaque"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            Me.PrecintoGroupBox.ResumeLayout(False)
            Me.PrecintoCamposEstaticosPanel.ResumeLayout(False)
            Me.PrecintoCamposEstaticosPanel.PerformLayout()
            Me.ContenedorGroupBox.ResumeLayout(False)
            Me.ContenedorSplitContainer.Panel1.ResumeLayout(False)
            Me.ContenedorSplitContainer.Panel2.ResumeLayout(False)
            CType(Me.ContenedorSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ContenedorSplitContainer.ResumeLayout(False)
            Me.ContenedorDatosGroupBox.ResumeLayout(False)
            Me.ContenedorDatosEstaticosPanel.ResumeLayout(False)
            Me.ContenedorDatosEstaticosPanel.PerformLayout()
            Me.ContenedoresDestapadosGroupBox.ResumeLayout(False)
            Me.ContenedoresPrecintoPanel.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            CType(Me.ContenedoresDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.CantContenedorPanel.ResumeLayout(False)
            Me.CantContenedorPanel.PerformLayout()
            Me.DocumentoGroupBox.ResumeLayout(False)
            Me.DocumentoSplitContainer.Panel1.ResumeLayout(False)
            Me.DocumentoSplitContainer.Panel2.ResumeLayout(False)
            CType(Me.DocumentoSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.DocumentoSplitContainer.ResumeLayout(False)
            Me.DocumentoDatosGroupBox.ResumeLayout(False)
            Me.DocumentoDatosGroupBox.PerformLayout()
            Me.DocumentosGroupBox.ResumeLayout(False)
            Me.DocumentosPanel.ResumeLayout(False)
            Me.Panel2.ResumeLayout(False)
            CType(Me.DocumentosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.CantDocumentoPanel.ResumeLayout(False)
            Me.CantDocumentoPanel.PerformLayout()
            Me.WorkAreaTableLayoutPanel.ResumeLayout(False)
            Me.PrecintoGroupPanel.ResumeLayout(False)
            Me.ContenedorGroupPanel.ResumeLayout(False)
            Me.DocumentoGroupPanel.ResumeLayout(False)
            Me.MenuPanel.ResumeLayout(False)
            Me.PrecintoMenuPanel.ResumeLayout(False)
            Me.ContenedorMenuPanel.ResumeLayout(False)
            Me.DocumentoMenuPanel.ResumeLayout(False)
            Me.WorkAreaPanel.ResumeLayout(False)
            Me.WorkAreaSplitContainer.Panel1.ResumeLayout(False)
            CType(Me.WorkAreaSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.WorkAreaSplitContainer.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents PrecintoEliminarButton As System.Windows.Forms.Button
        Friend WithEvents PrecintoAbrirButton As System.Windows.Forms.Button
        Friend WithEvents PrecintoFinalizarButton As System.Windows.Forms.Button
        Friend WithEvents PrecintoInsertarButton As System.Windows.Forms.Button
        Friend WithEvents PrecintoNumeroTextBox As System.Windows.Forms.TextBox
        Friend WithEvents PrecintoLabel As System.Windows.Forms.Label
        Friend WithEvents PrecintoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ContenedorGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents DocumentoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents WorkAreaTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents PrecintoValidarButton As System.Windows.Forms.Button
        Friend WithEvents ContenedorSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents MenuPanel As System.Windows.Forms.Panel
        Friend WithEvents WorkAreaPanel As System.Windows.Forms.Panel
        Friend WithEvents WorkAreaSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents DocumentoMenuPanel As System.Windows.Forms.Panel
        Friend WithEvents ContenedorMenuPanel As System.Windows.Forms.Panel
        Friend WithEvents PrecintoMenuPanel As System.Windows.Forms.Panel
        Friend WithEvents PrecintoCamposDinamicosPanel As System.Windows.Forms.Panel
        Friend WithEvents PrecintoCamposEstaticosPanel As System.Windows.Forms.Panel
        Friend WithEvents ContenedorDatosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ContenedoresDestapadosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents DocumentoSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents DocumentoDatosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents DocumentosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ContenedorDatosEstaticosPanel As System.Windows.Forms.Panel
        Friend WithEvents ContenedorEsquemaLabel As System.Windows.Forms.Label
        Friend WithEvents ContenedorCerradoLabel As System.Windows.Forms.Label
        Friend WithEvents PrecintoCerradoLabel As System.Windows.Forms.Label
        Friend WithEvents ContenedorLabel As System.Windows.Forms.Label
        Friend WithEvents PrecintoGroupPanel As System.Windows.Forms.Panel
        Friend WithEvents ContenedorGroupPanel As System.Windows.Forms.Panel
        Friend WithEvents DocumentoGroupPanel As System.Windows.Forms.Panel
        Friend WithEvents ContenedorTokenTextBox As System.Windows.Forms.TextBox
        Friend WithEvents ContenedorInsertarButton As System.Windows.Forms.Button
        Friend WithEvents ContenedorEliminarButton As System.Windows.Forms.Button
        Friend WithEvents ContenedorAbrirButton As System.Windows.Forms.Button
        Friend WithEvents ContenedorCerrarButton As System.Windows.Forms.Button
        Friend WithEvents ContenedoresPrecintoPanel As System.Windows.Forms.Panel
        Friend WithEvents DocumentoInsertarButton As System.Windows.Forms.Button
        Friend WithEvents DocumentoEliminarButton As System.Windows.Forms.Button
        Friend WithEvents DocumentosPanel As System.Windows.Forms.Panel
        Public WithEvents ContenedorEsquemaComboBox As DesktopComboBoxControl
        Friend WithEvents DocumentoTokenTextBox As System.Windows.Forms.TextBox
        Friend WithEvents DocumentoLabel As System.Windows.Forms.Label
        Friend WithEvents ContenedorNuevoButton As System.Windows.Forms.Button
        Friend WithEvents DocumentoNuevoButton As System.Windows.Forms.Button
        Friend WithEvents DocumentosDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents ContenedoresDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents TokenCnt As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Esquema As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Token As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CantContenedorPanel As System.Windows.Forms.Panel
        Friend WithEvents CantContenedorLabel As System.Windows.Forms.Label
        Friend WithEvents CantidadLabel As System.Windows.Forms.Label
        Friend WithEvents CantDocumentoPanel As System.Windows.Forms.Panel
        Friend WithEvents CantDocLabel As System.Windows.Forms.Label
        Friend WithEvents CantidadDocLabel As System.Windows.Forms.Label
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
    End Class
End Namespace