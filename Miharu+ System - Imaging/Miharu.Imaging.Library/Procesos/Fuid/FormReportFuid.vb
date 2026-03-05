Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports System.Reflection
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.IO

Public Class FormReportFuid
    Inherits DesktopReportFUID
#Region " Propiedades "

    Friend Shared DesktopGlobal As New DesktopGlobal()
    Public Overrides ReadOnly Property ReportName As String
        Get
            Return "Formato FUID"
        End Get
    End Property

#End Region

#Region " Constructores "

    Public Sub New(ByRef nReportViewer As DesktopReportViewerFUID)
        MyBase.New(nReportViewer)

    End Sub
    Friend Shared ReadOnly Property AssemblyName() As String
        Get
            Return [Assembly].GetExecutingAssembly().GetName().Name
        End Get
    End Property
#End Region

#Region " Metodos "
    Public Overrides Sub Launch(reporteFUID As DataTable, TipoFuid As Integer, Fuidgeneral As Boolean, tipoGestion As String)
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Try
            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
            Dim Fecha = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@FechaUMVFuid")
            Dim FechaValidar As String = Nothing
            For Each item As DataRow In Fecha
                FechaValidar = CStr(item("Valor_Parametro_Sistema"))
            Next
            Dim InformeReportDataSource1 As New ReportDataSource("Fuid", reporteFUID)
            Me.ReportViewer.MainReportViewer1.Reset()
            If CDbl(dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@SerieHistoriaLaboralUMV").Item(0).Valor_Parametro_Sistema) = TipoFuid Then
                Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.ReportFuidHistoriaLaboral.rdlc"

                If FechaValidar = "False" Then
                    Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAAPLICACION", "Abril 27 de 2019"))
                Else
                    Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAAPLICACION", DateTime.Now.ToString()))
                End If
                'Dim EntidadRemitenteHistoriaLaboral = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@EntidadRemitenteHistoriaLaboral").Item(0).Valor_Parametro_Sistema
                ' Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("EntidadRemitenteHistoriaLaboral", EntidadRemitenteHistoriaLaboral))

                Dim EntidadProductoraHistoriaLaboral = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@EntidadProductoraHistoriaLaboral").Item(0).Valor_Parametro_Sistema
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("EntidadProductoraHistoriaLaboral", EntidadProductoraHistoriaLaboral))

                Dim UnidadAdministrativaHistoriaLaboral = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@UnidadAdministrativaHistoriaLaboral").Item(0).Valor_Parametro_Sistema
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("UnidadAdministrativaHistoriaLaboral", UnidadAdministrativaHistoriaLaboral))

                Dim OficinaProductoraHistoriaLaboral = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@OficinaProductoraHistoriaLaboral").Item(0).Valor_Parametro_Sistema
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("OficinaProductoraHistoriaLaboral", OficinaProductoraHistoriaLaboral))

                Dim ObjetoHistoriaLaboral = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@ObjetoHistoriaLaboral").Item(0).Descripcion_Parametro_Sistema
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("ObjetoHistoriaLaboral", ObjetoHistoriaLaboral))

            End If
            If CDbl(dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@SerieNominaUMV").Item(0).Valor_Parametro_Sistema) = TipoFuid Then
                Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.ReportFuidNominas.rdlc"
                If FechaValidar = "False" Then
                    Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAAPLICACION", "Febrero - 2019"))
                Else
                    Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAAPLICACION", DateTime.Now.ToString()))
                End If

                'Dim EntidadRemitenteNominas = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@EntidadRemitenteNominas").Item(0).Valor_Parametro_Sistema
                'Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("EntidadRemitenteNominas", EntidadRemitenteNominas))

                Dim EntidadProductoraNominas = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@EntidadProductoraNominas").Item(0).Valor_Parametro_Sistema
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("EntidadProductoraNominas", EntidadProductoraNominas))

                Dim UnidadAdministrativaNominas = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@UnidadAdministrativaNominas").Item(0).Valor_Parametro_Sistema
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("UnidadAdministrativaNominas", UnidadAdministrativaNominas))

                Dim OficinaProductoraNominas = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@OficinaProductoraNominas").Item(0).Valor_Parametro_Sistema
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("OficinaProductoraNominas", OficinaProductoraNominas))

                Dim ObjetoNominas = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@ObjetoNominas").Item(0).Descripcion_Parametro_Sistema
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("ObjetoNominas", ObjetoNominas))


            End If
            If CDbl(dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@SerieAutoliquidacionUMV").Item(0).Valor_Parametro_Sistema) = TipoFuid Then
                Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.ReportAutoLiquidaciones.rdlc"
                If FechaValidar = "False" Then
                    Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAAPLICACION", "Febrero - 2019"))
                Else
                    Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAAPLICACION", DateTime.Now.ToString()))
                End If
                'Dim EntidadRemitenteAutoliquidacion = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@EntidadRemitenteAutoliquidacion").Item(0).Valor_Parametro_Sistema
                'Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("EntidadRemitenteAutoliquidacion", EntidadRemitenteAutoliquidacion))

                Dim EntidadProductoraAutoliquidacion = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@EntidadProductoraAutoliquidacion").Item(0).Valor_Parametro_Sistema
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("EntidadProductoraAutoliquidacion", EntidadProductoraAutoliquidacion))

                Dim UnidadAdministrativaAutoliquidacion = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@UnidadAdministrativaAutoliquidacion").Item(0).Valor_Parametro_Sistema
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("UnidadAdministrativaAutoliquidacion", UnidadAdministrativaAutoliquidacion))

                Dim OficinaProductoraAutoliquidacion = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@OficinaProductoraAutoliquidacion").Item(0).Valor_Parametro_Sistema
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("OficinaProductoraAutoliquidacion", OficinaProductoraAutoliquidacion))

                Dim ObjetoAutoliquidacion = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@ObjetoAutoliquidacion").Item(0).Descripcion_Parametro_Sistema
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("ObjetoAutoliquidacion", ObjetoAutoliquidacion))

            End If

            Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(InformeReportDataSource1)



            Me.ReportViewer.MainReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            Me.ReportViewer.MainReportViewer1.RefreshReport()
        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        End Try
    End Sub

#End Region
End Class