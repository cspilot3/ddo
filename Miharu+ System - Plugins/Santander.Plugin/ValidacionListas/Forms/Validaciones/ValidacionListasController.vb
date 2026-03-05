Imports System.Threading
Imports Miharu
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging.Indexer
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Imaging.Indexer.Controller.Indexer.Capturas
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Slyg.Tools
Imports DBImaging.SchemaConfig
Imports System.Windows.Forms
Imports Miharu.FileProvider.Library
Imports Miharu.Imaging.Indexer.Generic
Imports System.IO
Imports System.Text
Imports Slyg.Tools.Imaging

Namespace ValidacionListas.Forms.Validaciones

    Public Class ValidacionListasController
        Inherits Miharu.Imaging.Indexer.Controller.Indexer.IndexerController


#Region " Declaraciones "

        Private Class FolioValidacionListas
            Public Property EsExterno() As Boolean
            Public Property Expediente() As Long
            Public Property Folder() As Short
            Public Property File() As Short
            Public Property Version() As Short
            Public Property Folio() As Short
            Public Property Filename() As String
        End Class

        'Protected MesaControlDataTable As New DBImaging.SchemaProcess.TBL_File_Data_MCDataTable()
        Protected FileDataDataTable As New DBCore.SchemaProcess.TBL_File_DataDataTable()
        Protected CamposDataTable As DBImaging.SchemaConfig.CTA_CampoDataTable
        Protected ListaItemsDataTable As DBCore.SchemaConfig.TBL_Campo_Lista_ItemDataTable
        Protected ValidacionesDataTable As DBImaging.SchemaConfig.CTA_ValidacionDataTable
        Protected MotivosDataTable As DBCore.SchemaConfig.TBL_Campo_Lista_ItemDataTable
        Protected TablaAsociadaDataTable As DBImaging.SchemaConfig.CTA_Tabla_AsociadaDataTable
        Protected AnexosDataTable As DBCore.SchemaImaging.CTA_DocumentosDataTable
        Private _foliosTable As New List(Of FolioValidacionListas)
        Private _centro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType


        Private _cargado As Boolean

        Protected ExpedienteRow As DBCore.SchemaProcess.TBL_ExpedienteRow
        Protected FileImagingRow As DBCore.SchemaImaging.TBL_FileRow
        Protected FileCoreRow As DBCore.SchemaProcess.TBL_FileRow


        Protected _idCargue As Integer
        Protected _idCarguePaquete As SlygNullable(Of Short)

        Public ControlCaptura As Miharu.Imaging.Indexer.View.IInputControl
        Public CedulaCapturada As String
        Public NombreCapturado As String
        Public ValidarProcuraduria As String
        Public RespuestaFactiva As String
        Public RespuestaContraloria As String
        Public RespuestaProcuraduria As String

        Public ValidacionListasControl As Santander.Plugin.ValidacionListas.Controls.ListValidationControl = Nothing

        Public _SantanderConnectionString As String
        Public _ToolsConnectionString As String

#End Region

#Region " Propiedades "

        Protected ReadOnly Property ProcessName As String
            Get
                Return "Validacion Listas"
            End Get
        End Property

#End Region

