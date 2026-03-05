Namespace Forms.Reportes.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormReporteUniversalVsParcial
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
            Dim ReportDataSource3 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormReporteUniversalVsParcial))
            Me.ReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
            Me.SuspendLayout()
            '
            'ReportViewer
            '
            Me.ReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
            ReportDataSource3.Name = "Universal"
            ReportDataSource3.Value = Nothing
            Me.ReportViewer.LocalReport.DataSources.Add(ReportDataSource3)
            Me.ReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.Universal Vs Parcial.rdlc"
            Me.ReportViewer.Location = New System.Drawing.Point(0, 0)
            Me.ReportViewer.Name = "ReportViewer"
            Me.ReportViewer.Size = New System.Drawing.Size(792, 573)
            Me.ReportViewer.TabIndex = 0
            '
            'FormReporteUniversalVsParcial
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(792, 573)
            Me.Controls.Add(Me.ReportViewer)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormReporteUniversalVsParcial"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Procesos y Canje S.A."
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ReportViewer As Microsoft.Reporting.WinForms.ReportViewer
    End Class
End Namespace