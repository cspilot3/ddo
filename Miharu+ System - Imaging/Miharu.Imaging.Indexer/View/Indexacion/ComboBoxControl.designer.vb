Namespace View.Indexacion

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ComboBoxControl
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
            Me.Table_UpPanel = New System.Windows.Forms.Panel()
            Me.Table_CloseButton = New System.Windows.Forms.Button()
            Me.FindListBox = New System.Windows.Forms.ListBox()
            Me.FindTextBox = New System.Windows.Forms.TextBox()
            Me.Table_UpPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'Table_UpPanel
            '
            Me.Table_UpPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
            Me.Table_UpPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Table_UpPanel.Controls.Add(Me.Table_CloseButton)
            Me.Table_UpPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.Table_UpPanel.Location = New System.Drawing.Point(0, 0)
            Me.Table_UpPanel.Name = "Table_UpPanel"
            Me.Table_UpPanel.Size = New System.Drawing.Size(365, 23)
            Me.Table_UpPanel.TabIndex = 0
            '
            'Table_CloseButton
            '
            Me.Table_CloseButton.Dock = System.Windows.Forms.DockStyle.Right
            Me.Table_CloseButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.cancel
            Me.Table_CloseButton.Location = New System.Drawing.Point(340, 0)
            Me.Table_CloseButton.Name = "Table_CloseButton"
            Me.Table_CloseButton.Size = New System.Drawing.Size(23, 21)
            Me.Table_CloseButton.TabIndex = 0
            Me.Table_CloseButton.UseVisualStyleBackColor = True
            '
            'FindListBox
            '
            Me.FindListBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.FindListBox.FormattingEnabled = True
            Me.FindListBox.Location = New System.Drawing.Point(0, 43)
            Me.FindListBox.Name = "FindListBox"
            Me.FindListBox.Size = New System.Drawing.Size(365, 188)
            Me.FindListBox.TabIndex = 1
            '
            'FindTextBox
            '
            Me.FindTextBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.FindTextBox.Location = New System.Drawing.Point(0, 23)
            Me.FindTextBox.Name = "FindTextBox"
            Me.FindTextBox.Size = New System.Drawing.Size(365, 20)
            Me.FindTextBox.TabIndex = 0
            '
            'ComboBoxControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Controls.Add(Me.FindListBox)
            Me.Controls.Add(Me.FindTextBox)
            Me.Controls.Add(Me.Table_UpPanel)
            Me.Name = "ComboBoxControl"
            Me.Size = New System.Drawing.Size(365, 231)
            Me.Table_UpPanel.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Table_UpPanel As System.Windows.Forms.Panel
        Friend WithEvents FindListBox As System.Windows.Forms.ListBox
        Friend WithEvents FindTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Table_CloseButton As System.Windows.Forms.Button

    End Class
End Namespace