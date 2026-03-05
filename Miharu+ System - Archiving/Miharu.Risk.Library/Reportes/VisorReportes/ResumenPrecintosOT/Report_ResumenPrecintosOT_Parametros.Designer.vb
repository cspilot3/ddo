Namespace Reportes.VisorReportes.ResumenPrecintosOT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Report_ResumenPrecintosOT_Parametros
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
            Me.BaseGroupBox = New System.Windows.Forms.GroupBox()
            Me.OTTextBox = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.BaseGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'BaseGroupBox
            '
            Me.BaseGroupBox.Controls.Add(Me.OTTextBox)
            Me.BaseGroupBox.Controls.Add(Me.Label2)
            Me.BaseGroupBox.Location = New System.Drawing.Point(12, 12)
            Me.BaseGroupBox.Name = "BaseGroupBox"
            Me.BaseGroupBox.Size = New System.Drawing.Size(260, 60)
            Me.BaseGroupBox.TabIndex = 23
            Me.BaseGroupBox.TabStop = False
            '
            'OTTextBox
            '
            Me.OTTextBox.Location = New System.Drawing.Point(92, 24)
            Me.OTTextBox.Name = "OTTextBox"
            Me.OTTextBox.Size = New System.Drawing.Size(162, 20)
            Me.OTTextBox.TabIndex = 4
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(22, 27)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(24, 13)
            Me.Label2.TabIndex = 3
            Me.Label2.Text = "OT"
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = My.Resources.Resources.Cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(192, 88)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 24)
            Me.CancelarButton.TabIndex = 22
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(93, 88)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 24)
            Me.AceptarButton.TabIndex = 21
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'Report_ParametrizacionProyectos_Parametros
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(284, 124)
            Me.ControlBox = False
            Me.Controls.Add(Me.BaseGroupBox)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "Report_ParametrizacionProyectos_Parametros"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Filtro"
            Me.BaseGroupBox.ResumeLayout(False)
            Me.BaseGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents BaseGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents OTTextBox As System.Windows.Forms.TextBox

    End Class
End Namespace