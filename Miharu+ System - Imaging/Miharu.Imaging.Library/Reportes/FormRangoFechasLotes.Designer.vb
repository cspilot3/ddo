Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Reportes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FormRangoFechasLotes
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
            Me.cbxSede = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblSede = New System.Windows.Forms.Label()
            Me.dntEstado = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.dntOFicina = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.LblOficina = New System.Windows.Forms.Label()
            Me.lblFechaInicial = New System.Windows.Forms.Label()
            Me.dtpFechaInicial = New System.Windows.Forms.DateTimePicker()
            Me.dtpFechaFinal = New System.Windows.Forms.DateTimePicker()
            Me.lblFechaFinal = New System.Windows.Forms.Label()
            Me.btnCancelar = New System.Windows.Forms.Button()
            Me.btnAceptar = New System.Windows.Forms.Button()
            Me.gbxBase.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbxBase
            '
            Me.gbxBase.Controls.Add(Me.cbxSede)
            Me.gbxBase.Controls.Add(Me.lblSede)
            Me.gbxBase.Controls.Add(Me.dntEstado)
            Me.gbxBase.Controls.Add(Me.Label2)
            Me.gbxBase.Controls.Add(Me.dntOFicina)
            Me.gbxBase.Controls.Add(Me.LblOficina)
            Me.gbxBase.Controls.Add(Me.lblFechaInicial)
            Me.gbxBase.Controls.Add(Me.dtpFechaInicial)
            Me.gbxBase.Controls.Add(Me.dtpFechaFinal)
            Me.gbxBase.Controls.Add(Me.lblFechaFinal)
            Me.gbxBase.Location = New System.Drawing.Point(12, 10)
            Me.gbxBase.Name = "gbxBase"
            Me.gbxBase.Size = New System.Drawing.Size(315, 280)
            Me.gbxBase.TabIndex = 17
            Me.gbxBase.TabStop = False
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
            Me.cbxSede.Location = New System.Drawing.Point(11, 142)
            Me.cbxSede.Name = "cbxSede"
            Me.cbxSede.Size = New System.Drawing.Size(292, 21)
            Me.cbxSede.TabIndex = 2
            '
            'lblSede
            '
            Me.lblSede.AutoSize = True
            Me.lblSede.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSede.Location = New System.Drawing.Point(14, 124)
            Me.lblSede.Name = "lblSede"
            Me.lblSede.Size = New System.Drawing.Size(40, 15)
            Me.lblSede.TabIndex = 38
            Me.lblSede.Text = "Sede"
            '
            'dntEstado
            '
            Me.dntEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.dntEstado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.dntEstado.DisabledEnter = False
            Me.dntEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.dntEstado.fk_Campo = 0
            Me.dntEstado.fk_Documento = 0
            Me.dntEstado.fk_Validacion = 0
            Me.dntEstado.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.dntEstado.FormattingEnabled = True
            Me.dntEstado.Location = New System.Drawing.Point(11, 239)
            Me.dntEstado.Name = "dntEstado"
            Me.dntEstado.Size = New System.Drawing.Size(292, 21)
            Me.dntEstado.TabIndex = 4
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(15, 221)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(51, 15)
            Me.Label2.TabIndex = 36
            Me.Label2.Text = "Estado"
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
            Me.dntOFicina.Location = New System.Drawing.Point(11, 192)
            Me.dntOFicina.Name = "dntOFicina"
            Me.dntOFicina.Size = New System.Drawing.Size(292, 21)
            Me.dntOFicina.TabIndex = 3
            '
            'LblOficina
            '
            Me.LblOficina.AutoSize = True
            Me.LblOficina.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblOficina.Location = New System.Drawing.Point(15, 174)
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
            Me.lblFechaInicial.Size = New System.Drawing.Size(181, 15)
            Me.lblFechaInicial.TabIndex = 15
            Me.lblFechaInicial.Text = "Fecha Digitalización Inicial"
            '
            'dtpFechaInicial
            '
            Me.dtpFechaInicial.Location = New System.Drawing.Point(11, 35)
            Me.dtpFechaInicial.Name = "dtpFechaInicial"
            Me.dtpFechaInicial.Size = New System.Drawing.Size(287, 20)
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
            Me.lblFechaFinal.Size = New System.Drawing.Size(174, 15)
            Me.lblFechaFinal.TabIndex = 16
            Me.lblFechaFinal.Text = "Fecha Digitalización Final"
            '
            'btnCancelar
            '
            Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCancelar.Location = New System.Drawing.Point(177, 296)
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
            Me.btnAceptar.Location = New System.Drawing.Point(78, 296)
            Me.btnAceptar.Name = "btnAceptar"
            Me.btnAceptar.Size = New System.Drawing.Size(80, 24)
            Me.btnAceptar.TabIndex = 5
            Me.btnAceptar.Text = "&Aceptar"
            Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnAceptar.UseVisualStyleBackColor = True
            '
            'FormRangoFechasLotes
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(339, 331)
            Me.ControlBox = False
            Me.Controls.Add(Me.gbxBase)
            Me.Controls.Add(Me.btnCancelar)
            Me.Controls.Add(Me.btnAceptar)
            Me.Name = "FormRangoFechasLotes"
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
        Friend WithEvents dntEstado As DesktopComboBoxControl
        Friend WithEvents Label2 As Label
        Friend WithEvents lblSede As Label
        Friend WithEvents cbxSede As DesktopComboBoxControl
    End Class
End Namespace