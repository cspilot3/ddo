Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Procesos.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormDestapeCorreccion
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
            Me.PrecintoInsertarButton = New System.Windows.Forms.Button()
            Me.PrecintoValidarButton = New System.Windows.Forms.Button()
            Me.PrecintoGroupBox = New System.Windows.Forms.GroupBox()
            Me.PrecintoCamposDinamicosPanel = New System.Windows.Forms.Panel()
            Me.PrecintoCamposEstaticosPanel = New System.Windows.Forms.Panel()
            Me.PrecintoCerradoLabel = New System.Windows.Forms.Label()
            Me.ContenedorGroupBox = New System.Windows.Forms.GroupBox()
            Me.ContenedorSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.ContenedorDatosGroupBox = New System.Windows.Forms.GroupBox()
            Me.ContenedorCamposDinamicosPanel = New System.Windows.Forms.Panel()
            Me.ContenedorDatosEstaticosPanel = New System.Windows.Forms.Panel()
            Me.CantidadDocumentosRecibidosTextBox = New System.Windows.Forms.TextBox()
            Me.CantidadDocumentosEnviadosTextBox = New System.Windows.Forms.TextBox()
            Me.CantidadDocumentosRecibidosLabel = New System.Windows.Forms.Label()
            Me.CantidadDocumentosEnviadosLabel = New System.Windows.Forms.Label()
            Me.ContenedorEsquemaComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.ContenedorTokenTextBox = New System.Windows.Forms.TextBox()
            Me.ContenedorLabel = New System.Windows.Forms.Label()
            Me.ContenedorCerradoLabel = New System.Windows.Forms.Label()
            Me.ContenedorEsquemaLabel = New System.Windows.Forms.Label()
            Me.ContenedoresDestapadosGroupBox = New System.Windows.Forms.GroupBox()
            Me.ContenedoresPrecintoPanel = New System.Windows.Forms.Panel()
            Me.ContPanel = New System.Windows.Forms.Panel()
            Me.ContenedoresDataGridView = New System.Windows.Forms.DataGridView()
            Me.TokenCnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Esquema = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CantContenedorPanel = New System.Windows.Forms.Panel()
            Me.CantContenedorLabel = New System.Windows.Forms.Label()
            Me.CantidadLabel = New System.Windows.Forms.Label()
            Me.WorkAreaTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            Me.PrecintoGroupPanel = New System.Windows.Forms.Panel()
            Me.ContenedorGroupPanel = New System.Windows.Forms.Panel()
            Me.MenuPanel = New System.Windows.Forms.Panel()
            Me.ContenedorInsertarButton = New System.Windows.Forms.Button()
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
            Me.ContPanel.SuspendLayout()
            CType(Me.ContenedoresDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.CantContenedorPanel.SuspendLayout()
            Me.WorkAreaTableLayoutPanel.SuspendLayout()
            Me.PrecintoGroupPanel.SuspendLayout()
            Me.ContenedorGroupPanel.SuspendLayout()
            Me.MenuPanel.SuspendLayout()
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
            Me.PrecintoNumeroTextBox.Size = New System.Drawing.Size(420, 21)
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
            'PrecintoInsertarButton
            '
            Me.PrecintoInsertarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PrecintoInsertarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.tick
            Me.PrecintoInsertarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.PrecintoInsertarButton.Location = New System.Drawing.Point(18, 11)
            Me.PrecintoInsertarButton.Name = "PrecintoInsertarButton"
            Me.PrecintoInsertarButton.Size = New System.Drawing.Size(172, 23)
            Me.PrecintoInsertarButton.TabIndex = 1030
            Me.PrecintoInsertarButton.Text = "Actualizar Precinto   F2"
            Me.PrecintoInsertarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'PrecintoValidarButton
            '
            Me.PrecintoValidarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrecintoValidarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PrecintoValidarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnBuscar
            Me.PrecintoValidarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.PrecintoValidarButton.Location = New System.Drawing.Point(431, 20)
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
            Me.PrecintoGroupBox.Size = New System.Drawing.Size(467, 527)
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
            Me.PrecintoCamposDinamicosPanel.Size = New System.Drawing.Size(461, 463)
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
            Me.PrecintoCamposEstaticosPanel.Size = New System.Drawing.Size(461, 44)
            Me.PrecintoCamposEstaticosPanel.TabIndex = 1001
            '
            'PrecintoCerradoLabel
            '
            Me.PrecintoCerradoLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrecintoCerradoLabel.AutoSize = True
            Me.PrecintoCerradoLabel.ForeColor = System.Drawing.Color.Maroon
            Me.PrecintoCerradoLabel.Location = New System.Drawing.Point(405, 4)
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
            Me.ContenedorGroupBox.Size = New System.Drawing.Size(468, 527)
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
            Me.ContenedorSplitContainer.Size = New System.Drawing.Size(462, 507)
            Me.ContenedorSplitContainer.SplitterDistance = 320
            Me.ContenedorSplitContainer.TabIndex = 9004
            Me.ContenedorSplitContainer.TabStop = False
            '
            'ContenedorDatosGroupBox
            '
            Me.ContenedorDatosGroupBox.Controls.Add(Me.ContenedorCamposDinamicosPanel)
            Me.ContenedorDatosGroupBox.Controls.Add(Me.ContenedorDatosEstaticosPanel)
            Me.ContenedorDatosGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedorDatosGroupBox.Location = New System.Drawing.Point(3, 3)
            Me.ContenedorDatosGroupBox.Name = "ContenedorDatosGroupBox"
            Me.ContenedorDatosGroupBox.Size = New System.Drawing.Size(456, 314)
            Me.ContenedorDatosGroupBox.TabIndex = 2001
            Me.ContenedorDatosGroupBox.TabStop = False
            Me.ContenedorDatosGroupBox.Text = "Datos del contenedor"
            '
            'ContenedorCamposDinamicosPanel
            '
            Me.ContenedorCamposDinamicosPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedorCamposDinamicosPanel.Location = New System.Drawing.Point(3, 157)
            Me.ContenedorCamposDinamicosPanel.Name = "ContenedorCamposDinamicosPanel"
            Me.ContenedorCamposDinamicosPanel.Size = New System.Drawing.Size(450, 154)
            Me.ContenedorCamposDinamicosPanel.TabIndex = 2010
            '
            'ContenedorDatosEstaticosPanel
            '
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.CantidadDocumentosRecibidosTextBox)
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.CantidadDocumentosEnviadosTextBox)
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.CantidadDocumentosRecibidosLabel)
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.CantidadDocumentosEnviadosLabel)
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.ContenedorEsquemaComboBox)
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.ContenedorTokenTextBox)
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.ContenedorLabel)
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.ContenedorCerradoLabel)
            Me.ContenedorDatosEstaticosPanel.Controls.Add(Me.ContenedorEsquemaLabel)
            Me.ContenedorDatosEstaticosPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.ContenedorDatosEstaticosPanel.Location = New System.Drawing.Point(3, 17)
            Me.ContenedorDatosEstaticosPanel.Name = "ContenedorDatosEstaticosPanel"
            Me.ContenedorDatosEstaticosPanel.Size = New System.Drawing.Size(450, 140)
            Me.ContenedorDatosEstaticosPanel.TabIndex = 2002
            '
            'CantidadDocumentosRecibidosTextBox
            '
            Me.CantidadDocumentosRecibidosTextBox.Location = New System.Drawing.Point(101, 111)
            Me.CantidadDocumentosRecibidosTextBox.MaxLength = 4
            Me.CantidadDocumentosRecibidosTextBox.Name = "CantidadDocumentosRecibidosTextBox"
            Me.CantidadDocumentosRecibidosTextBox.Size = New System.Drawing.Size(81, 21)
            Me.CantidadDocumentosRecibidosTextBox.TabIndex = 5
            Me.CantidadDocumentosRecibidosTextBox.Visible = False
            '
            'CantidadDocumentosEnviadosTextBox
            '
            Me.CantidadDocumentosEnviadosTextBox.Location = New System.Drawing.Point(6, 111)
            Me.CantidadDocumentosEnviadosTextBox.MaxLength = 4
            Me.CantidadDocumentosEnviadosTextBox.Name = "CantidadDocumentosEnviadosTextBox"
            Me.CantidadDocumentosEnviadosTextBox.Size = New System.Drawing.Size(81, 21)
            Me.CantidadDocumentosEnviadosTextBox.TabIndex = 4
            Me.CantidadDocumentosEnviadosTextBox.Visible = False
            '
            'CantidadDocumentosRecibidosLabel
            '
            Me.CantidadDocumentosRecibidosLabel.AutoSize = True
            Me.CantidadDocumentosRecibidosLabel.ForeColor = System.Drawing.Color.Maroon
            Me.CantidadDocumentosRecibidosLabel.Location = New System.Drawing.Point(98, 89)
            Me.CantidadDocumentosRecibidosLabel.Name = "CantidadDocumentosRecibidosLabel"
            Me.CantidadDocumentosRecibidosLabel.Size = New System.Drawing.Size(75, 13)
            Me.CantidadDocumentosRecibidosLabel.TabIndex = 9004
            Me.CantidadDocumentosRecibidosLabel.Text = "Procesables"
            Me.CantidadDocumentosRecibidosLabel.Visible = False
            '
            'CantidadDocumentosEnviadosLabel
            '
            Me.CantidadDocumentosEnviadosLabel.AutoSize = True
            Me.CantidadDocumentosEnviadosLabel.ForeColor = System.Drawing.Color.Maroon
            Me.CantidadDocumentosEnviadosLabel.Location = New System.Drawing.Point(3, 89)
            Me.CantidadDocumentosEnviadosLabel.Name = "CantidadDocumentosEnviadosLabel"
            Me.CantidadDocumentosEnviadosLabel.Size = New System.Drawing.Size(57, 13)
            Me.CantidadDocumentosEnviadosLabel.TabIndex = 9003
            Me.CantidadDocumentosEnviadosLabel.Text = "Enviados"
            Me.CantidadDocumentosEnviadosLabel.Visible = False
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
            Me.ContenedorEsquemaComboBox.Size = New System.Drawing.Size(435, 21)
            Me.ContenedorEsquemaComboBox.TabIndex = 3
            '
            'ContenedorTokenTextBox
            '
            Me.ContenedorTokenTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ContenedorTokenTextBox.Location = New System.Drawing.Point(4, 21)
            Me.ContenedorTokenTextBox.MaxLength = 50
            Me.ContenedorTokenTextBox.Name = "ContenedorTokenTextBox"
            Me.ContenedorTokenTextBox.Size = New System.Drawing.Size(437, 21)
            Me.ContenedorTokenTextBox.TabIndex = 2
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
            Me.ContenedorCerradoLabel.Location = New System.Drawing.Point(392, 4)
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
            Me.ContenedorEsquemaLabel.TabIndex = 1
            Me.ContenedorEsquemaLabel.Text = "Esquema"
            '
            'ContenedoresDestapadosGroupBox
            '
            Me.ContenedoresDestapadosGroupBox.Controls.Add(Me.ContenedoresPrecintoPanel)
            Me.ContenedoresDestapadosGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedoresDestapadosGroupBox.Location = New System.Drawing.Point(3, 3)
            Me.ContenedoresDestapadosGroupBox.Name = "ContenedoresDestapadosGroupBox"
            Me.ContenedoresDestapadosGroupBox.Size = New System.Drawing.Size(456, 177)
            Me.ContenedoresDestapadosGroupBox.TabIndex = 2050
            Me.ContenedoresDestapadosGroupBox.TabStop = False
            Me.ContenedoresDestapadosGroupBox.Text = "Contenedores destapados en el precinto"
            '
            'ContenedoresPrecintoPanel
            '
            Me.ContenedoresPrecintoPanel.Controls.Add(Me.ContPanel)
            Me.ContenedoresPrecintoPanel.Controls.Add(Me.CantContenedorPanel)
            Me.ContenedoresPrecintoPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedoresPrecintoPanel.Location = New System.Drawing.Point(3, 17)
            Me.ContenedoresPrecintoPanel.Name = "ContenedoresPrecintoPanel"
            Me.ContenedoresPrecintoPanel.Size = New System.Drawing.Size(450, 157)
            Me.ContenedoresPrecintoPanel.TabIndex = 0
            '
            'ContPanel
            '
            Me.ContPanel.Controls.Add(Me.ContenedoresDataGridView)
            Me.ContPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContPanel.Location = New System.Drawing.Point(0, 21)
            Me.ContPanel.Name = "ContPanel"
            Me.ContPanel.Size = New System.Drawing.Size(450, 136)
            Me.ContPanel.TabIndex = 2
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
            Me.ContenedoresDataGridView.Size = New System.Drawing.Size(450, 136)
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
            Me.CantContenedorPanel.Size = New System.Drawing.Size(450, 21)
            Me.CantContenedorPanel.TabIndex = 1
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
            'WorkAreaTableLayoutPanel
            '
            Me.WorkAreaTableLayoutPanel.ColumnCount = 2
            Me.WorkAreaTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.0!))
            Me.WorkAreaTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.0!))
            Me.WorkAreaTableLayoutPanel.Controls.Add(Me.PrecintoGroupPanel, 0, 0)
            Me.WorkAreaTableLayoutPanel.Controls.Add(Me.ContenedorGroupPanel, 1, 0)
            Me.WorkAreaTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.WorkAreaTableLayoutPanel.Location = New System.Drawing.Point(5, 5)
            Me.WorkAreaTableLayoutPanel.Name = "WorkAreaTableLayoutPanel"
            Me.WorkAreaTableLayoutPanel.RowCount = 1
            Me.WorkAreaTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.WorkAreaTableLayoutPanel.Size = New System.Drawing.Size(947, 522)
            Me.WorkAreaTableLayoutPanel.TabIndex = 9001
            '
            'PrecintoGroupPanel
            '
            Me.PrecintoGroupPanel.Controls.Add(Me.PrecintoGroupBox)
            Me.PrecintoGroupPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.PrecintoGroupPanel.Location = New System.Drawing.Point(3, 3)
            Me.PrecintoGroupPanel.Name = "PrecintoGroupPanel"
            Me.PrecintoGroupPanel.Size = New System.Drawing.Size(467, 527)
            Me.PrecintoGroupPanel.TabIndex = 31
            '
            'ContenedorGroupPanel
            '
            Me.ContenedorGroupPanel.Controls.Add(Me.ContenedorGroupBox)
            Me.ContenedorGroupPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedorGroupPanel.Location = New System.Drawing.Point(476, 3)
            Me.ContenedorGroupPanel.Name = "ContenedorGroupPanel"
            Me.ContenedorGroupPanel.Size = New System.Drawing.Size(468, 527)
            Me.ContenedorGroupPanel.TabIndex = 32
            '
            'MenuPanel
            '
            Me.MenuPanel.Controls.Add(Me.ContenedorInsertarButton)
            Me.MenuPanel.Controls.Add(Me.PrecintoInsertarButton)
            Me.MenuPanel.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.MenuPanel.Location = New System.Drawing.Point(5, 537)
            Me.MenuPanel.Name = "MenuPanel"
            Me.MenuPanel.Padding = New System.Windows.Forms.Padding(5)
            Me.MenuPanel.Size = New System.Drawing.Size(957, 41)
            Me.MenuPanel.TabIndex = 31
            '
            'ContenedorInsertarButton
            '
            Me.ContenedorInsertarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ContenedorInsertarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.tick
            Me.ContenedorInsertarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ContenedorInsertarButton.Location = New System.Drawing.Point(490, 11)
            Me.ContenedorInsertarButton.Name = "ContenedorInsertarButton"
            Me.ContenedorInsertarButton.Size = New System.Drawing.Size(173, 23)
            Me.ContenedorInsertarButton.TabIndex = 2030
            Me.ContenedorInsertarButton.Text = "Actualizar Contenedor F3"
            Me.ContenedorInsertarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'WorkAreaPanel
            '
            Me.WorkAreaPanel.Controls.Add(Me.WorkAreaTableLayoutPanel)
            Me.WorkAreaPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.WorkAreaPanel.Location = New System.Drawing.Point(5, 5)
            Me.WorkAreaPanel.Name = "WorkAreaPanel"
            Me.WorkAreaPanel.Padding = New System.Windows.Forms.Padding(5)
            Me.WorkAreaPanel.Size = New System.Drawing.Size(957, 532)
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
            Me.WorkAreaSplitContainer.Size = New System.Drawing.Size(967, 583)
            Me.WorkAreaSplitContainer.SplitterDistance = 545
            Me.WorkAreaSplitContainer.TabIndex = 10001
            Me.WorkAreaSplitContainer.TabStop = False
            '
            'FormDestapeCorreccion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(967, 583)
            Me.Controls.Add(Me.WorkAreaSplitContainer)
            Me.Name = "FormDestapeCorreccion"
            Me.ShowIcon = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Destape"
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
            Me.ContPanel.ResumeLayout(False)
            CType(Me.ContenedoresDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.CantContenedorPanel.ResumeLayout(False)
            Me.CantContenedorPanel.PerformLayout()
            Me.WorkAreaTableLayoutPanel.ResumeLayout(False)
            Me.PrecintoGroupPanel.ResumeLayout(False)
            Me.ContenedorGroupPanel.ResumeLayout(False)
            Me.MenuPanel.ResumeLayout(False)
            Me.WorkAreaPanel.ResumeLayout(False)
            Me.WorkAreaSplitContainer.Panel1.ResumeLayout(False)
            CType(Me.WorkAreaSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.WorkAreaSplitContainer.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents PrecintoInsertarButton As System.Windows.Forms.Button
        Friend WithEvents PrecintoNumeroTextBox As System.Windows.Forms.TextBox
        Friend WithEvents PrecintoLabel As System.Windows.Forms.Label
        Friend WithEvents PrecintoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ContenedorGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents WorkAreaTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents PrecintoValidarButton As System.Windows.Forms.Button
        Friend WithEvents ContenedorSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents MenuPanel As System.Windows.Forms.Panel
        Friend WithEvents WorkAreaPanel As System.Windows.Forms.Panel
        Friend WithEvents WorkAreaSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents PrecintoCamposDinamicosPanel As System.Windows.Forms.Panel
        Friend WithEvents PrecintoCamposEstaticosPanel As System.Windows.Forms.Panel
        Friend WithEvents ContenedorDatosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ContenedoresDestapadosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ContenedorCamposDinamicosPanel As System.Windows.Forms.Panel
        Friend WithEvents ContenedorDatosEstaticosPanel As System.Windows.Forms.Panel
        Friend WithEvents ContenedorEsquemaLabel As System.Windows.Forms.Label
        Friend WithEvents ContenedorCerradoLabel As System.Windows.Forms.Label
        Friend WithEvents PrecintoCerradoLabel As System.Windows.Forms.Label
        Friend WithEvents ContenedorLabel As System.Windows.Forms.Label
        Friend WithEvents PrecintoGroupPanel As System.Windows.Forms.Panel
        Friend WithEvents ContenedorGroupPanel As System.Windows.Forms.Panel
        Friend WithEvents ContenedorTokenTextBox As System.Windows.Forms.TextBox
        Friend WithEvents ContenedorInsertarButton As System.Windows.Forms.Button
        Friend WithEvents ContenedoresPrecintoPanel As System.Windows.Forms.Panel
        Friend WithEvents ContenedorEsquemaComboBox As DesktopComboBoxControl
        Friend WithEvents ContenedoresDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents TokenCnt As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Esquema As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CantContenedorPanel As System.Windows.Forms.Panel
        Friend WithEvents CantContenedorLabel As System.Windows.Forms.Label
        Friend WithEvents CantidadLabel As System.Windows.Forms.Label
        Friend WithEvents ContPanel As System.Windows.Forms.Panel
        Friend WithEvents CantidadDocumentosRecibidosLabel As System.Windows.Forms.Label
        Friend WithEvents CantidadDocumentosEnviadosLabel As System.Windows.Forms.Label
        Friend WithEvents CantidadDocumentosRecibidosTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CantidadDocumentosEnviadosTextBox As System.Windows.Forms.TextBox
    End Class
End Namespace