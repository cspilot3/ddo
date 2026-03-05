<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StaticArrow
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
        Me.picBase = New System.Windows.Forms.PictureBox
        CType(Me.picBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picBase
        '
        Me.picBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picBase.Location = New System.Drawing.Point(0, 0)
        Me.picBase.Name = "picBase"
        Me.picBase.Size = New System.Drawing.Size(150, 96)
        Me.picBase.TabIndex = 0
        Me.picBase.TabStop = False
        '
        'StaticArrow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.picBase)
        Me.Name = "StaticArrow"
        Me.Size = New System.Drawing.Size(150, 96)
        CType(Me.picBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picBase As System.Windows.Forms.PictureBox

End Class
