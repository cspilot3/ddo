Imports System.Windows.Forms
Imports System.Drawing
Imports Miharu.Imaging.Indexer.View.Comun
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools.Imaging
Imports Miharu.Imaging.OffLineViewer.Library.Visor
Imports System.IO
Imports Miharu.Imaging.Indexer.Controller.Indexer.Capturas
Imports Miharu.Imaging.Indexer.Controller.Indexer.Capturas.Captura
Imports System.Text
Imports System.Threading.Tasks
Imports System.Drawing.Imaging
Imports Miharu.Imaging.Indexer.View.Indexacion.Table
Imports MyTableInputControl = Miharu.Imaging.Indexer.View.Indexacion.TableInputControl
Imports Miharu.Imaging.Indexer.View.Indexacion.TableInputControl
Imports Miharu.Imaging.Indexer.Controller.Indexer.Capturas.OCR


Namespace View.Indexacion

    Public Class FormIndexerView
        Implements IIndexerView, IView

#Region " Declaraciones "
        Private _Controller As Controller.IController
        Private _IndexerController As Controller.Indexer.IIndexerController

        Private _Zoom As Short = 100
        Private ReadOnly _ZoomMAXEnfMarca As Short = 200            ' Zoom máximo permitido para enfocarMarca()

        Private _PreguntarPreviousImage As Boolean

        Private _ViewClosing As Boolean = False

        Private _Campos As List(Of CampoCaptura)
        Private _CamposLlave As List(Of CampoLlaveCaptura)
        Private Validaciones As List(Of ValidacionCaptura)
        Private ValidacionEmbargos As Boolean

        Private _OCRCaptura As IOCRCaptura                     ' Clase para manejar el proceso de OCR
        Private _SelectedInputControl As IInputControl
        Private _SelectedValidationControl As ValidationControl
        Private _ThumbnailHelper As DropThumbnailHelper

        Delegate Sub ShowDelegate(ByVal nShow As Boolean)
        Public ValidarRegistros As Integer
        Public ValidarTotal As Integer

        Private SelectedTableInputControl As TableInputControl

        Private _CancelProcess As Boolean

        Private Class AnexoClass
            Public Property idAnexo As Integer
            Public Property NombreAnexo As String
            Public Property TabPage As TabPage
            Public Property ImageViewer As ImageViewer
        End Class

        Private AnexosList As New List(Of AnexoClass)

        'set para dibujar rectangulo tabla
        Private IsDrawingEnabled As Boolean = False                                     ' Variable control para dibujar tabla OCR 
        Private rectangleStartPoint As Point                                            ' Punto inicial del rectangulo OCR
        Private rectangleEndPoint As Point                                              ' punto final del rectangulo OCR 
        Private IsDrawingTableEnabled As Boolean = False                                     ' Variable control para dibujar tabla OCR 
        Private rectangleTableStartPoint As Point                                       ' Punto inicial del rectangulo Bordes Tabla OCR
        Private rectangleTableEndPoint As Point                                         ' punto final del rectangulo Bordes Tabla OCR

        Private drawRectangle As Rectangle                                              ' Almacena las coordenadas finales del rectangulo dibujado
        Private drawRectangleTable As Rectangle                                         ' Almacena las coordenadas finales del rectangulo dibujado bordes Tabla OCR
        Dim ZoomDrawingRectangle As Single                                              ' Factor de zoom utilizado para el dibujo de rectángulos en la imagen.

        'set para dibujar lineas
        Private isDrawingLine As Boolean = False                                        ' Indicador para dibujar lineaHorizontal
        Dim selectedOrientationLine As LineOrientation?                                 ' Variable almacena estado de la orientacion d ela linea seleccionado (admite Nothing)
        Private horizontalLinesList As List(Of Point()) = New List(Of Point())          ' Almacena las lineas dibujadas horizontalmente
        Private verticalLinesList As List(Of Point()) = New List(Of Point())            ' Almacena las lineas dibujadas verticalmente

        Private Property dataResultDictionaryOcr As Dictionary(Of Integer, List(Of String()))
        Private Property dataResultListOcr As List(Of String)

#End Region

#Region "Enum"

        Public Enum LineOrientation
            Horizontal
            Vertical
        End Enum

#End Region

#Region " Constructores "

        Public Sub New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            Me.CapturaIndexerTable.SetView(Me)

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me._ThumbnailHelper = New DropThumbnailHelper()
            Me._ThumbnailHelper.Inicialize(Me, BackThumbnailPanel)

            AddHandler Me._ThumbnailHelper.OnChangeImagePosition, AddressOf ThumbnailHelper_OnChangeImagePosition

        End Sub

#End Region

