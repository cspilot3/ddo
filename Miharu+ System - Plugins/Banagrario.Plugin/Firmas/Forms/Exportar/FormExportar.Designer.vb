Namespace Firmas.Forms.Exportar
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormExportar
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormExportar))
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.FechaProcesoPicker = New System.Windows.Forms.DateTimePicker()
            Me.LbFechaProceso = New System.Windows.Forms.Label()
            Me.OTDataGridView = New System.Windows.Forms.DataGridView()
            Me.id_OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_OT_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnCerrado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.ColumnExportado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.ColumnTipoExportacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnRuta = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.OTLabel = New System.Windows.Forms.Label()
            Me.BuscarFechaButton = New System.Windows.Forms.Button()
            Me.BuscarCarpetaButton = New System.Windows.Forms.Button()
            Me.lblCarpeta = New System.Windows.Forms.Label()
            Me.CarpetaSalidaTextBox = New System.Windows.Forms.TextBox()
            Me.AvanceProgressBar = New System.Windows.Forms.ProgressBar()
            Me.BlancoNegroRadioButton = New System.Windows.Forms.RadioButton()
            Me.GrisesRadioButton = New System.Windows.Forms.RadioButton()
            Me.ColorRadioButton = New System.Windows.Forms.RadioButton()
            Me.Label1 = New System.Windows.Forms.Label()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(357, 381)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 30)
            Me.AceptarButton.TabIndex = 3
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(453, 381)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 30)
            Me.CancelarButton.TabIndex = 4
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'FechaProcesoPicker
            '
            Me.FechaProcesoPicker.Location = New System.Drawing.Point(15, 34)
            Me.FechaProcesoPicker.Name = "FechaProcesoPicker"
            Me.FechaProcesoPicker.Size = New System.Drawing.Size(331, 20)
            Me.FechaProcesoPicker.TabIndex = 35
            '
            'LbFechaProceso
            '
            Me.LbFechaProceso.AutoSize = True
            Me.LbFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LbFechaProceso.Location = New System.Drawing.Point(9, 9)
            Me.LbFechaProceso.Name = "LbFechaProceso"
            Me.LbFechaProceso.Size = New System.Drawing.Size(110, 13)
            Me.LbFechaProceso.TabIndex = 36
            Me.LbFechaProceso.Text = "Fecha de Proceso"
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
            Me.OTDataGridView.Location = New System.Drawing.Point(12, 81)
            Me.OTDataGridView.MultiSelect = False
            Me.OTDataGridView.Name = "OTDataGridView"
            Me.OTDataGridView.ReadOnly = True
            Me.OTDataGridView.RowHeadersWidth = 10
            Me.OTDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.OTDataGridView.Size = New System.Drawing.Size(521, 173)
            Me.OTDataGridView.TabIndex = 38
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
            'OTLabel
            '
            Me.OTLabel.AutoSize = True
            Me.OTLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OTLabel.Location = New System.Drawing.Point(10, 63)
            Me.OTLabel.Name = "OTLabel"
            Me.OTLabel.Size = New System.Drawing.Size(32, 15)
            Me.OTLabel.TabIndex = 37
            Me.OTLabel.Text = "OTs"
            '
            'BuscarFechaButton
            '
            Me.BuscarFechaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarFechaButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnBuscar
            Me.BuscarFechaButton.Location = New System.Drawing.Point(496, 24)
            Me.BuscarFechaButton.Name = "BuscarFechaButton"
            Me.BuscarFechaButton.Size = New System.Drawing.Size(37, 30)
            Me.BuscarFechaButton.TabIndex = 39
            Me.BuscarFechaButton.UseVisualStyleBackColor = True
            '
            'BuscarCarpetaButton
            '
            Me.BuscarCarpetaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarCarpetaButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.folder_image
            Me.BuscarCarpetaButton.Location = New System.Drawing.Point(496, 316)
            Me.BuscarCarpetaButton.Name = "BuscarCarpetaButton"
            Me.BuscarCarpetaButton.Size = New System.Drawing.Size(37, 30)
            Me.BuscarCarpetaButton.TabIndex = 42
            Me.BuscarCarpetaButton.UseVisualStyleBackColor = True
            '
            'lblCarpeta
            '
            Me.lblCarpeta.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblCarpeta.AutoSize = True
            Me.lblCarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCarpeta.Location = New System.Drawing.Point(12, 306)
            Me.lblCarpeta.Name = "lblCarpeta"
            Me.lblCarpeta.Size = New System.Drawing.Size(122, 15)
            Me.lblCarpeta.TabIndex = 40
            Me.lblCarpeta.Text = "Carpeta de Salida"
            '
            'CarpetaSalidaTextBox
            '
            Me.CarpetaSalidaTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CarpetaSalidaTextBox.Location = New System.Drawing.Point(15, 326)
            Me.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox"
            Me.CarpetaSalidaTextBox.Size = New System.Drawing.Size(475, 20)
            Me.CarpetaSalidaTextBox.TabIndex = 41
            '
            'AvanceProgressBar
            '
            Me.AvanceProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AvanceProgressBar.Location = New System.Drawing.Point(13, 352)
            Me.AvanceProgressBar.Name = "AvanceProgressBar"
            Me.AvanceProgressBar.Size = New System.Drawing.Size(520, 23)
            Me.AvanceProgressBar.TabIndex = 43
            '
            'BlancoNegroRadioButton
            '
            Me.BlancoNegroRadioButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BlancoNegroRadioButton.AutoSize = True
            Me.BlancoNegroRadioButton.Checked = True
            Me.BlancoNegroRadioButton.Location = New System.Drawing.Point(123, 284)
            Me.BlancoNegroRadioButton.Name = "BlancoNegroRadioButton"
            Me.BlancoNegroRadioButton.Size = New System.Drawing.Size(98, 17)
            Me.BlancoNegroRadioButton.TabIndex = 44
            Me.BlancoNegroRadioButton.TabStop = True
            Me.BlancoNegroRadioButton.Text = "Blanco y Negro"
            Me.BlancoNegroRadioButton.UseVisualStyleBackColor = True
            '
            'GrisesRadioButton
            '
            Me.GrisesRadioButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GrisesRadioButton.AutoSize = True
            Me.GrisesRadioButton.Location = New System.Drawing.Point(241, 284)
            Me.GrisesRadioButton.Name = "GrisesRadioButton"
            Me.GrisesRadioButton.Size = New System.Drawing.Size(104, 17)
            Me.GrisesRadioButton.TabIndex = 45
            Me.GrisesRadioButton.Text = "Escala de Grises"
            Me.GrisesRadioButton.UseVisualStyleBackColor = True
            '
            'ColorRadioButton
            '
            Me.ColorRadioButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ColorRadioButton.AutoSize = True
            Me.ColorRadioButton.Location = New System.Drawing.Point(365, 284)
            Me.ColorRadioButton.Name = "ColorRadioButton"
            Me.ColorRadioButton.Size = New System.Drawing.Size(49, 17)
            Me.ColorRadioButton.TabIndex = 46
            Me.ColorRadioButton.Text = "Color"
            Me.ColorRadioButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(15, 259)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(109, 13)
            Me.Label1.TabIndex = 47
            Me.Label1.Text = "Formato de Salida"
            '
            'FormExportar
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(545, 423)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.ColorRadioButton)
            Me.Controls.Add(Me.GrisesRadioButton)
            Me.Controls.Add(Me.BlancoNegroRadioButton)
            Me.Controls.Add(Me.AvanceProgressBar)
            Me.Controls.Add(Me.BuscarCarpetaButton)
            Me.Controls.Add(Me.lblCarpeta)
            Me.Controls.Add(Me.CarpetaSalidaTextBox)
            Me.Controls.Add(Me.BuscarFechaButton)
            Me.Controls.Add(Me.OTDataGridView)
            Me.Controls.Add(Me.OTLabel)
            Me.Controls.Add(Me.LbFechaProceso)
            Me.Controls.Add(Me.FechaProcesoPicker)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormExportar"
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Exportar - Firmas"
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents FechaProcesoPicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents LbFechaProceso As System.Windows.Forms.Label
        Friend WithEvents OTDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents id_OT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_OT_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnCerrado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents ColumnExportado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents ColumnTipoExportacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnRuta As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents OTLabel As System.Windows.Forms.Label
        Friend WithEvents BuscarFechaButton As System.Windows.Forms.Button
        Friend WithEvents BuscarCarpetaButton As System.Windows.Forms.Button
        Friend WithEvents lblCarpeta As System.Windows.Forms.Label
        Friend WithEvents CarpetaSalidaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents AvanceProgressBar As System.Windows.Forms.ProgressBar
        Friend WithEvents BlancoNegroRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents GrisesRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents ColorRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace