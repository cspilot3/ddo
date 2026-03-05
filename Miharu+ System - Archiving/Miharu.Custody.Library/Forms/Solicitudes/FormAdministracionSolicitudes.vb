Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBArchiving
Imports DBCore
Imports Miharu.Custody.Library.Forms.Reportes.Solicitudes
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports System.ComponentModel

Namespace Forms.Solicitudes

    Public Class FormAdministracionSolicitudes
        Inherits FormBase

#Region " Declaraciones "
        Public _IdSolicitud As Integer = -1
        Dim dtSolicitudesEncontradas As DataTable
#End Region

#Region " Enumeraciones "
        Public Enum IconoSolicitud As Integer
            Solicitud = 0
            Folder = 1
            File = 2
            ATiempo = 3
            ProximoVencer = 4
            Vencido = 5
        End Enum
#End Region

#Region " Eventos "
        Private Sub FormAdministracionSolicitudes_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaFiltros()
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            CargaFiltroProyecto(Program.Sesion.Usuario.id, CShort(EntidadDesktopComboBox.SelectedValue))
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            Try
                If (DevuelveSeleccionado(Me.EntidadDesktopComboBox) = False) Then
                    DesktopMessageBoxControl.DesktopMessageShow("Error, debe seleccionar Entidad!!", "Error Selección", DesktopMessageBoxControl.IconEnum.ErrorIcon, True, False)
                    Me.EntidadDesktopComboBox.Focus()
                    Return
                ElseIf (DevuelveSeleccionado(Me.ProyectoDesktopComboBox) = False) Then
                    DesktopMessageBoxControl.DesktopMessageShow("Error, debe seleccionar Proyecto!!", "Error Selección", DesktopMessageBoxControl.IconEnum.ErrorIcon, True, False)
                    Me.ProyectoDesktopComboBox.Focus()
                    Return
                End If
                If (Me.DesktopTextBoxControlSolicitud.Text <> "") Then
                    Me._IdSolicitud = CInt(Me.DesktopTextBoxControlSolicitud.Text)
                End If

                CargandoPictureBox.Visible = True
                'Me.btnDetenerBusqueda.Visible = True
                Me.DesktopTextBoxControlSolicitud.Enabled = False
                BuscarSolicitudes()

                
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Error buscar Solicitud!!", ex)
            End Try

        End Sub

        Private Sub SolicitudesTreeView_AfterSelect(ByVal sender As System.Object, ByVal e As TreeViewEventArgs) Handles SolicitudesTreeView.AfterSelect
            Try
                If Not SolicitudesTreeView.SelectedNode Is Nothing Then
                    Dim idSolicitud As Integer = -1

                    'Cuando se selecciona una Solicitud, se crean los folders de esta.
                    If SolicitudesTreeView.SelectedNode.Name.Contains("root") Then
                        idSolicitud = CInt(SolicitudesTreeView.SelectedNode.Name.Replace("root-", ""))
                        CreaCarpetasSolicitud(SolicitudesTreeView.SelectedNode, idSolicitud)
                    ElseIf SolicitudesTreeView.SelectedNode.Name.Contains("folder") Then
                        idSolicitud = CInt(SolicitudesTreeView.SelectedNode.Name.Replace("folder-", ""))
                        CreaItemsSolicitud(SolicitudesTreeView.SelectedNode, idSolicitud, SolicitudesTreeView.SelectedNode.Text)
                    End If

                    'Activan las opciones de pausar y activar solicitudes.
                    If idSolicitud <> -1 Then
                        ActivaBotonesAccion(idSolicitud)
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("SolicitudesTreeView_AfterSelect", ex)
            End Try
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub PlayButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PlayButton.Click
            ProcesaSolicitud(_IdSolicitud, 1)
        End Sub

        Private Sub PauseButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PauseButton.Click
            ProcesaSolicitud(_IdSolicitud, 2)
        End Sub

        Private Sub PrintButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PrintButton.Click
            ImprimirReporte()
        End Sub

        Private Sub ReversarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReversarButton.Click
            If Not SolicitudesTreeView.SelectedNode Is Nothing Then
                Dim Folder_CBarras As String = ""
                Dim File_CBarras As String = ""

                If SolicitudesTreeView.SelectedNode.Name.Contains("root") Then
                    Folder_CBarras = ""
                    File_CBarras = ""
                ElseIf SolicitudesTreeView.SelectedNode.Name.Contains("folder") Then
                    Folder_CBarras = SolicitudesTreeView.SelectedNode.Text
                Else
                    Dim arrFile = SolicitudesTreeView.SelectedNode.Text.Split(CChar("-"))
                    File_CBarras = arrFile(0)
                End If

                ReversaSolicitud(_IdSolicitud, Folder_CBarras, File_CBarras)
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un elemento para realizar la acción.", "Reversar Solicitudes", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub DesktopTextBoxControlSolicitud_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles DesktopTextBoxControlSolicitud.KeyUp
            Dim intVariable As Integer
            If (Not Int32.TryParse(Me.DesktopTextBoxControlSolicitud.Text, intVariable)) Then
                Me.DesktopTextBoxControlSolicitud.Text = ""
            End If
        End Sub

        Private Sub SolicitudesBackgroundWorker_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles SolicitudesBackgroundWorker.DoWork
            Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
            If worker.CancellationPending Then e.Cancel = True
            Me.BuscarButton.Enabled = False


            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim nEntidad As SlygNullable(Of Integer) = Nothing
                Dim nProyecto As SlygNullable(Of Integer) = Nothing
                Dim nPrioridad As SlygNullable(Of Integer) = Nothing
                Dim nIdentificacionUsuario As SlygNullable(Of String) = Nothing

                If CInt(EntidadDesktopComboBox.SelectedValue) <> -1 Then nEntidad = CInt(EntidadDesktopComboBox.SelectedValue)
                If CInt(ProyectoDesktopComboBox.SelectedValue) <> -1 Then nProyecto = CInt(ProyectoDesktopComboBox.SelectedValue)
                If CInt(PrioridadDesktopComboBox.SelectedValue) <> -1 Then nPrioridad = CInt(PrioridadDesktopComboBox.SelectedValue)
                If IdSolicitanteDesktopTextBox.Text <> "" Then nIdentificacionUsuario = CStr(IdSolicitanteDesktopTextBox.Text)
                Dim dSolicitudes As DataTable

                If (Me._IdSolicitud < 0) Then
                    dSolicitudes = dmArchiving.Schemadbo.PA_Obtiene_Solicitudes.DBExecute(nEntidad, nProyecto, nPrioridad, nIdentificacionUsuario, Nothing, Nothing, Nothing, "ADMINISTRADORSOL")
                Else
                    dSolicitudes = dmArchiving.Schemadbo.PA_Obtiene_Solicitudes.DBExecute(nEntidad, nProyecto, nPrioridad, nIdentificacionUsuario, Nothing, Nothing, CInt(Me._IdSolicitud), "ADMINISTRADORSOL")
                End If

                If (dSolicitudes.Rows.Count > 0) Then
                    Me.dtSolicitudesEncontradas = dSolicitudes
                Else
                    'DesktopMessageBoxControl.DesktopMessageShow("No se encontraron registros para este filtro!!", "Solicitud", DesktopMessageBoxControl.IconEnum.WarningIcon, True, False)
                End If
                
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Solicitudes", ex)
            End Try

        End Sub

        Private Sub SolicitudesBackgroundWorker_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles SolicitudesBackgroundWorker.RunWorkerCompleted
            LlenarArbol(dtSolicitudesEncontradas)
            Me.SolicitudesBackgroundWorker.CancelAsync()
            Me.BuscarButton.Enabled = True
            Me.btnDetenerBusqueda.Visible = False
            Me.SolicitudesBackgroundWorker.Dispose()
            Me.dtSolicitudesEncontradas = Nothing
            Me.DesktopTextBoxControlSolicitud.Enabled = True
        End Sub

        Private Sub SolicitudesBackgroundWorker_ProgressChanged(sender As System.Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles SolicitudesBackgroundWorker.ProgressChanged
            CargandoPictureBox.Visible = True
        End Sub

        Private Sub btnDetenerBusqueda_Click(sender As System.Object, e As System.EventArgs) Handles btnDetenerBusqueda.Click
            Me.SolicitudesBackgroundWorker.Dispose()
            'Me.SolicitudesBackgroundWorker.CancelAsync()

            Me.CargandoPictureBox.Visible = False
            Me.BuscarButton.Enabled = True
            Me.btnDetenerBusqueda.Visible = False
        End Sub

#End Region

#Region " Metodos "
        Private Sub LlenarArbol(ByVal dSolicitudes As DataTable)
            Try
                If dSolicitudes.Rows.Count > 0 Then
                    Me.DesktopTextBoxControlSolicitud.Enabled = True
                    Me.dtSolicitudesEncontradas = dSolicitudes

                    For Each solicitud As DataRow In dSolicitudes.Rows
                        Dim iconoEstado As New IconoSolicitud
                        Select Case CInt(solicitud("Estado"))
                            Case 0
                                iconoEstado = IconoSolicitud.ATiempo
                            Case 1
                                iconoEstado = IconoSolicitud.ProximoVencer
                            Case 2
                                iconoEstado = IconoSolicitud.Vencido
                        End Select

                        SolicitudesTreeView.Nodes.Add("root-" & solicitud("Id_Solicitud").ToString(), solicitud("Id_Solicitud").ToString() & " - " & solicitud("Nombres").ToString(), iconoEstado, iconoEstado)
                        SolicitudesTreeView.CollapseAll()
                    Next
                    Me.CargandoPictureBox.Visible = False
                    _IdSolicitud = -1
                    ActivaBotonesAccion(_IdSolicitud)
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No se encontraron resultados para la búsqueda realizada", "Búsqueda", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Llenar Arbol", ex)
            End Try
        End Sub
        Private Function DevuelveSeleccionado(ByVal comboParameter As DesktopComboBox.DesktopComboBoxControl) As Boolean
            Dim _retorno As Boolean = False
            Try
                If (comboParameter.SelectedIndex <> 0) Then
                    _retorno = True
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Seleccionar Item", ex)
            End Try
            Return _retorno
        End Function
        Private Sub CargaFiltros()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            CheckForIllegalCrossThreadCalls = False
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                'Carga filtro de las Entidades
                Dim tEntidades = dmArchiving.Schemadbo.CTA_Entidad_Rol_Usuario.DBFindByid_Usuario(Program.Sesion.Usuario.id).DefaultView.ToTable(True)
                Utilities.LlenarCombo(EntidadDesktopComboBox, tEntidades, tEntidades.Columns("fk_Entidad").ColumnName, tEntidades.Columns("Nombre_Entidad").ColumnName, True, "-1", "Seleccionar...")

                'Carga Filtro Proyectos
                CargaFiltroProyecto(Program.Sesion.Usuario.id, -1)

                'Carga Filtro Prioridades
                Dim tPrioridad = dmArchiving.Schemadbo.CTA_Prioridad_Rol_Usuario.DBFindByfk_Usuario(Program.Sesion.Usuario.id).DefaultView.ToTable(True)
                Utilities.LlenarCombo(PrioridadDesktopComboBox, tPrioridad, tPrioridad.Columns("id_Solicitud_Prioridad").ColumnName, tPrioridad.Columns("Nombre_Solicitud_Prioridad").ColumnName, True, "-1", "Todos...")

                'Crea filtro Estado
                EstadoDesktopComboBox.ValueMember = "Value"
                EstadoDesktopComboBox.DisplayMember = "Text"

                EstadoDesktopComboBox.Items.Add(New Utilities.ListItem("-1", "Seleccionar..."))
                EstadoDesktopComboBox.Items.Add(New Utilities.ListItem("1", "Activo"))
                EstadoDesktopComboBox.Items.Add(New Utilities.ListItem("0", "Inactivo"))
                EstadoDesktopComboBox.SelectedIndex = 1
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub CargaFiltroProyecto(ByVal fk_usuario As Integer, ByVal fk_Entidad As Short)
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim tProyectos = dmArchiving.Schemadbo.CTA_Proyecto_Rol_Usuario.DBFindByid_Usuariofk_Entidad(fk_usuario, fk_Entidad)
                Utilities.LlenarCombo(ProyectoDesktopComboBox, tProyectos, tProyectos.Columns("fk_Proyecto").ColumnName, tProyectos.Columns("Nombre_Proyecto").ColumnName, True, "-1", "Seleccionar...")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltroProyecto", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub BuscarSolicitudes()
            Try
                SolicitudesTreeView.Nodes.Clear()
                If (Not Me.SolicitudesBackgroundWorker.IsBusy) Then
                    CargandoPictureBox.Visible = True
                    Me.SolicitudesBackgroundWorker.RunWorkerAsync("SolicitudesPendientes")
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscarSolicitudes", ex)
            End Try
        End Sub

        Private Sub CreaCarpetasSolicitud(ByRef treeNode As TreeNode, ByVal idSolicitud As Integer)
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                treeNode.Nodes.Clear()
                Dim dSolicitudes = dmArchiving.Schemadbo.CTA_Folders_Solicitud.DBFindByfk_SolicitudActiva_Solicitud(idSolicitud, Nothing)


                For Each solicitud In dSolicitudes
                    treeNode.Nodes.Add("folder-" & solicitud.fk_Solicitud, solicitud.CBarras_Folder, IconoSolicitud.Folder, IconoSolicitud.Folder)
                Next
                treeNode.Expand()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CreaCarpetasSolicitud", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub CreaItemsSolicitud(ByRef treeNode As TreeNode, ByVal idSolicitud As Integer, ByVal CBarrasFolder As String)
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                treeNode.Nodes.Clear()
                Dim dSolicitudes = dmArchiving.Schemadbo.CTA_Items_Solicitud.DBFindByfk_SolicitudCBarras_Folderfk_Estado(idSolicitud, CBarrasFolder, Nothing)
                For Each solicitud In dSolicitudes
                    treeNode.Nodes.Add("file-" & solicitud.CBarras_File, solicitud.CBarras_File & "-" & solicitud.Nombre_Documento, IconoSolicitud.File, IconoSolicitud.File)
                Next
                treeNode.Expand()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CreaItemsSolicitud", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub ActivaBotonesAccion(ByVal idSolicitud As Integer)
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim dSolicitud = dmArchiving.SchemaCustody.TBL_Solicitud.DBFindByid_Solicitud(idSolicitud)
                If dSolicitud.Count > 0 Then
                    If dSolicitud(0).Bloqueada Then
                        PlayButton.Enabled = True
                        PauseButton.Enabled = False
                    Else
                        PlayButton.Enabled = False
                        PauseButton.Enabled = True
                    End If
                    _IdSolicitud = idSolicitud
                Else
                    PlayButton.Enabled = False
                    PauseButton.Enabled = False
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ActivaBotonesAccion", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub ProcesaSolicitud(ByVal IdSolicitud As Integer, ByVal Tipo As Byte)
            '1: Play - 2: Pause
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim Accion As String = ""
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dmArchiving.Transaction_Begin()

                Dim tSolicitud As New DBArchiving.SchemaCustody.TBL_SolicitudType
                Select Case Tipo
                    Case 1
                        tSolicitud.Bloqueada = False
                        Accion = "Activo"
                    Case 2
                        tSolicitud.Bloqueada = True
                        Accion = "Pauso"
                End Select
                dmArchiving.SchemaCustody.TBL_Solicitud.DBUpdate(tSolicitud, IdSolicitud)

                dmArchiving.Transaction_Commit()
                _IdSolicitud = -1
                ActivaBotonesAccion(_IdSolicitud)
                SolicitudesTreeView.CollapseAll()
                DesktopMessageBoxControl.DesktopMessageShow("Se " & Accion & " la solicitud número " & IdSolicitud & ".", "Activar / Pausar Solicitudes", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Catch ex As Exception
                dmArchiving.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("ActivaSolicitud", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub ReversaSolicitud(ByVal IdSolicitud As Integer, ByVal Folder_CBarras As String, ByVal File_CBarras As String)
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                'Tipo Accion:  0: Solicitud
                '              1: Folder
                '              2: File
                Dim TipoAccion As Integer = 0
                If Folder_CBarras <> "" Then
                    TipoAccion = 1
                ElseIf File_CBarras <> "" Then
                    TipoAccion = 2
                End If

                Select Case TipoAccion
                    Case 0
                        If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea reversar la solicitud número: [" & IdSolicitud & "]?", "Confirmación de Reversión", DesktopMessageBoxControl.IconEnum.WarningIcon) = DialogResult.OK Then
                            Dim dtItemsSolicitud = dmArchiving.Schemadbo.CTA_Items_Solicitud.DBFindByfk_Solicitud(IdSolicitud)
                            For Each file In dtItemsSolicitud
                                EliminaItemSolicitud(file.CBarras_File)
                            Next
                            BuscarSolicitudes()
                        End If
                    Case 1
                        If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea reversar la solicitud de la carpeta: [" & Folder_CBarras & "]?", "Confirmación de Reversión", DesktopMessageBoxControl.IconEnum.WarningIcon) = DialogResult.OK Then
                            Dim dtItemsFolder = dmArchiving.Schemadbo.CTA_Items_Solicitud.DBFindByCBarras_Folderfk_Solicitud(Folder_CBarras, IdSolicitud)
                            For Each file In dtItemsFolder
                                EliminaItemSolicitud(file.CBarras_File)
                            Next
                            BuscarSolicitudes()
                        End If
                    Case 2
                        If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea reversar la solicitud del documento: [" & File_CBarras & "]?", "Confirmación de Reversión", DesktopMessageBoxControl.IconEnum.WarningIcon) = DialogResult.OK Then
                            EliminaItemSolicitud(File_CBarras)
                            BuscarSolicitudes()
                        End If
                End Select

                'Si la solicitud no tiene items, entonces se elimina la solicitud.
                Dim dtItemsSolicitudEliminar = dmArchiving.Schemadbo.CTA_Items_Solicitud.DBFindByfk_Solicitud(IdSolicitud)
                If dtItemsSolicitudEliminar.Count = 0 Then
                    dmArchiving.SchemaCustody.TBL_Solicitud.DBDelete(IdSolicitud)
                End If

                DesktopMessageBoxControl.DesktopMessageShow("Se realizó la operación satisfactoriamente.", "Operación Éxitosa", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ReversaSolicitud", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub EliminaItemSolicitud(ByVal File_CBarras As String)
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                'Valida que el file se pueda reversar.
                'File Estado entre 1500-Salida bóveda y <1510-Alistamiento custodia.
                Dim FileEstado = dbmCore.Schemadbo.CTA_File_Estado.DBFindByModuloCBarras_File(DesktopConfig.Modulo.Archiving, File_CBarras)(0)
                If FileEstado.fk_Estado = EstadoEnum.Bandeja_salida_boveda Or FileEstado.fk_Estado = EstadoEnum.Alistamiento_custodia Then
                    Dim tFileEstado As New DBCore.SchemaProcess.TBL_File_EstadoType
                    tFileEstado.fk_Expediente = FileEstado.fk_Expediente
                    tFileEstado.fk_Folder = FileEstado.fk_Folder
                    tFileEstado.fk_File = FileEstado.fk_File
                    tFileEstado.Modulo = FileEstado.Modulo
                    tFileEstado.fk_Estado = EstadoEnum.Custodia
                    tFileEstado.fk_Usuario = Program.Sesion.Usuario.id
                    tFileEstado.Fecha_Log = SlygNullable.SysDate

                    'Actualiza el estado del file a Custodia
                    dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(tFileEstado, FileEstado.fk_Expediente, FileEstado.fk_Folder, FileEstado.fk_File, FileEstado.Modulo)

                    'Elimina el item de la solicitud
                    Dim dtItemSolicitud = dmArchiving.SchemaCustody.TBL_Solicitud_Item.DBFindByfk_Solicitudfk_Expedientefk_Folderfk_FileActiva_Solicitud(_IdSolicitud, FileEstado.fk_Expediente, FileEstado.fk_Folder, FileEstado.fk_File, True)
                    If dtItemSolicitud.Count > 0 Then
                        dmArchiving.SchemaCustody.TBL_Solicitud_Item.DBDelete(CInt(dtItemSolicitud(0).Item("fk_Solicitud")), CShort(dtItemSolicitud(0).Item("id_Item_Solicitud")))
                    End If
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("El documento con código: [" & File_CBarras & "] no se encuentra en un estado disponible para realizar la reversión.", "Documento inválido", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("EliminaItemSolicitud", ex)
            Finally
                dbmCore.Connection_Close()
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub ImprimirReporte()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim nEntidad As SlygNullable(Of Integer) = Nothing
                Dim nProyecto As SlygNullable(Of Integer) = Nothing
                Dim nPrioridad As SlygNullable(Of Integer) = Nothing
                Dim nIdentificacionUsuario As SlygNullable(Of Integer) = Nothing

                If CInt(EntidadDesktopComboBox.SelectedValue) <> -1 Then nEntidad = CInt(EntidadDesktopComboBox.SelectedValue)
                If CInt(ProyectoDesktopComboBox.SelectedValue) <> -1 Then nProyecto = CInt(ProyectoDesktopComboBox.SelectedValue)
                If CInt(PrioridadDesktopComboBox.SelectedValue) <> -1 Then nPrioridad = CInt(PrioridadDesktopComboBox.SelectedValue)
                If IdSolicitanteDesktopTextBox.Text <> "" Then nIdentificacionUsuario = CInt(IdSolicitanteDesktopTextBox.Text)

                Dim dtSolicitudes = dmArchiving.Schemadbo.PA_Obtiene_Solicitudes_Reporte.DBExecute(nEntidad, nProyecto, nPrioridad, nIdentificacionUsuario)
                Dim ReporteSolicitudes As New FormReporteSolicitudes(dtSolicitudes)
                ReporteSolicitudes.ShowDialog()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ImprimirReporte", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub
#End Region

        
        
        
    End Class

End Namespace