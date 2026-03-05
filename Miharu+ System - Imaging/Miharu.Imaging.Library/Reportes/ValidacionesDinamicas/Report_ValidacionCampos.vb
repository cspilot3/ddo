Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Microsoft.Reporting.WinForms

Namespace Reportes.ValidacionesDinamicas

    Public Class Report_ValidacionCampos
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Validacion Campos"
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
            Dim FiltrosForm As New FormRangoFechas(False)
            FiltrosForm.UsuarioDesktopComboBox.Visible = False
            FiltrosForm.UsuarioLabel.Visible = False

            Dim FechaProcesoInicio As Integer
            Dim FechaProcesoFinal As Integer

            If FiltrosForm.ShowDialog() = Windows.Forms.DialogResult.OK Then

                FechaProcesoInicio = CInt(Replace(FiltrosForm.FechaInicial.ToString("yyyy/MM/dd"), "/", ""))
                FechaProcesoFinal = CInt(Replace(FiltrosForm.FechaFinal.ToString("yyyy/MM/dd"), "/", ""))

                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Dim dsInforme As New DataTable

                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    dsInforme = dbmCore.SchemaReport.PA_Validaciones_Dinamicas_Resultado_Validacion_Campos.DBExecute(FechaProcesoInicio, FechaProcesoFinal)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    FiltrosForm.Close()
                End Try

                Dim InformeReportDataSource As New ReportDataSource("dsValidacionCampos", CType(dsInforme, DataTable))

                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_ValidacionCampos.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                Me.ReportViewer.MainReportViewer.RefreshReport()
            Else
                FiltrosForm.Close()
            End If
        End Sub

#End Region

    End Class
End Namespace