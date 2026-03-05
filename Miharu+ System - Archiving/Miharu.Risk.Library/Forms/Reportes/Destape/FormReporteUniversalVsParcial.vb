Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Reportes.Destape

    Public Class FormReporteUniversalVsParcial
        Inherits FormBase

#Region " Eventos "

        Private Sub Formreportes_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            CargaReportes(dbmArchiving.Schemadbo.RPT_Universal_vs_Parcial.DBFindByfk_entidadfk_proyecto(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto))

            dbmArchiving.Connection_Close()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaReportes(ByVal Table As DataTable)
            If ReportViewer.LocalReport.DataSources.Count > 0 Then ReportViewer.LocalReport.DataSources.RemoveAt(0)
            Utilities.NewDataSource(ReportViewer, "Universal", Table)
            Me.ReportViewer.RefreshReport()
        End Sub

#End Region

    End Class

End Namespace