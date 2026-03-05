Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Imaging.Library.Reportes

Namespace Imaging.Actualizaciones.Reportes.Faltantes
    
    Public Class ReportFaltantes
        Inherits DesktopReport
        
#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Faltantes"
            End Get
        End Property

        Protected _plugin As Plugin

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl, ByVal nPlugin As Plugin)
            MyBase.New(nReportViewer)
            _plugin = nPlugin
        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch()
            Dim rangoFechasForm As New FormRangoFechas(False)
            Dim fechaInicio As DateTime
            Dim fechaFinal As DateTime

            If rangoFechasForm.ShowDialog() = DialogResult.OK Then
                fechaInicio = rangoFechasForm.FechaInicial
                fechaFinal = rangoFechasForm.FechaFinal

                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

                Try
                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.CoomevaConnectionString)

                    dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim dsInforme = dbmIntegration.SchemaCoomevaReport.PA_Registros_Faltantes.DBExecute(fechaInicio, fechaFinal, Plugin.Imagenes_Actualizaciones_ProyectoId)

                    Dim informeReportDataSource As New ReportDataSource("ReporteFaltantesActDataSet", dsInforme)
                    'Dim parametros As New List(Of ReportParameter)
                    'parametros.Add(New ReportParameter("fechaInicio", fechaInicio.ToString))
                    'parametros.Add(New ReportParameter("fechaFinal", fechaFinal.ToString))

                    ReportViewer.MainReportViewer.Reset()

                    ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Coomeva.Plugin.ReportFaltantes.rdlc"
                    'ReportViewer.MainReportViewer.LocalReport.SetParameters(parametros)
                    ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                    ReportViewer.MainReportViewer.LocalReport.DataSources.Add(informeReportDataSource)
                    ReportViewer.MainReportViewer.RefreshReport()

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                    rangoFechasForm.Close()
                End Try

            Else
                rangoFechasForm.Close()
            End If
        End Sub

#End Region

    End Class
    
End Namespace

