Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.VisorReportes.DestapeOT

    Public Class Report_DestapeOT
        Inherits DesktopReport

#Region " Propiedades "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Destape OT"
            End Get
        End Property

        Public connectionstring As String = "SlygProvider=SqlServer;Data Source=10.64.64.56\certificacion;Initial Catalog=DB_Miharu.Archiving;Persist Security Info=True;User ID=sa;Password=tests123"

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl)
            MyBase.New(nReportViewer)

        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch()
            Dim FormParametros As New Report_DestapeOT_Parametros()

            Dim OT As Integer

            If FormParametros.ShowDialog = DialogResult.OK Then
                OT = FormParametros.OT

                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Dim TotalesDataTable As DataTable
                Dim AdicionDataTable As DataTable
                Dim NuevosDataTable As DataTable
                Dim DevolucionDataTable As DataTable
                Dim OTDataTable As DataTable

                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    TotalesDataTable = dbmArchiving.SchemaReport.CTA_Totales_X_OT.DBFindByOT(OT)
                    AdicionDataTable = dbmArchiving.SchemaReport.PA_Adicion_X_OT.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, OT)
                    NuevosDataTable = dbmArchiving.SchemaReport.PA_Nuevos_X_OT.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, OT)
                    DevolucionDataTable = dbmArchiving.SchemaReport.PA_Devolucion_X_OT.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, OT)
                    OTDataTable = dbmArchiving.Schemadbo.RPT_OTs_Encabezado.DBFindByid_Ot(OT)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    dbmArchiving.Connection_Close()
                    FormParametros.Close()
                End Try

                Dim InformeReportDataSource1 As New ReportDataSource("Totales", TotalesDataTable)
                Dim InformeReportDataSource2 As New ReportDataSource("Adicion", AdicionDataTable)
                Dim InformeReportDataSource3 As New ReportDataSource("Nuevos", NuevosDataTable)
                Dim InformeReportDataSource4 As New ReportDataSource("Devolucion", DevolucionDataTable)
                Dim InformeReportDataSource5 As New ReportDataSource("OTEncabezado", OTDataTable)

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.Report_DestapeOT.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()

                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("OTReportParameter", CStr(OT)))
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource1)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource2)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource3)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource4)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource5)

                Me.ReportViewer.MainReportViewer.RefreshReport()
            Else
                FormParametros.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace