Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Reportes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FormEfectividad
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
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

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.gbxBase = New System.Windows.Forms.GroupBox()
            Me.cmbColaDocumental = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.LblCola = New System.Windows.Forms.Label()
            Me.cbxSede = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblSede = New System.Windows.Forms.Label()
            Me.dntOFicina = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.LblOficina = New System.Windows.Forms.Label()
            Me.lblFechaInicial = New System.Windows.Forms.Label()
            Me.dtpFechaInicial = New System.Windows.Forms.DateTimePicker()
            Me.dtpFechaFinal = New System.Windows.Forms.DateTimePicker()
            Me.lblFechaFinal = New System.Windows.Forms.Label()
            Me.btnCancelar = New System.Windows.Forms.Button()
            Me.btnAceptar = New System.Windows.Forms.Button()
            Me.cmbBancoEpago = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.gbxBase.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbxBase
            '
            Me.gbxBase.Controls.Add(Me.cmbBancoEpago)
            Me.gbxBase.Controls.Add(Me.Label1)
            Me.gbxBase.Controls.Add(Me.cmbColaDocumental)
            Me.gbxBase.Controls.Add(Me.LblCola)
            Me.gbxBase.Controls.Add(Me.cbxSede)
            Me.gbxBase.Controls.Add(Me.lblSede)
            Me.gbxBase.Controls.Add(Me.dntOFicina)
            Me.gbxBase.Controls.Add(Me.LblOficina)
            Me.gbxBase.Controls.Add(Me.lblFechaInicial)
            Me.gbxBase.Controls.Add(Me.dtpFechaInicial)
            Me.gbxBase.Controls.Add(Me.dtpFechaFinal)
            Me.gbxBase.Controls.Add(Me.lblFechaFinal)
            Me.gbxBase.Location = New System.Drawing.Point(12, 10)
            Me.gbxBase.Name = "gbxBase"
            Me.gbxBase.Size = New System.Drawing.Size(315, 336)
            Me.gbxBase.TabIndex = 17
            Me.gbxBase.TabStop = False
            '
            'cmbColaDocumental
            '
            Me.cmbColaDocumental.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cmbColaDocumental.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cmbColaDocumental.DisabledEnter = False
            Me.cmbColaDocumental.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbColaDocumental.fk_Campo = 0
            Me.cmbColaDocumental.fk_Documento = 0
            Me.cmbColaDocumental.fk_Validacion = 0
            Me.cmbColaDocumental.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cmbColaDocumental.FormattingEnabled = True
            Me.cmbColaDocumental.Location = New System.Drawing.Point(11, 295)
            Me.cmbColaDocumental.Name = "cmbColaDocumental"
            Me.cmbColaDocumental.Size = New System.Drawing.Size(292, 21)
            Me.cmbColaDocumental.TabIndex = 40
            '
            'LblCola
            '
            Me.LblCola.AutoSize = True
            Me.LblCola.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblCola.Location = New System.Drawing.Point(13, 277)
            Me.LblCola.Name = "LblCola"
            Me.LblCola.Size = New System.Drawing.Size(117, 15)
            Me.LblCola.TabIndex = 39
            Me.LblCola.Text = "Cola Documental"
            '
            'cbxSede
            '
            Me.cbxSede.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cbxSede.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cbxSede.DisabledEnter = False
            Me.cbxSede.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxSede.fk_Campo = 0
            Me.cbxSede.fk_Documento = 0
            Me.cbxSede.fk_Validacion = 0
            Me.cbxSede.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cbxSede.FormattingEnabled = True
            Me.cbxSede.Location = New System.Drawing.Point(11, 189)
            Me.cbxSede.Name = "cbxSede"
            Me.cbxSede.Size = New System.Drawing.Size(292, 21)
            Me.cbxSede.TabIndex = 2
            '
            'lblSede
            '
            Me.lblSede.AutoSize = True
            Me.lblSede.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSede.Location = New System.Drawing.Point(14, 171)
            Me.lblSede.Name = "lblSede"
            Me.lblSede.Size = New System.Drawing.Size(40, 15)
            Me.lblSede.TabIndex = 38
            Me.lblSede.Text = "Sede"
            '
            'dntOFicina
            '
            Me.dntOFicina.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.dntOFicina.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.dntOFicina.DisabledEnter = False
            Me.dntOFicina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.dntOFicina.fk_Campo = 0
            Me.dntOFicina.fk_Documento = 0
            Me.dntOFicina.fk_Validacion = 0
            Me.dntOFicina.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.dntOFicina.FormattingEnabled = True
            Me.dntOFicina.Location = New System.Drawing.Point(11, 239)
            Me.dntOFicina.Name = "dntOFicina"
            Me.dntOFicina.Size = New System.Drawing.Size(292, 21)
            Me.dntOFicina.TabIndex = 3
            '
            'LblOficina
            '
            Me.LblOficina.AutoSize = True
            Me.LblOficina.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblOficina.Location = New System.Drawing.Point(15, 221)
            Me.LblOficina.Name = "LblOficina"
            Me.LblOficina.Size = New System.Drawing.Size(52, 15)
            Me.LblOficina.TabIndex = 17
            Me.LblOficina.Text = "Oficina"
            '
            'lblFechaInicial
            '
            Me.lblFechaInicial.AutoSize = True
            Me.lblFechaInicial.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaInicial.Location = New System.Drawing.Point(8, 16)
            Me.lblFechaInicial.Name = "lblFechaInicial"
            Me.lblFechaInicial.Size = New System.Drawing.Size(167, 15)
            Me.lblFechaInicial.TabIndex = 15
            Me.lblFechaInicial.Text = "Fecha Movimiento Inicial"
            '
            'dtpFechaInicial
            '
            Me.dtpFechaInicial.Location = New System.Drawing.Point(11, 35)
            Me.dtpFechaInicial.Name = "dtpFechaInicial"
            Me.dtpFechaInicial.Size = New System.Drawing.Size(292, 20)
            Me.dtpFechaInicial.TabIndex = 0
            '
            'dtpFechaFinal
            '
            Me.dtpFechaFinal.Location = New System.Drawing.Point(11, 91)
            Me.dtpFechaFinal.Name = "dtpFechaFinal"
            Me.dtpFechaFinal.Size = New System.Drawing.Size(292, 20)
            Me.dtpFechaFinal.TabIndex = 1
            '
            'lblFechaFinal
            '
            Me.lblFechaFinal.AutoSize = True
            Me.lblFechaFinal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaFinal.Location = New System.Drawing.Point(13, 67)
            Me.lblFechaFinal.Name = "lblFechaFinal"
            Me.lblFechaFinal.Size = New System.Drawing.Size(160, 15)
            Me.lblFechaFinal.TabIndex = 16
            Me.lblFechaFinal.Text = "Fecha Movimiento Final"
            '
            'btnCancelar
            '
            Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCancelar.Location = New System.Drawing.Point(177, 352)
            Me.btnCancelar.Name = "btnCancelar"
            Me.btnCancelar.Size = New System.Drawing.Size(80, 24)
            Me.btnCancelar.TabIndex = 6
            Me.btnCancelar.Text = "&Cancelar"
            Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnCancelar.UseVisualStyleBackColor = True
            '
            'btnAceptar
            '
            Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnAceptar.Location = New System.Drawing.Point(78, 352)
            Me.btnAceptar.Name = "btnAceptar"
            Me.btnAceptar.Size = New System.Drawing.Size(80, 24)
            Me.btnAceptar.TabIndex = 5
            Me.btnAceptar.Text = "&Aceptar"
            Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnAceptar.UseVisualStyleBackColor = True
            '
            'cmbBancoEpago
            '
            Me.cmbBancoEpago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cmbBancoEpago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cmbBancoEpago.DisabledEnter = False
            Me.cmbBancoEpago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbBancoEpago.fk_Campo = 0
            Me.cmbBancoEpago.fk_Documento = 0
            Me.cmbBancoEpago.fk_Validacion = 0
            Me.cmbBancoEpago.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cmbBancoEpago.FormattingEnabled = True
            Me.cmbBancoEpago.Location = New System.Drawing.Point(11, 141)
            Me.cmbBancoEpago.Name = "cmbBancoEpago"
            Me.cmbBancoEpago.Size = New System.Drawing.Size(292, 21)
            Me.cmbBancoEpago.TabIndex = 41
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(14, 123)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(92, 15)
            Me.Label1.TabIndex = 42
            Me.Label1.Text = "Banco/Epago"
            '
            'FormEfectividad
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(339, 386)
            Me.ControlBox = False
            Me.Controls.Add(Me.gbxBase)
            Me.Controls.Add(Me.btnCancelar)
            Me.Controls.Add(Me.btnAceptar)
            Me.Name = "FormEfectividad"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Filtrar por ..."
            Me.gbxBase.ResumeLayout(False)
            Me.gbxBase.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents gbxBase As System.Windows.Forms.GroupBox
        Friend WithEvents lblFechaInicial As System.Windows.Forms.Label
        Friend WithEvents dtpFechaInicial As System.Windows.Forms.DateTimePicker
        Friend WithEvents dtpFechaFinal As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblFechaFinal As System.Windows.Forms.Label
        Friend WithEvents btnCancelar As System.Windows.Forms.Button
        Friend WithEvents btnAceptar As System.Windows.Forms.Button
        Friend WithEvents LblOficina As System.Windows.Forms.Label
        Friend WithEvents dntOFicina As DesktopComboBoxControl
        Friend WithEvents lblSede As Label
        Friend WithEvents cbxSede As DesktopComboBoxControl
        Friend WithEvents cmbColaDocumental As DesktopComboBoxControl
        Friend WithEvents LblCola As Label
        Friend WithEvents cmbBancoEpago As DesktopComboBoxControl
        Friend WithEvents Label1 As Label
    End Class
End Namespace