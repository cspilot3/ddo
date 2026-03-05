Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Solicitudes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormDescuelgueDocumentos
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDescuelgueDocumentos))
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.FiltrosGroupBox = New System.Windows.Forms.GroupBox()
            Me.lblEntidadEncontrada = New System.Windows.Forms.Label()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.SolicitudesDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblSolicitudes = New System.Windows.Forms.Label()
            Me.lblEntidad = New System.Windows.Forms.Label()
            Me.SolicitudesGroupBox = New System.Windows.Forms.GroupBox()
            Me.btnImprimirCB = New System.Windows.Forms.Button()
            Me.CargandoPictureBox = New System.Windows.Forms.PictureBox()
            Me.btnExportar = New System.Windows.Forms.Button()
            Me.btnDescuelgue = New System.Windows.Forms.Button()
            Me.cbarrasDesktopCBarrasControl = New Miharu.Desktop.Controls.DesktopCBarras.DesktopCBarrasControl()
            Me.lblCbarras = New System.Windows.Forms.Label()
            Me.SolicitudesDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.CBarras_Folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarras_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Data_1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Data_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Esquema = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Codigo_Boveda_Posicion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Codigo_Caja = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Solicitud_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.R = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.id_Item_Solicitud = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
            Me.DescuelgueBackgroundWorker = New System.ComponentModel.BackgroundWorker()
            Me.DescuelgueBackgroundWorker_2 = New System.ComponentModel.BackgroundWorker()
            Me.pbDescolgando = New System.Windows.Forms.ProgressBar()
            Me.lblDescolgando = New System.Windows.Forms.Label()
            Me.FiltrosGroupBox.SuspendLayout()
            Me.SolicitudesGroupBox.SuspendLayout()
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.SolicitudesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'FiltrosGroupBox
            '
            resources.ApplyResources(Me.FiltrosGroupBox, "FiltrosGroupBox")
            Me.FiltrosGroupBox.Controls.Add(Me.lblEntidadEncontrada)
            Me.FiltrosGroupBox.Controls.Add(Me.BuscarButton)
            Me.FiltrosGroupBox.Controls.Add(Me.SolicitudesDesktopComboBox)
            Me.FiltrosGroupBox.Controls.Add(Me.lblSolicitudes)
            Me.FiltrosGroupBox.Controls.Add(Me.lblEntidad)
            Me.FiltrosGroupBox.Name = "FiltrosGroupBox"
            Me.FiltrosGroupBox.TabStop = False
            '
            'lblEntidadEncontrada
            '
            resources.ApplyResources(Me.lblEntidadEncontrada, "lblEntidadEncontrada")
            Me.lblEntidadEncontrada.Name = "lblEntidadEncontrada"
            '
            'BuscarButton
            '
            resources.ApplyResources(Me.BuscarButton, "BuscarButton")
            Me.BuscarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'SolicitudesDesktopComboBox
            '
            resources.ApplyResources(Me.SolicitudesDesktopComboBox, "SolicitudesDesktopComboBox")
            Me.SolicitudesDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SolicitudesDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SolicitudesDesktopComboBox.DisabledEnter = False
            Me.SolicitudesDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SolicitudesDesktopComboBox.fk_Campo = 0
            Me.SolicitudesDesktopComboBox.fk_Documento = 0
            Me.SolicitudesDesktopComboBox.fk_Validacion = 0
            Me.SolicitudesDesktopComboBox.FormattingEnabled = True
            Me.SolicitudesDesktopComboBox.Name = "SolicitudesDesktopComboBox"
            '
            'lblSolicitudes
            '
            resources.ApplyResources(Me.lblSolicitudes, "lblSolicitudes")
            Me.lblSolicitudes.Name = "lblSolicitudes"
            '
            'lblEntidad
            '
            resources.ApplyResources(Me.lblEntidad, "lblEntidad")
            Me.lblEntidad.Name = "lblEntidad"
            '
            'SolicitudesGroupBox
            '
            Me.SolicitudesGroupBox.Controls.Add(Me.btnImprimirCB)
            Me.SolicitudesGroupBox.Controls.Add(Me.CargandoPictureBox)
            Me.SolicitudesGroupBox.Controls.Add(Me.btnExportar)
            Me.SolicitudesGroupBox.Controls.Add(Me.btnDescuelgue)
            Me.SolicitudesGroupBox.Controls.Add(Me.cbarrasDesktopCBarrasControl)
            Me.SolicitudesGroupBox.Controls.Add(Me.lblCbarras)
            Me.SolicitudesGroupBox.Controls.Add(Me.SolicitudesDataGridView)
            resources.ApplyResources(Me.SolicitudesGroupBox, "SolicitudesGroupBox")
            Me.SolicitudesGroupBox.Name = "SolicitudesGroupBox"
            Me.SolicitudesGroupBox.TabStop = False
            '
            'btnImprimirCB
            '
            resources.ApplyResources(Me.btnImprimirCB, "btnImprimirCB")
            Me.btnImprimirCB.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnPrinter
            Me.btnImprimirCB.Name = "btnImprimirCB"
            Me.btnImprimirCB.UseVisualStyleBackColor = True
            '
            'CargandoPictureBox
            '
            Me.CargandoPictureBox.Image = Global.Miharu.Custody.Library.My.Resources.Resources.ajax_loader
            resources.ApplyResources(Me.CargandoPictureBox, "CargandoPictureBox")
            Me.CargandoPictureBox.Name = "CargandoPictureBox"
            Me.CargandoPictureBox.TabStop = False
            '
            'btnExportar
            '
            resources.ApplyResources(Me.btnExportar, "btnExportar")
            Me.btnExportar.Image = Global.Miharu.Custody.Library.My.Resources.Resources.excel_csv
            Me.btnExportar.Name = "btnExportar"
            Me.btnExportar.UseVisualStyleBackColor = True
            '
            'btnDescuelgue
            '
            resources.ApplyResources(Me.btnDescuelgue, "btnDescuelgue")
            Me.btnDescuelgue.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Aceptar
            Me.btnDescuelgue.Name = "btnDescuelgue"
            Me.btnDescuelgue.UseVisualStyleBackColor = True
            '
            'cbarrasDesktopCBarrasControl
            '
            Me.cbarrasDesktopCBarrasControl.AllowDrop = True
            Me.cbarrasDesktopCBarrasControl.CausesValidation = False
            Me.cbarrasDesktopCBarrasControl.FocusIn = System.Drawing.Color.LightYellow
            Me.cbarrasDesktopCBarrasControl.FocusOut = System.Drawing.Color.White
            resources.ApplyResources(Me.cbarrasDesktopCBarrasControl, "cbarrasDesktopCBarrasControl")
            Me.cbarrasDesktopCBarrasControl.Name = "cbarrasDesktopCBarrasControl"
            Me.cbarrasDesktopCBarrasControl.Permite_Pegar = False
            Me.cbarrasDesktopCBarrasControl.ShortcutsEnabled = False
            '
            'lblCbarras
            '
            resources.ApplyResources(Me.lblCbarras, "lblCbarras")
            Me.lblCbarras.Name = "lblCbarras"
            '
            'SolicitudesDataGridView
            '
            Me.SolicitudesDataGridView.AllowUserToAddRows = False
            Me.SolicitudesDataGridView.AllowUserToDeleteRows = False
            resources.ApplyResources(Me.SolicitudesDataGridView, "SolicitudesDataGridView")
            Me.SolicitudesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.SolicitudesDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.SolicitudesDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.SolicitudesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.SolicitudesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CBarras_Folder, Me.CBarras_File, Me.Data_1, Me.Data_2, Me.Nombre_Esquema, Me.Nombre_Documento, Me.Codigo_Boveda_Posicion, Me.Codigo_Caja, Me.Nombre_Solicitud_Tipo, Me.R, Me.id_Item_Solicitud})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.SolicitudesDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.SolicitudesDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.SolicitudesDataGridView.MultiSelect = False
            Me.SolicitudesDataGridView.Name = "SolicitudesDataGridView"
            Me.SolicitudesDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.SolicitudesDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.SolicitudesDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.SolicitudesDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle4
            Me.SolicitudesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            '
            'CBarras_Folder
            '
            Me.CBarras_Folder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.CBarras_Folder.DataPropertyName = "CBarras_Folder"
            resources.ApplyResources(Me.CBarras_Folder, "CBarras_Folder")
            Me.CBarras_Folder.Name = "CBarras_Folder"
            Me.CBarras_Folder.ReadOnly = True
            '
            'CBarras_File
            '
            Me.CBarras_File.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.CBarras_File.DataPropertyName = "CBarras_File"
            resources.ApplyResources(Me.CBarras_File, "CBarras_File")
            Me.CBarras_File.Name = "CBarras_File"
            Me.CBarras_File.ReadOnly = True
            '
            'Data_1
            '
            Me.Data_1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Data_1.DataPropertyName = "Data_1"
            resources.ApplyResources(Me.Data_1, "Data_1")
            Me.Data_1.Name = "Data_1"
            Me.Data_1.ReadOnly = True
            '
            'Data_2
            '
            Me.Data_2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Data_2.DataPropertyName = "Data_2"
            resources.ApplyResources(Me.Data_2, "Data_2")
            Me.Data_2.Name = "Data_2"
            Me.Data_2.ReadOnly = True
            '
            'Nombre_Esquema
            '
            Me.Nombre_Esquema.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Nombre_Esquema.DataPropertyName = "Nombre_Esquema"
            resources.ApplyResources(Me.Nombre_Esquema, "Nombre_Esquema")
            Me.Nombre_Esquema.Name = "Nombre_Esquema"
            Me.Nombre_Esquema.ReadOnly = True
            '
            'Nombre_Documento
            '
            Me.Nombre_Documento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Nombre_Documento.DataPropertyName = "Nombre_Documento"
            resources.ApplyResources(Me.Nombre_Documento, "Nombre_Documento")
            Me.Nombre_Documento.Name = "Nombre_Documento"
            Me.Nombre_Documento.ReadOnly = True
            '
            'Codigo_Boveda_Posicion
            '
            Me.Codigo_Boveda_Posicion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Codigo_Boveda_Posicion.DataPropertyName = "Codigo_Boveda_Posicion"
            resources.ApplyResources(Me.Codigo_Boveda_Posicion, "Codigo_Boveda_Posicion")
            Me.Codigo_Boveda_Posicion.Name = "Codigo_Boveda_Posicion"
            Me.Codigo_Boveda_Posicion.ReadOnly = True
            '
            'Codigo_Caja
            '
            Me.Codigo_Caja.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Codigo_Caja.DataPropertyName = "Codigo_Caja"
            resources.ApplyResources(Me.Codigo_Caja, "Codigo_Caja")
            Me.Codigo_Caja.Name = "Codigo_Caja"
            Me.Codigo_Caja.ReadOnly = True
            '
            'Nombre_Solicitud_Tipo
            '
            Me.Nombre_Solicitud_Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Nombre_Solicitud_Tipo.DataPropertyName = "Nombre_Solicitud_Tipo"
            resources.ApplyResources(Me.Nombre_Solicitud_Tipo, "Nombre_Solicitud_Tipo")
            Me.Nombre_Solicitud_Tipo.Name = "Nombre_Solicitud_Tipo"
            Me.Nombre_Solicitud_Tipo.ReadOnly = True
            '
            'R
            '
            Me.R.FalseValue = ""
            resources.ApplyResources(Me.R, "R")
            Me.R.Name = "R"
            Me.R.ReadOnly = True
            Me.R.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.R.TrueValue = ""
            '
            'id_Item_Solicitud
            '
            Me.id_Item_Solicitud.DataPropertyName = "id_Item_Solicitud"
            resources.ApplyResources(Me.id_Item_Solicitud, "id_Item_Solicitud")
            Me.id_Item_Solicitud.Name = "id_Item_Solicitud"
            Me.id_Item_Solicitud.ReadOnly = True
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            resources.ApplyResources(Me.CerrarButton, "CerrarButton")
            Me.CerrarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'DescuelgueBackgroundWorker
            '
            Me.DescuelgueBackgroundWorker.WorkerReportsProgress = True
            Me.DescuelgueBackgroundWorker.WorkerSupportsCancellation = True
            '
            'DescuelgueBackgroundWorker_2
            '
            Me.DescuelgueBackgroundWorker_2.WorkerReportsProgress = True
            Me.DescuelgueBackgroundWorker_2.WorkerSupportsCancellation = True
            '
            'pbDescolgando
            '
            resources.ApplyResources(Me.pbDescolgando, "pbDescolgando")
            Me.pbDescolgando.Name = "pbDescolgando"
            Me.pbDescolgando.Step = 1
            Me.pbDescolgando.Style = System.Windows.Forms.ProgressBarStyle.Continuous
            '
            'lblDescolgando
            '
            resources.ApplyResources(Me.lblDescolgando, "lblDescolgando")
            Me.lblDescolgando.Name = "lblDescolgando"
            '
            'FormDescuelgueDocumentos
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CerrarButton
            Me.Controls.Add(Me.lblDescolgando)
            Me.Controls.Add(Me.pbDescolgando)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.SolicitudesGroupBox)
            Me.Controls.Add(Me.FiltrosGroupBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormDescuelgueDocumentos"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.FiltrosGroupBox.ResumeLayout(False)
            Me.FiltrosGroupBox.PerformLayout()
            Me.SolicitudesGroupBox.ResumeLayout(False)
            Me.SolicitudesGroupBox.PerformLayout()
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.SolicitudesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents FiltrosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents SolicitudesDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents lblSolicitudes As System.Windows.Forms.Label
        Friend WithEvents lblEntidad As System.Windows.Forms.Label
        Friend WithEvents SolicitudesGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents SolicitudesDataGridView As DesktopDataGridViewControl
        Friend WithEvents lblCbarras As System.Windows.Forms.Label
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents btnDescuelgue As System.Windows.Forms.Button
        Friend WithEvents btnExportar As System.Windows.Forms.Button
        Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
        Friend WithEvents cbarrasDesktopCBarrasControl As DesktopCbarrasControl
        Friend WithEvents lblEntidadEncontrada As System.Windows.Forms.Label
        Friend WithEvents DescuelgueBackgroundWorker As System.ComponentModel.BackgroundWorker
        Public WithEvents CargandoPictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents DescuelgueBackgroundWorker_2 As System.ComponentModel.BackgroundWorker
        Friend WithEvents pbDescolgando As System.Windows.Forms.ProgressBar
        Friend WithEvents lblDescolgando As System.Windows.Forms.Label
        Friend WithEvents btnImprimirCB As System.Windows.Forms.Button
        Friend WithEvents CBarras_Folder As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarras_File As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Data_1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Data_2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Esquema As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Codigo_Boveda_Posicion As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Codigo_Caja As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Solicitud_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents R As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents id_Item_Solicitud As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace