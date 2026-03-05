<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEditarFacturacion
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
        Me.components = New System.ComponentModel.Container()
        Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEditarFacturacion))
        Me.ProyectoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.ProyectoClienteLabel = New System.Windows.Forms.Label()
        Me.EntidadClienteComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.EntidadClienteLabel = New System.Windows.Forms.Label()
        Me.EsquemaClienteComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.EsquemaClienteLabel = New System.Windows.Forms.Label()
        Me.FechaMovimientoLabel = New System.Windows.Forms.Label()
        Me.FechaMovimientoDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.HoraMovimientoDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.CantidadLabel = New System.Windows.Forms.Label()
        Me.CantidadTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
        Me.ClienteGroupBox = New System.Windows.Forms.GroupBox()
        Me.FechaCantidadGroupBox = New System.Windows.Forms.GroupBox()
        Me.ModificarButton = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.CerrarButton = New System.Windows.Forms.Button()
        Me.EsquemaFacturacionGroupBox = New System.Windows.Forms.GroupBox()
        Me.EquemaLabel = New System.Windows.Forms.Label()
        Me.EsquemaFacturacionComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.ServicioLabel = New System.Windows.Forms.Label()
        Me.ServicioFacturacionComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.IDLabel = New System.Windows.Forms.Label()
        Me.ClienteGroupBox.SuspendLayout()
        Me.FechaCantidadGroupBox.SuspendLayout()
        Me.EsquemaFacturacionGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'ProyectoComboBox
        '
        Me.ProyectoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ProyectoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ProyectoComboBox.DisabledEnter = False
        Me.ProyectoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ProyectoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ProyectoComboBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProyectoComboBox.FormattingEnabled = True
        Me.ProyectoComboBox.Location = New System.Drawing.Point(118, 43)
        Me.ProyectoComboBox.Name = "ProyectoComboBox"
        Me.ProyectoComboBox.Size = New System.Drawing.Size(208, 21)
        Me.ProyectoComboBox.TabIndex = 2
        '
        'ProyectoClienteLabel
        '
        Me.ProyectoClienteLabel.AutoSize = True
        Me.ProyectoClienteLabel.Location = New System.Drawing.Point(9, 46)
        Me.ProyectoClienteLabel.Name = "ProyectoClienteLabel"
        Me.ProyectoClienteLabel.Size = New System.Drawing.Size(103, 13)
        Me.ProyectoClienteLabel.TabIndex = 6
        Me.ProyectoClienteLabel.Text = "Proyecto Cliente:"
        '
        'EntidadClienteComboBox
        '
        Me.EntidadClienteComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.EntidadClienteComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.EntidadClienteComboBox.DisabledEnter = False
        Me.EntidadClienteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EntidadClienteComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EntidadClienteComboBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EntidadClienteComboBox.FormattingEnabled = True
        Me.EntidadClienteComboBox.Location = New System.Drawing.Point(118, 13)
        Me.EntidadClienteComboBox.Name = "EntidadClienteComboBox"
        Me.EntidadClienteComboBox.Size = New System.Drawing.Size(208, 21)
        Me.EntidadClienteComboBox.TabIndex = 1
        '
        'EntidadClienteLabel
        '
        Me.EntidadClienteLabel.AutoSize = True
        Me.EntidadClienteLabel.Location = New System.Drawing.Point(9, 16)
        Me.EntidadClienteLabel.Name = "EntidadClienteLabel"
        Me.EntidadClienteLabel.Size = New System.Drawing.Size(94, 13)
        Me.EntidadClienteLabel.TabIndex = 4
        Me.EntidadClienteLabel.Text = "Entidad Cliente:"
        '
        'EsquemaClienteComboBox
        '
        Me.EsquemaClienteComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.EsquemaClienteComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.EsquemaClienteComboBox.DisabledEnter = False
        Me.EsquemaClienteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EsquemaClienteComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EsquemaClienteComboBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EsquemaClienteComboBox.FormattingEnabled = True
        Me.EsquemaClienteComboBox.Location = New System.Drawing.Point(118, 72)
        Me.EsquemaClienteComboBox.Name = "EsquemaClienteComboBox"
        Me.EsquemaClienteComboBox.Size = New System.Drawing.Size(208, 21)
        Me.EsquemaClienteComboBox.TabIndex = 3
        '
        'EsquemaClienteLabel
        '
        Me.EsquemaClienteLabel.AutoSize = True
        Me.EsquemaClienteLabel.Location = New System.Drawing.Point(9, 75)
        Me.EsquemaClienteLabel.Name = "EsquemaClienteLabel"
        Me.EsquemaClienteLabel.Size = New System.Drawing.Size(103, 13)
        Me.EsquemaClienteLabel.TabIndex = 8
        Me.EsquemaClienteLabel.Text = "Esquema Cliente:"
        '
        'FechaMovimientoLabel
        '
        Me.FechaMovimientoLabel.AutoSize = True
        Me.FechaMovimientoLabel.Location = New System.Drawing.Point(9, 17)
        Me.FechaMovimientoLabel.Name = "FechaMovimientoLabel"
        Me.FechaMovimientoLabel.Size = New System.Drawing.Size(113, 13)
        Me.FechaMovimientoLabel.TabIndex = 10
        Me.FechaMovimientoLabel.Text = "Fecha Movimiento:"
        '
        'FechaMovimientoDateTimePicker
        '
        Me.FechaMovimientoDateTimePicker.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FechaMovimientoDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.FechaMovimientoDateTimePicker.Location = New System.Drawing.Point(147, 13)
        Me.FechaMovimientoDateTimePicker.Name = "FechaMovimientoDateTimePicker"
        Me.FechaMovimientoDateTimePicker.Size = New System.Drawing.Size(94, 21)
        Me.FechaMovimientoDateTimePicker.TabIndex = 6
        '
        'HoraMovimientoDateTimePicker
        '
        Me.HoraMovimientoDateTimePicker.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HoraMovimientoDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.HoraMovimientoDateTimePicker.Location = New System.Drawing.Point(247, 13)
        Me.HoraMovimientoDateTimePicker.Name = "HoraMovimientoDateTimePicker"
        Me.HoraMovimientoDateTimePicker.Size = New System.Drawing.Size(79, 21)
        Me.HoraMovimientoDateTimePicker.TabIndex = 7
        '
        'CantidadLabel
        '
        Me.CantidadLabel.AutoSize = True
        Me.CantidadLabel.Location = New System.Drawing.Point(9, 42)
        Me.CantidadLabel.Name = "CantidadLabel"
        Me.CantidadLabel.Size = New System.Drawing.Size(60, 13)
        Me.CantidadLabel.TabIndex = 13
        Me.CantidadLabel.Text = "Cantidad:"
        '
        'CantidadTextBox
        '
        Me.CantidadTextBox.DisabledEnter = False
        Me.CantidadTextBox.DisabledTab = False
        Me.CantidadTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.CantidadTextBox.FocusOut = System.Drawing.Color.White
        Me.CantidadTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CantidadTextBox.Location = New System.Drawing.Point(147, 42)
        Me.CantidadTextBox.Name = "CantidadTextBox"
        Rango1.MaxValue = 2147483647
        Rango1.MinValue = 0
        Me.CantidadTextBox.Rango = Rango1
        Me.CantidadTextBox.Size = New System.Drawing.Size(179, 21)
        Me.CantidadTextBox.TabIndex = 8
        Me.CantidadTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.CantidadTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Numerico
        '
        'ClienteGroupBox
        '
        Me.ClienteGroupBox.Controls.Add(Me.ProyectoClienteLabel)
        Me.ClienteGroupBox.Controls.Add(Me.EntidadClienteLabel)
        Me.ClienteGroupBox.Controls.Add(Me.EntidadClienteComboBox)
        Me.ClienteGroupBox.Controls.Add(Me.ProyectoComboBox)
        Me.ClienteGroupBox.Controls.Add(Me.EsquemaClienteLabel)
        Me.ClienteGroupBox.Controls.Add(Me.EsquemaClienteComboBox)
        Me.ClienteGroupBox.Location = New System.Drawing.Point(3, 24)
        Me.ClienteGroupBox.Name = "ClienteGroupBox"
        Me.ClienteGroupBox.Size = New System.Drawing.Size(336, 103)
        Me.ClienteGroupBox.TabIndex = 16
        Me.ClienteGroupBox.TabStop = False
        '
        'FechaCantidadGroupBox
        '
        Me.FechaCantidadGroupBox.Controls.Add(Me.FechaMovimientoLabel)
        Me.FechaCantidadGroupBox.Controls.Add(Me.FechaMovimientoDateTimePicker)
        Me.FechaCantidadGroupBox.Controls.Add(Me.HoraMovimientoDateTimePicker)
        Me.FechaCantidadGroupBox.Controls.Add(Me.CantidadTextBox)
        Me.FechaCantidadGroupBox.Controls.Add(Me.CantidadLabel)
        Me.FechaCantidadGroupBox.Location = New System.Drawing.Point(3, 204)
        Me.FechaCantidadGroupBox.Name = "FechaCantidadGroupBox"
        Me.FechaCantidadGroupBox.Size = New System.Drawing.Size(336, 70)
        Me.FechaCantidadGroupBox.TabIndex = 20
        Me.FechaCantidadGroupBox.TabStop = False
        '
        'ModificarButton
        '
        Me.ModificarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.Aceptar
        Me.ModificarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ModificarButton.Location = New System.Drawing.Point(166, 280)
        Me.ModificarButton.Name = "ModificarButton"
        Me.ModificarButton.Size = New System.Drawing.Size(88, 27)
        Me.ModificarButton.TabIndex = 9
        Me.ModificarButton.Text = "&Modificar"
        Me.ModificarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.ModificarButton, "Modificar la facturación")
        Me.ModificarButton.UseVisualStyleBackColor = True
        '
        'CerrarButton
        '
        Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CerrarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnSalir
        Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CerrarButton.Location = New System.Drawing.Point(260, 280)
        Me.CerrarButton.Name = "CerrarButton"
        Me.CerrarButton.Size = New System.Drawing.Size(78, 27)
        Me.CerrarButton.TabIndex = 10
        Me.CerrarButton.Text = "&Cerrar"
        Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.CerrarButton, "Modificar la facturación")
        Me.CerrarButton.UseVisualStyleBackColor = True
        '
        'EsquemaFacturacionGroupBox
        '
        Me.EsquemaFacturacionGroupBox.Controls.Add(Me.EquemaLabel)
        Me.EsquemaFacturacionGroupBox.Controls.Add(Me.EsquemaFacturacionComboBox)
        Me.EsquemaFacturacionGroupBox.Controls.Add(Me.ServicioLabel)
        Me.EsquemaFacturacionGroupBox.Controls.Add(Me.ServicioFacturacionComboBox)
        Me.EsquemaFacturacionGroupBox.Location = New System.Drawing.Point(3, 127)
        Me.EsquemaFacturacionGroupBox.Name = "EsquemaFacturacionGroupBox"
        Me.EsquemaFacturacionGroupBox.Size = New System.Drawing.Size(336, 76)
        Me.EsquemaFacturacionGroupBox.TabIndex = 18
        Me.EsquemaFacturacionGroupBox.TabStop = False
        '
        'EquemaLabel
        '
        Me.EquemaLabel.AutoSize = True
        Me.EquemaLabel.Location = New System.Drawing.Point(9, 17)
        Me.EquemaLabel.Name = "EquemaLabel"
        Me.EquemaLabel.Size = New System.Drawing.Size(61, 13)
        Me.EquemaLabel.TabIndex = 0
        Me.EquemaLabel.Text = "Esquema:"
        '
        'EsquemaFacturacionComboBox
        '
        Me.EsquemaFacturacionComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.EsquemaFacturacionComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.EsquemaFacturacionComboBox.DisabledEnter = False
        Me.EsquemaFacturacionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EsquemaFacturacionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EsquemaFacturacionComboBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EsquemaFacturacionComboBox.FormattingEnabled = True
        Me.EsquemaFacturacionComboBox.Location = New System.Drawing.Point(76, 14)
        Me.EsquemaFacturacionComboBox.Name = "EsquemaFacturacionComboBox"
        Me.EsquemaFacturacionComboBox.Size = New System.Drawing.Size(250, 21)
        Me.EsquemaFacturacionComboBox.TabIndex = 4
        '
        'ServicioLabel
        '
        Me.ServicioLabel.AutoSize = True
        Me.ServicioLabel.Location = New System.Drawing.Point(9, 47)
        Me.ServicioLabel.Name = "ServicioLabel"
        Me.ServicioLabel.Size = New System.Drawing.Size(55, 13)
        Me.ServicioLabel.TabIndex = 2
        Me.ServicioLabel.Text = "Servicio:"
        '
        'ServicioFacturacionComboBox
        '
        Me.ServicioFacturacionComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ServicioFacturacionComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ServicioFacturacionComboBox.DisabledEnter = False
        Me.ServicioFacturacionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ServicioFacturacionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ServicioFacturacionComboBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ServicioFacturacionComboBox.FormattingEnabled = True
        Me.ServicioFacturacionComboBox.Location = New System.Drawing.Point(76, 44)
        Me.ServicioFacturacionComboBox.Name = "ServicioFacturacionComboBox"
        Me.ServicioFacturacionComboBox.Size = New System.Drawing.Size(250, 21)
        Me.ServicioFacturacionComboBox.TabIndex = 5
        '
        'IDLabel
        '
        Me.IDLabel.AutoSize = True
        Me.IDLabel.ForeColor = System.Drawing.Color.DarkGreen
        Me.IDLabel.Location = New System.Drawing.Point(0, 9)
        Me.IDLabel.Name = "IDLabel"
        Me.IDLabel.Size = New System.Drawing.Size(20, 13)
        Me.IDLabel.TabIndex = 21
        Me.IDLabel.Text = "ID"
        Me.IDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FormEditarFacturacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CerrarButton
        Me.ClientSize = New System.Drawing.Size(341, 312)
        Me.Controls.Add(Me.IDLabel)
        Me.Controls.Add(Me.EsquemaFacturacionGroupBox)
        Me.Controls.Add(Me.CerrarButton)
        Me.Controls.Add(Me.ModificarButton)
        Me.Controls.Add(Me.FechaCantidadGroupBox)
        Me.Controls.Add(Me.ClienteGroupBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormEditarFacturacion"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Editar Facturación"
        Me.ClienteGroupBox.ResumeLayout(False)
        Me.ClienteGroupBox.PerformLayout()
        Me.FechaCantidadGroupBox.ResumeLayout(False)
        Me.FechaCantidadGroupBox.PerformLayout()
        Me.EsquemaFacturacionGroupBox.ResumeLayout(False)
        Me.EsquemaFacturacionGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ProyectoComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    Friend WithEvents ProyectoClienteLabel As System.Windows.Forms.Label
    Friend WithEvents EntidadClienteComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    Friend WithEvents EntidadClienteLabel As System.Windows.Forms.Label
    Friend WithEvents EsquemaClienteComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    Friend WithEvents EsquemaClienteLabel As System.Windows.Forms.Label
    Friend WithEvents FechaMovimientoLabel As System.Windows.Forms.Label
    Friend WithEvents FechaMovimientoDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents HoraMovimientoDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents CantidadLabel As System.Windows.Forms.Label
    Friend WithEvents CantidadTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
    Friend WithEvents ClienteGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents FechaCantidadGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ModificarButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents CerrarButton As System.Windows.Forms.Button
    Friend WithEvents EsquemaFacturacionGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents EquemaLabel As System.Windows.Forms.Label
    Friend WithEvents EsquemaFacturacionComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    Friend WithEvents ServicioLabel As System.Windows.Forms.Label
    Friend WithEvents ServicioFacturacionComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    Friend WithEvents IDLabel As System.Windows.Forms.Label
End Class
