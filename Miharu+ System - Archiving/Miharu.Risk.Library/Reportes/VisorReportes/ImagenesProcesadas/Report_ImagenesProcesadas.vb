Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.VisorReportes.ImagenesProcesadas

    Public Class Report_ImagenesProcesadas
        Inherits DesktopReport

#Region " Propiedades "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte de Imagenes Procesadas"
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
            Dim FormParametros As New Report_ImagenesProcesadas_Parametros()

            Dim Fecha_Inicio As DateTime
            Dim Fecha_Final As DateTime

            If FormParametros.ShowDialog = DialogResult.OK Then
                Fecha_Inicio = FormParametros.FechaInicial
                Fecha_Final = FormParametros.FechaFinal

                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Dim Imagenes_ProcesadasDataTable As DataTable
                Dim EsquemaDataTable As DBArchiving.SchemaCore.CTA_Esquema_NombresDataTable

                Dim Entidad As String = String.Empty
                Dim Proyecto As String = String.Empty
                Dim esquema As String = String.Empty

                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    Imagenes_ProcesadasDataTable = dbmArchiving.SchemaReport.PA_Imagenes_Procesadas.DBExecute(Fecha_Inicio, Fecha_Final, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
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

                Dim InformeReportDataSource1 As New ReportDataSource("ImagenesProcesadasDataSet", Imagenes_ProcesadasDataTable)

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.Report_ImagenesProcesadas.rdlc"
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