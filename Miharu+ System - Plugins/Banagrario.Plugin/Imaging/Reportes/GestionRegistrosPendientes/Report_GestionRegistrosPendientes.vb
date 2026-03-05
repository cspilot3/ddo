Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Imaging.Reportes.GestionRegistrosPendientes

    Public Class Report_GestionRegistrosPendientes
        Inherits DesktopReport

#Region " Declaraciones "

        Private _plugin As BanagrarioImagingPlugin

#End Region

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Gestion de Registros Pendientes"
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
            Dim RangoFechasForm As New Report_GestionRegistrosPendientes_Parametros(_plugin)
            Dim FechaInicio As DateTime
            Dim Regional As Short
            Dim Cob As Short
            Dim Oficina As Short

            If RangoFechasForm.ShowDialog() = DialogResult.OK Then
                FechaInicio = RangoFechasForm.FechaInicial
                Regional = RangoFechasForm.Regional
                Cob = RangoFechasForm.COB
                Oficina = RangoFechasForm.Oficina

                Dim datos As DataTable

                Dim dbmAgrario As New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)

                Try

                    dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    datos = dbmAgrario.SchemaReport.PA_Gestion_Registros_Sobrantes.DBExecute(Regional, _
                                                                                               Cob, _
                                                                                               Oficina, _
                                                                                               FechaInicio.ToString("yyyy/MM/dd"))


                    Dim InformeReportDataSource As New ReportDataSource("GestionRegistrosDataSet", datos)
                    Dim Parametros As New List(Of ReportParameter)
                    Parametros.Add(New ReportParameter("FechaReportParameter", FechaInicio.ToString("yyyy/MM/dd")))

                    Me.ReportViewer.MainReportViewer.Reset()
                    Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.Report_GestionRegistrosPendientes.rdlc"
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                    Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                    Me.ReportViewer.MainReportViewer.RefreshReport()

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("GestionPendientes", ex)
                Finally
                    dbmAgrario.Connection_Close()
                End Try
            Else
                RangoFechasForm.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace