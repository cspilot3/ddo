Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Citibank.Plugin.Embargos
Imports System.Drawing

Namespace Imaging.Embargos
    Public Class EmbargosImagingWrapper

#Region " Declaraciones "
        Public _plugin As EmbargosImagingPlugin = Nothing
        Public WithEvents ProcesoPluginToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents ConfiguracionImagenesToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents GeneracionCartasToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents EntregaTransportadorToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents CarguedeCartasToolStripMenuItem As New ToolStripMenuItem()
        Public _IntegrationConnectionString As String
        Public _ToolsConnectionString As String
#End Region

#Region " Costructores "
        Public Sub New(ByVal nPlugin As EmbargosImagingPlugin)
            _plugin = nPlugin
        End Sub
#End Region

#Region " Metodos "
        Public Sub AplicarCambios()
            Try
                ProcesoPluginToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ConfiguracionImagenesToolStripMenuItem, GeneracionCartasToolStripMenuItem, EntregaTransportadorToolStripMenuItem, CarguedeCartasToolStripMenuItem})

                ProcesoPluginToolStripMenuItem.Name = "ProcesoPluginToolStripMenuItem"
                ProcesoPluginToolStripMenuItem.Size = New Size(193, 22)
                ProcesoPluginToolStripMenuItem.Visible = True
                If _plugin.Manager.ImagingGlobal.Proyecto = Program.Imagenes_Embargos_ProyectoId Then
                    ProcesoPluginToolStripMenuItem.Text = "Proceso Embargos..."
                Else
                    ProcesoPluginToolStripMenuItem.Text = "Proceso DesEmbargos..."
                End If

                ''Configuración de Logo y Firma ...
                ConfiguracionImagenesToolStripMenuItem.Name = "ConfiguracionImagenesToolStripMenuItem"
                ConfiguracionImagenesToolStripMenuItem.Size = New Size(193, 22)
                ConfiguracionImagenesToolStripMenuItem.Text = "Configuración de Logo y Firma ..."
                ConfiguracionImagenesToolStripMenuItem.Visible = True
                AddHandler ConfiguracionImagenesToolStripMenuItem.Click, AddressOf ConfiguracionImagenesToolStripMenuItem_Click

                ''Generacion de Cartas ...
                GeneracionCartasToolStripMenuItem.Name = "GeneracionCartasToolStripMenuItem"
                GeneracionCartasToolStripMenuItem.Size = New Size(193, 22)
                GeneracionCartasToolStripMenuItem.Text = "Generacion de Cartas"
                GeneracionCartasToolStripMenuItem.Visible = True
                AddHandler GeneracionCartasToolStripMenuItem.Click, AddressOf GeneracionCartasToolStripMenuItem_Click

                ''Entrega a Transportador ...
                EntregaTransportadorToolStripMenuItem.Name = "EntregaTransportadorToolStripMenuItem"
                EntregaTransportadorToolStripMenuItem.Size = New Size(193, 22)
                EntregaTransportadorToolStripMenuItem.Text = "Entrega a Transportador"
                EntregaTransportadorToolStripMenuItem.Visible = True
                AddHandler EntregaTransportadorToolStripMenuItem.Click, AddressOf EntregaTransportadorToolStripMenuItem_Click

                ''Cargue de Cartas
                CarguedeCartasToolStripMenuItem.Name = "CarguedeCartasToolStripMenuItem"
                CarguedeCartasToolStripMenuItem.Size = New Size(193, 22)
                CarguedeCartasToolStripMenuItem.Text = "Cargue Acuse"
                CarguedeCartasToolStripMenuItem.Visible = True
                AddHandler CarguedeCartasToolStripMenuItem.Click, AddressOf CarguedeCartasToolStripMenuItem_Click

                _plugin.WorkSpace.MainMenuStrip.Items.AddRange(New ToolStripItem() {ProcesoPluginToolStripMenuItem})

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Citibank - Embargos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Public Sub DeshacerCambios()

        End Sub

        Public Sub ConfiguracionImagenesToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim ConfiguracionImagenesForm = New Forms.FormConfigImagenes(_plugin)
            ConfiguracionImagenesForm.Show()

        End Sub

        Public Sub GeneracionCartasToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim GenerarForm = New Forms.FormGenerarCartas(_plugin)
            GenerarForm.Show()
        End Sub

        Public Sub EntregaTransportadorToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim EntregarTranspForm = New Forms.FormEntregaTransportador(_plugin)
            EntregarTranspForm.Show()
        End Sub

        Public Sub CarguedeCartasToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim CargueCartas = New Forms.FormCargueCartas(_plugin)
            CargueCartas.Show()
        End Sub

#End Region

    End Class
End Namespace

