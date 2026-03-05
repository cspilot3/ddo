Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO

Public Class PDFManager

#Region " Constructores "
    Public Sub New()
    End Sub

    Public Sub New(ByVal data As DataTable)
        _dataTable = data
    End Sub
#End Region

#Region " Declaraciones "

    Private _dataTable As DataTable = Nothing
    Private _Generacion As Boolean = False

    ''Resumen del Documento
    Private _msDocTitle As String = ""
    Private _msDocAuthor As String = ""
    Private _msDocSubject As String = ""
    Private _msDocKeywords As String = ""

    ''Opciones de vista del documento
    Private _mbView2PageLayout As Boolean = False
    Private _mbViewToolbar As Boolean = True
    Private _mbViewMenubar As Boolean = True
    Private _mbViewWindowUI As Boolean = True
    Private _mbViewResizeToFit As Boolean = True
    Private _mbViewCenterOnScreen As Boolean = True
    Private _menShowPaperSize As PaperSize = PaperSize.LetterUS

    ''Opciones de elementos a mostrar en el documento
    Private _mbShowTitle As Boolean = True
    Private _mbShowPageNumber As Boolean = True
    Private _mbShowWatermark As Boolean = True
    Private _msShowWatermarkText As String = "WATERMARK"
    Private _msShowWatermarkFile As String = "watermark.png"
    Private _mbShowLandscape As Boolean = True


    ''Opciones de Letra (para encabezado y detalle)
    Private _menBodyTypeFace As TypeFace = TypeFace.Arial
    Private _mfBodyTypeSize As Double = 10.0F
    Private _mbBodyTypeStyleBold As Boolean = False
    Private _mbBodyTypeStyleItalics As Boolean = False

    Private _menHeaderTypeFace As TypeFace = TypeFace.Arial
    Private _mfHeaderTypeSize As Double = 10.0F
    Private _mbHeaderTypeStyleBold As Boolean = True
    Private _mbHeaderTypeStyleItalics As Boolean = False

    ''Opciones de Permisos y cifrado
    Private _mbEncryptionNeeded As Boolean = False
    Private _msEncryptionPasswordOfCreator As String = ""
    Private _msEncryptionPasswordOfReader As String = ""
    Private _mbEncryptionStrong As Boolean = False

    Private _mbAllowPrinting As Boolean = True
    Private _mbAllowModifyContents As Boolean = True
    Private _mbAllowCopy As Boolean = True
    Private _mbAllowModifyAnnotations As Boolean = True
    Private _mbAllowFillIn As Boolean = True
    Private _mbAllowScreenReaders As Boolean = True
    Private _mbAllowAssembly As Boolean = True
    Private _mbAllowDegradedPrinting As Boolean = True
#End Region

#Region " Enumeraciones "
    Public Enum PaperSize
        LetterUS
        LegalUS
        A4
        NOTE
    End Enum

    Public Enum TypeFace
        Times
        Arial
        Courier
    End Enum

    Public Enum ViewLayout
        OnePage = PdfWriter.PageLayoutSinglePage
        TwoPage = PdfWriter.PageLayoutTwoColumnLeft
    End Enum

    Public Enum ViewMode
        PorDefecto
    End Enum
#End Region

#Region " Propiedades "

    Public Property ShowPaperSize() As PaperSize
        Get
            Return _menShowPaperSize
        End Get
        Set(ByVal value As PaperSize)
            _menShowPaperSize = value
        End Set
    End Property

    Public Property View2PageLayout() As Boolean
        Get
            Return _mbView2PageLayout
        End Get
        Set(ByVal value As Boolean)
            _mbView2PageLayout = value
        End Set
    End Property

    Public Property ViewMenubar() As Boolean
        Get
            Return _mbViewMenubar
        End Get
        Set(ByVal value As Boolean)
            _mbViewMenubar = value
        End Set
    End Property

    Public Property ViewToolbar() As Boolean
        Get
            Return _mbViewToolbar
        End Get
        Set(ByVal value As Boolean)
            _mbViewToolbar = value
        End Set
    End Property

    Public Property ViewWindowUI() As Boolean
        Get
            Return _mbViewWindowUI
        End Get
        Set(ByVal value As Boolean)
            _mbViewWindowUI = value
        End Set
    End Property

    Public Property ViewResizeToFit() As Boolean
        Get
            Return _mbViewResizeToFit
        End Get
        Set(ByVal value As Boolean)
            _mbViewResizeToFit = value
        End Set
    End Property

    Public Property ViewCenterOnScreen() As Boolean
        Get
            Return _mbViewCenterOnScreen
        End Get
        Set(ByVal value As Boolean)
            _mbViewCenterOnScreen = value
        End Set
    End Property

    Public Property ShowTitle() As Boolean
        Get
            Return _mbShowTitle
        End Get
        Set(ByVal value As Boolean)
            _mbShowTitle = value
        End Set
    End Property

    Public Property ShowPageNumber() As Boolean
        Get
            Return _mbShowPageNumber
        End Get
        Set(ByVal value As Boolean)
            _mbShowPageNumber = value
        End Set
    End Property

    Public Property ShowWatermark() As Boolean
        Get
            Return _mbShowWatermark
        End Get
        Set(ByVal value As Boolean)
            _mbShowWatermark = value
        End Set
    End Property

    Public Property ShowWatermarkText() As Boolean
        Get
            Return _msShowWatermarkText
        End Get
        Set(ByVal value As Boolean)
            _msShowWatermarkText = value
        End Set
    End Property

    Public Property ShowWatermarkFile() As Boolean
        Get
            Return _msShowWatermarkFile
        End Get
        Set(ByVal value As Boolean)
            _msShowWatermarkFile = value
        End Set
    End Property

    Public Property ShowLandscape() As Boolean
        Get
            Return _mbShowLandscape
        End Get
        Set(ByVal value As Boolean)
            _mbShowLandscape = value
        End Set
    End Property
#End Region

#Region "GENERARPDF"
    Public Function GenerarPdf(ByVal sNombrePdf As String) As Boolean
        _Generacion = False

        If Not File.Exists(sNombrePdf) Then
            Dim documento As Document = New Document(PageSize.LETTER, 20.0, 20.0, 20.0, 20.0)
            Try
                '1. Crear un obtejo del tipo Writer PDF
                Dim _writer As PdfWriter
                _writer = PdfWriter.GetInstance(documento, New FileStream(sNombrePdf, FileMode.Create))

                Dim Titulo, SubTitulo, Vacio As Phrase
                Dim c, d, e As PdfPCell
                Dim page As Rectangle = documento.PageSize
                Dim head As PdfPTable = New PdfPTable(1)
                head.TotalWidth = page.Width

                Titulo = New Phrase(14.0, "Titulo del Reporte")
                c = New PdfPCell(Titulo)
                c.Border = Rectangle.NO_BORDER
                c.VerticalAlignment = Element.ALIGN_TOP
                c.HorizontalAlignment = Element.ALIGN_CENTER
                Titulo.Font.Color = iTextSharp.text.BaseColor.BLUE
                Titulo.Font.Size = 14.0
                Titulo.Font.SetFamily(iTextSharp.text.Font.BOLDITALIC)

                SubTitulo = New Phrase("Prueba Subtitulo")
                d = New PdfPCell(SubTitulo)
                d.Border = Rectangle.NO_BORDER
                d.VerticalAlignment = Element.ALIGN_TOP
                d.HorizontalAlignment = Element.ALIGN_LEFT

                Vacio = New Phrase("")
                e = New PdfPCell(Vacio)
                e.Border = Rectangle.NO_BORDER
                e.VerticalAlignment = Element.ALIGN_TOP

                head.AddCell(c)
                head.AddCell(d)
                head.AddCell(e)
                head.AddCell(e)


                '2. Abrir el documento y luego agregar la data
                documento.Open()
                documento.Add(head)
                ''a partir de aca prueba
                'cb = _writer.DirectContent

                '' documento.NewPage()
                'cb.BeginText()
                'fuente = FontFactory.GetFont(FontFactory.TIMES_ROMAN, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL).BaseFont
                'cb.SetFontAndSize(fuente, 12)
                'cb.SetColorFill(iTextSharp.text.BaseColor.BLUE)
                'cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Informe Generico Titulo", 600, PageSize.LEDGER.Height - 60, 0)
                'cb.EndText()
                '_writer.Flush()

                Dim _dataDataTable As PdfPTable = getPDFTable(_dataTable)
                '_dataDataTable.CalculateHeightsFast()
                '_dataDataTable.SpacingAfter = 60.0
                documento.Add(_dataDataTable)

                documento.PageSize.Rotate()

                '3. Cerrar el documento
                documento.Close()
            Catch ex As Exception
                documento.Close()
                WriteErrorLog(ex.Message)
            End Try
        End If

        Return _Generacion
    End Function
#End Region

#Region "DATATABLE"
    Public Function getPDFTable(ByVal data As DataTable) As PdfPTable
        ''Crear la tabla y asignar sus propiedades
        Dim tabla As PdfPTable = New PdfPTable(data.Columns.Count)

        Try
            tabla.DefaultCell.Padding = 2
            tabla.WidthPercentage = 90

            tabla.DefaultCell.BorderWidth = 1
            tabla.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER


            ''Adicionar los nombres de las columnas
            For Each columna As DataColumn In data.Columns
                tabla.AddCell(columna.ColumnName)
            Next

            ''Adicionar la columna para finalizar el header del encabezado de la tabla
            tabla.HeaderRows = 1

            tabla.SplitRows = False
            tabla.DefaultCell.BorderWidth = 1

            ''Adicionar data
            Dim i As Integer = 1
            For Each row As DataRow In data.Rows
                'Adicionar fila por defecto
                If (i Mod 2 = 1) Then tabla.DefaultCell.GrayFill = 0.9F

                'Adicionar los datos de cada celda de la fila
                Dim x As Integer
                For x = 0 To row.ItemArray.Length - 1
                    tabla.AddCell(row(x).ToString())
                Next x

                If (i Mod 2 = 1) Then tabla.DefaultCell.GrayFill = 1.0F

                i += 1
            Next
        Catch ex As Exception
            WriteErrorLog(ex.Message)
        End Try
        Return tabla
    End Function

#End Region

#Region "GENERALES"
    'Registrar errores en Log
    Private Sub WriteErrorLog(ByVal nMessage As String)
        Try
            Dim sw As New StreamWriter(Program.AppDataPath & "log_GenerarPDF.txt", True)

            sw.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sw.WriteLine("Mensaje: " & nMessage)
            sw.WriteLine("--------------------------------------------------------------")
            sw.WriteLine("")

            sw.Flush()
            sw.Close()
        Catch ex As Exception
            Try : EventLog.WriteEntry(ex.Message, "") : Catch : End Try
        End Try

        System.Windows.Forms.Application.DoEvents()
    End Sub
#End Region

End Class
