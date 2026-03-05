Namespace Forms.Boveda
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormVisualizacionCajasLlenas
        Inherits Miharu.Desktop.Library.FormBase

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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormVisualizacionCajasLlenas))
            Me.EstanteSplitContainer = New System.Windows.Forms.SplitContainer()
            Me.ToolTipMensajes = New System.Windows.Forms.ToolTip(Me.components)
            CType(Me.EstanteSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.EstanteSplitContainer.SuspendLayout()
            Me.SuspendLayout()
            '
            'EstanteSplitContainer
            '
            Me.EstanteSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.EstanteSplitContainer.Location = New System.Drawing.Point(0, 0)
            Me.EstanteSplitContainer.Margin = New System.Windows.Forms.Padding(1)
            Me.EstanteSplitContainer.Name = "EstanteSplitContainer"
            Me.EstanteSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            Me.EstanteSplitContainer.Panel1MinSize = 30
            Me.EstanteSplitContainer.Size = New System.Drawing.Size(792, 573)
            Me.EstanteSplitContainer.SplitterDistance = 30
            Me.EstanteSplitContainer.TabIndex = 0
            '
            'ToolTipMensajes
            '
            Me.ToolTipMensajes.IsBalloon = True
            '
            'FormVisualizacionCajasLlenas
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(792, 573)
            Me.Controls.Add(Me.EstanteSplitContainer)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.KeyPreview = True
            Me.Name = "FormVisualizacionCajasLlenas"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Consulta cajas llenas"
            CType(Me.EstanteSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.EstanteSplitContainer.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents EstanteSplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents ToolTipMensajes As System.Windows.Forms.ToolTip
    End Class
End Namespace