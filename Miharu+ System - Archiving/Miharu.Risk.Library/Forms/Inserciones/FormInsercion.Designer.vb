Namespace Forms.Inserciones
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormInsercion
        Inherits Miharu.Desktop.Library.FormBase

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormInsercion))
            Me.TipologiaLabel = New System.Windows.Forms.Label()
            Me.FoliosLabel = New System.Windows.Forms.Label()
            Me.FoliosTextBox = New System.Windows.Forms.TextBox()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'TipologiaLabel
            '
            Me.TipologiaLabel.AutoSize = True
            Me.TipologiaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TipologiaLabel.Location = New System.Drawing.Point(15, 13)
            Me.TipologiaLabel.Name = "TipologiaLabel"
            Me.TipologiaLabel.Size = New System.Drawing.Size(59, 13)
            Me.TipologiaLabel.TabIndex = 0
            Me.TipologiaLabel.Text = "Tipologia"
            '
            'FoliosLabel
            '
            Me.FoliosLabel.AutoSize = True
            Me.FoliosLabel.Location = New System.Drawing.Point(47, 52)
            Me.FoliosLabel.Name = "FoliosLabel"
            Me.FoliosLabel.Size = New System.Drawing.Size(39, 13)
            Me.FoliosLabel.TabIndex = 1
            Me.FoliosLabel.Text = "Folios"
            '
            'FoliosTextBox
            '
            Me.FoliosTextBox.Location = New System.Drawing.Point(92, 49)
            Me.FoliosTextBox.MaxLength = 4
            Me.FoliosTextBox.Name = "FoliosTextBox"
            Me.FoliosTextBox.Size = New System.Drawing.Size(116, 21)
            Me.FoliosTextBox.TabIndex = 0
            Me.FoliosTextBox.Text = "1"
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(254, 49)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(99, 29)
            Me.CancelarButton.TabIndex = 2
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(254, 13)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(99, 29)
            Me.AceptarButton.TabIndex = 1
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'FormInsercion
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(372, 91)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.FoliosTextBox)
            Me.Controls.Add(Me.FoliosLabel)
            Me.Controls.Add(Me.TipologiaLabel)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormInsercion"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Inserciones"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents TipologiaLabel As System.Windows.Forms.Label
        Friend WithEvents FoliosLabel As System.Windows.Forms.Label
        Friend WithEvents FoliosTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
    End Class
End Namespace