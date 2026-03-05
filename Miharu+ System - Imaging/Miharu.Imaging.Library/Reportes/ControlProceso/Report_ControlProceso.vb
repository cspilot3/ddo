Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Microsoft.Reporting.WinForms

Namespace Reportes.ControlProceso

    Public Class Report_ControlProceso
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Control de Proceso"
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
            Dim FiltrosForm As New Reportes.FormRangoFechas(False)
            Dim FechaInicial As DateTime
            Dim FechaFinal As DateTime

            If FiltrosForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                FechaInicial = FiltrosForm.FechaInicial
                FechaFinal = FiltrosForm.FechaFinal

                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dsInforme As New DataTable 'DBImaging.SchemaProcess.CTA_DestapeDataTable
                Dim dtEntidad As New DataTable
                Dim dtProyecto As New DataTable

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    dsInforme = dbmImaging.SchemaProcess.PA_ControlProceso.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, FechaInicial, FechaFinal)
                    dtEntidad = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(Program.ImagingGlobal.Entidad)
                    dtproyecto = dbmImaging.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)


                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    FiltrosForm.Close()
                End Try

                'Exportación csv
                Me.ReportViewer.Inicializar(Nothing, "")

                Dim InformeReportDataSource As New ReportDataSource("dsControlProceso", CType(dsInforme, DataTable))
                Dim NombreProyectoParameter As New ReportParameter("Cliente", dtEntidad.Rows(0)("Nombre_Entidad").ToString)
                Dim NombreEntidadParameter As New ReportParameter("Proyecto", dtProyecto.Rows(0)("Nombre_Proyecto").ToString)

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_ControlProceso.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(NombreEntidadParameter)
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(NombreProyectoParameter)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                Me.ReportViewer.MainReportViewer.RefreshReport()

            Else
                FiltrosForm.Close()
            End If
        End Sub

#End Region

    End Class
End Namespace