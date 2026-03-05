Imports System.Windows.Forms
Imports DBAgrario
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Risk.Reportes.EmpaqueContenedor

    Public Class Report_EmpaqueContenedores
        Inherits DesktopReport

#Region " Declaraciones "

        Public _plugin As BanagrarioRiskPlugin = Nothing

        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Empaque Contenedores"
            End Get
        End Property

#End Region

#Region " Constructores "


        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl, ByVal nPlugin As BanagrarioRiskPlugin)
            MyBase.New(nReportViewer)
            _plugin = nPlugin
        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch()
            Dim Report_EmpaqueContenedor_Parametros As New FormEmpaqueContenedor(_plugin)

            Dim FechaProcesoIni As String
            Dim FechaProcesoFin As String
            Dim fk_Documento As Integer
            Dim fk_Sede As Integer

            If Report_EmpaqueContenedor_Parametros.ShowDialog = DialogResult.OK Then

                FechaProcesoIni = Report_EmpaqueContenedor_Parametros.Fecha1DateTimePicker.Value.ToString("yyyy/MM/dd")
                FechaProcesoFin = Report_EmpaqueContenedor_Parametros.Fecha2DateTimePicker.Value.ToString("yyyy/MM/dd")
                fk_Documento = Program.BanagrarioDocumentoTapaId
                fk_Sede = CInt(Report_EmpaqueContenedor_Parametros.SedeDesktopComboBox.SelectedValue)
                '    numCiudad = CInt(Report_DocumentosIlegibles_Parametros.CiudadComboBox.SelectedValue)
                '    numOficinaTipo = CInt(Report_DocumentosIlegibles_Parametros.TipoOficinaComboBox.SelectedValue)
                Dim dbmAgrario As New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
                Dim ReportEmpaqueContenedorData As DataTable
                Try
                    dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    ReportEmpaqueContenedorData = dbmAgrario.SchemaReport.PA_Empaque_Contenedor.DBExecute(fk_Sede, FechaProcesoIni, FechaProcesoFin, fk_Documento)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Pluging", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    dbmAgrario.Connection_Close()
                    Report_EmpaqueContenedor_Parametros.Close()
                End Try
                Dim InformeReportDataSource As New ReportDataSource("ReportDataSet", ReportEmpaqueContenedorData)
                Dim Parametros As New List(Of ReportParameter)
                Parametros.Add(New ReportParameter("Fecha_Inicio", FechaProcesoIni.ToString))
                Parametros.Add(New ReportParameter("Fecha_Fin", FechaProcesoFin.ToString))
                Parametros.Add(New ReportParameter("Sede", fk_Sede.ToString))
                Parametros.Add(New ReportParameter("Documento", fk_Documento.ToString))
                Me.ReportViewer.MainReportViewer.Reset()
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.ReportEmpaqueContenedores.rdlc"
                Me.ReportViewer.MainReportViewer.LocalReport.SetParameters(Parametros)
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Add(InformeReportDataSource)
                Me.ReportViewer.MainReportViewer.RefreshReport()
            Else
                Report_EmpaqueContenedor_Parametros.Close()
            End If
        End Sub

#End Region

    End Class

End Namespace