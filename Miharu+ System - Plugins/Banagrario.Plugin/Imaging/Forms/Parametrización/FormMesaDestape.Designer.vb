Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Imaging.Forms.Parametrización

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormMesaDestape
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
            Me.components = New System.ComponentModel.Container()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMesaDestape))
            Me.lblNovedad = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.MesaDestapeDataGridView = New System.Windows.Forms.DataGridView()
            Me.AgregarMesaDestapeButton = New System.Windows.Forms.Button()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.SedeDesktopComboBox = New DesktopComboBoxControl()
            Me.CentroProcesamientoDesktopComboBox = New DesktopComboBoxControl()
            Me.PAGetMesasDestapeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.DSMesasDestape = New Banagrario.Plugin.DSMesasDestape()
            Me.PA_Get_MesasDestapeTableAdapter = New Banagrario.Plugin.DSMesasDestapeTableAdapters.PA_Get_MesasDestapeTableAdapter()
            Me.IdMesaDestapeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.PCNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ActivaDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.fk_Entidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Sede = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Centro_Procesamiento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            CType(Me.MesaDestapeDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PAGetMesasDestapeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DSMesasDestape, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lblNovedad
            '
            Me.lblNovedad.AutoSize = True
            Me.lblNovedad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblNovedad.Location = New System.Drawing.Point(12, 19)
            Me.lblNovedad.Name = "lblNovedad"
            Me.lblNovedad.Size = New System.Drawing.Size(36, 13)
            Me.lblNovedad.TabIndex = 2
            Me.lblNovedad.Text = "Sede"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(12, 53)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(78, 13)
            Me.Label1.TabIndex = 4
            Me.Label1.Text = "Centro Proc."
            '
            'MesaDestapeDataGridView
            '
            Me.MesaDestapeDataGridView.AllowUserToAddRows = False
            Me.MesaDestapeDataGridView.AllowUserToDeleteRows = False
            Me.MesaDestapeDataGridView.AutoGenerateColumns = False
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.MesaDestapeDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
            Me.MesaDestapeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.MesaDestapeDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdMesaDestapeDataGridViewTextBoxColumn, Me.PCNameDataGridViewTextBoxColumn, Me.ActivaDataGridViewCheckBoxColumn, Me.fk_Entidad, Me.fk_Sede, Me.fk_Centro_Procesamiento})
            Me.MesaDestapeDataGridView.DataSource = Me.PAGetMesasDestapeBindingSource
            Me.MesaDestapeDataGridView.Location = New System.Drawing.Point(15, 90)
            Me.MesaDestapeDataGridView.MultiSelect = False
            Me.MesaDestapeDataGridView.Name = "MesaDestapeDataGridView"
            Me.MesaDestapeDataGridView.ReadOnly = True
            Me.MesaDestapeDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.MesaDestapeDataGridView.Size = New System.Drawing.Size(415, 150)
            Me.MesaDestapeDataGridView.TabIndex = 6
            '
            'AgregarMesaDestapeButton
            '
            Me.AgregarMesaDestapeButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.AgregarMesaDestapeButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AgregarMesaDestapeButton.Image = CType(resources.GetObject("AgregarMesaDestapeButton.Image"), System.Drawing.Image)
            Me.AgregarMesaDestapeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AgregarMesaDestapeButton.Location = New System.Drawing.Point(15, 251)
            Me.AgregarMesaDestapeButton.Name = "AgregarMesaDestapeButton"
            Me.AgregarMesaDestapeButton.Size = New System.Drawing.Size(168, 33)
            Me.AgregarMesaDestapeButton.TabIndex = 35
            Me.AgregarMesaDestapeButton.Text = "Agregar Mesa Destape"
            Me.AgregarMesaDestapeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AgregarMesaDestapeButton.UseVisualStyleBackColor = True
            '
            'BuscarButton
            '
            Me.BuscarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BuscarButton.Image = CType(resources.GetObject("BuscarButton.Image"), System.Drawing.Image)
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(338, 19)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(89, 31)
            Me.BuscarButton.TabIndex = 36
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(338, 251)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 37
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'SedeDesktopComboBox
            '
            Me.SedeDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SedeDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SedeDesktopComboBox.DisabledEnter = False
            Me.SedeDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SedeDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SedeDesktopComboBox.FormattingEnabled = True
            Me.SedeDesktopComboBox.Location = New System.Drawing.Point(93, 11)
            Me.SedeDesktopComboBox.Name = "SedeDesktopComboBox"
            Me.SedeDesktopComboBox.Size = New System.Drawing.Size(207, 21)
            Me.SedeDesktopComboBox.TabIndex = 38
            '
            'CentroProcesamientoDesktopComboBox
            '
            Me.CentroProcesamientoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.CentroProcesamientoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.CentroProcesamientoDesktopComboBox.DisabledEnter = False
            Me.CentroProcesamientoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CentroProcesamientoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CentroProcesamientoDesktopComboBox.FormattingEnabled = True
            Me.CentroProcesamientoDesktopComboBox.Location = New System.Drawing.Point(93, 45)
            Me.CentroProcesamientoDesktopComboBox.Name = "CentroProcesamientoDesktopComboBox"
            Me.CentroProcesamientoDesktopComboBox.Size = New System.Drawing.Size(207, 21)
            Me.CentroProcesamientoDesktopComboBox.TabIndex = 39
            '
            'PAGetMesasDestapeBindingSource
            '
            Me.PAGetMesasDestapeBindingSource.DataMember = "PA_Get_MesasDestape"
            Me.PAGetMesasDestapeBindingSource.DataSource = Me.DSMesasDestape
            '
            'DSMesasDestape
            '
            Me.DSMesasDestape.DataSetName = "DSMesasDestape"
            Me.DSMesasDestape.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'PA_Get_MesasDestapeTableAdapter
            '
            Me.PA_Get_MesasDestapeTableAdapter.ClearBeforeFill = True
            '
            'IdMesaDestapeDataGridViewTextBoxColumn
            '
            Me.IdMesaDestapeDataGridViewTextBoxColumn.DataPropertyName = "id_Mesa_Destape"
            Me.IdMesaDestapeDataGridViewTextBoxColumn.HeaderText = "Id"
            Me.IdMesaDestapeDataGridViewTextBoxColumn.Name = "IdMesaDestapeDataGridViewTextBoxColumn"
            Me.IdMesaDestapeDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdMesaDestapeDataGridViewTextBoxColumn.Width = 70
            '
            'PCNameDataGridViewTextBoxColumn
            '
            Me.PCNameDataGridViewTextBoxColumn.DataPropertyName = "PC_Name"
            Me.PCNameDataGridViewTextBoxColumn.HeaderText = "Máquina"
            Me.PCNameDataGridViewTextBoxColumn.Name = "PCNameDataGridViewTextBoxColumn"
            Me.PCNameDataGridViewTextBoxColumn.ReadOnly = True
            Me.PCNameDataGridViewTextBoxColumn.Width = 200
            '
            'ActivaDataGridViewCheckBoxColumn
            '
            Me.ActivaDataGridViewCheckBoxColumn.DataPropertyName = "Activa"
            Me.ActivaDataGridViewCheckBoxColumn.HeaderText = "Activa"
            Me.ActivaDataGridViewCheckBoxColumn.Name = "ActivaDataGridViewCheckBoxColumn"
            Me.ActivaDataGridViewCheckBoxColumn.ReadOnly = True
            '
            'fk_Entidad
            '
            Me.fk_Entidad.DataPropertyName = "fk_Entidad"
            Me.fk_Entidad.HeaderText = "fk_Entidad"
            Me.fk_Entidad.Name = "fk_Entidad"
            Me.fk_Entidad.ReadOnly = True
            Me.fk_Entidad.Visible = False
            '
            'fk_Sede
            '
            Me.fk_Sede.DataPropertyName = "fk_Sede"
            Me.fk_Sede.HeaderText = "fk_Sede"
            Me.fk_Sede.Name = "fk_Sede"
            Me.fk_Sede.ReadOnly = True
            Me.fk_Sede.Visible = False
            '
            'fk_Centro_Procesamiento
            '
            Me.fk_Centro_Procesamiento.DataPropertyName = "fk_Centro_Procesamiento"
            Me.fk_Centro_Procesamiento.HeaderText = "fk_Centro_Procesamiento"
            Me.fk_Centro_Procesamiento.Name = "fk_Centro_Procesamiento"
            Me.fk_Centro_Procesamiento.ReadOnly = True
            Me.fk_Centro_Procesamiento.Visible = False
            '
            'FormMesaDestape
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(448, 296)
            Me.Controls.Add(Me.CentroProcesamientoDesktopComboBox)
            Me.Controls.Add(Me.SedeDesktopComboBox)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.BuscarButton)
            Me.Controls.Add(Me.AgregarMesaDestapeButton)
            Me.Controls.Add(Me.MesaDestapeDataGridView)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.lblNovedad)
            Me.Name = "FormMesaDestape"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Mesa Destape"
            CType(Me.MesaDestapeDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PAGetMesasDestapeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DSMesasDestape, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents lblNovedad As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents MesaDestapeDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents AgregarMesaDestapeButton As System.Windows.Forms.Button
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents DSMesasDestape As Banagrario.Plugin.DSMesasDestape
        Friend WithEvents PAGetMesasDestapeBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents PA_Get_MesasDestapeTableAdapter As Banagrario.Plugin.DSMesasDestapeTableAdapters.PA_Get_MesasDestapeTableAdapter
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents SedeDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents CentroProcesamientoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents IdMesaDestapeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents PCNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ActivaDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents fk_Entidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Sede As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Centro_Procesamiento As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace