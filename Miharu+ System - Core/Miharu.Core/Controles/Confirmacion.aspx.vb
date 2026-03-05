Public Class Confirmacion
    Inherits PopupBase

#Region "DECLARACIONES"

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack) Then

            lblMensaje.Text = CStr(MySesion.Pagina.Parameter("mensaje"))
            MySesion.Pagina.Parameter("RESPUESTA") = "No"

        End If
        btnCancelar.Attributes("onclick") &= "return DisableAndGo(this);"
        btnAceptar.Attributes("onclick") &= "return DisableAndGo(this);"
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        MySesion.Pagina.Parameter("RESPUESTA") = "No"
        CloseWindow(True)
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        MySesion.Pagina.Parameter("RESPUESTA") = "Si"
        CloseWindow(True)
    End Sub

End Class