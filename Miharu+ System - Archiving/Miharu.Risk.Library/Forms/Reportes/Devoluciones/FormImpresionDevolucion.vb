Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports DBArchiving
Imports Miharu.Desktop.Library.Config
Imports Microsoft.Reporting.WinForms

Namespace Forms.Reportes.Devoluciones

    Public Class FormImpresionDevolucion
        Inherits FormBase

#Region " Declaraciones "
        Private _NombreReporte As String
        Private _DataSource As xsdDevoluciones
        Private _TituloReporte As String
        Private _OcultarLlave2 As Object
#End Region

#Region " Constructor "
        Public Sub New(ByVal source As xsdDevoluciones, ByVal Reporte As String, ByVal TituloReporte As String, ByVal OcultarLlave2 As Object)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _DataSource = source
            _NombreReporte = Reporte
            _TituloReporte = TituloReporte
            _OcultarLlave2 = OcultarLlave2
        End Sub
#End Region

#Region " Eventos "
        Private Sub FormImpresionDevolucion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaReporte()
            Me.rptDevolucion.RefreshReport()
        End Sub
#End Region

#Region " Metodos "
        Private Sub CargaReporte()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                If rptDevolucion.LocalReport.DataSources.Count > 0 Then rptDevolucion.LocalReport.DataSources.RemoveAt(0)

                rptDevolucion.LocalReport.ReportEmbeddedResource = _NombreReporte
                rptDevolucion.LocalReport.DisplayName = _TituloReporte

                'Ordena los datos por llaves
                Dim dataItems = _DataSource.Items.DefaultView
                dataItems.Sort = "Llave1, Llave2 asc"

                Dim Parametros As New List(Of ReportParameter)
                If Not _OcultarLlave2 Is Nothing Then
                    Parametros.Add(New ReportParameter("OcultarLlave2Parameter", _OcultarLlave2.ToString))

                    rptDevolucion.LocalReport.SetParameters(Parametros)
                End If
                

                Utilities.NewDataSource(rptDevolucion, "DataDevolucionesItems", dataItems.ToTable())
                Utilities.NewDataSource(rptDevolucion, "DataDevolucionesReproceso", _DataSource.Reproceso)

                Me.rptDevolucion.RefreshReport()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaReporte", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub
#End Region

    End Class

End Namespace