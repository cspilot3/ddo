Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Desktop.Library
Imports System.Windows.Forms

Namespace Imaging.Beps
    Public Class Wrapper

#Region " Declaraciones "

        Public _Plugin As Plugin = Nothing
        Public WithEvents BepsToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents PublicacionToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents RechazosToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents RechazosBizagiToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents RechazarBizagiToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents CausalesRechazosBizagiToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents CruceRadicadosToolStripMenuItem As New ToolStripMenuItem()

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As Plugin)
            _Plugin = nPlugin
        End Sub

#End Region

#Region " Metodos "

        Public Sub AplicarCambios()
            Try

                'Menu Padre
                BepsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {RechazosToolStripMenuItem, PublicacionToolStripMenuItem, RechazosBizagiToolStripMenuItem, CruceRadicadosToolStripMenuItem})
                BepsToolStripMenuItem.Name = "BepsToolStripMenuItem"
                BepsToolStripMenuItem.Size = New Drawing.Size(193, 22)
                BepsToolStripMenuItem.Text = "Beps ..."
                BepsToolStripMenuItem.Visible = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Colpensiones_Beps.Path)

                'Menu Hijo de BepsToolStripMenuItem
                RechazosToolStripMenuItem.Name = "RechazosToolStripMenuItem"
                RechazosToolStripMenuItem.Size = New Drawing.Size(193, 22)
                RechazosToolStripMenuItem.Text = "Rechazos"
                RechazosToolStripMenuItem.Visible = _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_Validos
                AddHandler RechazosToolStripMenuItem.Click, AddressOf RechazosToolStripMenuItem_Click

                'Menu Hijo de BepsToolStripMenuItem
                PublicacionToolStripMenuItem.Name = "PublicacionToolStripMenuItem"
                PublicacionToolStripMenuItem.Size = New Drawing.Size(193, 22)
                PublicacionToolStripMenuItem.Text = "Publicación"
                PublicacionToolStripMenuItem.Visible = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Colpensiones_Beps.Colpensiones_Beps_Funcionario.Publicacion)
                AddHandler PublicacionToolStripMenuItem.Click, AddressOf PublicacionToolStripMenuItem_Click

                'Menu Hijo de BepsToolStripMenuItem - Padre
                RechazosBizagiToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {RechazarBizagiToolStripMenuItem, CausalesRechazosBizagiToolStripMenuItem})
                RechazosBizagiToolStripMenuItem.Name = "RechazosBizagiToolStripMenuItem"
                RechazosBizagiToolStripMenuItem.Size = New Drawing.Size(193, 22)
                RechazosBizagiToolStripMenuItem.Text = "Bizagi - Rechazos"
                RechazosBizagiToolStripMenuItem.Visible = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Colpensiones_Beps.Colpensiones_Beps_Funcionario_Especial.Path)

                'Menu Hijo de RechazosBizagiToolStripMenuItem
                RechazarBizagiToolStripMenuItem.Name = "RechazarBizagiToolStripMenuItem"
                RechazarBizagiToolStripMenuItem.Size = New Drawing.Size(193, 22)
                RechazarBizagiToolStripMenuItem.Text = "Bizagi - Rechazar"
                RechazarBizagiToolStripMenuItem.Visible = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Colpensiones_Beps.Colpensiones_Beps_Funcionario_Especial.Rechazos_Bizagi)
                AddHandler RechazarBizagiToolStripMenuItem.Click, AddressOf RechazarBizagiToolStripMenuItem_Click


                'Menu Hijo de RechazosBizagiToolStripMenuItem
                CausalesRechazosBizagiToolStripMenuItem.Name = "CausalesRechazosBizagiToolStripMenuItem"
                CausalesRechazosBizagiToolStripMenuItem.Size = New Drawing.Size(193, 22)
                CausalesRechazosBizagiToolStripMenuItem.Text = "Bizagi - Causales de Rechazos"
                CausalesRechazosBizagiToolStripMenuItem.Visible = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Colpensiones_Beps.Colpensiones_Beps_Funcionario_Especial.ConfiguracionRechazos_Bizagi)
                AddHandler CausalesRechazosBizagiToolStripMenuItem.Click, AddressOf CausalesRechazosBizagiToolStripMenuItem_Click

                'Menu Hijo de BepsToolStripMenuItem
                CruceRadicadosToolStripMenuItem.Name = "CruceRadicadosToolStripMenuItem"
                CruceRadicadosToolStripMenuItem.Size = New Drawing.Size(193, 22)
                CruceRadicadosToolStripMenuItem.Text = "Cruce Radicados"
                CruceRadicadosToolStripMenuItem.Visible = _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_Validos
                AddHandler CruceRadicadosToolStripMenuItem.Click, AddressOf CruceRadicadosToolStripMenuItem_Click

                _Plugin.WorkSpace.MainMenuStrip.Items.AddRange(New ToolStripItem() {BepsToolStripMenuItem})

                _Plugin.WorkSpace.RechazosToolStripMenuItem.Visible = False

                'Reportes
                _Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Inventario.ReportInventario(_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _Plugin))
                _Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Rechazados.ReportRechazados(_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _Plugin))
                _Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Validos.ReportValidos(_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _Plugin))

            Catch ex As Exception
                Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Colpensiones al workspace, " + ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Public Sub DeshacerCambios()

        End Sub

        Private Sub PublicacionToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim Publicacion As New Imaging.Beps.Publicacion.FormPublicacion(_Plugin)
            Publicacion.ShowDialog()
        End Sub

        Private Sub RechazosToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim Rechazos As New Imaging.Beps.Rechazos.FormRechazos_ValidacionesCampos(_Plugin)
            Rechazos.ShowDialog()
        End Sub

        Private Sub RechazarBizagiToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim RechazarBizagi As New Imaging.Beps.Rechazo_Bizagi.FormRechazos_Bizagi(_Plugin)
            RechazarBizagi.ShowDialog()
        End Sub

        Private Sub CausalesRechazosBizagiToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim CausalesRechazosBizagi As New Imaging.Beps.Rechazo_Bizagi.FormCausalRechazo(_Plugin)
            CausalesRechazosBizagi.ShowDialog()
        End Sub

        Private Sub CruceRadicadosToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim CruceRadicados As New Imaging.Beps.Cruce.FormCruceRemisiones(_Plugin)
            CruceRadicados.ShowDialog()
        End Sub
#End Region
    End Class
End Namespace

