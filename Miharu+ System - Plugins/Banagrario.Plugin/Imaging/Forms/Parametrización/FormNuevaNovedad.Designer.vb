Namespace Imaging.Forms.Parametrización
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNuevaNovedad
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNuevaNovedad))
            Me.NovedadTextBox = New System.Windows.Forms.TextBox()
            Me.lblNovedad = New System.Windows.Forms.Label()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.lblActivo = New System.Windows.Forms.Label()
            Me.CheckBoxActivo = New System.Windows.Forms.CheckBox()
            Me.SuspendLayout()
            '
            'NovedadTextBox
            '
            Me.NovedadTextBox.Location = New System.Drawing.Point(86, 30)
            Me.NovedadTextBox.Name = "NovedadTextBox"
            Me.NovedadTextBox.Size = New System.Drawing.Size(283, 20)
            Me.NovedadTextBox.TabIndex = 35
            '
            'lblNovedad
            '
            Me.lblNovedad.AutoSize = True
            Me.lblNovedad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblNovedad.Location = New System.Drawing.Point(13, 33)
            Me.lblNovedad.Name = "lblNovedad"
            Me.lblNovedad.Size = New System.Drawing.Size(58, 13)
            Me.lblNovedad.TabIndex = 34
            Me.lblNovedad.Text = "Novedad"
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(286, 83)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 24)
            Me.CancelarButton.TabIndex = 41
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'AceptarButton
            '
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnGuardar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(200, 83)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 24)
            Me.AceptarButton.TabIndex = 40
            Me.AceptarButton.Text = "&Guardar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblActivo
            '
            Me.lblActivo.AutoSize = True
            Me.lblActivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblActivo.Location = New System.Drawing.Point(13, 63)
            Me.lblActivo.Name = "lblActivo"
            Me.lblActivo.Size = New System.Drawing.Size(61, 13)
            Me.lblActivo.TabIndex = 42
            Me.lblActivo.Text = "Eliminada"
            '
            'CheckBoxActivo
            '
            Me.CheckBoxActivo.AutoSize = True
            Me.CheckBoxActivo.Location = New System.Drawing.Point(86, 62)
            Me.CheckBoxActivo.Name = "CheckBoxActivo"
            Me.CheckBoxActivo.Size = New System.Drawing.Size(15, 14)
            Me.CheckBoxActivo.TabIndex = 43
            Me.CheckBoxActivo.UseVisualStyleBackColor = True
            '
            'FormNuevaNovedad
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(388, 134)
            Me.Controls.Add(Me.CheckBoxActivo)
            Me.Controls.Add(Me.lblActivo)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.NovedadTextBox)
            Me.Controls.Add(Me.lblNovedad)
            Me.Name = "FormNuevaNovedad"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Novedad"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents NovedadTextBox As System.Windows.Forms.TextBox
        Friend WithEvents lblNovedad As System.Windows.Forms.Label
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents lblActivo As System.Windows.Forms.Label
        Friend WithEvents CheckBoxActivo As System.Windows.Forms.CheckBox
    End Class
End Namespace