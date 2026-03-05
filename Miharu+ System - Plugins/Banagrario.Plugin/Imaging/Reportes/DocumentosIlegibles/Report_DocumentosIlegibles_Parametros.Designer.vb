Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Imaging.Reportes.DocumentosIlegibles

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Report_DocumentosIlegibles_Parametros
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
            Me.gbxBase = New System.Windows.Forms.GroupBox()
            Me.CiudadComboBox = New DesktopComboBoxControl()
            Me.TipoOficinaComboBox = New DesktopComboBoxControl()
            Me.TipoOficinaL = New System.Windows.Forms.Label()
            Me.CiudadL = New System.Windows.Forms.Label()
            Me.lblFechaInicial = New System.Windows.Forms.Label()
            Me.dtpFechaproceso = New System.Windows.Forms.DateTimePicker()
            Me.btnCancelar = New System.Windows.Forms.Button()
            Me.btnAceptar = New System.Windows.Forms.Button()
            Me.gbxBase.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbxBase
            '
            Me.gbxBase.Controls.Add(Me.CiudadComboBox)
            Me.gbxBase.Controls.Add(Me.TipoOficinaComboBox)
            Me.gbxBase.Controls.Add(Me.TipoOficinaL)
            Me.gbxBase.Controls.Add(Me.CiudadL)
            Me.gbxBase.Controls.Add(Me.lblFechaInicial)
            Me.gbxBase.Controls.Add(Me.dtpFechaproceso)
            Me.gbxBase.Location = New System.Drawing.Point(5, 16)
            Me.gbxBase.Name = "gbxBase"
            Me.gbxBase.Size = New System.Drawing.Size(274, 192)
            Me.gbxBase.TabIndex = 23
            Me.gbxBase.TabStop = False
            '
            'CiudadComboBox
            '
            Me.CiudadComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.CiudadComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.CiudadComboBox.DisabledEnter = False
            Me.CiudadComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CiudadComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CiudadComboBox.FormattingEnabled = True
            Me.CiudadComboBox.Location = New System.Drawing.Point(16, 91)
            Me.CiudadComboBox.Name = "CiudadComboBox"
            Me.CiudadComboBox.Size = New System.Drawing.Size(220, 21)
            Me.CiudadComboBox.TabIndex = 7
            '
            'TipoOficinaComboBox
            '
            Me.TipoOficinaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipoOficinaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipoOficinaComboBox.DisabledEnter = False
            Me.TipoOficinaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipoOficinaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipoOficinaComboBox.FormattingEnabled = True
            Me.TipoOficinaComboBox.Location = New System.Drawing.Point(18, 150)
            Me.TipoOficinaComboBox.Name = "TipoOficinaComboBox"
            Me.TipoOficinaComboBox.Size = New System.Drawing.Size(220, 21)
            Me.TipoOficinaComboBox.TabIndex = 6
            '
            'TipoOficinaL
            '
            Me.TipoOficinaL.AutoSize = True
            Me.TipoOficinaL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TipoOficinaL.Location = New System.Drawing.Point(15, 132)
            Me.TipoOficinaL.Name = "TipoOficinaL"
            Me.TipoOficinaL.Size = New System.Drawing.Size(84, 15)
            Me.TipoOficinaL.TabIndex = 5
            Me.TipoOficinaL.Text = "Tipo Oficina"
            '
            'CiudadL
            '
            Me.CiudadL.AutoSize = True
            Me.CiudadL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CiudadL.Location = New System.Drawing.Point(15, 73)
            Me.CiudadL.Name = "CiudadL"
            Me.CiudadL.Size = New System.Drawing.Size(52, 15)
            Me.CiudadL.TabIndex = 4
            Me.CiudadL.Text = "Ciudad"
            '
            'lblFechaInicial
            '
            Me.lblFechaInicial.AutoSize = True
            Me.lblFechaInicial.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaInicial.Location = New System.Drawing.Point(15, 16)
            Me.lblFechaInicial.Name = "lblFechaInicial"
            Me.lblFechaInicial.Size = New System.Drawing.Size(102, 15)
            Me.lblFechaInicial.TabIndex = 1
            Me.lblFechaInicial.Text = "Fecha Proceso"
            '
            'dtpFechaproceso
            '
            Me.dtpFechaproceso.Location = New System.Drawing.Point(18, 35)
            Me.dtpFechaproceso.Name = "dtpFechaproceso"
            Me.dtpFechaproceso.Size = New System.Drawing.Size(218, 20)
            Me.dtpFechaproceso.TabIndex = 0
            '
            'btnCancelar
            '
            Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCancelar.Location = New System.Drawing.Point(163, 223)
            Me.btnCancelar.Name = "btnCancelar"
            Me.btnCancelar.Size = New System.Drawing.Size(80, 24)
            Me.btnCancelar.TabIndex = 22
            Me.btnCancelar.Text = "&Cancelar"
            Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnCancelar.UseVisualStyleBackColor = True
            '
            'btnAceptar
            '
            Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnAceptar.Location = New System.Drawing.Point(64, 223)
            Me.btnAceptar.Name = "btnAceptar"
            Me.btnAceptar.Size = New System.Drawing.Size(80, 24)
            Me.btnAceptar.TabIndex = 21
            Me.btnAceptar.Text = "&Aceptar"
            Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnAceptar.UseVisualStyleBackColor = True
            '
            'Report_DocumentosIlegibles_Parametros
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(284, 262)
            Me.ControlBox = False
            Me.Controls.Add(Me.gbxBase)
            Me.Controls.Add(Me.btnCancelar)
            Me.Controls.Add(Me.btnAceptar)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "Report_DocumentosIlegibles_Parametros"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Filtro Documentos Ilegibles"
            Me.gbxBase.ResumeLayout(False)
            Me.gbxBase.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents gbxBase As System.Windows.Forms.GroupBox
        Friend WithEvents TipoOficinaL As System.Windows.Forms.Label
        Friend WithEvents CiudadL As System.Windows.Forms.Label
        Friend WithEvents lblFechaInicial As System.Windows.Forms.Label
        Friend WithEvents dtpFechaproceso As System.Windows.Forms.DateTimePicker
        Friend WithEvents btnCancelar As System.Windows.Forms.Button
        Friend WithEvents btnAceptar As System.Windows.Forms.Button
        Friend WithEvents CiudadComboBox As DesktopComboBoxControl
        Friend WithEvents TipoOficinaComboBox As DesktopComboBoxControl

    End Class
End Namespace