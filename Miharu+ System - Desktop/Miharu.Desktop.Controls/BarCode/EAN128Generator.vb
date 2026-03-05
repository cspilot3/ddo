Imports System
Imports System.Text
Imports System.Drawing

Namespace BarCode

    Public Enum CodeSet
        CodeA
        CodeB
    End Enum

    Public Class EAN128Generator

#Region " Declaraciones "

        Private Shared cPatterns As Integer(,) = _
                { _
                    {2, 1, 2, 2, 2, 2, 0, 0}, {2, 2, 2, 1, 2, 2, 0, 0}, {2, 2, 2, 2, 2, 1, 0, 0}, {1, 2, 1, 2, 2, 3, 0, 0}, _
                    {1, 2, 1, 3, 2, 2, 0, 0}, {1, 3, 1, 2, 2, 2, 0, 0}, {1, 2, 2, 2, 1, 3, 0, 0}, {1, 2, 2, 3, 1, 2, 0, 0}, _
                    {1, 3, 2, 2, 1, 2, 0, 0}, {2, 2, 1, 2, 1, 3, 0, 0}, {2, 2, 1, 3, 1, 2, 0, 0}, {2, 3, 1, 2, 1, 2, 0, 0}, _
                    {1, 1, 2, 2, 3, 2, 0, 0}, {1, 2, 2, 1, 3, 2, 0, 0}, {1, 2, 2, 2, 3, 1, 0, 0}, {1, 1, 3, 2, 2, 2, 0, 0}, _
                    {1, 2, 3, 1, 2, 2, 0, 0}, {1, 2, 3, 2, 2, 1, 0, 0}, {2, 2, 3, 2, 1, 1, 0, 0}, {2, 2, 1, 1, 3, 2, 0, 0}, _
                    {2, 2, 1, 2, 3, 1, 0, 0}, {2, 1, 3, 2, 1, 2, 0, 0}, {2, 2, 3, 1, 1, 2, 0, 0}, {3, 1, 2, 1, 3, 1, 0, 0}, _
                    {3, 1, 1, 2, 2, 2, 0, 0}, {3, 2, 1, 1, 2, 2, 0, 0}, {3, 2, 1, 2, 2, 1, 0, 0}, {3, 1, 2, 2, 1, 2, 0, 0}, _
                    {3, 2, 2, 1, 1, 2, 0, 0}, {3, 2, 2, 2, 1, 1, 0, 0}, {2, 1, 2, 1, 2, 3, 0, 0}, {2, 1, 2, 3, 2, 1, 0, 0}, _
                    {2, 3, 2, 1, 2, 1, 0, 0}, {1, 1, 1, 3, 2, 3, 0, 0}, {1, 3, 1, 1, 2, 3, 0, 0}, {1, 3, 1, 3, 2, 1, 0, 0}, _
                    {1, 1, 2, 3, 1, 3, 0, 0}, {1, 3, 2, 1, 1, 3, 0, 0}, {1, 3, 2, 3, 1, 1, 0, 0}, {2, 1, 1, 3, 1, 3, 0, 0}, _
                    {2, 3, 1, 1, 1, 3, 0, 0}, {2, 3, 1, 3, 1, 1, 0, 0}, {1, 1, 2, 1, 3, 3, 0, 0}, {1, 1, 2, 3, 3, 1, 0, 0}, _
                    {1, 3, 2, 1, 3, 1, 0, 0}, {1, 1, 3, 1, 2, 3, 0, 0}, {1, 1, 3, 3, 2, 1, 0, 0}, {1, 3, 3, 1, 2, 1, 0, 0}, _
                    {3, 1, 3, 1, 2, 1, 0, 0}, {2, 1, 1, 3, 3, 1, 0, 0}, {2, 3, 1, 1, 3, 1, 0, 0}, {2, 1, 3, 1, 1, 3, 0, 0}, _
                    {2, 1, 3, 3, 1, 1, 0, 0}, {2, 1, 3, 1, 3, 1, 0, 0}, {3, 1, 1, 1, 2, 3, 0, 0}, {3, 1, 1, 3, 2, 1, 0, 0}, _
                    {3, 3, 1, 1, 2, 1, 0, 0}, {3, 1, 2, 1, 1, 3, 0, 0}, {3, 1, 2, 3, 1, 1, 0, 0}, {3, 3, 2, 1, 1, 1, 0, 0}, _
                    {3, 1, 4, 1, 1, 1, 0, 0}, {2, 2, 1, 4, 1, 1, 0, 0}, {4, 3, 1, 1, 1, 1, 0, 0}, {1, 1, 1, 2, 2, 4, 0, 0}, _
                    {1, 1, 1, 4, 2, 2, 0, 0}, {1, 2, 1, 1, 2, 4, 0, 0}, {1, 2, 1, 4, 2, 1, 0, 0}, {1, 4, 1, 1, 2, 2, 0, 0}, _
                    {1, 4, 1, 2, 2, 1, 0, 0}, {1, 1, 2, 2, 1, 4, 0, 0}, {1, 1, 2, 4, 1, 2, 0, 0}, {1, 2, 2, 1, 1, 4, 0, 0}, _
                    {1, 2, 2, 4, 1, 1, 0, 0}, {1, 4, 2, 1, 1, 2, 0, 0}, {1, 4, 2, 2, 1, 1, 0, 0}, {2, 4, 1, 2, 1, 1, 0, 0}, _
                    {2, 2, 1, 1, 1, 4, 0, 0}, {4, 1, 3, 1, 1, 1, 0, 0}, {2, 4, 1, 1, 1, 2, 0, 0}, {1, 3, 4, 1, 1, 1, 0, 0}, _
                    {1, 1, 1, 2, 4, 2, 0, 0}, {1, 2, 1, 1, 4, 2, 0, 0}, {1, 2, 1, 2, 4, 1, 0, 0}, {1, 1, 4, 2, 1, 2, 0, 0}, _
                    {1, 2, 4, 1, 1, 2, 0, 0}, {1, 2, 4, 2, 1, 1, 0, 0}, {4, 1, 1, 2, 1, 2, 0, 0}, {4, 2, 1, 1, 1, 2, 0, 0}, _
                    {4, 2, 1, 2, 1, 1, 0, 0}, {2, 1, 2, 1, 4, 1, 0, 0}, {2, 1, 4, 1, 2, 1, 0, 0}, {4, 1, 2, 1, 2, 1, 0, 0}, _
                    {1, 1, 1, 1, 4, 3, 0, 0}, {1, 1, 1, 3, 4, 1, 0, 0}, {1, 3, 1, 1, 4, 1, 0, 0}, {1, 1, 4, 1, 1, 3, 0, 0}, _
                    {1, 1, 4, 3, 1, 1, 0, 0}, {4, 1, 1, 1, 1, 3, 0, 0}, {4, 1, 1, 3, 1, 1, 0, 0}, {1, 1, 3, 1, 4, 1, 0, 0}, _
                    {1, 1, 4, 1, 3, 1, 0, 0}, {3, 1, 1, 1, 4, 1, 0, 0}, {4, 1, 1, 1, 3, 1, 0, 0}, {2, 1, 1, 4, 1, 2, 0, 0}, _
                    {2, 1, 1, 2, 1, 4, 0, 0}, {2, 1, 1, 2, 3, 2, 0, 0}, {2, 3, 3, 1, 1, 1, 2, 0} _
                }

        Private Const cQuietWidth As Integer = 10
        Private Const AddQuietZone As Boolean = False

#End Region

#Region " Funciones "

        Public Shared Function getCBarrasImage(ByVal nCodigo As String, ByVal nAncho As Integer, ByVal nAlto As Integer, ByVal nProporcion As Byte, ByVal nCodeAlign As AlignType) As Bitmap
            Dim content As Code128Content = New Code128Content(nCodigo)
            Dim codes() As Integer = content.Codes
            Dim widthCBarras, widthImagen, height As Integer

            widthCBarras = ((codes.Length - 3) * 11 + 35) * nProporcion
            height = nAlto 'Convert.ToInt32(System.Math.Ceiling(Convert.ToSingle(width) * 0.15F))

            'If (AddQuietZone) Then
            '    widthCBarras += 2 * cQuietWidth * nProporcion
            'End If

            widthImagen = CInt(IIf(widthCBarras > nAncho, widthCBarras, nAncho))

            'Dim BarCodeImage As New Bitmap(nAncho, nAlto)
            Dim BarCodeImage As Bitmap = New Bitmap(widthImagen, height)
            Dim MyGraphic As Graphics = Graphics.FromImage(BarCodeImage)

            Try
                MyGraphic.FillRectangle(Brushes.White, 0, 0, widthImagen, height)

                Dim cursor As Integer = CInt(IIf(AddQuietZone, cQuietWidth * nProporcion, 0))

                If nCodeAlign = AlignType.Center Then
                    cursor = CInt(cursor + (widthImagen - widthCBarras) / 2)
                ElseIf nCodeAlign = AlignType.Right Then
                    cursor += (widthImagen - widthCBarras)
                End If

                For codeidx As Integer = 0 To codes.Length - 1
                    Dim code As Integer = codes(codeidx)

                    For bar As Integer = 0 To 7 Step 2
                        Dim barwidth As Integer = cPatterns(code, bar) * nProporcion
                        Dim spcwidth As Integer = cPatterns(code, bar + 1) * nProporcion

                        If (barwidth > 0) Then
                            MyGraphic.FillRectangle(Brushes.Black, cursor, 0, barwidth, height)
                        End If

                        cursor += (barwidth + spcwidth)
                    Next
                Next

            Catch ex As Exception
                MyGraphic.DrawString("ERROR", New Font("Courier", 14), Brushes.Red, 10, 10)
            End Try

            Return BarCodeImage

        End Function

