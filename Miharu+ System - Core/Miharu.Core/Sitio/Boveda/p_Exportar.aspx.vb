Public Class p_Exportar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("DataExportar") Is Nothing Then
            Dim dtTabla As DataTable = CType(Session("DataExportar"), DataTable)
            clsExportar.exportarCSVSimple(dtTabla, Session("NombreArchivo").ToString(), Session("Separador").ToString(), HttpContext.Current)
        End If
    End Sub
End Class