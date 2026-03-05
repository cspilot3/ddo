Namespace Reportes.VisorReportes.Validaciones

    Public Class Report_Validaciones_Parametros

#Region " Propiedades "

        Public Property Fecha_Inicio() As DateTime
            Get
                Return dtpFechaInicial.Value
            End Get
            Set(ByVal value As DateTime)
                dtpFechaInicial.Value = value
            End Set
        End Property

        Public Property Fecha_Final() As DateTime
            Get
                Return dtpFechaFinal.Value
            End Get
            Set(ByVal value As DateTime)
                dtpFechaFinal.Value = value
            End Set
        End Property

#End Region

#Region " Constructores "

        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()
        End Sub

#End Region

#Region " Eventos "

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

#End Region

    End Class

End Namespace