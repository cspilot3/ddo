Public Class ListItemDataTable
    Inherits DataTable

    Public Sub New(ByVal nData As DataTable, ByVal nTextColumnName As String, ByVal nValueColumnName As String, ByVal nTextFormat As String)
        Columns.Add(nTextColumnName, GetType(String))
        Columns.Add(nValueColumnName, GetType(String))

        For Each row As DataRow In nData.Rows
            Dim text = ""
            If (nTextFormat <> "") Then
                If (nTextFormat.IndexOf("yyyy-MM-dd") > -1) Then
                    text = DateTime.Parse(CStr(row(nTextColumnName))).ToString("yyyy-MM-dd")
                Else
                    text = String.Format("{0:" + nTextFormat + "}", row(nTextColumnName))
                End If
            Else
                text = row(nTextColumnName).ToString()
            End If

            Add(text, row(nValueColumnName).ToString())
        Next
    End Sub

    Public Sub New(ByVal nTextColumnName As String, ByVal nValueColumnName As String)
        Columns.Add(nTextColumnName, GetType(String))
        Columns.Add(nValueColumnName, GetType(String))
    End Sub

    Public Sub Add(ByVal nText As String, ByVal nValue As String)
        Rows.Add(New Object() {nText, nValue})
    End Sub
End Class
