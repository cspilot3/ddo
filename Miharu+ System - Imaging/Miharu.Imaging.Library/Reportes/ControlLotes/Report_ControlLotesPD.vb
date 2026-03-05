Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Library.MiharuDMZ

Namespace Reportes.ControlLotes

    Public Class Report_ControlLotesPD
        Inherits DesktopReport

#Region " Declaraciones "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte de Lotes por Detalle Diario"
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
            Dim RangoFechasForm As New FormRangoFechasLotes("DetalleLotes")
            Dim FechaInicio As DateTime
            Dim FechaFinal As DateTime
            Dim Oficina As String
            Dim Estado As String

            If RangoFechasForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                FechaInicio = RangoFechasForm.FechaInicial
                FechaFinal = RangoFechasForm.FechaFinal
                Oficina = RangoFechasForm.Oficina.Value.ToString()
                Estado = RangoFechasForm.Estado.Value

                'Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim InformeDataTable As New DBImaging.SchemaProcess.CTA_Reporte_ControlLote_DetalleDataTable

                Try
                    'dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    'dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    InformeDataTable.Clear()
                    'InformeDataTable = dbmImaging.SchemaProcess.PA_Reporte_ControlLote_Detalle.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaInicio, FechaFinal, Oficina, Estado)

                    Dim QueryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Reporte_ControlLote_Detalle]", New List(Of QueryParameter) From {
                                          New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad.ToString()},
                                                      New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                                      New QueryParameter With {.name = "FechaInicial", .value = FechaInicio.ToString("yyyy-MM-dd")},
                                                      New QueryParameter With {.name = "FechaFinal", .value = FechaFinal.ToString("yyyy-MM-dd")},
                                                      New QueryParameter With {.name = "Oficina", .value = Oficina.ToString()},
                                                      New QueryParameter With {.name = "Estado", .value = Estado.ToString()}
                                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

                    InformeDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Reporte_ControlLote_DetalleDataTable(), QueryResponse.dataTable), DBImaging.SchemaProcess.CTA_Reporte_ControlLote_DetalleDataTable)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    'If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    RangoFechasForm.Close()
                End Try

                Dim InformeReportDataSource As New ReportDataSource("DS_ControlLotes_PD", CType(InformeDataTable, DataTable))
                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("FechaIni", FechaInicio.Date.ToString))
                Parametros.Add(New ReportParameter("FechaFin", FechaFinal.Date.ToString))
                Parametros.Add(New ReportParameter("Oficina", Oficina.ToString))

                'Dim recursos = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames()

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_ControlLotes_PD.rdlc"
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