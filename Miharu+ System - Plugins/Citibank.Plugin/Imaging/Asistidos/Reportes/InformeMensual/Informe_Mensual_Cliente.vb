Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports System.Windows.Forms
Imports Miharu.Imaging.Library.Reportes

Namespace Imaging.Asistidos.Reportes.InformeMensual

    Public Class Informe_Mensual_Cliente
        Inherits DesktopReport

#Region " Declaraciones "
        Dim _nombreReporte As String = ""
#End Region

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Informe Mensual Clientes"
            End Get
        End Property

        Protected _plugin As AsistidosImagingPlugin

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl, ByVal nPlugin As AsistidosImagingPlugin)
            MyBase.New(nReportViewer)
            _plugin = nPlugin
        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch()
            Dim formFechasRecaudo As New FormFechasRecaudo()
            Dim FechaRecaudoInicia As String
            Dim FechaRecaudoFin As String

            If formFechasRecaudo.ShowDialog() = Windows.Forms.DialogResult.OK Then
                FechaRecaudoInicia = formFechasRecaudo.Fecha_Recaudo_Inicio
                FechaRecaudoFin = formFechasRecaudo.Fecha_Recaudo_Fin

                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
                Dim dsInforme As DataTable

                Try
                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.IntegrationConnectionString)
                    dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    _nombreReporte = String.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("INFORME_MENSUAL_CLIENTE").FirstOrDefault().Formato_Reporte, FechaRecaudoInicia)

                    dsInforme = dbmIntegration.SchemaBcoCitibank.PA_Reporte_Mensual_Impuestos_Asistidos.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, FechaRecaudoInicia, FechaRecaudoFin)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                    formFechasRecaudo.Close()
                End Try

                Dim InformeReportDataSource As New ReportDataSource("dsReporteMensual", CType(dsInforme, DataTable))

                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("MesInforme", Convert.ToDateTime(formFechasRecaudo.Fecha_Recaudo_Inicio).ToString("MMMM").ToUpper()))
                Parametros.Add(New ReportParameter("AnoInforme", Convert.ToDateTime(formFechasRecaudo.Fecha_Recaudo_Inicio).Year.ToString))

                ReportViewer.MainReportViewer.Reset()
                ReportViewer.MainReportViewer.LocalReport.DisplayName = _nombreReporte
                ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Citibank.Plugin.Informe_Mensual_Cliente.rdlc"
                ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
                ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                ReportViewer.MainReportViewer.RefreshReport()
            Else
                formFechasRecaudo.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace