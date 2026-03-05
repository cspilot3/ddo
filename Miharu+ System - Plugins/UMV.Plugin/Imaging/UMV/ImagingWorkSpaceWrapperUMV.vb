Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Plugins
Imports System.Windows.Forms
Imports Miharu.Desktop.Library

Namespace Imaging.UMV
    Public Class ImagingWorkSpaceWrapperUMV
#Region " Declaraciones "
        Public _Plugin As UMVPlugin = Nothing
        'Private _exportarButton As Button
        Public WithEvents CreacionEstibasToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents CrearToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents ExportarToolStripMenuItem As New ToolStripMenuItem()

#End Region

#Region " Constructores "
        Public Sub New(ByVal nPlugin As UMVPlugin)
            _Plugin = nPlugin
        End Sub
#End Region

#Region " Metodos "
        Public Sub AplicarCambios()
            Try
                'Me._exportarButton = New Button()

                '' BotónExportar Imagen
                'Me._exportarButton.AccessibleDescription = ""
                'Me._exportarButton.Anchor = AnchorStyles.None
                'Me._exportarButton.BackColor = Drawing.Color.LightSteelBlue
                'Me._exportarButton.Enabled = False
                'Me._exportarButton.FlatStyle = FlatStyle.Flat
                'Me._exportarButton.Font = New Drawing.Font("Tahoma", 8.25!, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(0, Byte))
                ''Me._exportarImagenesButton.Image = My.Resources.Resources.export_image
                'Me._exportarButton.ImageAlign = Drawing.ContentAlignment.TopCenter
                'Me._exportarButton.Location = New Drawing.Point(322, 136)
                'Me._exportarButton.Name = "ExportarButton"
                'Me._exportarButton.Size = New Drawing.Size(123, 60)
                'Me._exportarButton.TabIndex = 22
                'Me._exportarButton.Tag = "Ctrl + E"
                'Me._exportarButton.Text = "&Exportar"
                'Me._exportarButton.TextAlign = Drawing.ContentAlignment.BottomCenter
                'Me._exportarButton.UseVisualStyleBackColor = False

                ' Reemplazar Exportar
                'PluginHelper.DisableControl(_Plugin.WorkSpace.ExportarButton)
                'Me._exportarButton = PluginHelper.CloneButton(_Plugin.WorkSpace.ExportarButton)
                'PluginHelper.ReplaceControl(_Plugin.WorkSpace.ExportarButton, _exportarButton)
                '_exportarButton.Visible = True
                '_exportarButton.Enabled = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Exportar)
                'AddHandler Me._exportarButton.Click, AddressOf ExportarButton_Click
                'PluginHelper.DisableControl(_Plugin.WorkSpace.ExportarButton)

                'Menu UMV Padre
                CreacionEstibasToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {CrearToolStripMenuItem, ExportarToolStripMenuItem})
                CreacionEstibasToolStripMenuItem.Name = "CreacionEstibasToolStripMenuItem"
                CreacionEstibasToolStripMenuItem.Size = New Drawing.Size(193, 22)
                CreacionEstibasToolStripMenuItem.Text = "Unidad de Mantenimiento Vial ..."
                CreacionEstibasToolStripMenuItem.Visible = True

                'Menu Hijo Cargue Log - Padre(UMV)
                CrearToolStripMenuItem.Name = "CrearToolStripMenuItem"
                CrearToolStripMenuItem.Size = New Drawing.Size(193, 22)
                CrearToolStripMenuItem.Text = "Crear Estibas"
                CrearToolStripMenuItem.Visible = True
                AddHandler CrearToolStripMenuItem.Click, AddressOf CrearToolStripMenuItem_Click

                'Menu Hijo Cargue Log - Padre(UMV)
                ExportarToolStripMenuItem.Name = "ExportarToolStripMenuItem"
                ExportarToolStripMenuItem.Size = New Drawing.Size(193, 22)
                ExportarToolStripMenuItem.Text = "Exportar"
                ExportarToolStripMenuItem.Visible = True
                AddHandler ExportarToolStripMenuItem.Click, AddressOf ExportarToolStripMenuItem_Click

                _Plugin.WorkSpace.MainMenuStrip.Items.AddRange(New ToolStripItem() {CreacionEstibasToolStripMenuItem})

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

#Region " Eventos "
        'Private Sub ExportarButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        '    Try
        '        Dim exportarImagenesPlugin As New Imaging.UMV.Exportar.FormExportar(_Plugin)
        '        exportarImagenesPlugin.ShowDialog()
        '    Catch ex As Exception
        '        DesktopMessageBoxControl.DesktopMessageShow("Exportar", ex)
        '    End Try
        'End Sub

        Public Sub CrearToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim CreacionEstibas = New Imaging.Estiba.FormCreacionEstibas(_Plugin)
            CreacionEstibas.ShowDialog()
        End Sub

        Public Sub ExportarToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Try
                Dim exportarImagenesPlugin As New Imaging.UMV.Exportar.FormExportar(_Plugin)
                exportarImagenesPlugin.ShowDialog()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Exportar", ex)
            End Try
        End Sub
#End Region
    End Class
End Namespace

