Namespace Procesos.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormFechaProceso
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
            Me.FechaProcesoDataGridView = New System.Windows.Forms.DataGridView()
            Me.fk_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Fecha_Proceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Fecha_Proceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Usuario_Apertura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Usuario_Apertura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fecha_Apertura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Cerrado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.fk_Usuario_Cierre = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Usuario_Cierre = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Fecha_Cierre = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.FechaFinalDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.FechaInicialDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.CrearFechaProcesoButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.CerrarFechaButton = New System.Windows.Forms.Button()
            Me.ProcessDataSet1 = New DBImaging.ProcessDataSet()
            CType(Me.FechaProcesoDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ProcessDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'FechaProcesoDataGridView
            '
            Me.FechaProcesoDataGridView.AllowUserToAddRows = False
            Me.FechaProcesoDataGridView.AllowUserToDeleteRows = False
            Me.FechaProcesoDataGridView.AllowUserToOrderColumns = True
            Me.FechaProcesoDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FechaProcesoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.FechaProcesoDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fk_Entidad, Me.fk_Proyecto, Me.id_Fecha_Proceso, Me.Fecha_Proceso, Me.fk_Usuario_Apertura, Me.Usuario_Apertura, Me.fecha_Apertura, Me.Cerrado, Me.fk_Usuario_Cierre, Me.Usuario_Cierre, Me.Fecha_Cierre})
            Me.FechaProcesoDataGridView.Location = New System.Drawing.Point(12, 61)
            Me.FechaProcesoDataGridView.MultiSelect = False
            Me.FechaProcesoDataGridView.Name = "FechaProcesoDataGridView"
            Me.FechaProcesoDataGridView.ReadOnly = True
            Me.FechaProcesoDataGridView.Size = New System.Drawing.Size(644, 283)
            Me.FechaProcesoDataGridView.TabIndex = 4
            '
            'fk_Entidad
            '
            Me.fk_Entidad.DataPropertyName = "fk_Entidad"
            Me.fk_Entidad.HeaderText = "Entidad"
            Me.fk_Entidad.Name = "fk_Entidad"
            Me.fk_Entidad.ReadOnly = True
            Me.fk_Entidad.Visible = False
            '
            'fk_Proyecto
            '
            Me.fk_Proyecto.DataPropertyName = "fk_Proyecto"
            Me.fk_Proyecto.HeaderText = "Proyecto"
            Me.fk_Proyecto.Name = "fk_Proyecto"
            Me.fk_Proyecto.ReadOnly = True
            Me.fk_Proyecto.Visible = False
            '
            'id_Fecha_Proceso
            '
            Me.id_Fecha_Proceso.DataPropertyName = "id_Fecha_Proceso"
            Me.id_Fecha_Proceso.HeaderText = "Id Fecha"
            Me.id_Fecha_Proceso.Name = "id_Fecha_Proceso"
            Me.id_Fecha_Proceso.ReadOnly = True
            Me.id_Fecha_Proceso.Visible = False
            '
            'Fecha_Proceso
            '
            Me.Fecha_Proceso.DataPropertyName = "Fecha_Proceso"
            Me.Fecha_Proceso.HeaderText = "Fecha Proceso"
            Me.Fecha_Proceso.Name = "Fecha_Proceso"
            Me.Fecha_Proceso.ReadOnly = True
            '
            'fk_Usuario_Apertura
            '
            Me.fk_Usuario_Apertura.DataPropertyName = "fk_Usuario_Apertura"
            Me.fk_Usuario_Apertura.HeaderText = "Id Usuario Apertura"
            Me.fk_Usuario_Apertura.Name = "fk_Usuario_Apertura"
            Me.fk_Usuario_Apertura.ReadOnly = True
            Me.fk_Usuario_Apertura.Visible = False
            '
            'Usuario_Apertura
            '
            Me.Usuario_Apertura.DataPropertyName = "Usuario_Apertura"
            Me.Usuario_Apertura.HeaderText = "Usuario Apertura"
            Me.Usuario_Apertura.Name = "Usuario_Apertura"
            Me.Usuario_Apertura.ReadOnly = True
            '
            'fecha_Apertura
            '
            Me.fecha_Apertura.DataPropertyName = "Fecha_Apertura"
            Me.fecha_Apertura.HeaderText = "Fecha Apertura"
            Me.fecha_Apertura.Name = "fecha_Apertura"
            Me.fecha_Apertura.ReadOnly = True
            '
            'Cerrado
            '
            Me.Cerrado.DataPropertyName = "Cerrado"
            Me.Cerrado.HeaderText = "Cerrada"
            Me.Cerrado.Name = "Cerrado"
            Me.Cerrado.ReadOnly = True
            '
            'fk_Usuario_Cierre
            '
            Me.fk_Usuario_Cierre.DataPropertyName = "fk_Usuario_Cierre"
            Me.fk_Usuario_Cierre.HeaderText = "Id Usuario Cierre"
            Me.fk_Usuario_Cierre.Name = "fk_Usuario_Cierre"
            Me.fk_Usuario_Cierre.ReadOnly = True
            Me.fk_Usuario_Cierre.Visible = False
            '
            'Usuario_Cierre
            '
            Me.Usuario_Cierre.DataPropertyName = "Usuario_Cierre"
            Me.Usuario_Cierre.HeaderText = "Usuario Cierre"
            Me.Usuario_Cierre.Name = "Usuario_Cierre"
            Me.Usuario_Cierre.ReadOnly = True
            '
            'Fecha_Cierre
            '
            Me.Fecha_Cierre.DataPropertyName = "Fecha_Cierre"
            Me.Fecha_Cierre.HeaderText = "Fecha Cierrre"
            Me.Fecha_Cierre.Name = "Fecha_Cierre"
            Me.Fecha_Cierre.ReadOnly = True
            '
            'BuscarButton
            '
            Me.BuscarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(567, 16)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(89, 31)
            Me.BuscarButton.TabIndex = 11
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'FechaFinalDateTimePicker
            '
            Me.FechaFinalDateTimePicker.Location = New System.Drawing.Point(218, 27)
            Me.FechaFinalDateTimePicker.Name = "FechaFinalDateTimePicker"
            Me.FechaFinalDateTimePicker.Size = New System.Drawing.Size(200, 20)
            Me.FechaFinalDateTimePicker.TabIndex = 10
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(215, 9)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(73, 13)
            Me.Label2.TabIndex = 9
            Me.Label2.Text = "Fecha Final"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(9, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(80, 13)
            Me.Label1.TabIndex = 8
            Me.Label1.Text = "Fecha Inicial"
            '
            'FechaInicialDateTimePicker
            '
            Me.FechaInicialDateTimePicker.Location = New System.Drawing.Point(12, 27)
            Me.FechaInicialDateTimePicker.Name = "FechaInicialDateTimePicker"
            Me.FechaInicialDateTimePicker.Size = New System.Drawing.Size(200, 20)
            Me.FechaInicialDateTimePicker.TabIndex = 7
            '
            'CrearFechaProcesoButton
            '
            Me.CrearFechaProcesoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.CrearFechaProcesoButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CrearFechaProcesoButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.sheet_add
            Me.CrearFechaProcesoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CrearFechaProcesoButton.Location = New System.Drawing.Point(12, 350)
            Me.CrearFechaProcesoButton.Name = "CrearFechaProcesoButton"
            Me.CrearFechaProcesoButton.Size = New System.Drawing.Size(168, 33)
            Me.CrearFechaProcesoButton.TabIndex = 13
            Me.CrearFechaProcesoButton.Text = "Abrir Nueva Fecha ..."
            Me.CrearFechaProcesoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CrearFechaProcesoButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancelar
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(570, 350)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 33)
            Me.CerrarButton.TabIndex = 12
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'CerrarFechaButton
            '
            Me.CerrarFechaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.CerrarFechaButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarFechaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.sheet_delete
            Me.CerrarFechaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarFechaButton.Location = New System.Drawing.Point(186, 350)
            Me.CerrarFechaButton.Name = "CerrarFechaButton"
            Me.CerrarFechaButton.Size = New System.Drawing.Size(134, 33)
            Me.CerrarFechaButton.TabIndex = 14
            Me.CerrarFechaButton.Text = "Cerrar Fecha ..."
            Me.CerrarFechaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarFechaButton.UseVisualStyleBackColor = True
            '
            'ProcessDataSet1
            '
            Me.ProcessDataSet1.DataSetName = "NewDataSet"
            '
            'FormFechaProceso
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(669, 390)
            Me.Controls.Add(Me.CerrarFechaButton)
            Me.Controls.Add(Me.CrearFechaProcesoButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.BuscarButton)
            Me.Controls.Add(Me.FechaFinalDateTimePicker)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.FechaInicialDateTimePicker)
            Me.Controls.Add(Me.FechaProcesoDataGridView)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Name = "FormFechaProceso"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Fechas de Proceso"
            CType(Me.FechaProcesoDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ProcessDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents FechaProcesoDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents FechaFinalDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents FechaInicialDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents CrearFechaProcesoButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarFechaButton As System.Windows.Forms.Button
        Friend WithEvents ProcessDataSet1 As DBImaging.ProcessDataSet
        Friend WithEvents fk_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Fecha_Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Fecha_Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Usuario_Apertura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Usuario_Apertura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fecha_Apertura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Cerrado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents fk_Usuario_Cierre As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Usuario_Cierre As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Fecha_Cierre As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace