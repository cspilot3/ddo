Namespace Imaging.Carpeta_Unica.Forms.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCapturaTapa
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
            Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle31 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle32 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim Rango4 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.ObservacionesDesktopComboBoxControl = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.OficinasDesktopComboBoxControl = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.BtnCancelar = New System.Windows.Forms.Button()
            Me.RegistrosGroupBox = New System.Windows.Forms.GroupBox()
            Me.ContenedoresDataGridView = New System.Windows.Forms.DataGridView()
            Me.BtnGuardar = New System.Windows.Forms.Button()
            Me.LblCodOficina = New System.Windows.Forms.Label()
            Me.FechaProcesodateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaProcesolabel = New System.Windows.Forms.Label()
            Me.NumProductoTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.LblNumeroProducto = New System.Windows.Forms.Label()
            Me.LblCodObservacion = New System.Windows.Forms.Label()
            Me.Fecha_Proceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Numero_Producto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Observacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Proceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Oficina = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.GroupBox1.SuspendLayout()
            Me.RegistrosGroupBox.SuspendLayout()
            CType(Me.ContenedoresDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.ObservacionesDesktopComboBoxControl)
            Me.GroupBox1.Controls.Add(Me.OficinasDesktopComboBoxControl)
            Me.GroupBox1.Controls.Add(Me.BtnCancelar)
            Me.GroupBox1.Controls.Add(Me.RegistrosGroupBox)
            Me.GroupBox1.Controls.Add(Me.BtnGuardar)
            Me.GroupBox1.Controls.Add(Me.LblCodOficina)
            Me.GroupBox1.Controls.Add(Me.FechaProcesodateTimePicker)
            Me.GroupBox1.Controls.Add(Me.FechaProcesolabel)
            Me.GroupBox1.Controls.Add(Me.NumProductoTextBox)
            Me.GroupBox1.Controls.Add(Me.LblNumeroProducto)
            Me.GroupBox1.Controls.Add(Me.LblCodObservacion)
            Me.GroupBox1.Location = New System.Drawing.Point(6, 5)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(668, 428)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            '
            'ObservacionesDesktopComboBoxControl
            '
            Me.ObservacionesDesktopComboBoxControl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ObservacionesDesktopComboBoxControl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ObservacionesDesktopComboBoxControl.DisabledEnter = False
            Me.ObservacionesDesktopComboBoxControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ObservacionesDesktopComboBoxControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ObservacionesDesktopComboBoxControl.FormattingEnabled = True
            Me.ObservacionesDesktopComboBoxControl.Location = New System.Drawing.Point(446, 77)
            Me.ObservacionesDesktopComboBoxControl.Name = "ObservacionesDesktopComboBoxControl"
            Me.ObservacionesDesktopComboBoxControl.Size = New System.Drawing.Size(202, 21)
            Me.ObservacionesDesktopComboBoxControl.TabIndex = 4
            '
            'OficinasDesktopComboBoxControl
            '
            Me.OficinasDesktopComboBoxControl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OficinasDesktopComboBoxControl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OficinasDesktopComboBoxControl.DisabledEnter = False
            Me.OficinasDesktopComboBoxControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OficinasDesktopComboBoxControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OficinasDesktopComboBoxControl.FormattingEnabled = True
            Me.OficinasDesktopComboBoxControl.Location = New System.Drawing.Point(446, 30)
            Me.OficinasDesktopComboBoxControl.Name = "OficinasDesktopComboBoxControl"
            Me.OficinasDesktopComboBoxControl.Size = New System.Drawing.Size(202, 21)
            Me.OficinasDesktopComboBoxControl.TabIndex = 3
            '
            'BtnCancelar
            '
            Me.BtnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnCancelar.Image = Global.BCS.Plugin.My.Resources.Resources.btnSalir
            Me.BtnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnCancelar.Location = New System.Drawing.Point(392, 376)
            Me.BtnCancelar.Name = "BtnCancelar"
            Me.BtnCancelar.Size = New System.Drawing.Size(80, 34)
            Me.BtnCancelar.TabIndex = 7
            Me.BtnCancelar.Text = "Cancelar"
            Me.BtnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BtnCancelar.UseVisualStyleBackColor = True
            '
            'RegistrosGroupBox
            '
            Me.RegistrosGroupBox.Controls.Add(Me.ContenedoresDataGridView)
            Me.RegistrosGroupBox.Location = New System.Drawing.Point(10, 146)
            Me.RegistrosGroupBox.Name = "RegistrosGroupBox"
            Me.RegistrosGroupBox.Size = New System.Drawing.Size(652, 215)
            Me.RegistrosGroupBox.TabIndex = 38
            Me.RegistrosGroupBox.TabStop = False
            '
            'ContenedoresDataGridView
            '
            Me.ContenedoresDataGridView.AllowUserToAddRows = False
            Me.ContenedoresDataGridView.AllowUserToDeleteRows = False
            DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ContenedoresDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle25
            Me.ContenedoresDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ContenedoresDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Fecha_Proceso, Me.Numero_Producto, Me.Observacion, Me.Proceso, Me.Oficina})
            DataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle31.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.ContenedoresDataGridView.DefaultCellStyle = DataGridViewCellStyle31
            Me.ContenedoresDataGridView.Location = New System.Drawing.Point(9, 13)
            Me.ContenedoresDataGridView.Name = "ContenedoresDataGridView"
            Me.ContenedoresDataGridView.ReadOnly = True
            DataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle32.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle32.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ContenedoresDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle32
            Me.ContenedoresDataGridView.Size = New System.Drawing.Size(632, 179)
            Me.ContenedoresDataGridView.TabIndex = 2
            '
            'BtnGuardar
            '
            Me.BtnGuardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnGuardar.Image = Global.BCS.Plugin.My.Resources.Resources.btnGuardar
            Me.BtnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnGuardar.Location = New System.Drawing.Point(189, 376)
            Me.BtnGuardar.Name = "BtnGuardar"
            Me.BtnGuardar.Size = New System.Drawing.Size(79, 34)
            Me.BtnGuardar.TabIndex = 6
            Me.BtnGuardar.Text = "Guardar"
            Me.BtnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BtnGuardar.UseVisualStyleBackColor = True
            '
            'LblCodOficina
            '
            Me.LblCodOficina.AutoSize = True
            Me.LblCodOficina.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblCodOficina.Location = New System.Drawing.Point(389, 36)
            Me.LblCodOficina.Name = "LblCodOficina"
            Me.LblCodOficina.Size = New System.Drawing.Size(51, 13)
            Me.LblCodOficina.TabIndex = 37
            Me.LblCodOficina.Text = "Oficina:"
            '
            'FechaProcesodateTimePicker
            '
            Me.FechaProcesodateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesodateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaProcesodateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaProcesodateTimePicker.Location = New System.Drawing.Point(123, 33)
            Me.FechaProcesodateTimePicker.Name = "FechaProcesodateTimePicker"
            Me.FechaProcesodateTimePicker.Size = New System.Drawing.Size(82, 20)
            Me.FechaProcesodateTimePicker.TabIndex = 1
            '
            'FechaProcesolabel
            '
            Me.FechaProcesolabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesolabel.AutoSize = True
            Me.FechaProcesolabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesolabel.Location = New System.Drawing.Point(13, 39)
            Me.FechaProcesolabel.Name = "FechaProcesolabel"
            Me.FechaProcesolabel.Size = New System.Drawing.Size(96, 13)
            Me.FechaProcesolabel.TabIndex = 35
            Me.FechaProcesolabel.Text = "Fecha Proceso:"
            '
            'NumProductoTextBox
            '
            Me.NumProductoTextBox._Obligatorio = False
            Me.NumProductoTextBox._PermitePegar = False
            Me.NumProductoTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.NumProductoTextBox.Cantidad_Decimales = CType(0, Short)
            Me.NumProductoTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.NumProductoTextBox.DateFormat = Nothing
            Me.NumProductoTextBox.DisabledEnter = True
            Me.NumProductoTextBox.DisabledTab = False
            Me.NumProductoTextBox.EnabledShortCuts = False
            Me.NumProductoTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.NumProductoTextBox.FocusOut = System.Drawing.Color.White
            Me.NumProductoTextBox.Location = New System.Drawing.Point(123, 73)
            Me.NumProductoTextBox.MaskedTextBox_Property = ""
            Me.NumProductoTextBox.MaximumLength = CType(20, Short)
            Me.NumProductoTextBox.MinimumLength = CType(0, Short)
            Me.NumProductoTextBox.Name = "NumProductoTextBox"
            Me.NumProductoTextBox.Obligatorio = False
            Me.NumProductoTextBox.permitePegar = False
            Rango4.MaxValue = 9.2233720368547758E+18R
            Rango4.MinValue = 0.0R
            Me.NumProductoTextBox.Rango = Rango4
            Me.NumProductoTextBox.Size = New System.Drawing.Size(196, 20)
            Me.NumProductoTextBox.TabIndex = 2
            Me.NumProductoTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Numerico
            Me.NumProductoTextBox.Usa_Decimales = False
            Me.NumProductoTextBox.Validos_Cantidad_Puntos = False
            '
            'LblNumeroProducto
            '
            Me.LblNumeroProducto.AutoSize = True
            Me.LblNumeroProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblNumeroProducto.Location = New System.Drawing.Point(13, 80)
            Me.LblNumeroProducto.Name = "LblNumeroProducto"
            Me.LblNumeroProducto.Size = New System.Drawing.Size(95, 13)
            Me.LblNumeroProducto.TabIndex = 33
            Me.LblNumeroProducto.Text = "Num. Producto:"
            '
            'LblCodObservacion
            '
            Me.LblCodObservacion.AutoSize = True
            Me.LblCodObservacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblCodObservacion.Location = New System.Drawing.Point(357, 80)
            Me.LblCodObservacion.Name = "LblCodObservacion"
            Me.LblCodObservacion.Size = New System.Drawing.Size(82, 13)
            Me.LblCodObservacion.TabIndex = 0
            Me.LblCodObservacion.Text = "Observación:"
            '
            'Fecha_Proceso
            '
            Me.Fecha_Proceso.DataPropertyName = "Fecha_Proceso"
            DataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Fecha_Proceso.DefaultCellStyle = DataGridViewCellStyle26
            Me.Fecha_Proceso.HeaderText = "FECHA"
            Me.Fecha_Proceso.Name = "Fecha_Proceso"
            Me.Fecha_Proceso.ReadOnly = True
            Me.Fecha_Proceso.Width = 80
            '
            'Numero_Producto
            '
            Me.Numero_Producto.DataPropertyName = "Numero_Producto"
            DataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Numero_Producto.DefaultCellStyle = DataGridViewCellStyle27
            Me.Numero_Producto.HeaderText = "NUM. PRODUCTO"
            Me.Numero_Producto.Name = "Numero_Producto"
            Me.Numero_Producto.ReadOnly = True
            '
            'Observacion
            '
            Me.Observacion.DataPropertyName = "Observacion"
            DataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Observacion.DefaultCellStyle = DataGridViewCellStyle28
            Me.Observacion.HeaderText = "OBSERVACION"
            Me.Observacion.Name = "Observacion"
            Me.Observacion.ReadOnly = True
            Me.Observacion.Width = 150
            '
            'Proceso
            '
            Me.Proceso.DataPropertyName = "Proceso"
            DataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle29.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Proceso.DefaultCellStyle = DataGridViewCellStyle29
            Me.Proceso.HeaderText = "PROCESO"
            Me.Proceso.Name = "Proceso"
            Me.Proceso.ReadOnly = True
            Me.Proceso.Width = 130
            '
            'Oficina
            '
            Me.Oficina.DataPropertyName = "Oficina"
            DataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle30.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Oficina.DefaultCellStyle = DataGridViewCellStyle30
            Me.Oficina.HeaderText = "OFICINA"
            Me.Oficina.Name = "Oficina"
            Me.Oficina.ReadOnly = True
            Me.Oficina.Width = 130
            '
            'FormCapturaTapa
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(686, 445)
            Me.Controls.Add(Me.GroupBox1)
            Me.Name = "FormCapturaTapa"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Captura tapa"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.RegistrosGroupBox.ResumeLayout(False)
            CType(Me.ContenedoresDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents LblCodObservacion As System.Windows.Forms.Label
        Friend WithEvents LblNumeroProducto As System.Windows.Forms.Label
        Private WithEvents FechaProcesodateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaProcesolabel As System.Windows.Forms.Label
        Friend WithEvents LblCodOficina As System.Windows.Forms.Label
        Friend WithEvents RegistrosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ContenedoresDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents BtnGuardar As System.Windows.Forms.Button
        Friend WithEvents BtnCancelar As System.Windows.Forms.Button
        Friend WithEvents ObservacionesDesktopComboBoxControl As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents OficinasDesktopComboBoxControl As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents NumProductoTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Fecha_Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Numero_Producto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Observacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Oficina As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace