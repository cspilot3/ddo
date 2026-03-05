Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNuevaValidacion
        Inherits Miharu.Desktop.Library.FormBase

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNuevaValidacion))
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.PreguntaReporteDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.CategoriaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.ListaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.UsaMotivoCheckBox = New System.Windows.Forms.CheckBox()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.PreguntaDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label11 = New System.Windows.Forms.Label()
            Me.EliminadoCheckBox = New System.Windows.Forms.CheckBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ObligatoriaCheckBox = New System.Windows.Forms.CheckBox()
            Me.SubsanableCheckBox = New System.Windows.Forms.CheckBox()
            Me.lblTipoValidacion = New System.Windows.Forms.Label()
            Me.TipoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.SuspendLayout()
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(234, 264)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(90, 30)
            Me.GuardarButton.TabIndex = 9
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(330, 264)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 10
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Location = New System.Drawing.Point(10, 169)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(108, 13)
            Me.Label10.TabIndex = 72
            Me.Label10.Text = "Pregunta Reporte"
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Location = New System.Drawing.Point(10, 141)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(62, 13)
            Me.Label8.TabIndex = 71
            Me.Label8.Text = "Categoria"
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(10, 113)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(34, 13)
            Me.Label7.TabIndex = 70
            Me.Label7.Text = "Lista"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(10, 85)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(70, 13)
            Me.Label6.TabIndex = 69
            Me.Label6.Text = "Usa Motivo"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(10, 43)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(69, 13)
            Me.Label5.TabIndex = 68
            Me.Label5.Text = "Obligatoria"
            '
            'PreguntaReporteDesktopTextBox
            '
            Me.PreguntaReporteDesktopTextBox._Obligatorio = False
            Me.PreguntaReporteDesktopTextBox._PermitePegar = False
            Me.PreguntaReporteDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.PreguntaReporteDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.PreguntaReporteDesktopTextBox.DateFormat = Nothing
            Me.PreguntaReporteDesktopTextBox.DisabledEnter = False
            Me.PreguntaReporteDesktopTextBox.DisabledTab = False
            Me.PreguntaReporteDesktopTextBox.EnabledShortCuts = False
            Me.PreguntaReporteDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.PreguntaReporteDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.PreguntaReporteDesktopTextBox.Location = New System.Drawing.Point(123, 166)
            Me.PreguntaReporteDesktopTextBox.MaskedTextBox_Property = ""
            Me.PreguntaReporteDesktopTextBox.MaximumLength = CType(250, Short)
            Me.PreguntaReporteDesktopTextBox.MinimumLength = CType(0, Short)
            Me.PreguntaReporteDesktopTextBox.Name = "PreguntaReporteDesktopTextBox"
            Me.PreguntaReporteDesktopTextBox.Obligatorio = False
            Me.PreguntaReporteDesktopTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.PreguntaReporteDesktopTextBox.Rango = Rango1
            Me.PreguntaReporteDesktopTextBox.Size = New System.Drawing.Size(297, 21)
            Me.PreguntaReporteDesktopTextBox.TabIndex = 6
            Me.PreguntaReporteDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.PreguntaReporteDesktopTextBox.Usa_Decimales = False
            Me.PreguntaReporteDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'CategoriaDesktopComboBox
            '
            Me.CategoriaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.CategoriaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.CategoriaDesktopComboBox.DisabledEnter = False
            Me.CategoriaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CategoriaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CategoriaDesktopComboBox.FormattingEnabled = True
            Me.CategoriaDesktopComboBox.Location = New System.Drawing.Point(123, 138)
            Me.CategoriaDesktopComboBox.Name = "CategoriaDesktopComboBox"
            Me.CategoriaDesktopComboBox.Size = New System.Drawing.Size(297, 21)
            Me.CategoriaDesktopComboBox.TabIndex = 5
            '
            'ListaDesktopComboBox
            '
            Me.ListaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ListaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ListaDesktopComboBox.DisabledEnter = False
            Me.ListaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ListaDesktopComboBox.Enabled = False
            Me.ListaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ListaDesktopComboBox.FormattingEnabled = True
            Me.ListaDesktopComboBox.Location = New System.Drawing.Point(123, 110)
            Me.ListaDesktopComboBox.Name = "ListaDesktopComboBox"
            Me.ListaDesktopComboBox.Size = New System.Drawing.Size(297, 21)
            Me.ListaDesktopComboBox.TabIndex = 4
            '
            'UsaMotivoCheckBox
            '
            Me.UsaMotivoCheckBox.AutoSize = True
            Me.UsaMotivoCheckBox.Location = New System.Drawing.Point(123, 85)
            Me.UsaMotivoCheckBox.Name = "UsaMotivoCheckBox"
            Me.UsaMotivoCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.UsaMotivoCheckBox.TabIndex = 3
            Me.UsaMotivoCheckBox.UseVisualStyleBackColor = True
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(10, 15)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(59, 13)
            Me.Label4.TabIndex = 62
            Me.Label4.Text = "Pregunta"
            '
            'PreguntaDesktopTextBox
            '
            Me.PreguntaDesktopTextBox._Obligatorio = False
            Me.PreguntaDesktopTextBox._PermitePegar = False
            Me.PreguntaDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.PreguntaDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.PreguntaDesktopTextBox.DateFormat = Nothing
            Me.PreguntaDesktopTextBox.DisabledEnter = False
            Me.PreguntaDesktopTextBox.DisabledTab = False
            Me.PreguntaDesktopTextBox.EnabledShortCuts = False
            Me.PreguntaDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.PreguntaDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.PreguntaDesktopTextBox.Location = New System.Drawing.Point(123, 12)
            Me.PreguntaDesktopTextBox.MaskedTextBox_Property = ""
            Me.PreguntaDesktopTextBox.MaximumLength = CType(250, Short)
            Me.PreguntaDesktopTextBox.MinimumLength = CType(0, Short)
            Me.PreguntaDesktopTextBox.Name = "PreguntaDesktopTextBox"
            Me.PreguntaDesktopTextBox.Obligatorio = False
            Me.PreguntaDesktopTextBox.permitePegar = False
            Rango2.MaxValue = 2147483647.0R
            Rango2.MinValue = 0.0R
            Me.PreguntaDesktopTextBox.Rango = Rango2
            Me.PreguntaDesktopTextBox.Size = New System.Drawing.Size(297, 21)
            Me.PreguntaDesktopTextBox.TabIndex = 0
            Me.PreguntaDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.PreguntaDesktopTextBox.Usa_Decimales = False
            Me.PreguntaDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Location = New System.Drawing.Point(10, 197)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(61, 13)
            Me.Label11.TabIndex = 76
            Me.Label11.Text = "Eliminado"
            '
            'EliminadoCheckBox
            '
            Me.EliminadoCheckBox.AutoSize = True
            Me.EliminadoCheckBox.Location = New System.Drawing.Point(123, 197)
            Me.EliminadoCheckBox.Name = "EliminadoCheckBox"
            Me.EliminadoCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.EliminadoCheckBox.TabIndex = 7
            Me.EliminadoCheckBox.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(10, 64)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(72, 13)
            Me.Label1.TabIndex = 77
            Me.Label1.Text = "Subsanable"
            '
            'ObligatoriaCheckBox
            '
            Me.ObligatoriaCheckBox.AutoSize = True
            Me.ObligatoriaCheckBox.Location = New System.Drawing.Point(123, 42)
            Me.ObligatoriaCheckBox.Name = "ObligatoriaCheckBox"
            Me.ObligatoriaCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.ObligatoriaCheckBox.TabIndex = 1
            Me.ObligatoriaCheckBox.UseVisualStyleBackColor = True
            '
            'SubsanableCheckBox
            '
            Me.SubsanableCheckBox.AutoSize = True
            Me.SubsanableCheckBox.Location = New System.Drawing.Point(123, 63)
            Me.SubsanableCheckBox.Name = "SubsanableCheckBox"
            Me.SubsanableCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.SubsanableCheckBox.TabIndex = 2
            Me.SubsanableCheckBox.UseVisualStyleBackColor = True
            '
            'lblTipoValidacion
            '
            Me.lblTipoValidacion.AutoSize = True
            Me.lblTipoValidacion.Location = New System.Drawing.Point(10, 230)
            Me.lblTipoValidacion.Name = "lblTipoValidacion"
            Me.lblTipoValidacion.Size = New System.Drawing.Size(31, 13)
            Me.lblTipoValidacion.TabIndex = 80
            Me.lblTipoValidacion.Text = "Tipo"
            '
            'TipoDesktopComboBox
            '
            Me.TipoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipoDesktopComboBox.DisabledEnter = False
            Me.TipoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipoDesktopComboBox.FormattingEnabled = True
            Me.TipoDesktopComboBox.Location = New System.Drawing.Point(123, 222)
            Me.TipoDesktopComboBox.Name = "TipoDesktopComboBox"
            Me.TipoDesktopComboBox.Size = New System.Drawing.Size(297, 21)
            Me.TipoDesktopComboBox.TabIndex = 8
            '
            'FormNuevaValidacion
            '
            Me.AcceptButton = Me.GuardarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(432, 306)
            Me.Controls.Add(Me.TipoDesktopComboBox)
            Me.Controls.Add(Me.lblTipoValidacion)
            Me.Controls.Add(Me.SubsanableCheckBox)
            Me.Controls.Add(Me.ObligatoriaCheckBox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.Label11)
            Me.Controls.Add(Me.EliminadoCheckBox)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Label10)
            Me.Controls.Add(Me.Label8)
            Me.Controls.Add(Me.Label7)
            Me.Controls.Add(Me.Label6)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.PreguntaReporteDesktopTextBox)
            Me.Controls.Add(Me.CategoriaDesktopComboBox)
            Me.Controls.Add(Me.ListaDesktopComboBox)
            Me.Controls.Add(Me.UsaMotivoCheckBox)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.PreguntaDesktopTextBox)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormNuevaValidacion"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Agregar Validación"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents PreguntaDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents UsaMotivoCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents ListaDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents CategoriaDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents PreguntaReporteDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label11 As System.Windows.Forms.Label
        Friend WithEvents EliminadoCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ObligatoriaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents SubsanableCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents lblTipoValidacion As System.Windows.Forms.Label
        Friend WithEvents TipoDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    End Class
End Namespace