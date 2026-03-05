Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config
Imports DBArchiving
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.VisorReportes.MesaControlFisicos

    Public Class EstadosOT
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "EstadosOT"
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
            Dim Reproceso As New FormParametrosMesaControl()


            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            If Reproceso.ShowDialog = DialogResult.OK Then
                Dim TableEstadosOT = dbmArchiving.Schemadbo.PA_RPT_EstadosOT.DBExecute(Utilities.DInt(Reproceso.ClienteDesktopComboBox.SelectedValue), Utilities.DInt(Reproceso.ProyectoDesktopComboBox.SelectedValue), Utilities.DInt(Reproceso.EsquemaDesktopComboBox.SelectedValue), Utilities.DInt(IIf(Reproceso.OTDesktopTextBox.Text = "", Nothing, Reproceso.OTDesktopTextBox.Text)))
                If Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Count > 0 Then Me.ReportViewer.MainReportViewer.LocalReport.DataSources.RemoveAt(0)
                Utilities.NewDataSource(Me.ReportViewer.MainReportViewer, "EstadosOT", TableEstadosOT)
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.EstadosOT.rdlc"
                Me.ReportViewer.MainReportViewer.RefreshReport()
                dbmArchiving.Connection_Close()
            Else
                Reproceso.Close()
            End If

        End Sub

#End Region

    End Class

End Namespace