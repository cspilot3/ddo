Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.VisorReportes.UniversalvsParcial

    Public Class Report_UniversalvsParcial
        Inherits DesktopReport

#Region " Propiedades "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte Universal vs Parcial"
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
            Dim FormParametros As New Universal_vs_Parcial_Parametros()

            Dim Fecha_Inicio As DateTime = Now
            Dim Fecha_Final As DateTime = Now

            If FormParametros.ShowDialog = DialogResult.OK Then

                Dim id_Entidad = FormParametros.Entidad
                Dim id_Proyecto = FormParametros.Proyecto
                Dim id_Esquema = FormParametros.Esquema

                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Dim Recibidos As DataTable 'Universal_vs_Parcial.U_RecibidosDataTable
                Dim Faltantes As DataTable 'Universal_vs_Parcial.U_FaltantesDataTable
                Dim Sobrantes As DataTable 'Universal_vs_Parcial.U_SobrantesDataTable
                Dim Resumen As DataTable 'Universal_vs_Parcial.U_RecibidosDataTable
                Dim EsquemaDataTable As DBArchiving.SchemaCore.CTA_Esquema_NombresDataTable

                Dim Entidad As String = String.Empty
                Dim Proyecto As String = String.Empty
                Dim esquema As String = String.Empty

                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    Recibidos = dbmArchiving.SchemaReport.PA_Universal_Vs_Parcial_Recibidos.DBExecute(id_Entidad, id_Proyecto, id_Esquema)
                    Faltantes = dbmArchiving.SchemaReport.PA_Universal_Vs_Parcial_Faltantes.DBExecute(id_Entidad, id_Proyecto, id_Esquema)
                    Sobrantes = dbmArchiving.SchemaReport.PA_Universal_Vs_Parcial_Sobrantes.DBExecute(id_Entidad, id_Proyecto, id_Esquema)
                    Resumen = dbmArchiving.SchemaReport.PA_Universal_Vs_Parcial_Resumen.DBExecute(id_Entidad, id_Proyecto, id_Esquema)

                    EsquemaDataTable = dbmArchiving.SchemaCore.CTA_Esquema_Nombres.DBFindByfk_Entidadfk_Proyectoid_Esquema(id_Entidad, id_Proyecto, id_Esquema)
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

                Dim InformeReportDataSource1 As New ReportDataSource("Faltantes", Faltantes)
                Dim InformeReportDataSource2 As New ReportDataSource("Sobrantes", Sobrantes)
                Dim InformeReportDataSource3 As New ReportDataSource("Recibidos", Recibidos)
                Dim InformeReportDataSource4 As New ReportDataSource("Resumen", Resumen)

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.Report_UniversalvsParcial.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()

                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource1)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource2)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource3)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource4)

                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Entidad", Entidad))
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Proyecto", Proyecto))
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Esquema", esquema))
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Fecha_Inicio", CStr(Fecha_Inicio)))
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Fecha_Final", CStr(Fecha_Final)))

                Me.ReportViewer.MainReportViewer.RefreshReport()
            Else
                FormParametros.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace