Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Controls

Namespace Imaging.Forms.Parametrización
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNuevoCampoMatrizDocumento
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
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.DocumentoTextBox = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.NombreCampoTextBox = New System.Windows.Forms.TextBox()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.LlaveTextBox = New System.Windows.Forms.TextBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.CampoDesktopComboBox = New DesktopComboBoxControl()
            Me.CampoTablaDesktopComboBox = New DesktopComboBoxControl()
            Me.TipologiaDesktopComboBox = New DesktopComboBoxControl()
            Me.ProductoDesktopCheckBox = New DesktopCheckBox.DesktopCheckBoxControl()
            Me.FechaInicialProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaFinalProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.ColumnaMaskedTextBox = New System.Windows.Forms.MaskedTextBox()
            Me.AplicaFechaFinCheckBox = New System.Windows.Forms.CheckBox()
            Me.Label11 = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(175, 323)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(90, 30)
            Me.GuardarButton.TabIndex = 34
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(271, 323)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 35
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(16, 20)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(71, 13)
            Me.Label1.TabIndex = 36
            Me.Label1.Text = "Documento"
            '
            'DocumentoTextBox
            '
            Me.DocumentoTextBox.Location = New System.Drawing.Point(159, 17)
            Me.DocumentoTextBox.Name = "DocumentoTextBox"
            Me.DocumentoTextBox.ReadOnly = True
            Me.DocumentoTextBox.Size = New System.Drawing.Size(200, 20)
            Me.DocumentoTextBox.TabIndex = 37
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(16, 46)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(55, 13)
            Me.Label2.TabIndex = 38
            Me.Label2.Text = "Columna"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(16, 72)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(45, 13)
            Me.Label3.TabIndex = 40
            Me.Label3.Text = "Campo"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(16, 98)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(81, 13)
            Me.Label4.TabIndex = 42
            Me.Label4.Text = "Campo Tabla"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label5.Location = New System.Drawing.Point(16, 124)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(61, 13)
            Me.Label5.TabIndex = 44
            Me.Label5.Text = "Tipología"
            '
            'NombreCampoTextBox
            '
            Me.NombreCampoTextBox.Location = New System.Drawing.Point(159, 148)
            Me.NombreCampoTextBox.Name = "NombreCampoTextBox"
            Me.NombreCampoTextBox.Size = New System.Drawing.Size(200, 20)
            Me.NombreCampoTextBox.TabIndex = 47
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label6.Location = New System.Drawing.Point(16, 151)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(92, 13)
            Me.Label6.TabIndex = 46
            Me.Label6.Text = "Nombre Campo"
            '
            'LlaveTextBox
            '
            Me.LlaveTextBox.Location = New System.Drawing.Point(159, 174)
            Me.LlaveTextBox.Name = "LlaveTextBox"
            Me.LlaveTextBox.Size = New System.Drawing.Size(200, 20)
            Me.LlaveTextBox.TabIndex = 49
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label7.Location = New System.Drawing.Point(16, 177)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(38, 13)
            Me.Label7.TabIndex = 48
            Me.Label7.Text = "Llave"
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label8.Location = New System.Drawing.Point(16, 203)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(76, 13)
            Me.Label8.TabIndex = 50
            Me.Label8.Text = "Es Producto"
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label9.Location = New System.Drawing.Point(16, 229)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(127, 13)
            Me.Label9.TabIndex = 52
            Me.Label9.Text = "Fecha Inicio Proceso"
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label10.Location = New System.Drawing.Point(16, 279)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(113, 13)
            Me.Label10.TabIndex = 54
            Me.Label10.Text = "Fecha Fin Proceso"
            '
            'CampoDesktopComboBox
            '
            Me.CampoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.CampoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.CampoDesktopComboBox.DisabledEnter = False
            Me.CampoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CampoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CampoDesktopComboBox.FormattingEnabled = True
            Me.CampoDesktopComboBox.Location = New System.Drawing.Point(159, 69)
            Me.CampoDesktopComboBox.Name = "CampoDesktopComboBox"
            Me.CampoDesktopComboBox.Size = New System.Drawing.Size(200, 21)
            Me.CampoDesktopComboBox.TabIndex = 56
            '
            'CampoTablaDesktopComboBox
            '
            Me.CampoTablaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.CampoTablaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.CampoTablaDesktopComboBox.DisabledEnter = False
            Me.CampoTablaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CampoTablaDesktopComboBox.Enabled = False
            Me.CampoTablaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CampoTablaDesktopComboBox.FormattingEnabled = True
            Me.CampoTablaDesktopComboBox.Location = New System.Drawing.Point(159, 95)
            Me.CampoTablaDesktopComboBox.Name = "CampoTablaDesktopComboBox"
            Me.CampoTablaDesktopComboBox.Size = New System.Drawing.Size(200, 21)
            Me.CampoTablaDesktopComboBox.TabIndex = 57
            '
            'TipologiaDesktopComboBox
            '
            Me.TipologiaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipologiaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipologiaDesktopComboBox.DisabledEnter = False
            Me.TipologiaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipologiaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipologiaDesktopComboBox.FormattingEnabled = True
            Me.TipologiaDesktopComboBox.Location = New System.Drawing.Point(159, 121)
            Me.TipologiaDesktopComboBox.Name = "TipologiaDesktopComboBox"
            Me.TipologiaDesktopComboBox.Size = New System.Drawing.Size(200, 21)
            Me.TipologiaDesktopComboBox.TabIndex = 58
            '
            'ProductoDesktopCheckBox
            '
            Me.ProductoDesktopCheckBox.AutoSize = True
            Me.ProductoDesktopCheckBox.DisabledEnter = False
            Me.ProductoDesktopCheckBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ProductoDesktopCheckBox.FocusOut = System.Drawing.Color.Gainsboro
            Me.ProductoDesktopCheckBox.Location = New System.Drawing.Point(159, 203)
            Me.ProductoDesktopCheckBox.Name = "ProductoDesktopCheckBox"
            Me.ProductoDesktopCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.ProductoDesktopCheckBox.TabIndex = 59
            Me.ProductoDesktopCheckBox.UseVisualStyleBackColor = True
            '
            'FechaInicialProcesoDateTimePicker
            '
            Me.FechaInicialProcesoDateTimePicker.Location = New System.Drawing.Point(159, 229)
            Me.FechaInicialProcesoDateTimePicker.Name = "FechaInicialProcesoDateTimePicker"
            Me.FechaInicialProcesoDateTimePicker.Size = New System.Drawing.Size(200, 20)
            Me.FechaInicialProcesoDateTimePicker.TabIndex = 60
            '
            'FechaFinalProcesoDateTimePicker
            '
            Me.FechaFinalProcesoDateTimePicker.Enabled = False
            Me.FechaFinalProcesoDateTimePicker.Location = New System.Drawing.Point(159, 276)
            Me.FechaFinalProcesoDateTimePicker.Name = "FechaFinalProcesoDateTimePicker"
            Me.FechaFinalProcesoDateTimePicker.Size = New System.Drawing.Size(200, 20)
            Me.FechaFinalProcesoDateTimePicker.TabIndex = 61
            '
            'ColumnaMaskedTextBox
            '
            Me.ColumnaMaskedTextBox.Location = New System.Drawing.Point(159, 43)
            Me.ColumnaMaskedTextBox.Mask = "99999"
            Me.ColumnaMaskedTextBox.Name = "ColumnaMaskedTextBox"
            Me.ColumnaMaskedTextBox.Size = New System.Drawing.Size(202, 20)
            Me.ColumnaMaskedTextBox.TabIndex = 62
            Me.ColumnaMaskedTextBox.ValidatingType = GetType(Integer)
            '
            'AplicaFechaFinCheckBox
            '
            Me.AplicaFechaFinCheckBox.AutoSize = True
            Me.AplicaFechaFinCheckBox.Location = New System.Drawing.Point(159, 256)
            Me.AplicaFechaFinCheckBox.Name = "AplicaFechaFinCheckBox"
            Me.AplicaFechaFinCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.AplicaFechaFinCheckBox.TabIndex = 63
            Me.AplicaFechaFinCheckBox.UseVisualStyleBackColor = True
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label11.Location = New System.Drawing.Point(16, 256)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(102, 13)
            Me.Label11.TabIndex = 64
            Me.Label11.Text = "Aplica Fecha Fin"
            '
            'FormNuevoCampoMatrizDocumento
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(373, 365)
            Me.Controls.Add(Me.Label11)
            Me.Controls.Add(Me.AplicaFechaFinCheckBox)
            Me.Controls.Add(Me.ColumnaMaskedTextBox)
            Me.Controls.Add(Me.FechaFinalProcesoDateTimePicker)
            Me.Controls.Add(Me.FechaInicialProcesoDateTimePicker)
            Me.Controls.Add(Me.ProductoDesktopCheckBox)
            Me.Controls.Add(Me.TipologiaDesktopComboBox)
            Me.Controls.Add(Me.CampoTablaDesktopComboBox)
            Me.Controls.Add(Me.CampoDesktopComboBox)
            Me.Controls.Add(Me.Label10)
            Me.Controls.Add(Me.Label9)
            Me.Controls.Add(Me.Label8)
            Me.Controls.Add(Me.LlaveTextBox)
            Me.Controls.Add(Me.Label7)
            Me.Controls.Add(Me.NombreCampoTextBox)
            Me.Controls.Add(Me.Label6)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.DocumentoTextBox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Name = "FormNuevoCampoMatrizDocumento"
            Me.Text = "Campo Matriz Documento"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents DocumentoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents NombreCampoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents LlaveTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents CampoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents CampoTablaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents TipologiaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents ProductoDesktopCheckBox As DesktopCheckBox.DesktopCheckBoxControl
        Friend WithEvents FechaInicialProcesoDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaFinalProcesoDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents ColumnaMaskedTextBox As System.Windows.Forms.MaskedTextBox
        Friend WithEvents AplicaFechaFinCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label11 As System.Windows.Forms.Label
    End Class
End Namespace