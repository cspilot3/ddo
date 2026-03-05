Imports Deceval.Plugin.Imaging.Exportar
Imports Miharu.Desktop.Library.Plugins
Imports System.Windows.Forms

Namespace Imaging.Agrario

    Public Class Wrapper

#Region " Declaraciones "

        Public _Plugin As Plugin = Nothing
        Private _ExportarButton As Button

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
                'Exportar 
                _ExportarButton = PluginHelper.CloneButton(_Plugin.WorkSpace.ExportarButton)
                PluginHelper.ReplaceControl(_Plugin.WorkSpace.ExportarButton, _ExportarButton)
                _Plugin.WorkSpace.ExportarButton.Visible = False
                AddHandler _ExportarButton.Click, AddressOf ExportarButton_Click

            Catch ex As Exception
                Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Coomeva al workspace, " + ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Public Sub DeshacerCambios()

        End Sub
        Sub ExportarButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim exportarImagenesPlugin As New Agrario.Exportar.FormExport()
                exportarImagenesPlugin.Plugin = Me._Plugin
                exportarImagenesPlugin.ShowDialog()

            Catch ex As Exception
                Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub



#End Region

    End Class

End Namespace