Namespace Forms.Boveda

    Public Class ControlPosicion

#Region " Propiedades "

        Public Property Posicion As String

        Public Property Caja As String

        Public Property Entidad As String

        Public Property Proyecto As String

        Public Property Carpetas As Integer

        Public Property TipoCaja As String

#End Region

#Region " Constructor "

        Public Sub New(ByVal nPosicion As String, ByVal nCaja As String, ByVal nEntidad As String, ByVal nProyecto As String)
            InitializeComponent()

            Posicion = nPosicion
            Caja = nCaja
            Entidad = nEntidad
            Proyecto = nProyecto
        End Sub

        Public Sub New()
            InitializeComponent()
        End Sub

#End Region

#Region " Eventos "

        Private Sub ControlPosicion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            PosicionLabel.Text = Posicion
            CajaLabel.Text = Caja
            EntidadLabel.Text = Entidad
            ProyectoLabel.Text = Proyecto
            TipoLabel.Text = TipoCaja
            CarpetasLabel.Text = CStr(Carpetas)

            If Caja <> "" Then
                Me.BackColor = Drawing.Color.LightGreen
                PropiedadesPanel.BackColor = Drawing.Color.DarkSeaGreen

                L1.ForeColor = Drawing.Color.Black
                L2.ForeColor = Drawing.Color.Black
                L3.ForeColor = Drawing.Color.Black
                L4.ForeColor = Drawing.Color.Black

                EntidadLabel.ForeColor = Drawing.Color.Black
                ProyectoLabel.ForeColor = Drawing.Color.Black
                PosicionLabel.ForeColor = Drawing.Color.Black
                CajaLabel.ForeColor = Drawing.Color.Black
            Else
                Me.BackColor = Drawing.Color.IndianRed
                PropiedadesPanel.BackColor = Drawing.Color.LightCoral

                L1.ForeColor = Drawing.Color.White
                L2.ForeColor = Drawing.Color.White
                L3.ForeColor = Drawing.Color.White
                L4.ForeColor = Drawing.Color.White

                EntidadLabel.ForeColor = Drawing.Color.White
                ProyectoLabel.ForeColor = Drawing.Color.White
                PosicionLabel.ForeColor = Drawing.Color.White
                CajaLabel.ForeColor = Drawing.Color.White
            End If

        End Sub

#End Region

    End Class

End Namespace