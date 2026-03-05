Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNuevaValidacionDinamica
        Inherits Miharu.Desktop.Library.FormBase

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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNuevaValidacionDinamica))
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.TipoValidacionDinamicaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label11 = New System.Windows.Forms.Label()
            Me.EliminadoCheckBox = New System.Windows.Forms.CheckBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Documentos1DesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Documentos2DesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.ValorCompararDesktopTextBoxControl = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.ValidacionesDesktopComboBoxControl = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.DocumentoObligatorio1DesktopComboBoxControl = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.DocumentoObligatorio2DesktopComboBoxControl = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label12 = New System.Windows.Forms.Label()
            Me.DocumentoObligatorio3DesktopComboBoxControl = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Campo1DesktopComboBoxControl = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Campo2DesktopComboBoxControl = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label13 = New System.Windows.Forms.Label()
            Me.DocumentoValidacionDesktopComboBoxControl = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label14 = New System.Windows.Forms.Label()
            Me.MultiplicaCantidadDocumentoObligatorioCheckBox = New System.Windows.Forms.CheckBox()
            Me.OperadorDesktopComboBoxControl = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(458, 366)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(90, 30)
            Me.GuardarButton.TabIndex = 15
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(554, 366)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 16
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Location = New System.Drawing.Point(12, 110)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(60, 13)
            Me.Label10.TabIndex = 72
            Me.Label10.Text = "Operador"
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(12, 21)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(146, 13)
            Me.Label7.TabIndex = 70
            Me.Label7.Text = "Tipo Validación Dinámica"
            '
            'TipoValidacionDinamicaDesktopComboBox
            '
            Me.TipoValidacionDinamicaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipoValidacionDinamicaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipoValidacionDinamicaDesktopComboBox.DisabledEnter = False
            Me.TipoValidacionDinamicaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipoValidacionDinamicaDesktopComboBox.fk_Campo = 0
            Me.TipoValidacionDinamicaDesktopComboBox.fk_Documento = 0
            Me.TipoValidacionDinamicaDesktopComboBox.fk_Validacion = 0
            Me.TipoValidacionDinamicaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipoValidacionDinamicaDesktopComboBox.FormattingEnabled = True
            Me.TipoValidacionDinamicaDesktopComboBox.Location = New System.Drawing.Point(164, 24)
            Me.TipoValidacionDinamicaDesktopComboBox.Name = "TipoValidacionDinamicaDesktopComboBox"
            Me.TipoValidacionDinamicaDesktopComboBox.Size = New System.Drawing.Size(482, 21)
            Me.TipoValidacionDinamicaDesktopComboBox.TabIndex = 1
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Location = New System.Drawing.Point(15, 330)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(61, 13)
            Me.Label11.TabIndex = 76
            Me.Label11.Text = "Eliminado"
            '
            'EliminadoCheckBox
            '
            Me.EliminadoCheckBox.AutoSize = True
            Me.EliminadoCheckBox.Location = New System.Drawing.Point(120, 330)
            Me.EliminadoCheckBox.Name = "EliminadoCheckBox"
            Me.EliminadoCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.EliminadoCheckBox.TabIndex = 14
            Me.EliminadoCheckBox.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(14, 51)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(82, 13)
            Me.Label1.TabIndex = 82
            Me.Label1.Text = "Documento 1"
            '
            'Documentos1DesktopComboBox
            '
            Me.Documentos1DesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.Documentos1DesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.Documentos1DesktopComboBox.DisabledEnter = False
            Me.Documentos1DesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Documentos1DesktopComboBox.fk_Campo = 0
            Me.Documentos1DesktopComboBox.fk_Documento = 0
            Me.Documentos1DesktopComboBox.fk_Validacion = 0
            Me.Documentos1DesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Documentos1DesktopComboBox.FormattingEnabled = True
            Me.Documentos1DesktopComboBox.Location = New System.Drawing.Point(114, 51)
            Me.Documentos1DesktopComboBox.Name = "Documentos1DesktopComboBox"
            Me.Documentos1DesktopComboBox.Size = New System.Drawing.Size(322, 21)
            Me.Documentos1DesktopComboBox.TabIndex = 2
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(452, 48)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(56, 13)
            Me.Label2.TabIndex = 84
            Me.Label2.Text = "Campo 1"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(452, 79)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(56, 13)
            Me.Label3.TabIndex = 88
            Me.Label3.Text = "Campo 2"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(14, 80)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(82, 13)
            Me.Label4.TabIndex = 86
            Me.Label4.Text = "Documento 2"
            '
            'Documentos2DesktopComboBox
            '
            Me.Documentos2DesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.Documentos2DesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.Documentos2DesktopComboBox.DisabledEnter = False
            Me.Documentos2DesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Documentos2DesktopComboBox.fk_Campo = 0
            Me.Documentos2DesktopComboBox.fk_Documento = 0
            Me.Documentos2DesktopComboBox.fk_Validacion = 0
            Me.Documentos2DesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Documentos2DesktopComboBox.FormattingEnabled = True
            Me.Documentos2DesktopComboBox.Location = New System.Drawing.Point(114, 80)
            Me.Documentos2DesktopComboBox.Name = "Documentos2DesktopComboBox"
            Me.Documentos2DesktopComboBox.Size = New System.Drawing.Size(322, 21)
            Me.Documentos2DesktopComboBox.TabIndex = 4
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(189, 110)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(95, 13)
            Me.Label5.TabIndex = 90
            Me.Label5.Text = "Valor Comparar"
            '
            'ValorCompararDesktopTextBoxControl
            '
            Me.ValorCompararDesktopTextBoxControl._Obligatorio = False
            Me.ValorCompararDesktopTextBoxControl._PermitePegar = False
            Me.ValorCompararDesktopTextBoxControl.Cantidad_Decimales = CType(0, Short)
            Me.ValorCompararDesktopTextBoxControl.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ValorCompararDesktopTextBoxControl.DateFormat = Nothing
            Me.ValorCompararDesktopTextBoxControl.DisabledEnter = False
            Me.ValorCompararDesktopTextBoxControl.DisabledTab = False
            Me.ValorCompararDesktopTextBoxControl.EnabledShortCuts = False
            Me.ValorCompararDesktopTextBoxControl.fk_Campo = 0
            Me.ValorCompararDesktopTextBoxControl.fk_Documento = 0
            Me.ValorCompararDesktopTextBoxControl.fk_Validacion = 0
            Me.ValorCompararDesktopTextBoxControl.FocusIn = System.Drawing.Color.LightYellow
            Me.ValorCompararDesktopTextBoxControl.FocusOut = System.Drawing.Color.White
            Me.ValorCompararDesktopTextBoxControl.Location = New System.Drawing.Point(302, 107)
            Me.ValorCompararDesktopTextBoxControl.MaskedTextBox_Property = ""
            Me.ValorCompararDesktopTextBoxControl.MaximumLength = CType(250, Short)
            Me.ValorCompararDesktopTextBoxControl.MinimumLength = CType(0, Short)
            Me.ValorCompararDesktopTextBoxControl.Name = "ValorCompararDesktopTextBoxControl"
            Me.ValorCompararDesktopTextBoxControl.Obligatorio = False
            Me.ValorCompararDesktopTextBoxControl.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.ValorCompararDesktopTextBoxControl.Rango = Rango1
            Me.ValorCompararDesktopTextBoxControl.Size = New System.Drawing.Size(64, 21)
            Me.ValorCompararDesktopTextBoxControl.TabIndex = 7
            Me.ValorCompararDesktopTextBoxControl.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ValorCompararDesktopTextBoxControl.Usa_Decimales = False
            Me.ValorCompararDesktopTextBoxControl.Validos_Cantidad_Puntos = False
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(15, 171)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(64, 13)
            Me.Label6.TabIndex = 92
            Me.Label6.Text = "Validación"
            '
            'ValidacionesDesktopComboBoxControl
            '
            Me.ValidacionesDesktopComboBoxControl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ValidacionesDesktopComboBoxControl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ValidacionesDesktopComboBoxControl.DisabledEnter = False
            Me.ValidacionesDesktopComboBoxControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ValidacionesDesktopComboBoxControl.fk_Campo = 0
            Me.ValidacionesDesktopComboBoxControl.fk_Documento = 0
            Me.ValidacionesDesktopComboBoxControl.fk_Validacion = 0
            Me.ValidacionesDesktopComboBoxControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ValidacionesDesktopComboBoxControl.FormattingEnabled = True
            Me.ValidacionesDesktopComboBoxControl.Location = New System.Drawing.Point(85, 168)
            Me.ValidacionesDesktopComboBoxControl.Name = "ValidacionesDesktopComboBoxControl"
            Me.ValidacionesDesktopComboBoxControl.Size = New System.Drawing.Size(559, 21)
            Me.ValidacionesDesktopComboBoxControl.TabIndex = 9
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Location = New System.Drawing.Point(11, 10)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(147, 13)
            Me.Label8.TabIndex = 94
            Me.Label8.Text = "Documento Obligatorio 1"
            '
            'DocumentoObligatorio1DesktopComboBoxControl
            '
            Me.DocumentoObligatorio1DesktopComboBoxControl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DocumentoObligatorio1DesktopComboBoxControl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DocumentoObligatorio1DesktopComboBoxControl.DisabledEnter = False
            Me.DocumentoObligatorio1DesktopComboBoxControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentoObligatorio1DesktopComboBoxControl.fk_Campo = 0
            Me.DocumentoObligatorio1DesktopComboBoxControl.fk_Documento = 0
            Me.DocumentoObligatorio1DesktopComboBoxControl.fk_Validacion = 0
            Me.DocumentoObligatorio1DesktopComboBoxControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DocumentoObligatorio1DesktopComboBoxControl.FormattingEnabled = True
            Me.DocumentoObligatorio1DesktopComboBoxControl.Location = New System.Drawing.Point(174, 10)
            Me.DocumentoObligatorio1DesktopComboBoxControl.Name = "DocumentoObligatorio1DesktopComboBoxControl"
            Me.DocumentoObligatorio1DesktopComboBoxControl.Size = New System.Drawing.Size(467, 21)
            Me.DocumentoObligatorio1DesktopComboBoxControl.TabIndex = 10
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Location = New System.Drawing.Point(11, 39)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(147, 13)
            Me.Label9.TabIndex = 96
            Me.Label9.Text = "Documento Obligatorio 2"
            '
            'DocumentoObligatorio2DesktopComboBoxControl
            '
            Me.DocumentoObligatorio2DesktopComboBoxControl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DocumentoObligatorio2DesktopComboBoxControl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DocumentoObligatorio2DesktopComboBoxControl.DisabledEnter = False
            Me.DocumentoObligatorio2DesktopComboBoxControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentoObligatorio2DesktopComboBoxControl.fk_Campo = 0
            Me.DocumentoObligatorio2DesktopComboBoxControl.fk_Documento = 0
            Me.DocumentoObligatorio2DesktopComboBoxControl.fk_Validacion = 0
            Me.DocumentoObligatorio2DesktopComboBoxControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DocumentoObligatorio2DesktopComboBoxControl.FormattingEnabled = True
            Me.DocumentoObligatorio2DesktopComboBoxControl.Location = New System.Drawing.Point(174, 39)
            Me.DocumentoObligatorio2DesktopComboBoxControl.Name = "DocumentoObligatorio2DesktopComboBoxControl"
            Me.DocumentoObligatorio2DesktopComboBoxControl.Size = New System.Drawing.Size(467, 21)
            Me.DocumentoObligatorio2DesktopComboBoxControl.TabIndex = 11
            '
            'Label12
            '
            Me.Label12.AutoSize = True
            Me.Label12.Location = New System.Drawing.Point(11, 67)
            Me.Label12.Name = "Label12"
            Me.Label12.Size = New System.Drawing.Size(147, 13)
            Me.Label12.TabIndex = 98
            Me.Label12.Text = "Documento Obligatorio 3"
            '
            'DocumentoObligatorio3DesktopComboBoxControl
            '
            Me.DocumentoObligatorio3DesktopComboBoxControl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DocumentoObligatorio3DesktopComboBoxControl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DocumentoObligatorio3DesktopComboBoxControl.DisabledEnter = False
            Me.DocumentoObligatorio3DesktopComboBoxControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentoObligatorio3DesktopComboBoxControl.fk_Campo = 0
            Me.DocumentoObligatorio3DesktopComboBoxControl.fk_Documento = 0
            Me.DocumentoObligatorio3DesktopComboBoxControl.fk_Validacion = 0
            Me.DocumentoObligatorio3DesktopComboBoxControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DocumentoObligatorio3DesktopComboBoxControl.FormattingEnabled = True
            Me.DocumentoObligatorio3DesktopComboBoxControl.Location = New System.Drawing.Point(174, 61)
            Me.DocumentoObligatorio3DesktopComboBoxControl.Name = "DocumentoObligatorio3DesktopComboBoxControl"
            Me.DocumentoObligatorio3DesktopComboBoxControl.Size = New System.Drawing.Size(467, 21)
            Me.DocumentoObligatorio3DesktopComboBoxControl.TabIndex = 12
            '
            'Campo1DesktopComboBoxControl
            '
            Me.Campo1DesktopComboBoxControl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.Campo1DesktopComboBoxControl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.Campo1DesktopComboBoxControl.DisabledEnter = False
            Me.Campo1DesktopComboBoxControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Campo1DesktopComboBoxControl.fk_Campo = 0
            Me.Campo1DesktopComboBoxControl.fk_Documento = 0
            Me.Campo1DesktopComboBoxControl.fk_Validacion = 0
            Me.Campo1DesktopComboBoxControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Campo1DesktopComboBoxControl.FormattingEnabled = True
            Me.Campo1DesktopComboBoxControl.Location = New System.Drawing.Point(514, 49)
            Me.Campo1DesktopComboBoxControl.Name = "Campo1DesktopComboBoxControl"
            Me.Campo1DesktopComboBoxControl.Size = New System.Drawing.Size(130, 21)
            Me.Campo1DesktopComboBoxControl.TabIndex = 3
            '
            'Campo2DesktopComboBoxControl
            '
            Me.Campo2DesktopComboBoxControl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.Campo2DesktopComboBoxControl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.Campo2DesktopComboBoxControl.DisabledEnter = False
            Me.Campo2DesktopComboBoxControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Campo2DesktopComboBoxControl.fk_Campo = 0
            Me.Campo2DesktopComboBoxControl.fk_Documento = 0
            Me.Campo2DesktopComboBoxControl.fk_Validacion = 0
            Me.Campo2DesktopComboBoxControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Campo2DesktopComboBoxControl.FormattingEnabled = True
            Me.Campo2DesktopComboBoxControl.Location = New System.Drawing.Point(514, 83)
            Me.Campo2DesktopComboBoxControl.Name = "Campo2DesktopComboBoxControl"
            Me.Campo2DesktopComboBoxControl.Size = New System.Drawing.Size(130, 21)
            Me.Campo2DesktopComboBoxControl.TabIndex = 5
            '
            'Label13
            '
            Me.Label13.AutoSize = True
            Me.Label13.Location = New System.Drawing.Point(15, 139)
            Me.Label13.Name = "Label13"
            Me.Label13.Size = New System.Drawing.Size(132, 13)
            Me.Label13.TabIndex = 102
            Me.Label13.Text = "Documento Validación"
            '
            'DocumentoValidacionDesktopComboBoxControl
            '
            Me.DocumentoValidacionDesktopComboBoxControl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DocumentoValidacionDesktopComboBoxControl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DocumentoValidacionDesktopComboBoxControl.DisabledEnter = False
            Me.DocumentoValidacionDesktopComboBoxControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentoValidacionDesktopComboBoxControl.fk_Campo = 0
            Me.DocumentoValidacionDesktopComboBoxControl.fk_Documento = 0
            Me.DocumentoValidacionDesktopComboBoxControl.fk_Validacion = 0
            Me.DocumentoValidacionDesktopComboBoxControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DocumentoValidacionDesktopComboBoxControl.FormattingEnabled = True
            Me.DocumentoValidacionDesktopComboBoxControl.Location = New System.Drawing.Point(178, 139)
            Me.DocumentoValidacionDesktopComboBoxControl.Name = "DocumentoValidacionDesktopComboBoxControl"
            Me.DocumentoValidacionDesktopComboBoxControl.Size = New System.Drawing.Size(466, 21)
            Me.DocumentoValidacionDesktopComboBoxControl.TabIndex = 8
            '
            'Label14
            '
            Me.Label14.AutoSize = True
            Me.Label14.Location = New System.Drawing.Point(12, 99)
            Me.Label14.Name = "Label14"
            Me.Label14.Size = New System.Drawing.Size(247, 13)
            Me.Label14.TabIndex = 104
            Me.Label14.Text = "Multiplica Cantidad Documento Obligatorio"
            '
            'MultiplicaCantidadDocumentoObligatorioCheckBox
            '
            Me.MultiplicaCantidadDocumentoObligatorioCheckBox.AutoSize = True
            Me.MultiplicaCantidadDocumentoObligatorioCheckBox.Location = New System.Drawing.Point(278, 99)
            Me.MultiplicaCantidadDocumentoObligatorioCheckBox.Name = "MultiplicaCantidadDocumentoObligatorioCheckBox"
            Me.MultiplicaCantidadDocumentoObligatorioCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.MultiplicaCantidadDocumentoObligatorioCheckBox.TabIndex = 13
            Me.MultiplicaCantidadDocumentoObligatorioCheckBox.UseVisualStyleBackColor = True
            '
            'OperadorDesktopComboBoxControl
            '
            Me.OperadorDesktopComboBoxControl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OperadorDesktopComboBoxControl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OperadorDesktopComboBoxControl.DisabledEnter = False
            Me.OperadorDesktopComboBoxControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OperadorDesktopComboBoxControl.fk_Campo = 0
            Me.OperadorDesktopComboBoxControl.fk_Documento = 0
            Me.OperadorDesktopComboBoxControl.fk_Validacion = 0
            Me.OperadorDesktopComboBoxControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OperadorDesktopComboBoxControl.FormattingEnabled = True
            Me.OperadorDesktopComboBoxControl.Location = New System.Drawing.Point(96, 107)
            Me.OperadorDesktopComboBoxControl.Name = "OperadorDesktopComboBoxControl"
            Me.OperadorDesktopComboBoxControl.Size = New System.Drawing.Size(76, 21)
            Me.OperadorDesktopComboBoxControl.TabIndex = 105
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.Label8)
            Me.Panel1.Controls.Add(Me.DocumentoObligatorio1DesktopComboBoxControl)
            Me.Panel1.Controls.Add(Me.Label14)
            Me.Panel1.Controls.Add(Me.DocumentoObligatorio2DesktopComboBoxControl)
            Me.Panel1.Controls.Add(Me.MultiplicaCantidadDocumentoObligatorioCheckBox)
            Me.Panel1.Controls.Add(Me.Label9)
            Me.Panel1.Controls.Add(Me.DocumentoObligatorio3DesktopComboBoxControl)
            Me.Panel1.Controls.Add(Me.Label12)
            Me.Panel1.Location = New System.Drawing.Point(2, 195)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(651, 129)
            Me.Panel1.TabIndex = 106
            '
            'FormNuevaValidacionDinamica
            '
            Me.AcceptButton = Me.GuardarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(656, 408)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.OperadorDesktopComboBoxControl)
            Me.Controls.Add(Me.Label13)
            Me.Controls.Add(Me.DocumentoValidacionDesktopComboBoxControl)
            Me.Controls.Add(Me.Campo2DesktopComboBoxControl)
            Me.Controls.Add(Me.Campo1DesktopComboBoxControl)
            Me.Controls.Add(Me.Label6)
            Me.Controls.Add(Me.ValidacionesDesktopComboBoxControl)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.ValorCompararDesktopTextBoxControl)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.Documentos2DesktopComboBox)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.Documentos1DesktopComboBox)
            Me.Controls.Add(Me.Label11)
            Me.Controls.Add(Me.EliminadoCheckBox)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Label10)
            Me.Controls.Add(Me.Label7)
            Me.Controls.Add(Me.TipoValidacionDinamicaDesktopComboBox)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormNuevaValidacionDinamica"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Agregar Validación"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents TipoValidacionDinamicaDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label11 As System.Windows.Forms.Label
        Friend WithEvents EliminadoCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Documentos1DesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Documentos2DesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents ValorCompararDesktopTextBoxControl As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents ValidacionesDesktopComboBoxControl As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents DocumentoObligatorio1DesktopComboBoxControl As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents DocumentoObligatorio2DesktopComboBoxControl As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label12 As System.Windows.Forms.Label
        Friend WithEvents DocumentoObligatorio3DesktopComboBoxControl As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Campo1DesktopComboBoxControl As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Campo2DesktopComboBoxControl As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label13 As System.Windows.Forms.Label
        Friend WithEvents DocumentoValidacionDesktopComboBoxControl As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Label14 As System.Windows.Forms.Label
        Friend WithEvents MultiplicaCantidadDocumentoObligatorioCheckBox As System.Windows.Forms.CheckBox
        Friend WithEvents OperadorDesktopComboBoxControl As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
    End Class
End Namespace