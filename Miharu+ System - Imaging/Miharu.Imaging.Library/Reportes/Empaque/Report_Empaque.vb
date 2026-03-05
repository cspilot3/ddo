Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Microsoft.Reporting.WinForms

Namespace Reportes.Empaque

    Public Class Report_Empaque
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Empaque"
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
                Dim dsInforme As New DBImaging.SchemaProcess.CTA_EmpaqueDataTable

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    dsInforme = dbmImaging.SchemaProcess.PA_Empaque.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, FechaProceso, Punto)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    FiltrosForm.Close()
                End Try

                Dim InformeReportDataSource As New ReportDataSource("dsEmpaque", CType(dsInforme, DataTable))

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_Empaque.rdlc"
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