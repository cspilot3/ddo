Namespace Forms.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeleccionarEsquema
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
            Me.EsquemaComboBox = New System.Windows.Forms.ComboBox()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'EsquemaComboBox
            '
            Me.EsquemaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaComboBox.FormattingEnabled = True
            Me.EsquemaComboBox.Location = New System.Drawing.Point(12, 25)
            Me.EsquemaComboBox.Name = "EsquemaComboBox"
            Me.EsquemaComboBox.Size = New System.Drawing.Size(299, 21)
            Me.EsquemaComboBox.TabIndex = 0
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(106, 64)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(100, 30)
            Me.AceptarButton.TabIndex = 1
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(212, 64)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(100, 30)
            Me.CerrarButton.TabIndex = 2
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(9, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(64, 13)
            Me.Label1.TabIndex = 29
            Me.Label1.Text = "Esquemas"
            '
            'FormSeleccionarEsquema
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(324, 106)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.EsquemaComboBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormSeleccionarEsquema"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Seleccionar Esquema"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents EsquemaComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace