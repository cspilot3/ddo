Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Imaging.Reportes.Contenedor

    Public Class Report_ConsolidadoContenedor
        Inherits DesktopReport

#Region " Declaraciones "

        Private _plugin As BanagrarioImagingPlugin

#End Region

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte Recepcion y Destape"
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

            Dim ParametrosForm As New FormParametrosContenedor(_plugin)
            Dim FechaProceso As DateTime
            Dim FechaMovimiento As DateTime
            Dim Regional As Short
            Dim Cob As Short
            Dim Oficina As Short
            Dim Modo As Short

            If ParametrosForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                FechaProceso = ParametrosForm.FechaProceso
                FechaMovimiento = ParametrosForm.FechaMovimiento
                Regional = ParametrosForm.Regional
                Cob = ParametrosForm.COB
                Oficina = ParametrosForm.Oficina

                If ((ParametrosForm.ModoFechaProceso = True) And (ParametrosForm.ModoFechaMovimiento = True)) Then
                    Modo = 3
                ElseIf ((ParametrosForm.ModoFechaMovimiento = True) And (ParametrosForm.ModoFechaProceso = False)) Then
                    Modo = 2
                ElseIf (ParametrosForm.ModoFechaProceso = True) Then
                    Modo = 1
                Else
                    Modo = 0
                End If

                Dim datos As DataTable

                Dim dbmAgrario As New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)

                Try

                    dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    datos = dbmAgrario.SchemaReport.PA_Contenedores_Consolidado.DBExecute(Regional, _
                                                                                          Cob, _
                                                                                          Oficina, _
                                                                                          FechaProceso.ToString("yyyy/MM/dd"), _
                                                                                          FechaMovimiento.ToString("yyyy/MM/dd"), _
                                                                                          Modo)



                    Dim InformeReportDataSource As New ReportDataSource("DsContenedoresConsolidado", datos)
                    Dim Parametros As New List(Of ReportParameter)
                    Parametros.Add(New ReportParameter("FechaReportParameter", FechaProceso.ToString("yyyy/MM/dd")))

                    Me.ReportViewer.MainReportViewer.Reset()
                    Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.Report_ConsolidadoContenedor.rdlc"
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                    Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                    Me.ReportViewer.MainReportViewer.RefreshReport()

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Reporte_ContenedoresConsolidado ", ex)
                Finally
                    dbmAgrario.Connection_Close()
                End Try
            Else
                ParametrosForm.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace