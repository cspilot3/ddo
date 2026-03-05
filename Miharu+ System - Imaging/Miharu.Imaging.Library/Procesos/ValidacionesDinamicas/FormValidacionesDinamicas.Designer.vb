Namespace Procesos.ValidacionesDinamicas
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormValidacionesDinamicas
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
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.OTDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblOT = New System.Windows.Forms.Label()
            Me.FechaProcesodateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaProcesolabel = New System.Windows.Forms.Label()
            Me.BtnCancelar = New System.Windows.Forms.Button()
            Me.BtnAceptar = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.OTDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.lblOT)
            Me.GroupBox1.Controls.Add(Me.FechaProcesodateTimePicker)
            Me.GroupBox1.Controls.Add(Me.FechaProcesolabel)
            Me.GroupBox1.Controls.Add(Me.BtnCancelar)
            Me.GroupBox1.Controls.Add(Me.BtnAceptar)
            Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GroupBox1.Location = New System.Drawing.Point(13, 13)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(289, 160)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Filtros"
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
            Me.OTDesktopComboBox.Location = New System.Drawing.Point(125, 55)
            Me.OTDesktopComboBox.Name = "OTDesktopComboBox"
            Me.OTDesktopComboBox.Size = New System.Drawing.Size(127, 21)
            Me.OTDesktopComboBox.TabIndex = 33
            '
            'lblOT
            '
            Me.lblOT.AutoSize = True
            Me.lblOT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblOT.Location = New System.Drawing.Point(6, 63)
            Me.lblOT.Name = "lblOT"
            Me.lblOT.Size = New System.Drawing.Size(24, 13)
            Me.lblOT.TabIndex = 34
            Me.lblOT.Text = "OT"
            '
            'FechaProcesodateTimePicker
            '
            Me.FechaProcesodateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesodateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaProcesodateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaProcesodateTimePicker.Location = New System.Drawing.Point(125, 29)
            Me.FechaProcesodateTimePicker.Name = "FechaProcesodateTimePicker"
            Me.FechaProcesodateTimePicker.Size = New System.Drawing.Size(127, 20)
            Me.FechaProcesodateTimePicker.TabIndex = 31
            '
            'FechaProcesolabel
            '
            Me.FechaProcesolabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesolabel.AutoSize = True
            Me.FechaProcesolabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesolabel.Location = New System.Drawing.Point(6, 29)
            Me.FechaProcesolabel.Name = "FechaProcesolabel"
            Me.FechaProcesolabel.Size = New System.Drawing.Size(96, 13)
            Me.FechaProcesolabel.TabIndex = 32
            Me.FechaProcesolabel.Text = "Fecha Proceso:"
            '
            'BtnCancelar
            '
            Me.BtnCancelar.Location = New System.Drawing.Point(177, 112)
            Me.BtnCancelar.Name = "BtnCancelar"
            Me.BtnCancelar.Size = New System.Drawing.Size(75, 23)
            Me.BtnCancelar.TabIndex = 5
            Me.BtnCancelar.Text = "&Cancelar"
            Me.BtnCancelar.UseVisualStyleBackColor = True
            '
            'BtnAceptar
            '
            Me.BtnAceptar.Location = New System.Drawing.Point(86, 112)
            Me.BtnAceptar.Name = "BtnAceptar"
            Me.BtnAceptar.Size = New System.Drawing.Size(75, 23)
            Me.BtnAceptar.TabIndex = 4
            Me.BtnAceptar.Text = "&Aceptar"
            Me.BtnAceptar.UseVisualStyleBackColor = True
            '
            'FormValidacionesDinamicas
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(313, 189)
            Me.ControlBox = False
            Me.Controls.Add(Me.GroupBox1)
            Me.Name = "FormValidacionesDinamicas"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Validaciones Dinamicas"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents BtnCancelar As System.Windows.Forms.Button
        Friend WithEvents BtnAceptar As System.Windows.Forms.Button
        Friend WithEvents OTDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents lblOT As System.Windows.Forms.Label
        Private WithEvents FechaProcesodateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaProcesolabel As System.Windows.Forms.Label
    End Class
End Namespace