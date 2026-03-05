Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.VisorReportes.ResumenOT

    Public Class Report_ResumenOT
        Inherits DesktopReport

#Region " Propiedades "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Resumen Ordenes de Trabajo"
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
            Dim FormParametros As New Report_ResumenOT_Parametros()

            Dim Fecha_Inicio As DateTime
            Dim Fecha_Final As DateTime

            If FormParametros.ShowDialog = DialogResult.OK Then
                Fecha_Inicio = FormParametros.Fecha_Inicio
                Fecha_Final = FormParametros.Fecha_Final

                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Dim ResumenOTDataTable As DataTable
                Dim EsquemaDataTable As DBArchiving.SchemaCore.CTA_Esquema_NombresDataTable

                Dim Entidad As String = String.Empty
                Dim Proyecto As String = String.Empty
                Dim esquema As String = String.Empty

                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    ResumenOTDataTable = dbmArchiving.SchemaReport.PA_Resumen_OT.DBExecute(CDate(Fecha_Inicio), Fecha_Final, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
                    EsquemaDataTable = dbmArchiving.SchemaCore.CTA_Esquema_Nombres.DBFindByfk_Entidadfk_Proyectoid_Esquema(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Nothing)

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

                Dim InformeReportDataSource1 As New ReportDataSource("OT_RESUMEN", ResumenOTDataTable)

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.Report_ResumenOT.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()

                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource1)
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