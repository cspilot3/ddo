Imports System.IO
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports Miharu.Web.Controls
Imports Miharu.Imaging.Library
Imports Slyg.Tools.Imaging
Imports System.Drawing
Imports Miharu.Desktop.Library

Partial Public Class visormultiple
    Inherits System.Web.UI.Page

#Region " Declaraciones "

    Private TokenImagen1 As String
    Private TokenImagen2 As String
    Private TokenImagen3 As String
    Private TokenImagen4 As String

#End Region

#Region " Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Config_Page()
        End If
    End Sub


#End Region

#Region " Metodos "

    Private Sub Config_Page()
        If Request.Params.Count > 0 Then
            Dim Tokens As String = Request.Params("token")
            Dim ArregloToken As String()
            ArregloToken = Tokens.Split(CChar(","))
            If ArregloToken.Length > 0 Then
                TokenImagen1 = ArregloToken(0).ToString()
            End If
            If ArregloToken.Length > 1 Then
                TokenImagen2 = ArregloToken(1).ToString()
            End If
            If ArregloToken.Length > 2 Then
                TokenImagen3 = ArregloToken(2).ToString()
            End If
            If ArregloToken.Length > 3 Then
                TokenImagen4 = ArregloToken(3).ToString()
            End If

            Iframe1.Attributes.Add("src", "visor.aspx?token=" + TokenImagen1)
            Iframe2.Attributes.Add("src", "visor.aspx?token=" + TokenImagen2)
            Iframe3.Attributes.Add("src", "visor.aspx?token=" + TokenImagen3)
            Iframe4.Attributes.Add("src", "visor.aspx?token=" + TokenImagen4)
        Else
            Return
        End If
    End Sub

#End Region

End Class