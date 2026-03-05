Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports System.Reflection

Namespace Imaging.Carpeta_Unica
    Public Class FormImagingWorkSpaceWrapperCarpetaUnica

#Region " Declaraciones "
        Public _plugin As CarpetaUnicaPlugin = Nothing

        Public WithEvents CarpetaUnicaToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents CargueLogToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents CruceToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents ReportesToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents ActualizacionEmpaqueToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents CierraCajaEmpaqueToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents Reportes2ToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents SoporteCarpetaUnicaToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents EliminacionCargueCarpetaUnicaToolStripMenuItem As New ToolStripMenuItem()
#End Region

#Region " Constructores "
        Public Sub New(ByVal nPlugin As CarpetaUnicaPlugin)
            _plugin = nPlugin
        End Sub
#End Region

#Region " Metodos "
        Public Sub AplicarCambios()
            Try
                'Menu Carpeta Unica Padre
                CarpetaUnicaToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {CargueLogToolStripMenuItem, CruceToolStripMenuItem, ReportesToolStripMenuItem, ActualizacionEmpaqueToolStripMenuItem, CierraCajaEmpaqueToolStripMenuItem, Reportes2ToolStripMenuItem})
                CarpetaUnicaToolStripMenuItem.Name = "CarpetaUnicaToolStripMenuItem"
                CarpetaUnicaToolStripMenuItem.Size = New Drawing.Size(193, 22)
                CarpetaUnicaToolStripMenuItem.Text = "Carpeta Unica ..."
                CarpetaUnicaToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BCS.Path)

                'Menu Hijo Cargue Log - Padre(Carpeta Unica)
                CargueLogToolStripMenuItem.Name = "CargueLogToolStripMenuItem"
                CargueLogToolStripMenuItem.Size = New Drawing.Size(193, 22)
                CargueLogToolStripMenuItem.Text = "Cargue Log"
                CargueLogToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BCS.BCSCoordinadorCU.Cargue_Log)
                AddHandler CargueLogToolStripMenuItem.Click, AddressOf CargueLogToolStripMenuItem_Click

                'Menu Hijo Cruce - Padre(Carpeta Unica)
                CruceToolStripMenuItem.Name = "CruceToolStripMenuItem"
                CruceToolStripMenuItem.Size = New Drawing.Size(193, 22)
                CruceToolStripMenuItem.Text = "Cruce - Publicación"
                CruceToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BCS.BCSCoordinadorCU.Cruce)
                AddHandler CruceToolStripMenuItem.Click, AddressOf CruceToolStripMenuItem_Click

                'Menu Hijo Reportes - Padre(Carpeta Unica)
                ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem"
                ReportesToolStripMenuItem.Size = New Drawing.Size(193, 22)
                ReportesToolStripMenuItem.Text = "Generar Reportes"
                ReportesToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BCS.BCSCoordinadorCU.Reportes)
                AddHandler ReportesToolStripMenuItem.Click, AddressOf ReportesToolStripMenuItem_Click

                'Menu Hijo Cruce - Padre(Carpeta Unica)
                ActualizacionEmpaqueToolStripMenuItem.Name = "ActualizacionEmpaqueToolStripMenuItem"
                ActualizacionEmpaqueToolStripMenuItem.Size = New Drawing.Size(193, 22)
                ActualizacionEmpaqueToolStripMenuItem.Text = "Actualización Empaque"
                ActualizacionEmpaqueToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BCS.BCSCoordinadorCU.ActEmpaque)
                AddHandler ActualizacionEmpaqueToolStripMenuItem.Click, AddressOf ActualizacionEmpaqueToolStripMenuItem_Click

                'Menu Soporte Carpeta Unica Padre
                SoporteCarpetaUnicaToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {EliminacionCargueCarpetaUnicaToolStripMenuItem})
                SoporteCarpetaUnicaToolStripMenuItem.Name = "SoporteCarpetaUnicaToolStripMenuItem"
                SoporteCarpetaUnicaToolStripMenuItem.Size = New Drawing.Size(193, 22)
                SoporteCarpetaUnicaToolStripMenuItem.Text = "Soporte Carpeta Unica ..."
                SoporteCarpetaUnicaToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BCS.Soporte.Path)

                'Menu Hijo Eliminación Cargues - Padre(Soporte Carpeta Unica)
                EliminacionCargueCarpetaUnicaToolStripMenuItem.Name = "EliminacionCargueCarpetaUnicaToolStripMenuItem"
                EliminacionCargueCarpetaUnicaToolStripMenuItem.Size = New Drawing.Size(193, 22)
                EliminacionCargueCarpetaUnicaToolStripMenuItem.Text = "Eliminación Cargues"
                EliminacionCargueCarpetaUnicaToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BCS.Soporte.Eliminacion_Cargues)
                AddHandler EliminacionCargueCarpetaUnicaToolStripMenuItem.Click, AddressOf EliminacionCargueCarpetaUnicaToolStripMenuItem_Click

                'Menu Hijo Cierre Caja Cargues - Padre(Carpeta Unica)
                CierraCajaEmpaqueToolStripMenuItem.Name = "CierraCajaEmpaqueToolStripMenuItem"
                CierraCajaEmpaqueToolStripMenuItem.Size = New Drawing.Size(193, 22)
                CierraCajaEmpaqueToolStripMenuItem.Text = "Cierra Cajas"
                CierraCajaEmpaqueToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BCS.Soporte.Eliminacion_Cargues)
                AddHandler CierraCajaEmpaqueToolStripMenuItem.Click, AddressOf CierraCajaEmpaqueToolStripMenuItem_Click

                'Menu Hijo Reportes2 - Padre(Carpeta Unica)
                Reportes2ToolStripMenuItem.Name = "Reportes2ToolStripMenuItem"
                Reportes2ToolStripMenuItem.Size = New Drawing.Size(193, 22)
                Reportes2ToolStripMenuItem.Text = "Generar Reportes 2"
                Reportes2ToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BCS.BCSCoordinadorCU.Reportes)
                AddHandler Reportes2ToolStripMenuItem.Click, AddressOf Reportes2ToolStripMenuItem_Click

                _plugin.WorkSpace.MainMenuStrip.Items.AddRange(New ToolStripItem() {CarpetaUnicaToolStripMenuItem, SoporteCarpetaUnicaToolStripMenuItem})

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Caja Social-Carpeta Unica al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Public Sub DeshacerCambios()
            Try

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible deshacer los cambios de Caja Social-Carpeta Unica al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub
#End Region

