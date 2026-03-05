<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCentralizador
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
        Me.cbxFechaRecaudo = New System.Windows.Forms.ComboBox()
        Me.lblMedio = New System.Windows.Forms.Label()
        Me.lblFechaRecaudo = New System.Windows.Forms.Label()
        Me.DGVTotalCiald = New System.Windows.Forms.DataGridView()
        Me.FechaRecaudo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CialdTot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RegistrosCiald = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Relevados = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Centralizados = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Faltantes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnGenerar = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.AceptacionMedios = New System.Windows.Forms.CheckBox()
        Me.EntregaDenuncios = New System.Windows.Forms.CheckBox()
        Me.Anexo5 = New System.Windows.Forms.CheckBox()
        Me.Anexo6 = New System.Windows.Forms.CheckBox()
        Me.AnexoF11 = New System.Windows.Forms.CheckBox()
        Me.BtnBuscar = New System.Windows.Forms.Button()
        Me.cbxMedioMagnetico = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        CType(Me.DGVTotalCiald, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbxFechaRecaudo
        '
        Me.cbxFechaRecaudo.FormattingEnabled = True
        Me.cbxFechaRecaudo.Location = New System.Drawing.Point(28, 33)
        Me.cbxFechaRecaudo.Name = "cbxFechaRecaudo"
        Me.cbxFechaRecaudo.Size = New System.Drawing.Size(154, 21)
        Me.cbxFechaRecaudo.TabIndex = 27
        '
        'lblMedio
        '
        Me.lblMedio.AutoSize = True
        Me.lblMedio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMedio.Location = New System.Drawing.Point(229, 15)
        Me.lblMedio.Name = "lblMedio"
        Me.lblMedio.Size = New System.Drawing.Size(118, 15)
        Me.lblMedio.TabIndex = 30
        Me.lblMedio.Text = "Medio Magnetico"
        '
        'lblFechaRecaudo
        '
        Me.lblFechaRecaudo.AutoSize = True
        Me.lblFechaRecaudo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaRecaudo.Location = New System.Drawing.Point(32, 15)
        Me.lblFechaRecaudo.Name = "lblFechaRecaudo"
        Me.lblFechaRecaudo.Size = New System.Drawing.Size(111, 15)
        Me.lblFechaRecaudo.TabIndex = 29
        Me.lblFechaRecaudo.Text = "Fecha Recaudo:"
        '
        'DGVTotalCiald
        '
        Me.DGVTotalCiald.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGVTotalCiald.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTotalCiald.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FechaRecaudo, Me.CialdTot, Me.RegistrosCiald, Me.Relevados, Me.Centralizados, Me.Faltantes})
        Me.DGVTotalCiald.Location = New System.Drawing.Point(12, 160)
        Me.DGVTotalCiald.Name = "DGVTotalCiald"
        Me.DGVTotalCiald.ReadOnly = True
        Me.DGVTotalCiald.RowHeadersVisible = False
        Me.DGVTotalCiald.Size = New System.Drawing.Size(653, 288)
        Me.DGVTotalCiald.TabIndex = 31
        '
        'FechaRecaudo
        '
        Me.FechaRecaudo.HeaderText = "Fecha Recaudo"
        Me.FechaRecaudo.Name = "FechaRecaudo"
        Me.FechaRecaudo.ReadOnly = True
        '
        'CialdTot
        '
        Me.CialdTot.HeaderText = "Nombre Ciald"
        Me.CialdTot.Name = "CialdTot"
        Me.CialdTot.ReadOnly = True
        Me.CialdTot.Width = 200
        '
        'RegistrosCiald
        '
        Me.RegistrosCiald.HeaderText = "Registros Ciald"
        Me.RegistrosCiald.Name = "RegistrosCiald"
        Me.RegistrosCiald.ReadOnly = True
        Me.RegistrosCiald.Width = 80
        '
        'Relevados
        '
        Me.Relevados.HeaderText = "Relevados"
        Me.Relevados.Name = "Relevados"
        Me.Relevados.ReadOnly = True
        Me.Relevados.Width = 80
        '
        'Centralizados
        '
        Me.Centralizados.HeaderText = "Centralizados"
        Me.Centralizados.Name = "Centralizados"
        Me.Centralizados.ReadOnly = True
        Me.Centralizados.Width = 80
        '
        'Faltantes
        '
        Me.Faltantes.HeaderText = "Faltantes"
        Me.Faltantes.Name = "Faltantes"
        Me.Faltantes.ReadOnly = True
        Me.Faltantes.Width = 80
        '
        'BtnGenerar
        '
        Me.BtnGenerar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGenerar.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.auto_indexar
        Me.BtnGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnGenerar.Location = New System.Drawing.Point(481, 85)
        Me.BtnGenerar.Name = "BtnGenerar"
        Me.BtnGenerar.Size = New System.Drawing.Size(153, 41)
        Me.BtnGenerar.TabIndex = 34
        Me.BtnGenerar.Text = "     Generar Reportes"
        Me.BtnGenerar.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.AceptacionMedios)
        Me.GroupBox4.Controls.Add(Me.EntregaDenuncios)
        Me.GroupBox4.Controls.Add(Me.Anexo5)
        Me.GroupBox4.Controls.Add(Me.Anexo6)
        Me.GroupBox4.Controls.Add(Me.AnexoF11)
        Me.GroupBox4.Location = New System.Drawing.Point(17, 69)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(362, 85)
        Me.GroupBox4.TabIndex = 35
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Reportes a generar Exportar"
        '
        'AceptacionMedios
        '
        Me.AceptacionMedios.AutoSize = True
        Me.AceptacionMedios.Location = New System.Drawing.Point(215, 53)
        Me.AceptacionMedios.Name = "AceptacionMedios"
        Me.AceptacionMedios.Size = New System.Drawing.Size(132, 17)
        Me.AceptacionMedios.TabIndex = 4
        Me.AceptacionMedios.Text = "Aceptacion de Medios"
        Me.AceptacionMedios.UseVisualStyleBackColor = True
        '
        'EntregaDenuncios
        '
        Me.EntregaDenuncios.AutoSize = True
        Me.EntregaDenuncios.Location = New System.Drawing.Point(16, 53)
        Me.EntregaDenuncios.Name = "EntregaDenuncios"
        Me.EntregaDenuncios.Size = New System.Drawing.Size(187, 17)
        Me.EntregaDenuncios.TabIndex = 3
        Me.EntregaDenuncios.Text = "Entrega de denuncios autorizados"
        Me.EntregaDenuncios.UseVisualStyleBackColor = True
        '
        'Anexo5
        '
        Me.Anexo5.AutoSize = True
        Me.Anexo5.Location = New System.Drawing.Point(138, 30)
        Me.Anexo5.Name = "Anexo5"
        Me.Anexo5.Size = New System.Drawing.Size(65, 17)
        Me.Anexo5.TabIndex = 2
        Me.Anexo5.Text = "Anexo 5"
        Me.Anexo5.UseVisualStyleBackColor = True
        '
        'Anexo6
        '
        Me.Anexo6.AutoSize = True
        Me.Anexo6.Location = New System.Drawing.Point(265, 29)
        Me.Anexo6.Name = "Anexo6"
        Me.Anexo6.Size = New System.Drawing.Size(65, 17)
        Me.Anexo6.TabIndex = 1
        Me.Anexo6.Text = "Anexo 6"
        Me.Anexo6.UseVisualStyleBackColor = True
        '
        'AnexoF11
        '
        Me.AnexoF11.AutoSize = True
        Me.AnexoF11.Location = New System.Drawing.Point(16, 30)
        Me.AnexoF11.Name = "AnexoF11"
        Me.AnexoF11.Size = New System.Drawing.Size(77, 17)
        Me.AnexoF11.TabIndex = 0
        Me.AnexoF11.Text = "Anexo F11"
        Me.AnexoF11.UseVisualStyleBackColor = True
        '
        'BtnBuscar
        '
        Me.BtnBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBuscar.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnBuscar
        Me.BtnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnBuscar.Location = New System.Drawing.Point(481, 15)
        Me.BtnBuscar.Name = "BtnBuscar"
        Me.BtnBuscar.Size = New System.Drawing.Size(153, 39)
        Me.BtnBuscar.TabIndex = 36
        Me.BtnBuscar.Text = "   Buscar"
        Me.BtnBuscar.UseVisualStyleBackColor = True
        '
        'cbxMedioMagnetico
        '
        Me.cbxMedioMagnetico.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbxMedioMagnetico.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbxMedioMagnetico.DisabledEnter = False
        Me.cbxMedioMagnetico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxMedioMagnetico.fk_Campo = 0
        Me.cbxMedioMagnetico.fk_Documento = 0
        Me.cbxMedioMagnetico.fk_Validacion = 0
        Me.cbxMedioMagnetico.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cbxMedioMagnetico.FormattingEnabled = True
        Me.cbxMedioMagnetico.Location = New System.Drawing.Point(225, 33)
        Me.cbxMedioMagnetico.Name = "cbxMedioMagnetico"
        Me.cbxMedioMagnetico.Size = New System.Drawing.Size(154, 21)
        Me.cbxMedioMagnetico.TabIndex = 37
        '
        'FormCentralizador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(676, 460)
        Me.Controls.Add(Me.cbxMedioMagnetico)
        Me.Controls.Add(Me.BtnBuscar)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.BtnGenerar)
        Me.Controls.Add(Me.DGVTotalCiald)
        Me.Controls.Add(Me.cbxFechaRecaudo)
        Me.Controls.Add(Me.lblMedio)
        Me.Controls.Add(Me.lblFechaRecaudo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormCentralizador"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generación de Anexos"
        CType(Me.DGVTotalCiald, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cbxFechaRecaudo As ComboBox
    Friend WithEvents lblMedio As Label
    Friend WithEvents lblFechaRecaudo As Label
    Friend WithEvents DGVTotalCiald As DataGridView
    Friend WithEvents FechaRecaudo As DataGridViewTextBoxColumn
    Friend WithEvents CialdTot As DataGridViewTextBoxColumn
    Friend WithEvents RegistrosCiald As DataGridViewTextBoxColumn
    Friend WithEvents Relevados As DataGridViewTextBoxColumn
    Friend WithEvents Centralizados As DataGridViewTextBoxColumn
    Friend WithEvents Faltantes As DataGridViewTextBoxColumn
    Friend WithEvents BtnGenerar As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents EntregaDenuncios As CheckBox
    Friend WithEvents Anexo5 As CheckBox
    Friend WithEvents Anexo6 As CheckBox
    Friend WithEvents AnexoF11 As CheckBox
    Friend WithEvents BtnBuscar As Button
    Friend WithEvents AceptacionMedios As CheckBox
    Friend WithEvents cbxMedioMagnetico As Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
End Class
