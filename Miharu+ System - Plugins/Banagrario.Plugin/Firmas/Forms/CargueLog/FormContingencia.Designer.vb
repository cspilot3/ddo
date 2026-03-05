Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Firmas.Forms.CargueLog
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormContingencia
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormContingencia))
            Me.ExcluirButton = New System.Windows.Forms.Button()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.FechaProcesoPicker = New System.Windows.Forms.DateTimePicker()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.OficinaDesktopComboBox = New DesktopComboBoxControl()
            Me.COBDesktopComboBox = New DesktopComboBoxControl()
            Me.COBLabel = New System.Windows.Forms.Label()
            Me.RegionalDesktopComboBox = New DesktopComboBoxControl()
            Me.RegionalLabel = New System.Windows.Forms.Label()
            Me.OficinaLabel = New System.Windows.Forms.Label()
            Me.TransaccionesDesktopComboBox = New DesktopComboBoxControl()
            Me.NumeroCuentaTextbox = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'ExcluirButton
            '
            Me.ExcluirButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ExcluirButton.Image = CType(resources.GetObject("ExcluirButton.Image"), System.Drawing.Image)
            Me.ExcluirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ExcluirButton.Location = New System.Drawing.Point(274, 240)
            Me.ExcluirButton.Name = "ExcluirButton"
            Me.ExcluirButton.Size = New System.Drawing.Size(80, 23)
            Me.ExcluirButton.TabIndex = 3
            Me.ExcluirButton.Text = "&Excluir"
            Me.ExcluirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(360, 240)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 23)
            Me.CancelarButton.TabIndex = 4
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'FechaProcesoPicker
            '
            Me.FechaProcesoPicker.Location = New System.Drawing.Point(143, 125)
            Me.FechaProcesoPicker.Name = "FechaProcesoPicker"
            Me.FechaProcesoPicker.Size = New System.Drawing.Size(283, 20)
            Me.FechaProcesoPicker.TabIndex = 35
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label10.Location = New System.Drawing.Point(12, 164)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(77, 13)
            Me.Label10.TabIndex = 37
            Me.Label10.Text = "Transacción"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(11, 131)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(128, 13)
            Me.Label1.TabIndex = 38
            Me.Label1.Text = "Fecha de Movimiento"
            '
            'OficinaDesktopComboBox
            '
            Me.OficinaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OficinaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OficinaDesktopComboBox.DisabledEnter = False
            Me.OficinaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OficinaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OficinaDesktopComboBox.FormattingEnabled = True
            Me.OficinaDesktopComboBox.Location = New System.Drawing.Point(143, 88)
            Me.OficinaDesktopComboBox.Name = "OficinaDesktopComboBox"
            Me.OficinaDesktopComboBox.Size = New System.Drawing.Size(283, 21)
            Me.OficinaDesktopComboBox.TabIndex = 46
            '
            'COBDesktopComboBox
            '
            Me.COBDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.COBDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.COBDesktopComboBox.DisabledEnter = False
            Me.COBDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.COBDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.COBDesktopComboBox.FormattingEnabled = True
            Me.COBDesktopComboBox.Location = New System.Drawing.Point(143, 51)
            Me.COBDesktopComboBox.Name = "COBDesktopComboBox"
            Me.COBDesktopComboBox.Size = New System.Drawing.Size(283, 21)
            Me.COBDesktopComboBox.TabIndex = 44
            '
            'COBLabel
            '
            Me.COBLabel.AutoSize = True
            Me.COBLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.COBLabel.Location = New System.Drawing.Point(12, 54)
            Me.COBLabel.Name = "COBLabel"
            Me.COBLabel.Size = New System.Drawing.Size(36, 13)
            Me.COBLabel.TabIndex = 43
            Me.COBLabel.Text = "COB:"
            '
            'RegionalDesktopComboBox
            '
            Me.RegionalDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.RegionalDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.RegionalDesktopComboBox.DisabledEnter = False
            Me.RegionalDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.RegionalDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.RegionalDesktopComboBox.FormattingEnabled = True
            Me.RegionalDesktopComboBox.Location = New System.Drawing.Point(143, 14)
            Me.RegionalDesktopComboBox.Name = "RegionalDesktopComboBox"
            Me.RegionalDesktopComboBox.Size = New System.Drawing.Size(283, 21)
            Me.RegionalDesktopComboBox.TabIndex = 42
            '
            'RegionalLabel
            '
            Me.RegionalLabel.AutoSize = True
            Me.RegionalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.RegionalLabel.Location = New System.Drawing.Point(11, 17)
            Me.RegionalLabel.Name = "RegionalLabel"
            Me.RegionalLabel.Size = New System.Drawing.Size(61, 13)
            Me.RegionalLabel.TabIndex = 41
            Me.RegionalLabel.Text = "Regional:"
            '
            'OficinaLabel
            '
            Me.OficinaLabel.AutoSize = True
            Me.OficinaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OficinaLabel.Location = New System.Drawing.Point(12, 91)
            Me.OficinaLabel.Name = "OficinaLabel"
            Me.OficinaLabel.Size = New System.Drawing.Size(51, 13)
            Me.OficinaLabel.TabIndex = 47
            Me.OficinaLabel.Text = "Oficina:"
            '
            'TransaccionesDesktopComboBox
            '
            Me.TransaccionesDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TransaccionesDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TransaccionesDesktopComboBox.DisabledEnter = False
            Me.TransaccionesDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TransaccionesDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TransaccionesDesktopComboBox.FormattingEnabled = True
            Me.TransaccionesDesktopComboBox.Location = New System.Drawing.Point(143, 161)
            Me.TransaccionesDesktopComboBox.Name = "TransaccionesDesktopComboBox"
            Me.TransaccionesDesktopComboBox.Size = New System.Drawing.Size(283, 21)
            Me.TransaccionesDesktopComboBox.TabIndex = 48
            '
            'NumeroCuentaTextbox
            '
            Me.NumeroCuentaTextbox.Location = New System.Drawing.Point(143, 198)
            Me.NumeroCuentaTextbox.MaxLength = 20
            Me.NumeroCuentaTextbox.Name = "NumeroCuentaTextbox"
            Me.NumeroCuentaTextbox.Size = New System.Drawing.Size(283, 20)
            Me.NumeroCuentaTextbox.TabIndex = 39
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(12, 201)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(112, 13)
            Me.Label2.TabIndex = 40
            Me.Label2.Text = "Numero de Cuenta"
            '
            'FormContingencia
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(451, 272)
            Me.Controls.Add(Me.TransaccionesDesktopComboBox)
            Me.Controls.Add(Me.OficinaLabel)
            Me.Controls.Add(Me.OficinaDesktopComboBox)
            Me.Controls.Add(Me.COBDesktopComboBox)
            Me.Controls.Add(Me.COBLabel)
            Me.Controls.Add(Me.RegionalDesktopComboBox)
            Me.Controls.Add(Me.RegionalLabel)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.NumeroCuentaTextbox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.Label10)
            Me.Controls.Add(Me.FechaProcesoPicker)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.ExcluirButton)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormContingencia"
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Exclusión de Tarjetas por Contingencia"
            Me.TopMost = True
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ExcluirButton As System.Windows.Forms.Button
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents FechaProcesoPicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents OficinaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents COBDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents COBLabel As System.Windows.Forms.Label
        Friend WithEvents RegionalDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents RegionalLabel As System.Windows.Forms.Label
        Friend WithEvents OficinaLabel As System.Windows.Forms.Label
        Friend WithEvents TransaccionesDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents NumeroCuentaTextbox As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
    End Class
End Namespace