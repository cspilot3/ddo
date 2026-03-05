Namespace Imaging.Forms.Cargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCargue
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCargue))
            Me.lblTitulo = New System.Windows.Forms.Label()
            Me.pnlBottom = New System.Windows.Forms.Panel()
            Me.pnlBottomLeft = New System.Windows.Forms.Panel()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
            Me.pnlDetallePaquetes = New System.Windows.Forms.Panel()
            Me.dgvPaquetes = New System.Windows.Forms.DataGridView()
            Me.IdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.PathDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ValidosDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.InvalidosDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TotalDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.PaqueteValido = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.dsCargue = New DBImaging.Esquemas.xsdCargue()
            Me.pnlPaquetes = New System.Windows.Forms.Panel()
            Me.lblPaquetes = New System.Windows.Forms.Label()
            Me.pnlEstadistica = New System.Windows.Forms.Panel()
            Me.lblInvalidos = New System.Windows.Forms.Label()
            Me.lblValidos = New System.Windows.Forms.Label()
            Me.lblTotal = New System.Windows.Forms.Label()
            Me.lblInvalidosE = New System.Windows.Forms.Label()
            Me.lblValidosE = New System.Windows.Forms.Label()
            Me.lblTotalE = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.dgvItems = New System.Windows.Forms.DataGridView()
            Me.IdDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.PathDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FoliosDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TamañoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.KeyDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ValidoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.DescripcionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlItems = New System.Windows.Forms.Panel()
            Me.lblItems = New System.Windows.Forms.Label()
            Me.pnlBottom.SuspendLayout()
            Me.pnlBottomLeft.SuspendLayout()
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.pnlDetallePaquetes.SuspendLayout()
            CType(Me.dgvPaquetes, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dsCargue, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlPaquetes.SuspendLayout()
            Me.pnlEstadistica.SuspendLayout()
            CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlItems.SuspendLayout()
            Me.SuspendLayout()
            '
            'lblTitulo
            '
            Me.lblTitulo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTitulo.ForeColor = System.Drawing.Color.SteelBlue
            Me.lblTitulo.Location = New System.Drawing.Point(0, 0)
            Me.lblTitulo.Margin = New System.Windows.Forms.Padding(3, 0, 3, 5)
            Me.lblTitulo.Name = "lblTitulo"
            Me.lblTitulo.Size = New System.Drawing.Size(823, 40)
            Me.lblTitulo.TabIndex = 2
            Me.lblTitulo.Text = "Resultado del cargue"
            Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlBottom
            '
            Me.pnlBottom.Controls.Add(Me.pnlBottomLeft)
            Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlBottom.Location = New System.Drawing.Point(0, 452)
            Me.pnlBottom.Name = "pnlBottom"
            Me.pnlBottom.Size = New System.Drawing.Size(823, 49)
            Me.pnlBottom.TabIndex = 3
            '
            'pnlBottomLeft
            '
            Me.pnlBottomLeft.Controls.Add(Me.CancelarButton)
            Me.pnlBottomLeft.Controls.Add(Me.AceptarButton)
            Me.pnlBottomLeft.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlBottomLeft.Location = New System.Drawing.Point(623, 0)
            Me.pnlBottomLeft.Name = "pnlBottomLeft"
            Me.pnlBottomLeft.Size = New System.Drawing.Size(200, 49)
            Me.pnlBottomLeft.TabIndex = 5
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.cancel
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(111, 13)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 24)
            Me.CancelarButton.TabIndex = 4
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'AceptarButton
            '
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.tick
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(15, 13)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 23)
            Me.AceptarButton.TabIndex = 3
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'SplitContainer1
            '
            Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer1.Location = New System.Drawing.Point(0, 40)
            Me.SplitContainer1.Name = "SplitContainer1"
            Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.pnlDetallePaquetes)
            Me.SplitContainer1.Panel1.Controls.Add(Me.pnlEstadistica)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.dgvItems)
            Me.SplitContainer1.Panel2.Controls.Add(Me.pnlItems)
            Me.SplitContainer1.Size = New System.Drawing.Size(823, 412)
            Me.SplitContainer1.SplitterDistance = 170
            Me.SplitContainer1.TabIndex = 5
            '
            'pnlDetallePaquetes
            '
            Me.pnlDetallePaquetes.Controls.Add(Me.dgvPaquetes)
            Me.pnlDetallePaquetes.Controls.Add(Me.pnlPaquetes)
            Me.pnlDetallePaquetes.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlDetallePaquetes.Location = New System.Drawing.Point(0, 0)
            Me.pnlDetallePaquetes.Name = "pnlDetallePaquetes"
            Me.pnlDetallePaquetes.Size = New System.Drawing.Size(661, 168)
            Me.pnlDetallePaquetes.TabIndex = 7
            '
            'dgvPaquetes
            '
            Me.dgvPaquetes.AllowUserToAddRows = False
            Me.dgvPaquetes.AllowUserToDeleteRows = False
            Me.dgvPaquetes.AllowUserToResizeRows = False
            Me.dgvPaquetes.AutoGenerateColumns = False
            Me.dgvPaquetes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvPaquetes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdDataGridViewTextBoxColumn, Me.PathDataGridViewTextBoxColumn, Me.ValidosDataGridViewTextBoxColumn, Me.InvalidosDataGridViewTextBoxColumn, Me.TotalDataGridViewTextBoxColumn, Me.PaqueteValido, Me.Descripcion})
            Me.dgvPaquetes.DataMember = "Paquete"
            Me.dgvPaquetes.DataSource = Me.dsCargue
            Me.dgvPaquetes.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvPaquetes.Location = New System.Drawing.Point(0, 25)
            Me.dgvPaquetes.Name = "dgvPaquetes"
            Me.dgvPaquetes.ReadOnly = True
            Me.dgvPaquetes.Size = New System.Drawing.Size(661, 143)
            Me.dgvPaquetes.TabIndex = 6
            '
            'IdDataGridViewTextBoxColumn
            '
            Me.IdDataGridViewTextBoxColumn.DataPropertyName = "id"
            Me.IdDataGridViewTextBoxColumn.HeaderText = ""
            Me.IdDataGridViewTextBoxColumn.Name = "IdDataGridViewTextBoxColumn"
            Me.IdDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdDataGridViewTextBoxColumn.Width = 40
            '
            'PathDataGridViewTextBoxColumn
            '
            Me.PathDataGridViewTextBoxColumn.DataPropertyName = "Path"
            Me.PathDataGridViewTextBoxColumn.HeaderText = "Path"
            Me.PathDataGridViewTextBoxColumn.Name = "PathDataGridViewTextBoxColumn"
            Me.PathDataGridViewTextBoxColumn.ReadOnly = True
            Me.PathDataGridViewTextBoxColumn.Width = 200
            '
            'ValidosDataGridViewTextBoxColumn
            '
            Me.ValidosDataGridViewTextBoxColumn.DataPropertyName = "Validos"
            Me.ValidosDataGridViewTextBoxColumn.HeaderText = "Válidos"
            Me.ValidosDataGridViewTextBoxColumn.Name = "ValidosDataGridViewTextBoxColumn"
            Me.ValidosDataGridViewTextBoxColumn.ReadOnly = True
            Me.ValidosDataGridViewTextBoxColumn.Width = 60
            '
            'InvalidosDataGridViewTextBoxColumn
            '
            Me.InvalidosDataGridViewTextBoxColumn.DataPropertyName = "Invalidos"
            Me.InvalidosDataGridViewTextBoxColumn.HeaderText = "Inválidos"
            Me.InvalidosDataGridViewTextBoxColumn.Name = "InvalidosDataGridViewTextBoxColumn"
            Me.InvalidosDataGridViewTextBoxColumn.ReadOnly = True
            Me.InvalidosDataGridViewTextBoxColumn.Width = 60
            '
            'TotalDataGridViewTextBoxColumn
            '
            Me.TotalDataGridViewTextBoxColumn.DataPropertyName = "Total"
            Me.TotalDataGridViewTextBoxColumn.HeaderText = "Total"
            Me.TotalDataGridViewTextBoxColumn.Name = "TotalDataGridViewTextBoxColumn"
            Me.TotalDataGridViewTextBoxColumn.ReadOnly = True
            Me.TotalDataGridViewTextBoxColumn.Width = 60
            '
            'PaqueteValido
            '
            Me.PaqueteValido.DataPropertyName = "PaqueteValido"
            Me.PaqueteValido.FillWeight = 80.0!
            Me.PaqueteValido.HeaderText = "Paq. Válido"
            Me.PaqueteValido.Name = "PaqueteValido"
            Me.PaqueteValido.ReadOnly = True
            Me.PaqueteValido.Width = 80
            '
            'Descripcion
            '
            Me.Descripcion.DataPropertyName = "Descripcion"
            Me.Descripcion.HeaderText = "Descripción"
            Me.Descripcion.Name = "Descripcion"
            Me.Descripcion.ReadOnly = True
            Me.Descripcion.Width = 200
            '
            'dsCargue
            '
            Me.dsCargue.DataSetName = "xsdCargue"
            Me.dsCargue.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'pnlPaquetes
            '
            Me.pnlPaquetes.BackColor = System.Drawing.Color.SteelBlue
            Me.pnlPaquetes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlPaquetes.Controls.Add(Me.lblPaquetes)
            Me.pnlPaquetes.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlPaquetes.Location = New System.Drawing.Point(0, 0)
            Me.pnlPaquetes.Name = "pnlPaquetes"
            Me.pnlPaquetes.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
            Me.pnlPaquetes.Size = New System.Drawing.Size(661, 25)
            Me.pnlPaquetes.TabIndex = 0
            '
            'lblPaquetes
            '
            Me.lblPaquetes.Dock = System.Windows.Forms.DockStyle.Left
            Me.lblPaquetes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblPaquetes.ForeColor = System.Drawing.Color.Transparent
            Me.lblPaquetes.Location = New System.Drawing.Point(10, 0)
            Me.lblPaquetes.Name = "lblPaquetes"
            Me.lblPaquetes.Size = New System.Drawing.Size(365, 23)
            Me.lblPaquetes.TabIndex = 0
            Me.lblPaquetes.Text = "Paquetes"
            Me.lblPaquetes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'pnlEstadistica
            '
            Me.pnlEstadistica.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.pnlEstadistica.Controls.Add(Me.lblInvalidos)
            Me.pnlEstadistica.Controls.Add(Me.lblValidos)
            Me.pnlEstadistica.Controls.Add(Me.lblTotal)
            Me.pnlEstadistica.Controls.Add(Me.lblInvalidosE)
            Me.pnlEstadistica.Controls.Add(Me.lblValidosE)
            Me.pnlEstadistica.Controls.Add(Me.lblTotalE)
            Me.pnlEstadistica.Controls.Add(Me.Label1)
            Me.pnlEstadistica.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlEstadistica.Location = New System.Drawing.Point(661, 0)
            Me.pnlEstadistica.Name = "pnlEstadistica"
            Me.pnlEstadistica.Size = New System.Drawing.Size(160, 168)
            Me.pnlEstadistica.TabIndex = 8
            '
            'lblInvalidos
            '
            Me.lblInvalidos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblInvalidos.ForeColor = System.Drawing.Color.Red
            Me.lblInvalidos.Location = New System.Drawing.Point(67, 64)
            Me.lblInvalidos.Name = "lblInvalidos"
            Me.lblInvalidos.Size = New System.Drawing.Size(80, 13)
            Me.lblInvalidos.TabIndex = 6
            Me.lblInvalidos.Text = "0"
            Me.lblInvalidos.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'lblValidos
            '
            Me.lblValidos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblValidos.ForeColor = System.Drawing.Color.Green
            Me.lblValidos.Location = New System.Drawing.Point(67, 38)
            Me.lblValidos.Name = "lblValidos"
            Me.lblValidos.Size = New System.Drawing.Size(80, 13)
            Me.lblValidos.TabIndex = 5
            Me.lblValidos.Text = "0"
            Me.lblValidos.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'lblTotal
            '
            Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTotal.ForeColor = System.Drawing.Color.Blue
            Me.lblTotal.Location = New System.Drawing.Point(67, 90)
            Me.lblTotal.Name = "lblTotal"
            Me.lblTotal.Size = New System.Drawing.Size(80, 13)
            Me.lblTotal.TabIndex = 4
            Me.lblTotal.Text = "0"
            Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'lblInvalidosE
            '
            Me.lblInvalidosE.AutoSize = True
            Me.lblInvalidosE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblInvalidosE.Location = New System.Drawing.Point(6, 64)
            Me.lblInvalidosE.Name = "lblInvalidosE"
            Me.lblInvalidosE.Size = New System.Drawing.Size(62, 13)
            Me.lblInvalidosE.TabIndex = 3
            Me.lblInvalidosE.Text = "Inválidos:"
            '
            'lblValidosE
            '
            Me.lblValidosE.AutoSize = True
            Me.lblValidosE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblValidosE.Location = New System.Drawing.Point(6, 38)
            Me.lblValidosE.Name = "lblValidosE"
            Me.lblValidosE.Size = New System.Drawing.Size(52, 13)
            Me.lblValidosE.TabIndex = 2
            Me.lblValidosE.Text = "Válidos:"
            '
            'lblTotalE
            '
            Me.lblTotalE.AutoSize = True
            Me.lblTotalE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTotalE.Location = New System.Drawing.Point(6, 90)
            Me.lblTotalE.Name = "lblTotalE"
            Me.lblTotalE.Size = New System.Drawing.Size(44, 13)
            Me.lblTotalE.TabIndex = 1
            Me.lblTotalE.Text = "Total: "
            '
            'Label1
            '
            Me.Label1.BackColor = System.Drawing.Color.Gray
            Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.White
            Me.Label1.Location = New System.Drawing.Point(0, 0)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(156, 25)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Cargue"
            Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'dgvItems
            '
            Me.dgvItems.AllowUserToAddRows = False
            Me.dgvItems.AllowUserToDeleteRows = False
            Me.dgvItems.AllowUserToResizeRows = False
            Me.dgvItems.AutoGenerateColumns = False
            Me.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdDataGridViewTextBoxColumn1, Me.PathDataGridViewTextBoxColumn1, Me.FoliosDataGridViewTextBoxColumn, Me.TamañoDataGridViewTextBoxColumn, Me.KeyDataGridViewTextBoxColumn, Me.ValidoDataGridViewCheckBoxColumn, Me.DescripcionDataGridViewTextBoxColumn})
            Me.dgvItems.DataMember = "Paquete.Paquete_Item"
            Me.dgvItems.DataSource = Me.dsCargue
            Me.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvItems.Location = New System.Drawing.Point(0, 25)
            Me.dgvItems.Name = "dgvItems"
            Me.dgvItems.ReadOnly = True
            Me.dgvItems.Size = New System.Drawing.Size(821, 211)
            Me.dgvItems.TabIndex = 5
            '
            'IdDataGridViewTextBoxColumn1
            '
            Me.IdDataGridViewTextBoxColumn1.DataPropertyName = "id"
            Me.IdDataGridViewTextBoxColumn1.HeaderText = ""
            Me.IdDataGridViewTextBoxColumn1.Name = "IdDataGridViewTextBoxColumn1"
            Me.IdDataGridViewTextBoxColumn1.ReadOnly = True
            Me.IdDataGridViewTextBoxColumn1.Width = 40
            '
            'PathDataGridViewTextBoxColumn1
            '
            Me.PathDataGridViewTextBoxColumn1.DataPropertyName = "Path"
            Me.PathDataGridViewTextBoxColumn1.HeaderText = "Nombre"
            Me.PathDataGridViewTextBoxColumn1.Name = "PathDataGridViewTextBoxColumn1"
            Me.PathDataGridViewTextBoxColumn1.ReadOnly = True
            Me.PathDataGridViewTextBoxColumn1.Width = 200
            '
            'FoliosDataGridViewTextBoxColumn
            '
            Me.FoliosDataGridViewTextBoxColumn.DataPropertyName = "Folios"
            Me.FoliosDataGridViewTextBoxColumn.HeaderText = "Folios"
            Me.FoliosDataGridViewTextBoxColumn.Name = "FoliosDataGridViewTextBoxColumn"
            Me.FoliosDataGridViewTextBoxColumn.ReadOnly = True
            Me.FoliosDataGridViewTextBoxColumn.Width = 40
            '
            'TamañoDataGridViewTextBoxColumn
            '
            Me.TamañoDataGridViewTextBoxColumn.DataPropertyName = "Tamaño"
            DataGridViewCellStyle1.Format = "N0"
            DataGridViewCellStyle1.NullValue = Nothing
            Me.TamañoDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle1
            Me.TamañoDataGridViewTextBoxColumn.HeaderText = "Tamaño (B)"
            Me.TamañoDataGridViewTextBoxColumn.Name = "TamañoDataGridViewTextBoxColumn"
            Me.TamañoDataGridViewTextBoxColumn.ReadOnly = True
            '
            'KeyDataGridViewTextBoxColumn
            '
            Me.KeyDataGridViewTextBoxColumn.DataPropertyName = "Key"
            Me.KeyDataGridViewTextBoxColumn.HeaderText = "Key"
            Me.KeyDataGridViewTextBoxColumn.Name = "KeyDataGridViewTextBoxColumn"
            Me.KeyDataGridViewTextBoxColumn.ReadOnly = True
            '
            'ValidoDataGridViewCheckBoxColumn
            '
            Me.ValidoDataGridViewCheckBoxColumn.DataPropertyName = "Valido"
            Me.ValidoDataGridViewCheckBoxColumn.HeaderText = "Valido"
            Me.ValidoDataGridViewCheckBoxColumn.Name = "ValidoDataGridViewCheckBoxColumn"
            Me.ValidoDataGridViewCheckBoxColumn.ReadOnly = True
            Me.ValidoDataGridViewCheckBoxColumn.Width = 40
            '
            'DescripcionDataGridViewTextBoxColumn
            '
            Me.DescripcionDataGridViewTextBoxColumn.DataPropertyName = "Descripcion"
            Me.DescripcionDataGridViewTextBoxColumn.HeaderText = "Descripcion"
            Me.DescripcionDataGridViewTextBoxColumn.Name = "DescripcionDataGridViewTextBoxColumn"
            Me.DescripcionDataGridViewTextBoxColumn.ReadOnly = True
            Me.DescripcionDataGridViewTextBoxColumn.Width = 400
            '
            'pnlItems
            '
            Me.pnlItems.BackColor = System.Drawing.Color.SteelBlue
            Me.pnlItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlItems.Controls.Add(Me.lblItems)
            Me.pnlItems.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlItems.Location = New System.Drawing.Point(0, 0)
            Me.pnlItems.Name = "pnlItems"
            Me.pnlItems.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
            Me.pnlItems.Size = New System.Drawing.Size(821, 25)
            Me.pnlItems.TabIndex = 1
            '
            'lblItems
            '
            Me.lblItems.Dock = System.Windows.Forms.DockStyle.Left
            Me.lblItems.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblItems.ForeColor = System.Drawing.Color.Transparent
            Me.lblItems.Location = New System.Drawing.Point(10, 0)
            Me.lblItems.Name = "lblItems"
            Me.lblItems.Size = New System.Drawing.Size(380, 23)
            Me.lblItems.TabIndex = 0
            Me.lblItems.Text = "Items"
            Me.lblItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'FormCargue
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(823, 501)
            Me.Controls.Add(Me.SplitContainer1)
            Me.Controls.Add(Me.pnlBottom)
            Me.Controls.Add(Me.lblTitulo)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormCargue"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Cargue"
            Me.TopMost = True
            Me.pnlBottom.ResumeLayout(False)
            Me.pnlBottomLeft.ResumeLayout(False)
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.ResumeLayout(False)
            Me.pnlDetallePaquetes.ResumeLayout(False)
            CType(Me.dgvPaquetes, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dsCargue, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlPaquetes.ResumeLayout(False)
            Me.pnlEstadistica.ResumeLayout(False)
            Me.pnlEstadistica.PerformLayout()
            CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlItems.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents lblTitulo As System.Windows.Forms.Label
        Friend WithEvents pnlBottom As System.Windows.Forms.Panel
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents dsCargue As DBImaging.Esquemas.xsdCargue
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents pnlPaquetes As System.Windows.Forms.Panel
        Friend WithEvents lblPaquetes As System.Windows.Forms.Label
        Friend WithEvents dgvPaquetes As System.Windows.Forms.DataGridView
        Friend WithEvents dgvItems As System.Windows.Forms.DataGridView
        Friend WithEvents pnlItems As System.Windows.Forms.Panel
        Friend WithEvents lblItems As System.Windows.Forms.Label
        Friend WithEvents pnlDetallePaquetes As System.Windows.Forms.Panel
        Friend WithEvents pnlEstadistica As System.Windows.Forms.Panel
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents lblInvalidos As System.Windows.Forms.Label
        Friend WithEvents lblValidos As System.Windows.Forms.Label
        Friend WithEvents lblTotal As System.Windows.Forms.Label
        Friend WithEvents lblInvalidosE As System.Windows.Forms.Label
        Friend WithEvents lblValidosE As System.Windows.Forms.Label
        Friend WithEvents lblTotalE As System.Windows.Forms.Label
        Friend WithEvents pnlBottomLeft As System.Windows.Forms.Panel
        Friend WithEvents IdDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents PathDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FoliosDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents TamañoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents KeyDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ValidoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents DescripcionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents PathDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ValidosDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents InvalidosDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents TotalDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents PaqueteValido As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Descripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace