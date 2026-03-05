Imports DBImaging
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Microsoft.Reporting.WinForms

Namespace Reportes.ImagenesExportadas

    Public Class ReportImagenesExportadas
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Imagenes Exportadas"
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
            Dim rangoFechasForm As New FormRangoFechas(False)
            Dim fechaInicio As DateTime
            Dim fechaFinal As DateTime

            If rangoFechasForm.ShowDialog() = DialogResult.OK Then
                fechaInicio = rangoFechasForm.FechaInicial
                fechaFinal = rangoFechasForm.FechaFinal

                Dim dbmImaging As DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Dim dsInforme =
                            dbmImaging.SchemaProcess.PA_Reporte_Imagenes_Exportadas.DBExecute(Program.ImagingGlobal.Entidad, fechaInicio, fechaFinal)

                    Dim informeReportDataSource As New ReportDataSource("dsImagenesExportadas", dsInforme)
                    Dim parametros As New List(Of ReportParameter)
                    parametros.Add(New ReportParameter("FechaInicialReportParameter", fechaInicio.ToString))
                    parametros.Add(New ReportParameter("FechaFinalReportParameter", fechaFinal.ToString))

                    ReportViewer.MainReportViewer.Reset()
                    ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_ImagenesExportadas.rdlc"
                    ReportViewer.MainReportViewer.LocalReport.SetParameters(parametros)
                    ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                    ReportViewer.MainReportViewer.LocalReport.DataSources.Add(informeReportDataSource)
                    ReportViewer.MainReportViewer.RefreshReport()

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    rangoFechasForm.Close()
                End Try

            Else
                rangoFechasForm.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace