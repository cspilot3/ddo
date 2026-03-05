Imports System.IO
Imports System.Web.UI

Public Class FileUpload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Sub BtnSubir_Click(sender As Object, e As EventArgs) Handles BtnSubir.Click

        Dim extension As String = System.IO.Path.GetExtension(FileUpload1.FileName)
        If FileUpload1.HasFile Then


            If extension = ".csv" Or extension = ".txt" Then
                FileUpload1.SaveAs(Server.MapPath("~/_temporal/" + FileUpload1.FileName))
                Session("fileupload") = FileUpload1.FileName
                LblInfo.Text = "Archivo cargado con éxito!, Cierra la ventana"

            Else
                LblInfo.Text = "Selecciona archivos (*.csv|*.txt)"

            End If

        Else
            LblInfo.Text = "Debes seleccionar un archivo (*.csv|*.txt)"
        End If
    End Sub
End Class