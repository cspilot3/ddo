Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config
Imports DBAgrario
Imports DBAgrario.SchemaConfig
Imports DBAgrario.SchemaProcess
Imports DBImaging.SchemaProcess
Imports Slyg.Tools.Progress
Imports System.Text
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports Microsoft.Reporting.WinForms

Namespace Firmas.Forms.PrepararData
    Public Class FormPrepararData

#Region " Declaraciones "

        Public Plugin As FirmasImagingPlugin
        Private _publicacion As Integer = 2

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As FirmasImagingPlugin)
            InitializeComponent()

            Plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub CruzarButton_Click(sender As System.Object, e As EventArgs) Handles CruzarButton.Click
            'Valida que exista un archivo log cargado
            Try
                If ArchivoCargado() Then
                    If Validar() Then
                        PrepararData()
                        RealizarCruce()
                    End If
                Else
                    MessageBox.Show("No se realizó el cargue del archivo log para la fecha de proceso : " & DateTime.Parse(fechaProcesoDateTimePicker.Value.ToString).ToString("yyyyMMdd"), "No Existe archivo Log", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                DMB.DesktopMessageShow("Preparacion de Data", ex)
            End Try

        End Sub

        Private Sub PublicarButton_Click(sender As System.Object, e As EventArgs) Handles PublicarButton.Click
            If ArchivoCargado() Then
                PublicarData()
            Else
                MessageBox.Show("No se realizó el cargue del archivo log para la fecha de proceso : " & DateTime.Parse(fechaProcesoDateTimePicker.Value.ToString).ToString("yyyyMMdd"), "No Existe archivo Log", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub EnviarCorreoButton_Click(sender As System.Object, e As EventArgs) Handles EnviarCorreoButton.Click
            Dim Respuesta = DMB.DesktopMessageShow("¿Está seguro que desea Enviar Correo?", "EnviarCorreo", DMB.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                EnviarCorreo()
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub PrepararData()
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(Plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                Dim fechaProceso = CInt(DateTime.Parse(fechaProcesoDateTimePicker.Value.ToString).ToString("yyyyMMdd"))

                Dim fechasProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidad_Procesamientofk_Entidadfk_Proyectoid_fecha_proceso(Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, fechaProceso)

                If (fechasProcesoDataTable.Rows.Count > 0) Then

                    For Each tblFechaProcesoRow As TBL_Fecha_ProcesoRow In fechasProcesoDataTable
                        If tblFechaProcesoRow.Cerrado = True Then
                            Throw New Exception("No se puede realizar cruce porque la Fecha de Proceso: " + tblFechaProcesoRow.Fecha_Proceso.ToShortDateString() + " se encuentra Cerrada")
                        End If
                    Next

                    Dim ResultadoPreparacionDataTable = dbmAgrario.SchemaFirmas.PA_Preparacion_Data_Proceso.DBExecute(CInt(fechaProceso), Plugin.Manager.Sesion.Usuario.id)

                    If ResultadoPreparacionDataTable.Rows.Count > 0 Then
                        Dim ConteoMaestro = CInt(ResultadoPreparacionDataTable.Rows(0)("ConteoMaestro"))
                        Dim ConteoDetalle = CInt(ResultadoPreparacionDataTable.Rows(0)("ConteoDetalle"))

                        MessageBox.Show("La preparación se realizo correctamente con: " + ConteoMaestro.ToString + " Tarjetas de Firmas y " + ConteoDetalle.ToString + " Recortes", "Preparacion de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    MessageBox.Show("La fecha seleccionada no se ha procesado", "Preparacion de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
                If dbmImaging IsNot Nothing Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub RealizarCruce()
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(Plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                dbmAgrario.SchemaFirmas.PA_Ejecutar_Cruce.DBExecute(CInt(DateTime.Parse(fechaProcesoDateTimePicker.Value.ToString).ToString("yyyyMMdd")), Plugin.Manager.Sesion.Usuario.id)

                MessageBox.Show("El cruce se realizó correctamente", "Cruce de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LanzarInforme()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub PublicarData()
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(Plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                Dim fechaProceso = CInt(DateTime.Parse(fechaProcesoDateTimePicker.Value.ToString).ToString("yyyyMMdd"))

                Dim fechasProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidad_Procesamientofk_Entidadfk_Proyectoid_fecha_proceso(Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, fechaProceso)

                If (fechasProcesoDataTable.Rows.Count > 0) Then
                    dbmAgrario.SchemaFirmas.PA_Publicar_Reportes.DBExecute(CInt(DateTime.Parse(fechaProcesoDateTimePicker.Value.ToString).ToString("yyyyMMdd")))
                    CorreoPublicacion(fechaProceso, dbmAgrario)
                    'MessageBox.Show("El Proceso de publicación de reportes se realizó correctamente", "Cruce de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("La fecha seleccionada no se ha procesado", "Publicacion de Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
                If dbmImaging IsNot Nothing Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub EnviarCorreo()

            Dim dmBanAgrario As New DBAgrarioDataBaseManager(Me.Plugin.BancoAgrarioConnectionString)

            Try

                dmBanAgrario.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                'dmBanAgrario.DataBase.Identifier_Date_Format = Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                Dim OficinasCruce As CTA_Regional_COB_OficinaDataTable
                Dim OficinasDictionary As New Dictionary(Of Integer, String)

                OficinasCruce = dmBanAgrario.SchemaConfig.CTA_Regional_COB_Oficina.DBFindByid_Regionalid_COBid_Oficina(Nothing, Nothing, Nothing)

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
                Dim FechaProceso = Integer.Parse(fechaProcesoDateTimePicker.Value.ToString("yyyyMMdd").Replace("/", ""))

                Dim i = 0
                For Each Item In OficinasDictionary

                    ProgressForm.Process = Item.Value
                    ProgressForm.Action = "Enviando Correo"
                    ProgressForm.ValueAction = 1
                    Application.DoEvents()


                    Dim OficinaDataTable = dmBanAgrario.SchemaProcess.CTA_Oficina_Correos.DBFindByid_Oficina(Item.Key)

                    If (OficinaDataTable.Count > 0) Then
                        Dim OficinaRow = CType(OficinaDataTable.Rows(0), CTA_Oficina_CorreosRow)
                        If Not OficinaRow.IsCorreo_ContactoNull Then

                            Dim datosCierre = dmBanAgrario.SchemaFirmasReport.PA_Resultado_Cierre_Correo.DBExecute(-1, -1, CInt(Item.Key), FechaProceso)
                            Dim datosCruce = dmBanAgrario.SchemaFirmasReport.PA_Cruce_Exitoso_Correo.DBExecute(-1, -1, CInt(Item.Key), -1, -1, FechaProceso, FechaProceso, 2)
                            Dim datosRechazadas = dmBanAgrario.SchemaFirmasReport.PA_Tarjetas_Rechazadas_Correo.DBExecute(-1, -1, CInt(Item.Key), -1, -1, FechaProceso, FechaProceso, 2)
                            Dim datosFaltantes = dmBanAgrario.SchemaFirmasReport.PA_Gestion_Tarjetas_Firmas_Faltantes_Correo.DBExecute(-1, -1, CInt(Item.Key), -1, -1, FechaProceso, FechaProceso, 2)
                            Dim datosSobrantes = dmBanAgrario.SchemaFirmasReport.PA_Soportes_Sobrantes_Correo.DBExecute(-1, -1, CInt(Item.Key), -1, -1, FechaProceso, FechaProceso, 2)
                            Dim datosDocumentosNI = dmBanAgrario.SchemaFirmasReport.PA_Documentos_No_Identificado_Correo.DBExecute(-1, -1, CInt(Item.Key), -1, -1, FechaProceso, FechaProceso, 2)

                            Dim noData = (datosCierre.Rows.Count = 0 And _
                                             datosCruce.Rows.Count = 0 And _
                                             datosRechazadas.Rows.Count = 0 And _
                                             datosFaltantes.Rows.Count = 0 And _
                                             datosSobrantes.Rows.Count = 0 And _
                                             datosDocumentosNI.Rows.Count = 0)

                            If Not noData Then

                                Dim DTFormato = dmBanAgrario.SchemaConfig.PA_Formato_Correo.DBExecute
                                Dim Asunto As String = ""
                                If DTFormato.Count > 0 Then

                                    For Each ParametroRow In DTFormato

                                        If ParametroRow.Nombre_Parametro = "Asunto_Correo_TF" Then
                                            Asunto = ParametroRow.Valor_Parametro
                                        End If
                                    Next

                                End If
                                Asunto = Replace(Replace(Asunto, "@Oficina", Item.Value), "@FechaMovimiento", fechaProcesoDateTimePicker.Text)

                                'enviar_correo(OficinaRow.Correo_Contacto, "", "", GetDataTableAsHTML(datos), "Reporte de Movimiento Físico pendiente de Envío a P&C - Oficina: " & Item.Value & " - Fecha Movimiento Banco: " & FechaProcesodateTimePicker.Text)
                                Dim CuerpoCorreo = GetDataTableAsHTML(datosCierre, datosCruce, datosRechazadas, datosFaltantes, datosSobrantes, datosDocumentosNI)

                                enviar_correo(OficinaRow.Correo_Contacto, "", "", CuerpoCorreo, Asunto)
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
                DMB.DesktopMessageShow("EnvioCorreo", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub enviar_correo(ByVal MailTo As String, ByVal MailCC As String, ByVal MailCCO As String, ByVal Message As String, ByVal Subject As String)

            'Declarando Variables  

            Dim DBMTools = New DBTools.DBToolsDataBaseManager(Me.Plugin.ToolsConnectionString)

            Try
                DBMTools.Connection_Open()

                Dim MailType = New DBTools.SchemaMail.TBL_QueueType()

                MailType.id_Queue = Guid.NewGuid()
                MailType.fk_Entidad = CShort(1)
                MailType.fk_Usuario = 137
                MailType.Fecha_Queue = DateTime.Now

                MailType.EmailAddress_Queue = MailTo
                MailType.CC_Queue = MailCC
                MailType.CCO_Queue = MailCCO

                MailType.Subject_Queue = Subject
                MailType.Message_Queue = Message

                DBMTools.SchemaMail.TBL_Queue.DBInsert(MailType)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Envio de Correo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                DBMTools.Connection_Close()
            End Try

        End Sub

        Private Sub LanzarInforme()

            Dim dmBanAgrario As New DBAgrario.DBAgrarioDataBaseManager(Me.Plugin.BancoAgrarioConnectionString)
            Try
                dmBanAgrario.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                If (Me.CierreReportViewer.LocalReport.DataSources.Count > 0) Then
                    Me.CierreReportViewer.LocalReport.DataSources.RemoveAt(0)
                End If

                Dim CierreDataTable = dmBanAgrario.SchemaFirmas.PA_Reporte_Resultado_Cierre.DBExecute(CInt(DateTime.Parse(fechaProcesoDateTimePicker.Value.ToString).ToString("yyyyMMdd")))
                Me.CierreReportViewer.LocalReport.ReportEmbeddedResource = "Banagrario.Plugin.Resultado_Cierre_Firmas.rdlc"
                Utilities.NewDataSource(CierreReportViewer, "TBL_Resultado_CierreDataSet", CierreDataTable)
                Me.CierreReportViewer.RefreshReport()

            Catch ex As Exception
                DMB.DesktopMessageShow("CargarReporte", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try

        End Sub

        Private Sub CorreoPublicacion(fechaproceso As Integer, dmBanAgrario As DBAgrarioDataBaseManager)

            Dim Respuesta = MessageBox.Show("Se realizó la publicación de la información correctamente. ¿Desea enviar el correo con el informe de publicación?", "Publicación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If (Respuesta = DialogResult.Yes) Then
                Dim message As New StringBuilder()

                Dim consolidado = dmBanAgrario.SchemaFirmasReport.PA_Resultado_Cierre_Correo_Publicacion.DBExecute(fechaproceso)

                ' Crear correo
                message.AppendLine("Buen día, Nos permitimos informar que el proceso de la fecha de proceso P&C " & fechaproceso & " se encuentra publicado y arrojo los siguientes datos:<br/><br/>")

                message.AppendLine("<table style='text-align: center; border-collapse: collapse; border-spacing: 0; font-family: Verdana;'>")
                message.AppendLine("	<tr style='background-color: #C0C0C0; font-weight: bold; font-size: 12px;'>")
                message.AppendLine("		<td rowspan='2' style='border: 1px solid #000000;'>Fecha Proceso<br/>P&amp;C</td>")
                message.AppendLine("		<td rowspan='2' style='border: 1px solid #000000;'>REGIONAL</td>")
                message.AppendLine("		<td colspan='8' style='border: 1px solid #000000;'>RESULTADO DEL PROCESO</td>")
                message.AppendLine("	</tr>")
                message.AppendLine("	<tr style='background-color: #C0C0C0; font-weight: bold; font-size: 10px;'>")
                message.AppendLine("		<td style='border: 1px solid #000000;'>Tarjetas Exitosas</td>")
                message.AppendLine("		<td style='border: 1px solid #000000;'>Tarjetas Rechazadas</td>")
                message.AppendLine("		<td style='border: 1px solid #000000;'>Tarjetas Faltantes</td>")
                message.AppendLine("		<td style='border: 1px solid #000000;'>Soportes Sobrantes</td>")
                message.AppendLine("		<td style='border: 1px solid #000000;'>Tarjetas Excluidas</td>")
                message.AppendLine("		<td style='border: 1px solid #000000;'>Doc. No Identificados</td>")
                message.AppendLine("	</tr>")

                Dim TotalTarjetasExitosas = 0
                Dim TotalTarjetasRechazadas = 0
                Dim TotalTarjetasFaltantes = 0
                Dim TotalSoportes_Sobrantes = 0
                Dim TotaltarjetasExcluidas = 0
                Dim TotalDoc_No_Identificado = 0

                For Each item As DataRow In consolidado.Rows
                    message.AppendLine("	<tr style='font-size: 10px;'>")
                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & CStr(item("Fecha_Proceso")) & "</td>")
                    message.AppendLine("		<td style='border: 1px solid #000000; text-align: left'>" & CStr(item("Nombre_Regional")) & "</td>")

                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & item("Exitosos").ToString & "</td>")
                    TotalTarjetasExitosas += CInt(item("Exitosos"))

                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & item("Rechazadas").ToString & "</td>")
                    TotalTarjetasRechazadas += CInt(item("Rechazadas"))

                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & item("Faltantes").ToString & "</td>")
                    TotalTarjetasFaltantes += CInt(item("Faltantes"))

                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & item("Sobrantes").ToString & "</td>")
                    TotalSoportes_Sobrantes += CInt(item("Sobrantes"))

                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & item("Excluidos").ToString & "</td>")
                    TotalSoportes_Sobrantes += CInt(item("Excluidos"))

                    message.AppendLine("		<td style='border: 1px solid #000000;'>" & item("Documentos no identificados").ToString & "</td>")
                    TotalDoc_No_Identificado += CInt(item("Documentos no identificados"))

                    message.AppendLine("	</tr>")
                Next

                message.AppendLine("	<tr style='font-weight: bold; font-size: 12px;'>")
                message.AppendLine("		<td colspan='2' style='border: 1px solid #000000;'>TOTALES</td>")
                message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotalTarjetasExitosas.ToString & "</td>")
                message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotalTarjetasRechazadas.ToString & "</td>")
                message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotalTarjetasFaltantes.ToString & "</td>")
                message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotalSoportes_Sobrantes.ToString & "</td>")
                message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotaltarjetasExcluidas.ToString & "</td>")
                message.AppendLine("		<td style='border: 1px solid #000000;'>" & TotalDoc_No_Identificado.ToString & "</td>")
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

                ' Leer lista de correos
                Dim emailList = dmBanAgrario.SchemaConfig.TBL_Mail_List_Item.DBFindByfk_Mail_ListActivo(_publicacion, True)
                Dim emails As String = ""
                For Each mailtem As DBAgrario.SchemaConfig.TBL_Mail_List_ItemRow In emailList
                    If (emails <> "") Then emails &= ";"
                    emails &= mailtem.Email
                Next

                SendMail(emails, "", "", "Reporte del Resultado del Proceso: " & fechaproceso, message.ToString(), "", Nothing)
            End If
        End Sub

        Private Sub SendMail(ByVal MailTo As String, ByVal MailCC As String, ByVal MailCCO As String, ByVal Subject As String, ByVal Message As String, nAttachName As String, nAttach As Byte())
            Dim DBMTools = New DBTools.DBToolsDataBaseManager(Me.Plugin.ToolsConnectionString)

            Try
                DBMTools.Connection_Open()

                DBMTools.InsertMail(1, 137, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Envio de Correo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                DBMTools.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function ArchivoCargado() As Boolean
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Try
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(Plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                Dim CargueDataTable = dbmAgrario.SchemaFirmas.TBL_Cargue.DBFindByFecha_Proceso(DateTime.Parse(fechaProcesoDateTimePicker.Value.ToString).ToString("yyyyMMdd"))
                If CargueDataTable.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                DMB.DesktopMessageShow("Validar Cargue Archivo Log", ex)
                Return False
            Finally
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
            End Try
        End Function

        Private Function GetDataTableAsHTML(nDatosCierre As DataTable, nDatosCruce As DataTable, nDatosRechazadas As DataTable, nDatosFaltantes As DataTable, nDatosSobrantes As DataTable, nDatosDocumentosNI As DataTable) As String
            Dim sb = New System.Text.StringBuilder()
            Dim sCierre = getHTML(nDatosCierre)
            Dim sCruce = getHTML(nDatosCruce)
            Dim sRechazadas = getHTML(nDatosRechazadas)
            Dim sFaltantes = getHTML(nDatosFaltantes)
            Dim sSobrantes = getHTML(nDatosSobrantes)
            Dim sDocumentosNI = getHTML(nDatosDocumentosNI)

            Dim DBMaAgrario As DBAgrarioDataBaseManager = Nothing
            Dim Saludo As String = ""
            Dim Firma As String = ""
            Try
                DBMaAgrario = New DBAgrarioDataBaseManager(Me.Plugin.BancoAgrarioConnectionString)

                DBMaAgrario.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                Dim DTFormato = DBMaAgrario.SchemaConfig.PA_Formato_Correo.DBExecute

                If DTFormato.Count > 0 Then

                    For Each ParametroRow In DTFormato

                        If ParametroRow.Nombre_Parametro = "Formato_Saludo_Correo_TF" Then
                            Saludo = ParametroRow.Valor_Parametro
                        End If

                        If ParametroRow.Nombre_Parametro = "Formato_Firma_Correo_TF" Then
                            Firma = ParametroRow.Valor_Parametro
                        End If
                    Next

                End If

                'Cuerpo del correo, detalle de los Registros Sobrantes

                Saludo = Saludo.Replace("@ReporteCierre", sCierre)
                Saludo = Saludo.Replace("@ReporteCruceExitoso", sCruce)
                Saludo = Saludo.Replace("@ReporteTarjetasRechazadas", sRechazadas)
                Saludo = Saludo.Replace("@ReporteTarjetasFaltantes", sFaltantes)
                Saludo = Saludo.Replace("@ReporteSoportesSobrantes", sSobrantes)
                Saludo = Saludo.Replace("@ReporteDocumentosNoIdentificados", sDocumentosNI)

                'Saludo
                sb.Append(Saludo)

                sb.Append("<BR>")
                'Firma
                sb.Append(Firma)

            Catch ex As Exception

            Finally
                DBMaAgrario.Connection_Close()
            End Try

            Return sb.ToString()

        End Function

        Public Function getHTML(thisTable As DataTable) As String
            Dim sb = New StringBuilder
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

            Return sb.ToString
        End Function

        Private Function Validar() As Boolean

            ' Validar si la OT se puede exportar
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerradofk_Entidad_Procesamiento(Plugin.Manager.ImagingGlobal.Entidad, _
                                                                                                                                               Plugin.Manager.ImagingGlobal.Proyecto, _
                                                                                                                                               CInt(fechaProcesoDateTimePicker.Value.ToString("yyyyMMdd")), _
                                                                                                                                               Nothing, _
                                                                                                                                               Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad)

                For Each tblOtRow As TBL_OTRow In OTDataTable

                    Dim Resultado = dbmImaging.SchemaProcess.PA_Validar_Cargado_Completo.DBExecute(tblOtRow.id_OT)

                    If (Not Resultado) Then
                        Throw New Exception("La OT: " + tblOtRow.id_OT.ToString() + " no ha sido totalmente procesada")
                    End If
                Next


            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return True
        End Function

#End Region

        Private Sub FormPrepararData_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load

            Me.CierreReportViewer.RefreshReport()
        End Sub
    End Class
End Namespace