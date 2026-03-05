Namespace Procesos.Cargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormDesbloquear
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDesbloquear))
            Me.IndexacionCheckBox = New System.Windows.Forms.CheckBox()
            Me.IndexacionTextBox = New System.Windows.Forms.TextBox()
            Me.OptionPanel = New System.Windows.Forms.Panel()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.DesbloquearButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.PreCapturaTextBox = New System.Windows.Forms.TextBox()
            Me.PreCapturaCheckBox = New System.Windows.Forms.CheckBox()
            Me.SegundaCapturaTextBox = New System.Windows.Forms.TextBox()
            Me.SegundaCapturaCheckBox = New System.Windows.Forms.CheckBox()
            Me.PrimeraCapturaTextBox = New System.Windows.Forms.TextBox()
            Me.PrimeraCapturaCheckBox = New System.Windows.Forms.CheckBox()
            Me.CalidadCapturaTextBox = New System.Windows.Forms.TextBox()
            Me.CalidadCapturaCheckBox = New System.Windows.Forms.CheckBox()
            Me.TerceraCapturaTextBox = New System.Windows.Forms.TextBox()
            Me.TerceraCapturaCheckBox = New System.Windows.Forms.CheckBox()
            Me.ValidacionesTextBox = New System.Windows.Forms.TextBox()
            Me.ValidacionesCheckBox = New System.Windows.Forms.CheckBox()
            Me.RecorteTextBox = New System.Windows.Forms.TextBox()
            Me.RecorteCheckBox = New System.Windows.Forms.CheckBox()
            Me.MainGroupBox = New System.Windows.Forms.GroupBox()
            Me.ValidacionListasTextBox = New System.Windows.Forms.TextBox()
            Me.ValidacionListasCheckBox = New System.Windows.Forms.CheckBox()
            Me.CalidadRecorteTextBox = New System.Windows.Forms.TextBox()
            Me.CalidadRecorteCheckBox = New System.Windows.Forms.CheckBox()
            Me.FechaProcesoLabel = New System.Windows.Forms.Label()
            Me.OCRCapturaCheckBox = New System.Windows.Forms.CheckBox()
            Me.OCRCapturaTextBox = New System.Windows.Forms.TextBox()
            Me.OptionPanel.SuspendLayout()
            Me.MainGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'IndexacionCheckBox
            '
            Me.IndexacionCheckBox.AutoSize = True
            Me.IndexacionCheckBox.Location = New System.Drawing.Point(20, 68)
            Me.IndexacionCheckBox.Name = "IndexacionCheckBox"
            Me.IndexacionCheckBox.Size = New System.Drawing.Size(78, 17)
            Me.IndexacionCheckBox.TabIndex = 1
            Me.IndexacionCheckBox.Text = "Indexación"
            Me.IndexacionCheckBox.UseVisualStyleBackColor = True
            '
            'IndexacionTextBox
            '
            Me.IndexacionTextBox.Location = New System.Drawing.Point(145, 66)
            Me.IndexacionTextBox.Name = "IndexacionTextBox"
            Me.IndexacionTextBox.ReadOnly = True
            Me.IndexacionTextBox.Size = New System.Drawing.Size(85, 20)
            Me.IndexacionTextBox.TabIndex = 1
            Me.IndexacionTextBox.TabStop = False
            Me.IndexacionTextBox.Text = "0"
            Me.IndexacionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'OptionPanel
            '
            Me.OptionPanel.Controls.Add(Me.BuscarButton)
            Me.OptionPanel.Controls.Add(Me.DesbloquearButton)
            Me.OptionPanel.Controls.Add(Me.CerrarButton)
            Me.OptionPanel.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.OptionPanel.Location = New System.Drawing.Point(5, 239)
            Me.OptionPanel.Name = "OptionPanel"
            Me.OptionPanel.Size = New System.Drawing.Size(494, 42)
            Me.OptionPanel.TabIndex = 1
            '
            'BuscarButton
            '
            Me.BuscarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(12, 6)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(89, 31)
            Me.BuscarButton.TabIndex = 2
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'DesbloquearButton
            '
            Me.DesbloquearButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DesbloquearButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DesbloquearButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.lock_open
            Me.DesbloquearButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.DesbloquearButton.Location = New System.Drawing.Point(277, 6)
            Me.DesbloquearButton.Name = "DesbloquearButton"
            Me.DesbloquearButton.Size = New System.Drawing.Size(107, 33)
            Me.DesbloquearButton.TabIndex = 0
            Me.DesbloquearButton.Text = "Desbloquear"
            Me.DesbloquearButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.DesbloquearButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancelar
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(403, 6)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 33)
            Me.CerrarButton.TabIndex = 1
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'PreCapturaTextBox
            '
            Me.PreCapturaTextBox.Location = New System.Drawing.Point(145, 120)
            Me.PreCapturaTextBox.Name = "PreCapturaTextBox"
            Me.PreCapturaTextBox.ReadOnly = True
            Me.PreCapturaTextBox.Size = New System.Drawing.Size(85, 20)
            Me.PreCapturaTextBox.TabIndex = 4
            Me.PreCapturaTextBox.TabStop = False
            Me.PreCapturaTextBox.Text = "0"
            Me.PreCapturaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'PreCapturaCheckBox
            '
            Me.PreCapturaCheckBox.AutoSize = True
            Me.PreCapturaCheckBox.Location = New System.Drawing.Point(20, 122)
            Me.PreCapturaCheckBox.Name = "PreCapturaCheckBox"
            Me.PreCapturaCheckBox.Size = New System.Drawing.Size(82, 17)
            Me.PreCapturaCheckBox.TabIndex = 2
            Me.PreCapturaCheckBox.Text = "Pre Captura"
            Me.PreCapturaCheckBox.UseVisualStyleBackColor = True
            '
            'SegundaCapturaTextBox
            '
            Me.SegundaCapturaTextBox.Location = New System.Drawing.Point(145, 172)
            Me.SegundaCapturaTextBox.Name = "SegundaCapturaTextBox"
            Me.SegundaCapturaTextBox.ReadOnly = True
            Me.SegundaCapturaTextBox.Size = New System.Drawing.Size(85, 20)
            Me.SegundaCapturaTextBox.TabIndex = 8
            Me.SegundaCapturaTextBox.TabStop = False
            Me.SegundaCapturaTextBox.Text = "0"
            Me.SegundaCapturaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'SegundaCapturaCheckBox
            '
            Me.SegundaCapturaCheckBox.AutoSize = True
            Me.SegundaCapturaCheckBox.Location = New System.Drawing.Point(20, 174)
            Me.SegundaCapturaCheckBox.Name = "SegundaCapturaCheckBox"
            Me.SegundaCapturaCheckBox.Size = New System.Drawing.Size(109, 17)
            Me.SegundaCapturaCheckBox.TabIndex = 4
            Me.SegundaCapturaCheckBox.Text = "Segunda Captura"
            Me.SegundaCapturaCheckBox.UseVisualStyleBackColor = True
            '
            'PrimeraCapturaTextBox
            '
            Me.PrimeraCapturaTextBox.Location = New System.Drawing.Point(145, 146)
            Me.PrimeraCapturaTextBox.Name = "PrimeraCapturaTextBox"
            Me.PrimeraCapturaTextBox.ReadOnly = True
            Me.PrimeraCapturaTextBox.Size = New System.Drawing.Size(85, 20)
            Me.PrimeraCapturaTextBox.TabIndex = 6
            Me.PrimeraCapturaTextBox.TabStop = False
            Me.PrimeraCapturaTextBox.Text = "0"
            Me.PrimeraCapturaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'PrimeraCapturaCheckBox
            '
            Me.PrimeraCapturaCheckBox.AutoSize = True
            Me.PrimeraCapturaCheckBox.Location = New System.Drawing.Point(20, 148)
            Me.PrimeraCapturaCheckBox.Name = "PrimeraCapturaCheckBox"
            Me.PrimeraCapturaCheckBox.Size = New System.Drawing.Size(101, 17)
            Me.PrimeraCapturaCheckBox.TabIndex = 3
            Me.PrimeraCapturaCheckBox.Text = "Primera Captura"
            Me.PrimeraCapturaCheckBox.UseVisualStyleBackColor = True
            '
            'CalidadCapturaTextBox
            '
            Me.CalidadCapturaTextBox.Location = New System.Drawing.Point(400, 66)
            Me.CalidadCapturaTextBox.Name = "CalidadCapturaTextBox"
            Me.CalidadCapturaTextBox.ReadOnly = True
            Me.CalidadCapturaTextBox.Size = New System.Drawing.Size(85, 20)
            Me.CalidadCapturaTextBox.TabIndex = 12
            Me.CalidadCapturaTextBox.TabStop = False
            Me.CalidadCapturaTextBox.Text = "0"
            Me.CalidadCapturaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'CalidadCapturaCheckBox
            '
            Me.CalidadCapturaCheckBox.AutoSize = True
            Me.CalidadCapturaCheckBox.Location = New System.Drawing.Point(275, 68)
            Me.CalidadCapturaCheckBox.Name = "CalidadCapturaCheckBox"
            Me.CalidadCapturaCheckBox.Size = New System.Drawing.Size(101, 17)
            Me.CalidadCapturaCheckBox.TabIndex = 6
            Me.CalidadCapturaCheckBox.Text = "Calidad Captura"
            Me.CalidadCapturaCheckBox.UseVisualStyleBackColor = True
            '
            'TerceraCapturaTextBox
            '
            Me.TerceraCapturaTextBox.Location = New System.Drawing.Point(145, 198)
            Me.TerceraCapturaTextBox.Name = "TerceraCapturaTextBox"
            Me.TerceraCapturaTextBox.ReadOnly = True
            Me.TerceraCapturaTextBox.Size = New System.Drawing.Size(85, 20)
            Me.TerceraCapturaTextBox.TabIndex = 10
            Me.TerceraCapturaTextBox.TabStop = False
            Me.TerceraCapturaTextBox.Text = "0"
            Me.TerceraCapturaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'TerceraCapturaCheckBox
            '
            Me.TerceraCapturaCheckBox.AutoSize = True
            Me.TerceraCapturaCheckBox.Location = New System.Drawing.Point(20, 200)
            Me.TerceraCapturaCheckBox.Name = "TerceraCapturaCheckBox"
            Me.TerceraCapturaCheckBox.Size = New System.Drawing.Size(103, 17)
            Me.TerceraCapturaCheckBox.TabIndex = 5
            Me.TerceraCapturaCheckBox.Text = "Tercera Captura"
            Me.TerceraCapturaCheckBox.UseVisualStyleBackColor = True
            '
            'ValidacionesTextBox
            '
            Me.ValidacionesTextBox.Location = New System.Drawing.Point(400, 145)
            Me.ValidacionesTextBox.Name = "ValidacionesTextBox"
            Me.ValidacionesTextBox.ReadOnly = True
            Me.ValidacionesTextBox.Size = New System.Drawing.Size(85, 20)
            Me.ValidacionesTextBox.TabIndex = 14
            Me.ValidacionesTextBox.TabStop = False
            Me.ValidacionesTextBox.Text = "0"
            Me.ValidacionesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'ValidacionesCheckBox
            '
            Me.ValidacionesCheckBox.AutoSize = True
            Me.ValidacionesCheckBox.Location = New System.Drawing.Point(275, 147)
            Me.ValidacionesCheckBox.Name = "ValidacionesCheckBox"
            Me.ValidacionesCheckBox.Size = New System.Drawing.Size(86, 17)
            Me.ValidacionesCheckBox.TabIndex = 9
            Me.ValidacionesCheckBox.Text = "Validaciones"
            Me.ValidacionesCheckBox.UseVisualStyleBackColor = True
            '
            'RecorteTextBox
            '
            Me.RecorteTextBox.Location = New System.Drawing.Point(400, 93)
            Me.RecorteTextBox.Name = "RecorteTextBox"
            Me.RecorteTextBox.ReadOnly = True
            Me.RecorteTextBox.Size = New System.Drawing.Size(85, 20)
            Me.RecorteTextBox.TabIndex = 16
            Me.RecorteTextBox.TabStop = False
            Me.RecorteTextBox.Text = "0"
            Me.RecorteTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'RecorteCheckBox
            '
            Me.RecorteCheckBox.AutoSize = True
            Me.RecorteCheckBox.Location = New System.Drawing.Point(275, 95)
            Me.RecorteCheckBox.Name = "RecorteCheckBox"
            Me.RecorteCheckBox.Size = New System.Drawing.Size(64, 17)
            Me.RecorteCheckBox.TabIndex = 7
            Me.RecorteCheckBox.Text = "Recorte"
            Me.RecorteCheckBox.UseVisualStyleBackColor = True
            '
            'MainGroupBox
            '
            Me.MainGroupBox.Controls.Add(Me.OCRCapturaCheckBox)
            Me.MainGroupBox.Controls.Add(Me.OCRCapturaTextBox)
            Me.MainGroupBox.Controls.Add(Me.ValidacionListasTextBox)
            Me.MainGroupBox.Controls.Add(Me.ValidacionListasCheckBox)
            Me.MainGroupBox.Controls.Add(Me.CalidadRecorteTextBox)
            Me.MainGroupBox.Controls.Add(Me.CalidadRecorteCheckBox)
            Me.MainGroupBox.Controls.Add(Me.FechaProcesoLabel)
            Me.MainGroupBox.Controls.Add(Me.IndexacionCheckBox)
            Me.MainGroupBox.Controls.Add(Me.RecorteTextBox)
            Me.MainGroupBox.Controls.Add(Me.IndexacionTextBox)
            Me.MainGroupBox.Controls.Add(Me.RecorteCheckBox)
            Me.MainGroupBox.Controls.Add(Me.PreCapturaCheckBox)
            Me.MainGroupBox.Controls.Add(Me.ValidacionesTextBox)
            Me.MainGroupBox.Controls.Add(Me.PreCapturaTextBox)
            Me.MainGroupBox.Controls.Add(Me.ValidacionesCheckBox)
            Me.MainGroupBox.Controls.Add(Me.PrimeraCapturaCheckBox)
            Me.MainGroupBox.Controls.Add(Me.CalidadCapturaTextBox)
            Me.MainGroupBox.Controls.Add(Me.PrimeraCapturaTextBox)
            Me.MainGroupBox.Controls.Add(Me.CalidadCapturaCheckBox)
            Me.MainGroupBox.Controls.Add(Me.SegundaCapturaCheckBox)
            Me.MainGroupBox.Controls.Add(Me.TerceraCapturaTextBox)
            Me.MainGroupBox.Controls.Add(Me.SegundaCapturaTextBox)
            Me.MainGroupBox.Controls.Add(Me.TerceraCapturaCheckBox)
            Me.MainGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.MainGroupBox.Location = New System.Drawing.Point(5, 0)
            Me.MainGroupBox.Name = "MainGroupBox"
            Me.MainGroupBox.Size = New System.Drawing.Size(494, 239)
            Me.MainGroupBox.TabIndex = 0
            Me.MainGroupBox.TabStop = False
            '
            'ValidacionListasTextBox
            '
            Me.ValidacionListasTextBox.Location = New System.Drawing.Point(400, 171)
            Me.ValidacionListasTextBox.Name = "ValidacionListasTextBox"
            Me.ValidacionListasTextBox.ReadOnly = True
            Me.ValidacionListasTextBox.Size = New System.Drawing.Size(85, 20)
            Me.ValidacionListasTextBox.TabIndex = 21
            Me.ValidacionListasTextBox.TabStop = False
            Me.ValidacionListasTextBox.Text = "0"
            Me.ValidacionListasTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'ValidacionListasCheckBox
            '
            Me.ValidacionListasCheckBox.AutoSize = True
            Me.ValidacionListasCheckBox.Location = New System.Drawing.Point(275, 173)
            Me.ValidacionListasCheckBox.Name = "ValidacionListasCheckBox"
            Me.ValidacionListasCheckBox.Size = New System.Drawing.Size(105, 17)
            Me.ValidacionListasCheckBox.TabIndex = 20
            Me.ValidacionListasCheckBox.Text = "Validacion Listas"
            Me.ValidacionListasCheckBox.UseVisualStyleBackColor = True
            '
            'CalidadRecorteTextBox
            '
            Me.CalidadRecorteTextBox.Location = New System.Drawing.Point(400, 120)
            Me.CalidadRecorteTextBox.Name = "CalidadRecorteTextBox"
            Me.CalidadRecorteTextBox.ReadOnly = True
            Me.CalidadRecorteTextBox.Size = New System.Drawing.Size(85, 20)
            Me.CalidadRecorteTextBox.TabIndex = 19
            Me.CalidadRecorteTextBox.TabStop = False
            Me.CalidadRecorteTextBox.Text = "0"
            Me.CalidadRecorteTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'CalidadRecorteCheckBox
            '
            Me.CalidadRecorteCheckBox.AutoSize = True
            Me.CalidadRecorteCheckBox.Location = New System.Drawing.Point(275, 122)
            Me.CalidadRecorteCheckBox.Name = "CalidadRecorteCheckBox"
            Me.CalidadRecorteCheckBox.Size = New System.Drawing.Size(107, 17)
            Me.CalidadRecorteCheckBox.TabIndex = 8
            Me.CalidadRecorteCheckBox.Text = "Calidad Recortes"
            Me.CalidadRecorteCheckBox.UseVisualStyleBackColor = True
            '
            'FechaProcesoLabel
            '
            Me.FechaProcesoLabel.Dock = System.Windows.Forms.DockStyle.Top
            Me.FechaProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoLabel.ForeColor = System.Drawing.Color.Green
            Me.FechaProcesoLabel.Location = New System.Drawing.Point(3, 16)
            Me.FechaProcesoLabel.Name = "FechaProcesoLabel"
            Me.FechaProcesoLabel.Size = New System.Drawing.Size(488, 29)
            Me.FechaProcesoLabel.TabIndex = 0
            Me.FechaProcesoLabel.Text = "Fecha de Proceso"
            Me.FechaProcesoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'OCRCapturaCheckBox
            '
            Me.OCRCapturaCheckBox.AutoSize = True
            Me.OCRCapturaCheckBox.Location = New System.Drawing.Point(20, 95)
            Me.OCRCapturaCheckBox.Name = "OCRCapturaCheckBox"
            Me.OCRCapturaCheckBox.Size = New System.Drawing.Size(89, 17)
            Me.OCRCapturaCheckBox.TabIndex = 22
            Me.OCRCapturaCheckBox.Text = "OCR Captura"
            Me.OCRCapturaCheckBox.UseVisualStyleBackColor = True
            '
            'OCRCapturaTextBox
            '
            Me.OCRCapturaTextBox.Location = New System.Drawing.Point(145, 93)
            Me.OCRCapturaTextBox.Name = "OCRCapturaTextBox"
            Me.OCRCapturaTextBox.ReadOnly = True
            Me.OCRCapturaTextBox.Size = New System.Drawing.Size(85, 20)
            Me.OCRCapturaTextBox.TabIndex = 23
            Me.OCRCapturaTextBox.TabStop = False
            Me.OCRCapturaTextBox.Text = "0"
            Me.OCRCapturaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'FormDesbloquear
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(504, 281)
            Me.Controls.Add(Me.MainGroupBox)
            Me.Controls.Add(Me.OptionPanel)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormDesbloquear"
            Me.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Desbloquear"
            Me.OptionPanel.ResumeLayout(False)
            Me.MainGroupBox.ResumeLayout(False)
            Me.MainGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents IndexacionCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents IndexacionTextBox As System.Windows.Forms.TextBox
        Friend WithEvents OptionPanel As System.Windows.Forms.Panel
        Friend WithEvents DesbloquearButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents PreCapturaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents PreCapturaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents SegundaCapturaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents SegundaCapturaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents PrimeraCapturaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents PrimeraCapturaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents CalidadCapturaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CalidadCapturaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents TerceraCapturaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents TerceraCapturaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents ValidacionesTextBox As System.Windows.Forms.TextBox
        Friend WithEvents ValidacionesCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents RecorteTextBox As System.Windows.Forms.TextBox
        Friend WithEvents RecorteCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents FechaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents CalidadRecorteTextBox As System.Windows.Forms.TextBox
        Friend WithEvents CalidadRecorteCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents ValidacionListasTextBox As System.Windows.Forms.TextBox
        Friend WithEvents ValidacionListasCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents OCRCapturaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents OCRCapturaTextBox As System.Windows.Forms.TextBox
    End Class
End Namespace