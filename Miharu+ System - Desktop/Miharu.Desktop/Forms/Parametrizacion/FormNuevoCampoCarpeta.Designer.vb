Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNuevoCampoCarpeta
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNuevoCampoCarpeta))
            Me.Nombre_Campo_TextBox = New System.Windows.Forms.TextBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label14 = New System.Windows.Forms.Label()
            Me.Eliminado_CheckBox = New System.Windows.Forms.CheckBox()
            Me.CampoTipoTextBox = New System.Windows.Forms.TextBox()
            Me.CampoListaTextBox = New System.Windows.Forms.TextBox()
            Me.id_CampoTextBox = New System.Windows.Forms.TextBox()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.IdDocumentoTextBox = New System.Windows.Forms.TextBox()
            Me.Precaptura_CheckBox = New System.Windows.Forms.CheckBox()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.PrimeraCaptura_CheckBox = New System.Windows.Forms.CheckBox()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.DobleCaptura_CheckBox = New System.Windows.Forms.CheckBox()
            Me.DobleCaptura = New System.Windows.Forms.Label()
            Me.UsaMarca_CheckBox = New System.Windows.Forms.CheckBox()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.Btn_AsignarMarca = New System.Windows.Forms.Button()
            Me.Marca_X_Campo_TextBox = New System.Windows.Forms.TextBox()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.Marca_Y_Campo_TextBox = New System.Windows.Forms.TextBox()
            Me.Label11 = New System.Windows.Forms.Label()
            Me.Marca_Height_Campo_TextBox = New System.Windows.Forms.TextBox()
            Me.Label12 = New System.Windows.Forms.Label()
            Me.Marca_Width_Campo_TextBox = New System.Windows.Forms.TextBox()
            Me.Label13 = New System.Windows.Forms.Label()
            Me.Tipo_Llave_ComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Longitud_Minima_TextBox = New Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl()
            Me.Longitud_Campo_TextBox = New Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl()
            Me.Campo_Tipo_ComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.SuspendLayout()
            '
            'Nombre_Campo_TextBox
            '
            Me.Nombre_Campo_TextBox.Location = New System.Drawing.Point(165, 12)
            Me.Nombre_Campo_TextBox.MaxLength = 99
            Me.Nombre_Campo_TextBox.Name = "Nombre_Campo_TextBox"
            Me.Nombre_Campo_TextBox.Size = New System.Drawing.Size(214, 20)
            Me.Nombre_Campo_TextBox.TabIndex = 1
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(13, 125)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(120, 13)
            Me.Label7.TabIndex = 20
            Me.Label7.Text = "Longitud Minima Campo"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(13, 98)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(84, 13)
            Me.Label4.TabIndex = 14
            Me.Label4.Text = "Longitud Campo"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(406, 9)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(82, 13)
            Me.Label3.TabIndex = 12
            Me.Label3.Text = "Fase Captura"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(12, 75)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(64, 13)
            Me.Label2.TabIndex = 10
            Me.Label2.Text = "Campo Tipo"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(12, 19)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(80, 13)
            Me.Label1.TabIndex = 9
            Me.Label1.Text = "Nombre Campo"
            '
            'Label14
            '
            Me.Label14.AutoSize = True
            Me.Label14.Location = New System.Drawing.Point(406, 100)
            Me.Label14.Name = "Label14"
            Me.Label14.Size = New System.Drawing.Size(52, 13)
            Me.Label14.TabIndex = 30
            Me.Label14.Text = "Eliminado"
            '
            'Eliminado_CheckBox
            '
            Me.Eliminado_CheckBox.AutoSize = True
            Me.Eliminado_CheckBox.Location = New System.Drawing.Point(559, 100)
            Me.Eliminado_CheckBox.Name = "Eliminado_CheckBox"
            Me.Eliminado_CheckBox.Size = New System.Drawing.Size(15, 14)
            Me.Eliminado_CheckBox.TabIndex = 14
            Me.Eliminado_CheckBox.UseVisualStyleBackColor = True
            '
            'CampoTipoTextBox
            '
            Me.CampoTipoTextBox.Location = New System.Drawing.Point(3, 314)
            Me.CampoTipoTextBox.Name = "CampoTipoTextBox"
            Me.CampoTipoTextBox.Size = New System.Drawing.Size(36, 20)
            Me.CampoTipoTextBox.TabIndex = 49
            Me.CampoTipoTextBox.Visible = False
            '
            'CampoListaTextBox
            '
            Me.CampoListaTextBox.Location = New System.Drawing.Point(3, 314)
            Me.CampoListaTextBox.Name = "CampoListaTextBox"
            Me.CampoListaTextBox.Size = New System.Drawing.Size(36, 20)
            Me.CampoListaTextBox.TabIndex = 48
            Me.CampoListaTextBox.Visible = False
            '
            'id_CampoTextBox
            '
            Me.id_CampoTextBox.Location = New System.Drawing.Point(15, 314)
            Me.id_CampoTextBox.Name = "id_CampoTextBox"
            Me.id_CampoTextBox.Size = New System.Drawing.Size(36, 20)
            Me.id_CampoTextBox.TabIndex = 47
            Me.id_CampoTextBox.Visible = False
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.GuardarButton.Image = CType(resources.GetObject("GuardarButton.Image"), System.Drawing.Image)
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(322, 327)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(122, 30)
            Me.GuardarButton.TabIndex = 15
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CerrarButton.Image = CType(resources.GetObject("CerrarButton.Image"), System.Drawing.Image)
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(453, 327)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(122, 30)
            Me.CerrarButton.TabIndex = 16
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'IdDocumentoTextBox
            '
            Me.IdDocumentoTextBox.Location = New System.Drawing.Point(12, 314)
            Me.IdDocumentoTextBox.Name = "IdDocumentoTextBox"
            Me.IdDocumentoTextBox.Size = New System.Drawing.Size(36, 20)
            Me.IdDocumentoTextBox.TabIndex = 50
            Me.IdDocumentoTextBox.Visible = False
            '
            'Precaptura_CheckBox
            '
            Me.Precaptura_CheckBox.AutoSize = True
            Me.Precaptura_CheckBox.Location = New System.Drawing.Point(559, 31)
            Me.Precaptura_CheckBox.Name = "Precaptura_CheckBox"
            Me.Precaptura_CheckBox.Size = New System.Drawing.Size(15, 14)
            Me.Precaptura_CheckBox.TabIndex = 11
            Me.Precaptura_CheckBox.UseVisualStyleBackColor = True
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(406, 31)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(59, 13)
            Me.Label5.TabIndex = 54
            Me.Label5.Text = "Precaptura"
            '
            'PrimeraCaptura_CheckBox
            '
            Me.PrimeraCaptura_CheckBox.AutoSize = True
            Me.PrimeraCaptura_CheckBox.Location = New System.Drawing.Point(559, 54)
            Me.PrimeraCaptura_CheckBox.Name = "PrimeraCaptura_CheckBox"
            Me.PrimeraCaptura_CheckBox.Size = New System.Drawing.Size(15, 14)
            Me.PrimeraCaptura_CheckBox.TabIndex = 12
            Me.PrimeraCaptura_CheckBox.UseVisualStyleBackColor = True
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(406, 54)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(82, 13)
            Me.Label6.TabIndex = 56
            Me.Label6.Text = "Primera Captura"
            '
            'DobleCaptura_CheckBox
            '
            Me.DobleCaptura_CheckBox.AutoSize = True
            Me.DobleCaptura_CheckBox.Location = New System.Drawing.Point(559, 77)
            Me.DobleCaptura_CheckBox.Name = "DobleCaptura_CheckBox"
            Me.DobleCaptura_CheckBox.Size = New System.Drawing.Size(15, 14)
            Me.DobleCaptura_CheckBox.TabIndex = 13
            Me.DobleCaptura_CheckBox.UseVisualStyleBackColor = True
            '
            'DobleCaptura
            '
            Me.DobleCaptura.AutoSize = True
            Me.DobleCaptura.Location = New System.Drawing.Point(406, 77)
            Me.DobleCaptura.Name = "DobleCaptura"
            Me.DobleCaptura.Size = New System.Drawing.Size(75, 13)
            Me.DobleCaptura.TabIndex = 58
            Me.DobleCaptura.Text = "Doble Captura"
            '
            'UsaMarca_CheckBox
            '
            Me.UsaMarca_CheckBox.AutoSize = True
            Me.UsaMarca_CheckBox.Location = New System.Drawing.Point(166, 155)
            Me.UsaMarca_CheckBox.Name = "UsaMarca_CheckBox"
            Me.UsaMarca_CheckBox.Size = New System.Drawing.Size(15, 14)
            Me.UsaMarca_CheckBox.TabIndex = 61
            Me.UsaMarca_CheckBox.UseVisualStyleBackColor = True
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Location = New System.Drawing.Point(13, 156)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(37, 13)
            Me.Label8.TabIndex = 60
            Me.Label8.Text = "Marca"
            '
            'Btn_AsignarMarca
            '
            Me.Btn_AsignarMarca.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Btn_AsignarMarca.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.Btn_AsignarMarca.Image = Global.Miharu.Desktop.My.Resources.Resources.Imaging
            Me.Btn_AsignarMarca.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Btn_AsignarMarca.Location = New System.Drawing.Point(207, 146)
            Me.Btn_AsignarMarca.Name = "Btn_AsignarMarca"
            Me.Btn_AsignarMarca.Size = New System.Drawing.Size(144, 23)
            Me.Btn_AsignarMarca.TabIndex = 6
            Me.Btn_AsignarMarca.Text = "&Asignar Marca"
            Me.Btn_AsignarMarca.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.Btn_AsignarMarca.UseVisualStyleBackColor = True
            '
            'Marca_X_Campo_TextBox
            '
            Me.Marca_X_Campo_TextBox.Location = New System.Drawing.Point(166, 179)
            Me.Marca_X_Campo_TextBox.MaxLength = 99
            Me.Marca_X_Campo_TextBox.Name = "Marca_X_Campo_TextBox"
            Me.Marca_X_Campo_TextBox.Size = New System.Drawing.Size(87, 20)
            Me.Marca_X_Campo_TextBox.TabIndex = 7
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Location = New System.Drawing.Point(13, 180)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(14, 13)
            Me.Label9.TabIndex = 65
            Me.Label9.Text = "X"
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Location = New System.Drawing.Point(13, 209)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(14, 13)
            Me.Label10.TabIndex = 67
            Me.Label10.Text = "Y"
            '
            'Marca_Y_Campo_TextBox
            '
            Me.Marca_Y_Campo_TextBox.Location = New System.Drawing.Point(166, 208)
            Me.Marca_Y_Campo_TextBox.MaxLength = 99
            Me.Marca_Y_Campo_TextBox.Name = "Marca_Y_Campo_TextBox"
            Me.Marca_Y_Campo_TextBox.Size = New System.Drawing.Size(87, 20)
            Me.Marca_Y_Campo_TextBox.TabIndex = 8
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Location = New System.Drawing.Point(13, 261)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(38, 13)
            Me.Label11.TabIndex = 69
            Me.Label11.Text = "Ancho"
            '
            'Marca_Height_Campo_TextBox
            '
            Me.Marca_Height_Campo_TextBox.ForeColor = System.Drawing.SystemColors.WindowText
            Me.Marca_Height_Campo_TextBox.Location = New System.Drawing.Point(166, 260)
            Me.Marca_Height_Campo_TextBox.MaxLength = 99
            Me.Marca_Height_Campo_TextBox.Name = "Marca_Height_Campo_TextBox"
            Me.Marca_Height_Campo_TextBox.Size = New System.Drawing.Size(87, 20)
            Me.Marca_Height_Campo_TextBox.TabIndex = 10
            '
            'Label12
            '
            Me.Label12.AutoSize = True
            Me.Label12.Location = New System.Drawing.Point(13, 236)
            Me.Label12.Name = "Label12"
            Me.Label12.Size = New System.Drawing.Size(38, 13)
            Me.Label12.TabIndex = 71
            Me.Label12.Text = "Ancho"
            '
            'Marca_Width_Campo_TextBox
            '
            Me.Marca_Width_Campo_TextBox.Location = New System.Drawing.Point(166, 235)
            Me.Marca_Width_Campo_TextBox.MaxLength = 99
            Me.Marca_Width_Campo_TextBox.Name = "Marca_Width_Campo_TextBox"
            Me.Marca_Width_Campo_TextBox.Size = New System.Drawing.Size(87, 20)
            Me.Marca_Width_Campo_TextBox.TabIndex = 9
            '
            'Label13
            '
            Me.Label13.AutoSize = True
            Me.Label13.Location = New System.Drawing.Point(12, 46)
            Me.Label13.Name = "Label13"
            Me.Label13.Size = New System.Drawing.Size(57, 13)
            Me.Label13.TabIndex = 72
            Me.Label13.Text = "Tipo Llave"
            '
            'Tipo_Llave_ComboBox
            '
            Me.Tipo_Llave_ComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.Tipo_Llave_ComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.Tipo_Llave_ComboBox.DisabledEnter = False
            Me.Tipo_Llave_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Tipo_Llave_ComboBox.fk_Campo = 0
            Me.Tipo_Llave_ComboBox.fk_Documento = 0
            Me.Tipo_Llave_ComboBox.fk_Validacion = 0
            Me.Tipo_Llave_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Tipo_Llave_ComboBox.FormattingEnabled = True
            Me.Tipo_Llave_ComboBox.Location = New System.Drawing.Point(165, 38)
            Me.Tipo_Llave_ComboBox.Name = "Tipo_Llave_ComboBox"
            Me.Tipo_Llave_ComboBox.Size = New System.Drawing.Size(214, 21)
            Me.Tipo_Llave_ComboBox.TabIndex = 2
            '
            'Longitud_Minima_TextBox
            '
            Me.Longitud_Minima_TextBox.CantidadDecimales = CType(0, Short)
            Me.Longitud_Minima_TextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Longitud_Minima_TextBox.FocusOut = System.Drawing.Color.White
            Me.Longitud_Minima_TextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Longitud_Minima_TextBox.Location = New System.Drawing.Point(166, 119)
            Me.Longitud_Minima_TextBox.MaxLength = 5
            Me.Longitud_Minima_TextBox.MaxValue = 0.0R
            Me.Longitud_Minima_TextBox.MinValue = 0.0R
            Me.Longitud_Minima_TextBox.Name = "Longitud_Minima_TextBox"
            Me.Longitud_Minima_TextBox.Size = New System.Drawing.Size(214, 20)
            Me.Longitud_Minima_TextBox.TabIndex = 5
            Me.Longitud_Minima_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.Longitud_Minima_TextBox.UsaDecimales = False
            Me.Longitud_Minima_TextBox.UsaRango = False
            '
            'Longitud_Campo_TextBox
            '
            Me.Longitud_Campo_TextBox.CantidadDecimales = CType(0, Short)
            Me.Longitud_Campo_TextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Longitud_Campo_TextBox.FocusOut = System.Drawing.Color.White
            Me.Longitud_Campo_TextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Longitud_Campo_TextBox.Location = New System.Drawing.Point(166, 92)
            Me.Longitud_Campo_TextBox.MaxLength = 5
            Me.Longitud_Campo_TextBox.MaxValue = 0.0R
            Me.Longitud_Campo_TextBox.MinValue = 0.0R
            Me.Longitud_Campo_TextBox.Name = "Longitud_Campo_TextBox"
            Me.Longitud_Campo_TextBox.Size = New System.Drawing.Size(214, 20)
            Me.Longitud_Campo_TextBox.TabIndex = 4
            Me.Longitud_Campo_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.Longitud_Campo_TextBox.UsaDecimales = False
            Me.Longitud_Campo_TextBox.UsaRango = False
            '
            'Campo_Tipo_ComboBox
            '
            Me.Campo_Tipo_ComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.Campo_Tipo_ComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.Campo_Tipo_ComboBox.DisabledEnter = False
            Me.Campo_Tipo_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Campo_Tipo_ComboBox.fk_Campo = 0
            Me.Campo_Tipo_ComboBox.fk_Documento = 0
            Me.Campo_Tipo_ComboBox.fk_Validacion = 0
            Me.Campo_Tipo_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Campo_Tipo_ComboBox.FormattingEnabled = True
            Me.Campo_Tipo_ComboBox.Location = New System.Drawing.Point(165, 67)
            Me.Campo_Tipo_ComboBox.Name = "Campo_Tipo_ComboBox"
            Me.Campo_Tipo_ComboBox.Size = New System.Drawing.Size(214, 21)
            Me.Campo_Tipo_ComboBox.TabIndex = 3
            '
            'FormNuevoCampoCarpeta
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(587, 369)
            Me.Controls.Add(Me.Tipo_Llave_ComboBox)
            Me.Controls.Add(Me.Label13)
            Me.Controls.Add(Me.Label12)
            Me.Controls.Add(Me.Marca_Width_Campo_TextBox)
            Me.Controls.Add(Me.Label11)
            Me.Controls.Add(Me.Marca_Height_Campo_TextBox)
            Me.Controls.Add(Me.Label10)
            Me.Controls.Add(Me.Marca_Y_Campo_TextBox)
            Me.Controls.Add(Me.Label9)
            Me.Controls.Add(Me.Marca_X_Campo_TextBox)
            Me.Controls.Add(Me.Btn_AsignarMarca)
            Me.Controls.Add(Me.UsaMarca_CheckBox)
            Me.Controls.Add(Me.Label8)
            Me.Controls.Add(Me.DobleCaptura_CheckBox)
            Me.Controls.Add(Me.DobleCaptura)
            Me.Controls.Add(Me.PrimeraCaptura_CheckBox)
            Me.Controls.Add(Me.Label6)
            Me.Controls.Add(Me.Precaptura_CheckBox)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.Longitud_Minima_TextBox)
            Me.Controls.Add(Me.Longitud_Campo_TextBox)
            Me.Controls.Add(Me.IdDocumentoTextBox)
            Me.Controls.Add(Me.CampoTipoTextBox)
            Me.Controls.Add(Me.CampoListaTextBox)
            Me.Controls.Add(Me.id_CampoTextBox)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Eliminado_CheckBox)
            Me.Controls.Add(Me.Label14)
            Me.Controls.Add(Me.Campo_Tipo_ComboBox)
            Me.Controls.Add(Me.Nombre_Campo_TextBox)
            Me.Controls.Add(Me.Label7)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Name = "FormNuevoCampoCarpeta"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Nuevo Campo Carpeta"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Campo_Tipo_ComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Nombre_Campo_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label14 As System.Windows.Forms.Label
        Friend WithEvents Eliminado_CheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents CampoTipoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CampoListaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents id_CampoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents IdDocumentoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Longitud_Campo_TextBox As Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl
        Friend WithEvents Longitud_Minima_TextBox As Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl
        Friend WithEvents Precaptura_CheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents PrimeraCaptura_CheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents DobleCaptura_CheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents DobleCaptura As System.Windows.Forms.Label
        Friend WithEvents UsaMarca_CheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents Btn_AsignarMarca As System.Windows.Forms.Button
        Friend WithEvents Marca_X_Campo_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents Marca_Y_Campo_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label11 As System.Windows.Forms.Label
        Friend WithEvents Marca_Height_Campo_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label12 As System.Windows.Forms.Label
        Friend WithEvents Marca_Width_Campo_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Tipo_Llave_ComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label13 As System.Windows.Forms.Label
    End Class
End Namespace