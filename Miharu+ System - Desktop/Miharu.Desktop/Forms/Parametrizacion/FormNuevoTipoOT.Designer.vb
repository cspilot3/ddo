Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNuevoTipoOT
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
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.DescripcionReporteDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.NombreDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.EntidadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ProyectoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(236, 141)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(105, 30)
            Me.GuardarButton.TabIndex = 98
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
            Me.CerrarButton.Location = New System.Drawing.Point(348, 141)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(105, 30)
            Me.CerrarButton.TabIndex = 97
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Location = New System.Drawing.Point(14, 101)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(72, 13)
            Me.Label10.TabIndex = 96
            Me.Label10.Text = "Descripción"
            '
            'DescripcionReporteDesktopTextBox
            '
            Me.DescripcionReporteDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.DescripcionReporteDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.DescripcionReporteDesktopTextBox.DateFormat = Nothing
            Me.DescripcionReporteDesktopTextBox.DisabledEnter = False
            Me.DescripcionReporteDesktopTextBox.DisabledTab = False
            Me.DescripcionReporteDesktopTextBox.EnabledShortCuts = False
            Me.DescripcionReporteDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.DescripcionReporteDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.DescripcionReporteDesktopTextBox.Location = New System.Drawing.Point(106, 98)
            Me.DescripcionReporteDesktopTextBox.MaskedTextBox_Property = ""
            Me.DescripcionReporteDesktopTextBox.MaximumLength = CType(0, Short)
            Me.DescripcionReporteDesktopTextBox.MinimumLength = CType(0, Short)
            Me.DescripcionReporteDesktopTextBox.Name = "DescripcionReporteDesktopTextBox"
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.DescripcionReporteDesktopTextBox.Rango = Rango1
            Me.DescripcionReporteDesktopTextBox.Size = New System.Drawing.Size(346, 21)
            Me.DescripcionReporteDesktopTextBox.TabIndex = 91
            Me.DescripcionReporteDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.DescripcionReporteDesktopTextBox.Usa_Decimales = False
            Me.DescripcionReporteDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(14, 75)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(51, 13)
            Me.Label4.TabIndex = 86
            Me.Label4.Text = "Nombre"
            '
            'NombreDesktopTextBox
            '
            Me.NombreDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.NombreDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.NombreDesktopTextBox.DateFormat = Nothing
            Me.NombreDesktopTextBox.DisabledEnter = False
            Me.NombreDesktopTextBox.DisabledTab = False
            Me.NombreDesktopTextBox.EnabledShortCuts = False
            Me.NombreDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.NombreDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.NombreDesktopTextBox.Location = New System.Drawing.Point(106, 72)
            Me.NombreDesktopTextBox.MaskedTextBox_Property = ""
            Me.NombreDesktopTextBox.MaximumLength = CType(100, Short)
            Me.NombreDesktopTextBox.MinimumLength = CType(0, Short)
            Me.NombreDesktopTextBox.Name = "NombreDesktopTextBox"
            Rango2.MaxValue = 2147483647.0R
            Rango2.MinValue = 0.0R
            Me.NombreDesktopTextBox.Rango = Rango2
            Me.NombreDesktopTextBox.Size = New System.Drawing.Size(346, 21)
            Me.NombreDesktopTextBox.TabIndex = 85
            Me.NombreDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.NombreDesktopTextBox.Usa_Decimales = False
            Me.NombreDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'EntidadDesktopComboBox
            '
            Me.EntidadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadDesktopComboBox.DisabledEnter = False
            Me.EntidadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadDesktopComboBox.FormattingEnabled = True
            Me.EntidadDesktopComboBox.Location = New System.Drawing.Point(106, 16)
            Me.EntidadDesktopComboBox.Name = "EntidadDesktopComboBox"
            Me.EntidadDesktopComboBox.Size = New System.Drawing.Size(346, 21)
            Me.EntidadDesktopComboBox.TabIndex = 80
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(14, 19)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(52, 13)
            Me.Label1.TabIndex = 79
            Me.Label1.Text = "Entidad:"
            '
            'ProyectoComboBox
            '
            Me.ProyectoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProyectoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProyectoComboBox.DisabledEnter = False
            Me.ProyectoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProyectoComboBox.FormattingEnabled = True
            Me.ProyectoComboBox.Location = New System.Drawing.Point(106, 45)
            Me.ProyectoComboBox.Name = "ProyectoComboBox"
            Me.ProyectoComboBox.Size = New System.Drawing.Size(346, 21)
            Me.ProyectoComboBox.TabIndex = 78
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Location = New System.Drawing.Point(14, 48)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(61, 13)
            Me.Label9.TabIndex = 77
            Me.Label9.Text = "Proyecto:"
            '
            'FormNuevoTipoOT
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(474, 183)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Label10)
            Me.Controls.Add(Me.DescripcionReporteDesktopTextBox)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.NombreDesktopTextBox)
            Me.Controls.Add(Me.EntidadDesktopComboBox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.ProyectoComboBox)
            Me.Controls.Add(Me.Label9)
            Me.Name = "FormNuevoTipoOT"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Nuevo Tipo OT"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents DescripcionReporteDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents NombreDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents EntidadDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ProyectoComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label9 As System.Windows.Forms.Label
    End Class
End Namespace