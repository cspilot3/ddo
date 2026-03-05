Namespace View.Indexacion.Table

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class IndexerTableRow
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
            Me.BodyFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.SuspendLayout()
            '
            'BodyFlowLayoutPanel
            '
            Me.BodyFlowLayoutPanel.BackColor = System.Drawing.Color.Silver
            Me.BodyFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.BodyFlowLayoutPanel.Location = New System.Drawing.Point(0, 0)
            Me.BodyFlowLayoutPanel.Margin = New System.Windows.Forms.Padding(0)
            Me.BodyFlowLayoutPanel.Name = "BodyFlowLayoutPanel"
            Me.BodyFlowLayoutPanel.Size = New System.Drawing.Size(399, 119)
            Me.BodyFlowLayoutPanel.TabIndex = 0
            Me.BodyFlowLayoutPanel.WrapContents = False
            '
            'IndexerTableRow
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.BodyFlowLayoutPanel)
            Me.Margin = New System.Windows.Forms.Padding(0, 0, 0, 2)
            Me.Name = "IndexerTableRow"
            Me.Size = New System.Drawing.Size(399, 119)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents BodyFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel

    End Class

End Namespace