
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace View.Indexacion

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ListInputControl
        Inherits InputControl

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
            Me.CapturaOldTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            Me.ValueOld1DesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.ValueOld2DesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.ValueDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.CapturaOldPanel = New System.Windows.Forms.Panel()
            Me.CapturaValidacionListasPanel = New System.Windows.Forms.Panel()
            Me.CapturaValidacionListasTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            Me.ValueValidacionListasDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.CapturaOldTableLayoutPanel.SuspendLayout()
            Me.CapturaOldPanel.SuspendLayout()
            Me.CapturaValidacionListasPanel.SuspendLayout()
            Me.CapturaValidacionListasTableLayoutPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'EtiquetaLabel
            '
            Me.EtiquetaLabel.Location = New System.Drawing.Point(5, 5)
            Me.EtiquetaLabel.Size = New System.Drawing.Size(270, 16)
            '
            'CapturaOldTableLayoutPanel
            '
            Me.CapturaOldTableLayoutPanel.AutoSize = True
            Me.CapturaOldTableLayoutPanel.ColumnCount = 1
            Me.CapturaOldTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.CapturaOldTableLayoutPanel.Controls.Add(Me.ValueOld1DesktopComboBox, 0, 0)
            Me.CapturaOldTableLayoutPanel.Controls.Add(Me.ValueOld2DesktopComboBox, 0, 2)
            Me.CapturaOldTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CapturaOldTableLayoutPanel.Location = New System.Drawing.Point(0, 0)
            Me.CapturaOldTableLayoutPanel.Margin = New System.Windows.Forms.Padding(0)
            Me.CapturaOldTableLayoutPanel.Name = "CapturaOldTableLayoutPanel"
            Me.CapturaOldTableLayoutPanel.RowCount = 3
            Me.CapturaOldTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.CapturaOldTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
            Me.CapturaOldTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.CapturaOldTableLayoutPanel.Size = New System.Drawing.Size(270, 62)
            Me.CapturaOldTableLayoutPanel.TabIndex = 4
            '
            'ValueOld1DesktopComboBox
            '
            Me.ValueOld1DesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ValueOld1DesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ValueOld1DesktopComboBox.DisabledEnter = False
            Me.ValueOld1DesktopComboBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.ValueOld1DesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ValueOld1DesktopComboBox.Enabled = False
            Me.ValueOld1DesktopComboBox.fk_Campo = 0
            Me.ValueOld1DesktopComboBox.fk_Documento = 0
            Me.ValueOld1DesktopComboBox.fk_Validacion = 0
            Me.ValueOld1DesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ValueOld1DesktopComboBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueOld1DesktopComboBox.FormattingEnabled = True
            Me.ValueOld1DesktopComboBox.Location = New System.Drawing.Point(3, 3)
            Me.ValueOld1DesktopComboBox.Name = "ValueOld1DesktopComboBox"
            Me.ValueOld1DesktopComboBox.Size = New System.Drawing.Size(264, 22)
            Me.ValueOld1DesktopComboBox.TabIndex = 8
            Me.ValueOld1DesktopComboBox.TabStop = False
            '
            'ValueOld2DesktopComboBox
            '
            Me.ValueOld2DesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ValueOld2DesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ValueOld2DesktopComboBox.DisabledEnter = False
            Me.ValueOld2DesktopComboBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.ValueOld2DesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ValueOld2DesktopComboBox.Enabled = False
            Me.ValueOld2DesktopComboBox.fk_Campo = 0
            Me.ValueOld2DesktopComboBox.fk_Documento = 0
            Me.ValueOld2DesktopComboBox.fk_Validacion = 0
            Me.ValueOld2DesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ValueOld2DesktopComboBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueOld2DesktopComboBox.FormattingEnabled = True
            Me.ValueOld2DesktopComboBox.Location = New System.Drawing.Point(3, 36)
            Me.ValueOld2DesktopComboBox.Name = "ValueOld2DesktopComboBox"
            Me.ValueOld2DesktopComboBox.Size = New System.Drawing.Size(264, 22)
            Me.ValueOld2DesktopComboBox.TabIndex = 7
            Me.ValueOld2DesktopComboBox.TabStop = False
            '
            'ValueDesktopComboBox
            '
            Me.ValueDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ValueDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ValueDesktopComboBox.DisabledEnter = False
            Me.ValueDesktopComboBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.ValueDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ValueDesktopComboBox.fk_Campo = 0
            Me.ValueDesktopComboBox.fk_Documento = 0
            Me.ValueDesktopComboBox.fk_Validacion = 0
            Me.ValueDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ValueDesktopComboBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueDesktopComboBox.FormattingEnabled = True
            Me.ValueDesktopComboBox.Location = New System.Drawing.Point(5, 83)
            Me.ValueDesktopComboBox.Name = "ValueDesktopComboBox"
            Me.ValueDesktopComboBox.Size = New System.Drawing.Size(270, 22)
            Me.ValueDesktopComboBox.TabIndex = 0
            '
            'CapturaOldPanel
            '
            Me.CapturaOldPanel.AutoSize = True
            Me.CapturaOldPanel.Controls.Add(Me.CapturaOldTableLayoutPanel)
            Me.CapturaOldPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.CapturaOldPanel.Location = New System.Drawing.Point(5, 21)
            Me.CapturaOldPanel.Name = "CapturaOldPanel"
            Me.CapturaOldPanel.Size = New System.Drawing.Size(270, 62)
            Me.CapturaOldPanel.TabIndex = 6
            '
            'CapturaValidacionListasPanel
            '
            Me.CapturaValidacionListasPanel.AutoSize = True
            Me.CapturaValidacionListasPanel.Controls.Add(Me.CapturaValidacionListasTableLayoutPanel)
            Me.CapturaValidacionListasPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.CapturaValidacionListasPanel.Location = New System.Drawing.Point(5, 105)
            Me.CapturaValidacionListasPanel.Name = "CapturaValidacionListasPanel"
            Me.CapturaValidacionListasPanel.Size = New System.Drawing.Size(270, 28)
            Me.CapturaValidacionListasPanel.TabIndex = 7
            '
            'CapturaValidacionListasTableLayoutPanel
            '
            Me.CapturaValidacionListasTableLayoutPanel.AutoSize = True
            Me.CapturaValidacionListasTableLayoutPanel.ColumnCount = 1
            Me.CapturaValidacionListasTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.CapturaValidacionListasTableLayoutPanel.Controls.Add(Me.ValueValidacionListasDesktopComboBox, 0, 0)
            Me.CapturaValidacionListasTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CapturaValidacionListasTableLayoutPanel.Location = New System.Drawing.Point(0, 0)
            Me.CapturaValidacionListasTableLayoutPanel.Name = "CapturaValidacionListasTableLayoutPanel"
            Me.CapturaValidacionListasTableLayoutPanel.RowCount = 1
            Me.CapturaValidacionListasTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.CapturaValidacionListasTableLayoutPanel.Size = New System.Drawing.Size(270, 28)
            Me.CapturaValidacionListasTableLayoutPanel.TabIndex = 0
            '
            'ValueValidacionListasDesktopComboBox
            '
            Me.ValueValidacionListasDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ValueValidacionListasDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ValueValidacionListasDesktopComboBox.DisabledEnter = False
            Me.ValueValidacionListasDesktopComboBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.ValueValidacionListasDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ValueValidacionListasDesktopComboBox.Enabled = False
            Me.ValueValidacionListasDesktopComboBox.fk_Campo = 0
            Me.ValueValidacionListasDesktopComboBox.fk_Documento = 0
            Me.ValueValidacionListasDesktopComboBox.fk_Validacion = 0
            Me.ValueValidacionListasDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ValueValidacionListasDesktopComboBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueValidacionListasDesktopComboBox.FormattingEnabled = True
            Me.ValueValidacionListasDesktopComboBox.Location = New System.Drawing.Point(3, 3)
            Me.ValueValidacionListasDesktopComboBox.Name = "ValueValidacionListasDesktopComboBox"
            Me.ValueValidacionListasDesktopComboBox.Size = New System.Drawing.Size(264, 22)
            Me.ValueValidacionListasDesktopComboBox.TabIndex = 9
            Me.ValueValidacionListasDesktopComboBox.TabStop = False
            '
            'ListInputControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.Controls.Add(Me.CapturaValidacionListasPanel)
            Me.Controls.Add(Me.ValueDesktopComboBox)
            Me.Controls.Add(Me.CapturaOldPanel)
            Me.Name = "ListInputControl"
            Me.Padding = New System.Windows.Forms.Padding(5, 5, 5, 10)
            Me.Size = New System.Drawing.Size(280, 146)
            Me.Controls.SetChildIndex(Me.EtiquetaLabel, 0)
            Me.Controls.SetChildIndex(Me.CapturaOldPanel, 0)
            Me.Controls.SetChildIndex(Me.ValueDesktopComboBox, 0)
            Me.Controls.SetChildIndex(Me.CapturaValidacionListasPanel, 0)
            Me.CapturaOldTableLayoutPanel.ResumeLayout(False)
            Me.CapturaOldPanel.ResumeLayout(False)
            Me.CapturaOldPanel.PerformLayout()
            Me.CapturaValidacionListasPanel.ResumeLayout(False)
            Me.CapturaValidacionListasPanel.PerformLayout()
            Me.CapturaValidacionListasTableLayoutPanel.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents CapturaOldTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents CapturaOldPanel As System.Windows.Forms.Panel
        Public WithEvents ValueDesktopComboBox As DesktopComboBoxControl
        Public WithEvents ValueOld1DesktopComboBox As DesktopComboBoxControl
        Public WithEvents ValueOld2DesktopComboBox As DesktopComboBoxControl
        Friend WithEvents CapturaValidacionListasPanel As System.Windows.Forms.Panel
        Friend WithEvents CapturaValidacionListasTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
        Public WithEvents ValueValidacionListasDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl

    End Class

End Namespace