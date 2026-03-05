Namespace View.Recorte

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class RecorteDataControl
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RecorteDataControl))
            Me.DataButton = New System.Windows.Forms.Button()
            Me.DeleteButton = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'DataButton
            '
            Me.DataButton.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DataButton.Location = New System.Drawing.Point(3, 3)
            Me.DataButton.Name = "DataButton"
            Me.DataButton.Size = New System.Drawing.Size(208, 26)
            Me.DataButton.TabIndex = 0
            Me.DataButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.DataButton.UseVisualStyleBackColor = True
            '
            'DeleteButton
            '
            Me.DeleteButton.BackgroundImage = CType(resources.GetObject("DeleteButton.BackgroundImage"), System.Drawing.Image)
            Me.DeleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.DeleteButton.Dock = System.Windows.Forms.DockStyle.Right
            Me.DeleteButton.Location = New System.Drawing.Point(211, 3)
            Me.DeleteButton.Name = "DeleteButton"
            Me.DeleteButton.Size = New System.Drawing.Size(32, 26)
            Me.DeleteButton.TabIndex = 1
            Me.DeleteButton.UseVisualStyleBackColor = True
            Me.DeleteButton.Visible = False
            '
            'RecorteDataControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.DataButton)
            Me.Controls.Add(Me.DeleteButton)
            Me.Name = "RecorteDataControl"
            Me.Padding = New System.Windows.Forms.Padding(3)
            Me.Size = New System.Drawing.Size(246, 32)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents DataButton As System.Windows.Forms.Button
        Friend WithEvents DeleteButton As System.Windows.Forms.Button

    End Class

End Namespace