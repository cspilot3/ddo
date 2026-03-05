Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.Conciliacion

    Public Class Informe_Mensual_Cliente
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Informe Mensual Clientes"
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
            Dim formFechasRecaudo As New FormFechasRecaudo()
            Dim FechaRecaudoInicia As String
            Dim FechaRecaudoFin As String

            If formFechasRecaudo.ShowDialog() = Windows.Forms.DialogResult.OK Then
                FechaRecaudoInicia = formFechasRecaudo.Fecha_Recaudo_Inicio
                FechaRecaudoFin = formFechasRecaudo.Fecha_Recaudo_Fin

                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
                Dim dsInforme As DBIntegration.SchemaCITIBANK.CTA_Reporte_Conciliacion_PrecapturaDataTable

                Try
                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager("SlygProvider=SqlServer;Data Source=10.64.18.59\SQLSTD11DDLO2;Initial Catalog=DB_Miharu.Integration;Persist Security Info=True;User ID=u_app_core;Password=Ap$_073U")
                    dbmIntegration.Connection_Open(Program.Sesion.Usuario.id)

                    dsInforme = dbmIntegration.SchemaCITIBANK.PA_Reporte_Mensual_Cliente.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, FechaRecaudoInicia, FechaRecaudoFin)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                    formFechasRecaudo.Close()
                End Try

                Dim InformeReportDataSource As New ReportDataSource("dsConciliacionPrecaptura", CType(dsInforme, DataTable))

                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("MesInforme", Convert.ToDateTime(formFechasRecaudo.Fecha_Recaudo_Inicio).ToString("MMMM").ToUpper()))
                Parametros.Add(New ReportParameter("AnoInforme", Convert.ToDateTime(formFechasRecaudo.Fecha_Recaudo_Inicio).Year.ToString))

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Informe_Mensual_Cliente.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                Me.ReportViewer.MainReportViewer.RefreshReport()

            Else
                formFechasRecaudo.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace