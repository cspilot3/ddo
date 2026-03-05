Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Imaging.Controls
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PluginSearchControlParameters))
            Me.RegionalComboBox = New DesktopComboBoxControl()
            Me.RegionalLabel = New System.Windows.Forms.Label()
            Me.MainTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            Me.EstadoDesktopComboBox = New DesktopComboBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.EsquemaComboBox = New DesktopComboBoxControl()
            Me.EsquemaLabel = New System.Windows.Forms.Label()
            Me.COBLabel = New System.Windows.Forms.Label()
            Me.COBComboBox = New DesktopComboBoxControl()
            Me.OficinaLabel = New System.Windows.Forms.Label()
            Me.OficinaComboBox = New DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.BuscarPanel = New System.Windows.Forms.Panel()
            Me.BusquedaButton = New System.Windows.Forms.Button()
            Me.DocumentoComboBox = New DesktopComboBoxControl()
            Me.fechaProcesoPanel = New System.Windows.Forms.Panel()
            Me.FechaFinLabel = New System.Windows.Forms.Label()
            Me.FechaInicioLabel = New System.Windows.Forms.Label()
            Me.FechaFinDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaInicioDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.ProcesoRadioButton = New System.Windows.Forms.RadioButton()
            Me.MovimientoRadioButton = New System.Windows.Forms.RadioButton()
            Me.MainTableLayoutPanel.SuspendLayout()
            Me.BuscarPanel.SuspendLayout()
            Me.fechaProcesoPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'RegionalComboBox
            '
            Me.RegionalComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.RegionalComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.MainTableLayoutPanel.SetColumnSpan(Me.RegionalComboBox, 2)
            Me.RegionalComboBox.DisabledEnter = False
            Me.RegionalComboBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.RegionalComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.RegionalComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.RegionalComboBox.FormattingEnabled = True
            Me.RegionalComboBox.Location = New System.Drawing.Point(88, 3)
            Me.RegionalComboBox.Name = "RegionalComboBox"
            Me.RegionalComboBox.Size = New System.Drawing.Size(257, 21)
            Me.RegionalComboBox.TabIndex = 1
            '
            'RegionalLabel
            '
            Me.RegionalLabel.Dock = System.Windows.Forms.DockStyle.Top
            Me.RegionalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.RegionalLabel.Location = New System.Drawing.Point(3, 0)
            Me.RegionalLabel.Name = "RegionalLabel"
            Me.RegionalLabel.Size = New System.Drawing.Size(79, 25)
            Me.RegionalLabel.TabIndex = 0
            Me.RegionalLabel.Text = "Regional"
            Me.RegionalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'MainTableLayoutPanel
            '
            Me.MainTableLayoutPanel.ColumnCount = 3
            Me.MainTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
            Me.MainTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.MainTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.MainTableLayoutPanel.Controls.Add(Me.EstadoDesktopComboBox, 1, 5)
            Me.MainTableLayoutPanel.Controls.Add(Me.Label3, 0, 5)
            Me.MainTableLayoutPanel.Controls.Add(Me.EsquemaComboBox, 1, 3)
            Me.MainTableLayoutPanel.Controls.Add(Me.EsquemaLabel, 0, 3)
            Me.MainTableLayoutPanel.Controls.Add(Me.RegionalLabel, 0, 0)
            Me.MainTableLayoutPanel.Controls.Add(Me.RegionalComboBox, 1, 0)
            Me.MainTableLayoutPanel.Controls.Add(Me.COBLabel, 0, 1)
            Me.MainTableLayoutPanel.Controls.Add(Me.COBComboBox, 1, 1)
            Me.MainTableLayoutPanel.Controls.Add(Me.OficinaLabel, 0, 2)
            Me.MainTableLayoutPanel.Controls.Add(Me.OficinaComboBox, 1, 2)
            Me.MainTableLayoutPanel.Controls.Add(Me.Label1, 0, 4)
            Me.MainTableLayoutPanel.Controls.Add(Me.Label2, 0, 7)
            Me.MainTableLayoutPanel.Controls.Add(Me.BuscarPanel, 2, 8)
            Me.MainTableLayoutPanel.Controls.Add(Me.DocumentoComboBox, 1, 4)
            Me.MainTableLayoutPanel.Controls.Add(Me.fechaProcesoPanel, 1, 7)
            Me.MainTableLayoutPanel.Controls.Add(Me.ProcesoRadioButton, 1, 6)
            Me.MainTableLayoutPanel.Controls.Add(Me.MovimientoRadioButton, 2, 6)
            Me.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.MainTableLayoutPanel.Location = New System.Drawing.Point(10, 10)
            Me.MainTableLayoutPanel.Name = "MainTableLayoutPanel"
            Me.MainTableLayoutPanel.RowCount = 9
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
            Me.MainTableLayoutPanel.Size = New System.Drawing.Size(348, 278)
            Me.MainTableLayoutPanel.TabIndex = 25
            '
            'EstadoDesktopComboBox
            '
            Me.EstadoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EstadoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.MainTableLayoutPanel.SetColumnSpan(Me.EstadoDesktopComboBox, 2)
            Me.EstadoDesktopComboBox.DisabledEnter = False
            Me.EstadoDesktopComboBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.EstadoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EstadoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EstadoDesktopComboBox.FormattingEnabled = True
            Me.EstadoDesktopComboBox.Location = New System.Drawing.Point(88, 153)
            Me.EstadoDesktopComboBox.Name = "EstadoDesktopComboBox"
            Me.EstadoDesktopComboBox.Size = New System.Drawing.Size(257, 21)
            Me.EstadoDesktopComboBox.TabIndex = 14
            '
            'Label3
            '
            Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(3, 150)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(79, 24)
            Me.Label3.TabIndex = 13
            Me.Label3.Text = "Estado"
            Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'EsquemaComboBox
            '
            Me.EsquemaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.MainTableLayoutPanel.SetColumnSpan(Me.EsquemaComboBox, 2)
            Me.EsquemaComboBox.DisabledEnter = False
            Me.EsquemaComboBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.EsquemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaComboBox.FormattingEnabled = True
            Me.EsquemaComboBox.Location = New System.Drawing.Point(88, 93)
            Me.EsquemaComboBox.Name = "EsquemaComboBox"
            Me.EsquemaComboBox.Size = New System.Drawing.Size(257, 21)
            Me.EsquemaComboBox.TabIndex = 7
            '
            'EsquemaLabel
            '
            Me.EsquemaLabel.Dock = System.Windows.Forms.DockStyle.Top
            Me.EsquemaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EsquemaLabel.Location = New System.Drawing.Point(3, 90)
            Me.EsquemaLabel.Name = "EsquemaLabel"
            Me.EsquemaLabel.Size = New System.Drawing.Size(79, 27)
            Me.EsquemaLabel.TabIndex = 6
            Me.EsquemaLabel.Text = "Esquema"
            Me.EsquemaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'COBLabel
            '
            Me.COBLabel.Dock = System.Windows.Forms.DockStyle.Top
            Me.COBLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.COBLabel.Location = New System.Drawing.Point(3, 30)
            Me.COBLabel.Name = "COBLabel"
            Me.COBLabel.Size = New System.Drawing.Size(79, 25)
            Me.COBLabel.TabIndex = 2
            Me.COBLabel.Text = "COB"
            Me.COBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'COBComboBox
            '
            Me.COBComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.COBComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.MainTableLayoutPanel.SetColumnSpan(Me.COBComboBox, 2)
            Me.COBComboBox.DisabledEnter = False
            Me.COBComboBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.COBComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.COBComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.COBComboBox.FormattingEnabled = True
            Me.COBComboBox.Location = New System.Drawing.Point(88, 33)
            Me.COBComboBox.Name = "COBComboBox"
            Me.COBComboBox.Size = New System.Drawing.Size(257, 21)
            Me.COBComboBox.TabIndex = 3
            '
            'OficinaLabel
            '
            Me.OficinaLabel.Dock = System.Windows.Forms.DockStyle.Top
            Me.OficinaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OficinaLabel.Location = New System.Drawing.Point(3, 60)
            Me.OficinaLabel.Name = "OficinaLabel"
            Me.OficinaLabel.Size = New System.Drawing.Size(79, 25)
            Me.OficinaLabel.TabIndex = 4
            Me.OficinaLabel.Text = "Oficina"
            Me.OficinaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'OficinaComboBox
            '
            Me.OficinaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OficinaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.MainTableLayoutPanel.SetColumnSpan(Me.OficinaComboBox, 2)
            Me.OficinaComboBox.DisabledEnter = False
            Me.OficinaComboBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.OficinaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OficinaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OficinaComboBox.FormattingEnabled = True
            Me.OficinaComboBox.Location = New System.Drawing.Point(88, 63)
            Me.OficinaComboBox.Name = "OficinaComboBox"
            Me.OficinaComboBox.Size = New System.Drawing.Size(257, 21)
            Me.OficinaComboBox.TabIndex = 5
            '
            'Label1
            '
            Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(3, 120)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(79, 27)
            Me.Label1.TabIndex = 8
            Me.Label1.Text = "Documento"
            Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Label2
            '
            Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(3, 210)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(79, 24)
            Me.Label2.TabIndex = 10
            Me.Label2.Text = "Fecha"
            Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'BuscarPanel
            '
            Me.BuscarPanel.Controls.Add(Me.BusquedaButton)
            Me.BuscarPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.BuscarPanel.Location = New System.Drawing.Point(219, 243)
            Me.BuscarPanel.Name = "BuscarPanel"
            Me.BuscarPanel.Size = New System.Drawing.Size(126, 32)
            Me.BuscarPanel.TabIndex = 12
            '
            'BusquedaButton
            '
            Me.BusquedaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BusquedaButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BusquedaButton.Image = CType(resources.GetObject("BusquedaButton.Image"), System.Drawing.Image)
            Me.BusquedaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BusquedaButton.Location = New System.Drawing.Point(52, 3)
            Me.BusquedaButton.Name = "BusquedaButton"
            Me.BusquedaButton.Size = New System.Drawing.Size(71, 25)
            Me.BusquedaButton.TabIndex = 0
            Me.BusquedaButton.Text = "&Buscar"
            Me.BusquedaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BusquedaButton.UseVisualStyleBackColor = True
            '
            'DocumentoComboBox
            '
            Me.DocumentoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DocumentoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.MainTableLayoutPanel.SetColumnSpan(Me.DocumentoComboBox, 2)
            Me.DocumentoComboBox.DisabledEnter = False
            Me.DocumentoComboBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.DocumentoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DocumentoComboBox.FormattingEnabled = True
            Me.DocumentoComboBox.Location = New System.Drawing.Point(88, 123)
            Me.DocumentoComboBox.Name = "DocumentoComboBox"
            Me.DocumentoComboBox.Size = New System.Drawing.Size(257, 21)
            Me.DocumentoComboBox.TabIndex = 9
            '
            'fechaProcesoPanel
            '
            Me.MainTableLayoutPanel.SetColumnSpan(Me.fechaProcesoPanel, 2)
            Me.fechaProcesoPanel.Controls.Add(Me.FechaFinLabel)
            Me.fechaProcesoPanel.Controls.Add(Me.FechaInicioLabel)
            Me.fechaProcesoPanel.Controls.Add(Me.FechaFinDateTimePicker)
            Me.fechaProcesoPanel.Controls.Add(Me.FechaInicioDateTimePicker)
            Me.fechaProcesoPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.fechaProcesoPanel.Location = New System.Drawing.Point(88, 213)
            Me.fechaProcesoPanel.Name = "fechaProcesoPanel"
            Me.fechaProcesoPanel.Size = New System.Drawing.Size(257, 24)
            Me.fechaProcesoPanel.TabIndex = 15
            '
            'FechaFinLabel
            '
            Me.FechaFinLabel.AutoSize = True
            Me.FechaFinLabel.Location = New System.Drawing.Point(132, 5)
            Me.FechaFinLabel.Name = "FechaFinLabel"
            Me.FechaFinLabel.Size = New System.Drawing.Size(21, 13)
            Me.FechaFinLabel.TabIndex = 14
            Me.FechaFinLabel.Text = "Fin"
            '
            'FechaInicioLabel
            '
            Me.FechaInicioLabel.AutoSize = True
            Me.FechaInicioLabel.Location = New System.Drawing.Point(3, 5)
            Me.FechaInicioLabel.Name = "FechaInicioLabel"
            Me.FechaInicioLabel.Size = New System.Drawing.Size(32, 13)
            Me.FechaInicioLabel.TabIndex = 13
            Me.FechaInicioLabel.Text = "Inicio"
            '
            'FechaFinDateTimePicker
            '
            Me.FechaFinDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.FechaFinDateTimePicker.Location = New System.Drawing.Point(159, 1)
            Me.FechaFinDateTimePicker.Name = "FechaFinDateTimePicker"
            Me.FechaFinDateTimePicker.Size = New System.Drawing.Size(84, 20)
            Me.FechaFinDateTimePicker.TabIndex = 12
            '
            'FechaInicioDateTimePicker
            '
            Me.FechaInicioDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.FechaInicioDateTimePicker.Location = New System.Drawing.Point(41, 1)
            Me.FechaInicioDateTimePicker.Name = "FechaInicioDateTimePicker"
            Me.FechaInicioDateTimePicker.Size = New System.Drawing.Size(84, 20)
            Me.FechaInicioDateTimePicker.TabIndex = 11
            '
            'ProcesoRadioButton
            '
            Me.ProcesoRadioButton.AutoSize = True
            Me.ProcesoRadioButton.Checked = True
            Me.ProcesoRadioButton.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.ProcesoRadioButton.Location = New System.Drawing.Point(88, 190)
            Me.ProcesoRadioButton.Name = "ProcesoRadioButton"
            Me.ProcesoRadioButton.Size = New System.Drawing.Size(125, 17)
            Me.ProcesoRadioButton.TabIndex = 17
            Me.ProcesoRadioButton.TabStop = True
            Me.ProcesoRadioButton.Text = "Proceso"
            Me.ProcesoRadioButton.UseVisualStyleBackColor = True
            '
            'MovimientoRadioButton
            '
            Me.MovimientoRadioButton.AutoSize = True
            Me.MovimientoRadioButton.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.MovimientoRadioButton.Location = New System.Drawing.Point(219, 190)
            Me.MovimientoRadioButton.Name = "MovimientoRadioButton"
            Me.MovimientoRadioButton.Size = New System.Drawing.Size(126, 17)
            Me.MovimientoRadioButton.TabIndex = 18
            Me.MovimientoRadioButton.Text = "Movimiento"
            Me.MovimientoRadioButton.UseVisualStyleBackColor = True
            '
            'PluginSearchControlParameters
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoScroll = True
            Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Controls.Add(Me.MainTableLayoutPanel)
            Me.Name = "PluginSearchControlParameters"
            Me.Padding = New System.Windows.Forms.Padding(10, 10, 10, 5)
            Me.Size = New System.Drawing.Size(368, 308)
            Me.MainTableLayoutPanel.ResumeLayout(False)
            Me.MainTableLayoutPanel.PerformLayout()
            Me.BuscarPanel.ResumeLayout(False)
            Me.fechaProcesoPanel.ResumeLayout(False)
            Me.fechaProcesoPanel.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents RegionalComboBox As DesktopComboBoxControl 'System.Windows.Forms.ComboBox
        Friend WithEvents RegionalLabel As System.Windows.Forms.Label
        Friend WithEvents MainTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents COBLabel As System.Windows.Forms.Label
        Friend WithEvents COBComboBox As DesktopComboBoxControl 'System.Windows.Forms.ComboBox
        Friend WithEvents OficinaLabel As System.Windows.Forms.Label
        Friend WithEvents OficinaComboBox As DesktopComboBoxControl 'System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents DocumentoComboBox As DesktopComboBoxControl 'System.Windows.Forms.ComboBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents FechaInicioDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents EsquemaComboBox As DesktopComboBoxControl
        Friend WithEvents EsquemaLabel As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents EstadoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents fechaProcesoPanel As System.Windows.Forms.Panel
        Friend WithEvents FechaFinDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaFinLabel As System.Windows.Forms.Label
        Friend WithEvents FechaInicioLabel As System.Windows.Forms.Label
        Friend WithEvents ProcesoRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents MovimientoRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents BuscarPanel As System.Windows.Forms.Panel
        Friend WithEvents BusquedaButton As System.Windows.Forms.Button

    End Class
End Namespace