Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Library.MiharuDMZ

Namespace Reportes.ControlLotes

    Public Class Report_ControlLotesSobrantes
        Inherits DesktopReport

#Region " Declaraciones "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte de Sobrantes"
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
            Dim EfectividadForm As New FormEfectividad("ReporteSobrantes")
            Dim FechaInicio As DateTime
            Dim FechaFinal As DateTime
            Dim BancoEpago As Integer
            Dim Sede As Integer
            Dim Oficina As String
            Dim ColaD As Integer

            If EfectividadForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                FechaInicio = EfectividadForm.FechaInicial
                FechaFinal = EfectividadForm.FechaFinal
                BancoEpago = EfectividadForm.BancoEpago
                Sede = EfectividadForm.Sede
                Oficina = EfectividadForm.Oficina.Value
                ColaD = EfectividadForm.ColaDocumental.Value

                'Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim InformeDataTable As New DBImaging.SchemaProcess.CTA_Reporte_ControlLote_SobrantesDataTable

                Try
                    'dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    'dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    InformeDataTable.Clear()
                    'InformeDataTable = dbmImaging.SchemaProcess.PA_Reporte_ControlLote_Sobrantes.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaInicio, FechaFinal, BancoEpago, Sede, Oficina, ColaD)

                    Dim QueryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Reporte_ControlLote_Sobrantes]", New List(Of QueryParameter) From {
                                         New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad.ToString()},
                                                      New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                                      New QueryParameter With {.name = "FechaInicial", .value = FechaInicio.ToString("yyyy-MM-dd")},
                                                      New QueryParameter With {.name = "FechaFinal", .value = FechaFinal.ToString("yyyy-MM-dd")},
                                                      New QueryParameter With {.name = "BancoEpago", .value = BancoEpago.ToString()},
                                                      New QueryParameter With {.name = "Sede", .value = Sede.ToString()},
                                                      New QueryParameter With {.name = "Oficina", .value = Oficina.ToString()},
                                                      New QueryParameter With {.name = "ColaDocumental", .value = ColaD.ToString()}
                                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

                    InformeDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Reporte_ControlLote_SobrantesDataTable(), QueryResponse.dataTable), DBImaging.SchemaProcess.CTA_Reporte_ControlLote_SobrantesDataTable)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    'If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    EfectividadForm.Close()
                End Try

                Dim InformeReportDataSource As New ReportDataSource("DS_Sobrante", CType(InformeDataTable, DataTable))

                'Exportación csv
                Dim nombreReporte As String = ""
                If BancoEpago = 1 Then
                    nombreReporte = "SoporteSobrantesBanco" & FechaInicio.ToString("ddMMyyyy") & ".csv"
                ElseIf BancoEpago = 2 Then
                    nombreReporte = "SoporteSobrantesEP" & FechaInicio.ToString("ddMMyyyy") & ".csv"
                Else
                    nombreReporte = "SoporteSobrantes" & FechaInicio.ToString("ddMMyyyy") & ".csv"
                End If
                Me.ReportViewer.Inicializar(CType(InformeDataTable, DataTable), nombreReporte)

                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("FechaIni", FechaInicio.ToString))
                Parametros.Add(New ReportParameter("FechaFin", FechaFinal.ToString))
                Parametros.Add(New ReportParameter("Oficina", Oficina.ToString))
                Parametros.Add(New ReportParameter("ColaDocumental", ColaD.ToString))

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_ControlLotes_Sobrantes.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                Me.ReportViewer.MainReportViewer.RefreshReport()
            Else
                EfectividadForm.Close()
            End If
        End Sub

#End Region

    End Class
End Namespace