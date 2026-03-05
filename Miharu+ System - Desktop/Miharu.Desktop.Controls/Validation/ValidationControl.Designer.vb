
Namespace Validation
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ValidationControl
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
            Me.components = New System.ComponentModel.Container()
            Me.ValidacionLabel = New System.Windows.Forms.Label()
            Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.MotivoDesktopComboBox = New DesktopComboBox.DesktopComboBoxControl()
            Me.ValidacionDesktopComboBox = New DesktopComboBox.DesktopComboBoxControl()
            Me.SuspendLayout()
            '
            'ValidacionLabel
            '
            Me.ValidacionLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.ValidacionLabel.AutoEllipsis = True
            Me.ValidacionLabel.AutoSize = True
            Me.ValidacionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValidacionLabel.Location = New System.Drawing.Point(49, 9)
            Me.ValidacionLabel.Name = "ValidacionLabel"
            Me.ValidacionLabel.Size = New System.Drawing.Size(83, 13)
            Me.ValidacionLabel.TabIndex = 2
            Me.ValidacionLabel.Text = "TextoValidacion"
            '
            'ToolTip
            '
            Me.ToolTip.AutomaticDelay = 200
            Me.ToolTip.AutoPopDelay = 2000
            Me.ToolTip.InitialDelay = 200
            Me.ToolTip.ReshowDelay = 15
            Me.ToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
            Me.ToolTip.ToolTipTitle = "Documento / Transacción"
            Me.ToolTip.UseAnimation = False
            '
            'MotivoDesktopComboBox
            '
            Me.MotivoDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.MotivoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.MotivoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.MotivoDesktopComboBox.DisabledEnter = False
            Me.MotivoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.MotivoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.MotivoDesktopComboBox.FormattingEnabled = True
            Me.MotivoDesktopComboBox.Location = New System.Drawing.Point(216, 5)
            Me.MotivoDesktopComboBox.Name = "MotivoDesktopComboBox"
            Me.MotivoDesktopComboBox.Size = New System.Drawing.Size(38, 21)
            Me.MotivoDesktopComboBox.TabIndex = 3
            '
            'ValidacionDesktopComboBox
            '
            Me.ValidacionDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ValidacionDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ValidacionDesktopComboBox.DisabledEnter = False
            Me.ValidacionDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ValidacionDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ValidacionDesktopComboBox.FormattingEnabled = True
            Me.ValidacionDesktopComboBox.Location = New System.Drawing.Point(8, 5)
            Me.ValidacionDesktopComboBox.Name = "ValidacionDesktopComboBox"
            Me.ValidacionDesktopComboBox.Size = New System.Drawing.Size(40, 21)
            Me.ValidacionDesktopComboBox.TabIndex = 1
            '
            'ValidationControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Controls.Add(Me.MotivoDesktopComboBox)
            Me.Controls.Add(Me.ValidacionLabel)
            Me.Controls.Add(Me.ValidacionDesktopComboBox)
            Me.Margin = New System.Windows.Forms.Padding(0)
            Me.Name = "ValidationControl"
            Me.Padding = New System.Windows.Forms.Padding(1)
            Me.Size = New System.Drawing.Size(260, 30)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ValidacionDesktopComboBox As DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents ValidacionLabel As System.Windows.Forms.Label
        Friend WithEvents MotivoDesktopComboBox As DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents ToolTip As System.Windows.Forms.ToolTip

    End Class
End Namespace