Namespace Procesos.Correos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEnviarCorreo
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.ParameterPanel = New System.Windows.Forms.Panel()
            Me.BuscarFechaButton = New System.Windows.Forms.Button()
            Me.OTDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.EnviarTodosCheckBox = New System.Windows.Forms.CheckBox()
            Me.lblOT = New System.Windows.Forms.Label()
            Me.FechaProcesoFinalLabel = New System.Windows.Forms.Label()
            Me.FechaProcesoFinalDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaProcesoLabel = New System.Windows.Forms.Label()
            Me.DataGridViewCorreos = New System.Windows.Forms.DataGridView()
            Me.FechaProceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Expediente = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Version = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreImagen = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Oficio = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Radicado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Correo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TipoNotificacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreNotificacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreCartaRespuesta = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EnviarCorreo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.fk_DocumentoPpal = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EnviarCorreoButton = New System.Windows.Forms.Button()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.TotalRegistrosTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.ParameterPanel.SuspendLayout()
            CType(Me.DataGridViewCorreos, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'ParameterPanel
            '
            Me.ParameterPanel.Controls.Add(Me.BuscarFechaButton)
            Me.ParameterPanel.Controls.Add(Me.OTDesktopComboBox)
            Me.ParameterPanel.Controls.Add(Me.EnviarTodosCheckBox)
            Me.ParameterPanel.Controls.Add(Me.lblOT)
            Me.ParameterPanel.Controls.Add(Me.FechaProcesoFinalLabel)
            Me.ParameterPanel.Controls.Add(Me.FechaProcesoFinalDateTimePicker)
            Me.ParameterPanel.Controls.Add(Me.FechaProcesoDateTimePicker)
            Me.ParameterPanel.Controls.Add(Me.FechaProcesoLabel)
            Me.ParameterPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.ParameterPanel.Location = New System.Drawing.Point(0, 0)
            Me.ParameterPanel.Name = "ParameterPanel"
            Me.ParameterPanel.Size = New System.Drawing.Size(1043, 122)
            Me.ParameterPanel.TabIndex = 1
            '
            'BuscarFechaButton
            '
            Me.BuscarFechaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarFechaButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarFechaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnBuscar
            Me.BuscarFechaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarFechaButton.Location = New System.Drawing.Point(932, 35)
            Me.BuscarFechaButton.Name = "BuscarFechaButton"
            Me.BuscarFechaButton.Size = New System.Drawing.Size(99, 36)
            Me.BuscarFechaButton.TabIndex = 33
            Me.BuscarFechaButton.Text = "Buscar   "
            Me.BuscarFechaButton.UseVisualStyleBackColor = True
            '
            'OTDesktopComboBox
            '
            Me.OTDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OTDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OTDesktopComboBox.DisabledEnter = False
            Me.OTDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OTDesktopComboBox.fk_Campo = 0
            Me.OTDesktopComboBox.fk_Documento = 0
            Me.OTDesktopComboBox.fk_Validacion = 0
            Me.OTDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OTDesktopComboBox.FormattingEnabled = True
            Me.OTDesktopComboBox.Location = New System.Drawing.Point(594, 45)
            Me.OTDesktopComboBox.Name = "OTDesktopComboBox"
            Me.OTDesktopComboBox.Size = New System.Drawing.Size(127, 21)
            Me.OTDesktopComboBox.TabIndex = 32
            '
            'EnviarTodosCheckBox
            '
            Me.EnviarTodosCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EnviarTodosCheckBox.AutoSize = True
            Me.EnviarTodosCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EnviarTodosCheckBox.Location = New System.Drawing.Point(912, 95)
            Me.EnviarTodosCheckBox.Name = "EnviarTodosCheckBox"
            Me.EnviarTodosCheckBox.Size = New System.Drawing.Size(123, 21)
            Me.EnviarTodosCheckBox.TabIndex = 3
            Me.EnviarTodosCheckBox.Text = "Enviar Todos"
            Me.EnviarTodosCheckBox.UseVisualStyleBackColor = True
            '
            'lblOT
            '
            Me.lblOT.AutoSize = True
            Me.lblOT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblOT.Location = New System.Drawing.Point(591, 25)
            Me.lblOT.Name = "lblOT"
            Me.lblOT.Size = New System.Drawing.Size(24, 13)
            Me.lblOT.TabIndex = 31
            Me.lblOT.Text = "OT"
            '
            'FechaProcesoFinalLabel
            '
            Me.FechaProcesoFinalLabel.AutoSize = True
            Me.FechaProcesoFinalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoFinalLabel.Location = New System.Drawing.Point(301, 25)
            Me.FechaProcesoFinalLabel.Name = "FechaProcesoFinalLabel"
            Me.FechaProcesoFinalLabel.Size = New System.Drawing.Size(138, 15)
            Me.FechaProcesoFinalLabel.TabIndex = 16
            Me.FechaProcesoFinalLabel.Text = "Fecha Proceso Final"
            '
            'FechaProcesoFinalDateTimePicker
            '
            Me.FechaProcesoFinalDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoFinalDateTimePicker.Location = New System.Drawing.Point(304, 44)
            Me.FechaProcesoFinalDateTimePicker.Name = "FechaProcesoFinalDateTimePicker"
            Me.FechaProcesoFinalDateTimePicker.Size = New System.Drawing.Size(279, 22)
            Me.FechaProcesoFinalDateTimePicker.TabIndex = 15
            '
            'FechaProcesoDateTimePicker
            '
            Me.FechaProcesoDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoDateTimePicker.Location = New System.Drawing.Point(13, 45)
            Me.FechaProcesoDateTimePicker.Name = "FechaProcesoDateTimePicker"
            Me.FechaProcesoDateTimePicker.Size = New System.Drawing.Size(279, 22)
            Me.FechaProcesoDateTimePicker.TabIndex = 14
            '
            'FechaProcesoLabel
            '
            Me.FechaProcesoLabel.AutoSize = True
            Me.FechaProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoLabel.Location = New System.Drawing.Point(13, 25)
            Me.FechaProcesoLabel.Name = "FechaProcesoLabel"
            Me.FechaProcesoLabel.Size = New System.Drawing.Size(145, 15)
            Me.FechaProcesoLabel.TabIndex = 13
            Me.FechaProcesoLabel.Text = "Fecha Proceso Inicial"
            '
            'DataGridViewCorreos
            '
            Me.DataGridViewCorreos.AllowUserToAddRows = False
            Me.DataGridViewCorreos.AllowUserToDeleteRows = False
            Me.DataGridViewCorreos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridViewCorreos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FechaProceso, Me.OT, Me.Expediente, Me.fk_Folder, Me.fk_File, Me.Version, Me.NombreImagen, Me.Oficio, Me.Radicado, Me.Correo, Me.TipoNotificacion, Me.NombreNotificacion, Me.NombreCartaRespuesta, Me.EnviarCorreo, Me.fk_DocumentoPpal})
            Me.DataGridViewCorreos.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DataGridViewCorreos.Location = New System.Drawing.Point(0, 0)
            Me.DataGridViewCorreos.Name = "DataGridViewCorreos"
            Me.DataGridViewCorreos.Size = New System.Drawing.Size(1043, 311)
            Me.DataGridViewCorreos.TabIndex = 2
            '
            'FechaProceso
            '
            Me.FechaProceso.DataPropertyName = "FechaProceso"
            Me.FechaProceso.HeaderText = "Fecha Proceso"
            Me.FechaProceso.Name = "FechaProceso"
            '
            'OT
            '
            Me.OT.DataPropertyName = "OT"
            Me.OT.HeaderText = "OT"
            Me.OT.Name = "OT"
            '
            'Expediente
            '
            Me.Expediente.DataPropertyName = "fk_Expediente"
            Me.Expediente.HeaderText = "Expediente"
            Me.Expediente.Name = "Expediente"
            '
            'fk_Folder
            '
            Me.fk_Folder.DataPropertyName = "fk_Folder"
            Me.fk_Folder.HeaderText = "Folder"
            Me.fk_Folder.Name = "fk_Folder"
            Me.fk_Folder.Visible = False
            '
            'fk_File
            '
            Me.fk_File.DataPropertyName = "fk_File"
            Me.fk_File.HeaderText = "File"
            Me.fk_File.Name = "fk_File"
            Me.fk_File.Visible = False
            '
            'Version
            '
            Me.Version.DataPropertyName = "fk_Version"
            Me.Version.HeaderText = "Version"
            Me.Version.Name = "Version"
            Me.Version.Visible = False
            '
            'NombreImagen
            '
            Me.NombreImagen.DataPropertyName = "NombreImagen"
            Me.NombreImagen.HeaderText = "Nombre Imagen"
            Me.NombreImagen.Name = "NombreImagen"
            '
            'Oficio
            '
            Me.Oficio.DataPropertyName = "Oficio"
            Me.Oficio.HeaderText = "Oficio"
            Me.Oficio.Name = "Oficio"
            '
            'Radicado
            '
            Me.Radicado.DataPropertyName = "Radicado"
            Me.Radicado.HeaderText = "Radicado"
            Me.Radicado.Name = "Radicado"
            '
            'Correo
            '
            Me.Correo.DataPropertyName = "Correo"
            Me.Correo.HeaderText = "Correo"
            Me.Correo.Name = "Correo"
            '
            'TipoNotificacion
            '
            Me.TipoNotificacion.DataPropertyName = "TipoNotificacion"
            Me.TipoNotificacion.HeaderText = "Tipo Notificación"
            Me.TipoNotificacion.Name = "TipoNotificacion"
            Me.TipoNotificacion.Visible = False
            '
            'NombreNotificacion
            '
            Me.NombreNotificacion.DataPropertyName = "Nombre_Notificacion"
            Me.NombreNotificacion.HeaderText = "Tipo Notificacion"
            Me.NombreNotificacion.Name = "NombreNotificacion"
            '
            'NombreCartaRespuesta
            '
            Me.NombreCartaRespuesta.DataPropertyName = "FormatoCartaRespuesta"
            Me.NombreCartaRespuesta.HeaderText = "Formato Carta Respuesta"
            Me.NombreCartaRespuesta.Name = "NombreCartaRespuesta"
            '
            'EnviarCorreo
            '
            Me.EnviarCorreo.DataPropertyName = "EnviarCorreo"
            Me.EnviarCorreo.HeaderText = "Enviar Correo"
            Me.EnviarCorreo.Name = "EnviarCorreo"
            Me.EnviarCorreo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.EnviarCorreo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            '
            'fk_DocumentoPpal
            '
            Me.fk_DocumentoPpal.DataPropertyName = "fk_DocumentoPpal"
            Me.fk_DocumentoPpal.HeaderText = "fk_DocumentoPpal"
            Me.fk_DocumentoPpal.Name = "fk_DocumentoPpal"
            Me.fk_DocumentoPpal.Visible = False
            '
            'EnviarCorreoButton
            '
            Me.EnviarCorreoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EnviarCorreoButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EnviarCorreoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.EnviarCorreoButton.Location = New System.Drawing.Point(811, 12)
            Me.EnviarCorreoButton.Name = "EnviarCorreoButton"
            Me.EnviarCorreoButton.Size = New System.Drawing.Size(109, 35)
            Me.EnviarCorreoButton.TabIndex = 4
            Me.EnviarCorreoButton.Text = "Enviar Correos"
            Me.EnviarCorreoButton.UseVisualStyleBackColor = True
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.DataGridViewCorreos)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel1.Location = New System.Drawing.Point(0, 122)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(1043, 311)
            Me.Panel1.TabIndex = 5
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.Label1)
            Me.Panel2.Controls.Add(Me.TotalRegistrosTextBox)
            Me.Panel2.Controls.Add(Me.CerrarButton)
            Me.Panel2.Controls.Add(Me.EnviarCorreoButton)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel2.Location = New System.Drawing.Point(0, 378)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(1043, 55)
            Me.Panel2.TabIndex = 6
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancelar
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(947, 12)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 33)
            Me.CerrarButton.TabIndex = 8
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(13, 20)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(104, 15)
            Me.Label1.TabIndex = 37
            Me.Label1.Text = "Total Registros"
            '
            'TotalRegistrosTextBox
            '
            Me.TotalRegistrosTextBox._Obligatorio = False
            Me.TotalRegistrosTextBox._PermitePegar = False
            Me.TotalRegistrosTextBox.Cantidad_Decimales = CType(0, Short)
            Me.TotalRegistrosTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.TotalRegistrosTextBox.DateFormat = Nothing
            Me.TotalRegistrosTextBox.DisabledEnter = False
            Me.TotalRegistrosTextBox.DisabledTab = False
            Me.TotalRegistrosTextBox.EnabledShortCuts = False
            Me.TotalRegistrosTextBox.fk_Campo = 0
            Me.TotalRegistrosTextBox.fk_Documento = 0
            Me.TotalRegistrosTextBox.fk_Validacion = 0
            Me.TotalRegistrosTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.TotalRegistrosTextBox.FocusOut = System.Drawing.Color.White
            Me.TotalRegistrosTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TotalRegistrosTextBox.Location = New System.Drawing.Point(123, 15)
            Me.TotalRegistrosTextBox.MaskedTextBox_Property = ""
            Me.TotalRegistrosTextBox.MaximumLength = CType(0, Short)
            Me.TotalRegistrosTextBox.MinimumLength = CType(0, Short)
            Me.TotalRegistrosTextBox.Name = "TotalRegistrosTextBox"
            Me.TotalRegistrosTextBox.Obligatorio = False
            Me.TotalRegistrosTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.TotalRegistrosTextBox.Rango = Rango1
            Me.TotalRegistrosTextBox.Size = New System.Drawing.Size(57, 21)
            Me.TotalRegistrosTextBox.TabIndex = 36
            Me.TotalRegistrosTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.TotalRegistrosTextBox.Usa_Decimales = False
            Me.TotalRegistrosTextBox.Validos_Cantidad_Puntos = False
            '
            'FormEnviarCorreo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1043, 433)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.ParameterPanel)
            Me.Name = "FormEnviarCorreo"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Enviar Correo"
            Me.ParameterPanel.ResumeLayout(False)
            Me.ParameterPanel.PerformLayout()
            CType(Me.DataGridViewCorreos, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel1.ResumeLayout(False)
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ParameterPanel As System.Windows.Forms.Panel
        Friend WithEvents BuscarFechaButton As System.Windows.Forms.Button
        Friend WithEvents OTDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents lblOT As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoFinalLabel As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoFinalDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaProcesoDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents DataGridViewCorreos As System.Windows.Forms.DataGridView
        Friend WithEvents EnviarTodosCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents EnviarCorreoButton As System.Windows.Forms.Button
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents FechaProceso As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents OT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Expediente As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Folder As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_File As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Version As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreImagen As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Oficio As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Radicado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Correo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents TipoNotificacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreNotificacion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreCartaRespuesta As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EnviarCorreo As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents fk_DocumentoPpal As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents TotalRegistrosTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
    End Class
End Namespace

