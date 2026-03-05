Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Imaging.Reportes.Seguimiento

    Public Class ReportSeguimientoProcesoDiario
        Inherits DesktopReport

#Region " Declaraciones "

        Private _plugin As BanagrarioImagingPlugin

#End Region

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte Seguimiento Proceso Diario"
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl, ByVal nPlugin As BanagrarioImagingPlugin)
            MyBase.New(nReportViewer)
            _plugin = nPlugin
        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch()

            Dim parametrosForm As New FormSeguimientoProcesoDiario(_plugin)
            Dim fechaProceso As DateTime
            Dim sedeDestape As Short

            If parametrosForm.ShowDialog() = DialogResult.OK Then
                fechaProceso = parametrosForm.FechaProceso
                sedeDestape = parametrosForm.SedeDestape

                Dim datos As DataTable

                Dim dmBanAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

                Try
                    dmBanAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                    dmBanAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    datos = dmBanAgrario.SchemaReport.PA_Seguimiento_Proceso_Diario.DBExecute(fechaProceso.ToString("yyyy/MM/dd"), _
                                                                                          sedeDestape)

                    Dim informeReportDataSource As New ReportDataSource("DsContenedoresConsolidado", datos)
                    Dim parametros As New List(Of ReportParameter)
                    parametros.Add(New ReportParameter("FechaReportParameter", fechaProceso.ToString("yyyy/MM/dd")))

                    Me.ReportViewer.MainReportViewer.Reset()
                    Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.Report_SeguimientoProcesoDiario.rdlc"
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                    Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(parametros)
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(informeReportDataSource)
                    Me.ReportViewer.MainReportViewer.RefreshReport()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    dmBanAgrario.Connection_Close()
                    parametrosForm.Close()
                End Try

            Else
                parametrosForm.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace

