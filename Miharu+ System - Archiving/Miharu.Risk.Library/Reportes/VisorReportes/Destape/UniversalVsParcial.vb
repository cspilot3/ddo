Imports Miharu.Desktop.Library.Config
Imports DBArchiving
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.VisorReportes.Destape

    Public Class UniversalVsParcial
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "UniversalVsParcial"
            End Get
        End Property
#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl)
            MyBase.New(nReportViewer)
        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch()

            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim table As DataTable
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            table = dbmArchiving.Schemadbo.RPT_Universal_vs_Parcial.DBFindByfk_entidadfk_proyecto(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
            Me.ReportViewer.MainReportViewer.Reset()
            Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.UniversalVsParcial.rdlc"
            If Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Count > 0 Then Me.ReportViewer.MainReportViewer.LocalReport.DataSources.RemoveAt(0)
            Utilities.NewDataSource(Me.ReportViewer.MainReportViewer, "Universal", table)
            Me.ReportViewer.MainReportViewer.RefreshReport()
            dbmArchiving.Connection_Close()
        End Sub


#End Region

    End Class

End Namespace