Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Reportes

    Public Class FormRangoFechas2

#Region " Declaraciones "

        Private _TipoReporte As String

#End Region

#Region " Propiedades "

        Public Property FechaInicial() As Date
            Get
                Return dtpFechaInicial.Value.Date
            End Get
            Set(ByVal value As Date)
                dtpFechaInicial.Value = value
            End Set
        End Property
        Public Property FechaFinal() As Date
            Get
                Return dtpFechaFinal.Value.Date.AddDays(1).AddSeconds(-1)
            End Get
            Set(ByVal value As Date)
                dtpFechaFinal.Value = value
            End Set
        End Property
#End Region

#Region " Constructor "

        Public Sub New(ByVal nTipoReporte As String)
            InitializeComponent()
            _TipoReporte = nTipoReporte
        End Sub

#End Region

#Region " Eventos "

        Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
            If Not Validar() Then
                Exit Sub
            End If
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub FormRangoFechas_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Select Case _TipoReporte
                Case "PublicacionFilenet"
                    lblFechaInicial.Text = "Fecha Inicial de Publicación"
                    lblFechaFinal.Text = "Fecha Final de Publicación"
            End Select
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If dtpFechaInicial.Value > dtpFechaFinal.Value Then
                MessageBox.Show("La Fecha Final debe ser superior a la Fecha Inicial", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error)
                dtpFechaFinal.Focus()
                Return False
            End If

            If _TipoReporte = "PublicacionFilenet" Then
                If DateDiff(DateInterval.Day, dtpFechaInicial.Value, dtpFechaFinal.Value) > 31 Then
                    MessageBox.Show("El rango entre Fecha Inicial y Fecha Final no puede ser superior a 31 días", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    dtpFechaFinal.Focus()
                    Return False
                End If
            End If

            Return True
        End Function
#End Region

#Region " Metodos "


#End Region

    End Class

End Namespace