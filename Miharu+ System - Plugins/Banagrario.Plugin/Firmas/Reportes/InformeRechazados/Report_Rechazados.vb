Imports Banagrario.Plugin.Firmas
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports DBImaging
Imports Microsoft.Reporting.WinForms
Imports System.Windows.Forms
Imports Banagrario.Plugin.Firmas.Reportes



Namespace Firmas.Reportes.InformeRechazados

    Public Class Report_Rechazados
        Inherits DesktopReport

#Region " Declaraciones "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Rechazados - Banco Agrario"
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

                    Dim InformeDataTable = dbmFirmas.SchemaFirmasReport.PA_Tarjetas_Rechazadas.DBExecute(-1, -1, -1, -1, 0, Integer.Parse(FechaInicio.ToString("yyyy/MM/dd").Replace("/", "")), Integer.Parse(FechaFinal.ToString("yyyy/MM/dd").Replace("/", "")), 2)
                    Dim InformeReportDataSource As New ReportDataSource("TBL_Tarjetas_RechazadasDataSet", CType(InformeDataTable, DataTable))

                    Me.ReportViewer.MainReportViewer.Reset()
                    Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.Report_Rechazados.rdlc"
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                    Me.ReportViewer.MainReportViewer.RefreshReport()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    dbmFirmas.Connection_Close()
                    RangoFechasForm.Close()
                End Try
            Else
                RangoFechasForm.Close()
            End If
        End Sub


#End Region

    End Class
End Namespace