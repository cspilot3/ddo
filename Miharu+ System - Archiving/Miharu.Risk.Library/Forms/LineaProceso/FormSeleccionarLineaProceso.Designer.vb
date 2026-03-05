Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.LineaProceso
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeleccionarLineaProceso
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
            Dim Rango1 As Rango = New Rango()
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSeleccionarLineaProceso))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.OtBuscarDesktopTextBox = New DesktopTextBoxControl()
            Me.SeleccionarButton = New System.Windows.Forms.Button()
            Me.LineasDataGridView = New DesktopDataGridViewControl()
            Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FechaCreacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreUsuario = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.GroupBox1.SuspendLayout()
            CType(Me.LineasDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.BuscarButton)
            Me.GroupBox1.Controls.Add(Me.OtBuscarDesktopTextBox)
            Me.GroupBox1.Controls.Add(Me.SeleccionarButton)
            Me.GroupBox1.Controls.Add(Me.LineasDataGridView)
            Me.GroupBox1.Location = New System.Drawing.Point(2, 2)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(410, 366)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Lineas de Proceso"
            '
            'BuscarButton
            '
            Me.BuscarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(321, 29)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(80, 24)
            Me.BuscarButton.TabIndex = 1
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'OtBuscarDesktopTextBox
            '
            Me.OtBuscarDesktopTextBox.DisabledEnter = False
            Me.OtBuscarDesktopTextBox.DisabledTab = False
            Me.OtBuscarDesktopTextBox.EnabledShortCuts = False
            Me.OtBuscarDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.OtBuscarDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.OtBuscarDesktopTextBox.Location = New System.Drawing.Point(10, 29)
            Me.OtBuscarDesktopTextBox.Name = "OtBuscarDesktopTextBox"
            Rango1.MaxValue = CType(2147483647, Long)
            Rango1.MinValue = CType(0, Long)
            Me.OtBuscarDesktopTextBox.Rango = Rango1
            Me.OtBuscarDesktopTextBox.ShortcutsEnabled = False
            Me.OtBuscarDesktopTextBox.Size = New System.Drawing.Size(305, 21)
            Me.OtBuscarDesktopTextBox.TabIndex = 0
            Me.OtBuscarDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            '
            'SeleccionarButton
            '
            Me.SeleccionarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SeleccionarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.SeleccionarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.SeleccionarButton.Location = New System.Drawing.Point(304, 334)
            Me.SeleccionarButton.Name = "SeleccionarButton"
            Me.SeleccionarButton.Size = New System.Drawing.Size(97, 26)
            Me.SeleccionarButton.TabIndex = 3
            Me.SeleccionarButton.Text = "&Seleccionar"
            Me.SeleccionarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.SeleccionarButton.UseVisualStyleBackColor = True
            '
            'LineasDataGridView
            '
            Me.LineasDataGridView.AllowUserToAddRows = False
            Me.LineasDataGridView.AllowUserToDeleteRows = False
            Me.LineasDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.LineasDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.LineasDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.LineasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.LineasDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.FechaCreacion, Me.NombreUsuario})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.LineasDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.LineasDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.LineasDataGridView.Location = New System.Drawing.Point(10, 59)
            Me.LineasDataGridView.MultiSelect = False
            Me.LineasDataGridView.Name = "LineasDataGridView"
            Me.LineasDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.LineasDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.LineasDataGridView.RowHeadersVisible = False
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LineasDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle4
            Me.LineasDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.LineasDataGridView.Size = New System.Drawing.Size(391, 260)
            Me.LineasDataGridView.TabIndex = 2
            '
            'Id
            '
            Me.Id.DataPropertyName = "id_Linea_Proceso"
            Me.Id.HeaderText = "Id"
            Me.Id.Name = "Id"
            Me.Id.ReadOnly = True
            Me.Id.Width = 44
            '
            'FechaCreacion
            '
            Me.FechaCreacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.FechaCreacion.DataPropertyName = "Fecha_Creacion"
            Me.FechaCreacion.HeaderText = "F. Creación"
            Me.FechaCreacion.Name = "FechaCreacion"
            Me.FechaCreacion.ReadOnly = True
            '
            'NombreUsuario
            '
            Me.NombreUsuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.NombreUsuario.DataPropertyName = "Nombres"
            Me.NombreUsuario.HeaderText = "Usuario"
            Me.NombreUsuario.Name = "NombreUsuario"
            Me.NombreUsuario.ReadOnly = True
            '
            'FormSeleccionarLineaProceso
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(419, 376)
            Me.Controls.Add(Me.GroupBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.KeyPreview = True
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormSeleccionarLineaProceso"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Línea de Proceso"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.LineasDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents LineasDataGridView As DesktopDataGridViewControl
        Friend WithEvents SeleccionarButton As System.Windows.Forms.Button
        Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FechaCreacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreUsuario As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents OtBuscarDesktopTextBox As DesktopTextBoxControl
    End Class
End Namespace