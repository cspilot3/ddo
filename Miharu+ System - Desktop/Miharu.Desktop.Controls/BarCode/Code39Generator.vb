Imports System.Drawing

Namespace BarCode

    Public Class Code39Generator

#Region " Declaraciones "

        ' Constantes
        Private Const alphabet39 As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*"

        Private Shared coded39Char As String() = {"000110100", "100100001", "001100001", "101100000", _
                                            "000110001", "100110000", "001110000", "000100101", _
                                            "100100100", "001100100", "100001001", "001001001", _
                                            "101001000", "000011001", "100011000", "001011000", _
                                            "000001101", "100001100", "001001100", "000011100", _
                                            "100000011", "001000011", "101000010", "000010011", _
                                            "100010010", "001010010", "000000111", "100000110", _
                                            "001000110", "000010110", "110000001", "011000001", _
                                            "111000000", "010010001", "110010000", "011010000", _
                                            "010000101", "110000100", "011000100", "010101000", _
                                            "010100010", "010001010", "000101010", "010010100"}

#End Region

#Region " Funciones "

        Public Shared Function getCBarrasImage(ByVal nCodigo As String, ByVal nAncho As Integer, ByVal nAlto As Integer, ByVal nProporcion As Byte, ByVal nCodeAlign As AlignType) As Bitmap
            Dim BarCodeImage As New Bitmap(nAncho, nAlto)
            Const intercharacterGap As String = "0"
            Dim str As String = "*" + nCodigo.ToUpper() & "*"
            Dim strLength As Integer = str.Length
            Dim MyGraphic As Graphics = Graphics.FromImage(BarCodeImage)
            Dim encodedString As String = ""

            Try
                For i As Integer = 0 To nCodigo.Length - 1
                    If alphabet39.IndexOf(nCodigo.Chars(i)) = -1 Or nCodigo.Chars(i) = "*"c Then
                        MyGraphic.DrawString("CODIGO DE BARRAS INVALIDO", New Font("Courier", 14), Brushes.Red, 10, 10)
                        Return BarCodeImage
                    End If
                Next

                For i As Integer = 0 To strLength - 1
                    If (i > 0) Then
                        encodedString += intercharacterGap
                    End If

                    encodedString += coded39Char(alphabet39.IndexOf(str.Chars(i)))
                Next

                Dim encodedStringLength As Integer = encodedString.Length
                Dim widthOfBarCodeString As Integer = 0
                Const wideToNarrowRatio As Double = 3

                If (nCodeAlign <> AlignType.Left) Then
                    For i As Integer = 0 To encodedStringLength - 1
                        If (encodedString.Chars(i) = "1"c) Then
                            widthOfBarCodeString += CInt(wideToNarrowRatio * CInt(nProporcion))
                        Else
                            widthOfBarCodeString += CInt(nProporcion)
                        End If
                    Next
                End If

                Dim x As Integer = 0
                Dim wid As Integer
                Const yTop As Integer = 0

                If (nCodeAlign = AlignType.Center) Then
                    x = CInt((nAncho - widthOfBarCodeString) / 2)
                ElseIf (nCodeAlign = AlignType.Right) Then
                    x = nAncho - widthOfBarCodeString
                End If

                For i As Integer = 0 To encodedStringLength - 1
                    If (encodedString.Chars(i) = "1"c) Then
                        wid = CInt(wideToNarrowRatio * CInt(nProporcion))
                    Else
                        wid = CInt(nProporcion)
                    End If

                    MyGraphic.FillRectangle(CType(IIf(i Mod 2 = 0, Brushes.Black, Brushes.White), Brush), x, yTop, wid, nAlto)

                    x += wid
                Next
            Catch ex As Exception
                MyGraphic.DrawString("ERROR", New Font("Courier", 14), Brushes.Red, 10, 10)
            End Try

            Return BarCodeImage

        End Function

#End Region

    End Class

End Namespace