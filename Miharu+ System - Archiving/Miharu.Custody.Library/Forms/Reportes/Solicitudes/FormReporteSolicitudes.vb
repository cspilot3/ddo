Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Reportes.Solicitudes

    Public Class FormReporteSolicitudes
        Inherits FormBase

#Region " Declaraciones "
        Private _Solicitudes As DataTable
#End Region

#Region " Constructores "

        Public Sub New(ByVal datos As DataTable)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _Solicitudes = datos
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormReporteSolicitudes_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            If SolicitudesReportViewer.LocalReport.DataSources.Count > 0 Then SolicitudesReportViewer.LocalReport.DataSources.RemoveAt(0)
            Utilities.NewDataSource(SolicitudesReportViewer, "SolicitudesDataSet", _Solicitudes)
            Me.SolicitudesReportViewer.RefreshReport()
        End Sub

#End Region

    End Class

End Namespace