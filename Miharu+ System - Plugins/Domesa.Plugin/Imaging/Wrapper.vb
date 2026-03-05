Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Plugins
Imports Domesa.Plugin.Imaging.Exportar

Namespace Imaging

    Public Class Wrapper

#Region " Declaraciones "

        Public _Plugin As Plugin = Nothing

        Private _ExportarButton As Button
        Public WithEvents ConfiguracionPluginToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents ExportarToolStripMenuItem As New ToolStripMenuItem()
#End Region

#Region " Costructores "

        Public Sub New(ByVal nPlugin As Plugin)
            _Plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "

#End Region

#Region " Metodos "

        Public Sub AplicarCambios()
            Try
                'Menu de Configuraciones

                ConfiguracionPluginToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {ExportarToolStripMenuItem})
                ConfiguracionPluginToolStripMenuItem.Name = "ConfiguracionPluginToolStripMenuItem"
                ConfiguracionPluginToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
                ConfiguracionPluginToolStripMenuItem.Text = "Domesa"

                'Configuracion Matriz Documento
                ExportarToolStripMenuItem.Name = "ExportarToolStripMenuItem"
                ExportarToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
                ExportarToolStripMenuItem.Text = "Exportar"
                AddHandler ExportarToolStripMenuItem.Click, AddressOf ExportarToolStripMenuItem_Click

                _Plugin.WorkSpace.MainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ConfiguracionPluginToolStripMenuItem})

                ''Configuracion Matriz Documento
                'DocumentosToolStripMenuItem.Name = "DocumentosToolStripMenuItem"
                'DocumentosToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
                'DocumentosToolStripMenuItem.Text = "Documentos..."


                ''Configuracion Matriz Documento
                'ValidacionesToolStripMenuItem.Name = "ValidacionesToolStripMenuItem"
                'ValidacionesToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
                'ValidacionesToolStripMenuItem.Text = "Validaciones..."

                '_Plugin.WorkSpace.MainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ConfiguracionPluginToolStripMenuItem})

                ' SearchControl
                'Dim SearchControl = New PluginSearchControlParameters(_Plugin)
                '_Plugin.WorkSpace.WorkSpaceImagingSearchControl.SetSearchControl(SearchControl)

                'Reportes
                '_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Facturacion.ReportFacturacion(_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _Plugin))

                '_Plugin.WorkSpace.WorkSpaceImagingSearchControl.ReprocesoButton.Visible = False
                '_Plugin.WorkSpace.WorkSpaceImagingSearchControl.DeleteFileButton.Visible = False
                '_Plugin.WorkSpace.WorkSpaceImagingSearchControl.AdicionarButton.Visible = False
                '_Plugin.WorkSpace.WorkSpaceImagingSearchControl.AddDocumentFileButton.Visible = False
                '_Plugin.WorkSpace.WorkSpaceImagingSearchControl.DeleteFolderButton.Visible = False

                ''Exportar 
                '_ExportarButton = PluginHelper.CloneButton(_Plugin.WorkSpace.ExportarButton)
                'PluginHelper.ReplaceControl(_Plugin.WorkSpace.ExportarButton, _ExportarButton)
                '_Plugin.WorkSpace.ExportarButton.Visible = False
                'AddHandler _ExportarButton.Click, AddressOf ExportarButton_Click

            Catch ex As Exception
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Domesa al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        'Sub ExportarButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        '    Try
        '        Dim exportarImagenesPlugin As New FormExport()
        '        exportarImagenesPlugin.Plugin = Me._Plugin
        '        exportarImagenesPlugin.ShowDialog()
        '    Catch ex As Exception
        '        Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
        '    End Try
        'End Sub

        Public Sub ExportarToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Try
                Dim exportarImagenesPlugin As New FormExport()
                exportarImagenesPlugin.Plugin = Me._Plugin
                exportarImagenesPlugin.ShowDialog()
            Catch ex As Exception
                Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub
        Public Sub DeshacerCambios()

        End Sub

#End Region

    End Class

End Namespace