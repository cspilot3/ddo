Namespace Procesos.Configuracion.Imaging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParametros
        Inherits Miharu.Desktop.Library.FormBase

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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormParametros))
            Me.ParametrosDataGridView = New System.Windows.Forms.DataGridView()
            Me.NombreParametroDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ValorParametroDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DescripcionParametroDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TBLParametrosBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            CType(Me.ParametrosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.TBLParametrosBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ParametrosDataGridView
            '
            Me.ParametrosDataGridView.AllowUserToOrderColumns = True
            Me.ParametrosDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ParametrosDataGridView.AutoGenerateColumns = False
            Me.ParametrosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ParametrosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NombreParametroDataGridViewTextBoxColumn, Me.ValorParametroDataGridViewTextBoxColumn, Me.DescripcionParametroDataGridViewTextBoxColumn})
            Me.ParametrosDataGridView.DataSource = Me.TBLParametrosBindingSource
            Me.ParametrosDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            Me.ParametrosDataGridView.Location = New System.Drawing.Point(14, 12)
            Me.ParametrosDataGridView.Name = "ParametrosDataGridView"
            Me.ParametrosDataGridView.RowHeadersWidth = 20
            Me.ParametrosDataGridView.Size = New System.Drawing.Size(852, 239)
            Me.ParametrosDataGridView.TabIndex = 35
            '
            'NombreParametroDataGridViewTextBoxColumn
            '
            Me.NombreParametroDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Parametro"
            Me.NombreParametroDataGridViewTextBoxColumn.HeaderText = "Nombre Parametro"
            Me.NombreParametroDataGridViewTextBoxColumn.Name = "NombreParametroDataGridViewTextBoxColumn"
            Me.NombreParametroDataGridViewTextBoxColumn.Width = 200
            '
            'ValorParametroDataGridViewTextBoxColumn
            '
            Me.ValorParametroDataGridViewTextBoxColumn.DataPropertyName = "Valor_Parametro"
            Me.ValorParametroDataGridViewTextBoxColumn.HeaderText = "Valor Parametro"
            Me.ValorParametroDataGridViewTextBoxColumn.Name = "ValorParametroDataGridViewTextBoxColumn"
            Me.ValorParametroDataGridViewTextBoxColumn.Width = 200
            '
            'DescripcionParametroDataGridViewTextBoxColumn
            '
            Me.DescripcionParametroDataGridViewTextBoxColumn.DataPropertyName = "Descripcion_Parametro"
            Me.DescripcionParametroDataGridViewTextBoxColumn.HeaderText = "Descripcion Parametro"
            Me.DescripcionParametroDataGridViewTextBoxColumn.Name = "DescripcionParametroDataGridViewTextBoxColumn"
            Me.DescripcionParametroDataGridViewTextBoxColumn.Width = 300
            '
            'TBLParametrosBindingSource
            '
            Me.TBLParametrosBindingSource.DataSource = GetType(DBImaging.SchemaConfig.TBL_ParametroDataTable)
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(612, 264)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(122, 30)
            Me.GuardarButton.TabIndex = 36
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(743, 264)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(122, 30)
            Me.CerrarButton.TabIndex = 37
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormParametros
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(880, 306)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.ParametrosDataGridView)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormParametros"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Parámetros Generales"
            CType(Me.ParametrosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.TBLParametrosBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ParametrosDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents NombreParametroDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ValorParametroDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DescripcionParametroDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents TBLParametrosBindingSource As System.Windows.Forms.BindingSource
    End Class
End Namespace