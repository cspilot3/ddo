Namespace Imaging.UMV.Exportar
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormExportar
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
            Me.MainGroupBox = New System.Windows.Forms.GroupBox()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CarpetaSalidaTextBox = New System.Windows.Forms.TextBox()
            Me.BuscarCarpetaButton = New System.Windows.Forms.Button()
            Me.OTLabel = New System.Windows.Forms.Label()
            Me.EstibasDataGridView = New System.Windows.Forms.DataGridView()
            Me.Expediente = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Exportar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Ruta = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.ExportarButton = New System.Windows.Forms.Button()
            Me.MainGroupBox.SuspendLayout()
            Me.Panel2.SuspendLayout()
            CType(Me.EstibasDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'MainGroupBox
            '
            Me.MainGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MainGroupBox.Controls.Add(Me.Panel2)
            Me.MainGroupBox.Controls.Add(Me.OTLabel)
            Me.MainGroupBox.Controls.Add(Me.EstibasDataGridView)
            Me.MainGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MainGroupBox.Location = New System.Drawing.Point(12, 24)
            Me.MainGroupBox.Name = "MainGroupBox"
            Me.MainGroupBox.Size = New System.Drawing.Size(700, 273)
            Me.MainGroupBox.TabIndex = 3
            Me.MainGroupBox.TabStop = False
            Me.MainGroupBox.Text = "Parametros de exportación"
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.Label1)
            Me.Panel2.Controls.Add(Me.CarpetaSalidaTextBox)
            Me.Panel2.Controls.Add(Me.BuscarCarpetaButton)
            Me.Panel2.Location = New System.Drawing.Point(15, 169)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(621, 79)
            Me.Panel2.TabIndex = 20
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(3, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(122, 15)
            Me.Label1.TabIndex = 5
            Me.Label1.Text = "Carpeta de Salida"
            '
            'CarpetaSalidaTextBox
            '
            Me.CarpetaSalidaTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CarpetaSalidaTextBox.Location = New System.Drawing.Point(7, 28)
            Me.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox"
            Me.CarpetaSalidaTextBox.Size = New System.Drawing.Size(550, 20)
            Me.CarpetaSalidaTextBox.TabIndex = 6
            '
            'BuscarCarpetaButton
            '
            Me.BuscarCarpetaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarCarpetaButton.Image = Global.UMV.Plugin.My.Resources.Resources.MainFolder
            Me.BuscarCarpetaButton.Location = New System.Drawing.Point(563, 22)
            Me.BuscarCarpetaButton.Name = "BuscarCarpetaButton"
            Me.BuscarCarpetaButton.Size = New System.Drawing.Size(43, 30)
            Me.BuscarCarpetaButton.TabIndex = 7
            Me.BuscarCarpetaButton.UseVisualStyleBackColor = True
            '
            'OTLabel
            '
            Me.OTLabel.AutoSize = True
            Me.OTLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OTLabel.Location = New System.Drawing.Point(12, 16)
            Me.OTLabel.Name = "OTLabel"
            Me.OTLabel.Size = New System.Drawing.Size(47, 15)
            Me.OTLabel.TabIndex = 3
            Me.OTLabel.Text = "Estiba"
            Me.OTLabel.Visible = False
            '
            'EstibasDataGridView
            '
            Me.EstibasDataGridView.AllowUserToAddRows = False
            Me.EstibasDataGridView.AllowUserToDeleteRows = False
            Me.EstibasDataGridView.AllowUserToResizeColumns = False
            Me.EstibasDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EstibasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.EstibasDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Expediente, Me.Exportar, Me.Ruta})
            Me.EstibasDataGridView.Location = New System.Drawing.Point(15, 45)
            Me.EstibasDataGridView.MultiSelect = False
            Me.EstibasDataGridView.Name = "EstibasDataGridView"
            Me.EstibasDataGridView.RowHeadersWidth = 10
            Me.EstibasDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.EstibasDataGridView.Size = New System.Drawing.Size(673, 101)
            Me.EstibasDataGridView.TabIndex = 16
            '
            'Expediente
            '
            Me.Expediente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Expediente.DataPropertyName = "CodigoEstiba"
            Me.Expediente.HeaderText = "Estiba"
            Me.Expediente.Name = "Expediente"
            Me.Expediente.ReadOnly = True
            '
            'Exportar
            '
            Me.Exportar.DataPropertyName = "Exportada"
            Me.Exportar.HeaderText = "Exportada"
            Me.Exportar.Name = "Exportar"
            Me.Exportar.Width = 60
            '
            'Ruta
            '
            Me.Ruta.DataPropertyName = "Ruta"
            Me.Ruta.HeaderText = "Ruta"
            Me.Ruta.Name = "Ruta"
            Me.Ruta.ReadOnly = True
            Me.Ruta.Width = 300
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.UMV.Plugin.My.Resources.Resources.cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(595, 320)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(104, 37)
            Me.CancelarButton.TabIndex = 8
            Me.CancelarButton.Text = "Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'ExportarButton
            '
            Me.ExportarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ExportarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ExportarButton.Image = Global.UMV.Plugin.My.Resources.Resources.Aceptar
            Me.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ExportarButton.Location = New System.Drawing.Point(480, 320)
            Me.ExportarButton.Name = "ExportarButton"
            Me.ExportarButton.Size = New System.Drawing.Size(104, 37)
            Me.ExportarButton.TabIndex = 7
            Me.ExportarButton.Text = "Exportar"
            Me.ExportarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ExportarButton.UseVisualStyleBackColor = True
            '
            'FormExportar
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(724, 403)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.ExportarButton)
            Me.Controls.Add(Me.MainGroupBox)
            Me.Name = "FormExportar"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "FormExportar"
            Me.MainGroupBox.ResumeLayout(False)
            Me.MainGroupBox.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            CType(Me.EstibasDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
        Private WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CarpetaSalidaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents BuscarCarpetaButton As System.Windows.Forms.Button
        Friend WithEvents OTLabel As System.Windows.Forms.Label
        Friend WithEvents EstibasDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents Expediente As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Exportar As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Ruta As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents ExportarButton As System.Windows.Forms.Button
    End Class
End Namespace