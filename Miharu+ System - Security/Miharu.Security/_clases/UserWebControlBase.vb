Imports Miharu.Security.Library.Session

Namespace _clases

    Public Class UserWebControlBase
        Inherits UserControl

#Region " Declaraciones "

        Private _MySesion As Sesion

#End Region

#Region " Propiedades "

        Public ReadOnly Property MySesion() As Sesion
            Get
                Return _MySesion
            End Get
        End Property

#End Region

#Region " Eventos "

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            _MySesion = CType(Session("Sesion"), Sesion)

            If _MySesion Is Nothing Then
                _MySesion = New Sesion
            End If
        End Sub

#End Region

    End Class

End Namespace