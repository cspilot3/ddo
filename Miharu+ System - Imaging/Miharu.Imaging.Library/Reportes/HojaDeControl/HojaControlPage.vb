Public Class HojaControlPage

    Shared offset As Integer
    Shared currentgroup As Object

    Public Function GetGroupPageNumber(group As Object, pagenumber As Integer) As Object

        ' If Not (group = currentgroup) Then
        'offset = pagenumber - 1
        'currentgroup = group
        'End If
        Return pagenumber - offset
    End Function

End Class
