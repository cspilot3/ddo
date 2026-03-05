Imports Miharu.Desktop.Library

Namespace Reportes.VisorReportes.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormReporteOTs
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormReporteOTs))
            Me.ReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
            Me.SplitContainer = New System.Windows.Forms.SplitContainer()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Fecha2DateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Fecha1DateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.OTsListBox = New System.Windows.Forms.ListBox()
            CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer.Panel1.SuspendLayout()
            Me.SplitContainer.Panel2.SuspendLayout()
            Me.SplitContainer.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'ReportViewer
            '
            Me.ReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.OTs.rdlc"
            Me.ReportViewer.Location = New System.Drawing.Point(0, 0)
            Me.ReportViewer.Name = "ReportViewer"
            Me.ReportViewer.Size = New System.Drawing.Size(764, 316)
            Me.ReportViewer.TabIndex = 5
            '
            'SplitContainer
            '
            Me.SplitContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                               Or System.Windows.Forms.AnchorStyles.Left) _
                                              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.SplitContainer.Location = New System.Drawing.Point(12, 12)
            Me.SplitContainer.Name = "SplitContainer"
            Me.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer.Panel1
            '
            Me.SplitContainer.Panel1.Controls.Add(Me.GroupBox1)
            Me.SplitContainer.Panel1.Controls.Add(Me.Label1)
            Me.SplitContainer.Panel1.Controls.Add(Me.OTsListBox)
            '
            'SplitContainer.Panel2
            '
            Me.SplitContainer.Panel2.Controls.Add(Me.ReportViewer)
            Me.SplitContainer.Size = New System.Drawing.Size(768, 549)
            Me.SplitContainer.SplitterDistance = 225
            Me.SplitContainer.TabIndex = 0
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Fecha2DateTimePicker)
            Me.GroupBox1.Controls.Add(Me.Fecha1DateTimePicker)
            Me.GroupBox1.Controls.Add(Me.BuscarButton)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Location = New System.Drawing.Point(20, 49)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(270, 153)
            Me.GroupBox1.TabIndex = 6
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Filtros"
            '
            'Fecha2DateTimePicker
            '
            Me.Fecha2DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.Fecha2DateTimePicker.Location = New System.Drawing.Point(125, 65)
            Me.Fecha2DateTimePicker.Name = "Fecha2DateTimePicker"
            Me.Fecha2DateTimePicker.Size = New System.Drawing.Size(119, 21)
            Me.Fecha2DateTimePicker.TabIndex = 7
            Me.Fecha2DateTimePicker.Value = New Date(2010, 9, 23, 0, 0, 0, 0)
            '
            'Fecha1DateTimePicker
            '
            Me.Fecha1DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.Fecha1DateTimePicker.Location = New System.Drawing.Point(125, 37)
            Me.Fecha1DateTimePicker.Name = "Fecha1DateTimePicker"
            Me.Fecha1DateTimePicker.Size = New System.Drawing.Size(119, 21)
            Me.Fecha1DateTimePicker.TabIndex = 6
            Me.Fecha1DateTimePicker.Value = New Date(2010, 1, 1, 0, 0, 0, 0)
            '
            'BuscarButton
            '
            Me.BuscarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar2
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(159, 110)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(85, 30)
            Me.BuscarButton.TabIndex = 3
            Me.BuscarButton.Text = "&Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(19, 41)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(77, 13)
            Me.Label2.TabIndex = 4
            Me.Label2.Text = "Fecha Inicial"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(19, 65)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(69, 13)
            Me.Label3.TabIndex = 5
            Me.Label3.Text = "Fecha Final"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(15, 16)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(200, 13)
            Me.Label1.TabIndex = 3
            Me.Label1.Text = "Rango de fechas para filtro de OTs"
            '
            'OTsListBox
            '
            Me.OTsListBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                           Or System.Windows.Forms.AnchorStyles.Left) _
                                          Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OTsListBox.FormattingEnabled = True
            Me.OTsListBox.Location = New System.Drawing.Point(308, 16)
            Me.OTsListBox.Name = "OTsListBox"
            Me.OTsListBox.Size = New System.Drawing.Size(441, 186)
            Me.OTsListBox.TabIndex = 4
            '
            'FormReporteOTs
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(792, 573)
            Me.Controls.Add(Me.SplitContainer)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormReporteOTs"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "OTs"
            Me.SplitContainer.Panel1.ResumeLayout(False)
            Me.SplitContainer.Panel1.PerformLayout()
            Me.SplitContainer.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer.ResumeLayout(False)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ReportViewer As Microsoft.Reporting.WinForms.ReportViewer
        Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents OTsListBox As System.Windows.Forms.ListBox
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents Fecha2DateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Fecha1DateTimePicker As System.Windows.Forms.DateTimePicker
    End Class
End Namespace