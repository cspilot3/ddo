Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNuevaTablaAsociada
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNuevaTablaAsociada))
            Me.Cantidad_Decimales_TextBox = New Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl()
            Me.Longitud_Campo_TextBox = New Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl()
            Me.IdDocumentoTextBox = New System.Windows.Forms.TextBox()
            Me.CampoTipoTextBox = New System.Windows.Forms.TextBox()
            Me.CampoListaTextBox = New System.Windows.Forms.TextBox()
            Me.id_CampoTextBox = New System.Windows.Forms.TextBox()
            Me.Valor_Defecto_TextBox = New System.Windows.Forms.TextBox()
            Me.Caracter_Decimal_TextBox = New System.Windows.Forms.TextBox()
            Me.Eliminado_CheckBox = New System.Windows.Forms.CheckBox()
            Me.Exportable_CheckBox = New System.Windows.Forms.CheckBox()
            Me.Campo_Busqueda_ComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label19 = New System.Windows.Forms.Label()
            Me.Label15 = New System.Windows.Forms.Label()
            Me.Label14 = New System.Windows.Forms.Label()
            Me.Label12 = New System.Windows.Forms.Label()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.Campo_Busqueda_CheckBox = New System.Windows.Forms.CheckBox()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.Campo_Lista_ComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Campo_Tipo_ComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Usa_Decimales_CheckBox = New System.Windows.Forms.CheckBox()
            Me.Nombre_Campo_TextBox = New System.Windows.Forms.TextBox()
            Me.Obligatorio_CheckBox = New System.Windows.Forms.CheckBox()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'Cantidad_Decimales_TextBox
            '
            Me.Cantidad_Decimales_TextBox.CantidadDecimales = CType(0, Short)
            Me.Cantidad_Decimales_TextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Cantidad_Decimales_TextBox.FocusOut = System.Drawing.Color.White
            Me.Cantidad_Decimales_TextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Cantidad_Decimales_TextBox.Location = New System.Drawing.Point(163, 142)
            Me.Cantidad_Decimales_TextBox.MaxValue = 0.0R
            Me.Cantidad_Decimales_TextBox.MinValue = 0.0R
            Me.Cantidad_Decimales_TextBox.Name = "Cantidad_Decimales_TextBox"
            Me.Cantidad_Decimales_TextBox.Size = New System.Drawing.Size(214, 20)
            Me.Cantidad_Decimales_TextBox.TabIndex = 95
            Me.Cantidad_Decimales_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.Cantidad_Decimales_TextBox.UsaDecimales = False
            Me.Cantidad_Decimales_TextBox.UsaRango = False
            '
            'Longitud_Campo_TextBox
            '
            Me.Longitud_Campo_TextBox.CantidadDecimales = CType(0, Short)
            Me.Longitud_Campo_TextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Longitud_Campo_TextBox.FocusOut = System.Drawing.Color.White
            Me.Longitud_Campo_TextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Longitud_Campo_TextBox.Location = New System.Drawing.Point(163, 116)
            Me.Longitud_Campo_TextBox.MaxLength = 5
            Me.Longitud_Campo_TextBox.MaxValue = 0.0R
            Me.Longitud_Campo_TextBox.MinValue = 0.0R
            Me.Longitud_Campo_TextBox.Name = "Longitud_Campo_TextBox"
            Me.Longitud_Campo_TextBox.Size = New System.Drawing.Size(214, 20)
            Me.Longitud_Campo_TextBox.TabIndex = 93
            Me.Longitud_Campo_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.Longitud_Campo_TextBox.UsaDecimales = False
            Me.Longitud_Campo_TextBox.UsaRango = False
            '
            'IdDocumentoTextBox
            '
            Me.IdDocumentoTextBox.Location = New System.Drawing.Point(364, 276)
            Me.IdDocumentoTextBox.Name = "IdDocumentoTextBox"
            Me.IdDocumentoTextBox.Size = New System.Drawing.Size(36, 20)
            Me.IdDocumentoTextBox.TabIndex = 92
            Me.IdDocumentoTextBox.Visible = False
            '
            'CampoTipoTextBox
            '
            Me.CampoTipoTextBox.Location = New System.Drawing.Point(364, 276)
            Me.CampoTipoTextBox.Name = "CampoTipoTextBox"
            Me.CampoTipoTextBox.Size = New System.Drawing.Size(36, 20)
            Me.CampoTipoTextBox.TabIndex = 91
            Me.CampoTipoTextBox.Visible = False
            '
            'CampoListaTextBox
            '
            Me.CampoListaTextBox.Location = New System.Drawing.Point(364, 276)
            Me.CampoListaTextBox.Name = "CampoListaTextBox"
            Me.CampoListaTextBox.Size = New System.Drawing.Size(36, 20)
            Me.CampoListaTextBox.TabIndex = 90
            Me.CampoListaTextBox.Visible = False
            '
            'id_CampoTextBox
            '
            Me.id_CampoTextBox.Location = New System.Drawing.Point(364, 276)
            Me.id_CampoTextBox.Name = "id_CampoTextBox"
            Me.id_CampoTextBox.Size = New System.Drawing.Size(36, 20)
            Me.id_CampoTextBox.TabIndex = 89
            Me.id_CampoTextBox.Visible = False
            '
            'Valor_Defecto_TextBox
            '
            Me.Valor_Defecto_TextBox.Location = New System.Drawing.Point(163, 191)
            Me.Valor_Defecto_TextBox.MaxLength = 4
            Me.Valor_Defecto_TextBox.Name = "Valor_Defecto_TextBox"
            Me.Valor_Defecto_TextBox.Size = New System.Drawing.Size(214, 20)
            Me.Valor_Defecto_TextBox.TabIndex = 86
            '
            'Caracter_Decimal_TextBox
            '
            Me.Caracter_Decimal_TextBox.Location = New System.Drawing.Point(163, 165)
            Me.Caracter_Decimal_TextBox.MaxLength = 4
            Me.Caracter_Decimal_TextBox.Name = "Caracter_Decimal_TextBox"
            Me.Caracter_Decimal_TextBox.Size = New System.Drawing.Size(214, 20)
            Me.Caracter_Decimal_TextBox.TabIndex = 82
            '
            'Eliminado_CheckBox
            '
            Me.Eliminado_CheckBox.AutoSize = True
            Me.Eliminado_CheckBox.Location = New System.Drawing.Point(554, 99)
            Me.Eliminado_CheckBox.Name = "Eliminado_CheckBox"
            Me.Eliminado_CheckBox.Size = New System.Drawing.Size(15, 14)
            Me.Eliminado_CheckBox.TabIndex = 81
            Me.Eliminado_CheckBox.UseVisualStyleBackColor = True
            '
            'Exportable_CheckBox
            '
            Me.Exportable_CheckBox.AutoSize = True
            Me.Exportable_CheckBox.Location = New System.Drawing.Point(554, 72)
            Me.Exportable_CheckBox.Name = "Exportable_CheckBox"
            Me.Exportable_CheckBox.Size = New System.Drawing.Size(15, 14)
            Me.Exportable_CheckBox.TabIndex = 80
            Me.Exportable_CheckBox.UseVisualStyleBackColor = True
            '
            'Campo_Busqueda_ComboBox
            '
            Me.Campo_Busqueda_ComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.Campo_Busqueda_ComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.Campo_Busqueda_ComboBox.DisabledEnter = False
            Me.Campo_Busqueda_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Campo_Busqueda_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Campo_Busqueda_ComboBox.FormattingEnabled = True
            Me.Campo_Busqueda_ComboBox.Location = New System.Drawing.Point(163, 89)
            Me.Campo_Busqueda_ComboBox.Name = "Campo_Busqueda_ComboBox"
            Me.Campo_Busqueda_ComboBox.Size = New System.Drawing.Size(214, 21)
            Me.Campo_Busqueda_ComboBox.TabIndex = 78
            '
            'Label19
            '
            Me.Label19.AutoSize = True
            Me.Label19.Location = New System.Drawing.Point(12, 194)
            Me.Label19.Name = "Label19"
            Me.Label19.Size = New System.Drawing.Size(91, 13)
            Me.Label19.TabIndex = 77
            Me.Label19.Text = "Valor Por Defecto"
            '
            'Label15
            '
            Me.Label15.AutoSize = True
            Me.Label15.Location = New System.Drawing.Point(12, 168)
            Me.Label15.Name = "Label15"
            Me.Label15.Size = New System.Drawing.Size(88, 13)
            Me.Label15.TabIndex = 74
            Me.Label15.Text = "Caracter Decimal"
            '
            'Label14
            '
            Me.Label14.AutoSize = True
            Me.Label14.Location = New System.Drawing.Point(401, 99)
            Me.Label14.Name = "Label14"
            Me.Label14.Size = New System.Drawing.Size(52, 13)
            Me.Label14.TabIndex = 73
            Me.Label14.Text = "Eliminado"
            '
            'Label12
            '
            Me.Label12.AutoSize = True
            Me.Label12.Location = New System.Drawing.Point(401, 72)
            Me.Label12.Name = "Label12"
            Me.Label12.Size = New System.Drawing.Size(57, 13)
            Me.Label12.TabIndex = 71
            Me.Label12.Text = "Exportable"
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Location = New System.Drawing.Point(12, 92)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(91, 13)
            Me.Label10.TabIndex = 69
            Me.Label10.Text = "Campo Busqueda"
            '
            'Campo_Busqueda_CheckBox
            '
            Me.Campo_Busqueda_CheckBox.AutoSize = True
            Me.Campo_Busqueda_CheckBox.Location = New System.Drawing.Point(554, 15)
            Me.Campo_Busqueda_CheckBox.Name = "Campo_Busqueda_CheckBox"
            Me.Campo_Busqueda_CheckBox.Size = New System.Drawing.Size(15, 14)
            Me.Campo_Busqueda_CheckBox.TabIndex = 67
            Me.Campo_Busqueda_CheckBox.UseVisualStyleBackColor = True
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Location = New System.Drawing.Point(401, 15)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(106, 13)
            Me.Label9.TabIndex = 68
            Me.Label9.Text = "Es Campo Busqueda"
            '
            'Campo_Lista_ComboBox
            '
            Me.Campo_Lista_ComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.Campo_Lista_ComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.Campo_Lista_ComboBox.DisabledEnter = False
            Me.Campo_Lista_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Campo_Lista_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Campo_Lista_ComboBox.FormattingEnabled = True
            Me.Campo_Lista_ComboBox.Location = New System.Drawing.Point(163, 63)
            Me.Campo_Lista_ComboBox.Name = "Campo_Lista_ComboBox"
            Me.Campo_Lista_ComboBox.Size = New System.Drawing.Size(214, 21)
            Me.Campo_Lista_ComboBox.TabIndex = 59
            '
            'Campo_Tipo_ComboBox
            '
            Me.Campo_Tipo_ComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.Campo_Tipo_ComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.Campo_Tipo_ComboBox.DisabledEnter = False
            Me.Campo_Tipo_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Campo_Tipo_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Campo_Tipo_ComboBox.FormattingEnabled = True
            Me.Campo_Tipo_ComboBox.Location = New System.Drawing.Point(163, 37)
            Me.Campo_Tipo_ComboBox.Name = "Campo_Tipo_ComboBox"
            Me.Campo_Tipo_ComboBox.Size = New System.Drawing.Size(214, 21)
            Me.Campo_Tipo_ComboBox.TabIndex = 57
            '
            'Usa_Decimales_CheckBox
            '
            Me.Usa_Decimales_CheckBox.AutoSize = True
            Me.Usa_Decimales_CheckBox.Location = New System.Drawing.Point(554, 126)
            Me.Usa_Decimales_CheckBox.Name = "Usa_Decimales_CheckBox"
            Me.Usa_Decimales_CheckBox.Size = New System.Drawing.Size(15, 14)
            Me.Usa_Decimales_CheckBox.TabIndex = 65
            Me.Usa_Decimales_CheckBox.UseVisualStyleBackColor = True
            '
            'Nombre_Campo_TextBox
            '
            Me.Nombre_Campo_TextBox.Location = New System.Drawing.Point(163, 12)
            Me.Nombre_Campo_TextBox.MaxLength = 99
            Me.Nombre_Campo_TextBox.Name = "Nombre_Campo_TextBox"
            Me.Nombre_Campo_TextBox.Size = New System.Drawing.Size(214, 20)
            Me.Nombre_Campo_TextBox.TabIndex = 54
            '
            'Obligatorio_CheckBox
            '
            Me.Obligatorio_CheckBox.AutoSize = True
            Me.Obligatorio_CheckBox.Location = New System.Drawing.Point(554, 45)
            Me.Obligatorio_CheckBox.Name = "Obligatorio_CheckBox"
            Me.Obligatorio_CheckBox.Size = New System.Drawing.Size(15, 14)
            Me.Obligatorio_CheckBox.TabIndex = 61
            Me.Obligatorio_CheckBox.UseVisualStyleBackColor = True
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Location = New System.Drawing.Point(12, 145)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(101, 13)
            Me.Label8.TabIndex = 66
            Me.Label8.Text = "Cantidad Decimales"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(401, 126)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(78, 13)
            Me.Label6.TabIndex = 63
            Me.Label6.Text = "Usa Decimales"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(401, 45)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(57, 13)
            Me.Label5.TabIndex = 62
            Me.Label5.Text = "Obligatorio"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(12, 119)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(84, 13)
            Me.Label4.TabIndex = 60
            Me.Label4.Text = "Longitud Campo"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(12, 66)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(65, 13)
            Me.Label3.TabIndex = 58
            Me.Label3.Text = "Campo Lista"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(12, 40)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(64, 13)
            Me.Label2.TabIndex = 56
            Me.Label2.Text = "Campo Tipo"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(12, 15)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(80, 13)
            Me.Label1.TabIndex = 55
            Me.Label1.Text = "Nombre Campo"
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
            Me.GuardarButton.Image = CType(resources.GetObject("GuardarButton.Image"), System.Drawing.Image)
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(318, 229)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(122, 30)
            Me.GuardarButton.TabIndex = 87
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
            Me.CerrarButton.Location = New System.Drawing.Point(449, 229)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(122, 30)
            Me.CerrarButton.TabIndex = 88
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormNuevaTablaAsociada
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(584, 271)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.Cantidad_Decimales_TextBox)
            Me.Controls.Add(Me.Longitud_Campo_TextBox)
            Me.Controls.Add(Me.IdDocumentoTextBox)
            Me.Controls.Add(Me.CampoTipoTextBox)
            Me.Controls.Add(Me.CampoListaTextBox)
            Me.Controls.Add(Me.id_CampoTextBox)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Valor_Defecto_TextBox)
            Me.Controls.Add(Me.Caracter_Decimal_TextBox)
            Me.Controls.Add(Me.Eliminado_CheckBox)
            Me.Controls.Add(Me.Exportable_CheckBox)
            Me.Controls.Add(Me.Campo_Busqueda_ComboBox)
            Me.Controls.Add(Me.Label19)
            Me.Controls.Add(Me.Label15)
            Me.Controls.Add(Me.Label14)
            Me.Controls.Add(Me.Label12)
            Me.Controls.Add(Me.Label10)
            Me.Controls.Add(Me.Campo_Busqueda_CheckBox)
            Me.Controls.Add(Me.Label9)
            Me.Controls.Add(Me.Campo_Lista_ComboBox)
            Me.Controls.Add(Me.Campo_Tipo_ComboBox)
            Me.Controls.Add(Me.Usa_Decimales_CheckBox)
            Me.Controls.Add(Me.Nombre_Campo_TextBox)
            Me.Controls.Add(Me.Obligatorio_CheckBox)
            Me.Controls.Add(Me.Label8)
            Me.Controls.Add(Me.Label6)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.Name = "FormNuevaTablaAsociada"
            Me.Text = "Campo Tabla Asociada"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents Cantidad_Decimales_TextBox As Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl
        Friend WithEvents Longitud_Campo_TextBox As Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl
        Friend WithEvents IdDocumentoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CampoTipoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CampoListaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents id_CampoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Valor_Defecto_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Caracter_Decimal_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Eliminado_CheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Exportable_CheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Campo_Busqueda_ComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label19 As System.Windows.Forms.Label
        Friend WithEvents Label15 As System.Windows.Forms.Label
        Friend WithEvents Label14 As System.Windows.Forms.Label
        Friend WithEvents Label12 As System.Windows.Forms.Label
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents Campo_Busqueda_CheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents Campo_Lista_ComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Campo_Tipo_ComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Usa_Decimales_CheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Nombre_Campo_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Obligatorio_CheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace