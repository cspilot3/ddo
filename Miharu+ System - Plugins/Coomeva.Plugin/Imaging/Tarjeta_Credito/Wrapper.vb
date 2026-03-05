Imports Coomeva.Plugin.Imaging.Anexos.Exportar
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Desktop.Library

Namespace Imaging.Tarjeta_Credito

    Public Class Wrapper

#Region " Declaraciones "

        Public _Plugin As Plugin = Nothing
        Public WithEvents CoomevaTCToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents PublicacionToolStripMenuItem As New ToolStripMenuItem()

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

                'Menu Carpeta Unica Padre
                CoomevaTCToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {PublicacionToolStripMenuItem})
                CoomevaTCToolStripMenuItem.Name = "CoomevaTCToolStripMenuItem"
                CoomevaTCToolStripMenuItem.Size = New Drawing.Size(193, 22)
                CoomevaTCToolStripMenuItem.Text = "Coomeva TC Ciudades ..."
                CoomevaTCToolStripMenuItem.Visible = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.CoomevaTC.Path)


                'Menu Hijo Cargue Log - Padre(Carpeta Unica)
                PublicacionToolStripMenuItem.Name = "PublicacionToolStripMenuItem"
                PublicacionToolStripMenuItem.Size = New Drawing.Size(193, 22)
                PublicacionToolStripMenuItem.Text = "Publicación"
                PublicacionToolStripMenuItem.Visible = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.CoomevaTC.CoomevaTC_Funcionario.Publicacion)
                AddHandler PublicacionToolStripMenuItem.Click, AddressOf PublicacionToolStripMenuItem_Click


                _Plugin.WorkSpace.MainMenuStrip.Items.AddRange(New ToolStripItem() {CoomevaTCToolStripMenuItem})

            Catch ex As Exception
                Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Coomeva al workspace, " + ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Public Sub DeshacerCambios()

        End Sub

#End Region

        Private Sub PublicacionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PublicacionToolStripMenuItem.Click
            Dim Publicacion = New Imaging.Tarjeta_Credito.Procesos.Publicacion.FormPublicacion(_Plugin)
            Publicacion.ShowDialog()
        End Sub

    End Class

End Namespace