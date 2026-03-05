Imports Miharu.Core.Clases

Namespace Main
    Public Class MainPage
        Inherits System.Web.UI.Page

        Private _MySesion As Miharu.Security.Library.Session.Sesion

        Public Property MySesion() As Miharu.Security.Library.Session.Sesion
            Get
                Return _MySesion
            End Get
            Set(ByVal value As Miharu.Security.Library.Session.Sesion)
                _MySesion = value
            End Set
        End Property

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            DBCore.DBCoreDataBaseManager.IdentifierDateFormat = Program.getIdentifierDateFormat

            _MySesion = CType(Session("Sesion"), Miharu.Security.Library.Session.Sesion)
            If (_MySesion Is Nothing) OrElse (_MySesion.Pagina Is Nothing) Then
                Session("SesionError") = True
                Response.Redirect("~/Main/Login.aspx")
            End If

            If Not IsPostBack Then
                If (Not _MySesion Is Nothing) AndAlso (Not _MySesion.Pagina Is Nothing) Then
                    ifPagina.Attributes("src") = ResolveUrl(_MySesion.Pagina.PageDir)
                End If
            End If
        End Sub

        Private Sub MenuControl1_PageChanged(ByRef nPage As Miharu.Security.Library.Session.Pagina) Handles MenuControl1.PageChanged
            ifPagina.Attributes("src") = ResolveUrl(_MySesion.Pagina.PageDir)
        End Sub
    End Class
End Namespace