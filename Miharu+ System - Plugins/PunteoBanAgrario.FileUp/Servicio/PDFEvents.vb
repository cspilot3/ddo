Imports iTextSharp.text.pdf
Imports iTextSharp.text

Public Class PDFEvents
    Inherits PdfPageEventHelper

    Public Overrides Sub OnStartPage(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document)
        Dim ch As New Chunk("This is my Stack Overflow Header on page ")
        
        document.Add(ch)
    End Sub


End Class
