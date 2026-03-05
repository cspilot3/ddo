Namespace View.Indexacion

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class InputControl
        Inherits System.Windows.Forms.UserControl

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
            Me.EtiquetaLabel = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'EtiquetaLabel
            '
            Me.EtiquetaLabel.Dock = System.Windows.Forms.DockStyle.Top
            Me.EtiquetaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EtiquetaLabel.ForeColor = System.Drawing.Color.Maroon
            Me.EtiquetaLabel.Location = New System.Drawing.Point(0, 0)
            Me.EtiquetaLabel.Name = "EtiquetaLabel"
            Me.EtiquetaLabel.Size = New System.Drawing.Size(249, 16)
            Me.EtiquetaLabel.TabIndex = 4
            Me.EtiquetaLabel.Text = "Etiqueta"
            '
            'InputControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.EtiquetaLabel)
            Me.Name = "InputControl"
            Me.Size = New System.Drawing.Size(249, 120)
            Me.ResumeLayout(False)

        End Sub
        Public WithEvents EtiquetaLabel As System.Windows.Forms.Label

    End Class
End Namespace