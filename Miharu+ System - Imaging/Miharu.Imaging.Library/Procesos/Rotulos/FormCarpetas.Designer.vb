<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCarpetas
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
        Me.OTLabel = New System.Windows.Forms.Label()
        Me.CarpetasDataGridView = New System.Windows.Forms.DataGridView()
        Me.Expediente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Carpeta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.CarpetasDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OTLabel
        '
        Me.OTLabel.AutoSize = True
        Me.OTLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OTLabel.Location = New System.Drawing.Point(12, 18)
        Me.OTLabel.Name = "OTLabel"
        Me.OTLabel.Size = New System.Drawing.Size(145, 15)
        Me.OTLabel.TabIndex = 19
        Me.OTLabel.Text = "Seleecion la Carpeta:"
        '
        'CarpetasDataGridView
        '
        Me.CarpetasDataGridView.AllowUserToAddRows = False
        Me.CarpetasDataGridView.AllowUserToDeleteRows = False
        Me.CarpetasDataGridView.AllowUserToResizeColumns = False
        Me.CarpetasDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CarpetasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CarpetasDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Expediente, Me.Carpeta})
        Me.CarpetasDataGridView.Location = New System.Drawing.Point(15, 45)
        Me.CarpetasDataGridView.MultiSelect = False
        Me.CarpetasDataGridView.Name = "CarpetasDataGridView"
        Me.CarpetasDataGridView.ReadOnly = True
        Me.CarpetasDataGridView.RowHeadersWidth = 10
        Me.CarpetasDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.CarpetasDataGridView.Size = New System.Drawing.Size(219, 305)
        Me.CarpetasDataGridView.TabIndex = 20
        Me.CarpetasDataGridView.Visible = False
        '
        'Expediente
        '
        Me.Expediente.DataPropertyName = "Expediente"
        Me.Expediente.HeaderText = "Expedientes"
        Me.Expediente.Name = "Expediente"
        '
        'Carpeta
        '
        Me.Carpeta.DataPropertyName = "Carpeta"
        Me.Carpeta.HeaderText = "Carpetas"
        Me.Carpeta.Name = "Carpeta"
        '
        'FormCarpetas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(248, 395)
        Me.Controls.Add(Me.CarpetasDataGridView)
        Me.Controls.Add(Me.OTLabel)
        Me.Name = "FormCarpetas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormCarpetas"
        CType(Me.CarpetasDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OTLabel As System.Windows.Forms.Label
    Friend WithEvents CarpetasDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Expediente As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Carpeta As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
