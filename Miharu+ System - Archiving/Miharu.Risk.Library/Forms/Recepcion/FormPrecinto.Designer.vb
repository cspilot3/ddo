Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Library

Namespace Forms.Recepcion

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormPrecinto
        Inherits FormBase

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPrecinto))
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.PuntoOrigenCodigoComboBox = New System.Windows.Forms.ComboBox()
            Me.PuntoOrigenNombreComboBox = New System.Windows.Forms.ComboBox()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.PrecintoDesktopTextBox2 = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.PrecintoDesktopTextBox1 = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(232, 187)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(93, 32)
            Me.CancelarButton.TabIndex = 11
            Me.CancelarButton.Text = "&Cerrar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.PuntoOrigenCodigoComboBox)
            Me.GroupBox1.Controls.Add(Me.PuntoOrigenNombreComboBox)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.CancelarButton)
            Me.GroupBox1.Controls.Add(Me.AceptarButton)
            Me.GroupBox1.Controls.Add(Me.PrecintoDesktopTextBox2)
            Me.GroupBox1.Controls.Add(Me.PrecintoDesktopTextBox1)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(334, 231)
            Me.GroupBox1.TabIndex = 8
            Me.GroupBox1.TabStop = False
            '
            'PuntoOrigenCodigoComboBox
            '
            Me.PuntoOrigenCodigoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.PuntoOrigenCodigoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.PuntoOrigenCodigoComboBox.FormattingEnabled = True
            Me.PuntoOrigenCodigoComboBox.Location = New System.Drawing.Point(10, 151)
            Me.PuntoOrigenCodigoComboBox.Name = "PuntoOrigenCodigoComboBox"
            Me.PuntoOrigenCodigoComboBox.Size = New System.Drawing.Size(112, 21)
            Me.PuntoOrigenCodigoComboBox.TabIndex = 6
            '
            'PuntoOrigenNombreComboBox
            '
            Me.PuntoOrigenNombreComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.PuntoOrigenNombreComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.PuntoOrigenNombreComboBox.FormattingEnabled = True
            Me.PuntoOrigenNombreComboBox.Location = New System.Drawing.Point(133, 151)
            Me.PuntoOrigenNombreComboBox.Name = "PuntoOrigenNombreComboBox"
            Me.PuntoOrigenNombreComboBox.Size = New System.Drawing.Size(183, 21)
            Me.PuntoOrigenNombreComboBox.TabIndex = 8
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(6, 108)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(116, 19)
            Me.Label4.TabIndex = 4
            Me.Label4.Text = "Punto Origen"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(149, 135)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(51, 13)
            Me.Label3.TabIndex = 7
            Me.Label3.Text = "Nombre"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(16, 135)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(45, 13)
            Me.Label2.TabIndex = 5
            Me.Label2.Text = "Código"
            '
            'AceptarButton
            '
            Me.AceptarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(133, 187)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(93, 32)
            Me.AceptarButton.TabIndex = 10
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'PrecintoDesktopTextBox2
            '
            Me.PrecintoDesktopTextBox2._Obligatorio = False
            Me.PrecintoDesktopTextBox2._PermitePegar = False
            Me.PrecintoDesktopTextBox2.Cantidad_Decimales = CType(2, Short)
            Me.PrecintoDesktopTextBox2.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.PrecintoDesktopTextBox2.DateFormat = Nothing
            Me.PrecintoDesktopTextBox2.DisabledEnter = True
            Me.PrecintoDesktopTextBox2.DisabledTab = False
            Me.PrecintoDesktopTextBox2.EnabledShortCuts = False
            Me.PrecintoDesktopTextBox2.FocusIn = System.Drawing.Color.LightYellow
            Me.PrecintoDesktopTextBox2.FocusOut = System.Drawing.Color.White
            Me.PrecintoDesktopTextBox2.Location = New System.Drawing.Point(10, 72)
            Me.PrecintoDesktopTextBox2.MaskedTextBox_Property = ""
            Me.PrecintoDesktopTextBox2.MaximumLength = CType(0, Short)
            Me.PrecintoDesktopTextBox2.MinimumLength = CType(0, Short)
            Me.PrecintoDesktopTextBox2.Name = "PrecintoDesktopTextBox2"
            Me.PrecintoDesktopTextBox2.Obligatorio = False
            Me.PrecintoDesktopTextBox2.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.PrecintoDesktopTextBox2.Rango = Rango1
            Me.PrecintoDesktopTextBox2.Size = New System.Drawing.Size(306, 21)
            Me.PrecintoDesktopTextBox2.TabIndex = 3
            Me.PrecintoDesktopTextBox2.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.PrecintoDesktopTextBox2.Usa_Decimales = False
            Me.PrecintoDesktopTextBox2.Validos_Cantidad_Puntos = False
            '
            'PrecintoDesktopTextBox1
            '
            Me.PrecintoDesktopTextBox1._Obligatorio = False
            Me.PrecintoDesktopTextBox1._PermitePegar = False
            Me.PrecintoDesktopTextBox1.Cantidad_Decimales = CType(2, Short)
            Me.PrecintoDesktopTextBox1.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.PrecintoDesktopTextBox1.DateFormat = Nothing
            Me.PrecintoDesktopTextBox1.DisabledEnter = True
            Me.PrecintoDesktopTextBox1.DisabledTab = False
            Me.PrecintoDesktopTextBox1.EnabledShortCuts = False
            Me.PrecintoDesktopTextBox1.FocusIn = System.Drawing.Color.LightYellow
            Me.PrecintoDesktopTextBox1.FocusOut = System.Drawing.Color.White
            Me.PrecintoDesktopTextBox1.Location = New System.Drawing.Point(10, 46)
            Me.PrecintoDesktopTextBox1.MaskedTextBox_Property = ""
            Me.PrecintoDesktopTextBox1.MaximumLength = CType(0, Short)
            Me.PrecintoDesktopTextBox1.MinimumLength = CType(0, Short)
            Me.PrecintoDesktopTextBox1.Name = "PrecintoDesktopTextBox1"
            Me.PrecintoDesktopTextBox1.Obligatorio = False
            Me.PrecintoDesktopTextBox1.permitePegar = False
            Rango2.MaxValue = 2147483647.0R
            Rango2.MinValue = 0.0R
            Me.PrecintoDesktopTextBox1.Rango = Rango2
            Me.PrecintoDesktopTextBox1.Size = New System.Drawing.Size(306, 21)
            Me.PrecintoDesktopTextBox1.TabIndex = 2
            Me.PrecintoDesktopTextBox1.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.PrecintoDesktopTextBox1.Usa_Decimales = False
            Me.PrecintoDesktopTextBox1.Validos_Cantidad_Puntos = False
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(6, 12)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(77, 19)
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "Precinto"
            '
            'FormPrecinto
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(354, 251)
            Me.Controls.Add(Me.GroupBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormPrecinto"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Recepcionar Precinto"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents PrecintoDesktopTextBox1 As DesktopTextBoxControl
        Friend WithEvents PrecintoDesktopTextBox2 As DesktopTextBoxControl
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents PuntoOrigenCodigoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents PuntoOrigenNombreComboBox As System.Windows.Forms.ComboBox
    End Class
End Namespace