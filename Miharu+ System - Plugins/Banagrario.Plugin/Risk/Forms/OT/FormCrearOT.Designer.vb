Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Risk.Forms.OT

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCrearOT
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
            Me.FechaOperacionDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.EsquemaComboBox = New DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.SedeComboBox = New DesktopComboBoxControl()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'FechaOperacionDateTimePicker
            '
            Me.FechaOperacionDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.FechaOperacionDateTimePicker.Location = New System.Drawing.Point(12, 141)
            Me.FechaOperacionDateTimePicker.Name = "FechaOperacionDateTimePicker"
            Me.FechaOperacionDateTimePicker.Size = New System.Drawing.Size(166, 21)
            Me.FechaOperacionDateTimePicker.TabIndex = 47
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(15, 119)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(100, 13)
            Me.Label2.TabIndex = 46
            Me.Label2.Text = "Fecha Operación"
            '
            'EsquemaComboBox
            '
            Me.EsquemaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaComboBox.DisabledEnter = False
            Me.EsquemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaComboBox.FormattingEnabled = True
            Me.EsquemaComboBox.Location = New System.Drawing.Point(12, 33)
            Me.EsquemaComboBox.Name = "EsquemaComboBox"
            Me.EsquemaComboBox.Size = New System.Drawing.Size(436, 21)
            Me.EsquemaComboBox.TabIndex = 42
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(14, 15)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(135, 13)
            Me.Label1.TabIndex = 45
            Me.Label1.Text = "Esquema de Operacion"
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(354, 188)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(94, 30)
            Me.CerrarButton.TabIndex = 44
            Me.CerrarButton.Text = "    &Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(236, 188)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(88, 30)
            Me.AceptarButton.TabIndex = 43
            Me.AceptarButton.Text = "    &Aceptar"
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.SedeComboBox)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.AceptarButton)
            Me.GroupBox1.Controls.Add(Me.CerrarButton)
            Me.GroupBox1.Controls.Add(Me.FechaOperacionDateTimePicker)
            Me.GroupBox1.Controls.Add(Me.EsquemaComboBox)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(468, 224)
            Me.GroupBox1.TabIndex = 48
            Me.GroupBox1.TabStop = False
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(14, 65)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(45, 13)
            Me.Label3.TabIndex = 49
            Me.Label3.Text = "Ciudad"
            '
            'SedeComboBox
            '
            Me.SedeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SedeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SedeComboBox.DisabledEnter = False
            Me.SedeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SedeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SedeComboBox.FormattingEnabled = True
            Me.SedeComboBox.Location = New System.Drawing.Point(12, 83)
            Me.SedeComboBox.Name = "SedeComboBox"
            Me.SedeComboBox.Size = New System.Drawing.Size(436, 21)
            Me.SedeComboBox.TabIndex = 48
            '
            'FormCrearOT
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(484, 252)
            Me.Controls.Add(Me.GroupBox1)
            Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCrearOT"
            Me.Padding = New System.Windows.Forms.Padding(3)
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Crear OT"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents FechaOperacionDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents EsquemaComboBox As DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents SedeComboBox As DesktopComboBoxControl
    End Class
End Namespace