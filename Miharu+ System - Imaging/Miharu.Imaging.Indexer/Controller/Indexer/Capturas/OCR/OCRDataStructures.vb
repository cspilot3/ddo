Imports System.Drawing


Namespace Controller.Indexer.Capturas.OCR

    Public Class OCRDataStructures

        Public Class WordInfo
            Public Property Word As String
            Public Property boundingBox As Rectangle
            Public Property Confidence As Integer
        End Class

        Public Class JsonDataDictionaryCleanData
            Public Linea As Integer
            Public Entidad As Integer
            Public Documento As Integer
            Public Campo As Integer
            Public cellDataConfidence As New Dictionary(Of Integer, View.Indexacion.TableInputControl.CleanedTableDataStructure)
        End Class

        ''' <summary>
        ''' Estrucutura para capturar del JSON los datos del rectangulo principal
        ''' </summary>
        ''' <remarks></remarks>
        Public Class TableBoundingBox
            Public Property height As Integer
            Public Property width As Integer
            Public Property x As Integer
            Public Property y As Integer
        End Class

        ''' <summary>
        ''' Estrucutura principal para tomar los datos obtenidos de la deteccion de tablas
        ''' </summary>
        ''' <remarks></remarks>
        Public Class TableStructure
            Public Property column_lines As List(Of List(Of Integer()))
            Public Property row_lines As List(Of List(Of Integer()))
            Public Property table_bounding_box As TableBoundingBox
        End Class

        Public Class TableStructureWrapper
            Public Property table_structure As List(Of TableStructure)
        End Class

        'Estructura para el Json que se enviara para realizar el OCR al Rectangulo dibujado
        Public Class DataStructureRectangleOCR
            Public Property ImageBytes As Byte()                                        ' Almacena los Bytes correspondiente a la imagen
            Public Property RectangleDataOCR As Rectangle                               ' Almacena las coordenadas del rectangulo para realizar el OCR
        End Class

        ' Clase para representar el JSON completo List
        Public Class ResultadosJSONList
            Public Property dataOCR As List(Of String)
        End Class

        'Estructura para el Json que se enviara para realizar la deteccion de la tabla presente en la imagen
        Public Class DataStructureImageBytes
            Public Property ImageBytes As Byte()                                        ' Almacena los Bytes correspondiente a la imagen
        End Class

        'Estructura para el Json que se enviara para realizar el OCR a la tabla
        Public Class DataStructureOCRTableRectangle
            Public Property ImageBytes As Byte()                                        ' Almacena los Bytes correspondiente a la imagen
            Public Property CoordinatesData As Dictionary(Of Integer, List(Of Rectangle)) ' Almacena el diccionario que contiene cada uno d elas coordenadas de las celdas
        End Class

        Public Class TableDataStructure
            Public Property verticalLines As List(Of Point())
            Public Property horizontalLines As List(Of Point())
            Public Property mainRectangleTable As Rectangle
        End Class

        ' Clase para sereliezar el json con el pth del archivo HOCR
        Public Class PathHOCR
            Public Property filePath As String
        End Class

        ' Clase para desserielizar el json con el contenido del archivo HOCR
        Public Class DataStructureFileHOCR
            Public Property fileContentHOCR As String                                     ' Almacena los Bytes correspondiente a la imagen
        End Class

    End Class

End Namespace
