Imports Miharu.Desktop.Controls.DesktopReportDataGridView
Imports Slyg.Tools
Imports Miharu.Imaging.Library.Controls
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Library.Config
Imports System.Data

Namespace Procesos.Correos

    Public Class FormSeguimientoCorreo

        ' Constantes para nombres de columnas.
        Private Class ColumnNames
            Public Const Expediente As String = "ColumnExpediente"
            Public Const Folder As String = "Columnfk_Folder"
            Public Const File As String = "Columnid_File"
            Public Const FechaLog As String = "ColumnFechaLog"
            Public Const NumeroGuia As String = "ColumnNumeroGuia"
            Public Const Correo As String = "ColumnCorreo"
            Public Const Activo As String = "ColumnActivo"
            Public Const Estado As String = "ColumnEstado"
            Public Const ReenviarCorreo As String = "ColumnReenviarCorreo"
            Public Const TipoNotificacion As String = "ColumnTipoNotificacion"
            Public Const EnviarFisico As String = "EnviarFisicoColumn"
            Public Const IdTrackingMail As String = "ColumnIdTrackingMail"
        End Class


        Private ascending As Boolean = True ' Variable para controlar el orden

#Region " Eventos "

        Private Sub FormSeguimientoCorreo_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            cargarOTs()
            cargarEstados()

            ' Suscribirse al evento ColumnHeaderMouseClick
            AddHandler DataGridViewCorreos.ColumnHeaderMouseClick, AddressOf DataGridViewCorreos_ColumnHeaderMouseClick
        End Sub

        Private Sub DataGridViewCorreos_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)

            ' Obtener el índice de la columna en la que se hizo clic
            Dim columnIndex As Integer = e.ColumnIndex

            ' Verificar si el índice es válido
            If columnIndex >= 0 And columnIndex < DataGridViewCorreos.Columns.Count Then
                Dim columnName As String = DataGridViewCorreos.Columns(columnIndex).Name

                ' Ordenar la columna seleccionada
                If ascending Then
                    DataGridViewCorreos.Sort(DataGridViewCorreos.Columns(columnIndex), System.ComponentModel.ListSortDirection.Ascending)
                Else
                    DataGridViewCorreos.Sort(DataGridViewCorreos.Columns(columnIndex), System.ComponentModel.ListSortDirection.Descending)
                End If

                ' Alternar el orden para la próxima vez que se haga clic en el encabezado
                ascending = Not ascending
            End If

            InactivarBotonEnvioCorreo()
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

        Private Sub BtnExportar_Click(sender As System.Object, e As System.EventArgs) Handles BtnExportar.Click
            If DataGridViewCorreos.RowCount > 0 Then
                Dim ParametrosForm = New FormParametrosExportacion
                Dim GrillaExportar = New DesktopReportDataGridViewControl

                GrillaExportar.InternalGridView.DataSource = DataGridViewCorreos.DataSource

                If ParametrosForm.ShowDialog = System.Windows.Forms.DialogResult.OK Then

                    GrillaExportar.Salto_Linea = ""

                    If FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd") = FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd") Then
                        GrillaExportar.Titulo = "Seguimiento Correo" + Program.ImagingGlobal.Entidad.ToString() + "_" + Program.ImagingGlobal.Proyecto.ToString() + "_" + FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd")
                    Else
                        GrillaExportar.Titulo = "Seguimiento Correo" + Program.ImagingGlobal.Entidad.ToString() + "_" + Program.ImagingGlobal.Proyecto.ToString() + "_" + FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd") + "_" + FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd")
                    End If


                    GrillaExportar.Exportar(Convert.ToBoolean(ParametrosForm.ManejaEncabezado), Convert.ToBoolean(ParametrosForm.SeparadoComa),
                                             Convert.ToBoolean(ParametrosForm.SeparadoTabulador), Convert.ToBoolean(ParametrosForm.SeparadoPuntoyComa),
                                             Convert.ToBoolean(ParametrosForm.SeparadoVacio), Convert.ToBoolean(ParametrosForm.FormatExcel),
                                             Convert.ToBoolean(ParametrosForm.FormatTexto), GrillaExportar.Salto_Linea)
                End If
            Else
                MessageBox.Show("No hay registros para exportar", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub


        Private Sub DataGridViewCorreos_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewCorreos.CellContentClick
            Dim senderGrid = DirectCast(sender, DataGridView)

            If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn And e.RowIndex >= 0 Then
                Dim cell = CType(senderGrid.Item(ColumnNames.ReenviarCorreo, e.RowIndex), DataGridViewButtonCell)

                If cell.Style.BackColor = Drawing.Color.GreenYellow Then

                    Dim rowView As DataGridViewRow = DataGridViewCorreos.Rows(e.RowIndex)
                    Dim valueTrackingNumber = senderGrid.Item(ColumnNames.NumeroGuia, e.RowIndex).Value.ToString()

                    If CBool(rowView.Cells(ColumnNames.EnviarFisico).Value) AndAlso String.IsNullOrEmpty(valueTrackingNumber) Then
                        MessageBox.Show("Envio Fisico: El Numero de Guia no puede estar vacío o contener solo espacios en blanco. Por favor, ingrese un valor válido.", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                    Dim confirmResult As DialogResult = MessageBox.Show("¿Está seguro de que desea continuar y enviar el correo?", "Confirmación de envío", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If confirmResult = DialogResult.No Then
                        Return
                    End If

                    EnviarCorreos(rowView)
                End If
            End If
        End Sub


        Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewCorreos.CellClick

            ' Validar que la fila y columna sean válidas.
            If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub

            Dim senderGrid = DirectCast(sender, DataGridView)
            Dim Cell = CType(senderGrid.Item(ColumnNames.ReenviarCorreo, e.RowIndex), DataGridViewButtonCell)

            If Cell.Style.BackColor <> Drawing.Color.GreenYellow Then
                Return
            End If

            Dim clickedColumnName = senderGrid.Columns(e.ColumnIndex).Name

            ' Procesar según la columna seleccionada.
            Select Case clickedColumnName
                Case ColumnNames.NumeroGuia
                    HandleNumeroGuiaClick(senderGrid, e)

                Case ColumnNames.Correo
                    HandleCorreoClick(senderGrid, e)
            End Select

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


#End Region

#Region " Metodos "
        ' Maneja el clic en la columna "NumeroGuia".
        Private Sub HandleNumeroGuiaClick(senderGrid As DataGridView, e As DataGridViewCellEventArgs)
            Dim emailStatus = senderGrid.Item(ColumnNames.Estado, e.RowIndex).Value.ToString()

            If emailStatus <> DBTools.EstadosCorreos.EntregadoTransportadora Then
                UpdateFieldTrackingNumber(e)
            End If
        End Sub

        Private Sub HandleCorreoClick(senderGrid As DataGridView, e As DataGridViewCellEventArgs)
            Dim isRowActive As Boolean = CBool(senderGrid.Item(ColumnNames.Activo, e.RowIndex).Value)
            Dim emailStatus = senderGrid.Item(ColumnNames.Estado, e.RowIndex).Value.ToString()

            If isRowActive AndAlso emailStatus = DBTools.EstadosCorreos.Rechazado Then
                UpdateFieldCorreo(senderGrid, e)
            End If
        End Sub

        Private Sub UpdateFieldTrackingNumber(e As System.Windows.Forms.DataGridViewCellEventArgs)
            Dim EditarCampoForm As New FormEditarCampo()

            EditarCampoForm.Text = "Editar Campo Numero Guia"
            EditarCampoForm.AnteriorDesktopTextBox.Visible = True
            EditarCampoForm.NuevoDesktopTextBox.Visible = True

            ' Editamos para campo tipo Texto
            EditarCampoForm.AnteriorDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            EditarCampoForm.NuevoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal

            EditarCampoForm.AnteriorDesktopTextBox.MaximumLength = 100
            EditarCampoForm.AnteriorDesktopTextBox.MinimumLength = 0
            EditarCampoForm.AnteriorDesktopTextBox.Usa_Decimales = False

            EditarCampoForm.NuevoDesktopTextBox.MaximumLength = 100
            EditarCampoForm.NuevoDesktopTextBox.MinimumLength = 0
            EditarCampoForm.NuevoDesktopTextBox.Usa_Decimales = False

            EditarCampoForm.AnteriorDesktopTextBox.Text = "INGRESE NUMERO DE GUIA"
            EditarCampoForm.AnteriorDesktopTextBox.Enabled = False

            If (EditarCampoForm.ShowDialog() = DialogResult.OK) Then
                Dim newTrackingNumber = EditarCampoForm.NuevoDesktopTextBox.Text.ToString().Replace(",", "")

                ' Verifica si el valor del correo es nulo, vacío o solo contiene espacios
                If String.IsNullOrWhiteSpace(newTrackingNumber) Then
                    MessageBox.Show("El Numero de Guia no puede estar vacío o contener solo espacios en blanco. Por favor, ingrese un valor válido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Dim confirmResult As DialogResult = MessageBox.Show("¿Está seguro de que desea almacenar el Numero de Guia?", "Confirmación de Guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirmResult = DialogResult.No Then
                    Return
                End If

                Dim rowView = DataGridViewCorreos.Rows(e.RowIndex)

                rowView.Cells(ColumnNames.NumeroGuia).Value = newTrackingNumber       ' Cambiamos valor del correo 
            End If
        End Sub

        Private Sub UpdateFieldCorreo(senderGrid As DataGridView, e As System.Windows.Forms.DataGridViewCellEventArgs)

            Dim EditarCampoForm As New FormEditarCampo()
            Dim cellCorreo = CType(senderGrid.Item("ColumnCorreo", e.RowIndex), DataGridViewTextBoxCell)

            EditarCampoForm.AnteriorDesktopTextBox.Visible = True
            EditarCampoForm.NuevoDesktopTextBox.Visible = True

            ' Editamos para campo tipo Texto
            EditarCampoForm.AnteriorDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            EditarCampoForm.NuevoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal

            EditarCampoForm.AnteriorDesktopTextBox.MaximumLength = 100
            EditarCampoForm.AnteriorDesktopTextBox.MinimumLength = 0
            EditarCampoForm.AnteriorDesktopTextBox.Usa_Decimales = False

            EditarCampoForm.NuevoDesktopTextBox.MaximumLength = 100
            EditarCampoForm.NuevoDesktopTextBox.MinimumLength = 0
            EditarCampoForm.NuevoDesktopTextBox.Usa_Decimales = False

            EditarCampoForm.AnteriorDesktopTextBox.Text = cellCorreo.Value.ToString()

            If (EditarCampoForm.ShowDialog() = DialogResult.OK) Then

                Dim newValueCorreo = EditarCampoForm.NuevoDesktopTextBox.Text.ToString().Replace(",", "")

                ' Verifica si el valor del correo es nulo, vacío o solo contiene espacios
                If String.IsNullOrWhiteSpace(newValueCorreo) Then
                    MessageBox.Show("El correo no puede estar vacío o contener solo espacios en blanco. Por favor, ingrese un valor válido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Dim confirmResult As DialogResult = MessageBox.Show("¿Está seguro de que desea continuar y enviar el correo?", "Confirmación de envío", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirmResult = DialogResult.No Then
                    Return
                End If

                Dim rowView = DataGridViewCorreos.Rows(e.RowIndex)

                rowView.Cells(ColumnNames.Correo).Value = newValueCorreo       ' Cambiamos valor del correo 

                EnviarCorreos(rowView)
            End If
        End Sub


        Private Sub cargarOTs()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim OTData = dbmImaging.SchemaProcess.PA_Get_OT_Rango_Fecha.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd")), CInt(FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd")), True)

                OTDesktopComboBox.DataSource = Nothing
                If OTData.Count > 0 Then
                    Utilities.LlenarCombo(OTDesktopComboBox, OTData, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, True, "-1", "--TODOS--")
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub cargarEstados()

            Dim dbmTools As DBTools.DBToolsDataBaseManager = Nothing

            Try
                dbmTools = New DBTools.DBToolsDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Tools)
                dbmTools.Connection_Open()

                Dim EstadoData = dbmTools.SchemaConfig.TBL_Estado_Correo.DBGet(Nothing)

                ESTADODesktopComboBox.DataSource = Nothing
                If EstadoData.Count > 0 Then
                    Utilities.LlenarCombo(ESTADODesktopComboBox, EstadoData, DBTools.SchemaConfig.TBL_Estado_CorreoEnum.id_Estado_Correo.ColumnName, DBTools.SchemaConfig.TBL_Estado_CorreoEnum.Nombre_Estado_Correo.ColumnName, True, "-1", "--TODOS--")
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmTools IsNot Nothing) Then dbmTools.Connection_Close()
            End Try
        End Sub

        Private Sub Buscar()
            If Validar() Then

                Dim dbmTools As DBTools.DBToolsDataBaseManager = Nothing
                Try
                    dbmTools = New DBTools.DBToolsDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Tools)
                    dbmTools.Connection_Open()

                    Dim valueOTDesktopComboBox = CInt(IIf(OTDesktopComboBox.SelectedValue.Equals("--TODOS--"), -1, OTDesktopComboBox.SelectedValue))
                    Dim valueESTADODesktopComboBox = CInt(IIf(ESTADODesktopComboBox.SelectedValue.Equals("--TODOS--"), -1, ESTADODesktopComboBox.SelectedValue))

                    Dim SeguimientoCorreoDataTable = dbmTools.SchemaMail.PA_Seguimiento_Correo.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd")), CInt(FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd")), valueOTDesktopComboBox, valueESTADODesktopComboBox)

                    DataGridViewCorreos.AutoGenerateColumns = False
                    DataGridViewCorreos.DataSource = SeguimientoCorreoDataTable
                    DataGridViewCorreos.Refresh()

                    InactivarBotonEnvioCorreo()

                    If (Me.DataGridViewCorreos.RowCount = 0) Then
                        MessageBox.Show("No se encontraron Expedientes para el rango de fechas de proceso seleccionadas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    CountEmailStatus(SeguimientoCorreoDataTable)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmTools IsNot Nothing) Then dbmTools.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub CountEmailStatus(SeguimientoCorreoDataTable As DataTable)
            Dim programadosCount As Integer = 0
            Dim enviadoCount As Integer = 0
            Dim rechazadosCount As Integer = 0
            Dim recibidoCount As Integer = 0

            ' Recorrer todas las filas del DataTable
            For Each row As DataRow In SeguimientoCorreoDataTable.Rows
                Select Case row("Estado").ToString()
                    Case DBTools.EstadosCorreos.Programado
                        programadosCount += 1
                    Case DBTools.EstadosCorreos.Enviado
                        enviadoCount += 1
                    Case DBTools.EstadosCorreos.Rechazado
                        rechazadosCount += 1
                    Case DBTools.EstadosCorreos.Recibido
                        recibidoCount += 1
                End Select
            Next

            TotalRegistrosTextBox.Text = CStr(Me.DataGridViewCorreos.RowCount)
            EstadoProgramadosTextBox.Text = CStr(programadosCount)
            EstadoEnviadosTextBox.Text = CStr(enviadoCount)
            EstadoRechazadoTextBox.Text = CStr(rechazadosCount)
            EstadoRecibidoTextBox.Text = CStr(recibidoCount)

        End Sub

        Private Sub EnviarCorreos(rowView As DataGridViewRow)

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmTools As DBTools.DBToolsDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                dbmTools = New DBTools.DBToolsDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Tools)
                dbmTools.Connection_Open()

                Dim NotificacionDatatable = dbmCore.SchemaConfig.TBL_Notificacion.DBGet(Nothing)
                Dim NotificacionPlantillaDatatable = dbmCore.SchemaConfig.TBL_Notificacion_Plantilla.DBGet(Nothing)

                Dim rowNotificacion As DBCore.SchemaConfig.TBL_NotificacionRow()
                Dim rowNotificacionPlantilla As DBCore.SchemaConfig.TBL_Notificacion_PlantillaRow()

                Dim TipoNotificacion = CInt(rowView.Cells(ColumnNames.TipoNotificacion).Value)


                rowNotificacion = CType(NotificacionDatatable.Select("id_Notificacion = " + TipoNotificacion.ToString()), DBCore.SchemaConfig.TBL_NotificacionRow())
                rowNotificacionPlantilla = CType(NotificacionPlantillaDatatable.Select("fk_Notificacion = " + TipoNotificacion.ToString()), DBCore.SchemaConfig.TBL_Notificacion_PlantillaRow())

                Dim messageContent = rowNotificacionPlantilla(0).Saludo & rowNotificacionPlantilla(0).Cuerpo & rowNotificacionPlantilla(0).Firma

                'Registro Elegido
                Dim _Expediente = CLng(rowView.Cells(ColumnNames.Expediente).Value)
                Dim _Folder = CShort(rowView.Cells(ColumnNames.Folder).Value)
                Dim _File = CInt(rowView.Cells(ColumnNames.File).Value)
                Dim _idTrackingMail As SlygNullable(Of Guid) = New SlygNullable(Of Guid)(Guid.Parse(rowView.Cells(ColumnNames.IdTrackingMail).Value.ToString()))
                Dim _EnviarFisico = CType(rowView.Cells(ColumnNames.EnviarFisico).Value, Boolean)

                Dim TrackingMailDataTable = dbmTools.SchemaMail.TBL_Tracking_Mail.DBFindByid_Tracking_Mailfk_Expedientefk_Folderfk_File(_idTrackingMail, _Expediente, _Folder, _File)

                If rowNotificacion(0).realizarTracking Then

                    'Clonamos la información del registro actual
                    Dim TrackingMailRowActually = New DBTools.SchemaMail.TBL_Tracking_MailType()
                    TrackingMailRowActually.id_Tracking_Mail = TrackingMailDataTable(0).id_Tracking_Mail
                    TrackingMailRowActually.fk_Queue = TrackingMailDataTable(0).fk_Queue
                    TrackingMailRowActually.fk_Entidad = TrackingMailDataTable(0).fk_Entidad
                    TrackingMailRowActually.fk_Proyecto = TrackingMailDataTable(0).fk_Proyecto
                    TrackingMailRowActually.fk_Expediente = TrackingMailDataTable(0).fk_Expediente
                    TrackingMailRowActually.fk_Folder = TrackingMailDataTable(0).fk_Folder
                    TrackingMailRowActually.fk_File = TrackingMailDataTable(0).fk_File
                    TrackingMailRowActually.fk_Usuario = TrackingMailDataTable(0).fk_Usuario
                    TrackingMailRowActually.Fecha_Log = TrackingMailDataTable(0).Fecha_Log
                    TrackingMailRowActually.EmailAddress_Queue = TrackingMailDataTable(0).EmailAddress_Queue
                    TrackingMailRowActually.CC_Queue = TrackingMailDataTable(0).CC_Queue
                    TrackingMailRowActually.CCO_Queue = TrackingMailDataTable(0).CCO_Queue
                    TrackingMailRowActually.Subject_Queue = TrackingMailDataTable(0).Subject_Queue
                    TrackingMailRowActually.Message_Queue = TrackingMailDataTable(0).Message_Queue
                    TrackingMailRowActually.Attach_Queue = TrackingMailDataTable(0).Attach_Queue
                    TrackingMailRowActually.AttachName_Queue = TrackingMailDataTable(0).AttachName_Queue
                    TrackingMailRowActually.Fecha_Envio = TrackingMailDataTable(0).Fecha_Envio
                    TrackingMailRowActually.fk_Estado_Correo = TrackingMailDataTable(0).fk_Estado_Correo
                    TrackingMailRowActually.Detalle_Envio = TrackingMailDataTable(0).Detalle_Envio
                    TrackingMailRowActually.IsActive = False

                    ' verifica campo Emailfrom 
                    If Not IsDBNull(TrackingMailDataTable(0)("EmailFrom")) Then
                        TrackingMailRowActually.EmailFrom = TrackingMailDataTable(0).EmailFrom
                    End If

                    'Verifica campo EmailFromdisplay
                    If IsDBNull(TrackingMailDataTable(0)("EmailFromDisplay")) Then
                        TrackingMailRowActually.EmailFromDisplay = TrackingMailDataTable(0).EmailFromDisplay
                    End If

                    dbmTools.SchemaMail.TBL_Tracking_Mail.DBUpdate(TrackingMailRowActually, TrackingMailRowActually.id_Tracking_Mail)

                    Dim EstadoCorreo As Integer = DBTools.EnumEstadosCorreos.Programado
                    If _EnviarFisico Then
                        EstadoCorreo = DBTools.EnumEstadosCorreos.ProgramadoTransportadora
                    End If

                    Dim valueTrackingNumber = rowView.Cells(ColumnNames.NumeroGuia).Value.ToString()

                    ' Asignamos valores para el nuevo registro
                    Dim mailTrackingType As New DBTools.SchemaMail.TBL_Tracking_MailType()
                    mailTrackingType.id_Tracking_Mail = Guid.NewGuid()
                    mailTrackingType.fk_Queue = Guid.NewGuid()
                    mailTrackingType.fk_Entidad = Program.ImagingGlobal.Entidad
                    mailTrackingType.fk_Proyecto = Program.ImagingGlobal.Proyecto
                    mailTrackingType.fk_Expediente = _Expediente
                    mailTrackingType.fk_Folder = _Folder
                    mailTrackingType.fk_File = _File
                    mailTrackingType.fk_Usuario = Program.Sesion.Usuario.id
                    mailTrackingType.Fecha_Log = SlygNullable.SysDate
                    mailTrackingType.Fecha_Envio = DateTime.Now
                    mailTrackingType.fk_Estado_Correo = CShort(EstadoCorreo)
                    mailTrackingType.Detalle_Envio = DBNull.Value
                    mailTrackingType.IsActive = True
                    mailTrackingType.EmailAddress_Queue = rowView.Cells("ColumnCorreo").Value.ToString()
                    mailTrackingType.CC_Queue = TrackingMailDataTable(0).CC_Queue
                    mailTrackingType.CCO_Queue = TrackingMailDataTable(0).CCO_Queue
                    mailTrackingType.Subject_Queue = rowNotificacionPlantilla(0).Asunto
                    mailTrackingType.Message_Queue = messageContent
                    mailTrackingType.AttachName_Queue = TrackingMailDataTable(0).AttachName_Queue
                    mailTrackingType.Attach_Queue = TrackingMailDataTable(0).Attach_Queue
                    mailTrackingType.Numero_Guia = valueTrackingNumber

                    If Not rowNotificacion(0).correoRemitente Is Nothing Then
                        mailTrackingType.EmailFrom = rowNotificacion(0).correoRemitente
                    End If

                    If Not rowNotificacion(0).correoDisplay Is Nothing Then
                        mailTrackingType.EmailFromDisplay = rowNotificacion(0).correoDisplay
                    End If

                    ' Insertamos nuevo registro 
                    dbmTools.SchemaMail.TBL_Tracking_Mail.DBInsert(mailTrackingType)
                End If

                MessageBox.Show("Correo programados para envio. ", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Buscar()

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmTools IsNot Nothing) Then dbmTools.Connection_Close()
            End Try
        End Sub

        Private Sub InactivarBotonEnvioCorreo()
            For Each row As DataGridViewRow In DataGridViewCorreos.Rows
                Dim cell = CType(row.Cells("ColumnReenviarCorreo"), DataGridViewButtonCell)
                If Not CBool(row.Cells("ColumnActivo").Value) Then
                    cell.FlatStyle = FlatStyle.Flat
                    cell.Style.BackColor = Drawing.Color.Red
                Else
                    cell.FlatStyle = FlatStyle.Flat
                    cell.Style.BackColor = Drawing.Color.GreenYellow
                End If
            Next
        End Sub
#End Region

    End Class
End Namespace