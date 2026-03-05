Namespace Controls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class PluginSearchControlParameters
        Inherits System.Windows.Forms.UserControl

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
            Me.ValorLabel = New System.Windows.Forms.Label()
            Me.PuntoLabel = New System.Windows.Forms.Label()
            Me.CampoLabel = New System.Windows.Forms.Label()
            Me.FechaProcesoLabel = New System.Windows.Forms.Label()
            Me.MainTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            Me.ValorTextBox = New System.Windows.Forms.TextBox()
            Me.BuscarPanel = New System.Windows.Forms.Panel()
            Me.BusquedaButton = New System.Windows.Forms.Button()
            Me.FechaProcesoPanel = New System.Windows.Forms.Panel()
            Me.FechaFinLabel = New System.Windows.Forms.Label()
            Me.FechaInicioLabel = New System.Windows.Forms.Label()
            Me.FechaFinDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaInicioDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.PuntoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.CampoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.MainTableLayoutPanel.SuspendLayout()
            Me.BuscarPanel.SuspendLayout()
            Me.FechaProcesoPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'ValorLabel
            '
            Me.ValorLabel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValorLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValorLabel.Location = New System.Drawing.Point(3, 57)
            Me.ValorLabel.Name = "ValorLabel"
            Me.ValorLabel.Size = New System.Drawing.Size(60, 27)
            Me.ValorLabel.TabIndex = 25
            Me.ValorLabel.Text = "Valor"
            Me.ValorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'PuntoLabel
            '
            Me.PuntoLabel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.PuntoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PuntoLabel.Location = New System.Drawing.Point(3, 0)
            Me.PuntoLabel.Name = "PuntoLabel"
            Me.PuntoLabel.Size = New System.Drawing.Size(60, 29)
            Me.PuntoLabel.TabIndex = 21
            Me.PuntoLabel.Text = "Punto"
            Me.PuntoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'CampoLabel
            '
            Me.CampoLabel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CampoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CampoLabel.Location = New System.Drawing.Point(3, 29)
            Me.CampoLabel.Name = "CampoLabel"
            Me.CampoLabel.Size = New System.Drawing.Size(60, 28)
            Me.CampoLabel.TabIndex = 28
            Me.CampoLabel.Text = "Campo"
            Me.CampoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'FechaProcesoLabel
            '
            Me.FechaProcesoLabel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.FechaProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoLabel.Location = New System.Drawing.Point(3, 84)
            Me.FechaProcesoLabel.Name = "FechaProcesoLabel"
            Me.FechaProcesoLabel.Size = New System.Drawing.Size(60, 31)
            Me.FechaProcesoLabel.TabIndex = 30
            Me.FechaProcesoLabel.Text = "Fecha Proceso"
            Me.FechaProcesoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'MainTableLayoutPanel
            '
            Me.MainTableLayoutPanel.ColumnCount = 2
            Me.MainTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
            Me.MainTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.MainTableLayoutPanel.Controls.Add(Me.FechaProcesoLabel, 0, 3)
            Me.MainTableLayoutPanel.Controls.Add(Me.CampoLabel, 0, 1)
            Me.MainTableLayoutPanel.Controls.Add(Me.PuntoLabel, 0, 0)
            Me.MainTableLayoutPanel.Controls.Add(Me.ValorTextBox, 1, 2)
            Me.MainTableLayoutPanel.Controls.Add(Me.ValorLabel, 0, 2)
            Me.MainTableLayoutPanel.Controls.Add(Me.BuscarPanel, 1, 4)
            Me.MainTableLayoutPanel.Controls.Add(Me.FechaProcesoPanel, 1, 3)
            Me.MainTableLayoutPanel.Controls.Add(Me.PuntoComboBox, 1, 0)
            Me.MainTableLayoutPanel.Controls.Add(Me.CampoComboBox, 1, 1)
            Me.MainTableLayoutPanel.Location = New System.Drawing.Point(10, 4)
            Me.MainTableLayoutPanel.Name = "MainTableLayoutPanel"
            Me.MainTableLayoutPanel.RowCount = 5
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 104.0!))
            Me.MainTableLayoutPanel.Size = New System.Drawing.Size(312, 150)
            Me.MainTableLayoutPanel.TabIndex = 0
            '
            'ValorTextBox
            '
            Me.ValorTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValorTextBox.Location = New System.Drawing.Point(69, 60)
            Me.ValorTextBox.Name = "ValorTextBox"
            Me.ValorTextBox.Size = New System.Drawing.Size(240, 20)
            Me.ValorTextBox.TabIndex = 3
            '
            'BuscarPanel
            '
            Me.BuscarPanel.Controls.Add(Me.BusquedaButton)
            Me.BuscarPanel.Location = New System.Drawing.Point(69, 118)
            Me.BuscarPanel.Name = "BuscarPanel"
            Me.BuscarPanel.Size = New System.Drawing.Size(240, 30)
            Me.BuscarPanel.TabIndex = 7
            '
            'BusquedaButton
            '
            Me.BusquedaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BusquedaButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BusquedaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BusquedaButton.Location = New System.Drawing.Point(162, 3)
            Me.BusquedaButton.Name = "BusquedaButton"
            Me.BusquedaButton.Size = New System.Drawing.Size(75, 23)
            Me.BusquedaButton.TabIndex = 8
            Me.BusquedaButton.Text = "&Buscar"
            Me.BusquedaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BusquedaButton.UseVisualStyleBackColor = True
            '
            'FechaProcesoPanel
            '
            Me.FechaProcesoPanel.Controls.Add(Me.FechaFinLabel)
            Me.FechaProcesoPanel.Controls.Add(Me.FechaInicioLabel)
            Me.FechaProcesoPanel.Controls.Add(Me.FechaFinDateTimePicker)
            Me.FechaProcesoPanel.Controls.Add(Me.FechaInicioDateTimePicker)
            Me.FechaProcesoPanel.Location = New System.Drawing.Point(69, 87)
            Me.FechaProcesoPanel.Name = "FechaProcesoPanel"
            Me.FechaProcesoPanel.Size = New System.Drawing.Size(240, 25)
            Me.FechaProcesoPanel.TabIndex = 4
            '
            'FechaFinLabel
            '
            Me.FechaFinLabel.AutoSize = True
            Me.FechaFinLabel.Location = New System.Drawing.Point(128, 7)
            Me.FechaFinLabel.Name = "FechaFinLabel"
            Me.FechaFinLabel.Size = New System.Drawing.Size(21, 13)
            Me.FechaFinLabel.TabIndex = 18
            Me.FechaFinLabel.Text = "Fin"
            '
            'FechaInicioLabel
            '
            Me.FechaInicioLabel.AutoSize = True
            Me.FechaInicioLabel.Location = New System.Drawing.Point(3, 7)
            Me.FechaInicioLabel.Name = "FechaInicioLabel"
            Me.FechaInicioLabel.Size = New System.Drawing.Size(32, 13)
            Me.FechaInicioLabel.TabIndex = 17
            Me.FechaInicioLabel.Text = "Inicio"
            '
            'FechaFinDateTimePicker
            '
            Me.FechaFinDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.FechaFinDateTimePicker.Location = New System.Drawing.Point(155, 3)
            Me.FechaFinDateTimePicker.Name = "FechaFinDateTimePicker"
            Me.FechaFinDateTimePicker.Size = New System.Drawing.Size(84, 20)
            Me.FechaFinDateTimePicker.TabIndex = 6
            '
            'FechaInicioDateTimePicker
            '
            Me.FechaInicioDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.FechaInicioDateTimePicker.Location = New System.Drawing.Point(37, 3)
            Me.FechaInicioDateTimePicker.Name = "FechaInicioDateTimePicker"
            Me.FechaInicioDateTimePicker.Size = New System.Drawing.Size(84, 20)
            Me.FechaInicioDateTimePicker.TabIndex = 5
            '
            'PuntoComboBox
            '
            Me.PuntoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.PuntoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.PuntoComboBox.DisabledEnter = False
            Me.PuntoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.PuntoComboBox.fk_Campo = 0
            Me.PuntoComboBox.fk_Documento = 0
            Me.PuntoComboBox.fk_Validacion = 0
            Me.PuntoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.PuntoComboBox.FormattingEnabled = True
            Me.PuntoComboBox.Location = New System.Drawing.Point(69, 3)
            Me.PuntoComboBox.Name = "PuntoComboBox"
            Me.PuntoComboBox.Size = New System.Drawing.Size(240, 21)
            Me.PuntoComboBox.TabIndex = 1
            '
            'CampoComboBox
            '
            Me.CampoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.CampoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.CampoComboBox.DisabledEnter = False
            Me.CampoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CampoComboBox.fk_Campo = 0
            Me.CampoComboBox.fk_Documento = 0
            Me.CampoComboBox.fk_Validacion = 0
            Me.CampoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CampoComboBox.FormattingEnabled = True
            Me.CampoComboBox.Location = New System.Drawing.Point(69, 32)
            Me.CampoComboBox.Name = "CampoComboBox"
            Me.CampoComboBox.Size = New System.Drawing.Size(240, 21)
            Me.CampoComboBox.TabIndex = 2
            '
            'PluginSearchControlParameters
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoScroll = True
            Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Controls.Add(Me.MainTableLayoutPanel)
            Me.Name = "PluginSearchControlParameters"
            Me.Padding = New System.Windows.Forms.Padding(10)
            Me.Size = New System.Drawing.Size(332, 164)
            Me.MainTableLayoutPanel.ResumeLayout(False)
            Me.MainTableLayoutPanel.PerformLayout()
            Me.BuscarPanel.ResumeLayout(False)
            Me.FechaProcesoPanel.ResumeLayout(False)
            Me.FechaProcesoPanel.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ValorLabel As System.Windows.Forms.Label
        Friend WithEvents PuntoLabel As System.Windows.Forms.Label
        Friend WithEvents CampoLabel As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents MainTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents ValorTextBox As System.Windows.Forms.TextBox
        Friend WithEvents BuscarPanel As System.Windows.Forms.Panel
        Friend WithEvents BusquedaButton As System.Windows.Forms.Button
        Friend WithEvents FechaProcesoPanel As System.Windows.Forms.Panel
        Friend WithEvents FechaFinLabel As System.Windows.Forms.Label
        Friend WithEvents FechaInicioLabel As System.Windows.Forms.Label
        Friend WithEvents FechaFinDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaInicioDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents PuntoComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents CampoComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl

    End Class

End Namespace