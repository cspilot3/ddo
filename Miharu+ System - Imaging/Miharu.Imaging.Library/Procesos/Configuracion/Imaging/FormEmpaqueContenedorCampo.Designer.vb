Namespace Procesos.Configuracion.Imaging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEmpaqueContenedorCampo
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
            Me.components = New System.ComponentModel.Container()
            Me.CamposDataGridView = New System.Windows.Forms.DataGridView()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Button1 = New System.Windows.Forms.Button()
            Me._DB_Miharu_ImagingDataSet = New Miharu.Imaging.Library._DB_Miharu_ImagingDataSet()
            Me.DBMiharuImagingDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.fk_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Campo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Eliminado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me._DB_Miharu_ImagingDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DBMiharuImagingDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'CamposDataGridView
            '
            Me.CamposDataGridView.AllowUserToAddRows = False
            Me.CamposDataGridView.AllowUserToDeleteRows = False
            Me.CamposDataGridView.AllowUserToResizeColumns = False
            Me.CamposDataGridView.AllowUserToResizeRows = False
            Me.CamposDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CamposDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
            Me.CamposDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CamposDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fk_Entidad, Me.fk_Proyecto, Me.id_Campo, Me.Nombre_Campo, Me.Eliminado})
            Me.CamposDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            Me.CamposDataGridView.Location = New System.Drawing.Point(14, 28)
            Me.CamposDataGridView.MultiSelect = False
            Me.CamposDataGridView.Name = "CamposDataGridView"
            Me.CamposDataGridView.RowHeadersWidth = 20
            Me.CamposDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
            Me.CamposDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
            Me.CamposDataGridView.Size = New System.Drawing.Size(430, 139)
            Me.CamposDataGridView.TabIndex = 31
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.sheet_add
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(228, 180)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(105, 30)
            Me.GuardarButton.TabIndex = 32
            Me.GuardarButton.Text = "&Agregar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(339, 180)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(105, 30)
            Me.CerrarButton.TabIndex = 33
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Button1
            '
            Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button1.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGuardar
            Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Button1.Location = New System.Drawing.Point(228, 180)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(105, 30)
            Me.Button1.TabIndex = 34
            Me.Button1.Text = "&Guardar"
            Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.Button1.UseVisualStyleBackColor = True
            Me.Button1.Visible = False
            '
            '_DB_Miharu_ImagingDataSet
            '
            Me._DB_Miharu_ImagingDataSet.DataSetName = "_DB_Miharu_ImagingDataSet"
            Me._DB_Miharu_ImagingDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'DBMiharuImagingDataSetBindingSource
            '
            Me.DBMiharuImagingDataSetBindingSource.DataSource = Me._DB_Miharu_ImagingDataSet
            Me.DBMiharuImagingDataSetBindingSource.Position = 0
            '
            'fk_Entidad
            '
            Me.fk_Entidad.DataPropertyName = "fk_Entidad"
            Me.fk_Entidad.HeaderText = "fk_Entidad"
            Me.fk_Entidad.Name = "fk_Entidad"
            Me.fk_Entidad.ReadOnly = True
            Me.fk_Entidad.Visible = False
            '
            'fk_Proyecto
            '
            Me.fk_Proyecto.DataPropertyName = "fk_Proyecto"
            Me.fk_Proyecto.HeaderText = "fk_Proyecto"
            Me.fk_Proyecto.Name = "fk_Proyecto"
            Me.fk_Proyecto.ReadOnly = True
            Me.fk_Proyecto.Visible = False
            '
            'id_Campo
            '
            Me.id_Campo.DataPropertyName = "id_Campo"
            Me.id_Campo.HeaderText = "Id Campo"
            Me.id_Campo.Name = "id_Campo"
            Me.id_Campo.ReadOnly = True
            Me.id_Campo.Width = 50
            '
            'Nombre_Campo
            '
            Me.Nombre_Campo.DataPropertyName = "Nombre_Campo"
            Me.Nombre_Campo.HeaderText = "Nombre Campo"
            Me.Nombre_Campo.Name = "Nombre_Campo"
            Me.Nombre_Campo.ReadOnly = True
            Me.Nombre_Campo.Width = 200
            '
            'Eliminado
            '
            Me.Eliminado.DataPropertyName = "Eliminado"
            Me.Eliminado.HeaderText = "Eliminado"
            Me.Eliminado.Name = "Eliminado"
            '
            'FormEmpaqueContenedorCampo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(468, 222)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.CamposDataGridView)
            Me.Controls.Add(Me.Button1)
            Me.Name = "FormEmpaqueContenedorCampo"
            Me.ShowIcon = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Parametrización Campos Empaque Contenedor"
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me._DB_Miharu_ImagingDataSet, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DBMiharuImagingDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CamposDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents _DB_Miharu_ImagingDataSet As Miharu.Imaging.Library._DB_Miharu_ImagingDataSet
        Friend WithEvents DBMiharuImagingDataSetBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Friend WithEvents fk_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Campo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Eliminado As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace