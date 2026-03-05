Imports Banagrario.Plugin.Firmas
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports DBImaging
Imports Microsoft.Reporting.WinForms
Imports System.Windows.Forms
Imports Banagrario.Plugin.Firmas.Reportes



Namespace Firmas.Reportes.InformeSobrantes

    Public Class Report_Sobrantes
        Inherits DesktopReport

#Region " Declaraciones "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Sobrantes - Banco Agrario"
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
                Dim InformeDataTable As New DBAgrario.SchemaFirmasReport.CTA_SobrantesDataTable

                Try
                    dbmFirmas.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    InformeDataTable.Clear()
                    InformeDataTable = dbmFirmas.SchemaFirmasReport.PA_Sobrantes.DBExecute(Integer.Parse(FechaInicio.ToString("yyyy/MM/dd").Replace("/", "")), Integer.Parse(FechaFinal.ToString("yyyy/MM/dd").Replace("/", "")))
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    dbmFirmas.Connection_Close()
                    RangoFechasForm.Close()
                End Try


                Dim InformeReportDataSource As New ReportDataSource("DS_Sobrantes", CType(InformeDataTable, DataTable))
                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("FechaInicioReportParameter", FechaInicio.ToString))
                Parametros.Add(New ReportParameter("FechaFinalReportParameter", FechaFinal.ToString))

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.Report_Sobrantes.rdlc"
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