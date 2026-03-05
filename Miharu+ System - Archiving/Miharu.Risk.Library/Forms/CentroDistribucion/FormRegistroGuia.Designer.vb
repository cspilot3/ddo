Imports Miharu.Desktop.Controls.DesktopDataGridView

Namespace Forms.CentroDistribucion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormRegistroGuia
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
            Me.gbxRegistroGuia = New System.Windows.Forms.GroupBox()
            Me.lblRegistroRemisionGuia = New System.Windows.Forms.Label()
            Me.txtNumeroGuia = New System.Windows.Forms.TextBox()
            Me.txtNumeroRemision = New System.Windows.Forms.TextBox()
            Me.btnCerrar = New System.Windows.Forms.Button()
            Me.btnAceptar = New System.Windows.Forms.Button()
            Me.lblNumeroGuia = New System.Windows.Forms.Label()
            Me.lblNumeroRemision = New System.Windows.Forms.Label()
            Me.gbxRegistroGuia.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbxRegistroGuia
            '
            Me.gbxRegistroGuia.Controls.Add(Me.lblRegistroRemisionGuia)
            Me.gbxRegistroGuia.Controls.Add(Me.txtNumeroGuia)
            Me.gbxRegistroGuia.Controls.Add(Me.txtNumeroRemision)
            Me.gbxRegistroGuia.Controls.Add(Me.btnCerrar)
            Me.gbxRegistroGuia.Controls.Add(Me.btnAceptar)
            Me.gbxRegistroGuia.Controls.Add(Me.lblNumeroGuia)
            Me.gbxRegistroGuia.Controls.Add(Me.lblNumeroRemision)
            Me.gbxRegistroGuia.Location = New System.Drawing.Point(13, 13)
            Me.gbxRegistroGuia.Name = "gbxRegistroGuia"
            Me.gbxRegistroGuia.Size = New System.Drawing.Size(331, 160)
            Me.gbxRegistroGuia.TabIndex = 2
            Me.gbxRegistroGuia.TabStop = False
            '
            'lblRegistroRemisionGuia
            '
            Me.lblRegistroRemisionGuia.AutoSize = True
            Me.lblRegistroRemisionGuia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblRegistroRemisionGuia.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.lblRegistroRemisionGuia.Location = New System.Drawing.Point(5, 13)
            Me.lblRegistroRemisionGuia.Name = "lblRegistroRemisionGuia"
            Me.lblRegistroRemisionGuia.Size = New System.Drawing.Size(302, 13)
            Me.lblRegistroRemisionGuia.TabIndex = 5
            Me.lblRegistroRemisionGuia.Text = "INGRESE EL NÚMERO DE REMISIÓN Y GUÍA ASOCIADA:"
            '
            'txtNumeroGuia
            '
            Me.txtNumeroGuia.Location = New System.Drawing.Point(41, 95)
            Me.txtNumeroGuia.Name = "txtNumeroGuia"
            Me.txtNumeroGuia.ShortcutsEnabled = False
            Me.txtNumeroGuia.Size = New System.Drawing.Size(235, 21)
            Me.txtNumeroGuia.TabIndex = 2
            '
            'txtNumeroRemision
            '
            Me.txtNumeroRemision.Location = New System.Drawing.Point(41, 55)
            Me.txtNumeroRemision.Name = "txtNumeroRemision"
            Me.txtNumeroRemision.ShortcutsEnabled = False
            Me.txtNumeroRemision.Size = New System.Drawing.Size(235, 21)
            Me.txtNumeroRemision.TabIndex = 1
            '
            'btnCerrar
            '
            Me.btnCerrar.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Cancelar
            Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCerrar.Location = New System.Drawing.Point(201, 129)
            Me.btnCerrar.Name = "btnCerrar"
            Me.btnCerrar.Size = New System.Drawing.Size(75, 23)
            Me.btnCerrar.TabIndex = 4
            Me.btnCerrar.Text = "&Cerrar"
            Me.btnCerrar.UseVisualStyleBackColor = True
            '
            'btnAceptar
            '
            Me.btnAceptar.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnAceptar.Location = New System.Drawing.Point(41, 129)
            Me.btnAceptar.Name = "btnAceptar"
            Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
            Me.btnAceptar.TabIndex = 3
            Me.btnAceptar.Text = "&Aceptar"
            Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnAceptar.UseVisualStyleBackColor = True
            '
            'lblNumeroGuia
            '
            Me.lblNumeroGuia.AutoSize = True
            Me.lblNumeroGuia.Location = New System.Drawing.Point(40, 79)
            Me.lblNumeroGuia.Name = "lblNumeroGuia"
            Me.lblNumeroGuia.Size = New System.Drawing.Size(96, 13)
            Me.lblNumeroGuia.TabIndex = 4
            Me.lblNumeroGuia.Text = "Número de Guía"
            '
            'lblNumeroRemision
            '
            Me.lblNumeroRemision.AutoSize = True
            Me.lblNumeroRemision.Location = New System.Drawing.Point(40, 40)
            Me.lblNumeroRemision.Name = "lblNumeroRemision"
            Me.lblNumeroRemision.Size = New System.Drawing.Size(123, 13)
            Me.lblNumeroRemision.TabIndex = 3
            Me.lblNumeroRemision.Text = "Número de Remisión"
            '
            'FormRegistroGuia
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(356, 190)
            Me.Controls.Add(Me.gbxRegistroGuia)
            Me.Name = "FormRegistroGuia"
            Me.Text = "Registro de Guía"
            Me.gbxRegistroGuia.ResumeLayout(False)
            Me.gbxRegistroGuia.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents gbxRegistroGuia As System.Windows.Forms.GroupBox
        Friend WithEvents lblNumeroGuia As System.Windows.Forms.Label
        Friend WithEvents lblNumeroRemision As System.Windows.Forms.Label
        Friend WithEvents btnCerrar As System.Windows.Forms.Button
        Friend WithEvents btnAceptar As System.Windows.Forms.Button
        Friend WithEvents txtNumeroGuia As System.Windows.Forms.TextBox
        Friend WithEvents txtNumeroRemision As System.Windows.Forms.TextBox
        Friend WithEvents lblRegistroRemisionGuia As System.Windows.Forms.Label
    End Class

End Namespace
