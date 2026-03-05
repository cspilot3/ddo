Imports System.Windows.Forms
Imports DBAgrario
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Imaging.Reportes.DocumentosIlegibles

    Public Class Report_DocumentosIlegibles
        Inherits DesktopReport

#Region " Declaraciones "

        Public _plugin As BanagrarioImagingPlugin = Nothing

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Documentos Ilegibles"
            End Get
        End Property

#End Region

#Region " Constructores "


        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl, ByVal nPlugin As BanagrarioImagingPlugin)
            MyBase.New(nReportViewer)
            _plugin = nPlugin
        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch()
            Dim Report_DocumentosIlegibles_Parametros As New Report_DocumentosIlegibles_Parametros(_plugin)

            Dim FechaProceso As String
            Dim numCiudad As Integer
            Dim numOficinaTipo As Integer

            If Report_DocumentosIlegibles_Parametros.ShowDialog = DialogResult.OK Then
                FechaProceso = (Report_DocumentosIlegibles_Parametros.Fechaproceso.ToString("yyyy/MM/dd"))
                numCiudad = CInt(Report_DocumentosIlegibles_Parametros.CiudadComboBox.SelectedValue)
                numOficinaTipo = CInt(Report_DocumentosIlegibles_Parametros.TipoOficinaComboBox.SelectedValue)
                Dim dbmAgrario As New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
                Dim Rprtilegibles As DataTable
                Try
                    dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    Rprtilegibles = dbmAgrario.SchemaReport.PA_Reporte_Documentos_Ilegibles.DBExecute(FechaProceso, numCiudad, numOficinaTipo)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Pluging", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    dbmAgrario.Connection_Close()
                    Report_DocumentosIlegibles_Parametros.Close()
                End Try
                'Dim InformeReportDataSource As New ReportDataSource("DS_Reporte_Productividad", CType(InformeDataTable, DataTable))
                Dim InformeReportDataSource As New ReportDataSource("ReportDataSet", Rprtilegibles)
                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("Fecha", FechaProceso.ToString))
                Parametros.Add(New ReportParameter("ciudad", numOficinaTipo.ToString))
                Parametros.Add(New ReportParameter("oficina", numCiudad.ToString))
                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.Report_DocumentosIlegibles.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                Me.ReportViewer.MainReportViewer.RefreshReport()
            Else
                Report_DocumentosIlegibles_Parametros.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace