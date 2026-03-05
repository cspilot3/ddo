Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Imaging.Forms.Correciones

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCorrecciones
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCorrecciones))
            Me.LblContenedor = New System.Windows.Forms.Label()
            Me.LblCargue = New System.Windows.Forms.Label()
            Me.LblPaquete = New System.Windows.Forms.Label()
            Me.LblValorContenedor = New System.Windows.Forms.Label()
            Me.LblValorCargue = New System.Windows.Forms.Label()
            Me.LblValorPaquete = New System.Windows.Forms.Label()
            Me.LblTipoCambio = New System.Windows.Forms.Label()
            Me.CBFechaMovimiento = New System.Windows.Forms.CheckBox()
            Me.CBCodigoOficina = New System.Windows.Forms.CheckBox()
            Me.LblNuevaFechaMovimiento = New System.Windows.Forms.Label()
            Me.LblNuevoCodigoOficina = New System.Windows.Forms.Label()
            Me.LblOrigenError = New System.Windows.Forms.Label()
            Me.RdbFuncionarioBanco = New System.Windows.Forms.RadioButton()
            Me.RdbFuncionariopyc = New System.Windows.Forms.RadioButton()
            Me.BtnGuardar = New System.Windows.Forms.Button()
            Me.FechaProcesodateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.OficinaDesktopComboBox = New DesktopComboBoxControl()
            Me.Lblfk_Proceso = New System.Windows.Forms.Label()
            Me.LblFecMovAnterior = New System.Windows.Forms.Label()
            Me.LblCodOfiAnterior = New System.Windows.Forms.Label()
            Me.DatosCarguePanel = New System.Windows.Forms.Panel()
            Me.TipoCambioPanel = New System.Windows.Forms.Panel()
            Me.CambioLLavesPanel = New System.Windows.Forms.Panel()
            Me.OrigenErrorPanel = New System.Windows.Forms.Panel()
            Me.DatosCarguePanel.SuspendLayout()
            Me.TipoCambioPanel.SuspendLayout()
            Me.CambioLLavesPanel.SuspendLayout()
            Me.OrigenErrorPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'LblContenedor
            '
            Me.LblContenedor.AutoSize = True
            Me.LblContenedor.Location = New System.Drawing.Point(12, 9)
            Me.LblContenedor.Name = "LblContenedor"
            Me.LblContenedor.Size = New System.Drawing.Size(68, 13)
            Me.LblContenedor.TabIndex = 0
            Me.LblContenedor.Text = "Contenedor: "
            '
            'LblCargue
            '
            Me.LblCargue.AutoSize = True
            Me.LblCargue.Location = New System.Drawing.Point(12, 32)
            Me.LblCargue.Name = "LblCargue"
            Me.LblCargue.Size = New System.Drawing.Size(47, 13)
            Me.LblCargue.TabIndex = 1
            Me.LblCargue.Text = "Cargue: "
            '
            'LblPaquete
            '
            Me.LblPaquete.AutoSize = True
            Me.LblPaquete.Location = New System.Drawing.Point(12, 57)
            Me.LblPaquete.Name = "LblPaquete"
            Me.LblPaquete.Size = New System.Drawing.Size(53, 13)
            Me.LblPaquete.TabIndex = 2
            Me.LblPaquete.Text = "Paquete: "
            '
            'LblValorContenedor
            '
            Me.LblValorContenedor.AutoSize = True
            Me.LblValorContenedor.Location = New System.Drawing.Point(110, 9)
            Me.LblValorContenedor.Name = "LblValorContenedor"
            Me.LblValorContenedor.Size = New System.Drawing.Size(30, 13)
            Me.LblValorContenedor.TabIndex = 3
            Me.LblValorContenedor.Text = "valor"
            '
            'LblValorCargue
            '
            Me.LblValorCargue.AutoSize = True
            Me.LblValorCargue.Location = New System.Drawing.Point(110, 32)
            Me.LblValorCargue.Name = "LblValorCargue"
            Me.LblValorCargue.Size = New System.Drawing.Size(30, 13)
            Me.LblValorCargue.TabIndex = 4
            Me.LblValorCargue.Text = "valor"
            '
            'LblValorPaquete
            '
            Me.LblValorPaquete.AutoSize = True
            Me.LblValorPaquete.Location = New System.Drawing.Point(110, 57)
            Me.LblValorPaquete.Name = "LblValorPaquete"
            Me.LblValorPaquete.Size = New System.Drawing.Size(30, 13)
            Me.LblValorPaquete.TabIndex = 5
            Me.LblValorPaquete.Text = "valor"
            '
            'LblTipoCambio
            '
            Me.LblTipoCambio.AutoSize = True
            Me.LblTipoCambio.Location = New System.Drawing.Point(12, 17)
            Me.LblTipoCambio.Name = "LblTipoCambio"
            Me.LblTipoCambio.Size = New System.Drawing.Size(87, 13)
            Me.LblTipoCambio.TabIndex = 6
            Me.LblTipoCambio.Text = "Tipo de Cambio: "
            '
            'CBFechaMovimiento
            '
            Me.CBFechaMovimiento.AutoSize = True
            Me.CBFechaMovimiento.Location = New System.Drawing.Point(113, 17)
            Me.CBFechaMovimiento.Name = "CBFechaMovimiento"
            Me.CBFechaMovimiento.Size = New System.Drawing.Size(128, 17)
            Me.CBFechaMovimiento.TabIndex = 7
            Me.CBFechaMovimiento.Text = "Fecha de Movimiento"
            Me.CBFechaMovimiento.UseVisualStyleBackColor = True
            '
            'CBCodigoOficina
            '
            Me.CBCodigoOficina.AutoSize = True
            Me.CBCodigoOficina.Location = New System.Drawing.Point(113, 41)
            Me.CBCodigoOficina.Name = "CBCodigoOficina"
            Me.CBCodigoOficina.Size = New System.Drawing.Size(110, 17)
            Me.CBCodigoOficina.TabIndex = 8
            Me.CBCodigoOficina.Text = "Codigo de Oficina"
            Me.CBCodigoOficina.UseVisualStyleBackColor = True
            '
            'LblNuevaFechaMovimiento
            '
            Me.LblNuevaFechaMovimiento.AutoSize = True
            Me.LblNuevaFechaMovimiento.Location = New System.Drawing.Point(12, 12)
            Me.LblNuevaFechaMovimiento.Name = "LblNuevaFechaMovimiento"
            Me.LblNuevaFechaMovimiento.Size = New System.Drawing.Size(150, 13)
            Me.LblNuevaFechaMovimiento.TabIndex = 9
            Me.LblNuevaFechaMovimiento.Text = "Nueva Fecha de Movimiento: "
            '
            'LblNuevoCodigoOficina
            '
            Me.LblNuevoCodigoOficina.AutoSize = True
            Me.LblNuevoCodigoOficina.Location = New System.Drawing.Point(12, 49)
            Me.LblNuevoCodigoOficina.Name = "LblNuevoCodigoOficina"
            Me.LblNuevoCodigoOficina.Size = New System.Drawing.Size(132, 13)
            Me.LblNuevoCodigoOficina.TabIndex = 11
            Me.LblNuevoCodigoOficina.Text = "Nuevo Código de Oficina: "
            '
            'LblOrigenError
            '
            Me.LblOrigenError.AutoSize = True
            Me.LblOrigenError.Location = New System.Drawing.Point(67, 13)
            Me.LblOrigenError.Name = "LblOrigenError"
            Me.LblOrigenError.Size = New System.Drawing.Size(86, 13)
            Me.LblOrigenError.TabIndex = 13
            Me.LblOrigenError.Text = "Origen del Error: "
            '
            'RdbFuncionarioBanco
            '
            Me.RdbFuncionarioBanco.AutoSize = True
            Me.RdbFuncionarioBanco.Checked = True
            Me.RdbFuncionarioBanco.Location = New System.Drawing.Point(165, 11)
            Me.RdbFuncionarioBanco.Name = "RdbFuncionarioBanco"
            Me.RdbFuncionarioBanco.Size = New System.Drawing.Size(114, 17)
            Me.RdbFuncionarioBanco.TabIndex = 14
            Me.RdbFuncionarioBanco.TabStop = True
            Me.RdbFuncionarioBanco.Text = "Funcionario Banco"
            Me.RdbFuncionarioBanco.UseVisualStyleBackColor = True
            '
            'RdbFuncionariopyc
            '
            Me.RdbFuncionariopyc.AutoSize = True
            Me.RdbFuncionariopyc.Location = New System.Drawing.Point(165, 34)
            Me.RdbFuncionariopyc.Name = "RdbFuncionariopyc"
            Me.RdbFuncionariopyc.Size = New System.Drawing.Size(104, 17)
            Me.RdbFuncionariopyc.TabIndex = 15
            Me.RdbFuncionariopyc.Text = "Funcionario PYC"
            Me.RdbFuncionariopyc.UseVisualStyleBackColor = True
            '
            'BtnGuardar
            '
            Me.BtnGuardar.Location = New System.Drawing.Point(146, 346)
            Me.BtnGuardar.Name = "BtnGuardar"
            Me.BtnGuardar.Size = New System.Drawing.Size(75, 23)
            Me.BtnGuardar.TabIndex = 16
            Me.BtnGuardar.Text = "Guardar"
            Me.BtnGuardar.UseVisualStyleBackColor = True
            '
            'FechaProcesodateTimePicker
            '
            Me.FechaProcesodateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaProcesodateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaProcesodateTimePicker.Location = New System.Drawing.Point(168, 12)
            Me.FechaProcesodateTimePicker.Name = "FechaProcesodateTimePicker"
            Me.FechaProcesodateTimePicker.Size = New System.Drawing.Size(81, 20)
            Me.FechaProcesodateTimePicker.TabIndex = 29
            '
            'OficinaDesktopComboBox
            '
            Me.OficinaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OficinaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OficinaDesktopComboBox.DisabledEnter = False
            Me.OficinaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OficinaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OficinaDesktopComboBox.FormattingEnabled = True
            Me.OficinaDesktopComboBox.Location = New System.Drawing.Point(168, 46)
            Me.OficinaDesktopComboBox.Name = "OficinaDesktopComboBox"
            Me.OficinaDesktopComboBox.Size = New System.Drawing.Size(206, 21)
            Me.OficinaDesktopComboBox.TabIndex = 30
            '
            'Lblfk_Proceso
            '
            Me.Lblfk_Proceso.AutoSize = True
            Me.Lblfk_Proceso.Location = New System.Drawing.Point(280, 332)
            Me.Lblfk_Proceso.Name = "Lblfk_Proceso"
            Me.Lblfk_Proceso.Size = New System.Drawing.Size(75, 13)
            Me.Lblfk_Proceso.TabIndex = 31
            Me.Lblfk_Proceso.Text = "Lblfk_Proceso"
            Me.Lblfk_Proceso.Visible = False
            '
            'LblFecMovAnterior
            '
            Me.LblFecMovAnterior.AutoSize = True
            Me.LblFecMovAnterior.Location = New System.Drawing.Point(280, 351)
            Me.LblFecMovAnterior.Name = "LblFecMovAnterior"
            Me.LblFecMovAnterior.Size = New System.Drawing.Size(96, 13)
            Me.LblFecMovAnterior.TabIndex = 32
            Me.LblFecMovAnterior.Text = "LblFecMovAnterior"
            Me.LblFecMovAnterior.Visible = False
            '
            'LblCodOfiAnterior
            '
            Me.LblCodOfiAnterior.AutoSize = True
            Me.LblCodOfiAnterior.Location = New System.Drawing.Point(280, 371)
            Me.LblCodOfiAnterior.Name = "LblCodOfiAnterior"
            Me.LblCodOfiAnterior.Size = New System.Drawing.Size(89, 13)
            Me.LblCodOfiAnterior.TabIndex = 33
            Me.LblCodOfiAnterior.Text = "LblCodOfiAnterior"
            Me.LblCodOfiAnterior.Visible = False
            '
            'DatosCarguePanel
            '
            Me.DatosCarguePanel.Controls.Add(Me.LblContenedor)
            Me.DatosCarguePanel.Controls.Add(Me.LblCargue)
            Me.DatosCarguePanel.Controls.Add(Me.LblPaquete)
            Me.DatosCarguePanel.Controls.Add(Me.LblValorContenedor)
            Me.DatosCarguePanel.Controls.Add(Me.LblValorCargue)
            Me.DatosCarguePanel.Controls.Add(Me.LblValorPaquete)
            Me.DatosCarguePanel.Location = New System.Drawing.Point(15, 12)
            Me.DatosCarguePanel.Name = "DatosCarguePanel"
            Me.DatosCarguePanel.Size = New System.Drawing.Size(377, 80)
            Me.DatosCarguePanel.TabIndex = 34
            '
            'TipoCambioPanel
            '
            Me.TipoCambioPanel.Controls.Add(Me.LblTipoCambio)
            Me.TipoCambioPanel.Controls.Add(Me.CBFechaMovimiento)
            Me.TipoCambioPanel.Controls.Add(Me.CBCodigoOficina)
            Me.TipoCambioPanel.Location = New System.Drawing.Point(15, 98)
            Me.TipoCambioPanel.Name = "TipoCambioPanel"
            Me.TipoCambioPanel.Size = New System.Drawing.Size(377, 72)
            Me.TipoCambioPanel.TabIndex = 35
            '
            'CambioLLavesPanel
            '
            Me.CambioLLavesPanel.Controls.Add(Me.LblNuevaFechaMovimiento)
            Me.CambioLLavesPanel.Controls.Add(Me.LblNuevoCodigoOficina)
            Me.CambioLLavesPanel.Controls.Add(Me.FechaProcesodateTimePicker)
            Me.CambioLLavesPanel.Controls.Add(Me.OficinaDesktopComboBox)
            Me.CambioLLavesPanel.Location = New System.Drawing.Point(15, 177)
            Me.CambioLLavesPanel.Name = "CambioLLavesPanel"
            Me.CambioLLavesPanel.Size = New System.Drawing.Size(377, 73)
            Me.CambioLLavesPanel.TabIndex = 36
            '
            'OrigenErrorPanel
            '
            Me.OrigenErrorPanel.Controls.Add(Me.LblOrigenError)
            Me.OrigenErrorPanel.Controls.Add(Me.RdbFuncionarioBanco)
            Me.OrigenErrorPanel.Controls.Add(Me.RdbFuncionariopyc)
            Me.OrigenErrorPanel.Location = New System.Drawing.Point(15, 256)
            Me.OrigenErrorPanel.Name = "OrigenErrorPanel"
            Me.OrigenErrorPanel.Size = New System.Drawing.Size(377, 64)
            Me.OrigenErrorPanel.TabIndex = 37
            '
            'FormCorrecciones
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(404, 390)
            Me.Controls.Add(Me.OrigenErrorPanel)
            Me.Controls.Add(Me.CambioLLavesPanel)
            Me.Controls.Add(Me.TipoCambioPanel)
            Me.Controls.Add(Me.DatosCarguePanel)
            Me.Controls.Add(Me.LblCodOfiAnterior)
            Me.Controls.Add(Me.LblFecMovAnterior)
            Me.Controls.Add(Me.Lblfk_Proceso)
            Me.Controls.Add(Me.BtnGuardar)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormCorrecciones"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Correcciones"
            Me.DatosCarguePanel.ResumeLayout(False)
            Me.DatosCarguePanel.PerformLayout()
            Me.TipoCambioPanel.ResumeLayout(False)
            Me.TipoCambioPanel.PerformLayout()
            Me.CambioLLavesPanel.ResumeLayout(False)
            Me.CambioLLavesPanel.PerformLayout()
            Me.OrigenErrorPanel.ResumeLayout(False)
            Me.OrigenErrorPanel.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents LblContenedor As System.Windows.Forms.Label
        Friend WithEvents LblCargue As System.Windows.Forms.Label
        Friend WithEvents LblPaquete As System.Windows.Forms.Label
        Friend WithEvents LblValorContenedor As System.Windows.Forms.Label
        Friend WithEvents LblValorCargue As System.Windows.Forms.Label
        Friend WithEvents LblValorPaquete As System.Windows.Forms.Label
        Friend WithEvents LblTipoCambio As System.Windows.Forms.Label
        Friend WithEvents CBFechaMovimiento As System.Windows.Forms.CheckBox
        Friend WithEvents CBCodigoOficina As System.Windows.Forms.CheckBox
        Friend WithEvents LblNuevaFechaMovimiento As System.Windows.Forms.Label
        Friend WithEvents LblNuevoCodigoOficina As System.Windows.Forms.Label
        Friend WithEvents LblOrigenError As System.Windows.Forms.Label
        Friend WithEvents RdbFuncionarioBanco As System.Windows.Forms.RadioButton
        Friend WithEvents RdbFuncionariopyc As System.Windows.Forms.RadioButton
        Friend WithEvents BtnGuardar As System.Windows.Forms.Button
        Private WithEvents FechaProcesodateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents OficinaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Lblfk_Proceso As System.Windows.Forms.Label
        Friend WithEvents LblFecMovAnterior As System.Windows.Forms.Label
        Friend WithEvents LblCodOfiAnterior As System.Windows.Forms.Label
        Friend WithEvents DatosCarguePanel As System.Windows.Forms.Panel
        Friend WithEvents TipoCambioPanel As System.Windows.Forms.Panel
        Friend WithEvents CambioLLavesPanel As System.Windows.Forms.Panel
        Friend WithEvents OrigenErrorPanel As System.Windows.Forms.Panel
    End Class
End Namespace