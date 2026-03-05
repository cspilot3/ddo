<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCargue_Log_Actualizacion_Radicado
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
        Me.GroupBoxPublicacion = New System.Windows.Forms.GroupBox()
        Me.OTDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.lblOT = New System.Windows.Forms.Label()
        Me.lblFechaProceso = New System.Windows.Forms.Label()
        Me.FechaProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.BtnCancelar = New System.Windows.Forms.Button()
        Me.BtnCargar = New System.Windows.Forms.Button()
        Me.SelectFolderButton = New System.Windows.Forms.Button()
        Me.RutaTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
        Me.lblSeleccionarArchivo = New System.Windows.Forms.Label()
        Me.GroupBoxPublicacion.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxPublicacion
        '
        Me.GroupBoxPublicacion.Controls.Add(Me.lblSeleccionarArchivo)
        Me.GroupBoxPublicacion.Controls.Add(Me.SelectFolderButton)
        Me.GroupBoxPublicacion.Controls.Add(Me.RutaTextBox)
        Me.GroupBoxPublicacion.Controls.Add(Me.OTDesktopComboBox)
        Me.GroupBoxPublicacion.Controls.Add(Me.lblOT)
        Me.GroupBoxPublicacion.Controls.Add(Me.BtnCancelar)
        Me.GroupBoxPublicacion.Controls.Add(Me.BtnCargar)
        Me.GroupBoxPublicacion.Controls.Add(Me.lblFechaProceso)
        Me.GroupBoxPublicacion.Controls.Add(Me.FechaProcesoDateTimePicker)
        Me.GroupBoxPublicacion.Location = New System.Drawing.Point(12, 21)
        Me.GroupBoxPublicacion.Name = "GroupBoxPublicacion"
        Me.GroupBoxPublicacion.Size = New System.Drawing.Size(413, 197)
        Me.GroupBoxPublicacion.TabIndex = 3
        Me.GroupBoxPublicacion.TabStop = False
        Me.GroupBoxPublicacion.Text = "Filtros de Publicacion"
        '
        'OTDesktopComboBox
        '
        Me.OTDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OTDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.OTDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.OTDesktopComboBox.DisabledEnter = False
        Me.OTDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OTDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.OTDesktopComboBox.FormattingEnabled = True
        Me.OTDesktopComboBox.Location = New System.Drawing.Point(149, 49)
        Me.OTDesktopComboBox.Name = "OTDesktopComboBox"
        Me.OTDesktopComboBox.Size = New System.Drawing.Size(96, 21)
        Me.OTDesktopComboBox.TabIndex = 2
        '
        'lblOT
        '
        Me.lblOT.AutoSize = True
        Me.lblOT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOT.Location = New System.Drawing.Point(16, 57)
        Me.lblOT.Name = "lblOT"
        Me.lblOT.Size = New System.Drawing.Size(28, 13)
        Me.lblOT.TabIndex = 15
        Me.lblOT.Text = "OT:"
        '
        'lblFechaProceso
        '
        Me.lblFechaProceso.AutoSize = True
        Me.lblFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaProceso.Location = New System.Drawing.Point(16, 29)
        Me.lblFechaProceso.Name = "lblFechaProceso"
        Me.lblFechaProceso.Size = New System.Drawing.Size(96, 13)
        Me.lblFechaProceso.TabIndex = 9
        Me.lblFechaProceso.Text = "Fecha Proceso:"
        '
        'FechaProcesoDateTimePicker
        '
        Me.FechaProcesoDateTimePicker.Location = New System.Drawing.Point(149, 23)
        Me.FechaProcesoDateTimePicker.Name = "FechaProcesoDateTimePicker"
        Me.FechaProcesoDateTimePicker.Size = New System.Drawing.Size(200, 20)
        Me.FechaProcesoDateTimePicker.TabIndex = 1
        '
        'BtnCancelar
        '
        Me.BtnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancelar.Image = Global.Colpensiones.Plugin.My.Resources.Resources.btnSalir
        Me.BtnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnCancelar.Location = New System.Drawing.Point(285, 139)
        Me.BtnCancelar.Name = "BtnCancelar"
        Me.BtnCancelar.Size = New System.Drawing.Size(97, 33)
        Me.BtnCancelar.TabIndex = 4
        Me.BtnCancelar.Text = "Cancelar"
        Me.BtnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCancelar.UseVisualStyleBackColor = True
        '
        'BtnCargar
        '
        Me.BtnCargar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCargar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCargar.Image = Global.Colpensiones.Plugin.My.Resources.Resources.Process_Accept
        Me.BtnCargar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnCargar.Location = New System.Drawing.Point(114, 139)
        Me.BtnCargar.Name = "BtnCargar"
        Me.BtnCargar.Size = New System.Drawing.Size(96, 33)
        Me.BtnCargar.TabIndex = 3
        Me.BtnCargar.Text = "Cargar"
        Me.BtnCargar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCargar.UseVisualStyleBackColor = True
        '
        'SelectFolderButton
        '
        Me.SelectFolderButton.Location = New System.Drawing.Point(355, 73)
        Me.SelectFolderButton.Name = "SelectFolderButton"
        Me.SelectFolderButton.Size = New System.Drawing.Size(27, 23)
        Me.SelectFolderButton.TabIndex = 17
        Me.SelectFolderButton.UseVisualStyleBackColor = True
        '
        'RutaTextBox
        '
        Me.RutaTextBox._Obligatorio = False
        Me.RutaTextBox._PermitePegar = False
        Me.RutaTextBox.Cantidad_Decimales = CType(0, Short)
        Me.RutaTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
        Me.RutaTextBox.DateFormat = Nothing
        Me.RutaTextBox.DisabledEnter = False
        Me.RutaTextBox.DisabledTab = False
        Me.RutaTextBox.EnabledShortCuts = False
        Me.RutaTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.RutaTextBox.FocusOut = System.Drawing.Color.White
        Me.RutaTextBox.Location = New System.Drawing.Point(149, 76)
        Me.RutaTextBox.MaskedTextBox_Property = ""
        Me.RutaTextBox.MaximumLength = CType(0, Short)
        Me.RutaTextBox.MinimumLength = CType(0, Short)
        Me.RutaTextBox.Name = "RutaTextBox"
        Me.RutaTextBox.Obligatorio = False
        Me.RutaTextBox.permitePegar = False
        Rango1.MaxValue = 9.2233720368547758E+18R
        Rango1.MinValue = 0.0R
        Me.RutaTextBox.Rango = Rango1
        Me.RutaTextBox.Size = New System.Drawing.Size(200, 20)
        Me.RutaTextBox.TabIndex = 16
        Me.RutaTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
        Me.RutaTextBox.Usa_Decimales = False
        Me.RutaTextBox.Validos_Cantidad_Puntos = False
        '
        'lblSeleccionarArchivo
        '
        Me.lblSeleccionarArchivo.AutoSize = True
        Me.lblSeleccionarArchivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeleccionarArchivo.Location = New System.Drawing.Point(16, 79)
        Me.lblSeleccionarArchivo.Name = "lblSeleccionarArchivo"
        Me.lblSeleccionarArchivo.Size = New System.Drawing.Size(125, 13)
        Me.lblSeleccionarArchivo.TabIndex = 18
        Me.lblSeleccionarArchivo.Text = "Seleccionar Archivo:"
        '
        'FormCargue_Log_Actualizacion_Radicado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 228)
        Me.Controls.Add(Me.GroupBoxPublicacion)
        Me.Name = "FormCargue_Log_Actualizacion_Radicado"
        Me.Text = "FormCargue_Log_Atualizacion_Radicado"
        Me.GroupBoxPublicacion.ResumeLayout(False)
        Me.GroupBoxPublicacion.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBoxPublicacion As System.Windows.Forms.GroupBox
    Friend WithEvents OTDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    Friend WithEvents lblOT As System.Windows.Forms.Label
    Friend WithEvents BtnCancelar As System.Windows.Forms.Button
    Friend WithEvents BtnCargar As System.Windows.Forms.Button
    Friend WithEvents lblFechaProceso As System.Windows.Forms.Label
    Friend WithEvents FechaProcesoDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblSeleccionarArchivo As System.Windows.Forms.Label
    Friend WithEvents SelectFolderButton As System.Windows.Forms.Button
    Friend WithEvents RutaTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
End Class
