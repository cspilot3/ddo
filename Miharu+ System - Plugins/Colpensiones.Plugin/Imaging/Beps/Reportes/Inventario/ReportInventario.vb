Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Imaging.Library.Reportes
Imports System.Windows.Forms

Namespace Imaging.Beps.Reportes.Inventario
    Public Class ReportInventario
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte de Inventario"
            End Get
        End Property

        Protected _plugin As Plugin

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl, ByVal nPlugin As Plugin)
            MyBase.New(nReportViewer)
            _plugin = nPlugin
        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch()
            Dim rangoFechasForm As New FormRangoFechasOT()
            Dim fechaInicio As DateTime
            Dim fechaFinal As DateTime
            Dim OT As Int32

            If rangoFechasForm.ShowDialog() = DialogResult.OK Then
                fechaInicio = rangoFechasForm.FechaInicial
                fechaFinal = rangoFechasForm.FechaFinal
                OT = rangoFechasForm.OT

                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

                Try
                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.ColpensionesConnectionString)

                    dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim dsInforme = dbmIntegration.SchemaColpensionesBEPS.PA_Report_Inventario.DBExecute(CInt(fechaInicio.ToString("yyyyMMdd")), CInt(fechaFinal.ToString("yyyyMMdd")), OT)

                    Dim informeReportDataSource As New ReportDataSource("ReporteInventarioDataSet", CType(dsInforme, DataTable))

                    ReportViewer.MainReportViewer.Reset()

                    ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Colpensiones.Plugin.ReportInventario.rdlc"
                    'ReportViewer.MainReportViewer.LocalReport.SetParameters(parametros)
                    ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                    ReportViewer.MainReportViewer.LocalReport.DataSources.Add(informeReportDataSource)
                    ReportViewer.MainReportViewer.RefreshReport()

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                    rangoFechasForm.Close()
                End Try

            Else
                rangoFechasForm.Close()
            End If
        End Sub

#End Region


    End Class
End Namespace

