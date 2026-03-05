Namespace Procesos.Exportar
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormExport
        Inherits Miharu.Desktop.Library.FormBase

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormExport))
            Me.TXTRadioButton = New System.Windows.Forms.RadioButton()
            Me.MainGroupBox = New System.Windows.Forms.GroupBox()
            Me.GroupBoxExportar = New System.Windows.Forms.GroupBox()
            Me.CheckBoxExpedientesValidos = New System.Windows.Forms.CheckBox()
            Me.CheckBoxExpedientes = New System.Windows.Forms.CheckBox()
            Me.FechaProcesoFinalLabel = New System.Windows.Forms.Label()
            Me.FechaProcesoFinalDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.BuscarFechaButton = New System.Windows.Forms.Button()
            Me.FechaProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.OTLabel = New System.Windows.Forms.Label()
            Me.FechaProcesoLabel = New System.Windows.Forms.Label()
            Me.BuscarCarpetaButton = New System.Windows.Forms.Button()
            Me.lblCarpeta = New System.Windows.Forms.Label()
            Me.CarpetaSalidaTextBox = New System.Windows.Forms.TextBox()
            Me.XMLRadioButton = New System.Windows.Forms.RadioButton()
            Me.VisorRadioButton = New System.Windows.Forms.RadioButton()
            Me.ExpedientesDataGridView = New System.Windows.Forms.DataGridView()
            Me.Expediente = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Fecha_Proceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Exportar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Ruta = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.OTDataGridView = New System.Windows.Forms.DataGridView()
            Me.id_OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_OT_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnCerrado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.ColumnExportado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.ColumnTipoExportacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnRuta = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.ExportarButton = New System.Windows.Forms.Button()
            Me.OCRCheckBox = New System.Windows.Forms.CheckBox()
            Me.MainGroupBox.SuspendLayout()
            Me.GroupBoxExportar.SuspendLayout()
            CType(Me.ExpedientesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'TXTRadioButton
            '
            Me.TXTRadioButton.AutoSize = True
            Me.TXTRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TXTRadioButton.Location = New System.Drawing.Point(160, 368)
            Me.TXTRadioButton.Name = "TXTRadioButton"
            Me.TXTRadioButton.Size = New System.Drawing.Size(50, 19)
            Me.TXTRadioButton.TabIndex = 10
            Me.TXTRadioButton.Text = "TXT"
            Me.TXTRadioButton.UseVisualStyleBackColor = True
            '
            'MainGroupBox
            '
            Me.MainGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MainGroupBox.Controls.Add(Me.OCRCheckBox)
            Me.MainGroupBox.Controls.Add(Me.GroupBoxExportar)
            Me.MainGroupBox.Controls.Add(Me.FechaProcesoFinalLabel)
            Me.MainGroupBox.Controls.Add(Me.FechaProcesoFinalDateTimePicker)
            Me.MainGroupBox.Controls.Add(Me.BuscarFechaButton)
            Me.MainGroupBox.Controls.Add(Me.FechaProcesoDateTimePicker)
            Me.MainGroupBox.Controls.Add(Me.OTLabel)
            Me.MainGroupBox.Controls.Add(Me.FechaProcesoLabel)
            Me.MainGroupBox.Controls.Add(Me.TXTRadioButton)
            Me.MainGroupBox.Controls.Add(Me.BuscarCarpetaButton)
            Me.MainGroupBox.Controls.Add(Me.lblCarpeta)
            Me.MainGroupBox.Controls.Add(Me.CarpetaSalidaTextBox)
            Me.MainGroupBox.Controls.Add(Me.XMLRadioButton)
            Me.MainGroupBox.Controls.Add(Me.VisorRadioButton)
            Me.MainGroupBox.Controls.Add(Me.ExpedientesDataGridView)
            Me.MainGroupBox.Controls.Add(Me.OTDataGridView)
            Me.MainGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MainGroupBox.Location = New System.Drawing.Point(10, 6)
            Me.MainGroupBox.Name = "MainGroupBox"
            Me.MainGroupBox.Size = New System.Drawing.Size(652, 419)
            Me.MainGroupBox.TabIndex = 0
            Me.MainGroupBox.TabStop = False
            Me.MainGroupBox.Text = "Parametros de exportación"
            '
            'GroupBoxExportar
            '
            Me.GroupBoxExportar.Controls.Add(Me.CheckBoxExpedientesValidos)
            Me.GroupBoxExportar.Controls.Add(Me.CheckBoxExpedientes)
            Me.GroupBoxExportar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GroupBoxExportar.Location = New System.Drawing.Point(15, 66)
            Me.GroupBoxExportar.Name = "GroupBoxExportar"
            Me.GroupBoxExportar.Size = New System.Drawing.Size(623, 44)
            Me.GroupBoxExportar.TabIndex = 17
            Me.GroupBoxExportar.TabStop = False
            Me.GroupBoxExportar.Text = "Exportar por:"
            '
            'CheckBoxExpedientesValidos
            '
            Me.CheckBoxExpedientesValidos.AutoSize = True
            Me.CheckBoxExpedientesValidos.Location = New System.Drawing.Point(288, 21)
            Me.CheckBoxExpedientesValidos.Name = "CheckBoxExpedientesValidos"
            Me.CheckBoxExpedientesValidos.Size = New System.Drawing.Size(170, 17)
            Me.CheckBoxExpedientesValidos.TabIndex = 1
            Me.CheckBoxExpedientesValidos.Text = "Expedientes validos x OT"
            Me.CheckBoxExpedientesValidos.UseVisualStyleBackColor = True
            '
            'CheckBoxExpedientes
            '
            Me.CheckBoxExpedientes.AutoSize = True
            Me.CheckBoxExpedientes.Location = New System.Drawing.Point(15, 21)
            Me.CheckBoxExpedientes.Name = "CheckBoxExpedientes"
            Me.CheckBoxExpedientes.Size = New System.Drawing.Size(95, 17)
            Me.CheckBoxExpedientes.TabIndex = 0
            Me.CheckBoxExpedientes.Text = "Expedientes"
            Me.CheckBoxExpedientes.UseVisualStyleBackColor = True
            '
            'FechaProcesoFinalLabel
            '
            Me.FechaProcesoFinalLabel.AutoSize = True
            Me.FechaProcesoFinalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoFinalLabel.Location = New System.Drawing.Point(300, 19)
            Me.FechaProcesoFinalLabel.Name = "FechaProcesoFinalLabel"
            Me.FechaProcesoFinalLabel.Size = New System.Drawing.Size(138, 15)
            Me.FechaProcesoFinalLabel.TabIndex = 12
            Me.FechaProcesoFinalLabel.Text = "Fecha Proceso Final"
            '
            'FechaProcesoFinalDateTimePicker
            '
            Me.FechaProcesoFinalDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoFinalDateTimePicker.Location = New System.Drawing.Point(303, 38)
            Me.FechaProcesoFinalDateTimePicker.Name = "FechaProcesoFinalDateTimePicker"
            Me.FechaProcesoFinalDateTimePicker.Size = New System.Drawing.Size(279, 22)
            Me.FechaProcesoFinalDateTimePicker.TabIndex = 11
            '
            'BuscarFechaButton
            '
            Me.BuscarFechaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarFechaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnBuscar
            Me.BuscarFechaButton.Location = New System.Drawing.Point(597, 30)
            Me.BuscarFechaButton.Name = "BuscarFechaButton"
            Me.BuscarFechaButton.Size = New System.Drawing.Size(43, 30)
            Me.BuscarFechaButton.TabIndex = 2
            Me.BuscarFechaButton.UseVisualStyleBackColor = True
            '
            'FechaProcesoDateTimePicker
            '
            Me.FechaProcesoDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoDateTimePicker.Location = New System.Drawing.Point(15, 38)
            Me.FechaProcesoDateTimePicker.Name = "FechaProcesoDateTimePicker"
            Me.FechaProcesoDateTimePicker.Size = New System.Drawing.Size(279, 22)
            Me.FechaProcesoDateTimePicker.TabIndex = 1
            '
            'OTLabel
            '
            Me.OTLabel.AutoSize = True
            Me.OTLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OTLabel.Location = New System.Drawing.Point(12, 113)
            Me.OTLabel.Name = "OTLabel"
            Me.OTLabel.Size = New System.Drawing.Size(32, 15)
            Me.OTLabel.TabIndex = 3
            Me.OTLabel.Text = "OTs"
            '
            'FechaProcesoLabel
            '
            Me.FechaProcesoLabel.AutoSize = True
            Me.FechaProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoLabel.Location = New System.Drawing.Point(12, 19)
            Me.FechaProcesoLabel.Name = "FechaProcesoLabel"
            Me.FechaProcesoLabel.Size = New System.Drawing.Size(145, 15)
            Me.FechaProcesoLabel.TabIndex = 0
            Me.FechaProcesoLabel.Text = "Fecha Proceso Inicial"
            '
            'BuscarCarpetaButton
            '
            Me.BuscarCarpetaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarCarpetaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnDestape
            Me.BuscarCarpetaButton.Location = New System.Drawing.Point(595, 335)
            Me.BuscarCarpetaButton.Name = "BuscarCarpetaButton"
            Me.BuscarCarpetaButton.Size = New System.Drawing.Size(43, 30)
            Me.BuscarCarpetaButton.TabIndex = 7
            Me.BuscarCarpetaButton.UseVisualStyleBackColor = True
            '
            'lblCarpeta
            '
            Me.lblCarpeta.AutoSize = True
            Me.lblCarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCarpeta.Location = New System.Drawing.Point(10, 326)
            Me.lblCarpeta.Name = "lblCarpeta"
            Me.lblCarpeta.Size = New System.Drawing.Size(122, 15)
            Me.lblCarpeta.TabIndex = 5
            Me.lblCarpeta.Text = "Carpeta de Salida"
            '
            'CarpetaSalidaTextBox
            '
            Me.CarpetaSalidaTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CarpetaSalidaTextBox.Location = New System.Drawing.Point(14, 345)
            Me.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox"
            Me.CarpetaSalidaTextBox.Size = New System.Drawing.Size(573, 20)
            Me.CarpetaSalidaTextBox.TabIndex = 6
            '
            'XMLRadioButton
            '
            Me.XMLRadioButton.AutoSize = True
            Me.XMLRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.XMLRadioButton.Location = New System.Drawing.Point(90, 368)
            Me.XMLRadioButton.Name = "XMLRadioButton"
            Me.XMLRadioButton.Size = New System.Drawing.Size(54, 19)
            Me.XMLRadioButton.TabIndex = 9
            Me.XMLRadioButton.Text = "XML"
            Me.XMLRadioButton.UseVisualStyleBackColor = True
            '
            'VisorRadioButton
            '
            Me.VisorRadioButton.AutoSize = True
            Me.VisorRadioButton.Checked = True
            Me.VisorRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.VisorRadioButton.Location = New System.Drawing.Point(14, 368)
            Me.VisorRadioButton.Name = "VisorRadioButton"
            Me.VisorRadioButton.Size = New System.Drawing.Size(57, 19)
            Me.VisorRadioButton.TabIndex = 8
            Me.VisorRadioButton.TabStop = True
            Me.VisorRadioButton.Text = "Visor"
            Me.VisorRadioButton.UseVisualStyleBackColor = True
            '
            'ExpedientesDataGridView
            '
            Me.ExpedientesDataGridView.AllowUserToAddRows = False
            Me.ExpedientesDataGridView.AllowUserToDeleteRows = False
            Me.ExpedientesDataGridView.AllowUserToResizeColumns = False
            Me.ExpedientesDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ExpedientesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ExpedientesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Expediente, Me.Fecha_Proceso, Me.fk_OT, Me.Exportar, Me.Ruta})
            Me.ExpedientesDataGridView.Location = New System.Drawing.Point(14, 131)
            Me.ExpedientesDataGridView.MultiSelect = False
            Me.ExpedientesDataGridView.Name = "ExpedientesDataGridView"
            Me.ExpedientesDataGridView.RowHeadersWidth = 10
            Me.ExpedientesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ExpedientesDataGridView.Size = New System.Drawing.Size(624, 192)
            Me.ExpedientesDataGridView.TabIndex = 16
            Me.ExpedientesDataGridView.Visible = False
            '
            'Expediente
            '
            Me.Expediente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Expediente.DataPropertyName = "fk_Expediente"
            Me.Expediente.HeaderText = "Expediente"
            Me.Expediente.Name = "Expediente"
            Me.Expediente.ReadOnly = True
            '
            'Fecha_Proceso
            '
            Me.Fecha_Proceso.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Fecha_Proceso.DataPropertyName = "Fecha_Proceso"
            Me.Fecha_Proceso.HeaderText = "Fecha de Proceso"
            Me.Fecha_Proceso.Name = "Fecha_Proceso"
            Me.Fecha_Proceso.ReadOnly = True
            Me.Fecha_Proceso.Width = 150
            '
            'fk_OT
            '
            Me.fk_OT.DataPropertyName = "fk_OT"
            Me.fk_OT.HeaderText = "OT"
            Me.fk_OT.Name = "fk_OT"
            Me.fk_OT.ReadOnly = True
            Me.fk_OT.Width = 40
            '
            'Exportar
            '
            Me.Exportar.DataPropertyName = "Exportar"
            Me.Exportar.HeaderText = "Exportar"
            Me.Exportar.Name = "Exportar"
            Me.Exportar.Width = 60
            '
            'Ruta
            '
            Me.Ruta.DataPropertyName = "Ruta"
            Me.Ruta.HeaderText = "Ruta"
            Me.Ruta.Name = "Ruta"
            Me.Ruta.ReadOnly = True
            Me.Ruta.Width = 300
            '
            'OTDataGridView
            '
            Me.OTDataGridView.AllowUserToAddRows = False
            Me.OTDataGridView.AllowUserToDeleteRows = False
            Me.OTDataGridView.AllowUserToOrderColumns = True
            Me.OTDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OTDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.OTDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_OT, Me.Nombre_OT_Tipo, Me.ColumnCerrado, Me.ColumnExportado, Me.ColumnTipoExportacion, Me.ColumnRuta})
            Me.OTDataGridView.Location = New System.Drawing.Point(14, 131)
            Me.OTDataGridView.MultiSelect = False
            Me.OTDataGridView.Name = "OTDataGridView"
            Me.OTDataGridView.ReadOnly = True
            Me.OTDataGridView.RowHeadersWidth = 10
            Me.OTDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.OTDataGridView.Size = New System.Drawing.Size(624, 192)
            Me.OTDataGridView.TabIndex = 4
            '
            'id_OT
            '
            Me.id_OT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.id_OT.DataPropertyName = "id_OT"
            Me.id_OT.HeaderText = "OT"
            Me.id_OT.Name = "id_OT"
            Me.id_OT.ReadOnly = True
            '
            'Nombre_OT_Tipo
            '
            Me.Nombre_OT_Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Nombre_OT_Tipo.DataPropertyName = "Nombre_OT_Tipo"
            Me.Nombre_OT_Tipo.HeaderText = "Tipo OT"
            Me.Nombre_OT_Tipo.Name = "Nombre_OT_Tipo"
            Me.Nombre_OT_Tipo.ReadOnly = True
            Me.Nombre_OT_Tipo.Width = 200
            '
            'ColumnCerrado
            '
            Me.ColumnCerrado.DataPropertyName = "Cerrado"
            Me.ColumnCerrado.HeaderText = "Cerrado"
            Me.ColumnCerrado.Name = "ColumnCerrado"
            Me.ColumnCerrado.ReadOnly = True
            Me.ColumnCerrado.Width = 60
            '
            'ColumnExportado
            '
            Me.ColumnExportado.DataPropertyName = "Exportado"
            Me.ColumnExportado.HeaderText = "Exportado"
            Me.ColumnExportado.Name = "ColumnExportado"
            Me.ColumnExportado.ReadOnly = True
            Me.ColumnExportado.Width = 60
            '
            'ColumnTipoExportacion
            '
            Me.ColumnTipoExportacion.DataPropertyName = "Nombre_Exportacion_Tipo"
            Me.ColumnTipoExportacion.HeaderText = "Modo"
            Me.ColumnTipoExportacion.Name = "ColumnTipoExportacion"
            Me.ColumnTipoExportacion.ReadOnly = True
            Me.ColumnTipoExportacion.Width = 40
            '
            'ColumnRuta
            '
            Me.ColumnRuta.DataPropertyName = "Ruta"
            Me.ColumnRuta.HeaderText = "Ruta"
            Me.ColumnRuta.Name = "ColumnRuta"
            Me.ColumnRuta.ReadOnly = True
            Me.ColumnRuta.Width = 300
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(547, 431)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(104, 37)
            Me.CancelarButton.TabIndex = 2
            Me.CancelarButton.Text = "Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'ExportarButton
            '
            Me.ExportarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ExportarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ExportarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Aceptar
            Me.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ExportarButton.Location = New System.Drawing.Point(432, 431)
            Me.ExportarButton.Name = "ExportarButton"
            Me.ExportarButton.Size = New System.Drawing.Size(104, 37)
            Me.ExportarButton.TabIndex = 1
            Me.ExportarButton.Text = "Exportar"
            Me.ExportarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ExportarButton.UseVisualStyleBackColor = True
            '
            'OCRCheckBox
            '
            Me.OCRCheckBox.AutoSize = True
            Me.OCRCheckBox.Location = New System.Drawing.Point(15, 396)
            Me.OCRCheckBox.Name = "OCRCheckBox"
            Me.OCRCheckBox.Size = New System.Drawing.Size(52, 17)
            Me.OCRCheckBox.TabIndex = 21
            Me.OCRCheckBox.Text = "OCR"
            Me.OCRCheckBox.UseVisualStyleBackColor = True
            Me.OCRCheckBox.Visible = False
            '
            'FormExport
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(675, 480)
            Me.Controls.Add(Me.MainGroupBox)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.ExportarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormExport"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Exportar"
            Me.MainGroupBox.ResumeLayout(False)
            Me.MainGroupBox.PerformLayout()
            Me.GroupBoxExportar.ResumeLayout(False)
            Me.GroupBoxExportar.PerformLayout()
            CType(Me.ExpedientesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents TXTRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents BuscarCarpetaButton As System.Windows.Forms.Button
        Friend WithEvents lblCarpeta As System.Windows.Forms.Label
        Friend WithEvents CarpetaSalidaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents XMLRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents VisorRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents ExportarButton As System.Windows.Forms.Button
        Friend WithEvents FechaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents OTLabel As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents OTDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents BuscarFechaButton As System.Windows.Forms.Button
        Friend WithEvents id_OT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_OT_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnCerrado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents ColumnExportado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents ColumnTipoExportacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnRuta As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FechaProcesoFinalLabel As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoFinalDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents ExpedientesDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents Expediente As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Fecha_Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_OT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Exportar As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Ruta As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents GroupBoxExportar As System.Windows.Forms.GroupBox
        Friend WithEvents CheckBoxExpedientesValidos As System.Windows.Forms.CheckBox
        Friend WithEvents CheckBoxExpedientes As System.Windows.Forms.CheckBox
        Friend WithEvents OCRCheckBox As System.Windows.Forms.CheckBox
    End Class
End Namespace