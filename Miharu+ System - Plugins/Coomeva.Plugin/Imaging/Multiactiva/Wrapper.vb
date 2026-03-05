Imports Coomeva.Plugin.Imaging.Anexos.Exportar
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Desktop.Library

Namespace Imaging.Multiactiva

    Public Class Wrapper

#Region " Declaraciones "

        Public _Plugin As Plugin = Nothing
        Public WithEvents MultiactivaToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents MultiactivaPublicacionToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents MultiactivaProcesosEspecialesToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents MultiactivaRechazosToolStripMenuItem As New ToolStripMenuItem()

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

                _Plugin.WorkSpace.ProcesosEspecialesToolStripMenuItem.Visible = False

                _Plugin.WorkSpace.MainMenuStrip.Items.AddRange(New ToolStripItem() {MultiactivaToolStripMenuItem, MultiactivaProcesosEspecialesToolStripMenuItem})

                'Menu Multiactiva Procesos Especiales Padre
                MultiactivaProcesosEspecialesToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {MultiactivaRechazosToolStripMenuItem})
                MultiactivaProcesosEspecialesToolStripMenuItem.Name = "MultiactivaProcesosEspecialesToolStripMenuItem"
                MultiactivaProcesosEspecialesToolStripMenuItem.Size = New Drawing.Size(193, 22)
                MultiactivaProcesosEspecialesToolStripMenuItem.Text = "Procesos Especiales Multiactiva"
                MultiactivaProcesosEspecialesToolStripMenuItem.Enabled = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Coomeva_Multiactiva.Path)


                'Menu Multiactiva Procesos Especiales Hijo
                MultiactivaRechazosToolStripMenuItem.Name = "MultiactivaRechazosToolStripMenuItem"
                MultiactivaRechazosToolStripMenuItem.Size = New Drawing.Size(193, 22)
                MultiactivaRechazosToolStripMenuItem.Text = "Rechazos"
                MultiactivaRechazosToolStripMenuItem.Enabled = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Coomeva_Multiactiva.Coomeva_Multiactiva_Funcionario.Rechazos)


                'Menu Multiactiva Padre
                MultiactivaToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {MultiactivaPublicacionToolStripMenuItem})
                MultiactivaToolStripMenuItem.Name = "MultiactivaToolStripMenuItem"
                MultiactivaToolStripMenuItem.Size = New Drawing.Size(193, 22)
                MultiactivaToolStripMenuItem.Text = "Multiactiva Ciudades"
                MultiactivaToolStripMenuItem.Enabled = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Coomeva_Multiactiva.Path)


                'Menu Multiactiva Hijo
                MultiactivaPublicacionToolStripMenuItem.Name = "PublicacionToolStripMenuItem"
                MultiactivaPublicacionToolStripMenuItem.Size = New Drawing.Size(193, 22)
                MultiactivaPublicacionToolStripMenuItem.Text = "Publicación"
                MultiactivaPublicacionToolStripMenuItem.Enabled = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Coomeva_Multiactiva.Coomeva_Multiactiva_Funcionario.Publicacion)



            Catch ex As Exception
                Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Coomeva al workspace, " + ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Public Sub DeshacerCambios()

        End Sub

#End Region

        Private Sub MultiactivaPublicacionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MultiactivaPublicacionToolStripMenuItem.Click
            Dim Publicacion = New Imaging.Multiactiva.Procesos.Publicacion.FormPublicacion(_Plugin)
            Publicacion.ShowDialog()
        End Sub

        Private Sub MultiactivaRechazosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MultiactivaRechazosToolStripMenuItem.Click
            Dim Rechazos = New Imaging.Multiactiva.Procesos.Rechazos.FormRechazos_ValidacionesCampos(_Plugin)
            Rechazos.ShowDialog()
        End Sub

    End Class

End Namespace