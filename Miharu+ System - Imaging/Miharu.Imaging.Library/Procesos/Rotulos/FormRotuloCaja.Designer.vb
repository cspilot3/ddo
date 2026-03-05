<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormRotuloCaja
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
        Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RotuloCarpetaCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OTDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BuscarButton = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.RotuloCarpetaCheckBox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.OTDesktopTextBox)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.BuscarButton)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(17, 16)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(371, 104)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Rotulos Caja"
        '
        'RotuloCarpetaCheckBox
        '
        Me.RotuloCarpetaCheckBox.AutoSize = True
        Me.RotuloCarpetaCheckBox.Location = New System.Drawing.Point(168, 61)
        Me.RotuloCarpetaCheckBox.Name = "RotuloCarpetaCheckBox"
        Me.RotuloCarpetaCheckBox.Size = New System.Drawing.Size(15, 14)
        Me.RotuloCarpetaCheckBox.TabIndex = 22
        Me.RotuloCarpetaCheckBox.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Generar rótulos carpeta "
        '
        'OTDesktopTextBox
        '
        Me.OTDesktopTextBox._Obligatorio = True
        Me.OTDesktopTextBox._PermitePegar = True
        Me.OTDesktopTextBox.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OTDesktopTextBox.Cantidad_Decimales = CType(2, Short)
        Me.OTDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
        Me.OTDesktopTextBox.DateFormat = Nothing
        Me.OTDesktopTextBox.DisabledEnter = False
        Me.OTDesktopTextBox.DisabledTab = False
        Me.OTDesktopTextBox.EnabledShortCuts = False
        Me.OTDesktopTextBox.fk_Campo = 0
        Me.OTDesktopTextBox.fk_Documento = 0
        Me.OTDesktopTextBox.fk_Validacion = 0
        Me.OTDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.OTDesktopTextBox.FocusOut = System.Drawing.Color.White
        Me.OTDesktopTextBox.HideSelection = False
        Me.OTDesktopTextBox.Location = New System.Drawing.Point(83, 30)
        Me.OTDesktopTextBox.MaskedTextBox_Property = ""
        Me.OTDesktopTextBox.MaximumLength = CType(0, Short)
        Me.OTDesktopTextBox.MinimumLength = CType(0, Short)
        Me.OTDesktopTextBox.Name = "OTDesktopTextBox"
        Me.OTDesktopTextBox.Obligatorio = True
        Me.OTDesktopTextBox.permitePegar = True
        Rango2.MaxValue = 2147483647.0R
        Rango2.MinValue = 0.0R
        Me.OTDesktopTextBox.Rango = Rango2
        Me.OTDesktopTextBox.Size = New System.Drawing.Size(133, 20)
        Me.OTDesktopTextBox.TabIndex = 20
        Me.OTDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
        Me.OTDesktopTextBox.Usa_Decimales = False
        Me.OTDesktopTextBox.ValidatingType = GetType(Integer)
        Me.OTDesktopTextBox.Validos_Cantidad_Puntos = False
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 13)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Caja"
        '
        'BuscarButton
        '
        Me.BuscarButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BuscarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.replace_image
        Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BuscarButton.Location = New System.Drawing.Point(254, 30)
        Me.BuscarButton.Name = "BuscarButton"
        Me.BuscarButton.Size = New System.Drawing.Size(99, 30)
        Me.BuscarButton.TabIndex = 18
        Me.BuscarButton.Text = "&Generar"
        Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BuscarButton.UseVisualStyleBackColor = True
        '
        'FormRotuloCaja
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(404, 137)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormRotuloCaja"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FormRótuloCaja"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RotuloCarpetaCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents OTDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BuscarButton As System.Windows.Forms.Button
End Class
