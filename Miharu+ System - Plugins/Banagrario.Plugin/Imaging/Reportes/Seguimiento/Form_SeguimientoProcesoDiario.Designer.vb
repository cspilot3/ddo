Namespace Imaging.Reportes.Seguimiento

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeguimientoProcesoDiario
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
            Me.btnCancelar = New System.Windows.Forms.Button()
            Me.btnAceptar = New System.Windows.Forms.Button()
            Me.gbxBase = New System.Windows.Forms.GroupBox()
            Me.SedeDestapeComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.SedeDestapeLabel = New System.Windows.Forms.Label()
            Me.dtpFechaProceso = New System.Windows.Forms.DateTimePicker()
            Me.lblFechaProceso = New System.Windows.Forms.Label()
            Me.gbxBase.SuspendLayout()
            Me.SuspendLayout()
            '
            'btnCancelar
            '
            Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCancelar.Location = New System.Drawing.Point(170, 135)
            Me.btnCancelar.Name = "btnCancelar"
            Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
            Me.btnCancelar.TabIndex = 7
            Me.btnCancelar.Text = "&Cancelar"
            Me.btnCancelar.UseVisualStyleBackColor = True
            '
            'btnAceptar
            '
            Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnAceptar.Location = New System.Drawing.Point(31, 135)
            Me.btnAceptar.Name = "btnAceptar"
            Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
            Me.btnAceptar.TabIndex = 6
            Me.btnAceptar.Text = "&Aceptar"
            Me.btnAceptar.UseVisualStyleBackColor = True
            '
            'gbxBase
            '
            Me.gbxBase.Controls.Add(Me.SedeDestapeComboBox)
            Me.gbxBase.Controls.Add(Me.SedeDestapeLabel)
            Me.gbxBase.Controls.Add(Me.dtpFechaProceso)
            Me.gbxBase.Controls.Add(Me.lblFechaProceso)
            Me.gbxBase.Location = New System.Drawing.Point(12, 2)
            Me.gbxBase.Name = "gbxBase"
            Me.gbxBase.Size = New System.Drawing.Size(260, 127)
            Me.gbxBase.TabIndex = 5
            Me.gbxBase.TabStop = False
            '
            'SedeDestapeComboBox
            '
            Me.SedeDestapeComboBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SedeDestapeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SedeDestapeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SedeDestapeComboBox.DisabledEnter = False
            Me.SedeDestapeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SedeDestapeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SedeDestapeComboBox.FormattingEnabled = True
            Me.SedeDestapeComboBox.Location = New System.Drawing.Point(19, 87)
            Me.SedeDestapeComboBox.Name = "SedeDestapeComboBox"
            Me.SedeDestapeComboBox.Size = New System.Drawing.Size(235, 21)
            Me.SedeDestapeComboBox.TabIndex = 37
            '
            'SedeDestapeLabel
            '
            Me.SedeDestapeLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SedeDestapeLabel.AutoSize = True
            Me.SedeDestapeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.SedeDestapeLabel.Location = New System.Drawing.Point(16, 69)
            Me.SedeDestapeLabel.Name = "SedeDestapeLabel"
            Me.SedeDestapeLabel.Size = New System.Drawing.Size(91, 13)
            Me.SedeDestapeLabel.TabIndex = 36
            Me.SedeDestapeLabel.Text = "Sede Destape:"
            '
            'dtpFechaProceso
            '
            Me.dtpFechaProceso.Location = New System.Drawing.Point(18, 36)
            Me.dtpFechaProceso.Name = "dtpFechaProceso"
            Me.dtpFechaProceso.Size = New System.Drawing.Size(233, 20)
            Me.dtpFechaProceso.TabIndex = 1
            '
            'lblFechaProceso
            '
            Me.lblFechaProceso.AutoSize = True
            Me.lblFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaProceso.Location = New System.Drawing.Point(16, 16)
            Me.lblFechaProceso.Name = "lblFechaProceso"
            Me.lblFechaProceso.Size = New System.Drawing.Size(100, 13)
            Me.lblFechaProceso.TabIndex = 0
            Me.lblFechaProceso.Text = "Fecha Proceso: "
            '
            'Form_SeguimientoProcesoDiario
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(284, 174)
            Me.Controls.Add(Me.btnCancelar)
            Me.Controls.Add(Me.btnAceptar)
            Me.Controls.Add(Me.gbxBase)
            Me.Name = "FormSeguimientoProcesoDiario"
            Me.Text = "Form_SeguimientoProcesoDiario"
            Me.gbxBase.ResumeLayout(False)
            Me.gbxBase.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents btnCancelar As System.Windows.Forms.Button
        Friend WithEvents btnAceptar As System.Windows.Forms.Button
        Friend WithEvents gbxBase As System.Windows.Forms.GroupBox
        Friend WithEvents SedeDestapeComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents SedeDestapeLabel As System.Windows.Forms.Label
        Friend WithEvents dtpFechaProceso As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblFechaProceso As System.Windows.Forms.Label
    End Class

End Namespace

