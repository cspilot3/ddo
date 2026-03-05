Imports Miharu.Desktop.Library.Config
Imports DBCore
Imports DBArchiving
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms

Public Class FormReporteFacturacion
    Inherits Miharu.Desktop.Library.FormBase

#Region " Declaraciones "
    Private _TableFacturacion As DataTable
#End Region

#Region " Constructores "

    Public Sub New(ByVal TableFacturacion As DataTable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _TableFacturacion = TableFacturacion
    End Sub

#End Region

#Region " Eventos "
    Private Sub FormReporteFacturacion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.FacturacionReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Desktop.rptFacturacion.rdlc"
        Me.FacturacionReportViewer.LocalReport.DataSources.Clear()

        Utilities.NewDataSource(FacturacionReportViewer, "FacturacionDetalle", _TableFacturacion, New List(Of ReportParameter))
        Me.FacturacionReportViewer.RefreshReport()
    End Sub
#End Region
    
End Class