Imports System.Windows.Forms

Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class UsrDigitaliza
        Inherits System.Windows.Forms.UserControl

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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UsrDigitaliza))
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.CmbTipologia = New System.Windows.Forms.ComboBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.lblTipologia = New System.Windows.Forms.Label()
            Me.LblNumLote = New System.Windows.Forms.Label()
            Me.DgvRegistrosLote = New System.Windows.Forms.DataGridView()
            Me.Ide = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.BtnAddCarpeta = New System.Windows.Forms.Button()
            Me.BtnAddImagen = New System.Windows.Forms.Button()
            Me.BtnEscanear = New System.Windows.Forms.Button()
            Me.BtnEliminaLote = New System.Windows.Forms.Button()
            Me.FLPImagenes = New System.Windows.Forms.FlowLayoutPanel()
            Me.PicImagen = New System.Windows.Forms.PictureBox()
            Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.LblNomOfic = New System.Windows.Forms.Label()
            Me.PnCampos = New System.Windows.Forms.Panel()
            Me.DgvCarpetas = New System.Windows.Forms.DataGridView()
            Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DctosImg = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.SerieD = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.PanImagen = New System.Windows.Forms.Panel()
            Me.PicA = New System.Windows.Forms.PictureBox()
            Me.PicR = New System.Windows.Forms.PictureBox()
            Me.Panel4 = New System.Windows.Forms.Panel()
            Me.lblC_pdf = New System.Windows.Forms.Label()
            Me.BtnGiraIzquieda = New System.Windows.Forms.Button()
            Me.BtnAmpliar = New System.Windows.Forms.Button()
            Me.BtnReducir = New System.Windows.Forms.Button()
            Me.BtnAutoTamaño = New System.Windows.Forms.Button()
            Me.BtnNormal = New System.Windows.Forms.Button()
            Me.BtnEliminaPagina = New System.Windows.Forms.Button()
            Me.BtnGiraDerecha = New System.Windows.Forms.Button()
            Me.BtnUltimo = New System.Windows.Forms.Button()
            Me.BtnSiguiente = New System.Windows.Forms.Button()
            Me.BtnAnterior = New System.Windows.Forms.Button()
            Me.BtnPrimero = New System.Windows.Forms.Button()
            Me.TimerExportar = New System.Windows.Forms.Timer(Me.components)
            Me.Panel3 = New System.Windows.Forms.Panel()
            Me.BtnExportarLotes = New System.Windows.Forms.Button()
            Me.BtnNewCarpeta = New System.Windows.Forms.Button()
            CType(Me.DgvRegistrosLote, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PicImagen, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel1.SuspendLayout()
            CType(Me.DgvCarpetas, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.PanImagen.SuspendLayout()
            CType(Me.PicA, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PicR, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel4.SuspendLayout()
            Me.Panel3.SuspendLayout()
            Me.SuspendLayout()
            '
            'CmbTipologia
            '
            Me.CmbTipologia.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CmbTipologia.Font = New System.Drawing.Font("Times New Roman", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CmbTipologia.FormattingEnabled = True
            Me.CmbTipologia.ItemHeight = 12
            Me.CmbTipologia.Location = New System.Drawing.Point(86, 35)
            Me.CmbTipologia.Name = "CmbTipologia"
            Me.CmbTipologia.Size = New System.Drawing.Size(249, 20)
            Me.CmbTipologia.TabIndex = 1
            Me.CmbTipologia.Visible = False
            '
            'Label7
            '
            Me.Label7.BackColor = System.Drawing.SystemColors.Control
            Me.Label7.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label7.ForeColor = System.Drawing.Color.Black
            Me.Label7.Location = New System.Drawing.Point(6, 31)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(76, 31)
            Me.Label7.TabIndex = 0
            Me.Label7.Text = "Serie Documental"
            Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblTipologia
            '
            Me.lblTipologia.BackColor = System.Drawing.SystemColors.ButtonHighlight
            Me.lblTipologia.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTipologia.ForeColor = System.Drawing.Color.Black
            Me.lblTipologia.Location = New System.Drawing.Point(88, 32)
            Me.lblTipologia.Name = "lblTipologia"
            Me.lblTipologia.Size = New System.Drawing.Size(249, 26)
            Me.lblTipologia.TabIndex = 2
            Me.lblTipologia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'LblNumLote
            '
            Me.LblNumLote.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.LblNumLote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.LblNumLote.Font = New System.Drawing.Font("Times New Roman", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblNumLote.Location = New System.Drawing.Point(9, 1)
            Me.LblNumLote.Name = "LblNumLote"
            Me.LblNumLote.Size = New System.Drawing.Size(331, 27)
            Me.LblNumLote.TabIndex = 7
            Me.LblNumLote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'DgvRegistrosLote
            '
            Me.DgvRegistrosLote.AllowUserToAddRows = False
            Me.DgvRegistrosLote.AllowUserToDeleteRows = False
            Me.DgvRegistrosLote.AllowUserToOrderColumns = True
            Me.DgvRegistrosLote.BackgroundColor = System.Drawing.SystemColors.Control
            Me.DgvRegistrosLote.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.DgvRegistrosLote.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DgvRegistrosLote.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Ide, Me.Nombre})
            Me.DgvRegistrosLote.Location = New System.Drawing.Point(3, 396)
            Me.DgvRegistrosLote.Name = "DgvRegistrosLote"
            Me.DgvRegistrosLote.ReadOnly = True
            Me.DgvRegistrosLote.Size = New System.Drawing.Size(347, 165)
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
            Me.Ide.Width = 40
            '
            'Nombre
            '
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Nombre.DefaultCellStyle = DataGridViewCellStyle2
            Me.Nombre.HeaderText = "Nombre"
            Me.Nombre.MaxInputLength = 60
            Me.Nombre.Name = "Nombre"
            Me.Nombre.ReadOnly = True
            Me.Nombre.Width = 640
            '
            'BtnAddCarpeta
            '
            Me.BtnAddCarpeta.BackColor = System.Drawing.Color.White
            Me.BtnAddCarpeta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnAddCarpeta.FlatAppearance.BorderSize = 0
            Me.BtnAddCarpeta.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnAddCarpeta.ForeColor = System.Drawing.SystemColors.ControlText
            Me.BtnAddCarpeta.Image = CType(resources.GetObject("BtnAddCarpeta.Image"), System.Drawing.Image)
            Me.BtnAddCarpeta.Location = New System.Drawing.Point(90, 1)
            Me.BtnAddCarpeta.Name = "BtnAddCarpeta"
            Me.BtnAddCarpeta.Size = New System.Drawing.Size(35, 35)
            Me.BtnAddCarpeta.TabIndex = 44
            Me.BtnAddCarpeta.Tag = "Adiccionar una carpeta con imagenes"
            Me.BtnAddCarpeta.UseVisualStyleBackColor = False
            '
            'BtnAddImagen
            '
            Me.BtnAddImagen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnAddImagen.FlatAppearance.BorderSize = 0
            Me.BtnAddImagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnAddImagen.Image = CType(resources.GetObject("BtnAddImagen.Image"), System.Drawing.Image)
            Me.BtnAddImagen.Location = New System.Drawing.Point(46, 1)
            Me.BtnAddImagen.Name = "BtnAddImagen"
            Me.BtnAddImagen.Size = New System.Drawing.Size(35, 35)
            Me.BtnAddImagen.TabIndex = 43
            Me.BtnAddImagen.Tag = "Adiccionar un archivo de imagen"
            Me.BtnAddImagen.UseVisualStyleBackColor = True
            '
            'BtnEscanear
            '
            Me.BtnEscanear.BackColor = System.Drawing.Color.White
            Me.BtnEscanear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnEscanear.FlatAppearance.BorderColor = System.Drawing.Color.White
            Me.BtnEscanear.FlatAppearance.BorderSize = 0
            Me.BtnEscanear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnEscanear.Image = CType(resources.GetObject("BtnEscanear.Image"), System.Drawing.Image)
            Me.BtnEscanear.Location = New System.Drawing.Point(6, 1)
            Me.BtnEscanear.Name = "BtnEscanear"
            Me.BtnEscanear.Size = New System.Drawing.Size(35, 35)
            Me.BtnEscanear.TabIndex = 40
            Me.BtnEscanear.Tag = "Inicia el proceso de escaneo."
            Me.BtnEscanear.UseVisualStyleBackColor = False
            '
            'BtnEliminaLote
            '
            Me.BtnEliminaLote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnEliminaLote.FlatAppearance.BorderColor = System.Drawing.Color.White
            Me.BtnEliminaLote.FlatAppearance.BorderSize = 0
            Me.BtnEliminaLote.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnEliminaLote.Image = CType(resources.GetObject("BtnEliminaLote.Image"), System.Drawing.Image)
            Me.BtnEliminaLote.Location = New System.Drawing.Point(136, 1)
            Me.BtnEliminaLote.Name = "BtnEliminaLote"
            Me.BtnEliminaLote.Size = New System.Drawing.Size(35, 35)
            Me.BtnEliminaLote.TabIndex = 37
            Me.BtnEliminaLote.Tag = "Eliminar lote."
            Me.BtnEliminaLote.UseVisualStyleBackColor = True
            '
            'FLPImagenes
            '
            Me.FLPImagenes.AutoScroll = True
            Me.FLPImagenes.BackColor = System.Drawing.Color.White
            Me.FLPImagenes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.FLPImagenes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.FLPImagenes.Location = New System.Drawing.Point(356, 5)
            Me.FLPImagenes.Name = "FLPImagenes"
            Me.FLPImagenes.Size = New System.Drawing.Size(130, 557)
            Me.FLPImagenes.TabIndex = 30
            '
            'PicImagen
            '
            Me.PicImagen.BackColor = System.Drawing.Color.White
            Me.PicImagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.PicImagen.ErrorImage = Nothing
            Me.PicImagen.InitialImage = Nothing
            Me.PicImagen.Location = New System.Drawing.Point(3, 4)
            Me.PicImagen.Name = "PicImagen"
            Me.PicImagen.Size = New System.Drawing.Size(720, 800)
            Me.PicImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.PicImagen.TabIndex = 26
            Me.PicImagen.TabStop = False
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.LblNomOfic)
            Me.Panel1.Controls.Add(Me.PnCampos)
            Me.Panel1.Controls.Add(Me.CmbTipologia)
            Me.Panel1.Controls.Add(Me.Label7)
            Me.Panel1.Controls.Add(Me.lblTipologia)
            Me.Panel1.Controls.Add(Me.LblNumLote)
            Me.Panel1.Controls.Add(Me.DgvCarpetas)
            Me.Panel1.Location = New System.Drawing.Point(3, 5)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(347, 344)
            Me.Panel1.TabIndex = 0
            '
            'LblNomOfic
            '
            Me.LblNomOfic.BackColor = System.Drawing.SystemColors.Control
            Me.LblNomOfic.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblNomOfic.ForeColor = System.Drawing.Color.Black
            Me.LblNomOfic.Location = New System.Drawing.Point(3, 65)
            Me.LblNomOfic.Name = "LblNomOfic"
            Me.LblNomOfic.Size = New System.Drawing.Size(341, 16)
            Me.LblNomOfic.TabIndex = 57
            Me.LblNomOfic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'PnCampos
            '
            Me.PnCampos.AutoScroll = True
            Me.PnCampos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.PnCampos.Location = New System.Drawing.Point(3, 84)
            Me.PnCampos.Name = "PnCampos"
            Me.PnCampos.Size = New System.Drawing.Size(342, 109)
            Me.PnCampos.TabIndex = 9
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
            Me.DgvCarpetas.BackgroundColor = System.Drawing.SystemColors.Control
            Me.DgvCarpetas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.DgvCarpetas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
            Me.DgvCarpetas.ColumnHeadersHeight = 20
            Me.DgvCarpetas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            Me.DgvCarpetas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DctosImg, Me.SerieD})
            Me.DgvCarpetas.Location = New System.Drawing.Point(2, 199)
            Me.DgvCarpetas.Name = "DgvCarpetas"
            Me.DgvCarpetas.ReadOnly = True
            Me.DgvCarpetas.RowHeadersVisible = False
            Me.DgvCarpetas.RowHeadersWidth = 35
            Me.DgvCarpetas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DgvCarpetas.Size = New System.Drawing.Size(343, 141)
            Me.DgvCarpetas.TabIndex = 8
            Me.DgvCarpetas.Tag = "Carpetas de series documentales"
            Me.DgvCarpetas.Visible = False
            '
            'DataGridViewTextBoxColumn1
            '
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Times New Roman", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle3
            Me.DataGridViewTextBoxColumn1.FillWeight = 10.0!
            Me.DataGridViewTextBoxColumn1.HeaderText = "Carpeta (Lote)"
            Me.DataGridViewTextBoxColumn1.MaxInputLength = 3
            Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
            Me.DataGridViewTextBoxColumn1.ReadOnly = True
            Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DataGridViewTextBoxColumn1.Width = 120
            '
            'DataGridViewTextBoxColumn2
            '
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Times New Roman", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle4
            Me.DataGridViewTextBoxColumn2.FillWeight = 10.0!
            Me.DataGridViewTextBoxColumn2.HeaderText = "Estado"
            Me.DataGridViewTextBoxColumn2.MaxInputLength = 60
            Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
            Me.DataGridViewTextBoxColumn2.ReadOnly = True
            Me.DataGridViewTextBoxColumn2.Width = 80
            '
            'DctosImg
            '
            Me.DctosImg.FillWeight = 10.0!
            Me.DctosImg.HeaderText = "Imgs"
            Me.DctosImg.Name = "DctosImg"
            Me.DctosImg.ReadOnly = True
            Me.DctosImg.Width = 40
            '
            'SerieD
            '
            Me.SerieD.HeaderText = "Serie Documental"
            Me.SerieD.Name = "SerieD"
            Me.SerieD.ReadOnly = True
            Me.SerieD.Width = 350
            '
            'PanImagen
            '
            Me.PanImagen.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PanImagen.AutoScroll = True
            Me.PanImagen.BackColor = System.Drawing.Color.Transparent
            Me.PanImagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.PanImagen.Controls.Add(Me.PicImagen)
            Me.PanImagen.Location = New System.Drawing.Point(492, 5)
            Me.PanImagen.Name = "PanImagen"
            Me.PanImagen.Size = New System.Drawing.Size(763, 516)
            Me.PanImagen.TabIndex = 28
            '
            'PicA
            '
            Me.PicA.BackColor = System.Drawing.Color.White
            Me.PicA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.PicA.ErrorImage = Nothing
            Me.PicA.InitialImage = Nothing
            Me.PicA.Location = New System.Drawing.Point(501, 531)
            Me.PicA.Name = "PicA"
            Me.PicA.Size = New System.Drawing.Size(32, 23)
            Me.PicA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PicA.TabIndex = 53
            Me.PicA.TabStop = False
            Me.PicA.Visible = False
            '
            'PicR
            '
            Me.PicR.BackColor = System.Drawing.Color.White
            Me.PicR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.PicR.ErrorImage = Nothing
            Me.PicR.InitialImage = Nothing
            Me.PicR.Location = New System.Drawing.Point(542, 531)
            Me.PicR.Name = "PicR"
            Me.PicR.Size = New System.Drawing.Size(32, 23)
            Me.PicR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PicR.TabIndex = 54
            Me.PicR.TabStop = False
            Me.PicR.Visible = False
            '
            'Panel4
            '
            Me.Panel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Panel4.BackColor = System.Drawing.SystemColors.Window
            Me.Panel4.Controls.Add(Me.lblC_pdf)
            Me.Panel4.Controls.Add(Me.BtnGiraIzquieda)
            Me.Panel4.Controls.Add(Me.BtnAmpliar)
            Me.Panel4.Controls.Add(Me.BtnReducir)
            Me.Panel4.Controls.Add(Me.BtnAutoTamaño)
            Me.Panel4.Controls.Add(Me.BtnNormal)
            Me.Panel4.Controls.Add(Me.BtnEliminaPagina)
            Me.Panel4.Controls.Add(Me.BtnGiraDerecha)
            Me.Panel4.Controls.Add(Me.BtnUltimo)
            Me.Panel4.Controls.Add(Me.BtnSiguiente)
            Me.Panel4.Controls.Add(Me.BtnAnterior)
            Me.Panel4.Controls.Add(Me.BtnPrimero)
            Me.Panel4.Location = New System.Drawing.Point(492, 524)
            Me.Panel4.Name = "Panel4"
            Me.Panel4.Size = New System.Drawing.Size(763, 37)
            Me.Panel4.TabIndex = 52
            '
            'lblC_pdf
            '
            Me.lblC_pdf.BackColor = System.Drawing.SystemColors.ControlLightLight
            Me.lblC_pdf.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblC_pdf.ForeColor = System.Drawing.Color.Black
            Me.lblC_pdf.Location = New System.Drawing.Point(3, 7)
            Me.lblC_pdf.Name = "lblC_pdf"
            Me.lblC_pdf.Size = New System.Drawing.Size(97, 26)
            Me.lblC_pdf.TabIndex = 53
            Me.lblC_pdf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.lblC_pdf.Visible = False
            '
            'BtnGiraIzquieda
            '
            Me.BtnGiraIzquieda.BackgroundImage = CType(resources.GetObject("BtnGiraIzquieda.BackgroundImage"), System.Drawing.Image)
            Me.BtnGiraIzquieda.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnGiraIzquieda.FlatAppearance.BorderSize = 0
            Me.BtnGiraIzquieda.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnGiraIzquieda.Location = New System.Drawing.Point(302, 5)
            Me.BtnGiraIzquieda.Name = "BtnGiraIzquieda"
            Me.BtnGiraIzquieda.Size = New System.Drawing.Size(30, 25)
            Me.BtnGiraIzquieda.TabIndex = 52
            Me.BtnGiraIzquieda.Tag = "Girar imagen"
            Me.BtnGiraIzquieda.UseVisualStyleBackColor = True
            '
            'BtnAmpliar
            '
            Me.BtnAmpliar.BackgroundImage = CType(resources.GetObject("BtnAmpliar.BackgroundImage"), System.Drawing.Image)
            Me.BtnAmpliar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnAmpliar.FlatAppearance.BorderSize = 0
            Me.BtnAmpliar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnAmpliar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnAmpliar.Location = New System.Drawing.Point(547, 5)
            Me.BtnAmpliar.Name = "BtnAmpliar"
            Me.BtnAmpliar.Size = New System.Drawing.Size(30, 25)
            Me.BtnAmpliar.TabIndex = 51
            Me.BtnAmpliar.Tag = "Ampliar imagen"
            Me.BtnAmpliar.UseVisualStyleBackColor = True
            '
            'BtnReducir
            '
            Me.BtnReducir.BackgroundImage = CType(resources.GetObject("BtnReducir.BackgroundImage"), System.Drawing.Image)
            Me.BtnReducir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnReducir.FlatAppearance.BorderSize = 0
            Me.BtnReducir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnReducir.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnReducir.Location = New System.Drawing.Point(498, 5)
            Me.BtnReducir.Name = "BtnReducir"
            Me.BtnReducir.Size = New System.Drawing.Size(30, 25)
            Me.BtnReducir.TabIndex = 51
            Me.BtnReducir.Tag = "Reducir imagen"
            Me.BtnReducir.UseVisualStyleBackColor = True
            '
            'BtnAutoTamaño
            '
            Me.BtnAutoTamaño.BackgroundImage = CType(resources.GetObject("BtnAutoTamaño.BackgroundImage"), System.Drawing.Image)
            Me.BtnAutoTamaño.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnAutoTamaño.FlatAppearance.BorderSize = 0
            Me.BtnAutoTamaño.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnAutoTamaño.Location = New System.Drawing.Point(449, 5)
            Me.BtnAutoTamaño.Name = "BtnAutoTamaño"
            Me.BtnAutoTamaño.Size = New System.Drawing.Size(30, 25)
            Me.BtnAutoTamaño.TabIndex = 51
            Me.BtnAutoTamaño.Tag = "Auto tamaño de imagen"
            Me.BtnAutoTamaño.UseVisualStyleBackColor = True
            '
            'BtnNormal
            '
            Me.BtnNormal.BackgroundImage = CType(resources.GetObject("BtnNormal.BackgroundImage"), System.Drawing.Image)
            Me.BtnNormal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnNormal.FlatAppearance.BorderSize = 0
            Me.BtnNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnNormal.Location = New System.Drawing.Point(400, 5)
            Me.BtnNormal.Name = "BtnNormal"
            Me.BtnNormal.Size = New System.Drawing.Size(30, 25)
            Me.BtnNormal.TabIndex = 50
            Me.BtnNormal.Tag = "Imagen normal"
            Me.BtnNormal.UseVisualStyleBackColor = True
            '
            'BtnEliminaPagina
            '
            Me.BtnEliminaPagina.BackgroundImage = CType(resources.GetObject("BtnEliminaPagina.BackgroundImage"), System.Drawing.Image)
            Me.BtnEliminaPagina.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnEliminaPagina.Enabled = False
            Me.BtnEliminaPagina.FlatAppearance.BorderSize = 0
            Me.BtnEliminaPagina.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnEliminaPagina.Location = New System.Drawing.Point(596, 5)
            Me.BtnEliminaPagina.Name = "BtnEliminaPagina"
            Me.BtnEliminaPagina.Size = New System.Drawing.Size(30, 25)
            Me.BtnEliminaPagina.TabIndex = 35
            Me.BtnEliminaPagina.Tag = "Eliminar documento activo"
            Me.BtnEliminaPagina.UseVisualStyleBackColor = True
            '
            'BtnGiraDerecha
            '
            Me.BtnGiraDerecha.BackgroundImage = CType(resources.GetObject("BtnGiraDerecha.BackgroundImage"), System.Drawing.Image)
            Me.BtnGiraDerecha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnGiraDerecha.FlatAppearance.BorderSize = 0
            Me.BtnGiraDerecha.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnGiraDerecha.Location = New System.Drawing.Point(351, 5)
            Me.BtnGiraDerecha.Name = "BtnGiraDerecha"
            Me.BtnGiraDerecha.Size = New System.Drawing.Size(30, 25)
            Me.BtnGiraDerecha.TabIndex = 48
            Me.BtnGiraDerecha.Tag = "Girar imagen"
            Me.BtnGiraDerecha.UseVisualStyleBackColor = True
            '
            'BtnUltimo
            '
            Me.BtnUltimo.BackgroundImage = CType(resources.GetObject("BtnUltimo.BackgroundImage"), System.Drawing.Image)
            Me.BtnUltimo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnUltimo.FlatAppearance.BorderSize = 0
            Me.BtnUltimo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnUltimo.Location = New System.Drawing.Point(253, 5)
            Me.BtnUltimo.Name = "BtnUltimo"
            Me.BtnUltimo.Size = New System.Drawing.Size(30, 25)
            Me.BtnUltimo.TabIndex = 47
            Me.BtnUltimo.Tag = "Ultima imagen"
            Me.BtnUltimo.UseVisualStyleBackColor = True
            '
            'BtnSiguiente
            '
            Me.BtnSiguiente.BackgroundImage = CType(resources.GetObject("BtnSiguiente.BackgroundImage"), System.Drawing.Image)
            Me.BtnSiguiente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnSiguiente.FlatAppearance.BorderSize = 0
            Me.BtnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnSiguiente.Location = New System.Drawing.Point(204, 5)
            Me.BtnSiguiente.Name = "BtnSiguiente"
            Me.BtnSiguiente.Size = New System.Drawing.Size(30, 25)
            Me.BtnSiguiente.TabIndex = 46
            Me.BtnSiguiente.Tag = "Imagen siguiente"
            Me.BtnSiguiente.UseVisualStyleBackColor = True
            '
            'BtnAnterior
            '
            Me.BtnAnterior.BackgroundImage = CType(resources.GetObject("BtnAnterior.BackgroundImage"), System.Drawing.Image)
            Me.BtnAnterior.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnAnterior.FlatAppearance.BorderSize = 0
            Me.BtnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnAnterior.Location = New System.Drawing.Point(155, 5)
            Me.BtnAnterior.Name = "BtnAnterior"
            Me.BtnAnterior.Size = New System.Drawing.Size(30, 25)
            Me.BtnAnterior.TabIndex = 45
            Me.BtnAnterior.Tag = "Imagen anterior"
            Me.BtnAnterior.UseVisualStyleBackColor = True
            '
            'BtnPrimero
            '
            Me.BtnPrimero.BackgroundImage = CType(resources.GetObject("BtnPrimero.BackgroundImage"), System.Drawing.Image)
            Me.BtnPrimero.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnPrimero.FlatAppearance.BorderSize = 0
            Me.BtnPrimero.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnPrimero.Location = New System.Drawing.Point(106, 5)
            Me.BtnPrimero.Name = "BtnPrimero"
            Me.BtnPrimero.Size = New System.Drawing.Size(30, 25)
            Me.BtnPrimero.TabIndex = 44
            Me.BtnPrimero.Tag = "Primera Imagen"
            Me.BtnPrimero.UseVisualStyleBackColor = True
            '
            'TimerExportar
            '
            Me.TimerExportar.Interval = 30000
            '
            'Panel3
            '
            Me.Panel3.BackColor = System.Drawing.SystemColors.Window
            Me.Panel3.Controls.Add(Me.BtnEscanear)
            Me.Panel3.Controls.Add(Me.BtnExportarLotes)
            Me.Panel3.Controls.Add(Me.BtnNewCarpeta)
            Me.Panel3.Controls.Add(Me.BtnAddCarpeta)
            Me.Panel3.Controls.Add(Me.BtnEliminaLote)
            Me.Panel3.Controls.Add(Me.BtnAddImagen)
            Me.Panel3.Location = New System.Drawing.Point(3, 352)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(347, 41)
            Me.Panel3.TabIndex = 55
            '
            'BtnExportarLotes
            '
            Me.BtnExportarLotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnExportarLotes.Enabled = False
            Me.BtnExportarLotes.FlatAppearance.BorderSize = 0
            Me.BtnExportarLotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnExportarLotes.Image = CType(resources.GetObject("BtnExportarLotes.Image"), System.Drawing.Image)
            Me.BtnExportarLotes.Location = New System.Drawing.Point(306, 1)
            Me.BtnExportarLotes.Name = "BtnExportarLotes"
            Me.BtnExportarLotes.Size = New System.Drawing.Size(35, 35)
            Me.BtnExportarLotes.TabIndex = 44
            Me.BtnExportarLotes.Tag = "Exportar lotes escaneados."
            Me.BtnExportarLotes.UseVisualStyleBackColor = True
            '
            'BtnNewCarpeta
            '
            Me.BtnNewCarpeta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.BtnNewCarpeta.FlatAppearance.BorderSize = 0
            Me.BtnNewCarpeta.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BtnNewCarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BtnNewCarpeta.Image = CType(resources.GetObject("BtnNewCarpeta.Image"), System.Drawing.Image)
            Me.BtnNewCarpeta.Location = New System.Drawing.Point(262, 1)
            Me.BtnNewCarpeta.Name = "BtnNewCarpeta"
            Me.BtnNewCarpeta.Size = New System.Drawing.Size(35, 35)
            Me.BtnNewCarpeta.TabIndex = 0
            Me.BtnNewCarpeta.Tag = "Crear un nuevo lote  de escaneo."
            Me.BtnNewCarpeta.TextAlign = System.Drawing.ContentAlignment.TopCenter
            Me.BtnNewCarpeta.UseVisualStyleBackColor = True
            '
            'UsrDigitaliza
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.Controls.Add(Me.DgvRegistrosLote)
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.PanImagen)
            Me.Controls.Add(Me.Panel4)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.FLPImagenes)
            Me.Controls.Add(Me.PicA)
            Me.Controls.Add(Me.PicR)
            Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Name = "UsrDigitaliza"
            Me.Size = New System.Drawing.Size(1263, 573)
            CType(Me.DgvRegistrosLote, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PicImagen, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel1.ResumeLayout(False)
            CType(Me.DgvCarpetas, System.ComponentModel.ISupportInitialize).EndInit()
            Me.PanImagen.ResumeLayout(False)
            CType(Me.PicA, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PicR, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel4.ResumeLayout(False)
            Me.Panel3.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents LblNumLote As Label
        Friend WithEvents Label7 As Label
        Friend WithEvents lblTipologia As Label
        Friend WithEvents PicImagen As PictureBox
        Friend WithEvents CmbTipologia As ComboBox
        Friend WithEvents DgvRegistrosLote As DataGridView
        Friend WithEvents BtnEliminaLote As Button
        Friend WithEvents BtnEscanear As Button
        Private WithEvents FLPImagenes As FlowLayoutPanel
        Friend WithEvents ToolTip As ToolTip
        Friend WithEvents Panel1 As Panel
        Friend WithEvents PanImagen As Panel
        Friend WithEvents BtnAddImagen As Button
        Friend WithEvents BtnAddCarpeta As Button
        Public WithEvents DgvCarpetas As DataGridView
        Friend WithEvents PicA As PictureBox
        Friend WithEvents PicR As PictureBox
        Friend WithEvents BtnPrimero As Button
        Friend WithEvents BtnAnterior As Button
        Friend WithEvents BtnSiguiente As Button
        Friend WithEvents BtnUltimo As Button
        Friend WithEvents BtnGiraDerecha As Button
        Friend WithEvents BtnEliminaPagina As Button
        Friend WithEvents BtnNormal As Button
        Friend WithEvents BtnAutoTamaño As Button
        Friend WithEvents BtnReducir As Button
        Friend WithEvents BtnAmpliar As Button
        Friend WithEvents Panel4 As Panel
        Friend WithEvents PnCampos As Panel
        Friend WithEvents LblNomOfic As Label
        Friend WithEvents BtnGiraIzquieda As Button
        Friend WithEvents TimerExportar As Timer
        Friend WithEvents Panel3 As Panel
        Friend WithEvents BtnExportarLotes As Button
        Friend WithEvents BtnNewCarpeta As Button
        Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
        Friend WithEvents DctosImg As DataGridViewTextBoxColumn
        Friend WithEvents SerieD As DataGridViewTextBoxColumn
        Friend WithEvents lblC_pdf As Label
        Friend WithEvents Ide As DataGridViewTextBoxColumn
        Friend WithEvents Nombre As DataGridViewTextBoxColumn
    End Class

End Namespace
