Namespace DesktopCBarras
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCBarrasSelector
        Inherits System.Windows.Forms.Form

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
            Me.selectorDataGridView = New System.Windows.Forms.DataGridView()
            Me.id_EntidadColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_EntidadColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_ProyectoColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_ProyectoColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_EsquemaColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_EsquemaColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarras_RiskColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarras_PLColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            CType(Me.selectorDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'selectorDataGridView
            '
            Me.selectorDataGridView.AllowDrop = True
            Me.selectorDataGridView.AllowUserToAddRows = False
            Me.selectorDataGridView.AllowUserToDeleteRows = False
            Me.selectorDataGridView.AllowUserToResizeRows = False
            Me.selectorDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.selectorDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_EntidadColumn, Me.Nombre_EntidadColumn, Me.id_ProyectoColumn, Me.Nombre_ProyectoColumn, Me.id_EsquemaColumn, Me.Nombre_EsquemaColumn, Me.CBarras_RiskColumn, Me.CBarras_PLColumn})
            Me.selectorDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.selectorDataGridView.Location = New System.Drawing.Point(0, 0)
            Me.selectorDataGridView.Name = "selectorDataGridView"
            Me.selectorDataGridView.RowHeadersWidth = 10
            Me.selectorDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.selectorDataGridView.Size = New System.Drawing.Size(646, 261)
            Me.selectorDataGridView.TabIndex = 0
            '
            'id_EntidadColumn
            '
            Me.id_EntidadColumn.DataPropertyName = "id_Entidad"
            Me.id_EntidadColumn.HeaderText = "idEntidad"
            Me.id_EntidadColumn.Name = "id_EntidadColumn"
            Me.id_EntidadColumn.ReadOnly = True
            Me.id_EntidadColumn.Visible = False
            '
            'Nombre_EntidadColumn
            '
            Me.Nombre_EntidadColumn.DataPropertyName = "Nombre_Entidad"
            Me.Nombre_EntidadColumn.HeaderText = "Entidad"
            Me.Nombre_EntidadColumn.Name = "Nombre_EntidadColumn"
            Me.Nombre_EntidadColumn.ReadOnly = True
            Me.Nombre_EntidadColumn.Width = 150
            '
            'id_ProyectoColumn
            '
            Me.id_ProyectoColumn.DataPropertyName = "id_Proyecto"
            Me.id_ProyectoColumn.HeaderText = "idProyecto"
            Me.id_ProyectoColumn.Name = "id_ProyectoColumn"
            Me.id_ProyectoColumn.ReadOnly = True
            Me.id_ProyectoColumn.Visible = False
            '
            'Nombre_ProyectoColumn
            '
            Me.Nombre_ProyectoColumn.DataPropertyName = "Nombre_Proyecto"
            Me.Nombre_ProyectoColumn.HeaderText = "Proyecto"
            Me.Nombre_ProyectoColumn.Name = "Nombre_ProyectoColumn"
            Me.Nombre_ProyectoColumn.ReadOnly = True
            Me.Nombre_ProyectoColumn.Width = 150
            '
            'id_EsquemaColumn
            '
            Me.id_EsquemaColumn.DataPropertyName = "id_Esquema"
            Me.id_EsquemaColumn.HeaderText = "idEsquema"
            Me.id_EsquemaColumn.Name = "id_EsquemaColumn"
            Me.id_EsquemaColumn.ReadOnly = True
            Me.id_EsquemaColumn.Visible = False
            '
            'Nombre_EsquemaColumn
            '
            Me.Nombre_EsquemaColumn.DataPropertyName = "Nombre_Esquema"
            Me.Nombre_EsquemaColumn.HeaderText = "Esquema"
            Me.Nombre_EsquemaColumn.Name = "Nombre_EsquemaColumn"
            Me.Nombre_EsquemaColumn.ReadOnly = True
            Me.Nombre_EsquemaColumn.Width = 150
            '
            'CBarras_RiskColumn
            '
            Me.CBarras_RiskColumn.DataPropertyName = "CBarras_Risk"
            Me.CBarras_RiskColumn.HeaderText = "CBarras Risk"
            Me.CBarras_RiskColumn.Name = "CBarras_RiskColumn"
            Me.CBarras_RiskColumn.ReadOnly = True
            Me.CBarras_RiskColumn.Width = 150
            '
            'CBarras_PLColumn
            '
            Me.CBarras_PLColumn.DataPropertyName = "CBarras_PL"
            Me.CBarras_PLColumn.HeaderText = "CBarras PL"
            Me.CBarras_PLColumn.Name = "CBarras_PLColumn"
            Me.CBarras_PLColumn.ReadOnly = True
            Me.CBarras_PLColumn.Visible = False
            Me.CBarras_PLColumn.Width = 150
            '
            'FormCBarrasSelector
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(646, 261)
            Me.Controls.Add(Me.selectorDataGridView)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Name = "FormCBarrasSelector"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Seleccionar CBarras"
            CType(Me.selectorDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents selectorDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents id_EntidadColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_EntidadColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_ProyectoColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_ProyectoColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_EsquemaColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_EsquemaColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarras_RiskColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarras_PLColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace