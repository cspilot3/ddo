Imports Miharu.Desktop.Controls.DesktopDataGridView

Namespace Forms.CentroDistribucion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeleccionarRemision
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.gbxRemisiones = New System.Windows.Forms.GroupBox()
            Me.btnCerrar = New System.Windows.Forms.Button()
            Me.btnAceptar = New System.Windows.Forms.Button()
            Me.btnCerrarRemision = New System.Windows.Forms.Button()
            Me.gridRemisiones = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FechaCreacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreUsuario = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdCajaRemision = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.btnBuscarRemision = New System.Windows.Forms.Button()
            Me.txtRemision = New System.Windows.Forms.TextBox()
            Me.gbxRemisiones.SuspendLayout()
            CType(Me.gridRemisiones, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'gbxRemisiones
            '
            Me.gbxRemisiones.Controls.Add(Me.btnCerrar)
            Me.gbxRemisiones.Controls.Add(Me.btnAceptar)
            Me.gbxRemisiones.Controls.Add(Me.btnCerrarRemision)
            Me.gbxRemisiones.Controls.Add(Me.gridRemisiones)
            Me.gbxRemisiones.Controls.Add(Me.btnBuscarRemision)
            Me.gbxRemisiones.Controls.Add(Me.txtRemision)
            Me.gbxRemisiones.Location = New System.Drawing.Point(12, 12)
            Me.gbxRemisiones.Name = "gbxRemisiones"
            Me.gbxRemisiones.Size = New System.Drawing.Size(331, 269)
            Me.gbxRemisiones.TabIndex = 0
            Me.gbxRemisiones.TabStop = False
            Me.gbxRemisiones.Text = "Remisiones"
            '
            'btnCerrar
            '
            Me.btnCerrar.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCerrar.Location = New System.Drawing.Point(220, 226)
            Me.btnCerrar.Name = "btnCerrar"
            Me.btnCerrar.Size = New System.Drawing.Size(75, 23)
            Me.btnCerrar.TabIndex = 6
            Me.btnCerrar.Text = "&Cerrar"
            Me.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnCerrar.UseVisualStyleBackColor = True
            '
            'btnAceptar
            '
            Me.btnAceptar.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnAceptar.Location = New System.Drawing.Point(139, 226)
            Me.btnAceptar.Name = "btnAceptar"
            Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
            Me.btnAceptar.TabIndex = 5
            Me.btnAceptar.Text = "&Aceptar"
            Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnAceptar.UseVisualStyleBackColor = True
            '
            'btnCerrarRemision
            '
            Me.btnCerrarRemision.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir1
            Me.btnCerrarRemision.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCerrarRemision.Location = New System.Drawing.Point(6, 226)
            Me.btnCerrarRemision.Name = "btnCerrarRemision"
            Me.btnCerrarRemision.Size = New System.Drawing.Size(127, 23)
            Me.btnCerrarRemision.TabIndex = 4
            Me.btnCerrarRemision.Text = "&Cerrar Remisión"
            Me.btnCerrarRemision.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnCerrarRemision.UseVisualStyleBackColor = True
            '
            'gridRemisiones
            '
            Me.gridRemisiones.AllowUserToAddRows = False
            Me.gridRemisiones.AllowUserToDeleteRows = False
            Me.gridRemisiones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.gridRemisiones.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.gridRemisiones.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.gridRemisiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.gridRemisiones.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.FechaCreacion, Me.NombreUsuario, Me.IdCajaRemision})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.gridRemisiones.DefaultCellStyle = DataGridViewCellStyle2
            Me.gridRemisiones.GridColor = System.Drawing.SystemColors.Control
            Me.gridRemisiones.Location = New System.Drawing.Point(6, 47)
            Me.gridRemisiones.MultiSelect = False
            Me.gridRemisiones.Name = "gridRemisiones"
            Me.gridRemisiones.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.gridRemisiones.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.gridRemisiones.RowHeadersVisible = False
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.gridRemisiones.RowsDefaultCellStyle = DataGridViewCellStyle4
            Me.gridRemisiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.gridRemisiones.Size = New System.Drawing.Size(311, 173)
            Me.gridRemisiones.TabIndex = 3
            '
            'Id
            '
            Me.Id.DataPropertyName = "Nro_Remision"
            Me.Id.HeaderText = "Nro. Remisión"
            Me.Id.Name = "Id"
            Me.Id.ReadOnly = True
            '
            'FechaCreacion
            '
            Me.FechaCreacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.FechaCreacion.DataPropertyName = "Total_Carpetas_Pendientes"
            Me.FechaCreacion.HeaderText = "Carp. Pend"
            Me.FechaCreacion.Name = "FechaCreacion"
            Me.FechaCreacion.ReadOnly = True
            '
            'NombreUsuario
            '
            Me.NombreUsuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.NombreUsuario.DataPropertyName = "Total_Documentos_Pendientes"
            Me.NombreUsuario.HeaderText = "Doc. Pend"
            Me.NombreUsuario.Name = "NombreUsuario"
            Me.NombreUsuario.ReadOnly = True
            '
            'IdCajaRemision
            '
            Me.IdCajaRemision.DataPropertyName = "Id_Remision_Caja"
            Me.IdCajaRemision.HeaderText = "Id Remision Caja "
            Me.IdCajaRemision.Name = "IdCajaRemision"
            Me.IdCajaRemision.ReadOnly = True
            Me.IdCajaRemision.Visible = False
            Me.IdCajaRemision.Width = 119
            '
            'btnBuscarRemision
            '
            Me.btnBuscarRemision.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar
            Me.btnBuscarRemision.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBuscarRemision.Location = New System.Drawing.Point(232, 18)
            Me.btnBuscarRemision.Name = "btnBuscarRemision"
            Me.btnBuscarRemision.Size = New System.Drawing.Size(75, 23)
            Me.btnBuscarRemision.TabIndex = 1
            Me.btnBuscarRemision.Text = "&Buscar"
            Me.btnBuscarRemision.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnBuscarRemision.UseVisualStyleBackColor = True
            '
            'txtRemision
            '
            Me.txtRemision.Location = New System.Drawing.Point(6, 20)
            Me.txtRemision.Name = "txtRemision"
            Me.txtRemision.Size = New System.Drawing.Size(220, 21)
            Me.txtRemision.TabIndex = 0
            '
            'FormSeleccionarRemision
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(354, 290)
            Me.Controls.Add(Me.gbxRemisiones)
            Me.Name = "FormSeleccionarRemision"
            Me.Text = "Seleccionar Remisión"
            Me.gbxRemisiones.ResumeLayout(False)
            Me.gbxRemisiones.PerformLayout()
            CType(Me.gridRemisiones, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents gbxRemisiones As System.Windows.Forms.GroupBox
        Friend WithEvents btnBuscarRemision As System.Windows.Forms.Button
        Friend WithEvents txtRemision As System.Windows.Forms.TextBox
        Friend WithEvents btnCerrar As System.Windows.Forms.Button
        Friend WithEvents btnAceptar As System.Windows.Forms.Button
        Friend WithEvents btnCerrarRemision As System.Windows.Forms.Button
        Friend WithEvents gridRemisiones As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FechaCreacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreUsuario As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdCajaRemision As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class

End Namespace
