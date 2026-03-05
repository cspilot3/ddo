Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.VisorReportes.ParametrizacionProyectos

    Public Class Report_ParametrizacionProyectos
        Inherits DesktopReport

#Region " Propiedades "

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Parametrización proyectos"
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
            Dim FormParametros As New Report_ParametrizacionProyectos_Parametros()

            Dim Proyecto As Integer
            Dim Entidad As Integer
            Dim Esquema As Integer

            If FormParametros.ShowDialog = DialogResult.OK Then
                Entidad = FormParametros.Entidad
                Proyecto = FormParametros.Proyecto
                Esquema = FormParametros.Esquema

                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                Dim ParametrizacionProyectosDataTable As DataTable
                Dim ParametrizacionEsquemasDataTable As DataTable
                Dim ConfigCamposDataTable As DataTable
                Dim DocumentosObligatoriosDataTable As DataTable
                Dim ValidacionesDataTable As DataTable
                Dim ParametrizacionImagingDataTable As DataTable
                Dim MontoDataTable As DataTable
                Dim Proyecto_LlaveDataTable As DataTable


                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    ParametrizacionProyectosDataTable = dbmArchiving.SchemaReport.PA_Config_Parametrizacion_Risk.DBExecute(Entidad, Proyecto)
                    ParametrizacionEsquemasDataTable = dbmArchiving.SchemaReport.PA_Config_Parametrizacion_Risk_Esquema.DBExecute(Entidad, Proyecto)
                    ConfigCamposDataTable = dbmCore.SchemaReport.PA_Config_Campos_Documentos.DBExecute(Entidad, Proyecto)
                    DocumentosObligatoriosDataTable = dbmArchiving.SchemaReport.PA_Config_Documentos_Obligatorios.DBExecute(Entidad, Proyecto)
                    ValidacionesDataTable = dbmArchiving.SchemaReport.PA_Config_Validaciones.DBExecute(Entidad, Proyecto)
                    ParametrizacionImagingDataTable = dbmImaging.SchemaReport.PA_Config_Parametrizacion_Imaging_Proyecto.DBExecute(CShort(Entidad), CShort(Proyecto))
                    MontoDataTable = dbmArchiving.SchemaReport.PA_Config_Restriccion_Monto.DBExecute(CShort(Entidad), CShort(Proyecto), CShort(Esquema))
                    Proyecto_LlaveDataTable = dbmArchiving.SchemaReport.PA_Proyecto_Llave.DBExecute(Entidad, Proyecto)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    dbmArchiving.Connection_Close()
                    dbmCore.Connection_Close()
                    dbmImaging.Connection_Close()

                    FormParametros.Close()
                End Try

                Try
                    Dim InformeReportDataSource1 As New ReportDataSource("ParametrizacionRisk_DataSet", ParametrizacionProyectosDataTable)
                    Dim InformeReportDataSource2 As New ReportDataSource("ParametrizacionRiskEsquema_DataSet", ParametrizacionEsquemasDataTable)
                    Dim InformeReportDataSource3 As New ReportDataSource("Config_Campos_DocumentosDataSet", ConfigCamposDataTable)
                    Dim InformeReportDataSource4 As New ReportDataSource("Documentos_Obligatorios_DataSet", DocumentosObligatoriosDataTable)
                    Dim InformeReportDataSource5 As New ReportDataSource("Config_ValidacionesDataSet", ValidacionesDataTable)
                    Dim InformeReportDataSource6 As New ReportDataSource("Config_Parametrizacion_Imaging_Proyecto", ParametrizacionImagingDataTable)
                    Dim InformeReportDataSource7 As New ReportDataSource("MontoDataSet", MontoDataTable)
                    Dim InformeReportDataSource8 As New ReportDataSource("Config_Proyecto_LlaveDataSet", Proyecto_LlaveDataTable)

                    Me.ReportViewer.MainReportViewer.Reset()
                    Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.Report_ParametrizacionProyectos.rdlc"

                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()

                    Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("idProyecto", CStr(Proyecto)))
                    Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(New ReportParameter("EsquemaReportParameter", CStr(Esquema)))


                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource1)
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource2)
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource3)
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource4)
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource5)
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource6)
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource7)
                    Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource8)

                    Me.ReportViewer.MainReportViewer.RefreshReport()
                Catch ex As Exception

                End Try

            Else
                FormParametros.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace