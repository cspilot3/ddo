Imports Banagrario.Plugin.Imaging
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports DBImaging
Imports Microsoft.Reporting.WinForms
Imports System.Windows.Forms
Imports Banagrario.Plugin.Imaging.Reportes

Public Class Report_ProductividadDetallado
    Inherits DesktopReport

#Region " Declaraciones "

    Private _plugin As BanagrarioImagingPlugin

#End Region

#Region " Propiedades "

    Overrides ReadOnly Property ReportName As String
        Get
            Return "Productividad Detallado - Banco Agrario"
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
        Dim RangoFechasForm As New FormRangoFechas(True, _plugin)
        Dim FechaInicio As DateTime
        Dim FechaFinal As DateTime
        Dim Usuario As Integer = 0

        If RangoFechasForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            FechaInicio = RangoFechasForm.FechaInicial
            FechaFinal = RangoFechasForm.FechaFinal
            If RangoFechasForm.Usuario = Nothing Then
                Throw New Exception("El campo usuario no puede estar vacio")
            Else
                Usuario = RangoFechasForm.Usuario
            End If
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
            Dim dsInforme As New SchemaProcess.CTA_Productividad_ErrorDataTable
            Try
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dsInforme = dbmImaging.SchemaProcess.PA_Productividad_Error.DBExecute(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaInicio, FechaFinal, Usuario)
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Finally
                dbmImaging.Connection_Close()
                RangoFechasForm.Close()
            End Try

            Dim InformeReportDataSource As New ReportDataSource("dsProductividadDetallado", CType(dsInforme, DataTable))
            Dim Parametros As New List(Of ReportParameter)
            Parametros.Add(New ReportParameter("FechaInicioReportParameter", FechaInicio.ToString))
            Parametros.Add(New ReportParameter("FechaFinalReportParameter", FechaFinal.ToString))

            Me.ReportViewer.MainReportViewer.Reset()
            Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.Report_ProductividadDetallado.rdlc"
            Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
            Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
            Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
            Me.ReportViewer.MainReportViewer.RefreshReport()

        Else
            RangoFechasForm.Close()
        End If
    End Sub

#End Region

End Class
