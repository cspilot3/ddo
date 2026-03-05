Imports System.Windows.Forms
Imports Miharu.Desktop.Library
Namespace Imaging.Comerciales
    Public Class Wrapper
#Region " Declaraciones "
        Public _Plugin As Plugin = Nothing
        Public WithEvents BancoCoomevaToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents ReportesToolStripMenuItem As New ToolStripMenuItem()
#End Region
#Region " Constructores "
        Public Sub New(ByVal nPlugin As Plugin)
            _Plugin = nPlugin
        End Sub
#End Region
#Region " Metodos "
        Public Sub AplicarCambios()
            Try
                'Reportes
                'Menu Carpeta Unica Padre
                BancoCoomevaToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ReportesToolStripMenuItem})
                BancoCoomevaToolStripMenuItem.Name = "BancoCoomevaToolStripMenuItem"
                BancoCoomevaToolStripMenuItem.Size = New Drawing.Size(193, 22)
                BancoCoomevaToolStripMenuItem.Text = "Banco Coomeva..."
                BancoCoomevaToolStripMenuItem.Visible = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BCS.Path)

                'Menu Hijo Reportes - Padre(Carpeta Unica)
                ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem"
                ReportesToolStripMenuItem.Size = New Drawing.Size(193, 22)
                ReportesToolStripMenuItem.Text = "Generar Reporte Duplicados"
                ReportesToolStripMenuItem.Visible = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BCS.BCSCoordinadorCU.Reportes)
                AddHandler ReportesToolStripMenuItem.Click, AddressOf ReportesToolStripMenuItem_Click


                _Plugin.WorkSpace.MainMenuStrip.Items.AddRange(New ToolStripItem() {BancoCoomevaToolStripMenuItem})
                '_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Reporte_Duplicados.Reporte_Duplicados(_Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _Plugin))
            Catch ex As Exception
                Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Coomeva al workspace, " + ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub
        Public Sub DeshacerCambios()
        End Sub
#End Region
#Region "Eventos"
        Public Sub ReportesToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim Reportes = New FormRangoFechas(_Plugin)
            Reportes.ShowDialog()
        End Sub
#End Region
    End Class
End Namespace