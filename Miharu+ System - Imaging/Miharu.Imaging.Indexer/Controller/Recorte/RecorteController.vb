Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Miharu.FileProvider.Library
Imports Miharu.Imaging.Indexer.View.Recorte
Imports Slyg.Tools.Imaging
Imports System.Threading
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Slyg.Tools

Namespace Controller.Recorte

    Public Class RecorteController
        Inherits GenericController
        Implements IRecorteController

#Region " Declaraciones "

        Private _cargado As Boolean

        Protected ExpedienteRow As DBCore.SchemaProcess.TBL_ExpedienteRow
        Protected FileImagingRow As DBCore.SchemaImaging.TBL_FileRow
        Protected FileCoreRow As DBCore.SchemaProcess.TBL_FileRow

        Protected IdCargue As Integer
        Protected IdCarguePaquete As SlygNullable(Of Short)

        Protected RecorteDataTable As DBCore.SchemaImaging.TBL_File_RecorteDataTable

#End Region

#Region " Propiedades "

        Protected Overridable ReadOnly Property ProcessName As String
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

            Me._IndexerView = New FormRecorte()
            Me.View.SetController(Me)

            _TempPath = nTempPath.TrimEnd("\"c) & "\"
            If (Not Directory.Exists(nTempPath)) Then
                Directory.CreateDirectory(nTempPath)
            End If

            BorrarTemporal()
        End Sub

        Public Overrides ReadOnly Property Cargado As Boolean
            Get
                Return _Cargado
            End Get
        End Property

        Public Overrides Function Save() As Boolean
            If (Validar()) Then
                Try
                    Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                    Dim fechaInicioProceso = Now

                    Dim recortes = CType(Me.View, FormRecorte).Recortes

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

                        ' Actualizar la data de los recortes
                        Dim recorteType As New DBCore.SchemaImaging.TBL_File_RecorteType()
                        For Each item In recortes
                            recorteType.X = item.Selector.Posicion.X
                            recorteType.Y = item.Selector.Posicion.Y
                            recorteType.Alto = item.Selector.Tamaño.Height
                            recorteType.Ancho = item.Selector.Tamaño.Width
                            recorteType.Folio = item.Selector.Folio
                            recorteType.Angulo = item.Selector.Angulo
                            recorteType.Procesado = True
                            recorteType.fk_Usuario_Log = Me.IndexerSesion.Usuario.id
                            recorteType.Fecha_Log = SlygNullable.SysDate

                            dbmCore.SchemaImaging.TBL_File_Recorte.DBUpdate(recorteType, item.Data.fk_Expediente, item.Data.fk_Folder, item.Data.fk_File, item.Data.fk_Version, item.Data.id_File_Recorte)
                        Next

                        ' Insertar productividad
                        InsertarProductividad(dbmImaging, productividad, DesktopConfig.Etapa_Productividad.Recortes, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                        ' Actualizar estados
                        Dim nextEstado As DBCore.EstadoEnum

                        'If (Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Calidad) Then
                        nextEstado = DBCore.EstadoEnum.Calidad_Recorte
                        'Else
                        'NextEstado = DBCore.EstadoEnum.Indexado
                        'End If

                        Dim updateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                        updateEstado.Fecha_Log = SlygNullable.SysDate
                        updateEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                        updateEstado.fk_Estado = nextEstado
                        dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(updateEstado, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, DesktopConfig.Modulo.Imaging)

                        '---------------------------------------------------------------------------
                        ' Actualizar Dashboard
                        '---------------------------------------------------------------------------

                        If (nextEstado = DBCore.EstadoEnum.Indexado) Then
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                        Else
                            Dim capDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                            capDashboardType.fk_Usuario_log = DBNull.Value
                            capDashboardType.Sesion = DBNull.Value
                            capDashboardType.fk_Estado = nextEstado
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(capDashboardType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
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
            nUpdateInfo = False

            Dim intentos As Integer = 0

            If _CurrentFolioIndex + 1 < Me.Folios Then
                While Not _Cargado OrElse CurrentDocumentFile.Folio(_CurrentFolioIndex + 1).ThumbnailImage Is Nothing
                    Thread.Sleep(1000)

                    intentos += 1

                    If intentos > 180 Then Throw New Exception("Ha expirado el tiempo para el cargue del los folios restantes del Documento")
                End While

                _CurrentImageIndex += 1
                _CurrentFolioIndex += 1

                Me.View.ScrollThumbnail()

                Return True
            Else
                Return False
            End If
        End Function

        Public Overrides Function PreviousFolio(ByRef nUpdateInfo As Boolean) As Boolean
            nUpdateInfo = False

            If (_CurrentImageIndex >= 0) Then
                _CurrentImageIndex -= 1
                _CurrentFolioIndex -= 1

                Return True
            Else
                Return False
            End If
        End Function

        Public Overrides Sub Unlock()
            If (Not FileCoreRow Is Nothing) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)
                    dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                Catch
                    Throw
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If
        End Sub

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

                    'Me.View.Information = ""
                    'Dim LlavesDataTable = dbmCore.SchemaProcess.CTA_Expediente_LLave.DBFindByid_Expediente(FileCoreRow.fk_Expediente)
                    'For Each Llave In LlavesDataTable
                    '    Me.View.Information &= "[" & Llave.Nombre_Proyecto_Llave & "]: " & Llave.Valor_Llave.ToString() & vbCrLf
                    'Next

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

            Me.View.ThumbnailPanel.Controls.Add(newFolder.Panel)

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

        Public Overrides Function Indexar() As DialogResult
            View.Unlock = True

            _Ciclo += 1

            View.ActivarControles(True)

            Return View.ShowDialog()
        End Function

        Public Overrides Function SetCurrentFolio(ByVal value As Generic.Folio, ByRef nUpdateInfo As Boolean) As Boolean
            nUpdateInfo = False

            Dim intentos As Integer = 0

            If (value.GlobalIndex > 0) Then
                While Not _Cargado Or value.ThumbnailImage Is Nothing
                    Thread.Sleep(1000)

                    intentos += 1

                    If intentos > 180 Then Throw New Exception("Ha expirado el tiempo para el cargue del los folios restantes del Documento")
                End While
            End If

            If (CurrentFolio IsNot Nothing) Then
                CurrentFolio.Unselect()
            End If

            _CurrentFolderIndex = value.Parent.Parent.Index
            _CurrentDocumentFileIndex = value.Parent.Index
            _CurrentFolioIndex = value.Index
            _CurrentImageIndex = value.GlobalIndex

            CurrentFolio.Select()

            Return True
        End Function

        Public Overrides Function Reproceso(ByVal nMotivo As Short) As Boolean
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                ' Notificar envio reproceso
                EventManager.EnviarReproceso(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)
                dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)

                dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)
                dbmCore.Transaction_Begin(IsolationLevel.ReadUncommitted)

                ' Actualizar File
                Dim fileType As New DBImaging.SchemaProcess.TBL_FileType()
                fileType.Fecha_Reproceso = DBNull.Value
                dbmImaging.SchemaProcess.TBL_File.DBUpdate(fileType, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)

                ' Actualizar estado
                Dim updateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                updateEstado.Fecha_Log = SlygNullable.SysDate
                updateEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                updateEstado.fk_Estado = DBCore.EstadoEnum.Reproceso
                dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(updateEstado, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, DesktopConfig.Modulo.Imaging)

                'Elimina la data capturada del documento

                'Core
                dbmCore.SchemaProcess.TBL_File_Data.DBDelete(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, Nothing, Nothing)
                dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBDelete(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, Nothing, Nothing, Nothing, Nothing)
                dbmCore.SchemaImaging.TBL_File_Recorte.DBDelete(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, Nothing, Nothing)

                'Imaging
                dbmImaging.SchemaProcess.TBL_File_Data_MC.DBDelete(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, Nothing, Nothing)
                dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBDelete(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, Nothing, Nothing, Nothing, Nothing)

                'Elimina las validaciones del documento
                dbmCore.SchemaProcess.TBL_File_Validacion.DBDelete(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, Nothing, FileCoreRow.fk_Documento)

                'Solicita nuevamente validaciones opcionales
                Dim imagingFileType As New DBCore.SchemaImaging.TBL_FileType
                imagingFileType.Validaciones_Opcionales = False
                dbmCore.SchemaImaging.TBL_File.DBUpdate(imagingFileType, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, Nothing)

                '---------------------------------------------------------------------------
                ' Actualizar Dashboard
                '---------------------------------------------------------------------------
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
                    capDashboardType.fk_Estado = DBCore.EstadoEnum.Reproceso
                    capDashboardType.fk_Reproceso_Motivo = nMotivo
                    capDashboardType.fk_OT = cargueDatatable(0).fk_OT
                    dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBInsert(capDashboardType)
                Else
                    Dim capDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                    capDashboardType.fk_Estado = DBCore.EstadoEnum.Reproceso
                    capDashboardType.fk_Reproceso_Motivo = nMotivo
                    dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(capDashboardType, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                End If

                ' Actualizar validaciones opcionales validaciones opcionales
                dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBDelete(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                '---------------------------------------------------------------------------

                dbmImaging.Transaction_Commit()
                dbmCore.Transaction_Commit()

                Return True
            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()

                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            Return False
        End Function

        Public Overrides Function MotivosReproceso() As List(Of Item)
            Dim motivos As New List(Of Item)

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(1)

                Dim motivoReprocesoDataTable = dbmImaging.SchemaProcess.TBL_Reproceso_Motivo.DBGet(Nothing)
                Dim motivoReprocesoDataView As New DataView(motivoReprocesoDataTable)
                motivoReprocesoDataView.Sort = motivoReprocesoDataTable.Id_Reproceso_MotivoColumn.ColumnName

                For Each motivoItem As DataRowView In motivoReprocesoDataView
                    Dim motivoRow = CType(motivoItem.Row, DBImaging.SchemaProcess.TBL_Reproceso_MotivoRow)
                    motivos.Add(New Item(motivoRow.Id_Reproceso_Motivo, motivoRow.Nombre_Reproceso_Motivo))
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return motivos
        End Function

#End Region

#Region " Implementacion IRecorteController "

        Public Property CurrentRecorteIndex As Integer Implements IRecorteController.CurrentRecorteIndex

        Public Function GetRecortes() As DBCore.SchemaImaging.TBL_File_RecorteDataTable Implements IRecorteController.GetRecortes
            Return RecorteDataTable
        End Function

#End Region

#Region " Metodos "

        Protected Sub LoadConfig(ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short)

        End Sub

        Protected Sub InicializarFolios()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            If (Not View.ViewClosing) Then
                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                    dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)
                    dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                    Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
                    manager = New FileProviderManager(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, centro, dbmImaging, Me.IndexerSesion.Usuario.id)
                    manager.Connect()

                    For i As Integer = 1 To Folders(0).DocumentFile(0).Count - 1
                        If (View.ViewClosing) Then Return
                        InicializarFolio(manager, i)
                        Thread.Sleep(50)
                    Next

                    _Cargado = True
                Catch ex As Exception
                    If (Not View.ViewClosing) Then ShowMessage("InicializarFolios", ex)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    If (manager IsNot Nothing) Then manager.Disconnect()
                End Try
            End If
        End Sub

        Protected Sub InicializarFolio(ByVal nFolio As Integer)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)
                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
                manager = New FileProviderManager(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, centro, dbmImaging, Me.IndexerSesion.Usuario.id)
                manager.Connect()

                InicializarFolio(manager, nFolio)

            Catch ex As Exception
                If (Not View.ViewClosing) Then ShowMessage("InicializarFolio", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

        Private Sub InicializarFolio(ByRef nManager As FileProviderManager, ByVal nFolio As Integer)
            Try
                If (CType(View, Form).InvokeRequired) Then
                    Dim myDelegate As InicializarFolioDelegate

                    myDelegate = AddressOf InicializarFolio
                    CType(View, Form).Invoke(myDelegate, New Object() {nManager, nFolio})
                Else
                    Dim folioActual = CShort(Folders(0).DocumentFile(0).Folio(nFolio).GlobalIndex + 1)
                    Dim imagen() As Byte = Nothing
                    Dim thumbnail() As Byte = Nothing

                    If (Not nManager.ConnectionOpen) Then nManager.Connect()

                    nManager.GetFolio(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, folioActual, imagen, thumbnail)
                    Folders(0).DocumentFile(0).Folio(nFolio).FileName = _TempPath & Guid.NewGuid().ToString() & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    Folders(0).DocumentFile(0).Folio(nFolio).ThumbnailImage = New FreeImageAPI.FreeImageBitmap(New MemoryStream(thumbnail))

                    Using fileImage = New FileStream(Folders(0).DocumentFile(0).Folio(nFolio).FileName, FileMode.Create)
                        fileImage.Write(imagen, 0, imagen.Length)
                        fileImage.Close()
                    End Using
                End If
            Catch ex As Exception
                If (Not View.ViewClosing) Then ShowMessage("InicializarFolio", ex)
            End Try
        End Sub

        Protected Overrides Sub Clear()
            Try : If (View IsNot Nothing) Then View.Clear()
            Catch : End Try

            BorrarTemporal()
        End Sub

        Protected Sub InsertarProductividad(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nProductividad As DesktopConfig.ProductividadType, ByVal nEtapa As DesktopConfig.Etapa_Productividad, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short)
            Try
                Dim hora = Now.Hour
                Dim esNocturno = Not (hora >= 6 And hora < 22)
                dbmImaging.SchemaProcess.PA_Insertar_Productividad.DBExecute(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, ExpedienteRow.fk_Entidad, ExpedienteRow.fk_Proyecto, _
                                                                              ExpedienteRow.fk_Esquema, nProductividad.Usuario, 1, nProductividad.Campos, _
                                                                             nProductividad.Caracteres, nProductividad.Validaciones, nEtapa, esNocturno, Nothing, Nothing, nExpediente, nFolder, nFile, nProductividad.Fecha)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Insertar Productividad", ex)
            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Dim recortes = CType(Me.View, FormRecorte).Recortes

            For Each item As RecorteDataControl In recortes
                If (item.Selector Is Nothing) Then
                    MessageBox.Show("No se han realizado todos los recortes", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If
            Next

            Return True
        End Function

#End Region

    End Class

End Namespace