Public Class DescargaArchivoExcel
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dtDatos As DataTable = Nothing
        Dim byteArray() As Byte
        Dim Longitud As Integer
        Dim Opcion As String = ""
        Dim NombreArchivo As String = ""
        NombreArchivo = Session("NombreArchivoExportacion").ToString()
        Opcion = Session("OpcionExportacion").ToString()
        If Opcion = "1" Then
            dtDatos = CType(Session("DatosReporte"), DataTable)
            GenerarExcel(Response, dtDatos, NombreArchivo)
        Else
            Longitud = Integer.Parse(Session("byteArrayLength").ToString())
            byteArray = CType(Session("byteArray"), Byte())
            GeneraTxt(byteArray, NombreArchivo)
        End If
    End Sub
    Public Function GeneraTxt(ByVal byteArray() As Byte, ByVal NombreArchivo As String) As Boolean
        Response.Clear()
        Response.BufferOutput = True
        Response.ContentType = "application/text"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & NombreArchivo)
        Response.OutputStream.Write(byteArray, 0, byteArray.Length)
        Response.Flush()
        Response.End()
        GeneraTxt = True
    End Function
    Public Function GenerarExcel(ByVal salida As System.Web.HttpResponse, ByVal dtDatos As DataTable, ByVal NombreArchivo As String) As Boolean
        Dim contador1 As Integer
        Dim contador2 As Integer
        Dim objDataSet As DataSet
        Dim objDataRow As DataRow
        Try
            objDataSet = New DataSet()
            objDataSet.Tables.Add(dtDatos)
            salida.ContentEncoding = System.Text.Encoding.UTF8
            salida.Write("<html><head> <meta charset=UTF-8></head>")
            salida.Write("<body><table border=1>")
            If Not objDataSet.HasErrors Then
                contador1 = 0
                contador2 = 0
                salida.Write("<tr>")
                While contador1 < objDataSet.Tables(0).Columns.Count
                    salida.Write("<td bgcolor=#2874A6><b><font face=Arial size=1 color=ffffff Font-Bold=True align=center>" & objDataSet.Tables(0).Columns(contador1).ColumnName.ToString() & "</font></b></td>")
                    contador1 = contador1 + 1
                End While
                salida.Write("</tr>")
                contador2 = contador2 + 1
                For Each objDataRow In objDataSet.Tables(0).Rows
                    contador1 = 0
                    salida.Write("<tr>")
                    While contador1 < objDataSet.Tables(0).Columns.Count
                        If objDataRow.Item(contador1).ToString().Length > 1 And objDataRow.Item(contador1).GetType.ToString = "System.String" Then
                            If objDataRow.Item(contador1).ToString().Substring(0, 1) = "0" And Not (objDataRow.Item(contador1).ToString().Contains("/")) Then
                                salida.Write("<td><font face=arial size=1>" & "'" & objDataRow.Item(contador1).ToString() & "</font></td>")
                            Else
                                salida.Write("<td><font face=arial size=1>" & objDataRow.Item(contador1).ToString() & "</font></td>")
                            End If
                        Else
                            salida.Write("<td><font face=arial size=1>" & objDataRow.Item(contador1).ToString() & "</font></td>")
                        End If
                        contador1 = contador1 + 1
                    End While
                    contador2 = contador2 + 1
                    salida.Write("</tr>")
                Next
                salida.Write("</table></body></html>")
            End If
            Try
                'salida.ContentType = "application/vnd.ms-excel"
                salida.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.openxmlformats-officedocument .spreadsheetml, application/vnd.ms-excel"
                salida.AddHeader("Content-Disposition", "attachment;filename=" & NombreArchivo)
                salida.End()
                GenerarExcel = True
            Catch ex As Exception
            End Try
        Catch ex As Exception
            GenerarExcel = False
        Finally
        End Try
    End Function
End Class