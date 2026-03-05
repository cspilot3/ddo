Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Procesos.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormOT
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
            Me.CerrarOTButton = New System.Windows.Forms.Button()
            Me.CrearOTButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.FechaProcesoDataGridView = New System.Windows.Forms.DataGridView()
            Me.AbrirOTButton = New System.Windows.Forms.Button()
            Me.FechaProcesoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.fk_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Fecha_Proceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Usuario_Apertura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_OT_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_OT_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Usuario_Apertura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fecha_Apertura = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Cerrado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.fk_Usuario_Cierre = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Usuario_Cierre = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Fecha_Cierre = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.lineaProcesoColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            CType(Me.FechaProcesoDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'CerrarOTButton
            '
            Me.CerrarOTButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.CerrarOTButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarOTButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.sheet_delete
            Me.CerrarOTButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarOTButton.Location = New System.Drawing.Point(168, 368)
            Me.CerrarOTButton.Name = "CerrarOTButton"
            Me.CerrarOTButton.Size = New System.Drawing.Size(134, 33)
            Me.CerrarOTButton.TabIndex = 19
            Me.CerrarOTButton.Text = "Cerrar OT ..."
            Me.CerrarOTButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarOTButton.UseVisualStyleBackColor = True
            '
            'CrearOTButton
            '
            Me.CrearOTButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.CrearOTButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CrearOTButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.sheet_add
            Me.CrearOTButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CrearOTButton.Location = New System.Drawing.Point(17, 368)
            Me.CrearOTButton.Name = "CrearOTButton"
            Me.CrearOTButton.Size = New System.Drawing.Size(145, 33)
            Me.CrearOTButton.TabIndex = 18
            Me.CrearOTButton.Text = "Abrir Nueva OT ..."
            Me.CrearOTButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CrearOTButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancelar
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(527, 368)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 33)
            Me.CerrarButton.TabIndex = 17
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
            Me.Label1.TabIndex = 16
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
            Me.FechaProcesoDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fk_Entidad, Me.fk_Proyecto, Me.fk_Fecha_Proceso, Me.id_OT, Me.fk_Usuario_Apertura, Me.fk_OT_Tipo, Me.Nombre_OT_Tipo, Me.Usuario_Apertura, Me.fecha_Apertura, Me.Cerrado, Me.fk_Usuario_Cierre, Me.Usuario_Cierre, Me.Fecha_Cierre, Me.lineaProcesoColumn})
            Me.FechaProcesoDataGridView.Location = New System.Drawing.Point(15, 58)
            Me.FechaProcesoDataGridView.MultiSelect = False
            Me.FechaProcesoDataGridView.Name = "FechaProcesoDataGridView"
            Me.FechaProcesoDataGridView.ReadOnly = True
            Me.FechaProcesoDataGridView.Size = New System.Drawing.Size(599, 297)
            Me.FechaProcesoDataGridView.TabIndex = 15
            '
            'AbrirOTButton
            '
            Me.AbrirOTButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.AbrirOTButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AbrirOTButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.refresh
            Me.AbrirOTButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AbrirOTButton.Location = New System.Drawing.Point(308, 368)
            Me.AbrirOTButton.Name = "AbrirOTButton"
            Me.AbrirOTButton.Size = New System.Drawing.Size(134, 33)
            Me.AbrirOTButton.TabIndex = 21
            Me.AbrirOTButton.Text = "Reabrir OT ..."
            Me.AbrirOTButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AbrirOTButton.UseVisualStyleBackColor = True
            '
            'FechaProcesoComboBox
            '
            Me.FechaProcesoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.FechaProcesoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.FechaProcesoComboBox.DisabledEnter = False
            Me.FechaProcesoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.FechaProcesoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.FechaProcesoComboBox.FormattingEnabled = True
            Me.FechaProcesoComboBox.Location = New System.Drawing.Point(15, 26)
            Me.FechaProcesoComboBox.Name = "FechaProcesoComboBox"
            Me.FechaProcesoComboBox.Size = New System.Drawing.Size(329, 21)
            Me.FechaProcesoComboBox.TabIndex = 22
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
            'id_OT
            '
            Me.id_OT.DataPropertyName = "id_OT"
            Me.id_OT.HeaderText = "Id OT"
            Me.id_OT.Name = "id_OT"
            Me.id_OT.ReadOnly = True
            '
            'fk_Usuario_Apertura
            '
            Me.fk_Usuario_Apertura.DataPropertyName = "fk_Usuario_Apertura"
            Me.fk_Usuario_Apertura.HeaderText = "Id Usuario Apertura"
            Me.fk_Usuario_Apertura.Name = "fk_Usuario_Apertura"
            Me.fk_Usuario_Apertura.ReadOnly = True
            Me.fk_Usuario_Apertura.Visible = False
            '
            'fk_OT_Tipo
            '
            Me.fk_OT_Tipo.DataPropertyName = "fk_OT_Tipo"
            Me.fk_OT_Tipo.HeaderText = "Id Tipo OT"
            Me.fk_OT_Tipo.Name = "fk_OT_Tipo"
            Me.fk_OT_Tipo.ReadOnly = True
            Me.fk_OT_Tipo.Visible = False
            '
            'Nombre_OT_Tipo
            '
            Me.Nombre_OT_Tipo.DataPropertyName = "Nombre_OT_Tipo"
            Me.Nombre_OT_Tipo.HeaderText = "Tipo OT"
            Me.Nombre_OT_Tipo.Name = "Nombre_OT_Tipo"
            Me.Nombre_OT_Tipo.ReadOnly = True
            '
            'Usuario_Apertura
            '
            Me.Usuario_Apertura.DataPropertyName = "Nombres"
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
            Me.Usuario_Cierre.DataPropertyName = "Nombres1"
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
            'lineaProcesoColumn
            '
            Me.lineaProcesoColumn.DataPropertyName = "fk_Linea_Proceso"
            Me.lineaProcesoColumn.HeaderText = "Línea Proceso"
            Me.lineaProcesoColumn.Name = "lineaProcesoColumn"
            Me.lineaProcesoColumn.ReadOnly = True
            '
            'FormOT
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(630, 423)
            Me.Controls.Add(Me.FechaProcesoComboBox)
            Me.Controls.Add(Me.AbrirOTButton)
            Me.Controls.Add(Me.CerrarOTButton)
            Me.Controls.Add(Me.CrearOTButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.FechaProcesoDataGridView)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MinimizeBox = False
            Me.Name = "FormOT"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "OT"
            CType(Me.FechaProcesoDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents CerrarOTButton As System.Windows.Forms.Button
        Friend WithEvents CrearOTButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents AbrirOTButton As System.Windows.Forms.Button
        Friend WithEvents FechaProcesoComboBox As DesktopComboBoxControl
        Friend WithEvents fk_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Fecha_Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_OT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Usuario_Apertura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_OT_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_OT_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Usuario_Apertura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fecha_Apertura As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Cerrado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents fk_Usuario_Cierre As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Usuario_Cierre As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Fecha_Cierre As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents lineaProcesoColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace