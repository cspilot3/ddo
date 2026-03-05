Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Microsoft.Reporting.WinForms
Imports Miharu.Imaging.Library.Controls

Namespace Reportes.Ciald
    Public Class Report_Bean
        Inherits DesktopReport1

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte Bean"
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewer1Control)
            MyBase.New(nReportViewer)
        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch(datatableDestinatario As DataTable, solicitudSeleccionada As Integer, nombres As String, direccion As String, sede As String, precinto As String)
            Throw New NotImplementedException()
        End Sub

        Public Overrides Sub Launch(Carpeta As String)
            Throw New NotImplementedException()
        End Sub

        Public Overrides Sub Launch(Caja As String, OT As Integer, RotuloCarpetas As Boolean, Reportegenericofuid As Boolean, TipoFuid As Integer, TipoGestion As String)
            Throw New NotImplementedException()
        End Sub

        Public Overrides Sub Launch(FechaRecaudo As Integer)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dsInforme As New DBImaging.SchemaProcess.CTA_Reporte_BeanDataTable

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                dsInforme = dbmImaging.SchemaProcess.PA_Reporte_Bean.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, 1, FechaRecaudo, "-1", "-1")

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Dim recursos = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames()
            Dim InformeReportDataSource As New ReportDataSource("dsReportebean", CType(dsInforme, DataTable))

            Me.ReportViewer.MainReportViewer1.Reset()
            Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_Bean.rdlc"
            Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(InformeReportDataSource)
            Me.ReportViewer.MainReportViewer1.RefreshReport()
        End Sub
#End Region

    End Class
End Namespace
