Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Library.MiharuDMZ

Namespace Reportes.Filenet
    Public Class Report_Filenet_Documento
        Inherits DesktopReport

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl)
            MyBase.New(nReportViewer)
        End Sub

#End Region

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte de Publicación Filenet Documento"
            End Get
        End Property

        Public Overrides Sub Launch()
            Dim RangoFechasForm As New FormRangoFechas2("PublicacionFilenetDocumento")
            Dim FechaInicio As DateTime
            Dim FechaFinal As DateTime

            If RangoFechasForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                FechaInicio = RangoFechasForm.FechaInicial
                FechaFinal = RangoFechasForm.FechaFinal

                Dim InformeDataTable As New DBImaging.SchemaProcess.CTA_Reporte_Publicacion_Filenet_Por_DocumentoDataTable

                Try
                    InformeDataTable.Clear()

                    Dim QueryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Reporte_Publicacion_Filenet_Por_Documento]", New List(Of QueryParameter) From {
                                          New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad.ToString()},
                                                      New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                                      New QueryParameter With {.name = "fk_Esquema", .value = "-1".ToString()},
                                                      New QueryParameter With {.name = "FechaInicial", .value = FechaInicio.ToString("yyyy-MM-dd")},
                                                      New QueryParameter With {.name = "FechaFinal", .value = FechaFinal.ToString("yyyy-MM-dd")}
                                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

                    InformeDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Reporte_Publicacion_Filenet_Por_DocumentoDataTable(), QueryResponse.dataTable), DBImaging.SchemaProcess.CTA_Reporte_Publicacion_Filenet_Por_DocumentoDataTable)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    RangoFechasForm.Close()
                End Try
                Dim recursos = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames()
                'Exportación csv
                Me.ReportViewer.Inicializar(Nothing, "")

                Dim InformeReportDataSource As New ReportDataSource("DS_Reporte_Filenet_Documento", CType(InformeDataTable, DataTable))

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_Filenet_Documento.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                Me.ReportViewer.MainReportViewer.RefreshReport()
            Else
                RangoFechasForm.Close()
            End If
        End Sub
    End Class
End Namespace

