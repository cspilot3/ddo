Imports Miharu.Core.Main
Imports Miharu.Core.Clases

Public Class _Error
    Inherits FormBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BodyType = BodyType.Unique
                lblMensajeError.Text = "Error procesando la página" & vbCrLf & vbCrLf

                If Not IsNothing(Session("msgError")) Then
                    MessageDetail.Text = "<b>Message</b>: " & CType(Session("msgError"), Exception).Message & "<br/>"
                    MessageDetail.Text += "<b>StackTrace</b>: " & CType(Session("msgError"), Exception).StackTrace & "<br/>"
                    MessageDetail.Text += "<b>Source</b>: " & CType(Session("msgError"), Exception).Source & "<br/>"
                End If
            End If
        Catch ex As Exception
            Throw New Exception()
        End Try
    End Sub

End Class