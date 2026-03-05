Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports System.Windows.Forms
Imports Slyg.Tools
Imports System.IO
Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Text
Imports System.Drawing.Imaging
Imports Slyg.Tools.Imaging
Imports Newtonsoft.Json
Imports System.Net

Namespace Controller.Indexer.Capturas.OCR

    Public Class OCRCaptura
        Implements IOCRCaptura

#Region " Declaraciones "

        Private ReadOnly MinPercAcceptWordCrop As Double = 35.0
        Private _imageAdjustableMargin As Integer = 5              ' Margen adicional (en píxeles) aplicado a las imágenes recortadas antes de enviarlas a la API de OCR

        Private _Controller As Controller.IController

        Private Expediente As Integer
        Private Folder As Integer
        Private File As Integer

#End Region

#Region " Constructor"
        Public Sub New()

        End Sub
#End Region

#Region " Implementación IOCRCaptura "

        Public Sub SetController(nController As Object) Implements IOCRCaptura.SetController
            _Controller = CType(nController, Controller.IController)
        End Sub

        Public Sub SetExpediente(nExpediente As Integer) Implements IOCRCaptura.SetExpediente
            Expediente = nExpediente
        End Sub

        Public Sub SetFolder(nFolder As Integer) Implements IOCRCaptura.SetFolder
            Folder = nFolder
        End Sub

        Public Sub SetFile(nFile As Integer) Implements IOCRCaptura.SetFile
            File = nFile
        End Sub

        ''' <summary>
        ''' Convierte una cadena JSON en un diccionario de enteros a listas de arreglos de cadenas.
        ''' </summary>
        ''' <param name="responseText">La cadena JSON a convertir.</param>
        ''' <returns>Un diccionario de enteros a listas de arreglos de cadenas.</returns>
        Public Function ConvertJsonToIntegerStringDictionary(responseText As String) As Dictionary(Of Integer, List(Of String())) Implements IOCRCaptura.ConvertJsonToIntegerStringDictionary

            Dim dictionaryResult As New Dictionary(Of Integer, List(Of String()))                            ' Crear el diccionario final
            Dim jsonDict As Dictionary(Of String, List(Of List(Of String))) = JsonConvert.DeserializeObject(Of Dictionary(Of String, List(Of List(Of String))))(responseText)

            For Each keyValuePair In jsonDict                                              ' Iterar a través de las claves y valores en columnarOcrResults
                Dim key As Integer = Integer.Parse(keyValuePair.Key)                                         ' Convertir la clave a un entero
                Dim stringLists As New List(Of String())                                                     ' Convertir las listas de listas de cadenas en List(Of String())

                For Each dataStringListOCR In keyValuePair.Value

                    Dim stringArray As String() = dataStringListOCR.ToArray()                                ' Convertir la lista de cadenas en un arreglo de cadenas

                    If Not String.IsNullOrWhiteSpace(stringArray(0)) Then
                        stringArray(0) = stringArray(0).ToUpper
                    End If

                    stringLists.Add(stringArray)
                Next

                dictionaryResult.Add(key, stringLists)                                                      ' Agregar la lista de arreglos de cadenas al diccionario
            Next

            Return dictionaryResult                                                                          ' Devolver el diccionario resultante
        End Function

        ''' <summary>
        ''' Convierte una respuesta JSON en una cadena de datos HOCR.
        ''' </summary>
        ''' <param name="responseText">Texto de la respuesta JSON a ser convertida.</param>
        ''' <returns>Una cadena que contiene el contenido HOCR del archivo.</returns>
        Public Function ConvertJsonToDataXMLHOCR(responseText As String) As String Implements IOCRCaptura.ConvertJsonToDataXMLHOCR

            Dim stringHCORData As OCRDataStructures.DataStructureFileHOCR = JsonConvert.DeserializeObject(Of OCRDataStructures.DataStructureFileHOCR)(responseText)
            Dim fileHOCRData As String = stringHCORData.fileContentHOCR

            Return fileHOCRData
        End Function

        ''' <summary>
        ''' Convierte una cadena JSON en una lista de cadenas.
        ''' </summary>
        ''' <param name="responseText">La cadena JSON a convertir.</param>
        ''' <returns>Una lista de cadenas.</returns>
        Public Function ConvertJsonToStringList(responseText As String) As List(Of String) Implements IOCRCaptura.ConvertJsonToStringList

            Dim results As OCRDataStructures.ResultadosJSONList = JsonConvert.DeserializeObject(Of OCRDataStructures.ResultadosJSONList)(responseText)   ' Deserializar la respuesta JSON en la clase ResultadosJSON
            Return results.dataOCR
        End Function


        ''' <summary>
        ''' Envía la ruta de un archivo HOCR a un servicio para extraer su contenido y procesar la respuesta.
        ''' </summary>
        ''' <param name="pathHCOR">Ruta del archivo HOCR a ser procesado.</param>
        ''' <returns>El contenido procesado del archivo HOCR en formato XML o Nothing en caso de error.</returns>
        Public Function SendPathHOCRContent(pathHCOR As String) As String Implements IOCRCaptura.SendPathHOCRContent

            Try
                '' Envia la imagen con sus respectivas coordendas para extraer el OCR
                Dim jsonPayload As String = SerializeDataPathHOCRContent(pathHCOR) ' Crea y serializa los datos para la solicitud.
                Dim urlTableOCR As String = _Controller.IndexerDesktopGlobal.ConnectionURLStrings.GetFileHOCRContent            ' URL para realizar OCR TABLA
                Dim responseText As String = SendDataPostRequest(jsonPayload, urlTableOCR)                                   ' Realiza la solicitud POST.
                Dim dataResultOCRTable = ConvertJsonToDataXMLHOCR(responseText)                                  ' Procesa la respuesta.

                Return dataResultOCRTable
            Catch
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Envía una imagen para procesamiento OCR de una tabla, basado en coordenadas de rectángulos definidos por el usuario.
        ''' Realiza varios pasos de procesamiento, incluyendo eliminación de líneas, escalamiento de coordenadas y extracción de OCR.
        ''' </summary>
        ''' <param name="imagePicture">Imagen a procesar para OCR.</param>
        ''' <param name="rectangleTable">Área rectangular de la imagen que define la tabla.</param>
        ''' <param name="coordinatesDictionary">Diccionario de coordenadas de rectángulos por clave de columna.</param>
        ''' <returns>Diccionario que mapea cada clave de columna a una lista de strings con los valores OCR extraídos.</returns>
        Public Function SendImageOCRTable(imagePicture As Image, rectangleTable As Rectangle, coordinatesDictionary As Dictionary(Of Integer, List(Of Rectangle))) As Dictionary(Of Integer, List(Of String())) Implements IOCRCaptura.SendImageOCRTable

            Try
                If imagePicture IsNot Nothing Then
                    Dim adjustedRectangle As Rectangle = AdjustRectangleWithMargin(rectangleTable)
                    Dim croppedImageBytes() As Byte = CropImageToBytes(imagePicture, adjustedRectangle)
                    Dim grayscaleImageBytes() As Byte = ConvertBytesToGrayscale(croppedImageBytes)

                    ' Envia imagen para remover lineas 
                    Dim jsonPayloadimage As String = SerializeImageBytes(grayscaleImageBytes)                                         ' Crea y serializa los datos para la solicitud.
                    Dim urlDeleteLines As String = _Controller.IndexerDesktopGlobal.ConnectionURLStrings.DeleteLinesImage            ' URL para realizar OCR TABLA
                    Dim responseTextImage As String = SendDataPostRequest(jsonPayloadimage, urlDeleteLines)                                   ' Realiza la solicitud POST.
                    Dim removeLinesImageBytes() As Byte = DeserializeImageData(responseTextImage)
                    SaveImageToTemporaryFile(removeLinesImageBytes)

                    '' Declarar e inicializar el diccionario para almacenar las coordenadas escaladas
                    Dim scaledCoordinatesDictionary As New Dictionary(Of Integer, List(Of Rectangle))

                    ' Iterar a través de cada par clave-valor en el diccionario original
                    For Each kvp As KeyValuePair(Of Integer, List(Of Rectangle)) In coordinatesDictionary
                        Dim key As Integer = kvp.Key                                            ' Obtener la clave actual
                        Dim rectangleList As List(Of Rectangle) = kvp.Value                     ' Obtener la lista de puntos para la clave actual
                        Dim scaledRectangleList As List(Of Rectangle) = New List(Of Rectangle)

                        ' Iterar a través de cada punto en la lista
                        For Each currentRectangle In rectangleList
                            ' Escalar el rectángulo a los límites de la imagen
                            Dim scaledRectangle As Rectangle = ScaleRectangleToImageBounds(adjustedRectangle, currentRectangle, imagePicture)
                            scaledRectangleList.Add(scaledRectangle)
                        Next

                        scaledCoordinatesDictionary.Add(key, scaledRectangleList)               ' Agregar el arreglo de puntos escalados al diccionario de coordenadas escaladas
                    Next

                    '' Envia la imagen con sus respectivas coordendas para extraer el OCR
                    Dim jsonPayload As String = SerializeDataOCRTableRectangle(removeLinesImageBytes, scaledCoordinatesDictionary) ' Crea y serializa los datos para la solicitud.
                    Dim urlTableOCR As String = _Controller.IndexerDesktopGlobal.ConnectionURLStrings.CoordinateTable            ' URL para realizar OCR TABLA
                    Dim responseText As String = SendDataPostRequest(jsonPayload, urlTableOCR)                                   ' Realiza la solicitud POST.
                    Dim dataResultOCRTable = ConvertJsonToIntegerStringDictionary(responseText)                                  ' Procesa la respuesta.
                    Return dataResultOCRTable
                Else
                    Throw New ArgumentNullException("La imagen en ImagePictureBox es nula.")
                End If
            Catch
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Procesa una región rectangular de una imagen con OCR y retorna una lista de textos extraídos.
        ''' El método de procesamiento depende de los parámetros OCR.
        ''' </summary>
        ''' <param name="_ImagePicture">Imagen a procesar.</param>
        ''' <param name="_Rectangle">Área rectangular de la imagen a procesar.</param>
        ''' <returns>Lista de textos extraídos de la región especificada.</returns>
        Public Function ProcessOCRRectangle(_ImagePicture As Image, _Rectangle As Rectangle) As List(Of String) Implements IOCRCaptura.ProcessOCRRectangle
            Try

                If _Controller.IndexerImagingGlobal.proyectoOCRRow IsNot Nothing AndAlso _Controller.IndexerImagingGlobal.proyectoOCRRow.Usa_AWS_Textract Then

                    Dim hOCRFolioPath = BuildPathFileHOCR(CStr(Expediente), CStr(Folder), CStr(File), _Controller)

                    Dim stringHOCRAWS As String = SendPathHOCRContent(hOCRFolioPath)

                    If stringHOCRAWS Is Nothing Then
                        Return SendImageOCRRectangle(_ImagePicture, _Rectangle)
                    End If

                    Dim dataResultListOcr As List(Of String) = New List(Of String)()
                    Dim resultOCRTextract = GetExtractedStringHOCRWithinCrop(stringHOCRAWS, _Rectangle)
                    dataResultListOcr.Insert(0, resultOCRTextract)

                    Return dataResultListOcr
                Else
                    Return SendImageOCRRectangle(_ImagePicture, _Rectangle)
                End If

                Return New List(Of String)

            Catch ex As Exception
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Procesa una region rectangular tipo tabla dibujada por el usuario sobre una imagen.
        ''' El método de procesamiento depende de los parámetros OCR.
        ''' </summary>
        ''' <param name="_ImagePicture">Imagen a procesar.</param>
        ''' <param name="_RectangleTable">Área rectangular de la imagen a procesar.</param>
        ''' <param name="_CoordinatesDictionary">Coordendas de las diferentes columnas dibujadas por el usuario.</param>
        ''' <returns>Diccionario que mapea cada clave de columna a una lista de palabras y confianzas extraídas.</returns>
        Public Function ProcessOCRTable(_ImagePicture As Image, _RectangleTable As Rectangle, _CoordinatesDictionary As Dictionary(Of Integer, List(Of Rectangle))) As Dictionary(Of Integer, List(Of String())) Implements IOCRCaptura.ProcessOCRTable

            Try

                If _Controller.IndexerImagingGlobal.proyectoOCRRow IsNot Nothing AndAlso _Controller.IndexerImagingGlobal.proyectoOCRRow.Usa_AWS_Textract Then

                    Dim hOCRFolioPath = BuildPathFileHOCR(CStr(Expediente), CStr(Folder), CStr(File), _Controller)

                    Dim stringHOCRAWS As String = SendPathHOCRContent(hOCRFolioPath)

                    If stringHOCRAWS Is Nothing Then
                        Return SendImageOCRTable(_ImagePicture, _RectangleTable, _CoordinatesDictionary)
                    End If

                    Dim resultDictionaryOCRTextract = ExtractWordsAndConfidencesforColumns(stringHOCRAWS, _RectangleTable, _CoordinatesDictionary)
                    Return resultDictionaryOCRTextract

                Else
                    Return SendImageOCRTable(_ImagePicture, _RectangleTable, _CoordinatesDictionary)
                End If

                Return New Dictionary(Of Integer, List(Of String()))

            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Function ExtractTableLinesFromImage(imagePicture As Image, Rectangle As Rectangle) As OCRDataStructures.TableDataStructure Implements IOCRCaptura.ExtractTableLinesFromImage

            Try

                If imagePicture IsNot Nothing Then
                    ' TODO: agregar en un solo metodo y devolver grayscaleImageBytes
                    Dim adjustedRectangle As Rectangle = AdjustRectangleWithMargin(Rectangle)
                    Dim croppedImageBytes() As Byte = CropImageToBytes(imagePicture, adjustedRectangle)
                    Dim grayscaleImageBytes() As Byte = ConvertBytesToGrayscale(croppedImageBytes)
                    SaveImageToTemporaryFile(grayscaleImageBytes)                                                   ' Guarda la imagen en un archivo temporal.

                    ' TODO: organizar en un solo metodo, volver generico
                    Dim jsonPayload As String = SerializeImageBytes(grayscaleImageBytes)                            ' Crea y serializa los datos para la solicitud.
                    Dim urlTableOCR As String = _Controller.IndexerDesktopGlobal.ConnectionURLStrings.DetectTable   ' URL para realizar OCR TABLA
                    Dim responseText As String = SendDataPostRequest(jsonPayload, urlTableOCR)                      ' Realiza la solicitud POST.

                    Return ProcessJsonTableData(responseText, adjustedRectangle)
                Else
                    Throw New ArgumentNullException("La imagen en ImagePictureBox es nula.")
                End If
            Catch
                Throw
            End Try

        End Function

        Public Function SendCleanColumnDataOCR(_SelectedTableInputControl As TableInputControl) As Dictionary(Of Integer, List(Of String())) Implements IOCRCaptura.SendCleanColumnDataOCR

            Dim selectedTableControl = _SelectedTableInputControl

            If selectedTableControl IsNot Nothing AndAlso _Controller IsNot Nothing Then

                Dim extractedFileData = New OCRDataStructures.JsonDataDictionaryCleanData()
                With extractedFileData                                                                          ' Asignamos los valores que serán enviados en formato JSON para limpiar los campos configurados en las celdas.
                    .Linea = 1
                    .Entidad = _Controller.IndexerImagingGlobal.Entidad
                    .Documento = _Controller.CurrentDocumentFile.TipoDocumento
                    .Campo = selectedTableControl.CampoCaptura.id
                    .cellDataConfidence = selectedTableControl.DataCellOrderOCR
                End With

                ' Serializamos y envio Json para limpiar datos de la tabla 
                Dim jsonPayload As String = SerializeDataOCRDataTableOrder(extractedFileData)
                Dim urlTableOCR As String = _Controller.IndexerDesktopGlobal.ConnectionURLStrings.CleanDataTable ' URL para realizar OCR Rectangle
                Dim ocrTableResponse As String = SendDataPostRequest(jsonPayload, urlTableOCR)                      ' Realiza la solicitud POST.

                Return ConvertJsonToIntegerStringDictionary(ocrTableResponse)
            End If
        End Function

        Public Function CalculateOverlapPercentage(wordBoundingBox As Rectangle, areaBoundingBox As Rectangle) As Double Implements IOCRCaptura.CalculateOverlapPercentage
            Try
                ' Calcular el área de intersección entre los rectángulos
                Dim areaInterseccion As Double = Math.Max(0, Math.Min(wordBoundingBox.Right, areaBoundingBox.Right) - Math.Max(wordBoundingBox.Left, areaBoundingBox.Left)) *
                                                  Math.Max(0, Math.Min(wordBoundingBox.Bottom, areaBoundingBox.Bottom) - Math.Max(wordBoundingBox.Top, areaBoundingBox.Top))

                ' Calcular el área del rectángulo más pequeño
                Dim areaRectanguloMenor As Double = Math.Abs((wordBoundingBox.Right - wordBoundingBox.Left) * (wordBoundingBox.Bottom - wordBoundingBox.Top))
                Dim areaRectanguloMayor As Double = Math.Abs((areaBoundingBox.Right - areaBoundingBox.Left) * (areaBoundingBox.Bottom - areaBoundingBox.Top))

                ' Calcular el porcentaje de superposición
                Dim porcentaje As Double = (areaInterseccion / areaRectanguloMenor) * 100

                Return porcentaje
            Catch ex As Exception
                Throw
            End Try
        End Function

#Region " TextUtilitis "

        Public Function NormalizeText(text As String) As String Implements IOCRCaptura.NormalizeText
            Return RemoveDiacritics(text).ToLower()
        End Function

        ''' <summary>
        ''' Elimina los diacríticos de un texto utilizando la forma de normalización 'FormD'.
        ''' </summary>
        ''' <param name="input">Texto del cual eliminar los diacríticos.</param>
        Public Function RemoveDiacritics(input As String) As String Implements IOCRCaptura.RemoveDiacritics
            If String.IsNullOrEmpty(input) Then
                Return input
            End If

            Dim normalizedString As String = input.Normalize(System.Text.NormalizationForm.FormD)
            Dim stringBuilder As New System.Text.StringBuilder()

            For Each c As Char In normalizedString
                Dim unicodeCategory As Globalization.UnicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c)
                If unicodeCategory <> unicodeCategory.NonSpacingMark Then
                    stringBuilder.Append(c)
                End If
            Next

            Return stringBuilder.ToString().ToLower()
        End Function

#End Region

#End Region


#Region " Funciones "

#Region " Comunication Json "

        ' ''' <summary>
        ' ''' Serializa los datos de la tabla OCR en un formato JSON.
        ' ''' </summary>
        ' ''' <param name="dataTableOCR">Los datos de la tabla OCR a serializar.</param>
        ' ''' <returns>Una cadena JSON que representa los datos de la tabla OCR.</returns>
        Private Function SerializeDataOCRDataTableOrder(dataTableOCR As OCRDataStructures.JsonDataDictionaryCleanData) As String
            Return JsonConvert.SerializeObject(dataTableOCR)
        End Function

        ''' <summary>
        ''' Convierte una cadena JSON en un objeto TableStructureWrapper utilizando el formato especificado.
        ''' </summary>
        ''' <param name="jsonData">La cadena JSON que se convertirá en un objeto TableStructureWrapper.</param>
        ''' <returns>Un objeto TableStructureWrapper que contiene la estructura de datos deserializada desde la cadena JSON.</returns>
        Private Function ConvertJsonDataTable(jsonData As String) As OCRDataStructures.TableStructureWrapper
            Dim result As OCRDataStructures.TableStructureWrapper = JsonConvert.DeserializeObject(Of OCRDataStructures.TableStructureWrapper)(jsonData)
            Return result
        End Function

        ''' <summary>
        ''' Serializa los bytes de una imagen y las coordenadas de los rectángulos de la tabla en formato JSON.
        ''' </summary>
        ''' <param name="imageBytes">Los bytes de la imagen a serializar.</param>
        ''' <param name="coordinatesDictionary">El diccionario que contiene las coordenadas de los rectángulos de la tabla.</param>
        ''' <returns>Una cadena JSON que representa los bytes de la imagen y las coordenadas de los rectángulos de la tabla.</returns>
        Private Function SerializeDataOCRTableRectangle(imageBytes As Byte(), coordinatesDictionary As Dictionary(Of Integer, List(Of Rectangle))) As String

            Dim data As New OCRDataStructures.DataStructureOCRTableRectangle()
            data.ImageBytes = imageBytes
            data.CoordinatesData = coordinatesDictionary

            Return JsonConvert.SerializeObject(data)
        End Function

        ''' <summary>
        ''' Serializa la ruta de un archivo HOCR en formato JSON.
        ''' </summary>
        ''' <param name="_pathHOCR">Ruta del archivo HOCR a ser serializado.</param>
        ''' <returns>Una cadena JSON que representa la ruta del archivo HOCR.</returns>
        Private Function SerializeDataPathHOCRContent(_pathHOCR As String) As String

            Dim data As New OCRDataStructures.PathHOCR()
            data.filePath = _pathHOCR

            Return JsonConvert.SerializeObject(data)
        End Function


        ' Método para deserializar el JSON y obtener los bytes de la imagen
        Private Function DeserializeImageData(jsonString As String) As Byte()

            Dim imageData As OCRDataStructures.DataStructureImageBytes = JsonConvert.DeserializeObject(Of OCRDataStructures.DataStructureImageBytes)(jsonString)
            Dim imageBytes As Byte() = imageData.ImageBytes

            Return imageBytes
        End Function

        ''' <summary>
        ''' Serializa los datos de un OCR de rectángulo a formato JSON.
        ''' </summary>
        ''' <param name="imageBytes">Los bytes de la imagen en formato TIFF.</param>
        ''' <param name="dataRectangle">El objeto Rectangle para OCR.</param>
        ''' <returns>Una cadena JSON que contiene los datos serializados.</returns>
        ''' <remarks></remarks>
        Private Function SerializeDataOCRRectangle(imageBytes As Byte(), dataRectangle As Rectangle) As String

            Dim data As New OCRDataStructures.DataStructureRectangleOCR()
            data.ImageBytes = imageBytes
            data.RectangleDataOCR = dataRectangle

            Return JsonConvert.SerializeObject(data)
        End Function

        ''' <summary>
        ''' Serializa un array de bytes de una imagen en formato JSON.
        ''' </summary>
        ''' <param name="imageBytes">Los bytes de la imagen a serializar.</param>
        ''' <returns>Una cadena JSON que representa los bytes de la imagen.</returns>
        Private Function SerializeImageBytes(imageBytes As Byte()) As String

            Dim imageBytesData As New OCRDataStructures.DataStructureImageBytes()
            imageBytesData.ImageBytes = imageBytes

            Return JsonConvert.SerializeObject(imageBytesData)
        End Function

        ''' <summary>
        ''' Realiza una solicitud POST a una URL especificada con un JSON payload y devuelve la respuesta como una cadena.
        ''' </summary>
        ''' <param name="jsonPayload">El contenido JSON que se incluirá en la solicitud POST.</param>
        ''' <param name="url">La URL a la que se realizará la solicitud POST.</param>
        ''' <returns>La respuesta de la solicitud POST como una cadena o una cadena vacía si no se pudo obtener una respuesta.</returns>
        Private Function SendDataPostRequest(jsonPayload As String, url As String) As String

            ' Crea la solicitud HTTP con la URL especificada.
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"

            ' Agrega la cabecera Accept-Encoding para indicar la aceptación de compresión.
            'request.Headers.Add(HttpRequestHeader.Accept, "*/*")
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br")

            ' Escribe el JSON en el cuerpo de la solicitud.
            Using streamWriter As New StreamWriter(request.GetRequestStream())
                streamWriter.Write(jsonPayload)
            End Using

            ' Obtiene y devuelve la respuesta.
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Using reader As New StreamReader(response.GetResponseStream())
                        Return reader.ReadToEnd()
                    End Using
                End If
            End Using

            ' Si no se pudo obtener una respuesta, se devuelve una cadena vacía.
            Return String.Empty
        End Function


        Private Function ProcessJsonTableData(jsonData As String, boundingRectangle As Rectangle) As OCRDataStructures.TableDataStructure

            Dim DataTableRecibied As OCRDataStructures.TableDataStructure = New OCRDataStructures.TableDataStructure()

            Try
                Dim tableData = ConvertJsonDataTable(jsonData)

                If tableData.table_structure.Count = 1 Then

                    Dim tableStructure = tableData.table_structure(0)

                    Dim adjustedColumnLines As List(Of Point()) = ConvertIntegerListsToPointList(tableStructure.column_lines)
                    adjustedColumnLines = AddOffsetToPoints(adjustedColumnLines, boundingRectangle.X, boundingRectangle.Y)
                    DataTableRecibied.verticalLines = adjustedColumnLines

                    Dim adjustedRowLines As List(Of Point()) = ConvertIntegerListsToPointList(tableStructure.row_lines)
                    adjustedRowLines = AddOffsetToPoints(adjustedRowLines, boundingRectangle.X, boundingRectangle.Y)
                    DataTableRecibied.horizontalLines = adjustedRowLines

                    DataTableRecibied.mainRectangleTable = New Rectangle(
                        tableStructure.table_bounding_box.x + boundingRectangle.X,
                        tableStructure.table_bounding_box.y + boundingRectangle.Y,
                        tableStructure.table_bounding_box.width + boundingRectangle.X,
                        tableStructure.table_bounding_box.height + boundingRectangle.Y
                    )

                    Return DataTableRecibied
                Else
                    Return New OCRDataStructures.TableDataStructure()
                End If
            Catch ex As Exception
                Return New OCRDataStructures.TableDataStructure()
            End Try
        End Function