#Region "Eventos"
        Public Sub CargueLogToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim CargueLogCarpetaUnica = New Forms.FormsCargue.FormCargueBCS(_plugin)
            CargueLogCarpetaUnica.ShowDialog()
        End Sub

        Public Sub CruceToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim CierreCarpetaUnica = New Forms.Cierre.FormCierre(_plugin)
            CierreCarpetaUnica.ShowDialog()
        End Sub

        Public Sub ReportesToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim Reportes = New Forms.Reportes.FRM_Reporte(_plugin)
            Reportes.ShowDialog()
        End Sub

        Private Sub EliminacionCargueCarpetaUnicaToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim EliminacionCargues = New Forms.Soporte.Eliminacion_Cargue.FormEliminacionCargue(_plugin)
            EliminacionCargues.ShowDialog()
        End Sub

        Private Sub ActualizacionEmpaqueToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim ActEmpaque = New Forms.Empaque.FormActualizacionEmpaque(_plugin)
            ActEmpaque.ShowDialog()
        End Sub

        Private Sub CierraCajaEmpaqueToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim CierreCaja = New Forms.Empaque.FormCierreCaja(_plugin)
            CierreCaja.ShowDialog()
        End Sub

        Public Sub Reportes2ToolStripMenuItem_Click(sender As Object, e As EventArgs)
            Dim Reportes = New Forms.Reportes.FormReportes(_plugin)
            Reportes.ShowDialog()
        End Sub
#End Region

    End Class
End Namespace

