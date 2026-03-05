Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Microsoft.Reporting.WinForms

Namespace Reportes.ControlLotes

    Public Class Report_ControlLotesAT
        Inherits DesktopReport

#Region " Declaraciones "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte de Lotes Acumulado Transmitir"
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
            Dim RangoFechasForm As New FormRangoFechasLotes("ACUMULADOTRANSMITIR")
            Dim FechaInicio As DateTime
            Dim FechaFinal As DateTime
            Dim Oficina As String

            If RangoFechasForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                FechaInicio = RangoFechasForm.FechaInicial
                FechaFinal = RangoFechasForm.FechaFinal
                Oficina = RangoFechasForm.Oficina.Value

                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim InformeDataTable As New DBImaging.SchemaProcess.CTA_Reporte_ControlLote_AT1DataTable

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    InformeDataTable.Clear()
                    InformeDataTable = dbmImaging.SchemaProcess.PA_Reporte_ControlLote_AT1.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaInicio, FechaFinal, CInt(Oficina))
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    RangoFechasForm.Close()
                End Try

                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("FechaIni", FechaInicio.ToString))
                Parametros.Add(New ReportParameter("FechaFin", FechaFinal.ToString))
                Parametros.Add(New ReportParameter("Oficina", Oficina.ToString))
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_01")) = False Then
                    Parametros.Add(New ReportParameter("Dia_01", InformeDataTable.Rows(0).Item("Dia_01").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_01", "Dia_01"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_02")) = False Then
                    Parametros.Add(New ReportParameter("Dia_02", InformeDataTable.Rows(0).Item("Dia_02").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_02", "Dia_02"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_03")) = False Then
                    Parametros.Add(New ReportParameter("Dia_03", InformeDataTable.Rows(0).Item("Dia_03").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_03", "Dia_03"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_04")) = False Then
                    Parametros.Add(New ReportParameter("Dia_04", InformeDataTable.Rows(0).Item("Dia_04").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_04", "Dia_04"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_05")) = False Then
                    Parametros.Add(New ReportParameter("Dia_05", InformeDataTable.Rows(0).Item("Dia_05").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_05", "Dia_05"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_06")) = False Then
                    Parametros.Add(New ReportParameter("Dia_06", InformeDataTable.Rows(0).Item("Dia_06").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_06", "Dia_06"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_07")) = False Then
                    Parametros.Add(New ReportParameter("Dia_07", InformeDataTable.Rows(0).Item("Dia_07").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_07", "Dia_07"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_08")) = False Then
                    Parametros.Add(New ReportParameter("Dia_08", InformeDataTable.Rows(0).Item("Dia_08").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_08", "Dia_08"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_09")) = False Then
                    Parametros.Add(New ReportParameter("Dia_09", InformeDataTable.Rows(0).Item("Dia_09").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_09", "Dia_09"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_10")) = False Then
                    Parametros.Add(New ReportParameter("Dia_10", InformeDataTable.Rows(0).Item("Dia_10").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_10", "Dia_10"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_11")) = False Then
                    Parametros.Add(New ReportParameter("Dia_11", InformeDataTable.Rows(0).Item("Dia_11").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_11", "Dia_11"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_12")) = False Then
                    Parametros.Add(New ReportParameter("Dia_12", InformeDataTable.Rows(0).Item("Dia_12").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_12", "Dia_12"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_13")) = False Then
                    Parametros.Add(New ReportParameter("Dia_13", InformeDataTable.Rows(0).Item("Dia_13").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_13", "Dia_13"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_14")) = False Then
                    Parametros.Add(New ReportParameter("Dia_14", InformeDataTable.Rows(0).Item("Dia_14").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_14", "Dia_14"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_15")) = False Then
                    Parametros.Add(New ReportParameter("Dia_15", InformeDataTable.Rows(0).Item("Dia_15").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_15", "Dia_15"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_16")) = False Then
                    Parametros.Add(New ReportParameter("Dia_16", InformeDataTable.Rows(0).Item("Dia_16").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_16", "Dia_16"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_17")) = False Then
                    Parametros.Add(New ReportParameter("Dia_17", InformeDataTable.Rows(0).Item("Dia_17").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_17", "Dia_17"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_18")) = False Then
                    Parametros.Add(New ReportParameter("Dia_18", InformeDataTable.Rows(0).Item("Dia_18").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_18", "Dia_18"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_19")) = False Then
                    Parametros.Add(New ReportParameter("Dia_19", InformeDataTable.Rows(0).Item("Dia_19").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_19", "Dia_19"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_20")) = False Then
                    Parametros.Add(New ReportParameter("Dia_20", InformeDataTable.Rows(0).Item("Dia_20").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_20", "Dia_20"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_21")) = False Then
                    Parametros.Add(New ReportParameter("Dia_21", InformeDataTable.Rows(0).Item("Dia_21").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_21", "Dia_21"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_22")) = False Then
                    Parametros.Add(New ReportParameter("Dia_22", InformeDataTable.Rows(0).Item("Dia_22").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_22", "Dia_22"))
                End If
                If IsDBNull(InformeDataTable.Rows(0).Item("Dia_23")) = False Then
                    Parametros.Add(New ReportParameter("Dia_23", InformeDataTable.Rows(0).Item("Dia_23").ToString))
                Else
                    Parametros.Add(New ReportParameter("Dia_23", "Dia_23"))
                End If
                InformeDataTable.Rows(0).Delete()
                InformeDataTable.AcceptChanges()

                Dim InformeReportDataSource As New ReportDataSource("DS_ControlLote_AT", CType(InformeDataTable, DataTable))
                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_ControlLotes_AT.rdlc"
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