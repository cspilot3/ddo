Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBAgrario
Imports Microsoft.Reporting.WinForms

Namespace Risk.Forms.Recepcion

    Public Class ReporteRecepcionPrecintos

#Region " Declaraciones "

        Private _plugin As BanagrarioRiskPlugin
        Private _precintos As DataTable

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioRiskPlugin, nPrecintos As DataTable)
            InitializeComponent()
            _plugin = nBanagrarioDesktopPlugin
            _precintos = nPrecintos
        End Sub

#End Region

#Region " Eventos "

        Private Sub ReporteRecepcionPrecintos_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            GenerarReporte()
        End Sub

#End Region

#Region " Metodos "

        Public Sub GenerarReporte()

            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
                'dbmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                'If ReportViewer.LocalReport.DataSources.Count > 0 Then ReportViewer.LocalReport.DataSources.RemoveAt(0)
                'Utilities.NewDataSource(ReportViewer, "RecepcionPrecintosDataSet", _precintos)
                'Me.ReportViewer.RefreshReport()

                Dim informeReportDataSource As New ReportDataSource("ReportDataSet", _precintos)
                ReportViewer.Reset()
                Me.ReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.ReportRecepcionPrecintos.rdlc"
                Me.ReportViewer.LocalReport.DataSources.Clear()
                Me.ReportViewer.LocalReport.DataSources.Add(informeReportDataSource)
                Me.ReportViewer.RefreshReport()

            Catch ex As Exception
                Try : dbmAgrario.Connection_Close() : Catch : End Try

                DesktopMessageBoxControl.DesktopMessageShow("BuscarReporte", ex)
            End Try

        End Sub

#End Region

    End Class

End Namespace