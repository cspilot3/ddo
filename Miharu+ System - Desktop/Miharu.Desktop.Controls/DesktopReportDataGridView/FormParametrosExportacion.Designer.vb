Namespace DesktopReportDataGridView
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParametrosExportacion
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
            Me.chkEncabezado = New System.Windows.Forms.CheckBox()
            Me.OpcionesSeparadorGroupBox = New System.Windows.Forms.GroupBox()
            Me.CodificacionArchivoComboBox = New System.Windows.Forms.ComboBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.txtRadioButton = New System.Windows.Forms.RadioButton()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ExcelRadioButton = New System.Windows.Forms.RadioButton()
            Me.VacioRadioButton = New System.Windows.Forms.RadioButton()
            Me.PuntoComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.TabuladorRadioButton = New System.Windows.Forms.RadioButton()
            Me.ComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.ok = New System.Windows.Forms.Button()
            Me.OpcionesSeparadorGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'chkEncabezado
            '
            Me.chkEncabezado.AutoSize = True
            Me.chkEncabezado.Location = New System.Drawing.Point(18, 217)
            Me.chkEncabezado.Name = "chkEncabezado"
            Me.chkEncabezado.Size = New System.Drawing.Size(123, 17)
            Me.chkEncabezado.TabIndex = 16
            Me.chkEncabezado.Text = "Maneja encabezado"
            Me.chkEncabezado.UseVisualStyleBackColor = True
            '
            'OpcionesSeparadorGroupBox
            '
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.CodificacionArchivoComboBox)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.Label2)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.txtRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.Label1)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.ExcelRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.VacioRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.PuntoComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.TabuladorRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.ComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Location = New System.Drawing.Point(12, 13)
            Me.OpcionesSeparadorGroupBox.Name = "OpcionesSeparadorGroupBox"
            Me.OpcionesSeparadorGroupBox.Size = New System.Drawing.Size(277, 192)
            Me.OpcionesSeparadorGroupBox.TabIndex = 15
            Me.OpcionesSeparadorGroupBox.TabStop = False
            Me.OpcionesSeparadorGroupBox.Text = "Opciones de Separador (csv)"
            '
            'CodificacionArchivoComboBox
            '
            Me.CodificacionArchivoComboBox.FormattingEnabled = True
            Me.CodificacionArchivoComboBox.Items.AddRange(New Object() {"ANSI", "UTF8"})
            Me.CodificacionArchivoComboBox.Location = New System.Drawing.Point(6, 150)
            Me.CodificacionArchivoComboBox.Name = "CodificacionArchivoComboBox"
            Me.CodificacionArchivoComboBox.Size = New System.Drawing.Size(121, 21)
            Me.CodificacionArchivoComboBox.TabIndex = 9
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(6, 134)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(104, 13)
            Me.Label2.TabIndex = 8
            Me.Label2.Text = "Codificación Archivo"
            '
            'txtRadioButton
            '
            Me.txtRadioButton.AutoSize = True
            Me.txtRadioButton.Location = New System.Drawing.Point(148, 99)
            Me.txtRadioButton.Name = "txtRadioButton"
            Me.txtRadioButton.Size = New System.Drawing.Size(75, 17)
            Me.txtRadioButton.TabIndex = 6
            Me.txtRadioButton.TabStop = True
            Me.txtRadioButton.Text = "Texto (.txt)"
            Me.txtRadioButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(6, 80)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(68, 13)
            Me.Label1.TabIndex = 5
            Me.Label1.Text = "Otro Formato"
            '
            'ExcelRadioButton
            '
            Me.ExcelRadioButton.AutoSize = True
            Me.ExcelRadioButton.Location = New System.Drawing.Point(6, 99)
            Me.ExcelRadioButton.Name = "ExcelRadioButton"
            Me.ExcelRadioButton.Size = New System.Drawing.Size(80, 17)
            Me.ExcelRadioButton.TabIndex = 4
            Me.ExcelRadioButton.TabStop = True
            Me.ExcelRadioButton.Text = "Excel (.xlsx)"
            Me.ExcelRadioButton.UseVisualStyleBackColor = True
            '
            'VacioRadioButton
            '
            Me.VacioRadioButton.AutoSize = True
            Me.VacioRadioButton.Location = New System.Drawing.Point(148, 44)
            Me.VacioRadioButton.Name = "VacioRadioButton"
            Me.VacioRadioButton.Size = New System.Drawing.Size(63, 17)
            Me.VacioRadioButton.TabIndex = 3
            Me.VacioRadioButton.Text = "Vacío ()"
            Me.VacioRadioButton.UseVisualStyleBackColor = True
            '
            'PuntoComaRadioButton
            '
            Me.PuntoComaRadioButton.AutoSize = True
            Me.PuntoComaRadioButton.Location = New System.Drawing.Point(148, 21)
            Me.PuntoComaRadioButton.Name = "PuntoComaRadioButton"
            Me.PuntoComaRadioButton.Size = New System.Drawing.Size(103, 17)
            Me.PuntoComaRadioButton.TabIndex = 2
            Me.PuntoComaRadioButton.Text = "Punto y Coma (;)"
            Me.PuntoComaRadioButton.UseVisualStyleBackColor = True
            '
            'TabuladorRadioButton
            '
            Me.TabuladorRadioButton.AutoSize = True
            Me.TabuladorRadioButton.Location = New System.Drawing.Point(7, 44)
            Me.TabuladorRadioButton.Name = "TabuladorRadioButton"
            Me.TabuladorRadioButton.Size = New System.Drawing.Size(97, 17)
            Me.TabuladorRadioButton.TabIndex = 1
            Me.TabuladorRadioButton.Text = "Tabulador (     )"
            Me.TabuladorRadioButton.UseVisualStyleBackColor = True
            '
            'ComaRadioButton
            '
            Me.ComaRadioButton.AutoSize = True
            Me.ComaRadioButton.Checked = True
            Me.ComaRadioButton.Location = New System.Drawing.Point(7, 21)
            Me.ComaRadioButton.Name = "ComaRadioButton"
            Me.ComaRadioButton.Size = New System.Drawing.Size(64, 17)
            Me.ComaRadioButton.TabIndex = 0
            Me.ComaRadioButton.TabStop = True
            Me.ComaRadioButton.Text = "Coma (,)"
            Me.ComaRadioButton.UseVisualStyleBackColor = True
            '
            'ok
            '
            Me.ok.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ok.Image = Global.Miharu.Desktop.Controls.My.Resources.Resources.Aceptar
            Me.ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ok.Location = New System.Drawing.Point(198, 211)
            Me.ok.Name = "ok"
            Me.ok.Size = New System.Drawing.Size(91, 27)
            Me.ok.TabIndex = 17
            Me.ok.Text = "Aceptar"
            Me.ok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ok.UseVisualStyleBackColor = True
            '
            'FormParametrosExportacion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(302, 258)
            Me.Controls.Add(Me.ok)
            Me.Controls.Add(Me.chkEncabezado)
            Me.Controls.Add(Me.OpcionesSeparadorGroupBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Name = "FormParametrosExportacion"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Parametros Exportación"
            Me.OpcionesSeparadorGroupBox.ResumeLayout(False)
            Me.OpcionesSeparadorGroupBox.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents chkEncabezado As System.Windows.Forms.CheckBox
        Friend WithEvents OpcionesSeparadorGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents VacioRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents PuntoComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents TabuladorRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents ComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents ok As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ExcelRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents txtRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents CodificacionArchivoComboBox As System.Windows.Forms.ComboBox
    End Class
End Namespace