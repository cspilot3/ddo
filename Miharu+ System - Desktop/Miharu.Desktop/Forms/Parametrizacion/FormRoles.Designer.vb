Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormRoles
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRoles))
            Me.rolesDataSet = New Miharu.Desktop.FormRolesDataSet()
            Me.rolesDataGridView = New System.Windows.Forms.DataGridView()
            Me.IdRolDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreRolDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DescripcionRolDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.esquemasDataGridView = New System.Windows.Forms.DataGridView()
            Me.FkRolDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdEntidadDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreEntidadDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdProyectoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreProyectoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdEsquemaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreEsquemaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.documentosDataGridView = New System.Windows.Forms.DataGridView()
            Me.categoriasDataGridView = New System.Windows.Forms.DataGridView()
            Me.addButton = New System.Windows.Forms.Button()
            Me.closeButton = New System.Windows.Forms.Button()
            Me.downSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.upSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.mainSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.deleteButton = New System.Windows.Forms.Button()
            Me.saveButton = New System.Windows.Forms.Button()
            Me.FkRolDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkEntidadDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkProyectoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkEsquemaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdDocumentoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreDocumentoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.VerRegistroDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.VerDataDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.VerImagenDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.DescargarDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.FkRolDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkEntidadDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkProyectoDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkEsquemaDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdCategoriaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreCategoriaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.SeleccionadoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            CType(Me.rolesDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.rolesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.esquemasDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.documentosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.categoriasDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.downSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.downSplitContainer.Panel1.SuspendLayout()
            Me.downSplitContainer.Panel2.SuspendLayout()
            Me.downSplitContainer.SuspendLayout()
            CType(Me.upSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.upSplitContainer.Panel1.SuspendLayout()
            Me.upSplitContainer.Panel2.SuspendLayout()
            Me.upSplitContainer.SuspendLayout()
            CType(Me.mainSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.mainSplitContainer.Panel1.SuspendLayout()
            Me.mainSplitContainer.Panel2.SuspendLayout()
            Me.mainSplitContainer.SuspendLayout()
            Me.SuspendLayout()
            '
            'rolesDataSet
            '
            Me.rolesDataSet.DataSetName = "FormRolesDataSet"
            Me.rolesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'rolesDataGridView
            '
            Me.rolesDataGridView.AllowUserToAddRows = False
            Me.rolesDataGridView.AllowUserToDeleteRows = False
            Me.rolesDataGridView.AutoGenerateColumns = False
            Me.rolesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.rolesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdRolDataGridViewTextBoxColumn, Me.NombreRolDataGridViewTextBoxColumn, Me.DescripcionRolDataGridViewTextBoxColumn})
            Me.rolesDataGridView.DataMember = "Rol"
            Me.rolesDataGridView.DataSource = Me.rolesDataSet
            Me.rolesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.rolesDataGridView.Location = New System.Drawing.Point(0, 23)
            Me.rolesDataGridView.Name = "rolesDataGridView"
            Me.rolesDataGridView.ReadOnly = True
            Me.rolesDataGridView.RowHeadersWidth = 20
            Me.rolesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.rolesDataGridView.Size = New System.Drawing.Size(459, 128)
            Me.rolesDataGridView.TabIndex = 0
            '
            'IdRolDataGridViewTextBoxColumn
            '
            Me.IdRolDataGridViewTextBoxColumn.DataPropertyName = "id_Rol"
            Me.IdRolDataGridViewTextBoxColumn.HeaderText = "id"
            Me.IdRolDataGridViewTextBoxColumn.Name = "IdRolDataGridViewTextBoxColumn"
            Me.IdRolDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdRolDataGridViewTextBoxColumn.Width = 40
            '
            'NombreRolDataGridViewTextBoxColumn
            '
            Me.NombreRolDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Rol"
            Me.NombreRolDataGridViewTextBoxColumn.HeaderText = "Rol"
            Me.NombreRolDataGridViewTextBoxColumn.Name = "NombreRolDataGridViewTextBoxColumn"
            Me.NombreRolDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreRolDataGridViewTextBoxColumn.Width = 200
            '
            'DescripcionRolDataGridViewTextBoxColumn
            '
            Me.DescripcionRolDataGridViewTextBoxColumn.DataPropertyName = "Descripcion_Rol"
            Me.DescripcionRolDataGridViewTextBoxColumn.HeaderText = "Descripción"
            Me.DescripcionRolDataGridViewTextBoxColumn.Name = "DescripcionRolDataGridViewTextBoxColumn"
            Me.DescripcionRolDataGridViewTextBoxColumn.ReadOnly = True
            Me.DescripcionRolDataGridViewTextBoxColumn.Width = 500
            '
            'esquemasDataGridView
            '
            Me.esquemasDataGridView.AllowUserToAddRows = False
            Me.esquemasDataGridView.AllowUserToDeleteRows = False
            Me.esquemasDataGridView.AutoGenerateColumns = False
            Me.esquemasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.esquemasDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FkRolDataGridViewTextBoxColumn, Me.IdEntidadDataGridViewTextBoxColumn, Me.NombreEntidadDataGridViewTextBoxColumn, Me.IdProyectoDataGridViewTextBoxColumn, Me.NombreProyectoDataGridViewTextBoxColumn, Me.IdEsquemaDataGridViewTextBoxColumn, Me.NombreEsquemaDataGridViewTextBoxColumn})
            Me.esquemasDataGridView.DataMember = "Rol.FK_Rol_Esquema"
            Me.esquemasDataGridView.DataSource = Me.rolesDataSet
            Me.esquemasDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.esquemasDataGridView.Location = New System.Drawing.Point(0, 23)
            Me.esquemasDataGridView.Name = "esquemasDataGridView"
            Me.esquemasDataGridView.ReadOnly = True
            Me.esquemasDataGridView.RowHeadersWidth = 20
            Me.esquemasDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.esquemasDataGridView.Size = New System.Drawing.Size(328, 128)
            Me.esquemasDataGridView.TabIndex = 1
            '
            'FkRolDataGridViewTextBoxColumn
            '
            Me.FkRolDataGridViewTextBoxColumn.DataPropertyName = "fk_Rol"
            Me.FkRolDataGridViewTextBoxColumn.HeaderText = "fk_Rol"
            Me.FkRolDataGridViewTextBoxColumn.Name = "FkRolDataGridViewTextBoxColumn"
            Me.FkRolDataGridViewTextBoxColumn.ReadOnly = True
            Me.FkRolDataGridViewTextBoxColumn.Visible = False
            '
            'IdEntidadDataGridViewTextBoxColumn
            '
            Me.IdEntidadDataGridViewTextBoxColumn.DataPropertyName = "id_Entidad"
            Me.IdEntidadDataGridViewTextBoxColumn.HeaderText = "id_Entidad"
            Me.IdEntidadDataGridViewTextBoxColumn.Name = "IdEntidadDataGridViewTextBoxColumn"
            Me.IdEntidadDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdEntidadDataGridViewTextBoxColumn.Visible = False
            '
            'NombreEntidadDataGridViewTextBoxColumn
            '
            Me.NombreEntidadDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Entidad"
            Me.NombreEntidadDataGridViewTextBoxColumn.HeaderText = "Entidad"
            Me.NombreEntidadDataGridViewTextBoxColumn.Name = "NombreEntidadDataGridViewTextBoxColumn"
            Me.NombreEntidadDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreEntidadDataGridViewTextBoxColumn.Width = 200
            '
            'IdProyectoDataGridViewTextBoxColumn
            '
            Me.IdProyectoDataGridViewTextBoxColumn.DataPropertyName = "id_Proyecto"
            Me.IdProyectoDataGridViewTextBoxColumn.HeaderText = "id_Proyecto"
            Me.IdProyectoDataGridViewTextBoxColumn.Name = "IdProyectoDataGridViewTextBoxColumn"
            Me.IdProyectoDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdProyectoDataGridViewTextBoxColumn.Visible = False
            '
            'NombreProyectoDataGridViewTextBoxColumn
            '
            Me.NombreProyectoDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Proyecto"
            Me.NombreProyectoDataGridViewTextBoxColumn.HeaderText = "Proyecto"
            Me.NombreProyectoDataGridViewTextBoxColumn.Name = "NombreProyectoDataGridViewTextBoxColumn"
            Me.NombreProyectoDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreProyectoDataGridViewTextBoxColumn.Width = 200
            '
            'IdEsquemaDataGridViewTextBoxColumn
            '
            Me.IdEsquemaDataGridViewTextBoxColumn.DataPropertyName = "id_Esquema"
            Me.IdEsquemaDataGridViewTextBoxColumn.HeaderText = "id_Esquema"
            Me.IdEsquemaDataGridViewTextBoxColumn.Name = "IdEsquemaDataGridViewTextBoxColumn"
            Me.IdEsquemaDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdEsquemaDataGridViewTextBoxColumn.Visible = False
            '
            'NombreEsquemaDataGridViewTextBoxColumn
            '
            Me.NombreEsquemaDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Esquema"
            Me.NombreEsquemaDataGridViewTextBoxColumn.HeaderText = "Esquema"
            Me.NombreEsquemaDataGridViewTextBoxColumn.Name = "NombreEsquemaDataGridViewTextBoxColumn"
            Me.NombreEsquemaDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreEsquemaDataGridViewTextBoxColumn.Width = 200
            '
            'documentosDataGridView
            '
            Me.documentosDataGridView.AllowUserToAddRows = False
            Me.documentosDataGridView.AllowUserToDeleteRows = False
            Me.documentosDataGridView.AutoGenerateColumns = False
            Me.documentosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.documentosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FkRolDataGridViewTextBoxColumn1, Me.FkEntidadDataGridViewTextBoxColumn, Me.FkProyectoDataGridViewTextBoxColumn, Me.FkEsquemaDataGridViewTextBoxColumn, Me.IdDocumentoDataGridViewTextBoxColumn, Me.NombreDocumentoDataGridViewTextBoxColumn, Me.VerRegistroDataGridViewCheckBoxColumn, Me.VerDataDataGridViewCheckBoxColumn, Me.VerImagenDataGridViewCheckBoxColumn, Me.DescargarDataGridViewCheckBoxColumn})
            Me.documentosDataGridView.DataMember = "Rol.FK_Rol_Esquema.FK_Esquema_Documento"
            Me.documentosDataGridView.DataSource = Me.rolesDataSet
            Me.documentosDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.documentosDataGridView.Location = New System.Drawing.Point(0, 23)
            Me.documentosDataGridView.Name = "documentosDataGridView"
            Me.documentosDataGridView.RowHeadersWidth = 20
            Me.documentosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.documentosDataGridView.Size = New System.Drawing.Size(390, 124)
            Me.documentosDataGridView.TabIndex = 2
            '
            'categoriasDataGridView
            '
            Me.categoriasDataGridView.AllowUserToAddRows = False
            Me.categoriasDataGridView.AllowUserToDeleteRows = False
            Me.categoriasDataGridView.AutoGenerateColumns = False
            Me.categoriasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.categoriasDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FkRolDataGridViewTextBoxColumn2, Me.FkEntidadDataGridViewTextBoxColumn1, Me.FkProyectoDataGridViewTextBoxColumn1, Me.FkEsquemaDataGridViewTextBoxColumn1, Me.IdCategoriaDataGridViewTextBoxColumn, Me.NombreCategoriaDataGridViewTextBoxColumn, Me.SeleccionadoDataGridViewCheckBoxColumn})
            Me.categoriasDataGridView.DataMember = "Rol.FK_Rol_Esquema.FK_Esquema_Categoria"
            Me.categoriasDataGridView.DataSource = Me.rolesDataSet
            Me.categoriasDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.categoriasDataGridView.Location = New System.Drawing.Point(0, 23)
            Me.categoriasDataGridView.Name = "categoriasDataGridView"
            Me.categoriasDataGridView.RowHeadersWidth = 20
            Me.categoriasDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.categoriasDataGridView.Size = New System.Drawing.Size(397, 124)
            Me.categoriasDataGridView.TabIndex = 3
            '
            'addButton
            '
            Me.addButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.addButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.addButton.Image = CType(resources.GetObject("addButton.Image"), System.Drawing.Image)
            Me.addButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.addButton.Location = New System.Drawing.Point(12, 320)
            Me.addButton.Name = "addButton"
            Me.addButton.Size = New System.Drawing.Size(105, 38)
            Me.addButton.TabIndex = 55
            Me.addButton.Text = "&Agregar"
            Me.addButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.addButton.UseVisualStyleBackColor = True
            '
            'closeButton
            '
            Me.closeButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.closeButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.closeButton.Image = CType(resources.GetObject("closeButton.Image"), System.Drawing.Image)
            Me.closeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.closeButton.Location = New System.Drawing.Point(698, 320)
            Me.closeButton.Name = "closeButton"
            Me.closeButton.Size = New System.Drawing.Size(105, 38)
            Me.closeButton.TabIndex = 56
            Me.closeButton.Text = "&Cerrar"
            Me.closeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.closeButton.UseVisualStyleBackColor = True
            '
            'downSplitContainer
            '
            Me.downSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.downSplitContainer.Location = New System.Drawing.Point(0, 0)
            Me.downSplitContainer.Name = "downSplitContainer"
            '
            'downSplitContainer.Panel1
            '
            Me.downSplitContainer.Panel1.Controls.Add(Me.documentosDataGridView)
            Me.downSplitContainer.Panel1.Controls.Add(Me.Label4)
            '
            'downSplitContainer.Panel2
            '
            Me.downSplitContainer.Panel2.Controls.Add(Me.categoriasDataGridView)
            Me.downSplitContainer.Panel2.Controls.Add(Me.Label3)
            Me.downSplitContainer.Size = New System.Drawing.Size(791, 147)
            Me.downSplitContainer.SplitterDistance = 390
            Me.downSplitContainer.TabIndex = 57
            '
            'Label4
            '
            Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(0, 0)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(390, 23)
            Me.Label4.TabIndex = 3
            Me.Label4.Text = "Documentos"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Label3
            '
            Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(0, 0)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(397, 23)
            Me.Label3.TabIndex = 4
            Me.Label3.Text = "Categorías"
            Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'upSplitContainer
            '
            Me.upSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.upSplitContainer.Location = New System.Drawing.Point(0, 0)
            Me.upSplitContainer.Name = "upSplitContainer"
            '
            'upSplitContainer.Panel1
            '
            Me.upSplitContainer.Panel1.Controls.Add(Me.rolesDataGridView)
            Me.upSplitContainer.Panel1.Controls.Add(Me.Label1)
            '
            'upSplitContainer.Panel2
            '
            Me.upSplitContainer.Panel2.Controls.Add(Me.esquemasDataGridView)
            Me.upSplitContainer.Panel2.Controls.Add(Me.Label2)
            Me.upSplitContainer.Size = New System.Drawing.Size(791, 151)
            Me.upSplitContainer.SplitterDistance = 459
            Me.upSplitContainer.TabIndex = 58
            '
            'Label1
            '
            Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(0, 0)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(459, 23)
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "Roles"
            Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Label2
            '
            Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(0, 0)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(328, 23)
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "Esquemas"
            Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'mainSplitContainer
            '
            Me.mainSplitContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.mainSplitContainer.Location = New System.Drawing.Point(12, 12)
            Me.mainSplitContainer.Name = "mainSplitContainer"
            Me.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'mainSplitContainer.Panel1
            '
            Me.mainSplitContainer.Panel1.Controls.Add(Me.upSplitContainer)
            '
            'mainSplitContainer.Panel2
            '
            Me.mainSplitContainer.Panel2.Controls.Add(Me.downSplitContainer)
            Me.mainSplitContainer.Size = New System.Drawing.Size(791, 302)
            Me.mainSplitContainer.SplitterDistance = 151
            Me.mainSplitContainer.TabIndex = 59
            '
            'deleteButton
            '
            Me.deleteButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.deleteButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.deleteButton.Image = Global.Miharu.Desktop.My.Resources.Resources.Cancelar
            Me.deleteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.deleteButton.Location = New System.Drawing.Point(123, 320)
            Me.deleteButton.Name = "deleteButton"
            Me.deleteButton.Size = New System.Drawing.Size(105, 38)
            Me.deleteButton.TabIndex = 60
            Me.deleteButton.Text = "&Eliminar"
            Me.deleteButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.deleteButton.UseVisualStyleBackColor = True
            '
            'saveButton
            '
            Me.saveButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.saveButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.saveButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.saveButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnGuardar
            Me.saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.saveButton.Location = New System.Drawing.Point(292, 320)
            Me.saveButton.Name = "saveButton"
            Me.saveButton.Size = New System.Drawing.Size(105, 38)
            Me.saveButton.TabIndex = 61
            Me.saveButton.Text = "&Guardar"
            Me.saveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.saveButton.UseVisualStyleBackColor = True
            '
            'FkRolDataGridViewTextBoxColumn1
            '
            Me.FkRolDataGridViewTextBoxColumn1.DataPropertyName = "fk_Rol"
            Me.FkRolDataGridViewTextBoxColumn1.HeaderText = "fk_Rol"
            Me.FkRolDataGridViewTextBoxColumn1.Name = "FkRolDataGridViewTextBoxColumn1"
            Me.FkRolDataGridViewTextBoxColumn1.ReadOnly = True
            Me.FkRolDataGridViewTextBoxColumn1.Visible = False
            '
            'FkEntidadDataGridViewTextBoxColumn
            '
            Me.FkEntidadDataGridViewTextBoxColumn.DataPropertyName = "fk_Entidad"
            Me.FkEntidadDataGridViewTextBoxColumn.HeaderText = "fk_Entidad"
            Me.FkEntidadDataGridViewTextBoxColumn.Name = "FkEntidadDataGridViewTextBoxColumn"
            Me.FkEntidadDataGridViewTextBoxColumn.ReadOnly = True
            Me.FkEntidadDataGridViewTextBoxColumn.Visible = False
            '
            'FkProyectoDataGridViewTextBoxColumn
            '
            Me.FkProyectoDataGridViewTextBoxColumn.DataPropertyName = "fk_Proyecto"
            Me.FkProyectoDataGridViewTextBoxColumn.HeaderText = "fk_Proyecto"
            Me.FkProyectoDataGridViewTextBoxColumn.Name = "FkProyectoDataGridViewTextBoxColumn"
            Me.FkProyectoDataGridViewTextBoxColumn.ReadOnly = True
            Me.FkProyectoDataGridViewTextBoxColumn.Visible = False
            '
            'FkEsquemaDataGridViewTextBoxColumn
            '
            Me.FkEsquemaDataGridViewTextBoxColumn.DataPropertyName = "fk_Esquema"
            Me.FkEsquemaDataGridViewTextBoxColumn.HeaderText = "fk_Esquema"
            Me.FkEsquemaDataGridViewTextBoxColumn.Name = "FkEsquemaDataGridViewTextBoxColumn"
            Me.FkEsquemaDataGridViewTextBoxColumn.ReadOnly = True
            Me.FkEsquemaDataGridViewTextBoxColumn.Visible = False
            '
            'IdDocumentoDataGridViewTextBoxColumn
            '
            Me.IdDocumentoDataGridViewTextBoxColumn.DataPropertyName = "id_Documento"
            Me.IdDocumentoDataGridViewTextBoxColumn.HeaderText = "id"
            Me.IdDocumentoDataGridViewTextBoxColumn.Name = "IdDocumentoDataGridViewTextBoxColumn"
            Me.IdDocumentoDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdDocumentoDataGridViewTextBoxColumn.Width = 50
            '
            'NombreDocumentoDataGridViewTextBoxColumn
            '
            Me.NombreDocumentoDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Documento"
            Me.NombreDocumentoDataGridViewTextBoxColumn.HeaderText = "Documento"
            Me.NombreDocumentoDataGridViewTextBoxColumn.Name = "NombreDocumentoDataGridViewTextBoxColumn"
            Me.NombreDocumentoDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreDocumentoDataGridViewTextBoxColumn.Width = 300
            '
            'VerRegistroDataGridViewCheckBoxColumn
            '
            Me.VerRegistroDataGridViewCheckBoxColumn.DataPropertyName = "Ver_Registro"
            Me.VerRegistroDataGridViewCheckBoxColumn.HeaderText = "Registro"
            Me.VerRegistroDataGridViewCheckBoxColumn.Name = "VerRegistroDataGridViewCheckBoxColumn"
            Me.VerRegistroDataGridViewCheckBoxColumn.Width = 50
            '
            'VerDataDataGridViewCheckBoxColumn
            '
            Me.VerDataDataGridViewCheckBoxColumn.DataPropertyName = "Ver_Data"
            Me.VerDataDataGridViewCheckBoxColumn.HeaderText = "Data"
            Me.VerDataDataGridViewCheckBoxColumn.Name = "VerDataDataGridViewCheckBoxColumn"
            Me.VerDataDataGridViewCheckBoxColumn.Width = 50
            '
            'VerImagenDataGridViewCheckBoxColumn
            '
            Me.VerImagenDataGridViewCheckBoxColumn.DataPropertyName = "Ver_Imagen"
            Me.VerImagenDataGridViewCheckBoxColumn.HeaderText = "Imagen"
            Me.VerImagenDataGridViewCheckBoxColumn.Name = "VerImagenDataGridViewCheckBoxColumn"
            Me.VerImagenDataGridViewCheckBoxColumn.Width = 50
            '
            'DescargarDataGridViewCheckBoxColumn
            '
            Me.DescargarDataGridViewCheckBoxColumn.DataPropertyName = "Descargar"
            Me.DescargarDataGridViewCheckBoxColumn.HeaderText = "Descargar"
            Me.DescargarDataGridViewCheckBoxColumn.Name = "DescargarDataGridViewCheckBoxColumn"
            Me.DescargarDataGridViewCheckBoxColumn.Width = 60
            '
            'FkRolDataGridViewTextBoxColumn2
            '
            Me.FkRolDataGridViewTextBoxColumn2.DataPropertyName = "fk_Rol"
            Me.FkRolDataGridViewTextBoxColumn2.HeaderText = "fk_Rol"
            Me.FkRolDataGridViewTextBoxColumn2.Name = "FkRolDataGridViewTextBoxColumn2"
            Me.FkRolDataGridViewTextBoxColumn2.ReadOnly = True
            Me.FkRolDataGridViewTextBoxColumn2.Visible = False
            '
            'FkEntidadDataGridViewTextBoxColumn1
            '
            Me.FkEntidadDataGridViewTextBoxColumn1.DataPropertyName = "fk_Entidad"
            Me.FkEntidadDataGridViewTextBoxColumn1.HeaderText = "fk_Entidad"
            Me.FkEntidadDataGridViewTextBoxColumn1.Name = "FkEntidadDataGridViewTextBoxColumn1"
            Me.FkEntidadDataGridViewTextBoxColumn1.ReadOnly = True
            Me.FkEntidadDataGridViewTextBoxColumn1.Visible = False
            '
            'FkProyectoDataGridViewTextBoxColumn1
            '
            Me.FkProyectoDataGridViewTextBoxColumn1.DataPropertyName = "fk_Proyecto"
            Me.FkProyectoDataGridViewTextBoxColumn1.HeaderText = "fk_Proyecto"
            Me.FkProyectoDataGridViewTextBoxColumn1.Name = "FkProyectoDataGridViewTextBoxColumn1"
            Me.FkProyectoDataGridViewTextBoxColumn1.ReadOnly = True
            Me.FkProyectoDataGridViewTextBoxColumn1.Visible = False
            '
            'FkEsquemaDataGridViewTextBoxColumn1
            '
            Me.FkEsquemaDataGridViewTextBoxColumn1.DataPropertyName = "fk_Esquema"
            Me.FkEsquemaDataGridViewTextBoxColumn1.HeaderText = "fk_Esquema"
            Me.FkEsquemaDataGridViewTextBoxColumn1.Name = "FkEsquemaDataGridViewTextBoxColumn1"
            Me.FkEsquemaDataGridViewTextBoxColumn1.ReadOnly = True
            Me.FkEsquemaDataGridViewTextBoxColumn1.Visible = False
            '
            'IdCategoriaDataGridViewTextBoxColumn
            '
            Me.IdCategoriaDataGridViewTextBoxColumn.DataPropertyName = "id_Categoria"
            Me.IdCategoriaDataGridViewTextBoxColumn.HeaderText = "id"
            Me.IdCategoriaDataGridViewTextBoxColumn.Name = "IdCategoriaDataGridViewTextBoxColumn"
            Me.IdCategoriaDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdCategoriaDataGridViewTextBoxColumn.Width = 40
            '
            'NombreCategoriaDataGridViewTextBoxColumn
            '
            Me.NombreCategoriaDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Categoria"
            Me.NombreCategoriaDataGridViewTextBoxColumn.HeaderText = "Categoría"
            Me.NombreCategoriaDataGridViewTextBoxColumn.Name = "NombreCategoriaDataGridViewTextBoxColumn"
            Me.NombreCategoriaDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreCategoriaDataGridViewTextBoxColumn.Width = 300
            '
            'SeleccionadoDataGridViewCheckBoxColumn
            '
            Me.SeleccionadoDataGridViewCheckBoxColumn.DataPropertyName = "Seleccionado"
            Me.SeleccionadoDataGridViewCheckBoxColumn.HeaderText = "Seleccionado"
            Me.SeleccionadoDataGridViewCheckBoxColumn.Name = "SeleccionadoDataGridViewCheckBoxColumn"
            Me.SeleccionadoDataGridViewCheckBoxColumn.Width = 80
            '
            'FormRoles
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.closeButton
            Me.ClientSize = New System.Drawing.Size(815, 370)
            Me.Controls.Add(Me.saveButton)
            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.mainSplitContainer)
            Me.Controls.Add(Me.addButton)
            Me.Controls.Add(Me.closeButton)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormRoles"
            Me.Text = "Roles"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            CType(Me.rolesDataSet, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.rolesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.esquemasDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.documentosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.categoriasDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.downSplitContainer.Panel1.ResumeLayout(False)
            Me.downSplitContainer.Panel2.ResumeLayout(False)
            CType(Me.downSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.downSplitContainer.ResumeLayout(False)
            Me.upSplitContainer.Panel1.ResumeLayout(False)
            Me.upSplitContainer.Panel2.ResumeLayout(False)
            CType(Me.upSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.upSplitContainer.ResumeLayout(False)
            Me.mainSplitContainer.Panel1.ResumeLayout(False)
            Me.mainSplitContainer.Panel2.ResumeLayout(False)
            CType(Me.mainSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.mainSplitContainer.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents rolesDataSet As Miharu.Desktop.FormRolesDataSet
        Friend WithEvents rolesDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents esquemasDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents documentosDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents categoriasDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents addButton As System.Windows.Forms.Button
        Friend WithEvents closeButton As System.Windows.Forms.Button
        Friend WithEvents IdRolDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreRolDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DescripcionRolDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents downSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents upSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents mainSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents deleteButton As System.Windows.Forms.Button
        Friend WithEvents saveButton As System.Windows.Forms.Button
        Friend WithEvents FkRolDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdEntidadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreEntidadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdProyectoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreProyectoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdEsquemaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreEsquemaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents FkRolDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkEntidadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkProyectoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkEsquemaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdDocumentoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreDocumentoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents VerRegistroDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents VerDataDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents VerImagenDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents DescargarDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents FkRolDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkEntidadDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkProyectoDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkEsquemaDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdCategoriaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreCategoriaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents SeleccionadoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace