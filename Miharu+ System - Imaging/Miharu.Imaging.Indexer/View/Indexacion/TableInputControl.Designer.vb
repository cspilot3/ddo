

Namespace View.Indexacion

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class TableInputControl
        Inherits InputControl

        'UserControl overrides dispose to clean up the component list.
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
            Me.SelectButton = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'SelectButton
            '
            Me.SelectButton.Dock = System.Windows.Forms.DockStyle.Top
            Me.SelectButton.Location = New System.Drawing.Point(5, 21)
            Me.SelectButton.Name = "SelectButton"
            Me.SelectButton.Size = New System.Drawing.Size(270, 23)
            Me.SelectButton.TabIndex = 5
            Me.SelectButton.Text = "Editar"
            Me.SelectButton.UseVisualStyleBackColor = True
            '
            'TableInputControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.SelectButton)
            Me.Name = "TableInputControl"
            Me.Padding = New System.Windows.Forms.Padding(5, 5, 5, 10)
            Me.Size = New System.Drawing.Size(280, 52)
            Me.Controls.SetChildIndex(Me.SelectButton, 0)
            Me.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents SelectButton As System.Windows.Forms.Button

    End Class

End Namespace