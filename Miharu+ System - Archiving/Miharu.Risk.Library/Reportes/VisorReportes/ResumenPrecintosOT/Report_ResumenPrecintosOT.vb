Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Risk.Library.Reportes.VisorReportes.DestapeOT

Namespace Reportes.VisorReportes.ResumenPrecintosOT

    Public Class Report_ResumenPrecintosOT
        Inherits DesktopReport

#Region " Propiedades "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Resumen Precintos a Nivel de OT"
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
            Dim FormParametros As New Report_DestapeOT_Parametros()

            Dim OT As Integer

            If FormParametros.ShowDialog = DialogResult.OK Then
                OT = FormParametros.OT

                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Dim ResumenPrecintosDataTable As DataTable
                Dim OTDataTable As DataTable

                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)


                    ResumenPrecintosDataTable = dbmArchiving.SchemaReport.CTA_Resumen_Precintos_OT.DBFindByid_OT(OT)
                    OTDataTable = dbmArchiving.Schemadbo.RPT_OTs_Encabezado.DBFindByid_Ot(OT)


                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    dbmArchiving.Connection_Close()
                    FormParametros.Close()
                End Try

                Dim InformeReportDataSource1 As New ReportDataSource("ResumenPrecintosDataSet", ResumenPrecintosDataTable)
                Dim InformeReportDataSource2 As New ReportDataSource("OTEncabezado", OTDataTable)

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.Report_ResumenPrecintosOT.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()

                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("OTReportParameter", CStr(OT)))
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource1)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource2)

                Me.ReportViewer.MainReportViewer.RefreshReport()
            Else
                FormParametros.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace