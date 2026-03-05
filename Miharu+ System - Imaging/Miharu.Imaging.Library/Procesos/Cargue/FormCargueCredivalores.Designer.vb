<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCargueCredivalores
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
        Dim Label1 As System.Windows.Forms.Label
        Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCargueCredivalores))
        Me.SelectFolderButton = New System.Windows.Forms.Button()
        Me.ProgressBarReemplazar = New System.Windows.Forms.ProgressBar()
        Me.RutaGroupBox = New System.Windows.Forms.GroupBox()
        Me.CarpetaSalidaTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
        Me.CancelarMasivosButton = New System.Windows.Forms.Button()
        Me.AceptarButton = New System.Windows.Forms.Button()
        Label1 = New System.Windows.Forms.Label()
        Me.RutaGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(12, 18)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(70, 13)
        Label1.TabIndex = 16
        Label1.Text = "Parámetros"
        '
        'SelectFolderButton
        '
        Me.SelectFolderButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.folder_image
        Me.SelectFolderButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.SelectFolderButton.Location = New System.Drawing.Point(12, 121)
        Me.SelectFolderButton.Name = "SelectFolderButton"
        Me.SelectFolderButton.Size = New System.Drawing.Size(106, 26)
        Me.SelectFolderButton.TabIndex = 12
        Me.SelectFolderButton.Text = "Seleccionar"
        Me.SelectFolderButton.UseVisualStyleBackColor = True
        '
        'ProgressBarReemplazar
        '
        Me.ProgressBarReemplazar.Location = New System.Drawing.Point(15, 165)
        Me.ProgressBarReemplazar.Name = "ProgressBarReemplazar"
        Me.ProgressBarReemplazar.Size = New System.Drawing.Size(446, 23)
        Me.ProgressBarReemplazar.TabIndex = 17
        Me.ProgressBarReemplazar.Visible = False
        '
        'RutaGroupBox
        '
        Me.RutaGroupBox.Controls.Add(Me.CarpetaSalidaTextBox)
        Me.RutaGroupBox.Location = New System.Drawing.Point(12, 50)
        Me.RutaGroupBox.Name = "RutaGroupBox"
        Me.RutaGroupBox.Size = New System.Drawing.Size(449, 65)
        Me.RutaGroupBox.TabIndex = 13
        Me.RutaGroupBox.TabStop = False
        Me.RutaGroupBox.Text = "Ruta :"
        '
        'CarpetaSalidaTextBox
        '
        Me.CarpetaSalidaTextBox._Obligatorio = False
        Me.CarpetaSalidaTextBox._PermitePegar = False
        Me.CarpetaSalidaTextBox.Cantidad_Decimales = CType(0, Short)
        Me.CarpetaSalidaTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
        Me.CarpetaSalidaTextBox.DateFormat = Nothing
        Me.CarpetaSalidaTextBox.DisabledEnter = False
        Me.CarpetaSalidaTextBox.DisabledTab = False
        Me.CarpetaSalidaTextBox.EnabledShortCuts = False
        Me.CarpetaSalidaTextBox.fk_Campo = 0
        Me.CarpetaSalidaTextBox.fk_Documento = 0
        Me.CarpetaSalidaTextBox.fk_Validacion = 0
        Me.CarpetaSalidaTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.CarpetaSalidaTextBox.FocusOut = System.Drawing.Color.White
        Me.CarpetaSalidaTextBox.Location = New System.Drawing.Point(11, 25)
        Me.CarpetaSalidaTextBox.MaskedTextBox_Property = ""
        Me.CarpetaSalidaTextBox.MaximumLength = CType(0, Short)
        Me.CarpetaSalidaTextBox.MinimumLength = CType(0, Short)
        Me.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox"
        Me.CarpetaSalidaTextBox.Obligatorio = False
        Me.CarpetaSalidaTextBox.permitePegar = False
        Rango1.MaxValue = 9.2233720368547758E+18R
        Rango1.MinValue = 0.0R
        Me.CarpetaSalidaTextBox.Rango = Rango1
        Me.CarpetaSalidaTextBox.Size = New System.Drawing.Size(432, 20)
        Me.CarpetaSalidaTextBox.TabIndex = 0
        Me.CarpetaSalidaTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
        Me.CarpetaSalidaTextBox.Usa_Decimales = False
        Me.CarpetaSalidaTextBox.Validos_Cantidad_Puntos = False
        '
        'CancelarMasivosButton
        '
        Me.CancelarMasivosButton.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.CancelarMasivosButton.Image = CType(resources.GetObject("CancelarMasivosButton.Image"), System.Drawing.Image)
        Me.CancelarMasivosButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelarMasivosButton.Location = New System.Drawing.Point(246, 121)
        Me.CancelarMasivosButton.Name = "CancelarMasivosButton"
        Me.CancelarMasivosButton.Size = New System.Drawing.Size(79, 26)
        Me.CancelarMasivosButton.TabIndex = 15
        Me.CancelarMasivosButton.Text = "&Cancelar"
        Me.CancelarMasivosButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AceptarButton
        '
        Me.AceptarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGuardar
        Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AceptarButton.Location = New System.Drawing.Point(143, 121)
        Me.AceptarButton.Name = "AceptarButton"
        Me.AceptarButton.Size = New System.Drawing.Size(81, 26)
        Me.AceptarButton.TabIndex = 14
        Me.AceptarButton.Text = "&Procesar"
        Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FormCargueCredivalores
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 208)
        Me.Controls.Add(Me.SelectFolderButton)
        Me.Controls.Add(Me.ProgressBarReemplazar)
        Me.Controls.Add(Label1)
        Me.Controls.Add(Me.RutaGroupBox)
        Me.Controls.Add(Me.CancelarMasivosButton)
        Me.Controls.Add(Me.AceptarButton)
        Me.Name = "FormCargueCredivalores"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FormCargueCredivalores"
        Me.RutaGroupBox.ResumeLayout(False)
        Me.RutaGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SelectFolderButton As System.Windows.Forms.Button
    Friend WithEvents ProgressBarReemplazar As System.Windows.Forms.ProgressBar
    Friend WithEvents RutaGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents CarpetaSalidaTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
    Friend WithEvents CancelarMasivosButton As System.Windows.Forms.Button
    Friend WithEvents AceptarButton As System.Windows.Forms.Button
End Class
