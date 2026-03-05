<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormExport
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
        Me.MainGroupBox = New System.Windows.Forms.GroupBox()
        Me.ExpedientesDataGridView = New System.Windows.Forms.DataGridView()
        Me.FechaProcesoFinalLabel = New System.Windows.Forms.Label()
        Me.FechaProcesoFinalDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.BuscarFechaButton = New System.Windows.Forms.Button()
        Me.FechaProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.FechaProcesoLabel = New System.Windows.Forms.Label()
        Me.BuscarCarpetaButton = New System.Windows.Forms.Button()
        Me.lblCarpeta = New System.Windows.Forms.Label()
        Me.CarpetaSalidaTextBox = New System.Windows.Forms.TextBox()
        Me.CancelarButton = New System.Windows.Forms.Button()
        Me.ExportarButton = New System.Windows.Forms.Button()
        Me.Expediente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha_Recaudo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fk_OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Exportar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Ruta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fk_Folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fk_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_Version = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.File_Unique_Identifier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fk_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fk_Entidad_Servidor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fk_Servidor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre_Imagen_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tamaño_Imagen_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fk_Grupo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MainGroupBox.SuspendLayout()
        CType(Me.ExpedientesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainGroupBox
        '
        Me.MainGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MainGroupBox.Controls.Add(Me.ExpedientesDataGridView)
        Me.MainGroupBox.Controls.Add(Me.FechaProcesoFinalLabel)
        Me.MainGroupBox.Controls.Add(Me.FechaProcesoFinalDateTimePicker)
        Me.MainGroupBox.Controls.Add(Me.BuscarFechaButton)
        Me.MainGroupBox.Controls.Add(Me.FechaProcesoDateTimePicker)
        Me.MainGroupBox.Controls.Add(Me.FechaProcesoLabel)
        Me.MainGroupBox.Controls.Add(Me.BuscarCarpetaButton)
        Me.MainGroupBox.Controls.Add(Me.lblCarpeta)
        Me.MainGroupBox.Controls.Add(Me.CarpetaSalidaTextBox)
        Me.MainGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainGroupBox.Location = New System.Drawing.Point(4, 20)
        Me.MainGroupBox.Name = "MainGroupBox"
        Me.MainGroupBox.Size = New System.Drawing.Size(652, 116)
        Me.MainGroupBox.TabIndex = 3
        Me.MainGroupBox.TabStop = False
        Me.MainGroupBox.Text = "Parametros de exportación"
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
        Me.ExpedientesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Expediente, Me.Fecha_Recaudo, Me.fk_OT, Me.Exportar, Me.Ruta, Me.fk_Folder, Me.fk_File, Me.id_Version, Me.File_Unique_Identifier, Me.fk_Documento, Me.Nombre_Documento, Me.fk_Entidad_Servidor, Me.fk_Servidor, Me.Nombre_Imagen_File, Me.Tamaño_Imagen_File, Me.fk_Grupo})
        Me.ExpedientesDataGridView.Location = New System.Drawing.Point(15, 148)
        Me.ExpedientesDataGridView.MultiSelect = False
        Me.ExpedientesDataGridView.Name = "ExpedientesDataGridView"
        Me.ExpedientesDataGridView.RowHeadersWidth = 10
        Me.ExpedientesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ExpedientesDataGridView.Size = New System.Drawing.Size(624, 0)
        Me.ExpedientesDataGridView.TabIndex = 19
        Me.ExpedientesDataGridView.Visible = False
        '
        'FechaProcesoFinalLabel
        '
        Me.FechaProcesoFinalLabel.AutoSize = True
        Me.FechaProcesoFinalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FechaProcesoFinalLabel.Location = New System.Drawing.Point(335, 19)
        Me.FechaProcesoFinalLabel.Name = "FechaProcesoFinalLabel"
        Me.FechaProcesoFinalLabel.Size = New System.Drawing.Size(143, 15)
        Me.FechaProcesoFinalLabel.TabIndex = 12
        Me.FechaProcesoFinalLabel.Text = "Fecha Recaudo Final"
        '
        'FechaProcesoFinalDateTimePicker
        '
        Me.FechaProcesoFinalDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FechaProcesoFinalDateTimePicker.Location = New System.Drawing.Point(338, 38)
        Me.FechaProcesoFinalDateTimePicker.Name = "FechaProcesoFinalDateTimePicker"
        Me.FechaProcesoFinalDateTimePicker.Size = New System.Drawing.Size(301, 22)
        Me.FechaProcesoFinalDateTimePicker.TabIndex = 11
        '
        'BuscarFechaButton
        '
        Me.BuscarFechaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BuscarFechaButton.Image = Global.Citibank.Plugin.My.Resources.Resources.btnBuscar
        Me.BuscarFechaButton.Location = New System.Drawing.Point(597, 30)
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
        Me.FechaProcesoDateTimePicker.Size = New System.Drawing.Size(294, 22)
        Me.FechaProcesoDateTimePicker.TabIndex = 1
        '
        'FechaProcesoLabel
        '
        Me.FechaProcesoLabel.AutoSize = True
        Me.FechaProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FechaProcesoLabel.Location = New System.Drawing.Point(12, 19)
        Me.FechaProcesoLabel.Name = "FechaProcesoLabel"
        Me.FechaProcesoLabel.Size = New System.Drawing.Size(150, 15)
        Me.FechaProcesoLabel.TabIndex = 0
        Me.FechaProcesoLabel.Text = "Fecha Recaudo Inicial"
        '
        'BuscarCarpetaButton
        '
        Me.BuscarCarpetaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BuscarCarpetaButton.Image = Global.Citibank.Plugin.My.Resources.Resources.btnDestape
        Me.BuscarCarpetaButton.Location = New System.Drawing.Point(597, 75)
        Me.BuscarCarpetaButton.Name = "BuscarCarpetaButton"
        Me.BuscarCarpetaButton.Size = New System.Drawing.Size(43, 30)
        Me.BuscarCarpetaButton.TabIndex = 7
        Me.BuscarCarpetaButton.UseVisualStyleBackColor = True
        '
        'lblCarpeta
        '
        Me.lblCarpeta.AutoSize = True
        Me.lblCarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCarpeta.Location = New System.Drawing.Point(12, 66)
        Me.lblCarpeta.Name = "lblCarpeta"
        Me.lblCarpeta.Size = New System.Drawing.Size(122, 15)
        Me.lblCarpeta.TabIndex = 5
        Me.lblCarpeta.Text = "Carpeta de Salida"
        '
        'CarpetaSalidaTextBox
        '
        Me.CarpetaSalidaTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CarpetaSalidaTextBox.Location = New System.Drawing.Point(16, 85)
        Me.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox"
        Me.CarpetaSalidaTextBox.Size = New System.Drawing.Size(573, 20)
        Me.CarpetaSalidaTextBox.TabIndex = 6
        '
        'CancelarButton
        '
        Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CancelarButton.Image = Global.Citibank.Plugin.My.Resources.Resources.cancel
        Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelarButton.Location = New System.Drawing.Point(540, 142)
        Me.CancelarButton.Name = "CancelarButton"
        Me.CancelarButton.Size = New System.Drawing.Size(104, 37)
        Me.CancelarButton.TabIndex = 5
        Me.CancelarButton.Text = "Cancelar"
        Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelarButton.UseVisualStyleBackColor = True
        '
        'ExportarButton
        '
        Me.ExportarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExportarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExportarButton.Image = Global.Citibank.Plugin.My.Resources.Resources.Aceptar
        Me.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExportarButton.Location = New System.Drawing.Point(425, 142)
        Me.ExportarButton.Name = "ExportarButton"
        Me.ExportarButton.Size = New System.Drawing.Size(104, 37)
        Me.ExportarButton.TabIndex = 4
        Me.ExportarButton.Text = "Exportar"
        Me.ExportarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExportarButton.UseVisualStyleBackColor = True
        '
        'Expediente
        '
        Me.Expediente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Expediente.DataPropertyName = "fk_Expediente"
        Me.Expediente.HeaderText = "Expediente"
        Me.Expediente.Name = "Expediente"
        Me.Expediente.ReadOnly = True
        '
        'Fecha_Recaudo
        '
        Me.Fecha_Recaudo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Fecha_Recaudo.DataPropertyName = "Fecha_Recaudo"
        Me.Fecha_Recaudo.HeaderText = "Fecha de Recaudo"
        Me.Fecha_Recaudo.Name = "Fecha_Recaudo"
        Me.Fecha_Recaudo.ReadOnly = True
        Me.Fecha_Recaudo.Width = 150
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
        'fk_Folder
        '
        Me.fk_Folder.DataPropertyName = "fk_Folder"
        Me.fk_Folder.HeaderText = "Folder"
        Me.fk_Folder.Name = "fk_Folder"
        Me.fk_Folder.ReadOnly = True
        '
        'fk_File
        '
        Me.fk_File.DataPropertyName = "fk_File"
        Me.fk_File.HeaderText = "File"
        Me.fk_File.Name = "fk_File"
        Me.fk_File.ReadOnly = True
        '
        'id_Version
        '
        Me.id_Version.DataPropertyName = "id_Version"
        Me.id_Version.HeaderText = "id_Version"
        Me.id_Version.Name = "id_Version"
        Me.id_Version.ReadOnly = True
        '
        'File_Unique_Identifier
        '
        Me.File_Unique_Identifier.DataPropertyName = "File_Unique_Identifier"
        Me.File_Unique_Identifier.HeaderText = "File Unique Identifier"
        Me.File_Unique_Identifier.Name = "File_Unique_Identifier"
        Me.File_Unique_Identifier.ReadOnly = True
        '
        'fk_Documento
        '
        Me.fk_Documento.DataPropertyName = "fk_Documento"
        Me.fk_Documento.HeaderText = "Documento"
        Me.fk_Documento.Name = "fk_Documento"
        Me.fk_Documento.ReadOnly = True
        '
        'Nombre_Documento
        '
        Me.Nombre_Documento.DataPropertyName = "Nombre_Documento"
        Me.Nombre_Documento.HeaderText = "Nombre Documento"
        Me.Nombre_Documento.Name = "Nombre_Documento"
        Me.Nombre_Documento.ReadOnly = True
        '
        'fk_Entidad_Servidor
        '
        Me.fk_Entidad_Servidor.DataPropertyName = "fk_Entidad_Servidor"
        Me.fk_Entidad_Servidor.HeaderText = "fk_Entidad_Servidor"
        Me.fk_Entidad_Servidor.Name = "fk_Entidad_Servidor"
        Me.fk_Entidad_Servidor.ReadOnly = True
        '
        'fk_Servidor
        '
        Me.fk_Servidor.DataPropertyName = "fk_Servidor"
        Me.fk_Servidor.HeaderText = "Servidor"
        Me.fk_Servidor.Name = "fk_Servidor"
        Me.fk_Servidor.ReadOnly = True
        '
        'Nombre_Imagen_File
        '
        Me.Nombre_Imagen_File.DataPropertyName = "Nombre_Imagen_File"
        Me.Nombre_Imagen_File.HeaderText = "Nombre Imagen File"
        Me.Nombre_Imagen_File.Name = "Nombre_Imagen_File"
        Me.Nombre_Imagen_File.ReadOnly = True
        '
        'Tamaño_Imagen_File
        '
        Me.Tamaño_Imagen_File.HeaderText = "Tamaño Imagen File"
        Me.Tamaño_Imagen_File.Name = "Tamaño_Imagen_File"
        Me.Tamaño_Imagen_File.ReadOnly = True
        '
        'fk_Grupo
        '
        Me.fk_Grupo.DataPropertyName = "fk_Grupo"
        Me.fk_Grupo.HeaderText = "Grupo"
        Me.fk_Grupo.Name = "fk_Grupo"
        Me.fk_Grupo.ReadOnly = True
        '
        'FormExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(661, 191)
        Me.Controls.Add(Me.MainGroupBox)
        Me.Controls.Add(Me.CancelarButton)
        Me.Controls.Add(Me.ExportarButton)
        Me.Name = "FormExport"
        Me.Text = "FormExport"
        Me.MainGroupBox.ResumeLayout(False)
        Me.MainGroupBox.PerformLayout()
        CType(Me.ExpedientesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents FechaProcesoFinalLabel As System.Windows.Forms.Label
    Friend WithEvents FechaProcesoFinalDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents BuscarFechaButton As System.Windows.Forms.Button
    Friend WithEvents FechaProcesoDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents FechaProcesoLabel As System.Windows.Forms.Label
    Friend WithEvents BuscarCarpetaButton As System.Windows.Forms.Button
    Friend WithEvents lblCarpeta As System.Windows.Forms.Label
    Friend WithEvents CarpetaSalidaTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CancelarButton As System.Windows.Forms.Button
    Friend WithEvents ExportarButton As System.Windows.Forms.Button
    Friend WithEvents ExpedientesDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Expediente As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fecha_Recaudo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fk_OT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Exportar As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Ruta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fk_Folder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fk_File As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id_Version As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents File_Unique_Identifier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fk_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nombre_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fk_Entidad_Servidor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fk_Servidor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nombre_Imagen_File As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tamaño_Imagen_File As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fk_Grupo As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
