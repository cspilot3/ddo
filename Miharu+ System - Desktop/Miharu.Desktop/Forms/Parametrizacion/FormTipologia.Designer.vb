Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormTipologia
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormTipologia))
            Me.NuevoDocumentoButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.DocumentoDataGridView = New System.Windows.Forms.DataGridView()
            Me.id_Tipologia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Tipologia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            CType(Me.DocumentoDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'NuevoDocumentoButton
            '
            Me.NuevoDocumentoButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NuevoDocumentoButton.Image = CType(resources.GetObject("NuevoDocumentoButton.Image"), System.Drawing.Image)
            Me.NuevoDocumentoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.NuevoDocumentoButton.Location = New System.Drawing.Point(12, 458)
            Me.NuevoDocumentoButton.Name = "NuevoDocumentoButton"
            Me.NuevoDocumentoButton.Size = New System.Drawing.Size(145, 33)
            Me.NuevoDocumentoButton.TabIndex = 38
            Me.NuevoDocumentoButton.Text = "Nueva Tipologia"
            Me.NuevoDocumentoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.NuevoDocumentoButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = CType(resources.GetObject("CerrarButton.Image"), System.Drawing.Image)
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(318, 457)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 33)
            Me.CerrarButton.TabIndex = 37
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'DocumentoDataGridView
            '
            Me.DocumentoDataGridView.AllowUserToAddRows = False
            Me.DocumentoDataGridView.AllowUserToDeleteRows = False
            Me.DocumentoDataGridView.AllowUserToOrderColumns = True
            Me.DocumentoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DocumentoDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_Tipologia, Me.Nombre_Tipologia})
            Me.DocumentoDataGridView.Location = New System.Drawing.Point(12, 12)
            Me.DocumentoDataGridView.MultiSelect = False
            Me.DocumentoDataGridView.Name = "DocumentoDataGridView"
            Me.DocumentoDataGridView.ReadOnly = True
            Me.DocumentoDataGridView.Size = New System.Drawing.Size(394, 440)
            Me.DocumentoDataGridView.TabIndex = 35
            '
            'id_Tipologia
            '
            Me.id_Tipologia.DataPropertyName = "id_Tipologia"
            Me.id_Tipologia.HeaderText = "Id Tipologia"
            Me.id_Tipologia.Name = "id_Tipologia"
            Me.id_Tipologia.ReadOnly = True
            '
            'Nombre_Tipologia
            '
            Me.Nombre_Tipologia.DataPropertyName = "Nombre_Tipologia"
            Me.Nombre_Tipologia.HeaderText = "Tipologia"
            Me.Nombre_Tipologia.Name = "Nombre_Tipologia"
            Me.Nombre_Tipologia.ReadOnly = True
            Me.Nombre_Tipologia.Width = 250
            '
            'FormTipologia
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(420, 502)
            Me.Controls.Add(Me.NuevoDocumentoButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.DocumentoDataGridView)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormTipologia"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Tipologia"
            CType(Me.DocumentoDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents NuevoDocumentoButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents DocumentoDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents id_Tipologia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Tipologia As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace