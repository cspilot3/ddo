<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReemplazarImagenesForm
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
        Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReemplazarImagenesForm))
        Me.RutaGroupBox = New System.Windows.Forms.GroupBox()
        Me.SelectFolderButton = New System.Windows.Forms.Button()
        Me.RutaTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
        Me.CancelarMasivosButton = New System.Windows.Forms.Button()
        Me.AceptarButton = New System.Windows.Forms.Button()
        Me.ProgressBarReemplazar = New System.Windows.Forms.ProgressBar()
        Label1 = New System.Windows.Forms.Label()
        Me.RutaGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(22, 13)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(70, 13)
        Label1.TabIndex = 10
        Label1.Text = "Parámetros"
        '
        'RutaGroupBox
        '
        Me.RutaGroupBox.Controls.Add(Me.RutaTextBox)
        Me.RutaGroupBox.Location = New System.Drawing.Point(22, 45)
        Me.RutaGroupBox.Name = "RutaGroupBox"
        Me.RutaGroupBox.Size = New System.Drawing.Size(449, 65)
        Me.RutaGroupBox.TabIndex = 7
        Me.RutaGroupBox.TabStop = False
        Me.RutaGroupBox.Text = "Ruta :"
        '
        'SelectFolderButton
        '
        Me.SelectFolderButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.folder_image
        Me.SelectFolderButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.SelectFolderButton.Location = New System.Drawing.Point(22, 116)
        Me.SelectFolderButton.Name = "SelectFolderButton"
        Me.SelectFolderButton.Size = New System.Drawing.Size(106, 26)
        Me.SelectFolderButton.TabIndex = 1
        Me.SelectFolderButton.Text = "Seleccionar"
        Me.SelectFolderButton.UseVisualStyleBackColor = True
        '
        'RutaTextBox
        '
        Me.RutaTextBox._Obligatorio = False
        Me.RutaTextBox._PermitePegar = False
        Me.RutaTextBox.Cantidad_Decimales = CType(0, Short)
        Me.RutaTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
        Me.RutaTextBox.DateFormat = Nothing
        Me.RutaTextBox.DisabledEnter = False
        Me.RutaTextBox.DisabledTab = False
        Me.RutaTextBox.EnabledShortCuts = False
        Me.RutaTextBox.fk_Campo = 0
        Me.RutaTextBox.fk_Documento = 0
        Me.RutaTextBox.fk_Validacion = 0
        Me.RutaTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.RutaTextBox.FocusOut = System.Drawing.Color.White
        Me.RutaTextBox.Location = New System.Drawing.Point(11, 25)
        Me.RutaTextBox.MaskedTextBox_Property = ""
        Me.RutaTextBox.MaximumLength = CType(0, Short)
        Me.RutaTextBox.MinimumLength = CType(0, Short)
        Me.RutaTextBox.Name = "RutaTextBox"
        Me.RutaTextBox.Obligatorio = False
        Me.RutaTextBox.permitePegar = False
        Rango2.MaxValue = 9.2233720368547758E+18R
        Rango2.MinValue = 0.0R
        Me.RutaTextBox.Rango = Rango2
        Me.RutaTextBox.Size = New System.Drawing.Size(432, 20)
        Me.RutaTextBox.TabIndex = 0
        Me.RutaTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
        Me.RutaTextBox.Usa_Decimales = False
        Me.RutaTextBox.Validos_Cantidad_Puntos = False
        '
        'CancelarMasivosButton
        '
        Me.CancelarMasivosButton.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.CancelarMasivosButton.Image = CType(resources.GetObject("CancelarMasivosButton.Image"), System.Drawing.Image)
        Me.CancelarMasivosButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelarMasivosButton.Location = New System.Drawing.Point(256, 116)
        Me.CancelarMasivosButton.Name = "CancelarMasivosButton"
        Me.CancelarMasivosButton.Size = New System.Drawing.Size(79, 26)
        Me.CancelarMasivosButton.TabIndex = 9
        Me.CancelarMasivosButton.Text = "&Cancelar"
        Me.CancelarMasivosButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AceptarButton
        '
        Me.AceptarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGuardar
        Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AceptarButton.Location = New System.Drawing.Point(153, 116)
        Me.AceptarButton.Name = "AceptarButton"
        Me.AceptarButton.Size = New System.Drawing.Size(81, 26)
        Me.AceptarButton.TabIndex = 8
        Me.AceptarButton.Text = "&Procesar"
        Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ProgressBarReemplazar
        '
        Me.ProgressBarReemplazar.Location = New System.Drawing.Point(25, 160)
        Me.ProgressBarReemplazar.Name = "ProgressBarReemplazar"
        Me.ProgressBarReemplazar.Size = New System.Drawing.Size(446, 23)
        Me.ProgressBarReemplazar.TabIndex = 11
        Me.ProgressBarReemplazar.Visible = False
        '
        'ReemplazarImagenesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 195)
        Me.Controls.Add(Me.SelectFolderButton)
        Me.Controls.Add(Me.ProgressBarReemplazar)
        Me.Controls.Add(Label1)
        Me.Controls.Add(Me.RutaGroupBox)
        Me.Controls.Add(Me.CancelarMasivosButton)
        Me.Controls.Add(Me.AceptarButton)
        Me.Name = "ReemplazarImagenesForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cargue de Cartas"
        Me.RutaGroupBox.ResumeLayout(False)
        Me.RutaGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RutaGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents SelectFolderButton As System.Windows.Forms.Button
    Friend WithEvents RutaTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
    Friend WithEvents CancelarMasivosButton As System.Windows.Forms.Button
    Friend WithEvents AceptarButton As System.Windows.Forms.Button
    Friend WithEvents ProgressBarReemplazar As System.Windows.Forms.ProgressBar
End Class
