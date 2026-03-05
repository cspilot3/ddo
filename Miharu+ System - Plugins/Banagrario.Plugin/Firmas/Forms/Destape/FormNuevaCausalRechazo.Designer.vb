Namespace Firmas.Forms.Destape
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNuevaCausalRechazo))
            Me.CheckBoxActivo = New System.Windows.Forms.CheckBox()
            Me.lblActivo = New System.Windows.Forms.Label()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CausalRechazoTextBox = New System.Windows.Forms.TextBox()
            Me.lblCausalRechazo = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'CheckBoxActivo
            '
            Me.CheckBoxActivo.AutoSize = True
            Me.CheckBoxActivo.Location = New System.Drawing.Point(120, 60)
            Me.CheckBoxActivo.Name = "CheckBoxActivo"
            Me.CheckBoxActivo.Size = New System.Drawing.Size(15, 14)
            Me.CheckBoxActivo.TabIndex = 49
            Me.CheckBoxActivo.UseVisualStyleBackColor = True
            '
            'lblActivo
            '
            Me.lblActivo.AutoSize = True
            Me.lblActivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblActivo.Location = New System.Drawing.Point(43, 59)
            Me.lblActivo.Name = "lblActivo"
            Me.lblActivo.Size = New System.Drawing.Size(65, 13)
            Me.lblActivo.TabIndex = 48
            Me.lblActivo.Text = "Eliminada:"
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(323, 60)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 24)
            Me.CancelarButton.TabIndex = 47
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'GuardarButton
            '
            Me.GuardarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GuardarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(237, 60)
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
            'FormNuevaCausalRechazo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(427, 97)
            Me.Controls.Add(Me.CheckBoxActivo)
            Me.Controls.Add(Me.lblActivo)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CausalRechazoTextBox)
            Me.Controls.Add(Me.lblCausalRechazo)
            Me.Name = "FormNuevaCausalRechazo"
            Me.Text = "Nueva Causal Rechazo"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents CheckBoxActivo As System.Windows.Forms.CheckBox
        Friend WithEvents lblActivo As System.Windows.Forms.Label
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CausalRechazoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents lblCausalRechazo As System.Windows.Forms.Label
    End Class
End Namespace
