<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormFechaProcesoOT
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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
        Me.gbxBase = New System.Windows.Forms.GroupBox()
        Me.lblFechaProceso = New System.Windows.Forms.Label()
        Me.UsuarioLabel = New System.Windows.Forms.Label()
        Me.dtpFechaProceso = New System.Windows.Forms.DateTimePicker()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.OTDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.gbxBase.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbxBase
        '
        Me.gbxBase.Controls.Add(Me.OTDesktopComboBox)
        Me.gbxBase.Controls.Add(Me.lblFechaProceso)
        Me.gbxBase.Controls.Add(Me.UsuarioLabel)
        Me.gbxBase.Controls.Add(Me.dtpFechaProceso)
        Me.gbxBase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxBase.Location = New System.Drawing.Point(12, 4)
        Me.gbxBase.Name = "gbxBase"
        Me.gbxBase.Size = New System.Drawing.Size(268, 130)
        Me.gbxBase.TabIndex = 18
        Me.gbxBase.TabStop = False
        '
        'lblFechaProceso
        '
        Me.lblFechaProceso.AutoSize = True
        Me.lblFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaProceso.Location = New System.Drawing.Point(15, 16)
        Me.lblFechaProceso.Name = "lblFechaProceso"
        Me.lblFechaProceso.Size = New System.Drawing.Size(102, 15)
        Me.lblFechaProceso.TabIndex = 26
        Me.lblFechaProceso.Text = "Fecha Proceso"
        '
        'UsuarioLabel
        '
        Me.UsuarioLabel.AutoSize = True
        Me.UsuarioLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsuarioLabel.Location = New System.Drawing.Point(15, 69)
        Me.UsuarioLabel.Name = "UsuarioLabel"
        Me.UsuarioLabel.Size = New System.Drawing.Size(25, 15)
        Me.UsuarioLabel.TabIndex = 24
        Me.UsuarioLabel.Text = "OT"
        '
        'dtpFechaProceso
        '
        Me.dtpFechaProceso.Location = New System.Drawing.Point(18, 35)
        Me.dtpFechaProceso.Name = "dtpFechaProceso"
        Me.dtpFechaProceso.Size = New System.Drawing.Size(218, 20)
        Me.dtpFechaProceso.TabIndex = 0
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(152, 140)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(80, 24)
        Me.btnCancelar.TabIndex = 3
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnAceptar
        '
        Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(50, 140)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(80, 24)
        Me.btnAceptar.TabIndex = 2
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'OTDesktopComboBox
        '
        Me.OTDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OTDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.OTDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.OTDesktopComboBox.DisabledEnter = False
        Me.OTDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OTDesktopComboBox.fk_Campo = 0
        Me.OTDesktopComboBox.fk_Documento = 0
        Me.OTDesktopComboBox.fk_Validacion = 0
        Me.OTDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.OTDesktopComboBox.FormattingEnabled = True
        Me.OTDesktopComboBox.Location = New System.Drawing.Point(18, 87)
        Me.OTDesktopComboBox.Name = "OTDesktopComboBox"
        Me.OTDesktopComboBox.Size = New System.Drawing.Size(218, 21)
        Me.OTDesktopComboBox.TabIndex = 30
        '
        'FormFechaProcesoOT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 176)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.gbxBase)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "FormFechaProcesoOT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Filtrar Por..."
        Me.gbxBase.ResumeLayout(False)
        Me.gbxBase.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbxBase As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents dtpFechaProceso As System.Windows.Forms.DateTimePicker
    Friend WithEvents UsuarioLabel As System.Windows.Forms.Label
    Friend WithEvents lblFechaProceso As System.Windows.Forms.Label
    Friend WithEvents OTDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
End Class
