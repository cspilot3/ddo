Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Miharu.FileProvider.Library
Imports Miharu.Imaging.Indexer.View.Recorte
Imports System.Threading
Imports Slyg.Tools

Namespace Controller.Recorte

    Public Class CalidadRecorteController
        Inherits RecorteController
        
#Region " Propiedades "

        Protected Overrides ReadOnly Property ProcessName As String
            Get
                Return "Recorte"
            End Get
        End Property

#End Region

#Region " Implementacion IController "

        Public Overrides Sub Inicializar(nTempPath As String, nIndexerSesion As Security.Library.Session.Sesion, nIndexerDesktopGlobal As Desktop.Library.Config.DesktopGlobal, nIndexerImagingGlobal As Desktop.Library.Config.ImagingGlobal)
            Me.IndexerSesion = nIndexerSesion
            Me.IndexerDesktopGlobal = nIndexerDesktopGlobal
            Me.IndexerImagingGlobal = nIndexerImagingGlobal

            DBImaging.DBImagingDataBaseManager.IdentifierDateFormat = nIndexerDesktopGlobal.IdentifierDateFormat
            DBCore.DBCoreDataBaseManager.IdentifierDateFormat = nIndexerDesktopGlobal.IdentifierDateFormat
            DBSecurity.DBSecurityDataBaseManager.IdentifierDateFormat = nIndexerDesktopGlobal.IdentifierDateFormat

            Me._IndexerView = New FormCalidadRecorte()
            Me.View.SetController(Me)

            _TempPath = nTempPath.TrimEnd("\"c) & "\"
            If (Not Directory.Exists(nTempPath)) Then
                Directory.CreateDirectory(nTempPath)
            End If

            BorrarTemporal()
        End Sub

        Public Overrides Function Save() As Boolean
            If (Validar()) Then
                Try
                    Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                    Dim fechaInicioProceso = Now

                    Dim recortes = CType(Me.View, FormCalidadRecorte).Recortes

                    Dim productividad As New DesktopConfig.ProductividadType
                    productividad.Usuario = Me.IndexerSesion.Usuario.id
                    productividad.Campos = recortes.Count
                    productividad.Validaciones = 0

                    Try
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                        dbmCore = New DBCore.DBCoreDataBaseManager(dbmImaging, Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                        dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                        '--------------------------------------------------------------------------------------
                        ' REPORTAR DUPLICADOS
                        '--------------------------------------------------------------------------------------
                        Try
                            Dim fileBloqueadoDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)

                            If (fileBloqueadoDataTable(0).fk_Usuario_log <> Me.IndexerSesion.Usuario.id) Then
                                dbmImaging.SchemaAudit.PA_Inserta_Seguimiento_Imagenes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Recorte, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.PCName)
                            End If

                        Catch : End Try
                        '--------------------------------------------------------------------------------------

                        'dbmCore.LinkDataBaseManager(dbmImaging)
                        'dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)

                        dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)
                        'dbmCore.Transaction_Begin()

                        Dim recorteType As New DBCore.SchemaImaging.TBL_File_RecorteType()
                        Dim enviarReproceso As Boolean = False

                        ' Validar los recortes
                        For Each item As CalidadDataControl In recortes
                            If (item.Option = CalidadDataControl.EnumOption.OK) Then
                                recorteType.Bloqueado = True
                                recorteType.fk_Usuario_Log_Aprobacion = IndexerSesion.Usuario.id
                                recorteType.Fecha_Log_Aprobacion = SlygNullable.SysDate
                            Else
                                recorteType.Bloqueado = False
                                enviarReproceso = True
                                recorteType.fk_Usuario_Log_Aprobacion = IndexerSesion.Usuario.id
                                recorteType.Fecha_Log_Aprobacion = SlygNullable.SysDate
                            End If

                            dbmCore.SchemaImaging.TBL_File_Recorte.DBUpdate(recorteType, item.Data.fk_Expediente, item.Data.fk_Folder, item.Data.fk_File, item.Data.fk_Version, item.Data.id_File_Recorte)
                        Next

                        ' Insertar productividad
                        InsertarProductividad(dbmImaging, productividad, DesktopConfig.Etapa_Productividad.PreCaptura, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                        ' Actualizar estados
                        Dim nextEstado As DBCore.EstadoEnum
                        If (enviarReproceso) Then
                            nextEstado = DBCore.EstadoEnum.Calidad_Recorte
                        Else
                            nextEstado = DBCore.EstadoEnum.Indexado
                        End If

                        Dim updateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                        updateEstado.Fecha_Log = SlygNullable.SysDate
                        updateEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                        updateEstado.fk_Estado = nextEstado
                        dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(updateEstado, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, DesktopConfig.Modulo.Imaging)

                        '---------------------------------------------------------------------------
                        ' Actualizar Dashboard
                        '---------------------------------------------------------------------------
                        If (enviarReproceso) Then
                            'Obtener el Cargue/Paquete
                            Dim cargueImgDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

                            'Obtener el detalle de Asignación del Cargue/Paquete
                            Dim cargueDatatable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(cargueImgDataTable(0).fk_Cargue)
                            Dim carguePaqueteDatatable = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBGet(cargueImgDataTable(0).fk_Cargue, cargueImgDataTable(0).fk_Cargue_Paquete)

                            ' Actualizar/Insertar registro para capturas
                            Dim dashboardCapturasDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                            If (dashboardCapturasDataTable.Count = 0) Then
                                Dim capDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                                capDashboardType.fk_Expediente = FileImagingRow.fk_Expediente
                                capDashboardType.fk_Folder = FileImagingRow.fk_Folder
                                capDashboardType.fk_File = FileImagingRow.fk_File
                                capDashboardType.fk_Documento = FileCoreRow.fk_Documento
                                capDashboardType.fk_Cargue = carguePaqueteDatatable(0).fk_Cargue
                                capDashboardType.fk_Cargue_Paquete = carguePaqueteDatatable(0).id_Cargue_Paquete
                                capDashboardType.fk_Entidad_Procesamiento = cargueDatatable(0).fk_Entidad_Procesamiento
                                capDashboardType.fk_Sede_Procesamiento = carguePaqueteDatatable(0).fk_Sede_Procesamiento_Asignada
                                capDashboardType.fk_Centro_Procesamiento = carguePaqueteDatatable(0).fk_Centro_Procesamiento_Asignado
                                capDashboardType.fk_Entidad = cargueDatatable(0).fk_Entidad
                                capDashboardType.fk_Proyecto = cargueDatatable(0).fk_Proyecto
                                capDashboardType.fk_Estado = DBCore.EstadoEnum.Recorte
                                capDashboardType.fk_OT = cargueDatatable(0).fk_OT
                                dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBInsert(capDashboardType)
                            Else
                                Dim capDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                                capDashboardType.fk_Estado = DBCore.EstadoEnum.Recorte
                                dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(capDashboardType, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                            End If
                        Else
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                        End If

                        '---------------------------------------------------------------------------

                        dbmImaging.Transaction_Commit()

                        ' Actualizar el estado de los cargues que ya fueron procesados
                        Try
                            dbmImaging.Transaction_Begin(IsolationLevel.ReadCommitted)

                            Dim carguePaqueteFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_File.DBFindByid_Cargueid_Cargue_Paquete(IdCargue, IdCarguePaquete)
                            If (carguePaqueteFileDataTable.Count > 0) Then
                                Dim updatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                                updatePaquete.fk_Estado = carguePaqueteFileDataTable(0).Estado_File
                                dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(updatePaquete, IdCargue, IdCarguePaquete)

                                Dim cargueFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Estado.DBFindByid_Cargue(IdCargue)
                                If (cargueFileDataTable.Count > 0) Then
                                    Dim updateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                                    updateCargue.fk_Estado = cargueFileDataTable(0).Estado_File
                                    dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(updateCargue, IdCargue)
                                End If
                            End If

                            dbmImaging.Transaction_Commit()
                        Catch ex As Exception
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                            Throw
                        End Try

                    Catch ex As Exception
                        'If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                        DesktopMessageBoxControl.DesktopMessageShow("Publicar", ex)

                        Return False
                    Finally
                        'If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    End Try

                    EventManager.FinalizarRecorte(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)

                    '-------------------------------------------------------------------------------------------------------------
                    ' LOGGING - PERFORMANCE
                    '-------------------------------------------------------------------------------------------------------------
                    Dim fechaFinProceso = Now
                    Dim traceMessage As String = ""
                    traceMessage &= "Duración:" & vbTab & (fechaInicioProceso - fechaFinProceso).TotalMilliseconds & vbTab
                    traceMessage &= "Expediente:" & vbTab & FileCoreRow.fk_Expediente & vbTab
                    traceMessage &= "Folder:" & vbTab & FileCoreRow.fk_Folder & vbTab
                    traceMessage &= "File:" & vbTab & FileCoreRow.id_File & vbTab
                    DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[Recorte][Publicar]")
                    '-------------------------------------------------------------------------------------------------------------

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Publicar", ex)

                    Return False
                End Try

                Return True
            End If

            Return False
        End Function

        Public Overrides Function NextFolio(ByRef nUpdateInfo As Boolean) As Boolean
            If (CurrentRecorteIndex + 1 < Me.RecorteDataTable.Count) Then
                CurrentRecorteIndex += 1
                _CurrentFolioIndex = Me.RecorteDataTable(CurrentRecorteIndex).Folio - 1
                _CurrentImageIndex = _CurrentFolioIndex

                Dim intentos As Integer = 0

                If (CurrentRecorteIndex > 0) Then
                    While Not Cargado Or Me.CurrentFolio.ThumbnailImage Is Nothing
                        Thread.Sleep(1000)

                        intentos += 1

                        If intentos > 180 Then Throw New Exception("Ha expirado el tiempo para el cargue del los folios restantes del Documento")
                    End While
                End If

                Return True
            Else
                Return False
            End If
        End Function

        Public Overrides Function PreviousFolio(ByRef nUpdateInfo As Boolean) As Boolean
            If (CurrentRecorteIndex - 1 > 0) Then
                CurrentRecorteIndex -= 1
                _CurrentFolioIndex = Me.RecorteDataTable(CurrentRecorteIndex).Folio - 1
                _CurrentImageIndex = _CurrentFolioIndex

                Dim intentos As Integer = 0

                If (CurrentRecorteIndex > 0) Then
                    While Not Cargado Or Me.CurrentFolio.ThumbnailImage Is Nothing
                        Thread.Sleep(1000)

                        intentos += 1

                        If intentos > 180 Then Throw New Exception("Ha expirado el tiempo para el cargue del los folios restantes del Documento")
                    End While
                End If

                Return True
            Else
                Return False
            End If
        End Function

        Public Overrides Function NextIndexingElement(ot As Integer, nEstado As DBCore.EstadoEnum, nIdDocumento As SlygNullable(Of Integer)) As Boolean
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
                Dim bloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Capturas_get.DBExecute(Me.IndexerDesktopGlobal.SesionID)
                If bloqueoDatatable.Count > 0 Then
                    For Each BloqueoRow In bloqueoDatatable
                        dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas.DBExecute(BloqueoRow.fk_Expediente, BloqueoRow.fk_Folder, BloqueoRow.fk_File)
                    Next
                End If

                ' Calcular siguiente imagen disponible
                Dim bloqueados As Boolean = dbmImaging.SchemaProcess.PA_Bloqueo_File_Next.DBExecute(ot, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, nEstado, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.SesionID, Me.IndexerDesktopGlobal.PCName)
                If (Not bloqueados) Then Return False

                bloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Capturas_get.DBExecute(Me.IndexerDesktopGlobal.SesionID)
                If bloqueoDatatable.Count > 0 Then
                    Dim fileImagingDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(bloqueoDatatable(0).fk_Expediente, bloqueoDatatable(0).fk_Folder, bloqueoDatatable(0).fk_File, Nothing)

                    If (fileImagingDataTable.Count = 0) Then
                        Throw New Exception("No se encontró data asociada al Ex: " & bloqueoDatatable(0).fk_Expediente & " Fo: " & bloqueoDatatable(0).fk_Folder & " Fi: " & bloqueoDatatable(0).fk_File & " del Dashboard")
                    End If
                    FileImagingRow = fileImagingDataTable(fileImagingDataTable.Count - 1)

                    Dim fileCoreDataTable = dbmCore.SchemaProcess.TBL_File.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                    FileCoreRow = fileCoreDataTable(0)

                    Dim expedienteDataTable = dbmCore.SchemaProcess.TBL_Expediente.DBGet(FileImagingRow.fk_Expediente)
                    ExpedienteRow = expedienteDataTable(0)

                    Dim folderImagingDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

                    IdCargue = folderImagingDataTable(0).fk_Cargue
                    IdCarguePaquete = folderImagingDataTable(0).fk_Cargue_Paquete

                    ' Leer el listado de imagenes
                    manager = New FileProviderManager(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, dbmImaging, Me.IndexerSesion.Usuario.id)
                    manager.Connect()

                    If FileImagingRow.Es_Anexo Then
                        _ImageCount = manager.GetFolios(FileImagingRow.fk_Anexo)
                    Else
                        _ImageCount = manager.GetFolios(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)
                    End If

                    View.SetTitle(Me.ProcessName & " [Ex: " & FileCoreRow.fk_Expediente & " - Fo: " & FileCoreRow.fk_Folder & " - Fi: " & FileCoreRow.id_File & "]")

                    RecorteDataTable = dbmCore.SchemaImaging.TBL_File_Recorte.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, Nothing)

                    LoadConfig(dbmCore, dbmImaging, ExpedienteRow.fk_Entidad, ExpedienteRow.fk_Proyecto, ExpedienteRow.fk_Esquema)
                Else
                    Return False
                End If
            Catch
                Throw
            Finally
                If (manager IsNot Nothing) Then manager.Disconnect()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            ' Liberar la memoria
            Me.Clear()

            Application.DoEvents()

            CurrentRecorteIndex = 0
            _CurrentFolioIndex = 0

            If (_ImageCount = 0) Then
                Dim mensaje = "No se encontraron folios asociados a al File, por favor comuniquese con el administrador del sistema." & vbCrLf
                mensaje &= "[Expediente]: " & FileCoreRow.fk_Expediente & vbCrLf
                mensaje &= "[Folder]: " & FileCoreRow.fk_Folder & vbCrLf
                mensaje &= "[File]: " & FileCoreRow.id_File & vbCrLf

                Throw New Exception(mensaje)
            End If

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
            DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[" & Me.ProcessName & "][NextIndexingElement]")
            '-------------------------------------------------------------------------------------------------------------

            Return True
        End Function

        Public Overrides Function Reproceso(ByVal nMotivo As Short) As Boolean
            Return False
        End Function

        Public Overrides Function MotivosReproceso() As List(Of Item)
            Return New List(Of Item)
        End Function

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Dim recortes = CType(Me.View, FormCalidadRecorte).Recortes

            For Each item As CalidadDataControl In recortes
                If (item.Option = CalidadDataControl.EnumOption.Undefined) Then
                    MessageBox.Show("No se han validado todos los recortes", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If
            Next

            Return True
        End Function

#End Region

    End Class

End Namespace