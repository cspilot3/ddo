Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.VisorReportes.EmpaqueNivelCaja

    Public Class Report_EmpaqueNivelCaja
        Inherits DesktopReport

#Region " Propiedades "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte de Empaque a Nivel de Caja"
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
            Dim FormParametros As New Report_EmpaqueNivelCaja_Parametros()

            Dim Caja As String

            If FormParametros.ShowDialog = DialogResult.OK Then
                Caja = FormParametros.Caja

                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Dim EmpaqueNivelCajaDataTable As DataTable
                Dim EsquemaDataTable As DBArchiving.SchemaCore.CTA_Esquema_NombresDataTable
                Dim TotalesDataTable As DataTable

                Dim Entidad As String = String.Empty
                Dim Proyecto As String = String.Empty
                Dim esquema As String = String.Empty

                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    EmpaqueNivelCajaDataTable = dbmArchiving.Schemadbo.PA_Reporte_Empaque_Nivel_Caja.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Caja)
                    EsquemaDataTable = dbmArchiving.SchemaCore.CTA_Esquema_Nombres.DBFindByfk_Entidadfk_Proyectoid_Esquema(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Nothing)
                    TotalesDataTable = dbmArchiving.SchemaReport.PA_Totales_Caja.DBExecute(Caja)

                    If EsquemaDataTable.Count > 0 Then
                        Entidad = EsquemaDataTable(0).Nombre_Entidad
                        Proyecto = EsquemaDataTable(0).Nombre_Proyecto
                        esquema = EsquemaDataTable(0).Nombre_Esquema
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    dbmArchiving.Connection_Close()
                    FormParametros.Close()
                End Try

                Dim InformeReportDataSource1 As New ReportDataSource("EmpaqueNivelCajaDataSet", EmpaqueNivelCajaDataTable)
                Dim InformeReportDataSource2 As New ReportDataSource("Total_por_Caja", TotalesDataTable)

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.Report_EmpaqueNivelCaja.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()

                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource1)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource2)
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Entidad", Entidad))
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Proyecto", Proyecto))
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Esquema", esquema))

                Me.ReportViewer.MainReportViewer.RefreshReport()
            Else
                FormParametros.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace