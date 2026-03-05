Namespace Procesos.Cargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEstadoCargue
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
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEstadoCargue))
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.CarguesActivosCheckBox = New System.Windows.Forms.CheckBox()
            Me.FechaFinalDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.FechaInicialDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.DesbloquearButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.ReasignarCargueButton = New System.Windows.Forms.Button()
            Me.DatosDataGridView = New System.Windows.Forms.DataGridView()
            Me.DataProcessDataSet = New DBImaging.ProcessDataSet()
            Me.Columnid_Cargue = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Columnid_Cargue_Paquete = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnNombre_Estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnFecha_Cargue = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnFecha_Proceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnNombre_Sede = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnNombre_Centro_Procesamiento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnKey_1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnKey_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnKey_3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnPaquetes = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnItems = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnExpedientes = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnFiles = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnRetenido = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.OCR_Indexacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnIndexacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.OCR_Captura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnPre_Captura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnPrimera_Captura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnSegunda_Captura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnTercera_Captura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnCalidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnReproceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnIndexado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnValidaciones_Totales = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnValidaciones_Pendientes = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnValidacion_Listas = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnObservaciones = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            CType(Me.DatosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DataProcessDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.BuscarButton)
            Me.Panel1.Controls.Add(Me.CarguesActivosCheckBox)
            Me.Panel1.Controls.Add(Me.FechaFinalDateTimePicker)
            Me.Panel1.Controls.Add(Me.Label2)
            Me.Panel1.Controls.Add(Me.Label1)
            Me.Panel1.Controls.Add(Me.FechaInicialDateTimePicker)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(847, 64)
            Me.Panel1.TabIndex = 0
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
            'CarguesActivosCheckBox
            '
            Me.CarguesActivosCheckBox.AutoSize = True
            Me.CarguesActivosCheckBox.Checked = True
            Me.CarguesActivosCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
            Me.CarguesActivosCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CarguesActivosCheckBox.Location = New System.Drawing.Point(463, 32)
            Me.CarguesActivosCheckBox.Name = "CarguesActivosCheckBox"
            Me.CarguesActivosCheckBox.Size = New System.Drawing.Size(189, 17)
            Me.CarguesActivosCheckBox.TabIndex = 4
            Me.CarguesActivosCheckBox.Text = "Mostrar solo cargues activos"
            Me.CarguesActivosCheckBox.UseVisualStyleBackColor = True
            '
            'FechaFinalDateTimePicker
            '
            Me.FechaFinalDateTimePicker.Location = New System.Drawing.Point(222, 29)
            Me.FechaFinalDateTimePicker.Name = "FechaFinalDateTimePicker"
            Me.FechaFinalDateTimePicker.Size = New System.Drawing.Size(200, 20)
            Me.FechaFinalDateTimePicker.TabIndex = 3
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(219, 13)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(73, 13)
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "Fecha Final"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(13, 13)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(80, 13)
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "Fecha Inicial"
            '
            'FechaInicialDateTimePicker
            '
            Me.FechaInicialDateTimePicker.Location = New System.Drawing.Point(16, 29)
            Me.FechaInicialDateTimePicker.Name = "FechaInicialDateTimePicker"
            Me.FechaInicialDateTimePicker.Size = New System.Drawing.Size(200, 20)
            Me.FechaInicialDateTimePicker.TabIndex = 0
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.DesbloquearButton)
            Me.Panel2.Controls.Add(Me.CerrarButton)
            Me.Panel2.Controls.Add(Me.ReasignarCargueButton)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel2.Location = New System.Drawing.Point(0, 379)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(847, 42)
            Me.Panel2.TabIndex = 1
            '
            'DesbloquearButton
            '
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
            Me.DatosDataGridView.AutoGenerateColumns = False
            Me.DatosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DatosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Columnid_Cargue, Me.Columnid_Cargue_Paquete, Me.ColumnNombre_Estado, Me.ColumnFecha_Cargue, Me.ColumnFecha_Proceso, Me.ColumnNombre_Sede, Me.ColumnNombre_Centro_Procesamiento, Me.ColumnKey_1, Me.ColumnKey_2, Me.ColumnKey_3, Me.ColumnPaquetes, Me.ColumnItems, Me.ColumnExpedientes, Me.ColumnFiles, Me.ColumnRetenido, Me.OCR_Indexacion, Me.ColumnIndexacion, Me.OCR_Captura, Me.ColumnPre_Captura, Me.ColumnPrimera_Captura, Me.ColumnSegunda_Captura, Me.ColumnTercera_Captura, Me.ColumnCalidad, Me.ColumnReproceso, Me.ColumnIndexado, Me.ColumnValidaciones_Totales, Me.ColumnValidaciones_Pendientes, Me.ColumnValidacion_Listas, Me.ColumnObservaciones})
            Me.DatosDataGridView.DataMember = "CTA_Cargue_Seguimiento"
            Me.DatosDataGridView.DataSource = Me.DataProcessDataSet
            Me.DatosDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DatosDataGridView.Location = New System.Drawing.Point(0, 64)
            Me.DatosDataGridView.Name = "DatosDataGridView"
            Me.DatosDataGridView.ReadOnly = True
            Me.DatosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DatosDataGridView.Size = New System.Drawing.Size(847, 315)
            Me.DatosDataGridView.TabIndex = 2
            '
            'DataProcessDataSet
            '
            Me.DataProcessDataSet.DataSetName = "NewDataSet"
            '
            'Columnid_Cargue
            '
            Me.Columnid_Cargue.DataPropertyName = "id_Cargue"
            Me.Columnid_Cargue.HeaderText = "Cargue"
            Me.Columnid_Cargue.Name = "Columnid_Cargue"
            Me.Columnid_Cargue.ReadOnly = True
            '
            'Columnid_Cargue_Paquete
            '
            Me.Columnid_Cargue_Paquete.DataPropertyName = "id_Cargue_Paquete"
            Me.Columnid_Cargue_Paquete.HeaderText = "Paquete"
            Me.Columnid_Cargue_Paquete.Name = "Columnid_Cargue_Paquete"
            Me.Columnid_Cargue_Paquete.ReadOnly = True
            '
            'ColumnNombre_Estado
            '
            Me.ColumnNombre_Estado.DataPropertyName = "Nombre_Estado"
            Me.ColumnNombre_Estado.HeaderText = "Estado"
            Me.ColumnNombre_Estado.Name = "ColumnNombre_Estado"
            Me.ColumnNombre_Estado.ReadOnly = True
            '
            'ColumnFecha_Cargue
            '
            Me.ColumnFecha_Cargue.DataPropertyName = "Fecha_Cargue"
            Me.ColumnFecha_Cargue.HeaderText = "Fecha Cargue"
            Me.ColumnFecha_Cargue.Name = "ColumnFecha_Cargue"
            Me.ColumnFecha_Cargue.ReadOnly = True
            '
            'ColumnFecha_Proceso
            '
            Me.ColumnFecha_Proceso.DataPropertyName = "Fecha_Proceso"
            Me.ColumnFecha_Proceso.HeaderText = "Fecha Proceso"
            Me.ColumnFecha_Proceso.Name = "ColumnFecha_Proceso"
            Me.ColumnFecha_Proceso.ReadOnly = True
            '
            'ColumnNombre_Sede
            '
            Me.ColumnNombre_Sede.DataPropertyName = "Nombre_Sede"
            Me.ColumnNombre_Sede.HeaderText = "Sede"
            Me.ColumnNombre_Sede.Name = "ColumnNombre_Sede"
            Me.ColumnNombre_Sede.ReadOnly = True
            '
            'ColumnNombre_Centro_Procesamiento
            '
            Me.ColumnNombre_Centro_Procesamiento.DataPropertyName = "Nombre_Centro_Procesamiento"
            Me.ColumnNombre_Centro_Procesamiento.HeaderText = "Centro pro."
            Me.ColumnNombre_Centro_Procesamiento.Name = "ColumnNombre_Centro_Procesamiento"
            Me.ColumnNombre_Centro_Procesamiento.ReadOnly = True
            '
            'ColumnKey_1
            '
            Me.ColumnKey_1.DataPropertyName = "Key_1"
            Me.ColumnKey_1.HeaderText = "Key_1"
            Me.ColumnKey_1.Name = "ColumnKey_1"
            Me.ColumnKey_1.ReadOnly = True
            '
            'ColumnKey_2
            '
            Me.ColumnKey_2.DataPropertyName = "Key_2"
            Me.ColumnKey_2.HeaderText = "Key_2"
            Me.ColumnKey_2.Name = "ColumnKey_2"
            Me.ColumnKey_2.ReadOnly = True
            '
            'ColumnKey_3
            '
            Me.ColumnKey_3.DataPropertyName = "Key_3"
            Me.ColumnKey_3.HeaderText = "Key_3"
            Me.ColumnKey_3.Name = "ColumnKey_3"
            Me.ColumnKey_3.ReadOnly = True
            '
            'ColumnPaquetes
            '
            Me.ColumnPaquetes.DataPropertyName = "Paquetes"
            DataGridViewCellStyle1.Format = "N0"
            DataGridViewCellStyle1.NullValue = Nothing
            Me.ColumnPaquetes.DefaultCellStyle = DataGridViewCellStyle1
            Me.ColumnPaquetes.HeaderText = "Paquetes"
            Me.ColumnPaquetes.Name = "ColumnPaquetes"
            Me.ColumnPaquetes.ReadOnly = True
            '
            'ColumnItems
            '
            Me.ColumnItems.DataPropertyName = "Items"
            DataGridViewCellStyle2.Format = "N0"
            Me.ColumnItems.DefaultCellStyle = DataGridViewCellStyle2
            Me.ColumnItems.HeaderText = "Items"
            Me.ColumnItems.Name = "ColumnItems"
            Me.ColumnItems.ReadOnly = True
            '
            'ColumnExpedientes
            '
            Me.ColumnExpedientes.DataPropertyName = "Expedientes"
            DataGridViewCellStyle3.Format = "N0"
            DataGridViewCellStyle3.NullValue = Nothing
            Me.ColumnExpedientes.DefaultCellStyle = DataGridViewCellStyle3
            Me.ColumnExpedientes.HeaderText = "Expedientes"
            Me.ColumnExpedientes.Name = "ColumnExpedientes"
            Me.ColumnExpedientes.ReadOnly = True
            '
            'ColumnFiles
            '
            Me.ColumnFiles.DataPropertyName = "Files"
            DataGridViewCellStyle4.Format = "N0"
            Me.ColumnFiles.DefaultCellStyle = DataGridViewCellStyle4
            Me.ColumnFiles.HeaderText = "Files"
            Me.ColumnFiles.Name = "ColumnFiles"
            Me.ColumnFiles.ReadOnly = True
            '
            'ColumnRetenido
            '
            Me.ColumnRetenido.DataPropertyName = "Retenido"
            Me.ColumnRetenido.HeaderText = "Retenido"
            Me.ColumnRetenido.Name = "ColumnRetenido"
            Me.ColumnRetenido.ReadOnly = True
            '
            'OCR_Indexacion
            '
            Me.OCR_Indexacion.DataPropertyName = "OCR_Indexacion"
            Me.OCR_Indexacion.HeaderText = "OCR_Indexacion"
            Me.OCR_Indexacion.Name = "OCR_Indexacion"
            Me.OCR_Indexacion.ReadOnly = True
            '
            'ColumnIndexacion
            '
            Me.ColumnIndexacion.DataPropertyName = "Indexacion"
            DataGridViewCellStyle5.Format = "N0"
            Me.ColumnIndexacion.DefaultCellStyle = DataGridViewCellStyle5
            Me.ColumnIndexacion.HeaderText = "Indexacion"
            Me.ColumnIndexacion.Name = "ColumnIndexacion"
            Me.ColumnIndexacion.ReadOnly = True
            '
            'OCR_Captura
            '
            Me.OCR_Captura.DataPropertyName = "OCR_Captura"
            Me.OCR_Captura.HeaderText = "OCR_Captura"
            Me.OCR_Captura.Name = "OCR_Captura"
            Me.OCR_Captura.ReadOnly = True
            '
            'ColumnPre_Captura
            '
            Me.ColumnPre_Captura.DataPropertyName = "Pre_Captura"
            DataGridViewCellStyle6.Format = "N0"
            Me.ColumnPre_Captura.DefaultCellStyle = DataGridViewCellStyle6
            Me.ColumnPre_Captura.HeaderText = "Pre Captura"
            Me.ColumnPre_Captura.Name = "ColumnPre_Captura"
            Me.ColumnPre_Captura.ReadOnly = True
            '
            'ColumnPrimera_Captura
            '
            Me.ColumnPrimera_Captura.DataPropertyName = "Captura"
            DataGridViewCellStyle7.Format = "N0"
            Me.ColumnPrimera_Captura.DefaultCellStyle = DataGridViewCellStyle7
            Me.ColumnPrimera_Captura.HeaderText = "Captura"
            Me.ColumnPrimera_Captura.Name = "ColumnPrimera_Captura"
            Me.ColumnPrimera_Captura.ReadOnly = True
            '
            'ColumnSegunda_Captura
            '
            Me.ColumnSegunda_Captura.DataPropertyName = "Segunda_Captura"
            DataGridViewCellStyle8.Format = "N0"
            Me.ColumnSegunda_Captura.DefaultCellStyle = DataGridViewCellStyle8
            Me.ColumnSegunda_Captura.HeaderText = "Segunda Captura"
            Me.ColumnSegunda_Captura.Name = "ColumnSegunda_Captura"
            Me.ColumnSegunda_Captura.ReadOnly = True
            '
            'ColumnTercera_Captura
            '
            Me.ColumnTercera_Captura.DataPropertyName = "Tercera_Captura"
            DataGridViewCellStyle9.Format = "N0"
            Me.ColumnTercera_Captura.DefaultCellStyle = DataGridViewCellStyle9
            Me.ColumnTercera_Captura.HeaderText = "Tercera Captura"
            Me.ColumnTercera_Captura.Name = "ColumnTercera_Captura"
            Me.ColumnTercera_Captura.ReadOnly = True
            '
            'ColumnCalidad
            '
            Me.ColumnCalidad.DataPropertyName = "Calidad"
            Me.ColumnCalidad.HeaderText = "Calidad"
            Me.ColumnCalidad.Name = "ColumnCalidad"
            Me.ColumnCalidad.ReadOnly = True
            '
            'ColumnReproceso
            '
            Me.ColumnReproceso.DataPropertyName = "Reproceso"
            Me.ColumnReproceso.HeaderText = "Reproceso"
            Me.ColumnReproceso.Name = "ColumnReproceso"
            Me.ColumnReproceso.ReadOnly = True
            '
            'ColumnIndexado
            '
            Me.ColumnIndexado.DataPropertyName = "Indexado"
            DataGridViewCellStyle10.Format = "N0"
            Me.ColumnIndexado.DefaultCellStyle = DataGridViewCellStyle10
            Me.ColumnIndexado.HeaderText = "Indexado"
            Me.ColumnIndexado.Name = "ColumnIndexado"
            Me.ColumnIndexado.ReadOnly = True
            '
            'ColumnValidaciones_Totales
            '
            Me.ColumnValidaciones_Totales.DataPropertyName = "Validaciones_Totales"
            DataGridViewCellStyle11.Format = "N0"
            Me.ColumnValidaciones_Totales.DefaultCellStyle = DataGridViewCellStyle11
            Me.ColumnValidaciones_Totales.HeaderText = "Val. Totales"
            Me.ColumnValidaciones_Totales.Name = "ColumnValidaciones_Totales"
            Me.ColumnValidaciones_Totales.ReadOnly = True
            '
            'ColumnValidaciones_Pendientes
            '
            Me.ColumnValidaciones_Pendientes.DataPropertyName = "Validaciones_Pendientes"
            DataGridViewCellStyle12.Format = "N0"
            Me.ColumnValidaciones_Pendientes.DefaultCellStyle = DataGridViewCellStyle12
            Me.ColumnValidaciones_Pendientes.HeaderText = "Val. Pendientes"
            Me.ColumnValidaciones_Pendientes.Name = "ColumnValidaciones_Pendientes"
            Me.ColumnValidaciones_Pendientes.ReadOnly = True
            '
            'ColumnValidacion_Listas
            '
            Me.ColumnValidacion_Listas.DataPropertyName = "Validacion_Listas"
            Me.ColumnValidacion_Listas.HeaderText = "Validacion Listas"
            Me.ColumnValidacion_Listas.Name = "ColumnValidacion_Listas"
            Me.ColumnValidacion_Listas.ReadOnly = True
            '
            'ColumnObservaciones
            '
            Me.ColumnObservaciones.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.ColumnObservaciones.DataPropertyName = "Observaciones"
            Me.ColumnObservaciones.FillWeight = 120.0!
            Me.ColumnObservaciones.HeaderText = "Observaciones"
            Me.ColumnObservaciones.Name = "ColumnObservaciones"
            Me.ColumnObservaciones.ReadOnly = True
            '
            'FormEstadoCargue
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(847, 421)
            Me.Controls.Add(Me.DatosDataGridView)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormEstadoCargue"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Cargues"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            CType(Me.DatosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DataProcessDataSet, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents FechaFinalDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents FechaInicialDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents DatosDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents CarguesActivosCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents DataProcessDataSet As DBImaging.ProcessDataSet
        Friend WithEvents DesbloquearButton As System.Windows.Forms.Button
        Friend WithEvents ReasignarCargueButton As System.Windows.Forms.Button
        Friend WithEvents Columnid_Cargue As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Columnid_Cargue_Paquete As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnNombre_Estado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnFecha_Cargue As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnFecha_Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnNombre_Sede As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnNombre_Centro_Procesamiento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnKey_1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnKey_2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnKey_3 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnPaquetes As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnItems As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnExpedientes As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnFiles As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnRetenido As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents OCR_Indexacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnIndexacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents OCR_Captura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnPre_Captura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnPrimera_Captura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnSegunda_Captura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnTercera_Captura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnCalidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnReproceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnIndexado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnValidaciones_Totales As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnValidaciones_Pendientes As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnValidacion_Listas As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnObservaciones As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace