Imports System.Drawing
Imports System.Windows.Forms
Imports Miharu.Imaging.Indexer.View.Comun
Imports Miharu.Imaging.Indexer.Controller
Imports Miharu.Desktop.Library.Config
Imports Miharu.Imaging.Indexer.Controller.Recorte
Imports Slyg.Tools.Imaging

Namespace View.Recorte

    Public Class FormCalidadRecorte
        Implements IView

#Region " Declaraciones "

        Private SelectedDataControl As CalidadDataControl

        Public Recortes As New List(Of CalidadDataControl)

        Private _ViewClosing As Boolean = False

        Private _IndexerController As Object

#End Region

#Region " Constructores "

        Public Sub New()

            InitializeComponent()

            'Me._ThumbnailHelper = New DropThumbnailHelper()
            'Me._ThumbnailHelper.Inicialize(Me, BackThumbnailPanel)
            'AddHandler Me._ThumbnailHelper.OnChangeImagePosition, AddressOf ThumbnailHelper_OnChangeImagePosition

        End Sub

#End Region

#Region " Propiedades "

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
                Return Nothing '_ThumbnailHelper
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
                Return Nothing
            End Get
        End Property

        Sub ScrollThumbnail() Implements IView.ScrollThumbnail

        End Sub

        Friend Sub Thumbnail_MouseEnter(ByVal sender As System.Object, ByVal e As EventArgs) Implements IView.Thumbnail_MouseEnter

        End Sub

        Friend Sub Thumbnail_MouseLeave(ByVal sender As System.Object, ByVal e As EventArgs) Implements IView.Thumbnail_MouseLeave

        End Sub

        Friend Sub Thumbnail_Click(ByVal sender As System.Object, ByVal e As EventArgs) Implements IView.Thumbnail_Click

        End Sub

        Public Sub Clear() Implements IView.Clear
            If (ImagePictureBox.Image IsNot Nothing) Then ImagePictureBox.Image.Dispose()

            ImagePictureBox.Image = Nothing
        End Sub

        Public Sub ActivarControles(ByVal nActivo As Boolean) Implements IView.ActivarControles
            Dim index = CType(Me.Controller, IRecorteController).CurrentRecorteIndex
            PreviousFolioButton.Enabled = (index > 0) And nActivo
            NextFolioButton.Enabled = (index < Me.Recortes.Count) And nActivo
        End Sub

        Public Overloads Function ShowDialog() As DialogResult Implements IView.ShowDialog
            Cargar()

            SelectedDataControl = Recortes(0)

            ShowImagen(False)

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

            ImagePictureBox.Image = CropBitmap(CType(Me.Controller.CurrentImage, Bitmap), SelectedDataControl.Selector.Posicion.X, SelectedDataControl.Selector.Posicion.Y, SelectedDataControl.Selector.Tamaño.Width, SelectedDataControl.Selector.Tamaño.Height)

            Select Case SelectedDataControl.Selector.Angulo
                Case 90 : ImagePictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                Case 180 : ImagePictureBox.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)
                Case 270 : ImagePictureBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
            End Select

            ImagePanel.AutoScrollPosition = New Point(0, 0)

            If (ImageTemp IsNot Nothing) Then ImageTemp.Dispose()

            UpdateSelected()

            ActivarControles(True)
        End Sub

        Private Function PreviousRecorte() As Boolean Implements IView.PreviousFolio
            Dim UpdateInfo As Boolean = False

            If (Me.Controller.PreviousFolio(UpdateInfo)) Then
                SelectedDataControl = Recortes(CType(Controller, IRecorteController).CurrentRecorteIndex)
                ShowImagen(False)

                ActivarControles(True)
                Return True
            End If

            Return False
        End Function

        Private Sub NextRecorte() Implements IView.NextFolio
            Dim UpdateInfo As Boolean = False

            If (Me.Controller.NextFolio(UpdateInfo)) Then
                SelectedDataControl = Recortes(CType(Controller, IRecorteController).CurrentRecorteIndex)
                ShowImagen(False)

                ActivarControles(True)
            ElseIf (CType(Me.Controller, IRecorteController).CurrentRecorteIndex = Me.Recortes.Count - 1) Then
                ' Preguntar si se finaliza el el indexado
                Dim Respuesta As DialogResult
                Respuesta = MessageBox.Show("No se encontraron más recortes para validar, ¿desea dar por terminado el proceso y almacenar los datos?", Program.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

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

            ImagePictureBox.Dispose()
            'Función Agregada Para Obligar Liberación De Memoria
            Utilities.ClearMemory()

        End Sub

        Private Sub FormIndexerView_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
            Try
                If (e.Control) Then
                    Select Case e.KeyCode
                        Case Keys.F1
                            ShowAccesos()

                        Case Keys.G
                            If (SaveButton.Enabled And SaveButton.Visible) Then Save()

                    End Select
                ElseIf (e.Shift) Then
                    Select Case e.KeyCode
                        Case Keys.PageDown
                            If (NextFolioButton.Enabled And NextFolioButton.Visible) Then NextRecorte()

                        Case Keys.PageUp
                            If (PreviousFolioButton.Enabled And PreviousFolioButton.Visible) Then PreviousRecorte()

                    End Select

                ElseIf (e.Alt) Then
                    Select Case e.KeyCode
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

        Private Sub ShowAccesosToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ShowAccesosToolStripButton.Click
            ShowAccesos()
        End Sub

        Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SaveButton.Click
            Save()
        End Sub

        Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExitButton.Click
            Cancel()
        End Sub

        Private Sub PreviousFolioButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PreviousFolioButton.Click
            PreviousRecorte()
        End Sub

        Private Sub NextFolioButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles NextFolioButton.Click
            NextRecorte()
        End Sub

        Private Sub OnDataButtonClick(sender As CalidadDataControl)
            SelectedDataControl = sender

            If (sender.Selector.Folio - 1 <> Me.Controller.CurrentFolioIndex) Then
                Me.Controller.SetCurrentFolio(Controller.CurrentDocumentFile(sender.Selector.Folio - 1), False)
            End If

            CType(Me.Controller, IRecorteController).CurrentRecorteIndex = SelectedDataControl.id - 1

            ShowImagen(False)

            Refrescar()
        End Sub

        Private Sub OnOkButtonClick(sender As CalidadDataControl)
            NextRecorte()
        End Sub

        Private Sub OnErrorButtonClick(sender As CalidadDataControl)
            NextRecorte()
        End Sub

#End Region

#Region " Metodos "

        Private Sub ShowAccesos()
            Dim AccesosForm As New FormAccesosRapidos

            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + F1", "Mostrar este listado de teclas de acceso rapido"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + G", "Guardar los cambios"))

            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Shift] + [PageDown]", "Ver el siguiente folio"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Shift] + [PageUp]", "Ver el folio anterior"))

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

        Private Sub Refrescar()
            ImagePictureBox.Refresh()
        End Sub

        Public Sub Cargar()
            Dim RecortesDataTable = CType(Controller, IRecorteController).GetRecortes()

            Recortes.Clear()

            For Each item In RecortesDataTable
                Dim newDataControl = New CalidadDataControl() With
                    {
                        .id = item.id_File_Recorte,
                        .Label = item.Etiqueta,
                        .Data = item
                    }

                Recortes.Add(newDataControl)

                Dim newSelector = New Selector(newDataControl)
                newSelector.Posicion.X = item.X
                newSelector.Posicion.Y = item.Y
                newSelector.Folio = item.Folio
                newSelector.Tamaño.Height = item.Alto
                newSelector.Tamaño.Width = item.Ancho
                newSelector.Angulo = item.Angulo

                newDataControl.Selector = newSelector
            Next

            DataControlPanel.Controls.Clear()

            For i As Integer = Recortes.Count - 1 To 0 Step -1
                Dim item = Recortes(i)

                item.Dock = Windows.Forms.DockStyle.Top

                AddHandler item.OnDataButtonClick, AddressOf OnDataButtonClick
                AddHandler item.OnOkButtonClick, AddressOf OnOkButtonClick
                AddHandler item.OnErrorButtonClick, AddressOf OnErrorButtonClick

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

