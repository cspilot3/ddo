Imports Miharu.Desktop.Library

Namespace Forms.Reportes.Devoluciones
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormImpresionDevolucion
        Inherits FormBase

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormImpresionDevolucion))
            Me.rptDevolucion = New Microsoft.Reporting.WinForms.ReportViewer()
            Me.xsdDevolucionesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            CType(Me.xsdDevolucionesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'rptDevolucion
            '
            Me.rptDevolucion.Dock = System.Windows.Forms.DockStyle.Fill
            Me.rptDevolucion.LocalReport.ReportEmbeddedResource = ""
            Me.rptDevolucion.Location = New System.Drawing.Point(0, 0)
            Me.rptDevolucion.Name = "rptDevolucion"
            Me.rptDevolucion.Size = New System.Drawing.Size(792, 573)
            Me.rptDevolucion.TabIndex = 0
            '
            'xsdDevolucionesBindingSource
            '
            Me.xsdDevolucionesBindingSource.DataMember = "Items"
            Me.xsdDevolucionesBindingSource.DataSource = GetType(Miharu.Risk.Library.xsdDevoluciones)
            '
            'FormImpresionDevolucion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(792, 573)
            Me.Controls.Add(Me.rptDevolucion)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormImpresionDevolucion"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Impresión Devolución"
            CType(Me.xsdDevolucionesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents rptDevolucion As Microsoft.Reporting.WinForms.ReportViewer
        Friend WithEvents xsdDevolucionesBindingSource As System.Windows.Forms.BindingSource
    End Class
End Namespace