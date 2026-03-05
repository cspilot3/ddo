Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Procesos.Reproceso
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormReprocesos
        Inherits Desktop.Library.FormBase

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
            Me.components = New System.ComponentModel.Container()
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormReprocesos))
            Me.EliminarButton = New System.Windows.Forms.Button()
            Me.CentroComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.CentroProcesamientoL = New System.Windows.Forms.Label()
            Me.SedeComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.SedeProcesaminetoL = New System.Windows.Forms.Label()
            Me.MotivoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.ReemplazarImagenButton = New System.Windows.Forms.Button()
            Me.MotivoLabel = New System.Windows.Forms.Label()
            Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.ProcesarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.EliminaActualizacionButton = New System.Windows.Forms.Button()
            Me.ProcesarDemandaButton = New System.Windows.Forms.Button()
            Me.reIndexarButton = New System.Windows.Forms.Button()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.FiltroGroupBox = New System.Windows.Forms.GroupBox()
            Me.ReprocesosDataGridView = New Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl()
            Me.R = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.fk_Expediente = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Motivo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_CargueColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Cargue_PaqueteColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Observaciones = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Actualizado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.fk_Folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_File = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FiltroGroupBox.SuspendLayout()
            CType(Me.ReprocesosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'EliminarButton
            '
            Me.EliminarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EliminarButton.BackColor = System.Drawing.SystemColors.Control
            Me.EliminarButton.FlatAppearance.BorderSize = 0
            Me.EliminarButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.EliminarButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.EliminarButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EliminarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Remove_Image
            Me.EliminarButton.Location = New System.Drawing.Point(792, 295)
            Me.EliminarButton.Name = "EliminarButton"
            Me.EliminarButton.Size = New System.Drawing.Size(45, 44)
            Me.EliminarButton.TabIndex = 8
            Me.ToolTip.SetToolTip(Me.EliminarButton, "Eliminar imágen")
            Me.EliminarButton.UseVisualStyleBackColor = False
            '
            'CentroComboBox
            '
            Me.CentroComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.CentroComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.CentroComboBox.DisabledEnter = False
            Me.CentroComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CentroComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CentroComboBox.FormattingEnabled = True
            Me.CentroComboBox.Location = New System.Drawing.Point(156, 97)
            Me.CentroComboBox.Name = "CentroComboBox"
            Me.CentroComboBox.Size = New System.Drawing.Size(345, 21)
            Me.CentroComboBox.TabIndex = 11
            '
            'CentroProcesamientoL
            '
            Me.CentroProcesamientoL.AllowDrop = True
            Me.CentroProcesamientoL.AutoSize = True
            Me.CentroProcesamientoL.Location = New System.Drawing.Point(6, 100)
            Me.CentroProcesamientoL.Name = "CentroProcesamientoL"
            Me.CentroProcesamientoL.Size = New System.Drawing.Size(136, 13)
            Me.CentroProcesamientoL.TabIndex = 10
            Me.CentroProcesamientoL.Text = "Centro Procesamiento:"
            '
            'SedeComboBox
            '
            Me.SedeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SedeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SedeComboBox.DisabledEnter = False
            Me.SedeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SedeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SedeComboBox.FormattingEnabled = True
            Me.SedeComboBox.Location = New System.Drawing.Point(156, 62)
            Me.SedeComboBox.Name = "SedeComboBox"
            Me.SedeComboBox.Size = New System.Drawing.Size(345, 21)
            Me.SedeComboBox.TabIndex = 9
            '
            'SedeProcesaminetoL
            '
            Me.SedeProcesaminetoL.AutoSize = True
            Me.SedeProcesaminetoL.Location = New System.Drawing.Point(6, 65)
            Me.SedeProcesaminetoL.Name = "SedeProcesaminetoL"
            Me.SedeProcesaminetoL.Size = New System.Drawing.Size(126, 13)
            Me.SedeProcesaminetoL.TabIndex = 8
            Me.SedeProcesaminetoL.Text = "Sede Procesamiento:"
            '
            'MotivoComboBox
            '
            Me.MotivoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.MotivoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.MotivoComboBox.DisabledEnter = False
            Me.MotivoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.MotivoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.MotivoComboBox.FormattingEnabled = True
            Me.MotivoComboBox.Location = New System.Drawing.Point(156, 23)
            Me.MotivoComboBox.Name = "MotivoComboBox"
            Me.MotivoComboBox.Size = New System.Drawing.Size(345, 21)
            Me.MotivoComboBox.TabIndex = 7
            '
            'ReemplazarImagenButton
            '
            Me.ReemplazarImagenButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ReemplazarImagenButton.BackColor = System.Drawing.SystemColors.Control
            Me.ReemplazarImagenButton.FlatAppearance.BorderSize = 0
            Me.ReemplazarImagenButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.ReemplazarImagenButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.ReemplazarImagenButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ReemplazarImagenButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.replace_image
            Me.ReemplazarImagenButton.Location = New System.Drawing.Point(792, 245)
            Me.ReemplazarImagenButton.Name = "ReemplazarImagenButton"
            Me.ReemplazarImagenButton.Size = New System.Drawing.Size(45, 44)
            Me.ReemplazarImagenButton.TabIndex = 7
            Me.ToolTip.SetToolTip(Me.ReemplazarImagenButton, "Reemplazar Imagen")
            Me.ReemplazarImagenButton.UseVisualStyleBackColor = False
            '
            'MotivoLabel
            '
            Me.MotivoLabel.AutoSize = True
            Me.MotivoLabel.Location = New System.Drawing.Point(6, 28)
            Me.MotivoLabel.Name = "MotivoLabel"
            Me.MotivoLabel.Size = New System.Drawing.Size(49, 13)
            Me.MotivoLabel.TabIndex = 6
            Me.MotivoLabel.Text = "Motivo:"
            '
            'ProcesarButton
            '
            Me.ProcesarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ProcesarButton.BackColor = System.Drawing.SystemColors.Control
            Me.ProcesarButton.FlatAppearance.BorderColor = System.Drawing.Color.Black
            Me.ProcesarButton.FlatAppearance.BorderSize = 0
            Me.ProcesarButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.ProcesarButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.ProcesarButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProcesarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Process_Accept
            Me.ProcesarButton.Location = New System.Drawing.Point(792, 145)
            Me.ProcesarButton.Name = "ProcesarButton"
            Me.ProcesarButton.Size = New System.Drawing.Size(45, 44)
            Me.ProcesarButton.TabIndex = 9
            Me.ToolTip.SetToolTip(Me.ProcesarButton, "Cambiar tipo documental")
            Me.ProcesarButton.UseVisualStyleBackColor = False
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.BackColor = System.Drawing.SystemColors.Control
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.FlatAppearance.BorderSize = 0
            Me.CerrarButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.CerrarButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.CerrarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Close_Red
            Me.CerrarButton.Location = New System.Drawing.Point(792, 425)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(45, 44)
            Me.CerrarButton.TabIndex = 10
            Me.ToolTip.SetToolTip(Me.CerrarButton, "Cerrar Ventana")
            Me.CerrarButton.UseVisualStyleBackColor = False
            '
            'EliminaActualizacionButton
            '
            Me.EliminaActualizacionButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EliminaActualizacionButton.BackColor = System.Drawing.SystemColors.Control
            Me.EliminaActualizacionButton.FlatAppearance.BorderSize = 0
            Me.EliminaActualizacionButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.EliminaActualizacionButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.EliminaActualizacionButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EliminaActualizacionButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.remove_update
            Me.EliminaActualizacionButton.Location = New System.Drawing.Point(792, 345)
            Me.EliminaActualizacionButton.Name = "EliminaActualizacionButton"
            Me.EliminaActualizacionButton.Size = New System.Drawing.Size(45, 44)
            Me.EliminaActualizacionButton.TabIndex = 12
            Me.ToolTip.SetToolTip(Me.EliminaActualizacionButton, "Elimina actualización de imágenes.")
            Me.EliminaActualizacionButton.UseVisualStyleBackColor = False
            '
            'ProcesarDemandaButton
            '
            Me.ProcesarDemandaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ProcesarDemandaButton.BackColor = System.Drawing.SystemColors.Control
            Me.ProcesarDemandaButton.FlatAppearance.BorderColor = System.Drawing.Color.Black
            Me.ProcesarDemandaButton.FlatAppearance.BorderSize = 0
            Me.ProcesarDemandaButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.ProcesarDemandaButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.ProcesarDemandaButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProcesarDemandaButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Process_Accept
            Me.ProcesarDemandaButton.Location = New System.Drawing.Point(744, 65)
            Me.ProcesarDemandaButton.Name = "ProcesarDemandaButton"
            Me.ProcesarDemandaButton.Size = New System.Drawing.Size(75, 44)
            Me.ProcesarDemandaButton.TabIndex = 12
            Me.ToolTip.SetToolTip(Me.ProcesarDemandaButton, "Cambiar tipo documental por demanda")
            Me.ProcesarDemandaButton.UseVisualStyleBackColor = False
            '
            'reIndexarButton
            '
            Me.reIndexarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.reIndexarButton.BackColor = System.Drawing.SystemColors.Control
            Me.reIndexarButton.FlatAppearance.BorderColor = System.Drawing.Color.Black
            Me.reIndexarButton.FlatAppearance.BorderSize = 0
            Me.reIndexarButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.reIndexarButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.reIndexarButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.reIndexarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Reindexar2
            Me.reIndexarButton.Location = New System.Drawing.Point(792, 195)
            Me.reIndexarButton.Name = "reIndexarButton"
            Me.reIndexarButton.Size = New System.Drawing.Size(45, 44)
            Me.reIndexarButton.TabIndex = 13
            Me.ToolTip.SetToolTip(Me.reIndexarButton, "Re Indexar")
            Me.reIndexarButton.UseVisualStyleBackColor = False
            '
            'BuscarButton
            '
            Me.BuscarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(744, 20)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(75, 32)
            Me.BuscarButton.TabIndex = 0
            Me.BuscarButton.Text = "&Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'FiltroGroupBox
            '
            Me.FiltroGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FiltroGroupBox.Controls.Add(Me.ProcesarDemandaButton)
            Me.FiltroGroupBox.Controls.Add(Me.CentroComboBox)
            Me.FiltroGroupBox.Controls.Add(Me.CentroProcesamientoL)
            Me.FiltroGroupBox.Controls.Add(Me.SedeComboBox)
            Me.FiltroGroupBox.Controls.Add(Me.SedeProcesaminetoL)
            Me.FiltroGroupBox.Controls.Add(Me.MotivoComboBox)
            Me.FiltroGroupBox.Controls.Add(Me.MotivoLabel)
            Me.FiltroGroupBox.Controls.Add(Me.BuscarButton)
            Me.FiltroGroupBox.Location = New System.Drawing.Point(12, 12)
            Me.FiltroGroupBox.Name = "FiltroGroupBox"
            Me.FiltroGroupBox.Size = New System.Drawing.Size(825, 127)
            Me.FiltroGroupBox.TabIndex = 6
            Me.FiltroGroupBox.TabStop = False
            Me.FiltroGroupBox.Text = "Filtro de búsqueda"
            '
            'ReprocesosDataGridView
            '
            Me.ReprocesosDataGridView.AllowUserToAddRows = False
            Me.ReprocesosDataGridView.AllowUserToDeleteRows = False
            Me.ReprocesosDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ReprocesosDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.ReprocesosDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ReprocesosDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.ReprocesosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ReprocesosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.R, Me.fk_Expediente, Me.Documento, Me.Motivo, Me.fk_CargueColumn, Me.fk_Cargue_PaqueteColumn, Me.Observaciones, Me.Actualizado, Me.fk_Folder, Me.fk_File})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.ReprocesosDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.ReprocesosDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.ReprocesosDataGridView.Location = New System.Drawing.Point(12, 145)
            Me.ReprocesosDataGridView.MultiSelect = False
            Me.ReprocesosDataGridView.Name = "ReprocesosDataGridView"
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ReprocesosDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ReprocesosDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle4
            Me.ReprocesosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ReprocesosDataGridView.Size = New System.Drawing.Size(774, 324)
            Me.ReprocesosDataGridView.TabIndex = 11
            '
            'R
            '
            Me.R.HeaderText = "R"
            Me.R.Name = "R"
            Me.R.Width = 21
            '
            'fk_Expediente
            '
            Me.fk_Expediente.DataPropertyName = "fk_Expediente"
            Me.fk_Expediente.HeaderText = "Expediente"
            Me.fk_Expediente.Name = "fk_Expediente"
            Me.fk_Expediente.ReadOnly = True
            Me.fk_Expediente.Width = 95
            '
            'Documento
            '
            Me.Documento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Documento.DataPropertyName = "Nombre_Documento"
            Me.Documento.HeaderText = "Documento"
            Me.Documento.Name = "Documento"
            Me.Documento.ReadOnly = True
            '
            'Motivo
            '
            Me.Motivo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Motivo.DataPropertyName = "Nombre_Reproceso_Motivo"
            Me.Motivo.HeaderText = "Motivo"
            Me.Motivo.Name = "Motivo"
            Me.Motivo.ReadOnly = True
            Me.Motivo.Width = 170
            '
            'fk_CargueColumn
            '
            Me.fk_CargueColumn.DataPropertyName = "fk_Cargue"
            Me.fk_CargueColumn.HeaderText = "Cargue"
            Me.fk_CargueColumn.Name = "fk_CargueColumn"
            Me.fk_CargueColumn.Width = 72
            '
            'fk_Cargue_PaqueteColumn
            '
            Me.fk_Cargue_PaqueteColumn.DataPropertyName = "fk_Cargue_Paquete"
            Me.fk_Cargue_PaqueteColumn.HeaderText = "Paquete"
            Me.fk_Cargue_PaqueteColumn.Name = "fk_Cargue_PaqueteColumn"
            Me.fk_Cargue_PaqueteColumn.Width = 79
            '
            'Observaciones
            '
            Me.Observaciones.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Observaciones.DataPropertyName = "Observaciones"
            Me.Observaciones.HeaderText = "Observaciones"
            Me.Observaciones.Name = "Observaciones"
            Me.Observaciones.ReadOnly = True
            '
            'Actualizado
            '
            Me.Actualizado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.Actualizado.DataPropertyName = "Actualizado"
            Me.Actualizado.HeaderText = "Act."
            Me.Actualizado.Name = "Actualizado"
            Me.Actualizado.ReadOnly = True
            Me.Actualizado.Width = 35
            '
            'fk_Folder
            '
            Me.fk_Folder.DataPropertyName = "fk_Folder"
            Me.fk_Folder.HeaderText = "Folder"
            Me.fk_Folder.Name = "fk_Folder"
            Me.fk_Folder.Visible = False
            Me.fk_Folder.Width = 67
            '
            'fk_File
            '
            Me.fk_File.DataPropertyName = "fk_File"
            Me.fk_File.HeaderText = "File"
            Me.fk_File.Name = "fk_File"
            Me.fk_File.Visible = False
            Me.fk_File.Width = 51
            '
            'FormReprocesos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(849, 482)
            Me.Controls.Add(Me.reIndexarButton)
            Me.Controls.Add(Me.EliminarButton)
            Me.Controls.Add(Me.ReemplazarImagenButton)
            Me.Controls.Add(Me.FiltroGroupBox)
            Me.Controls.Add(Me.ReprocesosDataGridView)
            Me.Controls.Add(Me.ProcesarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.EliminaActualizacionButton)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormReprocesos"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Reprocesos"
            Me.FiltroGroupBox.ResumeLayout(False)
            Me.FiltroGroupBox.PerformLayout()
            CType(Me.ReprocesosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents EliminarButton As System.Windows.Forms.Button
        Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents CentroComboBox As DesktopComboBoxControl
        Friend WithEvents CentroProcesamientoL As System.Windows.Forms.Label
        Friend WithEvents SedeComboBox As DesktopComboBoxControl
        Friend WithEvents SedeProcesaminetoL As System.Windows.Forms.Label
        Friend WithEvents MotivoComboBox As DesktopComboBoxControl
        Friend WithEvents ReemplazarImagenButton As System.Windows.Forms.Button
        Friend WithEvents MotivoLabel As System.Windows.Forms.Label
        Friend WithEvents ProcesarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents EliminaActualizacionButton As System.Windows.Forms.Button
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents FiltroGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ReprocesosDataGridView As DesktopDataGridViewControl
        Friend WithEvents ProcesarDemandaButton As System.Windows.Forms.Button
        Friend WithEvents reIndexarButton As System.Windows.Forms.Button
        Friend WithEvents R As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents fk_Expediente As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Documento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Motivo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_CargueColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Cargue_PaqueteColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Observaciones As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Actualizado As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents fk_Folder As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_File As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace