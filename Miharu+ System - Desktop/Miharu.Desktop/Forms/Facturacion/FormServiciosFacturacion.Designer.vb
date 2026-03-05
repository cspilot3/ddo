Namespace Forms.Facturacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormServiciosFacturacion
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango3 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango4 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormServiciosFacturacion))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.id_ServicioDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.fk_Servicio_Padre_DesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Nombre_ServicioDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Descripcion_ServicioDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Codigo_ServicioDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.ServicioConsultaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label5)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.id_ServicioDesktopTextBox)
            Me.GroupBox1.Controls.Add(Me.fk_Servicio_Padre_DesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.Nombre_ServicioDesktopTextBox)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.Descripcion_ServicioDesktopTextBox)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.Codigo_ServicioDesktopTextBox)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 122)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(457, 218)
            Me.GroupBox1.TabIndex = 9
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Servicio"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(20, 55)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(67, 13)
            Me.Label5.TabIndex = 42
            Me.Label5.Text = "Id Servicio"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(20, 28)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(88, 13)
            Me.Label1.TabIndex = 37
            Me.Label1.Text = "Servicio Padre"
            '
            'id_ServicioDesktopTextBox
            '
            Me.id_ServicioDesktopTextBox.BackColor = System.Drawing.Color.Silver
            Me.id_ServicioDesktopTextBox.DisabledEnter = False
            Me.id_ServicioDesktopTextBox.DisabledTab = False
            Me.id_ServicioDesktopTextBox.Enabled = False
            Me.id_ServicioDesktopTextBox.FocusIn = System.Drawing.Color.Silver
            Me.id_ServicioDesktopTextBox.FocusOut = System.Drawing.Color.Silver
            Me.id_ServicioDesktopTextBox.Location = New System.Drawing.Point(152, 52)
            Me.id_ServicioDesktopTextBox.Name = "id_ServicioDesktopTextBox"
            Rango1.MaxValue = 2147483647
            Rango1.MinValue = 0
            Me.id_ServicioDesktopTextBox.Rango = Rango1
            Me.id_ServicioDesktopTextBox.Size = New System.Drawing.Size(276, 21)
            Me.id_ServicioDesktopTextBox.TabIndex = 11
            Me.id_ServicioDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            '
            'fk_Servicio_Padre_DesktopComboBox
            '
            Me.fk_Servicio_Padre_DesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.fk_Servicio_Padre_DesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.fk_Servicio_Padre_DesktopComboBox.BackColor = System.Drawing.Color.Silver
            Me.fk_Servicio_Padre_DesktopComboBox.DisabledEnter = False
            Me.fk_Servicio_Padre_DesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.fk_Servicio_Padre_DesktopComboBox.Enabled = False
            Me.fk_Servicio_Padre_DesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.fk_Servicio_Padre_DesktopComboBox.FormattingEnabled = True
            Me.fk_Servicio_Padre_DesktopComboBox.ItemHeight = 13
            Me.fk_Servicio_Padre_DesktopComboBox.Location = New System.Drawing.Point(152, 25)
            Me.fk_Servicio_Padre_DesktopComboBox.Name = "fk_Servicio_Padre_DesktopComboBox"
            Me.fk_Servicio_Padre_DesktopComboBox.Size = New System.Drawing.Size(276, 21)
            Me.fk_Servicio_Padre_DesktopComboBox.TabIndex = 1
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(20, 177)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(92, 13)
            Me.Label4.TabIndex = 40
            Me.Label4.Text = "Codigo servicio"
            '
            'Nombre_ServicioDesktopTextBox
            '
            Me.Nombre_ServicioDesktopTextBox.BackColor = System.Drawing.Color.Silver
            Me.Nombre_ServicioDesktopTextBox.DisabledEnter = False
            Me.Nombre_ServicioDesktopTextBox.DisabledTab = False
            Me.Nombre_ServicioDesktopTextBox.Enabled = False
            Me.Nombre_ServicioDesktopTextBox.FocusIn = System.Drawing.Color.Silver
            Me.Nombre_ServicioDesktopTextBox.FocusOut = System.Drawing.Color.Silver
            Me.Nombre_ServicioDesktopTextBox.Location = New System.Drawing.Point(152, 79)
            Me.Nombre_ServicioDesktopTextBox.Name = "Nombre_ServicioDesktopTextBox"
            Rango2.MaxValue = 2147483647
            Rango2.MinValue = 0
            Me.Nombre_ServicioDesktopTextBox.Rango = Rango2
            Me.Nombre_ServicioDesktopTextBox.Size = New System.Drawing.Size(276, 21)
            Me.Nombre_ServicioDesktopTextBox.TabIndex = 12
            Me.Nombre_ServicioDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(20, 109)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(119, 13)
            Me.Label3.TabIndex = 39
            Me.Label3.Text = "Descripcion servicio"
            '
            'Descripcion_ServicioDesktopTextBox
            '
            Me.Descripcion_ServicioDesktopTextBox.DisabledEnter = False
            Me.Descripcion_ServicioDesktopTextBox.DisabledTab = False
            Me.Descripcion_ServicioDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Descripcion_ServicioDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.Descripcion_ServicioDesktopTextBox.Location = New System.Drawing.Point(152, 106)
            Me.Descripcion_ServicioDesktopTextBox.Multiline = True
            Me.Descripcion_ServicioDesktopTextBox.Name = "Descripcion_ServicioDesktopTextBox"
            Rango3.MaxValue = 2147483647
            Rango3.MinValue = 0
            Me.Descripcion_ServicioDesktopTextBox.Rango = Rango3
            Me.Descripcion_ServicioDesktopTextBox.Size = New System.Drawing.Size(276, 62)
            Me.Descripcion_ServicioDesktopTextBox.TabIndex = 13
            Me.Descripcion_ServicioDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(20, 82)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(98, 13)
            Me.Label2.TabIndex = 38
            Me.Label2.Text = "Nombre servicio"
            '
            'Codigo_ServicioDesktopTextBox
            '
            Me.Codigo_ServicioDesktopTextBox.DisabledEnter = False
            Me.Codigo_ServicioDesktopTextBox.DisabledTab = False
            Me.Codigo_ServicioDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Codigo_ServicioDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.Codigo_ServicioDesktopTextBox.Location = New System.Drawing.Point(152, 174)
            Me.Codigo_ServicioDesktopTextBox.Name = "Codigo_ServicioDesktopTextBox"
            Rango4.MaxValue = 2147483647
            Rango4.MinValue = 0
            Me.Codigo_ServicioDesktopTextBox.Rango = Rango4
            Me.Codigo_ServicioDesktopTextBox.Size = New System.Drawing.Size(100, 21)
            Me.Codigo_ServicioDesktopTextBox.TabIndex = 14
            Me.Codigo_ServicioDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(274, 350)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(100, 30)
            Me.GuardarButton.TabIndex = 15
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(380, 350)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(89, 30)
            Me.CerrarButton.TabIndex = 16
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(19, 26)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(118, 13)
            Me.Label6.TabIndex = 44
            Me.Label6.Text = "Servicio a consultar"
            '
            'ServicioConsultaDesktopComboBox
            '
            Me.ServicioConsultaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ServicioConsultaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ServicioConsultaDesktopComboBox.DisabledEnter = False
            Me.ServicioConsultaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ServicioConsultaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ServicioConsultaDesktopComboBox.FormattingEnabled = True
            Me.ServicioConsultaDesktopComboBox.Location = New System.Drawing.Point(151, 23)
            Me.ServicioConsultaDesktopComboBox.Name = "ServicioConsultaDesktopComboBox"
            Me.ServicioConsultaDesktopComboBox.Size = New System.Drawing.Size(276, 21)
            Me.ServicioConsultaDesktopComboBox.TabIndex = 0
            '
            'GroupBox2
            '
            Me.GroupBox2.Controls.Add(Me.Label6)
            Me.GroupBox2.Controls.Add(Me.ServicioConsultaDesktopComboBox)
            Me.GroupBox2.Location = New System.Drawing.Point(13, 53)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(456, 59)
            Me.GroupBox2.TabIndex = 0
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Administrar"
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label7.ForeColor = System.Drawing.Color.Green
            Me.Label7.Location = New System.Drawing.Point(12, 20)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(283, 16)
            Me.Label7.TabIndex = 48
            Me.Label7.Text = "Administracion de servicios de facturación"
            '
            'FormServiciosFacturacion
            '
            Me.AcceptButton = Me.GuardarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(484, 392)
            Me.Controls.Add(Me.Label7)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.GroupBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormServiciosFacturacion"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Servicios de Facturación"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents fk_Servicio_Padre_DesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Nombre_ServicioDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Descripcion_ServicioDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Codigo_ServicioDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents id_ServicioDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents ServicioConsultaDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
    End Class
End Namespace