Imports System.IO
Imports System.Linq
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.MiharuDMZ
Imports Slyg.Tools.Imaging

Public Class ImagingFolioCreator

    Public Class FileDataResult
        Public Property fk_Expediente As Long
        Public Property fk_Folder As Short
        Public Property fk_File As Short
    End Class

#Region " Declaraciones "

    Private _NumeroMedioMagnetico As String
    Private _CodigoEntidad As String
    Private TempFilesPath As String

    Private MaxThumbnailWidth As Integer = 60
    Private MaxThumbnailHeight As Integer = 80

#End Region

#Region " Constructores "

    Public Sub New(ByVal codigoEntidad As String, ByVal numeroMedioMagnetico As String)
        _NumeroMedioMagnetico = numeroMedioMagnetico
        _CodigoEntidad = codigoEntidad
        TempFilesPath = Program.AppPath & Program.TempPath
    End Sub

#End Region


#Region " Metodos "
    ' to Do : optimizar para no hacer multiples llamados a base de datos cuando se createOnePerPage este en true
    Public Function ProcessPdfExpedientStorage(fileNamePDF As String, fkExpedienteAsociado As Integer, fkFolder As Integer, fkFile As Integer, fk_Documento As Integer, createOnePerPage As Boolean, Optional ByVal folioPage As Integer = 0) As Integer

        Dim fk_Expediente = fkExpedienteAsociado
        Dim fk_Folder = fkFolder
        Dim fileImage = 0

        Dim totalFoliosImage As Short

        If fkFile = 0 Then
            Dim queryResponseFile As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Process].[TBL_File]", New List(Of QueryParameter) From {
                                    New QueryParameter With {.name = "fk_Expediente", .value = fk_Expediente.ToString()},
                                    New QueryParameter With {.name = "fk_Folder", .value = fk_Folder.ToString()}
                                    }, QueryRequestType.Table, QueryResponseType.Table)

            Dim dtFile = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaProcess.TBL_FileDataTable(), queryResponseFile.dataTable), DBCore.SchemaProcess.TBL_FileDataTable)

            If dtFile Is Nothing OrElse dtFile.Count = 0 Then
                Throw New InvalidOperationException($"No fue posible encontrar los registros en dbmCore.SchemaProcess.TBL_File para el exp:{fk_Expediente} folder:{fk_Folder} para obtener el file máximo.")
            End If

            ' Obtener el valor máximo de id_File
            Dim maxIdFile As Short = dtFile.Max(Function(file) file.id_File)
            fileImage = CType(maxIdFile + 1, Short)
        Else
            fileImage = fkFile
        End If

        Dim formato = Utilities.GetEnumFormat(Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
        Dim compresion = Utilities.GetEnumCompression(CType(Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

        Dim flags As FreeImageAPI.FREE_IMAGE_SAVE_FLAGS = Utilities.GetEnumDefaultFlags(formato)

        If createOnePerPage Then
            totalFoliosImage = 1

            Dim dataImage = ImageManager.GetUnresizedFolioDataPdfOnly(fileNamePDF, folioPage, formato, compresion, flags)

            Dim dataImageThumbnail As Byte() = Nothing

            Using dataImageStream As Stream = New MemoryStream()
                dataImageStream.Write(dataImage, 0, dataImage.Length) ' ✅ corregido: usar dataImageBit.Length
                dataImageStream.Position = 0 ' Importante: reiniciar posición antes de leer

                Using bitmap As New Slyg.Tools.Imaging.FreeImageAPI.FreeImageBitmap(dataImageStream, ImageManager.GetImageFormat(formato))
                    Dim dataImageThumbnailList = ImageManager.GetThumbnailData(bitmap, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)
                    dataImageThumbnail = dataImageThumbnailList(0)
                End Using
            End Using

            Dim queryResponseCreateFolio As QueryResponse = ClientUtil.CreateImageFolioManager("", New List(Of QueryParameter) From {
                                                        New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                        New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.Proyecto.ToString()},
                                                        New QueryParameter With {.name = "fk_Documento", .value = fk_Documento.ToString()},
                                                        New QueryParameter With {.name = "fk_Usuario", .value = Program.Sesion.Usuario.id.ToString()},
                                                        New QueryParameter With {.name = "fk_Expediente", .value = fk_Expediente.ToString()},
                                                        New QueryParameter With {.name = "fk_Folder", .value = fk_Folder.ToString()},
                                                        New QueryParameter With {.name = "fk_File", .value = fileImage.ToString()},
                                                        New QueryParameter With {.name = "TotalFolios", .value = totalFoliosImage.ToString()},
                                                        New QueryParameter With {.name = "folio", .value = CInt(1).ToString()},
                                                        New QueryParameter With {.name = "dataImage", .value = Convert.ToBase64String(dataImage)},
                                                        New QueryParameter With {.name = "dataImageThumbnail", .value = Convert.ToBase64String(dataImageThumbnail)}
                                                    }, QueryRequestType.Table, QueryResponseType.NonQuery)

            If queryResponseCreateFolio.error IsNot Nothing Then
                Throw New Exception(queryResponseCreateFolio.error.ToString())
            End If

        Else
            totalFoliosImage = Convert.ToInt16(ImageManager.GetFolios(fileNamePDF))

            For folioActual As Integer = 1 To totalFoliosImage

                Dim dataImage = ImageManager.GetUnresizedFolioDataPdfOnly(fileNamePDF, folioActual, formato, compresion, flags)

                Dim dataImageThumbnail As Byte() = Nothing

                Using dataImageStream As Stream = New MemoryStream()
                    dataImageStream.Write(dataImage, 0, dataImage.Length) ' ✅ corregido: usar dataImageBit.Length
                    dataImageStream.Position = 0 ' Importante: reiniciar posición antes de leer

                    Using bitmap As New Slyg.Tools.Imaging.FreeImageAPI.FreeImageBitmap(dataImageStream, ImageManager.GetImageFormat(formato))
                        Dim dataImageThumbnailList = ImageManager.GetThumbnailData(bitmap, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)
                        dataImageThumbnail = dataImageThumbnailList(0)
                    End Using
                End Using

                Dim queryResponseCreateFolio As QueryResponse = ClientUtil.CreateImageFolioManager("", New List(Of QueryParameter) From {
                                                            New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                            New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.Proyecto.ToString()},
                                                            New QueryParameter With {.name = "fk_Documento", .value = fk_Documento.ToString()},
                                                            New QueryParameter With {.name = "fk_Usuario", .value = Program.Sesion.Usuario.id.ToString()},
                                                            New QueryParameter With {.name = "fk_Expediente", .value = fk_Expediente.ToString()},
                                                            New QueryParameter With {.name = "fk_Folder", .value = fk_Folder.ToString()},
                                                            New QueryParameter With {.name = "fk_File", .value = fileImage.ToString()},
                                                            New QueryParameter With {.name = "TotalFolios", .value = totalFoliosImage.ToString()},
                                                            New QueryParameter With {.name = "folio", .value = folioActual.ToString()},
                                                            New QueryParameter With {.name = "dataImage", .value = Convert.ToBase64String(dataImage)},
                                                            New QueryParameter With {.name = "dataImageThumbnail", .value = Convert.ToBase64String(dataImageThumbnail)}
                                                        }, QueryRequestType.Table, QueryResponseType.Scalar)

                If queryResponseCreateFolio.error IsNot Nothing Then
                    Throw New Exception(queryResponseCreateFolio.error.ToString())
                End If
            Next
        End If

        Return fileImage

    End Function

#End Region


#Region " Funciones "

    ''' <summary>
    ''' Obtiene la tabla de expediente asociado al medio magnético especificado para la entidad y proyecto activos.
    ''' </summary>
    ''' <returns>Devuelve un objeto DBCore.SchemaProcess.TBL_File_DataDataTable con la información del expediente.</returns>
    Public Function GetFileDataByMedioMagnetico() As DBCore.SchemaProcess.TBL_File_DataDataTable

        ' Buscar el Expediente correspondiente al Medio Magentico
        Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Process].[PA_Get_FileData_MediosMagneticos]", New List(Of QueryParameter) From {
                                                        New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                        New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                                        New QueryParameter With {.name = "fk_Numero_Medio_Magnetico", .value = _NumeroMedioMagnetico.ToString()}
                                                        }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

        Dim fileDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaProcess.TBL_File_DataDataTable(), queryResponse.dataTable), DBCore.SchemaProcess.TBL_File_DataDataTable)

        Return fileDataTable
    End Function

    Public Function RenderReportToTempPdf(ByVal reportViewer As ReportViewer, ByVal nameFile As String) As String
        Dim mimeType As String = ""
        Dim encoding As String = ""
        Dim extension As String = "pdf"
        Dim streamids As String() = Nothing
        Dim warnings As Warning() = Nothing

        Try
            ' Renderizar el reporte a PDF
            'reportViewer.SetDisplayMode(DisplayMode.PrintLayout)
            Dim pdfBytes As Byte() = reportViewer.LocalReport.Render("PDF", Nothing, mimeType, encoding, extension, streamids, warnings)

            Dim rutaArchivo As String = TempFilesPath & nameFile

            ' Guardar archivo en disco
            System.IO.File.WriteAllBytes(rutaArchivo, pdfBytes)

            ' Retornar ruta generada
            Return rutaArchivo

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function


#End Region

End Class