#Region " Implementacion IView "

        Public ReadOnly Property ThumbnailHelper As DropThumbnailHelper Implements IView.ThumbnailHelper
            Get
                Return _ThumbnailHelper
            End Get
        End Property

        Public ReadOnly Property ViewClosing As Boolean Implements IView.ViewClosing
            Get
                Return _ViewClosing
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

        Public ReadOnly Property Controller As Controller.IController Implements IView.Controller
            Get
                Return _Controller 'CType(, Controller.IController)
            End Get
        End Property

        Public Sub SetController(nController As Object) Implements IView.SetController
            _Controller = CType(nController, Controller.IController)
            _IndexerController = CType(nController, Controller.Indexer.IIndexerController)

            If _Controller.IndexerSesion.IsExternal Then
                _OCRCaptura = New OCRCapturaDMZ()
            Else
                _OCRCaptura = New OCRCaptura()
            End If

            _OCRCaptura.SetController(_Controller)
        End Sub

        Public Property Image As FreeImageAPI.FreeImageBitmap Implements IView.Image
            Get
                Return CType(ImagePictureBox.Image, FreeImageAPI.FreeImageBitmap)
            End Get
            Set(ByVal value As FreeImageAPI.FreeImageBitmap)
                ImagePictureBox.Image = CType(value, Bitmap)
            End Set
        End Property

        Public Property Unlock As Boolean Implements IView.Unlock

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

                If Folio.Parent.TipoDocumento = 0 Then
                    MensajeToolStripStatusLabel.Text = "Tipo Documental: " & TipoDocumentalComboBox.Text & " - Folios: " & Folio.Parent.Folios
                Else
                    MensajeToolStripStatusLabel.Text = "Tipo Documental: " & Folio.Parent.NombreTipoDocumento & " - Folios: " & Folio.Parent.Folios
                End If
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

                    ShowImagen(UpdateInfo)

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

        Public Overloads Function ShowDialog() As DialogResult Implements IView.ShowDialog
            ShowImagen(False)

            ShowAnexos()

            Return MyBase.ShowDialog()
        End Function

        Public Sub SetTitle(ByVal title As String) Implements IView.SetTitle
            Me.Text = title
        End Sub

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
            If Me.Controller.Cargado And Me.Controller.ImageCount > 0 Then
                Try
                    If Me.Controller.CurrentFolio.Id_Item_Folio > 1 Then
                        ToolTipLabel.BackColor = Color.Red
                    Else
                        ToolTipLabel.BackColor = Color.GreenYellow
                    End If
                    Me.ToolTipLabel.Font = New System.Drawing.Font("Roboto Mono", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    ToolTipLabel.Text = Me.Controller.CurrentFolio.NombreImagen
                Catch
                    Throw
                End Try
            End If

            Application.DoEvents()
        End Sub

        Public Sub ShowImagen(ByVal UpdateInfo As Boolean) Implements IView.ShowImagen
            IndexerController.ReemplazarImagen = False

            Dim ImageTemp = GetImageFromPictureBox()

            If Not Me.Controller.CurrentFolio Is Nothing Then
                If Not Me.Controller.CurrentFolio.FileName Is Nothing Then
                    ImagePictureBox.Image = CType(Me.Controller.CurrentImage, Bitmap)


                    ImagePanel.AutoScrollPosition = New Point(0, 0)

                    If (ImageTemp IsNot Nothing) Then ImageTemp.Dispose()

                    If Me.Controller.CurrentDocumentFile.Count = 0 Then
                        AjustarAncho()
                    Else
                        AplicarZoom()
                    End If

                    MensajeToolStripStatusLabel.Text = "Imagen cargada"


                    If (UpdateInfo) Then
                        EsquemaComboBox.SelectedValue = Me.Controller.CurrentFolder.Esquema
                        TipoDocumentalComboBox.SelectedValue = Me.Controller.CurrentDocumentFile.TipoDocumento
                    End If

                    ActivarControles(True)
                End If
            End If


        End Sub

        Public Function PreviousFolio() As Boolean Implements IView.PreviousFolio
            Dim UpdateInfo As Boolean = False

            If (Me.Controller.PreviousFolio(UpdateInfo)) Then
                ShowImagen(UpdateInfo)

                AjustarAncho()

                ActivarControles(True)
                Return True
            End If

            Return False
        End Function

        Public Sub NextFolio() Implements IView.NextFolio
            Dim UpdateInfo As Boolean = False

            If (Me.Controller.NextFolio(UpdateInfo)) Then
                ShowImagen(UpdateInfo)

                AjustarAncho()

                ActivarControles(True)
            ElseIf (Me.Controller.ImageCount = Controller.Folios) Then
                If (Me.SelectedTableInputControl IsNot Nothing) Then
                    If (Me.SelectedTableInputControl.Save()) Then
                        SelectedInputControl.NextControl.Focus()
                        HideGridControl()
                    Else
                        Return
                    End If
                End If

                Try
                    Me.Enabled = False
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
                Finally
                    Me.Enabled = True
                End Try
            Else
                ActivarControles(True)
            End If

            _PreguntarPreviousImage = True
        End Sub

        Public Sub ShowImagenRuta(ByVal path As String) Implements IView.ShowImagenRuta
            IndexerController.ReemplazarImagen = False

            Dim ImageTemp = ImagePictureBox.Image

            If Not Me.Controller.CurrentFolio Is Nothing Then
                If Not Me.Controller.CurrentFolio.FileName Is Nothing Then
                    ImagePictureBox.Image = CType(Me.Controller.CurrentImage, Bitmap)
                End If
            End If


            ImagePanel.AutoScrollPosition = New Point(0, 0)

            If (ImageTemp IsNot Nothing) Then ImageTemp.Dispose()

            If Me.Controller.CurrentDocumentFile.Count = 0 Then
                AjustarAncho()
            Else
                AplicarZoom()
            End If

            MensajeToolStripStatusLabel.Text = "Imagen cargada"


            'If (UpdateInfo) Then
            '    EsquemaComboBox.SelectedValue = Me.Controller.CurrentFolder.Esquema
            '    TipoDocumentalComboBox.SelectedValue = Me.Controller.CurrentDocumentFile.TipoDocumento
            'End If

            ActivarControles(True)
        End Sub

#End Region

#Region " Implementacion IIndexerView "

        Public Sub SetSearchControl(ByVal nSearchControl As IListValidationControl) Implements IIndexerView.SetSearchControl
            Dim SearchControl As Control = CType(nSearchControl, Control)
            SearchControl.Dock = DockStyle.Fill

            ValidacionListasPanel.Controls.Clear()
            ValidacionListasPanel.Controls.Add(SearchControl)
        End Sub

        Public ReadOnly Property AnexoCompleto As Boolean Implements IIndexerView.AnexoCompleto
            Get
                Return Me.AnexoTotalToolStripButton.Checked
            End Get
        End Property

        Public Property Information As String Implements IIndexerView.Information
            Get
                Return InformacionTextBox.Text
            End Get
            Set(ByVal value As String)
                InformacionTextBox.Text = value
            End Set
        End Property

        Public ReadOnly Property IndexerController As Controller.Indexer.IIndexerController Implements IIndexerView.IndexerController
            Get
                Return _IndexerController
            End Get
        End Property

        Public Property SelectedInputControl As IInputControl Implements IIndexerView.SelectedInputControl
            Get
                Return _SelectedInputControl
            End Get
            'NOTE: Evaluarla y optimizar el control de los botones OCR
            Set(ByVal value As IInputControl)

                _SelectedInputControl = value
                UpdateToolTip(value)

                If (IsOCRCaptureUsed()) Then

                    Dim previousValue As IInputControl = _SelectedInputControl           ' Almacena el valor actual en la variable de respaldo

                    HideGridControl()                                                    ' Cierra los controles de la tabla

                    If (value Is Nothing) Then
                        ConfigureDrawingLineOCRButtons(False)                            ' Deshabilita los botones de dibujo de la tabla y el rectangulo
                        ClearDrawingState()                                              ' Elimina el Rectangulo y las lineas de la tabla
                        ClearDrawingRectangleState()
                        SetTableAndRectangleDrawingState(False, False)                   ' Deshabilita boton para Dibujar tabla y Rectangulo
                    Else

                        Select Case value.Tipo
                            Case DesktopConfig.CampoTipo.TablaAsociada

                                If Not (previousValue IsNot Nothing AndAlso previousValue.Tipo = DesktopConfig.CampoTipo.TablaAsociada) Then
                                    ClearDrawingState()                                  ' Elimina el Rectangulo y las lineas de la tabla 
                                    ClearDrawingRectangleState()                         ' Elimina el Rectangulo de OCR
                                End If
                                SetTableAndRectangleDrawingState(True, True)            ' habilita Boton Dibujar Tabla y boton para Dibujar Rectangulo

                            Case DesktopConfig.CampoTipo.Texto,
                                 DesktopConfig.CampoTipo.Numerico,
                                 DesktopConfig.CampoTipo.Fecha,
                                 DesktopConfig.CampoTipo.Lista

                                ConfigureDrawingLineOCRButtons(False)                    ' Deshabilita los botones de dibujo de la tabla y el rectangulo
                                SetTableAndRectangleDrawingState(False, True)            ' Deshabilita Boton Dibujar Tabla y habilita boton para Dibujar Rectangulo
                                ClearDrawingRectangleState()                             ' Elimina el Rectangulo de OCR
                                ActiveDrawingRectangleButtonClick()

                                If previousValue IsNot Nothing AndAlso previousValue.Tipo = DesktopConfig.CampoTipo.TablaAsociada Then
                                    ClearDrawingState()                                      ' Elimina el Rectangulo y las lineas de la tabla
                                End If
                            Case Else
                                ConfigureDrawingLineOCRButtons(False)                    ' Deshabilita los botones de dibujo de la tabla y el rectangulo
                                ClearDrawingState()                                      ' Elimina el Rectangulo y las lineas de la tabla
                                ClearDrawingRectangleState()                             ' Elimina el Rectangulo de OCR
                                SetTableAndRectangleDrawingState(False, False)           ' Deshabilita boton para Dibujar tabla y Rectangulo
                        End Select

                    End If
                    SetVisibleToolStripSeparator7()                                      ' habilita o deshbailita el separador de los botones

                End If

                EnfocarMarca()
                ImagePictureBox.Refresh()
            End Set
        End Property

        Public Property SelectedValidationControl As ValidationControl Implements IIndexerView.SelectedValidationControl
            Get
                Return _SelectedValidationControl
            End Get
            Set(ByVal value As ValidationControl)
                _SelectedValidationControl = value

                EnfocarMarca()
                ImagePictureBox.Refresh()
            End Set
        End Property

        Public Sub ShowDesindexarFolioButton(ByVal nShow As Boolean) Implements IIndexerView.ShowDesindexarFolioButton
            If (DesIndexarFolioButton.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowDesindexarFolioButton
                DesIndexarFolioButton.Invoke(MyDelegate, New Object() {nShow})
            Else
                DesIndexarFolioButton.Visible = nShow
            End If
        End Sub

        Public Sub ShowAddFolioButton(ByVal nShow As Boolean) Implements IIndexerView.ShowAddFolioButton
            If (AddFolioButton.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowAddFolioButton
                AddFolioButton.Invoke(MyDelegate, New Object() {nShow})
            Else
                AddFolioButton.Visible = nShow
            End If
        End Sub

        Public Sub ShowDeleteFolioButton(ByVal nShow As Boolean) Implements IIndexerView.ShowDeleteFolioButton
            If (DeleteFolioButton.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowDeleteFolioButton
                DeleteFolioButton.Invoke(MyDelegate, New Object() {nShow})
            Else
                DeleteFolioButton.Visible = nShow
            End If
        End Sub

        Public Sub ShowNewFileButton(ByVal nShow As Boolean) Implements IIndexerView.ShowNewFileButton
            If (NewFileButton.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowNewFileButton
                NewFileButton.Invoke(MyDelegate, New Object() {nShow})
            Else
                NewFileButton.Visible = nShow
            End If
        End Sub

        Public Sub ShowNewFolderButton(ByVal nShow As Boolean) Implements IIndexerView.ShowNewFolderButton
            If (NewFolderButton.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowNewFolderButton
                NewFolderButton.Invoke(MyDelegate, New Object() {nShow})
            Else
                NewFolderButton.Visible = nShow
            End If
        End Sub

        Public Sub ShowSaveButton(ByVal nShow As Boolean) Implements IIndexerView.ShowSaveButton
            If (SaveButton.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowSaveButton
                SaveButton.Invoke(MyDelegate, New Object() {nShow})
            Else
                SaveButton.Visible = nShow
            End If
        End Sub

        Public Sub ShowDataPanel(ByVal nShow As Boolean) Implements IIndexerView.ShowDataPanel
            If (CamposGroupBox.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowDataPanel
                CamposGroupBox.Invoke(MyDelegate, New Object() {nShow})
            Else
                CamposGroupBox.Visible = nShow
            End If
        End Sub

        Public Sub ShowCamposLlavePanel(ByVal nShow As Boolean) Implements IIndexerView.ShowCamposLlavePanel
            If (CamposLlaveGroupBox.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowCamposLlavePanel
                CamposLlaveGroupBox.Invoke(MyDelegate, New Object() {nShow})
            Else
                CamposLlaveGroupBox.Visible = nShow
            End If
        End Sub

        Public Sub ShowToolTipData(ByVal nShow As Boolean) Implements IIndexerView.ShowToolTipData
            If (TotalSplitContainer.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowToolTipData
                TotalSplitContainer.Invoke(MyDelegate, New Object() {nShow})
            Else
                TotalSplitContainer.Panel2Collapsed = Not nShow
            End If
        End Sub

        Public Sub ShowInformationPanel(ByVal nShow As Boolean) Implements IIndexerView.ShowInformationPanel
            If (InformacionGroupBox.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowInformationPanel
                InformacionGroupBox.Invoke(MyDelegate, New Object() {nShow})
            Else
                InformacionGroupBox.Visible = nShow
            End If
        End Sub

        Public Sub ShowValidationsPanel(ByVal nShow As Boolean) Implements IIndexerView.ShowValidationsPanel
            If (ValidacionesGroupBox.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowValidationsPanel
                ValidacionesGroupBox.Invoke(MyDelegate, New Object() {nShow})
            Else
                ValidacionesGroupBox.Visible = nShow
            End If
        End Sub

        Public Sub ShowValidationsListasPanel(ByVal nShow As Boolean) Implements IIndexerView.ShowValidationsListasPanel
            If (ValidacionListasGroupBox.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowValidationsListasPanel
                ValidacionListasGroupBox.Invoke(MyDelegate, New Object() {nShow})
            Else
                ValidacionListasGroupBox.Visible = nShow
            End If
        End Sub

        Public Sub ShowNextButton(ByVal nShow As Boolean) Implements IIndexerView.ShowNextButton
            If (NextFolioButton.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowNextButton
                NextFolioButton.Invoke(MyDelegate, New Object() {nShow})
            Else
                NextFolioButton.Visible = nShow
            End If
        End Sub

        Public Sub ShowReprocesoButton(ByVal nShow As Boolean) Implements IIndexerView.ShowReprocesoButton
            If (NextFolioButton.InvokeRequired) Then
                Dim MyDelegate As ShowDelegate

                MyDelegate = AddressOf ShowNextButton
                ReprocesoButton.Invoke(MyDelegate, New Object() {nShow})
            Else
                If _Controller.IndexerSesion.IsExternal Then
                    ReprocesoButton.Visible = nShow
                Else
                    ReprocesoButton.Visible = nShow
                End If
            End If
        End Sub

        Public Sub ShowAutoIndexar(nShow As Boolean) Implements IIndexerView.ShowAutoIndexar
            Me.AnexosToolStripLabel.Visible = nShow
            Me.AutoIndexarToolStripButton.Visible = nShow
            Me.AutoindexarToolStripSeparator.Visible = nShow
            Me.AnexoFolderToolStripButton.Visible = nShow
            Me.AnexoTotalToolStripButton.Visible = nShow
            Me.AnexosToolStripSeparator.Visible = nShow
            Me.RulerToolStripButton.Visible = Not nShow

            Me.RulerToolStripSeparator.Visible = Not nShow
            Me.SubrayarToolStripButton.Visible = Not nShow
            Me.QuitarSubrayadoToolStripButton.Visible = Not nShow
        End Sub

        Private Sub ActivarControles(ByVal nActivo As Boolean) Implements IView.ActivarControles
            ' Imagen
            Dim ZoomMedidaX, ZoomMedidaY As Integer

            ZoomInToolStripButton.Enabled = (Me._Zoom < 1000) And nActivo
            ZoomOutToolStripButton.Enabled = (Me._Zoom > 10) And nActivo

            Try
                If Not ImagePictureBox.Image Is Nothing And nActivo Then
                    If (ImagePictureBox.Image.Height >= ImagePictureBox.Image.Width) Then
                        ZoomMedidaY = CInt(((MarcoDibujoPanel.Height - 30) / ImagePictureBox.Image.Height) * 100)
                        ZoomMedidaX = CInt(((MarcoDibujoPanel.Width - 50) / ImagePictureBox.Image.Width) * 100)
                    Else
                        ZoomMedidaY = CInt(((MarcoDibujoPanel.Height - 50) / ImagePictureBox.Image.Height) * 100)
                        ZoomMedidaX = CInt(((MarcoDibujoPanel.Width - 30) / ImagePictureBox.Image.Width) * 100)
                    End If

                    AjustarAltoToolStripButton.Enabled = (ZoomMedidaY <> Me._Zoom)
                    AjustarAnchoToolStripButton.Enabled = (ZoomMedidaX <> Me._Zoom)
                Else
                    AjustarAltoToolStripButton.Enabled = False
                    AjustarAnchoToolStripButton.Enabled = False
                End If
            Catch ex As Exception
                Throw
            End Try

            RotateLeftToolStripButton.Enabled = nActivo
            RotateRightToolStripButton.Enabled = nActivo
            FlipHorizontalToolStripButton.Enabled = nActivo
            FlipVerticalToolStripButton.Enabled = nActivo
            ZoomToolStripComboBox.Enabled = nActivo

            ' Navegacion
            EsquemaComboBox.Enabled = (Esquema_Enabled And Me.Controller.CurrentFolder IsNot Nothing) AndAlso (Me.Controller.CurrentFolder.Count = 1)

            If Me.Controller.CurrentDocumentFile IsNot Nothing Then
                Dim UltimaPagina As Boolean = (Me.Controller.CurrentImageIndex = Me.Controller.Folios - 1)

                NewFileButton.Enabled = UltimaPagina AndAlso (Me.Controller.CurrentDocumentFile.Folios > 1) AndAlso Me.IndexerController.AllowNewFile And nActivo
                NewFolderButton.Enabled = UltimaPagina AndAlso (Me.Controller.CurrentDocumentFile.Folios > 1) AndAlso Me.IndexerController.AllowNewFolder And nActivo

                DesIndexarFolioButton.Enabled = UltimaPagina And (Me.Controller.CurrentImageIndex > 0) And nActivo
                PreviousFolioButton.Enabled = (Me.Controller.CurrentImageIndex > 0) And nActivo
                NextFolioButton.Enabled = (Me.Controller.CurrentImageIndex < Me.Controller.ImageCount) And (TipoDocumentalComboBox.SelectedIndex >= 0) And nActivo

                TipoDocumentalComboBox.Enabled = TipoDocumental_Enabled 'And (Me.IndexerController.CurrentDocumentFile.Folios = 1)
            Else
                NewFileButton.Enabled = False
                NewFolderButton.Enabled = False

                DesIndexarFolioButton.Enabled = False
                PreviousFolioButton.Enabled = False
                NextFolioButton.Enabled = False

                TipoDocumentalComboBox.Enabled = False
            End If

            ' Información
            UpdateAvance()

            ' Autoindexar
            Me.AutoIndexarToolStripButton.Enabled = nActivo AndAlso (Me.Controller.CurrentImageIndex > 0) AndAlso (Me.Controller.CurrentDocumentFile.Folios > 1)
            Me.AnexoFolderToolStripButton.Enabled = nActivo
            Me.AnexoTotalToolStripButton.Enabled = nActivo

            If (Me.Controller.Folders.Count > 0) Then
                Select Case Me.Controller.CurrentFolder.Modo
                    Case Generic.Folder.FolderModoEnum.Anexo
                        Me.AnexoFolderToolStripButton.Checked = True

                    Case Generic.Folder.FolderModoEnum.Normal
                        Me.AnexoFolderToolStripButton.Checked = False

                End Select
            End If

            ' Subrayado
            Me.SubrayarToolStripButton.Enabled = Me.RulerToolStripButton.Checked
            Me.QuitarSubrayadoToolStripButton.Enabled = Me.Controller.CurrentFolio IsNot Nothing AndAlso Me.Controller.CurrentFolio.Lineas.Count > 0

            'Ajusta proporciones de campos y validaciones.
            Dim HeightSplit As Integer = CapturaSplitContainer.Height

            Try
                If (Not IsNothing(Me._Campos) And Not IsNothing(Me.Validaciones)) Then
                    Dim countCampos As Integer = Me._Campos.Count
                    Dim countCamposLlaves As Integer = Me._CamposLlave.Count
                    Dim countValidaciones As Integer = Me.Validaciones.Count
                    Dim avgElemento As Single

                    If countCampos > 0 And countCamposLlaves > 0 And countValidaciones > 0 And Me.ValidacionEmbargos = False Then
                        avgElemento = CSng((HeightSplit / (countCampos + countCamposLlaves + countValidaciones)))

                        CapturaSplitContainer.SplitterDistance = CInt((countCampos + countCamposLlaves) * avgElemento)

                    ElseIf (countCampos > 0 Or countCamposLlaves > 0) And countValidaciones > 0 And Me.ValidacionEmbargos = False Then
                        If countCampos = 0 Then
                            CamposSplitContainer.Panel1Collapsed = True
                        End If

                        If countCamposLlaves = 0 Then
                            CamposSplitContainer.Panel2Collapsed = True
                        End If

                        avgElemento = CSng((HeightSplit / (countCampos + countCamposLlaves + countValidaciones)))

                        CapturaSplitContainer.SplitterDistance = CInt((countCampos + countCamposLlaves) * avgElemento)
                    ElseIf (countCampos = 0 And countCamposLlaves = 0) And countValidaciones > 0 And Me.ValidacionEmbargos = False Then
                        CapturaSplitContainer.Panel1Collapsed = True

                    ElseIf (countCampos > 0 Or countCamposLlaves > 0) And countValidaciones = 0 And Me.ValidacionEmbargos = False Then
                        If countCampos = 0 Then
                            CamposSplitContainer.Panel1Collapsed = True
                        End If

                        If countCamposLlaves = 0 Then
                            CamposSplitContainer.Panel2Collapsed = True
                        End If

                        CapturaSplitContainer.Panel2Collapsed = True
                    ElseIf (countCampos > 0 Or countCamposLlaves > 0) And Me.ValidacionEmbargos = True Then
                        If countCampos = 0 Then
                            CamposSplitContainer.Panel1Collapsed = True
                        End If

                        If countCamposLlaves = 0 Then
                            CamposSplitContainer.Panel2Collapsed = True
                        End If

                        avgElemento = CSng((HeightSplit / (countCampos + countCamposLlaves + 1)))

                        CapturaSplitContainer.SplitterDistance = CInt((countCampos + countCamposLlaves) * avgElemento)
                    Else
                        CapturaSplitContainer.Panel1Collapsed = True
                    End If
                End If
            Catch
                Throw
            End Try
        End Sub

        Public Property Esquema_DataSource As DataView Implements IIndexerView.Esquema_DataSource
            Get
                Return CType(EsquemaComboBox.DataSource, DataView)
            End Get
            Set(ByVal value As DataView)
                EsquemaComboBox.ValueMember = "id_Esquema"
                EsquemaComboBox.DisplayMember = "Nombre_Esquema"

                EsquemaComboBox.DataSource = value
                Esquema_DataSource.Sort = "Nombre_Esquema"
            End Set
        End Property

        Public Property Esquema_Enabled As Boolean Implements IIndexerView.Esquema_Enabled

        Public Property Esquema_Index As Integer Implements IIndexerView.Esquema_Index
            Get
                Return EsquemaComboBox.SelectedIndex
            End Get
            Set(ByVal value As Integer)
                EsquemaComboBox.SelectedIndex = value
            End Set
        End Property

        Public Sub Esquema_Refresh() Implements IIndexerView.Esquema_Refresh
            EsquemaComboBox.Refresh()
        End Sub

        Public ReadOnly Property Esquema_Text As String Implements IIndexerView.Esquema_Text
            Get
                Return EsquemaComboBox.Text
            End Get
        End Property

        Public Property Esquema_Value As Short Implements IIndexerView.Esquema_Value
            Set(ByVal value As Short)
                EsquemaComboBox.SelectedValue = value
            End Set
            Get
                Return CShort(EsquemaComboBox.SelectedValue)
            End Get
        End Property

        Public Property TipoDocumental_DataSource As DataView Implements IIndexerView.TipoDocumental_DataSource
            Get
                Return CType(TipoDocumentalComboBox.DataSource, DataView)
            End Get
            Set(ByVal value As DataView)
                TipoDocumentalComboBox.ValueMember = "id_Documento"
                TipoDocumentalComboBox.DisplayMember = "Nombre_Documento"

                TipoDocumentalComboBox.DataSource = value
                TipoDocumental_DataSource.Sort = "Nombre_Documento"

                FiltrarTiposDocumentales(True)
            End Set
        End Property

        Public Property TipoDocumental_Enabled As Boolean Implements IIndexerView.TipoDocumental_Enabled

        Public Property TipoDocumental_Index As Integer Implements IIndexerView.TipoDocumental_Index
            Get
                Return TipoDocumentalComboBox.SelectedIndex
            End Get
            Set(ByVal value As Integer)
                TipoDocumentalComboBox.SelectedIndex = value
            End Set
        End Property

        Public Sub TipoDocumental_Refresh() Implements IIndexerView.TipoDocumental_Refresh
            TipoDocumentalComboBox.Refresh()
        End Sub

        Public ReadOnly Property TipoDocumental_Text As String Implements IIndexerView.TipoDocumental_Text
            Get
                Return TipoDocumentalComboBox.Text
            End Get
        End Property

        Public Property TipoDocumental_Value As Nullable(Of Integer) Implements IIndexerView.TipoDocumental_Value
            Get
                If (TipoDocumentalComboBox.SelectedIndex >= 0) Then
                    Return CInt(TipoDocumentalComboBox.SelectedValue)
                Else
                    Return Nothing
                End If
            End Get
            Set(ByVal value As Nullable(Of Integer))
                If (value Is Nothing) Then
                    TipoDocumentalComboBox.SelectedIndex = -1
                Else
                    TipoDocumentalComboBox.SelectedValue = value
                End If
            End Set
        End Property

        Public ReadOnly Property GridControl As Table.IndexerTable Implements IIndexerView.GridControl
            Get
                Return CapturaIndexerTable
            End Get
        End Property

        Public Sub ShowGridControl(TableControl As TableInputControl) Implements IIndexerView.ShowGridControl
            CapturaIndexerTable.Size = New Size(CapturaIndexerTable.Size.Width, 100)
            SaveButton.Enabled = False
            Me.SelectedTableInputControl = TableControl
            IndexerTablePanel.Visible = True
            CapturaIndexerTable.ColumHeaderHeight = 25
            CapturaIndexerTable.ShowControls()
        End Sub

        Public Sub HideGridControl() Implements IIndexerView.HideGridControl

            If (Me.Controller.IsOCRUsed) Then
                SetCheckBoxProperties(Me.DrawingTableButton, False, False, False)   ' Configurar boton para Dibujar tabla
                SetVisibleToolStripSeparator7()
            End If
            SaveButton.Enabled = True
            Me.SelectedTableInputControl = Nothing
            IndexerTablePanel.Visible = False
        End Sub


        Public ReadOnly Property Campos As List(Of CampoCaptura) Implements IIndexerView.Campos
            Get
                Return Me._Campos
            End Get
        End Property

        Public ReadOnly Property CamposLlave As List(Of CampoLlaveCaptura) Implements IIndexerView.CamposLlave
            Get
                Return Me._CamposLlave
            End Get
        End Property

        Public ReadOnly Property ListValidaciones As List(Of ValidacionCaptura) Implements IIndexerView.Validaciones
            Get
                Return Me.Validaciones
            End Get
        End Property

        Public ReadOnly Property ValidacionesListas As Boolean Implements IIndexerView.ValidacionListas
            Get
                Return Me.ValidacionEmbargos
            End Get
        End Property

        Public ReadOnly Property RequiereAutorizacion As Boolean Implements IIndexerView.RequiereAutorizacion
            Get
                For Each Campo In _Campos
                    If (Campo.Control.RequiereAutorizacion) Then
                        Return True
                    End If
                Next

                Return False
            End Get
        End Property

        Public ReadOnly Property CancelProcess As Boolean Implements IIndexerView.CancelProcess
            Get
                Return Me._CancelProcess
            End Get
        End Property

#End Region

#Region " Eventos "

        Protected Sub ThumbnailHelper_OnChangeImagePosition(ByVal e As ChangeImagenPositionEventArgs)
            Dim SourceFolio = CType(e.SourcePicture.Tag, Generic.Folio)
            Dim TargetFolio = CType(e.TargetPicture.Tag, Generic.Folio)

            Me.IndexerController.Move(SourceFolio, TargetFolio, e.TargetParentIndex)

            If (Controller.SetCurrentFolio(SourceFolio, True)) Then
                ShowImagen(True)

                AjustarAncho()

                ActivarControles(True)
            End If
        End Sub

        Private Sub FormIndexar_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
            _ViewClosing = True
        End Sub

        Private Sub FormIndexar_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
            If Unlock() Then Controller.Unlock()

            ImageFlotantePictureBox.Dispose()
            ImagePictureBox.Dispose()
            'Función Agregada Para Obligar Liberación De Memoria
            Utilities.ClearMemory()
            ''

        End Sub

        Private Sub FormIndexerView_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
            ' If (Not Me.KeyDownActive) Then
            Try
                If (e.Control) Then
                    If (e.Shift) Then
                        ' Marcar que se esta realizando una acción
                        '   Me.KeyDownActive = True

                        Select Case e.KeyCode
                            Case Keys.I
                                AutoIndexar()

                            Case Keys.F
                                AnexoFolder()

                            Case Keys.A
                                AnexoTotal()

                            Case Keys.R
                                ShowRuler()

                            Case Keys.Up
                                If (Me.SeñaladorRulerControl.Visible) Then Me.SeñaladorRulerControl.Desplazar_Up()

                            Case Keys.Down
                                If (Me.SeñaladorRulerControl.Visible) Then Me.SeñaladorRulerControl.Desplazar_Down()

                            Case Keys.Home
                                If (Me.SeñaladorRulerControl.Visible) Then Me.SeñaladorRulerControl.Desplazar_Top()

                                MarcoDibujoPanel.AutoScrollPosition = New Point(MarcoDibujoPanel.AutoScrollPosition.X * -1, 0)

                            Case Keys.End
                                If (Me.SeñaladorRulerControl.Visible) Then Me.SeñaladorRulerControl.Desplazar_Botton()

                                Dim Desplazamiento As Integer = (MarcoDibujoPanel.Height - ImagePictureBox.Height)
                                MarcoDibujoPanel.AutoScrollPosition = New Point(MarcoDibujoPanel.AutoScrollPosition.X * -1, (MarcoDibujoPanel.AutoScrollPosition.Y + Desplazamiento) * -2)

                            Case Keys.M
                                Subrayar()

                            Case Keys.B
                                QuitarSubrayado()

                        End Select
                    ElseIf (e.Alt) Then
                        ' Marcar que se esta realizando una acción
                        '     Me.KeyDownActive = True

                        Select Case e.KeyCode
                            Case Keys.NumPad1, Keys.D1
                                SeleccionarAnexo(1, True)

                            Case Keys.NumPad2, Keys.D2
                                SeleccionarAnexo(2, True)

                            Case Keys.NumPad3, Keys.D3
                                SeleccionarAnexo(3, True)

                            Case Keys.NumPad4, Keys.D4
                                SeleccionarAnexo(4, True)

                            Case Keys.NumPad5, Keys.D5
                                SeleccionarAnexo(5, True)

                            Case Keys.NumPad6, Keys.D6
                                SeleccionarAnexo(6, True)

                            Case Keys.NumPad7, Keys.D7
                                SeleccionarAnexo(7, True)

                            Case Keys.NumPad8, Keys.D8
                                SeleccionarAnexo(8, True)

                            Case Keys.NumPad9, Keys.D9
                                SeleccionarAnexo(9, True)

                        End Select
                    Else
                        ' Marcar que se esta realizando una acción
                        ' Me.KeyDownActive = True

                        Select Case e.KeyCode
                            Case Keys.NumPad0, Keys.D0
                                Me.DocumentsTabControl.SelectedIndex = 0

                            Case Keys.NumPad1, Keys.D1
                                SeleccionarAnexo(1, False)

                            Case Keys.NumPad2, Keys.D2
                                SeleccionarAnexo(2, False)

                            Case Keys.NumPad3, Keys.D3
                                SeleccionarAnexo(3, False)

                            Case Keys.NumPad4, Keys.D4
                                SeleccionarAnexo(4, False)

                            Case Keys.NumPad5, Keys.D5
                                SeleccionarAnexo(5, False)

                            Case Keys.NumPad6, Keys.D6
                                SeleccionarAnexo(6, False)

                            Case Keys.NumPad7, Keys.D7
                                SeleccionarAnexo(7, False)

                            Case Keys.NumPad8, Keys.D8
                                SeleccionarAnexo(8, False)

                            Case Keys.NumPad9, Keys.D9
                                SeleccionarAnexo(9, False)

                            Case Keys.F1
                                ShowAccesos()

                            Case Keys.F3
                                VisualizarAnexos()

                            Case Keys.F8
                                If (Me.DocumentsTabControl.TabPages.Count > 1) Then Me.DocumentsTabControl.SelectedIndex = 1

                            Case Keys.E
                                If (ReprocesoButton.Enabled And ReprocesoButton.Visible) Then Reproceso()

                            Case Keys.U
                                If (DesIndexarFolioButton.Enabled And DesIndexarFolioButton.Visible) Then DesIndexarFolio()

                            Case Keys.D
                                If (NewFileButton.Enabled And NewFileButton.Visible) Then NewDocumentFile()

                            Case Keys.F
                                If (NewFolderButton.Enabled And NewFolderButton.Visible) Then NewFolder()

                            Case Keys.I
                                If (AddFolioButton.Enabled And AddFolioButton.Visible) Then AddFolio()

                            Case Keys.G
                                If (SaveButton.Enabled And SaveButton.Visible) Then Save()

                            Case Keys.Delete
                                If (DeleteFolioButton.Enabled And DeleteFolioButton.Visible) Then DeleteFolio()

                            Case Keys.S
                                ShowThumbnail()

                            Case Keys.C
                                ' Ejecuta el evento de hacer clic en el botón de dibujo de rectángulo y, simultáneamente, aplica OCR al rectángulo creado.
                                If (IsOCRCaptureUsed() AndAlso
                                    Me.DrawingRactangleButton.Enabled) Then
                                    HandleDrawingRectangleButtonClick()
                                    e.Handled = True
                                    e.SuppressKeyPress = True
                                End If

                            Case Keys.T
                                ' Ejecuta el evento de hacer clic en el botón de dibujo de tablas para OCR
                                If (IsOCRCaptureUsed() AndAlso
                                    Me.DrawingTableButton.Visible AndAlso
                                    Me.DrawingTableButton.Enabled) Then
                                    HandleDrawingTableButton()
                                End If

                            Case Keys.H
                                ' Maneja el evento de hacer clic en el botón de línea Horizontales para dibujar en la tabla 
                                If (IsOCRCaptureUsed() AndAlso
                                    Me.DrawingTableButton.Checked AndAlso
                                    Me.DrawHorizontalLinesButton.Enabled) Then
                                    HandleDrawHorizontalLinesButtonClick()
                                End If

                            Case Keys.V
                                ' Maneja el evento de hacer clic en el botón de línea Verticales para dibujar en la tabla
                                If (IsOCRCaptureUsed() AndAlso
                                   Me.DrawingTableButton.Checked AndAlso
                                   Me.DrawVerticalLinesButton.Enabled) Then
                                    HandleDrawVerticalLinesButtonClick()
                                End If

                            Case Keys.B
                                ' Maneja el evento de hacer clic en el botón de eliminar línea Horizontales o Vertical para dibujar en la tabla 
                                If (IsOCRCaptureUsed() AndAlso
                                   Me.DrawingTableButton.Checked AndAlso
                                   Me.DeleteLineButton.Enabled) Then
                                    HandleDeleteLineButtonClick()
                                End If

                            Case Keys.O
                                ' Maneja el evento de hacer clic en el botón de realizar el OCR a la tabla Dibujada
                                If (IsOCRCaptureUsed() AndAlso
                                   Me.DrawingTableButton.Checked AndAlso
                                   Me.StartOCRButton.Enabled) Then
                                    HandleStartOCRButtonClick()
                                End If


                        End Select
                    End If
                ElseIf (e.Shift) Then
                    ' Marcar que se esta realizando una acción
                    'Me.KeyDownActive = True

                    Select Case e.KeyCode
                        Case Keys.PageDown
                            If (NextFolioButton.Enabled And NextFolioButton.Visible) Then NextFolio()

                        Case Keys.PageUp
                            If (PreviousFolioButton.Enabled And PreviousFolioButton.Visible) Then PreviousFolio()

                        Case Keys.Space
                            If (NewFolderButton.Enabled And NewFolderButton.Visible) Then NewFolder()

                    End Select

                ElseIf (e.Alt) Then
                    ' Marcar que se esta realizando una acción
                    'Me.KeyDownActive = True

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

                        Case Keys.OemPeriod
                            FlipHorizontal()

                        Case Keys.Oemcomma
                            FlipVertical()

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

                ElseIf (e.KeyCode = Keys.Escape) Then
                    ' Cancelar el proceso activo
                    Me._CancelProcess = True
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Finally

                '    Me.KeyDownActive = False
            End Try

            'ElseIf (e.KeyCode = Keys.Escape) Then
            '    Me._CancelProcess = True
            'End If
        End Sub

        Private Sub ImagePictureBox_Paint(ByVal sender As System.Object, ByVal e As PaintEventArgs) Handles ImagePictureBox.Paint
            If Not _ViewClosing Then
                DrawMarca(e.Graphics)
                DrawLines(e.Graphics)
            End If

            If (IsOCRCaptureUsed()) Then
                If Me.DrawingTableButton.Checked Then
                    UpdateDeleteLineButton(selectedOrientationLine)                                                         ' Habilita el Boton para eliminar lineas
                    Me.StartOCRButton.Enabled = (Me.verticalLinesList.Count > 0) AndAlso (Me.horizontalLinesList.Count >= 0) AndAlso Not Me.DrawingRactangleButton.Checked ' Habilita el Boton para Iniciar OCR
                End If

                DrawRectangleOnImage(e)
                DrawRectangleLinesOnImage(e)
            End If
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

        Private Sub RotateLeftToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles RotateLeftToolStripButton.Click
            RotateLeft()
        End Sub

        Private Sub RotateRightToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles RotateRightToolStripButton.Click
            RotateRight()
        End Sub

        Private Sub FlipHorizontalToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles FlipHorizontalToolStripButton.Click
            FlipHorizontal()
        End Sub

        Private Sub FlipVerticalToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles FlipVerticalToolStripButton.Click
            FlipVertical()
        End Sub

        Private Sub ShowThumbnailToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ShowThumbnailToolStripButton.Click
            ShowThumbnail()
        End Sub

        Private Sub ShowAccesosToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ShowAccesosToolStripButton.Click
            ShowAccesos()
        End Sub

        Private Sub DesIndexarFolioButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DesIndexarFolioButton.Click
            DesIndexarFolio()
        End Sub

        Private Sub PreviousFolioButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PreviousFolioButton.Click
            PreviousFolio()
        End Sub

        Private Sub NextFolioButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles NextFolioButton.Click
            NextFolio()
        End Sub

        Private Sub NewFileButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles NewFileButton.Click
            NewDocumentFile()
        End Sub

        Private Sub NewFolderButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles NewFolderButton.Click
            NewFolder()
        End Sub

        Private Sub ReprocesoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReprocesoButton.Click
            Reproceso()
        End Sub

        Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SaveButton.Click
            Save()
        End Sub

        Private Sub AddFolioButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AddFolioButton.Click
            AddFolio()
        End Sub

        Private Sub DeleteFolioButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DeleteFolioButton.Click
            DeleteFolio()
        End Sub

        Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExitButton.Click
            Cancel()
        End Sub

        Private Sub EsquemaComboBox_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles EsquemaComboBox.Enter
            TipoDocumentoComboBoxControl.Visible = False
        End Sub

        Private Sub EsquemaComboBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles EsquemaComboBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                TipoDocumentalComboBox.Focus()
            End If
        End Sub

        Private Sub EsquemaComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaComboBox.SelectedIndexChanged
            FiltrarTiposDocumentales(False)
        End Sub

        Private Sub TipoDocumentalComboBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TipoDocumentalComboBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                If (Me._Campos.Count > 0) Then
                    CType(Me._Campos(0).Control, Control).Focus()
                    'CType(Me._CamposLlave(0).Control, Control).Focus()
                ElseIf (Me._CamposLlave.Count > 0) Then
                    CType(Me._CamposLlave(0).Control, Control).Focus()
                ElseIf (Me.Validaciones.Count > 0) Then
                    Me.Validaciones(0).Control.Focus()
                Else
                    NextFolioButton.Focus()
                End If
            End If
        End Sub

        Private Sub TipoDocumentalComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipoDocumentalComboBox.SelectedIndexChanged
            ShowCampos()
        End Sub

        Private Sub TipoDocumentalComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipoDocumentalComboBox.Enter
            Dim Lista As New List(Of Slyg.Tools.GenericItem(Of String))

            For Each Fila As DataRowView In TipoDocumental_DataSource
                Lista.Add(New Slyg.Tools.GenericItem(Of String)(Fila("id_Documento").ToString(), Fila("Nombre_Documento").ToString()))
            Next

            EsquemaComboBox.Enabled = False
            TipoDocumentoComboBoxControl.Mostrar(TipoDocumentalComboBox, Lista)
        End Sub

        Private Sub TipoDocumentoComboBoxControl_SeleccionarSiguienteControl() Handles TipoDocumentoComboBoxControl.SeleccionarSiguienteControl
            ActivarControles(True)

            If (Me._Campos.Count > 0) Then
                CType(Me._Campos(0).Control, Control).Focus()
                'CType(Me._CamposLlave(0).Control, Control).Focus()
            ElseIf (Me._CamposLlave.Count > 0) Then
                CType(Me._CamposLlave(0).Control, Control).Focus()
            ElseIf (Me.Validaciones.Count > 0) Then
                Me.Validaciones(0).Control.Focus()
            Else
                NextFolioButton.Focus()
            End If
        End Sub

        Private Sub BreakButton_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles BreakButton.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                If (EsquemaComboBox.Enabled) Then
                    EsquemaComboBox.Focus()
                ElseIf (TipoDocumentalComboBox.Enabled) Then
                    TipoDocumentalComboBox.Focus()
                End If
            End If
        End Sub

        Private Sub BreakButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BreakButton.Click
            If (EsquemaComboBox.Enabled) Then
                EsquemaComboBox.Focus()
            ElseIf (TipoDocumentalComboBox.Enabled) Then
                TipoDocumentalComboBox.Focus()
            ElseIf (Me._Campos.Count > 0) Then
                CType(Me._Campos(0).Control, Control).Focus()
                'CType(Me._CamposLlave(0).Control, Control).Focus()
            ElseIf (Me._CamposLlave.Count > 0) Then
                CType(Me._CamposLlave(0).Control, Control).Focus()
            ElseIf (Me.Validaciones.Count > 0) Then
                Me.Validaciones(0).Control.Focus()
            Else
                NextFolioButton.Focus()
            End If
        End Sub

        ''' <summary>
        ''' ' Maneja el evento de clic en el botón de cierre de la tabla.
        ''' </summary>
        Private Sub Table_CloseButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Table_CloseButton.Click
            CloseButtonClick()
        End Sub

        ''' <summary>
        ''' Maneja el evento de clic en el botón de OCR de la tabla, realiza la limpieza, procesamiento y visualización de datos OCR
        ''' </summary>
        ''' <param name="sender">El objeto que desencadenó el evento.</param>
        ''' <param name="e">Argumentos del evento.</param>
        Private Sub Table_ColumnOCRButton_Click(sender As System.Object, e As System.EventArgs) Handles Table_ColumnOCRButton.Click

            Dim selectedTableControl As MyTableInputControl = Me.SelectedTableInputControl
            Dim controller As Controller.IController = Me._Controller

            If selectedTableControl IsNot Nothing AndAlso controller IsNot Nothing Then

                Dim saveColumnsResult As Boolean? = selectedTableControl.SaveColumnsOCR()                           'Verifica que el numero de columnas seleccionado por el usuario no se encuentren repetidas

                If saveColumnsResult.HasValue AndAlso saveColumnsResult.Value Then

                    selectedTableControl.InitiliazeOCRDictionaryOCRORder()                                          ' Inicializa el dictionario ordendado para proceder a ordenarlo
                    selectedTableControl.DeleteRowsItems()                                                          ' Borrar los elementos de las filas
                    ImagePictureBox.Invalidate()                                                                    ' Invalidar y refrescar la imagen para aplicar cambios
                    GenerateOcrDictFromColumns()                                                                                ' Procesar los datos OCR


                    ''TODO : Ajustar para evitar el error en el procedimiento almacenado
                    '' Convertir el diccionario original al nuevo formato
                    'Dim newDictionary As Dictionary(Of Integer, List(Of String())) =
                    '    selectedTableControl.DataCellOrderOCR.ToDictionary(
                    '        Function(kvp) kvp.Key,
                    '        Function(kvp) kvp.Value.cellDataConfidence.Select(Function(lst) lst.ToArray()).ToList()
                    '    )
                    'Me.dataResultDictionaryOcr = newDictionary

                    Me.dataResultDictionaryOcr = _OCRCaptura.SendCleanColumnDataOCR(selectedTableControl)
                    selectedTableControl.dataDictionaryOcr = Me.dataResultDictionaryOcr

                    Dim listaDeseada As List(Of String()) = Me.dataResultDictionaryOcr(0)                             ' Obtener la lista de datos deseada para la clave 0
                    Dim numberOfRow As Integer = listaDeseada.Count

                    selectedTableControl.InitializeOCRTableColumn()                                                   ' Inicializar la vista de datos para la columna OCR en la tabla
                    selectedTableControl.ShowData()                                                                   ' muestra los datos anteriormente almacenados

                    For rowNumber As Integer = 0 To numberOfRow - 1
                        selectedTableControl.IndexerTableAddRow()                                                    ' Agregar una nueva fila en la tabla si no es la primera columna
                        selectedTableControl.VisualizateDataRowOCR(rowNumber)
                    Next

                    SetCheckBoxProperties(Me.DrawingRactangleButton, True, True, False)     ' Habilita el Boton de dibujar rectangulo OCR
                End If

            End If
        End Sub


        ' NOTE: se debe subdividir en metodos para mejorar legibilidad
        ''' <summary>
        ''' ' Maneja el evento de clic en el botón de guardar datos en la tabla.
        ''' </summary>
        Private Sub Table_SaveButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Table_SaveButton.Click

            Dim selectedTableControl As MyTableInputControl = Me.SelectedTableInputControl

            If selectedTableControl IsNot Nothing Then
                SaveData(selectedTableControl)

                'If (IsOCRCaptureUsed()) Then
                '    ConfigureDrawingLineOCRButtons(False)               ' Deshabilita los botones de dibujo d etabla y rectangulo
                '    ClearDrawingState()                                 ' Elimina el rectangulo y lineas de la tabla
                '    ClearDrawingRectangleState()                        ' Elimina el Rectangulo de OCR
                '    selectedTableControl.SetDataOcr()           ' Elima los datos OCR despues de guardar
                'End If
            End If
        End Sub


        Private Sub SaveData(ByVal selectedTableControl As MyTableInputControl)
            If (selectedTableControl.Save()) Then
                selectedTableControl.NextControl.Focus()
                HideGridControl()

                If (IsOCRCaptureUsed()) Then
                    ConfigureDrawingLineOCRButtons(False)               ' Deshabilita los botones de dibujo d etabla y rectangulo
                    ClearDrawingState()                                 ' Elimina el rectangulo y lineas de la tabla
                    ClearDrawingRectangleState()                        ' Elimina el Rectangulo de OCR
                    selectedTableControl.SetDataOcr()           ' Elima los datos OCR despues de guardar
                End If
            End If
        End Sub

        Private Sub CapturaIndexerTable_NextControlEvent() Handles CapturaIndexerTable.NextControlEvent
            If (Me.SelectedTableInputControl IsNot Nothing) Then
                If (Me.SelectedTableInputControl.Save()) Then
                    SelectedInputControl.NextControl.Focus()
                    HideGridControl()
                End If
            End If
        End Sub

        Private Sub AnexoFolderToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AnexoFolderToolStripButton.Click
            AnexoFolder()
        End Sub

        Private Sub AnexoTotalToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AnexoTotalToolStripButton.Click
            AnexoTotal()
        End Sub

        Private Sub AutoIndexarToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AutoIndexarToolStripButton.Click
            AutoIndexar()
        End Sub

        Private Sub RulerToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles RulerToolStripButton.Click
            ShowRuler()
        End Sub

        Private Sub SubrayarToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SubrayarToolStripButton.Click
            Subrayar()
        End Sub

        Private Sub QuitarSubrayadoToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles QuitarSubrayadoToolStripButton.Click
            QuitarSubrayado()
        End Sub

        Private Sub SeñaladorRulerControl_LocationChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles SeñaladorRulerControl.LocationChanged
            Me.ImagePictureBox.Refresh()
        End Sub


        Private Sub VisualizarSeleccionadosButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles VisualizarSeleccionadosButton.Click
            VisualizarAnexos()
        End Sub

        Private Sub DocumentConfigTabPage_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles DocumentConfigTabPage.Enter
            DocumentsCheckedListBox.Focus()
        End Sub

#Region "Eventos OCR"

        ''' <summary>
        ''' Dibuja una tabla en la imagen cuando se hace clic en el botón (Dibujar Tabla)
        ''' </summary>
        Private Sub DrawingTableButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DrawingTableButton.Click
            If (IsOCRCaptureUsed()) Then
                HandleDrawingTableButton()
            End If
        End Sub

        ''' <summary>
        ''' Inicia el dibujo de un cuadro o rectángulo al hacer clic en el PictureBox de la imagen.
        ''' </summary>
        Protected Overridable Sub ImagePictureBox_MouseDown(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles ImagePictureBox.MouseDown

            If (IsOCRCaptureUsed()) Then

                If Me.DrawingRactangleButton.Checked AndAlso Me.IsDrawingEnabled Then
                    If e.Button = MouseButtons.Left AndAlso ImagePictureBox.Bounds.Contains(e.Location) Then    ' Verifica clic izquierdo dentro de los límites del PictureBox.
                        Me.rectangleStartPoint = e.Location                                                     ' Captura la ubicacion del punto de inicio al presionar el mouse 
                    End If
                ElseIf Me.DrawingTableButton.Checked AndAlso Me.IsDrawingTableEnabled Then
                    If e.Button = MouseButtons.Left AndAlso ImagePictureBox.Bounds.Contains(e.Location) Then    ' Verifica clic izquierdo dentro de los límites del PictureBox.
                        Me.rectangleTableStartPoint = e.Location                                                     ' Captura la ubicacion del punto de inicio al presionar el mouse 
                    End If
                End If
            End If
        End Sub

        ' NOTE: se debe subdividir en Metodos para mejorar su legibilidad
        ''' <summary>
        ''' Actualiza el dibujo del rectángulo durante el movimiento del ratón en el PictureBox de la imagen.
        ''' </summary>
        Private Sub ImagePictureBox_MouseMove(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles ImagePictureBox.MouseMove

            If (IsOCRCaptureUsed()) Then

                If Me.DrawingRactangleButton.Checked AndAlso Me.IsDrawingEnabled Then

                    ' Verifica si se está manteniendo presionado el botón izquierdo del ratón y si la ubicación está dentro de los límites del PictureBox.
                    If e.Button = MouseButtons.Left AndAlso ImagePictureBox.Bounds.Contains(e.Location) Then

                        Me.rectangleEndPoint = e.Location                   'Captura la ubicacion del punto final al mover el mouse
                        Me.ZoomDrawingRectangle = CSng((Me._Zoom / 100))    ' Calcula el Zoom en el cual se creo el rectangulo
                        Me.ImagePictureBox.Invalidate()                     'forzar la llamada al evento Paint para actualizar la visualización.
                    End If
                ElseIf Me.DrawingTableButton.Checked AndAlso Me.IsDrawingTableEnabled Then
                    ' Verifica si se está manteniendo presionado el botón izquierdo del ratón y si la ubicación está dentro de los límites del PictureBox.
                    If e.Button = MouseButtons.Left AndAlso ImagePictureBox.Bounds.Contains(e.Location) Then

                        Me.rectangleTableEndPoint = e.Location               'Captura la ubicacion del punto final al mover el mouse
                        Me.ZoomDrawingRectangle = CSng((Me._Zoom / 100))     ' Calcula el Zoom en el cual se creo el rectangulo
                        Me.ImagePictureBox.Invalidate()                      'forzar la llamada al evento Paint para actualizar la visualización.
                    End If
                End If
            End If
        End Sub

        ' NOTE: se debe subdividir en Metodos para mejorar su legibilidad
        ''' <summary>
        ''' Finaliza el dibujo del rectángulo al soltar el botón del ratón en el PictureBox de la imagen.
        ''' </summary>
        Private Sub ImagePictureBox_MouseUp(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles ImagePictureBox.MouseUp

            If (IsOCRCaptureUsed()) Then

                If (Me.DrawingRactangleButton.Checked AndAlso Me.IsDrawingEnabled AndAlso e.Button = MouseButtons.Left) Then

                    Me.IsDrawingEnabled = False                                                                 ' Finaliza el dibujo al soltar el boton izquierdo
                    Dim scaleLevelsImage As Double() = CalculateScaleImageXY()                                  ' calcular la relación de escala en eje X y Y
                    Dim imageOriginalRectangle As Rectangle = CalculateRectangleImageOriginal(Me.rectangleStartPoint, Me.rectangleEndPoint, scaleLevelsImage) ' Calcula el rectángulo en las coordenadas originales de la imagen, ajustado por la escala
                    Me.drawRectangle = imageOriginalRectangle                                                   ' Actualiza el rectángulo de dibujo utilizando el rectángulo calculado

                    ' Actualiza el rectángulo de dibujo utilizando el rectángulo calculado
                    Dim rectangleInfoString As String = " : X:" & imageOriginalRectangle.X.ToString() & " Y:" & imageOriginalRectangle.Y.ToString() & " W:" & imageOriginalRectangle.Width.ToString() & " H:" & imageOriginalRectangle.Height.ToString()
                    Me.labelTableCoordinates.Text = rectangleInfoString

                    If Not (Me.drawRectangle.IsEmpty) Then

                        ExtractTitleMetadata(Me.Text)
                        Dim imagePicture As Image = GetImageFromPictureBox()
                        Dim responseOCRProcess = _OCRCaptura.ProcessOCRRectangle(imagePicture, Me.drawRectangle)
                        ProcessOCRRectangleResponse(responseOCRProcess)

                        ClearDrawingRectangleState()
                        Me.DrawingRactangleButton.Checked = False
                        Me.ImagePictureBox.Invalidate()                   ' Invalida la imagen para que refleje los cambios

                    End If

                ElseIf (Me.DrawingTableButton.Checked AndAlso Me.IsDrawingTableEnabled AndAlso e.Button = MouseButtons.Left) Then

                    Me.IsDrawingTableEnabled = False                                                                 ' Finaliza el dibujo al soltar el boton izquierdo
                    Dim scaleLevelsImage As Double() = CalculateScaleImageXY()                                       ' calcular la relación de escala en eje X y Y
                    Dim imageOriginalRectangle As Rectangle = CalculateRectangleImageOriginal(Me.rectangleTableStartPoint, Me.rectangleTableEndPoint, scaleLevelsImage) ' Calcula el rectángulo en las coordenadas originales de la imagen, ajustado por la escala

                    If Not (imageOriginalRectangle.IsEmpty) Then

                        Dim imagePicture As Image = GetImageFromPictureBox()
                        Dim detectedTable = _OCRCaptura.ExtractTableLinesFromImage(imagePicture, imageOriginalRectangle)

                        If Not (detectedTable.horizontalLines Is Nothing AndAlso detectedTable.verticalLines Is Nothing AndAlso detectedTable.mainRectangleTable.IsEmpty()) Then

                            Me.verticalLinesList = detectedTable.verticalLines
                            Me.horizontalLinesList = detectedTable.horizontalLines

                            Me.rectangleStartPoint = New Point(
                                detectedTable.mainRectangleTable.X,
                                detectedTable.mainRectangleTable.Y
                            )

                            Me.rectangleEndPoint = New Point(
                                detectedTable.mainRectangleTable.Width,
                                detectedTable.mainRectangleTable.Height
                            )

                            Me.drawRectangleTable = New Rectangle(
                                rectangleStartPoint.X,
                                rectangleStartPoint.Y,
                                rectangleEndPoint.X - rectangleStartPoint.X,
                                rectangleEndPoint.Y - rectangleStartPoint.Y
                            )

                        Else
                            Me.drawRectangleTable = imageOriginalRectangle                                                   ' Actualiza el rectángulo de dibujo utilizando el rectángulo calculado

                        End If

                        Dim rectangleInfoString As String = " : X:" & Me.drawRectangleTable.X.ToString() & " Y:" & Me.drawRectangleTable.Y.ToString() & " W:" & Me.drawRectangleTable.Width.ToString() & " H:" & Me.drawRectangleTable.Height.ToString()
                        Me.labelTableCoordinates.Text = rectangleInfoString

                        Me.DrawHorizontalLinesButton.Enabled = True                                             'Habilita los botones para realizar lineas Horizontal
                        Me.DrawVerticalLinesButton.Enabled = True                                               'Habilita los botones para realizar lineas verticales

                    End If

                    Me.ImagePictureBox.Invalidate()                                                             ' Invalida la imagen para que refleje los cambios
                End If
            End If

        End Sub

        ''' <summary>
        ''' Inicia el dibujo de líneas horizontales en la imagen al hacer clic en el botón (Dibujar linea Horizontal).
        ''' </summary>
        Private Sub DrawHorizontalLinesButton_Click(sender As System.Object, e As System.EventArgs) Handles DrawHorizontalLinesButton.Click
            If (IsOCRCaptureUsed()) Then
                HandleDrawHorizontalLinesButtonClick()
            End If
        End Sub

        ''' <summary>
        ''' Inicia el dibujo de líneas Verticales en la imagen al hacer clic en el botón (Dibujar linea Verticales).
        ''' </summary>
        Private Sub DrawVerticalLinesButton_Click(sender As System.Object, e As System.EventArgs) Handles DrawVerticalLinesButton.Click
            If (IsOCRCaptureUsed()) Then
                HandleDrawVerticalLinesButtonClick()
            End If
        End Sub

        ''' <summary>
        ''' Elimina una línea de la tabla anterior al hacer clic en el botón (Eliminar linea anterior)
        ''' </summary>
        Private Sub DeleteLineButton_Click(sender As System.Object, e As System.EventArgs) Handles DeleteLineButton.Click
            If (IsOCRCaptureUsed()) Then
                HandleDeleteLineButtonClick()
            End If
        End Sub

        ''' <summary>
        ''' Inicia el dibujo de líneas en la imagen al hacer clic en el botón (linea horizontal o Linea Vertical),
        ''' guarda la línea horizontal creada y actualiza la imagen si la captura OCR está en uso.
        ''' </summary>
        Protected Overridable Sub DrawLinesButton_MouseDown(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles ImagePictureBox.MouseDown

            If (IsOCRCaptureUsed()) Then
                If Me.selectedOrientationLine IsNot Nothing Then      ' Verificar si hay una línea de orientación seleccionada

                    saveLines(e, selectedOrientationLine)             ' Verifica y  Guarda la linea creada
                    Me.ImagePictureBox.Invalidate()                   ' Actualizar la imagen para reflejar los cambios
                End If
            End If
        End Sub

        ''' <summary>
        ''' Inicia el proceso OCR de la Tabla al hacer clic en el botón (Inicia OCR)
        ''' </summary>
        Private Sub StartOCRButton_Click(sender As System.Object, e As System.EventArgs) Handles StartOCRButton.Click
            If (IsOCRCaptureUsed()) Then
                HandleStartOCRButtonClick()
            End If
        End Sub

        ''' <summary>
        ''' Inicia el dibujo de un rectángulo OCRCampo en la imagen al hacer clic en el botón correspondiente.
        ''' </summary>
        Private Sub DrawingRactangleButton_Click(sender As System.Object, e As System.EventArgs) Handles DrawingRactangleButton.Click
            If (IsOCRCaptureUsed()) Then
                HandleDrawingRectangleButtonClick()
            End If
        End Sub

#End Region

#End Region


#Region " Metodos "

        ''' <summary>
        ''' Extrae los valores de 'Expediente', 'Folder' y 'File' de un string de título dado.
        ''' </summary>
        ''' <param name="titulo">El string de título que contiene los metadatos dentro de corchetes</param>
        ''' <exception cref="Exception">Lanza una excepción si la extracción o conversión falla.</exception>
        Private Sub ExtractTitleMetadata(titulo As String)
            Try
                If Not String.IsNullOrWhiteSpace(titulo) Then

                    ' Encontrar la parte relevante del string (dentro de los corchetes)
                    Dim inicio As Integer = titulo.IndexOf("[") + 1
                    Dim fin As Integer = titulo.IndexOf("]")
                    Dim parteRelevante As String = titulo.Substring(inicio, fin - inicio)

                    ' Dividir la parte relevante en sus componentes
                    Dim partes As String() = parteRelevante.Split(New String() {"Ex: ", " - Fo: ", " - Fi: "}, StringSplitOptions.None)

                    If partes.Length = 4 Then
                        ' Extraer los valores
                        Dim Expediente = CInt(partes(1).Trim())
                        Dim Folder = CInt(partes(2).Trim())
                        Dim File = CInt(partes(3).Trim())

                        _OCRCaptura.SetExpediente(Expediente)
                        _OCRCaptura.SetFolder(Folder)
                        _OCRCaptura.SetFile(File)

                    End If
                End If
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Actualiza el texto del ToolTipLabel según el control seleccionado (o si es nulo).
        ''' </summary>
        ''' <param name="value">El control seleccionado.</param>
        Private Sub UpdateToolTip(value As IInputControl)
            If Not _SelectedInputControl Is Nothing Then
                If _SelectedInputControl.Tipo <> DesktopConfig.CampoTipo.TablaAsociada Then
                    ToolTipLabel.Text = If(value Is Nothing, "", value.Etiqueta & ": " & _SelectedInputControl.Value.ToString())
                Else
                    ToolTipLabel.Text = If(value Is Nothing, "", value.Etiqueta)
                End If
            Else
                ToolTipLabel.Text = If(value Is Nothing, "", value.Etiqueta)
            End If

        End Sub

        ''' <summary>
        ''' Configura los botones de dibujo (tabla y rectángulo).
        ''' </summary>
        ''' <param name="tableButtonChecked">Indica si el botón de dibujo de tabla está marcado (visible, enable).</param>
        ''' <param name="rectangleButtonChecked">Indica si el botón de dibujo de rectángulo está marcado (visible, enable).</param>
        Private Sub SetTableAndRectangleDrawingState(tableButtonChecked As Boolean, rectangleButtonChecked As Boolean)

            SetCheckBoxProperties(Me.DrawingTableButton, tableButtonChecked, tableButtonChecked, False)             ' Configurar el botón de dibujo de tabla.
            SetCheckBoxProperties(Me.DrawingRactangleButton, rectangleButtonChecked, rectangleButtonChecked, False) ' Configurar el botón de dibujo de rectángulo.
        End Sub

        ''' <summary>
        ''' Maneja el evento de clic en el botón de Dibujar Rectángulo.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub HandleDrawingRectangleButtonClick()

            Me.DrawingRactangleButton.Checked = Not Me.DrawingRactangleButton.Checked

            If Me.DrawingRactangleButton.Checked Then
                Me.IsDrawingEnabled = True                 ' Habilita el dibujo del cuadro al hacer click
                Me.labelTableCoordinates.Visible = True    ' Muestra las coordenadas.
                ClearDrawingRectangleState()               ' Elimina el Rectangulo de OCR

                If Me.DrawingTableButton.Checked Then
                    ConfigureDrawingLineOCRButtons(True)
                    Me.isDrawingLine = False               ' Deshabilita el modo de dibujo de linea Horizontal
                    selectedOrientationLine = Nothing      ' Reset estado orientacion linea
                End If
            Else
                Me.IsDrawingEnabled = False                ' Deshabilita el dibujo del cuadro al hacer click
                Me.labelTableCoordinates.Visible = False   ' Muestra las coordenadas.

                If Me.DrawingTableButton.Checked Then
                    ConfigureDrawingLineButtons(True, True, False)                                          ' Configura los botones para dibujar lineas (H y V)
                End If

            End If

            Me.ImagePictureBox.Invalidate()               ' Invalida la imagen para que refleje los cambios
        End Sub

        ''' <summary>
        ''' Maneja el evento de clic en el botón para activar el dibujo de rectángulos.
        ''' </summary>
        Private Sub ActiveDrawingRectangleButtonClick()

            If Not Me.DrawingRactangleButton.Checked Then
                Me.DrawingRactangleButton.Checked = True
            End If

            Me.IsDrawingEnabled = True                 ' Habilita el dibujo del cuadro al hacer click
            Me.labelTableCoordinates.Visible = True    ' Muestra las coordenadas.
            ClearDrawingRectangleState()               ' Elimina el Rectangulo de OCR

            If Me.DrawingTableButton.Checked Then
                ConfigureDrawingLineOCRButtons(True)
                Me.isDrawingLine = False               ' Deshabilita el modo de dibujo de linea Horizontal
                selectedOrientationLine = Nothing      ' Reset estado orientacion linea
            End If
        End Sub

        ''' <summary>
        ''' Establece la visibilidad del ToolStripSeparator7 en función de la visibilidad de los botones DrawingTableButton y DrawingRectangleButton.
        ''' </summary>
        Private Sub SetVisibleToolStripSeparator7()

            Dim tableButtonVisible As Boolean = DrawingTableButton.Visible              ' Obtiene el estado de visibilidad de los boton DrawingTableButton
            Dim rectangleButtonVisible As Boolean = DrawingRactangleButton.Visible      ' Obtiene el estado de visibilidad de los boton DrawingRectangleButton.

            ToolStripSeparator7.Visible = tableButtonVisible Or rectangleButtonVisible  ' Establece la visibilidad de ToolStripSeparator7 en función de los botones.
        End Sub


        ''' <summary>
        ''' Cierra el control Table actual y realiza acciones adicionales si está configurado.
        ''' </summary>
        ''' <remarks>
        ''' Este método se utiliza para cerrar el control actual y, si existe un control siguiente configurado,
        ''' establece el foco en dicho control. Además, oculta un control de cuadrícula asociado.
        ''' </remarks>
        Private Sub CloseButtonClick()

            ' Comprueba si se está utilizando OCR
            Dim valueIsOCRCaptureUsed As Boolean = IsOCRCaptureUsed() ' Si se está utilizando OCR, establece los datos OCR

            If (Me.SelectedTableInputControl IsNot Nothing) Then
                If (valueIsOCRCaptureUsed) Then
                    Me.SelectedTableInputControl.EnableTableOCR = False
                    Me.SelectedTableInputControl.SetDataOcr()          ' Si se está utilizando OCR, establece los datos OCR
                End If
                Me.SelectedTableInputControl.DisposeUnusedControls()   ' Elimina los controles creados de la tabla
                Me.SelectedTableInputControl.NextControl.Focus()       ' Pasa al siguiente control y lo enfoca
            End If

            If valueIsOCRCaptureUsed Then
                ConfigureDrawingLineOCRButtons(False)
                ClearDrawingState()
                ClearDrawingRectangleState()                           ' Elimina el Rectangulo de OCR
            End If

            HideGridControl()
        End Sub

        ''' <summary>
        ''' Procesa datos OCR, organiza las columnas según la selección del usuario y construye un diccionario ordenado.
        ''' </summary>
        Private Sub GenerateOcrDictFromColumns()

            If Me.dataResultDictionaryOcr.Count > 0 AndAlso Me.dataResultDictionaryOcr.ContainsKey(0) Then   ' Verificar si hay datos OCR y si se encuentra en la clave 0

                If (SelectedTableInputControl IsNot Nothing) Then

                    For Each kvp As KeyValuePair(Of Integer, List(Of String())) In dataResultDictionaryOcr

                        Dim key As Integer = kvp.Key                                                        ' Obtiene la clave actual
                        Me.SelectedTableInputControl.ProcessAndAddOcrDataForSelectedColumn(key)
                    Next

                    SetButtonProperties(Me.Table_ColumnOCRButton, False, False)                             ' Deshabilita y oculta el boton de ColumnOCR para ejecutar tabla OCR
                    SetButtonProperties(Me.Table_SaveButton, True, True)                                    ' Deshabilita y oculta el boton de guardar tabla
                    Me.ImagePictureBox.Refresh()                                                            ' Actualizar y refrescar la imagen
                End If
            End If
        End Sub

        ''' <summary>
        ''' Procesa la respuesta JSON recibida del servicio OCR de una tabla y actualiza los datos necesarios en la interfaz.
        ''' </summary>
        ''' <param name="responseTextOCRTable">La respuesta JSON del servicio OCR de una tabla.</param>
        Private Sub ProcessOCRTableResponse(responseTextOCRTable As Dictionary(Of Integer, List(Of String())))

            Me.dataResultDictionaryOcr = responseTextOCRTable                                           ' Actualiza los datos necesarios.

            If (Me.SelectedTableInputControl IsNot Nothing) Then
                SetButtonProperties(Me.Table_ColumnOCRButton, True, True)                               ' Habilita el boton para mostrar data OCRTable
                SetButtonProperties(Me.Table_SaveButton, False, False)                                  ' Deshabilita boton guardar data de la tabla
                Me.SelectedTableInputControl.getDataOCR(Me.dataResultDictionaryOcr)
            End If
        End Sub


        ''' <summary>
        ''' Procesa la respuesta OCR de rectángulo actualizando los datos necesarios.
        ''' </summary>
        ''' <param name="responseTextOCR">El texto de respuesta OCR del servicio OCR Tesseract o Textract.</param>
        Private Sub ProcessOCRRectangleResponse(responseTextOCR As List(Of String))

            Try

                'Me.dataResultListOcr = ConvertJsonToStringList(responseText)  ' Procesa la respuesta JSON y actualiza los datos necesarios.
                Me.dataResultListOcr = responseTextOCR

                If _SelectedInputControl IsNot Nothing Then

                    If _SelectedInputControl.Tipo = DesktopConfig.CampoTipo.Lista Then
                        ProcessListInputControl()
                    ElseIf _SelectedInputControl.Tipo = DesktopConfig.CampoTipo.TablaAsociada Then
                        '_SelectedInputControl.Value = Me.dataResultListOcr(0)
                        If SelectedTableInputControl IsNot Nothing Then
                            SelectedTableInputControl.AssignOCRCellValue(Me.dataResultListOcr(0))
                        End If
                    Else
                        _SelectedInputControl.Value = Me.dataResultListOcr(0)
                    End If

                    Me.dataResultListOcr = Nothing                            ' Libera recursos de memoria
                    EnfocarMarca()                                            ' Procesa la respuesta JSON y actualiza los datos necesarios.
                    ImagePictureBox.Refresh()                                 ' Refresca la imagen en el PictureBox.
                End If

            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Procesa un control de entrada de lista, identifica el valor más similar a la entrada OCR
        ''' y actualiza el valor del control de entrada con ese resultado.
        ''' </summary>
        Private Sub ProcessListInputControl()

            Dim strReceived As String = Me.dataResultListOcr(0).ToLower().Trim()

            If Not String.IsNullOrEmpty(strReceived) AndAlso TypeOf _SelectedInputControl Is ListInputControl Then
                ProcessListInputControlItems(DirectCast(_SelectedInputControl, ListInputControl), strReceived)
            End If
        End Sub


        ''' <summary>
        ''' Procesa los elementos de un control de entrada de lista, identifica el valor más similar a la entrada OCR
        ''' y actualiza el valor del control de entrada con ese resultado.
        ''' </summary>
        ''' <param name="listInputControl">Control de entrada de lista que se va a procesar.</param>
        ''' <param name="inputStringOCR">Entrada proveniente del OCR para comparar con los elementos de la lista.</param>
        Private Sub ProcessListInputControlItems(listInputControl As ListInputControl, InputStringOCR As String)

            Dim valueComboBox As ComboBox = listInputControl.ValueDesktopComboBox
            Dim similarValue As String = String.Empty
            Dim minimumDistance As Integer = Integer.MaxValue

            For Each item As Object In valueComboBox.Items

                Dim row As DataRow = TryCast(item, DataRowView).Row

                If row IsNot Nothing Then
                    Dim etiquetaValueRow As Object = row("Etiqueta_Campo_Lista_Item")

                    If Not String.IsNullOrEmpty(etiquetaValueRow.ToString()) Then

                        Dim listItemValue As String = CStr(etiquetaValueRow).ToLower().Trim()
                        Dim currentDistance As Integer = CalculateLevenshteinDistance(InputStringOCR, listItemValue)

                        If currentDistance <= minimumDistance Then
                            minimumDistance = currentDistance
                            similarValue = row("Valor_Campo_Lista_Item").ToString()
                        End If
                    End If
                End If
            Next

            _SelectedInputControl.Value = similarValue
        End Sub


        ''' <summary>
        ''' Maneja el evento de clic en el botón de StartOCR, permitiendo
        ''' al usuario poder inciar el proceso de indetificacion de caracteres a cada casilla de la tabla
        ''' </summary>
        Private Sub HandleStartOCRButtonClick()

            'Habilita o deshabilita el check de boton al hacer click
            Me.StartOCRButton.Checked = Not Me.StartOCRButton.Checked

            If Me.StartOCRButton.Checked Then

                SetCheckBoxProperties(Me.DrawingTableButton, True, False, False)                        ' Configurar boton para Dibujar Tabla
                SetCheckBoxProperties(Me.DrawingRactangleButton, True, False, False)
                ConfigureDrawingLineButtons(True, False, False)                                         ' Configura los botones para dibujar lineas (H y V)
                selectedOrientationLine = Nothing                                                       ' Estblece orientacion Vertical para las lineas
                Me.ImagePictureBox.Refresh()                                                            ' Actualizar la imagen para reflejar los cambios
                Application.DoEvents()

                Dim rectangleTable As Rectangle = GetRectangle(drawRectangleTable)                      ' Crear las líneas de rectángulo
                Dim allLinesTable As New Dictionary(Of LineOrientation, List(Of Point()))               ' Crear un diccionario para almacenar las líneas horizontales y verticales

                ' Agregar las listas de líneas horizontal y vertical al diccionario
                allLinesTable.Add(LineOrientation.Horizontal, New List(Of Point())(Me.horizontalLinesList))
                allLinesTable.Add(LineOrientation.Vertical, New List(Of Point())(Me.verticalLinesList))

                Dim allLinesInBorderedTable = CreateLinesRectangleTable(rectangleTable, allLinesTable)
                Dim sortAllLinesInBorderedTable = SortHorizontalAndVerticalLines(allLinesInBorderedTable)
                Dim columnCoordinates = CalculateCellCoordinates(sortAllLinesInBorderedTable)           ' Calcular las coordenadas de las celdas

                Dim ColumnCoordinatesRectangle = CalculateCellCoordinatesRectangle(sortAllLinesInBorderedTable)

                ExtractTitleMetadata(Me.Text)
                Dim imagePicture As Image = GetImageFromPictureBox()
                Dim resulOCRTable = _OCRCaptura.ProcessOCRTable(imagePicture, rectangleTable, ColumnCoordinatesRectangle)
                ProcessOCRTableResponse(resulOCRTable)

                Me.StartOCRButton.Checked = False
                EnableDrawingTableOCRMode()
            Else
                EnableDrawingTableOCRMode()
            End If

            Me.ImagePictureBox.Invalidate()                                                             ' Actualizar la imagen para reflejar los cambios
        End Sub

        ''' <summary>
        ''' Habilita el modo de dibujo de tablas para OCR, configurando los controles necesarios.
        ''' </summary>
        ''' <remarks>
        ''' Este método habilita el modo de dibujo de tablas específicamente diseñado para el reconocimiento óptico de caracteres (OCR).
        ''' Configura los botones necesarios para esta funcionalidad, como el botón para dibujar tablas y los botones para dibujar líneas (horizontales y verticales).
        ''' </remarks>
        Private Sub EnableDrawingTableOCRMode()
            SetCheckBoxProperties(Me.DrawingTableButton, True, True, True)                          ' Configurar boton para Dibujar Tabla
            ConfigureDrawingLineButtons(True, True, False)                                          ' Configura los botones para dibujar lineas (H y V)
            SetCheckBoxProperties(Me.DrawingRactangleButton, True, True, False)
        End Sub

        ''' <summary>
        ''' Maneja el evento de clic en el botón de eliminación de líneas, permitiendo
        ''' al usuario eliminar la última línea en la orientación seleccionada.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub HandleDeleteLineButtonClick()

            'Habilita o deshabilita el check de boton al hacer click
            Me.DeleteLineButton.Checked = Not Me.DeleteLineButton.Checked


            If Me.DeleteLineButton.Checked Then

                If (horizontalLinesList.Count > 0) OrElse (verticalLinesList.Count > 0) Then
                    DeleteLastLine(selectedOrientationLine)   'elimina la última línea si hay líneas existentes.
                End If

                Me.DeleteLineButton.Checked = False           ' Desmarca el botón después de eliminar la línea.
            End If

            Me.ImagePictureBox.Invalidate()                   ' Actualizar la imagen para reflejar los cambios
        End Sub

        ''' <summary>
        ''' Maneja el evento de clic en el botón para dibujar líneas verticales.
        ''' Cambia el estado del botón, habilitando o deshabilitando el dibujo de líneas verticales.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub HandleDrawVerticalLinesButtonClick()

            Me.DrawVerticalLinesButton.Checked = Not Me.DrawVerticalLinesButton.Checked 'Habilita o deshabilita el check de boton al hacer click

            Me.DrawHorizontalLinesButton.Checked = False            ' Desmarca el botón de dibujo de líneas horizontales.

            If Me.DrawVerticalLinesButton.Checked Then
                Me.isDrawingLine = True                             'Habilita el dibujo de la linea Horizontal
                selectedOrientationLine = LineOrientation.Vertical  ' Estblece orientacion Vertical para las lineas
            Else
                selectedOrientationLine = Nothing                   ' Reset Orientacion lineas
                Me.isDrawingLine = False                            'deshabilita el dibujo de la linea Horizontal
            End If

            Me.ImagePictureBox.Invalidate()                         ' Actualizar la imagen para reflejar los cambios
        End Sub

        ''' <summary>
        ''' Maneja el evento de clic en el botón para dibujar líneas horizontales.
        ''' Ajusta la orientación de las líneas y el estado de los botones relacionados.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub HandleDrawHorizontalLinesButtonClick()

            Me.DrawHorizontalLinesButton.Checked = Not Me.DrawHorizontalLinesButton.Checked 'Habilita o deshabilita el check de boton al hacer click
            Me.DrawVerticalLinesButton.Checked = False                                      ' Desmarca el botón de dibujo de líneas verticales.

            If Me.DrawHorizontalLinesButton.Checked Then
                Me.isDrawingLine = True                               'Habilita el dibujo de la linea Horizontal
                selectedOrientationLine = LineOrientation.Horizontal  ' Estblece orientacion horizontal para las lineas
            Else
                selectedOrientationLine = Nothing                     ' Reset Orientacion lineas
                Me.isDrawingLine = False                              'deshabilita el dibujo de la linea Horizontal
            End If

            Me.ImagePictureBox.Invalidate()                           ' Actualizar la imagen para reflejar los cambios

        End Sub

        ''' <summary>
        ''' Maneja el evento de clic en el botón para habilitar/deshabilitar el dibujo de tablas.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub HandleDrawingTableButton()
            'Habilita o deshabilita el check de boton al hacer click
            Me.DrawingTableButton.Checked = Not Me.DrawingTableButton.Checked

            If _SelectedInputControl IsNot Nothing Then
                _SelectedInputControl.EnableTableOCR = Me.DrawingTableButton.Checked
            End If

            If Me.DrawingTableButton.Checked Then

                Me.IsDrawingTableEnabled = True               ' Habilita el dibujo del cuadro al hacer click
                Me.labelTableCoordinates.Visible = True
                ConfigureDrawingLineOCRButtons(True)
            Else
                Me.IsDrawingTableEnabled = False              ' Deshabilita el dibujo del cuadro al hacer click
                ClearDrawingState()                           ' Elimina la tabla dibujada
                ConfigureDrawingLineOCRButtons(False)
                Me.labelTableCoordinates.Visible = False
                selectedOrientationLine = Nothing             ' Reset estado orientacion linea
            End If

            ClearDrawingRectangleState()                      ' Elimina el Rectangulo OCR dibujado
            Me.ImagePictureBox.Invalidate()                   ' Invalida la imagen para que refleje los cambios
        End Sub

        ''' <summary>
        ''' Configura los botones para dibujar líneas según el estado habilitado/deshabilitado.
        ''' </summary>
        ''' <param name="enable">Especifica si los botones deben estar habilitados o deshabilitados.</param>
        ''' <remarks></remarks>
        Private Sub ConfigureDrawingLineOCRButtons(enable As Boolean)
            SetCheckBoxProperties(Me.StartOCRButton, enable, False, False)            ' Configura botón para realizar el OCR
            ConfigureDrawingLineButtons(enable, False, False)
        End Sub


        ''' <summary>
        ''' Configura los botones para operaciones de dibujo.
        ''' </summary>
        ''' <param name="visible">Indica si los botones deben ser visibles o no.</param>
        ''' <param name="enable">Indica si los botones deben estar habilitados o deshabilitados.</param>
        ''' <param name="isChecked">Indica si los botones deben estar marcados o desmarcados.</param>
        Private Sub ConfigureDrawingLineButtons(visible As Boolean, enable As Boolean, isChecked As Boolean)

            SetCheckBoxProperties(Me.DrawHorizontalLinesButton, visible, enable, isChecked)  ' Configurar boton para Dibujar Linea Horizontal
            SetCheckBoxProperties(Me.DrawVerticalLinesButton, visible, enable, isChecked)    ' Configurar boton para Dibujar Linea Vertical
            SetCheckBoxProperties(Me.DeleteLineButton, visible, enable, isChecked)           ' Configurar boton para eliminar lineas
        End Sub

        ''' <summary>
        ''' Configura las propiedades de un CheckBox, como Enabled, Visible y Checked.
        ''' </summary>
        ''' <param name="toolStripButton">El ToolStripButton que se va a configurar.</param>
        ''' <param name="visible">Indica si el CheckBox debe ser visible.</param>
        ''' <param name="enable">Indica si el CheckBox debe estar habilitado.</param>
        ''' <param name="isChecked">Indica si el CheckBox debe estar marcado (Checked).</param>
        ''' <remarks></remarks>
        Private Sub SetCheckBoxProperties(toolStripButton As ToolStripButton, visible As Boolean?, enable As Boolean?, isChecked As Boolean?)

            If visible.HasValue Then toolStripButton.Visible = visible.Value
            If enable.HasValue Then toolStripButton.Enabled = enable.Value
            If isChecked.HasValue Then toolStripButton.Checked = isChecked.Value

        End Sub

        ''' <summary>
        ''' Establece las propiedades de visibilidad y habilitación de un botón.
        ''' </summary>
        ''' <param name="button">Establece las propiedades de visibilidad y habilitación de un botón.</param>
        ''' <param name="visible">True para hacer visible el botón, False para ocultarlo. Null para no cambiar la visibilidad.</param>
        ''' <param name="enable">True para habilitar el botón, False para deshabilitarlo. Null para no cambiar la habilitación.</param>
        ''' <remarks></remarks>
        Private Sub SetButtonProperties(button As Button, visible As Boolean?, enable As Boolean?)

            If visible.HasValue Then button.Visible = visible.Value
            If enable.HasValue Then button.Enabled = enable.Value

        End Sub


        ''' <summary>
        ''' Restablece las variables utilizadas para dibujar un rectángulo y limpia el texto de coordenadas de la tabla.
        ''' </summary>
        Private Sub ClearDrawingRectangleState()
            ' Restablece las variables para dibujar un rectángulo.
            Me.drawRectangle = Rectangle.Empty
            Me.rectangleStartPoint = Nothing
            Me.rectangleEndPoint = Nothing

            Me.labelTableCoordinates.Text = "" ' Limpia y oculta el texto de coordenadas de la tabla.
        End Sub


        ''' <summary>
        ''' Limpia el estado de dibujo, restableciendo todas las variables y listas relacionadas con el dibujo.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ClearDrawingState()

            If Not drawRectangleTable.IsEmpty Then
                Me.drawRectangleTable = Rectangle.Empty
                Me.rectangleTableStartPoint = Nothing
                Me.rectangleTableEndPoint = Nothing

                Me.isDrawingLine = False           'deshabilita el modo de dibujo de linea Horizontal
                selectedOrientationLine = Nothing  ' Reset estado orientacion linea

                ' Limpia las listas de las lineas almacenadas
                horizontalLinesList.Clear()
                verticalLinesList.Clear()

                Me.labelTableCoordinates.Text = "" ' Limpia y oculta el texto de coordenadas de la tabla.

                If Me.DrawingTableButton.Checked Then
                    Me.DrawingTableButton.Checked = False
                End If
            End If

        End Sub

        ''' <summary>
        ''' Habilita o deshabilita el botón para eliminar líneas en función de la orientación y la existencia de líneas en la lista correspondiente.
        ''' </summary>
        ''' <param name="lineOrientation">La orientación de la línea (Horizontal o Vertical).</param>
        ''' <remarks></remarks>
        Private Sub UpdateDeleteLineButton(lineOrientation As LineOrientation?)

            ' ' Habilita el botón para eliminar líneas en función de la orientación y la existencia de líneas en la lista correspondiente.
            If lineOrientation IsNot Nothing Then
                If lineOrientation = FormIndexerView.LineOrientation.Horizontal AndAlso Me.horizontalLinesList.Count > 0 Then
                    Me.DeleteLineButton.Enabled = True                     ' Habilita el botón si hay líneas horizontales.
                ElseIf lineOrientation = FormIndexerView.LineOrientation.Vertical AndAlso Me.verticalLinesList.Count > 0 Then
                    Me.DeleteLineButton.Enabled = True                     ' Habilita el botón si hay líneas verticales.
                Else
                    Me.DeleteLineButton.Enabled = False                    ' Deshabilita el botón si no hay líneas de la orientación especificada.
                End If

            Else
                Me.DeleteLineButton.Enabled = False                        ' Si la orientación no está especificada, deshabilita el botón.
            End If

        End Sub

        ''' <summary>
        ''' Elimina la última línea horizontal o vertical de la lista correspondiente y actualiza la imagen.
        ''' </summary>
        ''' <param name="lineOrientation">La orientación de la línea a eliminar (Horizontal o Vertical).</param>
        ''' <remarks></remarks>
        Private Sub DeleteLastLine(lineOrientation As LineOrientation?)

            ' Verifica si se proporciona una orientación válida
            If lineOrientation IsNot Nothing Then
                Dim lastIndex As Integer

                ' Determina si se debe eliminar una línea horizontal o vertical
                If lineOrientation = FormIndexerView.LineOrientation.Horizontal Then
                    lastIndex = Me.horizontalLinesList.Count - 1   ' Obteniene la ultima posición
                    Me.horizontalLinesList.RemoveAt(lastIndex)     ' Elimina el último elemento
                Else
                    lastIndex = Me.verticalLinesList.Count - 1     ' Obteniene la ultima posición
                    Me.verticalLinesList.RemoveAt(lastIndex)       ' Elimina el último elemento
                End If

                Me.ImagePictureBox.Invalidate()                    ' Invalida la imagen para que refleje los cambios
            End If
        End Sub


        ''' <summary>
        ''' Guarda líneas horizontales o verticales según la orientación proporcionada en el punto especificado por el evento del ratón.
        ''' </summary>
        ''' <param name="e">Argumentos del evento del ratón que contiene la ubicación y el botón del ratón.</param>
        ''' <param name="orientation">La orientación de la línea (Horizontal o Vertical).</param>
        Private Sub saveLines(e As MouseEventArgs, orientation As LineOrientation?)

            ' calcula la relación de escala en el eje X y Y
            Dim scaleLevels As Double() = CalculateScaleImageXY()

            ' Calcula punto  y dimesiones del rectangulo en La imagen Original
            Dim xStartPoint As Integer = CInt(Math.Round(e.Location.X * scaleLevels(0)))
            Dim yStartPoint As Integer = CInt(Math.Round(e.Location.Y * scaleLevels(1)))

            ' Agrega la nueva línea a la lista correspondiente según la orientación
            If Me.drawRectangleTable.Contains(xStartPoint, yStartPoint) AndAlso e.Button = MouseButtons.Left Then

                ' Agrega la nueva línea a la lista correspondiente según la orientación
                If orientation = LineOrientation.Horizontal Then
                    Dim newLine As Point() = {New Point(Me.drawRectangleTable.Left, yStartPoint), New Point(Me.drawRectangleTable.Right, yStartPoint)}
                    Me.horizontalLinesList.Add(newLine)
                ElseIf orientation = LineOrientation.Vertical Then
                    Dim newLine As Point() = {New Point(xStartPoint, Me.drawRectangleTable.Top), New Point(xStartPoint, Me.drawRectangleTable.Bottom)}
                    Me.verticalLinesList.Add(newLine)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Dibuja un rectángulo en una imagen según las condiciones del botón de dibujo y la configuración de zoom.
        ''' </summary>
        ''' <param name="e">Argumentos de PaintEventArgs para el evento de pintura.</param>
        Private Sub DrawRectangleOnImage(e As PaintEventArgs)

            Using tempImageRectangle As New Bitmap(ImagePictureBox.Width, ImagePictureBox.Height) ' Crea un bitmap temporal para dibujar
                Using bufferGraphics As Graphics = Graphics.FromImage(tempImageRectangle)         ' Crea un objeto Graphics asociado al bitmap

                    If (Me.StartOCRButton.Checked AndAlso Me.DrawingRactangleButton.Checked) OrElse Me.DrawingRactangleButton.Checked Then    ' Verifica si el botón para dibujar rectángulo está marcado

                        bufferGraphics.Clear(Color.Transparent)                                   ' Limpia el rectángulo

                        ' Dibuja el rectángulo utilizando las coordenadas y dimensiones calculadas
                        Using Pen As New Pen(Color.Blue, 2)
                            Dim rectangleData As Rectangle = CalculateZoomedRectanglCoordinatesAndSize(drawRectangle, rectangleStartPoint, rectangleEndPoint, Me._Zoom)
                            bufferGraphics.DrawRectangle(Pen, rectangleData.X, rectangleData.Y, rectangleData.Width, rectangleData.Height)
                        End Using
                    Else
                        bufferGraphics.Clear(Color.Transparent)                                   ' Limpia el rectángulo
                    End If

                    e.Graphics.DrawImage(tempImageRectangle, 0, 0)                                ' Dibuja la imagen resultante en el Graphics del evento Paint
                End Using
            End Using
        End Sub

        ''' <summary>
        ''' Dibuja un rectángulo con sus respectivas lineas creadas por el usuario en un bitmap temporal 
        ''' y lo muestra en el objeto Graphics del evento Paint.
        ''' </summary>
        ''' <param name="e">Los argumentos del evento Paint que contienen el objeto Graphics para dibujar.</param>
        Private Sub DrawRectangleLinesOnImage(e As PaintEventArgs)

            ' Verificar si ImagePictureBox está inicializado y tiene un tamaño válido
            If ImagePictureBox Is Nothing OrElse ImagePictureBox.Width <= 0 OrElse ImagePictureBox.Height <= 0 Then
                Throw New InvalidOperationException("ImagePictureBox no está inicializado o tiene dimensiones no válidas.")
            End If

            Using tempImageRectangle As New Bitmap(ImagePictureBox.Width, ImagePictureBox.Height) ' Crea un bitmap temporal para dibujar
                Using bufferGraphics As Graphics = Graphics.FromImage(tempImageRectangle)         ' Crea un objeto Graphics asociado al bitmap

                    If Me.DrawingTableButton.Checked OrElse Me.StartOCRButton.Checked Then        ' Verifica si el botón para dibujar Tabla está marcado

                        bufferGraphics.Clear(Color.Transparent)                                   ' Limpia el rectángulo

                        ' Dibuja el rectángulo d elos bordes de la tabla utilizando las coordenadas y dimensiones calculadas
                        Using Pen As New Pen(Color.Red, 2)
                            Dim rectangleData As Rectangle = CalculateZoomedRectanglCoordinatesAndSize(drawRectangleTable, rectangleTableStartPoint, rectangleTableEndPoint, Me._Zoom)
                            bufferGraphics.DrawRectangle(Pen, rectangleData.X, rectangleData.Y, rectangleData.Width, rectangleData.Height)
                        End Using

                        ' Dibuja linea Horizontal creada previamente
                        If Me.horizontalLinesList.Count > 0 Then
                            DrawLines(bufferGraphics, Me.horizontalLinesList, Me._Zoom)
                        End If

                        ' Dibuja linea vertical creada previamente
                        If Me.verticalLinesList.Count > 0 Then
                            DrawLines(bufferGraphics, Me.verticalLinesList, Me._Zoom)
                        End If

                    Else
                        bufferGraphics.Clear(Color.Transparent)                                 ' Limpia el rectángulo
                    End If

                    e.Graphics.DrawImage(tempImageRectangle, 0, 0)                              ' Dibuja la imagen resultante en el Graphics del evento Paint
                End Using
            End Using
        End Sub

        ''' <summary>
        ''' Dibuja las líneas verticales almacenadas en la lista, teniendo en cuenta el zoom.
        ''' </summary>
        ''' <param name="bufferGraphics">El objeto Graphics en el que se dibujarán las líneas.</param>
        ''' <param name="verticalLinesList">La lista de líneas verticales a dibujar.</param>
        ''' <param name="zoom">El nivel de zoom actual en porcentaje.</param>
        Private Sub DrawLines(bufferGraphics As Graphics, verticalLinesList As List(Of Point()), zoom As Integer)
            For Each line As Point() In verticalLinesList
                Dim zoomCurrentRectangle As Single = CSng((zoom / 100)) ' Calcula el Zoom en el cual se encuentra la imagen

                Dim pointDataLine As New List(Of Point)

                ' Aplica el zoom a los puntos de la línea
                For Each p As Point In line
                    pointDataLine.Add(New Point(CInt(CInt(p.X) * zoomCurrentRectangle), CInt(CInt(p.Y) * zoomCurrentRectangle)))
                Next

                Dim newLine As Point() = {pointDataLine(0), pointDataLine(1)}

                ' Dibuja las líneas verticales almacenadas
                Using pen As New Pen(Color.Red, 2)
                    bufferGraphics.DrawLine(pen, newLine(0), newLine(1))
                End Using
            Next
        End Sub

        Private Sub AjustarAlto()
            '_Ajuste = EnumAjuste.ALTO

            If (ImagePictureBox.Image.Height >= ImagePictureBox.Image.Width) Then
                Me._Zoom = CShort(((MarcoDibujoPanel.Height - 30) / ImagePictureBox.Image.Height) * 100)
            Else
                Me._Zoom = CShort(((MarcoDibujoPanel.Height - 50) / ImagePictureBox.Image.Height) * 100)
            End If

            ZoomToolStripComboBox.Text = Format(Me._Zoom, "0") & "%"

            AplicarZoom()

            MarcoDibujoPanel.AutoScrollPosition = New Point(0, 0)

            If MarcoDibujoPanel.Width < ImagePanel.Width Then
                ImagePanel.Location = New Point(10, ImagePanel.Location.Y)
            End If
        End Sub

        ''' <summary>
        ''' Ajusta el zoom de la imagen y el desplazamiento del scroll para enfocar un rectángulo dibujado, listo para ser utilizado en el sistema de reconocimiento óptico de caracteres (OCR).
        ''' </summary>
        ''' <param name="rectangleTable">El rectángulo que se desea enfocar y utilizar como base para el sistema de OCR.</param>
        ''' <remarks>
        ''' Este método calcula el zoom necesario para que el rectángulo especificado se ajuste al ancho del panel de dibujo.
        ''' Luego, ajusta el desplazamiento del scroll para centrar el rectángulo dentro del panel, de manera que esté listo para ser utilizado en el sistema de OCR.
        ''' </remarks>
        Private Sub AjustarAnchoRectangleTableOCR(rectangleTable As Rectangle)

            If Not ImagePictureBox.Image Is Nothing Then

                If (ImagePictureBox.Image.Width >= ImagePictureBox.Image.Height) Then
                    Me._Zoom = CShort(((MarcoDibujoPanel.Width - 30) / rectangleTable.Width) * 100)
                Else
                    Me._Zoom = CShort(((MarcoDibujoPanel.Width - 50) / rectangleTable.Width) * 100)
                End If

                If Me._Zoom < 200 Then

                    ZoomToolStripComboBox.Text = Format(Me._Zoom, "0") & "%"
                    AplicarZoom()

                    Dim zoom As Integer = CType(Me._Zoom, Integer)
                    ' Calcular la posición ideal del scroll teniendo en cuenta el zoom
                    Dim scrollX As Integer = (rectangleTable.X * zoom \ 100) - (MarcoDibujoPanel.Width \ 2) + ((rectangleTable.Width * zoom \ 100) \ 2)
                    Dim scrollY As Integer = (rectangleTable.Y * zoom \ 100) - (MarcoDibujoPanel.Height \ 2) + ((rectangleTable.Height * zoom \ 100) \ 2)

                    MarcoDibujoPanel.AutoScrollPosition = New Point(scrollX, scrollY)
                End If
            End If
        End Sub


        Private Sub AjustarAncho()
            If Not ImagePictureBox.Image Is Nothing Then


                If (ImagePictureBox.Image.Width >= ImagePictureBox.Image.Height) Then
                    Me._Zoom = CShort(((MarcoDibujoPanel.Width - 30) / ImagePictureBox.Image.Width) * 100)
                Else
                    Me._Zoom = CShort(((MarcoDibujoPanel.Width - 50) / ImagePictureBox.Image.Width) * 100)
                End If

                ZoomToolStripComboBox.Text = Format(Me._Zoom, "0") & "%"

                AplicarZoom()

                MarcoDibujoPanel.AutoScrollPosition = New Point(0, 0)

                If MarcoDibujoPanel.Height < ImagePanel.Height Then
                    ImagePanel.Location = New Point(ImagePanel.Location.X, 10)
                End If
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
            If ((ImagePictureBox.Image.Height / MarcoDibujoPanel.Height) >= (ImagePictureBox.Image.Width / MarcoDibujoPanel.Width)) Then
                AjustarAlto()
            Else
                AjustarAncho()
            End If
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

                ActivarControles(True)
            End If
        End Sub

        Private Sub RotateLeft()
            MarcoDibujoPanel.AutoScrollPosition = New Point(0, 0)
            ImagePanel.Size = New Size(ImagePanel.Height, ImagePanel.Width)
            ImagePictureBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)

            Me.IndexerController.ReemplazarImagen = True

            AplicarZoom()
        End Sub

        Private Sub RotateRight()
            MarcoDibujoPanel.AutoScrollPosition = New Point(0, 0)
            ImagePanel.Size = New Size(ImagePanel.Height, ImagePanel.Width)
            ImagePictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)

            Me.IndexerController.ReemplazarImagen = True

            AplicarZoom()
        End Sub

        Private Sub FlipHorizontal()
            ImagePictureBox.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
            ImagePictureBox.Refresh()

            Me.IndexerController.ReemplazarImagen = True
        End Sub

        Private Sub FlipVertical()
            ImagePictureBox.Image.RotateFlip(RotateFlipType.RotateNoneFlipY)
            ImagePictureBox.Refresh()

            Me.IndexerController.ReemplazarImagen = True
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

        Private Sub DesIndexarFolio()
            If Me.Controller.CurrentImageIndex > 0 Then

                If (_PreguntarPreviousImage) Then
                    Dim Respuesta = MessageBox.Show("Esta acción provocará perdida de información al remover el folio actual, ¿desea continuar la operación?", Program.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If (Respuesta <> DialogResult.Yes) Then
                        Return
                    End If
                End If

                _PreguntarPreviousImage = False

                Me.IndexerController.DesindexarFolio()

                ShowImagen(True)

                AjustarAncho()

                ActivarControles(True)
            End If
        End Sub

        Private Sub ShowCampos()
            CamposPanel.Controls.Clear()
            CamposLlavePanel.Controls.Clear()
            ValidacionesPanel.Controls.Clear()

            If TipoDocumentalComboBox.SelectedIndex >= 0 Then
                Me._Campos = Me.IndexerController.Campos(CInt(TipoDocumentalComboBox.SelectedValue))
                Me._CamposLlave = Me.IndexerController.CamposLlave(CInt(TipoDocumentalComboBox.SelectedValue))
                Me.Validaciones = Me.IndexerController.Validaciones(CInt(TipoDocumentalComboBox.SelectedValue))
                Me.ValidacionEmbargos = Me.IndexerController.ValidacionListas()

                If (Me._Campos.Count > 0) Then
                    Dim PreviousControl As IInputControl = Nothing

                    For Each RowCampo In Me._Campos
                        RowCampo.Control.IndexerView = Me

                        Dim Control = CType(RowCampo.Control, Control)
                        Control.Dock = DockStyle.Top            ' Ancla el control en la parte superior del contenedor
                        CamposPanel.Controls.Add(Control)
                        Control.BringToFront()
                        Me.IndexerController.GetValueFileData(CInt(RowCampo.Control.CampoCaptura.id))

                        If (PreviousControl IsNot Nothing) Then
                            PreviousControl.NextControl = Control
                        End If

                        PreviousControl = RowCampo.Control
                    Next

                    If (Me._CamposLlave.Count > 0) Then
                        Me._Campos(Me._Campos.Count - 1).Control.NextControl = CType(_CamposLlave(0).Control, Control)
                    ElseIf (Me.Validaciones.Count > 0) Then
                        Me._Campos(Me._Campos.Count - 1).Control.NextControl = Me.Validaciones(0).Control
                    Else
                        Me._Campos(Me._Campos.Count - 1).Control.NextControl = NextFolioButton
                    End If
                End If

                If (Me._CamposLlave.Count > 0) Then
                    Dim PreviousControl As IInputControl = Nothing

                    Dim CamposLLave = Me.IndexerController.GetValueLlaveData()

                    For Each RowCampo In Me._CamposLlave
                        RowCampo.Control.IndexerView = Me

                        Dim Control = CType(RowCampo.Control, Control)
                        Control.Dock = DockStyle.Top
                        CamposLlavePanel.Controls.Add(Control)
                        Control.BringToFront()

                        If (PreviousControl IsNot Nothing) Then
                            PreviousControl.NextControl = Control
                        End If

                        PreviousControl = RowCampo.Control

                        If CamposLLave IsNot Nothing And RowCampo.fk_Tipo_Llave = 2 Then
                            RowCampo.Control.Value = CamposLLave.Campo_Empaque
                        End If
                    Next

                    If (Me.Validaciones.Count > 0) Then
                        Me._CamposLlave(Me._CamposLlave.Count - 1).Control.NextControl = Me.Validaciones(0).Control
                    Else
                        Me._CamposLlave(Me._CamposLlave.Count - 1).Control.NextControl = NextFolioButton
                    End If
                End If

                If (Me.Validaciones.Count > 0) Then
                    Dim PreviousControl As ValidationControl = Nothing

                    For Each RowValidacion In Me.Validaciones
                        RowValidacion.Control.IndexerView = Me

                        RowValidacion.Control.Dock = DockStyle.Top
                        ValidacionesPanel.Controls.Add(RowValidacion.Control)
                        RowValidacion.Control.BringToFront()

                        If (PreviousControl IsNot Nothing) Then
                            PreviousControl.NextControl = RowValidacion.Control
                        End If

                        PreviousControl = RowValidacion.Control
                    Next

                    Me.Validaciones(Me.Validaciones.Count - 1).Control.NextControl = NextFolioButton
                End If

                If Me.ValidacionEmbargos Then
                    Dim Control As ListValidationControl = New ListValidationControl

                    ValidacionListasPanel.Controls.Add(Control)

                End If
            End If

            ActivarControles(True)
        End Sub

        Private Sub ShowAnexos()
            Dim Anexos = Me.IndexerController.Anexos()

            ' Remover el TabPage de configuración
            If (Me.DocumentsTabControl.TabPages.Contains(Me.DocumentConfigTabPage)) Then
                Me.DocumentsTabControl.TabPages.Remove(Me.DocumentConfigTabPage)
            End If

            ' Eiminar los TabPages de anexos
            For Each Anexo In Me.AnexosList
                If (Anexo.TabPage IsNot Nothing) Then
                    Me.DocumentsTabControl.TabPages.Remove(Anexo.TabPage)

                    Anexo.ImageViewer.Dispose()
                    Anexo.ImageViewer = Nothing

                    Anexo.TabPage.Dispose()
                    Anexo.TabPage = Nothing
                End If
            Next

            Me.AnexosList.Clear()

            If (Anexos.Count > 0) Then
                Me.DocumentsCheckedListBox.Items.Clear()

                For Each Anexo In Anexos
                    Me.DocumentsCheckedListBox.Items.Add(Anexo.Display)

                    Dim NewAnexo As New AnexoClass()

                    NewAnexo.idAnexo = Integer.Parse(Anexo.Id)
                    NewAnexo.NombreAnexo = Anexo.Display
                    Me.AnexosList.Add(NewAnexo)
                Next

                Me.DocumentsTabControl.TabPages.Add(Me.DocumentConfigTabPage)
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
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + D", "Iniciar un nuevo Documento"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + F", "Iniciar un nuevo Folder"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Shift] + [Espacio]", "Iniciar un nuevo Folder"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + I", "Insertar una imagen"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + G", "Guardar los cambios"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + U", "Desindexar el último Folio"))
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
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + [.]", "Reflejar la imagen horizontalmente"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + [,]", "Reflejar la imagen verticalmente"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + [Up]", "Desplazar imagen hacia abajo"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + [Down]", "Desplazar imagen hacia arriba"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + [Left]", "Desplazar imagen hacia la derecha"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Alt] + [Right]", "Desplazar imagen hacia izquierda"))

            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Shift] + I", "Auto indexar"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Shift] + F", "Marcar folder actual como anexo"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Shift] + A", "Marcar todo el paquete como anexo"))

            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Shift] + R", "Mostrar u ocultar la regla de captura"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Shift] + [Inicio]", "Mover regla a la parte superior de la hoja"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Shift] + [Fin]", "Mover regla a la parte inferior de la hoja"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Shift] + M", "Subrayar"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Shift] + B", "Quitar subrayado"))

            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [F8]", "Seleccionar Tab de visualizacion de Documentos"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [F3]", "Cargar los documentos seleccionados"))

            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + 0", "Seleccionar Tab principal"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + 1", "Seleccionar Tab del primer documento anexo cargado"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + 2", "Seleccionar Tab del segundo documento anexo cargado"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + 3", "Seleccionar Tab del tercer documento anexo cargado"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + 4", "Seleccionar Tab del cuarto documento anexo cargado"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + 5", "Seleccionar Tab del quinto documento anexo cargado"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + 6", "Seleccionar Tab del sexto documento anexo cargado"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + 7", "Seleccionar Tab del septimo documento anexo cargado"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + 8", "Seleccionar Tab del octavo documento anexo cargado"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + 9", "Seleccionar Tab del noveno documento anexo cargado"))

            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Alt] + 1", "Seleccionar Tab del primer documento anexo y forzar el cargue"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Alt] + 2", "Seleccionar Tab del segundo documento anexo y forzar el cargue"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Alt] + 3", "Seleccionar Tab del tercer documento anexo y forzar el cargue"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Alt] + 4", "Seleccionar Tab del cuarto documento anexo y forzar el cargue"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Alt] + 5", "Seleccionar Tab del quinto documento anexo y forzar el cargue"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Alt] + 6", "Seleccionar Tab del sexto documento anexo y forzar el cargue"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Alt] + 7", "Seleccionar Tab del septimo documento anexo y forzar el cargue"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Alt] + 8", "Seleccionar Tab del octavo documento anexo y forzar el cargue"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + [Alt] + 9", "Seleccionar Tab del noveno documento anexo y forzar el cargue"))

            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + C", "Seleccionar Dibujar rectangulo OCR para el campo seleccionado"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + T", "Seleccionar Dibujar tabla"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + H", "Seleccionar Dibujar linea Horizontal en la tabla"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + V", "Seleccionar Dibujar linea Vertical en la tabla"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + B", "Seleccionar Eliminar Linea Anterior creada en la tabla"))
            AccesosForm.Lista.Add(New FormAccesosRapidos.TypeAcceso("[Ctrl] + O", "Seleccionar Ejecutar OCR en la Tabla Creada"))


            AccesosForm.ShowDialog()
        End Sub

        Private Sub NewDocumentFile()
            If (Me.IndexerController.NewDocumentFile()) Then
                ShowCampos()

                ActivarControles(True)
                BreakButton.Focus()
                'If (TipoDocumentalComboBox.Enabled) Then TipoDocumentalComboBox.Focus()

                MensajeToolStripStatusLabel.Text = "Nuevo documento..."
            End If
        End Sub

        Private Sub NewFolder()
            If (Me.IndexerController.NewFolder()) Then
                ShowCampos()

                ActivarControles(True)
                BreakButton.Focus()

                MensajeToolStripStatusLabel.Text = "Nueva carpeta..."
            End If
        End Sub

        Private Sub Save()
            Try
                Me.Enabled = False

                Dim Result = MessageBox.Show("Está seguro que desea almacenar los cambios realizados hasta el momento?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If (Result = DialogResult.Yes) Then
                    If (Controller.Save()) Then
                        _Unlock = False

                        Me.ImagePictureBox.Image = Nothing
                        Me.DialogResult = DialogResult.Yes
                        Me.Close()
                    End If
                End If
            Finally
                Me.Enabled = True
            End Try
        End Sub

        Private Sub AddFolio()
            If (Me.IndexerController.AddFolio()) Then
                UpdateAvance()
            End If
        End Sub

        Private Sub DeleteFolio()
            Dim Respuesta As DialogResult

            Respuesta = MessageBox.Show("Desea eliminar el folio actual?", Program.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If Respuesta = DialogResult.Yes Then
                Me.IndexerController.DeleteFolio()

                ShowImagen(False)

                AjustarAncho()

                ActivarControles(True)

                UpdateAvance()
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

        Private Sub EnfocarMarca()
            Try
                If (Me.SelectedInputControl IsNot Nothing) Then
                    If (Me.SelectedInputControl.CampoCaptura IsNot Nothing) Then
                        If (Me.SelectedInputControl.CampoCaptura.Usa_Marca) Then
                            Dim H, W As Integer

                            H = CInt(Me.ImagePictureBox.Image.Height * (Me.SelectedInputControl.CampoCaptura.Marca_Height_Campo / 100))
                            W = CInt(Me.ImagePictureBox.Image.Width * (Me.SelectedInputControl.CampoCaptura.Marca_Width_Campo / 100))

                            If ((H / Me.MarcoDibujoPanel.Height) >= (W / Me.MarcoDibujoPanel.Width)) Then
                                Me._Zoom = CShort(((Me.MarcoDibujoPanel.Height - 20) / H) * 75)
                            Else
                                Me._Zoom = CShort(((Me.MarcoDibujoPanel.Width - 20) / W) * 75)
                            End If

                            EnforceMaximumZoomForMarkFocus()

                            Me.ZoomToolStripComboBox.Text = Format(_Zoom, "0") & "%"

                            AplicarZoom()

                            H = CInt(Me.ImagePanel.Height * (Me.SelectedInputControl.CampoCaptura.Marca_Height_Campo / 100))
                            W = CInt(Me.ImagePanel.Width * (Me.SelectedInputControl.CampoCaptura.Marca_Width_Campo / 100))

                            Dim W1, H1, X1, Y1 As Integer
                            W1 = CInt((Me.ImagePanel.Width - Me.MarcoDibujoPanel.Width) + W)
                            H1 = CInt((Me.ImagePanel.Height - Me.MarcoDibujoPanel.Height) + H)

                            X1 = CInt(W1 * (Me.SelectedInputControl.CampoCaptura.Marca_X_Campo / 100))
                            Y1 = CInt(H1 * (Me.SelectedInputControl.CampoCaptura.Marca_Y_Campo / 100))

                            Me.MarcoDibujoPanel.AutoScrollPosition = New Point(X1, Y1)
                        End If
                    End If

                    If (Me.SelectedInputControl.CampoLlaveCaptura IsNot Nothing) Then
                        If (Me.SelectedInputControl.CampoLlaveCaptura.Usa_Marca) Then
                            Dim H, W As Integer

                            H = CInt(Me.ImagePictureBox.Image.Height * (Me.SelectedInputControl.CampoLlaveCaptura.Marca_Height_Campo / 100))
                            W = CInt(Me.ImagePictureBox.Image.Width * (Me.SelectedInputControl.CampoLlaveCaptura.Marca_Width_Campo / 100))

                            If ((H / Me.MarcoDibujoPanel.Height) >= (W / Me.MarcoDibujoPanel.Width)) Then
                                Me._Zoom = CShort(((Me.MarcoDibujoPanel.Height - 20) / H) * 75)
                            Else
                                Me._Zoom = CShort(((Me.MarcoDibujoPanel.Width - 20) / W) * 75)
                            End If

                            EnforceMaximumZoomForMarkFocus()

                            Me.ZoomToolStripComboBox.Text = Format(_Zoom, "0") & "%"

                            AplicarZoom()

                            H = CInt(Me.ImagePanel.Height * (Me.SelectedInputControl.CampoLlaveCaptura.Marca_Height_Campo / 100))
                            W = CInt(Me.ImagePanel.Width * (Me.SelectedInputControl.CampoLlaveCaptura.Marca_Width_Campo / 100))

                            Dim W1, H1, X1, Y1 As Integer
                            W1 = CInt((Me.ImagePanel.Width - Me.MarcoDibujoPanel.Width) + W)
                            H1 = CInt((Me.ImagePanel.Height - Me.MarcoDibujoPanel.Height) + H)

                            X1 = CInt(W1 * (Me.SelectedInputControl.CampoLlaveCaptura.Marca_X_Campo / 100))
                            Y1 = CInt(H1 * (Me.SelectedInputControl.CampoLlaveCaptura.Marca_Y_Campo / 100))

                            Me.MarcoDibujoPanel.AutoScrollPosition = New Point(X1, Y1)
                        End If
                    End If
                ElseIf (Me.SelectedValidationControl IsNot Nothing) Then
                    If (Me.SelectedValidationControl.ValidacionCaptura.Usa_Marca) Then
                        Dim H, W As Integer

                        H = CInt(ImagePictureBox.Image.Height * (Me.SelectedValidationControl.ValidacionCaptura.Marca_Height_Campo / 100))
                        W = CInt(ImagePictureBox.Image.Width * (Me.SelectedValidationControl.ValidacionCaptura.Marca_Width_Campo / 100))

                        If ((H / Me.MarcoDibujoPanel.Height) >= (W / Me.MarcoDibujoPanel.Width)) Then
                            _Zoom = CShort(((Me.MarcoDibujoPanel.Height - 20) / H) * 75)
                        Else
                            _Zoom = CShort(((Me.MarcoDibujoPanel.Width - 20) / W) * 75)
                        End If

                        EnforceMaximumZoomForMarkFocus()

                        Me.ZoomToolStripComboBox.Text = Format(_Zoom, "0") & "%"

                        AplicarZoom()

                        H = CInt(Me.ImagePanel.Height * (Me.SelectedValidationControl.ValidacionCaptura.Marca_Height_Campo / 100))
                        W = CInt(Me.ImagePanel.Width * (Me.SelectedValidationControl.ValidacionCaptura.Marca_Width_Campo / 100))

                        Dim W1, H1, X1, Y1 As Integer
                        W1 = CInt((Me.ImagePanel.Width - Me.MarcoDibujoPanel.Width) + W)
                        H1 = CInt((Me.ImagePanel.Height - Me.MarcoDibujoPanel.Height) + H)

                        X1 = CInt(W1 * (Me.SelectedValidationControl.ValidacionCaptura.Marca_X_Campo / 100))
                        Y1 = CInt(H1 * (Me.SelectedValidationControl.ValidacionCaptura.Marca_Y_Campo / 100))

                        MarcoDibujoPanel.AutoScrollPosition = New Point(X1, Y1)
                    End If
                End If
            Catch : End Try
        End Sub

        ''' <summary>
        ''' Limita el zoom al valor máximo permitido al enfocar una marca.
        ''' </summary>
        Private Sub EnforceMaximumZoomForMarkFocus()
            If Me._Zoom > Me._ZoomMAXEnfMarca Then
                Me._Zoom = Me._ZoomMAXEnfMarca
            End If
        End Sub


        Private Sub DrawMarca(ByRef nGraphics As Graphics)
            Try
                If (Me.SelectedInputControl IsNot Nothing) Then
                    If Me.SelectedInputControl.CampoCaptura IsNot Nothing Then
                        If Me.SelectedInputControl.CampoCaptura.Usa_Marca Then
                            Dim X, Y, W, H As Integer

                            X = CInt(ImagePictureBox.Width * (Me.SelectedInputControl.CampoCaptura.Marca_X_Campo / 100))
                            Y = CInt(ImagePictureBox.Height * (Me.SelectedInputControl.CampoCaptura.Marca_Y_Campo / 100))
                            W = CInt(ImagePictureBox.Width * (Me.SelectedInputControl.CampoCaptura.Marca_Width_Campo / 100))
                            H = CInt(ImagePictureBox.Height * (Me.SelectedInputControl.CampoCaptura.Marca_Height_Campo / 100))

                            nGraphics.FillRectangle(New SolidBrush(Color.FromArgb(30, 255, 128, 0)), X, Y, W, H)
                            nGraphics.DrawRectangle(Pens.Orange, X, Y, W, H)
                        End If
                    End If

                    If Me.SelectedInputControl.CampoLlaveCaptura IsNot Nothing Then
                        If Me.SelectedInputControl.CampoLlaveCaptura.Usa_Marca Then
                            Dim X, Y, W, H As Integer

                            X = CInt(ImagePictureBox.Width * (Me.SelectedInputControl.CampoLlaveCaptura.Marca_X_Campo / 100))
                            Y = CInt(ImagePictureBox.Height * (Me.SelectedInputControl.CampoLlaveCaptura.Marca_Y_Campo / 100))
                            W = CInt(ImagePictureBox.Width * (Me.SelectedInputControl.CampoLlaveCaptura.Marca_Width_Campo / 100))
                            H = CInt(ImagePictureBox.Height * (Me.SelectedInputControl.CampoLlaveCaptura.Marca_Height_Campo / 100))

                            nGraphics.FillRectangle(New SolidBrush(Color.FromArgb(30, 255, 128, 0)), X, Y, W, H)
                            nGraphics.DrawRectangle(Pens.Orange, X, Y, W, H)
                        End If
                    End If
                ElseIf (Me.SelectedValidationControl IsNot Nothing) Then
                    If Me.SelectedValidationControl.ValidacionCaptura.Usa_Marca Then
                        Dim X, Y, W, H As Integer

                        X = CInt(ImagePictureBox.Width * (Me.SelectedValidationControl.ValidacionCaptura.Marca_X_Campo / 100))
                        Y = CInt(ImagePictureBox.Height * (Me.SelectedValidationControl.ValidacionCaptura.Marca_Y_Campo / 100))
                        W = CInt(ImagePictureBox.Width * (Me.SelectedValidationControl.ValidacionCaptura.Marca_Width_Campo / 100))
                        H = CInt(ImagePictureBox.Height * (Me.SelectedValidationControl.ValidacionCaptura.Marca_Height_Campo / 100))

                        nGraphics.FillRectangle(New SolidBrush(Color.FromArgb(30, 255, 128, 0)), X, Y, W, H)
                        nGraphics.DrawRectangle(Pens.Orange, X, Y, W, H)
                    End If
                End If
            Catch : End Try
        End Sub

        Private Sub FiltrarTiposDocumentales(ByVal nForzar As Boolean)
            If ((EsquemaComboBox.Enabled Or nForzar) And TipoDocumental_DataSource IsNot Nothing) Then
                TipoDocumental_DataSource.RowFilter = "fk_Esquema = " & Esquema_Value
            End If
        End Sub

        Private Sub AnexoFolder()
            If (AnexoFolderToolStripButton.Visible) Then
                Me.AnexoFolderToolStripButton.Checked = Not Me.AnexoFolderToolStripButton.Checked

                For Each FolderItem In Me.Controller.Folders
                    FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal
                Next

                If (Me.AnexoFolderToolStripButton.Checked) Then
                    Me.Controller.CurrentFolder.Modo = Generic.Folder.FolderModoEnum.Anexo
                End If

                Me.AnexoTotalToolStripButton.Checked = False
                Me.BackThumbnailPanel.BackColor = SystemColors.ControlDarkDark

                ActivarControles(True)
            End If
        End Sub

        Private Sub AnexoTotal()
            If (AnexoTotalToolStripButton.Visible) Then
                Me.AnexoTotalToolStripButton.Checked = Not Me.AnexoTotalToolStripButton.Checked

                If (Me.AnexoTotalToolStripButton.Checked) Then
                    Me.BackThumbnailPanel.BackColor = Color.Purple

                    For Each FolderItem In Me.Controller.Folders
                        FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal
                    Next
                Else
                    Me.BackThumbnailPanel.BackColor = SystemColors.ControlDarkDark
                End If

                ActivarControles(True)
            End If
        End Sub

        Private Sub AutoIndexar()
            If (AutoIndexarToolStripButton.Visible And AutoIndexarToolStripButton.Enabled) Then
                If (Me.Controller.Folios < Me.Controller.ImageCount) Then
                    Dim AutoIndexarForm As New FormAutoIndexar()

                    AutoIndexarForm.FolderRadioButton.Enabled = NewFolderButton.Enabled
                    AutoIndexarForm.DocumentoRadioButton.Enabled = NewFileButton.Enabled

                    If (NewFolderButton.Enabled) Then
                        AutoIndexarForm.FolderRadioButton.Checked = True
                    Else
                        AutoIndexarForm.DocumentoRadioButton.Checked = True
                    End If

                    AutoIndexarForm.MaxFolios = Me.Controller.ImageCount - Me.Controller.Folios + 1
                    AutoIndexarForm.Esquema = Me.EsquemaComboBox.Text
                    AutoIndexarForm.Documento = Me.TipoDocumentalComboBox.Text

                    Dim Result = AutoIndexarForm.ShowDialog()

                    If (Result = DialogResult.OK) Then
                        Me.Enabled = False
                        Me.Cursor = Cursors.WaitCursor

                        Try
                            Me._CancelProcess = False
                            Me.IndexerController.AutoIndexar(CInt(Me.TipoDocumentalComboBox.SelectedValue), AutoIndexarForm.Folios, AutoIndexarForm.Modo)

                            MessageBox.Show("El proceso se realizó exitosamente", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            Me.Cursor = Cursors.Default
                            Me.Enabled = True
                        End Try
                    End If

                    ActivarControles(True)
                Else
                    MessageBox.Show("No quedan folios para indexar", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        End Sub

        Private Sub ShowRuler()
            If (Me.RulerToolStripButton.Visible And Me.RulerToolStripButton.Enabled) Then
                Me.RulerToolStripButton.Checked = Not Me.RulerToolStripButton.Checked
                Me.SeñaladorRulerControl.Visible = Me.RulerToolStripButton.Checked

                Me.ImagePictureBox.Refresh()
                ActivarControles(True)
            End If
        End Sub

        Private Sub DrawLines(ByVal nGraphics As Graphics)
            If (Me.RulerToolStripButton.Checked) Then
                ' Dibujar las líneas resaltadas
                Dim Margen = CInt(Me.ImagePictureBox.Width / 20)
                Dim X1 As Integer, X2 As Integer
                Dim ResaltadorBrush = New SolidBrush(Color.FromArgb(150, Color.YellowGreen))
                Dim ResaltadorPen = New Pen(ResaltadorBrush, 15)
                Dim PositionY As Integer

                X1 = Margen
                X2 = Me.ImagePictureBox.Width - (Margen * 2)

                For Each Linea In Me.Controller.CurrentFolio.Lineas
                    PositionY = CInt(Me.ImagePictureBox.Height * Linea)

                    nGraphics.DrawLine(ResaltadorPen, X1, PositionY, X2, PositionY)
                Next

                ' Dibujar sombra de la regla
                Dim ShadowBrush = New SolidBrush(Color.FromArgb(200, Color.Black))
                Dim ShadowPen = New Pen(ShadowBrush, 5)
                PositionY = Me.SeñaladorRulerControl.Location.Y + Me.SeñaladorRulerControl.Size.Height - 10
                nGraphics.DrawLine(ShadowPen, 4, PositionY, Me.ImagePictureBox.Size.Width - 4, PositionY)

            End If
        End Sub

        Private Sub Subrayar()
            If (Me.SubrayarToolStripButton.Visible And Me.SubrayarToolStripButton.Enabled) Then
                Dim PositionY As Integer = Me.SeñaladorRulerControl.Location.Y - 20
                Dim PercentY = PositionY / Me.ImagePictureBox.Height

                Me.Controller.CurrentFolio.Lineas.Add(CSng(PercentY))

                Me.ImagePictureBox.Refresh()
                ActivarControles(True)
            End If
        End Sub

        Private Sub QuitarSubrayado()
            If (Me.QuitarSubrayadoToolStripButton.Visible And Me.QuitarSubrayadoToolStripButton.Enabled) Then
                Me.Controller.CurrentFolio.Lineas.Clear()
                Me.ImagePictureBox.Refresh()

                ActivarControles(True)
            End If
        End Sub

        Private Sub VisualizarAnexos()
            Me.Enabled = False
            Me.Cursor = Cursors.WaitCursor

            Try
                ' Eiminar los TabPages de anexos
                For Each Anexo In Me.AnexosList
                    If (Anexo.TabPage IsNot Nothing) Then
                        Me.DocumentsTabControl.TabPages.Remove(Anexo.TabPage)
                    End If
                Next

                ' Leer el listado de documentos
                Dim Contador As Integer = 0
                For Each idAnexo As Integer In Me.DocumentsCheckedListBox.CheckedIndices
                    Dim Anexo = Me.AnexosList(idAnexo)

                    ' Si no se ha cargado el anexo, se solicita al controller
                    If (Anexo.TabPage Is Nothing) Then
                        Dim Filename = Me.IndexerController.LoadAnexo(Anexo.idAnexo)

                        Anexo.TabPage = New TabPage()
                        Anexo.ImageViewer = New ImageViewer()

                        Anexo.ImageViewer.Dock = DockStyle.Fill
                        Anexo.ImageViewer.ImagePath = Filename
                        Anexo.ImageViewer.MejorAjuste()
                        Anexo.TabPage.Controls.Add(Anexo.ImageViewer)
                    End If

                    Contador += 1

                    If (Contador < 10) Then Anexo.TabPage.Text = Anexo.NombreAnexo & " - [" & Contador & "]"

                    Me.DocumentsTabControl.TabPages.Add(Anexo.TabPage)
                    Application.DoEvents()
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Me.Enabled = True
                Me.Cursor = Cursors.Default
            End Try
        End Sub

        Private Sub SeleccionarAnexo(ByVal nPestaña As Integer, ByVal nForzar As Boolean)
            If (Me.DocumentsTabControl.TabPages.Count > 1) Then
                If (nForzar) Then
                    For i = 0 To nPestaña - 1
                        If (Not Me.DocumentsCheckedListBox.GetItemCheckState(i) = CheckState.Checked) Then
                            Me.DocumentsCheckedListBox.SetItemCheckState(i, CheckState.Checked)
                        End If
                    Next

                    VisualizarAnexos()
                End If

                If (Me.DocumentsTabControl.TabPages.Count > nPestaña + 1) Then Me.DocumentsTabControl.SelectedIndex = nPestaña + 1
            End If
        End Sub

#End Region

#Region " Funciones "

        ''' <summary>
        ''' Obtiene la imagen contenida en el PictureBox.
        ''' </summary>
        ''' <returns>La imagen contenida en el PictureBox.</returns>
        Private Function GetImageFromPictureBox() As Image
            Return ImagePictureBox.Image
        End Function

        ''' <summary>
        ''' Calcula la distancia de edición entre dos cadenas utilizando el algoritmo de Levenshtein.
        ''' La distancia de edición representa el número mínimo de operaciones requeridas para convertir una cadena en otra.
        ''' </summary>
        ''' <param name="str1">Primera cadena a comparar.</param>
        ''' <param name="str2">Segunda cadena a comparar.</param>
        ''' <returns>La distancia de edición entre las dos cadenas.</returns>
        Private Function CalculateLevenshteinDistance(ByVal str1 As String, ByVal str2 As String) As Integer
            Dim lenStr1 As Integer = str1.Length
            Dim lenStr2 As Integer = str2.Length

            ' Inicializa una matriz para almacenar las distancias de edición
            Dim distance(lenStr1, lenStr2) As Integer

            ' Inicializa la primera fila y columna de la matriz
            For i As Integer = 0 To lenStr1
                distance(i, 0) = i
            Next

            For j As Integer = 0 To lenStr2
                distance(0, j) = j
            Next

            ' Calcula las distancias de edición utilizando el algoritmo de Levenshtein
            For i As Integer = 1 To lenStr1
                For j As Integer = 1 To lenStr2
                    Dim cost As Integer = If(str1(i - 1) = str2(j - 1), 0, 1)

                    distance(i, j) = Math.Min(Math.Min(distance(i - 1, j) + 1, distance(i, j - 1) + 1), distance(i - 1, j - 1) + cost)
                Next
            Next

            ' Devuelve la distancia de edición entre las dos cadenas
            Return distance(lenStr1, lenStr2)
        End Function

        ''' <summary>
        ''' Verifica si la captura OCR está en uso.
        ''' </summary>
        ''' <returns>True si la captura OCR está en uso, False en caso contrario.</returns>
        Private Function IsOCRCaptureUsed() As Boolean
            ' Verifica si el Controller no es nulo y si IsOCRUsed es True
            Return Me.Controller IsNot Nothing AndAlso Me.Controller.IsOCRUsed
        End Function

        ''' <summary>
        ''' Calcula las coordenadas de las esquinas de las celdas en una tabla delimitada por líneas horizontales y verticales.
        ''' </summary>
        ''' <param name="sortAllLinesInBorderedTable">La tabla de líneas ordenada que define los límites de la tabla.</param>
        ''' <returns>Un diccionario que mapea identificadores de columna a listas de puntos que representan las coordenadas de las celdas.</returns>
        ''' <remarks>Este método calcula las coordenadas de las esquinas de las celdas en una tabla con límites definidos por líneas horizontales y verticales.</remarks>
        Private Function CalculateCellCoordinates(sortAllLinesInBorderedTable As Dictionary(Of LineOrientation, List(Of Point()))) As Dictionary(Of Integer, List(Of Point()))

            ' Diccionario para almacenar las coordenadas de las casillas por columna identificada
            Dim coordinatesByColumn As New Dictionary(Of Integer, List(Of Point()))
            Dim horizontalLines = sortAllLinesInBorderedTable(LineOrientation.Horizontal)
            Dim verticalLines = sortAllLinesInBorderedTable(LineOrientation.Vertical)

            Dim diccionarioRectangulos As New Dictionary(Of Integer, List(Of Rectangle))

            ' Calcular las coordenadas de las esquinas de las casillas
            For iLineVertical = 0 To verticalLines.Count - 2

                Dim pointLineVertical As Point = verticalLines(iLineVertical)(0)
                Dim pointLineNextVertical As Point = verticalLines(iLineVertical + 1)(1)

                Dim listaRectangulos As New List(Of Rectangle)

                Dim columnIdentified As Integer = iLineVertical ' Identificador de columna
                coordinatesByColumn(columnIdentified) = New List(Of Point())

                'For Each yLineaHorizontal In Me.horizontalLinesList
                For i = 0 To horizontalLines.Count - 2

                    Dim pointLineHorizontal As Point = horizontalLines(i)(0)
                    Dim pointLineNextHorizontal As Point = horizontalLines(i + 1)(1)

                    Dim x1 As Integer = pointLineVertical.X
                    Dim y1 As Integer = pointLineHorizontal.Y
                    Dim x2 As Integer = pointLineNextVertical.X
                    Dim y2 As Integer = pointLineNextHorizontal.Y

                    listaRectangulos.Add(New Rectangle(x1, y1, x2 - x1, y2 - y1))

                    Dim cellRectPoints As Point() = {New Point(x1, y1), New Point(x2, y2)}
                    coordinatesByColumn(columnIdentified).Add(cellRectPoints) ' Almacenar las coordenadas en el diccionario, en la columna identificada

                Next

                diccionarioRectangulos.Add(iLineVertical, listaRectangulos)
            Next

            Return coordinatesByColumn
        End Function

        ''' <summary>
        ''' Calcula las coordenadas de los rectángulos que representan las celdas de una tabla delimitada por líneas horizontales y verticales.
        ''' </summary>
        ''' <param name="sortAllLinesInBorderedTable">Diccionario que contiene listas de puntos que representan las líneas horizontales y verticales que delimitan la tabla.</param>
        ''' <returns>Un diccionario que asigna índices de línea vertical a listas de rectángulos que representan las celdas de la tabla.</returns>
        Private Function CalculateCellCoordinatesRectangle(sortAllLinesInBorderedTable As Dictionary(Of LineOrientation, List(Of Point()))) As Dictionary(Of Integer, List(Of Rectangle))

            Dim horizontalLines = sortAllLinesInBorderedTable(LineOrientation.Horizontal)
            Dim verticalLines = sortAllLinesInBorderedTable(LineOrientation.Vertical)

            Dim diccionarioRectangulos As New Dictionary(Of Integer, List(Of Rectangle))

            ' Calcular las coordenadas de las esquinas de las casillas
            For iLineVertical = 0 To verticalLines.Count - 2

                Dim pointLineVertical As Point = verticalLines(iLineVertical)(0)
                Dim pointLineNextVertical As Point = verticalLines(iLineVertical + 1)(1)

                Dim listaRectangulos As New List(Of Rectangle)

                'For Each yLineaHorizontal In Me.horizontalLinesList
                For i = 0 To horizontalLines.Count - 2

                    Dim pointLineHorizontal As Point = horizontalLines(i)(0)
                    Dim pointLineNextHorizontal As Point = horizontalLines(i + 1)(1)

                    Dim x1 As Integer = pointLineVertical.X
                    Dim y1 As Integer = pointLineHorizontal.Y
                    Dim width As Integer = pointLineNextVertical.X - pointLineVertical.X
                    Dim heigth As Integer = pointLineNextHorizontal.Y - pointLineHorizontal.Y

                    listaRectangulos.Add(New Rectangle(x1, y1, width, heigth))

                Next

                diccionarioRectangulos.Add(iLineVertical, listaRectangulos)
            Next

            Return diccionarioRectangulos
        End Function


        ''' <summary>
        ''' Crea líneas horizontales y verticales que forman un rectángulo del borde de la tabla y las agrega a una tabla de líneas existente.
        ''' </summary>
        ''' <param name="rectangle">El rectángulo del que se crearán las líneas.</param>
        ''' <param name="allLinesTable">La tabla de líneas a la que se agregarán las nuevas líneas.</param>
        ''' <returns>La tabla de líneas con las líneas del rectángulo agregadas.</returns>
        ''' <remarks>Este método crea líneas horizontales y verticales que forman el contorno de un rectángulo y las agrega a la tabla existente.</remarks>
        Private Function CreateLinesRectangleTable(rectangle As Rectangle, allLinesTable As Dictionary(Of LineOrientation, List(Of Point()))) As Dictionary(Of LineOrientation, List(Of Point()))

            ' Crear líneas horizontales
            Dim lineaHorizontal1 As Point() = {New Point(rectangle.X, rectangle.Y), New Point(rectangle.X + rectangle.Width, rectangle.Y)}
            Dim lineaHorizontal2 As Point() = {New Point(rectangle.X, rectangle.Y + rectangle.Height), New Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height)}

            ' Crear las líneas verticales
            Dim lineaVertical1 As Point() = {New Point(rectangle.X, rectangle.Y), New Point(rectangle.X, rectangle.Y + rectangle.Height)}
            Dim lineaVertical2 As Point() = {New Point(rectangle.X + rectangle.Width, rectangle.Y), New Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height)}

            ' Agregar las líneas a las listas
            allLinesTable(LineOrientation.Horizontal).Add(lineaHorizontal1)
            allLinesTable(LineOrientation.Horizontal).Add(lineaHorizontal2)
            allLinesTable(LineOrientation.Vertical).Add(lineaVertical1)
            allLinesTable(LineOrientation.Vertical).Add(lineaVertical2)

            Return allLinesTable
        End Function

        ''' <summary>
        ''' Obtiene un nuevo rectángulo a partir de otro rectángulo dado.
        ''' </summary>
        ''' <param name="rectangle">El rectángulo original del que se obtendrán las coordenadas y dimensiones.</param>
        ''' <returns>Un nuevo rectángulo con las mismas coordenadas y dimensiones que el rectángulo original.</returns>
        ''' <remarks>Este método se utiliza para crear una copia del rectángulo original.</remarks>
        Private Function GetRectangle(rectangle As Rectangle) As Rectangle

            'Obtener las coordenadas y dimensiones del rectángulo original
            Dim x1 As Integer = rectangle.Left
            Dim y1 As Integer = rectangle.Top
            Dim width As Integer = rectangle.Width
            Dim height As Integer = rectangle.Height

            ' Crear un nuevo rectángulo con las mismas coordenadas y dimensiones
            Dim newRectangle As New Rectangle(x1, y1, width, height)
            Return newRectangle
        End Function

        ''' <summary>
        ''' Ordena las líneas horizontales en orden ascendente según sus coordenadas 'Y' 
        ''' y las líneas verticales en orden ascendente según sus coordenadas 'X'.
        ''' </summary>
        ''' <param name="allLinesTable">La tabla de todas las líneas a ordenar.</param>
        ''' <returns>La tabla de líneas ordenadas.</returns>
        ''' <remarks>Este método ordena las líneas en su lugar en la tabla proporcionada.</remarks>
        Private Function SortHorizontalAndVerticalLines(allLinesTable As Dictionary(Of LineOrientation, List(Of Point()))) As Dictionary(Of LineOrientation, List(Of Point()))

            allLinesTable(LineOrientation.Horizontal).Sort(Function(line1, line2) line1(0).Y.CompareTo(line2(0).Y)) ' Organizar las líneas horizontales en orden ascendente según sus coordenadas y
            allLinesTable(LineOrientation.Vertical).Sort(Function(line1, line2) line1(0).X.CompareTo(line2(0).X)) ' Organizar las líneas verticales en orden ascendente según sus coordenadas x

            Return allLinesTable
        End Function

        ''' <summary>
        ''' Calcula el rectángulo en las coordenadas originales de la imagen, ajustado por los niveles de escala.
        ''' </summary>
        ''' <param name="scaleLevelsImage">Arreglo de dos elementos representando los niveles de escala en X e Y respectivamente.</param>
        ''' <returns>Un rectángulo ajustado por la escala en las coordenadas originales de la imagen.</returns>
        Private Function CalculateRectangleImageOriginal(_rectangleStartPoint As Point, _rectangleEndPoint As Point, scaleLevelsImage As Double()) As Rectangle

            Dim originalImageRect As New Rectangle()

            ' Calcula coordenadas y dimesiones del rectangulo ajustadas por el zoom en La imagen Original
            originalImageRect.X = CInt(Math.Round(_rectangleStartPoint.X * scaleLevelsImage(0)))
            originalImageRect.Y = CInt(Math.Round(_rectangleStartPoint.Y * scaleLevelsImage(1)))

            Dim xEndPoint As Integer = CInt(Math.Round(_rectangleEndPoint.X * scaleLevelsImage(0)))
            Dim yEndPoint As Integer = CInt(Math.Round(_rectangleEndPoint.Y * scaleLevelsImage(1)))

            originalImageRect.Width = CInt(Math.Abs(originalImageRect.X - xEndPoint))
            originalImageRect.Height = CInt(Math.Abs(originalImageRect.Y - yEndPoint))

            Return originalImageRect
        End Function

        ''' <summary>
        ''' Calcula la relación de escala en los ejes X y Y basada en el tamaño de la imagen y el PictureBox.
        ''' </summary>
        ''' <returns>Un arreglo de dos elementos Double que representa la relación de escala en X y Y, respectivamente.</returns>
        Private Function CalculateScaleImageXY() As Double()
            Dim scaleLevelImage(1) As Double
            scaleLevelImage(0) = CDbl(Me.ImagePictureBox.Image.Width) / CDbl(Me.ImagePictureBox.Width)      ' Calcular la relación de escala en el eje X
            scaleLevelImage(1) = CDbl(Me.ImagePictureBox.Image.Height) / CDbl(Me.ImagePictureBox.Height)     ' Calcular la relación de escala en el eje Y

            Return scaleLevelImage
        End Function

        ''' <summary>
        ''' Calcula y devuelve las coordenadas y dimensiones de un rectángulo zoomedRect basado en el rectángulo de dibujo _drawRectangle, o los puntos de inicio y fin _rectangleStartPoint y _rectangleEndPoint, teniendo en cuenta el nivel de zoom especificado _zoom.
        ''' </summary>
        ''' <param name="_drawRectangle">El rectángulo de dibujo actual.</param>
        ''' <param name="_rectangleStartPoint">El punto de inicio del rectángulo.</param>
        ''' <param name="_rectangleEndPoint">El punto final del rectángulo.</param>
        ''' <param name="_zoom">El nivel de zoom de la imagen.</param>
        ''' <returns>Un objeto Rectangle que representa el rectángulo calculado teniendo en cuenta el nivel de zoom.</returns>
        Private Function CalculateZoomedRectanglCoordinatesAndSize(_drawRectangle As Rectangle, _rectangleStartPoint As Point, _rectangleEndPoint As Point, _zoom As Integer) As Rectangle

            Dim zoomedRect As Rectangle                                                    ' Rectangle con coordenadas y dimensiones del rectangulo
            Dim ZoomCurrentRectangle As Single = CSng((_zoom / 100))                        ' Calcula el Zoom en el cual se encuentra la imagen

            If _drawRectangle <> Rectangle.Empty Then
                ' Verifica si se tiene un rectángulo dibujado
                zoomedRect.X = CInt(_drawRectangle.X * ZoomCurrentRectangle)                ' Coordenada X 
                zoomedRect.Y = CInt(_drawRectangle.Y * ZoomCurrentRectangle)                ' Coordenada Y
                zoomedRect.Width = CInt(_drawRectangle.Width * ZoomCurrentRectangle)        ' Ancho
                zoomedRect.Height = CInt(_drawRectangle.Height * ZoomCurrentRectangle)      ' Alto

            ElseIf _rectangleStartPoint <> Nothing AndAlso _rectangleEndPoint <> Nothing Then
                ' Se tiene un punto de inicio y un punto final
                zoomedRect.X = Math.Min(_rectangleStartPoint.X, _rectangleEndPoint.X)       ' Coordenada X 
                zoomedRect.Y = Math.Min(_rectangleStartPoint.Y, _rectangleEndPoint.Y)       ' Coordenada Y
                zoomedRect.Width = Math.Abs(_rectangleStartPoint.X - _rectangleEndPoint.X)  ' Ancho
                zoomedRect.Height = Math.Abs(_rectangleStartPoint.Y - _rectangleEndPoint.Y) ' Alto

            Else
                ' No se tiene un rectángulo ni puntos definidos
                zoomedRect.X = 0      ' Coordenada X 
                zoomedRect.Y = 0      ' Coordenada Y
                zoomedRect.Width = 0  ' Ancho
                zoomedRect.Height = 0 ' Alto
            End If

            Return zoomedRect

        End Function

#End Region

        Private Function ScaleRectangleToImageBounds(boundingRect As Rectangle, imagePicture As Drawing.Image) As Rectangle
            Throw New NotImplementedException
        End Function

        Private Sub Table_UpPanel_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Table_UpPanel.Paint
            PageStatusLabel.Text = _SelectedInputControl.Etiqueta
        End Sub
    End Class

End Namespace