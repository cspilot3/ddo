Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Procesos.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormDestapeMasivo
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
            Dim Rango5 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDestapeMasivo))
            Dim Rango6 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.RutaGroupBox = New System.Windows.Forms.GroupBox()
            Me.SelectFolderButton = New System.Windows.Forms.Button()
            Me.RutaTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.CancelarMasivosButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.OTTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.RutaGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'RutaGroupBox
            '
            Me.RutaGroupBox.Controls.Add(Me.SelectFolderButton)
            Me.RutaGroupBox.Controls.Add(Me.RutaTextBox)
            Me.RutaGroupBox.Location = New System.Drawing.Point(14, 43)
            Me.RutaGroupBox.Name = "RutaGroupBox"
            Me.RutaGroupBox.Size = New System.Drawing.Size(375, 65)
            Me.RutaGroupBox.TabIndex = 4
            Me.RutaGroupBox.TabStop = False
            Me.RutaGroupBox.Text = "Ruta"
            '
            'SelectFolderButton
            '
            Me.SelectFolderButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.folder_image
            Me.SelectFolderButton.Location = New System.Drawing.Point(346, 25)
            Me.SelectFolderButton.Name = "SelectFolderButton"
            Me.SelectFolderButton.Size = New System.Drawing.Size(23, 23)
            Me.SelectFolderButton.TabIndex = 1
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
            Rango5.MaxValue = 9.2233720368547758E+18R
            Rango5.MinValue = 0.0R
            Me.RutaTextBox.Rango = Rango5
            Me.RutaTextBox.Size = New System.Drawing.Size(331, 20)
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
            Me.CancelarMasivosButton.Location = New System.Drawing.Point(316, 125)
            Me.CancelarMasivosButton.Name = "CancelarMasivosButton"
            Me.CancelarMasivosButton.Size = New System.Drawing.Size(73, 23)
            Me.CancelarMasivosButton.TabIndex = 6
            Me.CancelarMasivosButton.Text = "&Cancelar"
            Me.CancelarMasivosButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'AceptarButton
            '
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(234, 125)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(69, 23)
            Me.AceptarButton.TabIndex = 5
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'OTTextBox
            '
            Me.OTTextBox._Obligatorio = False
            Me.OTTextBox._PermitePegar = False
            Me.OTTextBox.Cantidad_Decimales = CType(0, Short)
            Me.OTTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.OTTextBox.DateFormat = Nothing
            Me.OTTextBox.DisabledEnter = False
            Me.OTTextBox.DisabledTab = False
            Me.OTTextBox.Enabled = False
            Me.OTTextBox.EnabledShortCuts = False
            Me.OTTextBox.fk_Campo = 0
            Me.OTTextBox.fk_Documento = 0
            Me.OTTextBox.fk_Validacion = 0
            Me.OTTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.OTTextBox.FocusOut = System.Drawing.Color.White
            Me.OTTextBox.Location = New System.Drawing.Point(37, 6)
            Me.OTTextBox.MaskedTextBox_Property = ""
            Me.OTTextBox.MaximumLength = CType(0, Short)
            Me.OTTextBox.MinimumLength = CType(0, Short)
            Me.OTTextBox.Name = "OTTextBox"
            Me.OTTextBox.Obligatorio = False
            Me.OTTextBox.permitePegar = False
            Rango6.MaxValue = 9.2233720368547758E+18R
            Rango6.MinValue = 0.0R
            Me.OTTextBox.Rango = Rango6
            Me.OTTextBox.Size = New System.Drawing.Size(102, 20)
            Me.OTTextBox.TabIndex = 7
            Me.OTTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.OTTextBox.Usa_Decimales = False
            Me.OTTextBox.Validos_Cantidad_Puntos = False
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(11, 9)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(24, 13)
            Me.Label2.TabIndex = 24
            Me.Label2.Text = "OT"
            '
            'FormDestapeMasivo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(429, 162)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.OTTextBox)
            Me.Controls.Add(Me.RutaGroupBox)
            Me.Controls.Add(Me.CancelarMasivosButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
            Me.Name = "FormDestapeMasivo"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "FormDestapeMasivo"
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
        Friend WithEvents OTTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
    End Class
End Namespace

