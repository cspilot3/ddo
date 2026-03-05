Namespace DesktopReportDataGridView
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class DesktopReportDataGridViewControl
        Inherits System.Windows.Forms.UserControl

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
            Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
            Me.EtiquetaToolStripLabel = New System.Windows.Forms.ToolStripLabel()
            Me.ExportarToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.EnvioCorreoToolStripButton = New System.Windows.Forms.ToolStripButton()
            Me.ResultadoReporteDesktopDataGridViewControl = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.ToolStrip1.SuspendLayout()
            CType(Me.ResultadoReporteDesktopDataGridViewControl, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ToolStrip1
            '
            Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EtiquetaToolStripLabel, Me.ExportarToolStripButton, Me.EnvioCorreoToolStripButton})
            Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
            Me.ToolStrip1.Name = "ToolStrip1"
            Me.ToolStrip1.Size = New System.Drawing.Size(513, 25)
            Me.ToolStrip1.TabIndex = 0
            Me.ToolStrip1.Text = "ToolStrip1"
            '
            'EtiquetaToolStripLabel
            '
            Me.EtiquetaToolStripLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.EtiquetaToolStripLabel.Name = "EtiquetaToolStripLabel"
            Me.EtiquetaToolStripLabel.Size = New System.Drawing.Size(132, 22)
            Me.EtiquetaToolStripLabel.Text = "EtiquetaToolStripLabel"
            '
            'ExportarToolStripButton
            '
            Me.ExportarToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ExportarToolStripButton.Image = Global.Miharu.Desktop.Controls.My.Resources.Resources.Aceptar
            Me.ExportarToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.ExportarToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ExportarToolStripButton.Name = "ExportarToolStripButton"
            Me.ExportarToolStripButton.Size = New System.Drawing.Size(71, 22)
            Me.ExportarToolStripButton.Text = "Exportar"
            '
            'EnvioCorreoToolStripButton
            '
            Me.EnvioCorreoToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.EnvioCorreoToolStripButton.Image = Global.Miharu.Desktop.Controls.My.Resources.Resources.historiaE
            Me.EnvioCorreoToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.EnvioCorreoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.EnvioCorreoToolStripButton.Name = "EnvioCorreoToolStripButton"
            Me.EnvioCorreoToolStripButton.Size = New System.Drawing.Size(106, 28)
            Me.EnvioCorreoToolStripButton.Text = "Enviar Correo"
            Me.EnvioCorreoToolStripButton.Visible = False
            '
            'ResultadoReporteDesktopDataGridViewControl
            '
            Me.ResultadoReporteDesktopDataGridViewControl.AllowUserToAddRows = False
            Me.ResultadoReporteDesktopDataGridViewControl.AllowUserToDeleteRows = False
            Me.ResultadoReporteDesktopDataGridViewControl.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.ResultadoReporteDesktopDataGridViewControl.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gainsboro
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ResultadoReporteDesktopDataGridViewControl.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.ResultadoReporteDesktopDataGridViewControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.ResultadoReporteDesktopDataGridViewControl.DefaultCellStyle = DataGridViewCellStyle2
            Me.ResultadoReporteDesktopDataGridViewControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ResultadoReporteDesktopDataGridViewControl.GridColor = System.Drawing.Color.Gainsboro
            Me.ResultadoReporteDesktopDataGridViewControl.Location = New System.Drawing.Point(0, 25)
            Me.ResultadoReporteDesktopDataGridViewControl.MultiSelect = False
            Me.ResultadoReporteDesktopDataGridViewControl.Name = "ResultadoReporteDesktopDataGridViewControl"
            Me.ResultadoReporteDesktopDataGridViewControl.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gainsboro
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ResultadoReporteDesktopDataGridViewControl.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.ResultadoReporteDesktopDataGridViewControl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ResultadoReporteDesktopDataGridViewControl.Size = New System.Drawing.Size(513, 258)
            Me.ResultadoReporteDesktopDataGridViewControl.TabIndex = 1
            '
            'DesktopReportDataGridViewControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.ResultadoReporteDesktopDataGridViewControl)
            Me.Controls.Add(Me.ToolStrip1)
            Me.Name = "DesktopReportDataGridViewControl"
            Me.Size = New System.Drawing.Size(513, 283)
            Me.ToolStrip1.ResumeLayout(False)
            Me.ToolStrip1.PerformLayout()
            CType(Me.ResultadoReporteDesktopDataGridViewControl, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
        Friend WithEvents EtiquetaToolStripLabel As System.Windows.Forms.ToolStripLabel
        Friend WithEvents ResultadoReporteDesktopDataGridViewControl As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents ExportarToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents EnvioCorreoToolStripButton As System.Windows.Forms.ToolStripButton

    End Class
End Namespace