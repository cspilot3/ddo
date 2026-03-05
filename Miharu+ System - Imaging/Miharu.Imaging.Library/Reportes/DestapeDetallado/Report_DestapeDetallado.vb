Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Microsoft.Reporting.WinForms

Public Class Report_DestapeDetallado
    Inherits DesktopReport

#Region " Propiedades "

    Overrides ReadOnly Property ReportName As String
        Get
            Return "Destape Detallado"
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
        Dim FiltrosForm As New Reportes.FormDestape()
        Dim FechaProceso As DateTime
        Dim Punto As Integer

        If FiltrosForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            FechaProceso = FiltrosForm.FechaProceso
            Punto = FiltrosForm.Punto

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dsInforme As New DBImaging.SchemaProcess.CTA_Destape_DetalladoDataTable

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                dsInforme = dbmImaging.SchemaProcess.PA_Destape_Detallado.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, FechaProceso, Punto)
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                FiltrosForm.Close()
            End Try

            Dim InformeReportDataSource As New ReportDataSource("dsDestapeDetallado", CType(dsInforme, DataTable))

            Me.ReportViewer.MainReportViewer.Reset()
            Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_DestapeDetallado.rdlc"
            Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
            Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
            Me.ReportViewer.MainReportViewer.RefreshReport()

        Else
            FiltrosForm.Close()
        End If
    End Sub

#End Region

End Class
