Imports System.Drawing
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports Santander.Plugin.EmbargosDesembargos.Forms

Namespace EmbargosDesembargos
    Public Class Wrapper

#Region " Declaraciones "

        Public _Plugin As ImagingPlugin = Nothing
        Public WithEvents SantanderluginToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents GeneracionCartasToolStripMenuItem As New ToolStripMenuItem()
        Public _SantanderConnectionString As String
        Public _ToolsConnectionString As String

#End Region

#Region " Costructores "

        Public Sub New(ByVal nPlugin As ImagingPlugin)
            _Plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "

#End Region

#Region " Metodos "

        Public Sub AplicarCambios()
            Try


                SantanderluginToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {GeneracionCartasToolStripMenuItem})

                SantanderluginToolStripMenuItem.Name = "SantanderluginToolStripMenuItem"
                SantanderluginToolStripMenuItem.Size = New Size(193, 22)
                SantanderluginToolStripMenuItem.Text = "Santander Embargos-Desembargos..."
                SantanderluginToolStripMenuItem.Visible = True

                ''Generacion de Cartas ...
                GeneracionCartasToolStripMenuItem.Name = "GeneracionCartasToolStripMenuItem"
                GeneracionCartasToolStripMenuItem.Size = New Size(193, 22)
                GeneracionCartasToolStripMenuItem.Text = "Generacion de Cartas"
                GeneracionCartasToolStripMenuItem.Visible = True
                AddHandler GeneracionCartasToolStripMenuItem.Click, AddressOf GeneracionCartasToolStripMenuItem_Click

                _Plugin.WorkSpace.MainMenuStrip.Items.AddRange(New ToolStripItem() {SantanderluginToolStripMenuItem})

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Santander - Embargos Desembargos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub


        Public Sub DeshacerCambios()

        End Sub

        Public Sub GeneracionCartasToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim GenerarForm = New FormGenerarCartas(_Plugin)
            GenerarForm.Show()
        End Sub
#End Region
    End Class
End Namespace