#End Region

    End Class

    Public Class Code128Content
        Private mCodeList() As Integer

#Region " Constructores "

        Public Sub New(ByVal AsciiData As String)
            mCodeList = StringToCode128(AsciiData)
        End Sub

#End Region

#Region " Propiedades "

        Public ReadOnly Property Codes() As Integer()
            Get
                Return mCodeList
            End Get
        End Property

#End Region

#Region " Funciones "

        Private Function StringToCode128(ByVal AsciiData As String) As Integer()
            Dim asciiBytes() As Byte = Encoding.ASCII.GetBytes(AsciiData)

            Dim csa1 As Code128Code.CodeSetAllowed = CType(IIf(asciiBytes.Length > 0, Code128Code.CodesetAllowedForChar(asciiBytes(0)), Code128Code.CodeSetAllowed.CodeAorB), Code128Code.CodeSetAllowed)
            Dim csa2 As Code128Code.CodeSetAllowed = CType(IIf(asciiBytes.Length > 0, Code128Code.CodesetAllowedForChar(asciiBytes(0)), Code128Code.CodeSetAllowed.CodeAorB), Code128Code.CodeSetAllowed)

            Dim currcs As CodeSet = GetBestStartSet(csa1, csa2)

            Dim codesLocal As New ArrayList(asciiBytes.Length + 3)
            codesLocal.Add(Code128Code.StartCodeForCodeSet(currcs))

            For i As Integer = 0 To asciiBytes.Length - 1
                Dim thischar As Integer = asciiBytes(i)
                Dim nextchar As Integer

                If asciiBytes.Length > (i + 1) Then
                    nextchar = asciiBytes(i + 1)
                Else
                    nextchar = -1
                End If

                codesLocal.AddRange(Code128Code.CodesForChar(thischar, nextchar, currcs))

            Next

            Dim checksum As Integer = CInt(codesLocal(0))
            For i As Integer = 1 To codesLocal.Count - 1

                checksum += i * CInt(codesLocal(i))
            Next
            codesLocal.Add(checksum Mod 103)

            codesLocal.Add(Code128Code.StopCode())

            Return CType(codesLocal.ToArray(GetType(Integer)), Integer())
        End Function
        Private Function GetBestStartSet(ByVal csa1 As Code128Code.CodeSetAllowed, ByVal csa2 As Code128Code.CodeSetAllowed) As CodeSet
            Dim vote As Integer = 0

            vote += CInt(IIf(csa1 = Code128Code.CodeSetAllowed.CodeA, 1, 0))
            vote += CInt(IIf(csa1 = Code128Code.CodeSetAllowed.CodeB, -1, 0))
            vote += CInt(IIf(csa2 = Code128Code.CodeSetAllowed.CodeA, 1, 0))
            vote += CInt(IIf(csa2 = Code128Code.CodeSetAllowed.CodeB, -1, 0))

            Return CType(IIf(vote > 0, CodeSet.CodeA, CodeSet.CodeB), CodeSet)
        End Function

