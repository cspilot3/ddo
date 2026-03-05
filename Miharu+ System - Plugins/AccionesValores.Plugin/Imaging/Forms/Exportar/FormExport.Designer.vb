Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Imaging.Forms.Exportar
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormExport
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormExport))
            Me.gbxBase = New System.Windows.Forms.GroupBox()
            Me.DocumentoComboBox = New DesktopComboBoxControl()
            Me.EsquemaComboBox = New DesktopComboBoxControl()
            Me.lblTipologia = New System.Windows.Forms.Label()
            Me.lblFechaInicial = New System.Windows.Forms.Label()
            Me.dtpFechaInicial = New System.Windows.Forms.DateTimePicker()
            Me.dtpFechaFinal = New System.Windows.Forms.DateTimePicker()
            Me.BuscarCarpetaButton = New System.Windows.Forms.Button()
            Me.lblFechaFinal = New System.Windows.Forms.Label()
            Me.lblCarpeta = New System.Windows.Forms.Label()
            Me.CarpetaSalidaTextBox = New System.Windows.Forms.TextBox()
            Me.fbdCarpetaSalida = New System.Windows.Forms.FolderBrowserDialog()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.ExportarButton = New System.Windows.Forms.Button()
            Me.lblEsquema = New System.Windows.Forms.Label()
            Me.gbxBase.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbxBase
            '
            Me.gbxBase.Controls.Add(Me.DocumentoComboBox)
            Me.gbxBase.Controls.Add(Me.EsquemaComboBox)
            Me.gbxBase.Controls.Add(Me.lblTipologia)
            Me.gbxBase.Controls.Add(Me.lblEsquema)
            Me.gbxBase.Controls.Add(Me.lblFechaInicial)
            Me.gbxBase.Controls.Add(Me.dtpFechaInicial)
            Me.gbxBase.Controls.Add(Me.dtpFechaFinal)
            Me.gbxBase.Controls.Add(Me.BuscarCarpetaButton)
            Me.gbxBase.Controls.Add(Me.lblFechaFinal)
            Me.gbxBase.Controls.Add(Me.lblCarpeta)
            Me.gbxBase.Controls.Add(Me.CarpetaSalidaTextBox)
            Me.gbxBase.Location = New System.Drawing.Point(12, 12)
            Me.gbxBase.Name = "gbxBase"
            Me.gbxBase.Size = New System.Drawing.Size(336, 266)
            Me.gbxBase.TabIndex = 17
            Me.gbxBase.TabStop = False
            Me.gbxBase.Text = "Parametros de exportación"
            '
            'DocumentoComboBox
            '
            Me.DocumentoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DocumentoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DocumentoComboBox.DisabledEnter = False
            Me.DocumentoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DocumentoComboBox.FormattingEnabled = True
            Me.DocumentoComboBox.Location = New System.Drawing.Point(13, 85)
            Me.DocumentoComboBox.Name = "DocumentoComboBox"
            Me.DocumentoComboBox.Size = New System.Drawing.Size(312, 21)
            Me.DocumentoComboBox.TabIndex = 15
            '
            'EsquemaComboBox
            '
            Me.EsquemaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaComboBox.DisabledEnter = False
            Me.EsquemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaComboBox.FormattingEnabled = True
            Me.EsquemaComboBox.Location = New System.Drawing.Point(13, 43)
            Me.EsquemaComboBox.Name = "EsquemaComboBox"
            Me.EsquemaComboBox.Size = New System.Drawing.Size(312, 21)
            Me.EsquemaComboBox.TabIndex = 14
            '
            'lblTipologia
            '
            Me.lblTipologia.AutoSize = True
            Me.lblTipologia.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTipologia.Location = New System.Drawing.Point(11, 67)
            Me.lblTipologia.Name = "lblTipologia"
            Me.lblTipologia.Size = New System.Drawing.Size(80, 15)
            Me.lblTipologia.TabIndex = 12
            Me.lblTipologia.Text = "Documento"
            '
            'lblFechaInicial
            '
            Me.lblFechaInicial.AutoSize = True
            Me.lblFechaInicial.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaInicial.Location = New System.Drawing.Point(10, 133)
            Me.lblFechaInicial.Name = "lblFechaInicial"
            Me.lblFechaInicial.Size = New System.Drawing.Size(89, 15)
            Me.lblFechaInicial.TabIndex = 1
            Me.lblFechaInicial.Text = "Fecha Inicial"
            '
            'dtpFechaInicial
            '
            Me.dtpFechaInicial.Location = New System.Drawing.Point(107, 133)
            Me.dtpFechaInicial.Name = "dtpFechaInicial"
            Me.dtpFechaInicial.Size = New System.Drawing.Size(218, 20)
            Me.dtpFechaInicial.TabIndex = 0
            '
            'dtpFechaFinal
            '
            Me.dtpFechaFinal.Location = New System.Drawing.Point(107, 169)
            Me.dtpFechaFinal.Name = "dtpFechaFinal"
            Me.dtpFechaFinal.Size = New System.Drawing.Size(218, 20)
            Me.dtpFechaFinal.TabIndex = 2
            '
            'BuscarCarpetaButton
            '
            Me.BuscarCarpetaButton.Image = Global.AccionesValores.Plugin.My.Resources.Resources.btnDestape
            Me.BuscarCarpetaButton.Location = New System.Drawing.Point(288, 217)
            Me.BuscarCarpetaButton.Name = "BuscarCarpetaButton"
            Me.BuscarCarpetaButton.Size = New System.Drawing.Size(37, 30)
            Me.BuscarCarpetaButton.TabIndex = 8
            Me.BuscarCarpetaButton.UseVisualStyleBackColor = True
            '
            'lblFechaFinal
            '
            Me.lblFechaFinal.AutoSize = True
            Me.lblFechaFinal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaFinal.Location = New System.Drawing.Point(10, 169)
            Me.lblFechaFinal.Name = "lblFechaFinal"
            Me.lblFechaFinal.Size = New System.Drawing.Size(82, 15)
            Me.lblFechaFinal.TabIndex = 3
            Me.lblFechaFinal.Text = "Fecha Final"
            '
            'lblCarpeta
            '
            Me.lblCarpeta.AutoSize = True
            Me.lblCarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCarpeta.Location = New System.Drawing.Point(10, 207)
            Me.lblCarpeta.Name = "lblCarpeta"
            Me.lblCarpeta.Size = New System.Drawing.Size(122, 15)
            Me.lblCarpeta.TabIndex = 7
            Me.lblCarpeta.Text = "Carpeta de Salida"
            '
            'CarpetaSalidaTextBox
            '
            Me.CarpetaSalidaTextBox.Location = New System.Drawing.Point(13, 227)
            Me.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox"
            Me.CarpetaSalidaTextBox.Size = New System.Drawing.Size(269, 20)
            Me.CarpetaSalidaTextBox.TabIndex = 4
            '
            'CancelarButton
            '
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.AccionesValores.Plugin.My.Resources.Resources.cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(248, 293)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(89, 31)
            Me.CancelarButton.TabIndex = 16
            Me.CancelarButton.Text = "Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'ExportarButton
            '
            Me.ExportarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ExportarButton.Image = Global.AccionesValores.Plugin.My.Resources.Resources.Aceptar
            Me.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ExportarButton.Location = New System.Drawing.Point(149, 293)
            Me.ExportarButton.Name = "ExportarButton"
            Me.ExportarButton.Size = New System.Drawing.Size(89, 31)
            Me.ExportarButton.TabIndex = 15
            Me.ExportarButton.Text = "Exportar"
            Me.ExportarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ExportarButton.UseVisualStyleBackColor = True
            '
            'lblEsquema
            '
            Me.lblEsquema.AutoSize = True
            Me.lblEsquema.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblEsquema.Location = New System.Drawing.Point(10, 25)
            Me.lblEsquema.Name = "lblEsquema"
            Me.lblEsquema.Size = New System.Drawing.Size(67, 15)
            Me.lblEsquema.TabIndex = 10
            Me.lblEsquema.Text = "Esquema"
            '
            'FormExport
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(362, 331)
            Me.Controls.Add(Me.gbxBase)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.ExportarButton)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormExport"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Exportar Imagenes"
            Me.gbxBase.ResumeLayout(False)
            Me.gbxBase.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents gbxBase As System.Windows.Forms.GroupBox
        Friend WithEvents DocumentoComboBox As DesktopComboBoxControl
        Friend WithEvents EsquemaComboBox As DesktopComboBoxControl
        Friend WithEvents lblTipologia As System.Windows.Forms.Label
        Friend WithEvents lblFechaInicial As System.Windows.Forms.Label
        Friend WithEvents dtpFechaInicial As System.Windows.Forms.DateTimePicker
        Friend WithEvents dtpFechaFinal As System.Windows.Forms.DateTimePicker
        Friend WithEvents BuscarCarpetaButton As System.Windows.Forms.Button
        Friend WithEvents lblFechaFinal As System.Windows.Forms.Label
        Friend WithEvents lblCarpeta As System.Windows.Forms.Label
        Friend WithEvents CarpetaSalidaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents ExportarButton As System.Windows.Forms.Button
        Friend WithEvents fbdCarpetaSalida As System.Windows.Forms.FolderBrowserDialog
        Friend WithEvents lblEsquema As System.Windows.Forms.Label
    End Class
End Namespace