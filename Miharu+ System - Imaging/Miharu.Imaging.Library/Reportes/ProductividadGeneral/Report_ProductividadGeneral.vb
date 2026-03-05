Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Microsoft.Reporting.WinForms

Namespace Reportes.ProductividadGeneral

    Public Class Report_ProductividadGeneral
        Inherits DesktopReport

#Region " Declaraciones "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Productividad General"
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl)
            MyBase.New(nReportViewer)
        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch()
            Dim RangoFechasForm As New FormRangoFechas(False)
            Dim FechaInicio As DateTime
            Dim FechaFinal As DateTime

            If RangoFechasForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                FechaInicio = RangoFechasForm.FechaInicial
                FechaFinal = RangoFechasForm.FechaFinal

                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim InformeDataTable As New DBImaging.SchemaProcess.CTA_Reporte_ProductividadDataTable

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    InformeDataTable.Clear()
                    InformeDataTable = dbmImaging.SchemaProcess.PA_Reporte_Productividad.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaInicio, FechaFinal)
                    'InformeDataTable = dbmImaging.SchemaProcess.PA_Reporte_Productividad.DBExecute(FechaInicio, FechaFinal)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    RangoFechasForm.Close()
                End Try


                Dim InformeReportDataSource As New ReportDataSource("DS_Reporte_Productividad", CType(InformeDataTable, DataTable))
                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("FechaInicioReportParameter", FechaInicio.ToString))
                Parametros.Add(New ReportParameter("FechaFinalReportParameter", FechaFinal.ToString))

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_ProductividadGeneral.rdlc"
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
End Namespace