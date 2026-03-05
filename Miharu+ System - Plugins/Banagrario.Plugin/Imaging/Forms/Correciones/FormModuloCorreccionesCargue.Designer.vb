Namespace Imaging.Forms.Correciones
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormModuloCorreccionesCargue
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormModuloCorreccionesCargue))
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.FechaProcesodateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaProcesolabel = New System.Windows.Forms.Label()
            Me.CorreccionLlavesDataGridView = New System.Windows.Forms.DataGridView()
            Me.PAGetErrorLlavesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.GetErrorLlaves_DataSet = New Banagrario.Plugin.GetErrorLlaves_DataSet()
            Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.PA_Get_Error_LlavesTableAdapter = New Banagrario.Plugin.GetErrorLlaves_DataSetTableAdapters.PA_Get_Registros_Error_LlavesTableAdapter()
            Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkProcesoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreCOBDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkOficinaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreOficinaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FechaMovimientoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkCargueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkCarguePaqueteDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CodigoContenedorDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CantSoporteSobrantesDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CorregidoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Panel1.SuspendLayout()
            CType(Me.CorreccionLlavesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PAGetErrorLlavesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GetErrorLlaves_DataSet, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.BuscarButton)
            Me.Panel1.Controls.Add(Me.FechaProcesodateTimePicker)
            Me.Panel1.Controls.Add(Me.FechaProcesolabel)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(943, 100)
            Me.Panel1.TabIndex = 27
            '
            'BuscarButton
            '
            Me.BuscarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(519, 34)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(89, 31)
            Me.BuscarButton.TabIndex = 29
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'FechaProcesodateTimePicker
            '
            Me.FechaProcesodateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaProcesodateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaProcesodateTimePicker.Location = New System.Drawing.Point(352, 37)
            Me.FechaProcesodateTimePicker.Name = "FechaProcesodateTimePicker"
            Me.FechaProcesodateTimePicker.Size = New System.Drawing.Size(81, 20)
            Me.FechaProcesodateTimePicker.TabIndex = 28
            '
            'FechaProcesolabel
            '
            Me.FechaProcesolabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesolabel.AutoSize = True
            Me.FechaProcesolabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesolabel.Location = New System.Drawing.Point(250, 37)
            Me.FechaProcesolabel.Name = "FechaProcesolabel"
            Me.FechaProcesolabel.Size = New System.Drawing.Size(96, 13)
            Me.FechaProcesolabel.TabIndex = 27
            Me.FechaProcesolabel.Text = "Fecha Proceso:"
            '
            'CorreccionLlavesDataGridView
            '
            Me.CorreccionLlavesDataGridView.AllowUserToAddRows = False
            Me.CorreccionLlavesDataGridView.AllowUserToDeleteRows = False
            Me.CorreccionLlavesDataGridView.AutoGenerateColumns = False
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CorreccionLlavesDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.CorreccionLlavesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CorreccionLlavesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FkProcesoDataGridViewTextBoxColumn, Me.NombreCOBDataGridViewTextBoxColumn, Me.FkOficinaDataGridViewTextBoxColumn, Me.NombreOficinaDataGridViewTextBoxColumn, Me.FechaMovimientoDataGridViewTextBoxColumn, Me.FkCargueDataGridViewTextBoxColumn, Me.FkCarguePaqueteDataGridViewTextBoxColumn, Me.CodigoContenedorDataGridViewTextBoxColumn, Me.CantSoporteSobrantesDataGridViewTextBoxColumn, Me.CorregidoDataGridViewCheckBoxColumn})
            Me.CorreccionLlavesDataGridView.DataSource = Me.PAGetErrorLlavesBindingSource
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.CorreccionLlavesDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.CorreccionLlavesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CorreccionLlavesDataGridView.Location = New System.Drawing.Point(0, 100)
            Me.CorreccionLlavesDataGridView.Name = "CorreccionLlavesDataGridView"
            Me.CorreccionLlavesDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CorreccionLlavesDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.CorreccionLlavesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.CorreccionLlavesDataGridView.Size = New System.Drawing.Size(943, 312)
            Me.CorreccionLlavesDataGridView.TabIndex = 28
            '
            'PAGetErrorLlavesBindingSource
            '
            Me.PAGetErrorLlavesBindingSource.DataMember = "PA_Get_Error_Llaves"
            Me.PAGetErrorLlavesBindingSource.DataSource = Me.GetErrorLlaves_DataSet
            '
            'GetErrorLlaves_DataSet
            '
            Me.GetErrorLlaves_DataSet.DataSetName = "GetErrorLlaves_DataSet"
            Me.GetErrorLlaves_DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'DataGridViewTextBoxColumn1
            '
            Me.DataGridViewTextBoxColumn1.DataPropertyName = "Codigo_Contenedor"
            Me.DataGridViewTextBoxColumn1.HeaderText = "Contenedor"
            Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
            '
            'DataGridViewTextBoxColumn2
            '
            Me.DataGridViewTextBoxColumn2.DataPropertyName = "Codigo_Contenedor"
            Me.DataGridViewTextBoxColumn2.HeaderText = "Contenedor"
            Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
            Me.DataGridViewTextBoxColumn2.ReadOnly = True
            '
            'DataGridViewTextBoxColumn3
            '
            Me.DataGridViewTextBoxColumn3.DataPropertyName = "Codigo_Contenedor"
            Me.DataGridViewTextBoxColumn3.HeaderText = "Contenedor"
            Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
            '
            'PA_Get_Error_LlavesTableAdapter
            '
            Me.PA_Get_Error_LlavesTableAdapter.ClearBeforeFill = True
            '
            'DataGridViewTextBoxColumn4
            '
            Me.DataGridViewTextBoxColumn4.DataPropertyName = "Codigo_Contenedor"
            Me.DataGridViewTextBoxColumn4.HeaderText = "Contenedor"
            Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
            '
            'DataGridViewTextBoxColumn5
            '
            Me.DataGridViewTextBoxColumn5.DataPropertyName = "Codigo_Contenedor"
            Me.DataGridViewTextBoxColumn5.HeaderText = "Contenedor"
            Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
            '
            'FkProcesoDataGridViewTextBoxColumn
            '
            Me.FkProcesoDataGridViewTextBoxColumn.DataPropertyName = "fk_Proceso"
            Me.FkProcesoDataGridViewTextBoxColumn.HeaderText = "fk_Proceso"
            Me.FkProcesoDataGridViewTextBoxColumn.Name = "FkProcesoDataGridViewTextBoxColumn"
            Me.FkProcesoDataGridViewTextBoxColumn.ReadOnly = True
            Me.FkProcesoDataGridViewTextBoxColumn.Visible = False
            '
            'NombreCOBDataGridViewTextBoxColumn
            '
            Me.NombreCOBDataGridViewTextBoxColumn.DataPropertyName = "Nombre_COB"
            Me.NombreCOBDataGridViewTextBoxColumn.HeaderText = "Nombre del COB"
            Me.NombreCOBDataGridViewTextBoxColumn.Name = "NombreCOBDataGridViewTextBoxColumn"
            Me.NombreCOBDataGridViewTextBoxColumn.ReadOnly = True
            '
            'FkOficinaDataGridViewTextBoxColumn
            '
            Me.FkOficinaDataGridViewTextBoxColumn.DataPropertyName = "fk_Oficina"
            Me.FkOficinaDataGridViewTextBoxColumn.HeaderText = "Código Oficina"
            Me.FkOficinaDataGridViewTextBoxColumn.Name = "FkOficinaDataGridViewTextBoxColumn"
            Me.FkOficinaDataGridViewTextBoxColumn.ReadOnly = True
            '
            'NombreOficinaDataGridViewTextBoxColumn
            '
            Me.NombreOficinaDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Oficina"
            Me.NombreOficinaDataGridViewTextBoxColumn.HeaderText = "Nombre Oficina"
            Me.NombreOficinaDataGridViewTextBoxColumn.Name = "NombreOficinaDataGridViewTextBoxColumn"
            Me.NombreOficinaDataGridViewTextBoxColumn.ReadOnly = True
            '
            'FechaMovimientoDataGridViewTextBoxColumn
            '
            Me.FechaMovimientoDataGridViewTextBoxColumn.DataPropertyName = "Fecha_Movimiento"
            Me.FechaMovimientoDataGridViewTextBoxColumn.HeaderText = "Fecha Movimiento"
            Me.FechaMovimientoDataGridViewTextBoxColumn.Name = "FechaMovimientoDataGridViewTextBoxColumn"
            Me.FechaMovimientoDataGridViewTextBoxColumn.ReadOnly = True
            '
            'FkCargueDataGridViewTextBoxColumn
            '
            Me.FkCargueDataGridViewTextBoxColumn.DataPropertyName = "fk_Cargue"
            Me.FkCargueDataGridViewTextBoxColumn.HeaderText = "Cargue"
            Me.FkCargueDataGridViewTextBoxColumn.Name = "FkCargueDataGridViewTextBoxColumn"
            Me.FkCargueDataGridViewTextBoxColumn.ReadOnly = True
            '
            'FkCarguePaqueteDataGridViewTextBoxColumn
            '
            Me.FkCarguePaqueteDataGridViewTextBoxColumn.DataPropertyName = "fk_Cargue_Paquete"
            Me.FkCarguePaqueteDataGridViewTextBoxColumn.HeaderText = "Paquete"
            Me.FkCarguePaqueteDataGridViewTextBoxColumn.Name = "FkCarguePaqueteDataGridViewTextBoxColumn"
            Me.FkCarguePaqueteDataGridViewTextBoxColumn.ReadOnly = True
            '
            'CodigoContenedorDataGridViewTextBoxColumn
            '
            Me.CodigoContenedorDataGridViewTextBoxColumn.DataPropertyName = "Codigo_Contenedor"
            Me.CodigoContenedorDataGridViewTextBoxColumn.HeaderText = "Contenedor"
            Me.CodigoContenedorDataGridViewTextBoxColumn.Name = "CodigoContenedorDataGridViewTextBoxColumn"
            Me.CodigoContenedorDataGridViewTextBoxColumn.ReadOnly = True
            '
            'CantSoporteSobrantesDataGridViewTextBoxColumn
            '
            Me.CantSoporteSobrantesDataGridViewTextBoxColumn.DataPropertyName = "Cant_Soporte_Sobrantes"
            Me.CantSoporteSobrantesDataGridViewTextBoxColumn.HeaderText = "Cant. Soporte Sobrantes"
            Me.CantSoporteSobrantesDataGridViewTextBoxColumn.Name = "CantSoporteSobrantesDataGridViewTextBoxColumn"
            Me.CantSoporteSobrantesDataGridViewTextBoxColumn.ReadOnly = True
            '
            'CorregidoDataGridViewCheckBoxColumn
            '
            Me.CorregidoDataGridViewCheckBoxColumn.DataPropertyName = "Corregido"
            Me.CorregidoDataGridViewCheckBoxColumn.HeaderText = "Corregido"
            Me.CorregidoDataGridViewCheckBoxColumn.Name = "CorregidoDataGridViewCheckBoxColumn"
            Me.CorregidoDataGridViewCheckBoxColumn.ReadOnly = True
            '
            'FormModuloCorreccionesCargue
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(943, 412)
            Me.Controls.Add(Me.CorreccionLlavesDataGridView)
            Me.Controls.Add(Me.Panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormModuloCorreccionesCargue"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Corrección LLaves"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            CType(Me.CorreccionLlavesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PAGetErrorLlavesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GetErrorLlaves_DataSet, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Private WithEvents FechaProcesodateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaProcesolabel As System.Windows.Forms.Label
        Friend WithEvents CorreccionLlavesDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents PAGetErrorLlavesBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents GetErrorLlaves_DataSet As Banagrario.Plugin.GetErrorLlaves_DataSet
        Friend WithEvents PA_Get_Error_LlavesTableAdapter As Banagrario.Plugin.GetErrorLlaves_DataSetTableAdapters.PA_Get_Registros_Error_LlavesTableAdapter
        Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkProcesoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreCOBDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkOficinaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreOficinaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FechaMovimientoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkCargueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkCarguePaqueteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CodigoContenedorDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CantSoporteSobrantesDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CorregidoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace