Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO

Namespace BarCode

    ' Enumeraciones
    Public Enum AlignType
        Left
        Center
        Right
    End Enum

    Public Enum BarCodeWeight As Byte
        Small = 1
        Medium = 2
        Large = 3
    End Enum

    Public Enum BarCodeTypeType
        Code39
        EAN128
    End Enum

    <Xml.Serialization.XmlTypeAttribute([Namespace]:="http://slyg.com.co/miharu/BarCodeControl/")> _
    Public Class BarCodeControl

#Region " Declaraciones "

        Private _Align As AlignType = AlignType.Center
        Private _BarCode As String = "1234567890"
        Private _LeftMargin As Integer = 10
        Private _TopMargin As Integer = 10
        Private _BarCodeHeight As Integer = 50
        Private _ShowHeader As Boolean
        Private _ShowFooter As Boolean
        Private _HeaderText As String = "Header"
        Private _Weight As BarCodeWeight = BarCodeWeight.Small
        Private _HeaderFont As New Font("Courier", 18)
        Private _FooterFont As New Font("Courier", 8)
        Private _BarCodeType As BarCodeTypeType = BarCodeTypeType.Code39
        Private _FooterLines As New List(Of FooterLineItem)
        Private _FooterColumns As Integer = 1
        Private _AutoPrint As Boolean

#End Region

