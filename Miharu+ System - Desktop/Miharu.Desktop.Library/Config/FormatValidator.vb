Public Class FormatValidator

#Region " Funciones "

    Public Shared Function ValidateNumber(ByVal Value As String, Optional ByVal Precision As Integer = -1, Optional ByVal Scale As Integer = -1) As String
        If Not IsNumeric(Value) Then Throw New Exception("El texto [" & Value & "] no es un número valido ")

        Dim strValue As String() = Value.ToString().Replace("."c, ","c).Split(","c)

        If (Precision <> -1 AndAlso Precision < strValue(0).Length) Then Throw New Exception("El número [" & Value & "] tiene una precision mayor a la permitida")

        If (Scale <> -1 AndAlso strValue.Length > 1 AndAlso Scale < strValue(1).Length) Then Throw New Exception("El número [" & Value & "] tiene una escala mayor a la permitida")

        Return Value
    End Function

    Public Shared Function ValidateText(ByVal Value As String, Optional ByVal Length As Integer = -1) As String
        If (Length <> -1 AndAlso Length < Value.ToString().Length) Then Throw New Exception("El texto [" & Value & "] tiene una longitud mayor a la permitida")

        Return Value
    End Function

    Public Shared Function ValidateDate(ByVal Value As String, Optional ByVal dateFormat As String = "yyyyMMddhhmmss") As String
        Dim dateValue As DateTime
        Try
            dateValue = DateTime.Parse(Value)
        Catch ex As Exception
            Throw New Exception("El texto [" & Value & "] no es una fecha valida " & vbCrLf & ex.Message)
        End Try

        Return dateValue.ToString(dateFormat)
    End Function

    Public Shared Function getSeparador(ByVal nEtiqueta As String) As Char
        Select Case nEtiqueta
            Case "[Tabulador]", "t"
                Return CChar(vbTab)

            Case "[Dos puntos]", ":"
                Return ":"c

            Case "[Punto y coma]", ";"
                Return ";"c

            Case "[Coma]", ","
                Return ","c

            Case "[Barra]", "|"
                Return "|"c

            Case "[Espacio]", " "
                Return " "c

            Case Else
                Return CChar(vbTab)

        End Select
    End Function

    Public Shared Function getDelimitadorTexto(ByVal nEtiqueta As String) As Char
        Select Case nEtiqueta
            Case "(Ninguno)", " "
                Return CChar("")

            Case "[Comilla doble]", """"
                Return """"c

            Case "[Comilla sencilla]", "'"
                Return "'"c

            Case Else
                Return CChar("")

        End Select
    End Function

#End Region

End Class


