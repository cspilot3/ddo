Imports Miharu.Desktop.Controls.DesktopNumericTextBox

Namespace View.Indexacion.Table

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class IndexerTableCellNumeric

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
            Me.ValueDesktopTextBox = New DesktopNumericTextBoxControl()
            Me.ValueOld2DesktopTextBox = New DesktopNumericTextBoxControl()
            Me.ValueOld1DesktopTextBox = New DesktopNumericTextBoxControl()
            Me.BodyTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            Me.BodyTableLayoutPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'ValueTextBox
            '
            Me.ValueDesktopTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ValueDesktopTextBox.CantidadDecimales = CType(0, Short)
            Me.ValueDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ValueDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ValueDesktopTextBox.Location = New System.Drawing.Point(0, 43)
            Me.ValueDesktopTextBox.Margin = New System.Windows.Forms.Padding(0)
            Me.ValueDesktopTextBox.MaxLength = 0
            Me.ValueDesktopTextBox.Name = "ValueTextBox"
            Me.ValueDesktopTextBox.ShortcutsEnabled = True
            Me.ValueDesktopTextBox.Size = New System.Drawing.Size(253, 20)
            Me.ValueDesktopTextBox.TabIndex = 0
            Me.ValueDesktopTextBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueDesktopTextBox.UsaDecimales = False
            '
            'Value2TextBox
            '
            Me.ValueOld2DesktopTextBox.BackColor = System.Drawing.Color.LightSteelBlue
            Me.ValueOld2DesktopTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ValueOld2DesktopTextBox.Enabled = False
            Me.ValueOld2DesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ValueOld2DesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ValueOld2DesktopTextBox.Location = New System.Drawing.Point(0, 21)
            Me.ValueOld2DesktopTextBox.Margin = New System.Windows.Forms.Padding(0, 0, 0, 2)
            Me.ValueOld2DesktopTextBox.MaxLength = 0
            Me.ValueOld2DesktopTextBox.Name = "Value2TextBox"
            Me.ValueOld2DesktopTextBox.ShortcutsEnabled = True
            Me.ValueOld2DesktopTextBox.Size = New System.Drawing.Size(253, 20)
            Me.ValueOld2DesktopTextBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueOld2DesktopTextBox.TabIndex = 1
            '
            'Value1TextBox
            '
            Me.ValueOld1DesktopTextBox.BackColor = System.Drawing.Color.LightSteelBlue
            Me.ValueOld1DesktopTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ValueOld1DesktopTextBox.Enabled = False
            Me.ValueOld1DesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ValueOld1DesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ValueOld1DesktopTextBox.Location = New System.Drawing.Point(0, 0)
            Me.ValueOld1DesktopTextBox.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
            Me.ValueOld1DesktopTextBox.MaxLength = 0
            Me.ValueOld1DesktopTextBox.Name = "Value1TextBox"
            Me.ValueOld1DesktopTextBox.ShortcutsEnabled = True
            Me.ValueOld1DesktopTextBox.Size = New System.Drawing.Size(253, 20)
            Me.ValueOld1DesktopTextBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueOld1DesktopTextBox.TabIndex = 4
            '
            'BodyTableLayoutPanel
            '
            Me.BodyTableLayoutPanel.AutoSize = True
            Me.BodyTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.BodyTableLayoutPanel.ColumnCount = 1
            Me.BodyTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.BodyTableLayoutPanel.Controls.Add(Me.ValueOld1DesktopTextBox, 0, 0)
            Me.BodyTableLayoutPanel.Controls.Add(Me.ValueDesktopTextBox, 0, 2)
            Me.BodyTableLayoutPanel.Controls.Add(Me.ValueOld2DesktopTextBox, 0, 1)
            Me.BodyTableLayoutPanel.Location = New System.Drawing.Point(1, 1)
            Me.BodyTableLayoutPanel.Margin = New System.Windows.Forms.Padding(0)
            Me.BodyTableLayoutPanel.Name = "BodyTableLayoutPanel"
            Me.BodyTableLayoutPanel.RowCount = 3
            Me.BodyTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.BodyTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.BodyTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.BodyTableLayoutPanel.Size = New System.Drawing.Size(253, 63)
            Me.BodyTableLayoutPanel.TabIndex = 5
            '
            'IndexerTableCell
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Controls.Add(Me.BodyTableLayoutPanel)
            Me.Margin = New System.Windows.Forms.Padding(1)
            Me.Name = "IndexerTableCell"
            Me.Padding = New System.Windows.Forms.Padding(1)
            Me.Size = New System.Drawing.Size(255, 65)
            Me.BodyTableLayoutPanel.ResumeLayout(False)
            Me.BodyTableLayoutPanel.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents BodyTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
        Private WithEvents ValueDesktopTextBox As DesktopNumericTextBoxControl
        Private WithEvents ValueOld2DesktopTextBox As DesktopNumericTextBoxControl
        Private WithEvents ValueOld1DesktopTextBox As DesktopNumericTextBoxControl

    End Class

End Namespace