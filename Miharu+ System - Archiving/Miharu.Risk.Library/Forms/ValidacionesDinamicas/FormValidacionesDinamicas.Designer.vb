Namespace Forms.ValidacionesDinamicas
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.BtnCancelar = New System.Windows.Forms.Button()
            Me.BtnAceptar = New System.Windows.Forms.Button()
            Me.OTTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.OTLabel = New System.Windows.Forms.Label()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.BtnCancelar)
            Me.GroupBox1.Controls.Add(Me.BtnAceptar)
            Me.GroupBox1.Controls.Add(Me.OTTextBox)
            Me.GroupBox1.Controls.Add(Me.OTLabel)
            Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GroupBox1.Location = New System.Drawing.Point(13, 13)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(289, 160)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Filtros"
            '
            'BtnCancelar
            '
            Me.BtnCancelar.Location = New System.Drawing.Point(171, 102)
            Me.BtnCancelar.Name = "BtnCancelar"
            Me.BtnCancelar.Size = New System.Drawing.Size(75, 23)
            Me.BtnCancelar.TabIndex = 5
            Me.BtnCancelar.Text = "&Cancelar"
            Me.BtnCancelar.UseVisualStyleBackColor = True
            '
            'BtnAceptar
            '
            Me.BtnAceptar.Location = New System.Drawing.Point(47, 102)
            Me.BtnAceptar.Name = "BtnAceptar"
            Me.BtnAceptar.Size = New System.Drawing.Size(75, 23)
            Me.BtnAceptar.TabIndex = 4
            Me.BtnAceptar.Text = "&Aceptar"
            Me.BtnAceptar.UseVisualStyleBackColor = True
            '
            'OTTextBox
            '
            Me.OTTextBox._Obligatorio = False
            Me.OTTextBox._PermitePegar = False
            Me.OTTextBox.Cantidad_Decimales = CType(2, Short)
            Me.OTTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.OTTextBox.DateFormat = Nothing
            Me.OTTextBox.DisabledEnter = False
            Me.OTTextBox.DisabledTab = False
            Me.OTTextBox.EnabledShortCuts = False
            Me.OTTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.OTTextBox.FocusOut = System.Drawing.Color.White
            Me.OTTextBox.Location = New System.Drawing.Point(80, 23)
            Me.OTTextBox.Mask = "99999"
            Me.OTTextBox.MaskedTextBox_Property = "99999"
            Me.OTTextBox.MaximumLength = CType(0, Short)
            Me.OTTextBox.MinimumLength = CType(0, Short)
            Me.OTTextBox.Name = "OTTextBox"
            Me.OTTextBox.Obligatorio = False
            Me.OTTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.OTTextBox.Rango = Rango1
            Me.OTTextBox.Size = New System.Drawing.Size(166, 20)
            Me.OTTextBox.TabIndex = 3
            Me.OTTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.OTTextBox.Usa_Decimales = False
            Me.OTTextBox.ValidatingType = GetType(Integer)
            Me.OTTextBox.Validos_Cantidad_Puntos = False
            '
            'OTLabel
            '
            Me.OTLabel.AutoSize = True
            Me.OTLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OTLabel.Location = New System.Drawing.Point(6, 26)
            Me.OTLabel.Name = "OTLabel"
            Me.OTLabel.Size = New System.Drawing.Size(32, 13)
            Me.OTLabel.TabIndex = 2
            Me.OTLabel.Text = "O.T."
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
        Friend WithEvents OTTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents OTLabel As System.Windows.Forms.Label
        Friend WithEvents BtnCancelar As System.Windows.Forms.Button
        Friend WithEvents BtnAceptar As System.Windows.Forms.Button
    End Class
End Namespace