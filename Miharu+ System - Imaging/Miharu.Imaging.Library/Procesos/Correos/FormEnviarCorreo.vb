Imports DBTools
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports Miharu.FileProvider.Library
Imports System.IO
Imports Slyg.Tools.Imaging
Imports Miharu.Tools.Progress

Namespace Procesos.Correos
    Public Class FormEnviarCorreo

#Region " Declaraciones "
        Private ExpedientesSeleccion As New DataTable
        Private ViewExpedientes As New DataView
#End Region

#Region " Eventos "

        Private Sub FormEnviarCorreo_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            cargarOTs()
        End Sub

        Private Sub FechaProcesoDateTimePicker_ValueChanged(sender As Object, e As System.EventArgs) Handles FechaProcesoDateTimePicker.ValueChanged
            cargarOTs()
        End Sub

        Private Sub FechaProcesoFinalDateTimePicker_ValueChanged(sender As Object, e As System.EventArgs) Handles FechaProcesoFinalDateTimePicker.ValueChanged
            cargarOTs()
        End Sub

        Private Sub BuscarFechaButton_Click(sender As System.Object, e As System.EventArgs) Handles BuscarFechaButton.Click
            Buscar()
        End Sub

        Private Sub EnviarCorreoButton_Click(sender As System.Object, e As System.EventArgs) Handles EnviarCorreoButton.Click
            EnviarCorreos()
        End Sub

        Private Sub EnviarTodosCheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles EnviarTodosCheckBox.CheckedChanged
            MarcarDesmarcarCorreos(EnviarTodosCheckBox.Checked)
        End Sub

        Private Sub CerrarButton_Click(sender As System.Object, e As System.EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Dim FechaInicial = New DateTime(FechaProcesoDateTimePicker.Value.Year, FechaProcesoDateTimePicker.Value.Month, FechaProcesoDateTimePicker.Value.Day)
            Dim FechaFinal = New DateTime(FechaProcesoFinalDateTimePicker.Value.Year, FechaProcesoFinalDateTimePicker.Value.Month, FechaProcesoFinalDateTimePicker.Value.Day)

            If (FechaInicial > FechaFinal) Then
                MessageBox.Show("La fecha de Proceso final no puede ser inferior a la fecha de Proceso inicial", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            Return True
        End Function

        Private Function LlenaExpedientesSeleccion() As Integer

            'Se crea la tabla que contendrá los expedientes seleccionados en la grilla
            Dim ExpedientesSeleccion_ As New DataTable
            Dim totalExpedientesSeleccionados As Integer = 0

            For Each col As DataGridViewColumn In DataGridViewCorreos.Columns
                ExpedientesSeleccion_.Columns.Add(col.DataPropertyName)
            Next

            For Each row As DataGridViewRow In DataGridViewCorreos.Rows
                If CBool(row.Cells("EnviarCorreo").Value) Then 'Expedientes seleccionados
                    Dim newRow As DataRow = ExpedientesSeleccion_.NewRow()

                    For Each col As DataGridViewColumn In DataGridViewCorreos.Columns
                        newRow(col.Index) = row.Cells(col.Index).Value
                    Next

                    ExpedientesSeleccion_.Rows.Add(newRow)
                End If
            Next

            ExpedientesSeleccion = ExpedientesSeleccion_

            If ExpedientesSeleccion_.Rows.Count > 0 Then
                ViewExpedientes.RowFilter = String.Empty
                ViewExpedientes = New DataView(ExpedientesSeleccion_)
                totalExpedientesSeleccionados = ExpedientesSeleccion_.Rows.Count
            End If

            Return totalExpedientesSeleccionados

        End Function

#End Region

#Region " Metodos "

        Private Sub Buscar()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim ExpedientesDataTable = dbmImaging.SchemaProcess.PA_Get_Expedientes_Enviar_Correo.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd")), CInt(FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd")), CInt(IIf(OTDesktopComboBox.SelectedValue.Equals("--TODOS--"), -1, OTDesktopComboBox.SelectedValue)))

                'Cambia el nombre de acuerdo a lo configurado 
                'For Each rowExpedientes As DataRow In ExpedientesDataTable.Rows
                '    Dim nameFile As String = String.Empty

                '    Dim expediente = Convert.ToInt64(rowExpedientes("fk_Expediente"))
                '    Dim folder = Convert.ToInt16(rowExpedientes("fk_Folder"))
                '    Dim file = Convert.ToInt16(rowExpedientes("fk_File"))

                '    Dim nameFilesTable As DataTable = dbmImaging.SchemaProcess.PA_Get_NombreArchivoAdjunto.DBExecute(expediente, folder, file)
                '    Dim firstLine = nameFilesTable.Rows(0)
                '    nameFile = firstLine("Nombre_Imagen").ToString()

                '    If Not String.IsNullOrWhiteSpace(nameFile) Then
                '        rowExpedientes("NombreImagen") = nameFile + ".pdf"
                '    End If

                'Next

                DataGridViewCorreos.AutoGenerateColumns = False
                DataGridViewCorreos.DataSource = ExpedientesDataTable
                DataGridViewCorreos.Refresh()

                If (Me.DataGridViewCorreos.RowCount = 0) Then
                    MessageBox.Show("No se encontraron Expedientes para el rango de fechas de proceso seleccionadas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                DataGridViewCorreos.AutoGenerateColumns = False
                DataGridViewCorreos.DataSource = ExpedientesDataTable
                DataGridViewCorreos.Refresh()

                If (Me.DataGridViewCorreos.RowCount = 0) Then
                    MessageBox.Show("No se encontraron Expedientes para el rango de fechas de proceso seleccionadas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                TotalRegistrosTextBox.Text = CStr(Me.DataGridViewCorreos.RowCount)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub cargarOTs()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim OTData = dbmImaging.SchemaProcess.PA_Get_OT_Rango_Fecha.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd")), CInt(FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd")), True)

                OTDesktopComboBox.DataSource = Nothing
                If OTData.Count > 0 Then
                    Utilities.Llenarcombo(OTDesktopComboBox, OTData, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, True, "-1", "--TODOS--")
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub EnviarCorreos()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmTools As DBTools.DBToolsDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ProgressForm As New FormProgress

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                dbmTools = New DBTools.DBToolsDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Tools)
                dbmTools.Connection_Open()

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim ExpedientesSeleccionados As Integer = LlenaExpedientesSeleccion()

                If ExpedientesSeleccionados > 0 Then
                    Dim Respuesta As DialogResult

                    Respuesta = MessageBox.Show("Se encontraron :  " & ExpedientesSeleccionados & " Correos para remitir cartas respuestas" & vbCrLf & vbCrLf &
                                                "¿Está seguro de remitir los correos?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If Respuesta = DialogResult.Yes Then

                        Dim Progreso As Integer = 0

                        ProgressForm.Show()

                        ProgressForm.SetProceso("Enviando")
                        ProgressForm.SetAccion("Remitiendo correos...")
                        ProgressForm.SetProgreso(Progreso)
                        ProgressForm.SetMaxValue(ExpedientesSeleccionados)

                        Application.DoEvents()

                        Dim NotificacionDatatable = dbmCore.SchemaConfig.TBL_Notificacion.DBGet(Nothing)
                        Dim NotificacionPlantillaDatatable = dbmCore.SchemaConfig.TBL_Notificacion_Plantilla.DBGet(Nothing)

                        Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                        For Each rowView As System.Data.DataRowView In ViewExpedientes
                            Dim mailTrackingType As New DBTools.SchemaMail.TBL_Tracking_MailType()

                            If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                            ' Retrieve notification data
                            Dim TipoNotificacion = CInt(rowView.Row("TipoNotificacion"))
                            Dim rowNotificacion = CType(NotificacionDatatable.Select("id_Notificacion = " + TipoNotificacion.ToString()), DBCore.SchemaConfig.TBL_NotificacionRow())
                            Dim rowNotificacionPlantilla = CType(NotificacionPlantillaDatatable.Select("fk_Notificacion = " + TipoNotificacion.ToString()), DBCore.SchemaConfig.TBL_Notificacion_PlantillaRow())

                            Dim subjectContent = rowNotificacionPlantilla(0).Asunto
                            Dim messageContent = rowNotificacionPlantilla(0).Saludo & rowNotificacionPlantilla(0).Cuerpo & rowNotificacionPlantilla(0).Firma

                            Dim expedienteTrackingMail As Long = CLng(rowView.Row("fk_Expediente"))
                            Dim folderTrackingMail As Short = CShort(rowView.Row("fk_Folder"))
                            Dim fileTrackingMail As Short = CShort(rowView.Row("fk_File"))

                            Dim NotificacionParametrosDataTable = dbmCore.SchemaProcess.PA_Notificacion_Parametros.DBExecute(expedienteTrackingMail, folderTrackingMail, CShort(fileTrackingMail - 1), TipoNotificacion)

                            For Each tblNotificacionParametroRow In NotificacionParametrosDataTable
                                Select Case tblNotificacionParametroRow.fk_Parametro_tipo
                                    Case 1 ' Subject
                                        subjectContent = subjectContent.Replace(tblNotificacionParametroRow.Parametro, tblNotificacionParametroRow.Valor_Parametro).Replace("<b>", "").Replace("</b>", "")
                                    Case 2 ' Message
                                        messageContent = messageContent.Replace(tblNotificacionParametroRow.Parametro, tblNotificacionParametroRow.Valor_Parametro)
                                End Select
                            Next

                            mailTrackingType.id_Tracking_Mail = Guid.NewGuid()
                            mailTrackingType.fk_Queue = DBNull.Value
                            mailTrackingType.Id_Delivery = DBNull.Value
                            mailTrackingType.fk_Entidad = Program.ImagingGlobal.Entidad
                            mailTrackingType.fk_Expediente = expedienteTrackingMail
                            mailTrackingType.fk_Folder = folderTrackingMail
                            mailTrackingType.fk_File = CInt(fileTrackingMail)
                            mailTrackingType.fk_Usuario = Program.Sesion.Usuario.id
                            mailTrackingType.Fecha_Log = SlygNullable.SysDate
                            mailTrackingType.Fecha_Envio = DBNull.Value
                            mailTrackingType.fk_Estado_Correo = DBTools.EnumEstadosCorreos.Programado
                            mailTrackingType.Detalle_Envio = DBNull.Value
                            mailTrackingType.IsActive = True
                            mailTrackingType.EmailAddress_Queue = rowView.Row("Correo").ToString()
                            mailTrackingType.CC_Queue = ";"
                            mailTrackingType.CCO_Queue = ";"
                            mailTrackingType.Subject_Queue = subjectContent
                            mailTrackingType.Message_Queue = messageContent
                            mailTrackingType.fk_Proyecto = Program.ImagingGlobal.Proyecto

                            Dim manager As FileProviderManager = Nothing

                            manager = New FileProviderManager(expedienteTrackingMail, folderTrackingMail, fileTrackingMail, CShort(1), centro, dbmImaging, Program.Sesion.Usuario.id)
                            manager.Connect()

                            Try
                                Dim pdfFileName As String = ProcesarYGenerarPDF(manager, dbmImaging, rowView)

                                If Not String.IsNullOrEmpty(pdfFileName) Then
                                    mailTrackingType.AttachName_Queue = Path.GetFileName(pdfFileName)
                                    mailTrackingType.Attach_Queue = File.ReadAllBytes(pdfFileName)
                                End If

                                If Not rowNotificacion(0).correoRemitente Is Nothing Then
                                    mailTrackingType.EmailFrom = rowNotificacion(0).correoRemitente
                                End If

                                If Not rowNotificacion(0).correoDisplay Is Nothing Then
                                    mailTrackingType.EmailFromDisplay = rowNotificacion(0).correoDisplay
                                End If

                                dbmTools.SchemaMail.TBL_Tracking_Mail.DBInsert(mailTrackingType)

                                ' Actualizar estado
                                Dim UpdateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                                UpdateEstado.Fecha_Log = SlygNullable.SysDate
                                UpdateEstado.fk_Usuario = Program.Sesion.Usuario.id
                                UpdateEstado.fk_Estado = 60 'Correo Generado
                                dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(UpdateEstado, expedienteTrackingMail, folderTrackingMail, fileTrackingMail, DesktopConfig.Modulo.Imaging)

                                Progreso += 1
                                ProgressForm.SetProgreso(Progreso)
                                Application.DoEvents()
                            Catch ex As Exception
                                MessageBox.Show("Error al enviar correo: " & ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Finally
                                'Cerrar conexiones
                                If (manager IsNot Nothing) Then manager.Disconnect()
                            End Try
                        Next

                        MessageBox.Show("Correos seleccionados programados para envio.", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Buscar()

                    Else
                        MessageBox.Show("Operación cancelada por Usuario", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                Else
                    MessageBox.Show("No hay correos seleccionados para enviar.", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

                ProgressForm.Hide()
                Application.DoEvents()

            Finally
                'Cerrar conexiones
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmTools IsNot Nothing) Then dbmTools.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()

                ProgressForm.Close()
            End Try
        End Sub

        Private Function ProcesarYGenerarPDF(manager As FileProviderManager, ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, rowView As System.Data.DataRowView) As String
            Dim pdfFileName As String = Nothing
            Try
                Dim Folios = manager.GetFolios(CLng(rowView.Row("fk_Expediente")), CShort(rowView.Row("fk_Folder")), CShort(rowView.Row("fk_File")), CShort(1))

                Dim FileNames As New List(Of String)
                Dim FileName As String = Nothing
                Dim Compresion As ImageManager.EnumCompression

                For folio As Short = 1 To Folios
                    Dim Imagen() As Byte = Nothing
                    Dim Thumbnail() As Byte = Nothing

                    manager.GetFolio(CLng(rowView.Row("fk_Expediente")), CShort(rowView.Row("fk_Folder")), CShort(rowView.Row("fk_File")), CShort(1), folio, Imagen, Thumbnail)

                    FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    FileNames.Add(FileName)

                    Using fs = New FileStream(FileName, FileMode.Create)
                        fs.Write(Imagen, 0, Imagen.Length)
                        fs.Close()
                    End Using
                Next

                If (FileNames.Count > 0) Then
                    Dim Format As ImageManager.EnumFormat
                    Select Case Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                        Case ".bmp"
                            Format = ImageManager.EnumFormat.Bmp
                        Case ".gif"
                            Format = ImageManager.EnumFormat.Gif
                        Case ".jpg"
                            Format = ImageManager.EnumFormat.Jpeg
                            Compresion = ImageManager.EnumCompression.Jpeg
                        Case ".pdf"
                            Format = ImageManager.EnumFormat.Pdf
                            Compresion = ImageManager.EnumCompression.Jpeg
                        Case ".png"
                            Format = ImageManager.EnumFormat.Png
                        Case ".tif"
                            Format = ImageManager.EnumFormat.Tiff
                    End Select

                    Dim formatoAux As Slyg.Tools.Imaging.ImageManager.EnumFormat
                    Dim CompresionAux As Slyg.Tools.Imaging.ImageManager.EnumCompression

                    If Program.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF Then
                        formatoAux = ImageManager.EnumFormat.Pdf
                        CompresionAux = Utilities.GetEnumCompression(CType(formatoAux, DesktopConfig.FormatoImagenEnum))
                    Else
                        formatoAux = Format
                        CompresionAux = Compresion
                    End If

                    Dim ExtensionAux As String = IIf(formatoAux = ImageManager.EnumFormat.Pdf, ".pdf", Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida).ToString()

                    Dim nameImage As String = String.Empty

                    Dim nameFilesTable As DataTable = dbmImaging.SchemaProcess.PA_Get_NombreArchivoAdjunto.DBExecute(CLng(rowView.Row("fk_Expediente")), CShort(rowView.Row("fk_Folder")), CShort(rowView.Row("fk_File")), CInt(rowView.Row("fk_DocumentoPpal")), Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                    Dim nameFileImage = nameFilesTable.Rows(0)("Nombre_Imagen").ToString()

                    ' Cambia el nombre de acuerdo a la configuración o mantiene el dado
                    If Not String.IsNullOrWhiteSpace(nameFileImage) Then
                        nameImage = nameFileImage
                    Else
                        nameImage = rowView.Row("NombreImagen").ToString()
                    End If

                    Dim nombreImagenSinExtension As String = System.IO.Path.GetFileNameWithoutExtension(nameImage)
                    pdfFileName = Program.AppPath & Program.TempPath & nombreImagenSinExtension & ExtensionAux

                    ' Guardar PDF
                    '-------------------------------------------------------------------------
                    ImageManager.Save(FileNames, pdfFileName, "", ImageManager.EnumFormat.Pdf, CompresionAux, False, Program.AppPath & Program.TempPath, True)
                    '-------------------------------------------------------------------------
                End If

            Catch ex As Exception
                MessageBox.Show("Error al exportar imágenes a PDF: " & ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            Return pdfFileName
        End Function

        Private Sub MarcarDesmarcarCorreos(ByVal Marcar As Boolean)
            For Each row As DataGridViewRow In DataGridViewCorreos.Rows
                row.Cells("EnviarCorreo").Value = Marcar
            Next
        End Sub

#End Region

    End Class
End Namespace