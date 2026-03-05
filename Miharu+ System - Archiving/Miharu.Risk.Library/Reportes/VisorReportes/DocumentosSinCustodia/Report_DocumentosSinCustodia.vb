Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.VisorReportes.DocumentosSinCustodia

    Public Class Report_DocumentosSinCustodia
        Inherits DesktopReport

#Region " Propiedades "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Documentos sin Custodia"
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
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Dim DocumentosSinCustodiaDataTable As DataTable
            Dim EsquemaDataTable As DBArchiving.SchemaCore.CTA_Esquema_NombresDataTable

            Dim Entidad As String = String.Empty
            Dim Proyecto As String = String.Empty
            Dim esquema As String = String.Empty

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                DocumentosSinCustodiaDataTable = dbmArchiving.SchemaReport.PA_Documentos_Sin_Custodia.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
                EsquemaDataTable = dbmArchiving.SchemaCore.CTA_Esquema_Nombres.DBFindByfk_Entidadfk_Proyectoid_Esquema(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Nothing)

                If EsquemaDataTable.Count > 0 Then
                    Entidad = EsquemaDataTable(0).Nombre_Entidad
                    Proyecto = EsquemaDataTable(0).Nombre_Proyecto
                    esquema = EsquemaDataTable(0).Nombre_Esquema
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Finally
                dbmArchiving.Connection_Close()
            End Try

            Dim InformeReportDataSource1 As New ReportDataSource("Documentos_Sin_Custodia", DocumentosSinCustodiaDataTable)

            Me.ReportViewer.MainReportViewer.Reset()
            Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.Report_DocumentosSinCustodia.rdlc"

            Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
            Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Entidad", Entidad))
            Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Proyecto", Proyecto))
            Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("Esquema", esquema))

            Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource1)

            Me.ReportViewer.MainReportViewer.RefreshReport()

        End Sub

#End Region

    End Class

End Namespace