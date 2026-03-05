Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Imaging.Reportes.TiempoCiudadesConsolidado

    Public Class ReportTiempoCiudadesConsolidado
        Inherits DesktopReport

#Region " Declaraciones "

        Private _plugin As BanagrarioImagingPlugin

#End Region

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte Tiempo Ciudades Consolidado"
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
            Dim parametrosForm As New FormTiempoCiudadesConsolidado(_plugin)
            Dim fechaProceso As DateTime
            Dim fechaMovimiento As DateTime
            Dim regional As Short
            Dim cob As Short
            Dim oficina As Short
            Dim modo As Short

            If parametrosForm.ShowDialog() = DialogResult.OK Then
                fechaProceso = parametrosForm.FechaProceso
                fechaMovimiento = parametrosForm.FechaMovimiento
                regional = parametrosForm.Regional
                cob = parametrosForm.COB
                oficina = parametrosForm.Oficina

                If ((parametrosForm.ModoFechaProceso = True) And (parametrosForm.ModoFechaMovimiento = True)) Then
                    modo = 3
                ElseIf ((parametrosForm.ModoFechaMovimiento = True) And (parametrosForm.ModoFechaProceso = False)) Then
                    modo = 2
                ElseIf (parametrosForm.ModoFechaProceso = True) Then
                    modo = 1
                Else
                    modo = 0
                End If

                Dim datos As DataTable

                Dim dmBanAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

                Try
                    dmBanAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                    dmBanAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    datos = dmBanAgrario.SchemaReport.PA_Tiempo_Ciudades_Consolidado.DBExecute(regional, _
                                                                                          cob, _
                                                                                          oficina, _
                                                                                          fechaProceso.ToString("yyyy/MM/dd"), _
                                                                                          fechaMovimiento.ToString("yyyy/MM/dd"), _
                                                                                          modo)

                    Dim informeReportDataSource As New ReportDataSource("DsContenedoresConsolidado", datos)
                    Dim parametros As New List(Of ReportParameter)
                    parametros.Add(New ReportParameter("FechaReportParameter", fechaProceso.ToString("yyyy/MM/dd")))

                    Me.ReportViewer.MainReportViewer.Reset()
                    Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.Report_TiempoCiudadesConsolidado.rdlc"
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

