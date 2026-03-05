Namespace Arquitectura
    Public Class UserWebControlBase
        Inherits System.Web.UI.UserControl

#Region " DECLARACIONES "
        Private _MySesion As Security.Library.Session.Sesion
#End Region

#Region " PROPIEDADES "
        Public ReadOnly Property MySesion() As Security.Library.Session.Sesion
            Get
                Return _MySesion
            End Get
        End Property
#End Region

#Region " EVENTOS "

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            _MySesion = CType(Session("Sesion"), Security.Library.Session.Sesion)

            If _MySesion Is Nothing Then
                _MySesion = New Security.Library.Session.Sesion
            End If
        End Sub
#End Region

    End Class
End Namespace