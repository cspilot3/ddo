<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSolicitudesPorCliente
    Inherits Miharu.Desktop.Library.FormBase

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSolicitudesPorCliente))
        Me.gbEntidadesSolicitudes = New System.Windows.Forms.GroupBox()
        Me.dgvEntidadSolicitudes = New System.Windows.Forms.DataGridView()
        Me.IdEntidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CantidadSolicitudes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnMostrarFormDescuelgue = New System.Windows.Forms.Button()
        Me.btnCerrarVentana = New System.Windows.Forms.Button()
        Me.lblSolicitudes = New System.Windows.Forms.Label()
        Me.lblEntidad = New System.Windows.Forms.Label()
        Me.gbEntidadesSolicitudes.SuspendLayout()
        CType(Me.dgvEntidadSolicitudes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbEntidadesSolicitudes
        '
        Me.gbEntidadesSolicitudes.Controls.Add(Me.dgvEntidadSolicitudes)
        Me.gbEntidadesSolicitudes.Controls.Add(Me.btnMostrarFormDescuelgue)
        Me.gbEntidadesSolicitudes.Controls.Add(Me.btnCerrarVentana)
        Me.gbEntidadesSolicitudes.Controls.Add(Me.lblSolicitudes)
        Me.gbEntidadesSolicitudes.Controls.Add(Me.lblEntidad)
        Me.gbEntidadesSolicitudes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbEntidadesSolicitudes.Location = New System.Drawing.Point(14, 12)
        Me.gbEntidadesSolicitudes.Name = "gbEntidadesSolicitudes"
        Me.gbEntidadesSolicitudes.Size = New System.Drawing.Size(451, 356)
        Me.gbEntidadesSolicitudes.TabIndex = 0
        Me.gbEntidadesSolicitudes.TabStop = False
        Me.gbEntidadesSolicitudes.Text = "Entidades con Solicitudes"
        '
        'dgvEntidadSolicitudes
        '
        Me.dgvEntidadSolicitudes.AllowUserToAddRows = False
        Me.dgvEntidadSolicitudes.AllowUserToDeleteRows = False
        Me.dgvEntidadSolicitudes.AllowUserToResizeColumns = False
        Me.dgvEntidadSolicitudes.AllowUserToResizeRows = False
        Me.dgvEntidadSolicitudes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvEntidadSolicitudes.BackgroundColor = System.Drawing.Color.White
        Me.dgvEntidadSolicitudes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.dgvEntidadSolicitudes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvEntidadSolicitudes.ColumnHeadersVisible = False
        Me.dgvEntidadSolicitudes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdEntidad, Me.Nombre_Entidad, Me.CantidadSolicitudes})
        Me.dgvEntidadSolicitudes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dgvEntidadSolicitudes.Location = New System.Drawing.Point(26, 69)
        Me.dgvEntidadSolicitudes.MultiSelect = False
        Me.dgvEntidadSolicitudes.Name = "dgvEntidadSolicitudes"
        Me.dgvEntidadSolicitudes.ReadOnly = True
        Me.dgvEntidadSolicitudes.RowHeadersVisible = False
        Me.dgvEntidadSolicitudes.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvEntidadSolicitudes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvEntidadSolicitudes.Size = New System.Drawing.Size(396, 204)
        Me.dgvEntidadSolicitudes.TabIndex = 10
        '
        'IdEntidad
        '
        Me.IdEntidad.DataPropertyName = "id_Entidad"
        Me.IdEntidad.HeaderText = ""
        Me.IdEntidad.Name = "IdEntidad"
        Me.IdEntidad.ReadOnly = True
        Me.IdEntidad.Visible = False
        '
        'Nombre_Entidad
        '
        Me.Nombre_Entidad.DataPropertyName = "Nombre_Entidad"
        Me.Nombre_Entidad.HeaderText = ""
        Me.Nombre_Entidad.Name = "Nombre_Entidad"
        Me.Nombre_Entidad.ReadOnly = True
        '
        'CantidadSolicitudes
        '
        Me.CantidadSolicitudes.DataPropertyName = "CantidadSolicitudes"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.CantidadSolicitudes.DefaultCellStyle = DataGridViewCellStyle1
        Me.CantidadSolicitudes.HeaderText = ""
        Me.CantidadSolicitudes.Name = "CantidadSolicitudes"
        Me.CantidadSolicitudes.ReadOnly = True
        '
        'btnMostrarFormDescuelgue
        '
        Me.btnMostrarFormDescuelgue.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnMostrarFormDescuelgue.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnMostrarFormDescuelgue.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Arqueo
        Me.btnMostrarFormDescuelgue.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnMostrarFormDescuelgue.Location = New System.Drawing.Point(80, 298)
        Me.btnMostrarFormDescuelgue.Name = "btnMostrarFormDescuelgue"
        Me.btnMostrarFormDescuelgue.Size = New System.Drawing.Size(145, 41)
        Me.btnMostrarFormDescuelgue.TabIndex = 9
        Me.btnMostrarFormDescuelgue.Text = "Descolgar"
        Me.btnMostrarFormDescuelgue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMostrarFormDescuelgue.UseVisualStyleBackColor = True
        '
        'btnCerrarVentana
        '
        Me.btnCerrarVentana.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrarVentana.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnCerrarVentana.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
        Me.btnCerrarVentana.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnCerrarVentana.Location = New System.Drawing.Point(250, 298)
        Me.btnCerrarVentana.Name = "btnCerrarVentana"
        Me.btnCerrarVentana.Size = New System.Drawing.Size(124, 41)
        Me.btnCerrarVentana.TabIndex = 8
        Me.btnCerrarVentana.Text = "&Cerrar"
        Me.btnCerrarVentana.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCerrarVentana.UseVisualStyleBackColor = True
        '
        'lblSolicitudes
        '
        Me.lblSolicitudes.AutoSize = True
        Me.lblSolicitudes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSolicitudes.Location = New System.Drawing.Point(273, 34)
        Me.lblSolicitudes.Name = "lblSolicitudes"
        Me.lblSolicitudes.Size = New System.Drawing.Size(78, 15)
        Me.lblSolicitudes.TabIndex = 3
        Me.lblSolicitudes.Text = "Solicitudes"
        '
        'lblEntidad
        '
        Me.lblEntidad.AutoSize = True
        Me.lblEntidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntidad.Location = New System.Drawing.Point(77, 35)
        Me.lblEntidad.Name = "lblEntidad"
        Me.lblEntidad.Size = New System.Drawing.Size(56, 15)
        Me.lblEntidad.TabIndex = 2
        Me.lblEntidad.Text = "Entidad"
        '
        'FormSolicitudesPorCliente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(479, 380)
        Me.Controls.Add(Me.gbEntidadesSolicitudes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormSolicitudesPorCliente"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Descuelgue Documentos"
        Me.gbEntidadesSolicitudes.ResumeLayout(False)
        Me.gbEntidadesSolicitudes.PerformLayout()
        CType(Me.dgvEntidadSolicitudes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbEntidadesSolicitudes As System.Windows.Forms.GroupBox
    Friend WithEvents lblSolicitudes As System.Windows.Forms.Label
    Friend WithEvents lblEntidad As System.Windows.Forms.Label
    Friend WithEvents btnMostrarFormDescuelgue As System.Windows.Forms.Button
    Friend WithEvents btnCerrarVentana As System.Windows.Forms.Button
    Friend WithEvents dgvEntidadSolicitudes As System.Windows.Forms.DataGridView
    Friend WithEvents IdEntidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nombre_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CantidadSolicitudes As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
