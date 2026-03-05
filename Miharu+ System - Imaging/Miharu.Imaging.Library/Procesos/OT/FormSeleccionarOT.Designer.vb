Namespace Procesos.OT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeleccionarOT
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
            Me.FechaProcesoComboBox = New System.Windows.Forms.ComboBox()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.FechaProcesoDataGridView = New System.Windows.Forms.DataGridView()
            Me.id_OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_OT_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Fecha_Proceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.SeleccionarButton = New System.Windows.Forms.Button()
            CType(Me.FechaProcesoDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'FechaProcesoComboBox
            '
            Me.FechaProcesoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.FechaProcesoComboBox.FormattingEnabled = True
            Me.FechaProcesoComboBox.Location = New System.Drawing.Point(15, 26)
            Me.FechaProcesoComboBox.Name = "FechaProcesoComboBox"
            Me.FechaProcesoComboBox.Size = New System.Drawing.Size(356, 21)
            Me.FechaProcesoComboBox.TabIndex = 27
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancelar
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(283, 391)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 33)
            Me.CerrarButton.TabIndex = 24
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(12, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(92, 13)
            Me.Label1.TabIndex = 23
            Me.Label1.Text = "Fecha Proceso"
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
            Me.FechaProcesoDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_OT, Me.Nombre_OT_Tipo, Me.fk_Entidad, Me.fk_Proyecto, Me.fk_Fecha_Proceso})
            Me.FechaProcesoDataGridView.Location = New System.Drawing.Point(15, 58)
            Me.FechaProcesoDataGridView.MultiSelect = False
            Me.FechaProcesoDataGridView.Name = "FechaProcesoDataGridView"
            Me.FechaProcesoDataGridView.ReadOnly = True
            Me.FechaProcesoDataGridView.RowHeadersVisible = False
            Me.FechaProcesoDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.FechaProcesoDataGridView.Size = New System.Drawing.Size(356, 327)
            Me.FechaProcesoDataGridView.TabIndex = 22
            '
            'id_OT
            '
            Me.id_OT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.id_OT.DataPropertyName = "id_OT"
            Me.id_OT.HeaderText = "OT"
            Me.id_OT.Name = "id_OT"
            Me.id_OT.ReadOnly = True
            '
            'Nombre_OT_Tipo
            '
            Me.Nombre_OT_Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Nombre_OT_Tipo.DataPropertyName = "Nombre_OT_Tipo"
            Me.Nombre_OT_Tipo.HeaderText = "Tipo OT"
            Me.Nombre_OT_Tipo.Name = "Nombre_OT_Tipo"
            Me.Nombre_OT_Tipo.ReadOnly = True
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
            'fk_Fecha_Proceso
            '
            Me.fk_Fecha_Proceso.DataPropertyName = "fk_Fecha_Proceso"
            Me.fk_Fecha_Proceso.HeaderText = "Id Fecha"
            Me.fk_Fecha_Proceso.Name = "fk_Fecha_Proceso"
            Me.fk_Fecha_Proceso.ReadOnly = True
            Me.fk_Fecha_Proceso.Visible = False
            '
            'SeleccionarButton
            '
            Me.SeleccionarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.SeleccionarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.tick
            Me.SeleccionarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.SeleccionarButton.Location = New System.Drawing.Point(12, 390)
            Me.SeleccionarButton.Name = "SeleccionarButton"
            Me.SeleccionarButton.Size = New System.Drawing.Size(124, 34)
            Me.SeleccionarButton.TabIndex = 28
            Me.SeleccionarButton.Text = "&Seleccionar OT"
            Me.SeleccionarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'FormSeleccionarOT
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(375, 425)
            Me.Controls.Add(Me.SeleccionarButton)
            Me.Controls.Add(Me.FechaProcesoComboBox)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.FechaProcesoDataGridView)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormSeleccionarOT"
            Me.ShowIcon = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Seleccionar OT"
            CType(Me.FechaProcesoDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents FechaProcesoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents id_OT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_OT_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Fecha_Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents SeleccionarButton As System.Windows.Forms.Button
    End Class
End Namespace