#End Region

#Region " Procesamiento imagen "
        ''' <summary>
        ''' Guarda una imagen en un archivo temporal en formato TIFF.
        ''' </summary>
        ''' <param name="bytes">Los bytes de la imagen en formato TIFF.</param>
        Private Sub SaveImageToTemporaryFile(bytes As Byte())

            ' Crea un MemoryStream a partir de los bytes de la imagen.
            Using mss As New MemoryStream(bytes)

                Dim imageR As Image = System.Drawing.Image.FromStream(mss)                              ' Carga la imagen desde el MemoryStream.
                Dim tempFilePath As String = Path.Combine(Path.GetTempPath(), "imagen_temporal.tif")    ' Define la ruta del archivo temporal para guardar la imagen.
                imageR.Save(tempFilePath, System.Drawing.Imaging.ImageFormat.Tiff)                      ' Guarda la imagen en el archivo temporal en formato TIFF.
                imageR.Dispose()                                                                        ' Libera los recursos utilizados por la imagen.
            End Using
        End Sub

        ''' <summary>
        ''' Convierte la imagen proporcionada a escala de grises y retorna los bytes resultantes.
        ''' </summary>
        ''' <param name="originalImage">Imagen original a convertir.</param>
        ''' <returns>Un arreglo de bytes que representa la imagen en escala de grises.</returns>
        Private Function ConvertImageToGrayscaleBytes(originalImage As Image) As Byte()

            ' Crea un nuevo objeto Bitmap en escala de grises
            Using grayscaleImage As New Bitmap(originalImage.Width, originalImage.Height)

                ' Configura el modo de dibujo para mejorar la calidad
                Using g As Graphics = Graphics.FromImage(grayscaleImage)
                    g.CompositingMode = Drawing2D.CompositingMode.SourceCopy                 ' Establece el modo de composición para copiar directamente los píxeles de origen
                    g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality          ' Establece la calidad de composición en alta para mejorar la calidad general
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic     ' Establece el modo de interpolación en alta calidad bicúbica para reducir artefactos
                    g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality                    ' Establece el modo de suavizado para mejorar la calidad de los bordes
                    g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality                ' Establece el modo de compensación de píxeles para obtener resultados de alta calidad

                    ' Configura el modo de dibujo para convertir a escala de grises
                    Dim attributes As New ImageAttributes()
                    attributes.SetColorMatrix(New ColorMatrix(New Single()() {
                        New Single() {0.299F, 0.299F, 0.299F, 0, 0},
                        New Single() {0.587F, 0.587F, 0.587F, 0, 0},
                        New Single() {0.114F, 0.114F, 0.114F, 0, 0},
                        New Single() {0, 0, 0, 1, 0},
                        New Single() {0, 0, 0, 0, 1}
                    }))

                    ' Dibuja la imagen original en el nuevo objeto Bitmap en escala de grises
                    g.DrawImage(originalImage, New Rectangle(0, 0, originalImage.Width, originalImage.Height), 0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, attributes)
                End Using

                ' Convierte la imagen en escala de grises a un arreglo de bytes
                Using msGrayscale As New MemoryStream()
                    grayscaleImage.Save(msGrayscale, ImageFormat.Tiff) ' cambiar el formato según necesidades
                    Return msGrayscale.ToArray()
                End Using

            End Using
        End Function

        ''' <summary>
        ''' Convierte el arreglo de bytes de una imagen a escala de grises y retorna los bytes resultantes.
        ''' </summary>
        ''' <param name="imageBytes">Arreglo de bytes que representa la imagen original.</param>
        ''' <returns>Un arreglo de bytes que representa la imagen en escala de grises.</returns>
        Private Function ConvertBytesToGrayscale(imageBytes As Byte()) As Byte()
            Try
                Using ms As New MemoryStream(imageBytes)
                    Dim originalImage As Image = CType(FreeImageAPI.FreeImageBitmap.FromStream(ms), Drawing.Image)  ' Usa FreeImageAPI para obtener la imagen original
                    Return ConvertImageToGrayscaleBytes(originalImage)
                End Using
            Catch ex As Exception
                Throw New Exception("Error al convertir a imagen de escala de grises. Detalles: " & ex.ToString, ex)
            End Try
        End Function

        ''' <summary>
        ''' Obtiene el codificador de imágenes asociado a un tipo MIME específico.
        ''' </summary>
        ''' <param name="mimeType"></param>
        ''' <returns>
        ''' Un objeto ImageCodecInfo que representa el codificador de imágenes correspondiente o Nothing si no se encuentra ninguno.
        ''' </returns>
        ''' <remarks>
        ''' Esta función busca entre los codificadores disponibles y devuelve el que coincide con el tipo MIME especificado.
        ''' Si no se encuentra un codificador correspondiente, devuelve Nothing.
        ''' </remarks>
        Private Function GetEncoderInfo(ByVal mimeType As String) As ImageCodecInfo

            Dim encoders() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()  ' Obtener la lista de codificadores de imágenes disponibles

            For Each encoder As ImageCodecInfo In encoders                        ' Iterar a través de los codificadores disponibles

                If encoder.MimeType = mimeType Then                               ' Comprobar si el tipo MIME del codificador coincide con el tipo MIME especificado
                    Return encoder                                                ' Devolver el codificador correspondiente
                End If
            Next
            Return Nothing
        End Function

        ''' <summary>
        ''' Convierte una imagen en bytes en formato TIFF sin compresión.
        ''' </summary>
        ''' <param name="image">La imagen que se va a convertir.</param>
        ''' <returns>Los bytes de la imagen en formato TIFF.</returns>
        Private Function ConvertImageToTIFFBytes(image As Image) As Byte()

            ' Crea un MemoryStream para almacenar la imagen en formato TIFF.
            Using ms As New MemoryStream()

                ' Obtiene el codificador de formato TIFF.
                Dim tiffEncoder As System.Drawing.Imaging.ImageCodecInfo = GetEncoderInfo("image/tiff")

                ' Obtiene el codificador de formato TIFF.
                Dim encoderParams As New System.Drawing.Imaging.EncoderParameters(1)
                encoderParams.Param(0) = New System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Compression, CLng(System.Drawing.Imaging.EncoderValue.CompressionNone))

                ' Guarda la imagen en el MemoryStream en formato TIFF sin compresión.
                image.Save(ms, tiffEncoder, encoderParams)

                ' Convierte el contenido del MemoryStream a un array de bytes y lo devuelve.
                Return ms.ToArray()
            End Using
        End Function


        ''' <summary>
        ''' Recorta una imagen según un rectángulo y devuelve los bytes resultantes en formato TIFF.
        ''' </summary>
        ''' <param name="image">La imagen original a recortar.</param>
        ''' <param name="rectangle">El rectángulo que define la región a recortar.</param>
        ''' <returns>Los bytes de la imagen recortada en formato TIFF.</returns>
        Private Function CropImageToBytes(image As Image, rectangle As Rectangle) As Byte()
            Dim croppedBitmap As New Bitmap(rectangle.Width, rectangle.Height)
            Using graphics As Graphics = graphics.FromImage(croppedBitmap)
                graphics.DrawImage(image, New Rectangle(0, 0, rectangle.Width, rectangle.Height), rectangle, GraphicsUnit.Pixel)
            End Using
            Return ConvertImageToTIFFBytes(croppedBitmap)
        End Function

