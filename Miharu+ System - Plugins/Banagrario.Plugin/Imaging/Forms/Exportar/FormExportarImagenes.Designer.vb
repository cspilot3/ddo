Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Controls

Namespace Imaging.Forms.Exportar
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormExportarImagenes
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
            Dim Rango1 As Rango = New Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormExportarImagenes))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.EsquemaDesktopComboBox = New DesktopComboBoxControl()
            Me.FechaProcesodateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.DocumentoLabel = New System.Windows.Forms.Label()
            Me.FechaProcesolabel = New System.Windows.Forms.Label()
            Me.TransaccionDesktopComboBox = New DesktopComboBoxControl()
            Me.OficinaLabel = New System.Windows.Forms.Label()
            Me.COBLabel = New System.Windows.Forms.Label()
            Me.RegionalLabel = New System.Windows.Forms.Label()
            Me.FiltroOficinaGroupBox = New System.Windows.Forms.GroupBox()
            Me.OficinaDesktopComboBox = New DesktopComboBoxControl()
            Me.RegionalDesktopComboBox = New DesktopComboBoxControl()
            Me.COBDesktopComboBox = New DesktopComboBoxControl()
            Me.RutaExportacionLabel = New System.Windows.Forms.Label()
            Me.RutaExportacionFolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
            Me.SeleccionarRutaButton = New System.Windows.Forms.Button()
            Me.ExportarButton = New System.Windows.Forms.Button()
            Me.RutaExportacionDesktopTextBox = New DesktopTextBoxControl()
            Me.ComprimirDesktopCheckBox = New DesktopCheckBox.DesktopCheckBoxControl()
            Me.TipoExportacionTabControl = New System.Windows.Forms.TabControl()
            Me.TabPage1 = New System.Windows.Forms.TabPage()
            Me.TraficoPanel = New System.Windows.Forms.Panel()
            Me.SinTraficoDesktopCheckBox = New DesktopCheckBox.DesktopCheckBoxControl()
            Me.ConTraficoDesktopCheckBox = New DesktopCheckBox.DesktopCheckBoxControl()
            Me.TabPage2 = New System.Windows.Forms.TabPage()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.FechaFinDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.FechaInicioDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FiltroOficinaGroupBox.SuspendLayout()
            Me.TipoExportacionTabControl.SuspendLayout()
            Me.TabPage1.SuspendLayout()
            Me.TraficoPanel.SuspendLayout()
            Me.TabPage2.SuspendLayout()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(223, 24)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(54, 13)
            Me.Label1.TabIndex = 32
            Me.Label1.Text = "Esquema:"
            '
            'EsquemaDesktopComboBox
            '
            Me.EsquemaDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaDesktopComboBox.DisabledEnter = False
            Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaDesktopComboBox.FormattingEnabled = True
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(311, 21)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(420, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 31
            '
            'FechaProcesodateTimePicker
            '
            Me.FechaProcesodateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesodateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaProcesodateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaProcesodateTimePicker.Location = New System.Drawing.Point(101, 52)
            Me.FechaProcesodateTimePicker.Name = "FechaProcesodateTimePicker"
            Me.FechaProcesodateTimePicker.Size = New System.Drawing.Size(84, 20)
            Me.FechaProcesodateTimePicker.TabIndex = 0
            '
            'DocumentoLabel
            '
            Me.DocumentoLabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.DocumentoLabel.AutoSize = True
            Me.DocumentoLabel.Location = New System.Drawing.Point(223, 53)
            Me.DocumentoLabel.Name = "DocumentoLabel"
            Me.DocumentoLabel.Size = New System.Drawing.Size(69, 13)
            Me.DocumentoLabel.TabIndex = 30
            Me.DocumentoLabel.Text = "Transacción:"
            '
            'FechaProcesolabel
            '
            Me.FechaProcesolabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesolabel.AutoSize = True
            Me.FechaProcesolabel.Location = New System.Drawing.Point(4, 56)
            Me.FechaProcesolabel.Name = "FechaProcesolabel"
            Me.FechaProcesolabel.Size = New System.Drawing.Size(82, 13)
            Me.FechaProcesolabel.TabIndex = 22
            Me.FechaProcesolabel.Text = "Fecha Proceso:"
            '
            'TransaccionDesktopComboBox
            '
            Me.TransaccionDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.TransaccionDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TransaccionDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TransaccionDesktopComboBox.DisabledEnter = False
            Me.TransaccionDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TransaccionDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TransaccionDesktopComboBox.FormattingEnabled = True
            Me.TransaccionDesktopComboBox.Location = New System.Drawing.Point(311, 50)
            Me.TransaccionDesktopComboBox.Name = "TransaccionDesktopComboBox"
            Me.TransaccionDesktopComboBox.Size = New System.Drawing.Size(420, 21)
            Me.TransaccionDesktopComboBox.TabIndex = 1
            '
            'OficinaLabel
            '
            Me.OficinaLabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.OficinaLabel.AutoSize = True
            Me.OficinaLabel.Location = New System.Drawing.Point(39, 96)
            Me.OficinaLabel.Name = "OficinaLabel"
            Me.OficinaLabel.Size = New System.Drawing.Size(43, 13)
            Me.OficinaLabel.TabIndex = 28
            Me.OficinaLabel.Text = "Oficina:"
            '
            'COBLabel
            '
            Me.COBLabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.COBLabel.AutoSize = True
            Me.COBLabel.Location = New System.Drawing.Point(50, 61)
            Me.COBLabel.Name = "COBLabel"
            Me.COBLabel.Size = New System.Drawing.Size(32, 13)
            Me.COBLabel.TabIndex = 26
            Me.COBLabel.Text = "COB:"
            '
            'RegionalLabel
            '
            Me.RegionalLabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.RegionalLabel.AutoSize = True
            Me.RegionalLabel.Location = New System.Drawing.Point(30, 26)
            Me.RegionalLabel.Name = "RegionalLabel"
            Me.RegionalLabel.Size = New System.Drawing.Size(52, 13)
            Me.RegionalLabel.TabIndex = 24
            Me.RegionalLabel.Text = "Regional:"
            '
            'FiltroOficinaGroupBox
            '
            Me.FiltroOficinaGroupBox.Controls.Add(Me.OficinaDesktopComboBox)
            Me.FiltroOficinaGroupBox.Controls.Add(Me.COBLabel)
            Me.FiltroOficinaGroupBox.Controls.Add(Me.RegionalDesktopComboBox)
            Me.FiltroOficinaGroupBox.Controls.Add(Me.COBDesktopComboBox)
            Me.FiltroOficinaGroupBox.Controls.Add(Me.OficinaLabel)
            Me.FiltroOficinaGroupBox.Controls.Add(Me.RegionalLabel)
            Me.FiltroOficinaGroupBox.Location = New System.Drawing.Point(12, 13)
            Me.FiltroOficinaGroupBox.Name = "FiltroOficinaGroupBox"
            Me.FiltroOficinaGroupBox.Size = New System.Drawing.Size(452, 139)
            Me.FiltroOficinaGroupBox.TabIndex = 1
            Me.FiltroOficinaGroupBox.TabStop = False
            Me.FiltroOficinaGroupBox.Text = "Oficina"
            '
            'OficinaDesktopComboBox
            '
            Me.OficinaDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.OficinaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OficinaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OficinaDesktopComboBox.DisabledEnter = False
            Me.OficinaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OficinaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OficinaDesktopComboBox.FormattingEnabled = True
            Me.OficinaDesktopComboBox.Location = New System.Drawing.Point(88, 93)
            Me.OficinaDesktopComboBox.Name = "OficinaDesktopComboBox"
            Me.OficinaDesktopComboBox.Size = New System.Drawing.Size(324, 21)
            Me.OficinaDesktopComboBox.TabIndex = 2
            '
            'RegionalDesktopComboBox
            '
            Me.RegionalDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.RegionalDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.RegionalDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.RegionalDesktopComboBox.DisabledEnter = False
            Me.RegionalDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.RegionalDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.RegionalDesktopComboBox.FormattingEnabled = True
            Me.RegionalDesktopComboBox.Location = New System.Drawing.Point(88, 23)
            Me.RegionalDesktopComboBox.Name = "RegionalDesktopComboBox"
            Me.RegionalDesktopComboBox.Size = New System.Drawing.Size(324, 21)
            Me.RegionalDesktopComboBox.TabIndex = 0
            '
            'COBDesktopComboBox
            '
            Me.COBDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.COBDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.COBDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.COBDesktopComboBox.DisabledEnter = False
            Me.COBDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.COBDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.COBDesktopComboBox.FormattingEnabled = True
            Me.COBDesktopComboBox.Location = New System.Drawing.Point(88, 58)
            Me.COBDesktopComboBox.Name = "COBDesktopComboBox"
            Me.COBDesktopComboBox.Size = New System.Drawing.Size(324, 21)
            Me.COBDesktopComboBox.TabIndex = 1
            '
            'RutaExportacionLabel
            '
            Me.RutaExportacionLabel.AutoSize = True
            Me.RutaExportacionLabel.Location = New System.Drawing.Point(470, 13)
            Me.RutaExportacionLabel.Name = "RutaExportacionLabel"
            Me.RutaExportacionLabel.Size = New System.Drawing.Size(157, 13)
            Me.RutaExportacionLabel.TabIndex = 32
            Me.RutaExportacionLabel.Text = "Seleccione ruta de exportación:"
            '
            'SeleccionarRutaButton
            '
            Me.SeleccionarRutaButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.folder_add2
            Me.SeleccionarRutaButton.Location = New System.Drawing.Point(730, 28)
            Me.SeleccionarRutaButton.Name = "SeleccionarRutaButton"
            Me.SeleccionarRutaButton.Size = New System.Drawing.Size(35, 23)
            Me.SeleccionarRutaButton.TabIndex = 3
            Me.SeleccionarRutaButton.UseVisualStyleBackColor = True
            '
            'ExportarButton
            '
            Me.ExportarButton.AccessibleDescription = ""
            Me.ExportarButton.BackColor = System.Drawing.SystemColors.Control
            Me.ExportarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ExportarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ExportarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.export_image
            Me.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.ExportarButton.Location = New System.Drawing.Point(676, 92)
            Me.ExportarButton.Name = "ExportarButton"
            Me.ExportarButton.Size = New System.Drawing.Size(89, 60)
            Me.ExportarButton.TabIndex = 4
            Me.ExportarButton.Tag = "Ctrl + P"
            Me.ExportarButton.Text = "&Exportar"
            Me.ExportarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.ExportarButton.UseVisualStyleBackColor = False
            '
            'RutaExportacionDesktopTextBox
            '
            Me.RutaExportacionDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.RutaExportacionDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.RutaExportacionDesktopTextBox.DateFormat = Nothing
            Me.RutaExportacionDesktopTextBox.DisabledEnter = False
            Me.RutaExportacionDesktopTextBox.DisabledTab = False
            Me.RutaExportacionDesktopTextBox.EnabledShortCuts = False
            Me.RutaExportacionDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.RutaExportacionDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.RutaExportacionDesktopTextBox.Location = New System.Drawing.Point(473, 30)
            Me.RutaExportacionDesktopTextBox.Margin = New System.Windows.Forms.Padding(0)
            Me.RutaExportacionDesktopTextBox.MaskedTextBox_Property = ""
            Me.RutaExportacionDesktopTextBox.MaximumLength = CType(0, Short)
            Me.RutaExportacionDesktopTextBox.MinimumLength = CType(0, Short)
            Me.RutaExportacionDesktopTextBox.Name = "RutaExportacionDesktopTextBox"
            Rango1.MaxValue = 1.7976931348623157E+308R
            Rango1.MinValue = 0.0R
            Me.RutaExportacionDesktopTextBox.Rango = Rango1
            Me.RutaExportacionDesktopTextBox.Size = New System.Drawing.Size(244, 20)
            Me.RutaExportacionDesktopTextBox.TabIndex = 2
            Me.RutaExportacionDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.RutaExportacionDesktopTextBox.Usa_Decimales = False
            Me.RutaExportacionDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'ComprimirDesktopCheckBox
            '
            Me.ComprimirDesktopCheckBox.AutoSize = True
            Me.ComprimirDesktopCheckBox.DisabledEnter = False
            Me.ComprimirDesktopCheckBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ComprimirDesktopCheckBox.FocusOut = System.Drawing.Color.Gainsboro
            Me.ComprimirDesktopCheckBox.Location = New System.Drawing.Point(473, 53)
            Me.ComprimirDesktopCheckBox.Name = "ComprimirDesktopCheckBox"
            Me.ComprimirDesktopCheckBox.Size = New System.Drawing.Size(97, 17)
            Me.ComprimirDesktopCheckBox.TabIndex = 34
            Me.ComprimirDesktopCheckBox.Text = "Comprimir (ZIP)"
            Me.ComprimirDesktopCheckBox.UseVisualStyleBackColor = True
            '
            'TipoExportacionTabControl
            '
            Me.TipoExportacionTabControl.Controls.Add(Me.TabPage1)
            Me.TipoExportacionTabControl.Controls.Add(Me.TabPage2)
            Me.TipoExportacionTabControl.Location = New System.Drawing.Point(12, 158)
            Me.TipoExportacionTabControl.Name = "TipoExportacionTabControl"
            Me.TipoExportacionTabControl.SelectedIndex = 0
            Me.TipoExportacionTabControl.Size = New System.Drawing.Size(753, 150)
            Me.TipoExportacionTabControl.TabIndex = 35
            '
            'TabPage1
            '
            Me.TabPage1.Controls.Add(Me.TraficoPanel)
            Me.TabPage1.Controls.Add(Me.Label1)
            Me.TabPage1.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.TabPage1.Controls.Add(Me.FechaProcesolabel)
            Me.TabPage1.Controls.Add(Me.DocumentoLabel)
            Me.TabPage1.Controls.Add(Me.FechaProcesodateTimePicker)
            Me.TabPage1.Controls.Add(Me.TransaccionDesktopComboBox)
            Me.TabPage1.Location = New System.Drawing.Point(4, 22)
            Me.TabPage1.Name = "TabPage1"
            Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage1.Size = New System.Drawing.Size(745, 124)
            Me.TabPage1.TabIndex = 0
            Me.TabPage1.Text = "Fecha Proceso"
            Me.TabPage1.UseVisualStyleBackColor = True
            '
            'TraficoPanel
            '
            Me.TraficoPanel.Controls.Add(Me.SinTraficoDesktopCheckBox)
            Me.TraficoPanel.Controls.Add(Me.ConTraficoDesktopCheckBox)
            Me.TraficoPanel.Location = New System.Drawing.Point(226, 77)
            Me.TraficoPanel.Name = "TraficoPanel"
            Me.TraficoPanel.Size = New System.Drawing.Size(505, 41)
            Me.TraficoPanel.TabIndex = 33
            Me.TraficoPanel.Visible = False
            '
            'SinTraficoDesktopCheckBox
            '
            Me.SinTraficoDesktopCheckBox.AutoSize = True
            Me.SinTraficoDesktopCheckBox.DisabledEnter = False
            Me.SinTraficoDesktopCheckBox.FocusIn = System.Drawing.Color.LightYellow
            Me.SinTraficoDesktopCheckBox.FocusOut = System.Drawing.Color.Gainsboro
            Me.SinTraficoDesktopCheckBox.Location = New System.Drawing.Point(196, 13)
            Me.SinTraficoDesktopCheckBox.Name = "SinTraficoDesktopCheckBox"
            Me.SinTraficoDesktopCheckBox.Size = New System.Drawing.Size(77, 17)
            Me.SinTraficoDesktopCheckBox.TabIndex = 1
            Me.SinTraficoDesktopCheckBox.Text = "Sin Tráfico"
            Me.SinTraficoDesktopCheckBox.UseVisualStyleBackColor = True
            '
            'ConTraficoDesktopCheckBox
            '
            Me.ConTraficoDesktopCheckBox.AutoSize = True
            Me.ConTraficoDesktopCheckBox.Checked = True
            Me.ConTraficoDesktopCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
            Me.ConTraficoDesktopCheckBox.DisabledEnter = False
            Me.ConTraficoDesktopCheckBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ConTraficoDesktopCheckBox.FocusOut = System.Drawing.Color.Gainsboro
            Me.ConTraficoDesktopCheckBox.Location = New System.Drawing.Point(4, 13)
            Me.ConTraficoDesktopCheckBox.Name = "ConTraficoDesktopCheckBox"
            Me.ConTraficoDesktopCheckBox.Size = New System.Drawing.Size(81, 17)
            Me.ConTraficoDesktopCheckBox.TabIndex = 0
            Me.ConTraficoDesktopCheckBox.Text = "Con Tráfico"
            Me.ConTraficoDesktopCheckBox.UseVisualStyleBackColor = True
            '
            'TabPage2
            '
            Me.TabPage2.Controls.Add(Me.Label3)
            Me.TabPage2.Controls.Add(Me.FechaFinDateTimePicker)
            Me.TabPage2.Controls.Add(Me.Label2)
            Me.TabPage2.Controls.Add(Me.FechaInicioDateTimePicker)
            Me.TabPage2.Location = New System.Drawing.Point(4, 22)
            Me.TabPage2.Name = "TabPage2"
            Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage2.Size = New System.Drawing.Size(745, 124)
            Me.TabPage2.TabIndex = 1
            Me.TabPage2.Text = "Cargues"
            Me.TabPage2.UseVisualStyleBackColor = True
            '
            'Label3
            '
            Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(424, 56)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(57, 13)
            Me.Label3.TabIndex = 26
            Me.Label3.Text = "Fecha Fin:"
            '
            'FechaFinDateTimePicker
            '
            Me.FechaFinDateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaFinDateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaFinDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaFinDateTimePicker.Location = New System.Drawing.Point(505, 52)
            Me.FechaFinDateTimePicker.Name = "FechaFinDateTimePicker"
            Me.FechaFinDateTimePicker.Size = New System.Drawing.Size(84, 20)
            Me.FechaFinDateTimePicker.TabIndex = 25
            '
            'Label2
            '
            Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(155, 56)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(68, 13)
            Me.Label2.TabIndex = 24
            Me.Label2.Text = "Fecha Inicio:"
            '
            'FechaInicioDateTimePicker
            '
            Me.FechaInicioDateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaInicioDateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaInicioDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaInicioDateTimePicker.Location = New System.Drawing.Point(241, 52)
            Me.FechaInicioDateTimePicker.Name = "FechaInicioDateTimePicker"
            Me.FechaInicioDateTimePicker.Size = New System.Drawing.Size(84, 20)
            Me.FechaInicioDateTimePicker.TabIndex = 23
            '
            'FormExportarImagenes
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(777, 320)
            Me.Controls.Add(Me.TipoExportacionTabControl)
            Me.Controls.Add(Me.ComprimirDesktopCheckBox)
            Me.Controls.Add(Me.SeleccionarRutaButton)
            Me.Controls.Add(Me.RutaExportacionLabel)
            Me.Controls.Add(Me.RutaExportacionDesktopTextBox)
            Me.Controls.Add(Me.FiltroOficinaGroupBox)
            Me.Controls.Add(Me.ExportarButton)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormExportarImagenes"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Exportar Imagenes"
            Me.FiltroOficinaGroupBox.ResumeLayout(False)
            Me.FiltroOficinaGroupBox.PerformLayout()
            Me.TipoExportacionTabControl.ResumeLayout(False)
            Me.TabPage1.ResumeLayout(False)
            Me.TabPage1.PerformLayout()
            Me.TraficoPanel.ResumeLayout(False)
            Me.TraficoPanel.PerformLayout()
            Me.TabPage2.ResumeLayout(False)
            Me.TabPage2.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents OficinaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents OficinaLabel As System.Windows.Forms.Label
        Friend WithEvents COBDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents COBLabel As System.Windows.Forms.Label
        Friend WithEvents RegionalDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents RegionalLabel As System.Windows.Forms.Label
        Private WithEvents FechaProcesodateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaProcesolabel As System.Windows.Forms.Label
        Friend WithEvents ExportarButton As System.Windows.Forms.Button
        Friend WithEvents TransaccionDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents DocumentoLabel As System.Windows.Forms.Label
        Friend WithEvents FiltroOficinaGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents RutaExportacionDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents RutaExportacionLabel As System.Windows.Forms.Label
        Friend WithEvents SeleccionarRutaButton As System.Windows.Forms.Button
        Friend WithEvents RutaExportacionFolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents EsquemaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents ComprimirDesktopCheckBox As DesktopCheckBox.DesktopCheckBoxControl
        Friend WithEvents TipoExportacionTabControl As System.Windows.Forms.TabControl
        Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
        Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Private WithEvents FechaFinDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Private WithEvents FechaInicioDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents TraficoPanel As System.Windows.Forms.Panel
        Friend WithEvents SinTraficoDesktopCheckBox As DesktopCheckBox.DesktopCheckBoxControl
        Friend WithEvents ConTraficoDesktopCheckBox As DesktopCheckBox.DesktopCheckBoxControl
    End Class
End Namespace