Namespace Imaging.Actualizaciones

    Public Class Wrapper

#Region " Declaraciones "

        Public _Plugin As Plugin = Nothing

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As Plugin)
            _Plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "

#End Region

#Region " Metodos "

        Public Sub AplicarCambios()
            Try

                'Reportes
                _Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Facturacion.ReportFacturacionAct(_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _Plugin))
                _Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Faltantes.ReportFaltantes(_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _Plugin))
                _Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.NoProcesados.ReportNoProcesados(_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _Plugin))
                _Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Procesados.ReportProcesados(_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _Plugin))
                _Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ProcesadosTintaOtroColor.ReportProcesadosOtroColor(_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _Plugin))
                _Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Sobrantes.ReportSobrantes(_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _Plugin))

            Catch ex As Exception
                Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Coomeva al workspace, " + ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub



        Public Sub DeshacerCambios()

        End Sub

#End Region

    End Class

End Namespace