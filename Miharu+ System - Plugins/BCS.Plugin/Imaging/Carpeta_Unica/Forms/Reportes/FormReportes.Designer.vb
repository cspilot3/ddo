Namespace Imaging.Carpeta_Unica.Forms.Reportes

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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.dtpFechaProceso = New System.Windows.Forms.DateTimePicker()
            Me.lblFechaProceso = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.ArchivoRotulosSelectButton = New System.Windows.Forms.Button()
            Me.ArchivoRotulosTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.GenerarButton = New System.Windows.Forms.Button()
            Me.RutaTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.ArchivoRotulosCheckBox = New System.Windows.Forms.CheckBox()
            Me.ProcesoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.SelectFolderButton = New System.Windows.Forms.Button()
            Me.ReporteDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(41, 75)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(56, 13)
            Me.Label1.TabIndex = 65
            Me.Label1.Text = "Reporte:"
            '
            'dtpFechaProceso
            '
            Me.dtpFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpFechaProceso.Location = New System.Drawing.Point(144, 35)
            Me.dtpFechaProceso.Name = "dtpFechaProceso"
            Me.dtpFechaProceso.Size = New System.Drawing.Size(268, 20)
            Me.dtpFechaProceso.TabIndex = 64
            '
            'lblFechaProceso
            '
            Me.lblFechaProceso.AutoSize = True
            Me.lblFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaProceso.Location = New System.Drawing.Point(41, 39)
            Me.lblFechaProceso.Name = "lblFechaProceso"
            Me.lblFechaProceso.Size = New System.Drawing.Size(96, 13)
            Me.lblFechaProceso.TabIndex = 63
            Me.lblFechaProceso.Text = "Fecha Proceso:"
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.ArchivoRotulosSelectButton)
            Me.GroupBox1.Controls.Add(Me.ArchivoRotulosTextBox)
            Me.GroupBox1.Controls.Add(Me.GenerarButton)
            Me.GroupBox1.Controls.Add(Me.RutaTextBox)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.ArchivoRotulosCheckBox)
            Me.GroupBox1.Controls.Add(Me.ProcesoDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.SelectFolderButton)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(454, 260)
            Me.GroupBox1.TabIndex = 69
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Datos de Reporte"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(26, 134)
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
            Rango1.MaxValue = 9.2233720368547758E+18R
            Rango1.MinValue = 0.0R
            Me.ArchivoRotulosTextBox.Rango = Rango1
            Me.ArchivoRotulosTextBox.Size = New System.Drawing.Size(368, 20)
            Me.ArchivoRotulosTextBox.TabIndex = 75
            Me.ArchivoRotulosTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ArchivoRotulosTextBox.Usa_Decimales = False
            Me.ArchivoRotulosTextBox.Validos_Cantidad_Puntos = False
            Me.ArchivoRotulosTextBox.Visible = False
            '
            'GenerarButton
            '
            Me.GenerarButton.BackColor = System.Drawing.SystemColors.Control
            Me.GenerarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.GenerarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GenerarButton.Image = Global.BCS.Plugin.My.Resources.Resources.Process_Accept
            Me.GenerarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.GenerarButton.Location = New System.Drawing.Point(179, 185)
            Me.GenerarButton.Name = "GenerarButton"
            Me.GenerarButton.Size = New System.Drawing.Size(100, 60)
            Me.GenerarButton.TabIndex = 55
            Me.GenerarButton.Tag = "Ctrl + P"
            Me.GenerarButton.Text = "&Generar"
            Me.GenerarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.GenerarButton.UseVisualStyleBackColor = False
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
            Me.RutaTextBox.Location = New System.Drawing.Point(31, 150)
            Me.RutaTextBox.MaskedTextBox_Property = ""
            Me.RutaTextBox.MaximumLength = CType(0, Short)
            Me.RutaTextBox.MinimumLength = CType(0, Short)
            Me.RutaTextBox.Name = "RutaTextBox"
            Me.RutaTextBox.Obligatorio = False
            Me.RutaTextBox.permitePegar = False
            Rango2.MaxValue = 9.2233720368547758E+18R
            Rango2.MinValue = 0.0R
            Me.RutaTextBox.Rango = Rango2
            Me.RutaTextBox.Size = New System.Drawing.Size(366, 20)
            Me.RutaTextBox.TabIndex = 56
            Me.RutaTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.RutaTextBox.Usa_Decimales = False
            Me.RutaTextBox.Validos_Cantidad_Puntos = False
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
            'ProcesoDesktopComboBox
            '
            Me.ProcesoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProcesoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProcesoDesktopComboBox.DisabledEnter = False
            Me.ProcesoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProcesoDesktopComboBox.fk_Campo = 0
            Me.ProcesoDesktopComboBox.fk_Documento = 0
            Me.ProcesoDesktopComboBox.fk_Validacion = 0
            Me.ProcesoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProcesoDesktopComboBox.FormattingEnabled = True
            Me.ProcesoDesktopComboBox.Location = New System.Drawing.Point(132, 94)
            Me.ProcesoDesktopComboBox.Name = "ProcesoDesktopComboBox"
            Me.ProcesoDesktopComboBox.Size = New System.Drawing.Size(268, 21)
            Me.ProcesoDesktopComboBox.TabIndex = 63
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(29, 98)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(57, 13)
            Me.Label2.TabIndex = 63
            Me.Label2.Text = "Proceso:"
            '
            'SelectFolderButton
            '
            Me.SelectFolderButton.Image = Global.BCS.Plugin.My.Resources.Resources.MainFolder
            Me.SelectFolderButton.Location = New System.Drawing.Point(403, 148)
            Me.SelectFolderButton.Name = "SelectFolderButton"
            Me.SelectFolderButton.Size = New System.Drawing.Size(27, 23)
            Me.SelectFolderButton.TabIndex = 57
            Me.SelectFolderButton.UseVisualStyleBackColor = True
            '
            'ReporteDesktopComboBox
            '
            Me.ReporteDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ReporteDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ReporteDesktopComboBox.DisabledEnter = False
            Me.ReporteDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ReporteDesktopComboBox.fk_Campo = 0
            Me.ReporteDesktopComboBox.fk_Documento = 0
            Me.ReporteDesktopComboBox.fk_Validacion = 0
            Me.ReporteDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ReporteDesktopComboBox.FormattingEnabled = True
            Me.ReporteDesktopComboBox.Location = New System.Drawing.Point(144, 67)
            Me.ReporteDesktopComboBox.Name = "ReporteDesktopComboBox"
            Me.ReporteDesktopComboBox.Size = New System.Drawing.Size(268, 21)
            Me.ReporteDesktopComboBox.TabIndex = 66
            '
            'FormReportes
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(482, 285)
            Me.Controls.Add(Me.ReporteDesktopComboBox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.dtpFechaProceso)
            Me.Controls.Add(Me.lblFechaProceso)
            Me.Controls.Add(Me.GroupBox1)
            Me.Name = "FormReportes"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Generar Reportes"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ReporteDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents dtpFechaProceso As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblFechaProceso As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents ArchivoRotulosSelectButton As System.Windows.Forms.Button
        Friend WithEvents ArchivoRotulosTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents GenerarButton As System.Windows.Forms.Button
        Friend WithEvents RutaTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents ArchivoRotulosCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents ProcesoDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents SelectFolderButton As System.Windows.Forms.Button
    End Class
End Namespace