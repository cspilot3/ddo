
Imports Miharu.Desktop.Controls.DesktopNumericTextBox

Namespace View.Indexacion

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class TextInputNumericControl
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
            Me.CapturaOldPanel = New System.Windows.Forms.Panel()
            Me.CapturaOldTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            Me.ValueOld1DesktopTextBox = New Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl()
            Me.ValueOld2DesktopTextBox = New Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl()
            Me.ValueDesktopTextBox = New Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl()
            Me.CapturaValidacionListasPanel = New System.Windows.Forms.Panel()
            Me.CapturaValidacionListasTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            Me.ValueValidacionListasDesktopTextBox = New Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl()
            Me.CapturaOldPanel.SuspendLayout()
            Me.CapturaOldTableLayoutPanel.SuspendLayout()
            Me.CapturaValidacionListasPanel.SuspendLayout()
            Me.CapturaValidacionListasTableLayoutPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'EtiquetaLabel
            '
            Me.EtiquetaLabel.Location = New System.Drawing.Point(5, 5)
            Me.EtiquetaLabel.Size = New System.Drawing.Size(322, 16)
            '
            'CapturaOldPanel
            '
            Me.CapturaOldPanel.AutoSize = True
            Me.CapturaOldPanel.Controls.Add(Me.CapturaOldTableLayoutPanel)
            Me.CapturaOldPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.CapturaOldPanel.Location = New System.Drawing.Point(5, 21)
            Me.CapturaOldPanel.Name = "CapturaOldPanel"
            Me.CapturaOldPanel.Size = New System.Drawing.Size(322, 61)
            Me.CapturaOldPanel.TabIndex = 1
            '
            'CapturaOldTableLayoutPanel
            '
            Me.CapturaOldTableLayoutPanel.AutoSize = True
            Me.CapturaOldTableLayoutPanel.ColumnCount = 1
            Me.CapturaOldTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.CapturaOldTableLayoutPanel.Controls.Add(Me.ValueOld1DesktopTextBox, 0, 0)
            Me.CapturaOldTableLayoutPanel.Controls.Add(Me.ValueOld2DesktopTextBox, 0, 2)
            Me.CapturaOldTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CapturaOldTableLayoutPanel.Location = New System.Drawing.Point(0, 0)
            Me.CapturaOldTableLayoutPanel.Margin = New System.Windows.Forms.Padding(0)
            Me.CapturaOldTableLayoutPanel.Name = "CapturaOldTableLayoutPanel"
            Me.CapturaOldTableLayoutPanel.RowCount = 3
            Me.CapturaOldTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.CapturaOldTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
            Me.CapturaOldTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.CapturaOldTableLayoutPanel.Size = New System.Drawing.Size(322, 61)
            Me.CapturaOldTableLayoutPanel.TabIndex = 0
            '
            'ValueOld1DesktopTextBox
            '
            Me.ValueOld1DesktopTextBox.CantidadDecimales = CType(0, Short)
            Me.ValueOld1DesktopTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValueOld1DesktopTextBox.Enabled = False
            Me.ValueOld1DesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ValueOld1DesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ValueOld1DesktopTextBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueOld1DesktopTextBox.Location = New System.Drawing.Point(3, 3)
            Me.ValueOld1DesktopTextBox.MaxLength = 0
            Me.ValueOld1DesktopTextBox.MaxValue = 0.0R
            Me.ValueOld1DesktopTextBox.MinValue = 0.0R
            Me.ValueOld1DesktopTextBox.Name = "ValueOld1DesktopTextBox"
            Me.ValueOld1DesktopTextBox.ReadOnly = True
            Me.ValueOld1DesktopTextBox.Size = New System.Drawing.Size(316, 22)
            Me.ValueOld1DesktopTextBox.TabIndex = 0
            Me.ValueOld1DesktopTextBox.TabStop = False
            Me.ValueOld1DesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.ValueOld1DesktopTextBox.UsaDecimales = False
            Me.ValueOld1DesktopTextBox.UsaRango = False
            '
            'ValueOld2DesktopTextBox
            '
            Me.ValueOld2DesktopTextBox.CantidadDecimales = CType(0, Short)
            Me.ValueOld2DesktopTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValueOld2DesktopTextBox.Enabled = False
            Me.ValueOld2DesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ValueOld2DesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ValueOld2DesktopTextBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueOld2DesktopTextBox.Location = New System.Drawing.Point(3, 36)
            Me.ValueOld2DesktopTextBox.MaxLength = 0
            Me.ValueOld2DesktopTextBox.MaxValue = 0.0R
            Me.ValueOld2DesktopTextBox.MinValue = 0.0R
            Me.ValueOld2DesktopTextBox.Name = "ValueOld2DesktopTextBox"
            Me.ValueOld2DesktopTextBox.ReadOnly = True
            Me.ValueOld2DesktopTextBox.Size = New System.Drawing.Size(316, 22)
            Me.ValueOld2DesktopTextBox.TabIndex = 1
            Me.ValueOld2DesktopTextBox.TabStop = False
            Me.ValueOld2DesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.ValueOld2DesktopTextBox.UsaDecimales = False
            Me.ValueOld2DesktopTextBox.UsaRango = False
            '
            'ValueDesktopTextBox
            '
            Me.ValueDesktopTextBox.CantidadDecimales = CType(0, Short)
            Me.ValueDesktopTextBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.ValueDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ValueDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ValueDesktopTextBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueDesktopTextBox.Location = New System.Drawing.Point(5, 82)
            Me.ValueDesktopTextBox.MaxLength = 0
            Me.ValueDesktopTextBox.MaxValue = 0.0R
            Me.ValueDesktopTextBox.MinValue = 0.0R
            Me.ValueDesktopTextBox.Name = "ValueDesktopTextBox"
            Me.ValueDesktopTextBox.Size = New System.Drawing.Size(322, 22)
            Me.ValueDesktopTextBox.TabIndex = 0
            Me.ValueDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.ValueDesktopTextBox.UsaDecimales = False
            Me.ValueDesktopTextBox.UsaRango = False
            '
            'CapturaValidacionListasPanel
            '
            Me.CapturaValidacionListasPanel.AutoSize = True
            Me.CapturaValidacionListasPanel.Controls.Add(Me.CapturaValidacionListasTableLayoutPanel)
            Me.CapturaValidacionListasPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.CapturaValidacionListasPanel.Location = New System.Drawing.Point(5, 104)
            Me.CapturaValidacionListasPanel.Name = "CapturaValidacionListasPanel"
            Me.CapturaValidacionListasPanel.Size = New System.Drawing.Size(322, 28)
            Me.CapturaValidacionListasPanel.TabIndex = 5
            '
            'CapturaValidacionListasTableLayoutPanel
            '
            Me.CapturaValidacionListasTableLayoutPanel.AutoSize = True
            Me.CapturaValidacionListasTableLayoutPanel.ColumnCount = 1
            Me.CapturaValidacionListasTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.CapturaValidacionListasTableLayoutPanel.Controls.Add(Me.ValueValidacionListasDesktopTextBox, 0, 0)
            Me.CapturaValidacionListasTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CapturaValidacionListasTableLayoutPanel.Location = New System.Drawing.Point(0, 0)
            Me.CapturaValidacionListasTableLayoutPanel.Name = "CapturaValidacionListasTableLayoutPanel"
            Me.CapturaValidacionListasTableLayoutPanel.RowCount = 1
            Me.CapturaValidacionListasTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.CapturaValidacionListasTableLayoutPanel.Size = New System.Drawing.Size(322, 28)
            Me.CapturaValidacionListasTableLayoutPanel.TabIndex = 0
            '
            'ValueValidacionListasDesktopTextBox
            '
            Me.ValueValidacionListasDesktopTextBox.CantidadDecimales = CType(0, Short)
            Me.ValueValidacionListasDesktopTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValueValidacionListasDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ValueValidacionListasDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ValueValidacionListasDesktopTextBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueValidacionListasDesktopTextBox.Location = New System.Drawing.Point(3, 3)
            Me.ValueValidacionListasDesktopTextBox.MaxLength = 0
            Me.ValueValidacionListasDesktopTextBox.MaxValue = 0.0R
            Me.ValueValidacionListasDesktopTextBox.MinValue = 0.0R
            Me.ValueValidacionListasDesktopTextBox.Name = "ValueValidacionListasDesktopTextBox"
            Me.ValueValidacionListasDesktopTextBox.ReadOnly = True
            Me.ValueValidacionListasDesktopTextBox.Size = New System.Drawing.Size(316, 22)
            Me.ValueValidacionListasDesktopTextBox.TabIndex = 2
            Me.ValueValidacionListasDesktopTextBox.TabStop = False
            Me.ValueValidacionListasDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.ValueValidacionListasDesktopTextBox.UsaDecimales = False
            Me.ValueValidacionListasDesktopTextBox.UsaRango = False
            '
            'TextInputNumericControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.Controls.Add(Me.CapturaValidacionListasPanel)
            Me.Controls.Add(Me.ValueDesktopTextBox)
            Me.Controls.Add(Me.CapturaOldPanel)
            Me.Name = "TextInputNumericControl"
            Me.Padding = New System.Windows.Forms.Padding(5, 5, 5, 10)
            Me.Size = New System.Drawing.Size(332, 142)
            Me.Controls.SetChildIndex(Me.EtiquetaLabel, 0)
            Me.Controls.SetChildIndex(Me.CapturaOldPanel, 0)
            Me.Controls.SetChildIndex(Me.ValueDesktopTextBox, 0)
            Me.Controls.SetChildIndex(Me.CapturaValidacionListasPanel, 0)
            Me.CapturaOldPanel.ResumeLayout(False)
            Me.CapturaOldPanel.PerformLayout()
            Me.CapturaOldTableLayoutPanel.ResumeLayout(False)
            Me.CapturaOldTableLayoutPanel.PerformLayout()
            Me.CapturaValidacionListasPanel.ResumeLayout(False)
            Me.CapturaValidacionListasPanel.PerformLayout()
            Me.CapturaValidacionListasTableLayoutPanel.ResumeLayout(False)
            Me.CapturaValidacionListasTableLayoutPanel.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents CapturaOldPanel As System.Windows.Forms.Panel
        Friend WithEvents CapturaOldTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
        Public WithEvents ValueDesktopTextBox As DesktopNumericTextBoxControl
        Public WithEvents ValueOld1DesktopTextBox As DesktopNumericTextBoxControl
        Public WithEvents ValueOld2DesktopTextBox As DesktopNumericTextBoxControl
        Friend WithEvents CapturaValidacionListasPanel As System.Windows.Forms.Panel
        Friend WithEvents CapturaValidacionListasTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
        Public WithEvents ValueValidacionListasDesktopTextBox As Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl

    End Class

End Namespace
