Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.Conciliacion

    Public Class Reporte_ConciliacionPrecaptura
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Conciliación Precaptura"
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
            Dim FormOtCargue As New FormOTCargue()
            Dim Ot As Slyg.Tools.SlygNullable(Of Integer)
            Dim Cargue As Slyg.Tools.SlygNullable(Of Integer)

            If FormOtCargue.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Ot = FormOtCargue.OT
                Cargue = FormOtCargue.Cargue

                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
                Dim dsInforme As DBIntegration.SchemaCITIBANK.CTA_Reporte_Conciliacion_PrecapturaDataTable

                Try
                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager("SlygProvider=SqlServer;Data Source=10.64.18.59\SQLSTD11DDLO2;Initial Catalog=DB_Miharu.Integration;Persist Security Info=True;User ID=u_app_core;Password=Ap$_073U")
                    dbmIntegration.Connection_Open(Program.Sesion.Usuario.id)

                    dsInforme = dbmIntegration.SchemaCITIBANK.PA_Reporte_Conciliacion_Precaptura.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Ot, Cargue)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                    FormOtCargue.Close()
                End Try

                Dim InformeReportDataSource As New ReportDataSource("dsConciliacionPrecaptura", CType(dsInforme, DataTable))

                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("Fecha_inicio_recaudo", dsInforme(0).Fecha_Recaudo))
                Parametros.Add(New ReportParameter("Fecha_fin_recaudo", dsInforme(0).Fecha_Recaudo))
                Parametros.Add(New ReportParameter("Total_registros", dsInforme.Count.ToString))

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Reporte_ConciliacionPrecaptura.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                Me.ReportViewer.MainReportViewer.RefreshReport()

            Else
                FormOtCargue.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace