<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAyuda
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAyuda))
        Me.GroupBox = New System.Windows.Forms.GroupBox()
        Me.ComandosRichTextBox = New System.Windows.Forms.RichTextBox()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.CerrarButton = New System.Windows.Forms.Button()
        Me.GroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox
        '
        Me.GroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox.Controls.Add(Me.CerrarButton)
        Me.GroupBox.Controls.Add(Me.ComandosRichTextBox)
        Me.GroupBox.Location = New System.Drawing.Point(5, 0)
        Me.GroupBox.Name = "GroupBox"
        Me.GroupBox.Size = New System.Drawing.Size(527, 260)
        Me.GroupBox.TabIndex = 0
        Me.GroupBox.TabStop = False
        '
        'ComandosRichTextBox
        '
        Me.ComandosRichTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComandosRichTextBox.BackColor = System.Drawing.Color.White
        Me.ComandosRichTextBox.Location = New System.Drawing.Point(7, 12)
        Me.ComandosRichTextBox.Name = "ComandosRichTextBox"
        Me.ComandosRichTextBox.ReadOnly = True
        Me.ComandosRichTextBox.Size = New System.Drawing.Size(513, 207)
        Me.ComandosRichTextBox.TabIndex = 2
        Me.ComandosRichTextBox.Text = ""
        '
        'CerrarButton
        '
        Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CerrarButton.FlatAppearance.BorderSize = 0
        Me.CerrarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CerrarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.Close_Red
        Me.CerrarButton.Location = New System.Drawing.Point(489, 224)
        Me.CerrarButton.Name = "CerrarButton"
        Me.CerrarButton.Size = New System.Drawing.Size(33, 33)
        Me.CerrarButton.TabIndex = 3
        Me.ToolTip.SetToolTip(Me.CerrarButton, "Cerrar Ventana")
        Me.CerrarButton.UseVisualStyleBackColor = True
        '
        'FormAyuda
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CerrarButton
        Me.ClientSize = New System.Drawing.Size(537, 263)
        Me.Controls.Add(Me.GroupBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormAyuda"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ayuda"
        Me.GroupBox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents CerrarButton As System.Windows.Forms.Button
    Friend WithEvents ComandosRichTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
End Class
