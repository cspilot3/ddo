Imports Miharu.Imaging.OffLineViewer.Library.Visor

Namespace Procesos.Reproceso
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormVisorImagen
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormVisorImagen))
            Me.ReprocesoImageViewer = New ImageViewer()
            Me.SuspendLayout()
            '
            'ReprocesoImageViewer
            '
            Me.ReprocesoImageViewer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ReprocesoImageViewer.ImagePath = New List(Of String)
            Me.ReprocesoImageViewer.Location = New System.Drawing.Point(0, 0)
            Me.ReprocesoImageViewer.Name = "ReprocesoImageViewer"
            Me.ReprocesoImageViewer.SelectedPage = 1
            Me.ReprocesoImageViewer.Size = New System.Drawing.Size(792, 573)
            Me.ReprocesoImageViewer.TabIndex = 0
            Me.ReprocesoImageViewer.Zoom = CType(100, Short)
            '
            'FormVisorImagen
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(792, 573)
            Me.Controls.Add(Me.ReprocesoImageViewer)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.KeyPreview = True
            Me.Name = "FormVisorImagen"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Visualización de Imágenes"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ReprocesoImageViewer As ImageViewer
    End Class
End Namespace