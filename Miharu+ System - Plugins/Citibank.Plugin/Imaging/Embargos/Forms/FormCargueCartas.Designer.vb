Namespace Embargos.Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCargueCartas
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
            Me.ProcessButton = New System.Windows.Forms.Button()
            Me.GetFolderButton = New System.Windows.Forms.Button()
            Me.RutaTextBox = New System.Windows.Forms.TextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.ProcessButton)
            Me.GroupBox1.Controls.Add(Me.GetFolderButton)
            Me.GroupBox1.Controls.Add(Me.RutaTextBox)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Location = New System.Drawing.Point(8, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(424, 96)
            Me.GroupBox1.TabIndex = 1
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Parámetros"
            '
            'ProcessButton
            '
            Me.ProcessButton.Image = Global.Citibank.Plugin.My.Resources.Resources.btnGuardar
            Me.ProcessButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ProcessButton.Location = New System.Drawing.Point(117, 60)
            Me.ProcessButton.Name = "ProcessButton"
            Me.ProcessButton.Size = New System.Drawing.Size(82, 30)
            Me.ProcessButton.TabIndex = 3
            Me.ProcessButton.Text = "&Procesar"
            Me.ProcessButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ProcessButton.UseVisualStyleBackColor = True
            '
            'GetFolderButton
            '
            Me.GetFolderButton.Image = Global.Citibank.Plugin.My.Resources.Resources.folder_image
            Me.GetFolderButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GetFolderButton.Location = New System.Drawing.Point(10, 60)
            Me.GetFolderButton.Name = "GetFolderButton"
            Me.GetFolderButton.Size = New System.Drawing.Size(89, 30)
            Me.GetFolderButton.TabIndex = 2
            Me.GetFolderButton.Text = "&Seleccionar"
            Me.GetFolderButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GetFolderButton.UseVisualStyleBackColor = True
            '
            'RutaTextBox
            '
            Me.RutaTextBox.Location = New System.Drawing.Point(9, 34)
            Me.RutaTextBox.Name = "RutaTextBox"
            Me.RutaTextBox.Size = New System.Drawing.Size(409, 20)
            Me.RutaTextBox.TabIndex = 1
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(6, 16)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(41, 15)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Ruta:"
            '
            'FormCargueCartas
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(440, 113)
            Me.Controls.Add(Me.GroupBox1)
            Me.Name = "FormCargueCartas"
            Me.Text = "Cargue Cartas"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents ProcessButton As System.Windows.Forms.Button
        Friend WithEvents GetFolderButton As System.Windows.Forms.Button
        Friend WithEvents RutaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace