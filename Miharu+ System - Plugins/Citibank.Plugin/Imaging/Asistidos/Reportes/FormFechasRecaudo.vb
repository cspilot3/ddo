Public Class FormFechasRecaudo

    Private strFechaRecaudoInicio As String = ""
    Private strFechaRecaudoFin As String = ""

    Public Property Fecha_Recaudo_Inicio() As String
        Get
            Return Me.strFechaRecaudoInicio
        End Get
        Set(value As String)
            Me.strFechaRecaudoInicio = value
        End Set
    End Property

    Public Property Fecha_Recaudo_Fin() As String
        Get
            Return Me.strFechaRecaudoFin
        End Get
        Set(value As String)
            Me.strFechaRecaudoFin = value
        End Set
    End Property

    Private Sub btnAceptar_Click(sender As System.Object, e As System.EventArgs) Handles btnAceptar.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Fecha_Recaudo_Inicio = Me.dtpFechaInicial.Value.ToString("yyyy/MM/dd")
        Me.Fecha_Recaudo_Fin = Me.dtpFechaFinal.Value.ToString("yyyy/MM/dd")
        Me.Close()
    End Sub

End Class