#End Region

#Region " redimensionamiento datos "

        ''' <summary>
        ''' Añade un desplazamiento horizontal y vertical a cada punto en una lista de arreglos de puntos.
        ''' </summary>
        ''' <param name="pointsList">La lista de arreglos de puntos a los que se les aplicará el desplazamiento.</param>
        ''' <param name="offsetX">El valor del desplazamiento horizontal que se sumará a la coordenada X de cada punto.</param>
        ''' <param name="offsetY">El valor del desplazamiento vertical que se sumará a la coordenada Y de cada punto.</param>
        ''' <returns>Una nueva lista de arreglos de puntos con los puntos desplazados según el desplazamiento especificado.</returns>
        Private Function AddOffsetToPoints(pointsList As List(Of Point()), offsetX As Integer, offsetY As Integer) As List(Of Point())

            Dim adjustedPointsList As New List(Of Point())

            For Each arrayPoints As Point() In pointsList
                Dim newArrayPoints(arrayPoints.Length - 1) As Point
                For i As Integer = 0 To arrayPoints.Length - 1
                    Dim punto As Point = arrayPoints(i)
                    newArrayPoints(i) = New Point(punto.X + offsetX, punto.Y + offsetY)
                Next
                adjustedPointsList.Add(newArrayPoints)
            Next

            Return adjustedPointsList
        End Function


        ''' <summary>
        ''' Convierte una lista de listas de enteros en una lista de puntos.
        ''' Cada lista interna representa las coordenadas x e y de un punto.
        ''' </summary>
        ''' <param name="sourceList">La lista de listas de enteros que se convertirá en puntos.</param>
        ''' <returns>Una lista de puntos generada a partir de la lista de listas de enteros.</returns>
        Private Function ConvertIntegerListsToPointList(sourceList As List(Of List(Of Integer()))) As List(Of Point())

            Dim convertedPoints As New List(Of Point())

            For Each SubList In sourceList

                Dim currentPointList As New List(Of Point)

                For Each innerList In SubList
                    Dim newPoint As New Point(innerList(0), innerList(1))
                    currentPointList.Add(newPoint)
                Next
                Dim pointsArray As Point() = {currentPointList(0), currentPointList(1)}
                convertedPoints.Add(pointsArray)
            Next

            Return convertedPoints

        End Function


        ''' <summary>
        ''' Escala un rectángulo para que se ajuste a los límites de una imagen, considerando una posible escala en los ejes X e Y.
        ''' </summary>
        ''' <param name="adjustedRectangle">El rectángulo de referencia, que puede representar un área ajustada de la imagen.</param>
        ''' <param name="Rectangle">El rectángulo original que se va a escalar.</param>
        ''' <param name="image">La imagen a la que se ajustará el rectángulo.</param>
        ''' <param name="scaledX">El factor de escala en el eje X. Por defecto, es 1.0.</param>
        ''' <param name="scaledY">El factor de escala en el eje Y. Por defecto, es 1.0.</param>
        ''' <returns>Un nuevo rectángulo escalado.</returns>
        Private Function ScaleRectangleToImageBounds(adjustedRectangle As Rectangle, Rectangle As Rectangle, image As Image, Optional scaledX As Double = 1.0, Optional scaledY As Double = 1.0) As Rectangle
            Dim scaledRectangle As New Rectangle()
            scaledRectangle.X = CInt((Rectangle.X - adjustedRectangle.X) * scaledX)
            scaledRectangle.Y = CInt((Rectangle.Y - adjustedRectangle.Y) * scaledY)
            scaledRectangle.Width = CInt(Math.Min(Rectangle.Width * scaledX, (image.Width - Rectangle.X) * scaledX))
            scaledRectangle.Height = CInt(Math.Min(Rectangle.Height * scaledY, (image.Height - Rectangle.Y) * scaledY))
            Return scaledRectangle
        End Function

        ''' <summary>
        ''' Ajusta un rectángulo con un margen adicional.
        ''' </summary>
        ''' <param name="Rectangle">El rectángulo original.</param>
        ''' <returns>Un nuevo rectángulo ajustado con margen adicional.</returns>
        Private Function AdjustRectangleWithMargin(Rectangle As Rectangle) As Rectangle
            Dim adjustedRectangle As New Rectangle()
            adjustedRectangle.X = Rectangle.X - _imageAdjustableMargin
            adjustedRectangle.Y = Rectangle.Y - _imageAdjustableMargin
            adjustedRectangle.Width = Rectangle.Width + _imageAdjustableMargin * 2
            adjustedRectangle.Height = Rectangle.Height + _imageAdjustableMargin * 2
            Return adjustedRectangle
        End Function

#End Region


        ''' <summary>
        ''' Construye y devuelve la ruta completa de un archivo HOCR basada en el expediente, carpeta, archivo y el índice de imagen actual del controlador.
        ''' </summary>
        ''' <param name="expediente">Número o código del expediente</param>
        ''' <param name="folder">Nombre de la carpeta donde se encuentra el archivo</param>
        ''' <param name="file">Nombre del archivo dentro del expediente y carpeta</param>
        ''' <param name="controller">Objeto que contiene información del índice de la imagen actual</param>
        ''' <returns>Ruta completa del archivo HOCR generado.</returns>
        Private Function BuildPathFileHOCR(ByVal expediente As String, ByVal folder As String, ByVal file As String, ByVal controller As Object) As String
            Dim nameFileData As String = expediente & folder & file
            Dim nameHOCRImage As String = "HOCRTokePage" & (_Controller.CurrentImageIndex + 1) & ".txt"
            Dim hOCRFolioPath As String = _Controller.IndexerDesktopGlobal.OCRParameterStrings.PathOUTData & "\" & nameFileData & "\" & nameHOCRImage
            Return hOCRFolioPath
        End Function


        ''' <summary>
        ''' Envia y procesa la solictud para extraer el texto que contiene la imagen con Tesseract
        ''' </summary>
        ''' <param name="imagePicture">imagen a extraer caracteres</param>
        ''' <param name="Rectangle">cooordenada de la imagen en donde se extraerá la información</param>
        ''' <returns>devuelve el json con la respuesta de la data extraida por tesseract</returns>
        Private Function SendImageOCRRectangle(imagePicture As Image, Rectangle As Rectangle) As List(Of String)

            Try
                If imagePicture IsNot Nothing Then

                    Dim adjustedRectangle As Rectangle = AdjustRectangleWithMargin(Rectangle)
                    Dim croppedImageBytes() As Byte = CropImageToBytes(imagePicture, adjustedRectangle)
                    Dim grayscaleImageBytes() As Byte = ConvertBytesToGrayscale(croppedImageBytes)
                    ''Dim preprocessedImageBytes() As Byte = PreprocessImage(grayscaleImageBytes)
                    SaveImageToTemporaryFile(grayscaleImageBytes)                                                               ' Guarda la imagen en un archivo temporal.

                    ''Dim dimensionsCroppedImageBytes = GetImageDimensions(croppedImageBytes)
                    ''Dim dimensionsPreprocessedImageBytes = GetImageDimensions(preprocessedImageBytes)

                    ' '' Calcular el factor de escala
                    ''Dim scaleX As Double = dimensionsPreprocessedImageBytes.Item1 / dimensionsCroppedImageBytes.Item1
                    ''Dim scaleY As Double = dimensionsPreprocessedImageBytes.Item2 / dimensionsCroppedImageBytes.Item2

                    '' TODO: Agregar y optimizar el webapi para que lea la imagen mejorada preprocessedImageBytes
                    Dim scaledRectangle As Rectangle = ScaleRectangleToImageBounds(adjustedRectangle, Rectangle, imagePicture)
                    Dim jsonPayload As String = SerializeDataOCRRectangle(grayscaleImageBytes, scaledRectangle)                 ' Crea y serializa los datos para la solicitud.
                    Dim urlTableOCR As String = _Controller.IndexerDesktopGlobal.ConnectionURLStrings.Rectangle                 ' URL para realizar OCR Rectangle
                    Dim responseText As String = SendDataPostRequest(jsonPayload, urlTableOCR)
                    Dim dataResultListOcr As List(Of String) = ConvertJsonToStringList(responseText) ' Realiza la solicitud POST.

                    ' convertimos en mayusculas el texto
                    If dataResultListOcr.Count > 0 AndAlso Not String.IsNullOrWhiteSpace(dataResultListOcr(0)) Then
                        dataResultListOcr(0) = dataResultListOcr(0).ToUpper
                    End If

                    Return dataResultListOcr
                Else
                    Throw New ArgumentNullException("La imagen en ImagePictureBox es nula.")
                End If
            Catch
                Throw
            End Try

        End Function

        ''' <summary>
        ''' Extrae palabras y nivel deconfianzas para cada columna definida por rectángulos de coordenadas.
        ''' </summary>
        ''' <param name="stringHOCRAWS">Texto HOCR sin procesar para la extracción de palabras.</param>
        ''' <param name="_RectangleTable">Rectángulo de recorte para delimitar la búsqueda de palabras.</param>
        ''' <param name="_CoordinatesDictionary">Diccionario de coordenadas de rectángulos por clave de columna.</param>
        ''' <returns>Diccionario que mapea cada clave de columna a una lista de palabras y confianzas extraídas.</returns>
        Private Function ExtractWordsAndConfidencesforColumns(ByVal stringHOCRAWS As String, ByVal _RectangleTable As Rectangle, ByVal _CoordinatesDictionary As Dictionary(Of Integer, List(Of Rectangle))) As Dictionary(Of Integer, List(Of String()))

            Dim resultDictionaryOCRTextract As New Dictionary(Of Integer, List(Of String()))

            Try
                ' Verifica que los parámetros no sean Nothing y que el rectángulo tenga un ancho y alto válidos
                If String.IsNullOrWhiteSpace(stringHOCRAWS) OrElse (_RectangleTable.Height <= 0 OrElse _RectangleTable.Width <= 0) OrElse _CoordinatesDictionary Is Nothing Then
                    Return resultDictionaryOCRTextract
                End If

                Dim ListWordsInfoOCR = GetExtractedWordsInfoWithinCrop(stringHOCRAWS, _RectangleTable)           ' Obtener la lista de palabras y sus información desde el OCR

                ' Iterar a través de cada clave y valor en el diccionario de coordenadas
                For Each kvp As KeyValuePair(Of Integer, List(Of Rectangle)) In _CoordinatesDictionary
                    Dim key As Integer = kvp.Key                               ' Obtener la clave actual
                    Dim rectangleList As List(Of Rectangle) = kvp.Value        ' Obtener la lista de puntos para la clave actual

                    Dim resultColumnOCRTextract As New List(Of String())

                    ' Iterar a través de cada punto en la lista de rectángulos
                    For Each currentRectangle In rectangleList
                        Dim wordString As String = ""
                        Dim levelConfidence As Integer = 0
                        Dim index As Integer = 0
                        Dim listIndex As New List(Of Integer)

                        ' Iterar a través de cada palabra extraída del OCR
                        For Each WordInfo In ListWordsInfoOCR

                            Dim overlapPercentage As Double = CalculateOverlapPercentage(WordInfo.boundingBox, currentRectangle)                     ' Calcular el porcentaje de superposición entre la palabra y el rectángulo actual

                            ' Si el porcentaje de superposición es suficiente, procesar la palabra
                            If overlapPercentage >= 55.0 Then
                                wordString = If(String.IsNullOrWhiteSpace(wordString), WordInfo.Word, wordString & " " & WordInfo.Word)
                                levelConfidence = If(levelConfidence = 0, WordInfo.Confidence, CInt((levelConfidence + WordInfo.Confidence) / 2))
                                listIndex.Add(index)
                            End If

                            index += 1
                        Next

                        ' Agregar la palabra y la confianza a la lista resultante
                        Dim wordsColumn As String() = {wordString.ToUpper(), levelConfidence.ToString()}
                        resultColumnOCRTextract.Add(wordsColumn)

                        ' Ordenar y eliminar palabras en los índices especificados de la lista original
                        listIndex.Sort()
                        listIndex.Reverse()
                        For Each idx In listIndex
                            If idx >= 0 AndAlso idx < ListWordsInfoOCR.Count Then
                                ListWordsInfoOCR.RemoveAt(idx)
                            End If
                        Next
                    Next

                    ' Agregar la columna resultante al diccionario final
                    resultDictionaryOCRTextract.Add(key, resultColumnOCRTextract)
                Next

                Return resultDictionaryOCRTextract

            Catch ex As Exception
                Throw
            End Try
        End Function


