Imports System.Drawing
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Library.MiharuDMZ

Namespace Reportes.ControlLotes

    Public Class Report_ControlLotesFileNet
        Inherits DesktopReport

#Region " Declaraciones "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte de Publicación Filenet"
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
            Dim RangoFechasForm As New FormRangoFechas2("PublicacionFilenet")
            Dim FechaInicio As DateTime
            Dim FechaFinal As DateTime

            If RangoFechasForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                FechaInicio = RangoFechasForm.FechaInicial
                FechaFinal = RangoFechasForm.FechaFinal

                ' Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim InformeDataTable As New DBImaging.SchemaProcess.CTA_Reporte_ControlLote_FileNetDataTable

                Try
                    'dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    'dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    InformeDataTable.Clear()
                    'InformeDataTable = dbmImaging.SchemaProcess.PA_Reporte_ControlLote_FileNet.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaInicio, FechaFinal)

                    Dim QueryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Reporte_ControlLote_FileNet]", New List(Of QueryParameter) From {
                                          New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad.ToString()},
                                                      New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                                      New QueryParameter With {.name = "FechaInicial", .value = FechaInicio.ToString("yyyy-MM-dd")},
                                                      New QueryParameter With {.name = "FechaFinal", .value = FechaFinal.ToString("yyyy-MM-dd")}
                                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

                    InformeDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Reporte_ControlLote_FileNetDataTable(), QueryResponse.dataTable), DBImaging.SchemaProcess.CTA_Reporte_ControlLote_FileNetDataTable)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    'If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    RangoFechasForm.Close()
                End Try

                'Exportación csv
                Me.ReportViewer.Inicializar(Nothing, "")

                Dim InformeReportDataSource As New ReportDataSource("Ds_Reporte_Filenet", CType(InformeDataTable, DataTable))
                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("FechaIni", FechaInicio.ToString))
                Parametros.Add(New ReportParameter("FechaFin", FechaFinal.ToString))

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_ControlLotes_FileNet.rdlc"
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