#Region " Propiedades "

        Public Property Align() As AlignType
            Get
                Return _Align
            End Get
            Set(ByVal value As AlignType)
                _Align = value
                MainPanel.Invalidate()
            End Set
        End Property

        Public Property BarCode() As String
            Get
                Return _BarCode
            End Get
            Set(ByVal value As String)
                _BarCode = value.ToUpper()
                MainPanel.Invalidate()
            End Set
        End Property

        Public Property BarCodeHeight() As Integer
            Get
                Return _BarCodeHeight
            End Get
            Set(ByVal value As Integer)
                _BarCodeHeight = value
                MainPanel.Invalidate()
            End Set
        End Property

        Public Property LeftMargin() As Integer
            Get
                Return _LeftMargin
            End Get
            Set(ByVal value As Integer)
                _LeftMargin = value
                MainPanel.Invalidate()
            End Set
        End Property

        Public Property TopMargin() As Integer
            Get
                Return _TopMargin
            End Get
            Set(ByVal value As Integer)
                _TopMargin = value
                MainPanel.Invalidate()
            End Set
        End Property

        Public Property ShowHeader() As Boolean
            Get
                Return _ShowHeader
            End Get
            Set(ByVal value As Boolean)
                _ShowHeader = value
                MainPanel.Invalidate()
            End Set
        End Property

        Public Property ShowFooter() As Boolean
            Get
                Return _ShowFooter
            End Get
            Set(ByVal value As Boolean)
                _ShowFooter = value
                MainPanel.Invalidate()
            End Set
        End Property

        Public Property HeaderText() As String
            Get
                Return _HeaderText
            End Get
            Set(ByVal value As String)
                _HeaderText = value
                MainPanel.Invalidate()
            End Set
        End Property

        Public Property Weight() As BarCodeWeight
            Get
                Return _Weight
            End Get
            Set(ByVal value As BarCodeWeight)
                _Weight = value
                MainPanel.Invalidate()
            End Set
        End Property

        Public Property HeaderFont() As Font
            Get
                Return _HeaderFont
            End Get
            Set(ByVal value As Font)
                _HeaderFont = value
                MainPanel.Invalidate()
            End Set
        End Property

        Public Property FooterFont() As Font
            Get
                Return _FooterFont
            End Get
            Set(ByVal value As Font)
                _FooterFont = value
                MainPanel.Invalidate()
            End Set
        End Property

        <Xml.Serialization.XmlElement()> _
        Public Property FooterLines() As List(Of FooterLineItem)
            Get
                Return _FooterLines
            End Get
            Set(ByVal Value As List(Of FooterLineItem))
                If Value IsNot Nothing Then
                    _FooterLines = Value
                    MainPanel.Invalidate()
                End If
            End Set
        End Property

        Public Property FooterLinesString() As String
            Get
                Return getFooterLinesString()
            End Get
            Set(ByVal Value As String)
                setFooterLinesString(Value)
            End Set
        End Property

        Public Property FooterColumns() As Integer
            Get
                Return _FooterColumns
            End Get
            Set(ByVal value As Integer)
                If _FooterColumns < 1 Then
                    _FooterColumns = 1
                ElseIf _FooterColumns > 4 Then
                    _FooterColumns = 4
                Else
                    _FooterColumns = value
                End If
            End Set
        End Property

        Public Property BarCodeType() As BarCodeTypeType
            Get
                Return _BarCodeType
            End Get
            Set(ByVal Value As BarCodeTypeType)
                _BarCodeType = Value
            End Set
        End Property

        Public Property AutoPrint() As Boolean
            Get
                Return _AutoPrint
            End Get
            Set(ByVal value As Boolean)
                _AutoPrint = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub BarCodeControl_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If _AutoPrint Then PrintAuto()
        End Sub

        Private Sub BarCodeControl_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize
            MainPanel.Invalidate()
        End Sub

        Private Sub MainPanel_Paint(ByVal sender As Object, ByVal e As Windows.Forms.PaintEventArgs) Handles MainPanel.Paint
            Dim BarCodeImage As Bitmap = Nothing

            Select Case _BarCodeType
                Case BarCodeTypeType.Code39
                    BarCodeImage = Code39Generator.getCBarrasImage(_BarCode, MainPanel.Width - 10, _BarCodeHeight, _Weight, _Align)

                Case BarCodeTypeType.EAN128
                    BarCodeImage = EAN128Generator.getCBarrasImage(_BarCode, MainPanel.Width - 10, _BarCodeHeight, _Weight, _Align)

            End Select

            Dim yTop As Integer

            Dim hSize As SizeF = e.Graphics.MeasureString(_HeaderText, _HeaderFont)
            Dim fSize As SizeF = e.Graphics.MeasureString(_BarCode, _FooterFont)

            Dim headerX As Integer
            Dim footerX As Integer

            If (_Align = AlignType.Left) Then
                headerX = _LeftMargin
                footerX = _LeftMargin

            ElseIf (_Align = AlignType.Center) Then
                headerX = CInt((Width - CInt(hSize.Width)) / 2)
                footerX = CInt((Width - CInt(fSize.Width)) / 2)

            Else
                headerX = Width - CInt(hSize.Width) - _LeftMargin
                footerX = Width - CInt(fSize.Width) - _LeftMargin

            End If

            If (_ShowHeader) Then
                yTop = CInt(hSize.Height) + _TopMargin
                e.Graphics.DrawString(_HeaderText, _HeaderFont, Brushes.Black, headerX, _TopMargin)

            Else
                yTop = _TopMargin
            End If


            ' Dibujar CBarras
            e.Graphics.DrawImage(BarCodeImage, 5, yTop)

            yTop += _BarCodeHeight

            If _ShowFooter Then
                e.Graphics.DrawString(_BarCode, _FooterFont, Brushes.Black, footerX, yTop)
            End If

            Dim FooterLineRectangle As New Rectangle(0, 0, CInt((Width - _LeftMargin) / _FooterColumns), CInt(fSize.Height + 3))

            For i As Integer = 1 To _FooterColumns
                Dim FooterLineY As Integer = yTop + 5

                For Each Linea In _FooterLines
                    If Linea.Column = i Then
                        FooterLineY = CInt(FooterLineY + fSize.Height + 3)

                        FooterLineRectangle.X = (i - 1) * FooterLineRectangle.Width
                        FooterLineRectangle.Y = FooterLineY
                        e.Graphics.DrawString(Linea.Text, _FooterFont, Brushes.Black, FooterLineRectangle)
                    End If
                Next
            Next

        End Sub

        Private Sub BarCodePrintDocument_PrintPage(ByVal sender As Object, ByVal e As Printing.PrintPageEventArgs) Handles BarCodePrintDocument.PrintPage
            MainPanel_Paint(sender, New PaintEventArgs(e.Graphics, Me.ClientRectangle))
        End Sub

        Private Sub ImprimirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ImprimirToolStripMenuItem.Click
            PrintAuto()
        End Sub

