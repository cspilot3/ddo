Namespace Imaging.Forms.Seguimiento
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeguimientoCanje
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
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.FechaFinalDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.FechaInicialDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.CanjeDatosDataGridView = New System.Windows.Forms.DataGridView()
            Me._DB_Miharu_Banco_AgrarioDataSet1 = New Banagrario.Plugin._DB_Miharu_Banco_AgrarioDataSet()
            Me.ColumnFecha_Movimiento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnCiudad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnCantidad_a_Transmitir = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnCantidad_Transmitida = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Panel1.SuspendLayout()
            CType(Me.CanjeDatosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me._DB_Miharu_Banco_AgrarioDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.BuscarButton)
            Me.Panel1.Controls.Add(Me.FechaFinalDateTimePicker)
            Me.Panel1.Controls.Add(Me.Label2)
            Me.Panel1.Controls.Add(Me.Label1)
            Me.Panel1.Controls.Add(Me.FechaInicialDateTimePicker)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(643, 64)
            Me.Panel1.TabIndex = 0
            '
            'BuscarButton
            '
            Me.BuscarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(490, 24)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(89, 31)
            Me.BuscarButton.TabIndex = 10
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'FechaFinalDateTimePicker
            '
            Me.FechaFinalDateTimePicker.Location = New System.Drawing.Point(222, 29)
            Me.FechaFinalDateTimePicker.Name = "FechaFinalDateTimePicker"
            Me.FechaFinalDateTimePicker.Size = New System.Drawing.Size(200, 20)
            Me.FechaFinalDateTimePicker.TabIndex = 9
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(219, 13)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(73, 13)
            Me.Label2.TabIndex = 8
            Me.Label2.Text = "Fecha Final"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(13, 13)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(80, 13)
            Me.Label1.TabIndex = 7
            Me.Label1.Text = "Fecha Inicial"
            '
            'FechaInicialDateTimePicker
            '
            Me.FechaInicialDateTimePicker.Location = New System.Drawing.Point(16, 29)
            Me.FechaInicialDateTimePicker.Name = "FechaInicialDateTimePicker"
            Me.FechaInicialDateTimePicker.Size = New System.Drawing.Size(200, 20)
            Me.FechaInicialDateTimePicker.TabIndex = 6
            '
            'CanjeDatosDataGridView
            '
            Me.CanjeDatosDataGridView.AllowUserToAddRows = False
            Me.CanjeDatosDataGridView.AllowUserToDeleteRows = False
            Me.CanjeDatosDataGridView.AutoGenerateColumns = False
            Me.CanjeDatosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CanjeDatosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnFecha_Movimiento, Me.ColumnCiudad, Me.ColumnCantidad_a_Transmitir, Me.ColumnCantidad_Transmitida})
            Me.CanjeDatosDataGridView.DataMember = "CTA_Get_Consolidado_Registros_Canje"
            Me.CanjeDatosDataGridView.DataSource = Me._DB_Miharu_Banco_AgrarioDataSet1
            Me.CanjeDatosDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CanjeDatosDataGridView.Location = New System.Drawing.Point(0, 64)
            Me.CanjeDatosDataGridView.Name = "CanjeDatosDataGridView"
            Me.CanjeDatosDataGridView.ReadOnly = True
            Me.CanjeDatosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.CanjeDatosDataGridView.Size = New System.Drawing.Size(643, 199)
            Me.CanjeDatosDataGridView.TabIndex = 1
            '
            '_DB_Miharu_Banco_AgrarioDataSet1
            '
            Me._DB_Miharu_Banco_AgrarioDataSet1.DataSetName = "_DB_Miharu_Banco_AgrarioDataSet"
            Me._DB_Miharu_Banco_AgrarioDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'ColumnFecha_Movimiento
            '
            Me.ColumnFecha_Movimiento.DataPropertyName = "Fecha_Proceso"
            Me.ColumnFecha_Movimiento.HeaderText = "Fecha Movimiento"
            Me.ColumnFecha_Movimiento.Name = "ColumnFecha_Movimiento"
            Me.ColumnFecha_Movimiento.ReadOnly = True
            Me.ColumnFecha_Movimiento.Width = 150
            '
            'ColumnCiudad
            '
            Me.ColumnCiudad.DataPropertyName = "Nombre_Conexion"
            Me.ColumnCiudad.HeaderText = "Ciudad"
            Me.ColumnCiudad.Name = "ColumnCiudad"
            Me.ColumnCiudad.ReadOnly = True
            Me.ColumnCiudad.Width = 150
            '
            'ColumnCantidad_a_Transmitir
            '
            Me.ColumnCantidad_a_Transmitir.DataPropertyName = "Cantidad_Transmitir"
            Me.ColumnCantidad_a_Transmitir.HeaderText = "Cantidad a Transmitir"
            Me.ColumnCantidad_a_Transmitir.Name = "ColumnCantidad_a_Transmitir"
            Me.ColumnCantidad_a_Transmitir.ReadOnly = True
            Me.ColumnCantidad_a_Transmitir.Width = 150
            '
            'ColumnCantidad_Transmitida
            '
            Me.ColumnCantidad_Transmitida.DataPropertyName = "Cantidad_Transmitida"
            Me.ColumnCantidad_Transmitida.HeaderText = "Cantidad Transmtida"
            Me.ColumnCantidad_Transmitida.Name = "ColumnCantidad_Transmitida"
            Me.ColumnCantidad_Transmitida.ReadOnly = True
            Me.ColumnCantidad_Transmitida.Width = 150
            '
            'FormSeguimientoCanje
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(643, 263)
            Me.Controls.Add(Me.CanjeDatosDataGridView)
            Me.Controls.Add(Me.Panel1)
            Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
            Me.Location = New System.Drawing.Point(16, 29)
            Me.MaximizeBox = False
            Me.Name = "FormSeguimientoCanje"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Seguimiento Canje"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            CType(Me.CanjeDatosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me._DB_Miharu_Banco_AgrarioDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents FechaFinalDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents FechaInicialDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents CanjeDatosDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents CTA_Get_Consolidado_Registros_CanjeTableAdapter As Banagrario.Plugin._DB_Miharu_Banco_AgrarioDataSetTableAdapters.CTA_Get_Consolidado_Registros_CanjeTableAdapter
        Friend WithEvents FechaMovimientoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CiudadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CantidadATransmitirDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CantidadTransmtidaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents _DB_Miharu_Banco_AgrarioDataSet1 As Banagrario.Plugin._DB_Miharu_Banco_AgrarioDataSet
        Friend WithEvents ColumnFecha_Movimiento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnCiudad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnCantidad_a_Transmitir As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnCantidad_Transmitida As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace