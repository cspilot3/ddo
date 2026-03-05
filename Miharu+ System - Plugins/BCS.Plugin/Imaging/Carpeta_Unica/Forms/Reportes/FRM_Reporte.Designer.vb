Namespace Imaging.Carpeta_Unica.Forms.Reportes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FRM_Reporte
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
            Me.components = New System.ComponentModel.Container()
            Dim Rango3 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango4 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.dtpFechaProceso = New System.Windows.Forms.DateTimePicker()
            Me.lblFechaProceso = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.BackgroundWorkerReport = New System.ComponentModel.BackgroundWorker()
            Me.lblTitleProcess = New System.Windows.Forms.Label()
            Me.TimerReport = New System.Windows.Forms.Timer(Me.components)
            Me.lblTimeEstimate = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.ArchivoRotulosSelectButton = New System.Windows.Forms.Button()
            Me.ArchivoRotulosTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.GenerarButton = New System.Windows.Forms.Button()
            Me.RutaTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.ArchivoRotulosCheckBox = New System.Windows.Forms.CheckBox()
            Me.lblProgresoGeneral = New System.Windows.Forms.Label()
            Me.ProgressBarExportacionGeneral = New System.Windows.Forms.ProgressBar()
            Me.lblProcesoActual = New System.Windows.Forms.Label()
            Me.lblTiempoEstimado = New System.Windows.Forms.Label()
            Me.lblImgProcesadas = New System.Windows.Forms.Label()
            Me.lblCantidadImgs = New System.Windows.Forms.Label()
            Me.ProgressBarExportacíon = New System.Windows.Forms.ProgressBar()
            Me.ProcesoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.SelectFolderButton = New System.Windows.Forms.Button()
            Me.ReporteDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'dtpFechaProceso
            '
            Me.dtpFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpFechaProceso.Location = New System.Drawing.Point(145, 31)
            Me.dtpFechaProceso.Name = "dtpFechaProceso"
            Me.dtpFechaProceso.Size = New System.Drawing.Size(268, 20)
            Me.dtpFechaProceso.TabIndex = 50
            '
            'lblFechaProceso
            '
            Me.lblFechaProceso.AutoSize = True
            Me.lblFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaProceso.Location = New System.Drawing.Point(42, 35)
            Me.lblFechaProceso.Name = "lblFechaProceso"
            Me.lblFechaProceso.Size = New System.Drawing.Size(96, 13)
            Me.lblFechaProceso.TabIndex = 49
            Me.lblFechaProceso.Text = "Fecha Proceso:"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(42, 71)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(56, 13)
            Me.Label1.TabIndex = 53
            Me.Label1.Text = "Reporte:"
            '
            'BackgroundWorkerReport
            '
            Me.BackgroundWorkerReport.WorkerReportsProgress = True
            Me.BackgroundWorkerReport.WorkerSupportsCancellation = True
            '
            'lblTitleProcess
            '
            Me.lblTitleProcess.AutoSize = True
            Me.lblTitleProcess.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTitleProcess.Location = New System.Drawing.Point(39, 292)
            Me.lblTitleProcess.Name = "lblTitleProcess"
            Me.lblTitleProcess.Size = New System.Drawing.Size(82, 13)
            Me.lblTitleProcess.TabIndex = 59
            Me.lblTitleProcess.Text = "..TitleProcess"
            Me.lblTitleProcess.Visible = False
            '
            'TimerReport
            '
            Me.TimerReport.Interval = 5000
            '
            'lblTimeEstimate
            '
            Me.lblTimeEstimate.AutoSize = True
            Me.lblTimeEstimate.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTimeEstimate.Location = New System.Drawing.Point(43, 233)
            Me.lblTimeEstimate.Name = "lblTimeEstimate"
            Me.lblTimeEstimate.Size = New System.Drawing.Size(88, 13)
            Me.lblTimeEstimate.TabIndex = 61
            Me.lblTimeEstimate.Text = "..TimeEstimate"
            Me.lblTimeEstimate.Visible = False
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.ArchivoRotulosSelectButton)
            Me.GroupBox1.Controls.Add(Me.ArchivoRotulosTextBox)
            Me.GroupBox1.Controls.Add(Me.GenerarButton)
            Me.GroupBox1.Controls.Add(Me.RutaTextBox)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.ArchivoRotulosCheckBox)
            Me.GroupBox1.Controls.Add(Me.lblProgresoGeneral)
            Me.GroupBox1.Controls.Add(Me.ProgressBarExportacionGeneral)
            Me.GroupBox1.Controls.Add(Me.lblProcesoActual)
            Me.GroupBox1.Controls.Add(Me.lblTiempoEstimado)
            Me.GroupBox1.Controls.Add(Me.lblImgProcesadas)
            Me.GroupBox1.Controls.Add(Me.lblCantidadImgs)
            Me.GroupBox1.Controls.Add(Me.ProgressBarExportacíon)
            Me.GroupBox1.Controls.Add(Me.ProcesoDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.SelectFolderButton)
            Me.GroupBox1.Location = New System.Drawing.Point(13, 8)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(454, 499)
            Me.GroupBox1.TabIndex = 62
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Datos de Reporte"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(29, 182)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(112, 13)
            Me.Label4.TabIndex = 77
            Me.Label4.Text = "Carpeta de Salida:"
            '
            'ArchivoRotulosSelectButton
            '
            Me.ArchivoRotulosSelectButton.Image = Global.BCS.Plugin.My.Resources.Resources.File
            Me.ArchivoRotulosSelectButton.Location = New System.Drawing.Point(406, 140)
            Me.ArchivoRotulosSelectButton.Name = "ArchivoRotulosSelectButton"
            Me.ArchivoRotulosSelectButton.Size = New System.Drawing.Size(27, 23)
            Me.ArchivoRotulosSelectButton.TabIndex = 76
            Me.ArchivoRotulosSelectButton.UseVisualStyleBackColor = True
            '
            'ArchivoRotulosTextBox
            '
            Me.ArchivoRotulosTextBox._Obligatorio = False
            Me.ArchivoRotulosTextBox._PermitePegar = False
            Me.ArchivoRotulosTextBox.Cantidad_Decimales = CType(0, Short)
            Me.ArchivoRotulosTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ArchivoRotulosTextBox.DateFormat = Nothing
            Me.ArchivoRotulosTextBox.DisabledEnter = False
            Me.ArchivoRotulosTextBox.DisabledTab = False
            Me.ArchivoRotulosTextBox.EnabledShortCuts = False
            Me.ArchivoRotulosTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ArchivoRotulosTextBox.FocusOut = System.Drawing.Color.White
            Me.ArchivoRotulosTextBox.Location = New System.Drawing.Point(32, 143)
            Me.ArchivoRotulosTextBox.MaskedTextBox_Property = ""
            Me.ArchivoRotulosTextBox.MaximumLength = CType(0, Short)
            Me.ArchivoRotulosTextBox.MinimumLength = CType(0, Short)
            Me.ArchivoRotulosTextBox.Name = "ArchivoRotulosTextBox"
            Me.ArchivoRotulosTextBox.Obligatorio = False
            Me.ArchivoRotulosTextBox.permitePegar = False
            Rango3.MaxValue = 9.2233720368547758E+18R
            Rango3.MinValue = 0.0R
            Me.ArchivoRotulosTextBox.Rango = Rango3
            Me.ArchivoRotulosTextBox.Size = New System.Drawing.Size(368, 20)
            Me.ArchivoRotulosTextBox.TabIndex = 75
            Me.ArchivoRotulosTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ArchivoRotulosTextBox.Usa_Decimales = False
            Me.ArchivoRotulosTextBox.Validos_Cantidad_Puntos = False
            '
            'GenerarButton
            '
            Me.GenerarButton.BackColor = System.Drawing.SystemColors.Control
            Me.GenerarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.GenerarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GenerarButton.Image = Global.BCS.Plugin.My.Resources.Resources.Process_Accept
            Me.GenerarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.GenerarButton.Location = New System.Drawing.Point(179, 225)
            Me.GenerarButton.Name = "GenerarButton"
            Me.GenerarButton.Size = New System.Drawing.Size(100, 60)
            Me.GenerarButton.TabIndex = 55
            Me.GenerarButton.Tag = "Ctrl + P"
            Me.GenerarButton.Text = "&Generar"
            Me.GenerarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.GenerarButton.UseVisualStyleBackColor = False
            '
            'RutaTextBox
            '
            Me.RutaTextBox._Obligatorio = False
            Me.RutaTextBox._PermitePegar = False
            Me.RutaTextBox.Cantidad_Decimales = CType(0, Short)
            Me.RutaTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.RutaTextBox.DateFormat = Nothing
            Me.RutaTextBox.DisabledEnter = False
            Me.RutaTextBox.DisabledTab = False
            Me.RutaTextBox.EnabledShortCuts = False
            Me.RutaTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.RutaTextBox.FocusOut = System.Drawing.Color.White
            Me.RutaTextBox.Location = New System.Drawing.Point(34, 198)
            Me.RutaTextBox.MaskedTextBox_Property = ""
            Me.RutaTextBox.MaximumLength = CType(0, Short)
            Me.RutaTextBox.MinimumLength = CType(0, Short)
            Me.RutaTextBox.Name = "RutaTextBox"
            Me.RutaTextBox.Obligatorio = False
            Me.RutaTextBox.permitePegar = False
            Rango4.MaxValue = 9.2233720368547758E+18R
            Rango4.MinValue = 0.0R
            Me.RutaTextBox.Rango = Rango4
            Me.RutaTextBox.Size = New System.Drawing.Size(366, 20)
            Me.RutaTextBox.TabIndex = 56
            Me.RutaTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.RutaTextBox.Usa_Decimales = False
            Me.RutaTextBox.Validos_Cantidad_Puntos = False
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(31, 127)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(101, 13)
            Me.Label3.TabIndex = 73
            Me.Label3.Text = "Archivo Rotulos:"
            '
            'ArchivoRotulosCheckBox
            '
            Me.ArchivoRotulosCheckBox.AutoSize = True
            Me.ArchivoRotulosCheckBox.Location = New System.Drawing.Point(138, 127)
            Me.ArchivoRotulosCheckBox.Name = "ArchivoRotulosCheckBox"
            Me.ArchivoRotulosCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.ArchivoRotulosCheckBox.TabIndex = 72
            Me.ArchivoRotulosCheckBox.UseVisualStyleBackColor = True
            '
            'lblProgresoGeneral
            '
            Me.lblProgresoGeneral.AutoSize = True
            Me.lblProgresoGeneral.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProgresoGeneral.Location = New System.Drawing.Point(31, 451)
            Me.lblProgresoGeneral.Name = "lblProgresoGeneral"
            Me.lblProgresoGeneral.Size = New System.Drawing.Size(127, 13)
            Me.lblProgresoGeneral.TabIndex = 71
            Me.lblProgresoGeneral.Text = "Progreso General (%)"
            '
            'ProgressBarExportacionGeneral
            '
            Me.ProgressBarExportacionGeneral.Location = New System.Drawing.Point(30, 467)
            Me.ProgressBarExportacionGeneral.MarqueeAnimationSpeed = 10
            Me.ProgressBarExportacionGeneral.Name = "ProgressBarExportacionGeneral"
            Me.ProgressBarExportacionGeneral.Size = New System.Drawing.Size(389, 16)
            Me.ProgressBarExportacionGeneral.Step = 1
            Me.ProgressBarExportacionGeneral.TabIndex = 70
            Me.ProgressBarExportacionGeneral.Visible = False
            '
            'lblProcesoActual
            '
            Me.lblProcesoActual.AutoSize = True
            Me.lblProcesoActual.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProcesoActual.Location = New System.Drawing.Point(31, 411)
            Me.lblProcesoActual.Name = "lblProcesoActual"
            Me.lblProcesoActual.Size = New System.Drawing.Size(103, 13)
            Me.lblProcesoActual.TabIndex = 69
            Me.lblProcesoActual.Text = "Proceso Actual:  "
            '
            'lblTiempoEstimado
            '
            Me.lblTiempoEstimado.AutoSize = True
            Me.lblTiempoEstimado.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTiempoEstimado.Location = New System.Drawing.Point(26, 355)
            Me.lblTiempoEstimado.Name = "lblTiempoEstimado"
            Me.lblTiempoEstimado.Size = New System.Drawing.Size(109, 13)
            Me.lblTiempoEstimado.TabIndex = 68
            Me.lblTiempoEstimado.Text = "Tiempo Estimado: "
            '
            'lblImgProcesadas
            '
            Me.lblImgProcesadas.AutoSize = True
            Me.lblImgProcesadas.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblImgProcesadas.Location = New System.Drawing.Point(26, 334)
            Me.lblImgProcesadas.Name = "lblImgProcesadas"
            Me.lblImgProcesadas.Size = New System.Drawing.Size(138, 13)
            Me.lblImgProcesadas.TabIndex = 67
            Me.lblImgProcesadas.Text = "Imagenes Procesadas: "
            '
            'lblCantidadImgs
            '
            Me.lblCantidadImgs.AutoSize = True
            Me.lblCantidadImgs.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCantidadImgs.Location = New System.Drawing.Point(26, 312)
            Me.lblCantidadImgs.Name = "lblCantidadImgs"
            Me.lblCantidadImgs.Size = New System.Drawing.Size(123, 13)
            Me.lblCantidadImgs.TabIndex = 66
            Me.lblCantidadImgs.Text = "Cantidad Total IMG: "
            '
            'ProgressBarExportacíon
            '
            Me.ProgressBarExportacíon.Location = New System.Drawing.Point(30, 427)
            Me.ProgressBarExportacíon.MarqueeAnimationSpeed = 10
            Me.ProgressBarExportacíon.Name = "ProgressBarExportacíon"
            Me.ProgressBarExportacíon.Size = New System.Drawing.Size(389, 16)
            Me.ProgressBarExportacíon.Step = 1
            Me.ProgressBarExportacíon.TabIndex = 58
            Me.ProgressBarExportacíon.Visible = False
            '
            'ProcesoDesktopComboBox
            '
            Me.ProcesoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProcesoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProcesoDesktopComboBox.DisabledEnter = False
            Me.ProcesoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProcesoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProcesoDesktopComboBox.FormattingEnabled = True
            Me.ProcesoDesktopComboBox.Location = New System.Drawing.Point(132, 94)
            Me.ProcesoDesktopComboBox.Name = "ProcesoDesktopComboBox"
            Me.ProcesoDesktopComboBox.Size = New System.Drawing.Size(268, 21)
            Me.ProcesoDesktopComboBox.TabIndex = 63
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(29, 98)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(57, 13)
            Me.Label2.TabIndex = 63
            Me.Label2.Text = "Proceso:"
            '
            'SelectFolderButton
            '
            Me.SelectFolderButton.Image = Global.BCS.Plugin.My.Resources.Resources.MainFolder
            Me.SelectFolderButton.Location = New System.Drawing.Point(406, 195)
            Me.SelectFolderButton.Name = "SelectFolderButton"
            Me.SelectFolderButton.Size = New System.Drawing.Size(27, 23)
            Me.SelectFolderButton.TabIndex = 57
            Me.SelectFolderButton.UseVisualStyleBackColor = True
            '
            'ReporteDesktopComboBox
            '
            Me.ReporteDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ReporteDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ReporteDesktopComboBox.DisabledEnter = False
            Me.ReporteDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ReporteDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ReporteDesktopComboBox.FormattingEnabled = True
            Me.ReporteDesktopComboBox.Location = New System.Drawing.Point(145, 63)
            Me.ReporteDesktopComboBox.Name = "ReporteDesktopComboBox"
            Me.ReporteDesktopComboBox.Size = New System.Drawing.Size(268, 21)
            Me.ReporteDesktopComboBox.TabIndex = 54
            '
            'FRM_Reporte
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(479, 519)
            Me.Controls.Add(Me.lblTitleProcess)
            Me.Controls.Add(Me.lblTimeEstimate)
            Me.Controls.Add(Me.ReporteDesktopComboBox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.dtpFechaProceso)
            Me.Controls.Add(Me.lblFechaProceso)
            Me.Controls.Add(Me.GroupBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FRM_Reporte"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Generar Reportes"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents dtpFechaProceso As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblFechaProceso As System.Windows.Forms.Label
        Friend WithEvents ReporteDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents GenerarButton As System.Windows.Forms.Button
        Friend WithEvents SelectFolderButton As System.Windows.Forms.Button
        Friend WithEvents RutaTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents BackgroundWorkerReport As System.ComponentModel.BackgroundWorker
        Friend WithEvents lblTitleProcess As System.Windows.Forms.Label
        Friend WithEvents TimerReport As System.Windows.Forms.Timer
        Friend WithEvents lblTimeEstimate As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents ProcesoDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents lblTiempoEstimado As System.Windows.Forms.Label
        Friend WithEvents lblImgProcesadas As System.Windows.Forms.Label
        Friend WithEvents lblCantidadImgs As System.Windows.Forms.Label
        Friend WithEvents lblProgresoGeneral As System.Windows.Forms.Label
        Friend WithEvents ProgressBarExportacionGeneral As System.Windows.Forms.ProgressBar
        Friend WithEvents lblProcesoActual As System.Windows.Forms.Label
        Friend WithEvents ProgressBarExportacíon As System.Windows.Forms.ProgressBar
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents ArchivoRotulosSelectButton As System.Windows.Forms.Button
        Friend WithEvents ArchivoRotulosTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents ArchivoRotulosCheckBox As System.Windows.Forms.CheckBox
    End Class
End Namespace
