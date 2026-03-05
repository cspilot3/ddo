Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports System.Reflection
Imports Slyg.Tools
Imports Slyg.Tools.CSV
Imports UMV.Plugin.Imaging.UMV


Namespace Imaging.UMV
    Public Class FormImagingWorkSpaceWrapperUMV
        Inherits Imaging.Exportar.FormExporta


#Region " Declaraciones "

        Private _exportarButton As Button
#End Region

#Region " Constructores "
        Public Sub New(ByVal nPlugin As UMVPlugin)
            _plugin = nPlugin
        End Sub
#End Region

#Region " Metodos "
        Public Sub AplicarCambios()
            Try
                Me._exportarButton = New Button()

                ' BotónExportar Imagen
                Me._exportarButton.AccessibleDescription = ""
                Me._exportarButton.Anchor = AnchorStyles.None
                Me._exportarButton.BackColor = Drawing.Color.LightSteelBlue
                Me._exportarButton.Enabled = False
                Me._exportarButton.FlatStyle = FlatStyle.Flat
                Me._exportarButton.Font = New Drawing.Font("Tahoma", 8.25!, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(0, Byte))
                'Me._exportarImagenesButton.Image = My.Resources.Resources.export_image
                Me._exportarButton.ImageAlign = Drawing.ContentAlignment.TopCenter
                Me._exportarButton.Location = New Drawing.Point(322, 136)
                Me._exportarButton.Name = "ExportarButton"
                Me._exportarButton.Size = New Drawing.Size(123, 60)
                Me._exportarButton.TabIndex = 22
                Me._exportarButton.Tag = "Ctrl + E"
                Me._exportarButton.Text = "&Exportar"
                Me._exportarButton.TextAlign = Drawing.ContentAlignment.BottomCenter
                Me._exportarButton.UseVisualStyleBackColor = False

                ' Reemplazar Exportar
                PluginHelper.DisableControl(_plugin.WorkSpace.ExportarButton)
                Me._exportarButton = PluginHelper.CloneButton(_plugin.WorkSpace.ExportarButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.ExportarButton, _exportarButton)
                _exportarButton.Visible = True
                _exportarButton.Enabled = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Exportar)
                AddHandler Me._exportarButton.Click, AddressOf ExportarButton_Click
                PluginHelper.DisableControl(_plugin.WorkSpace.ExportarButton)


            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de UMV al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Public Sub DeshacerCambios()
            Try

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible deshacer los cambios de UMV al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub
#End Region

        Private Sub ExportarButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim exportarImagenesPlugin As New Imaging.Exportar.FormExporta
                exportarImagenesPlugin._plugin = _plugin
                exportarImagenesPlugin.FormExporta_Load(sender, e)
                exportarImagenesPlugin.ShowDialog()
                DesktopMessageBoxControl.DesktopMessageShow("FormExporta", "exportar", DesktopMessageBoxControl.IconEnum.WarningIcon, True, False)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Exportar", ex)
            End Try
        End Sub
    End Class
End Namespace
