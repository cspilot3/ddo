Namespace Forms.Reportes.Solicitudes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormReporteSolicitudes
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
            Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormReporteSolicitudes))
            Me.xsdAdministracionSolicitudesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.SolicitudesReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
            CType(Me.xsdAdministracionSolicitudesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'xsdAdministracionSolicitudesBindingSource
            '
            Me.xsdAdministracionSolicitudesBindingSource.DataMember = "Solicitudes"
            Me.xsdAdministracionSolicitudesBindingSource.DataSource = GetType(Miharu.Custody.Library.xsdAdministracionSolicitudes)
            '
            'SolicitudesReportViewer
            '
            Me.SolicitudesReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
            ReportDataSource1.Name = "SolicitudesDataSet"
            ReportDataSource1.Value = Me.xsdAdministracionSolicitudesBindingSource
            Me.SolicitudesReportViewer.LocalReport.DataSources.Add(ReportDataSource1)
            Me.SolicitudesReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Custody.Library.AdministracionSolicitudes.rdlc"
            Me.SolicitudesReportViewer.Location = New System.Drawing.Point(0, 0)
            Me.SolicitudesReportViewer.Name = "SolicitudesReportViewer"
            Me.SolicitudesReportViewer.Size = New System.Drawing.Size(792, 573)
            Me.SolicitudesReportViewer.TabIndex = 0
            '
            'FormReporteSolicitudes
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(792, 573)
            Me.Controls.Add(Me.SolicitudesReportViewer)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormReporteSolicitudes"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Reporte de Solicitudes"
            CType(Me.xsdAdministracionSolicitudesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents SolicitudesReportViewer As Microsoft.Reporting.WinForms.ReportViewer
        Friend WithEvents xsdAdministracionSolicitudesBindingSource As System.Windows.Forms.BindingSource
    End Class
End Namespace