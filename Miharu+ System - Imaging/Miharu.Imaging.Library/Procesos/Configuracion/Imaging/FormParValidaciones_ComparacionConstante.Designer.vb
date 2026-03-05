Namespace Procesos.Configuracion.Imaging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParValidaciones_ComparacionConstante
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormParValidaciones_ComparacionConstante))
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.MainGroupBox = New System.Windows.Forms.GroupBox()
            Me.ValorTextBox = New System.Windows.Forms.TextBox()
            Me.OperadorComboBox = New System.Windows.Forms.ComboBox()
            Me.OperadorLabel = New System.Windows.Forms.Label()
            Me.ValorLabel = New System.Windows.Forms.Label()
            Me.Campo1ComboBox = New System.Windows.Forms.ComboBox()
            Me.Campo1Label = New System.Windows.Forms.Label()
            Me.MainGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CancelarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.CancelarVen
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(339, 123)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(88, 32)
            Me.CancelarButton.TabIndex = 2
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.AceptarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(247, 123)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(86, 32)
            Me.AceptarButton.TabIndex = 1
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'MainGroupBox
            '
            Me.MainGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                             Or System.Windows.Forms.AnchorStyles.Left) _
                                            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MainGroupBox.Controls.Add(Me.ValorTextBox)
            Me.MainGroupBox.Controls.Add(Me.OperadorComboBox)
            Me.MainGroupBox.Controls.Add(Me.OperadorLabel)
            Me.MainGroupBox.Controls.Add(Me.ValorLabel)
            Me.MainGroupBox.Controls.Add(Me.Campo1ComboBox)
            Me.MainGroupBox.Controls.Add(Me.Campo1Label)
            Me.MainGroupBox.Location = New System.Drawing.Point(12, 12)
            Me.MainGroupBox.Name = "MainGroupBox"
            Me.MainGroupBox.Size = New System.Drawing.Size(415, 105)
            Me.MainGroupBox.TabIndex = 0
            Me.MainGroupBox.TabStop = False
            '
            'ValorTextBox
            '
            Me.ValorTextBox.Location = New System.Drawing.Point(87, 47)
            Me.ValorTextBox.Name = "ValorTextBox"
            Me.ValorTextBox.Size = New System.Drawing.Size(310, 20)
            Me.ValorTextBox.TabIndex = 3
            '
            'OperadorComboBox
            '
            Me.OperadorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OperadorComboBox.FormattingEnabled = True
            Me.OperadorComboBox.Items.AddRange(New Object() {"==", "<", "<=", ">", ">=", "<>"})
            Me.OperadorComboBox.Location = New System.Drawing.Point(87, 71)
            Me.OperadorComboBox.Name = "OperadorComboBox"
            Me.OperadorComboBox.Size = New System.Drawing.Size(82, 21)
            Me.OperadorComboBox.TabIndex = 5
            '
            'OperadorLabel
            '
            Me.OperadorLabel.AutoSize = True
            Me.OperadorLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OperadorLabel.Location = New System.Drawing.Point(18, 74)
            Me.OperadorLabel.Name = "OperadorLabel"
            Me.OperadorLabel.Size = New System.Drawing.Size(59, 13)
            Me.OperadorLabel.TabIndex = 4
            Me.OperadorLabel.Text = "Operador"
            '
            'ValorLabel
            '
            Me.ValorLabel.AutoSize = True
            Me.ValorLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValorLabel.Location = New System.Drawing.Point(18, 47)
            Me.ValorLabel.Name = "ValorLabel"
            Me.ValorLabel.Size = New System.Drawing.Size(36, 13)
            Me.ValorLabel.TabIndex = 2
            Me.ValorLabel.Text = "Valor"
            '
            'Campo1ComboBox
            '
            Me.Campo1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Campo1ComboBox.FormattingEnabled = True
            Me.Campo1ComboBox.Location = New System.Drawing.Point(87, 17)
            Me.Campo1ComboBox.Name = "Campo1ComboBox"
            Me.Campo1ComboBox.Size = New System.Drawing.Size(310, 21)
            Me.Campo1ComboBox.TabIndex = 1
            '
            'Campo1Label
            '
            Me.Campo1Label.AutoSize = True
            Me.Campo1Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Campo1Label.Location = New System.Drawing.Point(18, 20)
            Me.Campo1Label.Name = "Campo1Label"
            Me.Campo1Label.Size = New System.Drawing.Size(45, 13)
            Me.Campo1Label.TabIndex = 0
            Me.Campo1Label.Text = "Campo"
            '
            'FormParValidaciones_ComparacionConstante
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(439, 167)
            Me.Controls.Add(Me.MainGroupBox)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormParValidaciones_ComparacionConstante"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Campo"
            Me.MainGroupBox.ResumeLayout(False)
            Me.MainGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents OperadorComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents OperadorLabel As System.Windows.Forms.Label
        Friend WithEvents ValorLabel As System.Windows.Forms.Label
        Friend WithEvents Campo1ComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Campo1Label As System.Windows.Forms.Label
        Friend WithEvents ValorTextBox As System.Windows.Forms.TextBox
    End Class
End Namespace