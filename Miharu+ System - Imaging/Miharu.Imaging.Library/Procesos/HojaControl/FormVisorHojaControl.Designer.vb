<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormVisorHojaControl
    Inherits Miharu.Desktop.Library.FormBase

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
        Me.WorkSpaceViewerControl = New Miharu.Desktop.Controls.DesktopReportViewer.DesktopReportViewerHojaControl()
        Me.SuspendLayout()
        '
        'WorkSpaceViewerControl
        '
        Me.WorkSpaceViewerControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WorkSpaceViewerControl.Location = New System.Drawing.Point(0, 0)
        Me.WorkSpaceViewerControl.Name = "WorkSpaceViewerControl"
        Me.WorkSpaceViewerControl.Size = New System.Drawing.Size(906, 440)
        Me.WorkSpaceViewerControl.TabIndex = 9
        '
        'FormVisorHojaControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(906, 440)
        Me.Controls.Add(Me.WorkSpaceViewerControl)
        Me.Name = "FormVisorHojaControl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FormVisorHojaControl"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WorkSpaceViewerControl As Miharu.Desktop.Controls.DesktopReportViewer.DesktopReportViewerHojaControl
End Class
