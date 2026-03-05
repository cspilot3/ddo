<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormActas
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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
        Me.cbxFechaRecaudo = New System.Windows.Forms.ComboBox()
        Me.lblMedio = New System.Windows.Forms.Label()
        Me.lblFechaRecaudo = New System.Windows.Forms.Label()
        Me.BtnGenerar = New System.Windows.Forms.Button()
        Me.cbxActas = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.PnActas = New System.Windows.Forms.Panel()
        Me.cbxCiald = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.lblCiald = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cbxFechaRecaudo
        '
        Me.cbxFechaRecaudo.FormattingEnabled = True
        Me.cbxFechaRecaudo.Location = New System.Drawing.Point(12, 22)
        Me.cbxFechaRecaudo.Name = "cbxFechaRecaudo"
        Me.cbxFechaRecaudo.Size = New System.Drawing.Size(141, 21)
        Me.cbxFechaRecaudo.TabIndex = 27
        '
        'lblMedio
        '
        Me.lblMedio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMedio.Location = New System.Drawing.Point(362, 7)
        Me.lblMedio.Name = "lblMedio"
        Me.lblMedio.Size = New System.Drawing.Size(104, 15)
        Me.lblMedio.TabIndex = 30
        Me.lblMedio.Text = "Número Acta"
        Me.lblMedio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFechaRecaudo
        '
        Me.lblFechaRecaudo.AutoSize = True
        Me.lblFechaRecaudo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaRecaudo.Location = New System.Drawing.Point(22, 5)
        Me.lblFechaRecaudo.Name = "lblFechaRecaudo"
        Me.lblFechaRecaudo.Size = New System.Drawing.Size(111, 15)
        Me.lblFechaRecaudo.TabIndex = 29
        Me.lblFechaRecaudo.Text = "Fecha Recaudo:"
        Me.lblFechaRecaudo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnGenerar
        '
        Me.BtnGenerar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGenerar.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.auto_indexar
        Me.BtnGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnGenerar.Location = New System.Drawing.Point(526, 17)
        Me.BtnGenerar.Name = "BtnGenerar"
        Me.BtnGenerar.Size = New System.Drawing.Size(153, 23)
        Me.BtnGenerar.TabIndex = 34
        Me.BtnGenerar.Text = "     Generar Acta"
        Me.BtnGenerar.UseVisualStyleBackColor = True
        '
        'cbxActas
        '
        Me.cbxActas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbxActas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbxActas.DisabledEnter = False
        Me.cbxActas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxActas.fk_Campo = 0
        Me.cbxActas.fk_Documento = 0
        Me.cbxActas.fk_Validacion = 0
        Me.cbxActas.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cbxActas.FormattingEnabled = True
        Me.cbxActas.Location = New System.Drawing.Point(362, 22)
        Me.cbxActas.Name = "cbxActas"
        Me.cbxActas.Size = New System.Drawing.Size(104, 21)
        Me.cbxActas.TabIndex = 28
        '
        'PnActas
        '
        Me.PnActas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnActas.AutoScroll = True
        Me.PnActas.Location = New System.Drawing.Point(0, 49)
        Me.PnActas.Name = "PnActas"
        Me.PnActas.Size = New System.Drawing.Size(834, 555)
        Me.PnActas.TabIndex = 35
        '
        'cbxCiald
        '
        Me.cbxCiald.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbxCiald.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbxCiald.DisabledEnter = False
        Me.cbxCiald.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxCiald.fk_Campo = 0
        Me.cbxCiald.fk_Documento = 0
        Me.cbxCiald.fk_Validacion = 0
        Me.cbxCiald.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cbxCiald.FormattingEnabled = True
        Me.cbxCiald.Location = New System.Drawing.Point(181, 22)
        Me.cbxCiald.Name = "cbxCiald"
        Me.cbxCiald.Size = New System.Drawing.Size(155, 21)
        Me.cbxCiald.TabIndex = 36
        '
        'lblCiald
        '
        Me.lblCiald.AutoSize = True
        Me.lblCiald.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCiald.Location = New System.Drawing.Point(239, 4)
        Me.lblCiald.Name = "lblCiald"
        Me.lblCiald.Size = New System.Drawing.Size(44, 15)
        Me.lblCiald.TabIndex = 37
        Me.lblCiald.Text = "Ciald:"
        '
        'FormActas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 604)
        Me.Controls.Add(Me.cbxCiald)
        Me.Controls.Add(Me.lblCiald)
        Me.Controls.Add(Me.PnActas)
        Me.Controls.Add(Me.BtnGenerar)
        Me.Controls.Add(Me.cbxFechaRecaudo)
        Me.Controls.Add(Me.cbxActas)
        Me.Controls.Add(Me.lblMedio)
        Me.Controls.Add(Me.lblFechaRecaudo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "FormActas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consulta de Actas"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cbxFechaRecaudo As ComboBox
    Friend WithEvents cbxActas As Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    Friend WithEvents lblMedio As Label
    Friend WithEvents lblFechaRecaudo As Label
    Friend WithEvents BtnGenerar As Button
    Friend WithEvents PnActas As Panel
    Friend WithEvents cbxCiald As Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    Friend WithEvents lblCiald As Label
End Class
