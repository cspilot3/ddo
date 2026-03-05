Namespace Risk.Forms.OT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCerrarOT
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCerrarOT))
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.FechaFinalDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.RangoFechasLabel = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.FechaInicialDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.OTDataGridView = New System.Windows.Forms.DataGridView()
            Me.OTProcessDataSet = New DBAgrario.ProcessDataSet()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.InformeButton = New System.Windows.Forms.Button()
            Me.CerrarOTButton = New System.Windows.Forms.Button()
            Me.id_OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreCiudadDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreEstadoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FechaProcesoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.UCSRecibidasDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.UCSDestapadasDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ContenedoresDestapadosDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ContenedoresEmpacadosDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.GroupBox1.SuspendLayout()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.OTProcessDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'BuscarButton
            '
            Me.BuscarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(671, 49)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(93, 24)
            Me.BuscarButton.TabIndex = 1
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.FechaFinalDateTimePicker)
            Me.GroupBox1.Controls.Add(Me.RangoFechasLabel)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.FechaInicialDateTimePicker)
            Me.GroupBox1.Controls.Add(Me.OTDataGridView)
            Me.GroupBox1.Controls.Add(Me.BuscarButton)
            Me.GroupBox1.Location = New System.Drawing.Point(14, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(783, 362)
            Me.GroupBox1.TabIndex = 2
            Me.GroupBox1.TabStop = False
            '
            'FechaFinalDateTimePicker
            '
            Me.FechaFinalDateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaFinalDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaFinalDateTimePicker.Location = New System.Drawing.Point(297, 52)
            Me.FechaFinalDateTimePicker.Name = "FechaFinalDateTimePicker"
            Me.FechaFinalDateTimePicker.Size = New System.Drawing.Size(169, 21)
            Me.FechaFinalDateTimePicker.TabIndex = 57
            '
            'RangoFechasLabel
            '
            Me.RangoFechasLabel.AutoSize = True
            Me.RangoFechasLabel.Location = New System.Drawing.Point(21, 58)
            Me.RangoFechasLabel.Name = "RangoFechasLabel"
            Me.RangoFechasLabel.Size = New System.Drawing.Size(100, 13)
            Me.RangoFechasLabel.TabIndex = 56
            Me.RangoFechasLabel.Text = "Rango de fechas"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(18, 18)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(166, 19)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Ordenes de trabajo"
            '
            'FechaInicialDateTimePicker
            '
            Me.FechaInicialDateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaInicialDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaInicialDateTimePicker.Location = New System.Drawing.Point(124, 52)
            Me.FechaInicialDateTimePicker.Name = "FechaInicialDateTimePicker"
            Me.FechaInicialDateTimePicker.Size = New System.Drawing.Size(169, 21)
            Me.FechaInicialDateTimePicker.TabIndex = 55
            '
            'OTDataGridView
            '
            Me.OTDataGridView.AllowUserToAddRows = False
            Me.OTDataGridView.AllowUserToDeleteRows = False
            Me.OTDataGridView.AutoGenerateColumns = False
            Me.OTDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.OTDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_OT, Me.NombreCiudadDataGridViewTextBoxColumn, Me.NombreEstadoDataGridViewTextBoxColumn, Me.FechaProcesoDataGridViewTextBoxColumn, Me.UCSRecibidasDataGridViewTextBoxColumn, Me.UCSDestapadasDataGridViewTextBoxColumn, Me.ContenedoresDestapadosDataGridViewTextBoxColumn, Me.ContenedoresEmpacadosDataGridViewTextBoxColumn})
            Me.OTDataGridView.DataMember = "CTA_Cierre_OT"
            Me.OTDataGridView.DataSource = Me.OTProcessDataSet
            Me.OTDataGridView.Location = New System.Drawing.Point(22, 90)
            Me.OTDataGridView.MultiSelect = False
            Me.OTDataGridView.Name = "OTDataGridView"
            Me.OTDataGridView.ReadOnly = True
            Me.OTDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.OTDataGridView.Size = New System.Drawing.Size(742, 255)
            Me.OTDataGridView.TabIndex = 54
            '
            'OTProcessDataSet
            '
            Me.OTProcessDataSet.DataSetName = "NewDataSet"
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CancelarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(689, 380)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(89, 32)
            Me.CancelarButton.TabIndex = 4
            Me.CancelarButton.Text = "&Cerrar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'InformeButton
            '
            Me.InformeButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.InformeButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Edit
            Me.InformeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.InformeButton.Location = New System.Drawing.Point(207, 380)
            Me.InformeButton.Name = "InformeButton"
            Me.InformeButton.Size = New System.Drawing.Size(157, 32)
            Me.InformeButton.TabIndex = 6
            Me.InformeButton.Text = "&Generar Reporte"
            Me.InformeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.InformeButton.UseVisualStyleBackColor = True
            '
            'CerrarOTButton
            '
            Me.CerrarOTButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CerrarOTButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnRecepcion
            Me.CerrarOTButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarOTButton.Location = New System.Drawing.Point(36, 380)
            Me.CerrarOTButton.Name = "CerrarOTButton"
            Me.CerrarOTButton.Size = New System.Drawing.Size(121, 32)
            Me.CerrarOTButton.TabIndex = 5
            Me.CerrarOTButton.Text = "&Ver detalle OT"
            Me.CerrarOTButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarOTButton.UseVisualStyleBackColor = True
            '
            'id_OT
            '
            Me.id_OT.DataPropertyName = "id_OT"
            Me.id_OT.HeaderText = "OT"
            Me.id_OT.Name = "id_OT"
            Me.id_OT.ReadOnly = True
            '
            'NombreCiudadDataGridViewTextBoxColumn
            '
            Me.NombreCiudadDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Ciudad"
            Me.NombreCiudadDataGridViewTextBoxColumn.HeaderText = "Ciudad PyC"
            Me.NombreCiudadDataGridViewTextBoxColumn.Name = "NombreCiudadDataGridViewTextBoxColumn"
            Me.NombreCiudadDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreCiudadDataGridViewTextBoxColumn.Width = 150
            '
            'NombreEstadoDataGridViewTextBoxColumn
            '
            Me.NombreEstadoDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Estado"
            Me.NombreEstadoDataGridViewTextBoxColumn.HeaderText = "Estado OT"
            Me.NombreEstadoDataGridViewTextBoxColumn.Name = "NombreEstadoDataGridViewTextBoxColumn"
            Me.NombreEstadoDataGridViewTextBoxColumn.ReadOnly = True
            '
            'FechaProcesoDataGridViewTextBoxColumn
            '
            Me.FechaProcesoDataGridViewTextBoxColumn.DataPropertyName = "Fecha_Proceso"
            DataGridViewCellStyle1.Format = "yyyy/MM/dd"
            DataGridViewCellStyle1.NullValue = Nothing
            Me.FechaProcesoDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle1
            Me.FechaProcesoDataGridViewTextBoxColumn.HeaderText = "Fecha Proceso"
            Me.FechaProcesoDataGridViewTextBoxColumn.Name = "FechaProcesoDataGridViewTextBoxColumn"
            Me.FechaProcesoDataGridViewTextBoxColumn.ReadOnly = True
            '
            'UCSRecibidasDataGridViewTextBoxColumn
            '
            Me.UCSRecibidasDataGridViewTextBoxColumn.DataPropertyName = "UCS_Recibidas"
            Me.UCSRecibidasDataGridViewTextBoxColumn.HeaderText = "UCS Recibidas"
            Me.UCSRecibidasDataGridViewTextBoxColumn.Name = "UCSRecibidasDataGridViewTextBoxColumn"
            Me.UCSRecibidasDataGridViewTextBoxColumn.ReadOnly = True
            Me.UCSRecibidasDataGridViewTextBoxColumn.Width = 80
            '
            'UCSDestapadasDataGridViewTextBoxColumn
            '
            Me.UCSDestapadasDataGridViewTextBoxColumn.DataPropertyName = "UCS_Destapadas"
            Me.UCSDestapadasDataGridViewTextBoxColumn.HeaderText = "UCS Destapadas"
            Me.UCSDestapadasDataGridViewTextBoxColumn.Name = "UCSDestapadasDataGridViewTextBoxColumn"
            Me.UCSDestapadasDataGridViewTextBoxColumn.ReadOnly = True
            Me.UCSDestapadasDataGridViewTextBoxColumn.Width = 80
            '
            'ContenedoresDestapadosDataGridViewTextBoxColumn
            '
            Me.ContenedoresDestapadosDataGridViewTextBoxColumn.DataPropertyName = "Contenedores_Destapados"
            Me.ContenedoresDestapadosDataGridViewTextBoxColumn.HeaderText = "Contenedores Destapados"
            Me.ContenedoresDestapadosDataGridViewTextBoxColumn.Name = "ContenedoresDestapadosDataGridViewTextBoxColumn"
            Me.ContenedoresDestapadosDataGridViewTextBoxColumn.ReadOnly = True
            Me.ContenedoresDestapadosDataGridViewTextBoxColumn.Width = 80
            '
            'ContenedoresEmpacadosDataGridViewTextBoxColumn
            '
            Me.ContenedoresEmpacadosDataGridViewTextBoxColumn.DataPropertyName = "Contenedores_Empacados"
            Me.ContenedoresEmpacadosDataGridViewTextBoxColumn.HeaderText = "Contenedores Empacados"
            Me.ContenedoresEmpacadosDataGridViewTextBoxColumn.Name = "ContenedoresEmpacadosDataGridViewTextBoxColumn"
            Me.ContenedoresEmpacadosDataGridViewTextBoxColumn.ReadOnly = True
            Me.ContenedoresEmpacadosDataGridViewTextBoxColumn.Width = 80
            '
            'FormCerrarOT
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(809, 424)
            Me.Controls.Add(Me.InformeButton)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.CerrarOTButton)
            Me.Controls.Add(Me.CancelarButton)
            Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCerrarOT"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Cerrar OT"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.OTProcessDataSet, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents OTDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents OTProcessDataSet As DBAgrario.ProcessDataSet
        Friend WithEvents FechaFinalDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents RangoFechasLabel As System.Windows.Forms.Label
        Friend WithEvents FechaInicialDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents InformeButton As System.Windows.Forms.Button
        Friend WithEvents CerrarOTButton As System.Windows.Forms.Button
        Friend WithEvents id_OT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreCiudadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreEstadoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FechaProcesoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents UCSRecibidasDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents UCSDestapadasDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ContenedoresDestapadosDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ContenedoresEmpacadosDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace