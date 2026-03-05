Public Class FormValidarPath

#Region " Propiedades "

    Public Property Codigo_Oficina() As String

    Public Property Fecha_Proceso() As String

#End Region

#Region " Eventos "

    Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
        Me.Codigo_Oficina = Me.OficinaDesktopTextBox.Text
        Me.Fecha_Proceso = Me.FechaDesktopTextBox.Text.Replace("/", "")

        Me.Close()
    End Sub

#End Region

End Class