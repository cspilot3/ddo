Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.VisorReportes.ReporteNovedadesDeceval

    Public Class Report_NovedadesDeceval
        Inherits DesktopReport

#Region " Propiedades "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte Novedades Deceval"
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
            'Dim FormParametros As New Novedades_Deceval_Parametros()


            'If FormParametros.ShowDialog = DialogResult.OK Then

            Dim formExportar As New Reportes.VisorReportes.ReporteNovedadesDeceval.FormExport()
            If formExportar.ShowDialog = DialogResult.OK Then

                Dim Fecha_Inicio As String = formExportar.FechaInicioDateTimePicker.Value.ToString("dd/MM/yyyy")
                Dim Fecha_Final As String = formExportar.FechaFinDateTimePicker.Value.ToString("dd/MM/yyyy")

                'Dim id_Entidad = FormParametros.Entidad
                'Dim id_Proyecto = FormParametros.Proyecto
                'Dim id_Esquema = FormParametros.Esquema

                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Dim Novedades As DataTable
                Dim EsquemaDataTable As DBArchiving.SchemaCore.CTA_Esquema_NombresDataTable

                Dim Entidad As String = Nothing
                Dim Proyecto As String = Nothing

                Dim id_Entidad As Short = CShort(Program.RiskGlobal.Entidad)
                Dim id_Proyecto As Short = CShort(Program.RiskGlobal.Proyecto)
                'Dim id_Esquema As Short = CShort(formExportar.EsquemaDesktopComboBox.SelectedValue)

                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    EsquemaDataTable = dbmArchiving.SchemaCore.CTA_Esquema_Nombres.DBFindByfk_Entidadfk_Proyecto(id_Entidad, id_Proyecto)
                    If EsquemaDataTable.Count > 0 Then
                        Entidad = EsquemaDataTable(0).Nombre_Entidad
                        Proyecto = EsquemaDataTable(0).Nombre_Proyecto
                        'Esquema = EsquemaDataTable(0).Nombre_Esquema
                    End If

                    Dim _idLineaProceso As List(Of String) = New List(Of String)
                    Dim lineas As String = Nothing
                    _idLineaProceso = CType(Program.Sesion.Parameter("_LineasProceso"), Global.System.Collections.Generic.List(Of String))

                    For i = 0 To _idLineaProceso.Count - 1
                        If i = 0 Then
                            lineas = _idLineaProceso(i)
                        Else
                            lineas = lineas & "," & _idLineaProceso(i)
                        End If
                    Next

                    Novedades = dbmArchiving.SchemaReport.PA_Reporte_Novedades_MesaControl.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, lineas)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    dbmArchiving.Connection_Close()
                    'FormParametros.Close()
                End Try


                Dim InformeReportDataSource1 As New ReportDataSource("DataSet1", Novedades)

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.Report_NovedadesDeceval.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()

                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource1)

                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Entidad", CStr(Entidad)))
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Proyecto", CStr(Proyecto)))
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Esquema", "Esquema"))
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Fecha_Inicio", CStr(Fecha_Inicio)))
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Fecha_Final", CStr(Fecha_Final)))

                Me.ReportViewer.MainReportViewer.RefreshReport()
            End If
            'Else
            'FormParametros.Close()
            'End If
        End Sub

#End Region

    End Class

End Namespace