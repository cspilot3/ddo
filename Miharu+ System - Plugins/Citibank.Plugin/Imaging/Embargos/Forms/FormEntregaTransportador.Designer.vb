Namespace Embargos.Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEntregaTransportador
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
            Me.ResultadoTextBox = New System.Windows.Forms.TextBox()
            Me.NoGuiaTextBox = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.CbarrasCartaTextBox = New System.Windows.Forms.TextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.ResultadoTextBox)
            Me.GroupBox1.Location = New System.Drawing.Point(8, 104)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(325, 72)
            Me.GroupBox1.TabIndex = 7
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Resultado:"
            '
            'ResultadoTextBox
            '
            Me.ResultadoTextBox.BackColor = System.Drawing.SystemColors.ButtonFace
            Me.ResultadoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.ResultadoTextBox.Location = New System.Drawing.Point(7, 17)
            Me.ResultadoTextBox.Multiline = True
            Me.ResultadoTextBox.Name = "ResultadoTextBox"
            Me.ResultadoTextBox.ReadOnly = True
            Me.ResultadoTextBox.Size = New System.Drawing.Size(312, 47)
            Me.ResultadoTextBox.TabIndex = 1
            '
            'NoGuiaTextBox
            '
            Me.NoGuiaTextBox.Location = New System.Drawing.Point(13, 68)
            Me.NoGuiaTextBox.Name = "NoGuiaTextBox"
            Me.NoGuiaTextBox.Size = New System.Drawing.Size(230, 20)
            Me.NoGuiaTextBox.TabIndex = 6
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(12, 50)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(63, 15)
            Me.Label2.TabIndex = 5
            Me.Label2.Text = "No. Guia"
            '
            'CbarrasCartaTextBox
            '
            Me.CbarrasCartaTextBox.Location = New System.Drawing.Point(13, 27)
            Me.CbarrasCartaTextBox.Name = "CbarrasCartaTextBox"
            Me.CbarrasCartaTextBox.Size = New System.Drawing.Size(230, 20)
            Me.CbarrasCartaTextBox.TabIndex = 4
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(12, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(122, 15)
            Me.Label1.TabIndex = 3
            Me.Label1.Text = "Código de Barras:"
            '
            'FormEntregaTransportador
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(349, 189)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.NoGuiaTextBox)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.CbarrasCartaTextBox)
            Me.Controls.Add(Me.Label1)
            Me.Name = "FormEntregaTransportador"
            Me.Text = "FormEntregaTransportador"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents ResultadoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents NoGuiaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents CbarrasCartaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace

