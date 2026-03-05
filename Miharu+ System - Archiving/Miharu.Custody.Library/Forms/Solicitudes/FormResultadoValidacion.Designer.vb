Namespace Forms.Solicitudes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormResultadoValidacion
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormResultadoValidacion))
            Me.ResultadoGroupBox = New System.Windows.Forms.GroupBox()
            Me.ResultadosRichTextBox = New System.Windows.Forms.RichTextBox()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.TituloLabel = New System.Windows.Forms.Label()
            Me.CopiarButton = New System.Windows.Forms.Button()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.ResultadoGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'ResultadoGroupBox
            '
            Me.ResultadoGroupBox.Controls.Add(Me.ResultadosRichTextBox)
            Me.ResultadoGroupBox.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ResultadoGroupBox.Location = New System.Drawing.Point(12, 39)
            Me.ResultadoGroupBox.Name = "ResultadoGroupBox"
            Me.ResultadoGroupBox.Size = New System.Drawing.Size(649, 284)
            Me.ResultadoGroupBox.TabIndex = 0
            Me.ResultadoGroupBox.TabStop = False
            Me.ResultadoGroupBox.Text = "Resultados de Validación"
            '
            'ResultadosRichTextBox
            '
            Me.ResultadosRichTextBox.Location = New System.Drawing.Point(7, 22)
            Me.ResultadosRichTextBox.Name = "ResultadosRichTextBox"
            Me.ResultadosRichTextBox.ReadOnly = True
            Me.ResultadosRichTextBox.Size = New System.Drawing.Size(636, 256)
            Me.ResultadosRichTextBox.TabIndex = 0
            Me.ResultadosRichTextBox.Text = ""
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.FlatAppearance.BorderSize = 0
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(591, 330)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(70, 27)
            Me.CerrarButton.TabIndex = 1
            Me.CerrarButton.Text = "Ce&rrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'TituloLabel
            '
            Me.TituloLabel.AutoSize = True
            Me.TituloLabel.Location = New System.Drawing.Point(9, 9)
            Me.TituloLabel.Name = "TituloLabel"
            Me.TituloLabel.Size = New System.Drawing.Size(354, 13)
            Me.TituloLabel.TabIndex = 2
            Me.TituloLabel.Text = "Se generarón los siguientes errores, por favor revise el archivo de cargue."
            '
            'CopiarButton
            '
            Me.CopiarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CopiarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.MainCharge
            Me.CopiarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CopiarButton.Location = New System.Drawing.Point(515, 330)
            Me.CopiarButton.Name = "CopiarButton"
            Me.CopiarButton.Size = New System.Drawing.Size(70, 27)
            Me.CopiarButton.TabIndex = 3
            Me.CopiarButton.Text = "&Copiar"
            Me.CopiarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CopiarButton.UseVisualStyleBackColor = True
            '
            'GuardarButton
            '
            Me.GuardarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GuardarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSave
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(433, 330)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(76, 27)
            Me.GuardarButton.TabIndex = 4
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'FormResultadoValidacion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(674, 363)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CopiarButton)
            Me.Controls.Add(Me.TituloLabel)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.ResultadoGroupBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormResultadoValidacion"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Resultados de validación"
            Me.ResultadoGroupBox.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ResultadoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents TituloLabel As System.Windows.Forms.Label
        Friend WithEvents CopiarButton As System.Windows.Forms.Button
        Friend WithEvents ResultadosRichTextBox As System.Windows.Forms.RichTextBox
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
    End Class
End Namespace