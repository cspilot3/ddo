Namespace Forms.Reportes.CentroDistribucion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormImpresionRemision
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormImpresionRemision))
            Me.ItemsRemisionDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.rptRemision = New Microsoft.Reporting.WinForms.ReportViewer()
            Me.RPT_Folders_ReprocesoDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.RPT_Files_ReprocesoDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.xsdFilesReprocesoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            CType(Me.ItemsRemisionDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.RPT_Folders_ReprocesoDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.RPT_Files_ReprocesoDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.xsdFilesReprocesoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ItemsRemisionDataTableBindingSource
            '
            Me.ItemsRemisionDataTableBindingSource.DataSource = GetType(Miharu.Risk.Library.xsdRemision.ItemsRemisionDataTable)
            '
            'rptRemision
            '
            Me.rptRemision.Dock = System.Windows.Forms.DockStyle.Fill
            ReportDataSource1.Name = "ItemsRemision"
            ReportDataSource1.Value = Me.ItemsRemisionDataTableBindingSource
            Me.rptRemision.LocalReport.DataSources.Add(ReportDataSource1)
            Me.rptRemision.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.Remision.rdlc"
            Me.rptRemision.LocalReport.ReportPath = ""
            Me.rptRemision.Location = New System.Drawing.Point(0, 0)
            Me.rptRemision.Name = "rptRemision"
            Me.rptRemision.Size = New System.Drawing.Size(792, 573)
            Me.rptRemision.TabIndex = 0
            '
            'RPT_Folders_ReprocesoDataTableBindingSource
            '
            Me.RPT_Folders_ReprocesoDataTableBindingSource.DataSource = GetType(DBArchiving.Schemadbo.RPT_Folders_ReprocesoDataTable)
            '
            'RPT_Files_ReprocesoDataTableBindingSource
            '
            Me.RPT_Files_ReprocesoDataTableBindingSource.DataSource = GetType(DBArchiving.Schemadbo.RPT_Files_ReprocesoDataTable)
            '
            'xsdFilesReprocesoBindingSource
            '
            Me.xsdFilesReprocesoBindingSource.DataMember = "xsdFilesReproceso"
            '
            'FormImpresionRemision
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(792, 573)
            Me.Controls.Add(Me.rptRemision)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormImpresionRemision"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Impresión de Remisión"
            CType(Me.ItemsRemisionDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.RPT_Folders_ReprocesoDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.RPT_Files_ReprocesoDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.xsdFilesReprocesoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents rptRemision As Microsoft.Reporting.WinForms.ReportViewer
        Friend WithEvents RPT_Folders_ReprocesoDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents RPT_Files_ReprocesoDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents xsdFilesReprocesoBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents ItemsRemisionDataTableBindingSource As System.Windows.Forms.BindingSource
    End Class
End Namespace