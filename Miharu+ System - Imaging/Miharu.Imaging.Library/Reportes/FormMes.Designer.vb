Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Reportes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FormMes
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
            Me.cmbMes = New System.Windows.Forms.ComboBox()
            Me.cbxSede = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblSede = New System.Windows.Forms.Label()
            Me.dntEstado = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.dntOFicina = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.LblOficina = New System.Windows.Forms.Label()
            Me.lblFechaInicial = New System.Windows.Forms.Label()
            Me.btnCancelar = New System.Windows.Forms.Button()
            Me.btnAceptar = New System.Windows.Forms.Button()
            Me.gbxBase.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbxBase
            '
            Me.gbxBase.Controls.Add(Me.cmbMes)
            Me.gbxBase.Controls.Add(Me.cbxSede)
            Me.gbxBase.Controls.Add(Me.lblSede)
            Me.gbxBase.Controls.Add(Me.dntEstado)
            Me.gbxBase.Controls.Add(Me.Label2)
            Me.gbxBase.Controls.Add(Me.dntOFicina)
            Me.gbxBase.Controls.Add(Me.LblOficina)
            Me.gbxBase.Controls.Add(Me.lblFechaInicial)
            Me.gbxBase.Location = New System.Drawing.Point(12, 10)
            Me.gbxBase.Name = "gbxBase"
            Me.gbxBase.Size = New System.Drawing.Size(315, 223)
            Me.gbxBase.TabIndex = 17
            Me.gbxBase.TabStop = False
            '
            'cmbMes
            '
            Me.cmbMes.FormattingEnabled = True
            Me.cmbMes.Location = New System.Drawing.Point(5, 38)
            Me.cmbMes.Name = "cmbMes"
            Me.cmbMes.Size = New System.Drawing.Size(141, 21)
            Me.cmbMes.TabIndex = 0
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
            Me.cbxSede.Location = New System.Drawing.Point(5, 88)
            Me.cbxSede.Name = "cbxSede"
            Me.cbxSede.Size = New System.Drawing.Size(292, 21)
            Me.cbxSede.TabIndex = 2
            '
            'lblSede
            '
            Me.lblSede.AutoSize = True
            Me.lblSede.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSede.Location = New System.Drawing.Point(8, 70)
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
            Me.dntEstado.Location = New System.Drawing.Point(5, 185)
            Me.dntEstado.Name = "dntEstado"
            Me.dntEstado.Size = New System.Drawing.Size(292, 21)
            Me.dntEstado.TabIndex = 4
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(9, 167)
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
            Me.dntOFicina.Location = New System.Drawing.Point(5, 138)
            Me.dntOFicina.Name = "dntOFicina"
            Me.dntOFicina.Size = New System.Drawing.Size(292, 21)
            Me.dntOFicina.TabIndex = 3
            '
            'LblOficina
            '
            Me.LblOficina.AutoSize = True
            Me.LblOficina.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblOficina.Location = New System.Drawing.Point(9, 120)
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
            Me.lblFechaInicial.Size = New System.Drawing.Size(34, 15)
            Me.lblFechaInicial.TabIndex = 15
            Me.lblFechaInicial.Text = "Mes"
            '
            'btnCancelar
            '
            Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCancelar.Location = New System.Drawing.Point(177, 243)
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
            Me.btnAceptar.Location = New System.Drawing.Point(78, 243)
            Me.btnAceptar.Name = "btnAceptar"
            Me.btnAceptar.Size = New System.Drawing.Size(80, 24)
            Me.btnAceptar.TabIndex = 5
            Me.btnAceptar.Text = "&Aceptar"
            Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnAceptar.UseVisualStyleBackColor = True
            '
            'FormRangoMes
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(339, 275)
            Me.ControlBox = False
            Me.Controls.Add(Me.gbxBase)
            Me.Controls.Add(Me.btnCancelar)
            Me.Controls.Add(Me.btnAceptar)
            Me.Name = "FormRangoMes"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Filtrar por ..."
            Me.gbxBase.ResumeLayout(False)
            Me.gbxBase.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents gbxBase As System.Windows.Forms.GroupBox
        Friend WithEvents lblFechaInicial As System.Windows.Forms.Label
        Friend WithEvents btnCancelar As System.Windows.Forms.Button
        Friend WithEvents btnAceptar As System.Windows.Forms.Button
        Friend WithEvents LblOficina As System.Windows.Forms.Label
        Friend WithEvents dntOFicina As DesktopComboBoxControl
        Friend WithEvents dntEstado As DesktopComboBoxControl
        Friend WithEvents Label2 As Label
        Friend WithEvents lblSede As Label
        Friend WithEvents cbxSede As DesktopComboBoxControl
        Friend WithEvents cmbMes As ComboBox
    End Class
End Namespace