Public Class FrmFechaProceso

    Private strFechaProceso As String = ""

    Public Property Fecha_Proceso() As String
        Get
            Return Me.strFechaProceso
        End Get
        Set(value As String)
            Me.strFechaProceso = value
        End Set
    End Property

    Private Sub btnAceptar_Click(sender As System.Object, e As System.EventArgs) Handles btnAceptar.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Fecha_Proceso = Me.dtpFechaInicial.Value.ToString("yyyy/MM/dd")
        Me.Close()
    End Sub

    Private Sub FrmFechaProceso_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.AcceptButton = btnAceptar
    End Sub
End Class