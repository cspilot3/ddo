Namespace Embargos.Forms.FormGenerarCartas
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.GenerarButton = New System.Windows.Forms.Button()
            Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
            Me.FechaProcesoPicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaProcesoLabel = New System.Windows.Forms.Label()
            Me.OTLabel = New System.Windows.Forms.Label()
            Me.RutaGroupBox = New System.Windows.Forms.GroupBox()
            Me.SelectFolderButton = New System.Windows.Forms.Button()
            Me.RutaTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.OTDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.CTA_ImagenesDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.CTA_Formato_ParametrosDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.TBL_FormatoDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.RutaGroupBox.SuspendLayout()
            CType(Me.CTA_ImagenesDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CTA_Formato_ParametrosDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.TBL_FormatoDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GenerarButton
            '
            Me.GenerarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GenerarButton.Location = New System.Drawing.Point(316, 112)
            Me.GenerarButton.Name = "GenerarButton"
            Me.GenerarButton.Size = New System.Drawing.Size(75, 23)
            Me.GenerarButton.TabIndex = 0
            Me.GenerarButton.Text = "Generar"
            Me.GenerarButton.UseVisualStyleBackColor = True
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
            Me.ReportViewer1.Location = New System.Drawing.Point(12, 112)
            Me.ReportViewer1.Name = "ReportViewer1"
            Me.ReportViewer1.Size = New System.Drawing.Size(101, 39)
            Me.ReportViewer1.TabIndex = 1
            Me.ReportViewer1.Visible = False
            '
            'FechaProcesoPicker
            '
            Me.FechaProcesoPicker.Location = New System.Drawing.Point(145, 6)
            Me.FechaProcesoPicker.Name = "FechaProcesoPicker"
            Me.FechaProcesoPicker.Size = New System.Drawing.Size(252, 20)
            Me.FechaProcesoPicker.TabIndex = 2
            '
            'FechaProcesoLabel
            '
            Me.FechaProcesoLabel.AutoSize = True
            Me.FechaProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoLabel.Location = New System.Drawing.Point(12, 12)
            Me.FechaProcesoLabel.Name = "FechaProcesoLabel"
            Me.FechaProcesoLabel.Size = New System.Drawing.Size(114, 13)
            Me.FechaProcesoLabel.TabIndex = 49
            Me.FechaProcesoLabel.Text = "Fecha de Proceso:"
            '
            'OTLabel
            '
            Me.OTLabel.AutoSize = True
            Me.OTLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OTLabel.Location = New System.Drawing.Point(14, 41)
            Me.OTLabel.Name = "OTLabel"
            Me.OTLabel.Size = New System.Drawing.Size(28, 13)
            Me.OTLabel.TabIndex = 53
            Me.OTLabel.Text = "OT:"
            '
            'RutaGroupBox
            '
            Me.RutaGroupBox.Controls.Add(Me.SelectFolderButton)
            Me.RutaGroupBox.Controls.Add(Me.RutaTextBox)
            Me.RutaGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.RutaGroupBox.Location = New System.Drawing.Point(17, 62)
            Me.RutaGroupBox.Name = "RutaGroupBox"
            Me.RutaGroupBox.Size = New System.Drawing.Size(380, 40)
            Me.RutaGroupBox.TabIndex = 55
            Me.RutaGroupBox.TabStop = False
            Me.RutaGroupBox.Text = "Ruta Destino"
            '
            'SelectFolderButton
            '
            Me.SelectFolderButton.Image = Global.Santander.Plugin.My.Resources.Resources.folder_image
            Me.SelectFolderButton.Location = New System.Drawing.Point(347, 11)
            Me.SelectFolderButton.Name = "SelectFolderButton"
            Me.SelectFolderButton.Size = New System.Drawing.Size(27, 23)
            Me.SelectFolderButton.TabIndex = 1
            Me.SelectFolderButton.UseVisualStyleBackColor = True
            '
            'RutaTextBox
            '
            Me.RutaTextBox._Obligatorio = False
            Me.RutaTextBox._PermitePegar = False
            Me.RutaTextBox.Cantidad_Decimales = CType(0, Short)
            Me.RutaTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.RutaTextBox.DateFormat = Nothing
            Me.RutaTextBox.DisabledEnter = False
            Me.RutaTextBox.DisabledTab = False
            Me.RutaTextBox.EnabledShortCuts = False
            Me.RutaTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.RutaTextBox.FocusOut = System.Drawing.Color.White
            Me.RutaTextBox.Location = New System.Drawing.Point(6, 16)
            Me.RutaTextBox.MaskedTextBox_Property = ""
            Me.RutaTextBox.MaximumLength = CType(0, Short)
            Me.RutaTextBox.MinimumLength = CType(0, Short)
            Me.RutaTextBox.Name = "RutaTextBox"
            Me.RutaTextBox.Obligatorio = False
            Me.RutaTextBox.permitePegar = False
            Rango1.MaxValue = 9.2233720368547758E+18R
            Rango1.MinValue = 0.0R
            Me.RutaTextBox.Rango = Rango1
            Me.RutaTextBox.Size = New System.Drawing.Size(335, 20)
            Me.RutaTextBox.TabIndex = 0
            Me.RutaTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.RutaTextBox.Usa_Decimales = False
            Me.RutaTextBox.Validos_Cantidad_Puntos = False
            '
            'OTDesktopComboBox
            '
            Me.OTDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OTDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OTDesktopComboBox.DisabledEnter = False
            Me.OTDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OTDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OTDesktopComboBox.FormattingEnabled = True
            Me.OTDesktopComboBox.Location = New System.Drawing.Point(145, 38)
            Me.OTDesktopComboBox.Name = "OTDesktopComboBox"
            Me.OTDesktopComboBox.Size = New System.Drawing.Size(121, 21)
            Me.OTDesktopComboBox.TabIndex = 54
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
            'FormGenerarCartas
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(406, 143)
            Me.Controls.Add(Me.RutaGroupBox)
            Me.Controls.Add(Me.OTDesktopComboBox)
            Me.Controls.Add(Me.OTLabel)
            Me.Controls.Add(Me.FechaProcesoLabel)
            Me.Controls.Add(Me.FechaProcesoPicker)
            Me.Controls.Add(Me.ReportViewer1)
            Me.Controls.Add(Me.GenerarButton)
            Me.Name = "FormGenerarCartas"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Generación de Cartas"
            Me.RutaGroupBox.ResumeLayout(False)
            Me.RutaGroupBox.PerformLayout()
            CType(Me.CTA_ImagenesDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CTA_Formato_ParametrosDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.TBL_FormatoDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GenerarButton As System.Windows.Forms.Button
        Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
        Friend WithEvents CTA_ImagenesDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents CTA_Formato_ParametrosDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents TBL_FormatoDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents FechaProcesoPicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents OTDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents OTLabel As System.Windows.Forms.Label
        Friend WithEvents RutaGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents SelectFolderButton As System.Windows.Forms.Button
        Friend WithEvents RutaTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
    End Class
End Namespace