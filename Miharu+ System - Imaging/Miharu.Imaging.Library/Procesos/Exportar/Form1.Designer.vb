<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.BuscarCarpetaButton = New System.Windows.Forms.Button()
        Me.lblCarpeta = New System.Windows.Forms.Label()
        Me.CarpetaSalidaTextBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'BuscarCarpetaButton
        '
        Me.BuscarCarpetaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BuscarCarpetaButton.Location = New System.Drawing.Point(430, 89)
        Me.BuscarCarpetaButton.Name = "BuscarCarpetaButton"
        Me.BuscarCarpetaButton.Size = New System.Drawing.Size(77, 30)
        Me.BuscarCarpetaButton.TabIndex = 16
        Me.BuscarCarpetaButton.Text = "Cargar"
        Me.BuscarCarpetaButton.UseVisualStyleBackColor = True
        '
        'lblCarpeta
        '
        Me.lblCarpeta.AutoSize = True
        Me.lblCarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCarpeta.Location = New System.Drawing.Point(139, 76)
        Me.lblCarpeta.Name = "lblCarpeta"
        Me.lblCarpeta.Size = New System.Drawing.Size(86, 15)
        Me.lblCarpeta.TabIndex = 14
        Me.lblCarpeta.Text = "Leer archivo"
        '
        'CarpetaSalidaTextBox
        '
        Me.CarpetaSalidaTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CarpetaSalidaTextBox.Location = New System.Drawing.Point(21, 95)
        Me.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox"
        Me.CarpetaSalidaTextBox.Size = New System.Drawing.Size(381, 20)
        Me.CarpetaSalidaTextBox.TabIndex = 15
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(528, 194)
        Me.Controls.Add(Me.BuscarCarpetaButton)
        Me.Controls.Add(Me.lblCarpeta)
        Me.Controls.Add(Me.CarpetaSalidaTextBox)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BuscarCarpetaButton As System.Windows.Forms.Button
    Friend WithEvents lblCarpeta As System.Windows.Forms.Label
    Friend WithEvents CarpetaSalidaTextBox As System.Windows.Forms.TextBox
End Class
