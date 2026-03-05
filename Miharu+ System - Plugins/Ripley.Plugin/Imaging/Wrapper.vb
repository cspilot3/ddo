Imports Ripley.Plugin.Imaging.Configuracion
Imports Miharu.Desktop.Controls
Imports Ripley.Plugin.Controls
Imports Miharu.Desktop.Controls.DesktopMessageBox

Namespace Imaging

    Public Class Wrapper

#Region " Declaraciones "

        Public _Plugin As Plugin = Nothing
        Public WithEvents ConfiguracionPluginToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents CamposToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents DocumentosToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents ValidacionesToolStripMenuItem As New ToolStripMenuItem()

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
                ''Menu de Configuraciones

                'ConfiguracionPluginToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {CamposToolStripMenuItem, DocumentosToolStripMenuItem, ValidacionesToolStripMenuItem})
                'ConfiguracionPluginToolStripMenuItem.Name = "ConfiguracionPluginToolStripMenuItem"
                'ConfiguracionPluginToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
                'ConfiguracionPluginToolStripMenuItem.Text = "Configuración Ripley ..."

                ''Configuracion Matriz Documento
                'CamposToolStripMenuItem.Name = "CamposToolStripMenuItem"
                'CamposToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
                'CamposToolStripMenuItem.Text = "Campos ..."

                ''Configuracion Matriz Documento
                'DocumentosToolStripMenuItem.Name = "DocumentosToolStripMenuItem"
                'DocumentosToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
                'DocumentosToolStripMenuItem.Text = "Documentos..."


                ''Configuracion Matriz Documento
                'ValidacionesToolStripMenuItem.Name = "ValidacionesToolStripMenuItem"
                'ValidacionesToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
                'ValidacionesToolStripMenuItem.Text = "Validaciones..."

                _Plugin.WorkSpace.MainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ConfiguracionPluginToolStripMenuItem})

                ' SearchControl
                Dim SearchControl = New PluginSearchControlParameters(_Plugin)
                _Plugin.WorkSpace.WorkSpaceImagingSearchControl.SetSearchControl(SearchControl)

                'Reportes
                _Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Facturacion.ReportFacturacion(_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _Plugin))

                _Plugin.WorkSpace.WorkSpaceImagingSearchControl.ReprocesoButton.Visible = False
                _Plugin.WorkSpace.WorkSpaceImagingSearchControl.DeleteFileButton.Visible = False
                _Plugin.WorkSpace.WorkSpaceImagingSearchControl.AdicionarButton.Visible = False
                _Plugin.WorkSpace.WorkSpaceImagingSearchControl.AddDocumentFileButton.Visible = False
                _Plugin.WorkSpace.WorkSpaceImagingSearchControl.DeleteFolderButton.Visible = False

            Catch ex As Exception
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Ripley al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Sub ExportarButton_Click(ByVal sender As Object, ByVal e As EventArgs)

        End Sub

        Public Sub DeshacerCambios()

        End Sub

#End Region

    End Class

End Namespace