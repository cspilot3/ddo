Namespace Procesos.Exportar
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormExportCredivalores
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormExportCredivalores))
            Me.TXTRadioButton = New System.Windows.Forms.RadioButton()
            Me.MainGroupBox = New System.Windows.Forms.GroupBox()
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
            Me.Nombre_Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.ExportarButton = New System.Windows.Forms.Button()
            Me.Recuperar = New System.Windows.Forms.Button()
            Me.MainGroupBox.SuspendLayout()
            CType(Me.ExpedientesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
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
            Me.MainGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MainGroupBox.Location = New System.Drawing.Point(10, 6)
            Me.MainGroupBox.Name = "MainGroupBox"
            Me.MainGroupBox.Size = New System.Drawing.Size(652, 393)
            Me.MainGroupBox.TabIndex = 0
            Me.MainGroupBox.TabStop = False
            Me.MainGroupBox.Text = "Parametros de exportación"
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
            Me.OTLabel.Location = New System.Drawing.Point(10, 76)
            Me.OTLabel.Name = "OTLabel"
            Me.OTLabel.Size = New System.Drawing.Size(69, 15)
            Me.OTLabel.TabIndex = 3
            Me.OTLabel.Text = "Proyectos"
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
            Me.ExpedientesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nombre_Proyecto})
            Me.ExpedientesDataGridView.Location = New System.Drawing.Point(13, 94)
            Me.ExpedientesDataGridView.MultiSelect = False
            Me.ExpedientesDataGridView.Name = "ExpedientesDataGridView"
            Me.ExpedientesDataGridView.ReadOnly = True
            Me.ExpedientesDataGridView.RowHeadersWidth = 10
            Me.ExpedientesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ExpedientesDataGridView.Size = New System.Drawing.Size(624, 191)
            Me.ExpedientesDataGridView.TabIndex = 16
            Me.ExpedientesDataGridView.Visible = False
            '
            'Nombre_Proyecto
            '
            Me.Nombre_Proyecto.DataPropertyName = "Nombre_Proyecto"
            Me.Nombre_Proyecto.HeaderText = "Proyectos"
            Me.Nombre_Proyecto.Name = "Nombre_Proyecto"
            Me.Nombre_Proyecto.ReadOnly = True
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(547, 405)
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
            Me.ExportarButton.Location = New System.Drawing.Point(432, 405)
            Me.ExportarButton.Name = "ExportarButton"
            Me.ExportarButton.Size = New System.Drawing.Size(104, 37)
            Me.ExportarButton.TabIndex = 1
            Me.ExportarButton.Text = "Exportar"
            Me.ExportarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ExportarButton.UseVisualStyleBackColor = True
            '
            'Recuperar
            '
            Me.Recuperar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Recuperar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Recuperar.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Aceptar
            Me.Recuperar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Recuperar.Location = New System.Drawing.Point(255, 405)
            Me.Recuperar.Name = "Recuperar"
            Me.Recuperar.Size = New System.Drawing.Size(171, 37)
            Me.Recuperar.TabIndex = 3
            Me.Recuperar.Text = "Retomar exportación "
            Me.Recuperar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.Recuperar.UseVisualStyleBackColor = True
            '
            'FormExportCredivalores
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(675, 454)
            Me.Controls.Add(Me.Recuperar)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.ExportarButton)
            Me.Controls.Add(Me.MainGroupBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormExportCredivalores"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Exportar"
            Me.MainGroupBox.ResumeLayout(False)
            Me.MainGroupBox.PerformLayout()
            CType(Me.ExpedientesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
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
        Friend WithEvents BuscarFechaButton As System.Windows.Forms.Button
        Friend WithEvents FechaProcesoFinalLabel As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoFinalDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents ExpedientesDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents Nombre_Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Recuperar As System.Windows.Forms.Button
    End Class
End Namespace