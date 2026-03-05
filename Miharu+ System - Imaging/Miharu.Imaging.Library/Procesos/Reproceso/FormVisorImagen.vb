Imports System.IO
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools.Imaging
Imports Miharu.Desktop.Library
Imports Miharu.FileProvider.Library

Namespace Procesos.Reproceso

    Public Class FormVisorImagen
        Inherits FormBase

#Region " Declaraciones "

        Private _Expediente As Long
        Private _Folder As Short
        Private _File As Short

        Private _tempPath As String

        Private _ListImages As List(Of String)

#End Region

#Region " Contructor "

        Sub New(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short)

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.

            _Expediente = nExpediente
            _Folder = nFolder
            _File = nFile

            _tempPath = Program.AppPath & Program.TempPath
            If (Not Directory.Exists(_tempPath)) Then
                Directory.CreateDirectory(_tempPath)
            End If
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormVisorImagen_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            EliminaElementosTemp()
            CargarImagen()
        End Sub

        Private Sub FormVisorImagen_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        End Sub

        Private Sub FormVisorImagen_FormClosed(ByVal sender As System.Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
            EliminaElementosTemp()
        End Sub
#End Region

#Region " Metodos "

        Private Sub CargarImagen()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                manager = New FileProviderManager(_Expediente, _Folder, dbmImaging, Program.Sesion.Usuario.id)
                manager.Connect()

                'Obtiene el File a visualizar.
                Dim LastVersion = manager.GetLastVersion(_Expediente, _Folder, _File)
                Dim Folios = manager.GetFolios(_Expediente, _Folder, _File, LastVersion)

                If (Folios > 0) Then
                    Dim Formato = Utilities.getEnumFormat(Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                    Dim Compresion = Utilities.getEnumCompression(CType(Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                    _ListImages = New List(Of String)()

                    For folio = CShort(1) To Folios
                        Dim Imagen As Byte() = Nothing
                        Dim Thumbnail As Byte() = Nothing
                        manager.GetFolio(_Expediente, _Folder, _File, LastVersion, folio, Imagen, Thumbnail)

                        Using ms = New MemoryStream(Imagen)
                            Using bm = New FreeImageAPI.FreeImageBitmap(ms)
                                ImageManager.Save(bm, _tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, "", Formato, Compresion, False, _tempPath)
                            End Using
                        End Using

                        _ListImages.Add(_tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                    Next

                    ReprocesoImageViewer.ImagePath = _ListImages


                    ''Si el documento esta compuesto por más de una imagen, se crea un TIFF para pasarlo al visor.
                    'If (_ListImages.Count > 1) Then
                    '    ImageManager.Save(_ListImages, _tempPath & "ImageTIFF.TIF", "", ImageManager.EnumFormat.TIFF, ImageManager.EnumCompression.LZW, False, _tempPath, True)

                    '    ' Dim arrayImage As New ArrayList
                    '    Dim ThumbnailWhith, ThumbnailHeight As Integer
                    '    Dim ThumbnailPictureBox As PictureBox
                    '    Dim Pagina As Integer = 0

                    '    For Each folio In _ListImages
                    '        Using SingleBitmap = New Bitmap(folio)

                    '            ThumbnailWhith = 60
                    '            ThumbnailHeight = CInt((ThumbnailWhith / SingleBitmap.Width) * SingleBitmap.Height)

                    '            ThumbnailPictureBox = New PictureBox
                    '            ThumbnailPictureBox.Tag = (Pagina + 1)
                    '            ThumbnailPictureBox.BorderStyle = BorderStyle.FixedSingle
                    '            ThumbnailPictureBox.Cursor = Cursors.Hand
                    '            ThumbnailPictureBox.SizeMode = PictureBoxSizeMode.AutoSize
                    '            ThumbnailPictureBox.Image = SingleBitmap.GetThumbnailImage(ThumbnailWhith, ThumbnailHeight, AddressOf ThumbnailCallback, IntPtr.Zero)

                    '            Pagina += 1
                    '            arrayImage.Add(ThumbnailPictureBox)
                    '        End Using
                    '    Next

                    '    ReprocesoImageViewer.ImagePath = _tempPath & "ImageTIFF.TIF"

                    'ElseIf (_ListImages.Count = 1) Then
                    '    ImageManager.Save(_ListImages, _tempPath & "ImagenPreview" & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, "", Formato, Compresion, False, _tempPath, True)

                    '    ReprocesoImageViewer.ImagePath = _tempPath & "ImagenPreview1" & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    'End If

                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No existen folios en la imágen.", "No existen folios", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarImagen", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

        Private Sub EliminaElementosTemp()
            Try
                ReprocesoImageViewer.ImagePath = Nothing

                For Each fileImagen In Directory.GetFiles(_tempPath)
                    File.Delete(fileImagen)
                Next
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("EliminaElementosTemp", ex)
            End Try
        End Sub

#End Region

#Region " Funciones "

        Public Function ThumbnailCallback() As Boolean
            Return False
        End Function

#End Region

    End Class

End Namespace