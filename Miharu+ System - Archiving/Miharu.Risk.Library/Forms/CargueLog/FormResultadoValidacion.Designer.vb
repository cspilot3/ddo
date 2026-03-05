Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.CargueLog
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormResultadoValidacion
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango3 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango4 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango5 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango6 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango7 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango8 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango9 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango10 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango11 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango12 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormResultadoValidacion))
            Me.ResultadoGroupBox = New System.Windows.Forms.GroupBox()
            Me.ResultadosRichTextBox = New System.Windows.Forms.RichTextBox()
            Me.TituloLabel = New System.Windows.Forms.Label()
            Me.ResumenGroupBox = New System.Windows.Forms.GroupBox()
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.TipoDatoCamposDataNoCoincideLabel = New System.Windows.Forms.Label()
            Me.NumeroCamposDataNoCoincideDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.NumeroCamposDataNoCoincideLabel = New System.Windows.Forms.Label()
            Me.TipoDatoLlavesNoCoincideDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.TipoDatoLlavesNoCoincideLabel = New System.Windows.Forms.Label()
            Me.NumeroLlavesNoCoincideDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.NumeroLlavesNoCoincideLabel = New System.Windows.Forms.Label()
            Me.NuevoConLlavesInexistentesDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.NuevoConLlavesInexistentesLabel = New System.Windows.Forms.Label()
            Me.AdicionConLlavesInexistentesDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.AdicionConLlavesInexistentesLabel = New System.Windows.Forms.Label()
            Me.DevolucionSinCodigoBarrasDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.DevolucionSinCodigoBarrasLabel = New System.Windows.Forms.Label()
            Me.ClaseRegistroNoValidoDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.ClaseRegistroNoValidoLabel = New System.Windows.Forms.Label()
            Me.TipoDocumentoNoValidoDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.TipoDocumentoNoValidoLabel = New System.Windows.Forms.Label()
            Me.EsquemaNoValidoDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.EsquemaNoValidoLabel = New System.Windows.Forms.Label()
            Me.NoValidosDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.NoValidosLabel = New System.Windows.Forms.Label()
            Me.ValidosDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.ValidosLabel = New System.Windows.Forms.Label()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CopiarButton = New System.Windows.Forms.Button()
            Me.ResultadoGroupBox.SuspendLayout()
            Me.ResumenGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'ResultadoGroupBox
            '
            Me.ResultadoGroupBox.Controls.Add(Me.ResultadosRichTextBox)
            Me.ResultadoGroupBox.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ResultadoGroupBox.Location = New System.Drawing.Point(12, 39)
            Me.ResultadoGroupBox.Name = "ResultadoGroupBox"
            Me.ResultadoGroupBox.Size = New System.Drawing.Size(402, 284)
            Me.ResultadoGroupBox.TabIndex = 0
            Me.ResultadoGroupBox.TabStop = False
            Me.ResultadoGroupBox.Text = "Resultados de Validación"
            '
            'ResultadosRichTextBox
            '
            Me.ResultadosRichTextBox.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ResultadosRichTextBox.Location = New System.Drawing.Point(7, 22)
            Me.ResultadosRichTextBox.Name = "ResultadosRichTextBox"
            Me.ResultadosRichTextBox.ReadOnly = True
            Me.ResultadosRichTextBox.Size = New System.Drawing.Size(389, 256)
            Me.ResultadosRichTextBox.TabIndex = 0
            Me.ResultadosRichTextBox.Text = ""
            '
            'TituloLabel
            '
            Me.TituloLabel.AutoSize = True
            Me.TituloLabel.Location = New System.Drawing.Point(9, 9)
            Me.TituloLabel.Name = "TituloLabel"
            Me.TituloLabel.Size = New System.Drawing.Size(354, 13)
            Me.TituloLabel.TabIndex = 2
            Me.TituloLabel.Text = "Se generarón los siguientes errores, por favor revise el archivo de cargue."
            '
            'ResumenGroupBox
            '
            Me.ResumenGroupBox.Controls.Add(Me.TipoDatoCamposDataNoCoincideDesktopTextBox)
            Me.ResumenGroupBox.Controls.Add(Me.TipoDatoCamposDataNoCoincideLabel)
            Me.ResumenGroupBox.Controls.Add(Me.NumeroCamposDataNoCoincideDesktopTextBox)
            Me.ResumenGroupBox.Controls.Add(Me.NumeroCamposDataNoCoincideLabel)
            Me.ResumenGroupBox.Controls.Add(Me.TipoDatoLlavesNoCoincideDesktopTextBox)
            Me.ResumenGroupBox.Controls.Add(Me.TipoDatoLlavesNoCoincideLabel)
            Me.ResumenGroupBox.Controls.Add(Me.NumeroLlavesNoCoincideDesktopTextBox)
            Me.ResumenGroupBox.Controls.Add(Me.NumeroLlavesNoCoincideLabel)
            Me.ResumenGroupBox.Controls.Add(Me.NuevoConLlavesInexistentesDesktopTextBox)
            Me.ResumenGroupBox.Controls.Add(Me.NuevoConLlavesInexistentesLabel)
            Me.ResumenGroupBox.Controls.Add(Me.AdicionConLlavesInexistentesDesktopTextBox)
            Me.ResumenGroupBox.Controls.Add(Me.AdicionConLlavesInexistentesLabel)
            Me.ResumenGroupBox.Controls.Add(Me.DevolucionSinCodigoBarrasDesktopTextBox)
            Me.ResumenGroupBox.Controls.Add(Me.DevolucionSinCodigoBarrasLabel)
            Me.ResumenGroupBox.Controls.Add(Me.ClaseRegistroNoValidoDesktopTextBox)
            Me.ResumenGroupBox.Controls.Add(Me.ClaseRegistroNoValidoLabel)
            Me.ResumenGroupBox.Controls.Add(Me.TipoDocumentoNoValidoDesktopTextBox)
            Me.ResumenGroupBox.Controls.Add(Me.TipoDocumentoNoValidoLabel)
            Me.ResumenGroupBox.Controls.Add(Me.EsquemaNoValidoDesktopTextBox)
            Me.ResumenGroupBox.Controls.Add(Me.EsquemaNoValidoLabel)
            Me.ResumenGroupBox.Controls.Add(Me.NoValidosDesktopTextBox)
            Me.ResumenGroupBox.Controls.Add(Me.NoValidosLabel)
            Me.ResumenGroupBox.Controls.Add(Me.ValidosDesktopTextBox)
            Me.ResumenGroupBox.Controls.Add(Me.ValidosLabel)
            Me.ResumenGroupBox.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
            Me.ResumenGroupBox.Location = New System.Drawing.Point(420, 9)
            Me.ResumenGroupBox.Name = "ResumenGroupBox"
            Me.ResumenGroupBox.Size = New System.Drawing.Size(243, 314)
            Me.ResumenGroupBox.TabIndex = 5
            Me.ResumenGroupBox.TabStop = False
            Me.ResumenGroupBox.Text = "Resumen"
            '
            'TipoDatoCamposDataNoCoincideDesktopTextBox
            '
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox._Obligatorio = False
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox._PermitePegar = False
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.DateFormat = Nothing
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.DisabledEnter = False
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.DisabledTab = False
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.EnabledShortCuts = False
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.Location = New System.Drawing.Point(183, 282)
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.MaskedTextBox_Property = ""
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.MaximumLength = CType(0, Short)
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.MinimumLength = CType(0, Short)
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.Name = "TipoDatoCamposDataNoCoincideDesktopTextBox"
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.Obligatorio = False
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.Rango = Rango1
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.ReadOnly = True
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.Size = New System.Drawing.Size(50, 21)
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.TabIndex = 23
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.Usa_Decimales = False
            Me.TipoDatoCamposDataNoCoincideDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'TipoDatoCamposDataNoCoincideLabel
            '
            Me.TipoDatoCamposDataNoCoincideLabel.AutoSize = True
            Me.TipoDatoCamposDataNoCoincideLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TipoDatoCamposDataNoCoincideLabel.Location = New System.Drawing.Point(7, 285)
            Me.TipoDatoCamposDataNoCoincideLabel.Name = "TipoDatoCamposDataNoCoincideLabel"
            Me.TipoDatoCamposDataNoCoincideLabel.Size = New System.Drawing.Size(175, 13)
            Me.TipoDatoCamposDataNoCoincideLabel.TabIndex = 22
            Me.TipoDatoCamposDataNoCoincideLabel.Text = "Tipo dato campos data No Coincide"
            '
            'NumeroCamposDataNoCoincideDesktopTextBox
            '
            Me.NumeroCamposDataNoCoincideDesktopTextBox._Obligatorio = False
            Me.NumeroCamposDataNoCoincideDesktopTextBox._PermitePegar = False
            Me.NumeroCamposDataNoCoincideDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.NumeroCamposDataNoCoincideDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.NumeroCamposDataNoCoincideDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.NumeroCamposDataNoCoincideDesktopTextBox.DateFormat = Nothing
            Me.NumeroCamposDataNoCoincideDesktopTextBox.DisabledEnter = False
            Me.NumeroCamposDataNoCoincideDesktopTextBox.DisabledTab = False
            Me.NumeroCamposDataNoCoincideDesktopTextBox.EnabledShortCuts = False
            Me.NumeroCamposDataNoCoincideDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.NumeroCamposDataNoCoincideDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.NumeroCamposDataNoCoincideDesktopTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NumeroCamposDataNoCoincideDesktopTextBox.Location = New System.Drawing.Point(183, 260)
            Me.NumeroCamposDataNoCoincideDesktopTextBox.MaskedTextBox_Property = ""
            Me.NumeroCamposDataNoCoincideDesktopTextBox.MaximumLength = CType(0, Short)
            Me.NumeroCamposDataNoCoincideDesktopTextBox.MinimumLength = CType(0, Short)
            Me.NumeroCamposDataNoCoincideDesktopTextBox.Name = "NumeroCamposDataNoCoincideDesktopTextBox"
            Me.NumeroCamposDataNoCoincideDesktopTextBox.Obligatorio = False
            Me.NumeroCamposDataNoCoincideDesktopTextBox.permitePegar = False
            Rango2.MaxValue = 2147483647.0R
            Rango2.MinValue = 0.0R
            Me.NumeroCamposDataNoCoincideDesktopTextBox.Rango = Rango2
            Me.NumeroCamposDataNoCoincideDesktopTextBox.ReadOnly = True
            Me.NumeroCamposDataNoCoincideDesktopTextBox.Size = New System.Drawing.Size(50, 21)
            Me.NumeroCamposDataNoCoincideDesktopTextBox.TabIndex = 21
            Me.NumeroCamposDataNoCoincideDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.NumeroCamposDataNoCoincideDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.NumeroCamposDataNoCoincideDesktopTextBox.Usa_Decimales = False
            Me.NumeroCamposDataNoCoincideDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'NumeroCamposDataNoCoincideLabel
            '
            Me.NumeroCamposDataNoCoincideLabel.AutoSize = True
            Me.NumeroCamposDataNoCoincideLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NumeroCamposDataNoCoincideLabel.Location = New System.Drawing.Point(7, 263)
            Me.NumeroCamposDataNoCoincideLabel.Name = "NumeroCamposDataNoCoincideLabel"
            Me.NumeroCamposDataNoCoincideLabel.Size = New System.Drawing.Size(150, 13)
            Me.NumeroCamposDataNoCoincideLabel.TabIndex = 20
            Me.NumeroCamposDataNoCoincideLabel.Text = "Núm Campos data no coincide"
            '
            'TipoDatoLlavesNoCoincideDesktopTextBox
            '
            Me.TipoDatoLlavesNoCoincideDesktopTextBox._Obligatorio = False
            Me.TipoDatoLlavesNoCoincideDesktopTextBox._PermitePegar = False
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.DateFormat = Nothing
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.DisabledEnter = False
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.DisabledTab = False
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.EnabledShortCuts = False
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.Location = New System.Drawing.Point(183, 237)
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.MaskedTextBox_Property = ""
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.MaximumLength = CType(0, Short)
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.MinimumLength = CType(0, Short)
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.Name = "TipoDatoLlavesNoCoincideDesktopTextBox"
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.Obligatorio = False
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.permitePegar = False
            Rango3.MaxValue = 2147483647.0R
            Rango3.MinValue = 0.0R
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.Rango = Rango3
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.ReadOnly = True
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.Size = New System.Drawing.Size(50, 21)
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.TabIndex = 19
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.Usa_Decimales = False
            Me.TipoDatoLlavesNoCoincideDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'TipoDatoLlavesNoCoincideLabel
            '
            Me.TipoDatoLlavesNoCoincideLabel.AutoSize = True
            Me.TipoDatoLlavesNoCoincideLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TipoDatoLlavesNoCoincideLabel.Location = New System.Drawing.Point(7, 240)
            Me.TipoDatoLlavesNoCoincideLabel.Name = "TipoDatoLlavesNoCoincideLabel"
            Me.TipoDatoLlavesNoCoincideLabel.Size = New System.Drawing.Size(144, 13)
            Me.TipoDatoLlavesNoCoincideLabel.TabIndex = 18
            Me.TipoDatoLlavesNoCoincideLabel.Text = "Tipo dato Llaves No Coincide"
            '
            'NumeroLlavesNoCoincideDesktopTextBox
            '
            Me.NumeroLlavesNoCoincideDesktopTextBox._Obligatorio = False
            Me.NumeroLlavesNoCoincideDesktopTextBox._PermitePegar = False
            Me.NumeroLlavesNoCoincideDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.NumeroLlavesNoCoincideDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.NumeroLlavesNoCoincideDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.NumeroLlavesNoCoincideDesktopTextBox.DateFormat = Nothing
            Me.NumeroLlavesNoCoincideDesktopTextBox.DisabledEnter = False
            Me.NumeroLlavesNoCoincideDesktopTextBox.DisabledTab = False
            Me.NumeroLlavesNoCoincideDesktopTextBox.EnabledShortCuts = False
            Me.NumeroLlavesNoCoincideDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.NumeroLlavesNoCoincideDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.NumeroLlavesNoCoincideDesktopTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NumeroLlavesNoCoincideDesktopTextBox.Location = New System.Drawing.Point(183, 214)
            Me.NumeroLlavesNoCoincideDesktopTextBox.MaskedTextBox_Property = ""
            Me.NumeroLlavesNoCoincideDesktopTextBox.MaximumLength = CType(0, Short)
            Me.NumeroLlavesNoCoincideDesktopTextBox.MinimumLength = CType(0, Short)
            Me.NumeroLlavesNoCoincideDesktopTextBox.Name = "NumeroLlavesNoCoincideDesktopTextBox"
            Me.NumeroLlavesNoCoincideDesktopTextBox.Obligatorio = False
            Me.NumeroLlavesNoCoincideDesktopTextBox.permitePegar = False
            Rango4.MaxValue = 2147483647.0R
            Rango4.MinValue = 0.0R
            Me.NumeroLlavesNoCoincideDesktopTextBox.Rango = Rango4
            Me.NumeroLlavesNoCoincideDesktopTextBox.ReadOnly = True
            Me.NumeroLlavesNoCoincideDesktopTextBox.Size = New System.Drawing.Size(50, 21)
            Me.NumeroLlavesNoCoincideDesktopTextBox.TabIndex = 17
            Me.NumeroLlavesNoCoincideDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.NumeroLlavesNoCoincideDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.NumeroLlavesNoCoincideDesktopTextBox.Usa_Decimales = False
            Me.NumeroLlavesNoCoincideDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'NumeroLlavesNoCoincideLabel
            '
            Me.NumeroLlavesNoCoincideLabel.AutoSize = True
            Me.NumeroLlavesNoCoincideLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NumeroLlavesNoCoincideLabel.Location = New System.Drawing.Point(7, 217)
            Me.NumeroLlavesNoCoincideLabel.Name = "NumeroLlavesNoCoincideLabel"
            Me.NumeroLlavesNoCoincideLabel.Size = New System.Drawing.Size(133, 13)
            Me.NumeroLlavesNoCoincideLabel.TabIndex = 16
            Me.NumeroLlavesNoCoincideLabel.Text = "Número llaves No Coincide"
            '
            'NuevoConLlavesInexistentesDesktopTextBox
            '
            Me.NuevoConLlavesInexistentesDesktopTextBox._Obligatorio = False
            Me.NuevoConLlavesInexistentesDesktopTextBox._PermitePegar = False
            Me.NuevoConLlavesInexistentesDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.NuevoConLlavesInexistentesDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.NuevoConLlavesInexistentesDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.NuevoConLlavesInexistentesDesktopTextBox.DateFormat = Nothing
            Me.NuevoConLlavesInexistentesDesktopTextBox.DisabledEnter = False
            Me.NuevoConLlavesInexistentesDesktopTextBox.DisabledTab = False
            Me.NuevoConLlavesInexistentesDesktopTextBox.EnabledShortCuts = False
            Me.NuevoConLlavesInexistentesDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.NuevoConLlavesInexistentesDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.NuevoConLlavesInexistentesDesktopTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NuevoConLlavesInexistentesDesktopTextBox.Location = New System.Drawing.Point(183, 191)
            Me.NuevoConLlavesInexistentesDesktopTextBox.MaskedTextBox_Property = ""
            Me.NuevoConLlavesInexistentesDesktopTextBox.MaximumLength = CType(0, Short)
            Me.NuevoConLlavesInexistentesDesktopTextBox.MinimumLength = CType(0, Short)
            Me.NuevoConLlavesInexistentesDesktopTextBox.Name = "NuevoConLlavesInexistentesDesktopTextBox"
            Me.NuevoConLlavesInexistentesDesktopTextBox.Obligatorio = False
            Me.NuevoConLlavesInexistentesDesktopTextBox.permitePegar = False
            Rango5.MaxValue = 2147483647.0R
            Rango5.MinValue = 0.0R
            Me.NuevoConLlavesInexistentesDesktopTextBox.Rango = Rango5
            Me.NuevoConLlavesInexistentesDesktopTextBox.ReadOnly = True
            Me.NuevoConLlavesInexistentesDesktopTextBox.Size = New System.Drawing.Size(50, 21)
            Me.NuevoConLlavesInexistentesDesktopTextBox.TabIndex = 15
            Me.NuevoConLlavesInexistentesDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.NuevoConLlavesInexistentesDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.NuevoConLlavesInexistentesDesktopTextBox.Usa_Decimales = False
            Me.NuevoConLlavesInexistentesDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'NuevoConLlavesInexistentesLabel
            '
            Me.NuevoConLlavesInexistentesLabel.AutoSize = True
            Me.NuevoConLlavesInexistentesLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NuevoConLlavesInexistentesLabel.Location = New System.Drawing.Point(7, 194)
            Me.NuevoConLlavesInexistentesLabel.Name = "NuevoConLlavesInexistentesLabel"
            Me.NuevoConLlavesInexistentesLabel.Size = New System.Drawing.Size(141, 13)
            Me.NuevoConLlavesInexistentesLabel.TabIndex = 14
            Me.NuevoConLlavesInexistentesLabel.Text = "Nuevo con llaves existentes"
            '
            'AdicionConLlavesInexistentesDesktopTextBox
            '
            Me.AdicionConLlavesInexistentesDesktopTextBox._Obligatorio = False
            Me.AdicionConLlavesInexistentesDesktopTextBox._PermitePegar = False
            Me.AdicionConLlavesInexistentesDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.AdicionConLlavesInexistentesDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.AdicionConLlavesInexistentesDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.AdicionConLlavesInexistentesDesktopTextBox.DateFormat = Nothing
            Me.AdicionConLlavesInexistentesDesktopTextBox.DisabledEnter = False
            Me.AdicionConLlavesInexistentesDesktopTextBox.DisabledTab = False
            Me.AdicionConLlavesInexistentesDesktopTextBox.EnabledShortCuts = False
            Me.AdicionConLlavesInexistentesDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.AdicionConLlavesInexistentesDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.AdicionConLlavesInexistentesDesktopTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AdicionConLlavesInexistentesDesktopTextBox.Location = New System.Drawing.Point(183, 168)
            Me.AdicionConLlavesInexistentesDesktopTextBox.MaskedTextBox_Property = ""
            Me.AdicionConLlavesInexistentesDesktopTextBox.MaximumLength = CType(0, Short)
            Me.AdicionConLlavesInexistentesDesktopTextBox.MinimumLength = CType(0, Short)
            Me.AdicionConLlavesInexistentesDesktopTextBox.Name = "AdicionConLlavesInexistentesDesktopTextBox"
            Me.AdicionConLlavesInexistentesDesktopTextBox.Obligatorio = False
            Me.AdicionConLlavesInexistentesDesktopTextBox.permitePegar = False
            Rango6.MaxValue = 2147483647.0R
            Rango6.MinValue = 0.0R
            Me.AdicionConLlavesInexistentesDesktopTextBox.Rango = Rango6
            Me.AdicionConLlavesInexistentesDesktopTextBox.ReadOnly = True
            Me.AdicionConLlavesInexistentesDesktopTextBox.Size = New System.Drawing.Size(50, 21)
            Me.AdicionConLlavesInexistentesDesktopTextBox.TabIndex = 13
            Me.AdicionConLlavesInexistentesDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.AdicionConLlavesInexistentesDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.AdicionConLlavesInexistentesDesktopTextBox.Usa_Decimales = False
            Me.AdicionConLlavesInexistentesDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'AdicionConLlavesInexistentesLabel
            '
            Me.AdicionConLlavesInexistentesLabel.AutoSize = True
            Me.AdicionConLlavesInexistentesLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AdicionConLlavesInexistentesLabel.Location = New System.Drawing.Point(7, 171)
            Me.AdicionConLlavesInexistentesLabel.Name = "AdicionConLlavesInexistentesLabel"
            Me.AdicionConLlavesInexistentesLabel.Size = New System.Drawing.Size(152, 13)
            Me.AdicionConLlavesInexistentesLabel.TabIndex = 12
            Me.AdicionConLlavesInexistentesLabel.Text = "Adición con llaves inexistentes"
            '
            'DevolucionSinCodigoBarrasDesktopTextBox
            '
            Me.DevolucionSinCodigoBarrasDesktopTextBox._Obligatorio = False
            Me.DevolucionSinCodigoBarrasDesktopTextBox._PermitePegar = False
            Me.DevolucionSinCodigoBarrasDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.DevolucionSinCodigoBarrasDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.DevolucionSinCodigoBarrasDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.DevolucionSinCodigoBarrasDesktopTextBox.DateFormat = Nothing
            Me.DevolucionSinCodigoBarrasDesktopTextBox.DisabledEnter = False
            Me.DevolucionSinCodigoBarrasDesktopTextBox.DisabledTab = False
            Me.DevolucionSinCodigoBarrasDesktopTextBox.EnabledShortCuts = False
            Me.DevolucionSinCodigoBarrasDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.DevolucionSinCodigoBarrasDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.DevolucionSinCodigoBarrasDesktopTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DevolucionSinCodigoBarrasDesktopTextBox.Location = New System.Drawing.Point(183, 145)
            Me.DevolucionSinCodigoBarrasDesktopTextBox.MaskedTextBox_Property = ""
            Me.DevolucionSinCodigoBarrasDesktopTextBox.MaximumLength = CType(0, Short)
            Me.DevolucionSinCodigoBarrasDesktopTextBox.MinimumLength = CType(0, Short)
            Me.DevolucionSinCodigoBarrasDesktopTextBox.Name = "DevolucionSinCodigoBarrasDesktopTextBox"
            Me.DevolucionSinCodigoBarrasDesktopTextBox.Obligatorio = False
            Me.DevolucionSinCodigoBarrasDesktopTextBox.permitePegar = False
            Rango7.MaxValue = 2147483647.0R
            Rango7.MinValue = 0.0R
            Me.DevolucionSinCodigoBarrasDesktopTextBox.Rango = Rango7
            Me.DevolucionSinCodigoBarrasDesktopTextBox.ReadOnly = True
            Me.DevolucionSinCodigoBarrasDesktopTextBox.Size = New System.Drawing.Size(50, 21)
            Me.DevolucionSinCodigoBarrasDesktopTextBox.TabIndex = 11
            Me.DevolucionSinCodigoBarrasDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.DevolucionSinCodigoBarrasDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.DevolucionSinCodigoBarrasDesktopTextBox.Usa_Decimales = False
            Me.DevolucionSinCodigoBarrasDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'DevolucionSinCodigoBarrasLabel
            '
            Me.DevolucionSinCodigoBarrasLabel.AutoSize = True
            Me.DevolucionSinCodigoBarrasLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DevolucionSinCodigoBarrasLabel.Location = New System.Drawing.Point(7, 148)
            Me.DevolucionSinCodigoBarrasLabel.Name = "DevolucionSinCodigoBarrasLabel"
            Me.DevolucionSinCodigoBarrasLabel.Size = New System.Drawing.Size(117, 13)
            Me.DevolucionSinCodigoBarrasLabel.TabIndex = 10
            Me.DevolucionSinCodigoBarrasLabel.Text = "Dev. Sin Código Barras"
            '
            'ClaseRegistroNoValidoDesktopTextBox
            '
            Me.ClaseRegistroNoValidoDesktopTextBox._Obligatorio = False
            Me.ClaseRegistroNoValidoDesktopTextBox._PermitePegar = False
            Me.ClaseRegistroNoValidoDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.ClaseRegistroNoValidoDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.ClaseRegistroNoValidoDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ClaseRegistroNoValidoDesktopTextBox.DateFormat = Nothing
            Me.ClaseRegistroNoValidoDesktopTextBox.DisabledEnter = False
            Me.ClaseRegistroNoValidoDesktopTextBox.DisabledTab = False
            Me.ClaseRegistroNoValidoDesktopTextBox.EnabledShortCuts = False
            Me.ClaseRegistroNoValidoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ClaseRegistroNoValidoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ClaseRegistroNoValidoDesktopTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ClaseRegistroNoValidoDesktopTextBox.Location = New System.Drawing.Point(183, 122)
            Me.ClaseRegistroNoValidoDesktopTextBox.MaskedTextBox_Property = ""
            Me.ClaseRegistroNoValidoDesktopTextBox.MaximumLength = CType(0, Short)
            Me.ClaseRegistroNoValidoDesktopTextBox.MinimumLength = CType(0, Short)
            Me.ClaseRegistroNoValidoDesktopTextBox.Name = "ClaseRegistroNoValidoDesktopTextBox"
            Me.ClaseRegistroNoValidoDesktopTextBox.Obligatorio = False
            Me.ClaseRegistroNoValidoDesktopTextBox.permitePegar = False
            Rango8.MaxValue = 2147483647.0R
            Rango8.MinValue = 0.0R
            Me.ClaseRegistroNoValidoDesktopTextBox.Rango = Rango8
            Me.ClaseRegistroNoValidoDesktopTextBox.ReadOnly = True
            Me.ClaseRegistroNoValidoDesktopTextBox.Size = New System.Drawing.Size(50, 21)
            Me.ClaseRegistroNoValidoDesktopTextBox.TabIndex = 9
            Me.ClaseRegistroNoValidoDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.ClaseRegistroNoValidoDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ClaseRegistroNoValidoDesktopTextBox.Usa_Decimales = False
            Me.ClaseRegistroNoValidoDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'ClaseRegistroNoValidoLabel
            '
            Me.ClaseRegistroNoValidoLabel.AutoSize = True
            Me.ClaseRegistroNoValidoLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ClaseRegistroNoValidoLabel.Location = New System.Drawing.Point(7, 125)
            Me.ClaseRegistroNoValidoLabel.Name = "ClaseRegistroNoValidoLabel"
            Me.ClaseRegistroNoValidoLabel.Size = New System.Drawing.Size(106, 13)
            Me.ClaseRegistroNoValidoLabel.TabIndex = 8
            Me.ClaseRegistroNoValidoLabel.Text = "Clase Reg. No Válido"
            '
            'TipoDocumentoNoValidoDesktopTextBox
            '
            Me.TipoDocumentoNoValidoDesktopTextBox._Obligatorio = False
            Me.TipoDocumentoNoValidoDesktopTextBox._PermitePegar = False
            Me.TipoDocumentoNoValidoDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.TipoDocumentoNoValidoDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.TipoDocumentoNoValidoDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.TipoDocumentoNoValidoDesktopTextBox.DateFormat = Nothing
            Me.TipoDocumentoNoValidoDesktopTextBox.DisabledEnter = False
            Me.TipoDocumentoNoValidoDesktopTextBox.DisabledTab = False
            Me.TipoDocumentoNoValidoDesktopTextBox.EnabledShortCuts = False
            Me.TipoDocumentoNoValidoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.TipoDocumentoNoValidoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.TipoDocumentoNoValidoDesktopTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TipoDocumentoNoValidoDesktopTextBox.Location = New System.Drawing.Point(183, 99)
            Me.TipoDocumentoNoValidoDesktopTextBox.MaskedTextBox_Property = ""
            Me.TipoDocumentoNoValidoDesktopTextBox.MaximumLength = CType(0, Short)
            Me.TipoDocumentoNoValidoDesktopTextBox.MinimumLength = CType(0, Short)
            Me.TipoDocumentoNoValidoDesktopTextBox.Name = "TipoDocumentoNoValidoDesktopTextBox"
            Me.TipoDocumentoNoValidoDesktopTextBox.Obligatorio = False
            Me.TipoDocumentoNoValidoDesktopTextBox.permitePegar = False
            Rango9.MaxValue = 2147483647.0R
            Rango9.MinValue = 0.0R
            Me.TipoDocumentoNoValidoDesktopTextBox.Rango = Rango9
            Me.TipoDocumentoNoValidoDesktopTextBox.ReadOnly = True
            Me.TipoDocumentoNoValidoDesktopTextBox.Size = New System.Drawing.Size(50, 21)
            Me.TipoDocumentoNoValidoDesktopTextBox.TabIndex = 7
            Me.TipoDocumentoNoValidoDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.TipoDocumentoNoValidoDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.TipoDocumentoNoValidoDesktopTextBox.Usa_Decimales = False
            Me.TipoDocumentoNoValidoDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'TipoDocumentoNoValidoLabel
            '
            Me.TipoDocumentoNoValidoLabel.AutoSize = True
            Me.TipoDocumentoNoValidoLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TipoDocumentoNoValidoLabel.Location = New System.Drawing.Point(7, 102)
            Me.TipoDocumentoNoValidoLabel.Name = "TipoDocumentoNoValidoLabel"
            Me.TipoDocumentoNoValidoLabel.Size = New System.Drawing.Size(99, 13)
            Me.TipoDocumentoNoValidoLabel.TabIndex = 6
            Me.TipoDocumentoNoValidoLabel.Text = "Tipo Doc. No Válido"
            '
            'EsquemaNoValidoDesktopTextBox
            '
            Me.EsquemaNoValidoDesktopTextBox._Obligatorio = False
            Me.EsquemaNoValidoDesktopTextBox._PermitePegar = False
            Me.EsquemaNoValidoDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.EsquemaNoValidoDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.EsquemaNoValidoDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.EsquemaNoValidoDesktopTextBox.DateFormat = Nothing
            Me.EsquemaNoValidoDesktopTextBox.DisabledEnter = False
            Me.EsquemaNoValidoDesktopTextBox.DisabledTab = False
            Me.EsquemaNoValidoDesktopTextBox.EnabledShortCuts = False
            Me.EsquemaNoValidoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.EsquemaNoValidoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.EsquemaNoValidoDesktopTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EsquemaNoValidoDesktopTextBox.Location = New System.Drawing.Point(183, 76)
            Me.EsquemaNoValidoDesktopTextBox.MaskedTextBox_Property = ""
            Me.EsquemaNoValidoDesktopTextBox.MaximumLength = CType(0, Short)
            Me.EsquemaNoValidoDesktopTextBox.MinimumLength = CType(0, Short)
            Me.EsquemaNoValidoDesktopTextBox.Name = "EsquemaNoValidoDesktopTextBox"
            Me.EsquemaNoValidoDesktopTextBox.Obligatorio = False
            Me.EsquemaNoValidoDesktopTextBox.permitePegar = False
            Rango10.MaxValue = 2147483647.0R
            Rango10.MinValue = 0.0R
            Me.EsquemaNoValidoDesktopTextBox.Rango = Rango10
            Me.EsquemaNoValidoDesktopTextBox.ReadOnly = True
            Me.EsquemaNoValidoDesktopTextBox.Size = New System.Drawing.Size(50, 21)
            Me.EsquemaNoValidoDesktopTextBox.TabIndex = 5
            Me.EsquemaNoValidoDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.EsquemaNoValidoDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.EsquemaNoValidoDesktopTextBox.Usa_Decimales = False
            Me.EsquemaNoValidoDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'EsquemaNoValidoLabel
            '
            Me.EsquemaNoValidoLabel.AutoSize = True
            Me.EsquemaNoValidoLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.EsquemaNoValidoLabel.Location = New System.Drawing.Point(7, 79)
            Me.EsquemaNoValidoLabel.Name = "EsquemaNoValidoLabel"
            Me.EsquemaNoValidoLabel.Size = New System.Drawing.Size(97, 13)
            Me.EsquemaNoValidoLabel.TabIndex = 4
            Me.EsquemaNoValidoLabel.Text = "Esquema No Válido"
            '
            'NoValidosDesktopTextBox
            '
            Me.NoValidosDesktopTextBox._Obligatorio = False
            Me.NoValidosDesktopTextBox._PermitePegar = False
            Me.NoValidosDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.NoValidosDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.NoValidosDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.NoValidosDesktopTextBox.DateFormat = Nothing
            Me.NoValidosDesktopTextBox.DisabledEnter = False
            Me.NoValidosDesktopTextBox.DisabledTab = False
            Me.NoValidosDesktopTextBox.EnabledShortCuts = False
            Me.NoValidosDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.NoValidosDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.NoValidosDesktopTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NoValidosDesktopTextBox.Location = New System.Drawing.Point(183, 43)
            Me.NoValidosDesktopTextBox.MaskedTextBox_Property = ""
            Me.NoValidosDesktopTextBox.MaximumLength = CType(0, Short)
            Me.NoValidosDesktopTextBox.MinimumLength = CType(0, Short)
            Me.NoValidosDesktopTextBox.Name = "NoValidosDesktopTextBox"
            Me.NoValidosDesktopTextBox.Obligatorio = False
            Me.NoValidosDesktopTextBox.permitePegar = False
            Rango11.MaxValue = 2147483647.0R
            Rango11.MinValue = 0.0R
            Me.NoValidosDesktopTextBox.Rango = Rango11
            Me.NoValidosDesktopTextBox.ReadOnly = True
            Me.NoValidosDesktopTextBox.Size = New System.Drawing.Size(50, 21)
            Me.NoValidosDesktopTextBox.TabIndex = 3
            Me.NoValidosDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.NoValidosDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.NoValidosDesktopTextBox.Usa_Decimales = False
            Me.NoValidosDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'NoValidosLabel
            '
            Me.NoValidosLabel.AutoSize = True
            Me.NoValidosLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NoValidosLabel.Location = New System.Drawing.Point(7, 46)
            Me.NoValidosLabel.Name = "NoValidosLabel"
            Me.NoValidosLabel.Size = New System.Drawing.Size(64, 13)
            Me.NoValidosLabel.TabIndex = 2
            Me.NoValidosLabel.Text = "No Válidos"
            '
            'ValidosDesktopTextBox
            '
            Me.ValidosDesktopTextBox._Obligatorio = False
            Me.ValidosDesktopTextBox._PermitePegar = False
            Me.ValidosDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.ValidosDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.ValidosDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ValidosDesktopTextBox.DateFormat = Nothing
            Me.ValidosDesktopTextBox.DisabledEnter = False
            Me.ValidosDesktopTextBox.DisabledTab = False
            Me.ValidosDesktopTextBox.EnabledShortCuts = False
            Me.ValidosDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ValidosDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ValidosDesktopTextBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValidosDesktopTextBox.Location = New System.Drawing.Point(183, 19)
            Me.ValidosDesktopTextBox.MaskedTextBox_Property = ""
            Me.ValidosDesktopTextBox.MaximumLength = CType(0, Short)
            Me.ValidosDesktopTextBox.MinimumLength = CType(0, Short)
            Me.ValidosDesktopTextBox.Name = "ValidosDesktopTextBox"
            Me.ValidosDesktopTextBox.Obligatorio = False
            Me.ValidosDesktopTextBox.permitePegar = False
            Rango12.MaxValue = 2147483647.0R
            Rango12.MinValue = 0.0R
            Me.ValidosDesktopTextBox.Rango = Rango12
            Me.ValidosDesktopTextBox.ReadOnly = True
            Me.ValidosDesktopTextBox.Size = New System.Drawing.Size(50, 21)
            Me.ValidosDesktopTextBox.TabIndex = 1
            Me.ValidosDesktopTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.ValidosDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ValidosDesktopTextBox.Usa_Decimales = False
            Me.ValidosDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'ValidosLabel
            '
            Me.ValidosLabel.AutoSize = True
            Me.ValidosLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValidosLabel.Location = New System.Drawing.Point(7, 22)
            Me.ValidosLabel.Name = "ValidosLabel"
            Me.ValidosLabel.Size = New System.Drawing.Size(47, 13)
            Me.ValidosLabel.TabIndex = 0
            Me.ValidosLabel.Text = "Válidos"
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.FlatAppearance.BorderSize = 0
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Nothing 'Global.BCS.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(592, 329)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(70, 27)
            Me.CerrarButton.TabIndex = 1
            Me.CerrarButton.Text = "Ce&rrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'GuardarButton
            '
            Me.GuardarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GuardarButton.Image = Nothing ' Global.BCS.Plugin.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(434, 329)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(76, 27)
            Me.GuardarButton.TabIndex = 4
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CopiarButton
            '
            Me.CopiarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CopiarButton.Image = Nothing 'Global.BCS.Plugin.My.Resources.Resources.btnCopiar
            Me.CopiarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CopiarButton.Location = New System.Drawing.Point(516, 329)
            Me.CopiarButton.Name = "CopiarButton"
            Me.CopiarButton.Size = New System.Drawing.Size(70, 27)
            Me.CopiarButton.TabIndex = 3
            Me.CopiarButton.Text = "&Copiar"
            Me.CopiarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CopiarButton.UseVisualStyleBackColor = True
            '
            'FormResultadoValidacion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(674, 363)
            Me.Controls.Add(Me.ResumenGroupBox)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CopiarButton)
            Me.Controls.Add(Me.TituloLabel)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.ResultadoGroupBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormResultadoValidacion"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Resultados de validación"
            Me.ResultadoGroupBox.ResumeLayout(False)
            Me.ResumenGroupBox.ResumeLayout(False)
            Me.ResumenGroupBox.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ResultadoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents TituloLabel As System.Windows.Forms.Label
        Friend WithEvents CopiarButton As System.Windows.Forms.Button
        Friend WithEvents ResultadosRichTextBox As System.Windows.Forms.RichTextBox
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents ResumenGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents NoValidosDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents NoValidosLabel As System.Windows.Forms.Label
        Friend WithEvents ValidosDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents ValidosLabel As System.Windows.Forms.Label
        Friend WithEvents EsquemaNoValidoDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents EsquemaNoValidoLabel As System.Windows.Forms.Label
        Friend WithEvents TipoDocumentoNoValidoDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents TipoDocumentoNoValidoLabel As System.Windows.Forms.Label
        Friend WithEvents ClaseRegistroNoValidoDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents ClaseRegistroNoValidoLabel As System.Windows.Forms.Label
        Friend WithEvents DevolucionSinCodigoBarrasDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents DevolucionSinCodigoBarrasLabel As System.Windows.Forms.Label
        Friend WithEvents AdicionConLlavesInexistentesDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents AdicionConLlavesInexistentesLabel As System.Windows.Forms.Label
        Friend WithEvents NuevoConLlavesInexistentesDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents NuevoConLlavesInexistentesLabel As System.Windows.Forms.Label
        Friend WithEvents NumeroLlavesNoCoincideDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents NumeroLlavesNoCoincideLabel As System.Windows.Forms.Label
        Friend WithEvents TipoDatoLlavesNoCoincideDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents TipoDatoLlavesNoCoincideLabel As System.Windows.Forms.Label
        Friend WithEvents NumeroCamposDataNoCoincideDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents NumeroCamposDataNoCoincideLabel As System.Windows.Forms.Label
        Friend WithEvents TipoDatoCamposDataNoCoincideDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents TipoDatoCamposDataNoCoincideLabel As System.Windows.Forms.Label
    End Class
End Namespace