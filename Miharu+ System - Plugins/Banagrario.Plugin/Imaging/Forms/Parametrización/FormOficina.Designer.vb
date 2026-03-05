Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Imaging.Forms.Parametrización

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormOficina
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
            Dim Rango1 As Rango = New Rango()
            Dim Rango2 As Rango = New Rango()
            Dim Rango3 As Rango = New Rango()
            Dim Rango4 As Rango = New Rango()
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim Rango5 As Rango = New Rango()
            Me.DesktopComboCOB = New DesktopComboBoxControl()
            Me.DesktopComboTipOficina = New DesktopComboBoxControl()
            Me.lblTipodeOficina = New System.Windows.Forms.Label()
            Me.lblCOB = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.txtCodOficina = New DesktopTextBoxControl()
            Me.txtTipCuadre = New DesktopTextBoxControl()
            Me.lblCodOficina = New System.Windows.Forms.Label()
            Me.DesktopComboBoxDepartamento = New DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.txtCodOficinaCruce = New DesktopTextBoxControl()
            Me.txtNombreOficina = New DesktopTextBoxControl()
            Me.CheckBox14 = New System.Windows.Forms.CheckBox()
            Me.lblNomOficina = New System.Windows.Forms.Label()
            Me.lblCodOficinaCruce = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.CheckBox7 = New System.Windows.Forms.CheckBox()
            Me.CheckBox6 = New System.Windows.Forms.CheckBox()
            Me.CheckBox5 = New System.Windows.Forms.CheckBox()
            Me.CheckBox4 = New System.Windows.Forms.CheckBox()
            Me.CheckBox3 = New System.Windows.Forms.CheckBox()
            Me.CheckBoxMartes = New System.Windows.Forms.CheckBox()
            Me.CheckBoxLunes = New System.Windows.Forms.CheckBox()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.CkBoxOficinaActiva = New System.Windows.Forms.CheckBox()
            Me.lblOficinaActiva = New System.Windows.Forms.Label()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.CkBoxDomingo = New System.Windows.Forms.CheckBox()
            Me.CkBoxSabado = New System.Windows.Forms.CheckBox()
            Me.CkBoxViernes = New System.Windows.Forms.CheckBox()
            Me.CkBoxJueves = New System.Windows.Forms.CheckBox()
            Me.CkBoxMiercoles = New System.Windows.Forms.CheckBox()
            Me.CkBoxMartes = New System.Windows.Forms.CheckBox()
            Me.CkBoxLunes = New System.Windows.Forms.CheckBox()
            Me.TabControlOficina = New System.Windows.Forms.TabControl()
            Me.TabPageConsultar = New System.Windows.Forms.TabPage()
            Me.cmbRegional = New DesktopComboBoxControl()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.cmbCOB = New DesktopComboBoxControl()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.DataGridViewOficina = New System.Windows.Forms.DataGridView()
            Me.id_Oficina = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Name_Oficina = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_COB = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Oficina_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.COB = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_Oficina_Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_Departamento = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Activa = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Tipo_Cuadre = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Lunes = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Martes = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Miercoles = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Jueves = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Viernes = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Sabado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Domingo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Codigo_Oficina_Cruce = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TabPageDetalle = New System.Windows.Forms.TabPage()
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            Me.BtnEliminarOfic = New System.Windows.Forms.Button()
            Me.BtnGuardarOfic = New System.Windows.Forms.Button()
            Me.BtnNuevaOfic = New System.Windows.Forms.Button()
            Me.CorreoContactoLabel = New System.Windows.Forms.Label()
            Me.CorreoContactoDesktopTextBox = New DesktopTextBoxControl()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.TabControlOficina.SuspendLayout()
            Me.TabPageConsultar.SuspendLayout()
            CType(Me.DataGridViewOficina, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TabPageDetalle.SuspendLayout()
            Me.GroupBox3.SuspendLayout()
            Me.SuspendLayout()
            '
            'DesktopComboCOB
            '
            Me.DesktopComboCOB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DesktopComboCOB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DesktopComboCOB.DisabledEnter = False
            Me.DesktopComboCOB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DesktopComboCOB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DesktopComboCOB.FormattingEnabled = True
            Me.DesktopComboCOB.Location = New System.Drawing.Point(130, 6)
            Me.DesktopComboCOB.Name = "DesktopComboCOB"
            Me.DesktopComboCOB.Size = New System.Drawing.Size(242, 21)
            Me.DesktopComboCOB.TabIndex = 22
            '
            'DesktopComboTipOficina
            '
            Me.DesktopComboTipOficina.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DesktopComboTipOficina.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DesktopComboTipOficina.DisabledEnter = False
            Me.DesktopComboTipOficina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DesktopComboTipOficina.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DesktopComboTipOficina.FormattingEnabled = True
            Me.DesktopComboTipOficina.Location = New System.Drawing.Point(130, 33)
            Me.DesktopComboTipOficina.Name = "DesktopComboTipOficina"
            Me.DesktopComboTipOficina.Size = New System.Drawing.Size(242, 21)
            Me.DesktopComboTipOficina.TabIndex = 21
            '
            'lblTipodeOficina
            '
            Me.lblTipodeOficina.AutoSize = True
            Me.lblTipodeOficina.Location = New System.Drawing.Point(14, 36)
            Me.lblTipodeOficina.Name = "lblTipodeOficina"
            Me.lblTipodeOficina.Size = New System.Drawing.Size(82, 13)
            Me.lblTipodeOficina.TabIndex = 23
            Me.lblTipodeOficina.Text = "Tipo de Oficina:"
            '
            'lblCOB
            '
            Me.lblCOB.AutoSize = True
            Me.lblCOB.Location = New System.Drawing.Point(17, 10)
            Me.lblCOB.Name = "lblCOB"
            Me.lblCOB.Size = New System.Drawing.Size(29, 13)
            Me.lblCOB.TabIndex = 24
            Me.lblCOB.Text = "COB"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(17, 182)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(86, 13)
            Me.Label6.TabIndex = 40
            Me.Label6.Text = "Tipo de Cuadre :"
            '
            'txtCodOficina
            '
            Me.txtCodOficina.Cantidad_Decimales = CType(0, Short)
            Me.txtCodOficina.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.txtCodOficina.DisabledEnter = False
            Me.txtCodOficina.DisabledTab = False
            Me.txtCodOficina.FocusIn = System.Drawing.Color.LightYellow
            Me.txtCodOficina.FocusOut = System.Drawing.Color.White
            Me.txtCodOficina.Location = New System.Drawing.Point(130, 93)
            Me.txtCodOficina.MaximumLength = CType(0, Short)
            Me.txtCodOficina.MaxLength = 0
            Me.txtCodOficina.MinimumLength = CType(0, Short)
            Me.txtCodOficina.Name = "txtCodOficina"
            Rango1.MaxValue = 1.7976931348623157E+308R
            Rango1.MinValue = 0.0R
            Me.txtCodOficina.Rango = Rango1
            Me.txtCodOficina.ShortcutsEnabled = False
            Me.txtCodOficina.Size = New System.Drawing.Size(242, 20)
            Me.txtCodOficina.TabIndex = 25
            Me.txtCodOficina.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.txtCodOficina.Usa_Decimales = False
            Me.txtCodOficina.Validos_Cantidad_Puntos = False
            '
            'txtTipCuadre
            '
            Me.txtTipCuadre.Cantidad_Decimales = CType(0, Short)
            Me.txtTipCuadre.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.txtTipCuadre.DisabledEnter = False
            Me.txtTipCuadre.DisabledTab = False
            Me.txtTipCuadre.FocusIn = System.Drawing.Color.LightYellow
            Me.txtTipCuadre.FocusOut = System.Drawing.Color.White
            Me.txtTipCuadre.Location = New System.Drawing.Point(130, 179)
            Me.txtTipCuadre.MaximumLength = CType(0, Short)
            Me.txtTipCuadre.MaxLength = 0
            Me.txtTipCuadre.MinimumLength = CType(0, Short)
            Me.txtTipCuadre.Name = "txtTipCuadre"
            Rango2.MaxValue = 1.7976931348623157E+308R
            Rango2.MinValue = 0.0R
            Me.txtTipCuadre.Rango = Rango2
            Me.txtTipCuadre.ShortcutsEnabled = False
            Me.txtTipCuadre.Size = New System.Drawing.Size(245, 20)
            Me.txtTipCuadre.TabIndex = 42
            Me.txtTipCuadre.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.txtTipCuadre.Usa_Decimales = False
            Me.txtTipCuadre.Validos_Cantidad_Puntos = False
            '
            'lblCodOficina
            '
            Me.lblCodOficina.AutoSize = True
            Me.lblCodOficina.Location = New System.Drawing.Point(17, 98)
            Me.lblCodOficina.Name = "lblCodOficina"
            Me.lblCodOficina.Size = New System.Drawing.Size(79, 13)
            Me.lblCodOficina.TabIndex = 28
            Me.lblCodOficina.Text = "Codigo Oficina:"
            '
            'DesktopComboBoxDepartamento
            '
            Me.DesktopComboBoxDepartamento.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DesktopComboBoxDepartamento.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DesktopComboBoxDepartamento.DisabledEnter = False
            Me.DesktopComboBoxDepartamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DesktopComboBoxDepartamento.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DesktopComboBoxDepartamento.FormattingEnabled = True
            Me.DesktopComboBoxDepartamento.Location = New System.Drawing.Point(130, 66)
            Me.DesktopComboBoxDepartamento.Name = "DesktopComboBoxDepartamento"
            Me.DesktopComboBoxDepartamento.Size = New System.Drawing.Size(242, 21)
            Me.DesktopComboBoxDepartamento.TabIndex = 34
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(17, 182)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(86, 13)
            Me.Label2.TabIndex = 39
            Me.Label2.Text = "Tipo de Cuadre :"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(17, 69)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(77, 13)
            Me.Label1.TabIndex = 33
            Me.Label1.Text = "Departamento:"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(14, 313)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(110, 13)
            Me.Label5.TabIndex = 36
            Me.Label5.Text = "Codigo Oficina Cruce:"
            '
            'txtCodOficinaCruce
            '
            Me.txtCodOficinaCruce.Cantidad_Decimales = CType(0, Short)
            Me.txtCodOficinaCruce.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.txtCodOficinaCruce.DisabledEnter = False
            Me.txtCodOficinaCruce.DisabledTab = False
            Me.txtCodOficinaCruce.FocusIn = System.Drawing.Color.LightYellow
            Me.txtCodOficinaCruce.FocusOut = System.Drawing.Color.White
            Me.txtCodOficinaCruce.Location = New System.Drawing.Point(130, 310)
            Me.txtCodOficinaCruce.MaximumLength = CType(0, Short)
            Me.txtCodOficinaCruce.MaxLength = 0
            Me.txtCodOficinaCruce.MinimumLength = CType(0, Short)
            Me.txtCodOficinaCruce.Name = "txtCodOficinaCruce"
            Rango3.MaxValue = 1.7976931348623157E+308R
            Rango3.MinValue = 0.0R
            Me.txtCodOficinaCruce.Rango = Rango3
            Me.txtCodOficinaCruce.ShortcutsEnabled = False
            Me.txtCodOficinaCruce.Size = New System.Drawing.Size(242, 20)
            Me.txtCodOficinaCruce.TabIndex = 38
            Me.txtCodOficinaCruce.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.txtCodOficinaCruce.Usa_Decimales = False
            Me.txtCodOficinaCruce.Validos_Cantidad_Puntos = False
            '
            'txtNombreOficina
            '
            Me.txtNombreOficina.BackColor = System.Drawing.Color.White
            Me.txtNombreOficina.Cantidad_Decimales = CType(0, Short)
            Me.txtNombreOficina.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.txtNombreOficina.DisabledEnter = False
            Me.txtNombreOficina.DisabledTab = False
            Me.txtNombreOficina.FocusIn = System.Drawing.Color.LightYellow
            Me.txtNombreOficina.FocusOut = System.Drawing.Color.White
            Me.txtNombreOficina.Location = New System.Drawing.Point(130, 119)
            Me.txtNombreOficina.MaximumLength = CType(0, Short)
            Me.txtNombreOficina.MaxLength = 0
            Me.txtNombreOficina.MinimumLength = CType(0, Short)
            Me.txtNombreOficina.Name = "txtNombreOficina"
            Rango4.MaxValue = 1.7976931348623157E+308R
            Rango4.MinValue = 0.0R
            Me.txtNombreOficina.Rango = Rango4
            Me.txtNombreOficina.ShortcutsEnabled = False
            Me.txtNombreOficina.Size = New System.Drawing.Size(242, 20)
            Me.txtNombreOficina.TabIndex = 17
            Me.txtNombreOficina.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.txtNombreOficina.Usa_Decimales = False
            Me.txtNombreOficina.Validos_Cantidad_Puntos = False
            '
            'CheckBox14
            '
            Me.CheckBox14.AutoSize = True
            Me.CheckBox14.Location = New System.Drawing.Point(130, 149)
            Me.CheckBox14.Name = "CheckBox14"
            Me.CheckBox14.Size = New System.Drawing.Size(15, 14)
            Me.CheckBox14.TabIndex = 27
            Me.CheckBox14.UseVisualStyleBackColor = True
            '
            'lblNomOficina
            '
            Me.lblNomOficina.AutoSize = True
            Me.lblNomOficina.Location = New System.Drawing.Point(17, 122)
            Me.lblNomOficina.Name = "lblNomOficina"
            Me.lblNomOficina.Size = New System.Drawing.Size(83, 13)
            Me.lblNomOficina.TabIndex = 20
            Me.lblNomOficina.Text = "Nombre Oficina:"
            '
            'lblCodOficinaCruce
            '
            Me.lblCodOficinaCruce.AutoSize = True
            Me.lblCodOficinaCruce.Location = New System.Drawing.Point(14, 313)
            Me.lblCodOficinaCruce.Name = "lblCodOficinaCruce"
            Me.lblCodOficinaCruce.Size = New System.Drawing.Size(110, 13)
            Me.lblCodOficinaCruce.TabIndex = 35
            Me.lblCodOficinaCruce.Text = "Codigo Oficina Cruce:"
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.CheckBox7)
            Me.GroupBox1.Controls.Add(Me.CheckBox6)
            Me.GroupBox1.Controls.Add(Me.CheckBox5)
            Me.GroupBox1.Controls.Add(Me.CheckBox4)
            Me.GroupBox1.Controls.Add(Me.CheckBox3)
            Me.GroupBox1.Controls.Add(Me.CheckBoxMartes)
            Me.GroupBox1.Controls.Add(Me.CheckBoxLunes)
            Me.GroupBox1.Location = New System.Drawing.Point(20, 215)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(336, 73)
            Me.GroupBox1.TabIndex = 29
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Dias de Operación"
            '
            'CheckBox7
            '
            Me.CheckBox7.AutoSize = True
            Me.CheckBox7.Location = New System.Drawing.Point(239, 32)
            Me.CheckBox7.Name = "CheckBox7"
            Me.CheckBox7.Size = New System.Drawing.Size(68, 17)
            Me.CheckBox7.TabIndex = 6
            Me.CheckBox7.Text = "Domingo"
            Me.CheckBox7.UseVisualStyleBackColor = True
            '
            'CheckBox6
            '
            Me.CheckBox6.AutoSize = True
            Me.CheckBox6.Location = New System.Drawing.Point(141, 42)
            Me.CheckBox6.Name = "CheckBox6"
            Me.CheckBox6.Size = New System.Drawing.Size(63, 17)
            Me.CheckBox6.TabIndex = 5
            Me.CheckBox6.Text = "Sabado"
            Me.CheckBox6.UseVisualStyleBackColor = True
            '
            'CheckBox5
            '
            Me.CheckBox5.AutoSize = True
            Me.CheckBox5.Location = New System.Drawing.Point(77, 42)
            Me.CheckBox5.Name = "CheckBox5"
            Me.CheckBox5.Size = New System.Drawing.Size(64, 17)
            Me.CheckBox5.TabIndex = 4
            Me.CheckBox5.Text = "Viernes "
            Me.CheckBox5.UseVisualStyleBackColor = True
            '
            'CheckBox4
            '
            Me.CheckBox4.AutoSize = True
            Me.CheckBox4.Location = New System.Drawing.Point(6, 42)
            Me.CheckBox4.Name = "CheckBox4"
            Me.CheckBox4.Size = New System.Drawing.Size(60, 17)
            Me.CheckBox4.TabIndex = 3
            Me.CheckBox4.Text = "Jueves"
            Me.CheckBox4.UseVisualStyleBackColor = True
            '
            'CheckBox3
            '
            Me.CheckBox3.AutoSize = True
            Me.CheckBox3.Location = New System.Drawing.Point(141, 19)
            Me.CheckBox3.Name = "CheckBox3"
            Me.CheckBox3.Size = New System.Drawing.Size(71, 17)
            Me.CheckBox3.TabIndex = 2
            Me.CheckBox3.Text = "Miercoles"
            Me.CheckBox3.UseVisualStyleBackColor = True
            '
            'CheckBoxMartes
            '
            Me.CheckBoxMartes.AutoSize = True
            Me.CheckBoxMartes.Location = New System.Drawing.Point(77, 19)
            Me.CheckBoxMartes.Name = "CheckBoxMartes"
            Me.CheckBoxMartes.Size = New System.Drawing.Size(58, 17)
            Me.CheckBoxMartes.TabIndex = 1
            Me.CheckBoxMartes.Text = "Martes"
            Me.CheckBoxMartes.UseVisualStyleBackColor = True
            '
            'CheckBoxLunes
            '
            Me.CheckBoxLunes.AutoSize = True
            Me.CheckBoxLunes.Location = New System.Drawing.Point(6, 19)
            Me.CheckBoxLunes.Name = "CheckBoxLunes"
            Me.CheckBoxLunes.Size = New System.Drawing.Size(55, 17)
            Me.CheckBoxLunes.TabIndex = 0
            Me.CheckBoxLunes.Text = "Lunes"
            Me.CheckBoxLunes.UseVisualStyleBackColor = True
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(17, 150)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(76, 13)
            Me.Label4.TabIndex = 31
            Me.Label4.Text = "Oficina Activa:"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(17, 122)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(83, 13)
            Me.Label3.TabIndex = 19
            Me.Label3.Text = "Nombre Oficina:"
            '
            'CkBoxOficinaActiva
            '
            Me.CkBoxOficinaActiva.AutoSize = True
            Me.CkBoxOficinaActiva.Location = New System.Drawing.Point(130, 149)
            Me.CkBoxOficinaActiva.Name = "CkBoxOficinaActiva"
            Me.CkBoxOficinaActiva.Size = New System.Drawing.Size(15, 14)
            Me.CkBoxOficinaActiva.TabIndex = 26
            Me.CkBoxOficinaActiva.UseVisualStyleBackColor = True
            '
            'lblOficinaActiva
            '
            Me.lblOficinaActiva.AutoSize = True
            Me.lblOficinaActiva.Location = New System.Drawing.Point(17, 150)
            Me.lblOficinaActiva.Name = "lblOficinaActiva"
            Me.lblOficinaActiva.Size = New System.Drawing.Size(76, 13)
            Me.lblOficinaActiva.TabIndex = 32
            Me.lblOficinaActiva.Text = "Oficina Activa:"
            '
            'GroupBox2
            '
            Me.GroupBox2.Controls.Add(Me.CkBoxDomingo)
            Me.GroupBox2.Controls.Add(Me.CkBoxSabado)
            Me.GroupBox2.Controls.Add(Me.CkBoxViernes)
            Me.GroupBox2.Controls.Add(Me.CkBoxJueves)
            Me.GroupBox2.Controls.Add(Me.CkBoxMiercoles)
            Me.GroupBox2.Controls.Add(Me.CkBoxMartes)
            Me.GroupBox2.Controls.Add(Me.CkBoxLunes)
            Me.GroupBox2.Location = New System.Drawing.Point(20, 215)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(336, 73)
            Me.GroupBox2.TabIndex = 30
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Dias de Operación"
            '
            'CkBoxDomingo
            '
            Me.CkBoxDomingo.AutoSize = True
            Me.CkBoxDomingo.Location = New System.Drawing.Point(239, 32)
            Me.CkBoxDomingo.Name = "CkBoxDomingo"
            Me.CkBoxDomingo.Size = New System.Drawing.Size(68, 17)
            Me.CkBoxDomingo.TabIndex = 6
            Me.CkBoxDomingo.Text = "Domingo"
            Me.CkBoxDomingo.UseVisualStyleBackColor = True
            '
            'CkBoxSabado
            '
            Me.CkBoxSabado.AutoSize = True
            Me.CkBoxSabado.Location = New System.Drawing.Point(141, 42)
            Me.CkBoxSabado.Name = "CkBoxSabado"
            Me.CkBoxSabado.Size = New System.Drawing.Size(63, 17)
            Me.CkBoxSabado.TabIndex = 5
            Me.CkBoxSabado.Text = "Sabado"
            Me.CkBoxSabado.UseVisualStyleBackColor = True
            '
            'CkBoxViernes
            '
            Me.CkBoxViernes.AutoSize = True
            Me.CkBoxViernes.Location = New System.Drawing.Point(77, 42)
            Me.CkBoxViernes.Name = "CkBoxViernes"
            Me.CkBoxViernes.Size = New System.Drawing.Size(64, 17)
            Me.CkBoxViernes.TabIndex = 4
            Me.CkBoxViernes.Text = "Viernes "
            Me.CkBoxViernes.UseVisualStyleBackColor = True
            '
            'CkBoxJueves
            '
            Me.CkBoxJueves.AutoSize = True
            Me.CkBoxJueves.Location = New System.Drawing.Point(6, 42)
            Me.CkBoxJueves.Name = "CkBoxJueves"
            Me.CkBoxJueves.Size = New System.Drawing.Size(60, 17)
            Me.CkBoxJueves.TabIndex = 3
            Me.CkBoxJueves.Text = "Jueves"
            Me.CkBoxJueves.UseVisualStyleBackColor = True
            '
            'CkBoxMiercoles
            '
            Me.CkBoxMiercoles.AutoSize = True
            Me.CkBoxMiercoles.Location = New System.Drawing.Point(141, 19)
            Me.CkBoxMiercoles.Name = "CkBoxMiercoles"
            Me.CkBoxMiercoles.Size = New System.Drawing.Size(71, 17)
            Me.CkBoxMiercoles.TabIndex = 2
            Me.CkBoxMiercoles.Text = "Miercoles"
            Me.CkBoxMiercoles.UseVisualStyleBackColor = True
            '
            'CkBoxMartes
            '
            Me.CkBoxMartes.AutoSize = True
            Me.CkBoxMartes.Location = New System.Drawing.Point(77, 19)
            Me.CkBoxMartes.Name = "CkBoxMartes"
            Me.CkBoxMartes.Size = New System.Drawing.Size(58, 17)
            Me.CkBoxMartes.TabIndex = 1
            Me.CkBoxMartes.Text = "Martes"
            Me.CkBoxMartes.UseVisualStyleBackColor = True
            '
            'CkBoxLunes
            '
            Me.CkBoxLunes.AutoSize = True
            Me.CkBoxLunes.Location = New System.Drawing.Point(6, 19)
            Me.CkBoxLunes.Name = "CkBoxLunes"
            Me.CkBoxLunes.Size = New System.Drawing.Size(55, 17)
            Me.CkBoxLunes.TabIndex = 0
            Me.CkBoxLunes.Text = "Lunes"
            Me.CkBoxLunes.UseVisualStyleBackColor = True
            '
            'TabControlOficina
            '
            Me.TabControlOficina.Controls.Add(Me.TabPageConsultar)
            Me.TabControlOficina.Controls.Add(Me.TabPageDetalle)
            Me.TabControlOficina.Location = New System.Drawing.Point(21, 68)
            Me.TabControlOficina.Name = "TabControlOficina"
            Me.TabControlOficina.SelectedIndex = 0
            Me.TabControlOficina.Size = New System.Drawing.Size(606, 454)
            Me.TabControlOficina.TabIndex = 45
            '
            'TabPageConsultar
            '
            Me.TabPageConsultar.Controls.Add(Me.cmbRegional)
            Me.TabPageConsultar.Controls.Add(Me.Label8)
            Me.TabPageConsultar.Controls.Add(Me.cmbCOB)
            Me.TabPageConsultar.Controls.Add(Me.Label7)
            Me.TabPageConsultar.Controls.Add(Me.DataGridViewOficina)
            Me.TabPageConsultar.Location = New System.Drawing.Point(4, 22)
            Me.TabPageConsultar.Name = "TabPageConsultar"
            Me.TabPageConsultar.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPageConsultar.Size = New System.Drawing.Size(598, 428)
            Me.TabPageConsultar.TabIndex = 0
            Me.TabPageConsultar.Text = "Consultar"
            Me.TabPageConsultar.UseVisualStyleBackColor = True
            '
            'cmbRegional
            '
            Me.cmbRegional.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cmbRegional.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cmbRegional.DisabledEnter = False
            Me.cmbRegional.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbRegional.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cmbRegional.FormattingEnabled = True
            Me.cmbRegional.Location = New System.Drawing.Point(125, 15)
            Me.cmbRegional.Name = "cmbRegional"
            Me.cmbRegional.Size = New System.Drawing.Size(240, 21)
            Me.cmbRegional.TabIndex = 8
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Location = New System.Drawing.Point(22, 18)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(52, 13)
            Me.Label8.TabIndex = 7
            Me.Label8.Text = "Regional:"
            '
            'cmbCOB
            '
            Me.cmbCOB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cmbCOB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cmbCOB.DisabledEnter = False
            Me.cmbCOB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbCOB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cmbCOB.FormattingEnabled = True
            Me.cmbCOB.Location = New System.Drawing.Point(125, 44)
            Me.cmbCOB.Name = "cmbCOB"
            Me.cmbCOB.Size = New System.Drawing.Size(240, 21)
            Me.cmbCOB.TabIndex = 6
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(22, 47)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(32, 13)
            Me.Label7.TabIndex = 3
            Me.Label7.Text = "COB:"
            '
            'DataGridViewOficina
            '
            Me.DataGridViewOficina.AllowUserToAddRows = False
            Me.DataGridViewOficina.AllowUserToDeleteRows = False
            Me.DataGridViewOficina.AllowUserToOrderColumns = True
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DataGridViewOficina.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.DataGridViewOficina.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridViewOficina.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_Oficina, Me.Name_Oficina, Me.fk_COB, Me.fk_Oficina_Tipo, Me.COB, Me.id_Oficina_Tipo, Me.fk_Departamento, Me.Activa, Me.Tipo_Cuadre, Me.Lunes, Me.Martes, Me.Miercoles, Me.Jueves, Me.Viernes, Me.Sabado, Me.Domingo, Me.Codigo_Oficina_Cruce})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DataGridViewOficina.DefaultCellStyle = DataGridViewCellStyle2
            Me.DataGridViewOficina.Location = New System.Drawing.Point(16, 122)
            Me.DataGridViewOficina.Name = "DataGridViewOficina"
            Me.DataGridViewOficina.ReadOnly = True
            Me.DataGridViewOficina.Size = New System.Drawing.Size(556, 287)
            Me.DataGridViewOficina.TabIndex = 0
            '
            'id_Oficina
            '
            Me.id_Oficina.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.id_Oficina.DataPropertyName = "id_Oficina"
            Me.id_Oficina.HeaderText = "Cod Oficina"
            Me.id_Oficina.Name = "id_Oficina"
            Me.id_Oficina.ReadOnly = True
            Me.id_Oficina.Width = 87
            '
            'Name_Oficina
            '
            Me.Name_Oficina.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Name_Oficina.DataPropertyName = "Nombre_Oficina"
            Me.Name_Oficina.HeaderText = "Oficina"
            Me.Name_Oficina.Name = "Name_Oficina"
            Me.Name_Oficina.ReadOnly = True
            Me.Name_Oficina.Width = 65
            '
            'fk_COB
            '
            Me.fk_COB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.fk_COB.DataPropertyName = "Nombre_COB"
            Me.fk_COB.HeaderText = "COB"
            Me.fk_COB.Name = "fk_COB"
            Me.fk_COB.ReadOnly = True
            Me.fk_COB.Width = 54
            '
            'fk_Oficina_Tipo
            '
            Me.fk_Oficina_Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.fk_Oficina_Tipo.DataPropertyName = "Nombre_Oficina_Tipo"
            Me.fk_Oficina_Tipo.HeaderText = "Tipo Oficina"
            Me.fk_Oficina_Tipo.Name = "fk_Oficina_Tipo"
            Me.fk_Oficina_Tipo.ReadOnly = True
            Me.fk_Oficina_Tipo.Width = 89
            '
            'COB
            '
            Me.COB.DataPropertyName = "id_COB"
            Me.COB.HeaderText = "id_COB"
            Me.COB.Name = "COB"
            Me.COB.ReadOnly = True
            Me.COB.Visible = False
            '
            'id_Oficina_Tipo
            '
            Me.id_Oficina_Tipo.DataPropertyName = "id_Oficina_Tipo"
            Me.id_Oficina_Tipo.HeaderText = "idOficinaTipo"
            Me.id_Oficina_Tipo.Name = "id_Oficina_Tipo"
            Me.id_Oficina_Tipo.ReadOnly = True
            Me.id_Oficina_Tipo.Visible = False
            '
            'fk_Departamento
            '
            Me.fk_Departamento.DataPropertyName = "fk_Departamento"
            Me.fk_Departamento.HeaderText = "fkDepartamento"
            Me.fk_Departamento.Name = "fk_Departamento"
            Me.fk_Departamento.ReadOnly = True
            Me.fk_Departamento.Visible = False
            '
            'Activa
            '
            Me.Activa.DataPropertyName = "Activa"
            Me.Activa.HeaderText = "Activa"
            Me.Activa.Name = "Activa"
            Me.Activa.ReadOnly = True
            Me.Activa.Visible = False
            '
            'Tipo_Cuadre
            '
            Me.Tipo_Cuadre.DataPropertyName = "Tipo_Cuadre"
            Me.Tipo_Cuadre.HeaderText = "Tipo_Cuadre"
            Me.Tipo_Cuadre.Name = "Tipo_Cuadre"
            Me.Tipo_Cuadre.ReadOnly = True
            Me.Tipo_Cuadre.Visible = False
            '
            'Lunes
            '
            Me.Lunes.DataPropertyName = "Lunes"
            Me.Lunes.HeaderText = "Lunes"
            Me.Lunes.Name = "Lunes"
            Me.Lunes.ReadOnly = True
            Me.Lunes.Visible = False
            '
            'Martes
            '
            Me.Martes.DataPropertyName = "Martes"
            Me.Martes.HeaderText = "Martes"
            Me.Martes.Name = "Martes"
            Me.Martes.ReadOnly = True
            Me.Martes.Visible = False
            '
            'Miercoles
            '
            Me.Miercoles.DataPropertyName = "Miercoles"
            Me.Miercoles.HeaderText = "Miercoles"
            Me.Miercoles.Name = "Miercoles"
            Me.Miercoles.ReadOnly = True
            Me.Miercoles.Visible = False
            '
            'Jueves
            '
            Me.Jueves.DataPropertyName = "Jueves"
            Me.Jueves.HeaderText = "Jueves"
            Me.Jueves.Name = "Jueves"
            Me.Jueves.ReadOnly = True
            Me.Jueves.Visible = False
            '
            'Viernes
            '
            Me.Viernes.DataPropertyName = "Viernes"
            Me.Viernes.HeaderText = "Viernes"
            Me.Viernes.Name = "Viernes"
            Me.Viernes.ReadOnly = True
            Me.Viernes.Visible = False
            '
            'Sabado
            '
            Me.Sabado.DataPropertyName = "Sabado"
            Me.Sabado.HeaderText = "Sabado"
            Me.Sabado.Name = "Sabado"
            Me.Sabado.ReadOnly = True
            Me.Sabado.Visible = False
            '
            'Domingo
            '
            Me.Domingo.DataPropertyName = "Domingo"
            Me.Domingo.HeaderText = "Domingo"
            Me.Domingo.Name = "Domingo"
            Me.Domingo.ReadOnly = True
            Me.Domingo.Visible = False
            '
            'Codigo_Oficina_Cruce
            '
            Me.Codigo_Oficina_Cruce.DataPropertyName = "Codigo_Oficina_Cruce"
            Me.Codigo_Oficina_Cruce.HeaderText = "Codigo_Oficina_Cruce"
            Me.Codigo_Oficina_Cruce.Name = "Codigo_Oficina_Cruce"
            Me.Codigo_Oficina_Cruce.ReadOnly = True
            Me.Codigo_Oficina_Cruce.Visible = False
            '
            'TabPageDetalle
            '
            Me.TabPageDetalle.Controls.Add(Me.CorreoContactoLabel)
            Me.TabPageDetalle.Controls.Add(Me.CorreoContactoDesktopTextBox)
            Me.TabPageDetalle.Controls.Add(Me.DesktopComboCOB)
            Me.TabPageDetalle.Controls.Add(Me.GroupBox2)
            Me.TabPageDetalle.Controls.Add(Me.lblOficinaActiva)
            Me.TabPageDetalle.Controls.Add(Me.DesktopComboTipOficina)
            Me.TabPageDetalle.Controls.Add(Me.CkBoxOficinaActiva)
            Me.TabPageDetalle.Controls.Add(Me.Label3)
            Me.TabPageDetalle.Controls.Add(Me.lblTipodeOficina)
            Me.TabPageDetalle.Controls.Add(Me.Label4)
            Me.TabPageDetalle.Controls.Add(Me.GroupBox1)
            Me.TabPageDetalle.Controls.Add(Me.lblCOB)
            Me.TabPageDetalle.Controls.Add(Me.lblCodOficinaCruce)
            Me.TabPageDetalle.Controls.Add(Me.Label6)
            Me.TabPageDetalle.Controls.Add(Me.lblNomOficina)
            Me.TabPageDetalle.Controls.Add(Me.txtCodOficina)
            Me.TabPageDetalle.Controls.Add(Me.CheckBox14)
            Me.TabPageDetalle.Controls.Add(Me.txtTipCuadre)
            Me.TabPageDetalle.Controls.Add(Me.txtNombreOficina)
            Me.TabPageDetalle.Controls.Add(Me.lblCodOficina)
            Me.TabPageDetalle.Controls.Add(Me.txtCodOficinaCruce)
            Me.TabPageDetalle.Controls.Add(Me.DesktopComboBoxDepartamento)
            Me.TabPageDetalle.Controls.Add(Me.Label5)
            Me.TabPageDetalle.Controls.Add(Me.Label2)
            Me.TabPageDetalle.Controls.Add(Me.Label1)
            Me.TabPageDetalle.Location = New System.Drawing.Point(4, 22)
            Me.TabPageDetalle.Name = "TabPageDetalle"
            Me.TabPageDetalle.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPageDetalle.Size = New System.Drawing.Size(598, 428)
            Me.TabPageDetalle.TabIndex = 1
            Me.TabPageDetalle.Text = "Detalle"
            Me.TabPageDetalle.UseVisualStyleBackColor = True
            '
            'GroupBox3
            '
            Me.GroupBox3.Controls.Add(Me.BtnEliminarOfic)
            Me.GroupBox3.Controls.Add(Me.BtnGuardarOfic)
            Me.GroupBox3.Controls.Add(Me.BtnNuevaOfic)
            Me.GroupBox3.Location = New System.Drawing.Point(357, 12)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Size = New System.Drawing.Size(263, 71)
            Me.GroupBox3.TabIndex = 48
            Me.GroupBox3.TabStop = False
            '
            'BtnEliminarOfic
            '
            Me.BtnEliminarOfic.AccessibleDescription = ""
            Me.BtnEliminarOfic.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.BtnEliminarOfic.BackColor = System.Drawing.SystemColors.Control
            Me.BtnEliminarOfic.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnEliminarOfic.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnEliminarOfic.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.BtnEliminarOfic.Location = New System.Drawing.Point(179, 15)
            Me.BtnEliminarOfic.Name = "BtnEliminarOfic"
            Me.BtnEliminarOfic.Size = New System.Drawing.Size(57, 50)
            Me.BtnEliminarOfic.TabIndex = 47
            Me.BtnEliminarOfic.Tag = "Ctrl + P"
            Me.BtnEliminarOfic.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.BtnEliminarOfic.UseVisualStyleBackColor = False
            '
            'BtnGuardarOfic
            '
            Me.BtnGuardarOfic.AccessibleDescription = ""
            Me.BtnGuardarOfic.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.BtnGuardarOfic.BackColor = System.Drawing.SystemColors.Control
            Me.BtnGuardarOfic.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnGuardarOfic.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnGuardarOfic.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.BtnGuardarOfic.Location = New System.Drawing.Point(98, 15)
            Me.BtnGuardarOfic.Name = "BtnGuardarOfic"
            Me.BtnGuardarOfic.Size = New System.Drawing.Size(57, 50)
            Me.BtnGuardarOfic.TabIndex = 46
            Me.BtnGuardarOfic.Tag = "Ctrl + P"
            Me.BtnGuardarOfic.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.BtnGuardarOfic.UseVisualStyleBackColor = False
            '
            'BtnNuevaOfic
            '
            Me.BtnNuevaOfic.AccessibleDescription = ""
            Me.BtnNuevaOfic.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.BtnNuevaOfic.BackColor = System.Drawing.SystemColors.Control
            Me.BtnNuevaOfic.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnNuevaOfic.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnNuevaOfic.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.BtnNuevaOfic.Location = New System.Drawing.Point(18, 15)
            Me.BtnNuevaOfic.Name = "BtnNuevaOfic"
            Me.BtnNuevaOfic.Size = New System.Drawing.Size(56, 50)
            Me.BtnNuevaOfic.TabIndex = 44
            Me.BtnNuevaOfic.Tag = "Ctrl + P"
            Me.BtnNuevaOfic.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.BtnNuevaOfic.UseVisualStyleBackColor = False
            '
            'CorreoContactoLabel
            '
            Me.CorreoContactoLabel.AutoSize = True
            Me.CorreoContactoLabel.Location = New System.Drawing.Point(14, 339)
            Me.CorreoContactoLabel.Name = "CorreoContactoLabel"
            Me.CorreoContactoLabel.Size = New System.Drawing.Size(87, 13)
            Me.CorreoContactoLabel.TabIndex = 44
            Me.CorreoContactoLabel.Text = "Correo Contacto:"
            '
            'CorreoContactoDesktopTextBox
            '
            Me.CorreoContactoDesktopTextBox.BackColor = System.Drawing.Color.White
            Me.CorreoContactoDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.CorreoContactoDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.CorreoContactoDesktopTextBox.DisabledEnter = False
            Me.CorreoContactoDesktopTextBox.DisabledTab = False
            Me.CorreoContactoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.CorreoContactoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.CorreoContactoDesktopTextBox.Location = New System.Drawing.Point(130, 336)
            Me.CorreoContactoDesktopTextBox.MaximumLength = CType(0, Short)
            Me.CorreoContactoDesktopTextBox.MaxLength = 0
            Me.CorreoContactoDesktopTextBox.MinimumLength = CType(0, Short)
            Me.CorreoContactoDesktopTextBox.Name = "CorreoContactoDesktopTextBox"
            Rango5.MaxValue = 1.7976931348623157E+308R
            Rango5.MinValue = 0.0R
            Me.CorreoContactoDesktopTextBox.Rango = Rango5
            Me.CorreoContactoDesktopTextBox.ShortcutsEnabled = False
            Me.CorreoContactoDesktopTextBox.Size = New System.Drawing.Size(242, 20)
            Me.CorreoContactoDesktopTextBox.TabIndex = 43
            Me.CorreoContactoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.CorreoContactoDesktopTextBox.Usa_Decimales = False
            Me.CorreoContactoDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'FormOficina
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(639, 530)
            Me.Controls.Add(Me.GroupBox3)
            Me.Controls.Add(Me.TabControlOficina)
            Me.Name = "FormOficina"
            Me.Text = "Parametrizacion Oficina"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.TabControlOficina.ResumeLayout(False)
            Me.TabPageConsultar.ResumeLayout(False)
            Me.TabPageConsultar.PerformLayout()
            CType(Me.DataGridViewOficina, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TabPageDetalle.ResumeLayout(False)
            Me.TabPageDetalle.PerformLayout()
            Me.GroupBox3.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents DesktopComboCOB As DesktopComboBoxControl
        Friend WithEvents DesktopComboTipOficina As DesktopComboBoxControl
        Friend WithEvents lblTipodeOficina As System.Windows.Forms.Label
        Friend WithEvents lblCOB As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents txtCodOficina As DesktopTextBoxControl
        Friend WithEvents txtTipCuadre As DesktopTextBoxControl
        Friend WithEvents lblCodOficina As System.Windows.Forms.Label
        Friend WithEvents DesktopComboBoxDepartamento As DesktopComboBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents txtCodOficinaCruce As DesktopTextBoxControl
        Friend WithEvents txtNombreOficina As DesktopTextBoxControl
        Friend WithEvents CheckBox14 As System.Windows.Forms.CheckBox
        Friend WithEvents lblNomOficina As System.Windows.Forms.Label
        Friend WithEvents lblCodOficinaCruce As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
        Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
        Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
        Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
        Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
        Friend WithEvents CheckBoxMartes As System.Windows.Forms.CheckBox
        Friend WithEvents CheckBoxLunes As System.Windows.Forms.CheckBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents CkBoxOficinaActiva As System.Windows.Forms.CheckBox
        Friend WithEvents lblOficinaActiva As System.Windows.Forms.Label
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents CkBoxDomingo As System.Windows.Forms.CheckBox
        Friend WithEvents CkBoxSabado As System.Windows.Forms.CheckBox
        Friend WithEvents CkBoxViernes As System.Windows.Forms.CheckBox
        Friend WithEvents CkBoxJueves As System.Windows.Forms.CheckBox
        Friend WithEvents CkBoxMiercoles As System.Windows.Forms.CheckBox
        Friend WithEvents CkBoxMartes As System.Windows.Forms.CheckBox
        Friend WithEvents CkBoxLunes As System.Windows.Forms.CheckBox
        Friend WithEvents BtnNuevaOfic As System.Windows.Forms.Button
        Friend WithEvents TabControlOficina As System.Windows.Forms.TabControl
        Friend WithEvents TabPageConsultar As System.Windows.Forms.TabPage
        Friend WithEvents cmbCOB As DesktopComboBoxControl
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents DataGridViewOficina As System.Windows.Forms.DataGridView
        Friend WithEvents TabPageDetalle As System.Windows.Forms.TabPage
        Friend WithEvents cmbRegional As DesktopComboBoxControl
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents id_Oficina As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Name_Oficina As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_COB As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Oficina_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents COB As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_Oficina_Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_Departamento As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Activa As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Tipo_Cuadre As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Lunes As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Martes As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Miercoles As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Jueves As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Viernes As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Sabado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Domingo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Codigo_Oficina_Cruce As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents BtnGuardarOfic As System.Windows.Forms.Button
        Friend WithEvents BtnEliminarOfic As System.Windows.Forms.Button
        Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents CorreoContactoLabel As System.Windows.Forms.Label
        Friend WithEvents CorreoContactoDesktopTextBox As DesktopTextBoxControl
    End Class
End Namespace