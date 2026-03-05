Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Reportes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FormRangoFechasColaD
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
            Me.cmbMesInicial = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.ChBMensual = New System.Windows.Forms.CheckBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.dntColaDtal = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.LblCola = New System.Windows.Forms.Label()
            Me.dntOFicina = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.OficinaLabel = New System.Windows.Forms.Label()
            Me.lblFechaInicial = New System.Windows.Forms.Label()
            Me.dtpFechaInicial = New System.Windows.Forms.DateTimePicker()
            Me.dtpFechaFinal = New System.Windows.Forms.DateTimePicker()
            Me.btnCancelar = New System.Windows.Forms.Button()
            Me.btnAceptar = New System.Windows.Forms.Button()
            Me.gbxBase.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbxBase
            '
            Me.gbxBase.Controls.Add(Me.cmbMesInicial)
            Me.gbxBase.Controls.Add(Me.ChBMensual)
            Me.gbxBase.Controls.Add(Me.Label1)
            Me.gbxBase.Controls.Add(Me.dntColaDtal)
            Me.gbxBase.Controls.Add(Me.LblCola)
            Me.gbxBase.Controls.Add(Me.dntOFicina)
            Me.gbxBase.Controls.Add(Me.OficinaLabel)
            Me.gbxBase.Controls.Add(Me.lblFechaInicial)
            Me.gbxBase.Controls.Add(Me.dtpFechaInicial)
            Me.gbxBase.Controls.Add(Me.dtpFechaFinal)
            Me.gbxBase.Location = New System.Drawing.Point(12, 2)
            Me.gbxBase.Name = "gbxBase"
            Me.gbxBase.Size = New System.Drawing.Size(297, 227)
            Me.gbxBase.TabIndex = 17
            Me.gbxBase.TabStop = False
            '
            'cmbMesInicial
            '
            Me.cmbMesInicial.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cmbMesInicial.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cmbMesInicial.DisabledEnter = False
            Me.cmbMesInicial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbMesInicial.fk_Campo = 0
            Me.cmbMesInicial.fk_Documento = 0
            Me.cmbMesInicial.fk_Validacion = 0
            Me.cmbMesInicial.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cmbMesInicial.FormattingEnabled = True
            Me.cmbMesInicial.Location = New System.Drawing.Point(18, 82)
            Me.cmbMesInicial.Name = "cmbMesInicial"
            Me.cmbMesInicial.Size = New System.Drawing.Size(220, 21)
            Me.cmbMesInicial.TabIndex = 36
            Me.cmbMesInicial.Visible = False
            '
            'ChBMensual
            '
            Me.ChBMensual.AutoSize = True
            Me.ChBMensual.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ChBMensual.Location = New System.Drawing.Point(158, 32)
            Me.ChBMensual.Name = "ChBMensual"
            Me.ChBMensual.Size = New System.Drawing.Size(15, 14)
            Me.ChBMensual.TabIndex = 35
            Me.ChBMensual.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(15, 30)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(117, 15)
            Me.Label1.TabIndex = 34
            Me.Label1.Text = "Reporte Mensual"
            '
            'dntColaDtal
            '
            Me.dntColaDtal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.dntColaDtal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.dntColaDtal.DisabledEnter = False
            Me.dntColaDtal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.dntColaDtal.fk_Campo = 0
            Me.dntColaDtal.fk_Documento = 0
            Me.dntColaDtal.fk_Validacion = 0
            Me.dntColaDtal.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.dntColaDtal.FormattingEnabled = True
            Me.dntColaDtal.Location = New System.Drawing.Point(8, 187)
            Me.dntColaDtal.Name = "dntColaDtal"
            Me.dntColaDtal.Size = New System.Drawing.Size(280, 21)
            Me.dntColaDtal.TabIndex = 33
            '
            'LblCola
            '
            Me.LblCola.AutoSize = True
            Me.LblCola.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblCola.Location = New System.Drawing.Point(15, 169)
            Me.LblCola.Name = "LblCola"
            Me.LblCola.Size = New System.Drawing.Size(117, 15)
            Me.LblCola.TabIndex = 32
            Me.LblCola.Text = "Cola Documental"
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
            Me.dntOFicina.Location = New System.Drawing.Point(8, 138)
            Me.dntOFicina.Name = "dntOFicina"
            Me.dntOFicina.Size = New System.Drawing.Size(280, 21)
            Me.dntOFicina.TabIndex = 31
            '
            'OficinaLabel
            '
            Me.OficinaLabel.AutoSize = True
            Me.OficinaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OficinaLabel.Location = New System.Drawing.Point(15, 120)
            Me.OficinaLabel.Name = "OficinaLabel"
            Me.OficinaLabel.Size = New System.Drawing.Size(52, 15)
            Me.OficinaLabel.TabIndex = 17
            Me.OficinaLabel.Text = "Oficina"
            '
            'lblFechaInicial
            '
            Me.lblFechaInicial.AutoSize = True
            Me.lblFechaInicial.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaInicial.Location = New System.Drawing.Point(15, 64)
            Me.lblFechaInicial.Name = "lblFechaInicial"
            Me.lblFechaInicial.Size = New System.Drawing.Size(89, 15)
            Me.lblFechaInicial.TabIndex = 15
            Me.lblFechaInicial.Text = "Fecha Inicial"
            '
            'dtpFechaInicial
            '
            Me.dtpFechaInicial.Location = New System.Drawing.Point(18, 83)
            Me.dtpFechaInicial.Name = "dtpFechaInicial"
            Me.dtpFechaInicial.Size = New System.Drawing.Size(220, 20)
            Me.dtpFechaInicial.TabIndex = 0
            '
            'dtpFechaFinal
            '
            Me.dtpFechaFinal.Location = New System.Drawing.Point(177, 109)
            Me.dtpFechaFinal.Name = "dtpFechaFinal"
            Me.dtpFechaFinal.Size = New System.Drawing.Size(111, 20)
            Me.dtpFechaFinal.TabIndex = 1
            Me.dtpFechaFinal.Visible = False
            '
            'btnCancelar
            '
            Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCancelar.Location = New System.Drawing.Point(170, 237)
            Me.btnCancelar.Name = "btnCancelar"
            Me.btnCancelar.Size = New System.Drawing.Size(80, 24)
            Me.btnCancelar.TabIndex = 4
            Me.btnCancelar.Text = "&Cancelar"
            Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnCancelar.UseVisualStyleBackColor = True
            '
            'btnAceptar
            '
            Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnAceptar.Location = New System.Drawing.Point(71, 237)
            Me.btnAceptar.Name = "btnAceptar"
            Me.btnAceptar.Size = New System.Drawing.Size(80, 24)
            Me.btnAceptar.TabIndex = 3
            Me.btnAceptar.Text = "&Aceptar"
            Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnAceptar.UseVisualStyleBackColor = True
            '
            'FormRangoFechasColaD
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(321, 281)
            Me.ControlBox = False
            Me.Controls.Add(Me.gbxBase)
            Me.Controls.Add(Me.btnCancelar)
            Me.Controls.Add(Me.btnAceptar)
            Me.Name = "FormRangoFechasColaD"
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
        Friend WithEvents btnCancelar As System.Windows.Forms.Button
        Friend WithEvents btnAceptar As System.Windows.Forms.Button
        Friend WithEvents OficinaLabel As System.Windows.Forms.Label
        Friend WithEvents dntOFicina As DesktopComboBoxControl
        Friend WithEvents dntColaDtal As DesktopComboBoxControl
        Friend WithEvents LblCola As Label
        Friend WithEvents Label1 As Label
        Friend WithEvents ChBMensual As CheckBox
        Friend WithEvents cmbMesInicial As DesktopComboBoxControl
    End Class
End Namespace