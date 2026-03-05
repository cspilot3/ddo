Namespace Imaging.Hipotecarios.Forms.Reportes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormReportes
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
            Dim Rango3 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango4 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.PublicarButton = New System.Windows.Forms.Button()
            Me.CruzarButton = New System.Windows.Forms.Button()
            Me.dtpFechaProceso = New System.Windows.Forms.DateTimePicker()
            Me.lblFechaProceso = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.ArchivoRotulosSelectButton = New System.Windows.Forms.Button()
            Me.GenerarButton = New System.Windows.Forms.Button()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.ArchivoRotulosCheckBox = New System.Windows.Forms.CheckBox()
            Me.SelectFolderButton = New System.Windows.Forms.Button()
            Me.ArchivoRotulosTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.RutaTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.PublicarButton)
            Me.GroupBox1.Controls.Add(Me.CruzarButton)
            Me.GroupBox1.Controls.Add(Me.dtpFechaProceso)
            Me.GroupBox1.Controls.Add(Me.lblFechaProceso)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.ArchivoRotulosSelectButton)
            Me.GroupBox1.Controls.Add(Me.ArchivoRotulosTextBox)
            Me.GroupBox1.Controls.Add(Me.GenerarButton)
            Me.GroupBox1.Controls.Add(Me.RutaTextBox)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.ArchivoRotulosCheckBox)
            Me.GroupBox1.Controls.Add(Me.SelectFolderButton)
            Me.GroupBox1.Location = New System.Drawing.Point(21, 21)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(454, 202)
            Me.GroupBox1.TabIndex = 70
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Datos de Reporte"
            '
            'PublicarButton
            '
            Me.PublicarButton.AccessibleDescription = ""
            Me.PublicarButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.PublicarButton.BackColor = System.Drawing.SystemColors.Control
            Me.PublicarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.PublicarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PublicarButton.Image = Global.BCS.Plugin.My.Resources.Resources.Process_Accept
            Me.PublicarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.PublicarButton.Location = New System.Drawing.Point(164, 118)
            Me.PublicarButton.Name = "PublicarButton"
            Me.PublicarButton.Size = New System.Drawing.Size(100, 60)
            Me.PublicarButton.TabIndex = 81
            Me.PublicarButton.Tag = "Ctrl + P"
            Me.PublicarButton.Text = "&Publicar"
            Me.PublicarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.PublicarButton.UseVisualStyleBackColor = False
            '
            'CruzarButton
            '
            Me.CruzarButton.AccessibleDescription = ""
            Me.CruzarButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.CruzarButton.BackColor = System.Drawing.SystemColors.Control
            Me.CruzarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CruzarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CruzarButton.Image = Global.BCS.Plugin.My.Resources.Resources.cross
            Me.CruzarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CruzarButton.Location = New System.Drawing.Point(29, 118)
            Me.CruzarButton.Name = "CruzarButton"
            Me.CruzarButton.Size = New System.Drawing.Size(100, 60)
            Me.CruzarButton.TabIndex = 80
            Me.CruzarButton.Tag = "Ctrl + C"
            Me.CruzarButton.Text = "&Cruzar"
            Me.CruzarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CruzarButton.UseVisualStyleBackColor = False
            '
            'dtpFechaProceso
            '
            Me.dtpFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpFechaProceso.Location = New System.Drawing.Point(129, 29)
            Me.dtpFechaProceso.Name = "dtpFechaProceso"
            Me.dtpFechaProceso.Size = New System.Drawing.Size(268, 20)
            Me.dtpFechaProceso.TabIndex = 79
            '
            'lblFechaProceso
            '
            Me.lblFechaProceso.AutoSize = True
            Me.lblFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaProceso.Location = New System.Drawing.Point(26, 33)
            Me.lblFechaProceso.Name = "lblFechaProceso"
            Me.lblFechaProceso.Size = New System.Drawing.Size(96, 13)
            Me.lblFechaProceso.TabIndex = 78
            Me.lblFechaProceso.Text = "Fecha Proceso:"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(24, 67)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(112, 13)
            Me.Label4.TabIndex = 77
            Me.Label4.Text = "Carpeta de Salida:"
            '
            'ArchivoRotulosSelectButton
            '
            Me.ArchivoRotulosSelectButton.Image = Global.BCS.Plugin.My.Resources.Resources.File
            Me.ArchivoRotulosSelectButton.Location = New System.Drawing.Point(406, 286)
            Me.ArchivoRotulosSelectButton.Name = "ArchivoRotulosSelectButton"
            Me.ArchivoRotulosSelectButton.Size = New System.Drawing.Size(27, 23)
            Me.ArchivoRotulosSelectButton.TabIndex = 76
            Me.ArchivoRotulosSelectButton.UseVisualStyleBackColor = True
            Me.ArchivoRotulosSelectButton.Visible = False
            '
            'GenerarButton
            '
            Me.GenerarButton.BackColor = System.Drawing.SystemColors.Control
            Me.GenerarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.GenerarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GenerarButton.Image = Global.BCS.Plugin.My.Resources.Resources.Process_Accept
            Me.GenerarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.GenerarButton.Location = New System.Drawing.Point(297, 118)
            Me.GenerarButton.Name = "GenerarButton"
            Me.GenerarButton.Size = New System.Drawing.Size(100, 60)
            Me.GenerarButton.TabIndex = 55
            Me.GenerarButton.Tag = "Ctrl + P"
            Me.GenerarButton.Text = "&Generar"
            Me.GenerarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.GenerarButton.UseVisualStyleBackColor = False
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(31, 273)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(101, 13)
            Me.Label3.TabIndex = 73
            Me.Label3.Text = "Archivo Rotulos:"
            Me.Label3.Visible = False
            '
            'ArchivoRotulosCheckBox
            '
            Me.ArchivoRotulosCheckBox.AutoSize = True
            Me.ArchivoRotulosCheckBox.Location = New System.Drawing.Point(138, 273)
            Me.ArchivoRotulosCheckBox.Name = "ArchivoRotulosCheckBox"
            Me.ArchivoRotulosCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.ArchivoRotulosCheckBox.TabIndex = 72
            Me.ArchivoRotulosCheckBox.UseVisualStyleBackColor = True
            Me.ArchivoRotulosCheckBox.Visible = False
            '
            'SelectFolderButton
            '
            Me.SelectFolderButton.Image = Global.BCS.Plugin.My.Resources.Resources.MainFolder
            Me.SelectFolderButton.Location = New System.Drawing.Point(401, 81)
            Me.SelectFolderButton.Name = "SelectFolderButton"
            Me.SelectFolderButton.Size = New System.Drawing.Size(27, 23)
            Me.SelectFolderButton.TabIndex = 57
            Me.SelectFolderButton.UseVisualStyleBackColor = True
            '
            'ArchivoRotulosTextBox
            '
            Me.ArchivoRotulosTextBox._Obligatorio = False
            Me.ArchivoRotulosTextBox._PermitePegar = False
            Me.ArchivoRotulosTextBox.Cantidad_Decimales = CType(0, Short)
            Me.ArchivoRotulosTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ArchivoRotulosTextBox.DateFormat = Nothing
            Me.ArchivoRotulosTextBox.DisabledEnter = False
            Me.ArchivoRotulosTextBox.DisabledTab = False
            Me.ArchivoRotulosTextBox.EnabledShortCuts = False
            Me.ArchivoRotulosTextBox.fk_Campo = 0
            Me.ArchivoRotulosTextBox.fk_Documento = 0
            Me.ArchivoRotulosTextBox.fk_Validacion = 0
            Me.ArchivoRotulosTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ArchivoRotulosTextBox.FocusOut = System.Drawing.Color.White
            Me.ArchivoRotulosTextBox.Location = New System.Drawing.Point(32, 289)
            Me.ArchivoRotulosTextBox.MaskedTextBox_Property = ""
            Me.ArchivoRotulosTextBox.MaximumLength = CType(0, Short)
            Me.ArchivoRotulosTextBox.MinimumLength = CType(0, Short)
            Me.ArchivoRotulosTextBox.Name = "ArchivoRotulosTextBox"
            Me.ArchivoRotulosTextBox.Obligatorio = False
            Me.ArchivoRotulosTextBox.permitePegar = False
            Rango3.MaxValue = 9.2233720368547758E+18R
            Rango3.MinValue = 0.0R
            Me.ArchivoRotulosTextBox.Rango = Rango3
            Me.ArchivoRotulosTextBox.Size = New System.Drawing.Size(368, 20)
            Me.ArchivoRotulosTextBox.TabIndex = 75
            Me.ArchivoRotulosTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ArchivoRotulosTextBox.Usa_Decimales = False
            Me.ArchivoRotulosTextBox.Validos_Cantidad_Puntos = False
            Me.ArchivoRotulosTextBox.Visible = False
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
            Me.RutaTextBox.fk_Campo = 0
            Me.RutaTextBox.fk_Documento = 0
            Me.RutaTextBox.fk_Validacion = 0
            Me.RutaTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.RutaTextBox.FocusOut = System.Drawing.Color.White
            Me.RutaTextBox.Location = New System.Drawing.Point(29, 83)
            Me.RutaTextBox.MaskedTextBox_Property = ""
            Me.RutaTextBox.MaximumLength = CType(0, Short)
            Me.RutaTextBox.MinimumLength = CType(0, Short)
            Me.RutaTextBox.Name = "RutaTextBox"
            Me.RutaTextBox.Obligatorio = False
            Me.RutaTextBox.permitePegar = False
            Rango4.MaxValue = 9.2233720368547758E+18R
            Rango4.MinValue = 0.0R
            Me.RutaTextBox.Rango = Rango4
            Me.RutaTextBox.Size = New System.Drawing.Size(366, 20)
            Me.RutaTextBox.TabIndex = 56
            Me.RutaTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.RutaTextBox.Usa_Decimales = False
            Me.RutaTextBox.Validos_Cantidad_Puntos = False
            '
            'FormReportes
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(495, 234)
            Me.Controls.Add(Me.GroupBox1)
            Me.Name = "FormReportes"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Generación Delta"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents ArchivoRotulosSelectButton As System.Windows.Forms.Button
        Friend WithEvents ArchivoRotulosTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents GenerarButton As System.Windows.Forms.Button
        Friend WithEvents RutaTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents ArchivoRotulosCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents SelectFolderButton As System.Windows.Forms.Button
        Friend WithEvents dtpFechaProceso As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblFechaProceso As System.Windows.Forms.Label
        Friend WithEvents CruzarButton As System.Windows.Forms.Button
        Friend WithEvents PublicarButton As System.Windows.Forms.Button
    End Class
End Namespace

