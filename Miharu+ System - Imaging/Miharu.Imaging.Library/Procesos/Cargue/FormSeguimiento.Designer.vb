Namespace Procesos.Cargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeguimiento
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
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSeguimiento))
            Me.ParameterPanel = New System.Windows.Forms.Panel()
            Me.FechaProcesoComboBox = New System.Windows.Forms.ComboBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.OptionPanel = New System.Windows.Forms.Panel()
            Me.DesbloquearButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.ReasignarCargueButton = New System.Windows.Forms.Button()
            Me.DatosDataGridView = New System.Windows.Forms.DataGridView()
            Me.ColumnNombre_Sede = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnNombre_Centro_Procesamiento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Columnid_OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnSedeAsignada = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnCentroAsignado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnPrecintos = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnContenedores = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnContenedores_Cargados = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnContenedores_Empacados = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnExpedientes = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnDocumentos = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnDocumentos_Cargados = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnDocumentos_Empacados = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnPaquetes = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnOCRIndexacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnIndexacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnRetenido = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnOCRCaptura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnPreCaptura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnCaptura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnSegundaCaptura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnTerceraCaptura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnCalidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnRecortes = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnCalidadRecorte = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnValidacionListas = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnCorreccionCaptura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnIndexado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnReproceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnValidaciones = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ParameterPanel.SuspendLayout()
            Me.OptionPanel.SuspendLayout()
            CType(Me.DatosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ParameterPanel
            '
            Me.ParameterPanel.Controls.Add(Me.FechaProcesoComboBox)
            Me.ParameterPanel.Controls.Add(Me.Label1)
            Me.ParameterPanel.Controls.Add(Me.BuscarButton)
            Me.ParameterPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.ParameterPanel.Location = New System.Drawing.Point(0, 0)
            Me.ParameterPanel.Name = "ParameterPanel"
            Me.ParameterPanel.Size = New System.Drawing.Size(847, 64)
            Me.ParameterPanel.TabIndex = 0
            '
            'FechaProcesoComboBox
            '
            Me.FechaProcesoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.FechaProcesoComboBox.FormatString = "yyyy/MM/dd"
            Me.FechaProcesoComboBox.FormattingEnabled = True
            Me.FechaProcesoComboBox.Location = New System.Drawing.Point(12, 28)
            Me.FechaProcesoComboBox.Name = "FechaProcesoComboBox"
            Me.FechaProcesoComboBox.Size = New System.Drawing.Size(308, 21)
            Me.FechaProcesoComboBox.TabIndex = 22
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(9, 11)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(92, 13)
            Me.Label1.TabIndex = 21
            Me.Label1.Text = "Fecha Proceso"
            '
            'BuscarButton
            '
            Me.BuscarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(738, 24)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(89, 31)
            Me.BuscarButton.TabIndex = 5
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'OptionPanel
            '
            Me.OptionPanel.Controls.Add(Me.DesbloquearButton)
            Me.OptionPanel.Controls.Add(Me.CerrarButton)
            Me.OptionPanel.Controls.Add(Me.ReasignarCargueButton)
            Me.OptionPanel.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.OptionPanel.Location = New System.Drawing.Point(0, 379)
            Me.OptionPanel.Name = "OptionPanel"
            Me.OptionPanel.Size = New System.Drawing.Size(847, 42)
            Me.OptionPanel.TabIndex = 1
            '
            'DesbloquearButton
            '
            Me.DesbloquearButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DesbloquearButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DesbloquearButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.lock_open
            Me.DesbloquearButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.DesbloquearButton.Location = New System.Drawing.Point(630, 6)
            Me.DesbloquearButton.Name = "DesbloquearButton"
            Me.DesbloquearButton.Size = New System.Drawing.Size(107, 33)
            Me.DesbloquearButton.TabIndex = 7
            Me.DesbloquearButton.Text = "Desbloquear"
            Me.DesbloquearButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.DesbloquearButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancelar
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(756, 6)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 33)
            Me.CerrarButton.TabIndex = 7
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'ReasignarCargueButton
            '
            Me.ReasignarCargueButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ReasignarCargueButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.lightning_go
            Me.ReasignarCargueButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ReasignarCargueButton.Location = New System.Drawing.Point(3, 6)
            Me.ReasignarCargueButton.Name = "ReasignarCargueButton"
            Me.ReasignarCargueButton.Size = New System.Drawing.Size(99, 33)
            Me.ReasignarCargueButton.TabIndex = 8
            Me.ReasignarCargueButton.Text = "Reasignar"
            Me.ReasignarCargueButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ReasignarCargueButton.UseVisualStyleBackColor = True
            '
            'DatosDataGridView
            '
            Me.DatosDataGridView.AllowUserToAddRows = False
            Me.DatosDataGridView.AllowUserToDeleteRows = False
            Me.DatosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DatosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnNombre_Sede, Me.ColumnNombre_Centro_Procesamiento, Me.Columnid_OT, Me.ColumnSedeAsignada, Me.ColumnCentroAsignado, Me.ColumnPrecintos, Me.ColumnContenedores, Me.ColumnContenedores_Cargados, Me.ColumnContenedores_Empacados, Me.ColumnExpedientes, Me.ColumnDocumentos, Me.ColumnDocumentos_Cargados, Me.ColumnDocumentos_Empacados, Me.ColumnPaquetes, Me.ColumnOCRIndexacion, Me.ColumnIndexacion, Me.ColumnRetenido, Me.ColumnOCRCaptura, Me.ColumnPreCaptura, Me.ColumnCaptura, Me.ColumnSegundaCaptura, Me.ColumnTerceraCaptura, Me.ColumnCalidad, Me.ColumnRecortes, Me.ColumnCalidadRecorte, Me.ColumnValidacionListas, Me.ColumnCorreccionCaptura, Me.ColumnIndexado, Me.ColumnReproceso, Me.ColumnValidaciones})
            Me.DatosDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DatosDataGridView.Location = New System.Drawing.Point(0, 64)
            Me.DatosDataGridView.MultiSelect = False
            Me.DatosDataGridView.Name = "DatosDataGridView"
            Me.DatosDataGridView.ReadOnly = True
            Me.DatosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DatosDataGridView.Size = New System.Drawing.Size(847, 315)
            Me.DatosDataGridView.TabIndex = 2
            '
            'ColumnNombre_Sede
            '
            Me.ColumnNombre_Sede.DataPropertyName = "SedeCargue"
            Me.ColumnNombre_Sede.HeaderText = "Sede Destape"
            Me.ColumnNombre_Sede.Name = "ColumnNombre_Sede"
            Me.ColumnNombre_Sede.ReadOnly = True
            Me.ColumnNombre_Sede.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnNombre_Centro_Procesamiento
            '
            Me.ColumnNombre_Centro_Procesamiento.DataPropertyName = "CentroCargue"
            Me.ColumnNombre_Centro_Procesamiento.HeaderText = "Centro Destape"
            Me.ColumnNombre_Centro_Procesamiento.Name = "ColumnNombre_Centro_Procesamiento"
            Me.ColumnNombre_Centro_Procesamiento.ReadOnly = True
            Me.ColumnNombre_Centro_Procesamiento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'Columnid_OT
            '
            Me.Columnid_OT.DataPropertyName = "id_OT"
            Me.Columnid_OT.HeaderText = "OT"
            Me.Columnid_OT.Name = "Columnid_OT"
            Me.Columnid_OT.ReadOnly = True
            Me.Columnid_OT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnSedeAsignada
            '
            Me.ColumnSedeAsignada.DataPropertyName = "SedeAsignado"
            Me.ColumnSedeAsignada.HeaderText = "Sede Asignada"
            Me.ColumnSedeAsignada.Name = "ColumnSedeAsignada"
            Me.ColumnSedeAsignada.ReadOnly = True
            Me.ColumnSedeAsignada.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnCentroAsignado
            '
            Me.ColumnCentroAsignado.DataPropertyName = "CentroAsignado"
            Me.ColumnCentroAsignado.HeaderText = "Centro Asignado"
            Me.ColumnCentroAsignado.Name = "ColumnCentroAsignado"
            Me.ColumnCentroAsignado.ReadOnly = True
            Me.ColumnCentroAsignado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnPrecintos
            '
            Me.ColumnPrecintos.DataPropertyName = "Precintos"
            Me.ColumnPrecintos.HeaderText = "Precintos"
            Me.ColumnPrecintos.Name = "ColumnPrecintos"
            Me.ColumnPrecintos.ReadOnly = True
            Me.ColumnPrecintos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnContenedores
            '
            Me.ColumnContenedores.DataPropertyName = "Contenedores"
            Me.ColumnContenedores.HeaderText = "Contenedores"
            Me.ColumnContenedores.Name = "ColumnContenedores"
            Me.ColumnContenedores.ReadOnly = True
            Me.ColumnContenedores.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnContenedores_Cargados
            '
            Me.ColumnContenedores_Cargados.DataPropertyName = "Contenedores_Cargados"
            Me.ColumnContenedores_Cargados.HeaderText = "Contenedores Cargados"
            Me.ColumnContenedores_Cargados.Name = "ColumnContenedores_Cargados"
            Me.ColumnContenedores_Cargados.ReadOnly = True
            Me.ColumnContenedores_Cargados.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnContenedores_Empacados
            '
            Me.ColumnContenedores_Empacados.DataPropertyName = "Contenedores_Empacados"
            Me.ColumnContenedores_Empacados.HeaderText = "Contenedores Empacados"
            Me.ColumnContenedores_Empacados.Name = "ColumnContenedores_Empacados"
            Me.ColumnContenedores_Empacados.ReadOnly = True
            Me.ColumnContenedores_Empacados.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnExpedientes
            '
            Me.ColumnExpedientes.DataPropertyName = "Expedientes"
            Me.ColumnExpedientes.HeaderText = "Expedientes"
            Me.ColumnExpedientes.Name = "ColumnExpedientes"
            Me.ColumnExpedientes.ReadOnly = True
            Me.ColumnExpedientes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnDocumentos
            '
            Me.ColumnDocumentos.DataPropertyName = "Documentos"
            Me.ColumnDocumentos.HeaderText = "Documentos"
            Me.ColumnDocumentos.Name = "ColumnDocumentos"
            Me.ColumnDocumentos.ReadOnly = True
            Me.ColumnDocumentos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnDocumentos_Cargados
            '
            Me.ColumnDocumentos_Cargados.DataPropertyName = "Documentos_Cargados"
            Me.ColumnDocumentos_Cargados.HeaderText = "Documentos Cargados"
            Me.ColumnDocumentos_Cargados.Name = "ColumnDocumentos_Cargados"
            Me.ColumnDocumentos_Cargados.ReadOnly = True
            Me.ColumnDocumentos_Cargados.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnDocumentos_Empacados
            '
            Me.ColumnDocumentos_Empacados.DataPropertyName = "Documentos_Empacados"
            Me.ColumnDocumentos_Empacados.HeaderText = "Documentos Empacados"
            Me.ColumnDocumentos_Empacados.Name = "ColumnDocumentos_Empacados"
            Me.ColumnDocumentos_Empacados.ReadOnly = True
            Me.ColumnDocumentos_Empacados.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnPaquetes
            '
            Me.ColumnPaquetes.DataPropertyName = "Paquetes"
            Me.ColumnPaquetes.HeaderText = "Paquetes"
            Me.ColumnPaquetes.Name = "ColumnPaquetes"
            Me.ColumnPaquetes.ReadOnly = True
            '
            'ColumnOCRIndexacion
            '
            Me.ColumnOCRIndexacion.DataPropertyName = "OCR_Indexacion"
            Me.ColumnOCRIndexacion.HeaderText = "OCR Indexación"
            Me.ColumnOCRIndexacion.Name = "ColumnOCRIndexacion"
            Me.ColumnOCRIndexacion.ReadOnly = True
            Me.ColumnOCRIndexacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnIndexacion
            '
            Me.ColumnIndexacion.DataPropertyName = "Indexacion"
            Me.ColumnIndexacion.HeaderText = "Indexación"
            Me.ColumnIndexacion.Name = "ColumnIndexacion"
            Me.ColumnIndexacion.ReadOnly = True
            '
            'ColumnRetenido
            '
            Me.ColumnRetenido.DataPropertyName = "Retenido"
            Me.ColumnRetenido.HeaderText = "Retenido"
            Me.ColumnRetenido.Name = "ColumnRetenido"
            Me.ColumnRetenido.ReadOnly = True
            Me.ColumnRetenido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnOCRCaptura
            '
            Me.ColumnOCRCaptura.DataPropertyName = "OCR_Captura"
            Me.ColumnOCRCaptura.HeaderText = "OCR Captura"
            Me.ColumnOCRCaptura.Name = "ColumnOCRCaptura"
            Me.ColumnOCRCaptura.ReadOnly = True
            Me.ColumnOCRCaptura.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnPreCaptura
            '
            Me.ColumnPreCaptura.DataPropertyName = "Pre_Captura"
            Me.ColumnPreCaptura.HeaderText = "Pre Captura"
            Me.ColumnPreCaptura.Name = "ColumnPreCaptura"
            Me.ColumnPreCaptura.ReadOnly = True
            '
            'ColumnCaptura
            '
            Me.ColumnCaptura.DataPropertyName = "Captura"
            Me.ColumnCaptura.HeaderText = "Primera Captura"
            Me.ColumnCaptura.Name = "ColumnCaptura"
            Me.ColumnCaptura.ReadOnly = True
            '
            'ColumnSegundaCaptura
            '
            Me.ColumnSegundaCaptura.DataPropertyName = "Segunda_Captura"
            Me.ColumnSegundaCaptura.HeaderText = "Segunda Captura"
            Me.ColumnSegundaCaptura.Name = "ColumnSegundaCaptura"
            Me.ColumnSegundaCaptura.ReadOnly = True
            '
            'ColumnTerceraCaptura
            '
            Me.ColumnTerceraCaptura.DataPropertyName = "Tercera_Captura"
            Me.ColumnTerceraCaptura.HeaderText = "Tercera Captura"
            Me.ColumnTerceraCaptura.Name = "ColumnTerceraCaptura"
            Me.ColumnTerceraCaptura.ReadOnly = True
            '
            'ColumnCalidad
            '
            Me.ColumnCalidad.DataPropertyName = "Calidad"
            Me.ColumnCalidad.HeaderText = "Calidad"
            Me.ColumnCalidad.Name = "ColumnCalidad"
            Me.ColumnCalidad.ReadOnly = True
            Me.ColumnCalidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnRecortes
            '
            Me.ColumnRecortes.DataPropertyName = "Recorte"
            Me.ColumnRecortes.HeaderText = "Recorte"
            Me.ColumnRecortes.Name = "ColumnRecortes"
            Me.ColumnRecortes.ReadOnly = True
            '
            'ColumnCalidadRecorte
            '
            Me.ColumnCalidadRecorte.DataPropertyName = "Calidad_Recorte"
            Me.ColumnCalidadRecorte.HeaderText = "Calidad Recorte"
            Me.ColumnCalidadRecorte.Name = "ColumnCalidadRecorte"
            Me.ColumnCalidadRecorte.ReadOnly = True
            '
            'ColumnValidacionListas
            '
            Me.ColumnValidacionListas.DataPropertyName = "Validacion_Listas"
            Me.ColumnValidacionListas.HeaderText = "Adicional Captura"
            Me.ColumnValidacionListas.Name = "ColumnValidacionListas"
            Me.ColumnValidacionListas.ReadOnly = True
            '
            'ColumnCorreccionCaptura
            '
            Me.ColumnCorreccionCaptura.DataPropertyName = "Correccion_Captura"
            DataGridViewCellStyle1.Format = "0"
            Me.ColumnCorreccionCaptura.DefaultCellStyle = DataGridViewCellStyle1
            Me.ColumnCorreccionCaptura.HeaderText = "Correccion Captura"
            Me.ColumnCorreccionCaptura.Name = "ColumnCorreccionCaptura"
            Me.ColumnCorreccionCaptura.ReadOnly = True
            Me.ColumnCorreccionCaptura.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnIndexado
            '
            Me.ColumnIndexado.DataPropertyName = "Indexado"
            Me.ColumnIndexado.HeaderText = "Indexado"
            Me.ColumnIndexado.Name = "ColumnIndexado"
            Me.ColumnIndexado.ReadOnly = True
            '
            'ColumnReproceso
            '
            Me.ColumnReproceso.DataPropertyName = "Reproceso"
            Me.ColumnReproceso.HeaderText = "Reproceso"
            Me.ColumnReproceso.Name = "ColumnReproceso"
            Me.ColumnReproceso.ReadOnly = True
            Me.ColumnReproceso.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'ColumnValidaciones
            '
            Me.ColumnValidaciones.DataPropertyName = "Validaciones"
            DataGridViewCellStyle2.Format = "N0"
            Me.ColumnValidaciones.DefaultCellStyle = DataGridViewCellStyle2
            Me.ColumnValidaciones.HeaderText = "Validaciones"
            Me.ColumnValidaciones.Name = "ColumnValidaciones"
            Me.ColumnValidaciones.ReadOnly = True
            Me.ColumnValidaciones.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'FormSeguimiento
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(847, 421)
            Me.Controls.Add(Me.DatosDataGridView)
            Me.Controls.Add(Me.OptionPanel)
            Me.Controls.Add(Me.ParameterPanel)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormSeguimiento"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Cargues"
            Me.ParameterPanel.ResumeLayout(False)
            Me.ParameterPanel.PerformLayout()
            Me.OptionPanel.ResumeLayout(False)
            CType(Me.DatosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ParameterPanel As System.Windows.Forms.Panel
        Friend WithEvents OptionPanel As System.Windows.Forms.Panel
        Friend WithEvents DatosDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents DesbloquearButton As System.Windows.Forms.Button
        Friend WithEvents ReasignarCargueButton As System.Windows.Forms.Button
        Friend WithEvents FechaProcesoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ColumnNombre_Sede As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnNombre_Centro_Procesamiento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Columnid_OT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnSedeAsignada As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnCentroAsignado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnPrecintos As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnContenedores As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnContenedores_Cargados As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnContenedores_Empacados As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnExpedientes As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnDocumentos As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnDocumentos_Cargados As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnDocumentos_Empacados As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnPaquetes As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnOCRIndexacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnIndexacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnRetenido As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnOCRCaptura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnPreCaptura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnCaptura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnSegundaCaptura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnTerceraCaptura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnCalidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnRecortes As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnCalidadRecorte As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnValidacionListas As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnCorreccionCaptura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnIndexado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnReproceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnValidaciones As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace