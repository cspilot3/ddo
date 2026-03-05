Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Imaging.Forms.Cargue

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormValidacionFechCargue
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormValidacionFechCargue))
            Dim Rango1 As Rango = New Rango()
            Dim Rango2 As Rango = New Rango()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.lbl_Descripcion = New System.Windows.Forms.Label()
            Me.lbl_motivo = New System.Windows.Forms.Label()
            Me.Desktop_txt_Descripcion = New DesktopTextBoxControl()
            Me.Desktop_txt_Motivo = New DesktopTextBoxControl()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.CancelarButton)
            Me.Panel1.Controls.Add(Me.AceptarButton)
            Me.Panel1.Location = New System.Drawing.Point(115, 137)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(192, 33)
            Me.Panel1.TabIndex = 11
            Me.Panel1.UseWaitCursor = True
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(100, 2)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(88, 30)
            Me.CancelarButton.TabIndex = 36
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseWaitCursor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(3, 3)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(88, 30)
            Me.AceptarButton.TabIndex = 35
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseWaitCursor = True
            '
            'lbl_Descripcion
            '
            Me.lbl_Descripcion.AutoSize = True
            Me.lbl_Descripcion.Location = New System.Drawing.Point(12, 51)
            Me.lbl_Descripcion.Name = "lbl_Descripcion"
            Me.lbl_Descripcion.Size = New System.Drawing.Size(66, 13)
            Me.lbl_Descripcion.TabIndex = 9
            Me.lbl_Descripcion.Text = "Descripción:"
            '
            'lbl_motivo
            '
            Me.lbl_motivo.AutoSize = True
            Me.lbl_motivo.Location = New System.Drawing.Point(12, 9)
            Me.lbl_motivo.Name = "lbl_motivo"
            Me.lbl_motivo.Size = New System.Drawing.Size(42, 13)
            Me.lbl_motivo.TabIndex = 8
            Me.lbl_motivo.Text = "Motivo:"
            '
            'Desktop_txt_Descripcion
            '
            Me.Desktop_txt_Descripcion.Cantidad_Decimales = CType(0, Short)
            Me.Desktop_txt_Descripcion.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.Desktop_txt_Descripcion.DisabledEnter = False
            Me.Desktop_txt_Descripcion.DisabledTab = False
            Me.Desktop_txt_Descripcion.FocusIn = System.Drawing.Color.LightYellow
            Me.Desktop_txt_Descripcion.FocusOut = System.Drawing.Color.White
            Me.Desktop_txt_Descripcion.Location = New System.Drawing.Point(87, 48)
            Me.Desktop_txt_Descripcion.MaximumLength = CType(0, Short)
            Me.Desktop_txt_Descripcion.MaxLength = 0
            Me.Desktop_txt_Descripcion.MinimumLength = CType(0, Short)
            Me.Desktop_txt_Descripcion.Multiline = True
            Me.Desktop_txt_Descripcion.Name = "Desktop_txt_Descripcion"
            Rango1.MaxValue = 1.7976931348623157E+308R
            Rango1.MinValue = 0.0R
            Me.Desktop_txt_Descripcion.Rango = Rango1
            Me.Desktop_txt_Descripcion.ShortcutsEnabled = False
            Me.Desktop_txt_Descripcion.Size = New System.Drawing.Size(303, 83)
            Me.Desktop_txt_Descripcion.TabIndex = 10
            Me.Desktop_txt_Descripcion.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.Desktop_txt_Descripcion.Usa_Decimales = False
            Me.Desktop_txt_Descripcion.Validos_Cantidad_Puntos = False
            '
            'Desktop_txt_Motivo
            '
            Me.Desktop_txt_Motivo.Cantidad_Decimales = CType(0, Short)
            Me.Desktop_txt_Motivo.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.Desktop_txt_Motivo.DisabledEnter = False
            Me.Desktop_txt_Motivo.DisabledTab = False
            Me.Desktop_txt_Motivo.FocusIn = System.Drawing.Color.LightYellow
            Me.Desktop_txt_Motivo.FocusOut = System.Drawing.Color.White
            Me.Desktop_txt_Motivo.Location = New System.Drawing.Point(87, 6)
            Me.Desktop_txt_Motivo.MaximumLength = CType(0, Short)
            Me.Desktop_txt_Motivo.MaxLength = 0
            Me.Desktop_txt_Motivo.MinimumLength = CType(0, Short)
            Me.Desktop_txt_Motivo.Name = "Desktop_txt_Motivo"
            Rango2.MaxValue = 1.7976931348623157E+308R
            Rango2.MinValue = 0.0R
            Me.Desktop_txt_Motivo.Rango = Rango2
            Me.Desktop_txt_Motivo.ShortcutsEnabled = False
            Me.Desktop_txt_Motivo.Size = New System.Drawing.Size(303, 20)
            Me.Desktop_txt_Motivo.TabIndex = 7
            Me.Desktop_txt_Motivo.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.Desktop_txt_Motivo.Usa_Decimales = False
            Me.Desktop_txt_Motivo.Validos_Cantidad_Puntos = False
            '
            'Form_Validacion_Fech_Cargue
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(409, 176)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.Desktop_txt_Descripcion)
            Me.Controls.Add(Me.lbl_Descripcion)
            Me.Controls.Add(Me.lbl_motivo)
            Me.Controls.Add(Me.Desktop_txt_Motivo)
            Me.Name = "FormValidacionFechCargue"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Validación Fecha Cargue"
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents Desktop_txt_Descripcion As DesktopTextBoxControl
        Friend WithEvents lbl_Descripcion As System.Windows.Forms.Label
        Friend WithEvents lbl_motivo As System.Windows.Forms.Label
        Friend WithEvents Desktop_txt_Motivo As DesktopTextBoxControl
    End Class
End Namespace