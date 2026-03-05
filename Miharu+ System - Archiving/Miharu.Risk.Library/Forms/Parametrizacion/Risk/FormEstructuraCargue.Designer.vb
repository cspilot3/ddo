Imports Miharu.Desktop.Library

Namespace Forms.Parametrizacion.Risk

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEstructuraCargue
        Inherits FormBase

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEstructuraCargue))
            Me.OpcionesSeparadorGroupBox = New System.Windows.Forms.GroupBox()
            Me.PuntoComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.TabuladorRadioButton = New System.Windows.Forms.RadioButton()
            Me.ComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.GenerarArchivoButton = New System.Windows.Forms.Button()
            Me.GuardarArchivoSaveFileDialog = New System.Windows.Forms.SaveFileDialog()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GeneraArchivoProgressBar = New System.Windows.Forms.ProgressBar()
            Me.GenerarArchivoBackgroundWorker = New System.ComponentModel.BackgroundWorker()
            Me.AbrirArchivoCheckBox = New System.Windows.Forms.CheckBox()
            Me.OpcionesSeparadorGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'OpcionesSeparadorGroupBox
            '
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.PuntoComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.TabuladorRadioButton)
            Me.OpcionesSeparadorGroupBox.Controls.Add(Me.ComaRadioButton)
            Me.OpcionesSeparadorGroupBox.Location = New System.Drawing.Point(14, 12)
            Me.OpcionesSeparadorGroupBox.Name = "OpcionesSeparadorGroupBox"
            Me.OpcionesSeparadorGroupBox.Size = New System.Drawing.Size(192, 91)
            Me.OpcionesSeparadorGroupBox.TabIndex = 12
            Me.OpcionesSeparadorGroupBox.TabStop = False
            Me.OpcionesSeparadorGroupBox.Text = "Opciones de Separador"
            '
            'PuntoComaRadioButton
            '
            Me.PuntoComaRadioButton.AutoSize = True
            Me.PuntoComaRadioButton.Location = New System.Drawing.Point(7, 67)
            Me.PuntoComaRadioButton.Name = "PuntoComaRadioButton"
            Me.PuntoComaRadioButton.Size = New System.Drawing.Size(103, 17)
            Me.PuntoComaRadioButton.TabIndex = 2
            Me.PuntoComaRadioButton.Text = "Punto y Coma (;)"
            Me.PuntoComaRadioButton.UseVisualStyleBackColor = True
            '
            'TabuladorRadioButton
            '
            Me.TabuladorRadioButton.AutoSize = True
            Me.TabuladorRadioButton.Location = New System.Drawing.Point(8, 44)
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
            Me.ComaRadioButton.Location = New System.Drawing.Point(8, 21)
            Me.ComaRadioButton.Name = "ComaRadioButton"
            Me.ComaRadioButton.Size = New System.Drawing.Size(64, 17)
            Me.ComaRadioButton.TabIndex = 0
            Me.ComaRadioButton.TabStop = True
            Me.ComaRadioButton.Text = "Coma (,)"
            Me.ComaRadioButton.UseVisualStyleBackColor = True
            '
            'GenerarArchivoButton
            '
            Me.GenerarArchivoButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnGenerarEstructura
            Me.GenerarArchivoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GenerarArchivoButton.Location = New System.Drawing.Point(216, 72)
            Me.GenerarArchivoButton.Name = "GenerarArchivoButton"
            Me.GenerarArchivoButton.Size = New System.Drawing.Size(83, 31)
            Me.GenerarArchivoButton.TabIndex = 13
            Me.GenerarArchivoButton.Text = "&Generar"
            Me.GenerarArchivoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GenerarArchivoButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(305, 72)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(85, 31)
            Me.CerrarButton.TabIndex = 14
            Me.CerrarButton.Text = "&Cerrar       "
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'GeneraArchivoProgressBar
            '
            Me.GeneraArchivoProgressBar.ForeColor = System.Drawing.SystemColors.Desktop
            Me.GeneraArchivoProgressBar.Location = New System.Drawing.Point(14, 110)
            Me.GeneraArchivoProgressBar.Name = "GeneraArchivoProgressBar"
            Me.GeneraArchivoProgressBar.Size = New System.Drawing.Size(376, 23)
            Me.GeneraArchivoProgressBar.Step = 1
            Me.GeneraArchivoProgressBar.TabIndex = 15
            '
            'GenerarArchivoBackgroundWorker
            '
            Me.GenerarArchivoBackgroundWorker.WorkerReportsProgress = True
            Me.GenerarArchivoBackgroundWorker.WorkerSupportsCancellation = True
            '
            'AbrirArchivoCheckBox
            '
            Me.AbrirArchivoCheckBox.AutoSize = True
            Me.AbrirArchivoCheckBox.Checked = True
            Me.AbrirArchivoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
            Me.AbrirArchivoCheckBox.Location = New System.Drawing.Point(216, 12)
            Me.AbrirArchivoCheckBox.Name = "AbrirArchivoCheckBox"
            Me.AbrirArchivoCheckBox.Size = New System.Drawing.Size(99, 17)
            Me.AbrirArchivoCheckBox.TabIndex = 16
            Me.AbrirArchivoCheckBox.Text = "Abrir archivo"
            Me.AbrirArchivoCheckBox.UseVisualStyleBackColor = True
            '
            'FormEstructuraCargue
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(402, 144)
            Me.Controls.Add(Me.AbrirArchivoCheckBox)
            Me.Controls.Add(Me.GeneraArchivoProgressBar)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.GenerarArchivoButton)
            Me.Controls.Add(Me.OpcionesSeparadorGroupBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormEstructuraCargue"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Generación estructura de archivo de cargue"
            Me.OpcionesSeparadorGroupBox.ResumeLayout(False)
            Me.OpcionesSeparadorGroupBox.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents OpcionesSeparadorGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents PuntoComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents TabuladorRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents ComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents GenerarArchivoButton As System.Windows.Forms.Button
        Friend WithEvents GuardarArchivoSaveFileDialog As System.Windows.Forms.SaveFileDialog
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents GeneraArchivoProgressBar As System.Windows.Forms.ProgressBar
        Friend WithEvents GenerarArchivoBackgroundWorker As System.ComponentModel.BackgroundWorker
        Friend WithEvents AbrirArchivoCheckBox As System.Windows.Forms.CheckBox
    End Class
End Namespace