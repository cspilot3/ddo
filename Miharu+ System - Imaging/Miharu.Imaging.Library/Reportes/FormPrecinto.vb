Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library

Namespace Reportes

    Public Class FormPrecinto

#Region " Propiedades "

        Public Property FechaProcesoInicial() As Date
            Get
                Return dtpFechaInicial.Value.Date
            End Get
            Set(ByVal value As Date)
                dtpFechaInicial.Value = value
            End Set
        End Property

        Public Property FechaProcesoFinal() As Date
            Get
                Return dtpFechaFinal.Value.Date.AddDays(1).AddSeconds(-1)
            End Get
            Set(ByVal value As Date)
                dtpFechaFinal.Value = value
            End Set
        End Property

        Public Property OT() As Slyg.Tools.SlygNullable(Of Integer)
            Get
                If (dntOT.Text = "") Then
                    Return DBNull.Value
                Else
                    Return Integer.Parse(dntOT.Text)
                End If
            End Get
            Set(ByVal value As Slyg.Tools.SlygNullable(Of Integer))
                dntOT.Text = value.Value.ToString
            End Set
        End Property

        Public Property Precinto() As Slyg.Tools.SlygNullable(Of String)
            Get
                If (dtPrecinto.Text = "") Then
                    Return DBNull.Value
                Else
                    Return dtPrecinto.Text
                End If
            End Get
            Set(ByVal value As Slyg.Tools.SlygNullable(Of String))
                dtPrecinto.Text = value.Value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

#End Region

    End Class

End Namespace
