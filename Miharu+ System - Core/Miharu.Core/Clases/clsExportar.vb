Public Class clsExportar

    Public Shared Sub exportarExcel(ByVal dtTabla As DataTable, ByVal sNombre As String, ByRef contex As HttpContext) 'As HttpContext 
        Dim attachment As String = "attachment; filename=" & sNombre & ".xls"
        Dim tab As String = ""
        contex.Response.ClearContent()
        contex.Response.AppendHeader("Content-Disposition", attachment)
        contex.Response.ContentType = "application/vnd.ms-excel"
        contex.Response.ContentEncoding = System.Text.Encoding.UTF8
        'Inicio Tabla 
        contex.Response.Write("<table border=1>")
        contex.Response.Write("<tr>")
        'Encabezado 
        For Each columna As DataColumn In dtTabla.Columns
            contex.Response.Write("<th>" & columna.ColumnName & "</th>")
        Next
        contex.Response.Write("</tr>")
        contex.Response.Write(Environment.NewLine)
        'Registros 
        For Each fila As DataRow In dtTabla.Rows
            contex.Response.Write("<tr>")
            For i = 0 To dtTabla.Columns.Count - 1
                contex.Response.Write("<td>" & fila(i).ToString() & "</td>")
            Next
            contex.Response.Write("</tr>")
        Next
        'Fin Tabla 
        contex.Response.Write("</table>")
        contex.Response.Flush()
        'Return contex 
    End Sub

    Public Shared Sub exportarExcel_Grid(ByVal dtTabla As GridView, ByVal sNombre As String, ByRef contex As HttpContext)
        Dim attachment As String = "attachment; filename=" & sNombre & ".xls"
        Dim tab As String = ""
        contex.Response.ClearContent()
        contex.Response.AppendHeader("Content-Disposition", attachment)
        contex.Response.ContentType = "application/vnd.ms-excel"
        contex.Response.ContentEncoding = System.Text.Encoding.UTF8
        'Inicio Tabla 
        contex.Response.Write("<table border=1>")
        contex.Response.Write("<tr>")
        'Encabezado 
        For Each columna As DataControlField In dtTabla.Columns
            contex.Response.Write("<th>" & columna.HeaderText & "</th>")
        Next
        contex.Response.Write("</tr>")
        contex.Response.Write(Environment.NewLine)
        'Registros 
        For Each fila As GridViewRow In dtTabla.Rows
            contex.Response.Write("<tr>")
            For i = 0 To fila.Cells.Count - 1
                contex.Response.Write("<td>" & fila.Cells(i).Text & "</td>")
            Next
            contex.Response.Write("</tr>")
        Next
        'Fin Tabla 
        contex.Response.Write("</table>")
        contex.Response.Flush()
    End Sub

    Public Shared Sub exportarCSVSimple(ByVal dtTabla As DataTable, ByVal sNombre As String, ByVal separador As String, ByRef contex As HttpContext)
        Dim attachment As String = "attachment; filename=" & sNombre & ".csv"
        Dim tab As String = ""
        contex.Response.ClearContent()
        contex.Response.AppendHeader("Content-Disposition", attachment)
        contex.Response.ContentType = "text/plain"
        contex.Response.ContentEncoding = System.Text.Encoding.UTF8

        'Registros 
        For Each fila As DataRow In dtTabla.Rows
            contex.Response.Write(fila("Codigo_Boveda_Posicion").ToString() & separador)
        Next
        contex.Response.Flush()
    End Sub
End Class
