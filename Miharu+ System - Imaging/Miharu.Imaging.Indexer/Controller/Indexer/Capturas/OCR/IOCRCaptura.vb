Imports System.Drawing


Namespace Controller.Indexer.Capturas.OCR

    Public Interface IOCRCaptura

        Sub SetController(nController As Object)
        Sub SetExpediente(nExpediente As Integer)
        Sub SetFolder(nFolder As Integer)
        Sub SetFile(nFile As Integer)

        Function ConvertJsonToIntegerStringDictionary(responseText As String) As Dictionary(Of Integer, List(Of String()))
        Function ConvertJsonToDataXMLHOCR(responseText As String) As String
        Function ConvertJsonToStringList(responseText As String) As List(Of String)
        Function SendPathHOCRContent(pathHCOR As String) As String
        Function SendImageOCRTable(imagePicture As Image, rectangleTable As Rectangle, coordinatesDictionary As Dictionary(Of Integer, List(Of Rectangle))) As Dictionary(Of Integer, List(Of String()))
        Function ProcessOCRRectangle(_ImagePicture As Image, _Rectangle As Rectangle) As List(Of String)
        Function ProcessOCRTable(_ImagePicture As Image, _RectangleTable As Rectangle, _CoordinatesDictionary As Dictionary(Of Integer, List(Of Rectangle))) As Dictionary(Of Integer, List(Of String()))
        Function ExtractTableLinesFromImage(imagePicture As Image, Rectangle As Rectangle) As OCRDataStructures.TableDataStructure
        Function SendCleanColumnDataOCR(_SelectedTableInputControl As View.Indexacion.TableInputControl) As Dictionary(Of Integer, List(Of String()))
        Function CalculateOverlapPercentage(wordBoundingBox As Rectangle, areaBoundingBox As Rectangle) As Double
        Function NormalizeText(text As String) As String
        Function RemoveDiacritics(input As String) As String

    End Interface

End Namespace
