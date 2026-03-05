Namespace Embargos.Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormGenerarCartas
        Inherits System.Windows.Forms.Form

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
            Me.components = New System.ComponentModel.Container()
            Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Dim ReportDataSource2 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Dim ReportDataSource3 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Dim ReportDataSource4 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Me.CTA_ImagenesDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.CTA_Formato_ParametrosDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.TBL_FormatoDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.OTDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.OTLabel = New System.Windows.Forms.Label()
            Me.FechaProcesoLabel = New System.Windows.Forms.Label()
            Me.FechaProcesoPicker = New System.Windows.Forms.DateTimePicker()
            Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
            Me.GenerarButton = New System.Windows.Forms.Button()
            CType(Me.CTA_ImagenesDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CTA_Formato_ParametrosDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.TBL_FormatoDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'CTA_ImagenesDataTableBindingSource
            '
            Me.CTA_ImagenesDataTableBindingSource.DataSource = GetType(DBIntegration.SchemaConfig.CTA_ImagenesDataTable)
            '
            'CTA_Formato_ParametrosDataTableBindingSource
            '
            Me.CTA_Formato_ParametrosDataTableBindingSource.DataSource = GetType(DBIntegration.SchemaProcess.CTA_Formato_ParametrosDataTable)
            '
            'TBL_FormatoDataTableBindingSource
            '
            Me.TBL_FormatoDataTableBindingSource.DataSource = GetType(DBIntegration.SchemaConfig.TBL_FormatoDataTable)
            '
            'OTDesktopComboBox
            '
            Me.OTDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OTDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OTDesktopComboBox.DisabledEnter = False
            Me.OTDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OTDesktopComboBox.fk_Campo = 0
            Me.OTDesktopComboBox.fk_Documento = 0
            Me.OTDesktopComboBox.fk_Validacion = 0
            Me.OTDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OTDesktopComboBox.FormattingEnabled = True
            Me.OTDesktopComboBox.Location = New System.Drawing.Point(145, 45)
            Me.OTDesktopComboBox.Name = "OTDesktopComboBox"
            Me.OTDesktopComboBox.Size = New System.Drawing.Size(121, 21)
            Me.OTDesktopComboBox.TabIndex = 61
            '
            'OTLabel
            '
            Me.OTLabel.AutoSize = True
            Me.OTLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OTLabel.Location = New System.Drawing.Point(14, 48)
            Me.OTLabel.Name = "OTLabel"
            Me.OTLabel.Size = New System.Drawing.Size(28, 13)
            Me.OTLabel.TabIndex = 60
            Me.OTLabel.Text = "OT:"
            '
            'FechaProcesoLabel
            '
            Me.FechaProcesoLabel.AutoSize = True
            Me.FechaProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoLabel.Location = New System.Drawing.Point(12, 19)
            Me.FechaProcesoLabel.Name = "FechaProcesoLabel"
            Me.FechaProcesoLabel.Size = New System.Drawing.Size(114, 13)
            Me.FechaProcesoLabel.TabIndex = 59
            Me.FechaProcesoLabel.Text = "Fecha de Proceso:"
            '
            'FechaProcesoPicker
            '
            Me.FechaProcesoPicker.Location = New System.Drawing.Point(145, 13)
            Me.FechaProcesoPicker.Name = "FechaProcesoPicker"
            Me.FechaProcesoPicker.Size = New System.Drawing.Size(252, 20)
            Me.FechaProcesoPicker.TabIndex = 58
            '
            'ReportViewer1
            '
            Me.ReportViewer1.DocumentMapWidth = 84
            ReportDataSource1.Name = "CTA_ImagenesDataSet"
            ReportDataSource1.Value = Me.CTA_ImagenesDataTableBindingSource
            ReportDataSource2.Name = "CTA_Formato_Parametros_AsuntoDataSet"
            ReportDataSource2.Value = Me.CTA_Formato_ParametrosDataTableBindingSource
            ReportDataSource3.Name = "CTA_Formato_Parametros_FirmaDataSet"
            ReportDataSource3.Value = Me.CTA_Formato_ParametrosDataTableBindingSource
            ReportDataSource4.Name = "TBL_FormatoDataSet"
            ReportDataSource4.Value = Me.TBL_FormatoDataTableBindingSource
            Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
            Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource2)
            Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource3)
            Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource4)
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "Santander.Plugin.Report_GenerarCartas.rdlc"
            Me.ReportViewer1.Location = New System.Drawing.Point(12, 79)
            Me.ReportViewer1.Name = "ReportViewer1"
            Me.ReportViewer1.Size = New System.Drawing.Size(101, 39)
            Me.ReportViewer1.TabIndex = 57
            Me.ReportViewer1.Visible = False
            '
            'GenerarButton
            '
            Me.GenerarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GenerarButton.Location = New System.Drawing.Point(307, 95)
            Me.GenerarButton.Name = "GenerarButton"
            Me.GenerarButton.Size = New System.Drawing.Size(75, 23)
            Me.GenerarButton.TabIndex = 56
            Me.GenerarButton.Text = "Generar"
            Me.GenerarButton.UseVisualStyleBackColor = True
            '
            'FormGenerarCartas
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(405, 129)
            Me.Controls.Add(Me.OTDesktopComboBox)
            Me.Controls.Add(Me.OTLabel)
            Me.Controls.Add(Me.FechaProcesoLabel)
            Me.Controls.Add(Me.FechaProcesoPicker)
            Me.Controls.Add(Me.ReportViewer1)
            Me.Controls.Add(Me.GenerarButton)
            Me.Name = "FormGenerarCartas"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "FormGenerarCartas"
            CType(Me.CTA_ImagenesDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CTA_Formato_ParametrosDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.TBL_FormatoDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents OTDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents OTLabel As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoPicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents CTA_ImagenesDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents CTA_Formato_ParametrosDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents TBL_FormatoDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
        Friend WithEvents GenerarButton As System.Windows.Forms.Button
    End Class
End Namespace

