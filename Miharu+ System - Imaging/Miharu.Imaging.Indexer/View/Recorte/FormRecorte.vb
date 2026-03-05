Imports System.Drawing
Imports System.Windows.Forms
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Imaging.Indexer.View.Comun
Imports Miharu.Imaging.Indexer.Controller
Imports Miharu.Desktop.Library.Config
Imports Miharu.Imaging.Indexer.Controller.Recorte
Imports Slyg.Tools.Imaging

Namespace View.Recorte

    Public Class FormRecorte
        Implements IView

#Region " Declaraciones "

        Private _Zoom As Short = 100

        Private InicialX As Integer
        Private InicialY As Integer

        Private oldX As Integer
        Private oldY As Integer

        Private SelectedDataControl As RecorteDataControl

        Private EnumDrawState As DrawStates

        Public Recortes As New List(Of RecorteDataControl)

        Private _ViewClosing As Boolean = False

        Private _ThumbnailHelper As DropThumbnailHelper

        Private _IndexerController As Object

#End Region

#Region " Constructores "

        Public Sub New()

            InitializeComponent()

            DrawState = DrawStates.WaitState

            Me._ThumbnailHelper = New DropThumbnailHelper()
            Me._ThumbnailHelper.Inicialize(Me, BackThumbnailPanel)

            AddHandler Me._ThumbnailHelper.OnChangeImagePosition, AddressOf ThumbnailHelper_OnChangeImagePosition

        End Sub

#End Region

#Region " Propiedades "

        Public Property DrawState() As DrawStates
            Get
                Return EnumDrawState
            End Get
            Set(ByVal NewEstado As DrawStates)
                EnumDrawState = NewEstado
                Select Case DrawState
                    Case DrawStates.WaitState
                        ImagePictureBox.Cursor = Cursors.Default
                        SelectedDataControl = Nothing

                    Case DrawStates.ComponentSelected
                        ImagePictureBox.Cursor = Cursors.Default

                    Case DrawStates.PreDrawComponent
                        ImagePictureBox.Cursor = Cursors.Cross
                        SelectedDataControl = Nothing
                        ImagePictureBox.Refresh()

                End Select
            End Set
        End Property

#End Region

#Region " Implementacion IView "

        Public ReadOnly Property Controller As IController Implements IView.Controller
            Get
                Return CType(_IndexerController, IController)
            End Get
        End Property

        Public Sub SetController(nController As Object) Implements IView.SetController
            _IndexerController = nController
        End Sub

        Public Property Unlock As Boolean Implements IView.Unlock

        Public Property Image As FreeImageAPI.FreeImageBitmap Implements IView.Image
            Get
                Return CType(ImagePictureBox.Image, FreeImageAPI.FreeImageBitmap)
            End Get
            Set(ByVal value As FreeImageAPI.FreeImageBitmap)
                ImagePictureBox.Image = CType(value, Bitmap)
            End Set
        End Property

        Public Sub UpdateAvance() Implements IView.UpdateAvance
            If Me.Controller.Cargado And Me.Controller.ImageCount > 0 Then
                Try
                    AvanceToolStripStatusLabel.Text = "Folio: " & Me.Controller.CurrentImageIndex + 1 & " de " & Me.Controller.ImageCount
                    ProgresoToolStripProgressBar.Style = ProgressBarStyle.Blocks
                    ProgresoToolStripProgressBar.Maximum = Me.Controller.ImageCount
                    ProgresoToolStripProgressBar.Minimum = 0
                    ProgresoToolStripProgressBar.Value = Me.Controller.CurrentImageIndex + 1
                    ProgresoToolStripProgressBar.ToolTipText = CInt(ProgresoToolStripProgressBar.Maximum / CInt(IIf(ProgresoToolStripProgressBar.Value = 0, 0, ProgresoToolStripProgressBar.Value))) * 100 & "%"
                Catch
                    Throw
                End Try
            Else
                AvanceToolStripStatusLabel.Text = "Folio: " & Me.Controller.CurrentImageIndex + 1 & " de ..."
                ProgresoToolStripProgressBar.Style = ProgressBarStyle.Marquee
                ProgresoToolStripProgressBar.MarqueeAnimationSpeed = 100
                ProgresoToolStripProgressBar.Maximum = 100
                ProgresoToolStripProgressBar.Minimum = 0
                ProgresoToolStripProgressBar.Value = 0
                ProgresoToolStripProgressBar.ToolTipText = "0%"
                ContadorToolStripStatusLabel.Text = "Paquete: " & Controller.Ciclo
            End If

            Application.DoEvents()
        End Sub

        Public Sub UpdateNombreImagen() Implements IView.UpdateNombreImagen

        End Sub

        Public ReadOnly Property ThumbnailHelper As DropThumbnailHelper Implements IView.ThumbnailHelper
            Get
                Return _ThumbnailHelper
            End Get
        End Property

        Public ReadOnly Property ThumbnailWidth As Integer Implements IView.ThumbnailWidth
            Get
                Return 160
            End Get
        End Property

        Public ReadOnly Property ThumbnailHeight As Integer Implements IView.ThumbnailHeight
            Get
                Return 220
            End Get
        End Property

        Public ReadOnly Property ThumbnailPanel As FlowLayoutPanel Implements IView.ThumbnailPanel
            Get
                Return ThumbnailFlowLayoutPanel
            End Get
        End Property

        Sub ScrollThumbnail() Implements IView.ScrollThumbnail
            BackThumbnailPanel.AutoScrollPosition = New Point(ThumbnailFlowLayoutPanel.Width - BackThumbnailPanel.Width, 0)
        End Sub

        Friend Sub Thumbnail_MouseEnter(ByVal sender As System.Object, ByVal e As EventArgs) Implements IView.Thumbnail_MouseEnter
            Dim pic As PictureBox = CType(sender, PictureBox)
            Dim Folio As Generic.Folio = CType(pic.Tag, Generic.Folio)

            If (Folio.ThumbnailImage IsNot Nothing) Then
                ImageFlotantePictureBox.Image = CType(Folio.ThumbnailImage, Bitmap)

                Const Ancho As Integer = 80
                Const Alto As Integer = 120

                If (Folio.ThumbnailImage.Width / Ancho) > (Folio.ThumbnailImage.Height / Alto) Then
                    ImageFlotantePanel.Size = New Size(Ancho, CInt(Folio.ThumbnailImage.Height / (Folio.ThumbnailImage.Width / Ancho)))
                Else
                    ImageFlotantePanel.Size = New Size(CInt(Folio.ThumbnailImage.Width / (Folio.ThumbnailImage.Height / Alto)), Alto)
                End If

                ImageFlotantePanel.Location = New Point(CInt((pic.Left + pic.Parent.Left + pic.Parent.Parent.Left + BackThumbnailPanel.AutoScrollOffset.X) - ((ImageFlotantePanel.Width - pic.Width) / 2)), BackThumbnailPanel.Top - ImageFlotantePanel.Height - 10)

                If ImageFlotantePanel.Left < 0 Then ImageFlotantePanel.Left = 0
                If ImageFlotantePanel.Left + ImageFlotantePanel.Width > Me.Width Then ImageFlotantePanel.Left = Me.Width - ImageFlotantePanel.Width

                ImageFlotantePanel.Visible = True

                MensajeToolStripStatusLabel.Text = "Tipo Documental: " & Folio.Parent.NombreTipoDocumento & " - Folios: " & Folio.Parent.Folios
            Else
                ImageFlotantePanel.Visible = False
            End If
        End Sub

        Friend Sub Thumbnail_MouseLeave(ByVal sender As System.Object, ByVal e As EventArgs) Implements IView.Thumbnail_MouseLeave
            ImageFlotantePanel.Visible = False

            MensajeToolStripStatusLabel.Text = ""
        End Sub

        Friend Sub Thumbnail_Click(ByVal sender As System.Object, ByVal e As EventArgs) Implements IView.Thumbnail_Click
            Try
                Dim pic As PictureBox = CType(sender, PictureBox)
                Dim Folio As Generic.Folio = CType(pic.Tag, Generic.Folio)
                Dim UpdateInfo As Boolean = False

                If (Controller.SetCurrentFolio(Folio, UpdateInfo)) Then
                    ShowImagen(False)

                    AjustarAncho()

                    ActivarControles(True)
                End If
            Catch ex As Exception
                Throw New Exception("Error al mostrar la imagen, " + ex.Message, ex)
            End Try

        End Sub

        Public Sub Clear() Implements IView.Clear
            ThumbnailFlowLayoutPanel.Controls.Clear()

            If (ImageFlotantePictureBox.Image IsNot Nothing) Then ImageFlotantePictureBox.Image.Dispose()
            If (ImagePictureBox.Image IsNot Nothing) Then ImagePictureBox.Image.Dispose()

            ImageFlotantePictureBox.Image = Nothing
            ImagePictureBox.Image = Nothing
        End Sub

        Public Sub ActivarControles(ByVal nActivo As Boolean) Implements IView.ActivarControles
            PreviousFolioButton.Enabled = (Me.Controller.CurrentImageIndex > 0) And nActivo
            NextFolioButton.Enabled = (Me.Controller.CurrentImageIndex < Me.Controller.ImageCount) And nActivo
        End Sub

        Public Overloads Function ShowDialog() As DialogResult Implements IView.ShowDialog
            ShowImagen(False)

            Cargar()

            Return MyBase.ShowDialog()
        End Function

        Public Sub SetTitle(ByVal title As String) Implements IView.SetTitle
            Me.Text = title
        End Sub

        Public ReadOnly Property ViewClosing As Boolean Implements IView.ViewClosing
            Get
                Return _ViewClosing
            End Get
        End Property

        Public Sub ShowImagen(ByVal UpdateInfo As Boolean) Implements IView.ShowImagen
            Dim ImageTemp = ImagePictureBox.Image

            DrawState = DrawStates.WaitState
            SelectedDataControl = Nothing

            ImagePictureBox.Image = CType(Me.Controller.CurrentImage, Bitmap)
            ImagePanel.AutoScrollPosition = New Point(0, 0)

            If (ImageTemp IsNot Nothing) Then ImageTemp.Dispose()

            AplicarZoom()

            ActivarControles(True)
        End Sub

        Private Function PreviousFolio() As Boolean Implements IView.PreviousFolio
            Dim UpdateInfo As Boolean = False

            If (Me.Controller.PreviousFolio(UpdateInfo)) Then
                ShowImagen(False)

                AjustarAncho()

                ActivarControles(True)
                Return True
            End If
            Return False
        End Function

        Private Sub NextFolio() Implements IView.NextFolio
            Dim UpdateInfo As Boolean = False

            If (Me.Controller.NextFolio(UpdateInfo)) Then
                ShowImagen(False)

                AjustarAncho()

                ActivarControles(True)
            ElseIf (Me.Controller.ImageCount = Controller.Folios) Then
                ' Preguntar si se finaliza el el indexado
                Dim Respuesta As DialogResult
                Respuesta = MessageBox.Show("No se encontraron más folios para indexar, ¿desea dar por terminado el proceso y almacenar los datos?", Program.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If Respuesta = DialogResult.Yes Then
                    If (Me.Controller.Save()) Then
                        Me.DialogResult = DialogResult.Yes
                        Me.Close()
                    End If
                Else
                    ActivarControles(True)
                End If
            Else
                ActivarControles(True)
            End If

        End Sub

        Public Sub ShowImagenRuta(ByVal path As String) Implements IView.ShowImagenRuta

        End Sub

#End Region

#Region " Eventos "

        Protected Sub ThumbnailHelper_OnChangeImagePosition(ByVal e As ChangeImagenPositionEventArgs)

        End Sub

        Private Sub FormIndexerView_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
            _ViewClosing = True
        End Sub

        Private Sub FormIndexerView_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
            If Unlock() Then Controller.Unlock()

            ImageFlotantePictureBox.Dispose()
            ImagePictureBox.Dispose()
            'Función Agregada Para Obligar Liberación De Memoria
            Utilities.ClearMemory()
            ''

        End Sub

        Private Sub FormIndexerView_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
            Try
                If (e.Control) Then
                    Select Case e.KeyCode
                        Case Keys.F1
                            ShowAccesos()

                        Case Keys.E
                            If (ReprocesoButton.Enabled And ReprocesoButton.Visible) Then Reproceso()

                        Case Keys.G
                            If (SaveButton.Enabled And SaveButton.Visible) Then Save()

                        Case Keys.S
                            ShowThumbnail()

                    End Select
                ElseIf (e.Shift) Then
                    Select Case e.KeyCode
                        Case Keys.PageDown
                            If (NextFolioButton.Enabled And NextFolioButton.Visible) Then NextFolio()

                        Case Keys.PageUp
                            If (PreviousFolioButton.Enabled And PreviousFolioButton.Visible) Then PreviousFolio()

                    End Select

                ElseIf (e.Alt) Then
                    Select Case e.KeyCode
                        Case Keys.A
                            MejorAjuste()

                        Case Keys.W
                            AjustarAncho()

                        Case Keys.H
                            AjustarAlto()

                        Case Keys.R
                            RotateRight()

                        Case Keys.L
                            RotateLeft()

                        Case Keys.Oemplus, Keys.Add
                            ZoomIn()

                        Case Keys.OemMinus, Keys.Subtract
                            ZoomOut()

                        Case Keys.Up
                            Dim Desplazamiento As Integer = CInt((MarcoDibujoPanel.Height - ImagePictureBox.Height) / 10)

                            MarcoDibujoPanel.AutoScrollPosition = New Point(MarcoDibujoPanel.AutoScrollPosition.X * -1, (MarcoDibujoPanel.AutoScrollPosition.Y - Desplazamiento) * -1)

                        Case Keys.Down
                            Dim Desplazamiento As Integer = CInt((MarcoDibujoPanel.Height - ImagePictureBox.Height) / 10)

                            MarcoDibujoPanel.AutoScrollPosition = New Point(MarcoDibujoPanel.AutoScrollPosition.X * -1, (MarcoDibujoPanel.AutoScrollPosition.Y + Desplazamiento) * -1)

                        Case Keys.Left
                            Dim Desplazamiento As Integer = CInt((MarcoDibujoPanel.Width - ImagePictureBox.Width) / 10)

                            MarcoDibujoPanel.AutoScrollPosition = New Point((MarcoDibujoPanel.AutoScrollPosition.X - Desplazamiento) * -1, MarcoDibujoPanel.AutoScrollPosition.Y * -1)

                        Case Keys.Right
                            Dim Desplazamiento As Integer = CInt((MarcoDibujoPanel.Width - ImagePictureBox.Width) / 10)

                            MarcoDibujoPanel.AutoScrollPosition = New Point((MarcoDibujoPanel.AutoScrollPosition.X + Desplazamiento) * -1, MarcoDibujoPanel.AutoScrollPosition.Y * -1)
                    End Select
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        Private Sub ImagePictureBox_Paint(ByVal sender As System.Object, ByVal e As Windows.Forms.PaintEventArgs) Handles ImagePictureBox.Paint
            MovablePaint(e)
        End Sub

        Private Sub ImagePictureBox_MouseDown(ByVal sender As System.Object, ByVal e As Windows.Forms.MouseEventArgs) Handles ImagePictureBox.MouseDown
            MovableMouseDown(e)
        End Sub

        Private Sub ImagePictureBox_MouseMove(ByVal sender As System.Object, ByVal e As Windows.Forms.MouseEventArgs) Handles ImagePictureBox.MouseMove

            MovableMouseMove(e)
        End Sub

        Private Sub ImagePictureBox_MouseUp(ByVal sender As System.Object, ByVal e As Windows.Forms.MouseEventArgs) Handles ImagePictureBox.MouseUp
            MovableMouseUp(e)
        End Sub

        Private Sub ImagePictureBox_DragEnter(sender As System.Object, e As Windows.Forms.DragEventArgs) Handles ImagePictureBox.DragEnter
            ImagePictureBox.Refresh()
        End Sub

        Private Sub ImagePictureBox_DragLeave(sender As System.Object, e As EventArgs) Handles ImagePictureBox.DragLeave
            ImagePictureBox.Refresh()
        End Sub

        Private Sub ImagePictureBox_MouseLeave(sender As System.Object, e As EventArgs) Handles ImagePictureBox.MouseLeave
            ImagePictureBox.Refresh()
        End Sub

        Private Sub ImagePictureBox_MouseEnter(sender As System.Object, e As EventArgs) Handles ImagePictureBox.MouseEnter
            ImagePictureBox.Refresh()
        End Sub

        Private Sub AjustarAltoToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AjustarAltoToolStripButton.Click
            AjustarAlto()
        End Sub

        Private Sub AjustarAnchoToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AjustarAnchoToolStripButton.Click
            AjustarAncho()
        End Sub

        Private Sub ZoomOutToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ZoomOutToolStripButton.Click
            ZoomOut()
        End Sub

        Private Sub ZoomToolStripComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ZoomToolStripComboBox.SelectedIndexChanged
            ActualizarValorZoom(ZoomToolStripComboBox.Text)
        End Sub

        Private Sub ZoomToolStripComboBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles ZoomToolStripComboBox.KeyDown
            If e.Control Then
                Select Case e.KeyCode
                    Case Keys.PageUp, Keys.PageDown, Keys.Up, Keys.Down, Keys.Left, Keys.Right
                        e.SuppressKeyPress = True

                End Select
            End If
        End Sub

        Private Sub ZoomToolStripComboBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles ZoomToolStripComboBox.KeyUp
            If e.KeyCode = Keys.Enter Then
                ActualizarValorZoom(ZoomToolStripComboBox.Text)
            End If
        End Sub

        Private Sub ZoomInToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ZoomInToolStripButton.Click
            ZoomIn()
        End Sub

        Private Sub ShowThumbnailToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ShowThumbnailToolStripButton.Click
            ShowThumbnail()
        End Sub

        Private Sub ShowAccesosToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ShowAccesosToolStripButton.Click
            ShowAccesos()
        End Sub

        Private Sub ReprocesoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReprocesoButton.Click
            Reproceso()
        End Sub

        Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SaveButton.Click
            Save()
        End Sub

        Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExitButton.Click
            Cancel()
        End Sub

        Private Sub PreviousFolioButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PreviousFolioButton.Click
            PreviousFolio()
        End Sub

        Private Sub NextFolioButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles NextFolioButton.Click
            NextFolio()
        End Sub

        Private Sub RotateLeftToolStripButton_Click(sender As System.Object, e As EventArgs) Handles RotateLeftToolStripButton.Click
            RotateLeft()
        End Sub

        Private Sub RotateRightToolStripButton_Click(sender As System.Object, e As EventArgs) Handles RotateRightToolStripButton.Click
            RotateRight()
        End Sub

        Private Sub OnDataButtonClick(sender As RecorteDataControl)
            DrawState = DrawStates.WaitState
            If (sender.Selector Is Nothing) Then
                DrawState = DrawStates.PreDrawComponent
            ElseIf (sender.Selector.Folio - 1 <> Me.Controller.CurrentFolioIndex) Then
                Me.Controller.SetCurrentFolio(Controller.CurrentDocumentFile(sender.Selector.Folio - 1), False)
                ShowImagen(False)
            End If

            Me.SelectedDataControl = sender

            Recortar(sender)
            
            UpdateSelected()

            Refrescar()

            EnfocarMarca()
        End Sub

        Private Sub OnDeleteButtonClick(sender As RecorteDataControl)
            DrawState = DrawStates.WaitState
            Recortar(Nothing)
            Refrescar()
        End Sub

#End Region

#Region " Metodos "

        Private Sub AjustarAlto()
            If (ImagePictureBox.Image.Height >= ImagePictureBox.Image.Width) Then
                Me._Zoom = CShort(((MarcoDibujoPanel.Height - 30) / ImagePictureBox.Image.Height) * 100)
            Else
                Me._Zoom = CShort(((MarcoDibujoPanel.Height - 50) / ImagePictureBox.Image.Height) * 100)
            End If

            AplicarZoom()

            MarcoDibujoPanel.AutoScrollPosition = New Point(0, 0)
            ZoomToolStripComboBox.Text = Format(Me._Zoom, "0") & "%"

            If MarcoDibujoPanel.Width < ImagePanel.Width Then
                ImagePanel.Location = New Point(10, ImagePanel.Location.Y)
            End If
        End Sub

        Private Sub AjustarAncho()
            If (ImagePictureBox.Image.Width >= ImagePictureBox.Image.Height) Then
                Me._Zoom = CShort(((MarcoDibujoPanel.Width - 30) / ImagePictureBox.Image.Width) * 100)
            Else
                Me._Zoom = CShort(((MarcoDibujoPanel.Width - 50) / ImagePictureBox.Image.Width) * 100)
            End If

            AplicarZoom()

            MarcoDibujoPanel.AutoScrollPosition = New Point(0, 0)
            ZoomToolStripComboBox.Text = Format(Me._Zoom, "0") & "%"

            If MarcoDibujoPanel.Height < ImagePanel.Height Then
                ImagePanel.Location = New Point(ImagePanel.Location.X, 10)
            End If
        End Sub

        Private Sub RotateLeft()
            If (RotateLeftToolStripButton.Visible AndAlso SelectedDataControl IsNot Nothing) Then
                SelectedDataControl.Selector.RotateLeft()
                ThumbnailPictureBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)

                ThumbnailPictureBox.Refresh()
            End If
        End Sub

        Private Sub RotateRight()
            If (RotateRightToolStripButton.Visible AndAlso SelectedDataControl IsNot Nothing) Then
                SelectedDataControl.Selector.RotateRight()
                ThumbnailPictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)

                ThumbnailPictureBox.Refresh()
            End If
        End Sub

        Private Sub ZoomIn()
            If Me._Zoom < 1000 Then
                Me._Zoom = CShort(Me._Zoom - (Me._Zoom Mod 10) + 10)
                ZoomToolStripComboBox.Text = Format(Me._Zoom, "0") & "%"
                AplicarZoom()

            End If
        End Sub

        Private Sub ZoomOut()
            If Me._Zoom > 10 Then
                If (Me._Zoom Mod 10 = 0) Then
                    Me._Zoom = CShort(Me._Zoom - 10)
                Else
                    Me._Zoom = CShort(Me._Zoom - (Me._Zoom Mod 10))
                End If

                ZoomToolStripComboBox.Text = Format(Me._Zoom, "0") & "%"

                AplicarZoom()
            End If
        End Sub

        Private Sub ActualizarValorZoom(ByVal nZoom As String)
            Dim Valor As String

            Valor = nZoom
            Valor = Valor.TrimEnd("%"c)
            Valor = Valor.TrimEnd(" "c)

            If IsNumeric(Valor) Then
                If Val(Valor) < 10 Or Val(Valor) > 1000 Then
                    MessageBox.Show("El valor debe se mayor o igual a 10 y menor o igual a 1000", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    ZoomToolStripComboBox.Text = Me._Zoom & "%"
                    Return
                Else
                    Me._Zoom = CShort(Valor)
                End If
            End If

            ZoomToolStripComboBox.Text = Me._Zoom & "%"

            AplicarZoom()
        End Sub

        Private Sub MejorAjuste()
            Try

                If Not ((ImagePictureBox.Image.Height / MarcoDibujoPanel.Height) >= (ImagePictureBox.Image.Width / MarcoDibujoPanel.Width)) Then
                    AjustarAlto()
                Else
                    AjustarAncho()
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub AplicarZoom()
            If Not ImagePictureBox.Image Is Nothing Then
                Dim ZoomReal As Single = CSng((Me._Zoom / 100))
                Dim NewWidth As Integer
                Dim NewHeight As Integer
                Dim IPosX, IPosY, ASPosX, ASPosY As Integer
                Dim DiffPosX, DiffPosY As Integer
                Dim BarraX, BarraY As Integer

                NewWidth = CInt((ImagePictureBox.Image.Width * ZoomReal) + 20)
                NewHeight = CInt((ImagePictureBox.Image.Height * ZoomReal) + 20)

                If (MarcoDibujoPanel.Size.Width <= NewWidth) Then BarraX = 20
                If (MarcoDibujoPanel.Size.Height <= NewHeight) Then BarraY = 20

                If (MarcoDibujoPanel.Size.Width - BarraY > NewWidth) Then
                    ' Mas pequeño que el marco
                    ASPosX = 0
                    IPosX = CInt((MarcoDibujoPanel.Size.Width - BarraY - NewWidth) / 2)
                Else
                    ' Mas grande que el marco
                    DiffPosX = (NewWidth - ImagePanel.Size.Width)

                    If ImagePanel.Location.X > 0 Then
                        ASPosX = CInt((-1 * MarcoDibujoPanel.AutoScrollPosition.X) + (DiffPosX / 2) - ImagePanel.Location.X)
                    Else
                        ASPosX = CInt((-1 * MarcoDibujoPanel.AutoScrollPosition.X) + (DiffPosX / 2))
                    End If

                    If ASPosX < 0 Then ASPosX *= -1

                    IPosX = 0

                End If

                If (MarcoDibujoPanel.Size.Height - BarraX > NewHeight) Then
                    ASPosY = 0
                    IPosY = CInt((MarcoDibujoPanel.Size.Height - BarraX - NewHeight) / 2)
                Else
                    DiffPosY = (NewHeight - ImagePanel.Size.Height)

                    If ImagePanel.Location.X > 0 Then
                        ASPosY = CInt((-1 * MarcoDibujoPanel.AutoScrollPosition.Y) + (DiffPosY / 2) - ImagePanel.Location.Y)
                    Else
                        ASPosY = CInt((-1 * MarcoDibujoPanel.AutoScrollPosition.Y) + (DiffPosY / 2))
                    End If

                    If ASPosY < 0 Then ASPosY *= -1

                    IPosY = 0
                End If

                MarcoDibujoPanel.AutoScrollPosition = New Point(0, 0)
                ImagePanel.SetBounds(IPosX, IPosY, NewWidth, NewHeight)
                MarcoDibujoPanel.AutoScrollPosition = New Point(ASPosX, ASPosY)


            End If
        End Sub

        Private Sub Reproceso()
            Dim f = New FormReprocesoMotivo()
            f.LoadData(Me.Controller.MotivosReproceso())
            If (f.ShowDialog() = DialogResult.OK) Then
                If (Controller.Reproceso(f.Motivo)) Then
                    Me.DialogResult = DialogResult.Yes
                    Me.Close()
                End If
            End If
        End Sub

        Private Sub ShowThumbnail()
            ShowThumbnailToolStripButton.Checked = Not ShowThumbnailToolStripButton.Checked
            BackThumbnailPanel.Visible = ShowThumbnailToolStripButton.Checked

            If (BackThumbnailPanel.Visible) Then
                ThumbnailHelper.BeginDrag()
            Else
                ThumbnailHelper.EndDrag()
            End If
        End Sub

        Private Sub ShowAccesos()
            Dim AccesosForm As New FormAccesosRapidos

            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + F1", "Mostrar este listado de teclas de acceso rapido"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + G", "Guardar los cambios"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + E", "Enviar el documento a Reproceso"))

            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Shift] + [PageDown]", "Ver el siguiente folio"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Shift] + [PageUp]", "Ver el folio anterior"))

            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + S", "Mostrar u ocultar el esquema de indexación"))

            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + A", "Ajustar la imagen a la pantalla"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + W", "Ajustar la imagen al ancho de la pantalla"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + H", "Ajustar la imagen al alto de la pantalla"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + R", "Rotar la imagen a la derecha"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + L", "Rotar la imagen a la izquierda"))

            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + '+'", "Acercar la imagen"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + '-'", "Alejar la imagen"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + [Up]", "Desplazar imagen hacia abajo"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + [Down]", "Desplazar imagen hacia arriba"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + [Left]", "Desplazar imagen hacia la derecha"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + [Right]", "Desplazar imagen hacia izquierda"))

            AccesosForm.ShowDialog()
        End Sub

        Private Sub Save()
            Dim Result = MessageBox.Show("Está seguro que desea almacenar los cambios realizados hasta el momento?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If (Result = DialogResult.Yes) Then

                If (Controller.Save()) Then
                    _Unlock = False

                    Me.ImagePictureBox.Image = Nothing
                    Me.DialogResult = DialogResult.Yes
                    Me.Close()
                End If
            End If
        End Sub

        Private Sub Cancel()
            Dim Respuesta As DialogResult

            If (Controller.PreguntarSalida) Then
                Respuesta = MessageBox.Show("¿Esta seguro que desea cancelar el proceso actual y perder los cambios realizados?", Program.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Else
                Respuesta = DialogResult.Yes
            End If

            If Respuesta = DialogResult.Yes Then
                Me.DialogResult = DialogResult.Cancel
                Me.Clear()
                Me.Close()
            End If
        End Sub

        Private Sub MovableMouseDown(ByVal e As Windows.Forms.MouseEventArgs)
            InicialX = e.X
            InicialY = e.Y

            Select Case DrawState
                Case DrawStates.WaitState
                    SelectedDataControl = FindComponent(e.X, e.Y)
                    UpdateSelected()

                    If Not SelectedDataControl Is Nothing Then
                        DrawState = DrawStates.ComponentSelected
                        ImagePictureBox.Refresh()
                    End If

                Case DrawStates.ComponentSelected
                    Dim MovableMode As Movables

                    MovableMode = SelectedDataControl.Selector.IsMovable(e.X, e.Y)

                    Select Case MovableMode
                        Case Movables.NoMovable
                            ImagePictureBox.Cursor = Cursors.Default

                            SelectedDataControl = FindComponent(e.X, e.Y)
                            UpdateSelected()

                            If (SelectedDataControl IsNot Nothing) Then
                                DrawState = DrawStates.ComponentSelected
                            Else
                                DrawState = DrawStates.WaitState
                            End If

                            Refrescar()

                        Case Movables.movableUpLeft
                            DrawState = DrawStates.MovingUpLeft

                        Case Movables.movableLeft
                            DrawState = DrawStates.MovingLeft

                        Case Movables.movableDownLeft
                            DrawState = DrawStates.MovingDownLeft

                        Case Movables.movableUpRight
                            DrawState = DrawStates.MovingUpRight

                        Case Movables.movableRight
                            DrawState = DrawStates.MovingRight

                        Case Movables.movableDownRight
                            DrawState = DrawStates.MovingDownRight

                        Case Movables.movableUP
                            DrawState = DrawStates.MovingUp

                        Case Movables.movableDown
                            DrawState = DrawStates.MovingDown

                        Case Movables.MovableAll
                            DrawState = DrawStates.MovingAll

                    End Select

                Case DrawStates.PreDrawComponent
                    DrawState = DrawStates.DrawComponent

                Case DrawStates.DrawComponent


            End Select
        End Sub

        Private Sub MovableMouseMove(e As MouseEventArgs)
            If (SelectedDataControl Is Nothing) Then
                DrawState = DrawStates.WaitState
            End If

            Select Case DrawState
                Case DrawStates.ComponentSelected
                    Dim MovableMode As Movables

                    SelectedDataControl.Selector.Escala = _Zoom
                    MovableMode = SelectedDataControl.Selector.IsMovable(e.X, e.Y)

                    Select Case MovableMode
                        Case Movables.NoMovable
                            ImagePictureBox.Cursor = Cursors.Default

                        Case Movables.movableUpLeft
                            ImagePictureBox.Cursor = Cursors.SizeNWSE

                        Case Movables.movableLeft
                            ImagePictureBox.Cursor = Cursors.SizeWE

                        Case Movables.movableDownLeft
                            ImagePictureBox.Cursor = Cursors.SizeNESW

                        Case Movables.movableUpRight
                            ImagePictureBox.Cursor = Cursors.SizeNESW

                        Case Movables.movableRight
                            ImagePictureBox.Cursor = Cursors.SizeWE

                        Case Movables.movableDownRight
                            ImagePictureBox.Cursor = Cursors.SizeNWSE

                        Case Movables.movableUP
                            ImagePictureBox.Cursor = Cursors.SizeNS

                        Case Movables.movableDown
                            ImagePictureBox.Cursor = Cursors.SizeNS

                        Case Movables.MovableAll
                            ImagePictureBox.Cursor = Cursors.SizeAll

                    End Select

                Case DrawStates.PreDrawComponent
                    ImagePictureBox.Refresh()
                    ImagePictureBox.Cursor = Cursors.Cross

                Case DrawStates.DrawComponent
                    ImagePictureBox.Refresh()

                    Dim objGraphics As Graphics
                    Dim WhitePen As New Pen(Color.Orange, 2)

                    objGraphics = ImagePictureBox.CreateGraphics()

                    objGraphics.DrawRectangle(WhitePen, InicialX, InicialY, oldX - InicialX, oldY - InicialY)

                Case DrawStates.MovingUpLeft
                    SelectedDataControl.Selector.MovingUpLeft(e.X - oldX, e.Y - oldY)
                    ImagePictureBox.Refresh()

                Case DrawStates.MovingLeft
                    SelectedDataControl.Selector.MovingLeft(e.X - oldX, e.Y - oldY)
                    ImagePictureBox.Refresh()

                Case DrawStates.MovingDownLeft
                    SelectedDataControl.Selector.MovingDownLeft(e.X - oldX, e.Y - oldY)
                    ImagePictureBox.Refresh()

                Case DrawStates.MovingUpRight
                    SelectedDataControl.Selector.MovingUpRight(e.X - oldX, e.Y - oldY)
                    ImagePictureBox.Refresh()

                Case DrawStates.MovingRight
                    SelectedDataControl.Selector.MovingRight(e.X - oldX, e.Y - oldY)
                    ImagePictureBox.Refresh()

                Case DrawStates.MovingDownRight
                    SelectedDataControl.Selector.MovingDownRight(e.X - oldX, e.Y - oldY)
                    ImagePictureBox.Refresh()

                Case DrawStates.MovingUp
                    SelectedDataControl.Selector.MovingUp(e.X - oldX, e.Y - oldY)
                    ImagePictureBox.Refresh()

                Case DrawStates.MovingDown
                    SelectedDataControl.Selector.MovingDown(e.X - oldX, e.Y - oldY)
                    ImagePictureBox.Refresh()

                Case DrawStates.MovingAll
                    SelectedDataControl.Selector.MovingAll(e.X - oldX, e.Y - oldY)
                    ImagePictureBox.Refresh()

            End Select

            oldX = e.X
            oldY = e.Y
        End Sub

        Private Sub MovableMouseUp(e As MouseEventArgs)
            Select Case DrawState
                Case DrawStates.DrawComponent
                    If (e.X + 10 >= InicialX And e.Y + 10 >= InicialY) Then
                        Dim fX = NuevaCoordenada(InicialX)
                        Dim fY = NuevaCoordenada(InicialY)
                        Dim fW = NuevaCoordenada(e.X - InicialX)
                        Dim fH = NuevaCoordenada(e.Y - InicialY)

                        Dim newSelector = New Selector(SelectedDataControl)
                        newSelector.Posicion.X = fX
                        newSelector.Posicion.Y = fY
                        newSelector.Tamaño.Width = fW
                        newSelector.Tamaño.Height = fH

                        newSelector.Folio = CShort(Me.Controller.CurrentFolioIndex + 1)
                        SelectedDataControl.Selector = newSelector

                        DrawState = DrawStates.ComponentSelected
                    End If

                Case DrawStates.MovingUpLeft
                    DrawState = DrawStates.ComponentSelected

                Case DrawStates.MovingLeft
                    DrawState = DrawStates.ComponentSelected

                Case DrawStates.MovingDownLeft
                    DrawState = DrawStates.ComponentSelected

                Case DrawStates.MovingUpRight
                    DrawState = DrawStates.ComponentSelected

                Case DrawStates.MovingRight
                    DrawState = DrawStates.ComponentSelected

                Case DrawStates.MovingDownRight
                    DrawState = DrawStates.ComponentSelected

                Case DrawStates.MovingUp
                    DrawState = DrawStates.ComponentSelected

                Case DrawStates.MovingDown
                    DrawState = DrawStates.ComponentSelected

                Case DrawStates.MovingAll
                    DrawState = DrawStates.ComponentSelected

            End Select

            Recortar(SelectedDataControl)
            Refrescar()
        End Sub

        Private Sub MovablePaint(e As PaintEventArgs)
            For Each componente In Me.Recortes
                If (componente.Selector IsNot Nothing AndAlso componente.Selector.Folio = Me.Controller.CurrentFolioIndex + 1) Then
                    componente.Selector.Escala = _Zoom
                    componente.Selector.Draw(e.Graphics)
                End If
            Next

            If (Not SelectedDataControl Is Nothing) Then
                If (SelectedDataControl.Selector IsNot Nothing) Then
                    SelectedDataControl.Selector.Selected(e.Graphics)
                End If
            End If
        End Sub

        Private Sub Refrescar()
            ImagePictureBox.Refresh()
        End Sub

        Private Sub Recortar(dataControl As RecorteDataControl)
            If (SelectedDataControl IsNot Nothing AndAlso SelectedDataControl.Selector IsNot Nothing) Then
                ThumbnailPictureBox.Image = CropBitmap(New Bitmap(ImagePictureBox.Image), dataControl.Selector.Posicion.X, dataControl.Selector.Posicion.Y, dataControl.Selector.Tamaño.Width, dataControl.Selector.Tamaño.Height)

                Select Case dataControl.Selector.Angulo
                    Case 90 : ThumbnailPictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                    Case 180 : ThumbnailPictureBox.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)
                    Case 270 : ThumbnailPictureBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                End Select

                RotateLeftToolStripButton.Visible = True
                RotateRightToolStripButton.Visible = True
            Else
                ThumbnailPictureBox.Image = Nothing

                RotateLeftToolStripButton.Visible = False
                RotateRightToolStripButton.Visible = False
            End If

            ThumbnailPictureBox.Refresh()
        End Sub

        Public Sub Cargar()
            Dim RecortesDataTable = CType(Controller, IRecorteController).GetRecortes()

            Recortes.Clear()

            For Each item In RecortesDataTable
                Dim newDataControl = New RecorteDataControl() With
                    {
                        .id = item.id_File_Recorte,
                        .Label = item.Etiqueta,
                        .Data = item
                    }

                Recortes.Add(newDataControl)

                If (item.Procesado) Then
                    Dim newSelector = New Selector(newDataControl)
                    newSelector.Posicion.X = item.X
                    newSelector.Posicion.Y = item.Y
                    newSelector.Folio = item.Folio
                    newSelector.Tamaño.Height = item.Alto
                    newSelector.Tamaño.Width = item.Ancho

                    If (item.IsAnguloNull()) Then
                        newSelector.Angulo = 0
                    Else
                        newSelector.Angulo = item.Angulo
                    End If
                    
                    newDataControl.Selector = newSelector
                End If
            Next

            DataControlPanel.Controls.Clear()

            For i As Integer = Recortes.Count - 1 To 0 Step -1
                Dim item = Recortes(i)

                item.Dock = Windows.Forms.DockStyle.Top

                AddHandler item.OnDataButtonClick, AddressOf OnDataButtonClick
                AddHandler item.OnDeleteButtonClick, AddressOf OnDeleteButtonClick

                DataControlPanel.Controls.Add(item)
            Next
        End Sub

        Private Sub UpdateSelected()
            For Each item In Recortes
                item.Selected = False
            Next

            If (SelectedDataControl IsNot Nothing) Then
                SelectedDataControl.Selected = True
            End If
        End Sub

        Private Sub EnfocarMarca()
            Try
                If (Me.SelectedDataControl IsNot Nothing) Then
                    If (Me.SelectedDataControl.Selector IsNot Nothing) Then
                        Dim pX, pY, pW, pH As Double
                        Dim H, W As Integer

                        pH = Me.SelectedDataControl.Selector.Tamaño.Height / Me.ImagePictureBox.Image.Height
                        pW = Me.SelectedDataControl.Selector.Tamaño.Width / Me.ImagePictureBox.Image.Width

                        pX = Me.SelectedDataControl.Selector.Posicion.X / Me.ImagePictureBox.Image.Height
                        pY = Me.SelectedDataControl.Selector.Posicion.Y / Me.ImagePictureBox.Image.Width

                        H = Me.SelectedDataControl.Selector.Tamaño.Height
                        W = Me.SelectedDataControl.Selector.Tamaño.Width

                        If ((H / Me.MarcoDibujoPanel.Height) >= (W / Me.MarcoDibujoPanel.Width)) Then
                            Me._Zoom = CShort(((Me.MarcoDibujoPanel.Height - 20) / H) * 75)
                        Else
                            Me._Zoom = CShort(((Me.MarcoDibujoPanel.Width - 20) / W) * 75)
                        End If

                        Me.ZoomToolStripComboBox.Text = Format(_Zoom, "0") & "%"

                        AplicarZoom()

                        H = CInt(Me.ImagePanel.Height * pH)
                        W = CInt(Me.ImagePanel.Width * pW)

                        Dim W1, H1, X1, Y1 As Integer
                        W1 = CInt((Me.ImagePanel.Width - Me.MarcoDibujoPanel.Width) + W)
                        H1 = CInt((Me.ImagePanel.Height - Me.MarcoDibujoPanel.Height) + H)

                        X1 = CInt(W1 * pX)
                        Y1 = CInt(H1 * pY)

                        Me.MarcoDibujoPanel.AutoScrollPosition = New Point(X1, Y1)
                    End If
                End If
            Catch : End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function CropBitmap(ByVal bitmap As Bitmap, ByVal cropX As Integer, ByVal cropY As Integer, ByVal cropWidth As Integer, ByVal cropHeight As Integer) As Bitmap
            If (cropHeight <= 0) Then cropHeight = 10
            If (cropWidth <= 0) Then cropWidth = 10

            Dim rect = New Rectangle(cropX, cropY, cropWidth, cropHeight)
            Dim cropped = bitmap.Clone(rect, bitmap.PixelFormat)
            Return cropped
        End Function

        Public Function FindComponent(ByVal X As Integer, ByVal Y As Integer) As RecorteDataControl
            Dim i As Integer

            For i = Recortes.Count - 1 To 0 Step -1
                If (Recortes(i).Selector IsNot Nothing) Then
                    Recortes(i).Selector.Escala = _Zoom
                    If (Recortes(i).Selector.Folio = Me.Controller.CurrentFolioIndex + 1 AndAlso Recortes(i).Selector.IsSelecting(X, Y)) Then
                        Return Recortes(i)
                    End If
                End If
            Next i

            Return Nothing
        End Function

        Private Function NuevaCoordenada(ByVal Coordenada As Integer) As Integer
            Return CInt(Coordenada / (_Zoom / 100))
        End Function

#End Region

    End Class

End Namespace