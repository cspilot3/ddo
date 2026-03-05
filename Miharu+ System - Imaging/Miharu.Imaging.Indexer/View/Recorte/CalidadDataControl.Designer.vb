Namespace View.Recorte

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class CalidadDataControl
        Inherits System.Windows.Forms.UserControl

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
            Me.DataButton = New System.Windows.Forms.Button()
            Me.OKRadioButton = New System.Windows.Forms.RadioButton()
            Me.ErrorRadioButton = New System.Windows.Forms.RadioButton()
            Me.OptionPanel = New System.Windows.Forms.Panel()
            Me.OptionPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'DataButton
            '
            Me.DataButton.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DataButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DataButton.Location = New System.Drawing.Point(3, 3)
            Me.DataButton.Name = "DataButton"
            Me.DataButton.Size = New System.Drawing.Size(188, 26)
            Me.DataButton.TabIndex = 0
            Me.DataButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.DataButton.UseVisualStyleBackColor = True
            '
            'OKRadioButton
            '
            Me.OKRadioButton.Appearance = System.Windows.Forms.Appearance.Button
            Me.OKRadioButton.Dock = System.Windows.Forms.DockStyle.Right
            Me.OKRadioButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.tick
            Me.OKRadioButton.Location = New System.Drawing.Point(0, 0)
            Me.OKRadioButton.Name = "OKRadioButton"
            Me.OKRadioButton.Size = New System.Drawing.Size(26, 26)
            Me.OKRadioButton.TabIndex = 2
            Me.OKRadioButton.TabStop = True
            Me.OKRadioButton.UseVisualStyleBackColor = True
            '
            'ErrorRadioButton
            '
            Me.ErrorRadioButton.Appearance = System.Windows.Forms.Appearance.Button
            Me.ErrorRadioButton.Dock = System.Windows.Forms.DockStyle.Right
            Me.ErrorRadioButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.cancel
            Me.ErrorRadioButton.Location = New System.Drawing.Point(26, 0)
            Me.ErrorRadioButton.Name = "ErrorRadioButton"
            Me.ErrorRadioButton.Size = New System.Drawing.Size(26, 26)
            Me.ErrorRadioButton.TabIndex = 3
            Me.ErrorRadioButton.TabStop = True
            Me.ErrorRadioButton.UseVisualStyleBackColor = True
            '
            'OptionPanel
            '
            Me.OptionPanel.AutoSize = True
            Me.OptionPanel.Controls.Add(Me.OKRadioButton)
            Me.OptionPanel.Controls.Add(Me.ErrorRadioButton)
            Me.OptionPanel.Dock = System.Windows.Forms.DockStyle.Right
            Me.OptionPanel.Location = New System.Drawing.Point(191, 3)
            Me.OptionPanel.Name = "OptionPanel"
            Me.OptionPanel.Size = New System.Drawing.Size(52, 26)
            Me.OptionPanel.TabIndex = 4
            Me.OptionPanel.Visible = False
            '
            'CalidadDataControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.DataButton)
            Me.Controls.Add(Me.OptionPanel)
            Me.Name = "CalidadDataControl"
            Me.Padding = New System.Windows.Forms.Padding(3)
            Me.Size = New System.Drawing.Size(246, 32)
            Me.OptionPanel.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents DataButton As System.Windows.Forms.Button
        Friend WithEvents OKRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents ErrorRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents OptionPanel As System.Windows.Forms.Panel

    End Class

End Namespace