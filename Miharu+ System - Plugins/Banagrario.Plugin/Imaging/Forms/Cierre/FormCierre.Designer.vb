Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Imaging.Forms.Cierre
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCierre
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
            Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCierre))
            Me.CTA_Reporte_CierreDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.FechaProcesolabel = New System.Windows.Forms.Label()
            Me.FechaProcesodateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.InformeTabPage = New System.Windows.Forms.TabPage()
            Me.CierreReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
            Me.FiltroGroupBox = New System.Windows.Forms.GroupBox()
            Me.EnviarCorreoButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.TabControl1 = New System.Windows.Forms.TabControl()
            Me.TabPage_Fech_Cruce = New System.Windows.Forms.TabPage()
            Me.FechasPendientesPublicacionListBox = New System.Windows.Forms.ListBox()
            Me.TabPageFechPublic = New System.Windows.Forms.TabPage()
            Me.FechasPendientesCruceListBox = New System.Windows.Forms.ListBox()
            Me.OficinaDesktopComboBox = New DesktopComboBoxControl()
            Me.OficinaLabel = New System.Windows.Forms.Label()
            Me.COBDesktopComboBox = New DesktopComboBoxControl()
            Me.COBLabel = New System.Windows.Forms.Label()
            Me.RegionalDesktopComboBox = New DesktopComboBoxControl()
            Me.RegionalLabel = New System.Windows.Forms.Label()
            Me.PublicarButton = New System.Windows.Forms.Button()
            Me.CruzarButton = New System.Windows.Forms.Button()
            Me.PrepararInformeButton = New System.Windows.Forms.Button()
            Me.CierreTabPage = New System.Windows.Forms.TabPage()
            Me.imageList = New System.Windows.Forms.ImageList(Me.components)
            Me.CierreTabControl = New System.Windows.Forms.TabControl()
            CType(Me.CTA_Reporte_CierreDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.InformeTabPage.SuspendLayout()
            Me.FiltroGroupBox.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.TabControl1.SuspendLayout()
            Me.TabPage_Fech_Cruce.SuspendLayout()
            Me.TabPageFechPublic.SuspendLayout()
            Me.CierreTabPage.SuspendLayout()
            Me.CierreTabControl.SuspendLayout()
            Me.SuspendLayout()
            '
            'FechaProcesolabel
            '
            Me.FechaProcesolabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesolabel.AutoSize = True
            Me.FechaProcesolabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesolabel.Location = New System.Drawing.Point(24, 49)
            Me.FechaProcesolabel.Name = "FechaProcesolabel"
            Me.FechaProcesolabel.Size = New System.Drawing.Size(96, 13)
            Me.FechaProcesolabel.TabIndex = 22
            Me.FechaProcesolabel.Text = "Fecha Proceso:"
            '
            'FechaProcesodateTimePicker
            '
            Me.FechaProcesodateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesodateTimePicker.CustomFormat = "yyyy/MM/dd"
            Me.FechaProcesodateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.FechaProcesodateTimePicker.Location = New System.Drawing.Point(128, 45)
            Me.FechaProcesodateTimePicker.Name = "FechaProcesodateTimePicker"
            Me.FechaProcesodateTimePicker.Size = New System.Drawing.Size(84, 20)
            Me.FechaProcesodateTimePicker.TabIndex = 23
            '
            'InformeTabPage
            '
            Me.InformeTabPage.Controls.Add(Me.CierreReportViewer)
            Me.InformeTabPage.ImageIndex = 1
            Me.InformeTabPage.Location = New System.Drawing.Point(4, 26)
            Me.InformeTabPage.Name = "InformeTabPage"
            Me.InformeTabPage.Padding = New System.Windows.Forms.Padding(3)
            Me.InformeTabPage.Size = New System.Drawing.Size(737, 389)
            Me.InformeTabPage.TabIndex = 1
            Me.InformeTabPage.Text = "Informe"
            Me.InformeTabPage.UseVisualStyleBackColor = True
            '
            'CierreReportViewer
            '
            Me.CierreReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
            ReportDataSource1.Name = "CierreDataSet"
            ReportDataSource1.Value = Me.CTA_Reporte_CierreDataTableBindingSource
            Me.CierreReportViewer.LocalReport.DataSources.Add(ReportDataSource1)
            Me.CierreReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.ResultadoCierre.rdlc"
            Me.CierreReportViewer.Location = New System.Drawing.Point(3, 3)
            Me.CierreReportViewer.Name = "CierreReportViewer"
            Me.CierreReportViewer.Size = New System.Drawing.Size(731, 383)
            Me.CierreReportViewer.TabIndex = 0
            '
            'FiltroGroupBox
            '
            Me.FiltroGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FiltroGroupBox.Controls.Add(Me.EnviarCorreoButton)
            Me.FiltroGroupBox.Controls.Add(Me.GroupBox1)
            Me.FiltroGroupBox.Controls.Add(Me.OficinaDesktopComboBox)
            Me.FiltroGroupBox.Controls.Add(Me.OficinaLabel)
            Me.FiltroGroupBox.Controls.Add(Me.COBDesktopComboBox)
            Me.FiltroGroupBox.Controls.Add(Me.COBLabel)
            Me.FiltroGroupBox.Controls.Add(Me.RegionalDesktopComboBox)
            Me.FiltroGroupBox.Controls.Add(Me.RegionalLabel)
            Me.FiltroGroupBox.Controls.Add(Me.FechaProcesodateTimePicker)
            Me.FiltroGroupBox.Controls.Add(Me.FechaProcesolabel)
            Me.FiltroGroupBox.Controls.Add(Me.PublicarButton)
            Me.FiltroGroupBox.Controls.Add(Me.CruzarButton)
            Me.FiltroGroupBox.Controls.Add(Me.PrepararInformeButton)
            Me.FiltroGroupBox.Location = New System.Drawing.Point(8, 13)
            Me.FiltroGroupBox.Name = "FiltroGroupBox"
            Me.FiltroGroupBox.Size = New System.Drawing.Size(721, 368)
            Me.FiltroGroupBox.TabIndex = 0
            Me.FiltroGroupBox.TabStop = False
            Me.FiltroGroupBox.Text = "Filtro de Cierre"
            '
            'EnviarCorreoButton
            '
            Me.EnviarCorreoButton.AccessibleDescription = ""
            Me.EnviarCorreoButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.EnviarCorreoButton.BackColor = System.Drawing.SystemColors.Control
            Me.EnviarCorreoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.EnviarCorreoButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EnviarCorreoButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.envio_correo
            Me.EnviarCorreoButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.EnviarCorreoButton.Location = New System.Drawing.Point(391, 230)
            Me.EnviarCorreoButton.Name = "EnviarCorreoButton"
            Me.EnviarCorreoButton.Size = New System.Drawing.Size(100, 60)
            Me.EnviarCorreoButton.TabIndex = 37
            Me.EnviarCorreoButton.Tag = "Ctrl + P"
            Me.EnviarCorreoButton.Text = "&Enviar Correo"
            Me.EnviarCorreoButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.EnviarCorreoButton.UseVisualStyleBackColor = False
            Me.EnviarCorreoButton.Visible = False
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.GroupBox1.Controls.Add(Me.TabControl1)
            Me.GroupBox1.Location = New System.Drawing.Point(506, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(184, 327)
            Me.GroupBox1.TabIndex = 35
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Fechas Pendientes"
            '
            'TabControl1
            '
            Me.TabControl1.Controls.Add(Me.TabPage_Fech_Cruce)
            Me.TabControl1.Controls.Add(Me.TabPageFechPublic)
            Me.TabControl1.Location = New System.Drawing.Point(6, 19)
            Me.TabControl1.Name = "TabControl1"
            Me.TabControl1.SelectedIndex = 0
            Me.TabControl1.Size = New System.Drawing.Size(167, 290)
            Me.TabControl1.TabIndex = 33
            '
            'TabPage_Fech_Cruce
            '
            Me.TabPage_Fech_Cruce.Controls.Add(Me.FechasPendientesPublicacionListBox)
            Me.TabPage_Fech_Cruce.Location = New System.Drawing.Point(4, 22)
            Me.TabPage_Fech_Cruce.Name = "TabPage_Fech_Cruce"
            Me.TabPage_Fech_Cruce.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage_Fech_Cruce.Size = New System.Drawing.Size(159, 264)
            Me.TabPage_Fech_Cruce.TabIndex = 0
            Me.TabPage_Fech_Cruce.Text = "Publicación"
            Me.TabPage_Fech_Cruce.UseVisualStyleBackColor = True
            '
            'FechasPendientesPublicacionListBox
            '
            Me.FechasPendientesPublicacionListBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.FechasPendientesPublicacionListBox.FormattingEnabled = True
            Me.FechasPendientesPublicacionListBox.Location = New System.Drawing.Point(3, 3)
            Me.FechasPendientesPublicacionListBox.Name = "FechasPendientesPublicacionListBox"
            Me.FechasPendientesPublicacionListBox.Size = New System.Drawing.Size(153, 258)
            Me.FechasPendientesPublicacionListBox.TabIndex = 32
            '
            'TabPageFechPublic
            '
            Me.TabPageFechPublic.Controls.Add(Me.FechasPendientesCruceListBox)
            Me.TabPageFechPublic.Location = New System.Drawing.Point(4, 22)
            Me.TabPageFechPublic.Name = "TabPageFechPublic"
            Me.TabPageFechPublic.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPageFechPublic.Size = New System.Drawing.Size(159, 264)
            Me.TabPageFechPublic.TabIndex = 1
            Me.TabPageFechPublic.Text = "Cruce"
            Me.TabPageFechPublic.UseVisualStyleBackColor = True
            '
            'FechasPendientesCruceListBox
            '
            Me.FechasPendientesCruceListBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.FechasPendientesCruceListBox.FormattingEnabled = True
            Me.FechasPendientesCruceListBox.Location = New System.Drawing.Point(3, 3)
            Me.FechasPendientesCruceListBox.Name = "FechasPendientesCruceListBox"
            Me.FechasPendientesCruceListBox.Size = New System.Drawing.Size(153, 258)
            Me.FechasPendientesCruceListBox.TabIndex = 31
            '
            'OficinaDesktopComboBox
            '
            Me.OficinaDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.OficinaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OficinaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OficinaDesktopComboBox.DisabledEnter = False
            Me.OficinaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OficinaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OficinaDesktopComboBox.FormattingEnabled = True
            Me.OficinaDesktopComboBox.Location = New System.Drawing.Point(128, 161)
            Me.OficinaDesktopComboBox.Name = "OficinaDesktopComboBox"
            Me.OficinaDesktopComboBox.Size = New System.Drawing.Size(302, 21)
            Me.OficinaDesktopComboBox.TabIndex = 29
            Me.OficinaDesktopComboBox.Visible = False
            '
            'OficinaLabel
            '
            Me.OficinaLabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.OficinaLabel.AutoSize = True
            Me.OficinaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OficinaLabel.Location = New System.Drawing.Point(24, 169)
            Me.OficinaLabel.Name = "OficinaLabel"
            Me.OficinaLabel.Size = New System.Drawing.Size(51, 13)
            Me.OficinaLabel.TabIndex = 28
            Me.OficinaLabel.Text = "Oficina:"
            Me.OficinaLabel.Visible = False
            '
            'COBDesktopComboBox
            '
            Me.COBDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.COBDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.COBDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.COBDesktopComboBox.DisabledEnter = False
            Me.COBDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.COBDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.COBDesktopComboBox.FormattingEnabled = True
            Me.COBDesktopComboBox.Location = New System.Drawing.Point(128, 121)
            Me.COBDesktopComboBox.Name = "COBDesktopComboBox"
            Me.COBDesktopComboBox.Size = New System.Drawing.Size(302, 21)
            Me.COBDesktopComboBox.TabIndex = 27
            '
            'COBLabel
            '
            Me.COBLabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.COBLabel.AutoSize = True
            Me.COBLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.COBLabel.Location = New System.Drawing.Point(24, 124)
            Me.COBLabel.Name = "COBLabel"
            Me.COBLabel.Size = New System.Drawing.Size(36, 13)
            Me.COBLabel.TabIndex = 26
            Me.COBLabel.Text = "COB:"
            '
            'RegionalDesktopComboBox
            '
            Me.RegionalDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.RegionalDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.RegionalDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.RegionalDesktopComboBox.DisabledEnter = False
            Me.RegionalDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.RegionalDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.RegionalDesktopComboBox.FormattingEnabled = True
            Me.RegionalDesktopComboBox.Location = New System.Drawing.Point(128, 81)
            Me.RegionalDesktopComboBox.Name = "RegionalDesktopComboBox"
            Me.RegionalDesktopComboBox.Size = New System.Drawing.Size(302, 21)
            Me.RegionalDesktopComboBox.TabIndex = 25
            '
            'RegionalLabel
            '
            Me.RegionalLabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.RegionalLabel.AutoSize = True
            Me.RegionalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.RegionalLabel.Location = New System.Drawing.Point(24, 84)
            Me.RegionalLabel.Name = "RegionalLabel"
            Me.RegionalLabel.Size = New System.Drawing.Size(61, 13)
            Me.RegionalLabel.TabIndex = 24
            Me.RegionalLabel.Text = "Regional:"
            '
            'PublicarButton
            '
            Me.PublicarButton.AccessibleDescription = ""
            Me.PublicarButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.PublicarButton.BackColor = System.Drawing.SystemColors.Control
            Me.PublicarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.PublicarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PublicarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Process_Accept
            Me.PublicarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.PublicarButton.Location = New System.Drawing.Point(269, 230)
            Me.PublicarButton.Name = "PublicarButton"
            Me.PublicarButton.Size = New System.Drawing.Size(100, 60)
            Me.PublicarButton.TabIndex = 21
            Me.PublicarButton.Tag = "Ctrl + P"
            Me.PublicarButton.Text = "&Publicar"
            Me.PublicarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.PublicarButton.UseVisualStyleBackColor = False
            '
            'CruzarButton
            '
            Me.CruzarButton.AccessibleDescription = ""
            Me.CruzarButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.CruzarButton.BackColor = System.Drawing.SystemColors.Control
            Me.CruzarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CruzarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CruzarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.cross
            Me.CruzarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CruzarButton.Location = New System.Drawing.Point(148, 230)
            Me.CruzarButton.Name = "CruzarButton"
            Me.CruzarButton.Size = New System.Drawing.Size(100, 60)
            Me.CruzarButton.TabIndex = 21
            Me.CruzarButton.Tag = "Ctrl + C"
            Me.CruzarButton.Text = "&Cruzar"
            Me.CruzarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CruzarButton.UseVisualStyleBackColor = False
            '
            'PrepararInformeButton
            '
            Me.PrepararInformeButton.AccessibleDescription = ""
            Me.PrepararInformeButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.PrepararInformeButton.BackColor = System.Drawing.SystemColors.Control
            Me.PrepararInformeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.PrepararInformeButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PrepararInformeButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.prepare
            Me.PrepararInformeButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.PrepararInformeButton.Location = New System.Drawing.Point(26, 230)
            Me.PrepararInformeButton.Name = "PrepararInformeButton"
            Me.PrepararInformeButton.Size = New System.Drawing.Size(100, 60)
            Me.PrepararInformeButton.TabIndex = 20
            Me.PrepararInformeButton.Tag = "Ctrl + P"
            Me.PrepararInformeButton.Text = "&Preparar Inf."
            Me.PrepararInformeButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.PrepararInformeButton.UseVisualStyleBackColor = False
            '
            'CierreTabPage
            '
            Me.CierreTabPage.Controls.Add(Me.FiltroGroupBox)
            Me.CierreTabPage.ImageIndex = 0
            Me.CierreTabPage.Location = New System.Drawing.Point(4, 26)
            Me.CierreTabPage.Name = "CierreTabPage"
            Me.CierreTabPage.Padding = New System.Windows.Forms.Padding(3)
            Me.CierreTabPage.Size = New System.Drawing.Size(737, 389)
            Me.CierreTabPage.TabIndex = 0
            Me.CierreTabPage.Text = "Cierre"
            Me.CierreTabPage.UseVisualStyleBackColor = True
            '
            'imageList
            '
            Me.imageList.ImageStream = CType(resources.GetObject("imageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList.TransparentColor = System.Drawing.Color.Transparent
            Me.imageList.Images.SetKeyName(0, "Cierre.ico")
            Me.imageList.Images.SetKeyName(1, "Reportes.ico")
            '
            'CierreTabControl
            '
            Me.CierreTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
            Me.CierreTabControl.Controls.Add(Me.CierreTabPage)
            Me.CierreTabControl.Controls.Add(Me.InformeTabPage)
            Me.CierreTabControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CierreTabControl.ImageList = Me.imageList
            Me.CierreTabControl.Location = New System.Drawing.Point(0, 0)
            Me.CierreTabControl.Name = "CierreTabControl"
            Me.CierreTabControl.SelectedIndex = 0
            Me.CierreTabControl.Size = New System.Drawing.Size(745, 419)
            Me.CierreTabControl.TabIndex = 2
            '
            'FormCierre
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(745, 419)
            Me.Controls.Add(Me.CierreTabControl)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormCierre"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "FormCierre"
            CType(Me.CTA_Reporte_CierreDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.InformeTabPage.ResumeLayout(False)
            Me.FiltroGroupBox.ResumeLayout(False)
            Me.FiltroGroupBox.PerformLayout()
            Me.GroupBox1.ResumeLayout(False)
            Me.TabControl1.ResumeLayout(False)
            Me.TabPage_Fech_Cruce.ResumeLayout(False)
            Me.TabPageFechPublic.ResumeLayout(False)
            Me.CierreTabPage.ResumeLayout(False)
            Me.CierreTabControl.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents FechaProcesolabel As System.Windows.Forms.Label
        Private WithEvents CTA_Reporte_CierreDataTableBindingSource As System.Windows.Forms.BindingSource
        Private WithEvents FechaProcesodateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents InformeTabPage As System.Windows.Forms.TabPage
        Private WithEvents CierreReportViewer As Microsoft.Reporting.WinForms.ReportViewer
        Friend WithEvents FiltroGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CruzarButton As System.Windows.Forms.Button
        Friend WithEvents PrepararInformeButton As System.Windows.Forms.Button
        Friend WithEvents CierreTabPage As System.Windows.Forms.TabPage
        Private WithEvents imageList As System.Windows.Forms.ImageList
        Friend WithEvents CierreTabControl As System.Windows.Forms.TabControl
        Friend WithEvents OficinaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents OficinaLabel As System.Windows.Forms.Label
        Friend WithEvents COBDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents COBLabel As System.Windows.Forms.Label
        Friend WithEvents RegionalDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents RegionalLabel As System.Windows.Forms.Label
        Friend WithEvents PublicarButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
        Friend WithEvents TabPage_Fech_Cruce As System.Windows.Forms.TabPage
        Friend WithEvents FechasPendientesCruceListBox As System.Windows.Forms.ListBox
        Friend WithEvents TabPageFechPublic As System.Windows.Forms.TabPage
        Friend WithEvents FechasPendientesPublicacionListBox As System.Windows.Forms.ListBox
        Friend WithEvents EnviarCorreoButton As System.Windows.Forms.Button
    End Class
End Namespace