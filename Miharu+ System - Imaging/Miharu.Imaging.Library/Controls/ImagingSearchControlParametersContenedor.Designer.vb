Namespace Controls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class ImagingSearchControlParametersContenedor
        Inherits System.Windows.Forms.UserControl

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
        <System.Diagnostics.DebuggerNonUserCode()>
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
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImagingSearchControlParametersContenedor))
            Me.CampoComboBox = New System.Windows.Forms.ComboBox()
            Me.ValorTextBox = New System.Windows.Forms.TextBox()
            Me.CampoLabel = New System.Windows.Forms.Label()
            Me.MainTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            Me.ValorLabel = New System.Windows.Forms.Label()
            Me.BuscarPanel = New System.Windows.Forms.Panel()
            Me.BusquedaButton = New System.Windows.Forms.Button()
            Me.MainTableLayoutPanel.SuspendLayout()
            Me.BuscarPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'CampoComboBox
            '
            Me.CampoComboBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CampoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CampoComboBox.FormattingEnabled = True
            Me.CampoComboBox.Location = New System.Drawing.Point(73, 3)
            Me.CampoComboBox.Name = "CampoComboBox"
            Me.CampoComboBox.Size = New System.Drawing.Size(210, 21)
            Me.CampoComboBox.TabIndex = 24
            '
            'ValorTextBox
            '
            Me.ValorTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValorTextBox.Location = New System.Drawing.Point(73, 38)
            Me.ValorTextBox.Name = "ValorTextBox"
            Me.ValorTextBox.Size = New System.Drawing.Size(210, 20)
            Me.ValorTextBox.TabIndex = 23
            '
            'CampoLabel
            '
            Me.CampoLabel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CampoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CampoLabel.Location = New System.Drawing.Point(3, 0)
            Me.CampoLabel.Name = "CampoLabel"
            Me.CampoLabel.Size = New System.Drawing.Size(64, 25)
            Me.CampoLabel.TabIndex = 21
            Me.CampoLabel.Text = "Campo"
            Me.CampoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'MainTableLayoutPanel
            '
            Me.MainTableLayoutPanel.ColumnCount = 2
            Me.MainTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
            Me.MainTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.MainTableLayoutPanel.Controls.Add(Me.CampoLabel, 0, 0)
            Me.MainTableLayoutPanel.Controls.Add(Me.ValorTextBox, 1, 2)
            Me.MainTableLayoutPanel.Controls.Add(Me.CampoComboBox, 1, 0)
            Me.MainTableLayoutPanel.Controls.Add(Me.ValorLabel, 0, 2)
            Me.MainTableLayoutPanel.Controls.Add(Me.BuscarPanel, 1, 3)
            Me.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.MainTableLayoutPanel.Location = New System.Drawing.Point(10, 10)
            Me.MainTableLayoutPanel.Name = "MainTableLayoutPanel"
            Me.MainTableLayoutPanel.RowCount = 4
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
            Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.MainTableLayoutPanel.Size = New System.Drawing.Size(286, 197)
            Me.MainTableLayoutPanel.TabIndex = 25
            '
            'ValorLabel
            '
            Me.ValorLabel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValorLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValorLabel.Location = New System.Drawing.Point(3, 35)
            Me.ValorLabel.Name = "ValorLabel"
            Me.ValorLabel.Size = New System.Drawing.Size(64, 25)
            Me.ValorLabel.TabIndex = 25
            Me.ValorLabel.Text = "Valor"
            Me.ValorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'BuscarPanel
            '
            Me.BuscarPanel.Controls.Add(Me.BusquedaButton)
            Me.BuscarPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.BuscarPanel.Location = New System.Drawing.Point(73, 63)
            Me.BuscarPanel.Name = "BuscarPanel"
            Me.BuscarPanel.Size = New System.Drawing.Size(210, 30)
            Me.BuscarPanel.TabIndex = 27
            '
            'BusquedaButton
            '
            Me.BusquedaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BusquedaButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BusquedaButton.Image = CType(resources.GetObject("BusquedaButton.Image"), System.Drawing.Image)
            Me.BusquedaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BusquedaButton.Location = New System.Drawing.Point(132, 3)
            Me.BusquedaButton.Name = "BusquedaButton"
            Me.BusquedaButton.Size = New System.Drawing.Size(75, 23)
            Me.BusquedaButton.TabIndex = 26
            Me.BusquedaButton.Text = "&Buscar"
            Me.BusquedaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BusquedaButton.UseVisualStyleBackColor = True
            '
            'ImagingSearchControlParametersContenedor
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Controls.Add(Me.MainTableLayoutPanel)
            Me.Name = "ImagingSearchControlParametersContenedor"
            Me.Padding = New System.Windows.Forms.Padding(10)
            Me.Size = New System.Drawing.Size(306, 217)
            Me.MainTableLayoutPanel.ResumeLayout(False)
            Me.MainTableLayoutPanel.PerformLayout()
            Me.BuscarPanel.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CampoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents ValorTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CampoLabel As System.Windows.Forms.Label
        Friend WithEvents MainTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents ValorLabel As System.Windows.Forms.Label
        Friend WithEvents BuscarPanel As System.Windows.Forms.Panel
        Friend WithEvents BusquedaButton As System.Windows.Forms.Button

    End Class
End Namespace