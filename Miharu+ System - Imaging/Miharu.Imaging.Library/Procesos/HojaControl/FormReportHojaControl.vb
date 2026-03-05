Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports System.Reflection
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports Slyg.Tools.Imaging

Public Class FormReportHojaControl
    Inherits DesktopReportHojaControl

#Region " Propiedades "

    Friend Shared DesktopGlobal As New DesktopGlobal()
    Public Overrides ReadOnly Property ReportName As String
        Get
            Return "Formato Rotulo de Carpeta"
        End Get
    End Property

#End Region

#Region " Constructores "

    Public Sub New(ByRef nReportViewer As DesktopReportViewerHojaControl)
        MyBase.New(nReportViewer)

    End Sub
    Friend Shared ReadOnly Property AssemblyName() As String
        Get
            Return [Assembly].GetExecutingAssembly().GetName().Name
        End Get
    End Property
#End Region

#Region " Metodos "

    Public Overrides Sub Launch(Hoja As DataTable)
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Dim FechaElaboracion As String = Nothing

        Try
            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
            FechaElaboracion = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@FechaElaboracionHC").Item(0).Valor_Parametro_Sistema
        Catch ex As Exception

        End Try

        Me.ReportViewer.MainReportViewer1.Reset()
        Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.ReportHojaControl.rdlc"
        Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("HojaControl", Hoja))
        Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAELABORACION", FechaElaboracion))
        Me.ReportViewer.MainReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        Me.ReportViewer.MainReportViewer1.RefreshReport()
    End Sub

    Public Overrides Sub Launch(Ruta As String, Cedulas As System.Data.DataTable)

    End Sub
#End Region

End Class