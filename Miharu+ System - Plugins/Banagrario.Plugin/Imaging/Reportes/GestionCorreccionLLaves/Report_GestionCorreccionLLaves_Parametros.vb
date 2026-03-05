Namespace Imaging.Reportes.GestionCorreccionLLaves

    Public Class ReportGestionCorreccionLLavesParametros

#Region " Declaraciones "

        Public Plugin As BanagrarioImagingPlugin = Nothing

#End Region

#Region "Propiedades"

        Public Property FechaProceso() As Date
            Get
                Return dtpFechaProceso.Value.Date
            End Get
            Set(ByVal value As Date)
                dtpFechaProceso.Value = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

        Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

#End Region

#Region "Métodos"

        Public Sub New(ByVal nPlugin As BanagrarioImagingPlugin)

            InitializeComponent()
            Plugin = nPlugin

        End Sub

#End Region

    End Class

End Namespace