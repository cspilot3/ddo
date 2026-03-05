Namespace Imaging.Beps.Rechazos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormRechazos_ValidacionesCampos
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
            Me.Button1 = New System.Windows.Forms.Button()
            Me.RechazarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.FechaProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.OTDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.lblOT)
            Me.GroupBox1.Controls.Add(Me.Button1)
            Me.GroupBox1.Controls.Add(Me.RechazarButton)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.FechaProcesoDateTimePicker)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(358, 155)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Filtros de Rechazo"
            '
            'OTDesktopComboBox
            '
            Me.OTDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.OTDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OTDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OTDesktopComboBox.DisabledEnter = False
            Me.OTDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OTDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OTDesktopComboBox.FormattingEnabled = True
            Me.OTDesktopComboBox.Location = New System.Drawing.Point(127, 59)
            Me.OTDesktopComboBox.Name = "OTDesktopComboBox"
            Me.OTDesktopComboBox.Size = New System.Drawing.Size(200, 21)
            Me.OTDesktopComboBox.TabIndex = 2
            '
            'lblOT
            '
            Me.lblOT.AutoSize = True
            Me.lblOT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblOT.Location = New System.Drawing.Point(19, 59)
            Me.lblOT.Name = "lblOT"
            Me.lblOT.Size = New System.Drawing.Size(24, 13)
            Me.lblOT.TabIndex = 27
            Me.lblOT.Text = "OT"
            '
            'Button1
            '
            Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Button1.Image = Global.Colpensiones.Plugin.My.Resources.Resources.btnSalir
            Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Button1.Location = New System.Drawing.Point(218, 109)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(91, 33)
            Me.Button1.TabIndex = 4
            Me.Button1.Text = "Cerrar"
            Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.Button1.UseVisualStyleBackColor = True
            '
            'RechazarButton
            '
            Me.RechazarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RechazarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.RechazarButton.Image = Global.Colpensiones.Plugin.My.Resources.Resources.Process_Accept
            Me.RechazarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.RechazarButton.Location = New System.Drawing.Point(47, 109)
            Me.RechazarButton.Name = "RechazarButton"
            Me.RechazarButton.Size = New System.Drawing.Size(102, 33)
            Me.RechazarButton.TabIndex = 3
            Me.RechazarButton.Text = "Rechazar"
            Me.RechazarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.RechazarButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(16, 29)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(96, 13)
            Me.Label1.TabIndex = 9
            Me.Label1.Text = "Fecha Proceso:"
            '
            'FechaProcesoDateTimePicker
            '
            Me.FechaProcesoDateTimePicker.Location = New System.Drawing.Point(127, 23)
            Me.FechaProcesoDateTimePicker.Name = "FechaProcesoDateTimePicker"
            Me.FechaProcesoDateTimePicker.Size = New System.Drawing.Size(200, 20)
            Me.FechaProcesoDateTimePicker.TabIndex = 1
            '
            'FormRechazos_ValidacionesCampos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(383, 184)
            Me.ControlBox = False
            Me.Controls.Add(Me.GroupBox1)
            Me.Name = "FormRechazos_ValidacionesCampos"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Rechazos Validaciones - Campos"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents FechaProcesoDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Friend WithEvents RechazarButton As System.Windows.Forms.Button
        Friend WithEvents OTDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents lblOT As System.Windows.Forms.Label
    End Class
End Namespace
