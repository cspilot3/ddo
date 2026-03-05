Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Risk.Reportes.EmpaqueContenedoresTulaEnlace

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormContenedoresTulaEnlace
        Inherits System.Windows.Forms.Form

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
            Dim Rango1 As Rango = New Rango()
            Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormContenedoresTulaEnlace))
            Me.CTA_Empaque_Contenedores_TulaEnlaceDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.lblPrecinto = New System.Windows.Forms.Panel()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Btn_Buscar = New System.Windows.Forms.Button()
            Me.PrecintoDesktopTextBox = New DesktopTextBoxControl()
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.ReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
            CType(Me.CTA_Empaque_Contenedores_TulaEnlaceDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.lblPrecinto.SuspendLayout()
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'CTA_Empaque_Contenedores_TulaEnlaceDataTableBindingSource
            '
            Me.CTA_Empaque_Contenedores_TulaEnlaceDataTableBindingSource.DataSource = GetType(DBAgrario.SchemaReport.CTA_Empaque_Contenedores_TulaEnlaceDataTable)
            '
            'lblPrecinto
            '
            Me.lblPrecinto.Controls.Add(Me.Label1)
            Me.lblPrecinto.Controls.Add(Me.Btn_Buscar)
            Me.lblPrecinto.Controls.Add(Me.PrecintoDesktopTextBox)
            Me.lblPrecinto.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblPrecinto.Location = New System.Drawing.Point(0, 0)
            Me.lblPrecinto.Name = "lblPrecinto"
            Me.lblPrecinto.Size = New System.Drawing.Size(993, 51)
            Me.lblPrecinto.TabIndex = 2
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(335, 21)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(49, 13)
            Me.Label1.TabIndex = 2
            Me.Label1.Text = "Precinto:"
            '
            'Btn_Buscar
            '
            Me.Btn_Buscar.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnBuscar
            Me.Btn_Buscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Btn_Buscar.Location = New System.Drawing.Point(611, 15)
            Me.Btn_Buscar.Name = "Btn_Buscar"
            Me.Btn_Buscar.Size = New System.Drawing.Size(93, 23)
            Me.Btn_Buscar.TabIndex = 1
            Me.Btn_Buscar.Text = "&Buscar"
            Me.Btn_Buscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.Btn_Buscar.UseVisualStyleBackColor = True
            '
            'PrecintoDesktopTextBox
            '
            Me.PrecintoDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.PrecintoDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.PrecintoDesktopTextBox.DisabledEnter = False
            Me.PrecintoDesktopTextBox.DisabledTab = False
            Me.PrecintoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.PrecintoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.PrecintoDesktopTextBox.Location = New System.Drawing.Point(416, 17)
            Me.PrecintoDesktopTextBox.MaximumLength = CType(0, Short)
            Me.PrecintoDesktopTextBox.MaxLength = 0
            Me.PrecintoDesktopTextBox.MinimumLength = CType(0, Short)
            Me.PrecintoDesktopTextBox.Name = "PrecintoDesktopTextBox"
            Rango1.MaxValue = 1.7976931348623157E+308R
            Rango1.MinValue = 0.0R
            Me.PrecintoDesktopTextBox.Rango = Rango1
            Me.PrecintoDesktopTextBox.ShortcutsEnabled = False
            Me.PrecintoDesktopTextBox.Size = New System.Drawing.Size(168, 20)
            Me.PrecintoDesktopTextBox.TabIndex = 0
            Me.PrecintoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.PrecintoDesktopTextBox.Usa_Decimales = False
            Me.PrecintoDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'SplitContainer1
            '
            Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer1.Name = "SplitContainer1"
            Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.lblPrecinto)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
            Me.SplitContainer1.Size = New System.Drawing.Size(993, 466)
            Me.SplitContainer1.SplitterDistance = 51
            Me.SplitContainer1.TabIndex = 3
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.ReportViewer)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(993, 411)
            Me.Panel1.TabIndex = 1
            '
            'ReportViewer
            '
            Me.ReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
            ReportDataSource1.Name = "TulaEnlaceDataSet"
            ReportDataSource1.Value = Me.CTA_Empaque_Contenedores_TulaEnlaceDataTableBindingSource
            Me.ReportViewer.LocalReport.DataSources.Add(ReportDataSource1)
            Me.ReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.ReportContenedoresTulaEnlace.rdlc"
            Me.ReportViewer.Location = New System.Drawing.Point(0, 0)
            Me.ReportViewer.Name = "ReportViewer"
            Me.ReportViewer.Size = New System.Drawing.Size(993, 411)
            Me.ReportViewer.TabIndex = 0
            '
            'FormContenedoresTulaEnlace
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(993, 466)
            Me.Controls.Add(Me.SplitContainer1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormContenedoresTulaEnlace"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Empaque Contenedores Tula Enlace"
            CType(Me.CTA_Empaque_Contenedores_TulaEnlaceDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.lblPrecinto.ResumeLayout(False)
            Me.lblPrecinto.PerformLayout()
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents lblPrecinto As System.Windows.Forms.Panel
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Btn_Buscar As System.Windows.Forms.Button
        Friend WithEvents PrecintoDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents CTA_Empaque_Contenedores_TulaEnlaceDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents ReportViewer As Microsoft.Reporting.WinForms.ReportViewer
    End Class
End Namespace