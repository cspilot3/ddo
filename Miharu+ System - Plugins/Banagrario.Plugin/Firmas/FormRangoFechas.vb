Imports System.Windows.Forms

Namespace Firmas

    Public Class FormRangoFechas

#Region " Declaraciones "

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
                Return dtpFechaFinal.Value.Date.AddDays(1).AddMilliseconds(-10)
            End Get
            Set(ByVal value As Date)
                dtpFechaFinal.Value = value
            End Set
        End Property

#End Region

#Region " Constructor "

        Public Sub New()

            InitializeComponent()

        End Sub

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
        Private Sub FormRangoFechas_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        End Sub

#End Region

#Region " Funciones "

        'Private Function Validar() As Boolean
        '    If dtpFechaInicial.Value > dtpFechaFinal.Value Then
        '        MessageBox.Show("La Fecha Final debe ser superior a la Fecha Inicial", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        dtpFechaFinal.Focus()

        '    Else
        '        Return True
        '    End If

        '    Return False
        'End Function

#End Region

#Region " Metodos "


#End Region

    End Class
End Namespace