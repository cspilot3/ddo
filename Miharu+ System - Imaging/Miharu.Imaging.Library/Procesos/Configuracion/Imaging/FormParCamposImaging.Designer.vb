Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Procesos.Configuracion.Imaging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParCamposImaging
        Inherits Miharu.Desktop.Library.FormBase

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
            Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormParCamposImaging))
            Me.CamposDataGridView = New System.Windows.Forms.DataGridView()
            Me.FkEntidadDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FkDocumentoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EsLlaveDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.EsCampoFolderDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.EsObligatorioCampoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.FkCampoTipoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EsCampoIndexacionDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.UsaCapturaDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.UsaCapturaCorreccionMaquinaDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.UsaDobleCapturaDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Usa_Captura_Proceso_Adicional = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.UsaMarcaDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.AsignarMarcaColumn = New System.Windows.Forms.DataGridViewImageColumn()
            Me.MarcaXCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.MarcaYCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.MarcaWidthCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.MarcaHeightCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Usa_Trigger = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.AsignarCampoTrigger = New System.Windows.Forms.DataGridViewImageColumn()
            Me.Es_Campo_Cargue = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.ColumnaCargueCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ExpresionRegularCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.OrdenCampoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.MascaraDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FormatoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EliminadoCampoDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.Tabla_Min_Registros = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Tabla_Max_Registros = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CTACampoConfiguracionDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.EsquemaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.id_DocumentoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CTACampoConfiguracionDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'CamposDataGridView
            '
            Me.CamposDataGridView.AllowUserToAddRows = False
            Me.CamposDataGridView.AllowUserToDeleteRows = False
            Me.CamposDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CamposDataGridView.AutoGenerateColumns = False
            Me.CamposDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CamposDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FkEntidadDataGridViewTextBoxColumn, Me.FkDocumentoDataGridViewTextBoxColumn, Me.IdCampoDataGridViewTextBoxColumn, Me.NombreCampoDataGridViewTextBoxColumn, Me.EsLlaveDataGridViewCheckBoxColumn, Me.EsCampoFolderDataGridViewCheckBoxColumn, Me.EsObligatorioCampoDataGridViewCheckBoxColumn, Me.FkCampoTipoDataGridViewTextBoxColumn, Me.EsCampoIndexacionDataGridViewCheckBoxColumn, Me.UsaCapturaDataGridViewCheckBoxColumn, Me.UsaCapturaCorreccionMaquinaDataGridViewCheckBoxColumn, Me.UsaDobleCapturaDataGridViewCheckBoxColumn, Me.Usa_Captura_Proceso_Adicional, Me.UsaMarcaDataGridViewCheckBoxColumn, Me.AsignarMarcaColumn, Me.MarcaXCampoDataGridViewTextBoxColumn, Me.MarcaYCampoDataGridViewTextBoxColumn, Me.MarcaWidthCampoDataGridViewTextBoxColumn, Me.MarcaHeightCampoDataGridViewTextBoxColumn, Me.Usa_Trigger, Me.AsignarCampoTrigger, Me.Es_Campo_Cargue, Me.ColumnaCargueCampoDataGridViewTextBoxColumn, Me.ExpresionRegularCampoDataGridViewTextBoxColumn, Me.OrdenCampoDataGridViewTextBoxColumn, Me.MascaraDataGridViewTextBoxColumn, Me.FormatoDataGridViewTextBoxColumn, Me.EliminadoCampoDataGridViewCheckBoxColumn, Me.Tabla_Min_Registros, Me.Tabla_Max_Registros})
            Me.CamposDataGridView.DataSource = Me.CTACampoConfiguracionDataTableBindingSource
            Me.CamposDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            Me.CamposDataGridView.Location = New System.Drawing.Point(12, 107)
            Me.CamposDataGridView.Name = "CamposDataGridView"
            Me.CamposDataGridView.RowHeadersWidth = 20
            Me.CamposDataGridView.Size = New System.Drawing.Size(921, 290)
            Me.CamposDataGridView.TabIndex = 30
            '
            'FkEntidadDataGridViewTextBoxColumn
            '
            Me.FkEntidadDataGridViewTextBoxColumn.DataPropertyName = "fk_Entidad"
            DataGridViewCellStyle1.NullValue = "0"
            Me.FkEntidadDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle1
            Me.FkEntidadDataGridViewTextBoxColumn.HeaderText = "fk_Entidad"
            Me.FkEntidadDataGridViewTextBoxColumn.Name = "FkEntidadDataGridViewTextBoxColumn"
            Me.FkEntidadDataGridViewTextBoxColumn.Visible = False
            '
            'FkDocumentoDataGridViewTextBoxColumn
            '
            Me.FkDocumentoDataGridViewTextBoxColumn.DataPropertyName = "fk_Documento"
            Me.FkDocumentoDataGridViewTextBoxColumn.HeaderText = "fk_Documento"
            Me.FkDocumentoDataGridViewTextBoxColumn.Name = "FkDocumentoDataGridViewTextBoxColumn"
            Me.FkDocumentoDataGridViewTextBoxColumn.Visible = False
            '
            'IdCampoDataGridViewTextBoxColumn
            '
            Me.IdCampoDataGridViewTextBoxColumn.DataPropertyName = "id_Campo"
            DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.IdCampoDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
            Me.IdCampoDataGridViewTextBoxColumn.HeaderText = "ID"
            Me.IdCampoDataGridViewTextBoxColumn.Name = "IdCampoDataGridViewTextBoxColumn"
            Me.IdCampoDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdCampoDataGridViewTextBoxColumn.Width = 40
            '
            'NombreCampoDataGridViewTextBoxColumn
            '
            Me.NombreCampoDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Campo"
            DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.NombreCampoDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle3
            Me.NombreCampoDataGridViewTextBoxColumn.HeaderText = "Campo"
            Me.NombreCampoDataGridViewTextBoxColumn.Name = "NombreCampoDataGridViewTextBoxColumn"
            Me.NombreCampoDataGridViewTextBoxColumn.ReadOnly = True
            Me.NombreCampoDataGridViewTextBoxColumn.Width = 150
            '
            'EsLlaveDataGridViewCheckBoxColumn
            '
            Me.EsLlaveDataGridViewCheckBoxColumn.DataPropertyName = "Es_Llave"
            DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            DataGridViewCellStyle4.NullValue = False
            Me.EsLlaveDataGridViewCheckBoxColumn.DefaultCellStyle = DataGridViewCellStyle4
            Me.EsLlaveDataGridViewCheckBoxColumn.HeaderText = "Llave"
            Me.EsLlaveDataGridViewCheckBoxColumn.Name = "EsLlaveDataGridViewCheckBoxColumn"
            Me.EsLlaveDataGridViewCheckBoxColumn.ReadOnly = True
            Me.EsLlaveDataGridViewCheckBoxColumn.Width = 50
            '
            'EsCampoFolderDataGridViewCheckBoxColumn
            '
            Me.EsCampoFolderDataGridViewCheckBoxColumn.DataPropertyName = "Es_Campo_Folder"
            Me.EsCampoFolderDataGridViewCheckBoxColumn.HeaderText = "Es_Campo_Folder"
            Me.EsCampoFolderDataGridViewCheckBoxColumn.Name = "EsCampoFolderDataGridViewCheckBoxColumn"
            Me.EsCampoFolderDataGridViewCheckBoxColumn.Visible = False
            '
            'EsObligatorioCampoDataGridViewCheckBoxColumn
            '
            Me.EsObligatorioCampoDataGridViewCheckBoxColumn.DataPropertyName = "Es_Obligatorio_Campo"
            Me.EsObligatorioCampoDataGridViewCheckBoxColumn.HeaderText = "Obligatorio"
            Me.EsObligatorioCampoDataGridViewCheckBoxColumn.Name = "EsObligatorioCampoDataGridViewCheckBoxColumn"
            Me.EsObligatorioCampoDataGridViewCheckBoxColumn.Width = 80
            '
            'FkCampoTipoDataGridViewTextBoxColumn
            '
            Me.FkCampoTipoDataGridViewTextBoxColumn.DataPropertyName = "fk_Campo_Tipo"
            Me.FkCampoTipoDataGridViewTextBoxColumn.HeaderText = "fk_Campo_Tipo"
            Me.FkCampoTipoDataGridViewTextBoxColumn.Name = "FkCampoTipoDataGridViewTextBoxColumn"
            Me.FkCampoTipoDataGridViewTextBoxColumn.Visible = False
            '
            'EsCampoIndexacionDataGridViewCheckBoxColumn
            '
            Me.EsCampoIndexacionDataGridViewCheckBoxColumn.DataPropertyName = "Es_Campo_Indexacion"
            Me.EsCampoIndexacionDataGridViewCheckBoxColumn.HeaderText = "Precaptura"
            Me.EsCampoIndexacionDataGridViewCheckBoxColumn.Name = "EsCampoIndexacionDataGridViewCheckBoxColumn"
            Me.EsCampoIndexacionDataGridViewCheckBoxColumn.Width = 85
            '
            'UsaCapturaDataGridViewCheckBoxColumn
            '
            Me.UsaCapturaDataGridViewCheckBoxColumn.DataPropertyName = "Usa_Captura"
            Me.UsaCapturaDataGridViewCheckBoxColumn.HeaderText = "Captura"
            Me.UsaCapturaDataGridViewCheckBoxColumn.Name = "UsaCapturaDataGridViewCheckBoxColumn"
            Me.UsaCapturaDataGridViewCheckBoxColumn.Width = 60
            '
            'UsaCapturaCorreccionMaquinaDataGridViewCheckBoxColumn
            '
            Me.UsaCapturaCorreccionMaquinaDataGridViewCheckBoxColumn.DataPropertyName = "Usa_Captura_Correccion_Maquina"
            Me.UsaCapturaCorreccionMaquinaDataGridViewCheckBoxColumn.HeaderText = "Cap correccion maquina"
            Me.UsaCapturaCorreccionMaquinaDataGridViewCheckBoxColumn.Name = "UsaCapturaCorreccionMaquinaDataGridViewCheckBoxColumn"
            '
            'UsaDobleCapturaDataGridViewCheckBoxColumn
            '
            Me.UsaDobleCapturaDataGridViewCheckBoxColumn.DataPropertyName = "Usa_Doble_Captura"
            Me.UsaDobleCapturaDataGridViewCheckBoxColumn.HeaderText = "Doble cap."
            Me.UsaDobleCapturaDataGridViewCheckBoxColumn.Name = "UsaDobleCapturaDataGridViewCheckBoxColumn"
            Me.UsaDobleCapturaDataGridViewCheckBoxColumn.Width = 90
            '
            'Usa_Captura_Proceso_Adicional
            '
            Me.Usa_Captura_Proceso_Adicional.DataPropertyName = "Usa_Captura_Proceso_Adicional"
            Me.Usa_Captura_Proceso_Adicional.HeaderText = "Adicional Cap."
            Me.Usa_Captura_Proceso_Adicional.Name = "Usa_Captura_Proceso_Adicional"
            '
            'UsaMarcaDataGridViewCheckBoxColumn
            '
            Me.UsaMarcaDataGridViewCheckBoxColumn.DataPropertyName = "Usa_Marca"
            Me.UsaMarcaDataGridViewCheckBoxColumn.HeaderText = "Marca"
            Me.UsaMarcaDataGridViewCheckBoxColumn.Name = "UsaMarcaDataGridViewCheckBoxColumn"
            Me.UsaMarcaDataGridViewCheckBoxColumn.Width = 50
            '
            'AsignarMarcaColumn
            '
            Me.AsignarMarcaColumn.HeaderText = "AsignarMarcaColumn"
            Me.AsignarMarcaColumn.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGenerarEstructura
            Me.AsignarMarcaColumn.Name = "AsignarMarcaColumn"
            Me.AsignarMarcaColumn.Width = 30
            '
            'MarcaXCampoDataGridViewTextBoxColumn
            '
            Me.MarcaXCampoDataGridViewTextBoxColumn.DataPropertyName = "Marca_X_Campo"
            DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.MarcaXCampoDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle5
            Me.MarcaXCampoDataGridViewTextBoxColumn.HeaderText = "X"
            Me.MarcaXCampoDataGridViewTextBoxColumn.Name = "MarcaXCampoDataGridViewTextBoxColumn"
            Me.MarcaXCampoDataGridViewTextBoxColumn.ReadOnly = True
            Me.MarcaXCampoDataGridViewTextBoxColumn.Width = 60
            '
            'MarcaYCampoDataGridViewTextBoxColumn
            '
            Me.MarcaYCampoDataGridViewTextBoxColumn.DataPropertyName = "Marca_Y_Campo"
            DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.MarcaYCampoDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle6
            Me.MarcaYCampoDataGridViewTextBoxColumn.HeaderText = "Y"
            Me.MarcaYCampoDataGridViewTextBoxColumn.Name = "MarcaYCampoDataGridViewTextBoxColumn"
            Me.MarcaYCampoDataGridViewTextBoxColumn.ReadOnly = True
            Me.MarcaYCampoDataGridViewTextBoxColumn.Width = 60
            '
            'MarcaWidthCampoDataGridViewTextBoxColumn
            '
            Me.MarcaWidthCampoDataGridViewTextBoxColumn.DataPropertyName = "Marca_Width_Campo"
            DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.MarcaWidthCampoDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle7
            Me.MarcaWidthCampoDataGridViewTextBoxColumn.HeaderText = "Ancho"
            Me.MarcaWidthCampoDataGridViewTextBoxColumn.Name = "MarcaWidthCampoDataGridViewTextBoxColumn"
            Me.MarcaWidthCampoDataGridViewTextBoxColumn.ReadOnly = True
            Me.MarcaWidthCampoDataGridViewTextBoxColumn.Width = 60
            '
            'MarcaHeightCampoDataGridViewTextBoxColumn
            '
            Me.MarcaHeightCampoDataGridViewTextBoxColumn.DataPropertyName = "Marca_Height_Campo"
            DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.MarcaHeightCampoDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle8
            Me.MarcaHeightCampoDataGridViewTextBoxColumn.HeaderText = "Alto"
            Me.MarcaHeightCampoDataGridViewTextBoxColumn.Name = "MarcaHeightCampoDataGridViewTextBoxColumn"
            Me.MarcaHeightCampoDataGridViewTextBoxColumn.ReadOnly = True
            Me.MarcaHeightCampoDataGridViewTextBoxColumn.Width = 60
            '
            'Usa_Trigger
            '
            Me.Usa_Trigger.DataPropertyName = "Usa_Trigger"
            Me.Usa_Trigger.HeaderText = "Usa Trigger"
            Me.Usa_Trigger.Name = "Usa_Trigger"
            '
            'AsignarCampoTrigger
            '
            Me.AsignarCampoTrigger.HeaderText = "Trigger"
            Me.AsignarCampoTrigger.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cog_go
            Me.AsignarCampoTrigger.Name = "AsignarCampoTrigger"
            Me.AsignarCampoTrigger.Width = 40
            '
            'Es_Campo_Cargue
            '
            Me.Es_Campo_Cargue.DataPropertyName = "Es_Campo_Cargue"
            Me.Es_Campo_Cargue.HeaderText = "Cargue"
            Me.Es_Campo_Cargue.Name = "Es_Campo_Cargue"
            Me.Es_Campo_Cargue.Width = 50
            '
            'ColumnaCargueCampoDataGridViewTextBoxColumn
            '
            Me.ColumnaCargueCampoDataGridViewTextBoxColumn.DataPropertyName = "Columna_Cargue_Campo"
            Me.ColumnaCargueCampoDataGridViewTextBoxColumn.HeaderText = "Col Cargue"
            Me.ColumnaCargueCampoDataGridViewTextBoxColumn.Name = "ColumnaCargueCampoDataGridViewTextBoxColumn"
            '
            'ExpresionRegularCampoDataGridViewTextBoxColumn
            '
            Me.ExpresionRegularCampoDataGridViewTextBoxColumn.DataPropertyName = "Expresion_Regular_Campo"
            Me.ExpresionRegularCampoDataGridViewTextBoxColumn.HeaderText = "Expresion_Regular_Campo"
            Me.ExpresionRegularCampoDataGridViewTextBoxColumn.Name = "ExpresionRegularCampoDataGridViewTextBoxColumn"
            Me.ExpresionRegularCampoDataGridViewTextBoxColumn.Visible = False
            '
            'OrdenCampoDataGridViewTextBoxColumn
            '
            Me.OrdenCampoDataGridViewTextBoxColumn.DataPropertyName = "Orden_Campo"
            Me.OrdenCampoDataGridViewTextBoxColumn.HeaderText = "Orden"
            Me.OrdenCampoDataGridViewTextBoxColumn.Name = "OrdenCampoDataGridViewTextBoxColumn"
            Me.OrdenCampoDataGridViewTextBoxColumn.Width = 60
            '
            'MascaraDataGridViewTextBoxColumn
            '
            Me.MascaraDataGridViewTextBoxColumn.DataPropertyName = "Mascara"
            Me.MascaraDataGridViewTextBoxColumn.HeaderText = "Mascara"
            Me.MascaraDataGridViewTextBoxColumn.Name = "MascaraDataGridViewTextBoxColumn"
            '
            'FormatoDataGridViewTextBoxColumn
            '
            Me.FormatoDataGridViewTextBoxColumn.DataPropertyName = "Formato"
            Me.FormatoDataGridViewTextBoxColumn.HeaderText = "Formato"
            Me.FormatoDataGridViewTextBoxColumn.Name = "FormatoDataGridViewTextBoxColumn"
            '
            'EliminadoCampoDataGridViewCheckBoxColumn
            '
            Me.EliminadoCampoDataGridViewCheckBoxColumn.DataPropertyName = "Eliminado_Campo"
            Me.EliminadoCampoDataGridViewCheckBoxColumn.HeaderText = "Eliminado_Campo"
            Me.EliminadoCampoDataGridViewCheckBoxColumn.Name = "EliminadoCampoDataGridViewCheckBoxColumn"
            Me.EliminadoCampoDataGridViewCheckBoxColumn.Visible = False
            '
            'Tabla_Min_Registros
            '
            Me.Tabla_Min_Registros.DataPropertyName = "Tabla_Min_Registros"
            Me.Tabla_Min_Registros.HeaderText = "Mínimo Registros"
            Me.Tabla_Min_Registros.Name = "Tabla_Min_Registros"
            '
            'Tabla_Max_Registros
            '
            Me.Tabla_Max_Registros.DataPropertyName = "Tabla_Max_Registros"
            Me.Tabla_Max_Registros.HeaderText = "Máximo Registros"
            Me.Tabla_Max_Registros.Name = "Tabla_Max_Registros"
            '
            'CTACampoConfiguracionDataTableBindingSource
            '
            Me.CTACampoConfiguracionDataTableBindingSource.DataSource = GetType(DBImaging.SchemaConfig.CTA_Campo_ParametrizacionDataTable)
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label7)
            Me.GroupBox1.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.id_DocumentoDesktopComboBox)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(448, 80)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Campo"
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(12, 21)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(58, 13)
            Me.Label7.TabIndex = 22
            Me.Label7.Text = "Esquema"
            '
            'EsquemaDesktopComboBox
            '
            Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaDesktopComboBox.DisabledEnter = False
            Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaDesktopComboBox.fk_Campo = 0
            Me.EsquemaDesktopComboBox.fk_Documento = 0
            Me.EsquemaDesktopComboBox.fk_Validacion = 0
            Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaDesktopComboBox.FormattingEnabled = True
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(90, 18)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(338, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 21
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(13, 53)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(72, 13)
            Me.Label1.TabIndex = 12
            Me.Label1.Text = "Documento"
            '
            'id_DocumentoDesktopComboBox
            '
            Me.id_DocumentoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.id_DocumentoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.id_DocumentoDesktopComboBox.DisabledEnter = False
            Me.id_DocumentoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.id_DocumentoDesktopComboBox.fk_Campo = 0
            Me.id_DocumentoDesktopComboBox.fk_Documento = 0
            Me.id_DocumentoDesktopComboBox.fk_Validacion = 0
            Me.id_DocumentoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.id_DocumentoDesktopComboBox.FormattingEnabled = True
            Me.id_DocumentoDesktopComboBox.Location = New System.Drawing.Point(91, 50)
            Me.id_DocumentoDesktopComboBox.Name = "id_DocumentoDesktopComboBox"
            Me.id_DocumentoDesktopComboBox.Size = New System.Drawing.Size(337, 21)
            Me.id_DocumentoDesktopComboBox.TabIndex = 0
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(746, 403)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(90, 30)
            Me.GuardarButton.TabIndex = 9
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
            Me.CerrarButton.Location = New System.Drawing.Point(842, 403)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 10
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormParCamposImaging
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(944, 445)
            Me.Controls.Add(Me.CamposDataGridView)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormParCamposImaging"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Configuracion Campos Imaging"
            CType(Me.CamposDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CTACampoConfiguracionDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents id_DocumentoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents EsquemaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents CamposDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents CTACampoConfiguracionDataTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents FkEntidadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FkDocumentoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EsLlaveDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents EsCampoFolderDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents EsObligatorioCampoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents FkCampoTipoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EsCampoIndexacionDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents UsaCapturaDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents UsaCapturaCorreccionMaquinaDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents UsaDobleCapturaDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Usa_Captura_Proceso_Adicional As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents UsaMarcaDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents AsignarMarcaColumn As System.Windows.Forms.DataGridViewImageColumn
        Friend WithEvents MarcaXCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents MarcaYCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents MarcaWidthCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents MarcaHeightCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Usa_Trigger As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents AsignarCampoTrigger As System.Windows.Forms.DataGridViewImageColumn
        Friend WithEvents Es_Campo_Cargue As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents ColumnaCargueCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ExpresionRegularCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents OrdenCampoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents MascaraDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FormatoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EliminadoCampoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents Tabla_Min_Registros As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Tabla_Max_Registros As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace