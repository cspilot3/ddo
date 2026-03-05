Namespace DesktopValidacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class DesktopValidacionControl
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
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
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.CaptionLabel = New System.Windows.Forms.Label()
            Me.MotivoComboBox = New System.Windows.Forms.ComboBox()
            Me.SeleccionComboBox = New System.Windows.Forms.ComboBox()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.CaptionLabel)
            Me.Panel1.Controls.Add(Me.MotivoComboBox)
            Me.Panel1.Controls.Add(Me.SeleccionComboBox)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(349, 29)
            Me.Panel1.TabIndex = 5
            '
            'CaptionLabel
            '
            Me.CaptionLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.CaptionLabel.AutoSize = True
            Me.CaptionLabel.Location = New System.Drawing.Point(5, 7)
            Me.CaptionLabel.Name = "CaptionLabel"
            Me.CaptionLabel.Size = New System.Drawing.Size(13, 13)
            Me.CaptionLabel.TabIndex = 2
            Me.CaptionLabel.Text = "_"
            '
            'MotivoComboBox
            '
            Me.MotivoComboBox.AllowDrop = True
            Me.MotivoComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MotivoComboBox.DropDownHeight = 100
            Me.MotivoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.MotivoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.MotivoComboBox.FormattingEnabled = True
            Me.MotivoComboBox.IntegralHeight = False
            Me.MotivoComboBox.Location = New System.Drawing.Point(203, 4)
            Me.MotivoComboBox.Name = "MotivoComboBox"
            Me.MotivoComboBox.Size = New System.Drawing.Size(142, 21)
            Me.MotivoComboBox.TabIndex = 3
            '
            'SeleccionComboBox
            '
            Me.SeleccionComboBox.AllowDrop = True
            Me.SeleccionComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.SeleccionComboBox.DropDownHeight = 100
            Me.SeleccionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SeleccionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SeleccionComboBox.FormattingEnabled = True
            Me.SeleccionComboBox.IntegralHeight = False
            Me.SeleccionComboBox.Location = New System.Drawing.Point(151, 3)
            Me.SeleccionComboBox.Name = "SeleccionComboBox"
            Me.SeleccionComboBox.Size = New System.Drawing.Size(46, 21)
            Me.SeleccionComboBox.TabIndex = 0
            '
            'DesktopValidacion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.Panel1)
            Me.Name = "DesktopValidacion"
            Me.Size = New System.Drawing.Size(349, 29)
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents CaptionLabel As System.Windows.Forms.Label
        Friend WithEvents MotivoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents SeleccionComboBox As System.Windows.Forms.ComboBox

    End Class
End Namespace