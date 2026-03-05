Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO

Namespace Procesos.Configuracion.Imaging

    Public Class FormConfigCampo

#Region " Declaraciones "

        Private _Zoom As Short = 100
        Private _Moviendo As Boolean = False

#End Region

#Region " Propiedades "
        Public mX As Integer
        Public mY As Integer

        Public Property X As Integer
        Public Property Y As Integer
        Public Property W As Integer
        Public Property H As Integer

        Public ReadOnly Property FileName As String
            Get
                Return FileNameTextBox.Text
            End Get
        End Property

#End Region

#Region " Eventos "

        Private Sub FormConfigCampo_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            MejorAjuste()
            ShowMedidas()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub OpenButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles OpenButton.Click
            Cargar()
        End Sub

        Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SaveButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

        Private Sub ImagePictureBox_MouseDown(ByVal sender As System.Object, ByVal e As Windows.Forms.MouseEventArgs) Handles ImagePictureBox.MouseDown
            _Moviendo = True

            _X = CInt(e.X / ImagePictureBox.Width * 100)
            _Y = CInt(e.Y / ImagePictureBox.Height * 100)

            mX = e.X
            mY = e.Y
            _W = 0
            _H = 0
        End Sub

        Private Sub ImagePictureBox_MouseMove(ByVal sender As System.Object, ByVal e As Windows.Forms.MouseEventArgs) Handles ImagePictureBox.MouseMove
            If (_Moviendo) Then
                _W = CInt((e.X - mX) / ImagePictureBox.Width * 100)
                _H = CInt((e.Y - mY) / ImagePictureBox.Height * 100)

                ImagePictureBox.Refresh()
            End If
        End Sub

        Private Sub ImagePictureBox_MouseUp(ByVal sender As System.Object, ByVal e As Windows.Forms.MouseEventArgs) Handles ImagePictureBox.MouseUp
            _Moviendo = False

            If (e.X > _X) Then
                _W = CInt((e.X - mX) / ImagePictureBox.Width * 100)
            Else
                _W = CInt((mX - e.X) / ImagePictureBox.Width * 100)
                _X = CInt(e.X / ImagePictureBox.Width * 100)
            End If

            If (e.Y > _Y) Then
                _H = CInt((e.Y - mY) / ImagePictureBox.Height * 100)
            Else
                _H = CInt((mY - e.Y) / ImagePictureBox.Height * 100)
                _Y = CInt(e.Y / ImagePictureBox.Height * 100)
            End If

            ShowMedidas()
        End Sub

        Private Sub ImagePictureBox_Paint(ByVal sender As System.Object, ByVal e As Windows.Forms.PaintEventArgs) Handles ImagePictureBox.Paint
            Dim nX, nY, nW, nH As Integer

            Try
                nX = CInt(ImagePictureBox.Width * (_X / 100))
                nY = CInt(ImagePictureBox.Height * (_Y / 100))
                nW = CInt(ImagePictureBox.Width * (_W / 100))
                nH = CInt(ImagePictureBox.Height * (_H / 100))

                e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(30, 255, 128, 0)), nX, nY, nW, nH)
                e.Graphics.DrawRectangle(Pens.Orange, nX, nY, nW, nH)
            Catch
            End Try
        End Sub

#End Region

#Region " Metodos "

        Private Sub MejorAjuste()
            If Not ((ImagePictureBox.Image.Height / MarcoDibujoPanel.Height) >= (ImagePictureBox.Image.Width / MarcoDibujoPanel.Width)) Then
                AjustarAlto()
            Else
                AjustarAncho()
            End If
        End Sub

        Private Sub AjustarAlto()
            If (ImagePictureBox.Image.Height >= ImagePictureBox.Image.Width) Then
                Me._Zoom = CShort(((MarcoDibujoPanel.Height - 30) / ImagePictureBox.Image.Height) * 100)
            Else
                Me._Zoom = CShort(((MarcoDibujoPanel.Height - 50) / ImagePictureBox.Image.Height) * 100)
            End If

            AplicarZoom()

            MarcoDibujoPanel.AutoScrollPosition = New Point(0, 0)

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

            If MarcoDibujoPanel.Height < ImagePanel.Height Then
                ImagePanel.Location = New Point(ImagePanel.Location.X, 10)
            End If
        End Sub

        Private Sub AplicarZoom()
            If Not ImagePictureBox.Image Is Nothing Then
                Dim ZoomReal As Single = CSng((Me._Zoom / 100))
                Dim NewWidth As Integer = CInt((ImagePictureBox.Image.Width * ZoomReal) + 20)
                Dim NewHeight As Integer = CInt((ImagePictureBox.Image.Height * ZoomReal) + 20)
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

        Private Sub ShowMedidas()
            XLabel.Text = "X: " & X
            YLabel.Text = "Y: " & Y
            WLabel.Text = "W: " & W
            HLabel.Text = "H: " & H
        End Sub

        Public Function Cargar() As Boolean
            Dim Selector As New OpenFileDialog

            Selector.Multiselect = False
            Selector.Title = "Seleccionar imagen"
            Selector.Filter = "Archivos de imagen |*.gif;*.jpg;*.png;*.bmp;*.tif|Imagenes GIF (*.gif)|*.gif|Imagenes BMP (*.bmp)|*.bmp|Imagenes JPG (*.jpg)|*.jpg|Imagenes PNG (*.png)|*.png|Imagenes TIFF (*.tif)|*.tif"

            Dim Respuesta = Selector.ShowDialog()

            If Respuesta = Windows.Forms.DialogResult.OK Then
                FileNameTextBox.Text = Selector.FileName
                ImagePictureBox.Image = New Bitmap(Selector.FileName)
                MejorAjuste()

                Return True
            Else
                Return False
            End If
        End Function

        Public Function Cargar(ByVal nFileName As String) As Boolean
            If (File.Exists(nFileName)) Then
                FileNameTextBox.Text = nFileName
                ImagePictureBox.Image = New Bitmap(nFileName)

                MejorAjuste()

                ShowMedidas()
                Return True
            Else
                Return False
            End If
        End Function

#End Region

    End Class

End Namespace