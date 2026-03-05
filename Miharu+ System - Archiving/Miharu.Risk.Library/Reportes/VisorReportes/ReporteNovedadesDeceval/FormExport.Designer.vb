Namespace Reportes.VisorReportes.ReporteNovedadesDeceval
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
            Me.FechaFinDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.BuscarFechaButton = New System.Windows.Forms.Button()
            Me.LineasProcesoDataGridView = New System.Windows.Forms.DataGridView()
            Me.FechaInicioDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.LineasProcesoLabel = New System.Windows.Forms.Label()
            Me.FechaProcesoLabel = New System.Windows.Forms.Label()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.btnGenerarReporteNovedades = New System.Windows.Forms.Button()
            Me.lblFechaInicio = New System.Windows.Forms.Label()
            Me.lblFechaFin = New System.Windows.Forms.Label()
            Me.ColumnSeleccionar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.MainGroupBox.SuspendLayout()
            CType(Me.LineasProcesoDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'MainGroupBox
            '
            Me.MainGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MainGroupBox.Controls.Add(Me.lblFechaFin)
            Me.MainGroupBox.Controls.Add(Me.CancelarButton)
            Me.MainGroupBox.Controls.Add(Me.lblFechaInicio)
            Me.MainGroupBox.Controls.Add(Me.btnGenerarReporteNovedades)
            Me.MainGroupBox.Controls.Add(Me.FechaFinDateTimePicker)
            Me.MainGroupBox.Controls.Add(Me.BuscarFechaButton)
            Me.MainGroupBox.Controls.Add(Me.LineasProcesoDataGridView)
            Me.MainGroupBox.Controls.Add(Me.FechaInicioDateTimePicker)
            Me.MainGroupBox.Controls.Add(Me.LineasProcesoLabel)
            Me.MainGroupBox.Controls.Add(Me.FechaProcesoLabel)
            Me.MainGroupBox.Location = New System.Drawing.Point(9, 6)
            Me.MainGroupBox.Name = "MainGroupBox"
            Me.MainGroupBox.Size = New System.Drawing.Size(559, 355)
            Me.MainGroupBox.TabIndex = 0
            Me.MainGroupBox.TabStop = False
            Me.MainGroupBox.Text = "Parametros de exportación"
            '
            'FechaFinDateTimePicker
            '
            Me.FechaFinDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaFinDateTimePicker.Location = New System.Drawing.Point(113, 83)
            Me.FechaFinDateTimePicker.Name = "FechaFinDateTimePicker"
            Me.FechaFinDateTimePicker.Size = New System.Drawing.Size(361, 26)
            Me.FechaFinDateTimePicker.TabIndex = 5
            '
            'BuscarFechaButton
            '
            Me.BuscarFechaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarFechaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar
            Me.BuscarFechaButton.Location = New System.Drawing.Point(491, 79)
            Me.BuscarFechaButton.Name = "BuscarFechaButton"
            Me.BuscarFechaButton.Size = New System.Drawing.Size(37, 30)
            Me.BuscarFechaButton.TabIndex = 2
            Me.BuscarFechaButton.UseVisualStyleBackColor = True
            '
            'LineasProcesoDataGridView
            '
            Me.LineasProcesoDataGridView.AllowUserToAddRows = False
            Me.LineasProcesoDataGridView.AllowUserToDeleteRows = False
            Me.LineasProcesoDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LineasProcesoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.LineasProcesoDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnSeleccionar})
            Me.LineasProcesoDataGridView.Location = New System.Drawing.Point(9, 159)
            Me.LineasProcesoDataGridView.MultiSelect = False
            Me.LineasProcesoDataGridView.Name = "LineasProcesoDataGridView"
            Me.LineasProcesoDataGridView.RowHeadersWidth = 10
            Me.LineasProcesoDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.LineasProcesoDataGridView.Size = New System.Drawing.Size(535, 116)
            Me.LineasProcesoDataGridView.TabIndex = 4
            '
            'FechaInicioDateTimePicker
            '
            Me.FechaInicioDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaInicioDateTimePicker.Location = New System.Drawing.Point(113, 51)
            Me.FechaInicioDateTimePicker.Name = "FechaInicioDateTimePicker"
            Me.FechaInicioDateTimePicker.Size = New System.Drawing.Size(361, 26)
            Me.FechaInicioDateTimePicker.TabIndex = 1
            '
            'LineasProcesoLabel
            '
            Me.LineasProcesoLabel.AutoSize = True
            Me.LineasProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LineasProcesoLabel.Location = New System.Drawing.Point(7, 128)
            Me.LineasProcesoLabel.Name = "LineasProcesoLabel"
            Me.LineasProcesoLabel.Size = New System.Drawing.Size(126, 15)
            Me.LineasProcesoLabel.TabIndex = 3
            Me.LineasProcesoLabel.Text = "Líneas de Proceso"
            '
            'FechaProcesoLabel
            '
            Me.FechaProcesoLabel.AutoSize = True
            Me.FechaProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoLabel.Location = New System.Drawing.Point(6, 32)
            Me.FechaProcesoLabel.Name = "FechaProcesoLabel"
            Me.FechaProcesoLabel.Size = New System.Drawing.Size(121, 15)
            Me.FechaProcesoLabel.TabIndex = 0
            Me.FechaProcesoLabel.Text = "Fecha de proceso"
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(455, 299)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(89, 37)
            Me.CancelarButton.TabIndex = 2
            Me.CancelarButton.Text = "Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'btnGenerarReporteNovedades
            '
            Me.btnGenerarReporteNovedades.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnGenerarReporteNovedades.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnGenerarReporteNovedades.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSigiente
            Me.btnGenerarReporteNovedades.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnGenerarReporteNovedades.Location = New System.Drawing.Point(294, 299)
            Me.btnGenerarReporteNovedades.Name = "btnGenerarReporteNovedades"
            Me.btnGenerarReporteNovedades.Size = New System.Drawing.Size(149, 37)
            Me.btnGenerarReporteNovedades.TabIndex = 1
            Me.btnGenerarReporteNovedades.Text = "Reporte Novedades"
            Me.btnGenerarReporteNovedades.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnGenerarReporteNovedades.UseVisualStyleBackColor = True
            '
            'lblFechaInicio
            '
            Me.lblFechaInicio.AutoSize = True
            Me.lblFechaInicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaInicio.Location = New System.Drawing.Point(20, 64)
            Me.lblFechaInicio.Name = "lblFechaInicio"
            Me.lblFechaInicio.Size = New System.Drawing.Size(77, 13)
            Me.lblFechaInicio.TabIndex = 6
            Me.lblFechaInicio.Text = "Fecha Inicio"
            '
            'lblFechaFin
            '
            Me.lblFechaFin.AutoSize = True
            Me.lblFechaFin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaFin.Location = New System.Drawing.Point(20, 93)
            Me.lblFechaFin.Name = "lblFechaFin"
            Me.lblFechaFin.Size = New System.Drawing.Size(63, 13)
            Me.lblFechaFin.TabIndex = 25
            Me.lblFechaFin.Text = "Fecha Fin"
            '
            'ColumnSeleccionar
            '
            Me.ColumnSeleccionar.HeaderText = ""
            Me.ColumnSeleccionar.Name = "ColumnSeleccionar"
            Me.ColumnSeleccionar.Width = 50
            '
            'FormExport
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(579, 384)
            Me.Controls.Add(Me.MainGroupBox)
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
            CType(Me.LineasProcesoDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents btnGenerarReporteNovedades As System.Windows.Forms.Button
        Friend WithEvents FechaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents LineasProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents FechaInicioDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents LineasProcesoDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents BuscarFechaButton As System.Windows.Forms.Button
        Friend WithEvents FechaFinDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblFechaFin As System.Windows.Forms.Label
        Friend WithEvents lblFechaInicio As System.Windows.Forms.Label
        Friend WithEvents ColumnSeleccionar As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace