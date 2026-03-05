Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Coomeva.Plugin.Forms.Cargue
Imports Coomeva.Plugin.Forms

Namespace Risk.FormWrappers

    Public Class FormRiskWorkSpaceWrapper
        Public _plugin As CoomevaRiskPlugin = Nothing

        Public _CargueUniversalButton As Button = Nothing
        Public _CargueParcialButton As Button = Nothing
        Public _CargueActualizacionButton As Button = Nothing
        Public _OTButton As Button = Nothing
        Public _RecepcionButton As Button = Nothing
        Public _DestapeButton As Button = Nothing
        Public _EmpaqueButton As Button = Nothing
        Public _CerrarOTButton As Button = Nothing
        Public _CierraFechaRecoleccionButton As Button = Nothing
        Public _Envio_CorreoButton As Button = Nothing
        Public _DesembolsosButton As Button = Nothing
        Public _PrevalidarButton As Button = Nothing

        Public Sub New(ByVal nPlugin As CoomevaRiskPlugin)
            _plugin = nPlugin
        End Sub

        Public Sub AplicarCambios()
            Try


                'DesembolsosButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Cargue.Desembolsos)
                'PrevalidarButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.CPI.Prevalidar)
                'Controles no utilizados
                '_plugin.WorkSpace.MainTabControl.TabPages.Remove(_plugin.WorkSpace.FisicosTabPage)

                'PluginHelper.DisableControl(_plugin.WorkSpace.actualizarButton)
                'PluginHelper.DisableControl(_plugin.WorkSpace.insercionesButton)
                'PluginFormHelper.DisableControl(_plugin.FormRiskWorkSpace.ActualizarAbjFlecha)
                'PluginHelper.DisableControl(_plugin.WorkSpace.cargueUniversalButton)

                'PluginFormHelper.DisableControl(_plugin.FormRiskWorkSpace.CargueUniversalFlecha)
                'PluginFormHelper.DisableControl(_plugin.FormRiskWorkSpace.DevolucionIzqFlecha)
                'PluginHelper.DisableControl(_plugin.WorkSpace.devolucionesButton)
                'PluginFormHelper.DisableControl(_plugin.FormRiskWorkSpace.DevolucionDerFlecha)
                'PluginFormHelper.DisableControl(_plugin.FormRiskWorkSpace.CDistribucionAbjFlecha)
                'PluginHelper.DisableControl(_plugin.WorkSpace.custodiaButton)
                'PluginHelper.DisableControl(_plugin.WorkSpace.MesasFlowLayoutPanel)
                'PluginHelper.DisableControl(_plugin.WorkSpace.mesaControlFisicoButton)
                'PluginHelper.DisableControl(_plugin.WorkSpace.mesaControlImagenesButton)
                'PluginHelper.DisableControl(_plugin.WorkSpace.destape_MesaFlecha)
                'PluginHelper.DisableControl(_plugin.WorkSpace.centroDistribucionButton)
                'PluginFormHelper.DisableControl(_plugin.WorkSpace.NoIdentificadosButton)
                'PluginHelper.DisableControl(_plugin.WorkSpace.empaque_CentroDFlecha)
                'PluginHelper.DisableControl(_plugin.WorkSpace.centroD_CustodiaFlecha)
                'PluginHelper.DisableControl(_plugin.WorkSpace.destape_DevolucionFlecha)
                'PluginHelper.DisableControl(_plugin.WorkSpace.mesa_DevolucionFlecha)
                'PluginHelper.DisableControl(_plugin.WorkSpace.Actualizar_RecepcionFlecha)
                'PluginHelper.DisableControl(_plugin.WorkSpace.destape_MesaFlecha)

                'PluginFormHelper.DisableControl(_plugin.FormRiskWorkSpace.CEmpaqueAbjFlecha)

                '_plugin.FormRiskWorkSpace.DestapeToMesaFlecha.Width = 277

                'TODO: Deshabilitar botones de acuerdo al perfil del usuario

                'Cargue
                _CargueUniversalButton = PluginHelper.CloneButton(_plugin.WorkSpace.cargueUniversalButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.cargueUniversalButton, _CargueUniversalButton)
                _plugin.WorkSpace.cargueUniversalButton.Visible = False
                AddHandler _CargueUniversalButton.Click, AddressOf _CargueUniversalButton_Click

                _CargueParcialButton = PluginHelper.CloneButton(_plugin.WorkSpace.cargueParcialButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.cargueParcialButton, _CargueParcialButton)
                _plugin.WorkSpace.cargueParcialButton.Visible = False
                AddHandler _CargueParcialButton.Click, AddressOf _CargueParcialButton_Click

                _CargueActualizacionButton = PluginHelper.CloneButton(_plugin.WorkSpace.actualizarButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.actualizarButton, _CargueActualizacionButton)
                _plugin.WorkSpace.actualizarButton.Visible = False
                AddHandler _CargueActualizacionButton.Click, AddressOf _CargueActualizacionButton_Click

                _CierraFechaRecoleccionButton = PluginHelper.CloneButton(_plugin.WorkSpace.CierreFechaRecoleccion)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.CierreFechaRecoleccion, _CierraFechaRecoleccionButton)
                _plugin.WorkSpace.CierreFechaRecoleccion.Visible = False
                _CierraFechaRecoleccionButton.Enabled = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Risk.ProcesoV2.Cierre.CierreFecha)
                AddHandler _CierraFechaRecoleccionButton.Click, AddressOf _CierraFechaRecoleccionButton_Click

                _Envio_CorreoButton = PluginHelper.CloneButton(_plugin.WorkSpace.Envio_CorreoButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.Envio_CorreoButton, _Envio_CorreoButton)
                _plugin.WorkSpace.Envio_CorreoButton.Visible = False
                AddHandler _Envio_CorreoButton.Click, AddressOf _Envio_CorreoButton_Click
                _Envio_CorreoButton.Enabled = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Risk.ProcesoV2.Correo.EnviarCorreo)


                _DesembolsosButton = PluginHelper.CloneButton(_plugin.WorkSpace.DesembolsosButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.DesembolsosButton, _DesembolsosButton)
                _plugin.WorkSpace.DesembolsosButton.Visible = False
                _DesembolsosButton.Enabled = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Risk.Proceso.Cargue.Desembolsos)
                AddHandler _DesembolsosButton.Click, AddressOf _DesembolsosButton_Click

                _PrevalidarButton = PluginHelper.CloneButton(_plugin.WorkSpace.PrevalidarButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.PrevalidarButton, _PrevalidarButton)
                _plugin.WorkSpace.PrevalidarButton.Visible = False
                _plugin.WorkSpace.PrevalidarFlecha.Visible = True
                _PrevalidarButton.Visible = True
                _PrevalidarButton.Enabled = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.CPI.Prevalidar)
                AddHandler _PrevalidarButton.Click, AddressOf _PrevalidarButton_Click

                

                ''Crear OT
                '_OTButton = PluginHelper.CloneButton(_plugin.WorkSpace.otButton)
                'PluginHelper.ReplaceControl(_plugin.WorkSpace.otButton, _OTButton)
                '_plugin.WorkSpace.otButton.Visible = False
                'AddHandler _OTButton.Click, AddressOf OTButton_Click

                ''Recepcion
                '_RecepcionButton = PluginHelper.CloneButton(_plugin.WorkSpace.recepcionButton)
                'PluginHelper.ReplaceControl(_plugin.WorkSpace.recepcionButton, _RecepcionButton)
                '_plugin.WorkSpace.recepcionButton.Visible = False
                'AddHandler _RecepcionButton.Click, AddressOf RecepcionButton_Click

                ''Destape            
                '_DestapeButton = PluginHelper.CloneButton(_plugin.WorkSpace.destapeButton)
                'PluginHelper.ReplaceControl(_plugin.WorkSpace.destapeButton, _DestapeButton)
                '_plugin.WorkSpace.destapeButton.Visible = False
                'AddHandler _DestapeButton.Click, AddressOf DestapeButton_Click

                ''Empaque            
                '_EmpaqueButton = PluginHelper.CloneButton(_plugin.WorkSpace.empaqueButton)
                'PluginHelper.ReplaceControl(_plugin.WorkSpace.empaqueButton, _EmpaqueButton)
                '_plugin.WorkSpace.empaqueButton.Visible = False
                'AddHandler _EmpaqueButton.Click, AddressOf EmpaqueButton_Click

                ''Cerrar OT            
                '_CerrarOTButton = PluginHelper.CloneButton(_plugin.WorkSpace.cerrarOTButton)
                'PluginHelper.ReplaceControl(_plugin.WorkSpace.cerrarOTButton, _CerrarOTButton)
                '_plugin.WorkSpace.cerrarOTButton.Visible = False
                'AddHandler _CerrarOTButton.Click, AddressOf CerrarOTButton_Click

                'Reportes
                ' _plugin.WorkSpace.WorkSpaceRiskReportViewerControl.ReportList.Add(New Report_EmpaqueContenedores(_plugin.WorkSpace.WorkSpaceRiskReportViewerControl, _plugin))


            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Banagrario al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        'Sub OTButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        '    Dim formCrearOT As New FormCrearOT(_plugin)
        '    formCrearOT.ShowDialog()
        'End Sub

        'Private Sub RecepcionButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        '    Dim dlgRecepcionPrecintos As New FormRecepcionPrecintos(_plugin)
        '    dlgRecepcionPrecintos.ShowDialog()
        'End Sub

        'Private Sub DestapeButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        '    Dim dlgDestape As New FormDestape(_plugin)
        '    Dim dmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

        '    Try
        '        dmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
        '        DBAgrario.DBAgrarioDataBaseManager.IdentifierDateFormat = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
        '        dmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

        '        Dim MesaDestapeDataTable = dmAgrario.SchemaConfig.TBL_Mesa_Destape.DBFindByPC_Namefk_Entidadfk_Sedefk_Centro_ProcesamientoActiva(Environment.MachineName, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, True)
        '        If MesaDestapeDataTable.Count > 0 Then
        '            dlgDestape.ShowDialog()
        '        Else
        '            DesktopMessageBoxControl.DesktopMessageShow("La máquina: " & Environment.MachineName & " No se encuentra registrada para hacer Destape", "Destape", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
        '        End If

        '    Catch ex As Exception
        '        DesktopMessageBoxControl.DesktopMessageShow("No fue posible validar permisos para hacer Destape , " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
        '    Finally
        '        If (dmAgrario IsNot Nothing) Then dmAgrario.Connection_Close()
        '    End Try

        'End Sub

        'Private Sub EmpaqueButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        '    Dim dlgEmpaque As New FormEmpaque(_plugin)
        '    dlgEmpaque.ShowDialog()
        'End Sub

        'Private Sub CerrarOTButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        '    Dim dlgCerrar As New FormCerrarOT(_plugin)
        '    dlgCerrar.ShowDialog()
        'End Sub

        Private Sub _CargueParcialButton_Click(sender As Object, e As EventArgs)
            Dim formSeleccionarCargue = New FormCargue(_plugin, DesktopConfig.TipoCargue.Parcial)
            formSeleccionarCargue.ShowDialog()
        End Sub

        Private Sub _CargueActualizacionButton_Click(sender As Object, e As EventArgs)
            Dim formSeleccionarCargue = New FormCargue(_plugin, DesktopConfig.TipoCargue.Actualizacion)
            formSeleccionarCargue.ShowDialog()
        End Sub

        Private Sub _CargueUniversalButton_Click(sender As Object, e As EventArgs)
            Dim formSeleccionarCargue = New FormCargue(_plugin, DesktopConfig.TipoCargue.Universal)
            formSeleccionarCargue.ShowDialog()
        End Sub

        Private Sub _CierraFechaRecoleccionButton_Click(sender As Object, e As EventArgs)
            Dim formCierreFechaRecoleccion = New FormCierreFechaRecoleccion(_plugin)
            formCierreFechaRecoleccion.ShowDialog()
        End Sub

        Private Sub _Envio_CorreoButton_Click(sender As Object, e As EventArgs)
            Dim formGestionOficinas = New FormGestionOficinas(_plugin)
            formGestionOficinas.ShowDialog()
        End Sub

        Private Sub _DesembolsosButton_Click(sender As Object, e As EventArgs)
            Dim formDesembolsos = New Desembolsos.FormDesembolsosOpciones(_plugin)
            formDesembolsos.ShowDialog()
        End Sub


        Private Sub _PrevalidarButton_Click(sender As Object, e As EventArgs)
            Dim formSeleccionarCargue = New FormCargue(_plugin, DesktopConfig.TipoCargue.Prevalidar)
            formSeleccionarCargue.ShowDialog()
        End Sub


    End Class

End Namespace