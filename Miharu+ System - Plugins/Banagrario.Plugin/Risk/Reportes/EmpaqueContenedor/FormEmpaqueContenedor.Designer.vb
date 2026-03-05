Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Risk.Reportes.EmpaqueContenedor
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEmpaqueContenedor
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEmpaqueContenedor))
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.Fecha2DateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.lbl_Sede = New System.Windows.Forms.Label()
            Me.Fecha1DateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.SedeDesktopComboBox = New DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'BuscarButton
            '
            Me.BuscarButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.BuscarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Aceptar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(427, 36)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(83, 30)
            Me.BuscarButton.TabIndex = 29
            Me.BuscarButton.Text = "&Aceptar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'Fecha2DateTimePicker
            '
            Me.Fecha2DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.Fecha2DateTimePicker.Location = New System.Drawing.Point(271, 29)
            Me.Fecha2DateTimePicker.Name = "Fecha2DateTimePicker"
            Me.Fecha2DateTimePicker.Size = New System.Drawing.Size(96, 20)
            Me.Fecha2DateTimePicker.TabIndex = 34
            '
            'lbl_Sede
            '
            Me.lbl_Sede.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.lbl_Sede.AutoSize = True
            Me.lbl_Sede.Location = New System.Drawing.Point(13, 60)
            Me.lbl_Sede.Name = "lbl_Sede"
            Me.lbl_Sede.Size = New System.Drawing.Size(108, 13)
            Me.lbl_Sede.TabIndex = 30
            Me.lbl_Sede.Text = "Sede Procesamiento:"
            '
            'Fecha1DateTimePicker
            '
            Me.Fecha1DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.Fecha1DateTimePicker.Location = New System.Drawing.Point(165, 30)
            Me.Fecha1DateTimePicker.Name = "Fecha1DateTimePicker"
            Me.Fecha1DateTimePicker.Size = New System.Drawing.Size(100, 20)
            Me.Fecha1DateTimePicker.TabIndex = 33
            '
            'SedeDesktopComboBox
            '
            Me.SedeDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.SedeDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SedeDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SedeDesktopComboBox.DisabledEnter = False
            Me.SedeDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SedeDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SedeDesktopComboBox.FormattingEnabled = True
            Me.SedeDesktopComboBox.Location = New System.Drawing.Point(166, 57)
            Me.SedeDesktopComboBox.Name = "SedeDesktopComboBox"
            Me.SedeDesktopComboBox.Size = New System.Drawing.Size(202, 21)
            Me.SedeDesktopComboBox.TabIndex = 31
            '
            'Label1
            '
            Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(13, 28)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(104, 13)
            Me.Label1.TabIndex = 32
            Me.Label1.Text = "Fecha Proceso PyC:"
            '
            'FormEmpaqueContenedor
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(549, 110)
            Me.Controls.Add(Me.BuscarButton)
            Me.Controls.Add(Me.Fecha2DateTimePicker)
            Me.Controls.Add(Me.lbl_Sede)
            Me.Controls.Add(Me.Fecha1DateTimePicker)
            Me.Controls.Add(Me.SedeDesktopComboBox)
            Me.Controls.Add(Me.Label1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormEmpaqueContenedor"
            Me.ShowInTaskbar = False
            Me.Text = "Empaque Contenedor"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents Fecha2DateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents lbl_Sede As System.Windows.Forms.Label
        Friend WithEvents Fecha1DateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents SedeDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace