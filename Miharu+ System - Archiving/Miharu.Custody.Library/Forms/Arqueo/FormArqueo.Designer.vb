Namespace Forms.Arqueo
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormArqueo
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormArqueo))
            Me.ArqueoTabControl = New System.Windows.Forms.TabControl()
            Me.TabPage1 = New System.Windows.Forms.TabPage()
            Me.FechaCierreCheckBox = New System.Windows.Forms.CheckBox()
            Me.FechaCreacionCheckBox = New System.Windows.Forms.CheckBox()
            Me.InactivosCheckBox = New System.Windows.Forms.CheckBox()
            Me.FechaCierreHastaDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaCierreDesdeDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaCreacionHastaDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaCreacionDesdeDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.ActivosCheckBox = New System.Windows.Forms.CheckBox()
            Me.CodigoTextBox = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.TabPage2 = New System.Windows.Forms.TabPage()
            Me.gvBase = New System.Windows.Forms.DataGridView()
            Me.TabPage3 = New System.Windows.Forms.TabPage()
            Me.ParametrosDataGridView = New System.Windows.Forms.DataGridView()
            Me.IdArqueoParametroDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Sede = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreBovedaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreBovedaSeccionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CodigoBovedaEstanteDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FilaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ProfundidadDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreEntidadDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreProyectoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CustodyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.dsCustody = New DBCore.CustodyDataSet()
            Me.RemoveParameterButton = New System.Windows.Forms.Button()
            Me.AddParameterButton = New System.Windows.Forms.Button()
            Me.Label13 = New System.Windows.Forms.Label()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.EditDescripcionTextBox = New System.Windows.Forms.TextBox()
            Me.EditModoComboBox = New System.Windows.Forms.ComboBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.EditActivoCheckBox = New System.Windows.Forms.CheckBox()
            Me.EditCodigoTextBox = New System.Windows.Forms.TextBox()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
            Me.BuscarToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.NuevoToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.GuardarToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.ReporteToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.InfoStatusStrip = New System.Windows.Forms.StatusStrip()
            Me.InformacionGeneralLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.InformacionLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.ArqueoTabControl.SuspendLayout()
            Me.TabPage1.SuspendLayout()
            Me.TabPage2.SuspendLayout()
            CType(Me.gvBase, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TabPage3.SuspendLayout()
            CType(Me.ParametrosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CustodyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dsCustody, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ToolStrip1.SuspendLayout()
            Me.InfoStatusStrip.SuspendLayout()
            Me.SuspendLayout()
            '
            'ArqueoTabControl
            '
            Me.ArqueoTabControl.AllowDrop = True
            Me.ArqueoTabControl.Controls.Add(Me.TabPage1)
            Me.ArqueoTabControl.Controls.Add(Me.TabPage2)
            Me.ArqueoTabControl.Controls.Add(Me.TabPage3)
            Me.ArqueoTabControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ArqueoTabControl.Location = New System.Drawing.Point(0, 55)
            Me.ArqueoTabControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.ArqueoTabControl.Name = "ArqueoTabControl"
            Me.ArqueoTabControl.SelectedIndex = 0
            Me.ArqueoTabControl.Size = New System.Drawing.Size(576, 374)
            Me.ArqueoTabControl.TabIndex = 0
            '
            'TabPage1
            '
            Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
            Me.TabPage1.Controls.Add(Me.FechaCierreCheckBox)
            Me.TabPage1.Controls.Add(Me.FechaCreacionCheckBox)
            Me.TabPage1.Controls.Add(Me.InactivosCheckBox)
            Me.TabPage1.Controls.Add(Me.FechaCierreHastaDateTimePicker)
            Me.TabPage1.Controls.Add(Me.FechaCierreDesdeDateTimePicker)
            Me.TabPage1.Controls.Add(Me.FechaCreacionHastaDateTimePicker)
            Me.TabPage1.Controls.Add(Me.FechaCreacionDesdeDateTimePicker)
            Me.TabPage1.Controls.Add(Me.ActivosCheckBox)
            Me.TabPage1.Controls.Add(Me.CodigoTextBox)
            Me.TabPage1.Controls.Add(Me.Label2)
            Me.TabPage1.Location = New System.Drawing.Point(4, 25)
            Me.TabPage1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.TabPage1.Name = "TabPage1"
            Me.TabPage1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.TabPage1.Size = New System.Drawing.Size(568, 345)
            Me.TabPage1.TabIndex = 0
            Me.TabPage1.Text = "Filtro"
            '
            'FechaCierreCheckBox
            '
            Me.FechaCierreCheckBox.AutoSize = True
            Me.FechaCierreCheckBox.Location = New System.Drawing.Point(59, 214)
            Me.FechaCierreCheckBox.Name = "FechaCierreCheckBox"
            Me.FechaCierreCheckBox.Size = New System.Drawing.Size(118, 20)
            Me.FechaCierreCheckBox.TabIndex = 25
            Me.FechaCierreCheckBox.Text = "Fecha de Cierre"
            Me.FechaCierreCheckBox.UseVisualStyleBackColor = True
            '
            'FechaCreacionCheckBox
            '
            Me.FechaCreacionCheckBox.AutoSize = True
            Me.FechaCreacionCheckBox.Location = New System.Drawing.Point(59, 123)
            Me.FechaCreacionCheckBox.Name = "FechaCreacionCheckBox"
            Me.FechaCreacionCheckBox.Size = New System.Drawing.Size(133, 20)
            Me.FechaCreacionCheckBox.TabIndex = 24
            Me.FechaCreacionCheckBox.Text = "Fecha de Creacion"
            Me.FechaCreacionCheckBox.UseVisualStyleBackColor = True
            '
            'InactivosCheckBox
            '
            Me.InactivosCheckBox.AutoSize = True
            Me.InactivosCheckBox.Location = New System.Drawing.Point(376, 72)
            Me.InactivosCheckBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.InactivosCheckBox.Name = "InactivosCheckBox"
            Me.InactivosCheckBox.Size = New System.Drawing.Size(85, 20)
            Me.InactivosCheckBox.TabIndex = 23
            Me.InactivosCheckBox.Text = "No activos"
            Me.InactivosCheckBox.UseVisualStyleBackColor = True
            '
            'FechaCierreHastaDateTimePicker
            '
            Me.FechaCierreHastaDateTimePicker.Enabled = False
            Me.FechaCierreHastaDateTimePicker.Location = New System.Drawing.Point(216, 254)
            Me.FechaCierreHastaDateTimePicker.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.FechaCierreHastaDateTimePicker.Name = "FechaCierreHastaDateTimePicker"
            Me.FechaCierreHastaDateTimePicker.Size = New System.Drawing.Size(297, 23)
            Me.FechaCierreHastaDateTimePicker.TabIndex = 22
            '
            'FechaCierreDesdeDateTimePicker
            '
            Me.FechaCierreDesdeDateTimePicker.Enabled = False
            Me.FechaCierreDesdeDateTimePicker.Location = New System.Drawing.Point(215, 211)
            Me.FechaCierreDesdeDateTimePicker.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.FechaCierreDesdeDateTimePicker.Name = "FechaCierreDesdeDateTimePicker"
            Me.FechaCierreDesdeDateTimePicker.Size = New System.Drawing.Size(297, 23)
            Me.FechaCierreDesdeDateTimePicker.TabIndex = 21
            '
            'FechaCreacionHastaDateTimePicker
            '
            Me.FechaCreacionHastaDateTimePicker.Enabled = False
            Me.FechaCreacionHastaDateTimePicker.Location = New System.Drawing.Point(215, 161)
            Me.FechaCreacionHastaDateTimePicker.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.FechaCreacionHastaDateTimePicker.Name = "FechaCreacionHastaDateTimePicker"
            Me.FechaCreacionHastaDateTimePicker.Size = New System.Drawing.Size(297, 23)
            Me.FechaCreacionHastaDateTimePicker.TabIndex = 20
            '
            'FechaCreacionDesdeDateTimePicker
            '
            Me.FechaCreacionDesdeDateTimePicker.Enabled = False
            Me.FechaCreacionDesdeDateTimePicker.Location = New System.Drawing.Point(215, 119)
            Me.FechaCreacionDesdeDateTimePicker.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.FechaCreacionDesdeDateTimePicker.Name = "FechaCreacionDesdeDateTimePicker"
            Me.FechaCreacionDesdeDateTimePicker.Size = New System.Drawing.Size(297, 23)
            Me.FechaCreacionDesdeDateTimePicker.TabIndex = 19
            '
            'ActivosCheckBox
            '
            Me.ActivosCheckBox.AutoSize = True
            Me.ActivosCheckBox.Location = New System.Drawing.Point(216, 72)
            Me.ActivosCheckBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.ActivosCheckBox.Name = "ActivosCheckBox"
            Me.ActivosCheckBox.Size = New System.Drawing.Size(67, 20)
            Me.ActivosCheckBox.TabIndex = 12
            Me.ActivosCheckBox.Text = "Activos"
            Me.ActivosCheckBox.UseVisualStyleBackColor = True
            '
            'CodigoTextBox
            '
            Me.CodigoTextBox.Location = New System.Drawing.Point(215, 32)
            Me.CodigoTextBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.CodigoTextBox.Name = "CodigoTextBox"
            Me.CodigoTextBox.Size = New System.Drawing.Size(298, 23)
            Me.CodigoTextBox.TabIndex = 3
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(56, 32)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(110, 16)
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "Código de Arqueo"
            '
            'TabPage2
            '
            Me.TabPage2.Controls.Add(Me.gvBase)
            Me.TabPage2.Location = New System.Drawing.Point(4, 25)
            Me.TabPage2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.TabPage2.Name = "TabPage2"
            Me.TabPage2.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.TabPage2.Size = New System.Drawing.Size(568, 345)
            Me.TabPage2.TabIndex = 1
            Me.TabPage2.Text = "Detalle"
            Me.TabPage2.UseVisualStyleBackColor = True
            '
            'gvBase
            '
            Me.gvBase.AllowUserToAddRows = False
            Me.gvBase.AllowUserToDeleteRows = False
            Me.gvBase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.gvBase.Dock = System.Windows.Forms.DockStyle.Fill
            Me.gvBase.Location = New System.Drawing.Point(3, 4)
            Me.gvBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.gvBase.Name = "gvBase"
            Me.gvBase.ReadOnly = True
            Me.gvBase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.gvBase.Size = New System.Drawing.Size(562, 337)
            Me.gvBase.TabIndex = 0
            '
            'TabPage3
            '
            Me.TabPage3.BackColor = System.Drawing.SystemColors.Control
            Me.TabPage3.Controls.Add(Me.ParametrosDataGridView)
            Me.TabPage3.Controls.Add(Me.RemoveParameterButton)
            Me.TabPage3.Controls.Add(Me.AddParameterButton)
            Me.TabPage3.Controls.Add(Me.Label13)
            Me.TabPage3.Controls.Add(Me.Label10)
            Me.TabPage3.Controls.Add(Me.EditDescripcionTextBox)
            Me.TabPage3.Controls.Add(Me.EditModoComboBox)
            Me.TabPage3.Controls.Add(Me.Label7)
            Me.TabPage3.Controls.Add(Me.EditActivoCheckBox)
            Me.TabPage3.Controls.Add(Me.EditCodigoTextBox)
            Me.TabPage3.Controls.Add(Me.Label8)
            Me.TabPage3.Location = New System.Drawing.Point(4, 25)
            Me.TabPage3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.TabPage3.Name = "TabPage3"
            Me.TabPage3.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.TabPage3.Size = New System.Drawing.Size(568, 345)
            Me.TabPage3.TabIndex = 2
            Me.TabPage3.Text = "Edicion"
            '
            'ParametrosDataGridView
            '
            Me.ParametrosDataGridView.AllowUserToAddRows = False
            Me.ParametrosDataGridView.AllowUserToDeleteRows = False
            Me.ParametrosDataGridView.AutoGenerateColumns = False
            Me.ParametrosDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
            Me.ParametrosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ParametrosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdArqueoParametroDataGridViewTextBoxColumn, Me.fk_Sede, Me.NombreBovedaDataGridViewTextBoxColumn, Me.NombreBovedaSeccionDataGridViewTextBoxColumn, Me.CodigoBovedaEstanteDataGridViewTextBoxColumn, Me.FilaDataGridViewTextBoxColumn, Me.ColumnaDataGridViewTextBoxColumn, Me.ProfundidadDataGridViewTextBoxColumn, Me.NombreEntidadDataGridViewTextBoxColumn, Me.NombreProyectoDataGridViewTextBoxColumn})
            Me.ParametrosDataGridView.DataMember = "CTA_Arqueo_Parametro_ESQ"
            Me.ParametrosDataGridView.DataSource = Me.CustodyBindingSource
            Me.ParametrosDataGridView.Location = New System.Drawing.Point(33, 193)
            Me.ParametrosDataGridView.Name = "ParametrosDataGridView"
            Me.ParametrosDataGridView.ReadOnly = True
            Me.ParametrosDataGridView.Size = New System.Drawing.Size(450, 125)
            Me.ParametrosDataGridView.TabIndex = 24
            '
            'IdArqueoParametroDataGridViewTextBoxColumn
            '
            Me.IdArqueoParametroDataGridViewTextBoxColumn.DataPropertyName = "id_Arqueo_Parametro"
            Me.IdArqueoParametroDataGridViewTextBoxColumn.HeaderText = "id"
            Me.IdArqueoParametroDataGridViewTextBoxColumn.Name = "IdArqueoParametroDataGridViewTextBoxColumn"
            Me.IdArqueoParametroDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdArqueoParametroDataGridViewTextBoxColumn.Width = 43
            '
            'fk_Sede
            '
            Me.fk_Sede.DataPropertyName = "fk_Sede"
            Me.fk_Sede.HeaderText = "Sede"
            Me.fk_Sede.Name = "fk_Sede"
            Me.fk_Sede.ReadOnly = True
            Me.fk_Sede.Width = 62
            '
            'NombreBovedaDataGridViewTextBoxColumn
            '
            Me.NombreBovedaDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Boveda"
            Me.NombreBovedaDataGridViewTextBoxColumn.HeaderText = "Boveda"
            Me.NombreBovedaDataGridViewTextBoxColumn.Name = "NombreBovedaDataGridViewTextBoxColumn"
            Me.NombreBovedaDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreBovedaDataGridViewTextBoxColumn.Width = 74
            '
            'NombreBovedaSeccionDataGridViewTextBoxColumn
            '
            Me.NombreBovedaSeccionDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Boveda_Seccion"
            Me.NombreBovedaSeccionDataGridViewTextBoxColumn.HeaderText = "Seccion"
            Me.NombreBovedaSeccionDataGridViewTextBoxColumn.Name = "NombreBovedaSeccionDataGridViewTextBoxColumn"
            Me.NombreBovedaSeccionDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreBovedaSeccionDataGridViewTextBoxColumn.Width = 77
            '
            'CodigoBovedaEstanteDataGridViewTextBoxColumn
            '
            Me.CodigoBovedaEstanteDataGridViewTextBoxColumn.DataPropertyName = "Codigo_Boveda_Estante"
            Me.CodigoBovedaEstanteDataGridViewTextBoxColumn.HeaderText = "Estante"
            Me.CodigoBovedaEstanteDataGridViewTextBoxColumn.Name = "CodigoBovedaEstanteDataGridViewTextBoxColumn"
            Me.CodigoBovedaEstanteDataGridViewTextBoxColumn.ReadOnly = True
            Me.CodigoBovedaEstanteDataGridViewTextBoxColumn.Width = 75
            '
            'FilaDataGridViewTextBoxColumn
            '
            Me.FilaDataGridViewTextBoxColumn.DataPropertyName = "Fila"
            Me.FilaDataGridViewTextBoxColumn.HeaderText = "Fila"
            Me.FilaDataGridViewTextBoxColumn.Name = "FilaDataGridViewTextBoxColumn"
            Me.FilaDataGridViewTextBoxColumn.ReadOnly = True
            Me.FilaDataGridViewTextBoxColumn.Width = 53
            '
            'ColumnaDataGridViewTextBoxColumn
            '
            Me.ColumnaDataGridViewTextBoxColumn.DataPropertyName = "Columna"
            Me.ColumnaDataGridViewTextBoxColumn.HeaderText = "Columna"
            Me.ColumnaDataGridViewTextBoxColumn.Name = "ColumnaDataGridViewTextBoxColumn"
            Me.ColumnaDataGridViewTextBoxColumn.ReadOnly = True
            Me.ColumnaDataGridViewTextBoxColumn.Width = 83
            '
            'ProfundidadDataGridViewTextBoxColumn
            '
            Me.ProfundidadDataGridViewTextBoxColumn.DataPropertyName = "Profundidad"
            Me.ProfundidadDataGridViewTextBoxColumn.HeaderText = "Profundidad"
            Me.ProfundidadDataGridViewTextBoxColumn.Name = "ProfundidadDataGridViewTextBoxColumn"
            Me.ProfundidadDataGridViewTextBoxColumn.ReadOnly = True
            Me.ProfundidadDataGridViewTextBoxColumn.Width = 101
            '
            'NombreEntidadDataGridViewTextBoxColumn
            '
            Me.NombreEntidadDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Entidad"
            Me.NombreEntidadDataGridViewTextBoxColumn.HeaderText = "Entidad"
            Me.NombreEntidadDataGridViewTextBoxColumn.Name = "NombreEntidadDataGridViewTextBoxColumn"
            Me.NombreEntidadDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreEntidadDataGridViewTextBoxColumn.Width = 75
            '
            'NombreProyectoDataGridViewTextBoxColumn
            '
            Me.NombreProyectoDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Proyecto"
            Me.NombreProyectoDataGridViewTextBoxColumn.HeaderText = "Proyecto"
            Me.NombreProyectoDataGridViewTextBoxColumn.Name = "NombreProyectoDataGridViewTextBoxColumn"
            Me.NombreProyectoDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreProyectoDataGridViewTextBoxColumn.Width = 82
            '
            'CustodyBindingSource
            '
            Me.CustodyBindingSource.DataSource = Me.dsCustody
            Me.CustodyBindingSource.Position = 0
            '
            'dsCustody
            '
            Me.dsCustody.DataSetName = "NewDataSet"
            '
            'RemoveParameterButton
            '
            Me.RemoveParameterButton.BackgroundImage = Global.Miharu.Custody.Library.My.Resources.Resources.RemoveParameter
            Me.RemoveParameterButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
            Me.RemoveParameterButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.RemoveParameterButton.Location = New System.Drawing.Point(489, 269)
            Me.RemoveParameterButton.Name = "RemoveParameterButton"
            Me.RemoveParameterButton.Size = New System.Drawing.Size(43, 49)
            Me.RemoveParameterButton.TabIndex = 27
            Me.RemoveParameterButton.UseVisualStyleBackColor = True
            '
            'AddParameterButton
            '
            Me.AddParameterButton.BackgroundImage = Global.Miharu.Custody.Library.My.Resources.Resources.AddParameter
            Me.AddParameterButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
            Me.AddParameterButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.AddParameterButton.Location = New System.Drawing.Point(489, 219)
            Me.AddParameterButton.Name = "AddParameterButton"
            Me.AddParameterButton.Size = New System.Drawing.Size(43, 49)
            Me.AddParameterButton.TabIndex = 26
            Me.AddParameterButton.UseVisualStyleBackColor = True
            '
            'Label13
            '
            Me.Label13.AutoSize = True
            Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label13.Location = New System.Drawing.Point(30, 174)
            Me.Label13.Name = "Label13"
            Me.Label13.Size = New System.Drawing.Size(137, 16)
            Me.Label13.TabIndex = 25
            Me.Label13.Text = "Parametros de Arqueo"
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label10.Location = New System.Drawing.Point(30, 91)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(73, 16)
            Me.Label10.TabIndex = 23
            Me.Label10.Text = "Descripción"
            '
            'EditDescripcionTextBox
            '
            Me.EditDescripcionTextBox.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EditDescripcionTextBox.Location = New System.Drawing.Point(189, 91)
            Me.EditDescripcionTextBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.EditDescripcionTextBox.Multiline = True
            Me.EditDescripcionTextBox.Name = "EditDescripcionTextBox"
            Me.EditDescripcionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.EditDescripcionTextBox.Size = New System.Drawing.Size(294, 82)
            Me.EditDescripcionTextBox.TabIndex = 22
            '
            'EditModoComboBox
            '
            Me.EditModoComboBox.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EditModoComboBox.FormattingEnabled = True
            Me.EditModoComboBox.Items.AddRange(New Object() {"Caja", "Carpeta", "Documento"})
            Me.EditModoComboBox.Location = New System.Drawing.Point(189, 51)
            Me.EditModoComboBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.EditModoComboBox.Name = "EditModoComboBox"
            Me.EditModoComboBox.Size = New System.Drawing.Size(294, 24)
            Me.EditModoComboBox.TabIndex = 21
            Me.EditModoComboBox.Text = "Seleccione ..."
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label7.Location = New System.Drawing.Point(30, 59)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(102, 16)
            Me.Label7.TabIndex = 20
            Me.Label7.Text = "Modo de Arqueo"
            '
            'EditActivoCheckBox
            '
            Me.EditActivoCheckBox.AutoSize = True
            Me.EditActivoCheckBox.Checked = True
            Me.EditActivoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
            Me.EditActivoCheckBox.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EditActivoCheckBox.Location = New System.Drawing.Point(489, 22)
            Me.EditActivoCheckBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.EditActivoCheckBox.Name = "EditActivoCheckBox"
            Me.EditActivoCheckBox.Size = New System.Drawing.Size(61, 20)
            Me.EditActivoCheckBox.TabIndex = 19
            Me.EditActivoCheckBox.Text = "Activo"
            Me.EditActivoCheckBox.UseVisualStyleBackColor = True
            '
            'EditCodigoTextBox
            '
            Me.EditCodigoTextBox.Enabled = False
            Me.EditCodigoTextBox.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EditCodigoTextBox.Location = New System.Drawing.Point(189, 19)
            Me.EditCodigoTextBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.EditCodigoTextBox.Name = "EditCodigoTextBox"
            Me.EditCodigoTextBox.Size = New System.Drawing.Size(294, 23)
            Me.EditCodigoTextBox.TabIndex = 18
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label8.Location = New System.Drawing.Point(30, 27)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(110, 16)
            Me.Label8.TabIndex = 17
            Me.Label8.Text = "Código de Arqueo"
            '
            'ToolStrip1
            '
            Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(48, 48)
            Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BuscarToolStripButton, Me.NuevoToolStripButton, Me.GuardarToolStripButton, Me.ReporteToolStripButton})
            Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
            Me.ToolStrip1.Name = "ToolStrip1"
            Me.ToolStrip1.Size = New System.Drawing.Size(576, 55)
            Me.ToolStrip1.TabIndex = 1
            Me.ToolStrip1.Text = "ToolStrip1"
            '
            'BuscarToolStripButton
            '
            Me.BuscarToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.BuscarToolStripButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.SearchBox
            Me.BuscarToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.BuscarToolStripButton.Name = "BuscarToolStripButton"
            Me.BuscarToolStripButton.Size = New System.Drawing.Size(52, 52)
            Me.BuscarToolStripButton.Text = "BuscarToolStripButton"
            Me.BuscarToolStripButton.ToolTipText = "Busca segun los parametros en la pestaña Filtro"
            '
            'NuevoToolStripButton
            '
            Me.NuevoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.NuevoToolStripButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.AddBox
            Me.NuevoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.NuevoToolStripButton.Name = "NuevoToolStripButton"
            Me.NuevoToolStripButton.Size = New System.Drawing.Size(52, 52)
            Me.NuevoToolStripButton.Text = "NuevoToolStripButton"
            Me.NuevoToolStripButton.ToolTipText = "Definir un nuevo arqueo"
            '
            'GuardarToolStripButton
            '
            Me.GuardarToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.GuardarToolStripButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.SaveBox
            Me.GuardarToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.GuardarToolStripButton.Name = "GuardarToolStripButton"
            Me.GuardarToolStripButton.Size = New System.Drawing.Size(52, 52)
            Me.GuardarToolStripButton.Text = "GuardarToolStripButton"
            Me.GuardarToolStripButton.ToolTipText = "Guardar la definición del arqueo actual"
            '
            'ReporteToolStripButton
            '
            Me.ReporteToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ReporteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ReporteToolStripButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.ReportBox
            Me.ReporteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ReporteToolStripButton.Name = "ReporteToolStripButton"
            Me.ReporteToolStripButton.Size = New System.Drawing.Size(52, 52)
            Me.ReporteToolStripButton.Text = "ReporteToolStripButton"
            Me.ReporteToolStripButton.ToolTipText = "Genera un Informe del Arqueo"
            '
            'InfoStatusStrip
            '
            Me.InfoStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InformacionGeneralLabel, Me.ToolStripStatusLabel1, Me.InformacionLabel})
            Me.InfoStatusStrip.Location = New System.Drawing.Point(0, 429)
            Me.InfoStatusStrip.Name = "InfoStatusStrip"
            Me.InfoStatusStrip.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
            Me.InfoStatusStrip.Size = New System.Drawing.Size(576, 22)
            Me.InfoStatusStrip.TabIndex = 2
            Me.InfoStatusStrip.Text = "StatusStrip1"
            '
            'InformacionGeneralLabel
            '
            Me.InformacionGeneralLabel.Name = "InformacionGeneralLabel"
            Me.InformacionGeneralLabel.Size = New System.Drawing.Size(104, 17)
            Me.InformacionGeneralLabel.Text = "Informacion General"
            '
            'ToolStripStatusLabel1
            '
            Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
            Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(11, 17)
            Me.ToolStripStatusLabel1.Text = "|"
            '
            'InformacionLabel
            '
            Me.InformacionLabel.Name = "InformacionLabel"
            Me.InformacionLabel.Size = New System.Drawing.Size(38, 17)
            Me.InformacionLabel.Text = "Status"
            Me.InformacionLabel.ToolTipText = "Estado del Proceso"
            '
            'FormArqueo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(576, 451)
            Me.Controls.Add(Me.ArqueoTabControl)
            Me.Controls.Add(Me.ToolStrip1)
            Me.Controls.Add(Me.InfoStatusStrip)
            Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.Name = "FormArqueo"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Arqueos"
            Me.ArqueoTabControl.ResumeLayout(False)
            Me.TabPage1.ResumeLayout(False)
            Me.TabPage1.PerformLayout()
            Me.TabPage2.ResumeLayout(False)
            CType(Me.gvBase, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TabPage3.ResumeLayout(False)
            Me.TabPage3.PerformLayout()
            CType(Me.ParametrosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CustodyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dsCustody, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ToolStrip1.ResumeLayout(False)
            Me.ToolStrip1.PerformLayout()
            Me.InfoStatusStrip.ResumeLayout(False)
            Me.InfoStatusStrip.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ArqueoTabControl As System.Windows.Forms.TabControl
        Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
        Friend WithEvents gvBase As System.Windows.Forms.DataGridView
        Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
        Friend WithEvents EditModoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents EditActivoCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents EditCodigoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
        Friend WithEvents ActivosCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents CodigoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
        Friend WithEvents NuevoToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents BuscarToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents GuardarToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents EditDescripcionTextBox As System.Windows.Forms.TextBox
        Friend WithEvents FechaCierreHastaDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaCierreDesdeDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaCreacionHastaDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaCreacionDesdeDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents InfoStatusStrip As System.Windows.Forms.StatusStrip
        Friend WithEvents InformacionLabel As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents RemoveParameterButton As System.Windows.Forms.Button
        Friend WithEvents AddParameterButton As System.Windows.Forms.Button
        Friend WithEvents Label13 As System.Windows.Forms.Label
        Friend WithEvents ParametrosDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents InactivosCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents FechaCreacionCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents FechaCierreCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents CustodyBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents dsCustody As DBCore.CustodyDataSet
        Friend WithEvents IdArqueoParametroDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Sede As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreBovedaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreBovedaSeccionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CodigoBovedaEstanteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FilaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ProfundidadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreEntidadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreProyectoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ReporteToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents InformacionGeneralLabel As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel

    End Class
End Namespace