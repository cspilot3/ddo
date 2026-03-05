Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Controls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEditarCampo
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
            Dim Rango1 As Rango = New Rango()
            Dim Rango2 As Rango = New Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEditarCampo))
            Me.AnteriorDesktopTextBox = New DesktopTextBoxControl()
            Me.NuevoDesktopTextBox = New DesktopTextBoxControl()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.NuevoComboBox = New DesktopComboBoxControl()
            Me.RadioBtnValfalse = New System.Windows.Forms.RadioButton()
            Me.RadioBtnValtrue = New System.Windows.Forms.RadioButton()
            Me.SuspendLayout()
            '
            'AnteriorDesktopTextBox
            '
            Me.AnteriorDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.AnteriorDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.AnteriorDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.AnteriorDesktopTextBox.DisabledEnter = False
            Me.AnteriorDesktopTextBox.DisabledTab = False
            Me.AnteriorDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.AnteriorDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.AnteriorDesktopTextBox.Location = New System.Drawing.Point(12, 12)
            Me.AnteriorDesktopTextBox.MaximumLength = CType(0, Short)
            Me.AnteriorDesktopTextBox.MaxLength = 0
            Me.AnteriorDesktopTextBox.MinimumLength = CType(0, Short)
            Me.AnteriorDesktopTextBox.Name = "AnteriorDesktopTextBox"
            Rango1.MaxValue = 1.7976931348623157E+308R
            Rango1.MinValue = 0.0R
            Me.AnteriorDesktopTextBox.Rango = Rango1
            Me.AnteriorDesktopTextBox.ReadOnly = True
            Me.AnteriorDesktopTextBox.ShortcutsEnabled = True
            Me.AnteriorDesktopTextBox.Size = New System.Drawing.Size(373, 20)
            Me.AnteriorDesktopTextBox.TabIndex = 1
            Me.AnteriorDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.AnteriorDesktopTextBox.Usa_Decimales = False
            Me.AnteriorDesktopTextBox.Validos_Cantidad_Puntos = False
            Me.AnteriorDesktopTextBox.Visible = False
            Me.AnteriorDesktopTextBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            '
            'NuevoDesktopTextBox
            '
            Me.NuevoDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.NuevoDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.NuevoDesktopTextBox.DisabledEnter = False
            Me.NuevoDesktopTextBox.DisabledTab = False
            Me.NuevoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.NuevoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.NuevoDesktopTextBox.Location = New System.Drawing.Point(12, 38)
            Me.NuevoDesktopTextBox.MaximumLength = CType(0, Short)
            Me.NuevoDesktopTextBox.MaxLength = 0
            Me.NuevoDesktopTextBox.MinimumLength = CType(0, Short)
            Me.NuevoDesktopTextBox.Name = "NuevoDesktopTextBox"
            Rango2.MaxValue = 1.7976931348623157E+308R
            Rango2.MinValue = 0.0R
            Me.NuevoDesktopTextBox.Rango = Rango2
            Me.NuevoDesktopTextBox.ShortcutsEnabled = True
            Me.NuevoDesktopTextBox.Size = New System.Drawing.Size(373, 20)
            Me.NuevoDesktopTextBox.TabIndex = 2
            Me.NuevoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.NuevoDesktopTextBox.Usa_Decimales = False
            Me.NuevoDesktopTextBox.Validos_Cantidad_Puntos = False
            Me.NuevoDesktopTextBox.Visible = False
            Me.NuevoDesktopTextBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancel
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(284, 79)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 24)
            Me.CancelarButton.TabIndex = 4
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'AceptarButton
            '
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.tick
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(188, 79)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 23)
            Me.AceptarButton.TabIndex = 3
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'NuevoComboBox
            '
            Me.NuevoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.NuevoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.NuevoComboBox.DisabledEnter = False
            Me.NuevoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.NuevoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.NuevoComboBox.FormattingEnabled = True
            Me.NuevoComboBox.Location = New System.Drawing.Point(12, 37)
            Me.NuevoComboBox.Name = "NuevoComboBox"
            Me.NuevoComboBox.Size = New System.Drawing.Size(373, 21)
            Me.NuevoComboBox.TabIndex = 6
            Me.NuevoComboBox.Visible = False
            '
            'RadioBtnValfalse
            '
            Me.RadioBtnValfalse.AutoSize = True
            Me.RadioBtnValfalse.Location = New System.Drawing.Point(207, 39)
            Me.RadioBtnValfalse.Name = "RadioBtnValfalse"
            Me.RadioBtnValfalse.Size = New System.Drawing.Size(39, 17)
            Me.RadioBtnValfalse.TabIndex = 10
            Me.RadioBtnValfalse.Text = "No"
            Me.RadioBtnValfalse.UseVisualStyleBackColor = True
            Me.RadioBtnValfalse.Visible = False
            '
            'RadioBtnValtrue
            '
            Me.RadioBtnValtrue.AutoSize = True
            Me.RadioBtnValtrue.Checked = True
            Me.RadioBtnValtrue.Location = New System.Drawing.Point(148, 38)
            Me.RadioBtnValtrue.Name = "RadioBtnValtrue"
            Me.RadioBtnValtrue.Size = New System.Drawing.Size(34, 17)
            Me.RadioBtnValtrue.TabIndex = 9
            Me.RadioBtnValtrue.TabStop = True
            Me.RadioBtnValtrue.Text = "Si"
            Me.RadioBtnValtrue.UseVisualStyleBackColor = True
            Me.RadioBtnValtrue.Visible = False
            '
            'FormEditarCampo
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(400, 112)
            Me.Controls.Add(Me.RadioBtnValfalse)
            Me.Controls.Add(Me.RadioBtnValtrue)
            Me.Controls.Add(Me.NuevoComboBox)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.NuevoDesktopTextBox)
            Me.Controls.Add(Me.AnteriorDesktopTextBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormEditarCampo"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Editar Campo"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Public WithEvents AnteriorDesktopTextBox As DesktopTextBoxControl
        Public WithEvents NuevoDesktopTextBox As DesktopTextBoxControl
        Public WithEvents NuevoComboBox As DesktopComboBoxControl
        Friend WithEvents RadioBtnValfalse As System.Windows.Forms.RadioButton
        Friend WithEvents RadioBtnValtrue As System.Windows.Forms.RadioButton
    End Class
End Namespace