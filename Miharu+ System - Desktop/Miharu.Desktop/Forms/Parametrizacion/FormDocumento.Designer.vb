Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormDocumento
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDocumento))
            Me.NuevoDocumentoButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.DocumentoDataGridView = New System.Windows.Forms.DataGridView()
            Me.fk_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Esquema = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Tipologia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Tipologia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Documento_Grupo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Eliminado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.EntidadComboBox = New System.Windows.Forms.ComboBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ProyectoComboBox = New System.Windows.Forms.ComboBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.EsquemaComboBox = New System.Windows.Forms.ComboBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.EliminarButton = New System.Windows.Forms.Button()
            CType(Me.DocumentoDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'NuevoDocumentoButton
            '
            Me.NuevoDocumentoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.NuevoDocumentoButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NuevoDocumentoButton.Image = CType(resources.GetObject("NuevoDocumentoButton.Image"), System.Drawing.Image)
            Me.NuevoDocumentoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.NuevoDocumentoButton.Location = New System.Drawing.Point(12, 457)
            Me.NuevoDocumentoButton.Name = "NuevoDocumentoButton"
            Me.NuevoDocumentoButton.Size = New System.Drawing.Size(145, 33)
            Me.NuevoDocumentoButton.TabIndex = 25
            Me.NuevoDocumentoButton.Text = "Nuevo Documento"
            Me.NuevoDocumentoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.NuevoDocumentoButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = CType(resources.GetObject("CerrarButton.Image"), System.Drawing.Image)
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(806, 457)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 33)
            Me.CerrarButton.TabIndex = 24
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'DocumentoDataGridView
            '
            Me.DocumentoDataGridView.AllowUserToAddRows = False
            Me.DocumentoDataGridView.AllowUserToDeleteRows = False
            Me.DocumentoDataGridView.AllowUserToOrderColumns = True
            Me.DocumentoDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DocumentoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DocumentoDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fk_Entidad, Me.fk_Proyecto, Me.fk_Esquema, Me.id_Documento, Me.Nombre_Documento, Me.fk_Tipologia, Me.Nombre_Tipologia, Me.fk_Documento_Grupo, Me.Eliminado})
            Me.DocumentoDataGridView.Location = New System.Drawing.Point(12, 53)
            Me.DocumentoDataGridView.MultiSelect = False
            Me.DocumentoDataGridView.Name = "DocumentoDataGridView"
            Me.DocumentoDataGridView.ReadOnly = True
            Me.DocumentoDataGridView.Size = New System.Drawing.Size(882, 398)
            Me.DocumentoDataGridView.TabIndex = 22
            '
            'fk_Entidad
            '
            Me.fk_Entidad.DataPropertyName = "fk_Entidad"
            Me.fk_Entidad.HeaderText = "id Entidad"
            Me.fk_Entidad.Name = "fk_Entidad"
            Me.fk_Entidad.ReadOnly = True
            Me.fk_Entidad.Visible = False
            '
            'fk_Proyecto
            '
            Me.fk_Proyecto.DataPropertyName = "fk_Proyecto"
            Me.fk_Proyecto.HeaderText = "id Proyecto"
            Me.fk_Proyecto.Name = "fk_Proyecto"
            Me.fk_Proyecto.ReadOnly = True
            Me.fk_Proyecto.Visible = False
            '
            'fk_Esquema
            '
            Me.fk_Esquema.DataPropertyName = "fk_Esquema"
            Me.fk_Esquema.HeaderText = "id Esquema"
            Me.fk_Esquema.Name = "fk_Esquema"
            Me.fk_Esquema.ReadOnly = True
            Me.fk_Esquema.Visible = False
            '
            'id_Documento
            '
            Me.id_Documento.DataPropertyName = "id_Documento"
            Me.id_Documento.HeaderText = "Id Documento"
            Me.id_Documento.Name = "id_Documento"
            Me.id_Documento.ReadOnly = True
            '
            'Nombre_Documento
            '
            Me.Nombre_Documento.DataPropertyName = "Nombre_Documento"
            Me.Nombre_Documento.HeaderText = "Nombre Documento"
            Me.Nombre_Documento.Name = "Nombre_Documento"
            Me.Nombre_Documento.ReadOnly = True
            Me.Nombre_Documento.Width = 400
            '
            'fk_Tipologia
            '
            Me.fk_Tipologia.DataPropertyName = "fk_Tipologia"
            Me.fk_Tipologia.HeaderText = "Id Tipologia"
            Me.fk_Tipologia.Name = "fk_Tipologia"
            Me.fk_Tipologia.ReadOnly = True
            Me.fk_Tipologia.Visible = False
            '
            'Nombre_Tipologia
            '
            Me.Nombre_Tipologia.DataPropertyName = "Nombre_Tipologia"
            Me.Nombre_Tipologia.HeaderText = "Tipologia"
            Me.Nombre_Tipologia.Name = "Nombre_Tipologia"
            Me.Nombre_Tipologia.ReadOnly = True
            Me.Nombre_Tipologia.Width = 200
            '
            'fk_Documento_Grupo
            '
            Me.fk_Documento_Grupo.DataPropertyName = "fk_Documento_Grupo"
            Me.fk_Documento_Grupo.HeaderText = "Documento Grupo"
            Me.fk_Documento_Grupo.Name = "fk_Documento_Grupo"
            Me.fk_Documento_Grupo.ReadOnly = True
            '
            'Eliminado
            '
            Me.Eliminado.DataPropertyName = "Eliminado"
            Me.Eliminado.HeaderText = "Eliminado"
            Me.Eliminado.Name = "Eliminado"
            Me.Eliminado.ReadOnly = True
            '
            'EntidadComboBox
            '
            Me.EntidadComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadComboBox.FormattingEnabled = True
            Me.EntidadComboBox.Location = New System.Drawing.Point(12, 26)
            Me.EntidadComboBox.Name = "EntidadComboBox"
            Me.EntidadComboBox.Size = New System.Drawing.Size(207, 21)
            Me.EntidadComboBox.TabIndex = 27
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(9, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(50, 13)
            Me.Label1.TabIndex = 23
            Me.Label1.Text = "Entidad"
            '
            'ProyectoComboBox
            '
            Me.ProyectoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoComboBox.FormattingEnabled = True
            Me.ProyectoComboBox.Location = New System.Drawing.Point(259, 26)
            Me.ProyectoComboBox.Name = "ProyectoComboBox"
            Me.ProyectoComboBox.Size = New System.Drawing.Size(207, 21)
            Me.ProyectoComboBox.TabIndex = 30
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(256, 9)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(57, 13)
            Me.Label2.TabIndex = 29
            Me.Label2.Text = "Proyecto"
            '
            'BuscarButton
            '
            Me.BuscarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarButton.Image = CType(resources.GetObject("BuscarButton.Image"), System.Drawing.Image)
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(806, 14)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(89, 31)
            Me.BuscarButton.TabIndex = 31
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'EsquemaComboBox
            '
            Me.EsquemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaComboBox.FormattingEnabled = True
            Me.EsquemaComboBox.Location = New System.Drawing.Point(506, 26)
            Me.EsquemaComboBox.Name = "EsquemaComboBox"
            Me.EsquemaComboBox.Size = New System.Drawing.Size(207, 21)
            Me.EsquemaComboBox.TabIndex = 33
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(503, 9)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(58, 13)
            Me.Label3.TabIndex = 32
            Me.Label3.Text = "Esquema"
            '
            'EliminarButton
            '
            Me.EliminarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.EliminarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EliminarButton.Image = CType(resources.GetObject("EliminarButton.Image"), System.Drawing.Image)
            Me.EliminarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.EliminarButton.Location = New System.Drawing.Point(163, 457)
            Me.EliminarButton.Name = "EliminarButton"
            Me.EliminarButton.Size = New System.Drawing.Size(158, 33)
            Me.EliminarButton.TabIndex = 34
            Me.EliminarButton.Text = "Habilitar Documento"
            Me.EliminarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.EliminarButton.UseVisualStyleBackColor = True
            '
            'FormDocumento
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(906, 502)
            Me.Controls.Add(Me.EliminarButton)
            Me.Controls.Add(Me.EsquemaComboBox)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.BuscarButton)
            Me.Controls.Add(Me.ProyectoComboBox)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.EntidadComboBox)
            Me.Controls.Add(Me.NuevoDocumentoButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.DocumentoDataGridView)
            Me.Name = "FormDocumento"
            Me.ShowIcon = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Documento"
            CType(Me.DocumentoDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents NuevoDocumentoButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents DocumentoDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents EntidadComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ProyectoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents EsquemaComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents fk_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Esquema As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Tipologia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Tipologia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Documento_Grupo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Eliminado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents EliminarButton As System.Windows.Forms.Button
    End Class
End Namespace