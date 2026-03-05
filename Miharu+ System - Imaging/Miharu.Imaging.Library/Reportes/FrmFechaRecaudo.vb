Public Class FrmFechaRecaudo

    Private strFechaRecaudo As String = ""

    Public Property Fecha_Recaudo() As String
        Get
            Return Me.strFechaRecaudo
        End Get
        Set(value As String)
            Me.strFechaRecaudo = value
        End Set
    End Property

    Private Sub btnAceptar_Click(sender As System.Object, e As System.EventArgs) Handles btnAceptar.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Fecha_Recaudo = Me.dtpFechaInicial.Value.ToString("yyyy/MM/dd")
        Me.Close()
    End Sub
End Class