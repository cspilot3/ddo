Namespace Forms.Reportes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormConfirmacionReporte
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
            Me.ResultadosDataGridView = New Miharu.Desktop.Controls.DesktopReportDataGridView.DesktopReportDataGridViewControl()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.ok = New System.Windows.Forms.Button()
            Me.cancel = New System.Windows.Forms.Button()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'ResultadosDataGridView
            '
            Me.ResultadosDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ResultadosDataGridView.Location = New System.Drawing.Point(0, 0)
            Me.ResultadosDataGridView.Name = "ResultadosDataGridView"
            Me.ResultadosDataGridView.Size = New System.Drawing.Size(537, 216)
            Me.ResultadosDataGridView.TabIndex = 0
            Me.ResultadosDataGridView.Titulo = "La consulta retorno los siguientes datos para confirmación. Desea Continuar?"
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.ok)
            Me.Panel1.Controls.Add(Me.cancel)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel1.Location = New System.Drawing.Point(0, 216)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(537, 49)
            Me.Panel1.TabIndex = 1
            '
            'ok
            '
            Me.ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ok.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ok.Image = Global.Miharu.Desktop.My.Resources.Resources.Aceptar
            Me.ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ok.Location = New System.Drawing.Point(344, 10)
            Me.ok.Name = "ok"
            Me.ok.Size = New System.Drawing.Size(91, 36)
            Me.ok.TabIndex = 7
            Me.ok.Text = "Aceptar"
            Me.ok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ok.UseVisualStyleBackColor = True
            '
            'cancel
            '
            Me.cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cancel.Image = Global.Miharu.Desktop.My.Resources.Resources.Cancelar
            Me.cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.cancel.Location = New System.Drawing.Point(441, 10)
            Me.cancel.Name = "cancel"
            Me.cancel.Size = New System.Drawing.Size(91, 36)
            Me.cancel.TabIndex = 8
            Me.cancel.Text = "Cancelar"
            Me.cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.cancel.UseVisualStyleBackColor = True
            '
            'FormConfirmacionReporte
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(537, 265)
            Me.Controls.Add(Me.ResultadosDataGridView)
            Me.Controls.Add(Me.Panel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
            Me.Name = "FormConfirmacionReporte"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Confirmacion Data Reporte"
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ResultadosDataGridView As Miharu.Desktop.Controls.DesktopReportDataGridView.DesktopReportDataGridViewControl
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents ok As System.Windows.Forms.Button
        Friend WithEvents cancel As System.Windows.Forms.Button
    End Class
End Namespace