#Region " Implementación IIndexerController "

        Public Overrides Sub Inicializar(ByVal nTempPath As String, nIndexerSesion As Security.Library.Session.Sesion, nIndexerDesktopGlobal As DesktopGlobal, nIndexerImagingGlobal As ImagingGlobal)
            'MyBase.Inicializar(nTempPath, nIndexerSesion, nIndexerDesktopGlobal, nIndexerImagingGlobal)

            Me.IndexerSesion = nIndexerSesion
            Me.IndexerDesktopGlobal = nIndexerDesktopGlobal
            Me.IndexerImagingGlobal = nIndexerImagingGlobal

            DBImaging.DBImagingDataBaseManager.IdentifierDateFormat = nIndexerDesktopGlobal.IdentifierDateFormat
            DBCore.DBCoreDataBaseManager.IdentifierDateFormat = nIndexerDesktopGlobal.IdentifierDateFormat
            DBSecurity.DBSecurityDataBaseManager.IdentifierDateFormat = nIndexerDesktopGlobal.IdentifierDateFormat

            _IndexerView = New FormIndexerView
            View.SetController(Me)

            IndexerView.ShowDesindexarFolioButton(False)
            IndexerView.ShowSaveButton(True)
            IndexerView.ShowAddFolioButton(False)
            IndexerView.ShowDeleteFolioButton(False)
            IndexerView.ShowNewFolderButton(False)
            IndexerView.ShowNewFileButton(False)
            IndexerView.ShowNextButton(True)
            IndexerView.ShowReprocesoButton(False)

            IndexerView.ShowInformationPanel(False)
            IndexerView.ShowDataPanel(True)
            IndexerView.ShowValidationsPanel(False)
            IndexerView.ShowAutoIndexar(False)

            IndexerView.Esquema_Enabled = False
            IndexerView.TipoDocumental_Enabled = False

            IndexerView.ShowValidationsListasPanel(True)

            _TempPath = nTempPath.TrimEnd("\"c) & "\"
            If (Not Directory.Exists(nTempPath)) Then
                Directory.CreateDirectory(nTempPath)
            End If

            BorrarTemporal()

        End Sub

        Public Overrides ReadOnly Property Cargado As Boolean
            Get
                Return _cargado
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

            _CurrentImageIndex += 1
            _CurrentFolioIndex += 1

            Return True

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
            'Throw New NotImplementedException()

        End Sub

        Public Overrides Function NextIndexingElement(ByVal ot As Integer, nEstado As DBCore.EstadoEnum, nIdDocumento As SlygNullable(Of Integer)) As Boolean
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Dim fechaInicioProceso = Now

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)
                dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)

                ' Mirar si existe un registro previamente asignado
                Dim bloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Validacion_Listas_get.DBExecute(Me.IndexerDesktopGlobal.SesionId)
                If bloqueoDatatable.Count > 0 Then
                    For Each BloqueoRow In bloqueoDatatable
                        dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Validacion_Listas.DBExecute(BloqueoRow.fk_Expediente, BloqueoRow.fk_Folder, BloqueoRow.fk_File)
                    Next
                End If

                ' Calcular siguiente imagen disponible
                Dim bloqueados As Boolean
                If (nIdDocumento Is Nothing) Then
                    bloqueados = dbmImaging.SchemaProcess.PA_Bloqueo_Validacion_Listas_Next.DBExecute(ot, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.SesionId, Me.IndexerDesktopGlobal.PcName)
                Else
                    bloqueados = dbmImaging.SchemaProcess.PA_Bloqueo_Validacion_Listas_Next_Documento.DBExecute(ot, nIdDocumento, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.SesionId, Me.IndexerDesktopGlobal.PcName)
                End If
                If (Not bloqueados) Then Return False

                bloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Validacion_Listas_get.DBExecute(Me.IndexerDesktopGlobal.SesionId)
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

                    _idCargue = folderImagingDataTable(0).fk_Cargue
                    _idCarguePaquete = folderImagingDataTable(0).fk_Cargue_Paquete

                    Me._centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                    ' Leer el listado de imagenes
                    manager = New FileProviderManager(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, dbmImaging, Me.IndexerSesion.Usuario.id)

                    manager.Connect()
                    '_ImageCount = manager.GetFolios(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)

                    View.SetTitle("Validacion Listas [Ex: " & FileCoreRow.fk_Expediente & " - Fo: " & FileCoreRow.fk_Folder & " - Fi: " & FileCoreRow.id_File & "]")


                    IndexerView.Information = ""
                    'Dim llavesDataTable = dbmCore.SchemaProcess.CTA_Expediente_LLave.DBFindByid_Expediente(FileCoreRow.fk_Expediente)
                    'For Each Llave In llavesDataTable
                    '    IndexerView.Information &= "[" & Llave.Nombre_Proyecto_Llave & "]: " & Llave.Valor_Llave.ToString() & vbCrLf
                    'Next

                    ' Anexos del documento
                    'AnexosDataTable = dbmCore.SchemaImaging.CTA_Documentos.DBFindByfk_Expedientefk_Folder(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

                    LoadConfig(dbmCore, dbmImaging, ExpedienteRow.fk_Entidad, ExpedienteRow.fk_Proyecto, ExpedienteRow.fk_Esquema)
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw

            Finally
                If (manager IsNot Nothing) Then manager.Disconnect()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            ' Liberar la memoria
            Clear()


            Application.DoEvents()

            _CurrentFolderIndex = 0
            _CurrentDocumentFileIndex = 0
            _CurrentFolioIndex = 0
            _CurrentImageIndex = 0

            'If (_ImageCount = 0) Then
            '    Throw New Exception("El File no tiene folios, por favor valide con Soporte. Expediente: " & FileCoreRow.fk_Expediente & " - Folder: " & FileCoreRow.fk_Folder & " - File: " & FileCoreRow.id_File)
            'End If

            ' Crear data inicial
            Dim newFolder = New Generic.Folder(Me)
            Folders.Add(newFolder)

            Dim newDocumento = newFolder.NewDocumentFile()
            newFolder.Add(newDocumento)

            'If _ImageCount > 0 Then
            '    For i = 0 To _ImageCount - 1
            '        Dim newFolio = newDocumento.NewFolio(True)
            '        newDocumento.Add(newFolio)
            '    Next

            '    InicializarFolio(_CurrentFolioIndex)
            'End If

            'Dim newFolio = newDocumento.NewFolio(True)
            'newDocumento.Add(newFolio)
            'InicializarFolio(newFolio)

            View.ThumbnailPanel.Controls.Add(newFolder.Panel)

            'Try
            '    Dim hilo As New Thread(AddressOf InicializarFolios)
            '    hilo.Start()
            'Catch ext As ThreadStateException
            '    MessageBox.Show("Error: " + ext.Message, "Error en Thread", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Catch ex As Exception
            '    DesktopMessageBoxControl.DesktopMessageShow("NextIndexingElement", ex)
            'End Try

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

            IndexerView.Esquema_Enabled = False
            IndexerView.Esquema_DataSource.RowFilter = "id_Esquema = " & ExpedienteRow.fk_Esquema
            IndexerView.Esquema_Refresh()
            IndexerView.Esquema_Index = CInt(IIf(IndexerView.Esquema_DataSource.Count = 1, 0, -1))

            IndexerView.TipoDocumental_Enabled = False
            IndexerView.TipoDocumental_DataSource.RowFilter = "id_Documento = " & FileCoreRow.fk_Documento
            IndexerView.TipoDocumental_Refresh()
            IndexerView.TipoDocumental_Index = CInt(IIf(IndexerView.TipoDocumental_DataSource.Count = 1, 0, -1))

            CurrentFolder.Esquema = IndexerView.Esquema_Value

            If (IndexerView.TipoDocumental_DataSource.Count > 0) Then
                CurrentDocumentFile.TipoDocumento = IndexerView.TipoDocumental_Value.Value
                CurrentDocumentFile.NombreTipoDocumento = IndexerView.TipoDocumental_Text
            Else
                Throw New Exception("Error al cargar el File, es posible que el documento: " & FileCoreRow.fk_Documento & ", halla sido eliminado. Expediente: " & FileCoreRow.fk_Expediente & ", Folder: " & FileCoreRow.fk_Folder & ", File: " & FileCoreRow.id_File)
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

            'If (value.GlobalIndex > 0) Then
            '    While Not _cargado Or value.ThumbnailImage Is Nothing
            '        Thread.Sleep(1000)

            '        intentos += 1

            '        If intentos > 180 Then Throw New Exception("Ha expirado el tiempo para el cargue del los folios restantes del Documento")
            '    End While
            'End If

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
            Throw New NotImplementedException()
        End Function

        Public Overrides Sub Move(ByVal nSourceFolio As Generic.Folio, ByVal nTargetFolio As Generic.Folio, ByVal nTargetIndex As Integer)
            Throw New NotImplementedException()
        End Sub

        Public Overrides Sub AutoIndexar(ByVal nDocumento As Integer, ByVal nFolios As Integer, ByVal nModo As ModoAutoIndexarEnum)
            Throw New NotImplementedException()
        End Sub

        Public Overrides Function Anexos() As List(Of Item)
            Dim resultado = New List(Of Item)

            Dim idAnexo As Integer = 0
            For Each Anexo In AnexosDataTable
                If (Anexo.fk_Expediente <> FileImagingRow.fk_Expediente Or Anexo.fk_Folder <> FileImagingRow.fk_Folder Or Anexo.fk_File <> FileImagingRow.fk_File Or Anexo.id_Version <> FileImagingRow.id_Version) Then
                    resultado.Add(New Item(Anexo, Anexo.Nombre_Documento, idAnexo.ToString()))
                End If

                idAnexo += 1
            Next

            Return resultado
        End Function

        Public Overrides Function LoadAnexo(ByVal idAnexo As Integer) As IEnumerable(Of String)
            Throw New NotImplementedException()
        End Function

        Public Overrides Function MotivosReproceso() As List(Of Item)
            Throw New NotImplementedException()
        End Function

        Public Overrides ReadOnly Property ShowSecondControls As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides Function AddFolio() As Boolean
            Throw New NotImplementedException()
        End Function

        Public Function AdicionarFolio(ByVal nFileName As String, ByVal nfolio As Integer, ByVal nTotalFolios As Integer) As Boolean

            Dim NewFolio As New Miharu.Imaging.Indexer.Generic.Folio(CurrentDocumentFile, False)

            NewFolio.FileName = nFileName

            CurrentDocumentFile.Add(NewFolio)

            _ImageCount = nTotalFolios

            Return True
        End Function

        Public Overrides Function DeleteFolio() As Boolean
            Throw New NotImplementedException()
        End Function

        Private Function GetSize(ByRef documentItem As Miharu.Imaging.Indexer.Generic.DocumentFile, ByRef modo As Miharu.Imaging.Indexer.Generic.Folder.FolderModoEnum, ByRef CantFolios As Short, ByVal formato As Slyg.Tools.Imaging.ImageManager.EnumFormat, ByVal compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression) As Long
            Dim Peso As Long = 0
            Dim idFolioItem As Integer = 0
            For Each folioItem As Generic.Folio In documentItem
                If (modo = Generic.Folder.FolderModoEnum.Normal) Then idFolioItem += 1
                Dim FolioBitmap = ImageManager.GetFolioBitmap(folioItem.FileName, 1)
                Dim dataImage = ImageManager.GetFolioData(FolioBitmap, 1, 1, formato, compresion)
                Peso += dataImage.Length
            Next
            CantFolios = idFolioItem
            Return Peso
        End Function

        Public Overrides Function Save() As Boolean

            'Dim items As Integer = 0
            Dim manager As FileProviderManager = Nothing
            Dim Productividad As New DesktopConfig.ProductividadType


            Dim formato = Utilities.GetEnumFormat(Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
            Dim compresion = Utilities.GetEnumCompression(CType(Me.IndexerImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

            Productividad.Usuario = Me.IndexerSesion.Usuario.id

            If (Validar()) Then
                Try
                    Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                    Dim FechaInicioProceso = Now

                    Try
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                        dbmCore = New DBCore.DBCoreDataBaseManager(dbmImaging, Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                        dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)


                        'dbmCore.LinkDataBaseManager(dbmImaging)
                        'dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)

                        dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)
                        'dbmCore.Transaction_Begin()

                        Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Me.IndexerDesktopGlobal.ServidorImagenRow.fk_Entidad, Me.IndexerDesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType

                        manager = New FileProviderManager(servidor, _centro, dbmImaging, Me.IndexerSesion.Usuario.id)
                        manager.Connect()

                        ' Reportar usuario captura
                        Dim FileType = New DBImaging.SchemaProcess.TBL_FileType()
                        FileType.Usuario_Validacion_Listas = Me.IndexerSesion.Usuario.id
                        FileType.Fecha_Validacion_Listas = SlygNullable.SysDate
                        dbmImaging.SchemaProcess.TBL_File.DBUpdate(FileType, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)

                        ' Actualizar llaves
                        dbmCore.SchemaProcess.PA_Calcular_Llaves.DBExecute(FileImagingRow.fk_Expediente)

                        ' Aplicar validaciones automáticas
                        'dbmImaging.SchemaProcess.PA_Aplicar_Validaciones_Automaticas.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento)

                        'Insercion de campos repuestas a las validaciones
                        Dim dataType = New DBCore.SchemaProcess.TBL_File_DataType

                        dataType.fk_Expediente = FileCoreRow.fk_Expediente
                        dataType.fk_Folder = FileCoreRow.fk_Folder
                        dataType.fk_File = FileCoreRow.id_File
                        dataType.fk_Documento = FileCoreRow.fk_Documento
                        'Factiva
                        dataType.fk_Campo = CShort(5)
                        dataType.Valor_File_Data = RespuestaFactiva
                        dataType.Conteo_File_Data = Len(RespuestaFactiva)

                        dbmCore.SchemaProcess.TBL_File_Data.DBInsert(dataType)

                        Productividad.Campos += 1
                        Productividad.Caracteres += RespuestaFactiva.ToString().Length

                        'Contraloria
                        dataType.fk_Campo = CShort(6)
                        dataType.Valor_File_Data = RespuestaContraloria
                        dataType.Conteo_File_Data = Len(RespuestaContraloria)

                        dbmCore.SchemaProcess.TBL_File_Data.DBInsert(dataType)

                        Productividad.Campos += 1
                        Productividad.Caracteres += RespuestaContraloria.ToString().Length

                        'Procuraduria
                        dataType.fk_Campo = CShort(7)
                        dataType.Valor_File_Data = RespuestaProcuraduria
                        dataType.Conteo_File_Data = Len(RespuestaProcuraduria)

                        dbmCore.SchemaProcess.TBL_File_Data.DBInsert(dataType)

                        Productividad.Campos += 1
                        Productividad.Caracteres += RespuestaProcuraduria.ToString().Length

                        ' Insertar productividad
                        InsertarProductividad(dbmImaging, Productividad, DesktopConfig.Etapa_Productividad.ProcesoAdicional_Captura, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)


                        'Actualizacion numero de folios por expediente-folder-file
                        Dim UpdateFoliosImaging As New DBCore.SchemaImaging.TBL_FileType()
                        UpdateFoliosImaging.Folios_Documento_File = _ImageCount + 1
                        dbmCore.SchemaImaging.TBL_File.DBUpdate(UpdateFoliosImaging, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)

                        'Actualizacion numero de folios por expediente-folder-file
                        Dim UpdateFoliosProcess As New DBCore.SchemaProcess.TBL_FileType()
                        UpdateFoliosProcess.Folios_File = _ImageCount + 1
                        dbmCore.SchemaProcess.TBL_File.DBUpdate(UpdateFoliosProcess, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)

                        'Imagenes
                        Dim idFolderItem As Integer = 0
                        idFolderItem = 0
                        For Each FolderItem In Folders
                            ' Crear FolderItem
                            If (FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal) Then idFolderItem += 1

                            Dim idDocumentoItem As Integer = 0
                            For Each documentItem As Generic.DocumentFile In FolderItem
                                If (FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal) Then idDocumentoItem += 1

                                ''20210305
                                'Dim CantFolios As Short = 0
                                'manager.EvaluateVolumen(FileImagingRow.fk_Expediente, GetSize(documentItem, FolderItem.Modo, CantFolios, formato, compresion), CantFolios)

                                Dim idFolioItem As Integer = 0
                                For Each folioItem As Generic.Folio In documentItem
                                    If (FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal) Then idFolioItem += 1

                                    ' Preparar data de la imagen
                                    'ImageManager.Save(folioItem.ThumbnailImage, Program.AppPath & Program.TempPath & "temp" & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, "", formato, compresion, False, Program.AppPath & Program.TempPath)

                                    ' Dim dataImage = ImageManager.GetData(folioItem.FileName)
                                    Dim FolioBitmap = ImageManager.GetFolioBitmap(folioItem.FileName, 1)
                                    Dim dataImage = ImageManager.GetFolioData(FolioBitmap, 1, 1, formato, compresion)
                                    Dim dataImageThumbnail = ImageManager.GetThumbnailData(folioItem.FileName, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)

                                    ' Actualizar estado folio cargue
                                    'dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBUpdate(FolioType, PaqueteRow.fk_Cargue, PaqueteRow.id_Cargue_Paquete, FolioItem.Cargue_Item, FolioItem.Id_Item_Folio)
                                    'dbmImaging.SchemaProcess.PA_Set_Cargue_Folio_Indexado.DBExecute(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete, folioItem.Cargue_Item, folioItem.Id_Item_Folio)

                                    ' Si es un File
                                    If (FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal) Then
                                        manager.CreateItem(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, Guid.NewGuid())
                                        manager.CreateFolio(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, idFolioItem, dataImage, dataImageThumbnail(0), False)
                                    End If
                                Next
                            Next
                        Next


                        ' Actualizar estado
                        Dim NextEstado = DBCore.EstadoEnum.Indexado

                        Dim UpdateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                        UpdateEstado.Fecha_Log = SlygNullable.SysDate
                        UpdateEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                        UpdateEstado.fk_Estado = NextEstado
                        dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(UpdateEstado, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, DesktopConfig.Modulo.Imaging)

                        '---------------------------------------------------------------------------
                        ' Actualizar Dashboard
                        '---------------------------------------------------------------------------
                        If (NextEstado = DBCore.EstadoEnum.Indexado) Then
                            dbmImaging.SchemaProcess.TBL_Dashboard_Validacion_Listas.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                        End If
                        '---------------------------------------------------------------------------

                        'dbmCore.Transaction_Commit()
                        dbmImaging.Transaction_Commit()

                        ' Actualizar el estado de los cargues que ya fueron procesados
                        Try
                            dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                            Dim CarguePaqueteFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_File.DBFindByid_Cargueid_Cargue_Paquete(_idCargue, _idCarguePaquete)
                            If (CarguePaqueteFileDataTable.Count > 0) Then
                                Dim UpdatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                                UpdatePaquete.fk_Estado = CarguePaqueteFileDataTable(0).Estado_File
                                dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(UpdatePaquete, _idCargue, _idCarguePaquete)

                                Dim CargueFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Estado.DBFindByid_Cargue(_idCargue)
                                If (CargueFileDataTable.Count > 0) Then
                                    Dim UpdateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                                    UpdateCargue.fk_Estado = CargueFileDataTable(0).Estado_File
                                    dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(UpdateCargue, _idCargue)
                                End If
                            End If

                            dbmImaging.Transaction_Commit()
                        Catch ex As Exception
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                            Throw
                        End Try
                        ' End If
                    Catch ex As Exception
                        'If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                        DesktopMessageBoxControl.DesktopMessageShow("Publicar_Validacion_Listas", ex)

                        Return False
                    Finally
                        'If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    End Try


                    If RespuestaContraloria = "POSITIVO" Or RespuestaFactiva = "POSITIVO" Or RespuestaFactiva = "MULTIPLE" Or RespuestaProcuraduria = "POSITIVO" Then
                        EnviarCorreo()
                    End If

                    'Borrar Archivos PDF cargados
                    'ValidacionListasControl.BorrarArchivos()
                    '-------------------------------------------------------------------------------------------------------------
                    ' LOGGING - PERFORMANCE
                    '-------------------------------------------------------------------------------------------------------------
                    Dim FechaFinProceso = Now
                    Dim TraceMessage As String = "[Validacion Listas][Publicar] - Inicio: " & FechaInicioProceso.ToString() & " Fin: " & FechaFinProceso.ToString()
                    DesktopTrace.Trace(TraceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, Program.AssemblyName)
                    '-------------------------------------------------------------------------------------------------------------

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Publicar_Validacion_Listas", ex)

                    Return False
                End Try

                Return True

            End If

            Return False
        End Function

        Public Overrides Function ValidacionListas() As Boolean
            Dim _ValidacionListas As Boolean

            _ValidacionListas = True

            Return _ValidacionListas

        End Function

        Public Overrides Function Campos(ByVal idDocumento As Integer) As List(Of CampoCaptura)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Me._Campos.Clear()

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                For Each RowData In FileDataDataTable

                    Dim CampoRow As DBImaging.SchemaConfig.CTA_CampoRow = CType(CamposDataTable.Select("fk_Documento = " & idDocumento & " AND id_Campo = " & RowData.fk_Campo)(0), DBImaging.SchemaConfig.CTA_CampoRow)
                    'Dim ControlCaptura As Imaging.Indexer.View.IInputControl
                    Dim CampoCaptura As New CampoCaptura()
                    Dim ListDefinicionCaptura As New List(Of DefinicionCaptura)

                    ' Control
                    Select Case CType(CampoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                        Case DesktopConfig.CampoTipo.Texto
                            Dim DefinicionCaptura = New DefinicionCaptura()


                            ControlCaptura = New TextInputControl()
                            ControlCaptura.ValueValidacionListas = RowData.Valor_File_Data


                            DefinicionCaptura.Type = DesktopConfig.CampoTipo.Texto
                            DefinicionCaptura.Mascara = CampoRow.Mascara
                            DefinicionCaptura.FormatoFecha = CampoRow.Formato
                            DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo
                            DefinicionCaptura.MaximumLength = CampoRow.Length_Campo
                            DefinicionCaptura.MinimumLength = CampoRow.Length_Min_Campo

                            ListDefinicionCaptura.Add(DefinicionCaptura)
                            ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                            If (CampoRow.Valor_por_Defecto <> "") Then
                                ControlCaptura.Value = CampoRow.Valor_por_Defecto
                            End If

                        Case DesktopConfig.CampoTipo.Numerico
                            Dim DefinicionCaptura = New DefinicionCaptura()

                            ControlCaptura = New TextInputNumericControl()
                            ControlCaptura.ValueValidacionListas = RowData.Valor_File_Data

                            DefinicionCaptura.Type = DesktopConfig.CampoTipo.Numerico
                            DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo
                            DefinicionCaptura.MaximumLength = CampoRow.Length_Campo
                            DefinicionCaptura.MinimumLength = CampoRow.Length_Min_Campo
                            DefinicionCaptura.Usa_Decimales = CampoRow.Usa_Decimales

                            If CampoRow.Usa_Decimales Then
                                DefinicionCaptura.Caracter_Decimal = CChar(CampoRow.Caracter_Decimal)
                                DefinicionCaptura.Cantidad_Decimales = CampoRow.Cantidad_Decimales
                            End If

                            ListDefinicionCaptura.Add(DefinicionCaptura)
                            ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                            If (CampoRow.Valor_por_Defecto <> "") Then
                                ControlCaptura.Value = CampoRow.Valor_por_Defecto
                            End If

                        Case DesktopConfig.CampoTipo.Fecha
                            Dim DefinicionCaptura = New DefinicionCaptura()

                            ControlCaptura = New TextInputControl()
                            ControlCaptura.ValueValidacionListas = RowData.Valor_File_Data

                            DefinicionCaptura.Type = DesktopConfig.CampoTipo.Fecha
                            DefinicionCaptura.Mascara = CampoRow.Mascara
                            DefinicionCaptura.FormatoFecha = CampoRow.Formato
                            DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo

                            ListDefinicionCaptura.Add(DefinicionCaptura)
                            ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                            If (CampoRow.Valor_por_Defecto <> "") Then
                                ControlCaptura.Value = CampoRow.Valor_por_Defecto
                            End If

                        Case DesktopConfig.CampoTipo.Lista
                            Dim DefinicionCaptura = New DefinicionCaptura()
                            ControlCaptura = New ListInputControl()

                            ControlCaptura.ValueValidacionListas = RowData.Valor_File_Data

                            DefinicionCaptura.Type = DesktopConfig.CampoTipo.Lista
                            DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo

                            ' Datos de la lista
                            If (Not CampoRow.Isfk_Campo_ListaNull()) Then

                                Dim dtvLista As New DataView(ListaItemsDataTable)


                                dtvLista.RowFilter = "fk_Campo_Lista = " & CampoRow.fk_Campo_Lista

                                Dim ListControl = CType(ControlCaptura, ListInputControl)

                                ListControl.ValueValidacionListasDesktopComboBox.ValueMember = "Valor_Campo_Lista_Item"
                                ListControl.ValueValidacionListasDesktopComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
                                ListControl.ValueValidacionListasDesktopComboBox.DataSource = dtvLista
                                ListControl.ValueValidacionListasDesktopComboBox.SelectedValue = RowData.Valor_File_Data
                                ListControl.ValueValidacionListasDesktopComboBox.Refresh()
                            End If

                            ListDefinicionCaptura.Add(DefinicionCaptura)
                            ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                        Case DesktopConfig.CampoTipo.SiNo
                            Dim DefinicionCaptura = New DefinicionCaptura()

                            ControlCaptura = New TextInputControl()

                            ControlCaptura.ValueValidacionListas = RowData.Valor_File_Data

                            DefinicionCaptura.Type = DesktopConfig.CampoTipo.SiNo
                            DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo

                            Dim ListControl = CType(ControlCaptura, ListInputControl)
                            ListControl.ValueValidacionListasDesktopComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                            ListControl.ValueValidacionListasDesktopComboBox.Items.Add("Si")
                            ListControl.ValueValidacionListasDesktopComboBox.Items.Add("No")
                            ListControl.ValueValidacionListasDesktopComboBox.SelectedIndex = 0

                            ListDefinicionCaptura.Add(DefinicionCaptura)
                            ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                        Case DesktopConfig.CampoTipo.Query
                            Dim DefinicionCaptura = New DefinicionCaptura()
                            ControlCaptura = New TextInputControl()

                            DefinicionCaptura.Type = DesktopConfig.CampoTipo.Texto
                            DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo

                            Dim TextControl = CType(ControlCaptura, TextInputControl)
                            TextControl.ValueValidacionListasDesktopTextBox.MaxLength = CampoRow.Length_Campo
                            TextControl.ValueValidacionListasDesktopTextBox.Text = "Automatico"
                            TextControl.ValueValidacionListasDesktopTextBox.Enabled = False

                            ListDefinicionCaptura.Add(DefinicionCaptura)
                            ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                        Case Else
                            Throw New Exception("Tipo de campo no válido para Validacion Listas: " & CampoRow.fk_Campo_Tipo)

                    End Select


                    ControlCaptura.Tipo = CType(CampoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                    ControlCaptura.Etiqueta = CampoRow.Nombre_Campo
                    ControlCaptura.CampoCaptura = CampoCaptura
                    ControlCaptura.ShowPrimaryControls = False
                    ControlCaptura.ShowSecondControls = False
                    ControlCaptura.ShowValidacionListasControls = True


                    CampoCaptura.id = CampoRow.id_Campo
                    CampoCaptura.Control = ControlCaptura
                    CampoCaptura.Marca_Height_Campo = CampoRow.Marca_Height_Campo
                    CampoCaptura.Marca_Width_Campo = CampoRow.Marca_Width_Campo
                    CampoCaptura.Marca_X_Campo = CampoRow.Marca_X_Campo
                    CampoCaptura.Marca_Y_Campo = CampoRow.Marca_Y_Campo
                    CampoCaptura.Usa_Marca = CampoRow.Usa_Marca


                    _Campos.Add(CampoCaptura)

                    If ControlCaptura.Etiqueta.ToString() = "Identificacion" Then
                        CedulaCapturada = ControlCaptura.ValueValidacionListas.ToString()
                    End If

                    If ControlCaptura.Etiqueta.ToString() = "Nombres y Apellidos/ Razon social" Then
                        NombreCapturado = ControlCaptura.ValueValidacionListas.ToString()
                    End If

                    If ControlCaptura.Etiqueta.ToString() = "Procuraduria" Then
                        ValidarProcuraduria = IIf((ControlCaptura.ValueValidacionListas.ToString() = "1"), "SI", "NO")
                    End If
                Next

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("getCampos", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return _Campos
        End Function


        Public Overrides Function CamposLlave(ByVal idDocumento As Integer) As List(Of CampoLlaveCaptura)
            Me._CamposLlave.Clear()
            Return _CamposLlave
        End Function

        Public Overrides Function GetValueFileData(idCampo As Integer) As String
            Return Nothing
        End Function

       Public Overrides Function GetValueLlaveData() As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaRow
            Return Nothing
        End Function

        Public Overrides Function Validaciones(ByVal idDocumento As Integer) As List(Of ValidacionCaptura)
            Me._Validaciones.Clear()

            For Each ValidacionRow As DBImaging.SchemaConfig.CTA_ValidacionRow In ValidacionesDataTable.Select("fk_Documento = " & idDocumento)
                Dim ControlCaptura As New ValidationControl
                Dim ValidacionCaptura As New ValidacionCaptura()

                ControlCaptura.Etiqueta = ValidacionRow.Pregunta_Validacion
                ControlCaptura.ValidacionCaptura = ValidacionCaptura
                ControlCaptura.UsaMotivo = ValidacionRow.Usa_Motivo
                ControlCaptura.EsObligatorio = ValidacionRow.Obligatorio

                If ValidacionRow.Usa_Motivo Then
                    If ValidacionRow.Isfk_Campo_ListaNull Then
                        Throw New Exception("La validacion [" & ValidacionRow.Pregunta_Validacion & "] no tiene un campo lista asociado para los motivos")
                    Else
                        Dim MotivoView As New DataView(MotivosDataTable)
                        MotivoView.RowFilter = MotivosDataTable.fk_Campo_ListaColumn.ColumnName & " = " & ValidacionRow.fk_Campo_Lista
                        ControlCaptura.ValueMember = MotivosDataTable.Valor_Campo_Lista_ItemColumn.ColumnName
                        ControlCaptura.DisplayMember = MotivosDataTable.Etiqueta_Campo_Lista_ItemColumn.ColumnName
                        ControlCaptura.DataSource = MotivoView.ToTable(True)
                    End If
                End If

                ValidacionCaptura.id = ValidacionRow.id_Validacion
                ValidacionCaptura.Control = ControlCaptura
                ValidacionCaptura.Usa_Marca = ValidacionRow.Usa_Marca
                ValidacionCaptura.Marca_X_Campo = ValidacionRow.Marca_X_Campo
                ValidacionCaptura.Marca_Y_Campo = ValidacionRow.Marca_Y_Campo
                ValidacionCaptura.Marca_Width_Campo = ValidacionRow.Marca_Width_Campo
                ValidacionCaptura.Marca_Height_Campo = ValidacionRow.Marca_Height_Campo

                _Validaciones.Add(ValidacionCaptura)
            Next

            Return _Validaciones
        End Function


#End Region

#Region " Metodos "

        Protected Sub LoadConfig(ByRef nDBMCore As DBCore.DBCoreDataBaseManager, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short)
            Me.EsquemaDataTable = nDBMCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, nEsquema)
            Me.DocumentoDataTable = nDBMImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquemaid_DocumentoEliminado(nEntidad, nProyecto, nEsquema, FileCoreRow.fk_Documento, False)

            Dim Orden = New DBImaging.SchemaConfig.CTA_CampoEnumList()
            Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.fk_Documento, True)
            Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.Orden_Campo, True)
            Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.id_Campo, True)
            Me.CamposDataTable = nDBMImaging.SchemaConfig.CTA_Campo.DBFindByfk_Entidadfk_Documentofk_Proyectoid_EsquemaUsa_CapturaEs_Campo_IndexacionEliminado_Campo(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, FileCoreRow.fk_Documento, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, True, Nothing, False, 0, Orden)

            ' Lista Item
            Me.ListaItemsDataTable = nDBMCore.SchemaConfig.PA_Campo_Lista_Item_getDocumento.DBExecute(FileCoreRow.fk_Documento)

            Dim Orden_Validacion = New DBImaging.SchemaConfig.CTA_ValidacionEnumList()
            Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.fk_Documento, True)
            Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.Orden_Validacion, True)
            Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.id_Validacion, True)
            Me.ValidacionesDataTable = nDBMImaging.SchemaConfig.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(FileCoreRow.fk_Documento, DBImaging.EnumEtapaCaptura.Captura, 0, Orden_Validacion)

            Me.TablaAsociadaDataTable = nDBMImaging.SchemaConfig.CTA_Tabla_Asociada.DBFindByfk_Documento(FileCoreRow.fk_Documento)

            ' Leer datos mesa de control
            'Me.MesaControlDataTable = nDBMImaging.SchemaProcess.TBL_File_Data_MC.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, Nothing, Nothing)
            Me.FileDataDataTable = nDBMCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_File(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)

            ' Anexos del documento
            Me.AnexosDataTable = nDBMCore.SchemaImaging.CTA_Documentos.DBFindByfk_Expedientefk_Folder(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

            'Llena los campos lista para la seleccion de motivos
            Me.MotivosDataTable = nDBMCore.SchemaConfig.TBL_Campo_Lista_Item.DBGet(nEntidad, Nothing, Nothing)

            Me.IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
            Me.IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)

            ValidacionListasControl = New Santander.Plugin.ValidacionListas.Controls.ListValidationControl()
            ValidacionListasControl._ValidacionListasController = Me
            Me.IndexerView.SetSearchControl(ValidacionListasControl)
            Me.IndexerView.ShowSaveButton(False)

            CargarCadenasConexion()

        End Sub

        Private Sub InicializarFolio(ByRef nFolio As Folio)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Try
                If (_foliosTable.Count > 0) Then
                    Dim folioActual = _foliosTable(nFolio.GlobalIndex)

                    If (folioActual.EsExterno) Then
                        'Dim thumbnail = ImageManager.GetThumbnailData(folioActual.Filename, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)
                        nFolio.FileName = folioActual.Filename
                        'nFolio.ThumbnailImage = New FreeImageAPI.FreeImageBitmap(New MemoryStream(thumbnail(0)))
                    Else
                        Dim imagen() As Byte = Nothing
                        Dim thumbnail() As Byte = Nothing

                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                        dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                        'manager = New FileProviderManager(folioActual.Expediente, folioActual.Folder, dbmImaging, Me.IndexerSesion.Usuario.id)
                        'manager.Connect()

                        'manager.GetFolio(folioActual.Expediente, folioActual.Folder, folioActual.File, folioActual.Version, folioActual.Folio, imagen, thumbnail)

                        nFolio.FileName = folioActual.Filename ' _TempPath & Guid.NewGuid().ToString() & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                        ' nFolio.ThumbnailImage = New FreeImageAPI.FreeImageBitmap(New MemoryStream(thumbnail))

                        Using fileImage = New FileStream(nFolio.FileName, FileMode.Create)
                            fileImage.Write(imagen, 0, imagen.Length)
                            fileImage.Close()
                        End Using
                    End If
                Else
                    Throw New Exception("No se encontró la imagen en la base de datos, por favor revise la configuración.")
                End If
            Catch ex As Exception
                If (Not View.ViewClosing) Then ShowMessage("InicializarFolio", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing AndAlso manager.ConnectionOpen) Then manager.Disconnect()
            End Try
        End Sub

        Private Sub SendMail(ByVal MailTo As String, ByVal MailCC As String, ByVal MailCCO As String, ByVal Subject As String, ByVal Message As String, nAttachName As String, nAttach As Byte())
            Dim DBMTools As DBTools.DBToolsDataBaseManager = Nothing

            Try
                DBMTools = New DBTools.DBToolsDataBaseManager(Me._ToolsConnectionString)
                DBMTools.Connection_Open()

                DBMTools.InsertMail(1, 137, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Envio de Correo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                DBMTools.Connection_Close()
            End Try
        End Sub

        Private Sub EnviarCorreo()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)

                '------------------Validacion para enviar correos--------------------
                Dim NotificacionDataTable = dbmCore.SchemaConfig.TBL_Notificacion.DBFindByNombre_Notificacion("Santander Listas")
                If NotificacionDataTable.Count = 1 Then
                    Dim NotificacionListasDataTable = dbmCore.SchemaConfig.TBL_Notificacion_Lista.DBFindByfk_NotificacionNombre_Lista(NotificacionDataTable(0).id_Notificacion, "Santander Listas Validacion")
                    If NotificacionListasDataTable.Count = 1 Then
                        Dim CorreoDatatable = dbmCore.SchemaConfig.PA_Busqueda_Parametros_Correo.DBExecute(NotificacionDataTable(0).id_Notificacion, NotificacionListasDataTable(0).id_Notificacion_Lista)
                        If CorreoDatatable.Count = 1 Then
                            Dim Mensaje As String = ""
                            Dim CuerpoCorreo As String = ""
                            Dim Novedades As String = ""
                            CuerpoCorreo = CorreoDatatable(0).CUERPO.Replace("@NumIdent", CedulaCapturada)
                            CuerpoCorreo = CuerpoCorreo.Replace("@Nombre", NombreCapturado)

                            If (RespuestaFactiva = "POSITIVO") Then
                                Novedades = Novedades & "FACTIVA - POSITIVO </BR>"
                            End If
                            If (RespuestaContraloria = "POSITIVO") Then
                                Novedades = Novedades & "CONTRALORIA - POSITIVO </BR>"
                            End If
                            If ((RespuestaProcuraduria = "POSITIVO")) Then
                                Novedades = Novedades & "PROCURADURIA - POSITIVO </BR>"
                            End If
                            If (RespuestaFactiva = "MULTIPLE") Then
                                Novedades = Novedades & "FACTIVA - MULTIPLE </BR>"
                            End If

                            CuerpoCorreo = CuerpoCorreo.Replace("@Novedades", Novedades)

                            Mensaje = CorreoDatatable(0).SALUDO & CuerpoCorreo & CorreoDatatable(0).FIRMA

                            SendMail(CorreoDatatable(0).CORREOS, "", "", CorreoDatatable(0).ASUNTO, Mensaje, "", Nothing)
                        End If
                    End If
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Generacion de correo - Validacion Listas", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

        End Sub

        Private Sub CargarCadenasConexion()
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Security)
                dbmSecurity.Connection_Open(1)
                Dim ModuloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(Nothing)

                If (ModuloDataTable.Count > 0) Then
                    For Each modulo In ModuloDataTable
                        Select Case modulo.id_Modulo
                            Case 27
                                Me._SantanderConnectionString = modulo.ConnectionString
                            Case 3
                                Me._ToolsConnectionString = modulo.ConnectionString
                        End Select
                    Next
                Else
                    Throw New Exception("No se pudo cargar la cadena de conexión para el módulo: " & Program.ModuloId.ToString())
                End If
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Protected Sub InsertarProductividad(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nProductividad As DesktopConfig.ProductividadType, ByVal nEtapa As DesktopConfig.Etapa_Productividad, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short)
            Try
                Dim hora = Now.Hour
                Dim esNocturno = Not (hora >= 6 And hora < 22)
                dbmImaging.SchemaProcess.PA_Insertar_Productividad.DBExecute(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, ExpedienteRow.fk_Entidad, ExpedienteRow.fk_Proyecto, _
                                                                              ExpedienteRow.fk_Esquema, nProductividad.Usuario, 1, nProductividad.Campos, _
                                                                             nProductividad.Caracteres, nProductividad.Validaciones, nEtapa, esNocturno, Nothing, Nothing, nExpediente, nFolder, nFile, nProductividad.Fecha)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Insertar Productividad", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

    End Class

End Namespace
