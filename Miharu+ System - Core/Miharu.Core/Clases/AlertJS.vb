Imports Miharu
Imports System.Web
Imports System.Text
Imports System.Web.UI

''' <summary>
''' A JavaScript alert
''' </summary>
Public Class AlertJS
    ''' <summary>
    ''' Muestra un client-side JavaScript alert en el Browser.
    ''' </summary>
    ''' <param name="message">El mensaje aparece en el alert.</param>
    Public Shared Sub Show(ByVal message As String)

        ' Limpia el mensaje para permitir  comillas simples
        Dim cleanMessage As String = message.Replace("'", "\\'")
        Dim script As String = "<script type='text/javascript'>alert('" + cleanMessage + "');</script>"

        ' Obtiene la pagina web que se esta ejecutando
        Dim page As Page = CType(HttpContext.Current.CurrentHandler, Page)

        If (Not page Is Nothing And Not page.ClientScript.IsClientScriptBlockRegistered("alert")) Then
            page.ClientScript.RegisterClientScriptBlock(GetType(AlertJS), "alert", script)
        End If
    End Sub

End Class
