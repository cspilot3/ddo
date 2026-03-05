Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Microsoft.Reporting.WinForms

Namespace Reportes.ProductividadDetallado

    Public Class Report_ProductividadDetallado
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Productividad Detallado"
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
            Dim RangoFechasForm As New FormRangoFechas(True)
            Dim FechaInicio As DateTime
            Dim FechaFinal As DateTime
            Dim Usuario As Integer = 0

            If RangoFechasForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                FechaInicio = RangoFechasForm.FechaInicial
                FechaFinal = RangoFechasForm.FechaFinal
                If RangoFechasForm.Usuario = Nothing Then
                    Throw New Exception("El campo usuario no puede estar vacio")
                Else
                    Usuario = RangoFechasForm.Usuario
                End If

                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dsInforme As New DBImaging.SchemaProcess.CTA_Productividad_ErrorDataTable

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    dsInforme = dbmImaging.SchemaProcess.PA_Productividad_Error.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaInicio, FechaFinal, Usuario)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    RangoFechasForm.Close()
                End Try

                Dim InformeReportDataSource As New ReportDataSource("dsProductividadDetallado", CType(dsInforme, DataTable))
                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("FechaInicioReportParameter", FechaInicio.ToString))
                Parametros.Add(New ReportParameter("FechaFinalReportParameter", FechaFinal.ToString))

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_ProductividadDetallado.rdlc"
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