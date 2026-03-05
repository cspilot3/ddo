Imports Deceval.Plugin.Imaging.Exportar
Imports Deceval.Plugin.Imaging.CruceInformacionDeceval
Imports Miharu.Desktop.Library.Plugins
Imports System.Windows.Forms
Imports System.Drawing
Namespace Imaging.Exportar
    Public Class Wrapper
#Region " Declaraciones "
        Public _Plugin As Plugin = Nothing
        Private _ExportarButton As Button

        Public WithEvents ProcesoPluginToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents CruzarInformacionToolStripMenuItem As New ToolStripMenuItem()
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
                'Menu padre Cruzar Informacion ...
                ProcesoPluginToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {CruzarInformacionToolStripMenuItem})
                ProcesoPluginToolStripMenuItem.Name = "ProcesoPluginToolStripMenuItem"
                ProcesoPluginToolStripMenuItem.Size = New Size(193, 22)
                ProcesoPluginToolStripMenuItem.Text = "Decival"
                ProcesoPluginToolStripMenuItem.Visible = True
                'Menu Hijo Cruzar Informacion ...
                CruzarInformacionToolStripMenuItem.Name = "CruzarInformacionToolStripMenuItem"
                CruzarInformacionToolStripMenuItem.Size = New Size(193, 22)
                CruzarInformacionToolStripMenuItem.Text = "Cruzar Información."
                CruzarInformacionToolStripMenuItem.Visible = True
                AddHandler CruzarInformacionToolStripMenuItem.Click, AddressOf CruzarInformacionToolStripMenuItem_Click
                _Plugin.WorkSpace.MainMenuStrip.Items.AddRange(New ToolStripItem() {ProcesoPluginToolStripMenuItem})
            Catch ex As Exception
                Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Deceval al workspace, " + ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub
        Public Sub DeshacerCambios()
        End Sub
        Sub ExportarButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim exportarImagenesPlugin As New FormExport()
                exportarImagenesPlugin.Plugin = Me._Plugin
                exportarImagenesPlugin.ShowDialog()
            Catch ex As Exception
                Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub
        Public Sub CruzarInformacionToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Try
                Dim _cruzarInformacion As New CruceInformacionDeceval.Exportar.FormCruceInformacionDeceval
                _cruzarInformacion.Plugin = Me._Plugin
                _cruzarInformacion.ShowDialog()
            Catch ex As Exception
                Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub
#End Region
    End Class
End Namespace