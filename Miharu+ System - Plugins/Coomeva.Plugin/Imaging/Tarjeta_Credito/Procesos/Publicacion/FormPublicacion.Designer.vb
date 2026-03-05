Namespace Imaging.Tarjeta_Credito.Procesos.Publicacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormPublicacion
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
            Me.GroupBoxPublicacion = New System.Windows.Forms.GroupBox()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.lblFechaProceso = New System.Windows.Forms.Label()
            Me.FechaProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.GroupBoxPublicacion.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBoxPublicacion
            '
            Me.GroupBoxPublicacion.Controls.Add(Me.CerrarButton)
            Me.GroupBoxPublicacion.Controls.Add(Me.AceptarButton)
            Me.GroupBoxPublicacion.Controls.Add(Me.lblFechaProceso)
            Me.GroupBoxPublicacion.Controls.Add(Me.FechaProcesoDateTimePicker)
            Me.GroupBoxPublicacion.Location = New System.Drawing.Point(12, 12)
            Me.GroupBoxPublicacion.Name = "GroupBoxPublicacion"
            Me.GroupBoxPublicacion.Size = New System.Drawing.Size(345, 145)
            Me.GroupBoxPublicacion.TabIndex = 1
            Me.GroupBoxPublicacion.TabStop = False
            Me.GroupBoxPublicacion.Text = "Filtros de Publicacion"
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Coomeva.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(226, 74)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(74, 33)
            Me.CerrarButton.TabIndex = 14
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = Global.Coomeva.Plugin.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(55, 74)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(79, 33)
            Me.AceptarButton.TabIndex = 13
            Me.AceptarButton.Text = "Publicar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'lblFechaProceso
            '
            Me.lblFechaProceso.AutoSize = True
            Me.lblFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaProceso.Location = New System.Drawing.Point(16, 29)
            Me.lblFechaProceso.Name = "lblFechaProceso"
            Me.lblFechaProceso.Size = New System.Drawing.Size(96, 13)
            Me.lblFechaProceso.TabIndex = 9
            Me.lblFechaProceso.Text = "Fecha Proceso:"
            '
            'FechaProcesoDateTimePicker
            '
            Me.FechaProcesoDateTimePicker.Location = New System.Drawing.Point(127, 23)
            Me.FechaProcesoDateTimePicker.Name = "FechaProcesoDateTimePicker"
            Me.FechaProcesoDateTimePicker.Size = New System.Drawing.Size(200, 20)
            Me.FechaProcesoDateTimePicker.TabIndex = 8
            '
            'FormPublicacion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(370, 171)
            Me.ControlBox = False
            Me.Controls.Add(Me.GroupBoxPublicacion)
            Me.Name = "FormPublicacion"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Publicación"
            Me.GroupBoxPublicacion.ResumeLayout(False)
            Me.GroupBoxPublicacion.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBoxPublicacion As System.Windows.Forms.GroupBox
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents lblFechaProceso As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoDateTimePicker As System.Windows.Forms.DateTimePicker
    End Class
End Namespace
