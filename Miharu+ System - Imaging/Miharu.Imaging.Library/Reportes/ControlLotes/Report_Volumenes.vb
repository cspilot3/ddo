Imports System.Drawing
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Library.MiharuDMZ

Namespace Reportes.ControlLotes

    Public Class Report_Volumenes
        Inherits DesktopReport

#Region " Declaraciones "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte de Volumenes"
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
            Dim RangoFechasForm As New FormRangoFechasLotes("VOLUMENMES")
            Dim FechaInicio As DateTime
            Dim FechaFinal As DateTime
            'Dim Oficina As Integer

            If RangoFechasForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                FechaInicio = RangoFechasForm.FechaInicial
                FechaFinal = RangoFechasForm.FechaFinal
                ' Oficina = RangoFechasForm.Oficina.Value

                'Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim InformeDataTable As New DBImaging.SchemaProcess.CTA_Reporte_VolumenesDataTable

                Try
                    'dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    'dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    InformeDataTable.Clear()
                    'InformeDataTable = dbmImaging.SchemaProcess.PA_Reporte_Volumenes.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaInicio, FechaFinal)

                    Dim QueryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Reporte_Volumenes]", New List(Of QueryParameter) From {
                                        New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad.ToString()},
                                                     New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                                     New QueryParameter With {.name = "FechaInicial", .value = FechaInicio.ToString("yyyy-MM-dd")},
                                                     New QueryParameter With {.name = "FechaFinal", .value = FechaFinal.ToString("yyyy-MM-dd")}
                                               }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

                    InformeDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Reporte_VolumenesDataTable(), QueryResponse.dataTable), DBImaging.SchemaProcess.CTA_Reporte_VolumenesDataTable)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    'If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    RangoFechasForm.Close()
                End Try
                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("FechaIni", FechaInicio.ToString))
                Parametros.Add(New ReportParameter("FechaFin", FechaFinal.ToString))
                If IsDBNull(InformeDataTable.Rows(0).Item("Mes1")) = False Then
                    Parametros.Add(New ReportParameter("Mes1", NombreMes(CInt(InformeDataTable.Rows(0).Item("Mes1").ToString))))
                Else
                    Parametros.Add(New ReportParameter("Mes1", "Mes1"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Mes2")) = False Then
                    Parametros.Add(New ReportParameter("Mes2", NombreMes(CInt(InformeDataTable.Rows(0).Item("Mes2").ToString))))
                Else
                    Parametros.Add(New ReportParameter("Mes2", "Mes2"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Mes3")) = False Then
                    Parametros.Add(New ReportParameter("Mes3", NombreMes(CInt(InformeDataTable.Rows(0).Item("Mes3").ToString))))
                Else
                    Parametros.Add(New ReportParameter("Mes3", "Mes3"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Mes4")) = False Then
                    Parametros.Add(New ReportParameter("Mes4", NombreMes(CInt(InformeDataTable.Rows(0).Item("Mes4").ToString))))
                Else
                    Parametros.Add(New ReportParameter("Mes4", "Mes4"))
                End If
                InformeDataTable.Rows(0).Delete()
                InformeDataTable.AcceptChanges()

                'Exportación csv
                Me.ReportViewer.Inicializar(Nothing, "")

                Dim InformeReportDataSource As New ReportDataSource("DS_Volumenes", CType(InformeDataTable, DataTable))
                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_Volumenes.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                Me.ReportViewer.MainReportViewer.RefreshReport()
            Else
                RangoFechasForm.Close()
            End If
        End Sub

        Private Function NombreMes(ByVal AnoMes As Integer) As String
            Dim Mes As Integer = CInt(Right(CStr(AnoMes), 2))
            Select Case Mes
                Case 1
                    Return "Cant. Ene. " & Left(CStr(AnoMes), 4)
                Case 2
                    Return "Cant. Feb. " & Left(CStr(AnoMes), 4)
                Case 3
                    Return "Cant. Mar. " & Left(CStr(AnoMes), 4)
                Case 4
                    Return "Cant. Abr. " & Left(CStr(AnoMes), 4)
                Case 5
                    Return "Cant. May. " & Left(CStr(AnoMes), 4)
                Case 6
                    Return "Cant. Jun. " & Left(CStr(AnoMes), 4)
                Case 7
                    Return "Cant. Jul. " & Left(CStr(AnoMes), 4)
                Case 8
                    Return "Cant. Ago. " & Left(CStr(AnoMes), 4)
                Case 9
                    Return "Cant. Sep. " & Left(CStr(AnoMes), 4)
                Case 10
                    Return "Cant. Oct. " & Left(CStr(AnoMes), 4)
                Case 11
                    Return "Cant. Nov. " & Left(CStr(AnoMes), 4)
                Case 12
                    Return "Cant. Dic. " & Left(CStr(AnoMes), 4)
                Case Else
                    Return ""
            End Select
        End Function

#End Region

    End Class
End Namespace