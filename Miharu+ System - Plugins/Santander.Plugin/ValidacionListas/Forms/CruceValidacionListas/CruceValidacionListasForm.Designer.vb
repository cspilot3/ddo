Namespace ValidacionListas.Forms.CruceValidacionListas
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class CruceValidacionListasForm
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CruceValidacionListasForm))
            Me.CruceGroupBox = New System.Windows.Forms.GroupBox()
            Me.CargueLabel = New System.Windows.Forms.Label()
            Me.OTLabel = New System.Windows.Forms.Label()
            Me.FechaProcesoLabel = New System.Windows.Forms.Label()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.FechaProcesoPicker = New System.Windows.Forms.DateTimePicker()
            Me.OTDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.CargueDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.CruceGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'CruceGroupBox
            '
            Me.CruceGroupBox.Controls.Add(Me.CargueDesktopComboBox)
            Me.CruceGroupBox.Controls.Add(Me.OTDesktopComboBox)
            Me.CruceGroupBox.Controls.Add(Me.CargueLabel)
            Me.CruceGroupBox.Controls.Add(Me.OTLabel)
            Me.CruceGroupBox.Controls.Add(Me.FechaProcesoLabel)
            Me.CruceGroupBox.Controls.Add(Me.CancelarButton)
            Me.CruceGroupBox.Controls.Add(Me.AceptarButton)
            Me.CruceGroupBox.Controls.Add(Me.FechaProcesoPicker)
            Me.CruceGroupBox.Location = New System.Drawing.Point(12, 6)
            Me.CruceGroupBox.Name = "CruceGroupBox"
            Me.CruceGroupBox.Size = New System.Drawing.Size(394, 162)
            Me.CruceGroupBox.TabIndex = 0
            Me.CruceGroupBox.TabStop = False
            '
            'CargueLabel
            '
            Me.CargueLabel.AutoSize = True
            Me.CargueLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CargueLabel.Location = New System.Drawing.Point(11, 85)
            Me.CargueLabel.Name = "CargueLabel"
            Me.CargueLabel.Size = New System.Drawing.Size(51, 13)
            Me.CargueLabel.TabIndex = 51
            Me.CargueLabel.Text = "Cargue:"
            '
            'OTLabel
            '
            Me.OTLabel.AutoSize = True
            Me.OTLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OTLabel.Location = New System.Drawing.Point(11, 50)
            Me.OTLabel.Name = "OTLabel"
            Me.OTLabel.Size = New System.Drawing.Size(28, 13)
            Me.OTLabel.TabIndex = 49
            Me.OTLabel.Text = "OT:"
            '
            'FechaProcesoLabel
            '
            Me.FechaProcesoLabel.AutoSize = True
            Me.FechaProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoLabel.Location = New System.Drawing.Point(11, 16)
            Me.FechaProcesoLabel.Name = "FechaProcesoLabel"
            Me.FechaProcesoLabel.Size = New System.Drawing.Size(114, 13)
            Me.FechaProcesoLabel.TabIndex = 48
            Me.FechaProcesoLabel.Text = "Fecha de Proceso:"
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(301, 124)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 23)
            Me.CancelarButton.TabIndex = 5
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'AceptarButton
            '
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(203, 124)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 23)
            Me.AceptarButton.TabIndex = 4
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'FechaProcesoPicker
            '
            Me.FechaProcesoPicker.Location = New System.Drawing.Point(131, 16)
            Me.FechaProcesoPicker.Name = "FechaProcesoPicker"
            Me.FechaProcesoPicker.Size = New System.Drawing.Size(252, 20)
            Me.FechaProcesoPicker.TabIndex = 1
            '
            'OTDesktopComboBox
            '
            Me.OTDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OTDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OTDesktopComboBox.DisabledEnter = False
            Me.OTDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OTDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OTDesktopComboBox.FormattingEnabled = True
            Me.OTDesktopComboBox.Location = New System.Drawing.Point(131, 50)
            Me.OTDesktopComboBox.Name = "OTDesktopComboBox"
            Me.OTDesktopComboBox.Size = New System.Drawing.Size(121, 21)
            Me.OTDesktopComboBox.TabIndex = 52
            '
            'CargueDesktopComboBox
            '
            Me.CargueDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.CargueDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.CargueDesktopComboBox.DisabledEnter = False
            Me.CargueDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CargueDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CargueDesktopComboBox.FormattingEnabled = True
            Me.CargueDesktopComboBox.Location = New System.Drawing.Point(131, 85)
            Me.CargueDesktopComboBox.Name = "CargueDesktopComboBox"
            Me.CargueDesktopComboBox.Size = New System.Drawing.Size(121, 21)
            Me.CargueDesktopComboBox.TabIndex = 53
            '
            'CruceValidacionListasForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(418, 180)
            Me.Controls.Add(Me.CruceGroupBox)
            Me.Name = "CruceValidacionListasForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Generacion Data - Validacion Listas"
            Me.CruceGroupBox.ResumeLayout(False)
            Me.CruceGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CruceGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CargueLabel As System.Windows.Forms.Label
        Friend WithEvents OTLabel As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents FechaProcesoPicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents CargueDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents OTDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    End Class
End Namespace
