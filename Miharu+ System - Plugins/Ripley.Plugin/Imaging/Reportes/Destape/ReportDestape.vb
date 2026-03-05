Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Miharu

Namespace Reportes.Destape

    Public Class ReportDestape
        Inherits DesktopReport

#Region " Propiedades "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Conciliacion de Carpetas"
            End Get
        End Property

        Public Property ConnectionString As String

        Public Property UserId As Integer

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl, ByVal nConnectionString As String, nUserId As Integer)
            MyBase.New(nReportViewer)
            ConnectionString = nConnectionString
            UserId = nUserId
        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch()
            'Dim FormParametros As New Report_Facturacion_Parametros()


            'If FormParametros.ShowDialog = Windows.Forms.DialogResult.OK Then
            '    Dim FechaInicio = (FormParametros.FechaInicial)
            '    Dim FechaFinal = (FormParametros.FechaFinal)

            '    Dim DBMLoanFactory As New DB_Miharu_LoanFactory.DB_Miharu_LoanFactoryDataBaseManager(Me.ConnectionString)

            '    Dim ConciliacionVORDataTable As New DataTable
            '    Dim ConciliacionDestapadasDataTable As New DataTable
            '    Dim ConciliacionFisicosPendientesDataTable As New DataTable
            '    Dim ConciliacionSobrantesDataTable As New DataTable

            '    Try
            '        DBMLoanFactory.Connection_Open(Me.UserId)
            '        ConciliacionVORDataTable = DBMLoanFactory.SchemaReport.PA_Reporte_Conciliacion_Carpetas_Fisicas_VOR.DBExecute(FechaInicio, FechaFinal)
            '        ConciliacionDestapadasDataTable = DBMLoanFactory.SchemaReport.PA_Reporte_Conciliacion_Carpetas_Fisicas_Destapadas.DBExecute(FechaInicio, FechaFinal)
            '        ConciliacionFisicosPendientesDataTable = DBMLoanFactory.SchemaReport.PA_Reporte_Conciliacion_Carpetas_Fisicas_Fisicos_Pendientes.DBExecute(FechaInicio, FechaFinal)
            '        ConciliacionSobrantesDataTable = DBMLoanFactory.SchemaReport.PA_Reporte_Conciliacion_Carpetas_Fisicas_Sobrantes.DBExecute(FechaInicio, FechaFinal)

            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        Return
            '    Finally
            '        DBMLoanFactory.Connection_Close()
            '        FormParametros.Close()
            '    End Try

            '    Dim InformeReportDataSource1 As New ReportDataSource("VORDataSet", ConciliacionVORDataTable)
            '    Dim InformeReportDataSource2 As New ReportDataSource("DestapadasDataSet", ConciliacionDestapadasDataTable)
            '    Dim InformeReportDataSource3 As New ReportDataSource("FisicosPendientesDataSet", ConciliacionFisicosPendientesDataTable)
            '    Dim InformeReportDataSource4 As New ReportDataSource("SobrantesDataSet", ConciliacionSobrantesDataTable)

            '    Dim Parametros As New List(Of ReportParameter)
            '    Parametros.Add(New ReportParameter("FechaInicio", FechaInicio.ToShortDateString))
            '    Parametros.Add(New ReportParameter("FechaFinal", FechaFinal.ToShortDateString))
            '    Me.ReportViewer.MainReportViewer.Reset()
            '    Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "LoanFactory.Plugin.Report_ConciliacionCarpetas.rdlc"
            '    Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
            '    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
            '    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource1)
            '    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource2)
            '    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource3)
            '    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource4)

            '    Me.ReportViewer.MainReportViewer.RefreshReport()
            'Else
            '    FormParametros.Close()
            'End If
        End Sub

#End Region

    End Class
End Namespace