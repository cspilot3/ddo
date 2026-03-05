Imports System.IO
Imports System.Linq
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.ControlLotes

    Public Class Report_ControlLotesEfectividadEP
        Inherits DesktopReport

#Region " Declaraciones "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte de Efectividad Epago"
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
            Dim RangoFechasForm As New FormRangoFechasColaD(True, True)
            Dim FechaInicio As DateTime
            Dim FechaFinal As DateTime
            Dim Oficina As Integer
            Dim ColaD As Integer

            If RangoFechasForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                FechaInicio = RangoFechasForm.FechaInicial
                FechaFinal = RangoFechasForm.FechaFinal
                Oficina = RangoFechasForm.Oficina.Value
                ColaD = RangoFechasForm.ColaDtal.Value

                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim InformeDataTable As New DBImaging.SchemaProcess.CTA_Reporte_ControlLote_Efectividad_EPDataTable

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    InformeDataTable.Clear()
                    InformeDataTable = dbmImaging.SchemaProcess.PA_Reporte_ControlLote_Efectividad_EP.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaInicio, FechaFinal, Oficina, CShort(ColaD))
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    RangoFechasForm.Close()
                End Try

                Dim InformeReportDataSource As New ReportDataSource("DS_Efectividad_EP", CType(InformeDataTable, DataTable))
                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("FechaIni", FechaInicio.ToString))
                Parametros.Add(New ReportParameter("FechaFin", FechaFinal.ToString))
                Parametros.Add(New ReportParameter("Oficina", Oficina.ToString))
                Parametros.Add(New ReportParameter("ColaDocumental", ColaD.ToString))

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_ControlLotes_Efectividad_EP.rdlc"
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