#End Region

#Region " Funciones "

        Private Function CropBitmap(ByVal bitmap As Bitmap, ByVal cropX As Integer, ByVal cropY As Integer, ByVal cropWidth As Integer, ByVal cropHeight As Integer) As Bitmap
            Dim x1, x2, x3, x4 As Integer
            Dim y1, y2, y3, y4 As Integer

            x1 = 0
            y1 = 0

            x2 = cropX
            y2 = cropY

            cropX -= 20
            cropY -= 20

            If (cropX < 0) Then cropX = 0
            If (cropY < 0) Then cropX = 0

            x2 = x2 - cropX
            y2 = y2 - cropY

            x3 = x2 + cropWidth
            y3 = y2 + cropHeight

            cropWidth += 40
            cropHeight += 40
            If (cropX + cropWidth > bitmap.Width) Then cropWidth = bitmap.Width - cropX
            If (cropY + cropHeight > bitmap.Height) Then cropHeight = bitmap.Height - cropY

            x4 = cropWidth
            y4 = cropHeight

            Dim rect = New Rectangle(cropX, cropY, cropWidth, cropHeight)
            Dim cropped = bitmap.Clone(rect, bitmap.PixelFormat)

            Dim g = Graphics.FromImage(cropped)

            ' Dibujar recuadro
            Dim cropRectangle = New Rectangle(x2, y2, x3 - x2, y3 - y2)
            Dim topRectangle = New Rectangle(x1, y1, x4 - x1, y2 - y1)
            Dim bottomRectangle = New Rectangle(x1, y3, x4 - x1, y4 - y3)
            Dim leftRectangle = New Rectangle(x1, y2, x2 - x1, y3 - y2)
            Dim rightRectangle = New Rectangle(x3, y2, x4 - x3, y3 - y2)

            Dim backgroundBrush As New SolidBrush(Color.FromArgb(70, 255, 165, 0))
            Dim linePen As New Pen(Color.Orange, 2)

            g.FillRectangle(backgroundBrush, topRectangle)
            g.FillRectangle(backgroundBrush, bottomRectangle)
            g.FillRectangle(backgroundBrush, leftRectangle)
            g.FillRectangle(backgroundBrush, rightRectangle)
            g.DrawRectangle(linePen, cropRectangle)

            Return cropped
        End Function

#End Region

    End Class

End Namespace