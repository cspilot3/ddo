Namespace Visor
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ImageViewer
        Inherits System.Windows.Forms.UserControl

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImageViewer))
            Me.btnReflejarHorizontal = New System.Windows.Forms.Button()
            Me.btnReflejarVertical = New System.Windows.Forms.Button()
            Me.btnRotarIzquierda = New System.Windows.Forms.Button()
            Me.pnlBotones = New System.Windows.Forms.Panel()
            Me.pnlFolio = New System.Windows.Forms.Panel()
            Me.btnPaginaPrimera = New System.Windows.Forms.Button()
            Me.btnPaginaFinal = New System.Windows.Forms.Button()
            Me.btnPaginaSiguiente = New System.Windows.Forms.Button()
            Me.btnPaginaAnterior = New System.Windows.Forms.Button()
            Me.nudPaginas = New System.Windows.Forms.NumericUpDown()
            Me.btnRotarDerecha = New System.Windows.Forms.Button()
            Me.cbxZoom = New System.Windows.Forms.ComboBox()
            Me.chkVerMiniaturas = New System.Windows.Forms.CheckBox()
            Me.btnZoomOut = New System.Windows.Forms.Button()
            Me.btnZoomIn = New System.Windows.Forms.Button()
            Me.btnAjustarAncho = New System.Windows.Forms.Button()
            Me.btnAjustarAlto = New System.Windows.Forms.Button()
            Me.pnlMiniaturas = New System.Windows.Forms.Panel()
            Me.pnlBase = New System.Windows.Forms.Panel()
            Me.pnlMarcoDibujo = New System.Windows.Forms.Panel()
            Me.pnlImage = New System.Windows.Forms.Panel()
            Me.picImage = New System.Windows.Forms.PictureBox()
            Me.ToolTipMenu = New System.Windows.Forms.ToolTip(Me.components)
            Me.pnlBotones.SuspendLayout()
            Me.pnlFolio.SuspendLayout()
            CType(Me.nudPaginas, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlBase.SuspendLayout()
            Me.pnlMarcoDibujo.SuspendLayout()
            Me.pnlImage.SuspendLayout()
            CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'btnReflejarHorizontal
            '
            Me.btnReflejarHorizontal.FlatAppearance.BorderSize = 0
            Me.btnReflejarHorizontal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnReflejarHorizontal.Image = CType(resources.GetObject("btnReflejarHorizontal.Image"), System.Drawing.Image)
            Me.btnReflejarHorizontal.Location = New System.Drawing.Point(272, 1)
            Me.btnReflejarHorizontal.Name = "btnReflejarHorizontal"
            Me.btnReflejarHorizontal.Size = New System.Drawing.Size(20, 30)
            Me.btnReflejarHorizontal.TabIndex = 13
            Me.ToolTipMenu.SetToolTip(Me.btnReflejarHorizontal, "Reflejar imagen horizontalmente")
            '
            'btnReflejarVertical
            '
            Me.btnReflejarVertical.FlatAppearance.BorderSize = 0
            Me.btnReflejarVertical.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnReflejarVertical.Image = CType(resources.GetObject("btnReflejarVertical.Image"), System.Drawing.Image)
            Me.btnReflejarVertical.Location = New System.Drawing.Point(295, 1)
            Me.btnReflejarVertical.Name = "btnReflejarVertical"
            Me.btnReflejarVertical.Size = New System.Drawing.Size(20, 30)
            Me.btnReflejarVertical.TabIndex = 14
            Me.ToolTipMenu.SetToolTip(Me.btnReflejarVertical, "Reflejar imagen verticalmente")
            '
            'btnRotarIzquierda
            '
            Me.btnRotarIzquierda.FlatAppearance.BorderSize = 0
            Me.btnRotarIzquierda.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnRotarIzquierda.Image = CType(resources.GetObject("btnRotarIzquierda.Image"), System.Drawing.Image)
            Me.btnRotarIzquierda.Location = New System.Drawing.Point(226, 1)
            Me.btnRotarIzquierda.Name = "btnRotarIzquierda"
            Me.btnRotarIzquierda.Size = New System.Drawing.Size(20, 30)
            Me.btnRotarIzquierda.TabIndex = 11
            Me.ToolTipMenu.SetToolTip(Me.btnRotarIzquierda, "Rotar imagen a la izquierda")
            '
            'pnlBotones
            '
            Me.pnlBotones.Controls.Add(Me.pnlFolio)
            Me.pnlBotones.Controls.Add(Me.btnReflejarHorizontal)
            Me.pnlBotones.Controls.Add(Me.btnReflejarVertical)
            Me.pnlBotones.Controls.Add(Me.btnRotarIzquierda)
            Me.pnlBotones.Controls.Add(Me.btnRotarDerecha)
            Me.pnlBotones.Controls.Add(Me.cbxZoom)
            Me.pnlBotones.Controls.Add(Me.chkVerMiniaturas)
            Me.pnlBotones.Controls.Add(Me.btnZoomOut)
            Me.pnlBotones.Controls.Add(Me.btnZoomIn)
            Me.pnlBotones.Controls.Add(Me.btnAjustarAncho)
            Me.pnlBotones.Controls.Add(Me.btnAjustarAlto)
            Me.pnlBotones.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlBotones.Location = New System.Drawing.Point(100, 0)
            Me.pnlBotones.Name = "pnlBotones"
            Me.pnlBotones.Size = New System.Drawing.Size(541, 32)
            Me.pnlBotones.TabIndex = 0
            '
            'pnlFolio
            '
            Me.pnlFolio.Controls.Add(Me.btnPaginaPrimera)
            Me.pnlFolio.Controls.Add(Me.btnPaginaFinal)
            Me.pnlFolio.Controls.Add(Me.btnPaginaSiguiente)
            Me.pnlFolio.Controls.Add(Me.btnPaginaAnterior)
            Me.pnlFolio.Controls.Add(Me.nudPaginas)
            Me.pnlFolio.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlFolio.Location = New System.Drawing.Point(382, 0)
            Me.pnlFolio.Name = "pnlFolio"
            Me.pnlFolio.Size = New System.Drawing.Size(159, 32)
            Me.pnlFolio.TabIndex = 15
            '
            'btnPaginaPrimera
            '
            Me.btnPaginaPrimera.FlatAppearance.BorderSize = 0
            Me.btnPaginaPrimera.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnPaginaPrimera.Image = CType(resources.GetObject("btnPaginaPrimera.Image"), System.Drawing.Image)
            Me.btnPaginaPrimera.Location = New System.Drawing.Point(21, 0)
            Me.btnPaginaPrimera.Name = "btnPaginaPrimera"
            Me.btnPaginaPrimera.Size = New System.Drawing.Size(17, 30)
            Me.btnPaginaPrimera.TabIndex = 6
            Me.ToolTipMenu.SetToolTip(Me.btnPaginaPrimera, "Mostrar la primera página")
            '
            'btnPaginaFinal
            '
            Me.btnPaginaFinal.FlatAppearance.BorderSize = 0
            Me.btnPaginaFinal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnPaginaFinal.Image = CType(resources.GetObject("btnPaginaFinal.Image"), System.Drawing.Image)
            Me.btnPaginaFinal.Location = New System.Drawing.Point(134, 0)
            Me.btnPaginaFinal.Name = "btnPaginaFinal"
            Me.btnPaginaFinal.Size = New System.Drawing.Size(17, 30)
            Me.btnPaginaFinal.TabIndex = 10
            Me.ToolTipMenu.SetToolTip(Me.btnPaginaFinal, "Mostrar la última página")
            '
            'btnPaginaSiguiente
            '
            Me.btnPaginaSiguiente.FlatAppearance.BorderSize = 0
            Me.btnPaginaSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnPaginaSiguiente.Image = CType(resources.GetObject("btnPaginaSiguiente.Image"), System.Drawing.Image)
            Me.btnPaginaSiguiente.Location = New System.Drawing.Point(119, 0)
            Me.btnPaginaSiguiente.Name = "btnPaginaSiguiente"
            Me.btnPaginaSiguiente.Size = New System.Drawing.Size(17, 30)
            Me.btnPaginaSiguiente.TabIndex = 9
            Me.ToolTipMenu.SetToolTip(Me.btnPaginaSiguiente, "Mostrar la siguiente página")
            '
            'btnPaginaAnterior
            '
            Me.btnPaginaAnterior.FlatAppearance.BorderSize = 0
            Me.btnPaginaAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnPaginaAnterior.Image = CType(resources.GetObject("btnPaginaAnterior.Image"), System.Drawing.Image)
            Me.btnPaginaAnterior.Location = New System.Drawing.Point(38, 0)
            Me.btnPaginaAnterior.Name = "btnPaginaAnterior"
            Me.btnPaginaAnterior.Size = New System.Drawing.Size(17, 30)
            Me.btnPaginaAnterior.TabIndex = 7
            Me.ToolTipMenu.SetToolTip(Me.btnPaginaAnterior, "Mostrar la anterior página")
            '
            'nudPaginas
            '
            Me.nudPaginas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.nudPaginas.Location = New System.Drawing.Point(58, 5)
            Me.nudPaginas.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.nudPaginas.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.nudPaginas.Name = "nudPaginas"
            Me.nudPaginas.Size = New System.Drawing.Size(58, 22)
            Me.nudPaginas.TabIndex = 8
            Me.nudPaginas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.ToolTipMenu.SetToolTip(Me.nudPaginas, "Número de la página a visualizar")
            Me.nudPaginas.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            'btnRotarDerecha
            '
            Me.btnRotarDerecha.FlatAppearance.BorderSize = 0
            Me.btnRotarDerecha.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnRotarDerecha.Image = CType(resources.GetObject("btnRotarDerecha.Image"), System.Drawing.Image)
            Me.btnRotarDerecha.Location = New System.Drawing.Point(249, 1)
            Me.btnRotarDerecha.Name = "btnRotarDerecha"
            Me.btnRotarDerecha.Size = New System.Drawing.Size(20, 30)
            Me.btnRotarDerecha.TabIndex = 12
            Me.ToolTipMenu.SetToolTip(Me.btnRotarDerecha, "Rotar imagen a la derecha")
            '
            'cbxZoom
            '
            Me.cbxZoom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cbxZoom.Items.AddRange(New Object() {"10%", "20%", "50%", "75%", "100%", "150%", "200%", "400%"})
            Me.cbxZoom.Location = New System.Drawing.Point(115, 5)
            Me.cbxZoom.Name = "cbxZoom"
            Me.cbxZoom.Size = New System.Drawing.Size(70, 24)
            Me.cbxZoom.TabIndex = 4
            Me.cbxZoom.Text = "100%"
            Me.ToolTipMenu.SetToolTip(Me.cbxZoom, "Porcentage de ampliación de la imagen")
            '
            'chkVerMiniaturas
            '
            Me.chkVerMiniaturas.Appearance = System.Windows.Forms.Appearance.Button
            Me.chkVerMiniaturas.Checked = True
            Me.chkVerMiniaturas.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkVerMiniaturas.FlatAppearance.BorderSize = 0
            Me.chkVerMiniaturas.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.chkVerMiniaturas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.chkVerMiniaturas.Image = CType(resources.GetObject("chkVerMiniaturas.Image"), System.Drawing.Image)
            Me.chkVerMiniaturas.Location = New System.Drawing.Point(0, 1)
            Me.chkVerMiniaturas.Name = "chkVerMiniaturas"
            Me.chkVerMiniaturas.Size = New System.Drawing.Size(27, 30)
            Me.chkVerMiniaturas.TabIndex = 0
            Me.ToolTipMenu.SetToolTip(Me.chkVerMiniaturas, "Visualizar miniaturas")
            '
            'btnZoomOut
            '
            Me.btnZoomOut.FlatAppearance.BorderSize = 0
            Me.btnZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnZoomOut.Image = CType(resources.GetObject("btnZoomOut.Image"), System.Drawing.Image)
            Me.btnZoomOut.Location = New System.Drawing.Point(94, 1)
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.Size = New System.Drawing.Size(20, 30)
            Me.btnZoomOut.TabIndex = 3
            Me.ToolTipMenu.SetToolTip(Me.btnZoomOut, "Reducir la imagen")
            '
            'btnZoomIn
            '
            Me.btnZoomIn.FlatAppearance.BorderSize = 0
            Me.btnZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnZoomIn.Image = CType(resources.GetObject("btnZoomIn.Image"), System.Drawing.Image)
            Me.btnZoomIn.Location = New System.Drawing.Point(186, 1)
            Me.btnZoomIn.Name = "btnZoomIn"
            Me.btnZoomIn.Size = New System.Drawing.Size(20, 30)
            Me.btnZoomIn.TabIndex = 5
            Me.ToolTipMenu.SetToolTip(Me.btnZoomIn, "Ampliar la imagen")
            '
            'btnAjustarAncho
            '
            Me.btnAjustarAncho.FlatAppearance.BorderSize = 0
            Me.btnAjustarAncho.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnAjustarAncho.Image = CType(resources.GetObject("btnAjustarAncho.Image"), System.Drawing.Image)
            Me.btnAjustarAncho.Location = New System.Drawing.Point(61, 1)
            Me.btnAjustarAncho.Name = "btnAjustarAncho"
            Me.btnAjustarAncho.Size = New System.Drawing.Size(20, 30)
            Me.btnAjustarAncho.TabIndex = 2
            Me.ToolTipMenu.SetToolTip(Me.btnAjustarAncho, "Ajustar imagen al ancho")
            '
            'btnAjustarAlto
            '
            Me.btnAjustarAlto.FlatAppearance.BorderSize = 0
            Me.btnAjustarAlto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnAjustarAlto.Image = CType(resources.GetObject("btnAjustarAlto.Image"), System.Drawing.Image)
            Me.btnAjustarAlto.Location = New System.Drawing.Point(38, 1)
            Me.btnAjustarAlto.Name = "btnAjustarAlto"
            Me.btnAjustarAlto.Size = New System.Drawing.Size(20, 30)
            Me.btnAjustarAlto.TabIndex = 1
            Me.ToolTipMenu.SetToolTip(Me.btnAjustarAlto, "Ajustar imagen al alto")
            '
            'pnlMiniaturas
            '
            Me.pnlMiniaturas.AutoScroll = True
            Me.pnlMiniaturas.BackColor = System.Drawing.SystemColors.ControlDarkDark
            Me.pnlMiniaturas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.pnlMiniaturas.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlMiniaturas.Location = New System.Drawing.Point(0, 0)
            Me.pnlMiniaturas.Name = "pnlMiniaturas"
            Me.pnlMiniaturas.Size = New System.Drawing.Size(100, 383)
            Me.pnlMiniaturas.TabIndex = 1
            '
            'pnlBase
            '
            Me.pnlBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlBase.Controls.Add(Me.pnlMarcoDibujo)
            Me.pnlBase.Controls.Add(Me.pnlBotones)
            Me.pnlBase.Controls.Add(Me.pnlMiniaturas)
            Me.pnlBase.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBase.Location = New System.Drawing.Point(0, 0)
            Me.pnlBase.Name = "pnlBase"
            Me.pnlBase.Size = New System.Drawing.Size(643, 385)
            Me.pnlBase.TabIndex = 2
            '
            'pnlMarcoDibujo
            '
            Me.pnlMarcoDibujo.AutoScroll = True
            Me.pnlMarcoDibujo.BackColor = System.Drawing.Color.Teal
            Me.pnlMarcoDibujo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.pnlMarcoDibujo.Controls.Add(Me.pnlImage)
            Me.pnlMarcoDibujo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlMarcoDibujo.Location = New System.Drawing.Point(100, 32)
            Me.pnlMarcoDibujo.Name = "pnlMarcoDibujo"
            Me.pnlMarcoDibujo.Size = New System.Drawing.Size(541, 351)
            Me.pnlMarcoDibujo.TabIndex = 2
            '
            'pnlImage
            '
            Me.pnlImage.BackColor = System.Drawing.SystemColors.ControlDarkDark
            Me.pnlImage.Controls.Add(Me.picImage)
            Me.pnlImage.Location = New System.Drawing.Point(16, 8)
            Me.pnlImage.Name = "pnlImage"
            Me.pnlImage.Padding = New System.Windows.Forms.Padding(10)
            Me.pnlImage.Size = New System.Drawing.Size(384, 264)
            Me.pnlImage.TabIndex = 0
            '
            'picImage
            '
            Me.picImage.BackColor = System.Drawing.Color.White
            Me.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.picImage.Dock = System.Windows.Forms.DockStyle.Fill
            Me.picImage.Location = New System.Drawing.Point(10, 10)
            Me.picImage.Name = "picImage"
            Me.picImage.Size = New System.Drawing.Size(364, 244)
            Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.picImage.TabIndex = 0
            Me.picImage.TabStop = False
            '
            'ImageViewer
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.pnlBase)
            Me.Name = "ImageViewer"
            Me.Size = New System.Drawing.Size(643, 385)
            Me.pnlBotones.ResumeLayout(False)
            Me.pnlFolio.ResumeLayout(False)
            CType(Me.nudPaginas, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlBase.ResumeLayout(False)
            Me.pnlMarcoDibujo.ResumeLayout(False)
            Me.pnlImage.ResumeLayout(False)
            CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents btnReflejarHorizontal As System.Windows.Forms.Button
        Friend WithEvents ToolTipMenu As System.Windows.Forms.ToolTip
        Friend WithEvents btnReflejarVertical As System.Windows.Forms.Button
        Friend WithEvents btnRotarIzquierda As System.Windows.Forms.Button
        Friend WithEvents pnlBotones As System.Windows.Forms.Panel
        Friend WithEvents btnRotarDerecha As System.Windows.Forms.Button
        Friend WithEvents cbxZoom As System.Windows.Forms.ComboBox
        Friend WithEvents chkVerMiniaturas As System.Windows.Forms.CheckBox
        Friend WithEvents nudPaginas As System.Windows.Forms.NumericUpDown
        Friend WithEvents btnZoomOut As System.Windows.Forms.Button
        Friend WithEvents btnZoomIn As System.Windows.Forms.Button
        Friend WithEvents btnPaginaAnterior As System.Windows.Forms.Button
        Friend WithEvents btnPaginaSiguiente As System.Windows.Forms.Button
        Friend WithEvents btnPaginaFinal As System.Windows.Forms.Button
        Friend WithEvents btnPaginaPrimera As System.Windows.Forms.Button
        Friend WithEvents btnAjustarAncho As System.Windows.Forms.Button
        Friend WithEvents btnAjustarAlto As System.Windows.Forms.Button
        Friend WithEvents pnlMiniaturas As System.Windows.Forms.Panel
        Friend WithEvents pnlBase As System.Windows.Forms.Panel
        Friend WithEvents pnlMarcoDibujo As System.Windows.Forms.Panel
        Friend WithEvents pnlImage As System.Windows.Forms.Panel
        Friend WithEvents picImage As System.Windows.Forms.PictureBox
        Friend WithEvents pnlFolio As System.Windows.Forms.Panel

    End Class
End Namespace