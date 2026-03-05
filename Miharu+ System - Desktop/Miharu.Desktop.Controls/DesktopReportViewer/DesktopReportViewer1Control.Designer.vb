Namespace DesktopReportViewer
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class DesktopReportViewer1Control
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
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.MainReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.MainReportViewer1)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(776, 410)
            Me.Panel1.TabIndex = 0
            '
            'MainReportViewer1
            '
            Me.MainReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.MainReportViewer1.Location = New System.Drawing.Point(0, 0)
            Me.MainReportViewer1.Name = "MainReportViewer1"
            Me.MainReportViewer1.Size = New System.Drawing.Size(776, 410)
            Me.MainReportViewer1.TabIndex = 0
            '
            'DesktopReportViewerControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.Panel1)
            Me.Name = "DesktopReportViewerControl"
            Me.Size = New System.Drawing.Size(776, 410)
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Public WithEvents MainReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer

    End Class
End Namespace