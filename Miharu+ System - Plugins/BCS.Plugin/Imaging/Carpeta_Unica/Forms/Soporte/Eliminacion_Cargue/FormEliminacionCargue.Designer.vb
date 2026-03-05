Namespace Imaging.Carpeta_Unica.Forms.Soporte.Eliminacion_Cargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEliminacionCargue
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
            Me.GroupBoxEliminacionCargue = New System.Windows.Forms.GroupBox()
            Me.txtMotivo = New System.Windows.Forms.TextBox()
            Me.lblMotivo = New System.Windows.Forms.Label()
            Me.btnSalir = New System.Windows.Forms.Button()
            Me.BtnAceptar = New System.Windows.Forms.Button()
            Me.lblidCarguePaquete = New System.Windows.Forms.Label()
            Me.lblidCargue = New System.Windows.Forms.Label()
            Me.txtidCargue = New System.Windows.Forms.TextBox()
            Me.txtidCarguePaquete = New System.Windows.Forms.TextBox()
            Me.GroupBoxEliminacionCargue.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBoxEliminacionCargue
            '
            Me.GroupBoxEliminacionCargue.Controls.Add(Me.txtidCarguePaquete)
            Me.GroupBoxEliminacionCargue.Controls.Add(Me.txtidCargue)
            Me.GroupBoxEliminacionCargue.Controls.Add(Me.txtMotivo)
            Me.GroupBoxEliminacionCargue.Controls.Add(Me.lblMotivo)
            Me.GroupBoxEliminacionCargue.Controls.Add(Me.btnSalir)
            Me.GroupBoxEliminacionCargue.Controls.Add(Me.BtnAceptar)
            Me.GroupBoxEliminacionCargue.Controls.Add(Me.lblidCarguePaquete)
            Me.GroupBoxEliminacionCargue.Controls.Add(Me.lblidCargue)
            Me.GroupBoxEliminacionCargue.Location = New System.Drawing.Point(12, 13)
            Me.GroupBoxEliminacionCargue.Name = "GroupBoxEliminacionCargue"
            Me.GroupBoxEliminacionCargue.Size = New System.Drawing.Size(372, 254)
            Me.GroupBoxEliminacionCargue.TabIndex = 0
            Me.GroupBoxEliminacionCargue.TabStop = False
            Me.GroupBoxEliminacionCargue.Text = "Filtro"
            '
            'txtMotivo
            '
            Me.txtMotivo.Location = New System.Drawing.Point(139, 107)
            Me.txtMotivo.MaxLength = 500
            Me.txtMotivo.Multiline = True
            Me.txtMotivo.Name = "txtMotivo"
            Me.txtMotivo.Size = New System.Drawing.Size(214, 74)
            Me.txtMotivo.TabIndex = 3
            '
            'lblMotivo
            '
            Me.lblMotivo.AutoSize = True
            Me.lblMotivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblMotivo.Location = New System.Drawing.Point(17, 135)
            Me.lblMotivo.Name = "lblMotivo"
            Me.lblMotivo.Size = New System.Drawing.Size(49, 13)
            Me.lblMotivo.TabIndex = 0
            Me.lblMotivo.Text = "Motivo:"
            '
            'btnSalir
            '
            Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnSalir.Image = Global.BCS.Plugin.My.Resources.Resources.btnSalir
            Me.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnSalir.Location = New System.Drawing.Point(235, 198)
            Me.btnSalir.Name = "btnSalir"
            Me.btnSalir.Size = New System.Drawing.Size(77, 34)
            Me.btnSalir.TabIndex = 5
            Me.btnSalir.Text = "    Salir"
            Me.btnSalir.UseVisualStyleBackColor = True
            '
            'BtnAceptar
            '
            Me.BtnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnAceptar.Image = Global.BCS.Plugin.My.Resources.Resources.Aceptar
            Me.BtnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnAceptar.Location = New System.Drawing.Point(54, 198)
            Me.BtnAceptar.Name = "BtnAceptar"
            Me.BtnAceptar.Size = New System.Drawing.Size(88, 34)
            Me.BtnAceptar.TabIndex = 4
            Me.BtnAceptar.Text = "Aceptar"
            Me.BtnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BtnAceptar.UseVisualStyleBackColor = True
            '
            'lblidCarguePaquete
            '
            Me.lblidCarguePaquete.AutoSize = True
            Me.lblidCarguePaquete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblidCarguePaquete.Location = New System.Drawing.Point(17, 69)
            Me.lblidCarguePaquete.Name = "lblidCarguePaquete"
            Me.lblidCarguePaquete.Size = New System.Drawing.Size(116, 13)
            Me.lblidCarguePaquete.TabIndex = 0
            Me.lblidCarguePaquete.Text = "id Cargue Paquete:"
            '
            'lblidCargue
            '
            Me.lblidCargue.AutoSize = True
            Me.lblidCargue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblidCargue.Location = New System.Drawing.Point(17, 32)
            Me.lblidCargue.Name = "lblidCargue"
            Me.lblidCargue.Size = New System.Drawing.Size(65, 13)
            Me.lblidCargue.TabIndex = 0
            Me.lblidCargue.Text = "id Cargue:"
            '
            'txtidCargue
            '
            Me.txtidCargue.Location = New System.Drawing.Point(139, 32)
            Me.txtidCargue.Name = "txtidCargue"
            Me.txtidCargue.Size = New System.Drawing.Size(214, 20)
            Me.txtidCargue.TabIndex = 1
            '
            'txtidCarguePaquete
            '
            Me.txtidCarguePaquete.Location = New System.Drawing.Point(139, 69)
            Me.txtidCarguePaquete.Name = "txtidCarguePaquete"
            Me.txtidCarguePaquete.Size = New System.Drawing.Size(214, 20)
            Me.txtidCarguePaquete.TabIndex = 2
            '
            'FormEliminacionCargue
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(396, 279)
            Me.ControlBox = False
            Me.Controls.Add(Me.GroupBoxEliminacionCargue)
            Me.Name = "FormEliminacionCargue"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Eliminación Cargue - Paquete"
            Me.GroupBoxEliminacionCargue.ResumeLayout(False)
            Me.GroupBoxEliminacionCargue.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBoxEliminacionCargue As System.Windows.Forms.GroupBox
        Friend WithEvents lblidCarguePaquete As System.Windows.Forms.Label
        Friend WithEvents lblidCargue As System.Windows.Forms.Label
        Friend WithEvents btnSalir As System.Windows.Forms.Button
        Friend WithEvents BtnAceptar As System.Windows.Forms.Button
        Friend WithEvents txtMotivo As System.Windows.Forms.TextBox
        Friend WithEvents lblMotivo As System.Windows.Forms.Label
        Friend WithEvents txtidCarguePaquete As System.Windows.Forms.TextBox
        Friend WithEvents txtidCargue As System.Windows.Forms.TextBox
    End Class
End Namespace
