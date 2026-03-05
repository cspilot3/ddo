Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Controls

Namespace Imaging.Forms.Parametrización
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNuevoTipologiaTx
        Inherits System.Windows.Forms.Form

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
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.TipologiaDesktopComboBox = New DesktopComboBoxControl()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.DesmaterializadaDesktopCheckBox = New DesktopCheckBox.DesktopCheckBoxControl()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.ProductoDesktopComboBox = New DesktopComboBoxControl()
            Me.TipoMovimientoDesktopComboBox = New DesktopComboBoxControl()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.CodigoTXMaskedTextBox = New System.Windows.Forms.MaskedTextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.CamposMinimosCruceMaskedTextBox = New System.Windows.Forms.MaskedTextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CamposMinimosCruceSuperiorMaskedTextBox = New System.Windows.Forms.MaskedTextBox()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.UsaLlavesDesktopCheckBox = New DesktopCheckBox.DesktopCheckBoxControl()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.MultiregistroDesktopCheckBox = New DesktopCheckBox.DesktopCheckBoxControl()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.NaturalezaMedioPagoComboBox = New System.Windows.Forms.ComboBox()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.EliminadoDesktopCheckBox = New DesktopCheckBox.DesktopCheckBoxControl()
            Me.Label11 = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(201, 298)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(90, 30)
            Me.GuardarButton.TabIndex = 36
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(297, 298)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 37
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'TipologiaDesktopComboBox
            '
            Me.TipologiaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipologiaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipologiaDesktopComboBox.DisabledEnter = False
            Me.TipologiaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipologiaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipologiaDesktopComboBox.FormattingEnabled = True
            Me.TipologiaDesktopComboBox.Location = New System.Drawing.Point(157, 12)
            Me.TipologiaDesktopComboBox.Name = "TipologiaDesktopComboBox"
            Me.TipologiaDesktopComboBox.Size = New System.Drawing.Size(200, 21)
            Me.TipologiaDesktopComboBox.TabIndex = 60
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label5.Location = New System.Drawing.Point(14, 15)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(61, 13)
            Me.Label5.TabIndex = 59
            Me.Label5.Text = "Tipología"
            '
            'DesmaterializadaDesktopCheckBox
            '
            Me.DesmaterializadaDesktopCheckBox.AutoSize = True
            Me.DesmaterializadaDesktopCheckBox.DisabledEnter = False
            Me.DesmaterializadaDesktopCheckBox.FocusIn = System.Drawing.Color.LightYellow
            Me.DesmaterializadaDesktopCheckBox.FocusOut = System.Drawing.Color.Gainsboro
            Me.DesmaterializadaDesktopCheckBox.Location = New System.Drawing.Point(157, 118)
            Me.DesmaterializadaDesktopCheckBox.Name = "DesmaterializadaDesktopCheckBox"
            Me.DesmaterializadaDesktopCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.DesmaterializadaDesktopCheckBox.TabIndex = 62
            Me.DesmaterializadaDesktopCheckBox.UseVisualStyleBackColor = True
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label8.Location = New System.Drawing.Point(14, 118)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(103, 13)
            Me.Label8.TabIndex = 61
            Me.Label8.Text = "Desmaterializada"
            '
            'ProductoDesktopComboBox
            '
            Me.ProductoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProductoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProductoDesktopComboBox.DisabledEnter = False
            Me.ProductoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProductoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProductoDesktopComboBox.FormattingEnabled = True
            Me.ProductoDesktopComboBox.Location = New System.Drawing.Point(157, 65)
            Me.ProductoDesktopComboBox.Name = "ProductoDesktopComboBox"
            Me.ProductoDesktopComboBox.Size = New System.Drawing.Size(200, 21)
            Me.ProductoDesktopComboBox.TabIndex = 66
            '
            'TipoMovimientoDesktopComboBox
            '
            Me.TipoMovimientoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipoMovimientoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipoMovimientoDesktopComboBox.DisabledEnter = False
            Me.TipoMovimientoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipoMovimientoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipoMovimientoDesktopComboBox.FormattingEnabled = True
            Me.TipoMovimientoDesktopComboBox.Location = New System.Drawing.Point(157, 39)
            Me.TipoMovimientoDesktopComboBox.Name = "TipoMovimientoDesktopComboBox"
            Me.TipoMovimientoDesktopComboBox.Size = New System.Drawing.Size(200, 21)
            Me.TipoMovimientoDesktopComboBox.TabIndex = 65
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(14, 68)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(58, 13)
            Me.Label4.TabIndex = 64
            Me.Label4.Text = "Producto"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(14, 42)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(100, 13)
            Me.Label3.TabIndex = 63
            Me.Label3.Text = "Tipo Movimiento"
            '
            'CodigoTXMaskedTextBox
            '
            Me.CodigoTXMaskedTextBox.Location = New System.Drawing.Point(157, 92)
            Me.CodigoTXMaskedTextBox.Mask = "99999"
            Me.CodigoTXMaskedTextBox.Name = "CodigoTXMaskedTextBox"
            Me.CodigoTXMaskedTextBox.Size = New System.Drawing.Size(202, 20)
            Me.CodigoTXMaskedTextBox.TabIndex = 68
            Me.CodigoTXMaskedTextBox.ValidatingType = GetType(Integer)
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(14, 95)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(64, 13)
            Me.Label2.TabIndex = 67
            Me.Label2.Text = "Código Tx"
            '
            'CamposMinimosCruceMaskedTextBox
            '
            Me.CamposMinimosCruceMaskedTextBox.Location = New System.Drawing.Point(157, 138)
            Me.CamposMinimosCruceMaskedTextBox.Mask = "99999"
            Me.CamposMinimosCruceMaskedTextBox.Name = "CamposMinimosCruceMaskedTextBox"
            Me.CamposMinimosCruceMaskedTextBox.Size = New System.Drawing.Size(202, 20)
            Me.CamposMinimosCruceMaskedTextBox.TabIndex = 70
            Me.CamposMinimosCruceMaskedTextBox.ValidatingType = GetType(Integer)
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(14, 141)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(137, 13)
            Me.Label1.TabIndex = 69
            Me.Label1.Text = "Campos Minimos Cruce"
            '
            'CamposMinimosCruceSuperiorMaskedTextBox
            '
            Me.CamposMinimosCruceSuperiorMaskedTextBox.Location = New System.Drawing.Point(157, 167)
            Me.CamposMinimosCruceSuperiorMaskedTextBox.Mask = "99999"
            Me.CamposMinimosCruceSuperiorMaskedTextBox.Name = "CamposMinimosCruceSuperiorMaskedTextBox"
            Me.CamposMinimosCruceSuperiorMaskedTextBox.Size = New System.Drawing.Size(202, 20)
            Me.CamposMinimosCruceSuperiorMaskedTextBox.TabIndex = 72
            Me.CamposMinimosCruceSuperiorMaskedTextBox.ValidatingType = GetType(Integer)
            '
            'Label6
            '
            Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label6.Location = New System.Drawing.Point(14, 167)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(137, 31)
            Me.Label6.TabIndex = 71
            Me.Label6.Text = "Campos Minimos Cruce Superior"
            '
            'UsaLlavesDesktopCheckBox
            '
            Me.UsaLlavesDesktopCheckBox.AutoSize = True
            Me.UsaLlavesDesktopCheckBox.DisabledEnter = False
            Me.UsaLlavesDesktopCheckBox.FocusIn = System.Drawing.Color.LightYellow
            Me.UsaLlavesDesktopCheckBox.FocusOut = System.Drawing.Color.Gainsboro
            Me.UsaLlavesDesktopCheckBox.Location = New System.Drawing.Point(157, 205)
            Me.UsaLlavesDesktopCheckBox.Name = "UsaLlavesDesktopCheckBox"
            Me.UsaLlavesDesktopCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.UsaLlavesDesktopCheckBox.TabIndex = 74
            Me.UsaLlavesDesktopCheckBox.UseVisualStyleBackColor = True
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label7.Location = New System.Drawing.Point(14, 205)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(107, 13)
            Me.Label7.TabIndex = 73
            Me.Label7.Text = "Usa Llaves Cruce"
            '
            'MultiregistroDesktopCheckBox
            '
            Me.MultiregistroDesktopCheckBox.AutoSize = True
            Me.MultiregistroDesktopCheckBox.DisabledEnter = False
            Me.MultiregistroDesktopCheckBox.FocusIn = System.Drawing.Color.LightYellow
            Me.MultiregistroDesktopCheckBox.FocusOut = System.Drawing.Color.Gainsboro
            Me.MultiregistroDesktopCheckBox.Location = New System.Drawing.Point(157, 225)
            Me.MultiregistroDesktopCheckBox.Name = "MultiregistroDesktopCheckBox"
            Me.MultiregistroDesktopCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.MultiregistroDesktopCheckBox.TabIndex = 76
            Me.MultiregistroDesktopCheckBox.UseVisualStyleBackColor = True
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label9.Location = New System.Drawing.Point(14, 225)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(76, 13)
            Me.Label9.TabIndex = 75
            Me.Label9.Text = "Multiregistro"
            '
            'NaturalezaMedioPagoComboBox
            '
            Me.NaturalezaMedioPagoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.NaturalezaMedioPagoComboBox.FormattingEnabled = True
            Me.NaturalezaMedioPagoComboBox.Items.AddRange(New Object() {"DB", "CR"})
            Me.NaturalezaMedioPagoComboBox.Location = New System.Drawing.Point(157, 246)
            Me.NaturalezaMedioPagoComboBox.Name = "NaturalezaMedioPagoComboBox"
            Me.NaturalezaMedioPagoComboBox.Size = New System.Drawing.Size(202, 21)
            Me.NaturalezaMedioPagoComboBox.TabIndex = 77
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label10.Location = New System.Drawing.Point(14, 249)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(139, 13)
            Me.Label10.TabIndex = 78
            Me.Label10.Text = "Naturaleza Medio Pago"
            '
            'EliminadoDesktopCheckBox
            '
            Me.EliminadoDesktopCheckBox.AutoSize = True
            Me.EliminadoDesktopCheckBox.DisabledEnter = False
            Me.EliminadoDesktopCheckBox.FocusIn = System.Drawing.Color.LightYellow
            Me.EliminadoDesktopCheckBox.FocusOut = System.Drawing.Color.Gainsboro
            Me.EliminadoDesktopCheckBox.Location = New System.Drawing.Point(157, 273)
            Me.EliminadoDesktopCheckBox.Name = "EliminadoDesktopCheckBox"
            Me.EliminadoDesktopCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.EliminadoDesktopCheckBox.TabIndex = 80
            Me.EliminadoDesktopCheckBox.UseVisualStyleBackColor = True
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label11.Location = New System.Drawing.Point(14, 273)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(61, 13)
            Me.Label11.TabIndex = 79
            Me.Label11.Text = "Eliminado"
            '
            'FormNuevoTipologiaTx
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(399, 340)
            Me.Controls.Add(Me.EliminadoDesktopCheckBox)
            Me.Controls.Add(Me.Label11)
            Me.Controls.Add(Me.Label10)
            Me.Controls.Add(Me.NaturalezaMedioPagoComboBox)
            Me.Controls.Add(Me.MultiregistroDesktopCheckBox)
            Me.Controls.Add(Me.Label9)
            Me.Controls.Add(Me.UsaLlavesDesktopCheckBox)
            Me.Controls.Add(Me.Label7)
            Me.Controls.Add(Me.CamposMinimosCruceSuperiorMaskedTextBox)
            Me.Controls.Add(Me.Label6)
            Me.Controls.Add(Me.CamposMinimosCruceMaskedTextBox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.CodigoTXMaskedTextBox)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.ProductoDesktopComboBox)
            Me.Controls.Add(Me.TipoMovimientoDesktopComboBox)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.DesmaterializadaDesktopCheckBox)
            Me.Controls.Add(Me.Label8)
            Me.Controls.Add(Me.TipologiaDesktopComboBox)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Name = "FormNuevoTipologiaTx"
            Me.Text = "FormNuevoTipologiaTx"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents TipologiaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents DesmaterializadaDesktopCheckBox As DesktopCheckBox.DesktopCheckBoxControl
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents ProductoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents TipoMovimientoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents CodigoTXMaskedTextBox As System.Windows.Forms.MaskedTextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents CamposMinimosCruceMaskedTextBox As System.Windows.Forms.MaskedTextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CamposMinimosCruceSuperiorMaskedTextBox As System.Windows.Forms.MaskedTextBox
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents UsaLlavesDesktopCheckBox As DesktopCheckBox.DesktopCheckBoxControl
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents MultiregistroDesktopCheckBox As DesktopCheckBox.DesktopCheckBoxControl
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents NaturalezaMedioPagoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents EliminadoDesktopCheckBox As DesktopCheckBox.DesktopCheckBoxControl
        Friend WithEvents Label11 As System.Windows.Forms.Label
    End Class
End Namespace