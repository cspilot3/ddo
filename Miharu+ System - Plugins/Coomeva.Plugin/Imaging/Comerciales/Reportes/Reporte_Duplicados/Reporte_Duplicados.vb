Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Namespace Imaging.Comerciales.Reportes.Reporte_Duplicados
    Public Class Reporte_Duplicados
        Inherits DesktopReport

#Region " Declaraciones "

        Private _plugin As Plugin

#End Region

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte Duplicados Log"
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl, ByVal nPlugin As Plugin)
            MyBase.New(nReportViewer)
            _plugin = nPlugin
        End Sub

#End Region

#Region " Metodos "
        Public Overrides Sub Launch()
            Dim ParametrosForm As New FormRangoFechas(_plugin)
            Dim FechaProcesoInicial As DateTime
            Dim FechaProcesoFinal As DateTime

            If ParametrosForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                FechaProcesoInicial = ParametrosForm.FechaInicial
                FechaProcesoFinal = ParametrosForm.FechaFinal

                Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(_plugin.CoomevaConnectionString)

                Try
                    dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim Datos = dbmIntegration.SchemaCoomeva.PA_Reporte_Duplicados_Log.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, FechaProcesoInicial.ToString("yyyyMMdd"))
                    'Dim InformeReportDataSource As New ReportDataSource("DsContenedoresConsolidado", Datos)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                End Try

            End If
        End Sub
#End Region
        

        
    End Class
End Namespace

