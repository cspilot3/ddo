Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Imaging.Reportes.GestionCorreccionLLaves

    Public Class ReportGestionCorreccionLLaves
        Inherits DesktopReport

#Region " Declaraciones "

        Private _plugin As BanagrarioImagingPlugin

#End Region

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte de Correcciones"
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

            Dim fechaProcesoForm As New ReportGestionCorreccionLLavesParametros(_plugin)
            Dim fechaProceso As DateTime


            If fechaProcesoForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                fechaProceso = fechaProcesoForm.FechaProceso

                Dim datos As DataTable

                Dim dbmAgrario As New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)

                Try

                    dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    datos = dbmAgrario.SchemaReport.PA_Get_Correcciones_LLaves.DBExecute(fechaProceso.ToString("yyyy/MM/dd"))


                    Dim informeReportDataSource As New ReportDataSource("Correcciones_LLavesDataSet", datos)
                    Dim parametros As New List(Of ReportParameter)
                    parametros.Add(New ReportParameter("FechaReportParameter", fechaProceso.ToString("yyyy/MM/dd")))

                    Me.ReportViewer.MainReportViewer.Reset()
                    Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.Report_GestionCorreccionLLaves.rdlc"
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                    Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(parametros)
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(informeReportDataSource)
                    Me.ReportViewer.MainReportViewer.RefreshReport()

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("GestionCorreccionLLaves ", ex)
                Finally
                    dbmAgrario.Connection_Close()
                End Try
            Else
                fechaProcesoForm.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace