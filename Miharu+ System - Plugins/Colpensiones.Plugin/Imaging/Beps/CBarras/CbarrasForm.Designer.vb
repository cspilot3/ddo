Namespace Imaging.Beps.CBarras


    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class CbarrasForm
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
            Me.lblcedula = New System.Windows.Forms.Label()
            Me.txtcedula = New System.Windows.Forms.TextBox()
            Me.lblRadicado = New System.Windows.Forms.Label()
            Me.TextBox1 = New System.Windows.Forms.TextBox()
            Me.btnbuscar = New System.Windows.Forms.Button()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.btnguardar = New System.Windows.Forms.Button()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'lblcedula
            '
            Me.lblcedula.AutoSize = True
            Me.lblcedula.Location = New System.Drawing.Point(37, 33)
            Me.lblcedula.Name = "lblcedula"
            Me.lblcedula.Size = New System.Drawing.Size(40, 13)
            Me.lblcedula.TabIndex = 0
            Me.lblcedula.Text = "Cedula"
            '
            'txtcedula
            '
            Me.txtcedula.Location = New System.Drawing.Point(164, 30)
            Me.txtcedula.Name = "txtcedula"
            Me.txtcedula.Size = New System.Drawing.Size(100, 20)
            Me.txtcedula.TabIndex = 1
            '
            'lblRadicado
            '
            Me.lblRadicado.AutoSize = True
            Me.lblRadicado.Location = New System.Drawing.Point(27, 27)
            Me.lblRadicado.Name = "lblRadicado"
            Me.lblRadicado.Size = New System.Drawing.Size(108, 13)
            Me.lblRadicado.TabIndex = 2
            Me.lblRadicado.Text = "Número de Radicado"
            '
            'TextBox1
            '
            Me.TextBox1.Location = New System.Drawing.Point(151, 19)
            Me.TextBox1.Name = "TextBox1"
            Me.TextBox1.Size = New System.Drawing.Size(100, 20)
            Me.TextBox1.TabIndex = 3
            '
            'btnbuscar
            '
            Me.btnbuscar.Location = New System.Drawing.Point(349, 28)
            Me.btnbuscar.Name = "btnbuscar"
            Me.btnbuscar.Size = New System.Drawing.Size(75, 23)
            Me.btnbuscar.TabIndex = 4
            Me.btnbuscar.Text = "Buscar"
            Me.btnbuscar.UseVisualStyleBackColor = True
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.btnguardar)
            Me.Panel1.Controls.Add(Me.lblRadicado)
            Me.Panel1.Controls.Add(Me.TextBox1)
            Me.Panel1.Location = New System.Drawing.Point(12, 97)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(514, 152)
            Me.Panel1.TabIndex = 5
            '
            'btnguardar
            '
            Me.btnguardar.Location = New System.Drawing.Point(336, 99)
            Me.btnguardar.Name = "btnguardar"
            Me.btnguardar.Size = New System.Drawing.Size(75, 23)
            Me.btnguardar.TabIndex = 4
            Me.btnguardar.Text = "Guardar"
            Me.btnguardar.UseVisualStyleBackColor = True
            '
            'CbarrasForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(538, 261)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.btnbuscar)
            Me.Controls.Add(Me.txtcedula)
            Me.Controls.Add(Me.lblcedula)
            Me.Name = "CbarrasForm"
            Me.Text = "CbarrasForm"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents lblcedula As System.Windows.Forms.Label
        Friend WithEvents txtcedula As System.Windows.Forms.TextBox
        Friend WithEvents lblRadicado As System.Windows.Forms.Label
        Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
        Friend WithEvents btnbuscar As System.Windows.Forms.Button
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents btnguardar As System.Windows.Forms.Button
    End Class
End Namespace