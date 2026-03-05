Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.FileProvider.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports System.Threading
Imports System.Windows.Forms
Imports Slyg.Tools

Namespace Controller.Indexer.Capturas.Reproceso

    Public Class ReProcessController
        Inherits CapturasController

#Region " Declaraciones "

        Dim _idExpediente As Long
        Dim _idFolder As Short
        Dim _idFile As Short

#End Region

#Region " Propiedades "

        Protected Overrides ReadOnly Property ProcessName As String
            Get
                Return "Reproceso"
            End Get
        End Property

#End Region

#Region " Implementación IIndexerController "

        Public Overrides Sub Inicializar(ByVal nTempPath As String, nIndexerSesion As Security.Library.Session.Sesion, nIndexerDesktopGlobal As DesktopGlobal, nIndexerImagingGlobal As ImagingGlobal)
            MyBase.Inicializar(nTempPath, nIndexerSesion, nIndexerDesktopGlobal, nIndexerImagingGlobal)

            IndexerView.ShowDesindexarFolioButton(False)
            IndexerView.ShowSaveButton(True)
            IndexerView.ShowAddFolioButton(False)
            IndexerView.ShowDeleteFolioButton(False)
            IndexerView.ShowNewFolderButton(False)
            IndexerView.ShowNewFileButton(False)
            IndexerView.ShowNextButton(True)
            IndexerView.ShowReprocesoButton(False)

            IndexerView.ShowInformationPanel(Me.IndexerImagingGlobal.ProyectoImagingRow.Show_Information)
            IndexerView.ShowDataPanel(False)
            IndexerView.ShowValidationsPanel(False)
            IndexerView.ShowValidationsListasPanel(False)
            IndexerView.ShowToolTipData(False)
            IndexerView.ShowAutoIndexar(False)

            IndexerView.Esquema_Enabled = True
            IndexerView.TipoDocumental_Enabled = True
        End Sub

        Public Overrides ReadOnly Property ShowSecondControls As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides Function AddFolio() As Boolean
            Throw New NotImplementedException()
        End Function

        Public Overrides Function DeleteFolio() As Boolean
            'CurrentDocumentFile.Remove(CurrentFolio)

            'If (_CurrentFolioIndex >= CurrentDocumentFile.Count) Then
            '    _CurrentFolioIndex -= 1
            'End If

            'SetCurrentFolio(CurrentFolio)

            '_InderxerView.ShowDeleteFolioButton(Folios > 1 And _Captura = EnumCaptura.Primera)
            Throw New NotImplementedException()
        End Function

        Public Overrides Function Save() As Boolean
            If (Validar()) Then
                Try
                    Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                    Dim fechaInicioProceso = Now

                    Try
                        dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                        ' Insertar datos
                        dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)
                        dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                        '--------------------------------------------------------------------------------------
                        ' REPORTAR DUPLICADOS
                        '--------------------------------------------------------------------------------------
                        Try
                            Dim fileBloqueadoDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                            If (fileBloqueadoDataTable(0).fk_Usuario_log <> Me.IndexerSesion.Usuario.id) Then
                                dbmImaging.SchemaAudit.PA_Inserta_Seguimiento_Imagenes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Reproceso, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.PCName)
                            End If

                        Catch : End Try
                        '--------------------------------------------------------------------------------------

                        dbmCore.Transaction_Begin(IsolationLevel.ReadUncommitted)
                        dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                        'Actualizar tipo documental
                        Dim updateFileCore As New DBCore.SchemaProcess.TBL_FileType()
                        updateFileCore.fk_Documento = CInt(Me.IndexerView.TipoDocumental_Value)
                        dbmCore.SchemaProcess.TBL_File.DBUpdate(updateFileCore, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                        'Actualizar el esquema del Expediente
                        'TODO: Falta la implementación para cuando hay mas de un File
                        If (Me.IndexerView.Esquema_Value <> ExpedienteRow.fk_Esquema) Then
                            Dim updateExpediente As New DBCore.SchemaProcess.TBL_ExpedienteType()
                            updateExpediente.fk_Esquema = Me.IndexerView.Esquema_Value
                            dbmCore.SchemaProcess.TBL_Expediente.DBUpdate(updateExpediente, FileCoreRow.fk_Expediente)

                            Dim anexosFileDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByfk_ExpedienteEs_Anexo(FileCoreRow.fk_Expediente, True)

                            If (anexosFileDataTable.Count > 0) Then
                                Dim anexoDataTable = DocumentoIndexacionDataTable.Select("fk_Esquema = " & Me.IndexerView.Esquema_Value & " AND Es_Anexo = 1")

                                If (anexoDataTable Is Nothing OrElse anexoDataTable.Length = 0) Then
                                    MessageBox.Show("El esquema: " & Me.IndexerView.Esquema_Value & " no tiene definido un documento como anexo", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

                                    Return False
                                End If

                                Dim updateFileCoreAnexo As New DBCore.SchemaProcess.TBL_FileType()
                                updateFileCoreAnexo.fk_Documento = CType(anexoDataTable(0), DBImaging.SchemaConfig.CTA_Documento_IndexacionRow).id_Documento
                                For Each AnexoRow In anexosFileDataTable
                                    dbmCore.SchemaProcess.TBL_File.DBUpdate(updateFileCoreAnexo, AnexoRow.fk_Expediente, AnexoRow.fk_Folder, AnexoRow.fk_File)
                                Next
                            End If
                        End If


                        'Actualiza el File de Imaging, para remover el reproceso
                        Dim fileImaging As New DBImaging.SchemaProcess.TBL_FileType
                        fileImaging.fk_Documento = updateFileCore.fk_Documento
                        fileImaging.Fecha_Reproceso = SlygNullable.SysDate
                        dbmImaging.SchemaProcess.TBL_File.DBUpdate(fileImaging, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, Nothing)

                        'Eliminar los datos de la mesa de control
                        dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, Nothing, Nothing, Nothing, Nothing, Nothing)
                        dbmImaging.SchemaProcess.TBL_File_Data_MC.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, Nothing, Nothing, Nothing)

                        'Elimar los datos de la captura
                        dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, Nothing, Nothing, Nothing, Nothing)
                        dbmCore.SchemaProcess.TBL_File_Data.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, Nothing, Nothing)

                        ' Actualizar estados
                        Dim nextEstado As DBCore.EstadoEnum = CType(dbmImaging.SchemaProcess.PA_Next_Estado.DBExecute(updateFileCore.fk_Documento, DBCore.EstadoEnum.Indexacion), DBCore.EstadoEnum)

                        ' Preparar los recortes
                        If (nextEstado = DBCore.EstadoEnum.Recorte) Then
                            If Not (Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Recortes AndAlso dbmImaging.SchemaProcess.PA_Preparar_Recortes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)) Then
                                nextEstado = DBCore.EstadoEnum.Indexado
                            End If
                        End If

                        ' Establece estado heredado del doccumento de captura campos llaves, cuando este no esta en el expediente.
                        If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                            If (nextEstado = DBCore.EstadoEnum.Indexado) Then
                                nextEstado = CType(dbmImaging.SchemaProcess.PA_Next_Estado_Documento_Captura_Campos_Llave.DBExecute(Me.IndexerImagingGlobal.Entidad, Me.IndexerImagingGlobal.Proyecto, ExpedienteRow.fk_Esquema, CType(FileCoreRow.fk_Expediente, Global.Slyg.Tools.SlygNullable(Of Integer)), FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Indexacion), DBCore.EstadoEnum)
                            End If
                        End If

                        Dim updateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                        updateEstado.Fecha_Log = SlygNullable.SysDate
                        updateEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                        updateEstado.fk_Estado = nextEstado
                        dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(updateEstado, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, DesktopConfig.Modulo.Imaging)

                        '---------------------------------------------------------------------------
                        ' Actualizar Dashboard
                        '---------------------------------------------------------------------------
                        If (nextEstado = DBCore.EstadoEnum.Indexado) Then
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                        Else
                            Dim capDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                            capDashboardType.fk_Documento = updateFileCore.fk_Documento
                            capDashboardType.fk_Usuario_log = DBNull.Value
                            capDashboardType.Sesion = DBNull.Value
                            capDashboardType.fk_Estado = nextEstado
                            capDashboardType.fk_Reproceso_Motivo = DBNull.Value
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(capDashboardType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                        End If

                        ' Actualizar validaciones opcionales
                        dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                        If (dbmImaging.SchemaConfig.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(updateFileCore.fk_Documento, DBImaging.EnumEtapaCaptura.Opcional, 1, New DBImaging.SchemaConfig.CTA_ValidacionEnumList(DBImaging.SchemaConfig.CTA_ValidacionEnum.id_Validacion, True)).Count > 0) Then
                            'Obtener el Cargue/Paquete
                            Dim cargueImgDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

                            'Obtener el detalle de Asignación del Cargue/Paquete
                            Dim cargueDatatable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(cargueImgDataTable(0).fk_Cargue)
                            Dim carguePaqueteDatatable = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBGet(cargueImgDataTable(0).fk_Cargue, cargueImgDataTable(0).fk_Cargue_Paquete)

                            Dim capDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_ValidacionesType()
                            capDashboardType.fk_Expediente = FileCoreRow.fk_Expediente
                            capDashboardType.fk_Folder = FileCoreRow.fk_Folder
                            capDashboardType.fk_File = FileCoreRow.id_File
                            capDashboardType.fk_Documento = updateFileCore.fk_Documento
                            capDashboardType.fk_Cargue = cargueImgDataTable(0).fk_Cargue
                            capDashboardType.fk_Cargue_Paquete = carguePaqueteDatatable(0).id_Cargue_Paquete
                            capDashboardType.fk_Entidad_Procesamiento = cargueDatatable(0).fk_Entidad_Procesamiento
                            capDashboardType.fk_Sede_Procesamiento = carguePaqueteDatatable(0).fk_Sede_Procesamiento_Asignada
                            capDashboardType.fk_Centro_Procesamiento = carguePaqueteDatatable(0).fk_Centro_Procesamiento_Asignado
                            capDashboardType.fk_Entidad = cargueDatatable(0).fk_Entidad
                            capDashboardType.fk_Proyecto = cargueDatatable(0).fk_Proyecto
                            capDashboardType.Procesado = False
                            capDashboardType.fk_OT = cargueDatatable(0).fk_OT

                            dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBInsert(capDashboardType)
                        End If
                        '---------------------------------------------------------------------------

                        dbmCore.Transaction_Commit()
                        dbmImaging.Transaction_Commit()

                        If Not (nextEstado = DBCore.EstadoEnum.Indexado) Then
                            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Transmision_Data_Maquina Then
                                Dim dbmSofTrac As DBSofTrac.DBSofTracDataBaseManager = Nothing
                                dbmSofTrac = New DBSofTrac.DBSofTracDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Softrac)
                                dbmSofTrac.Connection_Open(Me.IndexerSesion.Usuario.id)

                                Try
                                    dbmSofTrac.SchemaProcess.PA_Transmision_Data_Miharu_Expediente.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, Me.IndexerImagingGlobal.Entidad, Me.IndexerImagingGlobal.Proyecto, Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Correccion_Captura_Maquina)
                                Catch ex As Exception
                                    Throw New Exception("Error transmitiendo data SofTrac: " + ex.Message)
                                Finally
                                    If (dbmSofTrac IsNot Nothing) Then dbmSofTrac.Connection_Close()
                                End Try
                            End If
                        End If

                        ' Actualizar el estado de los cargues que ya fueron procesados
                        Try
                            dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                            Dim carguePaqueteFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_File.DBFindByid_Cargueid_Cargue_Paquete(_idCargue, _idCarguePaquete)
                            If (carguePaqueteFileDataTable.Count > 0) Then
                                Dim updatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                                updatePaquete.fk_Estado = carguePaqueteFileDataTable(0).Estado_File
                                dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(updatePaquete, _idCargue, _idCarguePaquete)

                                Dim cargueFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Estado.DBFindByid_Cargue(_idCargue)
                                If (cargueFileDataTable.Count > 0) Then
                                    Dim updateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                                    updateCargue.fk_Estado = cargueFileDataTable(0).Estado_File
                                    dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(updateCargue, _idCargue)
                                End If
                            End If

                            dbmImaging.Transaction_Commit()
                        Catch ex As Exception
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                            Throw
                        End Try

                    Catch ex As Exception
                        If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                        DesktopMessageBoxControl.DesktopMessageShow("Publicar", ex)

                        Return False
                    Finally
                        If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    End Try

                    EventManager.FinalizarReclasificar(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)

                    '-------------------------------------------------------------------------------------------------------------
                    ' LOGGING - PERFORMANCE
                    '-------------------------------------------------------------------------------------------------------------
                    Dim fechaFinProceso = Now
                    Dim traceMessage As String = ""
                    traceMessage &= "Duración:" & vbTab & (fechaInicioProceso - fechaFinProceso).TotalMilliseconds & vbTab
                    traceMessage &= "Expediente:" & vbTab & FileCoreRow.fk_Expediente & vbTab
                    traceMessage &= "Folder:" & vbTab & FileCoreRow.fk_Folder & vbTab
                    traceMessage &= "File:" & vbTab & FileCoreRow.id_File & vbTab
                    DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[Reprocesos][Publicar]")
                    '-------------------------------------------------------------------------------------------------------------

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Publicar", ex)

                    Return False
                End Try

                Return True
            End If

            Return False
        End Function

        Public Overrides Function ValidacionListas() As Boolean
            Dim _ValidacionListas As Boolean

            _ValidacionListas = False

            Return _ValidacionListas
        End Function

        Public Overrides Function Campos(ByVal idDocumento As Integer) As List(Of View.Indexacion.CampoCaptura)
            Me._Campos.Clear()

            Return _Campos
        End Function

        Public Overrides Function CamposLLave(ByVal idDocumento As Integer) As List(Of View.Indexacion.CampoLlaveCaptura)
            Me._CamposLlave.Clear()

            Return _CamposLlave
        End Function

        Public Overrides Function GetValueFileData(idCampo As Integer) As String
            Return Nothing
        End Function

        Public Overrides Function GetValueLlaveData() As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaRow
            Return Nothing
        End Function

        Public Overrides Function Validaciones(ByVal idDocumento As Integer) As List(Of View.Indexacion.ValidacionCaptura)
            Me._Validaciones.Clear()
            Return _Validaciones
        End Function

        Public Overrides Function NextIndexingElement(ByVal ot As Integer, nEstado As DBCore.EstadoEnum, nIdDocumento As SlygNullable(Of Integer)) As Boolean
            Dim manager As FileProviderManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Dim fechaInicioProceso = Now

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)
                dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)

                ' Mirar si existe un registro previamente asignado
                Dim bloqueoDatatable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBFindBySesionfk_OT(Me.IndexerDesktopGlobal.SesionID, ot)
                If bloqueoDatatable.Count > 0 Then
                    For Each BloqueoRow In bloqueoDatatable
                        dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas.DBExecute(BloqueoRow.fk_Expediente, BloqueoRow.fk_Folder, BloqueoRow.fk_File)
                    Next
                End If

                Dim fileCoreDataTable = dbmCore.SchemaProcess.TBL_File.DBGet(Me._idExpediente, Me._idFolder, Me._idFile)
                If (fileCoreDataTable.Count = 0) Then
                    Throw New Exception("No se encontró data asociada al Ex: " & bloqueoDatatable(0).fk_Expediente & " Fo: " & bloqueoDatatable(0).fk_Folder & " Fi: " & bloqueoDatatable(0).fk_File & " del Dashboard")
                End If
                FileCoreRow = fileCoreDataTable(0)

                bloqueoDatatable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                If (bloqueoDatatable.Count > 0 AndAlso Not bloqueoDatatable(0).IsSesionNull AndAlso bloqueoDatatable(0).Sesion <> Me.IndexerDesktopGlobal.SesionID) Then
                    MessageBox.Show("El registro se encuentra bloqueado por el usuario del equipo: " & bloqueoDatatable(0).PCName, "Registro bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If

                dbmCore.Transaction_Begin()
                dbmImaging.Transaction_Begin()

                ' Bloquear el item
                Dim bloqueoType As New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                bloqueoType.fk_Usuario_log = Me.IndexerSesion.Usuario.id
                bloqueoType.Sesion = Me.IndexerDesktopGlobal.SesionID
                bloqueoType.PCName = Me.IndexerDesktopGlobal.PCName

                dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(bloqueoType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                Dim fileImagingDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, Nothing)
                FileImagingRow = fileImagingDataTable(fileImagingDataTable.Count - 1)

                Dim fileType As New DBCore.SchemaImaging.TBL_FileType()
                fileType.fk_Usuario_Log = Me.IndexerSesion.Usuario.id
                dbmCore.SchemaImaging.TBL_File.DBUpdate(fileType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)

                Dim expedienteDataTable = dbmCore.SchemaProcess.TBL_Expediente.DBGet(FileCoreRow.fk_Expediente)
                ExpedienteRow = expedienteDataTable(0)

                Dim folderImagingDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder)

                _idCargue = folderImagingDataTable(0).fk_Cargue
                _idCarguePaquete = folderImagingDataTable(0).fk_Cargue_Paquete

                ' Leer el listado de imagenes
                manager = New FileProviderManager(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, dbmImaging, Me.IndexerSesion.Usuario.id)
                manager.Connect()

                If FileImagingRow.Es_Anexo Then
                    _ImageCount = manager.GetFolios(FileImagingRow.fk_Anexo)
                Else
                    _ImageCount = manager.GetFolios(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)
                End If


                View.SetTitle("Reproceso [Ex: " & FileCoreRow.fk_Expediente & " - Fo: " & FileCoreRow.fk_Folder & " - Fi: " & FileCoreRow.id_File & " - Ver: " & FileImagingRow.id_Version & "]")

                IndexerView.Information = ""
                Dim llavesDataTable = dbmCore.SchemaProcess.CTA_Expediente_LLave.DBFindByid_Expediente(FileCoreRow.fk_Expediente)
                For Each Llave In llavesDataTable
                    IndexerView.Information &= "[" & Llave.Nombre_Proyecto_Llave & "]: " & Llave.Valor_Llave.ToString() & vbCrLf
                Next

                ' Anexos del documento
                AnexosDataTable = dbmCore.SchemaImaging.CTA_Documentos.DBFindByfk_Expedientefk_Folder(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

                LoadConfig(dbmCore, dbmImaging, ExpedienteRow.fk_Entidad, ExpedienteRow.fk_Proyecto, ExpedienteRow.fk_Esquema)

                ' Definir si se puede cambiar el esquema
                Dim totalFiles = dbmCore.SchemaImaging.PA_Get_Files_No_Anexo.DBExecute(ExpedienteRow.id_Expediente)

                Me.IndexerView.Esquema_Enabled = (totalFiles = 1)


                dbmImaging.Transaction_Commit()
                dbmCore.Transaction_Commit()

            Catch ex As Exception
                If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                Throw

            Finally
                If (manager IsNot Nothing) Then manager.Disconnect()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            ' Liberar la memoria
            For Each Item In _Folders
                Item.Dispose()
            Next

            ' Liberar la memoria
            Me.Clear()


            Application.DoEvents()

            _CurrentFolderIndex = 0
            _CurrentDocumentFileIndex = 0
            _CurrentFolioIndex = 0
            _CurrentImageIndex = 0

            ' Crear data inicial
            Dim newFolder = New Generic.Folder(Me)
            Folders.Add(newFolder)

            Dim newDocumento = newFolder.NewDocumentFile()
            newFolder.Add(newDocumento)

            For i = 0 To _ImageCount - 1
                Dim newFolio = newDocumento.NewFolio(True)
                newDocumento.Add(newFolio)
            Next

            InicializarFolio(_CurrentFolioIndex)

            View.ThumbnailPanel.Controls.Add(newFolder.Panel)

            Try
                Dim hilo As New Thread(AddressOf InicializarFolios)
                hilo.Start()
            Catch ext As ThreadStateException
                MessageBox.Show("Error: " + ext.Message, "Error en Thread", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("NextIndexingElement", ex)
            End Try

            '-------------------------------------------------------------------------------------------------------------
            ' LOGGING - PERFORMANCE
            '-------------------------------------------------------------------------------------------------------------
            Dim fechaFinProceso = Now
            Dim traceMessage As String = ""
            traceMessage &= "Duración:" & vbTab & (fechaInicioProceso - fechaFinProceso).TotalMilliseconds & vbTab
            traceMessage &= "Expediente:" & vbTab & FileCoreRow.fk_Expediente & vbTab
            traceMessage &= "Folder:" & vbTab & FileCoreRow.fk_Folder & vbTab
            traceMessage &= "File:" & vbTab & FileCoreRow.id_File & vbTab
            DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[Reproceso][NextIndexingElement]")
            '-------------------------------------------------------------------------------------------------------------

            Return True
        End Function

        Public Overrides Function Indexar() As DialogResult
            View.Unlock = True

            '_InderxerView.Esquema_Enabled = True
            IndexerView.Esquema_Refresh()
            IndexerView.Esquema_Index = CInt(IIf(IndexerView.Esquema_DataSource.Count = 1, 0, -1))
            IndexerView.Esquema_Value = ExpedienteRow.fk_Esquema

            IndexerView.TipoDocumental_Enabled = True
            IndexerView.TipoDocumental_Refresh()
            IndexerView.TipoDocumental_Index = CInt(IIf(IndexerView.TipoDocumental_DataSource.Count = 1, 0, -1))
            IndexerView.TipoDocumental_Value = FileCoreRow.fk_Documento

            CurrentFolder.Esquema = IndexerView.Esquema_Value
            CurrentDocumentFile.TipoDocumento = IndexerView.TipoDocumental_Value.Value
            CurrentDocumentFile.NombreTipoDocumento = IndexerView.TipoDocumental_Text

            _Ciclo += 1

            View.ActivarControles(True)
            Return View.ShowDialog()
        End Function

        Public Overrides Function Reproceso(ByVal nMotivo As Short) As Boolean
            Throw New NotImplementedException()
        End Function

#End Region

#Region " Metodos "

        Protected Overrides Sub LoadConfig(ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short)
            EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, Nothing)
            'DocumentoDataTable = dbmImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(nEntidad, nProyecto, Nothing, False)
            DocumentoIndexacionDataTable = dbmImaging.SchemaConfig.CTA_Documento_Indexacion.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(nEntidad, nProyecto, Nothing, False)
            IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
            IndexerView.Esquema_Value = ExpedienteRow.fk_Esquema

            'IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)
            IndexerView.TipoDocumental_DataSource = New DataView(DocumentoIndexacionDataTable)
        End Sub

        Public Sub SetData(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short)
            Me._idExpediente = nExpediente
            Me._idFolder = nFolder
            Me._idFile = nFile
        End Sub

#End Region

    End Class

End Namespace