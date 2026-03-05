Imports System.IO
Imports Miharu.FileProvider.Library
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Slyg.Tools.Imaging
Imports Miharu.Imaging.OffLineViewer.Library.Visor
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Carpeta_Unica.Comprimir_Imagenes
    Public Class FormComprimirImagen
        Friend WithEvents CentralOffLineViewer As ImageViewer
        Private FileDataTable As New DBCore.SchemaImaging.CTA_Busqueda_FilesDataTable
        Private _Plugin As CarpetaUnicaPlugin

#Region " Contructores "

        Public Sub New(ByVal nCarpetaUnicaDesktopPlugin As CarpetaUnicaPlugin)
            InitializeComponent()
            _Plugin = nCarpetaUnicaDesktopPlugin
        End Sub

#End Region

        Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

            Try
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim Expediente = CLng(TextBox1.Text)
                Dim Folder As Integer = 1
                Dim File As Integer = 1

                dbmCore.SchemaImaging.CTA_Busqueda_Files.DBFillByfk_Expedientefk_Folder(FileDataTable, Expediente, Folder)

                EscribirImagen(Expediente, Folder, File)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                If (dbmCore IsNot Nothing) Then
                    dbmCore.Connection_Close()
                End If
            End Try

        End Sub

        Private Sub EscribirImagen(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short)
            Dim tempPath = Program.AppPath & Program.TempPath

            If (Not Directory.Exists(tempPath)) Then
                Directory.CreateDirectory(tempPath)
            Else
                Try
                    'CentralOffLineViewer.ImagePath = Nothing

                    For Each fileImagen In Directory.GetFiles(tempPath)
                        File.Delete(fileImagen)
                    Next
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("EliminaElementosTemp", ex)
                End Try
            End If

            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)


            Try

                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim manager As New FileProviderManager(nExpediente, nFolder, dbmImaging, _Plugin.Manager.Sesion.Usuario.id)
                Try

                    manager.Connect()

                    Dim FilesDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(nExpediente, nFolder, nFile, Nothing, 1, New DBCore.SchemaImaging.TBL_FileEnumList(DBCore.SchemaImaging.TBL_FileEnum.id_Version, False))
                    If (FileDataTable.Count = 0) Then Throw New Exception("File no encontrado")
                    Dim FileRow = FilesDataTable(0)

                    Dim ListImages = New List(Of String)
                    Const Formato As ImageManager.EnumFormat = ImageManager.EnumFormat.Tiff
                    'Const Compresion As ImageManager.EnumCompression = ImageManager.EnumCompression.Lzw

                    Dim Compresion As ImageManager.EnumCompression
                    If (_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                        Compresion = ImageManager.EnumCompression.Ccitt4
                    Else
                        Compresion = ImageManager.EnumCompression.Lzw
                    End If



                    'Obtiene el File a visualizar.
                    If (FileRow.Es_Anexo) Then
                        Dim folios = manager.GetFolios(FileRow.fk_Anexo)
                        If (folios > 0) Then
                            For folio = CShort(1) To folios
                                Dim Imagen As Byte() = Nothing
                                Dim Thumbnail As Byte() = Nothing
                                manager.GetFolio(FilesDataTable(0).fk_Anexo, folio, Imagen, Thumbnail)
                                ImageManager.Save(New FreeImageAPI.FreeImageBitmap(New MemoryStream(Imagen)), tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, "", Formato, Compresion, False, tempPath)
                                ListImages.Add(tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                            Next
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("No existe una imagen asociada para visualizar.", "Imágen no encontrada", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        End If
                    Else
                        'Se obtienen los folios del file.
                        Dim folios = manager.GetFolios(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version)
                        If (folios > 0) Then
                            For folio = CShort(1) To folios
                                Dim Imagen As Byte() = Nothing
                                Dim Thumbnail As Byte() = Nothing
                                manager.GetFolio(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, folio, Imagen, Thumbnail)

                                Using ms = New MemoryStream(Imagen)
                                    Using bm = New FreeImageAPI.FreeImageBitmap(ms)
                                        ImageManager.Save(bm, tempPath & folio.ToString() & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, "", Formato, Compresion, False, tempPath)
                                    End Using
                                End Using

                                ListImages.Add(tempPath & folio.ToString() & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                            Next
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("No existe una imagen asociada para visualizar.", "Imágen no encontrada", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        End If
                    End If

                    'CentralOffLineViewer.ImagePath = ListImages
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("CargarImagen", ex)
                Finally
                    If (manager IsNot Nothing) Then manager.Disconnect()
                End Try

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarImagen", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()

            End Try
        End Sub
    End Class
End Namespace