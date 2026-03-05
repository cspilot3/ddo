Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports System.Windows.Forms
Imports Slyg.Tools
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools.Progress
Imports System.Text
Imports Microsoft.Reporting.WinForms

Namespace Imaging.Forms.Cierre

    Public Class FormCierre

#Region " Declaraciones "

        Private _Plugin As BanagrarioImagingPlugin
        Private _RegionalDataTable As DBAgrario.SchemaConfig.TBL_RegionalDataTable
        Private _COBDataTable As DBAgrario.SchemaConfig.TBL_COBDataTable
        Private _OficinaDataTable As DBAgrario.SchemaConfig.TBL_OficinaDataTable

#End Region

#Region " Contructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)
            InitializeComponent()

            _Plugin = nBanagrarioDesktopPlugin
            CargaTablas()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormCierre_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaRegional()
            CargarFechasPendientesCruce()
            CargaFechasPendientesPublicacion()
            Me.CierreReportViewer.RefreshReport()
        End Sub

        Private Sub RegionalDesktopComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RegionalDesktopComboBox.SelectedIndexChanged
            CargaCOB()
        End Sub

        Private Sub COBDesktopComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles COBDesktopComboBox.SelectedIndexChanged
            CargaOficina()
        End Sub

        Private Sub CruzarButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CruzarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar el Cruce?", "Cruce", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                GeneraCruce()
            End If
        End Sub

        Private Sub PublicarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PublicarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea Publicar los resultados del Proceso?", "Publicación", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)
            If (Respuesta = DialogResult.OK) Then
                PublicarInformacion()
                CargaFechasPendientesPublicacion()
            End If
        End Sub

        Private Sub PrepararInformeButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PrepararInformeButton.Click
            If (DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea preparar la información?, este proceso puede tardar algunos minutos.", "Preparar Informe", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False) = DialogResult.OK) Then
                Application.DoEvents()
                PrepararInformacion()
                CargarFechasPendientesCruce()
            End If
        End Sub

        Private Sub CierreTabControl_Selected(ByVal sender As System.Object, ByVal e As TabControlEventArgs) Handles CierreTabControl.Selected
            If e.TabPageIndex = 1 Then
                CargarReporte()
            End If
        End Sub

        Private Sub EnviarCorreoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EnviarCorreoButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea Enviar Correo?", "EnviarCorreo", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                EnviarCorreoOficinas()
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaTablas()
            Dim dmBanAgrario As New DBAgrario.DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            Try

                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                _RegionalDataTable = dmBanAgrario.SchemaConfig.TBL_Regional.DBGet(Nothing)
                _COBDataTable = dmBanAgrario.SchemaConfig.TBL_COB.DBGet(Nothing)
                _OficinaDataTable = dmBanAgrario.SchemaConfig.TBL_Oficina.DBGet(Nothing)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub CargaRegional()
            Try
                Utilities.LlenarCombo(RegionalDesktopComboBox, _RegionalDataTable, DBAgrario.SchemaConfig.TBL_RegionalEnum.id_Regional.ColumnName, DBAgrario.SchemaConfig.TBL_RegionalEnum.Nombre_Regional.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            End Try
        End Sub

        Private Sub CargarFechasPendientesCruce()
            Dim dmBanAgrario As New DBAgrario.DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            Try

                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim FechasDataTable = dmBanAgrario.SchemaProcess.CTA_Fechas_Pendientes_Cruce.DBGet(0, New DBAgrario.SchemaProcess.CTA_Fechas_Pendientes_CruceEnumList(DBAgrario.SchemaProcess.CTA_Fechas_Pendientes_CruceEnum.Fecha_Movimiento, True))

                FechasPendientesCruceListBox.Items.Clear()

                For Each Fecha In FechasDataTable
                    FechasPendientesCruceListBox.Items.Add(Fecha.Fecha_Movimiento)
                Next

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarFechasPendientesCruce", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try
        End Sub


        Private Sub CargaFechasPendientesPublicacion()

            Dim dbm_BanAgrario As New DBAgrario.DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            Try
                dbm_BanAgrario.Connection_Open(Me._Plugin.Manager.Sesion.Usuario.id)
                Dim FechasPublicacionDataTable = dbm_BanAgrario.SchemaProcess.CTA_Fechas_Pendientes_Publicacion.DBGet(0, New DBAgrario.SchemaProcess.CTA_Fechas_Pendientes_PublicacionEnumList(DBAgrario.SchemaProcess.CTA_Fechas_Pendientes_PublicacionEnum.Fecha_Proceso, True))

                FechasPendientesPublicacionListBox.Items.Clear()

                For Each Fecha In FechasPublicacionDataTable
                    FechasPendientesPublicacionListBox.Items.Add(Fecha.Fecha_Proceso)
                Next
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarFechasPendientesCruce", ex)
            Finally
                dbm_BanAgrario.Connection_Close()
            End Try


        End Sub

        Private Sub CargaCOB()
            Try
                Dim COBView = _COBDataTable.DefaultView
                COBView.RowFilter = "fk_Regional = " + RegionalDesktopComboBox.SelectedValue.ToString()

                Utilities.LlenarCombo(COBDesktopComboBox, COBView.ToTable(), DBAgrario.SchemaConfig.TBL_COBEnum.id_COB.ColumnName, DBAgrario.SchemaConfig.TBL_COBEnum.Nombre_COB.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaCOB", ex)
            End Try
        End Sub

        Private Sub CargaOficina()
            Try
                Dim OficinaView = _OficinaDataTable.DefaultView
                OficinaView.RowFilter = "fk_COB = " + COBDesktopComboBox.SelectedValue.ToString()

                Utilities.LlenarCombo(OficinaDesktopComboBox, OficinaView.ToTable(), DBAgrario.SchemaConfig.TBL_OficinaEnum.id_Oficina.ColumnName, DBAgrario.SchemaConfig.TBL_OficinaEnum.Nombre_Oficina.ColumnName, True, "-1", "--TODOS--")

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaOficina", ex)
            End Try
        End Sub

        Private Sub PrepararInformacion()

            Dim dmBanAgrario As New DBAgrario.DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            Try
                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                Dim Regional = CShort(RegionalDesktopComboBox.SelectedValue)
                Dim COB = CShort(COBDesktopComboBox.SelectedValue)
                Const Oficina As Integer = -1
                Me.Cursor = Cursors.AppStarting
                dmBanAgrario.SchemaCrossing.PA_01_Get_Data.DBExecute(Regional, COB, Oficina, _Plugin.Manager.Sesion.Usuario.id, FechaProcesodateTimePicker.Value.ToString("yyyy/MM/dd"))
                'dmBanAgrario.SchemaCrossing.PA_01_Get_Data.DBExecute(_Plugin.Manager.Sesion.Usuario.id, FechaProcesodateTimePicker.Value.ToString("yyyy/MM/dd"))
                DesktopMessageBoxControl.DesktopMessageShow("Se realizó la preparación de la información correctamente.", "Informe", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Me.Cursor = Cursors.Default

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Preparar Informacion", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub GeneraCruce()
            Dim dmBanAgrario As New DBAgrario.DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            Dim idProceso As Long
            Dim ProgressForm As New FormProgress()


            Try
                Me.Enabled = False
                Me.Cursor = Cursors.WaitCursor

                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                'dmBanAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                Dim ProcesoDataTable = dmBanAgrario.SchemaProcess.TBL_Proceso.DBFindByFecha_Proceso(FechaProcesodateTimePicker.Value.ToString("yyyy/MM/dd"))

                If (ProcesoDataTable.Count = 0) Then
                    MessageBox.Show("No se encuentra información asociada a la Fecha de Proceso seleccionada")
                    Return
                End If

                Dim RegistrosPendientes = dmBanAgrario.SchemaCrossing.PA_New_00_Cruce_Validacion_Registros_Pendientes.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyy/MM/dd"))

                If (RegistrosPendientes > 0) Then
                    MessageBox.Show("La Fecha de Proceso seleccionada aun tiene registros pendientes por insertar, Favor intente mas tarde.")
                    Return
                End If

                idProceso = ProcesoDataTable(0).id_Proceso

                Dim datos_Generales_Cruce = "Regional: " & CShort(RegionalDesktopComboBox.SelectedValue) & vbCrLf & _
                                            " COB: " & CShort(COBDesktopComboBox.SelectedValue) & vbCrLf & _
                                            " PC: " & Environment.MachineName & vbCrLf & _
                                            " Usuario: " & _Plugin.Manager.Sesion.Usuario.id & vbCrLf & _
                                            " Cant. de Transacciones: " & 0

                ReportarLogCruce(dmBanAgrario, idProceso, CShort(DBAgrario.Tipo_Log_CruceEnum.Inicio_Cruce), datos_Generales_Cruce)

                Dim OficinasCruce As DBAgrario.SchemaConfig.CTA_Regional_COB_OficinaDataTable
                Dim OficinasDictionary As New Dictionary(Of Integer, String)

                If (Integer.Parse(RegionalDesktopComboBox.SelectedValue.ToString()) = -1) Then
                    OficinasCruce = dmBanAgrario.SchemaConfig.CTA_Regional_COB_Oficina.DBFindByid_Regionalid_COBid_Oficina(Nothing, Nothing, Nothing)
                ElseIf (Integer.Parse(COBDesktopComboBox.SelectedValue.ToString()) = -1) Then
                    OficinasCruce = dmBanAgrario.SchemaConfig.CTA_Regional_COB_Oficina.DBFindByid_Regionalid_COBid_Oficina(Short.Parse(RegionalDesktopComboBox.SelectedValue.ToString()), Nothing, Nothing)
                ElseIf (Integer.Parse(OficinaDesktopComboBox.SelectedValue.ToString()) = -1) Then
                    OficinasCruce = dmBanAgrario.SchemaConfig.CTA_Regional_COB_Oficina.DBFindByid_Regionalid_COBid_Oficina(Short.Parse(RegionalDesktopComboBox.SelectedValue.ToString()), Short.Parse(COBDesktopComboBox.SelectedValue.ToString()), Nothing)
                Else
                    OficinasCruce = dmBanAgrario.SchemaConfig.CTA_Regional_COB_Oficina.DBFindByid_Regionalid_COBid_Oficina(Short.Parse(RegionalDesktopComboBox.SelectedValue.ToString()), Short.Parse(COBDesktopComboBox.SelectedValue.ToString()), Integer.Parse(OficinaDesktopComboBox.SelectedValue.ToString()))
                End If

                'Se adicional las oficinas que cumplen los filtros
                For Each oficina In OficinasCruce
                    OficinasDictionary.Add(oficina.id_Oficina, oficina.Nombre_Oficina)
                Next

                'Iniciar proceso                
                ProgressForm.Process = ""
                ProgressForm.Action = ""
                ProgressForm.ValueProcess = 0
                ProgressForm.ValueAction = 0
                ProgressForm.MaxValueProcess = OficinasDictionary.Count
                ProgressForm.MaxValueAction = 1

                ProgressForm.Show()

                Dim i As Integer = 0
                For Each Item In OficinasDictionary

                    ProgressForm.Process = Item.Value
                    ProgressForm.Action = "Ejecutando Cruce"
                    ProgressForm.ValueAction = 1
                    Application.DoEvents()
                    dmBanAgrario.SchemaCrossing.PA_New_00_Cruce.DBExecute(idProceso, CShort(Item.Key), _Plugin.Manager.Sesion.Usuario.id)
                    If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")


                    'ProgressForm.Action = "Reportando Registros Sobrantes"
                    'ProgressForm.ValueAction = 1
                    'Application.DoEvents()
                    'dmBanAgrario.SchemaCrossing.PA_New_25_Reportar_Registro_Sobrante.DBExecute(idProceso, Item.Key)
                    'If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                    'ProgressForm.Action = "Reportando Registros Sobrantes"
                    'ProgressForm.ValueAction = 1
                    'Application.DoEvents()
                    'dmBanAgrario.SchemaCrossing.PA_New_25_Reportar_Registro_Sobrante.DBExecute(idProceso, Item.Key)
                    'If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                    'ProgressForm.Action = "Reportando Soportes Sobrantes"
                    'ProgressForm.ValueAction = 2
                    'Application.DoEvents()
                    'dmBanAgrario.SchemaCrossing.PA_New_26_Reportar_Soporte_Sobrante.DBExecute(idProceso, Item.Key)
                    'If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                    'ProgressForm.Action = "Reportando Cierre"
                    'ProgressForm.ValueAction = 3
                    'Application.DoEvents()
                    'dmBanAgrario.SchemaCrossing.PA_New_27_Reportar_Cierre.DBExecute(idProceso, _Plugin.Manager.Sesion.Usuario.id, Item.Key)
                    'If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                    i += 1
                    ProgressForm.ValueProcess = i
                Next

                ProgressForm.Action = "Actualizar estados del Paquete"
                ProgressForm.ValueAction = 1
                Application.DoEvents()
                dmBanAgrario.SchemaCrossing.PA_13a_Actualizar_Estados_Paquete.DBExecute(idProceso)

                ReportarLogCruce(dmBanAgrario, idProceso, CShort(DBAgrario.Tipo_Log_CruceEnum.Inicio_Reporte_Cierre), Nothing)

                'Insertar el registro (log) del cruce
                dmBanAgrario.SchemaReport.PA_Registrar_Cruce_Log.DBExecute(CShort(RegionalDesktopComboBox.SelectedValue), _
                                                                           CShort(COBDesktopComboBox.SelectedValue), _
                                                                           FechaProcesodateTimePicker.Value.ToString("yyyy/MM/dd"), _
                                                                           _Plugin.Manager.Sesion.Usuario.id, _
                                                                           Environment.MachineName)

                ReportarLogCruce(dmBanAgrario, idProceso, CShort(DBAgrario.Tipo_Log_CruceEnum.Fin_Proceso), Nothing)


                CargarReporte()

                ' dmBanAgrario.SchemaProcess.PA_LLaves_Insercion_TBL_Error_Llaves.DBExecute(idProceso)

            Catch ex As Exception
                ProgressForm.Hide()
                Application.DoEvents()

                DesktopMessageBoxControl.DesktopMessageShow("GeneraCruce", ex)
            Finally
                ProgressForm.Visible = False
                ProgressForm.Close()

                dmBanAgrario.Connection_Close()
                Me.Enabled = True
                Me.Cursor = Cursors.Default
            End Try
        End Sub

        Private Sub CargarReporte()
            Dim dmBanAgrario As New DBAgrario.DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            Try
                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                If (Me.CierreReportViewer.LocalReport.DataSources.Count > 0) Then
                    Me.CierreReportViewer.LocalReport.DataSources.RemoveAt(0)
                End If

                Dim CierreDataTable = dmBanAgrario.SchemaReport.CTA_Reporte_Cierre.DBFindByFecha_Proceso(FechaProcesodateTimePicker.Text)
                Utilities.NewDataSource(CierreReportViewer, "CierreDataSet", CierreDataTable)
                Me.CierreReportViewer.RefreshReport()
                CierreTabControl.SelectedIndex = 1

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarReporte", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub PublicarInformacion()
            Dim dmBanAgrario As New DBAgrario.DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            Try
                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                Dim Regional = CShort(RegionalDesktopComboBox.SelectedValue)
                Dim COB = CShort(COBDesktopComboBox.SelectedValue)
                'Dim Oficina = CShort(OficinaDesktopComboBox.SelectedValue)
                Dim fechaProceso = FechaProcesodateTimePicker.Value.ToString("yyyy/MM/dd")

                dmBanAgrario.SchemaReport.PA_Publicar_Proceso_Detalle.DBExecute(Regional, _
                                                                                COB, _
                                                                                fechaProceso, _
                                                                                _Plugin.Manager.Sesion.Usuario.id, _
                                                                                Environment.MachineName)


                Dim Respuesta = MessageBox.Show("Se realizó la publicación de la información correctamente. ¿Desea enviar el correo con el informe de publicación?", "Publicación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If (Respuesta = DialogResult.Yes) Then
                    Dim message As New StringBuilder()

                    Dim consolidado = dmBanAgrario.SchemaReport.PA_Mail_Publicacion_Consolidado.DBExecute(fechaProceso)

                    ' Crear correo
                    message.AppendLine("Buen día, Nos permitimos informar que el proceso de la fecha de proceso P&C " & fechaProceso & " se encuentra publicado y arrojo los siguientes datos:<br/><br/>")

                    message.AppendLine("<table style='text-align: center; border-collapse: collapse; border-spacing: 0; font-family: Verdana;'>")
                    message.AppendLine("	<tr style='background-color: #C0C0C0; font-weight: bold; font-size: 12px;'>")
                    message.AppendLine("		<td rowspan='2' style='border: 1px solid #000000;'>Fecha Proceso<br/>P&amp;C</td>")
                    message.AppendLine("		<td rowspan='2' style='border: 1px solid #000000;'>REGIONAL</td>")
                    message.AppendLine("		<td colspan='8' style='border: 1px solid #000000;'>RESULTADO DEL PROCESO</td>")
                    message.AppendLine("	</tr>")
                    message.AppendLine("	<tr style='background-color: #C0C0C0; font-weight: bold; font-size: 10px;'>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>Cruce<br/>Exitoso")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>Cruce<br/>Automático</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>Tx. no<br/>identificada</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>Doc. no<br/>identificado</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>Soportes<br/>Sobrantes</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>Registros<br/>sobrantes</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>Validaciones<br/>de Forma</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>Contenedores<br/>Oficina</td>")
                    message.AppendLine("	</tr>")


                    Dim TotalCruceExitoso = 0
                    Dim TotalCruceAutomatico = 0
                    Dim TotalTx_No_Identificada = 0
                    Dim TotalDoc_No_Identificado = 0
                    Dim TotalSoportes_Sobrantes = 0
                    Dim TotalRegistros_Sobrantes = 0
                    Dim TotalValidaciones = 0
                    Dim TotalContenedores = 0

                    For Each item In consolidado
                        message.AppendLine("	<tr style='font-size: 10px;'>")
                        message.AppendLine("		<td style='border: 1px solid #000000;'>" & item.Fecha_Proceso & "</td>")
                        message.AppendLine("		<td style='border: 1px solid #000000; text-align: left'>" & item.Nombre_Regional & "</td>")

                        message.AppendLine("		<td style='border: 1px solid #000000;'>" & item.CruceExitoso.ToString("#,##0") & "</td>")
                        TotalCruceExitoso += item.CruceExitoso

                        message.AppendLine("		<td style='border: 1px solid #000000;'>" & item.CruceAutomatico.ToString("#,##0") & "</td>")
                        TotalCruceAutomatico += item.CruceAutomatico

                        message.AppendLine("		<td style='border: 1px solid #000000;'>" & item.Tx_No_Identificada.ToString("#,##0") & "</td>")
                        TotalTx_No_Identificada += item.Tx_No_Identificada

                        message.AppendLine("		<td style='border: 1px solid #000000;'>" & item.Doc_No_Identificado.ToString("#,##0") & "</td>")
                        TotalDoc_No_Identificado += item.Doc_No_Identificado

                        message.AppendLine("		<td style='border: 1px solid #000000;'>" & item.Soportes_Sobrantes.ToString("#,##0") & "</td>")
                        TotalSoportes_Sobrantes += item.Soportes_Sobrantes

                        message.AppendLine("		<td style='border: 1px solid #000000;'>" & item.Registros_Sobrantes.ToString("#,##0") & "</td>")
                        TotalRegistros_Sobrantes += item.Registros_Sobrantes

                        message.AppendLine("		<td style='border: 1px solid #000000;'>" & item.Validaciones.ToString("#,##0") & "</td>")
                        TotalValidaciones += item.Validaciones

                        message.AppendLine("		<td style='border: 1px solid #000000;'>" & item.Contenedores.ToString("#,##0") & "</td>")
                        TotalContenedores += item.Contenedores

                        message.AppendLine("	</tr>")
                    Next

                    message.AppendLine("	<tr style='font-weight: bold; font-size: 12px;'>")
                    message.AppendLine("		<td colspan='2' style='border: 1px solid #000000;'>TOTALES</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotalCruceExitoso.ToString("#,##0") & "</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotalCruceAutomatico.ToString("#,##0") & "</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotalTx_No_Identificada.ToString("#,##0") & "</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotalDoc_No_Identificado.ToString("#,##0") & "</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotalSoportes_Sobrantes.ToString("#,##0") & "</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotalRegistros_Sobrantes.ToString("#,##0") & "</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotalValidaciones.ToString("#,##0") & "</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotalContenedores.ToString("#,##0") & "</td>")
                    message.AppendLine("	</tr>")
                    message.AppendLine("</table>")

                    message.AppendLine("<br/>")
                    message.AppendLine("<b>Nota:</b> Este correo se genera automáticamente, favor no responder.<br/><br/><br/>")

                    message.AppendLine("Gracias por su atención,<br/><br/><br/>")

                    message.AppendLine("<b>Procesos & Canje S.A</b><br/>")
                    message.AppendLine("Calle 20B N. 43ª 60 INT 3<br/>")
                    message.AppendLine("Bogotá D.C.<br/>")
                    message.AppendLine("Colombia<br/>")
                    message.AppendLine("PBX (571) 423 2180<br/>")

                    ' Crear adjunto
                    Dim mimeType As String = ""
                    Dim encoding As String = ""
                    Dim fileNameExtension As String = ""
                    Dim warnings As Warning() = Nothing
                    Dim streamids As String() = Nothing

                    Dim report = New ReportViewer()

                    Dim CierreCorreoDataTable = dmBanAgrario.SchemaReport.PA_Mail_Publicacion_Detalle.DBExecute(fechaProceso)

                    report.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.ResultadoCierreCorreo.rdlc"
                    report.LocalReport.DataSources.Add(New ReportDataSource("CierreDataSet", CType(CierreCorreoDataTable, DataTable)))

                    Dim attach = report.LocalReport.Render("Excel", Nothing, mimeType, encoding, fileNameExtension, streamids, warnings)

                    ' Leer lista de correos
                    Dim emailList = dmBanAgrario.SchemaConfig.TBL_Mail_List_Item.DBFindByfk_Mail_ListActivo(DBAgrario.EmailListEnum.Notificacion_Publicacion, True)
                    Dim emails As String = ""
                    For Each mailtem As DBAgrario.SchemaConfig.TBL_Mail_List_ItemRow In emailList
                        If (emails <> "") Then emails &= ";"
                        emails &= mailtem.Email
                    Next

                    SendMail(emails, "", "", "Reporte del Resultado del Proceso: " & fechaProceso, message.ToString(), "Reporte del proceso_" & Replace(fechaProceso, "/", "") & ".xls", attach)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("PublicarInformacion", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub SendMail(ByVal MailTo As String, ByVal MailCC As String, ByVal MailCCO As String, ByVal Subject As String, ByVal Message As String, nAttachName As String, nAttach As Byte())
            Dim DBMTools = New DBTools.DBToolsDataBaseManager(Me._Plugin.ToolsConnectionString)

            Try
                DBMTools.Connection_Open()

                DBMTools.InsertMail(1, 137, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Envio de Correo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                DBMTools.Connection_Close()
            End Try
        End Sub

        Private Function GetDataTableAsHTML(ByVal thisTable As DataTable) As String
            Dim sb = New StringBuilder()
            Dim DBMaAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim Saludo As String = ""
            Dim Firma As String = ""
            Try
                DBMaAgrario = New DBAgrario.DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

                DBMaAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                Dim DTFormato = DBMaAgrario.SchemaConfig.PA_Formato_Correo.DBExecute

                If DTFormato.Count > 0 Then

                    For Each ParametroRow In DTFormato

                        If ParametroRow.Nombre_Parametro = "Formato_Saludo_Correo_RS" Then
                            Saludo = ParametroRow.Valor_Parametro
                        End If

                        If ParametroRow.Nombre_Parametro = "Formato_Firma_Correo_RS" Then
                            Firma = ParametroRow.Valor_Parametro
                        End If
                    Next


                End If

                'Saludo
                sb.Append(Saludo)

                'Cuerpo del correo, detalle de los Registros Sobrantes
                sb.Append("<TABLE BORDER=1>")
                sb.Append("<TR>")

                'first append the column names.
                For Each column As DataColumn In thisTable.Columns
                    sb.Append("<TD ALIGN=CENTER WIDTH=350><FONT SIZE=2><B>")
                    sb.Append(column.ColumnName)
                    sb.Append("</B></FONT></TD>")
                Next

                sb.Append("</TR>")

                'next, the column values.
                For Each row As DataRow In thisTable.Rows
                    sb.Append("<TR>")

                    For Each column As DataColumn In thisTable.Columns

                        sb.Append("<TD>")
                        If (row(column).ToString().Trim().Length > 0) Then
                            sb.Append(row(column))
                        Else
                            sb.Append(" ")
                        End If
                        sb.Append("</TD>")

                    Next
                    sb.Append("</TR>")
                Next
                sb.Append("</TABLE>")

                sb.Append("<BR>")
                'Firma
                sb.Append(Firma)

            Catch ex As Exception

            Finally
                DBMaAgrario.Connection_Close()
            End Try

            Return sb.ToString()

        End Function

        Private Sub ReportarLogCruce(ByVal dmBanAgrario As DBAgrario.DBAgrarioDataBaseManager, ByVal nProceso As Long, ByVal nTipo As Short, ByVal nObservacion As SlygNullable(Of String))
            Try
                Dim LogCruceRow As New DBAgrario.SchemaAudit.TBL_Log_CruceType()

                LogCruceRow.fk_proceso = nProceso
                LogCruceRow.fk_Log_Proceso_Tipo = nTipo
                LogCruceRow.Fecha_Log = SlygNullable.SysDate
                LogCruceRow.Observacion = nObservacion
                dmBanAgrario.SchemaAudit.TBL_Log_Cruce.DBInsert(LogCruceRow)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub EnviarCorreoOficinas()
            Dim dmBanAgrario As New DBAgrario.DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

            Try

                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                'dmBanAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                Dim OficinasCruce As DBAgrario.SchemaConfig.CTA_Regional_COB_OficinaDataTable
                Dim OficinasDictionary As New Dictionary(Of Integer, String)

                If (Integer.Parse(RegionalDesktopComboBox.SelectedValue.ToString()) = -1) Then
                    OficinasCruce = dmBanAgrario.SchemaConfig.CTA_Regional_COB_Oficina.DBFindByid_Regionalid_COBid_Oficina(Nothing, Nothing, Nothing)
                ElseIf (Integer.Parse(COBDesktopComboBox.SelectedValue.ToString()) = -1) Then
                    OficinasCruce = dmBanAgrario.SchemaConfig.CTA_Regional_COB_Oficina.DBFindByid_Regionalid_COBid_Oficina(Short.Parse(RegionalDesktopComboBox.SelectedValue.ToString()), Nothing, Nothing)
                ElseIf (Integer.Parse(OficinaDesktopComboBox.SelectedValue.ToString()) = -1) Then
                    OficinasCruce = dmBanAgrario.SchemaConfig.CTA_Regional_COB_Oficina.DBFindByid_Regionalid_COBid_Oficina(Short.Parse(RegionalDesktopComboBox.SelectedValue.ToString()), Short.Parse(COBDesktopComboBox.SelectedValue.ToString()), Nothing)
                Else
                    OficinasCruce = dmBanAgrario.SchemaConfig.CTA_Regional_COB_Oficina.DBFindByid_Regionalid_COBid_Oficina(Short.Parse(RegionalDesktopComboBox.SelectedValue.ToString()), Short.Parse(COBDesktopComboBox.SelectedValue.ToString()), Integer.Parse(OficinaDesktopComboBox.SelectedValue.ToString()))
                End If

                'Se adicional las oficinas que cumplen los filtros
                For Each oficina In OficinasCruce
                    OficinasDictionary.Add(oficina.id_Oficina, oficina.Nombre_Oficina)
                Next

                'Iniciar proceso
                Dim ProgressForm As New FormProgress()

                ProgressForm.Process = ""
                ProgressForm.Action = ""
                ProgressForm.ValueProcess = 0
                ProgressForm.ValueAction = 0
                ProgressForm.MaxValueProcess = OficinasDictionary.Count
                ProgressForm.MaxValueAction = 1

                ProgressForm.Show()


                Dim i = 0
                For Each Item In OficinasDictionary

                    ProgressForm.Process = Item.Value
                    ProgressForm.Action = "Enviando Correo"
                    ProgressForm.ValueAction = 1
                    Application.DoEvents()

                    Dim datos = dmBanAgrario.SchemaReport.PA_Gestion_Registros_Sobrantes.DBExecute(-1, -1, Item.Key, FechaProcesodateTimePicker.Text)

                    If datos.Rows.Count > 0 Then
                        Dim OficinaDataTable = dmBanAgrario.SchemaProcess.CTA_Oficina_Correos.DBFindByid_Oficina(Item.Key)

                        If (OficinaDataTable.Count > 0) Then
                            Dim OficinaRow = CType(OficinaDataTable.Rows(0), DBAgrario.SchemaProcess.CTA_Oficina_CorreosRow)
                            If Not OficinaRow.IsCorreo_ContactoNull() Then
                                Dim DTFormato = dmBanAgrario.SchemaConfig.PA_Formato_Correo.DBExecute
                                Dim Asunto As String = ""
                                If DTFormato.Count > 0 Then

                                    For Each ParametroRow In DTFormato

                                        If ParametroRow.Nombre_Parametro = "Asunto_Correo_RS" Then
                                            Asunto = ParametroRow.Valor_Parametro
                                        End If
                                    Next

                                End If
                                Asunto = Replace(Replace(Asunto, "@Oficina", Item.Value), "@FechaMovimiento", FechaProcesodateTimePicker.Text)

                                'enviar_correo(OficinaRow.Correo_Contacto, "", "", GetDataTableAsHTML(datos), "Reporte de Movimiento Físico pendiente de Envío a P&C - Oficina: " & Item.Value & " - Fecha Movimiento Banco: " & FechaProcesodateTimePicker.Text)
                                SendMail(OficinaRow.Correo_Contacto, "", "", Asunto, GetDataTableAsHTML(datos), "", Nothing)
                                'punteo.bac@procesosycanje.com.co; punteo.electronico@bancoagrario.gov.co
                            End If
                        End If
                    End If


                    If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                    i += 1
                    ProgressForm.ValueProcess = i
                Next

                ProgressForm.Close()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("EnvioCorreo", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try
        End Sub

#End Region

    End Class
End Namespace