#Region " procesamiento HOCR"


        ''' <summary>
        ''' Lee el contenido de un archivo HOCR basado en la configuración actual y retorna su contenido como una cadena.
        ''' </summary>
        ''' <returns>El contenido del archivo HOCR como una cadena.</returns>
        Private Function ReadHOCRFile(hOCRFolioPath As String) As String

            Dim stringHOCRAWS As String
            Try
                Using sr As New StreamReader(hOCRFolioPath)
                    stringHOCRAWS = sr.ReadToEnd() ' Leer todo el contenido del archivo y asignarlo a una variable
                End Using

                Return stringHOCRAWS

            Catch ex As Exception
                Throw
            End Try

        End Function


        ''' <summary>
        ''' Extrae información detallada de palabras dentro de una región recortada de un texto HOCR.
        ''' Utiliza un control WebBrowser para procesar HTML y obtener palabras con sus coordenadas y confianza.
        ''' </summary>
        ''' <param name="hocrText">Texto HOCR que contiene las palabras extraídas.</param>
        ''' <param name="croppedArea">Área rectangular que delimita la región recortada para buscar palabras.</param>
        ''' <returns>Lista de objetos WordInfo que contiene cada palabra, su rectángulo delimitador y confianza.</returns>
        Private Function GetExtractedWordsInfoWithinCrop(hocrText As String, croppedArea As Rectangle) As List(Of OCRDataStructures.WordInfo)

            Dim listofWords As New List(Of OCRDataStructures.WordInfo)

            Try
                ' Crear un control WebBrowser para procesar el HTML
                Dim webBrowser As New WebBrowser()
                webBrowser.DocumentText = hocrText

                ' Esperar a que el documento se cargue
                While webBrowser.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While

                ' Asegurarse de que el documento se haya cargado correctamente
                Dim doc As HtmlDocument = webBrowser.Document
                If doc Is Nothing Then
                    Throw New Exception("El documento HTML no se cargó correctamente.")
                End If

                Dim spanElements As HtmlElementCollection = doc.GetElementsByTagName("span")

                For Each wordNode As HtmlElement In spanElements
                    If wordNode.GetAttribute("className").Contains("ocrx_word") Then
                        Dim word As String = wordNode.InnerText

                        If Not String.IsNullOrWhiteSpace(word) Then
                            Dim boundingBox As Rectangle = ParseBoundingBox(wordNode.GetAttribute("title"))
                            Dim confidence As Integer = CInt(ParseConfidence(wordNode.GetAttribute("title")))

                            If Not boundingBox.IsEmpty Then
                                Dim overlapPercentage As Double = CalculateOverlapPercentage(boundingBox, croppedArea)

                                If overlapPercentage >= MinPercAcceptWordCrop Then
                                    Dim wordNormalized As String = NormalizeText(word.Trim())

                                    Dim wordOCR = New OCRDataStructures.WordInfo()
                                    wordOCR.Word = wordNormalized
                                    wordOCR.boundingBox = boundingBox
                                    wordOCR.Confidence = confidence
                                    listofWords.Add(wordOCR)

                                End If
                            End If
                        End If
                    End If
                Next

                Return listofWords

            Catch ex As Exception
                Throw
            End Try

        End Function

        ''' <summary>
        ''' Extrae y devuelve las palabras contenidas dentro de un área recortada en un texto HOCR.
        ''' </summary>
        ''' <param name="hocrText">El texto HOCR que contiene las palabras y sus coordenadas.</param>
        ''' <param name="croppedArea">El área rectangular que delimita las palabras a extraer.</param>
        ''' <returns>
        ''' Una cadena que representa las palabras extraídas dentro del área recortada, organizadas y filtradas según los parámetros de confianza.
        ''' </returns>
        Private Function GetExtractedStringHOCRWithinCrop(hocrText As String, croppedArea As Rectangle) As String

            Dim stringOCRBuilder As New StringBuilder()
            Dim listofWords As New List(Of OCRDataStructures.WordInfo)

            Try
                ' Crear un control WebBrowser para procesar el HTML
                Dim webBrowser As New WebBrowser()
                webBrowser.DocumentText = hocrText

                ' Esperar a que el documento se cargue
                While webBrowser.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While

                ' Asegurarse de que el documento se haya cargado correctamente
                Dim doc As HtmlDocument = webBrowser.Document
                If doc Is Nothing Then
                    Throw New Exception("El documento HTML no se cargó correctamente.")
                End If

                Dim spanElements As HtmlElementCollection = doc.GetElementsByTagName("span")

                For Each wordNode As HtmlElement In spanElements
                    If wordNode.GetAttribute("className").Contains("ocrx_word") Then
                        Dim word As String = wordNode.InnerText

                        If Not String.IsNullOrWhiteSpace(word) Then
                            Dim boundingBox As Rectangle = ParseBoundingBox(wordNode.GetAttribute("title"))
                            Dim confidence As Integer = CInt(ParseConfidence(wordNode.GetAttribute("title")))

                            If Not boundingBox.IsEmpty Then
                                Dim overlapPercentage As Double = CalculateOverlapPercentage(boundingBox, croppedArea)

                                If overlapPercentage >= MinPercAcceptWordCrop Then
                                    Dim wordNormalized As String = NormalizeText(word.Trim())

                                    ' almacena las palabras que coinciden con el area marcada por el usuario
                                    Dim wordOCR = New OCRDataStructures.WordInfo()
                                    wordOCR.Word = wordNormalized
                                    wordOCR.boundingBox = boundingBox
                                    wordOCR.Confidence = confidence
                                    listofWords.Add(wordOCR)
                                End If
                            End If
                        End If
                    End If
                Next

                ' Verifica que tenga almenos una palabra, sino procede a crearla
                If (listofWords.Count = 0) Then
                    ' Crea una nueva palabra por defecto
                    Dim defaultWordInfo As New OCRDataStructures.WordInfo With {
                        .Word = "",
                        .boundingBox = New Rectangle(0, 0, 0, 0),
                        .Confidence = 0
                    }
                    listofWords.Add(defaultWordInfo)
                End If

                ' organiza las palabras en forma lectura Normal
                Dim wordinforOrder As List(Of OCRDataStructures.WordInfo) = OrganizeWords(listofWords)

                ' Evalua el nivel de confianza de cada palabra para almacenarla 
                For Each wordinfo As OCRDataStructures.WordInfo In wordinforOrder

                    If Not _Controller.IndexerDesktopGlobal.OCRParameterStrings.ApplyWordConfidence Then
                        stringOCRBuilder.Append(wordinfo.Word.ToUpper()).Append(" ")
                    Else
                        If wordinfo.Confidence >= _Controller.IndexerDesktopGlobal.OCRParameterStrings.MinWordConfidence Then
                            stringOCRBuilder.Append(wordinfo.Word.ToUpper()).Append(" ")
                        End If
                    End If
                Next

                Dim result As String = stringOCRBuilder.ToString().Trim()
                Return result

            Catch ex As Exception
                Throw
            End Try

        End Function


        ''' <summary>
        ''' Organiza una lista de palabras por su posición vertical y superposición significativa.
        ''' </summary>
        ''' <param name="words">La lista de palabras a organizar.</param>
        ''' <returns>
        ''' Una lista de WordInfo que representa las palabras organizadas por su posición vertical y superposición significativa.
        ''' </returns>
        Private Shared Function OrganizeWords(words As List(Of OCRDataStructures.WordInfo)) As List(Of OCRDataStructures.WordInfo)
            ' Constante para el umbral de superposición vertical
            Const VerticalOverlapThreshold As Double = 0.5

            ' Ordenar las palabras primero por su posición vertical
            Dim sortedWords = words.OrderBy(Function(w) w.boundingBox.Top).ToList()
            Dim organizedWords As New List(Of OCRDataStructures.WordInfo)
            Dim currentGroup As New List(Of OCRDataStructures.WordInfo)

            For Each word In sortedWords
                If currentGroup.Count = 0 Then
                    currentGroup.Add(word)
                Else
                    Dim previousWord = currentGroup.Last()
                    If VerticalOverlap(previousWord, word) > VerticalOverlapThreshold Then
                        ' La palabra actual se superpone significativamente con la anterior
                        currentGroup.Add(word)
                    Else
                        ' Ordenar y añadir el grupo actual a las palabras organizadas
                        organizedWords.AddRange(currentGroup.OrderBy(Function(w) w.boundingBox.Left))
                        currentGroup.Clear()
                        currentGroup.Add(word)
                    End If
                End If
            Next

            ' Añadir el último grupo
            If currentGroup.Any() Then
                organizedWords.AddRange(currentGroup.OrderBy(Function(w) w.boundingBox.Left))
            End If

            Return organizedWords
        End Function


        ''' <summary>
        ''' Calcula la relación de superposición vertical entre dos palabras.
        ''' </summary>
        ''' <param name="word1">La primera palabra para comparar.</param>
        ''' <param name="word2">La segunda palabra para comparar.</param>
        ''' <returns>
        ''' Un valor Double que representa la relación de superposición vertical entre las dos palabras.
        ''' El valor es la relación de la altura de la superposición con la altura total de las dos palabras.
        ''' Si la altura total es cero, devuelve cero.
        ''' </returns>
        Private Shared Function VerticalOverlap(word1 As OCRDataStructures.WordInfo, word2 As OCRDataStructures.WordInfo) As Double
            Dim overlapHeight As Double = Math.Min(word1.boundingBox.Bottom, word2.boundingBox.Bottom) -
                                          Math.Max(word1.boundingBox.Top, word2.boundingBox.Top)
            Dim totalHeight As Double = Math.Max(word1.boundingBox.Height, word2.boundingBox.Height)

            If totalHeight > 0 Then
                Return overlapHeight / totalHeight
            Else
                Return 0
            End If
        End Function



        Private Function ParseBoundingBox(title As String) As Rectangle

            Try
                Dim parts = title.Split(New String() {"bbox ", " "}, StringSplitOptions.RemoveEmptyEntries)

                If parts.Length >= 4 Then
                    Dim x As Integer = CleanNumber(parts(0))
                    Dim y As Integer = CleanNumber(parts(1))
                    Dim width As Integer = CleanNumber(parts(2)) - x
                    Dim height As Integer = CleanNumber(parts(3)) - y

                    Return New Rectangle(x, y, width, height)
                End If

                Return Rectangle.Empty
            Catch ex As Exception
                Throw
            End Try

        End Function

        Private Function CleanNumber(value As String) As Integer
            Dim cleanValue As String = Regex.Replace(value, "\D", "") ' Utilizamos una expresión regular para mantener solo los dígitos
            Return Integer.Parse(cleanValue)
        End Function

        Private Function ParseConfidence(title As String) As Integer

            Try
                Dim confidenceString = title.Split(New String() {"x_wconf "}, StringSplitOptions.RemoveEmptyEntries)(1).Split(";"c)(0)
                Return Integer.Parse(confidenceString)
            Catch ex As Exception
                Throw
            End Try
        End Function

#End Region



#End Region

    End Class

End Namespace
