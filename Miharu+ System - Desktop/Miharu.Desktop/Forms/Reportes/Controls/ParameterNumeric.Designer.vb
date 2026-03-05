Namespace Forms.Reportes.Controls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ParameterNumeric
        Inherits System.Windows.Forms.UserControl

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
            Me.nullPanel = New System.Windows.Forms.Panel()
            Me.nullCheckBox = New System.Windows.Forms.CheckBox()
            Me.valueTextBox = New Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl()
            Me.nullPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'nullPanel
            '
            Me.nullPanel.Controls.Add(Me.nullCheckBox)
            Me.nullPanel.Dock = System.Windows.Forms.DockStyle.Right
            Me.nullPanel.Location = New System.Drawing.Point(253, 0)
            Me.nullPanel.Name = "nullPanel"
            Me.nullPanel.Size = New System.Drawing.Size(63, 21)
            Me.nullPanel.TabIndex = 2
            '
            'nullCheckBox
            '
            Me.nullCheckBox.Dock = System.Windows.Forms.DockStyle.Right
            Me.nullCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.nullCheckBox.Location = New System.Drawing.Point(11, 0)
            Me.nullCheckBox.MaximumSize = New System.Drawing.Size(52, 22)
            Me.nullCheckBox.Name = "nullCheckBox"
            Me.nullCheckBox.Size = New System.Drawing.Size(52, 21)
            Me.nullCheckBox.TabIndex = 2
            Me.nullCheckBox.Text = "Nulo"
            Me.nullCheckBox.UseVisualStyleBackColor = True
            '
            'valueTextBox
            '
            Me.valueTextBox.CantidadDecimales = CType(0, Short)
            Me.valueTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.valueTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.valueTextBox.FocusOut = System.Drawing.Color.White
            Me.valueTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.valueTextBox.Location = New System.Drawing.Point(0, 0)
            Me.valueTextBox.MaxValue = 0.0R
            Me.valueTextBox.MinValue = 0.0R
            Me.valueTextBox.Name = "valueTextBox"
            Me.valueTextBox.Size = New System.Drawing.Size(253, 20)
            Me.valueTextBox.TabIndex = 0
            Me.valueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.valueTextBox.UsaDecimales = False
            Me.valueTextBox.UsaRango = False
            '
            'ParameterNumeric
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.valueTextBox)
            Me.Controls.Add(Me.nullPanel)
            Me.Name = "ParameterNumeric"
            Me.Size = New System.Drawing.Size(316, 21)
            Me.nullPanel.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents valueTextBox As Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl
        Friend WithEvents nullPanel As System.Windows.Forms.Panel
        Friend WithEvents nullCheckBox As System.Windows.Forms.CheckBox

    End Class
End Namespace