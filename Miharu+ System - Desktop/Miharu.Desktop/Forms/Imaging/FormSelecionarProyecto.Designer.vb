<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSelecionarProyecto
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CargarLogLlavesRadioButton = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CargarLogRadioButton = New System.Windows.Forms.RadioButton()
        Me.SeleccionfechasRadioButton = New System.Windows.Forms.RadioButton()
        Me.ProyectoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.EntidadComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CancelarButton = New System.Windows.Forms.Button()
        Me.AceptarButton = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CargarLogLlavesRadioButton)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.CargarLogRadioButton)
        Me.GroupBox1.Controls.Add(Me.SeleccionfechasRadioButton)
        Me.GroupBox1.Controls.Add(Me.ProyectoComboBox)
        Me.GroupBox1.Controls.Add(Me.EntidadComboBox)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 19)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(346, 205)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'CargarLogLlavesRadioButton
        '
        Me.CargarLogLlavesRadioButton.AutoSize = True
        Me.CargarLogLlavesRadioButton.Location = New System.Drawing.Point(12, 174)
        Me.CargarLogLlavesRadioButton.Name = "CargarLogLlavesRadioButton"
        Me.CargarLogLlavesRadioButton.Size = New System.Drawing.Size(206, 17)
        Me.CargarLogLlavesRadioButton.TabIndex = 7
        Me.CargarLogLlavesRadioButton.TabStop = True
        Me.CargarLogLlavesRadioButton.Text = "Cargar log de llaves y exportar clientes"
        Me.CargarLogLlavesRadioButton.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 132)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(157, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Opciones de Exportacion: "
        '
        'CargarLogRadioButton
        '
        Me.CargarLogRadioButton.AutoSize = True
        Me.CargarLogRadioButton.Location = New System.Drawing.Point(191, 151)
        Me.CargarLogRadioButton.Name = "CargarLogRadioButton"
        Me.CargarLogRadioButton.Size = New System.Drawing.Size(122, 17)
        Me.CargarLogRadioButton.TabIndex = 5
        Me.CargarLogRadioButton.TabStop = True
        Me.CargarLogRadioButton.Text = "Cargar log y exportar"
        Me.CargarLogRadioButton.UseVisualStyleBackColor = True
        '
        'SeleccionfechasRadioButton
        '
        Me.SeleccionfechasRadioButton.AutoSize = True
        Me.SeleccionfechasRadioButton.Location = New System.Drawing.Point(12, 151)
        Me.SeleccionfechasRadioButton.Name = "SeleccionfechasRadioButton"
        Me.SeleccionfechasRadioButton.Size = New System.Drawing.Size(125, 17)
        Me.SeleccionfechasRadioButton.TabIndex = 4
        Me.SeleccionfechasRadioButton.TabStop = True
        Me.SeleccionfechasRadioButton.Text = "Seleccion de Fechas"
        Me.SeleccionfechasRadioButton.UseVisualStyleBackColor = True
        '
        'ProyectoComboBox
        '
        Me.ProyectoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ProyectoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ProyectoComboBox.DisabledEnter = False
        Me.ProyectoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ProyectoComboBox.fk_Campo = 0
        Me.ProyectoComboBox.fk_Documento = 0
        Me.ProyectoComboBox.fk_Validacion = 0
        Me.ProyectoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ProyectoComboBox.FormattingEnabled = True
        Me.ProyectoComboBox.Location = New System.Drawing.Point(12, 85)
        Me.ProyectoComboBox.Name = "ProyectoComboBox"
        Me.ProyectoComboBox.Size = New System.Drawing.Size(317, 21)
        Me.ProyectoComboBox.TabIndex = 3
        '
        'EntidadComboBox
        '
        Me.EntidadComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.EntidadComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.EntidadComboBox.DisabledEnter = False
        Me.EntidadComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EntidadComboBox.fk_Campo = 0
        Me.EntidadComboBox.fk_Documento = 0
        Me.EntidadComboBox.fk_Validacion = 0
        Me.EntidadComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EntidadComboBox.FormattingEnabled = True
        Me.EntidadComboBox.Location = New System.Drawing.Point(12, 32)
        Me.EntidadComboBox.Name = "EntidadComboBox"
        Me.EntidadComboBox.Size = New System.Drawing.Size(317, 21)
        Me.EntidadComboBox.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(9, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Proyecto:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(9, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Entidad Cliente:"
        '
        'CancelarButton
        '
        Me.CancelarButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelarButton.Location = New System.Drawing.Point(268, 230)
        Me.CancelarButton.Name = "CancelarButton"
        Me.CancelarButton.Size = New System.Drawing.Size(88, 32)
        Me.CancelarButton.TabIndex = 7
        Me.CancelarButton.Text = "&Cancelar"
        Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelarButton.UseVisualStyleBackColor = True
        '
        'AceptarButton
        '
        Me.AceptarButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AceptarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AceptarButton.Location = New System.Drawing.Point(176, 230)
        Me.AceptarButton.Name = "AceptarButton"
        Me.AceptarButton.Size = New System.Drawing.Size(86, 32)
        Me.AceptarButton.TabIndex = 6
        Me.AceptarButton.Text = "&Aceptar"
        Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AceptarButton.UseVisualStyleBackColor = True
        '
        'FormSelecionarProyecto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(368, 274)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CancelarButton)
        Me.Controls.Add(Me.AceptarButton)
        Me.Name = "FormSelecionarProyecto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Proyecto"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents EntidadComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CancelarButton As System.Windows.Forms.Button
    Friend WithEvents AceptarButton As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CargarLogRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents SeleccionfechasRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents ProyectoComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    Friend WithEvents CargarLogLlavesRadioButton As System.Windows.Forms.RadioButton
End Class
