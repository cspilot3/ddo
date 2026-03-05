Namespace Imaging.GobAtlantico.Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormExportarGobAtlantico
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
            Me.MainGroupBox = New System.Windows.Forms.GroupBox()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.VisorRadioButton = New System.Windows.Forms.RadioButton()
            Me.XMLRadioButton = New System.Windows.Forms.RadioButton()
            Me.CarpetaSalidaTextBox = New System.Windows.Forms.TextBox()
            Me.TXTRadioButton = New System.Windows.Forms.RadioButton()
            Me.FechaProcesoFinalLabel = New System.Windows.Forms.Label()
            Me.FechaProcesoFinalDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.BuscarFechaButton = New System.Windows.Forms.Button()
            Me.FechaProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.OTLabel = New System.Windows.Forms.Label()
            Me.FechaProcesoLabel = New System.Windows.Forms.Label()
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
            Me.BuscarCarpetaButton = New System.Windows.Forms.Button()
            Me.MainGroupBox.SuspendLayout()
            Me.Panel2.SuspendLayout()
            CType(Me.ExpedientesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'MainGroupBox
            '
            Me.MainGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MainGroupBox.Controls.Add(Me.Panel2)
            Me.MainGroupBox.Controls.Add(Me.FechaProcesoFinalLabel)
            Me.MainGroupBox.Controls.Add(Me.FechaProcesoFinalDateTimePicker)
            Me.MainGroupBox.Controls.Add(Me.BuscarFechaButton)
            Me.MainGroupBox.Controls.Add(Me.FechaProcesoDateTimePicker)
            Me.MainGroupBox.Controls.Add(Me.OTLabel)
            Me.MainGroupBox.Controls.Add(Me.FechaProcesoLabel)
            Me.MainGroupBox.Controls.Add(Me.ExpedientesDataGridView)
            Me.MainGroupBox.Controls.Add(Me.OTDataGridView)
            Me.MainGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MainGroupBox.Location = New System.Drawing.Point(18, 21)
            Me.MainGroupBox.Name = "MainGroupBox"
            Me.MainGroupBox.Size = New System.Drawing.Size(640, 172)
            Me.MainGroupBox.TabIndex = 7
            Me.MainGroupBox.TabStop = False
            Me.MainGroupBox.Text = "Parametros de exportación"
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.Label1)
            Me.Panel2.Controls.Add(Me.VisorRadioButton)
            Me.Panel2.Controls.Add(Me.XMLRadioButton)
            Me.Panel2.Controls.Add(Me.CarpetaSalidaTextBox)
            Me.Panel2.Controls.Add(Me.BuscarCarpetaButton)
            Me.Panel2.Controls.Add(Me.TXTRadioButton)
            Me.Panel2.Location = New System.Drawing.Point(14, 71)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(621, 79)
            Me.Panel2.TabIndex = 20
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(3, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(122, 15)
            Me.Label1.TabIndex = 5
            Me.Label1.Text = "Carpeta de Salida"
            '
            'VisorRadioButton
            '
            Me.VisorRadioButton.AutoSize = True
            Me.VisorRadioButton.Checked = True
            Me.VisorRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.VisorRadioButton.Location = New System.Drawing.Point(7, 51)
            Me.VisorRadioButton.Name = "VisorRadioButton"
            Me.VisorRadioButton.Size = New System.Drawing.Size(57, 19)
            Me.VisorRadioButton.TabIndex = 8
            Me.VisorRadioButton.TabStop = True
            Me.VisorRadioButton.Text = "Visor"
            Me.VisorRadioButton.UseVisualStyleBackColor = True
            Me.VisorRadioButton.Visible = False
            '
            'XMLRadioButton
            '
            Me.XMLRadioButton.AutoSize = True
            Me.XMLRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.XMLRadioButton.Location = New System.Drawing.Point(83, 51)
            Me.XMLRadioButton.Name = "XMLRadioButton"
            Me.XMLRadioButton.Size = New System.Drawing.Size(54, 19)
            Me.XMLRadioButton.TabIndex = 9
            Me.XMLRadioButton.Text = "XML"
            Me.XMLRadioButton.UseVisualStyleBackColor = True
            Me.XMLRadioButton.Visible = False
            '
            'CarpetaSalidaTextBox
            '
            Me.CarpetaSalidaTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CarpetaSalidaTextBox.Location = New System.Drawing.Point(7, 28)
            Me.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox"
            Me.CarpetaSalidaTextBox.Size = New System.Drawing.Size(550, 20)
            Me.CarpetaSalidaTextBox.TabIndex = 6
            '
            'TXTRadioButton
            '
            Me.TXTRadioButton.AutoSize = True
            Me.TXTRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TXTRadioButton.Location = New System.Drawing.Point(153, 51)
            Me.TXTRadioButton.Name = "TXTRadioButton"
            Me.TXTRadioButton.Size = New System.Drawing.Size(50, 19)
            Me.TXTRadioButton.TabIndex = 10
            Me.TXTRadioButton.Text = "TXT"
            Me.TXTRadioButton.UseVisualStyleBackColor = True
            Me.TXTRadioButton.Visible = False
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
            Me.BuscarFechaButton.Location = New System.Drawing.Point(585, 30)
            Me.BuscarFechaButton.Name = "BuscarFechaButton"
            Me.BuscarFechaButton.Size = New System.Drawing.Size(43, 30)
            Me.BuscarFechaButton.TabIndex = 2
            Me.BuscarFechaButton.UseVisualStyleBackColor = True
            Me.BuscarFechaButton.Visible = False
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
            Me.OTLabel.Visible = False
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
            Me.ExpedientesDataGridView.Location = New System.Drawing.Point(13, 131)
            Me.ExpedientesDataGridView.MultiSelect = False
            Me.ExpedientesDataGridView.Name = "ExpedientesDataGridView"
            Me.ExpedientesDataGridView.RowHeadersWidth = 10
            Me.ExpedientesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ExpedientesDataGridView.Size = New System.Drawing.Size(613, 0)
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
            Me.OTDataGridView.Size = New System.Drawing.Size(612, 0)
            Me.OTDataGridView.TabIndex = 4
            Me.OTDataGridView.Visible = False
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
            Me.CancelarButton.Image = Global.BancoItau.Plugin.My.Resources.Resources.cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(544, 205)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(104, 37)
            Me.CancelarButton.TabIndex = 9
            Me.CancelarButton.Text = "Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'ExportarButton
            '
            Me.ExportarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ExportarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ExportarButton.Image = Global.BancoItau.Plugin.My.Resources.Resources.Aceptar
            Me.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ExportarButton.Location = New System.Drawing.Point(429, 205)
            Me.ExportarButton.Name = "ExportarButton"
            Me.ExportarButton.Size = New System.Drawing.Size(104, 37)
            Me.ExportarButton.TabIndex = 8
            Me.ExportarButton.Text = "Exportar"
            Me.ExportarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ExportarButton.UseVisualStyleBackColor = True
            '
            'BuscarCarpetaButton
            '
            Me.BuscarCarpetaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarCarpetaButton.Image = Global.BancoItau.Plugin.My.Resources.Resources.btnBuscar
            Me.BuscarCarpetaButton.Location = New System.Drawing.Point(563, 22)
            Me.BuscarCarpetaButton.Name = "BuscarCarpetaButton"
            Me.BuscarCarpetaButton.Size = New System.Drawing.Size(43, 30)
            Me.BuscarCarpetaButton.TabIndex = 7
            Me.BuscarCarpetaButton.UseVisualStyleBackColor = True
            '
            'FormExportarGobAtlantico
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(677, 262)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.ExportarButton)
            Me.Controls.Add(Me.MainGroupBox)
            Me.Name = "FormExportarGobAtlantico"
            Me.Text = "Exportar"
            Me.MainGroupBox.ResumeLayout(False)
            Me.MainGroupBox.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            CType(Me.ExpedientesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents ExportarButton As System.Windows.Forms.Button
        Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
        Private WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents VisorRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents XMLRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents CarpetaSalidaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents BuscarCarpetaButton As System.Windows.Forms.Button
        Friend WithEvents TXTRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents FechaProcesoFinalLabel As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoFinalDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents BuscarFechaButton As System.Windows.Forms.Button
        Friend WithEvents FechaProcesoDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents OTLabel As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents ExpedientesDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents Expediente As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Fecha_Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_OT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Exportar As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Ruta As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents OTDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents id_OT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_OT_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnCerrado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents ColumnExportado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents ColumnTipoExportacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnRuta As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace

