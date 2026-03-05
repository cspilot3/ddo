Imports System.IO
Imports System.Threading
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Slyg.Tools
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports Slyg.Tools.Imaging
Imports System.Windows.Forms
Imports Miharu.FileProvider.Library

Namespace Controller.Indexer.Capturas

    Public MustInherit Class CapturasController
        Inherits IndexerController

#Region " Declaraciones "

        Protected CamposOCRTataTable As DBOCR.SchemaProcess.TBL_File_Data_CamposDataTable
        Protected CamposDataTable As DBImaging.SchemaConfig.CTA_CampoDataTable
        Protected CamposLlaveDataTable As DBImaging.SchemaConfig.CTA_Campo_LlaveDataTable
        Protected TriggersDataTable As DBImaging.SchemaConfig.TBL_Campo_TriggerDataTable
        Protected TriggersValidacionesDataTable As DBImaging.SchemaConfig.TBL_Campo_Trigger_ValidacionDataTable
        Protected ListaItemsDataTable As DBCore.SchemaConfig.TBL_Campo_Lista_ItemDataTable
        Protected ValidacionesDataTable As DBImaging.SchemaConfig.CTA_ValidacionDataTable
        Protected TablaAsociadaDataTable As DBImaging.SchemaConfig.CTA_Tabla_AsociadaDataTable
        Protected MotivosDataTable As DBCore.SchemaConfig.TBL_Campo_Lista_ItemDataTable
        Protected AnexosDataTable As DBCore.SchemaImaging.CTA_DocumentosDataTable

        Protected ExpedienteRow As DBCore.SchemaProcess.TBL_ExpedienteRow
        Protected FileImagingRow As DBCore.SchemaImaging.TBL_FileRow
        Protected FileCoreRow As DBCore.SchemaProcess.TBL_FileRow
        Protected AnexoImagingRow As DBCore.SchemaImaging.TBL_AnexoRow
        Protected DocumentoRow As DBCore.SchemaConfig.TBL_DocumentoRow

        Private _cargado As Boolean

        Protected _idCargue As Integer
        Protected _idCarguePaquete As SlygNullable(Of Short)
#End Region

#Region " Propiedades "

        Protected MustOverride ReadOnly Property ProcessName As String

#End Region

