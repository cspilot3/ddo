Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.DestapeContenedor

    Public Class Report_DestapeContenedor
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Contenedor"
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
            Dim RangoFechasForm As New FormPrecinto()
            Dim FechaProcesoInicio As DateTime
            Dim FechaProcesoFinal As DateTime
            Dim Ot As Slyg.Tools.SlygNullable(Of Integer)
            Dim Precinto As Slyg.Tools.SlygNullable(Of String)

            If RangoFechasForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                FechaProcesoInicio = RangoFechasForm.FechaProcesoInicial
                FechaProcesoFinal = RangoFechasForm.FechaProcesoFinal
                Ot = RangoFechasForm.OT
                Precinto = RangoFechasForm.Precinto

                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dsInforme As DBImaging.SchemaProcess.CTA_Destape_ContenedorDataTable

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    dsInforme = dbmImaging.SchemaProcess.PA_Destape_Contenedor.DBExecute(FechaProcesoInicio, FechaProcesoFinal, Ot, Precinto, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    RangoFechasForm.Close()
                End Try

                Dim InformeReportDataSource As New ReportDataSource("dsDestapeContenedor", CType(dsInforme, DataTable))

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_DestapeContenedor.rdlc"
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