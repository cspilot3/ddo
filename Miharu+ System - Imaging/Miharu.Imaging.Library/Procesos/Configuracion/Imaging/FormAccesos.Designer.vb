Namespace Procesos.Configuracion.Imaging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormAccesos
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAccesos))
            Me.MainGroupBox = New System.Windows.Forms.GroupBox()
            Me.EntidadLabel = New System.Windows.Forms.Label()
            Me.PermisosGroupBox = New System.Windows.Forms.GroupBox()
            Me.ValidacionListasCheckBox = New System.Windows.Forms.CheckBox()
            Me.CalidadRecorteCheckBox = New System.Windows.Forms.CheckBox()
            Me.RecorteCheckBox = New System.Windows.Forms.CheckBox()
            Me.CalidadCapturaCheckBox = New System.Windows.Forms.CheckBox()
            Me.TerceraCapturaCheckBox = New System.Windows.Forms.CheckBox()
            Me.SegundaCapturaCheckBox = New System.Windows.Forms.CheckBox()
            Me.PrimeraCapturaCheckBox = New System.Windows.Forms.CheckBox()
            Me.PreCapturaCheckBox = New System.Windows.Forms.CheckBox()
            Me.ValidacionesOpcionalesCheckBox = New System.Windows.Forms.CheckBox()
            Me.ReprocesosCheckBox = New System.Windows.Forms.CheckBox()
            Me.IndexacionCheckBox = New System.Windows.Forms.CheckBox()
            Me.CargueCheckBox = New System.Windows.Forms.CheckBox()
            Me.CentroProcesamientoComboBox = New System.Windows.Forms.ComboBox()
            Me.CentroProcesamientoLabel = New System.Windows.Forms.Label()
            Me.SedeComboBox = New System.Windows.Forms.ComboBox()
            Me.SedeLabel = New System.Windows.Forms.Label()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.CorreccionMaquinaCheckBox = New System.Windows.Forms.CheckBox()
            Me.MainGroupBox.SuspendLayout()
            Me.PermisosGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'MainGroupBox
            '
            Me.MainGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MainGroupBox.Controls.Add(Me.EntidadLabel)
            Me.MainGroupBox.Controls.Add(Me.PermisosGroupBox)
            Me.MainGroupBox.Controls.Add(Me.CentroProcesamientoComboBox)
            Me.MainGroupBox.Controls.Add(Me.CentroProcesamientoLabel)
            Me.MainGroupBox.Controls.Add(Me.SedeComboBox)
            Me.MainGroupBox.Controls.Add(Me.SedeLabel)
            Me.MainGroupBox.Location = New System.Drawing.Point(12, 12)
            Me.MainGroupBox.Name = "MainGroupBox"
            Me.MainGroupBox.Size = New System.Drawing.Size(403, 372)
            Me.MainGroupBox.TabIndex = 0
            Me.MainGroupBox.TabStop = False
            '
            'EntidadLabel
            '
            Me.EntidadLabel.AutoSize = True
            Me.EntidadLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EntidadLabel.Location = New System.Drawing.Point(6, 16)
            Me.EntidadLabel.Name = "EntidadLabel"
            Me.EntidadLabel.Size = New System.Drawing.Size(71, 20)
            Me.EntidadLabel.TabIndex = 5
            Me.EntidadLabel.Text = "Entidad"
            '
            'PermisosGroupBox
            '
            Me.PermisosGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PermisosGroupBox.Controls.Add(Me.CorreccionMaquinaCheckBox)
            Me.PermisosGroupBox.Controls.Add(Me.ValidacionListasCheckBox)
            Me.PermisosGroupBox.Controls.Add(Me.CalidadRecorteCheckBox)
            Me.PermisosGroupBox.Controls.Add(Me.RecorteCheckBox)
            Me.PermisosGroupBox.Controls.Add(Me.CalidadCapturaCheckBox)
            Me.PermisosGroupBox.Controls.Add(Me.TerceraCapturaCheckBox)
            Me.PermisosGroupBox.Controls.Add(Me.SegundaCapturaCheckBox)
            Me.PermisosGroupBox.Controls.Add(Me.PrimeraCapturaCheckBox)
            Me.PermisosGroupBox.Controls.Add(Me.PreCapturaCheckBox)
            Me.PermisosGroupBox.Controls.Add(Me.ValidacionesOpcionalesCheckBox)
            Me.PermisosGroupBox.Controls.Add(Me.ReprocesosCheckBox)
            Me.PermisosGroupBox.Controls.Add(Me.IndexacionCheckBox)
            Me.PermisosGroupBox.Controls.Add(Me.CargueCheckBox)
            Me.PermisosGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PermisosGroupBox.Location = New System.Drawing.Point(9, 157)
            Me.PermisosGroupBox.Name = "PermisosGroupBox"
            Me.PermisosGroupBox.Size = New System.Drawing.Size(386, 206)
            Me.PermisosGroupBox.TabIndex = 4
            Me.PermisosGroupBox.TabStop = False
            Me.PermisosGroupBox.Text = "Permisos"
            '
            'ValidacionListasCheckBox
            '
            Me.ValidacionListasCheckBox.AutoSize = True
            Me.ValidacionListasCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValidacionListasCheckBox.Location = New System.Drawing.Point(27, 145)
            Me.ValidacionListasCheckBox.Name = "ValidacionListasCheckBox"
            Me.ValidacionListasCheckBox.Size = New System.Drawing.Size(109, 17)
            Me.ValidacionListasCheckBox.TabIndex = 11
            Me.ValidacionListasCheckBox.Text = "Adicional Captura"
            Me.ValidacionListasCheckBox.UseVisualStyleBackColor = True
            '
            'CalidadRecorteCheckBox
            '
            Me.CalidadRecorteCheckBox.AutoSize = True
            Me.CalidadRecorteCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CalidadRecorteCheckBox.Location = New System.Drawing.Point(235, 145)
            Me.CalidadRecorteCheckBox.Name = "CalidadRecorteCheckBox"
            Me.CalidadRecorteCheckBox.Size = New System.Drawing.Size(102, 17)
            Me.CalidadRecorteCheckBox.TabIndex = 10
            Me.CalidadRecorteCheckBox.Text = "Calidad Recorte"
            Me.CalidadRecorteCheckBox.UseVisualStyleBackColor = True
            '
            'RecorteCheckBox
            '
            Me.RecorteCheckBox.AutoSize = True
            Me.RecorteCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.RecorteCheckBox.Location = New System.Drawing.Point(235, 122)
            Me.RecorteCheckBox.Name = "RecorteCheckBox"
            Me.RecorteCheckBox.Size = New System.Drawing.Size(64, 17)
            Me.RecorteCheckBox.TabIndex = 9
            Me.RecorteCheckBox.Text = "Recorte"
            Me.RecorteCheckBox.UseVisualStyleBackColor = True
            '
            'CalidadCapturaCheckBox
            '
            Me.CalidadCapturaCheckBox.AutoSize = True
            Me.CalidadCapturaCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CalidadCapturaCheckBox.Location = New System.Drawing.Point(235, 99)
            Me.CalidadCapturaCheckBox.Name = "CalidadCapturaCheckBox"
            Me.CalidadCapturaCheckBox.Size = New System.Drawing.Size(101, 17)
            Me.CalidadCapturaCheckBox.TabIndex = 8
            Me.CalidadCapturaCheckBox.Text = "Calidad Captura"
            Me.CalidadCapturaCheckBox.UseVisualStyleBackColor = True
            '
            'TerceraCapturaCheckBox
            '
            Me.TerceraCapturaCheckBox.AutoSize = True
            Me.TerceraCapturaCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TerceraCapturaCheckBox.Location = New System.Drawing.Point(235, 76)
            Me.TerceraCapturaCheckBox.Name = "TerceraCapturaCheckBox"
            Me.TerceraCapturaCheckBox.Size = New System.Drawing.Size(102, 17)
            Me.TerceraCapturaCheckBox.TabIndex = 7
            Me.TerceraCapturaCheckBox.Text = "Tercera captura"
            Me.TerceraCapturaCheckBox.UseVisualStyleBackColor = True
            '
            'SegundaCapturaCheckBox
            '
            Me.SegundaCapturaCheckBox.AutoSize = True
            Me.SegundaCapturaCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.SegundaCapturaCheckBox.Location = New System.Drawing.Point(235, 53)
            Me.SegundaCapturaCheckBox.Name = "SegundaCapturaCheckBox"
            Me.SegundaCapturaCheckBox.Size = New System.Drawing.Size(108, 17)
            Me.SegundaCapturaCheckBox.TabIndex = 6
            Me.SegundaCapturaCheckBox.Text = "Segunda captura"
            Me.SegundaCapturaCheckBox.UseVisualStyleBackColor = True
            '
            'PrimeraCapturaCheckBox
            '
            Me.PrimeraCapturaCheckBox.AutoSize = True
            Me.PrimeraCapturaCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PrimeraCapturaCheckBox.Location = New System.Drawing.Point(235, 30)
            Me.PrimeraCapturaCheckBox.Name = "PrimeraCapturaCheckBox"
            Me.PrimeraCapturaCheckBox.Size = New System.Drawing.Size(100, 17)
            Me.PrimeraCapturaCheckBox.TabIndex = 5
            Me.PrimeraCapturaCheckBox.Text = "Primera captura"
            Me.PrimeraCapturaCheckBox.UseVisualStyleBackColor = True
            '
            'PreCapturaCheckBox
            '
            Me.PreCapturaCheckBox.AutoSize = True
            Me.PreCapturaCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PreCapturaCheckBox.Location = New System.Drawing.Point(27, 122)
            Me.PreCapturaCheckBox.Name = "PreCapturaCheckBox"
            Me.PreCapturaCheckBox.Size = New System.Drawing.Size(81, 17)
            Me.PreCapturaCheckBox.TabIndex = 4
            Me.PreCapturaCheckBox.Text = "Pre captura"
            Me.PreCapturaCheckBox.UseVisualStyleBackColor = True
            '
            'ValidacionesOpcionalesCheckBox
            '
            Me.ValidacionesOpcionalesCheckBox.AutoSize = True
            Me.ValidacionesOpcionalesCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValidacionesOpcionalesCheckBox.Location = New System.Drawing.Point(27, 99)
            Me.ValidacionesOpcionalesCheckBox.Name = "ValidacionesOpcionalesCheckBox"
            Me.ValidacionesOpcionalesCheckBox.Size = New System.Drawing.Size(140, 17)
            Me.ValidacionesOpcionalesCheckBox.TabIndex = 3
            Me.ValidacionesOpcionalesCheckBox.Text = "Validaciones opcionales"
            Me.ValidacionesOpcionalesCheckBox.UseVisualStyleBackColor = True
            '
            'ReprocesosCheckBox
            '
            Me.ReprocesosCheckBox.AutoSize = True
            Me.ReprocesosCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ReprocesosCheckBox.Location = New System.Drawing.Point(27, 76)
            Me.ReprocesosCheckBox.Name = "ReprocesosCheckBox"
            Me.ReprocesosCheckBox.Size = New System.Drawing.Size(83, 17)
            Me.ReprocesosCheckBox.TabIndex = 2
            Me.ReprocesosCheckBox.Text = "Reprocesos"
            Me.ReprocesosCheckBox.UseVisualStyleBackColor = True
            '
            'IndexacionCheckBox
            '
            Me.IndexacionCheckBox.AutoSize = True
            Me.IndexacionCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.IndexacionCheckBox.Location = New System.Drawing.Point(27, 53)
            Me.IndexacionCheckBox.Name = "IndexacionCheckBox"
            Me.IndexacionCheckBox.Size = New System.Drawing.Size(78, 17)
            Me.IndexacionCheckBox.TabIndex = 1
            Me.IndexacionCheckBox.Text = "Indexación"
            Me.IndexacionCheckBox.UseVisualStyleBackColor = True
            '
            'CargueCheckBox
            '
            Me.CargueCheckBox.AutoSize = True
            Me.CargueCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CargueCheckBox.Location = New System.Drawing.Point(27, 30)
            Me.CargueCheckBox.Name = "CargueCheckBox"
            Me.CargueCheckBox.Size = New System.Drawing.Size(60, 17)
            Me.CargueCheckBox.TabIndex = 0
            Me.CargueCheckBox.Text = "Cargue"
            Me.CargueCheckBox.UseVisualStyleBackColor = True
            '
            'CentroProcesamientoComboBox
            '
            Me.CentroProcesamientoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CentroProcesamientoComboBox.FormattingEnabled = True
            Me.CentroProcesamientoComboBox.Location = New System.Drawing.Point(9, 117)
            Me.CentroProcesamientoComboBox.Name = "CentroProcesamientoComboBox"
            Me.CentroProcesamientoComboBox.Size = New System.Drawing.Size(381, 21)
            Me.CentroProcesamientoComboBox.TabIndex = 3
            '
            'CentroProcesamientoLabel
            '
            Me.CentroProcesamientoLabel.AutoSize = True
            Me.CentroProcesamientoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CentroProcesamientoLabel.Location = New System.Drawing.Point(6, 101)
            Me.CentroProcesamientoLabel.Name = "CentroProcesamientoLabel"
            Me.CentroProcesamientoLabel.Size = New System.Drawing.Size(148, 13)
            Me.CentroProcesamientoLabel.TabIndex = 2
            Me.CentroProcesamientoLabel.Text = "Centro de procesamiento"
            '
            'SedeComboBox
            '
            Me.SedeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SedeComboBox.FormattingEnabled = True
            Me.SedeComboBox.Location = New System.Drawing.Point(9, 68)
            Me.SedeComboBox.Name = "SedeComboBox"
            Me.SedeComboBox.Size = New System.Drawing.Size(381, 21)
            Me.SedeComboBox.TabIndex = 1
            '
            'SedeLabel
            '
            Me.SedeLabel.AutoSize = True
            Me.SedeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.SedeLabel.Location = New System.Drawing.Point(6, 52)
            Me.SedeLabel.Name = "SedeLabel"
            Me.SedeLabel.Size = New System.Drawing.Size(36, 13)
            Me.SedeLabel.TabIndex = 0
            Me.SedeLabel.Text = "Sede"
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(205, 390)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(90, 30)
            Me.GuardarButton.TabIndex = 11
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(301, 390)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 12
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'CorreccionMaquinaCheckBox
            '
            Me.CorreccionMaquinaCheckBox.AutoSize = True
            Me.CorreccionMaquinaCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CorreccionMaquinaCheckBox.Location = New System.Drawing.Point(27, 168)
            Me.CorreccionMaquinaCheckBox.Name = "CorreccionMaquinaCheckBox"
            Me.CorreccionMaquinaCheckBox.Size = New System.Drawing.Size(161, 17)
            Me.CorreccionMaquinaCheckBox.TabIndex = 12
            Me.CorreccionMaquinaCheckBox.Text = "Corrección Maquina Captura"
            Me.CorreccionMaquinaCheckBox.UseVisualStyleBackColor = True
            '
            'FormAccesos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(427, 429)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.MainGroupBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormAccesos"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Accesos"
            Me.MainGroupBox.ResumeLayout(False)
            Me.MainGroupBox.PerformLayout()
            Me.PermisosGroupBox.ResumeLayout(False)
            Me.PermisosGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents SedeComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents SedeLabel As System.Windows.Forms.Label
        Friend WithEvents CentroProcesamientoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents CentroProcesamientoLabel As System.Windows.Forms.Label
        Friend WithEvents PermisosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CargueCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents TerceraCapturaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents SegundaCapturaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents PrimeraCapturaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents PreCapturaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents ValidacionesOpcionalesCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents ReprocesosCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents IndexacionCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents EntidadLabel As System.Windows.Forms.Label
        Friend WithEvents CalidadCapturaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents RecorteCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents CalidadRecorteCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents ValidacionListasCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents CorreccionMaquinaCheckBox As System.Windows.Forms.CheckBox
    End Class
End Namespace