Namespace Procesos.Cruce

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCruce
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
            Me.CrucePrecapturaCheckBox = New System.Windows.Forms.CheckBox()
            Me.OTDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblOT = New System.Windows.Forms.Label()
            Me.PrepararDataButton = New System.Windows.Forms.Button()
            Me.CruzarButton = New System.Windows.Forms.Button()
            Me.FechaProcesodateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaProcesolabel = New System.Windows.Forms.Label()
            Me.FiltroGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'FiltroGroupBox
            '
            Me.FiltroGroupBox.Controls.Add(Me.CrucePrecapturaCheckBox)
            Me.FiltroGroupBox.Controls.Add(Me.OTDesktopComboBox)
            Me.FiltroGroupBox.Controls.Add(Me.lblOT)
            Me.FiltroGroupBox.Controls.Add(Me.PrepararDataButton)
            Me.FiltroGroupBox.Controls.Add(Me.CruzarButton)
            Me.FiltroGroupBox.Controls.Add(Me.FechaProcesodateTimePicker)
            Me.FiltroGroupBox.Controls.Add(Me.FechaProcesolabel)
            Me.FiltroGroupBox.Location = New System.Drawing.Point(12, 12)
            Me.FiltroGroupBox.Name = "FiltroGroupBox"
            Me.FiltroGroupBox.Size = New System.Drawing.Size(325, 197)
            Me.FiltroGroupBox.TabIndex = 1
            Me.FiltroGroupBox.TabStop = False
            Me.FiltroGroupBox.Text = "Filtros"
            '
            'CrucePrecapturaCheckBox
            '
            Me.CrucePrecapturaCheckBox.AutoSize = True
            Me.CrucePrecapturaCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CrucePrecapturaCheckBox.Location = New System.Drawing.Point(38, 97)
            Me.CrucePrecapturaCheckBox.Name = "CrucePrecapturaCheckBox"
            Me.CrucePrecapturaCheckBox.Size = New System.Drawing.Size(124, 17)
            Me.CrucePrecapturaCheckBox.TabIndex = 32
            Me.CrucePrecapturaCheckBox.Text = "Cruce precaptura"
            Me.CrucePrecapturaCheckBox.UseVisualStyleBackColor = True
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
            'PrepararDataButton
            '
            Me.PrepararDataButton.AccessibleDescription = ""
            Me.PrepararDataButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.PrepararDataButton.BackColor = System.Drawing.SystemColors.Control
            Me.PrepararDataButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.PrepararDataButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PrepararDataButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.prepare
            Me.PrepararDataButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.PrepararDataButton.Location = New System.Drawing.Point(38, 124)
            Me.PrepararDataButton.Name = "PrepararDataButton"
            Me.PrepararDataButton.Size = New System.Drawing.Size(98, 67)
            Me.PrepararDataButton.TabIndex = 2
            Me.PrepararDataButton.Tag = "Ctrl + P"
            Me.PrepararDataButton.Text = "&Preparar Data"
            Me.PrepararDataButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.PrepararDataButton.UseVisualStyleBackColor = False
            '
            'CruzarButton
            '
            Me.CruzarButton.AccessibleDescription = ""
            Me.CruzarButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.CruzarButton.BackColor = System.Drawing.SystemColors.Control
            Me.CruzarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CruzarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CruzarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cross
            Me.CruzarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CruzarButton.Location = New System.Drawing.Point(183, 124)
            Me.CruzarButton.Name = "CruzarButton"
            Me.CruzarButton.Size = New System.Drawing.Size(98, 67)
            Me.CruzarButton.TabIndex = 3
            Me.CruzarButton.Tag = "Ctrl + C"
            Me.CruzarButton.Text = "&Cruzar"
            Me.CruzarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CruzarButton.UseVisualStyleBackColor = False
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
            'FormCruce
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(349, 226)
            Me.Controls.Add(Me.FiltroGroupBox)
            Me.Name = "FormCruce"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Proceso Cierre"
            Me.FiltroGroupBox.ResumeLayout(False)
            Me.FiltroGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents FiltroGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CruzarButton As System.Windows.Forms.Button
        Private WithEvents FechaProcesodateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaProcesolabel As System.Windows.Forms.Label
        Friend WithEvents PrepararDataButton As System.Windows.Forms.Button
        Friend WithEvents OTDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents lblOT As System.Windows.Forms.Label
        Friend WithEvents CrucePrecapturaCheckBox As System.Windows.Forms.CheckBox
    End Class
End Namespace