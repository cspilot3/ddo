Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports System.Windows.Forms
Imports Slyg.Tools
Imports Miharu.Tools.Progress
Imports Miharu.Desktop.Library.Config
Imports System.Text
Imports Miharu.Security.Library.Session
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Library


Namespace Imaging.Carpeta_Unica.Forms.Cierre

    Public Class FormCierre

#Region " Declaraciones "

        Private _Plugin As CarpetaUnicaPlugin
        Private _TipoProcesoDataTable As DBIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_ProcesoDataTable
        Private _TipoJornadaProcesoDataTable As DBIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_JornadaDataTable
        Private _ReporteDatatable As DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteDataTable

#End Region

#Region " Contructores "

        Public Sub New(ByVal nCarpetaUnicaDesktopPlugin As CarpetaUnicaPlugin)
            InitializeComponent()

            _Plugin = nCarpetaUnicaDesktopPlugin
            CargaTablas()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormCierre_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaTipoProceso()
        End Sub

        Private Sub CruzarButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CruzarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar el Cruce?", "Cruce", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                GeneraCruce()
            End If
        End Sub

        Private Sub PublicarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PublicarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar la publicación?", "Publicación", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                PublicarInformacion()
            End If
        End Sub

        Private Sub PublicacionBackGrondWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles PublicacionBackGrondWorker.RunWorkerCompleted
            DeshabilitaControles(True)
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaTablas()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Try

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                _TipoProcesoDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBGet(Nothing)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then
                    dbmIntegration.Connection_Close()
                End If
            End Try
        End Sub

        Private Sub CargaTipoProceso()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                _TipoProcesoDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByActivoAplica_Tipo_Proceso(True, True)

                If _TipoProcesoDataTable.Count > 0 Then
                    Utilities.LlenarCombo(ProcesoDesktopComboBox, _TipoProcesoDataTable, DBIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_ProcesoEnum.id_Tipo_Proceso.ColumnName, DBIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_ProcesoEnum.Nombre_Tipo_Proceso.ColumnName, True, "-1", "--TODOS--")
                Else
                    MessageBox.Show("El proyecto no tiene procesos activos, por favor validar", "CargaFiltros", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            End Try
        End Sub

        Private Sub GeneraCruce()

            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
            Dim Mensaje As String = ""
            Dim ProgressForm As New Slyg.Tools.Progress.FormProgress()

            Try
                Me.Enabled = False
                Me.Cursor = Cursors.WaitCursor

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim ProcesoDataTable As DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable
                Dim OTPendientesProceso As Boolean = False

                ProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_Proyectoid_fecha_proceso(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"))

                Dim RegistrosPendientes = dbmIntegration.SchemaBCSCarpetaUnica.PA_New_00_Cruce_Validacion_Registros_Pendientes.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), Integer.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()))

                If (RegistrosPendientes > 0) Then
                    MessageBox.Show("La Fecha de Proceso seleccionada aun tiene " & RegistrosPendientes.ToString() & " registros pendientes por insertar, Favor intente mas tarde.")
                    Return
                End If

                If (ProcesoDataTable.Count = 1) Then
                    If (ProcesoDataTable(0).Cerrado = False) Then
                        Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerrado(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), Nothing)
                        If OTDataTable.Count > 0 Then
                            For Each ot As DBImaging.SchemaProcess.TBL_OTRow In OTDataTable
                                Dim Respuesta = dbmIntegration.SchemaBCSCarpetaUnica.PA_Validar_Documentos_Pendientes_OT.DBExecute(ot.id_OT, Integer.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()), _Plugin.Manager.Sesion.Usuario.id)

                                If Integer.Parse(Respuesta(0)(0)) = 1 Then
                                    OTPendientesProceso = True
                                End If
                            Next

                            If (OTPendientesProceso) Then
                                MessageBox.Show("La Fecha de Proceso seleccionada tiene OT's con documentos pendientes de proceso.", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Else

                                Dim ProcesosCruce As DBIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_ProcesoDataTable
                                Dim ProcesosDictionary As New Dictionary(Of Integer, String)

                                If (Integer.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()) = -1) Then

                                    Dim ProcesosAutorizar = dbmIntegration.SchemaBCSCarpetaUnica.PA_Obtiene_Procesos_Autorizar.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, -1)

                                    If ProcesosAutorizar.Rows.Count > 0 Then
                                        ValidarPermisoAutorizar(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Autorizar procesos para cruce: ", _Plugin.Manager.Sesion.Usuario, _Plugin.Manager.DesktopGlobal.SecurityServiceUrl, _Plugin.Manager.DesktopGlobal.ClientIpAddress, FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), Integer.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()))
                                    End If

                                    ProcesosCruce = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByActivoAplica_Tipo_Proceso(1, 1)

                                    'Se adicional los procesos
                                    For Each proceso In ProcesosCruce
                                        ProcesosDictionary.Add(proceso.id_Tipo_Proceso, proceso.Nombre_Tipo_Proceso)
                                    Next

                                    'Iniciar proceso                
                                    ProgressForm.Process = ""
                                    ProgressForm.Action = ""
                                    ProgressForm.ValueProcess = 0
                                    ProgressForm.ValueAction = 0
                                    ProgressForm.MaxValueProcess = ProcesosDictionary.Count
                                    ProgressForm.MaxValueAction = 5

                                    ProgressForm.Show()

                                    Dim i As Integer = 0
                                    Dim msg As String = ""
                                    Dim Usa_Cruce_En_Linea As Boolean = 0

                                    For Each Item In ProcesosDictionary
                                        ProgressForm.Process = Item.Value
                                        ProgressForm.Action = "Ejecutando Cruce"
                                        ProgressForm.ValueAction = 1
                                        Application.DoEvents()

                                        Usa_Cruce_En_Linea = ProcesosCruce(0).Usa_Cruce_En_Linea

                                        Dim ResultadoCruce = dbmIntegration.SchemaBCSCarpetaUnica.PA_New_05_Cruce_Masivo.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), CShort(Item.Key), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)
                                        If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                                        If ResultadoCruce = "OK" Then
                                            If Item.Value = "PASIVO" Then

                                                ProgressForm.Action = "Reportando documentos obligatorios"
                                                ProgressForm.ValueAction = 2
                                                Application.DoEvents()
                                                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_06_Reportar_Documentos_Obligatorios_Pasivo.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CShort(Item.Key))
                                                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                                                If Not (Usa_Cruce_En_Linea) Then
                                                    ProgressForm.Action = "Inserción novedades campos"
                                                    ProgressForm.ValueAction = 3
                                                    Application.DoEvents()
                                                    dbmIntegration.SchemaBCSCarpetaUnica.PA_New_05_Insercion_Novedades_Validaciones_Campos.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CShort(Item.Key))
                                                    If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                                                End If

                                                ProgressForm.Action = "Actualización multiproducto"
                                                ProgressForm.ValueAction = 4
                                                Application.DoEvents()
                                                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_05B_Multiproducto_Pasivo.DBExecute(CShort(Item.Key), FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"))
                                                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                                                ProgressForm.Action = "Actualización Novedades"
                                                ProgressForm.ValueAction = 5
                                                Application.DoEvents()
                                                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_07_Actualizacion_Novedades.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), CShort(Item.Key), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto)
                                                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                                            Else
                                                ProgressForm.Action = "Reportando documentos obligatorios"
                                                ProgressForm.ValueAction = 2
                                                Application.DoEvents()
                                                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_06_Reportar_Documentos_Obligatorios.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CShort(Item.Key))
                                                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                                                If Not (Usa_Cruce_En_Linea) Then
                                                    ProgressForm.Action = "Inserción novedades campos"
                                                    ProgressForm.ValueAction = 3
                                                    Application.DoEvents()
                                                    dbmIntegration.SchemaBCSCarpetaUnica.PA_New_05_Insercion_Novedades_Validaciones_Campos.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CShort(Item.Key))
                                                    If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                                                End If

                                                ProgressForm.Action = "Actualización Novedades"
                                                ProgressForm.ValueAction = 4
                                                Application.DoEvents()
                                                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_07_Actualizacion_Novedades.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), CShort(Item.Key), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto)
                                                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                                            End If

                                        Else
                                            msg = msg + Item.Value + ": " + ResultadoCruce + ". "
                                        End If

                                        i += 1
                                        ProgressForm.ValueProcess = i
                                    Next


                                    If msg <> "" Then
                                        MessageBox.Show(msg.ToString(), "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Else
                                        MessageBox.Show("Cruce Exitoso", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    End If

                                Else
                                    Dim ProcesosAutorizar = dbmIntegration.SchemaBCSCarpetaUnica.PA_Obtiene_Procesos_Autorizar.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, Integer.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()))

                                    If ProcesosAutorizar.Rows.Count > 0 Then
                                        ValidarPermisoAutorizar(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Autorizar el destape del precinto " & "", _Plugin.Manager.Sesion.Usuario, _Plugin.Manager.DesktopGlobal.SecurityServiceUrl, _Plugin.Manager.DesktopGlobal.ClientIpAddress, FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), Integer.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()))
                                    End If

                                    'Iniciar proceso 
                                    ProgressForm.Process = ""
                                    ProgressForm.Action = ""
                                    ProgressForm.ValueProcess = 0
                                    ProgressForm.ValueAction = 0
                                    ProgressForm.MaxValueProcess = 1
                                    ProgressForm.MaxValueAction = 5

                                    ProgressForm.Show()

                                    ProgressForm.Process = ProcesoDesktopComboBox.Text.ToString()
                                    ProgressForm.Action = "Ejecutando Cruce"
                                    ProgressForm.ValueAction = 1
                                    Application.DoEvents()

                                    Dim TipoProcesoDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByid_Tipo_Proceso(Int32.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()))

                                    Dim ResultadoCruceProceso = dbmIntegration.SchemaBCSCarpetaUnica.PA_New_05_Cruce_Masivo.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), Int32.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)
                                    If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")



                                    If ResultadoCruceProceso = "OK" Then
                                        If ProcesoDesktopComboBox.Text.ToString() = "PASIVO" Then

                                            ProgressForm.Action = "Reportando documentos obligatorios"
                                            ProgressForm.ValueAction = 2
                                            Application.DoEvents()
                                            dbmIntegration.SchemaBCSCarpetaUnica.PA_New_06_Reportar_Documentos_Obligatorios_Pasivo.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, Int32.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()))
                                            If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                                            If Not (TipoProcesoDataTable(0).Usa_Cruce_En_Linea) Then
                                                ProgressForm.Action = "Inserción novedades campos"
                                                ProgressForm.ValueAction = 3
                                                Application.DoEvents()
                                                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_05_Insercion_Novedades_Validaciones_Campos.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, Int32.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()))
                                                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                                            End If

                                            ProgressForm.Action = "Actualización multiproducto"
                                            ProgressForm.ValueAction = 4
                                            Application.DoEvents()
                                            dbmIntegration.SchemaBCSCarpetaUnica.PA_New_05B_Multiproducto_Pasivo.DBExecute(Int32.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()), FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"))
                                            If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                                            ProgressForm.Action = "Actualización Novedades"
                                            ProgressForm.ValueAction = 5
                                            Application.DoEvents()
                                            dbmIntegration.SchemaBCSCarpetaUnica.PA_New_07_Actualizacion_Novedades.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), Int32.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto)
                                            If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                                            MessageBox.Show("Cruce Exitoso", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                        Else

                                            ProgressForm.Action = "Reportando documentos obligatorios"
                                            ProgressForm.ValueAction = 2
                                            Application.DoEvents()
                                            dbmIntegration.SchemaBCSCarpetaUnica.PA_New_06_Reportar_Documentos_Obligatorios.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, Int32.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()))
                                            If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                                            If Not (TipoProcesoDataTable(0).Usa_Cruce_En_Linea) Then
                                                ProgressForm.Action = "Inserción novedades campos"
                                                ProgressForm.ValueAction = 3
                                                Application.DoEvents()
                                                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_05_Insercion_Novedades_Validaciones_Campos.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, Int32.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()))
                                                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                                            End If

                                            ProgressForm.Action = "Actualización Novedades"
                                            ProgressForm.ValueAction = 4
                                            Application.DoEvents()
                                            dbmIntegration.SchemaBCSCarpetaUnica.PA_New_07_Actualizacion_Novedades.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), Int32.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto)
                                            If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                                            MessageBox.Show("Cruce Exitoso", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        End If
                                    Else
                                        MessageBox.Show(ResultadoCruceProceso.ToString(), "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    End If
                                End If
                            End If

                        Else
                            MessageBox.Show("No se encuentra OTs asociadas a la Fecha de Proceso seleccionada.", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    Else
                        MessageBox.Show("La Fecha de Proceso seleccionada ya fue cerrada.", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                Else
                    MessageBox.Show("No se encuentra información asociada a la Fecha de Proceso seleccionada.", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                End If

            Catch ex As Exception
                ProgressForm.Hide()
                Application.DoEvents()
                DesktopMessageBoxControl.DesktopMessageShow("GeneraCruce", ex)
            Finally
                ProgressForm.Visible = False
                ProgressForm.Close()

                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()

                Me.Enabled = True
                Me.Cursor = Cursors.Default
            End Try
        End Sub

        Public Function ValidarPermisoAutorizar(ByVal nPermiso As String, ByVal nAsseblyName As String, ByVal nMensaje As String, ByVal nUsuario As Usuario, ByVal nSecurityServiceUrl As String, ByVal nClientIpAddress As String, ByVal nFechaProceso As String, ByVal nTipoProceso As Integer) As Boolean
            If (nUsuario.PerfilManager.PuedeEditar(nPermiso)) Then
                Dim dlgAutorizacion As New FormAutorizacion(_Plugin, nMensaje, nAsseblyName, nPermiso, nSecurityServiceUrl, nClientIpAddress, nFechaProceso, nTipoProceso)
                If (dlgAutorizacion.ShowDialog() = DialogResult.OK) Then
                    Return True
                End If
            Else
                Return True
            End If

            Return False
        End Function

        Private Sub PublicarInformacion()

            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

            Dim ProgressForm1 As New FormProgress


            Try
                Me.Enabled = False
                Me.Cursor = Cursors.WaitCursor

                Dim procesoSeleccionado = Me.ProcesoDesktopComboBox.SelectedValue

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                'validar que no hayan fechas inferiores sin publicar
                Dim VerificaPublicados_Anteriores = dbmIntegration.SchemaBCSCarpetaUnica.PA_VerificaPublicacion_FechasAnteriores.DBExecute(Me.FechaProcesodateTimePicker.Value.ToString("yyyyMMdd").ToString(), CInt(procesoSeleccionado), Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto)

                If (VerificaPublicados_Anteriores.Rows.Count > 0) Then
                    Dim valido = VerificaPublicados_Anteriores.Rows(0)("VALIDO")

                    If (valido = 0) Then
                        DesktopMessageBoxControl.DesktopMessageShow(VerificaPublicados_Anteriores.Rows(0)("MSJ_STR").ToString(), "Validación Errada Publicación", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        Return
                    End If
                End If

                Dim ProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_Proyectoid_fecha_proceso(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"))
                If (ProcesoDataTable.Count = 1) Then

                    If (ProcesoDataTable(0).Cerrado = True) Then


                        Dim dtProcesosCruzados As New DataTable
                        dtProcesosCruzados = dbmIntegration.SchemaBCSCarpetaUnica.PA_Trae_ReportesCruzados.DBExecute(Me.FechaProcesodateTimePicker.Value.ToString("yyyyMMdd").ToString(), CInt(procesoSeleccionado))
                        Dim publicado = True

                        If (dtProcesosCruzados.Rows.Count > 0) Then

                            ProgressForm1.SetProceso("Preparando publicacion")
                            ProgressForm1.SetAccion("Cruce Fatca")
                            ProgressForm1.SetMaxValue(2)


                            DeshabilitaControles(False)
                            Application.DoEvents()
                            ProgressForm1.Show()



                            Try
                                dbmIntegration.SchemaBCSCarpetaUnica.PA_Cruce_Fatca.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd").ToString(), CInt(_Plugin.Manager.ImagingGlobal.Entidad), CInt(_Plugin.Manager.ImagingGlobal.Proyecto), ProcesoDesktopComboBox.SelectedValue.ToString(), CInt(_Plugin.Manager.Sesion.Usuario.id))

                                ProgressForm1.SetProgreso(1)
                                ProgressForm1.SetAccion("Mover A Historicos / Insercion Novedades Faltantes")

                                Application.DoEvents()

                                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_Report_00_Publicar.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd").ToString(), CInt(procesoSeleccionado), CInt(_Plugin.Manager.ImagingGlobal.Entidad), CInt(_Plugin.Manager.ImagingGlobal.Proyecto), _Plugin.Manager.Sesion.Usuario.id)
                                ProgressForm1.Hide()

                                PublicacionRun(dbmImaging, dbmIntegration, procesoSeleccionado, publicado)

                                'Actualizar a Publicados los Reportes en Tabla Control
                                For index = 0 To dtProcesosCruzados.Rows.Count - 1
                                    Dim ProcesoActual_aux = dtProcesosCruzados.Rows(index)("fk_Tipo_Proceso")
                                    Dim NombreProcesoActual = dtProcesosCruzados.Rows(index)("Nombre_Tipo_Proceso")
                                    Dim idControlProceso = dtProcesosCruzados.Rows(index)("id_Control_Proceso")
                                    Dim RegistroControl_Actualizar As New DBIntegration.SchemaBCSCarpetaUnica.TBL_Control_ProcesoType
                                    RegistroControl_Actualizar.Publicado = publicado
                                    dbmIntegration.SchemaBCSCarpetaUnica.TBL_Control_Proceso.DBUpdate(RegistroControl_Actualizar, CType(idControlProceso, Long))
                                Next


                            Catch ex As Exception
                                Throw New Exception
                            End Try
                        Else
                            MessageBox.Show("No hay procesos pendientes de Publicacion.")
                        End If

                    Else
                        MessageBox.Show("La Fecha de Proceso seleccionada aún se encuentra abierta, por favor cierrela e intente nuevamente.")
                    End If
                Else
                    MessageBox.Show("No se encuentra información asociada a la Fecha de Proceso seleccionada")
                End If

            Catch ex As Exception
                ProgressForm1.Hide()
                DesktopMessageBoxControl.DesktopMessageShow("Publicación", ex)
            Finally
                ProgressForm1.Hide()
                ProgressForm1.Dispose()
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()

                Me.Enabled = True
                Me.Cursor = Cursors.Default
            End Try

        End Sub

        Public Sub PublicacionRun(dbmImaging As DBImaging.DBImagingDataBaseManager, dbmIntegration As DBIntegration.DBIntegrationDataBaseManager, ByVal procesoSeleccionado As Integer, ByRef ErrorPublic As Boolean)

            Dim ProgressForm2 As New Slyg.Tools.Progress.FormProgress()

            Dim Publicado_Delta As Boolean = False,
                Publicado_Goro As Boolean = False,
                Publicado_Sipla As Boolean = False
            Dim Msj_Respuesta As String = ""
            Dim Error_Publicacion As Boolean = False
            Dim CantRegistrosView As DataView
            Dim CantRegDeltaEjecucion As Integer, CantRegDeltaConstructorEjecucion As Integer, CantRegGoroEjecucion As Integer, CantRegGoroFaltanteEjecucion As Integer, CantRegSiplaEjecucion As Integer, CantRegFatcaEjecucion As Integer
            Dim TotalRegistros As Integer, TotalRegistrosAux As Integer
            Dim RegistrosDelta As Integer = 0
            Dim RegistrosDeltaConstructor As Integer = 0
            Dim RegistrosGoro As Integer = 0
            Dim RegistrosGoroFaltante As Integer = 0
            Dim RegistrosSipla As Integer = 0
            Dim RegistrosFatca As Integer = 0
            Dim subProgreso As Double = 0 'Cantidad en que aumenta el porcentaje de cada ejecución. basados en las veces que sea necesario ejecutar el SP correspondiente.
            Dim ProgresoAux As Integer = 0
            Dim progresoActual As Double = 0
            Dim PorcentajeEjecucion As Integer = 0 'Cantidad total del porcentaje de la ejecución.
            Dim Progreso As Double
            Dim SalirCiclo As Boolean = False
            Dim fk_Reporte As Integer = Nothing

            ProgressForm2.Process = "Publicación"
            ProgressForm2.ValueProcess = 0
            ProgressForm2.ValueAction = 0
            ProgressForm2.MaxValueProcess = 7
            ProgressForm2.MaxValueAction = 100


            ProgressForm2.Show()
            Try

                If ProcesoDesktopComboBox.SelectedValue <> -1 Then
                    fk_Reporte = ProcesoDesktopComboBox.SelectedValue
                End If


                Dim Proceso As SlygNullable(Of Integer) = Nothing

                If ProcesoDesktopComboBox.SelectedValue <> -1 Then
                    Proceso = CInt(ProcesoDesktopComboBox.SelectedValue)
                End If

                Dim Reporte_Proceso As DataTable
                If (procesoSeleccionado.ToString() = "-1") Then
                    Reporte_Proceso = dbmIntegration.SchemaBCSCarpetaUnica.CTA_Config_Reporte_Proceso.DBFindByfk_Reportefk_Tipo_procesoAplica_Publicacion(Nothing, Nothing, 1)
                Else
                    Reporte_Proceso = dbmIntegration.SchemaBCSCarpetaUnica.CTA_Config_Reporte_Proceso.DBFindByfk_Reportefk_Tipo_procesoAplica_Publicacion(Nothing, CInt(procesoSeleccionado), 1)
                End If


                Dim LogReporte = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_Log_Publicacion.DBFindByfk_fecha_procesofk_tipo_ProcesoPublicado(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), procesoSeleccionado, 1)

                If (ProcesoDesktopComboBox.SelectedValue <> -1 And LogReporte.Rows.Count() = 0) _
                        Or (ProcesoDesktopComboBox.SelectedValue = -1 And LogReporte.Rows.Count() < Reporte_Proceso.Rows.Count()) Then

                    CantRegistrosView = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, Nothing).DefaultView

                    CantRegistrosView.RowFilter = "Nombre_Parametro_Sistema = 'Registros_Ejecucion_Delta'"
                    CantRegDeltaEjecucion = CantRegistrosView(0)("Valor_Parametro_Sistema")

                    CantRegistrosView.RowFilter = "Nombre_Parametro_Sistema = 'Registros_Ejecucion_Delta_Constructor'"
                    CantRegDeltaConstructorEjecucion = CantRegistrosView(0)("Valor_Parametro_Sistema")

                    CantRegistrosView.RowFilter = "Nombre_Parametro_Sistema = 'Registros_Ejecucion_Goro_Principal'"
                    CantRegGoroEjecucion = CantRegistrosView(0)("Valor_Parametro_Sistema")

                    CantRegistrosView.RowFilter = "Nombre_Parametro_Sistema = 'Registros_Ejecucion_Goro'"
                    CantRegGoroFaltanteEjecucion = CantRegistrosView(0)("Valor_Parametro_Sistema")

                    CantRegistrosView.RowFilter = "Nombre_Parametro_Sistema = 'Registros_Ejecucion_Sipla'"
                    CantRegSiplaEjecucion = CantRegistrosView(0)("Valor_Parametro_Sistema")

                    CantRegistrosView.RowFilter = "Nombre_Parametro_Sistema = 'Registros_Ejecucion_Fatca'"
                    CantRegFatcaEjecucion = CantRegistrosView(0)("Valor_Parametro_Sistema")




                    ProgressForm2.Action = "Generando Temporales"
                    ProgressForm2.ValueProcess = 1

                    Application.DoEvents()

                    '=================================== INICIO  RITM0384708 ==========================================================================
                    Dim MensajeErrorReporte As String = ""
                    Dim MensajeErrorImagen As String = ""
                    Dim GeneraImagenTapa As GenerarImagenTapa = New GenerarImagenTapa(dbmImaging, dbmIntegration,
                                                                                      _Plugin,
                                                                                      Me.FechaProcesodateTimePicker.Value.ToString("yyyyMMdd").ToString(),
                                                                                      CInt(procesoSeleccionado))
                    MensajeErrorReporte = GeneraImagenTapa.GenerarReporte()
                    If Not MensajeErrorReporte.Equals("") Then
                        Throw New Exception(MensajeErrorReporte)
                    End If
                    MensajeErrorImagen = GeneraImagenTapa.IndexarFolioHilos()
                    If Not MensajeErrorImagen.Equals("") Then
                        Throw New Exception(MensajeErrorImagen)
                    End If
                    '=================================== FIN  RITM0384708 ==========================================================================

                    'PUBLICACION TEMPORALES
                    Dim Respuesta_Temporales = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_Publica_Temporales.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), CInt(procesoSeleccionado), _Plugin.Manager.Sesion.Usuario.id)

                    If Respuesta_Temporales.Rows(0)("Result") = 0 Then
                        Throw New Exception(Respuesta_Temporales.Rows(0)("MSJ_ERROR"))
                    End If

                    RegistrosDelta = CInt(Respuesta_Temporales.Rows(0)("Delta"))
                    RegistrosDeltaConstructor = (CInt(Respuesta_Temporales.Rows(0)("Delta_Constructor")))
                    RegistrosGoro = CInt(Respuesta_Temporales.Rows(0)("GoroPpal"))
                    RegistrosGoroFaltante = CInt(Respuesta_Temporales.Rows(0)("GoroFaltante"))
                    RegistrosSipla = CInt(Respuesta_Temporales.Rows(0)("Sipla"))
                    RegistrosFatca = CInt(Respuesta_Temporales.Rows(0)("Fatca"))
                    TotalRegistros = RegistrosDelta + RegistrosDeltaConstructor + RegistrosGoro + RegistrosGoroFaltante + RegistrosSipla + RegistrosFatca

                    TotalRegistrosAux = CInt(Math.Round((TotalRegistros * 105) / 100, 0)) 'Ajuste de Progreso de PUBLICACION TEMPORALES que se dejó en 5%
                    Progreso = 0
                    progresoActual = Progreso


                    ProgressForm2.Action = "Generando Reporte Delta"
                    ProgressForm2.ValueProcess = 2
                    ProgressForm2.ValueAction = 0
                    Application.DoEvents()

                    'EJECUCION DELTA
                    'subProgreso = calculaSubProgreso(RegistrosDelta, RegistrosDelta, CantRegDeltaEjecucion)
                    progresoActual = 0
                    SalirCiclo = False
                    While SalirCiclo = False
                        If ProgressForm2.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")
                        '****Falta Agregar el campo fk_Tipo Proceso
                        Dim Respuesta_Ejecucion = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_Publicar_DELTA_Ejecucion.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.Sesion.Usuario.id, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CInt(procesoSeleccionado), CantRegDeltaEjecucion)
                        If ProcesaRespuestaPublicacion(Respuesta_Ejecucion) Then
                            SalirCiclo = 1
                        Else

                            progresoActual = calculaSubProgreso(RegistrosDelta, RegistrosDelta, Convert.ToInt32(Respuesta_Ejecucion(0)("Registros")))
                        End If


                        Progreso = progresoActual
                        ProgressForm2.ValueAction = CInt(Math.Round(Progreso, 0))
                        Application.DoEvents()

                    End While
                    progresoActual = 0
                    Progreso = 0

                    ProgressForm2.Action = "Generando Reporte Delta Constructor"
                    ProgressForm2.ValueProcess = 3
                    ProgressForm2.ValueAction = 0
                    Application.DoEvents()

                    'EJECUCION DELTA CONSRUCTOR
                    ' subProgreso = calculaSubProgreso(RegistrosDeltaConstructor, RegistrosDeltaConstructor, CantRegDeltaConstructorEjecucion)
                    SalirCiclo = False
                    While SalirCiclo = False
                        If ProgressForm2.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")
                        '****Falta Agregar el SP de Constructor
                        Dim Respuesta_Ejecucion = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_Publicar_DELTA_Constructor_Ejecucion.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.Sesion.Usuario.id, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CInt(procesoSeleccionado), CantRegDeltaConstructorEjecucion)
                        If ProcesaRespuestaPublicacion(Respuesta_Ejecucion) Then
                            SalirCiclo = 1
                        Else

                            progresoActual = calculaSubProgreso(RegistrosDeltaConstructor, RegistrosDeltaConstructor, Convert.ToInt32(Respuesta_Ejecucion(0)("Registros")))
                        End If


                        Progreso = CInt(Math.Round(progresoActual, 0))
                        ProgressForm2.ValueAction = CInt(Math.Round(Progreso, 0))
                        Application.DoEvents()

                    End While
                    progresoActual = 0
                    Progreso = 0

                    'EJECUCION GORO
                    ProgressForm2.Action = "Generando Reporte GORO"
                    ProgressForm2.ValueProcess = 4
                    ProgressForm2.ValueAction = 0
                    Application.DoEvents()


                    'GORO -- PRINCIPAL
                    'subProgreso = calculaSubProgreso(RegistrosGoro, RegistrosGoro, CantRegGoroEjecucion)

                    SalirCiclo = False
                    While SalirCiclo = False
                        If ProgressForm2.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")
                        Dim Respuesta_Ejecucion = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_Publicar_GORO_Ejecucion_Ppal.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.Sesion.Usuario.id, CInt(procesoSeleccionado), CantRegGoroEjecucion)
                        If ProcesaRespuestaPublicacion(Respuesta_Ejecucion) Then
                            SalirCiclo = 1
                        Else

                            progresoActual = calculaSubProgreso(RegistrosGoro, RegistrosGoro, Convert.ToInt32(Respuesta_Ejecucion(0)("Registros")))
                        End If
                        Progreso = CInt(Math.Round(progresoActual, 0))
                        ProgressForm2.ValueAction = CInt(Math.Round(Progreso, 0))
                        Application.DoEvents()
                    End While
                    progresoActual = 0
                    Progreso = 0

                    'EJECUCION GORO
                    ProgressForm2.Action = "Generando Reporte GORO Faltantes"
                    ProgressForm2.ValueProcess = 5
                    ProgressForm2.ValueAction = 0
                    Application.DoEvents()

                    ' Publicar Goro -- FALTANTES
                    'subProgreso = calculaSubProgreso(RegistrosGoroFaltante, RegistrosGoroFaltante, CantRegGoroFaltanteEjecucion)

                    SalirCiclo = False
                    While SalirCiclo = False
                        If ProgressForm2.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")
                        Dim Respuesta_Ejecucion = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_Publicar_GORO_Ejecucion.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.Sesion.Usuario.id, CInt(procesoSeleccionado), CantRegGoroFaltanteEjecucion)
                        If ProcesaRespuestaPublicacion(Respuesta_Ejecucion) Then
                            SalirCiclo = 1
                        Else

                            progresoActual = calculaSubProgreso(RegistrosGoroFaltante, RegistrosGoroFaltante, Convert.ToInt32(Respuesta_Ejecucion(0)("Registros")))
                        End If
                        Progreso = CInt(Math.Round(progresoActual, 0))
                        ProgressForm2.ValueAction = CInt(Math.Round(Progreso, 0))
                        Application.DoEvents()
                    End While
                    progresoActual = 0
                    Progreso = 0

                    'SIPLA.
                    'subProgreso = calculaSubProgreso(RegistrosSipla, RegistrosSipla, CantRegSiplaEjecucion)

                    ProgressForm2.Action = "Generando Reporte SIPLA"
                    ProgressForm2.ValueProcess = 6
                    ProgressForm2.ValueAction = 0
                    Application.DoEvents()

                    SalirCiclo = False
                    While SalirCiclo = False
                        If ProgressForm2.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")
                        Dim Respuesta_Ejecucion = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_Publicar_SIPLA_Ejecucion.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.Sesion.Usuario.id, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CInt(procesoSeleccionado), CantRegSiplaEjecucion)
                        If ProcesaRespuestaPublicacion(Respuesta_Ejecucion) Then
                            SalirCiclo = 1
                        Else

                            progresoActual = calculaSubProgreso(RegistrosSipla, RegistrosSipla, Convert.ToInt32(Respuesta_Ejecucion(0)("Registros")))
                        End If
                        Progreso = CInt(Math.Round(progresoActual, 0))
                        ProgressForm2.ValueAction = CInt(Math.Round(Progreso, 0))
                        Application.DoEvents()

                    End While
                    progresoActual = 0
                    Progreso = 0

                    'FATCA.
                    subProgreso = calculaSubProgreso(RegistrosFatca, RegistrosFatca, CantRegFatcaEjecucion)

                    ProgressForm2.Action = "Generando Reporte Fatca"
                    ProgressForm2.ValueProcess = 7
                    ProgressForm2.ValueAction = 0
                    Application.DoEvents()

                    SalirCiclo = False
                    While SalirCiclo = False
                        If ProgressForm2.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")
                        Dim Respuesta_Ejecucion = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_Publicar_FATCA_Ejecucion.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.Sesion.Usuario.id, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CInt(ProcesoDesktopComboBox.SelectedValue), CantRegFatcaEjecucion)
                        If ProcesaRespuestaPublicacion(Respuesta_Ejecucion) Then
                            SalirCiclo = 1
                        Else

                            progresoActual = calculaSubProgreso(RegistrosFatca, RegistrosFatca, Convert.ToInt32(Respuesta_Ejecucion(0)("Registros")))
                        End If
                        Progreso = CInt(Math.Round(progresoActual, 0))
                        ProgressForm2.ValueAction = CInt(Math.Round(Progreso, 0))
                        Application.DoEvents()

                    End While

                    Application.DoEvents()

                    ProgressForm2.Hide()
                    MessageBox.Show("Proceso de Publicacion Exitoso", "Resultado de la Publicacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Else
                    ProgressForm2.Hide()
                    MessageBox.Show("No se puede Publicar la fecha Seleccionada, Fecha ya Publicada.", "Publicacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If


            Catch ex As Exception
                'ProgressForm2.Hide()
                Error_Publicacion = True
                ErrorPublic = False
                MessageBox.Show(ex.Message, "Publicacion", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ProgressForm2.Hide()
                ProgressForm2.Dispose()
                DeshabilitaControles(True)
            End Try

            If Error_Publicacion Then
                'dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                Try
                    dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_Publicar_Reversar.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), CInt(procesoSeleccionado), _Plugin.Manager.Sesion.Usuario.id)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Publicacion", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If


        End Sub

        Private Function calculaSubProgreso(TotalReg_Reporte As Integer, TotalReg_Sumatoria As Integer, CantidadReg_Ejecucion As Integer) As Double
            Dim subProgreso As Double
            Dim PorcentajeEjecucion As Double

            If TotalReg_Reporte > 0 Then
                PorcentajeEjecucion = ((100 * TotalReg_Reporte) / TotalReg_Sumatoria)
                subProgreso = (PorcentajeEjecucion / (TotalReg_Reporte / CantidadReg_Ejecucion))
                If subProgreso = 0 Then
                    subProgreso = 1
                End If

                If subProgreso > PorcentajeEjecucion Then
                    subProgreso = PorcentajeEjecucion
                End If
            Else
                subProgreso = 0
            End If

            Return subProgreso
        End Function

        Private Function ProcesaRespuestaPublicacion(ByVal dt As DataTable) As Boolean


            If dt(0)("Error") = True Then
                Throw New Exception(dt(0)("MSJ_ERROR"))
            End If

            Return CBool(dt.Rows(0)("Finalizado"))

        End Function

        Private Sub DeshabilitaControles(ByVal opcion As Boolean)

            Me.FechaProcesodateTimePicker.Enabled = opcion
            Me.CruzarButton.Enabled = opcion
            Me.PublicarButton.Enabled = opcion
            Me.ProcesoDesktopComboBox.Enabled = opcion

        End Sub


#End Region


    End Class

End Namespace
