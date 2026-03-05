Namespace Forms.Recepcion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormRemisiones
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRemisiones))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.RemisionesListBox = New System.Windows.Forms.ListBox()
            Me.RecibirButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                          Or System.Windows.Forms.AnchorStyles.Left) _
                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.RemisionesListBox)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(267, 212)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Remisiones Pendientes"
            '
            'RemisionesListBox
            '
            Me.RemisionesListBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                  Or System.Windows.Forms.AnchorStyles.Left) _
                                                 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RemisionesListBox.FormattingEnabled = True
            Me.RemisionesListBox.Location = New System.Drawing.Point(6, 17)
            Me.RemisionesListBox.Name = "RemisionesListBox"
            Me.RemisionesListBox.Size = New System.Drawing.Size(255, 186)
            Me.RemisionesListBox.TabIndex = 0
            '
            'RecibirButton
            '
            Me.RecibirButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnAgregar
            Me.RecibirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.RecibirButton.Location = New System.Drawing.Point(134, 230)
            Me.RecibirButton.Name = "RecibirButton"
            Me.RecibirButton.Size = New System.Drawing.Size(72, 23)
            Me.RecibirButton.TabIndex = 1
            Me.RecibirButton.Text = "&Recibir"
            Me.RecibirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.RecibirButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.BackgroundImage = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.CerrarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(212, 230)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(67, 23)
            Me.CerrarButton.TabIndex = 2
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormRemisiones
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(292, 256)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.RecibirButton)
            Me.Controls.Add(Me.GroupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "FormRemisiones"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Recepción de Remisiones"
            Me.GroupBox1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents RemisionesListBox As System.Windows.Forms.ListBox
        Friend WithEvents RecibirButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
    End Class
End Namespace