Namespace Confinanciera.Garantias.Imaging.Forms
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormExport))
            Me.MainGroupBox = New System.Windows.Forms.GroupBox()
            Me.BuscarFechaButton = New System.Windows.Forms.Button()
            Me.OTDataGridView = New System.Windows.Forms.DataGridView()
            Me.id_OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_OT_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnCerrado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.ColumnExportado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.ColumnTipoExportacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnRuta = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FechaProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.OTLabel = New System.Windows.Forms.Label()
            Me.FechaProcesoLabel = New System.Windows.Forms.Label()
            Me.BuscarCarpetaButton = New System.Windows.Forms.Button()
            Me.lblCarpeta = New System.Windows.Forms.Label()
            Me.CarpetaSalidaTextBox = New System.Windows.Forms.TextBox()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.ExportarButton = New System.Windows.Forms.Button()
            Me.MainGroupBox.SuspendLayout()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'MainGroupBox
            '
            Me.MainGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MainGroupBox.Controls.Add(Me.BuscarFechaButton)
            Me.MainGroupBox.Controls.Add(Me.OTDataGridView)
            Me.MainGroupBox.Controls.Add(Me.FechaProcesoDateTimePicker)
            Me.MainGroupBox.Controls.Add(Me.OTLabel)
            Me.MainGroupBox.Controls.Add(Me.FechaProcesoLabel)
            Me.MainGroupBox.Controls.Add(Me.BuscarCarpetaButton)
            Me.MainGroupBox.Controls.Add(Me.lblCarpeta)
            Me.MainGroupBox.Controls.Add(Me.CarpetaSalidaTextBox)
            Me.MainGroupBox.Location = New System.Drawing.Point(9, 6)
            Me.MainGroupBox.Name = "MainGroupBox"
            Me.MainGroupBox.Size = New System.Drawing.Size(559, 297)
            Me.MainGroupBox.TabIndex = 0
            Me.MainGroupBox.TabStop = False
            Me.MainGroupBox.Text = "Parametros de exportación"
            '
            'BuscarFechaButton
            '
            Me.BuscarFechaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarFechaButton.Image = Global.Garantias.Plugin.My.Resources.Resources.Buscar
            Me.BuscarFechaButton.Location = New System.Drawing.Point(389, 43)
            Me.BuscarFechaButton.Name = "BuscarFechaButton"
            Me.BuscarFechaButton.Size = New System.Drawing.Size(37, 30)
            Me.BuscarFechaButton.TabIndex = 2
            Me.BuscarFechaButton.UseVisualStyleBackColor = True
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
            Me.OTDataGridView.Location = New System.Drawing.Point(14, 94)
            Me.OTDataGridView.MultiSelect = False
            Me.OTDataGridView.Name = "OTDataGridView"
            Me.OTDataGridView.ReadOnly = True
            Me.OTDataGridView.RowHeadersWidth = 10
            Me.OTDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.OTDataGridView.Size = New System.Drawing.Size(535, 127)
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
            'FechaProcesoDateTimePicker
            '
            Me.FechaProcesoDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoDateTimePicker.Location = New System.Drawing.Point(13, 43)
            Me.FechaProcesoDateTimePicker.Name = "FechaProcesoDateTimePicker"
            Me.FechaProcesoDateTimePicker.Size = New System.Drawing.Size(361, 26)
            Me.FechaProcesoDateTimePicker.TabIndex = 1
            '
            'OTLabel
            '
            Me.OTLabel.AutoSize = True
            Me.OTLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OTLabel.Location = New System.Drawing.Point(12, 76)
            Me.OTLabel.Name = "OTLabel"
            Me.OTLabel.Size = New System.Drawing.Size(32, 15)
            Me.OTLabel.TabIndex = 3
            Me.OTLabel.Text = "OTs"
            '
            'FechaProcesoLabel
            '
            Me.FechaProcesoLabel.AutoSize = True
            Me.FechaProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoLabel.Location = New System.Drawing.Point(10, 24)
            Me.FechaProcesoLabel.Name = "FechaProcesoLabel"
            Me.FechaProcesoLabel.Size = New System.Drawing.Size(121, 15)
            Me.FechaProcesoLabel.TabIndex = 0
            Me.FechaProcesoLabel.Text = "Fecha de proceso"
            '
            'BuscarCarpetaButton
            '
            Me.BuscarCarpetaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarCarpetaButton.Image = Global.Garantias.Plugin.My.Resources.Resources.Folder
            Me.BuscarCarpetaButton.Location = New System.Drawing.Point(512, 245)
            Me.BuscarCarpetaButton.Name = "BuscarCarpetaButton"
            Me.BuscarCarpetaButton.Size = New System.Drawing.Size(37, 30)
            Me.BuscarCarpetaButton.TabIndex = 7
            Me.BuscarCarpetaButton.UseVisualStyleBackColor = True
            '
            'lblCarpeta
            '
            Me.lblCarpeta.AutoSize = True
            Me.lblCarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCarpeta.Location = New System.Drawing.Point(11, 235)
            Me.lblCarpeta.Name = "lblCarpeta"
            Me.lblCarpeta.Size = New System.Drawing.Size(122, 15)
            Me.lblCarpeta.TabIndex = 5
            Me.lblCarpeta.Text = "Carpeta de Salida"
            '
            'CarpetaSalidaTextBox
            '
            Me.CarpetaSalidaTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CarpetaSalidaTextBox.Location = New System.Drawing.Point(14, 255)
            Me.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox"
            Me.CarpetaSalidaTextBox.Size = New System.Drawing.Size(492, 20)
            Me.CarpetaSalidaTextBox.TabIndex = 6
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.Garantias.Plugin.My.Resources.Resources.Cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(469, 319)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(89, 37)
            Me.CancelarButton.TabIndex = 2
            Me.CancelarButton.Text = "Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'ExportarButton
            '
            Me.ExportarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ExportarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ExportarButton.Image = Global.Garantias.Plugin.My.Resources.Resources.Aceptar
            Me.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ExportarButton.Location = New System.Drawing.Point(370, 319)
            Me.ExportarButton.Name = "ExportarButton"
            Me.ExportarButton.Size = New System.Drawing.Size(89, 37)
            Me.ExportarButton.TabIndex = 1
            Me.ExportarButton.Text = "Exportar"
            Me.ExportarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ExportarButton.UseVisualStyleBackColor = True
            '
            'FormExport
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(579, 368)
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
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents BuscarCarpetaButton As System.Windows.Forms.Button
        Friend WithEvents lblCarpeta As System.Windows.Forms.Label
        Friend WithEvents CarpetaSalidaTextBox As System.Windows.Forms.TextBox
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
    End Class
End Namespace