#Region " Implementación IController IIndexerController "

        Public Overrides Sub Inicializar(ByVal nTempPath As String, ByVal nIndexerSesion As Security.Library.Session.Sesion, ByVal nIndexerDesktopGlobal As DesktopGlobal, ByVal nIndexerImagingGlobal As ImagingGlobal)
            Me.IndexerSesion = nIndexerSesion
            Me.IndexerDesktopGlobal = nIndexerDesktopGlobal
            Me.IndexerImagingGlobal = nIndexerImagingGlobal

            DBImaging.DBImagingDataBaseManager.IdentifierDateFormat = nIndexerDesktopGlobal.IdentifierDateFormat
            DBCore.DBCoreDataBaseManager.IdentifierDateFormat = nIndexerDesktopGlobal.IdentifierDateFormat
            DBSecurity.DBSecurityDataBaseManager.IdentifierDateFormat = nIndexerDesktopGlobal.IdentifierDateFormat

            _IndexerView = New FormIndexerView
            View.SetController(Me)

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

        Public Overrides ReadOnly Property AllowNewFile As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property AllowNewFolder As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides Function NewDocumentFile() As Boolean
            Throw New NotImplementedException()
        End Function

        Public Overrides Function NewFolder() As Boolean
            Throw New NotImplementedException()
        End Function

        Public Overrides Function DesindexarFolio() As Boolean
            Throw New NotImplementedException()
        End Function

        Public Overrides Function NextFolio(ByRef nUpdateInfo As Boolean) As Boolean
            nUpdateInfo = False

            Dim intentos As Integer = 0

            If _CurrentFolioIndex + 1 < Me.Folios Then
                While Not _cargado Or CurrentDocumentFile.Folio(_CurrentFolioIndex + 1).ThumbnailImage Is Nothing
                    Thread.Sleep(1000)

                    intentos += 1

                    If intentos > 180 Then Throw New Exception("Ha expirado el tiempo para el cargue del los folios restantes del Documento")
                End While

                SaveFolio()

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

        Public Overrides Function NextIndexingElement(ByVal ot As Integer, ByVal nEstado As DBCore.EstadoEnum, ByVal nIdDocumento As SlygNullable(Of Integer)) As Boolean
            Dim manager As FileProviderManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmOCR As DBOCR.DBOCRDataBaseManager = Nothing

            Dim fechaInicioProceso = Now

            ' Estado si el proyecto usa OCR_Captura
            Me._Usa_OCR_Captura = Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_OCR_Captura

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)
                dbmOCR = New DBOCR.DBOCRDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.OCR)

                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)
                dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)
                dbmOCR.Connection_Open(Me.IndexerSesion.Usuario.id)

                ' Mirar si existe un registro previamente asignado
                Dim bloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Capturas_get.DBExecute(Me.IndexerDesktopGlobal.SesionID)
                If bloqueoDatatable.Count > 0 Then
                    For Each BloqueoRow In bloqueoDatatable
                        dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas.DBExecute(BloqueoRow.fk_Expediente, BloqueoRow.fk_Folder, BloqueoRow.fk_File)
                    Next
                End If

                Dim bloqueAnexoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Capturas_Anexo_get.DBExecute(Me.IndexerDesktopGlobal.SesionId)
                If bloqueAnexoDatatable.Count > 0 Then
                    For Each BloqueoAnexoRow In bloqueAnexoDatatable
                        dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas_Anexo.DBExecute(BloqueoAnexoRow.fk_Anexo)
                    Next
                End If

                ' Calcular siguiente imagen disponible
                Dim bloqueados As Boolean = dbmImaging.SchemaProcess.PA_Bloqueo_File_Next.DBExecute(ot, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, nEstado, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.SesionID, Me.IndexerDesktopGlobal.PCName)
                If (Not bloqueados) Then Return False

                bloqueAnexoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Capturas_Anexo_get.DBExecute(Me.IndexerDesktopGlobal.SesionId)
                bloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Capturas_get.DBExecute(Me.IndexerDesktopGlobal.SesionId)

                If (bloqueoDatatable.Count > 0 Or bloqueAnexoDatatable.Count > 0) Then

                    If bloqueAnexoDatatable.Count > 0 Then
                        Dim AnexoDataTable = dbmCore.SchemaImaging.TBL_Anexo.DBGet(bloqueAnexoDatatable(0).fk_Anexo)

                        If (AnexoDataTable.Count = 0) Then
                            Throw New Exception("No se encontró data asociada al Anexo: " & bloqueAnexoDatatable(0).fk_Anexo & " del Dashboard")
                        End If
                        AnexoImagingRow = AnexoDataTable(AnexoDataTable.Count - 1)

                        'Dim ExpedienteAnexoDatatable = dbmCore.SchemaImaging.TBL_File.DBFindByEs_Anexofk_Anexo(True, AnexoImagingRow.id_Anexo)

                        'If ExpedienteAnexoDatatable.Count > 0 Then
                        '    Dim expedienteDataTable = dbmCore.SchemaProcess.TBL_Expediente.DBGet(ExpedienteAnexoDatatable(0).fk_Expediente)
                        '    ExpedienteRow = expedienteDataTable(0)
                        'End If
                        Dim DocumentoDatatable = dbmCore.SchemaConfig.TBL_Documento.DBGet(AnexoImagingRow.fk_Documento)
                        DocumentoRow = DocumentoDatatable(0)

                        _idCargue = AnexoImagingRow.fk_Cargue
                        _idCarguePaquete = AnexoImagingRow.fk_Cargue_Paquete

                        ' Leer el listado de imagenes
                        manager = New FileProviderManager(AnexoImagingRow.id_Anexo, dbmImaging, Me.IndexerSesion.Usuario.id)
                        manager.Connect()

                        _ImageCount = manager.GetFolios(AnexoImagingRow.id_Anexo)

                        View.SetTitle(Me.ProcessName & " [Anexo: " & AnexoImagingRow.id_Anexo & "]")
                        IndexerView.Information = ""
                        LoadConfig(dbmCore, dbmImaging, DocumentoRow.fk_Entidad, DocumentoRow.fk_Proyecto, DocumentoRow.fk_Esquema)

                    ElseIf bloqueoDatatable.Count > 0 Then
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

                        _idCargue = folderImagingDataTable(0).fk_Cargue
                        _idCarguePaquete = folderImagingDataTable(0).fk_Cargue_Paquete

                        ' Leer el listado de imagenes
                        '                        manager = New FileProviderManager(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, dbmImaging, Me.IndexerSesion.Usuario.id)
                        manager = New FileProviderManager(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, dbmImaging, Me.IndexerSesion.Usuario.id)
                        manager.Connect()

                        If FileImagingRow.Es_Anexo Then
                            _ImageCount = manager.GetFolios(FileImagingRow.fk_Anexo)
                        Else
                            _ImageCount = manager.GetFolios(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)
                        End If


                        View.SetTitle(Me.ProcessName & " [Ex: " & FileCoreRow.fk_Expediente & " - Fo: " & FileCoreRow.fk_Folder & " - Fi: " & FileCoreRow.id_File & "]")

                        IndexerView.Information = ""
                        Dim llavesDataTable = dbmCore.SchemaProcess.CTA_Expediente_LLave.DBFindByid_Expediente(FileCoreRow.fk_Expediente)
                        For Each Llave In llavesDataTable
                            IndexerView.Information &= "[" & Llave.Nombre_Proyecto_Llave & "]: " & Llave.Valor_Llave.ToString() & vbCrLf
                        Next

                        ' Obtiene los datos de los campos reconocidos por el OCR
                        If _Usa_OCR_Captura Then
                            CamposOCRTataTable = dbmOCR.SchemaProcess.TBL_File_Data_Campos.DBFindByfk_Expedientefk_Folderfk_Filefk_Documento(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento)
                        End If

                        LoadConfig(dbmCore, dbmImaging, ExpedienteRow.fk_Entidad, ExpedienteRow.fk_Proyecto, ExpedienteRow.fk_Esquema)

                        End If
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

            _CurrentFolderIndex = 0
            _CurrentDocumentFileIndex = 0
            _CurrentFolioIndex = 0
            _CurrentImageIndex = 0

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

            View.ThumbnailPanel.Controls.Add(newFolder.Panel)

            Try
                Dim hilo As New Thread(AddressOf InicializarFolios)
                hilo.Start()
            Catch ext As ThreadStateException
                MessageBox.Show("Error: " + ext.Message, "Error en Thread", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                DMB.DesktopMessageShow("NextIndexingElement", ex)
            End Try

            '-------------------------------------------------------------------------------------------------------------
            ' LOGGING - PERFORMANCE
            '-------------------------------------------------------------------------------------------------------------
            Dim fechaFinProceso = Now
            Dim traceMessage As String = ""
            traceMessage &= "Duración:" & vbTab & (fechaInicioProceso - fechaFinProceso).TotalMilliseconds & vbTab

            If AnexoImagingRow IsNot Nothing Then
                traceMessage &= "Anexo:" & vbTab & AnexoImagingRow.id_Anexo & vbTab
            Else
                traceMessage &= "Expediente:" & vbTab & FileCoreRow.fk_Expediente & vbTab
                traceMessage &= "Folder:" & vbTab & FileCoreRow.fk_Folder & vbTab
                traceMessage &= "File:" & vbTab & FileCoreRow.id_File & vbTab
            End If
           
            DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[" & Me.ProcessName & "][NextIndexingElement]")
            '-------------------------------------------------------------------------------------------------------------

            Return True
        End Function

        Public Overrides Function Indexar() As DialogResult
            View.Unlock = True

            IndexerView.Esquema_Enabled = False
            If AnexoImagingRow IsNot Nothing Then
                IndexerView.Esquema_DataSource.RowFilter = "id_Esquema = " & DocumentoRow.fk_Esquema
            Else
                IndexerView.Esquema_DataSource.RowFilter = "id_Esquema = " & ExpedienteRow.fk_Esquema
            End If

            IndexerView.Esquema_Refresh()
            IndexerView.Esquema_Index = CInt(IIf(IndexerView.Esquema_DataSource.Count = 1, 0, -1))

            IndexerView.TipoDocumental_Enabled = False
            If AnexoImagingRow IsNot Nothing Then
                IndexerView.TipoDocumental_DataSource.RowFilter = "id_Documento = " & AnexoImagingRow.fk_Documento
            Else
                IndexerView.TipoDocumental_DataSource.RowFilter = "id_Documento = " & FileCoreRow.fk_Documento
            End If

            IndexerView.TipoDocumental_Refresh()
            IndexerView.TipoDocumental_Index = CInt(IIf(IndexerView.TipoDocumental_DataSource.Count = 1, 0, -1))

            CurrentFolder.Esquema = IndexerView.Esquema_Value

            If (IndexerView.TipoDocumental_DataSource.Count > 0) Then
                CurrentDocumentFile.TipoDocumento = IndexerView.TipoDocumental_Value.Value
                CurrentDocumentFile.NombreTipoDocumento = IndexerView.TipoDocumental_Text
            Else
                If AnexoImagingRow IsNot Nothing Then
                    Throw New Exception("Error al cargar el File, es posible que el documento: " & AnexoImagingRow.fk_Documento & ", halla sido eliminado. Anexo: " & AnexoImagingRow.id_Anexo)
                Else
                    Throw New Exception("Error al cargar el File, es posible que el documento: " & FileCoreRow.fk_Documento & ", halla sido eliminado. Expediente: " & FileCoreRow.fk_Expediente & ", Folder: " & FileCoreRow.fk_Folder & ", File: " & FileCoreRow.id_File)
                End If

            End If

            _Ciclo += 1

            View.ActivarControles(True)

            Return View.ShowDialog()
        End Function

        Public Overrides Function Validar() As Boolean
            For Each Campo In _Campos
                Try
                    If Not Campo.Control Is Nothing Then
                        If (Not Campo.Control.Validar()) Then
                            Return False
                        End If
                    Else
                        MessageBox.Show("C")
                    End If
                Catch
                    Return False
                End Try
            Next

            If _Validaciones.Count > 0 Then
                For Each item In _Validaciones
                    If (Not item.Control.Validar()) Then
                        Return False
                    End If
                Next
            End If

            Return True
        End Function

        Public Overrides Function SetCurrentFolio(ByVal value As Generic.Folio, ByRef nUpdateInfo As Boolean) As Boolean
            nUpdateInfo = False

            Dim intentos As Integer = 0

            If (value.GlobalIndex > 0) Then
                While Not _cargado Or value.ThumbnailImage Is Nothing
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
                fileType.Usuario_Primera_Captura = DBNull.Value
                fileType.Fecha_Primera_Captura = DBNull.Value
                fileType.Usuario_Segunda_Captura = DBNull.Value
                fileType.Fecha_Segunda_Captura = DBNull.Value
                fileType.Usuario_Tercera_Captura = DBNull.Value
                fileType.Fecha_Tercera_Captura = DBNull.Value
                fileType.Usuario_Calidad = DBNull.Value
                fileType.Fecha_Calidad = DBNull.Value
                fileType.Usuario_Validacion_Listas = DBNull.Value
                fileType.Fecha_Validacion_Listas = DBNull.Value
                fileType.Usuario_Recortes = DBNull.Value
                fileType.Fecha_Recortes = DBNull.Value
                fileType.Usuario_Calidad_Recortes = DBNull.Value
                fileType.Fecha_Calidad_Recortes = DBNull.Value
                fileType.Usuario_Proceso_Adicional = DBNull.Value
                fileType.Fecha_Proceso_Adicional = DBNull.Value
                fileType.Usuario_Modifica_Correccion = DBNull.Value
                fileType.Fecha_Modifica_Correccion = DBNull.Value
                fileType.Usuario_Correccion_Maquina = DBNull.Value
                fileType.Fecha_Correccion_Maquina = DBNull.Value
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

        Public Overrides Sub Move(ByVal nSourceFolio As Generic.Folio, ByVal nTargetFolio As Generic.Folio, ByVal nTargetIndex As Integer)
            Dim sourceDocument = nSourceFolio.Parent
            Dim sourceFolder = sourceDocument.Parent

            nSourceFolio.Move(nTargetFolio.Parent, nTargetIndex)

            If (sourceDocument.Count = 0) Then sourceFolder.Remove(sourceDocument)
            If (sourceFolder.Count = 0) Then
                Me.View.ThumbnailPanel.Controls.Remove(sourceFolder.Panel)
                Me.Folders.Remove(sourceFolder)
            End If

            Dim i As Integer = 0

            For Each Carpeta In Me.Folders
                For Each documento As Generic.DocumentFile In Carpeta
                    For Each pagina As Generic.Folio In documento
                        pagina.setIndex(i)
                        i += 1
                    Next
                Next
            Next
        End Sub

        Public Overrides Sub AutoIndexar(ByVal nDocumento As Integer, ByVal nFolios As Integer, ByVal nModo As ModoAutoIndexarEnum)
            Throw New NotImplementedException()
        End Sub

        Public Overrides Function Anexos() As List(Of Item)
            Dim resultado = New List(Of Item)

            If AnexoImagingRow Is Nothing Then
                Dim idAnexo As Integer = 0
                For Each Anexo In AnexosDataTable
                    If (Anexo.fk_Expediente <> FileImagingRow.fk_Expediente Or Anexo.fk_Folder <> FileImagingRow.fk_Folder Or Anexo.fk_File <> FileImagingRow.fk_File Or Anexo.id_Version <> FileImagingRow.id_Version) Then
                        resultado.Add(New Item(Anexo, Anexo.Nombre_Documento, idAnexo.ToString()))
                    End If

                    idAnexo += 1
                Next

            End If
            Return resultado
        End Function

        Public Overrides Function LoadAnexo(ByVal idAnexo As Integer) As IEnumerable(Of String)
            Dim manager As FileProviderManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                dbmCore.Connection_Open(0)
                dbmImaging.Connection_Open(0)

                ' Recuperar el file anexo
                Dim fileAnexo = Me.AnexosDataTable(idAnexo)
                Dim fileDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(fileAnexo.fk_Expediente, fileAnexo.fk_Folder, fileAnexo.fk_File, Nothing, 1, New DBCore.SchemaImaging.TBL_FileEnumList(DBCore.SchemaImaging.TBL_FileEnum.id_Version, False))
                If (fileDataTable.Count = 0) Then Throw New Exception("No se encontro el file")

                Dim fileRow = fileDataTable(0)

                Dim listImages = New List(Of String)
                Dim formato = Utilities.getEnumFormat(Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                Dim compresion = Utilities.getEnumCompression(CType(Me.IndexerImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                'Obtiene el File a visualizar.
                If (fileRow.Es_Anexo) Then
                    Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType

                    'Se obtienen los folios del file.
                    manager = New FileProviderManager(fileRow.fk_Anexo, centro, dbmImaging, Me.IndexerSesion.Usuario.id)
                    manager.Connect()
                    Dim totalFolios = manager.GetFolios(fileRow.fk_Anexo)
                    If (totalFolios > 0) Then
                        For folio = CShort(1) To totalFolios
                            Dim fileName = Me._TempPath & fileRow.fk_Anexo.ToString() & "-" & folio.ToString() & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                            Dim imagen As Byte() = Nothing
                            Dim thumbnail As Byte() = Nothing
                            manager.GetFolio(fileRow.fk_Anexo, folio, imagen, thumbnail)
                            ImageManager.Save(New FreeImageAPI.FreeImageBitmap(New MemoryStream(imagen)), fileName, "", formato, compresion, False, Me._TempPath)
                            listImages.Add(fileName)
                        Next
                    Else
                        DMB.DesktopMessageShow("No existen folios en la imágen.", "No existen folios", DMB.IconEnum.AdvertencyIcon, True)
                    End If
                Else
                    Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                    'Se obtienen los folios del file.
                    manager = New FileProviderManager(fileRow.fk_Expediente, fileRow.fk_Folder, centro, dbmImaging, Me.IndexerSesion.Usuario.id)
                    manager.Connect()
                    Dim totalFolios = manager.GetFolios(fileRow.fk_Expediente, fileRow.fk_Folder, fileRow.fk_File, fileRow.id_Version)
                    If (totalFolios > 0) Then
                        For folio = CShort(1) To totalFolios
                            Dim fileName = Me._TempPath & fileRow.fk_Expediente.ToString() & "-" & fileRow.fk_Folder.ToString() & "-" & fileRow.fk_File.ToString() & "-" & fileRow.id_Version.ToString() & "-" & folio.ToString() & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                            Dim imagen As Byte() = Nothing
                            Dim thumbnail As Byte() = Nothing
                            manager.GetFolio(fileRow.fk_Expediente, fileRow.fk_Folder, fileRow.fk_File, fileRow.id_Version, folio, imagen, thumbnail)

                            Using ms = New MemoryStream(imagen)
                                Using bm = New FreeImageAPI.FreeImageBitmap(ms)
                                    ImageManager.Save(bm, fileName, "", formato, compresion, False, Me._TempPath)
                                End Using
                            End Using

                            listImages.Add(fileName)
                        Next
                    Else
                        DMB.DesktopMessageShow("No existen folios en la imágen.", "No existen folios", DMB.IconEnum.AdvertencyIcon, True)
                    End If
                End If

                Dim resultFileName = Me._TempPath & "ImageTIFF-" & idAnexo.ToString() & ".tif"
                ImageManager.Save(listImages, resultFileName, "", ImageManager.EnumFormat.Tiff, ImageManager.EnumCompression.Lzw, False, Me._TempPath, True)

                Return listImages
            Catch ex As Exception
                DMB.DesktopMessageShow("CargarImagen", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try

            Return Nothing
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

        Public Overrides Function CamposLLave(ByVal idDocumento As Integer) As List(Of View.Indexacion.CampoLlaveCaptura)
            Me._CamposLlave.Clear()

            Return _CamposLlave
        End Function

#End Region

#Region " Metodos "

        Protected MustOverride Sub LoadConfig(ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short)

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

                    Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType

                    If AnexoImagingRow IsNot Nothing Then
                        manager = New FileProviderManager(AnexoImagingRow.id_Anexo, centro, dbmImaging, Me.IndexerSesion.Usuario.id)
                    Else
                        manager = New FileProviderManager(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, centro, dbmImaging, Me.IndexerSesion.Usuario.id)
                    End If

                    manager.Connect()

                    For i As Integer = 1 To Folders(0).DocumentFile(0).Count - 1
                        If (View.ViewClosing) Then Return
                        InicializarFolio(manager, i)
                        Thread.Sleep(50)
                    Next

                    _cargado = True
                Catch ex As Exception
                    If (Not View.ViewClosing) Then ShowMessage("InicializarFoliosjjbm", ex)
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

                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType

                If AnexoImagingRow IsNot Nothing Then
                    manager = New FileProviderManager(AnexoImagingRow.id_Anexo, centro, dbmImaging, Me.IndexerSesion.Usuario.id)
                Else
                    manager = New FileProviderManager(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, centro, dbmImaging, Me.IndexerSesion.Usuario.id)
                End If


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

        '' TODO: InicializarFolio-Base
        ''Private Sub InicializarFolio(ByRef nManager As FileProviderManager, ByVal nFolio As Integer)
        ''    Try
        ''        If (CType(_IndexerView, Form).InvokeRequired) Then
        ''            Dim myDelegate As InicializarFolioDelegate

        ''            myDelegate = AddressOf InicializarFolio
        ''            CType(_IndexerView, Form).Invoke(myDelegate, New Object() {nManager, nFolio})
        ''        Else
        ''            Dim folioActual = CShort(Folders(0).DocumentFile(0).Folio(nFolio).GlobalIndex + 1)
        ''            Dim imagen() As Byte = Nothing
        ''            Dim thumbnail() As Byte = Nothing

        ''            If (Not nManager.ConnectionOpen) Then nManager.Connect()

        ''            If AnexoImagingRow IsNot Nothing Then
        ''                nManager.GetFolio(AnexoImagingRow.id_Anexo, folioActual, imagen, thumbnail)
        ''            Else
        ''                nManager.GetFolio(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, folioActual, imagen, thumbnail)
        ''            End If
        ''            Folders(0).DocumentFile(0).Folio(nFolio).FileName = _TempPath & Guid.NewGuid().ToString() & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
        ''            Folders(0).DocumentFile(0).Folio(nFolio).ThumbnailImage = New FreeImageAPI.FreeImageBitmap(New MemoryStream(thumbnail))

        ''            Using fileImage = New FileStream(Folders(0).DocumentFile(0).Folio(nFolio).FileName, FileMode.Create)
        ''                fileImage.Write(imagen, 0, imagen.Length)
        ''                fileImage.Close()
        ''            End Using
        ''        End If
        ''    Catch ex As Exception
        ''        If (Not View.ViewClosing) Then ShowMessage("InicializarFolio", ex)
        ''    End Try
        ''End Sub

        Private Sub InicializarFolio(ByRef nManager As FileProviderManager, ByVal nFolio As Integer)

            Dim dbmocr As DBOCR.DBOCRDataBaseManager = Nothing

            Try
                dbmocr = New DBOCR.DBOCRDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.OCR)
                dbmocr.Connection_Open(Me.IndexerSesion.Usuario.id)

                If (CType(_IndexerView, Form).InvokeRequired) Then
                    Dim myDelegate As InicializarFolioDelegate

                    myDelegate = AddressOf InicializarFolio
                    CType(_IndexerView, Form).Invoke(myDelegate, New Object() {nManager, nFolio})
                Else
                    Dim folioActual = CShort(Folders(0).DocumentFile(0).Folio(nFolio).GlobalIndex + 1)
                    Dim imagen() As Byte = Nothing
                    Dim thumbnail() As Byte = Nothing

                    Try
                        If IsOCRUsed Then
                            Dim nameFileOUTOCR = dbmocr.SchemaConfig.TBL_Parametro.DBFindByNombre_Parametro("Path_OUT_Data")
                            Dim rutaImagenBase As String = nameFileOUTOCR(0).Valor_Parametro
                            'Dim rutaImagenBase As String = "C:\DuvanCastro\PYC\OCR_Results\Out_Data"
                            Dim nameImageExpediente As String = FileImagingRow.fk_Expediente & FileImagingRow.fk_Folder & FileImagingRow.fk_File
                            Dim filePath As String = rutaImagenBase & "\" & nameImageExpediente & "\"
                            Dim nameCorrectsesgoImage As String = filePath & nameImageExpediente & folioActual & ".tif"
                            imagen = File.ReadAllBytes(nameCorrectsesgoImage)
                            'imagen = ImageManager.GetFolioData(nameCorrectsesgoImage, 1, ".tif", compresion)

                            Dim dataImageThumbnail = ImageManager.GetThumbnailData(nameCorrectsesgoImage, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)
                            thumbnail = dataImageThumbnail(0)
                        End If
                    Catch
                        imagen = Nothing
                        thumbnail = Nothing
                    End Try


                    If imagen Is Nothing Or thumbnail Is Nothing Then
                        If (Not nManager.ConnectionOpen) Then nManager.Connect()

                        If AnexoImagingRow IsNot Nothing Then
                            nManager.GetFolio(AnexoImagingRow.id_Anexo, folioActual, imagen, thumbnail)
                        Else
                            nManager.GetFolio(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, folioActual, imagen, thumbnail)
                        End If
                    End If

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

        Protected Sub InsertarProductividad(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nProductividad As DesktopConfig.ProductividadType, ByVal nEtapa As DesktopConfig.Etapa_Productividad, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short)
            Try
                Dim hora = Now.Hour
                Dim esNocturno = Not (hora >= 6 And hora < 21)
                dbmImaging.SchemaProcess.PA_Insertar_Productividad.DBExecute(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, ExpedienteRow.fk_Entidad, ExpedienteRow.fk_Proyecto, _
                                                                              ExpedienteRow.fk_Esquema, nProductividad.Usuario, 1, nProductividad.Campos + nProductividad.CamposLlave, _
                                                                             nProductividad.Caracteres, nProductividad.Validaciones, nEtapa, esNocturno, Nothing, Nothing, nExpediente, nFolder, nFile, nProductividad.Fecha)
            Catch ex As Exception
                DMB.DesktopMessageShow("Insertar Productividad", ex)
            End Try
        End Sub

        Protected Sub InsertarProductividad_Error(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nErrores As DesktopConfig.ProductividadType(), ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short)
            Try
                Dim hora = Now.Hour
                Dim esNocturno = Not (hora >= 6 And hora < 21)

                For Each ErrorP In nErrores
                    If (ErrorP.Campos > 0) Then
                        dbmImaging.SchemaProcess.PA_Insertar_Productividad.DBExecute(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, _
                                                                                        ExpedienteRow.fk_Entidad, ExpedienteRow.fk_Proyecto, _
                                                                                        ExpedienteRow.fk_Esquema, ErrorP.Usuario, 1, ErrorP.Campos + ErrorP.CamposLlave, ErrorP.Caracteres, ErrorP.Validaciones, _
                                                                                        DesktopConfig.Etapa_Productividad.Errores_cometidos, esNocturno, Nothing, Nothing, nExpediente, nFolder, nFile, ErrorP.Fecha)
                    End If
                Next
            Catch ex As Exception
                DMB.DesktopMessageShow("Insertar Productividad Error Captura", ex)
            End Try

        End Sub

        Protected Sub InsertarProductividad_Error_Detallado(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nCampo As CampoCaptura, ByVal nCampoTabla As SlygNullable(Of Short), ByVal nRegistro As SlygNullable(Of Short), ByVal nProductividad As DesktopConfig.ProductividadType, ByVal nValorCaptura As Object, ByVal nUsuarioCorreccion As Integer, ByVal nValorCorreccion As Object, ByVal nEtapa As DesktopConfig.Etapa_Productividad)
            If (nProductividad.Usuario <> 1) Then ' Service
                Try
                    Dim productividadRow As New DBImaging.SchemaProcess.TBL_Productividad_ErrorType

                    productividadRow.fk_Expediente = FileCoreRow.fk_Expediente
                    productividadRow.fk_Folder = FileCoreRow.fk_Folder
                    productividadRow.fk_File = FileCoreRow.id_File
                    productividadRow.fk_Documento = FileCoreRow.fk_Documento
                    productividadRow.fk_Campo = nCampo.id
                    productividadRow.fk_Campo_Tabla = nCampoTabla
                    productividadRow.fk_File_Data_Asociada = nRegistro
                    productividadRow.fk_Etapa = nEtapa
                    productividadRow.fk_Usuario_Digitacion = nProductividad.Usuario
                    productividadRow.Fecha_Digitacion = nProductividad.Fecha
                    productividadRow.Valor_Captura = nValorCaptura
                    productividadRow.fk_Usuario_Correccion = nUsuarioCorreccion
                    productividadRow.Fecha_Correccion = SlygNullable.SysDate
                    productividadRow.Valor_Correccion = nValorCorreccion

                    dbmImaging.SchemaProcess.TBL_Productividad_Error.DBInsert(productividadRow)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Productividad_Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Sub

#End Region

    End Class

End Namespace