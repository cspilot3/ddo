Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.VisorReportes.EmpaqueCaja

    Public Class Report_EmpaqueCaja
        Inherits DesktopReport

#Region " Propiedades "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte Empaque por Fecha"
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
            Dim FormParametros As New Report_EmpaqueCaja_Parametros()

            Dim Fecha_Inicio As DateTime
            Dim Fecha_Final As DateTime

            If FormParametros.ShowDialog = DialogResult.OK Then
                Fecha_Inicio = FormParametros.Fecha_Inicio
                Fecha_Final = FormParametros.Fecha_Final

                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Dim EmpaqueCajaDataTable As DataTable

                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    EmpaqueCajaDataTable = dbmArchiving.SchemaReport.PA_Reporte_Empaque.DBExecute(Fecha_Inicio, Fecha_Final, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    dbmArchiving.Connection_Close()
                    FormParametros.Close()
                End Try

                Dim InformeReportDataSource1 As New ReportDataSource("EmpaqueCajaDataSet", EmpaqueCajaDataTable)

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.Report_EmpaqueCaja.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()

                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource1)
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Fecha_Inicio", CStr(Fecha_Inicio)))
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Fecha_Final", CStr(Fecha_Final)))


                Me.ReportViewer.MainReportViewer.RefreshReport()
            Else
                FormParametros.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace