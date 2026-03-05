Namespace DesktopReportViewer
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class DesktopReportViewerControl
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
            Me.lblExportarcsv = New System.Windows.Forms.Label()
            Me.MainReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.ReportListComboBox = New System.Windows.Forms.ComboBox()
            Me.ShowButton = New System.Windows.Forms.Button()
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.lblExportarcsv)
            Me.Panel1.Controls.Add(Me.MainReportViewer)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel1.Location = New System.Drawing.Point(0, 25)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(776, 385)
            Me.Panel1.TabIndex = 0
            '
            'lblExportarcsv
            '
            Me.lblExportarcsv.AutoSize = True
            Me.lblExportarcsv.BackColor = System.Drawing.Color.WhiteSmoke
            Me.lblExportarcsv.Enabled = False
            Me.lblExportarcsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.lblExportarcsv.ForeColor = System.Drawing.SystemColors.GrayText
            Me.lblExportarcsv.Location = New System.Drawing.Point(666, 7)
            Me.lblExportarcsv.Name = "lblExportarcsv"
            Me.lblExportarcsv.Size = New System.Drawing.Size(70, 13)
            Me.lblExportarcsv.TabIndex = 2
            Me.lblExportarcsv.Text = "Exportar CSV"
            '
            'MainReportViewer
            '
            Me.MainReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.MainReportViewer.Location = New System.Drawing.Point(0, 0)
            Me.MainReportViewer.Name = "MainReportViewer"
            Me.MainReportViewer.Size = New System.Drawing.Size(776, 385)
            Me.MainReportViewer.TabIndex = 0
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.ReportListComboBox)
            Me.Panel2.Controls.Add(Me.ShowButton)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(0, 0)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(776, 25)
            Me.Panel2.TabIndex = 1
            '
            'ReportListComboBox
            '
            Me.ReportListComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ReportListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ReportListComboBox.FormattingEnabled = True
            Me.ReportListComboBox.Location = New System.Drawing.Point(0, 1)
            Me.ReportListComboBox.Name = "ReportListComboBox"
            Me.ReportListComboBox.Size = New System.Drawing.Size(737, 21)
            Me.ReportListComboBox.TabIndex = 1
            '
            'ShowButton
            '
            Me.ShowButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ShowButton.BackgroundImage = Global.Miharu.Desktop.Controls.My.Resources.Resources.repert2
            Me.ShowButton.Location = New System.Drawing.Point(741, 0)
            Me.ShowButton.Name = "ShowButton"
            Me.ShowButton.Size = New System.Drawing.Size(30, 23)
            Me.ShowButton.TabIndex = 0
            Me.ShowButton.UseVisualStyleBackColor = True
            '
            'DesktopReportViewerControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.Panel2)
            Me.Name = "DesktopReportViewerControl"
            Me.Size = New System.Drawing.Size(776, 410)
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents ReportListComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents ShowButton As System.Windows.Forms.Button
        Public WithEvents MainReportViewer As Microsoft.Reporting.WinForms.ReportViewer
        Friend WithEvents lblExportarcsv As Windows.Forms.Label
    End Class
End Namespace