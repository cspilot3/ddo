Imports System.Windows.Forms

Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class Frm_Digitaliza
        Inherits System.Windows.Forms.Form

        'Form reemplaza a Dispose para limpiar la lista de componentes.
        <System.Diagnostics.DebuggerNonUserCode()>
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

        Dim UsuarioReg As String = "Brinks de Colombia"
        Dim CorreoReg As String = "alvaro.ordonez@brinks.com.co"
        Dim CodigoReg As String = "Q36h0tx2wkLIYIwObuNFnU8b66Xywx7EF8sKNUFx7Z0v88+4aKSpsdy33iqk4sTI3w9kLK4oY2lClWOoOjZGvnvQ49GnOvzn819cj7KbpvUs2R+ajURn9lQx+swUg1x6aMT6VRhH1+ZrBggI0eDdCr3j9wZLJmO2zxy8i4vJlQxA"


        'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
        'Se puede modificar usando el Diseñador de Windows Forms.  
        'No lo modifique con el editor de código.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Digitaliza))
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.LblTitulo = New System.Windows.Forms.Label()
            Me.GBDatosLote = New System.Windows.Forms.GroupBox()
            Me.CmbTipologia = New System.Windows.Forms.ComboBox()
            Me.CmbDigitaliza = New System.Windows.Forms.ComboBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.lblTipologia = New System.Windows.Forms.Label()
            Me.BtnDigitalizador = New System.Windows.Forms.Button()
            Me.LblDigitalizador = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.LblNumLote = New System.Windows.Forms.Label()
            Me.DtpFecha = New System.Windows.Forms.DateTimePicker()
            Me.LblFecha = New System.Windows.Forms.Label()
            Me.LblJornada = New System.Windows.Forms.Label()
            Me.LblStiker = New System.Windows.Forms.Label()
            Me.TxtNumStiker = New TextBoxMask.TextBoxMask()
            Me.TxtNumJornada = New TextBoxMask.TextBoxMask()
            Me.PanEscaner = New System.Windows.Forms.Panel()
            Me.BtnAddCarpeta = New System.Windows.Forms.Button()
            Me.BtnAddImagen = New System.Windows.Forms.Button()
            Me.DgvRegistrosLote = New System.Windows.Forms.DataGridView()
            Me.Ide = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.LblRegLote = New System.Windows.Forms.Label()
            Me.BtnEscanear = New System.Windows.Forms.Button()
            Me.BtnEliminaPagina = New System.Windows.Forms.Button()
            Me.BtnEliminaLote = New System.Windows.Forms.Button()
            Me.BtnVolver = New System.Windows.Forms.Button()
            Me.TxtNomJornada = New TextBoxMask.TextBoxMask()
            Me.FLPImagenes = New System.Windows.Forms.FlowLayoutPanel()
            Me.BtnServidor = New System.Windows.Forms.Button()
            Me.BtnDigitalizar = New System.Windows.Forms.Button()
            Me.BtnExportarLotes = New System.Windows.Forms.Button()
            Me.BtnCrearCarpeta = New System.Windows.Forms.Button()
            Me.BtnNewCarpeta = New System.Windows.Forms.Button()
            Me.PicImagen = New System.Windows.Forms.PictureBox()
            Me.BtnCerrar = New System.Windows.Forms.Button()
            Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.DgvCarpetas = New System.Windows.Forms.DataGridView()
            Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DctosImg = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.Panel3 = New System.Windows.Forms.Panel()
            Me.Panel5 = New System.Windows.Forms.Panel()
            Me.Panel4 = New System.Windows.Forms.Panel()
            Me.BtnAutoTamaño = New System.Windows.Forms.Button()
            Me.BtnNormal = New System.Windows.Forms.Button()
            Me.BntGiraDerecha = New System.Windows.Forms.Button()
            Me.BtnUltimo = New System.Windows.Forms.Button()
            Me.BtnSiguiente = New System.Windows.Forms.Button()
            Me.BtnAnterior = New System.Windows.Forms.Button()
            Me.BntPrimero = New System.Windows.Forms.Button()
            Me.PicA = New System.Windows.Forms.PictureBox()
            Me.PicR = New System.Windows.Forms.PictureBox()
            Me.GBDatosLote.SuspendLayout()
            Me.PanEscaner.SuspendLayout()
            CType(Me.DgvRegistrosLote, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PicImagen, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel1.SuspendLayout()
            CType(Me.DgvCarpetas, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel2.SuspendLayout()
            Me.Panel3.SuspendLayout()
            Me.Panel5.SuspendLayout()
            Me.Panel4.SuspendLayout()
            CType(Me.PicA, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PicR, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'LblTitulo
            '
            Me.LblTitulo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
            Me.LblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblTitulo.Location = New System.Drawing.Point(9, 6)
            Me.LblTitulo.Name = "LblTitulo"
            Me.LblTitulo.Size = New System.Drawing.Size(332, 17)
            Me.LblTitulo.TabIndex = 7
            Me.LblTitulo.Text = "4795 CENTRO INTERNACIONAL"
            Me.LblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'GBDatosLote
            '
            Me.GBDatosLote.BackColor = System.Drawing.SystemColors.Control
            Me.GBDatosLote.Controls.Add(Me.CmbTipologia)
            Me.GBDatosLote.Controls.Add(Me.CmbDigitaliza)
            Me.GBDatosLote.Controls.Add(Me.Label7)
            Me.GBDatosLote.Controls.Add(Me.lblTipologia)
            Me.GBDatosLote.Controls.Add(Me.BtnDigitalizador)
            Me.GBDatosLote.Controls.Add(Me.LblDigitalizador)
            Me.GBDatosLote.Controls.Add(Me.Label2)
            Me.GBDatosLote.Controls.Add(Me.LblNumLote)
            Me.GBDatosLote.Location = New System.Drawing.Point(6, 24)
            Me.GBDatosLote.Name = "GBDatosLote"
            Me.GBDatosLote.Size = New System.Drawing.Size(335, 85)
            Me.GBDatosLote.TabIndex = 1
            Me.GBDatosLote.TabStop = False
            '
            'CmbTipologia
            '
            Me.CmbTipologia.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CmbTipologia.Font = New System.Drawing.Font("Times New Roman", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CmbTipologia.FormattingEnabled = True
            Me.CmbTipologia.ItemHeight = 12
            Me.CmbTipologia.Location = New System.Drawing.Point(65, 58)
            Me.CmbTipologia.Name = "CmbTipologia"
            Me.CmbTipologia.Size = New System.Drawing.Size(242, 20)
            Me.CmbTipologia.TabIndex = 1
            Me.CmbTipologia.Visible = False
            '
            'CmbDigitaliza
            '
            Me.CmbDigitaliza.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CmbDigitaliza.Font = New System.Drawing.Font("Times New Roman", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CmbDigitaliza.FormattingEnabled = True
            Me.CmbDigitaliza.ItemHeight = 12
            Me.CmbDigitaliza.Items.AddRange(New Object() {"01 CRP CENTRO INTERNACIONAL", "02 CRP DIGITALIZADOR 1", "03 CRP DIGITALIZADOR 2"})
            Me.CmbDigitaliza.Location = New System.Drawing.Point(70, 33)
            Me.CmbDigitaliza.Name = "CmbDigitaliza"
            Me.CmbDigitaliza.Size = New System.Drawing.Size(239, 20)
            Me.CmbDigitaliza.TabIndex = 0
            Me.CmbDigitaliza.Visible = False
            '
            'Label7
            '
            Me.Label7.BackColor = System.Drawing.SystemColors.Control
            Me.Label7.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label7.ForeColor = System.Drawing.Color.Black
            Me.Label7.Location = New System.Drawing.Point(3, 60)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(60, 16)
            Me.Label7.TabIndex = 9
            Me.Label7.Text = "Tipologia"
            Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblTipologia
            '
            Me.lblTipologia.BackColor = System.Drawing.SystemColors.ButtonHighlight
            Me.lblTipologia.Font = New System.Drawing.Font("Times New Roman", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTipologia.ForeColor = System.Drawing.SystemColors.Highlight
            Me.lblTipologia.Location = New System.Drawing.Point(66, 60)
            Me.lblTipologia.Name = "lblTipologia"
            Me.lblTipologia.Size = New System.Drawing.Size(242, 16)
            Me.lblTipologia.TabIndex = 17
            Me.lblTipologia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'BtnDigitalizador
            '
            Me.BtnDigitalizador.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnDigitalizador.Location = New System.Drawing.Point(313, 32)
            Me.BtnDigitalizador.Name = "BtnDigitalizador"
            Me.BtnDigitalizador.Size = New System.Drawing.Size(18, 20)
            Me.BtnDigitalizador.TabIndex = 16
            Me.BtnDigitalizador.Text = "D"
            Me.BtnDigitalizador.UseVisualStyleBackColor = True
            '
            'LblDigitalizador
            '
            Me.LblDigitalizador.BackColor = System.Drawing.SystemColors.ButtonHighlight
            Me.LblDigitalizador.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblDigitalizador.Location = New System.Drawing.Point(66, 34)
            Me.LblDigitalizador.Name = "LblDigitalizador"
            Me.LblDigitalizador.Size = New System.Drawing.Size(242, 16)
            Me.LblDigitalizador.TabIndex = 15
            Me.LblDigitalizador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Label2
            '
            Me.Label2.BackColor = System.Drawing.SystemColors.Control
            Me.Label2.Font = New System.Drawing.Font("Times New Roman", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(3, 34)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(60, 17)
            Me.Label2.TabIndex = 8
            Me.Label2.Text = "Digitalizador"
            Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'LblNumLote
            '
            Me.LblNumLote.BackColor = System.Drawing.SystemColors.ButtonHighlight
            Me.LblNumLote.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblNumLote.Location = New System.Drawing.Point(4, 12)
            Me.LblNumLote.Name = "LblNumLote"
            Me.LblNumLote.Size = New System.Drawing.Size(327, 16)
            Me.LblNumLote.TabIndex = 7
            Me.LblNumLote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'DtpFecha
            '
            Me.DtpFecha.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.DtpFecha.Location = New System.Drawing.Point(132, 116)
            Me.DtpFecha.Name = "DtpFecha"
            Me.DtpFecha.Size = New System.Drawing.Size(106, 20)
            Me.DtpFecha.TabIndex = 0
            '
            'LblFecha
            '
            Me.LblFecha.BackColor = System.Drawing.SystemColors.ButtonFace
            Me.LblFecha.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblFecha.Location = New System.Drawing.Point(10, 117)
            Me.LblFecha.Name = "LblFecha"
            Me.LblFecha.Size = New System.Drawing.Size(120, 17)
            Me.LblFecha.TabIndex = 8
            Me.LblFecha.Text = "Fecha de Movimiento"
            Me.LblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'LblJornada
            '
            Me.LblJornada.BackColor = System.Drawing.SystemColors.ButtonFace
            Me.LblJornada.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblJornada.Location = New System.Drawing.Point(6, 141)
            Me.LblJornada.Name = "LblJornada"
            Me.LblJornada.Size = New System.Drawing.Size(125, 17)
            Me.LblJornada.TabIndex = 9
            Me.LblJornada.Text = "Tipo de Jornada"
            Me.LblJornada.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'LblStiker
            '
            Me.LblStiker.BackColor = System.Drawing.SystemColors.ButtonFace
            Me.LblStiker.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblStiker.Location = New System.Drawing.Point(6, 166)
            Me.LblStiker.Name = "LblStiker"
            Me.LblStiker.Size = New System.Drawing.Size(125, 17)
            Me.LblStiker.TabIndex = 10
            Me.LblStiker.Text = "Número de Sticker"
            Me.LblStiker.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.LblStiker.Visible = False
            '
            'TxtNumStiker
            '
            Me.TxtNumStiker.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TxtNumStiker.FormatoFecha = TextBoxMask.TextBoxMask.Formato.DDMMAAAA
            Me.TxtNumStiker.Location = New System.Drawing.Point(132, 165)
            Me.TxtNumStiker.MaxLength = 20
            Me.TxtNumStiker.Mayusculas = True
            Me.TxtNumStiker.Name = "TxtNumStiker"
            Me.TxtNumStiker.PermiteNulo = False
            Me.TxtNumStiker.Size = New System.Drawing.Size(153, 20)
            Me.TxtNumStiker.Solo = TextBoxMask.TextBoxMask.Permite.Todos
            Me.TxtNumStiker.TabIndex = 3
            Me.TxtNumStiker.Visible = False
            '
            'TxtNumJornada
            '
            Me.TxtNumJornada.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TxtNumJornada.FormatoFecha = TextBoxMask.TextBoxMask.Formato.DDMMAAAA
            Me.TxtNumJornada.Location = New System.Drawing.Point(132, 140)
            Me.TxtNumJornada.MaxLength = 1
            Me.TxtNumJornada.Mayusculas = True
            Me.TxtNumJornada.Name = "TxtNumJornada"
            Me.TxtNumJornada.PermiteNulo = False
            Me.TxtNumJornada.Size = New System.Drawing.Size(28, 20)
            Me.TxtNumJornada.Solo = TextBoxMask.TextBoxMask.Permite.Entero
            Me.TxtNumJornada.TabIndex = 2
            Me.TxtNumJornada.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'PanEscaner
            '
            Me.PanEscaner.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.PanEscaner.BackColor = System.Drawing.Color.White
            Me.PanEscaner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.PanEscaner.Controls.Add(Me.BtnAddCarpeta)
            Me.PanEscaner.Controls.Add(Me.BtnAddImagen)
            Me.PanEscaner.Controls.Add(Me.DgvRegistrosLote)
            Me.PanEscaner.Controls.Add(Me.LblRegLote)
            Me.PanEscaner.Controls.Add(Me.BtnEscanear)
            Me.PanEscaner.Controls.Add(Me.BtnEliminaPagina)
            Me.PanEscaner.Controls.Add(Me.BtnEliminaLote)
            Me.PanEscaner.Controls.Add(Me.BtnVolver)
            Me.PanEscaner.Location = New System.Drawing.Point(6, 8)
            Me.PanEscaner.Name = "PanEscaner"
            Me.PanEscaner.Size = New System.Drawing.Size(335, 266)
            Me.PanEscaner.TabIndex = 5
            Me.PanEscaner.Visible = False
            '
            'BtnAddCarpeta
            '
            Me.BtnAddCarpeta.BackgroundImage = CType(resources.GetObject("BtnAddCarpeta.BackgroundImage"), System.Drawing.Image)
            Me.BtnAddCarpeta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnAddCarpeta.Enabled = False
            Me.BtnAddCarpeta.FlatAppearance.BorderSize = 0
            Me.BtnAddCarpeta.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnAddCarpeta.Location = New System.Drawing.Point(98, 4)
            Me.BtnAddCarpeta.Name = "BtnAddCarpeta"
            Me.BtnAddCarpeta.Size = New System.Drawing.Size(35, 35)
            Me.BtnAddCarpeta.TabIndex = 44
            Me.BtnAddCarpeta.Tag = "Adiccionar una carpeta"
            Me.BtnAddCarpeta.UseVisualStyleBackColor = True
            '
            'BtnAddImagen
            '
            Me.BtnAddImagen.BackgroundImage = CType(resources.GetObject("BtnAddImagen.BackgroundImage"), System.Drawing.Image)
            Me.BtnAddImagen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnAddImagen.Enabled = False
            Me.BtnAddImagen.FlatAppearance.BorderSize = 0
            Me.BtnAddImagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnAddImagen.Location = New System.Drawing.Point(51, 4)
            Me.BtnAddImagen.Name = "BtnAddImagen"
            Me.BtnAddImagen.Size = New System.Drawing.Size(35, 35)
            Me.BtnAddImagen.TabIndex = 43
            Me.BtnAddImagen.Tag = "Adiccionar un archivo"
            Me.BtnAddImagen.UseVisualStyleBackColor = True
            '
            'DgvRegistrosLote
            '
            Me.DgvRegistrosLote.AllowUserToAddRows = False
            Me.DgvRegistrosLote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.DgvRegistrosLote.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.DgvRegistrosLote.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DgvRegistrosLote.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Ide, Me.Nombre})
            Me.DgvRegistrosLote.Location = New System.Drawing.Point(3, 63)
            Me.DgvRegistrosLote.Name = "DgvRegistrosLote"
            Me.DgvRegistrosLote.ReadOnly = True
            Me.DgvRegistrosLote.Size = New System.Drawing.Size(327, 198)
            Me.DgvRegistrosLote.TabIndex = 20
            Me.DgvRegistrosLote.Tag = "Información de imagenes escaneadas."
            '
            'Ide
            '
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Times New Roman", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Ide.DefaultCellStyle = DataGridViewCellStyle1
            Me.Ide.HeaderText = "Ide"
            Me.Ide.MaxInputLength = 3
            Me.Ide.Name = "Ide"
            Me.Ide.ReadOnly = True
            Me.Ide.Width = 30
            '
            'Nombre
            '
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Nombre.DefaultCellStyle = DataGridViewCellStyle2
            Me.Nombre.HeaderText = "Nombre"
            Me.Nombre.MaxInputLength = 60
            Me.Nombre.Name = "Nombre"
            Me.Nombre.ReadOnly = True
            Me.Nombre.Width = 230
            '
            'LblRegLote
            '
            Me.LblRegLote.BackColor = System.Drawing.SystemColors.GradientActiveCaption
            Me.LblRegLote.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.LblRegLote.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.LblRegLote.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblRegLote.Location = New System.Drawing.Point(3, 47)
            Me.LblRegLote.Name = "LblRegLote"
            Me.LblRegLote.Size = New System.Drawing.Size(327, 16)
            Me.LblRegLote.TabIndex = 19
            Me.LblRegLote.Text = "Documentos Digitalizados."
            Me.LblRegLote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'BtnEscanear
            '
            Me.BtnEscanear.BackgroundImage = CType(resources.GetObject("BtnEscanear.BackgroundImage"), System.Drawing.Image)
            Me.BtnEscanear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnEscanear.FlatAppearance.BorderColor = System.Drawing.Color.White
            Me.BtnEscanear.FlatAppearance.BorderSize = 0
            Me.BtnEscanear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnEscanear.Location = New System.Drawing.Point(10, 4)
            Me.BtnEscanear.Name = "BtnEscanear"
            Me.BtnEscanear.Size = New System.Drawing.Size(35, 35)
            Me.BtnEscanear.TabIndex = 40
            Me.BtnEscanear.Tag = "Inicia el proceso de escaneo."
            Me.BtnEscanear.UseVisualStyleBackColor = True
            '
            'BtnEliminaPagina
            '
            Me.BtnEliminaPagina.BackgroundImage = CType(resources.GetObject("BtnEliminaPagina.BackgroundImage"), System.Drawing.Image)
            Me.BtnEliminaPagina.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnEliminaPagina.Enabled = False
            Me.BtnEliminaPagina.FlatAppearance.BorderSize = 0
            Me.BtnEliminaPagina.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnEliminaPagina.Location = New System.Drawing.Point(146, 4)
            Me.BtnEliminaPagina.Name = "BtnEliminaPagina"
            Me.BtnEliminaPagina.Size = New System.Drawing.Size(35, 35)
            Me.BtnEliminaPagina.TabIndex = 35
            Me.BtnEliminaPagina.Tag = "Eliminar documento activo"
            Me.BtnEliminaPagina.UseVisualStyleBackColor = True
            '
            'BtnEliminaLote
            '
            Me.BtnEliminaLote.BackgroundImage = CType(resources.GetObject("BtnEliminaLote.BackgroundImage"), System.Drawing.Image)
            Me.BtnEliminaLote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnEliminaLote.FlatAppearance.BorderColor = System.Drawing.Color.White
            Me.BtnEliminaLote.FlatAppearance.BorderSize = 0
            Me.BtnEliminaLote.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnEliminaLote.Location = New System.Drawing.Point(196, 4)
            Me.BtnEliminaLote.Name = "BtnEliminaLote"
            Me.BtnEliminaLote.Size = New System.Drawing.Size(35, 35)
            Me.BtnEliminaLote.TabIndex = 37
            Me.BtnEliminaLote.Tag = "Eliminar lote."
            Me.BtnEliminaLote.UseVisualStyleBackColor = True
            '
            'BtnVolver
            '
            Me.BtnVolver.BackgroundImage = CType(resources.GetObject("BtnVolver.BackgroundImage"), System.Drawing.Image)
            Me.BtnVolver.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnVolver.FlatAppearance.BorderColor = System.Drawing.Color.White
            Me.BtnVolver.FlatAppearance.BorderSize = 0
            Me.BtnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnVolver.ForeColor = System.Drawing.SystemColors.Window
            Me.BtnVolver.Location = New System.Drawing.Point(289, 4)
            Me.BtnVolver.Name = "BtnVolver"
            Me.BtnVolver.Size = New System.Drawing.Size(35, 35)
            Me.BtnVolver.TabIndex = 42
            Me.BtnVolver.Tag = "Volver a opciones principales."
            Me.BtnVolver.UseVisualStyleBackColor = True
            '
            'TxtNomJornada
            '
            Me.TxtNomJornada.Enabled = False
            Me.TxtNomJornada.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TxtNomJornada.FormatoFecha = TextBoxMask.TextBoxMask.Formato.DDMMAAAA
            Me.TxtNomJornada.Location = New System.Drawing.Point(164, 140)
            Me.TxtNomJornada.MaxLength = 8
            Me.TxtNomJornada.Mayusculas = True
            Me.TxtNomJornada.Name = "TxtNomJornada"
            Me.TxtNomJornada.PermiteNulo = False
            Me.TxtNomJornada.Size = New System.Drawing.Size(177, 20)
            Me.TxtNomJornada.Solo = TextBoxMask.TextBoxMask.Permite.Todos
            Me.TxtNomJornada.TabIndex = 11
            '
            'FLPImagenes
            '
            Me.FLPImagenes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.FLPImagenes.AutoScroll = True
            Me.FLPImagenes.BackColor = System.Drawing.Color.White
            Me.FLPImagenes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.FLPImagenes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.FLPImagenes.Location = New System.Drawing.Point(356, 5)
            Me.FLPImagenes.Name = "FLPImagenes"
            Me.FLPImagenes.Size = New System.Drawing.Size(130, 1035)
            Me.FLPImagenes.TabIndex = 30
            '
            'BtnServidor
            '
            Me.BtnServidor.BackgroundImage = CType(resources.GetObject("BtnServidor.BackgroundImage"), System.Drawing.Image)
            Me.BtnServidor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnServidor.FlatAppearance.BorderSize = 0
            Me.BtnServidor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnServidor.Location = New System.Drawing.Point(58, 280)
            Me.BtnServidor.Name = "BtnServidor"
            Me.BtnServidor.Size = New System.Drawing.Size(35, 35)
            Me.BtnServidor.TabIndex = 41
            Me.BtnServidor.Tag = "??"
            Me.BtnServidor.UseVisualStyleBackColor = True
            '
            'BtnDigitalizar
            '
            Me.BtnDigitalizar.BackgroundImage = CType(resources.GetObject("BtnDigitalizar.BackgroundImage"), System.Drawing.Image)
            Me.BtnDigitalizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnDigitalizar.Enabled = False
            Me.BtnDigitalizar.FlatAppearance.BorderSize = 0
            Me.BtnDigitalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnDigitalizar.Location = New System.Drawing.Point(153, 280)
            Me.BtnDigitalizar.Name = "BtnDigitalizar"
            Me.BtnDigitalizar.Size = New System.Drawing.Size(35, 35)
            Me.BtnDigitalizar.TabIndex = 43
            Me.BtnDigitalizar.Tag = "Habilitar botones de escaneo."
            Me.BtnDigitalizar.UseVisualStyleBackColor = True
            '
            'BtnExportarLotes
            '
            Me.BtnExportarLotes.BackgroundImage = CType(resources.GetObject("BtnExportarLotes.BackgroundImage"), System.Drawing.Image)
            Me.BtnExportarLotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnExportarLotes.Enabled = False
            Me.BtnExportarLotes.FlatAppearance.BorderSize = 0
            Me.BtnExportarLotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnExportarLotes.Location = New System.Drawing.Point(199, 280)
            Me.BtnExportarLotes.Name = "BtnExportarLotes"
            Me.BtnExportarLotes.Size = New System.Drawing.Size(35, 35)
            Me.BtnExportarLotes.TabIndex = 44
            Me.BtnExportarLotes.Tag = "Exportar lotes escaneados."
            Me.BtnExportarLotes.UseVisualStyleBackColor = True
            '
            'BtnCrearCarpeta
            '
            Me.BtnCrearCarpeta.BackgroundImage = CType(resources.GetObject("BtnCrearCarpeta.BackgroundImage"), System.Drawing.Image)
            Me.BtnCrearCarpeta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnCrearCarpeta.Enabled = False
            Me.BtnCrearCarpeta.FlatAppearance.BorderSize = 0
            Me.BtnCrearCarpeta.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnCrearCarpeta.Location = New System.Drawing.Point(105, 280)
            Me.BtnCrearCarpeta.Name = "BtnCrearCarpeta"
            Me.BtnCrearCarpeta.Size = New System.Drawing.Size(35, 35)
            Me.BtnCrearCarpeta.TabIndex = 45
            Me.BtnCrearCarpeta.Tag = "Crer carpetas para el nuevo lote."
            Me.BtnCrearCarpeta.UseVisualStyleBackColor = True
            '
            'BtnNewCarpeta
            '
            Me.BtnNewCarpeta.BackgroundImage = CType(resources.GetObject("BtnNewCarpeta.BackgroundImage"), System.Drawing.Image)
            Me.BtnNewCarpeta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnNewCarpeta.FlatAppearance.BorderSize = 0
            Me.BtnNewCarpeta.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnNewCarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnNewCarpeta.Location = New System.Drawing.Point(6, 280)
            Me.BtnNewCarpeta.Name = "BtnNewCarpeta"
            Me.BtnNewCarpeta.Size = New System.Drawing.Size(35, 35)
            Me.BtnNewCarpeta.TabIndex = 0
            Me.BtnNewCarpeta.Tag = "Crear un nuevo lote  de escaneo."
            Me.BtnNewCarpeta.TextAlign = System.Drawing.ContentAlignment.TopCenter
            Me.BtnNewCarpeta.UseVisualStyleBackColor = True
            '
            'PicImagen
            '
            Me.PicImagen.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PicImagen.BackColor = System.Drawing.Color.White
            Me.PicImagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.PicImagen.ErrorImage = Nothing
            Me.PicImagen.InitialImage = Nothing
            Me.PicImagen.Location = New System.Drawing.Point(7, 3)
            Me.PicImagen.Name = "PicImagen"
            Me.PicImagen.Size = New System.Drawing.Size(2038, 899)
            Me.PicImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PicImagen.TabIndex = 26
            Me.PicImagen.TabStop = False
            '
            'BtnCerrar
            '
            Me.BtnCerrar.BackgroundImage = CType(resources.GetObject("BtnCerrar.BackgroundImage"), System.Drawing.Image)
            Me.BtnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnCerrar.FlatAppearance.BorderSize = 0
            Me.BtnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnCerrar.Location = New System.Drawing.Point(299, 280)
            Me.BtnCerrar.Name = "BtnCerrar"
            Me.BtnCerrar.Size = New System.Drawing.Size(35, 35)
            Me.BtnCerrar.TabIndex = 44
            Me.BtnCerrar.Tag = "Salir de la aplicación"
            Me.BtnCerrar.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.BackColor = System.Drawing.Color.WhiteSmoke
            Me.Label1.Location = New System.Drawing.Point(6, 116)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(4, 18)
            Me.Label1.TabIndex = 46
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.DgvCarpetas)
            Me.Panel1.Controls.Add(Me.DtpFecha)
            Me.Panel1.Controls.Add(Me.GBDatosLote)
            Me.Panel1.Controls.Add(Me.Label1)
            Me.Panel1.Controls.Add(Me.LblTitulo)
            Me.Panel1.Controls.Add(Me.LblFecha)
            Me.Panel1.Controls.Add(Me.TxtNomJornada)
            Me.Panel1.Controls.Add(Me.LblJornada)
            Me.Panel1.Controls.Add(Me.LblStiker)
            Me.Panel1.Controls.Add(Me.TxtNumStiker)
            Me.Panel1.Controls.Add(Me.TxtNumJornada)
            Me.Panel1.Location = New System.Drawing.Point(3, 5)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(347, 326)
            Me.Panel1.TabIndex = 49
            '
            'DgvCarpetas
            '
            Me.DgvCarpetas.AllowUserToAddRows = False
            Me.DgvCarpetas.AllowUserToDeleteRows = False
            Me.DgvCarpetas.AllowUserToOrderColumns = True
            Me.DgvCarpetas.AllowUserToResizeColumns = False
            Me.DgvCarpetas.AllowUserToResizeRows = False
            Me.DgvCarpetas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.DgvCarpetas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.DgvCarpetas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DgvCarpetas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DctosImg})
            Me.DgvCarpetas.Location = New System.Drawing.Point(2, 193)
            Me.DgvCarpetas.Name = "DgvCarpetas"
            Me.DgvCarpetas.ReadOnly = True
            Me.DgvCarpetas.RowHeadersVisible = False
            Me.DgvCarpetas.RowHeadersWidth = 35
            Me.DgvCarpetas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DgvCarpetas.Size = New System.Drawing.Size(343, 128)
            Me.DgvCarpetas.TabIndex = 47
            Me.DgvCarpetas.Tag = "Carpetas de series documentales"
            Me.DgvCarpetas.Visible = False
            '
            'DataGridViewTextBoxColumn1
            '
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Times New Roman", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle3
            Me.DataGridViewTextBoxColumn1.HeaderText = "Carpeta (Lote)"
            Me.DataGridViewTextBoxColumn1.MaxInputLength = 3
            Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
            Me.DataGridViewTextBoxColumn1.ReadOnly = True
            Me.DataGridViewTextBoxColumn1.Width = 190
            '
            'DataGridViewTextBoxColumn2
            '
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Times New Roman", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle4
            Me.DataGridViewTextBoxColumn2.HeaderText = "Estado"
            Me.DataGridViewTextBoxColumn2.MaxInputLength = 60
            Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
            Me.DataGridViewTextBoxColumn2.ReadOnly = True
            Me.DataGridViewTextBoxColumn2.Width = 80
            '
            'DctosImg
            '
            Me.DctosImg.HeaderText = "Imgs"
            Me.DctosImg.Name = "DctosImg"
            Me.DctosImg.ReadOnly = True
            Me.DctosImg.Width = 50
            '
            'Panel2
            '
            Me.Panel2.BackColor = System.Drawing.SystemColors.Window
            Me.Panel2.Controls.Add(Me.PanEscaner)
            Me.Panel2.Controls.Add(Me.BtnServidor)
            Me.Panel2.Controls.Add(Me.BtnCrearCarpeta)
            Me.Panel2.Controls.Add(Me.BtnExportarLotes)
            Me.Panel2.Controls.Add(Me.BtnCerrar)
            Me.Panel2.Controls.Add(Me.BtnDigitalizar)
            Me.Panel2.Controls.Add(Me.BtnNewCarpeta)
            Me.Panel2.Location = New System.Drawing.Point(3, 332)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(347, 332)
            Me.Panel2.TabIndex = 50
            '
            'Panel3
            '
            Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Panel3.Controls.Add(Me.Panel5)
            Me.Panel3.Controls.Add(Me.Panel4)
            Me.Panel3.Location = New System.Drawing.Point(492, 5)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(2065, 1035)
            Me.Panel3.TabIndex = 51
            '
            'Panel5
            '
            Me.Panel5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Panel5.AutoScroll = True
            Me.Panel5.BackColor = System.Drawing.Color.Transparent
            Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.Panel5.Controls.Add(Me.PicImagen)
            Me.Panel5.Location = New System.Drawing.Point(7, 44)
            Me.Panel5.Name = "Panel5"
            Me.Panel5.Size = New System.Drawing.Size(2052, 983)
            Me.Panel5.TabIndex = 28
            '
            'Panel4
            '
            Me.Panel4.BackColor = System.Drawing.SystemColors.Window
            Me.Panel4.Controls.Add(Me.BtnAutoTamaño)
            Me.Panel4.Controls.Add(Me.BtnNormal)
            Me.Panel4.Controls.Add(Me.BntGiraDerecha)
            Me.Panel4.Controls.Add(Me.BtnUltimo)
            Me.Panel4.Controls.Add(Me.BtnSiguiente)
            Me.Panel4.Controls.Add(Me.BtnAnterior)
            Me.Panel4.Controls.Add(Me.BntPrimero)
            Me.Panel4.Location = New System.Drawing.Point(7, 9)
            Me.Panel4.Name = "Panel4"
            Me.Panel4.Size = New System.Drawing.Size(748, 30)
            Me.Panel4.TabIndex = 27
            '
            'BtnAutoTamaño
            '
            Me.BtnAutoTamaño.BackgroundImage = CType(resources.GetObject("BtnAutoTamaño.BackgroundImage"), System.Drawing.Image)
            Me.BtnAutoTamaño.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnAutoTamaño.Enabled = False
            Me.BtnAutoTamaño.FlatAppearance.BorderSize = 0
            Me.BtnAutoTamaño.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnAutoTamaño.Location = New System.Drawing.Point(266, 8)
            Me.BtnAutoTamaño.Name = "BtnAutoTamaño"
            Me.BtnAutoTamaño.Size = New System.Drawing.Size(26, 15)
            Me.BtnAutoTamaño.TabIndex = 51
            Me.BtnAutoTamaño.Tag = "Adiccionar un archivo"
            Me.BtnAutoTamaño.Text = "Auto"
            Me.BtnAutoTamaño.UseVisualStyleBackColor = True
            '
            'BtnNormal
            '
            Me.BtnNormal.BackgroundImage = CType(resources.GetObject("BtnNormal.BackgroundImage"), System.Drawing.Image)
            Me.BtnNormal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnNormal.Enabled = False
            Me.BtnNormal.FlatAppearance.BorderSize = 0
            Me.BtnNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnNormal.Location = New System.Drawing.Point(224, 6)
            Me.BtnNormal.Name = "BtnNormal"
            Me.BtnNormal.Size = New System.Drawing.Size(26, 19)
            Me.BtnNormal.TabIndex = 50
            Me.BtnNormal.Tag = "Adiccionar un archivo"
            Me.BtnNormal.Text = "N"
            Me.BtnNormal.UseVisualStyleBackColor = True
            '
            'BntGiraDerecha
            '
            Me.BntGiraDerecha.BackgroundImage = CType(resources.GetObject("BntGiraDerecha.BackgroundImage"), System.Drawing.Image)
            Me.BntGiraDerecha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BntGiraDerecha.Enabled = False
            Me.BntGiraDerecha.FlatAppearance.BorderSize = 0
            Me.BntGiraDerecha.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BntGiraDerecha.Location = New System.Drawing.Point(178, 8)
            Me.BntGiraDerecha.Name = "BntGiraDerecha"
            Me.BntGiraDerecha.Size = New System.Drawing.Size(26, 15)
            Me.BntGiraDerecha.TabIndex = 48
            Me.BntGiraDerecha.Tag = "Adiccionar un archivo"
            Me.BntGiraDerecha.UseVisualStyleBackColor = True
            '
            'BtnUltimo
            '
            Me.BtnUltimo.BackgroundImage = CType(resources.GetObject("BtnUltimo.BackgroundImage"), System.Drawing.Image)
            Me.BtnUltimo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnUltimo.Enabled = False
            Me.BtnUltimo.FlatAppearance.BorderSize = 0
            Me.BtnUltimo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnUltimo.Location = New System.Drawing.Point(132, 8)
            Me.BtnUltimo.Name = "BtnUltimo"
            Me.BtnUltimo.Size = New System.Drawing.Size(26, 15)
            Me.BtnUltimo.TabIndex = 47
            Me.BtnUltimo.Tag = "Adiccionar un archivo"
            Me.BtnUltimo.UseVisualStyleBackColor = True
            '
            'BtnSiguiente
            '
            Me.BtnSiguiente.BackgroundImage = CType(resources.GetObject("BtnSiguiente.BackgroundImage"), System.Drawing.Image)
            Me.BtnSiguiente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnSiguiente.Enabled = False
            Me.BtnSiguiente.FlatAppearance.BorderSize = 0
            Me.BtnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnSiguiente.Location = New System.Drawing.Point(91, 8)
            Me.BtnSiguiente.Name = "BtnSiguiente"
            Me.BtnSiguiente.Size = New System.Drawing.Size(26, 15)
            Me.BtnSiguiente.TabIndex = 46
            Me.BtnSiguiente.Tag = "Adiccionar un archivo"
            Me.BtnSiguiente.UseVisualStyleBackColor = True
            '
            'BtnAnterior
            '
            Me.BtnAnterior.BackgroundImage = CType(resources.GetObject("BtnAnterior.BackgroundImage"), System.Drawing.Image)
            Me.BtnAnterior.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnAnterior.Enabled = False
            Me.BtnAnterior.FlatAppearance.BorderSize = 0
            Me.BtnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnAnterior.Location = New System.Drawing.Point(50, 8)
            Me.BtnAnterior.Name = "BtnAnterior"
            Me.BtnAnterior.Size = New System.Drawing.Size(26, 15)
            Me.BtnAnterior.TabIndex = 45
            Me.BtnAnterior.Tag = "Adiccionar un archivo"
            Me.BtnAnterior.UseVisualStyleBackColor = True
            '
            'BntPrimero
            '
            Me.BntPrimero.BackgroundImage = CType(resources.GetObject("BntPrimero.BackgroundImage"), System.Drawing.Image)
            Me.BntPrimero.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BntPrimero.Enabled = False
            Me.BntPrimero.FlatAppearance.BorderSize = 0
            Me.BntPrimero.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BntPrimero.Location = New System.Drawing.Point(9, 8)
            Me.BntPrimero.Name = "BntPrimero"
            Me.BntPrimero.Size = New System.Drawing.Size(26, 15)
            Me.BntPrimero.TabIndex = 44
            Me.BntPrimero.Tag = "Adiccionar un archivo"
            Me.BntPrimero.UseVisualStyleBackColor = True
            '
            'PicA
            '
            Me.PicA.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PicA.BackColor = System.Drawing.Color.White
            Me.PicA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.PicA.ErrorImage = Nothing
            Me.PicA.InitialImage = Nothing
            Me.PicA.Location = New System.Drawing.Point(499, 639)
            Me.PicA.Name = "PicA"
            Me.PicA.Size = New System.Drawing.Size(1329, 401)
            Me.PicA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PicA.TabIndex = 53
            Me.PicA.TabStop = False
            Me.PicA.Visible = False
            '
            'PicR
            '
            Me.PicR.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PicR.BackColor = System.Drawing.Color.White
            Me.PicR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.PicR.ErrorImage = Nothing
            Me.PicR.InitialImage = Nothing
            Me.PicR.Location = New System.Drawing.Point(532, 639)
            Me.PicR.Name = "PicR"
            Me.PicR.Size = New System.Drawing.Size(1329, 401)
            Me.PicR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PicR.TabIndex = 54
            Me.PicR.TabStop = False
            Me.PicR.Visible = False
            '
            'Frm_Digitaliza
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(1302, 376)
            Me.ControlBox = False
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.FLPImagenes)
            Me.Controls.Add(Me.PicA)
            Me.Controls.Add(Me.PicR)
            Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.KeyPreview = True
            Me.MinimizeBox = False
            Me.Name = "Frm_Digitaliza"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.TopMost = True
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            Me.GBDatosLote.ResumeLayout(False)
            Me.PanEscaner.ResumeLayout(False)
            CType(Me.DgvRegistrosLote, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PicImagen, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            CType(Me.DgvCarpetas, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel2.ResumeLayout(False)
            Me.Panel3.ResumeLayout(False)
            Me.Panel5.ResumeLayout(False)
            Me.Panel4.ResumeLayout(False)
            CType(Me.PicA, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PicR, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents LblTitulo As Label
        Friend WithEvents GBDatosLote As GroupBox
        Friend WithEvents BtnNewCarpeta As Button
        Friend WithEvents LblNumLote As Label
        Friend WithEvents DtpFecha As DateTimePicker
        Friend WithEvents LblDigitalizador As Label
        Friend WithEvents Label2 As Label
        Friend WithEvents Label7 As Label
        Friend WithEvents lblTipologia As Label
        Friend WithEvents LblFecha As Label
        Friend WithEvents LblJornada As Label
        Friend WithEvents LblStiker As Label
        Friend WithEvents TxtNumStiker As TextBoxMask.TextBoxMask
        Friend WithEvents TxtNumJornada As TextBoxMask.TextBoxMask
        Friend WithEvents PicImagen As PictureBox
        Friend WithEvents PanEscaner As Panel
        Friend WithEvents TxtNomJornada As TextBoxMask.TextBoxMask
        Friend WithEvents CmbDigitaliza As ComboBox
        Friend WithEvents CmbTipologia As ComboBox
        Friend WithEvents LblRegLote As Label
        Friend WithEvents DgvRegistrosLote As DataGridView
        Friend WithEvents BtnEliminaPagina As Button
        Friend WithEvents BtnEliminaLote As Button
        Friend WithEvents BtnEscanear As Button
        Private WithEvents FLPImagenes As FlowLayoutPanel
        Friend WithEvents BtnServidor As Button
        Friend WithEvents BtnDigitalizar As Button
        Friend WithEvents BtnExportarLotes As Button
        Friend WithEvents BtnCrearCarpeta As Button
        Friend WithEvents BtnVolver As Button
        Friend WithEvents BtnCerrar As Button
        Friend WithEvents BtnDigitalizador As Button
        Friend WithEvents ToolTip As ToolTip
        Friend WithEvents Label1 As Label
        Friend WithEvents Panel1 As Panel
        Friend WithEvents Panel2 As Panel
        Friend WithEvents Panel3 As Panel
        Friend WithEvents Panel4 As Panel
        Friend WithEvents Panel5 As Panel
        Friend WithEvents BtnAddImagen As Button
        Friend WithEvents BtnAddCarpeta As Button
        Friend WithEvents BtnUltimo As Windows.Forms.Button
        Friend WithEvents BtnSiguiente As Windows.Forms.Button
        Friend WithEvents BtnAnterior As Windows.Forms.Button
        Friend WithEvents BntPrimero As Windows.Forms.Button
        Friend WithEvents BntGiraDerecha As Windows.Forms.Button
        Friend WithEvents BtnNormal As Windows.Forms.Button
        Friend WithEvents DgvCarpetas As Windows.Forms.DataGridView
        Friend WithEvents Ide As Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre As Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
        Friend WithEvents DctosImg As DataGridViewTextBoxColumn
        Friend WithEvents PicA As PictureBox
        Friend WithEvents PicR As PictureBox
        Friend WithEvents BtnAutoTamaño As Button
    End Class

End Namespace
