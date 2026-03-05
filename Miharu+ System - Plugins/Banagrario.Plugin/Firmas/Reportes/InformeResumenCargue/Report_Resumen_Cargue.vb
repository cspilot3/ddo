Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Microsoft.Reporting.WinForms
Imports System.Windows.Forms

Namespace Firmas.Reportes.InformeResumenCargue

    Public Class Report_Resumen_Cargue
        Inherits DesktopReport

#Region " Declaraciones "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Resumen Cargue Log - Banco Agrario"
            End Get
        End Property

        Private _plugin As FirmasImagingPlugin

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl, ByVal nPlugin As FirmasImagingPlugin)
            MyBase.New(nReportViewer)

            _plugin = nPlugin
        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch()
            Dim RangoFechasForm As New FormRangoFechas()
            Dim FechaInicio As DateTime
            Dim FechaFinal As DateTime

            If RangoFechasForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                FechaInicio = RangoFechasForm.FechaInicial
                FechaFinal = RangoFechasForm.FechaFinal

                Dim dbmFirmas As New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)

                Try
                    dbmFirmas.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    Dim InformeDataTable = dbmFirmas.SchemaFirmasReport.PA_Resumen_Cargue.DBExecute(Integer.Parse(FechaInicio.ToString("yyyy/MM/dd").Replace("/", "")))
					
                    Dim InformeReportDataSource As New ReportDataSource("DS_Resumen_Cargue", InformeDataTable)
					Dim Parametros As New List(Of ReportParameter)
					Parametros.Add(New ReportParameter("FechaInicioReportParameter", FechaInicio.ToString))
					Parametros.Add(New ReportParameter("FechaFinalReportParameter", FechaFinal.ToString))

					Me.ReportViewer.MainReportViewer.Reset()
					Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.Report_Resumen_Cargue.rdlc"
					Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
					Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
					Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
					Me.ReportViewer.MainReportViewer.RefreshReport()
					
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    dbmFirmas.Connection_Close()
                End Try
            End If
        End Sub

#End Region

    End Class
End Namespace