#End Region

#Region " Metodos "

        Public Sub New()
            'El Diseñador de Windows Forms requiere esta llamada.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            '_FileIOPermission = New FileIOPermission(PermissionState.Unrestricted)
            '_FileIOPermission.Assert()
        End Sub

        'Protected Overrides Sub Finalize()
        '    MyBase.Finalize()

        '    'FileIOPermission.RevertAssert()
        'End Sub

        Public Sub SaveImage(ByVal FileName As String, ByVal Format As Imaging.ImageFormat)
            Dim bmp As New Bitmap(Width, Height, Imaging.PixelFormat.Format32bppArgb)

            Dim g As Graphics = Graphics.FromImage(bmp)
            g.FillRectangle(Brushes.White, 0, 0, Width, Height)

            MainPanel_Paint(Nothing, New PaintEventArgs(g, Me.ClientRectangle))

            bmp.Save(FileName, Format)
        End Sub

        Public Function GetImage(ByVal Format As Imaging.ImageFormat) As Byte()
            Dim bmp As New Bitmap(Width, Height, Imaging.PixelFormat.Format32bppArgb)

            Dim g As Graphics = Graphics.FromImage(bmp)
            g.FillRectangle(Brushes.White, 0, 0, Width, Height)

            MainPanel_Paint(Nothing, New PaintEventArgs(g, Me.ClientRectangle))

            Dim imageStream = New MemoryStream()

            bmp.Save(imageStream, Format)

            Return imageStream.GetBuffer()
        End Function

        Public Sub PrintAuto()
            Dim pd As New PrintDialog
            pd.Document = BarCodePrintDocument

            If pd.ShowDialog() = DialogResult.OK Then
                BarCodePrintDocument.PrinterSettings = pd.PrinterSettings
                BarCodePrintDocument.DefaultPageSettings.Margins.Left = 5
                BarCodePrintDocument.DefaultPageSettings.Margins.Right = 5
                BarCodePrintDocument.DefaultPageSettings.Margins.Top = 5
                BarCodePrintDocument.DefaultPageSettings.Margins.Bottom = 5

                Dim objPrintPreviewDialog As New PrintPreviewDialog

                objPrintPreviewDialog.Document = BarCodePrintDocument
                objPrintPreviewDialog.ShowDialog()
            End If
        End Sub

        Public Sub Print()
            BarCodePrintDocument.Print()
        End Sub

        Public Sub Print(ByVal sender As Object, ByVal g As Graphics)
            MainPanel_Paint(sender, New PaintEventArgs(g, Me.ClientRectangle))
        End Sub

        Private Sub setFooterLinesString(ByVal nValue As String)
            Try
                Dim lineas = nValue.Split(";"c)

                _FooterLines.Clear()

                For Each linea In lineas
                    Dim Partes = linea.Split("|"c)

                    If Partes.Length = 2 Then
                        If (IsNumeric(Partes(1))) Then
                            _FooterLines.Add(New FooterLineItem(Partes(0), CInt(Partes(1))))
                        End If
                    End If
                Next
            Catch : End Try
        End Sub

        Public Sub FooterLinesClear()
            Me._FooterLines.Clear()
        End Sub

        Public Sub FooterLinesAdd(line As FooterLineItem)
            Me._FooterLines.Add(line)
        End Sub

#End Region

#Region " Funciones "

        Private Function getFooterLinesString() As String
            Try

                Dim value As String = ""

                For Each Line In _FooterLines
                    value = Line.Text & "|" & Line.Column & ";"
                Next

                value = value.TrimEnd(";"c)

                Return value
            Catch ex As Exception
                Return ""
            End Try

        End Function

#End Region

    End Class

End Namespace

