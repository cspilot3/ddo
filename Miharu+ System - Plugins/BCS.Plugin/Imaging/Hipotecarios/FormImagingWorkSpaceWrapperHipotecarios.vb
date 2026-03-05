Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports Miharu.Desktop.Library

Namespace Imaging.Hipotecarios
    Public Class FormImagingWorkSpaceWrapperHipotecarios

#Region " Declaraciones "
        Public _plugin As HipotecariosPlugin = Nothing

        Public WithEvents HipotecariosToolStripMenuItem As New ToolStripMenuItem()
        'Public WithEvents CargueLogToolStripMenuItem As New ToolStripMenuItem()
        'Public WithEvents CruceToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents ReportesToolStripMenuItem As New ToolStripMenuItem()
        'Public WithEvents ActualizacionEmpaqueToolStripMenuItem As New ToolStripMenuItem()
        'Public WithEvents CierraCajaEmpaqueToolStripMenuItem As New ToolStripMenuItem()
        'Public WithEvents Reportes2ToolStripMenuItem As New ToolStripMenuItem()
        'Public WithEvents SoporteCarpetaUnicaToolStripMenuItem As New ToolStripMenuItem()
        'Public WithEvents EliminacionCargueCarpetaUnicaToolStripMenuItem As New ToolStripMenuItem()
#End Region

#Region " Constructores "
        Public Sub New(ByVal nPlugin As HipotecariosPlugin)
            _plugin = nPlugin
        End Sub
#End Region

#Region " Metodos "
        Public Sub AplicarCambios()
            Try
                'Menu Carpeta Unica Padre
                HipotecariosToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ReportesToolStripMenuItem})
                HipotecariosToolStripMenuItem.Name = "HipotecariosToolStripMenuItem"
                HipotecariosToolStripMenuItem.Size = New Drawing.Size(193, 22)
                HipotecariosToolStripMenuItem.Text = "Hipotecarios ..."
                HipotecariosToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BCS.Path)

                'Menu Hijo Reportes - Padre(Carpeta Unica)
                ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem"
                ReportesToolStripMenuItem.Size = New Drawing.Size(193, 22)
                ReportesToolStripMenuItem.Text = "Generar Reportes"
                ReportesToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BCS.BCSCoordinadorCU.Reportes)
                AddHandler ReportesToolStripMenuItem.Click, AddressOf ReportesToolStripMenuItem_Click


                _plugin.WorkSpace.MainMenuStrip.Items.AddRange(New ToolStripItem() {HipotecariosToolStripMenuItem})
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Caja Social- Hipotecarios al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Public Sub DeshacerCambios()
            Try

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible deshacer los cambios de Caja Social- Hipotecarios al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub
#End Region

#Region "Eventos"
        Private Sub ReportesToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim Reportes = New Forms.Reportes.FormReportes(_plugin)
            Reportes.ShowDialog()
        End Sub
#End Region


    End Class
End Namespace

