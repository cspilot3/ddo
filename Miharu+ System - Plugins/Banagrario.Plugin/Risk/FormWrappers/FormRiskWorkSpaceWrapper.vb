Imports System.Windows.Forms
Imports Banagrario.Plugin.Risk.Forms.Destape
Imports Banagrario.Plugin.Risk.Forms.Recepcion
Imports Banagrario.Plugin.Risk.Reportes.EmpaqueContenedor
Imports Banagrario.Plugin.Risk.Forms.Empaque
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Banagrario.Plugin.Risk.Forms.OT
Imports Miharu.Desktop.Library.Plugins

Namespace Risk.FormWrappers

    Public Class FormRiskWorkSpaceWrapper
        Public _plugin As BanagrarioRiskPlugin = Nothing

        Public _OTButton As Button = Nothing
        Public _RecepcionButton As Button = Nothing
        Public _DestapeButton As Button = Nothing
        Public _EmpaqueButton As Button = Nothing
        Public _CerrarOTButton As Button = Nothing

        Public Sub New(ByVal nPlugin As BanagrarioRiskPlugin)
            _plugin = nPlugin
        End Sub

        Public Sub AplicarCambios()
            Try
                'Controles no utilizados
                _plugin.WorkSpace.MainTabControl.TabPages.Remove(_plugin.WorkSpace.FisicosTabPage)

                PluginHelper.DisableControl(_plugin.WorkSpace.actualizarButton)
                PluginHelper.DisableControl(_plugin.WorkSpace.insercionesButton)
                'PluginFormHelper.DisableControl(_plugin.FormRiskWorkSpace.ActualizarAbjFlecha)
                PluginHelper.DisableControl(_plugin.WorkSpace.cargueUniversalButton)

                'PluginFormHelper.DisableControl(_plugin.FormRiskWorkSpace.CargueUniversalFlecha)
                'PluginFormHelper.DisableControl(_plugin.FormRiskWorkSpace.DevolucionIzqFlecha)
                PluginHelper.DisableControl(_plugin.WorkSpace.devolucionesButton)
                'PluginFormHelper.DisableControl(_plugin.FormRiskWorkSpace.DevolucionDerFlecha)
                'PluginFormHelper.DisableControl(_plugin.FormRiskWorkSpace.CDistribucionAbjFlecha)
                PluginHelper.DisableControl(_plugin.WorkSpace.custodiaButton)
                PluginHelper.DisableControl(_plugin.WorkSpace.MesasFlowLayoutPanel)
                PluginHelper.DisableControl(_plugin.WorkSpace.mesaControlFisicoButton)
                PluginHelper.DisableControl(_plugin.WorkSpace.mesaControlImagenesButton)
                PluginHelper.DisableControl(_plugin.WorkSpace.destape_MesaFlecha
                                          )
                PluginHelper.DisableControl(_plugin.WorkSpace.centroDistribucionButton)
                'PluginFormHelper.DisableControl(_plugin.WorkSpace.NoIdentificadosButton)
                PluginHelper.DisableControl(_plugin.WorkSpace.empaque_CentroDFlecha)
                PluginHelper.DisableControl(_plugin.WorkSpace.centroD_CustodiaFlecha)
                PluginHelper.DisableControl(_plugin.WorkSpace.destape_DevolucionFlecha)
                PluginHelper.DisableControl(_plugin.WorkSpace.mesa_DevolucionFlecha)
                PluginHelper.DisableControl(_plugin.WorkSpace.Actualizar_RecepcionFlecha)
                PluginHelper.DisableControl(_plugin.WorkSpace.destape_MesaFlecha)

                'PluginFormHelper.DisableControl(_plugin.FormRiskWorkSpace.CEmpaqueAbjFlecha)

                '_plugin.FormRiskWorkSpace.DestapeToMesaFlecha.Width = 277

                'TODO: Deshabilitar botones de acuerdo al perfil del usuario

                'Crear OT
                _OTButton = PluginHelper.CloneButton(_plugin.WorkSpace.otButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.otButton, _OTButton)
                _plugin.WorkSpace.otButton.Visible = False
                AddHandler _OTButton.Click, AddressOf OTButton_Click

                'Recepcion
                _RecepcionButton = PluginHelper.CloneButton(_plugin.WorkSpace.recepcionButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.recepcionButton, _RecepcionButton)
                _plugin.WorkSpace.recepcionButton.Visible = False
                AddHandler _RecepcionButton.Click, AddressOf RecepcionButton_Click

                'Destape            
                _DestapeButton = PluginHelper.CloneButton(_plugin.WorkSpace.destapeButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.destapeButton, _DestapeButton)
                _plugin.WorkSpace.destapeButton.Visible = False
                AddHandler _DestapeButton.Click, AddressOf DestapeButton_Click

                'Empaque            
                _EmpaqueButton = PluginHelper.CloneButton(_plugin.WorkSpace.empaqueButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.empaqueButton, _EmpaqueButton)
                _plugin.WorkSpace.empaqueButton.Visible = False
                AddHandler _EmpaqueButton.Click, AddressOf EmpaqueButton_Click

                'Cerrar OT            
                _CerrarOTButton = PluginHelper.CloneButton(_plugin.WorkSpace.cerrarOTButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.cerrarOTButton, _CerrarOTButton)
                _plugin.WorkSpace.cerrarOTButton.Visible = False
                AddHandler _CerrarOTButton.Click, AddressOf CerrarOTButton_Click

                'Reportes
                _plugin.WorkSpace.WorkSpaceRiskReportViewerControl.ReportList.Add(New Report_EmpaqueContenedores(_plugin.WorkSpace.WorkSpaceRiskReportViewerControl, _plugin))


            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Banagrario al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Sub OTButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim formCrearOT As New FormCrearOT(_plugin)
            formCrearOT.ShowDialog()
        End Sub

        Private Sub RecepcionButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim dlgRecepcionPrecintos As New FormRecepcionPrecintos(_plugin)
            dlgRecepcionPrecintos.ShowDialog()
        End Sub

        Private Sub DestapeButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim dlgDestape As New FormDestape(_plugin)
            Dim dmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                DBAgrario.DBAgrarioDataBaseManager.IdentifierDateFormat = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim MesaDestapeDataTable = dmAgrario.SchemaConfig.TBL_Mesa_Destape.DBFindByPC_Namefk_Entidadfk_Sedefk_Centro_ProcesamientoActiva(Environment.MachineName, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, True)
                If MesaDestapeDataTable.Count > 0 Then
                    dlgDestape.ShowDialog()
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("La máquina: " & Environment.MachineName & " No se encuentra registrada para hacer Destape", "Destape", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible validar permisos para hacer Destape , " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            Finally
                If (dmAgrario IsNot Nothing) Then dmAgrario.Connection_Close()
            End Try

        End Sub

        Private Sub EmpaqueButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim dlgEmpaque As New FormEmpaque(_plugin)
            dlgEmpaque.ShowDialog()
        End Sub

        Private Sub CerrarOTButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim dlgCerrar As New FormCerrarOT(_plugin)
            dlgCerrar.ShowDialog()
        End Sub
    End Class

End Namespace