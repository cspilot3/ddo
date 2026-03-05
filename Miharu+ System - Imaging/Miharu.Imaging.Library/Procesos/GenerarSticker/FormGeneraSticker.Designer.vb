Namespace Procesos.GenerarSticker

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormGeneraSticker
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
            Me.OTDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblOT = New System.Windows.Forms.Label()
            Me.GenerarButton = New System.Windows.Forms.Button()
            Me.FechaProcesodateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaProcesolabel = New System.Windows.Forms.Label()
            Me.FiltroGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'FiltroGroupBox
            '
            Me.FiltroGroupBox.Controls.Add(Me.OTDesktopComboBox)
            Me.FiltroGroupBox.Controls.Add(Me.lblOT)
            Me.FiltroGroupBox.Controls.Add(Me.GenerarButton)
            Me.FiltroGroupBox.Controls.Add(Me.FechaProcesodateTimePicker)
            Me.FiltroGroupBox.Controls.Add(Me.FechaProcesolabel)
            Me.FiltroGroupBox.Location = New System.Drawing.Point(12, 12)
            Me.FiltroGroupBox.Name = "FiltroGroupBox"
            Me.FiltroGroupBox.Size = New System.Drawing.Size(325, 197)
            Me.FiltroGroupBox.TabIndex = 1
            Me.FiltroGroupBox.TabStop = False
            Me.FiltroGroupBox.Text = "Filtros"
            '
            'OTDesktopComboBox
            '
            Me.OTDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.OTDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OTDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OTDesktopComboBox.DisabledEnter = False
            Me.OTDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OTDesktopComboBox.fk_Campo = 0
            Me.OTDesktopComboBox.fk_Documento = 0
            Me.OTDesktopComboBox.fk_Validacion = 0
            Me.OTDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OTDesktopComboBox.FormattingEnabled = True
            Me.OTDesktopComboBox.Location = New System.Drawing.Point(154, 59)
            Me.OTDesktopComboBox.Name = "OTDesktopComboBox"
            Me.OTDesktopComboBox.Size = New System.Drawing.Size(127, 21)
            Me.OTDesktopComboBox.TabIndex = 29
            '
            'lblOT
            '
            Me.lblOT.AutoSize = True
            Me.lblOT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblOT.Location = New System.Drawing.Point(35, 67)
            Me.lblOT.Name = "lblOT"
            Me.lblOT.Size = New System.Drawing.Size(24, 13)
            Me.lblOT.TabIndex = 30
            Me.lblOT.Text = "OT"
            '
            'GenerarButton
            '
            Me.GenerarButton.AccessibleDescription = ""
            Me.GenerarButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.GenerarButton.BackColor = System.Drawing.SystemColors.Control
            Me.GenerarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.GenerarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GenerarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Process_Accept
            Me.GenerarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.GenerarButton.Location = New System.Drawing.Point(111, 124)
            Me.GenerarButton.Name = "GenerarButton"
            Me.GenerarButton.Size = New System.Drawing.Size(98, 67)
            Me.GenerarButton.TabIndex = 3
            Me.GenerarButton.Tag = "Ctrl + C"
            Me.GenerarButton.Text = "&Generar"
            Me.GenerarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.GenerarButton.UseVisualStyleBackColor = False
            '
            'FechaProcesodateTimePicker
            '
            Me.FechaProcesodateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesodateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaProcesodateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaProcesodateTimePicker.Location = New System.Drawing.Point(154, 33)
            Me.FechaProcesodateTimePicker.Name = "FechaProcesodateTimePicker"
            Me.FechaProcesodateTimePicker.Size = New System.Drawing.Size(127, 20)
            Me.FechaProcesodateTimePicker.TabIndex = 1
            '
            'FechaProcesolabel
            '
            Me.FechaProcesolabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesolabel.AutoSize = True
            Me.FechaProcesolabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesolabel.Location = New System.Drawing.Point(35, 33)
            Me.FechaProcesolabel.Name = "FechaProcesolabel"
            Me.FechaProcesolabel.Size = New System.Drawing.Size(96, 13)
            Me.FechaProcesolabel.TabIndex = 28
            Me.FechaProcesolabel.Text = "Fecha Proceso:"
            '
            'FormGeneraSticker
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(349, 226)
            Me.Controls.Add(Me.FiltroGroupBox)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormGeneraSticker"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Generar Estampado"
            Me.FiltroGroupBox.ResumeLayout(False)
            Me.FiltroGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents FiltroGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents GenerarButton As System.Windows.Forms.Button
        Private WithEvents FechaProcesodateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaProcesolabel As System.Windows.Forms.Label
        Friend WithEvents OTDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents lblOT As System.Windows.Forms.Label
    End Class
End Namespace