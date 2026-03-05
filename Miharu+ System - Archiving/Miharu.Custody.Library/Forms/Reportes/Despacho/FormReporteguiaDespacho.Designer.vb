Namespace Forms.Reportes.Despacho
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormReporteguiaDespacho
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
            Dim ReportDataSource5 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Dim ReportDataSource6 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Me.ReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
            Me.OrdenRadioButton = New System.Windows.Forms.RadioButton()
            Me.GrupoRadioButton = New System.Windows.Forms.RadioButton()
            Me.ImprimirButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.SuspendLayout()
            '
            'ReportViewer
            '
            Me.ReportViewer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                             Or System.Windows.Forms.AnchorStyles.Left) _
                                            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            ReportDataSource5.Name = "GuiaDespachoEncabezado"
            ReportDataSource5.Value = Nothing
            ReportDataSource6.Name = "GuiaDespacho"
            ReportDataSource6.Value = Nothing
            Me.ReportViewer.LocalReport.DataSources.Add(ReportDataSource5)
            Me.ReportViewer.LocalReport.DataSources.Add(ReportDataSource6)
            Me.ReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Custody.Library.GuiaDespacho.rdlc"
            Me.ReportViewer.Location = New System.Drawing.Point(20, 25)
            Me.ReportViewer.Name = "ReportViewer"
            Me.ReportViewer.Size = New System.Drawing.Size(622, 324)
            Me.ReportViewer.TabIndex = 0
            '
            'OrdenRadioButton
            '
            Me.OrdenRadioButton.AutoSize = True
            Me.OrdenRadioButton.Location = New System.Drawing.Point(233, 29)
            Me.OrdenRadioButton.Name = "OrdenRadioButton"
            Me.OrdenRadioButton.Size = New System.Drawing.Size(188, 17)
            Me.OrdenRadioButton.TabIndex = 1
            Me.OrdenRadioButton.Text = "Imprimir en orden de punteo"
            Me.OrdenRadioButton.UseVisualStyleBackColor = True
            '
            'GrupoRadioButton
            '
            Me.GrupoRadioButton.AutoSize = True
            Me.GrupoRadioButton.Checked = True
            Me.GrupoRadioButton.Location = New System.Drawing.Point(21, 29)
            Me.GrupoRadioButton.Name = "GrupoRadioButton"
            Me.GrupoRadioButton.Size = New System.Drawing.Size(177, 17)
            Me.GrupoRadioButton.TabIndex = 2
            Me.GrupoRadioButton.TabStop = True
            Me.GrupoRadioButton.Text = "Imprimir con agrupaciones"
            Me.GrupoRadioButton.UseVisualStyleBackColor = True
            '
            'ImprimirButton
            '
            Me.ImprimirButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnPrinter
            Me.ImprimirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ImprimirButton.Location = New System.Drawing.Point(537, 22)
            Me.ImprimirButton.Name = "ImprimirButton"
            Me.ImprimirButton.Size = New System.Drawing.Size(110, 30)
            Me.ImprimirButton.TabIndex = 15
            Me.ImprimirButton.Text = "&Imprimir"
            Me.ImprimirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ImprimirButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.OrdenRadioButton)
            Me.GroupBox1.Controls.Add(Me.ImprimirButton)
            Me.GroupBox1.Controls.Add(Me.GrupoRadioButton)
            Me.GroupBox1.Location = New System.Drawing.Point(14, 13)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(666, 68)
            Me.GroupBox1.TabIndex = 16
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Opciones de impresión"
            '
            'GroupBox2
            '
            Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                          Or System.Windows.Forms.AnchorStyles.Left) _
                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox2.Controls.Add(Me.ReportViewer)
            Me.GroupBox2.Location = New System.Drawing.Point(15, 87)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(665, 367)
            Me.GroupBox2.TabIndex = 17
            Me.GroupBox2.TabStop = False
            '
            'FormReporteguiaDespacho
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(694, 469)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.GroupBox1)
            Me.Name = "FormReporteguiaDespacho"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.Text = "Guia de Despachon de solicitudes"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBox2.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ReportViewer As Microsoft.Reporting.WinForms.ReportViewer
        Friend WithEvents OrdenRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents GrupoRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents ImprimirButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    End Class
End Namespace