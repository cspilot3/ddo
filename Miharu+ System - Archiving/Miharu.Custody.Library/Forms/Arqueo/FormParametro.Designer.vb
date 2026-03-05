Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Arqueo
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParametro
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
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.SedeComboBox = New DesktopComboBoxControl()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.ProfundidadComboBox = New DesktopComboBoxControl()
            Me.ColumnaComboBox = New DesktopComboBoxControl()
            Me.FilaComboBox = New DesktopComboBoxControl()
            Me.EstanteComboBox = New DesktopComboBoxControl()
            Me.SeccionComboBox = New DesktopComboBoxControl()
            Me.BovedaComboBox = New DesktopComboBoxControl()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.ProyectoComboBox = New DesktopComboBoxControl()
            Me.EntidadComboBox = New DesktopComboBoxControl()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.OKButton = New System.Windows.Forms.Button()
            Me.CancelButton = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox1.Controls.Add(Me.SedeComboBox)
            Me.GroupBox1.Controls.Add(Me.Label10)
            Me.GroupBox1.Controls.Add(Me.ProfundidadComboBox)
            Me.GroupBox1.Controls.Add(Me.ColumnaComboBox)
            Me.GroupBox1.Controls.Add(Me.FilaComboBox)
            Me.GroupBox1.Controls.Add(Me.EstanteComboBox)
            Me.GroupBox1.Controls.Add(Me.SeccionComboBox)
            Me.GroupBox1.Controls.Add(Me.BovedaComboBox)
            Me.GroupBox1.Controls.Add(Me.Label7)
            Me.GroupBox1.Controls.Add(Me.Label8)
            Me.GroupBox1.Controls.Add(Me.Label9)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Location = New System.Drawing.Point(5, 11)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(521, 239)
            Me.GroupBox1.TabIndex = 3
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Ubicación"
            '
            'SedeComboBox
            '
            Me.SedeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SedeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SedeComboBox.DisabledEnter = False
            Me.SedeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SedeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SedeComboBox.FormattingEnabled = True
            Me.SedeComboBox.Location = New System.Drawing.Point(19, 42)
            Me.SedeComboBox.Name = "SedeComboBox"
            Me.SedeComboBox.Size = New System.Drawing.Size(231, 21)
            Me.SedeComboBox.TabIndex = 19
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Location = New System.Drawing.Point(15, 23)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(32, 13)
            Me.Label10.TabIndex = 18
            Me.Label10.Text = "Sede"
            '
            'ProfundidadComboBox
            '
            Me.ProfundidadComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProfundidadComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProfundidadComboBox.DisabledEnter = False
            Me.ProfundidadComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProfundidadComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProfundidadComboBox.FormattingEnabled = True
            Me.ProfundidadComboBox.Location = New System.Drawing.Point(273, 149)
            Me.ProfundidadComboBox.Name = "ProfundidadComboBox"
            Me.ProfundidadComboBox.Size = New System.Drawing.Size(231, 21)
            Me.ProfundidadComboBox.TabIndex = 17
            '
            'ColumnaComboBox
            '
            Me.ColumnaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ColumnaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ColumnaComboBox.DisabledEnter = False
            Me.ColumnaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ColumnaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ColumnaComboBox.FormattingEnabled = True
            Me.ColumnaComboBox.Location = New System.Drawing.Point(273, 94)
            Me.ColumnaComboBox.Name = "ColumnaComboBox"
            Me.ColumnaComboBox.Size = New System.Drawing.Size(231, 21)
            Me.ColumnaComboBox.TabIndex = 16
            '
            'FilaComboBox
            '
            Me.FilaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.FilaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.FilaComboBox.DisabledEnter = False
            Me.FilaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.FilaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.FilaComboBox.FormattingEnabled = True
            Me.FilaComboBox.Location = New System.Drawing.Point(275, 39)
            Me.FilaComboBox.Name = "FilaComboBox"
            Me.FilaComboBox.Size = New System.Drawing.Size(231, 21)
            Me.FilaComboBox.TabIndex = 15
            '
            'EstanteComboBox
            '
            Me.EstanteComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EstanteComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EstanteComboBox.DisabledEnter = False
            Me.EstanteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EstanteComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EstanteComboBox.FormattingEnabled = True
            Me.EstanteComboBox.Location = New System.Drawing.Point(19, 203)
            Me.EstanteComboBox.Name = "EstanteComboBox"
            Me.EstanteComboBox.Size = New System.Drawing.Size(231, 21)
            Me.EstanteComboBox.TabIndex = 14
            '
            'SeccionComboBox
            '
            Me.SeccionComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SeccionComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SeccionComboBox.DisabledEnter = False
            Me.SeccionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SeccionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SeccionComboBox.FormattingEnabled = True
            Me.SeccionComboBox.Location = New System.Drawing.Point(19, 148)
            Me.SeccionComboBox.Name = "SeccionComboBox"
            Me.SeccionComboBox.Size = New System.Drawing.Size(231, 21)
            Me.SeccionComboBox.TabIndex = 13
            '
            'BovedaComboBox
            '
            Me.BovedaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.BovedaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.BovedaComboBox.DisabledEnter = False
            Me.BovedaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.BovedaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.BovedaComboBox.FormattingEnabled = True
            Me.BovedaComboBox.Location = New System.Drawing.Point(19, 93)
            Me.BovedaComboBox.Name = "BovedaComboBox"
            Me.BovedaComboBox.Size = New System.Drawing.Size(231, 21)
            Me.BovedaComboBox.TabIndex = 12
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(280, 133)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(64, 13)
            Me.Label7.TabIndex = 11
            Me.Label7.Text = "Profundidad"
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Location = New System.Drawing.Point(280, 78)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(48, 13)
            Me.Label8.TabIndex = 10
            Me.Label8.Text = "Columna"
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Location = New System.Drawing.Point(280, 23)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(23, 13)
            Me.Label9.TabIndex = 9
            Me.Label9.Text = "Fila"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(16, 184)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(43, 13)
            Me.Label4.TabIndex = 5
            Me.Label4.Text = "Estante"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(16, 129)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(46, 13)
            Me.Label3.TabIndex = 4
            Me.Label3.Text = "Seccion"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(16, 74)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(44, 13)
            Me.Label1.TabIndex = 3
            Me.Label1.Text = "Boveda"
            '
            'GroupBox2
            '
            Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox2.Controls.Add(Me.ProyectoComboBox)
            Me.GroupBox2.Controls.Add(Me.EntidadComboBox)
            Me.GroupBox2.Controls.Add(Me.Label5)
            Me.GroupBox2.Controls.Add(Me.Label6)
            Me.GroupBox2.Location = New System.Drawing.Point(5, 258)
            Me.GroupBox2.Margin = New System.Windows.Forms.Padding(18)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Padding = New System.Windows.Forms.Padding(10)
            Me.GroupBox2.Size = New System.Drawing.Size(521, 79)
            Me.GroupBox2.TabIndex = 4
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Data"
            '
            'ProyectoComboBox
            '
            Me.ProyectoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProyectoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProyectoComboBox.DisabledEnter = False
            Me.ProyectoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProyectoComboBox.FormattingEnabled = True
            Me.ProyectoComboBox.Location = New System.Drawing.Point(277, 46)
            Me.ProyectoComboBox.Name = "ProyectoComboBox"
            Me.ProyectoComboBox.Size = New System.Drawing.Size(231, 21)
            Me.ProyectoComboBox.TabIndex = 12
            '
            'EntidadComboBox
            '
            Me.EntidadComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadComboBox.DisabledEnter = False
            Me.EntidadComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadComboBox.FormattingEnabled = True
            Me.EntidadComboBox.Location = New System.Drawing.Point(19, 43)
            Me.EntidadComboBox.Name = "EntidadComboBox"
            Me.EntidadComboBox.Size = New System.Drawing.Size(231, 21)
            Me.EntidadComboBox.TabIndex = 9
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(280, 30)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(49, 13)
            Me.Label5.TabIndex = 7
            Me.Label5.Text = "Proyecto"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(19, 27)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(43, 13)
            Me.Label6.TabIndex = 6
            Me.Label6.Text = "Entidad"
            '
            'OKButton
            '
            Me.OKButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.okArqueo
            Me.OKButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.OKButton.Location = New System.Drawing.Point(288, 349)
            Me.OKButton.Margin = New System.Windows.Forms.Padding(10)
            Me.OKButton.Name = "OKButton"
            Me.OKButton.Size = New System.Drawing.Size(110, 50)
            Me.OKButton.TabIndex = 5
            Me.OKButton.Text = "Aceptar"
            Me.OKButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.OKButton.UseVisualStyleBackColor = True
            '
            'CancelButton
            '
            Me.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.cancelArqueo
            Me.CancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelButton.Location = New System.Drawing.Point(398, 349)
            Me.CancelButton.Margin = New System.Windows.Forms.Padding(10)
            Me.CancelButton.Name = "CancelButton"
            Me.CancelButton.Size = New System.Drawing.Size(110, 50)
            Me.CancelButton.TabIndex = 6
            Me.CancelButton.Text = "Cancelar"
            Me.CancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelButton.UseVisualStyleBackColor = True
            '
            'FormParametro
            '
            Me.AcceptButton = Me.OKButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(539, 412)
            Me.Controls.Add(Me.OKButton)
            Me.Controls.Add(Me.CancelButton)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.GroupBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Name = "FormParametro"
            Me.Padding = New System.Windows.Forms.Padding(18)
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Nuevo parametro ..."
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents OKButton As System.Windows.Forms.Button
        Friend Shadows WithEvents CancelButton As System.Windows.Forms.Button
        Friend WithEvents EntidadComboBox As DesktopComboBoxControl
        Friend WithEvents ProyectoComboBox As DesktopComboBoxControl
        Friend WithEvents ProfundidadComboBox As DesktopComboBoxControl
        Friend WithEvents ColumnaComboBox As DesktopComboBoxControl
        Friend WithEvents FilaComboBox As DesktopComboBoxControl
        Friend WithEvents EstanteComboBox As DesktopComboBoxControl
        Friend WithEvents SeccionComboBox As DesktopComboBoxControl
        Friend WithEvents BovedaComboBox As DesktopComboBoxControl
        Friend WithEvents SedeComboBox As DesktopComboBoxControl
        Friend WithEvents Label10 As System.Windows.Forms.Label
    End Class
End Namespace