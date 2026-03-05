Namespace Firmas.Forms.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormValidarTarjetas
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormValidarTarjetas))
            Me.Label2 = New System.Windows.Forms.Label()
            Me.codigo_barras = New System.Windows.Forms.TextBox()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.ExcluirButton = New System.Windows.Forms.Button()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.FechaProcesoPicker = New System.Windows.Forms.DateTimePicker()
            Me.CausalRechazoCkListBox1 = New System.Windows.Forms.CheckedListBox()
            Me.ActualizarButton = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(12, 66)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(103, 13)
            Me.Label2.TabIndex = 44
            Me.Label2.Text = "Código de barras"
            '
            'codigo_barras
            '
            Me.codigo_barras.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.codigo_barras.Location = New System.Drawing.Point(15, 91)
            Me.codigo_barras.Name = "codigo_barras"
            Me.codigo_barras.Size = New System.Drawing.Size(397, 26)
            Me.codigo_barras.TabIndex = 43
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(335, 284)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(77, 23)
            Me.CancelarButton.TabIndex = 46
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'ExcluirButton
            '
            Me.ExcluirButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ExcluirButton.Image = CType(resources.GetObject("ExcluirButton.Image"), System.Drawing.Image)
            Me.ExcluirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ExcluirButton.Location = New System.Drawing.Point(249, 284)
            Me.ExcluirButton.Name = "ExcluirButton"
            Me.ExcluirButton.Size = New System.Drawing.Size(77, 23)
            Me.ExcluirButton.TabIndex = 45
            Me.ExcluirButton.Text = "&Rechazar"
            Me.ExcluirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(12, 130)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(117, 13)
            Me.Label3.TabIndex = 51
            Me.Label3.Text = "Causal de Rechazo"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(12, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(110, 13)
            Me.Label1.TabIndex = 53
            Me.Label1.Text = "Fecha de Proceso"
            '
            'FechaProcesoPicker
            '
            Me.FechaProcesoPicker.Enabled = False
            Me.FechaProcesoPicker.Location = New System.Drawing.Point(15, 34)
            Me.FechaProcesoPicker.Name = "FechaProcesoPicker"
            Me.FechaProcesoPicker.Size = New System.Drawing.Size(214, 20)
            Me.FechaProcesoPicker.TabIndex = 52
            '
            'CausalRechazoCkListBox1
            '
            Me.CausalRechazoCkListBox1.BackColor = System.Drawing.SystemColors.Control
            Me.CausalRechazoCkListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.CausalRechazoCkListBox1.CheckOnClick = True
            Me.CausalRechazoCkListBox1.FormattingEnabled = True
            Me.CausalRechazoCkListBox1.Location = New System.Drawing.Point(44, 158)
            Me.CausalRechazoCkListBox1.Name = "CausalRechazoCkListBox1"
            Me.CausalRechazoCkListBox1.Size = New System.Drawing.Size(368, 120)
            Me.CausalRechazoCkListBox1.TabIndex = 55
            '
            'ActualizarButton
            '
            Me.ActualizarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ActualizarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnGuardar
            Me.ActualizarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ActualizarButton.Location = New System.Drawing.Point(335, 27)
            Me.ActualizarButton.Name = "ActualizarButton"
            Me.ActualizarButton.Size = New System.Drawing.Size(77, 32)
            Me.ActualizarButton.TabIndex = 56
            Me.ActualizarButton.Text = "&Actualizar"
            Me.ActualizarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ActualizarButton.Visible = False
            '
            'FormValidarTarjetas
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(424, 319)
            Me.Controls.Add(Me.ActualizarButton)
            Me.Controls.Add(Me.CausalRechazoCkListBox1)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.FechaProcesoPicker)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.ExcluirButton)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.codigo_barras)
            Me.Name = "FormValidarTarjetas"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Validación de Tarjetas"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents codigo_barras As System.Windows.Forms.TextBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents ExcluirButton As System.Windows.Forms.Button
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoPicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents CausalRechazoCkListBox1 As System.Windows.Forms.CheckedListBox
        Friend WithEvents ActualizarButton As System.Windows.Forms.Button
    End Class
End Namespace