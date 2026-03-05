Imports Miharu

Public Class FormBase
    Inherits System.Web.UI.Page

#Region " Declaraciones "

    Private _MySesion As Miharu.Security.Library.Session.Sesion
    Private WithEvents MyMaster As Imaging.MiharuMasterForm

    Public Event HijaClose()

#End Region

#Region " Propiedades "

    Public ReadOnly Property MySesion() As Miharu.Security.Library.Session.Sesion
        Get
            Return _MySesion
        End Get
    End Property
    Public Shadows ReadOnly Property Master() As Imaging.MiharuMasterForm
        Get
            Return CType(MyBase.Master, Imaging.MiharuMasterForm)
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
        If _MySesion Is Nothing Or Session("Sesion") Is Nothing Then
            Master.ShowAlert("La sesión ha caducado, por favor salga y vuelva a ingresar al aplicativo", Imaging.MiharuMasterForm.MsgBoxIcon.IconError)
            Response.Redirect("~/_sitio/blankform.aspx")

        ElseIf _MySesion.Pagina.Name <> nPageName Then
            Master.ShowAlert("El usuario no esta autorizado para ingresar a esta sección", Imaging.MiharuMasterForm.MsgBoxIcon.IconError)
            Response.Redirect("~/_sitio/blankform.aspx")

        ElseIf nPathPermiso = "0" Then
            Return True

        ElseIf ValidarPermisos(nPathPermiso) Then
            Return True

        Else
            Master.ShowAlert("El usuario no esta autorizado para ingresar a esta sección", Imaging.MiharuMasterForm.MsgBoxIcon.IconError)
            Response.Redirect("~/_sitio/blankform.aspx")

        End If

        Return False
    End Function
    Private Function ValidarPermisos(ByVal nPathPermiso As String) As Boolean
        Return _MySesion.Usuario.PerfilManager.PuedeAcceder(nPathPermiso)
    End Function

#End Region

End Class
