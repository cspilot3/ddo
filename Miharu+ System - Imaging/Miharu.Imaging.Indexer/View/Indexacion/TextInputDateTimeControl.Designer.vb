
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopDateTimeTextBox

Namespace View.Indexacion

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class TextInputDateTimeControl
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango3 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.CapturaOldPanel = New System.Windows.Forms.Panel()
            Me.CapturaOldTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            Me.ValueOld1DesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.ValueOld2DesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.ValueDesktopTextBox = New Miharu.Desktop.Controls.DesktopDateTimeTextBox.DesktopDateTimeTextBoxControl()
            Me.CapturaValidacionListasPanel = New System.Windows.Forms.Panel()
            Me.CapturaValidacionListasTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            Me.ValueValidacionListasDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
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
            Me.ValueOld1DesktopTextBox._Obligatorio = False
            Me.ValueOld1DesktopTextBox._PermitePegar = False
            Me.ValueOld1DesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.ValueOld1DesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.ValueOld1DesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ValueOld1DesktopTextBox.DateFormat = Nothing
            Me.ValueOld1DesktopTextBox.DisabledEnter = False
            Me.ValueOld1DesktopTextBox.DisabledTab = False
            Me.ValueOld1DesktopTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValueOld1DesktopTextBox.Enabled = False
            Me.ValueOld1DesktopTextBox.EnabledShortCuts = False
            Me.ValueOld1DesktopTextBox.fk_Campo = 0
            Me.ValueOld1DesktopTextBox.fk_Documento = 0
            Me.ValueOld1DesktopTextBox.fk_Validacion = 0
            Me.ValueOld1DesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ValueOld1DesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ValueOld1DesktopTextBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueOld1DesktopTextBox.Location = New System.Drawing.Point(3, 3)
            Me.ValueOld1DesktopTextBox.MaskedTextBox_Property = ""
            Me.ValueOld1DesktopTextBox.MaximumLength = CType(0, Short)
            Me.ValueOld1DesktopTextBox.MinimumLength = CType(0, Short)
            Me.ValueOld1DesktopTextBox.Name = "ValueOld1DesktopTextBox"
            Me.ValueOld1DesktopTextBox.Obligatorio = False
            Me.ValueOld1DesktopTextBox.permitePegar = False
            Rango1.MaxValue = 1.7976931348623157E+308R
            Rango1.MinValue = 0.0R
            Me.ValueOld1DesktopTextBox.Rango = Rango1
            Me.ValueOld1DesktopTextBox.ReadOnly = True
            Me.ValueOld1DesktopTextBox.Size = New System.Drawing.Size(316, 22)
            Me.ValueOld1DesktopTextBox.TabIndex = 0
            Me.ValueOld1DesktopTextBox.TabStop = False
            Me.ValueOld1DesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ValueOld1DesktopTextBox.Usa_Decimales = False
            Me.ValueOld1DesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'ValueOld2DesktopTextBox
            '
            Me.ValueOld2DesktopTextBox._Obligatorio = False
            Me.ValueOld2DesktopTextBox._PermitePegar = False
            Me.ValueOld2DesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.ValueOld2DesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.ValueOld2DesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ValueOld2DesktopTextBox.DateFormat = Nothing
            Me.ValueOld2DesktopTextBox.DisabledEnter = False
            Me.ValueOld2DesktopTextBox.DisabledTab = False
            Me.ValueOld2DesktopTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValueOld2DesktopTextBox.Enabled = False
            Me.ValueOld2DesktopTextBox.EnabledShortCuts = False
            Me.ValueOld2DesktopTextBox.fk_Campo = 0
            Me.ValueOld2DesktopTextBox.fk_Documento = 0
            Me.ValueOld2DesktopTextBox.fk_Validacion = 0
            Me.ValueOld2DesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ValueOld2DesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ValueOld2DesktopTextBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueOld2DesktopTextBox.Location = New System.Drawing.Point(3, 36)
            Me.ValueOld2DesktopTextBox.MaskedTextBox_Property = ""
            Me.ValueOld2DesktopTextBox.MaximumLength = CType(0, Short)
            Me.ValueOld2DesktopTextBox.MinimumLength = CType(0, Short)
            Me.ValueOld2DesktopTextBox.Name = "ValueOld2DesktopTextBox"
            Me.ValueOld2DesktopTextBox.Obligatorio = False
            Me.ValueOld2DesktopTextBox.permitePegar = False
            Rango2.MaxValue = 1.7976931348623157E+308R
            Rango2.MinValue = 0.0R
            Me.ValueOld2DesktopTextBox.Rango = Rango2
            Me.ValueOld2DesktopTextBox.ReadOnly = True
            Me.ValueOld2DesktopTextBox.Size = New System.Drawing.Size(316, 22)
            Me.ValueOld2DesktopTextBox.TabIndex = 1
            Me.ValueOld2DesktopTextBox.TabStop = False
            Me.ValueOld2DesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ValueOld2DesktopTextBox.Usa_Decimales = False
            Me.ValueOld2DesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'ValueDesktopTextBox
            '
            Me.ValueDesktopTextBox.DateFormat = Nothing
            Me.ValueDesktopTextBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.ValueDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ValueDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ValueDesktopTextBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueDesktopTextBox.Location = New System.Drawing.Point(5, 82)
            Me.ValueDesktopTextBox.Mask = "9999/99/99"
            Me.ValueDesktopTextBox.Name = "ValueDesktopTextBox"
            Me.ValueDesktopTextBox.ResetOnPrompt = False
            Me.ValueDesktopTextBox.ResetOnSpace = False
            Me.ValueDesktopTextBox.Size = New System.Drawing.Size(322, 22)
            Me.ValueDesktopTextBox.TabIndex = 0
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
            Me.ValueValidacionListasDesktopTextBox._Obligatorio = False
            Me.ValueValidacionListasDesktopTextBox._PermitePegar = False
            Me.ValueValidacionListasDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.ValueValidacionListasDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.ValueValidacionListasDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ValueValidacionListasDesktopTextBox.DateFormat = Nothing
            Me.ValueValidacionListasDesktopTextBox.DisabledEnter = False
            Me.ValueValidacionListasDesktopTextBox.DisabledTab = False
            Me.ValueValidacionListasDesktopTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ValueValidacionListasDesktopTextBox.EnabledShortCuts = False
            Me.ValueValidacionListasDesktopTextBox.fk_Campo = 0
            Me.ValueValidacionListasDesktopTextBox.fk_Documento = 0
            Me.ValueValidacionListasDesktopTextBox.fk_Validacion = 0
            Me.ValueValidacionListasDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ValueValidacionListasDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ValueValidacionListasDesktopTextBox.Font = New System.Drawing.Font("Roboto Mono", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValueValidacionListasDesktopTextBox.Location = New System.Drawing.Point(3, 3)
            Me.ValueValidacionListasDesktopTextBox.MaskedTextBox_Property = ""
            Me.ValueValidacionListasDesktopTextBox.MaximumLength = CType(0, Short)
            Me.ValueValidacionListasDesktopTextBox.MinimumLength = CType(0, Short)
            Me.ValueValidacionListasDesktopTextBox.Name = "ValueValidacionListasDesktopTextBox"
            Me.ValueValidacionListasDesktopTextBox.Obligatorio = False
            Me.ValueValidacionListasDesktopTextBox.permitePegar = False
            Rango3.MaxValue = 1.7976931348623157E+308R
            Rango3.MinValue = 0.0R
            Me.ValueValidacionListasDesktopTextBox.Rango = Rango3
            Me.ValueValidacionListasDesktopTextBox.ReadOnly = True
            Me.ValueValidacionListasDesktopTextBox.Size = New System.Drawing.Size(316, 22)
            Me.ValueValidacionListasDesktopTextBox.TabIndex = 2
            Me.ValueValidacionListasDesktopTextBox.TabStop = False
            Me.ValueValidacionListasDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ValueValidacionListasDesktopTextBox.Usa_Decimales = False
            Me.ValueValidacionListasDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'TextInputDateTimeControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.Controls.Add(Me.CapturaValidacionListasPanel)
            Me.Controls.Add(Me.ValueDesktopTextBox)
            Me.Controls.Add(Me.CapturaOldPanel)
            Me.Name = "TextInputDateTimeControl"
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
        Public WithEvents ValueDesktopTextBox As DesktopDateTimeTextBoxControl
        Public WithEvents ValueOld1DesktopTextBox As DesktopTextBoxControl
        Public WithEvents ValueOld2DesktopTextBox As DesktopTextBoxControl
        Friend WithEvents CapturaValidacionListasPanel As System.Windows.Forms.Panel
        Friend WithEvents CapturaValidacionListasTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
        Public WithEvents ValueValidacionListasDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl

    End Class

End Namespace
