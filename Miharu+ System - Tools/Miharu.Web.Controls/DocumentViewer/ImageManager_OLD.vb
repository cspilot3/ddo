Imports System
Imports System.IO
Imports System.Drawing
Imports Gios.PDF.SplitMerge
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Namespace DocumentViewerObjects

    Public Class ImageManager_OLD

#Region " Enumeraciones "

        Public Enum EnumFormat As Byte
            BMP = 1
            GIF = 2
            JPEG = 3
            PDF = 4
            PNG = 5
            TIFF_Color = 6
            TIFF_Bitonal = 7
        End Enum

#End Region

#Region " Metodos "

        Public Shared Sub Split(ByVal nInputFileNames As List(Of String), ByRef nOutputFileNames As List(Of String), ByRef nOutputFileName As String, ByVal nSuffixFormat As String, ByVal nDelay As Short)
            Dim Folio As Integer = 1

            If Not Directory.Exists(Path.GetDirectoryName(nOutputFileName)) Then
                Directory.CreateDirectory(Path.GetDirectoryName(nOutputFileName))
            End If

            For Each Item In nInputFileNames
                Dim Extension As String = System.IO.Path.GetExtension(Item).ToUpper()

                Select Case Extension
                    Case ".TIF"
                        Dim Data() As Byte = Nothing
                        Dim Folios As Short

                        Folios = GetFoliosTIFF(Item)

                        For i = 1 To Folios
                            Data = GetFolioTIFF(Item, i, 1, EnumFormat.GIF)

                            If Not Data Is Nothing Then
                                Dim fsOutput As FileStream = Nothing

                                Try
                                    Dim TempFileName As String

                                    TempFileName = nOutputFileName & "-" & Folio.ToString(nSuffixFormat) & ".gif"

                                    fsOutput = New FileStream(TempFileName, FileMode.Create, FileAccess.Write)

                                    fsOutput.Write(Data, 0, Data.Length)
                                    fsOutput.Close()
                                    fsOutput = Nothing
                                    nOutputFileNames.Add(TempFileName)
                                    Folio += 1
                                Catch ex As Exception
                                    Throw ex
                                Finally
                                    If (fsOutput IsNot Nothing) Then
                                        fsOutput.Close()
                                    End If
                                End Try

                            End If
                        Next

                    Case ".PDF"
                        Using s As Stream = New FileStream(Item, FileMode.Open, FileAccess.Read, FileShare.Read)
                            Dim pa As PdfParser = PdfParser.Parse(s)

                            For i = 0 To pa.PageCount - 1
                                Dim TempFileName As String

                                TempFileName = nOutputFileName & "-" & Folio.ToString(nSuffixFormat) & ".pdf"

                                Using s2 As Stream = New FileStream(TempFileName, FileMode.Create, FileAccess.Write)
                                    Dim tempMerger As New PdfMerger(s2)
                                    Dim pagina() As Integer = {i}

                                    tempMerger.Add(pa, pagina)
                                    tempMerger.Finish()
                                End Using

                                nOutputFileNames.Add(TempFileName)
                                Folio += 1
                            Next
                        End Using

                    Case Else
                        Dim TempFileName As String = nOutputFileName & "-" & Folio.ToString(nSuffixFormat) & System.IO.Path.GetExtension(Item)

                        If File.Exists(TempFileName) Then
                            File.Delete(TempFileName)
                        End If
                        File.Copy(Item, TempFileName, True)


                        Folio += 1
                        nOutputFileNames.Add(TempFileName)

                End Select

                System.Threading.Thread.Sleep(nDelay)
            Next
        End Sub

        Public Shared Sub Save(ByVal nImage As Bitmap, ByVal nFileName As String, ByVal nFormat As EnumFormat, ByVal nTempPath As String)
            Dim Formato As System.Drawing.Imaging.ImageFormat
            Dim Contador As Integer = 1
            Dim Extension As String

            Select Case nFormat
                Case EnumFormat.BMP
                    Formato = ImageFormat.Bmp
                    Extension = ".BMP"
                    nImage.Save(nFileName.ToUpper().Replace(Extension, "") & Extension, Formato)

                Case EnumFormat.GIF
                    Formato = ImageFormat.Gif
                    Extension = ".GIF"
                    nImage.Save(nFileName.ToUpper().Replace(Extension, "") & Extension, Formato)

                Case EnumFormat.JPEG
                    Formato = ImageFormat.Jpeg
                    Extension = ".JPG"
                    nImage.Save(nFileName.ToUpper().Replace(Extension, "") & Extension, Formato)
                Case EnumFormat.PNG
                    Formato = ImageFormat.Png
                    Extension = ".PNG"
                    nImage.Save(nFileName.ToUpper().Replace(Extension, "") & Extension, Formato)

                Case EnumFormat.TIFF_Bitonal
                    Dim codecInfo As ImageCodecInfo = getCodecForString("TIFF")

                    ' Convertir a bitonal
                    Dim newImage As Bitmap = ConvertToBitonal(nImage)


                    Dim iparams As EncoderParameters = New EncoderParameters(1)
                    Dim iparam As Encoder = Encoder.Compression
                    Dim iparamPara As EncoderParameter

                    iparamPara = New EncoderParameter(iparam, CType(EncoderValue.CompressionCCITT4, Long))
                    iparams.Param(0) = iparamPara
                    newImage.Save(nFileName, codecInfo, iparams)

                Case EnumFormat.TIFF_Color
                    Dim codecInfo As ImageCodecInfo = getCodecForString("TIFF")

                    Dim iparams As EncoderParameters = New EncoderParameters(1)
                    Dim iparam As Encoder = Encoder.Compression
                    Dim iparamPara As EncoderParameter

                    iparamPara = New EncoderParameter(iparam, CType(EncoderValue.CompressionLZW, Long))
                    iparams.Param(0) = iparamPara
                    nImage.Save(nFileName, codecInfo, iparams)

                Case EnumFormat.PDF
                    Dim Project As New PdfProject
                    Dim NewElement As PdfProjectElement
                    Dim TempFileName = nTempPath + System.IO.Path.GetFileName(nFileName) & ".PNG"
                    Extension = ".PDF"

                    Formato = ImageFormat.Png
                    nImage.Save(TempFileName, Formato)


                    NewElement = CType(Activator.CreateInstance(GetType(PdfProjectImage), New Object() {TempFileName}), PdfProjectElement)
                    NewElement.Analyze()

                    Project.Elements.Add(NewElement)

                    Project.Target = nFileName.ToUpper().TrimEnd(Extension.ToCharArray()) & Extension

                    Using s As New FileStream(Project.TempTarget, FileMode.Create, FileAccess.Write)
                        Dim Merger As New PdfMerger(s)

                        For Each Element As PdfProjectElement In Project.Elements
                            If Merger.CancelPending Then Return
                            If Element.Enabled Then Element.AddToMerger(Merger)
                        Next

                        Merger.Finish()
                    End Using

                    File.Copy(Project.TempTarget, Project.Target, True)
                    File.Delete(Project.TempTarget)

                Case Else
                    Throw New Exception("Formato de salida no válido: " & [Enum].GetName(GetType(ImageFormat), nFormat))

            End Select
        End Sub

        Public Shared Sub Save(ByVal nFileNames As List(Of String), ByVal nOutputFileName As String, ByVal nFormat As EnumFormat, ByVal nTempPath As String)
            Select Case nFormat
                Case EnumFormat.PDF
                    SaveToPDF(nFileNames, nOutputFileName, nTempPath)

                Case EnumFormat.TIFF_Color
                    SaveToTIFF(nFileNames, nOutputFileName, False)

                Case EnumFormat.TIFF_Bitonal
                    SaveToTIFF(nFileNames, nOutputFileName, True)

                Case EnumFormat.GIF, EnumFormat.BMP, EnumFormat.JPEG, EnumFormat.PNG
                    SaveToImage(nFileNames, nOutputFileName, "-00000", nFormat)

            End Select
        End Sub

        Public Shared Sub SaveToImage(ByVal nFileList As List(Of String), ByVal nFileName As String, ByVal nSuffixFormat As String, ByVal nFormat As EnumFormat)
            Dim Formato As System.Drawing.Imaging.ImageFormat
            Dim Contador As Integer = 1
            Dim Extension As String

            Select Case nFormat
                Case EnumFormat.BMP
                    Formato = ImageFormat.Bmp
                    Extension = ".BMP"

                Case EnumFormat.GIF
                    Formato = ImageFormat.Gif
                    Extension = ".GIF"

                Case EnumFormat.JPEG
                    Formato = ImageFormat.Jpeg
                    Extension = ".JPG"

                Case EnumFormat.PNG
                    Formato = ImageFormat.Png
                    Extension = ".PNG"

                Case Else
                    Formato = ImageFormat.Gif
                    Extension = ".GIF"

            End Select

            For Each Item In nFileList
                Select Case System.IO.Path.GetExtension(Item).ToUpper()
                    Case ".TIF"
                        'Si es archivo TIFF
                        Dim Data() As Byte = Nothing
                        Dim Paginas As Short

                        Paginas = GetFoliosTIFF(Item)

                        For Pagina As Short = 1 To Paginas
                            Data = GetFolioTIFF(Item, Pagina, 1, nFormat)

                            If Not Data Is Nothing Then
                                Dim TempFileName As String

                                TempFileName = System.IO.Path.GetDirectoryName(nFileName) + "\" + System.IO.Path.GetFileNameWithoutExtension(nFileName) + Contador.ToString(nSuffixFormat) + Extension

                                Dim fsOutput As New FileStream(TempFileName, FileMode.Create, FileAccess.Write)

                                fsOutput.Write(Data, 0, Data.Length)
                                fsOutput.Close()
                            End If

                            Contador += 1
                        Next

                    Case ".PDF"
                        'Si es archivo PDF
                        Dim PDFDoc As AFPDFLibNET.AFPDFDoc = New AFPDFLibNET.AFPDFDoc()
                        Dim Data() As Byte = Nothing
                        Dim objPDF As PdfParser

                        Using InputFile As Stream = File.OpenRead(Item)
                            objPDF = PdfParser.Parse(InputFile)
                            PDFDoc.LoadFromFile(Item)

                            For Pagina As Integer = 1 To objPDF.PageCount
                                Dim TempFileName As String = String.Empty

                                TempFileName = System.IO.Path.GetDirectoryName(nFileName) + "\" + System.IO.Path.GetFileNameWithoutExtension(nFileName) + Contador.ToString(nSuffixFormat) + Extension

                                Dim NewImage As Bitmap

                                Using OutputFile As Stream = New FileStream(TempFileName, FileMode.Create, FileAccess.Write)
                                    Dim picImage As New PictureBox

                                    PDFDoc.CurrentPage = Pagina

                                    picImage.Height = PDFDoc.PageHeight
                                    picImage.Width = PDFDoc.PageWidth

                                    NewImage = PDFToImageConverter.ConvertPDFtoImage(PDFDoc, picImage.Handle.ToInt32(), Pagina)
                                    NewImage.Save(OutputFile, Formato)
                                    NewImage.Dispose()

                                    picImage.Image = Nothing

                                    OutputFile.Close()

                                    Contador += 1
                                End Using
                            Next

                            InputFile.Close()
                        End Using
                    Case Else
                        'si es otro, debe ser una imagen
                        Dim NewImage As New Bitmap(Item)
                        Dim FileName As String

                        FileName = System.IO.Path.GetDirectoryName(nFileName) + "\" + System.IO.Path.GetFileNameWithoutExtension(nFileName) + Contador.ToString(nSuffixFormat) + Extension
                        NewImage.Save(FileName, Formato)
                        Contador += 1
                End Select
            Next
        End Sub

        Private Shared Sub AddToPDF(ByRef nProject As PdfProject, ByVal nFileName As String, ByVal nTempPath As String)
            Dim Extension As String = System.IO.Path.GetExtension(nFileName).ToUpper()
            Dim NewElement As PdfProjectElement

            Select Case Extension
                Case ".TIF"
                    'Si el archivo insertado es un TIFF
                    Dim Data() As Byte = Nothing
                    Dim Folios As Short

                    Folios = GetFoliosTIFF(nFileName)

                    For i = 1 To Folios
                        Data = GetFolioTIFF(nFileName, i, 1, EnumFormat.PNG)

                        If Not Data Is Nothing Then
                            Dim TempFileName As String

                            If Not Directory.Exists(nTempPath) Then
                                Directory.CreateDirectory(nTempPath)
                            End If

                            TempFileName = nTempPath + System.IO.Path.GetFileNameWithoutExtension(nFileName) + "-" + i.ToString("0000") + ".png"

                            Dim fsOutput As New FileStream(TempFileName, FileMode.Create, FileAccess.Write)

                            fsOutput.Write(Data, 0, Data.Length)
                            fsOutput.Close()

                            NewElement = CType(Activator.CreateInstance(GetType(PdfProjectImage), New Object() {TempFileName}), PdfProjectElement)
                            NewElement.Analyze()

                            nProject.Elements.Add(NewElement)
                        End If
                    Next

                Case ".PDF"
                    'Si el archivo insertado es un PDF
                    Using DocumentoPDF As Stream = New FileStream(nFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
                        Dim ParsedDocument As PdfParser = PdfParser.Parse(DocumentoPDF)
                        For i = 0 To ParsedDocument.PageCount - 1
                            Dim TempFileName As String

                            If Not Directory.Exists(nTempPath) Then
                                Directory.CreateDirectory(nTempPath)
                            End If

                            TempFileName = nTempPath + System.IO.Path.GetFileNameWithoutExtension(nFileName) + "-" + i.ToString("0000") + ".pdf"

                            Using PaginaPDF As Stream = New FileStream(TempFileName, FileMode.Create, FileAccess.Write)
                                Dim tempMerger As New PdfMerger(PaginaPDF)
                                Dim pagina() As Integer = {i}

                                tempMerger.Add(ParsedDocument, pagina)
                                tempMerger.Finish()
                                NewElement = CType(Activator.CreateInstance(GetType(PdfProjectPdf), New Object() {TempFileName}), PdfProjectElement)
                                NewElement.Analyze()
                                nProject.Elements.Add(NewElement)
                            End Using
                        Next
                    End Using
                Case Else
                    'Por el contrario, debe ser un archivo de Imagen
                    NewElement = CType(Activator.CreateInstance(GetType(PdfProjectImage), New Object() {nFileName}), PdfProjectElement)
                    NewElement.Analyze()
                    nProject.Elements.Add(NewElement)

            End Select
        End Sub

        Private Shared Sub SaveToTIFF(ByVal nFileList As List(Of String), ByVal nFileName As String, ByVal nBitonal As Boolean)
            Dim Images As New List(Of Bitmap)

            ' Cargar los archivos por folio
            For Each FileItem As String In nFileList
                Dim Extension As String = System.IO.Path.GetExtension(FileItem.ToString()).ToUpper()

                Select Case Extension
                    Case ".TIF"
                        'Si es archivo TIFF
                        'Se crean temporales separando cada pagina                        
                        Dim Paginas As Short = GetFoliosTIFF(FileItem)
                        Dim Imagen As New Bitmap(FileItem)

                        For i = 1 To Paginas
                            Images.Add(GetFolio(Imagen, i))
                        Next

                    Case ".PDF"
                        'Si es archivo PDF
                        'Se crean temporales separando cada pagina
                        Dim PDFDoc As AFPDFLibNET.AFPDFDoc = New AFPDFLibNET.AFPDFDoc
                        Dim Data() As Byte = Nothing
                        Dim objPDF As PdfParser

                        Using InputFile As Stream = File.OpenRead(FileItem.ToString())
                            objPDF = PdfParser.Parse(InputFile)
                            PDFDoc.LoadFromFile(FileItem.ToString())

                            For i = 1 To objPDF.PageCount
                                Dim NewImage As Bitmap
                                Dim ImagePictureBox As New PictureBox

                                PDFDoc.CurrentPage = i

                                ImagePictureBox.Height = PDFDoc.PageHeight
                                ImagePictureBox.Width = PDFDoc.PageWidth

                                NewImage = PDFToImageConverter.ConvertPDFtoImage(PDFDoc, ImagePictureBox.Handle.ToInt32(), i)
                                ImagePictureBox.Image = Nothing

                                Images.Add(New Bitmap(NewImage))
                            Next

                            InputFile.Close()
                        End Using

                    Case Else
                        'Si es otra clase de archivo (imagen)
                        Images.Add(New Bitmap(FileItem))
                End Select
            Next

            If Images IsNot Nothing Then
                Dim codecInfo As ImageCodecInfo = getCodecForString("TIFF")

                ' Convertir a bitonal
                If (nBitonal) Then
                    For i As Integer = 0 To Images.Count - 1
                        If (Images(i) IsNot Nothing) Then
                            Images(i) = ConvertToBitonal(Images(i))
                        End If
                    Next
                End If

                If (Images.Count = 1) Then
                    Dim iparams As EncoderParameters = New EncoderParameters(1)
                    Dim iparam As Encoder = Encoder.Compression
                    Dim iparamPara As EncoderParameter

                    If (nBitonal) Then
                        iparamPara = New EncoderParameter(iparam, CType(EncoderValue.CompressionCCITT4, Long))
                    Else
                        iparamPara = New EncoderParameter(iparam, CType(EncoderValue.CompressionLZW, Long))
                    End If

                    iparams.Param(0) = iparamPara
                    Images(0).Save(nFileName, codecInfo, iparams)

                ElseIf (Images.Count > 1) Then
                    Dim saveEncoder As Encoder
                    Dim compressionEncoder As Encoder

                    Dim CompressionEncodeParam As EncoderParameter
                    Dim EncoderParams As EncoderParameters = New EncoderParameters(2)

                    saveEncoder = Encoder.SaveFlag
                    compressionEncoder = Encoder.Compression

                    If (nBitonal) Then
                        CompressionEncodeParam = New EncoderParameter(compressionEncoder, CType(EncoderValue.CompressionCCITT4, Long))
                    Else
                        CompressionEncodeParam = New EncoderParameter(compressionEncoder, CType(EncoderValue.CompressionLZW, Long))
                    End If

                    EncoderParams.Param(0) = CompressionEncodeParam
                    EncoderParams.Param(1) = New EncoderParameter(saveEncoder, CType(EncoderValue.MultiFrame, Long))

                    If File.Exists(nFileName) Then File.Delete(nFileName)

                    ' Save the first page (frame).
                    Images(0).Save(nFileName, codecInfo, EncoderParams)

                    For i As Integer = 1 To Images.Count - 1
                        EncoderParams.Param(1) = New EncoderParameter(saveEncoder, CType(EncoderValue.FrameDimensionPage, Long))
                        Images(0).SaveAdd(Images(i), EncoderParams)
                    Next

                    EncoderParams.Param(0) = New EncoderParameter(saveEncoder, CType(EncoderValue.Flush, Long))
                    Images(0).SaveAdd(EncoderParams)
                End If

                Images.Clear()
            End If
        End Sub

        Private Shared Sub SaveToPDF(ByVal nFileList As List(Of String), ByVal nFileName As String, ByVal nTempPath As String)
            Dim Project As New PdfProject

            For Each FileItem In nFileList
                AddToPDF(Project, FileItem, nTempPath)
            Next

            Project.Target = nFileName

            Using s As New FileStream(Project.TempTarget, FileMode.Create, FileAccess.Write)
                Dim Merger As New PdfMerger(s)

                For Each NewElement As PdfProjectElement In Project.Elements
                    If Merger.CancelPending Then Return
                    If NewElement.Enabled Then NewElement.AddToMerger(Merger)
                Next

                Merger.Finish()
            End Using

            File.Copy(Project.TempTarget, Project.Target, True)
            File.Delete(Project.TempTarget)
        End Sub

#End Region

#Region " Funciones "

        Public Shared Function GetFile(ByVal FileName As String) As Byte()
            Dim fsInput As New FileStream(FileName, FileMode.Open, FileAccess.Read)
            Dim Longitud As Integer = CInt(fsInput.Length - 1)
            Dim Data(Longitud) As Byte

            fsInput.Read(Data, 0, Data.Length)
            fsInput.Close()

            Return Data
        End Function

        Public Shared Function GetFolio(ByRef Imagen As Bitmap, ByVal Folio As Integer) As Bitmap
            Dim myFrameDimension As FrameDimension
            Dim myGuid() As Guid

            ' Crea un objeto mapa de bits a partir de la imagen            
            myGuid = Imagen.FrameDimensionsList()
            myFrameDimension = New FrameDimension(myGuid(0))
            Imagen.SelectActiveFrame(myFrameDimension, Folio - 1)
            Return New Bitmap(Imagen)
        End Function

        Public Shared Function GetFolio(ByVal FileName As String, ByVal Folio As Integer, ByVal Resolucion As Single, ByVal nFormat As EnumFormat) As Byte()
            Dim Extension As String = Path.GetExtension(FileName).ToLower()

            Select Case Extension
                Case ".tif"
                    Return GetFolioTIFF(FileName, Folio, Resolucion, nFormat)

                Case ".pdf"
                    Return GetFolioPDF(FileName, Folio, Resolucion, nFormat)

                Case Else
                    Dim files As New List(Of String)
                    files.Add(FileName)
                    If EsImagen(files) Then
                        Return GetFolio(files, Folio, Resolucion, nFormat)
                    Else
                        Return Nothing
                    End If
            End Select
        End Function

        Public Shared Function GetFolio(ByVal FileName As List(Of String), ByVal Folio As Integer, ByVal Resolucion As Single, ByVal ImageType As EnumFormat) As Byte()
            If ValidarExtensiones(FileName) Then
                If EsImagen(FileName) Then
                    Dim myEncoder As Encoder
                    Dim myImageCodecInfo As ImageCodecInfo
                    Dim myEncoderParameter As EncoderParameter
                    Dim myEncoderParameters As EncoderParameters
                    Dim Imagen As Bitmap
                    ' Crea un objeto mapa de bits a partir de la imagen
                    Imagen = New Bitmap(FileName(Folio - 1))
                    Imagen = New Bitmap(Imagen, CInt(Imagen.Width * Resolucion), CInt(Imagen.Height * Resolucion))
                    Select Case ImageType
                        Case EnumFormat.BMP
                            myImageCodecInfo = getEncoderInfo("image/bmp")
                        Case EnumFormat.GIF
                            myImageCodecInfo = getEncoderInfo("image/gif")
                        Case EnumFormat.JPEG
                            myImageCodecInfo = getEncoderInfo("image/jpeg")
                        Case EnumFormat.PNG
                            myImageCodecInfo = getEncoderInfo("image/png")
                        Case Else
                            myImageCodecInfo = getEncoderInfo("image/gif")
                    End Select
                    ' Crea un objeto Codificador basado en SaveFlag.
                    myEncoder = Encoder.SaveFlag
                    ' Crea los parámetros de codificación
                    myEncoderParameters = New EncoderParameters(1)
                    myEncoderParameter = New EncoderParameter(myEncoder, 0)
                    myEncoderParameters.Param(0) = myEncoderParameter
                    Dim myMemoryStream As New MemoryStream()
                    Imagen.Save(myMemoryStream, myImageCodecInfo, myEncoderParameters)
                    Dim Data(CInt(myMemoryStream.Length - 1)) As Byte
                    myMemoryStream.Position = 0
                    myMemoryStream.Read(Data, 0, Data.Length)
                    myMemoryStream.Close()

                    Return Data
                Else
                    Return GetFolio(FileName(0), 1, Resolucion, ImageType)
                End If
            Else
                Return Nothing
            End If
        End Function

        Private Shared Function GetFolioPDF(ByVal FileName As String, ByVal Folio As Integer) As Bitmap
            Dim Imagen As Bitmap

            ' Crea un objeto mapa de bits a partir de la imagen
            Dim PDFDoc As AFPDFLibNET.AFPDFDoc = New AFPDFLibNET.AFPDFDoc()
            Dim ImagePictureBox As New PictureBox

            PDFDoc.LoadFromFile(FileName)
            PDFDoc.CurrentPage = Folio
            ImagePictureBox.Height = PDFDoc.PageHeight
            ImagePictureBox.Width = PDFDoc.PageWidth
            Imagen = PDFToImageConverter.ConvertPDFtoImage(PDFDoc, ImagePictureBox.Handle.ToInt32(), Folio)
            ImagePictureBox.Image = Nothing

            Return Imagen
        End Function

        Private Shared Function GetFolioPDF(ByVal FileName As String, ByVal Folio As Integer, ByVal Resolucion As Single, ByVal nFormat As EnumFormat) As Byte()
            Dim myEncoder As Encoder
            Dim myImageCodecInfo As ImageCodecInfo
            Dim myEncoderParameter As EncoderParameter
            Dim myEncoderParameters As EncoderParameters
            Dim Imagen As Bitmap

            ' Crea un objeto mapa de bits a partir de la imagen
            Dim PDFDoc As AFPDFLibNET.AFPDFDoc = New AFPDFLibNET.AFPDFDoc()
            Dim ImagePictureBox As New PictureBox

            PDFDoc.LoadFromFile(FileName)
            PDFDoc.CurrentPage = Folio
            ImagePictureBox.Height = PDFDoc.PageHeight
            ImagePictureBox.Width = PDFDoc.PageWidth
            Imagen = PDFToImageConverter.ConvertPDFtoImage(PDFDoc, ImagePictureBox.Handle.ToInt32(), Folio)
            ImagePictureBox.Image = Nothing

            Imagen = New Bitmap(Imagen, CInt(Imagen.Width * Resolucion), CInt(Imagen.Height * Resolucion))

            'Comienza a trabajar la imagen de salida
            Select Case nFormat
                Case EnumFormat.BMP
                    myImageCodecInfo = getEncoderInfo("image/bmp")
                Case EnumFormat.GIF
                    myImageCodecInfo = getEncoderInfo("image/gif")
                Case EnumFormat.JPEG
                    myImageCodecInfo = getEncoderInfo("image/jpeg")
                Case EnumFormat.PNG
                    myImageCodecInfo = getEncoderInfo("image/png")
                Case EnumFormat.TIFF_Bitonal, EnumFormat.TIFF_Color
                    myImageCodecInfo = getEncoderInfo("image/tiff")
                Case Else
                    myImageCodecInfo = getEncoderInfo("image/gif")
            End Select

            ' Crea un objeto Codificador basado en SaveFlag.
            myEncoder = Encoder.SaveFlag

            ' Crea los parámetros de codificación
            myEncoderParameters = New EncoderParameters(1)
            myEncoderParameter = New EncoderParameter(myEncoder, 0)
            myEncoderParameters.Param(0) = myEncoderParameter

            Dim myMemoryStream As New MemoryStream()
            Imagen.Save(myMemoryStream, myImageCodecInfo, myEncoderParameters)

            Dim Data(CInt(myMemoryStream.Length - 1)) As Byte
            myMemoryStream.Position = 0
            myMemoryStream.Read(Data, 0, Data.Length)
            myMemoryStream.Close()

            Return Data
        End Function

        Private Shared Function GetFolioTIFF(ByVal FileName As String, ByVal Folio As Integer, ByVal Resolucion As Single, ByVal ImageType As EnumFormat) As Byte()
            Dim myEncoder As Encoder
            Dim myImageCodecInfo As ImageCodecInfo
            Dim myEncoderParameter As EncoderParameter
            Dim myEncoderParameters As EncoderParameters
            Dim myFrameDimension As FrameDimension
            Dim myGuid() As Guid
            Dim Imagen As Bitmap

            ' Crea un objeto mapa de bits a partir de la imagen
            Imagen = New Bitmap(FileName)
            myGuid = Imagen.FrameDimensionsList()
            myFrameDimension = New FrameDimension(myGuid(0))
            Imagen.SelectActiveFrame(myFrameDimension, Folio - 1)
            Imagen = New Bitmap(Imagen, CInt(Imagen.Width * Resolucion), CInt(Imagen.Height * Resolucion))

            Select Case ImageType
                Case EnumFormat.BMP
                    myImageCodecInfo = getEncoderInfo("image/bmp")
                Case EnumFormat.GIF
                    myImageCodecInfo = getEncoderInfo("image/gif")
                Case EnumFormat.JPEG
                    myImageCodecInfo = getEncoderInfo("image/jpeg")
                Case EnumFormat.PNG
                    myImageCodecInfo = getEncoderInfo("image/png")
                Case EnumFormat.TIFF_Color, EnumFormat.TIFF_Bitonal
                    myImageCodecInfo = getEncoderInfo("image/tiff")
                Case Else
                    myImageCodecInfo = getEncoderInfo("image/gif")
            End Select

            ' Crea un objeto Codificador basado en SaveFlag.
            myEncoder = Encoder.SaveFlag

            ' Crea los parámetros de codificación
            myEncoderParameters = New EncoderParameters(1)
            myEncoderParameter = New EncoderParameter(myEncoder, 0)
            myEncoderParameters.Param(0) = myEncoderParameter

            Dim myMemoryStream As New MemoryStream()
            Imagen.Save(myMemoryStream, myImageCodecInfo, myEncoderParameters)

            Dim Data(CInt(myMemoryStream.Length - 1)) As Byte
            myMemoryStream.Position = 0
            myMemoryStream.Read(Data, 0, Data.Length)
            myMemoryStream.Close()

            Return Data
        End Function

        Public Shared Function GetFolios(ByVal FileName As String) As Short
            Dim Extension As String = Path.GetExtension(FileName).ToLower

            Select Case Extension
                Case ".tif"
                    Return GetFoliosTIFF(FileName)

                Case ".pdf"
                    Return GetFoliosPDF(FileName)

                Case Else
                    Dim files As New List(Of String)

                    files.Add(FileName)

                    If EsImagen(files) Then
                        Return GetFolios(files)
                    Else
                        Return Nothing
                    End If
            End Select
        End Function

        Public Shared Function GetFolios(ByVal FileNames As List(Of String)) As Short
            If ValidarExtensiones(FileNames) Then
                If EsImagen(FileNames) Then
                    Return CShort(FileNames.Count)
                Else
                    Return GetFolios(FileNames(0))
                End If
            Else
                Return 0
            End If
        End Function

        Private Shared Function GetFoliosPDF(ByVal FileName As String) As Short
            Dim objPDF As PdfParser
            Dim s As Stream = System.IO.File.OpenRead(FileName)

            Using (s)
                objPDF = PdfParser.Parse(s)
                Return CShort(objPDF.PageCount)
            End Using
        End Function

        Private Shared Function GetFoliosTIFF(ByVal FileName As String) As Short
            Dim myFrameDimension As FrameDimension
            Dim myGuid() As Guid
            Dim Imagen As Bitmap

            ' Crea un objeto mapa de bits a partir de la imagen
            Imagen = New Bitmap(FileName)
            myGuid = Imagen.FrameDimensionsList()
            myFrameDimension = New FrameDimension(myGuid(0))

            Return CShort(Imagen.GetFrameCount(myFrameDimension))
        End Function

        Public Shared Function GetThumbnail(ByVal nImage As Bitmap, ByVal Ancho As Integer, ByVal Alto As Integer) As Bitmap
            If (nImage.Width / Ancho) > (nImage.Height / Alto) Then
                Return New Bitmap(nImage, Ancho, CInt(nImage.Height / (nImage.Width / Ancho)))
            Else
                Return New Bitmap(nImage, CInt(nImage.Width / (nImage.Height / Alto)), Alto)
            End If
        End Function

        Public Shared Function GetThumbnail(ByVal FileName As String, ByVal FolioInicial As Integer, ByVal FolioFinal As Integer, ByVal MaxAncho As Integer, ByVal MaxAlto As Integer, ByVal ImageType As EnumFormat) As List(Of Byte())
            Dim Extension As String = Path.GetExtension(FileName).ToLower

            Select Case Extension
                Case ".tif"
                    Return GetThumbnailTIFF(FileName, FolioInicial, FolioFinal, MaxAncho, MaxAlto, ImageType)
                Case ".pdf"
                    Return GetThumbnailPDF(FileName, FolioInicial, FolioFinal, MaxAncho, MaxAlto, ImageType)
                Case Else
                    Dim files As New List(Of String)
                    files.Add(FileName)
                    If EsImagen(files) Then
                        Return GetThumbnail(files, FolioInicial, FolioFinal, MaxAncho, MaxAlto, ImageType)
                    Else
                        Return Nothing
                    End If
            End Select
        End Function

        Public Shared Function GetThumbnail(ByVal FileName As List(Of String), ByVal FolioInicial As Integer, ByVal FolioFinal As Integer, ByVal MaxAncho As Integer, ByVal MaxAlto As Integer, ByVal ImageType As EnumFormat) As List(Of Byte())
            If ValidarExtensiones(FileName) Then
                If EsImagen(FileName) Then
                    Dim Imagenes As New List(Of Byte())

                    For Each Item As String In FileName
                        If FileName.IndexOf(Item) + 1 >= FolioInicial And FileName.IndexOf(Item) + 1 <= FolioFinal Then
                            Dim myEncoder As Encoder
                            Dim myImageCodecInfo As ImageCodecInfo
                            Dim myEncoderParameter As EncoderParameter
                            Dim myEncoderParameters As EncoderParameters
                            Dim myFrameDimension As FrameDimension
                            Dim myGuid() As Guid
                            Dim Imagen As New Bitmap(Item)
                            Dim Proporcion As Single

                            myGuid = Imagen.FrameDimensionsList()
                            myFrameDimension = New FrameDimension(myGuid(0))

                            Select Case ImageType
                                Case EnumFormat.BMP
                                    myImageCodecInfo = getEncoderInfo("image/bmp")
                                Case EnumFormat.GIF
                                    myImageCodecInfo = getEncoderInfo("image/gif")
                                Case EnumFormat.JPEG
                                    myImageCodecInfo = getEncoderInfo("image/jpeg")
                                Case EnumFormat.PNG
                                    myImageCodecInfo = getEncoderInfo("image/png")
                                Case Else
                                    myImageCodecInfo = getEncoderInfo("image/gif")
                            End Select

                            ' Crea un objeto Codificador basado en SaveFlag.
                            myEncoder = Encoder.SaveFlag

                            ' Crea los parámetros de codificación
                            myEncoderParameters = New EncoderParameters(1)
                            myEncoderParameter = New EncoderParameter(myEncoder, 0)
                            myEncoderParameters.Param(0) = myEncoderParameter

                            Dim ImagenThumbnail As Bitmap
                            Dim Data() As Byte


                            If MaxAncho > 0 Then
                                Proporcion = CSng(MaxAncho / Imagen.Width)
                            Else
                                Proporcion = CSng(MaxAlto / Imagen.Height)
                            End If

                            ImagenThumbnail = New Bitmap(Imagen, CInt(Imagen.Width * Proporcion), CInt(Imagen.Height * Proporcion))

                            Dim myMemoryStream As New MemoryStream()

                            ImagenThumbnail.Save(myMemoryStream, myImageCodecInfo, myEncoderParameters)
                            ReDim Data(CInt(myMemoryStream.Length - 1))

                            myMemoryStream.Position = 0
                            myMemoryStream.Read(Data, 0, Data.Length)
                            myMemoryStream.Close()

                            Imagenes.Add(Data)
                        End If
                    Next
                    Return Imagenes
                Else
                    Return GetThumbnail(FileName(0).ToString(), FolioInicial, FolioFinal, MaxAncho, MaxAlto, ImageType)
                End If
            Else
                Return Nothing
            End If
        End Function

        Private Shared Function GetThumbnailPDF(ByVal FileName As String, ByVal FolioInicial As Integer, ByVal FolioFinal As Integer, ByVal MaxAncho As Integer, ByVal MaxAlto As Integer, ByVal ImageType As EnumFormat) As List(Of Byte())
            Dim myEncoder As Encoder
            Dim myImageCodecInfo As ImageCodecInfo
            Dim myEncoderParameter As EncoderParameter
            Dim myEncoderParameters As EncoderParameters
            Dim Imagen As Bitmap
            Dim Proporcion As Single

            Dim PDFDoc As AFPDFLibNET.AFPDFDoc = New AFPDFLibNET.AFPDFDoc()
            Dim ImagePictureBox As New PictureBox
            Dim Imagenes As New List(Of Byte())

            'Crea un Documento PDF con la ruta FileName
            PDFDoc.LoadFromFile(FileName)

            For Folio As Integer = FolioInicial To FolioFinal
                PDFDoc.CurrentPage = Folio
                ImagePictureBox.Height = PDFDoc.PageHeight
                ImagePictureBox.Width = PDFDoc.PageWidth

                'Obtiene la imagen de la pagina No. Folio
                Imagen = PDFToImageConverter.ConvertPDFtoImage(PDFDoc, ImagePictureBox.Handle.ToInt32(), Folio)
                ImagePictureBox.Image = Nothing

                'Se obtiene la Informacion del codificador segun el formato de imagen deseada
                Select Case ImageType
                    Case EnumFormat.BMP
                        myImageCodecInfo = getEncoderInfo("image/bmp")

                    Case EnumFormat.GIF
                        myImageCodecInfo = getEncoderInfo("image/gif")

                    Case EnumFormat.JPEG
                        myImageCodecInfo = getEncoderInfo("image/jpeg")

                    Case EnumFormat.PNG
                        myImageCodecInfo = getEncoderInfo("image/png")

                    Case Else
                        myImageCodecInfo = getEncoderInfo("image/gif")

                End Select

                ' Crea un objeto Codificador basado en SaveFlag.
                myEncoder = Encoder.SaveFlag

                ' Crea los parámetros de codificación
                myEncoderParameters = New EncoderParameters(1)
                myEncoderParameter = New EncoderParameter(myEncoder, 0)
                myEncoderParameters.Param(0) = myEncoderParameter

                Dim ImagenThumbnail As Bitmap
                Dim Data() As Byte

                If MaxAncho > 0 Then
                    Proporcion = CSng(MaxAncho / Imagen.Width)
                Else
                    Proporcion = CSng(MaxAlto / Imagen.Height)
                End If

                ImagenThumbnail = New Bitmap(Imagen, CInt(Imagen.Width * Proporcion), CInt(Imagen.Height * Proporcion))

                Dim myMemoryStream As New MemoryStream()

                ImagenThumbnail.Save(myMemoryStream, myImageCodecInfo, myEncoderParameters)
                ReDim Data(CInt(myMemoryStream.Length - 1))

                myMemoryStream.Position = 0
                myMemoryStream.Read(Data, 0, Data.Length)
                myMemoryStream.Close()

                Imagenes.Add(Data)
            Next

            Return Imagenes
        End Function

        Private Shared Function GetThumbnailTIFF(ByVal FileName As String, ByVal FolioInicial As Integer, ByVal FolioFinal As Integer, ByVal MaxAncho As Integer, ByVal MaxAlto As Integer, ByVal ImageType As EnumFormat) As List(Of Byte())
            Dim myEncoder As Encoder
            Dim myImageCodecInfo As ImageCodecInfo
            Dim myEncoderParameter As EncoderParameter
            Dim myEncoderParameters As EncoderParameters
            Dim myFrameDimension As FrameDimension
            Dim myGuid() As Guid
            Dim Imagen As New Bitmap(FileName)
            Dim Proporcion As Single
            Dim Imagenes As New List(Of Byte())

            myGuid = Imagen.FrameDimensionsList()
            myFrameDimension = New FrameDimension(myGuid(0))

            Select Case ImageType
                Case EnumFormat.BMP
                    myImageCodecInfo = getEncoderInfo("image/bmp")
                Case EnumFormat.GIF
                    myImageCodecInfo = getEncoderInfo("image/gif")
                Case EnumFormat.JPEG
                    myImageCodecInfo = getEncoderInfo("image/jpeg")
                Case EnumFormat.PNG
                    myImageCodecInfo = getEncoderInfo("image/png")
                Case Else
                    myImageCodecInfo = getEncoderInfo("image/gif")
            End Select

            ' Crea un objeto Codificador basado en SaveFlag.
            myEncoder = Encoder.SaveFlag

            ' Crea los parámetros de codificación
            myEncoderParameters = New EncoderParameters(1)
            myEncoderParameter = New EncoderParameter(myEncoder, 0)
            myEncoderParameters.Param(0) = myEncoderParameter

            For Folio As Integer = FolioInicial To FolioFinal
                Dim ImagenThumbnail As Bitmap
                Dim Data() As Byte

                Imagen.SelectActiveFrame(myFrameDimension, Folio - 1)

                If MaxAncho > 0 Then
                    Proporcion = CSng(MaxAncho / Imagen.Width)
                Else
                    Proporcion = CSng(MaxAlto / Imagen.Height)
                End If

                ImagenThumbnail = New Bitmap(Imagen, CInt(Imagen.Width * Proporcion), CInt(Imagen.Height * Proporcion))

                Dim myMemoryStream As New MemoryStream()

                ImagenThumbnail.Save(myMemoryStream, myImageCodecInfo, myEncoderParameters)
                ReDim Data(CInt(myMemoryStream.Length - 1))

                myMemoryStream.Position = 0
                myMemoryStream.Read(Data, 0, Data.Length)
                myMemoryStream.Close()

                Imagenes.Add(Data)
            Next

            Return Imagenes
        End Function

        Private Shared Function getEncoderInfo(ByVal mimeType As String) As ImageCodecInfo
            Dim i As Integer
            Dim myEncoders() As ImageCodecInfo

            myEncoders = ImageCodecInfo.GetImageEncoders()

            For i = 0 To myEncoders.Length - 1
                If myEncoders(i).MimeType = mimeType Then
                    Return myEncoders(i)
                End If
            Next

            Return Nothing
        End Function

        Private Shared Function ValidarExtensiones(ByVal FileName As List(Of String)) As Boolean
            Dim ExtensionesValidas As New List(Of String)
            Dim Valido As Boolean = False

            ExtensionesValidas.Clear()

            ExtensionesValidas.Add(".gif")
            ExtensionesValidas.Add(".jpg")
            ExtensionesValidas.Add(".bmp")
            ExtensionesValidas.Add(".png")

            If FileName.Count = 1 Then
                ExtensionesValidas.Add(".pdf")
                ExtensionesValidas.Add(".tif")
            End If

            For Each Item In FileName
                If Not ExtensionesValidas.Contains(Path.GetExtension(Item).ToLower()) Then
                    Valido = False
                Else
                    Valido = True
                End If
            Next

            Return Valido
        End Function

        Private Shared Function EsImagen(ByVal FileName As List(Of String)) As Boolean
            Dim ExtensionesValidas As New List(Of String)
            Dim Valido As Boolean = False

            ExtensionesValidas.Clear()
            ExtensionesValidas.Add(".gif")
            ExtensionesValidas.Add(".jpg")
            ExtensionesValidas.Add(".bmp")
            ExtensionesValidas.Add(".png")

            For Each Item In FileName
                If Not ExtensionesValidas.Contains(Path.GetExtension(Item).ToLower()) Then
                    Valido = False
                Else
                    Valido = True
                End If
            Next

            Return Valido
        End Function

        Public Shared Function getEnumImageType(ByVal nExtension As String) As EnumFormat
            Select Case nExtension.ToLower().TrimStart("."c)
                Case "bmp"
                    Return EnumFormat.BMP
                Case "gif"
                    Return EnumFormat.GIF
                Case "jpg", "jpeg"
                    Return EnumFormat.JPEG
                Case "pdf"
                    Return EnumFormat.PDF
                Case "png"
                    Return EnumFormat.PNG
                Case "tif"
                    Return EnumFormat.TIFF_Color
            End Select
        End Function

        Private Shared Function ConvertToBitonal(ByVal original As Bitmap) As Bitmap
            Dim source As Bitmap = Nothing
            ' If original bitmap is not already in 32 BPP, ARGB format, then convert
            If original.PixelFormat <> PixelFormat.Format32bppArgb Then
                source = New Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb)
                source.SetResolution(original.HorizontalResolution, original.VerticalResolution)
                Using g As Graphics = Graphics.FromImage(source)
                    g.DrawImageUnscaled(original, 0, 0)
                End Using
            Else
                source = original
            End If
            ' Lock source bitmap in memory
            Dim sourceData As BitmapData = source.LockBits(New Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb)
            ' Copy image data to binary array
            Dim imageSize As Integer = sourceData.Stride * sourceData.Height
            Dim sourceBuffer(imageSize - 1) As Byte
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, imageSize)
            ' Unlock source bitmap
            source.UnlockBits(sourceData)
            ' Create destination bitmap
            Dim destination As New Bitmap(source.Width, source.Height, PixelFormat.Format1bppIndexed)
            ' Lock destination bitmap in memory
            Dim destinationData As BitmapData = destination.LockBits(New Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed)
            ' Create destination buffer
            imageSize = destinationData.Stride * destinationData.Height
            Dim destinationBuffer(imageSize - 1) As Byte
            Dim sourceIndex As Integer = 0
            Dim destinationIndex As Integer = 0
            Dim pixelTotal As Integer = 0
            Dim destinationValue As Byte = 0
            Dim pixelValue As Integer = 128
            Dim height As Integer = source.Height
            Dim width As Integer = source.Width
            Dim threshold As Integer = 500
            ' Iterate lines
            For y As Integer = 0 To height - 1
                sourceIndex = y * sourceData.Stride
                destinationIndex = y * destinationData.Stride
                destinationValue = 0
                pixelValue = 128
                ' Iterate pixels
                For x As Integer = 0 To width - 1
                    ' Compute pixel brightness (i.e. total of Red, Green, and Blue values)
                    pixelTotal = CInt(sourceBuffer(sourceIndex + 1)) + CInt(sourceBuffer(sourceIndex + 2)) + CInt(sourceBuffer(sourceIndex + 3))
                    If pixelTotal > threshold Then
                        destinationValue += CByte(pixelValue)
                    End If
                    If pixelValue = 1 Then
                        destinationBuffer(destinationIndex) = destinationValue
                        destinationIndex += 1
                        destinationValue = 0
                        pixelValue = 128
                    Else
                        pixelValue >>= 1
                    End If
                    sourceIndex += 4
                Next
                If pixelValue <> 128 Then
                    destinationBuffer(destinationIndex) = destinationValue
                End If
            Next
            ' Copy binary image data to destination bitmap
            Marshal.Copy(destinationBuffer, 0, destinationData.Scan0, imageSize)
            ' Unlock destination bitmap
            destination.UnlockBits(destinationData)
            ' Return
            Return destination
        End Function

        Private Shared Function getCodecForString(ByVal Type As String) As ImageCodecInfo
            Dim info() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
            For i As Integer = 0 To info.Length - 1
                Dim EnumName As String = Type.ToString()
                If info(i).FormatDescription.ToString() = EnumName Then
                    Return info(i)
                End If
            Next
            Return Nothing
        End Function

#End Region

    End Class

End Namespace