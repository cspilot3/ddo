Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Collections
Imports System.IO
Imports System.Threading

Namespace Visor

    Public Class ImageViewer

#Region " Declaraciones "

        Public Enum FormatoImagen As Byte
            BMP = 10
            GIF = 20
            JPEG = 30
            PNG = 40
            TIFF = 50
        End Enum

        'Private _Image As Image = Nothing
        Private _ImagePath As IEnumerable(Of String)

        Private Folios As List(Of ImageFolio)

        Private _ThumbnailImages As New ArrayList
        Private _SelectedPage As Integer = 1

        Private _Zoom As Short
        Private _Rotado As Boolean = False
        Private _AutoActualizar As Boolean = True
        Const ThumbnailWhith As Integer = 60

        Protected Delegate Sub InicializarFolioDelegate(nLocalProcessId As Guid, nImage As Bitmap, ByVal nFolio As Integer, nIndex As Integer)

        Private ProcessId As Guid
#End Region

#Region " Propiedades "

        Public Property ImagePath() As IEnumerable(Of String)
            Get
                Return _ImagePath
            End Get
            Set(ByVal Value As IEnumerable(Of String))
                _AutoActualizar = False

                If (Value IsNot Nothing) Then
                    _ImagePath = Value
                    Folios = New List(Of ImageFolio)()

                    _ThumbnailImages = New ArrayList()
                    ClearThumbnailImages()

                    Try
                        For Each filename In _ImagePath
                            Dim formato As FormatoImagen

                            Select Case Path.GetExtension(filename).ToUpper
                                Case ".BMP" : formato = FormatoImagen.BMP
                                Case ".GIF" : formato = FormatoImagen.GIF
                                Case ".JPG" : formato = FormatoImagen.JPEG
                                Case ".PNG" : formato = FormatoImagen.PNG
                                Case ".TIF" : formato = FormatoImagen.TIFF
                                Case Else : Throw New Exception("Formato de imagen no valido: " & filename)
                            End Select

                            LoadImage(filename, formato)
                        Next

                    Catch
                        _ImagePath = Nothing
                        Throw
                    End Try

                    ' Miniaturas
                    ProcessId = Guid.NewGuid()

                    If (Me.UseThread) Then
                        Dim hilo = New Thread(AddressOf LoadImageBack)
                        hilo.Start()
                    Else
                        LoadImageBack()
                    End If

                    nudPaginas.Maximum = Folios.Count
                    SelectedPage = 1
                Else
                    _ImagePath = Nothing
                    OcultarImagen()
                End If

                _AutoActualizar = True
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub

        'Public Property ImagePath(ByVal arrayList As ArrayList) As String
        '    Get
        '        Return _ImagePath
        '    End Get
        '    Set(ByVal Value As String)
        '        If Value <> "" Then
        '            Try
        '                Dim Formato As FormatoImagen

        '                _ImagePath = Value
        '                _Image = New Bitmap(Value)

        '                Select Case Path.GetExtension(_ImagePath).ToUpper
        '                    Case ".BMP" : Formato = FormatoImagen.BMP
        '                    Case ".GIF" : Formato = FormatoImagen.GIF
        '                    Case ".JPG" : Formato = FormatoImagen.JPEG
        '                    Case ".PNG" : Formato = FormatoImagen.PNG
        '                    Case ".TIF" : Formato = FormatoImagen.TIFF
        '                    Case Else : Throw New Exception("Formato de imagen no valido")
        '                End Select
        '                _ThumbnailImages = arrayList
        '                _Pages = arrayList.Count
        '                nudPaginas.Maximum = _Pages

        '                LoadImage(_Image, Formato)
        '            Catch ex As Exception
        '                _ImagePath = ""
        '                Throw
        '            End Try
        '        Else
        '            _ImagePath = ""
        '            OcultarImagen()
        '        End If
        '    End Set
        'End Property

        Public Property Zoom() As Short
            Get
                Return _Zoom
            End Get
            Set(ByVal Value As Short)
                If Value < 10 Or Value > 1000 Then
                    MessageBox.Show("El valor debe se mayor o igual a 10 y menor o igual a 1000", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    _Zoom = Value
                    cbxZoom.Text = _Zoom & "%"
                    AplicarZoom()
                End If
            End Set
        End Property

        Public Property SelectedPage() As Integer
            Get
                Return _SelectedPage
            End Get
            Set(ByVal Value As Integer)
                If (Folios Is Nothing OrElse Folios.Count = 0) Then Return

                If (Value > Folios.Count) Then
                    MessageBox.Show("El valor debe se mayor o igual a 1 y menor o igual a " & Folios.Count, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    _AutoActualizar = False

                    If Value <= 0 Then
                        _SelectedPage = 1
                    Else
                        _SelectedPage = Value
                    End If

                    nudPaginas.Value = _SelectedPage

                    _Rotado = False
                    _AutoActualizar = True

                    ShowSelectedImage()
                    pnlMiniaturas.Refresh()
                End If
            End Set
        End Property

        Public Property UseThread As Boolean = False

#End Region

#Region " Eventos "

        Private Sub ImageViewer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            ActivarControles(False)
        End Sub

        Private Sub cbxZoom_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cbxZoom.SelectedIndexChanged
            ActualizarValorZoom(cbxZoom.Text)
        End Sub

        Private Sub cbxZoom_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles cbxZoom.KeyUp
            If e.KeyCode = Keys.Enter Then
                ActualizarValorZoom(cbxZoom.Text)
            End If
        End Sub

        Private Sub chkVerMiniaturas_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkVerMiniaturas.CheckedChanged
            pnlMiniaturas.Visible = chkVerMiniaturas.Checked
        End Sub

        Private Sub pnlMarcoDibujo_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles pnlMarcoDibujo.Resize
            If _AutoActualizar Then AplicarZoom()
        End Sub

        Private Sub nudPaginas_ValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles nudPaginas.ValueChanged
            If _AutoActualizar Then SelectedPage = CInt(nudPaginas.Value)
        End Sub

        Private Sub btnRotarIzquierda_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRotarIzquierda.Click
            RotarIzquierda()
        End Sub

        Private Sub btnRotarDerecha_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRotarDerecha.Click
            RotarDerecha()
        End Sub

        Private Sub btnReflejarHorizontal_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnReflejarHorizontal.Click
            ReflejarHorizontal()
        End Sub

        Private Sub btnReflejarVertical_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnReflejarVertical.Click
            ReflejarVertical()
        End Sub

        Private Sub btnAjustarAlto_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAjustarAlto.Click
            AjustarAlto()
        End Sub

        Private Sub btnAjustarAncho_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAjustarAncho.Click
            AjustarAncho()
        End Sub

        Private Sub btnZoomIn_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnZoomIn.Click
            ZoomIn()
        End Sub

        Private Sub btnZoomOut_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnZoomOut.Click
            ZoomOut()
        End Sub

        Private Sub btnPaginaPrimera_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnPaginaPrimera.Click
            PaginaPrimera()
        End Sub

        Private Sub btnPaginaFinal_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnPaginaFinal.Click
            PaginaFinal()
        End Sub

        Private Sub btnPaginaAnterior_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnPaginaAnterior.Click
            PaginaAnterior()
        End Sub

        Private Sub btnPaginaSiguiente_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnPaginaSiguiente.Click
            Siguiente()
        End Sub

        Private Sub pnlMiniaturas_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles pnlMiniaturas.Paint
            If (_ThumbnailImages.Count > 0) Then
                Dim ThumbnailPictureBox As PictureBox = CType(_ThumbnailImages.Item(_SelectedPage - 1), PictureBox)
                Dim BluePen As New Pen(Color.Blue, 4)

                e.Graphics.DrawRectangle(BluePen, ThumbnailPictureBox.Location.X, ThumbnailPictureBox.Location.Y, ThumbnailPictureBox.Size.Width, ThumbnailPictureBox.Size.Height)
            End If
        End Sub

        Private Sub picThumbnail_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim ThumbnailPictureBox As PictureBox = CType(sender, PictureBox)

            SelectedPage = CInt(ThumbnailPictureBox.Tag)
        End Sub

#End Region

#Region " Metodos "

        Private Sub LoadImage(ByVal nFilename As String, ByVal nFormato As FormatoImagen)
            Select Case nFormato
                Case FormatoImagen.BMP, FormatoImagen.GIF, FormatoImagen.JPEG, FormatoImagen.PNG
                    Folios.Add(New ImageFolio(nFilename, 0))

                Case FormatoImagen.TIFF
                    LoadImageTIFF(nFilename)

                Case Else
                    Throw New Exception("El formato de imagen no es válido")

            End Select
        End Sub

        Private Sub LoadImageTIFF(ByVal nFilename As String)
            Using fileImage = New Bitmap(nFilename)
                Dim myGuid = fileImage.FrameDimensionsList()
                Dim myFrameDimension = New FrameDimension(myGuid(0))
                Dim pages = fileImage.GetFrameCount(myFrameDimension)

                For i As Integer = 0 To pages - 1
                    Folios.Add(New ImageFolio(nFilename, i))
                Next
            End Using
        End Sub

        Private Sub LoadImageBack()
            Dim LocalProcessId = ProcessId
            Dim fileImage As Bitmap = Nothing
            Dim previuosFilename As String = ""
            Dim index As Integer


            For Each folio In Folios
                If (LocalProcessId <> ProcessId) Then Return

                index += 1

                If (previuosFilename <> folio.Filename) Then
                    previuosFilename = folio.Filename

                    If (fileImage IsNot Nothing) Then fileImage.Dispose()

                    fileImage = New Bitmap(previuosFilename)
                End If

                InicializarFolio(LocalProcessId, fileImage, folio.Folio, index)
            Next

            If (fileImage IsNot Nothing) Then fileImage.Dispose()
        End Sub

        Private Sub InicializarFolio(nLocalProcessId As Guid, nImage As Bitmap, ByVal nFolio As Integer, nIndex As Integer)
            If (Me.InvokeRequired Or Me.pnlMiniaturas.InvokeRequired) Then
                Dim MyDelegate As InicializarFolioDelegate

                MyDelegate = AddressOf InicializarFolio
                Me.Invoke(MyDelegate, New Object() {nLocalProcessId, nImage, nFolio, nIndex})
            Else
                Dim myGuid = nImage.FrameDimensionsList()
                Dim myFrameDimension = New FrameDimension(myGuid(0))
                nImage.SelectActiveFrame(myFrameDimension, nFolio)

                Application.DoEvents()
                Using SingleBitmap = New Bitmap(nImage, nImage.Width, nImage.Height)
                    Dim ThumbnailHeight = CInt((ThumbnailWhith / SingleBitmap.Width) * SingleBitmap.Height)

                    Dim ThumbnailPictureBox = New PictureBox()
                    ThumbnailPictureBox.Tag = nIndex
                    ThumbnailPictureBox.BorderStyle = BorderStyle.FixedSingle
                    ThumbnailPictureBox.Cursor = Cursors.Hand
                    ThumbnailPictureBox.SizeMode = PictureBoxSizeMode.AutoSize

                    ThumbnailPictureBox.Image = SingleBitmap.GetThumbnailImage(ThumbnailWhith, ThumbnailHeight, AddressOf ThumbnailCallback, IntPtr.Zero)

                    If (nLocalProcessId <> ProcessId) Then Return

                    _ThumbnailImages.Add(ThumbnailPictureBox)

                    ShowThumbnailImage()
                End Using
            End If
        End Sub

        Private Sub ShowSelectedImage()
            If (Folios IsNot Nothing AndAlso Folios.Count > 0) Then
                _AutoActualizar = False

                Dim folio = Folios(_SelectedPage - 1)
                Dim fileImage = New Bitmap(folio.Filename)

                Dim myGuid = fileImage.FrameDimensionsList()
                Dim myFrameDimension = New FrameDimension(myGuid(0))
                fileImage.SelectActiveFrame(myFrameDimension, folio.Folio)

                If (picImage.Image IsNot Nothing) Then picImage.Image.Dispose()

                picImage.Image = fileImage
                pnlImage.AutoScrollPosition = New Point(0, 0)
                pnlImage.SetBounds(0, 0, CInt((fileImage.Width * (_Zoom / 100)) + 20), CInt((fileImage.Height * (_Zoom / 100)) + 20))

                _AutoActualizar = True

                MejorAjuste()
            End If
        End Sub

        Private Sub ClearThumbnailImages()
            pnlMiniaturas.SuspendLayout()

            pnlMiniaturas.Controls.Clear()

            pnlMiniaturas.ResumeLayout()
            pnlMiniaturas.Refresh()
        End Sub

        Private Sub ShowThumbnailImage()
            Dim PosY As Integer = 10

            pnlMiniaturas.SuspendLayout()

            If (_ThumbnailImages.Count > 1) Then
                Dim last = CType(_ThumbnailImages(_ThumbnailImages.Count - 2), PictureBox)
                PosY += last.Top + last.Height
            End If

            Dim ThumbnailPictureBox = CType(_ThumbnailImages(_ThumbnailImages.Count - 1), PictureBox)

            ThumbnailPictureBox.Location = New Point(10, PosY)
            pnlMiniaturas.Controls.Add(ThumbnailPictureBox)

            AddHandler ThumbnailPictureBox.Click, AddressOf picThumbnail_Click

            pnlMiniaturas.ResumeLayout()
            pnlMiniaturas.Refresh()

            Application.DoEvents()
        End Sub

        Private Sub OcultarImagen()
            If (picImage.Image IsNot Nothing) Then
                picImage.Image.Dispose()
                picImage.Image = Nothing
            End If

            For Each ThumbnailPictureBox As PictureBox In _ThumbnailImages
                If (ThumbnailPictureBox.Image IsNot Nothing) Then
                    ThumbnailPictureBox.Image.Dispose()
                    ThumbnailPictureBox.Image = Nothing
                End If
            Next

            _ThumbnailImages.Clear()
            pnlMiniaturas.Controls.Clear()
        End Sub

        Private Sub ActualizarValorZoom(ByVal nZoom As String)
            Dim Valor As Object

            Valor = nZoom
            Valor = Valor.ToString().TrimEnd("%"c)
            Valor = Valor.ToString().Trim()

            If IsNumeric(Valor) Then
                If Val(Valor) < 10 Or Val(Valor) > 1000 Then
                    MessageBox.Show("El valor debe se mayor o igual a 10 y menor o igual a 1000", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cbxZoom.Text = _Zoom & "%"
                    Return
                Else
                    _Zoom = CShort(Val(Valor))
                End If
            End If

            cbxZoom.Text = _Zoom & "%"

            AplicarZoom()
        End Sub

        Private Sub AplicarZoom()
            If (Not picImage.Image Is Nothing) Then
                Dim fileImage As Bitmap = CType(picImage.Image, Bitmap)
                Dim ZoomReal As Single = CSng((_Zoom / 100))
                Dim NewWidth, NewHeight As Integer
                Dim IPosX, IPosY, ASPosX, ASPosY As Integer
                Dim DiffPosX, DiffPosY As Integer
                Dim BarraX, BarraY As Integer

                If _Rotado Then
                    NewWidth = CInt((fileImage.Height * ZoomReal) + 20)
                    NewHeight = CInt((fileImage.Width * ZoomReal) + 20)
                Else
                    NewWidth = CInt((fileImage.Width * ZoomReal) + 20)
                    NewHeight = CInt((fileImage.Height * ZoomReal) + 20)
                End If

                If (pnlMarcoDibujo.Size.Width <= NewWidth) Then BarraX = 20
                If (pnlMarcoDibujo.Size.Height <= NewHeight) Then BarraY = 20

                If (pnlMarcoDibujo.Size.Width - BarraY > NewWidth) Then
                    ' Mas pequeño que el marco
                    ASPosX = 0
                    IPosX = CInt((pnlMarcoDibujo.Size.Width - BarraY - NewWidth) / 2)
                Else
                    ' Mas grande que el marco
                    DiffPosX = (NewWidth - pnlImage.Size.Width)

                    If pnlImage.Location.X > 0 Then
                        ASPosX = CInt((-1 * pnlMarcoDibujo.AutoScrollPosition.X) + (DiffPosX / 2) - pnlImage.Location.X)
                    Else
                        ASPosX = CInt((-1 * pnlMarcoDibujo.AutoScrollPosition.X) + (DiffPosX / 2))
                    End If

                    If ASPosX < 0 Then ASPosX = 0

                    IPosX = 0

                End If

                If (pnlMarcoDibujo.Size.Height - BarraX > NewHeight) Then
                    ASPosY = 0
                    IPosY = CInt((pnlMarcoDibujo.Size.Height - BarraX - NewHeight) / 2)
                Else
                    DiffPosY = (NewHeight - pnlImage.Size.Height)

                    If pnlImage.Location.X > 0 Then
                        ASPosY = CInt((-1 * pnlMarcoDibujo.AutoScrollPosition.Y) + (DiffPosY / 2) - pnlImage.Location.Y)
                    Else
                        ASPosY = CInt((-1 * pnlMarcoDibujo.AutoScrollPosition.Y) + (DiffPosY / 2))
                    End If

                    If ASPosY < 0 Then ASPosY = 0

                    IPosY = 0
                End If

                _AutoActualizar = False

                pnlMarcoDibujo.AutoScrollPosition = New Point(0, 0)
                pnlImage.SetBounds(IPosX, IPosY, NewWidth, NewHeight)
                pnlMarcoDibujo.AutoScrollPosition = New Point(ASPosX, ASPosY)

                pnlMarcoDibujo.AutoScroll = False
                pnlMarcoDibujo.AutoScroll = True

                ActivarControles(True)

                _AutoActualizar = True
            End If
        End Sub

        Private Sub PaginaPrimera()
            SelectedPage = 1
        End Sub

        Private Sub PaginaFinal()
            SelectedPage = Folios.Count
        End Sub

        Private Sub PaginaAnterior()
            SelectedPage -= 1
        End Sub

        Private Sub Siguiente()
            SelectedPage += 1
        End Sub

        Private Sub RotarIzquierda()
            _Rotado = Not _Rotado

            pnlMarcoDibujo.AutoScrollPosition = New Point(0, 0)
            pnlImage.Size = New Size(pnlImage.Height, pnlImage.Width)
            picImage.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)

            AplicarZoom()

            CargarNuevaImagen()
        End Sub

        Private Sub RotarDerecha()
            _Rotado = Not _Rotado

            pnlMarcoDibujo.AutoScrollPosition = New Point(0, 0)
            pnlImage.Size = New Size(pnlImage.Height, pnlImage.Width)
            picImage.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)

            AplicarZoom()

            CargarNuevaImagen()
        End Sub

        Private Sub ReflejarHorizontal()
            picImage.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
            picImage.Refresh()

            CargarNuevaImagen()
        End Sub

        Private Sub ReflejarVertical()
            picImage.Image.RotateFlip(RotateFlipType.RotateNoneFlipY)
            picImage.Refresh()

            CargarNuevaImagen()
        End Sub

        Public Sub MejorAjuste()
            If (Not picImage.Image Is Nothing) Then
                Dim fileImage As Bitmap = CType(picImage.Image, Bitmap)

                If (fileImage.Height >= fileImage.Width) Then
                    AjustarAlto()
                Else
                    AjustarAncho()
                End If
            End If
        End Sub

        Private Sub AjustarAlto()
            If (Not picImage.Image Is Nothing) Then
                Dim fileImage As Bitmap = CType(picImage.Image, Bitmap)

                pnlMarcoDibujo.AutoScrollPosition = New Point(0, 0)
                pnlImage.Location = New Point(10, 10)

                If (fileImage.Height >= fileImage.Width) Then
                    _Zoom = CShort(((pnlMarcoDibujo.Height - 30) / fileImage.Height) * 100)
                Else
                    _Zoom = CShort(((pnlMarcoDibujo.Height - 50) / fileImage.Height) * 100)
                End If

                cbxZoom.Text = Format(_Zoom, "0") & "%"

                AplicarZoom()
            End If
        End Sub

        Private Sub AjustarAncho()
            If (Not picImage.Image Is Nothing) Then
                Dim fileImage As Bitmap = CType(picImage.Image, Bitmap)

                pnlMarcoDibujo.AutoScrollPosition = New Point(0, 0)
                pnlImage.Location = New Point(10, 10)

                If (fileImage.Width >= fileImage.Height) Then
                    _Zoom = CShort(((pnlMarcoDibujo.Width - 30) / fileImage.Width) * 100)
                Else
                    _Zoom = CShort(((pnlMarcoDibujo.Width - 50) / fileImage.Width) * 100)
                End If

                cbxZoom.Text = Format(_Zoom, "0") & "%"

                AplicarZoom()
            End If
        End Sub

        Private Sub ZoomIn()
            If _Zoom < 1000 Then
                _Zoom = CShort(_Zoom - (_Zoom Mod 10) + 10)
                cbxZoom.Text = Format(_Zoom, "0") & "%"
                AplicarZoom()
            End If
        End Sub

        Private Sub ZoomOut()
            If _Zoom > 10 Then
                If (_Zoom Mod 10 = 0) Then
                    _Zoom = CShort(_Zoom - 10)
                Else
                    _Zoom = CShort(_Zoom - (_Zoom Mod 10))
                End If

                cbxZoom.Text = Format(_Zoom, "0") & "%"
                AplicarZoom()
            End If
        End Sub

        Private Sub ActivarControles(ByVal nActivo As Boolean)
            Dim ZoomMedidaX, ZoomMedidaY As Integer

            btnZoomIn.Enabled = (_Zoom < 1000) And nActivo
            btnZoomOut.Enabled = (_Zoom > 10) And nActivo

            Application.DoEvents()
            If (Not picImage.Image Is Nothing And nActivo) Then
                Dim fileImage As Bitmap = CType(picImage.Image, Bitmap)
                If (fileImage.Height >= fileImage.Width) Then
                    ZoomMedidaY = CInt(((pnlMarcoDibujo.Height - 30) / fileImage.Height) * 100)
                    ZoomMedidaX = CInt(((pnlMarcoDibujo.Width - 50) / fileImage.Width) * 100)
                Else
                    ZoomMedidaY = CInt(((pnlMarcoDibujo.Height - 50) / fileImage.Height) * 100)
                    ZoomMedidaX = CInt(((pnlMarcoDibujo.Width - 30) / fileImage.Width) * 100)
                End If

                btnAjustarAlto.Enabled = (ZoomMedidaY <> _Zoom)
                btnAjustarAncho.Enabled = (ZoomMedidaX <> _Zoom)
            Else
                btnAjustarAlto.Enabled = False
                btnAjustarAncho.Enabled = False
            End If

            btnPaginaPrimera.Enabled = (_SelectedPage > 1) And nActivo
            btnPaginaFinal.Enabled = (_SelectedPage < Folios.Count) And nActivo
            btnPaginaAnterior.Enabled = (_SelectedPage > 1) And nActivo
            btnPaginaSiguiente.Enabled = (_SelectedPage < Folios.Count) And nActivo

            btnRotarIzquierda.Enabled = nActivo
            btnRotarDerecha.Enabled = nActivo
            btnReflejarHorizontal.Enabled = nActivo
            btnReflejarVertical.Enabled = nActivo

            cbxZoom.Enabled = nActivo
            nudPaginas.Enabled = nActivo
        End Sub

        'Evento para cargar nueva imágen -- Oswaldo Ibarra -- 12/12/2019
        Public Sub CargarNuevaImagen()
            Try
                Dim ListImages As New List(Of String)
                Dim filePath As String
                Dim NumeroPagina As Integer = CInt(nudPaginas.Value)

                ListImages = CType(_ImagePath, Global.System.Collections.Generic.List(Of String))

                'Se convierte la nueva imágen a byte
                Dim Imagen = ImageToByteArray(picImage.Image)

                filePath = ListImages(NumeroPagina - 1).ToString()
                'Se carga la imágen en la ruta
                Dim ms As MemoryStream = New MemoryStream(Imagen)
                Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                img.Save(filePath, System.Drawing.Imaging.ImageFormat.Tiff)
            Catch ex As Exception
                Return
            End Try
        End Sub

#End Region

#Region " Funciones "

        Public Function ThumbnailCallback() As Boolean
            Return False
        End Function

        'Función para convertir imágen a Byte -- Oswaldo Ibarra -- 12/12/2019
        Public Function ImageToByteArray(ByVal imageIn As System.Drawing.Image) As Byte()
            Using ms = New MemoryStream()
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Tiff)
                Return ms.ToArray()
            End Using
        End Function

#End Region

    End Class

End Namespace