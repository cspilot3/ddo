Imports System.Windows.Forms
Imports AccionesValores.Plugin.Imaging.Forms.Exportar
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Plugins

Namespace Imaging

    Public Class Wrapper
        Public _plugin As Plugin = Nothing

        Private _ExportarButton As Button

        Public Sub New(ByVal nPlugin As Plugin)
            _plugin = nPlugin
        End Sub

        Public Sub AplicarCambios()
            Try
                'Exportar 
                _ExportarButton = PluginHelper.CloneButton(_plugin.WorkSpace.ExportarButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.ExportarButton, _ExportarButton)
                _plugin.WorkSpace.ExportarButton.Visible = False
                AddHandler _ExportarButton.Click, AddressOf ExportarButton_Click

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Banagrario al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Sub ExportarButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim exportarImagenesPlugin As FormExport
                exportarImagenesPlugin = New FormExport(Me._plugin)
                exportarImagenesPlugin.ShowDialog()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cierre", ex)
            End Try

        End Sub

    End Class

End Namespace