Namespace Imaging.Forms.Parametrización
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Tipologia_CodigoTx
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
            Me.TipologiaTXDataGridView = New System.Windows.Forms.DataGridView()
            Me.fk_Tipologia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Tipologia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Tipo_Movimiento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Descripcion_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Producto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Producto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Codigo_Tx = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Desmaterializada = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Campos_Min_Cruce = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Usa_Llaves_Cruce = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Multiregistro = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Naturaleza_Medio_Pago = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Campos_Min_Cruce_Superior = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Eliminado_Tipologia = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.fk_Usuario_Log = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Fecha_Log = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.AñadirButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            CType(Me.TipologiaTXDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'TipologiaTXDataGridView
            '
            Me.TipologiaTXDataGridView.AllowUserToAddRows = False
            Me.TipologiaTXDataGridView.AllowUserToDeleteRows = False
            Me.TipologiaTXDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TipologiaTXDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.TipologiaTXDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fk_Tipologia, Me.Nombre_Tipologia, Me.fk_Tipo_Movimiento, Me.Descripcion_Tipo, Me.fk_Producto, Me.Nombre_Producto, Me.Codigo_Tx, Me.Desmaterializada, Me.Campos_Min_Cruce, Me.Usa_Llaves_Cruce, Me.Multiregistro, Me.Naturaleza_Medio_Pago, Me.Campos_Min_Cruce_Superior, Me.Eliminado_Tipologia, Me.fk_Usuario_Log, Me.Fecha_Log})
            Me.TipologiaTXDataGridView.Location = New System.Drawing.Point(12, 12)
            Me.TipologiaTXDataGridView.MultiSelect = False
            Me.TipologiaTXDataGridView.Name = "TipologiaTXDataGridView"
            Me.TipologiaTXDataGridView.ReadOnly = True
            Me.TipologiaTXDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.TipologiaTXDataGridView.Size = New System.Drawing.Size(835, 262)
            Me.TipologiaTXDataGridView.TabIndex = 38
            '
            'fk_Tipologia
            '
            Me.fk_Tipologia.DataPropertyName = "fk_Tipologia"
            Me.fk_Tipologia.HeaderText = "IdTipologia"
            Me.fk_Tipologia.Name = "fk_Tipologia"
            Me.fk_Tipologia.ReadOnly = True
            '
            'Nombre_Tipologia
            '
            Me.Nombre_Tipologia.DataPropertyName = "Nombre_Tipologia"
            Me.Nombre_Tipologia.HeaderText = "Tipologia"
            Me.Nombre_Tipologia.Name = "Nombre_Tipologia"
            Me.Nombre_Tipologia.ReadOnly = True
            '
            'fk_Tipo_Movimiento
            '
            Me.fk_Tipo_Movimiento.DataPropertyName = "fk_Tipo_Movimiento"
            Me.fk_Tipo_Movimiento.HeaderText = "IdTipoMovimiento"
            Me.fk_Tipo_Movimiento.Name = "fk_Tipo_Movimiento"
            Me.fk_Tipo_Movimiento.ReadOnly = True
            Me.fk_Tipo_Movimiento.Visible = False
            '
            'Descripcion_Tipo
            '
            Me.Descripcion_Tipo.DataPropertyName = "Descripcion_Tipo"
            Me.Descripcion_Tipo.HeaderText = "Tipo Movimiento"
            Me.Descripcion_Tipo.Name = "Descripcion_Tipo"
            Me.Descripcion_Tipo.ReadOnly = True
            '
            'fk_Producto
            '
            Me.fk_Producto.DataPropertyName = "fk_Producto"
            Me.fk_Producto.HeaderText = "IdProducto"
            Me.fk_Producto.Name = "fk_Producto"
            Me.fk_Producto.ReadOnly = True
            Me.fk_Producto.Visible = False
            '
            'Nombre_Producto
            '
            Me.Nombre_Producto.DataPropertyName = "Nombre_Producto"
            Me.Nombre_Producto.HeaderText = "Producto"
            Me.Nombre_Producto.Name = "Nombre_Producto"
            Me.Nombre_Producto.ReadOnly = True
            '
            'Codigo_Tx
            '
            Me.Codigo_Tx.DataPropertyName = "Codigo_Tx"
            Me.Codigo_Tx.HeaderText = "Codigo Tx"
            Me.Codigo_Tx.Name = "Codigo_Tx"
            Me.Codigo_Tx.ReadOnly = True
            '
            'Desmaterializada
            '
            Me.Desmaterializada.DataPropertyName = "Desmaterializada"
            Me.Desmaterializada.HeaderText = "Desmaterializada"
            Me.Desmaterializada.Name = "Desmaterializada"
            Me.Desmaterializada.ReadOnly = True
            '
            'Campos_Min_Cruce
            '
            Me.Campos_Min_Cruce.DataPropertyName = "Campos_Min_Cruce"
            Me.Campos_Min_Cruce.HeaderText = "Campos Mínimos Cruce"
            Me.Campos_Min_Cruce.Name = "Campos_Min_Cruce"
            Me.Campos_Min_Cruce.ReadOnly = True
            '
            'Usa_Llaves_Cruce
            '
            Me.Usa_Llaves_Cruce.DataPropertyName = "Usa_Llaves_Cruce"
            Me.Usa_Llaves_Cruce.HeaderText = "Usa Llaves Cruce"
            Me.Usa_Llaves_Cruce.Name = "Usa_Llaves_Cruce"
            Me.Usa_Llaves_Cruce.ReadOnly = True
            '
            'Multiregistro
            '
            Me.Multiregistro.DataPropertyName = "Multiregistro"
            Me.Multiregistro.HeaderText = "Multiregistro"
            Me.Multiregistro.Name = "Multiregistro"
            Me.Multiregistro.ReadOnly = True
            '
            'Naturaleza_Medio_Pago
            '
            Me.Naturaleza_Medio_Pago.DataPropertyName = "Naturaleza_Medio_Pago"
            Me.Naturaleza_Medio_Pago.HeaderText = "Naturaleza Medio Pago"
            Me.Naturaleza_Medio_Pago.Name = "Naturaleza_Medio_Pago"
            Me.Naturaleza_Medio_Pago.ReadOnly = True
            '
            'Campos_Min_Cruce_Superior
            '
            Me.Campos_Min_Cruce_Superior.DataPropertyName = "Campos_Min_Cruce_Superior"
            Me.Campos_Min_Cruce_Superior.HeaderText = "Campos Mínimos Cruce Superior"
            Me.Campos_Min_Cruce_Superior.Name = "Campos_Min_Cruce_Superior"
            Me.Campos_Min_Cruce_Superior.ReadOnly = True
            '
            'Eliminado_Tipologia
            '
            Me.Eliminado_Tipologia.DataPropertyName = "Eliminado_Tipologia"
            Me.Eliminado_Tipologia.HeaderText = "Eliminado"
            Me.Eliminado_Tipologia.Name = "Eliminado_Tipologia"
            Me.Eliminado_Tipologia.ReadOnly = True
            '
            'fk_Usuario_Log
            '
            Me.fk_Usuario_Log.DataPropertyName = "fk_Usuario_Log"
            Me.fk_Usuario_Log.HeaderText = "Usuario"
            Me.fk_Usuario_Log.Name = "fk_Usuario_Log"
            Me.fk_Usuario_Log.ReadOnly = True
            Me.fk_Usuario_Log.Visible = False
            '
            'Fecha_Log
            '
            Me.Fecha_Log.DataPropertyName = "Fecha_Log"
            Me.Fecha_Log.HeaderText = "Fecha"
            Me.Fecha_Log.Name = "Fecha_Log"
            Me.Fecha_Log.ReadOnly = True
            Me.Fecha_Log.Visible = False
            '
            'AñadirButton
            '
            Me.AñadirButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.AñadirButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnRecepcion
            Me.AñadirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AñadirButton.Location = New System.Drawing.Point(12, 280)
            Me.AñadirButton.Name = "AñadirButton"
            Me.AñadirButton.Size = New System.Drawing.Size(90, 30)
            Me.AñadirButton.TabIndex = 40
            Me.AñadirButton.Text = "&Añadir"
            Me.AñadirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AñadirButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(757, 280)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 39
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Tipologia_CodigoTx
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(859, 322)
            Me.Controls.Add(Me.AñadirButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.TipologiaTXDataGridView)
            Me.Name = "Tipologia_CodigoTx"
            Me.Text = "Tipologia_CodigoTx"
            CType(Me.TipologiaTXDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents AñadirButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents TipologiaTXDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents fk_Tipologia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Tipologia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Tipo_Movimiento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Descripcion_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Producto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Producto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Codigo_Tx As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Desmaterializada As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Campos_Min_Cruce As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Usa_Llaves_Cruce As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Multiregistro As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Naturaleza_Medio_Pago As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Campos_Min_Cruce_Superior As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Eliminado_Tipologia As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents fk_Usuario_Log As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Fecha_Log As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace