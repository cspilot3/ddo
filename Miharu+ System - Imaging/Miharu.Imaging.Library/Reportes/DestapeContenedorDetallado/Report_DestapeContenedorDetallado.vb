Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Microsoft.Reporting.WinForms

Public Class Report_DestapeContenedorDetallado
    Inherits DesktopReport

#Region " Propiedades "

    Overrides ReadOnly Property ReportName As String
        Get
            Return "Contenedor Detallado"
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
        Dim RangoFechasForm As New Reportes.FormPrecinto()
        Dim FechaProcesoInicio As DateTime
        Dim FechaProcesoFinal As DateTime
        Dim Ot As New Slyg.Tools.SlygNullable(Of Integer)
        Dim Precinto As New Slyg.Tools.SlygNullable(Of String)

        If RangoFechasForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            FechaProcesoInicio = RangoFechasForm.FechaProcesoInicial
            FechaProcesoFinal = RangoFechasForm.FechaProcesoFinal
            Ot = RangoFechasForm.OT
            Precinto = RangoFechasForm.Precinto

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dsInforme As New DBImaging.SchemaProcess.CTA_Destape_Contenedor_DetalladoDataTable

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                dsInforme = dbmImaging.SchemaProcess.PA_Destape_Contenedor_Detallado.DBExecute(FechaProcesoInicio, FechaProcesoFinal, Ot, Precinto, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                RangoFechasForm.Close()
            End Try

            Dim InformeReportDataSource As New ReportDataSource("dsDestapeContenedorDetallado", CType(dsInforme, DataTable))

            Me.ReportViewer.MainReportViewer.Reset()
            Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_DestapeContenedorDetallado.rdlc"
            Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
            Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
            Me.ReportViewer.MainReportViewer.RefreshReport()

        Else
            RangoFechasForm.Close()
        End If
    End Sub

#End Region

End Class
