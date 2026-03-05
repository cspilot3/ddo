Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.Utils

Namespace Risk.Forms.Empaque

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEmpaque
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEmpaque))
            Dim Rango1 As Rango = New Rango()
            Dim Rango2 As Rango = New Rango()
            Dim Rango3 As Rango = New Rango()
            Me.PuntoDestinoComboBox = New DesktopComboBoxControl()
            Me.Cod_Contenedor_NuevoLabel = New System.Windows.Forms.Label()
            Me.ContenedorGroupBox = New System.Windows.Forms.GroupBox()
            Me.PuntoOkControl = New OkControl()
            Me.PrecintoOkControl = New OkControl()
            Me.PrecintoCajaTextBox = New DesktopTextBoxControl()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.CantidadDocumentosTextBox = New DesktopTextBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.ContenedorTextBox = New DesktopTextBoxControl()
            Me.ContenedorEncabezadoGroupBox = New System.Windows.Forms.GroupBox()
            Me.AnexoLabel = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.TipoContenedorNuevoLabel = New System.Windows.Forms.Label()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.CodigoContenedorNuevoLabel = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.CambioStickerDesktopCheckBox = New DesktopCheckBox.DesktopCheckBoxControl()
            Me.ImprimirButton = New System.Windows.Forms.Button()
            Me.lblContenedor = New System.Windows.Forms.Label()
            Me.OkControlTipoContendor = New OkControl()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.TipoContenedorComboBox = New DesktopComboBoxControl()
            Me.AnexoOkControl = New OkControl()
            Me.lbl_Anexo2 = New System.Windows.Forms.Label()
            Me.lbl_Anexo1 = New System.Windows.Forms.Label()
            Me.Anexo2ComboBox = New DesktopComboBoxControl()
            Me.Anexo1ComboBox = New DesktopComboBoxControl()
            Me.CantidadOkControl = New OkControl()
            Me.ContenedorOkControl = New OkControl()
            Me.ContenedoresGroupBox = New System.Windows.Forms.GroupBox()
            Me.Aux_ContenedoresGrillaPanel = New System.Windows.Forms.Panel()
            Me.ContenedoresDataGridView = New System.Windows.Forms.DataGridView()
            Me.FkCajaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdCajaContenedorDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Codigo_Contenedor_Destape = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Codigo_Contenedor_Empaque = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CantidadDocumentosDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.BanAgrarioData = New DBAgrario.ProcessDataSet()
            Me.Aux_ContenedoresPanel = New System.Windows.Forms.Panel()
            Me.Aux_ContenedoresLabel = New System.Windows.Forms.Label()
            Me.BotonesGroupBox = New System.Windows.Forms.GroupBox()
            Me.BotonesPanel = New System.Windows.Forms.Panel()
            Me.InsertarContenedorNoDestapadoButton = New System.Windows.Forms.Button()
            Me.InsertarContenedorButton = New System.Windows.Forms.Button()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.FinalizarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.ContenedorGroupBox.SuspendLayout()
            Me.ContenedorEncabezadoGroupBox.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.ContenedoresGroupBox.SuspendLayout()
            Me.Aux_ContenedoresGrillaPanel.SuspendLayout()
            CType(Me.ContenedoresDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.BanAgrarioData, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Aux_ContenedoresPanel.SuspendLayout()
            Me.BotonesGroupBox.SuspendLayout()
            Me.BotonesPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'PuntoDestinoComboBox
            '
            Me.PuntoDestinoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.PuntoDestinoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.PuntoDestinoComboBox.DisabledEnter = True
            Me.PuntoDestinoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.PuntoDestinoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.PuntoDestinoComboBox.FormattingEnabled = True
            Me.PuntoDestinoComboBox.Location = New System.Drawing.Point(523, 12)
            Me.PuntoDestinoComboBox.Name = "PuntoDestinoComboBox"
            Me.PuntoDestinoComboBox.Size = New System.Drawing.Size(306, 21)
            Me.PuntoDestinoComboBox.TabIndex = 1
            '
            'Cod_Contenedor_NuevoLabel
            '
            Me.Cod_Contenedor_NuevoLabel.AutoSize = True
            Me.Cod_Contenedor_NuevoLabel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Cod_Contenedor_NuevoLabel.Location = New System.Drawing.Point(455, 14)
            Me.Cod_Contenedor_NuevoLabel.Name = "Cod_Contenedor_NuevoLabel"
            Me.Cod_Contenedor_NuevoLabel.Size = New System.Drawing.Size(45, 14)
            Me.Cod_Contenedor_NuevoLabel.TabIndex = 3
            Me.Cod_Contenedor_NuevoLabel.Text = "Punto"
            '
            'ContenedorGroupBox
            '
            Me.ContenedorGroupBox.Controls.Add(Me.PuntoOkControl)
            Me.ContenedorGroupBox.Controls.Add(Me.PrecintoOkControl)
            Me.ContenedorGroupBox.Controls.Add(Me.PuntoDestinoComboBox)
            Me.ContenedorGroupBox.Controls.Add(Me.Cod_Contenedor_NuevoLabel)
            Me.ContenedorGroupBox.Controls.Add(Me.PrecintoCajaTextBox)
            Me.ContenedorGroupBox.Controls.Add(Me.Label8)
            Me.ContenedorGroupBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.ContenedorGroupBox.Location = New System.Drawing.Point(3, 3)
            Me.ContenedorGroupBox.Name = "ContenedorGroupBox"
            Me.ContenedorGroupBox.Size = New System.Drawing.Size(897, 39)
            Me.ContenedorGroupBox.TabIndex = 0
            Me.ContenedorGroupBox.TabStop = False
            '
            'PuntoOkControl
            '
            Me.PuntoOkControl.BackgroundImage = CType(resources.GetObject("PuntoOkControl.BackgroundImage"), System.Drawing.Image)
            Me.PuntoOkControl.Location = New System.Drawing.Point(844, 16)
            Me.PuntoOkControl.Name = "PuntoOkControl"
            Me.PuntoOkControl.OK = Microsoft.VisualBasic.TriState.[True]
            Me.PuntoOkControl.Size = New System.Drawing.Size(12, 12)
            Me.PuntoOkControl.TabIndex = 38
            Me.PuntoOkControl.TabStop = False
            Me.PuntoOkControl.Text = "OkControl9"
            '
            'PrecintoOkControl
            '
            Me.PrecintoOkControl.BackgroundImage = CType(resources.GetObject("PrecintoOkControl.BackgroundImage"), System.Drawing.Image)
            Me.PrecintoOkControl.Location = New System.Drawing.Point(428, 16)
            Me.PrecintoOkControl.Name = "PrecintoOkControl"
            Me.PrecintoOkControl.OK = Microsoft.VisualBasic.TriState.[True]
            Me.PrecintoOkControl.Size = New System.Drawing.Size(12, 12)
            Me.PrecintoOkControl.TabIndex = 2
            Me.PrecintoOkControl.TabStop = False
            Me.PrecintoOkControl.Text = "OkControl9"
            '
            'PrecintoCajaTextBox
            '
            Me.PrecintoCajaTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrecintoCajaTextBox.Cantidad_Decimales = CType(0, Short)
            Me.PrecintoCajaTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.PrecintoCajaTextBox.DisabledEnter = True
            Me.PrecintoCajaTextBox.DisabledTab = False
            Me.PrecintoCajaTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.PrecintoCajaTextBox.FocusOut = System.Drawing.Color.White
            Me.PrecintoCajaTextBox.Location = New System.Drawing.Point(177, 12)
            Me.PrecintoCajaTextBox.MaximumLength = CType(20, Short)
            Me.PrecintoCajaTextBox.MaxLength = 20
            Me.PrecintoCajaTextBox.MinimumLength = CType(0, Short)
            Me.PrecintoCajaTextBox.Name = "PrecintoCajaTextBox"
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.PrecintoCajaTextBox.Rango = Rango1
            Me.PrecintoCajaTextBox.ShortcutsEnabled = False
            Me.PrecintoCajaTextBox.Size = New System.Drawing.Size(234, 21)
            Me.PrecintoCajaTextBox.TabIndex = 0
            Me.PrecintoCajaTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.PrecintoCajaTextBox.Usa_Decimales = False
            Me.PrecintoCajaTextBox.Validos_Cantidad_Puntos = False
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label8.Location = New System.Drawing.Point(28, 14)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(58, 14)
            Me.Label8.TabIndex = 0
            Me.Label8.Text = "Precinto"
            '
            'CantidadDocumentosTextBox
            '
            Me.CantidadDocumentosTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CantidadDocumentosTextBox.Cantidad_Decimales = CType(0, Short)
            Me.CantidadDocumentosTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.CantidadDocumentosTextBox.DisabledEnter = True
            Me.CantidadDocumentosTextBox.DisabledTab = False
            Me.CantidadDocumentosTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.CantidadDocumentosTextBox.FocusOut = System.Drawing.Color.White
            Me.CantidadDocumentosTextBox.Location = New System.Drawing.Point(176, 127)
            Me.CantidadDocumentosTextBox.MaximumLength = CType(20, Short)
            Me.CantidadDocumentosTextBox.MaxLength = 20
            Me.CantidadDocumentosTextBox.MinimumLength = CType(0, Short)
            Me.CantidadDocumentosTextBox.Name = "CantidadDocumentosTextBox"
            Rango2.MaxValue = 2147483647.0R
            Rango2.MinValue = 0.0R
            Me.CantidadDocumentosTextBox.Rango = Rango2
            Me.CantidadDocumentosTextBox.ShortcutsEnabled = False
            Me.CantidadDocumentosTextBox.Size = New System.Drawing.Size(234, 21)
            Me.CantidadDocumentosTextBox.TabIndex = 5
            Me.CantidadDocumentosTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            Me.CantidadDocumentosTextBox.Usa_Decimales = False
            Me.CantidadDocumentosTextBox.Validos_Cantidad_Puntos = False
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(28, 129)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(142, 14)
            Me.Label2.TabIndex = 3
            Me.Label2.Text = "Cantidad documentos"
            '
            'ContenedorTextBox
            '
            Me.ContenedorTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ContenedorTextBox.Cantidad_Decimales = CType(0, Short)
            Me.ContenedorTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ContenedorTextBox.DisabledEnter = True
            Me.ContenedorTextBox.DisabledTab = False
            Me.ContenedorTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ContenedorTextBox.FocusOut = System.Drawing.Color.White
            Me.ContenedorTextBox.Location = New System.Drawing.Point(177, 19)
            Me.ContenedorTextBox.MaximumLength = CType(20, Short)
            Me.ContenedorTextBox.MaxLength = 20
            Me.ContenedorTextBox.MinimumLength = CType(0, Short)
            Me.ContenedorTextBox.Name = "ContenedorTextBox"
            Rango3.MaxValue = 9.2233720368547758E+18R
            Rango3.MinValue = 0.0R
            Me.ContenedorTextBox.Rango = Rango3
            Me.ContenedorTextBox.ShortcutsEnabled = False
            Me.ContenedorTextBox.Size = New System.Drawing.Size(234, 21)
            Me.ContenedorTextBox.TabIndex = 1
            Me.ContenedorTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            Me.ContenedorTextBox.Usa_Decimales = False
            Me.ContenedorTextBox.Validos_Cantidad_Puntos = False
            '
            'ContenedorEncabezadoGroupBox
            '
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.AnexoLabel)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.GroupBox1)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.ImprimirButton)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.CantidadDocumentosTextBox)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.lblContenedor)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.OkControlTipoContendor)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.Label4)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.TipoContenedorComboBox)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.AnexoOkControl)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.lbl_Anexo2)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.lbl_Anexo1)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.Anexo2ComboBox)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.Anexo1ComboBox)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.CantidadOkControl)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.ContenedorOkControl)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.Label2)
            Me.ContenedorEncabezadoGroupBox.Controls.Add(Me.ContenedorTextBox)
            Me.ContenedorEncabezadoGroupBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.ContenedorEncabezadoGroupBox.Location = New System.Drawing.Point(3, 42)
            Me.ContenedorEncabezadoGroupBox.Name = "ContenedorEncabezadoGroupBox"
            Me.ContenedorEncabezadoGroupBox.Size = New System.Drawing.Size(897, 172)
            Me.ContenedorEncabezadoGroupBox.TabIndex = 1
            Me.ContenedorEncabezadoGroupBox.TabStop = False
            '
            'AnexoLabel
            '
            Me.AnexoLabel.BackColor = System.Drawing.SystemColors.ButtonHighlight
            Me.AnexoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AnexoLabel.Location = New System.Drawing.Point(179, 76)
            Me.AnexoLabel.Name = "AnexoLabel"
            Me.AnexoLabel.Size = New System.Drawing.Size(177, 15)
            Me.AnexoLabel.TabIndex = 21
            Me.AnexoLabel.Text = "*************"
            Me.AnexoLabel.Visible = False
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.TipoContenedorNuevoLabel)
            Me.GroupBox1.Controls.Add(Me.Label7)
            Me.GroupBox1.Controls.Add(Me.CodigoContenedorNuevoLabel)
            Me.GroupBox1.Controls.Add(Me.Label5)
            Me.GroupBox1.Controls.Add(Me.CambioStickerDesktopCheckBox)
            Me.GroupBox1.Location = New System.Drawing.Point(458, 19)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(417, 70)
            Me.GroupBox1.TabIndex = 17
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Nuevo Sticker"
            '
            'TipoContenedorNuevoLabel
            '
            Me.TipoContenedorNuevoLabel.AutoSize = True
            Me.TipoContenedorNuevoLabel.Location = New System.Drawing.Point(293, 45)
            Me.TipoContenedorNuevoLabel.Name = "TipoContenedorNuevoLabel"
            Me.TipoContenedorNuevoLabel.Size = New System.Drawing.Size(0, 13)
            Me.TipoContenedorNuevoLabel.TabIndex = 20
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(156, 45)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(103, 13)
            Me.Label7.TabIndex = 19
            Me.Label7.Text = "Tipo Contenedor:"
            '
            'CodigoContenedorNuevoLabel
            '
            Me.CodigoContenedorNuevoLabel.AutoSize = True
            Me.CodigoContenedorNuevoLabel.Location = New System.Drawing.Point(293, 21)
            Me.CodigoContenedorNuevoLabel.Name = "CodigoContenedorNuevoLabel"
            Me.CodigoContenedorNuevoLabel.Size = New System.Drawing.Size(0, 13)
            Me.CodigoContenedorNuevoLabel.TabIndex = 18
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(156, 21)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(117, 13)
            Me.Label5.TabIndex = 17
            Me.Label5.Text = "Código Contenedor:"
            '
            'CambioStickerDesktopCheckBox
            '
            Me.CambioStickerDesktopCheckBox.AutoSize = True
            Me.CambioStickerDesktopCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CambioStickerDesktopCheckBox.DisabledEnter = False
            Me.CambioStickerDesktopCheckBox.FocusIn = System.Drawing.Color.LightYellow
            Me.CambioStickerDesktopCheckBox.FocusOut = System.Drawing.Color.Gainsboro
            Me.CambioStickerDesktopCheckBox.Location = New System.Drawing.Point(21, 27)
            Me.CambioStickerDesktopCheckBox.Name = "CambioStickerDesktopCheckBox"
            Me.CambioStickerDesktopCheckBox.Size = New System.Drawing.Size(97, 17)
            Me.CambioStickerDesktopCheckBox.TabIndex = 16
            Me.CambioStickerDesktopCheckBox.Text = "Cambio Sticker"
            Me.CambioStickerDesktopCheckBox.UseVisualStyleBackColor = True
            '
            'ImprimirButton
            '
            Me.ImprimirButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ImprimirButton.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
            Me.ImprimirButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Print1
            Me.ImprimirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ImprimirButton.Location = New System.Drawing.Point(760, 122)
            Me.ImprimirButton.Name = "ImprimirButton"
            Me.ImprimirButton.Size = New System.Drawing.Size(115, 29)
            Me.ImprimirButton.TabIndex = 10
            Me.ImprimirButton.Text = "&Imprimir"
            Me.ImprimirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ImprimirButton.UseVisualStyleBackColor = True
            '
            'lblContenedor
            '
            Me.lblContenedor.AutoSize = True
            Me.lblContenedor.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblContenedor.Location = New System.Drawing.Point(29, 21)
            Me.lblContenedor.Name = "lblContenedor"
            Me.lblContenedor.Size = New System.Drawing.Size(127, 14)
            Me.lblContenedor.TabIndex = 0
            Me.lblContenedor.Text = "Código Contenedor"
            '
            'OkControlTipoContendor
            '
            Me.OkControlTipoContendor.BackgroundImage = CType(resources.GetObject("OkControlTipoContendor.BackgroundImage"), System.Drawing.Image)
            Me.OkControlTipoContendor.Location = New System.Drawing.Point(428, 55)
            Me.OkControlTipoContendor.Name = "OkControlTipoContendor"
            Me.OkControlTipoContendor.OK = Microsoft.VisualBasic.TriState.[True]
            Me.OkControlTipoContendor.Size = New System.Drawing.Size(12, 12)
            Me.OkControlTipoContendor.TabIndex = 9
            Me.OkControlTipoContendor.TabStop = False
            Me.OkControlTipoContendor.Text = "OkControl9"
            Me.OkControlTipoContendor.Visible = False
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(28, 48)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(110, 14)
            Me.Label4.TabIndex = 7
            Me.Label4.Text = "Tipo Contenedor"
            '
            'TipoContenedorComboBox
            '
            Me.TipoContenedorComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipoContenedorComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipoContenedorComboBox.DisabledEnter = True
            Me.TipoContenedorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipoContenedorComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipoContenedorComboBox.FormattingEnabled = True
            Me.TipoContenedorComboBox.Location = New System.Drawing.Point(177, 46)
            Me.TipoContenedorComboBox.Name = "TipoContenedorComboBox"
            Me.TipoContenedorComboBox.Size = New System.Drawing.Size(199, 21)
            Me.TipoContenedorComboBox.TabIndex = 2
            '
            'AnexoOkControl
            '
            Me.AnexoOkControl.BackgroundImage = CType(resources.GetObject("AnexoOkControl.BackgroundImage"), System.Drawing.Image)
            Me.AnexoOkControl.Location = New System.Drawing.Point(428, 104)
            Me.AnexoOkControl.Name = "AnexoOkControl"
            Me.AnexoOkControl.OK = Microsoft.VisualBasic.TriState.[True]
            Me.AnexoOkControl.Size = New System.Drawing.Size(12, 12)
            Me.AnexoOkControl.TabIndex = 15
            Me.AnexoOkControl.TabStop = False
            Me.AnexoOkControl.Text = "OkControl2"
            Me.AnexoOkControl.Visible = False
            '
            'lbl_Anexo2
            '
            Me.lbl_Anexo2.AutoSize = True
            Me.lbl_Anexo2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lbl_Anexo2.Location = New System.Drawing.Point(29, 102)
            Me.lbl_Anexo2.Name = "lbl_Anexo2"
            Me.lbl_Anexo2.Size = New System.Drawing.Size(110, 14)
            Me.lbl_Anexo2.TabIndex = 13
            Me.lbl_Anexo2.Text = "Confirmar Anexo"
            '
            'lbl_Anexo1
            '
            Me.lbl_Anexo1.AutoSize = True
            Me.lbl_Anexo1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lbl_Anexo1.Location = New System.Drawing.Point(29, 75)
            Me.lbl_Anexo1.Name = "lbl_Anexo1"
            Me.lbl_Anexo1.Size = New System.Drawing.Size(46, 14)
            Me.lbl_Anexo1.TabIndex = 11
            Me.lbl_Anexo1.Text = "Anexo"
            '
            'Anexo2ComboBox
            '
            Me.Anexo2ComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.Anexo2ComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.Anexo2ComboBox.DisabledEnter = True
            Me.Anexo2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Anexo2ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Anexo2ComboBox.FormattingEnabled = True
            Me.Anexo2ComboBox.Location = New System.Drawing.Point(177, 100)
            Me.Anexo2ComboBox.Name = "Anexo2ComboBox"
            Me.Anexo2ComboBox.Size = New System.Drawing.Size(199, 21)
            Me.Anexo2ComboBox.TabIndex = 4
            '
            'Anexo1ComboBox
            '
            Me.Anexo1ComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.Anexo1ComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.Anexo1ComboBox.DisabledEnter = True
            Me.Anexo1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Anexo1ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Anexo1ComboBox.FormattingEnabled = True
            Me.Anexo1ComboBox.Location = New System.Drawing.Point(177, 73)
            Me.Anexo1ComboBox.Name = "Anexo1ComboBox"
            Me.Anexo1ComboBox.Size = New System.Drawing.Size(199, 21)
            Me.Anexo1ComboBox.TabIndex = 3
            '
            'CantidadOkControl
            '
            Me.CantidadOkControl.BackgroundImage = CType(resources.GetObject("CantidadOkControl.BackgroundImage"), System.Drawing.Image)
            Me.CantidadOkControl.Location = New System.Drawing.Point(428, 131)
            Me.CantidadOkControl.Name = "CantidadOkControl"
            Me.CantidadOkControl.OK = Microsoft.VisualBasic.TriState.[True]
            Me.CantidadOkControl.Size = New System.Drawing.Size(12, 12)
            Me.CantidadOkControl.TabIndex = 5
            Me.CantidadOkControl.TabStop = False
            Me.CantidadOkControl.Text = "OkControl9"
            Me.CantidadOkControl.Visible = False
            '
            'ContenedorOkControl
            '
            Me.ContenedorOkControl.BackgroundImage = CType(resources.GetObject("ContenedorOkControl.BackgroundImage"), System.Drawing.Image)
            Me.ContenedorOkControl.Location = New System.Drawing.Point(428, 23)
            Me.ContenedorOkControl.Name = "ContenedorOkControl"
            Me.ContenedorOkControl.OK = Microsoft.VisualBasic.TriState.[True]
            Me.ContenedorOkControl.Size = New System.Drawing.Size(12, 12)
            Me.ContenedorOkControl.TabIndex = 2
            Me.ContenedorOkControl.TabStop = False
            Me.ContenedorOkControl.Text = "OkControl9"
            Me.ContenedorOkControl.Visible = False
            '
            'ContenedoresGroupBox
            '
            Me.ContenedoresGroupBox.Controls.Add(Me.Aux_ContenedoresGrillaPanel)
            Me.ContenedoresGroupBox.Controls.Add(Me.Aux_ContenedoresPanel)
            Me.ContenedoresGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.ContenedoresGroupBox.Location = New System.Drawing.Point(3, 268)
            Me.ContenedoresGroupBox.Name = "ContenedoresGroupBox"
            Me.ContenedoresGroupBox.Size = New System.Drawing.Size(897, 284)
            Me.ContenedoresGroupBox.TabIndex = 14
            Me.ContenedoresGroupBox.TabStop = False
            '
            'Aux_ContenedoresGrillaPanel
            '
            Me.Aux_ContenedoresGrillaPanel.Controls.Add(Me.ContenedoresDataGridView)
            Me.Aux_ContenedoresGrillaPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Aux_ContenedoresGrillaPanel.Location = New System.Drawing.Point(3, 37)
            Me.Aux_ContenedoresGrillaPanel.Name = "Aux_ContenedoresGrillaPanel"
            Me.Aux_ContenedoresGrillaPanel.Padding = New System.Windows.Forms.Padding(10)
            Me.Aux_ContenedoresGrillaPanel.Size = New System.Drawing.Size(891, 244)
            Me.Aux_ContenedoresGrillaPanel.TabIndex = 17
            '
            'ContenedoresDataGridView
            '
            Me.ContenedoresDataGridView.AllowUserToAddRows = False
            Me.ContenedoresDataGridView.AllowUserToDeleteRows = False
            Me.ContenedoresDataGridView.AutoGenerateColumns = False
            Me.ContenedoresDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ContenedoresDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FkCajaDataGridViewTextBoxColumn, Me.IdCajaContenedorDataGridViewTextBoxColumn, Me.Codigo_Contenedor_Destape, Me.Codigo_Contenedor_Empaque, Me.CantidadDocumentosDataGridViewTextBoxColumn})
            Me.ContenedoresDataGridView.DataMember = "TBL_Caja_Contenedor"
            Me.ContenedoresDataGridView.DataSource = Me.BanAgrarioData
            Me.ContenedoresDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedoresDataGridView.Location = New System.Drawing.Point(10, 10)
            Me.ContenedoresDataGridView.Name = "ContenedoresDataGridView"
            Me.ContenedoresDataGridView.ReadOnly = True
            Me.ContenedoresDataGridView.Size = New System.Drawing.Size(871, 224)
            Me.ContenedoresDataGridView.TabIndex = 15
            '
            'FkCajaDataGridViewTextBoxColumn
            '
            Me.FkCajaDataGridViewTextBoxColumn.DataPropertyName = "fk_Caja"
            Me.FkCajaDataGridViewTextBoxColumn.HeaderText = "fk_Caja"
            Me.FkCajaDataGridViewTextBoxColumn.Name = "FkCajaDataGridViewTextBoxColumn"
            Me.FkCajaDataGridViewTextBoxColumn.ReadOnly = True
            Me.FkCajaDataGridViewTextBoxColumn.Visible = False
            '
            'IdCajaContenedorDataGridViewTextBoxColumn
            '
            Me.IdCajaContenedorDataGridViewTextBoxColumn.DataPropertyName = "id_Caja_Contenedor"
            Me.IdCajaContenedorDataGridViewTextBoxColumn.HeaderText = "id_Caja_Contenedor"
            Me.IdCajaContenedorDataGridViewTextBoxColumn.Name = "IdCajaContenedorDataGridViewTextBoxColumn"
            Me.IdCajaContenedorDataGridViewTextBoxColumn.ReadOnly = True
            Me.IdCajaContenedorDataGridViewTextBoxColumn.Visible = False
            '
            'Codigo_Contenedor_Destape
            '
            Me.Codigo_Contenedor_Destape.DataPropertyName = "Codigo_Contenedor_Destape"
            Me.Codigo_Contenedor_Destape.HeaderText = "Número Contenedor Destape"
            Me.Codigo_Contenedor_Destape.Name = "Codigo_Contenedor_Destape"
            Me.Codigo_Contenedor_Destape.ReadOnly = True
            '
            'Codigo_Contenedor_Empaque
            '
            Me.Codigo_Contenedor_Empaque.DataPropertyName = "Codigo_Contenedor_Empaque"
            Me.Codigo_Contenedor_Empaque.HeaderText = "Número Contenedor Empaque"
            Me.Codigo_Contenedor_Empaque.Name = "Codigo_Contenedor_Empaque"
            Me.Codigo_Contenedor_Empaque.ReadOnly = True
            '
            'CantidadDocumentosDataGridViewTextBoxColumn
            '
            Me.CantidadDocumentosDataGridViewTextBoxColumn.DataPropertyName = "Cantidad_Documentos"
            Me.CantidadDocumentosDataGridViewTextBoxColumn.HeaderText = "Cantidad Documentos"
            Me.CantidadDocumentosDataGridViewTextBoxColumn.Name = "CantidadDocumentosDataGridViewTextBoxColumn"
            Me.CantidadDocumentosDataGridViewTextBoxColumn.ReadOnly = True
            Me.CantidadDocumentosDataGridViewTextBoxColumn.Width = 200
            '
            'BanAgrarioData
            '
            Me.BanAgrarioData.DataSetName = "NewDataSet"
            '
            'Aux_ContenedoresPanel
            '
            Me.Aux_ContenedoresPanel.Controls.Add(Me.Aux_ContenedoresLabel)
            Me.Aux_ContenedoresPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.Aux_ContenedoresPanel.Location = New System.Drawing.Point(3, 17)
            Me.Aux_ContenedoresPanel.Name = "Aux_ContenedoresPanel"
            Me.Aux_ContenedoresPanel.Size = New System.Drawing.Size(891, 20)
            Me.Aux_ContenedoresPanel.TabIndex = 16
            '
            'Aux_ContenedoresLabel
            '
            Me.Aux_ContenedoresLabel.AutoSize = True
            Me.Aux_ContenedoresLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Aux_ContenedoresLabel.Location = New System.Drawing.Point(3, 0)
            Me.Aux_ContenedoresLabel.Name = "Aux_ContenedoresLabel"
            Me.Aux_ContenedoresLabel.Size = New System.Drawing.Size(383, 19)
            Me.Aux_ContenedoresLabel.TabIndex = 36
            Me.Aux_ContenedoresLabel.Text = "Listado de contenedores del precinto y oficina"
            '
            'BotonesGroupBox
            '
            Me.BotonesGroupBox.Controls.Add(Me.BotonesPanel)
            Me.BotonesGroupBox.Controls.Add(Me.CerrarButton)
            Me.BotonesGroupBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.BotonesGroupBox.Location = New System.Drawing.Point(3, 214)
            Me.BotonesGroupBox.Name = "BotonesGroupBox"
            Me.BotonesGroupBox.Size = New System.Drawing.Size(897, 45)
            Me.BotonesGroupBox.TabIndex = 3
            Me.BotonesGroupBox.TabStop = False
            '
            'BotonesPanel
            '
            Me.BotonesPanel.Controls.Add(Me.InsertarContenedorNoDestapadoButton)
            Me.BotonesPanel.Controls.Add(Me.InsertarContenedorButton)
            Me.BotonesPanel.Controls.Add(Me.GuardarButton)
            Me.BotonesPanel.Controls.Add(Me.FinalizarButton)
            Me.BotonesPanel.Location = New System.Drawing.Point(6, 11)
            Me.BotonesPanel.Name = "BotonesPanel"
            Me.BotonesPanel.Size = New System.Drawing.Size(737, 30)
            Me.BotonesPanel.TabIndex = 0
            '
            'InsertarContenedorNoDestapadoButton
            '
            Me.InsertarContenedorNoDestapadoButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.folder_add3
            Me.InsertarContenedorNoDestapadoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.InsertarContenedorNoDestapadoButton.Location = New System.Drawing.Point(393, 1)
            Me.InsertarContenedorNoDestapadoButton.Name = "InsertarContenedorNoDestapadoButton"
            Me.InsertarContenedorNoDestapadoButton.Size = New System.Drawing.Size(146, 28)
            Me.InsertarContenedorNoDestapadoButton.TabIndex = 2
            Me.InsertarContenedorNoDestapadoButton.Text = "    Insertar contenedor &no destapado"
            Me.InsertarContenedorNoDestapadoButton.UseVisualStyleBackColor = True
            Me.InsertarContenedorNoDestapadoButton.Visible = False
            '
            'InsertarContenedorButton
            '
            Me.InsertarContenedorButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.folder_add2
            Me.InsertarContenedorButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.InsertarContenedorButton.Location = New System.Drawing.Point(7, 1)
            Me.InsertarContenedorButton.Name = "InsertarContenedorButton"
            Me.InsertarContenedorButton.Size = New System.Drawing.Size(230, 28)
            Me.InsertarContenedorButton.TabIndex = 1
            Me.InsertarContenedorButton.Text = "   &Insertar contenedor (CTR+I o F9)"
            Me.InsertarContenedorButton.UseVisualStyleBackColor = True
            '
            'GuardarButton
            '
            Me.GuardarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(243, 1)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(140, 28)
            Me.GuardarButton.TabIndex = 1
            Me.GuardarButton.Text = "    &Guardar (CTR+G o F10)"
            Me.GuardarButton.UseVisualStyleBackColor = True
            Me.GuardarButton.Visible = False
            '
            'FinalizarButton
            '
            Me.FinalizarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Aceptar1
            Me.FinalizarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.FinalizarButton.Location = New System.Drawing.Point(545, 1)
            Me.FinalizarButton.Name = "FinalizarButton"
            Me.FinalizarButton.Size = New System.Drawing.Size(189, 28)
            Me.FinalizarButton.TabIndex = 3
            Me.FinalizarButton.Text = "   &Finalizar UCS (CTR+F o F5)"
            Me.FinalizarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(749, 13)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(115, 29)
            Me.CerrarButton.TabIndex = 1
            Me.CerrarButton.Text = "     &Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormEmpaque
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(903, 555)
            Me.Controls.Add(Me.BotonesGroupBox)
            Me.Controls.Add(Me.ContenedorEncabezadoGroupBox)
            Me.Controls.Add(Me.ContenedoresGroupBox)
            Me.Controls.Add(Me.ContenedorGroupBox)
            Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.KeyPreview = True
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormEmpaque"
            Me.Padding = New System.Windows.Forms.Padding(3)
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Empaque UCS"
            Me.ContenedorGroupBox.ResumeLayout(False)
            Me.ContenedorGroupBox.PerformLayout()
            Me.ContenedorEncabezadoGroupBox.ResumeLayout(False)
            Me.ContenedorEncabezadoGroupBox.PerformLayout()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ContenedoresGroupBox.ResumeLayout(False)
            Me.Aux_ContenedoresGrillaPanel.ResumeLayout(False)
            CType(Me.ContenedoresDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.BanAgrarioData, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Aux_ContenedoresPanel.ResumeLayout(False)
            Me.Aux_ContenedoresPanel.PerformLayout()
            Me.BotonesGroupBox.ResumeLayout(False)
            Me.BotonesPanel.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents PuntoDestinoComboBox As DesktopComboBoxControl
        Friend WithEvents Cod_Contenedor_NuevoLabel As System.Windows.Forms.Label
        Friend WithEvents ContenedorGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents PrecintoCajaTextBox As DesktopTextBoxControl
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents CantidadDocumentosTextBox As DesktopTextBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents ContenedorTextBox As DesktopTextBoxControl
        Friend WithEvents BanAgrarioData As DBAgrario.ProcessDataSet
        Friend WithEvents ContenedorEncabezadoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ContenedoresGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents Aux_ContenedoresGrillaPanel As System.Windows.Forms.Panel
        Friend WithEvents ContenedoresDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents CodigoContenedorDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Aux_ContenedoresPanel As System.Windows.Forms.Panel
        Friend WithEvents Aux_ContenedoresLabel As System.Windows.Forms.Label
        Friend WithEvents BotonesGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents BotonesPanel As System.Windows.Forms.Panel
        Friend WithEvents InsertarContenedorButton As System.Windows.Forms.Button
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents FinalizarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents InsertarContenedorNoDestapadoButton As System.Windows.Forms.Button
        Friend WithEvents FkCajaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdCajaContenedorDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Codigo_Contenedor_Destape As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Codigo_Contenedor_Empaque As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CantidadDocumentosDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents PuntoOkControl As OkControl
        Friend WithEvents PrecintoOkControl As OkControl
        Friend WithEvents CantidadOkControl As OkControl
        Friend WithEvents ContenedorOkControl As OkControl
        Friend WithEvents AnexoOkControl As OkControl
        Friend WithEvents lbl_Anexo2 As System.Windows.Forms.Label
        Friend WithEvents lbl_Anexo1 As System.Windows.Forms.Label
        Friend WithEvents Anexo2ComboBox As DesktopComboBoxControl
        Friend WithEvents Anexo1ComboBox As DesktopComboBoxControl
        Friend WithEvents lblContenedor As System.Windows.Forms.Label
        Friend WithEvents OkControlTipoContendor As OkControl
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents TipoContenedorComboBox As DesktopComboBoxControl
        Friend WithEvents ImprimirButton As System.Windows.Forms.Button
        Friend WithEvents CambioStickerDesktopCheckBox As DesktopCheckBox.DesktopCheckBoxControl
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents CodigoContenedorNuevoLabel As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents TipoContenedorNuevoLabel As System.Windows.Forms.Label
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents AnexoLabel As System.Windows.Forms.Label
    End Class
End Namespace