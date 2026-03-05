Namespace Imaging.Carpeta_Unica.Forms.Empaque


    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormActualizacionEmpaque
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
            Me.FiltroGroupBox = New System.Windows.Forms.GroupBox()
            Me.SalirButton = New System.Windows.Forms.Button()
            Me.ActualizarButton = New System.Windows.Forms.Button()
            Me.ProcesoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.ProcesoLabel = New System.Windows.Forms.Label()
            Me.FechaProcesodateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaProcesolabel = New System.Windows.Forms.Label()
            Me.FiltroGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'FiltroGroupBox
            '
            Me.FiltroGroupBox.Controls.Add(Me.SalirButton)
            Me.FiltroGroupBox.Controls.Add(Me.ActualizarButton)
            Me.FiltroGroupBox.Controls.Add(Me.ProcesoDesktopComboBox)
            Me.FiltroGroupBox.Controls.Add(Me.ProcesoLabel)
            Me.FiltroGroupBox.Controls.Add(Me.FechaProcesodateTimePicker)
            Me.FiltroGroupBox.Controls.Add(Me.FechaProcesolabel)
            Me.FiltroGroupBox.Location = New System.Drawing.Point(12, 12)
            Me.FiltroGroupBox.Name = "FiltroGroupBox"
            Me.FiltroGroupBox.Size = New System.Drawing.Size(432, 208)
            Me.FiltroGroupBox.TabIndex = 1
            Me.FiltroGroupBox.TabStop = False
            Me.FiltroGroupBox.Text = "Filtros"
            '
            'SalirButton
            '
            Me.SalirButton.AccessibleDescription = ""
            Me.SalirButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.SalirButton.BackColor = System.Drawing.SystemColors.Control
            Me.SalirButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.SalirButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.SalirButton.Image = Global.BCS.Plugin.My.Resources.Resources.btnSalir
            Me.SalirButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.SalirButton.Location = New System.Drawing.Point(270, 137)
            Me.SalirButton.Name = "SalirButton"
            Me.SalirButton.Size = New System.Drawing.Size(93, 47)
            Me.SalirButton.TabIndex = 35
            Me.SalirButton.Tag = "Ctrl + C"
            Me.SalirButton.Text = "Salir"
            Me.SalirButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.SalirButton.UseVisualStyleBackColor = False
            '
            'ActualizarButton
            '
            Me.ActualizarButton.AccessibleDescription = ""
            Me.ActualizarButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.ActualizarButton.BackColor = System.Drawing.SystemColors.Control
            Me.ActualizarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ActualizarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ActualizarButton.Image = Global.BCS.Plugin.My.Resources.Resources.Aceptar
            Me.ActualizarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.ActualizarButton.Location = New System.Drawing.Point(71, 137)
            Me.ActualizarButton.Name = "ActualizarButton"
            Me.ActualizarButton.Size = New System.Drawing.Size(93, 47)
            Me.ActualizarButton.TabIndex = 34
            Me.ActualizarButton.Tag = "Ctrl + C"
            Me.ActualizarButton.Text = "Actualizar"
            Me.ActualizarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.ActualizarButton.UseVisualStyleBackColor = False
            '
            'ProcesoDesktopComboBox
            '
            Me.ProcesoDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.ProcesoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProcesoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProcesoDesktopComboBox.DisabledEnter = False
            Me.ProcesoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProcesoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProcesoDesktopComboBox.FormattingEnabled = True
            Me.ProcesoDesktopComboBox.Location = New System.Drawing.Point(135, 65)
            Me.ProcesoDesktopComboBox.Name = "ProcesoDesktopComboBox"
            Me.ProcesoDesktopComboBox.Size = New System.Drawing.Size(271, 21)
            Me.ProcesoDesktopComboBox.TabIndex = 31
            '
            'ProcesoLabel
            '
            Me.ProcesoLabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.ProcesoLabel.AutoSize = True
            Me.ProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ProcesoLabel.Location = New System.Drawing.Point(31, 68)
            Me.ProcesoLabel.Name = "ProcesoLabel"
            Me.ProcesoLabel.Size = New System.Drawing.Size(57, 13)
            Me.ProcesoLabel.TabIndex = 30
            Me.ProcesoLabel.Text = "Proceso:"
            '
            'FechaProcesodateTimePicker
            '
            Me.FechaProcesodateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesodateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaProcesodateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaProcesodateTimePicker.Location = New System.Drawing.Point(135, 29)
            Me.FechaProcesodateTimePicker.Name = "FechaProcesodateTimePicker"
            Me.FechaProcesodateTimePicker.Size = New System.Drawing.Size(84, 20)
            Me.FechaProcesodateTimePicker.TabIndex = 29
            '
            'FechaProcesolabel
            '
            Me.FechaProcesolabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesolabel.AutoSize = True
            Me.FechaProcesolabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesolabel.Location = New System.Drawing.Point(31, 33)
            Me.FechaProcesolabel.Name = "FechaProcesolabel"
            Me.FechaProcesolabel.Size = New System.Drawing.Size(96, 13)
            Me.FechaProcesolabel.TabIndex = 28
            Me.FechaProcesolabel.Text = "Fecha Proceso:"
            '
            'FormActualizacionEmpaque
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(459, 234)
            Me.Controls.Add(Me.FiltroGroupBox)
            Me.Name = "FormActualizacionEmpaque"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Actualizacion Empaque"
            Me.FiltroGroupBox.ResumeLayout(False)
            Me.FiltroGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents FiltroGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ActualizarButton As System.Windows.Forms.Button
        Friend WithEvents ProcesoDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents ProcesoLabel As System.Windows.Forms.Label
        Private WithEvents FechaProcesodateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaProcesolabel As System.Windows.Forms.Label
        Friend WithEvents SalirButton As System.Windows.Forms.Button
    End Class
End Namespace