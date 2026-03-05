Namespace Firmas.Forms.PrepararData
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormPrepararData
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
            Me.Label1 = New System.Windows.Forms.Label()
            Me.PublicarButton = New System.Windows.Forms.Button()
            Me.CruzarButton = New System.Windows.Forms.Button()
            Me.fechaProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.EnviarCorreoButton = New System.Windows.Forms.Button()
            Me.CierreReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(12, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(118, 13)
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "Fechas Procesadas"
            '
            'PublicarButton
            '
            Me.PublicarButton.AccessibleDescription = ""
            Me.PublicarButton.BackColor = System.Drawing.SystemColors.Control
            Me.PublicarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.PublicarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PublicarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Process_Accept
            Me.PublicarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.PublicarButton.Location = New System.Drawing.Point(130, 38)
            Me.PublicarButton.Name = "PublicarButton"
            Me.PublicarButton.Size = New System.Drawing.Size(100, 60)
            Me.PublicarButton.TabIndex = 23
            Me.PublicarButton.Tag = "Ctrl + P"
            Me.PublicarButton.Text = "&Publicar"
            Me.PublicarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.PublicarButton.UseVisualStyleBackColor = False
            '
            'CruzarButton
            '
            Me.CruzarButton.AccessibleDescription = ""
            Me.CruzarButton.BackColor = System.Drawing.SystemColors.Control
            Me.CruzarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CruzarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CruzarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.cross
            Me.CruzarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CruzarButton.Location = New System.Drawing.Point(15, 38)
            Me.CruzarButton.Name = "CruzarButton"
            Me.CruzarButton.Size = New System.Drawing.Size(100, 60)
            Me.CruzarButton.TabIndex = 22
            Me.CruzarButton.Tag = "Ctrl + C"
            Me.CruzarButton.Text = "&Cruzar"
            Me.CruzarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CruzarButton.UseVisualStyleBackColor = False
            '
            'fechaProcesoDateTimePicker
            '
            Me.fechaProcesoDateTimePicker.Location = New System.Drawing.Point(146, 9)
            Me.fechaProcesoDateTimePicker.Name = "fechaProcesoDateTimePicker"
            Me.fechaProcesoDateTimePicker.Size = New System.Drawing.Size(200, 20)
            Me.fechaProcesoDateTimePicker.TabIndex = 24
            '
            'EnviarCorreoButton
            '
            Me.EnviarCorreoButton.AccessibleDescription = ""
            Me.EnviarCorreoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EnviarCorreoButton.BackColor = System.Drawing.SystemColors.Control
            Me.EnviarCorreoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.EnviarCorreoButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EnviarCorreoButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.envio_correo
            Me.EnviarCorreoButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.EnviarCorreoButton.Location = New System.Drawing.Point(627, 38)
            Me.EnviarCorreoButton.Name = "EnviarCorreoButton"
            Me.EnviarCorreoButton.Size = New System.Drawing.Size(100, 60)
            Me.EnviarCorreoButton.TabIndex = 38
            Me.EnviarCorreoButton.Tag = "Ctrl + P"
            Me.EnviarCorreoButton.Text = "&Enviar Correo"
            Me.EnviarCorreoButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.EnviarCorreoButton.UseVisualStyleBackColor = False
            '
            'CierreReportViewer
            '
            Me.CierreReportViewer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CierreReportViewer.Location = New System.Drawing.Point(12, 104)
            Me.CierreReportViewer.Name = "CierreReportViewer"
            Me.CierreReportViewer.Size = New System.Drawing.Size(715, 349)
            Me.CierreReportViewer.TabIndex = 39
            '
            'FormPrepararData
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(739, 465)
            Me.Controls.Add(Me.CierreReportViewer)
            Me.Controls.Add(Me.EnviarCorreoButton)
            Me.Controls.Add(Me.fechaProcesoDateTimePicker)
            Me.Controls.Add(Me.PublicarButton)
            Me.Controls.Add(Me.CruzarButton)
            Me.Controls.Add(Me.Label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MinimumSize = New System.Drawing.Size(362, 394)
            Me.Name = "FormPrepararData"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Preparación de Data"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents PublicarButton As System.Windows.Forms.Button
        Friend WithEvents CruzarButton As System.Windows.Forms.Button
        Friend WithEvents fechaProcesoDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents EnviarCorreoButton As System.Windows.Forms.Button
        Friend WithEvents CierreReportViewer As Microsoft.Reporting.WinForms.ReportViewer
    End Class
End Namespace