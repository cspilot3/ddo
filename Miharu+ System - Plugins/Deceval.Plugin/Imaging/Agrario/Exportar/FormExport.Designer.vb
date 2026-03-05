Namespace Imaging.Agrario.Exportar
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormExport))
            Me.MainGroupBox = New System.Windows.Forms.GroupBox()
            Me.TabControlExportacion = New System.Windows.Forms.TabControl()
            Me.TabOts = New System.Windows.Forms.TabPage()
            Me.OTDataGridView = New System.Windows.Forms.DataGridView()
            Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.DataGridViewCheckBoxColumn2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.DataGridViewCheckBoxColumn3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.BuscarFechaButton = New System.Windows.Forms.Button()
            Me.FechaProcesoFinalDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.FechaProcesoInicialDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.TabPageLog = New System.Windows.Forms.TabPage()
            Me.ArchivoDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.SelecionarArchivoLabel = New System.Windows.Forms.Label()
            Me.BuscarArchivoButton = New System.Windows.Forms.Button()
            Me.BuscarCarpetaButton = New System.Windows.Forms.Button()
            Me.lblCarpeta = New System.Windows.Forms.Label()
            Me.CarpetaSalidaTextBox = New System.Windows.Forms.TextBox()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.ExportarButton = New System.Windows.Forms.Button()
            Me.ArchivoOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
            Me.MainGroupBox.SuspendLayout()
            Me.TabControlExportacion.SuspendLayout()
            Me.TabOts.SuspendLayout()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TabPageLog.SuspendLayout()
            Me.SuspendLayout()
            '
            'MainGroupBox
            '
            Me.MainGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MainGroupBox.Controls.Add(Me.TabControlExportacion)
            Me.MainGroupBox.Controls.Add(Me.BuscarCarpetaButton)
            Me.MainGroupBox.Controls.Add(Me.lblCarpeta)
            Me.MainGroupBox.Controls.Add(Me.CarpetaSalidaTextBox)
            Me.MainGroupBox.Location = New System.Drawing.Point(9, 6)
            Me.MainGroupBox.Name = "MainGroupBox"
            Me.MainGroupBox.Size = New System.Drawing.Size(685, 361)
            Me.MainGroupBox.TabIndex = 0
            Me.MainGroupBox.TabStop = False
            Me.MainGroupBox.Text = "Parametros de exportación"
            '
            'TabControlExportacion
            '
            Me.TabControlExportacion.Controls.Add(Me.TabOts)
            Me.TabControlExportacion.Controls.Add(Me.TabPageLog)
            Me.TabControlExportacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TabControlExportacion.Location = New System.Drawing.Point(14, 19)
            Me.TabControlExportacion.Name = "TabControlExportacion"
            Me.TabControlExportacion.SelectedIndex = 0
            Me.TabControlExportacion.Size = New System.Drawing.Size(657, 277)
            Me.TabControlExportacion.TabIndex = 29
            '
            'TabOts
            '
            Me.TabOts.Controls.Add(Me.OTDataGridView)
            Me.TabOts.Controls.Add(Me.Label4)
            Me.TabOts.Controls.Add(Me.BuscarFechaButton)
            Me.TabOts.Controls.Add(Me.FechaProcesoFinalDateTimePicker)
            Me.TabOts.Controls.Add(Me.Label3)
            Me.TabOts.Controls.Add(Me.FechaProcesoInicialDateTimePicker)
            Me.TabOts.Controls.Add(Me.Label2)
            Me.TabOts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TabOts.Location = New System.Drawing.Point(4, 22)
            Me.TabOts.Name = "TabOts"
            Me.TabOts.Padding = New System.Windows.Forms.Padding(3)
            Me.TabOts.Size = New System.Drawing.Size(649, 251)
            Me.TabOts.TabIndex = 0
            Me.TabOts.Text = "Fecha Proceso/OT"
            Me.TabOts.UseVisualStyleBackColor = True
            '
            'OTDataGridView
            '
            Me.OTDataGridView.AllowUserToAddRows = False
            Me.OTDataGridView.AllowUserToDeleteRows = False
            Me.OTDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OTDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.OTDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewCheckBoxColumn1, Me.DataGridViewCheckBoxColumn2, Me.DataGridViewCheckBoxColumn3, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4})
            Me.OTDataGridView.Location = New System.Drawing.Point(6, 93)
            Me.OTDataGridView.MultiSelect = False
            Me.OTDataGridView.Name = "OTDataGridView"
            Me.OTDataGridView.RowHeadersWidth = 10
            Me.OTDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.OTDataGridView.Size = New System.Drawing.Size(625, 137)
            Me.OTDataGridView.TabIndex = 32
            '
            'DataGridViewTextBoxColumn1
            '
            Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.DataGridViewTextBoxColumn1.DataPropertyName = "id_OT"
            Me.DataGridViewTextBoxColumn1.HeaderText = "OT"
            Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
            '
            'DataGridViewTextBoxColumn2
            '
            Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.DataGridViewTextBoxColumn2.DataPropertyName = "Nombre_OT_Tipo"
            Me.DataGridViewTextBoxColumn2.HeaderText = "Tipo OT"
            Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
            Me.DataGridViewTextBoxColumn2.Width = 200
            '
            'DataGridViewCheckBoxColumn1
            '
            Me.DataGridViewCheckBoxColumn1.DataPropertyName = "Cerrado"
            Me.DataGridViewCheckBoxColumn1.HeaderText = "Cerrado"
            Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
            Me.DataGridViewCheckBoxColumn1.ReadOnly = True
            Me.DataGridViewCheckBoxColumn1.Width = 60
            '
            'DataGridViewCheckBoxColumn2
            '
            Me.DataGridViewCheckBoxColumn2.HeaderText = "Exportar"
            Me.DataGridViewCheckBoxColumn2.Name = "DataGridViewCheckBoxColumn2"
            '
            'DataGridViewCheckBoxColumn3
            '
            Me.DataGridViewCheckBoxColumn3.DataPropertyName = "Exportado"
            Me.DataGridViewCheckBoxColumn3.HeaderText = "Exportado"
            Me.DataGridViewCheckBoxColumn3.Name = "DataGridViewCheckBoxColumn3"
            Me.DataGridViewCheckBoxColumn3.ReadOnly = True
            Me.DataGridViewCheckBoxColumn3.Width = 60
            '
            'DataGridViewTextBoxColumn3
            '
            Me.DataGridViewTextBoxColumn3.DataPropertyName = "Nombre_Exportacion_Tipo"
            Me.DataGridViewTextBoxColumn3.HeaderText = "Modo"
            Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
            Me.DataGridViewTextBoxColumn3.Width = 40
            '
            'DataGridViewTextBoxColumn4
            '
            Me.DataGridViewTextBoxColumn4.DataPropertyName = "Ruta"
            Me.DataGridViewTextBoxColumn4.HeaderText = "Ruta"
            Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
            Me.DataGridViewTextBoxColumn4.Width = 300
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(7, 75)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(32, 15)
            Me.Label4.TabIndex = 31
            Me.Label4.Text = "OTs"
            '
            'BuscarFechaButton
            '
            Me.BuscarFechaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarFechaButton.Image = Global.Deceval.Plugin.My.Resources.Resources.Buscar
            Me.BuscarFechaButton.Location = New System.Drawing.Point(597, 24)
            Me.BuscarFechaButton.Name = "BuscarFechaButton"
            Me.BuscarFechaButton.Size = New System.Drawing.Size(37, 30)
            Me.BuscarFechaButton.TabIndex = 30
            Me.BuscarFechaButton.UseVisualStyleBackColor = True
            '
            'FechaProcesoFinalDateTimePicker
            '
            Me.FechaProcesoFinalDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoFinalDateTimePicker.Location = New System.Drawing.Point(299, 32)
            Me.FechaProcesoFinalDateTimePicker.Name = "FechaProcesoFinalDateTimePicker"
            Me.FechaProcesoFinalDateTimePicker.Size = New System.Drawing.Size(279, 22)
            Me.FechaProcesoFinalDateTimePicker.TabIndex = 29
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(301, 14)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(138, 15)
            Me.Label3.TabIndex = 28
            Me.Label3.Text = "Fecha Proceso Final"
            '
            'FechaProcesoInicialDateTimePicker
            '
            Me.FechaProcesoInicialDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoInicialDateTimePicker.Location = New System.Drawing.Point(9, 32)
            Me.FechaProcesoInicialDateTimePicker.Name = "FechaProcesoInicialDateTimePicker"
            Me.FechaProcesoInicialDateTimePicker.Size = New System.Drawing.Size(279, 22)
            Me.FechaProcesoInicialDateTimePicker.TabIndex = 26
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(6, 14)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(145, 15)
            Me.Label2.TabIndex = 25
            Me.Label2.Text = "Fecha Proceso Inicial"
            '
            'TabPageLog
            '
            Me.TabPageLog.Controls.Add(Me.ArchivoDesktopTextBox)
            Me.TabPageLog.Controls.Add(Me.SelecionarArchivoLabel)
            Me.TabPageLog.Controls.Add(Me.BuscarArchivoButton)
            Me.TabPageLog.Location = New System.Drawing.Point(4, 22)
            Me.TabPageLog.Name = "TabPageLog"
            Me.TabPageLog.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPageLog.Size = New System.Drawing.Size(649, 251)
            Me.TabPageLog.TabIndex = 1
            Me.TabPageLog.Text = "Log"
            Me.TabPageLog.UseVisualStyleBackColor = True
            '
            'ArchivoDesktopTextBox
            '
            Me.ArchivoDesktopTextBox._Obligatorio = False
            Me.ArchivoDesktopTextBox._PermitePegar = False
            Me.ArchivoDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.ArchivoDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.ArchivoDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ArchivoDesktopTextBox.DateFormat = Nothing
            Me.ArchivoDesktopTextBox.DisabledEnter = False
            Me.ArchivoDesktopTextBox.DisabledTab = False
            Me.ArchivoDesktopTextBox.EnabledShortCuts = False
            Me.ArchivoDesktopTextBox.fk_Campo = 0
            Me.ArchivoDesktopTextBox.fk_Documento = 0
            Me.ArchivoDesktopTextBox.fk_Validacion = 0
            Me.ArchivoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ArchivoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ArchivoDesktopTextBox.Location = New System.Drawing.Point(10, 38)
            Me.ArchivoDesktopTextBox.MaskedTextBox_Property = ""
            Me.ArchivoDesktopTextBox.MaximumLength = CType(0, Short)
            Me.ArchivoDesktopTextBox.MinimumLength = CType(0, Short)
            Me.ArchivoDesktopTextBox.Name = "ArchivoDesktopTextBox"
            Me.ArchivoDesktopTextBox.Obligatorio = False
            Me.ArchivoDesktopTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.ArchivoDesktopTextBox.Rango = Rango1
            Me.ArchivoDesktopTextBox.ReadOnly = True
            Me.ArchivoDesktopTextBox.Size = New System.Drawing.Size(301, 20)
            Me.ArchivoDesktopTextBox.TabIndex = 34
            Me.ArchivoDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ArchivoDesktopTextBox.Usa_Decimales = False
            Me.ArchivoDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'SelecionarArchivoLabel
            '
            Me.SelecionarArchivoLabel.AutoSize = True
            Me.SelecionarArchivoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.SelecionarArchivoLabel.Location = New System.Drawing.Point(7, 19)
            Me.SelecionarArchivoLabel.Name = "SelecionarArchivoLabel"
            Me.SelecionarArchivoLabel.Size = New System.Drawing.Size(185, 13)
            Me.SelecionarArchivoLabel.TabIndex = 33
            Me.SelecionarArchivoLabel.Text = "Seleccionar archivo de cargue:"
            '
            'BuscarArchivoButton
            '
            Me.BuscarArchivoButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarArchivoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarArchivoButton.Location = New System.Drawing.Point(342, 38)
            Me.BuscarArchivoButton.Name = "BuscarArchivoButton"
            Me.BuscarArchivoButton.Size = New System.Drawing.Size(87, 23)
            Me.BuscarArchivoButton.TabIndex = 35
            Me.BuscarArchivoButton.Text = "&Buscar"
            Me.BuscarArchivoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarArchivoButton.UseVisualStyleBackColor = True
            '
            'BuscarCarpetaButton
            '
            Me.BuscarCarpetaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarCarpetaButton.Image = Global.Deceval.Plugin.My.Resources.Resources.Folder
            Me.BuscarCarpetaButton.Location = New System.Drawing.Point(638, 320)
            Me.BuscarCarpetaButton.Name = "BuscarCarpetaButton"
            Me.BuscarCarpetaButton.Size = New System.Drawing.Size(37, 30)
            Me.BuscarCarpetaButton.TabIndex = 7
            Me.BuscarCarpetaButton.UseVisualStyleBackColor = True
            '
            'lblCarpeta
            '
            Me.lblCarpeta.AutoSize = True
            Me.lblCarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCarpeta.Location = New System.Drawing.Point(11, 310)
            Me.lblCarpeta.Name = "lblCarpeta"
            Me.lblCarpeta.Size = New System.Drawing.Size(122, 15)
            Me.lblCarpeta.TabIndex = 5
            Me.lblCarpeta.Text = "Carpeta de Salida"
            '
            'CarpetaSalidaTextBox
            '
            Me.CarpetaSalidaTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CarpetaSalidaTextBox.Location = New System.Drawing.Point(14, 330)
            Me.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox"
            Me.CarpetaSalidaTextBox.Size = New System.Drawing.Size(618, 20)
            Me.CarpetaSalidaTextBox.TabIndex = 6
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.Deceval.Plugin.My.Resources.Resources.Cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(595, 383)
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
            Me.ExportarButton.Image = Global.Deceval.Plugin.My.Resources.Resources.Aceptar
            Me.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ExportarButton.Location = New System.Drawing.Point(496, 383)
            Me.ExportarButton.Name = "ExportarButton"
            Me.ExportarButton.Size = New System.Drawing.Size(89, 37)
            Me.ExportarButton.TabIndex = 1
            Me.ExportarButton.Text = "Exportar"
            Me.ExportarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ExportarButton.UseVisualStyleBackColor = True
            '
            'ArchivoOpenFileDialog
            '
            Me.ArchivoOpenFileDialog.FileName = "ArchivoOpenFileDialog"
            '
            'FormExport
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(705, 432)
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
            Me.TabControlExportacion.ResumeLayout(False)
            Me.TabOts.ResumeLayout(False)
            Me.TabOts.PerformLayout()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TabPageLog.ResumeLayout(False)
            Me.TabPageLog.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents BuscarCarpetaButton As System.Windows.Forms.Button
        Friend WithEvents lblCarpeta As System.Windows.Forms.Label
        Friend WithEvents CarpetaSalidaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents ExportarButton As System.Windows.Forms.Button
        Friend WithEvents TabControlExportacion As System.Windows.Forms.TabControl
        Friend WithEvents TabOts As System.Windows.Forms.TabPage
        Friend WithEvents OTDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents DataGridViewCheckBoxColumn2 As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents DataGridViewCheckBoxColumn3 As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents BuscarFechaButton As System.Windows.Forms.Button
        Friend WithEvents FechaProcesoFinalDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoInicialDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents TabPageLog As System.Windows.Forms.TabPage
        Friend WithEvents ArchivoDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents SelecionarArchivoLabel As System.Windows.Forms.Label
        Friend WithEvents BuscarArchivoButton As System.Windows.Forms.Button
        Friend WithEvents ArchivoOpenFileDialog As System.Windows.Forms.OpenFileDialog
    End Class
End Namespace