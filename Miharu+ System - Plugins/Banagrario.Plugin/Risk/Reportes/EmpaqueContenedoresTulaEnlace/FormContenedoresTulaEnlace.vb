Imports DBAgrario
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Risk.Reportes.EmpaqueContenedoresTulaEnlace

    Public Class FormContenedoresTulaEnlace

#Region " Declaraciones "

        Private _plugin As BanagrarioRiskPlugin
#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioRiskPlugin)
            InitializeComponent()

            'FormHelper.ControlarEventoCerrarVentanaTeclaEscape(Me)
            _Plugin = nBanagrarioDesktopPlugin
        End Sub
        
#End Region

#Region " Eventos "
        Private Sub Btn_Buscar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Btn_Buscar.Click
            If PrecintoValido(PrecintoDesktopTextBox.Text) Then
                BuscarReporte(PrecintoDesktopTextBox.Text)
            End If
        End Sub
#End Region

#Region " Metodos "

        Public Sub BuscarReporte(ByVal nPrecinto As String)

            Dim dbmBancoAgrario As New DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
            dbmBancoAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            Try
                Dim tulaEnlaceDataTable = dbmBancoAgrario.SchemaReport.PA_Empaque_Contenedores_TulaEnlace.DBExecute(nPrecinto)
                If ReportViewer.LocalReport.DataSources.Count > 0 Then ReportViewer.LocalReport.DataSources.RemoveAt(0)
                Utilities.NewDataSource(ReportViewer, "TulaEnlaceDataSet", tulaEnlaceDataTable)
                Me.ReportViewer.RefreshReport()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscarReporte", ex)
            End Try

            dbmBancoAgrario.Connection_Close()
        End Sub

#End Region

#Region " Funciones "
        Function PrecintoValido(ByVal nPrecinto As String) As Boolean
            If nPrecinto.Length < 4 Then
                DesktopMessageBoxControl.DesktopMessageShow("El precinto debe tener como minimo 4 números", "Buscar Reporte", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Return False
            End If
            Return True
        End Function
#End Region
        
    End Class

End Namespace