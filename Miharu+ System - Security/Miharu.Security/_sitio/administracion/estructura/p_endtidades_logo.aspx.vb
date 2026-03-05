Imports Miharu.Security._clases

Namespace _sitio.administracion.estructura

    Partial Public Class p_endtidades_logo
        Inherits PaginaBasePopUp

#Region " Eventos "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        End Sub
        Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
            Cargar()
        End Sub
        Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Master.Cerrar(False)
        End Sub

#End Region

#Region " Metodos "

        Private Sub Cargar()
            If ifCargar.Value <> "" Then
                If UCase(ifCargar.Value).EndsWith(".BMP") Then
                    Try
                        If Not (ifCargar.PostedFile Is Nothing) Then

                            Dim FileName As String = CStr(Me.MySesion.Pagina.Parameter("TempFileNamePath"))

                            ifCargar.PostedFile.SaveAs(FileName)

                            Master.Cerrar(True)

                        End If
                    Catch ex As Exception
                        Master.ShowAlert(ex.Message, MiharuMasterPopUp.MsgBoxIcon.IconError)

                    End Try
                Else
                    Master.ShowAlert("La imagen debe estar en formato BMP", MiharuMasterPopUp.MsgBoxIcon.IconWarning)
                End If
            Else
                Master.ShowAlert("Debe seleccionar la imagen", MiharuMasterPopUp.MsgBoxIcon.IconWarning)
            End If
        End Sub

#End Region

    End Class
End Namespace