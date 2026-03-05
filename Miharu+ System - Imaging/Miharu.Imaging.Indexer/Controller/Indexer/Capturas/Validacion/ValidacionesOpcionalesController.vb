Imports System.Threading
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Slyg.Tools
Imports DBImaging.SchemaConfig
Imports System.Windows.Forms
Imports Miharu.FileProvider.Library

Namespace Controller.Indexer.Capturas.Validacion

    Public Class ValidacionesOpcionalesController
        Inherits CapturasController

#Region " Propiedades "

        Protected Overrides ReadOnly Property ProcessName As String
            Get
                Return "Validaciones"
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
            IndexerView.ShowReprocesoButton(True)

            IndexerView.ShowInformationPanel(Me.IndexerImagingGlobal.ProyectoImagingRow.Show_Information)
            IndexerView.ShowDataPanel(False)
            IndexerView.ShowValidationsPanel(True)
            IndexerView.ShowValidationsListasPanel(False)
            IndexerView.ShowToolTipData(True)
            IndexerView.ShowAutoIndexar(False)

            IndexerView.Esquema_Enabled = False
            IndexerView.TipoDocumental_Enabled = False
        End Sub

        Public Overrides ReadOnly Property ShowSecondControls As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides Function AddFolio() As Boolean
            Throw New NotImplementedException()
        End Function

        Public Overrides Function DeleteFolio() As Boolean
            Throw New NotImplementedException()
        End Function

        Public Overrides Function Save() As Boolean
            If (Validar()) Then
                Try
                    Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                    Dim fechaInicioProceso = Now

                    Try
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                        dbmCore = New DBCore.DBCoreDataBaseManager(dbmImaging, Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                        dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                        dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                        'dbmCore.LinkDataBaseManager(dbmImaging)

                        'dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)
                        'dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                        '--------------------------------------------------------------------------------------
                        ' REPORTAR DUPLICADOS
                        '--------------------------------------------------------------------------------------
                        Try
                            Dim fileBloqueadoDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                            If (fileBloqueadoDataTable(0).fk_Usuario_log <> Me.IndexerSesion.Usuario.id) Then
                                dbmImaging.SchemaAudit.PA_Inserta_Seguimiento_Imagenes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, 9999, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.PCName)
                                Return True
                            End If

                        Catch : End Try
                        '--------------------------------------------------------------------------------------

                        'dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)
                        'dbmCore.Transaction_Begin(IsolationLevel.ReadUncommitted)


                        'Almacenar validaciones
                        For Each item In _Validaciones
                            If (dbmCore.SchemaProcess.TBL_File_Validacion.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, FileCoreRow.fk_Documento).Count > 0) Then
                                'dbmCore.SchemaProcess.TBL_File_Validacion.DBUpdate(Nothing, Nothing, Nothing, Nothing, item.Control.Value, Nothing, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id)
                                dbmCore.SchemaProcess.TBL_File_Validacion.DBUpdate(Nothing, Nothing, Nothing, Nothing, item.Control.ValueControl, item.Control.Motivo, FileCoreRow.fk_Documento, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, FileCoreRow.fk_Documento)
                            Else
                                dbmCore.SchemaProcess.TBL_File_Validacion.DBInsert(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, item.Control.ValueControl, Nothing, FileCoreRow.fk_Documento)
                            End If
                        Next

                        Dim productividad As New DesktopConfig.ProductividadType
                        productividad.Usuario = Me.IndexerSesion.Usuario.id
                        productividad.Validaciones = _Validaciones.Count
                        InsertarProductividad(dbmImaging, productividad, DesktopConfig.Etapa_Productividad.Validaciones, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)

                        ' Marcar como procesado
                        Dim updateFile As New DBCore.SchemaImaging.TBL_FileType()
                        updateFile.Validaciones_Opcionales = True
                        dbmCore.SchemaImaging.TBL_File.DBUpdate(updateFile, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)

                        '---------------------------------------------------------------------------
                        ' Actualizar Dashboard
                        '---------------------------------------------------------------------------
                        ' Actualizar validaciones opcionales validaciones opcionales                        
                        dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                        '---------------------------------------------------------------------------

                        'dbmCore.Transaction_Commit()
                        dbmImaging.Transaction_Commit()

                    Catch ex As Exception
                        'If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                        DesktopMessageBoxControl.DesktopMessageShow("Publicar", ex)

                        Return False
                    Finally
                        'If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    End Try

                    EventManager.FinalizarValidaciones(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)

                    '-------------------------------------------------------------------------------------------------------------
                    ' LOGGING - PERFORMANCE
                    '-------------------------------------------------------------------------------------------------------------
                    Dim fechaFinProceso = Now
                    Dim traceMessage As String = ""
                    traceMessage &= "Duración:" & vbTab & (fechaInicioProceso - fechaFinProceso).TotalMilliseconds & vbTab
                    traceMessage &= "Expediente:" & vbTab & FileCoreRow.fk_Expediente & vbTab
                    traceMessage &= "Folder:" & vbTab & FileCoreRow.fk_Folder & vbTab
                    traceMessage &= "File:" & vbTab & FileCoreRow.id_File & vbTab
                    DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[Validaciones][Publicar]")
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


        Public Overrides Sub Unlock()
            If (Not FileCoreRow Is Nothing) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)
                    dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Validaciones.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                Catch ex As Exception
                    Throw

                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If
        End Sub

        Public Overrides Function Campos(ByVal idDocumento As Integer) As List(Of CampoCaptura)
            Me._Campos.Clear()

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

            For Each validacionRow As DBImaging.SchemaConfig.CTA_ValidacionRow In ValidacionesDataTable.Select("fk_Documento = " & idDocumento)
                Dim controlCaptura As New ValidationControl()
                Dim validacionCaptura As New ValidacionCaptura()

                controlCaptura.Etiqueta = validacionRow.Pregunta_Validacion
                controlCaptura.ValidacionCaptura = validacionCaptura
                controlCaptura.UsaMotivo = validacionRow.Usa_Motivo
                controlCaptura.EsObligatorio = validacionRow.Obligatorio

                If validacionRow.Usa_Motivo Then
                    If validacionRow.Isfk_Campo_ListaNull Then
                        Throw New Exception("La validacion [" & validacionRow.Pregunta_Validacion & "] no tiene un campo lista asociado para los motivos")
                    Else
                        Dim motivoView As New DataView(MotivosDataTable)
                        motivoView.RowFilter = MotivosDataTable.fk_Campo_ListaColumn.ColumnName & " = " & validacionRow.fk_Campo_Lista
                        controlCaptura.ValueMember = MotivosDataTable.Valor_Campo_Lista_ItemColumn.ColumnName
                        controlCaptura.DisplayMember = MotivosDataTable.Etiqueta_Campo_Lista_ItemColumn.ColumnName
                        controlCaptura.DataSource = motivoView.ToTable(True)
                    End If
                End If

                validacionCaptura.id = validacionRow.id_Validacion
                validacionCaptura.Control = controlCaptura
                validacionCaptura.Usa_Marca = validacionRow.Usa_Marca
                validacionCaptura.Marca_X_Campo = validacionRow.Marca_X_Campo
                validacionCaptura.Marca_Y_Campo = validacionRow.Marca_Y_Campo
                validacionCaptura.Marca_Width_Campo = validacionRow.Marca_Width_Campo
                validacionCaptura.Marca_Height_Campo = validacionRow.Marca_Height_Campo
                validacionCaptura.Obligatorio = validacionRow.Obligatorio

                _Validaciones.Add(validacionCaptura)
            Next

            Return _Validaciones
        End Function

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
                Dim bloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Validaciones_get.DBExecute(Me.IndexerDesktopGlobal.SesionID)
                If bloqueoDatatable.Count > 0 Then
                    For Each BloqueoRow In bloqueoDatatable
                        dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Validaciones.DBExecute(BloqueoRow.fk_Expediente, BloqueoRow.fk_Folder, BloqueoRow.fk_File)
                    Next
                End If

                ' Calcular siguiente imagen disponible
                Dim bloqueados As Boolean
                If (nIdDocumento Is Nothing) Then
                    bloqueados = dbmImaging.SchemaProcess.PA_Bloqueo_Validacion_Next.DBExecute(ot, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.SesionID, Me.IndexerDesktopGlobal.PCName)
                Else
                    bloqueados = dbmImaging.SchemaProcess.PA_Bloqueo_Validacion_Next_Documento.DBExecute(ot, nIdDocumento, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.SesionID, Me.IndexerDesktopGlobal.PCName)
                End If
                If (Not bloqueados) Then Return False

                bloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Validaciones_get.DBExecute(Me.IndexerDesktopGlobal.SesionID)
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

                    ' Leer el listado de imagenes
                    manager = New FileProviderManager(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, dbmImaging, Me.IndexerSesion.Usuario.id)

                    manager.Connect()
                    If FileImagingRow.Es_Anexo Then
                        _ImageCount = manager.GetFolios(FileImagingRow.fk_Anexo)
                    Else
                        _ImageCount = manager.GetFolios(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)
                    End If


                    View.SetTitle("Validaciones opcionales [Ex: " & FileCoreRow.fk_Expediente & " - Fo: " & FileCoreRow.fk_Folder & " - Fi: " & FileCoreRow.id_File & "]")


                    IndexerView.Information = ""
                    Dim llavesDataTable = dbmCore.SchemaProcess.CTA_Expediente_LLave.DBFindByid_Expediente(FileCoreRow.fk_Expediente)
                    For Each Llave In llavesDataTable
                        IndexerView.Information &= "[" & Llave.Nombre_Proyecto_Llave & "]: " & Llave.Valor_Llave.ToString() & vbCrLf
                    Next

                    ' Anexos del documento
                    AnexosDataTable = dbmCore.SchemaImaging.CTA_Documentos.DBFindByfk_Expedientefk_Folder(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

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

            If (_ImageCount = 0) Then
                Throw New Exception("El File no tiene folios, por favor valide con Soporte. Expediente: " & FileCoreRow.fk_Expediente & " - Folder: " & FileCoreRow.fk_Folder & " - File: " & FileCoreRow.id_File)
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

        Public Overrides Function DocumentosCaptura(ByVal nEsquema As Short) As DBImaging.SchemaProcess.CTA_Documentos_ValidacionesDataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                Return dbmImaging.SchemaProcess.PA_Documentos_Validaciones.DBExecute(Me.IndexerImagingGlobal.Entidad, Me.IndexerImagingGlobal.Proyecto, nEsquema, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return New DBImaging.SchemaProcess.CTA_Documentos_ValidacionesDataTable()
        End Function

        Public Overrides Function ShowDocumentosCaptura() As DialogResult
            Dim selectorForm = New FormDocumentSelector()

            selectorForm.LoadData(Me)

            Dim result = selectorForm.ShowDialog()

            Me.DocumentoCaptura = selectorForm.SelectedValue

            Return result
        End Function

#End Region

#Region " Metodos "

        Protected Overrides Sub LoadConfig(ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short)
            Me.EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, nEsquema)
            Me.DocumentoDataTable = dbmImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquemaid_DocumentoEliminado(nEntidad, nProyecto, nEsquema, FileCoreRow.fk_Documento, False)

            Dim ordenValidacion = New CTA_ValidacionEnumList()
            ordenValidacion.Add(CTA_ValidacionEnum.fk_Documento, True)
            ordenValidacion.Add(CTA_ValidacionEnum.Orden_Validacion, True)

            ValidacionesDataTable = dbmImaging.SchemaConfig.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(FileCoreRow.fk_Documento, DBImaging.EnumEtapaCaptura.Opcional, 0, ordenValidacion)

            'Llena los campos lista para la seleccion de motivos
            MotivosDataTable = New DBCore.SchemaConfig.TBL_Campo_Lista_ItemDataTable()
            dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBFill(MotivosDataTable, nEntidad, Nothing, Nothing)

            IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
            IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)
        End Sub

#End Region

    End Class

End Namespace