Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Library.MiharuDMZ


Namespace Reportes.ControlLotes

    Public Class Report_ControlLotesAD
        Inherits DesktopReport
        Dim InformeDataTable As New DBImaging.SchemaProcess.CTA_Reporte_ControlLote_AcumuladoDataTable
        Dim FechaInicio As Date
        Dim FechaFinal As Date

#Region " Declaraciones "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte Estado de Lotes Acumulado"
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
            Dim RangoFechasForm As New FormMes("AcumuladoLotes")

            Dim Oficina As String
            Dim Estado As String

            If RangoFechasForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                FechaInicio = CDate(RangoFechasForm.Mes.Value.Substring(0, 8).Replace("/", "-") + "-01")
                FechaFinal = New Date(FechaInicio.Year, FechaInicio.Month, Date.DaysInMonth(FechaInicio.Year, FechaInicio.Month))
                Oficina = RangoFechasForm.Oficina.Value
                Estado = RangoFechasForm.Estado.Value

                'Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                InformeDataTable = New DBImaging.SchemaProcess.CTA_Reporte_ControlLote_AcumuladoDataTable
                Try
                    'dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    'dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    InformeDataTable.Clear()
                    'InformeDataTable = dbmImaging.SchemaProcess.PA_Reporte_ControlLote_Acumulado.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaInicio, FechaFinal, Oficina, Estado)

                    Dim QueryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Reporte_ControlLote_Acumulado]", New List(Of QueryParameter) From {
                                          New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad.ToString()},
                                                      New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                                      New QueryParameter With {.name = "FechaInicial", .value = FechaInicio.ToString("yyyy-MM-dd")},
                                                      New QueryParameter With {.name = "FechaFinal", .value = FechaFinal.ToString("yyyy-MM-dd")},
                                                      New QueryParameter With {.name = "Oficina", .value = Oficina.ToString()},
                                                      New QueryParameter With {.name = "Estado", .value = Estado.ToString()}
                                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

                    InformeDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Reporte_ControlLote_AcumuladoDataTable(), QueryResponse.dataTable), DBImaging.SchemaProcess.CTA_Reporte_ControlLote_AcumuladoDataTable)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    'If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    RangoFechasForm.Close()
                End Try
                Dim MesConsulta As String = FechaInicio.ToString("MMMM yyyy", New Globalization.CultureInfo("es-ES")).ToUpper

                'Exportación csv
                Dim nombreReporte As String = ""
                nombreReporte = "Reporte_acumulado_estado_lotes_mes_" & FechaInicio.ToString("MMMM") & ".csv"
                Me.ReportViewer.Inicializar(CType(InformeDataTable, DataTable), nombreReporte)

                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("FechaIni", FechaInicio.ToString))
                Parametros.Add(New ReportParameter("FechaFin", FechaFinal.ToString))
                Parametros.Add(New ReportParameter("Oficina", Oficina.ToString))
                Parametros.Add(New ReportParameter("MesConsulta", MesConsulta.ToString))

                Dim InformeReportDataSource As New ReportDataSource("DS_ControlLote_AD", CType(InformeDataTable, DataTable))
                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_ControlLotes_AD.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                Me.ReportViewer.MainReportViewer.RefreshReport()

                'Dim toolStrip As ToolStrip = Nothing
                'toolStrip = CType(ReportViewer.Controls.Find("toolStrip1", True)(0), ToolStrip)
                'If toolStrip IsNot Nothing Then
                '    For Each item As ToolStripItem In toolStrip.Items
                '        If item.Text = "Exportar a CSV" Then
                '            toolStrip.Items.Remove(item)
                '            Exit For
                '        End If
                '    Next
                'End If
                'toolStrip = CType(ReportViewer.Controls.Find("toolStrip1", True)(0), ToolStrip)
                'Dim btnExportCSV As New ToolStripButton("Exportar a CSV")
                'AddHandler btnExportCSV.Click, AddressOf ExportToCSV_Click
                'toolStrip.Items.Add(btnExportCSV)

            Else
                RangoFechasForm.Close()
            End If
        End Sub

        'Private Sub ExportToCSV_Click(sender As Object, e As EventArgs)
        '    ' Usar el DataTable que ya tienes
        '    Dim dt As DataTable = InformeDataTable
        '    Dim sb As New System.Text.StringBuilder()
        '    Dim Separa As String = ConsultarParametroSistema(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, "SeparadorExportarCSV")
        '    If Trim(Separa) = "" Then
        '        MsgBox("No se encontró el separador para archivos CSV, se usará el valor por defecto ;", MsgBoxStyle.Exclamation, Program.AssemblyName)
        '        Separa = ";"
        '    End If
        '    ' Encabezados
        '    Dim columnNames As String = String.Join(Separa, dt.Columns.Cast(Of DataColumn).Select(Function(c) c.ColumnName))
        '    sb.AppendLine(columnNames)

        '    ' Filas
        '    For Each row As DataRow In dt.Rows
        '        Dim fields As String = String.Join(Separa, row.ItemArray.Select(Function(field) field.ToString().Replace(",", " ")))
        '        sb.AppendLine(fields)
        '    Next

        '    ' Guardar archivo
        '    Dim saveFileDialog As New SaveFileDialog()

        '    saveFileDialog.FileName = "Reporte_acumulado_estado_lotes_mes_" & FechaInicio.ToString("MMMM") & ".csv"

        '    saveFileDialog.Filter = "CSV files (*.csv)|*.csv"
        '    saveFileDialog.Title = "Guardar como CSV"
        '    If saveFileDialog.ShowDialog() = DialogResult.OK Then
        '        System.IO.File.WriteAllText(saveFileDialog.FileName, sb.ToString())
        '        MessageBox.Show("Exportación a CSV completada.")
        '    End If
        'End Sub

        'Public Function ConsultarParametroSistema(fk_Entidad As Integer, fk_Proyecto As Integer, Nombre_Parametro_Sistema As String) As String
        'Try
        '        Dim queryResponseCampoInactividad As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Config].[PA_Consulta_Parametro_Sistema_Get]", New List(Of QueryParameter) From {
        '                                  New QueryParameter With {.name = "fk_Entidad", .value = CInt(fk_Entidad).ToString()},
        '                                  New QueryParameter With {.name = "fk_Proyecto", .value = CInt(fk_Proyecto).ToString()},
        '                                  New QueryParameter With {.name = "Nombre_Parametro_Sistema", .value = Trim(Nombre_Parametro_Sistema)}
        '                            }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

        '        Return Trim(CStr(queryResponseCampoInactividad.dataTable.Rows(0).Item("Valor_Parametro_Sistema")))
        '    Catch ex As Exception
        '        Return Nothing
        '    End Try

        'End Function
#End Region

    End Class
End Namespace