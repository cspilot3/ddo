Namespace Forms.Facturacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormFacturacion
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormFacturacion))
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.BusquedaGroupBox = New System.Windows.Forms.GroupBox()
            Me.TransmitirButton = New System.Windows.Forms.Button()
            Me.GenerarButton = New System.Windows.Forms.Button()
            Me.FechaFinalLabel = New System.Windows.Forms.Label()
            Me.FechaFinalDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaInicialLabel = New System.Windows.Forms.Label()
            Me.FechaInicialDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.ReporteButton = New System.Windows.Forms.Button()
            Me.EditarButton = New System.Windows.Forms.Button()
            Me.FacturacionDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.Id_Facturacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Fecha_Facturacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombres = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Items = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.EditarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ReporteFacturaciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.BusquedaGroupBox.SuspendLayout()
            CType(Me.FacturacionDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ContextMenuStrip1.SuspendLayout()
            Me.SuspendLayout()
            '
            'BusquedaGroupBox
            '
            Me.BusquedaGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.BusquedaGroupBox.Controls.Add(Me.TransmitirButton)
            Me.BusquedaGroupBox.Controls.Add(Me.GenerarButton)
            Me.BusquedaGroupBox.Controls.Add(Me.FechaFinalLabel)
            Me.BusquedaGroupBox.Controls.Add(Me.FechaFinalDateTimePicker)
            Me.BusquedaGroupBox.Controls.Add(Me.FechaInicialLabel)
            Me.BusquedaGroupBox.Controls.Add(Me.FechaInicialDateTimePicker)
            Me.BusquedaGroupBox.Location = New System.Drawing.Point(4, 3)
            Me.BusquedaGroupBox.Name = "BusquedaGroupBox"
            Me.BusquedaGroupBox.Size = New System.Drawing.Size(617, 82)
            Me.BusquedaGroupBox.TabIndex = 0
            Me.BusquedaGroupBox.TabStop = False
            Me.BusquedaGroupBox.Text = "Rango Facturación"
            '
            'TransmitirButton
            '
            Me.TransmitirButton.Image = Global.Miharu.Desktop.My.Resources.Resources.Process_Billing
            Me.TransmitirButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.TransmitirButton.Location = New System.Drawing.Point(506, 12)
            Me.TransmitirButton.Name = "TransmitirButton"
            Me.TransmitirButton.Size = New System.Drawing.Size(68, 58)
            Me.TransmitirButton.TabIndex = 7
            Me.TransmitirButton.Text = "&Procesar"
            Me.TransmitirButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.ToolTip.SetToolTip(Me.TransmitirButton, "Transmisión de facturación.")
            Me.TransmitirButton.UseVisualStyleBackColor = True
            '
            'GenerarButton
            '
            Me.GenerarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.Billing
            Me.GenerarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.GenerarButton.Location = New System.Drawing.Point(432, 12)
            Me.GenerarButton.Name = "GenerarButton"
            Me.GenerarButton.Size = New System.Drawing.Size(68, 58)
            Me.GenerarButton.TabIndex = 6
            Me.GenerarButton.Text = "&Generar"
            Me.GenerarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.ToolTip.SetToolTip(Me.GenerarButton, "Generar facturación para la fecha seleccionada.")
            Me.GenerarButton.UseVisualStyleBackColor = True
            '
            'FechaFinalLabel
            '
            Me.FechaFinalLabel.AutoSize = True
            Me.FechaFinalLabel.Location = New System.Drawing.Point(239, 36)
            Me.FechaFinalLabel.Name = "FechaFinalLabel"
            Me.FechaFinalLabel.Size = New System.Drawing.Size(72, 13)
            Me.FechaFinalLabel.TabIndex = 5
            Me.FechaFinalLabel.Text = "Fecha Final:"
            '
            'FechaFinalDateTimePicker
            '
            Me.FechaFinalDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.FechaFinalDateTimePicker.Location = New System.Drawing.Point(317, 32)
            Me.FechaFinalDateTimePicker.Name = "FechaFinalDateTimePicker"
            Me.FechaFinalDateTimePicker.Size = New System.Drawing.Size(95, 21)
            Me.FechaFinalDateTimePicker.TabIndex = 4
            Me.ToolTip.SetToolTip(Me.FechaFinalDateTimePicker, "Fecha Final de facturación.")
            '
            'FechaInicialLabel
            '
            Me.FechaInicialLabel.AutoSize = True
            Me.FechaInicialLabel.Location = New System.Drawing.Point(43, 36)
            Me.FechaInicialLabel.Name = "FechaInicialLabel"
            Me.FechaInicialLabel.Size = New System.Drawing.Size(80, 13)
            Me.FechaInicialLabel.TabIndex = 3
            Me.FechaInicialLabel.Text = "Fecha Inicial:"
            '
            'FechaInicialDateTimePicker
            '
            Me.FechaInicialDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.FechaInicialDateTimePicker.Location = New System.Drawing.Point(129, 32)
            Me.FechaInicialDateTimePicker.Name = "FechaInicialDateTimePicker"
            Me.FechaInicialDateTimePicker.Size = New System.Drawing.Size(95, 21)
            Me.FechaInicialDateTimePicker.TabIndex = 2
            Me.ToolTip.SetToolTip(Me.FechaInicialDateTimePicker, "Fecha Inicial de facturación.")
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.BackColor = System.Drawing.Color.Transparent
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.FlatAppearance.BorderSize = 0
            Me.CerrarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CerrarButton.Image = CType(resources.GetObject("CerrarButton.Image"), System.Drawing.Image)
            Me.CerrarButton.Location = New System.Drawing.Point(583, 410)
            Me.CerrarButton.Margin = New System.Windows.Forms.Padding(0)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(43, 39)
            Me.CerrarButton.TabIndex = 2
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.ToolTip.SetToolTip(Me.CerrarButton, "Cerrar ventana")
            Me.CerrarButton.UseVisualStyleBackColor = False
            '
            'ReporteButton
            '
            Me.ReporteButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ReporteButton.BackColor = System.Drawing.Color.Transparent
            Me.ReporteButton.FlatAppearance.BorderSize = 0
            Me.ReporteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ReporteButton.Image = Global.Miharu.Desktop.My.Resources.Resources.ReportPrint
            Me.ReporteButton.Location = New System.Drawing.Point(490, 412)
            Me.ReporteButton.Name = "ReporteButton"
            Me.ReporteButton.Size = New System.Drawing.Size(42, 36)
            Me.ReporteButton.TabIndex = 4
            Me.ToolTip.SetToolTip(Me.ReporteButton, "Imprimir facturación")
            Me.ReporteButton.UseVisualStyleBackColor = False
            '
            'EditarButton
            '
            Me.EditarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EditarButton.BackColor = System.Drawing.Color.Transparent
            Me.EditarButton.FlatAppearance.BorderSize = 0
            Me.EditarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.EditarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.Edit
            Me.EditarButton.Location = New System.Drawing.Point(538, 413)
            Me.EditarButton.Name = "EditarButton"
            Me.EditarButton.Size = New System.Drawing.Size(42, 36)
            Me.EditarButton.TabIndex = 3
            Me.ToolTip.SetToolTip(Me.EditarButton, "Editar facturación")
            Me.EditarButton.UseVisualStyleBackColor = False
            '
            'FacturacionDataGridView
            '
            Me.FacturacionDataGridView.AllowUserToAddRows = False
            Me.FacturacionDataGridView.AllowUserToDeleteRows = False
            Me.FacturacionDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                        Or System.Windows.Forms.AnchorStyles.Left) _
                                                       Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FacturacionDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.FacturacionDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.FacturacionDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.FacturacionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.FacturacionDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id_Facturacion, Me.Nombre_Entidad, Me.Fecha_Facturacion, Me.Nombres, Me.Items})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.FacturacionDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.FacturacionDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.FacturacionDataGridView.Location = New System.Drawing.Point(4, 91)
            Me.FacturacionDataGridView.MultiSelect = False
            Me.FacturacionDataGridView.Name = "FacturacionDataGridView"
            Me.FacturacionDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.FacturacionDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FacturacionDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle4
            Me.FacturacionDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.FacturacionDataGridView.Size = New System.Drawing.Size(617, 319)
            Me.FacturacionDataGridView.TabIndex = 1
            '
            'Id_Facturacion
            '
            Me.Id_Facturacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Id_Facturacion.DataPropertyName = "Id_Facturacion"
            Me.Id_Facturacion.HeaderText = "Id"
            Me.Id_Facturacion.Name = "Id_Facturacion"
            Me.Id_Facturacion.ReadOnly = True
            Me.Id_Facturacion.Width = 60
            '
            'Nombre_Entidad
            '
            Me.Nombre_Entidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Nombre_Entidad.DataPropertyName = "Nombre_Entidad"
            Me.Nombre_Entidad.HeaderText = "Entidad"
            Me.Nombre_Entidad.Name = "Nombre_Entidad"
            Me.Nombre_Entidad.ReadOnly = True
            '
            'Fecha_Facturacion
            '
            Me.Fecha_Facturacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Fecha_Facturacion.DataPropertyName = "Fecha_Facturacion"
            Me.Fecha_Facturacion.HeaderText = "Fecha"
            Me.Fecha_Facturacion.Name = "Fecha_Facturacion"
            Me.Fecha_Facturacion.ReadOnly = True
            Me.Fecha_Facturacion.Width = 120
            '
            'Nombres
            '
            Me.Nombres.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Nombres.DataPropertyName = "Nombres"
            Me.Nombres.HeaderText = "Usuario"
            Me.Nombres.Name = "Nombres"
            Me.Nombres.ReadOnly = True
            '
            'Items
            '
            Me.Items.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Items.DataPropertyName = "Items"
            Me.Items.HeaderText = "Items"
            Me.Items.Name = "Items"
            Me.Items.ReadOnly = True
            Me.Items.Width = 60
            '
            'ContextMenuStrip1
            '
            Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditarToolStripMenuItem, Me.ReporteFacturaciónToolStripMenuItem})
            Me.ContextMenuStrip1.Name = "ContextMenuStrip"
            Me.ContextMenuStrip1.Size = New System.Drawing.Size(184, 48)
            '
            'EditarToolStripMenuItem
            '
            Me.EditarToolStripMenuItem.Image = Global.Miharu.Desktop.My.Resources.Resources.Edit
            Me.EditarToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.EditarToolStripMenuItem.Name = "EditarToolStripMenuItem"
            Me.EditarToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
            Me.EditarToolStripMenuItem.Text = "Editar Facturación"
            Me.EditarToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'ReporteFacturaciónToolStripMenuItem
            '
            Me.ReporteFacturaciónToolStripMenuItem.Image = Global.Miharu.Desktop.My.Resources.Resources.ReportPrint
            Me.ReporteFacturaciónToolStripMenuItem.Name = "ReporteFacturaciónToolStripMenuItem"
            Me.ReporteFacturaciónToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
            Me.ReporteFacturaciónToolStripMenuItem.Text = "Reporte Facturación"
            '
            'FormFacturacion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(626, 451)
            Me.ContextMenuStrip = Me.ContextMenuStrip1
            Me.Controls.Add(Me.ReporteButton)
            Me.Controls.Add(Me.EditarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.FacturacionDataGridView)
            Me.Controls.Add(Me.BusquedaGroupBox)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormFacturacion"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Facturación"
            Me.BusquedaGroupBox.ResumeLayout(False)
            Me.BusquedaGroupBox.PerformLayout()
            CType(Me.FacturacionDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ContextMenuStrip1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents BusquedaGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents FechaInicialLabel As System.Windows.Forms.Label
        Friend WithEvents FechaInicialDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaFinalLabel As System.Windows.Forms.Label
        Friend WithEvents FechaFinalDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents GenerarButton As System.Windows.Forms.Button
        Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents FacturacionDataGridView As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Id_Facturacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Fecha_Facturacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombres As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Items As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EditarButton As System.Windows.Forms.Button

        Friend WithEvents EditarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents ReporteButton As System.Windows.Forms.Button
        Friend WithEvents ReporteFacturaciónToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TransmitirButton As System.Windows.Forms.Button
    End Class
End Namespace