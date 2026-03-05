Namespace Imaging.Forms.Parametrización
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNovedades
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
            Me.components = New System.ComponentModel.Container()
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNovedades))
            Me.NovedadDataGridView = New System.Windows.Forms.DataGridView()
            Me.PAGetNovedadesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.DsNovedades = New Banagrario.Plugin.DsNovedades()
            Me.NuevoDocumentoButton = New System.Windows.Forms.Button()
            Me.PA_Get_NovedadesTableAdapter = New Banagrario.Plugin.DsNovedadesTableAdapters.PA_Get_NovedadesTableAdapter()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.id_NovedadDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NovedadDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EliminadoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            CType(Me.NovedadDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PAGetNovedadesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DsNovedades, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'NovedadDataGridView
            '
            Me.NovedadDataGridView.AllowUserToAddRows = False
            Me.NovedadDataGridView.AllowUserToDeleteRows = False
            Me.NovedadDataGridView.AutoGenerateColumns = False
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.NovedadDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.NovedadDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.NovedadDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NovedadDataGridViewTextBoxColumn, Me.EliminadoDataGridViewCheckBoxColumn, Me.id_NovedadDataGridViewTextBoxColumn})
            Me.NovedadDataGridView.DataSource = Me.PAGetNovedadesBindingSource
            Me.NovedadDataGridView.Location = New System.Drawing.Point(21, 27)
            Me.NovedadDataGridView.MultiSelect = False
            Me.NovedadDataGridView.Name = "NovedadDataGridView"
            Me.NovedadDataGridView.ReadOnly = True
            Me.NovedadDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.NovedadDataGridView.Size = New System.Drawing.Size(555, 181)
            Me.NovedadDataGridView.TabIndex = 33
            '
            'PAGetNovedadesBindingSource
            '
            Me.PAGetNovedadesBindingSource.DataMember = "PA_Get_Novedades"
            Me.PAGetNovedadesBindingSource.DataSource = Me.DsNovedades
            '
            'DsNovedades
            '
            Me.DsNovedades.DataSetName = "DsNovedades"
            Me.DsNovedades.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'NuevoDocumentoButton
            '
            Me.NuevoDocumentoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.NuevoDocumentoButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NuevoDocumentoButton.Image = CType(resources.GetObject("NuevoDocumentoButton.Image"), System.Drawing.Image)
            Me.NuevoDocumentoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.NuevoDocumentoButton.Location = New System.Drawing.Point(21, 238)
            Me.NuevoDocumentoButton.Name = "NuevoDocumentoButton"
            Me.NuevoDocumentoButton.Size = New System.Drawing.Size(145, 33)
            Me.NuevoDocumentoButton.TabIndex = 34
            Me.NuevoDocumentoButton.Text = "Nueva Novedad  "
            Me.NuevoDocumentoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.NuevoDocumentoButton.UseVisualStyleBackColor = True
            '
            'PA_Get_NovedadesTableAdapter
            '
            Me.PA_Get_NovedadesTableAdapter.ClearBeforeFill = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(483, 241)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 36
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'id_NovedadDataGridViewTextBoxColumn
            '
            Me.id_NovedadDataGridViewTextBoxColumn.DataPropertyName = "id_Novedad"
            Me.id_NovedadDataGridViewTextBoxColumn.HeaderText = "ID"
            Me.id_NovedadDataGridViewTextBoxColumn.Name = "id_NovedadDataGridViewTextBoxColumn"
            Me.id_NovedadDataGridViewTextBoxColumn.ReadOnly = True
            Me.id_NovedadDataGridViewTextBoxColumn.Visible = False
            '
            'NovedadDataGridViewTextBoxColumn
            '
            Me.NovedadDataGridViewTextBoxColumn.DataPropertyName = "Novedad"
            Me.NovedadDataGridViewTextBoxColumn.HeaderText = "Novedad"
            Me.NovedadDataGridViewTextBoxColumn.Name = "NovedadDataGridViewTextBoxColumn"
            Me.NovedadDataGridViewTextBoxColumn.ReadOnly = True
            Me.NovedadDataGridViewTextBoxColumn.Width = 410
            '
            'EliminadoDataGridViewCheckBoxColumn
            '
            Me.EliminadoDataGridViewCheckBoxColumn.DataPropertyName = "Eliminado"
            Me.EliminadoDataGridViewCheckBoxColumn.HeaderText = "Eliminado"
            Me.EliminadoDataGridViewCheckBoxColumn.Name = "EliminadoDataGridViewCheckBoxColumn"
            Me.EliminadoDataGridViewCheckBoxColumn.ReadOnly = True
            '
            'FormNovedades
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(599, 283)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.NuevoDocumentoButton)
            Me.Controls.Add(Me.NovedadDataGridView)
            Me.Name = "FormNovedades"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Novedades"
            CType(Me.NovedadDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PAGetNovedadesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DsNovedades, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents NovedadDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents NuevoDocumentoButton As System.Windows.Forms.Button
        Friend WithEvents DsNovedades As Banagrario.Plugin.DsNovedades
        Friend WithEvents PAGetNovedadesBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents PA_Get_NovedadesTableAdapter As Banagrario.Plugin.DsNovedadesTableAdapters.PA_Get_NovedadesTableAdapter
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents NovedadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EliminadoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents id_NovedadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace