Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Drawing
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Plugins

Namespace Imaging.Asistidos
    Public Class AsistidosImagingWrapper

#Region " Declaraciones "
        Public _plugin As AsistidosImagingPlugin = Nothing
        Public WithEvents ProcesoPluginToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents ConfiguracionImagenesToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents GeneracionCartasToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents EntregaTransportadorToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents CarguedeCartasToolStripMenuItem As New ToolStripMenuItem()
        Public _IntegrationConnectionString As String
        Public _ToolsConnectionString As String
        Private _ExportarButton As Button
#End Region

#Region " Costructores "
        Public Sub New(ByVal nPlugin As AsistidosImagingPlugin)
            _plugin = nPlugin
        End Sub
#End Region

#Region " Metodos "
        Public Sub AplicarCambios()
            Try
                ' Nuevos controles


                'Reportes
                _plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Conciliacion.Reporte_ConciliacionPrecaptura(_plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _plugin))
                _plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.InformeMensual.Informe_Mensual_Cliente(_plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _plugin))

                _ExportarButton = PluginHelper.CloneButton(_plugin.WorkSpace.ExportarButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.ExportarButton, _ExportarButton)
                _plugin.WorkSpace.ExportarButton.Visible = False
                AddHandler _ExportarButton.Click, AddressOf ExportarButton_Click
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Citibank - Asistidos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Public Sub DeshacerCambios()

        End Sub

        Private Sub ExportarButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim exportarImagenesPlugin As New FormExport(Me._plugin)
                exportarImagenesPlugin.ShowDialog()

            Catch ex As Exception
                Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Public Sub ConfiguracionImagenesToolStripMenuItem_Click(sender As Object, e As EventArgs)
            ''Dim ConfiguracionImagenesForm = New Forms.FormConfigImagenes(_plugin)
            ''ConfiguracionImagenesForm.Show()

        End Sub

        Public Sub GeneracionCartasToolStripMenuItem_Click(sender As Object, e As EventArgs)
            'Dim GenerarForm = New Forms.FormGenerarCartas(_plugin)
            'GenerarForm.Show()
        End Sub

        Public Sub EntregaTransportadorToolStripMenuItem_Click(sender As Object, e As EventArgs)
            ''Dim EntregarTranspForm = New Forms.FormEntregaTransportador(_plugin)
            ''EntregarTranspForm.Show()
        End Sub

        Public Sub CarguedeCartasToolStripMenuItem_Click(sender As Object, e As EventArgs)
            'Dim CargueCartas = New Forms.FormCargueCartas(_plugin)
            'CargueCartas.Show()
        End Sub

#End Region

    End Class
End Namespace

