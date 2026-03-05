
Namespace Forms.Desembolsos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCruceDesembolsos
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
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.CruzarButton = New System.Windows.Forms.Button()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.FechaRecolecciondateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaRecoleccionlabel = New System.Windows.Forms.Label()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.CruzarButton)
            Me.GroupBox1.Controls.Add(Me.CancelarButton)
            Me.GroupBox1.Controls.Add(Me.FechaRecolecciondateTimePicker)
            Me.GroupBox1.Controls.Add(Me.FechaRecoleccionlabel)
            Me.GroupBox1.Location = New System.Drawing.Point(14, 21)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(360, 160)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Cruce Desembolsos"
            '
            'CruzarButton
            '
            Me.CruzarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CruzarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CruzarButton.Image = Global.Coomeva.Plugin.My.Resources.Resources.Aceptar
            Me.CruzarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CruzarButton.Location = New System.Drawing.Point(31, 92)
            Me.CruzarButton.Name = "CruzarButton"
            Me.CruzarButton.Size = New System.Drawing.Size(117, 29)
            Me.CruzarButton.TabIndex = 29
            Me.CruzarButton.Text = "&Cruzar"
            Me.CruzarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CruzarButton.UseVisualStyleBackColor = True
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.Coomeva.Plugin.My.Resources.Resources.Cancelar2
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(196, 92)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(117, 29)
            Me.CancelarButton.TabIndex = 28
            Me.CancelarButton.Text = "&Salir"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'FechaRecolecciondateTimePicker
            '
            Me.FechaRecolecciondateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaRecolecciondateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaRecolecciondateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaRecolecciondateTimePicker.Location = New System.Drawing.Point(216, 28)
            Me.FechaRecolecciondateTimePicker.Name = "FechaRecolecciondateTimePicker"
            Me.FechaRecolecciondateTimePicker.Size = New System.Drawing.Size(96, 21)
            Me.FechaRecolecciondateTimePicker.TabIndex = 25
            '
            'FechaRecoleccionlabel
            '
            Me.FechaRecoleccionlabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaRecoleccionlabel.AutoSize = True
            Me.FechaRecoleccionlabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaRecoleccionlabel.Location = New System.Drawing.Point(28, 34)
            Me.FechaRecoleccionlabel.Name = "FechaRecoleccionlabel"
            Me.FechaRecoleccionlabel.Size = New System.Drawing.Size(71, 13)
            Me.FechaRecoleccionlabel.TabIndex = 24
            Me.FechaRecoleccionlabel.Text = "Mes Cruce:"
            '
            'FormCruceDesembolsos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(391, 194)
            Me.Controls.Add(Me.GroupBox1)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCruceDesembolsos"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Cruce Desembolsos"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Private WithEvents FechaRecolecciondateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaRecoleccionlabel As System.Windows.Forms.Label
        Friend WithEvents CruzarButton As System.Windows.Forms.Button
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
    End Class
End Namespace
