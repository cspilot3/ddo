Public Partial Class errorform
    Inherits System.Web.UI.Page

#Region " Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblTitulo.Text = CStr(Session("Error_Titulo"))
        lblMensaje.Text = CStr(Session("Error_Mensaje"))
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalir.Click
        Dim MyMaster As MiharuMasterPage = CType(Me.Master, MiharuMasterPage)

        MyMaster.Cerrar()

    End Sub

#End Region

End Class