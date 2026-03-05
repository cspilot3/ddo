Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Procesos.Configuracion.Imaging

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormAgregarPrecintoCampo
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
            Me.CampoTipoTextBox = New System.Windows.Forms.TextBox()
            Me.CampoListaTextBox = New System.Windows.Forms.TextBox()
            Me.id_CampoTextBox = New System.Windows.Forms.TextBox()
            Me.Campo_Lista_ComboBox = New DesktopComboBoxControl()
            Me.Campo_Tipo_ComboBox = New DesktopComboBoxControl()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Cantidad_Decimales_TextBox = New System.Windows.Forms.TextBox()
            Me.Usa_Decimales_CheckBox = New System.Windows.Forms.CheckBox()
            Me.Longitud_Minima_TextBox = New System.Windows.Forms.TextBox()
            Me.Longitud_Campo_TextBox = New System.Windows.Forms.TextBox()
            Me.Nombre_Campo_TextBox = New System.Windows.Forms.TextBox()
            Me.Obligatorio_CheckBox = New System.Windows.Forms.CheckBox()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.EliminadoCheckBox = New System.Windows.Forms.CheckBox()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'CampoTipoTextBox
            '
            Me.CampoTipoTextBox.Location = New System.Drawing.Point(63, 270)
            Me.CampoTipoTextBox.Name = "CampoTipoTextBox"
            Me.CampoTipoTextBox.Size = New System.Drawing.Size(41, 21)
            Me.CampoTipoTextBox.TabIndex = 33
            Me.CampoTipoTextBox.Visible = False
            '
            'CampoListaTextBox
            '
            Me.CampoListaTextBox.Location = New System.Drawing.Point(112, 270)
            Me.CampoListaTextBox.Name = "CampoListaTextBox"
            Me.CampoListaTextBox.Size = New System.Drawing.Size(41, 21)
            Me.CampoListaTextBox.TabIndex = 32
            Me.CampoListaTextBox.Visible = False
            '
            'id_CampoTextBox
            '
            Me.id_CampoTextBox.Location = New System.Drawing.Point(14, 270)
            Me.id_CampoTextBox.Name = "id_CampoTextBox"
            Me.id_CampoTextBox.Size = New System.Drawing.Size(41, 21)
            Me.id_CampoTextBox.TabIndex = 31
            Me.id_CampoTextBox.Visible = False
            '
            'Campo_Lista_ComboBox
            '
            Me.Campo_Lista_ComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.Campo_Lista_ComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.Campo_Lista_ComboBox.DisabledEnter = False
            Me.Campo_Lista_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Campo_Lista_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Campo_Lista_ComboBox.FormattingEnabled = True
            Me.Campo_Lista_ComboBox.Location = New System.Drawing.Point(210, 72)
            Me.Campo_Lista_ComboBox.Name = "Campo_Lista_ComboBox"
            Me.Campo_Lista_ComboBox.Size = New System.Drawing.Size(249, 21)
            Me.Campo_Lista_ComboBox.TabIndex = 18
            '
            'Campo_Tipo_ComboBox
            '
            Me.Campo_Tipo_ComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.Campo_Tipo_ComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.Campo_Tipo_ComboBox.DisabledEnter = False
            Me.Campo_Tipo_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Campo_Tipo_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Campo_Tipo_ComboBox.FormattingEnabled = True
            Me.Campo_Tipo_ComboBox.Location = New System.Drawing.Point(210, 45)
            Me.Campo_Tipo_ComboBox.Name = "Campo_Tipo_ComboBox"
            Me.Campo_Tipo_ComboBox.Size = New System.Drawing.Size(249, 21)
            Me.Campo_Tipo_ComboBox.TabIndex = 15
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(168, 264)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(142, 30)
            Me.GuardarButton.TabIndex = 29
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(317, 264)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(142, 30)
            Me.CerrarButton.TabIndex = 30
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Cantidad_Decimales_TextBox
            '
            Me.Cantidad_Decimales_TextBox.Location = New System.Drawing.Point(210, 193)
            Me.Cantidad_Decimales_TextBox.MaxLength = 4
            Me.Cantidad_Decimales_TextBox.Name = "Cantidad_Decimales_TextBox"
            Me.Cantidad_Decimales_TextBox.Size = New System.Drawing.Size(249, 21)
            Me.Cantidad_Decimales_TextBox.TabIndex = 27
            '
            'Usa_Decimales_CheckBox
            '
            Me.Usa_Decimales_CheckBox.AutoSize = True
            Me.Usa_Decimales_CheckBox.Location = New System.Drawing.Point(210, 170)
            Me.Usa_Decimales_CheckBox.Name = "Usa_Decimales_CheckBox"
            Me.Usa_Decimales_CheckBox.Size = New System.Drawing.Size(15, 14)
            Me.Usa_Decimales_CheckBox.TabIndex = 25
            Me.Usa_Decimales_CheckBox.UseVisualStyleBackColor = True
            '
            'Longitud_Minima_TextBox
            '
            Me.Longitud_Minima_TextBox.Location = New System.Drawing.Point(210, 144)
            Me.Longitud_Minima_TextBox.MaxLength = 4
            Me.Longitud_Minima_TextBox.Name = "Longitud_Minima_TextBox"
            Me.Longitud_Minima_TextBox.Size = New System.Drawing.Size(249, 21)
            Me.Longitud_Minima_TextBox.TabIndex = 24
            '
            'Longitud_Campo_TextBox
            '
            Me.Longitud_Campo_TextBox.Location = New System.Drawing.Point(210, 118)
            Me.Longitud_Campo_TextBox.MaxLength = 4
            Me.Longitud_Campo_TextBox.Name = "Longitud_Campo_TextBox"
            Me.Longitud_Campo_TextBox.Size = New System.Drawing.Size(249, 21)
            Me.Longitud_Campo_TextBox.TabIndex = 22
            '
            'Nombre_Campo_TextBox
            '
            Me.Nombre_Campo_TextBox.Location = New System.Drawing.Point(210, 19)
            Me.Nombre_Campo_TextBox.MaxLength = 99
            Me.Nombre_Campo_TextBox.Name = "Nombre_Campo_TextBox"
            Me.Nombre_Campo_TextBox.Size = New System.Drawing.Size(249, 21)
            Me.Nombre_Campo_TextBox.TabIndex = 14
            '
            'Obligatorio_CheckBox
            '
            Me.Obligatorio_CheckBox.AutoSize = True
            Me.Obligatorio_CheckBox.Location = New System.Drawing.Point(210, 99)
            Me.Obligatorio_CheckBox.Name = "Obligatorio_CheckBox"
            Me.Obligatorio_CheckBox.Size = New System.Drawing.Size(15, 14)
            Me.Obligatorio_CheckBox.TabIndex = 20
            Me.Obligatorio_CheckBox.UseVisualStyleBackColor = True
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Location = New System.Drawing.Point(28, 196)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(118, 13)
            Me.Label8.TabIndex = 28
            Me.Label8.Text = "Cantidad Decimales"
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(28, 147)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(142, 13)
            Me.Label7.TabIndex = 26
            Me.Label7.Text = "Longitud Minima Campo"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(28, 171)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(89, 13)
            Me.Label6.TabIndex = 23
            Me.Label6.Text = "Usa Decimales"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(28, 100)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(69, 13)
            Me.Label5.TabIndex = 21
            Me.Label5.Text = "Obligatorio"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(28, 121)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(98, 13)
            Me.Label4.TabIndex = 19
            Me.Label4.Text = "Longitud Campo"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(28, 75)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(76, 13)
            Me.Label3.TabIndex = 17
            Me.Label3.Text = "Campo Lista"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(28, 48)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(73, 13)
            Me.Label2.TabIndex = 16
            Me.Label2.Text = "Campo Tipo"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(28, 22)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(93, 13)
            Me.Label1.TabIndex = 13
            Me.Label1.Text = "Nombre Campo"
            '
            'EliminadoCheckBox
            '
            Me.EliminadoCheckBox.AutoSize = True
            Me.EliminadoCheckBox.Location = New System.Drawing.Point(210, 223)
            Me.EliminadoCheckBox.Name = "EliminadoCheckBox"
            Me.EliminadoCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.EliminadoCheckBox.TabIndex = 28
            Me.EliminadoCheckBox.UseVisualStyleBackColor = True
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Location = New System.Drawing.Point(28, 224)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(61, 13)
            Me.Label9.TabIndex = 34
            Me.Label9.Text = "Eliminado"
            '
            'FormAgregarPrecintoCampo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(484, 308)
            Me.Controls.Add(Me.EliminadoCheckBox)
            Me.Controls.Add(Me.Label9)
            Me.Controls.Add(Me.CampoTipoTextBox)
            Me.Controls.Add(Me.CampoListaTextBox)
            Me.Controls.Add(Me.id_CampoTextBox)
            Me.Controls.Add(Me.Campo_Lista_ComboBox)
            Me.Controls.Add(Me.Campo_Tipo_ComboBox)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Cantidad_Decimales_TextBox)
            Me.Controls.Add(Me.Usa_Decimales_CheckBox)
            Me.Controls.Add(Me.Longitud_Minima_TextBox)
            Me.Controls.Add(Me.Longitud_Campo_TextBox)
            Me.Controls.Add(Me.Nombre_Campo_TextBox)
            Me.Controls.Add(Me.Obligatorio_CheckBox)
            Me.Controls.Add(Me.Label8)
            Me.Controls.Add(Me.Label7)
            Me.Controls.Add(Me.Label6)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormAgregarPrecintoCampo"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Agregar Campo Precinto"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents CampoTipoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CampoListaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents id_CampoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Campo_Lista_ComboBox As DesktopComboBoxControl
        Friend WithEvents Campo_Tipo_ComboBox As DesktopComboBoxControl
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Cantidad_Decimales_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Usa_Decimales_CheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Longitud_Minima_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Longitud_Campo_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Nombre_Campo_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents Obligatorio_CheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents EliminadoCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label9 As System.Windows.Forms.Label
    End Class

End Namespace