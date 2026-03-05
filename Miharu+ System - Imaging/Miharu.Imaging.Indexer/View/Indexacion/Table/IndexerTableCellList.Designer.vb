Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace View.Indexacion.Table

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class IndexerTableCellList

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
            Me.ValueDesktopComboBox = New DesktopComboBoxControl()
            Me.ValueOld1DesktopComboBox = New DesktopComboBoxControl()
            Me.ValueOld2DesktopComboBox = New DesktopComboBoxControl()
            Me.BodyTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            Me.BodyTableLayoutPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'ValueDesktopComboBox
            '
            Me.ValueDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ValueDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ValueDesktopComboBox.DisabledEnter = True
            Me.ValueDesktopComboBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.ValueDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ValueDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ValueDesktopComboBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueDesktopComboBox.FormattingEnabled = True
            Me.ValueDesktopComboBox.Location = New System.Drawing.Point(0, 43)
            Me.ValueDesktopComboBox.Name = "ValueDesktopComboBox"
            Me.ValueDesktopComboBox.Margin = New System.Windows.Forms.Padding(0)
            Me.ValueDesktopComboBox.Size = New System.Drawing.Size(253, 20)
            Me.ValueDesktopComboBox.TabIndex = 0

            '
            'ValueOld2DesktopComboBox
            '
            Me.ValueOld2DesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ValueOld2DesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ValueOld2DesktopComboBox.DisabledEnter = True
            Me.ValueOld2DesktopComboBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.ValueOld2DesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ValueOld2DesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ValueOld2DesktopComboBox.FormattingEnabled = True
            Me.ValueOld2DesktopComboBox.TabStop = False
            Me.ValueOld2DesktopComboBox.BackColor = System.Drawing.Color.LightSteelBlue
            Me.ValueOld2DesktopComboBox.Enabled = False
            Me.ValueOld2DesktopComboBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueOld2DesktopComboBox.Location = New System.Drawing.Point(0, 21)
            Me.ValueOld2DesktopComboBox.Margin = New System.Windows.Forms.Padding(0, 0, 0, 2)
            Me.ValueOld2DesktopComboBox.Name = "ValueOld2DesktopComboBox"
            Me.ValueOld2DesktopComboBox.Size = New System.Drawing.Size(253, 20)
            Me.ValueOld2DesktopComboBox.TabIndex = 1
            '
            'BodyTableLayoutPanel
            '
            Me.BodyTableLayoutPanel.AutoSize = True
            Me.BodyTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.BodyTableLayoutPanel.ColumnCount = 1
            Me.BodyTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.BodyTableLayoutPanel.Controls.Add(Me.ValueOld1DesktopComboBox, 0, 0)
            Me.BodyTableLayoutPanel.Controls.Add(Me.ValueDesktopComboBox, 0, 2)
            Me.BodyTableLayoutPanel.Controls.Add(Me.ValueOld2DesktopComboBox, 0, 1)
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
            'ValueOld1DesktopComboBox
            '
            Me.ValueOld1DesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ValueOld1DesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ValueOld1DesktopComboBox.DisabledEnter = True
            Me.ValueOld1DesktopComboBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.ValueOld1DesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ValueOld1DesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ValueOld1DesktopComboBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueOld1DesktopComboBox.FormattingEnabled = True
            Me.ValueOld1DesktopComboBox.TabStop = False
            Me.ValueOld1DesktopComboBox.BackColor = System.Drawing.Color.LightSteelBlue
            Me.ValueOld1DesktopComboBox.Enabled = False
            Me.ValueOld1DesktopComboBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueOld1DesktopComboBox.Location = New System.Drawing.Point(0, 0)
            Me.ValueOld1DesktopComboBox.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
            Me.ValueOld1DesktopComboBox.Name = "ValueOld1DesktopComboBox"
            Me.ValueOld1DesktopComboBox.Size = New System.Drawing.Size(253, 20)
            Me.ValueOld1DesktopComboBox.TabIndex = 4
            '
            'IndexerTableCellList
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Controls.Add(Me.BodyTableLayoutPanel)
            Me.Margin = New System.Windows.Forms.Padding(1)
            Me.Name = "IndexerTableCellList"
            Me.Padding = New System.Windows.Forms.Padding(1)
            Me.Size = New System.Drawing.Size(255, 65)
            Me.BodyTableLayoutPanel.ResumeLayout(False)
            Me.BodyTableLayoutPanel.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents BodyTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
        Private WithEvents ValueDesktopComboBox As DesktopComboBoxControl
        Private WithEvents ValueOld2DesktopComboBox As DesktopComboBoxControl
        Private WithEvents ValueOld1DesktopComboBox As DesktopComboBoxControl

    End Class

End Namespace