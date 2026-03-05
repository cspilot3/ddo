Namespace Imaging.Beps.Rechazo_Bizagi
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNuevaCausalRechazo
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
            Me.CheckBoxActivo = New System.Windows.Forms.CheckBox()
            Me.lblActivo = New System.Windows.Forms.Label()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CausalRechazoTextBox = New System.Windows.Forms.TextBox()
            Me.lblCausalRechazo = New System.Windows.Forms.Label()
            Me.btnSalir = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ComboBoxTipoRechazo = New System.Windows.Forms.ComboBox()
            Me.SuspendLayout()
            '
            'CheckBoxActivo
            '
            Me.CheckBoxActivo.AutoSize = True
            Me.CheckBoxActivo.Location = New System.Drawing.Point(120, 94)
            Me.CheckBoxActivo.Name = "CheckBoxActivo"
            Me.CheckBoxActivo.Size = New System.Drawing.Size(15, 14)
            Me.CheckBoxActivo.TabIndex = 49
            Me.CheckBoxActivo.UseVisualStyleBackColor = True
            '
            'lblActivo
            '
            Me.lblActivo.AutoSize = True
            Me.lblActivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblActivo.Location = New System.Drawing.Point(43, 93)
            Me.lblActivo.Name = "lblActivo"
            Me.lblActivo.Size = New System.Drawing.Size(65, 13)
            Me.lblActivo.TabIndex = 48
            Me.lblActivo.Text = "Eliminada:"
            '
            'GuardarButton
            '
            Me.GuardarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GuardarButton.Image = Global.Colpensiones.Plugin.My.Resources.Resources.Process_Accept
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(236, 94)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(80, 24)
            Me.GuardarButton.TabIndex = 46
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'CausalRechazoTextBox
            '
            Me.CausalRechazoTextBox.Location = New System.Drawing.Point(120, 24)
            Me.CausalRechazoTextBox.Name = "CausalRechazoTextBox"
            Me.CausalRechazoTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
            Me.CausalRechazoTextBox.Size = New System.Drawing.Size(283, 20)
            Me.CausalRechazoTextBox.TabIndex = 45
            '
            'lblCausalRechazo
            '
            Me.lblCausalRechazo.AutoSize = True
            Me.lblCausalRechazo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCausalRechazo.Location = New System.Drawing.Point(11, 27)
            Me.lblCausalRechazo.Name = "lblCausalRechazo"
            Me.lblCausalRechazo.Size = New System.Drawing.Size(103, 13)
            Me.lblCausalRechazo.TabIndex = 44
            Me.lblCausalRechazo.Text = "Causal Rechazo:"
            '
            'btnSalir
            '
            Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnSalir.Image = Global.Colpensiones.Plugin.My.Resources.Resources.btnSalir
            Me.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnSalir.Location = New System.Drawing.Point(322, 94)
            Me.btnSalir.Name = "btnSalir"
            Me.btnSalir.Size = New System.Drawing.Size(80, 23)
            Me.btnSalir.TabIndex = 50
            Me.btnSalir.Text = "&Cancelar"
            Me.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnSalir.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(12, 53)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(90, 13)
            Me.Label1.TabIndex = 52
            Me.Label1.Text = "Tipo Rechazo:"
            '
            'ComboBoxTipoRechazo
            '
            Me.ComboBoxTipoRechazo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboBoxTipoRechazo.FormattingEnabled = True
            Me.ComboBoxTipoRechazo.Location = New System.Drawing.Point(120, 50)
            Me.ComboBoxTipoRechazo.Name = "ComboBoxTipoRechazo"
            Me.ComboBoxTipoRechazo.Size = New System.Drawing.Size(282, 21)
            Me.ComboBoxTipoRechazo.TabIndex = 53
            '
            'FormNuevaCausalRechazo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(427, 131)
            Me.Controls.Add(Me.ComboBoxTipoRechazo)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.btnSalir)
            Me.Controls.Add(Me.CheckBoxActivo)
            Me.Controls.Add(Me.lblActivo)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CausalRechazoTextBox)
            Me.Controls.Add(Me.lblCausalRechazo)
            Me.Name = "FormNuevaCausalRechazo"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Nueva Causal Rechazo"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents CheckBoxActivo As System.Windows.Forms.CheckBox
        Friend WithEvents lblActivo As System.Windows.Forms.Label
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CausalRechazoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents lblCausalRechazo As System.Windows.Forms.Label
        Friend WithEvents btnSalir As System.Windows.Forms.Button
        Friend WithEvents DesktopComboBoxControlTipoRechazo As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ComboBoxTipoRechazo As System.Windows.Forms.ComboBox
    End Class
End Namespace
