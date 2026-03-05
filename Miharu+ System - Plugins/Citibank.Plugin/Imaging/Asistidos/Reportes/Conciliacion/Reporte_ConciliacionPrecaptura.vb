Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports System.Windows.Forms
Imports Miharu.Imaging.Library.Reportes

Namespace Imaging.Asistidos.Reportes.Conciliacion

    Public Class Reporte_ConciliacionPrecaptura
        Inherits DesktopReport

#Region " Declaraciones "
        Dim _nombreReporte As String = ""
#End Region

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Conciliación Precaptura"
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
            Dim FormFechasRecaudo As New FormFechasRecaudo()
            Dim FechaRecaudoInicio As String
            Dim FechaRecaudoFin As String

            If FormFechasRecaudo.ShowDialog() = Windows.Forms.DialogResult.OK Then
                FechaRecaudoInicio = FormFechasRecaudo.Fecha_Recaudo_Inicio
                FechaRecaudoFin = FormFechasRecaudo.Fecha_Recaudo_Fin

                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
                Dim dsInforme As DataTable

                Try
                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager("SlygProvider=SqlServer;Data Source=10.64.18.59\SQLSTD11DDLO2;Initial Catalog=DB_Miharu.Integration;Persist Security Info=True;User ID=u_app_core;Password=Ap$_073U")
                    dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    _nombreReporte = String.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("INFORME_CONCILIACION_PRECAPTURA").FirstOrDefault().Formato_Reporte, FechaRecaudoInicio)

                    dsInforme = dbmIntegration.SchemaBcoCitibank.PA_Reporte_Conciliacion_Precaptura.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, FechaRecaudoInicio, FechaRecaudoFin)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                    FormFechasRecaudo.Close()
                End Try

                Dim InformeReportDataSource As New ReportDataSource("dsPrecaptura", CType(dsInforme, DataTable))

                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("Fecha_inicio_recaudo", FormFechasRecaudo.Fecha_Recaudo_Inicio))
                Parametros.Add(New ReportParameter("Fecha_fin_recaudo", FormFechasRecaudo.Fecha_Recaudo_Fin))
                Parametros.Add(New ReportParameter("Total_registros", dsInforme.Rows.Count.ToString))

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.DisplayName = _nombreReporte
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Citibank.Plugin.Reporte_ConciliacionPrecaptura.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                Me.ReportViewer.MainReportViewer.RefreshReport()

            Else
                FormFechasRecaudo.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace