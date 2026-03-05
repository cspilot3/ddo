Imports Miharu.Desktop.Controls.DesktopDataGridView

Namespace Procesos.Configuracion.Imaging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormTipoOT
        Inherits Miharu.Desktop.Library.FormBase

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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
            Me.AddTipoOT = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.TipoOTDataGridView = New DesktopDataGridViewControl()
            Me.id_OT_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Eliminado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            CType(Me.TipoOTDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'DataGridViewImageColumn1
            '
            Me.DataGridViewImageColumn1.HeaderText = "Eliminar"
            Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
            Me.DataGridViewImageColumn1.Width = 58
            '
            'AddTipoOT
            '
            Me.AddTipoOT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.AddTipoOT.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnAgregar
            Me.AddTipoOT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AddTipoOT.Location = New System.Drawing.Point(23, 401)
            Me.AddTipoOT.Name = "AddTipoOT"
            Me.AddTipoOT.Size = New System.Drawing.Size(129, 30)
            Me.AddTipoOT.TabIndex = 62
            Me.AddTipoOT.Text = "&Agregar Tipo OT"
            Me.AddTipoOT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AddTipoOT.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(429, 401)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 59
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'TipoOTDataGridView
            '
            Me.TipoOTDataGridView.AllowUserToAddRows = False
            Me.TipoOTDataGridView.AllowUserToDeleteRows = False
            Me.TipoOTDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TipoOTDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.TipoOTDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.TipoOTDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.TipoOTDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.TipoOTDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_OT_Tipo, Me.Nombre, Me.Descripcion, Me.fk_Entidad, Me.fk_Proyecto, Me.Eliminado})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.TipoOTDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.TipoOTDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.TipoOTDataGridView.Location = New System.Drawing.Point(23, 12)
            Me.TipoOTDataGridView.MultiSelect = False
            Me.TipoOTDataGridView.Name = "TipoOTDataGridView"
            Me.TipoOTDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.TipoOTDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.TipoOTDataGridView.RowHeadersVisible = False
            Me.TipoOTDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.TipoOTDataGridView.Size = New System.Drawing.Size(496, 383)
            Me.TipoOTDataGridView.TabIndex = 63
            '
            'id_OT_Tipo
            '
            Me.id_OT_Tipo.DataPropertyName = "id_OT_Tipo"
            Me.id_OT_Tipo.HeaderText = "Id"
            Me.id_OT_Tipo.Name = "id_OT_Tipo"
            Me.id_OT_Tipo.ReadOnly = True
            Me.id_OT_Tipo.Width = 44
            '
            'Nombre
            '
            Me.Nombre.DataPropertyName = "Nombre_OT_Tipo"
            Me.Nombre.HeaderText = "Nombre"
            Me.Nombre.Name = "Nombre"
            Me.Nombre.ReadOnly = True
            Me.Nombre.Width = 76
            '
            'Descripcion
            '
            Me.Descripcion.DataPropertyName = "Descripcion_OT_Tipo"
            Me.Descripcion.HeaderText = "Descripcion"
            Me.Descripcion.Name = "Descripcion"
            Me.Descripcion.ReadOnly = True
            Me.Descripcion.Width = 97
            '
            'fk_Entidad
            '
            Me.fk_Entidad.DataPropertyName = "fk_Entidad"
            Me.fk_Entidad.HeaderText = "fk_Entidad"
            Me.fk_Entidad.Name = "fk_Entidad"
            Me.fk_Entidad.ReadOnly = True
            Me.fk_Entidad.Visible = False
            Me.fk_Entidad.Width = 92
            '
            'fk_Proyecto
            '
            Me.fk_Proyecto.DataPropertyName = "fk_Proyecto"
            Me.fk_Proyecto.HeaderText = "fk_Proyecto"
            Me.fk_Proyecto.Name = "fk_Proyecto"
            Me.fk_Proyecto.ReadOnly = True
            Me.fk_Proyecto.Visible = False
            Me.fk_Proyecto.Width = 101
            '
            'Eliminado
            '
            Me.Eliminado.DataPropertyName = "Eliminado"
            Me.Eliminado.HeaderText = "Eliminado"
            Me.Eliminado.Name = "Eliminado"
            Me.Eliminado.ReadOnly = True
            Me.Eliminado.Width = 67
            '
            'FormTipoOT
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(531, 443)
            Me.Controls.Add(Me.TipoOTDataGridView)
            Me.Controls.Add(Me.AddTipoOT)
            Me.Controls.Add(Me.CerrarButton)
            Me.Name = "FormTipoOT"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Tipo OT"
            CType(Me.TipoOTDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
        Friend WithEvents AddTipoOT As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents TipoOTDataGridView As DesktopDataGridViewControl
        Friend WithEvents id_OT_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Descripcion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Eliminado As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace