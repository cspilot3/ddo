Namespace Risk.Forms.OT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCerrarOTDetalle
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
            Me.CerrarOTButton = New System.Windows.Forms.Button()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.ResumenOTTextBox = New System.Windows.Forms.TextBox()
            Me.OTLabel = New System.Windows.Forms.Label()
            Me.OTDataGridView = New System.Windows.Forms.DataGridView()
            Me.IdOTDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FechaProcesoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdPrecintoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DestapadoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.CodigoContenedorDestapeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkEstadoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CntDestapeEstadoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DestapeCantidadDocSegunCntDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DestapeCantidadDocRealDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CodigoContenedorEmpaqueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CantidadDocumentosDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CajaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DestinoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DireccionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CerradaDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.OTProcessDataSet = New DBAgrario.ProcessDataSet()
            Me.InformeButton = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.OTProcessDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'CerrarOTButton
            '
            Me.CerrarOTButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CerrarOTButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir1
            Me.CerrarOTButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarOTButton.Location = New System.Drawing.Point(12, 408)
            Me.CerrarOTButton.Name = "CerrarOTButton"
            Me.CerrarOTButton.Size = New System.Drawing.Size(92, 32)
            Me.CerrarOTButton.TabIndex = 9
            Me.CerrarOTButton.Text = "&Cerrar OT"
            Me.CerrarOTButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarOTButton.UseVisualStyleBackColor = True
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CancelarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(706, 408)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(89, 32)
            Me.CancelarButton.TabIndex = 8
            Me.CancelarButton.Text = "&Cerrar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.ResumenOTTextBox)
            Me.GroupBox1.Controls.Add(Me.OTLabel)
            Me.GroupBox1.Controls.Add(Me.OTDataGridView)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(783, 390)
            Me.GroupBox1.TabIndex = 7
            Me.GroupBox1.TabStop = False
            '
            'ResumenOTTextBox
            '
            Me.ResumenOTTextBox.Location = New System.Drawing.Point(6, 38)
            Me.ResumenOTTextBox.Multiline = True
            Me.ResumenOTTextBox.Name = "ResumenOTTextBox"
            Me.ResumenOTTextBox.ReadOnly = True
            Me.ResumenOTTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.ResumenOTTextBox.Size = New System.Drawing.Size(771, 95)
            Me.ResumenOTTextBox.TabIndex = 55
            '
            'OTLabel
            '
            Me.OTLabel.AutoSize = True
            Me.OTLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OTLabel.Location = New System.Drawing.Point(18, 16)
            Me.OTLabel.Name = "OTLabel"
            Me.OTLabel.Size = New System.Drawing.Size(315, 19)
            Me.OTLabel.TabIndex = 0
            Me.OTLabel.Text = "Orden de trabajo :     Fecha proceso:  "
            '
            'OTDataGridView
            '
            Me.OTDataGridView.AllowUserToAddRows = False
            Me.OTDataGridView.AllowUserToDeleteRows = False
            Me.OTDataGridView.AutoGenerateColumns = False
            Me.OTDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.OTDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdOTDataGridViewTextBoxColumn, Me.FechaProcesoDataGridViewTextBoxColumn, Me.IdPrecintoDataGridViewTextBoxColumn, Me.DestapadoDataGridViewCheckBoxColumn, Me.CodigoContenedorDestapeDataGridViewTextBoxColumn, Me.FkEstadoDataGridViewTextBoxColumn, Me.CntDestapeEstadoDataGridViewTextBoxColumn, Me.DestapeCantidadDocSegunCntDataGridViewTextBoxColumn, Me.DestapeCantidadDocRealDataGridViewTextBoxColumn, Me.CodigoContenedorEmpaqueDataGridViewTextBoxColumn, Me.CantidadDocumentosDataGridViewTextBoxColumn, Me.CajaDataGridViewTextBoxColumn, Me.DestinoDataGridViewTextBoxColumn, Me.DireccionDataGridViewTextBoxColumn, Me.CerradaDataGridViewCheckBoxColumn})
            Me.OTDataGridView.DataMember = "CTA_OT_Cierre_Detalle"
            Me.OTDataGridView.DataSource = Me.OTProcessDataSet
            Me.OTDataGridView.Location = New System.Drawing.Point(6, 139)
            Me.OTDataGridView.MultiSelect = False
            Me.OTDataGridView.Name = "OTDataGridView"
            Me.OTDataGridView.ReadOnly = True
            Me.OTDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.OTDataGridView.Size = New System.Drawing.Size(771, 245)
            Me.OTDataGridView.TabIndex = 54
            '
            'IdOTDataGridViewTextBoxColumn
            '
            Me.IdOTDataGridViewTextBoxColumn.DataPropertyName = "id_OT"
            Me.IdOTDataGridViewTextBoxColumn.HeaderText = "id_OT"
            Me.IdOTDataGridViewTextBoxColumn.Name = "IdOTDataGridViewTextBoxColumn"
            Me.IdOTDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdOTDataGridViewTextBoxColumn.Visible = False
            '
            'FechaProcesoDataGridViewTextBoxColumn
            '
            Me.FechaProcesoDataGridViewTextBoxColumn.DataPropertyName = "Fecha_Proceso"
            Me.FechaProcesoDataGridViewTextBoxColumn.HeaderText = "Fecha_Proceso"
            Me.FechaProcesoDataGridViewTextBoxColumn.Name = "FechaProcesoDataGridViewTextBoxColumn"
            Me.FechaProcesoDataGridViewTextBoxColumn.ReadOnly = True
            Me.FechaProcesoDataGridViewTextBoxColumn.Visible = False
            '
            'IdPrecintoDataGridViewTextBoxColumn
            '
            Me.IdPrecintoDataGridViewTextBoxColumn.DataPropertyName = "id_Precinto"
            Me.IdPrecintoDataGridViewTextBoxColumn.HeaderText = "Precinto"
            Me.IdPrecintoDataGridViewTextBoxColumn.Name = "IdPrecintoDataGridViewTextBoxColumn"
            Me.IdPrecintoDataGridViewTextBoxColumn.ReadOnly = True
            '
            'DestapadoDataGridViewCheckBoxColumn
            '
            Me.DestapadoDataGridViewCheckBoxColumn.DataPropertyName = "Destapado"
            Me.DestapadoDataGridViewCheckBoxColumn.HeaderText = "Destapado"
            Me.DestapadoDataGridViewCheckBoxColumn.Name = "DestapadoDataGridViewCheckBoxColumn"
            Me.DestapadoDataGridViewCheckBoxColumn.ReadOnly = True
            '
            'CodigoContenedorDestapeDataGridViewTextBoxColumn
            '
            Me.CodigoContenedorDestapeDataGridViewTextBoxColumn.DataPropertyName = "Codigo_Contenedor_Destape"
            Me.CodigoContenedorDestapeDataGridViewTextBoxColumn.HeaderText = "Codigo Contenedor Destape"
            Me.CodigoContenedorDestapeDataGridViewTextBoxColumn.Name = "CodigoContenedorDestapeDataGridViewTextBoxColumn"
            Me.CodigoContenedorDestapeDataGridViewTextBoxColumn.ReadOnly = True
            '
            'FkEstadoDataGridViewTextBoxColumn
            '
            Me.FkEstadoDataGridViewTextBoxColumn.DataPropertyName = "fk_Estado"
            Me.FkEstadoDataGridViewTextBoxColumn.HeaderText = "fk_Estado"
            Me.FkEstadoDataGridViewTextBoxColumn.Name = "FkEstadoDataGridViewTextBoxColumn"
            Me.FkEstadoDataGridViewTextBoxColumn.ReadOnly = True
            Me.FkEstadoDataGridViewTextBoxColumn.Visible = False
            '
            'CntDestapeEstadoDataGridViewTextBoxColumn
            '
            Me.CntDestapeEstadoDataGridViewTextBoxColumn.DataPropertyName = "CntDestapeEstado"
            Me.CntDestapeEstadoDataGridViewTextBoxColumn.HeaderText = "Estado Contenedor Destape"
            Me.CntDestapeEstadoDataGridViewTextBoxColumn.Name = "CntDestapeEstadoDataGridViewTextBoxColumn"
            Me.CntDestapeEstadoDataGridViewTextBoxColumn.ReadOnly = True
            '
            'DestapeCantidadDocSegunCntDataGridViewTextBoxColumn
            '
            Me.DestapeCantidadDocSegunCntDataGridViewTextBoxColumn.DataPropertyName = "Destape_Cantidad_Doc_Segun_Cnt"
            Me.DestapeCantidadDocSegunCntDataGridViewTextBoxColumn.HeaderText = "Destape Cantidad Doc Segun Contenedor"
            Me.DestapeCantidadDocSegunCntDataGridViewTextBoxColumn.Name = "DestapeCantidadDocSegunCntDataGridViewTextBoxColumn"
            Me.DestapeCantidadDocSegunCntDataGridViewTextBoxColumn.ReadOnly = True
            '
            'DestapeCantidadDocRealDataGridViewTextBoxColumn
            '
            Me.DestapeCantidadDocRealDataGridViewTextBoxColumn.DataPropertyName = "Destape_Cantidad_Doc_Real"
            Me.DestapeCantidadDocRealDataGridViewTextBoxColumn.HeaderText = "Destape Cantidad Doc Fisicos"
            Me.DestapeCantidadDocRealDataGridViewTextBoxColumn.Name = "DestapeCantidadDocRealDataGridViewTextBoxColumn"
            Me.DestapeCantidadDocRealDataGridViewTextBoxColumn.ReadOnly = True
            '
            'CodigoContenedorEmpaqueDataGridViewTextBoxColumn
            '
            Me.CodigoContenedorEmpaqueDataGridViewTextBoxColumn.DataPropertyName = "Codigo_Contenedor_Empaque"
            Me.CodigoContenedorEmpaqueDataGridViewTextBoxColumn.HeaderText = "Codigo Contenedor Empaque"
            Me.CodigoContenedorEmpaqueDataGridViewTextBoxColumn.Name = "CodigoContenedorEmpaqueDataGridViewTextBoxColumn"
            Me.CodigoContenedorEmpaqueDataGridViewTextBoxColumn.ReadOnly = True
            '
            'CantidadDocumentosDataGridViewTextBoxColumn
            '
            Me.CantidadDocumentosDataGridViewTextBoxColumn.DataPropertyName = "Cantidad_Documentos"
            Me.CantidadDocumentosDataGridViewTextBoxColumn.HeaderText = "Empaque Cantidad Documentos"
            Me.CantidadDocumentosDataGridViewTextBoxColumn.Name = "CantidadDocumentosDataGridViewTextBoxColumn"
            Me.CantidadDocumentosDataGridViewTextBoxColumn.ReadOnly = True
            '
            'CajaDataGridViewTextBoxColumn
            '
            Me.CajaDataGridViewTextBoxColumn.DataPropertyName = "Caja"
            Me.CajaDataGridViewTextBoxColumn.HeaderText = "Caja"
            Me.CajaDataGridViewTextBoxColumn.Name = "CajaDataGridViewTextBoxColumn"
            Me.CajaDataGridViewTextBoxColumn.ReadOnly = True
            '
            'DestinoDataGridViewTextBoxColumn
            '
            Me.DestinoDataGridViewTextBoxColumn.DataPropertyName = "Destino"
            Me.DestinoDataGridViewTextBoxColumn.HeaderText = "Destino"
            Me.DestinoDataGridViewTextBoxColumn.Name = "DestinoDataGridViewTextBoxColumn"
            Me.DestinoDataGridViewTextBoxColumn.ReadOnly = True
            '
            'DireccionDataGridViewTextBoxColumn
            '
            Me.DireccionDataGridViewTextBoxColumn.DataPropertyName = "Direccion"
            Me.DireccionDataGridViewTextBoxColumn.HeaderText = "Direccion"
            Me.DireccionDataGridViewTextBoxColumn.Name = "DireccionDataGridViewTextBoxColumn"
            Me.DireccionDataGridViewTextBoxColumn.ReadOnly = True
            '
            'CerradaDataGridViewCheckBoxColumn
            '
            Me.CerradaDataGridViewCheckBoxColumn.DataPropertyName = "Cerrada"
            Me.CerradaDataGridViewCheckBoxColumn.HeaderText = "Cerrada"
            Me.CerradaDataGridViewCheckBoxColumn.Name = "CerradaDataGridViewCheckBoxColumn"
            Me.CerradaDataGridViewCheckBoxColumn.ReadOnly = True
            '
            'OTProcessDataSet
            '
            Me.OTProcessDataSet.DataSetName = "NewDataSet"
            '
            'InformeButton
            '
            Me.InformeButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.InformeButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Edit
            Me.InformeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.InformeButton.Location = New System.Drawing.Point(150, 408)
            Me.InformeButton.Name = "InformeButton"
            Me.InformeButton.Size = New System.Drawing.Size(134, 32)
            Me.InformeButton.TabIndex = 10
            Me.InformeButton.Text = "&Exportar datos"
            Me.InformeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.InformeButton.UseVisualStyleBackColor = True
            '
            'FormCerrarOTDetalle
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(824, 452)
            Me.Controls.Add(Me.CerrarOTButton)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.InformeButton)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCerrarOTDetalle"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Detalle OT"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.OTProcessDataSet, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents OTProcessDataSet As DBAgrario.ProcessDataSet
        Friend WithEvents CerrarOTButton As System.Windows.Forms.Button
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents OTLabel As System.Windows.Forms.Label
        Friend WithEvents OTDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents InformeButton As System.Windows.Forms.Button
        Friend WithEvents IdOTDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FechaProcesoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdPrecintoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DestapadoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents CodigoContenedorDestapeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkEstadoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CntDestapeEstadoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DestapeCantidadDocSegunCntDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DestapeCantidadDocRealDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CodigoContenedorEmpaqueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CantidadDocumentosDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CajaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DestinoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DireccionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CerradaDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents ResumenOTTextBox As System.Windows.Forms.TextBox
    End Class
End Namespace