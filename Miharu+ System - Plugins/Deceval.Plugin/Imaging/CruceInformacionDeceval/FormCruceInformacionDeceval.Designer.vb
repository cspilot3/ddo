Namespace Imaging.CruceInformacionDeceval.Exportar
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCruceInformacionDeceval
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
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.CruceButton = New System.Windows.Forms.Button()
            Me.TabControl = New System.Windows.Forms.TabControl()
            Me.TabCruce = New System.Windows.Forms.TabPage()
            Me.GrillaCruzar = New System.Windows.Forms.DataGridView()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GrillaPendientes = New System.Windows.Forms.DataGridView()
            Me.LbEntrada = New System.Windows.Forms.Label()
            Me.MainGroupBox = New System.Windows.Forms.GroupBox()
            Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TabControl.SuspendLayout()
            Me.TabCruce.SuspendLayout()
            CType(Me.GrillaCruzar, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GrillaPendientes, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.MainGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.Deceval.Plugin.My.Resources.Resources.Cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(748, 439)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(89, 37)
            Me.CancelarButton.TabIndex = 4
            Me.CancelarButton.Text = "Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'CruceButton
            '
            Me.CruceButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CruceButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CruceButton.Image = Global.Deceval.Plugin.My.Resources.Resources.Aceptar
            Me.CruceButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CruceButton.Location = New System.Drawing.Point(649, 439)
            Me.CruceButton.Name = "CruceButton"
            Me.CruceButton.Size = New System.Drawing.Size(89, 37)
            Me.CruceButton.TabIndex = 3
            Me.CruceButton.Text = "Cruzar"
            Me.CruceButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CruceButton.UseVisualStyleBackColor = True
            '
            'TabControl
            '
            Me.TabControl.Controls.Add(Me.TabCruce)
            Me.TabControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TabControl.Location = New System.Drawing.Point(14, 19)
            Me.TabControl.Name = "TabControl"
            Me.TabControl.SelectedIndex = 0
            Me.TabControl.Size = New System.Drawing.Size(998, 401)
            Me.TabControl.TabIndex = 29
            '
            'TabCruce
            '
            Me.TabCruce.Controls.Add(Me.GrillaCruzar)
            Me.TabCruce.Controls.Add(Me.Label1)
            Me.TabCruce.Controls.Add(Me.GrillaPendientes)
            Me.TabCruce.Controls.Add(Me.LbEntrada)
            Me.TabCruce.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TabCruce.Location = New System.Drawing.Point(4, 22)
            Me.TabCruce.Name = "TabCruce"
            Me.TabCruce.Padding = New System.Windows.Forms.Padding(3)
            Me.TabCruce.Size = New System.Drawing.Size(990, 375)
            Me.TabCruce.TabIndex = 0
            Me.TabCruce.Text = "Cruce Información"
            Me.TabCruce.UseVisualStyleBackColor = True
            '
            'GrillaCruzar
            '
            Me.GrillaCruzar.AllowUserToAddRows = False
            Me.GrillaCruzar.AllowUserToDeleteRows = False
            Me.GrillaCruzar.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GrillaCruzar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.GrillaCruzar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.Column3, Me.Column4})
            Me.GrillaCruzar.Location = New System.Drawing.Point(9, 221)
            Me.GrillaCruzar.MultiSelect = False
            Me.GrillaCruzar.Name = "GrillaCruzar"
            Me.GrillaCruzar.RowHeadersWidth = 10
            Me.GrillaCruzar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.GrillaCruzar.Size = New System.Drawing.Size(808, 148)
            Me.GrillaCruzar.TabIndex = 36
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(2, 203)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(82, 15)
            Me.Label1.TabIndex = 35
            Me.Label1.Text = "Procesados"
            '
            'GrillaPendientes
            '
            Me.GrillaPendientes.AllowUserToAddRows = False
            Me.GrillaPendientes.AllowUserToDeleteRows = False
            Me.GrillaPendientes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GrillaPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.GrillaPendientes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.Column1, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.Column2})
            Me.GrillaPendientes.Location = New System.Drawing.Point(3, 23)
            Me.GrillaPendientes.MultiSelect = False
            Me.GrillaPendientes.Name = "GrillaPendientes"
            Me.GrillaPendientes.RowHeadersWidth = 10
            Me.GrillaPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.GrillaPendientes.Size = New System.Drawing.Size(814, 177)
            Me.GrillaPendientes.TabIndex = 34
            '
            'LbEntrada
            '
            Me.LbEntrada.AutoSize = True
            Me.LbEntrada.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LbEntrada.Location = New System.Drawing.Point(6, 5)
            Me.LbEntrada.Name = "LbEntrada"
            Me.LbEntrada.Size = New System.Drawing.Size(79, 15)
            Me.LbEntrada.TabIndex = 33
            Me.LbEntrada.Text = "Pendientes"
            '
            'MainGroupBox
            '
            Me.MainGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MainGroupBox.Controls.Add(Me.TabControl)
            Me.MainGroupBox.Location = New System.Drawing.Point(5, 7)
            Me.MainGroupBox.Name = "MainGroupBox"
            Me.MainGroupBox.Size = New System.Drawing.Size(841, 426)
            Me.MainGroupBox.TabIndex = 1
            Me.MainGroupBox.TabStop = False
            Me.MainGroupBox.Text = "Parametros"
            '
            'DataGridViewTextBoxColumn1
            '
            Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.DataGridViewTextBoxColumn1.DataPropertyName = "Id"
            Me.DataGridViewTextBoxColumn1.HeaderText = "Id"
            Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
            Me.DataGridViewTextBoxColumn1.Width = 30
            '
            'DataGridViewTextBoxColumn2
            '
            Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.DataGridViewTextBoxColumn2.DataPropertyName = "Fecha"
            Me.DataGridViewTextBoxColumn2.HeaderText = "Fecha"
            Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
            Me.DataGridViewTextBoxColumn2.Width = 140
            '
            'Column1
            '
            Me.Column1.DataPropertyName = "ArchivoEntrada"
            Me.Column1.HeaderText = "Archivo Entrada"
            Me.Column1.Name = "Column1"
            Me.Column1.Width = 130
            '
            'DataGridViewTextBoxColumn3
            '
            Me.DataGridViewTextBoxColumn3.DataPropertyName = "ArchivoSalida"
            Me.DataGridViewTextBoxColumn3.HeaderText = "Archivo Salida"
            Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
            Me.DataGridViewTextBoxColumn3.Width = 140
            '
            'DataGridViewTextBoxColumn4
            '
            Me.DataGridViewTextBoxColumn4.DataPropertyName = "Estado"
            Me.DataGridViewTextBoxColumn4.HeaderText = "Estado"
            Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
            '
            'Column2
            '
            Me.Column2.DataPropertyName = "Mensaje"
            Me.Column2.HeaderText = "Mensaje"
            Me.Column2.Name = "Column2"
            Me.Column2.Width = 260
            '
            'DataGridViewTextBoxColumn6
            '
            Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.DataGridViewTextBoxColumn6.DataPropertyName = "Fecha"
            Me.DataGridViewTextBoxColumn6.HeaderText = "Fecha"
            Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
            Me.DataGridViewTextBoxColumn6.Width = 140
            '
            'DataGridViewTextBoxColumn7
            '
            Me.DataGridViewTextBoxColumn7.DataPropertyName = "ArchivoEntrada"
            Me.DataGridViewTextBoxColumn7.HeaderText = "Archivo Entrada"
            Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
            Me.DataGridViewTextBoxColumn7.Width = 140
            '
            'DataGridViewTextBoxColumn8
            '
            Me.DataGridViewTextBoxColumn8.DataPropertyName = "ArchivoSalida"
            Me.DataGridViewTextBoxColumn8.HeaderText = "Archivo Salida"
            Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
            Me.DataGridViewTextBoxColumn8.Width = 140
            '
            'Column3
            '
            Me.Column3.DataPropertyName = "Estado"
            Me.Column3.HeaderText = "Estado"
            Me.Column3.Name = "Column3"
            '
            'Column4
            '
            Me.Column4.DataPropertyName = "Mensaje"
            Me.Column4.HeaderText = "Mensaje"
            Me.Column4.Name = "Column4"
            Me.Column4.Width = 275
            '
            'FormCruceInformacionDeceval
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(851, 487)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.CruceButton)
            Me.Controls.Add(Me.MainGroupBox)
            Me.Name = "FormCruceInformacionDeceval"
            Me.Text = "CruceInformacionDeceval"
            Me.TabControl.ResumeLayout(False)
            Me.TabCruce.ResumeLayout(False)
            Me.TabCruce.PerformLayout()
            CType(Me.GrillaCruzar, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GrillaPendientes, System.ComponentModel.ISupportInitialize).EndInit()
            Me.MainGroupBox.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents CruceButton As System.Windows.Forms.Button
        Friend WithEvents TabControl As System.Windows.Forms.TabControl
        Friend WithEvents TabCruce As System.Windows.Forms.TabPage
        Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents GrillaPendientes As System.Windows.Forms.DataGridView
        Friend WithEvents LbEntrada As System.Windows.Forms.Label
        Friend WithEvents GrillaCruzar As System.Windows.Forms.DataGridView
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace