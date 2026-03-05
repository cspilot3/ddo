<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPrecintoHojaControl
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
        Me.PrecintoDataGridView = New System.Windows.Forms.DataGridView()
        Me.Precinto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TipoGestionComboBox = New System.Windows.Forms.ComboBox()
        CType(Me.PrecintoDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OTLabel
        '
        Me.OTLabel.AutoSize = True
        Me.OTLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OTLabel.Location = New System.Drawing.Point(28, 61)
        Me.OTLabel.Name = "OTLabel"
        Me.OTLabel.Size = New System.Drawing.Size(143, 15)
        Me.OTLabel.TabIndex = 19
        Me.OTLabel.Text = "Selecione el precinto"
        '
        'PrecintoDataGridView
        '
        Me.PrecintoDataGridView.AllowUserToAddRows = False
        Me.PrecintoDataGridView.AllowUserToDeleteRows = False
        Me.PrecintoDataGridView.AllowUserToResizeColumns = False
        Me.PrecintoDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PrecintoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PrecintoDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Precinto})
        Me.PrecintoDataGridView.Location = New System.Drawing.Point(31, 90)
        Me.PrecintoDataGridView.MultiSelect = False
        Me.PrecintoDataGridView.Name = "PrecintoDataGridView"
        Me.PrecintoDataGridView.ReadOnly = True
        Me.PrecintoDataGridView.RowHeadersWidth = 10
        Me.PrecintoDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.PrecintoDataGridView.Size = New System.Drawing.Size(256, 140)
        Me.PrecintoDataGridView.TabIndex = 21
        Me.PrecintoDataGridView.Visible = False
        '
        'Precinto
        '
        Me.Precinto.DataPropertyName = "Precinto"
        Me.Precinto.HeaderText = "Precinto"
        Me.Precinto.Name = "Precinto"
        Me.Precinto.ReadOnly = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(28, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Tipo Gestión"
        '
        'TipoGestionComboBox
        '
        Me.TipoGestionComboBox.FormattingEnabled = True
        Me.TipoGestionComboBox.Location = New System.Drawing.Point(31, 37)
        Me.TipoGestionComboBox.Name = "TipoGestionComboBox"
        Me.TipoGestionComboBox.Size = New System.Drawing.Size(256, 21)
        Me.TipoGestionComboBox.TabIndex = 22
        '
        'FormPrecintoHojaControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(321, 242)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TipoGestionComboBox)
        Me.Controls.Add(Me.PrecintoDataGridView)
        Me.Controls.Add(Me.OTLabel)
        Me.Name = "FormPrecintoHojaControl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormCarpetas"
        CType(Me.PrecintoDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OTLabel As System.Windows.Forms.Label
    Friend WithEvents PrecintoDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Precinto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TipoGestionComboBox As System.Windows.Forms.ComboBox
End Class
