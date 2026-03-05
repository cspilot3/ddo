Namespace Imaging.Forms.Cargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCargueCanje
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
            Me.FiltroGroupBox = New System.Windows.Forms.GroupBox()
            Me.FechaProcDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.FechaMovDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.SeleccionarArchivo_Button = New System.Windows.Forms.Button()
            Me.ArchivoCargue_TextBox = New System.Windows.Forms.TextBox()
            Me.RegionalLabel = New System.Windows.Forms.Label()
            Me.CargarTif_Button = New System.Windows.Forms.Button()
            Me.CargarJpg_Button = New System.Windows.Forms.Button()
            Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Cancelar_Button = New System.Windows.Forms.Button()
            Me.Estado_Cargue_Label = New System.Windows.Forms.Label()
            Me.EstadoCargue_ProgressBar = New System.Windows.Forms.ProgressBar()
            Me.Cargar_BackgroundWorker = New System.ComponentModel.BackgroundWorker()
            Me.FiltroGroupBox.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'FiltroGroupBox
            '
            Me.FiltroGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FiltroGroupBox.Controls.Add(Me.FechaProcDateTimePicker)
            Me.FiltroGroupBox.Controls.Add(Me.Label2)
            Me.FiltroGroupBox.Controls.Add(Me.FechaMovDateTimePicker)
            Me.FiltroGroupBox.Controls.Add(Me.Label1)
            Me.FiltroGroupBox.Controls.Add(Me.SeleccionarArchivo_Button)
            Me.FiltroGroupBox.Controls.Add(Me.ArchivoCargue_TextBox)
            Me.FiltroGroupBox.Controls.Add(Me.RegionalLabel)
            Me.FiltroGroupBox.Controls.Add(Me.CargarTif_Button)
            Me.FiltroGroupBox.Controls.Add(Me.CargarJpg_Button)
            Me.FiltroGroupBox.Location = New System.Drawing.Point(12, 12)
            Me.FiltroGroupBox.Name = "FiltroGroupBox"
            Me.FiltroGroupBox.Size = New System.Drawing.Size(588, 213)
            Me.FiltroGroupBox.TabIndex = 1
            Me.FiltroGroupBox.TabStop = False
            Me.FiltroGroupBox.Text = "Opciones de cargue"
            '
            'FechaProcDateTimePicker
            '
            Me.FechaProcDateTimePicker.Location = New System.Drawing.Point(165, 29)
            Me.FechaProcDateTimePicker.Name = "FechaProcDateTimePicker"
            Me.FechaProcDateTimePicker.Size = New System.Drawing.Size(341, 20)
            Me.FechaProcDateTimePicker.TabIndex = 31
            '
            'Label2
            '
            Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(33, 29)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(96, 13)
            Me.Label2.TabIndex = 30
            Me.Label2.Text = "Fecha Proceso:"
            '
            'FechaMovDateTimePicker
            '
            Me.FechaMovDateTimePicker.Location = New System.Drawing.Point(167, 65)
            Me.FechaMovDateTimePicker.Name = "FechaMovDateTimePicker"
            Me.FechaMovDateTimePicker.Size = New System.Drawing.Size(341, 20)
            Me.FechaMovDateTimePicker.TabIndex = 29
            '
            'Label1
            '
            Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(30, 65)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(114, 13)
            Me.Label1.TabIndex = 28
            Me.Label1.Text = "Fecha Movimiento:"
            '
            'SeleccionarArchivo_Button
            '
            Me.SeleccionarArchivo_Button.Location = New System.Drawing.Point(519, 98)
            Me.SeleccionarArchivo_Button.Name = "SeleccionarArchivo_Button"
            Me.SeleccionarArchivo_Button.Size = New System.Drawing.Size(39, 23)
            Me.SeleccionarArchivo_Button.TabIndex = 27
            Me.SeleccionarArchivo_Button.Text = "..."
            Me.SeleccionarArchivo_Button.UseVisualStyleBackColor = True
            '
            'ArchivoCargue_TextBox
            '
            Me.ArchivoCargue_TextBox.Location = New System.Drawing.Point(167, 100)
            Me.ArchivoCargue_TextBox.Name = "ArchivoCargue_TextBox"
            Me.ArchivoCargue_TextBox.Size = New System.Drawing.Size(341, 20)
            Me.ArchivoCargue_TextBox.TabIndex = 26
            '
            'RegionalLabel
            '
            Me.RegionalLabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.RegionalLabel.AutoSize = True
            Me.RegionalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.RegionalLabel.Location = New System.Drawing.Point(30, 103)
            Me.RegionalLabel.Name = "RegionalLabel"
            Me.RegionalLabel.Size = New System.Drawing.Size(54, 13)
            Me.RegionalLabel.TabIndex = 24
            Me.RegionalLabel.Text = "Archivo:"
            '
            'CargarTif_Button
            '
            Me.CargarTif_Button.AccessibleDescription = ""
            Me.CargarTif_Button.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.CargarTif_Button.BackColor = System.Drawing.SystemColors.Control
            Me.CargarTif_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CargarTif_Button.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CargarTif_Button.Image = Global.Banagrario.Plugin.My.Resources.Resources.File_extension_tif
            Me.CargarTif_Button.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CargarTif_Button.Location = New System.Drawing.Point(343, 139)
            Me.CargarTif_Button.Name = "CargarTif_Button"
            Me.CargarTif_Button.Size = New System.Drawing.Size(171, 60)
            Me.CargarTif_Button.TabIndex = 21
            Me.CargarTif_Button.Tag = "Ctrl + C"
            Me.CargarTif_Button.Text = "Cargar desde &TIF"
            Me.CargarTif_Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CargarTif_Button.UseVisualStyleBackColor = False
            '
            'CargarJpg_Button
            '
            Me.CargarJpg_Button.AccessibleDescription = ""
            Me.CargarJpg_Button.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.CargarJpg_Button.BackColor = System.Drawing.SystemColors.Control
            Me.CargarJpg_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CargarJpg_Button.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CargarJpg_Button.Image = Global.Banagrario.Plugin.My.Resources.Resources.File_extension_jpg
            Me.CargarJpg_Button.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CargarJpg_Button.Location = New System.Drawing.Point(47, 139)
            Me.CargarJpg_Button.Name = "CargarJpg_Button"
            Me.CargarJpg_Button.Size = New System.Drawing.Size(193, 60)
            Me.CargarJpg_Button.TabIndex = 20
            Me.CargarJpg_Button.Tag = "Ctrl + P"
            Me.CargarJpg_Button.Text = "Cargar desde &JPG"
            Me.CargarJpg_Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CargarJpg_Button.UseVisualStyleBackColor = False
            '
            'OpenFileDialog
            '
            Me.OpenFileDialog.FileName = "OpenFileDialog1"
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Cancelar_Button)
            Me.GroupBox1.Controls.Add(Me.Estado_Cargue_Label)
            Me.GroupBox1.Controls.Add(Me.EstadoCargue_ProgressBar)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 231)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(588, 90)
            Me.GroupBox1.TabIndex = 30
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Estado"
            '
            'Cancelar_Button
            '
            Me.Cancelar_Button.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir
            Me.Cancelar_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Cancelar_Button.Location = New System.Drawing.Point(250, 62)
            Me.Cancelar_Button.Name = "Cancelar_Button"
            Me.Cancelar_Button.Size = New System.Drawing.Size(122, 23)
            Me.Cancelar_Button.TabIndex = 30
            Me.Cancelar_Button.Text = "Cancelar"
            Me.Cancelar_Button.UseVisualStyleBackColor = True
            '
            'Estado_Cargue_Label
            '
            Me.Estado_Cargue_Label.Location = New System.Drawing.Point(49, 16)
            Me.Estado_Cargue_Label.Name = "Estado_Cargue_Label"
            Me.Estado_Cargue_Label.Size = New System.Drawing.Size(509, 14)
            Me.Estado_Cargue_Label.TabIndex = 29
            Me.Estado_Cargue_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'EstadoCargue_ProgressBar
            '
            Me.EstadoCargue_ProgressBar.Location = New System.Drawing.Point(17, 33)
            Me.EstadoCargue_ProgressBar.Name = "EstadoCargue_ProgressBar"
            Me.EstadoCargue_ProgressBar.Size = New System.Drawing.Size(557, 23)
            Me.EstadoCargue_ProgressBar.TabIndex = 28
            '
            'Cargar_BackgroundWorker
            '
            Me.Cargar_BackgroundWorker.WorkerReportsProgress = True
            Me.Cargar_BackgroundWorker.WorkerSupportsCancellation = True
            '
            'FormCargueCanje
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(612, 333)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.FiltroGroupBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCargueCanje"
            Me.Text = "Cargue de archivos de canje"
            Me.FiltroGroupBox.ResumeLayout(False)
            Me.FiltroGroupBox.PerformLayout()
            Me.GroupBox1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents FiltroGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents RegionalLabel As System.Windows.Forms.Label
        Friend WithEvents CargarTif_Button As System.Windows.Forms.Button
        Friend WithEvents CargarJpg_Button As System.Windows.Forms.Button
        Friend WithEvents ArchivoCargue_TextBox As System.Windows.Forms.TextBox
        Friend WithEvents SeleccionarArchivo_Button As System.Windows.Forms.Button
        Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents EstadoCargue_ProgressBar As System.Windows.Forms.ProgressBar
        Friend WithEvents Estado_Cargue_Label As System.Windows.Forms.Label
        Friend WithEvents FechaMovDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Cargar_BackgroundWorker As System.ComponentModel.BackgroundWorker
        Friend WithEvents Cancelar_Button As System.Windows.Forms.Button
        Friend WithEvents FechaProcDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label2 As System.Windows.Forms.Label
    End Class
End Namespace