#End Region

    End Class

    Public Class Code128Code

#Region " Declaraciones "

        Private Const cSHIFT As Integer = 98
        Private Const cCODEA As Integer = 101
        Private Const cCODEB As Integer = 100
        Private Const cSTARTA As Integer = 103
        Private Const cSTARTB As Integer = 104
        Private Const cSTOP As Integer = 106

        Public Enum CodeSetAllowed
            CodeA
            CodeB
            CodeAorB
        End Enum

#End Region

#Region " Funciones "

        Public Shared Function CodesForChar(ByVal CharAscii As Integer, ByVal LookAheadAscii As Integer, ByRef CurrCodeSet As CodeSet) As Integer()
            Dim result() As Integer
            Dim shifter As Integer = -1

            If (Not CharCompatibleWithCodeset(CharAscii, CurrCodeSet)) Then
                If ((LookAheadAscii <> -1) And Not CharCompatibleWithCodeset(LookAheadAscii, CurrCodeSet)) Then
                    Select Case CurrCodeSet
                        Case CodeSet.CodeA
                            shifter = cCODEB
                            CurrCodeSet = CodeSet.CodeB

                        Case CodeSet.CodeB
                            shifter = cCODEA
                            CurrCodeSet = CodeSet.CodeA

                    End Select

                Else
                    shifter = cSHIFT
                End If
            End If

            If (shifter <> -1) Then
                ReDim result(1)
                result(0) = shifter
                result(1) = CodeValueForChar(CharAscii)
            Else
                ReDim result(0)
                result(0) = CodeValueForChar(CharAscii)
            End If

            Return result
        End Function

        Public Shared Function CodesetAllowedForChar(ByVal CharAscii As Integer) As CodeSetAllowed
            If (CharAscii >= 32 And CharAscii <= 95) Then
                Return CodeSetAllowed.CodeAorB
            Else
                Return CType(IIf(CharAscii < 32, CodeSetAllowed.CodeA, CodeSetAllowed.CodeB), CodeSetAllowed)
            End If
        End Function

        Public Shared Function CharCompatibleWithCodeset(ByVal CharAscii As Integer, ByVal currcs As CodeSet) As Boolean
            Dim csa As CodeSetAllowed = CodesetAllowedForChar(CharAscii)

            Return csa = CodeSetAllowed.CodeAorB Or (csa = CodeSetAllowed.CodeA And currcs = CodeSet.CodeA) Or (csa = CodeSetAllowed.CodeB And currcs = CodeSet.CodeB)
        End Function

        Public Shared Function CodeValueForChar(ByVal CharAscii As Integer) As Integer
            Return CInt(IIf(CharAscii >= 32, CharAscii - 32, CharAscii + 64))
        End Function

        Public Shared Function StartCodeForCodeSet(ByVal cs As CodeSet) As Integer
            Return CInt(IIf(cs = CodeSet.CodeA, cSTARTA, cSTARTB))
        End Function

        Public Shared Function StopCode() As Integer
            Return cSTOP
        End Function

#End Region

    End Class

End Namespace