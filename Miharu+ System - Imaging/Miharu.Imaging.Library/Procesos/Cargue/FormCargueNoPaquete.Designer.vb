Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Procesos.Cargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCargueNoPaquete
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
            Dim Rango1 As Rango = New Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCargueNoPaquete))
            Me.RutaGroupBox = New System.Windows.Forms.GroupBox()
            Me.SelectFolderButton = New System.Windows.Forms.Button()
            Me.RutaTextBox = New DesktopTextBoxControl()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.RutaGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'RutaGroupBox
            '
            Me.RutaGroupBox.Controls.Add(Me.SelectFolderButton)
            Me.RutaGroupBox.Controls.Add(Me.RutaTextBox)
            Me.RutaGroupBox.Location = New System.Drawing.Point(12, 12)
            Me.RutaGroupBox.Name = "RutaGroupBox"
            Me.RutaGroupBox.Size = New System.Drawing.Size(437, 64)
            Me.RutaGroupBox.TabIndex = 1
            Me.RutaGroupBox.TabStop = False
            Me.RutaGroupBox.Text = "Ruta"
            '
            'SelectFolderButton
            '
            Me.SelectFolderButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.folder_image
            Me.SelectFolderButton.Location = New System.Drawing.Point(404, 25)
            Me.SelectFolderButton.Name = "SelectFolderButton"
            Me.SelectFolderButton.Size = New System.Drawing.Size(27, 23)
            Me.SelectFolderButton.TabIndex = 1
            Me.SelectFolderButton.UseVisualStyleBackColor = True
            '
            'RutaTextBox
            '
            Me.RutaTextBox.Cantidad_Decimales = CType(0, Short)
            Me.RutaTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.RutaTextBox.DateFormat = Nothing
            Me.RutaTextBox.DisabledEnter = False
            Me.RutaTextBox.DisabledTab = False
            Me.RutaTextBox.EnabledShortCuts = False
            Me.RutaTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.RutaTextBox.FocusOut = System.Drawing.Color.White
            Me.RutaTextBox.Location = New System.Drawing.Point(13, 25)
            Me.RutaTextBox.MaskedTextBox_Property = ""
            Me.RutaTextBox.MaximumLength = CType(0, Short)
            Me.RutaTextBox.MinimumLength = CType(0, Short)
            Me.RutaTextBox.Name = "RutaTextBox"
            Rango1.MaxValue = 9.2233720368547758E+18R
            Rango1.MinValue = 0.0R
            Me.RutaTextBox.Rango = Rango1
            Me.RutaTextBox.Size = New System.Drawing.Size(385, 20)
            Me.RutaTextBox.TabIndex = 0
            Me.RutaTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.RutaTextBox.Usa_Decimales = False
            Me.RutaTextBox.Validos_Cantidad_Puntos = False
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(369, 82)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 23)
            Me.CancelarButton.TabIndex = 3
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'AceptarButton
            '
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(273, 82)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 23)
            Me.AceptarButton.TabIndex = 2
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'FormCargueNoPaquete
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(461, 117)
            Me.Controls.Add(Me.RutaGroupBox)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCargueNoPaquete"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Cargue"
            Me.RutaGroupBox.ResumeLayout(False)
            Me.RutaGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents RutaGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents SelectFolderButton As System.Windows.Forms.Button
        Friend WithEvents RutaTextBox As DesktopTextBoxControl
    End Class
End Namespace