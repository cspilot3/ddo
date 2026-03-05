Namespace Forms.Risk
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParProyecto
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormParProyecto))
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.EmpacaTipologiaCheckBox = New System.Windows.Forms.CheckBox()
            Me.fk_Boveda_CustodiaComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.fk_Sede_CustodiaComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.fk_Entidad_custodiaComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Usa_Custodia_ExternaCheckBox = New System.Windows.Forms.CheckBox()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Usa_Solo_Cargue_CarpetaCheckBox = New System.Windows.Forms.CheckBox()
            Me.Usa_Mesa_Control_ImagenesCheckBox = New System.Windows.Forms.CheckBox()
            Me.Usa_Cargue_ParcialCheckBox = New System.Windows.Forms.CheckBox()
            Me.Usa_Cargue_UniversalCheckBox = New System.Windows.Forms.CheckBox()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.ProyectoComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.EntidadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.Usa_Validacion_CargueCheckBox = New System.Windows.Forms.CheckBox()
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            Me.Usa_Validacion_DestapeCheckBox = New System.Windows.Forms.CheckBox()
            Me.MontoDestapeCheckBox = New System.Windows.Forms.CheckBox()
            Me.FoliosDestapeCheckBox = New System.Windows.Forms.CheckBox()
            Me.RequiereImpresionCBarrasCheckBox = New System.Windows.Forms.CheckBox()
            Me.AceptaDevolucionesCheckBox = New System.Windows.Forms.CheckBox()
            Me.GroupBox4 = New System.Windows.Forms.GroupBox()
            Me.DiasVencimientoDesktopNumericTextBoxControl = New Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.GroupBox5 = New System.Windows.Forms.GroupBox()
            Me.Usa_Empaque_AdicionCheckBox = New System.Windows.Forms.CheckBox()
            Me.GroupBox6 = New System.Windows.Forms.GroupBox()
            Me.Usar_Empaque_Documentos_Cancelados = New System.Windows.Forms.CheckBox()
            Me.Usar_Empaque_Carpeta_Cancelados = New System.Windows.Forms.CheckBox()
            Me.Usa_Control_Envio_DocumentosCheckBox = New System.Windows.Forms.CheckBox()
            Me.Usa_Proyecto_DecevalCheckBox = New System.Windows.Forms.CheckBox()
            Me.GroupBox7 = New System.Windows.Forms.GroupBox()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.GroupBox3.SuspendLayout()
            Me.GroupBox4.SuspendLayout()
            Me.GroupBox5.SuspendLayout()
            Me.GroupBox6.SuspendLayout()
            Me.GroupBox7.SuspendLayout()
            Me.SuspendLayout()
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(325, 545)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 43
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label5)
            Me.GroupBox1.Controls.Add(Me.Label6)
            Me.GroupBox1.Controls.Add(Me.EmpacaTipologiaCheckBox)
            Me.GroupBox1.Controls.Add(Me.fk_Boveda_CustodiaComboBox)
            Me.GroupBox1.Controls.Add(Me.fk_Sede_CustodiaComboBox)
            Me.GroupBox1.Controls.Add(Me.fk_Entidad_custodiaComboBox)
            Me.GroupBox1.Controls.Add(Me.Usa_Custodia_ExternaCheckBox)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Location = New System.Drawing.Point(7, 352)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(408, 176)
            Me.GroupBox1.TabIndex = 46
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Custodia"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Label5.Location = New System.Drawing.Point(10, 146)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(115, 13)
            Me.Label5.TabIndex = 59
            Me.Label5.Text = "Empaque por Tipologia"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Label6.Location = New System.Drawing.Point(10, 24)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(109, 13)
            Me.Label6.TabIndex = 58
            Me.Label6.Text = "Usa custodia externa"
            '
            'EmpacaTipologiaCheckBox
            '
            Me.EmpacaTipologiaCheckBox.AutoSize = True
            Me.EmpacaTipologiaCheckBox.Location = New System.Drawing.Point(151, 145)
            Me.EmpacaTipologiaCheckBox.Name = "EmpacaTipologiaCheckBox"
            Me.EmpacaTipologiaCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.EmpacaTipologiaCheckBox.TabIndex = 53
            Me.EmpacaTipologiaCheckBox.UseVisualStyleBackColor = True
            '
            'fk_Boveda_CustodiaComboBox
            '
            Me.fk_Boveda_CustodiaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.fk_Boveda_CustodiaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.fk_Boveda_CustodiaComboBox.DisabledEnter = False
            Me.fk_Boveda_CustodiaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.fk_Boveda_CustodiaComboBox.fk_Campo = 0
            Me.fk_Boveda_CustodiaComboBox.fk_Documento = 0
            Me.fk_Boveda_CustodiaComboBox.fk_Validacion = 0
            Me.fk_Boveda_CustodiaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.fk_Boveda_CustodiaComboBox.FormattingEnabled = True
            Me.fk_Boveda_CustodiaComboBox.Location = New System.Drawing.Point(151, 112)
            Me.fk_Boveda_CustodiaComboBox.Name = "fk_Boveda_CustodiaComboBox"
            Me.fk_Boveda_CustodiaComboBox.Size = New System.Drawing.Size(226, 21)
            Me.fk_Boveda_CustodiaComboBox.TabIndex = 52
            '
            'fk_Sede_CustodiaComboBox
            '
            Me.fk_Sede_CustodiaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.fk_Sede_CustodiaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.fk_Sede_CustodiaComboBox.DisabledEnter = False
            Me.fk_Sede_CustodiaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.fk_Sede_CustodiaComboBox.fk_Campo = 0
            Me.fk_Sede_CustodiaComboBox.fk_Documento = 0
            Me.fk_Sede_CustodiaComboBox.fk_Validacion = 0
            Me.fk_Sede_CustodiaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.fk_Sede_CustodiaComboBox.FormattingEnabled = True
            Me.fk_Sede_CustodiaComboBox.Location = New System.Drawing.Point(151, 81)
            Me.fk_Sede_CustodiaComboBox.Name = "fk_Sede_CustodiaComboBox"
            Me.fk_Sede_CustodiaComboBox.Size = New System.Drawing.Size(226, 21)
            Me.fk_Sede_CustodiaComboBox.TabIndex = 51
            '
            'fk_Entidad_custodiaComboBox
            '
            Me.fk_Entidad_custodiaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.fk_Entidad_custodiaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.fk_Entidad_custodiaComboBox.DisabledEnter = False
            Me.fk_Entidad_custodiaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.fk_Entidad_custodiaComboBox.fk_Campo = 0
            Me.fk_Entidad_custodiaComboBox.fk_Documento = 0
            Me.fk_Entidad_custodiaComboBox.fk_Validacion = 0
            Me.fk_Entidad_custodiaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.fk_Entidad_custodiaComboBox.FormattingEnabled = True
            Me.fk_Entidad_custodiaComboBox.Location = New System.Drawing.Point(151, 50)
            Me.fk_Entidad_custodiaComboBox.Name = "fk_Entidad_custodiaComboBox"
            Me.fk_Entidad_custodiaComboBox.Size = New System.Drawing.Size(226, 21)
            Me.fk_Entidad_custodiaComboBox.TabIndex = 50
            '
            'Usa_Custodia_ExternaCheckBox
            '
            Me.Usa_Custodia_ExternaCheckBox.AutoSize = True
            Me.Usa_Custodia_ExternaCheckBox.Location = New System.Drawing.Point(151, 23)
            Me.Usa_Custodia_ExternaCheckBox.Name = "Usa_Custodia_ExternaCheckBox"
            Me.Usa_Custodia_ExternaCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.Usa_Custodia_ExternaCheckBox.TabIndex = 49
            Me.Usa_Custodia_ExternaCheckBox.UseVisualStyleBackColor = True
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Label4.Location = New System.Drawing.Point(10, 115)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(90, 13)
            Me.Label4.TabIndex = 36
            Me.Label4.Text = "Bóveda custodia:"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Label3.Location = New System.Drawing.Point(10, 84)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(78, 13)
            Me.Label3.TabIndex = 35
            Me.Label3.Text = "Sede custodia:"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Label2.Location = New System.Drawing.Point(10, 53)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(90, 13)
            Me.Label2.TabIndex = 34
            Me.Label2.Text = "Entidad custodia:"
            '
            'Usa_Solo_Cargue_CarpetaCheckBox
            '
            Me.Usa_Solo_Cargue_CarpetaCheckBox.AutoSize = True
            Me.Usa_Solo_Cargue_CarpetaCheckBox.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Usa_Solo_Cargue_CarpetaCheckBox.Location = New System.Drawing.Point(9, 66)
            Me.Usa_Solo_Cargue_CarpetaCheckBox.Name = "Usa_Solo_Cargue_CarpetaCheckBox"
            Me.Usa_Solo_Cargue_CarpetaCheckBox.Size = New System.Drawing.Size(174, 17)
            Me.Usa_Solo_Cargue_CarpetaCheckBox.TabIndex = 53
            Me.Usa_Solo_Cargue_CarpetaCheckBox.Text = "Usa carque a nivel de carpetas"
            Me.Usa_Solo_Cargue_CarpetaCheckBox.UseVisualStyleBackColor = True
            '
            'Usa_Mesa_Control_ImagenesCheckBox
            '
            Me.Usa_Mesa_Control_ImagenesCheckBox.AutoSize = True
            Me.Usa_Mesa_Control_ImagenesCheckBox.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Usa_Mesa_Control_ImagenesCheckBox.Location = New System.Drawing.Point(9, 20)
            Me.Usa_Mesa_Control_ImagenesCheckBox.Name = "Usa_Mesa_Control_ImagenesCheckBox"
            Me.Usa_Mesa_Control_ImagenesCheckBox.Size = New System.Drawing.Size(160, 17)
            Me.Usa_Mesa_Control_ImagenesCheckBox.TabIndex = 48
            Me.Usa_Mesa_Control_ImagenesCheckBox.Text = "Usa Mesa Control Imágenes"
            Me.Usa_Mesa_Control_ImagenesCheckBox.UseVisualStyleBackColor = True
            '
            'Usa_Cargue_ParcialCheckBox
            '
            Me.Usa_Cargue_ParcialCheckBox.AutoSize = True
            Me.Usa_Cargue_ParcialCheckBox.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Usa_Cargue_ParcialCheckBox.Location = New System.Drawing.Point(9, 20)
            Me.Usa_Cargue_ParcialCheckBox.Name = "Usa_Cargue_ParcialCheckBox"
            Me.Usa_Cargue_ParcialCheckBox.Size = New System.Drawing.Size(114, 17)
            Me.Usa_Cargue_ParcialCheckBox.TabIndex = 46
            Me.Usa_Cargue_ParcialCheckBox.Text = "Usa cargue parcial"
            Me.Usa_Cargue_ParcialCheckBox.UseVisualStyleBackColor = True
            '
            'Usa_Cargue_UniversalCheckBox
            '
            Me.Usa_Cargue_UniversalCheckBox.AutoSize = True
            Me.Usa_Cargue_UniversalCheckBox.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Usa_Cargue_UniversalCheckBox.Location = New System.Drawing.Point(9, 43)
            Me.Usa_Cargue_UniversalCheckBox.Name = "Usa_Cargue_UniversalCheckBox"
            Me.Usa_Cargue_UniversalCheckBox.Size = New System.Drawing.Size(126, 17)
            Me.Usa_Cargue_UniversalCheckBox.TabIndex = 44
            Me.Usa_Cargue_UniversalCheckBox.Text = "Usa carque universal"
            Me.Usa_Cargue_UniversalCheckBox.UseVisualStyleBackColor = True
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(229, 545)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(90, 30)
            Me.GuardarButton.TabIndex = 44
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Location = New System.Drawing.Point(3, 38)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(61, 13)
            Me.Label9.TabIndex = 42
            Me.Label9.Text = "Proyecto:"
            '
            'ProyectoComboBox
            '
            Me.ProyectoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProyectoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProyectoComboBox.DisabledEnter = False
            Me.ProyectoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoComboBox.fk_Campo = 0
            Me.ProyectoComboBox.fk_Documento = 0
            Me.ProyectoComboBox.fk_Validacion = 0
            Me.ProyectoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProyectoComboBox.FormattingEnabled = True
            Me.ProyectoComboBox.Location = New System.Drawing.Point(78, 35)
            Me.ProyectoComboBox.Name = "ProyectoComboBox"
            Me.ProyectoComboBox.Size = New System.Drawing.Size(336, 21)
            Me.ProyectoComboBox.TabIndex = 50
            '
            'EntidadDesktopComboBox
            '
            Me.EntidadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadDesktopComboBox.DisabledEnter = False
            Me.EntidadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadDesktopComboBox.fk_Campo = 0
            Me.EntidadDesktopComboBox.fk_Documento = 0
            Me.EntidadDesktopComboBox.fk_Validacion = 0
            Me.EntidadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadDesktopComboBox.FormattingEnabled = True
            Me.EntidadDesktopComboBox.Location = New System.Drawing.Point(78, 6)
            Me.EntidadDesktopComboBox.Name = "EntidadDesktopComboBox"
            Me.EntidadDesktopComboBox.Size = New System.Drawing.Size(336, 21)
            Me.EntidadDesktopComboBox.TabIndex = 52
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(3, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(52, 13)
            Me.Label1.TabIndex = 51
            Me.Label1.Text = "Entidad:"
            '
            'GroupBox2
            '
            Me.GroupBox2.Controls.Add(Me.Usa_Validacion_CargueCheckBox)
            Me.GroupBox2.Controls.Add(Me.Usa_Solo_Cargue_CarpetaCheckBox)
            Me.GroupBox2.Controls.Add(Me.Usa_Cargue_ParcialCheckBox)
            Me.GroupBox2.Controls.Add(Me.Usa_Cargue_UniversalCheckBox)
            Me.GroupBox2.Location = New System.Drawing.Point(201, 67)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(213, 113)
            Me.GroupBox2.TabIndex = 53
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Cargue"
            '
            'Usa_Validacion_CargueCheckBox
            '
            Me.Usa_Validacion_CargueCheckBox.AutoSize = True
            Me.Usa_Validacion_CargueCheckBox.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Usa_Validacion_CargueCheckBox.Location = New System.Drawing.Point(9, 88)
            Me.Usa_Validacion_CargueCheckBox.Name = "Usa_Validacion_CargueCheckBox"
            Me.Usa_Validacion_CargueCheckBox.Size = New System.Drawing.Size(145, 17)
            Me.Usa_Validacion_CargueCheckBox.TabIndex = 54
            Me.Usa_Validacion_CargueCheckBox.Text = "Usa validación de cargue"
            Me.Usa_Validacion_CargueCheckBox.UseVisualStyleBackColor = True
            '
            'GroupBox3
            '
            Me.GroupBox3.Controls.Add(Me.Usa_Validacion_DestapeCheckBox)
            Me.GroupBox3.Controls.Add(Me.MontoDestapeCheckBox)
            Me.GroupBox3.Controls.Add(Me.FoliosDestapeCheckBox)
            Me.GroupBox3.Location = New System.Drawing.Point(6, 204)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Size = New System.Drawing.Size(189, 76)
            Me.GroupBox3.TabIndex = 54
            Me.GroupBox3.TabStop = False
            Me.GroupBox3.Text = "Destape"
            '
            'Usa_Validacion_DestapeCheckBox
            '
            Me.Usa_Validacion_DestapeCheckBox.AutoSize = True
            Me.Usa_Validacion_DestapeCheckBox.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Usa_Validacion_DestapeCheckBox.Location = New System.Drawing.Point(7, 56)
            Me.Usa_Validacion_DestapeCheckBox.Name = "Usa_Validacion_DestapeCheckBox"
            Me.Usa_Validacion_DestapeCheckBox.Size = New System.Drawing.Size(137, 17)
            Me.Usa_Validacion_DestapeCheckBox.TabIndex = 2
            Me.Usa_Validacion_DestapeCheckBox.Text = "Usa Validación Destape"
            Me.Usa_Validacion_DestapeCheckBox.UseVisualStyleBackColor = True
            '
            'MontoDestapeCheckBox
            '
            Me.MontoDestapeCheckBox.AutoSize = True
            Me.MontoDestapeCheckBox.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.MontoDestapeCheckBox.Location = New System.Drawing.Point(7, 37)
            Me.MontoDestapeCheckBox.Name = "MontoDestapeCheckBox"
            Me.MontoDestapeCheckBox.Size = New System.Drawing.Size(141, 17)
            Me.MontoDestapeCheckBox.TabIndex = 1
            Me.MontoDestapeCheckBox.Text = "Captura monto Destape"
            Me.MontoDestapeCheckBox.UseVisualStyleBackColor = True
            '
            'FoliosDestapeCheckBox
            '
            Me.FoliosDestapeCheckBox.AutoSize = True
            Me.FoliosDestapeCheckBox.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.FoliosDestapeCheckBox.Location = New System.Drawing.Point(7, 18)
            Me.FoliosDestapeCheckBox.Name = "FoliosDestapeCheckBox"
            Me.FoliosDestapeCheckBox.Size = New System.Drawing.Size(136, 17)
            Me.FoliosDestapeCheckBox.TabIndex = 0
            Me.FoliosDestapeCheckBox.Text = "Captura folios Destape"
            Me.FoliosDestapeCheckBox.UseVisualStyleBackColor = True
            '
            'RequiereImpresionCBarrasCheckBox
            '
            Me.RequiereImpresionCBarrasCheckBox.AutoSize = True
            Me.RequiereImpresionCBarrasCheckBox.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.RequiereImpresionCBarrasCheckBox.Location = New System.Drawing.Point(9, 66)
            Me.RequiereImpresionCBarrasCheckBox.Name = "RequiereImpresionCBarrasCheckBox"
            Me.RequiereImpresionCBarrasCheckBox.Size = New System.Drawing.Size(164, 17)
            Me.RequiereImpresionCBarrasCheckBox.TabIndex = 55
            Me.RequiereImpresionCBarrasCheckBox.Text = "Usa Impresión Código Barras"
            Me.RequiereImpresionCBarrasCheckBox.UseVisualStyleBackColor = True
            '
            'AceptaDevolucionesCheckBox
            '
            Me.AceptaDevolucionesCheckBox.AutoSize = True
            Me.AceptaDevolucionesCheckBox.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.AceptaDevolucionesCheckBox.Location = New System.Drawing.Point(9, 43)
            Me.AceptaDevolucionesCheckBox.Name = "AceptaDevolucionesCheckBox"
            Me.AceptaDevolucionesCheckBox.Size = New System.Drawing.Size(126, 17)
            Me.AceptaDevolucionesCheckBox.TabIndex = 56
            Me.AceptaDevolucionesCheckBox.Text = "Acepta Devoluciones"
            Me.AceptaDevolucionesCheckBox.UseVisualStyleBackColor = True
            '
            'GroupBox4
            '
            Me.GroupBox4.Controls.Add(Me.DiasVencimientoDesktopNumericTextBoxControl)
            Me.GroupBox4.Controls.Add(Me.Label7)
            Me.GroupBox4.Location = New System.Drawing.Point(201, 205)
            Me.GroupBox4.Name = "GroupBox4"
            Me.GroupBox4.Size = New System.Drawing.Size(213, 35)
            Me.GroupBox4.TabIndex = 57
            Me.GroupBox4.TabStop = False
            Me.GroupBox4.Text = "OT"
            '
            'DiasVencimientoDesktopNumericTextBoxControl
            '
            Me.DiasVencimientoDesktopNumericTextBoxControl.CantidadDecimales = CType(0, Short)
            Me.DiasVencimientoDesktopNumericTextBoxControl.FocusIn = System.Drawing.Color.LightYellow
            Me.DiasVencimientoDesktopNumericTextBoxControl.FocusOut = System.Drawing.Color.White
            Me.DiasVencimientoDesktopNumericTextBoxControl.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DiasVencimientoDesktopNumericTextBoxControl.Location = New System.Drawing.Point(141, 11)
            Me.DiasVencimientoDesktopNumericTextBoxControl.MaxValue = 0.0R
            Me.DiasVencimientoDesktopNumericTextBoxControl.MinValue = 0.0R
            Me.DiasVencimientoDesktopNumericTextBoxControl.Name = "DiasVencimientoDesktopNumericTextBoxControl"
            Me.DiasVencimientoDesktopNumericTextBoxControl.Size = New System.Drawing.Size(66, 20)
            Me.DiasVencimientoDesktopNumericTextBoxControl.TabIndex = 36
            Me.DiasVencimientoDesktopNumericTextBoxControl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.DiasVencimientoDesktopNumericTextBoxControl.UsaDecimales = False
            Me.DiasVencimientoDesktopNumericTextBoxControl.UsaRango = False
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Label7.Location = New System.Drawing.Point(10, 15)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(106, 13)
            Me.Label7.TabIndex = 35
            Me.Label7.Text = "Días de Vencimiento:"
            '
            'GroupBox5
            '
            Me.GroupBox5.Controls.Add(Me.Usa_Empaque_AdicionCheckBox)
            Me.GroupBox5.Location = New System.Drawing.Point(202, 242)
            Me.GroupBox5.Name = "GroupBox5"
            Me.GroupBox5.Size = New System.Drawing.Size(213, 39)
            Me.GroupBox5.TabIndex = 58
            Me.GroupBox5.TabStop = False
            Me.GroupBox5.Text = "Empaque"
            '
            'Usa_Empaque_AdicionCheckBox
            '
            Me.Usa_Empaque_AdicionCheckBox.AutoSize = True
            Me.Usa_Empaque_AdicionCheckBox.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Usa_Empaque_AdicionCheckBox.Location = New System.Drawing.Point(12, 16)
            Me.Usa_Empaque_AdicionCheckBox.Name = "Usa_Empaque_AdicionCheckBox"
            Me.Usa_Empaque_AdicionCheckBox.Size = New System.Drawing.Size(127, 17)
            Me.Usa_Empaque_AdicionCheckBox.TabIndex = 55
            Me.Usa_Empaque_AdicionCheckBox.Text = "Usa empaque adición"
            Me.Usa_Empaque_AdicionCheckBox.UseVisualStyleBackColor = True
            '
            'GroupBox6
            '
            Me.GroupBox6.Controls.Add(Me.Usar_Empaque_Documentos_Cancelados)
            Me.GroupBox6.Controls.Add(Me.Usar_Empaque_Carpeta_Cancelados)
            Me.GroupBox6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GroupBox6.Location = New System.Drawing.Point(7, 284)
            Me.GroupBox6.Name = "GroupBox6"
            Me.GroupBox6.Size = New System.Drawing.Size(408, 62)
            Me.GroupBox6.TabIndex = 55
            Me.GroupBox6.TabStop = False
            Me.GroupBox6.Text = "Empaque Cancelados"
            '
            'Usar_Empaque_Documentos_Cancelados
            '
            Me.Usar_Empaque_Documentos_Cancelados.AutoSize = True
            Me.Usar_Empaque_Documentos_Cancelados.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Usar_Empaque_Documentos_Cancelados.Location = New System.Drawing.Point(13, 43)
            Me.Usar_Empaque_Documentos_Cancelados.Name = "Usar_Empaque_Documentos_Cancelados"
            Me.Usar_Empaque_Documentos_Cancelados.Size = New System.Drawing.Size(175, 17)
            Me.Usar_Empaque_Documentos_Cancelados.TabIndex = 1
            Me.Usar_Empaque_Documentos_Cancelados.Text = "Empaque a nivel de documento"
            Me.Usar_Empaque_Documentos_Cancelados.UseVisualStyleBackColor = True
            '
            'Usar_Empaque_Carpeta_Cancelados
            '
            Me.Usar_Empaque_Carpeta_Cancelados.AutoSize = True
            Me.Usar_Empaque_Carpeta_Cancelados.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Usar_Empaque_Carpeta_Cancelados.Location = New System.Drawing.Point(13, 20)
            Me.Usar_Empaque_Carpeta_Cancelados.Name = "Usar_Empaque_Carpeta_Cancelados"
            Me.Usar_Empaque_Carpeta_Cancelados.Size = New System.Drawing.Size(159, 17)
            Me.Usar_Empaque_Carpeta_Cancelados.TabIndex = 0
            Me.Usar_Empaque_Carpeta_Cancelados.Text = "Empaque a nivel de carpeta"
            Me.Usar_Empaque_Carpeta_Cancelados.UseVisualStyleBackColor = True
            '
            'Usa_Control_Envio_DocumentosCheckBox
            '
            Me.Usa_Control_Envio_DocumentosCheckBox.AutoSize = True
            Me.Usa_Control_Envio_DocumentosCheckBox.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Usa_Control_Envio_DocumentosCheckBox.Location = New System.Drawing.Point(9, 88)
            Me.Usa_Control_Envio_DocumentosCheckBox.Name = "Usa_Control_Envio_DocumentosCheckBox"
            Me.Usa_Control_Envio_DocumentosCheckBox.Size = New System.Drawing.Size(173, 17)
            Me.Usa_Control_Envio_DocumentosCheckBox.TabIndex = 59
            Me.Usa_Control_Envio_DocumentosCheckBox.Text = "Usa Control Envío Documentos"
            Me.Usa_Control_Envio_DocumentosCheckBox.UseVisualStyleBackColor = True
            '
            'Usa_Proyecto_DecevalCheckBox
            '
            Me.Usa_Proyecto_DecevalCheckBox.AutoSize = True
            Me.Usa_Proyecto_DecevalCheckBox.Font = New System.Drawing.Font("Tahoma", 8.25!)
            Me.Usa_Proyecto_DecevalCheckBox.Location = New System.Drawing.Point(9, 111)
            Me.Usa_Proyecto_DecevalCheckBox.Name = "Usa_Proyecto_DecevalCheckBox"
            Me.Usa_Proyecto_DecevalCheckBox.Size = New System.Drawing.Size(131, 17)
            Me.Usa_Proyecto_DecevalCheckBox.TabIndex = 60
            Me.Usa_Proyecto_DecevalCheckBox.Text = "Usa Proyecto Deceval"
            Me.Usa_Proyecto_DecevalCheckBox.UseVisualStyleBackColor = True
            '
            'GroupBox7
            '
            Me.GroupBox7.Controls.Add(Me.Usa_Proyecto_DecevalCheckBox)
            Me.GroupBox7.Controls.Add(Me.Usa_Mesa_Control_ImagenesCheckBox)
            Me.GroupBox7.Controls.Add(Me.AceptaDevolucionesCheckBox)
            Me.GroupBox7.Controls.Add(Me.Usa_Control_Envio_DocumentosCheckBox)
            Me.GroupBox7.Controls.Add(Me.RequiereImpresionCBarrasCheckBox)
            Me.GroupBox7.Location = New System.Drawing.Point(4, 67)
            Me.GroupBox7.Name = "GroupBox7"
            Me.GroupBox7.Size = New System.Drawing.Size(191, 137)
            Me.GroupBox7.TabIndex = 61
            Me.GroupBox7.TabStop = False
            Me.GroupBox7.Text = "Proceso"
            '
            'FormParProyecto
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(423, 587)
            Me.Controls.Add(Me.GroupBox7)
            Me.Controls.Add(Me.GroupBox6)
            Me.Controls.Add(Me.GroupBox5)
            Me.Controls.Add(Me.GroupBox4)
            Me.Controls.Add(Me.GroupBox3)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.EntidadDesktopComboBox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.ProyectoComboBox)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.Label9)
            Me.Controls.Add(Me.CerrarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormParProyecto"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Parametrización de proyectos"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.GroupBox3.ResumeLayout(False)
            Me.GroupBox3.PerformLayout()
            Me.GroupBox4.ResumeLayout(False)
            Me.GroupBox4.PerformLayout()
            Me.GroupBox5.ResumeLayout(False)
            Me.GroupBox5.PerformLayout()
            Me.GroupBox6.ResumeLayout(False)
            Me.GroupBox6.PerformLayout()
            Me.GroupBox7.ResumeLayout(False)
            Me.GroupBox7.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Usa_Cargue_UniversalCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Usa_Mesa_Control_ImagenesCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Usa_Custodia_ExternaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Usa_Cargue_ParcialCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents fk_Boveda_CustodiaComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents fk_Sede_CustodiaComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents fk_Entidad_custodiaComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents ProyectoComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Usa_Solo_Cargue_CarpetaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents EntidadDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents MontoDestapeCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents FoliosDestapeCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents RequiereImpresionCBarrasCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents AceptaDevolucionesCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents EmpacaTipologiaCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Usa_Validacion_CargueCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents DiasVencimientoDesktopNumericTextBoxControl As Miharu.Desktop.Controls.DesktopNumericTextBox.DesktopNumericTextBoxControl
        Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
        Friend WithEvents Usa_Empaque_AdicionCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Usa_Validacion_DestapeCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
        Friend WithEvents Usar_Empaque_Documentos_Cancelados As System.Windows.Forms.CheckBox
        Friend WithEvents Usar_Empaque_Carpeta_Cancelados As System.Windows.Forms.CheckBox
        Friend WithEvents Usa_Control_Envio_DocumentosCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Usa_Proyecto_DecevalCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    End Class
End Namespace