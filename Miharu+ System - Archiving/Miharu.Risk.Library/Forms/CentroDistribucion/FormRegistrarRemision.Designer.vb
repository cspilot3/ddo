Imports Miharu.Desktop.Controls.DesktopDataGridView

Namespace Forms.CentroDistribucion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormRegistrarRemision
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
            Me.lblCarpetas = New System.Windows.Forms.Label()
            Me.lblDocumentos = New System.Windows.Forms.Label()
            Me.lblCBarras1 = New System.Windows.Forms.Label()
            Me.btnSacarCarpeta = New System.Windows.Forms.Button()
            Me.gridRemision = New System.Windows.Forms.DataGridView()
            Me.lblCCBarras2 = New System.Windows.Forms.Label()
            Me.btnCerrar = New System.Windows.Forms.Button()
            Me.lblNumeroCarpetas = New System.Windows.Forms.Label()
            Me.lblNumeroDocs = New System.Windows.Forms.Label()
            Me.txtCBarras1 = New System.Windows.Forms.TextBox()
            Me.txtCBarras2 = New System.Windows.Forms.TextBox()
            Me.ColumnOT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnExpediente = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnFolder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnFile = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnRegistroTipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Cbarras_Folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarras_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnDocumento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EnRemision = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            CType(Me.gridRemision, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lblCarpetas
            '
            Me.lblCarpetas.AutoSize = True
            Me.lblCarpetas.Location = New System.Drawing.Point(12, 23)
            Me.lblCarpetas.Name = "lblCarpetas"
            Me.lblCarpetas.Size = New System.Drawing.Size(61, 13)
            Me.lblCarpetas.TabIndex = 0
            Me.lblCarpetas.Text = "Carpetas:"
            '
            'lblDocumentos
            '
            Me.lblDocumentos.AutoSize = True
            Me.lblDocumentos.Location = New System.Drawing.Point(12, 53)
            Me.lblDocumentos.Name = "lblDocumentos"
            Me.lblDocumentos.Size = New System.Drawing.Size(81, 13)
            Me.lblDocumentos.TabIndex = 1
            Me.lblDocumentos.Text = "Documentos:"
            '
            'lblCBarras1
            '
            Me.lblCBarras1.AutoSize = True
            Me.lblCBarras1.Location = New System.Drawing.Point(12, 84)
            Me.lblCBarras1.Name = "lblCBarras1"
            Me.lblCBarras1.Size = New System.Drawing.Size(102, 13)
            Me.lblCBarras1.TabIndex = 2
            Me.lblCBarras1.Text = "Código de barras"
            '
            'btnSacarCarpeta
            '
            Me.btnSacarCarpeta.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir1
            Me.btnSacarCarpeta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnSacarCarpeta.Location = New System.Drawing.Point(12, 299)
            Me.btnSacarCarpeta.Name = "btnSacarCarpeta"
            Me.btnSacarCarpeta.Size = New System.Drawing.Size(112, 23)
            Me.btnSacarCarpeta.TabIndex = 3
            Me.btnSacarCarpeta.Text = "Sacar Carpeta"
            Me.btnSacarCarpeta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnSacarCarpeta.UseMnemonic = False
            Me.btnSacarCarpeta.UseVisualStyleBackColor = True
            Me.btnSacarCarpeta.Visible = False
            '
            'gridRemision
            '
            Me.gridRemision.AllowUserToAddRows = False
            Me.gridRemision.AllowUserToResizeColumns = False
            Me.gridRemision.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.gridRemision.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnOT, Me.ColumnExpediente, Me.ColumnFolder, Me.ColumnFile, Me.ColumnRegistroTipo, Me.Cbarras_Folder, Me.CBarras_File, Me.ColumnDocumento, Me.EnRemision})
            Me.gridRemision.Location = New System.Drawing.Point(12, 133)
            Me.gridRemision.Name = "gridRemision"
            Me.gridRemision.ReadOnly = True
            Me.gridRemision.RowHeadersWidth = 10
            Me.gridRemision.Size = New System.Drawing.Size(358, 150)
            Me.gridRemision.TabIndex = 7
            '
            'lblCCBarras2
            '
            Me.lblCCBarras2.AutoSize = True
            Me.lblCCBarras2.Location = New System.Drawing.Point(187, 84)
            Me.lblCCBarras2.Name = "lblCCBarras2"
            Me.lblCCBarras2.Size = New System.Drawing.Size(102, 13)
            Me.lblCCBarras2.TabIndex = 9
            Me.lblCCBarras2.Text = "Código de barras"
            '
            'btnCerrar
            '
            Me.btnCerrar.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCerrar.Location = New System.Drawing.Point(282, 299)
            Me.btnCerrar.Name = "btnCerrar"
            Me.btnCerrar.Size = New System.Drawing.Size(75, 23)
            Me.btnCerrar.TabIndex = 4
            Me.btnCerrar.Text = "&Cerrar"
            Me.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnCerrar.UseVisualStyleBackColor = True
            Me.btnCerrar.Visible = False
            '
            'lblNumeroCarpetas
            '
            Me.lblNumeroCarpetas.AutoSize = True
            Me.lblNumeroCarpetas.ForeColor = System.Drawing.Color.Green
            Me.lblNumeroCarpetas.Location = New System.Drawing.Point(97, 23)
            Me.lblNumeroCarpetas.Name = "lblNumeroCarpetas"
            Me.lblNumeroCarpetas.Size = New System.Drawing.Size(58, 13)
            Me.lblNumeroCarpetas.TabIndex = 16
            Me.lblNumeroCarpetas.Text = "Carpetas"
            '
            'lblNumeroDocs
            '
            Me.lblNumeroDocs.AutoSize = True
            Me.lblNumeroDocs.ForeColor = System.Drawing.Color.Green
            Me.lblNumeroDocs.Location = New System.Drawing.Point(97, 53)
            Me.lblNumeroDocs.Name = "lblNumeroDocs"
            Me.lblNumeroDocs.Size = New System.Drawing.Size(78, 13)
            Me.lblNumeroDocs.TabIndex = 17
            Me.lblNumeroDocs.Text = "Documentos"
            '
            'txtCBarras1
            '
            Me.txtCBarras1.Location = New System.Drawing.Point(13, 103)
            Me.txtCBarras1.Name = "txtCBarras1"
            Me.txtCBarras1.ShortcutsEnabled = False
            Me.txtCBarras1.Size = New System.Drawing.Size(142, 21)
            Me.txtCBarras1.TabIndex = 1
            '
            'txtCBarras2
            '
            Me.txtCBarras2.Location = New System.Drawing.Point(188, 103)
            Me.txtCBarras2.Name = "txtCBarras2"
            Me.txtCBarras2.ShortcutsEnabled = False
            Me.txtCBarras2.Size = New System.Drawing.Size(128, 21)
            Me.txtCBarras2.TabIndex = 2
            '
            'ColumnOT
            '
            Me.ColumnOT.DataPropertyName = "fk_OT"
            Me.ColumnOT.HeaderText = "OT"
            Me.ColumnOT.Name = "ColumnOT"
            Me.ColumnOT.ReadOnly = True
            Me.ColumnOT.Visible = False
            '
            'ColumnExpediente
            '
            Me.ColumnExpediente.DataPropertyName = "fk_expediente"
            Me.ColumnExpediente.HeaderText = "Expediente"
            Me.ColumnExpediente.Name = "ColumnExpediente"
            Me.ColumnExpediente.ReadOnly = True
            Me.ColumnExpediente.Visible = False
            '
            'ColumnFolder
            '
            Me.ColumnFolder.DataPropertyName = "fk_Folder"
            Me.ColumnFolder.HeaderText = "Folder"
            Me.ColumnFolder.Name = "ColumnFolder"
            Me.ColumnFolder.ReadOnly = True
            Me.ColumnFolder.Visible = False
            '
            'ColumnFile
            '
            Me.ColumnFile.DataPropertyName = "id_File"
            Me.ColumnFile.HeaderText = "File"
            Me.ColumnFile.Name = "ColumnFile"
            Me.ColumnFile.ReadOnly = True
            Me.ColumnFile.Visible = False
            '
            'ColumnRegistroTipo
            '
            Me.ColumnRegistroTipo.DataPropertyName = "fk_Registro_Tipo"
            Me.ColumnRegistroTipo.HeaderText = "Registro_Tipo"
            Me.ColumnRegistroTipo.Name = "ColumnRegistroTipo"
            Me.ColumnRegistroTipo.ReadOnly = True
            Me.ColumnRegistroTipo.Visible = False
            '
            'Cbarras_Folder
            '
            Me.Cbarras_Folder.DataPropertyName = "CBarras_Folder"
            Me.Cbarras_Folder.HeaderText = "Cbarras Folder"
            Me.Cbarras_Folder.Name = "Cbarras_Folder"
            Me.Cbarras_Folder.ReadOnly = True
            Me.Cbarras_Folder.Visible = False
            '
            'CBarras_File
            '
            Me.CBarras_File.DataPropertyName = "CBarras_File"
            Me.CBarras_File.HeaderText = "CBarras Documento"
            Me.CBarras_File.Name = "CBarras_File"
            Me.CBarras_File.ReadOnly = True
            '
            'ColumnDocumento
            '
            Me.ColumnDocumento.DataPropertyName = "Nombre_Documento"
            Me.ColumnDocumento.FillWeight = 189.6907!
            Me.ColumnDocumento.HeaderText = "Documento"
            Me.ColumnDocumento.Name = "ColumnDocumento"
            Me.ColumnDocumento.ReadOnly = True
            Me.ColumnDocumento.Width = 200
            '
            'EnRemision
            '
            Me.EnRemision.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.EnRemision.DataPropertyName = "EnRemision"
            Me.EnRemision.FillWeight = 10.30928!
            Me.EnRemision.HeaderText = "R"
            Me.EnRemision.Name = "EnRemision"
            Me.EnRemision.ReadOnly = True
            Me.EnRemision.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.EnRemision.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            '
            'FormRegistrarRemision
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(382, 344)
            Me.Controls.Add(Me.txtCBarras2)
            Me.Controls.Add(Me.txtCBarras1)
            Me.Controls.Add(Me.lblNumeroDocs)
            Me.Controls.Add(Me.lblNumeroCarpetas)
            Me.Controls.Add(Me.lblCarpetas)
            Me.Controls.Add(Me.lblDocumentos)
            Me.Controls.Add(Me.btnCerrar)
            Me.Controls.Add(Me.lblCCBarras2)
            Me.Controls.Add(Me.gridRemision)
            Me.Controls.Add(Me.btnSacarCarpeta)
            Me.Controls.Add(Me.lblCBarras1)
            Me.Name = "FormRegistrarRemision"
            Me.Text = "Remisión"
            CType(Me.gridRemision, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents lblCarpetas As System.Windows.Forms.Label
        Friend WithEvents lblDocumentos As System.Windows.Forms.Label
        Friend WithEvents lblCBarras1 As System.Windows.Forms.Label
        Friend WithEvents btnSacarCarpeta As System.Windows.Forms.Button
        Friend WithEvents gridRemision As System.Windows.Forms.DataGridView
        Friend WithEvents lblCCBarras2 As System.Windows.Forms.Label
        Friend WithEvents btnCerrar As System.Windows.Forms.Button
        Friend WithEvents lblNumeroCarpetas As System.Windows.Forms.Label
        Friend WithEvents lblNumeroDocs As System.Windows.Forms.Label
        Friend WithEvents txtCBarras1 As System.Windows.Forms.TextBox
        Friend WithEvents txtCBarras2 As System.Windows.Forms.TextBox
        Friend WithEvents ColumnOT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnExpediente As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnFolder As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnFile As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnRegistroTipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Cbarras_Folder As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarras_File As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnDocumento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EnRemision As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class

End Namespace
