Imports Miharu

Public Class PaginaBase
    Inherits System.Web.UI.Page

#Region " Declaraciones "

    Private _MySesion As Miharu.Security.Library.Session.Sesion
    Private WithEvents MyMaster As Imaging.MiharuMasterPage

    Public Event HijaClose()

#End Region

#Region " Propiedades "

    Public ReadOnly Property MySesion() As Security.Library.Session.Sesion
        Get
            Return _MySesion
        End Get
    End Property
    Public Shadows ReadOnly Property Master() As Imaging.MiharuMasterPage
        Get
            Return CType(MyBase.Master, Imaging.MiharuMasterPage)
        End Get
    End Property
    Public ReadOnly Property ConnectionString() As Program.TypeConnectionString
        Get
            If MySesion.Parameter("ConnectionStrings") Is Nothing Then
                Return Nothing
            Else
                Return CType(MySesion.Parameter("ConnectionStrings"), Program.TypeConnectionString)
            End If
        End Get
    End Property

#End Region

#Region " Eventos "

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _MySesion = CType(Session("Sesion"), Security.Library.Session.Sesion)
    End Sub
    Private Sub MyMaster_HijaClose() Handles MyMaster.HijaClose
        RaiseEvent HijaClose()
    End Sub

#End Region

#Region " Metodos "

    Protected Overrides Sub CreateChildControls()
        MyBase.CreateChildControls()

        MyMaster = Me.Master

    End Sub

    Protected Sub SelectText(ByVal ctl As System.Web.UI.WebControls.TextBox)
        ctl.Attributes.Add("onfocus", ctl.ClientID + ".select();")
    End Sub

#End Region

#Region " Funciones "

    Protected Function ValidarNavegacion(ByVal nPageName As String, ByVal nPathPermiso As String) As Boolean
        If _MySesion Is Nothing Then
            Response.Redirect("~/_sitio/login.aspx")
        ElseIf _MySesion.Pagina.Name <> nPageName Then
            Response.Redirect("~/_sitio/login.aspx")
        ElseIf nPathPermiso = "0" Then
            Return True
        ElseIf ValidarPermisos(nPathPermiso) Then
            Return True
        Else
            Response.Redirect("~/_sitio/login.aspx")
            Return False
        End If
    End Function
    Private Function ValidarPermisos(ByVal nPathPermiso As String) As Boolean
        Return _MySesion.Usuario.PerfilManager.PuedeAcceder(nPathPermiso)
    End Function

#End Region

End Class
